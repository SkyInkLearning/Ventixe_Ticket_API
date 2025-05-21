using Core.External.Models.Response;

namespace Core.External.Interfaces
{
    public interface IEventIdCheckingService
    {
        Task<ExternalResponse> EventExistanceCheck(string eventId);
    }
}