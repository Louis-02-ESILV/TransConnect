using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect.Véhicule
{
    internal class Camion:Vehicule, IVehicule
    {
        public int Volume { get; }
        public string Matiere { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="kilometrage">
        /// distance parcouru</param>
        /// <param name="volume">
        /// capactité max que peut contenir le camion</param>
        /// <param name="matiere">
        /// matière qu'il peut transporter</param>
        public Camion(int kilometrage, int volume, string matiere,double coefficient = 1.4):base(kilometrage,coefficient)
        {
            Volume = volume;
            Matiere = matiere;
        }
        public override string ToString()
        {
            return $"Camion ayant {Kilometrage}km, un volume de {Volume}L, et pouvant transporter du {Matiere}";
        }
        public override string ShortString()
        {
            return "Camion";
        }
    }
}
