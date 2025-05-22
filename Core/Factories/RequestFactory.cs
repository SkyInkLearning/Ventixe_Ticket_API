using Core.Domain.Models;

namespace Core.Factories;

public class RequestFactory
{
    public static TicketUserEventKey Create(string userId, string eventId)
    {
        if (userId == null || eventId == null) { return null!; }
        return new TicketUserEventKey()
        {
            UserId = userId,
            EventId = eventId
        };
    }

    public static TicketUserEventSeatKey Create(string userId, string eventId, string seatNumber)
    {
        if (userId == null || eventId == null || seatNumber == null) { return null!; }
        return new TicketUserEventSeatKey()
        {
            UserId = userId,
            EventId = eventId,
            SeatNumber = seatNumber
        };
    }
}
