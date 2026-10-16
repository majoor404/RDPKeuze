using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDPKeuze
{
    // Eén wachtwoordrecord uit ww.xml. Let op: de veldnamen _naam, _user en _ww komen
    // letterlijk in het bestand voor; hernoemen breekt bestaande ww.xml-bestanden.
    public class wachtwoord
    {
        public string _user;           // inlognaam waarop gezocht wordt (moet overeenkomen met server._usernaam)
        public string _ww;             // het wachtwoord; in ww.xml staat dit verhaspeld (Scramble)
        public string _naam;           // omschrijving die in het overzicht getoond wordt


        public wachtwoord() { }

        // Volledige constructor (naam, gebruiker, wachtwoord).
        public wachtwoord(string s, string p, string a)
        {
            _naam = s;
            _user = p;
            _ww = a;
        }
    }
}
