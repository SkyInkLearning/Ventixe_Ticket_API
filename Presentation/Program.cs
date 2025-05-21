using Core.External.Interfaces;
using Core.External.Models.Options;
using Core.External.Services;
using Core.Interfaces;
using Core.Internal.Services;
using Infrastructure.Context;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("AspDB")));

builder.Services.Configure<AzureServiceBusOptions>(builder.Configuration.GetSection("AzureServiceBus"));
builder.Services.Configure<EventCheckingOptions>(builder.Configuration.GetSection("EventApi"));
builder.Services.Configure<UserCheckingOptions>(builder.Configuration.GetSection("UserApi"));
builder.Services.Configure<InvoiceCheckingOptions>(builder.Configuration.GetSection("InvoiceApi"));

builder.Services.AddScoped<IEventIdCheckingService, EventIdCheckingService>();
builder.Services.AddScoped<IUserIdCheckingService, UserIdCheckingService>();
builder.Services.AddScoped<IInvoiceIdCheckingService, InvoiceIdCheckingService>();

builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ITicketService, TicketService>();


var app = builder.Build();

app.MapOpenApi();
app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthorization();
app.MapControllers();

app.Run();
