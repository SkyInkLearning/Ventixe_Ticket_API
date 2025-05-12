using Core.Domain.Entities;
using Core.Interfaces;
using Core.Internal.Services;
using Infrastructure.Context;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace TestingService;

public class TicketServiceTests
{
    private readonly DataContext _context;
    private readonly ITicketService _ticketService;
    private readonly ITicketRepository _ticketRepository;

    public TicketServiceTests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new DataContext(options);
        _ticketRepository = new TicketRepository(_context);
        _ticketService = new TicketService(_ticketRepository);
        _context.Database.EnsureCreated();
    }



    //CREATE
    [Fact]
    public async Task CreateAsync_ShouldReturnTrue_IfValidAddForm()
    {
        //Arrange
        var ticket = ServiceTestData.ValidCreateTicketForm[0];

        //Act
        var result = await _ticketService.CreateTicketAsync(ticket);

        //Assert
        Assert.True(result.Success);
    }
    [Fact]
    public async Task CreateAsync_ShouldReturnFalse_IfInvalidAddForm()
    {
        //Arrange
        _context.Tickets.AddRange(ServiceTestData.ValidTicketEntities);
        await _context.SaveChangesAsync();
        var ticket = ServiceTestData.InvalidCreateTicketForm[0];

        //Act
        var result = await _ticketService.CreateTicketAsync(ticket);

        //Assert
        Assert.False(result.Success);
    }


    //READ
    [Fact]
    public async Task GetAsync_ShouldReturnTrue_IfValidPredicate()
    {
        //Arrange
        _context.Tickets.AddRange(ServiceTestData.ValidTicketEntities);
        await _context.SaveChangesAsync();
        var validKey = ServiceTestData.ValidTicketUserEventSeatKey[0];

        //Act
        var result = await _ticketService.GetTicketAsync(validKey);

        //Assert
        Assert.True(result.Success);
    }
    [Fact]
    public async Task GetAsync_ShouldReturnFalse_IfInvalidPredicate()
    {
        //Arrange
        _context.Tickets.AddRange(ServiceTestData.ValidTicketEntities);
        await _context.SaveChangesAsync();
        var invalidKey = ServiceTestData.InvalidTicketUserEventSeatKey[1];

        //Act
        var result = await _ticketService.GetTicketAsync(invalidKey);

        //Assert
        Assert.False(result.Success);
    }

    [Fact]
    public async Task GetAllUsersTicketsAtEventAsync_ShouldReturnTrue_IfValidPredicate()
    {
        //Arrange
        _context.Tickets.AddRange(ServiceTestData.ValidTicketEntities);
        await _context.SaveChangesAsync();
        var validKey = ServiceTestData.ValidTicketUserEventKey[0];

        //Act
        var result = await _ticketService.GetAllUsersTicketsAtEventAsync(validKey);

        //Assert
        Assert.True(result.Success);
    }
    [Fact]
    public async Task GetAllUsersTicketsAtEventAsync_ShouldReturnFalse_IfInvalidPredicate()
    {
        //Arrange
        _context.Tickets.AddRange(ServiceTestData.ValidTicketEntities);
        await _context.SaveChangesAsync();
        var invalidKey = ServiceTestData.InvalidTicketUserEventKey[1];

        //Act
        var result = await _ticketService.GetAllUsersTicketsAtEventAsync(invalidKey);

        //Assert
        Assert.False(result.Success);
    }

    [Fact]
    public async Task GetAllUsersTicketsAsync_ShouldReturnTrue_IfValidPredicate()
    {
        //Arrange
        _context.Tickets.AddRange(ServiceTestData.ValidTicketEntities);
        await _context.SaveChangesAsync();
        var validPred = "1";

        //Act
        var result = await _ticketService.GetAllUsersTicketsAsync(validPred);

        //Assert
        Assert.True(result.Success);
    }
    [Fact]
    public async Task GetAllUsersTicketsAsync_ShouldReturnFalse_IfInvalidPredicate()
    {
        //Arrange
        _context.Tickets.AddRange(ServiceTestData.ValidTicketEntities);
        await _context.SaveChangesAsync();
        var invalidPred = "6";

        //Act
        var result = await _ticketService.GetAllUsersTicketsAsync(invalidPred);

        //Assert
        Assert.False(result.Success);
    }


    //UPDATE   //Need to add a real DB for this one..
    [Fact]
    public async Task UpdateAsync_ShouldReturnTrue_IfValidEntity()
    {
        //Arrange
        _context.Tickets.AddRange(ServiceTestData.ValidTicketEntities);
        await _context.SaveChangesAsync();
        var updateTicket = ServiceTestData.ValidUpdateTicketForm[0];

        //Act
        var result = await _ticketService.UpdateTicketAsync(updateTicket);

        //Assert
        Assert.True(result.Success);
    }
    [Fact]
    public async Task UpdateAsync_ShouldReturnFalse_IfInvalidEntity()
    {
        //Arrange
        _context.Tickets.AddRange(ServiceTestData.ValidTicketEntities);
        await _context.SaveChangesAsync();
        var updateTicket = ServiceTestData.InvalidUpdateTicketForm[0];

        //Act
        var result = await _ticketService.UpdateTicketAsync(updateTicket);

        //Assert
        Assert.False(result.Success);
    }


    //DELETE
    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_IfValidEntity()
    {
        //Arrange
        _context.Tickets.AddRange(ServiceTestData.ValidTicketEntities);
        await _context.SaveChangesAsync();
        var delete = ServiceTestData.ValidTicketUserEventSeatKey[0];

        //Act
        var result = await _ticketService.DeleteTicketAsync(delete);

        //Assert
        Assert.True(result.Success);
    }
    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_IfInvalidEntity()
    {
        //Arrange
        _context.Tickets.AddRange(ServiceTestData.ValidTicketEntities);
        await _context.SaveChangesAsync();
        var delete = ServiceTestData.InvalidTicketUserEventSeatKey[0];

        //Act
        var result = await _ticketService.DeleteTicketAsync(delete);

        //Assert
        Assert.False(result.Success);
    }

}
