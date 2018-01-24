namespace LearningSystem.Web.Infrastructure.Extensions
{
    using static WebConstants;
    using LearningSystem.Data;
    using LearningSystem.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading.Tasks;
    using System;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope() )
            {
                serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                Task
                    .Run(async () =>
                        {
                            var roles = new[]
                            {
                                AdministratorRole,
                                BlogAuthorRole,
                                TrainerRole
                            };

                            foreach(var role in roles)
                            {
                                bool roleExists = await roleManager.RoleExistsAsync(role);
                                if (!roleExists)
                                {
                                    await roleManager.CreateAsync(new IdentityRole
                                    {
                                        Name = role
                                    });
                                }
                            }

                            var adminUser = await userManager.FindByEmailAsync(AdministratorEmail);
                            if (adminUser == null)
                            {
                                adminUser = new User
                                {
                                    Email = AdministratorEmail,
                                    UserName = AdministratorRole,
                                    Name = AdministratorRole,
                                    BirthDate = DateTime.UtcNow
                                };

                                await userManager.CreateAsync(adminUser);
                                await userManager.AddToRoleAsync(adminUser, AdministratorRole);
                            }
                        }
                    )
                    .Wait();
            }
            return app;
        }
    }
}
