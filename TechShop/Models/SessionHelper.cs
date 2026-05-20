using Newtonsoft.Json;

namespace TechShop.Models
{
    public static class SessionHelper
    {
        // Objektum mentése a Session-be JSON formátumban
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        // Objektum kiolvasása a Session-ből
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}