﻿using FacilityManager.EntityFramework.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FacilityManager.EntityFramework
{
    public static class DependencyInjectionConfiguration
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFacilityRepository, FacilityRepository>();
        }
    }
}