using Core.Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Repository;

public class TicketRepository(DataContext context) : BaseRepository<TicketEntity>(context)
{
}
