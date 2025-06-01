using Core.External.Interfaces;
using Core.External.Models;
using Core.External.Models.Options;
using Core.External.Models.Response;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Core.External.Services;

public class EventIdCheckingService : IEventIdCheckingService
{
    // Entire method made with help from chatgpt to bring in all the events, as there is no controller to
    // only get one event, to validate if the eventid sent is located among the events.

    private readonly HttpClient _httpClient;
    private readonly string _eventApiUrl;

    public EventIdCheckingService(HttpClient httpClient, IOptions<EventCheckingOptions> options)
    {
        _httpClient = httpClient;
        _eventApiUrl = options.Value.Url;
    }

    public async Task<ExternalResponse> EventExistanceCheck(string eventId)
    {
        var response = await _httpClient.GetAsync($"{_eventApiUrl}/events");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        var events = JsonSerializer.Deserialize<List<Event>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (events == null) { return ExternalResponse.BadRequest("List of events returned null."); }

        if (!events.Any(e => e.Id.ToString() == eventId)) { return ExternalResponse.NotFound("No event with that id found."); }

        return ExternalResponse.Ok();
    }
}
