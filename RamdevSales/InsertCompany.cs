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
using System.Security.Cryptography;
using System.Globalization;
using System.Threading;
using System.Data.OleDb;

namespace RamdevSales
{
    public partial class InsertCompany : Form
    {
        //  static int iid = 0; 
        Connection conn = new Connection();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        ServerConnection sc = new ServerConnection();
        OleDbSettings ods = new OleDbSettings();
        DataSet ds,ds1,ds2 = new DataSet();
        OleDbConnection odsconn;
        OleDbDataAdapter adapter;
        public static string regdate = string.Empty;
        public static string regtime = string.Empty;
        public static string expdate = string.Empty;
        public static string expdt = string.Empty;
        public static string status = string.Empty;
        DataTable dt,dtreg = new DataTable();
        datetime defaultdateformate = new datetime();
        private CompanyList companyList;
        private Master master;
        public InsertCompany()
        {
            InitializeComponent();
        }
        int gener = 0;
        string d, t, s;
        int flag = 0;
        private TabControl tabControl;
        string database;
        string connectionstring = "";
        string databasename = "";
        
        public InsertCompany(CompanyList companyList)
        {
            InitializeComponent();
            this.companyList = companyList;
        }

        public InsertCompany(Master master)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
        }

        public InsertCompany(TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.tabControl = tabControl;
        }

        public InsertCompany(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }

        void getsr()
        {
            try
            {

                ds = ods.getdata("select max(CompanyId) from Company where isActive='1'");
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
                txthCompId.Text = count.ToString();

            }
            catch
            {
            }
            finally
            {

            }

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                // tabControl.TabPages.Remove(tabControl.SelectedTab);
                DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    master.RemoveCurrentTab();
                }
                return true;
            }


            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void CompanyInfo_Load(object sender, EventArgs e)
        {
            loadPage();
            tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(TabPagesDrawItem);
            //tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(TabPagesDrawItem);
            this.ActiveControl = txtCompName;
        }
        Brush backBrush;
        Brush foreBrush;
        private void TabPagesDrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            //Change appearance of tabcontrol
            //Brush backBrush;
            //Brush foreBrush;
            if (e.Index == tabControl1.SelectedIndex)
            {
                backBrush = new SolidBrush(Color.SteelBlue);
              //  backBrush = new SolidBrush(Color.SkyBlue);
               // backBrush = new SolidBrush(Color.DeepSkyBlue);
               // backBrush = new SolidBrush(Color.DodgerBlue);
                foreBrush = new SolidBrush(Color.White);
            }
            else
            {
                backBrush = new SolidBrush(Color.Snow);
                foreBrush = new SolidBrush(Color.DimGray);
            }

            e.Graphics.FillRectangle(backBrush, e.Bounds);

            //You may need to write the label here also?
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;

            Rectangle r = e.Bounds;
            r = new Rectangle(r.X, r.Y + 3, r.Width, r.Height - 3);
            e.Graphics.DrawString(this.tabControl1.TabPages[e.Index].Text, e.Font, foreBrush, r, sf);

            //NEw code
            //tabControl1.ColorScheme.TabBackground = Color.Transparent;
            //tabControl1.ColorScheme.TabBackground2 = Color.Transparent;
            //TabControl.BackColor = Color.Transparent;
            

        }
        private void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Rectangle lasttabrect = tabControl1.GetTabRect(tabControl1.TabPages.Count - 1);
            RectangleF emptyspacerect = new RectangleF(
                    lasttabrect.X + lasttabrect.Width + tabControl1.Left,
                    tabControl1.Top + lasttabrect.Y,
                    tabControl1.Width - (lasttabrect.X + lasttabrect.Width),
                    lasttabrect.Height);

            Brush b = Brushes.Transparent; // the color you want
            e.Graphics.FillRectangle(b, emptyspacerect);
        }
        
        private void loadPage()
        {
           
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            //getsr();
          //  getcompInfo();
           // this.ActiveControl = txtCompName;
            this.txtCompName.Focus();
            //txtCompName.Focus();
            dateTimePicker1.CustomFormat = "dd-MM-yyyy";
            dateTimePicker2.CustomFormat = "dd-MM-yyyy";
        }

        private void getcompInfo()
        {
            try
            {
                if (Master.companyId != null)
                {
                    dt = conn.getdataset("Select * from Company where companyId=" + Master.companyId + "");
                    if (dt.Rows.Count > 0)
                    {
                        txtCompName.Text = dt.Rows[0][1].ToString();
                        txtSubName.Text = dt.Rows[0][2].ToString();
                        txtAddress.Text = dt.Rows[0][3].ToString();
                        txtAddress2.Text = dt.Rows[0][4].ToString();
                        txtCity.Text = dt.Rows[0][5].ToString();
                        txtstate.Text = dt.Rows[0][6].ToString();
                        txtscode.Text = dt.Rows[0][7].ToString();
                        txtcountry.Text = dt.Rows[0][8].ToString();
                        txtPhone.Text = dt.Rows[0][9].ToString();
                        txtMobile.Text = dt.Rows[0][10].ToString();
                        txtEmail.Text = dt.Rows[0][11].ToString();
                        txtWebsite.Text = dt.Rows[0][12].ToString();
                        txtCSTNo.Text = dt.Rows[0][13].ToString();
                        txtPANNo.Text = dt.Rows[0][14].ToString();
                        txtVATNo.Text = dt.Rows[0][15].ToString();
                        txtDL1.Text = dt.Rows[0][16].ToString();
                        txtDL2.Text = dt.Rows[0][17].ToString();
                        txtDealsIn.Text = dt.Rows[0][18].ToString();
                        txtStockist.Text = dt.Rows[0][19].ToString();
                        //txtPath.Text = dt.Rows[0][20].ToString();
                        btnSave.Text = "Update";
                    }
                }
            }
            catch
            {
            }
        }

        private void txtCompName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    flag = 0;
                    //string str = txtCompName.Text.ToUpper().Trim();
                    //SqlCommand cmd1 = new SqlCommand("select CompanyName from Company where isactive=1", con);
                    //SqlDataAdapter sda = new SqlDataAdapter(cmd1);
                    //DataTable dt = new DataTable();
                    //sda.Fill(dt);
                    //if (txtCompName.Text != "")
                    //{
                    //    if (dt.Rows.Count > 0)
                    //    {
                    //        for (int i = 0; i < dt.Rows.Count; i++)
                    //        {
                    //            string val = dt.Rows[i][0].ToString().ToUpper().Trim();
                    //            if (val == str)
                    //            {
                    //                MessageBox.Show("Company Already Available Please add Another");
                    //                txtCompName.Focus();
                    //                flag = 1;
                    //                break;
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        txtSubName.Focus();
                    //    }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Company Name cannot be Blank");
                    //    txtCompName.Show();
                    //}
                    if (flag == 0)
                    {
                        txtSubName.Text = txtCompName.Text;
                        txtSubName.Focus();
                    }
                }
            }
            catch
            {
            }
           
        }

        private void txtSubName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //txtAddress.Focus();
                txtcurrency.Focus();
            }
        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAddress2.Focus();
            }
        }

        private void txtAddress2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCity.Focus();
            }
        }

        private void txtCity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtstate.Focus();
            }
        }

        private void txtPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMobile.Focus();
            }
        }

        private void txtMobile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtEmail.Focus();
            }
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtWebsite.Focus();
            }
        }

        private void txtWebsite_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCSTNo.Focus();
            }
        }

        private void txtCSTNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPANNo.Focus();
            }
        }

        private void txtPANNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtVATNo.Focus();
            }
        }

        private void txtVATNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDL1.Focus();
            }

        }

        private void txtDL1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDL2.Focus();
            }
        }

        private void txtDL2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDealsIn.Focus();
            }
        }

        private void txtDealsIn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtStockist.Focus();
            }
        }

        private void txtStockist_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //txtcurrency.Focus();
                chkdatabase.Focus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (chkdatabase.Checked == true)
            {
                if (sqlsetting.servername == "" && sqlsetting.username == "" && sqlsetting.pass == "")
                {
                    MessageBox.Show("Please Set SQL Database Setting");
                    return;
                }
                else
                {
                    submit();
                    master.RemoveCurrentTab();
                    this.Hide();
                    var form2 = new Master();
                    form2.Closed += (s, args) => this.Close();
                    form2.Show();
                }
            }
           
        }
        public static string Encryptregdate(string clearText)
        {
            string EncryptionKey = "00";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                    regdate = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Encryptexpdate(string clearText)
        {
            string EncryptionKey = "00";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                    expdate = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Encryptregtime(string clearText)
        {
            string EncryptionKey = "00";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                    //str = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Encryptstatus(string clearText)
        {
            string EncryptionKey = "00";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                    status = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public void createdatabasewithusername()
        {
            using (SqlConnection myConn = new SqlConnection("Data Source='" + sqlsetting.servername + "';User ID='" + sqlsetting.username + "';Password='" + sqlsetting.pass + "'"))
            {
                try
                {
                    string str;
                    string databasepath="";
                    string appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                   ds2= ods.getdata("select * from Path");
                    DataTable path = new DataTable();
                    path = ds2.Tables[0];
                    if (path.Rows.Count <= 0)
                    {
                        //if (path.Rows[0]["DefaultPath"].ToString() == "" || path.Rows[0]["DefaultPath"].ToString() == null)
                        //{
                          //  string Inspath = "INSERT INTO [Path]([DefaultPath] values('" + appPath + "')";
                            ods.execute("INSERT INTO [Path]([DefaultPath])VALUES('" + appPath + "')");
                            ds2 = ods.getdata("select * from Path");
                          //  DataTable path1 = new DataTable();
                            path = ds2.Tables[0];

                            databasepath = path.Rows[0]["DefaultPath"].ToString() + @"\Data\";

                        //}
                        //else
                        //{
                           
                        //}
                    }
                    else
                    {
                        databasepath = path.Rows[0]["DefaultPath"].ToString() + @"\Data\";
                    }
                     
                    //  appPath.Replace("'\'", "\\");
                    databasename = "Data" + txthCompId.Text;
                    databasepath = databasepath + databasename;
                    //FILENAME = 'J:\\contil\\29-02-2017\\contil\\Contil\\trunk\\Ramdev Sales Final\\RamdevSales\\bin\\Debug\\SQLDB\\tempdb12.mdf'
                    str = "CREATE DATABASE " + databasename + " ON PRIMARY " +
    "(NAME = " + databasename + "_Data, " +
    "FILENAME = '" + databasepath + ".mdf', " +
    "SIZE = 5MB, MAXSIZE = 50MB, FILEGROWTH = 100%) " +
    "LOG ON (NAME = " + databasename + ", " +
    "FILENAME = '" + databasepath + ".ldf', " +
    "SIZE = 512KB, " +
    "MAXSIZE = 10MB, " +
    "FILEGROWTH = 10%)";
                    if (myConn.State == ConnectionState.Open)
                    {
                        myConn.Close();
                    }
                    myConn.Open();
                    SqlCommand myCommand = new SqlCommand(str, myConn);
                    myCommand.ExecuteNonQuery();
                    SqlConnection Conn = new SqlConnection("Data Source='" + sqlsetting.servername + "';Initial Catalog='" + databasename + "';User ID='" + sqlsetting.username + "';Password='" + sqlsetting.pass + "'");

                    databasepath = path.Rows[0]["DefaultPath"].ToString() + @"\SQLDB\";
                   // string appPath1 = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\SQLDB\";
                    var fileContent = File.ReadAllText(databasepath + "\\script.sql");
                    var sqlqueries = fileContent.Split(new[] { " GO " }, StringSplitOptions.RemoveEmptyEntries);
                 
                   
                    var cmd = new SqlCommand("query", Conn);
                    
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                    }
                    Conn.Open();
                    foreach (var query in sqlqueries)
                    {
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                        
                        
                    }
                    //string date12 = date.convertdate(date1, "dd-MMM-yyyy", "yyyy-MM-dd", '-');
                    //string startdate =dateTimePicker1.Value.ToString("yyyy-MM-dd");
                    //string enddate = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                    string startdate = dateTimePicker1.Text;
                    string enddate = dateTimePicker2.Text;
                    string fy1 = defaultdateformate.convertdate(Convert.ToString(startdate), "dd-MM-yyyy", "yyyy-MM-dd", '-');    
                    //DateTime d = Convert.ToDateTime(fy1);
                    //string f = d.ToString("yyyy-MM-dd");
                    //string fy2 = Convert.ToString(d.AddDays(364));
                    string fy2 = defaultdateformate.convertdate(Convert.ToString(enddate), "dd-MM-yyyy", "yyyy-MM-dd", '-');    
                    //fy2 = DateTime.Parse(fy2).ToString("dd-MM-yyyy");//   dateTimePicker1.Value.AddDays(364));
                   // string fy2 = DateTime.Parse(dateTimePicker1.Text).ToString("dd-MM-yyyy");

                    bool concat = txtAddress.Text.Contains("'");
                    string address = string.Empty;
                    if (concat)
                    {
                        char[] textArray = txtAddress.Text.ToCharArray();
                        for (int i = 0; i < txtAddress.Text.Length; i++)
                        {
                            if (textArray[i].Equals('\''))
                            {
                                address += "'";
                            }
                            address += Convert.ToString(textArray[i]);
                        }
                    }
                    else
                    {
                        address = txtAddress.Text;
                    }

                    bool concat2 = txtAddress2.Text.Contains("'");
                    string address2 = string.Empty;
                    if (concat2)
                    {
                        char[] textArray = txtAddress2.Text.ToCharArray();
                        for (int i = 0; i < txtAddress2.Text.Length; i++)
                        {
                            if (textArray[i].Equals('\''))
                            {
                                address2 += "'";
                            }
                            address2 += Convert.ToString(textArray[i]);
                        }
                    }
                    else
                    {
                        address2 = txtAddress2.Text;
                    }

                    string company = "INSERT INTO [Company]([CompanyID],[CompanyName],[SubName],[Address],[Address2],[City],[State],[Statecode],[Country],[Phone],[Mobile],[Email],[Website],[CSTNo],[PANNo],[VATNo],[DLNo1],[DLNo2],[DealsIn],[Stockist],[SQLDatabase],[StartDate],[EndDate],[DatabaseName],[DatabaseConnectionString],[currency],[isActive])VALUES('" + txthCompId.Text + "','" + txtCompName.Text + "','" + txtSubName.Text + "','" + address + "','" + address2 + "','" + txtCity.Text + "','" + txtstate.Text + "','" + txtscode.Text + "','" + txtcountry.Text + "','" + txtPhone.Text + "','" + txtMobile.Text + "','" + txtEmail.Text + "','" + txtWebsite.Text + "','" + txtCSTNo.Text + "','" + txtPANNo.Text + "','" + txtVATNo.Text + "','" + txtDL1.Text + "','" + txtDL2.Text + "','" + txtDealsIn.Text + "','" + txtStockist.Text + "','" + database + "','" + fy1 + "','" + fy2 + "','" + databasename + "','" + connectionstring + "','" + txtcurrency.Text + "',1)";
                    SqlCommand myCommand1 = new SqlCommand(company,Conn);
                    myCommand1.ExecuteNonQuery();
                    Conn.Close();


                   // MessageBox.Show("DataBase is Created Successfully", "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    if (myConn.State == ConnectionState.Open)
                    {
                        myConn.Close();
                    }
                }

            }
        }
        public void afterdatabasewithusername()
        {
            connectionstring = "Data Source='" + sqlsetting.servername + "';Initial Catalog='" + databasename + "';User ID='" + sqlsetting.username + "';Password='" + sqlsetting.pass + "'";
            connectionstring = connectionstring.Replace("'", "");
            connectionstring = connectionstring.Trim('"');
            // conn.execute("INSERT INTO [Company]([CompanyID],[CompanyName],[SubName],[Address],[Address2],[City],[State],[Statecode],[Country],[Phone],[Mobile],[Email],[Website],[CSTNo],[PANNo],[VATNo],[DLNo1],[DLNo2],[DealsIn],[Stockist],[Path],[StartDate],[EndDate],[isActive])VALUES('" + txthCompId.Text + "','" + txtCompName.Text + "','" + txtSubName.Text + "','" + txtAddress.Text + "','" + txtAddress2.Text + "','" + txtCity.Text + "','" + txtstate.Text + "','" + txtscode.Text + "','" + txtcountry.Text + "','" + txtPhone.Text + "','" + txtMobile.Text + "','" + txtEmail.Text + "','" + txtWebsite.Text + "','" + txtCSTNo.Text + "','" + txtPANNo.Text + "','" + txtVATNo.Text + "','" + txtDL1.Text + "','" + txtDL2.Text + "','" + txtDealsIn.Text + "','" + txtStockist.Text + "','" + txtPath.Text + "','" + Convert.ToDateTime(dateTimePicker1.Text).ToString(dateformate) + "','" + Convert.ToDateTime(dateTimePicker2.Text).ToString(dateformate) + "',1)");
            //sc.execute("INSERT INTO [Company]([CompanyID],[CompanyName],[SubName],[Address],[Address2],[City],[Phone],[Mobile],[Email],[Website],[CSTNo],[PANNo],[VATNo],[DLNo1],[DLNo2],[DealsIn],[Stockist],[Path],[StartDate],[EndDate],[isActive])VALUES('" + txthCompId.Text + "','" + txtCompName.Text + "','" + txtSubName.Text + "','" + txtAddress.Text + "','" + txtAddress2.Text + "','" + txtCity.Text + "'," + txtPhone.Text + ",'" + txtMobile.Text + "','" + txtEmail.Text + "','" + txtWebsite.Text + "','" + txtCSTNo.Text + "','" + txtPANNo.Text + "','" + txtVATNo.Text + "'," + txtDL1.Text + "," + txtDL2.Text + ",'" + txtDealsIn.Text + "','" + txtStockist.Text + "','" + txtPath.Text + "','" + Convert.ToDateTime(dateTimePicker1.Text).ToString(dateformate) + "','" + Convert.ToDateTime(dateTimePicker2.Text).ToString(dateformate) + "',1)");
            string startdate = dateTimePicker1.Text;
            string enddate = dateTimePicker2.Text;
            string fy1 = defaultdateformate.convertdate(Convert.ToString(startdate), "dd-MM-yyyy", "yyyy-MM-dd", '-');
            string fy2 = defaultdateformate.convertdate(Convert.ToString(enddate), "dd-MM-yyyy", "yyyy-MM-dd", '-');
            bool concat = txtAddress.Text.Contains("'");
            string address = string.Empty;
            if (concat)
            {
                char[] textArray = txtAddress.Text.ToCharArray();
                for (int i = 0; i < txtAddress.Text.Length; i++)
                {
                    if (textArray[i].Equals('\''))
                    {
                        address += "'";
                    }
                    address += Convert.ToString(textArray[i]);
                }
            }
            else
            {
                address = txtAddress.Text;
            }

            bool concat2 = txtAddress2.Text.Contains("'");
            string address2 = string.Empty;
            if (concat2)
            {
                char[] textArray = txtAddress2.Text.ToCharArray();
                for (int i = 0; i < txtAddress2.Text.Length; i++)
                {
                    if (textArray[i].Equals('\''))
                    {
                        address2 += "'";
                    }
                    address2 += Convert.ToString(textArray[i]);
                }
            }
            else
            {
                address2 = txtAddress2.Text;
            }
            ods.execute("INSERT INTO [Company]([CompanyID],[CompanyName],[SubName],[Address],[Address2],[City],[State],[Statecode],[Country],[Phone],[Mobile],[Email],[Website],[CSTNo],[PANNo],[VATNo],[DLNo1],[DLNo2],[DealsIn],[Stockist],[SQLDatabase],[StartDate],[EndDate],[DatabaseName],[DatabaseConnectionString],[currency],[isActive])VALUES('" + txthCompId.Text + "','" + txtCompName.Text + "','" + txtSubName.Text + "','" + address + "','" + address2 + "','" + txtCity.Text + "','" + txtstate.Text + "','" + txtscode.Text + "','" + txtcountry.Text + "','" + txtPhone.Text + "','" + txtMobile.Text + "','" + txtEmail.Text + "','" + txtWebsite.Text + "','" + txtCSTNo.Text + "','" + txtPANNo.Text + "','" + txtVATNo.Text + "','" + txtDL1.Text + "','" + txtDL2.Text + "','" + txtDealsIn.Text + "','" + txtStockist.Text + "','" + database + "','" + fy1 + "','" + fy2 + "','" + databasename + "','" + connectionstring + "','" + txtcurrency.Text + "',1)");
            ds1 = ods.getdata("select * from company");
            dtreg = ds1.Tables[0];
            if (dtreg.Rows.Count == 1)
            {
                CultureInfo en = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentCulture = en;
                d = DateTime.Now.ToString("dd-MM-yyyy");
                d = defaultdateformate.convertdate(Convert.ToString(d), "dd-MM-yyyy", "yyyy-MM-dd", '-');
                t = DateTime.Now.ToString("H:mm tt");
                DateTime dt1 = Convert.ToDateTime(d);
                //DateTime dt1 = DateTime.ParseExact(d, "dd-MM-yyyy", en);
                //DateTime dt1 = Convert.ToDateTime(d);
                dt1 = Convert.ToDateTime(d).AddDays(365);
                //  dt1 = DateTime.Now.AddDays(364);
                //  DateTime dt2 = DateTime.Now.AddDays(7);
                DateTime dt2 = Convert.ToDateTime(d);
                dt2 = Convert.ToDateTime(d).AddDays(7);
                s = dt1.ToString("dd-MM-yyyy");
                string e = dt2.ToString("dd-MM-yyyy");
                regdate = Encryptregtime(d);
                regtime = Encryptregtime(t);
                expdate = Encryptregtime(s);
                expdt = Encryptregtime(e);
                string st = "Edu";
                Encryptstatus(st);
                ods.execute("INSERT INTO [tblreg]([d10],[d11],[d12],[d13],[d14],[d15],[d16],[d17],[d18])VALUES('" + regdate + "','" + regtime + "','" + regdate + "','" + regtime + "','" + expdate + "','" + regtime + "','" + status + "','" + regdate + "','" + expdt + "')");
            }
            SqlConnection Conn = new SqlConnection("Data Source='" + sqlsetting.servername + "';Initial Catalog='" + databasename + "';User ID='" + sqlsetting.username + "';Password='" + sqlsetting.pass + "'");
            string str1 = "Update Company set DatabaseConnectionString='" + connectionstring + "' where companyID='" + txthCompId.Text + "'";
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
            Conn.Open();
            var cmd = new SqlCommand(str1, Conn);
            cmd.ExecuteNonQuery();
            Conn.Close();
            clearAll();
            MessageBox.Show("Company Created Successfully.");
        }
        public void afterdatabasewithwindows()
        {
            connectionstring = "Data Source='" + sqlsetting.servername + "';Initial Catalog='" + databasename + "';Integrated Security=SSPI;";
            connectionstring = connectionstring.Replace("'", "");
            connectionstring = connectionstring.Trim('"');
            // conn.execute("INSERT INTO [Company]([CompanyID],[CompanyName],[SubName],[Address],[Address2],[City],[State],[Statecode],[Country],[Phone],[Mobile],[Email],[Website],[CSTNo],[PANNo],[VATNo],[DLNo1],[DLNo2],[DealsIn],[Stockist],[Path],[StartDate],[EndDate],[isActive])VALUES('" + txthCompId.Text + "','" + txtCompName.Text + "','" + txtSubName.Text + "','" + txtAddress.Text + "','" + txtAddress2.Text + "','" + txtCity.Text + "','" + txtstate.Text + "','" + txtscode.Text + "','" + txtcountry.Text + "','" + txtPhone.Text + "','" + txtMobile.Text + "','" + txtEmail.Text + "','" + txtWebsite.Text + "','" + txtCSTNo.Text + "','" + txtPANNo.Text + "','" + txtVATNo.Text + "','" + txtDL1.Text + "','" + txtDL2.Text + "','" + txtDealsIn.Text + "','" + txtStockist.Text + "','" + txtPath.Text + "','" + Convert.ToDateTime(dateTimePicker1.Text).ToString(dateformate) + "','" + Convert.ToDateTime(dateTimePicker2.Text).ToString(dateformate) + "',1)");
            //sc.execute("INSERT INTO [Company]([CompanyID],[CompanyName],[SubName],[Address],[Address2],[City],[Phone],[Mobile],[Email],[Website],[CSTNo],[PANNo],[VATNo],[DLNo1],[DLNo2],[DealsIn],[Stockist],[Path],[StartDate],[EndDate],[isActive])VALUES('" + txthCompId.Text + "','" + txtCompName.Text + "','" + txtSubName.Text + "','" + txtAddress.Text + "','" + txtAddress2.Text + "','" + txtCity.Text + "'," + txtPhone.Text + ",'" + txtMobile.Text + "','" + txtEmail.Text + "','" + txtWebsite.Text + "','" + txtCSTNo.Text + "','" + txtPANNo.Text + "','" + txtVATNo.Text + "'," + txtDL1.Text + "," + txtDL2.Text + ",'" + txtDealsIn.Text + "','" + txtStockist.Text + "','" + txtPath.Text + "','" + Convert.ToDateTime(dateTimePicker1.Text).ToString(dateformate) + "','" + Convert.ToDateTime(dateTimePicker2.Text).ToString(dateformate) + "',1)");
            string startdate = dateTimePicker1.Text;
            string enddate = dateTimePicker2.Text;
            string fy1 = defaultdateformate.convertdate(Convert.ToString(startdate), "dd-MM-yyyy", "yyyy-MM-dd", '-');
            string fy2 = defaultdateformate.convertdate(Convert.ToString(enddate), "dd-MM-yyyy", "yyyy-MM-dd", '-');
            bool concat = txtAddress.Text.Contains("'");
            string address = string.Empty;
            if (concat)
            {
                char[] textArray = txtAddress.Text.ToCharArray();
                for (int i = 0; i < txtAddress.Text.Length; i++)
                {
                    if (textArray[i].Equals('\''))
                    {
                        address += "'";
                    }
                    address += Convert.ToString(textArray[i]);
                }
            }
            else
            {
                address = txtAddress.Text;
            }

            bool concat2 = txtAddress2.Text.Contains("'");
            string address2 = string.Empty;
            if (concat2)
            {
                char[] textArray = txtAddress2.Text.ToCharArray();
                for (int i = 0; i < txtAddress2.Text.Length; i++)
                {
                    if (textArray[i].Equals('\''))
                    {
                        address2 += "'";
                    }
                    address2 += Convert.ToString(textArray[i]);
                }
            }
            else
            {
                address2 = txtAddress2.Text;
            }
            ods.execute("INSERT INTO [Company]([CompanyID],[CompanyName],[SubName],[Address],[Address2],[City],[State],[Statecode],[Country],[Phone],[Mobile],[Email],[Website],[CSTNo],[PANNo],[VATNo],[DLNo1],[DLNo2],[DealsIn],[Stockist],[SQLDatabase],[StartDate],[EndDate],[DatabaseName],[DatabaseConnectionString],[currency],[isActive])VALUES('" + txthCompId.Text + "','" + txtCompName.Text + "','" + txtSubName.Text + "','" + address + "','" + address2 + "','" + txtCity.Text + "','" + txtstate.Text + "','" + txtscode.Text + "','" + txtcountry.Text + "','" + txtPhone.Text + "','" + txtMobile.Text + "','" + txtEmail.Text + "','" + txtWebsite.Text + "','" + txtCSTNo.Text + "','" + txtPANNo.Text + "','" + txtVATNo.Text + "','" + txtDL1.Text + "','" + txtDL2.Text + "','" + txtDealsIn.Text + "','" + txtStockist.Text + "','" + database + "','" + fy1 + "','" + fy2 + "','" + databasename + "','" + connectionstring + "','" + txtcurrency.Text + "',1)");
            ds1 = ods.getdata("select * from company");
            dtreg = ds1.Tables[0];
            if (dtreg.Rows.Count == 1)
            {
                CultureInfo en = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentCulture = en;
                d = DateTime.Now.ToString("dd-MM-yyyy");
                d = defaultdateformate.convertdate(Convert.ToString(d), "dd-MM-yyyy", "yyyy-MM-dd", '-');
                t = DateTime.Now.ToString("H:mm tt");
                DateTime dt1 = Convert.ToDateTime(d);
                //DateTime dt1 = DateTime.ParseExact(d, "dd-MM-yyyy", en);
                //DateTime dt1 = Convert.ToDateTime(d);
                dt1 = Convert.ToDateTime(d).AddDays(365);
                //  dt1 = DateTime.Now.AddDays(364);
                //  DateTime dt2 = DateTime.Now.AddDays(7);
                DateTime dt2 = Convert.ToDateTime(d);
                dt2 = Convert.ToDateTime(d).AddDays(7);
                s = dt1.ToString("dd-MM-yyyy");
                string e = dt2.ToString("dd-MM-yyyy");
                regdate = Encryptregtime(d);
                regtime = Encryptregtime(t);
                expdate = Encryptregtime(s);
                expdt = Encryptregtime(e);
                string st = "Edu";
                Encryptstatus(st);
                ods.execute("INSERT INTO [tblreg]([d10],[d11],[d12],[d13],[d14],[d15],[d16],[d17],[d18])VALUES('" + regdate + "','" + regtime + "','" + regdate + "','" + regtime + "','" + expdate + "','" + regtime + "','" + status + "','" + regdate + "','" + expdt + "')");
            }
            SqlConnection Conn = new SqlConnection("Data Source='" + sqlsetting.servername + "';Initial Catalog='" + databasename + "';Integrated Security=SSPI;");
            string str1 = "Update Company set DatabaseConnectionString='" + connectionstring + "' where companyID='" + txthCompId.Text + "'";
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
            Conn.Open();
            var cmd = new SqlCommand(str1, Conn);
            cmd.ExecuteNonQuery();
            Conn.Close();
            clearAll();
            MessageBox.Show("Company Created Successfully.");
        }
        public void createdatabasewithwindows()
        {
            using (SqlConnection myConn = new SqlConnection("Data Source='" + sqlsetting.servername + "';Integrated Security=SSPI;"))
            {
                try
                {
                    string str;
                    string databasepath = "";
                    string appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                    ds2 = ods.getdata("select * from Path");
                    DataTable path = new DataTable();
                    path = ds2.Tables[0];
                    if (path.Rows.Count <= 0)
                    {
                        //if (path.Rows[0]["DefaultPath"].ToString() == "" || path.Rows[0]["DefaultPath"].ToString() == null)
                        //{
                        //  string Inspath = "INSERT INTO [Path]([DefaultPath] values('" + appPath + "')";
                        ods.execute("INSERT INTO [Path]([DefaultPath])VALUES('" + appPath + "')");
                        ds2 = ods.getdata("select * from Path");
                        //  DataTable path1 = new DataTable();
                        path = ds2.Tables[0];

                        databasepath = path.Rows[0]["DefaultPath"].ToString() + @"\Data\";

                        //}
                        //else
                        //{

                        //}
                    }
                    else
                    {
                        databasepath = path.Rows[0]["DefaultPath"].ToString() + @"\Data\";
                    }

                    //  appPath.Replace("'\'", "\\");
                    databasename = "Data" + txthCompId.Text;
                    databasepath = databasepath + databasename;
                    //FILENAME = 'J:\\contil\\29-02-2017\\contil\\Contil\\trunk\\Ramdev Sales Final\\RamdevSales\\bin\\Debug\\SQLDB\\tempdb12.mdf'
                    str = "CREATE DATABASE " + databasename + " ON PRIMARY " +
    "(NAME = " + databasename + "_Data, " +
    "FILENAME = '" + databasepath + ".mdf', " +
    "SIZE = 5MB, MAXSIZE = 50MB, FILEGROWTH = 100%) " +
    "LOG ON (NAME = " + databasename + ", " +
    "FILENAME = '" + databasepath + ".ldf', " +
    "SIZE = 512KB, " +
    "MAXSIZE = 10MB, " +
    "FILEGROWTH = 10%)";
                    if (myConn.State == ConnectionState.Open)
                    {
                        myConn.Close();
                    }
                    myConn.Open();
                    SqlCommand myCommand = new SqlCommand(str, myConn);
                    myCommand.ExecuteNonQuery();
                    SqlConnection Conn = new SqlConnection("Data Source='" + sqlsetting.servername + "';Initial Catalog='" + databasename + "';Integrated Security=SSPI;");

                    databasepath = path.Rows[0]["DefaultPath"].ToString() + @"\SQLDB\";
                    // string appPath1 = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\SQLDB\";
                    var fileContent = File.ReadAllText(databasepath + "\\script.sql");
                    var sqlqueries = fileContent.Split(new[] { " GO " }, StringSplitOptions.RemoveEmptyEntries);


                    var cmd = new SqlCommand("query", Conn);

                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                    }
                    Conn.Open();
                    foreach (var query in sqlqueries)
                    {
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();


                    }
                    //string date12 = date.convertdate(date1, "dd-MMM-yyyy", "yyyy-MM-dd", '-');
                    //string startdate =dateTimePicker1.Value.ToString("yyyy-MM-dd");
                    //string enddate = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                    string startdate = dateTimePicker1.Text;
                    string enddate = dateTimePicker2.Text;
                    string fy1 = defaultdateformate.convertdate(Convert.ToString(startdate), "dd-MM-yyyy", "yyyy-MM-dd", '-');
                    //DateTime d = Convert.ToDateTime(fy1);
                    //string f = d.ToString("yyyy-MM-dd");
                    //string fy2 = Convert.ToString(d.AddDays(364));
                    string fy2 = defaultdateformate.convertdate(Convert.ToString(enddate), "dd-MM-yyyy", "yyyy-MM-dd", '-');
                    //fy2 = DateTime.Parse(fy2).ToString("dd-MM-yyyy");//   dateTimePicker1.Value.AddDays(364));
                    // string fy2 = DateTime.Parse(dateTimePicker1.Text).ToString("dd-MM-yyyy");
                    bool concat = txtAddress.Text.Contains("'");
                    string address = string.Empty;
                    if (concat)
                    {
                        char[] textArray = txtAddress.Text.ToCharArray();
                        for (int i = 0; i < txtAddress.Text.Length; i++)
                        {
                            if (textArray[i].Equals('\''))
                            {
                                address += "'";
                            }
                            address += Convert.ToString(textArray[i]);
                        }
                    }
                    else
                    {
                        address = txtAddress.Text;
                    }

                    bool concat2 = txtAddress2.Text.Contains("'");
                    string address2 = string.Empty;
                    if (concat2)
                    {
                        char[] textArray = txtAddress2.Text.ToCharArray();
                        for (int i = 0; i < txtAddress2.Text.Length; i++)
                        {
                            if (textArray[i].Equals('\''))
                            {
                                address2 += "'";
                            }
                            address2 += Convert.ToString(textArray[i]);
                        }
                    }
                    else
                    {
                        address2 = txtAddress2.Text;
                    }
                    string company = "INSERT INTO [Company]([CompanyID],[CompanyName],[SubName],[Address],[Address2],[City],[State],[Statecode],[Country],[Phone],[Mobile],[Email],[Website],[CSTNo],[PANNo],[VATNo],[DLNo1],[DLNo2],[DealsIn],[Stockist],[SQLDatabase],[StartDate],[EndDate],[DatabaseName],[DatabaseConnectionString],[currency],[isActive])VALUES('" + txthCompId.Text + "','" + txtCompName.Text + "','" + txtSubName.Text + "','" + address + "','" + address2 + "','" + txtCity.Text + "','" + txtstate.Text + "','" + txtscode.Text + "','" + txtcountry.Text + "','" + txtPhone.Text + "','" + txtMobile.Text + "','" + txtEmail.Text + "','" + txtWebsite.Text + "','" + txtCSTNo.Text + "','" + txtPANNo.Text + "','" + txtVATNo.Text + "','" + txtDL1.Text + "','" + txtDL2.Text + "','" + txtDealsIn.Text + "','" + txtStockist.Text + "','" + database + "','" + fy1 + "','" + fy2 + "','" + databasename + "','" + connectionstring + "','" + txtcurrency.Text + "',1)";
                    SqlCommand myCommand1 = new SqlCommand(company, Conn);
                    myCommand1.ExecuteNonQuery();
                    Conn.Close();
                    // MessageBox.Show("DataBase is Created Successfully", "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    if (myConn.State == ConnectionState.Open)
                    {
                        myConn.Close();
                    }
                }
            }
        }
        private void submit()
        {
            try
            {
                if (txtCompName.Text != "")
                {
                    if (btnSave.Text == "Update")
                    {

                     //   conn.execute("UPDATE [dbo].[Company] SET [CompanyName]='" + txtCompName.Text + "',[SubName]='" + txtSubName.Text + "',[Address]='" + txtAddress.Text.Trim() + "',[Address2]='" + txtAddress2.Text.Trim() + "',[City]='" + txtCity.Text + "',State='" + txtstate.Text + "',Statecode='" + txtscode.Text + "',Country='" + txtcountry.Text + "',[Phone]=" + txtPhone.Text + ",[Mobile]='" + txtMobile.Text + "',[Email]='" + txtEmail.Text + "',[Website]='" + txtWebsite.Text + "',[CSTNo]='" + txtCSTNo.Text + "',[PANNo]='" + txtPANNo.Text + "',[VATNo]='" + txtVATNo.Text + "',[DLNo1]= " + txtDL1.Text + ",[DLNo2]=" + txtDL2.Text + ",[DealsIn]='" + txtDealsIn.Text + "',[Stockist]='" + txtStockist.Text.Trim() + "',[Path]='" + txtPath.Text + "',[StartDate]='" + Convert.ToDateTime(dateTimePicker1.Text).ToString(dateformate) + "',[EndDate]='" + Convert.ToDateTime(dateTimePicker2.Text).ToString(dateformate) + "',[isActive]=1 WHERE CompanyID='" + Master.companyId + "'");
                      //  ods.execute("UPDATE [Company] SET [CompanyName]='" + txtCompName.Text + "',[SubName]='" + txtSubName.Text + "',[Address]='" + txtAddress.Text.Trim() + "',[Address2]='" + txtAddress2.Text.Trim() + "',[City]='" + txtCity.Text + "',State='" + txtstate.Text + "',Statecode='" + txtscode.Text + "',Country='" + txtcountry.Text + "',[Phone]=" + txtPhone.Text + ",[Mobile]='" + txtMobile.Text + "',[Email]='" + txtEmail.Text + "',[Website]='" + txtWebsite.Text + "',[CSTNo]='" + txtCSTNo.Text + "',[PANNo]='" + txtPANNo.Text + "',[VATNo]='" + txtVATNo.Text + "',[DLNo1]= " + txtDL1.Text + ",[DLNo2]=" + txtDL2.Text + ",[DealsIn]='" + txtDealsIn.Text + "',[Stockist]='" + txtStockist.Text.Trim() + "',[Path]='" + txtPath.Text + "',[StartDate]='" + Convert.ToDateTime(dateTimePicker1.Text).ToString() + "',[EndDate]='" + Convert.ToDateTime(dateTimePicker2.Text).ToString() + "',[isActive]=1 WHERE CompanyID='" + Master.companyId + "'");

                     //   btnSave.Text = "Save";
                      //  MessageBox.Show("Company Updated Successfully.");
                        // this.Close();
                      

                    }
                    else
                    {
                      
                        getsr();
                        if (chkdatabase.Checked == true)
                        {
                            database = "1";
                        }
                        else
                        {
                            database = "0";
                        }
                        if (sqlsetting.Authentication == "Windows Authentication")
                        {
                            createdatabasewithwindows();
                            afterdatabasewithwindows();
                        }
                        else
                        {
                            createdatabasewithusername();
                            afterdatabasewithusername();
                        }
                       
                    }
                }
                else
                {
                    MessageBox.Show("Company Name cannot be Blank");
                    this.ActiveControl = txtCompName;
                    return;
                }


            }
            catch (Exception ex)
            {
               // MessageBox.Show("Error:" + ex.Message);
            }
            finally
            {
                
            }
        }

        private void clearAll()
        {
            sqlsetting.servername = "";
            sqlsetting.username = "";
            sqlsetting.pass = "";
            txtCompName.Text = "";
            txtSubName.Text = "";
            txtAddress.Text = "";
            txtAddress2.Text = "";
            txtCity.Text = "";
            txtscode.Text = "";
            txtstate.Text = "";
            txtcountry.Text = "";
            txtPhone.Text = "";
            txtMobile.Text = "";
            txtEmail.Text = "";
            txtWebsite.Text = "";
            txtCSTNo.Text = "";
            txtPANNo.Text = "";
            txtVATNo.Text = "";
            txtDL1.Text = "";
            txtDL2.Text = "";
            txtDealsIn.Text = "";
            txtStockist.Text = "";
            //txtPath.Text = "";
            dateTimePicker1.Text = "";
            dateTimePicker1.Text = "";
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();

            }
        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            //dialog.Filter = "Text files | *.txt |"; // file types, that will be allowed to upload
            dialog.Multiselect = false; // allow/deny user to upload more than one file at a time
            if (dialog.ShowDialog() == DialogResult.OK) // if user clicked OK
            {
                String path = dialog.FileName; // get name of file
                using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open), new UTF8Encoding())) // do anything you want, e.g. read it
                {
                    //txtPath.Text = path;
                    //txtPath.Focus();
                }
            }
        }

        private void txtPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                dateTimePicker1.Focus();
            }
        }

        //int cnt = 0;
        //internal void updatemode(string str, string p, int p_2)
        //{
        //    loadPage();
        //    cnt = 1;
        //    SqlCommand cmd = new SqlCommand("select * from Company where CompanyID='" + p + "' and isactive=1", con);
        //    SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    sda.Fill(dt);
        //    txthCompId.Text = p;
        //    txtCompName.Text = dt.Rows[0][1].ToString();
        //    txtSubName.Text=dt.Rows[0][2].ToString();
        //    txtAddress.Text = dt.Rows[0][3].ToString();
        //    txtAddress2.Text = dt.Rows[0][4].ToString();
        //    txtCity.Text = dt.Rows[0][5].ToString();
        //    txtPhone.Text = dt.Rows[0][6].ToString();
        //    txtMobile.Text = dt.Rows[0][7].ToString();
        //    txtEmail.Text = dt.Rows[0][8].ToString();
        //    txtWebsite.Text = dt.Rows[0][9].ToString();
        //    txtCSTNo.Text = dt.Rows[0][10].ToString();
        //    txtPANNo.Text = dt.Rows[0][11].ToString();
        //    txtVATNo.Text = dt.Rows[0][12].ToString();
        //    txtDL1.Text = dt.Rows[0][13].ToString();
        //    txtDL2.Text = dt.Rows[0][14].ToString();
        //    txtDealsIn.Text = dt.Rows[0][15].ToString();
        //    txtStockist.Text = dt.Rows[0][16].ToString();
        //    txtPath.Text = dt.Rows[0][17].ToString();
        //    if (dt.Rows[0][18].ToString() == "")
        //    {
        //        dt.Rows[0][18] = DateTime.Now.ToString(dateformate);
        //    }
        //    else
        //    {
        //        dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[0][18].ToString());
        //    }
        //    if (dt.Rows[0][19].ToString() == "")
        //    {
        //        dt.Rows[0][19] = DateTime.Now.ToString(dateformate);
        //    }
        //    else
        //    {
        //        dateTimePicker2.Value = Convert.ToDateTime(dt.Rows[0][19].ToString());
        //    }
            
        //    txtCompName.Focus();

        //    btnSave.Text = "Update";


        //    con.Close();
        //}

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            
            //dateTimePicker2.Value = DateTime.Now.AddDays(365);
            dateTimePicker2.Value = dateTimePicker1.Value.AddDays(364);
            btnSave.Focus();
        }

        private void dateTimePicker2_KeyDown(object sender, KeyEventArgs e)
        {
            btnSave.Focus();
        }


        internal void Passed(int p)
        {
            gener = p;
            master.enablemenu(false);
        }

        

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.Value = dateTimePicker1.Value.AddDays(364);
        }

        private void txtstate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtscode.Focus();
            }
        }

        private void txtscode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtcountry.Focus();
            }
        }

        private void txtcountry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPhone.Focus();
            }
        }

        private void linksettings_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sqlsetting s = new sqlsetting();
            s.ShowDialog();
        }

        private void chkdatabase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                dateTimePicker1.Focus();
            }
        }

        private void txtcurrency_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //chkdatabase.Focus();
                txtAddress.Focus();
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtMobile_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex rEmail = new System.Text.RegularExpressions.Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            if (txtEmail.Text.Length > 0)
            {
                if (!rEmail.IsMatch(txtEmail.Text))
                {
                    MessageBox.Show("Please provide valid email address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmail.Text = "";
                    txtEmail.Focus();
                }
            }
        }

        private void txtWebsite_Validating(object sender, CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex rEmail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9\-\.]+\.(co|in|com|org|net|mil|edu|COM|ORG|NET|MIL|EDU)$");
            if (txtWebsite.Text.Length > 0)
            {
                if (!rEmail.IsMatch(txtWebsite.Text))
                {
                    MessageBox.Show("Please provide valid Website address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtWebsite.Text = "";
                    txtWebsite.Focus();
                }
            }
        }

        private void txtCompName_Enter(object sender, EventArgs e)
        {
            txtCompName.BackColor = Color.LightYellow;
        }

        private void txtCompName_Leave(object sender, EventArgs e)
        {
            txtCompName.BackColor = Color.White;
        }

        private void txtSubName_Enter(object sender, EventArgs e)
        {
            txtSubName.BackColor = Color.LightYellow;
        }

        private void txtSubName_Leave(object sender, EventArgs e)
        {
            txtSubName.BackColor = Color.White;
        }

        private void txtAddress_Enter(object sender, EventArgs e)
        {
            txtAddress.BackColor = Color.LightYellow;
        }

        private void txtAddress_Leave(object sender, EventArgs e)
        {
            txtAddress.BackColor = Color.White;
        }

        private void txtAddress2_Enter(object sender, EventArgs e)
        {
            txtAddress2.BackColor = Color.LightYellow;
        }

        private void txtAddress2_Leave(object sender, EventArgs e)
        {
            txtAddress2.BackColor = Color.White;
        }

        private void txtCity_Enter(object sender, EventArgs e)
        {
            txtCity.BackColor = Color.LightYellow;
        }

        private void txtCity_Leave(object sender, EventArgs e)
        {
            txtCity.BackColor = Color.White;
        }

        private void txtstate_Enter(object sender, EventArgs e)
        {
            txtstate.BackColor = Color.LightYellow;
        }

        private void txtstate_Leave(object sender, EventArgs e)
        {
            txtstate.BackColor = Color.White;
        }

        private void txtscode_Enter(object sender, EventArgs e)
        {
            txtscode.BackColor = Color.LightYellow;
        }

        private void txtscode_Leave(object sender, EventArgs e)
        {
            txtscode.BackColor = Color.White;
        }

        private void txtcountry_Enter(object sender, EventArgs e)
        {
            txtcountry.BackColor = Color.LightYellow;
        }

        private void txtcountry_Leave(object sender, EventArgs e)
        {
            txtcountry.BackColor = Color.White;
        }

        private void txtPhone_Enter(object sender, EventArgs e)
        {
            txtPhone.BackColor = Color.LightYellow;
        }

        private void txtPhone_Leave(object sender, EventArgs e)
        {
            txtPhone.BackColor = Color.White;
        }

        private void txtMobile_Enter(object sender, EventArgs e)
        {
            txtMobile.BackColor = Color.LightYellow;
        }

        private void txtMobile_Leave(object sender, EventArgs e)
        {
            txtMobile.BackColor = Color.White;
        }

        private void txtEmail_Enter(object sender, EventArgs e)
        {
            txtEmail.BackColor = Color.LightYellow;
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            txtEmail.BackColor = Color.White;
        }

        private void txtWebsite_Enter(object sender, EventArgs e)
        {
            txtWebsite.BackColor = Color.LightYellow;
        }

        private void txtWebsite_Leave(object sender, EventArgs e)
        {
            txtWebsite.BackColor = Color.White;
        }

        private void txtCSTNo_Enter(object sender, EventArgs e)
        {
            txtCSTNo.BackColor = Color.LightYellow;
        }

        private void txtCSTNo_Leave(object sender, EventArgs e)
        {
            txtCSTNo.BackColor = Color.White;
        }

        private void txtPANNo_Enter(object sender, EventArgs e)
        {
            txtPANNo.BackColor = Color.LightYellow;
        }

        private void txtPANNo_Leave(object sender, EventArgs e)
        {
            txtPANNo.BackColor = Color.White;
        }

        private void txtVATNo_Enter(object sender, EventArgs e)
        {
            txtVATNo.BackColor = Color.LightYellow;
        }

        private void txtVATNo_Leave(object sender, EventArgs e)
        {
            txtVATNo.BackColor = Color.White;
        }

        private void txtDL1_Enter(object sender, EventArgs e)
        {
            txtDL1.BackColor = Color.LightYellow;
        }

        private void txtDL1_Leave(object sender, EventArgs e)
        {
            txtDL1.BackColor = Color.White;
        }

        private void txtDL2_Enter(object sender, EventArgs e)
        {
            txtDL2.BackColor = Color.LightYellow;
        }

        private void txtDL2_Leave(object sender, EventArgs e)
        {
            txtDL2.BackColor = Color.White;
        }

        private void txtDealsIn_Enter(object sender, EventArgs e)
        {
            txtDealsIn.BackColor = Color.LightYellow;
        }

        private void txtDealsIn_Leave(object sender, EventArgs e)
        {
            txtDealsIn.BackColor = Color.White;
        }

        private void txtStockist_Enter(object sender, EventArgs e)
        {
            txtStockist.BackColor = Color.LightYellow;
        }

        private void txtStockist_Leave(object sender, EventArgs e)
        {
            txtStockist.BackColor = Color.White;
        }

        private void txtcurrency_Enter(object sender, EventArgs e)
        {
            txtcurrency.BackColor = Color.LightYellow;
        }

        private void txtcurrency_Leave(object sender, EventArgs e)
        {
            txtcurrency.BackColor = Color.White;
        }

        

        private void txtCompName_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            //SolidBrush fillbrush = new SolidBrush(Color.Transparent);
            ////draw rectangle behind the tabs
            //Rectangle lasttabrect = tabControl1.GetTabRect(tabControl1.TabPages.Count - 1);
            //Rectangle background = new Rectangle();
            //background.Location = new Point(lasttabrect.Right, 0);

            ////pad the rectangle to cover the 1 pixel line between the top of the tabpage and the start of the tabs
            //background.Size = new Size(tabControl1.Right - background.Left, lasttabrect.Height + 1);
            //e.Graphics.FillRectangle(fillbrush, background);
            ChangeTabColor(e);
        }

        private void ChangeTabColor(DrawItemEventArgs e)
        {
            Brush BackBrush = new SolidBrush(Color.Transparent);

            e.Graphics.FillRectangle(BackBrush, e.Bounds);

            BackBrush.Dispose();
        }

        private void tabControl1_MouseHover(object sender, EventArgs e)
        {
            //for (int i = 0; i < tabControl1.TabCount - 1; i++)
            //{
            //    if (tabControl1.GetTabRect(i).Contains(e.X, e.Y))
            //    {
            //        tabControl1.SelectedIndex = i;
            //    }
            //}
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

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

        private void chkdatabase_CheckedChanged(object sender, EventArgs e)
        {
            if (chkdatabase.Checked == true)
            {
                linksettings.Visible = true;
            }
            else
            {
                linksettings.Visible = false;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        

       

































    }
}
