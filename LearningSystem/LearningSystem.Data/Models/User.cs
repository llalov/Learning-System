﻿namespace LearningSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class User : IdentityUser
    {
        [Required]
        [MaxLength(UserNameMaxLength)]
        [MinLength(UserNameMinLength)]
        public string Name { get; set; } = "User";

        public DateTime BirthDate { get; set; }

        public List<StudentCourse> Courses { get; set; } = new List<StudentCourse>();

        public List<Article> Articles { get; set; } = new List<Article>();

        public List<Course> Trainings { get; set; } = new List<Course>();
    }
}
