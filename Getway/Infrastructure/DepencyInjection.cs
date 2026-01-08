using Getway.Interface;
using Microsoft.EntityFrameworkCore;
namespace Getway.Infrastructure
{
    public static class DepencyInjection
    {

        public static IServiceCollection AddIInfrastructure( this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IUserRepository, UserRepository>();







           return services.AddIInfrastructure(configuration);
        }
    }
}
