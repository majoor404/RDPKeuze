using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace RDPKeuze
{
    public partial class FormRdpKeuze : Form
    {
        private List<server> server_lijst = new List<server>();

        public FormRdpKeuze()
        {
            InitializeComponent();
            DataRdp.Server_lijst = server_lijst;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // laad ronald.rdp in textbox
            textBox.Text = File.ReadAllText("ronald.rdp", Encoding.ASCII);
            UpdateUi();
        }

        // start button
        private void Button3_Click(object sender, EventArgs e)
        {
            // save naar ronald.rdp
            File.WriteAllText("ronald.rdp", textBox.Text);
            // run ...
            Process.Start(@"ronald.rdp");
        }

        private void EditLijst_Click(object sender, EventArgs e)
        {
            Lees_server_lijst();
            EditForm Edit = new EditForm();
            Edit.ShowDialog();
            Schrijf_server_lijst();
        }

        public void UpdateUi()
        {
            Lees_server_lijst();

            SectieLijst.Items.Clear();
            foreach (server a in DataRdp.Server_lijst)
            {
                if (!SectieLijst.Items.Contains(a._sectie))
                    SectieLijst.Items.Add(a._sectie);
            }

            computerlijst.Items.Clear();
            computerlijst.Text = "";
            LocatiePlaatst.Text = "";
        }

        private void Computerlijst_DropDown(object sender, EventArgs e)
        {
            computerlijst.Items.Clear();

            foreach (server a in DataRdp.Server_lijst)
            {
                if (a._sectie == SectieLijst.Text)
                {
                    if (!computerlijst.Items.Contains(a._plaats))
                        computerlijst.Items.Add(a._plaats);
                }
            }
        }

        private void Computerlijst_SelectedIndexChanged(object sender, EventArgs e)
        {
            LocatiePlaatst.Text = "";

            foreach (server a in DataRdp.Server_lijst)
            {


                if (a._plaats == computerlijst.Text)
                {
                    LocatiePlaatst.Text = a._adres;

                    textBox.Text = File.ReadAllText("ronald.rdp", Encoding.ASCII);
                    textBoxTemp.Text = textBox.Text;
                    textBox.Clear();

                    textBox.AppendText("full address:s:" + a._adres);
                    textBox.AppendText(Environment.NewLine);

                    textBox.AppendText("username:s:" + a._usernaam);
                    textBox.AppendText(Environment.NewLine);

                    textBox.AppendText("domain:s:" + a._domein);
                    textBox.AppendText(Environment.NewLine);

                    int aantal_regels = textBoxTemp.Lines.Count();
                    for (int i = 0; i < aantal_regels - 3; i++)
                    {
                        textBox.AppendText(textBoxTemp.Lines[i + 3]);
                        textBox.AppendText(Environment.NewLine);
                    }
                    textBox.SelectionStart = 1;
                    textBox.ScrollToCaret();
                    break;
                }
            }
        }

        public void Lees_server_lijst()
        {
            server_lijst.Clear();
            try
            {
                using (Stream stream = File.Open("data.bin", FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();

                    server_lijst = (List<server>)bin.Deserialize(stream);
                }
                DataRdp.Server_lijst = server_lijst;
            }
            catch (IOException)
            {
            }
        }

        public void Schrijf_server_lijst()
        {
            try
            {
                using (Stream stream = File.Open("data.bin", FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, server_lijst);
                }
            }
            catch (IOException)
            {
            }
            UpdateUi(); // zet alles in form weer juist met laatste data
        }

        private void SectieLijst_SelectedIndexChanged(object sender, EventArgs e)
        {
            computerlijst.Items.Clear();
            computerlijst.Text = "";
            LocatiePlaatst.Text = "";
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            UpdateUi();
        }

        private void linkLabel1_LinkGitHub(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/majoor404/RDPKeuze");
        }
    }
}
