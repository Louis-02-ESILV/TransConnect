using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TransConnect.Personne
{
    internal abstract class Personne
    {
        public int Numero { get; }
        public string Nom { get; set; }
        public string Prenom { get; }
        [JsonConverter(typeof(DateConverter))]
        public DateTime Naissance { get; }
        public string Adresse { get; set; }
        public string Mail { get; set; }
        public string Telephone { get; set; }
        public string MotDePasse { get; set; }
        public Personne(int numero, string nom, string prenom, DateTime naissance, string adresse, string mail, string telephone,string motdepasse)
        {
            Numero = numero;
            Nom = nom;
            Prenom = prenom;
            Naissance = naissance;
            Adresse = adresse;
            Mail = mail;
            Telephone = telephone;
            MotDePasse = motdepasse;
        }
       public override string ToString()
       {
            return $"Numero : {Numero}\n" +
                $"Nom :{Nom}\n" +
                $"Prenom :{Prenom}\n" +
                $"Naissance :{Naissance.ToShortDateString()}\n" +
                $"Adresse:{Adresse}\n" +
                $"Mail :{Mail}\n" +
                $"Telephone :{Telephone}\n";
       }
    }
}
