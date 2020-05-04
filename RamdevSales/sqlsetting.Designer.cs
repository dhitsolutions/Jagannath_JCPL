namespace RamdevSales
{
    partial class sqlsetting
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
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.chksqlmulty = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtusername = new System.Windows.Forms.TextBox();
            this.btncancle = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtpass = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbauthentication = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbservername = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBox1
            // 
            this.TextBox1.BackColor = System.Drawing.SystemColors.Highlight;
            this.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBox1.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox1.ForeColor = System.Drawing.Color.White;
            this.TextBox1.Location = new System.Drawing.Point(1, 6);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = true;
            this.TextBox1.Size = new System.Drawing.Size(398, 31);
            this.TextBox1.TabIndex = 24;
            this.TextBox1.TabStop = false;
            this.TextBox1.Text = "SQL SERVER SETTINGS";
            // 
            // chksqlmulty
            // 
            this.chksqlmulty.AutoSize = true;
            this.chksqlmulty.Location = new System.Drawing.Point(11, 16);
            this.chksqlmulty.Name = "chksqlmulty";
            this.chksqlmulty.Size = new System.Drawing.Size(300, 17);
            this.chksqlmulty.TabIndex = 25;
            this.chksqlmulty.Text = "Enable Sql Server For Multi User Application Data Storage";
            this.chksqlmulty.UseVisualStyleBackColor = true;
            this.chksqlmulty.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.txtusername);
            this.groupBox1.Controls.Add(this.btncancle);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.txtpass);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbauthentication);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbservername);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chksqlmulty);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(1, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(398, 220);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // txtusername
            // 
            this.txtusername.Location = new System.Drawing.Point(81, 107);
            this.txtusername.Name = "txtusername";
            this.txtusername.Size = new System.Drawing.Size(248, 20);
            this.txtusername.TabIndex = 2;
            this.txtusername.Enter += new System.EventHandler(this.txtusername_Enter);
            this.txtusername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtusername_KeyDown);
            this.txtusername.Leave += new System.EventHandler(this.txtusername_Leave);
            // 
            // btncancle
            // 
            this.btncancle.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btncancle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncancle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncancle.ForeColor = System.Drawing.Color.White;
            this.btncancle.Location = new System.Drawing.Point(197, 174);
            this.btncancle.Name = "btncancle";
            this.btncancle.Size = new System.Drawing.Size(155, 38);
            this.btncancle.TabIndex = 5;
            this.btncancle.Text = "No Thanks, Close it";
            this.btncancle.UseVisualStyleBackColor = false;
            this.btncancle.Click += new System.EventHandler(this.btncancle_Click);
            this.btncancle.Enter += new System.EventHandler(this.btncancle_Enter);
            this.btncancle.Leave += new System.EventHandler(this.btncancle_Leave);
            this.btncancle.MouseEnter += new System.EventHandler(this.btncancle_MouseEnter);
            this.btncancle.MouseLeave += new System.EventHandler(this.btncancle_MouseLeave);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(31, 174);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(157, 38);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Submit My Settings";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnSave.Enter += new System.EventHandler(this.btnSave_Enter);
            this.btnSave.Leave += new System.EventHandler(this.btnSave_Leave);
            this.btnSave.MouseEnter += new System.EventHandler(this.btnSave_MouseEnter);
            this.btnSave.MouseLeave += new System.EventHandler(this.btnSave_MouseLeave);
            // 
            // txtpass
            // 
            this.txtpass.Location = new System.Drawing.Point(81, 143);
            this.txtpass.Name = "txtpass";
            this.txtpass.PasswordChar = '*';
            this.txtpass.Size = new System.Drawing.Size(248, 20);
            this.txtpass.TabIndex = 3;
            this.txtpass.Enter += new System.EventHandler(this.txtpass_Enter);
            this.txtpass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpass_KeyDown);
            this.txtpass.Leave += new System.EventHandler(this.txtpass_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "User Name";
            // 
            // cmbauthentication
            // 
            this.cmbauthentication.FormattingEnabled = true;
            this.cmbauthentication.Items.AddRange(new object[] {
            "Windows Authentication",
            "SQL Server Authentication"});
            this.cmbauthentication.Location = new System.Drawing.Point(81, 73);
            this.cmbauthentication.Name = "cmbauthentication";
            this.cmbauthentication.Size = new System.Drawing.Size(248, 21);
            this.cmbauthentication.TabIndex = 1;
            this.cmbauthentication.SelectedIndexChanged += new System.EventHandler(this.cmbauthentication_SelectedIndexChanged);
            this.cmbauthentication.Enter += new System.EventHandler(this.cmbauthentication_Enter);
            this.cmbauthentication.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbauthentication_KeyDown);
            this.cmbauthentication.Leave += new System.EventHandler(this.cmbauthentication_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Authentication";
            // 
            // cmbservername
            // 
            this.cmbservername.FormattingEnabled = true;
            this.cmbservername.Location = new System.Drawing.Point(81, 37);
            this.cmbservername.Name = "cmbservername";
            this.cmbservername.Size = new System.Drawing.Size(248, 21);
            this.cmbservername.TabIndex = 0;
            this.cmbservername.Enter += new System.EventHandler(this.cmbservername_Enter);
            this.cmbservername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbservername_KeyDown);
            this.cmbservername.Leave += new System.EventHandler(this.cmbservername_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Server Name";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(398, 220);
            this.panel1.TabIndex = 33;
            // 
            // sqlsetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(400, 259);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.TextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "sqlsetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "sqlsetting";
            this.Load += new System.EventHandler(this.sqlsetting_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox TextBox1;
        private System.Windows.Forms.CheckBox chksqlmulty;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtpass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbauthentication;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbservername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btncancle;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtusername;
        private System.Windows.Forms.Panel panel1;
    }
}