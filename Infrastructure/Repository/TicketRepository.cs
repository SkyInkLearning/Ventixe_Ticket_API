using Core.Domain.Entities;
using Core.Domain.Response;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository;

public class TicketRepository(DataContext context) : BaseRepository<TicketEntity>(context), ITicketRepository
{


    public virtual async Task<RepositoryResponse<IEnumerable<TicketEntity>>> GetAllUsersTicketsAtEventAsync(Expression<Func<TicketEntity, bool>> expression)
    {
        try
        {
            if (expression == null) return RepositoryResponse<IEnumerable<TicketEntity>>.BadRequest("Expression is null.", null);

            var exists = await ExistsAsync(expression);
            if (!exists.Success) return RepositoryResponse<IEnumerable<TicketEntity>>.BadRequest("Expression is null.", null);

            var entities = await _dbSet.Where(expression).ToListAsync();

            return RepositoryResponse<IEnumerable<TicketEntity>>.Ok(entities);
        }
        catch (Exception ex)
        { return RepositoryResponse<IEnumerable<TicketEntity>>.Error(ex.Message, null); }
    }
    public virtual async Task<RepositoryResponse<IEnumerable<TicketEntity>>> GetAllUsersTicketsAsync(Expression<Func<TicketEntity, bool>> expression)
    {
        try
        {
            if (expression == null) return RepositoryResponse<IEnumerable<TicketEntity>>.BadRequest("Expression is null.", null);

            var exists = await ExistsAsync(expression);
            if (!exists.Success) return RepositoryResponse<IEnumerable<TicketEntity>>.BadRequest("Expression is null.", null);

            var entities = await _dbSet.Where(expression).ToListAsync();

            return RepositoryResponse<IEnumerable<TicketEntity>>.Ok(entities);
        }
        catch (Exception ex)
        { return RepositoryResponse<IEnumerable<TicketEntity>>.Error(ex.Message, null); }
    }

    public virtual async Task<RepositoryResponse<IEnumerable<TicketEntity>>> GetAllTicketsAtEventAsync(Expression<Func<TicketEntity, bool>> expression)
    {
        try
        {
            if (expression == null) return RepositoryResponse<IEnumerable<TicketEntity>>.BadRequest("Expression is null.", null);

            var exists = await ExistsAsync(expression);
            if (!exists.Success) return RepositoryResponse<IEnumerable<TicketEntity>>.BadRequest("Expression is null.", null);

            var entities = await _dbSet.Where(expression).ToListAsync();

            return RepositoryResponse<IEnumerable<TicketEntity>>.Ok(entities);
        }
        catch (Exception ex)
        { return RepositoryResponse<IEnumerable<TicketEntity>>.Error(ex.Message, null); }
    }
}
