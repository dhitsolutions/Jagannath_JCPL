using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

namespace RamdevSales
{
    public partial class ChangeFinancialYear : Form
    {
        private Master master;
        private TabControl tabControl;
        OleDbSettings ods = new OleDbSettings();
        Connection conn = new Connection();
        public string connectionstring = ConfigurationManager.ConnectionStrings["qry"].ToString();
        public string newconnectionstring = "";
        public ChangeFinancialYear()
        {
            InitializeComponent();
        }

        public ChangeFinancialYear(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }

        private void ChangeFinancialYear_Load(object sender, EventArgs e)
        {
            try
            {
                //DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
                //DTPFrom.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                //DTPFrom.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                //DTPTo.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                //DTPTo.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                //DTPFrom.Value = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                DTPFrom.CustomFormat = Master.dateformate;
                DTPTo.CustomFormat = Master.dateformate;
            }
            catch
            {
            }
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
        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }

        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            try
            {
                get();
            }
            catch
            {
            }
        }
        string str;
        DataTable path, check = new DataTable();
        DataTable company, newcompany, dtconnstring = new DataTable();
        DataSet ds,ds2,ds3,ds4 = new DataSet();
        SqlCommand cmd;
        public void query(string que, SqlConnection con)
        {

            // ERROR: Not supported in C#: OnErrorStatement
            cmd = new SqlCommand(que, con);
            con.Open();
            cmd.ExecuteNonQuery();
            //con.Close();

        }
        string databasepath = "";
        string newdatabasepath = "";
        string newcompanyno = "";
        string comid = "";
        void getsr()
        {
            try
            {

                ds3 = ods.getdata("select max(CompanyId) from Company where isActive='1'");
                newcompany = ds3.Tables[0];
                String str = newcompany.Rows[0][0].ToString();
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
                newcompanyno = count.ToString();
                comid = count.ToString();

            }
            catch
            {
            }
            finally
            {

            }

        }
        datetime defaultdateformate = new datetime();
        public void get()
        {
            ds2 = ods.getdata("select * from Path");
            ds = ods.getdata("select * from Company where CompanyID='" + Master.companyId + "'");
            if (ds2 != null)
            {
                check = ds2.Tables[0];
                databasepath = check.Rows[0]["DefaultPath"].ToString() + @"\ChangeFinancialYear\";
                newdatabasepath = check.Rows[0]["DefaultPath"].ToString() + @"\Data\";
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
                s = path2 + "\\" + database.Rows[0]["name"].ToString() + "";
                //  DateTime creation = File.GetCreationTime(@"C:\test.txt");
                query("Backup database " + database.Rows[0]["name"].ToString() + " to disk='" + s + ".bak'", con);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                DataTable restore= conn.getdataset("RESTORE FILELISTONLY FROM disk= '"+s+".bak"+"' ");
                getsr();
                newcompanyno = "Data" + newcompanyno;
                conn.execute("RESTORE DATABASE [" + newcompanyno + "] FROM  DISK = N'" + s + ".bak" + "'WITH  FILE = 1,  nounload, stats=10,recovery,MOVE N'" + restore.Rows[0]["LogicalName"].ToString() + "' TO N'" + newdatabasepath + "/" + newcompanyno + "_Data.mdf',MOVE N'" + restore.Rows[1]["LogicalName"].ToString() + "' TO N'" + newdatabasepath + "/" + newcompanyno + ".ldf'");
                string startdate = DTPFrom.Text;
                string enddate = DTPTo.Text;
                string fy1 = defaultdateformate.convertdate(Convert.ToString(startdate), "dd-MM-yyyy", "yyyy-MM-dd", '-');
                string fy2 = defaultdateformate.convertdate(Convert.ToString(enddate), "dd-MM-yyyy", "yyyy-MM-dd", '-');
                ds4 = ods.getdata("Select * from SQLSetting");
                dtconnstring = ds4.Tables[0];
                if (dtconnstring.Rows.Count > 0)
                {
                    ods.execute("UPDATE [SQLSetting] SET [OT7] ='" + connectionstring + "'");
                }
                else
                {
                    ods.execute("INSERT INTO [SQLSetting]([OT7])VALUES('" + connectionstring + "')");
                }
                newconnectionstring = connectionstring;
                var splitString = newconnectionstring.Split('=', ';');
                string s1 = "";

                for (int i = 0; i < splitString.Length; i++)
                {

                    if (i == 3)
                    {
                        splitString[i] = newcompanyno;
                    }
                  
                    if (i % 2 == 0)
                    {
                        s1 += splitString[i] + "=";
                    }
                    else
                    {
                        s1 += splitString[i] + ";";
                    }

                }
                ods.execute("INSERT INTO [Company]([CompanyID],[CompanyName],[SubName],[Address],[Address2],[City],[State],[Statecode],[Country],[Phone],[Mobile],[Email],[Website],[CSTNo],[PANNo],[VATNo],[DLNo1],[DLNo2],[DealsIn],[Stockist],[SQLDatabase],[StartDate],[EndDate],[DatabaseName],[DatabaseConnectionString],[currency],[isActive])VALUES('" + comid + "','" + company.Rows[0]["CompanyName"].ToString() + "','" + company.Rows[0]["SubName"].ToString() + "','" + company.Rows[0]["Address"].ToString() + "','" + company.Rows[0]["Address2"].ToString() + "','" + company.Rows[0]["City"].ToString() + "','" + company.Rows[0]["State"].ToString() + "','" + company.Rows[0]["Statecode"].ToString() + "','" + company.Rows[0]["Country"].ToString() + "','" + company.Rows[0]["Phone"].ToString() + "','" + company.Rows[0]["Mobile"].ToString() + "','" + company.Rows[0]["Email"].ToString() + "','" + company.Rows[0]["Website"].ToString() + "','" + company.Rows[0]["CSTNo"].ToString() + "','" + company.Rows[0]["PANNo"].ToString() + "','" + company.Rows[0]["VATNo"].ToString() + "','" + company.Rows[0]["DLNo1"].ToString() + "','" + company.Rows[0]["DLNo2"].ToString() + "','" + company.Rows[0]["DealsIn"].ToString() + "','" + company.Rows[0]["Stockist"].ToString() + "','" + company.Rows[0]["SQLDatabase"].ToString() + "','" + fy1 + "','" + fy2 + "','" + newcompanyno + "','" + s1 + "','" + company.Rows[0]["currency"].ToString() + "',1)");
                SqlConnection scon = new SqlConnection(s1);
                conn.execute("Update Company set CompanyID='" + comid + "',StartDate='" + fy1 + "',EndDate='" + fy2 + "',DatabaseName='" + newcompanyno + "',DatabaseConnectionString='" + s1 + "' WHERE CompanyID='" + Master.companyId + "'", scon);
                conn.execute("Truncate table BillMaster", scon);
                conn.execute("Truncate table BillProductMaster", scon);
                conn.execute("Truncate table BillPOSMaster", scon);
                conn.execute("Truncate table BillPOSProductMaster", scon);
                conn.execute("Truncate table Ledger", scon);
                conn.execute("Truncate table Ref", scon);
                conn.execute("Truncate table Billchargesmaster", scon);
                conn.execute("Truncate table paymentreceipt", scon);
                conn.execute("Truncate table Voucher", scon);
                conn.execute("Truncate table SaleOrderchargesmaster", scon);
                conn.execute("Truncate table SaleOrderMaster", scon);
                conn.execute("Truncate table SaleOrderProductMaster", scon);
                conn.execute("Truncate table Serials", scon);
                conn.execute("Truncate table tblrowmaterialsmaster", scon);
                conn.execute("Truncate table tblproductgeneratedmaster", scon);
                conn.execute("Truncate table tblfinishedgoodsmaster", scon);
                conn.execute("Truncate table tblproductionrawmaterialmaster", scon);
                conn.execute("Truncate table tblproductionmaster", scon);
                conn.execute("Truncate table tblfinishedgoodsqty", scon);
                conn.execute("Truncate table tblprocessmaster", scon);
                conn.execute("Truncate table tblitemsendtocustomer", scon);
                conn.execute("Truncate table tblsendtocustomer", scon);
                conn.execute("Truncate table tblitemreceivefromcompany", scon);
                conn.execute("Truncate table tblreceivefromcompany", scon);
                conn.execute("Truncate table tblsendtocompanyitemmaster", scon);
                conn.execute("Truncate table tblsendtocompanymaster", scon);
                conn.execute("Truncate table tblitemcomplainmaster", scon);
                conn.execute("Truncate table tblcomplainmaster", scon);
                Application.Exit();
            }
        }

        private void DTPFrom_ValueChanged(object sender, EventArgs e)
        {
            DTPTo.Value = DTPFrom.Value.AddDays(364);
        }
       
    }
}
