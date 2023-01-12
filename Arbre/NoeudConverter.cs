using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TransConnect.Personne;

namespace TransConnect.Arbre
{
    internal class NoeudConverter : JsonConverter<Noeud>
    {
        public override Noeud Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }
            Noeud n = null;
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return n;
                }
                else if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException();
                }
                string propertyName = reader.GetString();
                reader.Read();
                if (propertyName == "Fils")
                {
                    n.Fils = JsonSerializer.Deserialize<List<Noeud>>(ref reader, options);
                }
                else if (propertyName== "Racine")
                {
                    n = JsonSerializer.Deserialize<Noeud>(ref reader, options);
                }
                else if (propertyName == "Salarie")
                {
                    Salarie salarie = JsonSerializer.Deserialize<Salarie>(ref reader, options);
                    n = new Noeud(salarie);
                }
                else
                {
                    throw new JsonException();
                }
            }
            throw new JsonException();
        }
        public override void Write(Utf8JsonWriter writer, Noeud value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("Salarie");
            JsonSerializer.Serialize<Salarie>(writer, value.Salarie, options);
            writer.WritePropertyName("Fils");
            JsonSerializer.Serialize<List<Noeud>>(writer, value.Fils, options);
            writer.WriteEndObject();
        }
    }
}
