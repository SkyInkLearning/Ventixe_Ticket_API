using Core.Domain.Models;
using Core.External.Interfaces;
using Core.Factories;
using Core.Interfaces;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.Extensions.Attributes;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Http;

namespace Presentation.Controllers;

[UseApiKey]
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
    [SwaggerOperation(Summary = "Gets all the tickets at all events for a user.")]
    [SwaggerResponse(200, "Returns a list of tickets with that userId.")]
    [SwaggerResponse(400, "The user does not exist.")]
    [SwaggerResponse(404, "No tickets exists for that user.")]
    public async Task<IActionResult> GetAllUsersTickets(string userId)
    {
        // External checks: (400 if this is active..)
        //var userCheckResult = await _userCheck.UserExistanceCheck(userId);
        //if (!userCheckResult.Success) return BadRequest("No user with this id exists.");

        var tickets = await _ticketService.GetAllUsersTicketsAsync(userId);
        if (tickets == null) { return NotFound("Tickets not found."); }
        
        return Ok(tickets);
    }

    // Getting all the tickets of the user from that event.
    [HttpGet("user/{userId}/event/{eventId}")]
    [SwaggerOperation(Summary = "Gets all the tickets a specific user has at an event.")]
    [SwaggerResponse(200, "Returns a list of tickets with that user and event id.")]
    [SwaggerResponse(400, "Either the eventid or userid sent does not exist.")]
    [SwaggerResponse(400, "Either the eventid or userid was invalid.")]
    [SwaggerResponse(404, "This user does not have any tickets at this event.")]
    public async Task<IActionResult> GetAllUsersTicketsAtEvent(string userId, string eventId)
    {
        var eventCheckResult = await _eventCheck.EventExistanceCheck(eventId);
        if (!eventCheckResult.Success) return BadRequest("No event with this id exists.");

        //var userCheckResult = await _userCheck.UserExistanceCheck(userId);
        //if (!userCheckResult.Success) return BadRequest("No user with this id exists.");

        var ueKey = RequestFactory.Create(userId, eventId);
        if (ueKey == null) { return BadRequest("Invalid information given to the factory."); }

        var tickets = await _ticketService.GetAllUsersTicketsAtEventAsync(ueKey);
        if (tickets == null) { return NotFound("Tickets not found."); }

        return Ok(tickets);
    }

    // Getting one single ticket for a user at one event.
    [HttpGet("user/{userId}/event/{eventId}/seat/{seatNumber}")]
    [SwaggerOperation(Summary = "Gets a single ticket of a user at a specific event.")]
    [SwaggerResponse(200, "Returns a ticket for that specific userid/eventid and seatnumber.")]
    [SwaggerResponse(400, "Either the eventid or userid sent does not exist.")]
    [SwaggerResponse(400, "Either the eventid or userid was invalid.")]
    [SwaggerResponse(404, "This user does not have any tickets at this event.")]
    public async Task<IActionResult> GetATicket(string userId, string eventId, string seatNumber)
    {
        var eventCheckResult = await _eventCheck.EventExistanceCheck(eventId);
        if (!eventCheckResult.Success) return BadRequest("No event with this id exists.");

        //var userCheckResult = await _userCheck.UserExistanceCheck(userId);
        //if (!userCheckResult.Success) return BadRequest("No user with this id exists.");

        var uesKey = RequestFactory.Create(userId, eventId, seatNumber);
        if (uesKey == null) { return BadRequest("Invalid information given to the factory."); }

        var ticket = await _ticketService.GetTicketAsync(uesKey);
        if (ticket == null) { return NotFound("Ticket not found."); }

        return Ok(ticket);
    }

    // Getting all the tickets for an event.
    [HttpGet("event/{eventId}")]
    [SwaggerOperation(Summary = "Gets all tickets at a certain event.")]
    [SwaggerResponse(200, "Returns a list of all tickets that exists at that event.")]
    [SwaggerResponse(400, "The eventid does not exist.")]
    public async Task<IActionResult> GetAllEventTickets(string eventId)
    {
        // External checks:
        var eventCheckResult = await _eventCheck.EventExistanceCheck(eventId);
        if (!eventCheckResult.Success) return BadRequest("No event with this id exists.");

        var tickets = await _ticketService.GetAllTicketsForEvent(eventId);
        return Ok(tickets);
    }
}
