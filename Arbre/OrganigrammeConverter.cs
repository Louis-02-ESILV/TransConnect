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
    internal class OrganigrammeConverter : JsonConverter<Organigramme>
    {
        public override Organigramme Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Organigramme organigramme = new Organigramme(null);
            Noeud racine = new Noeud(null);
            organigramme.Racine = racine;
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.StartObject)
                {
                    racine = JsonSerializer.Deserialize<Noeud>(ref reader, options);
                    organigramme.Racine = racine;
                }
            }
            return organigramme;
        }
        public override void Write(Utf8JsonWriter writer, Organigramme value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.Racine, options);
        }
    }
}
