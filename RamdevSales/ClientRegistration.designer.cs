namespace RamdevSales
{
    partial class ClientRegistration
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
            this.components = new System.ComponentModel.Container();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnprint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnimport = new System.Windows.Forms.Button();
            this.btnsearch = new System.Windows.Forms.Button();
            this.btnexport = new System.Windows.Forms.Button();
            this.txtser = new System.Windows.Forms.TextBox();
            this.txtsearch = new System.Windows.Forms.ComboBox();
            this.btnnew = new System.Windows.Forms.Button();
            this.LVclient = new System.Windows.Forms.ListView();
            this.pnllist = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox2.SuspendLayout();
            this.pnllist.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBox1
            // 
            this.TextBox1.BackColor = System.Drawing.SystemColors.Highlight;
            this.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBox1.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox1.ForeColor = System.Drawing.Color.White;
            this.TextBox1.Location = new System.Drawing.Point(0, 0);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = true;
            this.TextBox1.Size = new System.Drawing.Size(1009, 31);
            this.TextBox1.TabIndex = 11;
            this.TextBox1.TabStop = false;
            this.TextBox1.Text = "ACCOUNTS";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.btnprint);
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Controls.Add(this.btnimport);
            this.groupBox2.Controls.Add(this.btnsearch);
            this.groupBox2.Controls.Add(this.btnexport);
            this.groupBox2.Controls.Add(this.txtser);
            this.groupBox2.Controls.Add(this.txtsearch);
            this.groupBox2.Controls.Add(this.btnnew);
            this.groupBox2.Controls.Add(this.LVclient);
            this.groupBox2.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.groupBox2.Location = new System.Drawing.Point(3, -1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1001, 505);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprint.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(796, 10);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(97, 34);
            this.btnprint.TabIndex = 22;
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
            this.btnClose.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(896, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(97, 34);
            this.btnClose.TabIndex = 21;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.Enter += new System.EventHandler(this.btnClose_Enter);
            this.btnClose.Leave += new System.EventHandler(this.btnClose_Leave);
            this.btnClose.MouseEnter += new System.EventHandler(this.btnClose_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            // 
            // btnimport
            // 
            this.btnimport.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnimport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnimport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnimport.ForeColor = System.Drawing.Color.White;
            this.btnimport.Location = new System.Drawing.Point(594, 10);
            this.btnimport.Name = "btnimport";
            this.btnimport.Size = new System.Drawing.Size(97, 34);
            this.btnimport.TabIndex = 19;
            this.btnimport.Text = "Import Item";
            this.btnimport.UseVisualStyleBackColor = false;
            this.btnimport.Click += new System.EventHandler(this.btnimport_Click);
            this.btnimport.Enter += new System.EventHandler(this.btnimport_Enter);
            this.btnimport.Leave += new System.EventHandler(this.btnimport_Leave);
            this.btnimport.MouseEnter += new System.EventHandler(this.btnimport_MouseEnter);
            this.btnimport.MouseLeave += new System.EventHandler(this.btnimport_MouseLeave);
            // 
            // btnsearch
            // 
            this.btnsearch.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnsearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsearch.ForeColor = System.Drawing.Color.White;
            this.btnsearch.Location = new System.Drawing.Point(386, 10);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(103, 34);
            this.btnsearch.TabIndex = 4;
            this.btnsearch.Text = "&OK";
            this.btnsearch.UseVisualStyleBackColor = false;
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click);
            this.btnsearch.Enter += new System.EventHandler(this.btnsearch_Enter);
            this.btnsearch.Leave += new System.EventHandler(this.btnsearch_Leave);
            this.btnsearch.MouseEnter += new System.EventHandler(this.btnsearch_MouseEnter);
            this.btnsearch.MouseLeave += new System.EventHandler(this.btnsearch_MouseLeave);
            // 
            // btnexport
            // 
            this.btnexport.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnexport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnexport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnexport.ForeColor = System.Drawing.Color.White;
            this.btnexport.Location = new System.Drawing.Point(695, 10);
            this.btnexport.Name = "btnexport";
            this.btnexport.Size = new System.Drawing.Size(97, 34);
            this.btnexport.TabIndex = 20;
            this.btnexport.Text = "Export Item";
            this.btnexport.UseVisualStyleBackColor = false;
            this.btnexport.Click += new System.EventHandler(this.btnexport_Click);
            this.btnexport.Enter += new System.EventHandler(this.btnexport_Enter);
            this.btnexport.Leave += new System.EventHandler(this.btnexport_Leave);
            this.btnexport.MouseEnter += new System.EventHandler(this.btnexport_MouseEnter);
            this.btnexport.MouseLeave += new System.EventHandler(this.btnexport_MouseLeave);
            // 
            // txtser
            // 
            this.txtser.BackColor = System.Drawing.Color.White;
            this.txtser.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtser.Location = new System.Drawing.Point(185, 16);
            this.txtser.Name = "txtser";
            this.txtser.Size = new System.Drawing.Size(193, 23);
            this.txtser.TabIndex = 3;
            this.txtser.TextChanged += new System.EventHandler(this.txtser_TextChanged);
            this.txtser.Enter += new System.EventHandler(this.txtser_Enter);
            this.txtser.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtser_KeyDown);
            this.txtser.Leave += new System.EventHandler(this.txtser_Leave);
            // 
            // txtsearch
            // 
            this.txtsearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtsearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtsearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtsearch.FormattingEnabled = true;
            this.txtsearch.Location = new System.Drawing.Point(8, 16);
            this.txtsearch.Name = "txtsearch";
            this.txtsearch.Size = new System.Drawing.Size(171, 24);
            this.txtsearch.TabIndex = 2;
            this.txtsearch.SelectedIndexChanged += new System.EventHandler(this.txtsearch_SelectedIndexChanged);
            this.txtsearch.Enter += new System.EventHandler(this.txtsearch_Enter);
            this.txtsearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtsearch_KeyDown);
            this.txtsearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtsearch_KeyUp);
            this.txtsearch.Leave += new System.EventHandler(this.txtsearch_Leave);
            // 
            // btnnew
            // 
            this.btnnew.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnnew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnnew.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnnew.ForeColor = System.Drawing.Color.White;
            this.btnnew.Location = new System.Drawing.Point(493, 10);
            this.btnnew.Name = "btnnew";
            this.btnnew.Size = new System.Drawing.Size(97, 34);
            this.btnnew.TabIndex = 0;
            this.btnnew.Text = "&New";
            this.btnnew.UseVisualStyleBackColor = false;
            this.btnnew.Click += new System.EventHandler(this.btnnew_Click);
            this.btnnew.Enter += new System.EventHandler(this.btnnew_Enter);
            this.btnnew.Leave += new System.EventHandler(this.btnnew_Leave);
            this.btnnew.MouseEnter += new System.EventHandler(this.btnnew_MouseEnter);
            this.btnnew.MouseLeave += new System.EventHandler(this.btnnew_MouseLeave);
            // 
            // LVclient
            // 
            this.LVclient.BackColor = System.Drawing.SystemColors.Window;
            this.LVclient.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LVclient.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LVclient.ForeColor = System.Drawing.Color.Navy;
            this.LVclient.FullRowSelect = true;
            this.LVclient.GridLines = true;
            this.LVclient.HideSelection = false;
            this.LVclient.Location = new System.Drawing.Point(6, 50);
            this.LVclient.MultiSelect = false;
            this.LVclient.Name = "LVclient";
            this.LVclient.Size = new System.Drawing.Size(982, 445);
            this.LVclient.TabIndex = 1;
            this.LVclient.UseCompatibleStateImageBehavior = false;
            this.LVclient.View = System.Windows.Forms.View.Details;
            this.LVclient.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LVclient_KeyDown);
            this.LVclient.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LVclient_MouseDoubleClick);
            // 
            // pnllist
            // 
            this.pnllist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnllist.Controls.Add(this.groupBox2);
            this.pnllist.Location = new System.Drawing.Point(0, 27);
            this.pnllist.Name = "pnllist";
            this.pnllist.Size = new System.Drawing.Size(1009, 514);
            this.pnllist.TabIndex = 18;
            this.pnllist.Paint += new System.Windows.Forms.PaintEventHandler(this.pnllist_Paint);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ClientRegistration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1021, 546);
            this.ControlBox = false;
            this.Controls.Add(this.pnllist);
            this.Controls.Add(this.TextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ClientRegistration";
            this.Text = "ClientRegistration";
            this.Load += new System.EventHandler(this.ClientRegistration_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.pnllist.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox TextBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.ListView LVclient;
        private System.Windows.Forms.Button btnnew;
        private System.Windows.Forms.TextBox txtser;
        private System.Windows.Forms.ComboBox txtsearch;
        private System.Windows.Forms.Button btnsearch;
        private System.Windows.Forms.Panel pnllist;
        private System.Windows.Forms.Button btnimport;
        private System.Windows.Forms.Button btnexport;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.Button btnprint;

    }
}