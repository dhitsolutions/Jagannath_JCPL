namespace RamdevSales
{
    partial class ClientSearch
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
            this.TxtclientSearch = new System.Windows.Forms.TextBox();
            this.LVclientSearch = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // TxtclientSearch
            // 
            this.TxtclientSearch.BackColor = System.Drawing.Color.Wheat;
            this.TxtclientSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtclientSearch.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtclientSearch.Location = new System.Drawing.Point(0, 0);
            this.TxtclientSearch.Name = "TxtclientSearch";
            this.TxtclientSearch.Size = new System.Drawing.Size(634, 23);
            this.TxtclientSearch.TabIndex = 13;
            // 
            // LVclientSearch
            // 
            this.LVclientSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LVclientSearch.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LVclientSearch.FullRowSelect = true;
            this.LVclientSearch.GridLines = true;
            this.LVclientSearch.HideSelection = false;
            this.LVclientSearch.Location = new System.Drawing.Point(0, 24);
            this.LVclientSearch.MultiSelect = false;
            this.LVclientSearch.Name = "LVclientSearch";
            this.LVclientSearch.Size = new System.Drawing.Size(634, 438);
            this.LVclientSearch.TabIndex = 12;
            this.LVclientSearch.UseCompatibleStateImageBehavior = false;
            this.LVclientSearch.View = System.Windows.Forms.View.Details;
            // 
            // ClientSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 462);
            this.Controls.Add(this.TxtclientSearch);
            this.Controls.Add(this.LVclientSearch);
            this.Name = "ClientSearch";
            this.Text = "ClientSearch";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox TxtclientSearch;
        internal System.Windows.Forms.ListView LVclientSearch;
    }
}