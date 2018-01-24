namespace LearningSystem.Data
{
    using LearningSystem.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            builder
                .Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.Courses)
                .HasForeignKey(s => s.StudentId);

            builder
                .Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.Students)
                .HasForeignKey(c => c.CourseId);

            builder
                .Entity<Article>()
                .HasOne(ar => ar.Author)
                .WithMany(au => au.Articles)
                .HasForeignKey(au => au.AuthorId);

            builder
                .Entity<Course>()
                .HasOne(c => c.Trainer)
                .WithMany(t => t.Trainings)
                .HasForeignKey(t => t.TrainerId);

            base.OnModelCreating(builder);
        }
    }
}
