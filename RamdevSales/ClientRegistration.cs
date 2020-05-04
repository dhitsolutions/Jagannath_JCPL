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
using ClosedXML.Excel;
using System.Globalization;


namespace RamdevSales
{
    public partial class ClientRegistration : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        public static string iid = "";
        private TabControl tabControl;
        private Master master;
        Connection conn = new Connection();

        public ClientRegistration()
        {
            InitializeComponent();

            LVclient.Columns.Add("ClientID", 0, HorizontalAlignment.Center);
            LVclient.Columns.Add("Account Name", 250, HorizontalAlignment.Center);
            LVclient.Columns.Add("Print Name", 100, HorizontalAlignment.Center);
            LVclient.Columns.Add("Group", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("OP.Bal", 70, HorizontalAlignment.Left);
            LVclient.Columns.Add("Dr/Cr", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("Address", 250, HorizontalAlignment.Left);
            LVclient.Columns.Add("City", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("State", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("Phone", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("Mobile", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("E-mail", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("CST No", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("VAT No", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("GST No", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("AdharCard No", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("AccountID", 100, HorizontalAlignment.Left);

        }

        public ClientRegistration(TabControl tabControl)
        {
            InitializeComponent();

            LVclient.Columns.Add("ClientID", 0, HorizontalAlignment.Center);
            LVclient.Columns.Add("Account Name", 250, HorizontalAlignment.Center);
            LVclient.Columns.Add("Print Name", 100, HorizontalAlignment.Center);
            LVclient.Columns.Add("Group", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("OP.Bal", 70, HorizontalAlignment.Left);
            LVclient.Columns.Add("Dr/Cr", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("Address", 250, HorizontalAlignment.Left);
            LVclient.Columns.Add("City", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("State", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("Phone", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("Mobile", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("E-mail", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("CST No", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("VAT No", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("GST No", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("AdharCard No", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("AccountID", 100, HorizontalAlignment.Left);
            this.tabControl = tabControl;
        }

        public ClientRegistration(Master master, TabControl tabControl)
        {
            InitializeComponent();

            LVclient.Columns.Add("ClientID", 0, HorizontalAlignment.Center);
            LVclient.Columns.Add("Account Name", 250, HorizontalAlignment.Center);
            LVclient.Columns.Add("Print Name", 100, HorizontalAlignment.Center);
            LVclient.Columns.Add("Group", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("OP.Bal", 70, HorizontalAlignment.Left);
            LVclient.Columns.Add("Dr/Cr", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("Address", 250, HorizontalAlignment.Left);
            LVclient.Columns.Add("City", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("State", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("Phone", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("Mobile", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("E-mail", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("CST No", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("VAT No", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("GST No", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("AdharCard No", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("AccountID", 100, HorizontalAlignment.Left);
            // TODO: Complete member initialization
            this.master = master;
            this.tabControl = tabControl;
        }
        public void binddrop()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='ClientMaster' and (column_name like '%AccountName%' or column_name like '%PrintName%' or column_name like '%Groupname%' or column_name like '%Address%' or column_name like '%City%' or column_name like '%State%')", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                // dt = sql.getdataset("select * from psm");
                DataRow dr;
                dr = dt.NewRow();
                dt.Rows.InsertAt(dr, 0);
                dr["column_name"] = "--Select Column Name--";
                if (dt.Rows.Count != 0)
                {
                    // cmbname.DataSource = dt.DefaultView;
                    // cmbname.ValueMember = "sp_id";
                    // cmbname.DisplayMember = "p_name";
                    // btnclr.Enabled = true;
                    // cmbname.SelectedIndex = -1;
                    txtsearch.DataSource = dt;
                    txtsearch.DisplayMember = "column_name";
                    txtsearch.ValueMember = "ClientID";
                }

            }
            catch
            {
            }
        }
        public void listviewbind()
        {
            try
            {
                LVclient.Items.Clear();

                SqlCommand cmd = new SqlCommand("select ClientID,AccountName,PrintName,GroupName,Opbal,Dr_cr,Address,City,State,Phone,Mobile,Email,Cstno,Vatno,Gstno,AdharNo from ClientMaster where isactive=1", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    LVclient.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                    LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                    LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                    LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                    LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                    LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                    LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                    LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[7].ToString());
                    LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[8].ToString());
                    LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[9].ToString());
                    LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[10].ToString());
                    LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[11].ToString());
                    LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[12].ToString());
                    LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[13].ToString());
                    LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[14].ToString());
                    LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[15].ToString());
                    // LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                    //    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                    //    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                    //    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[7].ToString());
                }
            }
            catch (Exception ex)
            {
                //  MessageBox.Show("Error:" + ex.Message);
            }
            finally
            {

            }
        }
        DataTable userrights = new DataTable();
        private void ClientRegistration_Load(object sender, EventArgs e)
        {
            con.Open();
            userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[11]["a"].ToString() == "False")
                {
                    btnnew.Enabled = false;
                }
                if (userrights.Rows[11]["p"].ToString() == "False")
                {
                    btnprint.Enabled = false;
                }
            }
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            listviewbind();
            binddrop();
            con.Close();
            this.ActiveControl = btnnew;

            // txtsearch.Focus();
        }

        private void LVDayBook_DoubleClick(object sender, EventArgs e)
        {

            if (LVclient.SelectedItems.Count > 0)
            {
                pnllist.Visible = false;
                //txtaccname.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[0].Text;
                //txtprintname.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[1].Text;
                //txtgrop.SelectedItem = LVclient.Items[LVclient.FocusedItem.Index].SubItems[2].Text;
                //txtopbal.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[3].Text;
                //txtcrdr.SelectedItem = LVclient.Items[LVclient.FocusedItem.Index].SubItems[4].Text;
                //txtaddress.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[5].Text;
                //txtcity.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[6].Text;
                //txtstate.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[7].Text;
                //txtphone.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[8].Text;
                //txtmobile.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[9].Text;
                //txtemail.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[10].Text;
                //txtcst.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[11].Text;
                //txtvat.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[12].Text;
                ////TxtTotalAmount.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[5].Text;
                ////TxtVATCode.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[6].Text;
                ////Double total = 0;
                //con.Open();
                //SqlCommand cmd = new SqlCommand("select clientID from clientmaster where AccountName='" + LVclient.Items[LVclient.FocusedItem.Index].SubItems[0].Text + "'", con);
                //clientID =Convert.ToInt32(cmd.ExecuteScalar().ToString());
                //btnsave.Text = "Update";
                //con.Close();
                //TxtBillTotal.Text = total.ToString();
                //LVFO.Items[LVFO.FocusedItem.Index].Remove();
                //totalcalculation();
            }

        }

        private void btnedit_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (LVclient.FocusedItem.Selected == true)
                {
                    con.Open();
                    //panel1.Visible = true;
                    //pnllist.Visible = false;
                    //txtaccname.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[0].Text;
                    //txtprintname.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[1].Text;
                    //txtgrop.SelectedItem = LVclient.Items[LVclient.FocusedItem.Index].SubItems[2].Text;
                    //txtopbal.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[3].Text;
                    //txtcrdr.SelectedItem = LVclient.Items[LVclient.FocusedItem.Index].SubItems[4].Text;
                    //txtaddress.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[5].Text;
                    //txtcity.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[6].Text;
                    //txtstate.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[7].Text;
                    //txtphone.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[8].Text;
                    //txtmobile.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[9].Text;
                    //txtemail.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[10].Text;
                    //txtcst.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[11].Text;
                    //txtvat.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[12].Text;
                    ////TxtTotalAmount.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[5].Text;
                    ////TxtVATCode.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[6].Text;
                    ////Double total = 0;

                    //SqlCommand cmd = new SqlCommand("select clientID from clientmaster where AccountName='" + LVclient.Items[LVclient.FocusedItem.Index].SubItems[0].Text + "'", con);
                    //clientID = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    //btnsave.Text = "Update";

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
                //  MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    master.RemoveCurrentTab();
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void btnnew_Click(object sender, EventArgs e)
        {
            try
            {
                Accountentry dlg = new Accountentry(master, tabControl);
                master.AddNewTab(dlg);
                dlg.Show();
            }
            catch (Exception ex)
            {
                //  MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                LVclient.Items.Clear();

                if (txtser.Text == "")
                {
                    SqlCommand cmd = new SqlCommand("select ClientID,AccountName,PrintName,GroupName,Opbal,Dr_cr,Address,City,State,Phone,Mobile,Email,Cstno,Vatno,Gstno,AdharNo from ClientMaster where isactive=1", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        LVclient.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[7].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[8].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[9].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[10].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[11].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[12].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[13].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[14].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[15].ToString());
                        // LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                        //    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                        //    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                        //    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[7].ToString());
                    }
                }
                else
                {
                    if (txtsearch.SelectedIndex == 0)
                    {
                        MessageBox.Show("Select Column Name");
                        return;
                    }
                    SqlCommand cmd = new SqlCommand("select ClientID,AccountName,PrintName,GroupName,Opbal,Dr_cr,Address,City,State,Phone,Mobile,Email,Cstno,Vatno,Gstno,AdharNo from ClientMaster where isactive=1 and " + txtsearch.Text + " like'%" + txtser.Text + "%'", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        LVclient.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[7].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[8].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[9].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[10].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[11].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[12].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[13].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[14].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[15].ToString());
                        // LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                        //    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                        //    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                        //    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[7].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Error: " + ex.Message);
            }
        }
        public void open()
        {
            try
            {
                this.Enabled = false;
                iid = LVclient.Items[LVclient.FocusedItem.Index].SubItems[0].Text;

                Accountentry dlg = new Accountentry(master, tabControl);


                dlg.Update(1, iid);
                master.AddNewTab(dlg);
                // dlg.Show();
            }
            finally
            {
                this.Enabled = true;
            }
        }
        private void LVclient_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[11]["v"].ToString() == "True" || userrights.Rows[11]["u"].ToString() == "True")
                {
                    open();
                }
                else
                {
                    MessageBox.Show("You don't have Permission To View");
                    return;
                }
            }
        }



        private void txtser_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtsearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            // e.SuppressKeyPress = true; // This will eliminate the beeping
            bool inList = false;
            for (int i = 0; i < txtsearch.Items.Count; i++)
            {
                s = txtsearch.GetItemText(txtsearch.Items[i]);
                if (s == txtsearch.Text)
                {
                    inList = true;
                    txtsearch.Text = s;
                    break;
                }
            }
            if (!inList)
            {
                txtsearch.Text = "";
            }
        }
        public static string s;
        private void txtsearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < txtsearch.Items.Count; i++)
                {
                    s = txtsearch.GetItemText(txtsearch.Items[i]);
                    if (s == txtsearch.Text)
                    {
                        inList = true;
                        txtsearch.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    txtsearch.Text = "";
                }

                txtser.Focus();
            }
        }

        private void txtser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnsearch.Focus();
            }
        }

        private void LVclient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[11]["v"].ToString() == "True" || userrights.Rows[11]["u"].ToString() == "True")
                    {
                        open();
                    }
                    else
                    {
                        MessageBox.Show("You don't have Permission To View");
                        return;
                    }
                }
            }
        }

        private void txtsearch_Enter(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.LightYellow;
        }

        private void txtsearch_Leave(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.White;
            txtsearch.Text = s;
        }

        private void txtser_Enter(object sender, EventArgs e)
        {
            txtser.BackColor = Color.LightYellow;
        }

        private void txtser_Leave(object sender, EventArgs e)
        {
            txtser.BackColor = Color.White;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnnew_MouseEnter(object sender, EventArgs e)
        {
            btnnew.UseVisualStyleBackColor = false;
            btnnew.BackColor = Color.FromArgb(9, 106, 3);
            btnnew.ForeColor = Color.White;
        }

        private void btnnew_MouseLeave(object sender, EventArgs e)
        {
            btnnew.UseVisualStyleBackColor = true;
            btnnew.BackColor = Color.FromArgb(51, 153, 255);
            btnnew.ForeColor = Color.White;
        }

        private void btnsearch_MouseEnter(object sender, EventArgs e)
        {
            btnsearch.UseVisualStyleBackColor = false;
            btnsearch.BackColor = Color.FromArgb(94, 191, 174);
            btnsearch.ForeColor = Color.White;
        }

        private void btnsearch_MouseLeave(object sender, EventArgs e)
        {
            btnsearch.UseVisualStyleBackColor = true;
            btnsearch.BackColor = Color.FromArgb(51, 153, 255);
            btnsearch.ForeColor = Color.White;
        }

        private void btnsearch_Enter(object sender, EventArgs e)
        {
            btnsearch.UseVisualStyleBackColor = false;
            btnsearch.BackColor = Color.FromArgb(94, 191, 174);
            btnsearch.ForeColor = Color.White;
        }

        private void btnsearch_Leave(object sender, EventArgs e)
        {
            btnsearch.UseVisualStyleBackColor = true;
            btnsearch.BackColor = Color.FromArgb(51, 153, 255);
            btnsearch.ForeColor = Color.White;
        }

        private void btnnew_Enter(object sender, EventArgs e)
        {
            btnnew.UseVisualStyleBackColor = false;
            btnnew.BackColor = Color.FromArgb(9, 106, 3);
            btnnew.ForeColor = Color.White;
        }

        private void btnnew_Leave(object sender, EventArgs e)
        {
            btnnew.UseVisualStyleBackColor = true;
            btnnew.BackColor = Color.FromArgb(51, 153, 255);
            btnnew.ForeColor = Color.White;
        }

        private void btnexport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("select c.ClientID,c.AccountName,c.PrintName,c.GroupName,c.Opbal,c.Dr_cr,c.Address,c.City,c.State,c.Phone,c.Mobile,c.Email,c.Cstno,c.Vatno,c.isactive,c.ismaintain,c.GstNo,c.AdharNo,c.statecode from ClientMaster c inner join AccountGroup a on c.GroupID=a.id where c.isactive=1 order by c.AccountName asc", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        string[] files = Directory.GetFiles(fbd.SelectedPath);
                        string folderPath = fbd.SelectedPath + "\\";
                        String DateTimeName = DateTime.Now.ToString("dd_MMM_yyyy hh_mm_ss");
                        // string folderPath = "C:\\Excel\\";
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(dt, "Account");
                            // wb.Worksheets.Add(dt1, "ItemPrice");
                            wb.SaveAs(folderPath + "Account" + DateTimeName + ".xlsx");
                        }
                        MessageBox.Show("Export Data Sucessfully");
                        DialogResult dr = MessageBox.Show("Do you want to Open Document?", "Account", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(folderPath + "Account" + DateTimeName + ".xlsx");
                            String pathToExecutable = "AcroRd32.exe";
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void btnimport_Click(object sender, EventArgs e)
        {
            ImportAccount frm = new ImportAccount(master, tabControl);
            master.AddNewTab(frm);
        }

        private void btnimport_Enter(object sender, EventArgs e)
        {
            btnimport.UseVisualStyleBackColor = false;
            btnimport.BackColor = Color.FromArgb(206, 204, 254);
            btnimport.ForeColor = Color.White;
        }

        private void btnimport_Leave(object sender, EventArgs e)
        {
            btnimport.UseVisualStyleBackColor = true;
            btnimport.BackColor = Color.FromArgb(51, 153, 255);
            btnimport.ForeColor = Color.White;
        }

        private void btnimport_MouseEnter(object sender, EventArgs e)
        {
            btnimport.UseVisualStyleBackColor = false;
            btnimport.BackColor = Color.FromArgb(206, 204, 254);
            btnimport.ForeColor = Color.White;
        }

        private void btnimport_MouseLeave(object sender, EventArgs e)
        {
            btnimport.UseVisualStyleBackColor = true;
            btnimport.BackColor = Color.FromArgb(51, 153, 255);
            btnimport.ForeColor = Color.White;
        }

        private void btnexport_Enter(object sender, EventArgs e)
        {
            btnexport.UseVisualStyleBackColor = false;
            btnexport.BackColor = Color.FromArgb(206, 204, 254);
            btnexport.ForeColor = Color.White;
        }

        private void btnexport_Leave(object sender, EventArgs e)
        {
            btnexport.UseVisualStyleBackColor = true;
            btnexport.BackColor = Color.FromArgb(51, 153, 255);
            btnexport.ForeColor = Color.White;
        }

        private void btnexport_MouseEnter(object sender, EventArgs e)
        {
            btnexport.UseVisualStyleBackColor = false;
            btnexport.BackColor = Color.FromArgb(206, 204, 254);
            btnexport.ForeColor = Color.White;
        }

        private void btnexport_MouseLeave(object sender, EventArgs e)
        {
            btnexport.UseVisualStyleBackColor = true;
            btnexport.BackColor = Color.FromArgb(51, 153, 255);
            btnexport.ForeColor = Color.White;
        }

        private void pnllist_Paint(object sender, PaintEventArgs e)
        {

        }
        string searchstr;
        private void txtsearch_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = txtsearch.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            txtsearch.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //empty the string for every 1 seconds
            //  searchstr = "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }

        private void btnClose_Enter(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = false;
            btnClose.BackColor = System.Drawing.Color.FromArgb(248, 152, 94);
            btnClose.ForeColor = System.Drawing.Color.White;
        }

        private void btnClose_Leave(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = true;
            btnClose.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnClose.ForeColor = System.Drawing.Color.White;
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = false;
            btnClose.BackColor = System.Drawing.Color.FromArgb(248, 152, 94);
            btnClose.ForeColor = System.Drawing.Color.White;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = true;
            btnClose.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnClose.ForeColor = System.Drawing.Color.White;
        }

        private void btnprint_Enter(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = false;
            btnprint.BackColor = System.Drawing.Color.FromArgb(176, 111, 193);
            btnprint.ForeColor = System.Drawing.Color.White;
        }

        private void btnprint_MouseEnter(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = false;
            btnprint.BackColor = System.Drawing.Color.FromArgb(176, 111, 193);
            btnprint.ForeColor = System.Drawing.Color.White;
        }

        private void btnprint_Leave(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = true;
            btnprint.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnprint.ForeColor = System.Drawing.Color.White;
        }

        private void btnprint_MouseLeave(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = true;
            btnprint.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnprint.ForeColor = System.Drawing.Color.White;
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            Printing prn = new Printing();
            Connection conn = new Connection();
            try
            {
                DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1 and CompanyID='" + Master.companyId + "' ");
                prn.execute("delete from printing");
                int j = 1;
                for (int i = 0; i < LVclient.Items.Count; i++)
                {
                    string AccountName = "", PrintName = "", Group = "", Opbal = "", DrCr = "", Address = "", City = "", State = "", Phone = "", Mobile = "", Email = "", GstNo = "", AadharNo = "";
                    AccountName = LVclient.Items[i].SubItems[1].Text;
                    PrintName = LVclient.Items[i].SubItems[2].Text;
                    Group = LVclient.Items[i].SubItems[3].Text;
                    Opbal = LVclient.Items[i].SubItems[4].Text;
                    DrCr = LVclient.Items[i].SubItems[5].Text;
                    Address = LVclient.Items[i].SubItems[6].Text;
                    City = LVclient.Items[i].SubItems[7].Text;
                    State = LVclient.Items[i].SubItems[8].Text;
                    Phone = LVclient.Items[i].SubItems[9].Text;
                    Mobile = LVclient.Items[i].SubItems[10].Text;
                    Email = LVclient.Items[i].SubItems[11].Text;
                    GstNo = LVclient.Items[i].SubItems[12].Text;
                    AadharNo = LVclient.Items[i].SubItems[13].Text;
                    string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28)VALUES";
                    qry += "('" + j++ + "','" + dt1.Rows[0][0].ToString() + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][2].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + dt1.Rows[0][11].ToString() + "','" + dt1.Rows[0][12].ToString() + "','" + dt1.Rows[0][13].ToString() + "','" + AccountName + "','" + PrintName + "','" + Group + "','" + Opbal + "','" + DrCr + "','" + Address + "','" + City + "','" + State + "','" + Phone + "','" + Mobile + "','" + Email + "','" + GstNo + "','" + AadharNo + "')";
                    prn.execute(qry);
                }
                Print popup = new Print("AccountReport");
                popup.ShowDialog();
                popup.Dispose();
            }
            catch (Exception excp)
            {

            }
        }







    }
}
