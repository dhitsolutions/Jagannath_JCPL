namespace RamdevSales
{
    partial class ItemSaleChart
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
            this.DTPTo = new System.Windows.Forms.DateTimePicker();
            this.BtnViewReport = new System.Windows.Forms.Button();
            this.DTPFrom = new System.Windows.Forms.DateTimePicker();
            this.btngenrpt = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkitem = new System.Windows.Forms.CheckedListBox();
            this.chkacc = new System.Windows.Forms.CheckedListBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtitems = new System.Windows.Forms.TextBox();
            this.drpitems = new System.Windows.Forms.ComboBox();
            this.txtaccount = new System.Windows.Forms.TextBox();
            this.drpaccount = new System.Windows.Forms.ComboBox();
            this.grdview = new System.Windows.Forms.DataGridView();
            this.btnclose = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdview)).BeginInit();
            this.SuspendLayout();
            // 
            // DTPTo
            // 
            this.DTPTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPTo.Location = new System.Drawing.Point(173, 34);
            this.DTPTo.Name = "DTPTo";
            this.DTPTo.Size = new System.Drawing.Size(146, 24);
            this.DTPTo.TabIndex = 202;
            // 
            // BtnViewReport
            // 
            this.BtnViewReport.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BtnViewReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnViewReport.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnViewReport.ForeColor = System.Drawing.Color.White;
            this.BtnViewReport.Location = new System.Drawing.Point(329, 28);
            this.BtnViewReport.Name = "BtnViewReport";
            this.BtnViewReport.Size = new System.Drawing.Size(97, 34);
            this.BtnViewReport.TabIndex = 203;
            this.BtnViewReport.Text = "OK";
            this.BtnViewReport.UseVisualStyleBackColor = false;
            this.BtnViewReport.Click += new System.EventHandler(this.BtnViewReport_Click);
            // 
            // DTPFrom
            // 
            this.DTPFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFrom.Location = new System.Drawing.Point(14, 35);
            this.DTPFrom.Name = "DTPFrom";
            this.DTPFrom.Size = new System.Drawing.Size(150, 24);
            this.DTPFrom.TabIndex = 201;
            // 
            // btngenrpt
            // 
            this.btngenrpt.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btngenrpt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btngenrpt.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btngenrpt.ForeColor = System.Drawing.Color.White;
            this.btngenrpt.Location = new System.Drawing.Point(734, 29);
            this.btngenrpt.Name = "btngenrpt";
            this.btngenrpt.Size = new System.Drawing.Size(97, 34);
            this.btngenrpt.TabIndex = 204;
            this.btngenrpt.Text = "Print";
            this.btngenrpt.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.chkitem);
            this.panel1.Controls.Add(this.chkacc);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtitems);
            this.panel1.Controls.Add(this.drpitems);
            this.panel1.Controls.Add(this.txtaccount);
            this.panel1.Controls.Add(this.drpaccount);
            this.panel1.Controls.Add(this.grdview);
            this.panel1.Controls.Add(this.DTPTo);
            this.panel1.Controls.Add(this.BtnViewReport);
            this.panel1.Controls.Add(this.btngenrpt);
            this.panel1.Controls.Add(this.DTPFrom);
            this.panel1.Controls.Add(this.btnclose);
            this.panel1.Controls.Add(this.Label2);
            this.panel1.Controls.Add(this.Label1);
            this.panel1.Location = new System.Drawing.Point(3, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(944, 425);
            this.panel1.TabIndex = 202;
            // 
            // chkitem
            // 
            this.chkitem.FormattingEnabled = true;
            this.chkitem.Location = new System.Drawing.Point(602, 313);
            this.chkitem.Name = "chkitem";
            this.chkitem.Size = new System.Drawing.Size(228, 109);
            this.chkitem.TabIndex = 227;
            // 
            // chkacc
            // 
            this.chkacc.FormattingEnabled = true;
            this.chkacc.Location = new System.Drawing.Point(204, 313);
            this.chkacc.Name = "chkacc";
            this.chkacc.Size = new System.Drawing.Size(192, 109);
            this.chkacc.TabIndex = 226;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(835, 317);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(97, 34);
            this.btnSearch.TabIndex = 225;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtitems
            // 
            this.txtitems.BackColor = System.Drawing.Color.White;
            this.txtitems.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtitems.Location = new System.Drawing.Point(598, 325);
            this.txtitems.Name = "txtitems";
            this.txtitems.Size = new System.Drawing.Size(212, 23);
            this.txtitems.TabIndex = 224;
            this.txtitems.Visible = false;
            // 
            // drpitems
            // 
            this.drpitems.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.drpitems.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.drpitems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpitems.FormattingEnabled = true;
            this.drpitems.Location = new System.Drawing.Point(401, 325);
            this.drpitems.Name = "drpitems";
            this.drpitems.Size = new System.Drawing.Size(191, 21);
            this.drpitems.TabIndex = 223;
            this.drpitems.SelectedIndexChanged += new System.EventHandler(this.drpitems_SelectedIndexChanged);
            // 
            // txtaccount
            // 
            this.txtaccount.BackColor = System.Drawing.Color.White;
            this.txtaccount.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtaccount.Location = new System.Drawing.Point(202, 325);
            this.txtaccount.Name = "txtaccount";
            this.txtaccount.Size = new System.Drawing.Size(193, 23);
            this.txtaccount.TabIndex = 222;
            this.txtaccount.Visible = false;
            // 
            // drpaccount
            // 
            this.drpaccount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.drpaccount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.drpaccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpaccount.FormattingEnabled = true;
            this.drpaccount.Location = new System.Drawing.Point(25, 325);
            this.drpaccount.Name = "drpaccount";
            this.drpaccount.Size = new System.Drawing.Size(171, 21);
            this.drpaccount.TabIndex = 221;
            this.drpaccount.SelectedIndexChanged += new System.EventHandler(this.drpaccount_SelectedIndexChanged);
            // 
            // grdview
            // 
            this.grdview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdview.Location = new System.Drawing.Point(14, 68);
            this.grdview.Name = "grdview";
            this.grdview.Size = new System.Drawing.Size(919, 242);
            this.grdview.TabIndex = 210;
            this.grdview.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdview_CellContentClick);
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(837, 28);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(97, 34);
            this.btnclose.TabIndex = 205;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(210, 12);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(63, 16);
            this.Label2.TabIndex = 207;
            this.Label2.Text = "To Date";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(45, 12);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(84, 16);
            this.Label1.TabIndex = 206;
            this.Label1.Text = "From Date";
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.Highlight;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox7.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.ForeColor = System.Drawing.Color.White;
            this.textBox7.Location = new System.Drawing.Point(2, 1);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(945, 31);
            this.textBox7.TabIndex = 201;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "ITEM WISE SALE CHART";
            // 
            // ItemSaleChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 460);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ItemSaleChart";
            this.Text = "ItemSaleChart";
            this.Load += new System.EventHandler(this.ItemSaleChart_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.DateTimePicker DTPTo;
        internal System.Windows.Forms.Button BtnViewReport;
        internal System.Windows.Forms.DateTimePicker DTPFrom;
        internal System.Windows.Forms.Button btngenrpt;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Button btnclose;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.DataGridView grdview;
        internal System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtitems;
        private System.Windows.Forms.ComboBox drpitems;
        private System.Windows.Forms.TextBox txtaccount;
        private System.Windows.Forms.ComboBox drpaccount;
        private System.Windows.Forms.CheckedListBox chkitem;
        private System.Windows.Forms.CheckedListBox chkacc;

    }
}