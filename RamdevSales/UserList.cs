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
    public partial class UserList : Form
    {
        Connection con = new Connection();
        DataTable dt = new DataTable();
        public static string iid = "";
        private Master master;
        private TabControl tabControl;
        public UserList()
        {
            InitializeComponent();
            listView1.Columns.Add("User Id", 50, HorizontalAlignment.Center);
            listView1.Columns.Add("User Name", 200, HorizontalAlignment.Center);
            listView1.Columns.Add("Position", 250, HorizontalAlignment.Center);
        }

        public UserList(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            listView1.Columns.Add("User Id", 50, HorizontalAlignment.Center);
            listView1.Columns.Add("User Name", 200, HorizontalAlignment.Center);
            listView1.Columns.Add("Position", 250, HorizontalAlignment.Center);
            this.master = master;
            this.tabControl = tabControl;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            UserInfo frm = new UserInfo(master, tabControl);
            master.AddNewTab(frm);
            //frm.MdiParent = this.MdiParent;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            //    String str = listView1.Items[listView1.FocusedItem.Index].SubItems[0].Text;

            //    UserInfo bd = new UserInfo(this);
            //    bd.updatemode(str, listView1.Items[listView1.FocusedItem.Index].SubItems[0].Text, 1);
            //    bd.Show();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    master.RemoveCurrentTab();
                }
                return true;
            }


            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void bindlist()
        {
            try
            {
                listView1.Items.Clear();
                dt = con.getdataset("Select UserId,UserName,Position from userInfo where CompanyId=" + Master.companyId + " and isActive=1");

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        listView1.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                        listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                        listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());

                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error:" + ex.Message);
            }
        }
        DataTable userrights = new DataTable();
        private void UserList_Load(object sender, EventArgs e)
        {
            userrights = con.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[8]["a"].ToString() == "False")
                {
                    btnNew.Enabled = false;
                }
            }
            bindlist();
            btnNew.Focus();
            this.ActiveControl = btnNew;
        }
        public void open()
        {
            try
            {
                //this.Enabled = false;
                iid = listView1.Items[listView1.FocusedItem.Index].SubItems[0].Text;

                UserInfo dlg = new UserInfo(master, tabControl);


                dlg.updatemode(1, iid);
                master.AddNewTab(dlg);
                dlg.Show();
            }
            finally
            {
                //this.Enabled = true;
            }
        }
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[7]["v"].ToString() == "True" || userrights.Rows[7]["u"].ToString() == "True")
                {
                    open();
                }
                else
                {
                    MessageBox.Show("You don't have Permission To View");
                    return;
                }
            }
        }

        

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[7]["v"].ToString() == "True" || userrights.Rows[7]["u"].ToString() == "True")
                    {
                        open();
                    }
                    else
                    {
                        MessageBox.Show("You don't have Permission To View");
                        return;
                    }
                }
            }
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    iid = listView1.Items[listView1.FocusedItem.Index].SubItems[0].Text;
                    DialogResult dr = MessageBox.Show("Do you want to Delete?", "Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        con.execute("Update UserInfo set isactive=0 where UserId='" + iid + "'");
                           master.RemoveCurrentTab();
                        UserList ul = new UserList(master, tabControl);
                        master.AddNewTab(ul);
                    }
                }
                catch
                {
                }
            }
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            try
            {
                bindlist();
            }
            catch
            {
            }
        }

        private void btnok_MouseEnter(object sender, EventArgs e)
        {
            btnok.UseVisualStyleBackColor = false;
            btnok.BackColor = Color.FromArgb(94,191,174);
            btnok.ForeColor = Color.White;
        }

        private void btnok_MouseLeave(object sender, EventArgs e)
        {
            btnok.UseVisualStyleBackColor = true;
            btnok.BackColor = Color.FromArgb(51, 153, 255);
            btnok.ForeColor = Color.White;
        }

        private void btnNew_MouseEnter(object sender, EventArgs e)
        {
            btnNew.UseVisualStyleBackColor = false;
            btnNew.BackColor = Color.FromArgb(9, 106, 3);
            btnNew.ForeColor = Color.White;
        }

        private void btnNew_MouseLeave(object sender, EventArgs e)
        {
            btnNew.UseVisualStyleBackColor = true;
            btnNew.BackColor = Color.FromArgb(51, 153, 255);
            btnNew.ForeColor = Color.White;
        }

        private void btnok_Enter(object sender, EventArgs e)
        {
            btnok.UseVisualStyleBackColor = false;
            btnok.BackColor = Color.FromArgb(94, 191, 174);
            btnok.ForeColor = Color.White;
        }

        private void btnok_Leave(object sender, EventArgs e)
        {
            btnok.UseVisualStyleBackColor = true;
            btnok.BackColor = Color.FromArgb(51, 153, 255);
            btnok.ForeColor = Color.White;
        }

        private void btnNew_Enter(object sender, EventArgs e)
        {
            btnNew.UseVisualStyleBackColor = false;
            btnNew.BackColor = Color.FromArgb(9, 106, 3);
            btnNew.ForeColor = Color.White;
        }

        private void btnNew_Leave(object sender, EventArgs e)
        {
            btnNew.UseVisualStyleBackColor = true;
            btnNew.BackColor = Color.FromArgb(51, 153, 255);
            btnNew.ForeColor = Color.White;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }

        private void btnClose_Enter(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = false;
            btnClose.BackColor = Color.FromArgb(248, 152, 94);
            btnClose.ForeColor = Color.White;
        }

        private void btnClose_Leave(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = true;
            btnClose.BackColor = Color.FromArgb(51, 153, 255);
            btnClose.ForeColor = Color.White;
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = false;
            btnClose.BackColor = Color.FromArgb(248, 152, 94);
            btnClose.ForeColor = Color.White;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = true;
            btnClose.BackColor = Color.FromArgb(51, 153, 255);
            btnClose.ForeColor = Color.White;
        }
    }
}
