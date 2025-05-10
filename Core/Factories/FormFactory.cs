using Core.Domain.Entities;
using Core.Domain.Models;

namespace Core.Factories;

public class FormFactory
{
    public static TicketEntity Create(CreateTicketForm createForm)
    {
        if (createForm == null) return null!;
        var entity = new TicketEntity()
        {
            EventId = createForm.EventId,
            UserId = createForm.UserId,
            InvoiceId = createForm.InvoiceId,
            TicketCategory = createForm.TicketCategory,
            SeatNumber = createForm.SeatNumber,
            Gate = createForm.Gate
        };
        return entity;
    }
    public static TicketEntity Create(UpdateTicketForm updateForm)
    {
        if (updateForm == null) return null!;
        var entity = new TicketEntity()
        {
            TicketId = updateForm.TicketId,
            EventId = updateForm.EventId,
            UserId = updateForm.UserId,
            InvoiceId = updateForm.InvoiceId,
            TicketCategory = updateForm.TicketCategory,
            SeatNumber = updateForm.SeatNumber,
            Gate = updateForm.Gate
        };
        return entity;
    }









}
