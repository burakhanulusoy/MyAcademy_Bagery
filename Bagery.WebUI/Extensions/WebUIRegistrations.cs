using Bagery.WebUI.Context;
using Bagery.WebUI.Interceptors;
using Bagery.WebUI.Repositories.BannerRepositories;
using Bagery.WebUI.UOW;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Bagery.WebUI.Extensions
{
    public static class WebUIRegistrations
    {

        public static void AddWebUiRegistration(this IServiceCollection services,IConfiguration configuration)
        {


            services.AddDbContext<AppDbContext>(options =>
            {

                options.UseNpgsql(configuration.GetConnectionString("PostgreSQL")); //secret.json 'da.
                options.AddInterceptors(new AuditDbContextInterceptor());

            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBannerRepository, BannerRepository>();


            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            });



        }



    }
}
