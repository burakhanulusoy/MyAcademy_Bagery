namespace Bagery.WebUI.Services.BageryAi
{
    internal class BageryAiDecision
    {
        public bool InScope { get; set; }
        public string Message { get; set; }
        public List<Guid> ProductIds { get; set; } = [];
    }
}
