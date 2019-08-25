using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SESMTTech.Gestor.PlanosDeAcao.Infrastructure.EntityFrameworkCore;
using System;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder RunPlanosDeAcaoMigrations(this IApplicationBuilder applicationBuilder)
        {
            if (applicationBuilder == null)
                throw new ArgumentNullException(nameof(applicationBuilder));

            using (var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var logger = serviceScope.ServiceProvider.GetService<ILoggerFactory>().CreateLogger<PlanosDeAcaoContext>();

                var sqlConfiguration = serviceScope.ServiceProvider.GetService<PlanosDeAcaoSqlConfiguration>();
                if (sqlConfiguration.EnableMigrations)
                {
                    logger.LogInformation("Applying migrations for Gestor PlanosDeAcao...");

                    using (var context = serviceScope.ServiceProvider.GetService<PlanosDeAcaoContext>())
                        context.Database.Migrate();

                    logger.LogInformation("Gestor PlanosDeAcao migrations applied.");
                }
                else
                    logger.LogInformation("Gestor PlanosDeAcao migrations disabled.");
            }

            return applicationBuilder;
        }
    }
}