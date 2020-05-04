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
using LoggingFramework;

namespace RamdevSales
{
    public partial class outstandinganalysis : Form
    {
        private Master master;
        private TabControl tabControl;
        OleDbSettings ods = new OleDbSettings();
        Connection conn = new Connection();
        Double opbal;
        string DC = "";
        int flag = 0;
        Printing prn = new Printing();
        public String Account = string.Empty;
        public String mobile = string.Empty;
        public String email = string.Empty;
        public String balance = string.Empty;
        public String todaybalance = string.Empty;
        DataTable temptable = new DataTable();
        DataTable payable = new DataTable();
        DataTable Receivable = new DataTable();
        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        public outstandinganalysis()
        {
            InitializeComponent();
        }

        public outstandinganalysis(Master master, TabControl tabControl)
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
            //LVclient.CheckBoxes = true;
            //LVclient.Columns.Add("", 20, HorizontalAlignment.Left);
            //LVclient.Columns.Add("Account Head", 200, HorizontalAlignment.Center);
            //LVclient.Columns.Add("Mobile", 150, HorizontalAlignment.Center);
            //LVclient.Columns.Add("E-mail", 150, HorizontalAlignment.Center);
            //LVclient.Columns.Add("Balance", 150, HorizontalAlignment.Center);
            DataTable dt = new DataTable();
            dt = conn.getdataset("select * from ClientMaster where isactive=1 and (GroupName='CUSTOMERS' or GroupName='SUPPLIERS' or GroupName='Sundry Debtors' or GroupName='Sundry Creditors')   order by AccountName asc");
            string str1 = "select * from ClientMaster where isactive=1 and (GroupName='CUSTOMERS' or GroupName='SUPPLIERS' or GroupName='Sundry Debtors' or GroupName='Sundry Creditors')   order by AccountName asc";
            LogGenerator.Info(str1);
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
                    DataTable opbalance = conn.getdataset("select * from ClientMaster where isactive=1 and ClientID='" + dt.Rows[i]["ClientID"].ToString() + "'");
                    string str2 = "select * from ClientMaster where isactive=1 and ClientID='" + dt.Rows[i]["ClientID"].ToString() + "'";
                    LogGenerator.Info(str2);
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

                    //for due amount
                    #region
                    //  DataTable SPdt1 = conn.getdataset("select * from Ledger where  isactive=1 and Date1<= '" + DTPFrom.Text + "' and Accountid='" + dt.Rows[i]["ClientID"].ToString() + "' order by Date1");

                    ////  string balance = "0.00";
                    //  string todaysbalance1 = "0.00";
                    //  Double debit1 = 0, credit1 = 0;
                    //  if (SPdt1.Rows.Count > 0)
                    //  {
                    //      for (int j = 0; j < SPdt1.Rows.Count; j++)
                    //      {
                    //          if (SPdt1.Rows[j]["TranType"].ToString() == "Sale")
                    //          {



                    //                  if (j != 0)
                    //                  {

                    //                      string[] str = todaysbalance1.Split(' ');
                    //                      char temp = str[1][0];
                    //                      DC = temp.ToString();
                    //                      opbal = Convert.ToDouble(str[0]);
                    //                  }
                    //                  todaysbalance1 = getbalance(opbal, Convert.ToDouble(SPdt1.Rows[j]["Amount"].ToString()), DC, SPdt1.Rows[j]["dc"].ToString(), i);

                    //          }
                    //          else if (SPdt1.Rows[j]["TranType"].ToString() == "Cash Recept")
                    //          {
                    //              if (j != 0)
                    //              {
                    //                  string[] str = todaysbalance1.Split(' ');
                    //                  char temp = str[1][0];
                    //                  DC = temp.ToString();
                    //                  opbal = Convert.ToDouble(str[0]);
                    //              }
                    //              todaysbalance1 = getbalance(opbal, Convert.ToDouble(SPdt1.Rows[j]["Amount"].ToString()), DC, SPdt1.Rows[j]["dc"].ToString(), i);
                    //          }
                    //          else if (SPdt1.Rows[j]["TranType"].ToString() == "Cash Invoice")
                    //          {
                    //              if (j != 0)
                    //              {
                    //                  string[] str = todaysbalance1.Split(' ');
                    //                  char temp = str[1][0];
                    //                  DC = temp.ToString();
                    //                  opbal = Convert.ToDouble(str[0]);
                    //              }
                    //              todaysbalance1 = getbalance(opbal, Convert.ToDouble(SPdt1.Rows[j]["Amount"].ToString()), DC, SPdt1.Rows[j]["dc"].ToString(), i);
                    //          }
                    //          else if (SPdt1.Rows[j]["TranType"].ToString() == "Rect")
                    //          {
                    //              if (j != 0)
                    //              {
                    //                  string[] str = todaysbalance1.Split(' ');
                    //                  char temp = str[1][0];
                    //                  DC = temp.ToString();
                    //                  opbal = Convert.ToDouble(str[0]);
                    //              }
                    //              todaysbalance1 = getbalance(opbal, Convert.ToDouble(SPdt1.Rows[j]["Amount"].ToString()), DC, SPdt1.Rows[j]["dc"].ToString(), i);
                    //          }
                    //          else if (SPdt1.Rows[j]["TranType"].ToString() == "SaleReturn")
                    //          {
                    //              if (j != 0)
                    //              {
                    //                  string[] str = todaysbalance1.Split(' ');
                    //                  char temp = str[1][0];
                    //                  DC = temp.ToString();
                    //                  opbal = Convert.ToDouble(str[0]);
                    //              }
                    //              todaysbalance1 = getbalance(opbal, Convert.ToDouble(SPdt1.Rows[j]["Amount"].ToString()), DC, SPdt1.Rows[j]["dc"].ToString(), i);
                    //          }
                    //          else if (SPdt1.Rows[j]["TranType"].ToString() == "Purchase")
                    //          {
                    //              if (j != 0)
                    //              {
                    //                  string[] str = todaysbalance1.Split(' ');
                    //                  char temp = str[1][0];
                    //                  DC = temp.ToString();
                    //                  opbal = Convert.ToDouble(str[0]);
                    //              }
                    //              todaysbalance1 = getbalance(opbal, Convert.ToDouble(SPdt1.Rows[j]["Amount"].ToString()), DC, SPdt1.Rows[j]["dc"].ToString(), i);
                    //          }
                    //          else if (SPdt1.Rows[j]["TranType"].ToString() == "PurchaseReturn")
                    //          {
                    //              if (j != 0)
                    //              {
                    //                  string[] str = todaysbalance1.Split(' ');
                    //                  char temp = str[1][0];
                    //                  DC = temp.ToString();

                    //                  opbal = Convert.ToDouble(str[0]);
                    //              }
                    //              if (DC == "C")
                    //              {
                    //                  DC = "D";
                    //              }
                    //              else
                    //              {
                    //                  DC = "C";
                    //              }
                    //              todaysbalance1 = getbalance(opbal, Convert.ToDouble(SPdt1.Rows[j]["Amount"].ToString()), DC, SPdt1.Rows[j]["dc"].ToString(), i);
                    //          }
                    //          else if (SPdt1.Rows[j]["TranType"].ToString() == "Pmnt")
                    //          {
                    //              if (j != 0)
                    //              {
                    //                  string[] str = todaysbalance1.Split(' ');
                    //                  char temp = str[1][0];
                    //                  DC = temp.ToString();
                    //                  opbal = Convert.ToDouble(str[0]);
                    //              }
                    //              todaysbalance1 = getbalance(opbal, Convert.ToDouble(SPdt1.Rows[j]["Amount"].ToString()), DC, SPdt1.Rows[j]["dc"].ToString(), i);
                    //          }
                    //          else if (SPdt1.Rows[j]["TranType"].ToString() == "Cheque Issued")
                    //          {
                    //              if (j != 0)
                    //              {
                    //                  string[] str = todaysbalance1.Split(' ');
                    //                  char temp = str[1][0];
                    //                  DC = temp.ToString();
                    //                  opbal = Convert.ToDouble(str[0]);
                    //              }
                    //              todaysbalance1 = getbalance(opbal, Convert.ToDouble(SPdt1.Rows[j]["Amount"].ToString()), DC, SPdt1.Rows[j]["dc"].ToString(), i);

                    //          }
                    //          else if (SPdt1.Rows[j]["TranType"].ToString() == "Draft Issued")
                    //          {
                    //              if (j != 0)
                    //              {
                    //                  string[] str = todaysbalance1.Split(' ');
                    //                  char temp = str[1][0];
                    //                  DC = temp.ToString();
                    //                  opbal = Convert.ToDouble(str[0]);
                    //              }
                    //              todaysbalance1 = getbalance(opbal, Convert.ToDouble(SPdt1.Rows[j]["Amount"].ToString()), DC, SPdt1.Rows[j]["dc"].ToString(), i);

                    //          }
                    //          else if (SPdt1.Rows[j]["TranType"].ToString() == "Cheque/Draft/Rtgs Received")
                    //          {
                    //              if (j != 0)
                    //              {
                    //                  string[] str = todaysbalance1.Split(' ');
                    //                  char temp = str[1][0];
                    //                  DC = temp.ToString();
                    //                  opbal = Convert.ToDouble(str[0]);
                    //              }
                    //              todaysbalance1 = getbalance(opbal, Convert.ToDouble(SPdt1.Rows[j]["Amount"].ToString()), DC, SPdt1.Rows[j]["dc"].ToString(), i);
                    //          }
                    //          else if (SPdt1.Rows[j]["TranType"].ToString() == "Deposit Cash Into Bank")
                    //          {
                    //              if (j != 0)
                    //              {
                    //                  string[] str = todaysbalance1.Split(' ');
                    //                  char temp = str[1][0];
                    //                  DC = temp.ToString();
                    //                  opbal = Convert.ToDouble(str[0]);
                    //              }
                    //              todaysbalance1 = getbalance(opbal, Convert.ToDouble(SPdt1.Rows[j]["Amount"].ToString()), DC, SPdt1.Rows[j]["dc"].ToString(), i);

                    //          }
                    //          else if (SPdt1.Rows[j]["TranType"].ToString() == "Withdraw Cash from Bank")
                    //          {
                    //              if (j != 0)
                    //              {
                    //                  string[] str = todaysbalance1.Split(' ');
                    //                  char temp = str[1][0];
                    //                  DC = temp.ToString();
                    //                  opbal = Convert.ToDouble(str[0]);
                    //              }
                    //              todaysbalance1 = getbalance(opbal, Convert.ToDouble(SPdt1.Rows[j]["Amount"].ToString()), DC, SPdt1.Rows[j]["dc"].ToString(), i);

                    //          }
                    //          else if (SPdt1.Rows[j]["TranType"].ToString() == "Bank Expenses")
                    //          {
                    //              if (j != 0)
                    //              {
                    //                  string[] str = todaysbalance1.Split(' ');
                    //                  char temp = str[1][0];
                    //                  DC = temp.ToString();
                    //                  opbal = Convert.ToDouble(str[0]);
                    //              }
                    //              todaysbalance1 = getbalance(opbal, Convert.ToDouble(SPdt1.Rows[j]["Amount"].ToString()), DC, SPdt1.Rows[j]["dc"].ToString(), i);
                    //          }
                    //          else if (SPdt1.Rows[j]["TranType"].ToString() == "Online Transfer")
                    //          {
                    //              if (j != 0)
                    //              {
                    //                  string[] str = todaysbalance1.Split(' ');
                    //                  char temp = str[1][0];
                    //                  DC = temp.ToString();
                    //                  opbal = Convert.ToDouble(str[0]);
                    //              }
                    //              todaysbalance1 = getbalance(opbal, Convert.ToDouble(SPdt1.Rows[j]["Amount"].ToString()), DC, SPdt1.Rows[j]["dc"].ToString(), i);
                    //          }
                    //          else if (SPdt1.Rows[j]["TranType"].ToString() == "DEBIT NOTE")
                    //          {
                    //              if (j != 0)
                    //              {
                    //                  string[] str = todaysbalance1.Split(' ');
                    //                  char temp = str[1][0];
                    //                  DC = temp.ToString();
                    //                  opbal = Convert.ToDouble(str[0]);
                    //              }
                    //              todaysbalance1 = getbalance(opbal, Convert.ToDouble(SPdt1.Rows[j]["Amount"].ToString()), DC, SPdt1.Rows[j]["dc"].ToString(), i);
                    //          }
                    //          else if (SPdt1.Rows[j]["TranType"].ToString() == "CREDIT NOTE")
                    //          {
                    //              if (j != 0)
                    //              {
                    //                  string[] str = todaysbalance1.Split(' ');
                    //                  char temp = str[1][0];
                    //                  DC = temp.ToString();
                    //                  opbal = Convert.ToDouble(str[0]);
                    //              }
                    //              todaysbalance1 = getbalance(opbal, Convert.ToDouble(SPdt1.Rows[j]["Amount"].ToString()), DC, SPdt1.Rows[j]["dc"].ToString(), i);
                    //          }



                    //      }

                    //      temptable.Rows.Add("", dt.Rows[i]["AccountName"].ToString(), dt.Rows[i]["Mobile"].ToString(), dt.Rows[i]["Email"].ToString(), 0, todaysbalance1);
                    //  }
                    #endregion

                    #region
                    DataTable SPdt = conn.getdataset("select * from Ledger where  isactive=1 and Date1<= '" + DTPFrom.Text + "' and Accountid='" + dt.Rows[i]["ClientID"].ToString() + "' order by Date1");

                    string balance = "0.00 Cr.";
                    string todaysbalance = "0.00 Cr.";
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
                                if (j != 0)
                                {
                                    string[] str = todaysbalance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
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
                                if (j != 0)
                                {
                                    string[] str = todaysbalance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);

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
                                    DateTime current = DTPFrom.Value;
                                    string curr = current.ToString(Master.dateformate);
                                    DateTime curr1 = Convert.ToDateTime(curr);
                                    DateTime duedate = Convert.ToDateTime(SPdt.Rows[j]["OD1"].ToString());
                                    string due = duedate.ToString(Master.dateformate);
                                    DateTime due1 = Convert.ToDateTime(due);
                                    if (curr1 >= due1)
                                    {
                                        if (j != 0)
                                        {
                                            string[] str = todaysbalance.Split(' ');
                                            char temp = str[1][0];
                                            DC = temp.ToString();
                                            opbal = Convert.ToDouble(str[0]);
                                        }
                                        todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                    }

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
                                    DateTime current = DTPFrom.Value;
                                    string curr = current.ToString(Master.dateformate);
                                    DateTime curr1 = Convert.ToDateTime(curr);
                                    DateTime duedate = Convert.ToDateTime(SPdt.Rows[j]["OD1"].ToString());
                                    string due = duedate.ToString(Master.dateformate);
                                    DateTime due1 = Convert.ToDateTime(due);
                                    if (curr1 >= due1)
                                    {
                                        if (j != 0)
                                        {
                                            string[] str = todaysbalance.Split(' ');
                                            char temp = str[1][0];
                                            DC = temp.ToString();
                                            opbal = Convert.ToDouble(str[0]);
                                        }
                                        todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                    }

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
                                DateTime current = DTPFrom.Value;
                                string curr = current.ToString(Master.dateformate);
                                DateTime curr1 = Convert.ToDateTime(curr);
                                DateTime duedate = Convert.ToDateTime(SPdt.Rows[j]["OD1"].ToString());
                                string due = duedate.ToString(Master.dateformate);
                                DateTime due1 = Convert.ToDateTime(due);
                                if (curr1 >= due1)
                                {
                                    if (j != 0)
                                    {
                                        string[] str = todaysbalance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                }
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
                                DateTime current = DTPFrom.Value;
                                string curr = current.ToString(Master.dateformate);
                                DateTime curr1 = Convert.ToDateTime(curr);
                                DateTime duedate = Convert.ToDateTime(SPdt.Rows[j]["OD1"].ToString());
                                string due = duedate.ToString(Master.dateformate);
                                DateTime due1 = Convert.ToDateTime(due);
                                if (curr1 >= due1)
                                {
                                    if (j != 0)
                                    {
                                        string[] str = todaysbalance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                }
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
                                DateTime current = DTPFrom.Value;
                                string curr = current.ToString(Master.dateformate);
                                DateTime curr1 = Convert.ToDateTime(curr);
                                DateTime duedate = Convert.ToDateTime(SPdt.Rows[j]["OD1"].ToString());
                                string due = duedate.ToString(Master.dateformate);
                                DateTime due1 = Convert.ToDateTime(due);
                                if (curr1 >= due1)
                                {
                                    if (j != 0)
                                    {
                                        string[] str = todaysbalance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                }
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
                                DateTime current = DTPFrom.Value;
                                string curr = current.ToString(Master.dateformate);
                                DateTime curr1 = Convert.ToDateTime(curr);
                                DateTime duedate = Convert.ToDateTime(SPdt.Rows[j]["OD1"].ToString());
                                string due = duedate.ToString(Master.dateformate);
                                DateTime due1 = Convert.ToDateTime(due);
                                if (curr1 >= due1)
                                {
                                    if (j != 0)
                                    {
                                        string[] str = todaysbalance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                }
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
                                DateTime current = DTPFrom.Value;
                                string curr = current.ToString(Master.dateformate);
                                DateTime curr1 = Convert.ToDateTime(curr);
                                DateTime duedate = Convert.ToDateTime(SPdt.Rows[j]["OD1"].ToString());
                                string due = duedate.ToString(Master.dateformate);
                                DateTime due1 = Convert.ToDateTime(due);
                                if (curr1 >= due1)
                                {
                                    if (j != 0)
                                    {
                                        string[] str = todaysbalance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                }
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
                                DateTime current = DTPFrom.Value;
                                string curr = current.ToString(Master.dateformate);
                                DateTime curr1 = Convert.ToDateTime(curr);
                                DateTime duedate = Convert.ToDateTime(SPdt.Rows[j]["OD1"].ToString());
                                string due = duedate.ToString(Master.dateformate);
                                DateTime due1 = Convert.ToDateTime(due);
                                if (curr1 >= due1)
                                {
                                    if (j != 0)
                                    {
                                        string[] str = todaysbalance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                }
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
                                if (j != 0)
                                {
                                    string[] str = todaysbalance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);

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
                                if (j != 0)
                                {
                                    string[] str = todaysbalance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);

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
                                if (j != 0)
                                {
                                    string[] str = todaysbalance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);

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
                                if (j != 0)
                                {
                                    string[] str = todaysbalance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);

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
                                if (j != 0)
                                {
                                    string[] str = todaysbalance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);

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
                                if (j != 0)
                                {
                                    string[] str = todaysbalance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);

                            }
                            else if (SPdt.Rows[j]["TranType"].ToString() == "Sale")
                            {
                                if (j != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                DateTime current = DTPFrom.Value;
                                string curr = current.ToString(Master.dateformate);
                                DateTime curr1 = Convert.ToDateTime(curr);
                                DateTime duedate = Convert.ToDateTime(SPdt.Rows[j]["OD1"].ToString());
                                string due = duedate.ToString(Master.dateformate);
                                DateTime due1 = Convert.ToDateTime(due);
                                if (curr1 >= due1)
                                {
                                    if (j != 0)
                                    {
                                        string[] str = todaysbalance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                }
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
                                if (j != 0)
                                {
                                    string[] str = todaysbalance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);

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
                                if (j != 0)
                                {
                                    string[] str = todaysbalance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                            }
                            else if (SPdt.Rows[j]["TranType"].ToString() == "Rect")
                            {
                                if (j != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                if (j != 0)
                                {
                                    string[] str = todaysbalance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                            }
                            else if (SPdt.Rows[j]["TranType"].ToString() == "SaleReturn")
                            {
                                if (j != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                DateTime current = DTPFrom.Value;
                                string curr = current.ToString(Master.dateformate);
                                DateTime curr1 = Convert.ToDateTime(curr);
                                DateTime duedate = Convert.ToDateTime(SPdt.Rows[j]["OD1"].ToString());
                                string due = duedate.ToString(Master.dateformate);
                                DateTime due1 = Convert.ToDateTime(due);
                                if (curr1 >= due1)
                                {
                                    if (j != 0)
                                    {
                                        string[] str = todaysbalance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                }
                            }
                            else if (SPdt.Rows[j]["TranType"].ToString() == "Purchase")
                            {
                                if (j != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                DateTime current = DTPFrom.Value;
                                string curr = current.ToString(Master.dateformate);
                                DateTime curr1 = Convert.ToDateTime(curr);
                                DateTime duedate = Convert.ToDateTime(SPdt.Rows[j]["OD1"].ToString());
                                string due = duedate.ToString(Master.dateformate);
                                DateTime due1 = Convert.ToDateTime(due);
                                if (curr1 >= due1)
                                {
                                    if (j != 0)
                                    {
                                        string[] str = todaysbalance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                }
                            }
                            else if (SPdt.Rows[j]["TranType"].ToString() == "PurchaseReturn")
                            {
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
                                DateTime current = DTPFrom.Value;
                                string curr = current.ToString(Master.dateformate);
                                DateTime curr1 = Convert.ToDateTime(curr);
                                DateTime duedate = Convert.ToDateTime(SPdt.Rows[j]["OD1"].ToString());
                                string due = duedate.ToString(Master.dateformate);
                                DateTime due1 = Convert.ToDateTime(due);
                                if (curr1 >= due1)
                                {
                                    if (j != 0)
                                    {
                                        string[] str = todaysbalance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                }
                            }
                            else if (SPdt.Rows[j]["TranType"].ToString() == "Pmnt")
                            {
                                if (j != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                if (j != 0)
                                {
                                    string[] str = todaysbalance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
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
                                if (j != 0)
                                {
                                    string[] str = todaysbalance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);

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
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                if (j != 0)
                                {
                                    string[] str = todaysbalance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);

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
                                if (j != 0)
                                {
                                    string[] str = todaysbalance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
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
                                if (j != 0)
                                {
                                    string[] str = todaysbalance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);

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
                                if (j != 0)
                                {
                                    string[] str = todaysbalance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);

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
                                if (j != 0)
                                {
                                    string[] str = todaysbalance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
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
                                if (j != 0)
                                {
                                    string[] str = todaysbalance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
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
                                DateTime current = DTPFrom.Value;
                                string curr = current.ToString(Master.dateformate);
                                DateTime curr1 = Convert.ToDateTime(curr);
                                DateTime duedate = Convert.ToDateTime(SPdt.Rows[j]["OD1"].ToString());
                                string due = duedate.ToString(Master.dateformate);
                                DateTime due1 = Convert.ToDateTime(due);
                                if (curr1 >= due1)
                                {
                                    if (j != 0)
                                    {
                                        string[] str = todaysbalance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                }
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
                                DateTime current = DTPFrom.Value;
                                string curr = current.ToString(Master.dateformate);
                                DateTime curr1 = Convert.ToDateTime(curr);
                                DateTime duedate = Convert.ToDateTime(SPdt.Rows[j]["OD1"].ToString());
                                string due = duedate.ToString(Master.dateformate);
                                DateTime due1 = Convert.ToDateTime(due);
                                if (curr1 >= due1)
                                {
                                    if (j != 0)
                                    {
                                        string[] str = todaysbalance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();
                                        opbal = Convert.ToDouble(str[0]);
                                    }
                                    todaysbalance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[j]["Amount"].ToString()), DC, SPdt.Rows[j]["dc"].ToString(), i);
                                }
                            }



                        }
                        if (balance == "")
                        {
                            balance = "0.00 Cr.";
                        }
                        if (todaysbalance == "")
                        {
                            todaysbalance = "0.00 Cr.";
                        }
                        string[] bal = balance.Split(' ');
                        string[] tdue = todaysbalance.Split(' ');
                        if (bal[1].ToString() == tdue[1].ToString())
                        {
                            temptable.Rows.Add("", dt.Rows[i]["AccountName"].ToString(), dt.Rows[i]["Mobile"].ToString(), dt.Rows[i]["Email"].ToString(), balance, todaysbalance);
                        }
                        else
                        {
                            temptable.Rows.Add("", dt.Rows[i]["AccountName"].ToString(), dt.Rows[i]["Mobile"].ToString(), dt.Rows[i]["Email"].ToString(), balance, "0.00 " + bal[1]);
                        }
                    }
                    else
                    {
                        temptable.Rows.Add("", dt.Rows[i]["AccountName"].ToString(), dt.Rows[i]["Mobile"].ToString(), dt.Rows[i]["Email"].ToString(), onlyopbal, "0.00 Dr.");
                    }
                    #endregion

                }
                payable = new DataTable();
                payable.Columns.Add("", typeof(string));
                payable.Columns.Add("AccountName", typeof(string));
                payable.Columns.Add("Mobile", typeof(string));
                payable.Columns.Add("Email", typeof(string));
                payable.Columns.Add("balance", typeof(string));
                payable.Columns.Add("todaysbalance", typeof(string));
                Receivable = new DataTable();
                Receivable.Columns.Add("", typeof(string));
                Receivable.Columns.Add("AccountName", typeof(string));
                Receivable.Columns.Add("Mobile", typeof(string));
                Receivable.Columns.Add("Email", typeof(string));
                Receivable.Columns.Add("balance", typeof(string));
                Receivable.Columns.Add("todaysbalance", typeof(string));

                for (int p = 0; p < temptable.Rows.Count; p++)
                {
                    string balance = temptable.Rows[p]["balance"].ToString();
                    var result = balance.Substring(balance.Length - 3);
                    if (result == "Dr.")
                    {
                        Receivable.Rows.Add("", temptable.Rows[p]["AccountName"].ToString(), temptable.Rows[p]["Mobile"].ToString(), temptable.Rows[p]["Email"].ToString(), temptable.Rows[p]["balance"].ToString(), temptable.Rows[p]["todaysbalance"].ToString());
                    }
                    else
                    {
                        payable.Rows.Add("", temptable.Rows[p]["AccountName"].ToString(), temptable.Rows[p]["Mobile"].ToString(), temptable.Rows[p]["Email"].ToString(), temptable.Rows[p]["balance"].ToString(), temptable.Rows[p]["todaysbalance"].ToString());
                    }
                }
            }

        }
        private void outstandinganalysis_Load(object sender, EventArgs e)
        {
            //DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
            //DTPFrom.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            //DTPFrom.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            //DTPFrom.Value = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            DTPFrom.CustomFormat = Master.dateformate;
            LVclient.CheckBoxes = true;
            LVclient.Columns.Add("", 20, HorizontalAlignment.Left);
            LVclient.Columns.Add("Account Head", 360, HorizontalAlignment.Left);
            LVclient.Columns.Add("Mobile", 150, HorizontalAlignment.Center);
            LVclient.Columns.Add("E-mail", 150, HorizontalAlignment.Center);
            LVclient.Columns.Add("Balance", 160, HorizontalAlignment.Center);
            LVclient.Columns.Add("Today's Due Amount", 160, HorizontalAlignment.Center);
            flag = 0;

            //    binddata();


        }

        private void btnrec_Click(object sender, EventArgs e)
        {
            flag = 0;
            txtnetamt.Text = "0";
            txttudaysbal.Text = "0";
            temptable = new DataTable();
            temptable.Columns.Add("", typeof(string));
            temptable.Columns.Add("AccountName", typeof(string));
            temptable.Columns.Add("Mobile", typeof(string));
            temptable.Columns.Add("Email", typeof(string));
            temptable.Columns.Add("balance", typeof(string));
            temptable.Columns.Add("Todaysbalance", typeof(string));
            binddata();
            LVclient.Items.Clear();
            for (int i = 0; i < Receivable.Rows.Count; i++)
            {
                string bal1 = Receivable.Rows[i]["balance"].ToString();
                String withoutLast1 = bal1.Substring(0, (bal1.Length - 3));
                Double d = Convert.ToDouble(withoutLast1);
                if (d > 0)
                {
                    ListViewItem li;
                    li = LVclient.Items.Add("");
                    li.SubItems.Add(Receivable.Rows[i]["AccountName"].ToString());
                    li.SubItems.Add(Receivable.Rows[i]["Mobile"].ToString());
                    li.SubItems.Add(Receivable.Rows[i]["Email"].ToString());
                    li.SubItems.Add(Receivable.Rows[i]["balance"].ToString());
                    li.SubItems.Add(Receivable.Rows[i]["todaysbalance"].ToString());
                    string bal = Receivable.Rows[i]["balance"].ToString();
                    String withoutLast = bal.Substring(0, (bal.Length - 3));
                    Double totalnet = +Convert.ToDouble(withoutLast);
                    Double d1t = Convert.ToDouble(txtnetamt.Text) + totalnet;
                    txtnetamt.Text = d1t.ToString("N2");
                    string bal11 = Receivable.Rows[i]["todaysbalance"].ToString();
                    String withoutLast11 = bal11.Substring(0, (bal11.Length - 3));
                    Double totalnet1 = +Convert.ToDouble(withoutLast11);
                    Double d1t1 = Convert.ToDouble(txttudaysbal.Text) + totalnet1;
                    txttudaysbal.Text = d1t1.ToString("N2");
                }
            }
        }

        private void btnpayble_Click(object sender, EventArgs e)
        {
            flag = 1;
            txtnetamt.Text = "0";
            txttudaysbal.Text = "0";
            temptable = new DataTable();
            temptable.Columns.Add("", typeof(string));
            temptable.Columns.Add("AccountName", typeof(string));
            temptable.Columns.Add("Mobile", typeof(string));
            temptable.Columns.Add("Email", typeof(string));
            temptable.Columns.Add("balance", typeof(string));
            temptable.Columns.Add("Todaysbalance", typeof(string));
            binddata();
            LVclient.Items.Clear();
            for (int i = 0; i < payable.Rows.Count; i++)
            {
                string bal1 = payable.Rows[i]["balance"].ToString();
                String withoutLast1 = bal1.Substring(0, (bal1.Length - 3));
                Double d = Convert.ToDouble(withoutLast1);
                if (d > 0)
                {
                    ListViewItem li;
                    li = LVclient.Items.Add("");
                    li.SubItems.Add(payable.Rows[i]["AccountName"].ToString());
                    li.SubItems.Add(payable.Rows[i]["Mobile"].ToString());
                    li.SubItems.Add(payable.Rows[i]["Email"].ToString());
                    li.SubItems.Add(payable.Rows[i]["balance"].ToString());
                    li.SubItems.Add(payable.Rows[i]["todaysbalance"].ToString());
                    string bal12 = payable.Rows[i]["balance"].ToString();
                    String withoutLast = bal12.Substring(0, (bal12.Length - 3));
                    Double totalnet = +Convert.ToDouble(withoutLast);
                    Double d1t = Convert.ToDouble(txtnetamt.Text) + totalnet;
                    txtnetamt.Text = d1t.ToString("N2");
                    string bal121 = payable.Rows[i]["todaysbalance"].ToString();
                    String withoutLast12 = bal121.Substring(0, (bal121.Length - 3));
                    Double totalnet12 = +Convert.ToDouble(withoutLast12);
                    Double d1t12 = Convert.ToDouble(txttudaysbal.Text) + totalnet12;
                    txttudaysbal.Text = d1t12.ToString("N2");
                }
            }
            //  binddata1();
        }
        Double totalnet = 0;
        Double tudaystotalnet = 0;
        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                prn.execute("delete from printing");
                string status;
                if (flag == 1)
                {
                    //status = "BANK ENTRY REGISTER FROM" + DTPFrom.Text;
                    status = "OUTSTANDING PAYABLE REPORT AS ON " + DTPFrom.Text;
                }
                else
                {
                    status = "OUTSTANDING RECEIVABLE REPORT AS ON " + DTPFrom.Text;
                }
                string[] a = new string[LVclient.CheckedItems.Count];
                string[] b = new string[LVclient.CheckedItems.Count];
                string[] c = new string[LVclient.CheckedItems.Count];
                string[] d = new string[LVclient.CheckedItems.Count];

                if (LVclient.Items.Count > 0)
                {
                    int checkCount = 0;
                    for (int i = 0; i < LVclient.Items.Count; i++)
                    {
                        if (Convert.ToBoolean(LVclient.Items[i].Checked) == true)
                        {
                            Account = LVclient.Items[i].SubItems[1].Text;
                            mobile = LVclient.Items[i].SubItems[2].Text;
                            email = LVclient.Items[i].SubItems[3].Text;
                            balance = LVclient.Items[i].SubItems[4].Text;
                            todaybalance = LVclient.Items[i].SubItems[5].Text;
                            String withoutLast = balance.Substring(0, (balance.Length - 3));
                            totalnet = +totalnet + Convert.ToDouble(withoutLast);
                            String withoutLast1 = todaybalance.Substring(0, (todaybalance.Length - 3));
                            tudaystotalnet = +tudaystotalnet + Convert.ToDouble(withoutLast1);
                            // Double d1t = Convert.ToDouble(txtnetamt.Text) + totalnet;
                            DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1 and CompanyID='" + Master.companyId + "' ");
                            string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T18)VALUES";
                            qry += "('" + Account + "','" + mobile + "','" + email + "','" + balance + "','" + dt1.Rows[0]["CompanyName"].ToString() + "','" + dt1.Rows[0]["SubName"].ToString() + "','" + dt1.Rows[0]["Address"].ToString() + "','" + dt1.Rows[0]["Address2"].ToString() + "','" + dt1.Rows[0]["City"].ToString() + "','" + dt1.Rows[0]["State"].ToString() + "','" + dt1.Rows[0]["Country"].ToString() + "','" + dt1.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Mobile"].ToString() + "','" + dt1.Rows[0]["Email"].ToString() + "','" + dt1.Rows[0]["CSTNo"].ToString() + "','" + status + "','" + todaybalance + "')";
                            prn.execute(qry);
                            checkCount++;
                        }
                    }
                    if (checkCount == 0)
                    {
                        MessageBox.Show("Select Atlist One Record for Printing.");
                        return;
                    }
                    string update = "UPDATE [Printing] SET [T17]='" + totalnet + "',[T19]='" + tudaystotalnet + "'";
                    prn.execute(update);
                    totalnet = 0;
                    tudaystotalnet = 0;
                    Print popup = new Print("Outstanding");
                    popup.ShowDialog();
                    popup.Dispose();
                }
                else
                {
                    MessageBox.Show("No Records For Printing..");
                    return;
                }
            }
            catch
            {
            }
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

        private void btnemail_Click(object sender, EventArgs e)
        {
            SendMailService.WriteErrorLog("start for loop");
            string Msg = "<html><Head></head><body><div>Dear Hitesh ,<br/><br/>";

            //Msg += " Your Products <b>";

            //Msg += dt.Rows[a]["name"].ToString() + "</b> will be expired on <b> ";

            //Msg += dt.Rows[a]["expirydate"].ToString() + "</b>. <br/>";

            //Msg += "Total Renewal Amount is:<b> INR " + dt.Rows[a]["premium"].ToString();

            //Msg += " for one year.</b> So please renew as early as possible.<br/><br/>";

            //Msg += "Our Bank Details is as follows:<br/>Account Name: D-HiT Solutions<br/>Account No: 3408008700000628<br/>Bank Details: Punjab National Bank, Atmajyoti Branch, Vadodara<br/>IFSC Code: PUNB0340800</div></body><html>";
            SendMailService.WriteErrorLog("data get successfully" + Msg);
            SendMailService.SendEmail("hiteshb577@gmail.com", "Your Website/Software will expire soon", Msg);
        }

        private void btnrec_Enter(object sender, EventArgs e)
        {
            btnrec.UseVisualStyleBackColor = false;
            btnrec.BackColor = Color.YellowGreen;
            btnrec.ForeColor = Color.White;
        }

        private void btnrec_Leave(object sender, EventArgs e)
        {
            btnrec.UseVisualStyleBackColor = true;
            btnrec.BackColor = Color.FromArgb(51, 153, 255);
            btnrec.ForeColor = Color.White;
        }

        private void btnrec_MouseEnter(object sender, EventArgs e)
        {
            btnrec.UseVisualStyleBackColor = false;
            btnrec.BackColor = Color.YellowGreen;
            btnrec.ForeColor = Color.White;
        }

        private void btnrec_MouseLeave(object sender, EventArgs e)
        {
            btnrec.UseVisualStyleBackColor = true;
            btnrec.BackColor = Color.FromArgb(51, 153, 255);
            btnrec.ForeColor = Color.White;
        }

        private void btnpayble_Enter(object sender, EventArgs e)
        {
            btnpayble.UseVisualStyleBackColor = false;
            btnpayble.BackColor = Color.YellowGreen;
            btnpayble.ForeColor = Color.White;
        }

        private void btnpayble_Leave(object sender, EventArgs e)
        {
            btnpayble.UseVisualStyleBackColor = true;
            btnpayble.BackColor = Color.FromArgb(51, 153, 255);
            btnpayble.ForeColor = Color.White;
        }

        private void btnpayble_MouseEnter(object sender, EventArgs e)
        {
            btnpayble.UseVisualStyleBackColor = false;
            btnpayble.BackColor = Color.YellowGreen;
            btnpayble.ForeColor = Color.White;
        }

        private void btnpayble_MouseLeave(object sender, EventArgs e)
        {
            btnpayble.UseVisualStyleBackColor = true;
            btnpayble.BackColor = Color.FromArgb(51, 153, 255);
            btnpayble.ForeColor = Color.White;
        }

        private void btnemail_Enter(object sender, EventArgs e)
        {
            btnemail.UseVisualStyleBackColor = true;
            btnemail.BackColor = Color.FromArgb(236, 233, 216);
            btnemail.ForeColor = Color.White;
        }

        private void btnemail_Leave(object sender, EventArgs e)
        {
            btnemail.UseVisualStyleBackColor = true;
            btnemail.BackColor = Color.FromArgb(51, 153, 255);
            btnemail.ForeColor = Color.White;
        }

        private void btnemail_MouseEnter(object sender, EventArgs e)
        {
            btnemail.UseVisualStyleBackColor = true;
            btnemail.BackColor = Color.FromArgb(236, 233, 216);
            btnemail.ForeColor = Color.White;
        }

        private void btnemail_MouseLeave(object sender, EventArgs e)
        {
            btnemail.UseVisualStyleBackColor = true;
            btnemail.BackColor = Color.FromArgb(51, 153, 255);
            btnemail.ForeColor = Color.White;
        }

        private void btnsms_Enter(object sender, EventArgs e)
        {
            btnsms.UseVisualStyleBackColor = true;
            btnsms.BackColor = Color.FromArgb(236, 233, 216);
            btnsms.ForeColor = Color.White;
        }

        private void btnsms_Leave(object sender, EventArgs e)
        {
            btnsms.UseVisualStyleBackColor = true;
            btnsms.BackColor = Color.FromArgb(51, 153, 255);
            btnsms.ForeColor = Color.White;
        }

        private void btnsms_MouseEnter(object sender, EventArgs e)
        {
            btnsms.UseVisualStyleBackColor = true;
            btnsms.BackColor = Color.FromArgb(236, 233, 216);
            btnsms.ForeColor = Color.White;
        }

        private void btnsms_MouseLeave(object sender, EventArgs e)
        {
            btnsms.UseVisualStyleBackColor = true;
            btnsms.BackColor = Color.FromArgb(51, 153, 255);
            btnsms.ForeColor = Color.White;
        }
        public void open()
        {
            try
            {
                this.Enabled = false;
                string iid = LVclient.Items[LVclient.FocusedItem.Index].SubItems[1].Text;
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

        private void chkselectall_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkselectall.Checked == true)
                {
                    for (int i = 0; i < LVclient.Items.Count; i++)
                    {
                        LVclient.Items[i].Checked = true;
                    }
                }
                else
                {
                    for (int i = 0; i < LVclient.Items.Count; i++)
                    {
                        LVclient.Items[i].Checked = false;
                    }
                }
            }
            catch
            {
            }
        }

    }
}
