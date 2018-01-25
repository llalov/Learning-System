namespace LearningSystem.Services.Admin.Interfaces
{
    using Models;
    using System.Collections.Generic;

    public interface IAdminUserService
    {
        IEnumerable<AdminUserListingServiceModel> All();
    }
}
