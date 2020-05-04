using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RamdevSales
{
    public partial class ChangePswd : Form
    {
        Connection cl = new Connection();
        DataTable dt = new DataTable();
        public ChangePswd()
        {
            InitializeComponent();
        }

        private void btnChangePswd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNewPswd.Text != null && txtOldPswd.Text != null)
                {
                    dt = cl.getdataset("Select Password from UserInfo where UserName='" + txtUserName.Text + "'");
                    if (dt.Rows.Count > 0)
                    {
                        if (txtOldPswd.Text == dt.Rows[0][0].ToString())
                        {
                            cl.execute("UPDATE UserInfo SET Password='" + txtNewPswd.Text + "' where UserName = '" + txtUserName.Text + "' AND Password='" + txtOldPswd.Text + "'");
                            MessageBox.Show("Password Updated Successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Old Password does not match. Please Try again!!!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("User does not exist. Please Add User First...");
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter the fields.");
                }
            }
            catch { }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            UserList frm = new UserList();
            frm.MdiParent = this.MdiParent;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
