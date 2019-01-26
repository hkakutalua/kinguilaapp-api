using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KinguilaAppApi.Converters
{
    public class OnlyDateConverter : IsoDateTimeConverter
    {
        public OnlyDateConverter()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}