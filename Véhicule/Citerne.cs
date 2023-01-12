using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect.Véhicule
{
    /// <summary>
    /// Le camion-citerne, avec sa cuve, est utilisé pour transporter des liquides ou encore des produits gazeux pour l'industrie chimique et agroalimentaire notamment. 
    /// Le camion-citerne peut avoir une cuve très différente suivant les produits qu'il transporte. 
    /// </summary>
    internal class Citerne : Camion, IVehicule
    {
        public string CuveType { get; }
        /// <summary>
        /// Le camion-citerne, avec sa cuve, est utilisé pour transporter des liquides ou encore des produits gazeux pour l'industrie chimique et agroalimentaire notamment. 
        /// Le camion-citerne peut avoir une cuve très différente suivant les produits qu'il transporte. 
        /// </summary>
        /// <param name="kilometrage">
        /// distance parcouru</param>
        /// <param name="volume">
        /// capactité max que peut contenir le camion</param>
        /// <param name="matiere">
        /// matière qu'il peut transporter</param>
        /// <param name="cuvetype">
        /// type de cuve (préssurisé,isolé,réfrigéré)</param>
        public Citerne(int kilometrage, int volume, string matiere, string cuvetype) : base(kilometrage, volume, matiere,1.7)
        {
            CuveType = cuvetype;
        }
        public override string ToString()
        {
            return base.ToString() + $"avec une cuve {CuveType}";
        }
        public override string ShortString()
        {
            return "Citerne";
        } 
    }
}
