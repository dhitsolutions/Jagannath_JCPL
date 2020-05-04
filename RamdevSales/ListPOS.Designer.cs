namespace RamdevSales
{
    partial class ListPOS
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkall = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.rbewallet = new System.Windows.Forms.RadioButton();
            this.rbcredit = new System.Windows.Forms.RadioButton();
            this.txtqty = new System.Windows.Forms.TextBox();
            this.rbcash = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.txtchange = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txttotalnet = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtotheramt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtcashamt = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.txtbillamt = new System.Windows.Forms.TextBox();
            this.LVbill = new System.Windows.Forms.ListView();
            this.btnnew = new System.Windows.Forms.Button();
            this.btnprint = new System.Windows.Forms.Button();
            this.txtser = new System.Windows.Forms.TextBox();
            this.txtsearch = new System.Windows.Forms.ComboBox();
            this.DTPTo = new System.Windows.Forms.DateTimePicker();
            this.BtnViewReport = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.DTPFrom = new System.Windows.Forms.DateTimePicker();
            this.Label2 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnclose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkall);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.rbewallet);
            this.groupBox1.Controls.Add(this.rbcredit);
            this.groupBox1.Controls.Add(this.txtqty);
            this.groupBox1.Controls.Add(this.rbcash);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtchange);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txttotalnet);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtotheramt);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtcashamt);
            this.groupBox1.Controls.Add(this.Label5);
            this.groupBox1.Controls.Add(this.txtbillamt);
            this.groupBox1.Controls.Add(this.LVbill);
            this.groupBox1.Location = new System.Drawing.Point(12, 91);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1036, 469);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // chkall
            // 
            this.chkall.AutoSize = true;
            this.chkall.Checked = true;
            this.chkall.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkall.Location = new System.Drawing.Point(5, 420);
            this.chkall.Name = "chkall";
            this.chkall.Size = new System.Drawing.Size(39, 18);
            this.chkall.TabIndex = 178;
            this.chkall.TabStop = true;
            this.chkall.Text = "All";
            this.chkall.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(332, 395);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 16);
            this.label8.TabIndex = 26;
            this.label8.Text = "Total Qty";
            // 
            // rbewallet
            // 
            this.rbewallet.AutoSize = true;
            this.rbewallet.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbewallet.Location = new System.Drawing.Point(234, 420);
            this.rbewallet.Name = "rbewallet";
            this.rbewallet.Size = new System.Drawing.Size(78, 18);
            this.rbewallet.TabIndex = 177;
            this.rbewallet.Text = "E-Wallet";
            this.rbewallet.UseVisualStyleBackColor = true;
            // 
            // rbcredit
            // 
            this.rbcredit.AutoSize = true;
            this.rbcredit.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbcredit.Location = new System.Drawing.Point(101, 420);
            this.rbcredit.Name = "rbcredit";
            this.rbcredit.Size = new System.Drawing.Size(135, 18);
            this.rbcredit.TabIndex = 176;
            this.rbcredit.Text = "Credit/Debit Card";
            this.rbcredit.UseVisualStyleBackColor = true;
            // 
            // txtqty
            // 
            this.txtqty.BackColor = System.Drawing.SystemColors.Window;
            this.txtqty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtqty.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtqty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtqty.Location = new System.Drawing.Point(312, 418);
            this.txtqty.Multiline = true;
            this.txtqty.Name = "txtqty";
            this.txtqty.Size = new System.Drawing.Size(114, 35);
            this.txtqty.TabIndex = 27;
            this.txtqty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // rbcash
            // 
            this.rbcash.AutoSize = true;
            this.rbcash.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbcash.Location = new System.Drawing.Point(45, 420);
            this.rbcash.Name = "rbcash";
            this.rbcash.Size = new System.Drawing.Size(57, 18);
            this.rbcash.TabIndex = 175;
            this.rbcash.Text = "Cash";
            this.rbcash.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Blue;
            this.label7.Location = new System.Drawing.Point(934, 395);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 16);
            this.label7.TabIndex = 24;
            this.label7.Text = "Change";
            // 
            // txtchange
            // 
            this.txtchange.BackColor = System.Drawing.SystemColors.Window;
            this.txtchange.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtchange.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtchange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtchange.Location = new System.Drawing.Point(912, 418);
            this.txtchange.Multiline = true;
            this.txtchange.Name = "txtchange";
            this.txtchange.Size = new System.Drawing.Size(114, 35);
            this.txtchange.TabIndex = 25;
            this.txtchange.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(794, 395);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 16);
            this.label6.TabIndex = 22;
            this.label6.Text = "Total Net Amt";
            // 
            // txttotalnet
            // 
            this.txttotalnet.BackColor = System.Drawing.SystemColors.Window;
            this.txttotalnet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txttotalnet.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttotalnet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txttotalnet.Location = new System.Drawing.Point(792, 418);
            this.txttotalnet.Multiline = true;
            this.txttotalnet.Name = "txttotalnet";
            this.txttotalnet.Size = new System.Drawing.Size(114, 35);
            this.txttotalnet.TabIndex = 23;
            this.txttotalnet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(687, 395);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 16);
            this.label4.TabIndex = 20;
            this.label4.Text = "Other Amt";
            // 
            // txtotheramt
            // 
            this.txtotheramt.BackColor = System.Drawing.SystemColors.Window;
            this.txtotheramt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtotheramt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtotheramt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtotheramt.Location = new System.Drawing.Point(672, 418);
            this.txtotheramt.Multiline = true;
            this.txtotheramt.Name = "txtotheramt";
            this.txtotheramt.Size = new System.Drawing.Size(114, 35);
            this.txtotheramt.TabIndex = 21;
            this.txtotheramt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(554, 395);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 16);
            this.label3.TabIndex = 18;
            this.label3.Text = "Total Cash Amt";
            // 
            // txtcashamt
            // 
            this.txtcashamt.BackColor = System.Drawing.SystemColors.Window;
            this.txtcashamt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtcashamt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcashamt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtcashamt.Location = new System.Drawing.Point(552, 418);
            this.txtcashamt.Multiline = true;
            this.txtcashamt.Name = "txtcashamt";
            this.txtcashamt.Size = new System.Drawing.Size(114, 35);
            this.txtcashamt.TabIndex = 19;
            this.txtcashamt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.ForeColor = System.Drawing.Color.Blue;
            this.Label5.Location = new System.Drawing.Point(434, 395);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(104, 16);
            this.Label5.TabIndex = 16;
            this.Label5.Text = "Total Bill Amt";
            // 
            // txtbillamt
            // 
            this.txtbillamt.BackColor = System.Drawing.SystemColors.Window;
            this.txtbillamt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbillamt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbillamt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtbillamt.Location = new System.Drawing.Point(432, 418);
            this.txtbillamt.Multiline = true;
            this.txtbillamt.Name = "txtbillamt";
            this.txtbillamt.Size = new System.Drawing.Size(114, 35);
            this.txtbillamt.TabIndex = 17;
            this.txtbillamt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LVbill
            // 
            this.LVbill.BackColor = System.Drawing.SystemColors.Window;
            this.LVbill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LVbill.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LVbill.ForeColor = System.Drawing.Color.Navy;
            this.LVbill.FullRowSelect = true;
            this.LVbill.GridLines = true;
            this.LVbill.HideSelection = false;
            this.LVbill.Location = new System.Drawing.Point(12, 19);
            this.LVbill.MultiSelect = false;
            this.LVbill.Name = "LVbill";
            this.LVbill.Size = new System.Drawing.Size(1013, 373);
            this.LVbill.TabIndex = 0;
            this.LVbill.UseCompatibleStateImageBehavior = false;
            this.LVbill.View = System.Windows.Forms.View.Details;
            this.LVbill.SelectedIndexChanged += new System.EventHandler(this.LVbill_SelectedIndexChanged);
            this.LVbill.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LVbill_KeyDown);
            this.LVbill.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LVbill_MouseDoubleClick);
            // 
            // btnnew
            // 
            this.btnnew.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnnew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnnew.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnnew.ForeColor = System.Drawing.Color.White;
            this.btnnew.Location = new System.Drawing.Point(856, 52);
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
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprint.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(751, 52);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(103, 34);
            this.btnprint.TabIndex = 5;
            this.btnprint.Text = "&Print";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnsearch_Click);
            this.btnprint.Enter += new System.EventHandler(this.btnsearch_Enter);
            this.btnprint.Leave += new System.EventHandler(this.btnsearch_Leave);
            this.btnprint.MouseEnter += new System.EventHandler(this.btnsearch_MouseEnter);
            this.btnprint.MouseLeave += new System.EventHandler(this.btnsearch_MouseLeave);
            // 
            // txtser
            // 
            this.txtser.BackColor = System.Drawing.Color.White;
            this.txtser.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtser.Location = new System.Drawing.Point(863, 57);
            this.txtser.Name = "txtser";
            this.txtser.Size = new System.Drawing.Size(16, 23);
            this.txtser.TabIndex = 4;
            this.txtser.Visible = false;
            this.txtser.TextChanged += new System.EventHandler(this.txtser_TextChanged);
            this.txtser.Enter += new System.EventHandler(this.txtser_Enter);
            this.txtser.Leave += new System.EventHandler(this.txtser_Leave);
            // 
            // txtsearch
            // 
            this.txtsearch.FormattingEnabled = true;
            this.txtsearch.Location = new System.Drawing.Point(523, 5);
            this.txtsearch.Name = "txtsearch";
            this.txtsearch.Size = new System.Drawing.Size(21, 21);
            this.txtsearch.TabIndex = 7;
            this.txtsearch.Visible = false;
            // 
            // DTPTo
            // 
            this.DTPTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPTo.Location = new System.Drawing.Point(180, 25);
            this.DTPTo.Name = "DTPTo";
            this.DTPTo.Size = new System.Drawing.Size(142, 22);
            this.DTPTo.TabIndex = 2;
            this.DTPTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTPTo_KeyDown);
            // 
            // BtnViewReport
            // 
            this.BtnViewReport.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BtnViewReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnViewReport.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnViewReport.ForeColor = System.Drawing.Color.White;
            this.BtnViewReport.Location = new System.Drawing.Point(343, 21);
            this.BtnViewReport.Name = "BtnViewReport";
            this.BtnViewReport.Size = new System.Drawing.Size(97, 34);
            this.BtnViewReport.TabIndex = 3;
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
            this.Label1.Location = new System.Drawing.Point(62, 3);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(84, 16);
            this.Label1.TabIndex = 10;
            this.Label1.Text = "From Date";
            // 
            // DTPFrom
            // 
            this.DTPFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFrom.Location = new System.Drawing.Point(23, 26);
            this.DTPFrom.Name = "DTPFrom";
            this.DTPFrom.Size = new System.Drawing.Size(143, 22);
            this.DTPFrom.TabIndex = 1;
            this.DTPFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTPFrom_KeyDown);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(221, 3);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(63, 16);
            this.Label2.TabIndex = 12;
            this.Label2.Text = "To Date";
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.Highlight;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox7.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.ForeColor = System.Drawing.Color.White;
            this.textBox7.Location = new System.Drawing.Point(0, -1);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(1055, 31);
            this.textBox7.TabIndex = 173;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "POS LIST";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnclose);
            this.panel1.Controls.Add(this.DTPTo);
            this.panel1.Controls.Add(this.Label1);
            this.panel1.Controls.Add(this.BtnViewReport);
            this.panel1.Controls.Add(this.DTPFrom);
            this.panel1.Controls.Add(this.txtsearch);
            this.panel1.Controls.Add(this.Label2);
            this.panel1.Location = new System.Drawing.Point(0, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1055, 534);
            this.panel1.TabIndex = 174;
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(953, 20);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(97, 35);
            this.btnclose.TabIndex = 178;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            this.btnclose.Enter += new System.EventHandler(this.btnclose_Enter);
            this.btnclose.Leave += new System.EventHandler(this.btnclose_Leave);
            this.btnclose.MouseEnter += new System.EventHandler(this.btnclose_MouseEnter);
            this.btnclose.MouseLeave += new System.EventHandler(this.btnclose_MouseLeave);
            // 
            // ListPOS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1060, 576);
            this.ControlBox = false;
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.btnprint);
            this.Controls.Add(this.btnnew);
            this.Controls.Add(this.txtser);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ListPOS";
            this.Text = "ListPOS";
            
            this.Load += new System.EventHandler(this.ListPOS_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.ListView LVbill;
        private System.Windows.Forms.Button btnnew;
        private System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.TextBox txtser;
        private System.Windows.Forms.ComboBox txtsearch;
        internal System.Windows.Forms.DateTimePicker DTPTo;
        internal System.Windows.Forms.Button BtnViewReport;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.DateTimePicker DTPFrom;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbcash;
        private System.Windows.Forms.RadioButton rbcredit;
        private System.Windows.Forms.RadioButton rbewallet;
        internal System.Windows.Forms.Button btnclose;
        internal System.Windows.Forms.Label label8;
        internal System.Windows.Forms.TextBox txtqty;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.TextBox txtchange;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.TextBox txttotalnet;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox txtotheramt;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox txtcashamt;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.TextBox txtbillamt;
        private System.Windows.Forms.RadioButton chkall;
    }
}