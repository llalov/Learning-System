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

        [Authorize]
        public async Task<IActionResult> Index(int page = 1)
            => View(new ArticleListingViewModel
                {
                    Articles = await this.Articles.AllAsync(page),
                    TotalArticles = await this.Articles.TotalCountAsync(),
                    CurrentPage = page 
                });

        [Authorize]
        public async Task<IActionResult> Details (int id)
        {
            if (!await this.Articles.Exists(id))
                return NotFound();
            
            return  View(await this.Articles.Details(id));
        }
                
        [Authorize(Roles = BlogAuthorRole)]
        public IActionResult Create()
            => View();
        
        [HttpPost]
        [Authorize(Roles = BlogAuthorRole)]
        public async Task<IActionResult> Create(BlogCreateArticleFormModel model)
        {
            var userId = this.UserManager.GetUserId(User);

            await this.Articles.CreateAsync(model.Title, model.Content, userId);

            return RedirectToAction(nameof(Index));
        }
    }
}
