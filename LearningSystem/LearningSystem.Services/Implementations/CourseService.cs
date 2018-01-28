namespace LearningSystem.Services.Implementations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces;
    using LearningSystem.Data;
    using LearningSystem.Services.Models;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext Db;

        public CourseService(ApplicationDbContext db)
        {
            this.Db = db;
        }

        public async Task<IEnumerable<CourseListingServiceModel>> AllActiveAsync()
            => await this.Db
                .Courses
                .OrderByDescending(c => c.StartDate)
                .Where(c => c.StartDate >= DateTime.UtcNow)
                .ProjectTo<CourseListingServiceModel>()
                .ToListAsync();

        public async Task<bool> Exist(int id)
            => await this.Db
                .Courses
                .Where(c => c.Id == id)
                .AnyAsync();
    }
}
