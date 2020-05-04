namespace RamdevSales
{
    partial class StockAvailability
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StockAvailability));
            this.cmbcomp = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.DTPTo = new System.Windows.Forms.DateTimePicker();
            this.BtnViewReport = new System.Windows.Forms.Button();
            this.btngenrpt = new System.Windows.Forms.Button();
            this.DTPFrom = new System.Windows.Forms.DateTimePicker();
            this.btnclose = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.txttotqty = new System.Windows.Forms.TextBox();
            this.txttotbill = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.grdshow = new System.Windows.Forms.DataGridView();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdshow)).BeginInit();
            this.GroupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbcomp
            // 
            this.cmbcomp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcomp.FormattingEnabled = true;
            this.cmbcomp.Location = new System.Drawing.Point(670, 42);
            this.cmbcomp.Name = "cmbcomp";
            this.cmbcomp.Size = new System.Drawing.Size(170, 28);
            this.cmbcomp.TabIndex = 124;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(667, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 16);
            this.label4.TabIndex = 123;
            this.label4.Text = "SELECT COMPANY";
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.cmbcomp);
            this.GroupBox2.Controls.Add(this.label4);
            this.GroupBox2.Controls.Add(this.DTPTo);
            this.GroupBox2.Controls.Add(this.BtnViewReport);
            this.GroupBox2.Controls.Add(this.btngenrpt);
            this.GroupBox2.Controls.Add(this.DTPFrom);
            this.GroupBox2.Controls.Add(this.btnclose);
            this.GroupBox2.Controls.Add(this.Label2);
            this.GroupBox2.Controls.Add(this.Label1);
            this.GroupBox2.Location = new System.Drawing.Point(1, 45);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(1249, 83);
            this.GroupBox2.TabIndex = 131;
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
            this.BtnViewReport.Location = new System.Drawing.Point(937, 12);
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
            this.btngenrpt.Location = new System.Drawing.Point(1017, 12);
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
            // 
            // btnclose
            // 
            this.btnclose.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.Image = ((System.Drawing.Image)(resources.GetObject("btnclose.Image")));
            this.btnclose.Location = new System.Drawing.Point(1172, 6);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(77, 71);
            this.btnclose.TabIndex = 122;
            this.btnclose.Text = "CLOSE";
            this.btnclose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnclose.UseVisualStyleBackColor = true;
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
            // txttotqty
            // 
            this.txttotqty.BackColor = System.Drawing.Color.Honeydew;
            this.txttotqty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txttotqty.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttotqty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txttotqty.Location = new System.Drawing.Point(1130, 548);
            this.txttotqty.Name = "txttotqty";
            this.txttotqty.Size = new System.Drawing.Size(114, 29);
            this.txttotqty.TabIndex = 136;
            this.txttotqty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txttotbill
            // 
            this.txttotbill.BackColor = System.Drawing.Color.Bisque;
            this.txttotbill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txttotbill.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttotbill.Location = new System.Drawing.Point(429, 546);
            this.txttotbill.Name = "txttotbill";
            this.txttotbill.Size = new System.Drawing.Size(176, 31);
            this.txttotbill.TabIndex = 134;
            this.txttotbill.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(365, 554);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 16);
            this.label3.TabIndex = 135;
            this.label3.Text = "TOTAL:";
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox4.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.Location = new System.Drawing.Point(0, 0);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(1262, 40);
            this.textBox4.TabIndex = 132;
            this.textBox4.TabStop = false;
            this.textBox4.Text = "DATE WISE INWARD REPORT";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(1028, 554);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 16);
            this.label5.TabIndex = 137;
            this.label5.Text = "TOTAL QTY:";
            // 
            // grdshow
            // 
            this.grdshow.BackgroundColor = System.Drawing.Color.LightYellow;
            this.grdshow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdshow.Location = new System.Drawing.Point(6, 19);
            this.grdshow.Name = "grdshow";
            this.grdshow.ReadOnly = true;
            this.grdshow.Size = new System.Drawing.Size(1216, 360);
            this.grdshow.TabIndex = 0;
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.grdshow);
            this.GroupBox3.Location = new System.Drawing.Point(12, 143);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(1238, 395);
            this.GroupBox3.TabIndex = 133;
            this.GroupBox3.TabStop = false;
            // 
            // StockAvailability
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1268, 601);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.txttotqty);
            this.Controls.Add(this.txttotbill);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.GroupBox3);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label5);
            this.Name = "StockAvailability";
            this.Text = "StockAvailability";
            this.Load += new System.EventHandler(this.StockAvailability_Load);
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdshow)).EndInit();
            this.GroupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbcomp;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.DateTimePicker DTPTo;
        internal System.Windows.Forms.Button BtnViewReport;
        internal System.Windows.Forms.Button btngenrpt;
        internal System.Windows.Forms.DateTimePicker DTPFrom;
        internal System.Windows.Forms.Button btnclose;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox txttotqty;
        internal System.Windows.Forms.TextBox txttotbill;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox textBox4;
        internal System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView grdshow;
        internal System.Windows.Forms.GroupBox GroupBox3;
    }
}