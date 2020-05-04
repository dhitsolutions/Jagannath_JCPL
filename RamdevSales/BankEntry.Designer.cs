namespace RamdevSales
{
    partial class BankEntry
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BankEntry));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbldate = new System.Windows.Forms.Label();
            this.lblvno = new System.Windows.Forms.Label();
            this.cmbRemark = new System.Windows.Forms.TextBox();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.lbltotalamt = new System.Windows.Forms.Label();
            this.txtExpences = new System.Windows.Forms.TextBox();
            this.lblexpe = new System.Windows.Forms.Label();
            this.btncancel = new System.Windows.Forms.Button();
            this.btnEditBank = new System.Windows.Forms.Button();
            this.btnAddBank = new System.Windows.Forms.Button();
            this.btnPartyEdit = new System.Windows.Forms.Button();
            this.btnAddParty = new System.Windows.Forms.Button();
            this.btndelete = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.lvserial = new System.Windows.Forms.ListView();
            this.BtnSubmit = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDated = new System.Windows.Forms.TextBox();
            this.lbldated = new System.Windows.Forms.Label();
            this.txtChequeNo = new System.Windows.Forms.TextBox();
            this.lblcheque = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbPartyName = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbEntry = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbBankSelect = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtRundate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtheader = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lbldate);
            this.panel1.Controls.Add(this.lblvno);
            this.panel1.Controls.Add(this.cmbRemark);
            this.panel1.Controls.Add(this.txtTotalAmount);
            this.panel1.Controls.Add(this.lbltotalamt);
            this.panel1.Controls.Add(this.txtExpences);
            this.panel1.Controls.Add(this.lblexpe);
            this.panel1.Controls.Add(this.btncancel);
            this.panel1.Controls.Add(this.btnEditBank);
            this.panel1.Controls.Add(this.btnAddBank);
            this.panel1.Controls.Add(this.btnPartyEdit);
            this.panel1.Controls.Add(this.btnAddParty);
            this.panel1.Controls.Add(this.btndelete);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.lvserial);
            this.panel1.Controls.Add(this.BtnSubmit);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txtDated);
            this.panel1.Controls.Add(this.lbldated);
            this.panel1.Controls.Add(this.txtChequeNo);
            this.panel1.Controls.Add(this.lblcheque);
            this.panel1.Controls.Add(this.txtAmount);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cmbPartyName);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cmbEntry);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cmbBankSelect);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.TxtRundate);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtheader);
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1039, 497);
            this.panel1.TabIndex = 0;
            // 
            // lbldate
            // 
            this.lbldate.AutoSize = true;
            this.lbldate.ForeColor = System.Drawing.Color.Red;
            this.lbldate.Location = new System.Drawing.Point(412, 138);
            this.lbldate.Name = "lbldate";
            this.lbldate.Size = new System.Drawing.Size(73, 13);
            this.lbldate.TabIndex = 269;
            this.lbldate.Text = "(dd/MM/yyyy)";
            this.lbldate.Visible = false;
            // 
            // lblvno
            // 
            this.lblvno.AutoSize = true;
            this.lblvno.Location = new System.Drawing.Point(489, 103);
            this.lblvno.Name = "lblvno";
            this.lblvno.Size = new System.Drawing.Size(35, 13);
            this.lblvno.TabIndex = 268;
            this.lblvno.Text = "lblvno";
            this.lblvno.Visible = false;
            // 
            // cmbRemark
            // 
            this.cmbRemark.Location = new System.Drawing.Point(531, 116);
            this.cmbRemark.Name = "cmbRemark";
            this.cmbRemark.Size = new System.Drawing.Size(369, 20);
            this.cmbRemark.TabIndex = 9;
            this.cmbRemark.Enter += new System.EventHandler(this.cmbRemark_Enter);
            this.cmbRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbRemark_KeyDown);
            this.cmbRemark.Leave += new System.EventHandler(this.cmbRemark_Leave);
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Location = new System.Drawing.Point(901, 81);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.Size = new System.Drawing.Size(130, 20);
            this.txtTotalAmount.TabIndex = 6;
            this.txtTotalAmount.Visible = false;
            this.txtTotalAmount.Enter += new System.EventHandler(this.txtTotalAmount_Enter);
            this.txtTotalAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTotalAmount_KeyDown);
            this.txtTotalAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTotalAmount_KeyPress);
            this.txtTotalAmount.Leave += new System.EventHandler(this.txtTotalAmount_Leave);
            // 
            // lbltotalamt
            // 
            this.lbltotalamt.AutoSize = true;
            this.lbltotalamt.Location = new System.Drawing.Point(830, 83);
            this.lbltotalamt.Name = "lbltotalamt";
            this.lbltotalamt.Size = new System.Drawing.Size(70, 13);
            this.lbltotalamt.TabIndex = 267;
            this.lbltotalamt.Text = "Total Amount";
            this.lbltotalamt.Visible = false;
            // 
            // txtExpences
            // 
            this.txtExpences.ForeColor = System.Drawing.Color.Red;
            this.txtExpences.Location = new System.Drawing.Point(723, 80);
            this.txtExpences.Name = "txtExpences";
            this.txtExpences.Size = new System.Drawing.Size(107, 20);
            this.txtExpences.TabIndex = 5;
            this.txtExpences.Text = "0";
            this.txtExpences.Visible = false;
            this.txtExpences.TextChanged += new System.EventHandler(this.txtExpences_TextChanged);
            this.txtExpences.Enter += new System.EventHandler(this.txtExpences_Enter);
            this.txtExpences.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtExpences_KeyDown);
            this.txtExpences.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtExpences_KeyPress);
            this.txtExpences.Leave += new System.EventHandler(this.txtExpences_Leave);
            // 
            // lblexpe
            // 
            this.lblexpe.AutoSize = true;
            this.lblexpe.Location = new System.Drawing.Point(667, 82);
            this.lblexpe.Name = "lblexpe";
            this.lblexpe.Size = new System.Drawing.Size(54, 13);
            this.lblexpe.TabIndex = 265;
            this.lblexpe.Text = "Expences";
            this.lblexpe.Visible = false;
            // 
            // btncancel
            // 
            this.btncancel.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btncancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncancel.ForeColor = System.Drawing.Color.White;
            this.btncancel.Location = new System.Drawing.Point(924, 454);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(97, 38);
            this.btncancel.TabIndex = 14;
            this.btncancel.Text = "&Close";
            this.btncancel.UseVisualStyleBackColor = false;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            this.btncancel.Enter += new System.EventHandler(this.btncancel_Enter);
            this.btncancel.Leave += new System.EventHandler(this.btncancel_Leave);
            this.btncancel.MouseEnter += new System.EventHandler(this.btncancel_MouseEnter);
            this.btncancel.MouseLeave += new System.EventHandler(this.btncancel_MouseLeave);
            // 
            // btnEditBank
            // 
            this.btnEditBank.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEditBank.BackgroundImage")));
            this.btnEditBank.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditBank.ForeColor = System.Drawing.Color.White;
            this.btnEditBank.Location = new System.Drawing.Point(644, 43);
            this.btnEditBank.Name = "btnEditBank";
            this.btnEditBank.Size = new System.Drawing.Size(23, 22);
            this.btnEditBank.TabIndex = 264;
            this.btnEditBank.TabStop = false;
            this.btnEditBank.UseVisualStyleBackColor = true;
            this.btnEditBank.Click += new System.EventHandler(this.btnEditBank_Click);
            // 
            // btnAddBank
            // 
            this.btnAddBank.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnAddBank.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddBank.BackgroundImage")));
            this.btnAddBank.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddBank.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddBank.ForeColor = System.Drawing.Color.White;
            this.btnAddBank.Location = new System.Drawing.Point(625, 44);
            this.btnAddBank.Name = "btnAddBank";
            this.btnAddBank.Size = new System.Drawing.Size(22, 22);
            this.btnAddBank.TabIndex = 263;
            this.btnAddBank.TabStop = false;
            this.btnAddBank.Text = "&Add";
            this.btnAddBank.UseVisualStyleBackColor = false;
            this.btnAddBank.Click += new System.EventHandler(this.btnAddBank_Click);
            // 
            // btnPartyEdit
            // 
            this.btnPartyEdit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPartyEdit.BackgroundImage")));
            this.btnPartyEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPartyEdit.ForeColor = System.Drawing.Color.White;
            this.btnPartyEdit.Location = new System.Drawing.Point(462, 78);
            this.btnPartyEdit.Name = "btnPartyEdit";
            this.btnPartyEdit.Size = new System.Drawing.Size(23, 22);
            this.btnPartyEdit.TabIndex = 259;
            this.btnPartyEdit.TabStop = false;
            this.btnPartyEdit.UseVisualStyleBackColor = true;
            this.btnPartyEdit.Click += new System.EventHandler(this.btnPartyEdit_Click);
            // 
            // btnAddParty
            // 
            this.btnAddParty.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnAddParty.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddParty.BackgroundImage")));
            this.btnAddParty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddParty.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddParty.ForeColor = System.Drawing.Color.White;
            this.btnAddParty.Location = new System.Drawing.Point(443, 79);
            this.btnAddParty.Name = "btnAddParty";
            this.btnAddParty.Size = new System.Drawing.Size(22, 22);
            this.btnAddParty.TabIndex = 258;
            this.btnAddParty.TabStop = false;
            this.btnAddParty.Text = "&Add";
            this.btnAddParty.UseVisualStyleBackColor = false;
            this.btnAddParty.Click += new System.EventHandler(this.btnAddParty_Click);
            // 
            // btndelete
            // 
            this.btndelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btndelete.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btndelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndelete.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndelete.ForeColor = System.Drawing.Color.White;
            this.btndelete.Location = new System.Drawing.Point(805, 453);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(107, 39);
            this.btndelete.TabIndex = 13;
            this.btndelete.Text = "&Delete";
            this.btndelete.UseVisualStyleBackColor = false;
            this.btndelete.Click += new System.EventHandler(this.btndelete_Click);
            this.btndelete.Enter += new System.EventHandler(this.btndelete_Enter);
            this.btndelete.Leave += new System.EventHandler(this.btndelete_Leave);
            this.btndelete.MouseEnter += new System.EventHandler(this.btndelete_MouseEnter);
            this.btndelete.MouseLeave += new System.EventHandler(this.btndelete_MouseLeave);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(687, 453);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(107, 39);
            this.btnPrint.TabIndex = 12;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            this.btnPrint.Enter += new System.EventHandler(this.btnPrint_Enter);
            this.btnPrint.Leave += new System.EventHandler(this.btnPrint_Leave);
            this.btnPrint.MouseEnter += new System.EventHandler(this.btnPrint_MouseEnter);
            this.btnPrint.MouseLeave += new System.EventHandler(this.btnPrint_MouseLeave);
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
            this.lvserial.Location = new System.Drawing.Point(19, 153);
            this.lvserial.MultiSelect = false;
            this.lvserial.Name = "lvserial";
            this.lvserial.Size = new System.Drawing.Size(1011, 297);
            this.lvserial.TabIndex = 11;
            this.lvserial.UseCompatibleStateImageBehavior = false;
            this.lvserial.View = System.Windows.Forms.View.Details;
            this.lvserial.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvserial_KeyDown);
            this.lvserial.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvserial_MouseDoubleClick);
            // 
            // BtnSubmit
            // 
            this.BtnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSubmit.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BtnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSubmit.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSubmit.ForeColor = System.Drawing.Color.White;
            this.BtnSubmit.Location = new System.Drawing.Point(932, 108);
            this.BtnSubmit.Name = "BtnSubmit";
            this.BtnSubmit.Size = new System.Drawing.Size(93, 39);
            this.BtnSubmit.TabIndex = 10;
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
            this.label8.Location = new System.Drawing.Point(481, 119);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 189;
            this.label8.Text = "Remark";
            // 
            // txtDated
            // 
            this.txtDated.Location = new System.Drawing.Point(333, 117);
            this.txtDated.Name = "txtDated";
            this.txtDated.Size = new System.Drawing.Size(144, 20);
            this.txtDated.TabIndex = 8;
            this.txtDated.Visible = false;
            this.txtDated.Enter += new System.EventHandler(this.txtDated_Enter);
            this.txtDated.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDated_KeyDown);
            this.txtDated.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDated_KeyPress);
            this.txtDated.Leave += new System.EventHandler(this.txtDated_Leave);
            // 
            // lbldated
            // 
            this.lbldated.AutoSize = true;
            this.lbldated.Location = new System.Drawing.Point(294, 119);
            this.lbldated.Name = "lbldated";
            this.lbldated.Size = new System.Drawing.Size(36, 13);
            this.lbldated.TabIndex = 187;
            this.lbldated.Text = "Dated";
            this.lbldated.Visible = false;
            // 
            // txtChequeNo
            // 
            this.txtChequeNo.Location = new System.Drawing.Point(110, 117);
            this.txtChequeNo.Name = "txtChequeNo";
            this.txtChequeNo.Size = new System.Drawing.Size(178, 20);
            this.txtChequeNo.TabIndex = 7;
            this.txtChequeNo.Visible = false;
            this.txtChequeNo.Enter += new System.EventHandler(this.txtChequeNo_Enter);
            this.txtChequeNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChequeNo_KeyDown);
            this.txtChequeNo.Leave += new System.EventHandler(this.txtChequeNo_Leave);
            // 
            // lblcheque
            // 
            this.lblcheque.AutoSize = true;
            this.lblcheque.Location = new System.Drawing.Point(16, 120);
            this.lblcheque.Name = "lblcheque";
            this.lblcheque.Size = new System.Drawing.Size(91, 13);
            this.lblcheque.TabIndex = 185;
            this.lblcheque.Text = "Cheque / DD No.";
            this.lblcheque.Visible = false;
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(531, 80);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(130, 20);
            this.txtAmount.TabIndex = 4;
            this.txtAmount.TextChanged += new System.EventHandler(this.txtAmount_TextChanged);
            this.txtAmount.Enter += new System.EventHandler(this.txtAmount_Enter);
            this.txtAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAmount_KeyDown);
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            this.txtAmount.Leave += new System.EventHandler(this.txtAmount_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(483, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 183;
            this.label5.Text = "Amount";
            // 
            // cmbPartyName
            // 
            this.cmbPartyName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbPartyName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPartyName.BackColor = System.Drawing.SystemColors.Window;
            this.cmbPartyName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPartyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPartyName.FormattingEnabled = true;
            this.cmbPartyName.Location = new System.Drawing.Point(81, 77);
            this.cmbPartyName.Name = "cmbPartyName";
            this.cmbPartyName.Size = new System.Drawing.Size(360, 24);
            this.cmbPartyName.TabIndex = 3;
            this.cmbPartyName.SelectedIndexChanged += new System.EventHandler(this.cmbPartyName_SelectedIndexChanged);
            this.cmbPartyName.Enter += new System.EventHandler(this.cmbPartyName_Enter);
            this.cmbPartyName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbPartyName_KeyDown);
            this.cmbPartyName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbPartyName_KeyUp);
            this.cmbPartyName.Leave += new System.EventHandler(this.cmbPartyName_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 181;
            this.label4.Text = "Party Name";
            // 
            // cmbEntry
            // 
            this.cmbEntry.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbEntry.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbEntry.BackColor = System.Drawing.SystemColors.Window;
            this.cmbEntry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEntry.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEntry.FormattingEnabled = true;
            this.cmbEntry.Items.AddRange(new object[] {
            "Cheque Issued",
            "Draft Issued",
            "Cheque/Draft/Rtgs Received",
            "Deposit Cash Into Bank",
            "Withdraw Cash from Bank",
            "Bank Expenses",
            "Online Transfer"});
            this.cmbEntry.Location = new System.Drawing.Point(710, 42);
            this.cmbEntry.Name = "cmbEntry";
            this.cmbEntry.Size = new System.Drawing.Size(320, 24);
            this.cmbEntry.TabIndex = 2;
            this.cmbEntry.SelectedIndexChanged += new System.EventHandler(this.cmbEntry_SelectedIndexChanged);
            this.cmbEntry.Enter += new System.EventHandler(this.cmbEntry_Enter);
            this.cmbEntry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbEntry_KeyDown);
            this.cmbEntry.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbEntry_KeyUp);
            this.cmbEntry.Leave += new System.EventHandler(this.cmbEntry_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(666, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 179;
            this.label3.Text = "Entry";
            // 
            // cmbBankSelect
            // 
            this.cmbBankSelect.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbBankSelect.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbBankSelect.BackColor = System.Drawing.SystemColors.Window;
            this.cmbBankSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBankSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBankSelect.FormattingEnabled = true;
            this.cmbBankSelect.Location = new System.Drawing.Point(245, 42);
            this.cmbBankSelect.Name = "cmbBankSelect";
            this.cmbBankSelect.Size = new System.Drawing.Size(378, 24);
            this.cmbBankSelect.TabIndex = 1;
            this.cmbBankSelect.SelectedIndexChanged += new System.EventHandler(this.cmbBankSelect_SelectedIndexChanged);
            this.cmbBankSelect.Enter += new System.EventHandler(this.cmbBankSelect_Enter);
            this.cmbBankSelect.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbBankSelect_KeyDown);
            this.cmbBankSelect.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbBankSelect_KeyUp);
            this.cmbBankSelect.Leave += new System.EventHandler(this.cmbBankSelect_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(174, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 177;
            this.label2.Text = "Select Bank";
            // 
            // TxtRundate
            // 
            this.TxtRundate.CustomFormat = "";
            this.TxtRundate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TxtRundate.Location = new System.Drawing.Point(51, 44);
            this.TxtRundate.Name = "TxtRundate";
            this.TxtRundate.Size = new System.Drawing.Size(103, 20);
            this.TxtRundate.TabIndex = 0;
            this.TxtRundate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtRundate_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 175;
            this.label1.Text = "Date";
            // 
            // txtheader
            // 
            this.txtheader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtheader.BackColor = System.Drawing.SystemColors.Highlight;
            this.txtheader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtheader.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtheader.ForeColor = System.Drawing.Color.White;
            this.txtheader.Location = new System.Drawing.Point(-1, -1);
            this.txtheader.Name = "txtheader";
            this.txtheader.ReadOnly = true;
            this.txtheader.Size = new System.Drawing.Size(1039, 31);
            this.txtheader.TabIndex = 174;
            this.txtheader.TabStop = false;
            this.txtheader.Text = "BANK ENTRY";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1500;
            // 
            // BankEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1044, 501);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BankEntry";
            this.Text = "BankEntry";
            this.Load += new System.EventHandler(this.BankEntry_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.TextBox txtheader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker TxtRundate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbBankSelect;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbEntry;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbPartyName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label lblcheque;
        private System.Windows.Forms.TextBox txtChequeNo;
        private System.Windows.Forms.Label lbldated;
        private System.Windows.Forms.TextBox txtDated;
        private System.Windows.Forms.Label label8;
        internal System.Windows.Forms.Button BtnSubmit;
        internal System.Windows.Forms.ListView lvserial;
        internal System.Windows.Forms.Button btnPrint;
        internal System.Windows.Forms.Button btndelete;
        private System.Windows.Forms.Button btnPartyEdit;
        internal System.Windows.Forms.Button btnAddParty;
        private System.Windows.Forms.Button btnEditBank;
        internal System.Windows.Forms.Button btnAddBank;
        private System.Windows.Forms.Button btncancel;
        private System.Windows.Forms.Label lblexpe;
        private System.Windows.Forms.TextBox txtExpences;
        private System.Windows.Forms.Label lbltotalamt;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.TextBox cmbRemark;
        private System.Windows.Forms.Label lblvno;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lbldate;
    }
}