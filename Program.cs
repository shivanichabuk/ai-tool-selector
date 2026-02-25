using System.Text.Json;
using AIToolSelector.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddHttpClient();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

List<Tool> tools = new();

var path = Path.Combine(AppContext.BaseDirectory, "Data", "tools.json");

if (File.Exists(path))
{
    var json = File.ReadAllText(path);
   var options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};

tools = JsonSerializer.Deserialize<List<Tool>>(json, options) ?? new();
}

// GET all tools
app.MapGet("/api/tools", () => tools);

app.MapGet("/api/recommend", (string task, string price, string level) =>
{
    var ranked = tools
        .Select(t => new
        {
            Tool = t,
            Score =
                (t.Task?.ToLower() == task.ToLower() ? 3 : 0) +
                (t.Price?.ToLower() == price.ToLower() ? 2 : 0) +
                (t.Level?.ToLower() == level.ToLower() ? 1 : 0)
        })
        .OrderByDescending(x => x.Score)
        .Select(x => x.Tool)
        .Take(3)
        .ToList();

    return ranked;
});
app.Run();