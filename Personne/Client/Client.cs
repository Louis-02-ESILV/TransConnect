using System.Text.Json.Serialization;
using TransConnect.Map;

namespace TransConnect.Personne.Client
{
    internal class Client : Personne,IEquatable<Client>,IComparer<Client>
    {
        [JsonConverter(typeof(CommandeConverter))]
        public List<Commande>? Commandes { get; set; }
        public Ville Ville { get; set; }
        public Client(int numero, string nom, string prenom, DateTime naissance, string adresse, string mail, string telephone,string motdepasse, Ville ville, List<Commande>? commandes = null) : base(numero, nom, prenom, naissance, adresse, mail, telephone,motdepasse)
        {
            Commandes = commandes;
            Ville = ville;
        }
        public override string ToString()
        {
            string retour = base.ToString();
            if(Commandes!= null)
            {
                retour+="Commandes :\n";
                for (int i = 0; i < Commandes.Count; i++)
                {
                    retour += $"Commande N°{i+1}:\n\t{Commandes[i]}\n";
                }
            }
            return retour + $"Ville : {Ville}";
        }
        public string ShortString()
        {
            return $"Client N°{Numero} : {Nom} à {Ville.Nom}";
        }
        public void GetCity(Graph graph)
        {
            this.Ville = graph.Find(Ville.Nom);
        }
        /// <summary>
        /// Retourne le Prix total de toutes les commandes du client
        /// </summary>
        [JsonIgnore]
        public double CoutTotal
        {
            get
            {
                double cout = 0;
                if (Commandes != null)
                {
                    for (int i = 0; i < Commandes.Count; i++)
                    {
                        cout += Commandes[i].Prix;
                    }
                }
                return Math.Round(cout,2);
            }
        }
        /// <summary>
        /// retourne le cout moyen de toutes les commandes du client
        /// </summary>
        [JsonIgnore]
        public double CoutMoyen
        {
            get
            {
                if (Commandes != null)
                {
                    return Math.Round(CoutTotal / Commandes.Count,2);
                }
                else
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// permet de determine si le client a le même numéro que celui passé en paramètre
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Client? other)
        {
            if (other == null)
            {
                return false;
            }
            return this.Numero == other.Numero;
        }
        /// <summary>
        /// Compare 2 clients entre eux
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(Client? x, Client? y)
        {
            if (x == null || y == null)
            {
                return 0;
            }
            return x.Numero.CompareTo(y.Numero);
        }
    }
}
