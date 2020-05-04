namespace RamdevSales
{
    partial class outstandinganalysis
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
            this.btnclose = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.DTPFrom = new System.Windows.Forms.DateTimePicker();
            this.btnprint = new System.Windows.Forms.Button();
            this.btnemail = new System.Windows.Forms.Button();
            this.btnsms = new System.Windows.Forms.Button();
            this.LVclient = new System.Windows.Forms.ListView();
            this.txtnetamt = new System.Windows.Forms.TextBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.btnrec = new System.Windows.Forms.Button();
            this.btnpayble = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txttudaysbal = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkselectall = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.Highlight;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox7.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.ForeColor = System.Drawing.Color.White;
            this.textBox7.Location = new System.Drawing.Point(3, 3);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(1030, 31);
            this.textBox7.TabIndex = 173;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "OUTSTANDING ANALYSIS";
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(922, 82);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(97, 34);
            this.btnclose.TabIndex = 6;
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
            this.Label1.Location = new System.Drawing.Point(68, 56);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(84, 16);
            this.Label1.TabIndex = 207;
            this.Label1.Text = "From Date";
            // 
            // DTPFrom
            // 
            this.DTPFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFrom.Location = new System.Drawing.Point(39, 77);
            this.DTPFrom.Name = "DTPFrom";
            this.DTPFrom.Size = new System.Drawing.Size(129, 22);
            this.DTPFrom.TabIndex = 0;
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprint.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(922, 45);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(97, 34);
            this.btnprint.TabIndex = 5;
            this.btnprint.Text = "&PRINT";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            this.btnprint.Enter += new System.EventHandler(this.btnprint_Enter);
            this.btnprint.Leave += new System.EventHandler(this.btnprint_Leave);
            this.btnprint.MouseEnter += new System.EventHandler(this.btnprint_MouseEnter);
            this.btnprint.MouseLeave += new System.EventHandler(this.btnprint_MouseLeave);
            // 
            // btnemail
            // 
            this.btnemail.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnemail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnemail.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnemail.ForeColor = System.Drawing.Color.White;
            this.btnemail.Location = new System.Drawing.Point(810, 45);
            this.btnemail.Name = "btnemail";
            this.btnemail.Size = new System.Drawing.Size(97, 34);
            this.btnemail.TabIndex = 3;
            this.btnemail.Text = "&EMAIL";
            this.btnemail.UseVisualStyleBackColor = false;
            this.btnemail.Click += new System.EventHandler(this.btnemail_Click);
            this.btnemail.Enter += new System.EventHandler(this.btnemail_Enter);
            this.btnemail.Leave += new System.EventHandler(this.btnemail_Leave);
            this.btnemail.MouseEnter += new System.EventHandler(this.btnemail_MouseEnter);
            this.btnemail.MouseLeave += new System.EventHandler(this.btnemail_MouseLeave);
            // 
            // btnsms
            // 
            this.btnsms.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnsms.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsms.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsms.ForeColor = System.Drawing.Color.White;
            this.btnsms.Location = new System.Drawing.Point(810, 85);
            this.btnsms.Name = "btnsms";
            this.btnsms.Size = new System.Drawing.Size(97, 34);
            this.btnsms.TabIndex = 4;
            this.btnsms.Text = "&SMS";
            this.btnsms.UseVisualStyleBackColor = false;
            this.btnsms.Enter += new System.EventHandler(this.btnsms_Enter);
            this.btnsms.Leave += new System.EventHandler(this.btnsms_Leave);
            this.btnsms.MouseEnter += new System.EventHandler(this.btnsms_MouseEnter);
            this.btnsms.MouseLeave += new System.EventHandler(this.btnsms_MouseLeave);
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
            this.LVclient.Location = new System.Drawing.Point(12, 125);
            this.LVclient.MultiSelect = false;
            this.LVclient.Name = "LVclient";
            this.LVclient.Size = new System.Drawing.Size(1007, 387);
            this.LVclient.TabIndex = 7;
            this.LVclient.UseCompatibleStateImageBehavior = false;
            this.LVclient.View = System.Windows.Forms.View.Details;
            this.LVclient.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LVclient_KeyDown);
            this.LVclient.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LVclient_MouseDoubleClick);
            // 
            // txtnetamt
            // 
            this.txtnetamt.BackColor = System.Drawing.SystemColors.Window;
            this.txtnetamt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtnetamt.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnetamt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtnetamt.Location = new System.Drawing.Point(695, 501);
            this.txtnetamt.Name = "txtnetamt";
            this.txtnetamt.ReadOnly = true;
            this.txtnetamt.Size = new System.Drawing.Size(154, 29);
            this.txtnetamt.TabIndex = 8;
            this.txtnetamt.TabStop = false;
            this.txtnetamt.Text = "0";
            this.txtnetamt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.ForeColor = System.Drawing.Color.Blue;
            this.Label7.Location = new System.Drawing.Point(724, 482);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(106, 16);
            this.Label7.TabIndex = 212;
            this.Label7.Text = "Total Balance";
            // 
            // btnrec
            // 
            this.btnrec.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnrec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnrec.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnrec.ForeColor = System.Drawing.Color.White;
            this.btnrec.Location = new System.Drawing.Point(697, 45);
            this.btnrec.Name = "btnrec";
            this.btnrec.Size = new System.Drawing.Size(97, 34);
            this.btnrec.TabIndex = 1;
            this.btnrec.Text = "&Receivable";
            this.btnrec.UseVisualStyleBackColor = false;
            this.btnrec.Click += new System.EventHandler(this.btnrec_Click);
            this.btnrec.Enter += new System.EventHandler(this.btnrec_Enter);
            this.btnrec.Leave += new System.EventHandler(this.btnrec_Leave);
            this.btnrec.MouseEnter += new System.EventHandler(this.btnrec_MouseEnter);
            this.btnrec.MouseLeave += new System.EventHandler(this.btnrec_MouseLeave);
            // 
            // btnpayble
            // 
            this.btnpayble.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnpayble.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnpayble.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnpayble.ForeColor = System.Drawing.Color.White;
            this.btnpayble.Location = new System.Drawing.Point(697, 85);
            this.btnpayble.Name = "btnpayble";
            this.btnpayble.Size = new System.Drawing.Size(97, 34);
            this.btnpayble.TabIndex = 2;
            this.btnpayble.Text = "&Payable";
            this.btnpayble.UseVisualStyleBackColor = false;
            this.btnpayble.Click += new System.EventHandler(this.btnpayble_Click);
            this.btnpayble.Enter += new System.EventHandler(this.btnpayble_Enter);
            this.btnpayble.Leave += new System.EventHandler(this.btnpayble_Leave);
            this.btnpayble.MouseEnter += new System.EventHandler(this.btnpayble_MouseEnter);
            this.btnpayble.MouseLeave += new System.EventHandler(this.btnpayble_MouseLeave);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txttudaysbal);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtnetamt);
            this.panel1.Controls.Add(this.Label7);
            this.panel1.Controls.Add(this.chkselectall);
            this.panel1.Location = new System.Drawing.Point(3, 34);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1030, 536);
            this.panel1.TabIndex = 216;
            // 
            // txttudaysbal
            // 
            this.txttudaysbal.BackColor = System.Drawing.SystemColors.Window;
            this.txttudaysbal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txttudaysbal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttudaysbal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txttudaysbal.Location = new System.Drawing.Point(850, 501);
            this.txttudaysbal.Name = "txttudaysbal";
            this.txttudaysbal.ReadOnly = true;
            this.txttudaysbal.Size = new System.Drawing.Size(154, 29);
            this.txttudaysbal.TabIndex = 217;
            this.txttudaysbal.TabStop = false;
            this.txttudaysbal.Text = "0";
            this.txttudaysbal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(846, 482);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 16);
            this.label2.TabIndex = 218;
            this.label2.Text = "Total Today\'s Balance";
            // 
            // chkselectall
            // 
            this.chkselectall.AutoSize = true;
            this.chkselectall.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkselectall.Location = new System.Drawing.Point(11, 67);
            this.chkselectall.Name = "chkselectall";
            this.chkselectall.Size = new System.Drawing.Size(82, 18);
            this.chkselectall.TabIndex = 0;
            this.chkselectall.Text = "Select All";
            this.chkselectall.UseVisualStyleBackColor = true;
            this.chkselectall.CheckedChanged += new System.EventHandler(this.chkselectall_CheckedChanged);
            // 
            // outstandinganalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1050, 583);
            this.ControlBox = false;
            this.Controls.Add(this.btnpayble);
            this.Controls.Add(this.btnrec);
            this.Controls.Add(this.LVclient);
            this.Controls.Add(this.btnsms);
            this.Controls.Add(this.btnemail);
            this.Controls.Add(this.btnprint);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.DTPFrom);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "outstandinganalysis";
            this.Text = "outstandinganalysis";
            this.Load += new System.EventHandler(this.outstandinganalysis_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox textBox7;
        internal System.Windows.Forms.Button btnclose;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.DateTimePicker DTPFrom;
        internal System.Windows.Forms.Button btnprint;
        internal System.Windows.Forms.Button btnemail;
        internal System.Windows.Forms.Button btnsms;
        internal System.Windows.Forms.ListView LVclient;
        internal System.Windows.Forms.TextBox txtnetamt;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Button btnrec;
        internal System.Windows.Forms.Button btnpayble;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkselectall;
        internal System.Windows.Forms.TextBox txttudaysbal;
        internal System.Windows.Forms.Label label2;
    }
}