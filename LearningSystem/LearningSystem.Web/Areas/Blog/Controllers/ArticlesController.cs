namespace LearningSystem.Web.Areas.Blog.Controllers
{
    using LearningSystem.Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Linq;
    using System.Threading.Tasks;
    using Models.Articles;
    using static WebConstants;

    [Authorize(Roles = BlogAuthorRole)]
    [Area("Blog")]
    public class ArticlesController : Controller
    {
        private readonly UserManager<User> UserManager;

        public ArticlesController(UserManager<User> userManager)
        {
            this.UserManager = userManager;
        }

        public IActionResult Create()
            => View();
        
        
        [HttpPost]
        public async Task<IActionResult> Create(BlogCreateArticleFormModel model)
        {

            throw new System.Exception("awdadawdaw");
        }
    }
}
