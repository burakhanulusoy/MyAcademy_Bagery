namespace Bagery.WebUI.Models.PanelModels
{
    // NewTab: true ise link yeni sekmede açılır (panel kapanmaz)
    public record PanelMenuItem(string Label, string Icon, string Url, bool NewTab = false);
    public record PanelMenuSection(string Title, List<PanelMenuItem> Items);

    public record PanelSidebarModel(string FullName, string? ImageUrl, string RoleLabel, List<PanelMenuSection> Sections);
}