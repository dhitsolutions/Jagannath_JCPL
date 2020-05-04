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
    public partial class Ledger : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        OleDbSettings ods = new OleDbSettings();
        Printing prn = new Printing();
        int time;
        int cnt = 0;
        public Ledger()
        {
            InitializeComponent();
        }

        public Ledger(Master master, TabControl tabControl)
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
            DTPTo.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            DTPTo.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            DTPFrom.Value = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            con.Open();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            LVledger.Columns.Add("Date", 100, HorizontalAlignment.Center);
            LVledger.Columns.Add("Type", 80, HorizontalAlignment.Center);
            LVledger.Columns.Add("Name", 100, HorizontalAlignment.Left);
            LVledger.Columns.Add("Remarks", 470, HorizontalAlignment.Left);
            LVledger.Columns.Add("Debit", 100, HorizontalAlignment.Right);
            LVledger.Columns.Add("Credit", 100, HorizontalAlignment.Right);
            LVledger.Columns.Add("Balance", 140, HorizontalAlignment.Right);
            LVledger.Columns.Add("Billno", 0, HorizontalAlignment.Right);
            LVledger.Columns.Add("ClientID", 0, HorizontalAlignment.Right);

            //  listviewbind();

            bindcustomer();
            con.Close();
            mouseclickid.Columns.Add("type", typeof(string));
            mouseclickid.Columns.Add("id", typeof(string));
            DTPFrom.CustomFormat = Master.dateformate;
            DTPTo.CustomFormat = Master.dateformate;
            this.ActiveControl = cmbaccname;
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
        public void bindcustomer()
        {
            SqlCommand cmd1 = new SqlCommand("select ClientID,AccountName from ClientMaster where isactive=1 order by AccountName", con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            cmbaccname.ValueMember = "ClientID";
            cmbaccname.DisplayMember = "AccountName";
            cmbaccname.DataSource = dt1;
            cmbaccname.SelectedIndex = -1;


            //  autobind(dt1, cmbaccname);
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
            if ((cmbaccname.Text).ToUpper() == "CASH" || cmbaccname.SelectedValue == "101")
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

                //for (int i = 0; i < OPdt.Rows.Count; i++)
                //{
                //    Double opbal = 0;
                //    if (OPdt.Rows[i]["DC"].ToString() == "D")
                //    {

                //    }
                //    else if (OPdt.Rows[i]["TranType"].ToString() == "Rect")
                //    {
                //        ListViewItem li;
                //        li = LVledger.Items.Add(Convert.ToDateTime(OPdt.Rows[i]["Date1"].ToString()).ToString("dd-MMM-yyyy"));
                //        li.SubItems.Add("Rect");
                //        li.SubItems.Add(OPdt.Rows[i]["OT1"].ToString());
                //        li.SubItems.Add("By Rcpt. No.: " + OPdt.Rows[i]["VoucherID"].ToString() + "; " + OPdt.Rows[i]["ShortNarration"].ToString());
                //        li.SubItems.Add("");
                //        li.SubItems.Add(Math.Round(Convert.ToDouble(OPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                //        li.SubItems.Add("Rect");
                //    }
                //}
                #endregion

                //for create ledger
                mouseclickid.Rows.Clear();
                LVledger.Items.Clear();
                #region
                DataTable SPdt = conn.getdataset("select * from Ledger where isactive=1 and Date1 between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and OT1='" + cmbaccname.Text + "' order by Date1");
                string balance = "0.00";
                balance = Convert.ToString(opbal);
                Double debit = 0, credit = 0;

                for (int i = 0; i < SPdt.Rows.Count; i++)
                {
                    if (SPdt.Rows[i]["TranType"].ToString() == "EXPCash Recept")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("EXPCash Recept");
                        li.SubItems.Add("EXPCash Recept");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                        //li.SubItems.Add("");
                        //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        //credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        li.SubItems.Add("");
                        mouseclickid.Rows.Add("EXPCash Recept", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "EXPCash Invoice")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("EXPCash Invoice");
                        li.SubItems.Add("EXPCash Invoice");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        li.SubItems.Add("");
                        mouseclickid.Rows.Add("EXPCash Invoice", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "PRCash Recept")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("PRCash Recept");
                        li.SubItems.Add("PRCash Recept");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add("");
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        mouseclickid.Rows.Add("PRCash Recept", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "SRCash Invoice")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("SRCash Invoice");
                        li.SubItems.Add("SRCash Invoice");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        li.SubItems.Add("");
                        mouseclickid.Rows.Add("SRCash Invoice", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "SaleCash Recept")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("SaleCash Recept");
                        li.SubItems.Add("SaleCash Recept");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add("");
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        mouseclickid.Rows.Add("SaleCash Recept", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "PurchaseCash Invoice")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("PurchaseCash Invoice");
                        li.SubItems.Add("PurchaseCash Invoice");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        li.SubItems.Add("");
                        mouseclickid.Rows.Add("PurchaseCash Invoice", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "DNCash Recept")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("DNCash Recept");
                        li.SubItems.Add("DNCash Recept");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add("");
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        mouseclickid.Rows.Add("DNCash Recept", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "CNCash Invoice")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("CNCash Invoice");
                        li.SubItems.Add("CNCash Invoice");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        li.SubItems.Add("");
                        mouseclickid.Rows.Add("CNCash Invoice", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHEREXP")
                    {
                        if (SPdt.Rows[i]["dc"].ToString() == "D")
                        {
                            ListViewItem li;
                            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add("EXPVOUCHERSALE");
                            li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            li.SubItems.Add("");
                            mouseclickid.Rows.Add("Sale", SPdt.Rows[i]["VoucherID"].ToString());
                            if (i != 0)
                            {
                                string[] str = balance.Split(' ');
                                char temp = str[1][0];
                                DC = temp.ToString();

                                opbal = Convert.ToDouble(str[0]);
                            }

                            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                            li.SubItems.Add(balance);
                            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                        }
                        else
                        {
                            ListViewItem li;
                            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add("EXPVOUCHERPURCHASE");
                            li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                            li.SubItems.Add("");
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            mouseclickid.Rows.Add("Purchase", SPdt.Rows[i]["VoucherID"].ToString());
                            if (i != 0)
                            {
                                string[] str = balance.Split(' ');
                                char temp = str[1][0];
                                DC = temp.ToString();

                                opbal = Convert.ToDouble(str[0]);
                            }

                            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                            li.SubItems.Add(balance);
                            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                        }
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERDN")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("GSTVOUCHERDN");
                        li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                        li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
                        string CD = SPdt.Rows[i]["dc"].ToString();
                        if (CD == "D")
                        {
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            li.SubItems.Add("");
                        }
                        else
                        {
                            li.SubItems.Add("");
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                        }

                        mouseclickid.Rows.Add("GSTVOUCHERDN", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERCN")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("GSTVOUCHERCN");
                        li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                        li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
                        string CD = SPdt.Rows[i]["dc"].ToString();
                        if (CD == "D")
                        {
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            li.SubItems.Add("");
                        }
                        else
                        {
                            li.SubItems.Add("");
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                        }

                        mouseclickid.Rows.Add("GSTVOUCHERCN", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERS")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("GSTVOUCHERSALE");
                        li.SubItems.Add("Sales");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        li.SubItems.Add("");
                        mouseclickid.Rows.Add("GSTVOUCHERSALE", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERSR")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("GSTVOUCHERSR");
                        li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                        li.SubItems.Add("Return Bill No: " + SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add("");
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        mouseclickid.Rows.Add("GSTVOUCHERSR", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERP")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("GSTVOUCHERP");
                        li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add("");
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        mouseclickid.Rows.Add("GSTVOUCHERP", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();

                            opbal = Convert.ToDouble(str[0]);
                        }

                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERPR")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("GSTVOUCHERPR");
                        li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                        li.SubItems.Add("Return Bill No: " + SPdt.Rows[i]["VoucherID"].ToString());

                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        li.SubItems.Add("");
                        debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        mouseclickid.Rows.Add("Purchase Return", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
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
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "Sale")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Sale");
                        li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                        debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        li.SubItems.Add("");
                        mouseclickid.Rows.Add("Sale", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();

                            opbal = Convert.ToDouble(str[0]);
                        }

                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "Cash Recept")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Cash Recept");
                        li.SubItems.Add("Sales");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add("");
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        mouseclickid.Rows.Add("Cash Recept", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "Rect")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Rect");
                        li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                        li.SubItems.Add("By Rcpt. No.: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["ShortNarration"].ToString());
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        li.SubItems.Add("");
                        debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        mouseclickid.Rows.Add("Rect", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        string CD = SPdt.Rows[i]["dc"].ToString();
                        if (CD == "C")
                        {
                            CD = "D";
                        }
                        else
                        {
                            CD = "C";
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);

                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "SaleReturn")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Sale Return");
                        li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                        li.SubItems.Add("Return Bill No: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["ShortNarration"].ToString());
                        li.SubItems.Add("");
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        mouseclickid.Rows.Add("Sale Return", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();

                            opbal = Convert.ToDouble(str[0]);
                        }
                        if (DC == "C")
                        {
                            DC = "D";
                        }
                        else
                        {
                            DC = "C";
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "Purchase")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Purchase");
                        li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add("");
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        mouseclickid.Rows.Add("Purchase", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();

                            opbal = Convert.ToDouble(str[0]);
                        }

                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "PurchaseReturn")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Purchase Return");
                        li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                        li.SubItems.Add("Return Bill No: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["ShortNarration"].ToString());
                        li.SubItems.Add("");
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        mouseclickid.Rows.Add("Purchase Return", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
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
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "Pmnt")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Pmnt");
                        li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                        li.SubItems.Add("By Rcpt. No.: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["ShortNarration"].ToString());
                        li.SubItems.Add("");
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));

                        credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        mouseclickid.Rows.Add("Pmnt", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();

                            opbal = Convert.ToDouble(str[0]);
                        }
                        string CD = SPdt.Rows[i]["dc"].ToString();
                        if (CD == "C")
                        {
                            CD = "D";
                        }
                        else
                        {
                            CD = "C";
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "Cash Invoice")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Cash Invoice");
                        li.SubItems.Add("Purchases");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        li.SubItems.Add("");
                        mouseclickid.Rows.Add("Cash Invoice", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "Cheque Issued")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Cheque Issued");
                        li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                        string CD = SPdt.Rows[i]["dc"].ToString();
                        li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                        if (CD == "D")
                        {
                            //  li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                            li.SubItems.Add("");
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        }
                        else
                        {
                            li.SubItems.Add("");
                            //  li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        }
                        credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        mouseclickid.Rows.Add("Cheque Issued", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();

                            opbal = Convert.ToDouble(str[0]);
                        }
                        CD = SPdt.Rows[i]["dc"].ToString();
                        if (CD == "C")
                        {
                            CD = "D";
                        }
                        else
                        {
                            CD = "C";
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "Draft Issued")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Draft Issued");
                        li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                        string CD = SPdt.Rows[i]["dc"].ToString();
                        li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                        if (CD == "D")
                        {
                            //  li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                            li.SubItems.Add("");
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        }
                        else
                        {
                            li.SubItems.Add("");
                            //li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        }
                        credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        mouseclickid.Rows.Add("Draft Issued", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();

                            opbal = Convert.ToDouble(str[0]);
                        }
                        CD = SPdt.Rows[i]["dc"].ToString();
                        if (CD == "C")
                        {
                            CD = "D";
                        }
                        else
                        {
                            CD = "C";
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "Cheque/Draft/Rtgs Received")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Cheque/Draft/Rtgs Received");
                        li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                        string CD = SPdt.Rows[i]["dc"].ToString();
                        li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                        if (CD == "D")
                        {
                            // li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                            li.SubItems.Add("");
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        }
                        else
                        {
                            li.SubItems.Add("");
                            // li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        }
                        credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        mouseclickid.Rows.Add("Cheque/Draft/Rtgs Received", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();

                            opbal = Convert.ToDouble(str[0]);
                        }
                        CD = SPdt.Rows[i]["dc"].ToString();
                        if (CD == "C")
                        {
                            CD = "D";
                        }
                        else
                        {
                            CD = "C";
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }

                    else if (SPdt.Rows[i]["TranType"].ToString() == "Deposit Cash Into Bank")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Deposit Cash Into Bank");
                        li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                        string CD = SPdt.Rows[i]["dc"].ToString();
                        li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                        if (CD == "D")
                        {
                            //  li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                            li.SubItems.Add("");
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        }
                        else
                        {
                            li.SubItems.Add("");
                            // li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        }
                        credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        mouseclickid.Rows.Add("Deposit Cash Into Bank", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();

                            opbal = Convert.ToDouble(str[0]);
                        }
                        CD = SPdt.Rows[i]["dc"].ToString();
                        if (CD == "C")
                        {
                            CD = "D";
                        }
                        else
                        {
                            CD = "C";
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }

                    else if (SPdt.Rows[i]["TranType"].ToString() == "Withdraw Cash from Bank")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Withdraw Cash from Bank");
                        li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                        string CD = SPdt.Rows[i]["dc"].ToString();
                        li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                        if (CD == "D")
                        {
                            //  li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                            li.SubItems.Add("");
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        }
                        else
                        {
                            li.SubItems.Add("");
                            //  li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        }
                        credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        mouseclickid.Rows.Add("Withdraw Cash from Bank", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();

                            opbal = Convert.ToDouble(str[0]);
                        }
                        CD = SPdt.Rows[i]["dc"].ToString();
                        if (CD == "C")
                        {
                            CD = "D";
                        }
                        else
                        {
                            CD = "C";
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }

                    else if (SPdt.Rows[i]["TranType"].ToString() == "Bank Expenses")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Bank Expenses");
                        li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                        string CD = SPdt.Rows[i]["dc"].ToString();
                        li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                        if (CD == "D")
                        {
                            // li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                            li.SubItems.Add("");
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        }
                        else
                        {
                            li.SubItems.Add("");
                            //   li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        }
                        credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        mouseclickid.Rows.Add("Bank Expenses", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();

                            opbal = Convert.ToDouble(str[0]);
                        }
                        CD = SPdt.Rows[i]["dc"].ToString();
                        if (CD == "C")
                        {
                            CD = "D";
                        }
                        else
                        {
                            CD = "C";
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }

                    else if (SPdt.Rows[i]["TranType"].ToString() == "Online Transfer")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Online Transfer");
                        li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                        string CD = SPdt.Rows[i]["dc"].ToString();
                        li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                        if (CD == "D")
                        {
                            //li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                            li.SubItems.Add("");
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        }
                        else
                        {
                            li.SubItems.Add("");
                            // li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        }
                        credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        mouseclickid.Rows.Add("Online Transfer", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();

                            opbal = Convert.ToDouble(str[0]);
                        }
                        CD = SPdt.Rows[i]["dc"].ToString();
                        if (CD == "C")
                        {
                            CD = "D";
                        }
                        else
                        {
                            CD = "C";
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "DEBIT NOTE")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("DEBIT NOTE");
                        li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                        li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
                        string CD = SPdt.Rows[i]["dc"].ToString();
                        if (CD == "D")
                        {
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            li.SubItems.Add("");
                        }
                        else
                        {
                            li.SubItems.Add("");
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                        }

                        mouseclickid.Rows.Add("DEBIT NOTE", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "CREDIT NOTE")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("CREDIT NOTE");
                        li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                        li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
                        string CD = SPdt.Rows[i]["dc"].ToString();
                        if (CD == "D")
                        {
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            li.SubItems.Add("");
                        }
                        else
                        {
                            li.SubItems.Add("");
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                        }

                        mouseclickid.Rows.Add("CREDIT NOTE", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }

                }
                #endregion

                txttotdebit.Text = debit.ToString("N2");
                txttotcredit.Text = credit.ToString("N2");
                txtbalance.Text = balance;
            }
            else
            {
                //for calculate OpBalance
                #region
                string totaldebit = conn.ExecuteScalar("select sum(amount) from Ledger where Date1 < '" + DTPFrom.Text + "' and Accountid='" + cmbaccname.SelectedValue + "' and dc='D' and isactive=1 ");
                string totalcredit = conn.ExecuteScalar("select sum(amount) from Ledger where Date1 < '" + DTPFrom.Text + "' and Accountid='" + cmbaccname.SelectedValue + "' and dc='C' and isactive=1 ");
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

                //for (int i = 0; i < OPdt.Rows.Count; i++)
                //{
                //    Double opbal = 0;
                //    if (OPdt.Rows[i]["DC"].ToString() == "D")
                //    {

                //    }
                //    else if (OPdt.Rows[i]["TranType"].ToString() == "Rect")
                //    {
                //        ListViewItem li;
                //        li = LVledger.Items.Add(Convert.ToDateTime(OPdt.Rows[i]["Date1"].ToString()).ToString("dd-MMM-yyyy"));
                //        li.SubItems.Add("Rect");
                //        li.SubItems.Add(OPdt.Rows[i]["OT1"].ToString());
                //        li.SubItems.Add("By Rcpt. No.: " + OPdt.Rows[i]["VoucherID"].ToString() + "; " + OPdt.Rows[i]["ShortNarration"].ToString());
                //        li.SubItems.Add("");
                //        li.SubItems.Add(Math.Round(Convert.ToDouble(OPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                //        li.SubItems.Add("Rect");
                //    }
                //}
                #endregion
                //for create ledger
                mouseclickid.Rows.Clear();
                LVledger.Items.Clear();
                #region
                DataTable SPdt = conn.getdataset("select * from Ledger where isactive=1 and Date1 between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and Accountid='" + cmbaccname.SelectedValue + "' order by Date1");
                string balance = "0.00";
                balance = Convert.ToString(opbal);
                Double debit = 0, credit = 0;
                for (int i = 0; i < SPdt.Rows.Count; i++)
                {
                    if (SPdt.Rows[i]["TranType"].ToString() == "EXPCash Recept")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("EXPCash Recept");
                        li.SubItems.Add("EXPCash Recept");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                        //li.SubItems.Add("");
                        //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        //credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        li.SubItems.Add("");
                        mouseclickid.Rows.Add("EXPCash Recept", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "EXPCash Invoice")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("EXPCash Invoice");
                        li.SubItems.Add("EXPCash Invoice");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        li.SubItems.Add("");
                        mouseclickid.Rows.Add("EXPCash Invoice", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "PRCash Recept")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("PRCash Recept");
                        li.SubItems.Add("PRCash Recept");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add("");
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        mouseclickid.Rows.Add("PRCash Recept", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "SRCash Invoice")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("SRCash Invoice");
                        li.SubItems.Add("SRCash Invoice");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        li.SubItems.Add("");
                        mouseclickid.Rows.Add("SRCash Invoice", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "SaleCash Recept")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("SaleCash Recept");
                        li.SubItems.Add("SaleCash Recept");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add("");
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        mouseclickid.Rows.Add("SaleCash Recept", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "PurchaseCash Invoice")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("PurchaseCash Invoice");
                        li.SubItems.Add("PurchaseCash Invoice");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        li.SubItems.Add("");
                        mouseclickid.Rows.Add("PurchaseCash Invoice", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "DNCash Recept")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("DNCash Recept");
                        li.SubItems.Add("DNCash Recept");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add("");
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        mouseclickid.Rows.Add("DNCash Recept", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "CNCash Invoice")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("CNCash Invoice");
                        li.SubItems.Add("CNCash Invoice");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        li.SubItems.Add("");
                        mouseclickid.Rows.Add("CNCash Invoice", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHEREXP")
                    {
                        if (SPdt.Rows[i]["dc"].ToString() == "D")
                        {
                            ListViewItem li;
                            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add("EXPVOUCHERSALE");
                            li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            li.SubItems.Add("");
                            mouseclickid.Rows.Add("Exp Voucher Sale", SPdt.Rows[i]["VoucherID"].ToString());
                            if (i != 0)
                            {
                                string[] str = balance.Split(' ');
                                char temp = str[1][0];
                                DC = temp.ToString();

                                opbal = Convert.ToDouble(str[0]);
                            }

                            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                            li.SubItems.Add(balance);
                            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                        }
                        else
                        {
                            ListViewItem li;
                            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add("EXPVOUCHERPURCHASE");
                            li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                            li.SubItems.Add("");
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            mouseclickid.Rows.Add("Exp VoucherPurchase", SPdt.Rows[i]["VoucherID"].ToString());
                            if (i != 0)
                            {
                                string[] str = balance.Split(' ');
                                char temp = str[1][0];
                                DC = temp.ToString();

                                opbal = Convert.ToDouble(str[0]);
                            }

                            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                            li.SubItems.Add(balance);
                            li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                            li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                        }
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERDN")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("GSTVOUCHERDN");
                        li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                        li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
                        string CD = SPdt.Rows[i]["dc"].ToString();
                        if (CD == "D")
                        {
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            li.SubItems.Add("");
                        }
                        else
                        {
                            li.SubItems.Add("");
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                        }

                        mouseclickid.Rows.Add("GSTVOUCHERDN", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERCN")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("GSTVOUCHERCN");
                        li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                        li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
                        string CD = SPdt.Rows[i]["dc"].ToString();
                        if (CD == "D")
                        {
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            li.SubItems.Add("");
                        }
                        else
                        {
                            li.SubItems.Add("");
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                        }

                        mouseclickid.Rows.Add("GSTVOUCHERCN", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERS")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("GSTVOUCHERSALE");
                        li.SubItems.Add("Sales");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        li.SubItems.Add("");
                        mouseclickid.Rows.Add("GSTVOUCHERSALE", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERSR")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("GSTVOUCHERSR");
                        li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                        li.SubItems.Add("Return Bill No: " + SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add("");
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        mouseclickid.Rows.Add("GSTVOUCHERSR", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERP")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("GSTVOUCHERP");
                        li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add("");
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        mouseclickid.Rows.Add("GSTVOUCHERP", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();

                            opbal = Convert.ToDouble(str[0]);
                        }

                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERPR")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("GSTVOUCHERPR");
                        li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                        li.SubItems.Add("Return Bill No: " + SPdt.Rows[i]["VoucherID"].ToString());

                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        li.SubItems.Add("");
                        debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        mouseclickid.Rows.Add("Purchase Return", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
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
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "Sale")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Sale");
                        li.SubItems.Add("Sales");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        li.SubItems.Add("");
                        mouseclickid.Rows.Add("Sale", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "Cash Recept")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Cash Recept");
                        li.SubItems.Add("Sales");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add("");
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        mouseclickid.Rows.Add("Cash Recept", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "Rect")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Rect");
                        li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                        li.SubItems.Add("By Rcpt. No.: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["ShortNarration"].ToString());
                        li.SubItems.Add("");
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        mouseclickid.Rows.Add("Rect", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "SaleReturn")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Sale Return");
                        li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                        li.SubItems.Add("Return Bill No: " + SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add("");
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        mouseclickid.Rows.Add("Sale Return", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "Purchase")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Purchase");
                        li.SubItems.Add("Purchases");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add("");
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        mouseclickid.Rows.Add("Purchase", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }

                    else if (SPdt.Rows[i]["TranType"].ToString() == "PurchaseReturn")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Purchase Return");
                        li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                        li.SubItems.Add("Return Bill No: " + SPdt.Rows[i]["VoucherID"].ToString());

                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        li.SubItems.Add("");
                        debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        mouseclickid.Rows.Add("Purchase Return", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
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
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "Pmnt")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Pmnt");
                        li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                        li.SubItems.Add("By Rcpt. No.: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["ShortNarration"].ToString());
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        li.SubItems.Add("");
                        mouseclickid.Rows.Add("Pmnt", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "Cash Invoice")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Cash Invoice");
                        li.SubItems.Add("Purchases");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                        li.SubItems.Add("");
                        mouseclickid.Rows.Add("Cash Invoice", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "Cheque Issued")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Cheque Issued");
                        li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                        li.SubItems.Add("By Rcpt. No.: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["OT5"].ToString() + "; " + SPdt.Rows[i]["OT6"].ToString());
                        string CD = SPdt.Rows[i]["dc"].ToString();
                        if (CD == "D")
                        {
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            li.SubItems.Add("");
                        }
                        else
                        {
                            li.SubItems.Add("");
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                        }

                        mouseclickid.Rows.Add("Cheque Issued", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "Draft Issued")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Draft Issued");
                        li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                        li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                        string CD = SPdt.Rows[i]["dc"].ToString();
                        if (CD == "D")
                        {
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["OT3"].ToString()), 2).ToString("N2"));
                            debit += Convert.ToDouble(SPdt.Rows[i]["OT3"].ToString());
                            li.SubItems.Add("");
                        }
                        else
                        {
                            li.SubItems.Add("");
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["OT3"].ToString()), 2).ToString("N2"));
                            credit += Convert.ToDouble(SPdt.Rows[i]["OT3"].ToString());


                        }

                        mouseclickid.Rows.Add("Draft Issued", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["OT3"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "Cheque/Draft/Rtgs Received")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Cheque/Draft/Rtgs Received");
                        li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                        li.SubItems.Add("By Rcpt. No.: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["OT5"].ToString() + "; " + SPdt.Rows[i]["OT6"].ToString());
                        string CD = SPdt.Rows[i]["dc"].ToString();
                        if (CD == "D")
                        {
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            li.SubItems.Add("");
                        }
                        else
                        {
                            li.SubItems.Add("");
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                        }

                        mouseclickid.Rows.Add("Cheque/Draft/Rtgs Received", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "Deposit Cash Into Bank")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Deposit Cash Into Bank");
                        li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                        li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                        string CD = SPdt.Rows[i]["dc"].ToString();
                        if (CD == "D")
                        {
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            li.SubItems.Add("");
                        }
                        else
                        {
                            li.SubItems.Add("");
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                        }

                        mouseclickid.Rows.Add("Deposit Cash Into Bank", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "Withdraw Cash from Bank")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Withdraw Cash from Bank");
                        li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                        li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                        string CD = SPdt.Rows[i]["dc"].ToString();
                        if (CD == "D")
                        {
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            li.SubItems.Add("");
                        }
                        else
                        {
                            li.SubItems.Add("");
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                        }

                        mouseclickid.Rows.Add("Withdraw Cash from Bank", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "Bank Expenses")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Bank Expenses");
                        li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                        li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                        string CD = SPdt.Rows[i]["dc"].ToString();
                        if (CD == "D")
                        {
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            li.SubItems.Add("");
                        }
                        else
                        {
                            li.SubItems.Add("");
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                        }

                        mouseclickid.Rows.Add("Bank Expenses", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "Online Transfer")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Online Transfer");
                        li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                        li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                        string CD = SPdt.Rows[i]["dc"].ToString();
                        if (CD == "D")
                        {
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            li.SubItems.Add("");
                        }
                        else
                        {
                            li.SubItems.Add("");
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                        }

                        mouseclickid.Rows.Add("Online Transfer", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "DEBIT NOTE")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("DEBIT NOTE");
                        li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                        li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
                        string CD = SPdt.Rows[i]["dc"].ToString();
                        if (CD == "D")
                        {
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            li.SubItems.Add("");
                        }
                        else
                        {
                            li.SubItems.Add("");
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                        }

                        mouseclickid.Rows.Add("DEBIT NOTE", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }
                    else if (SPdt.Rows[i]["TranType"].ToString() == "CREDIT NOTE")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("CREDIT NOTE");
                        li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                        li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
                        string CD = SPdt.Rows[i]["dc"].ToString();
                        if (CD == "D")
                        {
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            li.SubItems.Add("");
                        }
                        else
                        {
                            li.SubItems.Add("");
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                        }

                        mouseclickid.Rows.Add("CREDIT NOTE", SPdt.Rows[i]["VoucherID"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[1][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                    }

                }
                #endregion

                txttotdebit.Text = debit.ToString("N2");
                txttotcredit.Text = credit.ToString("N2");
                txtbalance.Text = balance;
            }
        }
        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            if (cmbaccname.Text != "")
            {

                EventHandler<EventArgs> ea = Canceled;
                if (ea != null)
                    ea(this, e);
                databind();
            }
            else
            {
                MessageBox.Show("Please Select Account Name");
                cmbaccname.Focus();
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

                DataTable dt = new DataTable();
                dt = conn.getdataset("select AccountName from ClientMaster where isactive=1 order by AccountName");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (cmbaccname.Text == dt.Rows[i]["AccountName"].ToString())
                    {
                        DTPFrom.Focus();
                    }
                }

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
            try
            {

                if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "EXPVOUCHERSALE")
                {
                    string[] strfinalarray = new string[5] { "EXP", "D", "GSTVOUCHEREXP", "EXP", "" };
                    string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[3].Text.Remove(0, 9);
                    int clientid = Convert.ToInt32(LVledger.Items[LVledger.FocusedItem.Index].SubItems[8].Text);
                    String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                    //  DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                    GSTVouchers bd = new GSTVouchers(this, master, tabControl, strfinalarray);
                    // Sale p = new Sale(this, master, tabControl);
                    //   if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                    //    {
                    bd.updatemode(str, billno, clientid, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd);
                    //   }
                }
                else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "EXPVOUCHERPURCHASE")
                {
                    string[] strfinalarray = new string[5] { "EXP", "D", "GSTVOUCHEREXP", "EXP", "" };
                    string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[3].Text.Remove(0, 9);
                    int clientid = Convert.ToInt32(LVledger.Items[LVledger.FocusedItem.Index].SubItems[8].Text);
                    String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                    //  DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                    GSTVouchers bd = new GSTVouchers(this, master, tabControl, strfinalarray);
                    // Sale p = new Sale(this, master, tabControl);
                    //   if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                    //    {
                    bd.updatemode(str, billno, clientid, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd);
                    //   }
                }
                else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "PRCash Recept")
                {
                    string[] strfinalarray = new string[5] { "PR", "D", "GSTVOUCHERPR", "PR", "" };
                    string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[3].Text.Remove(0, 9);
                    int clientid = Convert.ToInt32(LVledger.Items[LVledger.FocusedItem.Index].SubItems[8].Text);
                    String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                    //  DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                    GSTVouchers bd = new GSTVouchers(this, master, tabControl, strfinalarray);
                    // Sale p = new Sale(this, master, tabControl);
                    //   if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                    //    {
                    bd.updatemode(str, billno, clientid, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd);
                    //   }
                }
                else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "SRCash Invoice")
                {
                    string[] strfinalarray = new string[5] { "SR", "C", "GSTVOUCHERSR", "SR", "" };
                    string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[3].Text.Remove(0, 9);
                    int clientid = Convert.ToInt32(LVledger.Items[LVledger.FocusedItem.Index].SubItems[8].Text);
                    String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                    //  DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                    GSTVouchers bd = new GSTVouchers(this, master, tabControl, strfinalarray);
                    // Sale p = new Sale(this, master, tabControl);
                    //   if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                    //    {
                    bd.updatemode(str, billno, clientid, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd);
                    //   }
                }
                else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "SaleCash Recept")
                {
                    string[] strfinalarray = new string[5] { "S", "D", "GSTVOUCHERS", "S", "" };
                    string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[3].Text.Remove(0, 9);
                    int clientid = Convert.ToInt32(LVledger.Items[LVledger.FocusedItem.Index].SubItems[8].Text);
                    String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                    //  DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                    GSTVouchers bd = new GSTVouchers(this, master, tabControl, strfinalarray);
                    // Sale p = new Sale(this, master, tabControl);
                    //   if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                    //    {
                    bd.updatemode(str, billno, clientid, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd);
                    //   }
                }
                else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "EXPCash Recept")
                {
                    string[] strfinalarray = new string[5] { "EXP", "D", "GSTVOUCHEREXP", "EXP", "" };
                    string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[3].Text.Remove(0, 9);
                    int clientid = Convert.ToInt32(LVledger.Items[LVledger.FocusedItem.Index].SubItems[8].Text);
                    String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                    //  DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                    GSTVouchers bd = new GSTVouchers(this, master, tabControl, strfinalarray);
                    // Sale p = new Sale(this, master, tabControl);
                    //   if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                    //    {
                    bd.updatemode(str, billno, clientid, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd);
                    //   }
                }
                else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "EXPCash Invoice")
                {
                    string[] strfinalarray = new string[5] { "EXP", "D", "GSTVOUCHEREXP", "EXP", "" };
                    string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[3].Text.Remove(0, 9);
                    int clientid = Convert.ToInt32(LVledger.Items[LVledger.FocusedItem.Index].SubItems[8].Text);
                    String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                    //  DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                    GSTVouchers bd = new GSTVouchers(this, master, tabControl, strfinalarray);
                    // Sale p = new Sale(this, master, tabControl);
                    //   if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                    //    {
                    bd.updatemode(str, billno, clientid, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd);
                    //   }
                }
                else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "PurchaseCash Invoice")
                {
                    string[] strfinalarray = new string[5] { "P", "C", "GSTVOUCHERP", "P", "" };
                    string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[3].Text.Remove(0, 9);
                    int clientid = Convert.ToInt32(LVledger.Items[LVledger.FocusedItem.Index].SubItems[8].Text);
                    String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                    //  DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                    GSTVouchers bd = new GSTVouchers(this, master, tabControl, strfinalarray);
                    // Sale p = new Sale(this, master, tabControl);
                    //   if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                    //    {
                    bd.updatemode(str, billno, clientid, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd);
                    //   }
                }
                else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "DNCash Recept")
                {
                    string[] strfinalarray = new string[5] { "DN", "D", "GSTVOUCHERDN", "DN", "" };
                    string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[3].Text.Remove(0, 9);
                    int clientid = Convert.ToInt32(LVledger.Items[LVledger.FocusedItem.Index].SubItems[8].Text);
                    String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                    //  DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                    GSTVouchers bd = new GSTVouchers(this, master, tabControl, strfinalarray);
                    // Sale p = new Sale(this, master, tabControl);
                    //   if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                    //    {
                    bd.updatemode(str, billno, clientid, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd);
                    //   }
                }
                else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "CNCash Invoice")
                {
                    string[] strfinalarray = new string[5] { "CN", "C", "GSTVOUCHERCN", "CN", "" };
                    string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[3].Text.Remove(0, 9);
                    int clientid = Convert.ToInt32(LVledger.Items[LVledger.FocusedItem.Index].SubItems[8].Text);
                    String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                    //  DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                    GSTVouchers bd = new GSTVouchers(this, master, tabControl, strfinalarray);
                    // Sale p = new Sale(this, master, tabControl);
                    //   if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                    //    {
                    bd.updatemode(str, billno, clientid, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd);
                    //   }
                }
                else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "GSTVOUCHERSALE")
                {
                    string[] strfinalarray = new string[5] { "S", "D", "GSTVOUCHERS", "S", "" };
                    string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[3].Text.Remove(0, 9);
                    int clientid = Convert.ToInt32(LVledger.Items[LVledger.FocusedItem.Index].SubItems[8].Text);
                    String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                    //  DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                    GSTVouchers bd = new GSTVouchers(this, master, tabControl, strfinalarray);
                    // Sale p = new Sale(this, master, tabControl);
                    //   if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                    //    {
                    bd.updatemode(str, billno, clientid, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd);
                    //   }
                }
                else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "GSTVOUCHERP")
                {
                    string[] strfinalarray = new string[5] { "P", "C", "GSTVOUCHERP", "P", "" };
                    string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[3].Text.Remove(0, 9);
                    int clientid = Convert.ToInt32(LVledger.Items[LVledger.FocusedItem.Index].SubItems[8].Text);
                    String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                    //  DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                    GSTVouchers bd = new GSTVouchers(this, master, tabControl, strfinalarray);
                    // Sale p = new Sale(this, master, tabControl);
                    //   if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                    //    {
                    bd.updatemode(str, billno, clientid, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd);
                    //   }
                }
                else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "GSTVOUCHERSR")
                {
                    string[] strfinalarray = new string[5] { "SR", "C", "GSTVOUCHERSR", "SR", "" };
                    string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[3].Text.Remove(0, 9);
                    int clientid = Convert.ToInt32(LVledger.Items[LVledger.FocusedItem.Index].SubItems[8].Text);
                    String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                    //  DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                    GSTVouchers bd = new GSTVouchers(this, master, tabControl, strfinalarray);
                    // Sale p = new Sale(this, master, tabControl);
                    //   if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                    //    {
                    bd.updatemode(str, billno, clientid, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd);
                    //   }
                }
                else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "GSTVOUCHERPR")
                {
                    string[] strfinalarray = new string[5] { "CN", "C", "GSTVOUCHERCN", "CN", "" };
                    string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[3].Text.Remove(0, 9);
                    int clientid = Convert.ToInt32(LVledger.Items[LVledger.FocusedItem.Index].SubItems[8].Text);
                    String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                    //  DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                    GSTVouchers bd = new GSTVouchers(this, master, tabControl, strfinalarray);
                    // Sale p = new Sale(this, master, tabControl);
                    //   if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                    //    {
                    bd.updatemode(str, billno, clientid, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd);
                    //   }
                }
                else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "GSTVOUCHERCN")
                {
                    string[] strfinalarray = new string[5] { "CN", "C", "GSTVOUCHERCN", "CN", "" };
                    string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[3].Text.Remove(0, 9);
                    int clientid = Convert.ToInt32(LVledger.Items[LVledger.FocusedItem.Index].SubItems[8].Text);
                    String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                    //  DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                    GSTVouchers bd = new GSTVouchers(this, master, tabControl, strfinalarray);
                    // Sale p = new Sale(this, master, tabControl);
                    //   if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                    //    {
                    bd.updatemode(str, billno, clientid, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd);
                    //   }
                }
                else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "GSTVOUCHERDN")
                {
                    string[] strfinalarray = new string[5] { "DN", "D", "GSTVOUCHERDN", "DN", "" };
                    string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[3].Text.Remove(0, 9);
                    int clientid = Convert.ToInt32(LVledger.Items[LVledger.FocusedItem.Index].SubItems[8].Text);
                    String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                    //  DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                    GSTVouchers bd = new GSTVouchers(this, master, tabControl, strfinalarray);
                    // Sale p = new Sale(this, master, tabControl);
                    //   if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                    //    {
                    bd.updatemode(str, billno, clientid, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd);
                    //   }
                }
                else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Sale")
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[0]["v"].ToString() == "True" || userrights.Rows[0]["u"].ToString() == "True")
                        {
                            string[] strfinalarray = new string[5] { "S", "D", "Sale", "S", "" };
                            string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[3].Text.Remove(0, 9);
                            int clientid = Convert.ToInt32(LVledger.Items[LVledger.FocusedItem.Index].SubItems[8].Text);
                            String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                            DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                            DefaultSale bd = new DefaultSale(this, master, tabControl, strfinalarray);
                            // Sale p = new Sale(this, master, tabControl);
                            if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                            {
                                bd.updatemode(str, billno, clientid, strfinalarray);
                                //bd.MdiParent = this.MdiParent;
                                //bd.StartPosition = FormStartPosition.CenterScreen;
                                //bd.Show();
                                master.AddNewTab(bd);
                            }
                        }
                        else
                        {
                            MessageBox.Show("You don't have Permission To View");
                            return;
                        }
                    }

                }

                else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Cash Recept")
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[0]["v"].ToString() == "True" || userrights.Rows[0]["u"].ToString() == "True")
                        {
                            string[] strfinalarray = new string[5] { "S", "D", "Sale", "S", "" };
                            string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[3].Text.Remove(0, 9);
                            int clientid = Convert.ToInt32(LVledger.Items[LVledger.FocusedItem.Index].SubItems[8].Text);
                            String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                            DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                            DefaultSale bd = new DefaultSale(this, master, tabControl, strfinalarray);
                            // Sale p = new Sale(this, master, tabControl);
                            if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                            {
                                bd.updatemode(str, billno, clientid, strfinalarray);
                                //bd.MdiParent = this.MdiParent;
                                //bd.StartPosition = FormStartPosition.CenterScreen;
                                //bd.Show();
                                master.AddNewTab(bd);
                            }
                        }
                        else
                        {
                            MessageBox.Show("You don't have Permission To View");
                            return;
                        }
                    }

                }
                else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Purchase")
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[3]["v"].ToString() == "True" || userrights.Rows[3]["u"].ToString() == "True")
                        {
                            string[] strfinalarray = new string[5] { "P", "C", "Purchase", "P", "" };
                            string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[3].Text.Remove(0, 9);
                            String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                            int clientid = Convert.ToInt32(LVledger.Items[LVledger.FocusedItem.Index].SubItems[8].Text);
                            DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                            //  DefaultPurchase frm = new DefaultPurchase(this);
                            DefaultSale bd = new DefaultSale(this, master, tabControl, strfinalarray);
                            //Purchase p = new Purchase(this, master, tabControl);
                            if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                            {
                                bd.updatemode(str, billno, clientid, strfinalarray);
                                //bd.MdiParent = this.MdiParent;
                                //bd.StartPosition = FormStartPosition.CenterScreen;
                                //bd.Show();
                                master.AddNewTab(bd);
                            }
                        }
                        else
                        {
                            MessageBox.Show("You don't have Permission To View");
                            return;
                        }
                    }

                }
                else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Cash Invoice")
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[3]["v"].ToString() == "True" || userrights.Rows[3]["u"].ToString() == "True")
                        {
                            string[] strfinalarray = new string[5] { "P", "C", "Purchase", "P", "" };
                            string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[3].Text.Remove(0, 9);
                            String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                            int clientid = Convert.ToInt32(LVledger.Items[LVledger.FocusedItem.Index].SubItems[8].Text);
                            DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                            //  DefaultPurchase frm = new DefaultPurchase(this);
                            DefaultSale bd = new DefaultSale(this, master, tabControl, strfinalarray);
                            //Purchase p = new Purchase(this, master, tabControl);
                            if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                            {
                                bd.updatemode(str, billno, clientid, strfinalarray);
                                //bd.MdiParent = this.MdiParent;
                                //bd.StartPosition = FormStartPosition.CenterScreen;
                                //bd.Show();
                                master.AddNewTab(bd);
                            }
                        }
                        else
                        {
                            MessageBox.Show("You don't have Permission To View");
                            return;
                        }
                    }

                }
                else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Sale Return")
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[13]["v"].ToString() == "True" || userrights.Rows[13]["u"].ToString() == "True")
                        {
                            string[] strfinalarray = new string[5] { "SR", "C", "SaleReturn", "SR", "" };
                            string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[3].Text.Remove(0, 16);
                            int clientid = Convert.ToInt32(LVledger.Items[LVledger.FocusedItem.Index].SubItems[8].Text);
                            String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                            DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                            DefaultSale bd = new DefaultSale(this, master, tabControl, strfinalarray);
                            //    Sale p = new Sale(this, master, tabControl);
                            if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                            {
                                bd.updatemode(str, billno, clientid, strfinalarray);
                                //bd.MdiParent = this.MdiParent;
                                //bd.StartPosition = FormStartPosition.CenterScreen;
                                //bd.Show();
                                master.AddNewTab(bd);
                            }
                        }
                        else
                        {
                            MessageBox.Show("You don't have Permission To View");
                            return;
                        }
                    }

                }
                else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Purchase Return")
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[16]["v"].ToString() == "True" || userrights.Rows[16]["u"].ToString() == "True")
                        {
                            string[] strfinalarray = new string[5] { "PR", "D", "PurchaseReturn", "PR", "" };
                            string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[3].Text.Remove(0, 16);
                            int clientid = Convert.ToInt32(LVledger.Items[LVledger.FocusedItem.Index].SubItems[8].Text);
                            String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                            DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                            //  DefaultPurchase frm = new DefaultPurchase(this);
                            DefaultSale bd = new DefaultSale(this, master, tabControl, strfinalarray);
                            //  Purchase p = new Purchase(this, master, tabControl);
                            if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                            {
                                bd.updatemode(str, billno, clientid, strfinalarray);
                                //bd.MdiParent = this.MdiParent;
                                //bd.StartPosition = FormStartPosition.CenterScreen;
                                //bd.Show();
                                master.AddNewTab(bd);
                            }
                        }
                        else
                        {
                            MessageBox.Show("You don't have Permission To View");
                            return;
                        }
                    }

                }
                else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Rect")
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[2]["v"].ToString() == "True" || userrights.Rows[2]["u"].ToString() == "True")
                        {
                            String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[2].Text;

                            QReceipt bd = new QReceipt(master, tabControl);
                            bd.updatemode(str, mouseclickid.Rows[LVledger.FocusedItem.Index][1].ToString(), 1, LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text);
                            master.AddNewTab(bd);
                            //bd.Show();
                        }
                        else
                        {
                            MessageBox.Show("You don't have Permission To View");
                            return;
                        }
                    }

                }
                else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Pmnt")
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[5]["v"].ToString() == "True" || userrights.Rows[5]["u"].ToString() == "True")
                        {
                            String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[2].Text;

                            QPayment bd = new QPayment(master, tabControl);
                            bd.updatemode(str, mouseclickid.Rows[LVledger.FocusedItem.Index][1].ToString(), 1, LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text);
                            master.AddNewTab(bd);
                            //bd.Show();
                        }
                        else
                        {
                            MessageBox.Show("You don't have Permission To View");
                            return;
                        }
                    }

                }
                else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Cheque Issued")
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[20]["v"].ToString() == "True" || userrights.Rows[20]["u"].ToString() == "True")
                        {
                            String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[7].Text;
                            BankEntry be = new BankEntry(master, tabControl);
                            be.updatemode(str, LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text);
                            master.AddNewTab(be);
                        }
                        else
                        {
                            MessageBox.Show("You don't have Permission To View");
                            return;
                        }
                    }
                }
                else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Draft Issued")
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[20]["v"].ToString() == "True" || userrights.Rows[20]["u"].ToString() == "True")
                        {
                            String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[7].Text;
                            BankEntry be = new BankEntry(master, tabControl);
                            be.updatemode(str, LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text);
                            master.AddNewTab(be);
                        }
                        else
                        {
                            MessageBox.Show("You don't have Permission To View");
                            return;
                        }
                    }

                }
                else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Cheque/Draft/Rtgs Received")
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[20]["v"].ToString() == "True" || userrights.Rows[20]["u"].ToString() == "True")
                        {
                            String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[7].Text;
                            BankEntry be = new BankEntry(master, tabControl);
                            be.updatemode(str, LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text);
                            master.AddNewTab(be);
                        }
                        else
                        {
                            MessageBox.Show("You don't have Permission To View");
                            return;
                        }
                    }

                }
                else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Deposit Cash Into Bank")
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[20]["v"].ToString() == "True" || userrights.Rows[20]["u"].ToString() == "True")
                        {
                            String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[7].Text;
                            BankEntry be = new BankEntry(master, tabControl);
                            be.updatemode(str, LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text);
                            master.AddNewTab(be);
                        }
                        else
                        {
                            MessageBox.Show("You don't have Permission To View");
                            return;
                        }
                    }

                }
                else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Withdraw Cash from Bank")
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[20]["v"].ToString() == "True" || userrights.Rows[20]["u"].ToString() == "True")
                        {
                            String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[7].Text;
                            BankEntry be = new BankEntry(master, tabControl);
                            be.updatemode(str, LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text);
                            master.AddNewTab(be);
                        }
                        else
                        {
                            MessageBox.Show("You don't have Permission To View");
                            return;
                        }
                    }

                }
                else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Bank Expenses")
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[20]["v"].ToString() == "True" || userrights.Rows[20]["u"].ToString() == "True")
                        {
                            String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[7].Text;
                            BankEntry be = new BankEntry(master, tabControl);
                            be.updatemode(str, LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text);
                            master.AddNewTab(be);
                        }
                        else
                        {
                            MessageBox.Show("You don't have Permission To View");
                            return;
                        }
                    }

                }
                else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Online Transfer")
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[20]["v"].ToString() == "True" || userrights.Rows[20]["u"].ToString() == "True")
                        {
                            String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[7].Text;
                            BankEntry be = new BankEntry(master, tabControl);
                            be.updatemode(str, LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text);
                            master.AddNewTab(be);
                        }
                        else
                        {
                            MessageBox.Show("You don't have Permission To View");
                            return;
                        }
                    }

                }
                else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "DEBIT NOTE")
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[6]["v"].ToString() == "True" || userrights.Rows[6]["u"].ToString() == "True")
                        {
                            string[] debitorcredit = new string[5] { "D", "", "", "", "" };
                            String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[7].Text;
                            DebitandCreditNote be = new DebitandCreditNote(master, tabControl);
                            be.updatemode(str, LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text, debitorcredit);
                            master.AddNewTab(be);
                        }
                        else
                        {
                            MessageBox.Show("You don't have Permission To View");
                            return;
                        }
                    }

                }
                else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "CREDIT NOTE")
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[7]["v"].ToString() == "True" || userrights.Rows[7]["u"].ToString() == "True")
                        {
                            string[] debitorcredit = new string[5] { "C", "", "", "", "" };
                            String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[7].Text;
                            DebitandCreditNote be = new DebitandCreditNote(master, tabControl);
                            be.updatemode(str, LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text, debitorcredit);
                            master.AddNewTab(be);
                        }
                        else
                        {
                            MessageBox.Show("You don't have Permission To View");
                            return;
                        }
                    }

                }
            }
            catch
            {
            }
        }
        private void LVledger_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            open();
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
                        DataTable client = conn.getdataset("select * from clientmaster where isactive=1 and clientID='" + cmbaccname.SelectedValue + "'");
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
                            qry += "('" + date + "','" + type + "','" + Account + "','" + drAmount + "','" + crAmount + "','" + balance + "','" + txttotdebit.Text + "','" + txttotcredit.Text + "','" + txtbalance.Text + "','" + txtopbal.Text + "','" + dt1.Rows[0][0].ToString() + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][2].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + dt1.Rows[0][11].ToString() + "','" + dt1.Rows[0][12].ToString() + "','" + dt1.Rows[0][13].ToString() + "','" + DTPFrom.Text + "','" + DTPTo.Text + "','" + client.Rows[0][0].ToString() + "','" + client.Rows[0][1].ToString() + "','" + client.Rows[0][2].ToString() + "','" + client.Rows[0][3].ToString() + "','" + client.Rows[0][4].ToString() + "','" + client.Rows[0][5].ToString() + "','" + client.Rows[0][6].ToString() + "','" + client.Rows[0][7].ToString() + "','" + client.Rows[0][8].ToString() + "','" + client.Rows[0][9].ToString() + "','" + client.Rows[0][10].ToString() + "','" + client.Rows[0][11].ToString() + "','" + client.Rows[0][12].ToString() + "','" + client.Rows[0][13].ToString() + "','" + Remarks + "')";
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
            txtopbal.BackColor = Color.LightYellow;
        }

        private void txtopbal_Leave(object sender, EventArgs e)
        {
            txtopbal.BackColor = Color.White;
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


        internal void Updateoutstanding(int p, string iid, string stadate, string curdate)
        {
            try
            {
                cnt = 1;
                DataTable client = conn.getdataset("select ClientID from ClientMaster where isactive=1 and AccountName='" + iid + "'");
                string val = client.Rows[0]["ClientID"].ToString();
                loadpage();
                cmbaccname.Text = iid;
                cmbaccname.SelectedValue = val;


                if (cmbaccname.Text != "")
                {
                    if ((cmbaccname.Text).ToUpper() == "CASH" || cmbaccname.SelectedValue == "25")
                    {
                        #region
                        string totaldebit = conn.ExecuteScalar("select sum(amount) from Ledger where Date1 < '" + stadate + "' and OT1='" + cmbaccname.Text + "' and dc='D' and isactive=1 order by Date1");
                        string totalcredit = conn.ExecuteScalar("select sum(amount) from Ledger where Date1 < '" + stadate + "' and OT1='" + cmbaccname.Text + "' and dc='C' and isactive=1 order by Date1");
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

                        //for (int i = 0; i < OPdt.Rows.Count; i++)
                        //{
                        //    Double opbal = 0;
                        //    if (OPdt.Rows[i]["DC"].ToString() == "D")
                        //    {

                        //    }
                        //    else if (OPdt.Rows[i]["TranType"].ToString() == "Rect")
                        //    {
                        //        ListViewItem li;
                        //        li = LVledger.Items.Add(Convert.ToDateTime(OPdt.Rows[i]["Date1"].ToString()).ToString("dd-MMM-yyyy"));
                        //        li.SubItems.Add("Rect");
                        //        li.SubItems.Add(OPdt.Rows[i]["OT1"].ToString());
                        //        li.SubItems.Add("By Rcpt. No.: " + OPdt.Rows[i]["VoucherID"].ToString() + "; " + OPdt.Rows[i]["ShortNarration"].ToString());
                        //        li.SubItems.Add("");
                        //        li.SubItems.Add(Math.Round(Convert.ToDouble(OPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        //        li.SubItems.Add("Rect");
                        //    }
                        //}
                        #endregion

                        //for create ledger
                        mouseclickid.Rows.Clear();
                        LVledger.Items.Clear();
                        #region
                        DataTable SPdt = conn.getdataset("select * from Ledger where isactive=1 and Date1 between '" + stadate + "' and '" + curdate + "' and OT1='" + cmbaccname.Text + "' order by Date1");
                        string balance = "0.00";
                        Double debit = 0, credit = 0;
                        for (int i = 0; i < SPdt.Rows.Count; i++)
                        {
                            if (SPdt.Rows[i]["TranType"].ToString() == "EXPCash Recept")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("EXPCash Recept");
                                li.SubItems.Add("EXPCash Recept");
                                li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add("");
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                mouseclickid.Rows.Add("EXPCash Recept", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(client.Rows[0]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "EXPCash Invoice")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("EXPCash Invoice");
                                li.SubItems.Add("EXPCash Invoice");
                                li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                li.SubItems.Add("");
                                mouseclickid.Rows.Add("EXPCash Invoice", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(client.Rows[0]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "PRCash Recept")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("PRCash Recept");
                                li.SubItems.Add("PRCash Recept");
                                li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add("");
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                mouseclickid.Rows.Add("PRCash Recept", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(client.Rows[0]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "SRCash Invoice")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("SRCash Invoice");
                                li.SubItems.Add("SRCash Invoice");
                                li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                li.SubItems.Add("");
                                mouseclickid.Rows.Add("SRCash Invoice", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(client.Rows[0]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "SaleCash Recept")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("SaleCash Recept");
                                li.SubItems.Add("SaleCash Recept");
                                li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add("");
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                mouseclickid.Rows.Add("SaleCash Recept", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(client.Rows[0]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "PurchaseCash Invoice")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("PurchaseCash Invoice");
                                li.SubItems.Add("PurchaseCash Invoice");
                                li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                li.SubItems.Add("");
                                mouseclickid.Rows.Add("PurchaseCash Invoice", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(client.Rows[0]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "DNCash Recept")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("DNCash Recept");
                                li.SubItems.Add("DNCash Recept");
                                li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add("");
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                mouseclickid.Rows.Add("DNCash Recept", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(client.Rows[0]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "CNCash Invoice")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("CNCash Invoice");
                                li.SubItems.Add("CNCash Invoice");
                                li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                li.SubItems.Add("");
                                mouseclickid.Rows.Add("CNCash Invoice", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(client.Rows[0]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHEREXP")
                            {
                                if (SPdt.Rows[i]["dc"].ToString() == "D")
                                {
                                    ListViewItem li;
                                    li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                    li.SubItems.Add("EXPVOUCHERSALE");
                                    li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                                    li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                                    debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    li.SubItems.Add("");
                                    mouseclickid.Rows.Add("Sale", SPdt.Rows[i]["VoucherID"].ToString());
                                    if (i != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();

                                        opbal = Convert.ToDouble(str[0]);
                                    }

                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                    li.SubItems.Add(balance);
                                    li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                    li.SubItems.Add(client.Rows[0]["ClientID"].ToString());
                                }
                                else
                                {
                                    ListViewItem li;
                                    li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                    li.SubItems.Add("EXPVOUCHERPURCHASE");
                                    li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                                    li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                                    li.SubItems.Add("");
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                    mouseclickid.Rows.Add("Purchase", SPdt.Rows[i]["VoucherID"].ToString());
                                    if (i != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();

                                        opbal = Convert.ToDouble(str[0]);
                                    }

                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                    li.SubItems.Add(balance);
                                    li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                    li.SubItems.Add(client.Rows[0]["ClientID"].ToString());
                                }
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERDN")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("GSTVOUCHERDN");
                                li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                                li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
                                string CD = SPdt.Rows[i]["dc"].ToString();
                                if (CD == "D")
                                {
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                    li.SubItems.Add("");
                                }
                                else
                                {
                                    li.SubItems.Add("");
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                                }

                                mouseclickid.Rows.Add("GSTVOUCHERDN", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(client.Rows[0]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERCN")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("GSTVOUCHERCN");
                                li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                                li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
                                string CD = SPdt.Rows[i]["dc"].ToString();
                                if (CD == "D")
                                {
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                    li.SubItems.Add("");
                                }
                                else
                                {
                                    li.SubItems.Add("");
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                                }

                                mouseclickid.Rows.Add("GSTVOUCHERCN", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(client.Rows[0]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERS")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("GSTVOUCHERSALE");
                                li.SubItems.Add("Sales");
                                li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                li.SubItems.Add("");
                                mouseclickid.Rows.Add("GSTVOUCHERSALE", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(client.Rows[0]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERSR")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("GSTVOUCHERSR");
                                li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                                li.SubItems.Add("Return Bill No: " + SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add("");
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                mouseclickid.Rows.Add("GSTVOUCHERSR", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(client.Rows[0]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERP")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("GSTVOUCHERP");
                                li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                                li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add("");
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                mouseclickid.Rows.Add("GSTVOUCHERP", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();

                                    opbal = Convert.ToDouble(str[0]);
                                }

                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(client.Rows[0]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERPR")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("GSTVOUCHERPR");
                                li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                                li.SubItems.Add("Return Bill No: " + SPdt.Rows[i]["VoucherID"].ToString());

                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                li.SubItems.Add("");
                                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                mouseclickid.Rows.Add("Purchase Return", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
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
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(client.Rows[0]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "Sale")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("Sale");
                                li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                                li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                li.SubItems.Add("");
                                mouseclickid.Rows.Add("Sale", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();

                                    opbal = Convert.ToDouble(str[0]);
                                }

                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(client.Rows[i]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "Rect")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("Rect");
                                li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                                li.SubItems.Add("By Rcpt. No.: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["ShortNarration"].ToString());
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                li.SubItems.Add("");
                                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                mouseclickid.Rows.Add("Rect", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                string CD = SPdt.Rows[i]["dc"].ToString();
                                if (CD == "C")
                                {
                                    CD = "D";
                                }
                                else
                                {
                                    CD = "C";
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);

                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "SaleReturn")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("Sale Return");
                                li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                                li.SubItems.Add("Return Bill No: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["ShortNarration"].ToString());
                                li.SubItems.Add("");
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                mouseclickid.Rows.Add("Sale Return", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();

                                    opbal = Convert.ToDouble(str[0]);
                                }
                                if (DC == "C")
                                {
                                    DC = "D";
                                }
                                else
                                {
                                    DC = "C";
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(client.Rows[i]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "Purchase")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("Purchase");
                                li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                                li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add("");
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                mouseclickid.Rows.Add("Purchase", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();

                                    opbal = Convert.ToDouble(str[0]);
                                }

                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(client.Rows[i]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "PurchaseReturn")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("Purchase Return");
                                li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                                li.SubItems.Add("Return Bill No: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["ShortNarration"].ToString());
                                li.SubItems.Add("");
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                mouseclickid.Rows.Add("Purchase Return", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();

                                    opbal = Convert.ToDouble(str[0]);
                                }
                                if (DC == "C")
                                {
                                    DC = "D";
                                }
                                else
                                {
                                    DC = "C";
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(client.Rows[i]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "Pmnt")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("Pmnt");
                                li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                                li.SubItems.Add("By Rcpt. No.: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["ShortNarration"].ToString());
                                li.SubItems.Add("");
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));

                                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                mouseclickid.Rows.Add("Pmnt", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();

                                    opbal = Convert.ToDouble(str[0]);
                                }
                                string CD = SPdt.Rows[i]["dc"].ToString();
                                if (CD == "C")
                                {
                                    CD = "D";
                                }
                                else
                                {
                                    CD = "C";
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "Cheque Issued")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("Cheque Issued");
                                li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                                string CD = SPdt.Rows[i]["dc"].ToString();
                                li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                                if (CD == "D")
                                {
                                    //  li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                                    li.SubItems.Add("");
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                }
                                else
                                {
                                    li.SubItems.Add("");
                                    //  li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                }
                                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                mouseclickid.Rows.Add("Cheque Issued", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();

                                    opbal = Convert.ToDouble(str[0]);
                                }
                                CD = SPdt.Rows[i]["dc"].ToString();
                                if (CD == "C")
                                {
                                    CD = "D";
                                }
                                else
                                {
                                    CD = "C";
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "Draft Issued")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("Draft Issued");
                                li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                                string CD = SPdt.Rows[i]["dc"].ToString();
                                li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                                if (CD == "D")
                                {
                                    //  li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                                    li.SubItems.Add("");
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                }
                                else
                                {
                                    li.SubItems.Add("");
                                    //li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                }
                                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                mouseclickid.Rows.Add("Draft Issued", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();

                                    opbal = Convert.ToDouble(str[0]);
                                }
                                CD = SPdt.Rows[i]["dc"].ToString();
                                if (CD == "C")
                                {
                                    CD = "D";
                                }
                                else
                                {
                                    CD = "C";
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "Cheque/Draft/Rtgs Received")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("Cheque/Draft/Rtgs Received");
                                li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                                string CD = SPdt.Rows[i]["dc"].ToString();
                                li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                                if (CD == "D")
                                {
                                    // li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                                    li.SubItems.Add("");
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                }
                                else
                                {
                                    li.SubItems.Add("");
                                    // li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                }
                                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                mouseclickid.Rows.Add("Cheque/Draft/Rtgs Received", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();

                                    opbal = Convert.ToDouble(str[0]);
                                }
                                CD = SPdt.Rows[i]["dc"].ToString();
                                if (CD == "C")
                                {
                                    CD = "D";
                                }
                                else
                                {
                                    CD = "C";
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                            }

                            else if (SPdt.Rows[i]["TranType"].ToString() == "Deposit Cash Into Bank")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("Deposit Cash Into Bank");
                                li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                                string CD = SPdt.Rows[i]["dc"].ToString();
                                li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                                if (CD == "D")
                                {
                                    //  li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                                    li.SubItems.Add("");
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                }
                                else
                                {
                                    li.SubItems.Add("");
                                    // li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                }
                                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                mouseclickid.Rows.Add("Deposit Cash Into Bank", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();

                                    opbal = Convert.ToDouble(str[0]);
                                }
                                CD = SPdt.Rows[i]["dc"].ToString();
                                if (CD == "C")
                                {
                                    CD = "D";
                                }
                                else
                                {
                                    CD = "C";
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                            }

                            else if (SPdt.Rows[i]["TranType"].ToString() == "Withdraw Cash from Bank")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("Withdraw Cash from Bank");
                                li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                                string CD = SPdt.Rows[i]["dc"].ToString();
                                li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                                if (CD == "D")
                                {
                                    //  li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                                    li.SubItems.Add("");
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                }
                                else
                                {
                                    li.SubItems.Add("");
                                    //  li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                }
                                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                mouseclickid.Rows.Add("Withdraw Cash from Bank", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();

                                    opbal = Convert.ToDouble(str[0]);
                                }
                                CD = SPdt.Rows[i]["dc"].ToString();
                                if (CD == "C")
                                {
                                    CD = "D";
                                }
                                else
                                {
                                    CD = "C";
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                            }

                            else if (SPdt.Rows[i]["TranType"].ToString() == "Bank Expenses")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("Bank Expenses");
                                li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                                string CD = SPdt.Rows[i]["dc"].ToString();
                                li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                                if (CD == "D")
                                {
                                    // li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                                    li.SubItems.Add("");
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                }
                                else
                                {
                                    li.SubItems.Add("");
                                    //   li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                }
                                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                mouseclickid.Rows.Add("Bank Expenses", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();

                                    opbal = Convert.ToDouble(str[0]);
                                }
                                CD = SPdt.Rows[i]["dc"].ToString();
                                if (CD == "C")
                                {
                                    CD = "D";
                                }
                                else
                                {
                                    CD = "C";
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                            }

                            else if (SPdt.Rows[i]["TranType"].ToString() == "Online Transfer")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("Online Transfer");
                                li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                                string CD = SPdt.Rows[i]["dc"].ToString();
                                li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                                if (CD == "D")
                                {
                                    //li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                                    li.SubItems.Add("");
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                }
                                else
                                {
                                    li.SubItems.Add("");
                                    // li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                }
                                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                mouseclickid.Rows.Add("Online Transfer", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();

                                    opbal = Convert.ToDouble(str[0]);
                                }
                                CD = SPdt.Rows[i]["dc"].ToString();
                                if (CD == "C")
                                {
                                    CD = "D";
                                }
                                else
                                {
                                    CD = "C";
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "DEBIT NOTE")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("DEBIT NOTE");
                                li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                                li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
                                string CD = SPdt.Rows[i]["dc"].ToString();
                                if (CD == "D")
                                {
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                    li.SubItems.Add("");
                                }
                                else
                                {
                                    li.SubItems.Add("");
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                                }

                                mouseclickid.Rows.Add("DEBIT NOTE", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "CREDIT NOTE")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("CREDIT NOTE");
                                li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                                li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
                                string CD = SPdt.Rows[i]["dc"].ToString();
                                if (CD == "D")
                                {
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                    li.SubItems.Add("");
                                }
                                else
                                {
                                    li.SubItems.Add("");
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                                }

                                mouseclickid.Rows.Add("CREDIT NOTE", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                            }
                        }
                        #endregion

                        txttotdebit.Text = debit.ToString("N2");
                        txttotcredit.Text = credit.ToString("N2");
                        txtbalance.Text = balance;
                    }
                    else
                    {
                        //for calculate OpBalance
                        #region
                        string totaldebit = conn.ExecuteScalar("select sum(amount) from Ledger where Date1 < '" + stadate + "' and Accountid='" + cmbaccname.SelectedValue + "' and dc='D' and isactive=1 order by Date1");
                        string totalcredit = conn.ExecuteScalar("select sum(amount) from Ledger where Date1 < '" + stadate + "' and Accountid='" + cmbaccname.SelectedValue + "' and dc='C' and isactive=1 order by Date1");
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

                        //for (int i = 0; i < OPdt.Rows.Count; i++)
                        //{
                        //    Double opbal = 0;
                        //    if (OPdt.Rows[i]["DC"].ToString() == "D")
                        //    {

                        //    }
                        //    else if (OPdt.Rows[i]["TranType"].ToString() == "Rect")
                        //    {
                        //        ListViewItem li;
                        //        li = LVledger.Items.Add(Convert.ToDateTime(OPdt.Rows[i]["Date1"].ToString()).ToString("dd-MMM-yyyy"));
                        //        li.SubItems.Add("Rect");
                        //        li.SubItems.Add(OPdt.Rows[i]["OT1"].ToString());
                        //        li.SubItems.Add("By Rcpt. No.: " + OPdt.Rows[i]["VoucherID"].ToString() + "; " + OPdt.Rows[i]["ShortNarration"].ToString());
                        //        li.SubItems.Add("");
                        //        li.SubItems.Add(Math.Round(Convert.ToDouble(OPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        //        li.SubItems.Add("Rect");
                        //    }
                        //}
                        #endregion
                        //for create ledger
                        mouseclickid.Rows.Clear();
                        LVledger.Items.Clear();
                        #region
                        DataTable SPdt = conn.getdataset("select * from Ledger where isactive=1 and Date1 between '" + stadate + "' and '" + curdate + "' and Accountid='" + cmbaccname.SelectedValue + "' order by Date1");
                        string balance = "0.00";
                        Double debit = 0, credit = 0;
                        for (int i = 0; i < SPdt.Rows.Count; i++)
                        {
                            if (SPdt.Rows[i]["TranType"].ToString() == "EXPCash Recept")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("EXPCash Recept");
                                li.SubItems.Add("EXPCash Recept");
                                li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add("");
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                mouseclickid.Rows.Add("EXPCash Recept", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "EXPCash Invoice")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("EXPCash Invoice");
                                li.SubItems.Add("EXPCash Invoice");
                                li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                li.SubItems.Add("");
                                mouseclickid.Rows.Add("EXPCash Invoice", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "PRCash Recept")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("PRCash Recept");
                                li.SubItems.Add("PRCash Recept");
                                li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add("");
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                mouseclickid.Rows.Add("PRCash Recept", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "SRCash Invoice")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("SRCash Invoice");
                                li.SubItems.Add("SRCash Invoice");
                                li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                li.SubItems.Add("");
                                mouseclickid.Rows.Add("SRCash Invoice", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "SaleCash Recept")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("SaleCash Recept");
                                li.SubItems.Add("SaleCash Recept");
                                li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add("");
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                mouseclickid.Rows.Add("SaleCash Recept", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "PurchaseCash Invoice")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("PurchaseCash Invoice");
                                li.SubItems.Add("PurchaseCash Invoice");
                                li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                li.SubItems.Add("");
                                mouseclickid.Rows.Add("PurchaseCash Invoice", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "DNCash Recept")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("DNCash Recept");
                                li.SubItems.Add("DNCash Recept");
                                li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add("");
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                mouseclickid.Rows.Add("DNCash Recept", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "CNCash Invoice")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("CNCash Invoice");
                                li.SubItems.Add("CNCash Invoice");
                                li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                li.SubItems.Add("");
                                mouseclickid.Rows.Add("CNCash Invoice", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHEREXP")
                            {
                                if (SPdt.Rows[i]["dc"].ToString() == "D")
                                {
                                    ListViewItem li;
                                    li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                    li.SubItems.Add("EXPVOUCHERSALE");
                                    li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                                    li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                                    debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    li.SubItems.Add("");
                                    mouseclickid.Rows.Add("Exp Voucher Sale", SPdt.Rows[i]["VoucherID"].ToString());
                                    if (i != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();

                                        opbal = Convert.ToDouble(str[0]);
                                    }

                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                    li.SubItems.Add(balance);
                                    li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                    li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                                }
                                else
                                {
                                    ListViewItem li;
                                    li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                    li.SubItems.Add("EXPVOUCHERPURCHASE");
                                    li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                                    li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                                    li.SubItems.Add("");
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                    mouseclickid.Rows.Add("Exp VoucherPurchase", SPdt.Rows[i]["VoucherID"].ToString());
                                    if (i != 0)
                                    {
                                        string[] str = balance.Split(' ');
                                        char temp = str[1][0];
                                        DC = temp.ToString();

                                        opbal = Convert.ToDouble(str[0]);
                                    }

                                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                    li.SubItems.Add(balance);
                                    li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                    li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                                }
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERDN")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("GSTVOUCHERDN");
                                li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                                li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
                                string CD = SPdt.Rows[i]["dc"].ToString();
                                if (CD == "D")
                                {
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                    li.SubItems.Add("");
                                }
                                else
                                {
                                    li.SubItems.Add("");
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                                }

                                mouseclickid.Rows.Add("GSTVOUCHERDN", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERCN")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("GSTVOUCHERCN");
                                li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                                li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
                                string CD = SPdt.Rows[i]["dc"].ToString();
                                if (CD == "D")
                                {
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                    li.SubItems.Add("");
                                }
                                else
                                {
                                    li.SubItems.Add("");
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                                }

                                mouseclickid.Rows.Add("GSTVOUCHERCN", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERS")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("GSTVOUCHERSALE");
                                li.SubItems.Add("Sales");
                                li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                li.SubItems.Add("");
                                mouseclickid.Rows.Add("GSTVOUCHERSALE", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERSR")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("GSTVOUCHERSR");
                                li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                                li.SubItems.Add("Return Bill No: " + SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add("");
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                mouseclickid.Rows.Add("GSTVOUCHERSR", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERP")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("GSTVOUCHERP");
                                li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                                li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add("");
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                mouseclickid.Rows.Add("GSTVOUCHERP", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();

                                    opbal = Convert.ToDouble(str[0]);
                                }

                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "GSTVOUCHERPR")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("GSTVOUCHERPR");
                                li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                                li.SubItems.Add("Return Bill No: " + SPdt.Rows[i]["VoucherID"].ToString());

                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                li.SubItems.Add("");
                                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                mouseclickid.Rows.Add("Purchase Return", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
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
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "Sale")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("Sale");
                                li.SubItems.Add("Sales");
                                li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                li.SubItems.Add("");
                                mouseclickid.Rows.Add("Sale", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "Rect")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("Rect");
                                li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                                li.SubItems.Add("By Rcpt. No.: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["ShortNarration"].ToString());
                                li.SubItems.Add("");
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                mouseclickid.Rows.Add("Rect", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "SaleReturn")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("Sale Return");
                                li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                                li.SubItems.Add("Return Bill No: " + SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add("");
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                mouseclickid.Rows.Add("Sale Return", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "Purchase")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("Purchase");
                                li.SubItems.Add("Purchases");
                                li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add("");
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                mouseclickid.Rows.Add("Purchase", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                                li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "PurchaseReturn")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("Purchase Return");
                                li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                                li.SubItems.Add("Return Bill No: " + SPdt.Rows[i]["VoucherID"].ToString());

                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                li.SubItems.Add("");
                                credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                mouseclickid.Rows.Add("Purchase Return", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();

                                    opbal = Convert.ToDouble(str[0]);
                                }
                                if (DC == "C")
                                {
                                    DC = "D";
                                }
                                else
                                {
                                    DC = "C";
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(opbalance.Rows[0]["ClientID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "Pmnt")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("Pmnt");
                                li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                                li.SubItems.Add("By Rcpt. No.: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["ShortNarration"].ToString());
                                li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                li.SubItems.Add("");
                                mouseclickid.Rows.Add("Pmnt", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "Cheque Issued")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("Cheque Issued");
                                li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                                li.SubItems.Add("By Rcpt. No.: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["OT5"].ToString() + "; " + SPdt.Rows[i]["OT6"].ToString());
                                string CD = SPdt.Rows[i]["dc"].ToString();
                                if (CD == "D")
                                {
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                    li.SubItems.Add("");
                                }
                                else
                                {
                                    li.SubItems.Add("");
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                                }

                                mouseclickid.Rows.Add("Cheque Issued", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "Draft Issued")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("Draft Issued");
                                li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                                li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                                string CD = SPdt.Rows[i]["dc"].ToString();
                                if (CD == "D")
                                {
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["OT3"].ToString()), 2).ToString("N2"));
                                    debit += Convert.ToDouble(SPdt.Rows[i]["OT3"].ToString());
                                    li.SubItems.Add("");
                                }
                                else
                                {
                                    li.SubItems.Add("");
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["OT3"].ToString()), 2).ToString("N2"));
                                    credit += Convert.ToDouble(SPdt.Rows[i]["OT3"].ToString());


                                }

                                mouseclickid.Rows.Add("Draft Issued", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["OT3"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "Cheque/Draft/Rtgs Received")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("Cheque/Draft/Rtgs Received");
                                li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                                li.SubItems.Add("By Rcpt. No.: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["OT5"].ToString() + "; " + SPdt.Rows[i]["OT6"].ToString());
                                string CD = SPdt.Rows[i]["dc"].ToString();
                                if (CD == "D")
                                {
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                    li.SubItems.Add("");
                                }
                                else
                                {
                                    li.SubItems.Add("");
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                                }

                                mouseclickid.Rows.Add("Cheque/Draft/Rtgs Received", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "Deposit Cash Into Bank")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("Deposit Cash Into Bank");
                                li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                                li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                                string CD = SPdt.Rows[i]["dc"].ToString();
                                if (CD == "D")
                                {
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                    li.SubItems.Add("");
                                }
                                else
                                {
                                    li.SubItems.Add("");
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                                }

                                mouseclickid.Rows.Add("Deposit Cash Into Bank", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "Withdraw Cash from Bank")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("Withdraw Cash from Bank");
                                li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                                li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                                string CD = SPdt.Rows[i]["dc"].ToString();
                                if (CD == "D")
                                {
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                    li.SubItems.Add("");
                                }
                                else
                                {
                                    li.SubItems.Add("");
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                                }

                                mouseclickid.Rows.Add("Withdraw Cash from Bank", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "Bank Expenses")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("Bank Expenses");
                                li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                                li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                                string CD = SPdt.Rows[i]["dc"].ToString();
                                if (CD == "D")
                                {
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                    li.SubItems.Add("");
                                }
                                else
                                {
                                    li.SubItems.Add("");
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                                }

                                mouseclickid.Rows.Add("Bank Expenses", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "Online Transfer")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("Online Transfer");
                                li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                                li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                                string CD = SPdt.Rows[i]["dc"].ToString();
                                if (CD == "D")
                                {
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                    li.SubItems.Add("");
                                }
                                else
                                {
                                    li.SubItems.Add("");
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                                }

                                mouseclickid.Rows.Add("Online Transfer", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "DEBIT NOTE")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("DEBIT NOTE");
                                li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                                li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
                                string CD = SPdt.Rows[i]["dc"].ToString();
                                if (CD == "D")
                                {
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                    li.SubItems.Add("");
                                }
                                else
                                {
                                    li.SubItems.Add("");
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                                }

                                mouseclickid.Rows.Add("DEBIT NOTE", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                            }
                            else if (SPdt.Rows[i]["TranType"].ToString() == "CREDIT NOTE")
                            {
                                ListViewItem li;
                                li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                                li.SubItems.Add("CREDIT NOTE");
                                li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                                li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
                                string CD = SPdt.Rows[i]["dc"].ToString();
                                if (CD == "D")
                                {
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                                    li.SubItems.Add("");
                                }
                                else
                                {
                                    li.SubItems.Add("");
                                    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                    credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                                }

                                mouseclickid.Rows.Add("CREDIT NOTE", SPdt.Rows[i]["VoucherID"].ToString());
                                if (i != 0)
                                {
                                    string[] str = balance.Split(' ');
                                    char temp = str[1][0];
                                    DC = temp.ToString();
                                    opbal = Convert.ToDouble(str[0]);
                                }
                                balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                                li.SubItems.Add(balance);
                                li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                            }
                        }
                        #endregion

                        txttotdebit.Text = debit.ToString("N2");
                        txttotcredit.Text = credit.ToString("N2");
                        txtbalance.Text = balance;
                    }
                }
                else
                {
                    MessageBox.Show("Please Select Account Name");
                    cmbaccname.Focus();
                }
            }
            catch
            {
            }
            finally
            {
            }
        }

        private void cmbaccname_Leave(object sender, EventArgs e)
        {
            cmbaccname.Text = s;
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
                open();
            }
        }
    }
}
