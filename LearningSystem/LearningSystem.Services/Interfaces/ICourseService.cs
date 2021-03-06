﻿namespace LearningSystem.Services.Interfaces
{
    using Models.Course;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICourseService
    {
        Task<IEnumerable<CourseListingServiceModel>> AllActiveAsync();

        Task<IEnumerable<CourseListingServiceModel>> FindCoursesAsync(string searchText);

        Task<TModel> DetailsAsync<TModel>(int id) where TModel : class;

        Task<bool> Exist(int id);

        Task<bool> SignUpUserAsync(int courseId, string userId);

        Task<bool> SignOutUserAsync(int courseId, string userId);

        Task<bool> IsUserSignedInCourse(int courseId, string userId);
    }
}
