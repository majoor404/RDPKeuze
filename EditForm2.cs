using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RDPKeuze
{
    public partial class EditForm2 : Form
    {
        public EditForm2()
        {
            InitializeComponent();
        }

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

        private void buttonNameServer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text) || textBox3.Text == "")
            {
                _ = MessageBox.Show("Vul server naam in!! zonder '.'");
                return;
            }

            try
            {
                List<string> nameservers = File.ReadAllLines("nameserver.txt").ToList();

                for (int i = 0; i < nameservers.Count; i++)
                {
                    if (TestEnMeld(textBox3, nameservers[i]))
                    {
                        textBox3.Text = textBox3.Text + nameservers[i];
                        break;
                    }
                }

                MessageBox.Show("Klaar zoeken");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static bool TestEnMeld(System.Windows.Forms.TextBox server, string nameserver)
        {
            string text = server.Text + nameserver;
            MessageBox.Show($"Ping naar {text}");
            if (PingHost(text))
            {
                MessageBox.Show("Klaar zoeken, druk op Save.");
                return true;
            }
            else
            {
                return false;
            }
        }

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
            }
            finally
            {
                pinger?.Dispose();
            }

            return pingable;
        }
    }
}
