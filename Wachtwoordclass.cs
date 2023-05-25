using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RDPKeuze
{
    public partial class WachtWoordForm : Form
    {
        public static List<wachtwoord> wachtwoord_lijst = new List<wachtwoord>();

        public WachtWoordForm()
        {
            InitializeComponent();
        }

        private void WachtWoordForm_Shown(object sender, System.EventArgs e)
        {
            LaadZienOpScherm();
        }

        private void LaadZienOpScherm()
        {
            Laad();
            ListView.Items.Clear();
            string[] arr = new string[3];
            ListViewItem itm;
            foreach (wachtwoord a in wachtwoord_lijst)
            {
                arr[0] = a._naam;
                arr[1] = a._user;
                arr[2] = a._ww;

                itm = new ListViewItem(arr);
                _ = ListView.Items.Add(itm);
            }
        }

        private void Laad()
        {
            try
            {
                wachtwoord_lijst.Clear();
                string xmlTekst = File.ReadAllText("ww.xml");
                wachtwoord_lijst = FromXML<List<wachtwoord>>(xmlTekst);
            }
            catch { }

            // unscrammble
            foreach (wachtwoord wachtwoord_ in wachtwoord_lijst)
            {
                wachtwoord_._ww = Unscramble(wachtwoord_._ww);
            }
        }

        private void Save()
        {
            // scrammble
            foreach (wachtwoord wachtwoord_ in wachtwoord_lijst)
            {
                wachtwoord_._ww = Scramble(wachtwoord_._ww);
            }

            try
            {
                string xmlTekst = ToXML(wachtwoord_lijst);
                File.WriteAllText("ww.xml", xmlTekst);
            }
            catch { }
        }

        public static string ToXML<T>(T obj)
        {
            using (StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }

        public static T FromXML<T>(string xml)
        {
            using (StringReader stringReader = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stringReader);
            }
        }

        private void buttonClose_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        public static string Scramble(string woord)
        {
            // string test = unscramble("twtvfw8;");
            string ret = "";
            for (int h = 0; h < woord.Length; h++)
            {
                int xx22 = 0;
                xx22 += 2;
                xx22 += woord.Length - h - 1;
                //xx22 += xx22 = (int)woord[h];
                xx22 += woord[h];
                char character = (char)xx22;
                ret += character.ToString();
            }
            return ret;
        }

        public static string Unscramble(string woord)
        {
            string ret = "";
            if (woord == null || woord.Length < 1)
            {
                return ret;
            }

            for (int h = 0; h < woord.Length; h++)
            {
                int xx22;// = 0;
                xx22 = woord[h];
                xx22 -= 2;
                xx22 -= woord.Length - h - 1;
                char character = (char)xx22;
                ret += character.ToString();
            }
            return ret;
        }

        private void ListView_DoubleClick(object sender, System.EventArgs e)
        {
            if (ListView.SelectedItems.Count > 0)
            {
                int index = ListView.Items.IndexOf(ListView.SelectedItems[0]);
                WachtWoordEdit ed = new WachtWoordEdit();
                ed.textBox1.Text = wachtwoord_lijst[index]._naam;
                ed.textBox2.Text = wachtwoord_lijst[index]._user;
                ed.textBox3.Text = wachtwoord_lijst[index]._ww;
                DialogResult a =  ed.ShowDialog();
                if(a == DialogResult.OK)
                {
                    wachtwoord_lijst[index]._naam = ed.textBox1.Text;
                    wachtwoord_lijst[index]._user = ed.textBox2.Text;
                    wachtwoord_lijst[index]._ww = ed.textBox3.Text;
                    Save();
                    LaadZienOpScherm();
                }
            }
        }

        private void buttonZoek_Click(object sender, System.EventArgs e)
        {
            if(textBoxZoek.Text.Length > 0)
            {
                Laad();
                ListView.Items.Clear();
                string[] arr = new string[3];
                ListViewItem itm;
                foreach (wachtwoord a in wachtwoord_lijst)
                {
                    if (a._naam.ToLower().Contains(textBoxZoek.Text.ToLower()))
                    {
                        arr[0] = a._naam;
                        arr[1] = a._user;
                        arr[2] = a._ww;

                        itm = new ListViewItem(arr);
                        _ = ListView.Items.Add(itm);
                    }
                }
            }
        }
    }
}
