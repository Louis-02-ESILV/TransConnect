using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using TransConnect.Arbre;
using TransConnect.Map;
using TransConnect.Personne;
using TransConnect.Personne.Client;
using TransConnect.Véhicule;

namespace TransConnect
{
    internal class DataBase
    {
        // Organizational chart of employees
        internal Organigramme Employes { get; }
        // List of drivers
        public List<Chauffeur> Chauffeurs { get; }
        // List of clients
        internal List<Client> Clients { get; }
        // List of clients
        [JsonConverter(typeof(ListVehiculeConverter))]
        internal List<Vehicule> Flotte { get; }
        // Object representing the map
        Graph Carte { get; }
        public DataBase(string salaries, string clients, string flotte, string graph)
        {
            // Initialize fields
            Employes = new();
            Clients = new();
            Flotte = new();
            Carte = new();
            // Read and parse the employee data from the specified file
            JsonDocument text = JsonDocument.Parse(File.ReadAllText(salaries));
            Employes = JsonSerializer.Deserialize<Organigramme>(text);
            text = JsonDocument.Parse(File.ReadAllText(clients));
            Chauffeurs = Employes.FindChauffeurs(Employes.Racine);
            // Read and parse the client data from the specified file
            Clients = JsonSerializer.Deserialize<List<Client>>(text);
            // Read and parse the client data from the specified file
            text = JsonDocument.Parse(File.ReadAllText(flotte));
            Flotte = JsonSerializer.Deserialize<List<Vehicule>>(text);
            // Create a new Graph object using the specified file
            Carte = new Graph(graph);
            GetCities();
        }
        #region data
        /// <summary>
        /// Method used to save the data from the various fields to files
        /// </summary>
        /// <param name="person_Path"></param>
        /// <param name="client_Path"></param>
        /// <param name="flotte_Path"></param>
        /// <param name="graph_Path"></param>
        internal void SaveData(string person_Path, string client_Path, string flotte_Path, string graph_Path)
        {
            // JSON serialization options
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            // Serialize and write the employee,clients and vehicule data to the specified file
            var employees = JsonSerializer.Serialize(Employes, options);
            var clients = JsonSerializer.Serialize(Clients, options);
            var vehicule = JsonSerializer.Serialize(Flotte, options);
            File.WriteAllText(person_Path, employees);
            File.WriteAllText(client_Path, clients);
            File.WriteAllText(flotte_Path, vehicule);
            Carte.SaveData(graph_Path);

        }
        #endregion data
        #region Employes
        /// <summary>
        /// create a salary in the enterprise tree structure
        /// </summary>
        /// <param name="ajout">
        /// salarié qui sera ajouté dans l'organigramme</param>
        /// <param name="NumRespo">
        /// numéro de son responsable</param>
        /// <param name="subordonnes">
        /// potentiels subordonnées qu'il aura à sa charge</param>
        public void CreationSalarie(Salarie ajout, int NumRespo)
        {
            Noeud responsable = Employes.Find(Employes.Racine, NumRespo);
            if (!Employes.Contains(Employes.Racine, ajout))
            {
                Employes.Add(ajout, responsable);
            }
            else
            {
                Console.WriteLine("Le salarié existe déjà");
            }
        }
        /// <summary>
        /// Remove a salary
        /// </summary>
        /// <param name="numero">Number of the salary to remove</param>
        public void SuppressionSalarie(int numero)
        {
            Employes.Remove(numero);
        }
        /// <summary>
        /// print the enterprise tree structure
        /// </summary>
        public void Organigramme()
        {
            Employes.Racine.AffichageArborescence(Employes.Racine);
        }
        public Chauffeur GetChauffeur(DateTime livraison)
        {
            List<Chauffeur> conducteurs = new List<Chauffeur>();
            Chauffeur retour = null;
            for (int i = 0; i < Chauffeurs.Count; i++)
            {
                bool libre = true;
                for (int j = 0; j < Chauffeurs[i].Commandes.Count; j++)
                {
                    if (Chauffeurs[i].Commandes[j].Livraison == livraison)
                    {
                        libre = false;
                    }
                }
                if (libre)
                {
                    conducteurs.Add(Chauffeurs[i]);
                }
            }
            foreach (Chauffeur conducteur in conducteurs)
            {
                if (retour == null)
                {
                    retour = conducteur;
                }
                else
                {
                    if (conducteur.Commandes.Count < retour.Commandes.Count)
                    {
                        retour = conducteur;
                    }
                }
            }
            return retour;
        }
        #endregion Employes
        #region Clients
        /// <summary>
        /// define the ititnary of the client's cities
        /// </summary>
        public void GetCities()
        {
            foreach (Client client in Clients)
            {
                client.GetCity(Carte);
            }
        }
        /// <summary>
        /// create a new client
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <param name="naissance"></param>
        /// <param name="addresse"></param>
        /// <param name="mail"></param>
        /// <param name="telephone"></param>
        /// <param name="motdepasse"></param>
        /// <param name="ville"></param>
        public void CreationClient(int numero, string nom, string prenom, DateTime naissance, string addresse, string mail, string telephone, string motdepasse, string ville)
        {
            Client ajout = new(numero, nom, prenom, naissance, addresse, mail, telephone, motdepasse,Carte.Find(ville));
            if (!Clients.Contains(ajout))
            {
                Clients.Add(ajout);
            }
            else
            {
                Console.WriteLine("Le client existe déjà");
            }
        }
        /// <summary>
        /// remove a client
        /// </summary>
        /// <param name="numero">Number of the client to remove</param>
        /// <returns></returns>
        public bool SupprimerClient(int numero)
        {
            return Clients.Remove(Clients.Find(x=> x.Numero == numero));
        }
        #endregion Clients
        #region Commande
        /// <summary>
        /// Create a command
        /// </summary>
        /// <param name="client"></param>
        /// <param name="vehicule"></param>
        /// <param name="livraison"></param>
        /// <param name="produit"></param>
        /// <returns></returns>
        public Commande NewCommande(Client client, Vehicule vehicule, DateTime livraison, string produit)
        {
            Chauffeur conducteur = GetChauffeur(livraison);
            double prix = Math.Round(client.Ville.Trajet.TempsTotal.TotalHours * conducteur.Tarif_Horaire*vehicule.Coefficient,2);
            Commande c1 = new Commande(client.Numero, prix, vehicule, conducteur.Numero, produit, livraison);
            Noeud employe = Employes.Find(Employes.Racine, conducteur.Numero);
            if(employe.Salarie is Chauffeur chauffeur)
            {
                chauffeur.Commandes.Add(c1);
            }
            if(client.Commandes == null)
            {
                client.Commandes = new List<Commande>();
            }
            client.Commandes.Add(c1);
            return c1;
        }
        /// <summary>
        /// Get All the command in the society
        /// </summary>
        /// <returns></returns>
        public List<Commande> GetCommandes()
        {
            List<Commande> commandes = new();
            foreach (Client client in Clients)
            {
                if(client.Commandes!=null)
                {
                    foreach (Commande commande in client.Commandes)
                    {
                        commandes.Add(commande);
                    }
                }
            }
            return commandes;
        }
        /// <summary>
        /// Modify a command if the Datetime isn't in the past
        /// </summary>
        /// <param name="commande"></param>
        /// <param name="vehicule"> new vehicule of the command</param>
        /// <param name="livraison">new delivery's datetime</param>
        /// <param name="produit">new product</param>
        public void ModificationCommande(Commande commande, Vehicule vehicule, DateTime livraison, string produit)
        {
            if(commande.Livraison<DateTime.Now)
            {
                Console.Write("la livraison a déjà été éffectué");
            }
            else
            {
                commande.Vehicule = vehicule;
                commande.Livraison = livraison;
                commande.Produit = produit;
            }
        }
        #endregion Commande
        #region Vehicules
        /// <summary>
        /// Get vehcicules who aren't use for the delivery's datetime
        /// </summary>
        /// <param name="Livraison"></param>
        /// <returns></returns>
        public List<Vehicule> GetFlotte(DateTime Livraison)
        {
            List<Vehicule> retour = Flotte;
            foreach (Commande commande in GetCommandes())
            {
                if (commande.Livraison == Livraison)
                {
                    retour.Remove(commande.Vehicule);
                }
            }
            return retour;
        }
        public void CreationVehicule( int capacite = -1, int volume = -1, string matiere = null, string usage = null, int grp_electrogène= -1,bool grue = false,int bennes = -1,string cuvetype = null)
        {
            Vehicule ajout = null;
            if(capacite != -1)
            {
                ajout = new Voiture(0, capacite);
            }
            else if (volume != -1 && matiere != null)
            {
                ajout = new Camion(0, volume, matiere);
            }
            else if (usage != null)
            {
                ajout = new Camionette(0, usage);
            }
            else if (grp_electrogène != -1)
            {
                ajout = new Frigorifique(0,volume,matiere, grp_electrogène);
            }
            else if (bennes != -1)
            {
                ajout = new Benne(0,volume,matiere,bennes, grue);
            }
            else if (cuvetype != null)
            {
                ajout = new Citerne(0,volume,matiere, cuvetype);
            }
            else
            {
                Console.WriteLine("Le vehicule n'a pas pu être créer");
            }
            if (!Flotte.Contains(ajout))
            {
                Flotte.Add(ajout);
                Console.WriteLine("Le véhicules a été ajouté");
            }
            else
            {
                Console.WriteLine("Le véhicule existe déjà");
            }
        }
        #endregion Vehicules
    }
}
