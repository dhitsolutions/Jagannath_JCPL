using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Data.SqlClient;
using System.Globalization;

namespace RamdevSales
{
    public partial class UserLogin : Form
    {
        private Master master;
        private Master master_1;
        private CompanyList companyList;
        Connection cl = new Connection();
        ServerConnection sc = new ServerConnection();
        DataSet ds, ds1,ds2 = new DataSet();
        DataTable dt, dt1,dt2 = new DataTable();
        CultureInfo en = new CultureInfo("en-US");
        public UserLogin()
        {
            InitializeComponent();
        }

        public UserLogin(CompanyList companyList, Master master_1)
        {
            InitializeComponent();
            this.companyList = companyList;
            this.master_1 = master_1;
            this.ActiveControl = lblUName;
        }

        public UserLogin(Master master)
        {
            // TODO: Complete member initialization
            this.master = master;
            this.ActiveControl = lblUName;
        }

        public UserLogin(CompanyList companyList, Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.companyList = companyList;
            this.master = master;
            this.tabControl = tabControl;
            this.ActiveControl = lblUName;
        }

        public UserLogin(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
            this.ActiveControl = lblUName;
        }
        DataTable path, check = new DataTable();
        DataTable company = new DataTable();
        string databasepath = "";
        SqlCommand cmd;
        public void query(string que, SqlConnection con)
        {

            // ERROR: Not supported in C#: OnErrorStatement
            cmd = new SqlCommand(que, con);
            con.Open();
            cmd.ExecuteNonQuery();
            //con.Close();

        }
        public void get()
        {
            ds2 = ods.getdata("select * from Path");
            ds = ods.getdata("select * from Company where CompanyID='" + cmbselectcompany.SelectedValue + "'");
            if (ds2 != null)
            {
                check = ds2.Tables[0];
                databasepath = check.Rows[0]["DefaultPath"].ToString() + @"\Daily Backup\";
                if (!Directory.Exists(databasepath))
                {
                    //If Directory (Folder) does not exists. Create it.
                    Directory.CreateDirectory(databasepath);
                }
                string path2 = databasepath;
                string day = Convert.ToString(DateTime.Now.DayOfWeek);
                path2 = path2 + day;
                if (!Directory.Exists(path2))
                {
                    //If Directory (Folder) does not exists. Create it.
                    Directory.CreateDirectory(path2);
                }
                SqlConnection con = new SqlConnection(connectionstring);
                company = ds.Tables[0];
                //SqlCommand cmd1 = new SqlCommand("select * from sysdatabases where name='" + company.Rows[0]["DatabaseName"].ToString() + "'", con);
                SqlCommand cmd1 = new SqlCommand("SELECT * FROM master.sys.databases where name='" + company.Rows[0]["DatabaseName"].ToString() + "'", con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable database = new DataTable();
                sda1.Fill(database);

                string s = null;
                s = path2 + "\\" + database.Rows[0]["name"].ToString() + ".bak";
                //  DateTime creation = File.GetCreationTime(@"C:\test.txt");

                DateTime modification = File.GetLastWriteTime(s);
                DateTime dtcurrentdate = Convert.ToDateTime(DateTime.Now.ToString());
                string theDate = modification.ToString("dd-MM-yyyy");
                string theDate1 = dtcurrentdate.ToString("dd-MM-yyyy");
                DateTime dtmodfication = DateTime.ParseExact(theDate, "dd-MM-yyyy", en);
                DateTime dtcurr = DateTime.ParseExact(theDate1, "dd-MM-yyyy", en);
                if (dtmodfication < dtcurr)
                {
                    dalybackup();
                }
            }
        }
        public void dalybackup()
        {
            try
            {


                ds2 = ods.getdata("select * from Path");
                ds = ods.getdata("select * from Company where CompanyID='" + cmbselectcompany.SelectedValue + "'");
                if (ds2 != null)
                {
                    path = ds2.Tables[0];
                    databasepath = path.Rows[0]["DefaultPath"].ToString() + @"\Daily Backup\";
                    if (!Directory.Exists(databasepath))
                    {
                        //If Directory (Folder) does not exists. Create it.
                        Directory.CreateDirectory(databasepath);
                    }
                    string path1 = path.Rows[0]["DefaultPath"].ToString() + @"\Data\";
                    string path2 = databasepath;
                    string day = Convert.ToString(DateTime.Now.DayOfWeek);
                    path2 = path2 + day;
                    if (!Directory.Exists(path2))
                    {
                        //If Directory (Folder) does not exists. Create it.
                        Directory.CreateDirectory(path2);
                    }
                    SqlConnection con = new SqlConnection(connectionstring);
                    company = ds.Tables[0];
                    SqlCommand cmd1 = new SqlCommand("SELECT * FROM master.sys.databases where name='" + company.Rows[0]["DatabaseName"].ToString() + "'", con);
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    DataTable database = new DataTable();
                    sda1.Fill(database);

                    string s = null;
                    s = path2 + "\\" + database.Rows[0]["name"].ToString();

                    query("Backup database " + database.Rows[0]["name"].ToString() + " to disk='" + s + ".bak'", con);

                }
            }
            catch
            {
            }
        }
        private void UserLogin_Load(object sender, EventArgs e)
        {
            master.enablemenu(false);
            bindcompany();
            loadPage();
            this.ActiveControl= lblUName;
        }
        UpdateSoftware us = new UpdateSoftware();
        string connectionstring;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                // tabControl.TabPages.Remove(tabControl.SelectedTab);
                DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    master.RemoveCurrentTab();
                    CompanyList frm = new CompanyList(master, tabControl);
                    master.AddNewTab(frm);
                }
                return true;
            }


            return base.ProcessCmdKey(ref msg, keyData);
        }
        
        private void openlogin()
        {

            String str = cmbselectcompany.SelectedValue.ToString();
            txthCompId.Text = str;
            ds2 = ods.getdata("Select * from Company where CompanyID='"+str+"'");
            dt2 = ds2.Tables[0];
           // UserLogin ul = new UserLogin(master, tabControl);
            //UserLogin ul1 = new UserLogin(master);
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            connectionStringsSection.ConnectionStrings["qry"].ConnectionString = dt2.Rows[0]["DatabaseConnectionString"].ToString();
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");

            us.updatesof(connectionStringsSection.ConnectionStrings["qry"].ConnectionString,str);
            connectionstring = connectionStringsSection.ConnectionStrings["qry"].ConnectionString;
            DataSet company = ods.getdata("select * from Company where CompanyID='" + str + "'");
            updatemode(str, str, 1, company.Tables[0]);
           // master.RemoveCurrentTab();
           // master.AddNewTab(ul);

            //ul.Show();
            //   this.Close();

        }

        private void loadPage()
        {
          //  panel2.Visible = false;
            
            listView1.Columns.Add("User List", 285);
            lblUName.Focus();
            this.ActiveControl = lblUName;
        }

        int cnt = 0;
        public static DataTable companydt = new DataTable();
        private TabControl tabControl;
        internal void updatemode(string str, string p, int p_2, DataTable dt)
        {
            try
            {
                //  loadPage();
                companydt = dt;
                cnt = 1;
                ds = cl.getdata("select * from UserInfo where isActive=1 and CompanyId=" + str + "");
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        txthCompId.Text = str;
                        //txthUname.Text = dt.Rows[i].ItemArray[2].ToString();
                        listView1.Items.Add(dt.Rows[i].ItemArray[3].ToString());
                    }
                    listView1.Items[0].Selected = true;
                }

                else
                {
                    //cl.execute("insert into UserInfo values(1," + p + ",'admin','Super User','admin',1)");
                    cl.execute("INSERT INTO [dbo].[UserInfo]([UserId],[CompanyId],[UserName],[Position],[Password],[isActive])VALUES('" + "1" + "','" + p + "','" + "admin" + "','" + "admin" + "','" + "admin" + "','" + "1" + "')");
                    DataTable dt123 = cl.getdataset("select mId,mName from MenuMaster where isActive=1");
                    for (int i = 0; i < dt123.Rows.Count; i++)
                    {
                        cl.execute("INSERT INTO [UserRights]([uId],[uName],[cId],[mId],[a],[u],[d],[v],[p],[isActive]) values(" + "1" + ",'" + "admin" + "'," + p + "," + dt123.Rows[i][0].ToString() + ",'" + "True" + "','" + "True" + "','" + "True" + "','" + "True" + "','" + "True" + "',1)");
                    }
                    //  sc.execute("insert into UserInfo values(1," + p + ",'Admin','Super User','Admin',1)");
                    ds = cl.getdata("select * from UserInfo where isActive=1 and UserName='admin' and CompanyId=" + p + "");
                    dt = ds.Tables[0];
                    txthCompId.Text = p;
                    listView1.Items.Add(dt.Rows[0].ItemArray[3].ToString());
                    //UserInfo frm = new UserInfo(p);
                    //frm.Show();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            forpassword();
        }

        private void forpassword()
        {
            panel1.Visible = false;
            panel2.Visible = true;
            lblUName.Text = listView1.Items[listView1.FocusedItem.Index].SubItems[0].Text;
            txtPassword.Focus();
        }



        private void btnBack_Click(object sender, EventArgs e)
        {
            master.RemoveCurrentTab();
            CompanyList frm = new CompanyList(master, tabControl);
            master.AddNewTab(frm);
            // frm.Show();
        }

        private void btnBack1_Click(object sender, EventArgs e)
        {
            //panel1.Visible = true;
            //panel2.Visible = false;
            master.RemoveCurrentTab();
            CompanyList frm = new CompanyList(master, tabControl);
            master.AddNewTab(frm);
        }
        OleDbSettings ods = new OleDbSettings();
        public void bindcompany()
        {
            ds = ods.getdata("Select * from Company");
            dt = ds.Tables[0];

            cmbselectcompany.ValueMember = "CompanyId";
            cmbselectcompany.DisplayMember = "CompanyName";
            cmbselectcompany.DataSource = dt;
            //cmbselectcompany.SelectedIndex = -1;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            login();

        }

        private void login()
        {
            ds = cl.getdata("select * from UserInfo where isActive=1 and UserName='" + lblUName.Text + "' and CompanyId='" + txthCompId.Text + "'");
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][5].ToString() == txtPassword.Text)
                {

                    txtPassword.Text = "";
                    //         master = new Master(this);
                    Master.userid = dt.Rows[0]["Userid"].ToString();
                    try
                    {
                        master_1.setheader(companydt, dt.Rows[0]["UserName"].ToString());
                        master_1.enablemenu(true);
                        master_1.disablecompany(false);
                        master_1.compId(txthCompId.Text);
                    }
                    catch
                    {
                    }
                    try
                    {
                        master.setheader(companydt, dt.Rows[0]["UserName"].ToString());
                        master.enablemenu(true);
                        master.disablecompany(false);
                        master.compId(txthCompId.Text);
                    }
                    catch
                    {
                    }
                    //frm.Show();
                    //  this.Close();

                    int tabs = tabControl.TabPages.Count;
                    if (tabs > 0)
                    {
                        master.RemoveCurrentTab();
                        Businessplus bus = new Businessplus();
                        master.AddNewTab(bus);
                    }
                }
                else
                {
                    MessageBox.Show("Password Incorrect. Try again!!!");
                    txtPassword.Text = "";
                }
            }
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnNext.Focus();
            }
        }

        private void lblUName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(lblUName.Text))
                {
                    txtPassword.Focus();
                }
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                login();
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
            lblUName.Text = listView1.Items[listView1.FocusedItem.Index].SubItems[0].Text;
            txtPassword.Focus();
        }

        private void listView1_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            forpassword();
        }

        private void listView1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                forpassword();
            }

        }

        private void UserLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.Close();
        }

        private void lblUName_Enter(object sender, EventArgs e)
        {
            lblUName.BackColor = Color.LightYellow;
        }

        private void lblUName_Leave(object sender, EventArgs e)
        {
            lblUName.BackColor = Color.White;
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            txtPassword.BackColor = Color.LightYellow;
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            txtPassword.BackColor = Color.White;
        }

        private void btnBack1_MouseEnter(object sender, EventArgs e)
        {
            btnBack1.UseVisualStyleBackColor = false;
            btnBack1.BackColor = Color.FromArgb(128, 128, 128);
            btnBack1.ForeColor = Color.White;
        }

        private void btnBack1_MouseLeave(object sender, EventArgs e)
        {
            btnBack1.UseVisualStyleBackColor = true;
            btnBack1.BackColor = Color.FromArgb(51, 153, 255);
            btnBack1.ForeColor = Color.White;
        }

        private void btnBack_MouseEnter(object sender, EventArgs e)
        {
            btnBack.UseVisualStyleBackColor = false;
            btnBack.BackColor = Color.FromArgb(128, 128, 128);
            btnBack.ForeColor = Color.White;
        }

        private void btnBack_MouseLeave(object sender, EventArgs e)
        {
            btnBack.UseVisualStyleBackColor = true;
            btnBack.BackColor = Color.FromArgb(51, 153, 255);
            btnBack.ForeColor = Color.White;
        }

        private void btnNext_MouseEnter(object sender, EventArgs e)
        {
            btnNext.UseVisualStyleBackColor = false;
            btnNext.BackColor = Color.FromArgb(20, 209, 82);
            btnNext.ForeColor = Color.White;
        }

        private void btnNext_MouseLeave(object sender, EventArgs e)
        {
            btnNext.UseVisualStyleBackColor = true;
            btnNext.BackColor = Color.FromArgb(51, 153, 255);
            btnNext.ForeColor = Color.White;
        }

        private void btnLogin_MouseEnter(object sender, EventArgs e)
        {
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.BackColor = Color.YellowGreen;
            btnLogin.ForeColor = Color.White;
        }

        private void btnLogin_MouseLeave(object sender, EventArgs e)
        {
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.BackColor = Color.FromArgb(51, 153, 255);
            btnLogin.ForeColor = Color.White;
        }

        private void btnBack1_Enter(object sender, EventArgs e)
        {
            btnBack1.UseVisualStyleBackColor = false;
            btnBack1.BackColor = Color.FromArgb(128, 128, 128);
            btnBack1.ForeColor = Color.White;
        }

        private void btnBack1_Leave(object sender, EventArgs e)
        {
            btnBack1.UseVisualStyleBackColor = true;
            btnBack1.BackColor = Color.FromArgb(51, 153, 255);
            btnBack1.ForeColor = Color.White;
        }

        private void btnLogin_Enter(object sender, EventArgs e)
        {
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.BackColor = Color.YellowGreen;
            btnLogin.ForeColor = Color.White;
        }

        private void btnLogin_Leave(object sender, EventArgs e)
        {
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.BackColor = Color.FromArgb(51, 153, 255);
            btnLogin.ForeColor = Color.White;
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

        private void btnNext_Enter(object sender, EventArgs e)
        {
            btnNext.UseVisualStyleBackColor = false;
            btnNext.BackColor = Color.FromArgb(20, 209, 82);
            btnNext.ForeColor = Color.White;
        }

        private void btnNext_Leave(object sender, EventArgs e)
        {
            btnNext.UseVisualStyleBackColor = true;
            btnNext.BackColor = Color.FromArgb(51, 153, 255);
            btnNext.ForeColor = Color.White;
        }

        private void btntsuser_Click(object sender, EventArgs e)
        {
            TauchKeybord ts = new TauchKeybord(lblUName);
            ts.ShowDialog();
        }

        private void btntspass_Click(object sender, EventArgs e)
        {
            TauchKeybord ts = new TauchKeybord(txtPassword);
            ts.ShowDialog();
        }

        private void cmbselectcompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                openlogin();
                get();
                lblUName.Focus();
                this.ActiveControl = lblUName;
            }
        }

        private void cmbselectcompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            String str = cmbselectcompany.SelectedValue.ToString();
            txthCompId.Text = str;
            openlogin();
            get();
            bindcompanyyear();
            this.ActiveControl = lblUName;
        }
        public void bindcompanyyear()
        {
            try
            {
                ds = ods.getdata("Select * from Company where companyID='" + cmbselectcompany.SelectedValue + "'");
                dt = ds.Tables[0];
                String year = Convert.ToDateTime(dt.Rows[0].ItemArray[22].ToString()).Year.ToString() + "-" + Convert.ToDateTime(dt.Rows[0].ItemArray[22].ToString()).AddYears(1).Year.ToString();
                lblfy.Text = "F.Y.From" + Environment.NewLine + year;
            }
            catch
            {
            }
        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void lblUName_TextChanged(object sender, EventArgs e)
        {

        }

        private void UserLogin_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = lblUName;
        }


    }
}
