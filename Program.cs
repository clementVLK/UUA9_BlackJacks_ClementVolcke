namespace BlackJack
{
    using System; // Bibliothèque de base pour les entrées/sorties (Console)
    using System.Threading; // Bibliothèque nécessaire pour utiliser Thread.Sleep (pauses)

    public class Program
    {
        public static void Main()
        {
            bool pauvre = false;
            string rejouer; // Variable pour contrôler la boucle de jeu (initialisée à "O" pour entrer dans la boucle)
            List<string> packet; 
            List<string> HandPlayer = new List<string>();
            int scorePlayer = 0;
            int scoreCroupier = 0;
            // --- CONFIGURATION INITIALE ---
            Console.Title = "BlackJack - Volcke Clement"; // Définit le nom de la fenêtre Windows
            Console.OutputEncoding = System.Text.Encoding.UTF8; // Permet d'afficher les caractères spéciaux (cœur, carreau, etc.)
            Console.Clear(); // Efface l'écran pour partir sur une console propre

            // --- AFFICHAGE DU LOGO "BIENVENUE" ---
            Console.ForegroundColor = ConsoleColor.Cyan; // Change la couleur du texte en Cyan
            Console.WriteLine("\n      ★  BIENVENUE AU CASINO  ★"); // Message de bienvenue stylisé

            // --- AFFICHAGE DU LOGO "BLACKJACK" EN BLOCS ---
            // Utilisation du caractère @ devant la chaîne pour permettre le texte multi-lignes (Verbatim)
            Console.ForegroundColor = ConsoleColor.DarkRed; // Le rouge pour rappeler la couleur des tapis de casino
            Console.WriteLine(@"
                 ██████╗ ██╗      █████╗  ██████╗██╗  ██╗     ██╗ █████╗  ██████╗██╗  ██╗
                 ██╔══██╗██║     ██╔══██╗██╔════╝██║ ██╔╝     ██║██╔══██╗██╔════╝██║ ██╔╝
                 ██████╔╝██║     ███████║██║     █████╔╝      ██║███████║██║     █████╔╝ 
                 ██╔══██╗██║     ██╔══██║██║     ██╔═██╗ ██   ██║██╔══██║██║     ██╔═██╗ 
                 ██████╔╝███████╗██║  ██║╚██████╗██║  ██╗╚██████╔╝██║  ██║╚██████╗██║  ██╗
                 ╚═════╝ ╚══════╝╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝ ╚═════╝ ╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝");

            Console.ResetColor(); // Réinitialise la couleur par défaut (blanc sur noir)
            Console.WriteLine("\n" + new string('═', 80)); // Trace une ligne de séparation de 80 caractères '═'

            // --- PHASE DE SAISIE (PARAMÈTRES IN DU MODÈLE D'ANALYSE) ---
            // On récupère le nom de l'utilisateur (Variable NameUser de ton tableau)
            Console.Write("\n > Veuillez entrer votre nom de joueur : ");
            string nameUser = Console.ReadLine();

            // Initialisation de la monnaie (Variable MonnaiePoche de ton tableau)
            int monnaiePoche = 1000;
            do
            {

            
                // --- TRANSITION VERS LE TAPIS DE JEU ---
                Console.Clear(); // On vide l'écran pour ne laisser que la table de jeu
                Console.ForegroundColor = ConsoleColor.Yellow; // Couleur Or pour le solde
                Console.WriteLine($"\n BIENVENUE TABLE #8 : {nameUser.ToUpper()}"); // Affiche le nom en majuscules
                Console.WriteLine($" CRÉDITS ACTUELS    : {monnaiePoche} €"); // Affiche le capital de départ
                Console.ResetColor();
                Console.WriteLine(new string('─', 40)); // Ligne de séparation plus courte

                // --- ANIMATION IMMERSIVE ---
                // Simule l'action du croupier pour donner du réalisme au programme
                Console.Write("\n Le croupier prépare le paquet ");
                PacketCarte.CreationPacketCarte(out packet); // Appelle la méthode pour créer le paquet de cartes
                PacketCarte.MelangePacketCarte(packet); // Mélange le paquet pour plus de réalisme
                PacketCarte.DistributionCartes(out HandPlayer);

                for (int i = 0; i < 3; i++) // Boucle simple pour afficher trois petits points
                {
                    Thread.Sleep(1600); // Met le programme en pause pendant 600 millisecondes (1.6s)
                    Console.Write("."); // Ajoute un point à chaque tour de boucle
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\t\t╔════════════════════════════════════════╗");
                Console.WriteLine("\t\t║        [!] LA PARTIE DÉBUTE [!]        ║");
                Console.WriteLine("\t\t╚════════════════════════════════════════╝");
                Console.ResetColor();
                Thread.Sleep(1000); // Petite pause finale avant de passer à la suite

                // --- LOGIQUE SUIVANTE (DÉBUT DU NSD PRINCIPAL) ---
                // C'est ici que tu insères ta boucle de mise (tant que Mise > MonnaiePoche) [cite: 2]
                // --- TOUR DU JOUEUR ---
                TourJoueur(ref handPlayer, ref packet, ref scorePlayer);

                // --- TOUR DU CROUPIER (Uniquement si le joueur n'a pas explosé son score) ---
                if (scorePlayer <= 21)
                {
                    Console.Clear();
                    Console.WriteLine("\n--- VOS CARTES FINALES ---");
                    PacketCarte.AfficherCartesGraphiques(handPlayer, false);

                    Console.WriteLine("\n--- LE CROUPIER JOUE ---");
                    TourCroupier(ref handCroupier,ref packet, ref scoreCroupier, scorePlayer);
                }

                // --- DÉTERMINATION DU GAGNANT ET AJUSTEMENT DU SOLDE ---
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
                    pauvre = true;
                    ;
                }

                // Demande pour continuer
                Console.Write("\n Voulez-vous faire une autre donne ? (O/N) : ");
                rejouer = Console.ReadLine().ToUpper();
                if (rejouer != "O")
                {
                    Console.WriteLine($"\n Merci d'avoir joué ! Vous repartez avec un total de {monnaiePoche} € !");
                }
            }while ( rejouer != "O" ||pauvre == false);

        }


        public static void TourJoueur(ref List<string> HandPlayer, ref List<string> packet, ref int ScorePlayer)
        {
            bool joueurCouche = false;
            string choixPlayer = ""; // Initialisation vide pour entrer dans la boucle

            // CORRECTION : "ScorePlayer" avec une majuscule
            while (ScorePlayer < 21 && choixPlayer != "S" && joueurCouche == false)
            {
                Console.WriteLine("Voulez-vous piocher (H) ou rester (S) ?");
                choixPlayer = Console.ReadLine().ToUpper(); // Lire le choix et forcer en majuscule

                if (choixPlayer == "H")
                {
                    HandPlayer.Add(packet.Pop()); // ajout d'une carte à la main du joueur
                    ScorePlayer = CalculScore(HandPlayer); // recalcule du score
                }
                // CORRECTION : On utilise "else if" au lieu de "else" pour intercepter le "S"
                else if (choixPlayer == "S")
                {
                    joueurCouche = true;
                    Console.WriteLine("Vous décidez de vous arrêter.");
                }
                else
                {
                    Console.WriteLine("Choix invalide. Veuillez taper H ou S.");
                }
            }
        }

        public static void TourCroupier(ref List<string> HandCroupier, ref List<string> packet, ref int ScoreCroupier, int ScorePlayer)
        {
            // --- 1. Vérification si le joueur a dépassé 21 ---
            if (ScorePlayer > 21)
            {
                Console.WriteLine("Perdu");
                return; // Sort de la fonction si le joueur a déjà perdu
            }

            // --- 2. Initialisation du tour du croupier ---
            ScoreCroupier = CalculScore(HandCroupier);
            bool CroupierCouche = false;

            // --- 3. Logique du Croupier (si le joueur n'a pas sauté) ---
            if (ScorePlayer <= 21)
            {
                // On révèle la deuxième carte du croupier
                Console.WriteLine($"La deuxième carte du croupier était : {HandCroupier[1]}");

                // Règle classique : le croupier pioche tant qu'il est en dessous de 17
                while (ScoreCroupier < 17)
                {
                    for (int i = 0; i < 3; i++) // Boucle simple pour afficher trois petits points
                    {
                        Thread.Sleep(1200); // Met le programme en pause pendant 600 millisecondes (1.6s)
                        Console.Write("."); // Ajoute un point à chaque tour de boucle
                    }
                    Console.WriteLine(); // Retour à la ligne après les points

                    HandCroupier.Add(packet.Pop()); // On utilise le .Pop() créé précédemment
                    ScoreCroupier = CalculScore(HandCroupier);
                    Console.WriteLine($"Le croupier pioche. Nouveau score : {ScoreCroupier}");
                }
            } // CORRECTION : L'accolade fermante du "if (ScorePlayer <= 21)" est placée ici !
            else // Branche 'F' du ScorePlayer <= 21
            {
                CroupierCouche = true;
                Console.WriteLine("Croupier Bust !");
            }
        }
    }
}

      