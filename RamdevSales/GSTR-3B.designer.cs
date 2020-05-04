namespace RamdevSales
{
    partial class GSTR_3B
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
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lv4 = new System.Windows.Forms.ListView();
            this.lv3 = new System.Windows.Forms.ListView();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.chkb2b = new System.Windows.Forms.CheckBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.BtnViewReport = new System.Windows.Forms.Button();
            this.DTPFrom = new System.Windows.Forms.DateTimePicker();
            this.btnexcel = new System.Windows.Forms.Button();
            this.DTPTo = new System.Windows.Forms.DateTimePicker();
            this.btnclose = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.lv1 = new System.Windows.Forms.ListView();
            this.lv2 = new System.Windows.Forms.ListView();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox14
            // 
            this.textBox14.BackColor = System.Drawing.SystemColors.Highlight;
            this.textBox14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox14.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox14.ForeColor = System.Drawing.Color.White;
            this.textBox14.Location = new System.Drawing.Point(1, 2);
            this.textBox14.Multiline = true;
            this.textBox14.Name = "textBox14";
            this.textBox14.ReadOnly = true;
            this.textBox14.Size = new System.Drawing.Size(1032, 30);
            this.textBox14.TabIndex = 205;
            this.textBox14.TabStop = false;
            this.textBox14.Text = "GSTR-3B";
            this.textBox14.TextChanged += new System.EventHandler(this.textBox14_TextChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lv4);
            this.panel1.Controls.Add(this.lv3);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.chkb2b);
            this.panel1.Controls.Add(this.Label1);
            this.panel1.Controls.Add(this.BtnViewReport);
            this.panel1.Controls.Add(this.DTPFrom);
            this.panel1.Controls.Add(this.btnexcel);
            this.panel1.Controls.Add(this.DTPTo);
            this.panel1.Controls.Add(this.btnclose);
            this.panel1.Controls.Add(this.Label2);
            this.panel1.Location = new System.Drawing.Point(2, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1031, 558);
            this.panel1.TabIndex = 206;
            // 
            // lv4
            // 
            this.lv4.BackColor = System.Drawing.SystemColors.Window;
            this.lv4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lv4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lv4.ForeColor = System.Drawing.Color.Navy;
            this.lv4.FullRowSelect = true;
            this.lv4.GridLines = true;
            this.lv4.HideSelection = false;
            this.lv4.Location = new System.Drawing.Point(4, 483);
            this.lv4.MultiSelect = false;
            this.lv4.Name = "lv4";
            this.lv4.Size = new System.Drawing.Size(1022, 66);
            this.lv4.TabIndex = 231;
            this.lv4.UseCompatibleStateImageBehavior = false;
            this.lv4.View = System.Windows.Forms.View.Details;
            // 
            // lv3
            // 
            this.lv3.BackColor = System.Drawing.SystemColors.Window;
            this.lv3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lv3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lv3.ForeColor = System.Drawing.Color.Navy;
            this.lv3.FullRowSelect = true;
            this.lv3.GridLines = true;
            this.lv3.HideSelection = false;
            this.lv3.Location = new System.Drawing.Point(4, 347);
            this.lv3.MultiSelect = false;
            this.lv3.Name = "lv3";
            this.lv3.Size = new System.Drawing.Size(1022, 128);
            this.lv3.TabIndex = 230;
            this.lv3.UseCompatibleStateImageBehavior = false;
            this.lv3.View = System.Windows.Forms.View.Details;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(430, 11);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(90, 30);
            this.checkBox1.TabIndex = 229;
            this.checkBox1.Text = "Consider B2C\r\nSale Return";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // chkb2b
            // 
            this.chkb2b.AutoSize = true;
            this.chkb2b.Checked = true;
            this.chkb2b.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkb2b.Location = new System.Drawing.Point(322, 12);
            this.chkb2b.Name = "chkb2b";
            this.chkb2b.Size = new System.Drawing.Size(90, 30);
            this.chkb2b.TabIndex = 228;
            this.chkb2b.Text = "Consider B2B\r\nSale Return";
            this.chkb2b.UseVisualStyleBackColor = true;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(34, 4);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(84, 16);
            this.Label1.TabIndex = 226;
            this.Label1.Text = "From Date";
            // 
            // BtnViewReport
            // 
            this.BtnViewReport.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BtnViewReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnViewReport.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnViewReport.ForeColor = System.Drawing.Color.White;
            this.BtnViewReport.Location = new System.Drawing.Point(733, 10);
            this.BtnViewReport.Name = "BtnViewReport";
            this.BtnViewReport.Size = new System.Drawing.Size(97, 34);
            this.BtnViewReport.TabIndex = 223;
            this.BtnViewReport.Text = "&OK";
            this.BtnViewReport.UseVisualStyleBackColor = false;
            this.BtnViewReport.Click += new System.EventHandler(this.BtnViewReport_Click);
            // 
            // DTPFrom
            // 
            this.DTPFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFrom.Location = new System.Drawing.Point(10, 23);
            this.DTPFrom.Name = "DTPFrom";
            this.DTPFrom.Size = new System.Drawing.Size(140, 22);
            this.DTPFrom.TabIndex = 221;
            // 
            // btnexcel
            // 
            this.btnexcel.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnexcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnexcel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnexcel.ForeColor = System.Drawing.Color.White;
            this.btnexcel.Location = new System.Drawing.Point(832, 10);
            this.btnexcel.Name = "btnexcel";
            this.btnexcel.Size = new System.Drawing.Size(97, 34);
            this.btnexcel.TabIndex = 224;
            this.btnexcel.Text = "&Print";
            this.btnexcel.UseVisualStyleBackColor = false;
            this.btnexcel.Click += new System.EventHandler(this.btnexcel_Click);
            // 
            // DTPTo
            // 
            this.DTPTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPTo.Location = new System.Drawing.Point(163, 23);
            this.DTPTo.Name = "DTPTo";
            this.DTPTo.Size = new System.Drawing.Size(138, 22);
            this.DTPTo.TabIndex = 222;
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(931, 10);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(97, 34);
            this.btnclose.TabIndex = 225;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(203, 4);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(63, 16);
            this.Label2.TabIndex = 227;
            this.Label2.Text = "To Date";
            // 
            // lv1
            // 
            this.lv1.BackColor = System.Drawing.SystemColors.Window;
            this.lv1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lv1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lv1.ForeColor = System.Drawing.Color.Navy;
            this.lv1.FullRowSelect = true;
            this.lv1.GridLines = true;
            this.lv1.HideSelection = false;
            this.lv1.Location = new System.Drawing.Point(6, 83);
            this.lv1.MultiSelect = false;
            this.lv1.Name = "lv1";
            this.lv1.Size = new System.Drawing.Size(1022, 155);
            this.lv1.TabIndex = 222;
            this.lv1.UseCompatibleStateImageBehavior = false;
            this.lv1.View = System.Windows.Forms.View.Details;
            // 
            // lv2
            // 
            this.lv2.BackColor = System.Drawing.SystemColors.Window;
            this.lv2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lv2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lv2.ForeColor = System.Drawing.Color.Navy;
            this.lv2.FullRowSelect = true;
            this.lv2.GridLines = true;
            this.lv2.HideSelection = false;
            this.lv2.Location = new System.Drawing.Point(6, 244);
            this.lv2.MultiSelect = false;
            this.lv2.Name = "lv2";
            this.lv2.Size = new System.Drawing.Size(1022, 128);
            this.lv2.TabIndex = 223;
            this.lv2.UseCompatibleStateImageBehavior = false;
            this.lv2.View = System.Windows.Forms.View.Details;
            // 
            // GSTR_3B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 594);
            this.Controls.Add(this.lv2);
            this.Controls.Add(this.lv1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox14);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GSTR_3B";
            this.Text = "GSTR_3B";
            this.Load += new System.EventHandler(this.GSTR_3B_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button BtnViewReport;
        internal System.Windows.Forms.DateTimePicker DTPFrom;
        internal System.Windows.Forms.Button btnexcel;
        internal System.Windows.Forms.DateTimePicker DTPTo;
        internal System.Windows.Forms.Button btnclose;
        internal System.Windows.Forms.Label Label2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox chkb2b;
        internal System.Windows.Forms.ListView lv4;
        internal System.Windows.Forms.ListView lv3;
        internal System.Windows.Forms.ListView lv1;
        internal System.Windows.Forms.ListView lv2;
    }
}