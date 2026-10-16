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
    // Klein wachtwoordvenster dat alleen in Release-builds gebruikt wordt als poort
    // voor het wachtwoordbeheer (zie buttonWW_Click in MainFormRdp). Dit venster vraagt
    // het vaste master-wachtwoord; de controle zelf staat in het hoofdvenster.
    public partial class tsbww : Form
    {
        public tsbww()
        {
            InitializeComponent();
        }

        // Bij openen het invoerveld leegmaken en focus geven.
        private void tsbww_Shown(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.Focus();
        }
    }
}
