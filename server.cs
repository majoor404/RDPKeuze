namespace RDPKeuze
{
    // Eén verbinding die in data.xml bewaard wordt (één regel in de lijst).
    // De velden heten met underscore en zijn publiek, zodat de XML-serializer ze
    // direct weg kan schrijven.
    public class server
    {
        public string _sectie;         // groep/cluster in de sectiekiezer (bovenste keuzelijst)
        public string _plaats;         // weergavenaam van de computer (in de computerkiezer)
        public string _adres;          // hostnaam of IP waar de verbinding naartoe gaat
        public string _usernaam;       // inlognaam, eventueel met domein (domein\gebruiker)
        public string _domein;         // gebruik ik nu als type aanduiding ("RDP" of "VNC")
        public bool _multiscreen;      // use multimon:i: aan (meerdere schermen)

        public server() { }

        // Volledige constructor; volgorde van de parameters klopt met de aanroepen
        // in EditForm (sectie, plaats, adres, usernaam, type, multiscreen).
        public server(string s, string p, string a, string u, string d, bool m)
        {
            _sectie = s;
            _plaats = p;
            _adres = a;
            _usernaam = u;
            _domein = d;
            _multiscreen = m;
        }
    }
}
