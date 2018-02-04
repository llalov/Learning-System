namespace LearningSystem.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Interfaces;
    using LearningSystem.Data;
    using LearningSystem.Services.Models;
    using LearningSystem.Services.Models.UserProfile;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext Db;

        public UserService(ApplicationDbContext db)
        {
            this.Db = db;
        }

        public async Task<IEnumerable<UserListingServiceModel>> FindUsersAsync(string searchText)
        {
            var SearchText = searchText ?? string.Empty;

            return await this.Db
                    .Users
                    .Where(u => u.Name.ToLower().Contains(SearchText.ToLower()))
                    .ProjectTo<UserListingServiceModel>()
                    .ToListAsync();
        }
                
        public async Task<UserProfileServiceModel> GetUserProfileAsync(string userId)
            => await this.Db
                .Users
                .Where(u => u.Id == userId)
                .ProjectTo<UserProfileServiceModel>(new { studentId = userId })
                .FirstOrDefaultAsync();
    }
}
