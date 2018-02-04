﻿namespace LearningSystem.Services.Models
{
    using AutoMapper;
    using Data.Models;
    using LearningSystem.Common.Mapping;
    using System.Linq;

    public class StudentInCourseServiceModel : IMapFrom<User>, IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public Grade? Grade { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            int courseId = default(int);
            mapper
                .CreateMap<User, StudentInCourseServiceModel>()
                .ForMember(s => s.Grade, cfg => cfg.MapFrom(s => s
                    .Courses
                    .Where(c => c.CourseId == courseId)
                    .Select(c => c.Grade)
                    .FirstOrDefault()));
        }
    }
}