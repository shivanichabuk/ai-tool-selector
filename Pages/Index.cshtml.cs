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
        if (Task == "writing" && Budget == "free")
            Result = "ChatGPT Free";

        else if (Task == "coding")
            Result = "GitHub Copilot";

        else
            Result = "Canva AI";
    }
}