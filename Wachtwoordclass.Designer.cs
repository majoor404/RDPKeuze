namespace RDPKeuze
{
    partial class WachtWoordForm
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
            this.buttonClose = new System.Windows.Forms.Button();
            this.ListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textBoxZoek = new System.Windows.Forms.TextBox();
            this.buttonZoek = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClose.Location = new System.Drawing.Point(699, 481);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(189, 43);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // ListView
            // 
            this.ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.ListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListView.FullRowSelect = true;
            this.ListView.GridLines = true;
            this.ListView.HideSelection = false;
            this.ListView.Location = new System.Drawing.Point(12, 12);
            this.ListView.MultiSelect = false;
            this.ListView.Name = "ListView";
            this.ListView.Size = new System.Drawing.Size(876, 463);
            this.ListView.TabIndex = 16;
            this.ListView.UseCompatibleStateImageBehavior = false;
            this.ListView.View = System.Windows.Forms.View.Details;
            this.ListView.DoubleClick += new System.EventHandler(this.ListView_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Server";
            this.columnHeader1.Width = 280;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Usernaam";
            this.columnHeader2.Width = 280;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Passwoord";
            this.columnHeader3.Width = 280;
            // 
            // textBoxZoek
            // 
            this.textBoxZoek.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxZoek.Location = new System.Drawing.Point(12, 490);
            this.textBoxZoek.Name = "textBoxZoek";
            this.textBoxZoek.Size = new System.Drawing.Size(271, 24);
            this.textBoxZoek.TabIndex = 18;
            // 
            // buttonZoek
            // 
            this.buttonZoek.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZoek.Location = new System.Drawing.Point(299, 481);
            this.buttonZoek.Name = "buttonZoek";
            this.buttonZoek.Size = new System.Drawing.Size(189, 43);
            this.buttonZoek.TabIndex = 0;
            this.buttonZoek.Text = "Filter / Zoek";
            this.buttonZoek.UseVisualStyleBackColor = true;
            this.buttonZoek.Click += new System.EventHandler(this.buttonZoek_Click);
            // 
            // WachtWoordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 534);
            this.Controls.Add(this.textBoxZoek);
            this.Controls.Add(this.ListView);
            this.Controls.Add(this.buttonZoek);
            this.Controls.Add(this.buttonClose);
            this.Name = "WachtWoordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Wacht Woorden";
            this.Shown += new System.EventHandler(this.WachtWoordForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ListView ListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.TextBox textBoxZoek;
        private System.Windows.Forms.Button buttonZoek;
    }
}