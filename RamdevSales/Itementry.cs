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
    public partial class Itementry : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        public string strConnection = ConfigurationManager.ConnectionStrings["qry"].ToString();
        AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
        private string id;
        Connection cn = new Connection();
        int gener = 0;
        //  static int iid = 0; 
        public static string activecontroal;
        public static string pvc;
        int a, b, c, d;
        string proid;
        public Itementry()
        {
            InitializeComponent();
            //   pnlgst.Visible = false;
            // pnlvat.Visible = false;
            bindcompany();
            //  bindSystem();
            //bindsaletype();
            binditemprice();
            grdbatch.AllowUserToAddRows = false;
            DataRow dr = batch.NewRow();
            dr["batch"] = "NA";
            dr["Exp.Dt."] = "";
            dr["Mfg.Dt."] = "";
            dr["Exp.Days"] = "";
            dr["Barcode"] = "";
            dr["MRP"] = "0";
            dr["Basic Price"] = "0";
            dr["Sale Price"] = "0";
            dr["Purchase Price"] = "0";
            dr["Self Val"] = "0";
            dr["Min.Sale"] = "0";
            dr["Op.Packs"] = "0";
            dr["Op.Loose"] = "0";
            dr["Op. Stock(Val)"] = "0";
            batch.Rows.Add(dr);
            grdbatch.DataSource = batch;
        }
        DataTable options;

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
        //public Itementry(Sale sale)
        //{
        //    InitializeComponent();
        //    //  pnlgst.Visible = false;
        //    //pnlvat.Visible = false;
        //    bindgaunit();
        //    bindgroup();
        //    bindgpunit();
        //    bindcompany();
        //    bindtaxslab();
        //    binditemprice();
        //    grdbatch.AllowUserToAddRows = false;
        //    DataRow dr = batch.NewRow();
        //    dr["batch"] = "NA";
        //    dr["Exp.Dt."] = "";
        //    dr["Mfg.Dt."] = "";
        //    dr["Exp.Days"] = "";
        //    dr["Barcode"] = "";
        //    dr["MRP"] = "0";
        //    dr["Basic Price"] = "0";
        //    dr["Sale Price"] = "0";
        //    dr["Purchase Price"] = "0";
        //    dr["Self Val"] = "0";
        //    dr["Min.Sale"] = "0";
        //    dr["Op.Packs"] = "0";
        //    dr["Op.Loose"] = "0";
        //    dr["Op. Stock(Val)"] = "0";
        //    batch.Rows.Add(dr);
        //    grdbatch.DataSource = batch;
        //    // TODO: Complete member initialization
        //    this.sale = sale;

        //}
        //private DefaultPurchase purchase;
        //public Itementry(DefaultPurchase purchase)
        //{
        //    InitializeComponent();
        //    //  pnlgst.Visible = false;
        //    // pnlvat.Visible = false;
        //    bindgaunit();
        //    bindgroup();
        //    bindgpunit();
        //    bindtaxslab();
        //    bindcompany();
        //    binditemprice();
        //    grdbatch.AllowUserToAddRows = false;
        //    DataRow dr = batch.NewRow();
        //    dr["batch"] = "NA";
        //    dr["Exp.Dt."] = "";
        //    dr["Mfg.Dt."] = "";
        //    dr["Exp.Days"] = "";
        //    dr["Barcode"] = "";
        //    dr["MRP"] = "0";
        //    dr["Basic Price"] = "0";
        //    dr["Sale Price"] = "0";
        //    dr["Purchase Price"] = "0";
        //    dr["Self Val"] = "0";
        //    dr["Min.Sale"] = "0";
        //    dr["Op.Packs"] = "0";
        //    dr["Op.Loose"] = "0";
        //    dr["Op. Stock(Val)"] = "0";
        //    batch.Rows.Add(dr);
        //    grdbatch.DataSource = batch;
        //    // TODO: Complete member initialization
        //    this.purchase = purchase;

        //}

        //public Itementry(SaleReturn saleReturn)
        //{
        //    InitializeComponent();
        //    //  pnlgst.Visible = false;
        //    // pnlvat.Visible = false;
        //    bindgaunit();
        //    bindgroup();
        //    bindgpunit();
        //    bindtaxslab();
        //    bindcompany();
        //    binditemprice();
        //    grdbatch.AllowUserToAddRows = false;
        //    DataRow dr = batch.NewRow();
        //    dr["batch"] = "NA";
        //    dr["Exp.Dt."] = "";
        //    dr["Mfg.Dt."] = "";
        //    dr["Exp.Days"] = "";
        //    dr["Barcode"] = "";
        //    dr["MRP"] = "0";
        //    dr["Basic Price"] = "0";
        //    dr["Sale Price"] = "0";
        //    dr["Purchase Price"] = "0";
        //    dr["Self Val"] = "0";
        //    dr["Min.Sale"] = "0";
        //    dr["Op.Packs"] = "0";
        //    dr["Op.Loose"] = "0";
        //    dr["Op. Stock(Val)"] = "0";
        //    batch.Rows.Add(dr);
        //    grdbatch.DataSource = batch;
        //    // TODO: Complete member initialization
        //    this.saleReturn = saleReturn;

        //}

        //public Itementry(Purchase purchase_2)
        //{
        //    InitializeComponent();
        //    //   pnlgst.Visible = false;
        //    //  pnlvat.Visible = false;
        //    bindgaunit();
        //    bindgroup();
        //    bindgpunit();
        //    bindtaxslab();
        //    bindcompany();
        //    binditemprice();
        //    grdbatch.AllowUserToAddRows = false;
        //    DataRow dr = batch.NewRow();
        //    dr["batch"] = "NA";
        //    dr["Exp.Dt."] = "";
        //    dr["Mfg.Dt."] = "";
        //    dr["Exp.Days"] = "";
        //    dr["Barcode"] = "";
        //    dr["MRP"] = "0";
        //    dr["Basic Price"] = "0";
        //    dr["Sale Price"] = "0";
        //    dr["Purchase Price"] = "0";
        //    dr["Self Val"] = "0";
        //    dr["Min.Sale"] = "0";
        //    dr["Op.Packs"] = "0";
        //    dr["Op.Loose"] = "0";
        //    dr["Op. Stock(Val)"] = "0";
        //    batch.Rows.Add(dr);
        //    grdbatch.DataSource = batch;
        //    // TODO: Complete member initialization
        //    this.purchase_2 = purchase_2;

        //}

        public Itementry(DefaultSale defaultSale)
        {
            InitializeComponent();
            //     pnlgst.Visible = false;
            //   pnlvat.Visible = false;
            bindgaunit();
            bindgroup();
            bindgpunit();
            bindtaxslab();
            bindcompany();
            binditemprice();
            grdbatch.AllowUserToAddRows = false;
            DataRow dr = batch.NewRow();
            dr["batch"] = "NA";
            dr["Exp.Dt."] = "";
            dr["Mfg.Dt."] = "";
            dr["Exp.Days"] = "";
            dr["Barcode"] = "";
            dr["MRP"] = "0";
            dr["Basic Price"] = "0";
            dr["Sale Price"] = "0";
            dr["Purchase Price"] = "0";
            dr["Self Val"] = "0";
            dr["Min.Sale"] = "0";
            dr["Op.Packs"] = "0";
            dr["Op.Loose"] = "0";
            dr["Op. Stock(Val)"] = "0";
            batch.Rows.Add(dr);
            grdbatch.DataSource = batch;
            // TODO: Complete member initialization
            this.defaultSale = defaultSale;

        }

        public Itementry(TabControl tabControl)
        {
            InitializeComponent();
            //     pnlgst.Visible = false;
            //   pnlvat.Visible = false;
            bindgroup();
            bindgpunit();
            bindgaunit();
            bindtaxslab();
            bindcompany();
            binditemprice();
            grdbatch.AllowUserToAddRows = false;
            DataRow dr = batch.NewRow();
            dr["batch"] = "NA";
            dr["Exp.Dt."] = "";
            dr["Mfg.Dt."] = "";
            dr["Exp.Days"] = "";
            dr["Barcode"] = "";
            dr["MRP"] = "0";
            dr["Basic Price"] = "0";
            dr["Sale Price"] = "0";
            dr["Purchase Price"] = "0";
            dr["Self Val"] = "0";
            dr["Min.Sale"] = "0";
            dr["Op.Packs"] = "0";
            dr["Op.Loose"] = "0";
            dr["Op. Stock(Val)"] = "0";
            batch.Rows.Add(dr);
            grdbatch.DataSource = batch;
            this.tabControl = tabControl;
        }

        public Itementry(Master master, TabControl tabControl)
        {
            InitializeComponent();
            //   pnlgst.Visible = false;
            // pnlvat.Visible = false;
            bindgaunit();
            bindgroup();
            bindgpunit();
            bindtaxslab();
            bindcompany();
            binditemprice();
            grdbatch.AllowUserToAddRows = false;
            DataRow dr = batch.NewRow();
            dr["batch"] = "NA";
            dr["Exp.Dt."] = "";
            dr["Mfg.Dt."] = "";
            dr["Exp.Days"] = "";
            dr["Barcode"] = "";
            dr["MRP"] = "0";
            dr["Basic Price"] = "0";
            dr["Sale Price"] = "0";
            dr["Purchase Price"] = "0";
            dr["Self Val"] = "0";
            dr["Min.Sale"] = "0";
            dr["Op.Packs"] = "0";
            dr["Op.Loose"] = "0";
            dr["Op. Stock(Val)"] = "0";
            batch.Rows.Add(dr);
            grdbatch.DataSource = batch;
            this.master = master;
            this.tabControl = tabControl;
            this.ActiveControl = txtiname;
        }

        public Itementry(DefaultSale defaultSale, Master master, TabControl tabControl, string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            //    pnlgst.Visible = false;
            //  pnlvat.Visible = false;
            bindgroup();
            bindgaunit();
            bindgpunit();
            bindtaxslab();
            bindcompany();
            binditemprice();
            grdbatch.AllowUserToAddRows = false;
            DataRow dr = batch.NewRow();
            dr["batch"] = "NA";
            dr["Exp.Dt."] = "";
            dr["Mfg.Dt."] = "";
            dr["Exp.Days"] = "";
            dr["Barcode"] = "";
            dr["MRP"] = "0";
            dr["Basic Price"] = "0";
            dr["Sale Price"] = "0";
            dr["Purchase Price"] = "0";
            dr["Self Val"] = "0";
            dr["Min.Sale"] = "0";
            dr["Op.Packs"] = "0";
            dr["Op.Loose"] = "0";
            dr["Op. Stock(Val)"] = "0";
            batch.Rows.Add(dr);
            grdbatch.DataSource = batch;
            this.defaultSale = defaultSale;
            this.master = master;
            this.tabControl = tabControl;
            pvc = activecontroal;
        }

        //public Itementry(Sale sale, Master master, TabControl tabControl)
        //{
        //    // TODO: Complete member initialization
        //    InitializeComponent();
        //    bindtaxslab();
        //    bindgaunit();
        //    bindgroup();
        //    bindgpunit();
        //    //     pnlgst.Visible = false;
        //    //   pnlvat.Visible = false;
        //    bindcompany();
        //    binditemprice();
        //    grdbatch.AllowUserToAddRows = false;
        //    DataRow dr = batch.NewRow();
        //    dr["batch"] = "NA";
        //    dr["Exp.Dt."] = "";
        //    dr["Mfg.Dt."] = "";
        //    dr["Exp.Days"] = "";
        //    dr["Barcode"] = "";
        //    dr["MRP"] = "0";
        //    dr["Basic Price"] = "0";
        //    dr["Sale Price"] = "0";
        //    dr["Purchase Price"] = "0";
        //    dr["Self Val"] = "0";
        //    dr["Min.Sale"] = "0";
        //    dr["Op.Packs"] = "0";
        //    dr["Op.Loose"] = "0";
        //    dr["Op. Stock(Val)"] = "0";
        //    batch.Rows.Add(dr);
        //    grdbatch.DataSource = batch;
        //    this.sale = sale;
        //    this.master = master;
        //    this.tabControl = tabControl;
        //}

        public Itementry(DefaultSaleOrder defaultSaleOrder, Master master, TabControl tabControl, string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            //   pnlgst.Visible = false;
            // pnlvat.Visible = false;
            bindgroup();
            bindgaunit();
            bindgpunit();
            bindtaxslab();
            bindcompany();
            binditemprice();
            grdbatch.AllowUserToAddRows = false;
            DataRow dr = batch.NewRow();
            dr["batch"] = "NA";
            dr["Exp.Dt."] = "";
            dr["Mfg.Dt."] = "";
            dr["Exp.Days"] = "";
            dr["Barcode"] = "";
            dr["MRP"] = "0";
            dr["Basic Price"] = "0";
            dr["Sale Price"] = "0";
            dr["Purchase Price"] = "0";
            dr["Self Val"] = "0";
            dr["Min.Sale"] = "0";
            dr["Op.Packs"] = "0";
            dr["Op.Loose"] = "0";
            dr["Op. Stock(Val)"] = "0";
            batch.Rows.Add(dr);
            grdbatch.DataSource = batch;
            this.defaultSaleOrder = defaultSaleOrder;
            this.master = master;
            this.tabControl = tabControl;
            pvc = activecontroal;
        }

        public Itementry(frmComplainMasterData frmComplainMasterData, Master master, TabControl tabControl, string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            //   pnlgst.Visible = false;
            // pnlvat.Visible = false;
            bindgroup();
            bindgaunit();
            bindgpunit();
            bindtaxslab();
            bindcompany();
            binditemprice();
            grdbatch.AllowUserToAddRows = false;
            DataRow dr = batch.NewRow();
            dr["batch"] = "NA";
            dr["Exp.Dt."] = "";
            dr["Mfg.Dt."] = "";
            dr["Exp.Days"] = "";
            dr["Barcode"] = "";
            dr["MRP"] = "0";
            dr["Basic Price"] = "0";
            dr["Sale Price"] = "0";
            dr["Purchase Price"] = "0";
            dr["Self Val"] = "0";
            dr["Min.Sale"] = "0";
            dr["Op.Packs"] = "0";
            dr["Op.Loose"] = "0";
            dr["Op. Stock(Val)"] = "0";
            batch.Rows.Add(dr);
            grdbatch.DataSource = batch;
            this.frmComplainMasterData = frmComplainMasterData;
            this.master = master;
            this.tabControl = tabControl;
            pvc = activecontroal;
        }

        public Itementry(Process process, Master master, TabControl tabControl, string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            bindgaunit();
            bindgroup();
            bindgpunit();
            bindtaxslab();
            bindcompany();
            binditemprice();
            grdbatch.AllowUserToAddRows = false;
            DataRow dr = batch.NewRow();
            dr["batch"] = "NA";
            dr["Exp.Dt."] = "";
            dr["Mfg.Dt."] = "";
            dr["Exp.Days"] = "";
            dr["Barcode"] = "";
            dr["MRP"] = "0";
            dr["Basic Price"] = "0";
            dr["Sale Price"] = "0";
            dr["Purchase Price"] = "0";
            dr["Self Val"] = "0";
            dr["Min.Sale"] = "0";
            dr["Op.Packs"] = "0";
            dr["Op.Loose"] = "0";
            dr["Op. Stock(Val)"] = "0";
            batch.Rows.Add(dr);
            grdbatch.DataSource = batch;
            pvc = activecontroal;
            this.process = process;
            this.master = master;
            this.tabControl = tabControl;
        }

        public Itementry(SalePurchaseOrderSimpleformate salePurchaseOrderSimpleformate, Master master, TabControl tabControl, string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            bindgroup();
            bindgaunit();
            bindgpunit();
            bindtaxslab();
            bindcompany();
            binditemprice();
            grdbatch.AllowUserToAddRows = false;
            DataRow dr = batch.NewRow();
            dr["batch"] = "NA";
            dr["Exp.Dt."] = "";
            dr["Mfg.Dt."] = "";
            dr["Exp.Days"] = "";
            dr["Barcode"] = "";
            dr["MRP"] = "0";
            dr["Basic Price"] = "0";
            dr["Sale Price"] = "0";
            dr["Purchase Price"] = "0";
            dr["Self Val"] = "0";
            dr["Min.Sale"] = "0";
            dr["Op.Packs"] = "0";
            dr["Op.Loose"] = "0";
            dr["Op. Stock(Val)"] = "0";
            batch.Rows.Add(dr);
            grdbatch.DataSource = batch;
            this.salePurchaseOrderSimpleformate = salePurchaseOrderSimpleformate;
            this.master = master;
            this.tabControl = tabControl;
            pvc = activecontroal;
        }
        DataTable batch = new DataTable();
        int flag = 0;
        //   private Sale sale;
        // private SaleReturn saleReturn;
        // private Purchase purchase_2;
        private DefaultSale defaultSale;
        private TabControl tabControl;
        private Master master;
        private DefaultSaleOrder defaultSaleOrder;
        DataTable userrights = new DataTable();
        public void getitemno()
        {
            try
            {
                DataTable options = cn.getdataset("select * from options");
                if (options.Rows[0]["itembillno"].ToString() == "Continuous")
                {
                    string str = cn.ExecuteScalar("select max(convert(float,itemnumber)) from ProductMaster where isactive=1");
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
                    txtitemnumber.Text = options.Rows[0]["itemprefix"].ToString() + count.ToString();
                    txtitemnumber.ReadOnly = true;
                }
                else
                {
                    txtitemnumber.Text = "";
                    txtitemnumber.ReadOnly = false;
                    txtitemnumber.Focus();
                    this.ActiveControl = txtitemnumber;
                }
            }
            catch
            {
            }
        }
        private void Itementry_Load(object sender, EventArgs e)
        {
            con.Open();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            userrights = cn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[10]["d"].ToString() == "False")
                {
                    btndelete.Enabled = false;
                }
            }
            //  autogroup();
            //  punit();
            // aunit();
            options = cn.getdataset("select * from options");
            if (Convert.ToBoolean(options.Rows[0]["cess"].ToString()) == true)
            {
                lblcessper.Visible = true;
                lblcessamt.Visible = true;
                txtcessper.Visible = true;
                txtcessamt.Visible = true;
            }
            // lvsaletype.Columns.Add("id", 0, HorizontalAlignment.Left);



            grdbatch.Columns[0].Width = 49;
            grdbatch.Columns[1].Width = 59;
            grdbatch.Columns[2].Width = 60;
            grdbatch.Columns[3].Width = 55;
            grdbatch.Columns[4].Width = 95;
            grdbatch.Columns[5].Width = 95;
            grdbatch.Columns[6].Width = 95;
            grdbatch.Columns[7].Width = 95;
            grdbatch.Columns[8].Width = 105;
            grdbatch.Columns[9].Width = 95;
            grdbatch.Columns[10].Width = 95;
            grdbatch.Columns[11].Width = 95;
            grdbatch.Columns[12].Width = 95;
            grdbatch.Columns[13].Width = 105;

            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            grdbatch.Columns.Add(btn);
            btn.HeaderText = "Delete";
            // btn.Text = "Delete";
            btn.Name = "btndelete";
            btn.UseColumnTextForButtonValue = true;
            grdbatch.Columns[14].Width = 30;

            //grdbatch.Columns[0].ReadOnly = true;
            //grdbatch.Columns[1].ReadOnly = true;
            //grdbatch.Columns[2].ReadOnly = true;
            //grdbatch.Columns[3].ReadOnly = true;
            //grdbatch.Columns[4].ReadOnly = true;
            grdbatch.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdbatch.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdbatch.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdbatch.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdbatch.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdbatch.RowHeadersVisible = false;



            //   update();

            //set the interval  and start the timer
            //timer1.Interval = 1000;
            //timer1.Start();
            this.ActiveControl = txtiname;
            if (btnsubmit.Text != "Update")
            {
                getitemno();
            }
            con.Close();

        }

        private void binditemprice()
        {
            try
            {
                batch.Columns.Add("batch", typeof(string));
                batch.Columns.Add("Exp.Dt.", typeof(string));
                batch.Columns.Add("Mfg.Dt.", typeof(string));
                batch.Columns.Add("Exp.Days", typeof(string));
                batch.Columns.Add("Barcode", typeof(string));
                batch.Columns.Add("MRP", typeof(double));
                batch.Columns.Add("Basic Price", typeof(double));
                batch.Columns.Add("Sale Price", typeof(double));
                batch.Columns.Add("Purchase Price", typeof(double));
                batch.Columns.Add("Self Val", typeof(double));
                batch.Columns.Add("Min.Sale", typeof(double));
                batch.Columns.Add("Op.Packs", typeof(double));
                batch.Columns.Add("Op.Loose", typeof(double));
                batch.Columns.Add("Op. Stock(Val)", typeof(double));
            }
            catch
            {
            }
            //  DataTable price=cn.getdataset("select * from pricepricemaster where productid='"++"'");
        }
        public void bindcompany()
        {
            SqlCommand cmd = new SqlCommand("select CompanyID,CompanyName from CompanyMaster", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            cmbcompany.ValueMember = "CompanyID";
            cmbcompany.DisplayMember = "CompanyName";
            cmbcompany.DataSource = dt;
            cmbcompany.SelectedIndex = -1;

        }
        public void bindtaxslab()
        {
            SqlCommand cmd = new SqlCommand("select DISTINCT Taxslabname from TaxSlabMaster", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            // cmbtaxslab.ValueMember = "id";
            cmbtaxslab.DisplayMember = "Taxslabname";
            cmbtaxslab.DataSource = dt;
            cmbtaxslab.SelectedIndex = -1;
        }
        public void submit()
        {
            try
            {
                this.Enabled = false;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                string str = txtiname.Text.ToUpper().Trim();
                SqlCommand cmmd1 = new SqlCommand("select Product_Name from ProductMaster where isactive=1", con);
                SqlDataAdapter sdda = new SqlDataAdapter(cmmd1);
                DataTable dtt = new DataTable();
                sdda.Fill(dtt);
                flag = 0;
                if (btnsubmit.Text == "New")
                {
                    btnsubmit.Text = "Save";
                    txtiname.Focus();
                }
                else if (btnsubmit.Text == "Update" && flag == 0)
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[10]["u"].ToString() == "True")
                        {
                            if (chkbatch.Checked == true)
                            {
                                a = 1;
                            }
                            else
                            {
                                a = 0;
                            }
                            if (chkserial.Checked == true)
                            {
                                b = 1;
                            }
                            else
                            {
                                b = 0;
                            }
                            if (chkhot.Checked == true)
                            {
                                c = 1;
                            }
                            else
                            {
                                c = 0;
                            }
                            if (chkshare.Checked == true)
                            {
                                d = 1;
                            }
                            else
                            {
                                d = 0;
                            }
                            if (txtcessper.Text == "")
                            {
                                txtcessper.Text = "0";
                            }
                            if (txtcessamt.Text == "")
                            {
                                txtcessamt.Text = "0";
                            }
                            SqlCommand cmd = new SqlCommand("update ProductMaster set minstock='" + txtminstock.Text + "',maxstock='" + txtmaxstock.Text + "',reorderqty='" + txtreorderqty.Text + "',[OT1]='" + txtf1.Text + "',[OT2]='" + txtf2.Text + "',[OT3]='" + txtf3.Text + "',[OT4]='" + txtf4.Text + "',[OT5]='" + txtf5.Text + "',[OT6]='" + txtf6.Text + "',[OT7]='" + txtf7.Text + "',[OT8]='" + txtf8.Text + "',[OT9]='" + txtf9.Text + "',[OT10]='" + txtf10.Text + "',[OT11]='" + txtf11.Text + "',[OT12]='" + txtf12.Text + "',[OT13]='" + txtf13.Text + "',[OT14]='" + txtf14.Text + "',[OT15]='" + txtf15.Text + "',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',Companyid ='" + cmbcompany.SelectedValue + "', GroupName='" + txtgroup.Text + "',Hsn_Sac_Code='" + txthsn.Text + "',Product_Name='" + txtiname.Text + "',Unit='" + txtpunit.Text + "',IsBatch='" + a + "',isserial='" + b + "',Altunit='" + txtaunit.Text + "',Convfactor='" + txtcfactor.Text + "',cessper='" + txtcessper.Text + "',cessamt='" + txtcessamt.Text + "',Packing='" + txtpacking.Text + "',taxslab='" + cmbtaxslab.Text + "',isHotProduct='" + c + "',isshare='" + d + "',itemnumber='" + txtitemnumber.Text + "',itemimage='" + photo1[1] + "',isactive=1 where ProductID='" + id + "'", con);
                            cmd.ExecuteNonQuery();
                            cmd = new SqlCommand("update ProductPriceMaster set isactive=0,SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "' where productid='" + id + "'", con);
                            cmd.ExecuteNonQuery();
                            for (int i = 0; i < grdbatch.Rows.Count; i++)
                            {
                                String barcode = "";
                                try
                                {
                                    barcode = grdbatch.Rows[i].Cells["Barcode"].Value.ToString();
                                }
                                catch
                                {
                                    barcode = grdbatch.Rows[i].Cells["Barcode"].Value.ToString();
                                }
                                if (barcode == "" || barcode == "0")
                                {
                                    grdbatch.Rows[i].Cells["Barcode"].Value = (10000 + Convert.ToInt32(id));
                                }
                                Guid guid;
                                guid = Guid.NewGuid();
                                string sql1 = "INSERT INTO [dbo].[ProductPriceMaster]([Productid],[Batchno],[BasicPrice],[SalePrice],[MRP],[PurchasePrice],[Barcode],[OpStock] ,[ExpDt],[mfgdt],[Expdays],[SelfVal],[minsaleprice],[oploose],[opstockval],[SyncID],[SyncDatetime],[isshare],[isactive])VALUES('" + id + "','" + grdbatch.Rows[i].Cells["batch"].Value + "'," + grdbatch.Rows[i].Cells["Basic Price"].Value + "," + grdbatch.Rows[i].Cells["Sale Price"].Value + "," + grdbatch.Rows[i].Cells["MRP"].Value + "," + grdbatch.Rows[i].Cells["Purchase Price"].Value + ",'" + grdbatch.Rows[i].Cells["Barcode"].Value + "','" + grdbatch.Rows[i].Cells["Op.Packs"].Value + "','" + grdbatch.Rows[i].Cells["Exp.Dt."].Value + "','" + grdbatch.Rows[i].Cells["Mfg.Dt."].Value + "','" + grdbatch.Rows[i].Cells["Exp.Days"].Value + "','" + grdbatch.Rows[i].Cells["Self Val"].Value + "','" + grdbatch.Rows[i].Cells["Min.Sale"].Value + "','" + grdbatch.Rows[i].Cells["Op.Loose"].Value + "','" + grdbatch.Rows[i].Cells["Op. Stock(Val)"].Value + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "','" + d + "','1')";
                                SqlCommand cmd2 = new SqlCommand(sql1, con);
                                cmd2.ExecuteNonQuery();

                            }
                            clear();
                            MessageBox.Show("Update Successfully");
                            //cmd = new SqlCommand("update ProductPriceMaster set Batchno='" + txtbatchno.Text + "',BasicPrice='" + txtbasic.Text + "',SalePrice='" + txtsale.Text + "',MRP='" + txtmrp.Text + "',PurchasePrice='" + txtPurchase.Text + "',Barcode='" + txtbarcode.Text + "',OpStock='" + txtopstock.Text + "' where ProductID='" + id + "'", con);
                            //cmd.ExecuteNonQuery();
                            //   cmd = new SqlCommand("delete from ItemTaxMaster where productid='" + id + "'", con);
                            // cmd.ExecuteNonQuery();
                            //for (int i = 0; i < lvsaletype.Items.Count; i++)
                            //{
                            //    SqlCommand cmd3 = new SqlCommand("INSERT INTO [dbo].[ItemTaxMaster]([productid],[saletypeid],[vat],[AddVat])VALUES('" + id + "','" + lvsaletype.Items[i].SubItems[0].Text + "','" + lvsaletype.Items[i].SubItems[1].Text + "','" + lvsaletype.Items[i].SubItems[2].Text + "')", con);
                            //    cmd3.ExecuteNonQuery();
                            //}
                            //for (int i = 0; i < lvgst.Items.Count; i++)
                            //{
                            //    string qry = "INSERT INTO [dbo].[ItemTaxMaster]([productid],[saletypeid],[System],[category],[onwhich],[SGST],[CGST],[IGST],[Additax],[isonmrp],[isonfreegoods],[isactive])VALUES('" + id + "','" + lvgst.Items[i].SubItems[0].Text + "','" + lvgst.Items[i].SubItems[1].Text + "','" + lvgst.Items[i].SubItems[2].Text + "','" + lvgst.Items[i].SubItems[3].Text + "','" + lvgst.Items[i].SubItems[4].Text + "','" + lvgst.Items[i].SubItems[5].Text + "','" + lvgst.Items[i].SubItems[6].Text + "','" + lvgst.Items[i].SubItems[7].Text + "','" + lvgst.Items[i].SubItems[8].Text + "','" + lvgst.Items[i].SubItems[9].Text + "','1')";
                            //    SqlCommand cmd4 = new SqlCommand(qry, con);
                            //    cmd4.ExecuteNonQuery();
                            //}
                            btnsubmit.Text = "Save";
                            //    master.RemoveCurrentTab();
                            if (gener == 1)
                            {
                                try
                                {
                                    if (pvc == "cmbmainitem")
                                    {
                                        process.binditem();
                                        process.bindrowitem();
                                        process.bindproductionitem();
                                    }
                                    else if (pvc == "cmbitemname")
                                    {
                                        process.binditem();
                                        process.bindrowitem();
                                        process.bindproductionitem();
                                    }
                                    else
                                    {
                                        process.binditem();
                                        process.bindrowitem();
                                        process.bindproductionitem();

                                    }
                                }
                                catch
                                {
                                }
                                try
                                {
                                    //  sale.autoreaderbind();
                                    // sale.txtitemname.Text = txtiname.Text;
                                }
                                catch
                                {
                                }
                                try
                                {
                                    //  saleReturn.autoreaderbind();
                                    // saleReturn.txtitemname.Text = txtiname.Text;
                                }
                                catch
                                {

                                }
                                try
                                {
                                    //  purchase.autoreaderbind();
                                    // purchase.txtitemname.Text = txtiname.Text;
                                }
                                catch
                                {
                                }
                                try
                                {
                                    //  purchase_2.autoreaderbind();
                                    //purchase_2.txtitemname.Text = txtiname.Text;
                                }
                                catch
                                {
                                }
                                try
                                {
                                   // defaultSale.autoreaderbind();
                                    defaultSale.txtitemname.Text = txtiname.Text;
                                }
                                catch
                                {
                                }
                                try
                                {
                                   // defaultSaleOrder.autoreaderbind();
                                    defaultSaleOrder.txtitemname.Text = txtiname.Text;
                                }
                                catch
                                {
                                }
                                try
                                {
                                   // salePurchaseOrderSimpleformate.autoreaderbind();
                                    salePurchaseOrderSimpleformate.txtitemname.Text = txtiname.Text;
                                }
                                catch
                                {
                                }
                                try
                                {
                                    frmComplainMasterData.bindallitem();
                                    frmComplainMasterData.cmbItemName.Text = txtiname.Text;
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
                                    master.RemoveCurrentTab1(pvc, txtiname.Text);
                                }
                            }
                            else
                            {
                                //    master.RemoveCurrentTab();
                                //    Itemmaster dlg = new Itemmaster(master, tabControl);
                                //    master.AddNewTab(dlg);
                                // btnsubmit.Text = "New";
                                //btnsubmit.Text = "New";
                                this.ActiveControl = txtiname;
                                txtiname.Focus();
                            }

                            txtiname.Text = string.Empty;

                            //   btnsubmit.Focus();
                            //Itemmaster im = new Itemmaster();
                            //im.listviewbind();
                            //master.RemoveCurrentTab();
                        }
                        else
                        {
                            MessageBox.Show("You don't have Permission To Update");
                            return;
                        }
                    }
                }
                else if (flag == 0)
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[10]["a"].ToString() == "True")
                        {
                            if (txtiname.Text != "")
                            {
                                if (dtt.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dtt.Rows.Count; i++)
                                    {
                                        string val = dtt.Rows[i][0].ToString().ToUpper().Trim();
                                        if (val == str)
                                        {
                                            MessageBox.Show("Item Already Available Please add Another");
                                            txtiname.Focus();
                                            flag = 1;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    txtgroup1.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Item Name cannot be Blank");
                                // txtiname.Show();
                                this.ActiveControl = txtiname;
                                return;

                            }
                            if (chkbatch.Checked == true)
                            {
                                a = 1;
                            }
                            else
                            {
                                a = 0;
                            }
                            if (chkserial.Checked == true)
                            {
                                b = 1;
                            }
                            else
                            {
                                b = 0;
                            }
                            if (chkhot.Checked == true)
                            {
                                c = 1;
                            }
                            else
                            {
                                c = 0;
                            }
                            if (chkshare.Checked == true)
                            {
                                d = 1;
                            }
                            else
                            {
                                d = 0;
                            }
                            if (flag == 0)
                            {
                                if (txtcessper.Text == "")
                                {
                                    txtcessper.Text = "0";
                                }
                                if (txtcessamt.Text == "")
                                {
                                    txtcessamt.Text = "0";
                                }
                                Guid guid;
                                guid = Guid.NewGuid();
                                string sql = "INSERT INTO [dbo].[ProductMaster]([CompanyID],[GroupName],[Product_Name],[Unit],[Altunit],[Convfactor],[Packing],[IsBatch],[isserial],[Hsn_Sac_Code],[cessper],[cessamt],[taxslab],[isHotProduct],[itemnumber],[itemimage],[isshare],[SyncID],[Syncdatetime],[OT1],[OT2],[OT3],[OT4],[OT5],[OT6],[OT7],[OT8],[OT9],[OT10],[OT11],[OT12],[OT13],[OT14],[OT15],[minstock],[maxstock],[reorderqty],isactive) VALUES('" + cmbcompany.SelectedValue + "','" + txtgroup.Text + "','" + txtiname.Text + "','" + txtpunit.Text + "','" + txtaunit.Text + "','" + txtcfactor.Text + "','" + txtpacking.Text + "','" + a + "','" + b + "','" + txthsn.Text + "','" + txtcessper.Text + "','" + txtcessamt.Text + "','" + cmbtaxslab.Text + "','" + c + "','" + txtitemnumber.Text + "','" + photo1[1] + "','" + d + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "','" + txtf1.Text + "','" + txtf2.Text + "','" + txtf3.Text + "','" + txtf4.Text + "','" + txtf5.Text + "','" + txtf6.Text + "','" + txtf7.Text + "','" + txtf8.Text + "','" + txtf9.Text + "','" + txtf10.Text + "','" + txtf11.Text + "','" + txtf12.Text + "','" + txtf13.Text + "','" + txtf14.Text + "','" + txtf15.Text +"','" + txtminstock.Text + "','" + txtmaxstock.Text + "','" + txtreorderqty.Text + "','1')";
                                SqlCommand cmd = new SqlCommand(sql, con);
                                cmd.ExecuteNonQuery();


                                SqlCommand cmd1 = new SqlCommand("select ProductID from ProductMaster where Product_Name='" + txtiname.Text + "' and isactive=1", con);
                                SqlDataAdapter sda = new SqlDataAdapter(cmd1);
                                DataTable dt = new DataTable();
                                sda.Fill(dt);
                                proid = dt.Rows[0]["ProductID"].ToString();

                                for (int i = 0; i < grdbatch.Rows.Count; i++)
                                {

                                    if (grdbatch.Rows[i].Cells["Barcode"].Value == "" || grdbatch.Rows[i].Cells["Barcode"].Value == "0")
                                    {
                                        grdbatch.Rows[i].Cells["Barcode"].Value = (10000 + Convert.ToInt32(proid));
                                    }
                                    else
                                    {
                                    }
                                    Guid guid1;
                                    guid1 = Guid.NewGuid();
                                    string sql1 = "INSERT INTO [dbo].[ProductPriceMaster]([Productid],[Batchno],[BasicPrice],[SalePrice],[MRP],[PurchasePrice],[Barcode],[OpStock] ,[ExpDt],[mfgdt],[Expdays],[SelfVal],[minsaleprice],[oploose],[opstockval],[SyncID],[SyncDatetime],[isshare],[isactive])VALUES('" + proid + "','" + grdbatch.Rows[i].Cells["batch"].Value + "'," + grdbatch.Rows[i].Cells["Basic Price"].Value + "," + grdbatch.Rows[i].Cells["Sale Price"].Value + "," + grdbatch.Rows[i].Cells["MRP"].Value + "," + grdbatch.Rows[i].Cells["Purchase Price"].Value + ",'" + grdbatch.Rows[i].Cells["Barcode"].Value + "','" + grdbatch.Rows[i].Cells["Op.Packs"].Value + "','" + grdbatch.Rows[i].Cells["Exp.Dt."].Value + "','" + grdbatch.Rows[i].Cells["Mfg.Dt."].Value + "','" + grdbatch.Rows[i].Cells["Exp.Days"].Value + "','" + grdbatch.Rows[i].Cells["Self Val"].Value + "','" + grdbatch.Rows[i].Cells["Min.Sale"].Value + "','" + grdbatch.Rows[i].Cells["Op.Loose"].Value + "','" + grdbatch.Rows[i].Cells["Op. Stock(Val)"].Value + "','" + guid1 + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "','" + d + "','1')";
                                    SqlCommand cmd2 = new SqlCommand(sql1, con);
                                    cmd2.ExecuteNonQuery();

                                }





                                MessageBox.Show("Insert Data Successfully...");

                                clear();
                                if (gener == 1)
                                {
                                    try
                                    {
                                        // sale.autoreaderbind();
                                        process.binditem();
                                        process.bindproductionitem();
                                        process.bindrowitem();
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        // sale.autoreaderbind();
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        //saleReturn.autoreaderbind();
                                    }
                                    catch
                                    {

                                    }
                                    try
                                    {
                                        // purchase.autoreaderbind();
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        //  purchase_2.autoreaderbind();
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                       // defaultSale.autoreaderbind();
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                      //  defaultSaleOrder.autoreaderbind();
                                        defaultSaleOrder.txtitemname.Text = txtiname.Text;
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                      //  salePurchaseOrderSimpleformate.autoreaderbind();
                                        salePurchaseOrderSimpleformate.txtitemname.Text = txtiname.Text;
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        frmComplainMasterData.bindallitem();
                                        frmComplainMasterData.cmbItemName.Text = txtiname.Text;
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
                                        master.RemoveCurrentTab1(pvc, txtiname.Text);
                                    }
                                }
                                else
                                {
                                    this.ActiveControl = txtiname;
                                    //  master.RemoveCurrentTab();
                                    // Itemmaster dlg = new Itemmaster(master, tabControl);
                                    //  master.AddNewTab(dlg);
                                }
                                txtiname.Text = string.Empty;
                                con.Close();
                                //listviewbind();
                                //  btnsubmit.Text = "New";

                                // btnsubmit.Focus();
                                //  Itemmaster im = new Itemmaster();
                                // im.listviewbind();
                                //   master.RemoveCurrentTab();
                            }

                        }
                        else
                        {
                            MessageBox.Show("You don't have Permission To Submit");
                            return;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally
            {
                this.Enabled = true;
                con.Close();
            }
        }
        private void btnsubmit_Click(object sender, EventArgs e)
        {
            if (cmbtaxslab.SelectedIndex != -1)// || cmbcompany.SelectedIndex !=-1 || txtgroup.SelectedIndex !=-1 || txtpunit.SelectedIndex !=-1 || txtaunit.SelectedIndex !=-1)
            {
                if (cmbcompany.SelectedIndex != -1)
                {
                    if (txtgroup.SelectedIndex != -1)
                    {
                        if (txtpunit.SelectedIndex != -1)
                        {
                            if (txtaunit.SelectedIndex != -1)
                            {
                                if (!string.IsNullOrEmpty(txtitemnumber.Text))
                                {
                                    submit();
                                    this.ActiveControl = txtiname;
                                    txtiname.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("Enter Item Number");
                                    this.ActiveControl = txtitemnumber;
                                    txtitemnumber.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Select Alt.Unit");
                                this.ActiveControl = txtaunit;
                                txtaunit.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Select Primary Unit");
                            this.ActiveControl = txtpunit;
                            txtpunit.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select Item Group Name");
                        this.ActiveControl = txtgroup;
                        txtgroup.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Select Company Name");
                    this.ActiveControl = cmbcompany;
                    cmbcompany.Focus();
                }
            }
            else
            {
                MessageBox.Show("Select Tax Slab Value");
                this.ActiveControl = cmbtaxslab;
                cmbtaxslab.Focus();
            }
        }

        public void clear()
        {
            //  lvsaletype.Items.Clear();
            txtmaxstock.Text = "0";
            txtminstock.Text = "0";
            txtreorderqty.Text = "0";
            txtf1.Text = "";
            txtf2.Text = "";
            txtf3.Text = "";
            txtf4.Text = "";
            txtf5.Text = "";
            txtf6.Text = "";
            txtf7.Text = "";
            txtf8.Text = "";
            txtf9.Text = "";
            txtf10.Text = "";
            txtf11.Text = "";
            txtf12.Text = "";
            txtf13.Text = "";
            txtf14.Text = "";
            txtf15.Text = "";
            chkshare.Checked = false;
            chkhot.Checked = false;
            chkbatch.Checked = false;
            pic1.Image = null;
            txtgroup1.Text = string.Empty;
            txtpunit1.Text = string.Empty;
            cmbcompany.SelectedIndex = -1;
            txtpacking.Text = string.Empty;
            txtaunit1.Text = string.Empty;
            txtcfactor.Text = string.Empty;
            txthsn.Text = string.Empty;

            bindgaunit();
            bindgroup();
            bindgpunit();
            bindtaxslab();
            bindcompany();
            binditemprice();
            grdbatch.AllowUserToAddRows = false;
            DataRow dr = batch.NewRow();
            for (int i = 0; i < grdbatch.Rows.Count - 1; i++)
            {
                try
                {
                    int max = grdbatch.Rows.Count - 1;
                    grdbatch.Rows.Remove(grdbatch.Rows[max]);
                }
                catch
                {
                    MessageBox.Show("You can't delete A row ");
                }
            }


            grdbatch.Rows[0].Cells["batch"].Value = "NA";
            //dr["batch"] = "NA";
            grdbatch.Rows[0].Cells["Exp.Dt."].Value = "";
            grdbatch.Rows[0].Cells["Mfg.Dt."].Value = "";
            grdbatch.Rows[0].Cells["Exp.Days"].Value = "";
            grdbatch.Rows[0].Cells["Barcode"].Value = "";
            grdbatch.Rows[0].Cells["MRP"].Value = "0";
            grdbatch.Rows[0].Cells["Basic Price"].Value = "0";
            grdbatch.Rows[0].Cells["Sale Price"].Value = "0";
            grdbatch.Rows[0].Cells["Purchase Price"].Value = "0";
            grdbatch.Rows[0].Cells["Self Val"].Value = "0";
            grdbatch.Rows[0].Cells["Min.Sale"].Value = "0";
            grdbatch.Rows[0].Cells["Op.Packs"].Value = "0";
            grdbatch.Rows[0].Cells["Op.Loose"].Value = "0";
            grdbatch.Rows[0].Cells["Op. Stock(Val)"].Value = "0";
            getitemno();

        }
        //private void butadd_Click(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        ListViewItem li;
        //        //SqlCommand cmd2 = new SqlCommand("select max(id) from ItemTaxMaster", con);
        //        //SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
        //        //DataTable dt2 = new DataTable();
        //        //sda2.Fill(dt2);
        //        //li = lvsaletype.Items.Add(dt2.Rows[0].ItemArray[0].ToString());
        //        li = lvsaletype.Items.Add(cmsaletype.Text);
        //        li.SubItems.Add(txtvat.Text);
        //        li.SubItems.Add(txtaddvat.Text);
        //        cmsaletype.SelectedIndex = -1;
        //        txtvat.Text = string.Empty;
        //        txtaddvat.Text = string.Empty;

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error:" + ex.Message);
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}
        public void bindgroup()
        {
            SqlCommand cmd = new SqlCommand("select id,ItemGroupName from ItemGroupMaster where isactive=1", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            txtgroup.ValueMember = "id";
            txtgroup.DisplayMember = "ItemGroupName";
            txtgroup.DataSource = dt;
            txtgroup.SelectedIndex = -1;
        }
        public void bindgaunit()
        {
            SqlCommand cmd = new SqlCommand("select id,UnitName from UnitMaster where isactive=1", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            txtaunit.ValueMember = "id";
            txtaunit.DisplayMember = "UnitName";
            txtaunit.SelectedIndex = -1;
            txtaunit.DataSource = dt;

        }
        public void bindgpunit()
        {
            SqlCommand cmd = new SqlCommand("select id,UnitName from UnitMaster where isactive=1", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            txtpunit.ValueMember = "id";
            txtpunit.DisplayMember = "UnitName";
            txtpunit.SelectedIndex = -1;
            txtpunit.DataSource = dt;


        }
        public void autogroup()
        {
            SqlDataReader dReader;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = strConnection;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select distinct [GroupName] from [ProductMaster] where isactive=1 order by [GroupName] asc";
            conn.Open();
            dReader = cmd.ExecuteReader();
            if (dReader.HasRows == true)
            {
                while (dReader.Read())
                    namesCollection.Add(dReader["GroupName"].ToString());

            }
            else
            {
                //MessageBox.Show("Data not found");
            }
            dReader.Close();

            txtgroup1.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtgroup1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtgroup1.AutoCompleteCustomSource = namesCollection;
        }
        public void punit()
        {
            SqlDataReader dReader;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = strConnection;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select distinct [Unit] from [ProductMaster] where isactive=1 order by [Unit] asc";
            conn.Open();
            dReader = cmd.ExecuteReader();
            if (dReader.HasRows == true)
            {
                while (dReader.Read())
                    namesCollection.Add(dReader["Unit"].ToString());

            }
            else
            {
                //   MessageBox.Show("Data not found");
            }
            dReader.Close();

            txtpunit1.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtpunit1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtpunit1.AutoCompleteCustomSource = namesCollection;
        }
        public void aunit()
        {
            SqlDataReader dReader;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = strConnection;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select distinct [Altunit] from [ProductMaster] where isactive=1 order by [Altunit] asc";
            conn.Open();
            dReader = cmd.ExecuteReader();
            if (dReader.HasRows == true)
            {
                while (dReader.Read())
                    namesCollection.Add(dReader["Altunit"].ToString());

            }
            else
            {
                //MessageBox.Show("Data not found");
            }
            dReader.Close();

            txtaunit1.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtaunit1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtaunit1.AutoCompleteCustomSource = namesCollection;
        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void txtiname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                flag = 0;
                string str = txtiname.Text.ToUpper().Trim();
                SqlCommand cmd1 = new SqlCommand("select Product_Name from ProductMaster where isactive=1", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (txtiname.Text != "")
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string val = dt.Rows[i][0].ToString().ToUpper().Trim();
                            if (val == str)
                            {
                                MessageBox.Show("Item Already Available Please add Another");
                                txtiname.Focus();
                                flag = 1;
                                break;
                            }
                        }
                    }
                    else
                    {
                        //txtgroup.Focus();
                        txthsn.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Item Name cannot be Blank");
                    txtiname.Focus();
                    return;
                }
                if (flag == 0)
                {
                    //txtgroup.Focus();
                    txthsn.Focus();
                }
            }
        }

        private void txtgroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < txtgroup.Items.Count; i++)
                {
                    s = txtgroup.GetItemText(txtgroup.Items[i]);
                    if (s == txtgroup.Text)
                    {
                        inList = true;
                        txtgroup.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    txtgroup.Text = "";
                }

                // cmbcompany.Focus();
                txtpunit.Focus();
            }
            if (e.KeyCode == Keys.F3)
            {
                var privouscontroal = txtgroup;
                activecontroal = privouscontroal.Name;
                ItemGroup popup = new ItemGroup(this, tabControl, master, activecontroal);
                master.AddNewTab(popup);
            }
            if (e.KeyCode == Keys.F2)
            {
                if (txtgroup.Text != "" && txtgroup.Text != null)
                {
                    string group = txtgroup.SelectedValue.ToString();
                    if (group == " " || group == null)
                    {
                        MessageBox.Show("Select Group Name");
                    }
                    else
                    {
                        var privouscontroal = txtgroup;
                        activecontroal = privouscontroal.Name;
                        ItemGroup popup = new ItemGroup(this, tabControl, master, activecontroal);
                        popup.Update(group);
                        master.AddNewTab(popup);
                        //SqlCommand cmd5 = new SqlCommand("Select * from CompanyMaster where companyname ='"+company+"'", con);
                        //SqlDataAdapter sda = new SqlDataAdapter(cmd5);
                        //DataTable dt = new DataTable();
                        //sda.Fill(dt);
                    }
                }
            }
        }

        private void cmbcompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbcompany.Items.Count; i++)
                {
                    s = cmbcompany.GetItemText(cmbcompany.Items[i]);
                    if (s == cmbcompany.Text)
                    {
                        inList = true;
                        cmbcompany.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbcompany.Text = "";
                }
                // txtpunit.Focus();
                txtgroup.Focus();

            }
            if (e.KeyCode == Keys.F3)
            {
                var privouscontroal = cmbcompany;
                activecontroal = privouscontroal.Name;
                CompanyMaster popup = new CompanyMaster(this, tabControl, master, activecontroal);
                master.AddNewTab(popup);
                //popup.ShowDialog();



                //popup.Dispose();
            }
            if (e.KeyCode == Keys.F2)
            {
                if (cmbcompany.Text != "" && cmbcompany.Text != null)
                {
                    var privouscontroal = cmbcompany;
                    activecontroal = privouscontroal.Name;
                    string company = cmbcompany.SelectedValue.ToString();
                    if (company == " " || company == null)
                    {
                        MessageBox.Show("Select Company Name");
                    }
                    else
                    {
                        CompanyMaster popup = new CompanyMaster(this, tabControl, master, activecontroal);
                        popup.Update(company);
                        master.AddNewTab(popup);
                        //SqlCommand cmd5 = new SqlCommand("Select * from CompanyMaster where companyname ='"+company+"'", con);
                        //SqlDataAdapter sda = new SqlDataAdapter(cmd5);
                        //DataTable dt = new DataTable();
                        //sda.Fill(dt);
                    }
                }
            }
        }

        private void txtpunit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < txtpunit.Items.Count; i++)
                {
                    s = txtpunit.GetItemText(txtpunit.Items[i]);
                    if (s == txtpunit.Text)
                    {
                        inList = true;
                        txtpunit.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    txtpunit.Text = "";
                }

                // txtaunit1.Text = txtpunit1.Text;
                txtaunit.Focus();
                //txtcfactor.Text = "1";
                //txtpacking.Focus();
            }
            if (e.KeyCode == Keys.F3)
            {
                var privouscontroal = txtpunit;
                activecontroal = privouscontroal.Name;
                PrimaryUnit popup = new PrimaryUnit(this, tabControl, master, activecontroal);
                master.AddNewTab(popup);
            }
            if (e.KeyCode == Keys.F2)
            {
                var privouscontroal = txtpunit;
                activecontroal = privouscontroal.Name;
                string primaryunit = txtpunit.SelectedValue.ToString();
                PrimaryUnit popup = new PrimaryUnit(this, tabControl, master, activecontroal);
                popup.Update(primaryunit);
                master.AddNewTab(popup);
            }
        }

        private void txtpacking_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // txthsn.Focus();
                // txtcessper.Focus();
                chkbatch.Focus();
                chkbatch.ForeColor = Color.Green;
                addisnal();
            }
            else
            {
                chkbatch.ForeColor = Color.Black;
            }
            //if (e.KeyValue == 13)
            //{
            //    chkbatch.ForeColor = Color.Green;
            //}
        }

        private void txtbatchno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //  txtbarcode.Focus();
            }
        }

        private void txtbarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //  txtbasic.Focus();
            }
        }

        private void txtbasic_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // txtsale.Focus();
            }
        }

        private void txtsale_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //  txtmrp.Focus();
            }
        }

        private void txtmrp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //  txtPurchase.Focus();
            }
        }

        private void txtPurchase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //  txtopstock.Focus();
            }
        }

        private void txtopstock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkbatch.Focus();
            }
        }

        private void chkbatch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkbatch.ForeColor = Color.Black;
                chkhot.Focus();
                chkhot.ForeColor = Color.Green;
            }
            else
            {

            }
        }

        private void cmsaletype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //  txtvat.Focus();
            }
        }

        private void txtvat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //txtaddvat.Focus();
            }
        }

        private void txtaddvat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // butadd.Focus();
            }
        }

        public void Updatefromsale(string p)
        {
            userrights = cn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[10]["d"].ToString() == "False")
                {
                    btndelete.Enabled = false;
                }
            }
            SqlCommand cmd = new SqlCommand("select productid from productmaster where Product_Name='" + p + "' and isactive=1", con);
            con.Open();

            // pnlgst.Visible = false;
            //pnlvat.Visible = false;
            //    InitializeComponent();
            id = cmd.ExecuteScalar().ToString();
            con.Close();
            // SqlCommand cmd5 = new SqlCommand("select p.*,c.Companyname,pp.* from ProductMaster p inner join ProductPriceMaster pp on p.ProductID=pp.ProductID inner join companymaster c on c.companyid=p.companyid inner join ItemTaxMaster i on p.ProductID=i.ProductID where p.ProductID='" + id + "'", con);
            SqlCommand cmd5 = new SqlCommand("select p.*,pp.* from ProductMaster p inner join ProductPriceMaster pp on p.ProductID=pp.ProductID where p.Product_Name='" + p + "' and p.isactive=1 and pp.isactive=1", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd5);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            batch.Rows.Clear();
            options = cn.getdataset("select * from options");
            if (Convert.ToBoolean(options.Rows[0]["cess"].ToString()) == true)
            {
                lblcessper.Visible = true;
                lblcessamt.Visible = true;
                txtcessper.Visible = true;
                txtcessamt.Visible = true;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                txtitemnumber.Text = dt.Rows[i]["itemnumber"].ToString();
                txtcessper.Text = dt.Rows[i]["cessper"].ToString();
                txtcessamt.Text = dt.Rows[i]["cessamt"].ToString();
                DataRow dr = batch.NewRow();
                if (dt.Rows[i]["Batchno"].ToString() == "")
                {
                    dr["batch"] = "NA";
                }
                else
                {
                    dr["batch"] = dt.Rows[i]["Batchno"].ToString();
                }
                dr["Exp.Dt."] = dt.Rows[0]["ExpDt"].ToString();
                dr["Mfg.Dt."] = dt.Rows[0]["mfgdt"].ToString();
                dr["Exp.Days"] = dt.Rows[0]["Expdays"].ToString();
                dr["Barcode"] = dt.Rows[0]["Barcode"].ToString();
                dr["MRP"] = dt.Rows[0]["MRP"].ToString();
                dr["Basic Price"] = dt.Rows[0]["BasicPrice"].ToString();
                dr["Sale Price"] = dt.Rows[0]["SalePrice"].ToString();
                dr["Purchase Price"] = dt.Rows[0]["PurchasePrice"].ToString();
                dr["Self Val"] = dt.Rows[0]["SelfVal"].ToString();
                dr["Min.Sale"] = dt.Rows[i]["minsaleprice"].ToString();
                dr["Op.Packs"] = dt.Rows[0]["OpStock"].ToString();
                dr["Op.Loose"] = dt.Rows[0]["oploose"].ToString();
                dr["Op. Stock(Val)"] = dt.Rows[0]["opstockval"].ToString();

                batch.Rows.Add(dr);
            }
            string appPathpic = Path.GetDirectoryName(Application.ExecutablePath) + @"\Images\ItemImage\";
            try
            {
                pic1.Image = Image.FromFile(appPathpic + dt.Rows[0]["itemimage"].ToString());
            }
            catch { }
            photo1[1] = dt.Rows[0]["itemimage"].ToString();
            grdbatch.DataSource = batch;
            grdbatch.AllowUserToAddRows = false;
            txtiname.Text = dt.Rows[0]["Product_Name"].ToString();
            cmd5 = new SqlCommand("select companyname from companymaster where companyid='" + dt.Rows[0]["CompanyID"].ToString() + "'", con);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            cmbcompany.Text = cmd5.ExecuteScalar().ToString();
            con.Close();
            //   cmbcompany.SelectedValue = dt.Rows[0]["CompanyID"].ToString();
            txtgroup.Text = dt.Rows[0]["GroupName"].ToString();
            txtpunit.Text = dt.Rows[0]["Unit"].ToString();
            //txtbatchno.Text = dt.Rows[0]["Batchno"].ToString();
            //txtbasic.Text = dt.Rows[0]["BasicPrice"].ToString();
            //txtsale.Text = dt.Rows[0]["SalePrice"].ToString();
            //txtmrp.Text = dt.Rows[0]["MRP"].ToString();
            //txtPurchase.Text = dt.Rows[0]["PurchasePrice"].ToString();
            //txtbarcode.Text = dt.Rows[0]["Barcode"].ToString();
            //txtopstock.Text = dt.Rows[0]["OpStock"].ToString();
            txtminstock.Text = dt.Rows[0]["minstock"].ToString();
            txtmaxstock.Text = dt.Rows[0]["maxstock"].ToString();
            txtreorderqty.Text = dt.Rows[0]["reorderqty"].ToString();
            txtcfactor.Text = dt.Rows[0]["Convfactor"].ToString();
            txtaunit.Text = dt.Rows[0]["Altunit"].ToString();
            txtpacking.Text = dt.Rows[0]["Packing"].ToString();
            txthsn.Text = dt.Rows[0]["Hsn_Sac_Code"].ToString();
            txtf1.Text = dt.Rows[0]["OT1"].ToString();
            txtf2.Text = dt.Rows[0]["OT2"].ToString();
            txtf3.Text = dt.Rows[0]["OT3"].ToString();
            txtf4.Text = dt.Rows[0]["OT4"].ToString();
            txtf5.Text = dt.Rows[0]["OT5"].ToString();
            txtf6.Text = dt.Rows[0]["OT6"].ToString();
            txtf7.Text = dt.Rows[0]["OT7"].ToString();
            txtf8.Text = dt.Rows[0]["OT8"].ToString();
            txtf9.Text = dt.Rows[0]["OT9"].ToString();
            txtf10.Text = dt.Rows[0]["OT10"].ToString();
            txtf11.Text = dt.Rows[0]["OT11"].ToString();
            txtf12.Text = dt.Rows[0]["OT12"].ToString();
            txtf13.Text = dt.Rows[0]["OT13"].ToString();
            txtf14.Text = dt.Rows[0]["OT14"].ToString();
            txtf15.Text = dt.Rows[0]["OT15"].ToString();
            if (dt.Rows[0]["IsBatch"].ToString() == "True")
            {
                chkbatch.Checked = true;
            }
            else
            {
                chkbatch.Checked = false;
            }
            if (dt.Rows[0]["isserial"].ToString() == "True")
            {
                chkserial.Checked = true;
            }
            else
            {
                chkserial.Checked = false;
            }
            if (dt.Rows[0]["isHotProduct"].ToString() == "True")
            {
                chkhot.Checked = true;
            }
            else
            {
                chkhot.Checked = false;
            }
            if (dt.Rows[0]["isshare"].ToString() == "True")
            {
                chkshare.Checked = true;
            }
            else
            {
                chkshare.Checked = false;
            }
            cmbtaxslab.Text = dt.Rows[0]["taxslab"].ToString();
            //cmsaletype.Text = dt.Rows[0]["saletypeid"].ToString();
            //txtvat.Text = dt.Rows[0]["Vat"].ToString();
            //txtaddvat.Text = dt.Rows[0]["AddVat"].ToString();
            //SqlCommand cmd1 = new SqlCommand("select saletypeid,vat,addvat from ItemTaxMaster where productid='" + id + "'", con);
            //SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            //DataTable dt1 = new DataTable();
            //sda1.Fill(dt1);
            //for (int i = 0; i <= dt1.Rows.Count - 1; i++)
            //{
            //    flag = 1;
            //    lvsaletype.Items.Add(dt1.Rows[i].ItemArray[0].ToString());
            //    lvsaletype.Items[i].SubItems.Add(dt1.Rows[i].ItemArray[1].ToString());
            //    lvsaletype.Items[i].SubItems.Add(dt1.Rows[i].ItemArray[2].ToString());



            //}
            //string system = "Select System from ItemTaxMaster  where productid='" + id + "'";
            //SqlCommand cmd4 = new SqlCommand(system, con);
            //SqlDataAdapter sda2 = new SqlDataAdapter(cmd4);
            //DataTable system1 = new DataTable();
            //sda2.Fill(system1);
            //try
            //{
            //    ddlsystem.Text = system1.Rows[0]["System"].ToString();
            //}
            //catch
            //{
            //    ddlsystem.Text = "VAT";
            //}
            //if (ddlsystem.Text == "GST")
            //{
            //    pnlgst.Visible = true;
            //    SqlCommand cmd1 = new SqlCommand("select saletypeid,System,Category,onwhich,sgst,cgst,igst,Additax,isonmrp,isonfreegoods from ItemTaxMaster where productid='" + id + "'", con);
            //    SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            //    DataTable dt1 = new DataTable();
            //    sda1.Fill(dt1);
            //    for (int i = 0; i <= dt1.Rows.Count - 1; i++)
            //    {
            //        flag = 1;
            //        lvgst.Items.Add(dt1.Rows[i].ItemArray[0].ToString());
            //        lvgst.Items[i].SubItems.Add(dt1.Rows[i].ItemArray[1].ToString());
            //        lvgst.Items[i].SubItems.Add(dt1.Rows[i].ItemArray[2].ToString());
            //        lvgst.Items[i].SubItems.Add(dt1.Rows[i].ItemArray[3].ToString());
            //        lvgst.Items[i].SubItems.Add(dt1.Rows[i].ItemArray[4].ToString());
            //        lvgst.Items[i].SubItems.Add(dt1.Rows[i].ItemArray[5].ToString());
            //        lvgst.Items[i].SubItems.Add(dt1.Rows[i].ItemArray[6].ToString());
            //        lvgst.Items[i].SubItems.Add(dt1.Rows[i].ItemArray[7].ToString());
            //        lvgst.Items[i].SubItems.Add(dt1.Rows[i].ItemArray[8].ToString());
            //        lvgst.Items[i].SubItems.Add(dt1.Rows[i].ItemArray[9].ToString());
            //        //if (dt1.Rows[i].ItemArray[8].ToString() == "True")
            //        //{
            //        //    chkonmrp.Checked = true;
            //        //}
            //        //else
            //        //{
            //        //    chkonmrp.Checked = false;
            //        //}
            //        //if (dt1.Rows[i].ItemArray[9].ToString() == "True")
            //        //{
            //        //    chkfree.Checked = true;
            //        //}
            //        //else
            //        //{
            //        //    chkfree.Checked = false;
            //        //}



            //    }
            //}
            //else
            //{
            //    pnlvat.Visible = true;
            //    SqlCommand cmd1 = new SqlCommand("select saletypeid,vat,addvat from ItemTaxMaster where productid='" + id + "'", con);
            //    SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            //    DataTable dt1 = new DataTable();
            //    sda1.Fill(dt1);
            //    for (int i = 0; i <= dt1.Rows.Count - 1; i++)
            //    {
            //        flag = 1;
            //        lvsaletype.Items.Add(dt1.Rows[i].ItemArray[0].ToString());
            //        lvsaletype.Items[i].SubItems.Add(dt1.Rows[i].ItemArray[1].ToString());
            //        lvsaletype.Items[i].SubItems.Add(dt1.Rows[i].ItemArray[2].ToString());
            //    }
            //}
            //  DataTable options = cn.getdataset("select * from options");
            if (options.Rows[0]["itembillno"].ToString() == "Continuous")
            {
                txtitemnumber.ReadOnly = true;
                this.ActiveControl = txtiname;
            }
            else
            {
                txtitemnumber.ReadOnly = false;
                this.ActiveControl = txtitemnumber;
            }
            btnsubmit.Text = "Update";



        }


        public void Update(int p)
        {
            try
            {
                if (Itemmaster.iid != "")
                {
                    userrights = cn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[10]["d"].ToString() == "False")
                        {
                            btndelete.Enabled = false;
                        }
                    }
                    //    InitializeComponent();
                    id = Itemmaster.iid;
                    // SqlCommand cmd5 = new SqlCommand("select p.*,c.Companyname,pp.* from ProductMaster p inner join ProductPriceMaster pp on p.ProductID=pp.ProductID inner join companymaster c on c.companyid=p.companyid inner join ItemTaxMaster i on p.ProductID=i.ProductID where p.ProductID='" + id + "'", con);
                    SqlCommand cmd5 = new SqlCommand("select p.*,pp.* from ProductMaster p inner join ProductPriceMaster pp on p.ProductID=pp.ProductID and pp.isactive=1  where p.ProductID='" + id + "' and p.isactive=1", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd5);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    batch.Rows.Clear();
                    options = cn.getdataset("select * from options");
                    if (Convert.ToBoolean(options.Rows[0]["cess"].ToString()) == true)
                    {
                        lblcessper.Visible = true;
                        lblcessamt.Visible = true;
                        txtcessper.Visible = true;
                        txtcessamt.Visible = true;

                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        txtitemnumber.Text = dt.Rows[i]["itemnumber"].ToString();
                        txtcessper.Text = dt.Rows[i]["cessper"].ToString();
                        txtcessamt.Text = dt.Rows[i]["cessamt"].ToString();
                        DataRow dr = batch.NewRow();
                        if (dt.Rows[i]["Batchno"].ToString() == "")
                        {
                            dr["batch"] = "NA";
                        }
                        else
                        {
                            dr["batch"] = dt.Rows[i]["Batchno"].ToString();
                        }
                        dr["Exp.Dt."] = dt.Rows[i]["ExpDt"].ToString();
                        dr["Mfg.Dt."] = dt.Rows[i]["mfgdt"].ToString();
                        dr["Exp.Days"] = dt.Rows[i]["Expdays"].ToString();
                        dr["Barcode"] = dt.Rows[i]["Barcode"].ToString();
                        dr["MRP"] = dt.Rows[i]["MRP"].ToString();
                        dr["Basic Price"] = dt.Rows[i]["BasicPrice"].ToString();
                        dr["Sale Price"] = dt.Rows[i]["SalePrice"].ToString();
                        dr["Purchase Price"] = dt.Rows[i]["PurchasePrice"].ToString();
                        dr["Self Val"] = dt.Rows[i]["SelfVal"].ToString();
                        dr["Min.Sale"] = dt.Rows[i]["minsaleprice"].ToString();
                        dr["Op.Packs"] = dt.Rows[i]["OpStock"].ToString();
                        dr["Op.Loose"] = dt.Rows[i]["oploose"].ToString();
                        dr["Op. Stock(Val)"] = dt.Rows[i]["opstockval"].ToString();

                        batch.Rows.Add(dr);
                    }
                    string appPathpic = Path.GetDirectoryName(Application.ExecutablePath) + @"\Images\ItemImage\";
                    try
                    {
                        pic1.Image = Image.FromFile(appPathpic + dt.Rows[0]["itemimage"].ToString());
                    }
                    catch { }
                    photo1[1] = dt.Rows[0]["itemimage"].ToString();
                    grdbatch.DataSource = batch;
                    grdbatch.AllowUserToAddRows = false;
                    txtiname.Text = dt.Rows[0]["Product_Name"].ToString();
                    cmd5 = new SqlCommand("select companyname from companymaster where companyid='" + dt.Rows[0]["CompanyID"].ToString() + "'", con);
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    cmbcompany.Text = cmd5.ExecuteScalar().ToString();
                    con.Close();
                    //cmbcompany.SelectedValue = dt.Rows[0]["CompanyID"].ToString();
                    txtgroup.Text = dt.Rows[0]["GroupName"].ToString();
                    txtpunit.Text = dt.Rows[0]["Unit"].ToString();
                    //txtbatchno.Text = dt.Rows[0]["Batchno"].ToString();
                    //txtbasic.Text = dt.Rows[0]["BasicPrice"].ToString();
                    //txtsale.Text = dt.Rows[0]["SalePrice"].ToString();
                    //txtmrp.Text = dt.Rows[0]["MRP"].ToString();
                    //txtPurchase.Text = dt.Rows[0]["PurchasePrice"].ToString();
                    //txtbarcode.Text = dt.Rows[0]["Barcode"].ToString();
                    //txtopstock.Text = dt.Rows[0]["OpStock"].ToString();
                    txtcfactor.Text = dt.Rows[0]["Convfactor"].ToString();
                    txtaunit.Text = dt.Rows[0]["Altunit"].ToString();
                    txtpacking.Text = dt.Rows[0]["Packing"].ToString();
                    txthsn.Text = dt.Rows[0]["Hsn_Sac_Code"].ToString();
                    txtminstock.Text = dt.Rows[0]["minstock"].ToString();
                    txtmaxstock.Text = dt.Rows[0]["maxstock"].ToString();
                    txtreorderqty.Text = dt.Rows[0]["reorderqty"].ToString();
                    txtf1.Text = dt.Rows[0]["OT1"].ToString();
                    txtf2.Text = dt.Rows[0]["OT2"].ToString();
                    txtf3.Text = dt.Rows[0]["OT3"].ToString();
                    txtf4.Text = dt.Rows[0]["OT4"].ToString();
                    txtf5.Text = dt.Rows[0]["OT5"].ToString();
                    txtf6.Text = dt.Rows[0]["OT6"].ToString();
                    txtf7.Text = dt.Rows[0]["OT7"].ToString();
                    txtf8.Text = dt.Rows[0]["OT8"].ToString();
                    txtf9.Text = dt.Rows[0]["OT9"].ToString();
                    txtf10.Text = dt.Rows[0]["OT10"].ToString();
                    txtf11.Text = dt.Rows[0]["OT11"].ToString();
                    txtf12.Text = dt.Rows[0]["OT12"].ToString();
                    txtf13.Text = dt.Rows[0]["OT13"].ToString();
                    txtf14.Text = dt.Rows[0]["OT14"].ToString();
                    txtf15.Text = dt.Rows[0]["OT15"].ToString();
                    if (dt.Rows[0]["IsBatch"].ToString() == "True")
                    {
                        chkbatch.Checked = true;
                    }
                    else
                    {
                        chkbatch.Checked = false;
                    }
                    if (dt.Rows[0]["isserial"].ToString() == "True")
                    {
                        chkserial.Checked = true;
                    }
                    else
                    {
                        chkserial.Checked = false;
                    }
                    if (dt.Rows[0]["isHotProduct"].ToString() == "True")
                    {
                        chkhot.Checked = true;
                    }
                    else
                    {
                        chkhot.Checked = false;
                    }
                    if (dt.Rows[0]["isshare"].ToString() == "True")
                    {
                        chkshare.Checked = true;
                    }
                    else
                    {
                        chkshare.Checked = false;
                    }
                    cmbtaxslab.Text = dt.Rows[0]["taxslab"].ToString();
                    //cmsaletype.Text = dt.Rows[0]["saletypeid"].ToString();
                    //txtvat.Text = dt.Rows[0]["Vat"].ToString();
                    //txtaddvat.Text = dt.Rows[0]["AddVat"].ToString();
                    //SqlCommand cmd1 = new SqlCommand("select saletypeid,vat,addvat from ItemTaxMaster where productid='" + id + "'", con);
                    //SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    //DataTable dt1 = new DataTable();
                    //sda1.Fill(dt1);
                    //for (int i = 0; i <= dt1.Rows.Count - 1; i++)
                    //{
                    //    flag = 1;
                    //    lvsaletype.Items.Add(dt1.Rows[i].ItemArray[0].ToString());
                    //    lvsaletype.Items[i].SubItems.Add(dt1.Rows[i].ItemArray[1].ToString());
                    //    lvsaletype.Items[i].SubItems.Add(dt1.Rows[i].ItemArray[2].ToString());



                    //}
                    //    string system = "Select System from ItemTaxMaster  where isactive=1 and productid='" + id + "'";
                    //    SqlCommand cmd = new SqlCommand(system, con);
                    //    SqlDataAdapter sda2 = new SqlDataAdapter(cmd);
                    //    DataTable system1 = new DataTable();
                    //    sda2.Fill(system1);
                    //    if (system1.Rows.Count > 0)
                    //    {
                    //        ddlsystem.Text = system1.Rows[0]["System"].ToString();
                    //        if (system1.Rows[0]["System"].ToString() == "GST")
                    //        {
                    //            pnlgst.Visible = true;
                    //            SqlCommand cmd1 = new SqlCommand("select saletypeid,System,Category,onwhich,sgst,cgst,igst,Additax,isonmrp,isonfreegoods from ItemTaxMaster where isactive=1 and productid='" + id + "'", con);
                    //            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    //            DataTable dt1 = new DataTable();
                    //            sda1.Fill(dt1);
                    //            for (int i = 0; i <= dt1.Rows.Count - 1; i++)
                    //            {
                    //                flag = 1;
                    //                lvgst.Items.Add(dt1.Rows[i].ItemArray[0].ToString());
                    //                lvgst.Items[i].SubItems.Add(dt1.Rows[i].ItemArray[1].ToString());
                    //                lvgst.Items[i].SubItems.Add(dt1.Rows[i].ItemArray[2].ToString());
                    //                lvgst.Items[i].SubItems.Add(dt1.Rows[i].ItemArray[3].ToString());
                    //                lvgst.Items[i].SubItems.Add(dt1.Rows[i].ItemArray[4].ToString());
                    //                lvgst.Items[i].SubItems.Add(dt1.Rows[i].ItemArray[5].ToString());
                    //                lvgst.Items[i].SubItems.Add(dt1.Rows[i].ItemArray[6].ToString());
                    //                lvgst.Items[i].SubItems.Add(dt1.Rows[i].ItemArray[7].ToString());
                    //                lvgst.Items[i].SubItems.Add(dt1.Rows[i].ItemArray[8].ToString());
                    //                lvgst.Items[i].SubItems.Add(dt1.Rows[i].ItemArray[9].ToString());
                    //                //if (dt1.Rows[i].ItemArray[8].ToString() == "True")
                    //                //{
                    //                //    chkonmrp.Checked = true;
                    //                //}
                    //                //else
                    //                //{
                    //                //    chkonmrp.Checked = false;
                    //                //}
                    //                //if (dt1.Rows[i].ItemArray[9].ToString() == "True")
                    //                //{
                    //                //    chkfree.Checked = true;
                    //                //}
                    //                //else
                    //                //{
                    //                //    chkfree.Checked = false;
                    //                //}



                    //            }
                    //        }
                    //        else
                    //        {
                    //            pnlvat.Visible = true;
                    //            SqlCommand cmd1 = new SqlCommand("select saletypeid,vat,addvat from ItemTaxMaster where isactive=1 and productid='" + id + "'", con);
                    //            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    //            DataTable dt1 = new DataTable();
                    //            sda1.Fill(dt1);
                    //            for (int i = 0; i <= dt1.Rows.Count - 1; i++)
                    //            {
                    //                flag = 1;
                    //                lvsaletype.Items.Add(dt1.Rows[i].ItemArray[0].ToString());
                    //                lvsaletype.Items[i].SubItems.Add(dt1.Rows[i].ItemArray[1].ToString());
                    //                lvsaletype.Items[i].SubItems.Add(dt1.Rows[i].ItemArray[2].ToString());



                    //            }
                    //        }
                    //    }
                    // DataTable options = cn.getdataset("select * from options");
                    if (options.Rows[0]["itembillno"].ToString() == "Continuous")
                    {
                        txtitemnumber.ReadOnly = true;
                        this.ActiveControl = txtiname;
                    }
                    else
                    {
                        txtitemnumber.ReadOnly = false;
                        this.ActiveControl = txtitemnumber;
                    }
                    btnsubmit.Text = "Update";

                }
            }
            catch
            {
            }
        }

        internal void Passed(int p)
        {
            gener = p;
        }

        private void grdbatch_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (grdbatch.CurrentCell.ColumnIndex == 5 || grdbatch.CurrentCell.ColumnIndex == 6 || grdbatch.CurrentCell.ColumnIndex == 7 || grdbatch.CurrentCell.ColumnIndex == 8 || grdbatch.CurrentCell.ColumnIndex == 9 || grdbatch.CurrentCell.ColumnIndex == 10 || grdbatch.CurrentCell.ColumnIndex == 11 || grdbatch.CurrentCell.ColumnIndex == 12 || grdbatch.CurrentCell.ColumnIndex == 13) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
        }
        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)
            && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void grdbatch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                int columnindex = grdbatch.CurrentCell.ColumnIndex;
                int rowindex = grdbatch.CurrentCell.RowIndex;
                if (columnindex == 1 || columnindex == 2)
                {
                    try
                    {
                        grdbatch.Rows[rowindex].Cells[columnindex].Value = Convert.ToDateTime(grdbatch.Rows[rowindex].Cells[columnindex].Value).ToString("MM-yyyy");
                    }
                    catch
                    {
                        MessageBox.Show("Required Date Formate here");
                    }
                }

                if (columnindex < grdbatch.ColumnCount - 2)
                {
                    if (columnindex >= 5 && columnindex <= 13)
                    {
                        int colindex = Convert.ToInt32(columnindex);

                        for (int h = 0; h < grdbatch.Rows.Count; h++)
                        {
                            for (int k = 5; k < grdbatch.Columns.Count - 1; k++)
                            {
                                if ((grdbatch.Rows[h].Cells[k].Value.ToString() != "") && (grdbatch.Rows[h].Cells[k].Value.ToString() != null))
                                {
                                    grdbatch.CurrentCell = grdbatch.Rows[rowindex].Cells[columnindex + 1];

                                }
                                else
                                {
                                    grdbatch.Rows[h].Cells[k].Value = 0;
                                    grdbatch.CurrentCell = grdbatch.Rows[rowindex].Cells[columnindex + 1];
                                    //MessageBox.Show("Enter Prise");
                                    // break;
                                }
                            }
                        }
                    }
                    else
                    {
                        grdbatch.CurrentCell = grdbatch.Rows[rowindex].Cells[columnindex + 1];
                    }

                    //   grdbatch.CurrentCell = grdbatch.Rows[rowindex].Cells[columnindex + 1];
                }
                else
                {
                    if (chkbatch.Checked == true)
                    {

                        if (columnindex >= 5 && columnindex <= 13)
                        {
                            int colindex = Convert.ToInt32(columnindex);

                            for (int h = 0; h < grdbatch.Rows.Count; h++)
                            {
                                for (int k = 5; k < grdbatch.Columns.Count - 1; k++)
                                {
                                    if ((grdbatch.Rows[h].Cells[k].Value.ToString() != "") && (grdbatch.Rows[h].Cells[k].Value.ToString() != null))
                                    {
                                        grdbatch.CurrentCell = grdbatch.Rows[rowindex].Cells[columnindex + 1];

                                    }
                                    else
                                    {
                                        grdbatch.Rows[h].Cells[k].Value = 0;
                                        grdbatch.CurrentCell = grdbatch.Rows[rowindex].Cells[columnindex + 1];
                                        //MessageBox.Show("Enter Prise");
                                        // break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            grdbatch.CurrentCell = grdbatch.Rows[rowindex].Cells[columnindex + 1];
                        }
                        columnindex = 0;
                        //   grdbatch.AllowUserToAddRows = true;
                        DataRow dr = batch.NewRow();
                        dr["batch"] = "NA";
                        dr["Exp.Dt."] = "";
                        dr["Mfg.Dt."] = "";
                        dr["Exp.Days"] = "";
                        dr["Barcode"] = "";
                        dr["MRP"] = "0";
                        dr["Basic Price"] = "0";
                        dr["Sale Price"] = "0";
                        dr["Purchase Price"] = "0";
                        dr["Self Val"] = "0";
                        dr["Min.Sale"] = "0";
                        dr["Op.Packs"] = "0";
                        dr["Op.Loose"] = "0";
                        dr["Op. Stock(Val)"] = "0";
                        batch.Rows.Add(dr);
                        grdbatch.DataSource = batch;
                        //   grdbatch.Rows.AddCopy(grdbatch.Rows.Count - 1); 
                        grdbatch.CurrentCell = grdbatch.Rows[rowindex + 1].Cells[columnindex];
                    }
                    else
                    {
                        for (int h = 0; h < grdbatch.Rows.Count; h++)
                        {
                            for (int k = 5; k < grdbatch.Columns.Count - 1; k++)
                            {
                                if ((grdbatch.Rows[h].Cells[k].Value.ToString() != "") && (grdbatch.Rows[h].Cells[k].Value.ToString() != null))
                                {
                                    grdbatch.CurrentCell = grdbatch.Rows[rowindex].Cells[columnindex + 1];

                                }
                                else
                                {
                                    grdbatch.Rows[h].Cells[k].Value = 0;
                                    // grdbatch.CurrentCell = grdbatch.Rows[rowindex].Cells[columnindex + 1];
                                    //MessageBox.Show("Enter Prise");
                                    // break;
                                }
                            }
                        }
                        btnsubmit.Focus();
                    }

                }

            }
        }

        private void grdbatch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == grdbatch.Columns["btndelete"].Index)
            {
                if (grdbatch.Rows.Count > 1)
                {
                    grdbatch.Rows.RemoveAt(e.RowIndex);
                }
                else
                {
                    MessageBox.Show("At least a single Batch is Require");
                }
            }
        }

        private void txtcfactor_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                {

                    if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 45 || e.KeyChar == 8)
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
            catch
            {
            }
        }

        private void txtcfactor_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (Convert.ToDouble(txtcfactor.Text) == 0)
                {
                    txtcfactor.Text = "1";
                }
                if (e.KeyCode == Keys.Enter)
                {
                    txtpacking.Focus();
                }
            }
            catch
            {
            }
        }







        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Delete Item?", "Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    if (id == "" || id == null)
                    {
                        MessageBox.Show("Select Item");
                    }
                    else
                    {
                        cn.execute("Update productmaster set isactive=0 where productid=" + id);
                        cn.execute("update productpricemaster set isactive=0 where productid=" + id);
                        //  cn.execute("update itemtaxmaster set isactive=0 where productid=" + id);
                        MessageBox.Show("Delete Successfully");
                        master.RemoveCurrentTab();
                    }
                }
            }
            catch
            {
            }
        }



        private void txthsn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //chkbatch.Focus();
                cmbtaxslab.Focus();
            }
        }









        private void chkserial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtcessper.Visible == true)
                {
                    txtcessper.Focus();
                }
                else
                {
                    chkserial.ForeColor = Color.Black;
                    grdbatch.Focus();
                }
            }
        }

        private void cmbcompany_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbcompany.SelectedIndex = 0;
                cmbcompany.DroppedDown = true;
            }
            catch
            {
            }
        }



        private void txtiname_Enter(object sender, EventArgs e)
        {
            txtiname.BackColor = Color.LightYellow;
        }

        private void txtiname_Leave(object sender, EventArgs e)
        {
            txtiname.BackColor = Color.White;
        }

        private void txtgroup_Enter(object sender, EventArgs e)
        {
            try
            {
                txtgroup.SelectedIndex = 0;
                txtgroup.DroppedDown = true;
            }
            catch
            {
            }
        }


        private void txtcessper_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                txtcessamt.Focus();
            }
        }

        private void txtcessamt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                grdbatch.Focus();
                //  chklisttaxtype.Focus();
            }
        }

        private void txtgroup_Leave(object sender, EventArgs e)
        {
            txtgroup.Text = s;
            //  txtgroup1.BackColor = Color.White;
        }

        private void txtpunit_Enter(object sender, EventArgs e)
        {
            try
            {
                txtpunit.SelectedIndex = 0;
                txtpunit.DroppedDown = true;
            }
            catch
            {
            }
        }

        private void txtpunit_Leave(object sender, EventArgs e)
        {
            txtpunit.Text = s;
            //txtpunit1.BackColor = Color.White;
        }

        private void txtaunit_Enter(object sender, EventArgs e)
        {
            try
            {
                txtaunit.SelectedIndex = 0;
                txtaunit.DroppedDown = true;
            }
            catch
            {
            }
        }

        private void txtaunit_Leave(object sender, EventArgs e)
        {
            txtaunit.Text = s;
            // txtaunit1.BackColor = Color.White;
        }

        private void txtcfactor_Enter(object sender, EventArgs e)
        {
            txtcfactor.BackColor = Color.LightYellow;
        }

        private void txtcfactor_Leave(object sender, EventArgs e)
        {
            txtcfactor.BackColor = Color.White;
        }

        private void txtpacking_Enter(object sender, EventArgs e)
        {
            txtpacking.BackColor = Color.LightYellow;
        }

        private void txtpacking_Leave(object sender, EventArgs e)
        {
            txtpacking.BackColor = Color.White;
        }

        private void txthsn_Enter(object sender, EventArgs e)
        {
            txthsn.BackColor = Color.LightYellow;
        }

        private void txthsn_Leave(object sender, EventArgs e)
        {
            txthsn.BackColor = Color.White;
        }

        private void txtcessper_Enter(object sender, EventArgs e)
        {
            txtcessper.BackColor = Color.LightYellow;
        }

        private void txtcessper_Leave(object sender, EventArgs e)
        {
            txtcessper.BackColor = Color.White;
        }

        private void txtcessamt_Enter(object sender, EventArgs e)
        {
            txtcessamt.BackColor = Color.LightYellow;
        }

        private void txtcessamt_Leave(object sender, EventArgs e)
        {
            txtcessamt.BackColor = Color.White;
        }

        private void txtaunit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < txtaunit.Items.Count; i++)
                {
                    s = txtaunit.GetItemText(txtaunit.Items[i]);
                    if (s == txtaunit.Text)
                    {
                        inList = true;
                        txtaunit.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    txtaunit.Text = "";
                }

                txtcfactor.Text = "1";
                // txtpacking.Focus();
                txtcfactor.Focus();
            }
            if (e.KeyCode == Keys.F3)
            {
                var privouscontroal = txtaunit;
                activecontroal = privouscontroal.Name;
                PrimaryUnit popup = new PrimaryUnit(this, tabControl, master, activecontroal);
                master.AddNewTab(popup);
            }
            if (e.KeyCode == Keys.F2)
            {
                var privouscontroal = txtaunit;
                activecontroal = privouscontroal.Name;
                string primaryunit = txtaunit.SelectedValue.ToString();
                PrimaryUnit popup = new PrimaryUnit(this, tabControl, master, activecontroal);
                popup.Update(primaryunit);
                master.AddNewTab(popup);

                //PrimaryUnit popup = new PrimaryUnit(this, tabControl, master);

                //master.AddNewTab(popup);
            }
        }
        public static string s;
        private void cmbtaxslab_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                var privouscontroal = cmbtaxslab;
                activecontroal = privouscontroal.Name;
                TaxSlab popup = new TaxSlab(this, tabControl, master, activecontroal);
                master.AddNewTab(popup);
            }
            if (e.KeyCode == Keys.F2)
            {
                var privouscontroal = cmbtaxslab;
                activecontroal = privouscontroal.Name;
                TaxSlab popup = new TaxSlab(this, tabControl, master, activecontroal);
                string taxslab = cmbtaxslab.Text;
                popup.Update(taxslab);
                master.AddNewTab(popup);
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbtaxslab.Items.Count; i++)
                {
                    s = cmbtaxslab.GetItemText(cmbtaxslab.Items[i]);
                    if (s == cmbtaxslab.Text)
                    {
                        inList = true;
                        cmbtaxslab.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbtaxslab.Text = "";
                }
                cmbcompany.Focus();
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void txtaunit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < txtaunit.Items.Count; i++)
                {
                    s = txtaunit.GetItemText(txtaunit.Items[i]);
                    if (s == txtaunit.Text)
                    {
                        inList = true;
                        txtaunit.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    txtaunit.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void txtpacking_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtcessper_TextChanged(object sender, EventArgs e)
        {

        }

        private void grdbatch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbtaxslab_Enter(object sender, EventArgs e)
        {
            cmbtaxslab.SelectedIndex = 0;
            cmbtaxslab.DroppedDown = true;
        }

        private void btnsubmit_MouseEnter(object sender, EventArgs e)
        {
            btnsubmit.UseVisualStyleBackColor = false;
            btnsubmit.BackColor = Color.YellowGreen;
            btnsubmit.ForeColor = Color.White;
        }

        private void btnsubmit_MouseLeave(object sender, EventArgs e)
        {
            btnsubmit.UseVisualStyleBackColor = true;
            btnsubmit.BackColor = Color.FromArgb(51, 153, 255);
            btnsubmit.ForeColor = Color.White;
        }

        private void btndelete_MouseEnter(object sender, EventArgs e)
        {
            btndelete.UseVisualStyleBackColor = false;
            btndelete.BackColor = Color.FromArgb(255, 77, 77);
            btndelete.ForeColor = Color.White;
        }

        private void btndelete_MouseLeave(object sender, EventArgs e)
        {
            btndelete.UseVisualStyleBackColor = true;
            btndelete.BackColor = Color.FromArgb(51, 153, 255);
            btndelete.ForeColor = Color.White;
        }

        private void btncancel_MouseEnter(object sender, EventArgs e)
        {
            btncancel.UseVisualStyleBackColor = false;
            btncancel.BackColor = Color.FromArgb(248, 152, 94);
            btncancel.ForeColor = Color.White;
        }

        private void btncancel_MouseLeave(object sender, EventArgs e)
        {
            btncancel.UseVisualStyleBackColor = true;
            btncancel.BackColor = Color.FromArgb(51, 153, 255);
            btncancel.ForeColor = Color.White;
        }

        private void btnsubmit_Enter(object sender, EventArgs e)
        {
            btnsubmit.UseVisualStyleBackColor = false;
            btnsubmit.BackColor = Color.YellowGreen;
            btnsubmit.ForeColor = Color.White;
        }

        private void btnsubmit_Leave(object sender, EventArgs e)
        {
            btnsubmit.UseVisualStyleBackColor = true;
            btnsubmit.BackColor = Color.FromArgb(51, 153, 255);
            btnsubmit.ForeColor = Color.White;
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
        string searchstr;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //empty the string for every 1 seconds
            //  searchstr = "";
        }

        private void cmbtaxslab_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbtaxslab.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbtaxslab.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void cmbcompany_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbcompany.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbcompany.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void txtgroup_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = txtgroup.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            txtgroup.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void txtpunit_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = txtpunit.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            txtpunit.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void txtaunit_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = txtaunit.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            txtaunit.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void chkserial_Enter(object sender, EventArgs e)
        {
            chkserial.ForeColor = Color.Green;
            //  grdbatch.Focus();
        }

        private void btnNewCompany_Click(object sender, EventArgs e)
        {
            var privouscontroal = cmbcompany;
            activecontroal = privouscontroal.Name;
            CompanyMaster popup = new CompanyMaster(this, tabControl, master, activecontroal);
            master.AddNewTab(popup);
        }

        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            var privouscontroal = txtgroup;
            activecontroal = privouscontroal.Name;
            ItemGroup popup = new ItemGroup(this, tabControl, master, activecontroal);
            master.AddNewTab(popup);
        }

        private void btnCompanyEdit_Click(object sender, EventArgs e)
        {
            if (cmbcompany.Text != "" && cmbcompany.Text != null)
            {
                var privouscontroal = cmbcompany;
                activecontroal = privouscontroal.Name;
                string company = cmbcompany.SelectedValue.ToString();
                if (company == " " || company == null)
                {
                    MessageBox.Show("Select Company Name");
                }
                else
                {
                    CompanyMaster popup = new CompanyMaster(this, tabControl, master, activecontroal);
                    popup.Update(company);
                    master.AddNewTab(popup);
                    //SqlCommand cmd5 = new SqlCommand("Select * from CompanyMaster where companyname ='"+company+"'", con);
                    //SqlDataAdapter sda = new SqlDataAdapter(cmd5);
                    //DataTable dt = new DataTable();
                    //sda.Fill(dt);
                }
            }
            else
            {
                MessageBox.Show("Select Company Name");
            }
        }

        private void btnGroupEdit_Click(object sender, EventArgs e)
        {
            if (txtgroup.Text != "" && txtgroup.Text != null)
            {
                string group = txtgroup.SelectedValue.ToString();
                if (group == " " || group == null)
                {
                    MessageBox.Show("Select Group Name");
                }
                else
                {
                    var privouscontroal = txtgroup;
                    activecontroal = privouscontroal.Name;
                    ItemGroup popup = new ItemGroup(this, tabControl, master, activecontroal);
                    popup.Update(group);
                    master.AddNewTab(popup);
                    //SqlCommand cmd5 = new SqlCommand("Select * from CompanyMaster where companyname ='"+company+"'", con);
                    //SqlDataAdapter sda = new SqlDataAdapter(cmd5);
                    //DataTable dt = new DataTable();
                    //sda.Fill(dt);
                }
            }
            else
            {
                MessageBox.Show("Select Group Name");
            }
        }

        private void btnPrimaryUnitEdit_Click(object sender, EventArgs e)
        {
            if (txtpunit.Text != "" && txtpunit.Text != null)
            {
                var privouscontroal = txtpunit;
                activecontroal = privouscontroal.Name;
                string primaryunit = txtpunit.SelectedValue.ToString();
                PrimaryUnit popup = new PrimaryUnit(this, tabControl, master, activecontroal);
                popup.Update(primaryunit);
                master.AddNewTab(popup);
            }
            else
            {
                MessageBox.Show("Please Select PrimaryUnit");
            }
        }

        private void btnAddPrimaryUnit_Click(object sender, EventArgs e)
        {
            var privouscontroal = txtpunit;
            activecontroal = privouscontroal.Name;
            PrimaryUnit popup = new PrimaryUnit(this, tabControl, master, activecontroal);
            master.AddNewTab(popup);
        }

        private void btnAddAltUnit_Click(object sender, EventArgs e)
        {
            var privouscontroal = txtaunit;
            activecontroal = privouscontroal.Name;
            PrimaryUnit popup = new PrimaryUnit(this, tabControl, master, activecontroal);
            master.AddNewTab(popup);
        }

        private void btnEditAltUnit_Click(object sender, EventArgs e)
        {
            if (txtaunit.Text != "" && txtaunit.Text != null)
            {
                var privouscontroal = txtaunit;
                activecontroal = privouscontroal.Name;
                string primaryunit = txtaunit.SelectedValue.ToString();
                PrimaryUnit popup = new PrimaryUnit(this, tabControl, master, activecontroal);
                popup.Update(primaryunit);
                master.AddNewTab(popup);
            }
            else
            {
                MessageBox.Show("Please Select Alternate Unit");
            }
        }

        private void btnAddTaxSlab_Click(object sender, EventArgs e)
        {
            var privouscontroal = cmbtaxslab;
            activecontroal = privouscontroal.Name;
            TaxSlab popup = new TaxSlab(this, tabControl, master, activecontroal);
            master.AddNewTab(popup);
        }

        private void btnEditTaxSlab_Click(object sender, EventArgs e)
        {
            var privouscontroal = cmbtaxslab;
            activecontroal = privouscontroal.Name;
            TaxSlab popup = new TaxSlab(this, tabControl, master, activecontroal);
            string taxslab = cmbtaxslab.Text;
            if (taxslab != "" && taxslab != null)
            {
                popup.Update(taxslab);
                master.AddNewTab(popup);
            }
            else
            {
                MessageBox.Show("Please Select Tax Slab");
            }

        }

        private void txtpunit_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (txtpunit.SelectedIndex != -1)
            {
                txtaunit.Text = txtpunit.Text;
            }
            else
            {
                try
                {
                    bool inList = false;
                    for (int i = 0; i < txtpunit.Items.Count; i++)
                    {
                        s = txtpunit.GetItemText(txtpunit.Items[i]);
                        if (s == txtpunit.Text)
                        {
                            inList = true;
                            txtpunit.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        txtpunit.Text = "";
                    }
                }
                catch (Exception excp)
                {
                }
            }

        }

        private void cmbtaxslab_Leave(object sender, EventArgs e)
        {
            cmbtaxslab.Text = s;
        }

        private void cmbcompany_Leave(object sender, EventArgs e)
        {
            cmbcompany.Text = s;
        }

        private void cmbtaxslab_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbtaxslab.Items.Count; i++)
                {
                    s = cmbtaxslab.GetItemText(cmbtaxslab.Items[i]);
                    if (s == cmbtaxslab.Text)
                    {
                        inList = true;
                        cmbtaxslab.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbtaxslab.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void cmbcompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbcompany.Items.Count; i++)
                {
                    s = cmbcompany.GetItemText(cmbcompany.Items[i]);
                    if (s == cmbcompany.Text)
                    {
                        inList = true;
                        cmbcompany.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbcompany.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void txtgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < txtgroup.Items.Count; i++)
                {
                    s = txtgroup.GetItemText(txtgroup.Items[i]);
                    if (s == txtgroup.Text)
                    {
                        inList = true;
                        txtgroup.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    txtgroup.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void chkbatch_Enter(object sender, EventArgs e)
        {
            chkbatch.ForeColor = Color.Green;
        }

        private void chkbatch_Leave(object sender, EventArgs e)
        {
            chkbatch.ForeColor = Color.Black;
        }

        private void chkserial_Leave(object sender, EventArgs e)
        {
            chkserial.ForeColor = Color.Black;
        }

        private void chkhot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkhot.ForeColor = Color.Black;
                chkserial.Focus();
                chkserial.ForeColor = Color.Green;
            }
            else
            {

            }
        }
        int cnt = 0;
        private frmComplainMasterData frmComplainMasterData;
        private string activecontroal_2;
        private Process process;
        private SalePurchaseOrderSimpleformate salePurchaseOrderSimpleformate;
        private void chkhot_Enter(object sender, EventArgs e)
        {

            if (cnt == 0)
            {
                chkhot.ForeColor = Color.Green;
                this.ActiveControl = txtiname;
                cnt = 1;
            }
        }

        private void chkhot_Leave(object sender, EventArgs e)
        {
            chkhot.ForeColor = Color.Black;
        }

        private void txtitemnumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int flag = 0;
                string str = txtitemnumber.Text.ToUpper().Trim();
                SqlCommand cmd = new SqlCommand("select itemnumber from ProductMaster where isactive=1", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (txtitemnumber.Text != "")
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string val = dt.Rows[i][0].ToString().ToUpper().Trim();

                            if (val == str)
                            {
                                if (btnsubmit.Text != "Update")
                                {
                                    MessageBox.Show("Item/Product Number Already Available Please add Another");
                                    txtitemnumber.Focus();
                                    flag = 1;
                                    break;
                                }

                            }

                        }
                    }
                    else
                    {
                        txtiname.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Item/Product Number cannot be Blank");
                    txtitemnumber.Show();
                    return;
                }
                if (flag == 0)
                {
                    txtiname.Focus();
                }
            }
        }

        private void txtitemnumber_Enter(object sender, EventArgs e)
        {
            txtitemnumber.BackColor = Color.LightYellow;
        }

        private void txtitemnumber_Leave(object sender, EventArgs e)
        {
            txtitemnumber.BackColor = Color.White;
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
            string appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Images\ItemImage\";
            if (Directory.Exists(appPath) == false)
            {
                Directory.CreateDirectory(appPath);
            }
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string iName = txtiname.Text.Trim().Replace(" ", "").Replace("'", "''") + "-Logo-" + no + "-" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "" + ofd.SafeFileName;
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

        private void Itementry_Shown(object sender, EventArgs e)
        {
            try
            {
                DataTable options = cn.getdataset("select * from options");
                if (options.Rows[0]["itembillno"].ToString() == "Continuous")
                {
                /*    string str = cn.ExecuteScalar("select max(ProductID) from ProductMaster where isactive=1");
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
                    txtitemnumber.Text = options.Rows[0]["itemprefix"].ToString() + count.ToString();
                */    txtitemnumber.ReadOnly = true;
                }
                else
                {
                  //  txtitemnumber.Text = "";
                    txtitemnumber.ReadOnly = false;
                    txtitemnumber.Focus();
                    this.ActiveControl = txtitemnumber;
                }
            }
            catch
            {
            }
        }

        DataTable qp = new DataTable();
        public void addisnal()
        {
            try
            {
                #region
                qp = cn.getdataset("select * from Additional where MasterType='Item Entry'");
                if (qp.Rows.Count > 0)
                {
                    pnladditional.Visible = true;
                    if (qp.Rows[0]["nooffields"].ToString() == "1")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "2")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "3")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "4")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "5")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "6")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "7")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "8")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        lblf8.Visible = true;
                        txtf8.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                            lblf8.Text = qp.Rows[0]["field8"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "9")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        lblf8.Visible = true;
                        txtf8.Visible = true;
                        lblf9.Visible = true;
                        txtf9.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                            lblf8.Text = qp.Rows[0]["field8"].ToString();
                            lblf9.Text = qp.Rows[0]["field9"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "10")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        lblf8.Visible = true;
                        txtf8.Visible = true;
                        lblf9.Visible = true;
                        txtf9.Visible = true;
                        lblf10.Visible = true;
                        txtf10.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                            lblf8.Text = qp.Rows[0]["field8"].ToString();
                            lblf9.Text = qp.Rows[0]["field9"].ToString();
                            lblf10.Text = qp.Rows[0]["field10"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "11")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        lblf8.Visible = true;
                        txtf8.Visible = true;
                        lblf9.Visible = true;
                        txtf9.Visible = true;
                        lblf10.Visible = true;
                        txtf10.Visible = true;
                        lblf11.Visible = true;
                        txtf11.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                            lblf8.Text = qp.Rows[0]["field8"].ToString();
                            lblf9.Text = qp.Rows[0]["field9"].ToString();
                            lblf10.Text = qp.Rows[0]["field10"].ToString();
                            lblf11.Text = qp.Rows[0]["field11"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "12")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        lblf8.Visible = true;
                        txtf8.Visible = true;
                        lblf9.Visible = true;
                        txtf9.Visible = true;
                        lblf10.Visible = true;
                        txtf10.Visible = true;
                        lblf11.Visible = true;
                        txtf11.Visible = true;
                        lblf12.Visible = true;
                        txtf12.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                            lblf8.Text = qp.Rows[0]["field8"].ToString();
                            lblf9.Text = qp.Rows[0]["field9"].ToString();
                            lblf10.Text = qp.Rows[0]["field10"].ToString();
                            lblf11.Text = qp.Rows[0]["field11"].ToString();
                            lblf12.Text = qp.Rows[0]["field12"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "13")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        lblf8.Visible = true;
                        txtf8.Visible = true;
                        lblf9.Visible = true;
                        txtf9.Visible = true;
                        lblf10.Visible = true;
                        txtf10.Visible = true;
                        lblf11.Visible = true;
                        txtf11.Visible = true;
                        lblf12.Visible = true;
                        txtf12.Visible = true;
                        lblf13.Visible = true;
                        txtf13.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                            lblf8.Text = qp.Rows[0]["field8"].ToString();
                            lblf9.Text = qp.Rows[0]["field9"].ToString();
                            lblf10.Text = qp.Rows[0]["field10"].ToString();
                            lblf11.Text = qp.Rows[0]["field11"].ToString();
                            lblf12.Text = qp.Rows[0]["field12"].ToString();
                            lblf13.Text = qp.Rows[0]["field13"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "14")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        lblf8.Visible = true;
                        txtf8.Visible = true;
                        lblf9.Visible = true;
                        txtf9.Visible = true;
                        lblf10.Visible = true;
                        txtf10.Visible = true;
                        lblf11.Visible = true;
                        txtf11.Visible = true;
                        lblf12.Visible = true;
                        txtf12.Visible = true;
                        lblf13.Visible = true;
                        txtf13.Visible = true;
                        lblf14.Visible = true;
                        txtf14.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                            lblf8.Text = qp.Rows[0]["field8"].ToString();
                            lblf9.Text = qp.Rows[0]["field9"].ToString();
                            lblf10.Text = qp.Rows[0]["field10"].ToString();
                            lblf11.Text = qp.Rows[0]["field11"].ToString();
                            lblf12.Text = qp.Rows[0]["field12"].ToString();
                            lblf13.Text = qp.Rows[0]["field13"].ToString();
                            lblf14.Text = qp.Rows[0]["field14"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "15")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        lblf8.Visible = true;
                        txtf8.Visible = true;
                        lblf9.Visible = true;
                        txtf9.Visible = true;
                        lblf10.Visible = true;
                        txtf10.Visible = true;
                        lblf11.Visible = true;
                        txtf11.Visible = true;
                        lblf12.Visible = true;
                        txtf12.Visible = true;
                        lblf13.Visible = true;
                        txtf13.Visible = true;
                        lblf14.Visible = true;
                        txtf14.Visible = true;
                        lblf15.Visible = true;
                        txtf15.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                            lblf8.Text = qp.Rows[0]["field8"].ToString();
                            lblf9.Text = qp.Rows[0]["field9"].ToString();
                            lblf10.Text = qp.Rows[0]["field10"].ToString();
                            lblf11.Text = qp.Rows[0]["field11"].ToString();
                            lblf12.Text = qp.Rows[0]["field12"].ToString();
                            lblf13.Text = qp.Rows[0]["field13"].ToString();
                            lblf14.Text = qp.Rows[0]["field14"].ToString();
                            lblf15.Text = qp.Rows[0]["field15"].ToString();
                        }
                    }
                    txtf1.Focus();
                }
                else
                {
                    chkbatch.Focus();
                    chkbatch.ForeColor = Color.Green;
                }
                #endregion
            }
            catch
            {
            }
        }

        private void txtf1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "1")
                {
                    chkbatch.Focus();
                    chkbatch.ForeColor = Color.Green;
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf2.Focus();
                }
            }
        }

        private void txtf2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "2")
                {
                    chkbatch.Focus();
                    chkbatch.ForeColor = Color.Green;
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf3.Focus();
                }
            }
        }

        private void txtf3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "3")
                {
                    chkbatch.Focus();
                    chkbatch.ForeColor = Color.Green;
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf4.Focus();
                }
            }
        }

        private void txtf4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "4")
                {
                    chkbatch.Focus();
                    chkbatch.ForeColor = Color.Green;
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf5.Focus();
                }
            }
        }

        private void txtf5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "5")
                {
                    chkbatch.Focus();
                    chkbatch.ForeColor = Color.Green;
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf6.Focus();
                }
            }
        }

        private void txtf6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "6")
                {
                    chkbatch.Focus();
                    chkbatch.ForeColor = Color.Green;
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf7.Focus();
                }
            }
        }

        private void txtf7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "7")
                {
                    chkbatch.Focus();
                    chkbatch.ForeColor = Color.Green;
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf8.Focus();
                }
            }
        }

        private void txtf8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "8")
                {
                    chkbatch.Focus();
                    chkbatch.ForeColor = Color.Green;
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf9.Focus();
                }
            }
        }

        private void txtf9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "9")
                {
                    chkbatch.Focus();
                    chkbatch.ForeColor = Color.Green;
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf10.Focus();
                }
            }
        }

        private void txtf10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "10")
                {
                    chkbatch.Focus();
                    chkbatch.ForeColor = Color.Green;
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf11.Focus();
                }
            }
        }

        private void txtf11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "11")
                {
                    chkbatch.Focus();
                    chkbatch.ForeColor = Color.Green;
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf12.Focus();
                }
            }
        }

        private void txtf12_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "12")
                {
                    chkbatch.Focus();
                    chkbatch.ForeColor = Color.Green;
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf13.Focus();
                }
            }
        }

        private void txtf13_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "13")
                {
                    chkbatch.Focus();
                    chkbatch.ForeColor = Color.Green;
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf14.Focus();
                }
            }
        }

        private void txtf14_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "14")
                {
                    chkbatch.Focus();
                    chkbatch.ForeColor = Color.Green;
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf15.Focus();
                }
            }
        }

        private void txtf15_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkbatch.Focus();
                chkbatch.ForeColor = Color.Green;
                pnladditional.Visible = false;
            }
        }











    }
}
