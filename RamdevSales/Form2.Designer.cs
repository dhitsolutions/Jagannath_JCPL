namespace RamdevSales
{
    partial class Form2
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
            this.txtlist = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtlist
            // 
            this.txtlist.AutoCompleteCustomSource.AddRange(new string[] {
            "Afghanistan",
            "Albania",
            "Algeria",
            "Andorra",
            "Angola",
            "Antigua and Barbuda",
            "Argentina",
            "Armenia",
            "Aruba",
            "Australia",
            "Austria",
            "Azerbaijan"});
            this.txtlist.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtlist.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtlist.Location = new System.Drawing.Point(67, 60);
            this.txtlist.Name = "txtlist";
            this.txtlist.Size = new System.Drawing.Size(100, 20);
            this.txtlist.TabIndex = 0;
            this.txtlist.TextChanged += new System.EventHandler(this.txtlist_TextChanged);
            this.txtlist.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtlist_KeyPress);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 479);
            this.Controls.Add(this.txtlist);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtlist;

    }
}