namespace RamdevSales
{
    partial class frmReceiveFromCompany
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
            this.cmbscno = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblreceiveid = new System.Windows.Forms.Label();
            this.btndelete = new System.Windows.Forms.Button();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.btnprint = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.txtNewSerialNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rdbMaxRepair = new System.Windows.Forms.RadioButton();
            this.rdbCompanyRepair = new System.Windows.Forms.RadioButton();
            this.cmbSupplierName = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbSerialNo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbComplainId = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lvcompany = new System.Windows.Forms.ListView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbscno);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.lblreceiveid);
            this.panel1.Controls.Add(this.btndelete);
            this.panel1.Controls.Add(this.dtpDate);
            this.panel1.Controls.Add(this.btnprint);
            this.panel1.Controls.Add(this.btnclose);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnSubmit);
            this.panel1.Controls.Add(this.txtNewSerialNo);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.rdbMaxRepair);
            this.panel1.Controls.Add(this.rdbCompanyRepair);
            this.panel1.Controls.Add(this.cmbSupplierName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cmbSerialNo);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmbComplainId);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(3, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 96);
            this.panel1.TabIndex = 0;
            // 
            // cmbscno
            // 
            this.cmbscno.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbscno.FormattingEnabled = true;
            this.cmbscno.Location = new System.Drawing.Point(262, 8);
            this.cmbscno.Name = "cmbscno";
            this.cmbscno.Size = new System.Drawing.Size(89, 21);
            this.cmbscno.TabIndex = 1;
            this.cmbscno.SelectedIndexChanged += new System.EventHandler(this.cmbscno_SelectedIndexChanged);
            this.cmbscno.Enter += new System.EventHandler(this.cmbscno_Enter);
            this.cmbscno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbscno_KeyDown);
            this.cmbscno.Leave += new System.EventHandler(this.cmbscno_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(150, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Send To Company No.";
            // 
            // lblreceiveid
            // 
            this.lblreceiveid.AutoSize = true;
            this.lblreceiveid.Location = new System.Drawing.Point(696, 31);
            this.lblreceiveid.Name = "lblreceiveid";
            this.lblreceiveid.Size = new System.Drawing.Size(35, 13);
            this.lblreceiveid.TabIndex = 11;
            this.lblreceiveid.Text = "label7";
            this.lblreceiveid.Visible = false;
            // 
            // btndelete
            // 
            this.btndelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btndelete.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btndelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndelete.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndelete.ForeColor = System.Drawing.Color.White;
            this.btndelete.Location = new System.Drawing.Point(876, 5);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(97, 34);
            this.btndelete.TabIndex = 7;
            this.btndelete.Text = "Delete";
            this.btndelete.UseVisualStyleBackColor = false;
            this.btndelete.Click += new System.EventHandler(this.btndelete_Click);
            this.btndelete.Enter += new System.EventHandler(this.btndelete_Enter);
            this.btndelete.Leave += new System.EventHandler(this.btndelete_Leave);
            this.btndelete.MouseEnter += new System.EventHandler(this.btndelete_MouseEnter);
            this.btndelete.MouseLeave += new System.EventHandler(this.btndelete_MouseLeave);
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(45, 9);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(94, 20);
            this.dtpDate.TabIndex = 0;
            this.dtpDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDate_KeyDown);
            // 
            // btnprint
            // 
            this.btnprint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnprint.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprint.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(773, 43);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(97, 34);
            this.btnprint.TabIndex = 8;
            this.btnprint.Text = "Print";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Visible = false;
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
            this.btnclose.Location = new System.Drawing.Point(876, 44);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(97, 34);
            this.btnclose.TabIndex = 9;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            this.btnclose.Enter += new System.EventHandler(this.btnclose_Enter);
            this.btnclose.Leave += new System.EventHandler(this.btnclose_Leave);
            this.btnclose.MouseEnter += new System.EventHandler(this.btnclose_MouseEnter);
            this.btnclose.MouseLeave += new System.EventHandler(this.btnclose_MouseLeave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Date";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(773, 5);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(97, 34);
            this.btnSubmit.TabIndex = 6;
            this.btnSubmit.Text = "&Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            this.btnSubmit.Enter += new System.EventHandler(this.btnSubmit_Enter);
            this.btnSubmit.Leave += new System.EventHandler(this.btnSubmit_Leave);
            this.btnSubmit.MouseEnter += new System.EventHandler(this.btnSubmit_MouseEnter);
            this.btnSubmit.MouseLeave += new System.EventHandler(this.btnSubmit_MouseLeave);
            // 
            // txtNewSerialNo
            // 
            this.txtNewSerialNo.Location = new System.Drawing.Point(511, 45);
            this.txtNewSerialNo.Name = "txtNewSerialNo";
            this.txtNewSerialNo.Size = new System.Drawing.Size(256, 20);
            this.txtNewSerialNo.TabIndex = 5;
            this.txtNewSerialNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNewSerialNo_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(430, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "New Serial No.";
            // 
            // rdbMaxRepair
            // 
            this.rdbMaxRepair.AutoSize = true;
            this.rdbMaxRepair.Location = new System.Drawing.Point(159, 70);
            this.rdbMaxRepair.Name = "rdbMaxRepair";
            this.rdbMaxRepair.Size = new System.Drawing.Size(140, 17);
            this.rdbMaxRepair.TabIndex = 4;
            this.rdbMaxRepair.Text = "Receive By Complain ID";
            this.rdbMaxRepair.UseVisualStyleBackColor = true;
            this.rdbMaxRepair.CheckedChanged += new System.EventHandler(this.rdbMaxRepair_CheckedChanged);
            // 
            // rdbCompanyRepair
            // 
            this.rdbCompanyRepair.AutoSize = true;
            this.rdbCompanyRepair.Checked = true;
            this.rdbCompanyRepair.Location = new System.Drawing.Point(13, 70);
            this.rdbCompanyRepair.Name = "rdbCompanyRepair";
            this.rdbCompanyRepair.Size = new System.Drawing.Size(144, 17);
            this.rdbCompanyRepair.TabIndex = 3;
            this.rdbCompanyRepair.TabStop = true;
            this.rdbCompanyRepair.Text = "Receive By Company No";
            this.rdbCompanyRepair.UseVisualStyleBackColor = true;
            this.rdbCompanyRepair.CheckedChanged += new System.EventHandler(this.rdbCompanyRepair_CheckedChanged);
            // 
            // cmbSupplierName
            // 
            this.cmbSupplierName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplierName.FormattingEnabled = true;
            this.cmbSupplierName.Location = new System.Drawing.Point(93, 43);
            this.cmbSupplierName.Name = "cmbSupplierName";
            this.cmbSupplierName.Size = new System.Drawing.Size(325, 21);
            this.cmbSupplierName.TabIndex = 4;
            this.cmbSupplierName.SelectedIndexChanged += new System.EventHandler(this.cmbSupplierName_SelectedIndexChanged);
            this.cmbSupplierName.Enter += new System.EventHandler(this.cmbSupplierName_Enter);
            this.cmbSupplierName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSupplierName_KeyDown);
            this.cmbSupplierName.Leave += new System.EventHandler(this.cmbSupplierName_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Supplier Name";
            // 
            // cmbSerialNo
            // 
            this.cmbSerialNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSerialNo.FormattingEnabled = true;
            this.cmbSerialNo.Location = new System.Drawing.Point(647, 9);
            this.cmbSerialNo.Name = "cmbSerialNo";
            this.cmbSerialNo.Size = new System.Drawing.Size(121, 21);
            this.cmbSerialNo.TabIndex = 3;
            this.cmbSerialNo.SelectedIndexChanged += new System.EventHandler(this.cmbSerialNo_SelectedIndexChanged);
            this.cmbSerialNo.Enter += new System.EventHandler(this.cmbSerialNo_Enter);
            this.cmbSerialNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSerialNo_KeyDown);
            this.cmbSerialNo.Leave += new System.EventHandler(this.cmbSerialNo_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(589, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Serial No.";
            // 
            // cmbComplainId
            // 
            this.cmbComplainId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComplainId.FormattingEnabled = true;
            this.cmbComplainId.Location = new System.Drawing.Point(441, 9);
            this.cmbComplainId.Name = "cmbComplainId";
            this.cmbComplainId.Size = new System.Drawing.Size(143, 21);
            this.cmbComplainId.TabIndex = 2;
            this.cmbComplainId.SelectedIndexChanged += new System.EventHandler(this.cmbComplainId_SelectedIndexChanged);
            this.cmbComplainId.Enter += new System.EventHandler(this.cmbComplainId_Enter);
            this.cmbComplainId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbComplainId_KeyDown);
            this.cmbComplainId.Leave += new System.EventHandler(this.cmbComplainId_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(357, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
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
            this.TextBox1.Size = new System.Drawing.Size(992, 31);
            this.TextBox1.TabIndex = 13;
            this.TextBox1.TabStop = false;
            this.TextBox1.Text = "RECEIVE FROM COMPANY";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lvcompany);
            this.panel2.Location = new System.Drawing.Point(3, 107);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(984, 251);
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
            this.lvcompany.Location = new System.Drawing.Point(3, 4);
            this.lvcompany.MultiSelect = false;
            this.lvcompany.Name = "lvcompany";
            this.lvcompany.Size = new System.Drawing.Size(978, 244);
            this.lvcompany.TabIndex = 0;
            this.lvcompany.UseCompatibleStateImageBehavior = false;
            this.lvcompany.View = System.Windows.Forms.View.Details;
            this.lvcompany.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvcompany_KeyDown);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtRemarks);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Location = new System.Drawing.Point(3, 364);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(984, 100);
            this.panel3.TabIndex = 2;
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(73, 12);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(887, 62);
            this.txtRemarks.TabIndex = 0;
            this.txtRemarks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemarks_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Remarks:";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Location = new System.Drawing.Point(0, 31);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(992, 471);
            this.panel4.TabIndex = 0;
            // 
            // frmReceiveFromCompany
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1004, 510);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.panel4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmReceiveFromCompany";
            this.Text = "Receive From Company";
            this.Load += new System.EventHandler(this.frmReceiveFromCompany_Load);
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
        private System.Windows.Forms.ComboBox cmbComplainId;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox TextBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbSerialNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbSupplierName;
        private System.Windows.Forms.RadioButton rdbCompanyRepair;
        private System.Windows.Forms.RadioButton rdbMaxRepair;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNewSerialNo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel4;
        internal System.Windows.Forms.Button btnSubmit;
        internal System.Windows.Forms.Button btnclose;
        internal System.Windows.Forms.Button btndelete;
        internal System.Windows.Forms.Button btnprint;
        internal System.Windows.Forms.ListView lvcompany;
        private System.Windows.Forms.Label lblreceiveid;
        private System.Windows.Forms.ComboBox cmbscno;
        private System.Windows.Forms.Label label7;
    }
}