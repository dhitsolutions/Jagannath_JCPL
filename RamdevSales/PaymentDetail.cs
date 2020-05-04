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
using System.IO;


namespace RamdevSales
{
    public partial class PaymentDetail : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        static int Bill_No;
        
        public PaymentDetail()
        {
            InitializeComponent();

            LVClientpaymentDetail.Columns.Add("Bill No.", 100, HorizontalAlignment.Center);
            LVClientpaymentDetail.Columns.Add("Client Name", 250, HorizontalAlignment.Left);
            LVClientpaymentDetail.Columns.Add("Payment Status", 100, HorizontalAlignment.Center);
            LVClientpaymentDetail.Columns.Add("Payment Received Date", 150, HorizontalAlignment.Center);
            LVClientpaymentDetail.Columns.Add("Bill Amount", 150, HorizontalAlignment.Right);
            LVClientpaymentDetail.Columns.Add("Received Amount", 150, HorizontalAlignment.Right);
        }

        private void PaymentDetail_Load(object sender, EventArgs e)
        {
            listviewbind();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listviewbind()
        {
            try
            {
                LVClientpaymentDetail.Items.Clear();

                SqlCommand cmd = new SqlCommand("select bm.Bill_No,c.ClientName,pm.PaymentStatus,pm.PaymentDate,bm.Bill_Net_Amt,pm.ReceivedAmt from paymentmaster pm inner join BillMaster bm on bm.Bill_No=pm.Bill_No inner join ClientMaster c on c.ClientID=bm.ClientID order by bm.Bill_No desc", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                double bill = 0, rec=0;
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    LVClientpaymentDetail.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                    LVClientpaymentDetail.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                    LVClientpaymentDetail.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                    LVClientpaymentDetail.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                    LVClientpaymentDetail.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                    LVClientpaymentDetail.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                    bill = bill + Convert.ToDouble(dt.Rows[i].ItemArray[4].ToString());
                    try
                    {
                        rec = rec + Convert.ToDouble(dt.Rows[i].ItemArray[5].ToString());
                    }
                    catch
                    {
                        rec = rec + 0;
                    }
                }
                txttotbill.Text = bill.ToString("N2");
                txttotrec.Text = rec.ToString("N2");
            }
            catch
            {

            }
        }


        private void LVClientpaymentDetail_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (LVClientpaymentDetail.SelectedItems.Count > 0)
            {
                txtbillno.Text = LVClientpaymentDetail.Items[LVClientpaymentDetail.FocusedItem.Index].SubItems[0].Text;
                txtclientname.Text = LVClientpaymentDetail.Items[LVClientpaymentDetail.FocusedItem.Index].SubItems[1].Text;

                if (LVClientpaymentDetail.Items[LVClientpaymentDetail.FocusedItem.Index].SubItems[2].Text == "Pending")
                {
                    rdpending.Checked = true;
                }
                else
                {
                    rdpaid.Checked = true;
                }

                if (LVClientpaymentDetail.Items[LVClientpaymentDetail.FocusedItem.Index].SubItems[3].Text != "")
                {
                    dtpaymentdt.Text = Convert.ToDateTime(LVClientpaymentDetail.Items[LVClientpaymentDetail.FocusedItem.Index].SubItems[3].Text).ToString("dd-MM-yyyy");
                }

                if (LVClientpaymentDetail.Items[LVClientpaymentDetail.FocusedItem.Index].SubItems[4].Text != "")
                {
                    txtrecamt.Text = Convert.ToDouble(LVClientpaymentDetail.Items[LVClientpaymentDetail.FocusedItem.Index].SubItems[4].Text).ToString("N2");
                }
                else
                {
                    txtrecamt.Text = "0.00";
                }


                //con.Open();


                //SqlCommand cmd = new SqlCommand("select Bill_No  from BillMaster where Bill_No ='" + LVClientpaymentDetail.Items[LVClientpaymentDetail.FocusedItem.Index].SubItems[0].Text + "'", con);
                //Bill_No = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                //btnsubmit.Text = "Update";
                //con.Close();

            }
        }

        private void cball_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                LVClientpaymentDetail.Items.Clear();

                SqlCommand cmd = new SqlCommand("select bm.Bill_No,cm.ClientName,comp.CompanyName,pay.paymentstatus,pay.paymentdate from BillMaster bm inner join  ClientMaster cm on bm.Bill_No = cm.ClientID inner join  CompanyMaster comp on comp.CompanyID = bm.ClientID inner join paymentmaster pay on pay.PaymentID = bm.ClientID   ", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    LVClientpaymentDetail.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                    LVClientpaymentDetail.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                    LVClientpaymentDetail.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                    LVClientpaymentDetail.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                    LVClientpaymentDetail.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                }

            }
            catch
            {

            }
        }

        private void txtbillno_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtbillno.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("select bm.Bill_No,c.ClientName,pm.PaymentStatus,pm.PaymentDate,bm.Bill_Net_Amt,pm.ReceivedAmt from paymentmaster pm inner join BillMaster bm on bm.Bill_No=pm.Bill_No inner join ClientMaster c on c.ClientID=bm.ClientID where bm.bill_no ='" + txtbillno.Text + "'", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    txtclientname.Text = dt.Rows[0][2].ToString();
                    if(dt.Rows[0][2].ToString()=="")
                    txtrecamt.Text = dt.Rows[0][4].ToString();
                    listviewbind();
                }
                else
                {
                  
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
                txtbillno.Text = "";
                
            }
        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                    con.Open();
                    String payment="";
                    if (rdpaid.Checked == true)
                    {
                        payment = "Paid";
                    }
                    if (rdpending.Checked == true)
                    {
                        payment = "Pending";
                    }
                    SqlCommand cmd = new SqlCommand("update paymentmaster set paymentstatus='" + payment + "',paymentdate='"+Convert.ToDateTime(dtpaymentdt.Text).ToString("MM-dd-yyyy")+"' ,ReceivedAmt='"+Convert.ToDouble(txtrecamt.Text)+"' where Bill_No='" +txtbillno.Text + "'", con);
                    cmd.ExecuteNonQuery();
                    clear();
                    
                    listviewbind();
                    MessageBox.Show("Update Successfully...");

                
                //else
             
                   
                //    {
                //        string sql = "insert into ClientProductMargin values('" + SelectedValue + "','" + cmbproduct.SelectedValue + "','" + cmbproduct.SelectedValue + "','" + txtmargin.Text + "')";
                //        SqlCommand cmd = new SqlCommand(sql, con);
                //        cmd.ExecuteNonQuery();
                //        clear();
                //        //con.Close();
                //        listviewbind();
                //        MessageBox.Show("Insert Data Successfully...");
                //    }
                
                   
                
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
            txtclientname.Text = "";
            txtbillno.Text = "";
            txtrecamt.Text = "";
        }

        private void btnedit_Click(object sender, EventArgs e)
        {

        }

        private void LVClientpaymentDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (LVClientpaymentDetail.SelectedItems.Count > 0)
                {
                    txtbillno.Text = LVClientpaymentDetail.Items[LVClientpaymentDetail.FocusedItem.Index].SubItems[0].Text;
                    txtclientname.Text = LVClientpaymentDetail.Items[LVClientpaymentDetail.FocusedItem.Index].SubItems[1].Text;

                    if (LVClientpaymentDetail.Items[LVClientpaymentDetail.FocusedItem.Index].SubItems[2].Text == "Pending")
                    {
                        rdpending.Checked = true;
                    }
                    else
                    {
                        rdpaid.Checked = true;
                    }

                    if (LVClientpaymentDetail.Items[LVClientpaymentDetail.FocusedItem.Index].SubItems[3].Text != "")
                    {
                        dtpaymentdt.Text = Convert.ToDateTime(LVClientpaymentDetail.Items[LVClientpaymentDetail.FocusedItem.Index].SubItems[3].Text).ToString("dd-MM-yyyy");
                    }

                    if (LVClientpaymentDetail.Items[LVClientpaymentDetail.FocusedItem.Index].SubItems[4].Text != "")
                    {
                        txtrecamt.Text =Convert.ToDouble(LVClientpaymentDetail.Items[LVClientpaymentDetail.FocusedItem.Index].SubItems[4].Text).ToString("N2");
                    }
                    else
                    {
                        txtrecamt.Text = "0.00";
                    }

                }
            }
        }

        private void cmbstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LVClientpaymentDetail.Items.Clear();
                string qry="";
                if (cmbstatus.Text == "Select")
                {
                    qry="select bm.Bill_No,c.ClientName,pm.PaymentStatus,pm.PaymentDate,bm.Bill_Net_Amt,pm.ReceivedAmt from paymentmaster pm inner join BillMaster bm on bm.Bill_No=pm.Bill_No inner join ClientMaster c on c.ClientID=bm.ClientID order by bm.Bill_No desc";
                }
                else
                {
                    qry = "select bm.Bill_No,c.ClientName,pm.PaymentStatus,pm.PaymentDate,bm.Bill_Net_Amt,pm.ReceivedAmt from paymentmaster pm inner join BillMaster bm on bm.Bill_No=pm.Bill_No inner join ClientMaster c on c.ClientID=bm.ClientID where pm.PaymentStatus='" + cmbstatus.Text + "' order by bm.Bill_No desc";
                }

                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                double bill = 0, rec = 0;
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    LVClientpaymentDetail.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                    LVClientpaymentDetail.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                    LVClientpaymentDetail.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                    LVClientpaymentDetail.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                    LVClientpaymentDetail.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                    LVClientpaymentDetail.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                    bill = bill + Convert.ToDouble(dt.Rows[i].ItemArray[4].ToString());
                    try
                    {
                        rec = rec + Convert.ToDouble(dt.Rows[i].ItemArray[5].ToString());
                    }
                    catch
                    {
                        rec = rec + 0;
                    }
                }
                txttotbill.Text = bill.ToString("N2");
                txttotrec.Text = rec.ToString("N2");
            }
            catch
            {

            }
        }


    }
}
