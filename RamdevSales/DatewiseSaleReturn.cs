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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.rtf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Web;

namespace RamdevSales
{
    public partial class DatewiseSaleReturn : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        SqlCommand cmd;
        SqlDataAdapter sda;
        Printing prn = new Printing();
        Connection conn = new Connection();
        static Int32 bill;
        static double total, vat, net;
        public DatewiseSaleReturn()
        {
            InitializeComponent();
        }

        private void DateWiseReport_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet dtrange = conn.getdata("SELECT * FROM Company where CompanyId='" + Master.companyId + "'");
                DTPFrom.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["StartDate"].ToString());
                DTPFrom.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["EndDate"].ToString());
                DTPTo.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["StartDate"].ToString());
                DTPTo.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["EndDate"].ToString());
                DTPFrom.Value = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["StartDate"].ToString());
                LVDayBook.Columns.Add("Bill No", 70, HorizontalAlignment.Center);
                LVDayBook.Columns.Add("Bill Date", 100, HorizontalAlignment.Center);
                LVDayBook.Columns.Add("PO No", 100, HorizontalAlignment.Left);
                LVDayBook.Columns.Add("ClientName", 300, HorizontalAlignment.Left);
                LVDayBook.Columns.Add("Address", 200, HorizontalAlignment.Center);
                LVDayBook.Columns.Add("Total Amt", 100, HorizontalAlignment.Center);
                LVDayBook.Columns.Add("TAX Amt", 100, HorizontalAlignment.Center);
                LVDayBook.Columns.Add("Net Amt", 120, HorizontalAlignment.Center);
                bindgrid();
                btnnew.Focus();
            }
            catch { }
        }
       
        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            bindgrid();
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
                SqlCommand cmd = new SqlCommand("select b.bill_no,convert(varchar(11), b.bill_date, 113)as bill_date , b.po_no, c.subname,c.address,b.totalbasic,b.totaltax,b.totalnet from billmaster b inner join Company c on c.CompanyId=b.CompanyId  where b.isactive=1 and b.BillType='SR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString("MM-dd-yyyy") + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString("MM-dd-yyyy") + "' and b.CompanyId=" + Master.companyId + " order by b.Bill_No", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                bill = 0;
                total = 0;
                vat = 0;
                net = 0;
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        LVDayBook.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[7].ToString());
                        bill++;
                        total = total + Convert.ToDouble(dt.Rows[i][5].ToString());
                        vat = vat + Convert.ToDouble(dt.Rows[i][6].ToString());
                        net = net + Convert.ToDouble(dt.Rows[i][7].ToString());
                    }
                }
                TxtInvoice.Text = bill.ToString();
                txtbillamt.Text = total.ToString("N2");
                txtvat.Text = vat.ToString("N2");
                txtnetamt.Text = net.ToString("N2");

                //DataTable dt4 = new DataTable();
                //dt4 = conn.getdataset("select CompanyName,Address,Phone,VATNo from Company where CompanyID='" + Master.companyId + "' and isActive=1");

                //ods.execute("delete from printing");

                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string qry = "INSERT INTO [Printing]([T1],[T2],[T3],[T4],[T5],[T6],[T7],[T8],[T9],[T10],[T11],[T12])VALUES";
                //    qry += "('" + dt.Rows[i][0].ToString() + "','" + dt.Rows[i][1].ToString() + "','" + dt.Rows[i][2].ToString() + "','" + dt.Rows[i][3].ToString() + "','" + dt.Rows[i][4].ToString() + "','" + dt.Rows[i][5].ToString() + "','" + dt.Rows[i][6].ToString() + "','" + dt.Rows[i][7].ToString() + "','" + dt4.Rows[0][0].ToString() + "','" + dt4.Rows[0][1].ToString() + "'," + dt4.Rows[0][2].ToString() + ",'" + dt4.Rows[0][3].ToString() + "')";
                //    //cmd3 = new SqlCommand(qry, con);
                //    //cmd3.ExecuteNonQuery();
                //    ods.execute(qry);


                //}
                

            }

            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally
            {
                con.Close();
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

        private void btngenrpt_Click(object sender, EventArgs e)
        {
            Print popup = new Print("Sale Return");
            popup.ShowDialog();
            popup.Dispose();
        }

        
        private void LVDayBook_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                this.Enabled = false;
                String str = LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[0].Text;

                SaleReturn bd = new SaleReturn(this);
                bd.updatemode(str, LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[0].Text, 1);
                bd.Show();
            }
            finally
            {
                this.Enabled = true;
            }
        }

        private void LVDayBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                String str = "select p.Product_Name,bp.Product_Qty,bp.Free,p.Product_Price,bp.Product_Per_rate,bp.Product_total_Amt from BillProductMaster bp inner join ProductMaster p on p.ProductID=bp.ProductID where Bill_No='" + LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[0].Text + "' and BillType='SR' and isactive=1";

                SaleReturn bd = new SaleReturn(this);
                bd.updatemode(str, LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[0].Text, 1);
                bd.Show();
            }
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            SaleReturn frm = new SaleReturn();
            frm.MdiParent = this.MdiParent;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
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
    }

}
