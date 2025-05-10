using Core.Domain.Entities;
using Core.Domain.Models;

namespace Core.Factories;

public class TicketFactory
{
    public static Ticket Create(TicketEntity entity)
    {
        if (entity == null) return null!;

        var ticket = new Ticket() 
        {
            TicketId = entity.TicketId,
            EventId = entity.EventId,
            UserId = entity.UserId,
            InvoiceId = entity.InvoiceId,
            TicketCategory = entity.TicketCategory,
            SeatNumber = entity.SeatNumber,
            Gate = entity.Gate
        };
        return ticket;
    }




}
