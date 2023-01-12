using TransConnect.Arbre;
using TransConnect.Personne;
using TransConnect.Personne.Client;
using TransConnect.Véhicule;

namespace TransConnect
{
    internal class UI
    {
        DataBase db { get; }
        public UI(string salaries, string clients, string flotte, string graph)
        {
            db = new DataBase(salaries, clients, flotte, graph);
            Welcome();
            db.SaveData(salaries, clients, flotte, graph);
        }
        public void NewPage()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\r\n  ______                      ______                            __ \r" +
                            "\n /_  __/________ _____  _____/ ____/___  ____  ____  ___  _____/ /_\r" +
                            "\n  / / / ___/ __ `/ __ \\/ ___/ /   / __ \\/ __ \\/ __ \\/ _ \\/ ___/ __/\r" +
                            "\n / / / /  / /_/ / / / (__  ) /___/ /_/ / / / / / / /  __/ /__/ /_  \r" +
                            "\n/_/ /_/   \\__,_/_/ /_/____/\\____/\\____/_/ /_/_/ /_/\\___/\\___/\\__/  \r" +
                            "\n                                                                   \r\n");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public void Welcome()
        {
            NewPage();
            Console.WriteLine("Bienvenue, veuillez vous identifier\n");
            Console.Write("Mail : ");
            string? mail = Console.ReadLine();
            Console.Write("Mot de passe : ");
            string? password = Console.ReadLine();
            Connexion(mail, password);
        }
        public void Connexion(string mail, string mdp)
        {
            Noeud connexion;
            Client? client_connexion = null;
            do
            {
                connexion = db.Employes.Connect(db.Employes.Racine, mail, mdp);
                if (connexion != null)
                {
                    InterfaceSalarie(connexion);
                }
                else
                {
                    client_connexion = db.Clients.Find(x => x.Mail == mail && x.MotDePasse == mdp);
                    if (client_connexion != null)
                    {
                        InterfaceClient(client_connexion);
                    }
                    else
                    {
                        Console.WriteLine("Mail ou mot de passe incorrect, réssayez :");
                        Console.Write("Mail : ");
                        mail = Console.ReadLine();
                        Console.Write("Mot de passe : ");
                        mdp = Console.ReadLine();
                    }
                }

            } while (connexion == null && client_connexion == null);
        }
        public int Date (int limite1,int limite2 )
        {
            int retour;
            bool ok = int.TryParse(Console.ReadLine(), out retour);
            while(!ok || retour<limite1+1 || retour>limite2-1)
            {
                Console.Write("entrée invalide réessayez :");
                ok = int.TryParse(Console.ReadLine(), out retour);
            }
            return retour;
        }
        public void InterfaceSalarie(Noeud T)
        {
            string nb,choix;
            do
            {
                NewPage();
                Console.WriteLine($"Bienvenue {T.Salarie.Prenom} {T.Salarie.Nom}\n" +
                $"Que voulez vous faire :\n" +
                    "0 - Se déconnecter\n" +
                    "1 - L'organigramme de l'entreprise\n" +
                    "2 - Consulter mon organigramme\n" +
                    "3 - Voir mes informations\n" +
                    "4 - Modifier mes informations"
                    );
                if (T.Salarie is Chauffeur chauffeur)
                {
                    Console.WriteLine("5 - Afficher mes commandes");
                }
                if(T.Salarie.Poste == "Directeur Financier")
                {
                    Console.WriteLine("5 - Afficher les statistiques");
                }
                if(T.Salarie.Poste == "Directrice Commerciale")
                {
                        Console.WriteLine("5 - Afficher tous les clients\n" +
                        "6 - Créer un client\n" +
                        "7 - Supprimer un client\n");
                }
                if(T.Salarie.Poste == "Directrice des RH")
                {
                    Console.WriteLine("5 - créer un salarié\n" +
                        "6 - supprimer un salarié");
                }
                if (T.Salarie.Poste == "Directeur General")
                {
                    Console.WriteLine("5 - Afficher tous les clients\n"+
                        "6 - Créer un client\n" +
                        "7 - Supprimer un client\n" +
                        "8 - Afficher les statistiques\n" +
                        "9 -  Créer un salarié\n" +
                        "10 - Supprimer un salarié");
                }
                Console.Write("\nEntrez un numero :");
                nb = Console.ReadLine();
                switch (nb)
                {
                    #region Salarie
                    case "0":
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Vous êtes déconnecté");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                    case "1":
                        NewPage();
                        db.Organigramme();
                        Console.WriteLine("Pressez un bouton pour retourner en arrière");
                        Console.ReadKey();
                        break;
                    case "2":
                        NewPage();
                        Console.WriteLine("Mon organigramme :");
                        T.AffichageArborescence(T);
                        Console.WriteLine("Pressez un bouton pour retourner en arrière");
                        Console.ReadKey();
                        break;
                    case "3":
                        NewPage();
                        Console.WriteLine(T.Salarie);
                        Console.WriteLine("Pressez un bouton pour retourner en arrière");
                        Console.ReadKey();
                        break;
                    case "4":
                        do
                        {
                            NewPage();
                            Console.WriteLine("Quelle information voulez vous modifier ?\n" +
                                "0 - Retour\n" +
                                "1 - Nom\n" +
                                "2 - Adresse\n" +
                                "3 - Mail\n" +
                                "4 - Téléphone\n" +
                                "5 - Mot de passe");
                            choix = Console.ReadLine();
                            switch (choix)
                            {
                                case "0":
                                    break;
                                case "1":
                                    Console.WriteLine("Entrez votre nouveau nom :");
                                    T.Salarie.Nom = Console.ReadLine();
                                    break;
                                case "2":
                                    Console.WriteLine("Entrez votre nouvelle adresse :");
                                    T.Salarie.Adresse = Console.ReadLine();
                                    break;
                                case "3":
                                    Console.WriteLine("Entrez votre nouveau mail :");
                                    T.Salarie.Mail = Console.ReadLine();
                                    break;
                                case "4":
                                    Console.WriteLine("Entrez votre nouveau numéro de téléphone :");
                                    T.Salarie.Telephone = Console.ReadLine();
                                    break;
                                case "5":
                                    Console.WriteLine("Entrez votre nouveau mot de passe :");
                                    T.Salarie.MotDePasse = Console.ReadLine();
                                    break;
                                default:
                                    Console.WriteLine("Choix incorrect");
                                    break;
                            }
                        } while (choix != "0");
                        Console.WriteLine("Pressez un bouton pour retourner en arrière");
                        Console.ReadKey();
                        break;
                    #endregion Salarie
                    #region Chauffeur
                    case "5" when T.Salarie is Chauffeur chauffeur1:
                        NewPage();
                        Console.WriteLine("Mes commandes :");
                        foreach (Commande commande in chauffeur1.Commandes)
                        {
                            Console.WriteLine(commande);
                        }
                        Console.WriteLine("1 - Voir mon agenda");
                        Console.WriteLine("2 - Voir les vehicules que je vais conduire");
                        Console.WriteLine("3 - Voir mes itinéraires");
                        choix = Console.ReadLine();
                        switch (choix)
                        {
                            case "1":
                                NewPage();
                                Console.WriteLine("Mes prochaines commandes sont le :");
                                foreach (Commande commande in chauffeur1.Commandes)
                                {
                                    if(commande.Livraison>DateTime.Now)
                                    {
                                        Console.WriteLine(commande.Livraison);
                                    }
                                }
                                Console.WriteLine("Pressez un bouton pour retourner en arrière");
                                Console.ReadKey();
                                break;
                            case "2":
                                NewPage();
                                Console.WriteLine("Les véhicules que je vais conduire sont :");
                                foreach (Commande commande in chauffeur1.Commandes)
                                {
                                    Console.WriteLine(commande.Vehicule);
                                }
                                Console.WriteLine("Pressez un bouton pour retourner en arrière");
                                Console.ReadKey();
                                break;
                            case "3":
                                NewPage();
                                Console.WriteLine("Mes itinéraires sont :");
                                foreach (Commande commande in chauffeur1.Commandes)
                                {
                                    Console.WriteLine($"Itinéraire pour la commande du {commande.Livraison.ToShortDateString()}");
                                    Console.WriteLine(db.Clients.Find(x => x.Numero== commande.Num_Client).Ville.Trajet+"\n");
                                }
                                Console.WriteLine("Pressez un bouton pour retourner en arrière");
                                Console.ReadKey();
                                break;
                        }
                        break;
                    #endregion Chauffeur
                    #region Directeur
                    #region liste clients
                    case "5" when (T.Salarie.Poste == "Directeur General" || T.Salarie.Poste == "Directrice Commerciale"):
                        NewPage();
                        Console.WriteLine("Voici la liste des clients:");
                        foreach (Client client in db.Clients)
                        {
                            Console.WriteLine(client.ShortString());
                        }
                        string tri;
                        do
                        {
                            Console.WriteLine("\nSouhaitez vous les trier ?\n" +
                                "0 - retour\n" +
                                "1 - par ordre alphabetique\n" +
                                "2 - par Ville\n" +
                                "3 - par dépense\n");
                            tri = Console.ReadLine();
                            switch (tri)
                            {
                                case "0":
                                    break;
                                case "1":
                                    NewPage();
                                    Console.WriteLine("Voici la liste des clients triés par ordre alphabetique:");
                                    foreach (Client client in db.Clients.OrderBy(c => c.Nom))
                                    {
                                        Console.WriteLine(client.ShortString());
                                    }
                                    Console.WriteLine("Pressez un bouton pour retourner en arrière");
                                    Console.ReadKey();
                                    break;
                                case "2":
                                    NewPage();
                                    Console.WriteLine("Voici la liste des clients triés par Ville:");
                                    foreach (Client client in db.Clients.OrderBy(c => c.Ville.Nom))
                                    {
                                        Console.WriteLine(client.ShortString());
                                    }
                                    Console.WriteLine("Pressez un bouton pour retourner en arrière");
                                    Console.ReadKey();
                                    break;
                                case "3":
                                    NewPage();
                                    Console.WriteLine("Voici la liste des clients triés par dépense:");
                                    foreach (Client client in db.Clients.OrderBy(c => c.CoutTotal))
                                    {
                                        Console.WriteLine(client.ShortString() + ", dépense total :" + client.CoutTotal+" Euros");
                                    }
                                    Console.WriteLine("Pressez un bouton pour retourner en arrière");
                                    Console.ReadKey();
                                    break;
                            }
                        } while (tri != "0");
                        break;
                    #endregion liste clients
                    #region Creation Client
                    case "6" when (T.Salarie.Poste == "Directeur General") || (T.Salarie.Poste == "Directrice Commerciale"):
                        NewPage();
                        Console.WriteLine("Liste des clients :");
                        foreach(Client client in db.Clients)
                        {
                            Console.WriteLine(client.ShortString());
                        }
                        Console.Write("\ncréation d'un Client :\n" +
                            "entrez le Numéro du client:");
                        int.TryParse(Console.ReadLine(), out int cl_numero);
                        while (db.Clients.Find(x => x.Numero == cl_numero) != null || cl_numero!= 0)
                        {
                            Console.WriteLine("Numéro déjà utilisé ou saisie incorrecte, veuillez en choisir un autre :");
                            int.TryParse(Console.ReadLine(), out cl_numero);
                        }
                        NewPage();
                        Console.WriteLine("Nom :");
                        string cl_nom = Console.ReadLine();
                        Console.WriteLine("Prénom :");
                        string cl_prenom = Console.ReadLine();
                        NewPage();
                        Console.Write("Date de naissance :\n" +
                            "\tjour :");
                        int cl_jour = Date(0, 30);
                        Console.Write("\tmois :");
                        int cl_mois = Date(0,12);
                        Console.Write("\tannée :");
                        int cl_annee = Date(1900, DateTime.Now.Year);
                        DateTime cl_naissance = new DateTime(cl_annee, cl_mois, cl_jour);
                        NewPage();
                        Console.Write("Adresse :");
                        string cl_adresse = Console.ReadLine();
                        string cl_mail = $"{cl_prenom.ToLowerInvariant()}.{cl_nom.ToLowerInvariant()}@ext.transconnect.com";
                        Console.Write("Téléphone :");
                        string cl_telephone = Console.ReadLine();
                        string motdepasse = "1234";
                        Console.WriteLine("Ville :");
                        string ville = Console.ReadLine();
                        db.CreationClient(cl_numero, cl_nom, cl_prenom, cl_naissance, cl_adresse, cl_mail, cl_telephone, "1234", ville);
                        NewPage();
                        Console.WriteLine($"Salarié créé :\n{db.Clients[db.Clients.Count - 1]}");
                        break;
                    #endregion Creation Client
                    #region Supprimer client
                    case "7" when (T.Salarie.Poste == "Directeur General") || (T.Salarie.Poste == "Directrice Commerciale"):
                        foreach (Client client in db.Clients)
                        {
                            Console.WriteLine(client.ShortString());
                        }
                        Console.WriteLine("quel est le numéro de l'employé que vous voulez supprimer de l'organigramme");
                        int Num_client;
                        while (!int.TryParse(Console.ReadLine(), out Num_client) || db.Clients.Find(x=>x.Numero==Num_client)== null) { Console.Write("entrée invalide, réssayez : "); }
                        if(db.SupprimerClient(Num_client))
                        {
                            Console.WriteLine($"Le client N°{Num_client} a bien été supprimé");
                        }
                        else
                        {
                            Console.WriteLine("le client n'a pas été supprimé");
                        }
                        Console.WriteLine("Pressez un bouton pour retourner en arrière");
                        Console.ReadKey();
                        break;
                    #endregion Supprimer client
                    #region Statistiques
                    case "5" when T.Salarie.Poste == "Directeur Financier":
                    case "8" when (T.Salarie.Poste == "Directeur General") || (T.Salarie.Poste == "Directrice Commerciale"):
                        do
                        {
                            NewPage();
                            Console.Write("Statistiques :\n" +
                                "0 - Retour\n" +
                                "1 - nombre de livraison effectués par chauffeur\n" +
                                "2 - Commandes selon une période\n" +
                                "3 - Prix moyen des commandes\n" +
                                "4 - Prix moyen des commandes par client\n" +
                                "5 - lister les commandes d'un client\n" +
                                "Entrez le numéro de la statistique que vous souhaitez voir :");
                            choix = Console.ReadLine();
                            switch (choix)
                            {
                                case "0":
                                    break;
                                case "1":
                                    NewPage();
                                    Console.WriteLine("Nombre de livraisons effectués par chauffeur :");
                                    foreach (Chauffeur conducteur in db.Chauffeurs)
                                    {
                                        Console.WriteLine($"{conducteur.Nom} {conducteur.Prenom} : {conducteur.CommandesEffectues}");
                                    }
                                    Console.WriteLine("Pressez un bouton pour retourner en arrière");
                                    Console.ReadKey();
                                    break;
                                case "2":
                                    NewPage();
                                    Console.Write("Commandes selon une période :\n" +
                                        "Entrez la date de début :\n" +
                                        "\tjour :");
                                    int d1jour = Date(0, 31);
                                    Console.Write("\tmois :");
                                    int d1mois = Date(0, 12);
                                    Console.Write("\tannée :");
                                    int d1annee = Date(1900, 2030);
                                    DateTime debut = new DateTime(d1annee, d1mois, d1jour);
                                    Console.Write("Entrez la date de fin :\n" +
                                        "\tjour :");
                                    int d2jour = Date(0, 31);
                                    Console.Write("\tmois :");
                                    int d2mois = Date(0, 12);
                                    Console.Write("\tannée :");
                                    int d2annee = Date(1900, 2030);
                                    DateTime fin = new DateTime(d1annee, d1mois, d1jour);
                                    NewPage();
                                    Console.WriteLine("Commandes effectuées entre le {0} et le {1} :", debut.ToShortDateString(), fin.ToShortDateString());
                                    foreach (Commande commande in db.GetCommandes().Where(c => c.Livraison >= debut && c.Livraison <= fin))
                                    {
                                        Console.WriteLine(commande);
                                    }
                                    Console.WriteLine("Pressez un bouton pour retourner en arrière");
                                    Console.ReadKey();
                                    break;
                                case "3":
                                    NewPage();
                                    Console.WriteLine("Prix moyen des commandes :");
                                    Console.WriteLine(db.GetCommandes().Average(c => c.Prix));
                                    Console.WriteLine("Pressez un bouton pour retourner en arrière");
                                    Console.ReadKey();
                                    break;
                                case "4":
                                    NewPage();
                                    Console.WriteLine("Prix moyen des commandes par client :");
                                    foreach (Client client in db.Clients)
                                    {
                                        Console.WriteLine($"{client.Nom} {client.Prenom} : {db.GetCommandes().Where(c => c.Num_Client == client.Numero).Average(c => c.Prix)}");
                                    }
                                    Console.WriteLine("Pressez un bouton pour retourner en arrière");
                                    Console.ReadKey();
                                    break;
                                case "5":
                                    NewPage();
                                    Console.WriteLine("Lister les commandes d'un client :\n" +
                                        "Entrez le numéro du client :");
                                    int numclient;
                                    while (!int.TryParse(Console.ReadLine(), out numclient) || db.Clients.Find(x=>x.Numero == numclient)!= null) { Console.Write("entrée invalide, réssayez : "); }
                                    NewPage();
                                    Console.WriteLine("Commandes du client N°{0} :", numclient);
                                    foreach (Commande commande in db.GetCommandes().Where(c => c.Num_Client == numclient))
                                    {
                                        Console.WriteLine(commande);
                                    }
                                    Console.WriteLine("Pressez un bouton pour retourner en arrière");
                                    Console.ReadKey();
                                    break;
                            }
                        } while (choix != "0");

                        break;
                    #endregion Statistiques
                    #region Creation Salarie
                    case "5" when T.Salarie.Poste == "Directrice des RH":
                    case "9" when (T.Salarie.Poste == "Directeur General"):
                        NewPage();
                        db.Organigramme();
                        Console.Write("création d'un salarié :\n" +
                            "entrez le numero du responsable :");
                        int.TryParse(Console.ReadLine(), out int respo);
                        while (!db.Employes.Contains(respo))
                        {
                            Console.WriteLine("Employé non présent, veuillez en choisir un autre :");
                            int.TryParse(Console.ReadLine(), out respo);
                        }
                        Console.Write("Numéro :");
                        int.TryParse(Console.ReadLine(), out int numero);
                        while (db.Employes.Contains(numero))
                        {
                            Console.WriteLine("Numéro déjà utilisé, veuillez en choisir un autre :");
                            int.TryParse(Console.ReadLine(), out numero);
                        }
                        NewPage();
                        Console.WriteLine("Nom :");
                        string nom = Console.ReadLine();
                        Console.WriteLine("Prénom :");
                        string prenom = Console.ReadLine();
                        NewPage();
                        Console.Write("Date de naissance :\n" +
                            "\tjour :");
                        int jour = Date(0, 31);
                        Console.Write("\tmois :");
                        int mois = Date(0, 12);
                        Console.Write("\tannée :");
                        int annee = Date(1900, DateTime.Now.Year);
                        DateTime naissance = new DateTime(annee, mois, jour);
                        NewPage();
                        Console.Write("Adresse :");
                        string adresse = Console.ReadLine();
                        string mail = $"{prenom.ToLowerInvariant()}.{nom.ToLowerInvariant()}@transconnect.com";
                        Console.Write("Téléphone :");
                        string telephone = Console.ReadLine();
                        Console.Write("poste :");
                        string poste = Console.ReadLine();
                        Console.Write("salaire :");
                        int salaire = Date(0, 100000);
                        Salarie nouveau = new(numero, nom, prenom, naissance, adresse, mail, telephone, "1234", DateTime.Now, poste, salaire);
                        db.CreationSalarie(nouveau, respo);
                        NewPage();
                        Console.WriteLine($"Salarié créé :\n{nouveau}");
                        db.Organigramme();
                        Console.WriteLine("Pressez un bouton pour retourner en arrière");
                        Console.ReadKey();
                        break;
                    #endregion Creation Salarie
                    #region Suppression Salarie
                    case "6" when (T.Salarie.Poste == "Directrice des RH"):
                    case "10" when (T.Salarie.Poste == "Directeur General"):
                        db.Organigramme();
                        Console.Write("Quelle est le numéro de l'employé que vous souhaitez supprimer :");
                        int numero_Salarie;
                        while (!int.TryParse(Console.ReadLine(), out numero_Salarie) || db.Clients.Find(x => x.Numero == numero_Salarie) == null) { Console.Write("entrée invalide, réssayez : "); }
                        db.SuppressionSalarie(numero_Salarie);
                        Console.WriteLine($"L'employé N° {numero_Salarie} a été supprimé");
                        db.Organigramme();
                        Console.WriteLine("Pressez un bouton pour retourner en arrière");
                        Console.ReadKey();
                        break;
                    #endregion suppression
                    #endregion directeur
                    default:
                        NewPage();
                        Console.WriteLine("Choix invalide");
                        break;
                }
            } while (nb != "0");
        }
        public void InterfaceClient(Client T)
        {
            string choix;
            do
            {
                NewPage();
                Console.WriteLine($"Bienvenue {T.Prenom} {T.Nom}");
                if(T.Commandes != null)
                {
                    Console.Write("Voici vos commandes: \n");
                    foreach (Commande commande in T.Commandes)
                    {
                        Console.WriteLine(commande);
                    }
                }
                Console.WriteLine($"Que voulez vous faire :\n" +
                    "0 - Se déconnecter\n" +
                    "1 - faire une nouvelle commande");
                if(T.Commandes != null)
                {
                    Console.WriteLine("2 - Modifier une commande");
                }
                choix = Console.ReadLine();
                switch(choix)
                {
                    case "0":
                        break;
                    #region Créer Commande
                    case "1":
                        NewPage();
                        Console.Write("Nouvelle commande :\n" +
                                "Entrez la date de livraison :\n");
                        DateTime livraison = DateTime.MinValue;
                        while(livraison<DateTime.Now)
                        {
                            Console.Write("\tjour :");
                            int jour = Date(0, 31);
                            Console.Write("\tmois :");
                            int mois = Date(0, 12);
                            Console.Write("\tannée :");
                            int annee = Date(1900, 2030); 
                            Console.Write("\tmois :");
                            livraison = new DateTime(annee, mois, jour);
                            Console.Write("la date est passée, veuillez choisir une date dans le futur");
                        }
                        NewPage();
                        Console.WriteLine("Quelle produit allez vous transporter :");
                        string produit = Console.ReadLine();
                        NewPage();
                        Console.WriteLine("Voici les vehicules dont nous disposons à cette date :\n");
                        List<Vehicule> vehicules = db.GetFlotte(livraison);
                        for (int i = 0; i < vehicules.Count; i++)
                        {
                            Console.WriteLine($"{i} - {vehicules[i]}");
                        }
                        Console.WriteLine("Lequel voulez vous prendre ?");
                        int index = Date(0, vehicules.Count);
                        Commande nouvelle = db.NewCommande(T, vehicules[index],livraison,produit);
                        NewPage();
                        Console.WriteLine(nouvelle);
                        Console.WriteLine("Pressez un bouton pour retourner en arrière");
                        Console.ReadKey();
                        break;
                    #endregion Créer Commande
                    #region Modif Commande
                    case "2" when T.Commandes != null:
                        NewPage();
                        Console.WriteLine("Modification d'une commande");
                        Console.WriteLine("Quelle commande voulez vous modifier ?");
                        for (int i = 0; i < T.Commandes.Count; i++)
                        {
                            Console.WriteLine(($"{i} - {T.Commandes[i]}"));
                        }
                        index = Date(0, T.Commandes.Count);
                        NewPage();
                        string modifcommande;
                        do
                        {
                            Console.WriteLine("Que souhaitez vous modifier ?" +
                                "0 - retour\n" +
                                "1 - le produit\n" +
                                "2 - la date de livraison\n" +
                                "3 - le véhicule\n");
                            modifcommande = Console.ReadLine();
                            switch (modifcommande)
                            {
                                case "0":
                                    break;
                                #region Produit
                                case "1":
                                    NewPage();
                                    Console.WriteLine("Nouveau produit :");
                                    string nouveau_produit = Console.ReadLine();
                                    db.ModificationCommande(T.Commandes[index], T.Commandes[index].Vehicule, T.Commandes[index].Livraison, nouveau_produit);
                                    NewPage();
                                    Console.WriteLine($"Commande modifiée :\n{T.Commandes[index]}");
                                    Console.WriteLine("Pressez un bouton pour retourner en arrière");
                                    Console.ReadKey();
                                    break;
                                #endregion Produit
                                #region Date
                                case "2":
                                    NewPage();
                                    Console.WriteLine("Entrez la nouvelle date de livraison :\n" +
                                        "\tjour :");
                                    int jour = Date(0, 31);
                                    Console.Write("\tmois :");
                                    int mois = Date(0, 12);
                                    Console.Write("\tannée :");
                                    int annee = Date(1900, 2030);
                                    livraison = new DateTime(annee, mois, jour);
                                    db.ModificationCommande(T.Commandes[index], T.Commandes[index].Vehicule, livraison, T.Commandes[index].Produit);
                                    NewPage();
                                    Console.WriteLine($"Commande modifiée :\n{T.Commandes[index]}");
                                    Console.WriteLine("Pressez un bouton pour retourner en arrière");
                                    Console.ReadKey();
                                    break;
                                #endregion Date
                                #region Vehicule
                                case "3":
                                    NewPage();
                                    Console.WriteLine("Voici les vehicules dont nous disposons à cette date :\n" +
                                        "0 - Annuler\n");
                                    vehicules = db.GetFlotte(T.Commandes[index].Livraison);
                                    for (int i = 0; i < vehicules.Count; i++)
                                    {
                                        Console.WriteLine($"{i} - {vehicules[i]}");
                                    }
                                    Console.WriteLine("Lequel voulez vous prendre ?");
                                    int indexvehicule = Date(0, vehicules.Count);
                                    if (indexvehicule != 0)
                                    {
                                        db.ModificationCommande(T.Commandes[index], vehicules[indexvehicule], T.Commandes[index].Livraison, T.Commandes[index].Produit);
                                        NewPage();
                                        Console.WriteLine($"Commande modifiée :\n{T.Commandes[index]}");
                                        Console.WriteLine("Pressez un bouton pour retourner en arrière");
                                        Console.ReadKey();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Commandes non modifié");
                                    }
                                    break;
                                    #endregion Vehicule

                            }
                        } while (modifcommande != "0"); 
                        break;
                        #endregion Modif Commande
                }

            } while (choix != "0");
        }
    }
}
