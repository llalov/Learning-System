namespace LearningSystem.Services.Blog.Interfaces
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Models;
    

    public interface IBlogArticleService
    {
        Task<IEnumerable<BlogArticlesListingServiceModel>> AllAsync(int page = 1);

        Task<ArticleDetailsViewModel> Details(int id);

        Task<bool> Exists(int id);

        Task CreateAsync(string title, string content, string authorId);

        Task<int> TotalCountAsync();
    }
}
