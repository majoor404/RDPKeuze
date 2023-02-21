using System;
using System.IO;
using System.Linq;
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
            textBoxImport.Visible = false;
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

        private void VoegToeBut_Click(object sender, EventArgs e)
        {

            if (TestDubbel(textBox3.Text, textBox2.Text))
            {
                _ = MessageBox.Show("Adres + Naam bestaat al in lijst");
                return;
            }

            if (textBox1.Text == "")
            {
                _ = MessageBox.Show("Vul Sectie in voor \"Voeg toe\"");
                return;
            }

            ListView.SelectedItems.Clear();

            server a = new server(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);
            DataRdp.Server_lijst.Add(a);
            DataRdp.Schrijf_server_lijst();
            EditForm_Shown(this, null);
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = "";
        }

        private void ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListView.SelectedItems.Count > 0)
            {
                //string str = ListView.SelectedItems[0].Text.ToString();
                index = ListView.Items.IndexOf(ListView.SelectedItems[0]);
                textBox3.Text = DataRdp.Server_lijst[index]._adres;
                textBox5.Text = DataRdp.Server_lijst[index]._domein;
                textBox2.Text = DataRdp.Server_lijst[index]._plaats;
                textBox1.Text = DataRdp.Server_lijst[index]._sectie;
                textBox4.Text = DataRdp.Server_lijst[index]._usernaam;

                VncToggle.Checked = textBox5.Text == "VNC";
            }
            else
            {
                textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = "";
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                _ = MessageBox.Show("Vul Sectie in voor \"Save\"");
                return;
            }
            // test of het al bestaat in lijst
            if (!TestDubbel(textBox3.Text, textBox2.Text))
            {
                if (ListView.SelectedItems.Count > 0)
                {
                    DataRdp.Server_lijst[index]._adres = textBox3.Text;
                    DataRdp.Server_lijst[index]._domein = textBox5.Text;
                    DataRdp.Server_lijst[index]._plaats = textBox2.Text;
                    DataRdp.Server_lijst[index]._sectie = textBox1.Text;
                    DataRdp.Server_lijst[index]._usernaam = textBox4.Text;
                    DataRdp.Schrijf_server_lijst();
                    EditForm_Shown(this, null);
                }
            }
            else
            {
                _ = MessageBox.Show("Naam en Adres bestaan al in lijst");
            }



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
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
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
                server a = new server(textBox1.Text, "copy " + textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);
                DataRdp.Server_lijst.Add(a);
                EditForm_Shown(this, null);
            }
            else
            {
                _ = MessageBox.Show("selecteer een regel");
            }
        }

        private void VncToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (VncToggle.Checked)
            {
                textBox5.Text = "VNC";
                textBox5.Enabled = false;
                label5.Text = "Type";
                label4.Text = "Wachtwoord";
            }
            else
            {
                textBox5.Enabled = true;
                label5.Text = "Domein";
                label4.Text = "Gebruikers naam";
            }
        }

        private void ButtonImport_Click(object sender, EventArgs e)
        {
            textBoxImport.Clear();
            OpenFileDialog open = new OpenFileDialog
            {
                Title = "Open Rdp",
                Filter = "Rdp files (*.rdp)|*.rdp"
            };

            if (open.ShowDialog() == DialogResult.OK)
            {
                //textBoxImport.Visible = true;
                textBoxImport.Text = File.ReadAllText(open.FileName);
            }

            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = "";

            // zoek nu items en zet in textboxén
            for (int i = 0; i < textBoxImport.Lines.Count() - 1; i++)
            {
                string regel = textBoxImport.Lines[i].Trim();

                if (regel.Length > 15 && regel.Substring(0, 15) == "full address:s:")
                {
                    textBox3.Text = regel.Substring(15);
                }

                if (regel.Length > 11 && regel.Substring(0, 11) == "username:s:")
                {
                    textBox4.Text = regel.Substring(11);
                }

                textBox2.Text = Path.GetFileNameWithoutExtension(open.FileName);

            }
        }

        private bool TestDubbel(string adres, string plaats)
        {
            foreach (server a in DataRdp.Server_lijst)
            {
                if (a._adres == adres && a._plaats == plaats)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
