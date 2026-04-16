using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public static class PacketCarte
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
            int SizePacket = Packet.Count();
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
    }
}
