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
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace RamdevSales
{
    public partial class DateWisePurchaseOrderReport : Form
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        //SqlCommand cmd;

        Connection con = new Connection();
        Printing prn = new Printing();
        DataSet ds = new DataSet();
        DataTable dt,dt3 = new DataTable();
        static Int32 bill;
        static double total, vat, net;

        public DateWisePurchaseOrderReport()
        {
            InitializeComponent();
        }

        private void DateWisePurchaseOrderReport_Load(object sender, EventArgs e)
        {
            LVDayBook.Columns.Add("Order No", 70, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("Order Date", 100, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("ClientName", 300, HorizontalAlignment.Left);
            LVDayBook.Columns.Add("Address", 200, HorizontalAlignment.Center);
            //LVDayBook.Columns.Add("Total Amt", 100, HorizontalAlignment.Center);
            //LVDayBook.Columns.Add("Discount", 100, HorizontalAlignment.Center);
            //LVDayBook.Columns.Add("TAX Amt", 100, HorizontalAlignment.Center);
            //LVDayBook.Columns.Add("Net Amt", 120, HorizontalAlignment.Center);
            bindgrid();
            btnnew.Focus();
        }

        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            bindgrid();

        }

        public void bindgrid()
        {
            try
            {
                dt3 = con.getdataset("select a, u, d, v, p, mId, uId, cId from UserRights where mId=5 and uId='"+UserLogin.id+"' and cId= " + Master.companyId + " and isActive=1");


                if (dt3.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dt3.Rows[0][0]) == false)
                    {
                        btnnew.Visible = false;
                    }
                    if (Convert.ToBoolean(dt3.Rows[0][1]) == false)
                    {
                        LVDayBook.Enabled = false;
                        LVDayBook.GridLines = true;
                        LVDayBook.BackColor = System.Drawing.Color.LightYellow;

                    }
                    if(Convert.ToBoolean(dt3.Rows[0][4]) == false)
                    {
                        btngenrpt.Visible = false;

                    }

                }
                LVDayBook.Items.Clear();
                dt = con.getdataset("select b.OrderNo,convert(varchar(11), b.OrderDate, 113)as OrderDate, c.printname,c.address,b.totalbasic,b.totaltax,b.TotalDiscount,b.totalnet from PurchaseOrderMaster b inner join clientmaster c on c.clientid=b.clientid  where b.isactive=1 and b.CompanyId=" + Master.companyId + " and b.OrderDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString("MM-dd-yyyy") + "' and b.OrderDate<='" + Convert.ToDateTime(DTPTo.Text).AddDays(1).ToString("MM-dd-yyyy") + "' order by b.VchNo");
                //dt = con.getdataset("select b.OrderNo, b.OrderDate, c.subname,c.address from PurchaseOrderMaster b inner join Company c on c.CompanyId=b.CompanyId where b.isactive=1 and b.CompanyId=" + Master.companyId + " and b.OrderDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString("MM-dd-yyyy") + "' and b.OrderDate<='" + Convert.ToDateTime(DTPTo.Text).ToString("MM-dd-yyyy") + "' order by b.VchNo");
                
                bill = 0;
                total = 0;
                vat = 0;
                net = 0;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        LVDayBook.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                        //LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                        //LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                        //LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                        //LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[7].ToString());

                        bill++;
                        DateTime d = Convert.ToDateTime(dt.Rows[i][1].ToString());
                        if (d == DateTime.Now.Date)
                    {
                        btnnew.Visible = false;
                    }
                        //total = total + Convert.ToDouble(dt.Rows[i][4].ToString());
                        //vat = vat + Convert.ToDouble(dt.Rows[i][5].ToString());
                        //net = net + Convert.ToDouble(dt.Rows[i][7].ToString());
                    }
                    
                    TxtInvoice.Text = bill.ToString();
                    txtbillamt.Text = total.ToString("N2");
                    txtvat.Text = vat.ToString("N2");
                    txtnetamt.Text = net.ToString("N2");
                    DataTable dt4 = new DataTable();
                    dt4 = con.getdataset("select * from Company where CompanyID='" + Master.companyId + "' and isActive=1");

                    prn.execute("delete from printing");
                    if (dt4.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string qry = "INSERT INTO [Printing]([T1],[T2],[T3],[T4],[T5],[T6],[T7],[T8],[T9],[T10],[T11],[T12])VALUES";
                            qry += "('" + dt.Rows[i][0].ToString() + "','" + dt.Rows[i][1].ToString() + "','" + dt.Rows[i][2].ToString() + "','" + dt.Rows[i][3].ToString() + "','0','0','0','0','" + dt4.Rows[0]["CompanyName"].ToString() + "','" + dt4.Rows[0]["Address"].ToString() + "'," + dt4.Rows[0]["Phone"].ToString() + ",'" + dt4.Rows[0]["VATNo"].ToString() + "')";
                            //cmd3 = new SqlCommand(qry, con);
                            //cmd3.ExecuteNonQuery();
                            prn.execute(qry);

                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally
            {
                
            }
        }

        private void DTPFrom_ValueChanged(object sender, EventArgs e)
        {
            DTPTo.MinDate = Convert.ToDateTime(DTPFrom.Text);
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void LVDayBook_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            listback();
        }

        private void listback()
        {
            try
            {
                String str = LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[0].Text;

                PurchaseOrder bd = new PurchaseOrder(this);
                bd.updatemode(str, LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[0].Text, 1);
                bd.MdiParent = this.MdiParent;
                bd.StartPosition = FormStartPosition.CenterScreen;
                bd.Show();
                //this.Close();
            }
            catch { }
        }

        private void LVDayBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                listback();
            }
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            PurchaseOrder frm = new PurchaseOrder();
            frm.MdiParent = this.MdiParent;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
           // this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btngenrpt_Click(object sender, EventArgs e)
        {
            Print popup = new Print("Purchase Order");
            popup.ShowDialog();
            popup.Dispose();
        }

    }
}
