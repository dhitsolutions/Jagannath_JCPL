namespace RamdevSales
{
    partial class PurchaseSelectBills
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
            this.btnback = new System.Windows.Forms.Button();
            this.btncontinue = new System.Windows.Forms.Button();
            this.LVFO = new System.Windows.Forms.ListView();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnclose = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnback
            // 
            this.btnback.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnback.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnback.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnback.ForeColor = System.Drawing.Color.White;
            this.btnback.Location = new System.Drawing.Point(52, 344);
            this.btnback.Name = "btnback";
            this.btnback.Size = new System.Drawing.Size(97, 34);
            this.btnback.TabIndex = 26;
            this.btnback.Text = "&Back";
            this.btnback.UseVisualStyleBackColor = false;
            this.btnback.Click += new System.EventHandler(this.btnback_Click);
            this.btnback.Enter += new System.EventHandler(this.btnback_Enter);
            this.btnback.Leave += new System.EventHandler(this.btnback_Leave);
            this.btnback.MouseEnter += new System.EventHandler(this.btnback_MouseEnter);
            this.btnback.MouseLeave += new System.EventHandler(this.btnback_MouseLeave);
            // 
            // btncontinue
            // 
            this.btncontinue.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btncontinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncontinue.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncontinue.ForeColor = System.Drawing.Color.White;
            this.btncontinue.Location = new System.Drawing.Point(287, 344);
            this.btncontinue.Name = "btncontinue";
            this.btncontinue.Size = new System.Drawing.Size(97, 34);
            this.btncontinue.TabIndex = 25;
            this.btncontinue.Text = "&Continue";
            this.btncontinue.UseVisualStyleBackColor = false;
            this.btncontinue.Click += new System.EventHandler(this.btncontinue_Click);
            this.btncontinue.Enter += new System.EventHandler(this.btncontinue_Enter);
            this.btncontinue.Leave += new System.EventHandler(this.btncontinue_Leave);
            this.btncontinue.MouseEnter += new System.EventHandler(this.btncontinue_MouseEnter);
            this.btncontinue.MouseLeave += new System.EventHandler(this.btncontinue_MouseLeave);
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
            this.LVFO.Location = new System.Drawing.Point(25, 64);
            this.LVFO.MultiSelect = false;
            this.LVFO.Name = "LVFO";
            this.LVFO.Size = new System.Drawing.Size(465, 278);
            this.LVFO.TabIndex = 24;
            this.LVFO.UseCompatibleStateImageBehavior = false;
            this.LVFO.View = System.Windows.Forms.View.Details;
            this.LVFO.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.LVFO_ItemChecked);
            // 
            // textBox14
            // 
            this.textBox14.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.textBox14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox14.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox14.ForeColor = System.Drawing.Color.White;
            this.textBox14.Location = new System.Drawing.Point(14, 16);
            this.textBox14.Name = "textBox14";
            this.textBox14.ReadOnly = true;
            this.textBox14.Size = new System.Drawing.Size(490, 31);
            this.textBox14.TabIndex = 204;
            this.textBox14.TabStop = false;
            this.textBox14.Text = "PURCHASE SELECT BILL";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnclose);
            this.panel1.Location = new System.Drawing.Point(14, 47);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(490, 333);
            this.panel1.TabIndex = 205;
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(375, 297);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(97, 32);
            this.btnclose.TabIndex = 8;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            this.btnclose.Enter += new System.EventHandler(this.btnclose_Enter);
            this.btnclose.Leave += new System.EventHandler(this.btnclose_Leave);
            this.btnclose.MouseEnter += new System.EventHandler(this.btnclose_MouseEnter);
            this.btnclose.MouseLeave += new System.EventHandler(this.btnclose_MouseLeave);
            // 
            // PurchaseSelectBills
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(516, 392);
            this.Controls.Add(this.textBox14);
            this.Controls.Add(this.btnback);
            this.Controls.Add(this.btncontinue);
            this.Controls.Add(this.LVFO);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PurchaseSelectBills";
            this.Text = "PurchaseSelectBills";
            this.Load += new System.EventHandler(this.PurchaseSelectBills_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btnback;
        internal System.Windows.Forms.Button btncontinue;
        internal System.Windows.Forms.ListView LVFO;
        internal System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Button btnclose;
    }
}