using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace RDPKeuze
{
    class DataRdp
    {
        // data welk in elk formulier bekend moet zijn
        //public static List<server> Server_lijst { get; set; }
        public static List<server> Server_lijst = new List<server>();

        public static void Sorteer()
        {
            Server_lijst.Sort(Sorteer_Server);
        }

        public static int Sorteer_Server(server a, server b)
        {
            return a._sectie.CompareTo(b._sectie);
        }

        public static void Lees_server_lijst()
        {
            Server_lijst.Clear();
            try
            {
                using (Stream stream = File.Open("data.bin", FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();

                    Server_lijst = (List<server>)bin.Deserialize(stream);
                }
            }
            catch (IOException)
            {
            }
        }

        public static void Schrijf_server_lijst()
        {
            try
            {
                using (Stream stream = File.Open("data.bin", FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, Server_lijst);
                }
            }
            catch (IOException)
            {
            }
        }
    }
}
