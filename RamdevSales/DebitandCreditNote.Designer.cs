namespace RamdevSales
{
    partial class DebitandCreditNote
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebitandCreditNote));
            this.txtheader = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btndelete = new System.Windows.Forms.Button();
            this.btncancel = new System.Windows.Forms.Button();
            this.txtcredittotal = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtdebittotal = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtlongnarration = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lvserial = new System.Windows.Forms.ListView();
            this.txtshortnarration = new System.Windows.Forms.TextBox();
            this.btnAccountEdit = new System.Windows.Forms.Button();
            this.btnAddAccount = new System.Windows.Forms.Button();
            this.BtnSubmit = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbaccountName = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbdrcr = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtvchno = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtRundate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtheader
            // 
            this.txtheader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtheader.BackColor = System.Drawing.SystemColors.Highlight;
            this.txtheader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtheader.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtheader.ForeColor = System.Drawing.Color.White;
            this.txtheader.Location = new System.Drawing.Point(2, 3);
            this.txtheader.Name = "txtheader";
            this.txtheader.ReadOnly = true;
            this.txtheader.Size = new System.Drawing.Size(1072, 31);
            this.txtheader.TabIndex = 175;
            this.txtheader.TabStop = false;
            this.txtheader.Text = "DEBIT NOTE";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btndelete);
            this.panel1.Controls.Add(this.btncancel);
            this.panel1.Controls.Add(this.txtcredittotal);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtdebittotal);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtlongnarration);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.lvserial);
            this.panel1.Controls.Add(this.txtshortnarration);
            this.panel1.Controls.Add(this.btnAccountEdit);
            this.panel1.Controls.Add(this.btnAddAccount);
            this.panel1.Controls.Add(this.BtnSubmit);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txtAmount);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmbaccountName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cmbdrcr);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtvchno);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.TxtRundate);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtheader);
            this.panel1.Location = new System.Drawing.Point(5, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1077, 532);
            this.panel1.TabIndex = 0;
            // 
            // btndelete
            // 
            this.btndelete.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btndelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndelete.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndelete.ForeColor = System.Drawing.Color.White;
            this.btndelete.Location = new System.Drawing.Point(860, 45);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(97, 38);
            this.btndelete.TabIndex = 277;
            this.btndelete.Text = "&Delete";
            this.btndelete.UseVisualStyleBackColor = false;
            this.btndelete.Click += new System.EventHandler(this.btndelete_Click);
            this.btndelete.Enter += new System.EventHandler(this.btndelete_Enter);
            this.btndelete.Leave += new System.EventHandler(this.btndelete_Leave);
            this.btndelete.MouseEnter += new System.EventHandler(this.btndelete_MouseEnter);
            this.btndelete.MouseLeave += new System.EventHandler(this.btndelete_MouseLeave);
            // 
            // btncancel
            // 
            this.btncancel.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btncancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncancel.ForeColor = System.Drawing.Color.White;
            this.btncancel.Location = new System.Drawing.Point(963, 44);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(97, 38);
            this.btncancel.TabIndex = 276;
            this.btncancel.Text = "&Close";
            this.btncancel.UseVisualStyleBackColor = false;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            this.btncancel.Enter += new System.EventHandler(this.btncancel_Enter);
            this.btncancel.Leave += new System.EventHandler(this.btncancel_Leave);
            this.btncancel.MouseEnter += new System.EventHandler(this.btncancel_MouseEnter);
            this.btncancel.MouseLeave += new System.EventHandler(this.btncancel_MouseLeave);
            // 
            // txtcredittotal
            // 
            this.txtcredittotal.Location = new System.Drawing.Point(669, 479);
            this.txtcredittotal.Name = "txtcredittotal";
            this.txtcredittotal.Size = new System.Drawing.Size(130, 20);
            this.txtcredittotal.TabIndex = 10;
            this.txtcredittotal.Enter += new System.EventHandler(this.txtcredittotal_Enter);
            this.txtcredittotal.Leave += new System.EventHandler(this.txtcredittotal_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(700, 461);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 275;
            this.label9.Text = "Credit Total";
            // 
            // txtdebittotal
            // 
            this.txtdebittotal.Location = new System.Drawing.Point(469, 479);
            this.txtdebittotal.Name = "txtdebittotal";
            this.txtdebittotal.Size = new System.Drawing.Size(130, 20);
            this.txtdebittotal.TabIndex = 9;
            this.txtdebittotal.Enter += new System.EventHandler(this.txtdebittotal_Enter);
            this.txtdebittotal.Leave += new System.EventHandler(this.txtdebittotal_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(499, 461);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 273;
            this.label7.Text = "Debit Total";
            // 
            // txtlongnarration
            // 
            this.txtlongnarration.Location = new System.Drawing.Point(20, 438);
            this.txtlongnarration.Multiline = true;
            this.txtlongnarration.Name = "txtlongnarration";
            this.txtlongnarration.Size = new System.Drawing.Size(391, 87);
            this.txtlongnarration.TabIndex = 8;
            this.txtlongnarration.Enter += new System.EventHandler(this.txtlongnarration_Enter);
            this.txtlongnarration.Leave += new System.EventHandler(this.txtlongnarration_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 420);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 271;
            this.label6.Text = "Long Narration";
            // 
            // lvserial
            // 
            this.lvserial.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.lvserial.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvserial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvserial.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvserial.FullRowSelect = true;
            this.lvserial.GridLines = true;
            this.lvserial.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvserial.HideSelection = false;
            this.lvserial.Location = new System.Drawing.Point(14, 141);
            this.lvserial.MultiSelect = false;
            this.lvserial.Name = "lvserial";
            this.lvserial.Size = new System.Drawing.Size(1046, 278);
            this.lvserial.TabIndex = 7;
            this.lvserial.UseCompatibleStateImageBehavior = false;
            this.lvserial.View = System.Windows.Forms.View.Details;
            this.lvserial.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvserial_KeyDown);
            this.lvserial.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvserial_MouseDoubleClick);
            // 
            // txtshortnarration
            // 
            this.txtshortnarration.Location = new System.Drawing.Point(669, 113);
            this.txtshortnarration.Name = "txtshortnarration";
            this.txtshortnarration.Size = new System.Drawing.Size(391, 20);
            this.txtshortnarration.TabIndex = 5;
            this.txtshortnarration.Enter += new System.EventHandler(this.txtshortnarration_Enter);
            this.txtshortnarration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtshortnarration_KeyDown);
            this.txtshortnarration.Leave += new System.EventHandler(this.txtshortnarration_Leave);
            // 
            // btnAccountEdit
            // 
            this.btnAccountEdit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAccountEdit.BackgroundImage")));
            this.btnAccountEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccountEdit.ForeColor = System.Drawing.Color.White;
            this.btnAccountEdit.Location = new System.Drawing.Point(502, 111);
            this.btnAccountEdit.Name = "btnAccountEdit";
            this.btnAccountEdit.Size = new System.Drawing.Size(23, 22);
            this.btnAccountEdit.TabIndex = 268;
            this.btnAccountEdit.TabStop = false;
            this.btnAccountEdit.UseVisualStyleBackColor = true;
            this.btnAccountEdit.Click += new System.EventHandler(this.btnAccountEdit_Click);
            // 
            // btnAddAccount
            // 
            this.btnAddAccount.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnAddAccount.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddAccount.BackgroundImage")));
            this.btnAddAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddAccount.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddAccount.ForeColor = System.Drawing.Color.White;
            this.btnAddAccount.Location = new System.Drawing.Point(483, 112);
            this.btnAddAccount.Name = "btnAddAccount";
            this.btnAddAccount.Size = new System.Drawing.Size(22, 22);
            this.btnAddAccount.TabIndex = 267;
            this.btnAddAccount.TabStop = false;
            this.btnAddAccount.Text = "&Add";
            this.btnAddAccount.UseVisualStyleBackColor = false;
            this.btnAddAccount.Click += new System.EventHandler(this.btnAddAccount_Click);
            // 
            // BtnSubmit
            // 
            this.BtnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSubmit.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BtnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSubmit.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSubmit.ForeColor = System.Drawing.Color.White;
            this.BtnSubmit.Location = new System.Drawing.Point(756, 45);
            this.BtnSubmit.Name = "BtnSubmit";
            this.BtnSubmit.Size = new System.Drawing.Size(93, 39);
            this.BtnSubmit.TabIndex = 6;
            this.BtnSubmit.Text = "&Submit";
            this.BtnSubmit.UseVisualStyleBackColor = false;
            this.BtnSubmit.Click += new System.EventHandler(this.BtnSubmit_Click);
            this.BtnSubmit.Enter += new System.EventHandler(this.BtnSubmit_Enter);
            this.BtnSubmit.Leave += new System.EventHandler(this.BtnSubmit_Leave);
            this.BtnSubmit.MouseEnter += new System.EventHandler(this.BtnSubmit_MouseEnter);
            this.BtnSubmit.MouseLeave += new System.EventHandler(this.BtnSubmit_MouseLeave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(666, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 13);
            this.label8.TabIndex = 266;
            this.label8.Text = "Short Narration";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(526, 113);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(130, 20);
            this.txtAmount.TabIndex = 4;
            this.txtAmount.Enter += new System.EventHandler(this.txtAmount_Enter);
            this.txtAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAmount_KeyDown);
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            this.txtAmount.Leave += new System.EventHandler(this.txtAmount_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(523, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 265;
            this.label2.Text = "Amount";
            // 
            // cmbaccountName
            // 
            this.cmbaccountName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbaccountName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbaccountName.BackColor = System.Drawing.SystemColors.Window;
            this.cmbaccountName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbaccountName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbaccountName.FormattingEnabled = true;
            this.cmbaccountName.Location = new System.Drawing.Point(115, 111);
            this.cmbaccountName.Name = "cmbaccountName";
            this.cmbaccountName.Size = new System.Drawing.Size(360, 24);
            this.cmbaccountName.TabIndex = 3;
            this.cmbaccountName.SelectedIndexChanged += new System.EventHandler(this.cmbaccountName_SelectedIndexChanged);
            this.cmbaccountName.Enter += new System.EventHandler(this.cmbaccountName_Enter);
            this.cmbaccountName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbaccountName_KeyDown);
            this.cmbaccountName.Leave += new System.EventHandler(this.cmbaccountName_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(112, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 264;
            this.label3.Text = "Account Name";
            // 
            // cmbdrcr
            // 
            this.cmbdrcr.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbdrcr.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbdrcr.BackColor = System.Drawing.SystemColors.Window;
            this.cmbdrcr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbdrcr.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbdrcr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbdrcr.FormattingEnabled = true;
            this.cmbdrcr.Items.AddRange(new object[] {
            "C",
            "D"});
            this.cmbdrcr.Location = new System.Drawing.Point(20, 111);
            this.cmbdrcr.Name = "cmbdrcr";
            this.cmbdrcr.Size = new System.Drawing.Size(70, 24);
            this.cmbdrcr.TabIndex = 2;
            this.cmbdrcr.Enter += new System.EventHandler(this.cmbdrcr_Enter);
            this.cmbdrcr.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbdrcr_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 187;
            this.label4.Text = "Dr./Cr.";
            // 
            // txtvchno
            // 
            this.txtvchno.Location = new System.Drawing.Point(218, 55);
            this.txtvchno.Name = "txtvchno";
            this.txtvchno.Size = new System.Drawing.Size(130, 20);
            this.txtvchno.TabIndex = 1;
            this.txtvchno.Enter += new System.EventHandler(this.txtvchno_Enter);
            this.txtvchno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtvchno_KeyDown);
            this.txtvchno.Leave += new System.EventHandler(this.txtvchno_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(170, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 185;
            this.label5.Text = "Vch No";
            // 
            // TxtRundate
            // 
            this.TxtRundate.CustomFormat = "";
            this.TxtRundate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TxtRundate.Location = new System.Drawing.Point(46, 55);
            this.TxtRundate.Name = "TxtRundate";
            this.TxtRundate.Size = new System.Drawing.Size(103, 20);
            this.TxtRundate.TabIndex = 0;
            this.TxtRundate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtRundate_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 177;
            this.label1.Text = "Date";
            // 
            // DebitandCreditNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1087, 539);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DebitandCreditNote";
            this.Text = "DebitandCreditNote";
            this.Load += new System.EventHandler(this.DebitandCreditNote_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.TextBox txtheader;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker TxtRundate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtvchno;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbdrcr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtshortnarration;
        private System.Windows.Forms.Button btnAccountEdit;
        internal System.Windows.Forms.Button btnAddAccount;
        internal System.Windows.Forms.Button BtnSubmit;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbaccountName;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.ListView lvserial;
        private System.Windows.Forms.TextBox txtlongnarration;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtcredittotal;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtdebittotal;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btncancel;
        internal System.Windows.Forms.Button btndelete;
    }
}