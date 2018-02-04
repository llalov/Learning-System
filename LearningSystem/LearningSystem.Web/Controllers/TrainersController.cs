namespace LearningSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using LearningSystem.Services.Interfaces;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Models.Trainers;
    using Services.Models.Course;
    using LearningSystem.Web.Infrastructure.Extensions;

    [Authorize(Roles = WebConstants.TrainerRole)]
    public class TrainersController : Controller
    {
        private readonly ITrainerService Trainers;
        private readonly UserManager<User> UserManager;
        private readonly ICourseService CoursesService;

        public TrainersController(ITrainerService trainers, UserManager<User> userManager, ICourseService coursesService)
        {
            this.Trainers = trainers;
            this.UserManager = userManager;
            this.CoursesService = coursesService;
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

            return View(new StudentsInClassViewModel
            {
                Students = await this.Trainers.StudentsInCourse(courseId),
                Course = await CoursesService.DetailsAsync<CourseListingServiceModel>(courseId)
        });
        }

        [HttpPost]
        public async Task<IActionResult> GradeStudent(int courseId, string studentId, Grade grade)
        {
            var userId = UserManager.GetUserId(User);
            if (!await this.Trainers.IsTrainer(courseId, userId))
                return BadRequest();

            var success = await this.Trainers.AddStudentGrade(studentId, courseId, grade);

            if (!success)
            {
                TempData.AddErrorMessage("There was a problem adding the grade.");
                return RedirectToAction(nameof(Students), new { courseId });
            }

            TempData.AddSuccessMessage("Grade added.");
            return RedirectToAction(nameof(Students), new { courseId });
        }
    }
}
