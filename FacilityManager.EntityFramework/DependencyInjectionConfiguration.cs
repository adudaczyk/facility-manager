using FacilityManager.EntityFramework.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FacilityManager.EntityFramework
{
    public static class DependencyInjectionConfiguration
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IFacilityRepository, FacilityRepository>();
            services.AddScoped<ISubfacilityRepository, SubfacilityRepository>();
            services.AddScoped<IOccupancyRepository, OccupancyRepository>();
        }
    }
}