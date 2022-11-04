using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;

namespace SocialWebsite.Pages.ErrorPages;

//[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//[IgnoreAntiforgeryToken]
[BindProperties]
public class IndexModel : PageModel
{
    public int _StatusCode { get; set; }
    public string Message { get; set; }

    public IActionResult OnGet(int statusCode)
    {
        var validStatusCodes = Enum.GetValues(typeof(HttpStatusCode))
                            .Cast<HttpStatusCode>()
                            .Select(HttpStatusCode => (int)HttpStatusCode)
                            .ToList().Contains(statusCode);
        if (!validStatusCodes) statusCode = 404;

        _StatusCode = statusCode;
        Message = ReasonPhrases.GetReasonPhrase(_StatusCode);
        return Page();
    }
}
