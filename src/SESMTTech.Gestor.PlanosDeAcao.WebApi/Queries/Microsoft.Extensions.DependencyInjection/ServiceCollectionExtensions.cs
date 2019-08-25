using MediatR;
using SESMTTech.Gestor.PlanosDeAcao.WebApi.Queries;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPlanosDeAcaoQueries(this IServiceCollection services, PlanosDeAcaoSqlConfiguration coreBusinessSqlConfiguration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddSingleton(coreBusinessSqlConfiguration ?? throw new ArgumentNullException(nameof(coreBusinessSqlConfiguration)));
            services.AddTransient<PlanosDeAcaoQueryContext>();

            services.AddMediatR(typeof(PlanosDeAcaoQueryContext).Assembly);

            return services;
        }
    }
}