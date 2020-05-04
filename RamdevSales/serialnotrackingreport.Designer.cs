namespace RamdevSales
{
    partial class serialnotrackingreport
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtcurrentstatus = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lvsendtocustomer = new System.Windows.Forms.ListView();
            this.lvreceivefromcompany = new System.Windows.Forms.ListView();
            this.lvsendtocompany = new System.Windows.Forms.ListView();
            this.lvcomplain = new System.Windows.Forms.ListView();
            this.btnok = new System.Windows.Forms.Button();
            this.txtserialno = new System.Windows.Forms.TextBox();
            this.btncancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblnewserialno = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox14
            // 
            this.textBox14.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.textBox14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox14.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox14.ForeColor = System.Drawing.Color.White;
            this.textBox14.Location = new System.Drawing.Point(2, 1);
            this.textBox14.Name = "textBox14";
            this.textBox14.ReadOnly = true;
            this.textBox14.Size = new System.Drawing.Size(978, 31);
            this.textBox14.TabIndex = 204;
            this.textBox14.TabStop = false;
            this.textBox14.Text = "SERIAL NO.TRACKING";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblnewserialno);
            this.panel1.Controls.Add(this.txtcurrentstatus);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lvsendtocustomer);
            this.panel1.Controls.Add(this.lvreceivefromcompany);
            this.panel1.Controls.Add(this.lvsendtocompany);
            this.panel1.Controls.Add(this.lvcomplain);
            this.panel1.Controls.Add(this.btnok);
            this.panel1.Controls.Add(this.txtserialno);
            this.panel1.Controls.Add(this.btncancel);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(3, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(976, 470);
            this.panel1.TabIndex = 0;
            // 
            // txtcurrentstatus
            // 
            this.txtcurrentstatus.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcurrentstatus.ForeColor = System.Drawing.Color.Red;
            this.txtcurrentstatus.Location = new System.Drawing.Point(123, 436);
            this.txtcurrentstatus.Name = "txtcurrentstatus";
            this.txtcurrentstatus.Size = new System.Drawing.Size(847, 27);
            this.txtcurrentstatus.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(5, 438);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 18);
            this.label2.TabIndex = 214;
            this.label2.Text = "Current Status";
            // 
            // lvsendtocustomer
            // 
            this.lvsendtocustomer.BackColor = System.Drawing.Color.White;
            this.lvsendtocustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvsendtocustomer.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvsendtocustomer.ForeColor = System.Drawing.Color.Navy;
            this.lvsendtocustomer.FullRowSelect = true;
            this.lvsendtocustomer.GridLines = true;
            this.lvsendtocustomer.HideSelection = false;
            this.lvsendtocustomer.Location = new System.Drawing.Point(7, 346);
            this.lvsendtocustomer.MultiSelect = false;
            this.lvsendtocustomer.Name = "lvsendtocustomer";
            this.lvsendtocustomer.Size = new System.Drawing.Size(963, 86);
            this.lvsendtocustomer.TabIndex = 6;
            this.lvsendtocustomer.UseCompatibleStateImageBehavior = false;
            this.lvsendtocustomer.View = System.Windows.Forms.View.Details;
            this.lvsendtocustomer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvsendtocustomer_KeyDown);
            this.lvsendtocustomer.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvsendtocustomer_MouseDoubleClick);
            // 
            // lvreceivefromcompany
            // 
            this.lvreceivefromcompany.BackColor = System.Drawing.Color.White;
            this.lvreceivefromcompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvreceivefromcompany.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvreceivefromcompany.ForeColor = System.Drawing.Color.Navy;
            this.lvreceivefromcompany.FullRowSelect = true;
            this.lvreceivefromcompany.GridLines = true;
            this.lvreceivefromcompany.HideSelection = false;
            this.lvreceivefromcompany.Location = new System.Drawing.Point(7, 260);
            this.lvreceivefromcompany.MultiSelect = false;
            this.lvreceivefromcompany.Name = "lvreceivefromcompany";
            this.lvreceivefromcompany.Size = new System.Drawing.Size(963, 85);
            this.lvreceivefromcompany.TabIndex = 5;
            this.lvreceivefromcompany.UseCompatibleStateImageBehavior = false;
            this.lvreceivefromcompany.View = System.Windows.Forms.View.Details;
            this.lvreceivefromcompany.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvreceivefromcompany_KeyDown);
            this.lvreceivefromcompany.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvreceivefromcompany_MouseDoubleClick);
            // 
            // lvsendtocompany
            // 
            this.lvsendtocompany.BackColor = System.Drawing.Color.White;
            this.lvsendtocompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvsendtocompany.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvsendtocompany.ForeColor = System.Drawing.Color.Navy;
            this.lvsendtocompany.FullRowSelect = true;
            this.lvsendtocompany.GridLines = true;
            this.lvsendtocompany.HideSelection = false;
            this.lvsendtocompany.Location = new System.Drawing.Point(7, 174);
            this.lvsendtocompany.MultiSelect = false;
            this.lvsendtocompany.Name = "lvsendtocompany";
            this.lvsendtocompany.Size = new System.Drawing.Size(963, 85);
            this.lvsendtocompany.TabIndex = 4;
            this.lvsendtocompany.UseCompatibleStateImageBehavior = false;
            this.lvsendtocompany.View = System.Windows.Forms.View.Details;
            this.lvsendtocompany.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvsendtocompany_KeyDown);
            this.lvsendtocompany.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvsendtocompany_MouseDoubleClick);
            // 
            // lvcomplain
            // 
            this.lvcomplain.BackColor = System.Drawing.Color.White;
            this.lvcomplain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvcomplain.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvcomplain.ForeColor = System.Drawing.Color.Navy;
            this.lvcomplain.FullRowSelect = true;
            this.lvcomplain.GridLines = true;
            this.lvcomplain.HideSelection = false;
            this.lvcomplain.Location = new System.Drawing.Point(8, 87);
            this.lvcomplain.MultiSelect = false;
            this.lvcomplain.Name = "lvcomplain";
            this.lvcomplain.Size = new System.Drawing.Size(963, 86);
            this.lvcomplain.TabIndex = 3;
            this.lvcomplain.UseCompatibleStateImageBehavior = false;
            this.lvcomplain.View = System.Windows.Forms.View.Details;
            this.lvcomplain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvcomplain_KeyDown);
            this.lvcomplain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvcomplain_MouseDoubleClick);
            // 
            // btnok
            // 
            this.btnok.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnok.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnok.ForeColor = System.Drawing.Color.White;
            this.btnok.Location = new System.Drawing.Point(771, 21);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(97, 34);
            this.btnok.TabIndex = 1;
            this.btnok.Text = "&OK";
            this.btnok.UseVisualStyleBackColor = false;
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            this.btnok.Enter += new System.EventHandler(this.btnok_Enter);
            this.btnok.Leave += new System.EventHandler(this.btnok_Leave);
            this.btnok.MouseEnter += new System.EventHandler(this.btnok_MouseEnter);
            this.btnok.MouseLeave += new System.EventHandler(this.btnok_MouseLeave);
            // 
            // txtserialno
            // 
            this.txtserialno.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtserialno.Location = new System.Drawing.Point(223, 25);
            this.txtserialno.Name = "txtserialno";
            this.txtserialno.Size = new System.Drawing.Size(500, 27);
            this.txtserialno.TabIndex = 0;
            this.txtserialno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtserialno_KeyDown);
            // 
            // btncancel
            // 
            this.btncancel.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btncancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncancel.ForeColor = System.Drawing.Color.White;
            this.btncancel.Location = new System.Drawing.Point(874, 21);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(97, 34);
            this.btncancel.TabIndex = 2;
            this.btncancel.Text = "&Close";
            this.btncancel.UseVisualStyleBackColor = false;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            this.btncancel.Enter += new System.EventHandler(this.btncancel_Enter);
            this.btncancel.Leave += new System.EventHandler(this.btncancel_Leave);
            this.btncancel.MouseEnter += new System.EventHandler(this.btncancel_MouseEnter);
            this.btncancel.MouseLeave += new System.EventHandler(this.btncancel_MouseLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(315, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(255, 18);
            this.label1.TabIndex = 209;
            this.label1.Text = "Enter or scan your serial No.Here";
            // 
            // lblnewserialno
            // 
            this.lblnewserialno.AutoSize = true;
            this.lblnewserialno.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblnewserialno.Location = new System.Drawing.Point(221, 56);
            this.lblnewserialno.Name = "lblnewserialno";
            this.lblnewserialno.Size = new System.Drawing.Size(112, 18);
            this.lblnewserialno.TabIndex = 215;
            this.lblnewserialno.Text = "New Serial No";
            this.lblnewserialno.Visible = false;
            // 
            // serialnotrackingreport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 506);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox14);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "serialnotrackingreport";
            this.Text = "serialnotrackingreport";
            this.Load += new System.EventHandler(this.serialnotrackingreport_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Button btnok;
        private System.Windows.Forms.TextBox txtserialno;
        private System.Windows.Forms.Button btncancel;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.ListView lvcomplain;
        internal System.Windows.Forms.ListView lvreceivefromcompany;
        internal System.Windows.Forms.ListView lvsendtocompany;
        internal System.Windows.Forms.ListView lvsendtocustomer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtcurrentstatus;
        private System.Windows.Forms.Label lblnewserialno;
    }
}