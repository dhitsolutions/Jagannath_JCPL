namespace RamdevSales
{
    partial class OpStock
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary6>
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
            this.grdstock = new System.Windows.Forms.DataGridView();
            this.label11 = new System.Windows.Forms.Label();
            this.txttotamt = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.btndelete = new System.Windows.Forms.Button();
            this.btnsave = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnexcel = new System.Windows.Forms.Button();
            this.txtunitsalepricetotal = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdstock)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdstock
            // 
            this.grdstock.AllowUserToAddRows = false;
            this.grdstock.BackgroundColor = System.Drawing.SystemColors.Window;
            this.grdstock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdstock.Location = new System.Drawing.Point(12, 58);
            this.grdstock.Name = "grdstock";
            this.grdstock.RowHeadersVisible = false;
            this.grdstock.Size = new System.Drawing.Size(1101, 413);
            this.grdstock.TabIndex = 0;
            this.grdstock.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdstock_CellContentClick);
            this.grdstock.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdstock_CellValidated);
            this.grdstock.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.grdstock_EditingControlShowing);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(521, 474);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(117, 16);
            this.label11.TabIndex = 240;
            this.label11.Text = "Total Op. Stock";
            // 
            // txttotamt
            // 
            this.txttotamt.BackColor = System.Drawing.Color.White;
            this.txttotamt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txttotamt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttotamt.ForeColor = System.Drawing.Color.Black;
            this.txttotamt.Location = new System.Drawing.Point(525, 495);
            this.txttotamt.Multiline = true;
            this.txttotamt.Name = "txttotamt";
            this.txttotamt.ReadOnly = true;
            this.txttotamt.Size = new System.Drawing.Size(113, 30);
            this.txttotamt.TabIndex = 241;
            this.txttotamt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.textBox7.Size = new System.Drawing.Size(1126, 31);
            this.textBox7.TabIndex = 242;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "OPENING STOCK EDITOR";
            // 
            // btndelete
            // 
            this.btndelete.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btndelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndelete.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndelete.ForeColor = System.Drawing.Color.White;
            this.btndelete.Location = new System.Drawing.Point(1016, 491);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(97, 34);
            this.btndelete.TabIndex = 2;
            this.btndelete.Text = "&Close";
            this.btndelete.UseVisualStyleBackColor = false;
            this.btndelete.Click += new System.EventHandler(this.btndelete_Click);
            this.btndelete.Enter += new System.EventHandler(this.btndelete_Enter);
            this.btndelete.Leave += new System.EventHandler(this.btndelete_Leave);
            this.btndelete.MouseEnter += new System.EventHandler(this.btndelete_MouseEnter);
            this.btndelete.MouseLeave += new System.EventHandler(this.btndelete_MouseLeave);
            // 
            // btnsave
            // 
            this.btnsave.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnsave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsave.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsave.ForeColor = System.Drawing.Color.White;
            this.btnsave.Location = new System.Drawing.Point(810, 491);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(97, 34);
            this.btnsave.TabIndex = 1;
            this.btnsave.Text = "&Submit";
            this.btnsave.UseVisualStyleBackColor = false;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            this.btnsave.Enter += new System.EventHandler(this.btnsave_Enter);
            this.btnsave.Leave += new System.EventHandler(this.btnsave_Leave);
            this.btnsave.MouseEnter += new System.EventHandler(this.btnsave_MouseEnter);
            this.btnsave.MouseLeave += new System.EventHandler(this.btnsave_MouseLeave);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(9, 37);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(329, 13);
            this.linkLabel1.TabIndex = 243;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Click here to Automatically Update from Previous Year Closing Stock";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnexcel);
            this.panel1.Controls.Add(this.txtunitsalepricetotal);
            this.panel1.Location = new System.Drawing.Point(1, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1125, 502);
            this.panel1.TabIndex = 244;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(642, 442);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 16);
            this.label1.TabIndex = 245;
            this.label1.Text = "Total Unit Sale Price\r\n";
            // 
            // btnexcel
            // 
            this.btnexcel.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnexcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnexcel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnexcel.ForeColor = System.Drawing.Color.White;
            this.btnexcel.Location = new System.Drawing.Point(911, 459);
            this.btnexcel.Name = "btnexcel";
            this.btnexcel.Size = new System.Drawing.Size(97, 34);
            this.btnexcel.TabIndex = 245;
            this.btnexcel.Text = "&Excel";
            this.btnexcel.UseVisualStyleBackColor = false;
            this.btnexcel.Click += new System.EventHandler(this.btnexcel_Click);
            this.btnexcel.Enter += new System.EventHandler(this.btnexcel_Enter);
            this.btnexcel.Leave += new System.EventHandler(this.btnexcel_Leave);
            this.btnexcel.MouseEnter += new System.EventHandler(this.btnexcel_MouseEnter);
            this.btnexcel.MouseLeave += new System.EventHandler(this.btnexcel_MouseLeave);
            // 
            // txtunitsalepricetotal
            // 
            this.txtunitsalepricetotal.BackColor = System.Drawing.Color.White;
            this.txtunitsalepricetotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtunitsalepricetotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtunitsalepricetotal.ForeColor = System.Drawing.Color.Black;
            this.txtunitsalepricetotal.Location = new System.Drawing.Point(646, 463);
            this.txtunitsalepricetotal.Multiline = true;
            this.txtunitsalepricetotal.Name = "txtunitsalepricetotal";
            this.txtunitsalepricetotal.ReadOnly = true;
            this.txtunitsalepricetotal.Size = new System.Drawing.Size(148, 30);
            this.txtunitsalepricetotal.TabIndex = 246;
            this.txtunitsalepricetotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // OpStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1131, 585);
            this.ControlBox = false;
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txttotamt);
            this.Controls.Add(this.btndelete);
            this.Controls.Add(this.btnsave);
            this.Controls.Add(this.grdstock);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OpStock";
            this.Text = "OpStock";
           
            this.Load += new System.EventHandler(this.OpStock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdstock)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grdstock;
        internal System.Windows.Forms.Label label11;
        internal System.Windows.Forms.TextBox txttotamt;
        internal System.Windows.Forms.TextBox textBox7;
        internal System.Windows.Forms.Button btndelete;
        internal System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Button btnexcel;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox txtunitsalepricetotal;
    }
}