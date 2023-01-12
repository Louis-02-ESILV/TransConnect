using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TransConnect.Personne
{
    internal class PersonConverter: JsonConverter<Salarie>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(Salarie).IsAssignableFrom(typeToConvert);
        }
        public override Salarie Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            double tarif_distance = 0;
            int numero,salaire;
            numero = salaire  = 0;
            string nom, prenom, adresse, mail, telephone, poste,motdepasse;
            nom = prenom = adresse = mail = telephone = poste = motdepasse = null;
            DateTime naissance = DateTime.Now;
            DateTime embauche = DateTime.Now;
            List<Commande> commandes = new();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    if(tarif_distance!=0)
                    {
                        return new Chauffeur(numero, nom, prenom, naissance, adresse, mail, telephone,motdepasse, embauche, poste, salaire, tarif_distance, commandes);
                    }
                    else
                    {
                        return new Salarie(numero, nom, prenom, naissance, adresse, mail, telephone,motdepasse, embauche, poste, salaire);
                    }
                }
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string? propertyName = reader.GetString();
                    reader.Read();
                    switch (propertyName)
                    {
                        case "Numero":
                            numero = reader.GetInt32();
                            break;
                        case "Nom":
                            nom = reader.GetString();
                            break;
                        case "Prenom":
                            prenom = reader.GetString();
                            break;
                        case "Naissance":
                            naissance = DateTime.Parse(reader.GetString());
                            break;
                        case "Adresse":
                            adresse = reader.GetString();
                            break;
                        case "Mail":
                            mail = reader.GetString();
                            break;
                        case "Telephone":
                            telephone = reader.GetString();
                            break;
                        case "Embauche":
                            embauche = DateTime.Parse(reader.GetString());
                            break;
                        case "Poste":
                            poste = reader.GetString();
                            break;
                        case "Salaire":
                            salaire = reader.GetInt32();
                            break;
                        case "Tarif_Horaire":
                            tarif_distance = reader.GetDouble();
                            break;
                        case "Commandes":
                            commandes = JsonSerializer.Deserialize<List<Commande>>(ref reader, options);
                            break;
                        case "MotDePasse":
                            motdepasse = reader.GetString();
                            break;
                        default:
                            break;
                    }
                }
            }
            throw new JsonException();
        }
        public override void Write(Utf8JsonWriter writer, Salarie value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("Numero", value.Numero);
            writer.WriteString("Nom", value.Nom);
            writer.WriteString("Prenom", value.Prenom);
            writer.WriteString("Naissance", value.Naissance.ToString("dd/MM/yyyy"));
            writer.WriteString("Adresse", value.Adresse);
            writer.WriteString("Mail", value.Mail);
            writer.WriteString("Telephone", value.Telephone);
            writer.WriteString("MotDePasse", value.MotDePasse);
            if (value is Salarie)
            {
                writer.WriteString("Embauche", value.Embauche.ToString("dd/MM/yyyy"));
                writer.WriteString("Poste", value.Poste);
                writer.WriteNumber("Salaire", value.Salaire);
            }
            if (value is Chauffeur chauffeur)
            {
                writer.WriteNumber("Tarif_Horaire", chauffeur.Tarif_Horaire);
                var option = new JsonSerializerOptions { WriteIndented = true };
                CommandeConverter commandewrite = new CommandeConverter();
                writer.WritePropertyName("Commandes");
                commandewrite.Write(writer, chauffeur.Commandes, option);
            }
            writer.WriteEndObject();
        }
    }
}
