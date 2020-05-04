using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RamdevSales
{
    public partial class DateWiseSaleOrderReport : Form
    {
        Connection conn = new Connection();
       // Connection con = new Connection();
        DataSet ds = new DataSet();
        DataTable dt, dt3 = new DataTable();
        static Int32 bill;
        static double total, vat, net;
     
        public DateWiseSaleOrderReport()
        {
            InitializeComponent();
        }

        private void DateWiseSaleOrderReport_Load(object sender, EventArgs e)
        {

            LVDayBook.Columns.Add("Bill No", 70, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("Bill Date", 100, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("ClientName", 300, HorizontalAlignment.Left);
            LVDayBook.Columns.Add("Address", 200, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("Total Qty", 100, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("CompId", 0, HorizontalAlignment.Center);
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
                DataTable dt3 = new DataTable();
                dt3 = conn.getdataset("select a, u, d, v, p, mId, uId, cId from UserRights where mId=2 and uId='" + UserLogin.id + "' and cId= " + Master.companyId + " and isActive=1");


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
                    if (Convert.ToBoolean(dt3.Rows[0][4]) == false)
                    {
                        btngenrpt.Visible = false;

                    }

                }
                LVDayBook.Items.Clear();
                
                //dt = conn.getdataset("select b.BillNo, b.BillDate, c.subname,c.address,b.totalbasic,b.totaltax,b.TotalDiscount,b.totalnet from SaleMaster b inner join Company c on c.CompanyId=b.CompanyId  where b.isactive=1 and b.BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString("MM-dd-yyyy") + "' and b.BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString("MM-dd-yyyy") + "' order by b.VchNo");
                //DataTable company = new DataTable();
                //company = conn.getdataset("select CompanyType,CompanyID from company Where CompanyID='" + Master.companyId + "'");
                //int cID = Convert.ToInt32(company.Rows[0]["CompanyID"].ToString());
                if (Master.companyType == "Factory")
                {
                    dt = conn.getdataset("select b.OrderNo,convert(varchar(11), b.OrderDate, 113)as OrderDate, c.printname,c.address,b.totalqty,b.CompanyId from PurchaseOrderMaster b inner join Clientmaster c on c.clientid=b.CompanyId  where b.isactive=1 and b.OrderDate>='" + Convert.ToDateTime(DTPFrom.Text) + "' and b.OrderDate<='" + Convert.ToDateTime(DTPTo.Text).AddDays(1) + "' order by b.VchNo");
                }
                else
                {
                    dt = conn.getdataset("select b.OrderNo,convert(varchar(11), b.OrderDate, 113)as OrderDate, c.subname,c.address,b.totalqty,b.CompanyId from PurchaseOrderMaster b inner join Company c on c.CompanyId=b.CompanyId  where b.isactive=1 and b.OrderDate>='" + Convert.ToDateTime(DTPFrom.Text) + "' and b.OrderDate<='" + Convert.ToDateTime(DTPTo.Text).AddDays(1) + "' order by b.VchNo");
                }

                

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
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                        
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                        //LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                        //LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[7].ToString());

                        bill++;
                        //total = total + Convert.ToDouble(dt.Rows[i][4].ToString());
                        //vat = vat + Convert.ToDouble(dt.Rows[i][5].ToString());
                        //net = net + Convert.ToDouble(dt.Rows[i][7].ToString());
                    }

                    TxtInvoice.Text = bill.ToString();
                    txtbillamt.Text = total.ToString("N2");
                    txtvat.Text = vat.ToString("N2");
                    txtnetamt.Text = net.ToString("N2");
                    //DataTable dt4 = new DataTable();
                    //dt4 = con.getdataset("select CompanyName,Address,Phone,VATNo from Company where CompanyID='" + Master.companyId + "' and isActive=1");
                   
                    conn.execute("delete from printing");
                    
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string qry = "INSERT INTO [dbo].[Printing]([T1],[T2],[T3],[T4],[T5])VALUES";
                            qry += "('" + dt.Rows[i][0].ToString() + "','" + dt.Rows[i][1].ToString() + "','" + dt.Rows[i][2].ToString() + "','" + dt.Rows[i][3].ToString() + "','" + dt.Rows[i][4].ToString() + "')";
                            //cmd3 = new SqlCommand(qry, con);
                            //cmd3.ExecuteNonQuery();
                            conn.execute(qry);

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
                String compid = LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[5].Text;
                SaleOfOrder bd = new SaleOfOrder(this);
                bd.updatemode(str, compid, 1);
                bd.MdiParent = this.MdiParent;
                bd.StartPosition = FormStartPosition.CenterScreen;
                
                bd.Show();
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
            SaleOfOrder frm = new SaleOfOrder();
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
            DataTable dtcomp = new DataTable();
            dtcomp = conn.getdataset("Select CompanyType from Company where isactive=1 and Companyid=" + Master.companyId + "");
            if (dtcomp != null && dtcomp.Rows.Count > 0)
            {
                if (dtcomp.Rows[0][0].ToString() == "Factory")
                {
                    Purchase_Order_Master_Report frm1 = new Purchase_Order_Master_Report();
                    frm1.StartPosition = FormStartPosition.CenterScreen;
                    frm1.Show();
                }
                else
                {
                    purchaseoreport frm = new purchaseoreport();

                    frm.StartPosition = FormStartPosition.CenterScreen;

                    frm.Show();
                }
            }

            
        }

    }
}
