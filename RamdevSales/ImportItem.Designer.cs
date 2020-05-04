namespace RamdevSales
{
    partial class ImportItem
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnimoortdata = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnimport = new System.Windows.Forms.Button();
            this.lblitem = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtto = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtform = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBox1
            // 
            this.TextBox1.BackColor = System.Drawing.SystemColors.Highlight;
            this.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBox1.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox1.ForeColor = System.Drawing.Color.White;
            this.TextBox1.Location = new System.Drawing.Point(11, 12);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = true;
            this.TextBox1.Size = new System.Drawing.Size(542, 31);
            this.TextBox1.TabIndex = 25;
            this.TextBox1.TabStop = false;
            this.TextBox1.Text = "EXCEL DATA IMPORT";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(12, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(541, 164);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.White;
            this.groupBox4.Controls.Add(this.btnimoortdata);
            this.groupBox4.Location = new System.Drawing.Point(368, 19);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(167, 139);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Step 3";
            // 
            // btnimoortdata
            // 
            this.btnimoortdata.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnimoortdata.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnimoortdata.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnimoortdata.ForeColor = System.Drawing.Color.White;
            this.btnimoortdata.Location = new System.Drawing.Point(32, 33);
            this.btnimoortdata.Name = "btnimoortdata";
            this.btnimoortdata.Size = new System.Drawing.Size(97, 34);
            this.btnimoortdata.TabIndex = 8;
            this.btnimoortdata.Text = "Import Data";
            this.btnimoortdata.UseVisualStyleBackColor = false;
            this.btnimoortdata.Click += new System.EventHandler(this.btnimoortdata_Click);
            this.btnimoortdata.Enter += new System.EventHandler(this.btnimoortdata_Enter);
            this.btnimoortdata.Leave += new System.EventHandler(this.btnimoortdata_Leave);
            this.btnimoortdata.MouseEnter += new System.EventHandler(this.btnimoortdata_MouseEnter);
            this.btnimoortdata.MouseLeave += new System.EventHandler(this.btnimoortdata_MouseLeave);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.White;
            this.groupBox3.Controls.Add(this.btnimport);
            this.groupBox3.Controls.Add(this.lblitem);
            this.groupBox3.Location = new System.Drawing.Point(6, 18);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(356, 68);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Step 1";
            // 
            // btnimport
            // 
            this.btnimport.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnimport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnimport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnimport.ForeColor = System.Drawing.Color.White;
            this.btnimport.Location = new System.Drawing.Point(15, 19);
            this.btnimport.Name = "btnimport";
            this.btnimport.Size = new System.Drawing.Size(97, 34);
            this.btnimport.TabIndex = 6;
            this.btnimport.Text = "Select Excel";
            this.btnimport.UseVisualStyleBackColor = false;
            this.btnimport.Click += new System.EventHandler(this.btnimport_Click);
            this.btnimport.Enter += new System.EventHandler(this.btnimport_Enter);
            this.btnimport.Leave += new System.EventHandler(this.btnimport_Leave);
            this.btnimport.MouseEnter += new System.EventHandler(this.btnimport_MouseEnter);
            this.btnimport.MouseLeave += new System.EventHandler(this.btnimport_MouseLeave);
            // 
            // lblitem
            // 
            this.lblitem.AutoSize = true;
            this.lblitem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblitem.Location = new System.Drawing.Point(150, 30);
            this.lblitem.Name = "lblitem";
            this.lblitem.Size = new System.Drawing.Size(85, 13);
            this.lblitem.TabIndex = 7;
            this.lblitem.Text = "Selected Item";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.txtto);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtform);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(6, 92);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(356, 66);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Step 2";
            // 
            // txtto
            // 
            this.txtto.Enabled = false;
            this.txtto.Location = new System.Drawing.Point(183, 30);
            this.txtto.Name = "txtto";
            this.txtto.Size = new System.Drawing.Size(136, 20);
            this.txtto.TabIndex = 9;
            this.txtto.Text = "AC";
            this.txtto.Enter += new System.EventHandler(this.txtto_Enter);
            this.txtto.Leave += new System.EventHandler(this.txtto_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(180, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "To Row No.";
            // 
            // txtform
            // 
            this.txtform.Enabled = false;
            this.txtform.Location = new System.Drawing.Point(6, 30);
            this.txtform.Name = "txtform";
            this.txtform.Size = new System.Drawing.Size(134, 20);
            this.txtform.TabIndex = 8;
            this.txtform.Text = "A1";
            this.txtform.Enter += new System.EventHandler(this.txtform_Enter);
            this.txtform.Leave += new System.EventHandler(this.txtform_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Form Row No.";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ImportItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(565, 228);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.TextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ImportItem";
            this.Text = "ImportItem";
            this.Load += new System.EventHandler(this.ImportItem_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox TextBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnimport;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lblitem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtto;
        private System.Windows.Forms.TextBox txtform;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnimoortdata;
    }
}