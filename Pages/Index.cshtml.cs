using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using AIToolSelector.Models;

namespace AIToolSelector.Pages;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpFactory;

    public IndexModel(IHttpClientFactory httpFactory)
    {
        _httpFactory = httpFactory;
    }

    [BindProperty]
    public string Task { get; set; }

    [BindProperty]
    public string Budget { get; set; }

    [BindProperty]
    public string Level { get; set; }

    public List<Tool> Tools { get; set; } = new();

    public async Task OnPostAsync()
    {
        var client = _httpFactory.CreateClient();

        string url =
            $"https://ai-tool-selector-1.onrender.com/api/recommend?task={Task}&price={Budget}&level={Level}";

        var result = await client.GetFromJsonAsync<List<Tool>>(url);

        if (result != null)
            Tools = result;
    }
}