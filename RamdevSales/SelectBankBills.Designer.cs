namespace RamdevSales
{
    partial class SelectBankBills
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
            this.LVFO = new System.Windows.Forms.ListView();
            this.btncontinue = new System.Windows.Forms.Button();
            this.btnback = new System.Windows.Forms.Button();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btncancel = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.LVFO.Location = new System.Drawing.Point(29, 32);
            this.LVFO.MultiSelect = false;
            this.LVFO.Name = "LVFO";
            this.LVFO.Size = new System.Drawing.Size(446, 255);
            this.LVFO.TabIndex = 2;
            this.LVFO.UseCompatibleStateImageBehavior = false;
            this.LVFO.View = System.Windows.Forms.View.Details;
            this.LVFO.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.LVFO_ItemChecked);
            // 
            // btncontinue
            // 
            this.btncontinue.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btncontinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncontinue.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncontinue.ForeColor = System.Drawing.Color.White;
            this.btncontinue.Location = new System.Drawing.Point(273, 293);
            this.btncontinue.Name = "btncontinue";
            this.btncontinue.Size = new System.Drawing.Size(97, 34);
            this.btncontinue.TabIndex = 20;
            this.btncontinue.Text = "&Continue";
            this.btncontinue.UseVisualStyleBackColor = false;
            this.btncontinue.Click += new System.EventHandler(this.btncontinue_Click);
            this.btncontinue.Enter += new System.EventHandler(this.btncontinue_Enter);
            this.btncontinue.Leave += new System.EventHandler(this.btncontinue_Leave);
            this.btncontinue.MouseEnter += new System.EventHandler(this.btncontinue_MouseEnter);
            this.btncontinue.MouseLeave += new System.EventHandler(this.btncontinue_MouseLeave);
            // 
            // btnback
            // 
            this.btnback.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnback.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnback.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnback.ForeColor = System.Drawing.Color.White;
            this.btnback.Location = new System.Drawing.Point(29, 293);
            this.btnback.Name = "btnback";
            this.btnback.Size = new System.Drawing.Size(97, 34);
            this.btnback.TabIndex = 23;
            this.btnback.Text = "&Back";
            this.btnback.UseVisualStyleBackColor = false;
            this.btnback.Click += new System.EventHandler(this.btnback_Click);
            this.btnback.Enter += new System.EventHandler(this.btnback_Enter);
            this.btnback.Leave += new System.EventHandler(this.btnback_Leave);
            this.btnback.MouseEnter += new System.EventHandler(this.btnback_MouseEnter);
            this.btnback.MouseLeave += new System.EventHandler(this.btnback_MouseLeave);
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.Highlight;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox7.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.ForeColor = System.Drawing.Color.White;
            this.textBox7.Location = new System.Drawing.Point(14, 0);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(478, 31);
            this.textBox7.TabIndex = 221;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "Select Bills";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btncancel);
            this.panel1.Location = new System.Drawing.Point(14, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(478, 303);
            this.panel1.TabIndex = 222;
            // 
            // btncancel
            // 
            this.btncancel.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btncancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncancel.ForeColor = System.Drawing.Color.White;
            this.btncancel.Location = new System.Drawing.Point(361, 261);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(97, 34);
            this.btncancel.TabIndex = 16;
            this.btncancel.Text = "&Close";
            this.btncancel.UseVisualStyleBackColor = false;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            this.btncancel.Enter += new System.EventHandler(this.btncancel_Enter);
            this.btncancel.Leave += new System.EventHandler(this.btncancel_Leave);
            this.btncancel.MouseEnter += new System.EventHandler(this.btncancel_MouseEnter);
            this.btncancel.MouseLeave += new System.EventHandler(this.btncancel_MouseLeave);
            // 
            // SelectBills
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(496, 340);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.btnback);
            this.Controls.Add(this.btncontinue);
            this.Controls.Add(this.LVFO);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SelectBills";
            this.Text = "SelectBills";
            this.Load += new System.EventHandler(this.SelectBills_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ListView LVFO;
        internal System.Windows.Forms.Button btncontinue;
        internal System.Windows.Forms.Button btnback;
        internal System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btncancel;

    }
}