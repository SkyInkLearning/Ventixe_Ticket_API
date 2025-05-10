using Core.Domain.Models;
using Core.Domain.Response;
using Core.Factories;
using Infrastructure.Repository;

namespace Core.Internal.Services;

public class TicketService(ITicketRepository ticketRepository)
{
    private readonly ITicketRepository _ticketRepository = ticketRepository;



    public async Task<ServiceResponse> CreateTicketAsync(CreateTicketForm createTicketForm)
    {
        try
        {
            if (createTicketForm == null) { return ServiceResponse.BadRequest("Create form is null."); }

            var entity = FormFactory.Create(createTicketForm);
            if (entity == null) { return ServiceResponse.BadRequest("Entity is null."); }

            var result = await _ticketRepository.CreateAsync(entity);
            if (!result.Success) { return ServiceResponse.Error(result.Message); }

            return ServiceResponse.Ok();
        }
        catch (Exception ex) { return ServiceResponse.Error(ex.Message); }
    }
    public async Task<ServiceResponse<Ticket>> GetTicketAsync(TicketUserEventSeatKey key)
    {
        try
        {
            if (key == null) { return ServiceResponse<Ticket>.BadRequest("Key is null.", null); }

            var result = await _ticketRepository.GetAsync(ticket => ticket.UserId == key.UserId && ticket.EventId == key.EventId && ticket.SeatNumber == key.SeatNumber);
            if (result.Content == null) { return ServiceResponse<Ticket>.BadRequest("The entity is null.", null); }

            var ticket = TicketFactory.Create(result.Content);
            return ServiceResponse<Ticket>.Ok(ticket);
        }
        catch (Exception ex) { return ServiceResponse<Ticket>.Error(ex.Message, null); }
    }
    public async Task<ServiceResponse<IEnumerable<Ticket>>> GetAllUsersTicketsAtEventAsync(TicketUserEventKey key)
    {
        try
        {
            if (key == null) { return ServiceResponse<IEnumerable<Ticket>>.BadRequest("Key is null.", null); }

            var result = await _ticketRepository.GetAllUsersTicketsAtEventAsync(ticket => ticket.UserId == key.UserId && ticket.EventId == key.EventId);
            if (result.Content == null) { return ServiceResponse<IEnumerable<Ticket>>.BadRequest("The list of ticket entities is null.", null); }

            var tickets = result.Content.Select(TicketFactory.Create);
            return ServiceResponse<IEnumerable<Ticket>>.Ok(tickets);
        }
        catch (Exception ex) { return ServiceResponse<IEnumerable<Ticket>>.Error(ex.Message, null); }
    }
    public async Task<ServiceResponse<IEnumerable<Ticket>>> GetAllUsersTicketsAsync(string userId)
    {
        try
        {
            if (userId == null) { return ServiceResponse<IEnumerable<Ticket>>.BadRequest("Something in the data given is null.", null); }

            var result = await _ticketRepository.GetAllUsersTicketsAsync(ticket => ticket.UserId == userId);
            if (result.Content == null) { return ServiceResponse<IEnumerable<Ticket>>.BadRequest("The list of ticket entities is null.", null); }

            var tickets = result.Content.Select(TicketFactory.Create);
            return ServiceResponse<IEnumerable<Ticket>>.Ok(tickets);
        }
        catch (Exception ex) { return ServiceResponse<IEnumerable<Ticket>>.Error(ex.Message, null); }
    }

    public async Task<ServiceResponse> UpdateTicketAsync(UpdateTicketForm updateTicketForm)
    {
        try
        {
            if (updateTicketForm == null) { return ServiceResponse.BadRequest("Update form is null."); }

            var entity = FormFactory.Create(updateTicketForm);
            if (entity == null) { return ServiceResponse.BadRequest("Entity is null."); }

            var result = await _ticketRepository.UpdateAsync(entity);
            if (!result.Success) { return ServiceResponse.Error(result.Message); }

            return ServiceResponse.Ok();
        }
        catch (Exception ex) { return ServiceResponse.Error(ex.Message); }
    }

    public async Task<ServiceResponse> DeleteTicketAsync(TicketUserEventSeatKey key)
    {
        try
        {
            if (key == null) { return ServiceResponse.BadRequest("The key is null."); }

            var entity = await _ticketRepository.GetAsync(ticket => ticket.UserId == key.UserId && ticket.EventId == key.EventId && ticket.SeatNumber == key.SeatNumber);
            if (entity.Content == null) { return ServiceResponse.BadRequest("Entity is null."); }

            var result = await _ticketRepository.DeleteAsync(entity.Content);
            if (!result.Success) { return ServiceResponse.Error(result.Message); }

            return ServiceResponse.Ok();
        }
        catch (Exception ex) { return ServiceResponse.Error(ex.Message); }
    }
}
