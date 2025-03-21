﻿namespace LearningSystem.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data.Models;
    using Interfaces;
    using LearningSystem.Data;
    using LearningSystem.Services.Models.Course;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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

        public async Task<TModel> DetailsAsync<TModel>(int id) where TModel : class
            => await this.Db
                .Courses
                .Where(c => c.Id == id)
                .ProjectTo<TModel>()
                .FirstOrDefaultAsync();

        public async Task<bool> Exist(int id)
            => await this.Db
                .Courses
                .Where(c => c.Id == id)
                .AnyAsync();

        public async Task<bool> IsUserSignedInCourse(int courseId, string userId)
            => await this.Db
                .Courses
                .AnyAsync(c => c.Id == courseId 
                       && c.Students.Any(s => s.StudentId == userId));

        public async Task<bool> SignUpUserAsync(int courseId, string userId)
        {
            var course = await this.GetCourseInfo(courseId, userId);

            if (course == null || course.StartDate < DateTime.UtcNow || course.UserIsEnrolled)
                return false;
            
            var studentInCourse = new StudentCourse
            {
                CourseId = courseId,
                StudentId = userId
            };

            this.Db.Add(studentInCourse);
            await this.Db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SignOutUserAsync(int courseId, string userId)
        {
            var course = await this.GetCourseInfo(courseId, userId);
            if (course == null || course.StartDate < DateTime.UtcNow || !course.UserIsEnrolled)
                return false;

            var studentInCourse = await this.Db
                .FindAsync<StudentCourse>(userId, courseId);

            this.Db.Remove(studentInCourse);
            await this.Db.SaveChangesAsync();

            return true;
        }

        private async Task<CourseWithStudentInfo> GetCourseInfo(int courseId, string userId)
            => await this.Db
                    .Courses
                    .Where(c => c.Id == courseId)
                    .Select(c => new CourseWithStudentInfo
                    {
                        StartDate = c.StartDate,
                        UserIsEnrolled = c.Students.Any(s => s.StudentId == userId)
                    })
                    .FirstOrDefaultAsync();

        public async Task<IEnumerable<CourseListingServiceModel>> FindCoursesAsync(string searchText)
        {
            var SearchText = searchText ?? string.Empty;

            return await this.Db
                        .Courses
                        .OrderByDescending(c => c.Id)
                        .Where(c => c.Name.ToLower().Contains(SearchText.ToLower()))
                        .ProjectTo<CourseListingServiceModel>()
                        .ToListAsync();
        }
    }
}
