using System;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace RDPKeuze
{
    // Bewerkvenster voor de serverlijst (data.xml).
    // Toont de servers in een ListView, eventueel gefilterd op sectie, en kan
    // verwijderen/kopiëren/bewerken/aanmaken. Elke wijziging wordt direct weggeschreven
    // met DataRdp.Schrijf_server_lijst(); de hoofdvorm leest het bestand daarna opnieuw.
    public partial class EditForm : Form
    {
        // Index in DataRdp.Server_lijst van de rij die in de ListView geselecteerd is;
        // -1 betekent "niet gevonden" (dan wordt er niets aangepast).
        private int index = -1;
        private static readonly Random random = new Random();

        public EditForm()
        {
            InitializeComponent();
            VulFilter();
        }

        // (Her)vult de ListView vanuit DataRdp.Server_lijst, alleen met de servers uit de
        // sectie die in CBFilter gekozen is (leeg = alles). Wordt ook als verversing
        // aangeroepen na verwijderen/bewerken/kopiëren en bij een filterwijziging.
        private void EditForm_Shown(object sender, EventArgs e)
        {
            //textBoxImport.Visible = false;
            ListView.Items.Clear();
            string[] arr = new string[5];
            ListViewItem itm;
            foreach (server a in DataRdp.Server_lijst)
            {
                if (CBFilter.Text == "" || CBFilter.Text == a._sectie)
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
        }

        // Vult de sectiekeuze (filter) met alle unieke secties uit de serverlijst.
        private void VulFilter()
        {
            CBFilter.Items.Clear();
            CBFilter.Items.Add("");
            foreach (server a in DataRdp.Server_lijst)
            {
                if (!CBFilter.Items.Contains(a._sectie))
                    CBFilter.Items.Add($"{a._sectie}");
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Verwijderen: alleen doorgaan bij een selectie, daarna vragen om bevestiging,
        // de server uit de lijst halen, data.xml wegschrijven en de ListView verversen.
        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (ListView.SelectedItems.Count == 0)
            {
                return;
            }

            DialogResult dialogResult = MessageBox.Show("verwijder ?", "Verwijder deze server", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                index = GetMainIndex(ListView.Items.IndexOf(ListView.SelectedItems[0]));
                if (index < 0)
                {
                    return;
                }
                DataRdp.Server_lijst.RemoveAt(index);
                DataRdp.Schrijf_server_lijst();
                EditForm_Shown(this, null);
            }
        }


        // Sorteren op sectie en wegschrijven (Schrijf_server_lijst sorteert zelf ook);
        // alleen sorteren zonder schrijven zou na sluiten weer ongedaan gemaakt worden.
        public void buttonSorteer_Click(object sender, EventArgs e)
        {
            DataRdp.Schrijf_server_lijst();
            EditForm_Shown(this, null);
        }

        // Kopieert de geselecteerde server met een random suffix op de plaatsnaam
        // (bijv. "PC01 A1B2") zodat je een variant kunt aanmaken zonder het origineel
        // te raken; multischerm wordt bij de kopie altijd uitgezet.
        private void buttonCopy_Click(object sender, EventArgs e)
        {
            if (ListView.SelectedItems.Count > 0)
            {
                index = GetMainIndex(ListView.Items.IndexOf(ListView.SelectedItems[0]));
                if (index < 0)
                {
                    return;
                }
                server a = new server(DataRdp.Server_lijst[index]._sectie, DataRdp.Server_lijst[index]._plaats + " " + RandomString(4), DataRdp.Server_lijst[index]._adres, DataRdp.Server_lijst[index]._usernaam, DataRdp.Server_lijst[index]._domein, false);
                DataRdp.Server_lijst.Add(a);
                DataRdp.Schrijf_server_lijst();
                EditForm_Shown(this, null);
            }
            else
            {
                _ = MessageBox.Show("selecteer een regel");
            }
        }

        // Zet de geselecteerde ListView-rij om naar het bijbehorende index in
        // DataRdp.Server_lijst. De ListView kan gefilterd/gesorteerd zijn, dus er wordt
        // gematcht op de zichtbare velden (adres, plaats, sectie, usernaam).
        // -1 terug als de rij niet in de lijst staat; de aanroepers stoppen dan.
        private int GetMainIndex(int GesorteerdeIndex)
        {
            int index = 0;

            string adres = ListView.Items[GesorteerdeIndex].SubItems[0].Text;
            string sectie = ListView.Items[GesorteerdeIndex].SubItems[3].Text;
            string plaats = ListView.Items[GesorteerdeIndex].SubItems[2].Text;
            string user = ListView.Items[GesorteerdeIndex].SubItems[4].Text;

            foreach (server a in DataRdp.Server_lijst)
            {
                if (a._adres == adres && a._plaats == plaats && a._sectie == sectie && a._usernaam == user)
                    return index;
                index++;
            }

            return -1;
        }
        // Bewerken: opent EditForm2 met de waarden van de geselecteerde server. Bij OK
        // wordt alles teruggeschreven (sectie is verplicht), wordt _domein opnieuw als
        // type-aanduiding ("RDP", of "VNC" als de toggle aan staat) gezet en data.xml
        // weggeschreven.
        private void ButEdit2_Click(object sender, EventArgs e)
        {
            if (ListView.SelectedItems.Count > 0)
            {
                EditForm2 ef = new EditForm2();

                index = GetMainIndex(ListView.Items.IndexOf(ListView.SelectedItems[0]));
                if (index < 0)
                {
                    return;
                }

                //index = ListView.Items.IndexOf(ListView.SelectedItems[0]);
                ef.textBox3.Text = DataRdp.Server_lijst[index]._adres;
                ef.textBox2.Text = DataRdp.Server_lijst[index]._plaats;
                ef.textBox1.Text = DataRdp.Server_lijst[index]._sectie;

                int a = DataRdp.Server_lijst[index]._usernaam.IndexOf("\\");
                if (a > -1)
                {
                    ef.comboBoxDomein.Text = DataRdp.Server_lijst[index]._usernaam.Substring(0, a);
                    ef.textBox4.Text = DataRdp.Server_lijst[index]._usernaam.Substring(a + 1);
                }
                else
                {
                    ef.textBox4.Text = DataRdp.Server_lijst[index]._usernaam;
                }

                ef.VncToggle.Checked = DataRdp.Server_lijst[index]._domein == "VNC";

                DialogResult dialogResult = ef.ShowDialog();

                if (dialogResult == DialogResult.OK)
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

                        if (ef.comboBoxDomein.Text != "")
                        {
                            DataRdp.Server_lijst[index]._usernaam = $"{ef.comboBoxDomein.Text}\\{ef.textBox4.Text}";
                        }
                        else
                        {
                            DataRdp.Server_lijst[index]._usernaam = ef.textBox4.Text;
                        }

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

        // Nieuw: opent een leeg EditForm2; bij OK wordt de server toegevoegd en opgeslagen.
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

        // Filter gewijzigd → ListView opnieuw vullen met de nieuwe selectie.
        private void CBFilter_TextChanged(object sender, EventArgs e)
        {
            EditForm_Shown(this, null);
        }


        // Random tekst van letters/cijfers, gebruikt als suffix voor een gekopieerde server.
        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
