﻿namespace RamdevSales
{
    partial class BankEntryReport
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
            this.DTPTo = new System.Windows.Forms.DateTimePicker();
            this.BtnViewReport = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.DTPFrom = new System.Windows.Forms.DateTimePicker();
            this.Label2 = new System.Windows.Forms.Label();
            this.btnprint = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtamount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtexp = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtnetamt = new System.Windows.Forms.TextBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.LVDayBook = new System.Windows.Forms.ListView();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox14
            // 
            this.textBox14.BackColor = System.Drawing.SystemColors.Highlight;
            this.textBox14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox14.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox14.ForeColor = System.Drawing.Color.White;
            this.textBox14.Location = new System.Drawing.Point(2, 2);
            this.textBox14.Multiline = true;
            this.textBox14.Name = "textBox14";
            this.textBox14.ReadOnly = true;
            this.textBox14.Size = new System.Drawing.Size(1030, 29);
            this.textBox14.TabIndex = 204;
            this.textBox14.TabStop = false;
            this.textBox14.Text = "BANK ENTRY REGISTER";
            // 
            // DTPTo
            // 
            this.DTPTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPTo.Location = new System.Drawing.Point(172, 61);
            this.DTPTo.Name = "DTPTo";
            this.DTPTo.Size = new System.Drawing.Size(154, 22);
            this.DTPTo.TabIndex = 214;
            this.DTPTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTPTo_KeyDown);
            // 
            // BtnViewReport
            // 
            this.BtnViewReport.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BtnViewReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnViewReport.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnViewReport.ForeColor = System.Drawing.Color.White;
            this.BtnViewReport.Location = new System.Drawing.Point(340, 54);
            this.BtnViewReport.Name = "BtnViewReport";
            this.BtnViewReport.Size = new System.Drawing.Size(97, 34);
            this.BtnViewReport.TabIndex = 215;
            this.BtnViewReport.Text = "&OK";
            this.BtnViewReport.UseVisualStyleBackColor = false;
            this.BtnViewReport.Click += new System.EventHandler(this.BtnViewReport_Click);
            this.BtnViewReport.Enter += new System.EventHandler(this.BtnViewReport_Enter);
            this.BtnViewReport.Leave += new System.EventHandler(this.BtnViewReport_Leave);
            this.BtnViewReport.MouseEnter += new System.EventHandler(this.BtnViewReport_MouseEnter);
            this.BtnViewReport.MouseLeave += new System.EventHandler(this.BtnViewReport_MouseLeave);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.White;
            this.Label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(41, 42);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(84, 16);
            this.Label1.TabIndex = 218;
            this.Label1.Text = "From Date";
            // 
            // DTPFrom
            // 
            this.DTPFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFrom.Location = new System.Drawing.Point(8, 61);
            this.DTPFrom.Name = "DTPFrom";
            this.DTPFrom.Size = new System.Drawing.Size(152, 22);
            this.DTPFrom.TabIndex = 213;
            this.DTPFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTPFrom_KeyDown);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.White;
            this.Label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(215, 42);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(63, 16);
            this.Label2.TabIndex = 219;
            this.Label2.Text = "To Date";
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprint.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(829, 57);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(97, 34);
            this.btnprint.TabIndex = 216;
            this.btnprint.Text = "&Print";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            this.btnprint.Enter += new System.EventHandler(this.btnprint_Enter);
            this.btnprint.Leave += new System.EventHandler(this.btnprint_Leave);
            this.btnprint.MouseEnter += new System.EventHandler(this.btnprint_MouseEnter);
            this.btnprint.MouseLeave += new System.EventHandler(this.btnprint_MouseLeave);
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(932, 57);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(97, 34);
            this.btnclose.TabIndex = 217;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            this.btnclose.Enter += new System.EventHandler(this.btnclose_Enter);
            this.btnclose.Leave += new System.EventHandler(this.btnclose_Leave);
            this.btnclose.MouseEnter += new System.EventHandler(this.btnclose_MouseEnter);
            this.btnclose.MouseLeave += new System.EventHandler(this.btnclose_MouseLeave);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtamount);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtexp);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtnetamt);
            this.panel1.Controls.Add(this.Label7);
            this.panel1.Controls.Add(this.LVDayBook);
            this.panel1.Location = new System.Drawing.Point(4, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1028, 505);
            this.panel1.TabIndex = 220;
            // 
            // txtamount
            // 
            this.txtamount.BackColor = System.Drawing.SystemColors.Window;
            this.txtamount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtamount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtamount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtamount.Location = new System.Drawing.Point(549, 471);
            this.txtamount.Name = "txtamount";
            this.txtamount.Size = new System.Drawing.Size(154, 29);
            this.txtamount.TabIndex = 225;
            this.txtamount.TabStop = false;
            this.txtamount.Text = "0";
            this.txtamount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(607, 452);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 16);
            this.label4.TabIndex = 226;
            this.label4.Text = "Amount";
            // 
            // txtexp
            // 
            this.txtexp.BackColor = System.Drawing.SystemColors.Window;
            this.txtexp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtexp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtexp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtexp.Location = new System.Drawing.Point(709, 471);
            this.txtexp.Name = "txtexp";
            this.txtexp.Size = new System.Drawing.Size(154, 29);
            this.txtexp.TabIndex = 223;
            this.txtexp.TabStop = false;
            this.txtexp.Text = "0";
            this.txtexp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(767, 452);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 224;
            this.label3.Text = "Exp.";
            // 
            // txtnetamt
            // 
            this.txtnetamt.BackColor = System.Drawing.SystemColors.Window;
            this.txtnetamt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtnetamt.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnetamt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtnetamt.Location = new System.Drawing.Point(869, 471);
            this.txtnetamt.Name = "txtnetamt";
            this.txtnetamt.Size = new System.Drawing.Size(154, 29);
            this.txtnetamt.TabIndex = 221;
            this.txtnetamt.TabStop = false;
            this.txtnetamt.Text = "0";
            this.txtnetamt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.ForeColor = System.Drawing.Color.Blue;
            this.Label7.Location = new System.Drawing.Point(916, 452);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(72, 16);
            this.Label7.TabIndex = 222;
            this.Label7.Text = "Net Amt.";
            // 
            // LVDayBook
            // 
            this.LVDayBook.BackColor = System.Drawing.SystemColors.Window;
            this.LVDayBook.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LVDayBook.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LVDayBook.ForeColor = System.Drawing.Color.Navy;
            this.LVDayBook.FullRowSelect = true;
            this.LVDayBook.GridLines = true;
            this.LVDayBook.HideSelection = false;
            this.LVDayBook.Location = new System.Drawing.Point(4, 66);
            this.LVDayBook.MultiSelect = false;
            this.LVDayBook.Name = "LVDayBook";
            this.LVDayBook.Size = new System.Drawing.Size(1021, 380);
            this.LVDayBook.TabIndex = 221;
            this.LVDayBook.UseCompatibleStateImageBehavior = false;
            this.LVDayBook.View = System.Windows.Forms.View.Details;
            this.LVDayBook.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LVDayBook_KeyDown);
            this.LVDayBook.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LVDayBook_MouseDoubleClick);
            // 
            // BankEntryReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 537);
            this.Controls.Add(this.DTPTo);
            this.Controls.Add(this.BtnViewReport);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.DTPFrom);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.btnprint);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.textBox14);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BankEntryReport";
            this.Text = "BankEntryReport";
            this.Load += new System.EventHandler(this.BankEntryReport_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox textBox14;
        internal System.Windows.Forms.DateTimePicker DTPTo;
        internal System.Windows.Forms.Button BtnViewReport;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.DateTimePicker DTPFrom;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Button btnprint;
        internal System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.ListView LVDayBook;
        internal System.Windows.Forms.TextBox txtamount;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox txtexp;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox txtnetamt;
        internal System.Windows.Forms.Label Label7;
    }
}