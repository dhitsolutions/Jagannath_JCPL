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
    public partial class DailyStatus : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        OleDbSettings ods = new OleDbSettings();
        Printing prn = new Printing();
        int time;
        int cnt = 0;
        public DailyStatus()
        {
            InitializeComponent();
        }

        public DailyStatus(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }
        public void loadpage()
        {
            //DataSet dtrange = ods.getdata("SELECT SQLSetting.* FROM SQLSetting where OT6='" + Master.companyId + "'");
            //DTPFrom.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0][8].ToString());
            //DTPFrom.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0][9].ToString());
            //DTPTo.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0][8].ToString());
            //DTPTo.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0][9].ToString());
            //DTPFrom.Value = Convert.ToDateTime(dtrange.Tables[0].Rows[0][8].ToString());
            DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
            DTPFrom.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            DTPFrom.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            //DTPTo.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            //DTPTo.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            DTPFrom.Value = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            LVledger.Columns.Add("Date", 100, HorizontalAlignment.Center);
            LVledger.Columns.Add("Transaction Type", 200, HorizontalAlignment.Left);
            LVledger.Columns.Add("Debit(Credit)", 100, HorizontalAlignment.Center);
            LVledger.Columns.Add("Debit(Cash)", 100, HorizontalAlignment.Center);
            LVledger.Columns.Add("Credit(Credit)", 120, HorizontalAlignment.Right);
            LVledger.Columns.Add("Credit(Cash)", 120, HorizontalAlignment.Right);
            //LVledger.Columns.Add("Discount Amount", 120, HorizontalAlignment.Right);
            //LVledger.Columns.Add("Total Amount", 120, HorizontalAlignment.Right);
            LVledger.Columns.Add("Balance(Credit)", 120, HorizontalAlignment.Right);
            LVledger.Columns.Add("Balance(Cash)", 120, HorizontalAlignment.Right);
            

            //  listviewbind();

          
            //mouseclickid.Columns.Add("type", typeof(string));
            //mouseclickid.Columns.Add("id", typeof(string));
            DTPFrom.CustomFormat = Master.dateformate;
            //DTPTo.CustomFormat = Master.dateformate;
            this.ActiveControl = DTPFrom;
            //set the interval  and start the timer
            // timer1.Interval = 1000;
            // timer1.Start();

        }
        DataTable userrights = new DataTable();
        private void Ledger_Load(object sender, EventArgs e)
        {
            userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
            if (cnt == 0)
            {

                loadpage();
            }
        }
        DataTable mouseclickid = new DataTable();
       
        private void autobind(DataTable dt1, ComboBox cmbcustname)
        {
            string[] arr = new string[dt1.Rows.Count];
            //  string list="";
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                arr[i] = dt1.Rows[i][1].ToString();
            }

            //    var stringArr = dt.Rows[0].ItemArray.Select(x => x.ToString()).ToArray();

            //cmbcustname.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //cmbcustname.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //cmbcustname.AutoCompleteCustomSource.AddRange(arr);
        }
        public event EventHandler<EventArgs> Canceled;
        private Master master;
        private TabControl tabControl;

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
        public void databind()
        {
            //if ((cmbaccname.Text).ToUpper() == "CASH" || cmbaccname.SelectedValue == "101")
            //{

            //    #region
            //    string totaldebit = conn.ExecuteScalar("select sum(amount) from Ledger where Date1 < '" + DTPFrom.Text + "' and OT1='" + cmbaccname.Text + "' and dc='D' and isactive=1");
            //    string totalcredit = conn.ExecuteScalar("select sum(amount) from Ledger where Date1 < '" + DTPFrom.Text + "' and OT1='" + cmbaccname.Text + "' and dc='C' and isactive=1");
            //    if (totaldebit == "" || totaldebit == "NULL")
            //    {
            //        totaldebit = "0.00";
            //    }
            //    if (totalcredit == "" || totalcredit == "NULL")
            //    {
            //        totalcredit = "0.00";
            //    }
            //    Double opbal;
            //    string DC = "";
            //    DataTable opbalance = conn.getdataset("select * from ClientMaster where isactive=1 and ClientID=" + cmbaccname.SelectedValue);
            //    string stropbal = opbalance.Rows[0]["opbal"].ToString();
            //    string strDC = opbalance.Rows[0]["Dr_cr"].ToString();
            //    if (strDC == "Dr.")
            //    {
            //        double ttdebit = Convert.ToDouble(totaldebit) + Convert.ToDouble(stropbal);
            //        totaldebit = ttdebit.ToString();
            //    }
            //    else if (strDC == "Cr.")
            //    {
            //        double ttcredit = Convert.ToDouble(totalcredit) + Convert.ToDouble(stropbal);
            //        totalcredit = ttcredit.ToString();
            //    }

            //    if (Convert.ToDouble(totaldebit) >= Convert.ToDouble(totalcredit))
            //    {
            //        opbal = Convert.ToDouble(totaldebit) - Convert.ToDouble(totalcredit);
            //        txtopbal.Text = opbal.ToString("N2") + " Dr.";
            //        DC = "D";
            //    }
            //    else
            //    {
            //        opbal = Convert.ToDouble(totalcredit) - Convert.ToDouble(totaldebit);
            //        txtopbal.Text = opbal.ToString("N2") + " Cr.";
            //        DC = "C";
            //    }

            //    //for (int i = 0; i < OPdt.Rows.Count; i++)
            //    //{
            //    //    Double opbal = 0;
            //    //    if (OPdt.Rows[i]["DC"].ToString() == "D")
            //    //    {

            //    //    }
            //    //    else if (OPdt.Rows[i]["TranType"].ToString() == "Rect")
            //    //    {
            //    //        ListViewItem li;
            //    //        li = LVledger.Items.Add(Convert.ToDateTime(OPdt.Rows[i]["Date1"].ToString()).ToString("dd-MMM-yyyy"));
            //    //        li.SubItems.Add("Rect");
            //    //        li.SubItems.Add(OPdt.Rows[i]["OT1"].ToString());
            //    //        li.SubItems.Add("By Rcpt. No.: " + OPdt.Rows[i]["VoucherID"].ToString() + "; " + OPdt.Rows[i]["ShortNarration"].ToString());
            //    //        li.SubItems.Add("");
            //    //        li.SubItems.Add(Math.Round(Convert.ToDouble(OPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //    //        li.SubItems.Add("Rect");
            //    //    }
            //    //}
            //    #endregion

            //    //for create ledger
            //    mouseclickid.Rows.Clear();
            //    LVledger.Items.Clear();
            //    #region
            //    DataTable SPdt = conn.getdataset("select * from Ledger where isactive=1 and Date1 between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and OT1='" + cmbaccname.Text + "' order by Date1");
            //    string balance = "0.00";
            //    balance = Convert.ToString(opbal);
            //    Double debit = 0, credit = 0;

            //    for (int i = 0; i < SPdt.Rows.Count; i++)
            //    {
            //        if (SPdt.Rows[i]["TranType"].ToString() == "EXPCash Recept")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("EXPCash Recept");
            //            li.SubItems.Add("EXPCash Recept");
            //            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //            //li.SubItems.Add("");
            //            //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            //credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            li.SubItems.Add("");
            //            mouseclickid.Rows.Add("EXPCash Recept", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "EXPCash Invoice")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("EXPCash Invoice");
            //            li.SubItems.Add("EXPCash Invoice");
            //            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            li.SubItems.Add("");
            //            mouseclickid.Rows.Add("EXPCash Invoice", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "PRCash Recept")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("PRCash Recept");
            //            li.SubItems.Add("PRCash Recept");
            //            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add("");
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            mouseclickid.Rows.Add("PRCash Recept", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "SRCash Invoice")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("SRCash Invoice");
            //            li.SubItems.Add("SRCash Invoice");
            //            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            li.SubItems.Add("");
            //            mouseclickid.Rows.Add("SRCash Invoice", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "SaleCash Recept")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("SaleCash Recept");
            //            li.SubItems.Add("SaleCash Recept");
            //            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add("");
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            mouseclickid.Rows.Add("SaleCash Recept", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "PurchaseCash Invoice")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("PurchaseCash Invoice");
            //            li.SubItems.Add("PurchaseCash Invoice");
            //            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            li.SubItems.Add("");
            //            mouseclickid.Rows.Add("PurchaseCash Invoice", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "DNCash Recept")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("DNCash Recept");
            //            li.SubItems.Add("DNCash Recept");
            //            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add("");
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            mouseclickid.Rows.Add("DNCash Recept", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "CNCash Invoice")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("CNCash Invoice");
            //            li.SubItems.Add("CNCash Invoice");
            //            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            li.SubItems.Add("");
            //            mouseclickid.Rows.Add("CNCash Invoice", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHEREXP")
            //        {
            //            if (SPdt.Rows[i]["dc"].ToString() == "D")
            //            {
            //                ListViewItem li;
            //                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //                li.SubItems.Add("EXPVOUCHERSALE");
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
            //                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //                li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //            }
            //            else
            //            {
            //                ListViewItem li;
            //                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //                li.SubItems.Add("EXPVOUCHERPURCHASE");
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
            //                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //                li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //            }
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERDN")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("GSTVOUCHERDN");
            //            li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
            //            li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
            //            string CD = SPdt.Rows[i]["dc"].ToString();
            //            if (CD == "D")
            //            {
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //                li.SubItems.Add("");
            //            }
            //            else
            //            {
            //                li.SubItems.Add("");
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


            //            }

            //            mouseclickid.Rows.Add("GSTVOUCHERDN", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERCN")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("GSTVOUCHERCN");
            //            li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
            //            li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
            //            string CD = SPdt.Rows[i]["dc"].ToString();
            //            if (CD == "D")
            //            {
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //                li.SubItems.Add("");
            //            }
            //            else
            //            {
            //                li.SubItems.Add("");
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


            //            }

            //            mouseclickid.Rows.Add("GSTVOUCHERCN", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERS")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("GSTVOUCHERSALE");
            //            li.SubItems.Add("Sales");
            //            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            li.SubItems.Add("");
            //            mouseclickid.Rows.Add("GSTVOUCHERSALE", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERSR")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("GSTVOUCHERSR");
            //            li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
            //            li.SubItems.Add("Return Bill No: " + SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add("");
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            mouseclickid.Rows.Add("GSTVOUCHERSR", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERP")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("GSTVOUCHERP");
            //            li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
            //            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add("");
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            mouseclickid.Rows.Add("GSTVOUCHERP", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();

            //                opbal = Convert.ToDouble(str[0]);
            //            }

            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERPR")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("GSTVOUCHERPR");
            //            li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
            //            li.SubItems.Add("Return Bill No: " + SPdt.Rows[i]["VoucherID"].ToString());

            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            li.SubItems.Add("");
            //            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            mouseclickid.Rows.Add("Purchase Return", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();

            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            //if (DC == "C")
            //            //{
            //            //    DC = "D";
            //            //}
            //            //else
            //            //{
            //            //    DC = "C";
            //            //}
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "Sale")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("Sale");
            //            li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
            //            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            li.SubItems.Add("");
            //            mouseclickid.Rows.Add("Sale", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();

            //                opbal = Convert.ToDouble(str[0]);
            //            }

            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "Cash Recept")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("Cash Recept");
            //            li.SubItems.Add("Sales");
            //            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add("");
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            mouseclickid.Rows.Add("Cash Recept", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "Rect")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("Rect");
            //            li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
            //            li.SubItems.Add("By Rcpt. No.: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["ShortNarration"].ToString());
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            li.SubItems.Add("");
            //            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            mouseclickid.Rows.Add("Rect", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            string CD = SPdt.Rows[i]["dc"].ToString();
            //            if (CD == "C")
            //            {
            //                CD = "D";
            //            }
            //            else
            //            {
            //                CD = "C";
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);

            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "SaleReturn")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("Sale Return");
            //            li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
            //            li.SubItems.Add("Return Bill No: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["ShortNarration"].ToString());
            //            li.SubItems.Add("");
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            mouseclickid.Rows.Add("Sale Return", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();

            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            if (DC == "C")
            //            {
            //                DC = "D";
            //            }
            //            else
            //            {
            //                DC = "C";
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "Purchase")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("Purchase");
            //            li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
            //            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add("");
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            mouseclickid.Rows.Add("Purchase", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();

            //                opbal = Convert.ToDouble(str[0]);
            //            }

            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "PurchaseReturn")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("Purchase Return");
            //            li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
            //            li.SubItems.Add("Return Bill No: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["ShortNarration"].ToString());
            //            li.SubItems.Add("");
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            mouseclickid.Rows.Add("Purchase Return", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();

            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            //if (DC == "C")
            //            //{
            //            //    DC = "D";
            //            //}
            //            //else
            //            //{
            //            //    DC = "C";
            //            //}
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "Pmnt")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("Pmnt");
            //            li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
            //            li.SubItems.Add("By Rcpt. No.: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["ShortNarration"].ToString());
            //            li.SubItems.Add("");
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));

            //            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            mouseclickid.Rows.Add("Pmnt", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();

            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            string CD = SPdt.Rows[i]["dc"].ToString();
            //            if (CD == "C")
            //            {
            //                CD = "D";
            //            }
            //            else
            //            {
            //                CD = "C";
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "Cash Invoice")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("Cash Invoice");
            //            li.SubItems.Add("Purchases");
            //            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            li.SubItems.Add("");
            //            mouseclickid.Rows.Add("Cash Invoice", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "Cheque Issued")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("Cheque Issued");
            //            li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
            //            string CD = SPdt.Rows[i]["dc"].ToString();
            //            li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
            //            if (CD == "D")
            //            {
            //                //  li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
            //                li.SubItems.Add("");
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            }
            //            else
            //            {
            //                li.SubItems.Add("");
            //                //  li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            }
            //            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            mouseclickid.Rows.Add("Cheque Issued", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();

            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            CD = SPdt.Rows[i]["dc"].ToString();
            //            if (CD == "C")
            //            {
            //                CD = "D";
            //            }
            //            else
            //            {
            //                CD = "C";
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "Draft Issued")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("Draft Issued");
            //            li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
            //            string CD = SPdt.Rows[i]["dc"].ToString();
            //            li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
            //            if (CD == "D")
            //            {
            //                //  li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
            //                li.SubItems.Add("");
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            }
            //            else
            //            {
            //                li.SubItems.Add("");
            //                //li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            }
            //            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            mouseclickid.Rows.Add("Draft Issued", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();

            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            CD = SPdt.Rows[i]["dc"].ToString();
            //            if (CD == "C")
            //            {
            //                CD = "D";
            //            }
            //            else
            //            {
            //                CD = "C";
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "Cheque/Draft/Rtgs Received")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("Cheque/Draft/Rtgs Received");
            //            li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
            //            string CD = SPdt.Rows[i]["dc"].ToString();
            //            li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
            //            if (CD == "D")
            //            {
            //                // li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
            //                li.SubItems.Add("");
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            }
            //            else
            //            {
            //                li.SubItems.Add("");
            //                // li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            }
            //            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            mouseclickid.Rows.Add("Cheque/Draft/Rtgs Received", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();

            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            CD = SPdt.Rows[i]["dc"].ToString();
            //            if (CD == "C")
            //            {
            //                CD = "D";
            //            }
            //            else
            //            {
            //                CD = "C";
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }

            //        else if (SPdt.Rows[i]["TranType"].ToString() == "Deposit Cash Into Bank")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("Deposit Cash Into Bank");
            //            li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
            //            string CD = SPdt.Rows[i]["dc"].ToString();
            //            li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
            //            if (CD == "D")
            //            {
            //                //  li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
            //                li.SubItems.Add("");
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            }
            //            else
            //            {
            //                li.SubItems.Add("");
            //                // li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            }
            //            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            mouseclickid.Rows.Add("Deposit Cash Into Bank", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();

            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            CD = SPdt.Rows[i]["dc"].ToString();
            //            if (CD == "C")
            //            {
            //                CD = "D";
            //            }
            //            else
            //            {
            //                CD = "C";
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }

            //        else if (SPdt.Rows[i]["TranType"].ToString() == "Withdraw Cash from Bank")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("Withdraw Cash from Bank");
            //            li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
            //            string CD = SPdt.Rows[i]["dc"].ToString();
            //            li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
            //            if (CD == "D")
            //            {
            //                //  li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
            //                li.SubItems.Add("");
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            }
            //            else
            //            {
            //                li.SubItems.Add("");
            //                //  li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            }
            //            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            mouseclickid.Rows.Add("Withdraw Cash from Bank", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();

            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            CD = SPdt.Rows[i]["dc"].ToString();
            //            if (CD == "C")
            //            {
            //                CD = "D";
            //            }
            //            else
            //            {
            //                CD = "C";
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }

            //        else if (SPdt.Rows[i]["TranType"].ToString() == "Bank Expenses")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("Bank Expenses");
            //            li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
            //            string CD = SPdt.Rows[i]["dc"].ToString();
            //            li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
            //            if (CD == "D")
            //            {
            //                // li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
            //                li.SubItems.Add("");
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            }
            //            else
            //            {
            //                li.SubItems.Add("");
            //                //   li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            }
            //            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            mouseclickid.Rows.Add("Bank Expenses", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();

            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            CD = SPdt.Rows[i]["dc"].ToString();
            //            if (CD == "C")
            //            {
            //                CD = "D";
            //            }
            //            else
            //            {
            //                CD = "C";
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }

            //        else if (SPdt.Rows[i]["TranType"].ToString() == "Online Transfer")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("Online Transfer");
            //            li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
            //            string CD = SPdt.Rows[i]["dc"].ToString();
            //            li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
            //            if (CD == "D")
            //            {
            //                //li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
            //                li.SubItems.Add("");
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            }
            //            else
            //            {
            //                li.SubItems.Add("");
            //                // li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            }
            //            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            mouseclickid.Rows.Add("Online Transfer", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();

            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            CD = SPdt.Rows[i]["dc"].ToString();
            //            if (CD == "C")
            //            {
            //                CD = "D";
            //            }
            //            else
            //            {
            //                CD = "C";
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "DEBIT NOTE")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("DEBIT NOTE");
            //            li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
            //            li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
            //            string CD = SPdt.Rows[i]["dc"].ToString();
            //            if (CD == "D")
            //            {
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //                li.SubItems.Add("");
            //            }
            //            else
            //            {
            //                li.SubItems.Add("");
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


            //            }

            //            mouseclickid.Rows.Add("DEBIT NOTE", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "CREDIT NOTE")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("CREDIT NOTE");
            //            li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
            //            li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
            //            string CD = SPdt.Rows[i]["dc"].ToString();
            //            if (CD == "D")
            //            {
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //                li.SubItems.Add("");
            //            }
            //            else
            //            {
            //                li.SubItems.Add("");
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


            //            }

            //            mouseclickid.Rows.Add("CREDIT NOTE", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }

            //    }
            //    #endregion

            //    txttotdebit.Text = debit.ToString("N2");
            //    txttotcredit.Text = credit.ToString("N2");
            //    txtbalance.Text = balance;
            //}
            //else
            //{
            //    //for calculate OpBalance
            //    #region
            //    string totaldebit = conn.ExecuteScalar("select sum(amount) from Ledger where Date1 < '" + DTPFrom.Text + "' and Accountid='" + cmbaccname.SelectedValue + "' and dc='D' and isactive=1 ");
            //    string totalcredit = conn.ExecuteScalar("select sum(amount) from Ledger where Date1 < '" + DTPFrom.Text + "' and Accountid='" + cmbaccname.SelectedValue + "' and dc='C' and isactive=1 ");
            //    if (totaldebit == "" || totaldebit == "NULL")
            //    {
            //        totaldebit = "0.00";
            //    }
            //    if (totalcredit == "" || totalcredit == "NULL")
            //    {
            //        totalcredit = "0.00";
            //    }
            //    Double opbal;
            //    string DC = "";
            //    DataTable opbalance = conn.getdataset("select * from ClientMaster where isactive=1 and ClientID=" + cmbaccname.SelectedValue);
            //    string stropbal = opbalance.Rows[0]["opbal"].ToString();
            //    string strDC = opbalance.Rows[0]["Dr_cr"].ToString();
            //    if (strDC == "Dr.")
            //    {
            //        double ttdebit = Convert.ToDouble(totaldebit) + Convert.ToDouble(stropbal);
            //        totaldebit = ttdebit.ToString();
            //    }
            //    else if (strDC == "Cr.")
            //    {
            //        double ttcredit = Convert.ToDouble(totalcredit) + Convert.ToDouble(stropbal);
            //        totalcredit = ttcredit.ToString();
            //    }

            //    if (Convert.ToDouble(totaldebit) >= Convert.ToDouble(totalcredit))
            //    {
            //        opbal = Convert.ToDouble(totaldebit) - Convert.ToDouble(totalcredit);
            //        txtopbal.Text = opbal.ToString("N2") + " Dr.";
            //        DC = "D";
            //    }
            //    else
            //    {
            //        opbal = Convert.ToDouble(totalcredit) - Convert.ToDouble(totaldebit);
            //        txtopbal.Text = opbal.ToString("N2") + " Cr.";
            //        DC = "C";
            //    }

            //    //for (int i = 0; i < OPdt.Rows.Count; i++)
            //    //{
            //    //    Double opbal = 0;
            //    //    if (OPdt.Rows[i]["DC"].ToString() == "D")
            //    //    {

            //    //    }
            //    //    else if (OPdt.Rows[i]["TranType"].ToString() == "Rect")
            //    //    {
            //    //        ListViewItem li;
            //    //        li = LVledger.Items.Add(Convert.ToDateTime(OPdt.Rows[i]["Date1"].ToString()).ToString("dd-MMM-yyyy"));
            //    //        li.SubItems.Add("Rect");
            //    //        li.SubItems.Add(OPdt.Rows[i]["OT1"].ToString());
            //    //        li.SubItems.Add("By Rcpt. No.: " + OPdt.Rows[i]["VoucherID"].ToString() + "; " + OPdt.Rows[i]["ShortNarration"].ToString());
            //    //        li.SubItems.Add("");
            //    //        li.SubItems.Add(Math.Round(Convert.ToDouble(OPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //    //        li.SubItems.Add("Rect");
            //    //    }
            //    //}
            //    #endregion
            //    //for create ledger
            //    mouseclickid.Rows.Clear();
            //    LVledger.Items.Clear();
            //    #region
            //    DataTable SPdt = conn.getdataset("select * from Ledger where isactive=1 and Date1 between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and Accountid='" + cmbaccname.SelectedValue + "' order by Date1");
            //    string balance = "0.00";
            //    balance = Convert.ToString(opbal);
            //    Double debit = 0, credit = 0;
            //    for (int i = 0; i < SPdt.Rows.Count; i++)
            //    {
            //        if (SPdt.Rows[i]["TranType"].ToString() == "EXPCash Recept")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("EXPCash Recept");
            //            li.SubItems.Add("EXPCash Recept");
            //            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //            //li.SubItems.Add("");
            //            //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            //credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            li.SubItems.Add("");
            //            mouseclickid.Rows.Add("EXPCash Recept", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "EXPCash Invoice")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("EXPCash Invoice");
            //            li.SubItems.Add("EXPCash Invoice");
            //            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            li.SubItems.Add("");
            //            mouseclickid.Rows.Add("EXPCash Invoice", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "PRCash Recept")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("PRCash Recept");
            //            li.SubItems.Add("PRCash Recept");
            //            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add("");
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            mouseclickid.Rows.Add("PRCash Recept", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "SRCash Invoice")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("SRCash Invoice");
            //            li.SubItems.Add("SRCash Invoice");
            //            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            li.SubItems.Add("");
            //            mouseclickid.Rows.Add("SRCash Invoice", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "SaleCash Recept")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("SaleCash Recept");
            //            li.SubItems.Add("SaleCash Recept");
            //            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add("");
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            mouseclickid.Rows.Add("SaleCash Recept", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "PurchaseCash Invoice")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("PurchaseCash Invoice");
            //            li.SubItems.Add("PurchaseCash Invoice");
            //            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            li.SubItems.Add("");
            //            mouseclickid.Rows.Add("PurchaseCash Invoice", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "DNCash Recept")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("DNCash Recept");
            //            li.SubItems.Add("DNCash Recept");
            //            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add("");
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            mouseclickid.Rows.Add("DNCash Recept", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "CNCash Invoice")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("CNCash Invoice");
            //            li.SubItems.Add("CNCash Invoice");
            //            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            li.SubItems.Add("");
            //            mouseclickid.Rows.Add("CNCash Invoice", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHEREXP")
            //        {
            //            if (SPdt.Rows[i]["dc"].ToString() == "D")
            //            {
            //                ListViewItem li;
            //                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //                li.SubItems.Add("EXPVOUCHERSALE");
            //                li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
            //                li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                li.SubItems.Add("");
            //                mouseclickid.Rows.Add("Exp Voucher Sale", SPdt.Rows[i]["VoucherID"].ToString());
            //                if (i != 0)
            //                {
            //                    string[] str = balance.Split(' ');
            //                    char temp = str[1][0];
            //                    DC = temp.ToString();

            //                    opbal = Convert.ToDouble(str[0]);
            //                }

            //                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //                li.SubItems.Add(balance);
            //                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //                li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //            }
            //            else
            //            {
            //                ListViewItem li;
            //                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //                li.SubItems.Add("EXPVOUCHERPURCHASE");
            //                li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
            //                li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //                li.SubItems.Add("");
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //                mouseclickid.Rows.Add("Exp VoucherPurchase", SPdt.Rows[i]["VoucherID"].ToString());
            //                if (i != 0)
            //                {
            //                    string[] str = balance.Split(' ');
            //                    char temp = str[1][0];
            //                    DC = temp.ToString();

            //                    opbal = Convert.ToDouble(str[0]);
            //                }

            //                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //                li.SubItems.Add(balance);
            //                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //                li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //            }
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERDN")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("GSTVOUCHERDN");
            //            li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
            //            li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
            //            string CD = SPdt.Rows[i]["dc"].ToString();
            //            if (CD == "D")
            //            {
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //                li.SubItems.Add("");
            //            }
            //            else
            //            {
            //                li.SubItems.Add("");
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


            //            }

            //            mouseclickid.Rows.Add("GSTVOUCHERDN", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERCN")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("GSTVOUCHERCN");
            //            li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
            //            li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
            //            string CD = SPdt.Rows[i]["dc"].ToString();
            //            if (CD == "D")
            //            {
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //                li.SubItems.Add("");
            //            }
            //            else
            //            {
            //                li.SubItems.Add("");
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


            //            }

            //            mouseclickid.Rows.Add("GSTVOUCHERCN", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERS")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("GSTVOUCHERSALE");
            //            li.SubItems.Add("Sales");
            //            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            li.SubItems.Add("");
            //            mouseclickid.Rows.Add("GSTVOUCHERSALE", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERSR")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("GSTVOUCHERSR");
            //            li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
            //            li.SubItems.Add("Return Bill No: " + SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add("");
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            mouseclickid.Rows.Add("GSTVOUCHERSR", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERP")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("GSTVOUCHERP");
            //            li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
            //            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add("");
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            mouseclickid.Rows.Add("GSTVOUCHERP", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();

            //                opbal = Convert.ToDouble(str[0]);
            //            }

            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERPR")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("GSTVOUCHERPR");
            //            li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
            //            li.SubItems.Add("Return Bill No: " + SPdt.Rows[i]["VoucherID"].ToString());

            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            li.SubItems.Add("");
            //            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            mouseclickid.Rows.Add("Purchase Return", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();

            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            //if (DC == "C")
            //            //{
            //            //    DC = "D";
            //            //}
            //            //else
            //            //{
            //            //    DC = "C";
            //            //}
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "Sale")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("Sale");
            //            li.SubItems.Add("Sales");
            //            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            li.SubItems.Add("");
            //            mouseclickid.Rows.Add("Sale", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "Cash Recept")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("Cash Recept");
            //            li.SubItems.Add("Sales");
            //            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add("");
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            mouseclickid.Rows.Add("Cash Recept", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "Rect")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("Rect");
            //            li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
            //            li.SubItems.Add("By Rcpt. No.: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["ShortNarration"].ToString());
            //            li.SubItems.Add("");
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            mouseclickid.Rows.Add("Rect", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "SaleReturn")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("Sale Return");
            //            li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
            //            li.SubItems.Add("Return Bill No: " + SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add("");
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            mouseclickid.Rows.Add("Sale Return", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "Purchase")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("Purchase");
            //            li.SubItems.Add("Purchases");
            //            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add("");
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            mouseclickid.Rows.Add("Purchase", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }

            //        else if (SPdt.Rows[i]["TranType"].ToString() == "PurchaseReturn")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("Purchase Return");
            //            li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
            //            li.SubItems.Add("Return Bill No: " + SPdt.Rows[i]["VoucherID"].ToString());

            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            li.SubItems.Add("");
            //            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            mouseclickid.Rows.Add("Purchase Return", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();

            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            //if (DC == "C")
            //            //{
            //            //    DC = "D";
            //            //}
            //            //else
            //            //{
            //            //    DC = "C";
            //            //}
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "Pmnt")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("Pmnt");
            //            li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
            //            li.SubItems.Add("By Rcpt. No.: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["ShortNarration"].ToString());
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            li.SubItems.Add("");
            //            mouseclickid.Rows.Add("Pmnt", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "Cash Invoice")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("Cash Invoice");
            //            li.SubItems.Add("Purchases");
            //            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //            li.SubItems.Add("");
            //            mouseclickid.Rows.Add("Cash Invoice", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "Cheque Issued")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("Cheque Issued");
            //            li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
            //            li.SubItems.Add("By Rcpt. No.: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["OT5"].ToString() + "; " + SPdt.Rows[i]["OT6"].ToString());
            //            string CD = SPdt.Rows[i]["dc"].ToString();
            //            if (CD == "D")
            //            {
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //                li.SubItems.Add("");
            //            }
            //            else
            //            {
            //                li.SubItems.Add("");
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


            //            }

            //            mouseclickid.Rows.Add("Cheque Issued", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "Draft Issued")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("Draft Issued");
            //            li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
            //            li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
            //            string CD = SPdt.Rows[i]["dc"].ToString();
            //            if (CD == "D")
            //            {
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["OT3"].ToString()), 2).ToString("N2"));
            //                debit += Convert.ToDouble(SPdt.Rows[i]["OT3"].ToString());
            //                li.SubItems.Add("");
            //            }
            //            else
            //            {
            //                li.SubItems.Add("");
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["OT3"].ToString()), 2).ToString("N2"));
            //                credit += Convert.ToDouble(SPdt.Rows[i]["OT3"].ToString());


            //            }

            //            mouseclickid.Rows.Add("Draft Issued", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["OT3"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "Cheque/Draft/Rtgs Received")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("Cheque/Draft/Rtgs Received");
            //            li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
            //            li.SubItems.Add("By Rcpt. No.: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["OT5"].ToString() + "; " + SPdt.Rows[i]["OT6"].ToString());
            //            string CD = SPdt.Rows[i]["dc"].ToString();
            //            if (CD == "D")
            //            {
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //                li.SubItems.Add("");
            //            }
            //            else
            //            {
            //                li.SubItems.Add("");
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


            //            }

            //            mouseclickid.Rows.Add("Cheque/Draft/Rtgs Received", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "Deposit Cash Into Bank")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("Deposit Cash Into Bank");
            //            li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
            //            li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
            //            string CD = SPdt.Rows[i]["dc"].ToString();
            //            if (CD == "D")
            //            {
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //                li.SubItems.Add("");
            //            }
            //            else
            //            {
            //                li.SubItems.Add("");
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


            //            }

            //            mouseclickid.Rows.Add("Deposit Cash Into Bank", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "Withdraw Cash from Bank")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("Withdraw Cash from Bank");
            //            li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
            //            li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
            //            string CD = SPdt.Rows[i]["dc"].ToString();
            //            if (CD == "D")
            //            {
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //                li.SubItems.Add("");
            //            }
            //            else
            //            {
            //                li.SubItems.Add("");
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


            //            }

            //            mouseclickid.Rows.Add("Withdraw Cash from Bank", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "Bank Expenses")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("Bank Expenses");
            //            li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
            //            li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
            //            string CD = SPdt.Rows[i]["dc"].ToString();
            //            if (CD == "D")
            //            {
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //                li.SubItems.Add("");
            //            }
            //            else
            //            {
            //                li.SubItems.Add("");
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


            //            }

            //            mouseclickid.Rows.Add("Bank Expenses", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "Online Transfer")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("Online Transfer");
            //            li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
            //            li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
            //            string CD = SPdt.Rows[i]["dc"].ToString();
            //            if (CD == "D")
            //            {
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //                li.SubItems.Add("");
            //            }
            //            else
            //            {
            //                li.SubItems.Add("");
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


            //            }

            //            mouseclickid.Rows.Add("Online Transfer", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "DEBIT NOTE")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("DEBIT NOTE");
            //            li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
            //            li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
            //            string CD = SPdt.Rows[i]["dc"].ToString();
            //            if (CD == "D")
            //            {
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //                li.SubItems.Add("");
            //            }
            //            else
            //            {
            //                li.SubItems.Add("");
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


            //            }

            //            mouseclickid.Rows.Add("DEBIT NOTE", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }
            //        else if (SPdt.Rows[i]["TranType"].ToString() == "CREDIT NOTE")
            //        {
            //            ListViewItem li;
            //            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
            //            li.SubItems.Add("CREDIT NOTE");
            //            li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
            //            li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
            //            string CD = SPdt.Rows[i]["dc"].ToString();
            //            if (CD == "D")
            //            {
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
            //                li.SubItems.Add("");
            //            }
            //            else
            //            {
            //                li.SubItems.Add("");
            //                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
            //                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


            //            }

            //            mouseclickid.Rows.Add("CREDIT NOTE", SPdt.Rows[i]["VoucherID"].ToString());
            //            if (i != 0)
            //            {
            //                string[] str = balance.Split(' ');
            //                char temp = str[1][0];
            //                DC = temp.ToString();
            //                opbal = Convert.ToDouble(str[0]);
            //            }
            //            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
            //            li.SubItems.Add(balance);
            //            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
            //            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
            //        }

            //    }
            //    #endregion

            //    txttotdebit.Text = debit.ToString("N2");
            //    txttotcredit.Text = credit.ToString("N2");
            //    txtbalance.Text = balance;
            //}
        }
        private void BtnViewReport_Click(object sender, EventArgs e)
        {
                EventHandler<EventArgs> ea = Canceled;
                if (ea != null)
                    ea(this, e);
                binddata();
            
        }
        static double balance;
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
        private void binddata()
        {


            //for calculate OpBalance
            #region
            string totaldebitcredit = conn.ExecuteScalar("select sum(amount) from Ledger where Date1 < '" + DTPFrom.Text + "' and OT1='Credit' and dc='D' and isactive=1 ");
            string totaldebitcash = conn.ExecuteScalar("select sum(amount) from Ledger where Date1 < '" + DTPFrom.Text + "' and OT1='Cash' and dc='D' and isactive=1 ");
            string totalcreditcredit = conn.ExecuteScalar("select sum(amount) from Ledger where Date1 < '" + DTPFrom.Text + "' and OT1='Credit' and dc='C' and isactive=1 ");
            string totalcreditcash = conn.ExecuteScalar("select sum(amount) from Ledger where Date1 < '" + DTPFrom.Text + "' and OT1='Cash' and dc='C' and isactive=1 ");
            if (totaldebitcredit == "" || totaldebitcredit == "NULL")
            {
                totaldebitcredit = "0.00";
            }
            if (totaldebitcash == "" || totaldebitcash == "NULL")
            {
                totaldebitcash = "0.00";
            }
            if (totalcreditcredit == "" || totalcreditcredit == "NULL")
            {
                totalcreditcredit = "0.00";
            }
            if (totalcreditcash == "" || totalcreditcash == "NULL")
            {
                totalcreditcash = "0.00";
            }

            Double opbalC,opbalS;
            string DC = "";
            //DataTable opbalance = conn.getdataset("select * from ClientMaster where isactive=1 and ClientID=" + cmbaccname.SelectedValue);
            //string stropbal = opbalance.Rows[0]["opbal"].ToString();
            //string strDC = opbalance.Rows[0]["Dr_cr"].ToString();
            //if (strDC == "Dr.")
            //{
            //    double ttdebit = Convert.ToDouble(totaldebit) + Convert.ToDouble(stropbal);
            //    totaldebit = ttdebit.ToString();
            //}
            //else if (strDC == "Cr.")
            //{
            //    double ttcredit = Convert.ToDouble(totalcredit) + Convert.ToDouble(stropbal);
            //    totalcredit = ttcredit.ToString();
            //}

            if (Convert.ToDouble(totaldebitcredit) >= Convert.ToDouble(totalcreditcredit))
            {
                opbalC = Convert.ToDouble(totaldebitcredit) - Convert.ToDouble(totalcreditcredit);
                txtopbalcredit.Text = opbalC.ToString("N2") + " Dr.";
                DC = "D";
            }
            else
            {
                opbalC = Convert.ToDouble(totalcreditcredit) - Convert.ToDouble(totaldebitcredit);
                txtopbalcredit.Text = opbalC.ToString("N2") + " Cr.";
                DC = "C";
            }
            if (Convert.ToDouble(totaldebitcash) >= Convert.ToDouble(totalcreditcash))
            {
                opbalS = Convert.ToDouble(totaldebitcash) - Convert.ToDouble(totalcreditcash);
                txtopbalcash.Text = opbalS.ToString("N2") + " Dr.";
                DC = "D";
            }
            else
            {
                opbalS = Convert.ToDouble(totalcreditcash) - Convert.ToDouble(totaldebitcash);
                txtopbalcash.Text = opbalS.ToString("N2") + " Cr.";
                DC = "C";
            }
            #endregion
            mouseclickid.Rows.Clear();
            LVledger.Items.Clear();

            #region

            string balanceC = "0.00";
            string balanceS = "0.00";
            balanceC = Convert.ToString(opbalC);
            balanceS = Convert.ToString(opbalS);
          

            //ledger for all
            DataTable Ledger = conn.getdataset("select date1, trantype,sum(debitcredit) as debitcredit,sum(debitcash) as debitcash,sum(Creditcredit) as Creditcredit,sum(Creditcash) as Creditcash from (select date1, trantype,isnull((case when DC='D' and OT1='Credit' then sum(Amount)  end),0) as debitcredit,0 as debitcash,0 as Creditcredit,0 as Creditcash from Ledger where isactive=1 group by Date1,TranType,DC,OT1 union select date1, trantype, 0 as debitcredit,isnull((case when DC='D' and OT1='Cash' then sum(Amount) end),0)  as debitcash,0 as Creditcredit,0 as Creditcash from Ledger where isactive=1 group by Date1,TranType,DC,OT1 union all select date1, trantype, 0 as debitcredit,0 as debitcash,isnull((case when DC='C' and OT1='Credit' then sum(Amount) end),0)  as Creditcredit,0 as Creditcash from Ledger where isactive=1 group by Date1,TranType,DC,OT1 union all select date1, trantype,0 as debitcredit,0 as debitcash,0 as Creditcredit,isnull((case when DC='C' and OT1='Cash' then sum(Amount) end),0)  as Creditcash from Ledger where isactive=1 group by Date1,TranType,DC,OT1) entries where Date1='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' group by Date1,TranType order by Date1,trantype ");
            Ledger = changedtclone(Ledger);

          
            //POS data
            DataTable POS = conn.getdataset("select BillDate as date1, 'POS' as trantype,0 as debitcredit,sum(totalnet) as debitcash,0 as Creditcredit,0 as Creditcash from BillPOSMaster where isactive=1 and BillDate='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' group by BillDate order by BillDate");
            POS = changedtclone(POS);
            Ledger.Merge(POS);


            Ledger.DefaultView.Sort = "[date1] ASC";
            Ledger = Ledger.DefaultView.ToTable();

            double dc = 0;
            double ds = 0;
            double cc = 0;
            double cs = 0;
            double opbal = 0;
            String DCC = "";
            String DCS = "";
            for (int i = 0; i < Ledger.Rows.Count; i++)
            {
                ListViewItem li;
                li = LVledger.Items.Add(Convert.ToDateTime(Ledger.Rows[i]["date1"].ToString()).ToString(Master.dateformate));
                li.SubItems.Add(Ledger.Rows[i]["trantype"].ToString());
                li.SubItems.Add(Ledger.Rows[i]["debitcredit"].ToString());
                li.SubItems.Add(Ledger.Rows[i]["debitcash"].ToString());
                li.SubItems.Add(Ledger.Rows[i]["Creditcredit"].ToString());
                li.SubItems.Add(Ledger.Rows[i]["Creditcash"].ToString());
                if (Convert.ToDouble(Ledger.Rows[i]["debitcredit"].ToString()) >= Convert.ToDouble(Ledger.Rows[i]["Creditcredit"].ToString()))
                {
                    opbal = Convert.ToDouble(Ledger.Rows[i]["debitcredit"].ToString()) - Convert.ToDouble(Ledger.Rows[i]["Creditcredit"].ToString());
                    
                    DCC = "D";
                    if (i != 0)
                    {
                        string[] str = balanceC.Split(' ');
                        char temp = str[1][0];
                        DC = temp.ToString();
                        opbalC = Convert.ToDouble(str[0]);
                    }
                    balanceC = getbalance(opbalC, opbal, DC, DCC, i);
                    li.SubItems.Add(balanceC + " Dr.");
                }
                else
                {
                    opbal = Convert.ToDouble(Ledger.Rows[i]["Creditcredit"].ToString()) - Convert.ToDouble(Ledger.Rows[i]["debitcredit"].ToString());
                  
                    DCC = "C";
                    if (i != 0)
                    {
                        string[] str = balanceC.Split(' ');
                        char temp = str[1][0];
                        DC = temp.ToString();
                        opbalC = Convert.ToDouble(str[0]);
                    }
                    balanceC = getbalance(opbalC, opbal, DC, DCC, i);
                    li.SubItems.Add(balanceC + " Dr.");
                }
                if (Convert.ToDouble(Ledger.Rows[i]["debitcash"].ToString()) >= Convert.ToDouble(Ledger.Rows[i]["Creditcash"].ToString()))
                {
                    opbal = Convert.ToDouble(Ledger.Rows[i]["debitcash"].ToString()) - Convert.ToDouble(Ledger.Rows[i]["Creditcash"].ToString());
                    
                    DCS = "D";
                    if (i != 0)
                    {
                        string[] str = balanceS.Split(' ');
                        char temp = str[1][0];
                        DC = temp.ToString();
                        opbalS = Convert.ToDouble(str[0]);
                    }
                    balanceS = getbalance(opbalS, opbal, DC, DCS, i);
                    li.SubItems.Add(balanceS + " Dr.");

                }
                else
                {
                    opbal = Convert.ToDouble(Ledger.Rows[i]["Creditcash"].ToString()) - Convert.ToDouble(Ledger.Rows[i]["debitcash"].ToString());
                   
                    DC = "C";
                    if (i != 0)
                    {
                        string[] str = balanceS.Split(' ');
                        char temp = str[1][0];
                        DC = temp.ToString();
                        opbalS = Convert.ToDouble(str[0]);
                    }
                    balanceS = getbalance(opbalS, opbal, DC, DCS, i);
                    li.SubItems.Add(balanceS + " Dr.");
                }

                dc += Convert.ToDouble(Ledger.Rows[i]["debitcredit"].ToString());
                ds += Convert.ToDouble(Ledger.Rows[i]["debitcash"].ToString());
                cc += Convert.ToDouble(Ledger.Rows[i]["Creditcredit"].ToString());
                cs += Convert.ToDouble(Ledger.Rows[i]["Creditcash"].ToString());

                txtclcredit.Text = balanceC;
                txtclcash.Text = balanceS;
            }


            addblank();
            ListViewItem lia;
            lia = LVledger.Items.Add("Total");
            lia.SubItems.Add("");
            lia.SubItems.Add(dc.ToString("N2"));
            lia.SubItems.Add(ds.ToString("N2"));
            lia.SubItems.Add(cc.ToString("N2"));
            lia.SubItems.Add(cs.ToString("N2"));
            lia.SubItems.Add("");
            lia.SubItems.Add("");

            #endregion






            ////sale purchase data
            //DataTable salePurchase = conn.getdataset("select b.Bill_Date, count(*) as totalentry,sum(b.totalaqty) as qty,sum(b.totalbasic) as totalbasic,sum(b.totaladddiscount + b.totaldiscount) as Discount,sum(b.totaltax + isnull(bc.sgst,0)+isnull(bc.cgst,0)+isnull(bc.igst,0)) as totaltax,sum(b.totalnet) as total,b.BillType as type from billmaster b left join Billchargesmaster bc on bc.billno=b.billno where b.isactive=1 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by b.Bill_Date order by Bill_Date");
            //salePurchase = changedtclone(salePurchase);
            //main.Merge(salePurchase);

            ////Quick paymentReceipt
            //DataTable qPR = conn.getdataset("select date as bill_date,count(*) as totalentry,'' as qty,'' as totalbasic,sum(discountamt) as Discount, '' as totaltax, sum(netamt) as total,type from paymentreceipt where isactive=1 and date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by Date order by Date");
            //qPR = changedtclone(qPR);
            //main.Merge(qPR);

            ////bankentry
            //DataTable qBE = conn.getdataset("select date as bill_Date, count(*) as totalentry, '' as qty, '' as totalbasic, '0' as discount, '' as totaltax, sum(totalamount) as total,PaymentTerms as type from Voucher where isactive=1 and date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by date,PaymentTerms order by date");
            //qBE = changedtclone(qBE);
            //main.Merge(qBE);

            ////purchase
            //DataTable Purchase = conn.getdataset("select b.Bill_Date, count(*) as totalentry,sum(b.totalaqty) as qty, sum(b.totalbasic) as totalbasic,sum(b.totaladddiscount + b.totaldiscount) as Discount,sum(b.totaltax + isnull(bc.sgst,0)+isnull(bc.cgst,0)+isnull(bc.igst,0)) as totaltax,sum(b.totalnet) as total from billmaster b left join Billchargesmaster bc on bc.billno=b.billno where b.isactive=1 and b.BillType='P' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by b.Bill_Date order by Bill_Date");
            //Purchase = changedtclone(Purchase);
            //main.Merge(Purchase);

            ////sale return
            //DataTable SaleReturn = conn.getdataset("select b.Bill_Date, count(*) as totalentry,sum(b.totalaqty) as qty, sum(b.totalbasic) as totalbasic,sum(b.totaladddiscount + b.totaldiscount) as Discount,sum(b.totaltax + isnull(bc.sgst,0)+isnull(bc.cgst,0)+isnull(bc.igst,0)) as totaltax,sum(b.totalnet) as total from billmaster b left join Billchargesmaster bc on bc.billno=b.billno where b.isactive=1 and b.BillType='SR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by b.Bill_Date order by Bill_Date");
            //SaleReturn = changedtclone(SaleReturn);
            //main.Merge(SaleReturn);

            ////purchase return
            //DataTable PurchaseReturn = conn.getdataset("select b.Bill_Date, count(*) as totalentry,sum(b.totalaqty) as qty, sum(b.totalbasic) as totalbasic,sum(b.totaladddiscount + b.totaldiscount) as Discount,sum(b.totaltax + isnull(bc.sgst,0)+isnull(bc.cgst,0)+isnull(bc.igst,0)) as totaltax,sum(b.totalnet) as total from billmaster b left join Billchargesmaster bc on bc.billno=b.billno where b.isactive=1 and b.BillType='PR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by b.Bill_Date order by Bill_Date");
            //PurchaseReturn = changedtclone(PurchaseReturn);
            //main.Merge(PurchaseReturn);

           

            ////Quick Payment
            //DataTable qpayment = conn.getdataset("select date as bill_date,count(*) as totalentry,'' as qty,'' as totalbasic,sum(discountamt) as Discount, '' as totaltax, sum(netamt) as total from paymentreceipt where isactive=1 and type='P' and date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by Date order by Date");
            //qpayment = changedtclone(qpayment);
            //main.Merge(qpayment);

            //for (int i = 0; i < sale.Rows.Count; i++)
            //{
            //    ListViewItem li;
            //    li = LVledger.Items.Add(Convert.ToDateTime(sale.Rows[i]["Bill_Date"].ToString()).ToString(Master.dateformate));
            //    li.SubItems.Add("Sale");
            //    li.SubItems.Add(sale.Rows[i]["totalentry"].ToString());
            //    li.SubItems.Add(sale.Rows[i]["qty"].ToString());
            //    li.SubItems.Add(sale.Rows[i]["totalbasic"].ToString());
            //    li.SubItems.Add(sale.Rows[i]["totaltax"].ToString());
            //    li.SubItems.Add(sale.Rows[i]["total"].ToString());
            //    li.SubItems.Add("");
                
            //}

            
           
            //for (int i = 0; i < Purchase.Rows.Count; i++)
            //{
            //    ListViewItem li;
            //    li = LVledger.Items.Add(Convert.ToDateTime(Purchase.Rows[i]["Bill_Date"].ToString()).ToString(Master.dateformate));
            //    li.SubItems.Add("Purchase");
            //    li.SubItems.Add(Purchase.Rows[i]["totalentry"].ToString());
            //    li.SubItems.Add(Purchase.Rows[i]["qty"].ToString());
            //    li.SubItems.Add(Purchase.Rows[i]["totalbasic"].ToString());
            //    li.SubItems.Add(Purchase.Rows[i]["totaltax"].ToString());
            //    li.SubItems.Add(Purchase.Rows[i]["total"].ToString());
            //    li.SubItems.Add("");

            //}

        }

        private void addblank()
        {
            ListViewItem li;
            li = LVledger.Items.Add("");
            for (int i = 0; i < 6; i++)
            {
                li.SubItems.Add("");
            }
        }


       
        public static string s;
     

        private void DTPFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnViewReport.Focus();
            }
        }

        private void DTPTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnViewReport.Focus();
            }
        }
       
        private void LVledger_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //open();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }



        private void cmbaccname_Enter(object sender, EventArgs e)
        {
            if (cnt == 0)
            {
                //    cmbaccname.SelectedIndex = 0;
                // cmbaccname.DroppedDown = true;
            }
        }

        private void btngenrpt_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult dr1 = MessageBox.Show("Do you want to Print Ledger?", "Ledger", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    if (LVledger.Items.Count > 0)
                    {
                        prn.execute("delete from printing");
                        DataTable client = new DataTable();
                        //DataTable client = conn.getdataset("select * from clientmaster where isactive=1 and clientID='" + cmbaccname.SelectedValue + "'");
                        DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1");

                        //         string date = "", type = "", Account = "", drAmount = "", crAmount="",balance="";
                        for (int i = 0; i < LVledger.Items.Count; i++)
                        {
                            string date = "", type = "", Account = "", drAmount = "", crAmount = "", balance = "", Remarks = "";
                            date = LVledger.Items[i].SubItems[0].Text;
                            type = LVledger.Items[i].SubItems[1].Text;
                            Account = LVledger.Items[i].SubItems[2].Text;
                            drAmount = LVledger.Items[i].SubItems[4].Text;
                            crAmount = LVledger.Items[i].SubItems[5].Text;
                            balance = LVledger.Items[i].SubItems[6].Text;
                            Remarks = LVledger.Items[i].SubItems[3].Text;

                            string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34,T35,T36,T37,T38,T39,T40,T41)VALUES";
                            qry += "('" + date + "','" + type + "','" + Account + "','" + drAmount + "','" + crAmount + "','" + balance + "','','','','" + txtopbalcredit.Text + "','" + dt1.Rows[0][0].ToString() + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][2].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + dt1.Rows[0][11].ToString() + "','" + dt1.Rows[0][12].ToString() + "','" + dt1.Rows[0][13].ToString() + "','" + DTPFrom.Text + "','','" + client.Rows[0][0].ToString() + "','" + client.Rows[0][1].ToString() + "','" + client.Rows[0][2].ToString() + "','" + client.Rows[0][3].ToString() + "','" + client.Rows[0][4].ToString() + "','" + client.Rows[0][5].ToString() + "','" + client.Rows[0][6].ToString() + "','" + client.Rows[0][7].ToString() + "','" + client.Rows[0][8].ToString() + "','" + client.Rows[0][9].ToString() + "','" + client.Rows[0][10].ToString() + "','" + client.Rows[0][11].ToString() + "','" + client.Rows[0][12].ToString() + "','" + client.Rows[0][13].ToString() + "','" + Remarks + "')";
                            prn.execute(qry);

                        }
                        /*       for (int i = 0; i < LVledger.Items.Count; i++)
                               {
                                   string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34,T35,T36,T37,T38,T39,T40)VALUES";
                                   qry += "('" + date + "','" + type + "','" + Account + "','" + drAmount + "','" + crAmount + "','" + balance + "','" + txttotdebit.Text + "','" + txttotcredit.Text + "','" + txtbalance.Text + "','" + txtopbal.Text + "','" + dt1.Rows[0][0].ToString() + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][2].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + dt1.Rows[0][11].ToString() + "','" + dt1.Rows[0][12].ToString() + "','" + dt1.Rows[0][13].ToString() + "','"+ DTPFrom.Text +"','"+ DTPTo.Text+"','"+ client.Rows[0][0].ToString() + "','" + client.Rows[0][1].ToString() + "','" + client.Rows[0][2].ToString() + "','" + client.Rows[0][3].ToString() + "','" + client.Rows[0][4].ToString() + "','" + client.Rows[0][5].ToString() + "','" + client.Rows[0][6].ToString() + "','" + client.Rows[0][7].ToString() + "','" + client.Rows[0][8].ToString() + "','" + client.Rows[0][9].ToString() + "','" + client.Rows[0][10].ToString() + "','" + client.Rows[0][11].ToString() + "','" + client.Rows[0][12].ToString() + "','" + client.Rows[0][13].ToString()+"')";
                                   prn.execute(qry);

                               } */
                        string reportName = "Ledger";
                        Print popup = new Print(reportName);
                        popup.ShowDialog();
                        popup.Dispose();

                    }
                    else
                    {
                        MessageBox.Show("No Records For Print", "Ledger", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            catch
            {
            }
        }

        private void txtopbal_Enter(object sender, EventArgs e)
        {
            txtopbalcredit.BackColor = Color.LightYellow;
        }

        private void txtopbal_Leave(object sender, EventArgs e)
        {
            txtopbalcredit.BackColor = Color.White;
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

        private void btngenrpt_MouseLeave(object sender, EventArgs e)
        {
            btngenrpt.UseVisualStyleBackColor = true;
            btngenrpt.BackColor = Color.FromArgb(51, 153, 255);
            btngenrpt.ForeColor = Color.White;
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
        string searchstr;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //empty the string for every 1 seconds
            // searchstr = "";
        }

        private void LVledger_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //open();
            }
        }
    }
}
