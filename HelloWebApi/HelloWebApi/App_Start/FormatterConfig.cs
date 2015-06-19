using Newtonsoft.Json;
using System.Net.Http.Formatting;

namespace HelloWebApi.App_Start
{
    public static class FormatterConfig
    {
        public static void ConfigureFormatters(MediaTypeFormatterCollection formatters)
        {
            JsonSerializerSettings settings = formatters.JsonFormatter.SerializerSettings;
            settings.Formatting = Formatting.None;
        }
    }
}
