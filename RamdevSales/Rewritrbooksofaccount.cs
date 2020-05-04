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
    public partial class Rewritrbooksofaccount : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        OleDbSettings ods = new OleDbSettings();
        Connection conn = new Connection();
        private Master master;
        private TabControl tabControl;
        public static string rewritedata;

        public Rewritrbooksofaccount()
        {
            InitializeComponent();
        }

        public Rewritrbooksofaccount(Master master, TabControl tabControl)
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
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }

        private void Rewritrbooksofaccount_Load(object sender, EventArgs e)
        {
            try
            {
                LVDayBook.CheckBoxes = true;
                LVDayBook.Columns.Add("", 20, HorizontalAlignment.Left);
                LVDayBook.Columns.Add("Entery", 300, HorizontalAlignment.Left);
                LVDayBook.Items.Add("");
                LVDayBook.Items[0].SubItems.Add("Sale");
                LVDayBook.Items.Add("");
                LVDayBook.Items[1].SubItems.Add("Sale Return");
                LVDayBook.Items.Add("");
                LVDayBook.Items[2].SubItems.Add("Sale Order");
                LVDayBook.Items.Add("");
                LVDayBook.Items[3].SubItems.Add("Sale Challan");
                LVDayBook.Items.Add("");
                LVDayBook.Items[4].SubItems.Add("Purchase");
                LVDayBook.Items.Add("");
                LVDayBook.Items[5].SubItems.Add("Purchase Return");
                LVDayBook.Items.Add("");
                LVDayBook.Items[6].SubItems.Add("Purchase Order");
                LVDayBook.Items.Add("");
                LVDayBook.Items[7].SubItems.Add("Purchase Challan");
                LVDayBook.Items.Add("");
                LVDayBook.Items[8].SubItems.Add("POS");
                DTPFrom.CustomFormat = Master.dateformate;
                DTPTo.CustomFormat = Master.dateformate;
                this.ActiveControl = DTPFrom;
            }
            catch
            {
            }
        }
        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            try
            {
                // strfinalarray = new string[LVDayBook.CheckedItems.Count];
                for (int i = 0; i < LVDayBook.Items.Count; i++)
                {
                    if (Convert.ToBoolean(LVDayBook.Items[i].Checked) == true)
                    {
                        if (LVDayBook.Items[i].SubItems[1].Text == "Sale")
                        {
                            rewritedata = "True";
                            string[] strfinalarray = new string[5] { "S", "D", "Sale", "S", "" };
                            DataTable dt = conn.getdataset("select b.billno, b.bill_date, b.po_no, c.accountname,b.refno,b.totalcharges,b.totalbasic,sum(b.totaltax + isnull(bc.sgst,0)+isnull(bc.cgst,0)+isnull(bc.igst,0)) as totaltax,b.totalnet,c.GstNo,b.cgatamt,b.sgstamt,b.ClientID from billmaster b left join Billchargesmaster bc on bc.billno=b.billno and bc.isactive=1 inner join clientmaster c on c.clientid=b.clientid  where c.isactive=1 and b.isactive=1 and b.BillType='" + strfinalarray[0] + "' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'group by b.billno, b.bill_date, b.po_no, c.accountname,b.refno,b.totalcharges,b.totalbasic,b.totalnet,c.GstNo,b.cgatamt,b.sgstamt,b.clientID order by b.bill_date asc");
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                try
                                {
                                    String str = dt.Rows[j]["billno"].ToString();
                                    DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                                    DefaultSale bd = new DefaultSale(master, tabControl, strfinalarray);
                                    //  Sale p = new Sale(this, master, tabControl);
                                    if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                                    {
                                        bd.updatemode(str, dt.Rows[j]["billno"].ToString(), Convert.ToInt32(dt.Rows[j]["ClientID"].ToString()), strfinalarray);
                                        master.AddNewTab(bd);
                                        bd.BtnPayment_Click(sender, e);
                                        master.RemoveCurrentTab();
                                    }
                                }
                                catch
                                {
                                }
                                finally
                                {
                                }
                            }

                        }
                        else if (LVDayBook.Items[i].SubItems[1].Text == "Sale Return")
                        {
                            rewritedata = "True";
                            string[] strfinalarray = new string[5] { "SR", "C", "SaleReturn", "SR", "" };
                            DataTable dt = conn.getdataset("select b.billno, b.bill_date, b.po_no, c.accountname,b.refno,b.totalcharges,b.totalbasic,sum(b.totaltax + isnull(bc.sgst,0)+isnull(bc.cgst,0)+isnull(bc.igst,0)) as totaltax,b.totalnet,c.GstNo,b.cgatamt,b.sgstamt from billmaster b left join Billchargesmaster bc on bc.billno=b.billno and bc.isactive=1 inner join clientmaster c on c.clientid=b.clientid  where c.isactive=1 and b.isactive=1 and b.BillType='" + strfinalarray[0] + "' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'group by b.billno, b.bill_date, b.po_no, c.accountname,b.refno,b.totalcharges,b.totalbasic,b.totalnet,c.GstNo,b.cgatamt,b.sgstamt order by b.bill_date asc");
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                try
                                {
                                    String str = dt.Rows[j]["billno"].ToString();
                                    DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                                    DefaultSale bd = new DefaultSale(master, tabControl, strfinalarray);
                                    //  Sale p = new Sale(this, master, tabControl);
                                    if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                                    {
                                        bd.updatemode(str, dt.Rows[j]["billno"].ToString(), 1, strfinalarray);
                                        master.AddNewTab(bd);
                                        bd.BtnPayment_Click(sender, e);
                                        master.RemoveCurrentTab();
                                    }
                                }
                                catch
                                {
                                }
                                finally
                                {
                                }
                            }
                        }
                        else if (LVDayBook.Items[i].SubItems[1].Text == "Sale Order")
                        {
                            rewritedata = "True";
                            string[] strfinalarray = new string[5] { "SO", "D", "Sale Order", "SO", "" };
                            DataTable dt = conn.getdataset("select b.billno, b.bill_date, b.po_no, c.accountname,b.refno,b.totalcharges,b.totalbasic,sum(b.totaltax + isnull(bc.sgst,0)+isnull(bc.cgst,0)+isnull(bc.igst,0)) as totaltax,b.totalnet,c.GstNo,b.cgatamt,b.sgstamt from SaleOrderMaster b left join SaleOrderchargesmaster bc on bc.billno=b.billno and bc.isactive=1 inner join clientmaster c on c.clientid=b.clientid  where c.isactive=1 and b.isactive=1 and b.BillType='" + strfinalarray[0] + "' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'group by b.billno, b.bill_date, b.po_no, c.accountname,b.refno,b.totalcharges,b.totalbasic,b.totalnet,c.GstNo,b.cgatamt,b.sgstamt order by b.bill_date asc");
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                try
                                {
                                    String str = dt.Rows[j]["billno"].ToString();
                                    DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                                    DefaultSaleOrder bd = new DefaultSaleOrder(master, tabControl, strfinalarray);
                                    //  Sale p = new Sale(this, master, tabControl);
                                    if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                                    {
                                        bd.updatemode(str, dt.Rows[j]["billno"].ToString(), 1, strfinalarray);
                                        master.AddNewTab(bd);
                                        bd.BtnPayment_Click(sender, e);
                                        master.RemoveCurrentTab();
                                    }
                                }
                                catch
                                {
                                }
                                finally
                                {
                                }
                            }
                        }
                        else if (LVDayBook.Items[i].SubItems[1].Text == "Sale Challan")
                        {
                            rewritedata = "True";
                            string[] strfinalarray = new string[5] { "SC", "D", "Sale Challan", "SC", "" };
                            DataTable dt = conn.getdataset("select b.billno, b.bill_date, b.po_no, c.accountname,b.refno,b.totalcharges,b.totalbasic,sum(b.totaltax + isnull(bc.sgst,0)+isnull(bc.cgst,0)+isnull(bc.igst,0)) as totaltax,b.totalnet,c.GstNo,b.cgatamt,b.sgstamt from SaleOrderMaster b left join SaleOrderchargesmaster bc on bc.billno=b.billno and bc.isactive=1 inner join clientmaster c on c.clientid=b.clientid  where c.isactive=1 and b.isactive=1 and b.BillType='" + strfinalarray[0] + "' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'group by b.billno, b.bill_date, b.po_no, c.accountname,b.refno,b.totalcharges,b.totalbasic,b.totalnet,c.GstNo,b.cgatamt,b.sgstamt order by b.bill_date asc");
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                try
                                {
                                    String str = dt.Rows[j]["billno"].ToString();
                                    DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                                    DefaultSaleOrder bd = new DefaultSaleOrder(master, tabControl, strfinalarray);
                                    //  Sale p = new Sale(this, master, tabControl);
                                    if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                                    {
                                        bd.updatemode(str, dt.Rows[j]["billno"].ToString(), 1, strfinalarray);
                                        master.AddNewTab(bd);
                                        bd.BtnPayment_Click(sender, e);
                                        master.RemoveCurrentTab();
                                    }
                                }
                                catch
                                {
                                }
                                finally
                                {
                                }
                            }
                        }
                        else if (LVDayBook.Items[i].SubItems[1].Text == "Purchase")
                        {
                            rewritedata = "True";
                            string[] strfinalarray = new string[5] { "P", "C", "Purchase", "P", "" };
                            DataTable dt = conn.getdataset("select b.billno, b.bill_date, b.po_no, c.accountname,b.refno,b.totalcharges,b.totalbasic,sum(b.totaltax + isnull(bc.sgst,0)+isnull(bc.cgst,0)+isnull(bc.igst,0)) as totaltax,b.totalnet,c.GstNo,b.cgatamt,b.sgstamt,b.clientID from billmaster b left join Billchargesmaster bc on bc.billno=b.billno and bc.isactive=1 inner join clientmaster c on c.clientid=b.clientid  where c.isactive=1 and b.isactive=1 and b.BillType='" + strfinalarray[0] + "' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'group by b.billno, b.bill_date, b.po_no, c.accountname,b.refno,b.totalcharges,b.totalbasic,b.totalnet,c.GstNo,b.cgatamt,b.sgstamt,b.clientID order by b.bill_date asc");
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                try
                                {
                                    String str = dt.Rows[j]["billno"].ToString();
                                    DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                                    DefaultSale bd = new DefaultSale(master, tabControl, strfinalarray);
                                    //  Sale p = new Sale(this, master, tabControl);
                                    if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                                    {
                                        bd.updatemode(str, dt.Rows[j]["billno"].ToString(),Convert.ToInt32(dt.Rows[j]["ClientID"].ToString()), strfinalarray);
                                        master.AddNewTab(bd);
                                        bd.BtnPayment_Click(sender, e);
                                        master.RemoveCurrentTab();
                                    }
                                }
                                catch
                                {
                                }
                                finally
                                {
                                }
                            }
                        }
                        else if (LVDayBook.Items[i].SubItems[1].Text == "Purchase Return")
                        {
                            rewritedata = "True";
                            string[] strfinalarray = new string[5] { "PR", "D", "PurchaseReturn", "PR", "" };
                            DataTable dt = conn.getdataset("select b.billno, b.bill_date, b.po_no, c.accountname,b.refno,b.totalcharges,b.totalbasic,sum(b.totaltax + isnull(bc.sgst,0)+isnull(bc.cgst,0)+isnull(bc.igst,0)) as totaltax,b.totalnet,c.GstNo,b.cgatamt,b.sgstamt from billmaster b left join Billchargesmaster bc on bc.billno=b.billno and bc.isactive=1 inner join clientmaster c on c.clientid=b.clientid  where c.isactive=1 and b.isactive=1 and b.BillType='" + strfinalarray[0] + "' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'group by b.billno, b.bill_date, b.po_no, c.accountname,b.refno,b.totalcharges,b.totalbasic,b.totalnet,c.GstNo,b.cgatamt,b.sgstamt order by b.bill_date asc");
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                try
                                {
                                    String str = dt.Rows[j]["billno"].ToString();
                                    DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                                    DefaultSale bd = new DefaultSale(master, tabControl, strfinalarray);
                                    //  Sale p = new Sale(this, master, tabControl);
                                    if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                                    {
                                        bd.updatemode(str, dt.Rows[j]["billno"].ToString(), 1, strfinalarray);
                                        master.AddNewTab(bd);
                                        bd.BtnPayment_Click(sender, e);
                                        master.RemoveCurrentTab();
                                    }
                                }
                                catch
                                {
                                }
                                finally
                                {
                                }
                            }
                        }
                        else if (LVDayBook.Items[i].SubItems[1].Text == "Purchase Order")
                        {
                            rewritedata = "True";
                            string[] strfinalarray = new string[5] { "PO", "C", "Purchase Order", "PO", "" };
                            DataTable dt = conn.getdataset("select b.billno, b.bill_date, b.po_no, c.accountname,b.refno,b.totalcharges,b.totalbasic,sum(b.totaltax + isnull(bc.sgst,0)+isnull(bc.cgst,0)+isnull(bc.igst,0)) as totaltax,b.totalnet,c.GstNo,b.cgatamt,b.sgstamt from SaleOrderMaster b left join SaleOrderchargesmaster bc on bc.billno=b.billno and bc.isactive=1 inner join clientmaster c on c.clientid=b.clientid  where c.isactive=1 and b.isactive=1 and b.BillType='" + strfinalarray[0] + "' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'group by b.billno, b.bill_date, b.po_no, c.accountname,b.refno,b.totalcharges,b.totalbasic,b.totalnet,c.GstNo,b.cgatamt,b.sgstamt order by b.bill_date asc");
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                try
                                {
                                    String str = dt.Rows[j]["billno"].ToString();
                                    DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                                    DefaultSaleOrder bd = new DefaultSaleOrder(master, tabControl, strfinalarray);
                                    //  Sale p = new Sale(this, master, tabControl);
                                    if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                                    {
                                        bd.updatemode(str, dt.Rows[j]["billno"].ToString(), 1, strfinalarray);
                                        master.AddNewTab(bd);
                                        bd.BtnPayment_Click(sender, e);
                                        master.RemoveCurrentTab();
                                    }
                                }
                                catch
                                {
                                }
                                finally
                                {
                                }
                            }
                        }
                        else if (LVDayBook.Items[i].SubItems[1].Text == "Purchase Challan")
                        {
                            rewritedata = "True";
                            string[] strfinalarray = new string[5] { "PC", "C", "Purchase Challan", "PC", "" };
                            DataTable dt = conn.getdataset("select b.billno, b.bill_date, b.po_no, c.accountname,b.refno,b.totalcharges,b.totalbasic,sum(b.totaltax + isnull(bc.sgst,0)+isnull(bc.cgst,0)+isnull(bc.igst,0)) as totaltax,b.totalnet,c.GstNo,b.cgatamt,b.sgstamt from SaleOrderMaster b left join SaleOrderchargesmaster bc on bc.billno=b.billno and bc.isactive=1 inner join clientmaster c on c.clientid=b.clientid  where c.isactive=1 and b.isactive=1 and b.BillType='" + strfinalarray[0] + "' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'group by b.billno, b.bill_date, b.po_no, c.accountname,b.refno,b.totalcharges,b.totalbasic,b.totalnet,c.GstNo,b.cgatamt,b.sgstamt order by b.bill_date asc");
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                try
                                {
                                    String str = dt.Rows[j]["billno"].ToString();
                                    DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                                    DefaultSaleOrder bd = new DefaultSaleOrder(master, tabControl, strfinalarray);
                                    //  Sale p = new Sale(this, master, tabControl);
                                    if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                                    {
                                        bd.updatemode(str, dt.Rows[j]["billno"].ToString(), 1, strfinalarray);
                                        master.AddNewTab(bd);
                                        bd.BtnPayment_Click(sender, e);
                                        master.RemoveCurrentTab();
                                    }
                                }
                                catch
                                {
                                }
                                finally
                                {
                                }
                            }
                        }

                    }
                }
            }
            catch
            {
            }
        }
    }
}
