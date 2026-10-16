using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace RDPKeuze
{
    // Centrale opslag van de serverlijst in data.xml (naast de exe).
    // Alleen lezen/schrijven/sorteren; de UI werkt direct op Server_lijst en roept na
    // elke wijziging Schrijf_server_lijst() aan. Fouten bij lezen/schrijven worden
    // genegeerd (stille catch) zodat de app ook zonder bestanden opstart.
    internal class DataRdp
    {
        // data welk in elk formulier bekend moet zijn
        //public static List<server> Server_lijst { get; set; }

        // De hele serverlijst in het geheugen; gedeeld door alle vensters.
        public static List<server> Server_lijst = new List<server>();

        // Sorteert de lijst op sectie; wordt ook door Schrijf_server_lijst() gebruikt,
        // zodat data.xml altijd in sectievolgorde weggeschreven wordt.
        public static void Sorteer()
        {
            Server_lijst.Sort(Sorteer_Server);
        }

        public static int Sorteer_Server(server a, server b)
        {
            return a._sectie.CompareTo(b._sectie);
        }

        // Leest data.xml in Server_lijst. Lukt lezen niet (bestand weg/corrupt), dan
        // blijft de bestaande lijst staan: dat voorkomt dat een volgende save een
        // lege lijst over de echte data zou schrijven.
        public static void Lees_server_lijst()
        {
            try
            {
                string xmlTekst = File.ReadAllText(FormRdpKeuze.datapath + "data.xml");
                Server_lijst = FromXML<List<server>>(xmlTekst);
            }
            catch { }
        }

        // Sorteert de lijst en schrijft ze weg naar data.xml.
        public static void Schrijf_server_lijst()
        {
            Sorteer();
            try
            {
                string xmlTekst = ToXML(Server_lijst);
                File.WriteAllText(FormRdpKeuze.datapath + "data.xml", xmlTekst);
            }
            catch { }
        }

        // Hulpje om een object als XML-tekst te serialiseren (voor bestanden).
        public static string ToXML<T>(T obj)
        {
            using (StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }

        // Hulpje om XML-tekst (uit een bestand) weer terug om te zetten in een object.
        public static T FromXML<T>(string xml)
        {
            using (StringReader stringReader = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stringReader);
            }
        }
    }
}
