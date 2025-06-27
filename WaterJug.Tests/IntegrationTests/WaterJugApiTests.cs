using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text;
using System.Text.Json;
using WaterJug.Tests.Utils;
using Xunit;

[Collection("ApiTestCollection")]
public class WaterJugApiTests
{
    private readonly HttpClient _client;

    public WaterJugApiTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }


    /// <summary>
    /// Integration test to verify that a valid water jug problem is accepted
    /// </summary>
    [Fact]
    public async Task PostSolve_ReturnsSolution()
    {
        var payload = new
        {
            jug1Capacity = 2,
            jug2Capacity = 10,
            target = 4
        };

        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("/api/waterjugChallenge", content);
        var body = await response.Content.ReadAsStringAsync();
        Console.WriteLine("BODY: " + body);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(body.Contains("\"success\":true") && body.Contains("\"solution\""));
    }

    /// <summary>
    /// Integration test that confirms the history endpoint returns a list of solved records
    /// </summary>
    [Fact]
    public async Task GetHistory_ReturnsRecordsWhenAvailable()
    {
        var payload = new { jug1Capacity = 3, jug2Capacity = 5, target = 4 };
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var solve = await _client.PostAsync("/api/waterjugChallenge", content);
        Assert.Equal(HttpStatusCode.OK, solve.StatusCode);

        var response = await _client.GetAsync("/api/history");
        var json = await response.Content.ReadAsStringAsync();
        var doc = JsonDocument.Parse(json);
        var data = doc.RootElement.GetProperty("data");

        Assert.True(data.GetArrayLength() > 0);
    }

}