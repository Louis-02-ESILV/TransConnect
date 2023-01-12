using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TransConnect.Véhicule
{
    internal class VehiculeConverter:JsonConverter<Vehicule>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(Vehicule).IsAssignableFrom(typeToConvert);
        }
        public override Vehicule? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            int kilometrage = 0;
            int capacite = 0;
            string usage = null;
            int volume = 0;
            string matiere = null;
            int nb_benne = 0;
            string cuvetype = null;
            int grp = 0;
            bool grue = false;
            while(reader.Read() && reader.TokenType !=JsonTokenType.EndArray)
            {
                if(reader.TokenType == JsonTokenType.EndObject)
                {
                    if (grp != 0)
                    {
                        return new Frigorifique(kilometrage, volume, matiere, grp);
                    }
                    else if (nb_benne != 0)
                    {
                        return new Benne(kilometrage, volume, matiere, nb_benne,grue);
                    }
                    else if (cuvetype != null)
                    {
                        return new Citerne(kilometrage, volume, matiere, cuvetype);
                    }
                    else if(matiere != null && volume != 0)
                    {
                        return new Camion(kilometrage, volume, matiere);
                    }
                    else if (usage != null)
                    {
                        return new Camionette(kilometrage, usage);
                    }
                    else if (capacite !=0)
                    {
                        return new Voiture(kilometrage,capacite);
                    }
                }
                if(reader.TokenType ==JsonTokenType.PropertyName)
                {
                    string? propertyName = reader.GetString();
                    reader.Read();
                    switch(propertyName)
                    {
                        case "Kilometrage":
                            kilometrage = reader.GetInt32();
                            break;
                        case "Capacite":
                            capacite = reader.GetInt32();
                            break;
                        case "Usage":
                            usage = reader.GetString();
                            break;
                        case "Volume":
                            volume = reader.GetInt32();
                            break;
                        case "Matiere":
                            matiere = reader.GetString();
                            break;
                        case "Nb_bennes":
                            nb_benne = reader.GetInt32();
                            break;
                        case "Cuvetype":
                            cuvetype = reader.GetString();
                            break;
                        case "GrpElectrogene":
                            grp = reader.GetInt32();
                            break;
                        case "Grue":
                            grue = reader.GetBoolean();
                            break;
                        default:
                            break;
                    }
                }
            }
            return null;
        }
        public override void Write(Utf8JsonWriter writer, Vehicule value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("Kilometrage", value.Kilometrage);
            if (value is Voiture voiture)
            {
                writer.WriteNumber("Capacite", voiture.Capacite);
            }
            if (value is Camionette camionette)
            {
                writer.WriteString("Usage", camionette.Usage);
            }
            if (value is Camion camion)
            {
                writer.WriteNumber("Volume", camion.Volume);
                writer.WriteString("Matiere", camion.Matiere);
                if(camion is Benne benne)
                {
                    writer.WriteNumber("Nb_Bennes", benne.Nb_Bennes);
                    writer.WriteBoolean("Grue", benne.Grue);
                }
                else if (camion is Citerne citerne)
                {                    
                    writer.WriteString("CuveType", citerne.CuveType);
                }
                else if (camion is Frigorifique frigorifique)
                {
                    writer.WriteNumber("GrpElectrogene", frigorifique.GrpElectrogene);
                }
            }
            writer.WriteEndObject();
        }
    }
}
