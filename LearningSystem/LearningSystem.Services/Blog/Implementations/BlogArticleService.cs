namespace LearningSystem.Services.Blog.Implementations
{
    using System.Threading.Tasks;
    using Interfaces;
    using LearningSystem.Data;
    using Data.Models;
    using System;

    public class BlogArticleService : IBlogArticleService
    {
        private readonly ApplicationDbContext Db;

        public BlogArticleService(ApplicationDbContext db)
        {
            this.Db = db;
        }

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
    }
}
