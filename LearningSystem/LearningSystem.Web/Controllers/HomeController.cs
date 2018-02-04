namespace LearningSystem.Web.Controllers
{
    using LearningSystem.Services.Interfaces;
    using LearningSystem.Web.Models;
    using Microsoft.AspNetCore.Mvc;
    using Models.Home;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly ICourseService Courses;
        private readonly IUserService Users;


        public HomeController(ICourseService courses, IUserService users)
        {
            this.Courses = courses;
            this.Users = users;
        }

        public async Task<IActionResult> Index(HomeIndexViewModel model)
        {
            model.Courses = await this.Courses.AllActiveAsync();

            return View(model);
        }

        public async Task<IActionResult> Search(SearchFormModel model)
        {
            var viewModel = new SearchViewModel
            {
               SearchText = model.SearchText
            };

            if (model.SearchInUsers)
            {
                viewModel.SearchInUsers = true;
                viewModel.Users = await this.Users.FindUsersAsync(model.SearchText);
            }
                
            if (model.SearchInCourses)
            {
                viewModel.SearchInCourses = true;
                viewModel.Courses = await this.Courses.FindCoursesAsync(model.SearchText);
            }

            return View(viewModel);
        }
            
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
