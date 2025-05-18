using Azure.Messaging.ServiceBus;

namespace Presentation.ServiceBus;

public class ServiceBusReceiver
{
    public async Task Receiver()
    {
        var connectionString = "";
        var queName = "";

        var client = new ServiceBusClient(connectionString);
        var processor = client.CreateProcessor(queName, new ServiceBusProcessorOptions());

        processor.ProcessMessageAsync += async args =>
        {
            var body = args.Message.Body.ToString();

            await args.CompleteMessageAsync(args.Message);
        };

        processor.ProcessErrorAsync += args =>
        {
            return Task.CompletedTask;
        };

        await processor.StartProcessingAsync();
    }
}
