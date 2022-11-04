using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialWebsite.Data;
using SocialWebsite.Model;
using SocialWebsite.Model.DTO;
using SocialWebsite.Services.ManageState;

namespace SocialWebsite.Pages.Register;

public class IndexModel : StateModel
{
    private readonly IMapper _mapper;

    [BindProperty]
    public CreateUserDTO CreateUserDTO { get; set; }

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

        User existUser = _db.Users.SingleOrDefault(user => user.Email.Equals(CreateUserDTO.Email));
        if (existUser != null)
        {
            ErrorMessages.Add("Email is not available. Please use another email!");
            return Page();
        }

        var user = _mapper.Map<CreateUserDTO, User>(CreateUserDTO);

        _db.Users.Add(user);
        _db.SaveChanges();

        TempData["success"] = $"You have successfully registered an account with email={user.Email}";

        return Redirect("/Login");
    }
}
