using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TransConnect.Map
{
    internal class Ville : IEquatable<Ville>
    {
        public string Nom { get; }
        [JsonIgnore]
        public Itineraire Trajet { get; set; }
        public Ville(string nom)
        {
            Nom = nom;
        }
        public override string ToString()
        {
            return Nom;
        }
        /// <summary>
        /// Set the itinerary to the city
        /// </summary>
        /// <param name="trajet"></param>
        public void AddTrajet(Itineraire trajet)
        {
            Trajet = trajet;
        }
        /// <summary>
        /// Check if the city is the same to the other
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Ville other)
        {
            return Nom == other.Nom;
        }
        public static bool operator ==(Ville a, Ville b)
        {
            if(a is null && b is null)
            {
                return true;
            }
            else if (b is null || a is null)
            {
                return false;
            }
            else
            {
                return a.Nom == b.Nom;
            }
        }
        public static bool operator !=(Ville a, Ville b)
        {
            return !(a == b);
        }
    }
}
