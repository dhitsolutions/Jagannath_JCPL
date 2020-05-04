namespace RamdevSales
{
    partial class SaleOrder
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
            this.LVFO = new System.Windows.Forms.ListView();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.btnPayment = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.chkselectall = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LVFO
            // 
            this.LVFO.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.LVFO.BackColor = System.Drawing.Color.White;
            this.LVFO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LVFO.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LVFO.ForeColor = System.Drawing.Color.Maroon;
            this.LVFO.FullRowSelect = true;
            this.LVFO.GridLines = true;
            this.LVFO.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.LVFO.HideSelection = false;
            this.LVFO.Location = new System.Drawing.Point(12, 48);
            this.LVFO.MultiSelect = false;
            this.LVFO.Name = "LVFO";
            this.LVFO.Size = new System.Drawing.Size(362, 219);
            this.LVFO.TabIndex = 326;
            this.LVFO.UseCompatibleStateImageBehavior = false;
            this.LVFO.View = System.Windows.Forms.View.Details;
            this.LVFO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LVFO_KeyDown);
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.Highlight;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.ForeColor = System.Drawing.Color.White;
            this.textBox7.Location = new System.Drawing.Point(1, 0);
            this.textBox7.Multiline = true;
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(392, 42);
            this.textBox7.TabIndex = 328;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "Select Pending Challans which you want to convert to Bill";
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnPayment
            // 
            this.btnPayment.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayment.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPayment.ForeColor = System.Drawing.Color.White;
            this.btnPayment.Location = new System.Drawing.Point(189, 271);
            this.btnPayment.Name = "btnPayment";
            this.btnPayment.Size = new System.Drawing.Size(97, 34);
            this.btnPayment.TabIndex = 329;
            this.btnPayment.Text = "&Continue";
            this.btnPayment.UseVisualStyleBackColor = false;
            this.btnPayment.Click += new System.EventHandler(this.BtnPayment_Click);
            this.btnPayment.Enter += new System.EventHandler(this.btnPayment_Enter);
            this.btnPayment.Leave += new System.EventHandler(this.btnPayment_Leave);
            this.btnPayment.MouseEnter += new System.EventHandler(this.btnPayment_MouseEnter);
            this.btnPayment.MouseLeave += new System.EventHandler(this.btnPayment_MouseLeave);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(11, 270);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 34);
            this.button1.TabIndex = 330;
            this.button1.Text = "&Back";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.Enter += new System.EventHandler(this.button1_Enter);
            this.button1.Leave += new System.EventHandler(this.button1_Leave);
            this.button1.MouseEnter += new System.EventHandler(this.button1_MouseEnter);
            this.button1.MouseLeave += new System.EventHandler(this.button1_MouseLeave);
            // 
            // chkselectall
            // 
            this.chkselectall.AutoSize = true;
            this.chkselectall.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkselectall.Location = new System.Drawing.Point(96, 278);
            this.chkselectall.Name = "chkselectall";
            this.chkselectall.Size = new System.Drawing.Size(94, 20);
            this.chkselectall.TabIndex = 331;
            this.chkselectall.Text = "&Select All";
            this.chkselectall.UseVisualStyleBackColor = true;
            this.chkselectall.CheckedChanged += new System.EventHandler(this.chkselectall_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Location = new System.Drawing.Point(2, 42);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(391, 265);
            this.panel1.TabIndex = 332;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(284, 228);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(97, 34);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.Enter += new System.EventHandler(this.btnClose_Enter);
            this.btnClose.Leave += new System.EventHandler(this.btnClose_Leave);
            this.btnClose.MouseEnter += new System.EventHandler(this.btnClose_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            // 
            // SaleOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(398, 323);
            this.ControlBox = false;
            this.Controls.Add(this.chkselectall);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnPayment);
            this.Controls.Add(this.LVFO);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SaleOrder";
            this.Text = "SaleOrder";
            this.Load += new System.EventHandler(this.SaleOrder_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ListView LVFO;
        internal System.Windows.Forms.TextBox textBox7;
        internal System.Windows.Forms.Button btnPayment;
        internal System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chkselectall;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClose;
    }
}