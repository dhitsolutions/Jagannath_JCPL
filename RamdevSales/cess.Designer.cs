namespace RamdevSales
{
    partial class cess
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
            this.txtcessamt = new System.Windows.Forms.TextBox();
            this.lblcessamt = new System.Windows.Forms.Label();
            this.txtcessper = new System.Windows.Forms.TextBox();
            this.lblcessper = new System.Windows.Forms.Label();
            this.lblcess = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // txtcessamt
            // 
            this.txtcessamt.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcessamt.Location = new System.Drawing.Point(42, 82);
            this.txtcessamt.Name = "txtcessamt";
            this.txtcessamt.Size = new System.Drawing.Size(182, 24);
            this.txtcessamt.TabIndex = 84;
            this.txtcessamt.Text = "0";
            this.txtcessamt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtcessamt_KeyDown);
            // 
            // lblcessamt
            // 
            this.lblcessamt.AutoSize = true;
            this.lblcessamt.BackColor = System.Drawing.Color.White;
            this.lblcessamt.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcessamt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblcessamt.Location = new System.Drawing.Point(95, 62);
            this.lblcessamt.Name = "lblcessamt";
            this.lblcessamt.Size = new System.Drawing.Size(76, 17);
            this.lblcessamt.TabIndex = 86;
            this.lblcessamt.Text = "Cess Amt";
            // 
            // txtcessper
            // 
            this.txtcessper.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcessper.Location = new System.Drawing.Point(42, 31);
            this.txtcessper.Name = "txtcessper";
            this.txtcessper.Size = new System.Drawing.Size(182, 24);
            this.txtcessper.TabIndex = 83;
            this.txtcessper.Text = "0";
            this.txtcessper.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtcessper_KeyDown);
            // 
            // lblcessper
            // 
            this.lblcessper.AutoSize = true;
            this.lblcessper.BackColor = System.Drawing.Color.White;
            this.lblcessper.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcessper.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblcessper.Location = new System.Drawing.Point(95, 9);
            this.lblcessper.Name = "lblcessper";
            this.lblcessper.Size = new System.Drawing.Size(42, 17);
            this.lblcessper.TabIndex = 85;
            this.lblcessper.Text = "Cess";
            // 
            // lblcess
            // 
            this.lblcess.AutoSize = true;
            this.lblcess.BackColor = System.Drawing.Color.White;
            this.lblcess.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcess.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblcess.Location = new System.Drawing.Point(131, 9);
            this.lblcess.Name = "lblcess";
            this.lblcess.Size = new System.Drawing.Size(17, 17);
            this.lblcess.TabIndex = 87;
            this.lblcess.Text = "0";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(5, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(257, 107);
            this.panel1.TabIndex = 88;
            // 
            // cess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(266, 119);
            this.ControlBox = false;
            this.Controls.Add(this.lblcess);
            this.Controls.Add(this.txtcessamt);
            this.Controls.Add(this.lblcessamt);
            this.Controls.Add(this.txtcessper);
            this.Controls.Add(this.lblcessper);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "cess";
            this.Text = "cess";
            this.Load += new System.EventHandler(this.cess_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtcessamt;
        private System.Windows.Forms.Label lblcessamt;
        private System.Windows.Forms.TextBox txtcessper;
        private System.Windows.Forms.Label lblcessper;
        private System.Windows.Forms.Label lblcess;
        private System.Windows.Forms.Panel panel1;
    }
}