using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialWebsite.Data;
using SocialWebsite.Model;

namespace SocialWebsite.Services.ManageState;

public class StateModel : PageModel
{
    protected readonly ApplicationDbContext _db;

    public User User { get; private set; }
    public bool IsAuthenticated { get; private set; }

    public List<string> SuccessMessages = new List<string>();
    public List<string> ErrorMessages = new List<string>();

    public StateModel(ApplicationDbContext db)
    {
        _db = db;
    }

    // Run code after a handler method has been selected, but before model binding occurs.
    public override void OnPageHandlerSelected(PageHandlerSelectedContext context)
    {
        getAccount();
        getMessages();
    }

    private void getMessages()
    {
        if (TempData["success"] != null)
        {
            SuccessMessages.Add(TempData["success"].ToString());
        }

        if (TempData["error"] != null)
        {
            SuccessMessages.Add(TempData["error"].ToString());
        }

    }

    private void getAccount()
    {
        var result = HttpContext.Request.Cookies.TryGetValue("id", out string id);
        
        // not have id in cookie
        if (id == null)
        {
            User = null;
            IsAuthenticated = false;
            return;
        } 

        // get user
        User = _db.Users.SingleOrDefault(user => user.UserID.ToString().Equals(id));

        if (User == null)
        {
            IsAuthenticated = false;
            return;
        }

        IsAuthenticated = true;

    }
    
}

