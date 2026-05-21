using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class PacketCarte
    {
        public static void CreationPacketCarte(out List<string> Packet)
        {
            Packet = new List<string>();
            string[] listeB = { "Coeur", "Carreau", "Trèfle", "Pique" };
            string[] listeA = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Valet", "Dame", "Roi", "As" };

            for (int IB = 0; IB < listeB.Length; IB++)
            {
                for (int IA = 0; IA < listeA.Length; IA++)
                {
                    Packet.Add(listeA[IA] + " de " + listeB[IB]);
                }
            }
        }

        public static void MelangePacketCarte(List<string> Packet)
        {
            // Récupère la taille du paquet (sans parenthèses pour utiliser la propriété native Count et éviter une erreur LINQ)
            int SizePacket = Packet.Count;
            int iCardAlea;
            Random rnd = new Random();

            for (int iPacketPlace = 0; iPacketPlace < SizePacket; iPacketPlace++)
            {
                iCardAlea = rnd.Next(0, iPacketPlace + 1);
                string CardAlea = Packet[iCardAlea];
                Packet[iCardAlea] = Packet[iPacketPlace];
                Packet[iPacketPlace] = CardAlea;
            }
        }

        public static void DistributionCartes(out List<string> HandPlayer, out List<string> HandCroupier, ref List<string> Packet)
        {
            HandPlayer = new List<string>();
            HandCroupier = new List<string>();

            // Distribue les cartes en alternant entre le joueur et le croupier en retirant les cartes du paquet
            HandPlayer.Add(Packet.Pop());
            HandCroupier.Add(Packet.Pop());
            HandPlayer.Add(Packet.Pop());
            HandCroupier.Add(Packet.Pop());
        }

        public static void AfficherCartesGraphiques(List<string> main, bool masquerCachee = false)
        {
            // Sécurité : si la main n'existe pas ou ne contient aucune carte, on arrête immédiatement la méthode
            if (main == null || main.Count == 0) return;

            // Force la console à utiliser l'encodage UTF-8 pour pouvoir afficher correctement les symboles de cartes (♥, ♦, ♣, ♠)
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Crée un tableau de 7 chaînes de caractères initialisées à vide pour stocker les 7 lignes horizontales du dessin
            string[] lignes = new string[7] { "", "", "", "", "", "", "" };

            // Boucle qui va parcourir chaque carte présente dans la main du joueur ou du croupier
            for (int i = 0; i < main.Count; i++)
            {
                // Vérifie si on traite la deuxième carte (index 1) et si le paramètre demande de la cacher (cas du croupier)
                if (i == 1 && masquerCachee)
                {
                    // Ajoute le dessin opaque d'une carte retournée (dos de carte) aux différentes lignes du tableau
                    lignes[0] += "┌─────────┐ ";
                    lignes[1] += "│░░░░░░░░░│ ";
                    lignes[2] += "│░░░░░░░░░│ ";
                    lignes[3] += "│░░░???░░░│ ";
                    lignes[4] += "│░░░░░░░░░│ ";
                    lignes[5] += "│░░░░░░░░░│ ";
                    lignes[6] += "└─────────┘ ";
                    // Passe directement à l'itération de boucle suivante (la prochaine carte) sans exécuter le reste du code
                    continue;
                }

                // Découpe la chaîne de la carte actuelle (ex: "10 de Coeur") à chaque espace pour séparer les mots
                string[] parties = main[i].Split(' ');
                // Extrait le tout premier mot du tableau, qui correspond toujours à la valeur de la carte (ex: "10" ou "As")
                string valeurBrute = parties[0];
                // Extrait le tout dernier mot du tableau, qui correspond à la couleur de la carte (ex: "Coeur", "Pique")
                string couleurBrute = parties[parties.Length - 1];

                // Utilise un switch "classique" (compatible avec toutes les versions de C#) pour convertir les noms en raccourcis
                string valeurCourte = valeurBrute;
                switch (valeurBrute)
                {
                    // Convertit les têtes et l'As en une seule lettre (version anglaise ici avec J, Q, K pour être standard)
                    case "As": valeurCourte = "A"; break;
                    case "Valet": valeurCourte = "J"; break;
                    case "Dame": valeurCourte = "Q"; break;
                    case "Roi": valeurCourte = "K"; break;
                        // Pour les autres valeurs (2 à 10), la valeur reste celle de départ par défaut
                }

                // Utilise un switch classique pour convertir le mot de la couleur en son symbole graphique Unicode
                string symbole = couleurBrute;
                switch (couleurBrute)
                {
                    case "Coeur": symbole = "♥"; break;
                    case "Carreau": symbole = "♦"; break;
                    case "Trèfle": symbole = "♣"; break;
                    case "Pique": symbole = "♠"; break;
                }

                // Si le raccourci fait 2 caractères (ex: "10"), aucun espace n'est requis à gauche pour centrer, sinon on ajoute un espace
                string espaceGauche = valeurCourte.Length == 2 ? "" : " ";
                // Même logique pour l'alignement en bas à droite
                string espaceDroite = valeurCourte.Length == 2 ? "" : " ";

                // Concatène les bords, la valeur et le symbole, ligne par ligne, pour construire la face de la carte
                lignes[0] += "┌─────────┐ ";
                lignes[1] += $"│{valeurCourte}{espaceGauche}       │ "; // Valeur courte en haut à gauche
                lignes[2] += "│         │ ";
                lignes[3] += $"│    {symbole}    │ "; // Symbole (♥, ♦...) parfaitement au centre
                lignes[4] += "│         │ ";
                lignes[5] += $"│       {espaceDroite}{valeurCourte}│ "; // Valeur courte en bas à droite
                lignes[6] += "└─────────┘ ";
            }

            // Boucle foreach pour parcourir chacune des 7 lignes horizontales construites dans notre tableau final
            foreach (string ligne in lignes)
            {
                // Imprime la ligne complète dans la console (affichant ainsi toutes les cartes de la main côte à côte)
                Console.WriteLine(ligne);
            }
        }
    }

    // Architecture corrigée : On isole la méthode d'extension Pop() dans sa propre classe statique pour éviter les erreurs
    public class ListExtensions
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