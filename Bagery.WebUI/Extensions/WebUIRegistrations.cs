using Bagery.WebUI.Context;
using Bagery.WebUI.Entities;
using Bagery.WebUI.IdentityValidations;
using Bagery.WebUI.Interceptors;
using Bagery.WebUI.Repositories.BannerRepositories;
using Bagery.WebUI.Repositories.CategoryRepositories;
using Bagery.WebUI.Repositories.ProductRepositories;
using Bagery.WebUI.Repositories.ProductVariantRepositories;
using Bagery.WebUI.Services;
using Bagery.WebUI.Services.EmailServices;
using Bagery.WebUI.UOW;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
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
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IEmailService, EmailService>();

            
            //services.AddScoped<IBannerRepository, BannerRepository>();
            //services.AddScoped<ICategoryRepository, CategoryRepository>();
            //services.AddScoped<IProductRepository, ProductRepository>();
            //services.AddScoped<IProductVariantRepository, ProductVariantRepository>();


            //Scrutor ile Registration

            services.Scan(options => options.FromAssemblies(Assembly.GetExecutingAssembly())
                    .AddClasses(x=>x.Where(t=>t.Name.EndsWith("Repository")))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                   
            );


            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


            //identity settings
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;//en az 1 farklı karakter
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15); // Hesap kilitlendiğinde 15 dakika kapalı kalır.
                options.Lockout.MaxFailedAccessAttempts = 5; // 5 kez yanlış girerse kilitlenir.
                options.Lockout.AllowedForNewUsers = true; // Yeni kullanıcılar için de geçerli olsun.

            }).AddEntityFrameworkStores<AppDbContext>()
            .AddErrorDescriber<CustomErrorDescriber>()
            .AddDefaultTokenProviders();//token üretmek için koyduk...

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User/Login";
                options.LogoutPath = "/User/Logout";
                options.AccessDeniedPath = "/User/AccessDenied";
            });


            services.AddHttpContextAccessor();

        }



    }
}
