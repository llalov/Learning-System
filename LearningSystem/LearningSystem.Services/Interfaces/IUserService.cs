namespace LearningSystem.Services.Interfaces
{
    using Services.Models.UserProfile;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<UserProfileServiceModel> GetUserProfileAsync(string userId);
    }
}
