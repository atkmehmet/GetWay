using Microsoft.EntityFrameworkCore.Sq
namespace Getway.Infrastructure
{
    public static class DepencyInjection
    {

        public static IServiceCollection AddIInfrastructure( this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AppDb>(
                configuration=> 
                configuration.Use()
                );


            return services;
        }
    }
}
