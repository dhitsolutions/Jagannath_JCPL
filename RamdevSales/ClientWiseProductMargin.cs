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
    public partial class ClientWiseProductMargin : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        static int clientID;
       

        private void ClientWiseProductMargin_Load(object sender, EventArgs e)
        {
            try
            {


                SqlCommand cmd = new SqlCommand("select ClientID, ClientName from Clientmaster", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                cmbclientname.ValueMember = "ClientID";
                cmbclientname.DisplayMember = "ClientName";
                cmbclientname.DataSource = dt;
                cmbclientname.SelectedIndex = -1;
                txtmargin.Text = "";
                listviewbind();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally
            {

            }
        }

        public ClientWiseProductMargin()
        {
            InitializeComponent();

            LVclientproduct.Columns.Add("Client Name", 250, HorizontalAlignment.Center);
            LVclientproduct.Columns.Add("Company Name", 200, HorizontalAlignment.Center);
            LVclientproduct.Columns.Add("Product", 150, HorizontalAlignment.Left);
            LVclientproduct.Columns.Add("Margin per Pruduct", 150, HorizontalAlignment.Left);
        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                if (btnsubmit.Text == "Update")
                {
                    SqlCommand cmd = new SqlCommand("update ClientProductMargin set MarginInPer='" + txtmargin.Text + "' from ClientProductMargin  where  ClientID='" + clientID + "' and productid=(select productid from productmaster where Product_Name='" + cmbproduct.Text + "')", con);
                    cmd.ExecuteNonQuery();
                    clear();
                    btnsubmit.Text = "Save";
                    listviewbind();
                    MessageBox.Show("Update Successfully...");

                }
                else
                {
                    if (cmbclientname.SelectedIndex > -1 && cmbcomp.SelectedIndex > -1 && cmbproduct.SelectedIndex > -1)
                    {
                        SqlCommand cm = new SqlCommand("select * from ClientProductMargin where Productid='" + cmbproduct.SelectedValue + "' and clientID='" + cmbclientname.SelectedValue + "'", con);
                        SqlDataAdapter sd = new SqlDataAdapter(cm);
                        DataTable dt = new DataTable();
                        sd.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            MessageBox.Show("Product already Available");
                            cmbproduct.Focus();
                        }
                        else
                        {
                            string sql = "insert into ClientProductMargin values('" + cmbclientname.SelectedValue + "','" + cmbproduct.SelectedValue + "','" + txtmargin.Text + "')";
                            SqlCommand cmd = new SqlCommand(sql, con);
                            cmd.ExecuteNonQuery();
                            clear();
                            //con.Close();
                            listviewbind();
                            productbind();
                            MessageBox.Show("Insert Data Successfully...");
                            cmbproduct.Focus();
                        }

                    }

                    else
                    {
                        MessageBox.Show("Please Fill Data Properly...");
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

        private void listviewbind()
        {
            try
            {
                LVclientproduct.Items.Clear();

                SqlCommand cmd = new SqlCommand("select c.ClientName,cm.CompanyName, pm.Product_Name,cp.MarginInPer from ClientMaster c inner join ClientProductMargin cp on cp.ClientID=c.ClientID inner join ProductMaster pm on pm.ProductID=cp.ProductID inner join CompanyMaster cm on cm.CompanyID=pm.CompanyID where cp.ClientID='" + cmbclientname.SelectedValue + "' order by c.ClientName ", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    LVclientproduct.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                    LVclientproduct.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                    LVclientproduct.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                    LVclientproduct.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                }

            }
            catch
            {

            }

        }

        private void clear()
        {
            //cmbclientname.SelectedIndex = -1;
            //cmbcomp.SelectedIndex = -1;
            cmbproduct.SelectedIndex = -1;
            txtmargin.Text = "";

        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            try
            {

                if (LVclientproduct.FocusedItem.Selected == true)
                {
                    con.Open();

                    cmbclientname.Text = LVclientproduct.Items[LVclientproduct.FocusedItem.Index].SubItems[0].Text;
                    cmbcomp.Text = LVclientproduct.Items[LVclientproduct.FocusedItem.Index].SubItems[1].Text;
                    cmbproduct.Text = LVclientproduct.Items[LVclientproduct.FocusedItem.Index].SubItems[2].Text;
                    txtmargin.Text = LVclientproduct.Items[LVclientproduct.FocusedItem.Index].SubItems[3].Text;
                    //TxtPrice.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[4].Text;
                    //TxtTotalAmount.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[5].Text;
                    //TxtVATCode.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[6].Text;
                    //Double total = 0;

                    SqlCommand cmd = new SqlCommand("select ClientID from Clientmaster where ClientName='" + LVclientproduct.Items[LVclientproduct.FocusedItem.Index].SubItems[0].Text + "'", con);
                    clientID = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    btnsubmit.Text = "Update";

                    // SqlCommand cmd = new SqlCommand("update ClientMaster set ClientName ='" + txtclientname.Text + "', On_Bill_desc='" + txtbilldesc.Text + "',Contact_No='" + txtcontact.Text + "',Address='" + txtaddress.Text + "' where ClientName='" + txtclientname.Text + "' ", con);
                    //cmd.ExecuteNonQuery();
                    //MessageBox.Show("Update Successfully");
                    listviewbind();
                    // clear();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Select Row in Listview.....",ex.Message);
            }

        }

        private void cmbclientname_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {

                SqlCommand cmd = new SqlCommand("select CompanyId,companyname from Companymaster ", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                cmbcomp.ValueMember = "CompanyId";
                cmbcomp.DisplayMember = "companyname";
                cmbcomp.DataSource = dt;
                cmbcomp.SelectedItem = -1;


                LVclientproduct.Items.Clear();

                cmd = new SqlCommand("select c.ClientName,cm.CompanyName, pm.Product_Name,cp.MarginInPer from ClientMaster c inner join ClientProductMargin cp on cp.ClientID=c.ClientID inner join ProductMaster pm on pm.ProductID=cp.ProductID inner join CompanyMaster cm on cm.CompanyID=pm.CompanyID where c.ClientName='"+cmbclientname.Text+"'  order by c.ClientName ", con);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    LVclientproduct.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                    LVclientproduct.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                    LVclientproduct.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                    LVclientproduct.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
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

        private void cmbcomp_SelectedIndexChanged(object sender, EventArgs e)
        {
            productbind();
        }

        private void productbind()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select p.ProductID, p.Product_Name from ProductMaster p where  p.ProductID not in(select ProductID from ClientProductMargin where ClientID='" + cmbclientname.SelectedValue + "' ) and CompanyID='" + cmbcomp.SelectedValue + "'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                cmbproduct.ValueMember = "ProductID";
                cmbproduct.DisplayMember = "Product_Name";
                cmbproduct.DataSource = dt;
                cmbproduct.SelectedIndex = -1;
                txtmargin.Text = "";

                LVclientproduct.Items.Clear();

                cmd = new SqlCommand("select c.ClientName,cm.CompanyName, pm.Product_Name,cp.MarginInPer from ClientMaster c inner join ClientProductMargin cp on cp.ClientID=c.ClientID inner join ProductMaster pm on pm.ProductID=cp.ProductID inner join CompanyMaster cm on cm.CompanyID=pm.CompanyID where c.ClientName='" + cmbclientname.Text + "' and cm.CompanyName ='" + cmbcomp.Text + "'  order by c.ClientName ", con);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    LVclientproduct.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                    LVclientproduct.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                    LVclientproduct.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                    LVclientproduct.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
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

        private void LVclientproduct_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (LVclientproduct.SelectedItems.Count > 0)
            {
                cmbclientname.Text = LVclientproduct.Items[LVclientproduct.FocusedItem.Index].SubItems[0].Text;
                cmbcomp.Text = LVclientproduct.Items[LVclientproduct.FocusedItem.Index].SubItems[1].Text;
                cmbproduct.Text = LVclientproduct.Items[LVclientproduct.FocusedItem.Index].SubItems[2].Text;
                txtmargin.Text = LVclientproduct.Items[LVclientproduct.FocusedItem.Index].SubItems[3].Text;


                con.Open();

                SqlCommand cmd = new SqlCommand("select clientid from clientmaster where clientname ='" + LVclientproduct.Items[LVclientproduct.FocusedItem.Index].SubItems[0].Text + "'", con);
                clientID = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                btnsubmit.Text = "Update";
                con.Close();


            }

        }

        private void cmbproduct_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}
