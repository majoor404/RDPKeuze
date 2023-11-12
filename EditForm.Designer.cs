namespace RDPKeuze
{
    partial class EditForm
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
            this.label6 = new System.Windows.Forms.Label();
            this.ListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.ButEdit2 = new System.Windows.Forms.Button();
            this.ButNew2 = new System.Windows.Forms.Button();
            this.ButDel2 = new System.Windows.Forms.Button();
            this.ButClose2 = new System.Windows.Forms.Button();
            this.ButCopy2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(647, 763);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(250, 18);
            this.label6.TabIndex = 17;
            this.label6.Text = "ronald.majoor@tatasteeleurope.com";
            // 
            // ListView
            // 
            this.ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.ListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListView.FullRowSelect = true;
            this.ListView.GridLines = true;
            this.ListView.HideSelection = false;
            this.ListView.Location = new System.Drawing.Point(16, 12);
            this.ListView.Name = "ListView";
            this.ListView.Size = new System.Drawing.Size(893, 653);
            this.ListView.TabIndex = 15;
            this.ListView.UseCompatibleStateImageBehavior = false;
            this.ListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Adres";
            this.columnHeader1.Width = 280;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Type";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Plaats";
            this.columnHeader3.Width = 280;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Sectie";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Usernaam";
            this.columnHeader5.Width = 150;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(16, 683);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 46);
            this.button1.TabIndex = 20;
            this.button1.Text = "Refresh / Sorteer";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonSorteer_Click);
            // 
            // ButEdit2
            // 
            this.ButEdit2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButEdit2.Location = new System.Drawing.Point(16, 735);
            this.ButEdit2.Name = "ButEdit2";
            this.ButEdit2.Size = new System.Drawing.Size(137, 46);
            this.ButEdit2.TabIndex = 24;
            this.ButEdit2.Text = "Edit";
            this.ButEdit2.UseVisualStyleBackColor = true;
            this.ButEdit2.Click += new System.EventHandler(this.ButEdit2_Click);
            // 
            // ButNew2
            // 
            this.ButNew2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButNew2.Location = new System.Drawing.Point(191, 683);
            this.ButNew2.Name = "ButNew2";
            this.ButNew2.Size = new System.Drawing.Size(137, 46);
            this.ButNew2.TabIndex = 24;
            this.ButNew2.Text = "Nieuw";
            this.ButNew2.UseVisualStyleBackColor = true;
            this.ButNew2.Click += new System.EventHandler(this.ButNew2_Click);
            // 
            // ButDel2
            // 
            this.ButDel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButDel2.Location = new System.Drawing.Point(191, 735);
            this.ButDel2.Name = "ButDel2";
            this.ButDel2.Size = new System.Drawing.Size(137, 46);
            this.ButDel2.TabIndex = 24;
            this.ButDel2.Text = "Delete";
            this.ButDel2.UseVisualStyleBackColor = true;
            this.ButDel2.Click += new System.EventHandler(this.buttonDel_Click);
            // 
            // ButClose2
            // 
            this.ButClose2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButClose2.Location = new System.Drawing.Point(772, 683);
            this.ButClose2.Name = "ButClose2";
            this.ButClose2.Size = new System.Drawing.Size(137, 46);
            this.ButClose2.TabIndex = 24;
            this.ButClose2.Text = "Close";
            this.ButClose2.UseVisualStyleBackColor = true;
            this.ButClose2.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // ButCopy2
            // 
            this.ButCopy2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButCopy2.Location = new System.Drawing.Point(366, 732);
            this.ButCopy2.Name = "ButCopy2";
            this.ButCopy2.Size = new System.Drawing.Size(137, 46);
            this.ButCopy2.TabIndex = 24;
            this.ButCopy2.Text = "Copy";
            this.ButCopy2.UseVisualStyleBackColor = true;
            this.ButCopy2.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 790);
            this.Controls.Add(this.ButCopy2);
            this.Controls.Add(this.ButClose2);
            this.Controls.Add(this.ButDel2);
            this.Controls.Add(this.ButNew2);
            this.Controls.Add(this.ButEdit2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ListView);
            this.Name = "EditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Form";
            this.Shown += new System.EventHandler(this.EditForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView ListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button ButEdit2;
        private System.Windows.Forms.Button ButNew2;
        private System.Windows.Forms.Button ButDel2;
        private System.Windows.Forms.Button ButClose2;
        private System.Windows.Forms.Button ButCopy2;
    }
}