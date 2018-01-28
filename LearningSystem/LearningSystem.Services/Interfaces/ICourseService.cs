namespace LearningSystem.Services.Interfaces
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICourseService
    {
        Task<IEnumerable<CourseListingServiceModel>> AllActiveAsync();

        Task<bool> Exist(int id);
    }
}
