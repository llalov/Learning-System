namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using LearningSystem.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Users;
    using Services.Admin.Interfaces;
    using System.Linq;
    using System.Threading.Tasks;
    using Infrastructure.Extensions;

    public class UsersController : BaseAdminController
    {
        private readonly IAdminUserService Users;
        private readonly RoleManager<IdentityRole> RoleManager;
        private readonly UserManager<User> UserManager;

        public UsersController(IAdminUserService users, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.Users = users;
            this.RoleManager = roleManager;
            this.UserManager = userManager;
        }

        public IActionResult Index()
        {
            var users = this.Users.All(); 
            var roles = this.RoleManager
                .Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToList();

            return View(new AdminUsersViewModel
            {
                Users = users,
                Roles = roles
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(AddUserToRoleFormModel model)
        {
            var roleExists = await this.RoleManager.RoleExistsAsync(model.Role);
            var user = await this.UserManager.FindByIdAsync(model.UserId);
            var userExists = user != null;

            if (!roleExists || !userExists)
                ModelState.AddModelError(string.Empty, "Invalid identity details.");
            
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Index));

            await this.UserManager.AddToRoleAsync(user, model.Role);

            TempData.AddSuccessMessage($"User {user.UserName} added successfully to {model.Role}s.");

            return RedirectToAction(nameof(Index));
        }
            
    }
}
