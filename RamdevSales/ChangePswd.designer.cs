namespace RamdevSales
{
    partial class ChangePswd
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
            this.btnBack = new System.Windows.Forms.Button();
            this.btnChangePswd = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOldPswd = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.txtNewPswd = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.Font = new System.Drawing.Font("Verdana", 10F);
            this.btnBack.Location = new System.Drawing.Point(338, 47);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(88, 40);
            this.btnBack.TabIndex = 59;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnChangePswd
            // 
            this.btnChangePswd.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangePswd.Location = new System.Drawing.Point(146, 251);
            this.btnChangePswd.Name = "btnChangePswd";
            this.btnChangePswd.Size = new System.Drawing.Size(156, 41);
            this.btnChangePswd.TabIndex = 58;
            this.btnChangePswd.Text = "Change Password";
            this.btnChangePswd.UseVisualStyleBackColor = true;
            this.btnChangePswd.Click += new System.EventHandler(this.btnChangePswd_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 10F);
            this.label2.Location = new System.Drawing.Point(12, 201);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 17);
            this.label2.TabIndex = 56;
            this.label2.Text = "New Password";
            // 
            // txtOldPswd
            // 
            this.txtOldPswd.Font = new System.Drawing.Font("Verdana", 10F);
            this.txtOldPswd.Location = new System.Drawing.Point(146, 153);
            this.txtOldPswd.Name = "txtOldPswd";
            this.txtOldPswd.PasswordChar = '*';
            this.txtOldPswd.Size = new System.Drawing.Size(280, 24);
            this.txtOldPswd.TabIndex = 55;
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("Verdana", 10F);
            this.txtUserName.Location = new System.Drawing.Point(146, 110);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(280, 24);
            this.txtUserName.TabIndex = 54;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 10F);
            this.label4.Location = new System.Drawing.Point(12, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 17);
            this.label4.TabIndex = 53;
            this.label4.Text = "Old Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 10F);
            this.label3.Location = new System.Drawing.Point(12, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 17);
            this.label3.TabIndex = 52;
            this.label3.Text = "User Name";
            // 
            // TextBox1
            // 
            this.TextBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBox1.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox1.Location = new System.Drawing.Point(-1, 0);
            this.TextBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = true;
            this.TextBox1.Size = new System.Drawing.Size(442, 40);
            this.TextBox1.TabIndex = 51;
            this.TextBox1.TabStop = false;
            this.TextBox1.Text = "Change Password";
            this.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtNewPswd
            // 
            this.txtNewPswd.Font = new System.Drawing.Font("Verdana", 10F);
            this.txtNewPswd.Location = new System.Drawing.Point(146, 198);
            this.txtNewPswd.Name = "txtNewPswd";
            this.txtNewPswd.PasswordChar = '*';
            this.txtNewPswd.Size = new System.Drawing.Size(280, 24);
            this.txtNewPswd.TabIndex = 60;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 10F);
            this.btnCancel.Location = new System.Drawing.Point(322, 251);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(104, 40);
            this.btnCancel.TabIndex = 61;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ChangePswd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 304);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtNewPswd);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnChangePswd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtOldPswd);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TextBox1);
            this.Name = "ChangePswd";
            this.Text = "ChangePswd";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnChangePswd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOldPswd;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox TextBox1;
        private System.Windows.Forms.TextBox txtNewPswd;
        private System.Windows.Forms.Button btnCancel;
    }
}