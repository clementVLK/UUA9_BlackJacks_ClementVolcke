namespace BlackJack
{
    using System; // Bibliothèque de base pour les entrées/sorties (Console)
    using System.Threading; // Bibliothèque nécessaire pour utiliser Thread.Sleep (pauses)

    public class Program
    {
        public static void Main()
        {
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
        }
    }
}
