using System.Text.Json;
using System.Text.Json.Serialization;
using TransConnect.Véhicule;

namespace TransConnect
{
    internal class CommandeConverter : JsonConverter<List<Commande>>
    {
        public override List<Commande> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            List<Commande> commandes = new List<Commande>();
            Commande commande = new Commande();
            reader.Read();
            if(reader.TokenType ==JsonTokenType.StartObject)
            {
                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndArray)
                    {
                        return commandes;
                    }
                    if (reader.TokenType == JsonTokenType.EndObject)
                    {
                        commandes.Add(commande);
                        commande = new();
                    }
                    else if (reader.TokenType == JsonTokenType.PropertyName)
                    {
                        string propertyName = reader.GetString();
                        reader.Read();
                        if (propertyName == "Num_Client")
                        {
                            commande.Num_Client = reader.GetInt32();
                        }
                        else if (propertyName == "Prix")
                        {
                            commande.Prix = reader.GetDouble();
                        }
                        else if (propertyName == "Vehicule")
                        {
                            commande.Vehicule = JsonSerializer.Deserialize<Vehicule>(ref reader, options);
                        }
                        else if (propertyName == "Num_Chauffeur")
                        {
                            commande.Num_Chauffeur = reader.GetInt32();
                        }
                        else if (propertyName == "Produit")
                        {
                            commande.Produit = reader.GetString();
                        }
                        else if (propertyName == "Livraison")
                        {
                            commande.Livraison = DateTime.Parse(reader.GetString());
                        }
                        else
                        {
                            throw new JsonException();
                        }
                    }
                }
            }
            return commandes;
        }
        public override void Write(Utf8JsonWriter writer, List<Commande> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            for (int i = 0; i < value.Count; i++)
            {
                writer.WriteStartObject();
                writer.WriteNumber("Num_Client", value[i].Num_Client);
                writer.WriteNumber("Prix", value[i].Prix);
                writer.WriteNumber("Num_Chauffeur", value[i].Num_Chauffeur);
                writer.WriteString("Produit", value[i].Produit);
                writer.WritePropertyName("Vehicule");
                JsonSerializer.Serialize<Vehicule>(writer, value[i].Vehicule, options);
                writer.WritePropertyName("Livraison");
                JsonSerializer.Serialize<DateTime>(writer, value[i].Livraison, options);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }
    }
}
