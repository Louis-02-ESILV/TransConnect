using TransConnect;
using TransConnect.Personne;
using TransConnect.Véhicule;
using TransConnect.Map;

namespace TransConnect
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(90, 30);
            UI data = new UI("./Data/Db_Employes.json", "./Data/Db_Clients.json", "./Data/Db_Flotte.json", "./Data/Distances.csv");
        }
            

    }
}