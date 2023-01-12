using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TransConnect.Personne
{
    internal class Salarie : Personne, IEquatable<Salarie>
    {
        [JsonConverter(typeof(DateConverter))]
        public DateTime Embauche { get; }
        public string Poste { get; set; }
        public int Salaire { get; set; }
        public Salarie(int numero, string nom, string prenom, DateTime naissance, string adresse, string mail, string telephone, string motdepasse, DateTime embauche, string poste, int salaire) : base(numero, nom, prenom, naissance, adresse, mail, telephone, motdepasse)
        {
            Embauche = embauche;
            Poste = poste;
            Salaire = salaire;
        }
        public override string ToString()
        {
            return base.ToString() + $"Embauche :{Embauche.ToShortDateString()}\n" +
                $"Poste:{Poste}\n" +
                $"Salaire :{Salaire} EURO\n";
        }
        public virtual string Organigramme()
        {
            return $"{Numero} {Nom}";
        }
        public bool Equals(Salarie? other)
        {
            if (other is null)
            {
                return false;
            }
            else
            {
                return Numero == other.Numero;
            }
        }
        
    }
}
