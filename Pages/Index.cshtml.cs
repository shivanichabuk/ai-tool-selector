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

    var baseUrl = $"{Request.Scheme}://{Request.Host}";
    var url = $"{baseUrl}/api/recommend?task={Task}&price={Budget}&level={Level}";

    try
    {
        var result = await client.GetFromJsonAsync<List<Tool>>(url);

        if (result != null)
            Tools = result;
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        Tools = new List<Tool>();
    }
}

}