using System.Text.Json;
using AIToolSelector.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
builder.Services.AddHttpClient();
List<Tool> tools = new();

if (File.Exists("Data/tools.json"))
{
    var json = File.ReadAllText("Data/tools.json");
    tools = JsonSerializer.Deserialize<List<Tool>>(json) ?? new List<Tool>();
}

// list all tools
app.MapGet("/api/tools", () => tools);

// recommendation logic
app.MapGet("/api/recommend", (string task, string price, string level) =>
{
    var ranked = tools
        .Select(t => new
        {
            Tool = t,
            Score =
                (t.Task.Equals(task, StringComparison.OrdinalIgnoreCase) ? 3 : 0) +
                (t.Price.Equals(price, StringComparison.OrdinalIgnoreCase) ? 2 : 0) +
                (t.Level.Equals(level, StringComparison.OrdinalIgnoreCase) ? 1 : 0)
        })
        .OrderByDescending(x => x.Score)
        .ToList();

    if (ranked.Count == 0)
        return Results.NotFound("No tools available");

    return ranked.Take(3).Select(x => x.Tool);
});

app.Run();