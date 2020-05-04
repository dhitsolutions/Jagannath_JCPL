namespace RamdevSales
{
    partial class QPayment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QPayment));
            this.lblbankname = new System.Windows.Forms.Label();
            this.txtbankname = new System.Windows.Forms.TextBox();
            this.lblchqdate = new System.Windows.Forms.Label();
            this.txtchqdate = new System.Windows.Forms.TextBox();
            this.lblchqno = new System.Windows.Forms.Label();
            this.txtchqno = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txttotnet = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txttotdis = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txttotamt = new System.Windows.Forms.TextBox();
            this.btnprint = new System.Windows.Forms.Button();
            this.btndelete = new System.Windows.Forms.Button();
            this.LVFO = new System.Windows.Forms.ListView();
            this.btnsave = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtremark = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtnetamt = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtdiscount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtamt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbaccname = new System.Windows.Forms.ComboBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.txtrecno = new System.Windows.Forms.TextBox();
            this.cmbpaymode = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DTDate = new System.Windows.Forms.DateTimePicker();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnGroupEdit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblbillno = new System.Windows.Forms.Label();
            this.pnladditional = new System.Windows.Forms.Panel();
            this.lblf15 = new System.Windows.Forms.Label();
            this.txtf15 = new System.Windows.Forms.TextBox();
            this.lblf14 = new System.Windows.Forms.Label();
            this.txtf14 = new System.Windows.Forms.TextBox();
            this.lblf13 = new System.Windows.Forms.Label();
            this.txtf13 = new System.Windows.Forms.TextBox();
            this.lblf12 = new System.Windows.Forms.Label();
            this.txtf12 = new System.Windows.Forms.TextBox();
            this.lblf11 = new System.Windows.Forms.Label();
            this.txtf11 = new System.Windows.Forms.TextBox();
            this.lblf10 = new System.Windows.Forms.Label();
            this.txtf10 = new System.Windows.Forms.TextBox();
            this.lblf9 = new System.Windows.Forms.Label();
            this.txtf9 = new System.Windows.Forms.TextBox();
            this.lblf8 = new System.Windows.Forms.Label();
            this.txtf8 = new System.Windows.Forms.TextBox();
            this.lblf7 = new System.Windows.Forms.Label();
            this.txtf7 = new System.Windows.Forms.TextBox();
            this.lblf6 = new System.Windows.Forms.Label();
            this.txtf6 = new System.Windows.Forms.TextBox();
            this.lblf5 = new System.Windows.Forms.Label();
            this.txtf5 = new System.Windows.Forms.TextBox();
            this.lblf4 = new System.Windows.Forms.Label();
            this.txtf4 = new System.Windows.Forms.TextBox();
            this.lblf3 = new System.Windows.Forms.Label();
            this.txtf3 = new System.Windows.Forms.TextBox();
            this.lblf2 = new System.Windows.Forms.Label();
            this.txtf2 = new System.Windows.Forms.TextBox();
            this.lblf1 = new System.Windows.Forms.Label();
            this.txtf1 = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.pnladditional.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblbankname
            // 
            this.lblbankname.AutoSize = true;
            this.lblbankname.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblbankname.ForeColor = System.Drawing.Color.Black;
            this.lblbankname.Location = new System.Drawing.Point(719, 37);
            this.lblbankname.Name = "lblbankname";
            this.lblbankname.Size = new System.Drawing.Size(90, 16);
            this.lblbankname.TabIndex = 251;
            this.lblbankname.Text = "Bank Name";
            // 
            // txtbankname
            // 
            this.txtbankname.BackColor = System.Drawing.Color.White;
            this.txtbankname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbankname.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbankname.ForeColor = System.Drawing.Color.Black;
            this.txtbankname.Location = new System.Drawing.Point(717, 56);
            this.txtbankname.Name = "txtbankname";
            this.txtbankname.Size = new System.Drawing.Size(175, 24);
            this.txtbankname.TabIndex = 11;
            this.txtbankname.Enter += new System.EventHandler(this.txtbankname_Enter);
            this.txtbankname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtbankname_KeyDown);
            this.txtbankname.Leave += new System.EventHandler(this.txtbankname_Leave);
            // 
            // lblchqdate
            // 
            this.lblchqdate.AutoSize = true;
            this.lblchqdate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblchqdate.ForeColor = System.Drawing.Color.Black;
            this.lblchqdate.Location = new System.Drawing.Point(580, 37);
            this.lblchqdate.Name = "lblchqdate";
            this.lblchqdate.Size = new System.Drawing.Size(136, 16);
            this.lblchqdate.TabIndex = 249;
            this.lblchqdate.Text = "Cheque/Txn Date";
            // 
            // txtchqdate
            // 
            this.txtchqdate.BackColor = System.Drawing.Color.White;
            this.txtchqdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtchqdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtchqdate.ForeColor = System.Drawing.Color.Black;
            this.txtchqdate.Location = new System.Drawing.Point(579, 56);
            this.txtchqdate.Name = "txtchqdate";
            this.txtchqdate.Size = new System.Drawing.Size(137, 24);
            this.txtchqdate.TabIndex = 10;
            this.txtchqdate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtchqdate.Enter += new System.EventHandler(this.txtchqdate_Enter);
            this.txtchqdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtchqdate_KeyDown);
            this.txtchqdate.Leave += new System.EventHandler(this.txtchqdate_Leave);
            // 
            // lblchqno
            // 
            this.lblchqno.AutoSize = true;
            this.lblchqno.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblchqno.ForeColor = System.Drawing.Color.Black;
            this.lblchqno.Location = new System.Drawing.Point(458, 37);
            this.lblchqno.Name = "lblchqno";
            this.lblchqno.Size = new System.Drawing.Size(126, 16);
            this.lblchqno.TabIndex = 247;
            this.lblchqno.Text = "Cheque/Txn No.";
            // 
            // txtchqno
            // 
            this.txtchqno.BackColor = System.Drawing.Color.White;
            this.txtchqno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtchqno.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtchqno.ForeColor = System.Drawing.Color.Black;
            this.txtchqno.Location = new System.Drawing.Point(461, 56);
            this.txtchqno.Name = "txtchqno";
            this.txtchqno.Size = new System.Drawing.Size(117, 24);
            this.txtchqno.TabIndex = 9;
            this.txtchqno.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtchqno.Enter += new System.EventHandler(this.txtchqno_Enter);
            this.txtchqno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtchqno_KeyDown);
            this.txtchqno.Leave += new System.EventHandler(this.txtchqno_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(960, 521);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(134, 16);
            this.label9.TabIndex = 245;
            this.label9.Text = "Total Net Amount";
            // 
            // txttotnet
            // 
            this.txttotnet.BackColor = System.Drawing.Color.White;
            this.txttotnet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txttotnet.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.txttotnet.ForeColor = System.Drawing.Color.Black;
            this.txttotnet.Location = new System.Drawing.Point(955, 540);
            this.txttotnet.Name = "txttotnet";
            this.txttotnet.ReadOnly = true;
            this.txttotnet.Size = new System.Drawing.Size(146, 29);
            this.txttotnet.TabIndex = 246;
            this.txttotnet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(800, 521);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(150, 16);
            this.label10.TabIndex = 243;
            this.label10.Text = "Total Discount Amt.";
            // 
            // txttotdis
            // 
            this.txttotdis.BackColor = System.Drawing.Color.White;
            this.txttotdis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txttotdis.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.txttotdis.ForeColor = System.Drawing.Color.Black;
            this.txttotdis.Location = new System.Drawing.Point(798, 540);
            this.txttotdis.Name = "txttotdis";
            this.txttotdis.ReadOnly = true;
            this.txttotdis.Size = new System.Drawing.Size(156, 29);
            this.txttotdis.TabIndex = 244;
            this.txttotdis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(687, 521);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(105, 16);
            this.label11.TabIndex = 241;
            this.label11.Text = "Total Amount";
            // 
            // txttotamt
            // 
            this.txttotamt.BackColor = System.Drawing.Color.White;
            this.txttotamt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txttotamt.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.txttotamt.ForeColor = System.Drawing.Color.Black;
            this.txttotamt.Location = new System.Drawing.Point(684, 540);
            this.txttotamt.Name = "txttotamt";
            this.txttotamt.ReadOnly = true;
            this.txttotamt.Size = new System.Drawing.Size(113, 29);
            this.txttotamt.TabIndex = 242;
            this.txttotamt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprint.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(899, 64);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(97, 34);
            this.btnprint.TabIndex = 14;
            this.btnprint.Text = "&Print";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            this.btnprint.Enter += new System.EventHandler(this.btnprint_Enter);
            this.btnprint.Leave += new System.EventHandler(this.btnprint_Leave);
            this.btnprint.MouseEnter += new System.EventHandler(this.btnprint_MouseEnter);
            this.btnprint.MouseLeave += new System.EventHandler(this.btnprint_MouseLeave);
            // 
            // btndelete
            // 
            this.btndelete.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btndelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndelete.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndelete.ForeColor = System.Drawing.Color.White;
            this.btndelete.Location = new System.Drawing.Point(999, 64);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(97, 34);
            this.btndelete.TabIndex = 15;
            this.btndelete.Text = "&Delete";
            this.btndelete.UseVisualStyleBackColor = false;
            this.btndelete.Click += new System.EventHandler(this.btndelete_Click);
            this.btndelete.Enter += new System.EventHandler(this.btndelete_Enter);
            this.btndelete.Leave += new System.EventHandler(this.btndelete_Leave);
            this.btndelete.MouseEnter += new System.EventHandler(this.btndelete_MouseEnter);
            this.btndelete.MouseLeave += new System.EventHandler(this.btndelete_MouseLeave);
            // 
            // LVFO
            // 
            this.LVFO.BackColor = System.Drawing.SystemColors.Window;
            this.LVFO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LVFO.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LVFO.ForeColor = System.Drawing.Color.Navy;
            this.LVFO.FullRowSelect = true;
            this.LVFO.GridLines = true;
            this.LVFO.HideSelection = false;
            this.LVFO.Location = new System.Drawing.Point(5, 111);
            this.LVFO.MultiSelect = false;
            this.LVFO.Name = "LVFO";
            this.LVFO.Size = new System.Drawing.Size(1088, 375);
            this.LVFO.TabIndex = 16;
            this.LVFO.UseCompatibleStateImageBehavior = false;
            this.LVFO.View = System.Windows.Forms.View.Details;
            this.LVFO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LVFO_KeyDown);
            this.LVFO.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LVFO_MouseDoubleClick);
            // 
            // btnsave
            // 
            this.btnsave.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnsave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsave.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsave.ForeColor = System.Drawing.Color.White;
            this.btnsave.Location = new System.Drawing.Point(899, 102);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(97, 34);
            this.btnsave.TabIndex = 12;
            this.btnsave.Text = "&Submit";
            this.btnsave.UseVisualStyleBackColor = false;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            this.btnsave.Enter += new System.EventHandler(this.btnsave_Enter);
            this.btnsave.Leave += new System.EventHandler(this.btnsave_Leave);
            this.btnsave.MouseEnter += new System.EventHandler(this.btnsave_MouseEnter);
            this.btnsave.MouseLeave += new System.EventHandler(this.btnsave_MouseLeave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(792, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 16);
            this.label8.TabIndex = 235;
            this.label8.Text = "Remarks";
            // 
            // txtremark
            // 
            this.txtremark.BackColor = System.Drawing.Color.White;
            this.txtremark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtremark.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtremark.ForeColor = System.Drawing.Color.Black;
            this.txtremark.Location = new System.Drawing.Point(725, 107);
            this.txtremark.Name = "txtremark";
            this.txtremark.Size = new System.Drawing.Size(167, 24);
            this.txtremark.TabIndex = 8;
            this.txtremark.Enter += new System.EventHandler(this.txtremark_Enter);
            this.txtremark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtremark_KeyDown);
            this.txtremark.Leave += new System.EventHandler(this.txtremark_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(624, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 16);
            this.label7.TabIndex = 233;
            this.label7.Text = "Net Amount";
            // 
            // txtnetamt
            // 
            this.txtnetamt.BackColor = System.Drawing.Color.White;
            this.txtnetamt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtnetamt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnetamt.ForeColor = System.Drawing.Color.Black;
            this.txtnetamt.Location = new System.Drawing.Point(611, 107);
            this.txtnetamt.Name = "txtnetamt";
            this.txtnetamt.Size = new System.Drawing.Size(114, 24);
            this.txtnetamt.TabIndex = 7;
            this.txtnetamt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtnetamt.Enter += new System.EventHandler(this.txtnetamt_Enter);
            this.txtnetamt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtnetamt_KeyDown);
            this.txtnetamt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtnetamt_KeyPress);
            this.txtnetamt.Leave += new System.EventHandler(this.txtnetamt_Leave);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.White;
            this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(373, 106);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(22, 22);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.TabStop = false;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnAdd.Enter += new System.EventHandler(this.btnAdd_Enter);
            this.btnAdd.Leave += new System.EventHandler(this.btnAdd_Leave);
            this.btnAdd.MouseEnter += new System.EventHandler(this.btnAdd_MouseEnter);
            this.btnAdd.MouseLeave += new System.EventHandler(this.btnAdd_MouseLeave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(520, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 16);
            this.label6.TabIndex = 230;
            this.label6.Text = "Discount Amt.";
            // 
            // txtdiscount
            // 
            this.txtdiscount.BackColor = System.Drawing.Color.White;
            this.txtdiscount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtdiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdiscount.ForeColor = System.Drawing.Color.Black;
            this.txtdiscount.Location = new System.Drawing.Point(522, 107);
            this.txtdiscount.Name = "txtdiscount";
            this.txtdiscount.Size = new System.Drawing.Size(89, 24);
            this.txtdiscount.TabIndex = 6;
            this.txtdiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtdiscount.TextChanged += new System.EventHandler(this.txtdiscount_TextChanged);
            this.txtdiscount.Enter += new System.EventHandler(this.txtdiscount_Enter);
            this.txtdiscount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtdiscount_KeyDown);
            this.txtdiscount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtdiscount_KeyPress);
            this.txtdiscount.Leave += new System.EventHandler(this.txtdiscount_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(421, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 16);
            this.label4.TabIndex = 228;
            this.label4.Text = "Total Amount";
            // 
            // txtamt
            // 
            this.txtamt.BackColor = System.Drawing.Color.White;
            this.txtamt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtamt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtamt.ForeColor = System.Drawing.Color.Black;
            this.txtamt.Location = new System.Drawing.Point(425, 107);
            this.txtamt.Name = "txtamt";
            this.txtamt.Size = new System.Drawing.Size(97, 24);
            this.txtamt.TabIndex = 5;
            this.txtamt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtamt.TextChanged += new System.EventHandler(this.txtamt_TextChanged);
            this.txtamt.Enter += new System.EventHandler(this.txtamt_Enter);
            this.txtamt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtamt_KeyDown);
            this.txtamt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtamt_KeyPress);
            this.txtamt.Leave += new System.EventHandler(this.txtamt_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(121, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 16);
            this.label3.TabIndex = 227;
            this.label3.Text = "Account Name";
            // 
            // cmbaccname
            // 
            this.cmbaccname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbaccname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbaccname.BackColor = System.Drawing.SystemColors.Window;
            this.cmbaccname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbaccname.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbaccname.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbaccname.FormattingEnabled = true;
            this.cmbaccname.Location = new System.Drawing.Point(102, 105);
            this.cmbaccname.Name = "cmbaccname";
            this.cmbaccname.Size = new System.Drawing.Size(265, 24);
            this.cmbaccname.TabIndex = 3;
            this.cmbaccname.Enter += new System.EventHandler(this.cmbaccname_Enter);
            this.cmbaccname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbaccname_KeyDown);
            this.cmbaccname.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbaccname_KeyUp);
            this.cmbaccname.Leave += new System.EventHandler(this.cmbaccname_Leave);
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.ForeColor = System.Drawing.Color.Black;
            this.Label5.Location = new System.Drawing.Point(10, 87);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(90, 16);
            this.Label5.TabIndex = 224;
            this.Label5.Text = "Receipt No.";
            // 
            // txtrecno
            // 
            this.txtrecno.BackColor = System.Drawing.Color.White;
            this.txtrecno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtrecno.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtrecno.ForeColor = System.Drawing.Color.Black;
            this.txtrecno.Location = new System.Drawing.Point(6, 105);
            this.txtrecno.Name = "txtrecno";
            this.txtrecno.Size = new System.Drawing.Size(95, 24);
            this.txtrecno.TabIndex = 2;
            this.txtrecno.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtrecno.Enter += new System.EventHandler(this.txtrecno_Enter);
            this.txtrecno.Leave += new System.EventHandler(this.txtrecno_Leave);
            // 
            // cmbpaymode
            // 
            this.cmbpaymode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbpaymode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbpaymode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbpaymode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbpaymode.FormattingEnabled = true;
            this.cmbpaymode.Items.AddRange(new object[] {
            "Cash",
            "Cheque",
            "Netbanking"});
            this.cmbpaymode.Location = new System.Drawing.Point(289, 55);
            this.cmbpaymode.Name = "cmbpaymode";
            this.cmbpaymode.Size = new System.Drawing.Size(166, 24);
            this.cmbpaymode.TabIndex = 1;
            this.cmbpaymode.SelectedIndexChanged += new System.EventHandler(this.cmbpaymode_SelectedIndexChanged);
            this.cmbpaymode.Enter += new System.EventHandler(this.cmbpaymode_Enter);
            this.cmbpaymode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbpaymode_KeyDown);
            this.cmbpaymode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbpaymode_KeyUp);
            this.cmbpaymode.Leave += new System.EventHandler(this.cmbpaymode_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(214, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 32);
            this.label2.TabIndex = 222;
            this.label2.Text = "Payment \r\nMode:";
            // 
            // DTDate
            // 
            this.DTDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTDate.Location = new System.Drawing.Point(55, 56);
            this.DTDate.Name = "DTDate";
            this.DTDate.Size = new System.Drawing.Size(156, 20);
            this.DTDate.TabIndex = 0;
            this.DTDate.ValueChanged += new System.EventHandler(this.DTDate_ValueChanged);
            this.DTDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTDate_KeyDown);
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.Highlight;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox7.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.ForeColor = System.Drawing.Color.White;
            this.textBox7.Location = new System.Drawing.Point(0, 0);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(1105, 31);
            this.textBox7.TabIndex = 220;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "QUICK PAYMENT";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(7, 58);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(47, 16);
            this.Label1.TabIndex = 253;
            this.Label1.Text = "Date:";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(999, 103);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(97, 34);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.Enter += new System.EventHandler(this.btnClose_Enter);
            this.btnClose.Leave += new System.EventHandler(this.btnClose_Leave);
            this.btnClose.MouseEnter += new System.EventHandler(this.btnClose_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnGroupEdit
            // 
            this.btnGroupEdit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGroupEdit.BackgroundImage")));
            this.btnGroupEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGroupEdit.ForeColor = System.Drawing.Color.White;
            this.btnGroupEdit.Location = new System.Drawing.Point(393, 105);
            this.btnGroupEdit.Name = "btnGroupEdit";
            this.btnGroupEdit.Size = new System.Drawing.Size(23, 22);
            this.btnGroupEdit.TabIndex = 254;
            this.btnGroupEdit.TabStop = false;
            this.btnGroupEdit.UseVisualStyleBackColor = true;
            this.btnGroupEdit.Click += new System.EventHandler(this.btnGroupEdit_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pnladditional);
            this.panel1.Controls.Add(this.LVFO);
            this.panel1.Controls.Add(this.lblbillno);
            this.panel1.Location = new System.Drawing.Point(0, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1105, 543);
            this.panel1.TabIndex = 255;
            // 
            // lblbillno
            // 
            this.lblbillno.AutoSize = true;
            this.lblbillno.Location = new System.Drawing.Point(898, 13);
            this.lblbillno.Name = "lblbillno";
            this.lblbillno.Size = new System.Drawing.Size(0, 13);
            this.lblbillno.TabIndex = 0;
            this.lblbillno.Visible = false;
            // 
            // pnladditional
            // 
            this.pnladditional.BackColor = System.Drawing.Color.LightYellow;
            this.pnladditional.Controls.Add(this.lblf15);
            this.pnladditional.Controls.Add(this.txtf15);
            this.pnladditional.Controls.Add(this.lblf14);
            this.pnladditional.Controls.Add(this.txtf14);
            this.pnladditional.Controls.Add(this.lblf13);
            this.pnladditional.Controls.Add(this.txtf13);
            this.pnladditional.Controls.Add(this.lblf12);
            this.pnladditional.Controls.Add(this.txtf12);
            this.pnladditional.Controls.Add(this.lblf11);
            this.pnladditional.Controls.Add(this.txtf11);
            this.pnladditional.Controls.Add(this.lblf10);
            this.pnladditional.Controls.Add(this.txtf10);
            this.pnladditional.Controls.Add(this.lblf9);
            this.pnladditional.Controls.Add(this.txtf9);
            this.pnladditional.Controls.Add(this.lblf8);
            this.pnladditional.Controls.Add(this.txtf8);
            this.pnladditional.Controls.Add(this.lblf7);
            this.pnladditional.Controls.Add(this.txtf7);
            this.pnladditional.Controls.Add(this.lblf6);
            this.pnladditional.Controls.Add(this.txtf6);
            this.pnladditional.Controls.Add(this.lblf5);
            this.pnladditional.Controls.Add(this.txtf5);
            this.pnladditional.Controls.Add(this.lblf4);
            this.pnladditional.Controls.Add(this.txtf4);
            this.pnladditional.Controls.Add(this.lblf3);
            this.pnladditional.Controls.Add(this.txtf3);
            this.pnladditional.Controls.Add(this.lblf2);
            this.pnladditional.Controls.Add(this.txtf2);
            this.pnladditional.Controls.Add(this.lblf1);
            this.pnladditional.Controls.Add(this.txtf1);
            this.pnladditional.Location = new System.Drawing.Point(9, 129);
            this.pnladditional.Name = "pnladditional";
            this.pnladditional.Size = new System.Drawing.Size(1082, 152);
            this.pnladditional.TabIndex = 1;
            this.pnladditional.Visible = false;
            // 
            // lblf15
            // 
            this.lblf15.AutoSize = true;
            this.lblf15.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblf15.ForeColor = System.Drawing.Color.Black;
            this.lblf15.Location = new System.Drawing.Point(425, 106);
            this.lblf15.Name = "lblf15";
            this.lblf15.Size = new System.Drawing.Size(48, 16);
            this.lblf15.TabIndex = 285;
            this.lblf15.Text = "lblf15";
            this.lblf15.Visible = false;
            // 
            // txtf15
            // 
            this.txtf15.BackColor = System.Drawing.Color.White;
            this.txtf15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtf15.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtf15.ForeColor = System.Drawing.Color.Black;
            this.txtf15.Location = new System.Drawing.Point(364, 125);
            this.txtf15.Name = "txtf15";
            this.txtf15.Size = new System.Drawing.Size(167, 24);
            this.txtf15.TabIndex = 284;
            this.txtf15.Visible = false;
            this.txtf15.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtf15_KeyDown);
            // 
            // lblf14
            // 
            this.lblf14.AutoSize = true;
            this.lblf14.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblf14.ForeColor = System.Drawing.Color.Black;
            this.lblf14.Location = new System.Drawing.Point(252, 106);
            this.lblf14.Name = "lblf14";
            this.lblf14.Size = new System.Drawing.Size(48, 16);
            this.lblf14.TabIndex = 283;
            this.lblf14.Text = "lblf14";
            this.lblf14.Visible = false;
            // 
            // txtf14
            // 
            this.txtf14.BackColor = System.Drawing.Color.White;
            this.txtf14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtf14.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtf14.ForeColor = System.Drawing.Color.Black;
            this.txtf14.Location = new System.Drawing.Point(191, 125);
            this.txtf14.Name = "txtf14";
            this.txtf14.Size = new System.Drawing.Size(167, 24);
            this.txtf14.TabIndex = 282;
            this.txtf14.Visible = false;
            this.txtf14.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtf14_KeyDown);
            // 
            // lblf13
            // 
            this.lblf13.AutoSize = true;
            this.lblf13.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblf13.ForeColor = System.Drawing.Color.Black;
            this.lblf13.Location = new System.Drawing.Point(79, 106);
            this.lblf13.Name = "lblf13";
            this.lblf13.Size = new System.Drawing.Size(48, 16);
            this.lblf13.TabIndex = 281;
            this.lblf13.Text = "lblf13";
            this.lblf13.Visible = false;
            // 
            // txtf13
            // 
            this.txtf13.BackColor = System.Drawing.Color.White;
            this.txtf13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtf13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtf13.ForeColor = System.Drawing.Color.Black;
            this.txtf13.Location = new System.Drawing.Point(18, 125);
            this.txtf13.Name = "txtf13";
            this.txtf13.Size = new System.Drawing.Size(167, 24);
            this.txtf13.TabIndex = 280;
            this.txtf13.Visible = false;
            this.txtf13.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtf13_KeyDown);
            // 
            // lblf12
            // 
            this.lblf12.AutoSize = true;
            this.lblf12.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblf12.ForeColor = System.Drawing.Color.Black;
            this.lblf12.Location = new System.Drawing.Point(943, 56);
            this.lblf12.Name = "lblf12";
            this.lblf12.Size = new System.Drawing.Size(48, 16);
            this.lblf12.TabIndex = 279;
            this.lblf12.Text = "lblf12";
            this.lblf12.Visible = false;
            // 
            // txtf12
            // 
            this.txtf12.BackColor = System.Drawing.Color.White;
            this.txtf12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtf12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtf12.ForeColor = System.Drawing.Color.Black;
            this.txtf12.Location = new System.Drawing.Point(882, 75);
            this.txtf12.Name = "txtf12";
            this.txtf12.Size = new System.Drawing.Size(167, 24);
            this.txtf12.TabIndex = 278;
            this.txtf12.Visible = false;
            this.txtf12.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtf12_KeyDown);
            // 
            // lblf11
            // 
            this.lblf11.AutoSize = true;
            this.lblf11.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblf11.ForeColor = System.Drawing.Color.Black;
            this.lblf11.Location = new System.Drawing.Point(770, 56);
            this.lblf11.Name = "lblf11";
            this.lblf11.Size = new System.Drawing.Size(48, 16);
            this.lblf11.TabIndex = 277;
            this.lblf11.Text = "lblf11";
            this.lblf11.Visible = false;
            // 
            // txtf11
            // 
            this.txtf11.BackColor = System.Drawing.Color.White;
            this.txtf11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtf11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtf11.ForeColor = System.Drawing.Color.Black;
            this.txtf11.Location = new System.Drawing.Point(709, 75);
            this.txtf11.Name = "txtf11";
            this.txtf11.Size = new System.Drawing.Size(167, 24);
            this.txtf11.TabIndex = 276;
            this.txtf11.Visible = false;
            this.txtf11.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtf11_KeyDown);
            // 
            // lblf10
            // 
            this.lblf10.AutoSize = true;
            this.lblf10.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblf10.ForeColor = System.Drawing.Color.Black;
            this.lblf10.Location = new System.Drawing.Point(597, 56);
            this.lblf10.Name = "lblf10";
            this.lblf10.Size = new System.Drawing.Size(48, 16);
            this.lblf10.TabIndex = 275;
            this.lblf10.Text = "lblf10";
            this.lblf10.Visible = false;
            // 
            // txtf10
            // 
            this.txtf10.BackColor = System.Drawing.Color.White;
            this.txtf10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtf10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtf10.ForeColor = System.Drawing.Color.Black;
            this.txtf10.Location = new System.Drawing.Point(536, 75);
            this.txtf10.Name = "txtf10";
            this.txtf10.Size = new System.Drawing.Size(167, 24);
            this.txtf10.TabIndex = 274;
            this.txtf10.Visible = false;
            this.txtf10.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtf10_KeyDown);
            // 
            // lblf9
            // 
            this.lblf9.AutoSize = true;
            this.lblf9.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblf9.ForeColor = System.Drawing.Color.Black;
            this.lblf9.Location = new System.Drawing.Point(425, 56);
            this.lblf9.Name = "lblf9";
            this.lblf9.Size = new System.Drawing.Size(39, 16);
            this.lblf9.TabIndex = 273;
            this.lblf9.Text = "lblf9";
            this.lblf9.Visible = false;
            // 
            // txtf9
            // 
            this.txtf9.BackColor = System.Drawing.Color.White;
            this.txtf9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtf9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtf9.ForeColor = System.Drawing.Color.Black;
            this.txtf9.Location = new System.Drawing.Point(364, 75);
            this.txtf9.Name = "txtf9";
            this.txtf9.Size = new System.Drawing.Size(167, 24);
            this.txtf9.TabIndex = 272;
            this.txtf9.Visible = false;
            this.txtf9.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtf9_KeyDown);
            // 
            // lblf8
            // 
            this.lblf8.AutoSize = true;
            this.lblf8.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblf8.ForeColor = System.Drawing.Color.Black;
            this.lblf8.Location = new System.Drawing.Point(252, 56);
            this.lblf8.Name = "lblf8";
            this.lblf8.Size = new System.Drawing.Size(39, 16);
            this.lblf8.TabIndex = 271;
            this.lblf8.Text = "lblf8";
            this.lblf8.Visible = false;
            // 
            // txtf8
            // 
            this.txtf8.BackColor = System.Drawing.Color.White;
            this.txtf8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtf8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtf8.ForeColor = System.Drawing.Color.Black;
            this.txtf8.Location = new System.Drawing.Point(191, 75);
            this.txtf8.Name = "txtf8";
            this.txtf8.Size = new System.Drawing.Size(167, 24);
            this.txtf8.TabIndex = 270;
            this.txtf8.Visible = false;
            this.txtf8.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtf8_KeyDown);
            // 
            // lblf7
            // 
            this.lblf7.AutoSize = true;
            this.lblf7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblf7.ForeColor = System.Drawing.Color.Black;
            this.lblf7.Location = new System.Drawing.Point(79, 56);
            this.lblf7.Name = "lblf7";
            this.lblf7.Size = new System.Drawing.Size(39, 16);
            this.lblf7.TabIndex = 269;
            this.lblf7.Text = "lblf7";
            this.lblf7.Visible = false;
            // 
            // txtf7
            // 
            this.txtf7.BackColor = System.Drawing.Color.White;
            this.txtf7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtf7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtf7.ForeColor = System.Drawing.Color.Black;
            this.txtf7.Location = new System.Drawing.Point(18, 75);
            this.txtf7.Name = "txtf7";
            this.txtf7.Size = new System.Drawing.Size(167, 24);
            this.txtf7.TabIndex = 268;
            this.txtf7.Visible = false;
            this.txtf7.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtf7_KeyDown);
            // 
            // lblf6
            // 
            this.lblf6.AutoSize = true;
            this.lblf6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblf6.ForeColor = System.Drawing.Color.Black;
            this.lblf6.Location = new System.Drawing.Point(943, 9);
            this.lblf6.Name = "lblf6";
            this.lblf6.Size = new System.Drawing.Size(39, 16);
            this.lblf6.TabIndex = 267;
            this.lblf6.Text = "lblf6";
            this.lblf6.Visible = false;
            // 
            // txtf6
            // 
            this.txtf6.BackColor = System.Drawing.Color.White;
            this.txtf6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtf6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtf6.ForeColor = System.Drawing.Color.Black;
            this.txtf6.Location = new System.Drawing.Point(882, 28);
            this.txtf6.Name = "txtf6";
            this.txtf6.Size = new System.Drawing.Size(167, 24);
            this.txtf6.TabIndex = 266;
            this.txtf6.Visible = false;
            this.txtf6.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtf6_KeyDown);
            // 
            // lblf5
            // 
            this.lblf5.AutoSize = true;
            this.lblf5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblf5.ForeColor = System.Drawing.Color.Black;
            this.lblf5.Location = new System.Drawing.Point(770, 9);
            this.lblf5.Name = "lblf5";
            this.lblf5.Size = new System.Drawing.Size(39, 16);
            this.lblf5.TabIndex = 265;
            this.lblf5.Text = "lblf5";
            this.lblf5.Visible = false;
            // 
            // txtf5
            // 
            this.txtf5.BackColor = System.Drawing.Color.White;
            this.txtf5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtf5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtf5.ForeColor = System.Drawing.Color.Black;
            this.txtf5.Location = new System.Drawing.Point(709, 28);
            this.txtf5.Name = "txtf5";
            this.txtf5.Size = new System.Drawing.Size(167, 24);
            this.txtf5.TabIndex = 264;
            this.txtf5.Visible = false;
            this.txtf5.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtf5_KeyDown);
            // 
            // lblf4
            // 
            this.lblf4.AutoSize = true;
            this.lblf4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblf4.ForeColor = System.Drawing.Color.Black;
            this.lblf4.Location = new System.Drawing.Point(597, 9);
            this.lblf4.Name = "lblf4";
            this.lblf4.Size = new System.Drawing.Size(39, 16);
            this.lblf4.TabIndex = 263;
            this.lblf4.Text = "lblf4";
            this.lblf4.Visible = false;
            // 
            // txtf4
            // 
            this.txtf4.BackColor = System.Drawing.Color.White;
            this.txtf4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtf4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtf4.ForeColor = System.Drawing.Color.Black;
            this.txtf4.Location = new System.Drawing.Point(536, 28);
            this.txtf4.Name = "txtf4";
            this.txtf4.Size = new System.Drawing.Size(167, 24);
            this.txtf4.TabIndex = 262;
            this.txtf4.Visible = false;
            this.txtf4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtf4_KeyDown);
            // 
            // lblf3
            // 
            this.lblf3.AutoSize = true;
            this.lblf3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblf3.ForeColor = System.Drawing.Color.Black;
            this.lblf3.Location = new System.Drawing.Point(425, 9);
            this.lblf3.Name = "lblf3";
            this.lblf3.Size = new System.Drawing.Size(39, 16);
            this.lblf3.TabIndex = 261;
            this.lblf3.Text = "lblf3";
            this.lblf3.Visible = false;
            // 
            // txtf3
            // 
            this.txtf3.BackColor = System.Drawing.Color.White;
            this.txtf3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtf3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtf3.ForeColor = System.Drawing.Color.Black;
            this.txtf3.Location = new System.Drawing.Point(364, 28);
            this.txtf3.Name = "txtf3";
            this.txtf3.Size = new System.Drawing.Size(167, 24);
            this.txtf3.TabIndex = 260;
            this.txtf3.Visible = false;
            this.txtf3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtf3_KeyDown);
            // 
            // lblf2
            // 
            this.lblf2.AutoSize = true;
            this.lblf2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblf2.ForeColor = System.Drawing.Color.Black;
            this.lblf2.Location = new System.Drawing.Point(252, 9);
            this.lblf2.Name = "lblf2";
            this.lblf2.Size = new System.Drawing.Size(39, 16);
            this.lblf2.TabIndex = 259;
            this.lblf2.Text = "lblf2";
            this.lblf2.Visible = false;
            // 
            // txtf2
            // 
            this.txtf2.BackColor = System.Drawing.Color.White;
            this.txtf2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtf2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtf2.ForeColor = System.Drawing.Color.Black;
            this.txtf2.Location = new System.Drawing.Point(191, 28);
            this.txtf2.Name = "txtf2";
            this.txtf2.Size = new System.Drawing.Size(167, 24);
            this.txtf2.TabIndex = 258;
            this.txtf2.Visible = false;
            this.txtf2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtf2_KeyDown);
            // 
            // lblf1
            // 
            this.lblf1.AutoSize = true;
            this.lblf1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblf1.ForeColor = System.Drawing.Color.Black;
            this.lblf1.Location = new System.Drawing.Point(79, 9);
            this.lblf1.Name = "lblf1";
            this.lblf1.Size = new System.Drawing.Size(39, 16);
            this.lblf1.TabIndex = 257;
            this.lblf1.Text = "lblf1";
            this.lblf1.Visible = false;
            // 
            // txtf1
            // 
            this.txtf1.BackColor = System.Drawing.Color.White;
            this.txtf1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtf1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtf1.ForeColor = System.Drawing.Color.Black;
            this.txtf1.Location = new System.Drawing.Point(18, 28);
            this.txtf1.Name = "txtf1";
            this.txtf1.Size = new System.Drawing.Size(167, 24);
            this.txtf1.TabIndex = 256;
            this.txtf1.Visible = false;
            this.txtf1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtf1_KeyDown);
            // 
            // QPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1109, 586);
            this.ControlBox = false;
            this.Controls.Add(this.btnGroupEdit);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.lblbankname);
            this.Controls.Add(this.txtbankname);
            this.Controls.Add(this.lblchqdate);
            this.Controls.Add(this.txtchqdate);
            this.Controls.Add(this.lblchqno);
            this.Controls.Add(this.txtchqno);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txttotnet);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txttotdis);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txttotamt);
            this.Controls.Add(this.btnprint);
            this.Controls.Add(this.btndelete);
            this.Controls.Add(this.btnsave);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtremark);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtnetamt);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtdiscount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtamt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbaccname);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.txtrecno);
            this.Controls.Add(this.cmbpaymode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DTDate);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "QPayment";
            this.Text = "QPayment";
            this.Load += new System.EventHandler(this.QPayment_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnladditional.ResumeLayout(false);
            this.pnladditional.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lblbankname;
        internal System.Windows.Forms.TextBox txtbankname;
        internal System.Windows.Forms.Label lblchqdate;
        internal System.Windows.Forms.TextBox txtchqdate;
        internal System.Windows.Forms.Label lblchqno;
        internal System.Windows.Forms.TextBox txtchqno;
        internal System.Windows.Forms.Label label9;
        internal System.Windows.Forms.TextBox txttotnet;
        internal System.Windows.Forms.Label label10;
        internal System.Windows.Forms.TextBox txttotdis;
        internal System.Windows.Forms.Label label11;
        internal System.Windows.Forms.TextBox txttotamt;
        internal System.Windows.Forms.Button btnprint;
        internal System.Windows.Forms.Button btndelete;
        internal System.Windows.Forms.ListView LVFO;
        internal System.Windows.Forms.Button btnsave;
        internal System.Windows.Forms.Label label8;
        internal System.Windows.Forms.TextBox txtremark;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.TextBox txtnetamt;
        internal System.Windows.Forms.Button btnAdd;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.TextBox txtdiscount;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox txtamt;
        internal System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbaccname;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.TextBox txtrecno;
        private System.Windows.Forms.ComboBox cmbpaymode;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.DateTimePicker DTDate;
        internal System.Windows.Forms.TextBox textBox7;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnGroupEdit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblbillno;
        private System.Windows.Forms.Panel pnladditional;
        internal System.Windows.Forms.Label lblf15;
        internal System.Windows.Forms.TextBox txtf15;
        internal System.Windows.Forms.Label lblf14;
        internal System.Windows.Forms.TextBox txtf14;
        internal System.Windows.Forms.Label lblf13;
        internal System.Windows.Forms.TextBox txtf13;
        internal System.Windows.Forms.Label lblf12;
        internal System.Windows.Forms.TextBox txtf12;
        internal System.Windows.Forms.Label lblf11;
        internal System.Windows.Forms.TextBox txtf11;
        internal System.Windows.Forms.Label lblf10;
        internal System.Windows.Forms.TextBox txtf10;
        internal System.Windows.Forms.Label lblf9;
        internal System.Windows.Forms.TextBox txtf9;
        internal System.Windows.Forms.Label lblf8;
        internal System.Windows.Forms.TextBox txtf8;
        internal System.Windows.Forms.Label lblf7;
        internal System.Windows.Forms.TextBox txtf7;
        internal System.Windows.Forms.Label lblf6;
        internal System.Windows.Forms.TextBox txtf6;
        internal System.Windows.Forms.Label lblf5;
        internal System.Windows.Forms.TextBox txtf5;
        internal System.Windows.Forms.Label lblf4;
        internal System.Windows.Forms.TextBox txtf4;
        internal System.Windows.Forms.Label lblf3;
        internal System.Windows.Forms.TextBox txtf3;
        internal System.Windows.Forms.Label lblf2;
        internal System.Windows.Forms.TextBox txtf2;
        internal System.Windows.Forms.Label lblf1;
        internal System.Windows.Forms.TextBox txtf1;
    }
}