namespace RamdevSales
{
    partial class frmSendToCustomer
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblsendtocustumarid = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbReplacementType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCustId = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rdbOld = new System.Windows.Forms.RadioButton();
            this.rdbNew = new System.Windows.Forms.RadioButton();
            this.dtdate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.btnadd = new System.Windows.Forms.Button();
            this.cmbCustomerName = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbSerialNo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbComplainId = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lvcompany = new System.Windows.Forms.ListView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btndelete = new System.Windows.Forms.Button();
            this.btnprint = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTransportDetail = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblsendtocustumarid);
            this.panel1.Controls.Add(this.txtQty);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.cmbReplacementType);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtCustId);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.rdbOld);
            this.panel1.Controls.Add(this.rdbNew);
            this.panel1.Controls.Add(this.dtdate);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnadd);
            this.panel1.Controls.Add(this.cmbCustomerName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cmbSerialNo);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmbComplainId);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(7, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(709, 100);
            this.panel1.TabIndex = 0;
            // 
            // lblsendtocustumarid
            // 
            this.lblsendtocustumarid.AutoSize = true;
            this.lblsendtocustumarid.Location = new System.Drawing.Point(632, 44);
            this.lblsendtocustumarid.Name = "lblsendtocustumarid";
            this.lblsendtocustumarid.Size = new System.Drawing.Size(35, 13);
            this.lblsendtocustumarid.TabIndex = 22;
            this.lblsendtocustumarid.Text = "label7";
            this.lblsendtocustumarid.Visible = false;
            // 
            // txtQty
            // 
            this.txtQty.Enabled = false;
            this.txtQty.Location = new System.Drawing.Point(474, 74);
            this.txtQty.Name = "txtQty";
            this.txtQty.ReadOnly = true;
            this.txtQty.Size = new System.Drawing.Size(59, 20);
            this.txtQty.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(445, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Qty";
            // 
            // cmbReplacementType
            // 
            this.cmbReplacementType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReplacementType.FormattingEnabled = true;
            this.cmbReplacementType.Items.AddRange(new object[] {
            "In Warrenty",
            "Not In Warrenty"});
            this.cmbReplacementType.Location = new System.Drawing.Point(320, 72);
            this.cmbReplacementType.Name = "cmbReplacementType";
            this.cmbReplacementType.Size = new System.Drawing.Size(121, 21);
            this.cmbReplacementType.TabIndex = 5;
            this.cmbReplacementType.SelectedIndexChanged += new System.EventHandler(this.cmbReplacementType_SelectedIndexChanged);
            this.cmbReplacementType.Enter += new System.EventHandler(this.cmbReplacementType_Enter);
            this.cmbReplacementType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbReplacementType_KeyDown);
            this.cmbReplacementType.Leave += new System.EventHandler(this.cmbReplacementType_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(218, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Replacement Type";
            // 
            // txtCustId
            // 
            this.txtCustId.Enabled = false;
            this.txtCustId.Location = new System.Drawing.Point(93, 72);
            this.txtCustId.Name = "txtCustId";
            this.txtCustId.ReadOnly = true;
            this.txtCustId.Size = new System.Drawing.Size(100, 20);
            this.txtCustId.TabIndex = 4;
            this.txtCustId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustId_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Customer id";
            // 
            // rdbOld
            // 
            this.rdbOld.AutoSize = true;
            this.rdbOld.Checked = true;
            this.rdbOld.Location = new System.Drawing.Point(626, 14);
            this.rdbOld.Name = "rdbOld";
            this.rdbOld.Size = new System.Drawing.Size(41, 17);
            this.rdbOld.TabIndex = 16;
            this.rdbOld.TabStop = true;
            this.rdbOld.Text = "Old";
            this.rdbOld.UseVisualStyleBackColor = true;
            this.rdbOld.CheckedChanged += new System.EventHandler(this.rdbOld_CheckedChanged);
            // 
            // rdbNew
            // 
            this.rdbNew.AutoSize = true;
            this.rdbNew.Location = new System.Drawing.Point(573, 13);
            this.rdbNew.Name = "rdbNew";
            this.rdbNew.Size = new System.Drawing.Size(47, 17);
            this.rdbNew.TabIndex = 15;
            this.rdbNew.Text = "New";
            this.rdbNew.UseVisualStyleBackColor = true;
            this.rdbNew.CheckedChanged += new System.EventHandler(this.rdbNew_CheckedChanged);
            // 
            // dtdate
            // 
            this.dtdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtdate.Location = new System.Drawing.Point(49, 13);
            this.dtdate.Name = "dtdate";
            this.dtdate.Size = new System.Drawing.Size(94, 20);
            this.dtdate.TabIndex = 0;
            this.dtdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtdate_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Date";
            // 
            // btnadd
            // 
            this.btnadd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnadd.Location = new System.Drawing.Point(557, 68);
            this.btnadd.Name = "btnadd";
            this.btnadd.Size = new System.Drawing.Size(45, 26);
            this.btnadd.TabIndex = 7;
            this.btnadd.Text = "+";
            this.btnadd.UseVisualStyleBackColor = true;
            this.btnadd.Click += new System.EventHandler(this.btnadd_Click);
            // 
            // cmbCustomerName
            // 
            this.cmbCustomerName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomerName.FormattingEnabled = true;
            this.cmbCustomerName.Location = new System.Drawing.Point(93, 44);
            this.cmbCustomerName.Name = "cmbCustomerName";
            this.cmbCustomerName.Size = new System.Drawing.Size(325, 21);
            this.cmbCustomerName.TabIndex = 3;
            this.cmbCustomerName.SelectedIndexChanged += new System.EventHandler(this.cmbCustomerName_SelectedIndexChanged);
            this.cmbCustomerName.Enter += new System.EventHandler(this.cmbCustomerName_Enter);
            this.cmbCustomerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCustomerName_KeyDown);
            this.cmbCustomerName.Leave += new System.EventHandler(this.cmbCustomerName_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Customer Name";
            // 
            // cmbSerialNo
            // 
            this.cmbSerialNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSerialNo.FormattingEnabled = true;
            this.cmbSerialNo.Location = new System.Drawing.Point(445, 14);
            this.cmbSerialNo.Name = "cmbSerialNo";
            this.cmbSerialNo.Size = new System.Drawing.Size(121, 21);
            this.cmbSerialNo.TabIndex = 2;
            this.cmbSerialNo.SelectedIndexChanged += new System.EventHandler(this.cmbSerialNo_SelectedIndexChanged);
            this.cmbSerialNo.Enter += new System.EventHandler(this.cmbSerialNo_Enter);
            this.cmbSerialNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSerialNo_KeyDown);
            this.cmbSerialNo.Leave += new System.EventHandler(this.cmbSerialNo_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(387, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Serial No.";
            // 
            // cmbComplainId
            // 
            this.cmbComplainId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComplainId.FormattingEnabled = true;
            this.cmbComplainId.Location = new System.Drawing.Point(239, 14);
            this.cmbComplainId.Name = "cmbComplainId";
            this.cmbComplainId.Size = new System.Drawing.Size(143, 21);
            this.cmbComplainId.TabIndex = 1;
            this.cmbComplainId.SelectedIndexChanged += new System.EventHandler(this.cmbComplainId_SelectedIndexChanged);
            this.cmbComplainId.Enter += new System.EventHandler(this.cmbComplainId_Enter);
            this.cmbComplainId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbComplainId_KeyDown);
            this.cmbComplainId.Leave += new System.EventHandler(this.cmbComplainId_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(155, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Complain Id";
            // 
            // TextBox1
            // 
            this.TextBox1.BackColor = System.Drawing.SystemColors.Highlight;
            this.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBox1.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox1.ForeColor = System.Drawing.Color.White;
            this.TextBox1.Location = new System.Drawing.Point(0, 0);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = true;
            this.TextBox1.Size = new System.Drawing.Size(719, 31);
            this.TextBox1.TabIndex = 15;
            this.TextBox1.TabStop = false;
            this.TextBox1.Text = "SEND TO CUSTOMER";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lvcompany);
            this.panel2.Location = new System.Drawing.Point(7, 109);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(709, 175);
            this.panel2.TabIndex = 1;
            // 
            // lvcompany
            // 
            this.lvcompany.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.lvcompany.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvcompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvcompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvcompany.FullRowSelect = true;
            this.lvcompany.GridLines = true;
            this.lvcompany.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvcompany.HideSelection = false;
            this.lvcompany.Location = new System.Drawing.Point(4, 4);
            this.lvcompany.MultiSelect = false;
            this.lvcompany.Name = "lvcompany";
            this.lvcompany.Size = new System.Drawing.Size(702, 168);
            this.lvcompany.TabIndex = 0;
            this.lvcompany.UseCompatibleStateImageBehavior = false;
            this.lvcompany.View = System.Windows.Forms.View.Details;
            this.lvcompany.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvcompany_KeyDown);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btndelete);
            this.panel3.Controls.Add(this.btnprint);
            this.panel3.Controls.Add(this.btnclose);
            this.panel3.Controls.Add(this.btnSubmit);
            this.panel3.Controls.Add(this.txtRemarks);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.txtTransportDetail);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Location = new System.Drawing.Point(7, 290);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(706, 129);
            this.panel3.TabIndex = 2;
            // 
            // btndelete
            // 
            this.btndelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btndelete.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btndelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndelete.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndelete.ForeColor = System.Drawing.Color.White;
            this.btndelete.Location = new System.Drawing.Point(466, 67);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(97, 34);
            this.btndelete.TabIndex = 5;
            this.btndelete.Text = "Delete";
            this.btndelete.UseVisualStyleBackColor = false;
            this.btndelete.Click += new System.EventHandler(this.btndelete_Click);
            this.btndelete.Enter += new System.EventHandler(this.btndelete_Enter);
            this.btndelete.Leave += new System.EventHandler(this.btndelete_Leave);
            this.btndelete.MouseEnter += new System.EventHandler(this.btndelete_MouseEnter);
            this.btndelete.MouseLeave += new System.EventHandler(this.btndelete_MouseLeave);
            // 
            // btnprint
            // 
            this.btnprint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnprint.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprint.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(573, 25);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(97, 34);
            this.btnprint.TabIndex = 3;
            this.btnprint.Text = "Print";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            this.btnprint.Enter += new System.EventHandler(this.btnprint_Enter);
            this.btnprint.Leave += new System.EventHandler(this.btnprint_Leave);
            this.btnprint.MouseEnter += new System.EventHandler(this.btnprint_MouseEnter);
            this.btnprint.MouseLeave += new System.EventHandler(this.btnprint_MouseLeave);
            // 
            // btnclose
            // 
            this.btnclose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnclose.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(574, 67);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(97, 34);
            this.btnclose.TabIndex = 4;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            this.btnclose.Enter += new System.EventHandler(this.btnclose_Enter);
            this.btnclose.Leave += new System.EventHandler(this.btnclose_Leave);
            this.btnclose.MouseEnter += new System.EventHandler(this.btnclose_MouseEnter);
            this.btnclose.MouseLeave += new System.EventHandler(this.btnclose_MouseLeave);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(466, 26);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(97, 34);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "&Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            this.btnSubmit.Enter += new System.EventHandler(this.btnSubmit_Enter);
            this.btnSubmit.Leave += new System.EventHandler(this.btnSubmit_Leave);
            this.btnSubmit.MouseEnter += new System.EventHandler(this.btnSubmit_MouseEnter);
            this.btnSubmit.MouseLeave += new System.EventHandler(this.btnSubmit_MouseLeave);
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(98, 67);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(324, 50);
            this.txtRemarks.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 78);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Remarks";
            // 
            // txtTransportDetail
            // 
            this.txtTransportDetail.Location = new System.Drawing.Point(98, 11);
            this.txtTransportDetail.Multiline = true;
            this.txtTransportDetail.Name = "txtTransportDetail";
            this.txtTransportDetail.Size = new System.Drawing.Size(324, 50);
            this.txtTransportDetail.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Transport Detail";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Location = new System.Drawing.Point(2, 31);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(717, 445);
            this.panel4.TabIndex = 0;
            // 
            // frmSendToCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(721, 477);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.panel4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSendToCustomer";
            this.Text = "Send To Customer";
            this.Load += new System.EventHandler(this.frmSendToCustomer_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dtdate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnadd;
        private System.Windows.Forms.ComboBox cmbCustomerName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbSerialNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbComplainId;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox TextBox1;
        private System.Windows.Forms.RadioButton rdbOld;
        private System.Windows.Forms.RadioButton rdbNew;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCustId;
        private System.Windows.Forms.ComboBox cmbReplacementType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTransportDetail;
        private System.Windows.Forms.Label label9;
        internal System.Windows.Forms.Button btnclose;
        internal System.Windows.Forms.Button btnSubmit;
        internal System.Windows.Forms.Button btndelete;
        internal System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.Panel panel4;
        internal System.Windows.Forms.ListView lvcompany;
        private System.Windows.Forms.Label lblsendtocustumarid;
    }
}