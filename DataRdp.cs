using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

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
            //Server_lijst.Clear();
            //try
            //{
            //    using (Stream stream = File.Open("data.bin", FileMode.Open))
            //    {
            //        BinaryFormatter bin = new BinaryFormatter();

            //        Server_lijst = (List<server>)bin.Deserialize(stream);
            //    }
            //}
            //catch (IOException)
            //{
            //}

            try
            {
                string xmlTekst = File.ReadAllText("data.xml");
                Server_lijst.Clear();
                Server_lijst = FromXML<List<server>>(xmlTekst);
            }
            catch { }
        }

        public static void Schrijf_server_lijst()
        {
            //try
            //{
            //    using (Stream stream = File.Open("data.bin", FileMode.Create))
            //    {
            //        BinaryFormatter bin = new BinaryFormatter();
            //        bin.Serialize(stream, Server_lijst);
            //    }
            //}
            //catch (IOException)
            //{
            //}

            try
            {
                string xmlTekst = ToXML(Server_lijst);
                File.WriteAllText("data.xml", xmlTekst);
            }
            catch { }
        }

        public static string ToXML<T>(T obj)
        {
            using (StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }

        public static T FromXML<T>(string xml)
        {
            using (StringReader stringReader = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stringReader);
            }
        }

        public static void ZetOmNaarXML()
        {
            Server_lijst.Clear();
            try
            {
                using (Stream stream = File.Open("data.bin", FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();

                    Server_lijst = (List<server>)bin.Deserialize(stream);
                }

                try
                {
                    string xmlTekst = ToXML(Server_lijst);
                    File.WriteAllText("data.xml", xmlTekst);
                    // delete data.bin
                    string newFilePath = "data.bin.old";
                    File.Move("data.bin", newFilePath);
                }
                catch { }
            }
            catch (IOException)
            {
            }
        }
    }
}
