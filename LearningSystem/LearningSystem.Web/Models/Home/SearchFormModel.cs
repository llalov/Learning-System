using System.ComponentModel.DataAnnotations;

namespace LearningSystem.Web.Models.Home
{
    public class SearchFormModel
    {
        [Display(Name = "Search in courses")]
        public bool SearchInCourses { get; set; } = true;

        [Display(Name = "Search in users")]
        public bool SearchInUsers { get; set; } = true;

        public string SearchText { get; set; }
    }
}
