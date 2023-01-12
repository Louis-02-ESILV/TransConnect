using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect.Véhicule
{
    internal class Voiture : Vehicule, IVehicule
    {
        public int Capacite { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="kilometrage">
        /// distance parcouru</param>
        /// <param name="capacite">
        /// nombre de personne que peut contenir le véhciule</param>
        public Voiture (int kilometrage,int capacite):base(kilometrage,1.25)
        {
            Capacite = capacite;
        }
        public override string ToString()
        {
            return $"une voiture de {Capacite} places";
        }
        public override string ShortString()
        {
            return "une voiture";
        }
    }
}
