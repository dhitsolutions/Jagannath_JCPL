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
    public partial class Inward : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        static int count=1;
        public Inward()
        {
            InitializeComponent();
        }

        private void Inward_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select CompanyId,companyname from Companymaster ", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            cmbcomp.ValueMember = "CompanyId";
            cmbcomp.DisplayMember = "companyname";
            cmbcomp.DataSource = dt;


            LVInwrd.Columns.Add("Sr No", 176, HorizontalAlignment.Left);
            LVInwrd.Columns.Add("Product Name", 424, HorizontalAlignment.Left);
            LVInwrd.Columns.Add("Qty", 127, HorizontalAlignment.Center);


        }

        private void cmbcomp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select p.ProductID, p.Product_Name from ProductMaster p where CompanyId='" + cmbcomp.SelectedValue + "' order by p.Product_Name", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                cmbproduct.ValueMember = "ProductID";
                cmbproduct.DisplayMember = "Product_Name";
                cmbproduct.DataSource = dt;
                cmbproduct.SelectedIndex = -1;

                cmd = new SqlCommand("select SupplierDesc from companymaster where companyId='" + cmbcomp.SelectedValue + "'", con);
                con.Open();
                txtcmptotdetail.Text = cmd.ExecuteScalar().ToString();

            }
            catch
            {
            }
            finally
            {
                con.Close();
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (cmbproduct.SelectedIndex > -1)
            {
                if (txtqty.Text != "")
                {
                    ListViewItem li;
                    li = LVInwrd.Items.Add(count.ToString());
                    li.SubItems.Add(cmbproduct.Text);
                    li.SubItems.Add(txtqty.Text);
                    count++;
                }
            }
            txtqty.Text = "";
            cmbproduct.SelectedIndex = -1;

            LVInwrd.Items[LVInwrd.Items.Count - 1].Selected = true;
           cmbproduct.Focus();

        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
            if (LVInwrd.Items.Count > 0)
            {
                if (txtbillamt.Text != "")
                {

                    try
                    {

                        con.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO [Billing].[dbo].[InwardMstr]([InvoiceDate],[InvoiceNo],[ChequeDate],[ChequeNo],[Billamt],[CompanyID])VALUES ('" + Convert.ToDateTime(dtinvdt.Text).ToString("MM-dd-yyyy") + "','" + txtinvno.Text + "','" + Convert.ToDateTime(dtchqdt.Text).ToString("MM-dd-yyyy") + "','" + txtchqno.Text + "','" + txtbillamt.Text + "','" + cmbcomp.SelectedValue + "')", con);
                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("select MAX(InwardID) from InwardMstr", con);
                        String inwrdid = cmd.ExecuteScalar().ToString();



                        for (int i = 0; i < LVInwrd.Items.Count; i++)
                        {
                            cmd = new SqlCommand("select ProductID from ProductMaster where Product_Name='" + LVInwrd.Items[i].SubItems[1].Text + "'", con);
                            String prodid = cmd.ExecuteScalar().ToString();

                            SqlCommand cmd1 = new SqlCommand("INSERT INTO [Billing].[dbo].[InwardProductMstr]([InwardID],[ProductID],[qty])VALUES ('" + inwrdid + "','" + prodid + "','" + LVInwrd.Items[i].SubItems[2].Text + "')", con);
                            cmd1.ExecuteNonQuery();
                            //total = total + Convert.ToDouble(LVFO.Items[i].SubItems[4].Text);
                            //Double multi = 0;
                            //multi = (Convert.ToDouble(LVFO.Items[i].SubItems[4].Text) * (Convert.ToDouble(LVFO.Items[i].SubItems[5].Text))) / 100;
                            //vat = vat + multi;

                        }
                        MessageBox.Show("Insert successfully");
                        clear();
                        count = 1;
                    }
                    catch
                    {
                    }
                    finally
                    {
                        con.Close();
                    }


                }
            }
        }

        private void clear()
        {
            txtbillamt.Text = "";
            txtchqno.Text = "";
            txtcmptotdetail.Text = "";
            txtinvno.Text = "";
            txtqty.Text = "";
            txtbarcode.Text = "";
            cmbcomp.SelectedIndex = -1;
            cmbproduct.SelectedIndex = -1;
            LVInwrd.Items.Clear();
        }

        private void LVInwrd_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (LVInwrd.SelectedItems.Count > 0)
            {
                txtqty.Text = LVInwrd.Items[LVInwrd.FocusedItem.Index].SubItems[2].Text;
                cmbproduct.Text = LVInwrd.Items[LVInwrd.FocusedItem.Index].SubItems[1].Text;
                LVInwrd.Items[LVInwrd.FocusedItem.Index].Remove();
            }
        }

        private void LVInwrd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (LVInwrd.SelectedItems.Count > 0)
                {
                    txtqty.Text = LVInwrd.Items[LVInwrd.FocusedItem.Index].SubItems[2].Text;
                    cmbproduct.Text = LVInwrd.Items[LVInwrd.FocusedItem.Index].SubItems[1].Text;
                    LVInwrd.Items[LVInwrd.FocusedItem.Index].Remove();
                }
            }
        }

        private void cmbproduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(cmbproduct.SelectedIndex>0)
                txtbarcode.Text = cmbproduct.SelectedValue.ToString();
            }
            catch
            {
            }
            finally
            {
                con.Close();
            }

        }

        private void txtbarcode_Validated(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select Product_name from productMaster where ProductID='" + txtbarcode.Text + "'", con);
                cmbproduct.Text = cmd.ExecuteScalar().ToString();
            }
            catch
            {
            }
            finally
            {
                con.Close();
            }
        }
    }
}
