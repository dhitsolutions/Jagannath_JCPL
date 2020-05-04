namespace RamdevSales
{
    partial class PaymentStatus
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
            this.grdpayment = new System.Windows.Forms.DataGridView();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdpayment)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdpayment
            // 
            this.grdpayment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdpayment.Location = new System.Drawing.Point(140, 115);
            this.grdpayment.Name = "grdpayment";
            this.grdpayment.Size = new System.Drawing.Size(754, 535);
            this.grdpayment.TabIndex = 0;
            this.grdpayment.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdpayment_CellClick);
            this.grdpayment.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdpayment_CellEndEdit);
            this.grdpayment.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdpayment_CellValidated);
            this.grdpayment.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.grdpayment_EditingControlShowing);
            this.grdpayment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdpayment_KeyDown);
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
            this.TextBox1.TabIndex = 22;
            this.TextBox1.TabStop = false;
            this.TextBox1.Text = "Client Wise Payment Detail";
            this.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(140, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(754, 60);
            this.panel1.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(396, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "***Here Define the Status Of payment Pending Details.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(29, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(559, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = " If you want to change any details You Can Directly Change it by Clicking On It.";
            // 
            // PaymentStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 662);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.grdpayment);
            this.Name = "PaymentStatus";
            this.Text = "PaymentStatus";
            this.Load += new System.EventHandler(this.PaymentStatus_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdpayment)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grdpayment;
        internal System.Windows.Forms.TextBox TextBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}