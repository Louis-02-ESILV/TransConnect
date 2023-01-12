using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect.Personne.Client
{
    /// <summary>
    /// Compare by the name of the client
    /// </summary>
    internal class SortByNom: IComparer<Client>
    {
        public int Compare(Client? x, Client? y)
        {
            return x.Nom.CompareTo(y.Nom);
        }
        public static IComparer<Client> sortByNom()
        {
            return new SortByNom();
        }
    }
    /// <summary>
    /// Compare by the city of the client
    /// </summary>
    internal class SortByVille:IComparer<Client>
    {
        public int Compare(Client? x, Client? y)
        {
            if(x != null && y != null)
            {
                return x.Adresse.CompareTo(y.Adresse);
            }
            else
            {
                return 0;
            }
        }
        public static IComparer<Client> sortByVille()
        {
            return new SortByVille();
        }
    }
    /// <summary>
    /// Compare by the total cost of the client
    /// </summary>
    internal class SortByCout : IComparer<Client>
    {
        public int Compare(Client? x, Client? y)
        {
            if (x != null && y != null)
            {
                return x.CoutTotal.CompareTo(y.CoutTotal);
            }
            else
            {
                return 0;
            }
        }
        public static IComparer<Client> sortByCout()
        {
            return new SortByCout();
        }
    }
}
