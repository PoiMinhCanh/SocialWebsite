using SocialWebsite.Data;
using SocialWebsite.Services.ManageState;

namespace SocialWebsite.Pages
{
    public class IndexModel : StateModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ApplicationDbContext db, ILogger<IndexModel> logger) : base(db)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}