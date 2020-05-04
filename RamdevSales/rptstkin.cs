using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RamdevSales
{
    public partial class rptstkin : Form
    {
        SqlConnection con = new SqlConnection("Data Source=" + Environment.MachineName + ";Initial Catalog=Billing;User ID=sa;Password=root");
        SqlCommand cmd;
        SqlDataAdapter sda;
        static Int32 bill;
        static double total, vat, net;
        public rptstkin()
        {
            InitializeComponent();
        }

        private void rptstkin_Load(object sender, EventArgs e)
        {
            LVDayBook.Columns.Add("InvoiceNo", 150, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("InvoiceDate", 100, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("CompanyName", 200, HorizontalAlignment.Left);
            LVDayBook.Columns.Add("Billamt", 150, HorizontalAlignment.Right);
            LVDayBook.Columns.Add("ChequeNo", 100, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("ChequeDate", 100, HorizontalAlignment.Center);
            binddrop();
            String qry = "select i.InvoiceNo,i.InvoiceDate,c.SupplierDesc,i.Billamt,i.ChequeNo,i.ChequeDate from InwardMstr i inner join CompanyMaster c on c.CompanyID=i.CompanyID";
            ListBind(qry);
         
           
        }

        private void binddrop()
        {
            SqlCommand cmd = new SqlCommand("select CompanyId,companyname from Companymaster ", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            cmbcomp.ValueMember = "CompanyId";
            cmbcomp.DisplayMember = "companyname";
            cmbcomp.DataSource = dt;
            cmbcomp.SelectedIndex = -1;
        }

        private void ListBind(String qry)
        {
            cmd = new SqlCommand(qry, con);
            sda =new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            LVDayBook.Items.Clear();
            //bill = 0;
            total = 0;
            //vat = 0;
            //net = 0;
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
                    //bill++;
                    total = total + Convert.ToDouble(dt.Rows[i][3].ToString());
                }
            }
            else
            {
                //MessageBox.Show("Empty Stack");
            }
            //TxtInvoice.Text = bill.ToString();
            txttotbill.Text = total.ToString("N2");
        }

        private void cmbcomp_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            try
            {
                String qry = "select i.InvoiceNo,i.InvoiceDate,c.SupplierDesc,i.Billamt,i.ChequeNo,i.ChequeDate from InwardMstr i inner join CompanyMaster c on c.CompanyID=i.CompanyID where i.InvoiceDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString("MM-dd-yyyy") + "' and InvoiceDate<='" + Convert.ToDateTime(DTPTo.Text).ToString("MM-dd-yyyy") + "' and i.CompanyID='" + cmbcomp.SelectedValue + "'";
                ListBind(qry);
            }
            catch
            {
                String qry = "select i.InvoiceNo,i.InvoiceDate,c.SupplierDesc,i.Billamt,i.ChequeNo,i.ChequeDate from InwardMstr i inner join CompanyMaster c on c.CompanyID=i.CompanyID where i.InvoiceDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString("dd-MMM-yyyy") + "' and InvoiceDate<='" + Convert.ToDateTime(DTPTo.Text).ToString("dd-MMM-yyyy") + "'";
                ListBind(qry);
            }
        }

        private void LVDayBook_MouseClick(object sender, MouseEventArgs e)
        {
            String qry = "select p.Product_Name as Desription,sum(ip.qty) as Qty from InwardProductMstr ip inner join ProductMaster p on p.ProductID=ip.ProductID inner join InwardMstr i on i.InwardID=ip.InwardID where i.InvoiceNo='" + LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[0].Text + "' group by ip.ProductID,p.Product_Name,i.InvoiceNo,i.InvoiceDate order by p.Product_Name";
            bindgrid(qry);

        }

        private void bindgrid(String qry)
        {
            cmd = new SqlCommand(qry, con);
            sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            grdshow.DataSource = dt;
            grdshow.Columns[0].Width = 250;
            grdshow.Columns[1].Width = 100;
            bill = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bill = bill + Convert.ToInt32(dt.Rows[i][1].ToString());
            }
            txttotqty.Text = bill.ToString("N2");
        }

        private void LVDayBook_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            String qry = "select p.Product_Name as Desription,sum(ip.qty) as Qty from InwardProductMstr ip inner join ProductMaster p on p.ProductID=ip.ProductID inner join InwardMstr i on i.InwardID=ip.InwardID where i.InvoiceNo='" + e.Item.SubItems[0].Text + "' group by ip.ProductID,p.Product_Name,i.InvoiceNo,i.InvoiceDate order by p.Product_Name";
            bindgrid(qry);
        }

       
    }
}
