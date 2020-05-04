using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RamdevSales
{
    public partial class OpBalanceEditor : Form
    {
        private Master master;
        private TabControl tabControl;
        Connection conn = new Connection();
        DataTable dtconnstring = new DataTable();
        public OpBalanceEditor()
        {
            InitializeComponent();
        }

        public OpBalanceEditor(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
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
        DataSet ds = new DataSet();
        OleDbSettings ods = new OleDbSettings();
        public void binddata()
        {
            ds = ods.getdata("Select * from SQLSetting");
            dtconnstring = ds.Tables[0];
            string oldconnectionstring = dtconnstring.Rows[0]["OT7"].ToString();
            if (!string.IsNullOrEmpty(oldconnectionstring))
            {
                SqlConnection scon = new SqlConnection(oldconnectionstring);
                temptable = new DataTable();
                temptable.Columns.Add("AccountName", typeof(string));
                temptable.Columns.Add("Balance", typeof(string));
                temptable1 = new DataTable();
                temptable1.Columns.Add("AccountName", typeof(string));
                temptable1.Columns.Add("Dr Balance", typeof(string));
                temptable1.Columns.Add("Cr Balance", typeof(string));
                string onlyopbal = "";
                DataTable dt = new DataTable();
                dt = conn.getdataset("select * from ClientMaster where isactive=1  order by AccountName asc", scon);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        #region
                        //  string totaldebit = conn.ExecuteScalar("select sum(amount) from Ledger where Date1 < '" + DTPFrom.Text + "' and Accountid='" + dt.Rows[i]["ClientID"].ToString() + "' and dc='D' and isactive=1 order by Date1");
                        //  string totalcredit = conn.ExecuteScalar("select sum(amount) from Ledger where Date1 < '" + DTPFrom.Text + "' and Accountid='" + dt.Rows[i]["ClientID"].ToString() + "' and dc='C' and isactive=1 order by Date1");
                        string totaldebit = "";
                        string totalcredit = "";
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
                        DataTable opbalance = conn.getdataset("select * from ClientMaster where isactive=1 and ClientID=" + dt.Rows[i]["ClientID"].ToString(), scon);
                        string stropbal = opbalance.Rows[0]["opbal"].ToString();
                        string strDC = opbalance.Rows[0]["Dr_cr"].ToString();
                        if (strDC == "Dr.")
                        {
                            double ttdebit = Convert.ToDouble(totaldebit) + Convert.ToDouble(stropbal);
                            totaldebit = ttdebit.ToString();
                            onlyopbal = ttdebit.ToString() + " Dr.";
                        }
                        else if (strDC == "Cr.")
                        {
                            double ttcredit = Convert.ToDouble(totalcredit) + Convert.ToDouble(stropbal);
                            totalcredit = ttcredit.ToString();
                            onlyopbal = ttcredit.ToString() + " Cr.";
                        }
                        if (Convert.ToDouble(totaldebit) >= Convert.ToDouble(totalcredit))
                        {
                            opbal = Convert.ToDouble(totaldebit) - Convert.ToDouble(totalcredit);
                            //   txtopbal.Text = opbal.ToString("N2") + " Dr.";
                            DC = "D";
                        }
                        else
                        {
                            opbal = Convert.ToDouble(totalcredit) - Convert.ToDouble(totaldebit);
                            //   txtopbal.Text = opbal.ToString("N2") + " Cr.";
                            DC = "C";
                        }


                        #endregion
                        #region
                        //    DataTable SPdt = conn.getdataset("select * from Ledger where DC='D' and isactive=1 and Date1<= '" + DTPFrom.Text + "' and Accountid='" + dt.Rows[i]["ClientID"].ToString() + "' order by Date1");
                        DataTable SPdt = conn.getdataset("select * from Ledger where  isactive=1 and Accountid='" + dt.Rows[i]["ClientID"].ToString() + "' order by Date1", scon);
                        string balance = "0.00";
                        Double debit = 0, credit = 0;
                        if (SPdt.Rows.Count > 0)
                        {
                            for (int j = 0; j < SPdt.Rows.Count; j++)
                            {
                                if (SPdt.Rows[j]["TranType"].ToString() == "EXPCash Recept")
                                {
                                    if (j != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                }
                                else if (SPdt.Rows[j]["TranType"].ToString() == "EXPCash Invoice")
                                {

                                    if (j != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);

                                }
                                else if (SPdt.Rows[j]["TranType"].ToString() == "GSTVOUCHEREXP")
                                {
                                    if (SPdt.Rows[j]["dc"].ToString() == "D")
                                    {
                                        if (j != 0)
                                        {
                                            string[] str = balance.Split(' ');
                                            char temp = str[1][0];
                                            DC = temp.ToString();

                                            opbal = Convert.ToDouble(str[0]);
                                        }

                                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);

                                    }
                                    else
                                    {
                                        if (j != 0)
                                        {
                                            string[] str = balance.Split(' ');
                                            char temp = str[1][0];
                                            DC = temp.ToString();

                                            opbal = Convert.ToDouble(str[0]);
                                        }

                                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);

                                    }
                                }
                                if (SPdt.Rows[j]["TranType"].ToString() == "GSTVOUCHERS")
                                {
                                    if (j != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                }
                                else if (SPdt.Rows[j]["TranType"].ToString() == "GSTVOUCHERSR")
                                {
                                    if (j != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                }
                                else if (SPdt.Rows[j]["TranType"].ToString() == "GSTVOUCHERP")
                                {
                                    if (j != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                }
                                else if (SPdt.Rows[j]["TranType"].ToString() == "GSTVOUCHERPR")
                                {
                                    if (j != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                }
                                else if (SPdt.Rows[j]["TranType"].ToString() == "GSTVOUCHERDN")
                                {
                                    if (j != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                }
                                else if (SPdt.Rows[j]["TranType"].ToString() == "GSTVOUCHERCN")
                                {
                                    if (j != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                }
                                else if (SPdt.Rows[j]["TranType"].ToString() == "SaleCash Recept")
                                {
                                    if (j != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);

                                }
                                else if (SPdt.Rows[j]["TranType"].ToString() == "SRCash Invoice")
                                {
                                    if (j != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);

                                }
                                else if (SPdt.Rows[j]["TranType"].ToString() == "PurchaseCash Invoice")
                                {
                                    if (j != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);

                                }
                                else if (SPdt.Rows[j]["TranType"].ToString() == "PRCash Recept")
                                {
                                    if (j != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);

                                }
                                else if (SPdt.Rows[j]["TranType"].ToString() == "DNCash Recept")
                                {
                                    if (j != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);

                                }
                                else if (SPdt.Rows[j]["TranType"].ToString() == "CNCash Invoice")
                                {
                                    if (j != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                }
                                else if (SPdt.Rows[j]["TranType"].ToString() == "Sale")
                                {
                                    //ListViewItem li;
                                    //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                    //li.SubItems.Add("Sale");
                                    //li.SubItems.Add("Sales");
                                    //li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                                    //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    //debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                    //li.SubItems.Add("");
                                    //mouseclickid.Rows.Add("Sale", SPdt.Rows[i]["VoucherID"].ToString());
                                    if (j != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                    //  li.SubItems.Add(balance);
                                }
                                else if (SPdt.Rows[j]["TranType"].ToString() == "Cash Recept")
                                {
                                    if (j != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                }
                                else if (SPdt.Rows[j]["TranType"].ToString() == "Cash Invoice")
                                {
                                    if (j != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                }
                                else if (SPdt.Rows[j]["TranType"].ToString() == "Rect")
                                {
                                    //ListViewItem li;
                                    //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                    //li.SubItems.Add("Rect");
                                    //li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                                    //li.SubItems.Add("By Rcpt. No.: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["ShortNarration"].ToString());
                                    //li.SubItems.Add("");
                                    //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    //credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                    //mouseclickid.Rows.Add("Rect", SPdt.Rows[i]["VoucherID"].ToString());
                                    if (j != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                    //li.SubItems.Add(balance);
                                }
                                else if (SPdt.Rows[j]["TranType"].ToString() == "SaleReturn")
                                {
                                    //ListViewItem li;
                                    //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                    //li.SubItems.Add("Sale Return");
                                    //li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                                    //li.SubItems.Add("Return Bill No: " + SPdt.Rows[i]["VoucherID"].ToString());
                                    //li.SubItems.Add("");
                                    //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    //credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                    //mouseclickid.Rows.Add("Sale Return", SPdt.Rows[i]["VoucherID"].ToString());
                                    if (j != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                    // li.SubItems.Add(balance);
                                }
                                else if (SPdt.Rows[j]["TranType"].ToString() == "Purchase")
                                {
                                    //ListViewItem li;
                                    //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                    //li.SubItems.Add("Purchase");
                                    //li.SubItems.Add("Purchases");
                                    //li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                                    //li.SubItems.Add("");
                                    //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    //credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                    //mouseclickid.Rows.Add("Purchase", SPdt.Rows[i]["VoucherID"].ToString());
                                    if (j != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                    // li.SubItems.Add(balance);
                                }
                                else if (SPdt.Rows[j]["TranType"].ToString() == "PurchaseReturn")
                                {
                                    //ListViewItem li;
                                    //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                    //li.SubItems.Add("Purchase Return");
                                    //li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                                    //li.SubItems.Add("Return Bill No: " + SPdt.Rows[i]["VoucherID"].ToString());

                                    //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    //li.SubItems.Add("");
                                    //credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                    //mouseclickid.Rows.Add("Purchase Return", SPdt.Rows[i]["VoucherID"].ToString());
                                    if (j != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();

                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    //if (DC == "C")
                                    //{
                                    //    DC = "D";
                                    //}
                                    //else
                                    //{
                                    //    DC = "C";
                                    //}
                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                    //  li.SubItems.Add(balance);
                                }
                                else if (SPdt.Rows[j]["TranType"].ToString() == "Pmnt")
                                {
                                    //ListViewItem li;
                                    //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                    //li.SubItems.Add("Pmnt");
                                    //li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                                    //li.SubItems.Add("By Rcpt. No.: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["ShortNarration"].ToString());
                                    //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    //debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                    //li.SubItems.Add("");
                                    //mouseclickid.Rows.Add("Pmnt", SPdt.Rows[i]["VoucherID"].ToString());
                                    if (j != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                    // li.SubItems.Add(balance);
                                }
                                else if (SPdt.Rows[j]["TranType"].ToString() == "Cheque Issued")
                                {
                                    if (j != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);

                                }
                                else if (SPdt.Rows[j]["TranType"].ToString() == "Draft Issued")
                                {
                                    if (j != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["OT3"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);

                                }
                                else if (SPdt.Rows[j]["TranType"].ToString() == "Cheque/Draft/Rtgs Received")
                                {
                                    if (j != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                }
                                else if (SPdt.Rows[j]["TranType"].ToString() == "Deposit Cash Into Bank")
                                {
                                    if (j != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);

                                }
                                else if (SPdt.Rows[j]["TranType"].ToString() == "Withdraw Cash from Bank")
                                {
                                    if (j != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);

                                }
                                else if (SPdt.Rows[j]["TranType"].ToString() == "Bank Expenses")
                                {
                                    if (j != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                }
                                else if (SPdt.Rows[j]["TranType"].ToString() == "Online Transfer")
                                {
                                    if (j != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                }
                                else if (SPdt.Rows[j]["TranType"].ToString() == "DEBIT NOTE")
                                {
                                    if (j != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                }
                                else if (SPdt.Rows[j]["TranType"].ToString() == "CREDIT NOTE")
                                {
                                    if (j != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                }

                            }

                            temptable.Rows.Add(dt.Rows[i]["AccountName"].ToString(), balance);

                            //ListViewItem li;
                            //li = LVclient.Items.Add("");
                            //li.SubItems.Add(dt.Rows[i]["AccountName"].ToString());
                            //li.SubItems.Add(dt.Rows[i]["Mobile"].ToString());
                            //li.SubItems.Add(dt.Rows[i]["Email"].ToString());
                            //li.SubItems.Add(balance);
                            //String withoutLast = balance.Substring(0, (balance.Length - 3));
                            //Double totalnet = +Convert.ToDouble(withoutLast);
                            //Double d1t = Convert.ToDouble(txtnetamt.Text) + totalnet;
                            //txtnetamt.Text = d1t.ToString("N2");

                        }
                        else
                        {
                            temptable.Rows.Add(dt.Rows[i]["AccountName"].ToString(), onlyopbal);
                        }

                        #endregion
                    }
                    txttotaldr.Text = "0";
                    txttotalcr.Text = "0";
                    for (int p = 0; p < temptable.Rows.Count; p++)
                    {
                        string balance1 = temptable.Rows[p]["balance"].ToString();
                        var result = balance1.Substring(balance1.Length - 3);
                        String withoutLast1 = balance1.Substring(0, (balance1.Length - 3));
                        Double d = Convert.ToDouble(withoutLast1);
                      //  if (Convert.ToInt32(d) > 0)
                     //   {
                            if (result == "Dr.")
                            {
                                string bal = temptable.Rows[p]["balance"].ToString();
                                String withoutLast = bal.Substring(0, (bal.Length - 3));
                                temptable1.Rows.Add(temptable.Rows[p]["AccountName"].ToString(), withoutLast, "0");
                                Double wa = Convert.ToDouble(withoutLast);
                                //ListViewItem li;
                                //li = LVclient.Items.Add(temptable.Rows[p]["AccountName"].ToString());
                                //li.SubItems.Add(temptable.Rows[p]["balance"].ToString());
                                //li.SubItems.Add("0");
                                //string bal = temptable.Rows[p]["balance"].ToString();
                                //String withoutLast = bal.Substring(0, (bal.Length - 3));
                                Double totalnet = +Convert.ToDouble(withoutLast);
                                Double d1t = Convert.ToDouble(txttotaldr.Text) + totalnet;
                                txttotaldr.Text = d1t.ToString("N2");
                              //  conn.execute("update ClientMaster set Opbal='" + wa + "',Dr_cr='" + "Dr." + "' where AccountName='" + temptable.Rows[p]["AccountName"].ToString() + "'");
                            }
                            else
                            {
                                string bal = temptable.Rows[p]["balance"].ToString();
                                String withoutLast = bal.Substring(0, (bal.Length - 3));
                                Double wa = Convert.ToDouble(withoutLast);
                                Double totalnet = +Convert.ToDouble(withoutLast);
                                Double d1t = Convert.ToDouble(txttotalcr.Text) + totalnet;
                                txttotalcr.Text = d1t.ToString("N2");
                                temptable1.Rows.Add(temptable.Rows[p]["AccountName"].ToString(), "0", withoutLast);
                                //ListViewItem li;
                                //li = LVclient.Items.Add(temptable.Rows[p]["AccountName"].ToString());
                                //li.SubItems.Add("0");
                                //li.SubItems.Add(temptable.Rows[p]["balance"].ToString());
                              //  conn.execute("update ClientMaster set Opbal='" + wa + "',Dr_cr='" + "Cr." + "' where AccountName='" + temptable.Rows[p]["AccountName"].ToString() + "'");

                            }
                    //    }
                    }
                }
                grdstock.DataSource = temptable1;
                grdstock.Columns[0].Width = 400;
                grdstock.Columns[1].Width = 200;
                grdstock.Columns[2].Width = 200;
                grdstock.Columns[0].ReadOnly = true;
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
        DataTable temptable = new DataTable();
        DataTable temptable1 = new DataTable();
        private void OpBalanceEditor_Load(object sender, EventArgs e)
        {
            temptable = new DataTable();
            temptable.Columns.Add("AccountName", typeof(string));
            temptable.Columns.Add("Balance", typeof(string));
            temptable1 = new DataTable();
            temptable1.Columns.Add("AccountName", typeof(string));
            temptable1.Columns.Add("Dr Balance", typeof(string));
            temptable1.Columns.Add("Cr Balance", typeof(string));
            // binddata();
            DataTable dt = new DataTable();
            dt = conn.getdataset("select * from ClientMaster where isactive=1  order by AccountName asc");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Dr_cr"].ToString() == "Dr.")
                    {
                        temptable1.Rows.Add(dt.Rows[i]["AccountName"].ToString(), dt.Rows[i]["Opbal"].ToString(), "0");
                        string bal = dt.Rows[i]["Opbal"].ToString();
                        String withoutLast = bal.Substring(0, (bal.Length - 3));
                        Double totalnet = +Convert.ToDouble(withoutLast);
                        Double d1t = Convert.ToDouble(txttotaldr.Text) + totalnet;
                        txttotaldr.Text = d1t.ToString("N2");
                    }
                    else
                    {
                        temptable1.Rows.Add(dt.Rows[i]["AccountName"].ToString(), "0", dt.Rows[i]["Opbal"].ToString());
                        string bal = dt.Rows[i]["Opbal"].ToString();
                        String withoutLast = bal.Substring(0, (bal.Length - 3));
                        Double totalnet = +Convert.ToDouble(withoutLast);
                        Double d1t = Convert.ToDouble(txttotalcr.Text) + totalnet;
                        txttotalcr.Text = d1t.ToString("N2");
                    }
                }
            }

            grdstock.DataSource = temptable1;
            grdstock.Columns[0].Width = 400;
            grdstock.Columns[1].Width = 200;
            grdstock.Columns[2].Width = 200;
            grdstock.Columns[0].ReadOnly = true;
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }
        Double cr =0, dr = 0;
        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                if (grdstock.Rows.Count > 0)
                {
                    for (int i = 0; i < grdstock.Rows.Count; i++)
                    {
                        if (string.IsNullOrEmpty(Convert.ToString(grdstock.Rows[i].Cells[1].Value)))
                        {
                            dr = 0;
                        }
                        else
                        {
                            dr = Convert.ToDouble(grdstock.Rows[i].Cells[1].Value);
                        }
                        if (string.IsNullOrEmpty(Convert.ToString(grdstock.Rows[i].Cells[2].Value)))
                        {
                            cr = 0;
                        }
                        else
                        {
                            cr = Convert.ToDouble(grdstock.Rows[i].Cells[2].Value);
                        }
                        if (dr == 0)
                        {
                            conn.execute("update ClientMaster set Opbal='" + cr + "',Dr_cr='" + "Cr." + "' where AccountName='" + grdstock.Rows[i].Cells[0].Value + "'");
                        }
                        else
                        {
                            conn.execute("update ClientMaster set Opbal='" + dr + "',Dr_cr='" + "Dr." + "' where AccountName='" + grdstock.Rows[i].Cells[0].Value + "'");
                        }
                    }
                    MessageBox.Show("Opening Balance Update Successfully");
                }
            }
            catch
            {
            }
            finally
            {
                this.Enabled = true;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                binddata();
            }
            catch
            {
            }
        }
    }
}
