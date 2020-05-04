namespace RamdevSales
{
    partial class itemwisesaleorder2
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
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdview = new System.Windows.Forms.DataGridView();
            this.btnPrint = new System.Windows.Forms.Button();
            this.BtnViewReport = new System.Windows.Forms.Button();
            this.DTPTo = new System.Windows.Forms.DateTimePicker();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.btngenrpt = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.DTPFrom = new System.Windows.Forms.DateTimePicker();
            this.chkitem = new System.Windows.Forms.CheckedListBox();
            this.chkacc = new System.Windows.Forms.CheckedListBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.drpitems = new System.Windows.Forms.ComboBox();
            this.drpaccount = new System.Windows.Forms.ComboBox();
            this.txtitems = new System.Windows.Forms.TextBox();
            this.txtaccount = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdview)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.Highlight;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox7.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.ForeColor = System.Drawing.Color.White;
            this.textBox7.Location = new System.Drawing.Point(1, 0);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(945, 31);
            this.textBox7.TabIndex = 204;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "ITEM WISE SALE ORDER LIST";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.txtaccount);
            this.panel1.Controls.Add(this.txtitems);
            this.panel1.Controls.Add(this.chkitem);
            this.panel1.Controls.Add(this.chkacc);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.drpitems);
            this.panel1.Controls.Add(this.drpaccount);
            this.panel1.Controls.Add(this.grdview);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.BtnViewReport);
            this.panel1.Controls.Add(this.DTPTo);
            this.panel1.Controls.Add(this.Label1);
            this.panel1.Controls.Add(this.Label2);
            this.panel1.Controls.Add(this.btngenrpt);
            this.panel1.Controls.Add(this.btnclose);
            this.panel1.Controls.Add(this.DTPFrom);
            this.panel1.Location = new System.Drawing.Point(1, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(945, 504);
            this.panel1.TabIndex = 205;
            // 
            // grdview
            // 
            this.grdview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdview.Location = new System.Drawing.Point(11, 77);
            this.grdview.Name = "grdview";
            this.grdview.ReadOnly = true;
            this.grdview.Size = new System.Drawing.Size(919, 312);
            this.grdview.TabIndex = 232;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(630, 40);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(97, 34);
            this.btnPrint.TabIndex = 231;
            this.btnPrint.Text = "PDF";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // BtnViewReport
            // 
            this.BtnViewReport.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BtnViewReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnViewReport.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnViewReport.ForeColor = System.Drawing.Color.White;
            this.BtnViewReport.Location = new System.Drawing.Point(328, 39);
            this.BtnViewReport.Name = "BtnViewReport";
            this.BtnViewReport.Size = new System.Drawing.Size(97, 34);
            this.BtnViewReport.TabIndex = 226;
            this.BtnViewReport.Text = "OK";
            this.BtnViewReport.UseVisualStyleBackColor = false;
            this.BtnViewReport.Click += new System.EventHandler(this.BtnViewReport_Click);
            // 
            // DTPTo
            // 
            this.DTPTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPTo.Location = new System.Drawing.Point(172, 45);
            this.DTPTo.Name = "DTPTo";
            this.DTPTo.Size = new System.Drawing.Size(146, 24);
            this.DTPTo.TabIndex = 225;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(44, 23);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(84, 16);
            this.Label1.TabIndex = 229;
            this.Label1.Text = "From Date";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(209, 23);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(63, 16);
            this.Label2.TabIndex = 230;
            this.Label2.Text = "To Date";
            // 
            // btngenrpt
            // 
            this.btngenrpt.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btngenrpt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btngenrpt.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btngenrpt.ForeColor = System.Drawing.Color.White;
            this.btngenrpt.Location = new System.Drawing.Point(733, 40);
            this.btngenrpt.Name = "btngenrpt";
            this.btngenrpt.Size = new System.Drawing.Size(97, 34);
            this.btngenrpt.TabIndex = 227;
            this.btngenrpt.Text = "Print";
            this.btngenrpt.UseVisualStyleBackColor = false;
            this.btngenrpt.Click += new System.EventHandler(this.btngenrpt_Click);
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(836, 39);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(97, 34);
            this.btnclose.TabIndex = 228;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // DTPFrom
            // 
            this.DTPFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFrom.Location = new System.Drawing.Point(13, 46);
            this.DTPFrom.Name = "DTPFrom";
            this.DTPFrom.Size = new System.Drawing.Size(150, 24);
            this.DTPFrom.TabIndex = 224;
            // 
            // chkitem
            // 
            this.chkitem.FormattingEnabled = true;
            this.chkitem.Location = new System.Drawing.Point(589, 403);
            this.chkitem.Name = "chkitem";
            this.chkitem.Size = new System.Drawing.Size(228, 94);
            this.chkitem.TabIndex = 237;
            // 
            // chkacc
            // 
            this.chkacc.FormattingEnabled = true;
            this.chkacc.Location = new System.Drawing.Point(191, 403);
            this.chkacc.Name = "chkacc";
            this.chkacc.Size = new System.Drawing.Size(192, 94);
            this.chkacc.TabIndex = 236;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(823, 395);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(97, 34);
            this.btnSearch.TabIndex = 235;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // drpitems
            // 
            this.drpitems.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.drpitems.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.drpitems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpitems.FormattingEnabled = true;
            this.drpitems.Location = new System.Drawing.Point(389, 403);
            this.drpitems.Name = "drpitems";
            this.drpitems.Size = new System.Drawing.Size(191, 21);
            this.drpitems.TabIndex = 234;
            this.drpitems.SelectedIndexChanged += new System.EventHandler(this.drpitems_SelectedIndexChanged);
            // 
            // drpaccount
            // 
            this.drpaccount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.drpaccount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.drpaccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpaccount.FormattingEnabled = true;
            this.drpaccount.Location = new System.Drawing.Point(13, 403);
            this.drpaccount.Name = "drpaccount";
            this.drpaccount.Size = new System.Drawing.Size(171, 21);
            this.drpaccount.TabIndex = 233;
            this.drpaccount.SelectedIndexChanged += new System.EventHandler(this.drpaccount_SelectedIndexChanged);
            // 
            // txtitems
            // 
            this.txtitems.Location = new System.Drawing.Point(430, 457);
            this.txtitems.Name = "txtitems";
            this.txtitems.Size = new System.Drawing.Size(100, 20);
            this.txtitems.TabIndex = 238;
            this.txtitems.Visible = false;
            // 
            // txtaccount
            // 
            this.txtaccount.Location = new System.Drawing.Point(430, 431);
            this.txtaccount.Name = "txtaccount";
            this.txtaccount.Size = new System.Drawing.Size(100, 20);
            this.txtaccount.TabIndex = 239;
            this.txtaccount.Visible = false;
            // 
            // itemwisesaleorder2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 539);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "itemwisesaleorder2";
            this.Text = "itemwisesaleorder2";
            this.Load += new System.EventHandler(this.itemwisesaleorder2_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Button btnPrint;
        internal System.Windows.Forms.Button BtnViewReport;
        internal System.Windows.Forms.DateTimePicker DTPTo;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Button btngenrpt;
        internal System.Windows.Forms.Button btnclose;
        internal System.Windows.Forms.DateTimePicker DTPFrom;
        private System.Windows.Forms.DataGridView grdview;
        private System.Windows.Forms.CheckedListBox chkitem;
        private System.Windows.Forms.CheckedListBox chkacc;
        internal System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox drpitems;
        private System.Windows.Forms.ComboBox drpaccount;
        private System.Windows.Forms.TextBox txtitems;
        private System.Windows.Forms.TextBox txtaccount;
    }
}