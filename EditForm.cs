using System;
using System.Windows.Forms;


namespace RDPKeuze
{
    public partial class EditForm : Form
    {
        private int index = -1;
        public bool _change = false;

        public EditForm()
        {
            InitializeComponent();
        }

        private void EditForm_Shown(object sender, EventArgs e)
        {
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
                ListView.Items.Add(itm);


                //string b = $"{a._adres,-50}{a._domein,-30}{a._plaats,-50}{a._sectie,-30}{a._usernaam,-30}";
                //string[] row = { $"{a._adres,-40}{a._domein,-10}{a._plaats,-40}{a._sectie,-5}{a._usernaam,-30}" };
                //ListViewItem listItem = new ListViewItem(row);
                //ListView.Items.Add(listItem);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void VoegToeBut_Click(object sender, EventArgs e)
        {
            if (ListView.SelectedItems.Count == 0)
            {
                server a = new server(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);
                DataRdp.Server_lijst.Add(a);
                EditForm_Shown(this, null);
                _change = true;
            }
            else
            {
                MessageBox.Show("selecteer geen regel");
            }
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
            }
            else
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (ListView.SelectedItems.Count > 0)
            {
                DataRdp.Server_lijst[index]._adres = textBox3.Text;
                DataRdp.Server_lijst[index]._domein = textBox5.Text;
                DataRdp.Server_lijst[index]._plaats = textBox2.Text;
                DataRdp.Server_lijst[index]._sectie = textBox1.Text;
                DataRdp.Server_lijst[index]._usernaam = textBox4.Text;
                EditForm_Shown(this, null);
                _change = true;
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
                    EditForm_Shown(this, null);
                    _change = true;
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
                _change = true;
            }
            else
            {
                MessageBox.Show("selecteer een regel");
            }
        }
    }
}
