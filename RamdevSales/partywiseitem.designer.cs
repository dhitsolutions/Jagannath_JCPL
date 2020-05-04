namespace RamdevSales
{
    partial class partywiseitem
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
            this.cmbselectitemrate = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cmbFilter = new System.Windows.Forms.ComboBox();
            this.grdview = new System.Windows.Forms.DataGridView();
            this.DTPTo = new System.Windows.Forms.DateTimePicker();
            this.BtnViewReport = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.btngenrpt = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.DTPFrom = new System.Windows.Forms.DateTimePicker();
            this.btnclose = new System.Windows.Forms.Button();
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
            this.textBox7.Location = new System.Drawing.Point(1, 2);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(892, 31);
            this.textBox7.TabIndex = 202;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "PARTY WISE ITEM";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.cmbselectitemrate);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.cmbFilter);
            this.panel1.Controls.Add(this.grdview);
            this.panel1.Controls.Add(this.DTPTo);
            this.panel1.Controls.Add(this.BtnViewReport);
            this.panel1.Controls.Add(this.Label1);
            this.panel1.Controls.Add(this.btngenrpt);
            this.panel1.Controls.Add(this.Label2);
            this.panel1.Controls.Add(this.DTPFrom);
            this.panel1.Controls.Add(this.btnclose);
            this.panel1.Location = new System.Drawing.Point(1, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(892, 441);
            this.panel1.TabIndex = 0;
            // 
            // cmbselectitemrate
            // 
            this.cmbselectitemrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbselectitemrate.FormattingEnabled = true;
            this.cmbselectitemrate.Items.AddRange(new object[] {
            "Basic Price",
            "MRP",
            "Sale Price",
            "Purchase Price"});
            this.cmbselectitemrate.Location = new System.Drawing.Point(339, 24);
            this.cmbselectitemrate.Name = "cmbselectitemrate";
            this.cmbselectitemrate.Size = new System.Drawing.Size(126, 21);
            this.cmbselectitemrate.TabIndex = 219;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(336, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 16);
            this.label3.TabIndex = 218;
            this.label3.Text = "Select Item Rate";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(216, 404);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(153, 20);
            this.txtSearch.TabIndex = 217;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(379, 400);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(97, 34);
            this.btnSearch.TabIndex = 216;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cmbFilter
            // 
            this.cmbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilter.FormattingEnabled = true;
            this.cmbFilter.Items.AddRange(new object[] {
            "AccountName",
            "ItemName",
            "GroupName",
            "companyname"});
            this.cmbFilter.Location = new System.Drawing.Point(29, 404);
            this.cmbFilter.Name = "cmbFilter";
            this.cmbFilter.Size = new System.Drawing.Size(180, 21);
            this.cmbFilter.TabIndex = 215;
            // 
            // grdview
            // 
            this.grdview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdview.Location = new System.Drawing.Point(6, 60);
            this.grdview.Name = "grdview";
            this.grdview.Size = new System.Drawing.Size(877, 337);
            this.grdview.TabIndex = 5;
            // 
            // DTPTo
            // 
            this.DTPTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPTo.Location = new System.Drawing.Point(166, 24);
            this.DTPTo.Name = "DTPTo";
            this.DTPTo.Size = new System.Drawing.Size(146, 24);
            this.DTPTo.TabIndex = 1;
            this.DTPTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTPTo_KeyDown);
            // 
            // BtnViewReport
            // 
            this.BtnViewReport.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BtnViewReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnViewReport.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnViewReport.ForeColor = System.Drawing.Color.White;
            this.BtnViewReport.Location = new System.Drawing.Point(569, 19);
            this.BtnViewReport.Name = "BtnViewReport";
            this.BtnViewReport.Size = new System.Drawing.Size(97, 34);
            this.BtnViewReport.TabIndex = 2;
            this.BtnViewReport.Text = "OK";
            this.BtnViewReport.UseVisualStyleBackColor = false;
            this.BtnViewReport.Click += new System.EventHandler(this.BtnViewReport_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(37, 6);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(84, 16);
            this.Label1.TabIndex = 213;
            this.Label1.Text = "From Date";
            // 
            // btngenrpt
            // 
            this.btngenrpt.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btngenrpt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btngenrpt.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btngenrpt.ForeColor = System.Drawing.Color.White;
            this.btngenrpt.Location = new System.Drawing.Point(667, 19);
            this.btngenrpt.Name = "btngenrpt";
            this.btngenrpt.Size = new System.Drawing.Size(118, 34);
            this.btngenrpt.TabIndex = 3;
            this.btngenrpt.Text = "Excel all Data";
            this.btngenrpt.UseVisualStyleBackColor = false;
            this.btngenrpt.Click += new System.EventHandler(this.btngenrpt_Click);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(202, 6);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(63, 16);
            this.Label2.TabIndex = 214;
            this.Label2.Text = "To Date";
            // 
            // DTPFrom
            // 
            this.DTPFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFrom.Location = new System.Drawing.Point(7, 25);
            this.DTPFrom.Name = "DTPFrom";
            this.DTPFrom.Size = new System.Drawing.Size(150, 24);
            this.DTPFrom.TabIndex = 0;
            this.DTPFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTPFrom_KeyDown);
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(786, 19);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(97, 34);
            this.btnclose.TabIndex = 4;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // partywiseitem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 477);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "partywiseitem";
            this.Text = "partywiseitem";
            this.Load += new System.EventHandler(this.partywiseitem_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.DateTimePicker DTPTo;
        internal System.Windows.Forms.Button BtnViewReport;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button btngenrpt;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.DateTimePicker DTPFrom;
        internal System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.DataGridView grdview;
        private System.Windows.Forms.ComboBox cmbFilter;
        internal System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cmbselectitemrate;
        internal System.Windows.Forms.Label label3;
    }
}