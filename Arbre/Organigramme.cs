using System.Text.Json.Serialization;
using TransConnect.Personne;

namespace TransConnect.Arbre
{
    internal class Organigramme
    {
        [JsonConverter(typeof(NoeudConverter))]
        public Noeud Racine { get; set; }

        public Organigramme(Noeud identite)
        {
            Racine = identite;
        }
        public Organigramme()
        {
            Racine = null;
        }  
        /// <summary>
        /// return true is the salary already exist in the tree structure
        /// </summary>
        /// <param name="n"></param>
        /// <param name="employe"></param>
        /// <returns></returns>
        public bool Contains(Noeud n, Salarie employe)
        {
            if (n == null)
            {
                return false;
            }

            if (n.Salarie.Numero == employe.Numero)
            {
                return true;
            }

            // Recherche dans les sous-arbres de l'arbre n-aire
            foreach (Noeud enfant in n.Fils)
            {
                if (Contains(enfant, employe))
                {
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// return true if the salary with this number alredy exist
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        public bool Contains(int numero)
        {
            Noeud employe = Find(Racine, numero);
            if (employe != null)
            {
                return Contains(Racine, employe.Salarie);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <param name="Num"></param>
        /// <returns></returns>
        public Noeud Find(Noeud n, int Num)
        {
            if (n != null)
            {
                if (n.Salarie.Numero == Num)
                {
                    return n;
                }
                else
                {
                    for (int i = 0; i < n.Fils.Count; i++)
                    {
                        Noeud retour = Find(n.Fils[i], Num);
                        if (retour != null)
                        {
                            return retour;
                        }
                    }
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        public Noeud FindRespo(Noeud n, int Num)
        {
            if (n != null)
            {
                for (int i = 0; i < n.Fils.Count; i++)
                {
                    if (n.Fils[i].Salarie.Numero == Num)
                    {
                        return n;
                    }
                }
                for (int i = 0; i < n.Fils.Count; i++)
                {
                    Noeud retour = FindRespo(n.Fils[i], Num);
                    if (retour != null)
                    {
                        return retour;
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }
        public List<Chauffeur> FindChauffeurs(Noeud n)
        {
            List<Chauffeur> retour = new List<Chauffeur>();
            if (n != null)
            {
                if (n.Salarie is Chauffeur)
                {
                    retour.Add((Chauffeur)n.Salarie);
                }
                for (int i = 0; i < n.Fils.Count; i++)
                {
                    retour.AddRange(FindChauffeurs(n.Fils[i]));
                }
            }
            return retour;
        }
        /// <summary>
        /// check the connexion information to access to his session
        /// </summary>
        /// <param name="noeud"></param>
        /// <param name="mail"></param>
        /// <param name="mdp"></param>
        /// <returns></returns>
        public Noeud Connect(Noeud noeud,string mail, string mdp)
        {
            if (noeud != null)
            {
                if (noeud.Salarie.Mail == mail && noeud.Salarie.MotDePasse == mdp)
                {
                    return noeud;
                }
                else
                {
                    for (int i = 0; i < noeud.Fils.Count; i++)
                    {
                        Noeud retour = Connect(noeud.Fils[i], mail, mdp);
                        if (retour != null)
                        {
                            return retour;
                        }
                    }
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Add a new salary in the tree structure
        /// </summary>
        /// <param name="ajout"></param>
        /// <param name="parent"></param>
        public void Add(Salarie ajout, Noeud parent)
        {
            if (parent != null)
            {
                Noeud nouveau = new Noeud(ajout, new List<Noeud>());
                parent.Fils.Add(nouveau);
            }
        }
        /// <summary>
        /// Supprime un employe de l'organigramme
        /// </summary>
        /// <param name="n"></param>
        /// <param name="Num"></param>
        public void Remove(int Num)
        {
            Noeud employe = Find(Racine, Num);
            if (employe != null)
            {
                List<Noeud> subordonnes = new();
                if (employe.Fils != null)
                {
                    subordonnes = employe.Fils;
                }
                Noeud respo = FindRespo(Racine, Num);
                respo.Fils.Remove(employe);
                if (subordonnes != null)
                {
                    for (int i = 0; i < subordonnes.Count; i++)
                    {
                        respo.Fils.Add(subordonnes[i]);
                    }
                }
            }
            else
            {
                Console.WriteLine("L'employé n'existe pas");
            }
        }
        public void Move(int num,int numRespo)
        {
            Noeud employe = Find(Racine, num);
            if (employe != null)
            {
                Noeud respo = Find(Racine, numRespo);
                if (respo != null)
                {
                    Noeud ancienRespo = FindRespo(Racine, num);
                    ancienRespo.Fils.Remove(employe);
                    respo.Fils.Add(employe);
                }
                else
                {
                    Console.WriteLine("Le responsable n'existe pas");
                }
            }
            else
            {
                Console.WriteLine("L'employé n'existe pas");
            }
        }
    }
}
