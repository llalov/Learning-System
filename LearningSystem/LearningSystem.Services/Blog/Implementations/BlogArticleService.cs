namespace LearningSystem.Services.Blog.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data.Models;
    using Interfaces;
    using LearningSystem.Data;
    using LearningSystem.Services.Blog.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class BlogArticleService : IBlogArticleService
    {
        private readonly ApplicationDbContext Db;

        public BlogArticleService(ApplicationDbContext db)
        {
            this.Db = db;
        }

        public async Task<IEnumerable<BlogArticlesListingServiceModel>> AllAsync(int page = 1)
            => await this.Db
                .Articles
                .OrderByDescending(a => a.PublishDate)
                .Skip((page - 1) * 10)
                .Take(10)
                .ProjectTo<BlogArticlesListingServiceModel>()
                .ToListAsync();

        public async Task CreateAsync(string title, string content, string authorId)
        {
            var article = new Article
            {
                Title = title,
                Content = content,
                PublishDate = DateTime.UtcNow,
                AuthorId = authorId
            };

            this.Db.Add(article);
            await this.Db.SaveChangesAsync();
        }

        public async Task<ArticleDetailsViewModel> Details(int id)
            => await this.Db
                .Articles
                .Where(a => a.Id == id)
                .ProjectTo<ArticleDetailsViewModel>()
                .FirstOrDefaultAsync();

        public async Task<bool> Exists(int id)
            => await this.Db
                .Articles
                .Where(a => a.Id == id)
                .AnyAsync();

        public async Task<int> TotalCountAsync()
            => await this.Db.Articles.CountAsync(); 
    }
}
