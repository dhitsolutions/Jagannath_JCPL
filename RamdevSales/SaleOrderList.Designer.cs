namespace RamdevSales
{
    partial class SaleOrderList
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
            this.txttitle = new System.Windows.Forms.TextBox();
            this.btnnew = new System.Windows.Forms.Button();
            this.DTPTo = new System.Windows.Forms.DateTimePicker();
            this.btnclose = new System.Windows.Forms.Button();
            this.BtnViewReport = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.DTPFrom = new System.Windows.Forms.DateTimePicker();
            this.Label2 = new System.Windows.Forms.Label();
            this.LVDayBook = new System.Windows.Forms.ListView();
            this.TxtInvoice = new System.Windows.Forms.TextBox();
            this.txtnetamt = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.txtvat = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.txtbillamt = new System.Windows.Forms.TextBox();
            this.rbpending = new System.Windows.Forms.RadioButton();
            this.rbclear = new System.Windows.Forms.RadioButton();
            this.rball = new System.Windows.Forms.RadioButton();
            this.btngenrpt = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnpdf = new System.Windows.Forms.Button();
            this.btnprint = new System.Windows.Forms.Button();
            this.lvbillitemitem = new System.Windows.Forms.ListView();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtitems = new System.Windows.Forms.TextBox();
            this.drpitems = new System.Windows.Forms.ComboBox();
            this.drpaccount = new System.Windows.Forms.ComboBox();
            this.txtaccount = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txttitle
            // 
            this.txttitle.BackColor = System.Drawing.SystemColors.Highlight;
            this.txttitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txttitle.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttitle.ForeColor = System.Drawing.Color.White;
            this.txttitle.Location = new System.Drawing.Point(3, 0);
            this.txttitle.Name = "txttitle";
            this.txttitle.ReadOnly = true;
            this.txttitle.Size = new System.Drawing.Size(1070, 31);
            this.txttitle.TabIndex = 137;
            this.txttitle.TabStop = false;
            this.txttitle.Text = "SALE ORDER LIST";
            // 
            // btnnew
            // 
            this.btnnew.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnnew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnnew.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnnew.ForeColor = System.Drawing.Color.White;
            this.btnnew.Location = new System.Drawing.Point(690, 20);
            this.btnnew.Name = "btnnew";
            this.btnnew.Size = new System.Drawing.Size(97, 34);
            this.btnnew.TabIndex = 0;
            this.btnnew.Text = "&New";
            this.btnnew.UseVisualStyleBackColor = false;
            this.btnnew.Click += new System.EventHandler(this.btnnew_Click);
            this.btnnew.Enter += new System.EventHandler(this.btnnew_Enter);
            this.btnnew.Leave += new System.EventHandler(this.btnnew_Leave);
            this.btnnew.MouseEnter += new System.EventHandler(this.btnnew_MouseEnter);
            this.btnnew.MouseLeave += new System.EventHandler(this.btnnew_MouseLeave);
            // 
            // DTPTo
            // 
            this.DTPTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPTo.Location = new System.Drawing.Point(194, 57);
            this.DTPTo.Name = "DTPTo";
            this.DTPTo.Size = new System.Drawing.Size(139, 22);
            this.DTPTo.TabIndex = 3;
            this.DTPTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTPTo_KeyDown);
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(956, 20);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(97, 34);
            this.btnclose.TabIndex = 6;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            this.btnclose.Enter += new System.EventHandler(this.btnclose_Enter);
            this.btnclose.Leave += new System.EventHandler(this.btnclose_Leave);
            this.btnclose.MouseEnter += new System.EventHandler(this.btnclose_MouseEnter);
            this.btnclose.MouseLeave += new System.EventHandler(this.btnclose_MouseLeave);
            // 
            // BtnViewReport
            // 
            this.BtnViewReport.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BtnViewReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnViewReport.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnViewReport.ForeColor = System.Drawing.Color.White;
            this.BtnViewReport.Location = new System.Drawing.Point(350, 20);
            this.BtnViewReport.Name = "BtnViewReport";
            this.BtnViewReport.Size = new System.Drawing.Size(97, 34);
            this.BtnViewReport.TabIndex = 4;
            this.BtnViewReport.Text = "&OK";
            this.BtnViewReport.UseVisualStyleBackColor = false;
            this.BtnViewReport.Click += new System.EventHandler(this.BtnViewReport_Click);
            this.BtnViewReport.Enter += new System.EventHandler(this.BtnViewReport_Enter);
            this.BtnViewReport.Leave += new System.EventHandler(this.BtnViewReport_Leave);
            this.BtnViewReport.MouseEnter += new System.EventHandler(this.BtnViewReport_MouseEnter);
            this.BtnViewReport.MouseLeave += new System.EventHandler(this.BtnViewReport_MouseLeave);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(69, 37);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(84, 16);
            this.Label1.TabIndex = 138;
            this.Label1.Text = "From Date";
            // 
            // DTPFrom
            // 
            this.DTPFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFrom.Location = new System.Drawing.Point(27, 58);
            this.DTPFrom.Name = "DTPFrom";
            this.DTPFrom.Size = new System.Drawing.Size(150, 22);
            this.DTPFrom.TabIndex = 2;
            this.DTPFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTPFrom_KeyDown);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(230, 37);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(63, 16);
            this.Label2.TabIndex = 140;
            this.Label2.Text = "To Date";
            // 
            // LVDayBook
            // 
            this.LVDayBook.BackColor = System.Drawing.SystemColors.Window;
            this.LVDayBook.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LVDayBook.CheckBoxes = true;
            this.LVDayBook.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LVDayBook.ForeColor = System.Drawing.Color.Navy;
            this.LVDayBook.FullRowSelect = true;
            this.LVDayBook.GridLines = true;
            this.LVDayBook.HideSelection = false;
            this.LVDayBook.Location = new System.Drawing.Point(12, 88);
            this.LVDayBook.MultiSelect = false;
            this.LVDayBook.Name = "LVDayBook";
            this.LVDayBook.Size = new System.Drawing.Size(1053, 209);
            this.LVDayBook.TabIndex = 1;
            this.LVDayBook.UseCompatibleStateImageBehavior = false;
            this.LVDayBook.View = System.Windows.Forms.View.Details;
            this.LVDayBook.SelectedIndexChanged += new System.EventHandler(this.LVDayBook_SelectedIndexChanged);
            this.LVDayBook.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LVDayBook_KeyDown);
            this.LVDayBook.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LVDayBook_MouseDoubleClick);
            // 
            // TxtInvoice
            // 
            this.TxtInvoice.BackColor = System.Drawing.SystemColors.Window;
            this.TxtInvoice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtInvoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtInvoice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.TxtInvoice.Location = new System.Drawing.Point(9, 283);
            this.TxtInvoice.Multiline = true;
            this.TxtInvoice.Name = "TxtInvoice";
            this.TxtInvoice.Size = new System.Drawing.Size(114, 30);
            this.TxtInvoice.TabIndex = 150;
            this.TxtInvoice.TabStop = false;
            this.TxtInvoice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtInvoice.Enter += new System.EventHandler(this.TxtInvoice_Enter);
            this.TxtInvoice.Leave += new System.EventHandler(this.TxtInvoice_Leave);
            // 
            // txtnetamt
            // 
            this.txtnetamt.BackColor = System.Drawing.SystemColors.Window;
            this.txtnetamt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtnetamt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnetamt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtnetamt.Location = new System.Drawing.Point(921, 284);
            this.txtnetamt.Multiline = true;
            this.txtnetamt.Name = "txtnetamt";
            this.txtnetamt.Size = new System.Drawing.Size(137, 29);
            this.txtnetamt.TabIndex = 153;
            this.txtnetamt.TabStop = false;
            this.txtnetamt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtnetamt.Enter += new System.EventHandler(this.txtnetamt_Enter);
            this.txtnetamt.Leave += new System.EventHandler(this.txtnetamt_Leave);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.Color.Blue;
            this.Label3.Location = new System.Drawing.Point(8, 265);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(75, 16);
            this.Label3.TabIndex = 146;
            this.Label3.Text = "INVOICE:";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.ForeColor = System.Drawing.Color.Blue;
            this.Label6.Location = new System.Drawing.Point(792, 266);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(93, 16);
            this.Label6.TabIndex = 148;
            this.Label6.Text = "Gst Amount";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.ForeColor = System.Drawing.Color.Blue;
            this.Label7.Location = new System.Drawing.Point(940, 266);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(94, 16);
            this.Label7.TabIndex = 149;
            this.Label7.Text = "Net Amount";
            // 
            // txtvat
            // 
            this.txtvat.BackColor = System.Drawing.SystemColors.Window;
            this.txtvat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtvat.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtvat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtvat.Location = new System.Drawing.Point(786, 284);
            this.txtvat.Multiline = true;
            this.txtvat.Name = "txtvat";
            this.txtvat.Size = new System.Drawing.Size(135, 29);
            this.txtvat.TabIndex = 152;
            this.txtvat.TabStop = false;
            this.txtvat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtvat.Enter += new System.EventHandler(this.txtvat_Enter);
            this.txtvat.Leave += new System.EventHandler(this.txtvat_Leave);
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.ForeColor = System.Drawing.Color.Blue;
            this.Label5.Location = new System.Drawing.Point(662, 266);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(108, 16);
            this.Label5.TabIndex = 147;
            this.Label5.Text = "Basic Amount";
            // 
            // txtbillamt
            // 
            this.txtbillamt.BackColor = System.Drawing.SystemColors.Window;
            this.txtbillamt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbillamt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbillamt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtbillamt.Location = new System.Drawing.Point(652, 284);
            this.txtbillamt.Multiline = true;
            this.txtbillamt.Name = "txtbillamt";
            this.txtbillamt.Size = new System.Drawing.Size(134, 29);
            this.txtbillamt.TabIndex = 151;
            this.txtbillamt.TabStop = false;
            this.txtbillamt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtbillamt.Enter += new System.EventHandler(this.txtbillamt_Enter);
            this.txtbillamt.Leave += new System.EventHandler(this.txtbillamt_Leave);
            // 
            // rbpending
            // 
            this.rbpending.AutoSize = true;
            this.rbpending.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbpending.Location = new System.Drawing.Point(143, 289);
            this.rbpending.Name = "rbpending";
            this.rbpending.Size = new System.Drawing.Size(76, 18);
            this.rbpending.TabIndex = 154;
            this.rbpending.Text = "Pending";
            this.rbpending.UseVisualStyleBackColor = true;
            // 
            // rbclear
            // 
            this.rbclear.AutoSize = true;
            this.rbclear.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbclear.Location = new System.Drawing.Point(225, 289);
            this.rbclear.Name = "rbclear";
            this.rbclear.Size = new System.Drawing.Size(58, 18);
            this.rbclear.TabIndex = 155;
            this.rbclear.Text = "Clear";
            this.rbclear.UseVisualStyleBackColor = true;
            // 
            // rball
            // 
            this.rball.AutoSize = true;
            this.rball.Checked = true;
            this.rball.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rball.Location = new System.Drawing.Point(289, 290);
            this.rball.Name = "rball";
            this.rball.Size = new System.Drawing.Size(39, 18);
            this.rball.TabIndex = 156;
            this.rball.TabStop = true;
            this.rball.Text = "All";
            this.rball.UseVisualStyleBackColor = true;
            // 
            // btngenrpt
            // 
            this.btngenrpt.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btngenrpt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btngenrpt.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btngenrpt.ForeColor = System.Drawing.Color.White;
            this.btngenrpt.Location = new System.Drawing.Point(795, 20);
            this.btngenrpt.Name = "btngenrpt";
            this.btngenrpt.Size = new System.Drawing.Size(153, 34);
            this.btngenrpt.TabIndex = 5;
            this.btngenrpt.Text = "Generate &Report";
            this.btngenrpt.UseVisualStyleBackColor = false;
            this.btngenrpt.Click += new System.EventHandler(this.btngenrpt_Click);
            this.btngenrpt.Enter += new System.EventHandler(this.btngenrpt_Enter);
            this.btngenrpt.Leave += new System.EventHandler(this.btngenrpt_Leave);
            this.btngenrpt.MouseEnter += new System.EventHandler(this.btngenrpt_MouseEnter);
            this.btngenrpt.MouseLeave += new System.EventHandler(this.btngenrpt_MouseLeave);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.btnpdf);
            this.panel1.Controls.Add(this.txtitems);
            this.panel1.Controls.Add(this.btnprint);
            this.panel1.Controls.Add(this.drpitems);
            this.panel1.Controls.Add(this.drpaccount);
            this.panel1.Controls.Add(this.lvbillitemitem);
            this.panel1.Controls.Add(this.txtaccount);
            this.panel1.Controls.Add(this.txtnetamt);
            this.panel1.Controls.Add(this.rball);
            this.panel1.Controls.Add(this.txtvat);
            this.panel1.Controls.Add(this.Label7);
            this.panel1.Controls.Add(this.txtbillamt);
            this.panel1.Controls.Add(this.Label6);
            this.panel1.Controls.Add(this.btngenrpt);
            this.panel1.Controls.Add(this.rbclear);
            this.panel1.Controls.Add(this.BtnViewReport);
            this.panel1.Controls.Add(this.Label5);
            this.panel1.Controls.Add(this.rbpending);
            this.panel1.Controls.Add(this.btnclose);
            this.panel1.Controls.Add(this.TxtInvoice);
            this.panel1.Controls.Add(this.btnnew);
            this.panel1.Controls.Add(this.Label3);
            this.panel1.Location = new System.Drawing.Point(3, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1070, 497);
            this.panel1.TabIndex = 157;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // btnpdf
            // 
            this.btnpdf.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnpdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnpdf.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnpdf.ForeColor = System.Drawing.Color.White;
            this.btnpdf.Location = new System.Drawing.Point(105, 457);
            this.btnpdf.Name = "btnpdf";
            this.btnpdf.Size = new System.Drawing.Size(97, 34);
            this.btnpdf.TabIndex = 230;
            this.btnpdf.Text = "PDF";
            this.btnpdf.UseVisualStyleBackColor = false;
            this.btnpdf.Visible = false;
            this.btnpdf.Click += new System.EventHandler(this.btnpdf_Click);
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprint.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(7, 457);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(97, 34);
            this.btnprint.TabIndex = 229;
            this.btnprint.Text = "Print";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Visible = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // lvbillitemitem
            // 
            this.lvbillitemitem.BackColor = System.Drawing.SystemColors.Window;
            this.lvbillitemitem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvbillitemitem.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvbillitemitem.ForeColor = System.Drawing.Color.Navy;
            this.lvbillitemitem.FullRowSelect = true;
            this.lvbillitemitem.GridLines = true;
            this.lvbillitemitem.HideSelection = false;
            this.lvbillitemitem.Location = new System.Drawing.Point(8, 315);
            this.lvbillitemitem.MultiSelect = false;
            this.lvbillitemitem.Name = "lvbillitemitem";
            this.lvbillitemitem.Size = new System.Drawing.Size(1052, 142);
            this.lvbillitemitem.TabIndex = 163;
            this.lvbillitemitem.UseCompatibleStateImageBehavior = false;
            this.lvbillitemitem.View = System.Windows.Forms.View.Details;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(961, 458);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(97, 34);
            this.btnSearch.TabIndex = 232;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtitems
            // 
            this.txtitems.BackColor = System.Drawing.Color.White;
            this.txtitems.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtitems.Location = new System.Drawing.Point(827, 464);
            this.txtitems.Name = "txtitems";
            this.txtitems.Size = new System.Drawing.Size(131, 23);
            this.txtitems.TabIndex = 231;
            // 
            // drpitems
            // 
            this.drpitems.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.drpitems.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.drpitems.DropDownHeight = 70;
            this.drpitems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpitems.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.drpitems.FormattingEnabled = true;
            this.drpitems.IntegralHeight = false;
            this.drpitems.ItemHeight = 16;
            this.drpitems.Location = new System.Drawing.Point(652, 463);
            this.drpitems.Name = "drpitems";
            this.drpitems.Size = new System.Drawing.Size(171, 24);
            this.drpitems.TabIndex = 230;
            // 
            // drpaccount
            // 
            this.drpaccount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.drpaccount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.drpaccount.DropDownHeight = 70;
            this.drpaccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpaccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.drpaccount.FormattingEnabled = true;
            this.drpaccount.IntegralHeight = false;
            this.drpaccount.Location = new System.Drawing.Point(335, 463);
            this.drpaccount.Name = "drpaccount";
            this.drpaccount.Size = new System.Drawing.Size(171, 24);
            this.drpaccount.TabIndex = 228;
            // 
            // txtaccount
            // 
            this.txtaccount.BackColor = System.Drawing.Color.White;
            this.txtaccount.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtaccount.Location = new System.Drawing.Point(512, 464);
            this.txtaccount.Name = "txtaccount";
            this.txtaccount.Size = new System.Drawing.Size(131, 23);
            this.txtaccount.TabIndex = 229;
            // 
            // SaleOrderList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1076, 529);
            this.ControlBox = false;
            this.Controls.Add(this.LVDayBook);
            this.Controls.Add(this.DTPTo);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.DTPFrom);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.txttitle);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SaleOrderList";
            this.Text = "SaleOrderList";
            this.Load += new System.EventHandler(this.SaleOrderList_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txttitle;
        internal System.Windows.Forms.Button btnnew;
        internal System.Windows.Forms.DateTimePicker DTPTo;
        internal System.Windows.Forms.Button btnclose;
        internal System.Windows.Forms.Button BtnViewReport;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.DateTimePicker DTPFrom;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.ListView LVDayBook;
        internal System.Windows.Forms.TextBox TxtInvoice;
        internal System.Windows.Forms.TextBox txtnetamt;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.TextBox txtvat;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.TextBox txtbillamt;
        private System.Windows.Forms.RadioButton rbpending;
        private System.Windows.Forms.RadioButton rbclear;
        private System.Windows.Forms.RadioButton rball;
        internal System.Windows.Forms.Button btngenrpt;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.ListView lvbillitemitem;
        internal System.Windows.Forms.Button btnprint;
        internal System.Windows.Forms.Button btnpdf;
        internal System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtitems;
        private System.Windows.Forms.ComboBox drpitems;
        private System.Windows.Forms.ComboBox drpaccount;
        private System.Windows.Forms.TextBox txtaccount;
    }
}