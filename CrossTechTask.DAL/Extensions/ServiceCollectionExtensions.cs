using CrossTechTask.DAL.Service.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrossTechTask.DAL.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CrossTechDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CrossTechDb"))
            );

            services.AddScoped<ICrossTechDbContext, CrossTechDbContext>();

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPositionRepository, PositionRepository>();
        }
    }
}
