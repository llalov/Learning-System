namespace LearningSystem.Services.Admin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using LearningSystem.Data;
    using Services.Admin.Interfaces;
    using Services.Admin.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class AdminUserService : IAdminUserService
    {
        private readonly ApplicationDbContext Db;

        public AdminUserService(ApplicationDbContext db)
        {
            this.Db = db;
        }

        public IEnumerable<AdminUserListingServiceModel> All()
            => this.Db
                .Users
                .ProjectTo<AdminUserListingServiceModel>()
                .ToList();
    }
}
