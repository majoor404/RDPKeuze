using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace RDPKeuze
{
    public static class ZoekWW
    {
        static List<wachtwoord> wachtwoord_lijst = new List<wachtwoord>();
        // laad wachtwoorden
        public static List<wachtwoord> LaadWachtwoorden()
        {
            wachtwoord_lijst.Clear();
            try
            {
                string xmlTekst = System.IO.File.ReadAllText(FormRdpKeuze.datapath + "ww.xml");
                wachtwoord_lijst = FromXML<List<wachtwoord>>(xmlTekst);
            }
            catch { }
            // unscramble
            foreach (wachtwoord wachtwoord_ in wachtwoord_lijst)
            {
                wachtwoord_._ww = Unscramble(wachtwoord_._ww);
            }
            return wachtwoord_lijst;
        }

        private static T FromXML<T>(string xml)
        {
            using (StringReader stringReader = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stringReader);
            }
        }

        private static string Unscramble(string woord)
        {
            string ret = "";
            if (woord == null || woord.Length < 1)
            {
                return ret;
            }

            for (int h = 0; h < woord.Length; h++)
            {
                int xx22;// = 0;
                xx22 = woord[h];
                xx22 -= 2;
                xx22 -= woord.Length - h - 1;
                char character = (char)xx22;
                ret += character.ToString();
            }
            return ret;
        }

        public static void GetWW(string naam, out string ww)
        {
            ww = "";
            foreach (wachtwoord wachtwoord_ in wachtwoord_lijst)
            {
                if (wachtwoord_._user == naam)
                {
                    ww = wachtwoord_._ww;
                    break;
                }
            }
        }


    }
}
