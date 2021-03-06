﻿namespace LearningSystem.Services.Models.Course
{
    using LearningSystem.Common.Mapping;
    using System;
    using Data.Models;

    public class CourseListingServiceModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
