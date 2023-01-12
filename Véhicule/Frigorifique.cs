using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect.Véhicule
{
    internal class Frigorifique : Camion, IVehicule
    {
        public int GrpElectrogene { get; }
        public Frigorifique (int kilometrage,int volume,string matiere,int grp):base(kilometrage,volume,matiere,1.8)
        {
            if(grp<=3 && grp>=0)
            {
                GrpElectrogene = grp;
            }
            else
            {
                GrpElectrogene = 0;
            }
        }
        public override string ToString()
        {
            return $"un Camion Frigorique de volume :{Volume}, {Kilometrage}km, pouvant transporter du {Matiere} avec {GrpElectrogene} groupe(s) electrogène(s)";
        }
        public override string ShortString()
        {
            return "un camion frigorique";
        }
    }
}
