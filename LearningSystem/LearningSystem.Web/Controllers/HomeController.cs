namespace LearningSystem.Web.Controllers
{
    using LearningSystem.Services.Interfaces;
    using LearningSystem.Web.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly ICourseService Courses;

        public HomeController(ICourseService courses)
        {
            this.Courses = courses;
        }

        public async Task<IActionResult> Index()
            => View(await this.Courses.AllActiveAsync());
        
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
