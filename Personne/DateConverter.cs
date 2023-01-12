using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TransConnect.Personne
{
    public class DateConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader,Type typeToConvert,JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString()!);
        }
        public override void Write(Utf8JsonWriter writer,DateTime dateTimeValue,JsonSerializerOptions options)
        {
            writer.WriteStringValue(dateTimeValue.ToString("MM/dd/yyyy"));
        }
                
    }
}