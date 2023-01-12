using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransConnect.Map;
using TransConnect.Personne;
using TransConnect.Véhicule;

namespace TransConnect
{
    internal class Commande
    {
        public int Num_Client { get; set; }
        public double Prix { get; set; }
        public Vehicule Vehicule { get; set; }
        public int Num_Chauffeur { get; set; }
        public string Produit { get; set; }
        [JsonConverter(typeof(DateConverter))]
        public DateTime Livraison { get; set; }
        public Commande(int client, double prix, Vehicule vehicule, int chauffeur,string produit, DateTime livraison)
        {
            Num_Client = client;
            Prix = prix;
            Vehicule = vehicule;
            Num_Chauffeur = chauffeur;
            Produit = produit;
            Livraison = livraison;
        }
        public Commande ()
        {
            
        }
        public override string ToString()
        {
            return $"Client N°{Num_Client}, produit : {Produit} pour {Prix} EUROS " +
                $" \n\tlivré par l'employé N°{Num_Chauffeur} avec {Vehicule.ShortString()} le " +
                $"{Livraison.ToShortDateString()}";
        }
    }
}
