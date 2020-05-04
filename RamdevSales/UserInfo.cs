using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace RamdevSales
{
    public partial class UserInfo : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection cl = new Connection();
        ServerConnection sc = new ServerConnection();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        public string compId = string.Empty;
        public string uId = string.Empty;
        public UserInfo()
        {
            InitializeComponent();
        }
        private string id;
        private UserList userList;
        public UserInfo(UserList userList)
        {
            InitializeComponent();
            this.userList = userList;
            bindemptype();
        }

        public UserInfo(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
            bindemptype();
        }

        private void UserInfo_Load(object sender, EventArgs e)
        {
            //set the interval  and start the timer
            //   timer1.Interval = 1000;
            //   timer1.Start();
            try
            {
                userrights = cl.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[7]["d"].ToString() == "False")
                    {
                        btnDelete.Enabled = false;
                    }
                }
                this.ActiveControl = txtswipid;
            }
            catch
            {
            }
        }
        public void bindemptype()
        {

            SqlCommand cmd = new SqlCommand("select id,employeetype from tbluser_employeetype where isactive=1 order by employeetype asc", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt11 = new DataTable();
            sda.Fill(dt11);

            cmbPosition.ValueMember = "id";
            cmbPosition.DisplayMember = "employeetype";
            cmbPosition.DataSource = dt11;
            cmbPosition.SelectedIndex = -1;

        }
        void getsr()
        {
            try
            {

                ds = cl.getdata("select max(UserId) from UserInfo");
                dt = ds.Tables[0];
                String str = dt.Rows[0][0].ToString();
                int id, count;

                if (str == "")
                {

                    id = 1;
                    count = 1;
                }
                else
                {
                    id = Convert.ToInt32(str) + 1;
                    count = Convert.ToInt32(str) + 1;
                }
                uId = count.ToString();
            }
            catch
            {
            }
            finally
            {

            }

        }
        public void submit()
        {
            try
            {
                if (txtPassword.Text != "" && txtUserName.Text != "" && cmbPosition.SelectedIndex != -1)
                {
                    if (btnSave.Text == "&Submit")
                    {
                        if (userrights.Rows.Count > 0)
                        {
                            if (userrights.Rows[7]["a"].ToString() == "True")
                            {
                                getsr();
                                //  cl.execute("insert into UserInfo values('" + uId + "','" + Master.companyId + "','" + txtUserName.Text + "','" + cmbPosition.SelectedItem + "','" + txtPassword.Text + "',1)");
                                cl.execute("INSERT INTO [dbo].[UserInfo]([UserId],[CompanyId],[UserName],[Position],[Password],[isActive],[tital],[name],[address],[phoneno],[swipeid],[commissiontype],[commissiontypevalue],[targetcommission],[targetcommissionvalue],[Positionid])VALUES('" + uId + "','" + Master.companyId + "','" + txtUserName.Text + "','" + cmbPosition.Text + "','" + txtPassword.Text + "','" + "1" + "','" + cmbtital.Text + "','" + txtname.Text + "','" + txtaddress.Text + "','" + txtphoneno.Text + "','" + txtswipid.Text + "','" + cmbcommtyep.Text + "','" + txtcommtype.Text + "','" + cmbtargetcomm.Text + "','" + txttargetcomm.Text + "','" + cmbPosition.SelectedValue + "')");
                                //  sc.execute("insert into UserInfo values('" + uId + "','" + Master.companyId + "','" + txtUserName.Text + "','" + txtPassword.Text + "','" + cmbPosition.SelectedItem + "',1)");
                                MessageBox.Show("User Added Successfully.");
                            }
                            else
                            {
                                MessageBox.Show("You don't have Permission To Submit");
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (userrights.Rows.Count > 0)
                        {
                            if (userrights.Rows[7]["u"].ToString() == "True")
                            {
                                cl.execute("Update UserInfo Set UserName='" + txtUserName.Text + "',Position='" + cmbPosition.Text + "',Positionid='" + cmbPosition.SelectedValue + "',Password='" + txtPassword.Text + "',tital='" + cmbtital.Text + "',name='" + txtname.Text + "',address='" + txtaddress.Text + "',phoneno='" + txtphoneno.Text + "',swipeid='" + txtswipid.Text + "',commissiontype='" + cmbcommtyep.Text + "',commissiontypevalue='" + txtcommtype.Text + "',targetcommission='" + cmbtargetcomm.Text + "',targetcommissionvalue='" + txttargetcomm.Text + "' where UserId='" + id + "'");
                                MessageBox.Show("User Update Successfully.");
                            }
                            else
                            {
                                MessageBox.Show("You don't have Permission To Update");
                                return;
                            }
                        }
                    }
                    master.RemoveCurrentTab();
                    //UserList ul = new UserList(master, tabControl);
                    //master.AddNewTab(ul);
                }
                else
                {
                    txtUserName.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally
            {
                clearAll();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            submit();
        }

        private void clearAll()
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtswipid.Text = "";
            cmbtital.SelectedIndex = 0;
            txtname.Text = "";
            txtaddress.Text = "";
            txtphoneno.Text = "";
            cmbPosition.SelectedIndex = 0;
            cmbcommtyep.SelectedIndex = 0;
            cmbtargetcomm.SelectedIndex = 0;
            txtcommtype.Text = "";
            txttargetcomm.Text = "";
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                if (uId != string.Empty)
                {
                    cl.execute("update UserInfo set isactive=0 where UserId='" + uId + "'");
                  //  sc.execute("update UserInfo set isactive=0 where UserId='" + uId + "'");

                    MessageBox.Show("User Deleted Successfully.");
                }
                else
                {
                    MessageBox.Show("Select User Name");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally
            {
                clearAll();
            }
        }

        int cnt = 0;
        private Master master;
        private TabControl tabControl;
        DataTable userrights = new DataTable();
        internal void updatemode(int p, string iid)
        {
            //  btnChangePswd.Visible = true;
            id = iid;
            cnt = 1;
            uId = iid;
            userrights = cl.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[7]["d"].ToString() == "False")
                {
                    btnDelete.Enabled = false;
                }
            }
            dt = cl.getdataset("select * from UserInfo where UserId='" + iid + "' and isactive=1 and CompanyId=" + Master.companyId + "");

            txtUserName.Text = dt.Rows[0]["UserName"].ToString();
            cmbPosition.Text = dt.Rows[0]["Position"].ToString();
            txtswipid.Text = dt.Rows[0]["swipeid"].ToString();
            cmbtital.Text = dt.Rows[0]["tital"].ToString();
            txtname.Text = dt.Rows[0]["name"].ToString();
            txtaddress.Text = dt.Rows[0]["address"].ToString();
            txtphoneno.Text = dt.Rows[0]["phoneno"].ToString();
            cmbcommtyep.Text = dt.Rows[0]["commissiontype"].ToString();
            txtcommtype.Text = dt.Rows[0]["commissiontypevalue"].ToString();
            cmbtargetcomm.Text = dt.Rows[0]["targetcommission"].ToString();
            txttargetcomm.Text = dt.Rows[0]["targetcommissionvalue"].ToString();

            btnSave.Text = "Update";

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            //UserList frm = new UserList();
            //frm.MdiParent = this.MdiParent;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
            master.RemoveCurrentTab();

        }

        private void btnChangePswd_Click(object sender, EventArgs e)
        {
            //ChangePswd frm = new ChangePswd();
            //frm.MdiParent = this.MdiParent;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
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
            if (keyData == (Keys.Alt | Keys.U))
            {
                DialogResult dr = MessageBox.Show("Do you want to Update?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    submit();
                }
                return true;
            }


            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbPosition.Focus();
            }
        }
        public static string s;
        public static string activecontroal;
        private void cmbPosition_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbPosition.Items.Count; i++)
                {
                    s = cmbPosition.GetItemText(cmbPosition.Items[i]);
                    if (s == cmbPosition.Text)
                    {
                        inList = true;
                        cmbPosition.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbPosition.Text = "";
                }

                cmbcommtyep.Focus();
            }
            if (e.KeyCode == Keys.F3)
            {
                var privouscontroal = cmbPosition;
                activecontroal = privouscontroal.Name;
                Employeetype popup = new Employeetype(this, tabControl, master, activecontroal);
                master.AddNewTab(popup);

            }

            if (e.KeyCode == Keys.F2)
            {
                var privouscontroal = cmbPosition;
                activecontroal = privouscontroal.Name;
                Employeetype dlg = new Employeetype(this, tabControl, master, activecontroal);

                dlg.Update(Convert.ToString(cmbPosition.SelectedValue));
                master.AddNewTab(dlg);
                //  dlg.Show();
            }
        }

        private void cmbPosition_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbPosition.SelectedIndex = 0;
                cmbPosition.DroppedDown = true;
            }
            catch
            {
            }
        }

        private void txtUserName_Enter(object sender, EventArgs e)
        {
            txtUserName.BackColor = Color.LightYellow;
        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            txtUserName.BackColor = Color.White;
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            txtPassword.BackColor = Color.LightYellow;
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            txtPassword.BackColor = Color.White;
        }

        private void btnSave_MouseEnter(object sender, EventArgs e)
        {
            btnSave.UseVisualStyleBackColor = false;
            btnSave.BackColor = Color.YellowGreen;
            btnSave.ForeColor = Color.White;
        }

        private void btnSave_MouseLeave(object sender, EventArgs e)
        {
            btnSave.UseVisualStyleBackColor = true;
            btnSave.BackColor = Color.FromArgb(51, 153, 255);
            btnSave.ForeColor = Color.White;
        }

        private void btnReset_MouseEnter(object sender, EventArgs e)
        {
            btnReset.UseVisualStyleBackColor = false;
            btnReset.BackColor = Color.FromArgb(250, 185, 34);
            btnReset.ForeColor = Color.White;
        }

        private void btnReset_MouseLeave(object sender, EventArgs e)
        {
            btnReset.UseVisualStyleBackColor = true;
            btnReset.BackColor = Color.FromArgb(51, 153, 255);
            btnReset.ForeColor = Color.White;
        }

        private void btnDelete_MouseEnter(object sender, EventArgs e)
        {
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.BackColor = Color.FromArgb(255, 77, 77);
            btnDelete.ForeColor = Color.White;
        }

        private void btnDelete_MouseLeave(object sender, EventArgs e)
        {
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.BackColor = Color.FromArgb(51, 153, 255);
            btnDelete.ForeColor = Color.White;
        }

        private void btnBack_MouseEnter(object sender, EventArgs e)
        {
            btnBack.UseVisualStyleBackColor = false;
            btnBack.BackColor = Color.FromArgb(128, 128, 128);
            btnBack.ForeColor = Color.White;
        }

        private void btnChangePswd_MouseEnter(object sender, EventArgs e)
        {
            btnChangePswd.UseVisualStyleBackColor = true;
            btnChangePswd.BackColor = Color.FromArgb(64, 178, 204);
            btnChangePswd.ForeColor = Color.White;
        }

        private void btnChangePswd_MouseLeave(object sender, EventArgs e)
        {
            btnChangePswd.UseVisualStyleBackColor = true;
            btnChangePswd.BackColor = Color.FromArgb(51, 153, 255);
            btnChangePswd.ForeColor = Color.White;
        }

        private void btnBack_MouseLeave(object sender, EventArgs e)
        {
            btnBack.UseVisualStyleBackColor = true;
            btnBack.BackColor = Color.FromArgb(51, 153, 255);
            btnBack.ForeColor = Color.White;
        }

        private void btnBack_Enter(object sender, EventArgs e)
        {
            btnBack.UseVisualStyleBackColor = false;
            btnBack.BackColor = Color.FromArgb(128, 128, 128);
            btnBack.ForeColor = Color.White;
        }

        private void btnBack_Leave(object sender, EventArgs e)
        {
            btnBack.UseVisualStyleBackColor = true;
            btnBack.BackColor = Color.FromArgb(51, 153, 255);
            btnBack.ForeColor = Color.White;
        }

        private void btnSave_Enter(object sender, EventArgs e)
        {
            btnSave.UseVisualStyleBackColor = false;
            btnSave.BackColor = Color.YellowGreen;
            btnSave.ForeColor = Color.White;
        }

        private void btnSave_Leave(object sender, EventArgs e)
        {
            btnSave.UseVisualStyleBackColor = true;
            btnSave.BackColor = Color.FromArgb(51, 153, 255);
            btnSave.ForeColor = Color.White;
        }

        private void btnReset_Enter(object sender, EventArgs e)
        {
            btnReset.UseVisualStyleBackColor = false;
            btnReset.BackColor = Color.FromArgb(250, 185, 34);
            btnReset.ForeColor = Color.White;
        }

        private void btnReset_Leave(object sender, EventArgs e)
        {
            btnReset.UseVisualStyleBackColor = true;
            btnReset.BackColor = Color.FromArgb(51, 153, 255);
            btnReset.ForeColor = Color.White;
        }

        private void btnDelete_Enter(object sender, EventArgs e)
        {
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.BackColor = Color.FromArgb(255, 77, 77);
            btnDelete.ForeColor = Color.White;
        }

        private void btnDelete_Leave(object sender, EventArgs e)
        {
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.BackColor = Color.FromArgb(51, 153, 255);
            btnDelete.ForeColor = Color.White;
        }

        private void btnChangePswd_Enter(object sender, EventArgs e)
        {
            btnChangePswd.UseVisualStyleBackColor = true;
            btnChangePswd.BackColor = Color.FromArgb(64, 178, 204);
            btnChangePswd.ForeColor = Color.White;
        }

        private void btnChangePswd_Leave(object sender, EventArgs e)
        {
            btnChangePswd.UseVisualStyleBackColor = true;
            btnChangePswd.BackColor = Color.FromArgb(51, 153, 255);
            btnChangePswd.ForeColor = Color.White;
        }
        string searchstr;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //empty the string for every 1 seconds
            // searchstr = "";
        }

        private void cmbPosition_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbPosition.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbPosition.SelectedIndex = index;
            //        }
            //    }


            //}
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }

        private void cmbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbPosition.Items.Count; i++)
                {
                    s = cmbPosition.GetItemText(cmbPosition.Items[i]);
                    if (s == cmbPosition.Text)
                    {
                        inList = true;
                        cmbPosition.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbPosition.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void btncustype_Click(object sender, EventArgs e)
        {
            var privouscontroal = cmbPosition;
            activecontroal = privouscontroal.Name;
            Employeetype popup = new Employeetype(this, tabControl, master, activecontroal);
            master.AddNewTab(popup);
        }

        private void btncusendit_Click(object sender, EventArgs e)
        {
            if (cmbPosition.Text != null && cmbPosition.Text != "")
            {
                var privouscontroal = cmbPosition;
                activecontroal = privouscontroal.Name;
                Employeetype dlg = new Employeetype(this, tabControl, master, activecontroal);
                dlg.Update(Convert.ToString(cmbPosition.SelectedValue));
                master.AddNewTab(dlg);
                dlg.Show();
            }
            else
            {
                MessageBox.Show("Please Select User/Employee Type", "User/Employee Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtswipid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbtital.Focus();
            }
        }

        private void cmbtital_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbtital.SelectedIndex = 0;
                cmbtital.DroppedDown = true;
            }
            catch
            {
            }
        }

        private void cmbtital_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbtital.Items.Count; i++)
                {
                    s = cmbtital.GetItemText(cmbtital.Items[i]);
                    if (s == cmbtital.Text)
                    {
                        inList = true;
                        cmbtital.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbtital.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void cmbtital_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbtital.Items.Count; i++)
                {
                    s = cmbtital.GetItemText(cmbtital.Items[i]);
                    if (s == cmbtital.Text)
                    {
                        inList = true;
                        cmbtital.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbtital.Text = "";
                }
                txtname.Focus();
            }
        }

        private void txtname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtaddress.Focus();
            }
        }

        private void txtaddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtphoneno.Focus();
            }
        }

        private void txtphoneno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtUserName.Focus();
            }
        }

        private void cmbcommtyep_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbcommtyep.Items.Count; i++)
                {
                    s = cmbcommtyep.GetItemText(cmbcommtyep.Items[i]);
                    if (s == cmbcommtyep.Text)
                    {
                        inList = true;
                        cmbcommtyep.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbcommtyep.Text = "";
                }
                txtcommtype.Focus();
            }
        }

        private void txtcommtype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbtargetcomm.Focus();
            }
        }

        private void cmbtargetcomm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbtargetcomm.Items.Count; i++)
                {
                    s = cmbtargetcomm.GetItemText(cmbtargetcomm.Items[i]);
                    if (s == cmbtargetcomm.Text)
                    {
                        inList = true;
                        cmbtargetcomm.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbtargetcomm.Text = "";
                }
                txttargetcomm.Focus();
            }
        }

        private void txttargetcomm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave.Focus();
            }
        }

        private void cmbcommtyep_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbcommtyep.SelectedIndex = 0;
                cmbcommtyep.DroppedDown = true;
            }
            catch
            {
            }
        }

        private void cmbcommtyep_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbcommtyep.Items.Count; i++)
                {
                    s = cmbcommtyep.GetItemText(cmbcommtyep.Items[i]);
                    if (s == cmbcommtyep.Text)
                    {
                        inList = true;
                        cmbcommtyep.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbcommtyep.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void cmbtargetcomm_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbtargetcomm.Items.Count; i++)
                {
                    s = cmbtargetcomm.GetItemText(cmbtargetcomm.Items[i]);
                    if (s == cmbtargetcomm.Text)
                    {
                        inList = true;
                        cmbtargetcomm.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbtargetcomm.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void cmbtargetcomm_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbtargetcomm.SelectedIndex = 0;
                cmbtargetcomm.DroppedDown = true;
            }
            catch
            {
            }
        }

        private void txtphoneno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtswipid_Enter(object sender, EventArgs e)
        {
            txtswipid.BackColor = Color.LightYellow;
        }

        private void txtswipid_Leave(object sender, EventArgs e)
        {
            txtswipid.BackColor = Color.White;
        }

        private void txtname_Enter(object sender, EventArgs e)
        {
            txtname.BackColor = Color.LightYellow;
        }

        private void txtname_Leave(object sender, EventArgs e)
        {
            txtname.BackColor = Color.White;
        }

        private void txtaddress_Leave(object sender, EventArgs e)
        {
            txtaddress.BackColor = Color.White;
        }

        private void txtaddress_Enter(object sender, EventArgs e)
        {
            txtaddress.BackColor = Color.LightYellow;
        }

        private void txtphoneno_Leave(object sender, EventArgs e)
        {
            txtphoneno.BackColor = Color.White;
        }

        private void txtphoneno_Enter(object sender, EventArgs e)
        {
            txtphoneno.BackColor = Color.LightYellow;
        }

        private void txtcommtype_Leave(object sender, EventArgs e)
        {
            txtcommtype.BackColor = Color.White;
        }

        private void txtcommtype_Enter(object sender, EventArgs e)
        {
            txtcommtype.BackColor = Color.LightYellow;
        }

        private void txttargetcomm_Leave(object sender, EventArgs e)
        {
            txttargetcomm.BackColor = Color.White;
        }

        private void txttargetcomm_Enter(object sender, EventArgs e)
        {
            txttargetcomm.BackColor = Color.LightYellow;
        }



    }
}
