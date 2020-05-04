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

    public partial class SaleOrderList : Form
    {
        Connection conn = new Connection();
        private Master master;
        private TabControl tabControl;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        SqlCommand cmd;
        SqlDataAdapter sda;
        OleDbSettings ods = new OleDbSettings();
        Printing prn = new Printing();
        static Int32 bill;
        static double total, vat, net;
        private string p;
        private string[] strfinalarray;
        DataTable options = new DataTable();

        public SaleOrderList()
        {
            InitializeComponent();
        }

        public SaleOrderList(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }

        public SaleOrderList(Master master, TabControl tabControl, string[] strfinalarray)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
            this.strfinalarray = strfinalarray;
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
        private void SaleOrderList_Load(object sender, EventArgs e)
        {
            options = conn.getdataset("select * from options");
            DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
            DTPFrom.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            DTPFrom.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            DTPTo.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            DTPTo.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            //    DTPFrom.Value = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            LVDayBook.Columns.Add("Bill No", 70, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("Bill Date", 100, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("PO No", 100, HorizontalAlignment.Left);
            LVDayBook.Columns.Add("ClientName", 300, HorizontalAlignment.Left);
            LVDayBook.Columns.Add("Challan/Orderno", 120, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("Charges", 100, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("Total Amt", 100, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("TAX Amt", 100, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("Net Amt", 120, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("ClientID", 0, HorizontalAlignment.Center);

            lvbillitemitem.Columns.Add("Item Name", 150, HorizontalAlignment.Center);
            lvbillitemitem.Columns.Add("Qty", 100, HorizontalAlignment.Center);
            lvbillitemitem.Columns.Add("Free", 100, HorizontalAlignment.Left);
            lvbillitemitem.Columns.Add("Rate", 120, HorizontalAlignment.Left);
            lvbillitemitem.Columns.Add("Value", 120, HorizontalAlignment.Center);
            lvbillitemitem.Columns.Add("Dis[%]", 100, HorizontalAlignment.Center);
            lvbillitemitem.Columns.Add("Dis Amt", 100, HorizontalAlignment.Center);
            lvbillitemitem.Columns.Add("Tax[%]", 100, HorizontalAlignment.Center);
            lvbillitemitem.Columns.Add("Tax Amt", 100, HorizontalAlignment.Center);
            lvbillitemitem.Columns.Add("Amount", 120, HorizontalAlignment.Center);

            userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
            DateWiseReport d = new DateWiseReport();
            if (strfinalarray[0] == "SO")
            {
                txttitle.Text = "SALE ORDER LIST";
                this.Text = "SALE ORDER LIST";
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[1]["a"].ToString() == "False")
                    {
                        btnnew.Enabled = false;
                    }
                    if (userrights.Rows[1]["p"].ToString() == "False")
                    {
                        btngenrpt.Enabled = false;
                    }
                }
            }
            else if (strfinalarray[0] == "PO")
            {
                txttitle.Text = "PURCHASE ORDER LIST";
                this.Text = "PURCHASE ORDER LIST";
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[4]["a"].ToString() == "False")
                    {
                        btnnew.Enabled = false;
                    }
                    if (userrights.Rows[4]["p"].ToString() == "False")
                    {
                        btngenrpt.Enabled = false;
                    }
                }
            }
            else if (strfinalarray[0] == "SC")
            {
                txttitle.Text = "SALE CHALLAN LIST";
                this.Text = "SALE CHALLAN LIST";
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[12]["a"].ToString() == "False")
                    {
                        btnnew.Enabled = false;
                    }
                    if (userrights.Rows[12]["p"].ToString() == "False")
                    {
                        btngenrpt.Enabled = false;
                    }
                }
            }

            else if (strfinalarray[0] == "PC")
            {
                txttitle.Text = "PURCHASE CHALLAN LIST";
                this.Text = "PURCHASE CHALLAN LIST";
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[15]["a"].ToString() == "False")
                    {
                        btnnew.Enabled = false;
                    }
                    if (userrights.Rows[15]["p"].ToString() == "False")
                    {
                        btngenrpt.Enabled = false;
                    }
                }
            }

            bindgrid();
            bindfilter1drop();
            bindfilter2drop();
            btnnew.Focus();
            DTPFrom.CustomFormat = Master.dateformate;
            DTPTo.CustomFormat = Master.dateformate;
            this.ActiveControl = btnnew;
        }
        private void DTPFrom_ValueChanged(object sender, EventArgs e)
        {
            DTPTo.MinDate = Convert.ToDateTime(DTPFrom.Text);
        }
        public void bindgrid()
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                LVDayBook.Items.Clear();
                // SqlCommand cmd = new SqlCommand("select BillMaster.,ClientMaster.ClientName from BillMaster inner join ClientMaster on clientMaster.ClientID = billmaster.ClientID where Bill_Date>='"+Convert.ToDateTime(DTPFrom.Text).ToString("mm-dd-yyyy")+"' and Bill_Date<='"+Convert.ToDateTime(DTPTo.Text).ToString("mm-dd-yyyy")+"'", con);
                //SqlCommand cmd = new SqlCommand("select bm.Bill_No,bm.Bill_Date,bm.PO_No,cm.On_Bill_desc,cm.TinNO,bm.Bill_Amt,bm.Bill_vat_Amt,bm.Bill_Net_Amt from BillMaster bm inner join ClientMaster cm on cm.ClientID = bm.ClientID where Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString("MM-dd-yyyy") + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString("MM-dd-yyyy") + "' and bm.CompanyID='" + cmbcomp.SelectedValue + "'order by bm.Bill_No", con);
                SqlCommand cmd = new SqlCommand("select b.billno, b.bill_date, b.po_no, c.accountname,b.refno,b.totalcharges,sum(b.totalbasic - b.totaldiscount) as totalbasic,sum(b.totaltax + isnull(bc.sgst,0)+isnull(bc.cgst,0)+isnull(bc.igst,0)) as totaltax,b.totalnet,b.ClientID,b.Terms,b.cusname from SaleOrderMaster b left join SaleOrderchargesmaster bc on bc.billno=b.billno and bc.isactive=1 inner join clientmaster c on c.clientid=b.clientid  where b.isactive=1 and b.BillType='" + strfinalarray[0] + "' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by b.billno, b.bill_date, b.po_no, c.accountname,b.refno,b.totalcharges,b.totalnet,b.ClientID,b.Terms,b.cusname order by b.bill_date asc", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                //LVDayBook.Items.Clear();
                //int tot = 0, cash = 0, cheque = 0;
                //Double amt = 0, cashamt = 0, chequeamt = 0;
                //if (dt.Rows.Count > 0)
                //{
                //    tot = dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {
                    bill = 0;
                    total = 0;
                    vat = 0;
                    net = 0;
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        LVDayBook.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                        LVDayBook.Items[i].SubItems.Add(Convert.ToDateTime(dt.Rows[i].ItemArray[1].ToString()).ToString(Master.dateformate));
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                        if (dt.Rows[i]["Terms"].ToString() == "Cash" && dt.Rows[i]["ClientID"].ToString() == "101")
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[i]["cusname"].ToString()))
                            {
                                LVDayBook.Items[i].SubItems.Add(dt.Rows[i]["cusname"].ToString());
                            }
                            else
                            {
                                LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                            }
                        }
                        else
                        {
                            LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                        }
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[7].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[8].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[9].ToString());
                        bill++;
                        total = total + Convert.ToDouble(dt.Rows[i][6].ToString());
                        vat = vat + Convert.ToDouble(dt.Rows[i][7].ToString());
                        net = net + Convert.ToDouble(dt.Rows[i][8].ToString());
                    }

                    TxtInvoice.Text = bill.ToString();
                    txtbillamt.Text = total.ToString("N2");
                    txtvat.Text = vat.ToString("N2");
                    txtnetamt.Text = net.ToString("N2");
                }
                //if (dt.Rows[i][3].ToString() == "Cash")
                //{
                //    cash++;
                //    cashamt = cashamt + Convert.ToDouble(dt.Rows[i].ItemArray[4].ToString());
                //}
                //if (dt.Rows[i][3].ToString() == "Cheque")
                //{
                //    cheque++;
                //    chequeamt = chequeamt + Convert.ToDouble(dt.Rows[i].ItemArray[4].ToString());
                //}
                //amt = amt + Convert.ToDouble(dt.Rows[i].ItemArray[4].ToString());

                //}
                //     else
                //{
                //    LVDayBook.Items.Clear();
                //    MessageBox.Show("Empty Stack");
                //}
            }

            catch (Exception ex)
            {
                //  MessageBox.Show("Error:" + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            if (rbpending.Checked == true)
            {
                try
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();

                    LVDayBook.Items.Clear();
                    // SqlCommand cmd = new SqlCommand("select BillMaster.,ClientMaster.ClientName from BillMaster inner join ClientMaster on clientMaster.ClientID = billmaster.ClientID where Bill_Date>='"+Convert.ToDateTime(DTPFrom.Text).ToString("mm-dd-yyyy")+"' and Bill_Date<='"+Convert.ToDateTime(DTPTo.Text).ToString("mm-dd-yyyy")+"'", con);
                    //SqlCommand cmd = new SqlCommand("select bm.Bill_No,bm.Bill_Date,bm.PO_No,cm.On_Bill_desc,cm.TinNO,bm.Bill_Amt,bm.Bill_vat_Amt,bm.Bill_Net_Amt from BillMaster bm inner join ClientMaster cm on cm.ClientID = bm.ClientID where Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString("MM-dd-yyyy") + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString("MM-dd-yyyy") + "' and bm.CompanyID='" + cmbcomp.SelectedValue + "'order by bm.Bill_No", con);
                    // SqlCommand cmd = new SqlCommand("select b.billno, b.bill_date, b.po_no, c.accountname,c.address,b.totalbasic,b.totaltax,b.totalnet from SaleOrderMaster b inner join clientmaster c on c.clientid=b.clientid  where b.isactive=1 and BillType='" + strfinalarray[0] + "' and b.OrderStatus='Pending' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by b.bill_date asc", con);
                    //select b.billno, b.bill_date, b.po_no, c.accountname,b.refno,b.totalcharges,b.totalbasic,sum(b.totaltax + isnull(bc.sgst,0)+isnull(bc.cgst,0)+isnull(bc.igst,0)) as totaltax,b.totalnet from billmaster b left join Billchargesmaster bc on bc.billno=b.billno inner join clientmaster c on c.clientid=b.clientid  where c.isactive=1 and b.isactive=1 and b.BillType=
                    SqlCommand cmd = new SqlCommand("select b.billno, b.bill_date, b.po_no, c.accountname,b.refno,b.totalcharges,sum(b.totalbasic - b.totaldiscount) as totalbasic,sum(b.totaltax + isnull(bc.sgst,0)+isnull(bc.cgst,0)+isnull(bc.igst,0)) as totaltax,b.totalnet,b.ClientID,b.Terms,b.cusname from SaleOrderMaster b left join SaleOrderchargesmaster bc on bc.billno=b.billno and bc.isactive=1 inner join clientmaster c on c.clientid=b.clientid  where b.isactive=1 and b.BillType='" + strfinalarray[0] + "'  and b.OrderStatus='Pending' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' Group by b.billno, b.bill_date, b.po_no, c.accountname,b.refno,b.totalcharges,b.totalnet,b.ClientID,b.Terms,b.cusname order by b.bill_date asc", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        bill = 0;
                        total = 0;
                        vat = 0;
                        net = 0;
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            LVDayBook.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                            LVDayBook.Items[i].SubItems.Add(Convert.ToDateTime(dt.Rows[i].ItemArray[1].ToString()).ToString(Master.dateformate));
                            LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                            if (dt.Rows[i]["Terms"].ToString() == "Cash" && dt.Rows[i]["ClientID"].ToString() == "101")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[i]["cusname"].ToString()))
                                {
                                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i]["cusname"].ToString());
                                }
                                else
                                {
                                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                                }
                            }
                            else
                            {
                                LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                            }
                            LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                            LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                            LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                            LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[7].ToString());
                            LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[8].ToString());
                            LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[9].ToString());
                            bill++;
                            total = total + Convert.ToDouble(dt.Rows[i][6].ToString());
                            vat = vat + Convert.ToDouble(dt.Rows[i][7].ToString());
                            net = net + Convert.ToDouble(dt.Rows[i][8].ToString());
                        }

                        TxtInvoice.Text = bill.ToString();
                        txtbillamt.Text = total.ToString("N2");
                        txtvat.Text = vat.ToString("N2");
                        txtnetamt.Text = net.ToString("N2");
                    }

                }

                catch (Exception ex)
                {
                    //  MessageBox.Show("Error:" + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
            else if (rbclear.Checked == true)
            {
                try
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();

                    LVDayBook.Items.Clear();
                    // SqlCommand cmd = new SqlCommand("select BillMaster.,ClientMaster.ClientName from BillMaster inner join ClientMaster on clientMaster.ClientID = billmaster.ClientID where Bill_Date>='"+Convert.ToDateTime(DTPFrom.Text).ToString("mm-dd-yyyy")+"' and Bill_Date<='"+Convert.ToDateTime(DTPTo.Text).ToString("mm-dd-yyyy")+"'", con);
                    //SqlCommand cmd = new SqlCommand("select bm.Bill_No,bm.Bill_Date,bm.PO_No,cm.On_Bill_desc,cm.TinNO,bm.Bill_Amt,bm.Bill_vat_Amt,bm.Bill_Net_Amt from BillMaster bm inner join ClientMaster cm on cm.ClientID = bm.ClientID where Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString("MM-dd-yyyy") + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString("MM-dd-yyyy") + "' and bm.CompanyID='" + cmbcomp.SelectedValue + "'order by bm.Bill_No", con);
                    // SqlCommand cmd = new SqlCommand("select b.billno, b.bill_date, b.po_no, c.accountname,c.address,b.totalbasic,b.totaltax,b.totalnet from SaleOrderMaster b inner join clientmaster c on c.clientid=b.clientid  where b.isactive=1 and BillType='" + strfinalarray[0] + "' and b.OrderStatus='Clear' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by b.bill_date asc", con);
                    SqlCommand cmd = new SqlCommand("select b.billno, b.bill_date, b.po_no, c.accountname,b.refno,b.totalcharges,sum(b.totalbasic - b.totaldiscount) as totalbasic,sum(b.totaltax + isnull(bc.sgst,0)+isnull(bc.cgst,0)+isnull(bc.igst,0)) as totaltax,b.totalnet,b.ClientID,b.Terms,b.cusname from SaleOrderMaster b left join SaleOrderchargesmaster bc on bc.billno=b.billno and bc.isactive=1 inner join clientmaster c on c.clientid=b.clientid  where b.isactive=1 and b.BillType='" + strfinalarray[0] + "' and b.OrderStatus='Clear' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' Group by b.billno, b.bill_date, b.po_no, c.accountname,b.refno,b.totalcharges,b.totalnet,b.ClientID,b.Terms,b.cusname order by b.bill_date asc", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        bill = 0;
                        total = 0;
                        vat = 0;
                        net = 0;
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            LVDayBook.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                            LVDayBook.Items[i].SubItems.Add(Convert.ToDateTime(dt.Rows[i].ItemArray[1].ToString()).ToString(Master.dateformate));
                            LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                            if (dt.Rows[i]["Terms"].ToString() == "Cash" && dt.Rows[i]["ClientID"].ToString() == "101")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[i]["cusname"].ToString()))
                                {
                                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i]["cusname"].ToString());
                                }
                                else
                                {
                                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                                }
                            }
                            else
                            {
                                LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                            }
                            LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                            LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                            LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                            LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[7].ToString());
                            LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[8].ToString());
                            LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[9].ToString());
                            bill++;
                            total = total + Convert.ToDouble(dt.Rows[i][6].ToString());
                            vat = vat + Convert.ToDouble(dt.Rows[i][7].ToString());
                            net = net + Convert.ToDouble(dt.Rows[i][8].ToString());
                        }

                        TxtInvoice.Text = bill.ToString();
                        txtbillamt.Text = total.ToString("N2");
                        txtvat.Text = vat.ToString("N2");
                        txtnetamt.Text = net.ToString("N2");
                    }

                }

                catch (Exception ex)
                {
                    //  MessageBox.Show("Error:" + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                bindgrid();
            }
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                DefaultSaleOrder bd = new DefaultSaleOrder(master, tabControl, strfinalarray);
                SalePurchaseOrderSimpleformate bd1 = new SalePurchaseOrderSimpleformate(master, tabControl, strfinalarray);
                // Sale s = new Sale(master, tabControl);
                // Purchase p = new Purchase(master, tabControl);
                if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                {
                    master.AddNewTab(bd);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                }
                else if (dt1.Rows[0]["formname"].ToString() == bd1.Text)
                {
                    master.AddNewTab(bd1);
                    //p.MdiParent = this.MdiParent;
                    //p.StartPosition = FormStartPosition.CenterScreen;
                    //p.Show();
                }
                //else if (dt1.Rows[0]["formname"].ToString() == p.Text)
                //{
                //    master.AddNewTab(p);
                //}
            }
            catch
            {
            }
        }

        private void LVDayBook_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {

                if (userrights.Rows.Count > 0)
                {
                    if (strfinalarray[0] == "SO")
                    {
                        if (userrights.Rows[1]["v"].ToString() == "True" || userrights.Rows[1]["u"].ToString() == "True")
                        {
                            setform();
                        }
                        else
                        {
                            MessageBox.Show("You don't have Permission To View");
                            return;
                        }
                    }
                    else if (strfinalarray[0] == "SC")
                    {
                        if (userrights.Rows[12]["v"].ToString() == "True" || userrights.Rows[12]["u"].ToString() == "True")
                        {
                            setform();
                        }
                        else
                        {
                            MessageBox.Show("You don't have Permission To View");
                            return;
                        }
                    }
                    else if (strfinalarray[0] == "PO")
                    {
                        if (userrights.Rows[4]["v"].ToString() == "True" || userrights.Rows[4]["u"].ToString() == "True")
                        {
                            setform();
                        }
                        else
                        {
                            MessageBox.Show("You don't have Permission To View");
                            return;
                        }
                    }
                    else if (strfinalarray[0] == "PC")
                    {
                        if (userrights.Rows[15]["v"].ToString() == "True" || userrights.Rows[15]["u"].ToString() == "True")
                        {
                            setform();
                        }
                        else
                        {
                            MessageBox.Show("You don't have Permission To View");
                            return;
                        }
                    }
                }
            }
            finally
            {
                this.Enabled = true;
            }
        }

        DefaultSaleOrder bd;
        public void setform()
        {
            try
            {
                this.Enabled = false;
                String str = LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[0].Text;
                String clientid = LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[9].Text;
                DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                bd = new DefaultSaleOrder(this, master, tabControl, strfinalarray);
                SalePurchaseOrderSimpleformate bd1 = new SalePurchaseOrderSimpleformate(this, master, tabControl, strfinalarray);
                //  Sale p = new Sale(this, master, tabControl);
                if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                {
                    bd.updatemode(str, LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[0].Text, Convert.ToInt32(clientid), strfinalarray);
                    master.AddNewTab(bd);
                }
                else if (dt1.Rows[0]["formname"].ToString() == bd1.Text)
                {
                    bd1.updatemode(str, LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[0].Text, Convert.ToInt32(clientid), strfinalarray);
                    master.AddNewTab(bd1);
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

        private void LVDayBook_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (strfinalarray[0] == "SO")
                        {
                            if (userrights.Rows[1]["v"].ToString() == "True" || userrights.Rows[1]["u"].ToString() == "True")
                            {
                                setform();
                            }
                            else
                            {
                                MessageBox.Show("You don't have Permission To View");
                                return;
                            }
                        }
                        else if (strfinalarray[0] == "SC")
                        {
                            if (userrights.Rows[12]["v"].ToString() == "True" || userrights.Rows[12]["u"].ToString() == "True")
                            {
                                setform();
                            }
                            else
                            {
                                MessageBox.Show("You don't have Permission To View");
                                return;
                            }
                        }
                        else if (strfinalarray[0] == "PO")
                        {
                            if (userrights.Rows[4]["v"].ToString() == "True" || userrights.Rows[4]["u"].ToString() == "True")
                            {
                                setform();
                            }
                            else
                            {
                                MessageBox.Show("You don't have Permission To View");
                                return;
                            }
                        }
                        else if (strfinalarray[0] == "PC")
                        {
                            if (userrights.Rows[15]["v"].ToString() == "True" || userrights.Rows[15]["u"].ToString() == "True")
                            {
                                setform();
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
            finally
            {
                this.Enabled = true;
            }
        }

        private void TxtInvoice_Enter(object sender, EventArgs e)
        {
            TxtInvoice.BackColor = Color.LightYellow;
        }

        private void TxtInvoice_Leave(object sender, EventArgs e)
        {
            TxtInvoice.BackColor = Color.White;
        }

        private void txtbillamt_Enter(object sender, EventArgs e)
        {
            txtbillamt.BackColor = Color.LightYellow;
        }

        private void txtbillamt_Leave(object sender, EventArgs e)
        {
            txtbillamt.BackColor = Color.White;
        }

        private void txtvat_Enter(object sender, EventArgs e)
        {
            txtvat.BackColor = Color.LightYellow;
        }

        private void txtvat_Leave(object sender, EventArgs e)
        {
            txtvat.BackColor = Color.White;
        }

        private void txtnetamt_Enter(object sender, EventArgs e)
        {
            txtnetamt.BackColor = Color.LightYellow;
        }

        private void txtnetamt_Leave(object sender, EventArgs e)
        {
            txtnetamt.BackColor = Color.White;
        }

        private void btngenrpt_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult dr1 = MessageBox.Show("Do you want to Print ?", "Print", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    if (LVDayBook.Items.Count > 0)
                    {
                        prn.execute("delete from printing");
                        // DataTable client = conn.getdataset("select * from clientmaster where isactive=1 and clientID='" + cmbaccname.SelectedValue + "'");
                        DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1");

                        //         string date = "", type = "", Account = "", drAmount = "", crAmount="",balance="";
                        for (int i = 0; i < LVDayBook.Items.Count; i++)
                        {
                            string date = "", BillNo = "", ClientName = "", TotalAmt = "", TaxAmt = "", NetAmount = "", SaleList = "";
                            date = LVDayBook.Items[i].SubItems[1].Text;
                            BillNo = LVDayBook.Items[i].SubItems[0].Text;
                            ClientName = LVDayBook.Items[i].SubItems[3].Text;
                            TotalAmt = LVDayBook.Items[i].SubItems[5].Text;
                            TaxAmt = LVDayBook.Items[i].SubItems[6].Text;
                            NetAmount = LVDayBook.Items[i].SubItems[7].Text;
                            if (txttitle.Text == "SALE ORDER LIST")
                            {
                                SaleList = "SALE ORDER LIST FROM " + DTPFrom.Text + " To " + DTPTo.Text;
                            }
                            else if (txttitle.Text == "PURCHASE ORDER LIST")
                            {
                                SaleList = "PURCHASE ORDER LIST FROM " + DTPFrom.Text + " To " + DTPTo.Text;
                            }
                            else if (txttitle.Text == "SALE CHALLAN LIST")
                            {
                                SaleList = "SALE CHALLAN LIST FROM " + DTPFrom.Text + " To " + DTPTo.Text;
                            }
                            else if (txttitle.Text == "PURCHASE CHALLAN LIST")
                            {
                                SaleList = "PURCHASE CHALLAN LIST FROM " + DTPFrom.Text + " To " + DTPTo.Text;
                            }
                            string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26)VALUES";
                            qry += "('" + date + "','" + BillNo + "','" + ClientName + "','" + TotalAmt + "','" + TaxAmt + "','" + NetAmount + "','" + txtbillamt.Text + "','" + txtvat.Text + "','" + txtnetamt.Text + "','" + dt1.Rows[0][0].ToString() + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][2].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + dt1.Rows[0][11].ToString() + "','" + dt1.Rows[0][12].ToString() + "','" + dt1.Rows[0][13].ToString() + "','" + DTPFrom.Text + "','" + DTPTo.Text + "','" + SaleList + "')";//,'" + client.Rows[0][0].ToString() + "','" + client.Rows[0][1].ToString() + "','" + client.Rows[0][2].ToString() + "','" + client.Rows[0][3].ToString() + "','" + client.Rows[0][4].ToString() + "','" + client.Rows[0][5].ToString() + "','" + client.Rows[0][6].ToString() + "','" + client.Rows[0][7].ToString() + "','" + client.Rows[0][8].ToString() + "','" + client.Rows[0][9].ToString() + "','" + client.Rows[0][10].ToString() + "','" + client.Rows[0][11].ToString() + "','" + client.Rows[0][12].ToString() + "','" + client.Rows[0][13].ToString() + "')";
                            prn.execute(qry);

                        }
                        /*       for (int i = 0; i < LVledger.Items.Count; i++)
                               {
                                   string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34,T35,T36,T37,T38,T39,T40)VALUES";
                                   qry += "('" + date + "','" + type + "','" + Account + "','" + drAmount + "','" + crAmount + "','" + balance + "','" + txttotdebit.Text + "','" + txttotcredit.Text + "','" + txtbalance.Text + "','" + txtopbal.Text + "','" + dt1.Rows[0][0].ToString() + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][2].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + dt1.Rows[0][11].ToString() + "','" + dt1.Rows[0][12].ToString() + "','" + dt1.Rows[0][13].ToString() + "','"+ DTPFrom.Text +"','"+ DTPTo.Text+"','"+ client.Rows[0][0].ToString() + "','" + client.Rows[0][1].ToString() + "','" + client.Rows[0][2].ToString() + "','" + client.Rows[0][3].ToString() + "','" + client.Rows[0][4].ToString() + "','" + client.Rows[0][5].ToString() + "','" + client.Rows[0][6].ToString() + "','" + client.Rows[0][7].ToString() + "','" + client.Rows[0][8].ToString() + "','" + client.Rows[0][9].ToString() + "','" + client.Rows[0][10].ToString() + "','" + client.Rows[0][11].ToString() + "','" + client.Rows[0][12].ToString() + "','" + client.Rows[0][13].ToString()+"')";
                                   prn.execute(qry);

                               } */
                        string reportName = "SaleBillsList";
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

        private void BtnViewReport_Enter(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = false;
            BtnViewReport.BackColor = System.Drawing.Color.FromArgb(94, 191, 174);
            BtnViewReport.ForeColor = System.Drawing.Color.White;
        }

        private void BtnViewReport_Leave(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = true;
            BtnViewReport.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            BtnViewReport.ForeColor = System.Drawing.Color.White;
        }

        private void BtnViewReport_MouseEnter(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = false;
            BtnViewReport.BackColor = System.Drawing.Color.FromArgb(94, 191, 174);
            BtnViewReport.ForeColor = System.Drawing.Color.White;
        }

        private void BtnViewReport_MouseLeave(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = true;
            BtnViewReport.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            BtnViewReport.ForeColor = System.Drawing.Color.White;
        }

        private void btnnew_Enter(object sender, EventArgs e)
        {
            btnnew.UseVisualStyleBackColor = false;
            btnnew.BackColor = System.Drawing.Color.FromArgb(9, 106, 3);
            btnnew.ForeColor = System.Drawing.Color.White;
        }

        private void btnnew_Leave(object sender, EventArgs e)
        {
            btnnew.UseVisualStyleBackColor = true;
            btnnew.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnnew.ForeColor = System.Drawing.Color.White;
        }

        private void btnnew_MouseEnter(object sender, EventArgs e)
        {
            btnnew.UseVisualStyleBackColor = false;
            btnnew.BackColor = System.Drawing.Color.FromArgb(9, 106, 3);
            btnnew.ForeColor = System.Drawing.Color.White;
        }

        private void btnnew_MouseLeave(object sender, EventArgs e)
        {
            btnnew.UseVisualStyleBackColor = true;
            btnnew.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnnew.ForeColor = System.Drawing.Color.White;
        }

        private void btngenrpt_Enter(object sender, EventArgs e)
        {
            btngenrpt.UseVisualStyleBackColor = false;
            btngenrpt.BackColor = System.Drawing.Color.FromArgb(176, 111, 193);
            btngenrpt.ForeColor = System.Drawing.Color.White;
        }

        private void btngenrpt_Leave(object sender, EventArgs e)
        {
            btngenrpt.UseVisualStyleBackColor = true;
            btngenrpt.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btngenrpt.ForeColor = System.Drawing.Color.White;
        }

        private void btngenrpt_MouseEnter(object sender, EventArgs e)
        {
            btngenrpt.UseVisualStyleBackColor = false;
            btngenrpt.BackColor = System.Drawing.Color.FromArgb(176, 111, 193);
            btngenrpt.ForeColor = System.Drawing.Color.White;
        }

        private void btngenrpt_MouseLeave(object sender, EventArgs e)
        {
            btngenrpt.UseVisualStyleBackColor = true;
            btngenrpt.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btngenrpt.ForeColor = System.Drawing.Color.White;
        }

        private void btnclose_Enter(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = false;
            btnclose.BackColor = System.Drawing.Color.FromArgb(248, 152, 94);
            btnclose.ForeColor = System.Drawing.Color.White;
        }

        private void btnclose_Leave(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = true;
            btnclose.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnclose.ForeColor = System.Drawing.Color.White;
        }

        private void btnclose_MouseEnter(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = false;
            btnclose.BackColor = System.Drawing.Color.FromArgb(248, 152, 94);
            btnclose.ForeColor = System.Drawing.Color.White;
        }

        private void btnclose_MouseLeave(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = true;
            btnclose.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnclose.ForeColor = System.Drawing.Color.White;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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

        private void LVDayBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            binditem();
        }

        DataTable item = new DataTable();
        public void binditem()
        {
            try
            {
                String str = LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[0].Text;
                string clientid = LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[9].Text;
                if (!string.IsNullOrEmpty(str))
                {
                    string clientid1 = conn.ExecuteScalar("select ClientID from SaleOrderProductMaster where billno='" + str + "' and billtype='" + strfinalarray[0] + "' and isactive=1");
                    if (!string.IsNullOrEmpty(clientid1))
                    {
                        item = conn.getdataset("select * from SaleOrderProductMaster where isactive=1 and ClientID='" + clientid + "' and billno='" + str + "' and Billtype='" + strfinalarray[0] + "'");
                    }
                    else
                    {
                        item = conn.getdataset("select * from SaleOrderProductMaster where isactive=1 and billno='" + str + "' and Billtype='" + strfinalarray[0] + "'");
                    }
                    lvbillitemitem.Items.Clear();
                    for (int i = 0; i < item.Rows.Count; i++)
                    {
                        lvbillitemitem.Items.Add(item.Rows[i]["productname"].ToString());
                        lvbillitemitem.Items[i].SubItems.Add(item.Rows[i]["qty"].ToString());
                        lvbillitemitem.Items[i].SubItems.Add(item.Rows[i]["free"].ToString());
                        lvbillitemitem.Items[i].SubItems.Add(item.Rows[i]["Rate"].ToString());
                        lvbillitemitem.Items[i].SubItems.Add(item.Rows[i]["Total"].ToString());
                        lvbillitemitem.Items[i].SubItems.Add(item.Rows[i]["discountper"].ToString());
                        lvbillitemitem.Items[i].SubItems.Add(item.Rows[i]["discountamt"].ToString());
                        Double taxper = Convert.ToDouble(item.Rows[i]["sgstper"].ToString()) + Convert.ToDouble(item.Rows[i]["cgstper"].ToString()) + Convert.ToDouble(item.Rows[i]["igstper"].ToString());
                        lvbillitemitem.Items[i].SubItems.Add(Convert.ToString(taxper));
                        lvbillitemitem.Items[i].SubItems.Add(item.Rows[i]["Tax"].ToString());
                        lvbillitemitem.Items[i].SubItems.Add(item.Rows[i]["Amount"].ToString());
                    }
                }
            }
            catch
            {
            }
        }

        public static string autoprint = "false";
        public static string autopdf = "false";

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                //Print popup = new Print(strfinalarray[2]);
                //for (int i = 0; i < LVDayBook.Items.Count; i++)
                //{
                //    if (LVDayBook.Items[i].Checked == true)
                //    {
                //        autoprint = "True";
                //        LVDayBook.Items[i].Focused = true;
                //        MousedblClick();
                //        bd.btnCalculator_Click(sender, e);
                //        popup.btnprint_Click(sender, e);
                //        master.RemoveCurrentTab();
                //        autoprint = "false";
                //    }
                //}
            }
            catch
            {
            }
        }

        private void btnpdf_Click(object sender, EventArgs e)
        {
            try
            {
                //Print popup = new Print(strfinalarray[2]);
                //SQLReport sql = new SQLReport();
                //if (options.Rows[0]["pdfpath"].ToString() != "")
                //{
                //    for (int i = 0; i < LVDayBook.Items.Count; i++)
                //    {
                //        if (LVDayBook.Items[i].Checked == true)
                //        {
                //            autoprint = "True";
                //            autopdf = "True";
                //            LVDayBook.Items[i].Focused = true;
                //            MousedblClick();
                //            bd.btnCalculator_Click(sender, e);
                //            popup.btnpreview_Click(sender, e);
                //            master.RemoveCurrentTab();
                //            autoprint = "false";
                //            autopdf = "false";
                //        }
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("First Set Path from Options");
                //}
            }
            catch
            {
            }
        }

        private void MousedblClick()
        {
            if (userrights.Rows.Count > 0)
            {
                if (strfinalarray[0] == "SO")
                {
                    if (userrights.Rows[0]["v"].ToString() == "True" || userrights.Rows[0]["u"].ToString() == "True")
                    {
                        setform();
                    }
                    else
                    {
                        MessageBox.Show("You don't have Permission To View");
                        return;
                    }
                }
                else if (strfinalarray[0] == "SC")
                {
                    if (userrights.Rows[13]["v"].ToString() == "True" || userrights.Rows[13]["u"].ToString() == "True")
                    {
                        setform();
                    }
                    else
                    {
                        MessageBox.Show("You don't have Permission To View");
                        return;
                    }
                }
                else if (strfinalarray[0] == "PO")
                {
                    if (userrights.Rows[3]["v"].ToString() == "True" || userrights.Rows[3]["u"].ToString() == "True")
                    {
                        setform();
                    }
                    else
                    {
                        MessageBox.Show("You don't have Permission To View");
                        return;
                    }
                }
                else if (strfinalarray[0] == "PC")
                {
                    if (userrights.Rows[16]["v"].ToString() == "True" || userrights.Rows[16]["u"].ToString() == "True")
                    {
                        setform();
                    }
                    else
                    {
                        MessageBox.Show("You don't have Permission To View");
                        return;
                    }
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                bindsearch();
            }
            catch
            {
            }
            finally
            {
                con.Close();
            }
        }
        private void bindsearch()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();

            LVDayBook.Items.Clear();
            // SqlCommand cmd = new SqlCommand("select BillMaster.,ClientMaster.ClientName from BillMaster inner join ClientMaster on clientMaster.ClientID = billmaster.ClientID where Bill_Date>='"+Convert.ToDateTime(DTPFrom.Text).ToString("mm-dd-yyyy")+"' and Bill_Date<='"+Convert.ToDateTime(DTPTo.Text).ToString("mm-dd-yyyy")+"'", con);
            //SqlCommand cmd = new SqlCommand("select bm.Bill_No,bm.Bill_Date,bm.PO_No,cm.On_Bill_desc,cm.TinNO,bm.Bill_Amt,bm.Bill_vat_Amt,bm.Bill_Net_Amt from BillMaster bm inner join ClientMaster cm on cm.ClientID = bm.ClientID where Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString("MM-dd-yyyy") + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString("MM-dd-yyyy") + "' and bm.CompanyID='" + cmbcomp.SelectedValue + "'order by bm.Bill_No", con);
            // SqlCommand cmd = new SqlCommand("select b.billno, b.bill_date, b.po_no, c.accountname,b.refno,b.totalcharges,b.totalbasic,b.totaltax,b.totalnet from billmaster b inner join clientmaster c on c.clientid=b.clientid  where b.isactive=1 and BillType='" + strfinalarray[0] + "' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by b.bill_date asc", con);
            //SqlCommand cmd = new SqlCommand("select b.billno, b.bill_date, b.po_no, c.accountname,b.refno,b.totalcharges,b.totalbasic,sum(b.totaltax + isnull(bc.sgst,0)+isnull(bc.cgst,0)+isnull(bc.igst,0)) as totaltax,b.totalnet from billmaster b left join Billchargesmaster bc on bc.billno=b.billno and bc.isactive=1 inner join clientmaster c on c.clientid=b.clientid  where c.isactive=1 and b.isactive=1 and b.BillType='" + strfinalarray[0] + "' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'group by b.billno, b.bill_date, b.po_no, c.accountname,b.refno,b.totalcharges,b.totalbasic,b.totalnet order by b.bill_date asc", con);
            //b.billno, b.bill_date, b.po_no, c.accountname,b.refno,b.totalcharges,sum(b.totalbasic - b.totaldiscount) as totalbasic,sum(b.totaltax + isnull(bc.sgst,0)+isnull(bc.cgst,0)+isnull(bc.igst,0)) as totaltax,b.totalnet,b.ClientID,b.Terms,b.cusname
            string qry;
            if (rbpending.Checked == true)
            {
                qry = "select b.billno, b.bill_date, b.po_no, c.accountname,b.refno,b.totalcharges,b.totalbasic,sum(b.totaltax + isnull(bc.sgst,0)+isnull(bc.cgst,0)+isnull(bc.igst,0)) as totaltax,b.totalnet,b.ClientID,c.Mobile,c.GstNo,b.cgatamt,b.sgstamt from SaleOrderMaster b left join SaleOrderchargesmaster bc on bc.billno=b.billno and bc.isactive=1 inner join clientmaster c on c.clientid=b.clientid  where c.isactive=1 and b.isactive=1 and b.OrderStatus='Pending'";
            }
            else if (rbclear.Checked == true)
            {
                qry = "select b.billno, b.bill_date, b.po_no, c.accountname,b.refno,b.totalcharges,b.totalbasic,sum(b.totaltax + isnull(bc.sgst,0)+isnull(bc.cgst,0)+isnull(bc.igst,0)) as totaltax,b.totalnet,b.ClientID,c.Mobile,c.GstNo,b.cgatamt,b.sgstamt,b.ClientID from SaleOrderMaster b left join SaleOrderchargesmaster bc on bc.billno=b.billno and bc.isactive=1 inner join clientmaster c on c.clientid=b.clientid  where c.isactive=1 and b.isactive=1 and b.OrderStatus='Clear'";
            }
            else
            {
                qry = "select b.billno, b.bill_date, b.po_no, c.accountname,b.refno,b.totalcharges,b.totalbasic,sum(b.totaltax + isnull(bc.sgst,0)+isnull(bc.cgst,0)+isnull(bc.igst,0)) as totaltax,b.totalnet,b.ClientID,c.Mobile,c.GstNo,b.cgatamt,b.sgstamt,b.ClientID from SaleOrderMaster b left join SaleOrderchargesmaster bc on bc.billno=b.billno and bc.isactive=1 inner join clientmaster c on c.clientid=b.clientid  where c.isactive=1 and b.isactive=1 ";
            }
            if (txtaccount.Text != "" && txtitems.Text == "")
            {
                qry += "and (" + drpaccount.Text + " like '%" + txtaccount.Text + "%')";
            }
            else if (txtaccount.Text != "" && txtitems.Text != "")
            {
                qry += "and (" + drpaccount.Text + " like '%" + txtaccount.Text + "%' and " + drpitems.Text + " like '%" + txtitems.Text + "%')";
            }
            else if (txtaccount.Text == "" && txtitems.Text != "")
            {
                qry += "and (" + drpitems.Text + " like '%" + txtitems.Text + "%')";
            }
            qry += " and b.BillType='" + strfinalarray[0] + "' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'group by b.billno, b.bill_date, b.po_no, c.accountname,b.refno,b.totalcharges,b.totalbasic,b.totalnet,c.GstNo,b.cgatamt,b.sgstamt,b.ClientID,c.Mobile order by b.bill_date asc";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            //LVDayBook.Items.Clear();
            //int tot = 0, cash = 0, cheque = 0;
            //Double amt = 0, cashamt = 0, chequeamt = 0;
            //if (dt.Rows.Count > 0)
            //{
            //    tot = dt.Rows.Count;
            bill = 0;
            total = 0;
            vat = 0;
            net = 0;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    LVDayBook.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                    LVDayBook.Items[i].SubItems.Add(Convert.ToDateTime(dt.Rows[i].ItemArray[1].ToString()).ToString(Master.dateformate));
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[7].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[8].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[9].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[10].ToString());

                    bill++;
                    total = total + Convert.ToDouble(dt.Rows[i][6].ToString());
                    vat = vat + Convert.ToDouble(dt.Rows[i][7].ToString());
                    net = net + Convert.ToDouble(dt.Rows[i][8].ToString());
                }

                TxtInvoice.Text = bill.ToString();
                txtbillamt.Text = total.ToString("N2");
                txtvat.Text = vat.ToString("N2");
                txtnetamt.Text = net.ToString("N2");
            }
        }

        private void bindfilter1drop()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("select column_name from INFORMATION_SCHEMA.COLUMNS where (TABLE_NAME='BillMaster' or TABLE_NAME='clientmaster') and (COLUMN_NAME like '%billno%' or COLUMN_NAME like '%Terms%' or COLUMN_NAME like '%PO_no%' or COLUMN_NAME like '%apprweight%' or COLUMN_NAME like '%dispatchdetails%' or COLUMN_NAME like '%remarks%' or COLUMN_NAME like '%duedate%' or COLUMN_NAME like '%delieveryat%' or COLUMN_NAME like '%fraight%' or COLUMN_NAME like '%vehicleno%' or COLUMN_NAME like '%grrrno%' or COLUMN_NAME like '%noofskids%' or COLUMN_NAME like '%cusname%' or COLUMN_NAME like '%AccountName%' or COLUMN_NAME like '%GroupName%' or COLUMN_NAME like '%Address%' or COLUMN_NAME like '%City%' or COLUMN_NAME like '%State%' or COLUMN_NAME like '%Phone%' or COLUMN_NAME like '%Mobile%' or COLUMN_NAME like '%Email%' or COLUMN_NAME like '%GstNo%' or COLUMN_NAME like '%AdharNo%' or COLUMN_NAME like '%statecode%' or COLUMN_NAME like '%accountnumber%' or COLUMN_NAME like '%customertype%' or COLUMN_NAME like '%noteorremarks%')", con);
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
                    drpitems.DataSource = dt;
                    drpitems.DisplayMember = "column_name";
                    drpaccount.DataSource = dt;
                    drpaccount.DisplayMember = "column_name";
                    drpitems.ValueMember = "ClientID";


                    drpaccount.ValueMember = "ClientID";
                }




            }
            catch
            {
            }
        }
        private void bindfilter2drop()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("select column_name from INFORMATION_SCHEMA.COLUMNS where (TABLE_NAME='BillMaster' or TABLE_NAME='clientmaster') and (COLUMN_NAME like '%billno%' or COLUMN_NAME like '%Terms%' or COLUMN_NAME like '%PO_no%' or COLUMN_NAME like '%apprweight%' or COLUMN_NAME like '%dispatchdetails%' or COLUMN_NAME like '%remarks%' or COLUMN_NAME like '%duedate%' or COLUMN_NAME like '%delieveryat%' or COLUMN_NAME like '%fraight%' or COLUMN_NAME like '%vehicleno%' or COLUMN_NAME like '%grrrno%' or COLUMN_NAME like '%noofskids%' or COLUMN_NAME like '%cusname%' or COLUMN_NAME like '%AccountName%' or COLUMN_NAME like '%GroupName%' or COLUMN_NAME like '%Address%' or COLUMN_NAME like '%City%' or COLUMN_NAME like '%State%' or COLUMN_NAME like '%Phone%' or COLUMN_NAME like '%Mobile%' or COLUMN_NAME like '%Email%' or COLUMN_NAME like '%GstNo%' or COLUMN_NAME like '%AdharNo%' or COLUMN_NAME like '%statecode%' or COLUMN_NAME like '%accountnumber%' or COLUMN_NAME like '%customertype%' or COLUMN_NAME like '%noteorremarks%')", con);
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

                    drpaccount.DataSource = dt;
                    drpaccount.DisplayMember = "column_name";
                    drpaccount.ValueMember = "ClientID";
                }




            }
            catch
            {
            }
        }
    }
}
