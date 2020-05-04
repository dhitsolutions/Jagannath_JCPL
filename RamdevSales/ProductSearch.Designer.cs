namespace RamdevSales
{
    partial class ProductSearch
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
            this.TxtProdcodeSearch = new System.Windows.Forms.TextBox();
            this.LVstudSearch = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // TxtProdcodeSearch
            // 
            this.TxtProdcodeSearch.BackColor = System.Drawing.Color.Wheat;
            this.TxtProdcodeSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtProdcodeSearch.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtProdcodeSearch.Location = new System.Drawing.Point(0, 0);
            this.TxtProdcodeSearch.Name = "TxtProdcodeSearch";
            this.TxtProdcodeSearch.Size = new System.Drawing.Size(910, 23);
            this.TxtProdcodeSearch.TabIndex = 13;
            this.TxtProdcodeSearch.TextChanged += new System.EventHandler(this.TxtProdcodeSearch_TextChanged);
            // 
            // LVstudSearch
            // 
            this.LVstudSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LVstudSearch.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LVstudSearch.FullRowSelect = true;
            this.LVstudSearch.GridLines = true;
            this.LVstudSearch.HideSelection = false;
            this.LVstudSearch.Location = new System.Drawing.Point(0, 24);
            this.LVstudSearch.MultiSelect = false;
            this.LVstudSearch.Name = "LVstudSearch";
            this.LVstudSearch.Size = new System.Drawing.Size(910, 438);
            this.LVstudSearch.TabIndex = 12;
            this.LVstudSearch.UseCompatibleStateImageBehavior = false;
            this.LVstudSearch.View = System.Windows.Forms.View.Details;
            this.LVstudSearch.SelectedIndexChanged += new System.EventHandler(this.LVstudSearch_SelectedIndexChanged);
            this.LVstudSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LVstudSearch_KeyDown);
            this.LVstudSearch.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LVstudSearch_MouseDoubleClick);
            // 
            // ProductSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 462);
            this.Controls.Add(this.TxtProdcodeSearch);
            this.Controls.Add(this.LVstudSearch);
            this.Name = "ProductSearch";
            this.Text = "ProductSearch";
            this.Load += new System.EventHandler(this.ProductSearch_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox TxtProdcodeSearch;
        internal System.Windows.Forms.ListView LVstudSearch;
    }
}