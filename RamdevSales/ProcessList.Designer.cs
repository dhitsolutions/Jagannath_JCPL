namespace RamdevSales
{
    partial class ProcessList
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
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.chkactiveprocess = new System.Windows.Forms.CheckBox();
            this.chkdisactiveprocess = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnsearch = new System.Windows.Forms.Button();
            this.lvprocess = new System.Windows.Forms.ListView();
            this.btnprint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnnew = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.Highlight;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox7.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.ForeColor = System.Drawing.Color.White;
            this.textBox7.Location = new System.Drawing.Point(1, 1);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(980, 31);
            this.textBox7.TabIndex = 176;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "LIST OF PROCESSES";
            // 
            // chkactiveprocess
            // 
            this.chkactiveprocess.AutoSize = true;
            this.chkactiveprocess.Checked = true;
            this.chkactiveprocess.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkactiveprocess.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkactiveprocess.Location = new System.Drawing.Point(11, 15);
            this.chkactiveprocess.Name = "chkactiveprocess";
            this.chkactiveprocess.Size = new System.Drawing.Size(125, 20);
            this.chkactiveprocess.TabIndex = 0;
            this.chkactiveprocess.Text = "Active Process";
            this.chkactiveprocess.UseVisualStyleBackColor = true;
            // 
            // chkdisactiveprocess
            // 
            this.chkdisactiveprocess.AutoSize = true;
            this.chkdisactiveprocess.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkdisactiveprocess.Location = new System.Drawing.Point(11, 41);
            this.chkdisactiveprocess.Name = "chkdisactiveprocess";
            this.chkdisactiveprocess.Size = new System.Drawing.Size(187, 20);
            this.chkdisactiveprocess.TabIndex = 1;
            this.chkdisactiveprocess.Text = "Deactivate This Process";
            this.chkdisactiveprocess.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnsearch);
            this.panel1.Controls.Add(this.lvprocess);
            this.panel1.Controls.Add(this.btnprint);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnnew);
            this.panel1.Controls.Add(this.chkdisactiveprocess);
            this.panel1.Controls.Add(this.chkactiveprocess);
            this.panel1.Location = new System.Drawing.Point(1, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(980, 463);
            this.panel1.TabIndex = 0;
            // 
            // btnsearch
            // 
            this.btnsearch.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnsearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsearch.ForeColor = System.Drawing.Color.White;
            this.btnsearch.Location = new System.Drawing.Point(539, 27);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(97, 34);
            this.btnsearch.TabIndex = 2;
            this.btnsearch.Text = "&OK";
            this.btnsearch.UseVisualStyleBackColor = false;
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click);
            this.btnsearch.Enter += new System.EventHandler(this.btnsearch_Enter);
            this.btnsearch.Leave += new System.EventHandler(this.btnsearch_Leave);
            this.btnsearch.MouseLeave += new System.EventHandler(this.btnsearch_MouseLeave);
            this.btnsearch.MouseHover += new System.EventHandler(this.btnsearch_MouseHover);
            // 
            // lvprocess
            // 
            this.lvprocess.BackColor = System.Drawing.SystemColors.Window;
            this.lvprocess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvprocess.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvprocess.ForeColor = System.Drawing.Color.Navy;
            this.lvprocess.FullRowSelect = true;
            this.lvprocess.GridLines = true;
            this.lvprocess.HideSelection = false;
            this.lvprocess.Location = new System.Drawing.Point(11, 67);
            this.lvprocess.MultiSelect = false;
            this.lvprocess.Name = "lvprocess";
            this.lvprocess.Size = new System.Drawing.Size(957, 385);
            this.lvprocess.TabIndex = 6;
            this.lvprocess.UseCompatibleStateImageBehavior = false;
            this.lvprocess.View = System.Windows.Forms.View.Details;
            this.lvprocess.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvprocess_KeyDown);
            this.lvprocess.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvprocess_MouseDoubleClick);
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprint.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(761, 27);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(97, 34);
            this.btnprint.TabIndex = 4;
            this.btnprint.Text = "&Print";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            this.btnprint.Enter += new System.EventHandler(this.btnprint_Enter);
            this.btnprint.Leave += new System.EventHandler(this.btnprint_Leave);
            this.btnprint.MouseLeave += new System.EventHandler(this.btnprint_MouseLeave);
            this.btnprint.MouseHover += new System.EventHandler(this.btnprint_MouseHover);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(871, 27);
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
            // btnnew
            // 
            this.btnnew.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnnew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnnew.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnnew.ForeColor = System.Drawing.Color.White;
            this.btnnew.Location = new System.Drawing.Point(651, 27);
            this.btnnew.Name = "btnnew";
            this.btnnew.Size = new System.Drawing.Size(97, 34);
            this.btnnew.TabIndex = 3;
            this.btnnew.Text = "&New";
            this.btnnew.UseVisualStyleBackColor = false;
            this.btnnew.Click += new System.EventHandler(this.btnnew_Click);
            this.btnnew.Enter += new System.EventHandler(this.btnnew_Enter);
            this.btnnew.Leave += new System.EventHandler(this.btnnew_Leave);
            this.btnnew.MouseEnter += new System.EventHandler(this.btnnew_MouseEnter);
            this.btnnew.MouseHover += new System.EventHandler(this.btnnew_MouseHover);
            // 
            // ProcessList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 496);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ProcessList";
            this.Text = "ProcessList";
            this.Load += new System.EventHandler(this.ProcessList_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.CheckBox chkactiveprocess;
        private System.Windows.Forms.CheckBox chkdisactiveprocess;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnnew;
        internal System.Windows.Forms.ListView lvprocess;
        private System.Windows.Forms.Button btnsearch;
    }
}