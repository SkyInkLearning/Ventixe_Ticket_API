using Core.Domain.Entities;
using Core.Domain.Models;

namespace TestingRepository;

public class RepoTestData
{
    public static readonly TicketEntity[] ValidTicketEntities = {
        new TicketEntity
        {
            TicketId = 1,
            EventId = "1",
            UserId = "1",
            InvoiceId = "1",
            TicketCategory = "1",
            SeatNumber = "1B",
            Gate = 2
        },
        new TicketEntity
        {
            TicketId = 2,
            EventId = "1",
            UserId = "2",
            InvoiceId = "2",
            TicketCategory = "2",
            SeatNumber = "2B",
            Gate = 2
        }
    };
    public static readonly TicketEntity[] InvalidTicketEntities = {
        new TicketEntity
        {
            TicketId = 1,
            TicketCategory = "1",
            SeatNumber = "1B",
            Gate = 2
        },
        new TicketEntity
        {
            TicketId = 2,
            EventId = "1",
        }
    };
    public static readonly TicketUserEventSeatKey[] ValidTicketUserEventSeatKey = {
        new TicketUserEventSeatKey
        {
            EventId = "1",
            UserId = "1",
            SeatNumber = "1B",
        },
        new TicketUserEventSeatKey
        {
            EventId = "5",
            UserId = "4",
            SeatNumber = "4B",
        }
    };
    public static readonly TicketUserEventKey[] ValidTicketUserEventKey = {
        new TicketUserEventKey
        {
            EventId = "1",
            UserId = "1",
        },
        new TicketUserEventKey
        {
            EventId = "5",
            UserId = "2",
        }
    };

    public static readonly TicketUserEventSeatKey[] InvalidTicketUserEventSeatKey = {
        new TicketUserEventSeatKey
        {
            EventId = "4",
            UserId = "3",
            SeatNumber = "16B",
        },
        new TicketUserEventSeatKey
        {
            EventId = "5",
            UserId = "4",
            SeatNumber = "4B",
        }
    };
    public static readonly TicketUserEventKey[] InvalidTicketUserEventKey = {
        new TicketUserEventKey
        {
            EventId = "5",
            UserId = "2",
        },
        new TicketUserEventKey
        {
            EventId = "7",
            UserId = "8",
        }
    };
}