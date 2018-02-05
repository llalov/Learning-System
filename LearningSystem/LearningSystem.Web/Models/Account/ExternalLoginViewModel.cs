namespace LearningSystem.Web.Models.AccountViewModels
{
    using System.ComponentModel.DataAnnotations;
    using System;

    public class ExternalLoginViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
    }
}
