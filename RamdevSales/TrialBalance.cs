using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace RamdevSales
{
    public partial class TrialBalance : Form
    {
        private Master master;
        private TabControl tabControl;
        OleDbSettings ods = new OleDbSettings();
        Connection conn = new Connection();
        Printing prn = new Printing();
        DataTable temptable = new DataTable();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        public TrialBalance()
        {
            InitializeComponent();
        }

        public TrialBalance(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
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
        private void TrialBalance_Load(object sender, EventArgs e)
        {
            try
            {
                LVclient.Columns.Add("Account Head", 400, HorizontalAlignment.Left);
                LVclient.Columns.Add("Dr. Amt.", 200, HorizontalAlignment.Center);
                LVclient.Columns.Add("Cr. Amt.", 200, HorizontalAlignment.Center);
                DTPFrom.CustomFormat = Master.dateformate;
            }
            catch
            {
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
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
        public void binddata()
        {
            string onlyopbal = "";
            LVclient.Items.Clear();
            DataTable dt = new DataTable();
            dt = conn.getdataset("select * from ClientMaster where isactive=1 and (GroupName='CUSTOMERS' or GroupName='SUPPLIERS' or GroupName='Sundry Debtors' or GroupName='Sundry Creditors')   order by AccountName asc");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    #region
                    string totaldebit = conn.ExecuteScalar("select sum(amount) from Ledger where Date1 < '" + DTPFrom.Text + "' and Accountid='" + dt.Rows[i]["ClientID"].ToString() + "' and dc='D' and isactive=1 order by Date1");
                    string totalcredit = conn.ExecuteScalar("select sum(amount) from Ledger where Date1 < '" + DTPFrom.Text + "' and Accountid='" + dt.Rows[i]["ClientID"].ToString() + "' and dc='C' and isactive=1 order by Date1");
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
                    DataTable opbalance = conn.getdataset("select * from ClientMaster where isactive=1 and ClientID='" + dt.Rows[i]["ClientID"].ToString()+"'");
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
                    DataTable SPdt = conn.getdataset("select * from Ledger where  isactive=1 and Date1<= '" + DTPFrom.Text + "' and Accountid='" + dt.Rows[i]["ClientID"].ToString() + "' order by Date1");
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
                        temptable.Rows.Add(dt.Rows[i]["AccountName"].ToString(),onlyopbal);
                    }

                    #endregion
                }
                for (int p = 0; p < temptable.Rows.Count; p++)
                {
                    string balance1 = temptable.Rows[p]["balance"].ToString();
                    var result = balance1.Substring(balance1.Length - 3);
                    String withoutLast1 = balance1.Substring(0, (balance1.Length - 3));
                    Double d = Convert.ToDouble(withoutLast1);
                    if (Convert.ToInt32(d) > 0)
                    {
                        if (result == "Dr.")
                        {
                            ListViewItem li;
                            li = LVclient.Items.Add(temptable.Rows[p]["AccountName"].ToString());
                            li.SubItems.Add(temptable.Rows[p]["balance"].ToString());
                            li.SubItems.Add("0");
                            string bal = temptable.Rows[p]["balance"].ToString();
                            String withoutLast = bal.Substring(0, (bal.Length - 3));
                            Double totalnet = +Convert.ToDouble(withoutLast);
                            Double d1t = Convert.ToDouble(txttotaldr.Text) + totalnet;
                            txttotaldr.Text = d1t.ToString("N2");
                        }
                        else
                        {
                            ListViewItem li;
                            li = LVclient.Items.Add(temptable.Rows[p]["AccountName"].ToString());
                            li.SubItems.Add("0");
                            li.SubItems.Add(temptable.Rows[p]["balance"].ToString());
                            string bal = temptable.Rows[p]["balance"].ToString();
                            String withoutLast = bal.Substring(0, (bal.Length - 3));
                            Double totalnet = +Convert.ToDouble(withoutLast);
                            Double d1t = Convert.ToDouble(txttotalcr.Text) + totalnet;
                            txttotalcr.Text = d1t.ToString("N2");
                        }
                    }
                }
            }
        }
        private void btnok_Click(object sender, EventArgs e)
        {
            txttotaldr.Text = "0";
            txttotalcr.Text = "0";
            temptable = new DataTable();
            temptable.Columns.Add("AccountName", typeof(string));
            temptable.Columns.Add("balance", typeof(string));
            binddata();
        }

        private void DTPFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnok.Focus();
            }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                prn.execute("delete from printing");
                string status;
                status = "CLOSING TRIAL BALANCE AS ON " + DTPFrom.Text;

                string[] a = new string[LVclient.CheckedItems.Count];
                string[] b = new string[LVclient.CheckedItems.Count];
                string[] c = new string[LVclient.CheckedItems.Count];
                string[] d = new string[LVclient.CheckedItems.Count];
                for (int i = 0; i < LVclient.Items.Count; i++)
                {
                    string Account = LVclient.Items[i].SubItems[0].Text;
                    string balance = LVclient.Items[i].SubItems[1].Text;
                    string balance1 = LVclient.Items[i].SubItems[2].Text;
                    //String withoutLast = balance.Substring(0, (balance.Length - 3));
                    //  totalnet = +totalnet + Convert.ToDouble(withoutLast);
                    // Double d1t = Convert.ToDouble(txtnetamt.Text) + totalnet;
                    DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1 and CompanyID='" + Master.companyId + "' ");
                    string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17)VALUES";
                    qry += "('" + Account + "','" + balance + "','" + dt1.Rows[0]["CompanyName"].ToString() + "','" + dt1.Rows[0]["SubName"].ToString() + "','" + dt1.Rows[0]["Address"].ToString() + "','" + dt1.Rows[0]["Address2"].ToString() + "','" + dt1.Rows[0]["City"].ToString() + "','" + dt1.Rows[0]["State"].ToString() + "','" + dt1.Rows[0]["Country"].ToString() + "','" + dt1.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Mobile"].ToString() + "','" + dt1.Rows[0]["Email"].ToString() + "','" + dt1.Rows[0]["CSTNo"].ToString() + "','" + status + "','" + txttotaldr.Text + "','" + txttotalcr.Text + "','" + balance1 + "')";
                    prn.execute(qry);
                }
                //  string update = "UPDATE [Printing] SET [T17]='" + totalnet + "'";
                //prn.execute(update);
                //totalnet = 0;
                Print popup = new Print("Trialbalance");
                popup.ShowDialog();
                popup.Dispose();
            }
            catch
            {
            }
        }

        private void btnok_Enter(object sender, EventArgs e)
        {
            btnok.UseVisualStyleBackColor = false;
            btnok.BackColor = System.Drawing.Color.FromArgb(94, 191, 174);
            btnok.ForeColor = System.Drawing.Color.White;
        }

        private void btnok_Leave(object sender, EventArgs e)
        {
            btnok.UseVisualStyleBackColor = true;
            btnok.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnok.ForeColor = System.Drawing.Color.White;
        }

        private void btnok_MouseEnter(object sender, EventArgs e)
        {
            btnok.UseVisualStyleBackColor = false;
            btnok.BackColor = System.Drawing.Color.FromArgb(94, 191, 174);
            btnok.ForeColor = System.Drawing.Color.White;
        }

        private void btnok_MouseLeave(object sender, EventArgs e)
        {
            btnok.UseVisualStyleBackColor = true;
            btnok.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnok.ForeColor = System.Drawing.Color.White;
        }

        private void btnprint_Enter(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = false;
            btnprint.BackColor = Color.FromArgb(176, 111, 193);
            btnprint.ForeColor = Color.White;
        }

        private void btnprint_Leave(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = true;
            btnprint.BackColor = Color.FromArgb(51, 153, 255);
            btnprint.ForeColor = Color.White;
        }

        private void btnprint_MouseEnter(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = false;
            btnprint.BackColor = Color.FromArgb(176, 111, 193);
            btnprint.ForeColor = Color.White;
        }

        private void btnprint_MouseLeave(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = true;
            btnprint.BackColor = Color.FromArgb(51, 153, 255);
            btnprint.ForeColor = Color.White;
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
        public void open()
        {
            try
            {
                this.Enabled = false;
                string iid = LVclient.Items[LVclient.FocusedItem.Index].SubItems[0].Text;
                Ledger le = new Ledger(master, tabControl);
                DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
                DataTable daterange = dtrange.Tables[0];
                //   DTPFrom.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                // DTPFrom.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                // DTPFrom.Value = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                string stadate = Convert.ToDateTime(daterange.Rows[0]["Startdate"].ToString()).ToString(Master.dateformate);
                string curdate = DateTime.Now.ToString(Master.dateformate);
                le.Updateoutstanding(1, iid, stadate, curdate);
                master.AddNewTab(le);
            }
            catch
            {
            }
            finally
            {
                this.Enabled = true;
            }
        }

        private void LVclient_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            open();
        }

        private void LVclient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                open();
            }
        }
    }
}
