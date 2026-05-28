namespace BlackJack
{
    using System; // Bibliothèque de base pour les entrées/sorties (Console)
    using System.Threading; // Bibliothèque nécessaire pour utiliser Thread.Sleep (pauses)

    public class Program
    {
        public static void Main()
        {
            int mise = 0;
            bool rejouer = true; // Initialisé à true pour que la boucle puisse s'exécuter
            List<string> packet;
            List<string> handPlayer = new List<string>();
            List<string> handCroupier = new List<string>();
            int scorePlayer = 0;
            int scoreCroupier = 0;

            // --- CONFIGURATION INITIALE ---
            Console.Title = "BlackJack - Volcke Clement";
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Clear();

            // --- AFFICHAGE DU LOGO "BIENVENUE" ---
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n      ★  BIENVENUE AU CASINO  ★");

            // --- AFFICHAGE DU LOGO "BLACKJACK" EN BLOCS ---
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(@"
         ██████╗ ██╗      █████╗  ██████╗██╗  ██╗     ██╗ █████╗  ██████╗██╗  ██╗
         ██╔══██╗██║     ██╔══██╗██╔════╝██║ ██╔╝     ██║██╔══██╗██╔════╝██║ ██╔╝
         ██████╔╝██║     ███████║██║     █████╔╝      ██║███████║██║     █████╔╝ 
         ██╔══██╗██║     ██╔══██║██║     ██╔═██╗ ██   ██║██╔══██║██║     ██╔═██╗ 
         ██████╔╝███████╗██║  ██║╚██████╗██║  ██╗╚██████╔╝██║  ██║╚██████╗██║  ██╗
         ╚═════╝ ╚══════╝╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝ ╚═════╝ ╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝");

            Console.ResetColor();
            Console.WriteLine("\n" + new string('═', 80));

            // --- PHASE DE SAISIE ---
            Console.Write("\n > Veuillez entrer votre nom de joueur : ");
            string nameUser = Console.ReadLine();
            Console.ResetColor();
            if (string.IsNullOrEmpty(nameUser)) nameUser = "Joueur";
            int monnaiePoche = 1000;

            string choixRegles = "";
            do
            {
                Console.Write($"\n {nameUser}, souhaitez-vous consulter les règles du jeu avant de commencer ? (O/N) : ");
                choixRegles = Console.ReadLine().ToUpper().Trim();

                if (choixRegles != "O" && choixRegles != "N")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" [!] Entrée invalide. Veuillez répondre strictement par 'O' (Oui) ou 'N' (Non).");
                    Console.ResetColor();
                }
            } while (choixRegles != "O" && choixRegles != "N");

            // Si le joueur veut voir les règles, on appelle la méthode
            if (choixRegles == "O")
            {
                ListExtensions.AfficherRegles();
            }

            do
            {
                // Réinitialisation pour la nouvelle manche
                scorePlayer = 0;
                scoreCroupier = 0;

                // --- TRANSITION VERS LE TAPIS DE JEU ---
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n BIENVENUE TABLE #8 : {nameUser.ToUpper()}");
                Console.WriteLine($" CRÉDITS ACTUELS    : {monnaiePoche} €");
                Console.ResetColor();
                Console.WriteLine(new string('─', 40));

                // --- BOUCLE DE MISE ---
                do
                {
                    Console.Write($"\n Combien souhaitez-vous miser ? (1 - {monnaiePoche} €) : ");
                    if (int.TryParse(Console.ReadLine(), out mise) && mise > 0 && mise <= monnaiePoche)
                    {
                        break;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" [!] Mise invalide. Veuillez entrer un montant correct.");
                    Console.ResetColor();
                } while (true);

                // --- PREPARATION DU JEU ---
                Console.Write("\n Le croupier prépare le paquet ");
                PacketCarte.CreationPacketCarte(out packet);
                PacketCarte.MelangePacketCarte(packet);
                PacketCarte.DistributionCartes(out handPlayer, out handCroupier, ref packet);

                for (int i = 0; i < 3; i++)
                {
                    Thread.Sleep(600); // Temps ajusté pour la fluidité (0.6s)
                    Console.Write(".");
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\t\t╔════════════════════════════════════════╗");
                Console.WriteLine("\t\t║        [!] LA PARTIE DÉBUTE [!]        ║");
                Console.WriteLine("\t\t╚════════════════════════════════════════╝");
                Console.ResetColor();
                Thread.Sleep(1000);

                // --- TOUR DU JOUEUR ---
                TourJoueur(ref handPlayer, ref packet, ref scorePlayer, handCroupier);

                // --- TOUR DU CROUPIER ---
                if (scorePlayer <= 21)
                {
                    TourCroupier(ref handCroupier, ref packet, ref scoreCroupier, scorePlayer);
                }

                // --- DÉTERMINATION DU GAGNANT ---
                Console.WriteLine("\n" + new string('═', 50));
                if (scorePlayer > 21)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($" BUST ! Vous avez dépassé 21. Vous perdez votre mise de {mise} €.");
                    monnaiePoche -= mise;
                }
                else if (scoreCroupier > 21)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($" LE CROUPIER CRASH ({scoreCroupier}) ! Vous gagnez {mise} € !");
                    monnaiePoche += mise;
                }
                else if (scorePlayer > scoreCroupier)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($" VICTOIRE ! Votre score ({scorePlayer}) bat le Croupier ({scoreCroupier}). Vous gagnez {mise} € !");
                    monnaiePoche += mise;
                }
                else if (scorePlayer < scoreCroupier)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($" DÉFAITE ! Le Croupier ({scoreCroupier}) bat votre score ({scorePlayer}). Vous perdez {mise} €.");
                    monnaiePoche -= mise;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($" ÉGALITÉ ({scorePlayer} partout) ! Vous récupérez votre mise.");
                }
                Console.ResetColor();
                Console.WriteLine(new string('═', 50));

                // Vérification de banqueroute
                if (monnaiePoche <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\n [!] Plus de crédits... Vous êtes ruiné. Le casino ferme ses portes !");
                    Console.ResetColor();
                    rejouer = false;
                }
                else
                {
                    string reponse = "";
                    do
                    {
                        Console.Write("\n Voulez-vous faire une autre donne ? (O/N) : ");
                        reponse = Console.ReadLine().ToUpper().Trim();

                        if (reponse != "O" && reponse != "N")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(" [!] Entrée invalide. Veuillez répondre strictement par 'O' (Oui) ou 'N' (Non).");
                            Console.ResetColor();
                        }
                    } while (reponse != "O" && reponse != "N");

                    if (reponse == "N")
                    {
                        rejouer = false;
                        Console.WriteLine($"\n Merci d'avoir joué ! Vous repartez avec un total de {monnaiePoche} € !");
                    }
                }

            } while (rejouer == true);
        }


        public static void TourJoueur(ref List<string> HandPlayer, ref List<string> packet, ref int ScorePlayer, List<string> HandCroupier)
        {
            bool joueurCouche = false;
            string choixPlayer = "";

            while (ScorePlayer < 21 && choixPlayer != "S" && !joueurCouche)
            {
                ScorePlayer = ListExtensions.CalculScore(HandPlayer);

                Console.Clear();
                Console.WriteLine("\n--- CARTES DU CROUPIER (Une carte cachée) ---");
                PacketCarte.AfficherCartesGraphiques(HandCroupier, true); // Masque la 2ème carte du Croupier

                Console.WriteLine("\n--- VOS CARTES ACTUELLES ---");
                PacketCarte.AfficherCartesGraphiques(HandPlayer, false);
                Console.WriteLine($" Votre score actuel : {ScorePlayer}");
                Console.WriteLine(new string('-', 45));

                if (ScorePlayer >= 21) break;

                Console.Write(" Voulez-vous piocher (H) ou rester (S) ? : ");
                choixPlayer = Console.ReadLine().ToUpper();

                if (choixPlayer == "H")
                {
                    HandPlayer.Add(packet.Pop());
                }
                else if (choixPlayer == "S")
                {
                    joueurCouche = true;
                    Console.WriteLine(" Vous décidez de vous arrêter.");
                    Thread.Sleep(1000);
                }
                else
                {
                    Console.WriteLine(" Choix invalide. Veuillez taper H ou S.");
                    Thread.Sleep(1000);
                }
            }
            ScorePlayer = ListExtensions.CalculScore(HandPlayer);
        }

        public static void TourCroupier(ref List<string> HandCroupier, ref List<string> packet, ref int ScoreCroupier, int ScorePlayer)
        {
            if (ScorePlayer > 21) return;

            ScoreCroupier = ListExtensions.CalculScore(HandCroupier);

            Console.Clear();
            Console.WriteLine("\n--- LE CROUPIER RÉVÈLE SA MAIN ---");
            PacketCarte.AfficherCartesGraphiques(HandCroupier, false);
            Console.WriteLine($" La deuxième carte du croupier était : {HandCroupier[1]}");
            Console.WriteLine($" Score initial du croupier : {ScoreCroupier}");
            Thread.Sleep(1500);

            // Le croupier pioche tant qu'il est en dessous de 17
            while (ScoreCroupier < 17)
            {
                Console.Write(" Le croupier réfléchit et pioche ");
                for (int i = 0; i < 3; i++)
                {
                    Thread.Sleep(500);
                    Console.Write(".");
                }
                Console.WriteLine();

                HandCroupier.Add(packet.Pop());
                ScoreCroupier = ListExtensions.CalculScore(HandCroupier);

                Console.Clear();
                Console.WriteLine("\n--- TOUR DU CROUPIER ---");
                PacketCarte.AfficherCartesGraphiques(HandCroupier, false);
                Console.WriteLine($" Le croupier pioche. Nouveau score : {ScoreCroupier}");
                Thread.Sleep(1500);
            }
        }
    }
}

      