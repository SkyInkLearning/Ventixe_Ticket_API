using Core.External.Models.Response;

namespace Core.External.Interfaces
{
    public interface IUserIdCheckingService
    {
        Task<ExternalResponse> UserExistanceCheck(string userId);
    }
}