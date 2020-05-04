namespace RamdevSales
{
    partial class Ledger
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
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.DTPTo = new System.Windows.Forms.DateTimePicker();
            this.BtnViewReport = new System.Windows.Forms.Button();
            this.btngenrpt = new System.Windows.Forms.Button();
            this.DTPFrom = new System.Windows.Forms.DateTimePicker();
            this.btnclose = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbaccname = new System.Windows.Forms.ComboBox();
            this.LVledger = new System.Windows.Forms.ListView();
            this.txtbalance = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.txttotcredit = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.txttotdebit = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtopbal = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.Highlight;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox7.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.ForeColor = System.Drawing.Color.White;
            this.textBox7.Location = new System.Drawing.Point(2, 0);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(1098, 31);
            this.textBox7.TabIndex = 172;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "LEDGER";
            // 
            // DTPTo
            // 
            this.DTPTo.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPTo.Location = new System.Drawing.Point(558, 64);
            this.DTPTo.Name = "DTPTo";
            this.DTPTo.Size = new System.Drawing.Size(149, 24);
            this.DTPTo.TabIndex = 2;
            this.DTPTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTPTo_KeyDown);
            // 
            // BtnViewReport
            // 
            this.BtnViewReport.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BtnViewReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnViewReport.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnViewReport.ForeColor = System.Drawing.Color.White;
            this.BtnViewReport.Location = new System.Drawing.Point(714, 59);
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
            this.btngenrpt.Location = new System.Drawing.Point(896, 60);
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
            // 
            // DTPFrom
            // 
            this.DTPFrom.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFrom.Location = new System.Drawing.Point(395, 65);
            this.DTPFrom.Name = "DTPFrom";
            this.DTPFrom.Size = new System.Drawing.Size(153, 24);
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
            this.btnclose.Location = new System.Drawing.Point(997, 60);
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
            this.Label2.Location = new System.Drawing.Point(595, 41);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(63, 16);
            this.Label2.TabIndex = 174;
            this.Label2.Text = "To Date";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(429, 42);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(84, 16);
            this.Label1.TabIndex = 173;
            this.Label1.Text = "From Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(165, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 16);
            this.label3.TabIndex = 180;
            this.label3.Text = "Account Name";
            // 
            // cmbaccname
            // 
            this.cmbaccname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbaccname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbaccname.BackColor = System.Drawing.SystemColors.Window;
            this.cmbaccname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbaccname.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbaccname.FormattingEnabled = true;
            this.cmbaccname.IntegralHeight = false;
            this.cmbaccname.Location = new System.Drawing.Point(21, 65);
            this.cmbaccname.Name = "cmbaccname";
            this.cmbaccname.Size = new System.Drawing.Size(365, 24);
            this.cmbaccname.TabIndex = 1;
            this.cmbaccname.SelectedIndexChanged += new System.EventHandler(this.cmbaccname_SelectedIndexChanged);
            this.cmbaccname.Enter += new System.EventHandler(this.cmbaccname_Enter);
            this.cmbaccname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbaccname_KeyDown);
            this.cmbaccname.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbaccname_KeyUp);
            this.cmbaccname.Leave += new System.EventHandler(this.cmbaccname_Leave);
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
            this.LVledger.Location = new System.Drawing.Point(12, 138);
            this.LVledger.MultiSelect = false;
            this.LVledger.Name = "LVledger";
            this.LVledger.Size = new System.Drawing.Size(1082, 326);
            this.LVledger.TabIndex = 6;
            this.LVledger.UseCompatibleStateImageBehavior = false;
            this.LVledger.View = System.Windows.Forms.View.Details;
            this.LVledger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LVledger_KeyDown);
            this.LVledger.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LVledger_MouseDoubleClick);
            // 
            // txtbalance
            // 
            this.txtbalance.BackColor = System.Drawing.SystemColors.Window;
            this.txtbalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbalance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtbalance.Location = new System.Drawing.Point(952, 487);
            this.txtbalance.Name = "txtbalance";
            this.txtbalance.Size = new System.Drawing.Size(139, 26);
            this.txtbalance.TabIndex = 188;
            this.txtbalance.TabStop = false;
            this.txtbalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.ForeColor = System.Drawing.Color.Blue;
            this.Label6.Location = new System.Drawing.Point(852, 471);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(69, 16);
            this.Label6.TabIndex = 184;
            this.Label6.Text = "Total Cr.";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.ForeColor = System.Drawing.Color.Blue;
            this.Label7.Location = new System.Drawing.Point(991, 471);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(66, 16);
            this.Label7.TabIndex = 185;
            this.Label7.Text = "Balance";
            // 
            // txttotcredit
            // 
            this.txttotcredit.BackColor = System.Drawing.SystemColors.Window;
            this.txttotcredit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txttotcredit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttotcredit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txttotcredit.Location = new System.Drawing.Point(811, 487);
            this.txttotcredit.Name = "txttotcredit";
            this.txttotcredit.Size = new System.Drawing.Size(141, 26);
            this.txttotcredit.TabIndex = 187;
            this.txttotcredit.TabStop = false;
            this.txttotcredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.ForeColor = System.Drawing.Color.Blue;
            this.Label5.Location = new System.Drawing.Point(712, 471);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(69, 16);
            this.Label5.TabIndex = 183;
            this.Label5.Text = "Total Dr.";
            // 
            // txttotdebit
            // 
            this.txttotdebit.BackColor = System.Drawing.SystemColors.Window;
            this.txttotdebit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txttotdebit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttotdebit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txttotdebit.Location = new System.Drawing.Point(664, 487);
            this.txttotdebit.Name = "txttotdebit";
            this.txttotdebit.Size = new System.Drawing.Size(147, 26);
            this.txttotdebit.TabIndex = 186;
            this.txttotdebit.TabStop = false;
            this.txttotdebit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(793, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 16);
            this.label4.TabIndex = 189;
            this.label4.Text = "Opening Balance";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // txtopbal
            // 
            this.txtopbal.BackColor = System.Drawing.SystemColors.Window;
            this.txtopbal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtopbal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtopbal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtopbal.Location = new System.Drawing.Point(924, 105);
            this.txtopbal.Name = "txtopbal";
            this.txtopbal.Size = new System.Drawing.Size(170, 26);
            this.txtopbal.TabIndex = 190;
            this.txtopbal.TabStop = false;
            this.txtopbal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtopbal.Enter += new System.EventHandler(this.txtopbal_Enter);
            this.txtopbal.Leave += new System.EventHandler(this.txtopbal_Leave);
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
            this.panel1.Location = new System.Drawing.Point(2, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1098, 489);
            this.panel1.TabIndex = 191;
            // 
            // Ledger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1109, 546);
            this.ControlBox = false;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtopbal);
            this.Controls.Add(this.txtbalance);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.txttotcredit);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.txttotdebit);
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
            this.Name = "Ledger";
            this.Text = "Ledger";
            this.Load += new System.EventHandler(this.Ledger_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox textBox7;
        internal System.Windows.Forms.DateTimePicker DTPTo;
        internal System.Windows.Forms.Button BtnViewReport;
        internal System.Windows.Forms.Button btngenrpt;
        internal System.Windows.Forms.DateTimePicker DTPFrom;
        internal System.Windows.Forms.Button btnclose;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbaccname;
        internal System.Windows.Forms.ListView LVledger;
        internal System.Windows.Forms.TextBox txtbalance;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.TextBox txttotcredit;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.TextBox txttotdebit;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox txtopbal;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
    }
}