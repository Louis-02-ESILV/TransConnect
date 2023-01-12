using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect.Map
{
    internal class Itineraire
    {
        public List<Route> Trajet { get; set; }
        public Itineraire(List<Route> trajet,Ville depart)
        {
            Trajet = new List<Route>();
            for (int i = 0; i < trajet.Count; i++)
            {
                if (trajet[i].Ville1==depart)
                {
                    Trajet.Add(trajet[i]);
                    depart = trajet[i].Ville2;
                }
                else if(trajet[i].Ville2 == depart)
                {
                    Trajet.Add(trajet[i]);
                    depart = trajet[i].Ville1;
                } 
            }
        }
        public Itineraire()
        {
            Trajet = new List<Route> ();
        }
        /// <summary>
        /// Add a road to the itinary
        /// </summary>
        /// <param name="ajout"></param>
        public void Add(Route ajout)
        {
            if(Trajet ==null)
            {
                Trajet = new List<Route>();
            }
            if (ajout != null)
            {
                if (Trajet.Count > 0)
                {
                    if (Trajet[Trajet.Count - 1].Ville2 == ajout.Ville1)
                    {
                        Trajet.Add(ajout);
                    }
                    else if (Trajet[Trajet.Count - 1].Ville2 == ajout.Ville2)
                    {
                        ajout.Switch();
                        Trajet.Add(ajout);
                    }
                }
                else
                {
                    Trajet.Add(ajout);
                }
            }
        }
        /// <summary>
        /// Total time of the itinary
        /// </summary>
        public TimeSpan TempsTotal
        {
            get
            {
                TimeSpan temps = new(0,0,0);
                for (int i = 0; i < Trajet.Count; i++)
                {
                    temps += Trajet[i].Temps;
                }
                return temps;
            }
        }
        /// <summary>
        /// Total distance of the itinary
        /// </summary>
        public int Distance
        {
            get
            {
                int distance = int.MaxValue;
                if(Trajet.Count > 0)
                {
                    distance = 0;
                    for (int i = 0; i < Trajet.Count; i++)
                    {
                        distance += Trajet[i].Distance;
                    }
                }
                return distance;
            }
        }
        /// <summary>
        /// Return the list of cities in the itinary
        /// </summary>
        public string Etape
        {
            get
            {
                string etape = null;
                if(Trajet.Count>0)
                {
                    for (int i = 0; i < Trajet.Count; i++)
                    {
                        etape += Trajet[i].Ville1 + "; ";
                    }
                    etape += Trajet[Trajet.Count - 1].Ville2;
                }
                return etape ;
            }
        }
        public override string ToString()
        {
            return $"{Etape}; \nDistance : {Distance}km durée : {TempsTotal}";
        }
    }
}
