namespace RamdevSales
{
    partial class ItemGroup
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
            this.LVclient = new System.Windows.Forms.ListView();
            this.Button18 = new System.Windows.Forms.Button();
            this.txtgrpname = new System.Windows.Forms.TextBox();
            this.ss = new System.Windows.Forms.Label();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btndelete = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.LVclient.Location = new System.Drawing.Point(12, 129);
            this.LVclient.MultiSelect = false;
            this.LVclient.Name = "LVclient";
            this.LVclient.Size = new System.Drawing.Size(697, 367);
            this.LVclient.TabIndex = 3;
            this.LVclient.UseCompatibleStateImageBehavior = false;
            this.LVclient.View = System.Windows.Forms.View.Details;
            this.LVclient.SelectedIndexChanged += new System.EventHandler(this.LVclient_SelectedIndexChanged);
            this.LVclient.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LVclient_KeyDown);
            this.LVclient.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LVclient_MouseDoubleClick);
            // 
            // Button18
            // 
            this.Button18.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Button18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button18.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button18.ForeColor = System.Drawing.Color.White;
            this.Button18.Location = new System.Drawing.Point(505, 38);
            this.Button18.Name = "Button18";
            this.Button18.Size = new System.Drawing.Size(98, 39);
            this.Button18.TabIndex = 1;
            this.Button18.Text = "Submit";
            this.Button18.UseVisualStyleBackColor = false;
            this.Button18.Click += new System.EventHandler(this.Button18_Click);
            this.Button18.Enter += new System.EventHandler(this.Button18_Enter);
            this.Button18.Leave += new System.EventHandler(this.Button18_Leave);
            this.Button18.MouseEnter += new System.EventHandler(this.Button18_MouseEnter);
            this.Button18.MouseLeave += new System.EventHandler(this.Button18_MouseLeave);
            // 
            // txtgrpname
            // 
            this.txtgrpname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtgrpname.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtgrpname.Location = new System.Drawing.Point(151, 48);
            this.txtgrpname.Name = "txtgrpname";
            this.txtgrpname.Size = new System.Drawing.Size(345, 23);
            this.txtgrpname.TabIndex = 0;
            this.txtgrpname.Enter += new System.EventHandler(this.txtgrpname_Enter);
            this.txtgrpname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtgrpname_KeyDown);
            this.txtgrpname.Leave += new System.EventHandler(this.txtgrpname_Leave);
            // 
            // ss
            // 
            this.ss.AutoSize = true;
            this.ss.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ss.Location = new System.Drawing.Point(12, 51);
            this.ss.Name = "ss";
            this.ss.Size = new System.Drawing.Size(136, 16);
            this.ss.TabIndex = 208;
            this.ss.Text = "Item Group Name";
            // 
            // textBox14
            // 
            this.textBox14.BackColor = System.Drawing.SystemColors.Highlight;
            this.textBox14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox14.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox14.ForeColor = System.Drawing.Color.White;
            this.textBox14.Location = new System.Drawing.Point(2, 0);
            this.textBox14.Name = "textBox14";
            this.textBox14.ReadOnly = true;
            this.textBox14.Size = new System.Drawing.Size(715, 31);
            this.textBox14.TabIndex = 207;
            this.textBox14.TabStop = false;
            this.textBox14.Text = "CREATE ITEM GROUP";
            this.textBox14.TextChanged += new System.EventHandler(this.textBox14_TextChanged);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(611, 38);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(98, 39);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.Enter += new System.EventHandler(this.btnClose_Enter);
            this.btnClose.Leave += new System.EventHandler(this.btnClose_Leave);
            this.btnClose.MouseEnter += new System.EventHandler(this.btnClose_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btndelete);
            this.panel1.Location = new System.Drawing.Point(2, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(715, 471);
            this.panel1.TabIndex = 209;
            // 
            // btndelete
            // 
            this.btndelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btndelete.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btndelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndelete.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndelete.ForeColor = System.Drawing.Color.White;
            this.btndelete.Location = new System.Drawing.Point(606, 51);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(98, 35);
            this.btndelete.TabIndex = 13;
            this.btndelete.Text = "&Delete";
            this.btndelete.UseVisualStyleBackColor = false;
            this.btndelete.Click += new System.EventHandler(this.btndelete_Click);
            this.btndelete.Enter += new System.EventHandler(this.btndelete_Enter);
            this.btndelete.Leave += new System.EventHandler(this.btndelete_Leave);
            this.btndelete.MouseEnter += new System.EventHandler(this.btndelete_MouseEnter);
            this.btndelete.MouseLeave += new System.EventHandler(this.btndelete_MouseLeave);
            // 
            // ItemGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(719, 508);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.LVclient);
            this.Controls.Add(this.Button18);
            this.Controls.Add(this.txtgrpname);
            this.Controls.Add(this.ss);
            this.Controls.Add(this.textBox14);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ItemGroup";
            this.Text = "ItemGroup";
            this.Load += new System.EventHandler(this.ItemGroup_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ListView LVclient;
        internal System.Windows.Forms.Button Button18;
        internal System.Windows.Forms.TextBox txtgrpname;
        internal System.Windows.Forms.Label ss;
        internal System.Windows.Forms.TextBox textBox14;
        internal System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Button btndelete;
    }
}