namespace LearningSystem.Web.Infrastructure.Extensions
{
    using LearningSystem.Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScore = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope() )
            {
                serviceScore.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();
            }
            return app;
        }
    }
}
