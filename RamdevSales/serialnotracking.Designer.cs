namespace RamdevSales
{
    partial class serialnotracking
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lvstockout = new System.Windows.Forms.ListView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvstockin = new System.Windows.Forms.ListView();
            this.btnok = new System.Windows.Forms.Button();
            this.txtserialno = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btncancel = new System.Windows.Forms.Button();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtstatus = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btnok);
            this.groupBox1.Controls.Add(this.txtserialno);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(843, 405);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(352, 227);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 18);
            this.label3.TabIndex = 211;
            this.label3.Text = "Serial No.Stock Out";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(352, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 18);
            this.label2.TabIndex = 210;
            this.label2.Text = "Serial No.Stock In";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lvstockout);
            this.groupBox3.Location = new System.Drawing.Point(6, 244);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(831, 154);
            this.groupBox3.TabIndex = 208;
            this.groupBox3.TabStop = false;
            // 
            // lvstockout
            // 
            this.lvstockout.BackColor = System.Drawing.Color.White;
            this.lvstockout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvstockout.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvstockout.ForeColor = System.Drawing.Color.Navy;
            this.lvstockout.FullRowSelect = true;
            this.lvstockout.GridLines = true;
            this.lvstockout.HideSelection = false;
            this.lvstockout.Location = new System.Drawing.Point(6, 18);
            this.lvstockout.MultiSelect = false;
            this.lvstockout.Name = "lvstockout";
            this.lvstockout.Size = new System.Drawing.Size(819, 129);
            this.lvstockout.TabIndex = 3;
            this.lvstockout.UseCompatibleStateImageBehavior = false;
            this.lvstockout.View = System.Windows.Forms.View.Details;
            this.lvstockout.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvstockout_KeyDown);
            this.lvstockout.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvstockout_MouseDoubleClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lvstockin);
            this.groupBox2.Location = new System.Drawing.Point(6, 84);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(831, 148);
            this.groupBox2.TabIndex = 206;
            this.groupBox2.TabStop = false;
            // 
            // lvstockin
            // 
            this.lvstockin.BackColor = System.Drawing.Color.White;
            this.lvstockin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvstockin.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvstockin.ForeColor = System.Drawing.Color.Navy;
            this.lvstockin.FullRowSelect = true;
            this.lvstockin.GridLines = true;
            this.lvstockin.HideSelection = false;
            this.lvstockin.Location = new System.Drawing.Point(6, 16);
            this.lvstockin.MultiSelect = false;
            this.lvstockin.Name = "lvstockin";
            this.lvstockin.Size = new System.Drawing.Size(819, 124);
            this.lvstockin.TabIndex = 2;
            this.lvstockin.UseCompatibleStateImageBehavior = false;
            this.lvstockin.View = System.Windows.Forms.View.Details;
            this.lvstockin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvstockin_KeyDown);
            this.lvstockin.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvstockin_MouseDoubleClick);
            // 
            // btnok
            // 
            this.btnok.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnok.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnok.ForeColor = System.Drawing.Color.White;
            this.btnok.Location = new System.Drawing.Point(518, 36);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(97, 34);
            this.btnok.TabIndex = 1;
            this.btnok.Text = "&OK";
            this.btnok.UseVisualStyleBackColor = false;
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            this.btnok.Enter += new System.EventHandler(this.btnok_Enter);
            this.btnok.Leave += new System.EventHandler(this.btnok_Leave);
            this.btnok.MouseEnter += new System.EventHandler(this.btnok_MouseEnter);
            this.btnok.MouseLeave += new System.EventHandler(this.btnok_MouseLeave);
            this.btnok.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnok_MouseMove);
            // 
            // txtserialno
            // 
            this.txtserialno.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtserialno.Location = new System.Drawing.Point(12, 40);
            this.txtserialno.Name = "txtserialno";
            this.txtserialno.Size = new System.Drawing.Size(500, 27);
            this.txtserialno.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(104, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(255, 18);
            this.label1.TabIndex = 203;
            this.label1.Text = "Enter or scan your serial No.Here";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btncancel);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(843, 456);
            this.panel1.TabIndex = 212;
            // 
            // btncancel
            // 
            this.btncancel.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btncancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncancel.ForeColor = System.Drawing.Color.White;
            this.btncancel.Location = new System.Drawing.Point(732, 35);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(97, 34);
            this.btncancel.TabIndex = 17;
            this.btncancel.Text = "&Close";
            this.btncancel.UseVisualStyleBackColor = false;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            this.btncancel.Enter += new System.EventHandler(this.btncancel_Enter);
            this.btncancel.Leave += new System.EventHandler(this.btncancel_Leave);
            this.btncancel.MouseEnter += new System.EventHandler(this.btncancel_MouseEnter);
            this.btncancel.MouseLeave += new System.EventHandler(this.btncancel_MouseLeave);
            // 
            // textBox14
            // 
            this.textBox14.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.textBox14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox14.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox14.ForeColor = System.Drawing.Color.White;
            this.textBox14.Location = new System.Drawing.Point(12, 11);
            this.textBox14.Name = "textBox14";
            this.textBox14.ReadOnly = true;
            this.textBox14.Size = new System.Drawing.Size(843, 31);
            this.textBox14.TabIndex = 203;
            this.textBox14.TabStop = false;
            this.textBox14.Text = "SERIAL NO.TRACKING";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(21, 450);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 18);
            this.label4.TabIndex = 213;
            this.label4.Text = "Serial No Status";
            // 
            // txtstatus
            // 
            this.txtstatus.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtstatus.ForeColor = System.Drawing.Color.Red;
            this.txtstatus.Location = new System.Drawing.Point(154, 447);
            this.txtstatus.Name = "txtstatus";
            this.txtstatus.Size = new System.Drawing.Size(688, 27);
            this.txtstatus.TabIndex = 213;
            // 
            // serialnotracking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(861, 554);
            this.ControlBox = false;
            this.Controls.Add(this.txtstatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox14);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "serialnotracking";
            this.Text = "serialnotracking";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtserialno;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Button btnok;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        internal System.Windows.Forms.ListView lvstockout;
        internal System.Windows.Forms.ListView lvstockin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btncancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtstatus;
    }
}