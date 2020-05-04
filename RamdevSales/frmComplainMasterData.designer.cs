namespace RamdevSales
{
    partial class frmComplainMasterData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmComplainMasterData));
            this.txtheader = new System.Windows.Forms.TextBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.TextBox5 = new System.Windows.Forms.TextBox();
            this.txtComplainId = new System.Windows.Forms.TextBox();
            this.TextBox2 = new System.Windows.Forms.TextBox();
            this.TextBox3 = new System.Windows.Forms.TextBox();
            this.cmbcustname = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.cmbReplacementType = new System.Windows.Forms.ComboBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.btnprint = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btndelete = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.txtserialno = new System.Windows.Forms.TextBox();
            this.pnlallitem = new System.Windows.Forms.Panel();
            this.lvallitem = new System.Windows.Forms.ListView();
            this.LVFO = new System.Windows.Forms.ListView();
            this.btnGroupEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cmbItemName = new System.Windows.Forms.TextBox();
            this.btnEditPartyName = new System.Windows.Forms.Button();
            this.btnAddPartyName = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.pnlallitem.SuspendLayout();
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
            this.txtheader.Location = new System.Drawing.Point(3, 1);
            this.txtheader.Name = "txtheader";
            this.txtheader.ReadOnly = true;
            this.txtheader.Size = new System.Drawing.Size(1103, 31);
            this.txtheader.TabIndex = 1;
            this.txtheader.TabStop = false;
            this.txtheader.Text = "COMPLAIN RECEIVE";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(455, 16);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(151, 20);
            this.dtpDate.TabIndex = 1;
            this.dtpDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDate_KeyDown);
            // 
            // TextBox5
            // 
            this.TextBox5.BackColor = System.Drawing.Color.White;
            this.TextBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextBox5.Enabled = false;
            this.TextBox5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox5.Location = new System.Drawing.Point(8, 47);
            this.TextBox5.Name = "TextBox5";
            this.TextBox5.Size = new System.Drawing.Size(94, 16);
            this.TextBox5.TabIndex = 0;
            this.TextBox5.TabStop = false;
            this.TextBox5.Text = "Complain No:";
            // 
            // txtComplainId
            // 
            this.txtComplainId.BackColor = System.Drawing.Color.White;
            this.txtComplainId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtComplainId.Enabled = false;
            this.txtComplainId.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtComplainId.ForeColor = System.Drawing.Color.Black;
            this.txtComplainId.Location = new System.Drawing.Point(139, 14);
            this.txtComplainId.Name = "txtComplainId";
            this.txtComplainId.ReadOnly = true;
            this.txtComplainId.Size = new System.Drawing.Size(143, 23);
            this.txtComplainId.TabIndex = 0;
            this.txtComplainId.TabStop = false;
            this.txtComplainId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TextBox2
            // 
            this.TextBox2.BackColor = System.Drawing.Color.White;
            this.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextBox2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox2.Location = new System.Drawing.Point(361, 16);
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.Size = new System.Drawing.Size(94, 16);
            this.TextBox2.TabIndex = 178;
            this.TextBox2.TabStop = false;
            this.TextBox2.Text = "Date:";
            // 
            // TextBox3
            // 
            this.TextBox3.BackColor = System.Drawing.Color.White;
            this.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextBox3.Enabled = false;
            this.TextBox3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox3.Location = new System.Drawing.Point(3, 62);
            this.TextBox3.Name = "TextBox3";
            this.TextBox3.Size = new System.Drawing.Size(134, 16);
            this.TextBox3.TabIndex = 179;
            this.TextBox3.TabStop = false;
            this.TextBox3.Text = "Customer Name:";
            // 
            // cmbcustname
            // 
            this.cmbcustname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmbcustname.BackColor = System.Drawing.Color.AntiqueWhite;
            this.cmbcustname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbcustname.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcustname.FormattingEnabled = true;
            this.cmbcustname.Location = new System.Drawing.Point(139, 57);
            this.cmbcustname.Name = "cmbcustname";
            this.cmbcustname.Size = new System.Drawing.Size(467, 24);
            this.cmbcustname.TabIndex = 2;
            this.cmbcustname.SelectedIndexChanged += new System.EventHandler(this.cmbcustname_SelectedIndexChanged);
            this.cmbcustname.Enter += new System.EventHandler(this.cmbcustname_Enter);
            this.cmbcustname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcustname_KeyDown);
            this.cmbcustname.Leave += new System.EventHandler(this.cmbcustname_Leave);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Enabled = false;
            this.textBox1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(24, 103);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(94, 16);
            this.textBox1.TabIndex = 181;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "Item Name:";
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.Color.White;
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox6.Enabled = false;
            this.textBox6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox6.Location = new System.Drawing.Point(357, 104);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(134, 16);
            this.textBox6.TabIndex = 183;
            this.textBox6.TabStop = false;
            this.textBox6.Text = "Replacement Type:";
            // 
            // cmbReplacementType
            // 
            this.cmbReplacementType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmbReplacementType.BackColor = System.Drawing.Color.AntiqueWhite;
            this.cmbReplacementType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReplacementType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbReplacementType.FormattingEnabled = true;
            this.cmbReplacementType.Items.AddRange(new object[] {
            "In Warrenty",
            "Not In Warrenty"});
            this.cmbReplacementType.Location = new System.Drawing.Point(357, 126);
            this.cmbReplacementType.Name = "cmbReplacementType";
            this.cmbReplacementType.Size = new System.Drawing.Size(150, 24);
            this.cmbReplacementType.TabIndex = 4;
            this.cmbReplacementType.Enter += new System.EventHandler(this.cmbReplacementType_Enter);
            this.cmbReplacementType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbReplacementType_KeyDown);
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.Color.White;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox7.Enabled = false;
            this.textBox7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.Location = new System.Drawing.Point(7, 158);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(94, 16);
            this.textBox7.TabIndex = 185;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "Description:";
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.Color.White;
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.ForeColor = System.Drawing.Color.Black;
            this.txtDescription.Location = new System.Drawing.Point(7, 180);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(526, 23);
            this.txtDescription.TabIndex = 7;
            this.txtDescription.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescription_KeyDown);
            // 
            // textBox10
            // 
            this.textBox10.BackColor = System.Drawing.Color.White;
            this.textBox10.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox10.Enabled = false;
            this.textBox10.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox10.Location = new System.Drawing.Point(537, 158);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(94, 16);
            this.textBox10.TabIndex = 189;
            this.textBox10.TabStop = false;
            this.textBox10.Text = "Remark:";
            // 
            // txtRemark
            // 
            this.txtRemark.BackColor = System.Drawing.Color.White;
            this.txtRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemark.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemark.ForeColor = System.Drawing.Color.Black;
            this.txtRemark.Location = new System.Drawing.Point(537, 180);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(556, 23);
            this.txtRemark.TabIndex = 8;
            this.txtRemark.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemark_KeyDown);
            // 
            // btnprint
            // 
            this.btnprint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnprint.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprint.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(876, 11);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(97, 34);
            this.btnprint.TabIndex = 11;
            this.btnprint.Text = "&Print";
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
            this.btnclose.Location = new System.Drawing.Point(980, 11);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(97, 34);
            this.btnclose.TabIndex = 12;
            this.btnclose.Text = "&Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            this.btnclose.Enter += new System.EventHandler(this.btnclose_Enter);
            this.btnclose.Leave += new System.EventHandler(this.btnclose_Leave);
            this.btnclose.MouseEnter += new System.EventHandler(this.btnclose_MouseEnter);
            this.btnclose.MouseLeave += new System.EventHandler(this.btnclose_MouseLeave);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(668, 11);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(97, 34);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "&Submit\r\n";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnSave.Enter += new System.EventHandler(this.btnSave_Enter);
            this.btnSave.Leave += new System.EventHandler(this.btnSave_Leave);
            this.btnSave.MouseEnter += new System.EventHandler(this.btnSave_MouseEnter);
            this.btnSave.MouseLeave += new System.EventHandler(this.btnSave_MouseLeave);
            // 
            // btndelete
            // 
            this.btndelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btndelete.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btndelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndelete.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndelete.ForeColor = System.Drawing.Color.White;
            this.btndelete.Location = new System.Drawing.Point(772, 11);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(97, 34);
            this.btndelete.TabIndex = 10;
            this.btndelete.Text = "&Delete";
            this.btndelete.UseVisualStyleBackColor = false;
            this.btndelete.Click += new System.EventHandler(this.btndelete_Click);
            this.btndelete.Enter += new System.EventHandler(this.btndelete_Enter);
            this.btndelete.Leave += new System.EventHandler(this.btndelete_Leave);
            this.btndelete.MouseEnter += new System.EventHandler(this.btndelete_MouseEnter);
            this.btndelete.MouseLeave += new System.EventHandler(this.btndelete_MouseLeave);
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.White;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Enabled = false;
            this.textBox4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.Location = new System.Drawing.Point(528, 104);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(43, 16);
            this.textBox4.TabIndex = 193;
            this.textBox4.TabStop = false;
            this.textBox4.Text = "Qty:";
            // 
            // txtQty
            // 
            this.txtQty.BackColor = System.Drawing.Color.White;
            this.txtQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQty.Enabled = false;
            this.txtQty.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQty.ForeColor = System.Drawing.Color.Black;
            this.txtQty.Location = new System.Drawing.Point(507, 126);
            this.txtQty.Name = "txtQty";
            this.txtQty.ReadOnly = true;
            this.txtQty.Size = new System.Drawing.Size(99, 23);
            this.txtQty.TabIndex = 5;
            this.txtQty.TabStop = false;
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQty_KeyDown);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.textBox8);
            this.panel1.Controls.Add(this.txtserialno);
            this.panel1.Controls.Add(this.pnlallitem);
            this.panel1.Controls.Add(this.LVFO);
            this.panel1.Controls.Add(this.btnGroupEdit);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.cmbItemName);
            this.panel1.Controls.Add(this.btnEditPartyName);
            this.panel1.Controls.Add(this.btnAddPartyName);
            this.panel1.Controls.Add(this.textBox4);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.dtpDate);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.TextBox2);
            this.panel1.Controls.Add(this.txtComplainId);
            this.panel1.Controls.Add(this.txtQty);
            this.panel1.Controls.Add(this.TextBox3);
            this.panel1.Controls.Add(this.btnclose);
            this.panel1.Controls.Add(this.cmbcustname);
            this.panel1.Controls.Add(this.textBox6);
            this.panel1.Controls.Add(this.btndelete);
            this.panel1.Controls.Add(this.cmbReplacementType);
            this.panel1.Controls.Add(this.btnprint);
            this.panel1.Controls.Add(this.txtRemark);
            this.panel1.Controls.Add(this.textBox10);
            this.panel1.Controls.Add(this.textBox7);
            this.panel1.Controls.Add(this.txtDescription);
            this.panel1.Location = new System.Drawing.Point(3, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1103, 508);
            this.panel1.TabIndex = 0;
            // 
            // textBox8
            // 
            this.textBox8.BackColor = System.Drawing.Color.White;
            this.textBox8.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox8.Enabled = false;
            this.textBox8.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox8.Location = new System.Drawing.Point(624, 104);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(75, 16);
            this.textBox8.TabIndex = 288;
            this.textBox8.TabStop = false;
            this.textBox8.Text = "Serial No:";
            // 
            // txtserialno
            // 
            this.txtserialno.BackColor = System.Drawing.Color.White;
            this.txtserialno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtserialno.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtserialno.ForeColor = System.Drawing.Color.Black;
            this.txtserialno.Location = new System.Drawing.Point(612, 126);
            this.txtserialno.Name = "txtserialno";
            this.txtserialno.Size = new System.Drawing.Size(481, 23);
            this.txtserialno.TabIndex = 6;
            this.txtserialno.TabStop = false;
            this.txtserialno.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtserialno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtserialno_KeyDown);
            // 
            // pnlallitem
            // 
            this.pnlallitem.Controls.Add(this.lvallitem);
            this.pnlallitem.Location = new System.Drawing.Point(7, 149);
            this.pnlallitem.Name = "pnlallitem";
            this.pnlallitem.Size = new System.Drawing.Size(299, 286);
            this.pnlallitem.TabIndex = 9;
            this.pnlallitem.Visible = false;
            // 
            // lvallitem
            // 
            this.lvallitem.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.lvallitem.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lvallitem.BackColor = System.Drawing.Color.White;
            this.lvallitem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvallitem.Font = new System.Drawing.Font("Arial", 9.5F, System.Drawing.FontStyle.Bold);
            this.lvallitem.ForeColor = System.Drawing.Color.Maroon;
            this.lvallitem.FullRowSelect = true;
            this.lvallitem.GridLines = true;
            this.lvallitem.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvallitem.HideSelection = false;
            this.lvallitem.Location = new System.Drawing.Point(3, 6);
            this.lvallitem.MultiSelect = false;
            this.lvallitem.Name = "lvallitem";
            this.lvallitem.Size = new System.Drawing.Size(288, 277);
            this.lvallitem.TabIndex = 0;
            this.lvallitem.UseCompatibleStateImageBehavior = false;
            this.lvallitem.View = System.Windows.Forms.View.Details;
            this.lvallitem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvallitem_KeyDown);
            this.lvallitem.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvallitem_MouseClick);
            this.lvallitem.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvallitem_MouseDoubleClick);
            // 
            // LVFO
            // 
            this.LVFO.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.LVFO.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LVFO.BackColor = System.Drawing.Color.White;
            this.LVFO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LVFO.Font = new System.Drawing.Font("Arial", 9.5F, System.Drawing.FontStyle.Bold);
            this.LVFO.ForeColor = System.Drawing.Color.Maroon;
            this.LVFO.FullRowSelect = true;
            this.LVFO.GridLines = true;
            this.LVFO.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.LVFO.HideSelection = false;
            this.LVFO.Location = new System.Drawing.Point(7, 209);
            this.LVFO.MultiSelect = false;
            this.LVFO.Name = "LVFO";
            this.LVFO.Size = new System.Drawing.Size(1086, 292);
            this.LVFO.TabIndex = 13;
            this.LVFO.UseCompatibleStateImageBehavior = false;
            this.LVFO.View = System.Windows.Forms.View.Details;
            this.LVFO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LVFO_KeyDown);
            this.LVFO.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LVFO_MouseDoubleClick);
            // 
            // btnGroupEdit
            // 
            this.btnGroupEdit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGroupEdit.BackgroundImage")));
            this.btnGroupEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGroupEdit.ForeColor = System.Drawing.Color.White;
            this.btnGroupEdit.Location = new System.Drawing.Point(328, 125);
            this.btnGroupEdit.Name = "btnGroupEdit";
            this.btnGroupEdit.Size = new System.Drawing.Size(23, 22);
            this.btnGroupEdit.TabIndex = 286;
            this.btnGroupEdit.TabStop = false;
            this.btnGroupEdit.UseVisualStyleBackColor = true;
            this.btnGroupEdit.Click += new System.EventHandler(this.btnGroupEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(309, 126);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(22, 22);
            this.btnAdd.TabIndex = 285;
            this.btnAdd.TabStop = false;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // cmbItemName
            // 
            this.cmbItemName.BackColor = System.Drawing.SystemColors.Window;
            this.cmbItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cmbItemName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbItemName.Location = new System.Drawing.Point(8, 126);
            this.cmbItemName.Name = "cmbItemName";
            this.cmbItemName.Size = new System.Drawing.Size(299, 22);
            this.cmbItemName.TabIndex = 3;
            this.cmbItemName.TextChanged += new System.EventHandler(this.cmbItemName_TextChanged);
            this.cmbItemName.Enter += new System.EventHandler(this.cmbItemName_Enter);
            this.cmbItemName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbItemName_KeyDown);
            this.cmbItemName.Leave += new System.EventHandler(this.cmbItemName_Leave);
            // 
            // btnEditPartyName
            // 
            this.btnEditPartyName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEditPartyName.BackgroundImage")));
            this.btnEditPartyName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditPartyName.ForeColor = System.Drawing.Color.White;
            this.btnEditPartyName.Location = new System.Drawing.Point(629, 57);
            this.btnEditPartyName.Name = "btnEditPartyName";
            this.btnEditPartyName.Size = new System.Drawing.Size(23, 22);
            this.btnEditPartyName.TabIndex = 281;
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
            this.btnAddPartyName.Location = new System.Drawing.Point(610, 58);
            this.btnAddPartyName.Name = "btnAddPartyName";
            this.btnAddPartyName.Size = new System.Drawing.Size(22, 22);
            this.btnAddPartyName.TabIndex = 280;
            this.btnAddPartyName.TabStop = false;
            this.btnAddPartyName.Text = "&Add";
            this.btnAddPartyName.UseVisualStyleBackColor = false;
            this.btnAddPartyName.Click += new System.EventHandler(this.btnAddPartyName_Click);
            // 
            // frmComplainMasterData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1109, 541);
            this.Controls.Add(this.TextBox5);
            this.Controls.Add(this.txtheader);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmComplainMasterData";
            this.Text = "frmComplainMasterData";
            this.Load += new System.EventHandler(this.frmComplainMasterData_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlallitem.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtheader;
        private System.Windows.Forms.DateTimePicker dtpDate;
        internal System.Windows.Forms.TextBox TextBox5;
        internal System.Windows.Forms.TextBox txtComplainId;
        internal System.Windows.Forms.TextBox TextBox2;
        internal System.Windows.Forms.TextBox TextBox3;
        private System.Windows.Forms.ComboBox cmbcustname;
        internal System.Windows.Forms.TextBox textBox1;
        internal System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.ComboBox cmbReplacementType;
        internal System.Windows.Forms.TextBox textBox7;
        internal System.Windows.Forms.TextBox txtDescription;
        internal System.Windows.Forms.TextBox textBox10;
        internal System.Windows.Forms.TextBox txtRemark;
        internal System.Windows.Forms.Button btnprint;
        internal System.Windows.Forms.Button btnclose;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.Button btndelete;
        internal System.Windows.Forms.TextBox textBox4;
        internal System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnEditPartyName;
        internal System.Windows.Forms.Button btnAddPartyName;
        internal System.Windows.Forms.TextBox cmbItemName;
        internal System.Windows.Forms.ListView lvallitem;
        private System.Windows.Forms.Panel pnlallitem;
        private System.Windows.Forms.Button btnGroupEdit;
        internal System.Windows.Forms.Button btnAdd;
        internal System.Windows.Forms.ListView LVFO;
        internal System.Windows.Forms.TextBox textBox8;
        internal System.Windows.Forms.TextBox txtserialno;

    }
}