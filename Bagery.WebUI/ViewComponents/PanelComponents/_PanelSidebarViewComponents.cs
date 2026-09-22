using Bagery.WebUI.Entities;
using Bagery.WebUI.Models.PanelModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.ViewComponents.PanelComponents
{
    // Writer ve User aynı menüyü kullanır; farkları (Yazarlık bölümü, link adresleri) role göre burada belirlenir
    public class _PanelSidebarViewComponents(UserManager<AppUser> _userManager) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var isWriter = HttpContext.User.IsInRole("Writer");

            // Her rolün kendi adresi: /Writer/... veya /User/...
            var area = isWriter ? "Writer" : "User";
            var dashboardUrl = isWriter ? "/Writer/Static/Dashboard" : "/User/Static/Index"; // login yönlendirmelerinle aynı

            var sections = new List<PanelMenuSection>
            {
                new("Genel", [ new("İstatistiklerim", "bx-bar-chart-alt-2", dashboardUrl) ])
            };

            // Sadece Writer
            if (isWriter)
            {
                sections.Add(new("Yazarlık",
                [
                    new("Bloglarım", "bx-news", "/Writer/Blog/Index"),
                    new("Yeni blog yaz", "bx-edit-alt", "/Writer/Blog/CreateBlog"),
                    new("Yorumlarım", "bx-comment-detail", "/Writer/Comment/Index")
                ]));
            }

            // Ortak
            sections.Add(new("Alışveriş",
            [
                new("Siparişlerim", "bx-receipt", $"/{area}/MyOrders/Index"),
                new("Mağazaya git", "bx-store", "/Product/Shop")
            ]));
            sections.Add(new("Hesabım",
            [
                new("Profilim", "bx-user-circle", $"/{area}/Profile/UpdateUser"),
                new("Şifre değiştir", "bx-lock-alt", $"/{area}/Profile/ChangePassword")
            ]));

            return View(new PanelSidebarModel(
                user?.FullName ?? "Kullanıcı",
                user?.ImageUrl,
                isWriter ? "Yazar" : "Üye",
                sections));
        }
    }
}