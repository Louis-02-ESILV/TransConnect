using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransConnect.Personne;
using System.Text.Json.Serialization;
using System.ComponentModel.Design;

namespace TransConnect.Arbre
{
    internal class Noeud
    {
        [JsonConverter(typeof(PersonConverter))]
        public Salarie Salarie { get; }
        public List<Noeud> Fils { get; set; }
        public Noeud(Salarie salarie, List<Noeud> fils=null)
        {
            Salarie = salarie;
            if(fils != null)
            {
                Fils = fils;
            }
        }
        public override string ToString()
        {
            return $"{Salarie.Nom} {Fils.Count}";
        }
        /// <summary>
        /// Find the node with the given name
        /// </summary>
        /// <param name="nom"></param>
        /// <returns></returns>
        public Noeud Find(string nom)
        {
            if (Salarie.Nom == nom)
            {
                return this;
            }
            else
            {
                foreach (Noeud n in Fils)
                {
                    Noeud? result = n.Find(nom);
                    if (result != null)
                    {
                        return result;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// return the node if the salary is a driver
        /// </summary>
        /// <returns></returns>
        public Chauffeur FindChauffeur()
        {
            if (Salarie is Chauffeur chauffeur)
            {
                return chauffeur;
            }
            else
            {
                foreach (Noeud n in Fils)
                {
                    Chauffeur? result = n.FindChauffeur();
                    if (result != null)
                    {
                        return result;
                    }
                }
                return null;
            }
        }
        /// <summary>
        /// Print the tree structure from this node
        /// </summary>
        /// <param name="n"></param>
        /// <param name="profondeur"></param>
        public void AffichageArborescence(Noeud n, int profondeur = 0)
        {
            if (n != null)
            {
                for (int i = 0; i < profondeur; i++)
                {
                    Console.Write("   ");
                }
                Console.Write("|--");
                if (n.Salarie != null)
                {
                    Console.WriteLine(n.Salarie.Organigramme());
                }
                if (n.Fils != null)
                {
                    for (int i = 0; i < n.Fils.Count; i++)
                    {
                        AffichageArborescence(n.Fils[i], profondeur + 1);
                    }
                }

            }
        }
    }
}
