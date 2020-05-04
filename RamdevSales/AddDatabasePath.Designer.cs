namespace RamdevSales
{
    partial class AddDatabasePath
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
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtdatabasepath = new System.Windows.Forms.TextBox();
            this.btnapply = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.txtsetpath = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // textBox14
            // 
            this.textBox14.BackColor = System.Drawing.SystemColors.Highlight;
            this.textBox14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox14.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox14.ForeColor = System.Drawing.Color.White;
            this.textBox14.Location = new System.Drawing.Point(2, 1);
            this.textBox14.Name = "textBox14";
            this.textBox14.ReadOnly = true;
            this.textBox14.Size = new System.Drawing.Size(444, 31);
            this.textBox14.TabIndex = 202;
            this.textBox14.TabStop = false;
            this.textBox14.Text = "SET DATABASE PATH";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 18);
            this.label1.TabIndex = 206;
            this.label1.Text = "Current Database Path";
            // 
            // txtdatabasepath
            // 
            this.txtdatabasepath.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdatabasepath.Location = new System.Drawing.Point(12, 71);
            this.txtdatabasepath.Name = "txtdatabasepath";
            this.txtdatabasepath.Size = new System.Drawing.Size(422, 27);
            this.txtdatabasepath.TabIndex = 0;
            // 
            // btnapply
            // 
            this.btnapply.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnapply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnapply.ForeColor = System.Drawing.Color.White;
            this.btnapply.Location = new System.Drawing.Point(239, 104);
            this.btnapply.Name = "btnapply";
            this.btnapply.Size = new System.Drawing.Size(97, 34);
            this.btnapply.TabIndex = 2;
            this.btnapply.Text = "Submit";
            this.btnapply.UseVisualStyleBackColor = false;
            this.btnapply.Click += new System.EventHandler(this.btnapply_Click);
            this.btnapply.Enter += new System.EventHandler(this.btnapply_Enter);
            this.btnapply.Leave += new System.EventHandler(this.btnapply_Leave);
            this.btnapply.MouseEnter += new System.EventHandler(this.btnapply_MouseEnter);
            this.btnapply.MouseLeave += new System.EventHandler(this.btnapply_MouseLeave);
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(344, 104);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(97, 34);
            this.btnclose.TabIndex = 3;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            this.btnclose.MouseEnter += new System.EventHandler(this.btnclose_MouseEnter);
            this.btnclose.MouseLeave += new System.EventHandler(this.btnclose_MouseLeave);
            // 
            // txtsetpath
            // 
            this.txtsetpath.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.txtsetpath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtsetpath.ForeColor = System.Drawing.Color.White;
            this.txtsetpath.Location = new System.Drawing.Point(12, 104);
            this.txtsetpath.Name = "txtsetpath";
            this.txtsetpath.Size = new System.Drawing.Size(133, 34);
            this.txtsetpath.TabIndex = 1;
            this.txtsetpath.Text = "&Select New Path";
            this.txtsetpath.UseVisualStyleBackColor = false;
            this.txtsetpath.Click += new System.EventHandler(this.txtsetpath_Click);
            this.txtsetpath.Enter += new System.EventHandler(this.txtsetpath_Enter);
            this.txtsetpath.Leave += new System.EventHandler(this.txtsetpath_Leave);
            this.txtsetpath.MouseEnter += new System.EventHandler(this.txtsetpath_MouseEnter);
            this.txtsetpath.MouseLeave += new System.EventHandler(this.txtsetpath_MouseLeave);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(3, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(443, 152);
            this.panel1.TabIndex = 207;
            // 
            // AddDatabasePath
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(446, 261);
            this.ControlBox = false;
            this.Controls.Add(this.txtsetpath);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.btnapply);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtdatabasepath);
            this.Controls.Add(this.textBox14);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddDatabasePath";
            this.Text = "AddDatabasePath";
            this.Load += new System.EventHandler(this.AddDatabasePath_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtdatabasepath;
        private System.Windows.Forms.Button btnapply;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Button txtsetpath;
        private System.Windows.Forms.Panel panel1;
    }
}