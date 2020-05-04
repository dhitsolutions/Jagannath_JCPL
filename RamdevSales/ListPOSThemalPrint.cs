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
    public partial class ListPOSThemalPrint : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        public static string iid = "";
        Connection sql = new Connection();
        private Master master;
        Printing prn = new Printing();
        OleDbSettings ods = new OleDbSettings();
        string paymenttype;
        private TabControl tabControl;
        private DefaultPOS defaultPOS;
        public ListPOSThemalPrint()
        {
            InitializeComponent();
            LVbill.Columns.Add("Date", 90, HorizontalAlignment.Center);
            LVbill.Columns.Add("Bill No",0, HorizontalAlignment.Left);
            LVbill.Columns.Add("Bill No", 80, HorizontalAlignment.Left);
            LVbill.Columns.Add("Account", 70, HorizontalAlignment.Left);
            LVbill.Columns.Add("Name", 0, HorizontalAlignment.Left);
            LVbill.Columns.Add("Qty", 100, HorizontalAlignment.Left);
            LVbill.Columns.Add("Bill AMT", 100, HorizontalAlignment.Left);
            LVbill.Columns.Add("Cash AMT", 100, HorizontalAlignment.Left);
            LVbill.Columns.Add("Other AMT", 100, HorizontalAlignment.Left);
            LVbill.Columns.Add("Net AMT", 100, HorizontalAlignment.Left);
            LVbill.Columns.Add("Change", 100, HorizontalAlignment.Left);
        }

        public ListPOSThemalPrint(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            LVbill.Columns.Add("Date", 90, HorizontalAlignment.Center);
            LVbill.Columns.Add("Bill No", 0, HorizontalAlignment.Left);
            LVbill.Columns.Add("Bill No", 80, HorizontalAlignment.Left);
            LVbill.Columns.Add("Account", 70, HorizontalAlignment.Left);
            LVbill.Columns.Add("Name", 0, HorizontalAlignment.Left);
            LVbill.Columns.Add("Qty", 100, HorizontalAlignment.Left);
            LVbill.Columns.Add("Bill AMT", 100, HorizontalAlignment.Left);
            LVbill.Columns.Add("Cash AMT", 100, HorizontalAlignment.Left);
            LVbill.Columns.Add("Other AMT", 100, HorizontalAlignment.Left);
            LVbill.Columns.Add("Net AMT", 100, HorizontalAlignment.Left);
            LVbill.Columns.Add("Change", 100, HorizontalAlignment.Left);
            this.master = master;
            this.tabControl = tabControl;
        }

        public ListPOSThemalPrint(DefaultPOS defaultPOS, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            LVbill.Columns.Add("Date", 90, HorizontalAlignment.Center);
            LVbill.Columns.Add("Bill No", 0, HorizontalAlignment.Left);
            LVbill.Columns.Add("Bill No", 80, HorizontalAlignment.Left);
            LVbill.Columns.Add("Account", 70, HorizontalAlignment.Left);
            LVbill.Columns.Add("Name", 0, HorizontalAlignment.Left);
            LVbill.Columns.Add("Qty", 100, HorizontalAlignment.Left);
            LVbill.Columns.Add("Bill AMT", 100, HorizontalAlignment.Left);
            LVbill.Columns.Add("Cash AMT", 100, HorizontalAlignment.Left);
            LVbill.Columns.Add("Other AMT", 100, HorizontalAlignment.Left);
            LVbill.Columns.Add("Net AMT", 100, HorizontalAlignment.Left);
            LVbill.Columns.Add("Change", 100, HorizontalAlignment.Left);
            this.defaultPOS = defaultPOS;
            this.tabControl = tabControl;
        }

        public ListPOSThemalPrint(DefaultPOS defaultPOS, Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            LVbill.Columns.Add("Date", 90, HorizontalAlignment.Center);
            LVbill.Columns.Add("Bill No", 0, HorizontalAlignment.Left);
            LVbill.Columns.Add("Bill No", 80, HorizontalAlignment.Left);
            LVbill.Columns.Add("Account", 70, HorizontalAlignment.Left);
            LVbill.Columns.Add("Name", 0, HorizontalAlignment.Left);
            LVbill.Columns.Add("Qty", 100, HorizontalAlignment.Left);
            LVbill.Columns.Add("Bill AMT", 100, HorizontalAlignment.Left);
            LVbill.Columns.Add("Cash AMT", 100, HorizontalAlignment.Left);
            LVbill.Columns.Add("Other AMT", 100, HorizontalAlignment.Left);
            LVbill.Columns.Add("Net AMT", 100, HorizontalAlignment.Left);
            LVbill.Columns.Add("Change", 100, HorizontalAlignment.Left);
            this.defaultPOS = defaultPOS;
            this.master = master;
            this.tabControl = tabControl;
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            //DefaultPOS dp = new DefaultPOS(master,tabControl);
            //master.AddNewTab(dp);
            DataTable dt1 = sql.getdataset("select * from FormFormat where isactive=1 and type='POS' and setdefault=1");
            POSNEW bd = new POSNEW();
            DefaultPOS p = new DefaultPOS(master, tabControl);
            if (dt1.Rows[0]["formname"].ToString() == p.Text)
            {
                master.AddNewTab(p);
                //bd.MdiParent = this.MdiParent;
                //bd.StartPosition = FormStartPosition.CenterScreen;
                //bd.Show();
            }
            else
            {
                bd.Size = new Size(this.Height, this.Width);
                bd.StartPosition = FormStartPosition.CenterScreen;
                bd.ShowDialog();

            }
            //dp.Show();
        }
        public void binddrop()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='BillPOSMaster'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                // dt = sql.getdataset("select * from psm");
                DataRow dr;
                dr = dt.NewRow();
                dt.Rows.InsertAt(dr, 0);
                dr["column_name"] = "--Select Column Name--";
                if (dt.Rows.Count != 0)
                {
                    // cmbname.DataSource = dt.DefaultView;
                    // cmbname.ValueMember = "sp_id";
                    // cmbname.DisplayMember = "p_name";
                    // btnclr.Enabled = true;
                    // cmbname.SelectedIndex = -1;
                    txtsearch.DataSource = dt;
                    txtsearch.DisplayMember = "column_name";
                    txtsearch.ValueMember = "BillId";
                }

            }
            catch
            {
            }
        }
        public void listviewbind1()
        {
            try
            {
                LVbill.Items.Clear();

                // SqlCommand cmd = new SqlCommand("select bpm.BillId,bpm.BillDate,bppm.itemname,bpm.totalqty,bpm.Terms,bpm.totalbasic,bpm.totaltax,bpm.totalnet from BillPOsMaster bpm inner join BillPOSProductMaster bppm on bpm.billid=bppm.billid where bpm.isactive=1 and bppm.isactive=1", con);
                // SqlCommand cmd = new SqlCommand("select bppm.itemname,bppm.qty,bppm.rate,bppm.amount,bppm.Discount,bppm.igst,bppm.total from BillPOsMaster bpm inner join BillPOSProductMaster bppm on bpm.billid=bppm.billid where bpm.isactive=1 and bppm.isactive=1", con);
                SqlCommand cmd = new SqlCommand("select billdate,billid,billno,terms,cardholdername,totalqty,totalbasic,cashtendered,totaltax,totalnet,change from BillPOsMaster where isactive=1  and BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by BillDate", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    LVbill.Items.Add(Convert.ToDateTime(dt.Rows[i].ItemArray[0].ToString()).ToString(Master.dateformate));
                    LVbill.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                    LVbill.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                    LVbill.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                    LVbill.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                    LVbill.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                    LVbill.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                    LVbill.Items[i].SubItems.Add(dt.Rows[i].ItemArray[7].ToString());
                    LVbill.Items[i].SubItems.Add(dt.Rows[i].ItemArray[8].ToString());
                    LVbill.Items[i].SubItems.Add(dt.Rows[i].ItemArray[9].ToString());
                    LVbill.Items[i].SubItems.Add(dt.Rows[i].ItemArray[10].ToString());

                    string a = "", b = "", c = "", d = "", s = "", f = "", g = "", h = "", q = "", j = "", k = "";


                    a = LVbill.Items[i].SubItems[0].Text;
                    b = LVbill.Items[i].SubItems[1].Text;
                    c = LVbill.Items[i].SubItems[2].Text;
                    d = LVbill.Items[i].SubItems[3].Text;
                    s = LVbill.Items[i].SubItems[4].Text;
                    f = LVbill.Items[i].SubItems[5].Text;
                    g = LVbill.Items[i].SubItems[6].Text;
                    h = LVbill.Items[i].SubItems[7].Text;
                    q = LVbill.Items[i].SubItems[8].Text;
                    j = LVbill.Items[i].SubItems[9].Text;
                    k = LVbill.Items[i].SubItems[10].Text;

                    if (f == "" || f == null)
                    {
                        f = "0";
                    }
                    if (g == "" || g == null)
                    {
                        g = "0";
                    }
                    if (h == "" || h == null)
                    {
                        h = "0";
                    }
                    if (q == "" || q == null)
                    {
                        q = "0";
                    }
                    if (j == "" || j == null)
                    {
                        j = "0";
                    }
                    if (k == "" || k == null)
                    {
                        k = "0";
                    }

                    qty1 = qty1 + Convert.ToDouble(f);
                    tbill = tbill1 + Convert.ToDouble(g);
                    tcash1 = tcash1 + Convert.ToDouble(h);
                    tother1 = tother1 + Convert.ToDouble(q);
                    tnet1 = tnet1 + Convert.ToDouble(j);
                    tchanges1 = tchanges1 + Convert.ToDouble(k);
                }
                txtqty.Text = qty1.ToString("N2");
                txtbillamt.Text = tbill.ToString("N2");
                txtcashamt.Text = tcash1.ToString("N2");
                txtotheramt.Text = tother1.ToString("N2");
                txttotalnet.Text = tnet1.ToString("N2");
                txtchange.Text = tchanges1.ToString("N2");
                qty1 = 0; tbill1 = 0; tcash1 = 0; tother1 = 0; tnet1 = 0; tchanges1 = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally
            {

            }
        }
        public void listviewbind()
        {
            try
            {
                LVbill.Items.Clear();

               // SqlCommand cmd = new SqlCommand("select bpm.BillId,bpm.BillDate,bppm.itemname,bpm.totalqty,bpm.Terms,bpm.totalbasic,bpm.totaltax,bpm.totalnet from BillPOsMaster bpm inner join BillPOSProductMaster bppm on bpm.billid=bppm.billid where bpm.isactive=1 and bppm.isactive=1", con);
               // SqlCommand cmd = new SqlCommand("select bppm.itemname,bppm.qty,bppm.rate,bppm.amount,bppm.Discount,bppm.igst,bppm.total from BillPOsMaster bpm inner join BillPOSProductMaster bppm on bpm.billid=bppm.billid where bpm.isactive=1 and bppm.isactive=1", con);
                SqlCommand cmd = new SqlCommand("select billdate,billid,billno,terms,cardholdername,totalqty,totalbasic,cashtendered,totaltax,totalnet,change from BillPOsMaster where isactive=1 and terms='" + paymenttype + "' and BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by BillDate", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    LVbill.Items.Add(Convert.ToDateTime(dt.Rows[i].ItemArray[0].ToString()).ToString(Master.dateformate));
                    LVbill.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                    LVbill.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                    LVbill.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                    LVbill.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                    LVbill.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                    LVbill.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                    LVbill.Items[i].SubItems.Add(dt.Rows[i].ItemArray[7].ToString());
                    LVbill.Items[i].SubItems.Add(dt.Rows[i].ItemArray[8].ToString());
                    LVbill.Items[i].SubItems.Add(dt.Rows[i].ItemArray[9].ToString());
                    LVbill.Items[i].SubItems.Add(dt.Rows[i].ItemArray[10].ToString());

                    string a = "", b = "", c = "", d = "", s = "", f = "", g = "", h = "", q = "", j = "", k = "";


                    a = LVbill.Items[i].SubItems[0].Text;
                    b = LVbill.Items[i].SubItems[1].Text;
                    c = LVbill.Items[i].SubItems[2].Text;
                    d = LVbill.Items[i].SubItems[3].Text;
                    s = LVbill.Items[i].SubItems[4].Text;
                    f = LVbill.Items[i].SubItems[5].Text;
                    g = LVbill.Items[i].SubItems[6].Text;
                    h = LVbill.Items[i].SubItems[7].Text;
                    q = LVbill.Items[i].SubItems[8].Text;
                    j = LVbill.Items[i].SubItems[9].Text;
                    k = LVbill.Items[i].SubItems[10].Text;

                    if (f == "" || f == null)
                               {
                                   f = "0";
                               }
                               if (g == "" || g == null)
                               {
                                   g = "0";
                               }
                               if (h == "" || h == null)
                               {
                                   h = "0";
                               }
                               if (q == "" || q == null)
                               {
                                   q = "0";
                               }
                               if (j == "" || j == null)
                               {
                                   j = "0";
                               }
                               if (k == "" || k == null)
                               {
                                   k = "0";
                               }
                               
                               qty1 = qty1 + Convert.ToDouble(f);
                               tbill = tbill1 + Convert.ToDouble(g);
                               tcash1 = tcash1 + Convert.ToDouble(h);
                               tother1 = tother1 + Convert.ToDouble(q);
                               tnet1 = tnet1 + Convert.ToDouble(j);
                               tchanges1 = tchanges1 + Convert.ToDouble(k);
                }
                txtqty.Text = qty1.ToString("N2");
                txtbillamt.Text = tbill.ToString("N2");
                txtcashamt.Text = tcash1.ToString("N2");
                txtotheramt.Text = tother1.ToString("N2");
                txttotalnet.Text = tnet1.ToString("N2");
                txtchange.Text = tchanges1.ToString("N2");
                qty1 = 0; tbill1 = 0; tcash1 = 0; tother1 = 0; tnet1 = 0; tchanges1 = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally
            {

            }
        }
        public void todaysbill()
        {
            try
            {
                LVbill.Items.Clear();

                // SqlCommand cmd = new SqlCommand("select bpm.BillId,bpm.BillDate,bppm.itemname,bpm.totalqty,bpm.Terms,bpm.totalbasic,bpm.totaltax,bpm.totalnet from BillPOsMaster bpm inner join BillPOSProductMaster bppm on bpm.billid=bppm.billid where bpm.isactive=1 and bppm.isactive=1", con);
                // SqlCommand cmd = new SqlCommand("select bppm.itemname,bppm.qty,bppm.rate,bppm.amount,bppm.Discount,bppm.igst,bppm.total from BillPOsMaster bpm inner join BillPOSProductMaster bppm on bpm.billid=bppm.billid where bpm.isactive=1 and bppm.isactive=1", con);
                SqlCommand cmd = new SqlCommand("select billdate,billid,billno,terms,cardholdername,totalqty,totalbasic,cashtendered,totaltax,totalnet,change from BillPOsMaster where isactive=1  and BillDate>='" + Convert.ToDateTime(DateTime.Now).ToString(Master.dateformate) + "' and BillDate<='" + Convert.ToDateTime(DateTime.Now).ToString(Master.dateformate) + "'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    LVbill.Items.Add(Convert.ToDateTime(dt.Rows[i].ItemArray[0].ToString()).ToString(Master.dateformate));
                    LVbill.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                    LVbill.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                    LVbill.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                    LVbill.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                    LVbill.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                    LVbill.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                    LVbill.Items[i].SubItems.Add(dt.Rows[i].ItemArray[7].ToString());
                    LVbill.Items[i].SubItems.Add(dt.Rows[i].ItemArray[8].ToString());
                    LVbill.Items[i].SubItems.Add(dt.Rows[i].ItemArray[9].ToString());
                    LVbill.Items[i].SubItems.Add(dt.Rows[i].ItemArray[10].ToString());


                    string a = "", b = "", c = "", d = "", s = "", f = "", g = "", h = "", q = "", j = "", k = "";


                    a = LVbill.Items[i].SubItems[0].Text;
                    b = LVbill.Items[i].SubItems[1].Text;
                    c = LVbill.Items[i].SubItems[2].Text;
                    d = LVbill.Items[i].SubItems[3].Text;
                    s = LVbill.Items[i].SubItems[4].Text;
                    f = LVbill.Items[i].SubItems[5].Text;
                    g = LVbill.Items[i].SubItems[6].Text;
                    h = LVbill.Items[i].SubItems[7].Text;
                    q = LVbill.Items[i].SubItems[8].Text;
                    j = LVbill.Items[i].SubItems[9].Text;
                    k = LVbill.Items[i].SubItems[10].Text;

                    if (f == "" || f == null)
                    {
                        f = "0";
                    }
                    if (g == "" || g == null)
                    {
                        g = "0";
                    }
                    if (h == "" || h == null)
                    {
                        h = "0";
                    }
                    if (q == "" || q == null)
                    {
                        q = "0";
                    }
                    if (j == "" || j == null)
                    {
                        j = "0";
                    }
                    if (k == "" || k == null)
                    {
                        k = "0";
                    }

                    qty2 = qty2 + Convert.ToDouble(f);
                    tbill2 = tbill2 + Convert.ToDouble(g);
                    tcash2 = tcash2 + Convert.ToDouble(h);
                    tother2 = tother2 + Convert.ToDouble(q);
                    tnet2 = tnet2 + Convert.ToDouble(j);
                    tchanges2 = tchanges2 + Convert.ToDouble(k);
                }
                txtqty.Text = qty2.ToString("N2");
                txtbillamt.Text = tbill2.ToString("N2");
                txtcashamt.Text = tcash2.ToString("N2");
                txtotheramt.Text = tother2.ToString("N2");
                txttotalnet.Text = tnet2.ToString("N2");
                txtchange.Text = tchanges2.ToString("N2");
                qty2 = 0; tbill2 = 0; tcash2 = 0; tother2 = 0; tnet2 = 0; tchanges2 = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally
            {

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
        DataTable userrights = new DataTable();
        private void ListPOS_Load(object sender, EventArgs e)
        {
            con.Open();
            userrights = sql.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[9]["a"].ToString() == "False")
                {
                    btnnew.Enabled = false;
                }
                if (userrights.Rows[9]["p"].ToString() == "False")
                {
                    btnprint.Enabled = false;
                }
            }
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
            DTPFrom.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            DTPFrom.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            DTPTo.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            DTPTo.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            DTPFrom.Value = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            if (rbcash.Checked == true)
            {
                paymenttype = "Cash";
            }
            else if (rbcredit.Checked == true)
            {
                paymenttype = "Credit/Debit Card";
            }
            else
            {
                paymenttype = "E-Wallet";
            }
            todaysbill();
         //   binddrop();
            DTPFrom.CustomFormat = Master.dateformate;
            DTPTo.CustomFormat = Master.dateformate;
            con.Close();
           this.ActiveControl = btnnew;
        }


        public void open()
        {
            try
            {
                this.Enabled = false;
                iid = LVbill.Items[LVbill.FocusedItem.Index].SubItems[1].Text;
                DataTable dt1 = sql.getdataset("select * from FormFormat where isactive=1 and type='POS' and setdefault=1");
                POSNEW bd = new POSNEW();
                DefaultPOS dlg = new DefaultPOS(master, tabControl);
                if (dt1.Rows[0]["formname"].ToString() == dlg.Text)
                {
                    dlg.Update(1, iid);
                    master.AddNewTab(dlg);
                    dlg.Show();
                }
                else
                {

                    bd.Update(1, iid);
                    bd.Size = new Size(this.Height, this.Width);
                    bd.StartPosition = FormStartPosition.CenterScreen;
                    bd.ShowDialog();

                }
            }
            finally
            {
                this.Enabled = true;
            }
        }
        private void LVbill_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[9]["v"].ToString() == "True" || userrights.Rows[9]["u"].ToString() == "True")
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
        double qty = 0, tbill = 0, tcash = 0, tother = 0, tnet = 0, tchanges = 0, qty1 = 0, tbill1 = 0, tcash1 = 0, tother1 = 0, tnet1 = 0, tchanges1 = 0, qty2 = 0, tbill2 = 0, tcash2 = 0, tother2 = 0, tnet2 = 0, tchanges2 = 0;
        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                  DialogResult dr1 = MessageBox.Show("Do you want to Print?", "POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                  if (dr1 == DialogResult.Yes)
                  {
                      if (LVbill.Items.Count > 0)
                      {
                          prn.execute("delete from printing");
                           DataTable dt1 = sql.getdataset("select * from company WHERE isactive=1");
                           string BillListDate = "Bill List From "+DTPFrom.Text+" To "+DTPTo.Text;
                        //         string date = "", type = "", Account = "", drAmount = "", crAmount="",balance="";
                           for (int i = 0; i < LVbill.Items.Count; i++)
                           {
                               string a="",b="",c="",d="",s="",f="",g="",h="",q="",j="",k="";


                               a = LVbill.Items[i].SubItems[0].Text;
                               b = LVbill.Items[i].SubItems[1].Text;
                               c = LVbill.Items[i].SubItems[2].Text;
                               d = LVbill.Items[i].SubItems[3].Text;
                               s = LVbill.Items[i].SubItems[4].Text;
                               f = LVbill.Items[i].SubItems[5].Text;
                               g = LVbill.Items[i].SubItems[6].Text;
                               h = LVbill.Items[i].SubItems[7].Text;
                               q = LVbill.Items[i].SubItems[8].Text;
                               j = LVbill.Items[i].SubItems[9].Text;
                               k = LVbill.Items[i].SubItems[10].Text;
                               if (f == "" || f == null)
                               {
                                   f = "0";
                               }
                               if (g == "" || g == null)
                               {
                                   g = "0";
                               }
                               if (h == "" || h == null)
                               {
                                   h = "0";
                               }
                               if (q == "" || q == null)
                               {
                                   q = "0";
                               }
                               if (j == "" || j == null)
                               {
                                   j = "0";
                               }
                               if (k == "" || k == null)
                               {
                                   k = "0";
                               }
                               
                               qty = qty + Convert.ToDouble(f);
                               tbill = tbill + Convert.ToDouble(g);
                               tcash = tcash + Convert.ToDouble(h);
                               tother = tother + Convert.ToDouble(q);
                               tnet = tnet + Convert.ToDouble(j);
                               tchanges = tchanges + Convert.ToDouble(k);

                               string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34)VALUES";
                               qry += "('" + dt1.Rows[0][0].ToString() + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][2].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + dt1.Rows[0][11].ToString() + "','" + dt1.Rows[0][12].ToString() + "','" + dt1.Rows[0][13].ToString() + "','" + DTPFrom.Text + "','" + DTPTo.Text + "','" + a + "','" + b + "','" + c + "','" + d + "','" + s + "','" + f + "','" + g + "','" + h + "','" + q + "','" + j + "','" + k + "','" + qty + "','" + tbill + "','" + tcash + "','" + tother + "','" + tnet + "','" + tchanges + "','" + BillListDate + "')";
                               prn.execute(qry);
                           }

                           string update = "UPDATE [Printing] SET [T28]='" + qty + "',[T29]='"+tbill+"',[T30]='"+tcash+"',[T31]='"+tother+"',[T32]='"+tnet+"',[T33]='"+tchanges+"'";
                           prn.execute(update);

                           string reportName = "PosBillListThermalPrint";
                           Print popup = new Print(reportName);
                           popup.ShowDialog();
                           popup.Dispose();
                           qty = 0; tbill = 0; tcash = 0; tother = 0; tnet = 0; tchanges = 0;
                      }
                  }
            }
            catch
            {
            }
        }

        private void LVbill_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[9]["v"].ToString() == "True" || userrights.Rows[9]["u"].ToString() == "True")
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
                if (e.KeyData == Keys.Delete)
                {
                    iid = LVbill.Items[LVbill.FocusedItem.Index].SubItems[1].Text;
                       DialogResult dr = MessageBox.Show("Do you want to Delete?", "Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                       if (dr == DialogResult.Yes)
                       {
                           sql.execute("Update BillPOSMaster set isactive=0 where BillId='"+iid+"'");
                           sql.execute("Update BillPOSProductMaster set isactive=0 where BillId='" + iid + "'");
                       }
                }
            }
            catch
            {
            }
        }

        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            
            if (rbcash.Checked == true)
            {
                paymenttype = "Cash";
                listviewbind();
            }
            else if (rbcredit.Checked == true)
            {
                paymenttype = "Credit/Debit Card";
                listviewbind();
            }
            else if (rbewallet.Checked == true)
            {
                paymenttype = "E-Wallet";
                listviewbind();
            }
            else
            {
                listviewbind1();
            }
        }

        private void txtser_Enter(object sender, EventArgs e)
        {
            txtser.BackColor = Color.LightYellow;
        }

        private void txtser_Leave(object sender, EventArgs e)
        {
            txtser.BackColor = Color.White;
        }

        private void txtser_TextChanged(object sender, EventArgs e)
        {

        }

        private void LVbill_SelectedIndexChanged(object sender, EventArgs e)
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

        private void btnnew_MouseEnter(object sender, EventArgs e)
        {
            btnnew.UseVisualStyleBackColor = false;
            btnnew.BackColor = Color.FromArgb(9, 106, 3);
            btnnew.ForeColor = Color.White;
        }

        private void btnnew_MouseLeave(object sender, EventArgs e)
        {
            btnnew.UseVisualStyleBackColor = true;
            btnnew.BackColor = Color.FromArgb(51, 153, 255);
            btnnew.ForeColor = Color.White;
        }

        private void btnsearch_MouseEnter(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = false;
            btnprint.BackColor = Color.FromArgb(0, 254, 22);
            btnprint.ForeColor = Color.White;
        }

        private void btnsearch_MouseLeave(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = true;
            btnprint.BackColor = Color.FromArgb(51, 153, 255);
            btnprint.ForeColor = Color.White;
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

        private void btnsearch_Enter(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = false;
            btnprint.BackColor = Color.FromArgb(0, 254, 22);
            btnprint.ForeColor = Color.White;
        }

        private void btnsearch_Leave(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = true;
            btnprint.BackColor = Color.FromArgb(51, 153, 255);
            btnprint.ForeColor = Color.White;
        }

        private void btnnew_Enter(object sender, EventArgs e)
        {
            btnnew.UseVisualStyleBackColor = false;
            btnnew.BackColor = Color.FromArgb(9, 106, 3);
            btnnew.ForeColor = Color.White;
        }

        private void btnnew_Leave(object sender, EventArgs e)
        {
            btnnew.UseVisualStyleBackColor = true;
            btnnew.BackColor = Color.FromArgb(51, 153, 255);
            btnnew.ForeColor = Color.White;
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
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

        private void DTPFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ActiveControl = DTPTo;
            }
        }

        private void DTPTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               this.ActiveControl = BtnViewReport;
            }
        }

    }
}
