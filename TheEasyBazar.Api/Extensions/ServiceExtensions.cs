using TheEasyBazar.Data.IRepositories;
using TheEasyBazar.Data.Repositories;
using TheEasyBazar.Service.Interfaces;
using TheEasyBazar.Service.Services;

namespace TheEasyBazar.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCustomService(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IUserService, UserService>();
        }
    }
}
