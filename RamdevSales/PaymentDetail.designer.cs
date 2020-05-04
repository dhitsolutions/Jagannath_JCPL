namespace RamdevSales
{
    partial class PaymentDetail
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
            this.btnsubmit = new System.Windows.Forms.Button();
            this.btncancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbstatus = new System.Windows.Forms.ComboBox();
            this.ss = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtrecamt = new System.Windows.Forms.TextBox();
            this.dtpaymentdt = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdpending = new System.Windows.Forms.RadioButton();
            this.rdpaid = new System.Windows.Forms.RadioButton();
            this.lblclientname = new System.Windows.Forms.Label();
            this.btnedit = new System.Windows.Forms.Button();
            this.txtbillno = new System.Windows.Forms.TextBox();
            this.txtclientname = new System.Windows.Forms.TextBox();
            this.lblpaystatus = new System.Windows.Forms.Label();
            this.lblbillno = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txttotrec = new System.Windows.Forms.TextBox();
            this.txttotbill = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LVClientpaymentDetail = new System.Windows.Forms.ListView();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBox1
            // 
            this.TextBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBox1.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox1.Location = new System.Drawing.Point(0, 0);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = true;
            this.TextBox1.Size = new System.Drawing.Size(1041, 40);
            this.TextBox1.TabIndex = 21;
            this.TextBox1.TabStop = false;
            this.TextBox1.Text = "Client Wise Payment Detail";
            this.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnsubmit
            // 
            this.btnsubmit.BackColor = System.Drawing.Color.AntiqueWhite;
            this.btnsubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsubmit.Location = new System.Drawing.Point(634, 87);
            this.btnsubmit.Name = "btnsubmit";
            this.btnsubmit.Size = new System.Drawing.Size(97, 34);
            this.btnsubmit.TabIndex = 8;
            this.btnsubmit.Text = "Submit";
            this.btnsubmit.UseVisualStyleBackColor = false;
            this.btnsubmit.Click += new System.EventHandler(this.btnsubmit_Click);
            // 
            // btncancel
            // 
            this.btncancel.BackColor = System.Drawing.Color.AntiqueWhite;
            this.btncancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncancel.Location = new System.Drawing.Point(845, 87);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(97, 34);
            this.btncancel.TabIndex = 10;
            this.btncancel.Text = "Cancel";
            this.btncancel.UseVisualStyleBackColor = false;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Menu;
            this.groupBox1.Controls.Add(this.cmbstatus);
            this.groupBox1.Controls.Add(this.ss);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtrecamt);
            this.groupBox1.Controls.Add(this.dtpaymentdt);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.lblclientname);
            this.groupBox1.Controls.Add(this.btnedit);
            this.groupBox1.Controls.Add(this.txtbillno);
            this.groupBox1.Controls.Add(this.txtclientname);
            this.groupBox1.Controls.Add(this.lblpaystatus);
            this.groupBox1.Controls.Add(this.lblbillno);
            this.groupBox1.Controls.Add(this.btncancel);
            this.groupBox1.Controls.Add(this.btnsubmit);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.groupBox1.Location = new System.Drawing.Point(15, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1010, 135);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Client Payment";
            // 
            // cmbstatus
            // 
            this.cmbstatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbstatus.FormattingEnabled = true;
            this.cmbstatus.Items.AddRange(new object[] {
            "Select",
            "Pending",
            "Paid"});
            this.cmbstatus.Location = new System.Drawing.Point(136, 93);
            this.cmbstatus.Name = "cmbstatus";
            this.cmbstatus.Size = new System.Drawing.Size(156, 28);
            this.cmbstatus.TabIndex = 126;
            this.cmbstatus.SelectedIndexChanged += new System.EventHandler(this.cmbstatus_SelectedIndexChanged);
            // 
            // ss
            // 
            this.ss.AutoSize = true;
            this.ss.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ss.Location = new System.Drawing.Point(11, 100);
            this.ss.Name = "ss";
            this.ss.Size = new System.Drawing.Size(119, 16);
            this.ss.TabIndex = 125;
            this.ss.Text = "SELECT STATUS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(307, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 16);
            this.label2.TabIndex = 35;
            this.label2.Text = "Received Amount";
            // 
            // txtrecamt
            // 
            this.txtrecamt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtrecamt.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtrecamt.Location = new System.Drawing.Point(435, 92);
            this.txtrecamt.Name = "txtrecamt";
            this.txtrecamt.Size = new System.Drawing.Size(122, 31);
            this.txtrecamt.TabIndex = 7;
            // 
            // dtpaymentdt
            // 
            this.dtpaymentdt.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpaymentdt.Location = new System.Drawing.Point(744, 38);
            this.dtpaymentdt.Name = "dtpaymentdt";
            this.dtpaymentdt.Size = new System.Drawing.Size(200, 23);
            this.dtpaymentdt.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(741, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 16);
            this.label1.TabIndex = 32;
            this.label1.Text = "Select Payment Date";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdpending);
            this.panel1.Controls.Add(this.rdpaid);
            this.panel1.Location = new System.Drawing.Point(558, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(167, 45);
            this.panel1.TabIndex = 31;
            // 
            // rdpending
            // 
            this.rdpending.AutoSize = true;
            this.rdpending.Location = new System.Drawing.Point(76, 12);
            this.rdpending.Name = "rdpending";
            this.rdpending.Size = new System.Drawing.Size(78, 21);
            this.rdpending.TabIndex = 5;
            this.rdpending.TabStop = true;
            this.rdpending.Text = "Pending";
            this.rdpending.UseVisualStyleBackColor = true;
            // 
            // rdpaid
            // 
            this.rdpaid.AutoSize = true;
            this.rdpaid.Location = new System.Drawing.Point(16, 12);
            this.rdpaid.Name = "rdpaid";
            this.rdpaid.Size = new System.Drawing.Size(54, 21);
            this.rdpaid.TabIndex = 4;
            this.rdpaid.TabStop = true;
            this.rdpaid.Text = "Paid";
            this.rdpaid.UseVisualStyleBackColor = true;
            // 
            // lblclientname
            // 
            this.lblclientname.AutoSize = true;
            this.lblclientname.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblclientname.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblclientname.Location = new System.Drawing.Point(194, 21);
            this.lblclientname.Name = "lblclientname";
            this.lblclientname.Size = new System.Drawing.Size(86, 16);
            this.lblclientname.TabIndex = 25;
            this.lblclientname.Text = "Client Name";
            // 
            // btnedit
            // 
            this.btnedit.BackColor = System.Drawing.Color.AntiqueWhite;
            this.btnedit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnedit.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnedit.Location = new System.Drawing.Point(738, 87);
            this.btnedit.Name = "btnedit";
            this.btnedit.Size = new System.Drawing.Size(97, 34);
            this.btnedit.TabIndex = 9;
            this.btnedit.Text = "Edit";
            this.btnedit.UseVisualStyleBackColor = false;
            this.btnedit.Click += new System.EventHandler(this.btnedit_Click);
            // 
            // txtbillno
            // 
            this.txtbillno.BackColor = System.Drawing.Color.Bisque;
            this.txtbillno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbillno.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbillno.Location = new System.Drawing.Point(14, 40);
            this.txtbillno.Name = "txtbillno";
            this.txtbillno.Size = new System.Drawing.Size(176, 31);
            this.txtbillno.TabIndex = 2;
            this.txtbillno.Validated += new System.EventHandler(this.txtbillno_Validated);
            // 
            // txtclientname
            // 
            this.txtclientname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtclientname.Enabled = false;
            this.txtclientname.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtclientname.Location = new System.Drawing.Point(191, 40);
            this.txtclientname.Name = "txtclientname";
            this.txtclientname.Size = new System.Drawing.Size(366, 31);
            this.txtclientname.TabIndex = 3;
            // 
            // lblpaystatus
            // 
            this.lblpaystatus.AutoSize = true;
            this.lblpaystatus.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpaystatus.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblpaystatus.Location = new System.Drawing.Point(555, 19);
            this.lblpaystatus.Name = "lblpaystatus";
            this.lblpaystatus.Size = new System.Drawing.Size(114, 16);
            this.lblpaystatus.TabIndex = 20;
            this.lblpaystatus.Text = "Payment Status";
            // 
            // lblbillno
            // 
            this.lblbillno.AutoSize = true;
            this.lblbillno.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblbillno.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblbillno.Location = new System.Drawing.Point(17, 24);
            this.lblbillno.Name = "lblbillno";
            this.lblbillno.Size = new System.Drawing.Size(82, 16);
            this.lblbillno.TabIndex = 18;
            this.lblbillno.Text = "Billl Number";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txttotrec);
            this.groupBox2.Controls.Add(this.txttotbill);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.LVClientpaymentDetail);
            this.groupBox2.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.groupBox2.Location = new System.Drawing.Point(23, 209);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(988, 500);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Client Payment Detail";
            // 
            // txttotrec
            // 
            this.txttotrec.BackColor = System.Drawing.Color.Bisque;
            this.txttotrec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txttotrec.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttotrec.Location = new System.Drawing.Point(790, 430);
            this.txttotrec.Name = "txttotrec";
            this.txttotrec.Size = new System.Drawing.Size(176, 31);
            this.txttotrec.TabIndex = 21;
            this.txttotrec.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txttotbill
            // 
            this.txttotbill.BackColor = System.Drawing.Color.Bisque;
            this.txttotbill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txttotbill.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttotbill.Location = new System.Drawing.Point(611, 430);
            this.txttotbill.Name = "txttotbill";
            this.txttotbill.Size = new System.Drawing.Size(176, 31);
            this.txttotbill.TabIndex = 19;
            this.txttotbill.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(547, 438);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 16);
            this.label3.TabIndex = 20;
            this.label3.Text = "TOTAL:";
            // 
            // LVClientpaymentDetail
            // 
            this.LVClientpaymentDetail.BackColor = System.Drawing.Color.LightYellow;
            this.LVClientpaymentDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LVClientpaymentDetail.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LVClientpaymentDetail.ForeColor = System.Drawing.Color.Navy;
            this.LVClientpaymentDetail.FullRowSelect = true;
            this.LVClientpaymentDetail.GridLines = true;
            this.LVClientpaymentDetail.HideSelection = false;
            this.LVClientpaymentDetail.Location = new System.Drawing.Point(20, 29);
            this.LVClientpaymentDetail.MultiSelect = false;
            this.LVClientpaymentDetail.Name = "LVClientpaymentDetail";
            this.LVClientpaymentDetail.Size = new System.Drawing.Size(946, 395);
            this.LVClientpaymentDetail.TabIndex = 1;
            this.LVClientpaymentDetail.UseCompatibleStateImageBehavior = false;
            this.LVClientpaymentDetail.View = System.Windows.Forms.View.Details;
            this.LVClientpaymentDetail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LVClientpaymentDetail_KeyDown);
            this.LVClientpaymentDetail.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LVClientpaymentDetail_MouseDoubleClick);
            // 
            // PaymentDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 713);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.TextBox1);
            this.Name = "PaymentDetail";
            this.Text = "PaymentDetail";
            this.Load += new System.EventHandler(this.PaymentDetail_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox TextBox1;
        private System.Windows.Forms.Button btnsubmit;
        private System.Windows.Forms.Button btncancel;
        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.TextBox txtbillno;
        internal System.Windows.Forms.TextBox txtclientname;
        internal System.Windows.Forms.Label lblpaystatus;
        internal System.Windows.Forms.Label lblbillno;
        private System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.ListView LVClientpaymentDetail;
        private System.Windows.Forms.Button btnedit;
        internal System.Windows.Forms.Label lblclientname;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdpending;
        private System.Windows.Forms.RadioButton rdpaid;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpaymentdt;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox txtrecamt;
        internal System.Windows.Forms.TextBox txttotrec;
        internal System.Windows.Forms.TextBox txttotbill;
        internal System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbstatus;
        internal System.Windows.Forms.Label ss;
    }
}