namespace LearningSystem.Services.Models
{
    using AutoMapper;
    using Data.Models;
    using LearningSystem.Common.Mapping;
    using System.Linq;

    public class UserListingServiceModel : IMapFrom<User>, IHaveCustomMapping
    {
        public string Name { get; set; }

        public string Username { get; set; }

        public int Courses { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<User, UserListingServiceModel>()
                .ForMember(u => u.Courses, cfg => cfg.MapFrom(u => u.Courses.Count()));
    }
}
