namespace RamdevSales
{
    partial class PurchaseReturnList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PurchaseReturnList));
            this.TxtInvoice = new System.Windows.Forms.TextBox();
            this.txtnetamt = new System.Windows.Forms.TextBox();
            this.btnnew = new System.Windows.Forms.Button();
            this.Label3 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.DTPTo = new System.Windows.Forms.DateTimePicker();
            this.Label7 = new System.Windows.Forms.Label();
            this.btnclose = new System.Windows.Forms.Button();
            this.txtvat = new System.Windows.Forms.TextBox();
            this.BtnViewReport = new System.Windows.Forms.Button();
            this.Label5 = new System.Windows.Forms.Label();
            this.LVDayBook = new System.Windows.Forms.ListView();
            this.txtbillamt = new System.Windows.Forms.TextBox();
            this.btngenrpt = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.DTPFrom = new System.Windows.Forms.DateTimePicker();
            this.Label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TxtInvoice
            // 
            this.TxtInvoice.BackColor = System.Drawing.Color.Honeydew;
            this.TxtInvoice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtInvoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtInvoice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.TxtInvoice.Location = new System.Drawing.Point(12, 518);
            this.TxtInvoice.Name = "TxtInvoice";
            this.TxtInvoice.Size = new System.Drawing.Size(114, 29);
            this.TxtInvoice.TabIndex = 150;
            this.TxtInvoice.TabStop = false;
            this.TxtInvoice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtnetamt
            // 
            this.txtnetamt.BackColor = System.Drawing.Color.Honeydew;
            this.txtnetamt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtnetamt.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnetamt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtnetamt.Location = new System.Drawing.Point(990, 518);
            this.txtnetamt.Name = "txtnetamt";
            this.txtnetamt.Size = new System.Drawing.Size(114, 29);
            this.txtnetamt.TabIndex = 153;
            this.txtnetamt.TabStop = false;
            this.txtnetamt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnnew
            // 
            this.btnnew.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnnew.Location = new System.Drawing.Point(842, 56);
            this.btnnew.Name = "btnnew";
            this.btnnew.Size = new System.Drawing.Size(81, 65);
            this.btnnew.TabIndex = 138;
            this.btnnew.Text = "&NEW";
            this.btnnew.UseVisualStyleBackColor = true;
            this.btnnew.Click += new System.EventHandler(this.btnnew_Click);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.Color.Blue;
            this.Label3.Location = new System.Drawing.Point(12, 498);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(75, 16);
            this.Label3.TabIndex = 146;
            this.Label3.Text = "INVOICE:";
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox4.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.Location = new System.Drawing.Point(1, 2);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(1125, 40);
            this.textBox4.TabIndex = 154;
            this.textBox4.TabStop = false;
            this.textBox4.Text = "Purchase Return List";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.ForeColor = System.Drawing.Color.Blue;
            this.Label6.Location = new System.Drawing.Point(883, 499);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(94, 16);
            this.Label6.TabIndex = 148;
            this.Label6.Text = "Vat Amount";
            // 
            // DTPTo
            // 
            this.DTPTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPTo.Location = new System.Drawing.Point(223, 83);
            this.DTPTo.Name = "DTPTo";
            this.DTPTo.Size = new System.Drawing.Size(181, 38);
            this.DTPTo.TabIndex = 142;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.ForeColor = System.Drawing.Color.Blue;
            this.Label7.Location = new System.Drawing.Point(997, 499);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(94, 16);
            this.Label7.TabIndex = 149;
            this.Label7.Text = "Net Amount";
            // 
            // btnclose
            // 
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.Image = ((System.Drawing.Image)(resources.GetObject("btnclose.Image")));
            this.btnclose.Location = new System.Drawing.Point(1028, 48);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(77, 83);
            this.btnclose.TabIndex = 145;
            this.btnclose.Text = "CLOSE";
            this.btnclose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // txtvat
            // 
            this.txtvat.BackColor = System.Drawing.Color.Honeydew;
            this.txtvat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtvat.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtvat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtvat.Location = new System.Drawing.Point(875, 518);
            this.txtvat.Name = "txtvat";
            this.txtvat.Size = new System.Drawing.Size(114, 29);
            this.txtvat.TabIndex = 152;
            this.txtvat.TabStop = false;
            this.txtvat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // BtnViewReport
            // 
            this.BtnViewReport.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnViewReport.Location = new System.Drawing.Point(419, 82);
            this.BtnViewReport.Name = "BtnViewReport";
            this.BtnViewReport.Size = new System.Drawing.Size(55, 39);
            this.BtnViewReport.TabIndex = 143;
            this.BtnViewReport.Text = "&OK";
            this.BtnViewReport.UseVisualStyleBackColor = true;
            this.BtnViewReport.Click += new System.EventHandler(this.BtnViewReport_Click);
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.ForeColor = System.Drawing.Color.Blue;
            this.Label5.Location = new System.Drawing.Point(762, 499);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(108, 16);
            this.Label5.TabIndex = 147;
            this.Label5.Text = "Basic Amount";
            // 
            // LVDayBook
            // 
            this.LVDayBook.BackColor = System.Drawing.Color.LightYellow;
            this.LVDayBook.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LVDayBook.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LVDayBook.ForeColor = System.Drawing.Color.Navy;
            this.LVDayBook.FullRowSelect = true;
            this.LVDayBook.GridLines = true;
            this.LVDayBook.HideSelection = false;
            this.LVDayBook.Location = new System.Drawing.Point(10, 136);
            this.LVDayBook.MultiSelect = false;
            this.LVDayBook.Name = "LVDayBook";
            this.LVDayBook.Size = new System.Drawing.Size(1095, 361);
            this.LVDayBook.TabIndex = 139;
            this.LVDayBook.UseCompatibleStateImageBehavior = false;
            this.LVDayBook.View = System.Windows.Forms.View.Details;
            this.LVDayBook.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LVDayBook_KeyDown);
            this.LVDayBook.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LVDayBook_MouseDoubleClick);
            // 
            // txtbillamt
            // 
            this.txtbillamt.BackColor = System.Drawing.Color.Honeydew;
            this.txtbillamt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbillamt.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbillamt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtbillamt.Location = new System.Drawing.Point(760, 518);
            this.txtbillamt.Name = "txtbillamt";
            this.txtbillamt.Size = new System.Drawing.Size(114, 29);
            this.txtbillamt.TabIndex = 151;
            this.txtbillamt.TabStop = false;
            this.txtbillamt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btngenrpt
            // 
            this.btngenrpt.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btngenrpt.Location = new System.Drawing.Point(929, 56);
            this.btngenrpt.Name = "btngenrpt";
            this.btngenrpt.Size = new System.Drawing.Size(90, 65);
            this.btngenrpt.TabIndex = 144;
            this.btngenrpt.Text = "GENERATE &REPORT";
            this.btngenrpt.UseVisualStyleBackColor = true;
            this.btngenrpt.Click += new System.EventHandler(this.btngenrpt_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(79, 60);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(84, 16);
            this.Label1.TabIndex = 137;
            this.Label1.Text = "From Date";
            // 
            // DTPFrom
            // 
            this.DTPFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPFrom.Location = new System.Drawing.Point(23, 83);
            this.DTPFrom.Name = "DTPFrom";
            this.DTPFrom.Size = new System.Drawing.Size(194, 38);
            this.DTPFrom.TabIndex = 141;
            this.DTPFrom.ValueChanged += new System.EventHandler(this.DTPFrom_ValueChanged);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(282, 60);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(63, 16);
            this.Label2.TabIndex = 140;
            this.Label2.Text = "To Date";
            // 
            // PurchaseReturnList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1110, 551);
            this.Controls.Add(this.TxtInvoice);
            this.Controls.Add(this.txtnetamt);
            this.Controls.Add(this.btnnew);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.DTPTo);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.txtvat);
            this.Controls.Add(this.BtnViewReport);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.LVDayBook);
            this.Controls.Add(this.txtbillamt);
            this.Controls.Add(this.btngenrpt);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.DTPFrom);
            this.Controls.Add(this.Label2);
            this.Name = "PurchaseReturnList";
            this.Text = "PurchaseReturnList";
            this.Load += new System.EventHandler(this.PurchaseReturnList_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox TxtInvoice;
        internal System.Windows.Forms.TextBox txtnetamt;
        internal System.Windows.Forms.Button btnnew;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox textBox4;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.DateTimePicker DTPTo;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Button btnclose;
        internal System.Windows.Forms.TextBox txtvat;
        internal System.Windows.Forms.Button BtnViewReport;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.ListView LVDayBook;
        internal System.Windows.Forms.TextBox txtbillamt;
        internal System.Windows.Forms.Button btngenrpt;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.DateTimePicker DTPFrom;
        internal System.Windows.Forms.Label Label2;
    }
}