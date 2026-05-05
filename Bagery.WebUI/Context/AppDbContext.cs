using Bagery.WebUI.Entities;
using Bagery.WebUI.Entities.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bagery.WebUI.Context
{
    public class AppDbContext(DbContextOptions options):IdentityDbContext<AppUser,AppRole,Guid>(options)
    {


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            foreach (var entitiyType in modelBuilder.Model.GetEntityTypes())
            {

                if (typeof(BaseEntity).IsAssignableFrom(entitiyType.ClrType))
                {
                    //wrapper yapıyoruz, yani sorgulara otomatik olarak silinmiş olanları getirme filtresi ekliyoruz
                    //select * from table where IsDeleted = false olanlar gelsin sadece diyeceğiz
                    modelBuilder.Entity(entitiyType.ClrType)
                                .HasQueryFilter(ConvertToDeleteFilter(entitiyType.ClrType));

                }

            }
        }


        private static LambdaExpression ConvertToDeleteFilter(Type type)
        {

            var parameter = Expression.Parameter(type, "e");

            var property = Expression.Property(Expression.Convert(parameter, typeof(BaseEntity)), "IsDeleted");

            var notDeleted = Expression.Not(property);

            return Expression.Lambda(notDeleted, parameter);


            //ısdeleted false olanalr gelcek
        }




        public DbSet<Banner> Banners { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactSocialMedia> ContactSocialMedias { get; set; }
        public DbSet<OurHistory> OurHistories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Video> Videos { get; set; }


    }
}
