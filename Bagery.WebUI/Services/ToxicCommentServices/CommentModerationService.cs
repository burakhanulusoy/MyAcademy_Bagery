using OpenAI.Chat;

namespace Bagery.WebUI.Services
{
    public class CommentModerationService(IConfiguration configuration)
    {
        private const string Model = "gpt-5.6-luna";

        private const string Prompt = """
            Sen bir blog yorum moderatörüsün.

            Küfür, argo, hakaret, aşağılama, kişisel saldırı,
            tehdit, nefret söylemi, cinsel/+18 uygunsuz içerik
            veya taciz içeren yorumlar TOXIC'tir.

            Normal eleştiri ve fikir ayrılığı TOXIC değildir.

            Sadece TOXIC veya SAFE yaz.
            """;

        public async Task<bool> IsToxicAsync(
            string comment,
            CancellationToken cancellationToken = default)
        {
            var apiKey = configuration["OpenAI:ApiKey"]
                ?? throw new InvalidOperationException(
                    "OpenAI API anahtarı bulunamadı.");

            var client = new ChatClient(Model, apiKey);

            ChatMessage[] messages =
            [
                new SystemChatMessage(Prompt),
                new UserChatMessage(comment)
            ];

            var response = await client.CompleteChatAsync(
                messages,
                cancellationToken: cancellationToken);

            return response.Value.Content[0].Text
                .Trim()
                .Equals("TOXIC", StringComparison.OrdinalIgnoreCase);
        }
    }
}