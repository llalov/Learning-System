namespace LearningSystem.Web.Models.Trainers
{
    using LearningSystem.Services.Models;
    using System.Collections.Generic;
    using Services.Models.Course;

    public class StudentsInClassViewModel
    {
        public IEnumerable<StudentInCourseServiceModel> Students { get; set; }

        public CourseListingServiceModel Course { get; set; }
    }
}
