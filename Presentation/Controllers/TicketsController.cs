using Core.Domain.Models;
using Core.External.Interfaces;
using Core.Factories;
using Core.Interfaces;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketsController(ITicketService ticketService, IEventIdCheckingService eventCheck, IUserIdCheckingService userCheck, IInvoiceIdCheckingService invoiceCheck) : ControllerBase
{
    private readonly ITicketService _ticketService = ticketService;

    private readonly IEventIdCheckingService _eventCheck = eventCheck;
    private readonly IUserIdCheckingService _userCheck = userCheck;
    private readonly IInvoiceIdCheckingService _invoiceCheck = invoiceCheck;

    // Getting all the tickets that the user has for all events.
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetAllUsersTickets(string userId)
    {
        // External checks:
        var userCheckResult = await _userCheck.UserExistanceCheck(userId);
        if (!userCheckResult.Success) return BadRequest("No user with this id exists.");

        var tickets = await _ticketService.GetAllUsersTicketsAsync(userId);
        if (tickets == null) { return NotFound("Tickets not found."); }
        
        return Ok(tickets);
    }

    // Getting all the tickets of the user from that event.
    [HttpGet("user/{userId}/event/{eventId}")]
    public async Task<IActionResult> GetAllUsersTicketsAtEvent(string userId, string eventId)
    {
        var eventCheckResult = await _eventCheck.EventExistanceCheck(eventId);
        if (!eventCheckResult.Success) return BadRequest("No event with this id exists.");

        var userCheckResult = await _userCheck.UserExistanceCheck(userId);
        if (!userCheckResult.Success) return BadRequest("No user with this id exists.");

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
        var eventCheckResult = await _eventCheck.EventExistanceCheck(eventId);
        if (!eventCheckResult.Success) return BadRequest("No event with this id exists.");

        var userCheckResult = await _userCheck.UserExistanceCheck(userId);
        if (!userCheckResult.Success) return BadRequest("No user with this id exists.");

        var uesKey = RequestFactory.Create(userId, eventId, seatNumber);
        if (uesKey == null) { return BadRequest("Invalid information given to the factory."); }

        var ticket = await _ticketService.GetTicketAsync(uesKey);
        if (ticket == null) { return NotFound("Ticket not found."); }

        return Ok(ticket);
    }

    // Getting all the tickets for an event.
    [HttpGet("event/{eventId}")]
    public async Task<IActionResult> GetAllEventTickets(string eventId)
    {
        // External checks:
        var eventCheckResult = await _eventCheck.EventExistanceCheck(eventId);
        if (!eventCheckResult.Success) return BadRequest("No event with this id exists.");

        var tickets = await _ticketService.GetAllTicketsForEvent(eventId);
        return Ok(tickets);
    }
}
