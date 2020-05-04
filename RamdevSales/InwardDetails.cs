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
    public partial class InwardDetails : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        private StockIn stockIn;

        public InwardDetails()
        {
            InitializeComponent();
        }

        public InwardDetails(StockIn stockIn)
        {
            // TODO: Complete member initialization
            this.stockIn = stockIn;
        }

        internal void Fromdatewise(string str, string p, int a)
        {
            InitializeComponent();
            lvproduct.Items.Clear();
            if (a == 1)
            {
                lvproduct.Columns.Add("Product Name", 350, HorizontalAlignment.Left);
                lvproduct.Columns.Add("Qty", 70, HorizontalAlignment.Center);
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
            }

            TxtBillNo.Text = p;
            callBillDetail();
        }

        private void callBillDetail()
        {
            SqlCommand cmd = new SqlCommand("select i.InvoiceDate,i.Billamt,c.CompanyName,c.SupplierDesc from InwardMstr i inner join CompanyMaster c on c.CompanyID=i.CompanyID where InvoiceNo='" + TxtBillNo.Text + "'", con);
            DataTable dt1 = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                
                TxtRundate.Text =Convert.ToDateTime(dt1.Rows[0][0].ToString()).ToString("dd-MM-yyyy");
                TxtBillTotal.Text = dt1.Rows[0][1].ToString();
                txtcompanyname.Text = dt1.Rows[0][2].ToString();
                txtsupplydesc.Text = dt1.Rows[0][3].ToString();
              

                //cmd = new SqlCommand("select clientname,on_bill_desc from clientmaster where companyid='" + dt1.Rows[0][2].ToString() + "'", con);
                //sda = new SqlDataAdapter(cmd);
                //DataTable dt2 = new DataTable();
                //sda.Fill(dt2);
                //txtcompanyname.Text = dt2.Rows[0][0].ToString();
                //txtsupplydesc.Text = dt2.Rows[0][1].ToString();
                
            }
        }
    }
}
