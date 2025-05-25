using Core.Domain.Entities;
using Core.Domain.Response;
using System.Linq.Expressions;

namespace Infrastructure.Repository;

public interface ITicketRepository : IBaseRepository<TicketEntity>
{
    Task<RepositoryResponse<IEnumerable<TicketEntity>>> GetAllUsersTicketsAtEventAsync(Expression<Func<TicketEntity, bool>> expression);
    Task<RepositoryResponse<IEnumerable<TicketEntity>>> GetAllUsersTicketsAsync(Expression<Func<TicketEntity, bool>> expression);
    Task<RepositoryResponse<IEnumerable<TicketEntity>>> GetAllTicketsAtEventAsync(Expression<Func<TicketEntity, bool>> expression);
}