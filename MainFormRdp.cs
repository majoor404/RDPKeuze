using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RDPKeuze
{
    // Hoofdvenster van RDPKeuze.
    // Werkwijze: kies sectie -> computer -> (eventueel) andere gebruiker; daarna bouwt
    // SchrijfDataIntextBox de volledige .rdp-tekst op in de tekstbox, zet het bijbehorende
    // wachtwoord op het klembord en start de Start-knop mstsc (of vncviewer bij VNC).
    // Alle data-bestanden staan naast de exe (datapath), niet in de projectmap.
    public partial class FormRdpKeuze : Form
    {
        private string vnc_adres = "";
        public static string datapath = "";

        public FormRdpKeuze()
        {
            InitializeComponent();
            // datapath = map waar de exe in draait; zo worden data.xml, ww.xml en
            // ronald.rdp altijd gevonden, ongeacht vanaf welke map de app gestart is.
            datapath = AppDomain.CurrentDomain.BaseDirectory;
        }

        // Leest het RDP-templatebestand (ronald.rdp) dat als basis dient voor nieuwe
        // verbindingen. Bestand ontbreekt of is onleesbaar → geeft "" terug in plaats
        // van een crash, zodat de app ook op een verse installatie opstart.
        private static string LeesRdpTemplate()
        {
            try
            {
                return File.ReadAllText(datapath + "ronald.rdp", Encoding.ASCII);
            }
            catch { }
            return "";
        }

        // Wordt eenmaal uitgevoerd als het venster voor het eerst getoond wordt:
        // template inladen, UI vullen (UpdateUi leest data.xml) en wachtwoorden inladen.
        private void Form1_Load(object sender, EventArgs e)
        {
            //laad ronald.rdp in textbox
            //DataRdp.Lees_server_lijst(); zit in updateUI
            textBox.Text = LeesRdpTemplate();
            UpdateUi();

            ZoekWW.LaadWachtwoorden();
        }

        // Start button: schrijft de huidige tekst uit de tekstbox weg naar ronald.rdp en
        // start dat bestand (Windows opent het met mstsc). Is het VNC-label zichtbaar,
        // dan wordt in plaats daarvan vncviewer.exe met het VNC-adres gestart.
        private void Button3_Click(object sender, EventArgs e)
        {
            string gekozen_sectie = SectieLijst.Text;

            if (!vnclabel.Visible)
            {
                // save naar ronald.rdp
                File.WriteAllText(datapath + "ronald.rdp", textBox.Text);
                // run ...
                _ = Process.Start(datapath + "ronald.rdp");
            }
            else if (File.Exists(datapath + "vncviewer.exe"))
            {
                Process process = new Process
                {
                    StartInfo =
              {
                  FileName = datapath + "vncviewer.exe",
                  Arguments = vnc_adres
              }
                };
                _ = process.Start();
            }
            else
            {
                _ = MessageBox.Show("vncviewer.exe niet gevonden in de programmamap.");
            }
            UpdateUi();
            SectieLijst.Text = gekozen_sectie;
        }

        // Opent het bewerkvenster (EditForm) voor de serverlijst en vernieuwt daarna de UI.
        private void EditLijst_Click(object sender, EventArgs e)
        {
            DataRdp.Lees_server_lijst();
            EditForm Edit = new EditForm();
            _ = Edit.ShowDialog();

            UpdateUi();
        }

        // Centrale verversing: leest data.xml opnieuw en vult de sectie-/computer-/
        // gebruikerslijsten. Roept dit overal na wijzigingen aan, dan klopt de UI altijd.
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
            UserLijst.Items.Clear();
            UserLijst.SelectedIndex = -1;
            LocatiePlaatst.Text = "";
        }

        // Vult de computerlijst met de plaatsnamen die bij de gekozen sectie horen.
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

        // Er is een computer gekozen: zet de gebruikersopties klaar (standaard de
        // usernaam van de server, aangevuld met de namen uit UserNaam.txt) en bouw
        // direct de .rdp-tekst op via SchrijfDataIntextBox.
        private void Computerlijst_SelectedIndexChanged(object sender, EventArgs e)
        {
            LocatiePlaatst.Text = "";
            vnclabel.Visible = false;
            multischerm.Checked = false;

            foreach (server a in DataRdp.Server_lijst)
            {
                if (a._plaats == computerlijst.Text && a._sectie == SectieLijst.Text)
                {
                    string standaard = $"{ a._usernaam}";
                    UserLijst.Items.Clear();
                    UserLijst.Items.Add(standaard);
                    try
                    {
                        List<string> GebruikersNamen = File.ReadAllLines(datapath + "UserNaam.txt").ToList();
                        for (int i = 0;i < GebruikersNamen.Count;i++)
                        {
                            if(!UserLijst.Items.Contains(GebruikersNamen[i]))
                                UserLijst.Items.Add(GebruikersNamen[i]);
                        }
                    } catch { } 
                   UserLijst.SelectedIndex = 0;
                    SchrijfDataIntextBox(a);
                }
            }
        }

        // Er is een andere gebruiker gekozen dan de standaard: pas de serverlijst aan
        // en bouw de .rdp-tekst opnieuw op. Slaat nog niets op; dat gebeurt pas als
        // ergens Schrijf_server_lijst() aangeroepen wordt.
        private void UserLijst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UserLijst.SelectedIndex == 0)
                return;

            foreach (server a in DataRdp.Server_lijst)
            {
                if (a._plaats == computerlijst.Text && a._sectie == SectieLijst.Text)
                {
                    // andere usernaam gekozen
                    a._usernaam = UserLijst.Text;
                    SchrijfDataIntextBox(a);
                }
            }
        }

        // Sectie gewijzigd: alle keuzes die daarvan afhangen (computer, gebruiker,
        // adres, VNC-label, zoekveld) weer leegmaken zodat er niets verkeerds blijft staan.
        private void SectieLijst_SelectedIndexChanged(object sender, EventArgs e)
        {
            computerlijst.Items.Clear();
            computerlijst.SelectedIndex = -1;
            UserLijst.Items.Clear();
            UserLijst.SelectedIndex = -1;
            LocatiePlaatst.Text = "";
            textBoxZoek.Text = "";
            vnclabel.Visible = false;
        }

        // Reload-knop: data.xml opnieuw lezen en de UI verversen.
        private void buttonReload_Click(object sender, EventArgs e)
        {
            UpdateUi();
        }

        // Opent de GitHub-pagina van het project in de browser.
        private void linkLabel1_LinkGitHub(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _ = System.Diagnostics.Process.Start("https://github.com/majoor404/RDPKeuze");
        }

        // Zodra het zoekveld actief wordt eerst alles verversen, zodat er in een
        // actuele serverlijst gezocht wordt.
        private void textBoxZoek_Enter(object sender, EventArgs e)
        {
            UpdateUi();
        }

        // Zoeken op plaatsnaam: toont de treffers in ZoekEnSelect en selecteert de
        // gekozen treffer daarna in de sectie-/computerlijsten, zodat alleen op
        // Start gedrukt hoeft te worden.
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

        // Na openen van het venster meteen in het zoekveld kunnen typen.
        private void FormRdpKeuze_Shown(object sender, EventArgs e)
        {
            _ = textBoxZoek.Focus();
        }

        // Enter in het zoekveld = hetzelfde als op de Zoek-knop drukken.
        private void textBoxZoek_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                GaZoeken(this, null);
            }
        }

        // Aan/uit-vinkje "multischerm": wijzigt use multimon:i: van de actieve server
        // en slaat data.xml direct op. Zonder actieve server wordt het vinkje teruggedraaid.
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

        // Bouwt de volledige .rdp-tekst in de tekstbox voor de gekozen server:
        //  - regel 1-3 vervangen door full address / username / domain,
        //  - de rest komt ongewijzigd uit het template (ronald.rdp),
        //  - use multimon:i: wordt aangepast aan de voorkeur van de server,
        //  - het wachtwoord van de gebruiker gaat naar het klembord (timer wist het).
        // Bij een VNC-server: alleen adres tonen, VNC-label zetten en het "wachtwoord"
        // (de usernaam) op het klembord plaatsen.
        private void SchrijfDataIntextBox(server a)
        {
            if (a._domein != "VNC")
            {
                LocatiePlaatst.Text = a._adres;

                multischerm.Checked = a._multiscreen;

                textBoxTemp.Text = LeesRdpTemplate();
                textBox.Clear();

                textBox.AppendText("full address:s:" + a._adres);
                textBox.AppendText(Environment.NewLine);

                textBox.AppendText("username:s:" + a._usernaam);
                textBox.AppendText(Environment.NewLine);

                textBox.AppendText("domain:s:");            // niet meer in gebruik.
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

                ZoekWW.GetWW(a._usernaam, out string ww);

                if (ww != "")
                {
                    // After this call, the data (string) is placed on the clipboard and tagged
                    // with a data format of "Text".
                    Clipboard.Clear();
                    Clipboard.SetData(DataFormats.Text, ww);
                    WWopClipboard.BackColor = System.Drawing.Color.LimeGreen;
                    timerResetWWopClipbord.Enabled = true; // na ? sec verwijderen van wachtwoord van klembord
                }
            }
            else
            {
                // vnc
                LocatiePlaatst.Text = a._adres;// + " Met VNC, passwoord op klembord";
                vnclabel.Visible = true;
                vnc_adres = a._adres;


                // After this call, the data (string) is placed on the clipboard and tagged
                // with a data format of "Text".
                Clipboard.Clear();
                Clipboard.SetData(DataFormats.Text, a._usernaam);
                timerResetWWopClipbord.Enabled = true; // na ? sec verwijderen van wachtwoord van klembord
            }
        }

        // Knop Wachtwoorden: opent het wachtwoordbeheer.
        // DEBUG-builds gaan er direct in; een Release-build moet eerst het vaste
        // master-wachtwoord in het tsbww-venster invoeren (dat hoort zo, niet weghalen).
        private void buttonWW_Click(object sender, EventArgs e)
        {
#if DEBUG
            WachtWoordForm wacht = new WachtWoordForm();
            wacht.ShowDialog(this);
#else
            tsbww testDialog = new tsbww();

            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (testDialog.ShowDialog(this) == DialogResult.OK)
            {
                if (testDialog.textBox1.Text == "Werkplaats4c14!!")
                {
                    testDialog.Dispose();
                    WachtWoordForm wacht = new WachtWoordForm();
                    wacht.ShowDialog(this);
                }
                else
                {
                    _ = MessageBox.Show("Verkeerd wachtwoord.");
                }

            }
            testDialog.Dispose();

#endif
        }

        // Timer afgebroken: wachtwoord van het klembord wissen en het groene lampje
        // terugzetten, zodat het wachtwoord niet onbeperkt op het klembord blijft staan.
        private void timerResetWWopClipbord_Tick(object sender, EventArgs e)
        {
            timerResetWWopClipbord.Enabled = false;
            Clipboard.Clear();
            WWopClipboard.BackColor = SystemColors.Control; // Control;// System.Drawing.Color.Gray;
        }
    }
}
