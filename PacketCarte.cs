using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public static class PacketCarte
    {
        public static void DistributionCartes(out List<string> HandPlayer, out List<string> HandCroupier, ref List<string> Packet)
        {
            HandPlayer = new List<string>();
            HandCroupier = new List<string>();

            // distribue les cartes en alternant entre le joueur et le croupier
            HandPlayer.Add(Packet.Pop());
            HandCroupier.Add(Packet.Pop());
            HandPlayer.Add(Packet.Pop());
            HandCroupier.Add(Packet.Pop());
        }
    }
    public static class ListExtensions
    {
        public static T Pop<T>(this List<T> list)
        {
            T element = list[0];
            list.RemoveAt(0);
            return element;
        }
    }
}
