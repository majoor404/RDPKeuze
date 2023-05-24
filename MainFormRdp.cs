using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RDPKeuze
{
    public partial class FormRdpKeuze : Form
    {
        private string vnc_adres = "";
        public FormRdpKeuze()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //laad ronald.rdp in textbox
            //DataRdp.Lees_server_lijst(); zit in updateUI
            textBox.Text = File.ReadAllText("ronald.rdp", Encoding.ASCII);

            //if (File.Exists("Data.bin"))
            //    DataRdp.ZetOmNaarXML();

            UpdateUi();
        }

        // start button
        private void Button3_Click(object sender, EventArgs e)
        {
            string gekozen_sectie = SectieLijst.Text;

            if (!vnclabel.Visible/*!LocatiePlaatst.Text.Contains("VNC")*/)
            {
                // save naar ronald.rdp
                File.WriteAllText("ronald.rdp", textBox.Text);
                // run ...
                _ = Process.Start(@"ronald.rdp");
            }
            else
            {
                Process process = new Process
                {
                    StartInfo =
              {
                  FileName = "vncviewer.exe",
                  Arguments = vnc_adres
              }
                };
                _ = process.Start();
            }
            UpdateUi();

            SectieLijst.Text = gekozen_sectie;
        }

        private void EditLijst_Click(object sender, EventArgs e)
        {
            DataRdp.Lees_server_lijst();
            EditForm Edit = new EditForm();
            _ = Edit.ShowDialog();
        }

        public void UpdateUi()
        {
            DataRdp.Lees_server_lijst();
            SectieLijst.Items.Clear();
            vnclabel.Visible = false;

            foreach (server a in DataRdp.Server_lijst)
            {
                if (!SectieLijst.Items.Contains(a._sectie))
                {
                    _ = SectieLijst.Items.Add(a._sectie);
                }
            }

            computerlijst.Items.Clear();
            computerlijst.SelectedIndex = -1;
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
                    {
                        _ = computerlijst.Items.Add(a._plaats);
                    }
                }
            }
        }

        private void Computerlijst_SelectedIndexChanged(object sender, EventArgs e)
        {
            LocatiePlaatst.Text = "";
            vnclabel.Visible = false;
            multischerm.Checked = false;

            foreach (server a in DataRdp.Server_lijst)
            {
                if (a._plaats == computerlijst.Text && a._sectie == SectieLijst.Text)
                {
                    SchrijfDataIntextBox(a);
                }
            }
        }

        private void SectieLijst_SelectedIndexChanged(object sender, EventArgs e)
        {
            computerlijst.Items.Clear();
            computerlijst.SelectedIndex = -1;
            LocatiePlaatst.Text = "";
            textBoxZoek.Text = "";
            vnclabel.Visible = false;
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            UpdateUi();
        }

        private void linkLabel1_LinkGitHub(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _ = System.Diagnostics.Process.Start("https://github.com/majoor404/RDPKeuze");
        }

        private void textBoxZoek_Enter(object sender, EventArgs e)
        {
            UpdateUi();
        }

        private void GaZoeken(object sender, EventArgs e)
        {
            ZoekEnSelect zoekform = new ZoekEnSelect();
            zoekform.listBoxGevonden.Items.Clear();

            foreach (server a in DataRdp.Server_lijst)
            {
                if (a._plaats.ToLower().Contains(textBoxZoek.Text.ToLower()))
                {
                    _ = zoekform.listBoxGevonden.Items.Add(a._plaats);
                }
            }
            _ = zoekform.ShowDialog();

            if (zoekform.listBoxGevonden.SelectedItem == null)
            {
                return;
            }

            string geselecteerd = zoekform.listBoxGevonden.SelectedItem.ToString();
            if (geselecteerd != "")
            {
                foreach (server a in DataRdp.Server_lijst)
                {
                    if (a._plaats == geselecteerd)
                    {
                        for (int i = 0; i < SectieLijst.Items.Count; i++)
                        {
                            if (SectieLijst.Items[i].ToString() == a._sectie)
                            {
                                SectieLijst.SelectedIndex = i;
                                i = SectieLijst.Items.Count + 1;

                                Computerlijst_DropDown(this, null);
                            }
                        }

                        for (int i = 0; i < computerlijst.Items.Count; i++)
                        {
                            if (computerlijst.Items[i].ToString() == a._plaats)
                            {
                                computerlijst.SelectedIndex = i;
                                i = computerlijst.Items.Count + 1;
                            }
                        }
                    }
                }
                _ = StartButton.Focus();
            }
        }

        private void FormRdpKeuze_Shown(object sender, EventArgs e)
        {
            _ = textBoxZoek.Focus();
        }

        private void textBoxZoek_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                GaZoeken(this, null);
            }
        }

        private void multischerm_Click(object sender, EventArgs e)
        {
            if (LocatiePlaatst.Text != "")
            {
                // zoek server
                foreach (server a in DataRdp.Server_lijst)
                {
                    if (LocatiePlaatst.Text == a._adres)
                    {
                        a._multiscreen = multischerm.Checked;
                        DataRdp.Schrijf_server_lijst();
                        SchrijfDataIntextBox(a);
                    }
                }
            }
            else
            {
                multischerm.Checked = false;
                _ = MessageBox.Show("Kies eerst een server waarmee u contact mee wilt maken.");
            }
        }

        private void SchrijfDataIntextBox(server a)
        {
            if (a._domein != "VNC")
            {
                LocatiePlaatst.Text = a._adres;

                multischerm.Checked = a._multiscreen;

                //textBox.Text = File.ReadAllText("ronald.rdp", Encoding.ASCII);
                //textBoxTemp.Text = textBox.Text;
                //textBox.Clear();

                textBoxTemp.Text = File.ReadAllText("ronald.rdp", Encoding.ASCII);
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

                int pos = textBox.Text.IndexOf("use multimon:i:");
                if (pos != -1)
                {
                    textBox.SelectionStart = pos + 15;
                    textBox.SelectionLength = 1;

                    textBox.SelectedText = a._multiscreen ? "1" : "0";
                }
                textBox.SelectionStart = 1;
                textBox.ScrollToCaret();
            }
            else
            {
                // vnc
                LocatiePlaatst.Text = a._adres;// + " Met VNC, passwoord op klembord";
                vnclabel.Visible = true;
                vnc_adres = a._adres;

                // After this call, the data (string) is placed on the clipboard and tagged
                // with a data format of "Text".
                Clipboard.SetData(DataFormats.Text, a._usernaam);
            }
        }
    }
}
