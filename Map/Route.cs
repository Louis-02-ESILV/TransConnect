using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect.Map
{
    internal class Route
    {
        public Ville Ville1 { get; set; }
        public Ville Ville2 { get; set; }
        public TimeSpan Temps { get; }
        public int Distance { get; }
        public Route(Ville ville1,Ville vill2,int distance, TimeSpan temps)
        {
            Ville1 = ville1;
            Ville2 = vill2;
            Temps = temps;
            Distance = distance;
        }
        public override string ToString()
        {
            return $"{Ville1};{Ville2};{Distance};{Temps}";
        }
        /// <summary>
        /// check if the city is in the road
        /// </summary>
        /// <param name="ville"></param>
        /// <returns></returns>
        public bool Contains(Ville ville)
        {
            return (Ville1 == ville || Ville2 == ville);
        }
        /// <summary>
        /// Switch a city to the other side of the road
        /// </summary>
        public void Switch()
        {
            Ville buffer = Ville1;
            Ville1 = Ville2;
            Ville2 = buffer;
        }
    }
}
