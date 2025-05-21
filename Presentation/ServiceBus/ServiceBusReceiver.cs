using Azure.Messaging.ServiceBus;
using Core.Domain.Models;
using Core.External.Models.Options;
using Core.Interfaces;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Presentation.ServiceBus;

public class ServiceBusReceiver : IAsyncDisposable
{
    private readonly ServiceBusClient _client;
    private readonly ServiceBusProcessor _processor;
    private readonly ITicketService _ticketService;

    public ServiceBusReceiver(IOptions<AzureServiceBusOptions> options, ITicketService ticketService)
    {
        var config = options.Value;
        _client = new ServiceBusClient(config.ConnectionString);
        _processor = _client.CreateProcessor(config.QueueName, new ServiceBusProcessorOptions());

        _processor.ProcessMessageAsync += HandleMessageAsync;
        _processor.ProcessErrorAsync += HandleErrorAsync;

        _ticketService = ticketService;
    }

    public async Task StartAsync()
    {
        await _processor.StartProcessingAsync();
    }
    public async Task StopAsync()
    {
        await _processor.StopProcessingAsync();
    }

    private async Task HandleMessageAsync(ProcessMessageEventArgs args)
    {
        var json = args.Message.Body.ToString();

        try
        {
            var wrapper = JsonSerializer.Deserialize<ServiceBusWrapper>(json);
            if (wrapper == null) { throw new InvalidOperationException("Message recieved is null."); }

            switch (wrapper.Type) 
            {
                case "CreateTicket":
                    var createModel = JsonSerializer.Deserialize<CreateTicketForm>(wrapper.Payload.GetRawText());
                    if (createModel == null) { throw new InvalidOperationException("The payload recieved is null."); }

                    await _ticketService.CreateTicketAsync(createModel);
                    break;
                case "UpdateTicket":
                    var updateModel = JsonSerializer.Deserialize<UpdateTicketForm>(wrapper.Payload.GetRawText());
                    if (updateModel == null) { throw new InvalidOperationException("The payload recieved is null."); }

                    await _ticketService.UpdateTicketAsync(updateModel);
                    break;
                case "DeleteTicket":
                    var deleteModel = JsonSerializer.Deserialize<TicketUserEventSeatKey>(wrapper.Payload.GetRawText());
                    if (deleteModel == null) { throw new InvalidOperationException("The payload recieved is null."); }

                    await _ticketService.DeleteTicketAsync(deleteModel);
                    break;

                default:
                    throw new InvalidOperationException($"{wrapper.Type} not valid.");
            }

            await args.CompleteMessageAsync(args.Message);

        }
        catch (Exception ex) { await args.AbandonMessageAsync(args.Message); }
    }

    private Task HandleErrorAsync(ProcessErrorEventArgs args)
    {
        return Task.CompletedTask;
    }
    public async ValueTask DisposeAsync()
    {
        await _processor.DisposeAsync();
        await _client.DisposeAsync();
    }
    private class ServiceBusWrapper
    {
        public string Type { get; set; } = null!;
        public JsonElement Payload { get; set; }
    }
}
