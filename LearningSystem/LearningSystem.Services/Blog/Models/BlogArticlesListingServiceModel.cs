namespace LearningSystem.Services.Blog.Models
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using System;

    public class BlogArticlesListingServiceModel : IMapFrom<Article>, IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Title { get; set; }
        
        public DateTime PublishDate { get; set; }

        public string Author { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Article, BlogArticlesListingServiceModel>()
                .ForMember(a => a.Author, cfg => cfg.MapFrom(a => a.Author.UserName));
    }
}
