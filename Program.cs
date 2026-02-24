using System.Text.Json;
using AIToolSelector.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();


List<Tool> tools = new();

if (File.Exists("Data/tools.json"))
{
    var json = File.ReadAllText("Data/tools.json");
    tools = JsonSerializer.Deserialize<List<Tool>>(json);
}

app.MapGet("/api/tools", () => tools); 

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Tool> tools = new();

if (File.Exists("Data/tools.json"))
{
    var json = File.ReadAllText("Data/tools.json");
    tools = JsonSerializer.Deserialize<List<Tool>>(json);
}

// API 1 — list all tools
app.MapGet("/api/tools", () => tools);

// API 2 — recommendation logic  ← PASTE HERE
app.MapGet("/api/recommend", (string task, string price, string level) =>
{
    var results = tools
        .Where(t =>
            t.Task == task &&
            t.Price == price &&
            t.Level == level)
        .ToList();

    return results.Count > 0 ? results : tools.Where(t => t.Task == task);
});

app.Run();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
