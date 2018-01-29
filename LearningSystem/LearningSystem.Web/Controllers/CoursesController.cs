namespace LearningSystem.Web.Controllers
{
    using LearningSystem.Data.Models;
    using LearningSystem.Services.Interfaces;
    using LearningSystem.Web.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Courses;
    using System.Threading.Tasks;

    public class CoursesController : Controller
    {
        private readonly ICourseService Courses;
        private readonly UserManager<User> UserManager;

        public CoursesController(ICourseService courses, UserManager<User> userManager)
        {
            this.Courses = courses;
            this.UserManager = userManager;
        }

        public async Task<IActionResult> Details(int id)
        {
            if (!await this.Courses.Exist(id))
                return NotFound();
            
            var model = new CourseDetailsViewModel
            {
                Course = await this.Courses.DetailsAsync(id)
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = UserManager.GetUserId(User);
                model.IsUserSignedInCouse = await Courses.IsUserSignedInCourse(id, userId);
            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SignUp(int id)
        {
            var userId = this.UserManager.GetUserId(User);

            var success = await this.Courses.SignUpUserAsync(id, userId);

            if (!success)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage("Thank you for your registration!");

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SignOut(int id)
        {
            var userId = this.UserManager.GetUserId(User);

            var success = await this.Courses.SignOutUserAsync(id, userId);

            if (!success)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage("You have been signed out successfully.");

            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
