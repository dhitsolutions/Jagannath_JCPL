namespace RamdevSales
{
    partial class TrialBalance
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
            this.btnok = new System.Windows.Forms.Button();
            this.btnprint = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.DTPFrom = new System.Windows.Forms.DateTimePicker();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.LVclient = new System.Windows.Forms.ListView();
            this.txttotaldr = new System.Windows.Forms.TextBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.txttotalcr = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btnok
            // 
            this.btnok.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnok.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnok.ForeColor = System.Drawing.Color.White;
            this.btnok.Location = new System.Drawing.Point(533, 42);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(97, 34);
            this.btnok.TabIndex = 1;
            this.btnok.Text = "&OK";
            this.btnok.UseVisualStyleBackColor = false;
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            this.btnok.Enter += new System.EventHandler(this.btnok_Enter);
            this.btnok.Leave += new System.EventHandler(this.btnok_Leave);
            this.btnok.MouseEnter += new System.EventHandler(this.btnok_MouseEnter);
            this.btnok.MouseLeave += new System.EventHandler(this.btnok_MouseLeave);
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprint.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(636, 42);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(97, 34);
            this.btnprint.TabIndex = 2;
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
            this.btnclose.Location = new System.Drawing.Point(739, 42);
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
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(84, 34);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(84, 16);
            this.Label1.TabIndex = 217;
            this.Label1.Text = "From Date";
            // 
            // DTPFrom
            // 
            this.DTPFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFrom.Location = new System.Drawing.Point(54, 52);
            this.DTPFrom.Name = "DTPFrom";
            this.DTPFrom.Size = new System.Drawing.Size(148, 22);
            this.DTPFrom.TabIndex = 0;
            this.DTPFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTPFrom_KeyDown);
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.Highlight;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox7.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.ForeColor = System.Drawing.Color.White;
            this.textBox7.Location = new System.Drawing.Point(12, 1);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(837, 31);
            this.textBox7.TabIndex = 220;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "TRIAL BALANCE";
            // 
            // LVclient
            // 
            this.LVclient.BackColor = System.Drawing.SystemColors.Window;
            this.LVclient.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LVclient.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LVclient.ForeColor = System.Drawing.Color.Navy;
            this.LVclient.FullRowSelect = true;
            this.LVclient.GridLines = true;
            this.LVclient.HideSelection = false;
            this.LVclient.Location = new System.Drawing.Point(26, 82);
            this.LVclient.MultiSelect = false;
            this.LVclient.Name = "LVclient";
            this.LVclient.Size = new System.Drawing.Size(810, 401);
            this.LVclient.TabIndex = 4;
            this.LVclient.UseCompatibleStateImageBehavior = false;
            this.LVclient.View = System.Windows.Forms.View.Details;
            this.LVclient.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LVclient_KeyDown);
            this.LVclient.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LVclient_MouseDoubleClick);
            // 
            // txttotaldr
            // 
            this.txttotaldr.BackColor = System.Drawing.SystemColors.Window;
            this.txttotaldr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txttotaldr.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttotaldr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txttotaldr.Location = new System.Drawing.Point(474, 506);
            this.txttotaldr.Name = "txttotaldr";
            this.txttotaldr.Size = new System.Drawing.Size(154, 29);
            this.txttotaldr.TabIndex = 6;
            this.txttotaldr.TabStop = false;
            this.txttotaldr.Text = "0";
            this.txttotaldr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.ForeColor = System.Drawing.Color.Blue;
            this.Label7.Location = new System.Drawing.Point(524, 487);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(69, 16);
            this.Label7.TabIndex = 222;
            this.Label7.Text = "Total Dr.";
            // 
            // txttotalcr
            // 
            this.txttotalcr.BackColor = System.Drawing.SystemColors.Window;
            this.txttotalcr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txttotalcr.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttotalcr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txttotalcr.Location = new System.Drawing.Point(671, 506);
            this.txttotalcr.Name = "txttotalcr";
            this.txttotalcr.Size = new System.Drawing.Size(154, 29);
            this.txttotalcr.TabIndex = 5;
            this.txttotalcr.TabStop = false;
            this.txttotalcr.Text = "0";
            this.txttotalcr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(717, 487);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 16);
            this.label2.TabIndex = 224;
            this.label2.Text = "Total Cr.";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(13, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(836, 513);
            this.panel1.TabIndex = 225;
            // 
            // TrialBalance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(861, 580);
            this.ControlBox = false;
            this.Controls.Add(this.txttotalcr);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txttotaldr);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.LVclient);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.btnok);
            this.Controls.Add(this.btnprint);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.DTPFrom);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TrialBalance";
            this.Text = "TrialBalance";
            this.Load += new System.EventHandler(this.TrialBalance_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btnok;
        internal System.Windows.Forms.Button btnprint;
        internal System.Windows.Forms.Button btnclose;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.DateTimePicker DTPFrom;
        internal System.Windows.Forms.TextBox textBox7;
        internal System.Windows.Forms.ListView LVclient;
        internal System.Windows.Forms.TextBox txttotaldr;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.TextBox txttotalcr;
        internal System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
    }
}