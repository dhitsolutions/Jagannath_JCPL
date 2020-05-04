namespace RamdevSales
{
    partial class SelectItemBatchWise
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
            this.LVFO = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // LVFO
            // 
            this.LVFO.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.LVFO.BackColor = System.Drawing.Color.White;
            this.LVFO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LVFO.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LVFO.ForeColor = System.Drawing.Color.Maroon;
            this.LVFO.FullRowSelect = true;
            this.LVFO.GridLines = true;
            this.LVFO.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.LVFO.HideSelection = false;
            this.LVFO.Location = new System.Drawing.Point(12, 12);
            this.LVFO.MultiSelect = false;
            this.LVFO.Name = "LVFO";
            this.LVFO.Size = new System.Drawing.Size(310, 238);
            this.LVFO.TabIndex = 327;
            this.LVFO.UseCompatibleStateImageBehavior = false;
            this.LVFO.View = System.Windows.Forms.View.Details;
            this.LVFO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LVFO_KeyDown);
            this.LVFO.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LVFO_MouseDoubleClick);
            // 
            // SelectItemBatchWise
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(224)))), ((int)(((byte)(236)))));
            this.ClientSize = new System.Drawing.Size(333, 262);
            this.ControlBox = false;
            this.Controls.Add(this.LVFO);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SelectItemBatchWise";
            this.Text = "SelectItemBatchWise";
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ListView LVFO;
    }
}