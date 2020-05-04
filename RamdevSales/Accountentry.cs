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
    public partial class Accountentry : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        public string constr = ConfigurationManager.ConnectionStrings["qry"].ToString();
        AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
        private string id;
        // private Sale sale;
        // private DefaultPurchase purchase;
        int cl = 0, flg, flgq;
        private ClientRegistration clientregistration;
        Connection cn = new Connection();
        int a = 0;
        int q = 0;
        public Accountentry()
        {
            InitializeComponent();
            bindgroup();
        }

        //public Accountentry(Sale sale)
        //{
        //    InitializeComponent();
        //    // TODO: Complete member initialization
        //    this.sale = sale;
        //    bindgroup();
        //}

        //public Accountentry(DefaultPurchase purchase)
        //{
        //    InitializeComponent();
        //    // TODO: Complete member initialization
        //    this.purchase = purchase;
        //    bindgroup();
        //}

        public Accountentry(QReceipt qReceipt)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this.qReceipt = qReceipt;
            flg = 1;
            bindgroup();

        }

        public Accountentry(QPayment qPayment)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this.qPayment = qPayment;
            flg = 1;
            bindgroup();

        }

        //public Accountentry(SaleReturn saleReturn)
        //{
        //    InitializeComponent();
        //    // TODO: Complete member initialization
        //    this.saleReturn = saleReturn;
        //    bindgroup();
        //}

        //public Accountentry(Purchase purchase_2)
        //{
        //    InitializeComponent();
        //    // TODO: Complete member initialization
        //    this.purchase_2 = purchase_2;
        //    bindgroup();
        //    // TODO: Complete member initialization

        //}

        public Accountentry(DefaultSale defaultSale)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this.defaultSale = defaultSale;
            bindgroup();
        }

        public Accountentry(TabControl tabControl)
        {
            InitializeComponent();
            bindgroup();
            this.tabControl = tabControl;
        }

        public Accountentry(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            bindgroup();
            bindcustype();
            this.master = master;
            this.tabControl = tabControl;
            //txtaccname.Focus();
        }

        public Accountentry(DefaultSale defaultSale, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            bindgroup();
            bindcustype();
            this.defaultSale = defaultSale;
            this.tabControl = tabControl;
        }

        public Accountentry(DefaultSale defaultSale, Master master, TabControl tabControl, string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.defaultSale = defaultSale;
            this.master = master;
            this.tabControl = tabControl;
            bindgroup();
            bindcustype();
            pvc = activecontroal;
        }

        //public Accountentry(SaleReturn saleReturn, Master master, TabControl tabControl)
        //{
        //    // TODO: Complete member initialization
        //    InitializeComponent();
        //    this.saleReturn = saleReturn;
        //    this.master = master;
        //    this.tabControl = tabControl;
        //    bindgroup();
        //}

        //public Accountentry(DefaultPurchase defaultPurchase, Master master, TabControl tabControl)
        //{
        //    // TODO: Complete member initialization
        //    InitializeComponent();
        //    this.defaultPurchase = defaultPurchase;
        //    this.master = master;
        //    this.tabControl = tabControl;
        //    bindgroup();
        //}

        //public Accountentry(Purchase purchase_2, Master master, TabControl tabControl)
        //{
        //    // TODO: Complete member initialization
        //    InitializeComponent();

        //    this.purchase_2 = purchase_2;
        //    this.master = master;
        //    this.tabControl = tabControl;
        //    bindgroup();
        //    flg = 1;
        //}





        //public Accountentry(Sale sale, Master master, TabControl tabControl)
        //{
        //    // TODO: Complete member initialization
        //    InitializeComponent();
        //    bindgroup();
        //    this.sale = sale;
        //    this.master = master;
        //    this.tabControl = tabControl;
        //    txtaccname.Focus();
        //}

        public Accountentry(DefaultSaleOrder defaultSaleOrder, Master master, TabControl tabControl, string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            bindgroup();
            bindcustype();
            this.defaultSaleOrder = defaultSaleOrder;
            this.master = master;
            this.tabControl = tabControl;
            pvc = activecontroal;
        }

        public Accountentry(QReceipt qReceipt, Master master, TabControl tabControl, string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.qReceipt = qReceipt;
            this.master = master;
            this.tabControl = tabControl;
            bindgroup();
            bindcustype();
            flg = 1;
            a = 1;
            this.ActiveControl = txtaccname;
            pvc = activecontroal;
        }
        public static string pvc;
        public Accountentry(QPayment qPayment, Master master, TabControl tabControl, string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.qPayment = qPayment;
            this.master = master;
            this.tabControl = tabControl;
            bindgroup();
            bindcustype();
            flgq = 1;
            q = 1;
            this.ActiveControl = txtaccname;
            pvc = activecontroal;
        }

        public Accountentry(BankEntry bankEntry, Master master, TabControl tabControl, string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.bankEntry = bankEntry;
            this.master = master;
            this.tabControl = tabControl;
            bindgroup();
            bindcustype();
            pvc = activecontroal;
            flg = 1;
        }

        public Accountentry(DebitandCreditNote debitandCreditNote, Master master, TabControl tabControl, string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.debitandCreditNote = debitandCreditNote;
            this.master = master;
            this.tabControl = tabControl;
            bindgroup();
            bindcustype();
            pvc = activecontroal;
            flg = 1;
        }

        public Accountentry(DefaultPOS defaultPOS, Master master, TabControl tabControl, string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.defaultPOS = defaultPOS;
            this.master = master;
            this.tabControl = tabControl;
            bindgroup();
            bindcustype();
            pvc = activecontroal;
            flg = 1;
        }

        public Accountentry(AgentCommissionReport agentCommissionReport, Master master, TabControl tabControl, string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.agentCommissionReport = agentCommissionReport;
            this.master = master;
            this.tabControl = tabControl;
            bindgroup();
            bindcustype();
            pvc = activecontroal;
            flg = 1;
        }

        public Accountentry(frmComplainMasterData frmComplainMasterData, Master master, TabControl tabControl, string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            bindgroup();
            bindcustype();
            this.frmComplainMasterData = frmComplainMasterData;
            this.master = master;
            this.tabControl = tabControl;
            pvc = activecontroal;
            flg = 1;
        }

        public Accountentry(frmSentToCompany frmSentToCompany, Master master, TabControl tabControl, string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            bindgroup();
            bindcustype();
            this.frmSentToCompany = frmSentToCompany;
            this.master = master;
            this.tabControl = tabControl;
            pvc = activecontroal;
            flg = 1;
        }

        public Accountentry(SalePurchaseOrderSimpleformate salePurchaseOrderSimpleformate, Master master, TabControl tabControl, string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            bindgroup();
            bindcustype();
            this.salePurchaseOrderSimpleformate = salePurchaseOrderSimpleformate;
            this.master = master;
            this.tabControl = tabControl;
            pvc = activecontroal;
        }

        public Accountentry(GSTVouchers gSTVouchers, Master master, TabControl tabControl, string activecontroal_2)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            bindgroup();
            bindcustype();
            this.gSTVouchers = gSTVouchers;
            this.master = master;
            this.tabControl = tabControl;
            this.activecontroal_2 = activecontroal_2;
        }
        public void bindgroup()
        {

            //DataTable dt = new DataTable();
            //dt = cn.getdataset("select * from accountgroup order by groupname asc");
            //txtgrop.Refresh();
            //txtgrop.ValueMember = "id";
            //txtgrop.DisplayMember = "groupname";
            //txtgrop.DataSource = dt;
            //txtgrop.SelectedIndex = -1;

            SqlCommand cmd = new SqlCommand("select id,groupname from accountgroup order by groupname asc", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt11 = new DataTable();
            sda.Fill(dt11);

            txtgrop.ValueMember = "id";
            txtgrop.DisplayMember = "groupname";
            txtgrop.DataSource = dt11;
            txtgrop.SelectedIndex = -1;
            // autobind(dt, cmbsaletype);

        }
        public void bindcustype()
        {

            SqlCommand cmd = new SqlCommand("select id,customertype from AccountCustomerType where isactive=1 order by customertype asc", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt11 = new DataTable();
            sda.Fill(dt11);

            cmbcustype.ValueMember = "id";
            cmbcustype.DisplayMember = "customertype";
            cmbcustype.DataSource = dt11;
            cmbcustype.SelectedIndex = -1;

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    if (string.IsNullOrEmpty(pvc) == true)
                    {
                        master.RemoveCurrentTab();
                    }
                    else
                    {
                        master.RemoveCurrentTab1(pvc, txtaccname.Text);
                    }
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
        public static string activecontroal;
        public void getaccountno()
        {
            try
            {
                DataTable options = cn.getdataset("select * from options");
                if (options.Rows[0]["accountbillno"].ToString() == "Continuous")
                {
                    string str = cn.ExecuteScalar("select max(ClientID) from ClientMaster where isactive=1");
                    Int64 id, count;
                    if (str == "")
                    {

                        id = Convert.ToInt64(1);
                        count = Convert.ToInt64(1);
                    }
                    else
                    {
                        id = Convert.ToInt64(str) + 1;
                        count = Convert.ToInt64(str) + 1;
                    }
                    txtaccountnumber.Text = options.Rows[0]["accountprefix"].ToString() + count.ToString();
                    txtaccountnumber.ReadOnly = true;
                }
                else
                {
                    txtaccountnumber.Text = "";
                    txtaccountnumber.ReadOnly = false;
                    txtaccountnumber.Focus();
                    this.ActiveControl = txtaccountnumber;
                }
            }
            catch
            {
            }
        }
        int cnt = 0;
        private void Accountentry_Load(object sender, EventArgs e)
        {
            try
            {
                if (cnt == 0)
                {
                    userrights = cn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[11]["d"].ToString() == "False")
                        {
                            btndelete.Enabled = false;
                        }
                    }
                    this.StartPosition = FormStartPosition.Manual;
                    this.Location = new Point(0, 0);
                    autoextendercity();
                    autoextenderstate();
                    getaccountno();

                    //this.ActiveControl = txtaccname;
                    //panel2.Controls.Add(txtaccname);
                    //txtaccname.Focus();
                    //txtaccname.Select();
                }
                //set the interval  and start the timer
                //                timer1.Interval = 1000;
                //              timer1.Start();

            }
            catch
            {
            }
        }
        public void clear()
        {
            txtaddress.Text = "";
            txtprintname.Text = "";
            txtgrop.SelectedIndex = 0;
            txtopbal.Text = "";
            txtcrdr.SelectedIndex = 0;
            txtstatecode.Text = "";
            txtstate.Text = "";
            txtcity.Text = "";
            txtmobile.Text = "";
            txtemail.Text = "";
            txtcst.Text = "";
            txtaccname.Text = "";
            txtphone.Text = "";
            txtvat.Text = "";
            txtadhar.Text = "";
            txtgst.Text = "";
            txtcrdr.SelectedIndex = -1;
            txtgrop.SelectedIndex = -1;
            cmbcustype.SelectedIndex = -1;
            txtaccountnumber.Text = "";
            txtnoteorremark.Text = "";
            txtcreditdaypurchase.Text = "";
            txtcreditdaysale.Text = "";
            txtcreditlemite.Text = "";
            txtbilllimite.Text = "";
            txtDeliveryAt.Text = "";
            getaccountno();

        }
        DataTable userrights = new DataTable();
        public void submit()
        {
            try
            {
                if (txtaccname.Text != "")
                {
                    this.Enabled = false;
                    con.Open();
                    if (btnsave.Text == "Update")
                    {
                        if (userrights.Rows.Count > 0)
                        {
                            if (userrights.Rows[11]["u"].ToString() == "True")
                            {
                                bool concat = txtaddress.Text.Contains("'");
                                string address = string.Empty;
                                if (concat)
                                {
                                    char[] textArray = txtaddress.Text.ToCharArray();
                                    for (int i = 0; i < txtaddress.Text.Length; i++)
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
                                    address = txtaddress.Text;
                                }
                                SqlCommand cmd = new SqlCommand("update ClientMaster set AccountName ='" + txtaccname.Text + "',statecode='" + txtstatecode.Text + "',GstNo='" + txtgst.Text + "',AdharNo='" + txtadhar.Text + "', PrintName='" + txtprintname.Text + "',GroupName='" + txtgrop.Text + "',Opbal='" + txtopbal.Text + "',Dr_cr='" + txtcrdr.Text + "',Address='" + address + "',City='" + txtcity.Text + "',State='" + txtstate.Text + "',Phone='" + txtphone.Text + "',Mobile='" + txtmobile.Text + "',Email='" + txtemail.Text + "',Cstno='" + txtcst.Text + "',Vatno='" + txtvat.Text + "',crelimite='" + txtcreditlemite.Text + "',billlimite='" + txtbilllimite.Text + "',credaysale='" + txtcreditdaysale.Text + "',credaypurchase='" + txtcreditdaypurchase.Text + "',accountnumber='" + txtaccountnumber.Text + "',customertypeid='" + cmbcustype.SelectedValue + "',customertype='" + cmbcustype.Text + "',noteorremarks='" + txtnoteorremark.Text + "',groupid='" + txtgrop.SelectedValue + "',isactive=1,ismaintain=0,Deliveryat='"+ txtDeliveryAt.Text +"' where clientID='" + id + "'", con);
                                cmd.ExecuteNonQuery();

                                // ClientRegistration dlg = new ClientRegistration(master, tabControl);
                                //dlg.MdiParent = this.MdiParent;
                                //dlg.StartPosition = FormStartPosition.CenterScreen;
                                //this.Hide();
                                //dlg.Show();
                                // master.AddNewTab(dlg);
                                //  listviewbind();
                                MessageBox.Show("Update Successfully");
                                if (gener == 1)
                                {
                                    try
                                    {

                                        //  sale.bindcustomer();

                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        // saleReturn.bindcustomer();
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        // purchase.bindcustomer();
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        // purchase_2.bindcustomer();
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        defaultSale.bindcustomer();
                                        defaultSale.binaagent();
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        defaultSaleOrder.bindcustomer();
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        salePurchaseOrderSimpleformate.bindcustomer();
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        bankEntry.bindbank();
                                        bankEntry.bindcustomer();
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        debitandCreditNote.bindcustomer();
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        defaultPOS.binaagent();
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        agentCommissionReport.binaagent();
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        frmComplainMasterData.bindcustomer();

                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        frmSentToCompany.bindcustomer();

                                    }
                                    catch
                                    {
                                    }
                                    if (string.IsNullOrEmpty(pvc) == true)
                                    {
                                        master.RemoveCurrentTab();
                                    }
                                    else
                                    {
                                        master.RemoveCurrentTab1(pvc, txtaccname.Text);
                                    }
                                }
                                else
                                {
                                    this.ActiveControl = txtaccname;
                                }
                                if (a == 1)
                                {

                                    qReceipt.bindcustomer();
                                    if (string.IsNullOrEmpty(pvc) == true)
                                    {
                                        master.RemoveCurrentTab();
                                    }
                                    else
                                    {
                                        master.RemoveCurrentTab1(pvc, txtaccname.Text);
                                    }

                                }
                                else
                                {
                                    this.ActiveControl = txtaccountnumber;
                                }
                                if (q == 1)
                                {
                                    qPayment.bindcustomer();
                                    if (string.IsNullOrEmpty(pvc) == true)
                                    {
                                        master.RemoveCurrentTab();
                                    }
                                    else
                                    {
                                        master.RemoveCurrentTab1(pvc, txtaccname.Text);
                                    }
                                }
                                else
                                {
                                    this.ActiveControl = txtaccname;
                                }
                                gener = 0;

                                clear();
                                btnsave.Text = "Submit";

                                //  master.RemoveCurrentTab();
                            }
                            else
                            {
                                MessageBox.Show("You don't have Permission To Update");
                                return;
                            }
                        }

                    }
                    else
                    {
                        if (userrights.Rows.Count > 0)
                        {
                            if (userrights.Rows[11]["a"].ToString() == "True")
                            {
                                bool concat = txtaddress.Text.Contains("'");
                                string address = string.Empty;
                                if (concat)
                                {
                                    char[] textArray = txtaddress.Text.ToCharArray();
                                    for (int i = 0; i < txtaddress.Text.Length; i++)
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
                                    address = txtaddress.Text;
                                }
                                string sql = "INSERT INTO [dbo].[ClientMaster]([AccountName],[PrintName],[GroupName],[Opbal],[Dr_cr],[Address],[City],[State],[Phone],[Mobile],[Email],[Cstno],[Vatno],[GstNo],[AdharNo],[GroupID],[statecode],[crelimite],[billlimite],[credaysale],[credaypurchase],[accountnumber],[customertypeid],[customertype],[noteorremarks],[isactive],[ismaintain],[Deliveryat])VALUES('" + txtaccname.Text + "','" + txtprintname.Text + "','" + txtgrop.Text + "','" + txtopbal.Text + "','" + txtcrdr.Text + "','" + address + "','" + txtcity.Text + "','" + txtstate.Text + "','" + txtphone.Text + "','" + txtmobile.Text + "','" + txtemail.Text + "','" + txtcst.Text + "','" + txtvat.Text + "','" + txtgst.Text + "','" + txtadhar.Text + "','" + txtgrop.SelectedValue + "','" + txtstatecode.Text + "','" + txtcreditlemite.Text + "','" + txtbilllimite.Text + "','" + txtcreditdaysale.Text + "','" + txtcreditdaypurchase.Text + "','" + txtaccountnumber.Text + "','" + cmbcustype.SelectedValue + "','" + cmbcustype.Text + "','" + txtnoteorremark.Text + "',1,0,'"+txtDeliveryAt.Text+"')";
                                SqlCommand cmd = new SqlCommand(sql, con);
                                cmd.ExecuteNonQuery();
                                clear();

                                con.Close();
                                // listviewbind();
                                MessageBox.Show("Insert Data Successfully...");
                                if (gener == 1)
                                {
                                    try
                                    {

                                        //   sale.bindcustomer();
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        // saleReturn.bindcustomer();
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        // purchase.bindcustomer();
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        // purchase_2.bindcustomer();
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        defaultSale.bindcustomer();
                                        defaultSale.binaagent();

                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        defaultSaleOrder.bindcustomer();
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        salePurchaseOrderSimpleformate.bindcustomer();
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        bankEntry.bindbank();
                                        bankEntry.bindcustomer();
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        debitandCreditNote.bindcustomer();
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        defaultPOS.binaagent();
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        agentCommissionReport.binaagent();
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        frmComplainMasterData.bindcustomer();

                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        frmSentToCompany.bindcustomer();

                                    }
                                    catch
                                    {
                                    }
                                    gener = 0;
                                    if (string.IsNullOrEmpty(pvc) == true)
                                    {
                                        master.RemoveCurrentTab();
                                    }
                                    else
                                    {
                                        master.RemoveCurrentTab1(pvc, txtaccname.Text);
                                    }
                                }
                                else
                                {
                                    this.ActiveControl = txtaccname;
                                    //  master.RemoveCurrentTab();
                                    //ClientRegistration dlg = new ClientRegistration(master, tabControl);
                                    //dlg.MdiParent = this.MdiParent;
                                    //dlg.StartPosition = FormStartPosition.CenterScreen;
                                    //this.Hide();
                                    //dlg.Show();
                                    //  master.AddNewTab(dlg);
                                }
                            }
                            else
                            {
                                MessageBox.Show("You don't have Permission To Submit");
                                return;
                            }
                        }
                    }
                    if (flg == 1)
                    {
                        // qreceipt = new QReceipt();
                        //  qreceipt.bindcustomer();
                        // qpayment = new QPayment();
                        // qpayment.bindcustomer();
                        qReceipt.bindcustomer();
                        if (string.IsNullOrEmpty(pvc) == true)
                        {
                            master.RemoveCurrentTab();
                        }
                        else
                        {
                            master.RemoveCurrentTab1(pvc, txtaccname.Text);
                        }


                        // tabControl.TabPages.Remove(tabControl.SelectedTab);
                    }
                    else
                    {
                        this.ActiveControl = txtaccname;
                    }
                    if (flgq == 1)
                    {
                        qPayment.bindcustomer();
                        if (string.IsNullOrEmpty(pvc) == true)
                        {
                            master.RemoveCurrentTab();
                        }
                        else
                        {
                            master.RemoveCurrentTab1(pvc, txtaccname.Text);
                        }

                    }
                    else
                    {
                        this.ActiveControl = txtaccname;
                    }

                }
                else
                {
                    MessageBox.Show("Account Name cannot be Blank");
                    this.ActiveControl = txtaccname;
                    return;
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Error:" + ex.Message);
            }
            finally
            {
                this.Enabled = true;
                con.Close();
            }
        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtaccountnumber.Text))
            {
                submit();
            }
            else
            {
                MessageBox.Show("Enter Account Number");
                this.ActiveControl = txtaccountnumber;
                txtaccountnumber.Focus();
            }
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(pvc) == true)
                {
                    master.RemoveCurrentTab();
                }
                else
                {
                    master.RemoveCurrentTab1(pvc, txtaccname.Text);
                }
            }

        }

        internal void Update(int p, string iid)
        {
            try
            {
                if (iid != "")
                {
                    userrights = cn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[11]["d"].ToString() == "False")
                        {
                            btndelete.Enabled = false;
                        }
                    }
                    cnt = 1;
                    this.StartPosition = FormStartPosition.Manual;
                    this.Location = new Point(0, 0);
                    autoextendercity();
                    autoextenderstate();
                    //this.ActiveControl = txtaccname;
                    //panel2.Controls.Add(txtaccname);
                    //txtaccname.Focus();
                    //txtaccname.Select();
                    id = iid;
                    SqlCommand cmd = new SqlCommand("select * from ClientMaster where isactive=1 and clientID='" + id + "'", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    txtaccname.Text = dt.Rows[0]["AccountName"].ToString();
                    txtprintname.Text = dt.Rows[0]["PrintName"].ToString();
                    txtgrop.Text = dt.Rows[0]["GroupName"].ToString();
                    txtopbal.Text = dt.Rows[0]["Opbal"].ToString();
                    txtcrdr.SelectedItem = dt.Rows[0]["Dr_cr"].ToString();
                    txtaddress.Text = dt.Rows[0]["Address"].ToString();
                    txtcity.Text = dt.Rows[0]["City"].ToString();
                    txtstate.Text = dt.Rows[0]["State"].ToString();
                    txtphone.Text = dt.Rows[0]["Phone"].ToString();
                    txtmobile.Text = dt.Rows[0]["Mobile"].ToString();
                    txtemail.Text = dt.Rows[0]["Email"].ToString();
                    txtcst.Text = dt.Rows[0]["Cstno"].ToString();
                    txtvat.Text = dt.Rows[0]["VATNo"].ToString();
                    txtadhar.Text = dt.Rows[0]["AdharNo"].ToString();
                    txtgst.Text = dt.Rows[0]["GstNo"].ToString();
                    txtstatecode.Text = dt.Rows[0]["statecode"].ToString();
                    txtcreditlemite.Text = dt.Rows[0]["crelimite"].ToString();
                    txtbilllimite.Text = dt.Rows[0]["billlimite"].ToString();
                    txtcreditdaysale.Text = dt.Rows[0]["credaysale"].ToString();
                    txtcreditdaypurchase.Text = dt.Rows[0]["credaypurchase"].ToString();
                    txtaccountnumber.Text = dt.Rows[0]["accountnumber"].ToString();
                    cmbcustype.Text = dt.Rows[0]["customertype"].ToString();
                    txtnoteorremark.Text = dt.Rows[0]["noteorremarks"].ToString();
                    txtDeliveryAt.Text = dt.Rows[0]["Deliveryat"].ToString();
                    DataTable options = cn.getdataset("select * from options");
                    if (options.Rows[0]["accountbillno"].ToString() == "Continuous")
                    {
                        txtaccountnumber.ReadOnly = true;
                        this.ActiveControl = txtaccname;
                    }
                    else
                    {
                        txtaccountnumber.ReadOnly = false;
                        this.ActiveControl = txtaccountnumber;
                    }
                    btnsave.Text = "Update";

                }
            }
            catch
            {
            }
        }
        int gener = 0;
        private int p;
        private ClientRegistration clientRegistration;
        private QReceipt qReceipt;
        //  private SaleReturn saleReturn;
        //   private Purchase purchase_2;
        private DefaultSale defaultSale;
        private TabControl tabControl;
        private Master master;
        //  private DefaultPurchase defaultPurchase;
        private DefaultSaleOrder defaultSaleOrder;
        internal void Passed(int p)
        {
            gener = p;
        }
        public void autoextenderstate()
        {
            SqlDataReader dReader;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = constr;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            // (dgvmst_subpat.DataSource as DataTable).DefaultView.RowFilter = string.Format("p_name like '%{0}%' ", txt_name.Text);
            cmd.CommandText = "select * from ClientMaster where isactive=1 order by AccountName";
            conn.Open();
            dReader = cmd.ExecuteReader();
            if (dReader.HasRows == true)
            {
                while (dReader.Read())
                    namesCollection.Add(dReader["State"].ToString());

            }
            else
            {
                // MessageBox.Show("Data not found");
            }
            dReader.Close();

            txtstate.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtstate.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtstate.AutoCompleteCustomSource = namesCollection;
        }
        public void autoextendercity()
        {
            SqlDataReader dReader;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = constr;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            // (dgvmst_subpat.DataSource as DataTable).DefaultView.RowFilter = string.Format("p_name like '%{0}%' ", txt_name.Text);
            cmd.CommandText = "select * from ClientMaster where isactive=1 order by AccountName";
            conn.Open();
            dReader = cmd.ExecuteReader();
            if (dReader.HasRows == true)
            {
                while (dReader.Read())
                    namesCollection.Add(dReader["City"].ToString());

            }
            else
            {
                // MessageBox.Show("Data not found");
            }
            dReader.Close();

            txtcity.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtcity.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtcity.AutoCompleteCustomSource = namesCollection;
        }
        private void txtaccname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int flag = 0;
                string str = txtaccname.Text.ToUpper().Trim();
                SqlCommand cmd = new SqlCommand("select AccountName from ClientMaster where isactive=1", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (txtaccname.Text != "")
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string val = dt.Rows[i][0].ToString().ToUpper().Trim();

                            if (val == str)
                            {
                                if (btnsave.Text != "Update")
                                {
                                    MessageBox.Show("Item Already Available Please add Another");
                                    txtaccname.Focus();
                                    flag = 1;
                                    break;
                                }

                            }

                        }
                    }
                    else
                    {
                        txtprintname.Text = txtaccname.Text;
                        txtprintname.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Item Name cannot be Blank");
                    txtaccname.Show();
                    return;
                }
                if (flag == 0)
                {
                    txtprintname.Text = txtaccname.Text;
                    txtprintname.Focus();
                }



            }
        }

        private void txtprintname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtgrop.Focus();
            }
        }
        public static string s;
        private void txtgrop_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < txtgrop.Items.Count; i++)
                {
                    s = txtgrop.GetItemText(txtgrop.Items[i]);
                    if (s == txtgrop.Text)
                    {
                        inList = true;
                        txtgrop.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    txtgrop.Text = "";
                }
                txtopbal.Text = "0";
                cmbcustype.Focus();
            }
            if (e.KeyCode == Keys.F3)
            {
                var privouscontroal = txtgrop;
                activecontroal = privouscontroal.Name;
                Group popup = new Group(this, tabControl, master, activecontroal);
                master.AddNewTab(popup);

            }

            if (e.KeyCode == Keys.F2)
            {
                var privouscontroal = txtgrop;
                activecontroal = privouscontroal.Name;
                Group dlg = new Group(this, tabControl, master, activecontroal);

                dlg.updatemode(1, txtgrop.Text);
                master.AddNewTab(dlg);
                //  dlg.Show();
            }
        }

        private void txtopbal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtcrdr.Focus();
            }
        }

        private void txtcity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtstate.Focus();
            }
        }

        private void txtstate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //txtphone.Focus();
                txtstatecode.Focus();
            }
        }

        private void txtphone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtmobile.Focus();
            }
        }

        private void txtmobile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtemail.Focus();
            }
        }

        private void txtemail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtcst.Focus();
            }
        }

        private void txtcst_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtvat.Focus();
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (id == "" || id == null)
                {
                    MessageBox.Show("Select Account Name");
                }
                else
                {
                    cn.execute("Update clientmaster set isactive=0 where clientid='" + id + "'");
                    MessageBox.Show("Delete successfully");
                    clear();
                }
            }
            catch
            {
            }
        }



        private void txtvat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtgst.Focus();
            }
        }

        private void txtgst_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtadhar.Focus();
            }
        }

        private void txtadhar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // txtstatecode.Focus();
                txtcreditlemite.Focus();
            }
        }

        private void txtgrop_Enter(object sender, EventArgs e)
        {
            try
            {
                txtgrop.SelectedIndex = 0;
                txtgrop.DroppedDown = true;
            }
            catch
            {
            }
        }

        private void txtcrdr_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true; // This will eliminate the beeping
                    bool inList = false;
                    for (int i = 0; i < txtcrdr.Items.Count; i++)
                    {
                        s = txtcrdr.GetItemText(txtcrdr.Items[i]);
                        if (s == txtcrdr.Text)
                        {
                            inList = true;
                            txtcrdr.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        txtcrdr.Text = "";
                    }
                    txtaddress.Focus();
                }
            }
            catch
            {
            }
        }

        private void txtphone_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtmobile_KeyPress(object sender, KeyPressEventArgs e)
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


        private void txtemail_Validating(object sender, CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex rEmail = new System.Text.RegularExpressions.Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            if (txtemail.Text.Length > 0)
            {
                if (!rEmail.IsMatch(txtemail.Text))
                {
                    MessageBox.Show("Please provide valid email address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtemail.Text = "";
                    txtemail.Focus();
                }
            }
        }

        private void txtaccname_Enter(object sender, EventArgs e)
        {
            txtaccname.BackColor = Color.LightYellow;
        }

        private void txtprintname_Enter(object sender, EventArgs e)
        {
            txtprintname.BackColor = Color.LightYellow;
        }

        private void txtopbal_Enter(object sender, EventArgs e)
        {
            txtopbal.BackColor = Color.LightYellow;
        }

        private void txtaccname_Leave(object sender, EventArgs e)
        {
            txtaccname.BackColor = Color.White;
        }

        private void txtprintname_Leave(object sender, EventArgs e)
        {
            txtprintname.BackColor = Color.White;
        }

        private void txtopbal_Leave(object sender, EventArgs e)
        {
            txtopbal.BackColor = Color.White;
        }

        private void txtaddress_Enter(object sender, EventArgs e)
        {
            txtaddress.BackColor = Color.LightYellow;
        }

        private void txtaddress_Leave(object sender, EventArgs e)
        {
            txtaddress.BackColor = Color.White;
        }

        private void txtcity_Enter(object sender, EventArgs e)
        {
            txtcity.BackColor = Color.LightYellow;
        }

        private void txtcity_Leave(object sender, EventArgs e)
        {
            txtcity.BackColor = Color.White;
        }

        private void txtstate_Enter(object sender, EventArgs e)
        {
            txtstate.BackColor = Color.LightYellow;
        }

        private void txtstate_Leave(object sender, EventArgs e)
        {
            txtstate.BackColor = Color.White;
        }

        private void txtphone_Enter(object sender, EventArgs e)
        {
            txtphone.BackColor = Color.LightYellow;
        }

        private void txtphone_Leave(object sender, EventArgs e)
        {
            txtphone.BackColor = Color.White;
        }

        private void txtmobile_Enter(object sender, EventArgs e)
        {
            txtmobile.BackColor = Color.LightYellow;
        }

        private void txtmobile_Leave(object sender, EventArgs e)
        {
            txtmobile.BackColor = Color.White;
        }

        private void txtemail_Enter(object sender, EventArgs e)
        {
            txtemail.BackColor = Color.LightYellow;
        }

        private void txtemail_Leave(object sender, EventArgs e)
        {
            txtemail.BackColor = Color.White;
        }

        private void txtcst_Enter(object sender, EventArgs e)
        {
            txtcst.BackColor = Color.LightYellow;
        }

        private void txtcst_Leave(object sender, EventArgs e)
        {
            txtcst.BackColor = Color.White;
        }

        private void txtvat_Enter(object sender, EventArgs e)
        {
            txtvat.BackColor = Color.LightYellow;
        }

        private void txtvat_Leave(object sender, EventArgs e)
        {
            txtvat.BackColor = Color.White;
        }

        private void txtgst_Enter(object sender, EventArgs e)
        {
            txtgst.BackColor = Color.LightYellow;
        }

        private void txtgst_Leave(object sender, EventArgs e)
        {
            txtgst.BackColor = Color.White;
        }

        private void txtadhar_Enter(object sender, EventArgs e)
        {
            txtadhar.BackColor = Color.LightYellow;
        }

        private void txtadhar_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtadhar_Leave(object sender, EventArgs e)
        {
            txtadhar.BackColor = Color.White;
        }

        private void txtstatecode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //  txtstatecode.Focus();
                //btnsave.Focus();
                txtphone.Focus();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtstatecode_Enter(object sender, EventArgs e)
        {
            txtstatecode.BackColor = Color.LightYellow;
        }

        private void txtstatecode_Leave(object sender, EventArgs e)
        {
            txtstatecode.BackColor = Color.White;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void txtemail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtadhar_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtaddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtcity.Focus();
            }
        }

        private void txtcst_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnsave_MouseHover(object sender, EventArgs e)
        {
            //btnsave.UseVisualStyleBackColor = false;
            //btnsave.BackColor = Color.YellowGreen;
            //btnsave.ForeColor = Color.White;
        }

        private void btnsave_MouseLeave(object sender, EventArgs e)
        {
            btnsave.UseVisualStyleBackColor = true;
            btnsave.BackColor = Color.FromArgb(51, 153, 255);
            btnsave.ForeColor = Color.White;
        }

        private void btnreset_MouseHover(object sender, EventArgs e)
        {

        }

        private void btnreset_MouseLeave(object sender, EventArgs e)
        {
            btnreset.UseVisualStyleBackColor = true;
            btnreset.BackColor = Color.FromArgb(51, 153, 255);
            btnreset.ForeColor = Color.White;
        }

        private void btndelete_MouseHover(object sender, EventArgs e)
        {

        }

        private void btndelete_MouseLeave(object sender, EventArgs e)
        {
            btndelete.UseVisualStyleBackColor = true;
            btndelete.BackColor = Color.FromArgb(51, 153, 255);
            btndelete.ForeColor = Color.White;
        }

        private void btncancel_MouseHover(object sender, EventArgs e)
        {

        }

        private void btncancel_MouseLeave(object sender, EventArgs e)
        {
            btncancel.UseVisualStyleBackColor = true;
            btncancel.BackColor = Color.FromArgb(51, 153, 255);
            btncancel.ForeColor = Color.White;
        }

        private void btnsave_MouseEnter(object sender, EventArgs e)
        {
            btnsave.UseVisualStyleBackColor = false;
            btnsave.BackColor = Color.YellowGreen;
            btnsave.ForeColor = Color.White;
        }

        private void btnreset_MouseEnter(object sender, EventArgs e)
        {
            btnreset.UseVisualStyleBackColor = false;
            btnreset.BackColor = Color.FromArgb(250, 185, 34);
            btnreset.ForeColor = Color.White;
        }

        private void btndelete_MouseEnter(object sender, EventArgs e)
        {
            btndelete.UseVisualStyleBackColor = false;
            btndelete.BackColor = Color.FromArgb(255, 77, 77);
            btndelete.ForeColor = Color.White;
        }

        private void btncancel_MouseEnter(object sender, EventArgs e)
        {
            btncancel.UseVisualStyleBackColor = false;
            btncancel.BackColor = Color.FromArgb(248, 152, 94);
            btncancel.ForeColor = Color.White;
        }

        private void btnsave_Enter(object sender, EventArgs e)
        {
            btnsave.UseVisualStyleBackColor = false;
            btnsave.BackColor = Color.YellowGreen;
            btnsave.ForeColor = Color.White;
        }

        private void btnsave_Leave(object sender, EventArgs e)
        {
            btnsave.UseVisualStyleBackColor = true;
            btnsave.BackColor = Color.FromArgb(51, 153, 255);
            btnsave.ForeColor = Color.White;
        }

        private void btnreset_Enter(object sender, EventArgs e)
        {
            btnreset.UseVisualStyleBackColor = false;
            btnreset.BackColor = Color.FromArgb(250, 185, 34);
            btnreset.ForeColor = Color.White;
        }

        private void btnreset_Leave(object sender, EventArgs e)
        {
            btnreset.UseVisualStyleBackColor = true;
            btnreset.BackColor = Color.FromArgb(51, 153, 255);
            btnreset.ForeColor = Color.White;
        }

        private void btndelete_Enter(object sender, EventArgs e)
        {
            btndelete.UseVisualStyleBackColor = false;
            btndelete.BackColor = Color.FromArgb(255, 77, 77);
            btndelete.ForeColor = Color.White;
        }

        private void btndelete_Leave(object sender, EventArgs e)
        {
            btndelete.UseVisualStyleBackColor = true;
            btndelete.BackColor = Color.FromArgb(51, 153, 255);
            btndelete.ForeColor = Color.White;
        }

        private void btncancel_Enter(object sender, EventArgs e)
        {
            btncancel.UseVisualStyleBackColor = false;
            btncancel.BackColor = Color.FromArgb(248, 152, 94);
            btncancel.ForeColor = Color.White;
        }

        private void btncancel_Leave(object sender, EventArgs e)
        {
            btncancel.UseVisualStyleBackColor = true;
            btncancel.BackColor = Color.FromArgb(51, 153, 255);
            btncancel.ForeColor = Color.White;
        }

        private void txtcrdr_Enter(object sender, EventArgs e)
        {
            try
            {
                txtcrdr.SelectedIndex = 0;
                txtcrdr.DroppedDown = true;
            }
            catch
            {
            }
        }
        string searchstr;
        private QPayment qPayment;
        private BankEntry bankEntry;
        private DebitandCreditNote debitandCreditNote;
        private string activecontroal_2;
        private DefaultPOS defaultPOS;
        private AgentCommissionReport agentCommissionReport;
        private frmComplainMasterData frmComplainMasterData;
        private frmSentToCompany frmSentToCompany;
        private SalePurchaseOrderSimpleformate salePurchaseOrderSimpleformate;
        private GSTVouchers gSTVouchers;

        private void timer1_Tick(object sender, EventArgs e)
        {
            //empty the string for every 1 seconds
            //  searchstr = "";
        }

        private void txtgrop_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = txtgrop.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            txtgrop.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        internal void Update(int accountid)
        {
            try
            {
                cnt = 1;
                if (accountid != null)
                {
                    userrights = cn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[11]["d"].ToString() == "False")
                        {
                            btndelete.Enabled = false;
                        }
                    }
                    this.StartPosition = FormStartPosition.Manual;
                    this.Location = new Point(0, 0);
                    autoextendercity();
                    autoextenderstate();
                    //this.ActiveControl = txtaccname;
                    //panel2.Controls.Add(txtaccname);
                    //txtaccname.Focus();
                    //txtaccname.Select();
                    id = accountid.ToString();
                    SqlCommand cmd = new SqlCommand("select * from ClientMaster where isactive=1 and clientID='" + id + "'", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    txtaccname.Text = dt.Rows[0]["AccountName"].ToString();
                    txtprintname.Text = dt.Rows[0]["PrintName"].ToString();
                    txtgrop.Text = dt.Rows[0]["GroupName"].ToString();
                    txtopbal.Text = dt.Rows[0]["Opbal"].ToString();
                    txtcrdr.SelectedItem = dt.Rows[0]["Dr_cr"].ToString();
                    txtaddress.Text = dt.Rows[0]["Address"].ToString();
                    txtcity.Text = dt.Rows[0]["City"].ToString();
                    txtstate.Text = dt.Rows[0]["State"].ToString();
                    txtphone.Text = dt.Rows[0]["Phone"].ToString();
                    txtmobile.Text = dt.Rows[0]["Mobile"].ToString();
                    txtemail.Text = dt.Rows[0]["Email"].ToString();
                    txtcst.Text = dt.Rows[0]["Cstno"].ToString();
                    txtvat.Text = dt.Rows[0]["VATNo"].ToString();
                    txtadhar.Text = dt.Rows[0]["AdharNo"].ToString();
                    txtgst.Text = dt.Rows[0]["GstNo"].ToString();
                    txtstatecode.Text = dt.Rows[0]["statecode"].ToString();
                    txtcreditlemite.Text = dt.Rows[0]["crelimite"].ToString();
                    txtbilllimite.Text = dt.Rows[0]["billlimite"].ToString();
                    txtcreditdaysale.Text = dt.Rows[0]["credaysale"].ToString();
                    txtcreditdaypurchase.Text = dt.Rows[0]["credaypurchase"].ToString();
                    txtaccountnumber.Text = dt.Rows[0]["accountnumber"].ToString();
                    cmbcustype.Text = dt.Rows[0]["customertype"].ToString();
                    txtnoteorremark.Text = dt.Rows[0]["noteorremarks"].ToString();
                    txtDeliveryAt.Text = dt.Rows[0]["Deliveryat"].ToString();
                    DataTable options = cn.getdataset("select * from options");
                    if (options.Rows[0]["accountbillno"].ToString() == "Continuous")
                    {
                        txtaccountnumber.ReadOnly = true;
                        this.ActiveControl = txtaccname;
                    }
                    else
                    {
                        txtaccountnumber.ReadOnly = false;
                        this.ActiveControl = txtaccountnumber;
                    }
                    btnsave.Text = "Update";

                }
            }
            catch
            {
            }
            // throw new NotImplementedException();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var privouscontroal = txtgrop;
            activecontroal = privouscontroal.Name;
            Group popup = new Group(this, tabControl, master, activecontroal);
            master.AddNewTab(popup);
        }

        private void btnGroupEdit_Click(object sender, EventArgs e)
        {
            if (txtgrop.Text != null && txtgrop.Text != "")
            {
                var privouscontroal = txtgrop;
                activecontroal = privouscontroal.Name;
                Group dlg = new Group(this, tabControl, master, activecontroal);
                dlg.updatemode(1, txtgrop.Text);
                master.AddNewTab(dlg);
                dlg.Show();
            }
            else
            {
                MessageBox.Show("Please Select Group Name", "Group", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtgrop_Leave(object sender, EventArgs e)
        {
            txtgrop.Text = s;
        }

        private void txtcrdr_Leave(object sender, EventArgs e)
        {
            txtcrdr.Text = s;
        }

        private void txtgrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < txtgrop.Items.Count; i++)
                {
                    s = txtgrop.GetItemText(txtgrop.Items[i]);
                    if (s == txtgrop.Text)
                    {
                        inList = true;
                        txtgrop.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    txtgrop.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void txtcrdr_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < txtcrdr.Items.Count; i++)
                {
                    s = txtcrdr.GetItemText(txtcrdr.Items[i]);
                    if (s == txtcrdr.Text)
                    {
                        inList = true;
                        txtcrdr.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    txtcrdr.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void txtcreditlemite_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // txtstatecode.Focus();
                txtbilllimite.Focus();
            }
        }

        private void txtbilllimite_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // txtstatecode.Focus();
                txtcreditdaysale.Focus();
            }
        }

        private void txtcreditdaysale_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // txtstatecode.Focus();
                txtcreditdaypurchase.Focus();
            }
        }

        private void txtcreditdaypurchase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // txtstatecode.Focus();
                txtnoteorremark.Focus();
            }
        }

        private void txtcreditlemite_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtbilllimite_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtcreditdaysale_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtcreditdaypurchase_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtcreditlemite_Enter(object sender, EventArgs e)
        {
            txtcreditlemite.BackColor = Color.LightYellow;
        }

        private void txtcreditlemite_Leave(object sender, EventArgs e)
        {
            txtcreditlemite.BackColor = Color.White;
        }

        private void txtbilllimite_Enter(object sender, EventArgs e)
        {
            txtbilllimite.BackColor = Color.LightYellow;
        }

        private void txtbilllimite_Leave(object sender, EventArgs e)
        {
            txtbilllimite.BackColor = Color.White;
        }

        private void txtcreditdaysale_Enter(object sender, EventArgs e)
        {
            txtcreditdaysale.BackColor = Color.LightYellow;
        }

        private void txtcreditdaysale_Leave(object sender, EventArgs e)
        {
            txtcreditdaysale.BackColor = Color.White;
        }

        private void txtcreditdaypurchase_Enter(object sender, EventArgs e)
        {
            txtcreditdaypurchase.BackColor = Color.LightYellow;
        }

        private void txtcreditdaypurchase_Leave(object sender, EventArgs e)
        {
            txtcreditdaypurchase.BackColor = Color.White;
        }

        private void txtaccountnumber_Enter(object sender, EventArgs e)
        {
            txtaccountnumber.BackColor = Color.LightYellow;
        }

        private void txtaccountnumber_Leave(object sender, EventArgs e)
        {
            txtaccountnumber.BackColor = Color.White;
        }

        private void txtaccountnumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int flag = 0;
                string str = txtaccountnumber.Text.ToUpper().Trim();
                SqlCommand cmd = new SqlCommand("select accountnumber from ClientMaster where isactive=1", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (txtaccountnumber.Text != "")
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string val = dt.Rows[i][0].ToString().ToUpper().Trim();

                            if (val == str)
                            {
                                if (btnsave.Text != "Update")
                                {
                                    MessageBox.Show("Account Number Already Available Please add Another");
                                    txtaccountnumber.Focus();
                                    flag = 1;
                                    break;
                                }

                            }

                        }
                    }
                    else
                    {
                        txtaccname.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Account Number cannot be Blank");
                    txtaccountnumber.Show();
                    return;
                }
                if (flag == 0)
                {
                    txtaccname.Focus();
                }

            }
        }

        private void cmbcustype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbcustype.Items.Count; i++)
                {
                    s = cmbcustype.GetItemText(cmbcustype.Items[i]);
                    if (s == cmbcustype.Text)
                    {
                        inList = true;
                        cmbcustype.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbcustype.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void cmbcustype_Leave(object sender, EventArgs e)
        {
            cmbcustype.Text = s;
        }

        private void cmbcustype_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbcustype.SelectedIndex = 0;
                cmbcustype.DroppedDown = true;
            }
            catch
            {
            }
        }

        private void cmbcustype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbcustype.Items.Count; i++)
                {
                    s = cmbcustype.GetItemText(cmbcustype.Items[i]);
                    if (s == cmbcustype.Text)
                    {
                        inList = true;
                        cmbcustype.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbcustype.Text = "";
                }
                txtopbal.Focus();
            }
            if (e.KeyCode == Keys.F3)
            {
                var privouscontroal = cmbcustype;
                activecontroal = privouscontroal.Name;
                Customertype popup = new Customertype(this, tabControl, master, activecontroal);
                master.AddNewTab(popup);

            }

            if (e.KeyCode == Keys.F2)
            {
                var privouscontroal = cmbcustype;
                activecontroal = privouscontroal.Name;
                Customertype dlg = new Customertype(this, tabControl, master, activecontroal);

                dlg.Update(Convert.ToString(cmbcustype.SelectedValue));
                master.AddNewTab(dlg);
                //  dlg.Show();
            }
        }

        private void btncustype_Click(object sender, EventArgs e)
        {
            var privouscontroal = cmbcustype;
            activecontroal = privouscontroal.Name;
            Customertype popup = new Customertype(this, tabControl, master, activecontroal);
            master.AddNewTab(popup);
        }

        private void btncusendit_Click(object sender, EventArgs e)
        {
            if (cmbcustype.Text != null && cmbcustype.Text != "")
            {
                var privouscontroal = cmbcustype;
                activecontroal = privouscontroal.Name;
                Customertype dlg = new Customertype(this, tabControl, master, activecontroal);
                dlg.Update(Convert.ToString(cmbcustype.SelectedValue));
                master.AddNewTab(dlg);
                dlg.Show();
            }
            else
            {
                MessageBox.Show("Please Select Customer Type", "Customer Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtnoteorremark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // txtstatecode.Focus();
                btnsave.Focus();
            }
        }

        private void txtaccountnumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 8)
            //{
            //    e.Handled = false;
            //}
            //else
            //{
            //    e.Handled = true;
            //}
        }

        private void txtnoteorremark_Enter(object sender, EventArgs e)
        {
            txtnoteorremark.BackColor = Color.LightYellow;
        }

        private void txtnoteorremark_Leave(object sender, EventArgs e)
        {
            txtnoteorremark.BackColor = Color.White;
        }

        private void Accountentry_Shown(object sender, EventArgs e)
        {
            try
            {
                DataTable options = cn.getdataset("select * from options");
                if (options.Rows[0]["accountbillno"].ToString() == "Continuous")
                {
              /*      string str = cn.ExecuteScalar("select max(ClientID) from ClientMaster where isactive=1");
                    Int64 id, count;
                    if (str == "")
                    {

                        id = Convert.ToInt64(1);
                        count = Convert.ToInt64(1);
                    }
                    else
                    {
                        id = Convert.ToInt64(str) + 1;
                        count = Convert.ToInt64(str) + 1;
                    }
                    txtaccountnumber.Text = options.Rows[0]["accountprefix"].ToString() + count.ToString();
                */    txtaccountnumber.ReadOnly = true;
                }
                else
                {
                  //  txtaccountnumber.Text = "";
                    txtaccountnumber.ReadOnly = false;
                    txtaccountnumber.Focus();
                    this.ActiveControl = txtaccountnumber;
                }
            }
            catch
            {
            }
        }
    }
}
