namespace RamdevSales
{
    partial class POSBillList
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
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.btnnew = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.DTPTo = new System.Windows.Forms.DateTimePicker();
            this.BtnViewReport = new System.Windows.Forms.Button();
            this.btngenrpt = new System.Windows.Forms.Button();
            this.DTPFrom = new System.Windows.Forms.DateTimePicker();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.LVDayBook = new System.Windows.Forms.ListView();
            this.rbewallet = new System.Windows.Forms.RadioButton();
            this.rbcdcard = new System.Windows.Forms.RadioButton();
            this.rbcash = new System.Windows.Forms.RadioButton();
            this.rball = new System.Windows.Forms.RadioButton();
            this.TxtInvoice = new System.Windows.Forms.TextBox();
            this.txtnetamt = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.txtvat = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.txtbillamt = new System.Windows.Forms.TextBox();
            this.GroupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.Highlight;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox4.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.ForeColor = System.Drawing.Color.White;
            this.textBox4.Location = new System.Drawing.Point(4, 1);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(961, 31);
            this.textBox4.TabIndex = 119;
            this.textBox4.TabStop = false;
            this.textBox4.Text = "BILL LIST";
            // 
            // GroupBox2
            // 
            this.GroupBox2.BackColor = System.Drawing.Color.White;
            this.GroupBox2.Controls.Add(this.btnnew);
            this.GroupBox2.Controls.Add(this.btnclose);
            this.GroupBox2.Controls.Add(this.DTPTo);
            this.GroupBox2.Controls.Add(this.BtnViewReport);
            this.GroupBox2.Controls.Add(this.btngenrpt);
            this.GroupBox2.Controls.Add(this.DTPFrom);
            this.GroupBox2.Controls.Add(this.Label2);
            this.GroupBox2.Controls.Add(this.Label1);
            this.GroupBox2.Location = new System.Drawing.Point(3, 3);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(953, 77);
            this.GroupBox2.TabIndex = 120;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Enter += new System.EventHandler(this.GroupBox2_Enter);
            // 
            // btnnew
            // 
            this.btnnew.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnnew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnnew.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnnew.ForeColor = System.Drawing.Color.White;
            this.btnnew.Location = new System.Drawing.Point(579, 26);
            this.btnnew.Name = "btnnew";
            this.btnnew.Size = new System.Drawing.Size(97, 34);
            this.btnnew.TabIndex = 0;
            this.btnnew.Text = "New";
            this.btnnew.UseVisualStyleBackColor = false;
            this.btnnew.Click += new System.EventHandler(this.btnnew_Click);
            this.btnnew.Enter += new System.EventHandler(this.btnnew_Enter);
            this.btnnew.Leave += new System.EventHandler(this.btnnew_Leave);
            this.btnnew.MouseEnter += new System.EventHandler(this.btnnew_MouseEnter);
            this.btnnew.MouseLeave += new System.EventHandler(this.btnnew_MouseLeave);
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(836, 25);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(97, 34);
            this.btnclose.TabIndex = 5;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            this.btnclose.Enter += new System.EventHandler(this.btnclose_Enter);
            this.btnclose.Leave += new System.EventHandler(this.btnclose_Leave);
            this.btnclose.MouseEnter += new System.EventHandler(this.btnclose_MouseEnter);
            this.btnclose.MouseLeave += new System.EventHandler(this.btnclose_MouseLeave);
            // 
            // DTPTo
            // 
            this.DTPTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPTo.Location = new System.Drawing.Point(150, 31);
            this.DTPTo.Name = "DTPTo";
            this.DTPTo.Size = new System.Drawing.Size(131, 22);
            this.DTPTo.TabIndex = 2;
            this.DTPTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTPTo_KeyDown);
            // 
            // BtnViewReport
            // 
            this.BtnViewReport.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BtnViewReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnViewReport.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnViewReport.ForeColor = System.Drawing.Color.White;
            this.BtnViewReport.Location = new System.Drawing.Point(287, 26);
            this.BtnViewReport.Name = "BtnViewReport";
            this.BtnViewReport.Size = new System.Drawing.Size(97, 34);
            this.BtnViewReport.TabIndex = 3;
            this.BtnViewReport.Text = "OK";
            this.BtnViewReport.UseVisualStyleBackColor = false;
            this.BtnViewReport.Click += new System.EventHandler(this.BtnViewReport_Click);
            this.BtnViewReport.Enter += new System.EventHandler(this.BtnViewReport_Enter);
            this.BtnViewReport.Leave += new System.EventHandler(this.BtnViewReport_Leave);
            this.BtnViewReport.MouseEnter += new System.EventHandler(this.BtnViewReport_MouseEnter);
            this.BtnViewReport.MouseLeave += new System.EventHandler(this.BtnViewReport_MouseLeave);
            // 
            // btngenrpt
            // 
            this.btngenrpt.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btngenrpt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btngenrpt.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btngenrpt.ForeColor = System.Drawing.Color.White;
            this.btngenrpt.Location = new System.Drawing.Point(682, 25);
            this.btngenrpt.Name = "btngenrpt";
            this.btngenrpt.Size = new System.Drawing.Size(148, 34);
            this.btngenrpt.TabIndex = 4;
            this.btngenrpt.Text = "Generate Report";
            this.btngenrpt.UseVisualStyleBackColor = false;
            this.btngenrpt.Click += new System.EventHandler(this.btngenrpt_Click);
            this.btngenrpt.Enter += new System.EventHandler(this.btngenrpt_Enter);
            this.btngenrpt.Leave += new System.EventHandler(this.btngenrpt_Leave);
            this.btngenrpt.MouseEnter += new System.EventHandler(this.btngenrpt_MouseEnter);
            this.btngenrpt.MouseLeave += new System.EventHandler(this.btngenrpt_MouseLeave);
            // 
            // DTPFrom
            // 
            this.DTPFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFrom.Location = new System.Drawing.Point(12, 32);
            this.DTPFrom.Name = "DTPFrom";
            this.DTPFrom.Size = new System.Drawing.Size(129, 22);
            this.DTPFrom.TabIndex = 1;
            this.DTPFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTPFrom_KeyDown);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(183, 13);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(63, 16);
            this.Label2.TabIndex = 1;
            this.Label2.Text = "To Date";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(40, 13);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(84, 16);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "From Date";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.GroupBox2);
            this.panel1.Controls.Add(this.GroupBox1);
            this.panel1.Location = new System.Drawing.Point(4, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(961, 507);
            this.panel1.TabIndex = 6;
            // 
            // GroupBox1
            // 
            this.GroupBox1.BackColor = System.Drawing.Color.White;
            this.GroupBox1.Controls.Add(this.LVDayBook);
            this.GroupBox1.Controls.Add(this.rbewallet);
            this.GroupBox1.Controls.Add(this.rbcdcard);
            this.GroupBox1.Controls.Add(this.rbcash);
            this.GroupBox1.Controls.Add(this.rball);
            this.GroupBox1.Controls.Add(this.TxtInvoice);
            this.GroupBox1.Controls.Add(this.txtnetamt);
            this.GroupBox1.Controls.Add(this.Label3);
            this.GroupBox1.Controls.Add(this.Label6);
            this.GroupBox1.Controls.Add(this.Label7);
            this.GroupBox1.Controls.Add(this.txtvat);
            this.GroupBox1.Controls.Add(this.Label5);
            this.GroupBox1.Controls.Add(this.txtbillamt);
            this.GroupBox1.Location = new System.Drawing.Point(3, 86);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(953, 418);
            this.GroupBox1.TabIndex = 121;
            this.GroupBox1.TabStop = false;
            // 
            // LVDayBook
            // 
            this.LVDayBook.BackColor = System.Drawing.SystemColors.Window;
            this.LVDayBook.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LVDayBook.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LVDayBook.ForeColor = System.Drawing.Color.Navy;
            this.LVDayBook.FullRowSelect = true;
            this.LVDayBook.GridLines = true;
            this.LVDayBook.HideSelection = false;
            this.LVDayBook.Location = new System.Drawing.Point(8, 16);
            this.LVDayBook.MultiSelect = false;
            this.LVDayBook.Name = "LVDayBook";
            this.LVDayBook.Size = new System.Drawing.Size(933, 329);
            this.LVDayBook.TabIndex = 0;
            this.LVDayBook.UseCompatibleStateImageBehavior = false;
            this.LVDayBook.View = System.Windows.Forms.View.Details;
            this.LVDayBook.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LVDayBook_KeyDown);
            this.LVDayBook.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LVDayBook_MouseDoubleClick);
            // 
            // rbewallet
            // 
            this.rbewallet.AutoSize = true;
            this.rbewallet.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbewallet.Location = new System.Drawing.Point(415, 372);
            this.rbewallet.Name = "rbewallet";
            this.rbewallet.Size = new System.Drawing.Size(78, 18);
            this.rbewallet.TabIndex = 163;
            this.rbewallet.Text = "E-Wallet";
            this.rbewallet.UseVisualStyleBackColor = true;
            // 
            // rbcdcard
            // 
            this.rbcdcard.AutoSize = true;
            this.rbcdcard.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbcdcard.Location = new System.Drawing.Point(274, 372);
            this.rbcdcard.Name = "rbcdcard";
            this.rbcdcard.Size = new System.Drawing.Size(135, 18);
            this.rbcdcard.TabIndex = 162;
            this.rbcdcard.Text = "Credit/Debit Card";
            this.rbcdcard.UseVisualStyleBackColor = true;
            // 
            // rbcash
            // 
            this.rbcash.AutoSize = true;
            this.rbcash.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbcash.Location = new System.Drawing.Point(212, 372);
            this.rbcash.Name = "rbcash";
            this.rbcash.Size = new System.Drawing.Size(57, 18);
            this.rbcash.TabIndex = 161;
            this.rbcash.Text = "Cash";
            this.rbcash.UseVisualStyleBackColor = true;
            // 
            // rball
            // 
            this.rball.AutoSize = true;
            this.rball.Checked = true;
            this.rball.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rball.Location = new System.Drawing.Point(150, 372);
            this.rball.Name = "rball";
            this.rball.Size = new System.Drawing.Size(39, 18);
            this.rball.TabIndex = 160;
            this.rball.TabStop = true;
            this.rball.Text = "All";
            this.rball.UseVisualStyleBackColor = true;
            // 
            // TxtInvoice
            // 
            this.TxtInvoice.BackColor = System.Drawing.SystemColors.Window;
            this.TxtInvoice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtInvoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtInvoice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.TxtInvoice.Location = new System.Drawing.Point(11, 370);
            this.TxtInvoice.Multiline = true;
            this.TxtInvoice.Name = "TxtInvoice";
            this.TxtInvoice.Size = new System.Drawing.Size(114, 31);
            this.TxtInvoice.TabIndex = 13;
            this.TxtInvoice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtInvoice.TextChanged += new System.EventHandler(this.TxtInvoice_TextChanged);
            this.TxtInvoice.Enter += new System.EventHandler(this.TxtInvoice_Enter);
            this.TxtInvoice.Leave += new System.EventHandler(this.TxtInvoice_Leave);
            // 
            // txtnetamt
            // 
            this.txtnetamt.BackColor = System.Drawing.SystemColors.Window;
            this.txtnetamt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtnetamt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnetamt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtnetamt.Location = new System.Drawing.Point(815, 371);
            this.txtnetamt.Multiline = true;
            this.txtnetamt.Name = "txtnetamt";
            this.txtnetamt.Size = new System.Drawing.Size(114, 30);
            this.txtnetamt.TabIndex = 17;
            this.txtnetamt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtnetamt.Enter += new System.EventHandler(this.txtnetamt_Enter);
            this.txtnetamt.Leave += new System.EventHandler(this.txtnetamt_Leave);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.Color.Blue;
            this.Label3.Location = new System.Drawing.Point(11, 348);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(75, 16);
            this.Label3.TabIndex = 7;
            this.Label3.Text = "INVOICE:";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.ForeColor = System.Drawing.Color.Blue;
            this.Label6.Location = new System.Drawing.Point(708, 348);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(96, 16);
            this.Label6.TabIndex = 10;
            this.Label6.Text = "GST Amount";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.ForeColor = System.Drawing.Color.Blue;
            this.Label7.Location = new System.Drawing.Point(822, 348);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(94, 16);
            this.Label7.TabIndex = 11;
            this.Label7.Text = "Net Amount";
            // 
            // txtvat
            // 
            this.txtvat.BackColor = System.Drawing.SystemColors.Window;
            this.txtvat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtvat.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtvat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtvat.Location = new System.Drawing.Point(700, 371);
            this.txtvat.Multiline = true;
            this.txtvat.Name = "txtvat";
            this.txtvat.Size = new System.Drawing.Size(114, 30);
            this.txtvat.TabIndex = 16;
            this.txtvat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtvat.Enter += new System.EventHandler(this.txtvat_Enter);
            this.txtvat.Leave += new System.EventHandler(this.txtvat_Leave);
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.ForeColor = System.Drawing.Color.Blue;
            this.Label5.Location = new System.Drawing.Point(587, 348);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(108, 16);
            this.Label5.TabIndex = 9;
            this.Label5.Text = "Basic Amount";
            // 
            // txtbillamt
            // 
            this.txtbillamt.BackColor = System.Drawing.SystemColors.Window;
            this.txtbillamt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbillamt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbillamt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtbillamt.Location = new System.Drawing.Point(585, 371);
            this.txtbillamt.Multiline = true;
            this.txtbillamt.Name = "txtbillamt";
            this.txtbillamt.Size = new System.Drawing.Size(114, 30);
            this.txtbillamt.TabIndex = 15;
            this.txtbillamt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtbillamt.Enter += new System.EventHandler(this.txtbillamt_Enter);
            this.txtbillamt.Leave += new System.EventHandler(this.txtbillamt_Leave);
            // 
            // POSBillList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(966, 557);
            this.ControlBox = false;
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "POSBillList";
            this.Text = "POSBillList";
            this.Load += new System.EventHandler(this.POSBillList_Load);
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox textBox4;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.Button btnnew;
        internal System.Windows.Forms.DateTimePicker DTPTo;
        internal System.Windows.Forms.Button BtnViewReport;
        internal System.Windows.Forms.DateTimePicker DTPFrom;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button btnclose;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.TextBox TxtInvoice;
        internal System.Windows.Forms.TextBox txtnetamt;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.ListView LVDayBook;
        internal System.Windows.Forms.TextBox txtvat;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.TextBox txtbillamt;
        internal System.Windows.Forms.Button btngenrpt;
        private System.Windows.Forms.RadioButton rball;
        private System.Windows.Forms.RadioButton rbewallet;
        private System.Windows.Forms.RadioButton rbcdcard;
        private System.Windows.Forms.RadioButton rbcash;
        private System.Windows.Forms.Panel panel1;
    }
}