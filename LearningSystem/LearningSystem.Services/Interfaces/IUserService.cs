namespace LearningSystem.Services.Interfaces
{
    using Services.Models.UserProfile;
    using System.Threading.Tasks;
    using Services.Models;
    using System.Collections.Generic;

    public interface IUserService
    {
        Task<UserProfileServiceModel> GetUserProfileAsync(string userId);

        Task<IEnumerable<UserListingServiceModel>> FindUsersAsync(string searchText);
    }
}
