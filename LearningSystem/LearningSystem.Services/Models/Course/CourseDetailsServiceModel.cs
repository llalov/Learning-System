
namespace LearningSystem.Services.Models.Course
{
    using LearningSystem.Common.Mapping;
    using System;
    using Data.Models;
    using AutoMapper;

    public class CourseDetailsServiceModel : IMapFrom<Course>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Trainer{ get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int StudentsCount { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Course, CourseDetailsServiceModel>()
                .ForMember(c => c.Trainer, cfg => cfg.MapFrom(c => c.Trainer.UserName))
                .ForMember(c => c.StudentsCount, cfg => cfg.MapFrom(c => c.Students.Count));
    }
}
