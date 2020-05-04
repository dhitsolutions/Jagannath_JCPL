namespace RamdevSales
{
    partial class ClientWiseProductMargin
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblper = new System.Windows.Forms.Label();
            this.cmbproduct = new System.Windows.Forms.ComboBox();
            this.cmbcomp = new System.Windows.Forms.ComboBox();
            this.cmbclientname = new System.Windows.Forms.ComboBox();
            this.btncancel = new System.Windows.Forms.Button();
            this.btnsubmit = new System.Windows.Forms.Button();
            this.txtmargin = new System.Windows.Forms.TextBox();
            this.lblmargin = new System.Windows.Forms.Label();
            this.lblproduct = new System.Windows.Forms.Label();
            this.lblcomp = new System.Windows.Forms.Label();
            this.lblclientname = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.LVclientproduct = new System.Windows.Forms.ListView();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.btnedit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Menu;
            this.groupBox1.Controls.Add(this.lblper);
            this.groupBox1.Controls.Add(this.cmbproduct);
            this.groupBox1.Controls.Add(this.cmbcomp);
            this.groupBox1.Controls.Add(this.cmbclientname);
            this.groupBox1.Controls.Add(this.btncancel);
            this.groupBox1.Controls.Add(this.btnsubmit);
            this.groupBox1.Controls.Add(this.txtmargin);
            this.groupBox1.Controls.Add(this.lblmargin);
            this.groupBox1.Controls.Add(this.lblproduct);
            this.groupBox1.Controls.Add(this.lblcomp);
            this.groupBox1.Controls.Add(this.lblclientname);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.groupBox1.Location = new System.Drawing.Point(11, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(525, 370);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Client Product Add";
            // 
            // lblper
            // 
            this.lblper.AutoSize = true;
            this.lblper.Location = new System.Drawing.Point(288, 193);
            this.lblper.Name = "lblper";
            this.lblper.Size = new System.Drawing.Size(20, 17);
            this.lblper.TabIndex = 19;
            this.lblper.Text = "%";
            // 
            // cmbproduct
            // 
            this.cmbproduct.FormattingEnabled = true;
            this.cmbproduct.Location = new System.Drawing.Point(200, 148);
            this.cmbproduct.Name = "cmbproduct";
            this.cmbproduct.Size = new System.Drawing.Size(291, 24);
            this.cmbproduct.TabIndex = 3;
            this.cmbproduct.SelectedIndexChanged += new System.EventHandler(this.cmbproduct_SelectedIndexChanged);
            // 
            // cmbcomp
            // 
            this.cmbcomp.FormattingEnabled = true;
            this.cmbcomp.Location = new System.Drawing.Point(200, 98);
            this.cmbcomp.Name = "cmbcomp";
            this.cmbcomp.Size = new System.Drawing.Size(291, 24);
            this.cmbcomp.TabIndex = 17;
            this.cmbcomp.SelectedIndexChanged += new System.EventHandler(this.cmbcomp_SelectedIndexChanged);
            // 
            // cmbclientname
            // 
            this.cmbclientname.BackColor = System.Drawing.Color.PeachPuff;
            this.cmbclientname.FormattingEnabled = true;
            this.cmbclientname.Items.AddRange(new object[] {
            "Select Clietn Name"});
            this.cmbclientname.Location = new System.Drawing.Point(200, 54);
            this.cmbclientname.Name = "cmbclientname";
            this.cmbclientname.Size = new System.Drawing.Size(291, 24);
            this.cmbclientname.TabIndex = 16;
            this.cmbclientname.SelectedIndexChanged += new System.EventHandler(this.cmbclientname_SelectedIndexChanged);
            // 
            // btncancel
            // 
            this.btncancel.BackColor = System.Drawing.Color.AntiqueWhite;
            this.btncancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncancel.Location = new System.Drawing.Point(390, 307);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(97, 34);
            this.btncancel.TabIndex = 15;
            this.btncancel.Text = "Cancel";
            this.btncancel.UseVisualStyleBackColor = false;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // btnsubmit
            // 
            this.btnsubmit.BackColor = System.Drawing.Color.AntiqueWhite;
            this.btnsubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsubmit.Location = new System.Drawing.Point(272, 307);
            this.btnsubmit.Name = "btnsubmit";
            this.btnsubmit.Size = new System.Drawing.Size(97, 34);
            this.btnsubmit.TabIndex = 12;
            this.btnsubmit.Text = "Submit";
            this.btnsubmit.UseVisualStyleBackColor = false;
            this.btnsubmit.Click += new System.EventHandler(this.btnsubmit_Click);
            // 
            // txtmargin
            // 
            this.txtmargin.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmargin.Location = new System.Drawing.Point(200, 190);
            this.txtmargin.Name = "txtmargin";
            this.txtmargin.Size = new System.Drawing.Size(86, 24);
            this.txtmargin.TabIndex = 4;
            // 
            // lblmargin
            // 
            this.lblmargin.AutoSize = true;
            this.lblmargin.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmargin.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblmargin.Location = new System.Drawing.Point(43, 193);
            this.lblmargin.Name = "lblmargin";
            this.lblmargin.Size = new System.Drawing.Size(141, 17);
            this.lblmargin.TabIndex = 7;
            this.lblmargin.Text = "Margin per Product";
            // 
            // lblproduct
            // 
            this.lblproduct.AutoSize = true;
            this.lblproduct.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblproduct.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblproduct.Location = new System.Drawing.Point(43, 148);
            this.lblproduct.Name = "lblproduct";
            this.lblproduct.Size = new System.Drawing.Size(109, 17);
            this.lblproduct.TabIndex = 6;
            this.lblproduct.Text = "Select Product";
            // 
            // lblcomp
            // 
            this.lblcomp.AutoSize = true;
            this.lblcomp.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcomp.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblcomp.Location = new System.Drawing.Point(43, 101);
            this.lblcomp.Name = "lblcomp";
            this.lblcomp.Size = new System.Drawing.Size(121, 17);
            this.lblcomp.TabIndex = 5;
            this.lblcomp.Text = "Select Company";
            // 
            // lblclientname
            // 
            this.lblclientname.AutoSize = true;
            this.lblclientname.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblclientname.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblclientname.Location = new System.Drawing.Point(43, 54);
            this.lblclientname.Name = "lblclientname";
            this.lblclientname.Size = new System.Drawing.Size(138, 17);
            this.lblclientname.TabIndex = 0;
            this.lblclientname.Text = "Select Client Name";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.LVclientproduct);
            this.groupBox2.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.groupBox2.Location = new System.Drawing.Point(544, 68);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(678, 370);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Client Product";
            // 
            // LVclientproduct
            // 
            this.LVclientproduct.BackColor = System.Drawing.Color.LightYellow;
            this.LVclientproduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LVclientproduct.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LVclientproduct.ForeColor = System.Drawing.Color.Navy;
            this.LVclientproduct.FullRowSelect = true;
            this.LVclientproduct.GridLines = true;
            this.LVclientproduct.HideSelection = false;
            this.LVclientproduct.Location = new System.Drawing.Point(20, 19);
            this.LVclientproduct.MultiSelect = false;
            this.LVclientproduct.Name = "LVclientproduct";
            this.LVclientproduct.Size = new System.Drawing.Size(642, 345);
            this.LVclientproduct.TabIndex = 1;
            this.LVclientproduct.UseCompatibleStateImageBehavior = false;
            this.LVclientproduct.View = System.Windows.Forms.View.Details;
            this.LVclientproduct.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LVclientproduct_MouseDoubleClick);
            // 
            // TextBox1
            // 
            this.TextBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBox1.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox1.Location = new System.Drawing.Point(0, 0);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = true;
            this.TextBox1.Size = new System.Drawing.Size(1234, 40);
            this.TextBox1.TabIndex = 17;
            this.TextBox1.TabStop = false;
            this.TextBox1.Text = "Client Wise Product Margin";
            this.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnedit
            // 
            this.btnedit.BackColor = System.Drawing.Color.AntiqueWhite;
            this.btnedit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnedit.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnedit.Location = new System.Drawing.Point(1109, 444);
            this.btnedit.Name = "btnedit";
            this.btnedit.Size = new System.Drawing.Size(97, 34);
            this.btnedit.TabIndex = 19;
            this.btnedit.Text = "Edit";
            this.btnedit.UseVisualStyleBackColor = false;
            this.btnedit.Click += new System.EventHandler(this.btnedit_Click);
            // 
            // ClientWiseProductMargin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 493);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.btnedit);
            this.Name = "ClientWiseProductMargin";
            this.Text = "ClientWiseProduct";
            this.Load += new System.EventHandler(this.ClientWiseProductMargin_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbcomp;
        private System.Windows.Forms.ComboBox cmbclientname;
        private System.Windows.Forms.Button btncancel;
        private System.Windows.Forms.Button btnsubmit;
        private System.Windows.Forms.TextBox txtmargin;
        private System.Windows.Forms.Label lblmargin;
        private System.Windows.Forms.Label lblproduct;
        internal System.Windows.Forms.Label lblcomp;
        private System.Windows.Forms.Label lblclientname;
        private System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.ListView LVclientproduct;
        internal System.Windows.Forms.TextBox TextBox1;
        private System.Windows.Forms.Button btnedit;
        private System.Windows.Forms.ComboBox cmbproduct;
        private System.Windows.Forms.Label lblper;
    }
}