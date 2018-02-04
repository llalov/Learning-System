namespace LearningSystem.Web.Models.Home
{
    using LearningSystem.Services.Models.Course;
    using System.Collections.Generic;
    using Services.Models;

    public class SearchViewModel
    {
        public IEnumerable<CourseListingServiceModel> Courses { get; set; }
            = new List<CourseListingServiceModel>();

        public IEnumerable<UserListingServiceModel> Users { get; set; }
           = new List<UserListingServiceModel>();

        public string SearchText { get; set; }

        public bool SearchInUsers { get; set; } = false;

        public bool SearchInCourses { get; set; } = false;
    }
}
