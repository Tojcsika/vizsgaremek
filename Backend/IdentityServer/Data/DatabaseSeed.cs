using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Data
{
    public static class DatabaseSeed
    {
        public static void Initialize(IdentityContext identityContext, ConfigurationDbContext configurationDbContext, PersistedGrantDbContext persistedGrantDbContext, IApplicationBuilder applicationBuilder)
        {
            identityContext.Database.Migrate();
            configurationDbContext.Database.Migrate();
            SeedConfigurationData(configurationDbContext);
            persistedGrantDbContext.Database.Migrate();
            SeedRolesAndUsers(applicationBuilder);
        }

        private static async void SeedRolesAndUsers(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<IdentityUser>>();
                if (userManager != null)
                {

                    if (await userManager.FindByNameAsync("admin") == null)
                    {
                        var newUser = new IdentityUser()
                        {
                            UserName = "admin",
                            Email = "admin@admin.admin",
                            EmailConfirmed = false,
                            PhoneNumber = "",
                            PhoneNumberConfirmed = false,
                            TwoFactorEnabled = false
                        };

                        var result = await userManager.CreateAsync(newUser, "Pa55w0rd!");
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(newUser, "Administrator");
                        }
                    }

                    if (await userManager.FindByNameAsync("Mick") == null)
                    {
                        var newUser = new IdentityUser()
                        {
                            UserName = "Mick",
                            Email = "mick@mick.com",
                            EmailConfirmed = false,
                            PhoneNumber = "",
                            PhoneNumberConfirmed = false,
                            TwoFactorEnabled = false,
                        };
                        var result = await userManager.CreateAsync(newUser, "MickPassword#0");
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(newUser, "Member");
                        }
                    }
                }
            }

        }

        private static void SeedConfigurationData(ConfigurationDbContext context)
        {
            if (!context.Clients.Any())
            {
                foreach (var client in Config.GetClients())
                {

                    context.Clients.Add(client.ToEntity());
                }

                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Config.GetIdentityResources())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }

                context.SaveChanges();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var resource in Config.GetApiScopes())
                {
                    context.ApiScopes.Add(resource.ToEntity());
                }

                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var resource in Config.GetApiResources())
                {
                    context.ApiResources.Add(resource.ToEntity());
                }

                context.SaveChanges();
            }
        }
    }
}
