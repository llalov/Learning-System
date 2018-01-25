namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using Data.Models;
    using LearningSystem.Web.Controllers;
    using LearningSystem.Web.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Courses;
    using Services.Admin.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using static WebConstants;

    public class CoursesController : BaseAdminController
    {
        private readonly UserManager<User> UserManager;
        private readonly IAdminCourseService Courses;

        public CoursesController(UserManager<User> userManager, IAdminCourseService courses)
        {
            this.UserManager = userManager;
            this.Courses = courses;
        }

        public async Task<IActionResult> Create()
        {
            var trainers = await UserManager.GetUsersInRoleAsync(TrainerRole);
            var trainersForList = trainers.Select(t => new SelectListItem
            {
                Text = t.UserName,
                Value = t.Id
            })
            .ToList();

            return View(new AdminCreateCourseFormModel
            {
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow,
                Trainers = trainersForList
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminCreateCourseFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Trainers = await this.GetTrainers();
                return View(model);
            }

            await this.Courses.Create(
                 model.Name,
                 model.Description,
                 model.StartDate,
                 model.EndDate,
                 model.TrainerId
                 );

            TempData.AddSuccessMessage($"Course {model.Name} created successfully!");

            return RedirectToAction(nameof(HomeController.Index), "Home", new { area = string.Empty });
        }

        private async Task<IEnumerable<SelectListItem>> GetTrainers()
        {
            var trainers = await this.UserManager.GetUsersInRoleAsync(WebConstants.TrainerRole);
            var trainerListItems = trainers
                .Select(t => new SelectListItem
                {
                    Text = t.UserName,
                    Value = t.Id
                })
                .ToList();

            return trainerListItems;
        }
    }
}
