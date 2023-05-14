namespace RDPKeuze
{
    partial class ZoekEnSelect
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
            this.listBoxGevonden = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBoxGevonden
            // 
            this.listBoxGevonden.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxGevonden.FormattingEnabled = true;
            this.listBoxGevonden.ItemHeight = 18;
            this.listBoxGevonden.Location = new System.Drawing.Point(13, 13);
            this.listBoxGevonden.Name = "listBoxGevonden";
            this.listBoxGevonden.ScrollAlwaysVisible = true;
            this.listBoxGevonden.Size = new System.Drawing.Size(406, 418);
            this.listBoxGevonden.TabIndex = 0;
            this.listBoxGevonden.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listBoxGevonden_KeyPress);
            this.listBoxGevonden.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxGevonden_MouseDoubleClick);
            // 
            // ZoekEnSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 450);
            this.Controls.Add(this.listBoxGevonden);
            this.Name = "ZoekEnSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Zoek En Select";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListBox listBoxGevonden;
    }
}