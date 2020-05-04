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
    public partial class ProductMaster : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        static int clientID;
        static String productID;
        static int CompanyID;

        public ProductMaster()
        {
            InitializeComponent();

            LVclientproductadd.Columns.Add("Company Name", 100, HorizontalAlignment.Left);
            LVclientproductadd.Columns.Add("Product Name", 250, HorizontalAlignment.Left);
            LVclientproductadd.Columns.Add("Barcord Number", 100, HorizontalAlignment.Center);
            LVclientproductadd.Columns.Add("M.R.P. Price", 150, HorizontalAlignment.Right);
            LVclientproductadd.Columns.Add("Vat Tax", 150, HorizontalAlignment.Center);

        }

        private void ProductMaster_Load(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select CompanyID,CompanyName from CompanyMaster", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                listviewbind();

                cmbcompany.ValueMember = "CompanyID";
                cmbcompany.DisplayMember = "CompanyName";
                cmbcompany.DataSource = dt;
                cmbcompany.SelectedIndex = -1;
                txtmrp.Text = "";
                txtvattax.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
        }

        private void listviewbind()
        {
            try
            {
                LVclientproductadd.Items.Clear();
                SqlCommand cmd = new SqlCommand("select cm.companyname,pm.Product_Name,pm.Product_barcode,pm.Product_Price,pm.Product_Vat from ProductMaster pm inner join CompanyMaster cm on cm.CompanyID = pm.CompanyID where cm.CompanyID='" + cmbcompany.SelectedValue + "' ", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    LVclientproductadd.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                    LVclientproductadd.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                    LVclientproductadd.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                    LVclientproductadd.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                    LVclientproductadd.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                }
            }
            catch
            {
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                if (btnsave.Text == "Update")
                {
                    string sql = "update Productmaster set Product_Name='" + txtprodname.Text + "',	Product_barcode='" + txtbarnum.Text + "',Product_Price='" + txtmrp.Text + "',Product_Vat='" + txtvattax.Text + "',productID='" + txtbarnum.Text + "' where productID ='" + productID + "' ";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    clear();
                    listviewbind();
                    btnsave.Text = "Save";
                    MessageBox.Show("Update Data sussecessfully...");

                }
                else
                {
                    if (cmbcompany.SelectedIndex > -1)
                    {
                        string sql = "insert into ProductMaster values('" + txtbarnum.Text + "','" + cmbcompany.SelectedValue + "','" + txtbarnum.Text + "','" + txtprodname.Text + "','" + txtmrp.Text + "','" + txtvattax.Text + "') ";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.ExecuteNonQuery();
                        clear();
                        listviewbind();
                        MessageBox.Show("Insert Data Susscessfully....");
                    }
                    else
                    {
                        MessageBox.Show("Please Fill Data Properly.");
                    }
                }
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

        private void clear()
        {
            txtbarnum.Text = "";
            txtmrp.Text = "";
            txtprodname.Text = "";
            txtvattax.Text = "";

        }


        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            try
            {
                if (LVclientproductadd.FocusedItem.Selected == true)
                {
                    con.Open();
                    cmbcompany.Text = LVclientproductadd.Items[LVclientproductadd.FocusedItem.Index].SubItems[0].Text;
                    txtprodname.Text = LVclientproductadd.Items[LVclientproductadd.FocusedItem.Index].SubItems[1].Text;
                    txtbarnum.Text = LVclientproductadd.Items[LVclientproductadd.FocusedItem.Index].SubItems[2].Text;
                    txtmrp.Text = LVclientproductadd.Items[LVclientproductadd.FocusedItem.Index].SubItems[3].Text;
                    txtvattax.Text = LVclientproductadd.Items[LVclientproductadd.FocusedItem.Index].SubItems[4].Text;

                    SqlCommand cmd = new SqlCommand("select ProductID from productmaster where Product_Name='" + LVclientproductadd.Items[LVclientproductadd.FocusedItem.Index].SubItems[1].Text + "'", con);
                    productID = cmd.ExecuteScalar().ToString();

                    btnsave.Text = "Update";
                    listviewbind();
                    con.Close();
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Please Select Row in Listview.....", ex.Message);
            }
        }

        private void LVclientproductadd_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (LVclientproductadd.SelectedItems.Count > 0)
                {
                    cmbcompany.Text = LVclientproductadd.Items[LVclientproductadd.FocusedItem.Index].SubItems[0].Text;
                    txtprodname.Text = LVclientproductadd.Items[LVclientproductadd.FocusedItem.Index].SubItems[1].Text;
                    txtbarnum.Text = LVclientproductadd.Items[LVclientproductadd.FocusedItem.Index].SubItems[2].Text;
                    txtmrp.Text = LVclientproductadd.Items[LVclientproductadd.FocusedItem.Index].SubItems[3].Text;
                    txtvattax.Text = LVclientproductadd.Items[LVclientproductadd.FocusedItem.Index].SubItems[4].Text;
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select ProductID from productmaster where Product_Name='" + LVclientproductadd.Items[LVclientproductadd.FocusedItem.Index].SubItems[1].Text + "'", con);
                    productID = cmd.ExecuteScalar().ToString();

                    btnsave.Text = "Update";
                    listviewbind();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Close();
            CompanyMaster compmast = new CompanyMaster();
            compmast.StartPosition = FormStartPosition.CenterScreen;
            compmast.Show();
        }

        private void cmbcompany_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                LVclientproductadd.Items.Clear();
                SqlCommand cmd = new SqlCommand("select cm.companyname,pm.Product_Name,pm.Product_barcode,pm.Product_Price,pm.Product_Vat from ProductMaster pm inner join CompanyMaster cm on cm.CompanyID = pm.CompanyID where cm.CompanyID='" + cmbcompany.SelectedValue + "' order by pm.Product_Name", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    LVclientproductadd.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                    LVclientproductadd.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                    LVclientproductadd.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                    LVclientproductadd.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                    LVclientproductadd.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                }
                clear();
            }
            catch
            {
            }

        }

        private void txtbarnum_Validated(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from Productmaster where productID='" + txtbarnum.Text + "'", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Product already available");
                txtbarnum.Focus();
            }
            else
            {

            }
        }


    }
}
