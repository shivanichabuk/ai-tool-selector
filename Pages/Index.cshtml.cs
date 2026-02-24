using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AIToolSelector.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public string Task { get; set; }

    [BindProperty]
    public string Budget { get; set; }

    [BindProperty]
    public string Level { get; set; }

    public string Result { get; set; }

   public void OnPost()
{
    var tools = new List<(string Name, string Task, string Budget, string Level)>
    {
        ("ChatGPT", "writing", "free", "beginner"),
        ("GitHub Copilot", "coding", "paid", "advanced"),
        ("Canva AI", "design", "free", "beginner"),
        ("Midjourney", "design", "paid", "advanced"),
        ("Claude", "writing", "paid", "advanced")
    };

    int bestScore = -1;
    string bestTool = "No match found";

    foreach (var tool in tools)
    {
        int score = 0;

        if (tool.Task == Task) score += 2;
        if (tool.Budget == Budget) score += 1;
        if (tool.Level == Level) score += 1;

        if (score > bestScore)
        {
            bestScore = score;
            bestTool = tool.Name;
        }
    }

    Result = bestTool;
}
}