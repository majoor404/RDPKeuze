using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace RDPKeuze
{
    public partial class EditForm : Form
    {
        private int index = -1;

        public EditForm()
        {
            InitializeComponent();
        }

        private void EditForm_Shown(object sender, EventArgs e)
        {
            //textBoxImport.Visible = false;
            ListView.Items.Clear();
            string[] arr = new string[5];
            ListViewItem itm;
            foreach (server a in DataRdp.Server_lijst)
            {
                arr[0] = a._adres;
                arr[1] = a._domein;
                arr[2] = a._plaats;
                arr[3] = a._sectie;
                arr[4] = a._usernaam;
                itm = new ListViewItem(arr);
                _ = ListView.Items.Add(itm);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("verwijder ?", "Verwijder deze server", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (ListView.SelectedItems.Count > 0)
                {
                    index = ListView.Items.IndexOf(ListView.SelectedItems[0]);
                    DataRdp.Server_lijst.RemoveAt(index);
                    DataRdp.Schrijf_server_lijst();
                    EditForm_Shown(this, null);
                }
            }
        }


        public void buttonSorteer_Click(object sender, EventArgs e)
        {
            DataRdp.Sorteer();
            EditForm_Shown(this, null);
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            if (ListView.SelectedItems.Count > 0)
            {
                server a = new server(DataRdp.Server_lijst[index]._sectie, "copy " + DataRdp.Server_lijst[index]._plaats, DataRdp.Server_lijst[index]._adres, DataRdp.Server_lijst[index]._usernaam, DataRdp.Server_lijst[index]._domein, false);
                DataRdp.Server_lijst.Add(a);
                EditForm_Shown(this, null);
            }
            else
            {
                _ = MessageBox.Show("selecteer een regel");
            }
        }
        

        private void ButEdit2_Click(object sender, EventArgs e)
        {
            if (ListView.SelectedItems.Count > 0)
            {
                EditForm2 ef = new EditForm2();

                index = ListView.Items.IndexOf(ListView.SelectedItems[0]);
                ef.textBox3.Text = DataRdp.Server_lijst[index]._adres;
                ef.textBox2.Text = DataRdp.Server_lijst[index]._plaats;
                ef.textBox1.Text = DataRdp.Server_lijst[index]._sectie;
                ef.textBox4.Text = DataRdp.Server_lijst[index]._usernaam;

                ef.VncToggle.Checked = DataRdp.Server_lijst[index]._domein == "VNC";

                DialogResult dialogResult = ef.ShowDialog();

                if(dialogResult == DialogResult.OK)
                {
                    if (ef.textBox1.Text == "")
                    {
                        _ = MessageBox.Show("Vul Sectie in voor \"Save\"");
                        return;
                    }
                    
                    if (ListView.SelectedItems.Count > 0)
                    {
                        DataRdp.Server_lijst[index]._adres = ef.textBox3.Text;
                        DataRdp.Server_lijst[index]._plaats = ef.textBox2.Text;
                        DataRdp.Server_lijst[index]._sectie = ef.textBox1.Text;
                        DataRdp.Server_lijst[index]._usernaam = ef.textBox4.Text;

                        DataRdp.Server_lijst[index]._domein = "RDP";
                        
                        if (ef.VncToggle.Checked)
                        {
                            DataRdp.Server_lijst[index]._domein = "VNC";
                        }

                        DataRdp.Schrijf_server_lijst();
                        EditForm_Shown(this, null);
                    }
                }
            }
        }

        private void ButNew2_Click(object sender, EventArgs e)
        {
            EditForm2 ef = new EditForm2();

            ef.textBox3.Text = ef.textBox2.Text = ef.textBox1.Text = ef.textBox4.Text = "";
            ef.VncToggle.Checked = false;

            DialogResult dialogResult = ef.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                if (ef.textBox1.Text == "")
                {
                    _ = MessageBox.Show("Vul Sectie in voor \"Save\"");
                    return;
                }

                string type = ef.VncToggle.Checked ? "VNC" : "RDP";

                server a = new server(ef.textBox1.Text, ef.textBox2.Text, ef.textBox3.Text, ef.textBox4.Text, type, false);
                DataRdp.Server_lijst.Add(a);
                DataRdp.Schrijf_server_lijst();
                EditForm_Shown(this, null);
            }
        }
    }
}
