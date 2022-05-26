using System.Collections.Generic;

namespace RDPKeuze
{
    class DataRdp
    {
        // data welk in elk formulier bekend moet zijn
        public static List<server> Server_lijst { get; set; }

        public static void Sorteer()
        {
            Server_lijst.Sort(Sorteer_Server);
        }

        public static int Sorteer_Server(server a, server b)
        {
            return a._sectie.CompareTo(b._sectie);
        }
    }
}
