namespace Bagery.WebUI.Models.PanelModels
{
    public record PanelMenuItem(string Label, string Icon, string Url);
    public record PanelMenuSection(string Title, List<PanelMenuItem> Items);

    // Menünün tamamı: profil kartı + bölümler
    public record PanelSidebarModel(string FullName, string? ImageUrl, string RoleLabel, List<PanelMenuSection> Sections);
}