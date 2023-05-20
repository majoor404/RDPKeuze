namespace RDPKeuze
{
    partial class FormRdpKeuze
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRdpKeuze));
            this.textBox = new System.Windows.Forms.TextBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.textBoxTemp = new System.Windows.Forms.TextBox();
            this.SectieLijst = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.EditLijst = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.computerlijst = new System.Windows.Forms.ComboBox();
            this.LocatiePlaatst = new System.Windows.Forms.Label();
            this.buttonReload = new System.Windows.Forms.Button();
            this.linkLabelGitHub = new System.Windows.Forms.LinkLabel();
            this.textBoxZoek = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.multischerm = new System.Windows.Forms.CheckBox();
            this.vnclabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox.Location = new System.Drawing.Point(243, 12);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(745, 606);
            this.textBox.TabIndex = 0;
            this.textBox.Visible = false;
            // 
            // StartButton
            // 
            this.StartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartButton.Location = new System.Drawing.Point(12, 198);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(378, 90);
            this.StartButton.TabIndex = 3;
            this.StartButton.Text = "Maak Contact";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.Button3_Click);
            // 
            // textBoxTemp
            // 
            this.textBoxTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTemp.Location = new System.Drawing.Point(611, 424);
            this.textBoxTemp.Multiline = true;
            this.textBoxTemp.Name = "textBoxTemp";
            this.textBoxTemp.Size = new System.Drawing.Size(363, 179);
            this.textBoxTemp.TabIndex = 4;
            this.textBoxTemp.Visible = false;
            // 
            // SectieLijst
            // 
            this.SectieLijst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SectieLijst.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SectieLijst.FormattingEnabled = true;
            this.SectieLijst.Location = new System.Drawing.Point(12, 28);
            this.SectieLijst.Name = "SectieLijst";
            this.SectieLijst.Size = new System.Drawing.Size(378, 26);
            this.SectieLijst.Sorted = true;
            this.SectieLijst.TabIndex = 5;
            this.SectieLijst.SelectedIndexChanged += new System.EventHandler(this.SectieLijst_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 18);
            this.label1.TabIndex = 6;
            this.label1.Text = "Sectie";
            // 
            // EditLijst
            // 
            this.EditLijst.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EditLijst.Location = new System.Drawing.Point(12, 346);
            this.EditLijst.Name = "EditLijst";
            this.EditLijst.Size = new System.Drawing.Size(378, 43);
            this.EditLijst.TabIndex = 7;
            this.EditLijst.Text = "Edit";
            this.EditLijst.UseVisualStyleBackColor = true;
            this.EditLijst.Click += new System.EventHandler(this.EditLijst_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "Computer";
            // 
            // computerlijst
            // 
            this.computerlijst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.computerlijst.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.computerlijst.FormattingEnabled = true;
            this.computerlijst.Location = new System.Drawing.Point(12, 77);
            this.computerlijst.Name = "computerlijst";
            this.computerlijst.Size = new System.Drawing.Size(379, 26);
            this.computerlijst.Sorted = true;
            this.computerlijst.TabIndex = 9;
            this.computerlijst.DropDown += new System.EventHandler(this.Computerlijst_DropDown);
            this.computerlijst.SelectedIndexChanged += new System.EventHandler(this.Computerlijst_SelectedIndexChanged);
            // 
            // LocatiePlaatst
            // 
            this.LocatiePlaatst.AutoSize = true;
            this.LocatiePlaatst.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LocatiePlaatst.Location = new System.Drawing.Point(12, 106);
            this.LocatiePlaatst.Name = "LocatiePlaatst";
            this.LocatiePlaatst.Size = new System.Drawing.Size(139, 18);
            this.LocatiePlaatst.TabIndex = 11;
            this.LocatiePlaatst.Text = "plaats holder locatie";
            // 
            // buttonReload
            // 
            this.buttonReload.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReload.Location = new System.Drawing.Point(12, 309);
            this.buttonReload.Name = "buttonReload";
            this.buttonReload.Size = new System.Drawing.Size(376, 31);
            this.buttonReload.TabIndex = 12;
            this.buttonReload.Text = "Reload";
            this.buttonReload.UseVisualStyleBackColor = true;
            this.buttonReload.Visible = false;
            this.buttonReload.Click += new System.EventHandler(this.buttonReload_Click);
            // 
            // linkLabelGitHub
            // 
            this.linkLabelGitHub.AutoSize = true;
            this.linkLabelGitHub.Location = new System.Drawing.Point(94, 408);
            this.linkLabelGitHub.Name = "linkLabelGitHub";
            this.linkLabelGitHub.Size = new System.Drawing.Size(207, 13);
            this.linkLabelGitHub.TabIndex = 14;
            this.linkLabelGitHub.TabStop = true;
            this.linkLabelGitHub.Text = "https://github.com/majoor404/RDPKeuze";
            this.linkLabelGitHub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkGitHub);
            // 
            // textBoxZoek
            // 
            this.textBoxZoek.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxZoek.Location = new System.Drawing.Point(12, 166);
            this.textBoxZoek.Name = "textBoxZoek";
            this.textBoxZoek.Size = new System.Drawing.Size(376, 24);
            this.textBoxZoek.TabIndex = 15;
            this.textBoxZoek.Enter += new System.EventHandler(this.textBoxZoek_Enter);
            this.textBoxZoek.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxZoek_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 18);
            this.label3.TabIndex = 8;
            this.label3.Text = "Zoek";
            // 
            // multischerm
            // 
            this.multischerm.AutoSize = true;
            this.multischerm.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.multischerm.Location = new System.Drawing.Point(32, 294);
            this.multischerm.Name = "multischerm";
            this.multischerm.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.multischerm.Size = new System.Drawing.Size(356, 22);
            this.multischerm.TabIndex = 16;
            this.multischerm.Text = "Alle beeldschermen voor externe sessie gebruiken";
            this.multischerm.UseVisualStyleBackColor = true;
            this.multischerm.Click += new System.EventHandler(this.multischerm_Click);
            // 
            // vnclabel
            // 
            this.vnclabel.AutoSize = true;
            this.vnclabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vnclabel.Location = new System.Drawing.Point(157, 106);
            this.vnclabel.Name = "vnclabel";
            this.vnclabel.Size = new System.Drawing.Size(237, 18);
            this.vnclabel.TabIndex = 11;
            this.vnclabel.Text = "Met VNC, passwoord op klembord";
            this.vnclabel.Visible = false;
            // 
            // FormRdpKeuze
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 430);
            this.Controls.Add(this.multischerm);
            this.Controls.Add(this.textBoxZoek);
            this.Controls.Add(this.linkLabelGitHub);
            this.Controls.Add(this.buttonReload);
            this.Controls.Add(this.vnclabel);
            this.Controls.Add(this.LocatiePlaatst);
            this.Controls.Add(this.computerlijst);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.EditLijst);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SectieLijst);
            this.Controls.Add(this.textBoxTemp);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.textBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormRdpKeuze";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RDP Keuze";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.FormRdpKeuze_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.TextBox textBoxTemp;
        private System.Windows.Forms.ComboBox SectieLijst;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button EditLijst;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox computerlijst;
        private System.Windows.Forms.Label LocatiePlaatst;
        private System.Windows.Forms.Button buttonReload;
        private System.Windows.Forms.LinkLabel linkLabelGitHub;
        private System.Windows.Forms.TextBox textBoxZoek;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox multischerm;
        private System.Windows.Forms.Label vnclabel;
    }
}

