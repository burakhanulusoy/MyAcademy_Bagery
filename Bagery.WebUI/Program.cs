using Bagery.WebUI.Extensions;
using Bagery.WebUI.Filters;
using Bagery.WebUI.Hubs;
using Bagery.WebUI.Services;
using Bagery.WebUI.Services.BageryAi;

var builder = WebApplication.CreateBuilder(args);

// YENÝ: QuestPDF her PDF üretiminden önce lisans türünü bilmek ister.
// Community: 1 milyon $ altý ciro, bireysel ve öðrenci projeleri için ücretsiz.
QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
// Add services to the container.

builder.Services.AddWebUiRegistration(builder.Configuration);
builder.Services.AddAmozonS3Registrations(builder.Configuration);


builder.Services.AddScoped<CommentModerationService>();

builder.Services.AddScoped<BageryAiService>();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ExceptionFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// DEÐÝÞTÝ: özel hata sayfasý sadece normal sayfalar için; /hubs altýndaki SignalR cevaplarý olduðu gibi kalsýn
app.UseWhen(
    context => !context.Request.Path.StartsWithSegments("/hubs"),
    branch => branch.UseStatusCodePagesWithReExecute("/User/PageNotFound", "?code={0}"));


app.UseRouting();

app.UseSession();  ///Session çalýþmasý için

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();





app.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
         );


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapHub<OrderHub>("/hubs/orders"); // YENÝ: tarayýcýlar buraya baðlanýr

app.Run();
