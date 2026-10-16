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
    // Pop-up met de zoekresultaten van het hoofdvenster (gevulde lijst met plaatsnamen).
    // Sluit zichzelf bij dubbelklik of Enter; het hoofdvenster leest daarna de keuze uit.
    public partial class ZoekEnSelect : Form
    {
        public ZoekEnSelect()
        {
            InitializeComponent();
        }

        // Dubbelklik op een treffer = kiezen (venster sluiten).
        private void listBoxGevonden_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Close();
        }

        // Enter op een treffer = kiezen (venster sluiten).
        private void listBoxGevonden_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Return)
                Close();
        }

        // Bij openen meteen de eerste treffer selecteren.
        private void ZoekEnSelect_Shown(object sender, EventArgs e)
        {
            if(listBoxGevonden.Items.Count > 0)
                listBoxGevonden.SelectedIndex = 0;
        }
    }
}
