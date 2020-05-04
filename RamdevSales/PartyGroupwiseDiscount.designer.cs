namespace RamdevSales
{
    partial class PartyGroupwiseDiscount
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
            this.cmbpartynm = new System.Windows.Forms.ComboBox();
            this.lblpartyname = new System.Windows.Forms.Label();
            this.cmbcompanynm = new System.Windows.Forms.ComboBox();
            this.lblcompany = new System.Windows.Forms.Label();
            this.BtnPayment = new System.Windows.Forms.Button();
            this.btndelete = new System.Windows.Forms.Button();
            this.Button17 = new System.Windows.Forms.Button();
            this.grdstock = new System.Windows.Forms.DataGridView();
            this.grdlist = new System.Windows.Forms.DataGridView();
            this.cmbpattern = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnclose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdstock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdlist)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.textBox7.Size = new System.Drawing.Size(1021, 31);
            this.textBox7.TabIndex = 172;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "PARTY WISE DISCOUNT";
            // 
            // cmbpartynm
            // 
            this.cmbpartynm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbpartynm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbpartynm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbpartynm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbpartynm.FormattingEnabled = true;
            this.cmbpartynm.Location = new System.Drawing.Point(164, 52);
            this.cmbpartynm.Name = "cmbpartynm";
            this.cmbpartynm.Size = new System.Drawing.Size(314, 24);
            this.cmbpartynm.TabIndex = 1;
            this.cmbpartynm.SelectedIndexChanged += new System.EventHandler(this.cmbpartynm_SelectedIndexChanged);
            this.cmbpartynm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbpartynm_KeyDown);
            this.cmbpartynm.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbpartynm_KeyUp);
            this.cmbpartynm.Leave += new System.EventHandler(this.cmbpartynm_Leave);
            // 
            // lblpartyname
            // 
            this.lblpartyname.AutoSize = true;
            this.lblpartyname.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpartyname.Location = new System.Drawing.Point(269, 33);
            this.lblpartyname.Name = "lblpartyname";
            this.lblpartyname.Size = new System.Drawing.Size(93, 16);
            this.lblpartyname.TabIndex = 182;
            this.lblpartyname.Text = "Party Name";
            // 
            // cmbcompanynm
            // 
            this.cmbcompanynm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcompanynm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcompanynm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbcompanynm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcompanynm.FormattingEnabled = true;
            this.cmbcompanynm.Location = new System.Drawing.Point(482, 52);
            this.cmbcompanynm.Name = "cmbcompanynm";
            this.cmbcompanynm.Size = new System.Drawing.Size(256, 24);
            this.cmbcompanynm.TabIndex = 2;
            this.cmbcompanynm.SelectedIndexChanged += new System.EventHandler(this.cmbcompanynm_SelectedIndexChanged);
            this.cmbcompanynm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcompanynm_KeyDown);
            this.cmbcompanynm.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbcompanynm_KeyUp);
            this.cmbcompanynm.Leave += new System.EventHandler(this.cmbcompanynm_Leave);
            // 
            // lblcompany
            // 
            this.lblcompany.AutoSize = true;
            this.lblcompany.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcompany.Location = new System.Drawing.Point(544, 33);
            this.lblcompany.Name = "lblcompany";
            this.lblcompany.Size = new System.Drawing.Size(123, 16);
            this.lblcompany.TabIndex = 184;
            this.lblcompany.Text = "Company Name";
            // 
            // BtnPayment
            // 
            this.BtnPayment.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BtnPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPayment.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPayment.ForeColor = System.Drawing.Color.White;
            this.BtnPayment.Location = new System.Drawing.Point(744, 48);
            this.BtnPayment.Name = "BtnPayment";
            this.BtnPayment.Size = new System.Drawing.Size(97, 34);
            this.BtnPayment.TabIndex = 3;
            this.BtnPayment.Text = "&Submit";
            this.BtnPayment.UseVisualStyleBackColor = false;
            this.BtnPayment.Click += new System.EventHandler(this.BtnPayment_Click);
            this.BtnPayment.Enter += new System.EventHandler(this.BtnPayment_Enter);
            this.BtnPayment.Leave += new System.EventHandler(this.BtnPayment_Leave);
            this.BtnPayment.MouseEnter += new System.EventHandler(this.BtnPayment_MouseEnter);
            this.BtnPayment.MouseLeave += new System.EventHandler(this.BtnPayment_MouseLeave);
            // 
            // btndelete
            // 
            this.btndelete.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btndelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndelete.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndelete.ForeColor = System.Drawing.Color.White;
            this.btndelete.Location = new System.Drawing.Point(855, 48);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(97, 34);
            this.btndelete.TabIndex = 4;
            this.btndelete.Text = "&Delete";
            this.btndelete.UseVisualStyleBackColor = false;
            this.btndelete.Click += new System.EventHandler(this.btndelete_Click);
            this.btndelete.Enter += new System.EventHandler(this.btndelete_Enter);
            this.btndelete.Leave += new System.EventHandler(this.btndelete_Leave);
            this.btndelete.MouseEnter += new System.EventHandler(this.btndelete_MouseEnter);
            this.btndelete.MouseLeave += new System.EventHandler(this.btndelete_MouseLeave);
            // 
            // Button17
            // 
            this.Button17.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Button17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button17.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button17.ForeColor = System.Drawing.Color.White;
            this.Button17.Location = new System.Drawing.Point(741, 52);
            this.Button17.Name = "Button17";
            this.Button17.Size = new System.Drawing.Size(97, 34);
            this.Button17.TabIndex = 186;
            this.Button17.Text = "&Copy";
            this.Button17.UseVisualStyleBackColor = false;
            this.Button17.Click += new System.EventHandler(this.Button17_Click);
            // 
            // grdstock
            // 
            this.grdstock.AllowUserToAddRows = false;
            this.grdstock.AllowUserToDeleteRows = false;
            this.grdstock.BackgroundColor = System.Drawing.Color.White;
            this.grdstock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdstock.Location = new System.Drawing.Point(8, 130);
            this.grdstock.Name = "grdstock";
            this.grdstock.Size = new System.Drawing.Size(733, 353);
            this.grdstock.TabIndex = 5;
            this.grdstock.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdstock_CellEndEdit);
            this.grdstock.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.grdstock_EditingControlShowing);
            this.grdstock.SelectionChanged += new System.EventHandler(this.grdstock_SelectionChanged);
            this.grdstock.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdstock_KeyDown);
            // 
            // grdlist
            // 
            this.grdlist.AllowUserToAddRows = false;
            this.grdlist.AllowUserToDeleteRows = false;
            this.grdlist.BackgroundColor = System.Drawing.Color.White;
            this.grdlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdlist.Location = new System.Drawing.Point(744, 130);
            this.grdlist.Name = "grdlist";
            this.grdlist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdlist.Size = new System.Drawing.Size(261, 353);
            this.grdlist.TabIndex = 6;
            this.grdlist.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdlist_CellContentClick);
            this.grdlist.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.grdlist_MouseDoubleClick);
            // 
            // cmbpattern
            // 
            this.cmbpattern.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbpattern.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbpattern.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbpattern.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbpattern.FormattingEnabled = true;
            this.cmbpattern.Items.AddRange(new object[] {
            "Item Wise",
            "Item Group wise",
            "Company Wise"});
            this.cmbpattern.Location = new System.Drawing.Point(6, 52);
            this.cmbpattern.Name = "cmbpattern";
            this.cmbpattern.Size = new System.Drawing.Size(152, 24);
            this.cmbpattern.TabIndex = 0;
            this.cmbpattern.SelectedIndexChanged += new System.EventHandler(this.cmbpattern_SelectedIndexChanged);
            this.cmbpattern.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbpattern_KeyDown);
            this.cmbpattern.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbpattern_KeyUp);
            this.cmbpattern.Leave += new System.EventHandler(this.cmbpattern_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(46, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 16);
            this.label2.TabIndex = 247;
            this.label2.Text = "Pattern";
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
            this.panel1.Controls.Add(this.btnclose);
            this.panel1.Controls.Add(this.Button17);
            this.panel1.Location = new System.Drawing.Point(2, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1019, 465);
            this.panel1.TabIndex = 248;
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(852, 52);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(97, 34);
            this.btnclose.TabIndex = 187;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            this.btnclose.Enter += new System.EventHandler(this.btnclose_Enter);
            this.btnclose.Leave += new System.EventHandler(this.btnclose_Leave);
            this.btnclose.MouseEnter += new System.EventHandler(this.btnclose_MouseEnter);
            this.btnclose.MouseLeave += new System.EventHandler(this.btnclose_MouseLeave);
            // 
            // PartyGroupwiseDiscount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 522);
            this.ControlBox = false;
            this.Controls.Add(this.cmbpattern);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.grdlist);
            this.Controls.Add(this.grdstock);
            this.Controls.Add(this.BtnPayment);
            this.Controls.Add(this.btndelete);
            this.Controls.Add(this.cmbcompanynm);
            this.Controls.Add(this.lblcompany);
            this.Controls.Add(this.cmbpartynm);
            this.Controls.Add(this.lblpartyname);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PartyGroupwiseDiscount";
            this.Text = "  ";
           
            this.Load += new System.EventHandler(this.PartyGroupwiseDiscount_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdstock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdlist)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.ComboBox cmbpartynm;
        internal System.Windows.Forms.Label lblpartyname;
        private System.Windows.Forms.ComboBox cmbcompanynm;
        internal System.Windows.Forms.Label lblcompany;
        internal System.Windows.Forms.Button BtnPayment;
        internal System.Windows.Forms.Button btndelete;
        internal System.Windows.Forms.Button Button17;
        private System.Windows.Forms.DataGridView grdstock;
        private System.Windows.Forms.DataGridView grdlist;
        private System.Windows.Forms.ComboBox cmbpattern;
        internal System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Button btnclose;
    }
}