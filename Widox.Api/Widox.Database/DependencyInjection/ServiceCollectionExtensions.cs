using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Widox.Database.Context;
using Widox.Models.DatabaseObjects;

namespace Widox.Database.DependencyInjection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWidoxDatabase(this IServiceCollection services)
        {
            services.AddDbContext<WidoxDbContext>(
                    options => options.UseNpgsql("Host=db; Database=widox_db; Username=widox_admin; Password=Secret01!; ")
                );
            services.AddIdentity<WidoxUser, WidoxRole>()
                .AddEntityFrameworkStores<WidoxDbContext>()
                .AddDefaultTokenProviders();


            return services;
        }

        public static IApplicationBuilder UseWidoxDatabase(this IApplicationBuilder app)
        {
            WidoxDbSeed.Initialize(app.ApplicationServices);
            return app;
        }


        public static class WidoxDbSeed
        {
            public static void Initialize(IServiceProvider serviceProvider)
            {
                using (var serviceScope = serviceProvider.CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<WidoxDbContext>();

                    // auto migration
                    context?.Database.Migrate();

                    // Seed the database.
                    if (context != null)
                        InitializeUserAndRoles(context);
                }
            }

            private static void InitializeUserAndRoles(WidoxDbContext context)
            {
                // init user and roles  
            }
        }
    }
}
