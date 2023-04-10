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
            // laad ronald.rdp in textbox
            //DataRdp.Lees_server_lijst(); zit in updateUI
            textBox.Text = File.ReadAllText("ronald.rdp", Encoding.ASCII);
            UpdateUi();
        }

        // start button
        private void Button3_Click(object sender, EventArgs e)
        {
            if (!LocatiePlaatst.Text.Contains("VNC"))
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
            AutoCompleteStringCollection data = new AutoCompleteStringCollection();

            foreach (server a in DataRdp.Server_lijst)
            {
                if (!SectieLijst.Items.Contains(a._sectie))
                {
                    _ = SectieLijst.Items.Add(a._sectie);
                }
                _ = data.Add(a._plaats);
            }

            computerlijst.Items.Clear();
            computerlijst.SelectedIndex = -1;
            LocatiePlaatst.Text = "";

            textBoxZoek.AutoCompleteCustomSource = data;
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

            foreach (server a in DataRdp.Server_lijst)
            {
                if (a._plaats == computerlijst.Text && a._sectie == SectieLijst.Text)
                {
                    if (a._domein != "VNC")
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
                    else
                    {
                        // vnc
                        LocatiePlaatst.Text = a._adres + " Met VNC, passwoord op klembord";
                        vnc_adres = a._adres;

                        // After this call, the data (string) is placed on the clipboard and tagged
                        // with a data format of "Text".
                        Clipboard.SetData(DataFormats.Text, a._usernaam);

                    }
                }
            }
        }

        private void SectieLijst_SelectedIndexChanged(object sender, EventArgs e)
        {
            computerlijst.Items.Clear();
            computerlijst.SelectedIndex = -1;
            LocatiePlaatst.Text = "";
            textBoxZoek.Text = "";
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
            //if (textBoxZoek.Text.Length == 0)
            //{
            //    SectieLijst.SelectedIndex = -1;
            //    computerlijst.SelectedIndex = -1;
            //}
        }

        private void GaZoeken(object sender, EventArgs e)
        {
            foreach (server a in DataRdp.Server_lijst)
            {
                //if (a._plaats == textBoxZoek.Text)
                if (a._plaats.ToLower().Contains(textBoxZoek.Text.ToLower()))
                {
                    SectieLijst.Text = a._sectie;

                    Computerlijst_DropDown(this, null);

                    for (int i = 0; i < computerlijst.Items.Count; i++)
                    {

                        if (computerlijst.Items[i].ToString() == a._plaats)
                        {
                            computerlijst.SelectedIndex = i;
                            break;
                        }
                    }
                    break;
                }
            }
        }

        private void textBoxZoek_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GaZoeken(this, null);
            }
        }

        private void textBoxZoek_Leave(object sender, EventArgs e)
        {
            if(textBoxZoek.Text.Length > 1)
                GaZoeken(this, null);
        }

        private void FormRdpKeuze_Shown(object sender, EventArgs e)
        {
            textBoxZoek.Focus();
        }
    }
}
