using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TransConnect.Véhicule
{
    internal class ListVehiculeConverter:JsonConverter<List<Vehicule>>
    {
        public override List<Vehicule>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            List<Vehicule>? vehicules = null;
            while (reader.TokenType!= JsonTokenType.EndArray)
            {
                vehicules.Add(JsonSerializer.Deserialize<Vehicule>(ref reader, options));
            }
            return vehicules;
        }
        public override void Write(Utf8JsonWriter writer, List<Vehicule> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (Vehicule vehicule in value)
            {
                JsonSerializer.Serialize(writer, vehicule, options);
            }
            writer.WriteEndArray();
        }
    }
}
