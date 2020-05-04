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
using System.IO;
using Microsoft.Synchronization;
using Microsoft.Synchronization.Files;
using Microsoft.Synchronization.Data;
using System.Globalization;
using System.Threading;

namespace RamdevSales
{
    public partial class CompanyList : Form
    {
        Connection cl = new Connection();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        OleDbSettings ods = new OleDbSettings();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        SqlCommand cmd;
        SqlDataAdapter sda;

        SqlDataReader dr;
        private Master master;

        public CompanyList()
        {
            InitializeComponent();
            listView1.Columns.Add("CompanyID", 0, HorizontalAlignment.Center);
            listView1.Columns.Add("Company", 500, HorizontalAlignment.Left);
            listView1.Columns.Add("F.Y.From", 200, HorizontalAlignment.Center);
            //  CultureInfo en = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = en;
        }

        public CompanyList(Master master)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;

            master.enablemenu(false);
            listView1.Columns.Add("CompanyID", 0, HorizontalAlignment.Center);
            listView1.Columns.Add("Company", 500, HorizontalAlignment.Left);

            listView1.Columns.Add("F.Y.From", 200, HorizontalAlignment.Center);
            // CultureInfo en = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = en;
        }
        CultureInfo en = new CultureInfo("en-US");
        public CompanyList(Master master, TabControl tabControl)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this.master = master;
            this.tabControl = tabControl;
            master.enablemenu(false);
            listView1.Columns.Add("CompanyID", 0, HorizontalAlignment.Center);
            listView1.Columns.Add("Company", 500, HorizontalAlignment.Left);

            listView1.Columns.Add("F.Y.From", 200, HorizontalAlignment.Center);
            //  CultureInfo en = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = en;

        }

        public CompanyList(TabControl tabControl)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this.tabControl = tabControl;
            master.enablemenu(false);
            listView1.Columns.Add("CompanyID", 0, HorizontalAlignment.Center);
            listView1.Columns.Add("Company", 500, HorizontalAlignment.Left);

            listView1.Columns.Add("F.Y.From", 200, HorizontalAlignment.Center);
            //    CultureInfo en = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = en;
        }

        private void CompanyList_Load(object sender, EventArgs e)
        {
            try
            {
                if (AddDatabasePath.newcompany == 1)
                {
                    btnNew.Enabled = false;
                }
                bindListView();
                //////  this.ActiveControl = listView1;
                //listView1.HideSelection = false;
                //listView1.Select();
                ////  listView1.Items[0].Checked = true;
                //listView1.FocusedItem = listView1.Items[0];
                //listView1.Items[0].Selected = true;
                //  listView1.Select();
                //  listView1.Items[0].Selected = true;
                listView1.Select();
                this.listView1.Items[0].Selected = true;

            }
            catch
            {
            }



        }

        private void bindListView()
        {
            try
            {
                //if (con.State == ConnectionState.Open)
                //{
                //    con.Close();
                //}
                //con.Open();
                ds = ods.getdata("Select * from Company");
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {

                    listView1.Items.Clear();
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        listView1.Items.Add(dt.Rows[i].ItemArray[1].ToString());
                        listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                        String year = Convert.ToDateTime(dt.Rows[i].ItemArray[22].ToString()).Year.ToString() + "-" + Convert.ToDateTime(dt.Rows[i].ItemArray[22].ToString()).AddYears(1).Year.ToString();
                        listView1.Items[i].SubItems.Add(year);
                    }
                    //   listView1.Items[0].Selected = true;
                    // listView1.Select();

                }
                else
                {
                    // AddConString frm = new AddConString();
                    // frm.Show();
                }
            }

            catch (Exception ex)
            {
                //  MessageBox.Show("Error:" + ex.Message);
            }

            finally
            {
                //con.Close();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            //this.Close();
            master.RemoveCurrentTab();
            InsertCompany frm = new InsertCompany(master, tabControl);
            master.AddNewTab(frm);
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
            // tabControl.TabPages.Remove(tabControl.SelectedTab);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                // this.Close();
                master.RemoveCurrentTab();
                Application.Exit();

            }
        }
        DataSet ds2 = new DataSet();
        string databasepath = "";
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            openlogin();
            get();

        }
        UpdateSoftware us = new UpdateSoftware();
        string connectionstring;
        private void openlogin()
        {

            String str = listView1.Items[listView1.FocusedItem.Index].SubItems[0].Text;

            UserLogin ul = new UserLogin(this, master, tabControl);
            //UserLogin ul1 = new UserLogin(master);
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            connectionStringsSection.ConnectionStrings["qry"].ConnectionString = dt.Rows[listView1.FocusedItem.Index][29].ToString();
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");

            us.updatesof(connectionStringsSection.ConnectionStrings["qry"].ConnectionString, dt.Rows[listView1.FocusedItem.Index][1].ToString());
            connectionstring = connectionStringsSection.ConnectionStrings["qry"].ConnectionString;
            DataSet company = ods.getdata("select * from Company where CompanyID='" + dt.Rows[listView1.FocusedItem.Index][1].ToString() + "'");
            ul.updatemode(str, dt.Rows[listView1.FocusedItem.Index][1].ToString(), 1, company.Tables[0]);
            master.RemoveCurrentTab();
            master.AddNewTab(ul);

            //ul.Show();
            //   this.Close();

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
        int genr = 0;
        private TabControl tabControl;
        internal void Passed(int p)
        {
            genr = p;
        }
        public void query(string que, SqlConnection con)
        {

            // ERROR: Not supported in C#: OnErrorStatement
            cmd = new SqlCommand(que, con);
            con.Open();
            cmd.ExecuteNonQuery();
            //con.Close();

        }
        string str;
        DataTable path, check = new DataTable();
        DataTable company = new DataTable();
        public void get()
        {
            ds2 = ods.getdata("select * from Path");
            ds = ods.getdata("select * from Company where CompanyID='" + listView1.Items[listView1.FocusedItem.Index].SubItems[0].Text + "'");
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
                ds = ods.getdata("select * from Company where CompanyID='" + listView1.Items[listView1.FocusedItem.Index].SubItems[0].Text + "'");
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

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.Enter)
            {

                openlogin();
                get();

            }
        }

        private void CompanyList_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnCancel_MouseEnter(object sender, EventArgs e)
        {

            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.BackColor = Color.FromArgb(248, 152, 94);
            btnCancel.ForeColor = Color.White;
        }

        private void btnCancel_MouseLeave(object sender, EventArgs e)
        {
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.BackColor = Color.FromArgb(51, 153, 255);
            btnCancel.ForeColor = Color.White;
        }

        private void btnNew_MouseEnter(object sender, EventArgs e)
        {
            btnNew.UseVisualStyleBackColor = false;
            btnNew.BackColor = Color.FromArgb(39, 198, 220);
            btnNew.ForeColor = Color.White;
        }

        private void btnNew_MouseLeave(object sender, EventArgs e)
        {
            btnNew.UseVisualStyleBackColor = true;
            btnNew.BackColor = Color.FromArgb(51, 153, 255);
            btnNew.ForeColor = Color.White;
        }

        private void btnNew_Enter(object sender, EventArgs e)
        {
            btnNew.UseVisualStyleBackColor = false;
            btnNew.BackColor = Color.FromArgb(39, 198, 220);
            btnNew.ForeColor = Color.White;
        }

        private void btnNew_Leave(object sender, EventArgs e)
        {
            btnNew.UseVisualStyleBackColor = true;
            btnNew.BackColor = Color.FromArgb(51, 153, 255);
            btnNew.ForeColor = Color.White;
        }

        private void btnCancel_Enter(object sender, EventArgs e)
        {
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.BackColor = Color.FromArgb(248, 152, 94);
            btnCancel.ForeColor = Color.White;
        }

        private void btnCancel_Leave(object sender, EventArgs e)
        {
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.BackColor = Color.FromArgb(51, 153, 255);
            btnCancel.ForeColor = Color.White;
        }
    }
}
