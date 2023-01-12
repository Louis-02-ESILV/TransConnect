using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TransConnect.Véhicule
{
    [JsonConverter(typeof(VehiculeConverter))]
    abstract class Vehicule : IVehicule
    {
        public int Kilometrage { get; set; }
        public double Coefficient { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="kilometrage">
        /// distance parcouru</param>
        public Vehicule(int kilometrage,double coefficient = 1)
        {
            Kilometrage = kilometrage;
            Coefficient = coefficient;
        }
        public override string ToString()
        {
            return $"Vehicule, Kilometrage : {Kilometrage}";
        }
        public virtual string ShortString()
        {
            return "Vehciule";
        }
    }
}
