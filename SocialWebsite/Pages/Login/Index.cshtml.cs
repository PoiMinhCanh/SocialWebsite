using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialWebsite.Data;
using SocialWebsite.Model;
using SocialWebsite.Model.DTO;
using SocialWebsite.Services.ManageState;

namespace SocialWebsite.Pages.Login;

public class IndexModel : StateModel
{
    private readonly IMapper _mapper;

    [BindProperty]
    public LoginDTO LoginDTO { get; set; }

    public IndexModel(ApplicationDbContext db, IMapper mapper) : base(db)
    {
        _mapper = mapper;
    }

    public IActionResult OnGet()
    {
        if (IsAuthenticated)
        {
            return Redirect("/");
        }
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();

        User user = _db.Users.SingleOrDefault(user => user.Email.Equals(LoginDTO.Email)
                                                                && user.Password.Equals(LoginDTO.Password));

        if (user == null)
        {
            ErrorMessages.Add("Email or password not correct. Please try again!");
            return Page();
        }

        // set value on cookies
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = LoginDTO.RememberMe ? DateTime.Now.AddDays(30) : null
        };

        HttpContext.Response.Cookies.Append("id", user.UserID.ToString(), cookieOptions);

        return Redirect("Index");
    }
}
