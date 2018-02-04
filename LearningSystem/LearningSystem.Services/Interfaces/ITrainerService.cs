namespace LearningSystem.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Course;
    using Models;
    public interface ITrainerService
    {
        Task<IEnumerable<CourseListingServiceModel>> GetCoursesAsync(string userId);

        Task<IEnumerable<StudentInCourseServiceModel>> StudentsInCourse(int courseId);

        Task<bool> IsTrainer(int courseId, string userId);
    }
}
