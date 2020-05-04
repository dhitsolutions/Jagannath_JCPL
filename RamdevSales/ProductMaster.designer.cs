namespace RamdevSales
{
    partial class ProductMaster
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
            this.txtprodname = new System.Windows.Forms.TextBox();
            this.txtvattax = new System.Windows.Forms.TextBox();
            this.lblvattax = new System.Windows.Forms.Label();
            this.cmbcompany = new System.Windows.Forms.ComboBox();
            this.btncancel = new System.Windows.Forms.Button();
            this.btnsave = new System.Windows.Forms.Button();
            this.txtmrp = new System.Windows.Forms.TextBox();
            this.lblmrp = new System.Windows.Forms.Label();
            this.lblproduct = new System.Windows.Forms.Label();
            this.lblcomp = new System.Windows.Forms.Label();
            this.LVclientproductadd = new System.Windows.Forms.ListView();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnedit = new System.Windows.Forms.Button();
            this.btnback = new System.Windows.Forms.Button();
            this.txtbarnum = new System.Windows.Forms.TextBox();
            this.lblbarcord = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Menu;
            this.groupBox1.Controls.Add(this.txtbarnum);
            this.groupBox1.Controls.Add(this.lblbarcord);
            this.groupBox1.Controls.Add(this.lblper);
            this.groupBox1.Controls.Add(this.txtprodname);
            this.groupBox1.Controls.Add(this.txtvattax);
            this.groupBox1.Controls.Add(this.lblvattax);
            this.groupBox1.Controls.Add(this.cmbcompany);
            this.groupBox1.Controls.Add(this.btncancel);
            this.groupBox1.Controls.Add(this.btnsave);
            this.groupBox1.Controls.Add(this.txtmrp);
            this.groupBox1.Controls.Add(this.lblmrp);
            this.groupBox1.Controls.Add(this.lblproduct);
            this.groupBox1.Controls.Add(this.lblcomp);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.groupBox1.Location = new System.Drawing.Point(12, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(525, 370);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Client Product Add";
            // 
            // lblper
            // 
            this.lblper.AutoSize = true;
            this.lblper.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblper.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblper.Location = new System.Drawing.Point(324, 238);
            this.lblper.Name = "lblper";
            this.lblper.Size = new System.Drawing.Size(23, 17);
            this.lblper.TabIndex = 25;
            this.lblper.Text = "%";
            // 
            // txtprodname
            // 
            this.txtprodname.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtprodname.Location = new System.Drawing.Point(200, 141);
            this.txtprodname.Name = "txtprodname";
            this.txtprodname.Size = new System.Drawing.Size(222, 24);
            this.txtprodname.TabIndex = 3;
            // 
            // txtvattax
            // 
            this.txtvattax.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtvattax.Location = new System.Drawing.Point(200, 235);
            this.txtvattax.Name = "txtvattax";
            this.txtvattax.Size = new System.Drawing.Size(116, 24);
            this.txtvattax.TabIndex = 5;
            // 
            // lblvattax
            // 
            this.lblvattax.AutoSize = true;
            this.lblvattax.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblvattax.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblvattax.Location = new System.Drawing.Point(43, 238);
            this.lblvattax.Name = "lblvattax";
            this.lblvattax.Size = new System.Drawing.Size(68, 17);
            this.lblvattax.TabIndex = 21;
            this.lblvattax.Text = "VAT Tax";
            // 
            // cmbcompany
            // 
            this.cmbcompany.BackColor = System.Drawing.Color.PeachPuff;
            this.cmbcompany.FormattingEnabled = true;
            this.cmbcompany.Items.AddRange(new object[] {
            "Select Clietn Name"});
            this.cmbcompany.Location = new System.Drawing.Point(200, 54);
            this.cmbcompany.Name = "cmbcompany";
            this.cmbcompany.Size = new System.Drawing.Size(222, 24);
            this.cmbcompany.TabIndex = 1;
            this.cmbcompany.SelectedIndexChanged += new System.EventHandler(this.cmbcompany_SelectedIndexChanged);
            // 
            // btncancel
            // 
            this.btncancel.BackColor = System.Drawing.Color.AntiqueWhite;
            this.btncancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncancel.Location = new System.Drawing.Point(390, 307);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(97, 34);
            this.btncancel.TabIndex = 7;
            this.btncancel.Text = "Cancel";
            this.btncancel.UseVisualStyleBackColor = false;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // btnsave
            // 
            this.btnsave.BackColor = System.Drawing.Color.AntiqueWhite;
            this.btnsave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsave.Location = new System.Drawing.Point(272, 307);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(97, 34);
            this.btnsave.TabIndex = 6;
            this.btnsave.Text = "Save";
            this.btnsave.UseVisualStyleBackColor = false;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // txtmrp
            // 
            this.txtmrp.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmrp.Location = new System.Drawing.Point(200, 190);
            this.txtmrp.Name = "txtmrp";
            this.txtmrp.Size = new System.Drawing.Size(116, 24);
            this.txtmrp.TabIndex = 4;
            // 
            // lblmrp
            // 
            this.lblmrp.AutoSize = true;
            this.lblmrp.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmrp.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblmrp.Location = new System.Drawing.Point(43, 193);
            this.lblmrp.Name = "lblmrp";
            this.lblmrp.Size = new System.Drawing.Size(89, 17);
            this.lblmrp.TabIndex = 7;
            this.lblmrp.Text = "M.R.P. Price";
            // 
            // lblproduct
            // 
            this.lblproduct.AutoSize = true;
            this.lblproduct.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblproduct.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblproduct.Location = new System.Drawing.Point(43, 144);
            this.lblproduct.Name = "lblproduct";
            this.lblproduct.Size = new System.Drawing.Size(106, 17);
            this.lblproduct.TabIndex = 6;
            this.lblproduct.Text = "Product Name";
            // 
            // lblcomp
            // 
            this.lblcomp.AutoSize = true;
            this.lblcomp.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcomp.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblcomp.Location = new System.Drawing.Point(43, 56);
            this.lblcomp.Name = "lblcomp";
            this.lblcomp.Size = new System.Drawing.Size(121, 17);
            this.lblcomp.TabIndex = 5;
            this.lblcomp.Text = "Select Company";
            // 
            // LVclientproductadd
            // 
            this.LVclientproductadd.BackColor = System.Drawing.Color.LightYellow;
            this.LVclientproductadd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LVclientproductadd.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LVclientproductadd.ForeColor = System.Drawing.Color.Navy;
            this.LVclientproductadd.FullRowSelect = true;
            this.LVclientproductadd.GridLines = true;
            this.LVclientproductadd.HideSelection = false;
            this.LVclientproductadd.Location = new System.Drawing.Point(20, 19);
            this.LVclientproductadd.MultiSelect = false;
            this.LVclientproductadd.Name = "LVclientproductadd";
            this.LVclientproductadd.Size = new System.Drawing.Size(642, 345);
            this.LVclientproductadd.TabIndex = 8;
            this.LVclientproductadd.UseCompatibleStateImageBehavior = false;
            this.LVclientproductadd.View = System.Windows.Forms.View.Details;
            this.LVclientproductadd.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LVclientproductadd_MouseDoubleClick);
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
            this.TextBox1.TabIndex = 21;
            this.TextBox1.TabStop = false;
            this.TextBox1.Text = "Product Details";
            this.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.LVclientproductadd);
            this.groupBox2.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.groupBox2.Location = new System.Drawing.Point(544, 68);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(678, 370);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Client Product";
            // 
            // btnedit
            // 
            this.btnedit.BackColor = System.Drawing.Color.AntiqueWhite;
            this.btnedit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnedit.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnedit.Location = new System.Drawing.Point(1109, 444);
            this.btnedit.Name = "btnedit";
            this.btnedit.Size = new System.Drawing.Size(97, 34);
            this.btnedit.TabIndex = 9;
            this.btnedit.Text = "Edit";
            this.btnedit.UseVisualStyleBackColor = false;
            this.btnedit.Click += new System.EventHandler(this.btnedit_Click);
            // 
            // btnback
            // 
            this.btnback.BackColor = System.Drawing.Color.AntiqueWhite;
            this.btnback.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnback.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnback.Location = new System.Drawing.Point(880, 447);
            this.btnback.Name = "btnback";
            this.btnback.Size = new System.Drawing.Size(204, 34);
            this.btnback.TabIndex = 26;
            this.btnback.Text = "Back on Company Detail";
            this.btnback.UseVisualStyleBackColor = false;
            this.btnback.Click += new System.EventHandler(this.btnback_Click);
            // 
            // txtbarnum
            // 
            this.txtbarnum.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbarnum.Location = new System.Drawing.Point(200, 94);
            this.txtbarnum.Name = "txtbarnum";
            this.txtbarnum.Size = new System.Drawing.Size(222, 24);
            this.txtbarnum.TabIndex = 2;
            this.txtbarnum.Validated += new System.EventHandler(this.txtbarnum_Validated);
            // 
            // lblbarcord
            // 
            this.lblbarcord.AutoSize = true;
            this.lblbarcord.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblbarcord.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblbarcord.Location = new System.Drawing.Point(43, 98);
            this.lblbarcord.Name = "lblbarcord";
            this.lblbarcord.Size = new System.Drawing.Size(123, 17);
            this.lblbarcord.TabIndex = 27;
            this.lblbarcord.Text = "Barcord Number";
            // 
            // ProductMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 493);
            this.Controls.Add(this.btnback);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnedit);
            this.Name = "ProductMaster";
            this.Text = "ProductMaster";
            this.Load += new System.EventHandler(this.ProductMaster_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtprodname;
        private System.Windows.Forms.TextBox txtvattax;
        private System.Windows.Forms.Label lblvattax;
        private System.Windows.Forms.ComboBox cmbcompany;
        private System.Windows.Forms.Button btncancel;
        private System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.TextBox txtmrp;
        private System.Windows.Forms.Label lblmrp;
        private System.Windows.Forms.Label lblproduct;
        internal System.Windows.Forms.Label lblcomp;
        internal System.Windows.Forms.ListView LVclientproductadd;
        internal System.Windows.Forms.TextBox TextBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnedit;
        private System.Windows.Forms.Label lblper;
        private System.Windows.Forms.Button btnback;
        private System.Windows.Forms.TextBox txtbarnum;
        private System.Windows.Forms.Label lblbarcord;
    }
}