namespace RamdevSales
{
    partial class DailyStatus
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
            this.components = new System.ComponentModel.Container();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.BtnViewReport = new System.Windows.Forms.Button();
            this.btngenrpt = new System.Windows.Forms.Button();
            this.DTPFrom = new System.Windows.Forms.DateTimePicker();
            this.btnclose = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.LVledger = new System.Windows.Forms.ListView();
            this.label4 = new System.Windows.Forms.Label();
            this.txtopbalcredit = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtclcash = new System.Windows.Forms.TextBox();
            this.txtclcredit = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtopbalcash = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.Highlight;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox7.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.ForeColor = System.Drawing.Color.White;
            this.textBox7.Location = new System.Drawing.Point(2, 0);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(1098, 31);
            this.textBox7.TabIndex = 172;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "DAILY STATUS";
            // 
            // BtnViewReport
            // 
            this.BtnViewReport.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BtnViewReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnViewReport.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnViewReport.ForeColor = System.Drawing.Color.White;
            this.BtnViewReport.Location = new System.Drawing.Point(187, 27);
            this.BtnViewReport.Name = "BtnViewReport";
            this.BtnViewReport.Size = new System.Drawing.Size(97, 47);
            this.BtnViewReport.TabIndex = 3;
            this.BtnViewReport.Text = "OK";
            this.BtnViewReport.UseVisualStyleBackColor = false;
            this.BtnViewReport.Click += new System.EventHandler(this.BtnViewReport_Click);
            this.BtnViewReport.Enter += new System.EventHandler(this.BtnViewReport_Enter);
            this.BtnViewReport.Leave += new System.EventHandler(this.BtnViewReport_Leave);
            this.BtnViewReport.MouseEnter += new System.EventHandler(this.BtnViewReport_MouseEnter);
            this.BtnViewReport.MouseLeave += new System.EventHandler(this.BtnViewReport_MouseLeave);
            // 
            // btngenrpt
            // 
            this.btngenrpt.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btngenrpt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btngenrpt.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btngenrpt.ForeColor = System.Drawing.Color.White;
            this.btngenrpt.Location = new System.Drawing.Point(896, 40);
            this.btngenrpt.Name = "btngenrpt";
            this.btngenrpt.Size = new System.Drawing.Size(97, 34);
            this.btngenrpt.TabIndex = 4;
            this.btngenrpt.Text = "Print";
            this.btngenrpt.UseVisualStyleBackColor = false;
            this.btngenrpt.Click += new System.EventHandler(this.btngenrpt_Click);
            this.btngenrpt.Enter += new System.EventHandler(this.btngenrpt_Enter);
            this.btngenrpt.Leave += new System.EventHandler(this.btngenrpt_Leave);
            this.btngenrpt.MouseEnter += new System.EventHandler(this.btngenrpt_MouseEnter);
            this.btngenrpt.MouseLeave += new System.EventHandler(this.btngenrpt_MouseLeave);
            // 
            // DTPFrom
            // 
            this.DTPFrom.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFrom.Location = new System.Drawing.Point(13, 50);
            this.DTPFrom.Name = "DTPFrom";
            this.DTPFrom.Size = new System.Drawing.Size(153, 24);
            this.DTPFrom.TabIndex = 1;
            this.DTPFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTPFrom_KeyDown);
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(997, 40);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(97, 34);
            this.btnclose.TabIndex = 5;
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
            this.Label1.Location = new System.Drawing.Point(47, 27);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(84, 16);
            this.Label1.TabIndex = 173;
            this.Label1.Text = "From Date";
            // 
            // LVledger
            // 
            this.LVledger.BackColor = System.Drawing.SystemColors.Window;
            this.LVledger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LVledger.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LVledger.ForeColor = System.Drawing.Color.Navy;
            this.LVledger.FullRowSelect = true;
            this.LVledger.GridLines = true;
            this.LVledger.HideSelection = false;
            this.LVledger.Location = new System.Drawing.Point(15, 106);
            this.LVledger.MultiSelect = false;
            this.LVledger.Name = "LVledger";
            this.LVledger.Size = new System.Drawing.Size(1082, 331);
            this.LVledger.TabIndex = 6;
            this.LVledger.UseCompatibleStateImageBehavior = false;
            this.LVledger.View = System.Windows.Forms.View.Details;
            this.LVledger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LVledger_KeyDown);
            this.LVledger.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LVledger_MouseDoubleClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(618, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 16);
            this.label4.TabIndex = 189;
            this.label4.Text = "Opening Balance";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // txtopbalcredit
            // 
            this.txtopbalcredit.BackColor = System.Drawing.SystemColors.Window;
            this.txtopbalcredit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtopbalcredit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtopbalcredit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtopbalcredit.Location = new System.Drawing.Point(749, 72);
            this.txtopbalcredit.Name = "txtopbalcredit";
            this.txtopbalcredit.Size = new System.Drawing.Size(170, 26);
            this.txtopbalcredit.TabIndex = 190;
            this.txtopbalcredit.TabStop = false;
            this.txtopbalcredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtopbalcredit.Enter += new System.EventHandler(this.txtopbal_Enter);
            this.txtopbalcredit.Leave += new System.EventHandler(this.txtopbal_Leave);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.LVledger);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtclcash);
            this.panel1.Controls.Add(this.txtclcredit);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtopbalcash);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtopbalcredit);
            this.panel1.Controls.Add(this.Label1);
            this.panel1.Controls.Add(this.DTPFrom);
            this.panel1.Controls.Add(this.BtnViewReport);
            this.panel1.Location = new System.Drawing.Point(2, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1098, 489);
            this.panel1.TabIndex = 191;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(937, 440);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 16);
            this.label5.TabIndex = 197;
            this.label5.Text = "Closing Bal. Cash";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(761, 440);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(141, 16);
            this.label6.TabIndex = 196;
            this.label6.Text = "Closing Bal. Credit";
            // 
            // txtclcash
            // 
            this.txtclcash.BackColor = System.Drawing.SystemColors.Window;
            this.txtclcash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtclcash.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtclcash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtclcash.Location = new System.Drawing.Point(921, 458);
            this.txtclcash.Name = "txtclcash";
            this.txtclcash.Size = new System.Drawing.Size(170, 26);
            this.txtclcash.TabIndex = 195;
            this.txtclcash.TabStop = false;
            this.txtclcash.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtclcredit
            // 
            this.txtclcredit.BackColor = System.Drawing.SystemColors.Window;
            this.txtclcredit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtclcredit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtclcredit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtclcredit.Location = new System.Drawing.Point(749, 458);
            this.txtclcredit.Name = "txtclcredit";
            this.txtclcredit.Size = new System.Drawing.Size(170, 26);
            this.txtclcredit.TabIndex = 194;
            this.txtclcredit.TabStop = false;
            this.txtclcredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(955, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 16);
            this.label3.TabIndex = 193;
            this.label3.Text = "Balance Cash";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(776, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 16);
            this.label2.TabIndex = 192;
            this.label2.Text = "Balance Credit";
            // 
            // txtopbalcash
            // 
            this.txtopbalcash.BackColor = System.Drawing.SystemColors.Window;
            this.txtopbalcash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtopbalcash.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtopbalcash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtopbalcash.Location = new System.Drawing.Point(921, 72);
            this.txtopbalcash.Name = "txtopbalcash";
            this.txtopbalcash.Size = new System.Drawing.Size(170, 26);
            this.txtopbalcash.TabIndex = 191;
            this.txtopbalcash.TabStop = false;
            this.txtopbalcash.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // DailyStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1109, 546);
            this.ControlBox = false;
            this.Controls.Add(this.btngenrpt);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DailyStatus";
            this.Text = "Daily Status";
            this.Load += new System.EventHandler(this.Ledger_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox textBox7;
        internal System.Windows.Forms.Button BtnViewReport;
        internal System.Windows.Forms.Button btngenrpt;
        internal System.Windows.Forms.DateTimePicker DTPFrom;
        internal System.Windows.Forms.Button btnclose;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.ListView LVledger;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox txtopbalcredit;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.TextBox txtopbalcash;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.TextBox txtclcash;
        internal System.Windows.Forms.TextBox txtclcredit;
    }
}