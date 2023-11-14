﻿using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace RDPKeuze
{
    internal class DataRdp
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
            try
            {
                Server_lijst.Clear();
                string xmlTekst = File.ReadAllText(FormRdpKeuze.datapath + "data.xml");
                Server_lijst = FromXML<List<server>>(xmlTekst);
            }
            catch { }
        }

        public static void Schrijf_server_lijst()
        {
            try
            {
                string xmlTekst = ToXML(Server_lijst);
                File.WriteAllText(FormRdpKeuze.datapath + "data.xml", xmlTekst);
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
    }
}
