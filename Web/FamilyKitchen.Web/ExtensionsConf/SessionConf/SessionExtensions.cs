namespace FamilyKitchen.Web.ExtensionsConf.SessionConf
{

    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;

    public static class SessionExtensions
    {
        public static void SetDataObject<T>(this ISession session, string key, T value)
        {

            session.SetString(key, JsonConvert.SerializeObject(value));

        }

        public static T GetDataObject<T>(this ISession session, string key)
        {

            var value = session.GetString(key);

            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
