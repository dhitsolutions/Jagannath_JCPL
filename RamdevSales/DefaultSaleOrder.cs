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
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace RamdevSales
{
    public partial class DefaultSaleOrder : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        Printing prn = new Printing();
        //  private DateWisePurchaseReport dateWisePurchaseReport;
        public static string productid = "";
        public static string saletype = "";
        public static string taxvalue = "";
        public static string taxforprice = "";
        public static string ataxforprice = "";
        public static string itemname = "";
        public static string[] updateitem;
        public static DataTable temptable = new DataTable();
        public static string activecontroal;
        public static DataTable options, dt, dt1, dt2, dt3, dt5, dt6, dt7, dt8 = new DataTable();
        static string Dprice, taxper, additaxper;
        public static string refnoupdate;
        public DefaultSaleOrder()
        {
            InitializeComponent();
            options = conn.getdataset("select * from options");
            loadcurrency();
        }
        private void loadcurrency()
        {
            try
            {
                lblrate.Text = "Rate" + Master.currency;
                lblamount.Text = "Amount" + Master.currency;
            }
            catch
            {
            }
        }
        //public DefaultSaleOrder(DateWisePurchaseReport dateWisePurchaseReport)
        //{

        //    InitializeComponent();
        //    this.dateWisePurchaseReport = dateWisePurchaseReport;
        //    options = conn.getdataset("select * from options");
        //    loadcurrency();
        //}

        public DefaultSaleOrder(Ledger ledger)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this.ledger = ledger;
            options = conn.getdataset("select * from options");
            loadcurrency();
        }

        public DefaultSaleOrder(DateWiseReport dateWiseReport)
        {
            InitializeComponent();
            this.dateWiseReport = dateWiseReport;
            options = conn.getdataset("select * from options");
            loadcurrency();
        }

        public DefaultSaleOrder(CashBook cashBook)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this.cashBook = cashBook;
            options = conn.getdataset("select * from options");
            loadcurrency();
        }

        public DefaultSaleOrder(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            options = conn.getdataset("select * from options");
            loadcurrency();
            this.master = master;
            this.tabControl = tabControl;
        }

        public DefaultSaleOrder(DateWiseReport dateWiseReport, Master master, TabControl tabControl)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this.dateWiseReport = dateWiseReport;
            this.master = master;
            this.tabControl = tabControl;
            options = conn.getdataset("select * from options");
            loadcurrency();
        }

        public DefaultSaleOrder(Master master, TabControl tabControl, string[] strfinalarray)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
            this.strfinalarray = strfinalarray;
            options = conn.getdataset("select * from options");
            loadcurrency();
        }

        public DefaultSaleOrder(DateWiseReport dateWiseReport, Master master, TabControl tabControl, string[] strfinalarray)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.dateWiseReport = dateWiseReport;
            this.master = master;
            this.tabControl = tabControl;
            this.strfinalarray = strfinalarray;
            options = conn.getdataset("select * from options");
            loadcurrency();
        }

        public DefaultSaleOrder(Ledger ledger, Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.ledger = ledger;
            this.master = master;
            this.tabControl = tabControl;
            options = conn.getdataset("select * from options");
            loadcurrency();
        }

        public DefaultSaleOrder(Ledger ledger, Master master, TabControl tabControl, string[] strfinalarray)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.ledger = ledger;
            this.master = master;
            this.tabControl = tabControl;
            this.strfinalarray = strfinalarray;
            options = conn.getdataset("select * from options");
            loadcurrency();
        }

        public DefaultSaleOrder(ItemWiseStock itemWiseStock, Master master, TabControl tabControl, string[] strfinalarray)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.itemWiseStock = itemWiseStock;
            this.master = master;
            this.tabControl = tabControl;
            this.strfinalarray = strfinalarray;
            options = conn.getdataset("select * from options");
            loadcurrency();
        }

        public DefaultSaleOrder(SaleOrderList saleOrderList, Master master, TabControl tabControl, string[] strfinalarray)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.saleOrderList = saleOrderList;
            this.master = master;
            this.tabControl = tabControl;
            this.strfinalarray = strfinalarray;
            options = conn.getdataset("select * from options");
            loadcurrency();
        }

        public DefaultSaleOrder(SaleOrder saleOrder, Master master, string p_2)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.saleOrder = saleOrder;
            this.master = master;
            this.p_2 = p_2;
            options = conn.getdataset("select * from options");
            loadcurrency();
        }
        DataTable userrights = new DataTable();
        private void DefaultSale_Load(object sender, EventArgs e)
        {
            try
            {
                if (cnt == 0)
                {
                    userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                    if (strfinalarray[0] == "SO")
                    {
                        if (userrights.Rows.Count > 0)
                        {
                            if (userrights.Rows[1]["a"].ToString() == "False")
                            {
                                BtnPayment.Enabled = false;
                            }
                            if (userrights.Rows[1]["d"].ToString() == "False")
                            {
                                btndelete.Enabled = false;
                            }
                        }
                        txtheader.Text = "SALE ORDER";
                        txttype.Text = "Sale Type:";
                        this.Text = "Sale Order";
                    }
                    else if (strfinalarray[0] == "SC")
                    {
                        if (userrights.Rows.Count > 0)
                        {
                            if (userrights.Rows[12]["a"].ToString() == "False")
                            {
                                BtnPayment.Enabled = false;
                            }
                            if (userrights.Rows[12]["d"].ToString() == "False")
                            {
                                btndelete.Enabled = false;
                            }
                        }
                        txtheader.Text = "SALE CHALLAN";
                        txttype.Text = "Sale Type:";
                        this.Text = "Sale Challan";
                    }
                    else if (strfinalarray[0] == "PO")
                    {
                        if (userrights.Rows.Count > 0)
                        {
                            if (userrights.Rows[4]["a"].ToString() == "False")
                            {
                                BtnPayment.Enabled = false;
                            }
                            if (userrights.Rows[4]["d"].ToString() == "False")
                            {
                                btndelete.Enabled = false;
                            }
                        }
                        txtheader.Text = "PURCHASE ORDER";
                        txttype.Text = "Purchase Type:";
                        this.Text = "Purchase Order";
                    }

                    else if (strfinalarray[0] == "PC")
                    {
                        if (userrights.Rows.Count > 0)
                        {
                            if (userrights.Rows[15]["a"].ToString() == "False")
                            {
                                BtnPayment.Enabled = false;
                            }
                            if (userrights.Rows[15]["d"].ToString() == "False")
                            {
                                btndelete.Enabled = false;
                            }
                        }
                        txtheader.Text = "PURCHASE CHALLAN";
                        txttype.Text = "Purchase Type:";
                        this.Text = "Purchase Challan";
                    }
                    options = conn.getdataset("select * from options");
                    loadpage();
                    bindperticular();
                    bindallitem();
                    TxtBillNo.ReadOnly = true;
                    //set the interval  and start the timer
                    //  timer1.Interval = 1000;
                    //  timer1.Start();
                }

                TxtRundate.CustomFormat = Master.dateformate;
                txtduedate.CustomFormat = Master.dateformate;



            }
            catch
            {

            }
        }
        private void loadpage()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            charges = 1;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            //  TxtRundate.Text = DateTime.Now.ToString(Master.dateformate);
            this.ActiveControl = TxtRundate;
            LVFO.Columns.Add("Description of Goods", 196, HorizontalAlignment.Left);
            LVFO.Columns.Add("Packing", 69, HorizontalAlignment.Center);
            LVFO.Columns.Add("Batch", 51, HorizontalAlignment.Center);
            LVFO.Columns.Add("P.Qty", 53, HorizontalAlignment.Center);
            LVFO.Columns.Add("A.Qty", 47, HorizontalAlignment.Center);
            LVFO.Columns.Add("Total Qty", 70, HorizontalAlignment.Right);
            LVFO.Columns.Add("Free", 53, HorizontalAlignment.Center);
            LVFO.Columns.Add(lblrate.Text, 74, HorizontalAlignment.Right);
            LVFO.Columns.Add("Per", 42, HorizontalAlignment.Center);
            LVFO.Columns.Add(lblamount.Text, 95, HorizontalAlignment.Center);
            LVFO.Columns.Add("Dis(%)", 50, HorizontalAlignment.Center);
            LVFO.Columns.Add("Dis Per", 63, HorizontalAlignment.Center);
            LVFO.Columns.Add("TAX", 63, HorizontalAlignment.Center);
            LVFO.Columns.Add("Add Tax", 65, HorizontalAlignment.Center);
            LVFO.Columns.Add("Total", 118, HorizontalAlignment.Center);
            LVFO.Columns.Add("Sgstper", 0, HorizontalAlignment.Center);
            LVFO.Columns.Add("Sgstamt", 0, HorizontalAlignment.Center);
            LVFO.Columns.Add("cgstper", 0, HorizontalAlignment.Center);
            LVFO.Columns.Add("cgstamt", 0, HorizontalAlignment.Center);
            LVFO.Columns.Add("igstper", 0, HorizontalAlignment.Center);
            LVFO.Columns.Add("igstamt", 0, HorizontalAlignment.Center);
            LVFO.Columns.Add("addtaxper", 0, HorizontalAlignment.Center);
            LVFO.Columns.Add("serial", 0, HorizontalAlignment.Center);
            LVFO.Columns.Add("cess", 0, HorizontalAlignment.Center);
            LVFO.Columns.Add("productid", 0, HorizontalAlignment.Center);

            LVCHARGES.Columns.Add("Perticulars", 237, HorizontalAlignment.Left);
            LVCHARGES.Columns.Add("Remarks", 215, HorizontalAlignment.Left);
            LVCHARGES.Columns.Add("Value", 167, HorizontalAlignment.Left);
            LVCHARGES.Columns.Add("@", 122, HorizontalAlignment.Left);
            LVCHARGES.Columns.Add("+/-", 91, HorizontalAlignment.Left);
            LVCHARGES.Columns.Add("Amount", 117, HorizontalAlignment.Right);
            LVCHARGES.Columns.Add("valueofexp", 0, HorizontalAlignment.Right);
            LVCHARGES.Columns.Add("tax", 0, HorizontalAlignment.Right);
            LVCHARGES.Columns.Add("sgst", 0, HorizontalAlignment.Right);
            LVCHARGES.Columns.Add("cgst", 0, HorizontalAlignment.Right);
            LVCHARGES.Columns.Add("igst", 0, HorizontalAlignment.Right);
            LVCHARGES.Columns.Add("additax", 0, HorizontalAlignment.Right);
            LVCHARGES.Columns.Add("additaxamt", 0, HorizontalAlignment.Right);
            LVCHARGES.Columns.Add("chargeID", 0, HorizontalAlignment.Right);
            lvserial.Columns.Add("", 1000, HorizontalAlignment.Right);
            lvallitem.Columns.Add("Product Name", 400, HorizontalAlignment.Left);
            temptable = new DataTable();
            temptable.Columns.Add("Itemname", typeof(string));
            temptable.Columns.Add("SERIAL", typeof(string));

            bindsaletype();
            bindcustomer();
            if (options.Rows[0]["salevoucherno"].ToString() == "Manual")
            {
                TxtBillNo.ReadOnly = false;
            }
            else if (options.Rows[0]["salervoucherno"].ToString() == "Manual")
            {
                TxtBillNo.ReadOnly = false;
            }
            else if (options.Rows[0]["purchasevoucherno"].ToString() == "Manual")
            {
                TxtBillNo.ReadOnly = false;
            }
            else if (options.Rows[0]["purchaservoucherno"].ToString() == "Manual")
            {
                TxtBillNo.ReadOnly = false;
            }
            // autoreaderbind();
            DataSet dtrange = conn.getdata("SELECT * FROM Company where CompanyID='" + Master.companyId + "'");
            TxtRundate.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            TxtRundate.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            txtduedate.MinDate = Convert.ToDateTime(TxtRundate.Value);
            txtduedate.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            con.Close();
        }
        public void bindbatch()
        {
            cmbbatch.Enabled = true;
            //dt8 = conn.getdataset("select Productid,Batchno from ProductPriceMaster where Productid='" + dt1.Rows[0]["ProductID"].ToString() + "' and isactive='1'");
            //DataRow dr;
            //dr = dt8.NewRow();
            //dr["Batchno"] = "Select Batch";
            //dt8.Rows.InsertAt(dr, 0);
            //cmbbatch.DataSource = dt8;
            //cmbbatch.DisplayMember = "Batchno";
            //cmbbatch.ValueMember = "Productid";
            //cmbbatch.Focus();
            dt8 = new DataTable();
            SqlCommand cmd1 = new SqlCommand("select Productid,Batchno from ProductPriceMaster where Productid='" + dt1.Rows[0]["ProductID"].ToString() + "' and isactive='1'", con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            sda1.Fill(dt8);
            cmbbatch.ValueMember = "Productid";
            cmbbatch.DisplayMember = "Batchno";
            cmbbatch.DataSource = dt8;
            cmbbatch.Focus();

            int count = cmbbatch.Items.Count - 1;
            cmbbatch.SelectedIndex = count;
        }
        public void bindsaletype()
        {
            SqlCommand cmd = new SqlCommand("select Purchasetypeid,Purchasetypename from PurchasetypeMaster where FormType='" + strfinalarray[0] + "' and isactive='1'", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            cmbsaletype.ValueMember = "Purchasetypeid";
            cmbsaletype.DisplayMember = "Purchasetypename";
            cmbsaletype.DataSource = dt;
            cmbsaletype.SelectedIndex = -1;

            //  autobind(dt, cmbsaletype);
        }

        public void bindcustomer()
        {
            string qry = "";
            if (Convert.ToBoolean(options.Rows[0]["showcustomersupplierseperate"].ToString()) == true)
            {
                if (strfinalarray[0] == "SO")
                {
                    DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='99'");
                    string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                    qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=99 or GroupID='" + groupid + "') order by AccountName";
                    //qry = "select ClientID,AccountName from ClientMaster where isactive=1 and groupID=99 order by AccountName";
                }
                else if (strfinalarray[0] == "SC")
                {
                    DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='99'");
                    string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                    qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=99 or GroupID='" + groupid + "') order by AccountName";
                    //qry = "select ClientID,AccountName from ClientMaster where isactive=1 and groupID=99 order by AccountName";
                }
                else if (strfinalarray[0] == "PO")
                {
                    DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='100'");
                    string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                    qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=100 or GroupID='" + groupid + "') order by AccountName";
                    //qry = "select ClientID,AccountName from ClientMaster where isactive=1 and groupID=100 order by AccountName";
                }
                else if (strfinalarray[0] == "PC")
                {
                    DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='100'");
                    string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                    qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=100 or GroupID='" + groupid + "') order by AccountName";
                    //qry = "select ClientID,AccountName from ClientMaster where isactive=1 and groupID=100 order by AccountName";
                }
            }
            else
            {
                DataTable Accountgroup = conn.getdataset("select * from AccountGroup where (id='99' or id='100')");
                string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                string groupid1 = Accountgroup.Rows[1]["UnderGroupID"].ToString();
                qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=99 or GroupID=100 or GroupID='" + groupid + "' or GroupID='" + groupid1 + "') order by AccountName";
                //qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=100 or groupid=99) order by AccountName";
            }


            SqlCommand cmd1 = new SqlCommand(qry, con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            cmbcustname.ValueMember = "ClientID";
            cmbcustname.DisplayMember = "AccountName";
            cmbcustname.DataSource = dt1;
            cmbcustname.SelectedIndex = -1;


            //  autobind(dt1, cmbcustname);
        }

        public void autoreaderbind()
        {
            try
            {
                AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();


                SqlDataReader dReader;
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = con;
                cmd1.CommandType = CommandType.Text;

                //start string

                String qry = "select ProductMaster.Product_Name from ProductMaster";
                //  con.Open();
                int count = 0;

                con.Close();
                qry = qry + " where isactive=1 order by ProductMaster.Product_Name";

                if (count == 0)
                {
                    //end string
                    cmd1.CommandText = qry;


                    con.Open();
                    dReader = cmd1.ExecuteReader();

                    if (dReader.HasRows == true)
                    {
                        while (dReader.Read())
                            namesCollection.Add(dReader["Product_Name"].ToString());

                    }
                    else
                    {
                        // MessageBox.Show("Data not found");
                    }
                    dReader.Close();

                    txtitemname.AutoCompleteMode = AutoCompleteMode.Suggest;
                    txtitemname.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    txtitemname.AutoCompleteCustomSource = namesCollection;
                }
                else
                {

                    //end string
                    cmd1.CommandText = qry;


                    //    con.Open();
                    dReader = cmd1.ExecuteReader();

                    if (dReader.HasRows == true)
                    {
                        while (dReader.Read())
                            namesCollection.Add(dReader["Product_Name"].ToString());

                    }
                    else
                    {
                        //  MessageBox.Show("Data not found");
                    }
                    dReader.Close();

                    txtitemname.AutoCompleteMode = AutoCompleteMode.Suggest;
                    txtitemname.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    txtitemname.AutoCompleteCustomSource = namesCollection;
                }
            }
            catch
            {
            }
            finally
            {
                con.Close();
            }
        }

        private void autobind(DataTable dt1, ComboBox cmbcustname)
        {
            string[] arr = new string[dt1.Rows.Count];
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                arr[i] = dt1.Rows[i][1].ToString();
            }

            cmbcustname.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cmbcustname.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbcustname.AutoCompleteCustomSource.AddRange(arr);
        }
        string num = "";
        string inword = "";
        public void GenerateWordsinRs()
        {
            decimal numberrs = Convert.ToDecimal(num);
            CultureInfo ci = new CultureInfo("en-IN");
            string aaa = String.Format("{0:#,##0.##}", numberrs);
            aaa = aaa + " " + ci.NumberFormat.CurrencySymbol.ToString();
            inword = aaa;


            string input = num;
            string a = "";
            string b = "";

            // take decimal part of input. convert it to word. add it at the end of method.
            string decimals = "";

            if (input.Contains("."))
            {
                decimals = input.Substring(input.IndexOf(".") + 1);
                // remove decimal part from input
                input = input.Remove(input.IndexOf("."));

            }
            string strWords = NumbersToWords(Convert.ToInt32(input));

            if (!num.Contains("."))
            {
                a = "Rupees :" + strWords + "  Only";
            }
            else
            {
                a = "Rupees :" + strWords + " ";
            }

            if (decimals.Length > 0)
            {
                // if there is any decimal part convert it to words and add it to strWords.
                string strwords2 = NumbersToWords(Convert.ToInt32(decimals));
                b = " and " + strwords2 + " Paisa Only ";
            }

            inword = a + b;
        }
        public static string NumbersToWords(int inputNumber)
        {
            int inputNo = inputNumber;

            if (inputNo == 0)
                return "Zero";

            int[] numbers = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (inputNo < 0)
            {
                sb.Append("Minus ");
                inputNo = -inputNo;
            }

            string[] words0 = {"" ,"One ", "Two ", "Three ", "Four ",
            "Five " ,"Six ", "Seven ", "Eight ", "Nine "};
            string[] words1 = {"Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ",
            "Fifteen ","Sixteen ","Seventeen ","Eighteen ", "Nineteen "};
            string[] words2 = {"Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ",
            "Seventy ","Eighty ", "Ninety "};
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };

            numbers[0] = inputNo % 1000; // units
            numbers[1] = inputNo / 1000;
            numbers[2] = inputNo / 100000;
            numbers[1] = numbers[1] - 100 * numbers[2]; // thousands
            numbers[3] = inputNo / 10000000; // crores
            numbers[2] = numbers[2] - 100 * numbers[3]; // lakhs

            for (int i = 3; i > 0; i--)
            {
                if (numbers[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (numbers[i] == 0) continue;
                u = numbers[i] % 10; // ones
                t = numbers[i] / 10;
                h = numbers[i] / 100; // hundreds
                t = t - 10 * h; // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    if (h > 0 || i == 0) sb.Append("");
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }
            return sb.ToString().TrimEnd();
        }
        public void btnCalculator_Click(object sender, EventArgs e)
        {
            print();
        }

        private void print()
        {
            if (txtheader.Text == "SALE ORDER")
            {
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[1]["u"].ToString() == "False")
                    {
                        MessageBox.Show("You don't have Permission To Print");
                        return;
                    }
                }
            }
            else if (txtheader.Text == "SALE CHALLAN")
            {
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[12]["u"].ToString() == "False")
                    {
                        MessageBox.Show("You don't have Permission To Print");
                        return;
                    }
                }
            }
            else if (txtheader.Text == "PURCHASE ORDER")
            {
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[4]["u"].ToString() == "False")
                    {
                        MessageBox.Show("You don't have Permission To Print");
                        return;
                    }
                }
            }
            else if (txtheader.Text == "PURCHASE CHALLAN")
            {
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[16]["u"].ToString() == "False")
                    {
                        MessageBox.Show("You don't have Permission To Print");
                        return;
                    }
                }
            }
            // ChangeNumbersToWords sh = new ChangeNumbersToWords();
            String s1 = Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00");
            if (s1 == ".00")
            {
                s1 = "0";
                num = s1;
            }
            else
            {
                num = s1;
            }
            GenerateWordsinRs();
            //  string[] words = s1.Split('.');


            //  string str = sh.changeToWords(words[0]);
            //  string str1 = sh.changeToWords(words[1]);
            //   if (str1 == " " || str1 == null || words[1] == "00")
            //   {
            //   str1 = "Zero ";
            //    }
            // String inword = "In words: " + str + "and " + str1 + "Paise Only";

            if (SaleOrderList.autoprint == "True")
            {
                DataTable client = conn.getdataset("select * from clientmaster where isactive=1 and clientID='" + cmbcustname.SelectedValue + "'");
                DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1 and CompanyID='" + Master.companyId + "'");
                prn.execute("delete from printing");
                int j = 1;
                string perticulars = "", remarks = "", value = "", at = "", plusminus = "", amount = "", valuelvf = "", taxlvf = "", sgstlvf = "", cgstlvf = "", igstlvf = "", addtaxlvf = "";
                for (int i = 0; i < LVCHARGES.Items.Count; i++)
                {
                    perticulars += LVCHARGES.Items[i].SubItems[0].Text + Environment.NewLine;
                    remarks += LVCHARGES.Items[i].SubItems[1].Text + Environment.NewLine;
                    value += LVCHARGES.Items[i].SubItems[2].Text + Environment.NewLine;
                    at += LVCHARGES.Items[i].SubItems[3].Text + Environment.NewLine;
                    plusminus += LVCHARGES.Items[i].SubItems[4].Text + Environment.NewLine;
                    amount += LVCHARGES.Items[i].SubItems[5].Text + Environment.NewLine;
                    valuelvf += LVCHARGES.Items[i].SubItems[6].Text + Environment.NewLine;
                    taxlvf += LVCHARGES.Items[i].SubItems[7].Text + Environment.NewLine;
                    sgstlvf += LVCHARGES.Items[i].SubItems[8].Text + Environment.NewLine;
                    cgstlvf += LVCHARGES.Items[i].SubItems[9].Text + Environment.NewLine;
                    igstlvf += LVCHARGES.Items[i].SubItems[10].Text + Environment.NewLine;
                    addtaxlvf += LVCHARGES.Items[i].SubItems[11].Text + Environment.NewLine;

                }

                for (int i = 0; i < LVFO.Items.Count; i++)
                {
                    try
                    {
                        DataTable hsn = conn.getdataset("select * from ProductMaster where isactive=1 and ProductID='" + LVFO.Items[i].SubItems[24].Text + "'");
                        DataTable item = conn.getdataset("select * from ProductPriceMaster where isactive=1 and ProductID='" + hsn.Rows[0]["ProductID"].ToString() + "' and Batchno='" + LVFO.Items[i].SubItems[2].Text + "'");
                        //DataTable item1 = conn.getdataset("select * from ItemTaxMaster where isactive=1 and ProductID='" + hsn.Rows[0]["ProductID"].ToString() + "' and saletypeid='"+cmbsaletype.Text+"'");
                        //old query  DataTable taxgroup = conn.getdataset("select sum(amount)-sum(discountamt) as basicamount,sum(sgstamt) as sgst,sum(cgstamt) as cgst,sum(amount)+sum(sgstamt)+sum(cgstamt)-sum(discountamt) as total, igdtamt ,sum(addtax) as addtax,sum(addtaxper) as addtaxper from BillProductMaster WHERE isactive=1 and billno='" + TxtBillNo.Text + "' group by igdtamt");
                        //new query created by sir ==select p.Hsn_Sac_Code,b.Productname,sum(b.Total),b.sgstper,sum( b.sgstamt),b.cgstper, sum(b.cgstamt),b.igstper,sum(b.igdtamt),b.addtaxper,sum(b.addtax),sum(b.Amount) from billproductmaster b inner join ProductMaster p on p.Product_Name=b.Productname  where  b.isactive=1 and p.isactive=1 group by p.Hsn_Sac_Code,b.Productname,b.sgstper,b.cgstper,b.igstper,b.addtaxper
                        DataTable taxgroup = conn.getdataset("select p.Hsn_Sac_Code,sum(b.Total)-sum(b.discountamt) as basicamount,b.sgstper as sgstper,sum( b.sgstamt) as sgst,b.cgstper as cgstper, sum(b.cgstamt) as cgst,sgstper + cgstper + igstper as igstper,sum(b.igdtamt) as igdtamt,b.addtaxper,sum(b.addtax) as addtax,sum(b.Total)+sum(b.sgstamt)+sum(b.cgstamt)-sum(b.discountamt) as total from SaleOrderProductMaster b inner join ProductMaster p on p.ProductID=b.ProductID where  b.billno='" + TxtBillNo.Text + "' and b.isactive=1 and p.isactive=1 group by p.Hsn_Sac_Code,b.sgstper,b.cgstper,b.igstper,b.addtaxper");
                        string HSNCODE = "HSN Code" + Environment.NewLine, taxable = "Taxable Amt" + Environment.NewLine, cgstper = "CGST % " + Environment.NewLine, cgstamt = "CGST AMT" + Environment.NewLine, sgstper = "SGST %" + Environment.NewLine, sgstamt = "SGST AMT" + Environment.NewLine, totalamt = "Total AMT" + Environment.NewLine, addper = "AddTax%" + Environment.NewLine, addamt = "AddAmt" + Environment.NewLine;
                        double cgst = 0, sgst = 0, basicamt = 0, nettotal = 0, Addtax = 0;
                        for (int t = 0; t < taxgroup.Rows.Count; t++)
                        {
                            HSNCODE += Environment.NewLine + taxgroup.Rows[t]["Hsn_Sac_Code"].ToString();
                            taxable += Environment.NewLine + taxgroup.Rows[t]["basicamount"].ToString();
                            basicamt += Convert.ToDouble(taxgroup.Rows[t]["basicamount"].ToString());

                            cgstper += Environment.NewLine + (Convert.ToDouble(taxgroup.Rows[t]["igstper"].ToString()) / 2).ToString();
                            cgstamt += Environment.NewLine + taxgroup.Rows[t]["cgst"].ToString();
                            cgst += Convert.ToDouble(taxgroup.Rows[t]["cgst"].ToString());

                            sgstper += Environment.NewLine + (Convert.ToDouble(taxgroup.Rows[t]["igstper"].ToString()) / 2).ToString();
                            sgstamt += Environment.NewLine + taxgroup.Rows[t]["sgst"].ToString();
                            sgst += Convert.ToDouble(taxgroup.Rows[t]["sgst"].ToString());

                            addper += Environment.NewLine + (Convert.ToDouble(taxgroup.Rows[t]["addtaxper"].ToString()) / 2).ToString();
                            addamt += Environment.NewLine + taxgroup.Rows[t]["addtax"].ToString();
                            Addtax += Convert.ToDouble(taxgroup.Rows[t]["addtax"].ToString());

                            totalamt += Environment.NewLine + taxgroup.Rows[t]["total"].ToString();
                            nettotal += Convert.ToDouble(taxgroup.Rows[t]["total"].ToString());
                        }
                        string customername = "";
                        string address = "";
                        string city = "";
                        string phone = "";
                        string mobile = "";
                        string account = cmbcustname.Text.ToUpper();
                        if (cmbterms.Text == "Cash" && account == "CASH")
                        {
                            customername = txtcustomername.Text;
                            address = txtaddress.Text;
                            city = txtcity.Text;
                            phone = txtphone.Text;
                            mobile = txtmobile.Text;
                        }
                        else
                        {
                            customername = client.Rows[0][2].ToString();
                            address = client.Rows[0][6].ToString();
                            city = client.Rows[0][7].ToString();
                            phone = client.Rows[0][9].ToString();
                            mobile = client.Rows[0][10].ToString();
                        }

                        //    DataTable hsn = conn.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + LVFO.Items[i].SubItems[0].Text + "'");
                        string serialno = Regex.Replace(LVFO.Items[i].SubItems[22].Text, @"\s+", string.Empty);
                        //    DataTable hsn = conn.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + LVFO.Items[i].SubItems[0].Text + "'");
                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34,T35,T36,T37,T38,T39,T40,T41,T42,T43,T44,T45,T46,T47,T48,T49,T50,T51,T52,T53,T54,T55,T56,T57,T58,T59,T60,T61,T62,T63,T64,T65,T66,T67,T68,T69,T70,T71,T72,T73,T74,T75,T76,T77,T78,T79,T80,T81,T82,T83,T84,T85,T86,T87,T88,T89,T90,T91,T92,T93,T94,T95,T96,T97,T98,T99,T100,P1,P2,P3,P4,P5,P6,P7,P8,P9,P10,P11,P12,P13,P14,P15,P16)VALUES";
                        qry += "('" + j++ + "','" + TxtBillNo.Text + "','" + TxtRundate.Text + "','" + cmbterms.Text + "','" + txtduedate.Text + "','" + cmbcustname.Text + "','" + txtpono.Text + "','" + cmbsaletype.Text + "','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','" + LVFO.Items[i].SubItems[2].Text + "','" + LVFO.Items[i].SubItems[3].Text + "','" + LVFO.Items[i].SubItems[4].Text + "','" + LVFO.Items[i].SubItems[5].Text + "','" + LVFO.Items[i].SubItems[6].Text + "','" + LVFO.Items[i].SubItems[7].Text + "','" + LVFO.Items[i].SubItems[8].Text + "','" + LVFO.Items[i].SubItems[9].Text + "','" + LVFO.Items[i].SubItems[10].Text + "','" + LVFO.Items[i].SubItems[11].Text + "','" + LVFO.Items[i].SubItems[12].Text + "','" + LVFO.Items[i].SubItems[13].Text + "','" + LVFO.Items[i].SubItems[14].Text + "','" + lbltotcount.Text + "','" + lbltotpqty.Text + "','" + txttotaqty.Text + "','" + txttotfree.Text + "','" + lblbasictot.Text + "','" + txttotdiscount.Text + "','" + txttotadis.Text + "','" + txttottax.Text + "','" + txttotaddvat.Text + "','" + txtamt.Text + "','" + txttotservice.Text + "','" + txttotalcharges.Text + "','" + txtroundoff.Text + "','" + TxtBillTotal.Text + "','" + dt1.Rows[0][0].ToString() + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][2].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + dt1.Rows[0][11].ToString() + "','" + dt1.Rows[0][12].ToString() + "','" + dt1.Rows[0][13].ToString() + "','" + inword + "','" + txttransport.Text + "','" + txtdelieveryat.Text + "','" + txtfraight.Text + "','" + txtvehicleno.Text + "','" + txtgrrrno.Text + "','" + txtremarks.Text + "','" + txtweight.Text + "','" + txtskids.Text + "','" + perticulars + "','" + remarks + "','" + value + "','" + at + "','" + plusminus + "','" + amount + "','" + client.Rows[0][0].ToString() + "','" + client.Rows[0][1].ToString() + "','" + customername + "','" + client.Rows[0][3].ToString() + "','" + client.Rows[0][4].ToString() + "','" + client.Rows[0][5].ToString() + "','" + address + "','" + city + "','" + client.Rows[0][8].ToString() + "','" + phone + "','" + mobile + "','" + client.Rows[0][11].ToString() + "','" + client.Rows[0][12].ToString() + "','" + client.Rows[0][13].ToString() + "','" + LVFO.Items[i].SubItems[15].Text + "','" + LVFO.Items[i].SubItems[16].Text + "','" + LVFO.Items[i].SubItems[17].Text + "','" + LVFO.Items[i].SubItems[18].Text + "','" + LVFO.Items[i].SubItems[19].Text + "','" + LVFO.Items[i].SubItems[20].Text + "','" + LVFO.Items[i].SubItems[21].Text + "','" + lblsgsttotsl.Text + "','" + lblcgattotal.Text + "','" + lbligsttotal.Text + "','" + valuelvf + "','" + taxlvf + "','" + sgstlvf + "','" + cgstlvf + "','" + igstlvf + "','" + addtaxlvf + "','" + hsn.Rows[0]["Hsn_Sac_Code"].ToString() + "','" + client.Rows[0][17].ToString() + "','" + HSNCODE + "','" + taxable + "','" + basicamt + "','" + cgstper + "','" + cgstamt + "','" + cgst + "','" + sgstper + "','" + sgstamt + "','" + sgst + "','" + addper + "','" + addamt + "','" + Addtax + "','" + totalamt + "','" + nettotal + "','" + client.Rows[0][19].ToString() + "','" + serialno + "','" + hsn.Rows[0]["itemnumber"].ToString() + "','" + client.Rows[0]["Deliveryat"].ToString() + "')";
                        prn.execute(qry);
                    }
                    catch
                    {
                    }
                }
                Print popup = new Print(strfinalarray[2]);
            }
            else
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Print Bill?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    DataTable client = conn.getdataset("select * from clientmaster where isactive=1 and clientID='" + cmbcustname.SelectedValue + "'");
                    DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1 and CompanyID='" + Master.companyId + "'");
                    prn.execute("delete from printing");
                    int j = 1;
                    string perticulars = "", remarks = "", value = "", at = "", plusminus = "", amount = "", valuelvf = "", taxlvf = "", sgstlvf = "", cgstlvf = "", igstlvf = "", addtaxlvf = "";
                    for (int i = 0; i < LVCHARGES.Items.Count; i++)
                    {
                        perticulars += LVCHARGES.Items[i].SubItems[0].Text + Environment.NewLine;
                        remarks += LVCHARGES.Items[i].SubItems[1].Text + Environment.NewLine;
                        value += LVCHARGES.Items[i].SubItems[2].Text + Environment.NewLine;
                        at += LVCHARGES.Items[i].SubItems[3].Text + Environment.NewLine;
                        plusminus += LVCHARGES.Items[i].SubItems[4].Text + Environment.NewLine;
                        amount += LVCHARGES.Items[i].SubItems[5].Text + Environment.NewLine;
                        valuelvf += LVCHARGES.Items[i].SubItems[6].Text + Environment.NewLine;
                        taxlvf += LVCHARGES.Items[i].SubItems[7].Text + Environment.NewLine;
                        sgstlvf += LVCHARGES.Items[i].SubItems[8].Text + Environment.NewLine;
                        cgstlvf += LVCHARGES.Items[i].SubItems[9].Text + Environment.NewLine;
                        igstlvf += LVCHARGES.Items[i].SubItems[10].Text + Environment.NewLine;
                        addtaxlvf += LVCHARGES.Items[i].SubItems[11].Text + Environment.NewLine;

                    }

                    for (int i = 0; i < LVFO.Items.Count; i++)
                    {
                        try
                        {
                            DataTable hsn = conn.getdataset("select * from ProductMaster where isactive=1 and ProductID='" + LVFO.Items[i].SubItems[24].Text + "'");
                            DataTable item = conn.getdataset("select * from ProductPriceMaster where isactive=1 and ProductID='" + hsn.Rows[0]["ProductID"].ToString() + "' and Batchno='" + LVFO.Items[i].SubItems[2].Text + "'");
                            //DataTable item1 = conn.getdataset("select * from ItemTaxMaster where isactive=1 and ProductID='" + hsn.Rows[0]["ProductID"].ToString() + "' and saletypeid='"+cmbsaletype.Text+"'");
                            //old query  DataTable taxgroup = conn.getdataset("select sum(amount)-sum(discountamt) as basicamount,sum(sgstamt) as sgst,sum(cgstamt) as cgst,sum(amount)+sum(sgstamt)+sum(cgstamt)-sum(discountamt) as total, igdtamt ,sum(addtax) as addtax,sum(addtaxper) as addtaxper from BillProductMaster WHERE isactive=1 and billno='" + TxtBillNo.Text + "' group by igdtamt");
                            //new query created by sir ==select p.Hsn_Sac_Code,b.Productname,sum(b.Total),b.sgstper,sum( b.sgstamt),b.cgstper, sum(b.cgstamt),b.igstper,sum(b.igdtamt),b.addtaxper,sum(b.addtax),sum(b.Amount) from billproductmaster b inner join ProductMaster p on p.Product_Name=b.Productname  where  b.isactive=1 and p.isactive=1 group by p.Hsn_Sac_Code,b.Productname,b.sgstper,b.cgstper,b.igstper,b.addtaxper
                            DataTable taxgroup = conn.getdataset("select p.Hsn_Sac_Code,sum(b.Total)-sum(b.discountamt) as basicamount,b.sgstper as sgstper,sum( b.sgstamt) as sgst,b.cgstper as cgstper, sum(b.cgstamt) as cgst,sgstper + cgstper + igstper as igstper,sum(b.igdtamt) as igdtamt,b.addtaxper,sum(b.addtax) as addtax,sum(b.Total)+sum(b.sgstamt)+sum(b.cgstamt)-sum(b.discountamt) as total from SaleOrderProductMaster b inner join ProductMaster p on p.ProductID=b.ProductID where  b.billno='" + TxtBillNo.Text + "' and b.isactive=1 and p.isactive=1 group by p.Hsn_Sac_Code,b.sgstper,b.cgstper,b.igstper,b.addtaxper");
                            string HSNCODE = "HSN Code" + Environment.NewLine, taxable = "Taxable Amt" + Environment.NewLine, cgstper = "CGST % " + Environment.NewLine, cgstamt = "CGST AMT" + Environment.NewLine, sgstper = "SGST %" + Environment.NewLine, sgstamt = "SGST AMT" + Environment.NewLine, totalamt = "Total AMT" + Environment.NewLine, addper = "AddTax%" + Environment.NewLine, addamt = "AddAmt" + Environment.NewLine;
                            double cgst = 0, sgst = 0, basicamt = 0, nettotal = 0, Addtax = 0;
                            for (int t = 0; t < taxgroup.Rows.Count; t++)
                            {
                                HSNCODE += Environment.NewLine + taxgroup.Rows[t]["Hsn_Sac_Code"].ToString();
                                taxable += Environment.NewLine + taxgroup.Rows[t]["basicamount"].ToString();
                                basicamt += Convert.ToDouble(taxgroup.Rows[t]["basicamount"].ToString());

                                cgstper += Environment.NewLine + (Convert.ToDouble(taxgroup.Rows[t]["igstper"].ToString()) / 2).ToString();
                                cgstamt += Environment.NewLine + taxgroup.Rows[t]["cgst"].ToString();
                                cgst += Convert.ToDouble(taxgroup.Rows[t]["cgst"].ToString());

                                sgstper += Environment.NewLine + (Convert.ToDouble(taxgroup.Rows[t]["igstper"].ToString()) / 2).ToString();
                                sgstamt += Environment.NewLine + taxgroup.Rows[t]["sgst"].ToString();
                                sgst += Convert.ToDouble(taxgroup.Rows[t]["sgst"].ToString());

                                addper += Environment.NewLine + (Convert.ToDouble(taxgroup.Rows[t]["addtaxper"].ToString()) / 2).ToString();
                                addamt += Environment.NewLine + taxgroup.Rows[t]["addtax"].ToString();
                                Addtax += Convert.ToDouble(taxgroup.Rows[t]["addtax"].ToString());

                                totalamt += Environment.NewLine + taxgroup.Rows[t]["total"].ToString();
                                nettotal += Convert.ToDouble(taxgroup.Rows[t]["total"].ToString());
                            }
                            string customername = "";
                            string address = "";
                            string city = "";
                            string phone = "";
                            string mobile = "";
                            string account = cmbcustname.Text.ToUpper();
                            if (cmbterms.Text == "Cash" && account == "CASH")
                            {
                                customername = txtcustomername.Text;
                                address = txtaddress.Text;
                                city = txtcity.Text;
                                phone = txtphone.Text;
                                mobile = txtmobile.Text;
                            }
                            else
                            {
                                customername = client.Rows[0][2].ToString();
                                address = client.Rows[0][6].ToString();
                                city = client.Rows[0][7].ToString();
                                phone = client.Rows[0][9].ToString();
                                mobile = client.Rows[0][10].ToString();
                            }

                            //    DataTable hsn = conn.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + LVFO.Items[i].SubItems[0].Text + "'");
                            string serialno = Regex.Replace(LVFO.Items[i].SubItems[22].Text, @"\s+", string.Empty);
                            //    DataTable hsn = conn.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + LVFO.Items[i].SubItems[0].Text + "'");
                            string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34,T35,T36,T37,T38,T39,T40,T41,T42,T43,T44,T45,T46,T47,T48,T49,T50,T51,T52,T53,T54,T55,T56,T57,T58,T59,T60,T61,T62,T63,T64,T65,T66,T67,T68,T69,T70,T71,T72,T73,T74,T75,T76,T77,T78,T79,T80,T81,T82,T83,T84,T85,T86,T87,T88,T89,T90,T91,T92,T93,T94,T95,T96,T97,T98,T99,T100,P1,P2,P3,P4,P5,P6,P7,P8,P9,P10,P11,P12,P13,P14,P15,P16)VALUES";
                            qry += "('" + j++ + "','" + TxtBillNo.Text + "','" + TxtRundate.Text + "','" + cmbterms.Text + "','" + txtduedate.Text + "','" + cmbcustname.Text + "','" + txtpono.Text + "','" + cmbsaletype.Text + "','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','" + LVFO.Items[i].SubItems[2].Text + "','" + LVFO.Items[i].SubItems[3].Text + "','" + LVFO.Items[i].SubItems[4].Text + "','" + LVFO.Items[i].SubItems[5].Text + "','" + LVFO.Items[i].SubItems[6].Text + "','" + LVFO.Items[i].SubItems[7].Text + "','" + LVFO.Items[i].SubItems[8].Text + "','" + LVFO.Items[i].SubItems[9].Text + "','" + LVFO.Items[i].SubItems[10].Text + "','" + LVFO.Items[i].SubItems[11].Text + "','" + LVFO.Items[i].SubItems[12].Text + "','" + LVFO.Items[i].SubItems[13].Text + "','" + LVFO.Items[i].SubItems[14].Text + "','" + lbltotcount.Text + "','" + lbltotpqty.Text + "','" + txttotaqty.Text + "','" + txttotfree.Text + "','" + lblbasictot.Text + "','" + txttotdiscount.Text + "','" + txttotadis.Text + "','" + txttottax.Text + "','" + txttotaddvat.Text + "','" + txtamt.Text + "','" + txttotservice.Text + "','" + txttotalcharges.Text + "','" + txtroundoff.Text + "','" + TxtBillTotal.Text + "','" + dt1.Rows[0][0].ToString() + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][2].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + dt1.Rows[0][11].ToString() + "','" + dt1.Rows[0][12].ToString() + "','" + dt1.Rows[0][13].ToString() + "','" + inword + "','" + txttransport.Text + "','" + txtdelieveryat.Text + "','" + txtfraight.Text + "','" + txtvehicleno.Text + "','" + txtgrrrno.Text + "','" + txtremarks.Text + "','" + txtweight.Text + "','" + txtskids.Text + "','" + perticulars + "','" + remarks + "','" + value + "','" + at + "','" + plusminus + "','" + amount + "','" + client.Rows[0][0].ToString() + "','" + client.Rows[0][1].ToString() + "','" + customername + "','" + client.Rows[0][3].ToString() + "','" + client.Rows[0][4].ToString() + "','" + client.Rows[0][5].ToString() + "','" + address + "','" + city + "','" + client.Rows[0][8].ToString() + "','" + phone + "','" + mobile + "','" + client.Rows[0][11].ToString() + "','" + client.Rows[0][12].ToString() + "','" + client.Rows[0][13].ToString() + "','" + LVFO.Items[i].SubItems[15].Text + "','" + LVFO.Items[i].SubItems[16].Text + "','" + LVFO.Items[i].SubItems[17].Text + "','" + LVFO.Items[i].SubItems[18].Text + "','" + LVFO.Items[i].SubItems[19].Text + "','" + LVFO.Items[i].SubItems[20].Text + "','" + LVFO.Items[i].SubItems[21].Text + "','" + lblsgsttotsl.Text + "','" + lblcgattotal.Text + "','" + lbligsttotal.Text + "','" + valuelvf + "','" + taxlvf + "','" + sgstlvf + "','" + cgstlvf + "','" + igstlvf + "','" + addtaxlvf + "','" + hsn.Rows[0]["Hsn_Sac_Code"].ToString() + "','" + client.Rows[0][17].ToString() + "','" + HSNCODE + "','" + taxable + "','" + basicamt + "','" + cgstper + "','" + cgstamt + "','" + cgst + "','" + sgstper + "','" + sgstamt + "','" + sgst + "','" + addper + "','" + addamt + "','" + Addtax + "','" + totalamt + "','" + nettotal + "','" + client.Rows[0][19].ToString() + "','" + serialno + "','" + hsn.Rows[0]["itemnumber"].ToString() + "','" + client.Rows[0]["Deliveryat"].ToString() + "')";
                            prn.execute(qry);
                        }
                        catch
                        {
                        }
                    }
                    Print popup = new Print(strfinalarray[2]);
                    popup.ShowDialog();
                    popup.Dispose();
                }
            }


        }
        public void clearitem()
        {


            txtitemname.Text = string.Empty;
            txtpacking.Text = string.Empty;
            txtbags.Text = string.Empty;
            qtyflag = 1;
            txtqty.Text = string.Empty;
            qtyflag = 1;
            txtpqty.Text = string.Empty;
            txtrate.Text = string.Empty;
            txtper.Text = string.Empty;
            txttotal.Text = string.Empty;
            txttax.Text = string.Empty;
            txtaddtax.Text = string.Empty;
            txtamount.Text = string.Empty;
            lbladdtax1.Text = "[]";
            lbltax1.Text = "[]";
            lblbagqty.Text = "[]";
            lblaltqty.Text = "[]";
            txtfree.Text = "";
            txtdisamt.Text = "";
            txtdisper.Text = "";

            cmbbatch.DataSource = null;
            cmbbatch.Items.Clear();
            lvserial.Items.Clear();
            //cmbbatch.Items.Clear();
            // cmbbatch.SelectedIndex = -1;
            //  cmbbatch.ResetText();




        }
        void getsr()
        {
            try
            {
                String str = conn.ExecuteScalar("select max(Bill_No) from SaleOrderMaster where isactive='1' and billtype='" + strfinalarray[0] + "' and saletype='" + cmbsaletype.SelectedValue + "'and id=(select max(id) from SaleOrderMaster where isactive=1 and BillType='" + strfinalarray[0] + "' and SaleType='" + cmbsaletype.SelectedValue + "' )");
                var charsToRemove = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "-" };
                foreach (var c in charsToRemove)
                {
                    str = str.Replace(c, string.Empty);
                }
                DataTable dt = conn.getdataset("select * from [PurchasetypeMaster] where isactive=1 and FormType='" + strfinalarray[0] + "' and Purchasetypeid='" + cmbsaletype.SelectedValue + "'");
                Int64 id, count;
                //     Object data = dr[1];

                if (str == "")
                {

                    id = Convert.ToInt64(dt.Rows[0]["startingno"].ToString());
                    count = Convert.ToInt64(dt.Rows[0]["startingno"].ToString());
                }
                else
                {
                    id = Convert.ToInt64(str) + 1;
                    count = Convert.ToInt64(str) + 1;
                }
                lblbill_no.Text = count.ToString();
                TxtBillNo.Text = dt.Rows[0]["prefix"].ToString() + count.ToString();

            }
            catch
            {
            }
            finally
            {

            }

        }
        public void clearall()
        {
            //getsr();
            txtcustomername.Text = string.Empty;
            txtaddress.Text = string.Empty;
            txtcity.Text = string.Empty;
            txtphone.Text = string.Empty;
            txtmobile.Text = string.Empty;
            txtpanno.Text = string.Empty;
            txtadharno.Text = string.Empty;
            TxtRundate.Text = DateTime.Now.ToShortDateString();
            cmbterms.SelectedIndex = -1;
            cmbcustname.SelectedIndex = -1;
            cmbsaletype.SelectedIndex = -1;
            txtpono.Text = string.Empty;
            lbltotpqty.Text = string.Empty;
            txttottax.Text = string.Empty;
            TxtBillTotal.Text = string.Empty;
            lblbasictot.Text = "0";
            txttottax.Text = "0";
            lbltotcount.Text = "0";
            lbltotpqty.Text = "0";
            txtweight.Text = string.Empty;
            txttransport.Text = string.Empty;
            txtremarks.Text = string.Empty;
            TxtBillNo.Text = "";
            TxtRundate.Focus();
            this.ActiveControl = TxtRundate;
            txtdelieveryat.Text = "";
            TxtBillNo.ReadOnly = true;
            temptable = new DataTable();
            cmbagentname.SelectedIndex = -1;
            pnlagent.Visible = false;
        }

        public void BtnPayment_Click(object sender, EventArgs e)
        {
            BtnPayment.Enabled = false;
            btnsubmit();
            BtnPayment.Enabled = true;
            this.ActiveControl = TxtRundate;
        }
        string oldbillno = "";
        private void btnsubmit()
        {
            try
            {

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                DialogResult dr = MessageBox.Show("Do you want to Save?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    if (!string.IsNullOrEmpty(TxtBillNo.Text))
                    {
                        if (options.Rows[0]["saleovoucherno"].ToString() == "Manual")
                        {
                            if (strfinalarray[0] == "SO")
                            {
                                string manual = TxtBillNo.Text;
                                var charsToRemove = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "-", "/", "!", "#", "$", "%", "&", "(", ")", "*", "+", ",", ".", ";", ":", "<", "=", ">", "?", "@", "[", "]", "~", "^", "_", "{", "}", "|" };
                                foreach (var c in charsToRemove)
                                {
                                    manual = manual.Replace(c, string.Empty);
                                }
                                lblbill_no.Text = manual;
                            }
                        }
                        if (options.Rows[0]["salecvoucherno"].ToString() == "Manual")
                        {
                            if (strfinalarray[0] == "SC")
                            {
                                string manual = TxtBillNo.Text;
                                var charsToRemove = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "-", "/", "!", "#", "$", "%", "&", "(", ")", "*", "+", ",", ".", ";", ":", "<", "=", ">", "?", "@", "[", "]", "~", "^", "_", "{", "}", "|" };
                                foreach (var c in charsToRemove)
                                {
                                    manual = manual.Replace(c, string.Empty);
                                }
                                lblbill_no.Text = manual;
                            }
                        }
                        if (options.Rows[0]["purchaseovoucherno"].ToString() == "Manual")
                        {
                            if (strfinalarray[0] == "PO")
                            {
                                string manual = TxtBillNo.Text;
                                var charsToRemove = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "-", "/", "!", "#", "$", "%", "&", "(", ")", "*", "+", ",", ".", ";", ":", "<", "=", ">", "?", "@", "[", "]", "~", "^", "_", "{", "}", "|" };
                                foreach (var c in charsToRemove)
                                {
                                    manual = manual.Replace(c, string.Empty);
                                }
                                lblbill_no.Text = manual;
                            }
                        }
                        if (options.Rows[0]["purchasecvoucherno"].ToString() == "Manual")
                        {
                            if (strfinalarray[0] == "PC")
                            {
                                string manual = TxtBillNo.Text;
                                var charsToRemove = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "-", "/", "!", "#", "$", "%", "&", "(", ")", "*", "+", ",", ".", ";", ":", "<", "=", ">", "?", "@", "[", "]", "~", "^", "_", "{", "}", "|" };
                                foreach (var c in charsToRemove)
                                {
                                    manual = manual.Replace(c, string.Empty);
                                }
                                lblbill_no.Text = manual;
                            }
                        }
                        this.Enabled = false;
                        if (BtnPayment.Text == "Update")
                        {
                            if (txtheader.Text == "SALE ORDER")
                            {
                                if (userrights.Rows.Count > 0)
                                {
                                    if (userrights.Rows[1]["u"].ToString() == "False")
                                    {
                                        MessageBox.Show("You don't have Permission To Update");
                                        return;
                                    }
                                }
                            }
                            else if (txtheader.Text == "SALE CHALLAN")
                            {
                                if (userrights.Rows.Count > 0)
                                {
                                    if (userrights.Rows[12]["u"].ToString() == "False")
                                    {
                                        MessageBox.Show("You don't have Permission To Update");
                                        return;
                                    }
                                }
                            }
                            else if (txtheader.Text == "PURCHASE ORDER")
                            {
                                if (userrights.Rows.Count > 0)
                                {
                                    if (userrights.Rows[4]["u"].ToString() == "False")
                                    {
                                        MessageBox.Show("You don't have Permission To Update");
                                        return;
                                    }
                                }
                            }
                            else if (txtheader.Text == "PURCHASE CHALLAN")
                            {
                                if (userrights.Rows.Count > 0)
                                {
                                    if (userrights.Rows[16]["u"].ToString() == "False")
                                    {
                                        MessageBox.Show("You don't have Permission To Update");
                                        return;
                                    }
                                }
                            }
                            string clientid = conn.ExecuteScalar("select ClientID from SaleOrderProductMaster where billno=(select billno from SaleOrderMaster where id='" + lblid.Text + "' and BillType='" + strfinalarray[0] + "') and billtype='" + strfinalarray[0] + "' and isactive=1");
                            if (string.IsNullOrEmpty(clientid))
                            {
                                SqlCommand cmd2 = new SqlCommand("Update SaleOrderProductMaster set ClientID='" + clientidupdate + "' where  isactive=1 and BillType='" + strfinalarray[0] + "'and billno=(select billno from SaleOrderMaster where id='" + lblid.Text + "' and BillType='" + strfinalarray[0] + "')", con);
                                cmd2.ExecuteNonQuery();
                            }
                            string charges = conn.ExecuteScalar("select ClientID from SaleOrderchargesmaster where billno=(select billno from SaleOrderMaster where id='" + lblid.Text + "' and BillType='" + strfinalarray[0] + "') and billtype='" + strfinalarray[0] + "' and isactive=1");
                            if (string.IsNullOrEmpty(charges))
                            {
                                conn.execute("update SaleOrderchargesmaster set ClientID='" + clientidupdate + "' where isactive=1 and billno=(select billno from SaleOrderMaster where id='" + lblid.Text + "' and BillType='" + strfinalarray[0] + "') and billtype='" + strfinalarray[0] + "'");
                            }
                            SqlCommand cmd21 = new SqlCommand("Update SaleOrderProductMaster set isactive='0',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "' where ClientID='" + clientidupdate + "' and isactive=1 and BillType='" + strfinalarray[0] + "'and billno=(select billno from SaleOrderMaster where id='" + lblid.Text + "' and BillType='" + strfinalarray[0] + "')", con);
                            cmd21.ExecuteNonQuery();
                            conn.execute("update SaleOrderchargesmaster set isactive='0',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "' where ClientID='" + clientidupdate + "' and isactive=1 and billno=(select billno from SaleOrderMaster where id='" + lblid.Text + "' and BillType='" + strfinalarray[0] + "') and billtype='" + strfinalarray[0] + "'");
                            conn.execute("Update Serials set isactive='0',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "' where isactive=1 and VoucherID='" + TxtBillNo.Text + "'");
                            for (int i = 0; i < LVFO.Items.Count; i++)
                            {
                                Guid guid;
                                guid = Guid.NewGuid();
                                string productid = conn.ExecuteScalar("select Productid from productmaster where isactive=1 and ProductID='" + LVFO.Items[i].SubItems[24].Text + "'");
                                // conn.execute("INSERT INTO [dbo].[BillProductMaster]([Bill_No],[Bill_Run_Date],[Productname],[Packing],[Bags],[MRP],[Pqty],[Aqty],[Rate],[Per],[Total],[Tax],[Amount],[isactive],[qty],[Billtype],[billno],[addtax],[batch],[free],[discountper],[discountamt],[productid])VALUES('"+lblbill_no.Text+"','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + LVFO.Items[i].SubItems[0].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[1].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[3].Text.Replace(",", "") + "','','" + LVFO.Items[i].SubItems[5].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[4].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[7].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[8].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[9].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[12].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[14].Text.Replace(",", "") + "','1','" + LVFO.Items[i].SubItems[5].Text.Replace(",", "") + "','S','" + TxtBillNo.Text + "','" + LVFO.Items[i].SubItems[13].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[2].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[6].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[10].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[11].Text.Replace(",", "") + "','"+productid+"')");
                                conn.execute("INSERT INTO [dbo].[SaleOrderProductMaster]([Bill_No],[Bill_Run_Date],[Productname],[Packing],[Bags],[MRP],[Pqty],[Aqty],[Rate],[Per],[Total],[Tax],[Amount],[isactive],[qty],[Billtype],[billno],[addtax],[batch],[free],[discountper],[discountamt],[productid],[sgstper],[sgstamt],[cgstper],[cgstamt],[igstper],[igdtamt],[addtaxper],[serialno],[refno],[cess],[SyncID],[SyncDatetime],[fkid],[ClientID])VALUES('" + lblbill_no.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + LVFO.Items[i].SubItems[0].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[1].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[3].Text.Replace(",", "") + "','','" + LVFO.Items[i].SubItems[5].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[4].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[7].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[8].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[9].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[12].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[14].Text.Replace(",", "") + "','1','" + LVFO.Items[i].SubItems[5].Text.Replace(",", "") + "','" + strfinalarray[0] + "','" + TxtBillNo.Text + "','" + LVFO.Items[i].SubItems[13].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[2].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[6].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[10].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[11].Text.Replace(",", "") + "','" + productid + "','" + LVFO.Items[i].SubItems[15].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[16].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[17].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[18].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[19].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[20].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[21].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[22].Text + "','" + refnoupdate + "','" + LVFO.Items[i].SubItems[23].Text.Replace(",", "") + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "','" + lblid.Text + "','" + cmbcustname.SelectedValue + "')");
                            }
                            for (int o = 0; o < temptable.Rows.Count; o++)
                            {
                                Guid guid;
                                guid = Guid.NewGuid();
                                conn.execute("INSERT INTO [dbo].[Serials]([Date],[VoucherID],[TransactionID],[SerialNo],[TransactionType],[VchNo],[PurchaseTypeID],[PurchaseTypeName],[SaleTypeID],[SaleTypeName],[PartyID],[PartyName],[ItemCompanyID],[SyncID],[SyncDatetime],[isactive])VALUES('" + DateTime.Now.ToString(Master.dateformate) + "','" + TxtBillNo.Text + "','" + strfinalarray[0] + "','" + temptable.Rows[o]["SERIAL"].ToString() + "','" + strfinalarray[2] + "','" + txtpono.Text + "','" + cmbsaletype.SelectedValue + "','" + cmbsaletype.Text + "','" + cmbsaletype.SelectedValue + "','" + cmbsaletype.Text + "','" + cmbcustname.SelectedValue + "','" + cmbcustname.Text + "','" + Master.companyId + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',1)");
                            }
                            for (int i = 0; i < LVCHARGES.Items.Count; i++)
                            {
                                Guid guid;
                                guid = Guid.NewGuid();
                                conn.execute("INSERT INTO [SaleOrderchargesmaster]([billno],[perticulars],[remarks],[value],[at],[plusminus],[amount],[valueofexp],[tax],[sgst],[cgst],[igst],[additax],[addtaxamt],[billtype],[billsundryid],[SyncID],[SyncDatetime],[fkid],[ClientID],[isactive])VALUES('" + TxtBillNo.Text + "','" + LVCHARGES.Items[i].SubItems[0].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[1].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[2].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[3].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[4].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[5].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[6].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[7].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[8].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[9].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[10].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[11].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[12].Text.Replace(",", "") + "','" + strfinalarray[0] + "','" + LVCHARGES.Items[i].SubItems[13].Text.Replace(",", "") + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "','" + lblid.Text + "','" + cmbcustname.SelectedValue + "',1)");
                            }
                            //  conn.execute("UPDATE [dbo].[SaleOrderMaster]SET [cusname]='" + txtcustomername.Text + "',[cusadd]='" + txtaddress.Text + "',[cuscity]='" + txtcity.Text + "',[cusphone]='" + txtphone.Text + "',[cusmobile]='" + txtmobile.Text + "',[cuspancard]='" + txtpanno.Text + "',[cusadhar]='" + txtadharno.Text + "',[Bill_No] = '" + lblbill_no.Text + "',[Bill_Date] = '" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',[Terms] = '" + cmbterms.Text + "',[ClientID] = '" + cmbcustname.SelectedValue + "',[PO_No] = '" + txtpono.Text.Replace(",", "") + "',[SaleType] = '" + cmbsaletype.SelectedValue + "',[count] = '" + lbltotcount.Text.Replace(",", "") + "',[totalqty] = '" + lbltotpqty.Text.Replace(",", "") + "',[totalbasic] = '" + lblbasictot.Text.Replace(",", "") + "',[totaltax] = '" + txttottax.Text.Replace(",", "") + "',[totalnet] = '" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "',[isactive] = '1',[apprweight] = '" + txtweight.Text.Replace(",", "") + "',[dispatchdetails] = '" + txttransport.Text + "',[remarks] = '" + txtremarks.Text + "',[BillType] = '" + strfinalarray[0] + "',[billno] = '" + TxtBillNo.Text + "',[totaladdtax] = '" + txttotaddvat.Text + "',[roudoff] = '" + txtroundoff.Text + "',[Duedate] = '" + Convert.ToDateTime(txtduedate.Text).ToString(Master.dateformate) + "',[totalaqty] = '" + txttotaqty.Text.Replace(",", "") + "',[totalfree] = '" + txttotfree.Text.Replace(",", "") + "',[totaldiscount] ='" + txttotdiscount.Text.Replace(",", "") + "',[totaladddiscount] = '" + txttotadis.Text.Replace(",", "") + "',[totalamount] = '" + txtamt.Text.Replace(",", "") + "',[totalservicejob] = '" + txttotservice.Text.Replace(",", "") + "',[totalcharges] = '" + txttotalcharges.Text.Replace(",", "") + "',[Delieveryat] = '" + txtdelieveryat.Text + "',[fraight] = '" + txtfraight.Text + "',[vehicleno] = '" + txtvehicleno.Text + "',[grrrno] = '" + txtgrrrno.Text + "',[noofskids] = '" + txtskids.Text + "',[sgstamt]='" + lblsgsttotsl.Text + "',[cgatamt]='" + lblcgattotal.Text + "',[igstamt]='" + lbligsttotal.Text + "',[refno]='" + refnoupdate + "',[totalcess]='" + txttotalcess.Text.Replace(",", "") + "',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',[agentID]='" + cmbagentname.SelectedValue + "' where billno=(select billno from SaleOrderMaster where isactive=1 and id='" + lblid.Text + "' and BillType='" + strfinalarray[0] + "') and [billtype]='" + strfinalarray[0] + "'");
                            conn.execute("UPDATE [dbo].[SaleOrderMaster]SET [cusname]='" + txtcustomername.Text + "',[cusadd]='" + txtaddress.Text + "',[cuscity]='" + txtcity.Text + "',[cusphone]='" + txtphone.Text + "',[cusmobile]='" + txtmobile.Text + "',[cuspancard]='" + txtpanno.Text + "',[cusadhar]='" + txtadharno.Text + "',[Bill_No] = '" + lblbill_no.Text + "',[Bill_Date] = '" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',[Terms] = '" + cmbterms.Text + "',[ClientID] = '" + cmbcustname.SelectedValue + "',[PO_No] = '" + txtpono.Text.Replace(",", "") + "',[SaleType] = '" + cmbsaletype.SelectedValue + "',[count] = '" + lbltotcount.Text.Replace(",", "") + "',[totalqty] = '" + lbltotpqty.Text.Replace(",", "") + "',[totalbasic] = '" + lblbasictot.Text.Replace(",", "") + "',[totaltax] = '" + txttottax.Text.Replace(",", "") + "',[totalnet] = '" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "',[isactive] = '1',[apprweight] = '" + txtweight.Text.Replace(",", "") + "',[dispatchdetails] = '" + txttransport.Text + "',[remarks] = '" + txtremarks.Text + "',[BillType] = '" + strfinalarray[0] + "',[billno] = '" + TxtBillNo.Text + "',[totaladdtax] = '" + txttotaddvat.Text + "',[roudoff] = '" + txtroundoff.Text + "',[Duedate] = '" + Convert.ToDateTime(txtduedate.Text).ToString(Master.dateformate) + "',[totalaqty] = '" + txttotaqty.Text.Replace(",", "") + "',[totalfree] = '" + txttotfree.Text.Replace(",", "") + "',[totaldiscount] ='" + txttotdiscount.Text.Replace(",", "") + "',[totaladddiscount] = '" + txttotadis.Text.Replace(",", "") + "',[totalamount] = '" + txtamt.Text.Replace(",", "") + "',[totalservicejob] = '" + txttotservice.Text.Replace(",", "") + "',[totalcharges] = '" + txttotalcharges.Text.Replace(",", "") + "',[Delieveryat] = '" + txtdelieveryat.Text + "',[fraight] = '" + txtfraight.Text + "',[vehicleno] = '" + txtvehicleno.Text + "',[grrrno] = '" + txtgrrrno.Text + "',[noofskids] = '" + txtskids.Text + "',[sgstamt]='" + lblsgsttotsl.Text + "',[cgatamt]='" + lblcgattotal.Text + "',[igstamt]='" + lbligsttotal.Text + "',[refno]='" + refnoupdate + "',[totalcess]='" + txttotalcess.Text.Replace(",", "") + "',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',[agentID]='" + cmbagentname.SelectedValue + "' where isactive=1 and id='" + lblid.Text + "' and [billtype]='" + strfinalarray[0] + "'");

                            MessageBox.Show("Data Updated Successfully.");
                            print();
                            clearitem();
                            clearall();
                            clearfooter();
                            LVFO.Items.Clear();
                            LVCHARGES.Items.Clear();


                            BtnPayment.Text = "&Save";
                        }
                        else
                        {
                            //  getsr();
                            Guid guid1;
                            guid1 = Guid.NewGuid();
                            conn.execute("INSERT INTO [dbo].[SaleOrderMaster]([Bill_No],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[BillType],[billno],[totaladdtax],[roudoff],[Duedate],[totalaqty],[totalfree],[totaldiscount],[totaladddiscount],[totalamount],[totalservicejob],[totalcharges],[Delieveryat],[fraight],[vehicleno],[grrrno],[noofskids],[sgstamt],[cgatamt],[igstamt],[OrderStatus],[refno],[totalcess],[agentID],[SyncID],[SyncDatetime],[cusname],[cusadd],[cuscity],[cusphone],[cusmobile],[cuspancard],[cusadhar])VALUES('" + lblbill_no.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + cmbterms.Text + "','" + cmbcustname.SelectedValue + "','" + txtpono.Text.Replace(",", "") + "','" + cmbsaletype.SelectedValue + "'," + lbltotcount.Text.Replace(",", "") + "," + lbltotpqty.Text.Replace(",", "") + "," + lblbasictot.Text.Replace(",", "") + "," + txttottax.Text.Replace(",", "") + "," + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + ",'1','" + txtweight.Text.Replace(",", "") + "','" + txttransport.Text + "','" + txtremarks.Text + "','" + strfinalarray[0] + "','" + TxtBillNo.Text + "','" + txttotaddvat.Text + "','" + txtroundoff.Text + "','" + Convert.ToDateTime(txtduedate.Text).ToString(Master.dateformate) + "'," + txttotaqty.Text.Replace(",", "") + "," + txttotfree.Text.Replace(",", "") + "," + txttotdiscount.Text.Replace(",", "") + "," + txttotadis.Text.Replace(",", "") + "," + txtamt.Text.Replace(",", "") + "," + txttotservice.Text.Replace(",", "") + "," + txttotalcharges.Text.Replace(",", "") + ",'" + txtdelieveryat.Text + "','" + txtfraight.Text + "','" + txtvehicleno.Text + "','" + txtgrrrno.Text + "','" + txtskids.Text + "','" + lblsgsttotsl.Text + "','" + lblcgattotal.Text + "','" + lbligsttotal.Text + "','" + "Pending" + "','" + refno + "','" + txttotalcess.Text.Replace(",", "") + "','" + cmbagentname.SelectedValue + "','" + guid1 + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "','" + txtcustomername.Text + "','" + txtaddress.Text + "','" + txtcity.Text + "','" + txtphone.Text + "','" + txtmobile.Text + "','" + txtpanno.Text + "','" + txtadharno.Text + "')");
                            string fkid = conn.ExecuteScalar("select max(id) from SaleOrderMaster where isactive=1");
                            if (string.IsNullOrEmpty(fkid))
                            {
                                fkid = "1";
                            }
                            for (int i = 0; i < LVFO.Items.Count; i++)
                            {
                                Guid guid;
                                guid = Guid.NewGuid();
                                string productid = conn.ExecuteScalar("select Productid from productmaster where isactive=1 and ProductID='" + LVFO.Items[i].SubItems[24].Text + "'");
                                conn.execute("INSERT INTO [dbo].[SaleOrderProductMaster]([Bill_No],[Bill_Run_Date],[Productname],[Packing],[Bags],[MRP],[Pqty],[Aqty],[Rate],[Per],[Total],[Tax],[Amount],[isactive],[qty],[Billtype],[billno],[addtax],[batch],[free],[discountper],[discountamt],[productid],[sgstper],[sgstamt],[cgstper],[cgstamt],[igstper],[igdtamt],[addtaxper],[serialno],[refno],[cess],[SyncID],[SyncDatetime],[fkid],[ClientID])VALUES('" + lblbill_no.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + LVFO.Items[i].SubItems[0].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[1].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[3].Text.Replace(",", "") + "','','" + LVFO.Items[i].SubItems[5].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[4].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[7].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[8].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[9].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[12].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[14].Text.Replace(",", "") + "','1','" + LVFO.Items[i].SubItems[5].Text.Replace(",", "") + "','" + strfinalarray[0] + "','" + TxtBillNo.Text + "','" + LVFO.Items[i].SubItems[13].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[2].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[6].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[10].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[11].Text.Replace(",", "") + "','" + productid + "','" + LVFO.Items[i].SubItems[15].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[16].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[17].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[18].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[19].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[20].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[21].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[22].Text + "','" + refno + "','" + LVFO.Items[i].SubItems[23].Text.Replace(",", "") + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "','" + fkid + "','" + cmbcustname.SelectedValue + "')");
                                //SqlCommand cmd1 = new SqlCommand("INSERT INTO [dbo].[BillProductMaster]([BillNo],[Bill_Run_Date],[Productname],[Packing],[Bags],[MRP],[Pqty],[Aqty],[Rate],[Per],[Total],[Tax],[addtax],[Amount],[isactive],[qty],[BillType],[Bill_no],[batch],[free],[discountper],[discountamt])VALUES('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + LVFO.Items[i].SubItems[1].Text + "','" + LVFO.Items[i].SubItems[2].Text + "','" + LVFO.Items[i].SubItems[0].Text + "','','" + LVFO.Items[i].SubItems[4].Text + "','','" + LVFO.Items[i].SubItems[5].Text + "','" + LVFO.Items[i].SubItems[6].Text + "','" + LVFO.Items[i].SubItems[7].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[8].Text + "','" + LVFO.Items[i].SubItems[9].Text + "','" + LVFO.Items[i].SubItems[10].Text.Replace(",", "") + "',1,'" + LVFO.Items[i].SubItems[3].Text + "','S','0','" + LVFO.Items[i].SubItems[2].Text + "','" + LVFO.Items[i].SubItems[6].Text + "','','')", con);
                                //cmd1.ExecuteNonQuery();

                            }
                            for (int o = 0; o < temptable.Rows.Count; o++)
                            {
                                Guid guid;
                                guid = Guid.NewGuid();
                                conn.execute("INSERT INTO [dbo].[Serials]([Date],[VoucherID],[TransactionID],[SerialNo],[TransactionType],[VchNo],[PurchaseTypeID],[PurchaseTypeName],[SaleTypeID],[SaleTypeName],[PartyID],[PartyName],[ItemCompanyID],[SyncID],[SyncDatetime],[isactive])VALUES('" + DateTime.Now.ToString(Master.dateformate) + "','" + TxtBillNo.Text + "','" + strfinalarray[0] + "','" + temptable.Rows[o]["SERIAL"].ToString() + "','" + strfinalarray[2] + "','" + txtpono.Text + "','" + cmbsaletype.SelectedValue + "','" + cmbsaletype.Text + "','" + cmbsaletype.SelectedValue + "','" + cmbsaletype.Text + "','" + cmbcustname.SelectedValue + "','" + cmbcustname.Text + "','" + Master.companyId + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',1)");
                            }
                            for (int i = 0; i < LVCHARGES.Items.Count; i++)
                            {
                                Guid guid;
                                guid = Guid.NewGuid();
                                // conn.execute("INSERT INTO [SaleOrderchargesmaster]([billno],[perticulars],[remarks],[value],[at],[plusminus],[amount],[valueofexp],[tax],[sgst],[cgst],[igst],[additax],[billtype],[isactive])VALUES('" + TxtBillNo.Text + "','" + LVCHARGES.Items[i].SubItems[0].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[1].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[2].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[3].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[4].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[5].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[6].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[7].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[8].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[9].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[10].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[11].Text.Replace(",", "") + "','" + strfinalarray[0] + "',1)");
                                conn.execute("INSERT INTO [SaleOrderchargesmaster]([billno],[perticulars],[remarks],[value],[at],[plusminus],[amount],[valueofexp],[tax],[sgst],[cgst],[igst],[additax],[addtaxamt],[billtype],[billsundryid],[SyncID],[SyncDatetime],[fkid],[ClientID],[isactive])VALUES('" + TxtBillNo.Text + "','" + LVCHARGES.Items[i].SubItems[0].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[1].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[2].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[3].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[4].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[5].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[6].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[7].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[8].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[9].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[10].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[11].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[12].Text.Replace(",", "") + "','" + strfinalarray[0] + "','" + LVCHARGES.Items[i].SubItems[13].Text.Replace(",", "") + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "','" + fkid + "','" + cmbcustname.SelectedValue + "',1)");
                            }

                            // SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[BillMaster]([BillNo],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType] ,[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[billtype],[Bill_no],[totaladdtax],[roudoff])VALUES('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + cmbterms.Text + "','" + cmbcustname.SelectedValue + "','" + txtpono.Text + "','" + cmbsaletype.SelectedValue + "'," + lbltotcount.Text + "," + lbltotpqty.Text + "," + lblbasictot.Text + "," + txttottax.Text + "," + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + ",1,'" + txtweight.Text + "','" + txttransport.Text + "','" + txtremarks.Text + "','S','0','" + txttotaddvat.Text + "','" + txtroundoff.Text + "')", con);
                            //cmd.ExecuteNonQuery();
                            if (updateitem != null)
                            {
                                for (int u = 0; u < updateitem.Count(); u++)
                                {
                                    conn.execute("Update SaleOrderMaster SET OrderStatus='Clear',syncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "' where isactive=1 and ClientID='" + cmbcustname.SelectedValue + "' and billno='" + updateitem[u] + "'");
                                }

                            }
                            MessageBox.Show("Data Inserted Successfully.");
                            if (updateitem != null)
                            {
                                updateitem = new string[updateitem.Count()];
                            }
                            print();
                            clearitem();
                            clearall();
                            clearfooter();
                            LVFO.Items.Clear();
                            LVCHARGES.Items.Clear();



                        }
                    }
                    else
                    {
                        MessageBox.Show("please fill all information");
                    }
                }
                else
                {
                    MessageBox.Show("please fill all information");
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

        private void clearfooter()
        {
            lbltotcount.Text = "0";
            lbltotpqty.Text = "0";
            txttotaqty.Text = "0";
            txttotfree.Text = "0";
            lblbasictot.Text = "0";
            txttotdiscount.Text = "0";
            txttotadis.Text = "0";
            txttottax.Text = "0";
            txttotaddvat.Text = "0";
            txtamt.Text = "0";
            txttotservice.Text = "0";
            txttotalcharges.Text = "0";
            txtroundoff.Text = "0";
            TxtBillTotal.Text = "0";
            lblsgsttotsl.Text = "0";
            lblcgattotal.Text = "0";
            lbligsttotal.Text = "0";

        }

        double addtax;
        double taxid, addtaxid;
        private void txtamt_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    ListViewItem li;
                    lbliteminfo.Visible = false;
                    li = LVFO.Items.Add(txtitemname.Text);
                    li.SubItems.Add(txtpacking.Text);
                    li.SubItems.Add("");
                    li.SubItems.Add(txtbags.Text);
                    li.SubItems.Add((Math.Round(Convert.ToDouble(txtqty.Text), 2).ToString()));
                    li.SubItems.Add((Math.Round(Convert.ToDouble(txtpqty.Text), 2).ToString()));
                    li.SubItems.Add(txtfree.Text);
                    li.SubItems.Add((Math.Round(Convert.ToDouble(txtrate.Text), 2).ToString()));
                    li.SubItems.Add(txtper.Text);
                    li.SubItems.Add((Math.Round(Convert.ToDouble(txttotal.Text), 2).ToString()));
                    li.SubItems.Add((Math.Round(Convert.ToDouble(txtdisper.Text), 2).ToString()));
                    li.SubItems.Add((Math.Round((Convert.ToDouble(txtdisamt.Text)), 2).ToString()));
                    li.SubItems.Add((Math.Round((Convert.ToDouble(txttax.Text)), 2).ToString()));
                    li.SubItems.Add((Math.Round((Convert.ToDouble(txtaddtax.Text)), 2).ToString()));
                    li.SubItems.Add((Math.Round(Convert.ToDouble(txtamount.Text), 2).ToString()));


                    totalcalculation();
                    clearitem();
                    txtitemname.Focus();
                }
            }
        }
        public void clearallitem()
        {
            txtpacking.Text = "0";
            // cmbbatch.SelectedIndex = 0;
            txtbags.Text = "1";
            // txtqty.Text = "0";
            txtpqty.Text = "1";
            //  txtfree.Text = "0";
            txtrate.Text = "0";
            // txtper.Text = "0";
            txttotal.Text = "0";
            txtdisper.Text = "0";
            txtdisamt.Text = "0";
            txttax.Text = "0";
            txtaddtax.Text = "0";
            txtamount.Text = "0";
        }
        private DataTable changedtclone(DataTable dt)
        {
            DataTable dtClone = dt.Clone(); //just copy structure, no data
            for (int i = 0; i < dtClone.Columns.Count; i++)
            {
                if (dtClone.Columns[i].DataType != typeof(string))
                    dtClone.Columns[i].DataType = typeof(string);
            }

            foreach (DataRow dr in dt.Rows)
            {
                dtClone.ImportRow(dr);
            }
            return dtClone;
        }
        decimal debit1 = 0;
        decimal credit1 = 0;
        private string getbalance(double opbal, double p, String DC, String actualdc, int i)
        {
            double balance;
            string bal = "";

            if (actualdc == "C")
            {
                balance = opbal + p;
                bal = balance.ToString("N2") + "";
            }

            else
            {
                balance = opbal - p;
                bal = balance.ToString("N2") + "";
            }

            return bal;
        }
        private void getstock(string p)
        {
            #region
            string proid = conn.ExecuteScalar("select Productid from ProductMaster where isactive=1 and Product_Name='" + p + "'");
            string openingstockfromitem = conn.ExecuteScalar("select opstock from productpricemaster where isactive=1 and productid= '" + proid + "'");
            string totalPurchase = "";
            string totalSale = "";
            if (totalPurchase == "" || totalPurchase == "NULL")
            {
                totalPurchase = "0.00";
            }
            if (totalSale == "" || totalSale == "NULL")
            {
                totalSale = "0.00";
            }
            Double opbal;
            string DC = "";
            if (Convert.ToDouble(totalPurchase) >= Convert.ToDouble(totalSale))
            {
                opbal = Convert.ToDouble(totalPurchase) - Convert.ToDouble(totalSale) + Convert.ToDouble(openingstockfromitem);
                // txtopbal.Text = opbal.ToString("N2");
                DC = "D";
            }
            else
            {
                opbal = Convert.ToDouble(totalSale) - Convert.ToDouble(totalPurchase) + Convert.ToDouble(openingstockfromitem);
                // txtopbal.Text = opbal.ToString("N2");
                DC = "C";
            }
            #endregion

            #region
            DataTable pos = conn.getdataset("select 'POS' as Billtype,BillDate as Bill_Run_Date,billno,totalnet as Rate,totalqty as pqty,BillId as Bill_No  from BillPOSMaster where isactive=1 order by BillDate asc");
            pos = changedtclone(pos);

            DataTable SPdt = conn.getdataset("select Billtype,Bill_Run_Date,billno,Rate,Pqty,Bill_No from billproductmaster where isactive=1 and productid='" + proid + "' order by bill_Run_Date");
            string balance = "0.00";
            balance = Convert.ToString(opbal);
            Double debit = 0, credit = 0;
            SPdt = changedtclone(SPdt);
            SPdt.Merge(pos);
            SPdt.DefaultView.Sort = "[bill_Run_Date] DESC";
            SPdt = SPdt.DefaultView.ToTable();
            debit = 0;
            credit = 0;
            debit1 = 0;
            credit1 = 0;
            for (int i = 0; i < SPdt.Rows.Count; i++)
            {
                if (SPdt.Rows[i]["Billtype"].ToString() == "S")
                {
                    if (i != 0)
                    {
                        string[] str = balance.Split(' ');
                        char temp = str[0][0];
                        DC = temp.ToString();
                        opbal = Convert.ToDouble(str[0]);
                    }
                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "D", i);
                }
                else if (SPdt.Rows[i]["Billtype"].ToString() == "POS")
                {
                    if (i != 0)
                    {
                        string[] str = balance.Split(' ');
                        char temp = str[0][0];
                        DC = temp.ToString();
                        opbal = Convert.ToDouble(str[0]);
                    }
                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "D", i);
                }
                else if (SPdt.Rows[i]["Billtype"].ToString() == "SR")
                {
                    if (i != 0)
                    {
                        string[] str = balance.Split(' ');
                        char temp = str[0][0];
                        DC = temp.ToString();
                        opbal = Convert.ToDouble(str[0]);
                    }
                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "C", i);
                }
                else if (SPdt.Rows[i]["Billtype"].ToString() == "P")
                {
                    if (i != 0)
                    {
                        string[] str = balance.Split(' ');
                        char temp = str[0][0];
                        DC = temp.ToString();
                        opbal = Convert.ToDouble(str[0]);
                    }
                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "C", i);
                }
                else if (SPdt.Rows[i]["Billtype"].ToString() == "PR")
                {
                    if (i != 0)
                    {
                        string[] str = balance.Split(' ');
                        char temp = str[0][0];
                        DC = temp.ToString();
                        opbal = Convert.ToDouble(str[0]);
                    }
                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "D", i);
                }

            }
            DataTable stockadjustment = conn.getdataset("select sm.*,sim.* from stockadujestmentmaster sm inner join stockadujestmentitemmaster sim on sm.id=sim.stockid where sm.isactive=1 and sim.isactive=1 and sim.itemid='" + proid + "' order by sm.stockdate");
            for (int i = 0; i < stockadjustment.Rows.Count; i++)
            {

                if (Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString()) > 0)
                {
                    credit += Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString());
                }
                else
                {
                    Double d = Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString());
                    Double a1 = d * -1;
                    debit += Convert.ToDouble(a1);
                }
                Double bal = Convert.ToDouble(balance);
                Double astock = Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString());
                Double fbalance = bal + astock;
                balance = Convert.ToString(fbalance);
            }
            string minstock = conn.ExecuteScalar("select minstock from ProductMaster where isactive=1 and ProductID='" + proid + "'");
            Double a = Convert.ToDouble(minstock);
            Double b = Convert.ToDouble(balance);
            if (a >= b)
            {
                MessageBox.Show(balance, "Min Stock level");
            }
            #endregion
        }
        public void enteritem()
        {
            #region
            //x = 0;
            //SqlCommand cmd5 = new SqlCommand("select p.*,b.* from productmaster p inner join ProductPriceMaster b on p.ProductID=b.ProductID where p.product_name='" + txtitemname.Text + "'", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd5);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);
            //lbliteminfo.Visible = true;
            //qtyflag = 0;
            //txtpacking.Text = dt.Rows[0]["Packing"].ToString();
            //DataTable dis= conn.getdataset("select specialrate,discount from partyrates where (partyid=0 or partyid='" + cmbcustname.SelectedValue + "') and itemid=" + dt.Rows[0]["productid"].ToString());
            //if (dis.Rows.Count > 0)
            //{
            //    if (Convert.ToDouble(dis.Rows[0]["specialrate"].ToString()) > 0)
            //    {
            //        txtrate.Text = dis.Rows[0]["specialrate"].ToString();
            //    }
            //    else
            //    {
            //        txtrate.Text = dt.Rows[0]["SalePrice"].ToString();
            //    }
            //    if (Convert.ToDouble(dis.Rows[0]["discount"].ToString()) > 0)
            //    {
            //        txtdisper.Text = dis.Rows[0]["discount"].ToString();
            //    }
            //    else
            //    {
            //        txtdisper.Text = dt.Rows[0]["discount"].ToString();
            //    }
            //}
            //else
            //{

            //    txtdisper.Text = "0.00";
            //}

            //txtper.Text = dt.Rows[0]["Unit"].ToString();
            //lblbagqty.Text = "[" + dt.Rows[0]["Unit"].ToString() + "]";
            //lblaltqty.Text = "[" + dt.Rows[0]["Altunit"].ToString() + "]";
            //txtqty.Text = dt.Rows[0]["Convfactor"].ToString();
            //txtdisamt.Text = "0.00";

            //txtfree.Text = "0";
            //lbliteminfo.Text = "Sale Price=" + dt.Rows[0]["SalePrice"].ToString() + "  MRP=" + dt.Rows[0]["MRP"].ToString() + "  Basic=" + dt.Rows[0]["BasicPrice"].ToString() + "  Prch Price=" + dt.Rows[0]["SalePrice"].ToString();
            //SqlCommand cmd6 = new SqlCommand("select * from TaxSlabMaster i inner join productmaster p on i.Taxslabname=p.taxslab where p.product_name like'%" + txtitemname.Text + "%' and i.saletypename= '%" + cmbsaletype.Text + "%'", con);
            //SqlDataAdapter sda6 = new SqlDataAdapter(cmd6);
            //DataTable dt1 = new DataTable();
            //sda6.Fill(dt1);
            //string istax = conn.ExecuteScalar("Select taxtypename from Purchasetypemaster where isactive=1 and type='S' and Purchasetypename='" + cmbsaletype.Text + "'");
            //if (dt1.Rows.Count > 0)
            //{
            //    txttax.Text = "0";
            //    txtaddtax.Text = "0";
            //    if (istax != "Tax Free")
            //    {
            //        lbltax1.Text = "[" + Math.Round(Convert.ToDouble(dt1.Rows[0]["Vat"].ToString()), 2).ToString() + "]";
            //        lbladdtax1.Text = "[" + Math.Round(Convert.ToDouble(dt1.Rows[0]["AddVat"].ToString()), 2).ToString() + "]";
            //    }
            //    else
            //    {
            //        lbltax1.Text = "[0]";
            //        lbladdtax1.Text = "[0]";
            //    }

            //    txtpacking.Focus();
            //    taxid = Math.Round(Convert.ToDouble(dt1.Rows[0]["Vat"].ToString()), 2);
            //    addtaxid = Math.Round(Convert.ToDouble(dt1.Rows[0]["AddVat"].ToString()), 2);

            //}
            //else
            //{

            //    MessageBox.Show("Not any Tax Available For This Sale Type");
            //    txttax.Text = "0";
            //    txtaddtax.Text = "0";
            //    lbltax1.Text = "[0]";
            //    lbladdtax1.Text = "[0]";
            //    taxid = 0;
            //    addtaxid = 0;
            //    txtpacking.Focus();
            //}
            #endregion
            pnlallitem.Visible = false;
            string v = txtitemname.Text.Replace("*", "");
            txtitemname.Text = v;
            if (txtitemname.Text == "")
            {
                MessageBox.Show("Please Select Item ");
                this.ActiveControl = txtitemname;
                return;
            }
            clearallitem();
            DataTable getitemid = conn.getdataset("select * from ProductPriceMaster where isactive=1 and Barcode='" + txtitemname.Text + "'");
            if (getitemid.Rows.Count > 0)
            {
                DataTable getitemname = conn.getdataset("select * from ProductMaster where isactive=1 and ProductID='" + getitemid.Rows[0]["ProductID"].ToString() + "'");
                txtitemname.Text = getitemname.Rows[0]["Product_Name"].ToString();
                //  cmbbatch.Text = getitemid.Rows[0]["BatchNo"].ToString();

            }
            dt = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + cmbsaletype.Text + "'");
            dt1 = conn.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + txtitemname.Text + "'");
            itemname = txtitemname.Text;
            if (options.Rows[0]["requirstockinfo"].ToString() == "True")
            {
                getstock(txtitemname.Text);
            }
            if (dt1.Rows.Count <= 0)
            {
                MessageBox.Show("Item Not Available");
                return;
            }
            dt2 = conn.getdataset("select * from TaxSlabMaster i inner join productmaster p on i.Taxslabname=p.taxslab where p.isactive=1 and i.isactive=1 and p.product_name='" + txtitemname.Text + "' and i.saletypename= '" + cmbsaletype.Text + "'");
            dt3 = conn.getdataset("select * from ProductPriceMaster where isactive=1 and Productid='" + dt1.Rows[0]["ProductID"].ToString() + "'");
            productid = dt1.Rows[0]["ProductID"].ToString();
            txtpacking.Text = dt1.Rows[0]["Packing"].ToString();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Please Select Type", "Select", MessageBoxButtons.OK);
                cmbsaletype.Focus();
            }
            else
            {
                if (dt.Rows[0]["Region"].ToString() == "Local")
                {
                    if (dt2.Rows.Count > 0 && dt3.Rows.Count > 0)
                    {
                        lbliteminfo.Text = "Tax=" + dt2.Rows[0]["Sgst"].ToString() + "% + " + dt2.Rows[0]["cgst"].ToString() + "%,AddTax=" + dt2.Rows[0]["additax"].ToString() + ",Sale Price=" + dt3.Rows[0]["SalePrice"].ToString() + ",MRP=" + dt3.Rows[0]["MRP"].ToString() + ",Basic=" + dt3.Rows[0]["BasicPrice"].ToString() + ",PurchasePrice=" + dt3.Rows[0]["PurchasePrice"].ToString() + "";
                    }
                    else if (dt2.Rows.Count > 0 && dt3.Rows.Count <= 0)
                    {
                        lbliteminfo.Text = "Tax=" + dt2.Rows[0]["Sgst"].ToString() + "% + " + dt2.Rows[0]["cgst"].ToString() + "%,AddTax=" + dt2.Rows[0]["additax"].ToString() + ",Sale Price=" + 0 + ",MRP=" + 0 + ",Basic=" + 0 + ",PurchasePrice=" + 0 + "";
                    }
                    else if (dt2.Rows.Count <= 0 && dt3.Rows.Count > 0)
                    {
                        lbliteminfo.Text = "Tax=" + 0 + "% + " + 0 + "%,AddTax=" + 0 + ",Sale Price=" + dt3.Rows[0]["SalePrice"].ToString() + ",MRP=" + dt3.Rows[0]["MRP"].ToString() + ",Basic=" + dt3.Rows[0]["BasicPrice"].ToString() + ",PurchasePrice=" + dt3.Rows[0]["PurchasePrice"].ToString() + "";
                    }
                    else if (dt2.Rows.Count <= 0 && dt3.Rows.Count <= 0)
                    {
                        lbliteminfo.Text = "Tax=" + 0 + "% + " + 0 + "%,AddTax=" + 0 + ",Sale Price=" + 0 + ",MRP=" + 0 + ",Basic=" + 0 + ",PurchasePrice=" + 0 + "";
                    }
                }
                else
                {
                    if (dt2.Rows.Count > 0 && dt3.Rows.Count > 0)
                    {
                        lbliteminfo.Text = "Tax=" + dt2.Rows[0]["Igst"].ToString() + "%,AddTax=" + dt2.Rows[0]["additax"].ToString() + ",Sale Price=" + dt3.Rows[0]["SalePrice"].ToString() + ",MRP=" + dt3.Rows[0]["MRP"].ToString() + ",Basic=" + dt3.Rows[0]["BasicPrice"].ToString() + ",PurchasePrice=" + dt3.Rows[0]["PurchasePrice"].ToString() + "";
                    }
                    else if (dt2.Rows.Count > 0 && dt3.Rows.Count <= 0)
                    {
                        lbliteminfo.Text = "Tax=" + dt2.Rows[0]["Igst"].ToString() + "%,AddTax=" + dt2.Rows[0]["additax"].ToString() + ",Sale Price=" + 0 + ",MRP=" + 0 + ",Basic=" + 0 + ",PurchasePrice=" + 0 + "";
                    }
                    else if (dt2.Rows.Count <= 0 && dt3.Rows.Count > 0)
                    {
                        lbliteminfo.Text = "Tax=" + 0 + "%,AddTax=" + 0 + ",Sale Price=" + dt3.Rows[0]["SalePrice"].ToString() + ",MRP=" + dt3.Rows[0]["MRP"].ToString() + ",Basic=" + dt3.Rows[0]["BasicPrice"].ToString() + ",PurchasePrice=" + dt3.Rows[0]["PurchasePrice"].ToString() + "";
                    }
                    else if (dt2.Rows.Count <= 0 && dt3.Rows.Count <= 0)
                    {
                        lbliteminfo.Text = "Tax=" + 0 + "%,AddTax=" + 0 + ",Sale Price=" + 0 + ",MRP=" + 0 + ",Basic=" + 0 + ",PurchasePrice=" + 0 + "";
                    }
                    // lbliteminfo.Text = "Tax=" + dt2.Rows[0]["Igst"].ToString() + "%,AddTax=" + dt2.Rows[0]["additax"].ToString() + ",Sale Price=" + dt3.Rows[0]["SalePrice"].ToString() + ",MRP=" + dt3.Rows[0]["MRP"].ToString() + ",Basic=" + dt3.Rows[0]["BasicPrice"].ToString() + "";
                }
            }
            if (Convert.ToBoolean(dt1.Rows[0]["IsBatch"].ToString()) == true)
            {
                bindbatch();
                int count = cmbbatch.Items.Count - 1;
                cmbbatch.SelectedIndex = count;
                cmbbatch.DroppedDown = true;
                //  SqlCommand cmd = new SqlCommand("select Productid,Batchno from ProductPriceMaster where Productid='" + dt1.Rows[0]["ProductID"].ToString() + "' and isactive='1'", con);
                //  SqlDataAdapter sda = new SqlDataAdapter(cmd);
                //  DataTable dt4 = new DataTable();
                //  sda.Fill(dt4);

                //  cmbbatch.ValueMember = "Productid";
                //  cmbbatch.DisplayMember = "Batchno";
                //  cmbbatch.DataSource = dt4;
                ////  cmbbatch.SelectedIndex = -1;
                //  cmbbatch.Focus();
            }
            else
            {

                cmbbatch.SelectedIndex = -1;
                cmbbatch.Enabled = false;
                //txtbags.Focus();
                x = 0;
                DataTable dprice = new DataTable();
                dprice = conn.getdataset("select PickupPrice from PurchasetypeMaster where isactive=1 and FormType='" + strfinalarray[0] + "' and Purchasetypename='" + cmbsaletype.Text + "'");
                Dprice = dprice.Rows[0]["PickupPrice"].ToString();
                SqlCommand cmd5 = new SqlCommand("select p.*,b.*,b." + Dprice + " as Dprice from productmaster p inner join ProductPriceMaster b on p.ProductID=b.ProductID where p.isactive=1 and b.isactive=1 and p.product_name='" + txtitemname.Text + "'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd5);
                DataTable dt9 = new DataTable();
                sda.Fill(dt9);
                lbliteminfo.Visible = true;
                qtyflag = 0;
                double[] dis = new double[2];
                dis = calculatediscount(dt9.Rows[0]["Product_Name"].ToString(), cmbcustname.Text, Convert.ToDouble(dt9.Rows[0]["Dprice"].ToString()));
                if (Convert.ToDouble(dis[1].ToString()) != 0)
                {
                    txtrate.Text = dis[1].ToString();
                }
                else
                {
                    txtrate.Text = dt9.Rows[0]["Dprice"].ToString();
                }
                txtdisper.Text = dis[0].ToString();
                //DataTable dis = conn.getdataset("select specialrate,discount from partyrates where (partyid=0 or partyid='" + cmbcustname.SelectedValue + "') and itemid=" + dt9.Rows[0]["productid"].ToString());
                //if (dis.Rows.Count > 0)
                //{
                //    if (Convert.ToDouble(dis.Rows[0]["specialrate"].ToString()) > 0)
                //    {
                //        txtrate.Text = dis.Rows[0]["specialrate"].ToString();
                //    }
                //    else
                //    {
                //        txtrate.Text = dt9.Rows[0]["Dprice"].ToString();
                //    }
                //    if (Convert.ToDouble(dis.Rows[0]["discount"].ToString()) > 0)
                //    {

                //        string disc = Convert.ToString(dis.Rows[0]["discount"].ToString());
                //        txtdisper.Text = disc;


                //        txtdisper.Text = dis.Rows[0]["discount"].ToString();
                //        txtdisper.Text = Convert.ToDouble(dis.Rows[0]["discount"].ToString()).ToString();
                //    }
                //    else
                //    {
                //        txtdisper.Text = dis.Rows[0]["discount"].ToString();
                //    }
                //}

                //else
                //{
                //    txtrate.Text = dt9.Rows[0]["Dprice"].ToString();
                //    txtdisper.Text = "0.00";
                //}

                txtper.Text = dt9.Rows[0]["Unit"].ToString();
                lblbagqty.Text = "[" + dt9.Rows[0]["Unit"].ToString() + "]";
                lblaltqty.Text = "[" + dt9.Rows[0]["Altunit"].ToString() + "]";
                txtqty.Text = dt9.Rows[0]["Convfactor"].ToString();
                txtdisamt.Text = "0.00";

                txtfree.Text = "0";
                SqlCommand cmd6 = new SqlCommand("select * from TaxSlabMaster i inner join productmaster p on i.Taxslabname=p.taxslab where p.isactive=1 and i.isactive=1 and p.product_name='" + txtitemname.Text + "' and i.saletypename= '" + cmbsaletype.Text + "'", con);
                SqlDataAdapter sda6 = new SqlDataAdapter(cmd6);
                DataTable dt10 = new DataTable();
                sda6.Fill(dt10);
                string istax = conn.ExecuteScalar("Select taxtypename from Purchasetypemaster where isactive=1 and FormType='" + strfinalarray[0] + "' and Purchasetypename='" + cmbsaletype.Text + "'");
                if (dt10.Rows.Count > 0)
                {
                    txttax.Text = "0";
                    txtaddtax.Text = "0";
                    if (istax != "Tax Free")
                    {
                        lbltax1.Text = "[" + Math.Round(Convert.ToDouble(dt10.Rows[0]["igst"].ToString()), 2).ToString() + "]";
                        taxforprice = lbltax1.Text;
                        taxper = dt10.Rows[0]["igst"].ToString();
                        lbladdtax1.Text = "[" + Math.Round(Convert.ToDouble(dt10.Rows[0]["additax"].ToString()), 2).ToString() + "]";
                        ataxforprice = lbladdtax1.Text;
                        additaxper = dt10.Rows[0]["additax"].ToString();
                        taxid = Math.Round(Convert.ToDouble(dt10.Rows[0]["igst"].ToString()), 2);
                        addtaxid = Math.Round(Convert.ToDouble(dt10.Rows[0]["additax"].ToString()), 2);
                    }
                    else
                    {
                        lbltax1.Text = "[0]";
                        taxforprice = lbltax1.Text;
                        lbladdtax1.Text = "[0]";
                        ataxforprice = lbladdtax1.Text;
                        taxid = 0;
                        addtaxid = 0;

                    }

                    txtpacking.Focus();


                }
                else
                {

                    //MessageBox.Show("Not any Tax Available For This Sale Type");
                    txttax.Text = "0";
                    txtaddtax.Text = "0";
                    lbltax1.Text = "[0]";
                    taxforprice = lbltax1.Text;
                    lbladdtax1.Text = "[0]";
                    ataxforprice = lbladdtax1.Text;
                    taxid = 0;
                    addtaxid = 0;
                    txtpacking.Focus();
                }
                txtbags.Focus();
            }
        }
        private void txtitemname_KeyDown(object sender, KeyEventArgs e)
        {

            {
                try
                {
                    if (e.KeyCode == Keys.Down)
                    {
                        try
                        {
                            // lvallitem.Items[0].Selected = true;
                            lvallitem.Items[0].Selected = true;
                            lvallitem.Select();
                        }
                        catch
                        {
                        }
                    }
                    if (e.KeyCode == Keys.Enter)
                    {

                        enteritem();



                    }
                    if (e.KeyCode == Keys.F3)
                    {
                        var privouscontroal = txtitemname;
                        activecontroal = privouscontroal.Name;
                        Itementry client = new Itementry(this, master, tabControl, activecontroal);

                        client.Passed(1);
                        //client.Show();
                        // autoreaderbind();
                        master.AddNewTab(client);

                    }
                    if (e.KeyCode == Keys.F2)
                    {
                        if (txtitemname.Text != "")
                        {
                            var privouscontroal = txtitemname;
                            activecontroal = privouscontroal.Name;
                            Itementry client = new Itementry(this, master, tabControl, activecontroal);
                            client.Updatefromsale(txtitemname.Text);
                            client.Passed(1);
                            //client.Show();
                            master.AddNewTab(client);
                        }
                    }
                }
                catch
                {
                }
            }
        }
        private void itemcalculation(String qty)
        {
            try
            {
                SqlCommand cmd5 = new SqlCommand("select convfactor from ProductMaster where isactive=1 and product_name='" + txtitemname.Text + "'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd5);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Double convfactor = Convert.ToDouble(dt.Rows[0]["convfactor"].ToString());

                double total = Convert.ToDouble(qty) * Convert.ToDouble(convfactor);

                double finaltotal = Convert.ToDouble(qty) * Convert.ToDouble(txtrate.Text);

                txttotal.Text = Math.Round(finaltotal, 2).ToString();
                if (!txtdisamt.Focused)
                {
                    txtdisamt.Text = (Math.Round((Convert.ToDouble(txtdisper.Text) * Convert.ToDouble(txttotal.Text)) / 100, 2)).ToString();
                }
                double discount = Convert.ToDouble(txttotal.Text) - Convert.ToDouble(txtdisamt.Text);


                string istax = conn.ExecuteScalar("Select taxtypename from Purchasetypemaster where isactive=1 and FormType='" + strfinalarray[0] + "' and Purchasetypename='" + cmbsaletype.Text + "'");
                if (istax != "Tax Free")
                {
                    double tax = Math.Round(discount * (Convert.ToDouble(taxid.ToString())) / 100, 2);
                    double addtax = Math.Round(discount * (Convert.ToDouble(addtaxid.ToString())) / 100, 2);
                    txttax.Text = tax.ToString();
                    txtaddtax.Text = addtax.ToString();

                }
                else
                {
                    txttax.Text = "0";
                    txtaddtax.Text = "0";
                    taxid = 0;
                    addtaxid = 0;
                }
                double amount = Math.Round(discount + ((discount * (Convert.ToDouble(taxid.ToString()) + Convert.ToDouble(addtaxid.ToString()))) / 100), 2);
                txtamount.Text = Math.Round(amount, 2).ToString();
            }
            catch
            {
            }
        }
        private void totalcalculation()
        {
            try
            {

                Int32 count = 0;
                Double total = 0;
                Double vat = 0, basic = 0, discount = 0;
                Double addvat = 0;
                Double sgsttotal = 0;
                Double cgsttotal = 0;
                Double igsttotal = 0;
                Double totalcess = 0;
                Double pqty = 0, aqty = 0, free = 0;
                for (int i = 0; i < LVFO.Items.Count; i++)
                {
                    count++;

                    aqty = aqty + Convert.ToDouble(LVFO.Items[i].SubItems[3].Text);
                    pqty = pqty + Convert.ToDouble(LVFO.Items[i].SubItems[5].Text);
                    free = free + Convert.ToDouble(LVFO.Items[i].SubItems[6].Text);
                    basic = basic + Convert.ToDouble(LVFO.Items[i].SubItems[9].Text);
                    discount += Convert.ToDouble(LVFO.Items[i].SubItems[11].Text);
                    vat += Convert.ToDouble(LVFO.Items[i].SubItems[12].Text);
                    addvat += Convert.ToDouble(LVFO.Items[i].SubItems[13].Text);
                    total = total + Convert.ToDouble(LVFO.Items[i].SubItems[14].Text);
                    Double multi = 0, add = 0;
                    sgsttotal = sgsttotal + Convert.ToDouble(LVFO.Items[i].SubItems[16].Text);
                    cgsttotal = cgsttotal + Convert.ToDouble(LVFO.Items[i].SubItems[18].Text);
                    igsttotal = igsttotal + Convert.ToDouble(LVFO.Items[i].SubItems[20].Text);
                    totalcess = totalcess + Convert.ToDouble(LVFO.Items[i].SubItems[23].Text);
                    //multi = (Convert.ToDouble(LVFO.Items[i].SubItems[7].Text) * (Convert.ToDouble(LVFO.Items[i].SubItems[8].Text) / 100));
                    //vat = vat + multi;
                    //add = (Convert.ToDouble(LVFO.Items[i].SubItems[7].Text) * (Convert.ToDouble(LVFO.Items[i].SubItems[9].Text) / 100));
                    //addvat += add;

                }
                try
                {
                    for (int j = 0; j < LVCHARGES.Items.Count; j++)
                    {
                        sgsttotal = sgsttotal + Convert.ToDouble(LVCHARGES.Items[j].SubItems[8].Text);
                        cgsttotal = cgsttotal + Convert.ToDouble(LVCHARGES.Items[j].SubItems[9].Text);
                        igsttotal = igsttotal + Convert.ToDouble(LVCHARGES.Items[j].SubItems[10].Text);
                        addvat = addvat + Convert.ToDouble(LVCHARGES.Items[j].SubItems[11].Text);
                    }
                }
                catch
                {
                    sgsttotal += 0;
                    cgsttotal += 0;
                    igsttotal += 0;
                    addvat += 0;
                }
                lbltotcount.Text = count.ToString("");
                lbltotpqty.Text = pqty.ToString("");
                txttotfree.Text = free.ToString("");
                txttotaqty.Text = aqty.ToString();
                lblbasictot.Text = basic.ToString();
                txttotdiscount.Text = discount.ToString("N2");
                txttottax.Text = Math.Round(vat, 2).ToString("N2");
                txttotaddvat.Text = Math.Round(addvat, 2).ToString("N2");
                txtamt.Text = Math.Round(total, 2).ToString("N2");
                lblsgsttotsl.Text = sgsttotal.ToString("");
                lblcgattotal.Text = cgsttotal.ToString("");
                lbligsttotal.Text = igsttotal.ToString("");
                txttotalcess.Text = totalcess.ToString("");
                getOptions(Math.Round(total, 2));


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Messagebox:" + ex.Message);
            }
        }

        private void getOptions(Double total)
        {

            DataTable dt = options;
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dt.Rows[0]["autoroundoffsale"].ToString()) == true)
                {

                    double charges = Convert.ToDouble(txttotalcharges.Text);
                    //   double cess = Convert.ToDouble(txttotalcess.Text);
                    TxtBillTotal.Text = Math.Round(total + charges, 0).ToString("N2");
                    txtroundoff.Text = Math.Round((Math.Round(Convert.ToDouble(TxtBillTotal.Text), 0) - Convert.ToDouble(total + charges)), 2).ToString();


                }
                else
                {
                    double charges = Convert.ToDouble(txttotalcharges.Text);
                    //  double cess = Convert.ToDouble(txttotalcess.Text);
                    TxtBillTotal.Text = Math.Round(total + charges, 2).ToString("N2");
                    txtroundoff.Text = "0";

                }
            }

        }
        int qtyflag;

        private void txtpqty_TextChanged(object sender, EventArgs e)
        {

            {
                try
                {
                    if (!txtbags.Focused)
                    {
                        //if (qtyflag == 0)
                        //{
                        //    qtyflag = 1;
                        if (txtpqty.Text != "")
                        {
                            if (txtqty.Text != "")
                            {
                                Double qty = Convert.ToDouble(Convert.ToDouble(txtpqty.Text) / Convert.ToDouble(txtqty.Text));

                                txtbags.Text = qty.ToString();

                            }
                            else
                            {
                                Double qty = Convert.ToDouble(Convert.ToDouble(txtpqty.Text) / 1);
                                txtbags.Text = qty.ToString();

                            }
                        }
                        else
                        {
                            if (txtqty.Text != "")
                            {
                                Double qty = Convert.ToDouble(1 * Convert.ToDouble(txtqty.Text));
                                txtbags.Text = qty.ToString();

                            }
                            else
                            {
                                txtpqty.Text = "0.00";
                            }

                        }
                        if (txtpqty.Text != "" && txtpqty.Text != "0.00")
                        {
                            itemcalculation(txtpqty.Text);

                        }
                        else
                        {
                            itemcalculation("0");
                        }
                        //  qtyflag = 0;
                    }
                }
                catch
                {
                    // qtyflag = 0;
                }


                try
                {
                    if (txtpqty.Text != "")
                    {


                    }
                    else
                    {
                        itemcalculation("1");
                    }
                }
                catch
                {
                    qtyflag = 0;
                }
            }
        }
        Int32 rowid = -1;
        private void LVFO_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            {
                if (LVFO.SelectedItems.Count > 0)
                {
                    rowid = LVFO.FocusedItem.Index;
                    lbliteminfo.Visible = true;

                    string dt11 = conn.ExecuteScalar("select Product_Name from ProductMaster where isactive=1 and ProductID='" + LVFO.Items[LVFO.FocusedItem.Index].SubItems[24].Text + "'");
                    txtitemname.Text = dt11;
                    //dt8 = conn.getdataset("select Productid,Batchno from ProductPriceMaster where Productid='" + dt11.Rows[0]["ProductID"].ToString() + "' and isactive='1'");
                    //DataRow dr;
                    //dr = dt8.NewRow();
                    //dr["Batchno"] = "Select Batch";
                    //dt8.Rows.InsertAt(dr, 0);
                    //cmbbatch.DataSource = dt8;
                    //cmbbatch.DisplayMember = "Batchno";
                    //cmbbatch.ValueMember = "Productid";
                    //bindbatch();
                    dt8 = new DataTable();
                    SqlCommand cmd1 = new SqlCommand("select Productid,Batchno from ProductPriceMaster where Productid='" + LVFO.Items[LVFO.FocusedItem.Index].SubItems[24].Text + "' and isactive='1'", con);
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    sda1.Fill(dt8);
                    cmbbatch.ValueMember = "Productid";
                    cmbbatch.DisplayMember = "Batchno";
                    cmbbatch.DataSource = dt8;
                    cmbbatch.Focus();
                    cmbbatch.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[2].Text;
                    if (LVFO.Items[LVFO.FocusedItem.Index].SubItems[2].Text == "")
                    {
                        cmbbatch.Enabled = false;
                        cmbbatch.SelectedIndex = -1;
                    }
                    else
                    {
                        cmbbatch.Enabled = true;

                    }
                    
                    txtpacking.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[1].Text;
                    txtbags.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[3].Text;
                    txtqty.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[4].Text;
                    txtpqty.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[5].Text;
                    txtfree.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[6].Text;
                    txtrate.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[7].Text;
                    txtper.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[8].Text;
                    txttotal.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[9].Text;
                    txtdisper.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[10].Text;
                    txtdisamt.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[11].Text;
                    txttax.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[12].Text;
                    txtaddtax.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[13].Text;
                    txtamount.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[14].Text;
                    string serial = LVFO.Items[LVFO.FocusedItem.Index].SubItems[22].Text;
                    LVFO.Items[LVFO.FocusedItem.Index].SubItems[22].Text = "";
                    if (!string.IsNullOrEmpty(serial))
                    {
                        string[] values = serial.Split(',');
                        temptable = new DataTable();
                        temptable.Columns.Add("Itemname", typeof(string));
                        temptable.Columns.Add("SERIAL", typeof(string));
                        for (int i = 0; i < values.Length; i++)
                        {
                            ListViewItem li = new ListViewItem();
                            li = lvserial.Items.Add(values[i] + Environment.NewLine);
                            temptable.Rows.Add(txtitemname.Text, values[i]);
                        }
                    }
                    SqlCommand cmd5 = new SqlCommand("select p.*,b.* from productmaster p inner join ProductPriceMaster b on p.ProductID=b.ProductID where p.isactive=1 and b.isactive=1 and p.ProductID='" + LVFO.Items[LVFO.FocusedItem.Index].SubItems[24].Text + "'", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd5);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    lblbagqty.Text = "[" + dt.Rows[0]["Unit"].ToString() + "]";
                    lblaltqty.Text = "[" + dt.Rows[0]["Altunit"].ToString() + "]";

                    SqlCommand cmd6 = new SqlCommand("select * from TaxSlabMaster i inner join productmaster p on i.Taxslabname=p.taxslab where p.isactive=1 and i.isactive=1 and p.ProductID='" + LVFO.Items[LVFO.FocusedItem.Index].SubItems[24].Text + "' and i.saletypename= '" + cmbsaletype.Text + "'", con);
                    SqlDataAdapter sda6 = new SqlDataAdapter(cmd6);
                    DataTable dt1 = new DataTable();
                    sda6.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {

                        lbltax1.Text = "[" + Math.Round(Convert.ToDouble(dt1.Rows[0]["igst"].ToString()), 2).ToString() + "]";
                        taxforprice = lbltax1.Text;
                        lbladdtax1.Text = "[" + Math.Round(Convert.ToDouble(dt1.Rows[0]["additax"].ToString()), 2).ToString() + "]";
                        ataxforprice = lbladdtax1.Text;
                        txtitemname.Focus();
                        taxid = Math.Round(Convert.ToDouble(dt1.Rows[0]["igst"].ToString()), 2);
                        addtaxid = Math.Round(Convert.ToDouble(dt1.Rows[0]["additax"].ToString()), 2);

                    }
                    else
                    {

                        //  MessageBox.Show("Not any Tax Available For This Sale Type");

                        lbltax1.Text = "[0]";
                        taxforprice = lbltax1.Text;
                        lbladdtax1.Text = "[0]";
                        ataxforprice = lbladdtax1.Text;
                        taxid = 0;
                        addtaxid = 0;
                        txtitemname.Focus();
                    }

                    Double total = 0;
                    try
                    {
                        total = Math.Round((Convert.ToDouble(TxtBillTotal.Text) - Convert.ToDouble(LVFO.Items[LVFO.FocusedItem.Index].SubItems[5].Text)), 2);
                    }
                    catch
                    {
                    }
                    TxtBillTotal.Text = total.ToString();
                    //      LVFO.Items[LVFO.FocusedItem.Index].Remove();
                    totalcalculation();
                }
            }
        }

        private void txtbags_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (string.IsNullOrEmpty(txtbags.Text))
                    {
                        txtbags.Text = "1";
                    }
                    txtpqty.Focus();
                    qtyflag = 0;
                    try
                    {
                        if (!txtpqty.Focused)
                        {
                            //if (qtyflag == 0)
                            //{
                            //    qtyflag = 1;
                            if (txtbags.Text != "")
                            {
                                if (txtqty.Text != "")
                                {
                                    Double qty = Convert.ToDouble(Convert.ToDouble(txtbags.Text) * Convert.ToDouble(txtqty.Text));
                                    txtpqty.Text = qty.ToString();
                                }
                                else
                                {
                                    Double qty = Convert.ToDouble(Convert.ToDouble(txtbags.Text) * 1);
                                    txtpqty.Text = qty.ToString();
                                }
                            }
                            else
                            {
                                if (txtqty.Text != "")
                                {
                                    Double qty = Convert.ToDouble(1 * Convert.ToDouble(txtqty.Text));
                                    txtpqty.Text = qty.ToString();
                                }
                                else
                                {
                                    txtpqty.Text = "0.00";
                                }

                            }
                            if (txtpqty.Text != "" && txtpqty.Text != "0.00")
                            {
                                itemcalculation(txtpqty.Text);

                            }
                            else
                            {
                                itemcalculation("0");
                            }
                        }
                    }
                    catch
                    {
                    }
                }

            }
        }

        private void txtpqty_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (string.IsNullOrEmpty(txtpqty.Text))
                    {
                        txtpqty.Text = "1";
                    }
                    txtfree.Focus();
                    try
                    {
                        if (!txtbags.Focused)
                        {
                            //if (qtyflag == 0)
                            //{
                            //    qtyflag = 1;
                            if (txtpqty.Text != "")
                            {
                                if (txtqty.Text != "")
                                {
                                    Double qty = Convert.ToDouble(Convert.ToDouble(txtpqty.Text) / Convert.ToDouble(txtqty.Text));

                                    txtbags.Text = qty.ToString();

                                }
                                else
                                {
                                    Double qty = Convert.ToDouble(Convert.ToDouble(txtpqty.Text) / 1);
                                    txtbags.Text = qty.ToString();

                                }
                            }
                            else
                            {
                                if (txtqty.Text != "")
                                {
                                    Double qty = Convert.ToDouble(1 * Convert.ToDouble(txtqty.Text));
                                    txtbags.Text = qty.ToString();

                                }
                                else
                                {
                                    txtpqty.Text = "0.00";
                                }

                            }
                            if (txtpqty.Text != "" && txtpqty.Text != "0.00")
                            {
                                itemcalculation(txtpqty.Text);

                            }
                            else
                            {
                                itemcalculation("0");
                            }
                            //  qtyflag = 0;
                        }
                    }
                    catch
                    {
                        // qtyflag = 0;
                    }
                    if (options.Rows[0]["itemspeed"].ToString() == "Qty +Free Only")
                    {
                        txtfree.Focus();
                    }
                    else if (options.Rows[0]["itemspeed"].ToString() == "Qty + Rate +Discount(%) Only")
                    {
                        txtrate.Focus();
                    }
                    else if (options.Rows[0]["itemspeed"].ToString() == "Qty + Free + Rate + Discount(%) Only")
                    {
                        txtfree.Focus();
                    }
                    else if (options.Rows[0]["itemspeed"].ToString() == "Qty + Free + Rate Only")
                    {
                        txtfree.Focus();
                    }
                    else if (options.Rows[0]["itemspeed"].ToString() == "Qty +Rate Only")
                    {
                        txtrate.Focus();
                    }
                    else if (options.Rows[0]["itemspeed"].ToString() == "Qty Only")
                    {
                        enteramount();
                    }
                }
            }
        }

        private void txtrate_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (string.IsNullOrEmpty(txtrate.Text))
                    {
                        txtrate.Text = "0";
                    }
                    txttotal.Focus();
                    if (options.Rows[0]["itemspeed"].ToString() == "Qty +Free Only")
                    {
                        //   enteramount();
                    }
                    else if (options.Rows[0]["itemspeed"].ToString() == "Qty + Rate +Discount(%) Only")
                    {
                        txtdisper.Focus();
                    }
                    else if (options.Rows[0]["itemspeed"].ToString() == "Qty + Free + Rate + Discount(%) Only ")
                    {
                        txtdisper.Focus();
                    }
                    else if (options.Rows[0]["itemspeed"].ToString() == "Qty + Free + Rate Only")
                    {
                        enteramount();
                    }
                    else if (options.Rows[0]["itemspeed"].ToString() == "Qty +Rate Only")
                    {
                        enteramount();
                    }
                }
                if (e.KeyCode == Keys.F3)
                {
                    Price p = new Price(txtrate, txtamount, strfinalarray);
                    p.Show();
                }
            }
        }
        int x, y;

        private void txtrate_TextChanged(object sender, EventArgs e)
        {

            {
                try
                {
                    if (Convert.ToDouble(txtrate.Text) > 0)
                    {
                        if (x == 0)
                        {

                            itemcalculation(txtpqty.Text);
                        }
                    }
                }
                catch
                {
                }
            }
        }
        public static string s;
        private void cmbterms_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true; // This will eliminate the beeping
                    bool inList = false;
                    for (int i = 0; i < cmbterms.Items.Count; i++)
                    {
                        s = cmbterms.GetItemText(cmbterms.Items[i]);
                        if (s == cmbterms.Text)
                        {
                            inList = true;
                            cmbterms.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        cmbterms.Text = "";
                    }
                    caseterms();
                    txtduedate.Focus();
                }
            }
        }
        public void getduedate()
        {
            try
            {
                DataTable due = conn.getdataset("select * from ClientMaster where isactive=1 and AccountName='" + cmbcustname.Text + "'");
                if (due.Rows.Count > 0)
                {
                    if (strfinalarray[0] == "SO")
                    {
                        DateTime billdate = TxtRundate.Value;
                        string creditdays = due.Rows[0]["credaysale"].ToString();
                        if (string.IsNullOrEmpty(creditdays))
                        {
                            creditdays = "0";
                        }
                        DateTime duedate = billdate.AddDays(Convert.ToDouble(creditdays));
                        txtduedate.Value = duedate;
                    }
                    else if (strfinalarray[0] == "SC")
                    {
                        DateTime billdate = TxtRundate.Value;
                        string creditdays = due.Rows[0]["credaysale"].ToString();
                        if (string.IsNullOrEmpty(creditdays))
                        {
                            creditdays = "0";
                        }
                        DateTime duedate = billdate.AddDays(Convert.ToDouble(creditdays));
                        txtduedate.Value = duedate;
                    }
                    else if (strfinalarray[0] == "PO")
                    {
                        DateTime billdate = TxtRundate.Value;
                        string creditdays = due.Rows[0]["credaypurchase"].ToString();
                        if (string.IsNullOrEmpty(creditdays))
                        {
                            creditdays = "0";
                        }
                        DateTime duedate = billdate.AddDays(Convert.ToDouble(creditdays));
                        txtduedate.Value = duedate;
                    }
                    else if (strfinalarray[0] == "PC")
                    {
                        DateTime billdate = TxtRundate.Value;
                        string creditdays = due.Rows[0]["credaypurchase"].ToString();
                        if (string.IsNullOrEmpty(creditdays))
                        {
                            creditdays = "0";
                        }
                        DateTime duedate = billdate.AddDays(Convert.ToDouble(creditdays));
                        txtduedate.Value = duedate;
                    }
                }
            }
            catch
            {
            }
        }
        private void cmbcustname_KeyDown(object sender, KeyEventArgs e)
        {

            {

                if (e.KeyCode == Keys.Enter)
                {

                    e.SuppressKeyPress = true; // This will eliminate the beeping
                    bool inList = false;
                    for (int i = 0; i < cmbcustname.Items.Count; i++)
                    {
                        s = cmbcustname.GetItemText(cmbcustname.Items[i]);
                        if (s == cmbcustname.Text)
                        {
                            inList = true;
                            cmbcustname.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        cmbcustname.Text = "";
                    }

                    getduedate();
                    if (cmbcustname.Text != "")
                    {
                        if (strfinalarray[0] == "SC" || strfinalarray[0] == "PC")
                        {
                            DataTable dtcn = new DataTable();
                            string biltype;
                            if (strfinalarray[0] == "SC")
                            {
                                biltype = "SO";
                            }
                            else
                            {
                                biltype = "PO";
                            }

                            dtcn = conn.getdataset("Select distinct ClientId from SaleOrderMaster where isactive=1 and OrderStatus='Pending' and ClientId='" + cmbcustname.SelectedValue + "' and BillType like '%" + biltype + "%'");
                            if (dtcn != null && dtcn.Rows.Count > 0)
                            {

                                SaleOrder frm = new SaleOrder(dtcn.Rows[0][0].ToString(), this, strfinalarray);

                                frm.ShowDialog();

                                //string userEnteredText = frm.EnteredText;
                                //frm.Dispose();

                            }
                        }
                        cmbsaletype.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Please Select Customer");
                        cmbcustname.Focus();
                    }
                }
                if (e.KeyCode == Keys.F3)
                {
                    var privouscontroal = cmbcustname;
                    activecontroal = privouscontroal.Name;
                    Accountentry client = new Accountentry(this, master, tabControl, activecontroal);

                    client.Passed(1);
                    //   client.Show();
                    master.AddNewTab(client);
                }
                if (e.KeyCode == Keys.F2)
                {
                    var privouscontroal = cmbcustname;
                    activecontroal = privouscontroal.Name;
                    string iid = cmbcustname.SelectedValue.ToString();

                    Accountentry client = new Accountentry(this, master, tabControl, activecontroal);
                    client.Update(1, iid);
                    client.Passed(1);
                    //  client.Show();
                    master.AddNewTab(client);
                }

            }
        }

        private void cmbsaletype_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true; // This will eliminate the beeping
                    bool inList = false;
                    for (int i = 0; i < cmbsaletype.Items.Count; i++)
                    {
                        s = cmbsaletype.GetItemText(cmbsaletype.Items[i]);
                        if (s == cmbsaletype.Text)
                        {
                            inList = true;
                            cmbsaletype.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        cmbsaletype.Text = "";
                    }

                    if (cmbsaletype.Text != "")
                    {
                        if (BtnPayment.Text == "&Save")
                        {
                            if (strfinalarray[0] == "SO")
                            {
                                if (options.Rows[0]["saleovoucherno"].ToString() == "Continuous")
                                {
                                    String str = conn.ExecuteScalar("select max(Bill_No) from SaleOrderMaster where isactive='1' and BillType='SO' and id=(select max(id) from SaleOrderMaster where isactive=1 and BillType='SO')");
                                    var charsToRemove = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "-" };
                                    foreach (var c in charsToRemove)
                                    {
                                        str = str.Replace(c, string.Empty);
                                    }
                                    DataTable dt = conn.getdataset("select * from [PurchasetypeMaster] where isactive=1 and FormType='" + strfinalarray[0] + "' and Purchasetypeid='" + cmbsaletype.SelectedValue + "'");
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
                                    lblbill_no.Text = count.ToString();
                                    TxtBillNo.Text = dt.Rows[0]["prefix"].ToString() + count.ToString();
                                }
                                else if (options.Rows[0]["saleovoucherno"].ToString() == "Type_Wise")
                                {
                                    getsr();
                                }
                                else
                                {
                                    TxtBillNo.Text = "";
                                    TxtBillNo.ReadOnly = false;
                                }
                            }
                            else if (strfinalarray[0] == "SC")
                            {
                                if (options.Rows[0]["salecvoucherno"].ToString() == "Continuous")
                                {
                                    String str = conn.ExecuteScalar("select max(Bill_No) from SaleOrderMaster where isactive='1' and BillType='SC' and id=(select max(id) from SaleOrderMaster where isactive=1 and BillType='SC')");
                                    var charsToRemove = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "-" };
                                    foreach (var c in charsToRemove)
                                    {
                                        str = str.Replace(c, string.Empty);
                                    }
                                    DataTable dt = conn.getdataset("select * from [PurchasetypeMaster] where isactive=1 and FormType='" + strfinalarray[0] + "' and Purchasetypeid='" + cmbsaletype.SelectedValue + "'");
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
                                    lblbill_no.Text = count.ToString();
                                    TxtBillNo.Text = dt.Rows[0]["prefix"].ToString() + count.ToString();
                                }
                                else if (options.Rows[0]["salecvoucherno"].ToString().Trim() == "Type_Wise")
                                {
                                    getsr();
                                }
                                else
                                {
                                    TxtBillNo.Text = "";
                                    TxtBillNo.ReadOnly = false;
                                }
                            }
                            else if (strfinalarray[0] == "PO")
                            {
                                if (options.Rows[0]["purchaseovoucherno"].ToString() == "Continuous")
                                {
                                    String str = conn.ExecuteScalar("select max(Bill_No) from SaleOrderMaster where isactive='1' and BillType='PO' and id=(select max(id) from SaleOrderMaster where isactive=1 and BillType='PO')");
                                    var charsToRemove = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "-" };
                                    foreach (var c in charsToRemove)
                                    {
                                        str = str.Replace(c, string.Empty);
                                    }
                                    DataTable dt = conn.getdataset("select * from [PurchasetypeMaster] where isactive=1 and FormType='" + strfinalarray[0] + "' and Purchasetypeid='" + cmbsaletype.SelectedValue + "'");
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
                                    lblbill_no.Text = count.ToString();
                                    TxtBillNo.Text = dt.Rows[0]["prefix"].ToString() + count.ToString();
                                }
                                else if (options.Rows[0]["purchaseovoucherno"].ToString() == "Type_Wise")
                                {
                                    getsr();
                                }
                                else
                                {
                                    TxtBillNo.Text = "";
                                    TxtBillNo.ReadOnly = false;
                                }
                            }
                            else if (strfinalarray[0] == "PC")
                            {
                                if (options.Rows[0]["purchasecvoucherno"].ToString() == "Continuous")
                                {
                                    String str = conn.ExecuteScalar("select max(Bill_No) from SaleOrderMaster where isactive='1' and BillType='PC' and id=(select max(id) from SaleOrderMaster where isactive=1 and BillType='PC')");
                                    var charsToRemove = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "-" };
                                    foreach (var c in charsToRemove)
                                    {
                                        str = str.Replace(c, string.Empty);
                                    }
                                    DataTable dt = conn.getdataset("select * from [PurchasetypeMaster] where isactive=1 and FormType='" + strfinalarray[0] + "' and Purchasetypeid='" + cmbsaletype.SelectedValue + "'");
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
                                    lblbill_no.Text = count.ToString();
                                    TxtBillNo.Text = dt.Rows[0]["prefix"].ToString() + count.ToString();
                                }
                                else if (options.Rows[0]["purchasecvoucherno"].ToString() == "Type_Wise")
                                {
                                    getsr();
                                }
                                else
                                {
                                    TxtBillNo.Text = "";
                                    TxtBillNo.ReadOnly = false;
                                }
                            }
                        }
                        else
                        {
                            if (options.Rows[0]["purchasecvoucherno"].ToString() == "Manual")
                            {
                                TxtBillNo.ReadOnly = false;
                            }
                            else if (options.Rows[0]["purchaseovoucherno"].ToString() == "Manual")
                            {
                                TxtBillNo.ReadOnly = false;
                            }
                            else if (options.Rows[0]["salecvoucherno"].ToString() == "Manual")
                            {
                                TxtBillNo.ReadOnly = false;
                            }
                            else if (options.Rows[0]["saleovoucherno"].ToString() == "Manual")
                            {
                                TxtBillNo.ReadOnly = false;
                            }
                        }
                      //  txtpono.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Please Select Sale type");
                        cmbsaletype.Focus();
                    }
                    DataTable reqgst = conn.getdataset("select * from [PurchasetypeMaster] where isactive=1 and FormType='" + strfinalarray[0] + "' and Purchasetypeid='" + cmbsaletype.SelectedValue + "'");
                    DataTable gstno = conn.getdataset("select * from clientmaster where isactive=1 and ClientID='" + cmbcustname.SelectedValue + "'");
                    if (reqgst.Rows[0]["TaxCalculation"].ToString() == "Tax Invoice")
                    {
                        if (gstno.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(gstno.Rows[0]["statecode"].ToString()))
                            {
                                string companystatecode = conn.ExecuteScalar("select Statecode from Company where isactive=1 and CompanyID='" + Master.companyId + "'");
                                if (!string.IsNullOrEmpty(companystatecode))
                                {
                                    if (reqgst.Rows[0]["Region"].ToString() == "Local")
                                    {
                                        if (gstno.Rows[0]["statecode"].ToString().Trim() == companystatecode.Trim())
                                        {
                                        }
                                        else
                                        {
                                            MessageBox.Show("This Party is Out of State So Please Select Inter state Type ");
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        if (gstno.Rows[0]["statecode"].ToString().Trim() != companystatecode.Trim())
                                        {
                                        }
                                        else
                                        {
                                            MessageBox.Show("Your Party Is Local Please Create Local GST Bill");
                                            return;
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Enter Company State Code");
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Enter Client State Code");
                                return;
                            }
                            if (gstno.Rows[0]["GstNo"].ToString() == "")
                            {
                                MessageBox.Show("You are Issuing Tax Invoice to a party whose GST NO is not Specified Kindly Check");
                                return;
                            }
                            //else
                            //{
                            //    DialogResult dr = MessageBox.Show("Do you want to Save?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            //    if (dr == DialogResult.Yes)
                            //    {
                            //        txtpono.Focus();
                            //    }
                            //}
                            txtpono.Focus();
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(gstno.Rows[0]["statecode"].ToString()))
                        {
                            string companystatecode = conn.ExecuteScalar("select Statecode from Company where isactive=1 and CompanyID='" + Master.companyId + "'");
                            if (!string.IsNullOrEmpty(companystatecode))
                            {
                                if (reqgst.Rows[0]["Region"].ToString() == "Local")
                                {
                                    if (gstno.Rows[0]["statecode"].ToString().Trim() == companystatecode.Trim())
                                    {
                                    }
                                    else
                                    {
                                        MessageBox.Show("This Party is Out of State So Please Select Inter state Type ");
                                        return;
                                    }
                                }
                                else
                                {
                                    if (gstno.Rows[0]["statecode"].ToString().Trim() != companystatecode.Trim())
                                    {
                                    }
                                    else
                                    {
                                        MessageBox.Show("Your Party Is Local Please Create Local GST Bill");
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Enter Company State Code");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Enter Client State Code");
                            return;
                        }
                        if (gstno.Rows[0]["GstNo"].ToString() != "")
                        {
                            DialogResult dr = MessageBox.Show("You are Issuing Retail Invoice to Taxable person whose GST No.is specified Are you sure to continue?", "GST No.is specified", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dr == DialogResult.Yes)
                            {
                                txtpono.Focus();
                            }
                        }
                        else
                        {
                            txtpono.Focus();
                        }


                    }
                    saletype = cmbsaletype.Text;
                }
                if (e.KeyCode == Keys.F3)
                {
                    if (strfinalarray[0] == "SO")
                    {
                        var privouscontroal = cmbsaletype;
                        activecontroal = privouscontroal.Name;
                        SaletypeEntry pt = new SaletypeEntry(this, master, tabControl, activecontroal);
                        //pt.Show();
                        master.AddNewTab(pt);
                    }
                    else if (strfinalarray[0] == "PO")
                    {
                        var privouscontroal = cmbsaletype;
                        activecontroal = privouscontroal.Name;
                        PurchaseTypeEntry p = new PurchaseTypeEntry(this, master, tabControl, activecontroal);
                        master.AddNewTab(p);
                    }
                    else if (strfinalarray[0] == "SC")
                    {
                        var privouscontroal = cmbsaletype;
                        activecontroal = privouscontroal.Name;
                        SaletypeEntry pt = new SaletypeEntry(this, master, tabControl, activecontroal);
                        //pt.Show();
                        master.AddNewTab(pt);
                    }
                    else if (strfinalarray[0] == "PC")
                    {
                        var privouscontroal = cmbsaletype;
                        activecontroal = privouscontroal.Name;
                        PurchaseTypeEntry p = new PurchaseTypeEntry(this, master, tabControl, activecontroal);
                        master.AddNewTab(p);
                    }
                }
                if (e.KeyCode == Keys.F2)
                {
                    if (strfinalarray[0] == "SO")
                    {
                        var privouscontroal = cmbsaletype;
                        activecontroal = privouscontroal.Name;
                        SaletypeEntry pt = new SaletypeEntry(this, master, tabControl, activecontroal);
                        pt.updatemode("Sale", cmbsaletype.Text, "SO");
                        // pt.Show();
                        master.AddNewTab(pt);
                    }
                    else if (strfinalarray[0] == "SC")
                    {
                        var privouscontroal = cmbsaletype;
                        activecontroal = privouscontroal.Name;
                        SaletypeEntry pt = new SaletypeEntry(this, master, tabControl, activecontroal);
                        pt.updatemode("Sale", cmbsaletype.Text, "SC");
                        // pt.Show();
                        master.AddNewTab(pt);
                    }
                    else if (strfinalarray[0] == "PO")
                    {
                        var privouscontroal = cmbsaletype;
                        activecontroal = privouscontroal.Name;
                        PurchaseTypeEntry p = new PurchaseTypeEntry(this, master, tabControl, activecontroal);
                        p.updatemode("Purchase", cmbsaletype.Text, "PO");
                        // pt.Show();
                        master.AddNewTab(p);
                    }
                    else if (strfinalarray[0] == "PC")
                    {
                        var privouscontroal = cmbsaletype;
                        activecontroal = privouscontroal.Name;
                        PurchaseTypeEntry p = new PurchaseTypeEntry(this, master, tabControl, activecontroal);
                        p.updatemode("Purchase", cmbsaletype.Text, "PC");
                        // pt.Show();
                        master.AddNewTab(p);
                    }

                }

            }
        }
        public void binaagent()
        {
            string qry = "";
            if (strfinalarray[0] == "SO" || strfinalarray[0] == "SC")
            {
                qry = "select ClientID,AccountName from ClientMaster where isactive=1 and groupID=50 order by AccountName";
            }
            SqlCommand cmd1 = new SqlCommand(qry, con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            cmbagentname.ValueMember = "ClientID";
            cmbagentname.DisplayMember = "AccountName";
            cmbagentname.DataSource = dt1;
            cmbagentname.SelectedIndex = -1;
        }
        private void txtpono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (strfinalarray[0] == "SO" || strfinalarray[0] == "SC")
                {
                    if (Convert.ToBoolean(options.Rows[0]["showagentnameinsale"].ToString()) == true)
                    {
                        binaagent();
                        pnlagent.Visible = true;
                        cmbagentname.Focus();

                    }
                    else
                    {
                        this.ActiveControl = TxtBillNo;
                    }
                }
                else
                {
                    this.ActiveControl = TxtBillNo;
                }
            }
        }

        private void DefaultSale_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }

                if (e.Alt == true && e.KeyCode == Keys.S)
                {
                    BtnPayment.PerformClick();
                }
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                if (pnlcusdetails.Visible == true)
                {
                    pnlcusdetails.Visible = false;
                    txtitemname.Focus();
                    return true;
                }
                if (pnlserial.Visible == true)
                {
                    pnlserial.Visible = false;
                    txtrate.Focus();
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        master.RemoveCurrentTab();
                    }
                }
                return true;
            }
            if (keyData == (Keys.Control | Keys.D1))
            {
                TAB1.SelectedIndex = 0;
            }
            if (keyData == (Keys.Control | Keys.D2))
            {
                TAB1.SelectedIndex = 1;
            }
            if (keyData == (Keys.Alt | Keys.U))
            {
                //DialogResult dr = MessageBox.Show("Do you want to Update?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (dr == DialogResult.Yes)
                //{
                btnsubmit();
                //}
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        int cnt = 0;
        private Ledger ledger;
        string refno;
        public void caseterms()
        {
            try
            {
                if (cmbterms.Text == "Cash")
                {
                    string qry = "";
                    if (Convert.ToBoolean(options.Rows[0]["showcustomersupplierseperate"].ToString()) == true)
                    {
                        if (strfinalarray[0] == "SO")
                        {
                            DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='99'");
                            string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                            qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=99 or groupID=11 or GroupID='" + groupid + "') order by AccountName";
                        }
                        else if (strfinalarray[0] == "SC")
                        {
                            DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='99'");
                            string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                            qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=99 or groupID=11 or GroupID='" + groupid + "') order by AccountName";
                        }
                        else if (strfinalarray[0] == "PO")
                        {
                            DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='100'");
                            string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                            qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=100 or groupID=11 or GroupID='" + groupid + "') order by AccountName";
                            //   qry = "select ClientID,AccountName from ClientMaster where isactive=1 and groupID=100 order by AccountName";
                        }
                        else if (strfinalarray[0] == "PC")
                        {
                            DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='100'");
                            string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                            qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=100 or groupID=11 or GroupID='" + groupid + "') order by AccountName";
                            //qry = "select ClientID,AccountName from ClientMaster where isactive=1 and groupID=100 order by AccountName";
                        }
                    }
                    else
                    {
                        DataTable Accountgroup = conn.getdataset("select * from AccountGroup where (id='99' or id='100')");
                        string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                        string groupid1 = Accountgroup.Rows[1]["UnderGroupID"].ToString();
                        qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=99 or groupID=11 or GroupID=100 or GroupID='" + groupid + "' or GroupID='" + groupid1 + "') order by AccountName";
                        //  qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=100 or groupid=99) order by AccountName";
                    }


                    SqlCommand cmd1 = new SqlCommand(qry, con);
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    sda1.Fill(dt1);
                    cmbcustname.ValueMember = "ClientID";
                    cmbcustname.DisplayMember = "AccountName";
                    cmbcustname.DataSource = dt1;
                    cmbcustname.SelectedIndex = -1;
                }
            }
            catch
            {
            }
        }
        DataTable billpro, billcharges, billorder, billocharges = new DataTable();
        public static string clientidupdate;
        public void getdata(string[] str, string[] p, int p_2, string[] strfinalarray, string[] str1)
        {
            try
            {
                cnt = 1;
                //set the interval  and start the timer
                // timer1.Interval = 1000;
                //  timer1.Start();
                int count = str1.Length;
                if (count == 1)
                {
                    txtpono.Text = str1[0];
                }
                updateitem = str;
                #region
                //if (con.State == ConnectionState.Open)
                //{
                //    con.Close();
                //}
                //con.Open();
                //charges = 1;
                //this.StartPosition = FormStartPosition.Manual;
                //this.Location = new Point(0, 0);
                //TxtRundate.Text = DateTime.Now.ToString(Master.dateformate);
                //this.ActiveControl = TxtRundate;
                //LVFO.Columns.Add("Description of Goods", 196, HorizontalAlignment.Left);
                //LVFO.Columns.Add("Packing", 69, HorizontalAlignment.Center);
                //LVFO.Columns.Add("Batch", 51, HorizontalAlignment.Center);
                //LVFO.Columns.Add("P.Qty", 53, HorizontalAlignment.Center);
                //LVFO.Columns.Add("A.Qty", 47, HorizontalAlignment.Center);
                //LVFO.Columns.Add("Total Qty", 53, HorizontalAlignment.Right);
                //LVFO.Columns.Add("Free", 53, HorizontalAlignment.Center);
                //LVFO.Columns.Add(lblrate.Text, 64, HorizontalAlignment.Right);
                //LVFO.Columns.Add("Per", 42, HorizontalAlignment.Center);
                //LVFO.Columns.Add(lblamount.Text, 84, HorizontalAlignment.Center);
                //LVFO.Columns.Add("Dis(%)", 44, HorizontalAlignment.Center);
                //LVFO.Columns.Add("Dis Per", 63, HorizontalAlignment.Center);
                //LVFO.Columns.Add("TAX", 63, HorizontalAlignment.Center);
                //LVFO.Columns.Add("Add Tax", 59, HorizontalAlignment.Center);
                //LVFO.Columns.Add("Total", 108, HorizontalAlignment.Center);
                //LVFO.Columns.Add("Sgstper", 0, HorizontalAlignment.Center);
                //LVFO.Columns.Add("Sgstamt", 0, HorizontalAlignment.Center);
                //LVFO.Columns.Add("cgstper", 0, HorizontalAlignment.Center);
                //LVFO.Columns.Add("cgstamt", 0, HorizontalAlignment.Center);
                //LVFO.Columns.Add("igstper", 0, HorizontalAlignment.Center);
                //LVFO.Columns.Add("igstamt", 0, HorizontalAlignment.Center);
                //LVFO.Columns.Add("addtaxper", 0, HorizontalAlignment.Center);
                //LVFO.Columns.Add("serial", 100, HorizontalAlignment.Center);


                //LVCHARGES.Columns.Add("Perticulars", 237, HorizontalAlignment.Left);
                //LVCHARGES.Columns.Add("Remarks", 215, HorizontalAlignment.Left);
                //LVCHARGES.Columns.Add("Value", 167, HorizontalAlignment.Left);
                //LVCHARGES.Columns.Add("@", 122, HorizontalAlignment.Left);
                //LVCHARGES.Columns.Add("+/-", 91, HorizontalAlignment.Left);
                //LVCHARGES.Columns.Add("Amount", 117, HorizontalAlignment.Right);
                //LVCHARGES.Columns.Add("valueofexp", 0, HorizontalAlignment.Right);
                //LVCHARGES.Columns.Add("tax", 0, HorizontalAlignment.Right);
                //LVCHARGES.Columns.Add("sgst", 0, HorizontalAlignment.Right);
                //LVCHARGES.Columns.Add("cgst", 0, HorizontalAlignment.Right);
                //LVCHARGES.Columns.Add("igst", 0, HorizontalAlignment.Right);
                //LVCHARGES.Columns.Add("additax", 0, HorizontalAlignment.Right);
                //lvserial.Columns.Add("", 1000, HorizontalAlignment.Right);
                //temptable = new DataTable();
                //temptable.Columns.Add("Itemname", typeof(string));
                //temptable.Columns.Add("SERIAL", typeof(string));


                //con.Close();
                #endregion
                for (int u = 0; u < str.Count(); u++)
                {
                    refno += p[u] + ",";
                    SqlCommand cmd = new SqlCommand("select * from SaleOrderMaster where billno='" + p[u] + "' and isactive=1 and billtype='" + strfinalarray[u] + "'", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    //SqlCommand cmd1 = new SqlCommand("select * from SaleOrderProductMaster where billno='" + p[u] + "' and billtype='" + strfinalarray[u] + "' and isactive=1", con);
                    //SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    //DataTable dt1 = new DataTable();
                    //sda1.Fill(dt1);

                    //DataTable dt2 = conn.getdataset("select * from SaleOrderchargesmaster where billno='" + p[u] + "' and billtype='" + strfinalarray[u] + "' and isactive=1");
                    string clientid = conn.ExecuteScalar("select ClientID from SaleOrderProductMaster where billno='" + p[u] + "' and billtype='" + strfinalarray[0] + "' and isactive=1");
                    if (string.IsNullOrEmpty(clientid))
                    {
                        SqlCommand cmd1 = new SqlCommand("select * from SaleOrderProductMaster where billno='" + p[u] + "' and billtype='" + strfinalarray[u] + "' and isactive=1", con);
                        SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                        billorder = new DataTable();
                        sda1.Fill(billorder);
                    }
                    else
                    {
                        SqlCommand cmd1 = new SqlCommand("select * from SaleOrderProductMaster where ClientID='" + p_2 + "' and  billno='" + p[u] + "' and billtype='" + strfinalarray[u] + "' and isactive=1", con);
                        SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                        billorder = new DataTable();
                        sda1.Fill(billorder);
                    }
                    dt1 = billorder;
                    string charges = conn.ExecuteScalar("select ClientID from SaleOrderchargesmaster where billno='" + p[u] + "' and billtype='" + strfinalarray[0] + "' and isactive=1");
                    if (string.IsNullOrEmpty(charges))
                    {
                        billocharges = conn.getdataset("select * from SaleOrderchargesmaster where billno='" + p[u] + "' and billtype='" + strfinalarray[u] + "' and isactive=1");
                    }
                    else
                    {
                        billocharges = conn.getdataset("select * from SaleOrderchargesmaster where ClientID='" + p_2 + "' and billno='" + p[u] + "' and billtype='" + strfinalarray[u] + "' and isactive=1");
                    }
                    dt2 = billocharges;
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        ListViewItem li;
                        string productname = conn.ExecuteScalar("select product_name from productmaster where isactive=1 and productid='" + dt1.Rows[i]["productid"].ToString() + "'");
                        //  li.SubItems.Add(productname);
                        li = LVFO.Items.Add(dt1.Rows[i]["Productname"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["Packing"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["batch"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["Bags"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["Aqty"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["Pqty"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["free"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["Rate"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["Per"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["Total"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["discountper"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["discountamt"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["Tax"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["addtax"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["Amount"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["sgstper"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["sgstamt"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["cgstper"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["cgstamt"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["igstper"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["igdtamt"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["addtaxper"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["serialno"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["cess"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["productid"].ToString());
                    }
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        ListViewItem li;
                        li = LVCHARGES.Items.Add(dt2.Rows[i]["perticulars"].ToString());
                        li.SubItems.Add(dt2.Rows[i]["remarks"].ToString());
                        li.SubItems.Add(dt2.Rows[i]["value"].ToString());
                        li.SubItems.Add(dt2.Rows[i]["at"].ToString());
                        li.SubItems.Add(dt2.Rows[i]["plusminus"].ToString());
                        li.SubItems.Add(dt2.Rows[i]["amount"].ToString());
                        li.SubItems.Add(dt2.Rows[i]["valueofexp"].ToString());
                        li.SubItems.Add(dt2.Rows[i]["tax"].ToString());
                        li.SubItems.Add(dt2.Rows[i]["sgst"].ToString());
                        li.SubItems.Add(dt2.Rows[i]["cgst"].ToString());
                        li.SubItems.Add(dt2.Rows[i]["igst"].ToString());
                        li.SubItems.Add(dt2.Rows[i]["additax"].ToString());
                        li.SubItems.Add(dt2.Rows[i]["addtaxamt"].ToString());
                        li.SubItems.Add(dt2.Rows[i]["billsundryid"].ToString());
                    }
                    if (Convert.ToBoolean(options.Rows[0]["itemsinasedingorderinsale"].ToString()) == true)
                    {
                        this.LVFO.ListViewItemSorter = new ListViewItemComparer(0);
                    }

                }
                refno = refno.TrimEnd(',');
                calculatetotalcharges();
                totalcalculation();
                clearitem();
                bindperticular();
                cmbsaletype.Focus();
                con.Close();
            }
            catch
            {
            }

        }
        internal void updatemode(string str, string p, int p_2, string[] strfinalarray)
        {
            try
            {
                userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                if (strfinalarray[0] == "SO")
                {
                    txtheader.Text = "SALE ORDER";
                    txttype.Text = "Sale Type:";
                    this.Text = "Sale Order";
                    if (Convert.ToBoolean(options.Rows[0]["showagentnameinsale"].ToString()) == true)
                    {
                        binaagent();
                        pnlagent.Visible = true;
                    }
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[1]["a"].ToString() == "False")
                        {
                            BtnPayment.Enabled = false;
                        }
                        if (userrights.Rows[1]["d"].ToString() == "False")
                        {
                            btndelete.Enabled = false;
                        }
                    }
                }
                else if (strfinalarray[0] == "SC")
                {
                    txtheader.Text = "SALE CHALLAN";
                    txttype.Text = "Sale Type:";
                    this.Text = "Sale Challan";
                    if (Convert.ToBoolean(options.Rows[0]["showagentnameinsale"].ToString()) == true)
                    {
                        binaagent();
                        pnlagent.Visible = true;
                    }
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[12]["a"].ToString() == "False")
                        {
                            BtnPayment.Enabled = false;
                        }
                        if (userrights.Rows[12]["d"].ToString() == "False")
                        {
                            btndelete.Enabled = false;
                        }
                    }
                }
                else if (strfinalarray[0] == "PO")
                {
                    txtheader.Text = "PURCHASE ORDER";
                    txttype.Text = "Purchase Type:";
                    this.Text = "Purchase Order";
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[4]["a"].ToString() == "False")
                        {
                            BtnPayment.Enabled = false;
                        }
                        if (userrights.Rows[4]["d"].ToString() == "False")
                        {
                            btndelete.Enabled = false;
                        }
                    }
                }

                else if (strfinalarray[0] == "PC")
                {
                    txtheader.Text = "PURCHASE CHALLAN";
                    txttype.Text = "Purchase Type:";
                    this.Text = "Purchase Challan";
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[15]["a"].ToString() == "False")
                        {
                            BtnPayment.Enabled = false;
                        }
                        if (userrights.Rows[15]["d"].ToString() == "False")
                        {
                            btndelete.Enabled = false;
                        }
                    }
                }
                loadpage();
                //set the interval  and start the timer
                //timer1.Interval = 1000;
                //timer1.Start();
                cnt = 1;

                SqlCommand cmd = new SqlCommand("select * from SaleOrderMaster where billno='" + p + "' and isactive=1 and billtype='" + strfinalarray[0] + "'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                //SqlCommand cmd1 = new SqlCommand("select * from SaleOrderProductMaster where billno='" + p + "' and billtype='" + strfinalarray[0] + "' and isactive=1", con);
                //SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                //DataTable dt1 = new DataTable();
                //sda1.Fill(dt1);

                //DataTable dt2 = conn.getdataset("select * from SaleOrderchargesmaster where billno='" + p + "' and billtype='" + strfinalarray[0] + "' and isactive=1");

                string clientid = conn.ExecuteScalar("select ClientID from SaleOrderProductMaster where billno='" + p + "' and billtype='" + strfinalarray[0] + "' and isactive=1");
                if (string.IsNullOrEmpty(clientid))
                {
                    SqlCommand cmd1 = new SqlCommand("select * from SaleOrderProductMaster where billno='" + p + "' and billtype='" + strfinalarray[0] + "' and isactive=1", con);
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    billpro = new DataTable();
                    sda1.Fill(billpro);
                }
                else
                {
                    SqlCommand cmd1 = new SqlCommand("select * from SaleOrderProductMaster where ClientID='" + p_2 + "' and billno='" + p + "' and billtype='" + strfinalarray[0] + "' and isactive=1", con);
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    billpro = new DataTable();
                    sda1.Fill(billpro);
                }
                dt1 = billpro;
                string charges = conn.ExecuteScalar("select ClientID from SaleOrderchargesmaster where billno='" + p + "' and billtype='" + strfinalarray[0] + "' and isactive=1");
                if (string.IsNullOrEmpty(charges))
                {
                    billcharges = conn.getdataset("select * from SaleOrderchargesmaster where billno='" + p + "' and billtype='" + strfinalarray[0] + "' and isactive=1");
                }
                else
                {
                    billcharges = conn.getdataset("select * from SaleOrderchargesmaster where ClientID='" + p_2 + "' and billno='" + p + "' and billtype='" + strfinalarray[0] + "' and isactive=1");
                }
                DataTable dt2 = billcharges;
                clientidupdate = dt.Rows[0]["ClientID"].ToString();
                oldbillno = dt.Rows[0]["billno"].ToString();
                lblid.Text = dt.Rows[0]["id"].ToString();
                lblbill_no.Text = dt.Rows[0]["bill_no"].ToString();
                TxtBillNo.Text = dt.Rows[0]["billno"].ToString();
                try
                {
                    TxtRundate.Text = Convert.ToDateTime(dt.Rows[0]["Bill_Date"].ToString()).ToString(Master.dateformate);
                    txtduedate.Text = Convert.ToDateTime(dt.Rows[0]["Duedate"].ToString()).ToString(Master.dateformate);
                    txtduedate.Text = Convert.ToDateTime(dt.Rows[0]["Duedate"].ToString()).ToString(Master.dateformate);
                }
                catch
                {
                }
                cmbterms.Text = dt.Rows[0]["Terms"].ToString();
                caseterms();
                txtcustomername.Text = dt.Rows[0]["cusname"].ToString();
                txtaddress.Text = dt.Rows[0]["cusadd"].ToString();
                txtcity.Text = dt.Rows[0]["cuscity"].ToString();
                txtphone.Text = dt.Rows[0]["cusphone"].ToString();
                txtmobile.Text = dt.Rows[0]["cusmobile"].ToString();
                txtpanno.Text = dt.Rows[0]["cuspancard"].ToString();
                txtadharno.Text = dt.Rows[0]["cusadhar"].ToString();
                cmd = new SqlCommand("select accountname from clientmaster where isactive=1 and clientid='" + dt.Rows[0]["ClientID"].ToString() + "' and isactive=1", con);
                con.Open();
                string clientname = cmd.ExecuteScalar().ToString();
                //  cmbcustname.SelectedIndex = cmbcustname.Items.IndexOf(clientname);
                cmbcustname.Text = clientname;
                if (strfinalarray[0] == "SO" || strfinalarray[0] == "SC")
                {
                    if (Convert.ToBoolean(options.Rows[0]["showagentnameinsale"].ToString()) == true)
                    {
                        DataTable agent = new DataTable();
                        agent = conn.getdataset("select accountname from clientmaster where isactive=1 and clientid='" + dt.Rows[0]["agentID"].ToString() + "' and isactive=1");
                        if (agent.Rows.Count > 0)
                        {
                            //  cmd = new SqlCommand("select accountname from clientmaster where isactive=1 and clientid='" + dt.Rows[0]["agentID"].ToString() + "' and isactive=1", con);
                            string agentname = agent.Rows[0]["accountname"].ToString();
                            cmbagentname.Text = agentname;
                            pnlagent.Visible = true;
                        }
                    }
                }
                txtpono.Text = dt.Rows[0]["PO_No"].ToString();

                cmd = new SqlCommand("select Purchasetypename from Purchasetypemaster where isactive=1 and FormType='" + strfinalarray[0] + "' and Purchasetypeid='" + dt.Rows[0]["SaleType"].ToString() + "'", con);
                string saletypename = cmd.ExecuteScalar().ToString();
                cmbsaletype.Text = saletypename;
                saletype = cmbsaletype.Text;
                //lbltotcount.Text = dt.Rows[0]["count"].ToString();
                //lbltotpqty.Text = dt.Rows[0]["totalqty"].ToString();
                //lblbasictot.Text = dt.Rows[0]["totalbasic"].ToString();
                //txttottax.Text = dt.Rows[0]["totaltax"].ToString();
                //TxtBillTotal.Text = dt.Rows[0]["totaltax"].ToString();

                txtweight.Text = dt.Rows[0]["apprweight"].ToString();
                txttransport.Text = dt.Rows[0]["dispatchdetails"].ToString();
                txtremarks.Text = dt.Rows[0]["remarks"].ToString();

                txtdelieveryat.Text = dt.Rows[0]["Delieveryat"].ToString();
                txtfraight.Text = dt.Rows[0]["fraight"].ToString();
                txtvehicleno.Text = dt.Rows[0]["vehicleno"].ToString();
                txtgrrrno.Text = dt.Rows[0]["grrrno"].ToString();
                txtskids.Text = dt.Rows[0]["noofskids"].ToString();
                refnoupdate = dt.Rows[0]["refno"].ToString();



                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    ListViewItem li;
                    string productname = conn.ExecuteScalar("select product_name from productmaster where isactive=1 and productid='" + dt1.Rows[i]["productid"].ToString() + "'");
                    //  li.SubItems.Add(productname);
                    li = LVFO.Items.Add(dt1.Rows[i]["Productname"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["Packing"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["batch"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["Bags"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["Aqty"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["Pqty"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["free"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["Rate"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["Per"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["Total"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["discountper"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["discountamt"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["Tax"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["addtax"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["Amount"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["sgstper"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["sgstamt"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["cgstper"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["cgstamt"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["igstper"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["igdtamt"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["addtaxper"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["serialno"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["cess"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["productid"].ToString());
                }
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    ListViewItem li;
                    li = LVCHARGES.Items.Add(dt2.Rows[i]["perticulars"].ToString());
                    li.SubItems.Add(dt2.Rows[i]["remarks"].ToString());
                    li.SubItems.Add(dt2.Rows[i]["value"].ToString());
                    li.SubItems.Add(dt2.Rows[i]["at"].ToString());
                    li.SubItems.Add(dt2.Rows[i]["plusminus"].ToString());
                    li.SubItems.Add(dt2.Rows[i]["amount"].ToString());
                    li.SubItems.Add(dt2.Rows[i]["valueofexp"].ToString());
                    li.SubItems.Add(dt2.Rows[i]["tax"].ToString());
                    li.SubItems.Add(dt2.Rows[i]["sgst"].ToString());
                    li.SubItems.Add(dt2.Rows[i]["cgst"].ToString());
                    li.SubItems.Add(dt2.Rows[i]["igst"].ToString());
                    li.SubItems.Add(dt2.Rows[i]["additax"].ToString());
                    li.SubItems.Add(dt2.Rows[i]["addtaxamt"].ToString());
                    li.SubItems.Add(dt2.Rows[i]["billsundryid"].ToString());
                }
                if (Convert.ToBoolean(options.Rows[0]["itemsinasedingorderinsale"].ToString()) == true)
                {
                    this.LVFO.ListViewItemSorter = new ListViewItemComparer(0);
                }
                calculatetotalcharges();
                totalcalculation();
                clearitem();
                txtitemname.Focus();
                bindperticular();
                BtnPayment.Text = "Update";
                con.Close();
            }
            catch
            {
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clearall();
            clearitem();
            LVFO.Items.Clear();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {

            {
                try
                {
                    DialogResult dr1 = MessageBox.Show("Do you want to Delete Bill?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr1 == DialogResult.Yes)
                    {
                        //this.Enabled = false;
                        //if (con.State == ConnectionState.Open)
                        //{
                        //    con.Close();
                        //}
                        //con.Open();
                        //SqlCommand cmd = new SqlCommand("update SaleOrderMaster set isactive=0  where billno='" + TxtBillNo.Text + "' and billtype='" + strfinalarray[0] + "'", con);
                        //cmd.ExecuteNonQuery();

                        //SqlCommand cmd2 = new SqlCommand("update SaleOrderProductMaster set isactive=0 where billno='" + TxtBillNo.Text + "' and BillType='" + strfinalarray[0] + "'", con);
                        //cmd2.ExecuteNonQuery();
                        //conn.execute("update SaleOrderchargesmaster set isactive='0' where isactive=1 and billno='" + TxtBillNo.Text + "' and billtype='" + strfinalarray[0] + "'");


                        //MessageBox.Show("Delete Successfully");
                        //master.RemoveCurrentTab();
                        this.Enabled = false;
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                        con.Open();
                        SqlCommand cmd = new SqlCommand("update SaleOrderMaster set isactive=0  where ClientID='" + clientidupdate + "' and billno='" + TxtBillNo.Text + "' and billtype='" + strfinalarray[0] + "'", con);
                        cmd.ExecuteNonQuery();

                        SqlCommand cmd2 = new SqlCommand("update SaleOrderProductMaster set isactive=0 where ClientID='" + clientidupdate + "' and billno='" + TxtBillNo.Text + "' and BillType='" + strfinalarray[0] + "'", con);
                        cmd2.ExecuteNonQuery();
                        conn.execute("update SaleOrderchargesmaster set isactive='0' where ClientID='" + clientidupdate + "' and billno='" + TxtBillNo.Text + "' and billtype='" + strfinalarray[0] + "'");
                        conn.execute("update Serials set isactive='0' where PartyID='" + clientidupdate + "' and VoucherID='" + TxtBillNo.Text + "' and TransactionID='" + strfinalarray[0] + "'");

                        MessageBox.Show("Delete Successfully");
                        master.RemoveCurrentTab();
                    }
                }
                finally
                {
                    this.Enabled = true;
                }
            }
        }

        private void txtpacking_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (string.IsNullOrEmpty(txtpacking.Text))
                    {
                        txtpacking.Text = "1";
                    }
                    txtbags.Focus();
                }
            }
        }

        private void txtpacking_TextChanged(object sender, EventArgs e)
        {

            {
                if (txtpacking.Text != "")
                {

                }
            }
        }

        private void txtqty_TextChanged(object sender, EventArgs e)
        {

            {
                try
                {
                    if (qtyflag == 0)
                    {
                        if (txtqty.Text != "")
                        {
                            if (txtbags.Text != "")
                            {
                                Double qty = Convert.ToDouble(Convert.ToDouble(txtbags.Text) * Convert.ToDouble(txtqty.Text));
                                txtpqty.Text = qty.ToString();
                            }
                            else
                            {
                                Double qty = Convert.ToDouble(1 * Convert.ToDouble(txtqty.Text));
                                txtpqty.Text = qty.ToString();
                            }
                        }
                        else
                        {
                            if (txtbags.Text != "")
                            {
                                Double qty = Convert.ToDouble(Convert.ToDouble(txtbags.Text) * 1);
                                txtpqty.Text = qty.ToString();
                            }
                            else
                            {
                                txtpqty.Text = "0.00";
                            }

                        }
                        if (txtpqty.Text != "" && txtpqty.Text != "0.00")
                        {
                            itemcalculation(txtpqty.Text);

                        }
                        else
                        {
                            itemcalculation("0");
                        }
                        qtyflag = 1;
                    }

                }
                catch
                {
                }
            }
        }

        private void txtqty_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtpqty.Focus();
                    try
                    {
                        if (qtyflag == 0)
                        {
                            if (txtqty.Text != "")
                            {
                                if (txtbags.Text != "")
                                {
                                    Double qty = Convert.ToDouble(Convert.ToDouble(txtbags.Text) * Convert.ToDouble(txtqty.Text));
                                    txtpqty.Text = qty.ToString();
                                }
                                else
                                {
                                    Double qty = Convert.ToDouble(1 * Convert.ToDouble(txtqty.Text));
                                    txtpqty.Text = qty.ToString();
                                }
                            }
                            else
                            {
                                if (txtbags.Text != "")
                                {
                                    Double qty = Convert.ToDouble(Convert.ToDouble(txtbags.Text) * 1);
                                    txtpqty.Text = qty.ToString();
                                }
                                else
                                {
                                    txtpqty.Text = "0.00";
                                }

                            }
                            if (txtpqty.Text != "" && txtpqty.Text != "0.00")
                            {
                                itemcalculation(txtpqty.Text);

                            }
                            else
                            {
                                itemcalculation("0");
                            }
                            qtyflag = 1;
                        }

                    }
                    catch
                    {
                    }
                }
            }
        }

        private void txtweight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtskids.Focus();
            }
        }
        private void txtdispatch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtremarks.Focus();
            }
        }

        private void txtremarks_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtweight.Focus();
            }
        }

        private void txtbags_TextChanged(object sender, EventArgs e)
        {

            {
                try
                {
                    if (!txtpqty.Focused)
                    {
                        //if (qtyflag == 0)
                        //{
                        //    qtyflag = 1;
                        if (txtbags.Text != "")
                        {
                            if (txtqty.Text != "")
                            {
                                Double qty = Convert.ToDouble(Convert.ToDouble(txtbags.Text) * Convert.ToDouble(txtqty.Text));
                                txtpqty.Text = qty.ToString();
                            }
                            else
                            {
                                Double qty = Convert.ToDouble(Convert.ToDouble(txtbags.Text) * 1);
                                txtpqty.Text = qty.ToString();
                            }
                        }
                        else
                        {
                            if (txtqty.Text != "")
                            {
                                Double qty = Convert.ToDouble(1 * Convert.ToDouble(txtqty.Text));
                                txtpqty.Text = qty.ToString();
                            }
                            else
                            {
                                txtpqty.Text = "0.00";
                            }

                        }
                        if (txtpqty.Text != "" && txtpqty.Text != "0.00")
                        {
                            itemcalculation(txtpqty.Text);

                        }
                        else
                        {
                            itemcalculation("0");
                        }
                    }
                    //qtyflag = 0;
                }
                catch
                {
                }
            }
        }

        private void txtbags_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtqty_KeyPress(object sender, KeyPressEventArgs e)
        {

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
        }

        private void txtpqty_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnclose_Click(object sender, EventArgs e)
        {

            {
                DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    master.RemoveCurrentTab();
                }
            }
        }

        private void TxtBillNo_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (!string.IsNullOrEmpty(TxtBillNo.Text))
                    {
                        string account = cmbcustname.Text.ToUpper();
                        if (cmbterms.Text == "Cash" && account == "CASH")
                        {
                            pnlcusdetails.Visible = true;
                            txtcustomername.Focus();
                            this.ActiveControl = txtcustomername;

                        }
                        else
                        {
                            txtitemname.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Enter Bill No");
                        TxtBillNo.Focus();
                    }
                }
            }
        }

        private void TxtRundate_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    cmbterms.Focus();
                    // this.cmbterms.BackColor = System.Drawing.Color.Red;
                }
            }
        }

        private void LVFO_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {

                    {
                        if (LVFO.SelectedItems.Count > 0)
                        {
                            rowid = LVFO.FocusedItem.Index;
                            lbliteminfo.Visible = true;

                            string dt11 = conn.ExecuteScalar("select Product_Name from ProductMaster where isactive=1 and ProductID='" + LVFO.Items[LVFO.FocusedItem.Index].SubItems[24].Text + "'");
                            txtitemname.Text = dt11;
                            //dt8 = conn.getdataset("select Productid,Batchno from ProductPriceMaster where Productid='" + dt11.Rows[0]["ProductID"].ToString() + "' and isactive='1'");
                            //DataRow dr;
                            //dr = dt8.NewRow();
                            //dr["Batchno"] = "Select Batch";
                            //dt8.Rows.InsertAt(dr, 0);
                            //cmbbatch.DataSource = dt8;
                            //cmbbatch.DisplayMember = "Batchno";
                            //cmbbatch.ValueMember = "Productid";
                            //bindbatch();
                            dt8 = new DataTable();
                            SqlCommand cmd1 = new SqlCommand("select Productid,Batchno from ProductPriceMaster where Productid='" + LVFO.Items[LVFO.FocusedItem.Index].SubItems[24].Text + "' and isactive='1'", con);
                            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                            sda1.Fill(dt8);
                            cmbbatch.ValueMember = "Productid";
                            cmbbatch.DisplayMember = "Batchno";
                            cmbbatch.DataSource = dt8;
                            cmbbatch.Focus();
                            cmbbatch.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[2].Text;
                            if (LVFO.Items[LVFO.FocusedItem.Index].SubItems[2].Text == "")
                            {
                                cmbbatch.Enabled = false;
                                cmbbatch.SelectedIndex = -1;
                            }
                            else
                            {
                                cmbbatch.Enabled = true;

                            }
                            
                            txtpacking.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[1].Text;
                            txtbags.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[3].Text;
                            txtqty.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[4].Text;
                            txtpqty.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[5].Text;
                            txtfree.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[6].Text;
                            txtrate.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[7].Text;
                            txtper.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[8].Text;
                            txttotal.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[9].Text;
                            txtdisper.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[10].Text;
                            txtdisamt.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[11].Text;
                            txttax.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[12].Text;
                            txtaddtax.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[13].Text;
                            txtamount.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[14].Text;
                            string serial = LVFO.Items[LVFO.FocusedItem.Index].SubItems[22].Text;
                            LVFO.Items[LVFO.FocusedItem.Index].SubItems[22].Text = "";
                            if (!string.IsNullOrEmpty(serial))
                            {
                                string[] values = serial.Split(',');
                                temptable = new DataTable();
                                temptable.Columns.Add("Itemname", typeof(string));
                                temptable.Columns.Add("SERIAL", typeof(string));
                                for (int i = 0; i < values.Length; i++)
                                {
                                    ListViewItem li = new ListViewItem();
                                    li = lvserial.Items.Add(values[i] + Environment.NewLine);
                                    temptable.Rows.Add(txtitemname.Text, values[i]);
                                }
                            }
                            SqlCommand cmd5 = new SqlCommand("select p.*,b.* from productmaster p inner join ProductPriceMaster b on p.ProductID=b.ProductID where p.isactive=1 and b.isactive=1 and p.ProductID='" + LVFO.Items[LVFO.FocusedItem.Index].SubItems[24].Text + "'", con);
                            SqlDataAdapter sda = new SqlDataAdapter(cmd5);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);

                            lblbagqty.Text = "[" + dt.Rows[0]["Unit"].ToString() + "]";
                            lblaltqty.Text = "[" + dt.Rows[0]["Altunit"].ToString() + "]";

                            SqlCommand cmd6 = new SqlCommand("select * from TaxSlabMaster i inner join productmaster p on i.Taxslabname=p.taxslab where p.isactive=1 and i.isactive=1 and p.ProductID='" + LVFO.Items[LVFO.FocusedItem.Index].SubItems[24].Text + "' and i.saletypename= '" + cmbsaletype.Text + "'", con);
                            SqlDataAdapter sda6 = new SqlDataAdapter(cmd6);
                            DataTable dt1 = new DataTable();
                            sda6.Fill(dt1);
                            if (dt1.Rows.Count > 0)
                            {

                                lbltax1.Text = "[" + Math.Round(Convert.ToDouble(dt1.Rows[0]["igst"].ToString()), 2).ToString() + "]";
                                taxforprice = lbltax1.Text;
                                lbladdtax1.Text = "[" + Math.Round(Convert.ToDouble(dt1.Rows[0]["additax"].ToString()), 2).ToString() + "]";
                                ataxforprice = lbladdtax1.Text;
                                txtitemname.Focus();
                                taxid = Math.Round(Convert.ToDouble(dt1.Rows[0]["igst"].ToString()), 2);
                                addtaxid = Math.Round(Convert.ToDouble(dt1.Rows[0]["additax"].ToString()), 2);

                            }
                            else
                            {

                                //  MessageBox.Show("Not any Tax Available For This Sale Type");

                                lbltax1.Text = "[0]";
                                taxforprice = lbltax1.Text;
                                lbladdtax1.Text = "[0]";
                                ataxforprice = lbladdtax1.Text;
                                taxid = 0;
                                addtaxid = 0;
                                txtitemname.Focus();
                            }

                            Double total = 0;
                            try
                            {
                                total = Math.Round((Convert.ToDouble(TxtBillTotal.Text) - Convert.ToDouble(LVFO.Items[LVFO.FocusedItem.Index].SubItems[5].Text)), 2);
                            }
                            catch
                            {
                            }
                            TxtBillTotal.Text = total.ToString();
                            //      LVFO.Items[LVFO.FocusedItem.Index].Remove();
                            totalcalculation();
                        }
                    }
                }
                if (e.KeyCode == Keys.Delete)
                {
                    DialogResult dr1 = MessageBox.Show("Do you want to Remove Item?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr1 == DialogResult.Yes)
                    {
                        LVFO.Items[LVFO.FocusedItem.Index].Remove();
                        totalcalculation();
                    }
                }

            }
        }

        private void cmbbatch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbbatch.Items.Count; i++)
                {
                    s = cmbbatch.GetItemText(cmbbatch.Items[i]);
                    if (s == cmbbatch.Text)
                    {
                        inList = true;
                        cmbbatch.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbbatch.Text = "";
                }


                //if (cmbbatch.SelectedIndex == 0)
                //{
                //    MessageBox.Show("Pls Select Batch");
                //    return;
                //}
                txtbags.Focus();
            }
            if (e.KeyCode == Keys.F3)
            {
                var privouscontroal = cmbbatch;
                activecontroal = privouscontroal.Name;
                Batch b = new Batch(this, strfinalarray, activecontroal);
                b.ShowDialog();

            }
        }

        private void txttotal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txttotal.Text))
                {
                    txttotal.Text = "0";
                }
                txtdisper.Focus();
            }
        }

        private void txttotal_TextChanged(object sender, EventArgs e)
        {

            {
                if (txttotal.Text != "" && y == 0)
                {
                    try
                    {
                        double amt = Convert.ToDouble(txttotal.Text) / Convert.ToDouble(txtpqty.Text);
                        txtrate.Text = Math.Round(amt, 2).ToString();
                        double discount = Convert.ToDouble(txttotal.Text) - Convert.ToDouble(txtdisamt.Text);

                        double amount = Math.Round(discount + ((discount * (Convert.ToDouble(taxid.ToString()) + Convert.ToDouble(addtaxid.ToString()))) / 100), 2);
                        txtamount.Text = Math.Round(amount, 2).ToString();
                        string istax = conn.ExecuteScalar("Select taxtypename from Purchasetypemaster where isactive=1 and FormType='" + strfinalarray[0] + "' and Purchasetypename='" + cmbsaletype.Text + "'");
                        if (istax != "Tax Free")
                        {
                            double tax = Math.Round(discount * (Convert.ToDouble(taxid.ToString())) / 100, 2);
                            double addtax = Math.Round(discount * (Convert.ToDouble(addtaxid.ToString())) / 100, 2);
                            txttax.Text = tax.ToString();
                            txtaddtax.Text = addtax.ToString();
                        }
                        else
                        {
                            txttax.Text = "0";
                            txtaddtax.Text = "0";
                            taxid = 0;
                            addtaxid = 0;

                        }
                        //txttax.Text = tax.ToString();
                        //txtaddtax.Text = addtax.ToString();
                    }
                    catch
                    {
                    }

                }
            }
        }

        private void txtdisper_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtdisper.Text != "" && p == 0 && Convert.ToDouble(txtdisper.Text) > 0)
                {
                    double amt = (Convert.ToDouble(txttotal.Text) * Convert.ToDouble(txtdisper.Text)) / 100;
                    txtdisamt.Text = Math.Round(amt, 2).ToString();
                    itemcalculation(txtpqty.Text);
                }
            }
            catch
            {
            }
        }

        private void txtdisper_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (string.IsNullOrEmpty(txtdisper.Text))
                    {
                        txtdisper.Text = "0";
                    }
                    txtdisamt.Focus();
                    if (options.Rows[0]["itemspeed"].ToString() == "Qty +Free Only")
                    {
                        // enteramount();
                    }
                    else if (options.Rows[0]["itemspeed"].ToString() == "Qty + Rate +Discount(%) Only")
                    {
                        enteramount();
                    }
                    else if (options.Rows[0]["itemspeed"].ToString() == "Qty + Free + Rate + Discount(%) Only ")
                    {
                        enteramount();
                    }
                    else if (options.Rows[0]["itemspeed"].ToString() == "Qty + Free + Rate Only")
                    {
                        // enteramount();
                    }
                    else if (options.Rows[0]["itemspeed"].ToString() == "Qty +Rate Only")
                    {
                        // txtrate.Focus();
                    }
                    else if (options.Rows[0]["itemspeed"].ToString() == "Qty Only")
                    {
                        // enteramount();
                    }
                }
            }
        }
        int p, q;
        private void txtdisper_KeyPress(object sender, KeyPressEventArgs e)
        {
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
            {
                p = 0;
                q = 1;
            }
        }

        private void txtfree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtfree.Text))
                {
                    txtfree.Text = "0";
                }
                DataTable serial = new DataTable();
                serial = conn.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + txtitemname.Text + "'");
                if (serial.Rows[0]["isserial"].ToString() == "True")
                {
                    pnlserial.Visible = true;
                    txtserial.Focus();
                }
                else
                {
                    x = 0;
                    txtrate.Focus();
                }
                if (options.Rows[0]["itemspeed"].ToString() == "Qty +Free Only")
                {
                    enteramount();
                    return;
                }
                else if (options.Rows[0]["itemspeed"].ToString() == "Qty + Rate +Discount(%) Only")
                {
                    txtrate.Focus();
                }
                else if (options.Rows[0]["itemspeed"].ToString() == "Qty + Free + Rate + Discount(%) Only")
                {
                    txtrate.Focus();
                }
                else if (options.Rows[0]["itemspeed"].ToString() == "Qty + Free + Rate Only")
                {
                    txtrate.Focus();
                }
                else if (options.Rows[0]["itemspeed"].ToString() == "Qty +Rate Only")
                {
                    txtrate.Focus();
                }
            }
        }

        private void txtfree_KeyPress(object sender, KeyPressEventArgs e)
        {
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
        }

        private void txtdisamt_TextChanged(object sender, EventArgs e)
        {

            {
                if ((txtdisamt.Text != "" && q == 0) && Convert.ToDouble(txtdisamt.Text) > 0)
                {
                    if (Convert.ToDouble(txttotal.Text) == 0)
                    {
                        txtdisper.Text = Math.Round(0.00, 2).ToString();
                    }
                    else
                    {
                        double amt = (100 * Convert.ToDouble(txtdisamt.Text)) / Convert.ToDouble(txttotal.Text);
                        txtdisper.Text = Math.Round(amt, 2).ToString();
                    }

                    itemcalculation(txtpqty.Text);
                }
            }
        }
        int cessflag = 0;
        private void txtdisamt_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (string.IsNullOrEmpty(txtdisamt.Text))
                    {
                        txtdisamt.Text = "0";
                    }
                    DataTable cess = conn.getdataset("select cessper,cessamt from ProductMaster where isactive=1 and Product_Name='" + txtitemname.Text + "'");
                    if (cess.Rows.Count > 0)
                    {

                        if (Convert.ToDouble(cess.Rows[0]["cessper"].ToString()) >= 0 && Convert.ToDouble(cess.Rows[0]["cessamt"].ToString()) >= 0 && cessflag == 0)
                        {
                            if (Convert.ToBoolean(options.Rows[0]["cess"].ToString()) == true)
                            {
                                cess c = new cess(cess.Rows[0]["cessper"].ToString(), cess.Rows[0]["cessamt"].ToString(), txttotal, txtcess, txtbags, txtdisamt);
                                c.ShowDialog();
                                cessflag = 1;
                            }
                            else
                            {
                                txtamount.Focus();
                            }
                        }
                        else
                        {
                            double tamt = Convert.ToDouble(txtamount.Text) + Convert.ToDouble(txtcess.Text);
                            txtamount.Text = Convert.ToString(tamt);
                            txtamount.Focus();
                        }
                    }
                    else
                    {
                        txtamount.Focus();
                    }
                }
            }
        }

        private void txtdisamt_KeyPress(object sender, KeyPressEventArgs e)
        {
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
            {
                p = 1;
                q = 0;
            }
        }

        private void txtrate_KeyPress(object sender, KeyPressEventArgs e)
        {
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
            {
                x = 0;
                y = 1;
            }
        }

        private void txttotal_Validated(object sender, EventArgs e)
        {

        }

        private void txttotal_KeyPress(object sender, KeyPressEventArgs e)
        {
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
            {
                x = 1;
                y = 0;
            }
        }

        private void txtcharremark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                DataTable dt = conn.getdataset("select * from billsundry where isactive=1 and billsundryid='" + cmbcharper.SelectedValue + "'");
                if (dt.Rows[0]["symbol"].ToString() == "%")
                {
                    txtcharval.Focus();
                }
                else
                {
                    if (dt.Rows[0]["OT3"].ToString() == "1")
                    {
                        ChargesPnl cp = new ChargesPnl(txtcharamt, this, strfinalarray);
                        cp.ShowDialog();
                        txtcharamt.Focus();
                    }
                    else
                    {
                        if (txtcharval.Text != "0")
                        {
                            txtcharval.Focus();
                        }
                        else
                        {
                            txtcharamt.Focus();
                        }
                    }
                }

            }
        }

        private void cmbcharper_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    try
                    {

                        e.SuppressKeyPress = true; // This will eliminate the beeping
                        bool inList = false;
                        for (int i = 0; i < cmbcharper.Items.Count; i++)
                        {
                            s = cmbcharper.GetItemText(cmbcharper.Items[i]);
                            if (s == cmbcharper.Text)
                            {
                                inList = true;
                                cmbcharper.Text = s;
                                break;
                            }
                        }
                        if (!inList)
                        {
                            cmbcharper.Text = "";
                        }


                        if (charges != 1)
                        {
                            if (cmbcharper.Text != "")
                            {

                                DataTable dt = conn.getdataset("select * from billsundry where isactive=1 and billsundryid='" + cmbcharper.SelectedValue + "'");
                                if (dt.Rows[0]["symbol"].ToString() == "%")
                                {

                                    if (dt.Rows[0]["applyon"].ToString() == "Net")
                                    {
                                        txtcharval.Enabled = true;
                                        txtcharat.Enabled = true;
                                        double value = (Convert.ToDouble(dt.Rows[0]["ON1"].ToString()) * Convert.ToDouble(TxtBillTotal.Text)) / 100;
                                        txtcharval.Text = Math.Round(value, 2).ToString();
                                        txtcharat.Text = dt.Rows[0]["percentage"].ToString();
                                    }
                                    else if (dt.Rows[0]["applyon"].ToString() == "Basic Amount")
                                    {
                                        txtcharval.Enabled = true;
                                        txtcharat.Enabled = true;
                                        double value = (Convert.ToDouble(dt.Rows[0]["ON1"].ToString()) * Convert.ToDouble(lblbasictot.Text)) / 100;
                                        txtcharval.Text = Math.Round(value, 2).ToString();
                                        txtcharat.Text = dt.Rows[0]["percentage"].ToString();
                                    }
                                    else if (dt.Rows[0]["applyon"].ToString() == "Items Total")
                                    {
                                        txtcharval.Enabled = true;
                                        txtcharat.Enabled = true;
                                        double value = (Convert.ToDouble(dt.Rows[0]["ON1"].ToString()) * Convert.ToDouble(lbltotpqty.Text)) / 100;
                                        txtcharval.Text = Math.Round(value, 2).ToString();
                                        txtcharat.Text = dt.Rows[0]["percentage"].ToString();
                                    }
                                    else if (dt.Rows[0]["applyon"].ToString() == "Taxable Amount")
                                    {
                                        txtcharval.Enabled = true;
                                        txtcharat.Enabled = true;
                                        double value = (Convert.ToDouble(dt.Rows[0]["ON1"].ToString()) * Convert.ToDouble(lblbasictot.Text) - Convert.ToDouble(txttotdiscount.Text) - Convert.ToDouble(txttotadis.Text)) / 100;
                                        txtcharval.Text = Math.Round(value, 2).ToString();
                                        txtcharat.Text = dt.Rows[0]["percentage"].ToString();
                                    }
                                    else if (dt.Rows[0]["applyon"].ToString() == "Tax Amount")
                                    {
                                        txtcharval.Enabled = true;
                                        txtcharat.Enabled = true;
                                        double value = (Convert.ToDouble(dt.Rows[0]["ON1"].ToString()) * Convert.ToDouble(txttottax.Text)) / 100;
                                        txtcharval.Text = Math.Round(value, 2).ToString();
                                        txtcharat.Text = dt.Rows[0]["percentage"].ToString();
                                    }
                                    else if (dt.Rows[0]["applyon"].ToString() == "Tax + AddTax")
                                    {
                                        txtcharval.Enabled = true;
                                        txtcharat.Enabled = true;
                                        double value = (Convert.ToDouble(dt.Rows[0]["ON1"].ToString()) * Convert.ToDouble(txttottax.Text) + Convert.ToDouble(txttotaddvat.Text)) / 100;
                                        txtcharval.Text = Math.Round(value, 2).ToString();
                                        txtcharat.Text = dt.Rows[0]["percentage"].ToString();
                                    }
                                    else if (dt.Rows[0]["applyon"].ToString() == "Service Charges")
                                    {
                                        txtcharval.Enabled = true;
                                        txtcharat.Enabled = true;
                                        double value = (Convert.ToDouble(dt.Rows[0]["ON1"].ToString()) * Convert.ToDouble(txttotservice.Text) + Convert.ToDouble(txttotalcharges.Text)) / 100;
                                        txtcharval.Text = Math.Round(value, 2).ToString();
                                        txtcharat.Text = dt.Rows[0]["percentage"].ToString();
                                    }
                                    else if (dt.Rows[0]["applyon"].ToString() == "Auto Round Off")
                                    {
                                        txtcharval.Enabled = true;
                                        txtcharat.Enabled = true;
                                        double value = (Convert.ToDouble(dt.Rows[0]["ON1"].ToString()) * Convert.ToDouble(txtroundoff.Text)) / 100;
                                        txtcharval.Text = Math.Round(value, 2).ToString();
                                        txtcharat.Text = dt.Rows[0]["percentage"].ToString();
                                    }
                                }
                                else
                                {
                                    txtcharval.Text = "0";
                                    //  txtcharval.Focus();
                                    // txtcharval.Enabled = false;
                                    // txtcharat.Enabled = false;
                                }

                                txtcharplusminus.Text = dt.Rows[0]["BillSundryType"].ToString();
                                txtcharamt.Text = Math.Round(Convert.ToDouble(txtcharval.Text) * (Convert.ToDouble(txtcharat.Text) / 100), 2).ToString();
                                //txtcharremark.Focus();
                            }
                        }

                    }
                    catch
                    {
                    }
                    txtcharremark.Focus();
                }
                if (e.KeyCode == Keys.F3)
                {
                    var privouscontroal = cmbcharper;
                    activecontroal = privouscontroal.Name;
                    ChargesHead popup = new ChargesHead(this, cmbcharper.Text, master, tabControl, activecontroal);
                    master.AddNewTab(popup);
                    // popup.ShowDialog();

                    //    string userEnteredText = popup.EnteredText;

                    //  popup.Dispose();

                }
                if (e.KeyCode == Keys.F2)
                {
                    var privouscontroal = cmbcharper;
                    activecontroal = privouscontroal.Name;
                    ChargesHead popup = new ChargesHead(this, cmbcharper.Text, master, tabControl, activecontroal);
                    popup.Update(1, cmbcharper.Text);
                    master.AddNewTab(popup);
                }

            }
        }
        public void bindperticular()
        {
            try
            {
                DataTable dt = conn.getdataset("select billsundryid,billsundryname from billsundry where isactive=1");


                cmbcharper.ValueMember = "billsundryid";
                cmbcharper.DisplayMember = "billsundryname";
                cmbcharper.DataSource = dt;
                cmbcharper.SelectedIndex = -1;
                charges = 0;
                //  autobind(dt, cmbcharper);
            }
            catch
            {
            }

        }
        int charges;
        private DateWiseReport dateWiseReport;
        private CashBook cashBook;
        private Master master;
        private TabControl tabControl;
        private void cmbcharper_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbcharper.Items.Count; i++)
                {
                    s = cmbcharper.GetItemText(cmbcharper.Items[i]);
                    if (s == cmbcharper.Text)
                    {
                        inList = true;
                        cmbcharper.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbcharper.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }
        private void addchargesdatadata()
        {
            DataTable dt = conn.getdataset("select * from billsundry where isactive=1 and billsundryid='" + cmbcharper.SelectedValue + "'");


        }

        private void txtcharval_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtcharat.Focus();
                }
            }
        }

        private void txtcharval_TextChanged(object sender, EventArgs e)
        {

            {
                if (txtcharval.Text != "")
                {
                    chargescalculator();
                }
            }
        }
        private void chargescalculator()
        {
            if (txtcharat.Text == "")
            {
                txtcharat.Text = "0";
            }
            txtcharamt.Text = Math.Round(Convert.ToDouble(txtcharval.Text) * (Convert.ToDouble(txtcharat.Text) / 100), 2).ToString();
        }

        private void txtcharat_TextChanged(object sender, EventArgs e)
        {

            {
                if (txtcharat.Text != "")
                {
                    chargescalculator();
                }
            }
        }

        private void txtcharplusminus_TextChanged(object sender, EventArgs e)
        {

            {

            }
        }

        private void txtcharamt_TextChanged(object sender, EventArgs e)
        {

            {

            }
        }

        private void txtcharamt_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    btncharadditem.Focus();
                }
            }
        }

        private void txtcharat_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    DataTable dt = conn.getdataset("select * from billsundry where isactive=1 and billsundryid='" + cmbcharper.SelectedValue + "'");
                    if (dt.Rows[0]["OT3"].ToString() == "1")
                    {
                        double val = Convert.ToDouble(txtcharval.Text);
                        double per = Convert.ToDouble(txtcharat.Text);
                        double totalval = val * per / 100;
                        taxvalue = Convert.ToString(totalval);
                        ChargesPnl cp = new ChargesPnl(txtcharamt, this, strfinalarray);
                        cp.ShowDialog();
                        txtcharamt.Focus();
                    }
                    else
                    {
                        txtcharamt.Focus();
                    }
                }
            }
        }

        private void btncharadditem_Click(object sender, EventArgs e)
        {

            {
                try
                {
                    ListViewItem li;
                    lbliteminfo.Visible = false;
                    li = LVCHARGES.Items.Add(cmbcharper.Text);
                    li.SubItems.Add(txtcharremark.Text);
                    li.SubItems.Add(txtcharval.Text);
                    li.SubItems.Add(txtcharat.Text);
                    li.SubItems.Add(txtcharplusminus.Text);
                    li.SubItems.Add((Math.Round(Convert.ToDouble(txtcharamt.Text), 2).ToString()));
                    DataTable dt = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + cmbsaletype.Text + "'");
                    string charge = conn.ExecuteScalar("select BillSundryID from BillSundry where isactive=1 and BillSundryName='" + cmbcharper.Text + "'");
                    if (dt.Rows[0]["Region"].ToString() == "Local")
                    {
                        if (ChargesPnl.valofexp == "")
                        {
                            li.SubItems.Add("0");
                        }
                        else
                        {
                            li.SubItems.Add(ChargesPnl.valofexp);
                        }
                        if (ChargesPnl.tax1 == "")
                        {
                            li.SubItems.Add("0");
                        }
                        else
                        {
                            li.SubItems.Add(ChargesPnl.tax1);
                        }
                        if (ChargesPnl.sgst1 == "")
                        {
                            li.SubItems.Add("0");
                        }
                        else
                        {
                            li.SubItems.Add(ChargesPnl.sgst1);
                        }
                        if (ChargesPnl.cgst1 == "")
                        {
                            li.SubItems.Add("0");
                        }
                        else
                        {
                            li.SubItems.Add(ChargesPnl.cgst1);
                        }
                        if (ChargesPnl.igst1 == "")
                        {
                            li.SubItems.Add("0");
                        }
                        else
                        {
                            li.SubItems.Add(ChargesPnl.igst1);
                        }
                        if (ChargesPnl.additax == "")
                        {
                            li.SubItems.Add("0");
                        }
                        else
                        {
                            li.SubItems.Add(ChargesPnl.additax);
                        }
                        if (ChargesPnl.additaxamt == "")
                        {
                            li.SubItems.Add("0");
                        }
                        else
                        {
                            li.SubItems.Add(ChargesPnl.additaxamt);
                        }
                        li.SubItems.Add(charge);
                    }
                    else
                    {
                        if (ChargesPnl.valofexp == "")
                        {
                            li.SubItems.Add("0");
                        }
                        else
                        {
                            li.SubItems.Add(ChargesPnl.valofexp);
                        }
                        if (ChargesPnl.tax1 == "")
                        {
                            li.SubItems.Add("0");
                        }
                        else
                        {
                            li.SubItems.Add(ChargesPnl.tax1);
                        }
                        if (ChargesPnl.sgst1 == "")
                        {
                            li.SubItems.Add("0");
                        }
                        else
                        {
                            li.SubItems.Add(ChargesPnl.sgst1);
                        }
                        if (ChargesPnl.cgst1 == "")
                        {
                            li.SubItems.Add("0");
                        }
                        else
                        {
                            li.SubItems.Add(ChargesPnl.cgst1);
                        }
                        if (ChargesPnl.igst1 == "")
                        {
                            li.SubItems.Add("0");
                        }
                        else
                        {
                            li.SubItems.Add(ChargesPnl.igst1);
                        }
                        if (ChargesPnl.additax == "")
                        {
                            li.SubItems.Add("0");
                        }
                        else
                        {
                            li.SubItems.Add(ChargesPnl.additax);
                        }
                        if (ChargesPnl.additaxamt == "")
                        {
                            li.SubItems.Add("0");
                        }
                        else
                        {
                            li.SubItems.Add(ChargesPnl.additaxamt);
                        }
                        li.SubItems.Add(charge);
                    }



                    calculatetotalcharges();

                    totalcalculation();
                    clearcharitem();
                    cmbcharper.Focus();
                }
                catch
                {
                }
            }
        }
        private void calculatetotalcharges()
        {
            Double charges = 0;
            for (int i = 0; i < LVCHARGES.Items.Count; i++)
            {
                if (LVCHARGES.Items[i].SubItems[4].Text == "+")
                {
                    string str = LVCHARGES.Items[i].SubItems[5].Text;
                    //  totalcalculation();

                    charges += Convert.ToDouble(LVCHARGES.Items[i].SubItems[5].Text);
                }
                if (LVCHARGES.Items[i].SubItems[4].Text == "-")
                {
                    string str = LVCHARGES.Items[i].SubItems[5].Text;
                    charges -= Convert.ToDouble(LVCHARGES.Items[i].SubItems[5].Text);
                }

            }
            txttotalcharges.Text = Math.Round(charges, 2).ToString();
        }

        private void clearcharitem()
        {
            cmbcharper.Text = "";
            txtcharamt.Text = "";
            txtcharat.Text = "";
            txtcharremark.Text = "";
            txtcharval.Text = "";
            txtcharplusminus.Text = "";
            ChargesPnl.valofexp = "";
            ChargesPnl.tax1 = "";
            ChargesPnl.sgst1 = "";
            ChargesPnl.cgst1 = "";
            ChargesPnl.igst1 = "";
            ChargesPnl.additax = "";

        }

        private void txtcharval_KeyPress(object sender, KeyPressEventArgs e)
        {

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
        }

        private void txtcharat_KeyPress(object sender, KeyPressEventArgs e)
        {

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
        }

        private void txtcharamt_KeyPress(object sender, KeyPressEventArgs e)
        {

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
        }
        public static string lvfvalue, lvftax, lvfsgst, lvfcgst, lvfigst, lvfaddtax, lvfaddtaxamt;
        public void open()
        {
            try
            {
                if (LVCHARGES.SelectedItems.Count > 0)
                {
                    cmbcharper.Text = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[0].Text;
                    txtcharremark.Text = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[1].Text;
                    txtcharval.Text = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[2].Text;
                    txtcharat.Text = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[3].Text;
                    txtcharplusminus.Text = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[4].Text;
                    txtcharamt.Text = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[5].Text;
                    lvfvalue = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[6].Text;
                    lvftax = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[7].Text;
                    lvfsgst = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[8].Text;
                    lvfcgst = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[9].Text;
                    lvfigst = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[10].Text;
                    lvfaddtax = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[11].Text;
                    lvfaddtax = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[12].Text;

                    LVCHARGES.Items[LVCHARGES.FocusedItem.Index].Remove();
                    calculatetotalcharges();
                    totalcalculation();
                    cmbcharper.Focus();

                }
            }
            catch
            {
            }
        }
        private void LVCHARGES_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            {
                open();
            }
        }

        private void LVCHARGES_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    open();
                }
                if (e.KeyCode == Keys.Delete)
                {
                    LVCHARGES.Items[LVCHARGES.FocusedItem.Index].Remove();
                    calculatetotalcharges();
                    totalcalculation();
                }
            }
        }

        private void txttransport_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtdelieveryat.Focus();
                }
            }
        }

        private void txtdelieveryat_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtfraight.Focus();
                }
            }
        }

        private void txtfraight_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtvehicleno.Focus();
                }
            }
        }

        private void txtvehicleno_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtgrrrno.Focus();
                }
            }
        }

        private void txtgrrrno_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtremarks.Focus();
            }
        }

        private void txtskids_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    BtnPayment.Focus();
                }
            }
        }

        private void txtduedate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbcustname.Focus();
            }
        }
        double sgstper, sgstamt, cgstper, cgstamt, igstper, igstamt, tax, addtaxper;
        private string[] strfinalarray;
        private ItemWiseStock itemWiseStock;
        private SaleOrderList saleOrderList;
        string serialno;
        private SaleOrder saleOrder;
        private string p_2;
        public void enteramount()
        {
            try
            {
                DataTable dt = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + cmbsaletype.Text + "'");
                dt1 = conn.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + txtitemname.Text + "'");
                if (dt.Rows[0]["Region"].ToString() == "Local")
                {
                    if (string.IsNullOrEmpty(taxper))
                    {
                        string a = lbltax1.Text;
                        a = a.Remove(a.Length - 1, 1);
                        var sub = a;
                        sub = sub.Substring(1);
                        taxper = sub;
                    }
                    tax = Convert.ToDouble(txttax.Text);
                    sgstamt = tax / 2;
                    cgstamt = tax / 2;
                    sgstper = Convert.ToDouble(taxper) / 2;
                    cgstper = Convert.ToDouble(taxper) / 2;
                    addtaxper = Convert.ToDouble(additaxper);
                    igstper = 0;
                    igstamt = 0.00;


                }
                else
                {
                    if (string.IsNullOrEmpty(taxper))
                    {
                        string a = lbltax1.Text;
                        a = a.Remove(a.Length - 1, 1);
                        var sub = a;
                        sub = sub.Substring(1);
                        taxper = sub;
                    }
                    tax = Convert.ToDouble(txttax.Text);
                    igstper = Convert.ToDouble(taxper);
                    igstamt = tax;
                    sgstper = 0;
                    sgstamt = 0.00;
                    cgstper = 0;
                    cgstamt = 0.00;
                    addtaxper = Convert.ToDouble(additaxper);
                }
                if (rowid >= 0)
                {
                    lbliteminfo.Visible = false;
                    LVFO.Items[rowid].SubItems[0].Text = txtitemname.Text;
                    LVFO.Items[rowid].SubItems[1].Text = txtpacking.Text;
                    if (cmbbatch.Text == "")
                    {
                        DataTable batch = conn.getdataset("select Productid,Batchno from ProductPriceMaster where Productid='" + dt1.Rows[0]["ProductID"].ToString() + "' and isactive='1'");
                        LVFO.Items[rowid].SubItems[2].Text = batch.Rows[0]["Batchno"].ToString();
                    }
                    else
                    {
                        LVFO.Items[rowid].SubItems[2].Text = cmbbatch.Text;
                    }
                    LVFO.Items[rowid].SubItems[3].Text = txtbags.Text;
                    LVFO.Items[rowid].SubItems[4].Text = (Math.Round(Convert.ToDouble(txtqty.Text), 2).ToString());
                    LVFO.Items[rowid].SubItems[5].Text = (Math.Round(Convert.ToDouble(txtpqty.Text), 2).ToString());
                    LVFO.Items[rowid].SubItems[6].Text = txtfree.Text;
                    LVFO.Items[rowid].SubItems[7].Text = (Math.Round(Convert.ToDouble(txtrate.Text), 2).ToString());
                    LVFO.Items[rowid].SubItems[8].Text = txtper.Text;
                    LVFO.Items[rowid].SubItems[9].Text = (Math.Round(Convert.ToDouble(txttotal.Text), 2).ToString());
                    LVFO.Items[rowid].SubItems[10].Text = (Math.Round(Convert.ToDouble(txtdisper.Text), 2).ToString());
                    LVFO.Items[rowid].SubItems[11].Text = (Math.Round((Convert.ToDouble(txtdisamt.Text)), 2).ToString());
                    LVFO.Items[rowid].SubItems[12].Text = (Math.Round((Convert.ToDouble(txttax.Text)), 2).ToString());
                    LVFO.Items[rowid].SubItems[13].Text = (Math.Round((Convert.ToDouble(txtaddtax.Text)), 2).ToString());
                    // double tamt = Convert.ToDouble(txtamount.Text) + Convert.ToDouble(txtcess.Text);
                    // txtamount.Text = Convert.ToString(tamt);
                    LVFO.Items[rowid].SubItems[14].Text = (Math.Round((Convert.ToDouble(txtamount.Text)), 2).ToString());
                    LVFO.Items[rowid].SubItems[15].Text = (Math.Round((Convert.ToDouble(sgstper)), 2).ToString());
                    LVFO.Items[rowid].SubItems[16].Text = (Math.Round((Convert.ToDouble(sgstamt)), 2).ToString());
                    LVFO.Items[rowid].SubItems[17].Text = (Math.Round((Convert.ToDouble(cgstper)), 2).ToString());
                    LVFO.Items[rowid].SubItems[18].Text = (Math.Round((Convert.ToDouble(cgstamt)), 2).ToString());
                    LVFO.Items[rowid].SubItems[19].Text = (Math.Round((Convert.ToDouble(igstper)), 2).ToString());
                    LVFO.Items[rowid].SubItems[20].Text = (Math.Round((Convert.ToDouble(igstamt)), 2).ToString());
                    LVFO.Items[rowid].SubItems[21].Text = (Math.Round((Convert.ToDouble(addtaxper)), 2).ToString());
                    for (int i = 0; i < lvserial.Items.Count; i++)
                    {
                        serialno += lvserial.Items[i].SubItems[0].Text + ",";


                    }
                    if (serialno != null)
                    {
                        //serialno = "NA";
                        serialno = serialno.TrimEnd(',');
                    }
                    LVFO.Items[rowid].SubItems[22].Text = (serialno);
                    LVFO.Items[rowid].SubItems[23].Text = (txtcess.Text);
                    LVFO.Items[rowid].SubItems[24].Text = (dt1.Rows[0]["ProductID"].ToString());
                    serialno = "";
                    txtcess.Text = "0";
                    rowid = -1;
                    cessflag = 0;
                }
                else
                {
                    ListViewItem li;
                    lbliteminfo.Visible = false;
                    li = LVFO.Items.Add(txtitemname.Text);
                    li.SubItems.Add(txtpacking.Text);
                    if (cmbbatch.Text == "")
                    {
                        DataTable batch1 = conn.getdataset("select Productid,Batchno from ProductPriceMaster where Productid='" + dt1.Rows[0]["ProductID"].ToString() + "' and isactive='1'");
                        li.SubItems.Add(batch1.Rows[0]["Batchno"].ToString());
                    }
                    else
                    {
                        li.SubItems.Add(cmbbatch.Text);
                    }
                    li.SubItems.Add(txtbags.Text);
                    li.SubItems.Add((Math.Round(Convert.ToDouble(txtqty.Text), 2).ToString()));
                    li.SubItems.Add((Math.Round(Convert.ToDouble(txtpqty.Text), 2).ToString()));
                    li.SubItems.Add(txtfree.Text);
                    li.SubItems.Add((Math.Round(Convert.ToDouble(txtrate.Text), 2).ToString()));
                    li.SubItems.Add(txtper.Text);
                    li.SubItems.Add((Math.Round(Convert.ToDouble(txttotal.Text), 2).ToString()));
                    li.SubItems.Add((Math.Round(Convert.ToDouble(txtdisper.Text), 2).ToString()));
                    li.SubItems.Add((Math.Round((Convert.ToDouble(txtdisamt.Text)), 2).ToString()));
                    li.SubItems.Add((Math.Round((Convert.ToDouble(txttax.Text)), 2).ToString()));
                    li.SubItems.Add((Math.Round((Convert.ToDouble(txtaddtax.Text)), 2).ToString()));
                    //   double tamt = Convert.ToDouble(txtamount.Text) + Convert.ToDouble(txtcess.Text);
                    // txtamount.Text = Convert.ToString(tamt);
                    li.SubItems.Add((Math.Round(Convert.ToDouble(txtamount.Text), 2).ToString()));
                    li.SubItems.Add((Math.Round(Convert.ToDouble(sgstper), 2).ToString()));
                    li.SubItems.Add((Math.Round(Convert.ToDouble(sgstamt), 2).ToString()));
                    li.SubItems.Add((Math.Round(Convert.ToDouble(cgstper), 2).ToString()));
                    li.SubItems.Add((Math.Round(Convert.ToDouble(cgstamt), 2).ToString()));
                    li.SubItems.Add((Math.Round(Convert.ToDouble(igstper), 2).ToString()));
                    li.SubItems.Add((Math.Round(Convert.ToDouble(igstamt), 2).ToString()));
                    li.SubItems.Add((Math.Round(Convert.ToDouble(addtaxper), 2).ToString()));
                    for (int i = 0; i < lvserial.Items.Count; i++)
                    {
                        serialno += lvserial.Items[i].SubItems[0].Text + ",";


                    }
                    if (serialno != null)
                    {
                        //serialno = "NA";
                        serialno = serialno.TrimEnd(',');
                    }
                    li.SubItems.Add(serialno);
                    li.SubItems.Add(txtcess.Text);
                    li.SubItems.Add(dt1.Rows[0]["ProductID"].ToString());
                    txtcess.Text = "0";
                    serialno = "";
                }
                if (Convert.ToDouble(txtamount.Text) > 0)
                {
                    if (Convert.ToBoolean(options.Rows[0]["itemsinasedingorderinsale"].ToString()) == true)
                    {
                        this.LVFO.ListViewItemSorter = new ListViewItemComparer(0);
                    }
                }

                totalcalculation();
                clearitem();
                txtitemname.Focus();
                cessflag = 0;
                bindallitem();
            }
            catch
            {
            }
        }
        private void txtamount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                enteramount();
            }
        }

        private void txtamount_TextChanged(object sender, EventArgs e)
        {

        }


        public double[] calculatediscount(string itemname, string partyname, double basicprice)
        {
            bool isadd = false;
            double itemper = 0, itemamt = 0, discount = 0, disamt = 0, dispercompany = 0, disamtcompant = 0;
            string discountval = "";
            DataTable dt12 = conn.getdataset("select * from PartyRates where itemname='" + itemname + "' and (PartyName='" + partyname + "' OR PartyID='0')");

            if (dt12.Rows.Count > 0 && isadd == false && (Convert.ToDouble(dt12.Rows[0]["Discount"].ToString()) != 0 || Convert.ToDouble(dt12.Rows[0]["SpecialRate"].ToString()) != 0))
            {
                itemper = Convert.ToDouble(dt12.Rows[0]["Discount"].ToString());
                itemamt = Convert.ToDouble(dt12.Rows[0]["SpecialRate"].ToString());
                isadd = true;
            }
            DataTable dtgroup = conn.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + itemname + "'");
            string groupid = dtgroup.Rows[0]["GroupName"].ToString();
            string companyid = dtgroup.Rows[0]["CompanyID"].ToString();
            DataTable dt123 = conn.getdataset("select * from PartyGroupDiscount where ItemGroupName='" + groupid + "'and(PartyName='" + partyname + "'OR PartyID=' ')");


            if (dt123.Rows.Count > 0 && isadd == false && (Convert.ToDouble(dt123.Rows[0]["Discount"].ToString()) != 0 || Convert.ToDouble(dt123.Rows[0]["SpecialRate"].ToString()) != 0))
            {
                itemper = Convert.ToDouble(dt123.Rows[0]["Discount"].ToString());
                itemamt = Convert.ToDouble(dt123.Rows[0]["SpecialRate"].ToString());
                isadd = true;
            }

            DataTable dt1234 = conn.getdataset("select * from PartyCompanyDiscount where ItemCompanyID='" + companyid + "'and(PartyName='" + partyname + "'OR PartyID=' ')");
            if (dt1234.Rows.Count > 0 && isadd == false && (Convert.ToDouble(dt1234.Rows[0]["Discount"].ToString()) != 0 || Convert.ToDouble(dt1234.Rows[0]["SpecialRate"].ToString()) != 0))
            {
                itemper = Convert.ToDouble(dt1234.Rows[0]["Discount"].ToString());
                itemamt = Convert.ToDouble(dt1234.Rows[0]["SpecialRate"].ToString());
                isadd = true;
            }

            //double disper = 0, disa = 0;
            //if (itemper > 0)
            //{
            //    disper = itemper;
            //    disa = 0;
            //    if (disper != 0)
            //    {
            //        disa = (Convert.ToDouble(basicprice) * Convert.ToDouble(disper)) / 100;
            //    }
            //    else
            //    {
            //        disper = (100 * Convert.ToDouble(disa)) / Convert.ToDouble(basicprice);
            //    }
            //    disper = Math.Round(disper, 2);
            //    disamt = Math.Round(disa, 2);

            //}
            //else if (itemamt > 0)
            //{
            //    //disper = 0;
            //    //disa = itemamt;
            //    //if (disper != 0)
            //    //{
            //    //    disa = (Convert.ToDouble(basicprice) * Convert.ToDouble(disper)) / 100;
            //    //}
            //    //else
            //    //{
            //    //    disper = (100 * Convert.ToDouble(disa)) / Convert.ToDouble(basicprice);
            //    //}
            //    //disper = Math.Round(disper, 2);
            //    //disamt = Math.Round(disa, 2);

            //}
            //else if (discount > 0)
            //{
            //    disper = discount;
            //    disa = 0;
            //    if (disper != 0)
            //    {
            //        disa = (Convert.ToDouble(basicprice) * Convert.ToDouble(disper)) / 100;
            //    }
            //    else
            //    {
            //        disper = (100 * Convert.ToDouble(disa)) / Convert.ToDouble(basicprice);
            //    }
            //    disper = Math.Round(disper, 2);
            //    disamt = Math.Round(disa, 2);
            //}
            //else if (disamt > 0)
            //{
            //    //disper = 0;
            //    //disa = disamt;
            //    //if (disper != 0)
            //    //{
            //    //    disa = (Convert.ToDouble(basicprice) * Convert.ToDouble(disper)) / 100;
            //    //}
            //    //else
            //    //{
            //    //    disper = (100 * Convert.ToDouble(disa)) / Convert.ToDouble(basicprice);
            //    //}
            //    //disper = Math.Round(disper, 2);
            //    //disamt = Math.Round(disa, 2);
            //}
            //else if (dispercompany > 0)
            //{
            //    disper = dispercompany;
            //    disa = 0;
            //    if (disper != 0)
            //    {
            //        disa = (Convert.ToDouble(basicprice) * Convert.ToDouble(disper)) / 100;
            //    }
            //    else
            //    {
            //        disper = (100 * Convert.ToDouble(disa)) / Convert.ToDouble(basicprice);
            //    }
            //    disper = Math.Round(disper, 2);
            //    disamt = Math.Round(disa, 2);
            //}
            //else if (disamtcompant > 0)
            //{
            //    //disper = 0;
            //    //disa = disamtcompant;
            //    //if (disper != 0)
            //    //{
            //    //    disa = (Convert.ToDouble(basicprice) * Convert.ToDouble(disper)) / 100;
            //    //}
            //    //else
            //    //{
            //    //    disper = (100 * Convert.ToDouble(disa)) / Convert.ToDouble(basicprice);
            //    //}
            //    //disper = Math.Round(disper, 2);
            //    //disamt = Math.Round(disa, 2);
            //}
            double[] dis = new double[2];
            dis[0] = itemper;
            dis[1] = itemamt;
            return dis;
        }
        private void cmbbatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbbatch.SelectedIndex == -1)
                {
                    lbliteminfo.Text = "iteminfo";
                    return;

                }
                else
                {
                    dt5 = conn.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + txtitemname.Text + "'");
                    dt6 = conn.getdataset("select * from TaxSlabMaster i inner join productmaster p on i.Taxslabname=p.taxslab where p.isactive=1 and i.isactive=1 and p.product_name='" + txtitemname.Text + "' and i.saletypename= '" + cmbsaletype.Text + "'");
                    dt7 = conn.getdataset("select * from ProductPriceMaster where isactive=1 and Productid='" + dt1.Rows[0]["ProductID"].ToString() + "' and Batchno='" + cmbbatch.Text + "'");
                    dt = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + cmbsaletype.Text + "'");
                    if (dt.Rows[0]["Region"].ToString() == "Local")
                    {
                        if (dt6.Rows.Count > 0 && dt7.Rows.Count > 0)
                        {
                            lbliteminfo.Text = "Tax=" + dt6.Rows[0]["Sgst"].ToString() + "% + " + dt6.Rows[0]["cgst"].ToString() + "%,AddTax=" + dt6.Rows[0]["additax"].ToString() + ",Sale Price=" + dt7.Rows[0]["SalePrice"].ToString() + ",MRP=" + dt7.Rows[0]["MRP"].ToString() + ",Basic=" + dt7.Rows[0]["BasicPrice"].ToString() + ",PurchasePrice=" + dt7.Rows[0]["PurchasePrice"].ToString() + "";
                        }
                        else if (dt6.Rows.Count > 0 && dt7.Rows.Count <= 0)
                        {
                            lbliteminfo.Text = "Tax=" + dt6.Rows[0]["Sgst"].ToString() + "% + " + dt6.Rows[0]["cgst"].ToString() + "%,AddTax=" + dt6.Rows[0]["additax"].ToString() + ",Sale Price=" + 0 + ",MRP=" + 0 + ",Basic=" + 0 + ",PurchasePrice=" + 0 + "";
                        }
                        else if (dt6.Rows.Count <= 0 && dt7.Rows.Count > 0)
                        {
                            lbliteminfo.Text = "Tax=" + 0 + "% + " + 0 + "%,AddTax=" + 0 + ",Sale Price=" + dt7.Rows[0]["SalePrice"].ToString() + ",MRP=" + dt7.Rows[0]["MRP"].ToString() + ",Basic=" + dt7.Rows[0]["BasicPrice"].ToString() + ",PurchasePrice=" + dt7.Rows[0]["PurchasePrice"].ToString() + "";
                        }
                        else if (dt6.Rows.Count <= 0 && dt7.Rows.Count <= 0)
                        {
                            lbliteminfo.Text = "Tax=" + 0 + "% + " + 0 + "%,AddTax=" + 0 + ",Sale Price=" + 0 + ",MRP=" + 0 + ",Basic=" + 0 + ",PurchasePrice=" + 0 + "";
                        }
                        // lbliteminfo.Text = "Tax=" + dt2.Rows[0]["Sgst"].ToString() + "% + " + dt2.Rows[0]["cgst"].ToString() + "%,AddTax=" + dt2.Rows[0]["additax"].ToString() + ",Sale Price=" + dt7.Rows[0]["SalePrice"].ToString() + ",MRP=" + dt7.Rows[0]["MRP"].ToString() + ",Basic=" + dt7.Rows[0]["BasicPrice"].ToString() + "";
                    }
                    else
                    {
                        if (dt6.Rows.Count > 0 && dt7.Rows.Count > 0)
                        {
                            lbliteminfo.Text = "Tax=" + dt6.Rows[0]["Igst"].ToString() + "%,AddTax=" + dt6.Rows[0]["additax"].ToString() + ",Sale Price=" + dt7.Rows[0]["SalePrice"].ToString() + ",MRP=" + dt7.Rows[0]["MRP"].ToString() + ",Basic=" + dt7.Rows[0]["BasicPrice"].ToString() + ",PurchasePrice=" + dt7.Rows[0]["PurchasePrice"].ToString() + "";
                        }
                        else if (dt6.Rows.Count > 0 && dt7.Rows.Count <= 0)
                        {
                            lbliteminfo.Text = "Tax=" + dt6.Rows[0]["Igst"].ToString() + "%,AddTax=" + dt6.Rows[0]["additax"].ToString() + ",Sale Price=" + 0 + ",MRP=" + 0 + ",Basic=" + 0 + ",PurchasePrice=" + 0 + "";
                        }
                        else if (dt6.Rows.Count <= 0 && dt7.Rows.Count > 0)
                        {
                            lbliteminfo.Text = "Tax=" + 0 + "%,AddTax=" + 0 + ",Sale Price=" + dt7.Rows[0]["SalePrice"].ToString() + ",MRP=" + dt7.Rows[0]["MRP"].ToString() + ",Basic=" + dt7.Rows[0]["BasicPrice"].ToString() + ",PurchasePrice=" + dt7.Rows[0]["PurchasePrice"].ToString() + "";
                        }
                        else if (dt6.Rows.Count <= 0 && dt7.Rows.Count <= 0)
                        {
                            lbliteminfo.Text = "Tax=" + 0 + "%,AddTax=" + 0 + ",Sale Price=" + 0 + ",MRP=" + 0 + ",Basic=" + 0 + ",PurchasePrice=" + 0 + "";
                        }

                    }
                    x = 0;
                    DataTable dprice = new DataTable();
                    dprice = conn.getdataset("select PickupPrice from PurchasetypeMaster where isactive=1 and FormType='" + strfinalarray[0] + "' and Purchasetypename='" + cmbsaletype.Text + "'");
                    Dprice = dprice.Rows[0]["PickupPrice"].ToString();
                    SqlCommand cmd5 = new SqlCommand("select p.*,b.*,b." + Dprice + " as Dprice from productmaster p inner join ProductPriceMaster b on p.ProductID=b.ProductID where p.isactive=1 and b.isactive=1 and p.product_name='" + txtitemname.Text + "' and b.Batchno='" + cmbbatch.Text + "'", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd5);
                    DataTable dt9 = new DataTable();
                    sda.Fill(dt9);
                    lbliteminfo.Visible = true;
                    qtyflag = 0;
                    double[] dis = new double[2];
                    dis = calculatediscount(dt9.Rows[0]["Product_Name"].ToString(), cmbcustname.Text, Convert.ToDouble(dt9.Rows[0]["Dprice"].ToString()));
                    if (Convert.ToDouble(dis[1].ToString()) != 0)
                    {
                        txtrate.Text = dis[1].ToString();
                    }
                    else
                    {
                        txtrate.Text = dt9.Rows[0]["Dprice"].ToString();
                    }
                    txtdisper.Text = dis[0].ToString();
                    //DataTable dis = conn.getdataset("select specialrate,discount from partyrates where (partyid=0 or partyid='" + cmbcustname.SelectedValue + "') and itemid=" + dt9.Rows[0]["productid"].ToString());
                    //if (dis.Rows.Count > 0)
                    //{
                    //    if (Convert.ToDouble(dis.Rows[0]["specialrate"].ToString()) > 0)
                    //    {
                    //        txtrate.Text = dis.Rows[0]["specialrate"].ToString();
                    //    }
                    //    else
                    //    {
                    //        txtrate.Text = dt9.Rows[0]["Dprice"].ToString();
                    //    }
                    //    if (Convert.ToDouble(dis.Rows[0]["discount"].ToString()) > 0)
                    //    {
                    //        txtdisper.Text = dis.Rows[0]["discount"].ToString();
                    //    }
                    //    else
                    //    {
                    //        txtdisper.Text = dis.Rows[0]["discount"].ToString();
                    //    }
                    //}
                    //else
                    //{
                    //    txtrate.Text = dt9.Rows[0]["Dprice"].ToString();
                    //    txtdisper.Text = "0.00";
                    //}

                    txtper.Text = dt9.Rows[0]["Unit"].ToString();
                    lblbagqty.Text = "[" + dt9.Rows[0]["Unit"].ToString() + "]";
                    lblaltqty.Text = "[" + dt9.Rows[0]["Altunit"].ToString() + "]";
                    txtqty.Text = dt9.Rows[0]["Convfactor"].ToString();
                    txtdisamt.Text = "0.00";

                    txtfree.Text = "0";
                    SqlCommand cmd6 = new SqlCommand("select * from TaxSlabMaster i inner join productmaster p on i.Taxslabname=p.taxslab where p.isactive=1 and i.isactive=1 and p.product_name='" + txtitemname.Text + "' and i.saletypename= '" + cmbsaletype.Text + "'", con);
                    SqlDataAdapter sda6 = new SqlDataAdapter(cmd6);
                    DataTable dt10 = new DataTable();
                    sda6.Fill(dt10);
                    string istax = conn.ExecuteScalar("Select taxtypename from Purchasetypemaster where isactive=1 and FormType='" + strfinalarray[0] + "' and Purchasetypename='" + cmbsaletype.Text + "'");
                    if (dt10.Rows.Count > 0)
                    {
                        txttax.Text = "0";
                        txtaddtax.Text = "0";
                        if (istax != "Tax Free")
                        {
                            lbltax1.Text = "[" + Math.Round(Convert.ToDouble(dt10.Rows[0]["igst"].ToString()), 2).ToString() + "]";
                            taxforprice = lbltax1.Text;
                            taxper = dt10.Rows[0]["igst"].ToString();
                            lbladdtax1.Text = "[" + Math.Round(Convert.ToDouble(dt10.Rows[0]["additax"].ToString()), 2).ToString() + "]";
                            ataxforprice = lbladdtax1.Text;
                            additaxper = dt10.Rows[0]["additax"].ToString();
                            taxid = Math.Round(Convert.ToDouble(dt10.Rows[0]["igst"].ToString()), 2);
                            addtaxid = Math.Round(Convert.ToDouble(dt10.Rows[0]["additax"].ToString()), 2);
                        }
                        else
                        {
                            lbltax1.Text = "[0]";
                            taxforprice = lbltax1.Text;
                            lbladdtax1.Text = "[0]";
                            ataxforprice = lbladdtax1.Text;
                            taxid = 0;
                            addtaxid = 0;
                        }




                    }
                    else
                    {

                        //  MessageBox.Show("Not any Tax Available For This Sale Type");
                        txttax.Text = "0";
                        txtaddtax.Text = "0";
                        lbltax1.Text = "[0]";
                        taxforprice = lbltax1.Text;
                        lbladdtax1.Text = "[0]";
                        ataxforprice = lbladdtax1.Text;
                        taxid = 0;
                        addtaxid = 0;
                        //txtpacking.Focus();
                    }

                }

            }
            catch
            {
            }
        }

        private void cmbterms_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbterms.SelectedIndex = 0;
                cmbterms.DroppedDown = true;
            }
            catch
            {
            }


        }

        private void cmbcustname_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbcustname.SelectedIndex = 0;
                cmbcustname.DroppedDown = true;
            }
            catch
            {
            }

        }

        private void cmbsaletype_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbsaletype.SelectedIndex = 0;
                cmbsaletype.DroppedDown = true;
            }
            catch
            {
            }

        }

        //private void cmbbatch_Enter(object sender, EventArgs e)
        //{
        //    if (cmbbatch.Enabled == true)
        //    {
        //       // cmbbatch.SelectedIndex = 0;
        //        int count = cmbbatch.Items.Count - 1;
        //        cmbbatch.SelectedIndex = count;

        //        cmbbatch.DroppedDown = true;
        //    }

        //}

        private void cmbcharper_Enter(object sender, EventArgs e)
        {
            try
            {

                cmbcharper.SelectedIndex = 0;
                cmbcharper.DroppedDown = true;
            }
            catch
            {
            }

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
        int flagserial = 0;
        private void txtserial_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    double qty11 = Convert.ToDouble(txtfree.Text) + Convert.ToDouble(txtbags.Text);
                    if (lvserial.Items.Count >= Convert.ToInt64(qty11))
                    {
                        txtserial.Text = "";
                        pnlserial.Visible = false;
                        txtrate.Focus();
                        flagserial = 1;
                    }
                    if (lvserial.Items.Count > Convert.ToInt64(txtbags.Text) && serialno != "NA")
                    {
                        MessageBox.Show("Serial no is More than Qty Please Remove");
                        return;
                    }
                    if (txtserial.Text == "" && flagserial == 0)
                    {
                        MessageBox.Show("Enter Serial No");
                        return;
                    }
                    string qty1 = txtbags.Text;
                    string free = txtfree.Text;
                    double qty = Convert.ToDouble(txtfree.Text) + Convert.ToDouble(txtbags.Text);
                    ListViewItem li = new ListViewItem();
                    foreach (ListViewItem item in lvserial.Items)
                    {
                        String withoutLast = item.Text.Substring(0, (item.Text.Length - 2));
                        if (withoutLast == txtserial.Text)
                        {
                            MessageBox.Show("Serial No is exists");
                            return;
                        }
                    }

                    if (lvserial.Items.Count >= Convert.ToInt64(qty))
                    {

                        txtserial.Text = "";
                        pnlserial.Visible = false;
                        txtrate.Focus();
                    }
                    else
                    {
                        li = lvserial.Items.Add(txtserial.Text + Environment.NewLine);
                        temptable.Rows.Add(txtitemname.Text, txtserial.Text);
                        txtserial.Text = "";
                        if (lvserial.Items.Count >= Convert.ToInt64(qty))
                        {
                            txtserial.Text = "";
                            pnlserial.Visible = false;
                            txtrate.Focus();
                        }
                    }
                    txtserial.Text = "";
                }
                flagserial = 0;

            }
            catch
            {
            }
        }

        private void txtpono_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txttax_KeyPress(object sender, KeyPressEventArgs e)
        {
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
        }

        private void txtaddtax_KeyPress(object sender, KeyPressEventArgs e)
        {
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
        }

        private void txtamount_KeyPress(object sender, KeyPressEventArgs e)
        {
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
        }

        private void txtserial_KeyPress(object sender, KeyPressEventArgs e)
        {
            {

                //if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 45 || e.KeyChar == 8)
                //{
                //    e.Handled = false;
                //}
                //else
                //{
                //    e.Handled = true;
                //}
            }
        }

        private void TxtRundate_ValueChanged(object sender, EventArgs e)
        {

            try
            {
                DataSet dtrange = conn.getdata("SELECT * FROM Company where CompanyID='" + Master.companyId + "'");
                TxtRundate.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                TxtRundate.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                txtduedate.MinDate = Convert.ToDateTime(TxtRundate.Value);
                txtduedate.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            }
            catch
            {
            }
        }

        private void lvserial_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    lvserial.Items[lvserial.FocusedItem.Index].Remove();
                }
            }
            catch
            {
            }
        }

        private void lvserial_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {

                txtserial.Text = lvserial.Items[lvserial.FocusedItem.Index].SubItems[0].Text;
                lvserial.Items[lvserial.FocusedItem.Index].Remove();
                txtserial.Focus();
            }
            catch
            {
            }
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void panel13_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtpono_Enter(object sender, EventArgs e)
        {
            txtpono.BackColor = Color.LightYellow;
        }

        private void txtpono_Leave(object sender, EventArgs e)
        {
            txtpono.BackColor = Color.White;
        }

        private void TxtBillNo_Enter(object sender, EventArgs e)
        {
            TxtBillNo.BackColor = Color.LightYellow;
        }

        private void TxtBillNo_Leave(object sender, EventArgs e)
        {
            TxtBillNo.BackColor = Color.White;
        }
        public void bindallitem()
        {
            try
            {
                DataTable allitem = new DataTable();
                lvallitem.Items.Clear();
                allitem = conn.getdataset("select ProductMaster.Product_Name from ProductMaster where isactive=1 order by ProductMaster.Product_Name asc");
                for (int i = 0; i < allitem.Rows.Count; i++)
                {
                    ListViewItem li;
                    li = lvallitem.Items.Add(allitem.Rows[i]["Product_Name"].ToString());
                }
            }
            catch
            {
            }
        }
        private void txtitemname_Enter(object sender, EventArgs e)
        {
            try
            {
                txtitemname.BackColor = Color.LightYellow;
                pnlallitem.Visible = true;
                bindallitem();
            }
            catch
            {
            }
        }

        private void txtitemname_Leave(object sender, EventArgs e)
        {
            txtitemname.BackColor = Color.White;
            try
            {
                if (lvallitem.Items[0].Selected == true)
                {
                    pnlallitem.Visible = true;
                }
                else
                {
                    pnlallitem.Visible = false;
                }
            }
            catch
            {
            }
            //  pnlallitem.Visible = false;
        }

        private void txtpacking_Enter(object sender, EventArgs e)
        {
            txtpacking.BackColor = Color.LightYellow;
        }

        private void txtpacking_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtpacking.Text))
            {
                txtpacking.Text = "1";
            }
            txtpacking.BackColor = Color.White;
        }

        private void txtbags_Enter(object sender, EventArgs e)
        {
            txtbags.BackColor = Color.LightYellow;
        }

        private void txtbags_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbags.Text))
            {
                txtbags.Text = "1";
            }
            txtbags.BackColor = Color.White;
        }

        private void txtqty_Enter(object sender, EventArgs e)
        {
            txtqty.BackColor = Color.LightYellow;
        }

        private void txtqty_Leave(object sender, EventArgs e)
        {
            txtqty.BackColor = Color.White;
        }

        private void txtpqty_Enter(object sender, EventArgs e)
        {
            txtpqty.BackColor = Color.LightYellow;
        }

        private void txtpqty_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtpqty.Text))
            {
                txtpqty.Text = "1";
            }
            txtpqty.BackColor = Color.White;
        }

        private void txtfree_Enter(object sender, EventArgs e)
        {
            txtfree.BackColor = Color.LightYellow;
        }

        private void txtfree_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtfree.Text))
            {
                txtfree.Text = "0";
            }
            txtfree.BackColor = Color.White;
        }

        private void txtrate_Enter(object sender, EventArgs e)
        {
            txtrate.BackColor = Color.LightYellow;
        }

        private void txtrate_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtrate.Text))
            {
                txtrate.Text = "0";
            }
            txtrate.BackColor = Color.White;
        }

        private void txtper_Enter(object sender, EventArgs e)
        {
            txtper.BackColor = Color.LightYellow;
        }

        private void txtper_Leave(object sender, EventArgs e)
        {
            txtper.BackColor = Color.White;
        }

        private void txttotal_Enter(object sender, EventArgs e)
        {
            txttotal.BackColor = Color.LightYellow;
        }

        private void txttotal_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txttotal.Text))
            {
                txttotal.Text = "0";
            }
            txttotal.BackColor = Color.White;
        }

        private void txtdisper_Enter(object sender, EventArgs e)
        {
            txtdisper.BackColor = Color.LightYellow;
        }

        private void txtdisper_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtdisper.Text))
            {
                txtdisper.Text = "0";
            }
            txtdisper.BackColor = Color.White;
        }

        private void txtdisamt_Enter(object sender, EventArgs e)
        {
            txtdisamt.BackColor = Color.LightYellow;
        }

        private void txtdisamt_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtdisamt.Text))
            {
                txtdisamt.Text = "0";
            }
            txtdisamt.BackColor = Color.White;
        }

        private void txttax_Enter(object sender, EventArgs e)
        {
            txttax.BackColor = Color.LightYellow;
        }

        private void txttax_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txttax.Text))
            {
                txttax.Text = "0";
            }
            txttax.BackColor = Color.White;
        }

        private void txtaddtax_Enter(object sender, EventArgs e)
        {
            txtaddtax.BackColor = Color.LightYellow;
        }

        private void txtaddtax_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtaddtax.Text))
            {
                txtaddtax.Text = "0";
            }
            txtaddtax.BackColor = Color.White;
        }

        private void txtamount_Enter(object sender, EventArgs e)
        {
            txtamount.BackColor = Color.LightYellow;
        }

        private void txtamount_Leave(object sender, EventArgs e)
        {
            txtamount.BackColor = Color.White;
        }

        private void txtcharremark_Enter(object sender, EventArgs e)
        {
            txtcharremark.BackColor = Color.LightYellow;
        }

        private void txtcharremark_Leave(object sender, EventArgs e)
        {
            txtcharremark.BackColor = Color.White;
        }

        private void txtcharval_Enter(object sender, EventArgs e)
        {
            txtcharval.BackColor = Color.LightYellow;
        }

        private void txtcharval_Leave(object sender, EventArgs e)
        {
            txtcharval.BackColor = Color.White;
        }

        private void txtcharat_Enter(object sender, EventArgs e)
        {
            txtcharat.BackColor = Color.LightYellow;
        }

        private void txtcharat_Leave(object sender, EventArgs e)
        {
            txtcharat.BackColor = Color.White;
        }

        private void txtcharplusminus_Enter(object sender, EventArgs e)
        {
            txtcharplusminus.BackColor = Color.LightYellow;
        }

        private void txtcharplusminus_Leave(object sender, EventArgs e)
        {
            txtcharplusminus.BackColor = Color.White;
        }

        private void txtcharamt_Enter(object sender, EventArgs e)
        {
            txtcharamt.BackColor = Color.LightYellow;
        }

        private void txtcharamt_Leave(object sender, EventArgs e)
        {
            txtcharamt.BackColor = Color.White;
        }

        private void BtnPayment_Enter(object sender, EventArgs e)
        {
            BtnPayment.UseVisualStyleBackColor = false;
            BtnPayment.BackColor = Color.YellowGreen;
            BtnPayment.ForeColor = Color.White;
        }

        private void BtnPayment_Leave(object sender, EventArgs e)
        {
            BtnPayment.UseVisualStyleBackColor = true;
            BtnPayment.BackColor = Color.FromArgb(51, 153, 255);
            BtnPayment.ForeColor = Color.White;
        }

        private void BtnPayment_MouseEnter(object sender, EventArgs e)
        {
            BtnPayment.UseVisualStyleBackColor = false;
            BtnPayment.BackColor = Color.YellowGreen;
            BtnPayment.ForeColor = Color.White;
        }

        private void BtnPayment_MouseLeave(object sender, EventArgs e)
        {
            BtnPayment.UseVisualStyleBackColor = true;
            BtnPayment.BackColor = Color.FromArgb(51, 153, 255);
            BtnPayment.ForeColor = Color.White;
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

        private void btnCalculator_Enter(object sender, EventArgs e)
        {
            btnCalculator.UseVisualStyleBackColor = false;
            btnCalculator.BackColor = Color.FromArgb(176, 111, 193);
            btnCalculator.ForeColor = Color.White;
        }

        private void btnCalculator_Leave(object sender, EventArgs e)
        {
            btnCalculator.UseVisualStyleBackColor = true;
            btnCalculator.BackColor = Color.FromArgb(51, 153, 255);
            btnCalculator.ForeColor = Color.White;
        }

        private void btnCalculator_MouseEnter(object sender, EventArgs e)
        {
            btnCalculator.UseVisualStyleBackColor = false;
            btnCalculator.BackColor = Color.FromArgb(176, 111, 193);
            btnCalculator.ForeColor = Color.White;
        }

        private void btnCalculator_MouseLeave(object sender, EventArgs e)
        {
            btnCalculator.UseVisualStyleBackColor = true;
            btnCalculator.BackColor = Color.FromArgb(51, 153, 255);
            btnCalculator.ForeColor = Color.White;
        }

        private void btnclose_Enter(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = false;
            btnclose.BackColor = Color.FromArgb(248, 152, 94);
            btnclose.ForeColor = Color.White;
        }

        private void btnclose_Leave(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = true;
            btnclose.BackColor = Color.FromArgb(51, 153, 255);
            btnclose.ForeColor = Color.White;
        }

        private void btnclose_MouseEnter(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = false;
            btnclose.BackColor = Color.FromArgb(248, 152, 94);
            btnclose.ForeColor = Color.White;
        }

        private void btnclose_MouseLeave(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = true;
            btnclose.BackColor = Color.FromArgb(51, 153, 255);
            btnclose.ForeColor = Color.White;
        }
        string searchstr;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //empty the string for every 1 seconds
            // searchstr = "";
        }

        private void cmbcustname_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbcustname.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbcustname.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void cmbsaletype_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbsaletype.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbsaletype.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void cmbterms_KeyUp(object sender, KeyEventArgs e)
        {
            //searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //// If the Search string is greater than 1 then use custom logic
            //if (searchstr.Length > 1)
            //{
            //    int index;
            //    // Search the Item that matches the string typed
            //    index = cmbterms.FindString(searchstr);
            //    // Select the Item in the Combo
            //    cmbterms.SelectedIndex = index;
            //}
        }

        private void cmbbatch_KeyUp(object sender, KeyEventArgs e)
        {
            //searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //// If the Search string is greater than 1 then use custom logic
            //if (searchstr.Length > 1)
            //{
            //    int index;
            //    // Search the Item that matches the string typed
            //    index = cmbbatch.FindString(searchstr);
            //    // Select the Item in the Combo
            //    cmbbatch.SelectedIndex = index;
            //}
        }

        private void cmbcharper_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbcharper.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbcharper.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            var privouscontroal = txtitemname;
            activecontroal = privouscontroal.Name;
            Itementry client = new Itementry(this, master, tabControl, activecontroal);
            client.Passed(1);
            //client.Show();
            master.AddNewTab(client);
            // autoreaderbind();
            txtitemname.Focus();
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            if (txtitemname.Text != "")
            {
                // SqlDataAdapter da = new SqlDataAdapter();
                // DataTable dtitem = new DataTable();
                SqlCommand cmd = new SqlCommand("select productid from productmaster where Product_Name='" + txtitemname.Text + "' and isactive=1", con);
                SqlDataAdapter sda6 = new SqlDataAdapter(cmd);
                DataTable dtitem = new DataTable();
                sda6.Fill(dtitem);
                if (dtitem.Rows.Count > 0)
                {
                    var privouscontroal = txtitemname;
                    activecontroal = privouscontroal.Name;
                    Itementry client = new Itementry(this, master, tabControl, activecontroal);
                    client.Updatefromsale(txtitemname.Text);
                    client.Passed(1);
                    //client.Show();
                    master.AddNewTab(client);
                }
                else
                {
                    MessageBox.Show("Please Enter/Select Valid Item.");
                }
            }
            else
            {
                MessageBox.Show("Please Enter Item Name");
            }
        }

        private void btnAddPartyName_Click(object sender, EventArgs e)
        {
            var privouscontroal = cmbcustname;
            activecontroal = privouscontroal.Name;
            Accountentry client = new Accountentry(this, master, tabControl, activecontroal);
            client.Passed(1);
            master.AddNewTab(client);
        }

        private void btnEditPartyName_Click(object sender, EventArgs e)
        {
            if (cmbcustname.Text != "" && cmbcustname.Text != null)
            {
                var privouscontroal = cmbcustname;
                activecontroal = privouscontroal.Name;
                string iid = cmbcustname.SelectedValue.ToString();
                Accountentry client = new Accountentry(this, master, tabControl, activecontroal);
                client.Update(1, iid);
                client.Passed(1);
                //  client.Show();
                master.AddNewTab(client);
            }
            else
            {
                MessageBox.Show("Please Select PartyName.");
            }
        }

        private void btnAddSaleType_Click(object sender, EventArgs e)
        {
            if (strfinalarray[0] == "SO")
            {
                var privouscontroal = cmbsaletype;
                activecontroal = privouscontroal.Name;
                SaletypeEntry pt = new SaletypeEntry(this, master, tabControl, activecontroal);
                //pt.Show();
                master.AddNewTab(pt);
            }
            else if (strfinalarray[0] == "PO")
            {
                var privouscontroal = cmbsaletype;
                activecontroal = privouscontroal.Name;
                PurchaseTypeEntry p = new PurchaseTypeEntry(this, master, tabControl, activecontroal);
                master.AddNewTab(p);
            }
            else if (strfinalarray[0] == "SC")
            {
                var privouscontroal = cmbsaletype;
                activecontroal = privouscontroal.Name;
                SaletypeEntry pt = new SaletypeEntry(this, master, tabControl, activecontroal);
                //pt.Show();
                master.AddNewTab(pt);
            }
            else if (strfinalarray[0] == "PC")
            {
                var privouscontroal = cmbsaletype;
                activecontroal = privouscontroal.Name;
                PurchaseTypeEntry p = new PurchaseTypeEntry(this, master, tabControl, activecontroal);
                master.AddNewTab(p);
            }
        }

        private void btnEditSaleType_Click(object sender, EventArgs e)
        {
            if (cmbsaletype.Text != "" && cmbsaletype.Text != null)
            {

                if (strfinalarray[0] == "SO")
                {
                    var privouscontroal = cmbsaletype;
                    activecontroal = privouscontroal.Name;
                    SaletypeEntry pt = new SaletypeEntry(this, master, tabControl, activecontroal);
                    pt.updatemode("Sale", cmbsaletype.Text, "SO");
                    // pt.Show();
                    master.AddNewTab(pt);
                }
                else if (strfinalarray[0] == "SC")
                {
                    var privouscontroal = cmbsaletype;
                    activecontroal = privouscontroal.Name;
                    SaletypeEntry pt = new SaletypeEntry(this, master, tabControl, activecontroal);
                    pt.updatemode("Sale", cmbsaletype.Text, "SC");
                    // pt.Show();
                    master.AddNewTab(pt);
                }
                else if (strfinalarray[0] == "PO")
                {
                    var privouscontroal = cmbsaletype;
                    activecontroal = privouscontroal.Name;
                    PurchaseTypeEntry p = new PurchaseTypeEntry(this, master, tabControl, activecontroal);
                    p.updatemode("Purchase", cmbsaletype.Text, "PO");
                    // pt.Show();
                    master.AddNewTab(p);
                }
                else if (strfinalarray[0] == "PC")
                {
                    var privouscontroal = cmbsaletype;
                    activecontroal = privouscontroal.Name;
                    PurchaseTypeEntry p = new PurchaseTypeEntry(this, master, tabControl, activecontroal);
                    p.updatemode("Purchase", cmbsaletype.Text, "PC");
                    // pt.Show();
                    master.AddNewTab(p);
                }
            }
            else
            {
                MessageBox.Show("Please Select Type");
            }
        }

        private void btnAddPerticular_Click(object sender, EventArgs e)
        {
            var privouscontroal = cmbcharper;
            activecontroal = privouscontroal.Name;
            ChargesHead popup = new ChargesHead(this, cmbcharper.Text, master, tabControl, activecontroal);
            master.AddNewTab(popup);
        }

        private void btnEditPerticular_Click(object sender, EventArgs e)
        {
            if (cmbcharper.Text != "" && cmbcharper.Text != null)
            {
                var privouscontroal = cmbcharper;
                activecontroal = privouscontroal.Name;
                ChargesHead popup = new ChargesHead(this, cmbcharper.Text, master, tabControl, activecontroal);
                popup.Update(1, cmbcharper.Text);
                master.AddNewTab(popup);
            }
            else
            {
                MessageBox.Show("Please Select Perticular Charges");
            }
        }

        private void lvallitem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                String str = lvallitem.Items[lvallitem.FocusedItem.Index].SubItems[0].Text;
                txtitemname.Text = str;
                txtitemname.Focus();
                enteritem();
            }
        }

        private void lvallitem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            String str = lvallitem.Items[lvallitem.FocusedItem.Index].SubItems[0].Text;
            txtitemname.Text = str;
            txtitemname.Focus();
            enteritem();
        }

        private void txtitemname_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // lvallitem.Items[0].Selected = true;
                SqlCommand cmd = new SqlCommand("select Product_Name from productmaster where Product_Name like'%" + txtitemname.Text + "%' and isactive=1", con);
                SqlDataAdapter sda6 = new SqlDataAdapter(cmd);
                DataTable dtitem = new DataTable();
                sda6.Fill(dtitem);
                lvallitem.Items.Clear();
                for (int i = 0; i < dtitem.Rows.Count; i++)
                {
                    ListViewItem li;
                    li = lvallitem.Items.Add(dtitem.Rows[i]["Product_Name"].ToString());
                }
                //  lvallitem.Focus();
                if (txtitemname.Text == "" && txtitemname.Text == null)
                {
                    bindallitem();
                }
            }
            catch
            {
            }

        }

        private void cmbterms_Leave(object sender, EventArgs e)
        {
            cmbterms.Text = s;
        }

        private void cmbcustname_Leave(object sender, EventArgs e)
        {
            cmbcustname.Text = s;
        }

        private void cmbsaletype_Leave(object sender, EventArgs e)
        {
            cmbsaletype.Text = s;
        }

        private void cmbbatch_Leave(object sender, EventArgs e)
        {
            cmbbatch.Text = s;
        }

        private void cmbcharper_Leave(object sender, EventArgs e)
        {
            cmbcharper.Text = s;
        }

        internal void bindbatchfromform(string batchno)
        {
            this.ActiveControl = cmbbatch;
            cmbbatch.Text = batchno;
        }

        private void tabcharges_Enter(object sender, EventArgs e)
        {
            if (cmbcharper.Items.Count > 0)
            {
                try
                {
                    this.ActiveControl = cmbcharper;
                    cmbcharper.SelectedIndex = 0;
                    cmbcharper.DroppedDown = true;
                }
                catch (Exception excp)
                {

                }
            }
        }

        private void cmbterms_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbterms.Items.Count; i++)
                {
                    s = cmbterms.GetItemText(cmbterms.Items[i]);
                    if (s == cmbterms.Text)
                    {
                        inList = true;
                        cmbterms.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbterms.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void cmbcustname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbcustname.Items.Count; i++)
                {
                    s = cmbcustname.GetItemText(cmbcustname.Items[i]);
                    if (s == cmbcustname.Text)
                    {
                        inList = true;
                        cmbcustname.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbcustname.Text = "";
                }
                getduedate();
            }
            catch (Exception excp)
            {
            }
        }

        private void cmbsaletype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbsaletype.Items.Count; i++)
                {
                    s = cmbsaletype.GetItemText(cmbsaletype.Items[i]);
                    if (s == cmbsaletype.Text)
                    {
                        inList = true;
                        cmbsaletype.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbsaletype.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void btncharadditem_Enter(object sender, EventArgs e)
        {
            btncharadditem.UseVisualStyleBackColor = false;
            btncharadditem.BackColor = Color.FromArgb(9, 106, 3);
            btncharadditem.ForeColor = Color.White;
        }

        private void btncharadditem_Leave(object sender, EventArgs e)
        {
            btncharadditem.UseVisualStyleBackColor = true;
            btncharadditem.BackColor = Color.FromArgb(51, 153, 255);
            btncharadditem.ForeColor = Color.White;
        }

        private void btncharadditem_MouseEnter(object sender, EventArgs e)
        {
            btncharadditem.UseVisualStyleBackColor = false;
            btncharadditem.BackColor = Color.FromArgb(9, 106, 3);
            btncharadditem.ForeColor = Color.White;
        }

        private void btncharadditem_MouseLeave(object sender, EventArgs e)
        {
            btncharadditem.UseVisualStyleBackColor = true;
            btncharadditem.BackColor = Color.FromArgb(51, 153, 255);
            btncharadditem.ForeColor = Color.White;
        }

        private void cmbagentname_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbagentname.SelectedIndex = 0;
                cmbagentname.DroppedDown = true;
            }
            catch
            {
            }
        }

        private void cmbagentname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbagentname.Items.Count; i++)
                {
                    s = cmbagentname.GetItemText(cmbagentname.Items[i]);
                    if (s == cmbagentname.Text)
                    {
                        inList = true;
                        cmbagentname.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbagentname.Text = "";
                }
                pnlagent.Visible = false;
                TxtBillNo.Focus();
            }
            if (e.KeyCode == Keys.F3)
            {
                var privouscontroal = cmbagentname;
                activecontroal = privouscontroal.Name;
                Accountentry client = new Accountentry(this, master, tabControl, activecontroal);

                client.Passed(1);
                //   client.Show();
                master.AddNewTab(client);
            }
            if (e.KeyCode == Keys.F2)
            {
                var privouscontroal = cmbagentname;
                activecontroal = privouscontroal.Name;
                string iid = cmbagentname.SelectedValue.ToString();

                Accountentry client = new Accountentry(this, master, tabControl, activecontroal);
                client.Update(1, iid);
                client.Passed(1);
                //  client.Show();
                master.AddNewTab(client);
            }
        }

        private void cmbagentname_Leave(object sender, EventArgs e)
        {
            try
            {
                cmbagentname.Text = s;
            }
            catch
            {
            }
        }

        private void lvallitem_MouseClick(object sender, MouseEventArgs e)
        {
            String str = lvallitem.Items[lvallitem.FocusedItem.Index].SubItems[0].Text;
            txtitemname.Text = str;
            txtitemname.Focus();
            enteritem();
        }

        private void txtphone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtmobile.Focus();
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



        private void txtadharno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pnlcusdetails.Visible = false;
                txtitemname.Focus();
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

        private void txtcustomername_KeyDown(object sender, KeyEventArgs e)
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
                txtcity.Focus();
            }
        }

        private void txtcity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtphone.Focus();
            }
        }

        private void txtmobile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtpanno.Focus();
            }
        }

        private void txtpanno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtadharno.Focus();
            }
        }


    }

}
