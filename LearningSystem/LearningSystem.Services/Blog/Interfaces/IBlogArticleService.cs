namespace LearningSystem.Services.Blog.Interfaces
{
    using System.Threading.Tasks;
    public interface IBlogArticleService
    {
        Task CreateAsync(string title, string content, string authorId);
    }
}
