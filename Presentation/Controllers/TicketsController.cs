using Core.Domain.Models;
using Core.Factories;
using Core.Interfaces;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketsController(ITicketService ticketService) : ControllerBase
{
    private readonly ITicketService _ticketService = ticketService;

    // Getting all the tickets that the user has for all events.
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetAllUsersTickets(string userId)
    {
        var tickets = await _ticketService.GetAllUsersTicketsAsync(userId);
        if (tickets == null) { return NotFound("Tickets not found."); }
        
        return Ok(tickets);
    }

    // Getting all the tickets of the user from that event.
    [HttpGet("user/{userId}/event/{eventId}")]
    public async Task<IActionResult> GetAllUsersTicketsAtEvent(string userId, string eventId)
    {
        var ueKey = RequestFactory.Create(userId, eventId);
        if (ueKey == null) { return BadRequest("Invalid information given to the factory."); }

        var tickets = await _ticketService.GetAllUsersTicketsAtEventAsync(ueKey);
        if (tickets == null) { return NotFound("Tickets not found."); }

        return Ok(tickets);
    }

    // Getting one single ticket for a user at one event.
    [HttpGet("user/{userId}/event/{eventId}/seat/{seatNumber}")]
    public async Task<IActionResult> GetATicket(string userId, string eventId, string seatNumber)
    {
        var uesKey = RequestFactory.Create(userId, eventId, seatNumber);
        if (uesKey == null) { return BadRequest("Invalid information given to the factory."); }

        var ticket = await _ticketService.GetTicketAsync(uesKey);
        if (ticket == null) { return NotFound("Ticket not found."); }

        return Ok(ticket);
    }
}
