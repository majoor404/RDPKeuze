﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RDPKeuze
{
    public partial class WachtWoordEdit : Form
    {
        public WachtWoordEdit()
        {
            InitializeComponent();
        }

        private void WachtWoordEdit_Shown(object sender, EventArgs e)
        {
            buttonCancel.Focus();
        }
    }
}
