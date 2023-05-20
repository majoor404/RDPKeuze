using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDPKeuze
{
	[Serializable()]

	public class server
    {
		public string _sectie;
		public string _plaats;
		public string _adres;
		public string _usernaam;
		public string _domein;
		public bool _multiscreen;

		public server() { }
		public server(string s, string p, string a, string u, string d , bool m)
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
