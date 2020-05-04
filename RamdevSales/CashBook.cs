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
    public partial class CashBook : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        DataTable mouseclickid = new DataTable();
        OleDbSettings ods = new OleDbSettings();
        Printing prn = new Printing();
        public CashBook()
        {
            InitializeComponent();
        }

        public CashBook(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }

        private void CashBook_Load(object sender, EventArgs e)
        {

            DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
            DTPFrom.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            DTPFrom.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            DTPTo.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            DTPTo.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            DTPFrom.Value = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            con.Open();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            LVledger.Columns.Add("Date", 100, HorizontalAlignment.Center);
            LVledger.Columns.Add("Type", 100, HorizontalAlignment.Left);
            LVledger.Columns.Add("Name", 270, HorizontalAlignment.Left);
            LVledger.Columns.Add("Receipt", 100, HorizontalAlignment.Left);
            LVledger.Columns.Add("Payment", 100, HorizontalAlignment.Right);
            LVledger.Columns.Add("Balance", 140, HorizontalAlignment.Right);
            LVledger.Columns.Add("Short Narration", 150, HorizontalAlignment.Left);

            //  listviewbind();
            bindcustomer();
            con.Close();
            mouseclickid.Columns.Add("type", typeof(string));
            mouseclickid.Columns.Add("id", typeof(string));
            DTPFrom.CustomFormat = Master.dateformate;
            DTPTo.CustomFormat = Master.dateformate;
            this.ActiveControl = cmbaccname;
            //set the interval  and start the timer
            //timer1.Interval = 1000;
            //timer1.Start();
        }
        public void bindcustomer()
        {
            SqlCommand cmd1 = new SqlCommand("select ClientID,AccountName from ClientMaster where isactive=1 and groupname like '%Cash-in-hand%' order by AccountName", con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            cmbaccname.ValueMember = "ClientID";
            cmbaccname.DisplayMember = "AccountName";
            cmbaccname.DataSource = dt1;
            cmbaccname.SelectedIndex = -1;


            // autobind(dt1, cmbaccname);
        }
        private void autobind(DataTable dt1, ComboBox cmbcustname)
        {
            string[] arr = new string[dt1.Rows.Count];
            //  string list="";
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                arr[i] = dt1.Rows[i][1].ToString();
            }

            //    var stringArr = dt.Rows[0].ItemArray.Select(x => x.ToString()).ToArray();

            cmbcustname.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cmbcustname.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbcustname.AutoCompleteCustomSource.AddRange(arr);
        }
        public event EventHandler<EventArgs> Canceled;
        private Master master;
        private TabControl tabControl;
        ListViewItem li;
        decimal debit1 = 0;
        decimal credit1 = 0;
        public void binddata()
        {
            try
            {
                #region
                string totaldebit = conn.ExecuteScalar("select sum(amount) from Ledger where Date1 < '" + DTPFrom.Text + "' and OT1='" + cmbaccname.Text + "' and dc='D' and isactive=1");
                string totalcredit = conn.ExecuteScalar("select sum(amount) from Ledger where Date1 < '" + DTPFrom.Text + "' and OT1='" + cmbaccname.Text + "' and dc='C' and isactive=1");
                if (totaldebit == "" || totaldebit == "NULL")
                {
                    totaldebit = "0.00";
                }
                if (totalcredit == "" || totalcredit == "NULL")
                {
                    totalcredit = "0.00";
                }
                Double opbal;
                string DC = "";
                DataTable opbalance = conn.getdataset("select * from ClientMaster where isactive=1 and ClientID=" + cmbaccname.SelectedValue);
                string stropbal = opbalance.Rows[0]["opbal"].ToString();
                string strDC = opbalance.Rows[0]["Dr_cr"].ToString();
                if (strDC == "Dr.")
                {
                    double ttdebit = Convert.ToDouble(totaldebit) + Convert.ToDouble(stropbal);
                    totaldebit = ttdebit.ToString();
                }
                else if (strDC == "Cr.")
                {
                    double ttcredit = Convert.ToDouble(totalcredit) + Convert.ToDouble(stropbal);
                    totalcredit = ttcredit.ToString();
                }

                if (Convert.ToDouble(totaldebit) >= Convert.ToDouble(totalcredit))
                {
                    opbal = Convert.ToDouble(totaldebit) - Convert.ToDouble(totalcredit);
                    txtopbal.Text = opbal.ToString("N2") + " Dr.";
                    DC = "D";
                }
                else
                {
                    opbal = Convert.ToDouble(totalcredit) - Convert.ToDouble(totaldebit);
                    txtopbal.Text = opbal.ToString("N2") + " Cr.";
                    DC = "C";
                }
                #endregion
                LVledger.Items.Clear();
                string balance = "0.00";
                Double debit = 0, credit = 0;

                DataTable pos = conn.getdataset("select billid as voucherid, billdate as date1,'POS' as Trantype,'0' as accountid,customername as accountname,totalnet as amount,'D' as DC,'' as ShortNarration, Terms as OT1,'' as OT2,'' as OT3,'' as OT4,'' as OT5,'' as OT6,'' as OT7,'' as OT8,saletypeid as OT9 from BillPOSMaster where isactive=1 and Terms='Cash' and BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by BillDate asc");
                pos = changedtclone(pos);

                DataTable SPdt = conn.getdataset("select voucherid,date1,Trantype,accountid,accountname,amount,DC,ShortNarration,OT1,OT2,OT3,OT4,OT5,OT6,OT7,OT8,OT9 from Ledger where isactive=1 and Date1 between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and OT1='" + cmbaccname.Text + "' order by Date1");
                SPdt = changedtclone(SPdt);
                SPdt.Merge(pos);
                SPdt.DefaultView.Sort = "[date1] DESC";
                SPdt = SPdt.DefaultView.ToTable();
                debit = 0;
                credit = 0;
                debit1 = 0;
                credit1 = 0;
                for (int i = 0; i < SPdt.Rows.Count; i++)
                {

                    if (SPdt.Rows[i]["TranType"].ToString() == "Sale")
                    {
                        // DataTable client = conn.getdataset("select * from ClientMaster where isactive=1 and ClientID='" + SPdt.Rows[i]["AccountID"].ToString() + "'");
                        // DataTable saletype = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypeid='" + SPdt.Rows[i]["SaleType"].ToString() + "'");
                        //  DataTable accgrpname = conn.getdataset("select * from AccountGroup where id='" + saletype.Rows[0]["Groupid"].ToString() + "'");
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Sale");
                        li.SubItems.Add("Sales");
                        //Double totalbasic = Convert.ToDouble(dt.Rows[i]["totalbasic"].ToString()) - Convert.ToDouble(dt.Rows[i]["totaldiscount"].ToString()) - Convert.ToDouble(dt.Rows[i]["totaladddiscount"].ToString());
                        li.SubItems.Add(Convert.ToString(SPdt.Rows[i]["Amount"].ToString()));
                        li.SubItems.Add("0");
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        // opbal = Convert.ToDouble(balance) + opbal;
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        //foreach (ListViewItem lstItem in LVledger.Items)
                        //{
                        //    debit1 += decimal.Parse(lstItem.SubItems[3].Text);
                        //    credit1 += decimal.Parse(lstItem.SubItems[4].Text);
                        //}
                        //decimal balance1 =  debit1 + credit1;
                        li.SubItems.Add(balance);
                        li.SubItems.Add("Bill No:" + SPdt.Rows[i]["VoucherID"].ToString() + "");
                    }
                    if (SPdt.Rows[i]["TranType"].ToString().ToUpper() == "POS")
                    {
                        // DataTable client = conn.getdataset("select * from ClientMaster where isactive=1 and ClientID='" + SPdt.Rows[i]["AccountID"].ToString() + "'");
                        // DataTable saletype = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypeid='" + SPdt.Rows[i]["SaleType"].ToString() + "'");
                        //  DataTable accgrpname = conn.getdataset("select * from AccountGroup where id='" + saletype.Rows[0]["Groupid"].ToString() + "'");
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("POS");
                        li.SubItems.Add("Sales");
                        //Double totalbasic = Convert.ToDouble(dt.Rows[i]["totalbasic"].ToString()) - Convert.ToDouble(dt.Rows[i]["totaldiscount"].ToString()) - Convert.ToDouble(dt.Rows[i]["totaladddiscount"].ToString());
                        li.SubItems.Add(Convert.ToString(SPdt.Rows[i]["Amount"].ToString()));
                        li.SubItems.Add("0");
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        // opbal = Convert.ToDouble(balance) + opbal;
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        //foreach (ListViewItem lstItem in LVledger.Items)
                        //{
                        //    debit1 += decimal.Parse(lstItem.SubItems[3].Text);
                        //    credit1 += decimal.Parse(lstItem.SubItems[4].Text);
                        //}
                        //decimal balance1 =  debit1 + credit1;
                        li.SubItems.Add(balance);
                        li.SubItems.Add("Bill No:" + SPdt.Rows[i]["VoucherID"].ToString() + "");
                    }
                    if (SPdt.Rows[i]["TranType"].ToString() == "Purchase")
                    {
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Purchase");
                        li.SubItems.Add("Purchases");
                        li.SubItems.Add("0");
                        //  Double totalbasic = Convert.ToDouble(dt1.Rows[i]["totalnet"].ToString()) - Convert.ToDouble(dt1.Rows[i]["totaldiscount"].ToString()) - Convert.ToDouble(dt1.Rows[i]["totaladddiscount"].ToString());
                        li.SubItems.Add(Convert.ToString(SPdt.Rows[i]["Amount"].ToString()));
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        //opbal = Convert.ToDouble(balance) + opbal;
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add("Bill No:" + SPdt.Rows[i]["VoucherID"].ToString() + "");
                    }
                    if (SPdt.Rows[i]["TranType"].ToString() == "DEBIT NOTE")
                    {
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add(SPdt.Rows[i]["TranType"].ToString());
                        li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                        li.SubItems.Add(SPdt.Rows[i]["Amount"].ToString());
                        li.SubItems.Add("0");
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        // opbal = Convert.ToDouble(balance) + opbal;
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
                    }
                    if (SPdt.Rows[i]["TranType"].ToString() == "CREDIT NOTE")
                    {
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add(SPdt.Rows[i]["TranType"].ToString());
                        li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                        li.SubItems.Add("0");
                        li.SubItems.Add(SPdt.Rows[i]["Amount"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        // opbal = Convert.ToDouble(balance) + opbal;
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
                    }
                    if (SPdt.Rows[i]["TranType"].ToString() == "Pmnt")
                    {
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Quick Payment");
                        li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                        li.SubItems.Add(SPdt.Rows[i]["Amount"].ToString());
                        li.SubItems.Add("0");
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        // opbal = Convert.ToDouble(balance) + opbal;
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
                    }
                    if (SPdt.Rows[i]["TranType"].ToString() == "Rect")
                    {
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Quick Receipt");
                        li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                        li.SubItems.Add("0");
                        li.SubItems.Add(SPdt.Rows[i]["Amount"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        // opbal = Convert.ToDouble(balance) + opbal;
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
                    }
                    if (SPdt.Rows[i]["TranType"].ToString() == "SaleReturn")
                    {
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Sale Returns");
                        li.SubItems.Add("Sales");
                        //Double totalbasic = Convert.ToDouble(dt.Rows[i]["totalbasic"].ToString()) - Convert.ToDouble(dt.Rows[i]["totaldiscount"].ToString()) - Convert.ToDouble(dt.Rows[i]["totaladddiscount"].ToString());
                        li.SubItems.Add("0");
                        li.SubItems.Add(Convert.ToString(SPdt.Rows[i]["Amount"].ToString()));

                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        // opbal = Convert.ToDouble(balance) + opbal;
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        //foreach (ListViewItem lstItem in LVledger.Items)
                        //{
                        //    debit1 += decimal.Parse(lstItem.SubItems[3].Text);
                        //    credit1 += decimal.Parse(lstItem.SubItems[4].Text);
                        //}
                        //decimal balance1 =  debit1 + credit1;
                        li.SubItems.Add(balance);
                        li.SubItems.Add("Bill No:" + SPdt.Rows[i]["VoucherID"].ToString() + "");
                    }
                    if (SPdt.Rows[i]["TranType"].ToString() == "PurchaseReturn")
                    {
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Purchase Return");
                        li.SubItems.Add("Purchases");
                        li.SubItems.Add(Convert.ToString(SPdt.Rows[i]["Amount"].ToString()));
                        li.SubItems.Add("0");
                        //  Double totalbasic = Convert.ToDouble(dt1.Rows[i]["totalnet"].ToString()) - Convert.ToDouble(dt1.Rows[i]["totaldiscount"].ToString()) - Convert.ToDouble(dt1.Rows[i]["totaladddiscount"].ToString());

                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        //opbal = Convert.ToDouble(balance) + opbal;
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add("Bill No:" + SPdt.Rows[i]["VoucherID"].ToString() + "");
                    }
                    if (SPdt.Rows[i]["OT7"].ToString() == "Bank Entry")
                    {
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add(SPdt.Rows[i]["TranType"].ToString());
                        li.SubItems.Add(SPdt.Rows[i]["OT8"].ToString());
                        li.SubItems.Add("0");
                        li.SubItems.Add(SPdt.Rows[i]["Amount"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        // opbal = Convert.ToDouble(balance) + opbal;
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
                    }


                }
                //pos
                #region

                //if (pos.Rows.Count > 0)
                //{
                //    for (int i = 0; i < pos.Rows.Count; i++)
                //    {
                //        DataTable saletype = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypeid='" + pos.Rows[i]["OT9"].ToString() + "'");
                //        DataTable accgrpname = conn.getdataset("select * from AccountGroup where id='" + saletype.Rows[0]["Groupid"].ToString() + "'");

                //        li = LVledger.Items.Add(Convert.ToDateTime(pos.Rows[i]["date1"].ToString()).ToString(Master.dateformate));
                //        li.SubItems.Add("POS");
                //        li.SubItems.Add(accgrpname.Rows[0]["groupname"].ToString());
                //        li.SubItems.Add(Convert.ToString(pos.Rows[i]["amount"].ToString()));
                //        li.SubItems.Add("0");
                //        string[] str = balance.Split(' ');
                //        char temp = str[1][0];
                //        DC = temp.ToString();
                //        opbal = Convert.ToDouble(str[0]);
                //        balance = getbalance(opbal, Convert.ToDouble(pos.Rows[i]["amount"].ToString()), DC, "D", i);
                //        li.SubItems.Add(balance);
                //        li.SubItems.Add("Bill No:" + pos.Rows[i]["vourcherid"].ToString() + "");
                //    }
                //}

                #endregion
                foreach (ListViewItem lstItem in LVledger.Items)
                {
                    debit1 += decimal.Parse(lstItem.SubItems[3].Text);
                    credit1 += decimal.Parse(lstItem.SubItems[4].Text);
                    //string bal1 = lstItem.SubItems[5].Text;
                    //String withoutLast1 = bal1.Substring(0, (bal1.Length - 3));
                    //decimal d = Convert.ToDecimal(withoutLast1);
                    //balancetotal += d;
                }
                txttotalreceive.Text = Convert.ToString(debit1.ToString("N2"));
                txttotalpayment.Text = Convert.ToString(credit1.ToString("N2"));
                txtbalance.Text = Convert.ToString(balance);
                #region

                //      this.LVledger.ListViewItemSorter = new ListViewItemComparer(1);
                ////Sale
                //#region
                //DataTable dt = new DataTable();
                //dt = conn.getdataset("select * from BillMaster where Terms='Cash' and isactive=1 and BillType='S' and Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Bill_Date asc");
                //if (dt.Rows.Count > 0)
                //{
                //    debit = 0;
                //    credit = 0;
                //    balancetotal = 0;
                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {
                //        debit1 = 0;
                //        credit1 = 0;
                //        DataTable client = conn.getdataset("select * from ClientMaster where isactive=1 and ClientID='" + dt.Rows[i]["ClientID"].ToString() + "'");
                //        DataTable saletype = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypeid='" + dt.Rows[i]["SaleType"].ToString() + "'");
                //        DataTable accgrpname = conn.getdataset("select * from AccountGroup where id='" + saletype.Rows[0]["Groupid"].ToString() + "'");
                //        li = LVledger.Items.Add(Convert.ToDateTime(dt.Rows[i]["Bill_Date"].ToString()).ToString(Master.dateformate));
                //        li.SubItems.Add("Sale");
                //        li.SubItems.Add(accgrpname.Rows[0]["groupname"].ToString());
                //        //Double totalbasic = Convert.ToDouble(dt.Rows[i]["totalbasic"].ToString()) - Convert.ToDouble(dt.Rows[i]["totaldiscount"].ToString()) - Convert.ToDouble(dt.Rows[i]["totaladddiscount"].ToString());
                //        li.SubItems.Add(Convert.ToString(dt.Rows[i]["totalnet"].ToString()));
                //        li.SubItems.Add("0");
                //        if (i != 0)
                //        {
                //            string[] str = balance.Split(' ');
                //            char temp = str[1][0];
                //            DC = temp.ToString();
                //            opbal = Convert.ToDouble(str[0]);
                //        }
                //       // opbal = Convert.ToDouble(balance) + opbal;
                //        balance = getbalance(opbal, Convert.ToDouble(dt.Rows[i]["totalnet"].ToString()), DC,"D", i);
                //        //foreach (ListViewItem lstItem in LVledger.Items)
                //        //{
                //        //    debit1 += decimal.Parse(lstItem.SubItems[3].Text);
                //        //    credit1 += decimal.Parse(lstItem.SubItems[4].Text);
                //        //}
                //        //decimal balance1 =  debit1 + credit1;
                //        li.SubItems.Add(balance);
                //        li.SubItems.Add("Bill No:"+dt.Rows[i]["billno"].ToString()+"");


                //    }


                //}
                //#endregion
                ////Purchase
                //#region
                //DataTable dt1 = new DataTable();
                //dt1 = conn.getdataset("select * from BillMaster where Terms='Cash' and isactive=1 and BillType='P' and Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Bill_Date asc");
                //if (dt1.Rows.Count > 0)
                //{
                //    debit = 0;
                //    credit = 0;
                //    balancetotal = 0;
                //    for (int i = 0; i < dt1.Rows.Count; i++)
                //    {
                //        debit1 = 0;
                //        credit1 = 0;
                //        DataTable client = conn.getdataset("select * from ClientMaster where isactive=1 and ClientID='" + dt1.Rows[i]["ClientID"].ToString() + "'");
                //        DataTable saletype = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypeid='" + dt1.Rows[i]["SaleType"].ToString() + "'");
                //        DataTable accgrpname = conn.getdataset("select * from AccountGroup where id='" + saletype.Rows[0]["Groupid"].ToString() + "'");
                //        li = LVledger.Items.Add(Convert.ToDateTime(dt1.Rows[i]["Bill_Date"].ToString()).ToString(Master.dateformate));
                //        li.SubItems.Add("Purchase");
                //        li.SubItems.Add(accgrpname.Rows[0]["groupname"].ToString());
                //        li.SubItems.Add("0");
                //        //  Double totalbasic = Convert.ToDouble(dt1.Rows[i]["totalnet"].ToString()) - Convert.ToDouble(dt1.Rows[i]["totaldiscount"].ToString()) - Convert.ToDouble(dt1.Rows[i]["totaladddiscount"].ToString());
                //        li.SubItems.Add(Convert.ToString(dt1.Rows[i]["totalnet"].ToString()));
                //        //if (i != 0)
                //       // {
                //            string[] str = balance.Split(' ');
                //            char temp = str[1][0];
                //            DC = temp.ToString();
                //            opbal = Convert.ToDouble(str[0]);
                //       // }
                //       // opbal = Convert.ToDouble(balance) + opbal;
                //        balance = getbalance(opbal, Convert.ToDouble(dt1.Rows[i]["totalnet"].ToString()), DC, "C", i);
                //        li.SubItems.Add(balance);
                //        li.SubItems.Add("Bill No:" + dt1.Rows[i]["billno"].ToString() + "");


                //    }


                //}
                //#endregion
                ////BankEntery
                //#region
                //DataTable dt2 = new DataTable();
                //dt2 = conn.getdataset("select * from Voucher where isactive=1 and TransactionType='Bank Entry' and PartyName='Cash' and Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Date asc");
                //if (dt2.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dt2.Rows.Count; i++)
                //    {
                //        li = LVledger.Items.Add(Convert.ToDateTime(dt2.Rows[i]["Date"].ToString()).ToString(Master.dateformate));
                //        li.SubItems.Add(dt2.Rows[i]["PaymentTerms"].ToString());
                //        li.SubItems.Add(dt2.Rows[i]["AccountName"].ToString());
                //        li.SubItems.Add("0");
                //        li.SubItems.Add(dt2.Rows[i]["TotalAmount"].ToString());
                //        string[] str = balance.Split(' ');
                //        char temp = str[1][0];
                //        DC = temp.ToString();
                //        opbal = Convert.ToDouble(str[0]);
                //        // }
                //        // opbal = Convert.ToDouble(balance) + opbal;
                //        balance = getbalance(opbal, Convert.ToDouble(dt2.Rows[i]["TotalAmount"].ToString()), DC, "C", i);
                //        li.SubItems.Add(balance);
                //        li.SubItems.Add(dt2.Rows[i]["OT1"].ToString());
                //    }
                //}
                //#endregion

                // //DebitNote
                //#region
                //DataTable dt3 = new DataTable();
                //dt3 = conn.getdataset("select * from Ledger where isactive=1 and TranType='DEBIT NOTE' and AccountName='Cash' and Date1>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Date1<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Date1 asc");
                //if (dt3.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dt3.Rows.Count; i++)
                //    {
                //        li = LVledger.Items.Add(Convert.ToDateTime(dt3.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                //        li.SubItems.Add(dt3.Rows[i]["TranType"].ToString());
                //        li.SubItems.Add(dt3.Rows[i]["OT1"].ToString());
                //        li.SubItems.Add("0");
                //        li.SubItems.Add(dt3.Rows[i]["Amount"].ToString());
                //        string[] str = balance.Split(' ');
                //        char temp = str[1][0];
                //        DC = temp.ToString();
                //        opbal = Convert.ToDouble(str[0]);
                //        // }
                //        // opbal = Convert.ToDouble(balance) + opbal;
                //        balance = getbalance(opbal, Convert.ToDouble(dt3.Rows[i]["Amount"].ToString()), DC, dt3.Rows[i]["dc"].ToString(), i);
                //        li.SubItems.Add(balance);
                //        li.SubItems.Add(dt3.Rows[i]["ShortNarration"].ToString());
                //    }
                //}
                //#endregion
                ////CREDITNOTE
                //#region
                //DataTable dt4 = new DataTable();
                //dt4 = conn.getdataset("select * from Ledger where isactive=1 and TranType='CREDIT NOTE' and AccountName='Cash' and Date1>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Date1<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Date1 asc");
                //if (dt4.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dt4.Rows.Count; i++)
                //    {
                //        li = LVledger.Items.Add(Convert.ToDateTime(dt4.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                //        li.SubItems.Add(dt4.Rows[i]["TranType"].ToString());
                //        li.SubItems.Add(dt4.Rows[i]["OT1"].ToString());
                //        li.SubItems.Add("0");
                //        li.SubItems.Add(dt4.Rows[i]["Amount"].ToString());
                //        string[] str = balance.Split(' ');
                //        char temp = str[1][0];
                //        DC = temp.ToString();
                //        opbal = Convert.ToDouble(str[0]);
                //        // }
                //        // opbal = Convert.ToDouble(balance) + opbal;
                //        balance = getbalance(opbal, Convert.ToDouble(dt4.Rows[i]["Amount"].ToString()), DC, dt4.Rows[i]["dc"].ToString(), i);
                //        li.SubItems.Add(balance);
                //        li.SubItems.Add(dt4.Rows[i]["ShortNarration"].ToString());
                //    }
                //}
                //#endregion
                ////Quick Payment
                //#region
                //DataTable dt5 = new DataTable();
                //dt5 = conn.getdataset("select p.*,l.AccountName from paymentreceipt p inner join Ledger l on p.recno=l.VoucherID where p.isactive=1 and p.type='P' and l.isactive=1 and l.TranType='Pmnt' and p.mode='Cash' and l.Date1>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and l.Date1<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by l.Date1 asc");
                //if (dt5.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dt5.Rows.Count; i++)
                //    {
                //        li = LVledger.Items.Add(Convert.ToDateTime(dt5.Rows[i]["Date"].ToString()).ToString(Master.dateformate));
                //        li.SubItems.Add("Quick Payment");
                //        li.SubItems.Add(dt5.Rows[i]["AccountName"].ToString());
                //        li.SubItems.Add("0");
                //        li.SubItems.Add(dt5.Rows[i]["netamt"].ToString());
                //        string[] str = balance.Split(' ');
                //        char temp = str[1][0];
                //        DC = temp.ToString();
                //        opbal = Convert.ToDouble(str[0]);
                //        // }
                //        // opbal = Convert.ToDouble(balance) + opbal;
                //        balance = getbalance(opbal, Convert.ToDouble(dt5.Rows[i]["netamt"].ToString()), DC,"C", i);
                //        li.SubItems.Add(balance);
                //        li.SubItems.Add(dt5.Rows[i]["remarks"].ToString());
                //    }
                //}
                //#endregion
                ////Quick Receipt
                //#region
                //DataTable dt6 = new DataTable();
                //dt6 = conn.getdataset("select p.*,l.AccountName from paymentreceipt p inner join Ledger l on p.recno=l.VoucherID where p.isactive=1 and p.type='R' and l.isactive=1 and l.TranType='Rect' and p.mode='Cash' and l.Date1>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and l.Date1<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by l.Date1 asc");
                //if (dt6.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dt6.Rows.Count; i++)
                //    {
                //        li = LVledger.Items.Add(Convert.ToDateTime(dt6.Rows[i]["Date"].ToString()).ToString(Master.dateformate));
                //        li.SubItems.Add("Quick Receipt");
                //        li.SubItems.Add(dt6.Rows[i]["AccountName"].ToString());
                //        li.SubItems.Add(dt6.Rows[i]["netamt"].ToString());
                //        li.SubItems.Add("0");
                //        string[] str = balance.Split(' ');
                //        char temp = str[1][0];
                //        DC = temp.ToString();
                //        opbal = Convert.ToDouble(str[0]);
                //        // }
                //        // opbal = Convert.ToDouble(balance) + opbal;
                //        balance = getbalance(opbal, Convert.ToDouble(dt6.Rows[i]["netamt"].ToString()), DC, "C", i);
                //        li.SubItems.Add(balance);
                //        li.SubItems.Add(dt6.Rows[i]["remarks"].ToString());
                //    }
                //}
                //#endregion
                //  //POS
                //#region
                //DataTable dt7 = new DataTable();
                //dt7 = conn.getdataset("select * from BillPOSMaster where isactive=1 and Terms='Cash' and BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by BillDate asc");
                //if (dt7.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dt7.Rows.Count; i++)
                //    {
                //        DataTable saletype = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypeid='" + dt7.Rows[i]["SaleTypeid"].ToString() + "'");
                //        DataTable accgrpname = conn.getdataset("select * from AccountGroup where id='" + saletype.Rows[0]["Groupid"].ToString() + "'");

                //        li = LVledger.Items.Add(Convert.ToDateTime(dt7.Rows[i]["BillDate"].ToString()).ToString(Master.dateformate));
                //        li.SubItems.Add("Sale");
                //        li.SubItems.Add(accgrpname.Rows[0]["groupname"].ToString());
                //        li.SubItems.Add(Convert.ToString(dt7.Rows[i]["totalnet"].ToString()));
                //        li.SubItems.Add("0");
                //        string[] str = balance.Split(' ');
                //        char temp = str[1][0];
                //        DC = temp.ToString();
                //        opbal = Convert.ToDouble(str[0]);
                //        balance = getbalance(opbal, Convert.ToDouble(dt7.Rows[i]["totalnet"].ToString()), DC, "D", i);
                //        li.SubItems.Add(balance);
                //        li.SubItems.Add("Bill No:" + dt7.Rows[i]["billno"].ToString() + "");
                //    }
                //}
                //#endregion
                ////Sale Return
                //#region
                //DataTable dt8 = new DataTable();
                //dt8 = conn.getdataset("select * from BillMaster where Terms='Cash' and isactive=1 and BillType='SR' and Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Bill_Date asc");
                //if (dt8.Rows.Count > 0)
                //{
                //    debit = 0;
                //    credit = 0;
                //    balancetotal = 0;
                //    for (int i = 0; i < dt8.Rows.Count; i++)
                //    {
                //        debit1 = 0;
                //        credit1 = 0;
                //        DataTable client = conn.getdataset("select * from ClientMaster where isactive=1 and ClientID='" + dt8.Rows[i]["ClientID"].ToString() + "'");
                //        DataTable saletype = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypeid='" + dt8.Rows[i]["SaleType"].ToString() + "'");
                //        DataTable accgrpname = conn.getdataset("select * from AccountGroup where id='" + saletype.Rows[0]["Groupid"].ToString() + "'");
                //        li = LVledger.Items.Add(Convert.ToDateTime(dt8.Rows[i]["Bill_Date"].ToString()).ToString(Master.dateformate));
                //        li.SubItems.Add("Sale Return");
                //        li.SubItems.Add(accgrpname.Rows[0]["groupname"].ToString());
                //        //Double totalbasic = Convert.ToDouble(dt.Rows[i]["totalbasic"].ToString()) - Convert.ToDouble(dt.Rows[i]["totaldiscount"].ToString()) - Convert.ToDouble(dt.Rows[i]["totaladddiscount"].ToString());
                //        li.SubItems.Add("0");
                //        li.SubItems.Add(Convert.ToString(dt8.Rows[i]["totalnet"].ToString()));
                //        //if (i != 0)
                //        //{
                //            string[] str = balance.Split(' ');
                //            char temp = str[1][0];
                //            DC = temp.ToString();
                //            opbal = Convert.ToDouble(str[0]);
                //       // }
                //        // opbal = Convert.ToDouble(balance) + opbal;
                //        balance = getbalance(opbal, Convert.ToDouble(dt8.Rows[i]["totalnet"].ToString()), DC, "C", i);
                //        //foreach (ListViewItem lstItem in LVledger.Items)
                //        //{
                //        //    debit1 += decimal.Parse(lstItem.SubItems[3].Text);
                //        //    credit1 += decimal.Parse(lstItem.SubItems[4].Text);
                //        //}
                //        //decimal balance1 =  debit1 + credit1;
                //        li.SubItems.Add(balance);
                //        li.SubItems.Add("Bill No:" + dt8.Rows[i]["billno"].ToString() + "");


                //    }


                //}
                //#endregion
                ////Purchase Return
                //#region
                //DataTable dt9 = new DataTable();
                //dt9 = conn.getdataset("select * from BillMaster where Terms='Cash' and isactive=1 and BillType='PR' and Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Bill_Date asc");
                //if (dt9.Rows.Count > 0)
                //{
                //    debit = 0;
                //    credit = 0;
                //    balancetotal = 0;
                //    for (int i = 0; i < dt9.Rows.Count; i++)
                //    {
                //        debit1 = 0;
                //        credit1 = 0;
                //        DataTable client = conn.getdataset("select * from ClientMaster where isactive=1 and ClientID='" + dt9.Rows[i]["ClientID"].ToString() + "'");
                //        DataTable saletype = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypeid='" + dt9.Rows[i]["SaleType"].ToString() + "'");
                //        DataTable accgrpname = conn.getdataset("select * from AccountGroup where id='" + saletype.Rows[0]["Groupid"].ToString() + "'");
                //        li = LVledger.Items.Add(Convert.ToDateTime(dt9.Rows[i]["Bill_Date"].ToString()).ToString(Master.dateformate));
                //        li.SubItems.Add("Purchase Return");
                //        li.SubItems.Add(accgrpname.Rows[0]["groupname"].ToString());
                //        li.SubItems.Add(Convert.ToString(dt9.Rows[i]["totalnet"].ToString()));
                //        li.SubItems.Add("0");
                //        //  Double totalbasic = Convert.ToDouble(dt1.Rows[i]["totalnet"].ToString()) - Convert.ToDouble(dt1.Rows[i]["totaldiscount"].ToString()) - Convert.ToDouble(dt1.Rows[i]["totaladddiscount"].ToString());
                //        //if (i != 0)
                //        // {
                //        string[] str = balance.Split(' ');
                //        char temp = str[1][0];
                //        DC = temp.ToString();
                //        opbal = Convert.ToDouble(str[0]);
                //        // }
                //        // opbal = Convert.ToDouble(balance) + opbal;
                //        balance = getbalance(opbal, Convert.ToDouble(dt9.Rows[i]["totalnet"].ToString()), DC, "D", i);
                //        li.SubItems.Add(balance);
                //        li.SubItems.Add("Bill No:" + dt9.Rows[i]["billno"].ToString() + "");


                //    }


                //}
                //#endregion
                //foreach (ListViewItem lstItem in LVledger.Items)
                //{
                //    debit += decimal.Parse(lstItem.SubItems[3].Text);
                //    credit += decimal.Parse(lstItem.SubItems[4].Text);
                //    //string bal1 = lstItem.SubItems[5].Text;
                //    //String withoutLast1 = bal1.Substring(0, (bal1.Length - 3));
                //    //decimal d = Convert.ToDecimal(withoutLast1);
                //    //balancetotal += d;
                //}
                //txttotdebit.Text = Convert.ToString(debit);
                //txttotcredit.Text = Convert.ToString(credit);
                //txtbalance.Text = Convert.ToString(balance);
                #endregion
            }
            catch
            {
            }
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
        private string getbalance(double opbal, double p, String DC, String actualdc, int i)
        {
            double balance;
            string bal = "";
            if (DC == "D")
            {
                if (actualdc == "D")
                {
                    balance = opbal + p;
                    bal = balance.ToString("N2") + " Dr.";
                }

                else
                {
                    if (opbal > p)
                    {
                        balance = opbal - p;
                        bal = balance.ToString("N2") + " Dr.";
                    }
                    else
                    {
                        balance = p - opbal;
                        bal = balance.ToString("N2") + " Cr.";
                    }
                }
            }
            if (DC == "C")
            {
                if (actualdc == "C")
                {
                    balance = opbal + p;
                    bal = balance.ToString("N2") + " Cr.";
                }
                else
                {
                    if (opbal > p)
                    {
                        balance = opbal - p;
                        bal = balance.ToString("N2") + " Cr.";
                    }
                    else
                    {
                        balance = p - opbal;
                        bal = balance.ToString("N2") + " Dr.";
                    }
                }
            }

            return bal;
        }
        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            binddata();
            #region
            //if (cmbaccname.Text != "")
            //{
            //    EventHandler<EventArgs> ea = Canceled;
            //    if (ea != null)
            //        ea(this, e);


            //    if ((cmbaccname.Text).ToUpper() == "CASH" || cmbaccname.SelectedValue == "25")
            //    {
            //        #region
            //        string totaldebit = conn.ExecuteScalar("select sum(amount) from Ledger where Date1 < '" + DTPFrom.Text + "' and OT1='" + cmbaccname.Text + "' and dc='D' and isactive=1 order by Date1");
            //        string totalcredit = conn.ExecuteScalar("select sum(amount) from Ledger where Date1 < '" + DTPFrom.Text + "' and OT1='" + cmbaccname.Text + "' and dc='C' and isactive=1 order by Date1");
            //        if (totaldebit == "" || totaldebit == "NULL")
            //        {
            //            totaldebit = "0.00";
            //        }
            //        if (totalcredit == "" || totalcredit == "NULL")
            //        {
            //            totalcredit = "0.00";
            //        }
            //        Double opbal;
            //        string DC = "";
            //        if (Convert.ToDouble(totaldebit) >= Convert.ToDouble(totalcredit))
            //        {
            //            opbal = Convert.ToDouble(totaldebit) - Convert.ToDouble(totalcredit);
            //            txtopbal.Text = opbal.ToString("N2") + " Dr.";
            //            DC = "D";
            //        }
            //        else
            //        {
            //            opbal = Convert.ToDouble(totalcredit) - Convert.ToDouble(totaldebit);
            //            txtopbal.Text = opbal.ToString("N2") + " Cr.";
            //            DC = "C";
            //        }

            //        //for (int i = 0; i < OPdt.Rows.Count; i++)
            //        //{
            //        //    Double opbal = 0;
            //        //    if (OPdt.Rows[i]["DC"].ToString() == "D")
            //        //    {

            //        //    }
            //        //    else if (OPdt.Rows[i]["TranType"].ToString() == "Rect")
            //        //    {
            //        //        ListViewItem li;
            //        //        li = LVledger.Items.Add(Convert.ToDateTime(OPdt.Rows[i]["Date1"].ToString()).ToString("dd-MMM-yyyy"));
            //        //        li.SubItems.Add("Rect");
            //        //        li.SubItems.Add(OPdt.Rows[i]["OT1"].ToString());
            //        //        li.SubItems.Add("By Rcpt. No.: " + OPdt.Rows[i]["VoucherID"].ToString() + "; " + OPdt.Rows[i]["ShortNarration"].ToString());
            //        //        li.SubItems.Add("");
            //        //        li.SubItems.Add(Math.Round(Convert.ToDouble(OPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //        //        li.SubItems.Add("Rect");
            //        //    }
            //        //}
            //        #endregion

            //        //for create ledger
            //        mouseclickid.Rows.Clear();
            //        LVledger.Items.Clear();
            //        #region
            //        DataTable SPdt = conn.getdataset("select * from Ledger where isactive=1 and Date1 between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and OT1='" + cmbaccname.Text + "' order by Date1");
            //        string balance = "0.00";
            //        Double debit = 0, credit = 0;
            //        for (int i = 0; i < SPdt.Rows.Count; i++)
            //        {
            //            if (SPdt.Rows[i]["TranType"].ToString() == "Sale")
            //            {
            //                ListViewItem li;
            //                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //                li.SubItems.Add("Sale");
            //                li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
            //                li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                li.SubItems.Add("");
            //                mouseclickid.Rows.Add("Sale", SPdt.Rows[i]["VoucherID"].ToString());
            //                if (i != 0)
            //                {
            //                    string[] str = balance.Split(' ');
            //                    char temp = str[1][0];
            //                    DC = temp.ToString();

            //                    opbal = Convert.ToDouble(str[0]);
            //                }

            //                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //                li.SubItems.Add(balance);
            //            }
            //            else if (SPdt.Rows[i]["TranType"].ToString() == "Rect")
            //            {
            //                ListViewItem li;
            //                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //                li.SubItems.Add("Rect");
            //                li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
            //                li.SubItems.Add("By Rcpt. No.: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["ShortNarration"].ToString());
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                li.SubItems.Add("");
            //                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //                mouseclickid.Rows.Add("Rect", SPdt.Rows[i]["VoucherID"].ToString());
            //                if (i != 0)
            //                {
            //                    string[] str = balance.Split(' ');
            //                    char temp = str[1][0];
            //                    DC = temp.ToString();
            //                    opbal = Convert.ToDouble(str[0]);
            //                }
            //                string CD = SPdt.Rows[i]["dc"].ToString();
            //                if (CD == "C")
            //                {
            //                    CD = "D";
            //                }
            //                else
            //                {
            //                    CD = "C";
            //                }
            //                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);

            //                li.SubItems.Add(balance);
            //            }
            //            else if (SPdt.Rows[i]["TranType"].ToString() == "Sale Return")
            //            {
            //                ListViewItem li;
            //                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //                li.SubItems.Add("Sale Return");
            //                li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
            //                li.SubItems.Add("Return Bill No: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["ShortNarration"].ToString());
            //                li.SubItems.Add("");
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //                mouseclickid.Rows.Add("Sale Return", SPdt.Rows[i]["VoucherID"].ToString());
            //                if (i != 0)
            //                {
            //                    string[] str = balance.Split(' ');
            //                    char temp = str[1][0];
            //                    DC = temp.ToString();

            //                    opbal = Convert.ToDouble(str[0]);
            //                }
            //                if (DC == "C")
            //                {
            //                    DC = "D";
            //                }
            //                else
            //                {
            //                    DC = "C";
            //                }
            //                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //                li.SubItems.Add(balance);
            //            }
            //            else if (SPdt.Rows[i]["TranType"].ToString() == "Purchase")
            //            {
            //                ListViewItem li;
            //                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //                li.SubItems.Add("Purchase");
            //                li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
            //                li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //                li.SubItems.Add("");
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //                mouseclickid.Rows.Add("Purchase", SPdt.Rows[i]["VoucherID"].ToString());
            //                if (i != 0)
            //                {
            //                    string[] str = balance.Split(' ');
            //                    char temp = str[1][0];
            //                    DC = temp.ToString();

            //                    opbal = Convert.ToDouble(str[0]);
            //                }

            //                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //                li.SubItems.Add(balance);
            //            }
            //            else if (SPdt.Rows[i]["TranType"].ToString() == "Pmnt")
            //            {
            //                ListViewItem li;
            //                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //                li.SubItems.Add("Pmnt");
            //                li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
            //                li.SubItems.Add("By Rcpt. No.: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["ShortNarration"].ToString());
            //                li.SubItems.Add("");
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));

            //                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //                mouseclickid.Rows.Add("Pmnt", SPdt.Rows[i]["VoucherID"].ToString());
            //                if (i != 0)
            //                {
            //                    string[] str = balance.Split(' ');
            //                    char temp = str[1][0];
            //                    DC = temp.ToString();

            //                    opbal = Convert.ToDouble(str[0]);
            //                }
            //                string CD = SPdt.Rows[i]["dc"].ToString();
            //                if (CD == "C")
            //                {
            //                    CD = "D";
            //                }
            //                else
            //                {
            //                    CD = "C";
            //                }
            //                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
            //                li.SubItems.Add(balance);
            //            }
            //        }
            //        #endregion

            //        txttotdebit.Text = debit.ToString("N2");
            //        txttotcredit.Text = credit.ToString("N2");
            //        txtbalance.Text = balance;
            //    }
            //    else
            //    {
            //        //for calculate OpBalance
            //        #region
            //        string totaldebit = conn.ExecuteScalar("select sum(amount) from Ledger where Date1 < '" + DTPFrom.Text + "' and Accountid='" + cmbaccname.SelectedValue + "' and dc='D' and isactive=1 order by Date1");
            //        string totalcredit = conn.ExecuteScalar("select sum(amount) from Ledger where Date1 < '" + DTPFrom.Text + "' and Accountid='" + cmbaccname.SelectedValue + "' and dc='C' and isactive=1 order by Date1");
            //        if (totaldebit == "" || totaldebit == "NULL")
            //        {
            //            totaldebit = "0.00";
            //        }
            //        if (totalcredit == "" || totalcredit == "NULL")
            //        {
            //            totalcredit = "0.00";
            //        }
            //        Double opbal;
            //        string DC = "";
            //        if (Convert.ToDouble(totaldebit) >= Convert.ToDouble(totalcredit))
            //        {
            //            opbal = Convert.ToDouble(totaldebit) - Convert.ToDouble(totalcredit);
            //            txtopbal.Text = opbal.ToString("N2") + " Dr.";
            //            DC = "D";
            //        }
            //        else
            //        {
            //            opbal = Convert.ToDouble(totalcredit) - Convert.ToDouble(totaldebit);
            //            txtopbal.Text = opbal.ToString("N2") + " Cr.";
            //            DC = "C";
            //        }

            //        //for (int i = 0; i < OPdt.Rows.Count; i++)
            //        //{
            //        //    Double opbal = 0;
            //        //    if (OPdt.Rows[i]["DC"].ToString() == "D")
            //        //    {

            //        //    }
            //        //    else if (OPdt.Rows[i]["TranType"].ToString() == "Rect")
            //        //    {
            //        //        ListViewItem li;
            //        //        li = LVledger.Items.Add(Convert.ToDateTime(OPdt.Rows[i]["Date1"].ToString()).ToString("dd-MMM-yyyy"));
            //        //        li.SubItems.Add("Rect");
            //        //        li.SubItems.Add(OPdt.Rows[i]["OT1"].ToString());
            //        //        li.SubItems.Add("By Rcpt. No.: " + OPdt.Rows[i]["VoucherID"].ToString() + "; " + OPdt.Rows[i]["ShortNarration"].ToString());
            //        //        li.SubItems.Add("");
            //        //        li.SubItems.Add(Math.Round(Convert.ToDouble(OPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //        //        li.SubItems.Add("Rect");
            //        //    }
            //        //}
            //        #endregion

            //        //for create ledger
            //        mouseclickid.Rows.Clear();
            //        LVledger.Items.Clear();
            //        #region
            //        DataTable SPdt = conn.getdataset("select * from Ledger where isactive=1 and Date1 between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and Accountid='" + cmbaccname.SelectedValue + "' order by Date1");
            //        string balance = "0.00";
            //        Double debit = 0, credit = 0;
            //        for (int i = 0; i < SPdt.Rows.Count; i++)
            //        {
            //            if (SPdt.Rows[i]["TranType"].ToString() == "Sale")
            //            {
            //                ListViewItem li;
            //                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //                li.SubItems.Add("Sale");
            //                li.SubItems.Add("Sales");
            //                li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //                li.SubItems.Add("");
            //                mouseclickid.Rows.Add("Sale", SPdt.Rows[i]["VoucherID"].ToString());
            //                if (i != 0)
            //                {
            //                    string[] str = balance.Split(' ');
            //                    char temp = str[1][0];
            //                    DC = temp.ToString();
            //                    opbal = Convert.ToDouble(str[0]);
            //                }
            //                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //                li.SubItems.Add(balance);
            //            }
            //            else if (SPdt.Rows[i]["TranType"].ToString() == "Rect")
            //            {
            //                ListViewItem li;
            //                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //                li.SubItems.Add("Rect");
            //                li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
            //                li.SubItems.Add("By Rcpt. No.: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["ShortNarration"].ToString());
            //                li.SubItems.Add("");
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //                mouseclickid.Rows.Add("Rect", SPdt.Rows[i]["VoucherID"].ToString());
            //                if (i != 0)
            //                {
            //                    string[] str = balance.Split(' ');
            //                    char temp = str[1][0];
            //                    DC = temp.ToString();
            //                    opbal = Convert.ToDouble(str[0]);
            //                }
            //                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //                li.SubItems.Add(balance);
            //            }
            //            else if (SPdt.Rows[i]["TranType"].ToString() == "Sale Return")
            //            {
            //                ListViewItem li;
            //                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //                li.SubItems.Add("Sale Return");
            //                li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
            //                li.SubItems.Add("Return Bill No: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["ShortNarration"].ToString());
            //                li.SubItems.Add("");
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //                mouseclickid.Rows.Add("Sale Return", SPdt.Rows[i]["VoucherID"].ToString());
            //                if (i != 0)
            //                {
            //                    string[] str = balance.Split(' ');
            //                    char temp = str[1][0];
            //                    DC = temp.ToString();
            //                    opbal = Convert.ToDouble(str[0]);
            //                }
            //                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //                li.SubItems.Add(balance);
            //            }
            //            else if (SPdt.Rows[i]["TranType"].ToString() == "Purchase")
            //            {
            //                ListViewItem li;
            //                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //                li.SubItems.Add("Purchase");
            //                li.SubItems.Add("Purchases");
            //                li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //                li.SubItems.Add("");
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //                mouseclickid.Rows.Add("Purchase", SPdt.Rows[i]["VoucherID"].ToString());
            //                if (i != 0)
            //                {
            //                    string[] str = balance.Split(' ');
            //                    char temp = str[1][0];
            //                    DC = temp.ToString();
            //                    opbal = Convert.ToDouble(str[0]);
            //                }
            //                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //                li.SubItems.Add(balance);
            //            }
            //            else if (SPdt.Rows[i]["TranType"].ToString() == "Pmnt")
            //            {
            //                ListViewItem li;
            //                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //                li.SubItems.Add("Pmnt");
            //                li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
            //                li.SubItems.Add("By Rcpt. No.: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["ShortNarration"].ToString());
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //                li.SubItems.Add("");
            //                mouseclickid.Rows.Add("Pmnt", SPdt.Rows[i]["VoucherID"].ToString());
            //                if (i != 0)
            //                {
            //                    string[] str = balance.Split(' ');
            //                    char temp = str[1][0];
            //                    DC = temp.ToString();
            //                    opbal = Convert.ToDouble(str[0]);
            //                }
            //                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //                li.SubItems.Add(balance);
            //            }
            //        }
            //        #endregion

            //        txttotdebit.Text = debit.ToString("N2");
            //        txttotcredit.Text = credit.ToString("N2");
            //        txtbalance.Text = balance;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Please Select Account Name");
            //    cmbaccname.Focus();
            //}
            #endregion
        }
        public static string s;
        private void cmbaccname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbaccname.Items.Count; i++)
                {
                    s = cmbaccname.GetItemText(cmbaccname.Items[i]);
                    if (s == cmbaccname.Text)
                    {
                        inList = true;
                        cmbaccname.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbaccname.Text = "";
                }

                DTPFrom.Focus();
            }
        }

        private void DTPFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DTPTo.Focus();
            }
        }

        private void DTPTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnViewReport.Focus();
            }
        }
        public void open()
        {
            //if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Sale")
            //{
            //    String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
            //    DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='S' and setdefault=1");
            //    DefaultSale bd = new DefaultSale(this);
            //    Sale p = new Sale(this);
            //    if (dt1.Rows[0]["formname"].ToString() == bd.Text)
            //    {
            //        // bd.updatemode(str, mouseclickid.Rows[LVledger.FocusedItem.Index][1].ToString(), 1);
            //        bd.MdiParent = this.MdiParent;
            //        bd.StartPosition = FormStartPosition.CenterScreen;
            //        bd.Show();
            //    }
            //    else if (dt1.Rows[0]["formname"].ToString() == p.Text)
            //    {
            //        p.updatemode(str, mouseclickid.Rows[LVledger.FocusedItem.Index][1].ToString(), 1);
            //        p.MdiParent = this.MdiParent;
            //        p.StartPosition = FormStartPosition.CenterScreen;
            //        p.Show();
            //    }




            //    bd.Show();
            //}
            //else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Purchase")
            //{
            //    String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
            //    DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='P' and setdefault=1");
            //  //  DefaultPurchase frm = new DefaultPurchase(this);

            //  //  Purchase p = new Purchase(this);
            //    if (dt1.Rows[0]["formname"].ToString() == frm.Text)
            //    {
            //        frm.updatemode(str, mouseclickid.Rows[LVledger.FocusedItem.Index][1].ToString(), 1);
            //        frm.MdiParent = this.MdiParent;
            //        frm.StartPosition = FormStartPosition.CenterScreen;
            //        frm.Show();
            //    }
            //    else if (dt1.Rows[0]["formname"].ToString() == p.Text)
            //    {
            //        p.updatemode(str, mouseclickid.Rows[LVledger.FocusedItem.Index][1].ToString(), 1);
            //        p.MdiParent = this.MdiParent;
            //        p.StartPosition = FormStartPosition.CenterScreen;
            //        p.Show();
            //    }

            //    //    DefaultPurchase bd = new DefaultPurchase(this);

            //    //  bd.Show();
            //}
            //else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Rect")
            //{
            //    String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[2].Text;

            //    QReceipt bd = new QReceipt(this);
            //    bd.updatemode(str, mouseclickid.Rows[LVledger.FocusedItem.Index][1].ToString(), 1, LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text);
            //    bd.Show();
            //}
            //else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Pmnt")
            //{
            //    String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[2].Text;

            //    QPayment bd = new QPayment(this);
            //    bd.updatemode(str, mouseclickid.Rows[LVledger.FocusedItem.Index][1].ToString(), 1, LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text);
            //    bd.Show();
            //}
        }
        private void LVledger_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //open();
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
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


        private void txttotdebit_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtopbal_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Label5_Click(object sender, EventArgs e)
        {

        }

        private void Label6_Click(object sender, EventArgs e)
        {

        }

        private void txttotcredit_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtbalance_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label7_Click(object sender, EventArgs e)
        {

        }

        private void BtnViewReport_MouseEnter(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = false;
            BtnViewReport.BackColor = Color.FromArgb(94, 191, 174);
            BtnViewReport.ForeColor = Color.White;
        }

        private void BtnViewReport_MouseLeave(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = true;
            BtnViewReport.BackColor = Color.FromArgb(51, 153, 255);
            BtnViewReport.ForeColor = Color.White;
        }

        private void btngenrpt_MouseEnter(object sender, EventArgs e)
        {
            btngenrpt.UseVisualStyleBackColor = false;
            btngenrpt.BackColor = Color.FromArgb(176, 111, 193);
            btngenrpt.ForeColor = Color.White;
        }

        private void btngenrpt_MouseMove(object sender, MouseEventArgs e)
        {
            //btngenrpt.UseVisualStyleBackColor = true;
            //btngenrpt.BackColor = Color.FromArgb(51, 153, 255);
            //btngenrpt.ForeColor = Color.White;
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

        private void btngenrpt_MouseLeave(object sender, EventArgs e)
        {
            btngenrpt.UseVisualStyleBackColor = true;
            btngenrpt.BackColor = Color.FromArgb(51, 153, 255);
            btngenrpt.ForeColor = Color.White;
        }

        private void BtnViewReport_Enter(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = false;
            BtnViewReport.BackColor = Color.FromArgb(94, 191, 174);
            BtnViewReport.ForeColor = Color.White;
        }

        private void BtnViewReport_Leave(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = true;
            BtnViewReport.BackColor = Color.FromArgb(51, 153, 255);
            BtnViewReport.ForeColor = Color.White;
        }

        private void btngenrpt_Enter(object sender, EventArgs e)
        {
            btngenrpt.UseVisualStyleBackColor = false;
            btngenrpt.BackColor = Color.FromArgb(176, 111, 193);
            btngenrpt.ForeColor = Color.White;
        }

        private void btngenrpt_Leave(object sender, EventArgs e)
        {
            btngenrpt.UseVisualStyleBackColor = true;
            btngenrpt.BackColor = Color.FromArgb(51, 153, 255);
            btngenrpt.ForeColor = Color.White;
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

        private void cmbaccname_Enter(object sender, EventArgs e)
        {
            if (cmbaccname.Items.Count > 0)
            {
                //  cmbaccname.SelectedIndex = 0;
                // cmbaccname.DroppedDown = true;
            }
        }

        private void btngenrpt_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Print CashBook?", "CashBook", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    prn.execute("delete from printing");
                    string status;
                    status = "Cash Book From" + DTPFrom.Text;
                    for (int i = 0; i < LVledger.Items.Count; i++)
                    {
                        DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1 and CompanyID='" + Master.companyId + "' ");
                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23)VALUES";
                        qry += "('" + dt1.Rows[0]["CompanyName"].ToString() + "','" + dt1.Rows[0]["SubName"].ToString() + "','" + dt1.Rows[0]["Address"].ToString() + "','" + dt1.Rows[0]["Address2"].ToString() + "','" + dt1.Rows[0]["City"].ToString() + "','" + dt1.Rows[0]["State"].ToString() + "','" + dt1.Rows[0]["Country"].ToString() + "','" + dt1.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Mobile"].ToString() + "','" + dt1.Rows[0]["Email"].ToString() + "','" + dt1.Rows[0]["CSTNo"].ToString() + "','" + status + "','" + LVledger.Items[i].SubItems[0].Text + "','" + LVledger.Items[i].SubItems[1].Text + "','" + LVledger.Items[i].SubItems[2].Text + "','" + LVledger.Items[i].SubItems[3].Text + "','" + LVledger.Items[i].SubItems[4].Text + "','" + LVledger.Items[i].SubItems[5].Text + "','" + LVledger.Items[i].SubItems[6].Text + "','" + txtopbal.Text + "','" + txttotalreceive.Text + "','" + txttotalpayment.Text + "','" + txtbalance.Text + "')";
                        prn.execute(qry);
                    }
                }
            }
            catch
            {
            }
            #region
            //try
            //{

            //    DialogResult dr1 = MessageBox.Show("Do you want to Print CashBook?", "CashBook", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (dr1 == DialogResult.Yes)
            //    {
            //        if (LVledger.Items.Count > 0)
            //        {
            //            prn.execute("delete from printing");
            //          //  DataTable client = conn.getdataset("select * from clientmaster where isactive=1 and clientID='" + cmbaccname.SelectedValue + "'");
            //            DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1");

            //            //         string date = "", type = "", Account = "", drAmount = "", crAmount="",balance="";
            //            for (int i = 0; i < LVledger.Items.Count; i++)
            //            {
            //                string type = "", AccountName = "", Receipt = "", Payment = "", balance = "",AccountBetweenDate="";
            //                //date = LVledger.Items[i].SubItems[1].Text;
            //                type = LVledger.Items[i].SubItems[1].Text;
            //                AccountName = LVledger.Items[i].SubItems[2].Text;
            //                Receipt = LVledger.Items[i].SubItems[4].Text;
            //                Payment = LVledger.Items[i].SubItems[5].Text;
            //                balance = LVledger.Items[i].SubItems[6].Text;
            //                AccountBetweenDate = "Account of " + cmbaccname.Text + " From "+ DTPFrom.Text+ " To "+DTPTo.Text;

            //                string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24)VALUES";
            //                qry += "('" + type + "','" + AccountName + "','" + Receipt + "','" + Payment + "','" + balance + "','" + AccountBetweenDate + "','" + dt1.Rows[0][0].ToString() + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][2].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + dt1.Rows[0][11].ToString() + "','" + dt1.Rows[0][12].ToString() + "','" + dt1.Rows[0][13].ToString() + "','" + txtopbal.Text + "','" + txttotdebit.Text + "','" + txttotcredit.Text + "','" + txtbalance.Text + "')";//,'" + DTPFrom.Text + "','" + DTPTo.Text + "','" + client.Rows[0][0].ToString() + "','" + client.Rows[0][1].ToString() + "','" + client.Rows[0][2].ToString() + "','" + client.Rows[0][3].ToString() + "','" + client.Rows[0][4].ToString() + "','" + client.Rows[0][5].ToString() + "','" + client.Rows[0][6].ToString() + "','" + client.Rows[0][7].ToString() + "','" + client.Rows[0][8].ToString() + "','" + client.Rows[0][9].ToString() + "','" + client.Rows[0][10].ToString() + "','" + client.Rows[0][11].ToString() + "','" + client.Rows[0][12].ToString() + "','" + client.Rows[0][13].ToString() + "')";
            //                prn.execute(qry);

            //            }
            //            /*       for (int i = 0; i < LVledger.Items.Count; i++)
            //                   {
            //                       string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34,T35,T36,T37,T38,T39,T40)VALUES";
            //                       qry += "('" + date + "','" + type + "','" + Account + "','" + drAmount + "','" + crAmount + "','" + balance + "','" + txttotdebit.Text + "','" + txttotcredit.Text + "','" + txtbalance.Text + "','" + txtopbal.Text + "','" + dt1.Rows[0][0].ToString() + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][2].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + dt1.Rows[0][11].ToString() + "','" + dt1.Rows[0][12].ToString() + "','" + dt1.Rows[0][13].ToString() + "','"+ DTPFrom.Text +"','"+ DTPTo.Text+"','"+ client.Rows[0][0].ToString() + "','" + client.Rows[0][1].ToString() + "','" + client.Rows[0][2].ToString() + "','" + client.Rows[0][3].ToString() + "','" + client.Rows[0][4].ToString() + "','" + client.Rows[0][5].ToString() + "','" + client.Rows[0][6].ToString() + "','" + client.Rows[0][7].ToString() + "','" + client.Rows[0][8].ToString() + "','" + client.Rows[0][9].ToString() + "','" + client.Rows[0][10].ToString() + "','" + client.Rows[0][11].ToString() + "','" + client.Rows[0][12].ToString() + "','" + client.Rows[0][13].ToString()+"')";
            //                       prn.execute(qry);

            //                   } */
            //            string reportName = "CashBook";
            //            Print popup = new Print(reportName);
            //            popup.ShowDialog();
            //            popup.Dispose();

            //        }
            //        else
            //        {
            //            MessageBox.Show("No Records For Print", "Ledger", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //    }



            //}
            //catch
            //{
            //}
            #endregion
        }
        string searchstr;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //empty the string for every 1 seconds
            //   searchstr = "";
        }

        private void cmbaccname_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbaccname.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbaccname.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void cmbaccname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbaccname.Items.Count; i++)
                {
                    s = cmbaccname.GetItemText(cmbaccname.Items[i]);
                    if (s == cmbaccname.Text)
                    {
                        inList = true;
                        cmbaccname.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbaccname.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void LVledger_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // open();
            }
        }

        private void cmbaccname_Leave(object sender, EventArgs e)
        {
            try
            {
                cmbaccname.Text = s;
            }
            catch
            {
            }
        }




    }
}

