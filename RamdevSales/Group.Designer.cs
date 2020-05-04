namespace RamdevSales
{
    partial class Group
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
            this.ss = new System.Windows.Forms.Label();
            this.txtgrpname = new System.Windows.Forms.TextBox();
            this.Button18 = new System.Windows.Forms.Button();
            this.LVclient = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtgrop = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkpgroup = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBox1
            // 
            this.TextBox1.BackColor = System.Drawing.SystemColors.Highlight;
            this.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBox1.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox1.ForeColor = System.Drawing.Color.White;
            this.TextBox1.Location = new System.Drawing.Point(3, 0);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = true;
            this.TextBox1.Size = new System.Drawing.Size(682, 31);
            this.TextBox1.TabIndex = 22;
            this.TextBox1.TabStop = false;
            this.TextBox1.Text = "GROUP";
            // 
            // ss
            // 
            this.ss.AutoSize = true;
            this.ss.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ss.Location = new System.Drawing.Point(9, 49);
            this.ss.Name = "ss";
            this.ss.Size = new System.Drawing.Size(160, 16);
            this.ss.TabIndex = 126;
            this.ss.Text = "Account Group Name";
            // 
            // txtgrpname
            // 
            this.txtgrpname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtgrpname.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtgrpname.Location = new System.Drawing.Point(177, 46);
            this.txtgrpname.Name = "txtgrpname";
            this.txtgrpname.Size = new System.Drawing.Size(296, 23);
            this.txtgrpname.TabIndex = 0;
            this.txtgrpname.Enter += new System.EventHandler(this.txtgrpname_Enter);
            this.txtgrpname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtgrpname_KeyDown);
            this.txtgrpname.Leave += new System.EventHandler(this.txtgrpname_Leave);
            // 
            // Button18
            // 
            this.Button18.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Button18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button18.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button18.ForeColor = System.Drawing.Color.White;
            this.Button18.Location = new System.Drawing.Point(478, 40);
            this.Button18.Name = "Button18";
            this.Button18.Size = new System.Drawing.Size(97, 34);
            this.Button18.TabIndex = 1;
            this.Button18.Text = "Submit";
            this.Button18.UseVisualStyleBackColor = false;
            this.Button18.Click += new System.EventHandler(this.Button18_Click);
            this.Button18.Enter += new System.EventHandler(this.Button18_Enter);
            this.Button18.Leave += new System.EventHandler(this.Button18_Leave);
            this.Button18.MouseEnter += new System.EventHandler(this.Button18_MouseEnter);
            this.Button18.MouseLeave += new System.EventHandler(this.Button18_MouseLeave);
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
            this.LVclient.Location = new System.Drawing.Point(8, 88);
            this.LVclient.MultiSelect = false;
            this.LVclient.Name = "LVclient";
            this.LVclient.Size = new System.Drawing.Size(664, 414);
            this.LVclient.TabIndex = 2;
            this.LVclient.UseCompatibleStateImageBehavior = false;
            this.LVclient.View = System.Windows.Forms.View.Details;
            this.LVclient.SelectedIndexChanged += new System.EventHandler(this.LVclient_SelectedIndexChanged);
            this.LVclient.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LVclient_KeyDown);
            this.LVclient.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LVclient_MouseDoubleClick);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.chkpgroup);
            this.panel1.Controls.Add(this.txtgrop);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.LVclient);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Location = new System.Drawing.Point(3, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(682, 514);
            this.panel1.TabIndex = 127;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(575, 7);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(97, 34);
            this.btnClose.TabIndex = 22;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.Enter += new System.EventHandler(this.btnClose_Enter);
            this.btnClose.Leave += new System.EventHandler(this.btnClose_Leave);
            this.btnClose.MouseEnter += new System.EventHandler(this.btnClose_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            // 
            // txtgrop
            // 
            this.txtgrop.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtgrop.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtgrop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtgrop.FormattingEnabled = true;
            this.txtgrop.Location = new System.Drawing.Point(173, 52);
            this.txtgrop.Name = "txtgrop";
            this.txtgrop.Size = new System.Drawing.Size(296, 21);
            this.txtgrop.TabIndex = 128;
            this.txtgrop.SelectedIndexChanged += new System.EventHandler(this.txtgrop_SelectedIndexChanged);
            this.txtgrop.Enter += new System.EventHandler(this.txtgrop_Enter);
            this.txtgrop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtgrop_KeyDown);
            this.txtgrop.Leave += new System.EventHandler(this.txtgrop_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(66, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 16);
            this.label3.TabIndex = 129;
            this.label3.Text = "Under Group";
            // 
            // chkpgroup
            // 
            this.chkpgroup.AutoSize = true;
            this.chkpgroup.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkpgroup.Location = new System.Drawing.Point(476, 55);
            this.chkpgroup.Name = "chkpgroup";
            this.chkpgroup.Size = new System.Drawing.Size(170, 18);
            this.chkpgroup.TabIndex = 130;
            this.chkpgroup.Text = "This is a Primary Group";
            this.chkpgroup.UseVisualStyleBackColor = true;
            this.chkpgroup.CheckedChanged += new System.EventHandler(this.chkpgroup_CheckedChanged);
            this.chkpgroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkpgroup_KeyDown);
            // 
            // Group
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(688, 546);
            this.ControlBox = false;
            this.Controls.Add(this.Button18);
            this.Controls.Add(this.txtgrpname);
            this.Controls.Add(this.ss);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Group";
            this.Text = "Group";
            this.Load += new System.EventHandler(this.Group_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox TextBox1;
        internal System.Windows.Forms.Label ss;
        internal System.Windows.Forms.TextBox txtgrpname;
        internal System.Windows.Forms.Button Button18;
        internal System.Windows.Forms.ListView LVclient;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox txtgrop;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkpgroup;

    }
}