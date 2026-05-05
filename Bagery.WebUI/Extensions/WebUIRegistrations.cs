using Bagery.WebUI.Context;
using Bagery.WebUI.Interceptors;
using Bagery.WebUI.Repositories.BannerRepositories;
using Bagery.WebUI.Repositories.CategoryRepositories;
using Bagery.WebUI.Repositories.ProductRepositories;
using Bagery.WebUI.Services;
using Bagery.WebUI.UOW;
using FluentValidation;
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
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();


            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());



        }



    }
}
