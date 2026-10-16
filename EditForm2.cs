using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Windows.Forms;

namespace RDPKeuze
{
    // Invoervenster voor één server: gebruikt door EditForm bij Nieuw/Bewerken.
    // Bevat het importeren van een bestaand .rdp-bestand (leest adres/gebruikersnaam uit)
    // en een knop om via nameserver.txt + ping de juiste domein-suffix te vinden.
    public partial class EditForm2 : Form
    {
        public EditForm2()
        {
            InitializeComponent();
        }

        // Import: kies een .rdp-bestand en haal daar de servergegevens uit.
        // textBox2 krijgt de bestandsnaam, textBox3 het adres (full address:s:) en
        // textBox4/comboBoxDomein de gebruikersnaam (username:s:, gesplitst op "\").
        private void ButImportRdp_Click(object sender, EventArgs e)
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

            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = "";
            textBox2.Text = Path.GetFileNameWithoutExtension(open.FileName);

            // zoek nu items en zet in textboxén
            for (int i = 0; i < textBoxImport.Lines.Count(); i++)
            {
                string regel = textBoxImport.Lines[i].Trim();

                if (regel.Length > 15 && regel.Substring(0, 15) == "full address:s:")
                {
                    textBox3.Text = regel.Substring(15);
                }

                if (regel.Length > 11 && regel.Substring(0, 11) == "username:s:")
                {
                    string user = regel.Substring(11);

                    int a = user.IndexOf("\\");
                    if (a > -1)
                    {
                        comboBoxDomein.Text = user.Substring(0, a);
                        textBox4.Text = user.Substring(a + 1);
                    }
                    else
                    {
                        textBox4.Text = user;
                    }
                }
            }
        }

        // Zoekt de domein-suffix bij de ingevulde servernaam: nameserver.txt staat per
        // twee regels (eerste = suffix, tweede = bijbehorend domein). Elke suffix wordt
        // met ping geprobeerd; de eerste die reageert wordt aangevuld in textBox3 en het
        // domein wordt in comboBoxDomein gezet.
        private void buttonNameServer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text) || textBox3.Text == "")
            {
                _ = MessageBox.Show("Vul server naam in!! zonder '.'");
                return;
            }

            try
            {
                List<string> nameservers = File.ReadAllLines(FormRdpKeuze.datapath + "nameserver.txt").ToList();

                for (int i = 0; i + 1 < nameservers.Count; i += 2)
                {
                    if (string.IsNullOrWhiteSpace(nameservers[i]))
                    {
                        continue;
                    }

                    if (TestEnMeld(textBox3, nameservers[i]))
                    {
                        textBox3.Text = textBox3.Text + nameservers[i];
                        comboBoxDomein.Text = nameservers[i + 1];
                        break;
                    }
                }

                _ = MessageBox.Show("Klaar zoeken");
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message);
            }
        }

        // Pingt servernaam+suffix en meldt het resultaat; true als het adres bestaat.
        private static bool TestEnMeld(System.Windows.Forms.TextBox server, string nameserver)
        {
            string text = server.Text + nameserver;
            _ = MessageBox.Show($"Ping naar {text}");
            if (PingHost(text))
            {
                _ = MessageBox.Show($"Gevonden, druk op Save.\n{text}");
                return true;
            }
            else
            {
                return false;
            }
        }

        // Pingt een hostnaam; false bij een fout of geen antwoord.
        private static bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
                return pingable;
            }
            finally
            {
                pinger?.Dispose();
            }
            return pingable;
        }
    }
}
