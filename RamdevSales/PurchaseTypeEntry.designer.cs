namespace RamdevSales
{
    partial class PurchaseTypeEntry
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
            this.txttitle = new System.Windows.Forms.TextBox();
            this.txtgrop = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtprintname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbxttype = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbregion = new System.Windows.Forms.ComboBox();
            this.btndelete = new System.Windows.Forms.Button();
            this.btncancel = new System.Windows.Forms.Button();
            this.btnreset = new System.Windows.Forms.Button();
            this.btnsave = new System.Windows.Forms.Button();
            this.txtstartfrom = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtinvoiceprefix = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbtaxcal = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtinvoiceheading = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbbtdefault = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbformtype = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtecom = new System.Windows.Forms.TextBox();
            this.chkecom = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // txttitle
            // 
            this.txttitle.BackColor = System.Drawing.SystemColors.Highlight;
            this.txttitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txttitle.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttitle.ForeColor = System.Drawing.Color.White;
            this.txttitle.Location = new System.Drawing.Point(0, 0);
            this.txttitle.Name = "txttitle";
            this.txttitle.ReadOnly = true;
            this.txttitle.Size = new System.Drawing.Size(767, 31);
            this.txttitle.TabIndex = 138;
            this.txttitle.TabStop = false;
            this.txttitle.Text = "PURCHASE TYPE";
            // 
            // txtgrop
            // 
            this.txtgrop.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtgrop.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtgrop.BackColor = System.Drawing.SystemColors.Window;
            this.txtgrop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtgrop.FormattingEnabled = true;
            this.txtgrop.Location = new System.Drawing.Point(171, 54);
            this.txtgrop.Name = "txtgrop";
            this.txtgrop.Size = new System.Drawing.Size(291, 21);
            this.txtgrop.TabIndex = 2;
            this.txtgrop.SelectedIndexChanged += new System.EventHandler(this.txtgrop_SelectedIndexChanged);
            this.txtgrop.Enter += new System.EventHandler(this.txtgrop_Enter);
            this.txtgrop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtgrop_KeyDown);
            this.txtgrop.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtgrop_KeyUp);
            this.txtgrop.Leave += new System.EventHandler(this.txtgrop_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(16, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 17);
            this.label3.TabIndex = 142;
            this.label3.Text = "Purchase Account";
            // 
            // txtprintname
            // 
            this.txtprintname.BackColor = System.Drawing.Color.White;
            this.txtprintname.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtprintname.Location = new System.Drawing.Point(171, 26);
            this.txtprintname.Name = "txtprintname";
            this.txtprintname.Size = new System.Drawing.Size(292, 23);
            this.txtprintname.TabIndex = 0;
            this.txtprintname.Enter += new System.EventHandler(this.txtprintname_Enter);
            this.txtprintname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtprintname_KeyDown);
            this.txtprintname.Leave += new System.EventHandler(this.txtprintname_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(16, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 17);
            this.label2.TabIndex = 141;
            this.label2.Text = "Purchase Type Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(15, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 17);
            this.label1.TabIndex = 143;
            this.label1.Text = "Select Tax Type";
            // 
            // cmbxttype
            // 
            this.cmbxttype.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbxttype.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbxttype.BackColor = System.Drawing.SystemColors.Window;
            this.cmbxttype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxttype.FormattingEnabled = true;
            this.cmbxttype.Location = new System.Drawing.Point(171, 28);
            this.cmbxttype.Name = "cmbxttype";
            this.cmbxttype.Size = new System.Drawing.Size(222, 21);
            this.cmbxttype.TabIndex = 0;
            this.cmbxttype.SelectedIndexChanged += new System.EventHandler(this.cmbxttype_SelectedIndexChanged);
            this.cmbxttype.Enter += new System.EventHandler(this.cmbxttype_Enter);
            this.cmbxttype.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbxttype_KeyDown);
            this.cmbxttype.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbxttype_KeyUp);
            this.cmbxttype.Leave += new System.EventHandler(this.cmbxttype_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(477, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 17);
            this.label4.TabIndex = 146;
            this.label4.Text = "Region";
            // 
            // cmbregion
            // 
            this.cmbregion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbregion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbregion.BackColor = System.Drawing.SystemColors.Window;
            this.cmbregion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbregion.FormattingEnabled = true;
            this.cmbregion.Items.AddRange(new object[] {
            "Local",
            "central",
            "Export"});
            this.cmbregion.Location = new System.Drawing.Point(571, 27);
            this.cmbregion.Name = "cmbregion";
            this.cmbregion.Size = new System.Drawing.Size(174, 21);
            this.cmbregion.TabIndex = 1;
            this.cmbregion.SelectedIndexChanged += new System.EventHandler(this.cmbregion_SelectedIndexChanged);
            this.cmbregion.Enter += new System.EventHandler(this.cmbregion_Enter);
            this.cmbregion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbregion_KeyDown);
            this.cmbregion.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbregion_KeyUp);
            this.cmbregion.Leave += new System.EventHandler(this.cmbregion_Leave);
            // 
            // btndelete
            // 
            this.btndelete.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btndelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndelete.ForeColor = System.Drawing.Color.White;
            this.btndelete.Location = new System.Drawing.Point(519, 14);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(97, 34);
            this.btndelete.TabIndex = 14;
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
            this.btncancel.Location = new System.Drawing.Point(622, 14);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(97, 34);
            this.btncancel.TabIndex = 15;
            this.btncancel.Text = "&Close";
            this.btncancel.UseVisualStyleBackColor = false;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            this.btncancel.Enter += new System.EventHandler(this.btncancel_Enter);
            this.btncancel.Leave += new System.EventHandler(this.btncancel_Leave);
            this.btncancel.MouseEnter += new System.EventHandler(this.btncancel_MouseEnter);
            this.btncancel.MouseLeave += new System.EventHandler(this.btncancel_MouseLeave);
            // 
            // btnreset
            // 
            this.btnreset.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnreset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnreset.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnreset.ForeColor = System.Drawing.Color.White;
            this.btnreset.Location = new System.Drawing.Point(416, 14);
            this.btnreset.Name = "btnreset";
            this.btnreset.Size = new System.Drawing.Size(97, 34);
            this.btnreset.TabIndex = 13;
            this.btnreset.Text = "&Reset";
            this.btnreset.UseVisualStyleBackColor = false;
            this.btnreset.Click += new System.EventHandler(this.btnreset_Click);
            this.btnreset.Enter += new System.EventHandler(this.btnreset_Enter);
            this.btnreset.Leave += new System.EventHandler(this.btnreset_Leave);
            this.btnreset.MouseEnter += new System.EventHandler(this.btnreset_MouseEnter);
            this.btnreset.MouseLeave += new System.EventHandler(this.btnreset_MouseLeave);
            // 
            // btnsave
            // 
            this.btnsave.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnsave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsave.ForeColor = System.Drawing.Color.White;
            this.btnsave.Location = new System.Drawing.Point(313, 14);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(97, 34);
            this.btnsave.TabIndex = 12;
            this.btnsave.Text = "&Submit";
            this.btnsave.UseVisualStyleBackColor = false;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            this.btnsave.Enter += new System.EventHandler(this.btnsave_Enter);
            this.btnsave.Leave += new System.EventHandler(this.btnsave_Leave);
            this.btnsave.MouseEnter += new System.EventHandler(this.btnsave_MouseEnter);
            this.btnsave.MouseLeave += new System.EventHandler(this.btnsave_MouseLeave);
            // 
            // txtstartfrom
            // 
            this.txtstartfrom.BackColor = System.Drawing.Color.White;
            this.txtstartfrom.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtstartfrom.Location = new System.Drawing.Point(573, 29);
            this.txtstartfrom.Name = "txtstartfrom";
            this.txtstartfrom.Size = new System.Drawing.Size(173, 23);
            this.txtstartfrom.TabIndex = 9;
            this.txtstartfrom.Enter += new System.EventHandler(this.txtstartfrom_Enter);
            this.txtstartfrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtstartfrom_KeyDown);
            this.txtstartfrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtstartfrom_KeyPress);
            this.txtstartfrom.Leave += new System.EventHandler(this.txtstartfrom_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(482, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 17);
            this.label6.TabIndex = 159;
            this.label6.Text = "Start From";
            // 
            // txtinvoiceprefix
            // 
            this.txtinvoiceprefix.BackColor = System.Drawing.Color.White;
            this.txtinvoiceprefix.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtinvoiceprefix.Location = new System.Drawing.Point(171, 27);
            this.txtinvoiceprefix.Name = "txtinvoiceprefix";
            this.txtinvoiceprefix.Size = new System.Drawing.Size(222, 23);
            this.txtinvoiceprefix.TabIndex = 8;
            this.txtinvoiceprefix.Enter += new System.EventHandler(this.txtinvoiceprefix_Enter);
            this.txtinvoiceprefix.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtinvoiceprefix_KeyDown);
            this.txtinvoiceprefix.Leave += new System.EventHandler(this.txtinvoiceprefix_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(15, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 17);
            this.label5.TabIndex = 157;
            this.label5.Text = "Invoice No. Prefix";
            // 
            // cmbtaxcal
            // 
            this.cmbtaxcal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbtaxcal.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbtaxcal.BackColor = System.Drawing.SystemColors.Window;
            this.cmbtaxcal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbtaxcal.FormattingEnabled = true;
            this.cmbtaxcal.Items.AddRange(new object[] {
            "Tax Invoice",
            "Bill Of Supply"});
            this.cmbtaxcal.Location = new System.Drawing.Point(171, 74);
            this.cmbtaxcal.Name = "cmbtaxcal";
            this.cmbtaxcal.Size = new System.Drawing.Size(222, 21);
            this.cmbtaxcal.TabIndex = 2;
            this.cmbtaxcal.SelectedIndexChanged += new System.EventHandler(this.cmbtaxcal_SelectedIndexChanged);
            this.cmbtaxcal.Enter += new System.EventHandler(this.cmbtaxcal_Enter);
            this.cmbtaxcal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbtaxcal_KeyDown);
            this.cmbtaxcal.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbtaxcal_KeyUp);
            this.cmbtaxcal.Leave += new System.EventHandler(this.cmbtaxcal_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(15, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(114, 17);
            this.label7.TabIndex = 160;
            this.label7.Text = "Tax Calculation";
            // 
            // txtinvoiceheading
            // 
            this.txtinvoiceheading.BackColor = System.Drawing.Color.White;
            this.txtinvoiceheading.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtinvoiceheading.Location = new System.Drawing.Point(171, 58);
            this.txtinvoiceheading.Name = "txtinvoiceheading";
            this.txtinvoiceheading.Size = new System.Drawing.Size(222, 23);
            this.txtinvoiceheading.TabIndex = 10;
            this.txtinvoiceheading.Enter += new System.EventHandler(this.txtinvoiceheading_Enter);
            this.txtinvoiceheading.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtinvoiceheading_KeyDown);
            this.txtinvoiceheading.Leave += new System.EventHandler(this.txtinvoiceheading_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(15, 61);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(118, 17);
            this.label8.TabIndex = 163;
            this.label8.Text = "Invoice Heading";
            // 
            // cmbbtdefault
            // 
            this.cmbbtdefault.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbbtdefault.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbbtdefault.BackColor = System.Drawing.SystemColors.Window;
            this.cmbbtdefault.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbtdefault.FormattingEnabled = true;
            this.cmbbtdefault.Items.AddRange(new object[] {
            "SalePrice",
            "BasicPrice",
            "PurchasePrice",
            "MRP",
            "Min-SalePrice",
            "Self Val.Price"});
            this.cmbbtdefault.Location = new System.Drawing.Point(171, 88);
            this.cmbbtdefault.Name = "cmbbtdefault";
            this.cmbbtdefault.Size = new System.Drawing.Size(222, 21);
            this.cmbbtdefault.TabIndex = 11;
            this.cmbbtdefault.SelectedIndexChanged += new System.EventHandler(this.cmbbtdefault_SelectedIndexChanged);
            this.cmbbtdefault.Enter += new System.EventHandler(this.cmbbtdefault_Enter);
            this.cmbbtdefault.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbbtdefault_KeyDown);
            this.cmbbtdefault.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbbtdefault_KeyUp);
            this.cmbbtdefault.Leave += new System.EventHandler(this.cmbbtdefault_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(15, 89);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(149, 17);
            this.label9.TabIndex = 164;
            this.label9.Text = "Pick Price By Default";
            // 
            // cmbformtype
            // 
            this.cmbformtype.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbformtype.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbformtype.BackColor = System.Drawing.SystemColors.Window;
            this.cmbformtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbformtype.FormattingEnabled = true;
            this.cmbformtype.Items.AddRange(new object[] {
            "S",
            "SR",
            "SO",
            "SC",
            "P",
            "PR",
            "PO",
            "PC"});
            this.cmbformtype.Location = new System.Drawing.Point(572, 28);
            this.cmbformtype.Name = "cmbformtype";
            this.cmbformtype.Size = new System.Drawing.Size(90, 21);
            this.cmbformtype.TabIndex = 1;
            this.cmbformtype.SelectedIndexChanged += new System.EventHandler(this.cmbformtype_SelectedIndexChanged);
            this.cmbformtype.Enter += new System.EventHandler(this.cmbformtype_Enter);
            this.cmbformtype.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbformtype_KeyDown);
            this.cmbformtype.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbformtype_KeyUp);
            this.cmbformtype.Leave += new System.EventHandler(this.cmbformtype_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Location = new System.Drawing.Point(478, 30);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 17);
            this.label10.TabIndex = 174;
            this.label10.Text = "Form Type";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.White;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(171, 53);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(285, 13);
            this.textBox2.TabIndex = 176;
            this.textBox2.TabStop = false;
            this.textBox2.Text = "(Tax Free - Not Calculate Tax, Tax Inclusive - Calculate Tax)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label11.Location = new System.Drawing.Point(180, 118);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(204, 13);
            this.label11.TabIndex = 179;
            this.label11.Text = "GST IN of E-Commernce Operator";
            // 
            // txtecom
            // 
            this.txtecom.BackColor = System.Drawing.Color.White;
            this.txtecom.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtecom.Location = new System.Drawing.Point(171, 138);
            this.txtecom.Name = "txtecom";
            this.txtecom.Size = new System.Drawing.Size(291, 23);
            this.txtecom.TabIndex = 4;
            this.txtecom.Visible = false;
            this.txtecom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtecom_KeyDown);
            // 
            // chkecom
            // 
            this.chkecom.AutoSize = true;
            this.chkecom.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkecom.Location = new System.Drawing.Point(171, 97);
            this.chkecom.Name = "chkecom";
            this.chkecom.Size = new System.Drawing.Size(278, 20);
            this.chkecom.TabIndex = 3;
            this.chkecom.Text = "Sales Made Via E-Commerce Operator";
            this.chkecom.UseVisualStyleBackColor = true;
            this.chkecom.CheckedChanged += new System.EventHandler(this.chkecom_CheckedChanged);
            this.chkecom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkecom_KeyDown);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Controls.Add(this.txtprintname);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.cmbformtype);
            this.panel1.Controls.Add(this.txtgrop);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(6, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(758, 117);
            this.panel1.TabIndex = 180;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(172, 83);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(483, 36);
            this.textBox1.TabIndex = 175;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "S - SALE              SO - SALE ORDER             SR - SALE RETURN            SC " +
    "- SALE CHALLAN\r\nP - PURCHASE   PO - PURCHASE ORDER   PR - PURCHASE RETURN PC - P" +
    "URCHASE CHALLAN";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.SteelBlue;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.ForeColor = System.Drawing.Color.White;
            this.textBox3.Location = new System.Drawing.Point(0, 0);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(757, 20);
            this.textBox3.TabIndex = 148;
            this.textBox3.TabStop = false;
            this.textBox3.Text = " General Information";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.cmbxttype);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtecom);
            this.panel2.Controls.Add(this.chkecom);
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.cmbregion);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.cmbtaxcal);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.textBox4);
            this.panel2.Location = new System.Drawing.Point(6, 155);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(758, 168);
            this.panel2.TabIndex = 181;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.SteelBlue;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.ForeColor = System.Drawing.Color.White;
            this.textBox4.Location = new System.Drawing.Point(0, 0);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(756, 20);
            this.textBox4.TabIndex = 177;
            this.textBox4.TabStop = false;
            this.textBox4.Text = " Taxation Details";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.txtinvoiceprefix);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.cmbbtdefault);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.txtstartfrom);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.txtinvoiceheading);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.textBox5);
            this.panel3.Location = new System.Drawing.Point(6, 327);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(758, 115);
            this.panel3.TabIndex = 182;
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.Color.SteelBlue;
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5.ForeColor = System.Drawing.Color.White;
            this.textBox5.Location = new System.Drawing.Point(-1, 0);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(757, 20);
            this.textBox5.TabIndex = 150;
            this.textBox5.TabStop = false;
            this.textBox5.Text = " Invoice Numbering";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btndelete);
            this.panel4.Controls.Add(this.btnsave);
            this.panel4.Controls.Add(this.btnreset);
            this.panel4.Controls.Add(this.btncancel);
            this.panel4.Location = new System.Drawing.Point(12, 448);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(730, 53);
            this.panel4.TabIndex = 183;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Location = new System.Drawing.Point(2, 31);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(765, 473);
            this.panel5.TabIndex = 176;
            // 
            // PurchaseTypeEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(768, 516);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.txttitle);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PurchaseTypeEntry";
            this.Text = "PurchaseTypeEntry";
            this.Load += new System.EventHandler(this.PurchaseTypeEntry_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txttitle;
        private System.Windows.Forms.ComboBox txtgrop;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtprintname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbxttype;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbregion;
        private System.Windows.Forms.Button btndelete;
        private System.Windows.Forms.Button btncancel;
        private System.Windows.Forms.Button btnreset;
        private System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.TextBox txtstartfrom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtinvoiceprefix;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbtaxcal;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtinvoiceheading;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbbtdefault;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbformtype;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtecom;
        private System.Windows.Forms.CheckBox chkecom;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel5;
    }
}