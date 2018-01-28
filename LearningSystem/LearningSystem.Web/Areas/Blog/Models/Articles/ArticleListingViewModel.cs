namespace LearningSystem.Web.Areas.Blog.Models.Articles
{
    using LearningSystem.Services.Blog.Models;
    using System;
    using System.Collections.Generic;

    public class ArticleListingViewModel
    {
        public IEnumerable<BlogArticlesListingServiceModel> Articles { get; set; }

        public int TotalArticles { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalArticles / 10);

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage <= 1 ? 1 : this.CurrentPage - 1;

        public int NextPage 
            => this.CurrentPage == this.TotalPages 
            ? this.TotalPages 
            : this.CurrentPage + 1;
    }
}
