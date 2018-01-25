namespace LearningSystem.Services.Admin.Implementations
{
    using System;
    using Interfaces;
    using Data;
    using Data.Models;
    using System.Threading.Tasks;

    public class AdminCourseService : IAdminCourseService
    {
        private readonly ApplicationDbContext Db;

        public AdminCourseService(ApplicationDbContext db)
        {
            this.Db = db;
        }
        public async Task Create(string name, string description, DateTime startDate, DateTime endDate, string trainerId)
        {
            var course = new Course
            {
                Name = name,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                TrainerId = trainerId
            };
            this.Db.Add(course);
            await this.Db.SaveChangesAsync();
        }
    }
}
