namespace RamdevSales
{
    partial class ChargesPnl
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
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtvalexp = new System.Windows.Forms.TextBox();
            this.txttax = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtsgst = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtcgst = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtigst = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtadditax = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // textBox14
            // 
            this.textBox14.BackColor = System.Drawing.SystemColors.Highlight;
            this.textBox14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox14.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox14.ForeColor = System.Drawing.Color.White;
            this.textBox14.Location = new System.Drawing.Point(4, 12);
            this.textBox14.Name = "textBox14";
            this.textBox14.ReadOnly = true;
            this.textBox14.Size = new System.Drawing.Size(643, 31);
            this.textBox14.TabIndex = 6;
            this.textBox14.TabStop = false;
            this.textBox14.Text = "GST CHARGABLE ON EXPENSES";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Value Of Exp";
            // 
            // txtvalexp
            // 
            this.txtvalexp.Location = new System.Drawing.Point(13, 82);
            this.txtvalexp.Name = "txtvalexp";
            this.txtvalexp.Size = new System.Drawing.Size(100, 20);
            this.txtvalexp.TabIndex = 0;
            this.txtvalexp.TextChanged += new System.EventHandler(this.txtvalexp_TextChanged);
            this.txtvalexp.Enter += new System.EventHandler(this.txtvalexp_Enter);
            this.txtvalexp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtvalexp_KeyDown);
            this.txtvalexp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtvalexp_KeyPress);
            this.txtvalexp.Leave += new System.EventHandler(this.txtvalexp_Leave);
            // 
            // txttax
            // 
            this.txttax.Location = new System.Drawing.Point(118, 82);
            this.txttax.Name = "txttax";
            this.txttax.Size = new System.Drawing.Size(100, 20);
            this.txttax.TabIndex = 1;
            this.txttax.TextChanged += new System.EventHandler(this.txttax_TextChanged);
            this.txttax.Enter += new System.EventHandler(this.txttax_Enter);
            this.txttax.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txttax_KeyDown);
            this.txttax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txttax_KeyPress);
            this.txttax.Leave += new System.EventHandler(this.txttax_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(151, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Tax";
            // 
            // txtsgst
            // 
            this.txtsgst.Location = new System.Drawing.Point(224, 82);
            this.txtsgst.Name = "txtsgst";
            this.txtsgst.Size = new System.Drawing.Size(100, 20);
            this.txtsgst.TabIndex = 2;
            this.txtsgst.Enter += new System.EventHandler(this.txtsgst_Enter);
            this.txtsgst.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtsgst_KeyDown);
            this.txtsgst.Leave += new System.EventHandler(this.txtsgst_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(235, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "SGST Amount";
            // 
            // txtcgst
            // 
            this.txtcgst.Location = new System.Drawing.Point(330, 82);
            this.txtcgst.Name = "txtcgst";
            this.txtcgst.Size = new System.Drawing.Size(100, 20);
            this.txtcgst.TabIndex = 3;
            this.txtcgst.Enter += new System.EventHandler(this.txtcgst_Enter);
            this.txtcgst.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtcgst_KeyDown);
            this.txtcgst.Leave += new System.EventHandler(this.txtcgst_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(341, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "CGST Amount";
            // 
            // txtigst
            // 
            this.txtigst.Location = new System.Drawing.Point(436, 82);
            this.txtigst.Name = "txtigst";
            this.txtigst.Size = new System.Drawing.Size(100, 20);
            this.txtigst.TabIndex = 4;
            this.txtigst.Enter += new System.EventHandler(this.txtigst_Enter);
            this.txtigst.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtigst_KeyDown);
            this.txtigst.Leave += new System.EventHandler(this.txtigst_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(447, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "IGST Amount";
            // 
            // txtadditax
            // 
            this.txtadditax.Location = new System.Drawing.Point(542, 82);
            this.txtadditax.Name = "txtadditax";
            this.txtadditax.Size = new System.Drawing.Size(100, 20);
            this.txtadditax.TabIndex = 5;
            this.txtadditax.Enter += new System.EventHandler(this.txtadditax_Enter);
            this.txtadditax.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtadditax_KeyDown);
            this.txtadditax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtadditax_KeyPress);
            this.txtadditax.Leave += new System.EventHandler(this.txtadditax_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(553, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Addi Tax[%].";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(4, 42);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(643, 84);
            this.panel1.TabIndex = 13;
            // 
            // ChargesPnl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(659, 128);
            this.Controls.Add(this.txtadditax);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtigst);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtcgst);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtsgst);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txttax);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtvalexp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox14);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ChargesPnl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChargesPnl";
            this.Load += new System.EventHandler(this.ChargesPnl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtvalexp;
        private System.Windows.Forms.TextBox txttax;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtsgst;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtcgst;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtigst;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtadditax;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
    }
}