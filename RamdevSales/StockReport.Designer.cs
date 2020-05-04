namespace RamdevSales
{
    partial class StockReport
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
            this.grdstock = new System.Windows.Forms.DataGridView();
            this.BtnPayment = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt9 = new System.Windows.Forms.TextBox();
            this.txt7 = new System.Windows.Forms.TextBox();
            this.txt8 = new System.Windows.Forms.TextBox();
            this.txt6 = new System.Windows.Forms.TextBox();
            this.txt5 = new System.Windows.Forms.TextBox();
            this.txt4 = new System.Windows.Forms.TextBox();
            this.txt3 = new System.Windows.Forms.TextBox();
            this.txt2 = new System.Windows.Forms.TextBox();
            this.txt1 = new System.Windows.Forms.TextBox();
            this.btnprint = new System.Windows.Forms.Button();
            this.btncancel = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtgroup = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdstock)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox7
            // 
            this.textBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox7.BackColor = System.Drawing.SystemColors.Highlight;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox7.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.ForeColor = System.Drawing.Color.White;
            this.textBox7.Location = new System.Drawing.Point(0, 0);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(1117, 31);
            this.textBox7.TabIndex = 248;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "STOCK MANAGEMENT";
            // 
            // grdstock
            // 
            this.grdstock.AllowUserToAddRows = false;
            this.grdstock.AllowUserToDeleteRows = false;
            this.grdstock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdstock.BackgroundColor = System.Drawing.SystemColors.Window;
            this.grdstock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdstock.Location = new System.Drawing.Point(12, 87);
            this.grdstock.Name = "grdstock";
            this.grdstock.Size = new System.Drawing.Size(1091, 391);
            this.grdstock.TabIndex = 243;
            this.grdstock.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdstock_KeyDown);
            this.grdstock.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.grdstock_MouseDoubleClick);
            // 
            // BtnPayment
            // 
            this.BtnPayment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPayment.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BtnPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPayment.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPayment.ForeColor = System.Drawing.Color.White;
            this.BtnPayment.Location = new System.Drawing.Point(801, 42);
            this.BtnPayment.Name = "BtnPayment";
            this.BtnPayment.Size = new System.Drawing.Size(97, 34);
            this.BtnPayment.TabIndex = 252;
            this.BtnPayment.Text = "&Excel";
            this.BtnPayment.UseVisualStyleBackColor = false;
            this.BtnPayment.Click += new System.EventHandler(this.BtnPayment_Click);
            this.BtnPayment.Enter += new System.EventHandler(this.BtnPayment_Enter);
            this.BtnPayment.Leave += new System.EventHandler(this.BtnPayment_Leave);
            this.BtnPayment.MouseEnter += new System.EventHandler(this.BtnPayment_MouseEnter);
            this.BtnPayment.MouseLeave += new System.EventHandler(this.BtnPayment_MouseLeave);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtgroup);
            this.panel1.Controls.Add(this.txt9);
            this.panel1.Controls.Add(this.txt7);
            this.panel1.Controls.Add(this.txt8);
            this.panel1.Controls.Add(this.txt6);
            this.panel1.Controls.Add(this.txt5);
            this.panel1.Controls.Add(this.txt4);
            this.panel1.Controls.Add(this.txt3);
            this.panel1.Controls.Add(this.txt2);
            this.panel1.Controls.Add(this.txt1);
            this.panel1.Controls.Add(this.btnprint);
            this.panel1.Controls.Add(this.btncancel);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Location = new System.Drawing.Point(1, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1112, 487);
            this.panel1.TabIndex = 253;
            // 
            // txt9
            // 
            this.txt9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt9.Location = new System.Drawing.Point(991, 452);
            this.txt9.Multiline = true;
            this.txt9.Name = "txt9";
            this.txt9.Size = new System.Drawing.Size(110, 22);
            this.txt9.TabIndex = 268;
            // 
            // txt7
            // 
            this.txt7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt7.Location = new System.Drawing.Point(770, 452);
            this.txt7.Multiline = true;
            this.txt7.Name = "txt7";
            this.txt7.Size = new System.Drawing.Size(110, 22);
            this.txt7.TabIndex = 267;
            // 
            // txt8
            // 
            this.txt8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt8.Location = new System.Drawing.Point(881, 452);
            this.txt8.Multiline = true;
            this.txt8.Name = "txt8";
            this.txt8.Size = new System.Drawing.Size(110, 22);
            this.txt8.TabIndex = 266;
            // 
            // txt6
            // 
            this.txt6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt6.Location = new System.Drawing.Point(659, 452);
            this.txt6.Multiline = true;
            this.txt6.Name = "txt6";
            this.txt6.Size = new System.Drawing.Size(110, 22);
            this.txt6.TabIndex = 265;
            // 
            // txt5
            // 
            this.txt5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt5.Location = new System.Drawing.Point(548, 452);
            this.txt5.Multiline = true;
            this.txt5.Name = "txt5";
            this.txt5.Size = new System.Drawing.Size(110, 22);
            this.txt5.TabIndex = 264;
            // 
            // txt4
            // 
            this.txt4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt4.Location = new System.Drawing.Point(437, 452);
            this.txt4.Multiline = true;
            this.txt4.Name = "txt4";
            this.txt4.Size = new System.Drawing.Size(110, 22);
            this.txt4.TabIndex = 263;
            // 
            // txt3
            // 
            this.txt3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt3.Location = new System.Drawing.Point(326, 452);
            this.txt3.Multiline = true;
            this.txt3.Name = "txt3";
            this.txt3.Size = new System.Drawing.Size(110, 22);
            this.txt3.TabIndex = 262;
            // 
            // txt2
            // 
            this.txt2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt2.Location = new System.Drawing.Point(215, 452);
            this.txt2.Multiline = true;
            this.txt2.Name = "txt2";
            this.txt2.Size = new System.Drawing.Size(110, 22);
            this.txt2.TabIndex = 261;
            // 
            // txt1
            // 
            this.txt1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt1.Location = new System.Drawing.Point(104, 452);
            this.txt1.Multiline = true;
            this.txt1.Name = "txt1";
            this.txt1.Size = new System.Drawing.Size(110, 22);
            this.txt1.TabIndex = 260;
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprint.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(903, 10);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(97, 34);
            this.btnprint.TabIndex = 259;
            this.btnprint.Text = "&Print";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            this.btnprint.Enter += new System.EventHandler(this.btnprint_Enter);
            this.btnprint.Leave += new System.EventHandler(this.btnprint_Leave);
            this.btnprint.MouseEnter += new System.EventHandler(this.btnprint_MouseEnter);
            this.btnprint.MouseLeave += new System.EventHandler(this.btnprint_MouseLeave);
            // 
            // btncancel
            // 
            this.btncancel.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btncancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncancel.ForeColor = System.Drawing.Color.White;
            this.btncancel.Location = new System.Drawing.Point(1004, 10);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(97, 34);
            this.btncancel.TabIndex = 257;
            this.btncancel.Text = "&Close";
            this.btncancel.UseVisualStyleBackColor = false;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            this.btncancel.Enter += new System.EventHandler(this.btncancel_Enter);
            this.btncancel.Leave += new System.EventHandler(this.btncancel_Leave);
            this.btncancel.MouseEnter += new System.EventHandler(this.btncancel_MouseEnter);
            this.btncancel.MouseLeave += new System.EventHandler(this.btncancel_MouseLeave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(497, 234);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(117, 16);
            this.label11.TabIndex = 256;
            this.label11.Text = "Total Op. Stock";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(10, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 17);
            this.label1.TabIndex = 271;
            this.label1.Text = "Group Name";
            // 
            // txtgroup
            // 
            this.txtgroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtgroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtgroup.BackColor = System.Drawing.SystemColors.Window;
            this.txtgroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtgroup.FormattingEnabled = true;
            this.txtgroup.Location = new System.Drawing.Point(10, 27);
            this.txtgroup.Name = "txtgroup";
            this.txtgroup.Size = new System.Drawing.Size(247, 21);
            this.txtgroup.TabIndex = 270;
            this.txtgroup.SelectedIndexChanged += new System.EventHandler(this.txtgroup_SelectedIndexChanged);
            // 
            // StockReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1115, 546);
            this.ControlBox = false;
            this.Controls.Add(this.BtnPayment);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.grdstock);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StockReport";
            this.Text = "StockReport";
            this.Load += new System.EventHandler(this.StockReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdstock)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.DataGridView grdstock;
        internal System.Windows.Forms.Button BtnPayment;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btncancel;
        internal System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.TextBox txt7;
        private System.Windows.Forms.TextBox txt8;
        private System.Windows.Forms.TextBox txt6;
        private System.Windows.Forms.TextBox txt5;
        private System.Windows.Forms.TextBox txt4;
        private System.Windows.Forms.TextBox txt3;
        private System.Windows.Forms.TextBox txt2;
        private System.Windows.Forms.TextBox txt1;
        private System.Windows.Forms.TextBox txt9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox txtgroup;
    }
}