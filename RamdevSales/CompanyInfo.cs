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

namespace RamdevSales
{
    public partial class CompanyInfo : Form
    {
       
        Connection conn = new Connection();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        ServerConnection sc = new ServerConnection();
        OleDbSettings ods = new OleDbSettings();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        datetime defaultdateformate = new datetime();
        private CompanyList companyList;
        private Master master;
        public CompanyInfo()
        {
            InitializeComponent();
           
        }
        int gener = 0;
        int flag = 0;
        string dateformate = string.Empty;
        private TabControl tabControl;
        public CompanyInfo(CompanyList companyList)
        {
            InitializeComponent();
            this.companyList = companyList;
        }

        public CompanyInfo(Master master)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
        }

        public CompanyInfo(TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.tabControl = tabControl;
        }

        public CompanyInfo(Master master, TabControl tabControl)
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

               ds =conn.getdata("select max(CompanyID) from Company where isactive='1'");
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
        private void CompanyInfo_Load(object sender, EventArgs e)
        {
            loadPage();
            tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(TabPagesDrawItem);
            this.ActiveControl = txtCompName;
        }

        private void loadPage()
        {
           
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            //getsr();
           
           // this.ActiveControl = txtCompName;
            txtCompName.Focus();
           
            getcompInfo();
            dateTimePicker1.CustomFormat = Master.dateformate;
            dateTimePicker2.CustomFormat = Master.dateformate;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
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
                        txtPath.Text = dt.Rows[0][20].ToString();
                        string factory;
                        if (dt.Rows[0]["isfactory"].ToString() == "True")
                        {
                            chkisfactory.Checked = true;
                        }
                        else
                        {
                            chkisfactory.Checked = false;
                        }
                        txtonlineconnectionstring.Text =dt.Rows[0]["LinkRemote"].ToString();
                        string startdate = DateTime.Parse(dt.Rows[0]["StartDate"].ToString()).ToString("yyyy-MM-dd");
                        string fy1 = defaultdateformate.convertdate(startdate, "yyyy-MM-dd", Master.dateformate,'-');
                      // string fy1 = DateTime.Parse(dt.Rows[0]["StartDate"].ToString()).ToString(Master.dateformate);
                        dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[0]["StartDate"].ToString());
                        txtcurrency.Text = dt.Rows[0]["currency"].ToString();
                        btnSave.Text = "Update";
                        string appPathpic = Path.GetDirectoryName(Application.ExecutablePath) + @"\Images\CompanyLogo\";
                        try
                        {
                            pic1.Image = Image.FromFile(appPathpic + dt.Rows[0]["companylogo"].ToString());
                        }
                        catch { }
                        photo1[1] = dt.Rows[0]["companylogo"].ToString();
                     
                    }
                }
            }
            catch
            {
            }
        }

        private void txtCompName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (btnSave.Text != "Update")
                {
                    flag = 0;
                    string str = txtCompName.Text.ToUpper().Trim();
                    SqlCommand cmd1 = new SqlCommand("select CompanyName from Company where isactive=1", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (txtCompName.Text != "")
                    {
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                string val = dt.Rows[i][0].ToString().ToUpper().Trim();
                                if (val == str)
                                {
                                    MessageBox.Show("Company Already Available Please add Another");
                                    txtCompName.Focus();
                                    flag = 1;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            txtSubName.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Company Name cannot be Blank");
                        txtCompName.Show();
                    }
                    if (flag == 0)
                    {
                        txtSubName.Focus();
                    }
                }
                else
                {
                    txtSubName.Focus();
                }
            }
           
        }

        private void txtSubName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               // txtAddress.Focus();
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
                dateTimePicker1.Focus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            submit();
        }

        private void submit()
        {
            try
            {
                if (txtCompName.Text != "")
                {
                    if (btnSave.Text == "Update")
                    {
                        string factory;
                        if (chkisfactory.Checked == true)
                        {
                            factory = "1";
                        }
                        else
                        {
                            factory = "0";
                        }
                        //conn.execute("UPDATE [dbo].[Company] SET [CompanyName]='" + txtCompName.Text + "',[SubName]='" + txtSubName.Text + "',[Address]='" + txtAddress.Text.Trim() + "',[Address2]='" + txtAddress2.Text.Trim() + "',[City]='" + txtCity.Text + "',[State]='" + txtstate.Text + "',[Statecode]='" + txtscode.Text + "',[Country]='" + txtcountry.Text + "',[Phone]='" + txtPhone.Text + "',[Mobile]='" + txtMobile.Text + "',[Email]='" + txtEmail.Text + "',[Website]='" + txtWebsite.Text + "',[CSTNo]='" + txtCSTNo.Text + "',[PANNo]='" + txtPANNo.Text + "',[VATNo]='" + txtVATNo.Text + "',[DLNo1]= '" + txtDL1.Text + "',[DLNo2]='" + txtDL2.Text + "',[DealsIn]='" + txtDealsIn.Text + "',[Stockist]='" + txtStockist.Text.Trim() + "',[StartDate]='" + Convert.ToDateTime(dateTimePicker1.Text).ToString() + "',[EndDate]='" + Convert.ToDateTime(dateTimePicker2.Text).ToString() +"',[currency]='"+txtcurrency.Text+ "',[isActive]=1 WHERE CompanyID='" + Master.companyId + "'");
                        conn.execute("UPDATE [dbo].[Company] SET [CompanyName]='" + txtCompName.Text + "',[SubName]='" + txtSubName.Text + "',[Address]='" + txtAddress.Text.Trim() + "',[Address2]='" + txtAddress2.Text.Trim() + "',[City]='" + txtCity.Text + "',[State]='" + txtstate.Text + "',[Statecode]='" + txtscode.Text + "',[Country]='" + txtcountry.Text + "',[Phone]='" + txtPhone.Text + "',[Mobile]='" + txtMobile.Text + "',[Email]='" + txtEmail.Text + "',[Website]='" + txtWebsite.Text + "',[CSTNo]='" + txtCSTNo.Text + "',[PANNo]='" + txtPANNo.Text + "',[VATNo]='" + txtVATNo.Text + "',[DLNo1]= '" + txtDL1.Text + "',[DLNo2]='" + txtDL2.Text + "',[DealsIn]='" + txtDealsIn.Text + "',[Stockist]='" + txtStockist.Text.Trim() + "',[currency]='" + txtcurrency.Text + "',[companylogo]='" + photo1[1] + "',[LinkRemote]='" + txtonlineconnectionstring.Text + "',isfactory='"+factory+"',[isActive]=1 WHERE CompanyID='" + Master.companyId + "'");
                        //ods.execute("UPDATE [Company] SET [CompanyName]='" + txtCompName.Text + "',[SubName]='" + txtSubName.Text + "',[Address]='" + txtAddress.Text.Trim() + "',[Address2]='" + txtAddress2.Text.Trim() + "',[City]='" + txtCity.Text + "',State='" + txtstate.Text + "',Statecode='" + txtscode.Text + "',Country='" + txtcountry.Text + "',[Phone]='" + txtPhone.Text + "',[Mobile]='" + txtMobile.Text + "',[Email]='" + txtEmail.Text + "',[Website]='" + txtWebsite.Text + "',[CSTNo]='" + txtCSTNo.Text + "',[PANNo]='" + txtPANNo.Text + "',[VATNo]='" + txtVATNo.Text + "',[DLNo1]= '" + txtDL1.Text + "',[DLNo2]='" + txtDL2.Text + "',[DealsIn]='" + txtDealsIn.Text + "',[Stockist]='" + txtStockist.Text.Trim() + "',[StartDate]='" + Convert.ToDateTime(dateTimePicker1.Text).ToString(dateformate) + "',[EndDate]='" + Convert.ToDateTime(dateTimePicker2.Text).ToString(dateformate) + "',[currency]='" + txtcurrency.Text + "',[isActive]=1 WHERE CompanyID='" + Master.companyId + "'");
                        ods.execute("UPDATE [Company] SET [CompanyName]='" + txtCompName.Text + "',[SubName]='" + txtSubName.Text + "',[Address]='" + txtAddress.Text.Trim() + "',[Address2]='" + txtAddress2.Text.Trim() + "',[City]='" + txtCity.Text + "',State='" + txtstate.Text + "',Statecode='" + txtscode.Text + "',Country='" + txtcountry.Text + "',[Phone]='" + txtPhone.Text + "',[Mobile]='" + txtMobile.Text + "',[Email]='" + txtEmail.Text + "',[Website]='" + txtWebsite.Text + "',[CSTNo]='" + txtCSTNo.Text + "',[PANNo]='" + txtPANNo.Text + "',[VATNo]='" + txtVATNo.Text + "',[DLNo1]= '" + txtDL1.Text + "',[DLNo2]='" + txtDL2.Text + "',[DealsIn]='" + txtDealsIn.Text + "',[Stockist]='" + txtStockist.Text.Trim() + "',[currency]='" + txtcurrency.Text + "',[LinkRemote]='"+txtonlineconnectionstring.Text+"',[isActive]=1 WHERE CompanyID='" + Master.companyId + "'");
                        btnSave.Text = "Submit";
                        MessageBox.Show("Company Updated Successfully.");
                        // this.Close();
                        master.RemoveCurrentTab();

                    }
                    else
                    {
                        getsr();

                      //  conn.execute("INSERT INTO [Company]([CompanyID],[CompanyName],[SubName],[Address],[Address2],[City],[State],[Statecode],[Country],[Phone],[Mobile],[Email],[Website],[CSTNo],[PANNo],[VATNo],[DLNo1],[DLNo2],[DealsIn],[Stockist],[Path],[StartDate],[EndDate],[isActive])VALUES('" + txthCompId.Text + "','" + txtCompName.Text + "','" + txtSubName.Text + "','" + txtAddress.Text + "','" + txtAddress2.Text + "','" + txtCity.Text + "','" + txtstate.Text + "','" + txtscode.Text + "','" + txtcountry.Text + "','" + txtPhone.Text + "','" + txtMobile.Text + "','" + txtEmail.Text + "','" + txtWebsite.Text + "','" + txtCSTNo.Text + "','" + txtPANNo.Text + "','" + txtVATNo.Text + "','" + txtDL1.Text + "','" + txtDL2.Text + "','" + txtDealsIn.Text + "','" + txtStockist.Text + "','" + txtPath.Text + "','" + Convert.ToDateTime(dateTimePicker1.Text).ToString(Master.dateformate) + "','" + Convert.ToDateTime(dateTimePicker2.Text).ToString(Master.dateformate) + "',1)");
                        //sc.execute("INSERT INTO [Company]([CompanyID],[CompanyName],[SubName],[Address],[Address2],[City],[Phone],[Mobile],[Email],[Website],[CSTNo],[PANNo],[VATNo],[DLNo1],[DLNo2],[DealsIn],[Stockist],[Path],[StartDate],[EndDate],[isActive])VALUES('" + txthCompId.Text + "','" + txtCompName.Text + "','" + txtSubName.Text + "','" + txtAddress.Text + "','" + txtAddress2.Text + "','" + txtCity.Text + "'," + txtPhone.Text + ",'" + txtMobile.Text + "','" + txtEmail.Text + "','" + txtWebsite.Text + "','" + txtCSTNo.Text + "','" + txtPANNo.Text + "','" + txtVATNo.Text + "'," + txtDL1.Text + "," + txtDL2.Text + ",'" + txtDealsIn.Text + "','" + txtStockist.Text + "','" + txtPath.Text + "','" + Convert.ToDateTime(dateTimePicker1.Text).ToString(dateformate) + "','" + Convert.ToDateTime(dateTimePicker2.Text).ToString(dateformate) + "',1)");
                      //  ods.execute("INSERT INTO [Company]([CompanyID],[CompanyName],[SubName],[Address],[Address2],[City],[Phone],[Mobile],[Email],[Website],[CSTNo],[PANNo],[VATNo],[DLNo1],[DLNo2],[DealsIn],[Stockist],[Path],[StartDate],[EndDate],[isActive])VALUES('" + txthCompId.Text + "','" + txtCompName.Text + "','" + txtSubName.Text + "','" + txtAddress.Text + "','" + txtAddress2.Text + "','" + txtCity.Text + "','" + txtPhone.Text + "','" + txtMobile.Text + "','" + txtEmail.Text + "','" + txtWebsite.Text + "','" + txtCSTNo.Text + "','" + txtPANNo.Text + "','" + txtVATNo.Text + "','" + txtDL1.Text + "','" + txtDL2.Text + "','" + txtDealsIn.Text + "','" + txtStockist.Text + "','" + txtPath.Text + "','" + Convert.ToDateTime(dateTimePicker1.Text).ToString(dateformate) + "','" + Convert.ToDateTime(dateTimePicker2.Text).ToString(dateformate) + "',1)");
                        clearAll();
                        MessageBox.Show("Data Inserted Successfully.");
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
                MessageBox.Show("Error:" + ex.Message);
            }
            finally
            {
                
            }
        }

        private void clearAll()
        {
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
            txtPath.Text = "";
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
                    txtPath.Text = path;
                    txtPath.Focus();
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

        private void txtcurrency_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //dateTimePicker1.Focus();
                txtAddress.Focus();
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

        private void txtPath_Enter(object sender, EventArgs e)
        {
            txtPath.BackColor = Color.LightYellow;
        }

        private void txtPath_Leave(object sender, EventArgs e)
        {
            txtPath.BackColor = Color.White;
        }

        private void txthCompId_Enter(object sender, EventArgs e)
        {
            txthCompId.BackColor = Color.LightYellow;
        }

        private void txthCompId_Leave(object sender, EventArgs e)
        {
            txthCompId.BackColor = Color.White;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtcurrency_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtStockist_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCSTNo_TextChanged(object sender, EventArgs e)
        {

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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
        public static string[] photo1 = new string[2];
        private string[] UploadPhoto(int no)
        {
            var resultsToReturn = new string[2];
            #region
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select a Image";
            ofd.Filter = "JPG files (*.jpg)|*.jpg|JPEG files (*.jpeg*)|*.jpeg*";
            // ofd.Filter = "jpg files (*.jpg)|*.jpg|All files (*.*)|*.*";
            string appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Images\CompanyLogo\";
            if (Directory.Exists(appPath) == false)
            {
                Directory.CreateDirectory(appPath);
            }
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string iName = txtCompName.Text.Trim().Replace(" ", "").Replace("'", "''") + "-Logo-" + no + "-" + DateTime.Now.ToString("ssMMyyHHmmdd") + "" + ofd.SafeFileName;
                    string filepath = ofd.FileName;
                    try
                    {
                        File.Delete(appPath + iName);
                    }
                    catch
                    {
                    }
                    File.Copy(filepath, appPath + iName);
                    //pic1.Image = new Bitmap(ofd.OpenFile());
                    //saveimage(pictureBox1.Image, appPath);
                    resultsToReturn[0] = appPath;
                    resultsToReturn[1] = iName;
                    return resultsToReturn;
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Unable to open file " + exp.Message);
                    resultsToReturn[0] = "Error";
                    resultsToReturn[1] = "Error";
                    return resultsToReturn;
                }
            }
            else
            {

                ofd.Dispose();
                resultsToReturn[0] = "Error";
                resultsToReturn[1] = "Error";
                return resultsToReturn;
            }

            #endregion
        }
        private void btn1uphoto_Click(object sender, EventArgs e)
        {
            try
            {
                photo1 = UploadPhoto(1);
                if (photo1[0] == "Error")
                {
                    MessageBox.Show("Something Wen't Wrong !!");
                }
                else
                {
                    pic1.Image = Image.FromFile(photo1[0] + photo1[1]);
                    //pic1.ImageLocation=photo1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pic1_Click(object sender, EventArgs e)
        {

        }

        

        
       























       



















    }

}
