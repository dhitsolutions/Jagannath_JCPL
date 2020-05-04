namespace RamdevSales
{
    partial class POSMultiProduct
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
            this.PnlMain = new System.Windows.Forms.Panel();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.LVFO = new System.Windows.Forms.ListView();
            this.btncancel = new System.Windows.Forms.Button();
            this.PnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlMain
            // 
            this.PnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this.PnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlMain.Controls.Add(this.textBox7);
            this.PnlMain.Controls.Add(this.LVFO);
            this.PnlMain.Controls.Add(this.btncancel);
            this.PnlMain.Location = new System.Drawing.Point(1, 0);
            this.PnlMain.Name = "PnlMain";
            this.PnlMain.Size = new System.Drawing.Size(368, 329);
            this.PnlMain.TabIndex = 0;
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.Highlight;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox7.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.ForeColor = System.Drawing.Color.White;
            this.textBox7.Location = new System.Drawing.Point(-1, -1);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(363, 31);
            this.textBox7.TabIndex = 222;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "Select Batch";
            // 
            // LVFO
            // 
            this.LVFO.BackColor = System.Drawing.SystemColors.Window;
            this.LVFO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LVFO.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LVFO.ForeColor = System.Drawing.Color.Navy;
            this.LVFO.FullRowSelect = true;
            this.LVFO.GridLines = true;
            this.LVFO.HideSelection = false;
            this.LVFO.Location = new System.Drawing.Point(10, 36);
            this.LVFO.MultiSelect = false;
            this.LVFO.Name = "LVFO";
            this.LVFO.Size = new System.Drawing.Size(352, 249);
            this.LVFO.TabIndex = 27;
            this.LVFO.UseCompatibleStateImageBehavior = false;
            this.LVFO.View = System.Windows.Forms.View.Details;
            this.LVFO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LVFO_KeyDown);
            this.LVFO.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LVFO_MouseDoubleClick);
            // 
            // btncancel
            // 
            this.btncancel.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btncancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncancel.ForeColor = System.Drawing.Color.White;
            this.btncancel.Location = new System.Drawing.Point(265, 291);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(97, 34);
            this.btncancel.TabIndex = 24;
            this.btncancel.Text = "&Close";
            this.btncancel.UseVisualStyleBackColor = false;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // POSMultiProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 330);
            this.Controls.Add(this.PnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "POSMultiProduct";
            this.Text = "POSMultiProduct";
            this.PnlMain.ResumeLayout(false);
            this.PnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PnlMain;
        private System.Windows.Forms.Button btncancel;
        internal System.Windows.Forms.ListView LVFO;
        internal System.Windows.Forms.TextBox textBox7;
    }
}