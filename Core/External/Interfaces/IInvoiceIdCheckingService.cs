using Core.External.Models.Response;

namespace Core.External.Interfaces
{
    public interface IInvoiceIdCheckingService
    {
        Task<ExternalResponse> InvoiceExistanceCheck(string invoiceId);
    }
}