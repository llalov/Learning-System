namespace LearningSystem.Services.Implementations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using LearningSystem.Services.Models.Course;
    using Services.Interfaces;
    using Data;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using LearningSystem.Services.Models;

    public class TrainerService : ITrainerService
    {
        private readonly ApplicationDbContext Db;

        public TrainerService(ApplicationDbContext db)
        {
            this.Db = db;
        }

        public async Task<IEnumerable<CourseListingServiceModel>> GetCoursesAsync(string userId)
            => await this.Db
                .Courses
                .Where(c => c.TrainerId == userId)
                .ProjectTo<CourseListingServiceModel>()
                .ToListAsync();

        public async Task<bool> IsTrainer(int courseId, string userId)
            => await this.Db
                .Courses
                .AnyAsync(c => c.Id == courseId && c.TrainerId == userId);

        public async Task<IEnumerable<StudentInCourseServiceModel>> StudentsInCourse(int courseId)
            => await this.Db
                .Courses
                .Where(c => c.Id == courseId)
                .SelectMany(c => c.Students.Select(s => s.Student))
                .ProjectTo<StudentInCourseServiceModel>(new { courseId })
                .ToListAsync();
    }
}
