﻿using FacilityManager.BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FacilityManager.BusinessLogic
{
    public static class DependencyInjectionConfiguration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFacilityService, FacilityService>();
        }
    }
}