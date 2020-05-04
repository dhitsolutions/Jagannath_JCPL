namespace RamdevSales
{
    partial class Inward
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
            this.LVInwrd = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtbarcode = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnadd = new System.Windows.Forms.Button();
            this.txtqty = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbproduct = new System.Windows.Forms.ComboBox();
            this.cmbcomp = new System.Windows.Forms.ComboBox();
            this.lblproduct = new System.Windows.Forms.Label();
            this.lblcomp = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtchqno = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtchqdt = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtinvno = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtinvdt = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtcmptotdetail = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtbillamt = new System.Windows.Forms.TextBox();
            this.btnsubmit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // LVInwrd
            // 
            this.LVInwrd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LVInwrd.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LVInwrd.FullRowSelect = true;
            this.LVInwrd.GridLines = true;
            this.LVInwrd.HideSelection = false;
            this.LVInwrd.Location = new System.Drawing.Point(12, 154);
            this.LVInwrd.MultiSelect = false;
            this.LVInwrd.Name = "LVInwrd";
            this.LVInwrd.Size = new System.Drawing.Size(905, 231);
            this.LVInwrd.TabIndex = 9;
            this.LVInwrd.UseCompatibleStateImageBehavior = false;
            this.LVInwrd.View = System.Windows.Forms.View.Details;
            this.LVInwrd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LVInwrd_KeyDown);
            this.LVInwrd.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LVInwrd_MouseDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtbarcode);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btnadd);
            this.groupBox1.Controls.Add(this.txtqty);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbproduct);
            this.groupBox1.Controls.Add(this.cmbcomp);
            this.groupBox1.Controls.Add(this.lblproduct);
            this.groupBox1.Controls.Add(this.lblcomp);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(358, 125);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Category";
            // 
            // txtbarcode
            // 
            this.txtbarcode.Location = new System.Drawing.Point(127, 67);
            this.txtbarcode.Name = "txtbarcode";
            this.txtbarcode.Size = new System.Drawing.Size(222, 20);
            this.txtbarcode.TabIndex = 23;
            this.txtbarcode.Validated += new System.EventHandler(this.txtbarcode_Validated);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(6, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 14);
            this.label7.TabIndex = 24;
            this.label7.Text = "Barcode";
            // 
            // btnadd
            // 
            this.btnadd.BackColor = System.Drawing.Color.PeachPuff;
            this.btnadd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnadd.Location = new System.Drawing.Point(195, 92);
            this.btnadd.Name = "btnadd";
            this.btnadd.Size = new System.Drawing.Size(75, 23);
            this.btnadd.TabIndex = 8;
            this.btnadd.Text = "ADD";
            this.btnadd.UseVisualStyleBackColor = false;
            this.btnadd.Click += new System.EventHandler(this.btnadd_Click);
            // 
            // txtqty
            // 
            this.txtqty.Location = new System.Drawing.Point(51, 96);
            this.txtqty.Name = "txtqty";
            this.txtqty.Size = new System.Drawing.Size(100, 20);
            this.txtqty.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(6, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 14);
            this.label3.TabIndex = 22;
            this.label3.Text = "Qty";
            // 
            // cmbproduct
            // 
            this.cmbproduct.FormattingEnabled = true;
            this.cmbproduct.Location = new System.Drawing.Point(127, 42);
            this.cmbproduct.Name = "cmbproduct";
            this.cmbproduct.Size = new System.Drawing.Size(225, 21);
            this.cmbproduct.TabIndex = 6;
            this.cmbproduct.SelectedIndexChanged += new System.EventHandler(this.cmbproduct_SelectedIndexChanged);
            // 
            // cmbcomp
            // 
            this.cmbcomp.FormattingEnabled = true;
            this.cmbcomp.Location = new System.Drawing.Point(127, 13);
            this.cmbcomp.Name = "cmbcomp";
            this.cmbcomp.Size = new System.Drawing.Size(225, 21);
            this.cmbcomp.TabIndex = 5;
            this.cmbcomp.SelectedIndexChanged += new System.EventHandler(this.cmbcomp_SelectedIndexChanged);
            // 
            // lblproduct
            // 
            this.lblproduct.AutoSize = true;
            this.lblproduct.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblproduct.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblproduct.Location = new System.Drawing.Point(6, 42);
            this.lblproduct.Name = "lblproduct";
            this.lblproduct.Size = new System.Drawing.Size(97, 14);
            this.lblproduct.TabIndex = 20;
            this.lblproduct.Text = "Select Product";
            // 
            // lblcomp
            // 
            this.lblcomp.AutoSize = true;
            this.lblcomp.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcomp.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblcomp.Location = new System.Drawing.Point(6, 16);
            this.lblcomp.Name = "lblcomp";
            this.lblcomp.Size = new System.Drawing.Size(108, 14);
            this.lblcomp.TabIndex = 19;
            this.lblcomp.Text = "Select Company";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtchqno);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.dtchqdt);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtinvno);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.dtinvdt);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(638, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(285, 125);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Received Invoice Details";
            // 
            // txtchqno
            // 
            this.txtchqno.Location = new System.Drawing.Point(98, 96);
            this.txtchqno.Name = "txtchqno";
            this.txtchqno.Size = new System.Drawing.Size(102, 20);
            this.txtchqno.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(6, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "Cheque No:";
            // 
            // dtchqdt
            // 
            this.dtchqdt.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtchqdt.Location = new System.Drawing.Point(98, 69);
            this.dtchqdt.Name = "dtchqdt";
            this.dtchqdt.Size = new System.Drawing.Size(102, 20);
            this.dtchqdt.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(6, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "Cheque Date:";
            // 
            // txtinvno
            // 
            this.txtinvno.Location = new System.Drawing.Point(98, 39);
            this.txtinvno.Name = "txtinvno";
            this.txtinvno.Size = new System.Drawing.Size(175, 20);
            this.txtinvno.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(6, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Invoice No:";
            // 
            // dtinvdt
            // 
            this.dtinvdt.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtinvdt.Location = new System.Drawing.Point(98, 12);
            this.dtinvdt.Name = "dtinvdt";
            this.dtinvdt.Size = new System.Drawing.Size(102, 20);
            this.dtinvdt.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Invoice Date:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtcmptotdetail);
            this.groupBox3.Location = new System.Drawing.Point(376, 13);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(256, 125);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Supplier Details";
            // 
            // txtcmptotdetail
            // 
            this.txtcmptotdetail.Enabled = false;
            this.txtcmptotdetail.Location = new System.Drawing.Point(7, 20);
            this.txtcmptotdetail.Multiline = true;
            this.txtcmptotdetail.Name = "txtcmptotdetail";
            this.txtcmptotdetail.Size = new System.Drawing.Size(243, 96);
            this.txtcmptotdetail.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(497, 424);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 14);
            this.label4.TabIndex = 24;
            this.label4.Text = "Bill Amount";
            // 
            // txtbillamt
            // 
            this.txtbillamt.BackColor = System.Drawing.Color.Bisque;
            this.txtbillamt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbillamt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbillamt.Location = new System.Drawing.Point(592, 418);
            this.txtbillamt.Name = "txtbillamt";
            this.txtbillamt.Size = new System.Drawing.Size(150, 26);
            this.txtbillamt.TabIndex = 10;
            // 
            // btnsubmit
            // 
            this.btnsubmit.BackColor = System.Drawing.Color.AntiqueWhite;
            this.btnsubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsubmit.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsubmit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnsubmit.Location = new System.Drawing.Point(791, 408);
            this.btnsubmit.Name = "btnsubmit";
            this.btnsubmit.Size = new System.Drawing.Size(120, 39);
            this.btnsubmit.TabIndex = 11;
            this.btnsubmit.Text = "SUBMIT";
            this.btnsubmit.UseVisualStyleBackColor = false;
            this.btnsubmit.Click += new System.EventHandler(this.btnsubmit_Click);
            // 
            // Inward
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 501);
            this.Controls.Add(this.btnsubmit);
            this.Controls.Add(this.txtbillamt);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.LVInwrd);
            this.Name = "Inward";
            this.Text = "Inward";
            this.Load += new System.EventHandler(this.Inward_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ListView LVInwrd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbproduct;
        private System.Windows.Forms.ComboBox cmbcomp;
        private System.Windows.Forms.Label lblproduct;
        internal System.Windows.Forms.Label lblcomp;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtinvno;
        internal System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtinvdt;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtqty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtchqno;
        internal System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtchqdt;
        internal System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtcmptotdetail;
        private System.Windows.Forms.TextBox txtbillamt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnadd;
        internal System.Windows.Forms.Button btnsubmit;
        private System.Windows.Forms.TextBox txtbarcode;
        private System.Windows.Forms.Label label7;

    }
}