namespace RamdevSales
{
    partial class PrintBarcode
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
            this.cmbbarcode = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.DTPTo = new System.Windows.Forms.DateTimePicker();
            this.BtnViewReport = new System.Windows.Forms.Button();
            this.btnprint1 = new System.Windows.Forms.Button();
            this.DTPFrom = new System.Windows.Forms.DateTimePicker();
            this.btnclose = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.btnprint2 = new System.Windows.Forms.Button();
            this.btnprint3 = new System.Windows.Forms.Button();
            this.LVledger = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtitems = new System.Windows.Forms.TextBox();
            this.drpitems = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbbarcode
            // 
            this.cmbbarcode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbbarcode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbbarcode.BackColor = System.Drawing.SystemColors.Window;
            this.cmbbarcode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbarcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbbarcode.FormattingEnabled = true;
            this.cmbbarcode.Items.AddRange(new object[] {
            "All",
            "Purchased Item",
            "Opening Stock"});
            this.cmbbarcode.Location = new System.Drawing.Point(22, 76);
            this.cmbbarcode.Name = "cmbbarcode";
            this.cmbbarcode.Size = new System.Drawing.Size(268, 24);
            this.cmbbarcode.TabIndex = 0;
            this.cmbbarcode.SelectedIndexChanged += new System.EventHandler(this.cmbbarcode_SelectedIndexChanged);
            this.cmbbarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbbarcode_KeyDown);
            this.cmbbarcode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbbarcode_KeyUp);
            this.cmbbarcode.Leave += new System.EventHandler(this.cmbbarcode_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(61, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(174, 16);
            this.label3.TabIndex = 189;
            this.label3.Text = "Select Barcode to Print";
            // 
            // DTPTo
            // 
            this.DTPTo.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPTo.Location = new System.Drawing.Point(449, 76);
            this.DTPTo.Name = "DTPTo";
            this.DTPTo.Size = new System.Drawing.Size(140, 24);
            this.DTPTo.TabIndex = 2;
            this.DTPTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTPTo_KeyDown);
            // 
            // BtnViewReport
            // 
            this.BtnViewReport.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BtnViewReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnViewReport.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnViewReport.ForeColor = System.Drawing.Color.White;
            this.BtnViewReport.Location = new System.Drawing.Point(600, 70);
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
            // btnprint1
            // 
            this.btnprint1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnprint1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprint1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint1.ForeColor = System.Drawing.Color.White;
            this.btnprint1.Location = new System.Drawing.Point(702, 70);
            this.btnprint1.Name = "btnprint1";
            this.btnprint1.Size = new System.Drawing.Size(97, 34);
            this.btnprint1.TabIndex = 4;
            this.btnprint1.Text = "Print1";
            this.btnprint1.UseVisualStyleBackColor = false;
            this.btnprint1.Click += new System.EventHandler(this.btnprint1_Click);
            this.btnprint1.Enter += new System.EventHandler(this.btnprint1_Enter);
            this.btnprint1.Leave += new System.EventHandler(this.btnprint1_Leave);
            this.btnprint1.MouseEnter += new System.EventHandler(this.btnprint1_MouseEnter);
            this.btnprint1.MouseLeave += new System.EventHandler(this.btnprint1_MouseLeave);
            // 
            // DTPFrom
            // 
            this.DTPFrom.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFrom.Location = new System.Drawing.Point(299, 76);
            this.DTPFrom.Name = "DTPFrom";
            this.DTPFrom.Size = new System.Drawing.Size(140, 24);
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
            this.btnclose.Location = new System.Drawing.Point(1011, 70);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(97, 34);
            this.btnclose.TabIndex = 7;
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
            this.Label2.Location = new System.Drawing.Point(489, 53);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(63, 16);
            this.Label2.TabIndex = 188;
            this.Label2.Text = "To Date";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(326, 53);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(84, 16);
            this.Label1.TabIndex = 187;
            this.Label1.Text = "From Date";
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.Highlight;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox7.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.ForeColor = System.Drawing.Color.White;
            this.textBox7.Location = new System.Drawing.Point(7, 9);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(1110, 31);
            this.textBox7.TabIndex = 190;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "PRINT BARCODE LABELS";
            // 
            // btnprint2
            // 
            this.btnprint2.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnprint2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprint2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint2.ForeColor = System.Drawing.Color.White;
            this.btnprint2.Location = new System.Drawing.Point(805, 70);
            this.btnprint2.Name = "btnprint2";
            this.btnprint2.Size = new System.Drawing.Size(97, 34);
            this.btnprint2.TabIndex = 5;
            this.btnprint2.Text = "Print2";
            this.btnprint2.UseVisualStyleBackColor = false;
            this.btnprint2.Click += new System.EventHandler(this.btnprint2_Click);
            this.btnprint2.Enter += new System.EventHandler(this.btnprint2_Enter);
            this.btnprint2.Leave += new System.EventHandler(this.btnprint2_Leave);
            this.btnprint2.MouseEnter += new System.EventHandler(this.btnprint2_MouseEnter);
            this.btnprint2.MouseLeave += new System.EventHandler(this.btnprint2_MouseLeave);
            // 
            // btnprint3
            // 
            this.btnprint3.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnprint3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprint3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint3.ForeColor = System.Drawing.Color.White;
            this.btnprint3.Location = new System.Drawing.Point(908, 70);
            this.btnprint3.Name = "btnprint3";
            this.btnprint3.Size = new System.Drawing.Size(97, 34);
            this.btnprint3.TabIndex = 6;
            this.btnprint3.Text = "Print3";
            this.btnprint3.UseVisualStyleBackColor = false;
            this.btnprint3.Click += new System.EventHandler(this.btnprint3_Click);
            this.btnprint3.Enter += new System.EventHandler(this.btnprint3_Enter);
            this.btnprint3.Leave += new System.EventHandler(this.btnprint3_Leave);
            this.btnprint3.MouseEnter += new System.EventHandler(this.btnprint3_MouseEnter);
            this.btnprint3.MouseLeave += new System.EventHandler(this.btnprint3_MouseLeave);
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
            this.LVledger.Location = new System.Drawing.Point(12, 120);
            this.LVledger.MultiSelect = false;
            this.LVledger.Name = "LVledger";
            this.LVledger.Size = new System.Drawing.Size(1096, 349);
            this.LVledger.TabIndex = 8;
            this.LVledger.UseCompatibleStateImageBehavior = false;
            this.LVledger.View = System.Windows.Forms.View.Details;
            this.LVledger.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LVledger_MouseDoubleClick);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.drpitems);
            this.panel1.Controls.Add(this.txtitems);
            this.panel1.Location = new System.Drawing.Point(7, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1110, 468);
            this.panel1.TabIndex = 194;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(419, 430);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(97, 34);
            this.btnSearch.TabIndex = 218;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtitems
            // 
            this.txtitems.BackColor = System.Drawing.Color.White;
            this.txtitems.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtitems.Location = new System.Drawing.Point(201, 438);
            this.txtitems.Name = "txtitems";
            this.txtitems.Size = new System.Drawing.Size(212, 23);
            this.txtitems.TabIndex = 217;
            // 
            // drpitems
            // 
            this.drpitems.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.drpitems.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.drpitems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpitems.FormattingEnabled = true;
            this.drpitems.Location = new System.Drawing.Point(4, 438);
            this.drpitems.Name = "drpitems";
            this.drpitems.Size = new System.Drawing.Size(191, 21);
            this.drpitems.TabIndex = 216;
            // 
            // PrintBarcode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1123, 514);
            this.ControlBox = false;
            this.Controls.Add(this.LVledger);
            this.Controls.Add(this.btnprint3);
            this.Controls.Add(this.btnprint2);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.cmbbarcode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DTPTo);
            this.Controls.Add(this.BtnViewReport);
            this.Controls.Add(this.btnprint1);
            this.Controls.Add(this.DTPFrom);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PrintBarcode";
            this.Text = "PrintBarcode";
            this.Load += new System.EventHandler(this.PrintBarcode_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbbarcode;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.DateTimePicker DTPTo;
        internal System.Windows.Forms.Button BtnViewReport;
        internal System.Windows.Forms.Button btnprint1;
        internal System.Windows.Forms.DateTimePicker DTPFrom;
        internal System.Windows.Forms.Button btnclose;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox textBox7;
        internal System.Windows.Forms.Button btnprint2;
        internal System.Windows.Forms.Button btnprint3;
        internal System.Windows.Forms.ListView LVledger;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer1;
        internal System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox drpitems;
        private System.Windows.Forms.TextBox txtitems;
    }
}