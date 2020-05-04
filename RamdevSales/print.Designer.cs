namespace RamdevSales
{
    partial class Print
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbformat = new System.Windows.Forms.ComboBox();
            this.btnpreview = new System.Windows.Forms.Button();
            this.lblrptname = new System.Windows.Forms.Label();
            this.txtheader = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnprint = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Format";
            // 
            // cmbformat
            // 
            this.cmbformat.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbformat.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbformat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbformat.FormattingEnabled = true;
            this.cmbformat.Location = new System.Drawing.Point(55, 21);
            this.cmbformat.Name = "cmbformat";
            this.cmbformat.Size = new System.Drawing.Size(259, 21);
            this.cmbformat.TabIndex = 1;
            this.cmbformat.SelectedIndexChanged += new System.EventHandler(this.cmbformat_SelectedIndexChanged);
            this.cmbformat.Enter += new System.EventHandler(this.cmbformat_Enter);
            this.cmbformat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbformat_KeyDown);
            this.cmbformat.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbformat_KeyUp);
            this.cmbformat.Leave += new System.EventHandler(this.cmbformat_Leave);
            // 
            // btnpreview
            // 
            this.btnpreview.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnpreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnpreview.ForeColor = System.Drawing.Color.White;
            this.btnpreview.Location = new System.Drawing.Point(182, 159);
            this.btnpreview.Name = "btnpreview";
            this.btnpreview.Size = new System.Drawing.Size(97, 34);
            this.btnpreview.TabIndex = 2;
            this.btnpreview.Text = "Preview";
            this.btnpreview.UseVisualStyleBackColor = false;
            this.btnpreview.Click += new System.EventHandler(this.btnpreview_Click);
            this.btnpreview.Enter += new System.EventHandler(this.btnpreview_Enter);
            this.btnpreview.Leave += new System.EventHandler(this.btnpreview_Leave);
            this.btnpreview.MouseEnter += new System.EventHandler(this.btnpreview_MouseEnter);
            this.btnpreview.MouseLeave += new System.EventHandler(this.btnpreview_MouseLeave);
            // 
            // lblrptname
            // 
            this.lblrptname.AutoSize = true;
            this.lblrptname.Location = new System.Drawing.Point(60, 57);
            this.lblrptname.Name = "lblrptname";
            this.lblrptname.Size = new System.Drawing.Size(35, 13);
            this.lblrptname.TabIndex = 3;
            this.lblrptname.Text = "label2";
            // 
            // txtheader
            // 
            this.txtheader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtheader.BackColor = System.Drawing.SystemColors.Highlight;
            this.txtheader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtheader.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtheader.ForeColor = System.Drawing.Color.White;
            this.txtheader.Location = new System.Drawing.Point(0, 0);
            this.txtheader.Name = "txtheader";
            this.txtheader.ReadOnly = true;
            this.txtheader.Size = new System.Drawing.Size(396, 31);
            this.txtheader.TabIndex = 173;
            this.txtheader.TabStop = false;
            this.txtheader.Text = "PRINT";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnprint);
            this.panel1.Controls.Add(this.cmbformat);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnpreview);
            this.panel1.Controls.Add(this.lblrptname);
            this.panel1.Location = new System.Drawing.Point(0, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(396, 212);
            this.panel1.TabIndex = 174;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(288, 158);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(97, 34);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.Enter += new System.EventHandler(this.btnClose_Enter);
            this.btnClose.Leave += new System.EventHandler(this.btnClose_Leave);
            this.btnClose.MouseEnter += new System.EventHandler(this.btnClose_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(77, 159);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(97, 34);
            this.btnprint.TabIndex = 4;
            this.btnprint.Text = "Print";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            this.btnprint.Enter += new System.EventHandler(this.btnprint_Enter);
            this.btnprint.Leave += new System.EventHandler(this.btnprint_Leave);
            this.btnprint.MouseEnter += new System.EventHandler(this.btnprint_MouseEnter);
            this.btnprint.MouseLeave += new System.EventHandler(this.btnprint_MouseLeave);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Print
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(397, 246);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtheader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Print";
            this.Text = "Print";
            this.Load += new System.EventHandler(this.Print_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbformat;
        private System.Windows.Forms.Button btnpreview;
        private System.Windows.Forms.Label lblrptname;
        internal System.Windows.Forms.TextBox txtheader;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnprint;
        internal System.Windows.Forms.Button btnClose;
    }
}