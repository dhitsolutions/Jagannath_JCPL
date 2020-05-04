namespace RamdevSales
{
    partial class CashBook
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
            this.label4 = new System.Windows.Forms.Label();
            this.txtopbal = new System.Windows.Forms.TextBox();
            this.txtbalance = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.txttotalpayment = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.txttotalreceive = new System.Windows.Forms.TextBox();
            this.LVledger = new System.Windows.Forms.ListView();
            this.cmbaccname = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.DTPTo = new System.Windows.Forms.DateTimePicker();
            this.BtnViewReport = new System.Windows.Forms.Button();
            this.btngenrpt = new System.Windows.Forms.Button();
            this.DTPFrom = new System.Windows.Forms.DateTimePicker();
            this.btnclose = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(581, 452);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 16);
            this.label4.TabIndex = 209;
            this.label4.Text = "Opening Balance";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // txtopbal
            // 
            this.txtopbal.BackColor = System.Drawing.SystemColors.Window;
            this.txtopbal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtopbal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtopbal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtopbal.Location = new System.Drawing.Point(584, 472);
            this.txtopbal.Multiline = true;
            this.txtopbal.Name = "txtopbal";
            this.txtopbal.Size = new System.Drawing.Size(127, 31);
            this.txtopbal.TabIndex = 210;
            this.txtopbal.TabStop = false;
            this.txtopbal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtopbal.TextChanged += new System.EventHandler(this.txtopbal_TextChanged);
            // 
            // txtbalance
            // 
            this.txtbalance.BackColor = System.Drawing.SystemColors.Window;
            this.txtbalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbalance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtbalance.Location = new System.Drawing.Point(943, 472);
            this.txtbalance.Multiline = true;
            this.txtbalance.Name = "txtbalance";
            this.txtbalance.Size = new System.Drawing.Size(114, 31);
            this.txtbalance.TabIndex = 208;
            this.txtbalance.TabStop = false;
            this.txtbalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtbalance.TextChanged += new System.EventHandler(this.txtbalance_TextChanged);
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.ForeColor = System.Drawing.Color.Blue;
            this.Label6.Location = new System.Drawing.Point(825, 452);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(113, 16);
            this.Label6.TabIndex = 204;
            this.Label6.Text = "Total Payment";
            this.Label6.Click += new System.EventHandler(this.Label6_Click);
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.ForeColor = System.Drawing.Color.Blue;
            this.Label7.Location = new System.Drawing.Point(961, 452);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(66, 16);
            this.Label7.TabIndex = 205;
            this.Label7.Text = "Balance";
            this.Label7.Click += new System.EventHandler(this.Label7_Click);
            // 
            // txttotalpayment
            // 
            this.txttotalpayment.BackColor = System.Drawing.SystemColors.Window;
            this.txttotalpayment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txttotalpayment.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttotalpayment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txttotalpayment.Location = new System.Drawing.Point(828, 472);
            this.txttotalpayment.Multiline = true;
            this.txttotalpayment.Name = "txttotalpayment";
            this.txttotalpayment.Size = new System.Drawing.Size(114, 31);
            this.txttotalpayment.TabIndex = 207;
            this.txttotalpayment.TabStop = false;
            this.txttotalpayment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txttotalpayment.TextChanged += new System.EventHandler(this.txttotcredit_TextChanged);
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.ForeColor = System.Drawing.Color.Blue;
            this.Label5.Location = new System.Drawing.Point(715, 452);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(105, 16);
            this.Label5.TabIndex = 203;
            this.Label5.Text = "Total Receive";
            this.Label5.Click += new System.EventHandler(this.Label5_Click);
            // 
            // txttotalreceive
            // 
            this.txttotalreceive.BackColor = System.Drawing.SystemColors.Window;
            this.txttotalreceive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txttotalreceive.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttotalreceive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txttotalreceive.Location = new System.Drawing.Point(713, 472);
            this.txttotalreceive.Multiline = true;
            this.txttotalreceive.Name = "txttotalreceive";
            this.txttotalreceive.Size = new System.Drawing.Size(114, 31);
            this.txttotalreceive.TabIndex = 206;
            this.txttotalreceive.TabStop = false;
            this.txttotalreceive.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txttotalreceive.TextChanged += new System.EventHandler(this.txttotdebit_TextChanged);
            // 
            // LVledger
            // 
            this.LVledger.BackColor = System.Drawing.SystemColors.Window;
            this.LVledger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LVledger.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LVledger.ForeColor = System.Drawing.Color.Navy;
            this.LVledger.FullRowSelect = true;
            this.LVledger.GridLines = true;
            this.LVledger.HideSelection = false;
            this.LVledger.Location = new System.Drawing.Point(8, 103);
            this.LVledger.MultiSelect = false;
            this.LVledger.Name = "LVledger";
            this.LVledger.Size = new System.Drawing.Size(1049, 339);
            this.LVledger.TabIndex = 6;
            this.LVledger.UseCompatibleStateImageBehavior = false;
            this.LVledger.View = System.Windows.Forms.View.Details;
            this.LVledger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LVledger_KeyDown);
            this.LVledger.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LVledger_MouseDoubleClick);
            // 
            // cmbaccname
            // 
            this.cmbaccname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbaccname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbaccname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbaccname.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbaccname.FormattingEnabled = true;
            this.cmbaccname.Location = new System.Drawing.Point(13, 63);
            this.cmbaccname.Name = "cmbaccname";
            this.cmbaccname.Size = new System.Drawing.Size(387, 26);
            this.cmbaccname.TabIndex = 0;
            this.cmbaccname.SelectedIndexChanged += new System.EventHandler(this.cmbaccname_SelectedIndexChanged);
            this.cmbaccname.Enter += new System.EventHandler(this.cmbaccname_Enter);
            this.cmbaccname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbaccname_KeyDown);
            this.cmbaccname.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbaccname_KeyUp);
            this.cmbaccname.Leave += new System.EventHandler(this.cmbaccname_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(157, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 16);
            this.label3.TabIndex = 201;
            this.label3.Text = "Account Name";
            // 
            // DTPTo
            // 
            this.DTPTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPTo.Location = new System.Drawing.Point(568, 62);
            this.DTPTo.Name = "DTPTo";
            this.DTPTo.Size = new System.Drawing.Size(159, 24);
            this.DTPTo.TabIndex = 2;
            this.DTPTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTPTo_KeyDown);
            // 
            // BtnViewReport
            // 
            this.BtnViewReport.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BtnViewReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnViewReport.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnViewReport.ForeColor = System.Drawing.Color.White;
            this.BtnViewReport.Location = new System.Drawing.Point(740, 56);
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
            this.btngenrpt.Location = new System.Drawing.Point(862, 56);
            this.btngenrpt.Name = "btngenrpt";
            this.btngenrpt.Size = new System.Drawing.Size(97, 34);
            this.btngenrpt.TabIndex = 4;
            this.btngenrpt.Text = "Print";
            this.btngenrpt.UseVisualStyleBackColor = false;
            this.btngenrpt.Click += new System.EventHandler(this.btngenrpt_Click);
            this.btngenrpt.Enter += new System.EventHandler(this.btngenrpt_Enter);
            this.btngenrpt.Leave += new System.EventHandler(this.btngenrpt_Leave);
            this.btngenrpt.MouseEnter += new System.EventHandler(this.btngenrpt_MouseEnter);
            this.btngenrpt.MouseLeave += new System.EventHandler(this.btngenrpt_MouseLeave);
            this.btngenrpt.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btngenrpt_MouseMove);
            // 
            // DTPFrom
            // 
            this.DTPFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFrom.Location = new System.Drawing.Point(404, 63);
            this.DTPFrom.Name = "DTPFrom";
            this.DTPFrom.Size = new System.Drawing.Size(154, 24);
            this.DTPFrom.TabIndex = 1;
            this.DTPFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTPFrom_KeyDown);
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(962, 55);
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
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(612, 40);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(63, 16);
            this.Label2.TabIndex = 200;
            this.Label2.Text = "To Date";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(438, 40);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(84, 16);
            this.Label1.TabIndex = 199;
            this.Label1.Text = "From Date";
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.Highlight;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox7.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.ForeColor = System.Drawing.Color.White;
            this.textBox7.Location = new System.Drawing.Point(0, 0);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(1064, 31);
            this.textBox7.TabIndex = 198;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "CASH BOOK";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(0, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1065, 478);
            this.panel1.TabIndex = 211;
            // 
            // CashBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1077, 546);
            this.ControlBox = false;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtopbal);
            this.Controls.Add(this.txtbalance);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.txttotalpayment);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.txttotalreceive);
            this.Controls.Add(this.LVledger);
            this.Controls.Add(this.cmbaccname);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DTPTo);
            this.Controls.Add(this.BtnViewReport);
            this.Controls.Add(this.btngenrpt);
            this.Controls.Add(this.DTPFrom);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CashBook";
            this.Text = "CashBook";
            this.Load += new System.EventHandler(this.CashBook_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox txtopbal;
        internal System.Windows.Forms.TextBox txtbalance;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.TextBox txttotalpayment;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.TextBox txttotalreceive;
        internal System.Windows.Forms.ListView LVledger;
        private System.Windows.Forms.ComboBox cmbaccname;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.DateTimePicker DTPTo;
        internal System.Windows.Forms.Button BtnViewReport;
        internal System.Windows.Forms.Button btngenrpt;
        internal System.Windows.Forms.DateTimePicker DTPFrom;
        internal System.Windows.Forms.Button btnclose;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
    }
}