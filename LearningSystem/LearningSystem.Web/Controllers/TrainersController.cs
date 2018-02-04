namespace LearningSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using LearningSystem.Services.Interfaces;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;

    [Authorize(Roles = WebConstants.TrainerRole)]
    public class TrainersController : Controller
    {
        private readonly ITrainerService Trainers;
        private readonly UserManager<User> UserManager;

        public TrainersController(ITrainerService trainers, UserManager<User> userManager)
        {
            this.Trainers = trainers;
            this.UserManager = userManager;
        }

       public async Task<IActionResult> Courses()
        {
            var userId = UserManager.GetUserId(User);
            var courses = await Trainers.GetCoursesAsync(userId);

            return View(courses);
        }

        public async Task<IActionResult> Students(int courseId)
        {
            var userId = UserManager.GetUserId(User);

            if (!await this.Trainers.IsTrainer(courseId, userId))
                return BadRequest();

            var students = await this.Trainers.StudentsInCourse(courseId);

            return View(students);
        }
    
    }
}
