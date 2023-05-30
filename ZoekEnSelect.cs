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
    public partial class ZoekEnSelect : Form
    {
        public ZoekEnSelect()
        {
            InitializeComponent();
        }

        private void listBoxGevonden_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Close();
        }

        private void listBoxGevonden_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Return)
                Close();
        }

        private void ZoekEnSelect_Shown(object sender, EventArgs e)
        {
            if(listBoxGevonden.Items.Count > 0)
                listBoxGevonden.SelectedIndex = 0;
        }
    }
}
