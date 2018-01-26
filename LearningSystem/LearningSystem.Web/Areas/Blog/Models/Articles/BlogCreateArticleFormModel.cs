namespace LearningSystem.Web.Areas.Blog.Models.Articles
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class BlogCreateArticleFormModel
    {
        [Required]
        [MinLength(ArticleTitleMinLength)]
        [MaxLength(ArticleTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
