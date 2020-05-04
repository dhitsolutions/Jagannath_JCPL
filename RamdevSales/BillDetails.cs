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
    public partial class BillDetails : Form
    {
        private DateWiseReport dateWiseReport;
        private DateWisePurchaseReport dateWisePurchaseReport;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        public BillDetails()
        {
            InitializeComponent();
        }

        public BillDetails(DateWiseReport dateWiseReport)
        {
            // TODO: Complete member initialization
            this.dateWiseReport = dateWiseReport;
        }

        public BillDetails(DateWisePurchaseReport dateWisePurchaseReport)
        {
            // TODO: Complete member initialization
            this.dateWisePurchaseReport = dateWisePurchaseReport;
        }

        private void BillDetails_Load(object sender, EventArgs e)
        {

        }

        internal void Fromdatewise(string str, string p, int a)
        {
            InitializeComponent();
            lvproduct.Items.Clear();
            if (a == 1)
            {
                lvproduct.Columns.Add("Product Name", 190, HorizontalAlignment.Left);
                lvproduct.Columns.Add("Qty", 70, HorizontalAlignment.Center);
                lvproduct.Columns.Add("Free", 70, HorizontalAlignment.Center);
                lvproduct.Columns.Add("Unit Price", 105, HorizontalAlignment.Right);
                lvproduct.Columns.Add("rate", 100, HorizontalAlignment.Right);
                lvproduct.Columns.Add("Total", 135, HorizontalAlignment.Right);
                a++;
            }
            SqlCommand cmd = new SqlCommand(str, con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                lvproduct.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                lvproduct.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                lvproduct.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                lvproduct.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                lvproduct.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                lvproduct.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());

            }

            TxtBillNo.Text = p;
            callBillDetail();

        }

        public void callBillDetail()
        {
           
            SqlCommand cmd = new SqlCommand("select * from BillMaster where bill_no='" + TxtBillNo.Text + "'", con);
            DataTable dt1 = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                TxtRundate.Text = Convert.ToDateTime(dt1.Rows[0][1].ToString()).ToString("dd-MM-yyyy"); 
                txtpono.Text = dt1.Rows[0][3].ToString();
                TxtBillTotal.Text = dt1.Rows[0][4].ToString();
                txtvatamt.Text = dt1.Rows[0][5].ToString();
                txtnetamt.Text = dt1.Rows[0][6].ToString();

                cmd = new SqlCommand("select clientname,on_bill_desc from clientmaster where clientid='" + dt1.Rows[0][2].ToString() + "'", con);
                sda = new SqlDataAdapter(cmd);
                DataTable dt2 = new DataTable();
                sda.Fill(dt2);
                txtcustnm.Text = dt2.Rows[0][0].ToString();
                txtbilldesc.Text = dt2.Rows[0][1].ToString();


            }
        }

        private void TxtBillNo_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F2)
            //{
            //    TxtBillNo.ReadOnly = false;
            //}
        }

        private void TxtBillNo_Validated(object sender, EventArgs e)
        {
          
        }

        private void TxtBillNo_Validating(object sender, CancelEventArgs e)
        {
            String str = "select p.Product_Name,bp.Product_Qty,bp.Free,p.Product_Price,bp.Product_Per_rate,bp.Product_total_Amt from BillProductMaster bp inner join ProductMaster p on p.ProductID=bp.ProductID where Bill_No='" + TxtBillNo.Text + "'";
            Fromdatewise(str, TxtBillNo.Text, 2);
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from PaymentMaster where Bill_No=" + TxtBillNo.Text + "", con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("delete from BillProductMaster where Bill_No=" + TxtBillNo.Text + "", con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("delete from BillMaster where Bill_No=" + TxtBillNo.Text + "", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("delete successfully");
                this.Close();
                dateWiseReport.bindgrid();
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
    }
}
