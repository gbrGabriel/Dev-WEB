using DevWEB.Business.Intefaces;
using DevWEB.Data.Context;
using DevWEB.Data.Repository;

namespace DevWEB.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ContextDB>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            return services;
        }
    }
}
