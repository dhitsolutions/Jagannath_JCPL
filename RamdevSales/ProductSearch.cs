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
    public partial class ProductSearch : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        static int prodid;
       public String retvalue = "";
       static string clientid;
       static string qry1;

        private Form1 form1;
       
        public ProductSearch()
        {
            InitializeComponent();
        }

        public ProductSearch(Form1 form1)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.form1 = form1;
        }

        public void Data_From_Form1(string data2,string client)
        {
            retvalue = data2;
            clientid = client;
        }

        public void Data_From_Form1(string p, string p_2, string qry)
        {
            retvalue = p;
            clientid = p_2;
            qry1 = qry;

        }
        private void ProductSearch_Load(object sender, EventArgs e)
        {
            LVstudSearch.Columns.Add("ProductID", 135, HorizontalAlignment.Left);
            LVstudSearch.Columns.Add("Barcode", 100, HorizontalAlignment.Left);
           
            LVstudSearch.Columns.Add("Product Name", 295, HorizontalAlignment.Left);
            LVstudSearch.Columns.Add("Product_Price", 135, HorizontalAlignment.Left);
            LVstudSearch.Columns.Add("Product_Vat", 100, HorizontalAlignment.Left);
            LVstudSearch.Columns.Add("Margin", 135, HorizontalAlignment.Left);

            productlist();

            Check_Right_CaseSale();

        }

        private void Check_Right_CaseSale()
        {
           
        }

        private void productlist()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(qry1, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    LVstudSearch.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                    LVstudSearch.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                    LVstudSearch.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                    LVstudSearch.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                    LVstudSearch.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                    LVstudSearch.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                  
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally
            {
                con.Close();

            }
        }

        private void ProductSearchFunction()
        {
            try
            {
                DataTable ds = new DataTable();
                Int32 inc = 0;
                SqlDataAdapter da;

                Int32 prodcode = 0;
                if (!String.IsNullOrEmpty(prodid.ToString()))
                {
                    prodcode = prodid;
                }

                String sqlconn = "Select ProductID,Product_Name  from ProductMaster where productid ='" + prodcode + "'";
                da = new SqlDataAdapter(sqlconn, con);
                da.Fill(ds);

               
               retvalue = ds.Rows[inc][0].ToString();

            }
            catch
            {

            }
        }

        private void LVstudSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LVstudSearch.SelectedItems.Count > 0)
            {

                form1.Data_From_Form2(LVstudSearch.SelectedItems[0].Text);
                //prodid = Convert.ToInt32(result.TxtProdcode.Text);
            }
        }

        private void LVstudSearch_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ProductSearchFunction();
            this.Hide();
        }

        private void LVstudSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProductSearchFunction();

                this.Close();
            }
            else if (e.KeyCode == Keys.Escape)
            {
               
                this.Close();
            }
        }

        private void TxtProdcodeSearch_TextChanged(object sender, EventArgs e)
        {
            ProductCodeSearch();

            if (TxtProdcodeSearch.Text == "")
                productlist();

        }

        private void ProductCodeSearch()
        {
            String studentsearch = String.Empty;

            LVstudSearch.Items.Clear();

            if (!String.IsNullOrEmpty(TxtProdcodeSearch.Text))
            {
                studentsearch = TxtProdcodeSearch.Text;

            }
            String myqry = "select * from ProductMaster where name like '%" + prodid + "%' order by id ASC";
            SqlCommand cmd = new SqlCommand(myqry, con);
            SqlDataAdapter sda = new SqlDataAdapter();
            DataTable dt = new DataTable();
            sda.Fill(dt);

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                LVstudSearch.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                LVstudSearch.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
               

            }
        }










      
    }
}
