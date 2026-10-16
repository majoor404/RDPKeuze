using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace RDPKeuze
{
    // Losse wachtwoordenlijst voor het hoofdvenster (apart van WachtWoordForm, dat het
    // wachtwoordbeheer opent). LaadWachtwoorden() wordt bij het opstarten aangeroepen;
    // daarna kan GetWW() het wachtwoord bij een usernaam ophalen zodat het hoofdvenster
    // het op het klembord kan plaatsen. Lezen gebeurt uit ww.xml in de map van de exe.
    public static class ZoekWW
    {
        static List<wachtwoord> wachtwoord_lijst = new List<wachtwoord>();

        // Leest ww.xml en onthasbelt (Unscramble) alle wachtwoorden in het geheugen.
        // Bestand ontbreekt of is onleesbaar → lege lijst, app blijft werken.
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
                wachtwoord_._ww = WachtWoordForm.Unscramble(wachtwoord_._ww);
            }
            return wachtwoord_lijst;
        }

        // Leest XML-tekst uit een bestand terug tot een object (hulpje).
        private static T FromXML<T>(string xml)
        {
            using (StringReader stringReader = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stringReader);
            }
        }

        // Zoekt het wachtwoord bij een usernaam; geeft "" als er niets gevonden is.
        // Het onthasbelen zelf zit in WachtWoordForm.Unscramble (één implementatie,
        // gedeeld met het wachtwoordbeheer).
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
