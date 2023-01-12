using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TransConnect.Personne
{
    internal class Chauffeur:Salarie
    {
        public double Tarif_Horaire { get;}
        [JsonConverter(typeof(CommandeConverter))]
        public List<Commande> Commandes { get; }
        public Chauffeur(int numero, string nom, string prenom, DateTime naissance, string adresse, string mail, string telephone,string motdepasse, DateTime embauche, string poste, int salaire, double tarif_distance, List<Commande> commandes):base (numero,nom,prenom,naissance,adresse,mail,telephone,motdepasse,embauche,poste,salaire)
        {
            Tarif_Horaire = tarif_distance;
            Commandes = commandes;
        }
        public override string ToString()
        {
            return base.ToString()+$"tarif_Distance : {Tarif_Horaire}" +
                $"\n Commandes : {Commandes}";
        }
        public override string Organigramme()
        {
            return base.Organigramme() + " (Chauffeur)";
        }
        public int CommandesEffectues
        {
            get
            {
                int nombre = 0;
                foreach (Commande commande in Commandes)
                {
                    if (commande.Livraison < DateTime.Now)
                    {
                        nombre++;
                    }
                }
                return nombre;
            }
        }
    }
}
