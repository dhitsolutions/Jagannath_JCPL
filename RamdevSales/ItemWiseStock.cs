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
    public partial class ItemWiseStock : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        OleDbSettings ods = new OleDbSettings();
        Printing prn = new Printing();
        public ItemWiseStock()
        {
            InitializeComponent();
        }
        int cid = 0;
        public ItemWiseStock(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        DataTable userrights = new DataTable();
        private void ItemWiseStock_Load(object sender, EventArgs e)
        {
            if(cid==0)
            {
                userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[10]["p"].ToString() == "False")
                    {
                        btngenrpt.Enabled = false;
                    }
                }
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
                LVledger.Columns.Add("Type", 100, HorizontalAlignment.Center);
                LVledger.Columns.Add("Bill No", 70, HorizontalAlignment.Left);
                LVledger.Columns.Add("Name", 370, HorizontalAlignment.Left);
                LVledger.Columns.Add("Price", 100, HorizontalAlignment.Right);
                LVledger.Columns.Add("Qty In", 100, HorizontalAlignment.Right);
                LVledger.Columns.Add("Oty Out", 100, HorizontalAlignment.Right);
                LVledger.Columns.Add("balance", 140, HorizontalAlignment.Right);

                //  listviewbind();
                binditems();
                con.Close();
                mouseclickid.Columns.Add("type", typeof(string));
                mouseclickid.Columns.Add("id", typeof(string));
                autoreaderbind();
                DTPFrom.CustomFormat = Master.dateformate;
                DTPTo.CustomFormat = Master.dateformate;
                this.ActiveControl = cmbaccname;
                //set the interval  and start the timer
                timer1.Interval = 1000;
                timer1.Start();
            }
        }
        DataTable mouseclickid = new DataTable();
        public void binditems()
        {
            SqlCommand cmd1 = new SqlCommand("select ProductID,Product_Name from ProductMaster where isactive=1 order by Product_Name", con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            cmbaccname.ValueMember = "ProductID";
            cmbaccname.DisplayMember = "Product_Name";
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

            cmbcustname.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cmbcustname.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbcustname.AutoCompleteCustomSource.AddRange(arr);
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
        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            LVledger.Items.Clear();
            {
                if (cmbaccname.Text != "")
                {
                    EventHandler<EventArgs> ea = Canceled;
                    if (ea != null)
                        ea(this, e);
                    //for calculate OpBalance
                    #region
                    string proid = conn.ExecuteScalar("select Productid from ProductMaster where isactive=1 and Product_Name='" + cmbaccname.Text + "'");
                    string openingstockfromitem = conn.ExecuteScalar("select opstock from productpricemaster where isactive=1 and productid= '" + proid + "'");
                    string totalPurchase = conn.ExecuteScalar("select sum(pqty) from billproductmaster where bill_Run_Date < '" + DTPFrom.Text + "' and productid='" + cmbaccname.SelectedValue + "' and isactive=1 and Billtype='P'");
                    string totalSale = conn.ExecuteScalar("select sum(pqty) from billproductmaster where bill_Run_Date < '" + DTPFrom.Text + "' and productid='" + cmbaccname.SelectedValue + "' and isactive=1 and Billtype='P'");
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
                        txtopbal.Text = opbal.ToString("N2");
                        DC = "D";
                    }
                    else
                    {
                        opbal = Convert.ToDouble(totalSale) - Convert.ToDouble(totalPurchase) + Convert.ToDouble(openingstockfromitem);
                        txtopbal.Text = opbal.ToString("N2");
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
                    DataTable pos = conn.getdataset("select 'POS' as Billtype,b.BillDate as Bill_Run_Date,b.billno,b.totalnet as Rate,bp.qty as pqty,b.BillId as Bill_No  from BillPOSMaster b inner join BillPOSProductMaster bp on b.billid=bp.billid where b.isactive=1 and bp.isactive=1 and b.BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and bp.itemname='"+cmbaccname.Text+"' order by BillDate asc");
                    pos = changedtclone(pos);

                    DataTable SPdt = conn.getdataset("select Billtype,Bill_Run_Date,billno,Rate,Pqty,Bill_No from billproductmaster where isactive=1 and bill_Run_Date between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and productid='" + proid + "' order by bill_Run_Date");
                    string balance = "0.00";
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
                            ListViewItem li;
                            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["bill_Run_Date"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add("Sale");
                            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                            string clientname = conn.ExecuteScalar("select accountname from clientmaster where isactive=1 and clientid=(select clientid from billmaster where billno='" + SPdt.Rows[i]["Billno"].ToString() + "')");
                            li.SubItems.Add(clientname);
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Rate"].ToString()), 2).ToString("N2"));

                            li.SubItems.Add("");
                            li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), 2)).ToString());
                            debit += Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString());

                            mouseclickid.Rows.Add("Sale", SPdt.Rows[i]["Bill_no"].ToString());
                            if (i != 0)
                            {
                                string[] str = balance.Split(' ');
                                char temp = str[0][0];
                                DC = temp.ToString();
                                opbal = Convert.ToDouble(str[0]);
                            }
                            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "D", i);
                            li.SubItems.Add(balance);
                        }
                       else if (SPdt.Rows[i]["Billtype"].ToString() == "POS")
                        {
                            ListViewItem li;
                            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["bill_Run_Date"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add("POS Sale");
                            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                            string clientname = conn.ExecuteScalar("select customername from BillPOSMaster where isactive=1 and billno='" + SPdt.Rows[i]["Billno"].ToString() + "'");
                            li.SubItems.Add(clientname);
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Rate"].ToString()), 2).ToString("N2"));

                            li.SubItems.Add("");
                            li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), 2)).ToString());
                            debit += Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString());

                            mouseclickid.Rows.Add("Sale", SPdt.Rows[i]["Bill_no"].ToString());
                            if (i != 0)
                            {
                                string[] str = balance.Split(' ');
                                char temp = str[0][0];
                                DC = temp.ToString();
                                opbal = Convert.ToDouble(str[0]);
                            }
                            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "D", i);
                            li.SubItems.Add(balance);
                        }
                        else if (SPdt.Rows[i]["Billtype"].ToString() == "SR")
                        {
                            ListViewItem li;
                            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["bill_Run_Date"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add("Sale Return");
                            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                            string clientname = conn.ExecuteScalar("select accountname from clientmaster where isactive=1 and clientid=(select clientid from billmaster where billno='" + SPdt.Rows[i]["Billno"].ToString() + "')");
                            li.SubItems.Add(clientname);
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Rate"].ToString()), 2).ToString("N2"));
                            li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), 2)).ToString());
                            li.SubItems.Add("");
                            //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            credit += Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString());
                            mouseclickid.Rows.Add("Sale Return", SPdt.Rows[i]["Pqty"].ToString());
                            if (i != 0)
                            {
                                string[] str = balance.Split(' ');
                                char temp = str[0][0];
                                DC = temp.ToString();
                                opbal = Convert.ToDouble(str[0]);
                            }
                            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "C", i);
                            li.SubItems.Add(balance);
                        }
                        else if (SPdt.Rows[i]["Billtype"].ToString() == "P")
                        {
                            ListViewItem li;
                            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["bill_Run_Date"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add("Purchase");
                            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                            string clientname = conn.ExecuteScalar("select accountname from clientmaster where isactive=1 and clientid=(select clientid from billmaster where billno='" + SPdt.Rows[i]["Billno"].ToString() + "')");
                            li.SubItems.Add(clientname);
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Rate"].ToString()), 2).ToString("N2"));
                            li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), 2)).ToString());
                            li.SubItems.Add("");
                            //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            credit += Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString());
                            mouseclickid.Rows.Add("Purchase", SPdt.Rows[i]["Pqty"].ToString());
                            if (i != 0)
                            {
                                string[] str = balance.Split(' ');
                                char temp = str[0][0];
                                DC = temp.ToString();
                                opbal = Convert.ToDouble(str[0]);
                            }
                            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "C", i);
                            li.SubItems.Add(balance);
                        }
                        else if (SPdt.Rows[i]["Billtype"].ToString() == "PR")
                        {
                            ListViewItem li;
                            li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["bill_Run_Date"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add("Purchase Return");
                            li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                            string clientname = conn.ExecuteScalar("select accountname from clientmaster where isactive=1 and clientid=(select clientid from billmaster where billno='" + SPdt.Rows[i]["Billno"].ToString() + "')");
                            li.SubItems.Add(clientname);
                            li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Rate"].ToString()), 2).ToString("N2"));
                            li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), 2)).ToString());
                            li.SubItems.Add("");
                            //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            credit += Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString());
                            mouseclickid.Rows.Add("Purchase Return", SPdt.Rows[i]["Pqty"].ToString());
                            if (i != 0)
                            {
                                string[] str = balance.Split(' ');
                                char temp = str[0][0];
                                DC = temp.ToString();
                                opbal = Convert.ToDouble(str[0]);
                            }
                            balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "D", i);
                            li.SubItems.Add(balance);
                        }

                    }
                    DataTable stockadjustment = conn.getdataset("select sm.*,sim.* from stockadujestmentmaster sm inner join stockadujestmentitemmaster sim on sm.id=sim.stockid where sm.isactive=1 and sim.isactive=1 and sim.itemid='" + cmbaccname.SelectedValue + "' and sm.stockdate between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' order by sm.stockdate");
                    for (int i = 0; i < stockadjustment.Rows.Count; i++)
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(stockadjustment.Rows[i]["stockdate"].ToString()).ToString("dd-MMM-yyyy"));
                        li.SubItems.Add("Stock Adjustment");
                        li.SubItems.Add("");
                        li.SubItems.Add(stockadjustment.Rows[i]["mainremark"].ToString());
                        li.SubItems.Add(Math.Round(Convert.ToDouble("0"), 2).ToString("N2"));
                        if (Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString()) > 0)
                        {
                            li.SubItems.Add((Math.Round(Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString()), 2)).ToString());
                            li.SubItems.Add("");
                            credit += Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString());
                        }
                        else
                        {
                            li.SubItems.Add("");
                            li.SubItems.Add((Math.Round(Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString()), 2)).ToString());
                            Double d = Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString());
                            Double a1 = d * -1;
                            debit += Convert.ToDouble(a1);
                        }
                        Double bal = Convert.ToDouble(balance);
                        Double astock = Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString());
                        Double fbalance = bal + astock;
                        balance = Convert.ToString(fbalance);
                        li.SubItems.Add(balance);
                    }
                    #endregion

                    txttotdebit.Text = debit.ToString("N2");
                    txttotcredit.Text = credit.ToString("N2");
                    txtbalance.Text = balance;
                }
                else
                {
                    MessageBox.Show("Please Select Account Name");
                    cmbaccname.Focus();
                }
            }
        }
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
        internal void getitemname(int p, string iid)
        {
            try
            {
                if (iid != "")
                {
                    cid = 1;
                    DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
                    DTPFrom.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                    DTPFrom.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                    DTPTo.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                    DTPTo.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                    DTPFrom.Value = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                    LVledger.Columns.Add("Date", 100, HorizontalAlignment.Center);
                    LVledger.Columns.Add("Type", 100, HorizontalAlignment.Center);
                    LVledger.Columns.Add("Bill No", 70, HorizontalAlignment.Left);
                    LVledger.Columns.Add("Name", 370, HorizontalAlignment.Left);
                    LVledger.Columns.Add("Price", 100, HorizontalAlignment.Right);
                    LVledger.Columns.Add("Qty In", 100, HorizontalAlignment.Right);
                    LVledger.Columns.Add("Oty Out", 100, HorizontalAlignment.Right);
                    LVledger.Columns.Add("balance", 140, HorizontalAlignment.Right);

                    //  listviewbind();
                    binditems();
                    mouseclickid.Columns.Add("type", typeof(string));
                    mouseclickid.Columns.Add("id", typeof(string));
                    cmbaccname.Text = iid;
                    DTPFrom.CustomFormat = Master.dateformate;
                    DTPTo.CustomFormat = Master.dateformate;
                    //set the interval  and start the timer
                    //timer1.Interval = 1000;
                    //timer1.Start();
                }
            }
            catch
            {
            }
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
                dt = conn.getdataset("select Product_Name from ProductMaster order by Product_Name");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (cmbaccname.Text == dt.Rows[i]["Product_Name"].ToString())
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

        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }


        public void open()
        {
            if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Sale")
            {
                string[] strfinalarray = new string[5] { "S", "D", "Sale", "", "" };
                string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[2].Text.Remove(0, 9);

                String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                DefaultSale bd = new DefaultSale(this, master, tabControl, strfinalarray);
             //   Sale p = new Sale(this, master, tabControl);
                if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                {
                    bd.updatemode(str, billno, 1, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd);
                }
                //else if (dt1.Rows[0]["formname"].ToString() == p.Text)
                //{
                //    p.updatemode(str, mouseclickid.Rows[LVledger.FocusedItem.Index][1].ToString(), 1);
                //    //p.MdiParent = this.MdiParent;
                //    //p.StartPosition = FormStartPosition.CenterScreen;
                //    //p.Show();
                //    master.AddNewTab(p);
                //}




                bd.Show();
            }
            else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Sale Return")
            {
                string[] strfinalarray = new string[5] { "SR", "C", "Sale Return", "SR", "" };
                string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[2].Text.Remove(0, 9);

                String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                DefaultSale bd = new DefaultSale(this, master, tabControl, strfinalarray);
            //    Sale p = new Sale(this, master, tabControl);
                if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                {
                    bd.updatemode(str, billno, 1, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd);
                }
                //else if (dt1.Rows[0]["formname"].ToString() == p.Text)
                //{
                //    p.updatemode(str, mouseclickid.Rows[LVledger.FocusedItem.Index][1].ToString(), 1);
                //    //p.MdiParent = this.MdiParent;
                //    //p.StartPosition = FormStartPosition.CenterScreen;
                //    //p.Show();
                //    master.AddNewTab(p);
                //}




              //  bd.Show();
            }
            else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Purchase")
            {
                string[] strfinalarray = new string[5] { "P", "C", "Purchase", "", "" };
                string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[2].Text.Remove(0, 9);
                String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                //DefaultPurchase frm = new DefaultPurchase(this);
                DefaultSale bd = new DefaultSale(this, master, tabControl, strfinalarray);
             //   Purchase p = new Purchase(this, master, tabControl);
                if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                {
                    bd.updatemode(str, billno, 1, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd);
                }
                //else if (dt1.Rows[0]["formname"].ToString() == p.Text)
                //{
                //    p.updatemode(str, mouseclickid.Rows[LVledger.FocusedItem.Index][1].ToString(), 1);
                //    //p.MdiParent = this.MdiParent;
                //    //p.StartPosition = FormStartPosition.CenterScreen;
                //    //p.Show();
                //    master.AddNewTab(p);
                //}

                //    DefaultPurchase bd = new DefaultPurchase(this);

                //  bd.Show();
            }
            else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Purchase Return")
            {

                string[] strfinalarray = new string[5] { "PR", "D", "Purchase Return", "PR", "" };
                string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[2].Text.Remove(0, 9);
                String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                //DefaultPurchase frm = new DefaultPurchase(this);
                DefaultSale bd = new DefaultSale(this, master, tabControl, strfinalarray);
                //Purchase p = new Purchase(this, master, tabControl);
                if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                {
                    bd.updatemode(str, billno, 1, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd);
                }
                //else if (dt1.Rows[0]["formname"].ToString() == p.Text)
                //{
                //    p.updatemode(str, mouseclickid.Rows[LVledger.FocusedItem.Index][1].ToString(), 1);
                //    //p.MdiParent = this.MdiParent;
                //    //p.StartPosition = FormStartPosition.CenterScreen;
                //    //p.Show();
                //    master.AddNewTab(p);
                //}

            }
            else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Rect")
            {
                String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[2].Text;

                QReceipt bd = new QReceipt();
                bd.updatemode(str, mouseclickid.Rows[LVledger.FocusedItem.Index][1].ToString(), 1, LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text);
                bd.Show();
            }
            else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Pmnt")
            {
                String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[2].Text;

                QPayment bd = new QPayment();
                bd.updatemode(str, mouseclickid.Rows[LVledger.FocusedItem.Index][1].ToString(), 1, LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text);
                bd.Show();
            }
        }
        private void LVledger_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[10]["v"].ToString() == "True" || userrights.Rows[10]["u"].ToString() == "True")
                {
                    open();
                }
                else
                {
                    MessageBox.Show("You don't have Permission To View");
                    return;
                }
            }
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
                qry = qry + " order by ProductMaster.Product_Name";

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
                      //  MessageBox.Show("Data not found");
                    }
                    dReader.Close();

                    cmbaccname.AutoCompleteMode = AutoCompleteMode.Suggest;
                    cmbaccname.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    cmbaccname.AutoCompleteCustomSource = namesCollection;
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
                        //MessageBox.Show("Data not found");
                    }
                    dReader.Close();

                    cmbaccname.AutoCompleteMode = AutoCompleteMode.Suggest;
                    cmbaccname.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    cmbaccname.AutoCompleteCustomSource = namesCollection;
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

        private void LVledger_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtopbal_Enter(object sender, EventArgs e)
        {
            txtopbal.BackColor = Color.LightYellow;
        }

        private void txtopbal_Leave(object sender, EventArgs e)
        {
            txtopbal.BackColor = Color.White;
        }

        private void Label5_Click(object sender, EventArgs e)
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

        private void cmbaccname_Enter(object sender, EventArgs e)
        {
            if (cmbaccname.Items.Count > 0)
            {
               // cmbaccname.SelectedIndex = 0;
               // cmbaccname.DroppedDown = true;
            }
        }

        private void btngenrpt_Click(object sender, EventArgs e)
        {
            DialogResult dr1 = MessageBox.Show("Do you want to Print ItemWiseStock?", "ItemWiseStock", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr1 == DialogResult.Yes)
            {
                if (LVledger.Items.Count > 0)
                {
                    prn.execute("delete from printing");
                    DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1");
                    
                    for (int i = 0; i < LVledger.Items.Count; i++)
                    {
                        string date = "", Name = "", Price = "", QtyIN = "", QtyOut = "", balance = "",ItemName="",ItemFormat="";
                        date = LVledger.Items[i].SubItems[0].Text;
                        Name = LVledger.Items[i].SubItems[3].Text;
                        Price = LVledger.Items[i].SubItems[4].Text;
                        QtyIN = LVledger.Items[i].SubItems[5].Text;
                        QtyOut = LVledger.Items[i].SubItems[6].Text;
                        balance = LVledger.Items[i].SubItems[7].Text;
                        ItemName = cmbaccname.Text;
                        ItemFormat = "ITEM WISE STOCK OF " + ItemName + " FROM " + DTPFrom.Text + " TO " + DTPTo.Text;
                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19)VALUES";
                        qry += "('" + date + "','" + Name + "','" + Price + "','" + QtyIN + "','" + QtyOut + "','" + balance + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + ItemFormat + "','" + txttotcredit.Text + "','" + txttotdebit.Text + "','" + txtbalance.Text + "')";
                        prn.execute(qry);
                    }
                    string reportName = "ItemWiseStock";
                    Print popup = new Print(reportName);
                    popup.ShowDialog();
                    popup.Dispose();
                }
                else
                {
                    MessageBox.Show("No Records For Print ItemWiseStock", "ItemWiseStock", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[10]["v"].ToString() == "True" || userrights.Rows[10]["u"].ToString() == "True")
                    {
                        open();
                    }
                    else
                    {
                        MessageBox.Show("You don't have Permission To View");
                        return;
                    }
                }
            }
        }


    }
}
