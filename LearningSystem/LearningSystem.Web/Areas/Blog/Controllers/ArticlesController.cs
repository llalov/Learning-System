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
    using LearningSystem.Services.Blog.Interfaces;

    [Authorize(Roles = BlogAuthorRole)]
    [Area("Blog")]
    public class ArticlesController : Controller
    {
        private readonly UserManager<User> UserManager;
        private readonly IBlogArticleService Articles;

        public ArticlesController(UserManager<User> userManager, IBlogArticleService articles)
        {
            this.UserManager = userManager;
            this.Articles = articles;
        }

        [AllowAnonymous]
        public IActionResult Index()
            => View();

        public IActionResult Create()
            => View();
        
        
        [HttpPost]
        public async Task<IActionResult> Create(BlogCreateArticleFormModel model)
        {
            var userId = this.UserManager.GetUserId(User);

            await this.Articles.CreateAsync(model.Title, model.Content, userId);

            return RedirectToAction(nameof(Index));
        }
    }
}
