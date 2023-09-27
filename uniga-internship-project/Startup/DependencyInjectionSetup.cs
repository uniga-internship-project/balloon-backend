using Microsoft.Extensions.DependencyInjection;
using uniga_internship_project.Services.AuthorizeSerivice;
using uniga_internship_project.Services.UserService;

namespace uniga_internship_project.Startup
{
    public static class DependencyInjectionSetup
    {
        public static IServiceCollection RegisterService(this IServiceCollection services)
        {
            // Add service here
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthorizeService, AuthorizeService>();
            return services;
        }
    }
}
