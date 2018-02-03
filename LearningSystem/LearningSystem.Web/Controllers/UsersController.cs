namespace LearningSystem.Web.Controllers
{
    using LearningSystem.Data.Models;
    using LearningSystem.Services.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class UsersController : Controller
    {
        private readonly IUserService UserService;
        private readonly UserManager<User> UserManager;

        public UsersController(IUserService userService, UserManager<User> userManager)
        {
            this.UserService = userService;
            this.UserManager = userManager;
        }

        public async Task<IActionResult> Profile(string username)
        {
            var user = await UserManager.FindByNameAsync(username);

            if (user == null)
            {
                return NotFound();
            }

            var profile = await UserService.GetUserProfileAsync(user.Id);

            return View(profile);
        }
    }
}
