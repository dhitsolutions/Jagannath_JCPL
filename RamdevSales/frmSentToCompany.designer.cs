namespace RamdevSales
{
    partial class frmSentToCompany
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSentToCompany));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbSerialNo = new System.Windows.Forms.ComboBox();
            this.cmbComplainId = new System.Windows.Forms.ComboBox();
            this.txtComplainId = new System.Windows.Forms.TextBox();
            this.btnEditPartyName = new System.Windows.Forms.Button();
            this.btnAddPartyName = new System.Windows.Forms.Button();
            this.cmbSupplierName = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnclose = new System.Windows.Forms.Button();
            this.btndelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnprint = new System.Windows.Forms.Button();
            this.btnAddToList = new System.Windows.Forms.Button();
            this.dtdate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lvcompany = new System.Windows.Forms.ListView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTransportDetail = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbSerialNo);
            this.panel1.Controls.Add(this.cmbComplainId);
            this.panel1.Controls.Add(this.txtComplainId);
            this.panel1.Controls.Add(this.btnEditPartyName);
            this.panel1.Controls.Add(this.btnAddPartyName);
            this.panel1.Controls.Add(this.cmbSupplierName);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.btnclose);
            this.panel1.Controls.Add(this.btndelete);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnprint);
            this.panel1.Controls.Add(this.btnAddToList);
            this.panel1.Controls.Add(this.dtdate);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(956, 116);
            this.panel1.TabIndex = 0;
            // 
            // cmbSerialNo
            // 
            this.cmbSerialNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmbSerialNo.BackColor = System.Drawing.Color.AntiqueWhite;
            this.cmbSerialNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSerialNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSerialNo.FormattingEnabled = true;
            this.cmbSerialNo.Location = new System.Drawing.Point(366, 50);
            this.cmbSerialNo.Name = "cmbSerialNo";
            this.cmbSerialNo.Size = new System.Drawing.Size(121, 24);
            this.cmbSerialNo.TabIndex = 4;
            this.cmbSerialNo.SelectedIndexChanged += new System.EventHandler(this.cmbSerialNo_SelectedIndexChanged);
            this.cmbSerialNo.Enter += new System.EventHandler(this.cmbSerialNo_Enter);
            this.cmbSerialNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSerialNo_KeyDown);
            this.cmbSerialNo.Leave += new System.EventHandler(this.cmbSerialNo_Leave);
            // 
            // cmbComplainId
            // 
            this.cmbComplainId.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmbComplainId.BackColor = System.Drawing.Color.AntiqueWhite;
            this.cmbComplainId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComplainId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbComplainId.FormattingEnabled = true;
            this.cmbComplainId.Location = new System.Drawing.Point(100, 48);
            this.cmbComplainId.Name = "cmbComplainId";
            this.cmbComplainId.Size = new System.Drawing.Size(195, 24);
            this.cmbComplainId.TabIndex = 3;
            this.cmbComplainId.SelectedIndexChanged += new System.EventHandler(this.cmbComplainId_SelectedIndexChanged);
            this.cmbComplainId.Enter += new System.EventHandler(this.cmbComplainId_Enter);
            this.cmbComplainId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbComplainId_KeyDown);
            this.cmbComplainId.Leave += new System.EventHandler(this.cmbComplainId_Leave);
            // 
            // txtComplainId
            // 
            this.txtComplainId.BackColor = System.Drawing.Color.White;
            this.txtComplainId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtComplainId.Enabled = false;
            this.txtComplainId.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtComplainId.ForeColor = System.Drawing.Color.Black;
            this.txtComplainId.Location = new System.Drawing.Point(41, 8);
            this.txtComplainId.Name = "txtComplainId";
            this.txtComplainId.ReadOnly = true;
            this.txtComplainId.Size = new System.Drawing.Size(110, 23);
            this.txtComplainId.TabIndex = 0;
            this.txtComplainId.TabStop = false;
            this.txtComplainId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnEditPartyName
            // 
            this.btnEditPartyName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEditPartyName.BackgroundImage")));
            this.btnEditPartyName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditPartyName.ForeColor = System.Drawing.Color.White;
            this.btnEditPartyName.Location = new System.Drawing.Point(705, 8);
            this.btnEditPartyName.Name = "btnEditPartyName";
            this.btnEditPartyName.Size = new System.Drawing.Size(23, 22);
            this.btnEditPartyName.TabIndex = 284;
            this.btnEditPartyName.TabStop = false;
            this.btnEditPartyName.UseVisualStyleBackColor = true;
            this.btnEditPartyName.Click += new System.EventHandler(this.btnEditPartyName_Click);
            // 
            // btnAddPartyName
            // 
            this.btnAddPartyName.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnAddPartyName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddPartyName.BackgroundImage")));
            this.btnAddPartyName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddPartyName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddPartyName.ForeColor = System.Drawing.Color.White;
            this.btnAddPartyName.Location = new System.Drawing.Point(686, 9);
            this.btnAddPartyName.Name = "btnAddPartyName";
            this.btnAddPartyName.Size = new System.Drawing.Size(22, 22);
            this.btnAddPartyName.TabIndex = 283;
            this.btnAddPartyName.TabStop = false;
            this.btnAddPartyName.Text = "&Add";
            this.btnAddPartyName.UseVisualStyleBackColor = false;
            this.btnAddPartyName.Click += new System.EventHandler(this.btnAddPartyName_Click);
            // 
            // cmbSupplierName
            // 
            this.cmbSupplierName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmbSupplierName.BackColor = System.Drawing.Color.AntiqueWhite;
            this.cmbSupplierName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplierName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSupplierName.FormattingEnabled = true;
            this.cmbSupplierName.Location = new System.Drawing.Point(405, 7);
            this.cmbSupplierName.Name = "cmbSupplierName";
            this.cmbSupplierName.Size = new System.Drawing.Size(277, 24);
            this.cmbSupplierName.TabIndex = 2;
            this.cmbSupplierName.SelectedIndexChanged += new System.EventHandler(this.cmbSupplierName_SelectedIndexChanged);
            this.cmbSupplierName.Enter += new System.EventHandler(this.cmbSupplierName_Enter);
            this.cmbSupplierName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSupplierName_KeyDown);
            this.cmbSupplierName.Leave += new System.EventHandler(this.cmbSupplierName_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(14, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 16);
            this.label8.TabIndex = 19;
            this.label8.Text = "No";
            // 
            // btnclose
            // 
            this.btnclose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnclose.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(842, 52);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(97, 34);
            this.btnclose.TabIndex = 8;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            this.btnclose.Enter += new System.EventHandler(this.btnclose_Enter);
            this.btnclose.Leave += new System.EventHandler(this.btnclose_Leave);
            this.btnclose.MouseEnter += new System.EventHandler(this.btnclose_MouseEnter);
            this.btnclose.MouseLeave += new System.EventHandler(this.btnclose_MouseLeave);
            // 
            // btndelete
            // 
            this.btndelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btndelete.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btndelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndelete.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndelete.ForeColor = System.Drawing.Color.White;
            this.btndelete.Location = new System.Drawing.Point(842, 8);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(97, 34);
            this.btndelete.TabIndex = 9;
            this.btndelete.Text = "Delete";
            this.btndelete.UseVisualStyleBackColor = false;
            this.btndelete.Click += new System.EventHandler(this.btndelete_Click);
            this.btndelete.Enter += new System.EventHandler(this.btndelete_Enter);
            this.btndelete.Leave += new System.EventHandler(this.btndelete_Leave);
            this.btndelete.MouseEnter += new System.EventHandler(this.btndelete_MouseEnter);
            this.btndelete.MouseLeave += new System.EventHandler(this.btndelete_MouseLeave);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(730, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(97, 34);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "&Submit";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnSave.Enter += new System.EventHandler(this.btnSave_Enter);
            this.btnSave.Leave += new System.EventHandler(this.btnSave_Leave);
            this.btnSave.MouseEnter += new System.EventHandler(this.btnSave_MouseEnter);
            this.btnSave.MouseLeave += new System.EventHandler(this.btnSave_MouseLeave);
            // 
            // btnprint
            // 
            this.btnprint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnprint.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprint.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(730, 52);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(97, 34);
            this.btnprint.TabIndex = 7;
            this.btnprint.Text = "Print";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            this.btnprint.Enter += new System.EventHandler(this.btnprint_Enter);
            this.btnprint.Leave += new System.EventHandler(this.btnprint_Leave);
            this.btnprint.MouseEnter += new System.EventHandler(this.btnprint_MouseEnter);
            this.btnprint.MouseLeave += new System.EventHandler(this.btnprint_MouseLeave);
            // 
            // btnAddToList
            // 
            this.btnAddToList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddToList.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnAddToList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddToList.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddToList.ForeColor = System.Drawing.Color.White;
            this.btnAddToList.Location = new System.Drawing.Point(509, 47);
            this.btnAddToList.Name = "btnAddToList";
            this.btnAddToList.Size = new System.Drawing.Size(78, 31);
            this.btnAddToList.TabIndex = 5;
            this.btnAddToList.Text = "Add";
            this.btnAddToList.UseVisualStyleBackColor = false;
            this.btnAddToList.Click += new System.EventHandler(this.btnAddToList_Click);
            // 
            // dtdate
            // 
            this.dtdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtdate.Location = new System.Drawing.Point(195, 9);
            this.dtdate.Name = "dtdate";
            this.dtdate.Size = new System.Drawing.Size(103, 20);
            this.dtdate.TabIndex = 1;
            this.dtdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtdate_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(153, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(304, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Supplier Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(298, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Serial No.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Complain Id";
            // 
            // TextBox1
            // 
            this.TextBox1.BackColor = System.Drawing.SystemColors.Highlight;
            this.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBox1.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox1.ForeColor = System.Drawing.Color.White;
            this.TextBox1.Location = new System.Drawing.Point(0, 1);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = true;
            this.TextBox1.Size = new System.Drawing.Size(964, 31);
            this.TextBox1.TabIndex = 0;
            this.TextBox1.TabStop = false;
            this.TextBox1.Text = "SEND TO COMPANY";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lvcompany);
            this.panel2.Location = new System.Drawing.Point(3, 125);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(956, 202);
            this.panel2.TabIndex = 15;
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
            this.lvcompany.Location = new System.Drawing.Point(3, 3);
            this.lvcompany.MultiSelect = false;
            this.lvcompany.Name = "lvcompany";
            this.lvcompany.Size = new System.Drawing.Size(950, 196);
            this.lvcompany.TabIndex = 0;
            this.lvcompany.UseCompatibleStateImageBehavior = false;
            this.lvcompany.View = System.Windows.Forms.View.Details;
            this.lvcompany.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvcompany_KeyDown);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtRemarks);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.txtTransportDetail);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Location = new System.Drawing.Point(3, 333);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(953, 117);
            this.panel3.TabIndex = 16;
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(131, 59);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(808, 50);
            this.txtRemarks.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(14, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 16);
            this.label6.TabIndex = 7;
            this.label6.Text = "Remarks";
            // 
            // txtTransportDetail
            // 
            this.txtTransportDetail.Location = new System.Drawing.Point(131, 3);
            this.txtTransportDetail.Multiline = true;
            this.txtTransportDetail.Name = "txtTransportDetail";
            this.txtTransportDetail.Size = new System.Drawing.Size(808, 50);
            this.txtTransportDetail.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Transport Detail";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Location = new System.Drawing.Point(0, 32);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(964, 457);
            this.panel4.TabIndex = 0;
            // 
            // frmSentToCompany
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(976, 502);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.TextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSentToCompany";
            this.Text = "Sent To Company";
            this.Load += new System.EventHandler(this.frmSentToCompany_Load);
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
        internal System.Windows.Forms.TextBox TextBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtdate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTransportDetail;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRemarks;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.Button btnclose;
        internal System.Windows.Forms.Button btndelete;
        internal System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.Panel panel4;
        internal System.Windows.Forms.Button btnAddToList;
        private System.Windows.Forms.Label label8;
        internal System.Windows.Forms.ListView lvcompany;
        private System.Windows.Forms.Button btnEditPartyName;
        internal System.Windows.Forms.Button btnAddPartyName;
        private System.Windows.Forms.ComboBox cmbSupplierName;
        internal System.Windows.Forms.TextBox txtComplainId;
        private System.Windows.Forms.ComboBox cmbSerialNo;
        private System.Windows.Forms.ComboBox cmbComplainId;
    }
}