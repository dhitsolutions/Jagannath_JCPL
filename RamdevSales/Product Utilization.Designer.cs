namespace RamdevSales
{
    partial class Product_Utilization
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lvproduct = new System.Windows.Forms.ListView();
            this.btnsearch = new System.Windows.Forms.Button();
            this.btnprint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.cmbitem = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lvproduct);
            this.panel1.Controls.Add(this.btnsearch);
            this.panel1.Controls.Add(this.btnprint);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.cmbitem);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(1, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1068, 497);
            this.panel1.TabIndex = 0;
            // 
            // lvproduct
            // 
            this.lvproduct.BackColor = System.Drawing.SystemColors.Window;
            this.lvproduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvproduct.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvproduct.ForeColor = System.Drawing.Color.Navy;
            this.lvproduct.FullRowSelect = true;
            this.lvproduct.GridLines = true;
            this.lvproduct.HideSelection = false;
            this.lvproduct.Location = new System.Drawing.Point(11, 65);
            this.lvproduct.MultiSelect = false;
            this.lvproduct.Name = "lvproduct";
            this.lvproduct.Size = new System.Drawing.Size(1050, 423);
            this.lvproduct.TabIndex = 4;
            this.lvproduct.UseCompatibleStateImageBehavior = false;
            this.lvproduct.View = System.Windows.Forms.View.Details;
            // 
            // btnsearch
            // 
            this.btnsearch.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnsearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsearch.ForeColor = System.Drawing.Color.White;
            this.btnsearch.Location = new System.Drawing.Point(743, 25);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(97, 34);
            this.btnsearch.TabIndex = 1;
            this.btnsearch.Text = "&OK";
            this.btnsearch.UseVisualStyleBackColor = false;
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click);
            this.btnsearch.Enter += new System.EventHandler(this.btnsearch_Enter);
            this.btnsearch.Leave += new System.EventHandler(this.btnsearch_Leave);
            this.btnsearch.MouseEnter += new System.EventHandler(this.btnsearch_MouseEnter);
            this.btnsearch.MouseLeave += new System.EventHandler(this.btnsearch_MouseLeave);
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprint.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(854, 25);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(97, 34);
            this.btnprint.TabIndex = 2;
            this.btnprint.Text = "&Print";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            this.btnprint.Enter += new System.EventHandler(this.btnprint_Enter);
            this.btnprint.Leave += new System.EventHandler(this.btnprint_Leave);
            this.btnprint.MouseEnter += new System.EventHandler(this.btnprint_MouseEnter);
            this.btnprint.MouseLeave += new System.EventHandler(this.btnprint_MouseLeave);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(964, 25);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(97, 34);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.Enter += new System.EventHandler(this.btnClose_Enter);
            this.btnClose.Leave += new System.EventHandler(this.btnClose_Leave);
            this.btnClose.MouseEnter += new System.EventHandler(this.btnClose_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            // 
            // cmbitem
            // 
            this.cmbitem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbitem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbitem.BackColor = System.Drawing.SystemColors.Window;
            this.cmbitem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbitem.FormattingEnabled = true;
            this.cmbitem.Location = new System.Drawing.Point(11, 33);
            this.cmbitem.Name = "cmbitem";
            this.cmbitem.Size = new System.Drawing.Size(347, 21);
            this.cmbitem.TabIndex = 0;
            this.cmbitem.SelectedIndexChanged += new System.EventHandler(this.cmbitem_SelectedIndexChanged);
            this.cmbitem.Enter += new System.EventHandler(this.cmbitem_Enter);
            this.cmbitem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbitem_KeyDown);
            this.cmbitem.Leave += new System.EventHandler(this.cmbitem_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(29, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 17);
            this.label4.TabIndex = 182;
            this.label4.Text = "Select Name Of Item";
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
            this.textBox7.Size = new System.Drawing.Size(1068, 31);
            this.textBox7.TabIndex = 177;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "PRODUCT UTILIZATION";
            // 
            // Product_Utilization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 532);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Product_Utilization";
            this.Text = "Product_Utilization";
            this.Load += new System.EventHandler(this.Product_Utilization_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.ComboBox cmbitem;
        internal System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnsearch;
        internal System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.ListView lvproduct;
    }
}