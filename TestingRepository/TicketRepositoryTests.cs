using Core.Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TestingRepository;

public class TicketRepositoryTests
{
    private readonly DataContext _context;
    private readonly TicketRepository _ticketRepository;

    public TicketRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new DataContext(options);
        _ticketRepository = new TicketRepository(_context);
        _context.Database.EnsureCreated();
    }




    //CREATE
    [Fact]
    public async Task CreateAsync_ShouldReturnTrue_IfValidEntity()
    {
        //Arrange
        var entity = RepoTestData.ValidTicketEntities[0];

        //Act
        var result = await _ticketRepository.CreateAsync(entity);

        //Assert
        Assert.True(result.Success);
    }
    [Fact]
    public async Task CreateAsync_ShouldReturnFalse_IfInvalidEntity()
    {
        //Arrange
        _context.Tickets.AddRange(RepoTestData.ValidTicketEntities);
        await _context.SaveChangesAsync();
        var entity = RepoTestData.ValidTicketEntities[0];

        //Act
        var result = await _ticketRepository.CreateAsync(entity);

        //Assert
        Assert.False(result.Success);
    }


    //READ
    [Fact]
    public async Task ExistsAsync_ShouldReturnTrue_IfValidPredicate()
    {
        //Arrange
        _context.Tickets.AddRange(RepoTestData.ValidTicketEntities);
        await _context.SaveChangesAsync();
        var validTicketId = "1";

        //Act
        var result = await _ticketRepository.ExistsAsync(Ticket => Ticket.EventId == validTicketId);

        //Assert
        Assert.True(result.Success);
    }
    [Fact]
    public async Task ExistsAsync_ShouldReturnFalse_IfInvalidPredicate()
    {
        //Arrange
        _context.Tickets.AddRange(RepoTestData.ValidTicketEntities);
        await _context.SaveChangesAsync();
        var inValidTicketId = "";

        //Act
        var result = await _ticketRepository.ExistsAsync(Ticket => Ticket.EventId == inValidTicketId);

        //Assert
        Assert.False(result.Success);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnTrue_IfValidPredicate()
    {
        //Arrange
        _context.Tickets.AddRange(RepoTestData.ValidTicketEntities);
        await _context.SaveChangesAsync();
        var validKey = RepoTestData.ValidTicketUserEventSeatKey[0];

        //Act
        var result = await _ticketRepository.GetAsync(ticket => ticket.UserId == validKey.UserId && ticket.EventId == validKey.EventId && ticket.SeatNumber == validKey.SeatNumber);

        //Assert
        Assert.True(result.Success);
    }
    [Fact]
    public async Task GetAsync_ShouldReturnFalse_IfInvalidPredicate()
    {
        //Arrange
        _context.Tickets.AddRange(RepoTestData.ValidTicketEntities);
        await _context.SaveChangesAsync();
        var validKey = RepoTestData.InvalidTicketUserEventSeatKey[1];

        //Act
        var result = await _ticketRepository.GetAsync(ticket => ticket.UserId == validKey.UserId && ticket.EventId == validKey.EventId && ticket.SeatNumber == validKey.SeatNumber);

        //Assert
        Assert.False(result.Success);
    }

    [Fact]
    public async Task GetAllUsersTicketsAtEventAsync_ShouldReturnTrue_IfValidPredicate()
    {
        //Arrange
        _context.Tickets.AddRange(RepoTestData.ValidTicketEntities);
        await _context.SaveChangesAsync();
        var validKey = RepoTestData.ValidTicketUserEventKey[0];

        //Act
        var result = await _ticketRepository.GetAllUsersTicketsAtEventAsync(entity => entity.EventId == validKey.EventId && entity.UserId == validKey.UserId);

        //Assert
        Assert.True(result.Success);
    }
    [Fact]
    public async Task GetAllUsersTicketsAtEventAsync_ShouldReturnFalse_IfInvalidPredicate()
    {
        //Arrange
        _context.Tickets.AddRange(RepoTestData.ValidTicketEntities);
        await _context.SaveChangesAsync();
        var validKey = RepoTestData.InvalidTicketUserEventKey[1];

        //Act
        var result = await _ticketRepository.GetAllUsersTicketsAtEventAsync(entity => entity.EventId == validKey.EventId && entity.UserId == validKey.UserId);

        //Assert
        Assert.False(result.Success);
    }

    [Fact]
    public async Task GetAllUsersTicketsAsync_ShouldReturnTrue_IfValidPredicate()
    {
        //Arrange
        _context.Tickets.AddRange(RepoTestData.ValidTicketEntities);
        await _context.SaveChangesAsync();
        var validUserId = "1";

        //Act
        var result = await _ticketRepository.GetAllUsersTicketsAsync(entity => entity.UserId == validUserId);

        //Assert
        Assert.True(result.Success);
    }
    [Fact]
    public async Task GetAllUsersTicketsAsync_ShouldReturnFalse_IfInvalidPredicate()
    {
        //Arrange
        _context.Tickets.AddRange(RepoTestData.ValidTicketEntities);
        await _context.SaveChangesAsync();
        var invalidUserId = "8585";

        //Act
        var result = await _ticketRepository.GetAllUsersTicketsAsync(entity => entity.UserId == invalidUserId);

        //Assert
        Assert.False(result.Success);
    }


    //UPDATE
    [Fact]
    public async Task UpdateAsync_ShouldReturnTrue_IfValidEntity()
    {
        //Arrange
        _context.Tickets.AddRange(RepoTestData.ValidTicketEntities);
        await _context.SaveChangesAsync();
        var validTicketId = 1;

        //Act
        var entity = await _ticketRepository.GetAsync(entity => entity.TicketId == validTicketId);
        var updateEntity = entity.Content;
        updateEntity.Gate = 36;

        var result = await _ticketRepository.UpdateAsync(updateEntity);

        //Assert
        Assert.True(result.Success);
    }
    [Fact]
    public async Task UpdateAsync_ShouldReturnFalse_IfInvalidEntity()
    {
        //Arrange
        _context.Tickets.AddRange(RepoTestData.ValidTicketEntities);
        await _context.SaveChangesAsync();
        var inValidEntity = new TicketEntity
        {
            TicketId = 666
        };

        //Act
        var result = await _ticketRepository.UpdateAsync(inValidEntity);

        //Assert
        Assert.False(result.Success);
    }


    //DELETE
    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_IfValidEntity()
    {
        //Arrange
        _context.Tickets.AddRange(RepoTestData.ValidTicketEntities);
        await _context.SaveChangesAsync();
        var validEntity = RepoTestData.ValidTicketEntities[0];

        //Act
        var result = await _ticketRepository.DeleteAsync(validEntity);

        //Assert
        Assert.True(result.Success);
    }
    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_IfInvalidEntity()
    {
        //Arrange
        _context.Tickets.AddRange(RepoTestData.ValidTicketEntities);
        await _context.SaveChangesAsync();
        var invalidEntity = RepoTestData.InvalidTicketEntities[1];

        //Act
        var result = await _ticketRepository.DeleteAsync(invalidEntity);

        //Assert
        Assert.False(result.Success);
    }
}
