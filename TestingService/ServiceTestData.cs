using Azure.Core;
using Core.Domain.Entities;
using Core.Domain.Models;

namespace TestingService;

public class ServiceTestData
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

    public static readonly CreateTicketForm[] ValidCreateTicketForm = {
        new CreateTicketForm
        {
            EventId = "1",
            UserId = "1",
            InvoiceId = "1",
            TicketCategory = "1",
            SeatNumber = "1B",
            Gate = 2
        },
        new CreateTicketForm
        {
            EventId = "1",
            UserId = "1",
            InvoiceId = "1",
            TicketCategory = "1",
            SeatNumber = "1B",
            Gate = 2
        }
    };
    public static readonly CreateTicketForm[] InvalidCreateTicketForm = {
        new CreateTicketForm
        {
            EventId = "5",
            UserId = "2",
        },
        new CreateTicketForm
        {
            EventId = "7",
            UserId = "8",
        }
    };


    public static readonly UpdateTicketForm[] ValidUpdateTicketForm = {
        new UpdateTicketForm
        {
            TicketId = 1,
            EventId = "1",
            UserId = "2",
            InvoiceId = "3",
            TicketCategory = "1",
            SeatNumber = "15B",
            Gate = 2
        },
        new UpdateTicketForm
        {
            TicketId = 2,
            EventId = "1",
            UserId = "4",
            InvoiceId = "5",
            TicketCategory = "1",
            SeatNumber = "16B",
            Gate = 2
        }
    };
    public static readonly UpdateTicketForm[] InvalidUpdateTicketForm = {
        new UpdateTicketForm
        {
            EventId = "5",
            UserId = "2",
        },
        new UpdateTicketForm
        {
            EventId = "7",
            UserId = "8",
        }
    };
}
