using System.Text.Json;
using System.Text.Json.Serialization;

namespace Bank.Common.Utilities
{
    public static class JsonSerializerExtension
    {
        public static T DeserializeTo<T>(this string json)
        {
            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true
            };
            options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            return JsonSerializer.Deserialize<T>(json, options);
        }


    }
}
