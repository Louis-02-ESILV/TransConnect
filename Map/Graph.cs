namespace TransConnect.Map
{
    internal class Graph
    {
        public Ville Depart { get; set; }
        public List<Ville> Villes { get; }
        public List<Route> Routes { get; }
        public Graph()
        {
            Villes = new List<Ville>();
            Routes = new List<Route>();
        }
        public Graph(string filename)
        {
            Villes = new List<Ville>();
            Routes = new List<Route>();
            string[] lines = System.IO.File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                string[] words = line.Split(';');
                Ville ville1 = new Ville(words[0]);
                Ville ville2 = new Ville(words[1]);
                int distance = int.Parse(words[2]);
                TimeSpan temps = TimeSpan.Parse(words[3]);
                Route route = new Route(ville1, ville2, distance, temps);
                if (Find(ville1.Nom) == null)
                {
                    Villes.Add(ville1);
                }
                if (Find(ville2.Nom) == null)
                {
                    Villes.Add(ville2);
                }
                Routes.Add(route);
            }
            Depart = Villes[0];
            Dijkstra();
        }
        /// <summary>
        /// Save the date of the graph in a file
        /// </summary>
        /// <param name="filepath">file path</param>
        public void SaveData(string filepath)
        {
            string[] retour = new string[Routes.Count]; 
            for (int i = 0; i < Routes.Count; i++)
            {
                retour[i] += Routes[i];
            }
            File.WriteAllLines(filepath, retour);
        }
        public void AddCity(string name)
        {
            Villes.Add(new Ville(name));
        }
        public void AddRoute(string depart, string arrivee, int distance, TimeSpan temps)
        {
            Routes.Add(new Route(Find(depart), Find(arrivee), distance, temps));
        }
        /// <summary>
        /// Find a city from his name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Ville Find(string name)
        {
            foreach (Ville v in Villes)
            {
                if (v.Nom == name)
                {
                    return v;
                }
            }
            return null;
        }
        /// <summary>
        /// Get roads which direct to the city
        /// </summary>
        /// <param name="ville"></param>
        /// <returns></returns>
        public List<Route> GetRoute(Ville ville)
        {
            List<Route> retour = new List<Route>();
            for (int i = 0; i < Routes.Count; i++)
            {
                if (Routes[i].Ville1.Equals(ville) || Routes[i].Ville2.Equals(ville))
                {
                    retour.Add(Routes[i]);
                }
            }
            return retour;
        }
        /// <summary>
        /// Set the better Itinerary for each city
        /// </summary>
        public void Dijkstra()
        {
            Dictionary<string, Itineraire> Dijkstra = new(Villes.Count) {};
            for (int i = 0; i < Villes.Count; i++)
            {
                Dijkstra.Add(Villes[i].Nom, new());
            }
            List<Ville> parcouru = new List<Ville>();
            Ville depart = Depart;
            for (int i = 0; i < Villes.Count; i++)
            {
                if(i!=0)
                {
                    depart = Villes[i];
                }
                List<Route>parcours = GetRoute(depart);
                for (int j = 0; j < parcours.Count; j++)
                {
                    if (parcours[j].Ville1==depart && !parcouru.Contains(parcours[j].Ville2))
                    {
                        if(Dijkstra[parcours[j].Ville2.Nom].Distance>= Dijkstra[parcours[j].Ville1.Nom].Distance+parcours[j].Distance)
                        {
                            Dijkstra[parcours[j].Ville2.Nom] = new Itineraire(Dijkstra[parcours[j].Ville1.Nom].Trajet,Depart);
                            Dijkstra[parcours[j].Ville2.Nom].Add(parcours[j]);
                        }
                    }
                    else if (parcours[j].Ville2 == depart && !parcouru.Contains(parcours[j].Ville1))
                    {
                        if (Dijkstra[parcours[j].Ville1.Nom].Distance >= Dijkstra[parcours[j].Ville2.Nom].Distance + parcours[j].Distance)
                        {
                            Dijkstra[parcours[j].Ville1.Nom] = new Itineraire(Dijkstra[parcours[j].Ville2.Nom].Trajet,Depart);
                            Dijkstra[parcours[j].Ville1.Nom].Add(parcours[j]);
                        }
                    }
                }
                parcouru.Add(depart);
            }
            foreach (Ville city in Villes)
            {
                city.AddTrajet(Dijkstra[city.Nom]);
            }
            Villes.Find(x => x.Nom == Depart.Nom).Trajet = new Itineraire(new List<Route>(), Depart);      
        }
    }


}
