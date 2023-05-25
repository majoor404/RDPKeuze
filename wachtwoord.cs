using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDPKeuze
{
    public class wachtwoord
    {
        public string _user;
        public string _ww;
        public string _naam;


        public wachtwoord() { }
        public wachtwoord(string s, string p, string a)
        {
            _naam = s;
            _user = p;
            _ww = a;
        }
    }
}
