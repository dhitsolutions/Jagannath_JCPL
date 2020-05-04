namespace RamdevSales
{
    partial class CustomerAndSupplierList
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cmbcors = new System.Windows.Forms.ComboBox();
            this.BtnViewReport = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.btnprint = new System.Windows.Forms.Button();
            this.LVcs = new System.Windows.Forms.ListView();
            this.panel1.SuspendLayout();
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
            this.textBox7.Size = new System.Drawing.Size(960, 31);
            this.textBox7.TabIndex = 221;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "CUSTOMER & SUPPLIER LIST";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.LVcs);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.btnclose);
            this.panel1.Controls.Add(this.cmbcors);
            this.panel1.Controls.Add(this.btnprint);
            this.panel1.Controls.Add(this.BtnViewReport);
            this.panel1.Location = new System.Drawing.Point(1, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(960, 472);
            this.panel1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(12, 7);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(216, 16);
            this.textBox1.TabIndex = 233;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "Select Customer Or Supplier";
            // 
            // cmbcors
            // 
            this.cmbcors.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcors.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcors.BackColor = System.Drawing.SystemColors.Window;
            this.cmbcors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbcors.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmbcors.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcors.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmbcors.FormattingEnabled = true;
            this.cmbcors.Items.AddRange(new object[] {
            "CUSTOMERS",
            "SUPPLIERS"});
            this.cmbcors.Location = new System.Drawing.Point(12, 28);
            this.cmbcors.Name = "cmbcors";
            this.cmbcors.Size = new System.Drawing.Size(216, 24);
            this.cmbcors.TabIndex = 0;
            // 
            // BtnViewReport
            // 
            this.BtnViewReport.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BtnViewReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnViewReport.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnViewReport.ForeColor = System.Drawing.Color.White;
            this.BtnViewReport.Location = new System.Drawing.Point(650, 22);
            this.BtnViewReport.Name = "BtnViewReport";
            this.BtnViewReport.Size = new System.Drawing.Size(97, 34);
            this.BtnViewReport.TabIndex = 1;
            this.BtnViewReport.Text = "&OK";
            this.BtnViewReport.UseVisualStyleBackColor = false;
            this.BtnViewReport.Click += new System.EventHandler(this.BtnViewReport_Click);
            this.BtnViewReport.Enter += new System.EventHandler(this.BtnViewReport_Enter);
            this.BtnViewReport.Leave += new System.EventHandler(this.BtnViewReport_Leave);
            this.BtnViewReport.MouseEnter += new System.EventHandler(this.BtnViewReport_MouseEnter);
            this.BtnViewReport.MouseLeave += new System.EventHandler(this.BtnViewReport_MouseLeave);
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(852, 22);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(97, 34);
            this.btnclose.TabIndex = 3;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            this.btnclose.Enter += new System.EventHandler(this.btnclose_Enter);
            this.btnclose.Leave += new System.EventHandler(this.btnclose_Leave);
            this.btnclose.MouseEnter += new System.EventHandler(this.btnclose_MouseEnter);
            this.btnclose.MouseLeave += new System.EventHandler(this.btnclose_MouseLeave);
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprint.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(753, 23);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(97, 34);
            this.btnprint.TabIndex = 2;
            this.btnprint.Text = "&Excel";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            this.btnprint.Enter += new System.EventHandler(this.btnprint_Enter);
            this.btnprint.Leave += new System.EventHandler(this.btnprint_Leave);
            this.btnprint.MouseEnter += new System.EventHandler(this.btnprint_MouseEnter);
            this.btnprint.MouseLeave += new System.EventHandler(this.btnprint_MouseLeave);
            // 
            // LVcs
            // 
            this.LVcs.BackColor = System.Drawing.SystemColors.Window;
            this.LVcs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LVcs.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LVcs.ForeColor = System.Drawing.Color.Navy;
            this.LVcs.FullRowSelect = true;
            this.LVcs.GridLines = true;
            this.LVcs.HideSelection = false;
            this.LVcs.Location = new System.Drawing.Point(3, 59);
            this.LVcs.MultiSelect = false;
            this.LVcs.Name = "LVcs";
            this.LVcs.Size = new System.Drawing.Size(954, 410);
            this.LVcs.TabIndex = 4;
            this.LVcs.UseCompatibleStateImageBehavior = false;
            this.LVcs.View = System.Windows.Forms.View.Details;
            // 
            // CustomerAndSupplierList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 504);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CustomerAndSupplierList";
            this.Text = "CustomerAndSupplierList";
            this.Load += new System.EventHandler(this.CustomerAndSupplierList_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox cmbcors;
        internal System.Windows.Forms.Button BtnViewReport;
        internal System.Windows.Forms.Button btnclose;
        internal System.Windows.Forms.Button btnprint;
        internal System.Windows.Forms.ListView LVcs;
    }
}