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
    }
}
