namespace RamdevSales
{
    partial class StockIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StockIn));
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.LVDayBook = new System.Windows.Forms.ListView();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.txtnetamt = new System.Windows.Forms.TextBox();
            this.TxtInvoice = new System.Windows.Forms.TextBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.DTPTo = new System.Windows.Forms.DateTimePicker();
            this.BtnViewReport = new System.Windows.Forms.Button();
            this.btngenrpt = new System.Windows.Forms.Button();
            this.DTPFrom = new System.Windows.Forms.DateTimePicker();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.btnclose = new System.Windows.Forms.Button();
            this.GroupBox1.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox4.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.Location = new System.Drawing.Point(0, 0);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(1125, 40);
            this.textBox4.TabIndex = 122;
            this.textBox4.TabStop = false;
            this.textBox4.Text = "DATE WISE STOCK INWARD REPORT";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.LVDayBook);
            this.GroupBox1.Location = new System.Drawing.Point(1, 134);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(860, 411);
            this.GroupBox1.TabIndex = 120;
            this.GroupBox1.TabStop = false;
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
            this.LVDayBook.Location = new System.Drawing.Point(11, 16);
            this.LVDayBook.MultiSelect = false;
            this.LVDayBook.Name = "LVDayBook";
            this.LVDayBook.Size = new System.Drawing.Size(830, 388);
            this.LVDayBook.TabIndex = 0;
            this.LVDayBook.UseCompatibleStateImageBehavior = false;
            this.LVDayBook.View = System.Windows.Forms.View.Details;
            this.LVDayBook.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LVDayBook_MouseDoubleClick);
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.txtnetamt);
            this.GroupBox3.Controls.Add(this.TxtInvoice);
            this.GroupBox3.Controls.Add(this.Label7);
            this.GroupBox3.Controls.Add(this.Label3);
            this.GroupBox3.Location = new System.Drawing.Point(867, 143);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(244, 395);
            this.GroupBox3.TabIndex = 123;
            this.GroupBox3.TabStop = false;
            // 
            // txtnetamt
            // 
            this.txtnetamt.BackColor = System.Drawing.Color.Honeydew;
            this.txtnetamt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtnetamt.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnetamt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtnetamt.Location = new System.Drawing.Point(124, 64);
            this.txtnetamt.Name = "txtnetamt";
            this.txtnetamt.Size = new System.Drawing.Size(114, 29);
            this.txtnetamt.TabIndex = 17;
            this.txtnetamt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxtInvoice
            // 
            this.TxtInvoice.BackColor = System.Drawing.Color.Honeydew;
            this.TxtInvoice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtInvoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtInvoice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.TxtInvoice.Location = new System.Drawing.Point(123, 25);
            this.TxtInvoice.Name = "TxtInvoice";
            this.TxtInvoice.Size = new System.Drawing.Size(114, 29);
            this.TxtInvoice.TabIndex = 13;
            this.TxtInvoice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.ForeColor = System.Drawing.Color.Blue;
            this.Label7.Location = new System.Drawing.Point(6, 71);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(99, 16);
            this.Label7.TabIndex = 11;
            this.Label7.Text = "Net Amount:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.Color.Blue;
            this.Label3.Location = new System.Drawing.Point(8, 33);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(75, 16);
            this.Label3.TabIndex = 7;
            this.Label3.Text = "INVOICE:";
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.DTPTo);
            this.GroupBox2.Controls.Add(this.BtnViewReport);
            this.GroupBox2.Controls.Add(this.btngenrpt);
            this.GroupBox2.Controls.Add(this.DTPFrom);
            this.GroupBox2.Controls.Add(this.Label2);
            this.GroupBox2.Controls.Add(this.Label1);
            this.GroupBox2.Location = new System.Drawing.Point(1, 45);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(1012, 83);
            this.GroupBox2.TabIndex = 121;
            this.GroupBox2.TabStop = false;
            // 
            // DTPTo
            // 
            this.DTPTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPTo.Location = new System.Drawing.Point(415, 23);
            this.DTPTo.Name = "DTPTo";
            this.DTPTo.Size = new System.Drawing.Size(181, 38);
            this.DTPTo.TabIndex = 3;
            // 
            // BtnViewReport
            // 
            this.BtnViewReport.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnViewReport.Location = new System.Drawing.Point(832, 12);
            this.BtnViewReport.Name = "BtnViewReport";
            this.BtnViewReport.Size = new System.Drawing.Size(81, 65);
            this.BtnViewReport.TabIndex = 71;
            this.BtnViewReport.Text = "VIEW REPORT";
            this.BtnViewReport.UseVisualStyleBackColor = true;
            this.BtnViewReport.Click += new System.EventHandler(this.BtnViewReport_Click);
            // 
            // btngenrpt
            // 
            this.btngenrpt.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btngenrpt.Location = new System.Drawing.Point(912, 12);
            this.btngenrpt.Name = "btngenrpt";
            this.btngenrpt.Size = new System.Drawing.Size(90, 65);
            this.btngenrpt.TabIndex = 73;
            this.btngenrpt.Text = "GENERATE REPORT";
            this.btngenrpt.UseVisualStyleBackColor = true;
            this.btngenrpt.Click += new System.EventHandler(this.btngenrpt_Click);
            // 
            // DTPFrom
            // 
            this.DTPFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPFrom.Location = new System.Drawing.Point(110, 24);
            this.DTPFrom.Name = "DTPFrom";
            this.DTPFrom.Size = new System.Drawing.Size(194, 38);
            this.DTPFrom.TabIndex = 2;
            this.DTPFrom.ValueChanged += new System.EventHandler(this.DTPFrom_ValueChanged);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(381, 33);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(32, 16);
            this.Label2.TabIndex = 1;
            this.Label2.Text = "TO:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(52, 33);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(54, 16);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "FROM:";
            // 
            // btnclose
            // 
            this.btnclose.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.Image = ((System.Drawing.Image)(resources.GetObject("btnclose.Image")));
            this.btnclose.Location = new System.Drawing.Point(1027, 46);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(77, 83);
            this.btnclose.TabIndex = 124;
            this.btnclose.Text = "CLOSE";
            this.btnclose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // StockIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1125, 552);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.GroupBox3);
            this.Controls.Add(this.GroupBox2);
            this.Name = "StockIn";
            this.Text = "StockIn";
            this.Load += new System.EventHandler(this.StockIn_Load);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox3.PerformLayout();
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox textBox4;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.ListView LVDayBook;
        internal System.Windows.Forms.GroupBox GroupBox3;
        internal System.Windows.Forms.TextBox txtnetamt;
        internal System.Windows.Forms.TextBox TxtInvoice;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.DateTimePicker DTPTo;
        internal System.Windows.Forms.Button BtnViewReport;
        internal System.Windows.Forms.Button btngenrpt;
        internal System.Windows.Forms.DateTimePicker DTPFrom;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button btnclose;
    }
}