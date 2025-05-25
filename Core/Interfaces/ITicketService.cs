using Core.Domain.Models;
using Core.Domain.Response;

namespace Core.Interfaces
{
    public interface ITicketService
    {
        Task<ServiceResponse> CreateTicketAsync(CreateTicketForm createTicketForm);
        Task<ServiceResponse> DeleteTicketAsync(TicketUserEventSeatKey key);
        Task<ServiceResponse<IEnumerable<Ticket>>> GetAllTicketsForEvent(string eventId);
        Task<ServiceResponse<IEnumerable<Ticket>>> GetAllUsersTicketsAsync(string userId);
        Task<ServiceResponse<IEnumerable<Ticket>>> GetAllUsersTicketsAtEventAsync(TicketUserEventKey key);
        Task<ServiceResponse<Ticket>> GetTicketAsync(TicketUserEventSeatKey key);
        Task<ServiceResponse> UpdateTicketAsync(UpdateTicketForm updateTicketForm);
    }
}