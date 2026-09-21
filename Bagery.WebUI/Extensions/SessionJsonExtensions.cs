using System.Text.Json;

namespace Bagery.WebUI.Extensions
{
    // Session sadece string tutabilir; sepeti JSON'a çevirip öyle saklıyoruz.
    // İsim bilerek "SessionExtensions" değil: ASP.NET'te aynı isimde bir sınıf zaten var, çakışmasın.
    public static class SessionJsonExtensions
    {
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T? GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value is null ? default : JsonSerializer.Deserialize<T>(value); // yoksa null döner
        }
    }
}