using Core.External.Interfaces;
using Core.External.Models.Options;
using Core.External.Services;
using Core.Interfaces;
using Core.Internal.Services;
using Infrastructure.Context;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v.1.0",
        Title = "Event TicketService API Documentation",
        Description = "Documentation for the TicketService API."
    });
    o.EnableAnnotations();
    o.ExampleFilters();

    var apiScheme = new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "X-API-KEY",
        Description = "API KEY",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "ApiKeyScheme",
        Reference = new OpenApiReference
        {
            Id = "ApiKey",
            Type = ReferenceType.SecurityScheme,
        }
    };

    o.AddSecurityDefinition("ApiKey", apiScheme);
    o.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { apiScheme, new List<string>() }
    });
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("AspDB")));

builder.Services.Configure<AzureServiceBusOptions>(builder.Configuration.GetSection("AzureServiceBus"));
builder.Services.Configure<EventCheckingOptions>(builder.Configuration.GetSection("EventApi"));
builder.Services.Configure<UserCheckingOptions>(builder.Configuration.GetSection("UserApi"));
builder.Services.Configure<InvoiceCheckingOptions>(builder.Configuration.GetSection("InvoiceApi"));

builder.Services.AddHttpClient<IEventIdCheckingService, EventIdCheckingService>();
builder.Services.AddHttpClient<IUserIdCheckingService, UserIdCheckingService>();
builder.Services.AddHttpClient<IInvoiceIdCheckingService, InvoiceIdCheckingService>();

builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ITicketService, TicketService>();


var app = builder.Build();

app.MapOpenApi();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Event TicketService API v.1.0");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthorization();
app.MapControllers();

app.Run();
