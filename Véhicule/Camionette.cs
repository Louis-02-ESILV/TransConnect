using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect.Véhicule
{
    internal class Camionette : Vehicule, IVehicule
    {
        public string Usage { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="kilometrage">
        /// distance parcouru</param>
        /// <param name="usage">
        /// usage pour lequel est fait le véhciule</param>
        public Camionette(int kilometrage, string usage):base (kilometrage,1.4)
        {
            Usage = usage;
        }
        public override string ToString()
        {
            return $"Camionette utilisé pour du {Usage}";
        }
        public override string ShortString()
        {
            return "Camionette";
        }
    }
}
