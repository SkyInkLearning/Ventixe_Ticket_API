using Core.External.Models.Response;
using Core.External.Models;
using Microsoft.Extensions.Options;
using System.Text.Json;
using Core.External.Models.Options;
using Core.External.Interfaces;

namespace Core.External.Services;

public class InvoiceIdCheckingService : IInvoiceIdCheckingService
{
    private readonly HttpClient _httpClient;
    private readonly string _invoiceApiUrl;

    public InvoiceIdCheckingService(HttpClient httpClient, IOptions<InvoiceCheckingOptions> options)
    {
        _httpClient = httpClient;
        _invoiceApiUrl = options.Value.Url;
    }

    public async Task<ExternalResponse> InvoiceExistanceCheck(string invoiceId)
    {
        // Need to check the future invoice microservice to figure out how to structure this more.
        // If there is a get one invoice controller then use that instead.

        var response = await _httpClient.GetAsync($"{_invoiceApiUrl}/");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        var invoices = JsonSerializer.Deserialize<List<Invoice>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (invoices == null) { return ExternalResponse.BadRequest("List of invoices returned null."); }

        if (!invoices.Any(i => i.Id.ToString() == invoiceId)) { return ExternalResponse.NotFound("No invoice with that id found."); }

        return ExternalResponse.Ok();
    }


}
