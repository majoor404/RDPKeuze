using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RDPKeuze
{
    // Dialoogvenster om één wachtwoordrecord te bekijken/wijzigen: textBox1 = naam,
    // textBox2 = gebruiker, textBox3 = wachtwoord. Het aanroepende venster
    // (WachtWoordForm) leest de waarden bij OK uit en slaat ze op.
    public partial class WachtWoordEdit : Form
    {
        public WachtWoordEdit()
        {
            InitializeComponent();
        }

        // Bij openen de focus op Annuleren zetten, zodat niet per ongeluk op OK
        // (opslaan) gedrukt wordt.
        private void WachtWoordEdit_Shown(object sender, EventArgs e)
        {
            buttonCancel.Focus();
        }
    }
}
