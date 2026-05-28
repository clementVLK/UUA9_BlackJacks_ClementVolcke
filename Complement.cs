using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public static class ListExtensions
    {
        // Méthode permettant de retirer et de renvoyer le premier élément d'une liste
        public static T Pop<T>(this List<T> list)
        {
            T element = list[0]; // Sauvegarde la première carte
            list.RemoveAt(0);    // Supprime cette carte de la liste d'origine
            return element;      // Renvoie la carte sauvegardée
        }
        public static int CalculScore(List<string> main)
        {
            int score = 0;
            int nombreAs = 0;

            foreach (string carte in main)
            {
                // On extrait le premier mot (ex : "Valet" ou "10")
                string valeurBrute = carte.Split(' ')[0];

                if (valeurBrute == "Valet" || valeurBrute == "Dame" || valeurBrute == "Roi")
                {
                    score += 10;
                }
                else if (valeurBrute == "As")
                {
                    score += 11;
                    nombreAs++;
                }
                else
                {
                    score += int.Parse(valeurBrute);
                }
            }

            // Gestion intelligente de la valeur de l'As (11 ou 1)
            while (score > 21 && nombreAs > 0)
            {
                score -= 10;
                nombreAs--;
            }

            return score;
        }
        public static void AfficherRegles()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n═" + new string('═', 65) + "═");
            Console.WriteLine("                       RÈGLES DU BLACKJACK                        ");
            Console.WriteLine("═" + new string('═', 65) + "═");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n [BUT DU JEU]");
            Console.ResetColor();
            Console.WriteLine("   Battre le croupier en obtenant un score le plus proche de 21.");
            Console.WriteLine("   Attention : Si vous dépassez 21, vous perdez d'office (BUST).");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n [VALEUR DES CARTES]");
            Console.ResetColor();
            Console.WriteLine("   • De 2 à 10 : Valeur indiquée sur la carte.");
            Console.WriteLine("   • Valet (J), Dame (Q), Roi (K) : Valent 10 points.");
            Console.WriteLine("   • As (A) : Vaut 11 ou 1 selon ce qui arrange votre jeu.");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n [DÉROULEMENT D'UNE DONNE]");
            Console.ResetColor();
            Console.WriteLine("   1. Vous misez une somme.");
            Console.WriteLine("   2. Le croupier distribue 2 cartes visibles à vous, et 2 au croupier (dont une cachée).");
            Console.WriteLine("   3. Votre tour : Vous pouvez piocher (H) autant de fois que voulu ou Rester (S).");
            Console.WriteLine("   4. Tour du croupier : Il révèle sa carte cachée et piochera TANT QUE son score est inférieur à 17.");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n [RÉSULTATS]");
            Console.ResetColor();
            Console.WriteLine("   • Votre score > Croupier ou Croupier > 21 : Vous gagnez 1x votre mise.");
            Console.WriteLine("   • Même score : Égalité, vous reprenez votre mise.");
            Console.WriteLine("   • Votre score < Croupier : Le casino remporte votre mise.");

            Console.WriteLine("\n" + new string('─', 67));
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(" > Appuyez sur une touche pour fermer les règles et lancer la partie...");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
