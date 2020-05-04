namespace RamdevSales
{
    partial class CompanyMaster
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtsupplierdesc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txttotalvat = new System.Windows.Forms.TextBox();
            this.lblvat = new System.Windows.Forms.Label();
            this.txtphno = new System.Windows.Forms.TextBox();
            this.lblphno = new System.Windows.Forms.Label();
            this.txtcompname = new System.Windows.Forms.TextBox();
            this.txtcompadd = new System.Windows.Forms.TextBox();
            this.txtcontact = new System.Windows.Forms.TextBox();
            this.lblcontact = new System.Windows.Forms.Label();
            this.lbladdrs = new System.Windows.Forms.Label();
            this.lblcomp = new System.Windows.Forms.Label();
            this.btnaddprod = new System.Windows.Forms.Button();
            this.btncancel = new System.Windows.Forms.Button();
            this.btnsave = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.LVclientadd = new System.Windows.Forms.ListView();
            this.btnedit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBox1
            // 
            this.TextBox1.BackColor = System.Drawing.SystemColors.Highlight;
            this.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBox1.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox1.ForeColor = System.Drawing.Color.White;
            this.TextBox1.Location = new System.Drawing.Point(0, 0);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = true;
            this.TextBox1.Size = new System.Drawing.Size(1095, 31);
            this.TextBox1.TabIndex = 22;
            this.TextBox1.TabStop = false;
            this.TextBox1.Text = "COMPANY DETAILS";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.txtsupplierdesc);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txttotalvat);
            this.groupBox1.Controls.Add(this.lblvat);
            this.groupBox1.Controls.Add(this.txtphno);
            this.groupBox1.Controls.Add(this.lblphno);
            this.groupBox1.Controls.Add(this.txtcompname);
            this.groupBox1.Controls.Add(this.txtcompadd);
            this.groupBox1.Controls.Add(this.txtcontact);
            this.groupBox1.Controls.Add(this.lblcontact);
            this.groupBox1.Controls.Add(this.lbladdrs);
            this.groupBox1.Controls.Add(this.lblcomp);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.groupBox1.Location = new System.Drawing.Point(11, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(405, 370);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtsupplierdesc
            // 
            this.txtsupplierdesc.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsupplierdesc.Location = new System.Drawing.Point(172, 277);
            this.txtsupplierdesc.Multiline = true;
            this.txtsupplierdesc.Name = "txtsupplierdesc";
            this.txtsupplierdesc.Size = new System.Drawing.Size(222, 68);
            this.txtsupplierdesc.TabIndex = 5;
            this.txtsupplierdesc.Enter += new System.EventHandler(this.txtsupplierdesc_Enter);
            this.txtsupplierdesc.Leave += new System.EventHandler(this.txtsupplierdesc_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(15, 304);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 17);
            this.label2.TabIndex = 33;
            this.label2.Text = "Supplier Description";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(233, 242);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 17);
            this.label1.TabIndex = 31;
            this.label1.Text = "( In Percentage)";
            this.label1.Visible = false;
            // 
            // txttotalvat
            // 
            this.txttotalvat.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttotalvat.Location = new System.Drawing.Point(172, 239);
            this.txttotalvat.Name = "txttotalvat";
            this.txttotalvat.Size = new System.Drawing.Size(55, 24);
            this.txttotalvat.TabIndex = 4;
            this.txttotalvat.Visible = false;
            this.txttotalvat.Enter += new System.EventHandler(this.txttotalvat_Enter);
            this.txttotalvat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txttotalvat_KeyDown);
            this.txttotalvat.Leave += new System.EventHandler(this.txttotalvat_Leave);
            // 
            // lblvat
            // 
            this.lblvat.AutoSize = true;
            this.lblvat.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblvat.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblvat.Location = new System.Drawing.Point(15, 246);
            this.lblvat.Name = "lblvat";
            this.lblvat.Size = new System.Drawing.Size(77, 17);
            this.lblvat.TabIndex = 30;
            this.lblvat.Text = "Total VAT";
            this.lblvat.Visible = false;
            // 
            // txtphno
            // 
            this.txtphno.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtphno.Location = new System.Drawing.Point(172, 191);
            this.txtphno.Name = "txtphno";
            this.txtphno.Size = new System.Drawing.Size(222, 24);
            this.txtphno.TabIndex = 3;
            this.txtphno.Enter += new System.EventHandler(this.txtphno_Enter);
            this.txtphno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtphno_KeyDown);
            this.txtphno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtphno_KeyPress);
            this.txtphno.Leave += new System.EventHandler(this.txtphno_Leave);
            // 
            // lblphno
            // 
            this.lblphno.AutoSize = true;
            this.lblphno.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblphno.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblphno.Location = new System.Drawing.Point(15, 195);
            this.lblphno.Name = "lblphno";
            this.lblphno.Size = new System.Drawing.Size(111, 17);
            this.lblphno.TabIndex = 28;
            this.lblphno.Text = "Phone Number";
            // 
            // txtcompname
            // 
            this.txtcompname.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcompname.Location = new System.Drawing.Point(172, 29);
            this.txtcompname.Name = "txtcompname";
            this.txtcompname.Size = new System.Drawing.Size(222, 24);
            this.txtcompname.TabIndex = 0;
            this.txtcompname.Enter += new System.EventHandler(this.txtcompname_Enter);
            this.txtcompname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtcompname_KeyDown);
            this.txtcompname.Leave += new System.EventHandler(this.txtcompname_Leave);
            // 
            // txtcompadd
            // 
            this.txtcompadd.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcompadd.Location = new System.Drawing.Point(172, 68);
            this.txtcompadd.Multiline = true;
            this.txtcompadd.Name = "txtcompadd";
            this.txtcompadd.Size = new System.Drawing.Size(222, 68);
            this.txtcompadd.TabIndex = 1;
            this.txtcompadd.Enter += new System.EventHandler(this.txtcompadd_Enter);
            this.txtcompadd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtcompadd_KeyDown);
            this.txtcompadd.Leave += new System.EventHandler(this.txtcompadd_Leave);
            // 
            // txtcontact
            // 
            this.txtcontact.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcontact.Location = new System.Drawing.Point(172, 153);
            this.txtcontact.Name = "txtcontact";
            this.txtcontact.Size = new System.Drawing.Size(222, 24);
            this.txtcontact.TabIndex = 2;
            this.txtcontact.Enter += new System.EventHandler(this.txtcontact_Enter);
            this.txtcontact.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtcontact_KeyDown);
            this.txtcontact.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtcontact_KeyPress);
            this.txtcontact.Leave += new System.EventHandler(this.txtcontact_Leave);
            // 
            // lblcontact
            // 
            this.lblcontact.AutoSize = true;
            this.lblcontact.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcontact.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblcontact.Location = new System.Drawing.Point(15, 157);
            this.lblcontact.Name = "lblcontact";
            this.lblcontact.Size = new System.Drawing.Size(111, 17);
            this.lblcontact.TabIndex = 20;
            this.lblcontact.Text = "Mobile Number";
            // 
            // lbladdrs
            // 
            this.lbladdrs.AutoSize = true;
            this.lbladdrs.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbladdrs.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbladdrs.Location = new System.Drawing.Point(15, 95);
            this.lbladdrs.Name = "lbladdrs";
            this.lbladdrs.Size = new System.Drawing.Size(137, 17);
            this.lbladdrs.TabIndex = 6;
            this.lbladdrs.Text = "Company Address";
            // 
            // lblcomp
            // 
            this.lblcomp.AutoSize = true;
            this.lblcomp.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcomp.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblcomp.Location = new System.Drawing.Point(15, 32);
            this.lblcomp.Name = "lblcomp";
            this.lblcomp.Size = new System.Drawing.Size(118, 17);
            this.lblcomp.TabIndex = 5;
            this.lblcomp.Text = "Company Name";
            // 
            // btnaddprod
            // 
            this.btnaddprod.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnaddprod.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnaddprod.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnaddprod.ForeColor = System.Drawing.Color.White;
            this.btnaddprod.Location = new System.Drawing.Point(875, 450);
            this.btnaddprod.Name = "btnaddprod";
            this.btnaddprod.Size = new System.Drawing.Size(101, 34);
            this.btnaddprod.TabIndex = 3;
            this.btnaddprod.Text = "Add Products";
            this.btnaddprod.UseVisualStyleBackColor = false;
            this.btnaddprod.Visible = false;
            this.btnaddprod.Click += new System.EventHandler(this.btnaddprod_Click);
            // 
            // btncancel
            // 
            this.btncancel.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btncancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncancel.ForeColor = System.Drawing.Color.White;
            this.btncancel.Location = new System.Drawing.Point(985, 410);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(97, 34);
            this.btncancel.TabIndex = 7;
            this.btncancel.Text = "Close";
            this.btncancel.UseVisualStyleBackColor = false;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            this.btncancel.Enter += new System.EventHandler(this.btncancel_Enter);
            this.btncancel.Leave += new System.EventHandler(this.btncancel_Leave);
            this.btncancel.MouseEnter += new System.EventHandler(this.btncancel_MouseEnter);
            this.btncancel.MouseLeave += new System.EventHandler(this.btncancel_MouseLeave);
            // 
            // btnsave
            // 
            this.btnsave.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnsave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsave.ForeColor = System.Drawing.Color.White;
            this.btnsave.Location = new System.Drawing.Point(882, 410);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(97, 34);
            this.btnsave.TabIndex = 6;
            this.btnsave.Text = "&Submit";
            this.btnsave.UseVisualStyleBackColor = false;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            this.btnsave.Enter += new System.EventHandler(this.btnsave_Enter);
            this.btnsave.Leave += new System.EventHandler(this.btnsave_Leave);
            this.btnsave.MouseEnter += new System.EventHandler(this.btnsave_MouseEnter);
            this.btnsave.MouseLeave += new System.EventHandler(this.btnsave_MouseLeave);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.LVclientadd);
            this.groupBox2.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.groupBox2.Location = new System.Drawing.Point(422, 37);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(663, 370);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // LVclientadd
            // 
            this.LVclientadd.BackColor = System.Drawing.SystemColors.Window;
            this.LVclientadd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LVclientadd.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LVclientadd.ForeColor = System.Drawing.Color.Navy;
            this.LVclientadd.FullRowSelect = true;
            this.LVclientadd.GridLines = true;
            this.LVclientadd.HideSelection = false;
            this.LVclientadd.Location = new System.Drawing.Point(8, 19);
            this.LVclientadd.MultiSelect = false;
            this.LVclientadd.Name = "LVclientadd";
            this.LVclientadd.Size = new System.Drawing.Size(649, 345);
            this.LVclientadd.TabIndex = 8;
            this.LVclientadd.UseCompatibleStateImageBehavior = false;
            this.LVclientadd.View = System.Windows.Forms.View.Details;
            this.LVclientadd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LVclientadd_KeyDown);
            this.LVclientadd.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LVclientadd_MouseDoubleClick);
            // 
            // btnedit
            // 
            this.btnedit.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnedit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnedit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnedit.ForeColor = System.Drawing.Color.White;
            this.btnedit.Location = new System.Drawing.Point(988, 449);
            this.btnedit.Name = "btnedit";
            this.btnedit.Size = new System.Drawing.Size(97, 34);
            this.btnedit.TabIndex = 9;
            this.btnedit.Text = "Edit";
            this.btnedit.UseVisualStyleBackColor = false;
            this.btnedit.Visible = false;
            this.btnedit.Click += new System.EventHandler(this.btnedit_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(0, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1095, 469);
            this.panel1.TabIndex = 25;
            // 
            // CompanyMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1132, 512);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnedit);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.btnsave);
            this.Controls.Add(this.btnaddprod);
            this.Controls.Add(this.btncancel);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CompanyMaster";
            this.Text = "CompanyMaster";
            this.Load += new System.EventHandler(this.CompanyMaster_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox TextBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtcompadd;
        private System.Windows.Forms.TextBox txtcontact;
        private System.Windows.Forms.Label lblcontact;
        private System.Windows.Forms.Button btncancel;
        private System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.Label lbladdrs;
        internal System.Windows.Forms.Label lblcomp;
        private System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.ListView LVclientadd;
        private System.Windows.Forms.TextBox txtcompname;
        private System.Windows.Forms.Button btnaddprod;
        private System.Windows.Forms.TextBox txtphno;
        private System.Windows.Forms.Label lblphno;
        private System.Windows.Forms.Button btnedit;
        private System.Windows.Forms.TextBox txttotalvat;
        private System.Windows.Forms.Label lblvat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtsupplierdesc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
    }
}