namespace RamdevSales
{
    partial class PromotionOffer
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkdiscount = new System.Windows.Forms.CheckBox();
            this.lblfreeitem = new System.Windows.Forms.Label();
            this.lblqty = new System.Windows.Forms.Label();
            this.txtqty = new System.Windows.Forms.TextBox();
            this.pnlallitem1 = new System.Windows.Forms.Panel();
            this.lvallitem1 = new System.Windows.Forms.ListView();
            this.pnlallitem = new System.Windows.Forms.Panel();
            this.lvallitem = new System.Windows.Forms.ListView();
            this.lblenterdis = new System.Windows.Forms.Label();
            this.txtdiscount = new System.Windows.Forms.TextBox();
            this.cmbdiscount = new System.Windows.Forms.ComboBox();
            this.lbldisvalue = new System.Windows.Forms.Label();
            this.lblfreeqty = new System.Windows.Forms.Label();
            this.txtfreeqty = new System.Windows.Forms.TextBox();
            this.txtfreeitem = new System.Windows.Forms.TextBox();
            this.chkfreeitem = new System.Windows.Forms.CheckBox();
            this.btnsave = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.DTPFrom = new System.Windows.Forms.DateTimePicker();
            this.txtitemname = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.DTPTo = new System.Windows.Forms.DateTimePicker();
            this.panel1.SuspendLayout();
            this.pnlallitem1.SuspendLayout();
            this.pnlallitem.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.Highlight;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox7.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.ForeColor = System.Drawing.Color.White;
            this.textBox7.Location = new System.Drawing.Point(1, 0);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(939, 31);
            this.textBox7.TabIndex = 200;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "PROMOTION OFFERS";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.chkdiscount);
            this.panel1.Controls.Add(this.lblfreeitem);
            this.panel1.Controls.Add(this.lblqty);
            this.panel1.Controls.Add(this.txtqty);
            this.panel1.Controls.Add(this.pnlallitem1);
            this.panel1.Controls.Add(this.pnlallitem);
            this.panel1.Controls.Add(this.lblenterdis);
            this.panel1.Controls.Add(this.txtdiscount);
            this.panel1.Controls.Add(this.cmbdiscount);
            this.panel1.Controls.Add(this.lbldisvalue);
            this.panel1.Controls.Add(this.lblfreeqty);
            this.panel1.Controls.Add(this.txtfreeqty);
            this.panel1.Controls.Add(this.txtfreeitem);
            this.panel1.Controls.Add(this.chkfreeitem);
            this.panel1.Controls.Add(this.btnsave);
            this.panel1.Controls.Add(this.btnclose);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.DTPFrom);
            this.panel1.Controls.Add(this.txtitemname);
            this.panel1.Controls.Add(this.Label1);
            this.panel1.Controls.Add(this.Label2);
            this.panel1.Controls.Add(this.DTPTo);
            this.panel1.Location = new System.Drawing.Point(1, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(939, 443);
            this.panel1.TabIndex = 201;
            // 
            // chkdiscount
            // 
            this.chkdiscount.AutoSize = true;
            this.chkdiscount.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkdiscount.Location = new System.Drawing.Point(350, 32);
            this.chkdiscount.Name = "chkdiscount";
            this.chkdiscount.Size = new System.Drawing.Size(163, 18);
            this.chkdiscount.TabIndex = 269;
            this.chkdiscount.Text = "Add Discount On Item";
            this.chkdiscount.UseVisualStyleBackColor = true;
            this.chkdiscount.CheckedChanged += new System.EventHandler(this.chkdiscount_CheckedChanged);
            // 
            // lblfreeitem
            // 
            this.lblfreeitem.AutoSize = true;
            this.lblfreeitem.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfreeitem.Location = new System.Drawing.Point(476, 58);
            this.lblfreeitem.Name = "lblfreeitem";
            this.lblfreeitem.Size = new System.Drawing.Size(117, 14);
            this.lblfreeitem.TabIndex = 268;
            this.lblfreeitem.Text = "Select Free Item";
            this.lblfreeitem.Visible = false;
            // 
            // lblqty
            // 
            this.lblqty.AutoSize = true;
            this.lblqty.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblqty.Location = new System.Drawing.Point(392, 58);
            this.lblqty.Name = "lblqty";
            this.lblqty.Size = new System.Drawing.Size(31, 14);
            this.lblqty.TabIndex = 267;
            this.lblqty.Text = "Qty";
            this.lblqty.Visible = false;
            // 
            // txtqty
            // 
            this.txtqty.BackColor = System.Drawing.SystemColors.Window;
            this.txtqty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtqty.Font = new System.Drawing.Font("Verdana", 8F);
            this.txtqty.Location = new System.Drawing.Point(359, 74);
            this.txtqty.Name = "txtqty";
            this.txtqty.Size = new System.Drawing.Size(111, 20);
            this.txtqty.TabIndex = 266;
            this.txtqty.Visible = false;
            this.txtqty.Enter += new System.EventHandler(this.txtqty_Enter);
            this.txtqty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtqty_KeyDown);
            this.txtqty.Leave += new System.EventHandler(this.txtqty_Leave);
            // 
            // pnlallitem1
            // 
            this.pnlallitem1.Controls.Add(this.lvallitem1);
            this.pnlallitem1.Location = new System.Drawing.Point(471, 96);
            this.pnlallitem1.Name = "pnlallitem1";
            this.pnlallitem1.Size = new System.Drawing.Size(346, 168);
            this.pnlallitem1.TabIndex = 261;
            this.pnlallitem1.Visible = false;
            // 
            // lvallitem1
            // 
            this.lvallitem1.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.lvallitem1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvallitem1.BackColor = System.Drawing.Color.White;
            this.lvallitem1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvallitem1.Font = new System.Drawing.Font("Arial", 9.5F, System.Drawing.FontStyle.Bold);
            this.lvallitem1.ForeColor = System.Drawing.Color.Maroon;
            this.lvallitem1.FullRowSelect = true;
            this.lvallitem1.GridLines = true;
            this.lvallitem1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvallitem1.HideSelection = false;
            this.lvallitem1.Location = new System.Drawing.Point(1, 1);
            this.lvallitem1.MultiSelect = false;
            this.lvallitem1.Name = "lvallitem1";
            this.lvallitem1.Size = new System.Drawing.Size(341, 162);
            this.lvallitem1.TabIndex = 260;
            this.lvallitem1.UseCompatibleStateImageBehavior = false;
            this.lvallitem1.View = System.Windows.Forms.View.Details;
            this.lvallitem1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvallitem1_KeyDown);
            this.lvallitem1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvallitem1_MouseDoubleClick);
            // 
            // pnlallitem
            // 
            this.pnlallitem.Controls.Add(this.lvallitem);
            this.pnlallitem.Location = new System.Drawing.Point(11, 96);
            this.pnlallitem.Name = "pnlallitem";
            this.pnlallitem.Size = new System.Drawing.Size(346, 168);
            this.pnlallitem.TabIndex = 205;
            this.pnlallitem.Visible = false;
            // 
            // lvallitem
            // 
            this.lvallitem.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.lvallitem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvallitem.BackColor = System.Drawing.Color.White;
            this.lvallitem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvallitem.Font = new System.Drawing.Font("Arial", 9.5F, System.Drawing.FontStyle.Bold);
            this.lvallitem.ForeColor = System.Drawing.Color.Maroon;
            this.lvallitem.FullRowSelect = true;
            this.lvallitem.GridLines = true;
            this.lvallitem.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvallitem.HideSelection = false;
            this.lvallitem.Location = new System.Drawing.Point(1, 1);
            this.lvallitem.MultiSelect = false;
            this.lvallitem.Name = "lvallitem";
            this.lvallitem.Size = new System.Drawing.Size(341, 162);
            this.lvallitem.TabIndex = 260;
            this.lvallitem.UseCompatibleStateImageBehavior = false;
            this.lvallitem.View = System.Windows.Forms.View.Details;
            this.lvallitem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvallitem_KeyDown);
            this.lvallitem.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvallitem_MouseDoubleClick);
            // 
            // lblenterdis
            // 
            this.lblenterdis.AutoSize = true;
            this.lblenterdis.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblenterdis.Location = new System.Drawing.Point(240, 98);
            this.lblenterdis.Name = "lblenterdis";
            this.lblenterdis.Size = new System.Drawing.Size(186, 16);
            this.lblenterdis.TabIndex = 265;
            this.lblenterdis.Text = "Enter Discount(%)/Amount";
            this.lblenterdis.Visible = false;
            // 
            // txtdiscount
            // 
            this.txtdiscount.BackColor = System.Drawing.SystemColors.Window;
            this.txtdiscount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtdiscount.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdiscount.Location = new System.Drawing.Point(242, 118);
            this.txtdiscount.Name = "txtdiscount";
            this.txtdiscount.Size = new System.Drawing.Size(184, 23);
            this.txtdiscount.TabIndex = 264;
            this.txtdiscount.Visible = false;
            this.txtdiscount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtdiscount_KeyDown);
            // 
            // cmbdiscount
            // 
            this.cmbdiscount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbdiscount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbdiscount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbdiscount.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbdiscount.FormattingEnabled = true;
            this.cmbdiscount.Items.AddRange(new object[] {
            "Enter Discount(%)",
            "Enter Discount Amount"});
            this.cmbdiscount.Location = new System.Drawing.Point(12, 117);
            this.cmbdiscount.Name = "cmbdiscount";
            this.cmbdiscount.Size = new System.Drawing.Size(224, 24);
            this.cmbdiscount.TabIndex = 261;
            this.cmbdiscount.Visible = false;
            this.cmbdiscount.SelectedIndexChanged += new System.EventHandler(this.cmbdiscount_SelectedIndexChanged);
            this.cmbdiscount.Enter += new System.EventHandler(this.cmbdiscount_Enter);
            this.cmbdiscount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbdiscount_KeyDown);
            // 
            // lbldisvalue
            // 
            this.lbldisvalue.AutoSize = true;
            this.lbldisvalue.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldisvalue.Location = new System.Drawing.Point(15, 97);
            this.lbldisvalue.Name = "lbldisvalue";
            this.lbldisvalue.Size = new System.Drawing.Size(106, 16);
            this.lbldisvalue.TabIndex = 262;
            this.lbldisvalue.Text = "Discount Value";
            this.lbldisvalue.Visible = false;
            // 
            // lblfreeqty
            // 
            this.lblfreeqty.AutoSize = true;
            this.lblfreeqty.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfreeqty.Location = new System.Drawing.Point(827, 58);
            this.lblfreeqty.Name = "lblfreeqty";
            this.lblfreeqty.Size = new System.Drawing.Size(107, 14);
            this.lblfreeqty.TabIndex = 263;
            this.lblfreeqty.Text = "No Of Free Qty";
            this.lblfreeqty.Visible = false;
            // 
            // txtfreeqty
            // 
            this.txtfreeqty.BackColor = System.Drawing.SystemColors.Window;
            this.txtfreeqty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtfreeqty.Font = new System.Drawing.Font("Verdana", 8F);
            this.txtfreeqty.Location = new System.Drawing.Point(823, 74);
            this.txtfreeqty.Name = "txtfreeqty";
            this.txtfreeqty.Size = new System.Drawing.Size(111, 20);
            this.txtfreeqty.TabIndex = 262;
            this.txtfreeqty.Visible = false;
            this.txtfreeqty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtfreeqty_KeyDown);
            // 
            // txtfreeitem
            // 
            this.txtfreeitem.BackColor = System.Drawing.SystemColors.Window;
            this.txtfreeitem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtfreeitem.Font = new System.Drawing.Font("Verdana", 8F);
            this.txtfreeitem.Location = new System.Drawing.Point(471, 74);
            this.txtfreeitem.Name = "txtfreeitem";
            this.txtfreeitem.Size = new System.Drawing.Size(346, 20);
            this.txtfreeitem.TabIndex = 208;
            this.txtfreeitem.Visible = false;
            this.txtfreeitem.TextChanged += new System.EventHandler(this.txtfreeitem_TextChanged);
            this.txtfreeitem.Enter += new System.EventHandler(this.txtfreeitem_Enter);
            this.txtfreeitem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtfreeitem_KeyDown);
            this.txtfreeitem.Leave += new System.EventHandler(this.txtfreeitem_Leave);
            // 
            // chkfreeitem
            // 
            this.chkfreeitem.AutoSize = true;
            this.chkfreeitem.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkfreeitem.Location = new System.Drawing.Point(350, 11);
            this.chkfreeitem.Name = "chkfreeitem";
            this.chkfreeitem.Size = new System.Drawing.Size(269, 18);
            this.chkfreeitem.TabIndex = 207;
            this.chkfreeitem.Text = "Free Other Item On This Selected Item";
            this.chkfreeitem.UseVisualStyleBackColor = true;
            this.chkfreeitem.CheckedChanged += new System.EventHandler(this.chkfreeitem_CheckedChanged);
            // 
            // btnsave
            // 
            this.btnsave.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnsave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsave.ForeColor = System.Drawing.Color.White;
            this.btnsave.Location = new System.Drawing.Point(736, 20);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(97, 34);
            this.btnsave.TabIndex = 206;
            this.btnsave.Text = "&Submit";
            this.btnsave.UseVisualStyleBackColor = false;
            this.btnsave.Enter += new System.EventHandler(this.btnsave_Enter);
            this.btnsave.Leave += new System.EventHandler(this.btnsave_Leave);
            this.btnsave.MouseEnter += new System.EventHandler(this.btnsave_MouseEnter);
            this.btnsave.MouseLeave += new System.EventHandler(this.btnsave_MouseLeave);
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(839, 20);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(97, 34);
            this.btnclose.TabIndex = 202;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            this.btnclose.Enter += new System.EventHandler(this.btnclose_Enter);
            this.btnclose.Leave += new System.EventHandler(this.btnclose_Leave);
            this.btnclose.MouseEnter += new System.EventHandler(this.btnclose_MouseEnter);
            this.btnclose.MouseLeave += new System.EventHandler(this.btnclose_MouseLeave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 14);
            this.label3.TabIndex = 204;
            this.label3.Text = "Select Item";
            // 
            // DTPFrom
            // 
            this.DTPFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFrom.Location = new System.Drawing.Point(11, 32);
            this.DTPFrom.Name = "DTPFrom";
            this.DTPFrom.Size = new System.Drawing.Size(154, 22);
            this.DTPFrom.TabIndex = 4;
            this.DTPFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTPFrom_KeyDown);
            // 
            // txtitemname
            // 
            this.txtitemname.BackColor = System.Drawing.SystemColors.Window;
            this.txtitemname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtitemname.Font = new System.Drawing.Font("Verdana", 8F);
            this.txtitemname.Location = new System.Drawing.Point(11, 74);
            this.txtitemname.Name = "txtitemname";
            this.txtitemname.Size = new System.Drawing.Size(346, 20);
            this.txtitemname.TabIndex = 203;
            this.txtitemname.TextChanged += new System.EventHandler(this.txtitemname_TextChanged);
            this.txtitemname.Enter += new System.EventHandler(this.txtitemname_Enter);
            this.txtitemname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtitemname_KeyDown);
            this.txtitemname.Leave += new System.EventHandler(this.txtitemname_Leave);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(27, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(124, 16);
            this.Label1.TabIndex = 3;
            this.Label1.Text = "Offer From Date";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(201, 10);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(103, 16);
            this.Label2.TabIndex = 5;
            this.Label2.Text = "Offer To Date";
            // 
            // DTPTo
            // 
            this.DTPTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPTo.Location = new System.Drawing.Point(177, 31);
            this.DTPTo.Name = "DTPTo";
            this.DTPTo.Size = new System.Drawing.Size(154, 22);
            this.DTPTo.TabIndex = 6;
            this.DTPTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTPTo_KeyDown);
            // 
            // PromotionOffer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 486);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PromotionOffer";
            this.Text = "PromotionOffer";
            this.Load += new System.EventHandler(this.PromotionOffer_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlallitem1.ResumeLayout(false);
            this.pnlallitem.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.DateTimePicker DTPFrom;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.DateTimePicker DTPTo;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox txtitemname;
        private System.Windows.Forms.Panel pnlallitem;
        internal System.Windows.Forms.ListView lvallitem;
        internal System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.CheckBox chkfreeitem;
        internal System.Windows.Forms.TextBox txtfreeitem;
        private System.Windows.Forms.Panel pnlallitem1;
        internal System.Windows.Forms.ListView lvallitem1;
        internal System.Windows.Forms.Label lblfreeqty;
        internal System.Windows.Forms.TextBox txtfreeqty;
        private System.Windows.Forms.ComboBox cmbdiscount;
        private System.Windows.Forms.Label lbldisvalue;
        private System.Windows.Forms.Label lblenterdis;
        internal System.Windows.Forms.TextBox txtdiscount;
        internal System.Windows.Forms.Label lblqty;
        internal System.Windows.Forms.TextBox txtqty;
        internal System.Windows.Forms.Label lblfreeitem;
        private System.Windows.Forms.CheckBox chkdiscount;
    }
}