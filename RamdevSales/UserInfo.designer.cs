namespace RamdevSales
{
    partial class UserInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserInfo));
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbPosition = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.btncusendit = new System.Windows.Forms.Button();
            this.btncustype = new System.Windows.Forms.Button();
            this.txtswipid = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtphoneno = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtaddress = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtname = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbtital = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChangePswd = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.cmbcommtyep = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtcommtype = new System.Windows.Forms.TextBox();
            this.txttargetcomm = new System.Windows.Forms.TextBox();
            this.cmbtargetcomm = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBox1
            // 
            this.TextBox1.BackColor = System.Drawing.SystemColors.Highlight;
            this.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBox1.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox1.ForeColor = System.Drawing.Color.White;
            this.TextBox1.Location = new System.Drawing.Point(2, 1);
            this.TextBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = true;
            this.TextBox1.Size = new System.Drawing.Size(509, 31);
            this.TextBox1.TabIndex = 26;
            this.TextBox1.TabStop = false;
            this.TextBox1.Text = "USER\'S INFORMATION";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Verdana", 10F);
            this.txtPassword.Location = new System.Drawing.Point(162, 243);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(280, 24);
            this.txtPassword.TabIndex = 6;
            this.txtPassword.Enter += new System.EventHandler(this.txtPassword_Enter);
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            this.txtPassword.Leave += new System.EventHandler(this.txtPassword_Leave);
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("Verdana", 10F);
            this.txtUserName.Location = new System.Drawing.Point(162, 213);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(280, 24);
            this.txtUserName.TabIndex = 5;
            this.txtUserName.Enter += new System.EventHandler(this.txtUserName_Enter);
            this.txtUserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserName_KeyDown);
            this.txtUserName.Leave += new System.EventHandler(this.txtUserName_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 10F);
            this.label4.Location = new System.Drawing.Point(21, 246);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 17);
            this.label4.TabIndex = 39;
            this.label4.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 10F);
            this.label3.Location = new System.Drawing.Point(21, 216);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 17);
            this.label3.TabIndex = 38;
            this.label3.Text = "User Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 10F);
            this.label2.Location = new System.Drawing.Point(20, 273);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 17);
            this.label2.TabIndex = 42;
            this.label2.Text = "Employee Type";
            // 
            // cmbPosition
            // 
            this.cmbPosition.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbPosition.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPosition.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPosition.FormattingEnabled = true;
            this.cmbPosition.Items.AddRange(new object[] {
            "Operator",
            "Casier",
            "Super User"});
            this.cmbPosition.Location = new System.Drawing.Point(162, 273);
            this.cmbPosition.Name = "cmbPosition";
            this.cmbPosition.Size = new System.Drawing.Size(280, 24);
            this.cmbPosition.TabIndex = 7;
            this.cmbPosition.SelectedIndexChanged += new System.EventHandler(this.cmbPosition_SelectedIndexChanged);
            this.cmbPosition.Enter += new System.EventHandler(this.cmbPosition_Enter);
            this.cmbPosition.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbPosition_KeyDown);
            this.cmbPosition.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbPosition_KeyUp);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Verdana", 10F);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(125, 373);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(97, 34);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "&Submit";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnSave.Enter += new System.EventHandler(this.btnSave_Enter);
            this.btnSave.Leave += new System.EventHandler(this.btnSave_Leave);
            this.btnSave.MouseEnter += new System.EventHandler(this.btnSave_MouseEnter);
            this.btnSave.MouseLeave += new System.EventHandler(this.btnSave_MouseLeave);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Verdana", 10F);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(333, 373);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(97, 34);
            this.btnDelete.TabIndex = 14;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.btnDelete.Enter += new System.EventHandler(this.btnDelete_Enter);
            this.btnDelete.Leave += new System.EventHandler(this.btnDelete_Leave);
            this.btnDelete.MouseEnter += new System.EventHandler(this.btnDelete_MouseEnter);
            this.btnDelete.MouseLeave += new System.EventHandler(this.btnDelete_MouseLeave);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Verdana", 10F);
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(228, 373);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(97, 34);
            this.btnReset.TabIndex = 13;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            this.btnReset.Enter += new System.EventHandler(this.btnReset_Enter);
            this.btnReset.Leave += new System.EventHandler(this.btnReset_Leave);
            this.btnReset.MouseEnter += new System.EventHandler(this.btnReset_MouseEnter);
            this.btnReset.MouseLeave += new System.EventHandler(this.btnReset_MouseLeave);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Verdana", 10F);
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(22, 373);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(97, 34);
            this.btnBack.TabIndex = 16;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            this.btnBack.Enter += new System.EventHandler(this.btnBack_Enter);
            this.btnBack.Leave += new System.EventHandler(this.btnBack_Leave);
            this.btnBack.MouseEnter += new System.EventHandler(this.btnBack_MouseEnter);
            this.btnBack.MouseLeave += new System.EventHandler(this.btnBack_MouseLeave);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txttargetcomm);
            this.panel1.Controls.Add(this.cmbtargetcomm);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.txtcommtype);
            this.panel1.Controls.Add(this.cmbcommtyep);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.btncusendit);
            this.panel1.Controls.Add(this.btncustype);
            this.panel1.Controls.Add(this.txtswipid);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txtphoneno);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtaddress);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtname);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cmbtital);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cmbPosition);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnChangePswd);
            this.panel1.Controls.Add(this.txtPassword);
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Controls.Add(this.txtUserName);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnReset);
            this.panel1.Location = new System.Drawing.Point(2, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(509, 468);
            this.panel1.TabIndex = 0;
            // 
            // btncusendit
            // 
            this.btncusendit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btncusendit.BackgroundImage")));
            this.btncusendit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncusendit.ForeColor = System.Drawing.Color.White;
            this.btncusendit.Location = new System.Drawing.Point(466, 274);
            this.btncusendit.Name = "btncusendit";
            this.btncusendit.Size = new System.Drawing.Size(23, 22);
            this.btncusendit.TabIndex = 265;
            this.btncusendit.TabStop = false;
            this.btncusendit.UseVisualStyleBackColor = true;
            this.btncusendit.Click += new System.EventHandler(this.btncusendit_Click);
            // 
            // btncustype
            // 
            this.btncustype.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btncustype.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btncustype.BackgroundImage")));
            this.btncustype.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncustype.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncustype.ForeColor = System.Drawing.Color.White;
            this.btncustype.Location = new System.Drawing.Point(447, 275);
            this.btncustype.Name = "btncustype";
            this.btncustype.Size = new System.Drawing.Size(22, 22);
            this.btncustype.TabIndex = 264;
            this.btncustype.TabStop = false;
            this.btncustype.Text = "&Add";
            this.btncustype.UseVisualStyleBackColor = false;
            this.btncustype.Click += new System.EventHandler(this.btncustype_Click);
            // 
            // txtswipid
            // 
            this.txtswipid.Font = new System.Drawing.Font("Verdana", 10F);
            this.txtswipid.Location = new System.Drawing.Point(162, 21);
            this.txtswipid.Name = "txtswipid";
            this.txtswipid.Size = new System.Drawing.Size(120, 24);
            this.txtswipid.TabIndex = 0;
            this.txtswipid.Enter += new System.EventHandler(this.txtswipid_Enter);
            this.txtswipid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtswipid_KeyDown);
            this.txtswipid.Leave += new System.EventHandler(this.txtswipid_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 10F);
            this.label8.Location = new System.Drawing.Point(20, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 17);
            this.label8.TabIndex = 51;
            this.label8.Text = "Swipe ID";
            // 
            // txtphoneno
            // 
            this.txtphoneno.Font = new System.Drawing.Font("Verdana", 10F);
            this.txtphoneno.Location = new System.Drawing.Point(162, 183);
            this.txtphoneno.Name = "txtphoneno";
            this.txtphoneno.Size = new System.Drawing.Size(280, 24);
            this.txtphoneno.TabIndex = 4;
            this.txtphoneno.Enter += new System.EventHandler(this.txtphoneno_Enter);
            this.txtphoneno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtphoneno_KeyDown);
            this.txtphoneno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtphoneno_KeyPress);
            this.txtphoneno.Leave += new System.EventHandler(this.txtphoneno_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 10F);
            this.label7.Location = new System.Drawing.Point(21, 186);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 17);
            this.label7.TabIndex = 50;
            this.label7.Text = "Phone No";
            // 
            // txtaddress
            // 
            this.txtaddress.Font = new System.Drawing.Font("Verdana", 10F);
            this.txtaddress.Location = new System.Drawing.Point(162, 83);
            this.txtaddress.Multiline = true;
            this.txtaddress.Name = "txtaddress";
            this.txtaddress.Size = new System.Drawing.Size(280, 91);
            this.txtaddress.TabIndex = 3;
            this.txtaddress.Enter += new System.EventHandler(this.txtaddress_Enter);
            this.txtaddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtaddress_KeyDown);
            this.txtaddress.Leave += new System.EventHandler(this.txtaddress_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 10F);
            this.label6.Location = new System.Drawing.Point(21, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 17);
            this.label6.TabIndex = 48;
            this.label6.Text = "Address";
            // 
            // txtname
            // 
            this.txtname.Font = new System.Drawing.Font("Verdana", 10F);
            this.txtname.Location = new System.Drawing.Point(162, 53);
            this.txtname.Name = "txtname";
            this.txtname.Size = new System.Drawing.Size(280, 24);
            this.txtname.TabIndex = 2;
            this.txtname.Enter += new System.EventHandler(this.txtname_Enter);
            this.txtname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtname_KeyDown);
            this.txtname.Leave += new System.EventHandler(this.txtname_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 10F);
            this.label5.Location = new System.Drawing.Point(21, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 17);
            this.label5.TabIndex = 46;
            this.label5.Text = "Name";
            // 
            // cmbtital
            // 
            this.cmbtital.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbtital.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbtital.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbtital.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbtital.FormattingEnabled = true;
            this.cmbtital.Items.AddRange(new object[] {
            "Mr.",
            "Mrs.",
            "Miss."});
            this.cmbtital.Location = new System.Drawing.Point(331, 21);
            this.cmbtital.Name = "cmbtital";
            this.cmbtital.Size = new System.Drawing.Size(111, 24);
            this.cmbtital.TabIndex = 1;
            this.cmbtital.SelectedIndexChanged += new System.EventHandler(this.cmbtital_SelectedIndexChanged);
            this.cmbtital.Enter += new System.EventHandler(this.cmbtital_Enter);
            this.cmbtital.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbtital_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 10F);
            this.label1.Location = new System.Drawing.Point(288, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 17);
            this.label1.TabIndex = 43;
            this.label1.Text = "Tital";
            // 
            // btnChangePswd
            // 
            this.btnChangePswd.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnChangePswd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePswd.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangePswd.ForeColor = System.Drawing.Color.White;
            this.btnChangePswd.Location = new System.Drawing.Point(23, 413);
            this.btnChangePswd.Name = "btnChangePswd";
            this.btnChangePswd.Size = new System.Drawing.Size(199, 34);
            this.btnChangePswd.TabIndex = 17;
            this.btnChangePswd.Text = "Change Password";
            this.btnChangePswd.UseVisualStyleBackColor = false;
            this.btnChangePswd.Visible = false;
            this.btnChangePswd.Click += new System.EventHandler(this.btnChangePswd_Click);
            this.btnChangePswd.Enter += new System.EventHandler(this.btnChangePswd_Enter);
            this.btnChangePswd.Leave += new System.EventHandler(this.btnChangePswd_Leave);
            this.btnChangePswd.MouseEnter += new System.EventHandler(this.btnChangePswd_MouseEnter);
            this.btnChangePswd.MouseLeave += new System.EventHandler(this.btnChangePswd_MouseLeave);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(333, 413);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(97, 34);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.Enter += new System.EventHandler(this.btnClose_Enter);
            this.btnClose.Leave += new System.EventHandler(this.btnClose_Leave);
            this.btnClose.MouseEnter += new System.EventHandler(this.btnClose_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            // 
            // cmbcommtyep
            // 
            this.cmbcommtyep.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcommtyep.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcommtyep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbcommtyep.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcommtyep.FormattingEnabled = true;
            this.cmbcommtyep.Items.AddRange(new object[] {
            "Percentage(%)",
            "Flat"});
            this.cmbcommtyep.Location = new System.Drawing.Point(162, 303);
            this.cmbcommtyep.Name = "cmbcommtyep";
            this.cmbcommtyep.Size = new System.Drawing.Size(163, 24);
            this.cmbcommtyep.TabIndex = 8;
            this.cmbcommtyep.SelectedIndexChanged += new System.EventHandler(this.cmbcommtyep_SelectedIndexChanged);
            this.cmbcommtyep.Enter += new System.EventHandler(this.cmbcommtyep_Enter);
            this.cmbcommtyep.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcommtyep_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 10F);
            this.label9.Location = new System.Drawing.Point(18, 306);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(132, 17);
            this.label9.TabIndex = 266;
            this.label9.Text = "Commission Type";
            // 
            // txtcommtype
            // 
            this.txtcommtype.Font = new System.Drawing.Font("Verdana", 10F);
            this.txtcommtype.Location = new System.Drawing.Point(331, 303);
            this.txtcommtype.Name = "txtcommtype";
            this.txtcommtype.Size = new System.Drawing.Size(111, 24);
            this.txtcommtype.TabIndex = 9;
            this.txtcommtype.Enter += new System.EventHandler(this.txtcommtype_Enter);
            this.txtcommtype.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtcommtype_KeyDown);
            this.txtcommtype.Leave += new System.EventHandler(this.txtcommtype_Leave);
            // 
            // txttargetcomm
            // 
            this.txttargetcomm.Font = new System.Drawing.Font("Verdana", 10F);
            this.txttargetcomm.Location = new System.Drawing.Point(331, 333);
            this.txttargetcomm.Name = "txttargetcomm";
            this.txttargetcomm.Size = new System.Drawing.Size(111, 24);
            this.txttargetcomm.TabIndex = 11;
            this.txttargetcomm.Enter += new System.EventHandler(this.txttargetcomm_Enter);
            this.txttargetcomm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txttargetcomm_KeyDown);
            this.txttargetcomm.Leave += new System.EventHandler(this.txttargetcomm_Leave);
            // 
            // cmbtargetcomm
            // 
            this.cmbtargetcomm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbtargetcomm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbtargetcomm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbtargetcomm.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbtargetcomm.FormattingEnabled = true;
            this.cmbtargetcomm.Items.AddRange(new object[] {
            "Percentage(%)",
            "Flat"});
            this.cmbtargetcomm.Location = new System.Drawing.Point(162, 333);
            this.cmbtargetcomm.Name = "cmbtargetcomm";
            this.cmbtargetcomm.Size = new System.Drawing.Size(163, 24);
            this.cmbtargetcomm.TabIndex = 10;
            this.cmbtargetcomm.SelectedIndexChanged += new System.EventHandler(this.cmbtargetcomm_SelectedIndexChanged);
            this.cmbtargetcomm.Enter += new System.EventHandler(this.cmbtargetcomm_Enter);
            this.cmbtargetcomm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbtargetcomm_KeyDown);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 10F);
            this.label10.Location = new System.Drawing.Point(18, 336);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(144, 17);
            this.label10.TabIndex = 269;
            this.label10.Text = "Target Commission";
            // 
            // UserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(512, 501);
            this.ControlBox = false;
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UserInfo";
            this.Text = "UserInfo";
            this.Load += new System.EventHandler(this.UserInfo_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox TextBox1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbPosition;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnChangePswd;
        private System.Windows.Forms.ComboBox cmbtital;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtname;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtaddress;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtphoneno;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtswipid;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btncusendit;
        internal System.Windows.Forms.Button btncustype;
        private System.Windows.Forms.TextBox txtcommtype;
        private System.Windows.Forms.ComboBox cmbcommtyep;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txttargetcomm;
        private System.Windows.Forms.ComboBox cmbtargetcomm;
        private System.Windows.Forms.Label label10;
    }
}