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
    public partial class CompanyMaster : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        static int CompanyID;
        private Itementry itementry;
        int a;
        private TabControl tabControl;
        private Master master;
        public static string pvc;
        public CompanyMaster()
        {
            InitializeComponent();
            loaddata();
            a = 0;


        }
        public void loaddata()
        {
            LVclientadd.Columns.Add("Company Name", 150, HorizontalAlignment.Center);
            LVclientadd.Columns.Add("Address", 200, HorizontalAlignment.Center);
            LVclientadd.Columns.Add("Mobil No.", 150, HorizontalAlignment.Center);
            LVclientadd.Columns.Add("Phone No.", 150, HorizontalAlignment.Center);
            LVclientadd.Columns.Add("Supplier Description", 100, HorizontalAlignment.Center);
        }
        public CompanyMaster(Itementry itementry)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.itementry = itementry;
            loaddata();
            a = 1;
        }

        public CompanyMaster(Itementry itementry, TabControl tabControl, Master master, string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            
            this.itementry = itementry;
            this.tabControl = tabControl;
            this.master = master;
            loaddata();
            a = 1;
            pvc = activecontroal;
        }

        public CompanyMaster(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            loaddata();
            this.master = master;
            this.tabControl = tabControl;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            if (keyData == Keys.Escape)
            {
                 DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                 if (dr == DialogResult.Yes)
                 {
                     if (string.IsNullOrEmpty(pvc) == true)
                     {
                         master.RemoveCurrentTab();
                     }
                     else
                     {
                         master.RemoveCurrentTab1(pvc, txtcompname.Text);
                     }
                 }
                return true;
            }


            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void CompanyMaster_Load(object sender, EventArgs e)
        {
            try
            {
                listviewbind();
                txtcompname.Focus();
                this.ActiveControl = txtcompname;
            }
            catch
            {
            }
        }

        private void btnaddprod_Click(object sender, EventArgs e)
        {
            try
            {
                //tabControl.TabPages.Remove(tabControl.SelectedTab);
                //ProductMaster prodmast = new ProductMaster();
                //master.AddNewTab(prodmast);
                //prodmast.StartPosition = FormStartPosition.CenterScreen;
                //prodmast.Show();
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
                    string sql = "Update CompanyMaster set companyname='" + txtcompname.Text + "',address='" + txtcompadd.Text + "',mobno='" + txtcontact.Text + "',phno='" + txtphno.Text + "',Supplierdesc='" + txtsupplierdesc.Text + "',VAT='" + txttotalvat.Text + "' where companyid='" + CompanyID + "'";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    clear();
                    listviewbind();
                    btnsave.Text = "Save";
                    MessageBox.Show("Update Data Successfully...");
                    if (a == 1)
                    {
                        itementry.bindcompany();
                        if (string.IsNullOrEmpty(pvc) == true)
                        {
                            master.RemoveCurrentTab();
                        }
                        else
                        {
                            master.RemoveCurrentTab1(pvc, txtcompname.Text);
                        }
                    }
                    else
                    {
                        this.ActiveControl = txtcompname;
                    }
                    txtcompname.Text = "";
                    
                   
                }
                else
                {
                    string sql = "Insert into CompanyMaster values('" + txtcompname.Text + "','" + txtcompadd.Text + "','" + txtcontact.Text + "','" + txtphno.Text + "','" + txttotalvat.Text + "','" + txtsupplierdesc.Text + "')";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    clear();
                    listviewbind();
                    MessageBox.Show("Insert Data Successfully...");
                    if (a == 1)
                    {
                        itementry.bindcompany();
                        if (string.IsNullOrEmpty(pvc) == true)
                        {
                            master.RemoveCurrentTab();
                        }
                        else
                        {
                            master.RemoveCurrentTab1(pvc, txtcompname.Text);
                        }
                    }
                    else
                    {
                        this.ActiveControl = txtcompname;
                    }
                    txtcompname.Text = "";
                    
                   
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
                LVclientadd.Items.Clear();
                SqlCommand cmd = new SqlCommand("select CompanyName,address,mobno,phno,VAT,Supplierdesc from CompanyMaster", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    LVclientadd.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                    LVclientadd.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                    LVclientadd.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                    LVclientadd.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                    //LVclientadd.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                    LVclientadd.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                }
            }
            catch
            {
            }
        }

        private void clear()
        {
            txtphno.Text = "";
            txtcontact.Text = "";
          
            txtcompadd.Text = "";
            txttotalvat.Text = "";
            txtsupplierdesc.Text = "";
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            try
            {
                 DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                 if (dr == DialogResult.Yes)
                 {
                     if (string.IsNullOrEmpty(pvc) == true)
                     {
                         master.RemoveCurrentTab();
                     }
                     else
                     {
                         master.RemoveCurrentTab1(pvc, txtcompname.Text);
                     }
                 }
            }
            catch
            {
            }
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            try
            {
                if (LVclientadd.FocusedItem.Selected == true)
                {
                    con.Open();
                    txtcompname.Text = LVclientadd.Items[LVclientadd.FocusedItem.Index].SubItems[0].Text;
                    txtcompadd.Text = LVclientadd.Items[LVclientadd.FocusedItem.Index].SubItems[1].Text;
                    txtcontact.Text = LVclientadd.Items[LVclientadd.FocusedItem.Index].SubItems[2].Text;
                    txtphno.Text = LVclientadd.Items[LVclientadd.FocusedItem.Index].SubItems[3].Text;
                    txttotalvat.Text = LVclientadd.Items[LVclientadd.FocusedItem.Index].SubItems[4].Text;
                    txtsupplierdesc.Text = LVclientadd.Items[LVclientadd.FocusedItem.Index].SubItems[5].Text;

                    SqlCommand cmd = new SqlCommand("select CompanyID from CompanyMaster where companyname ='" + LVclientadd.Items[LVclientadd.FocusedItem.Index].SubItems[0].Text + "' ", con);
                    CompanyID = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                    btnsave.Text = "Update";
                    listviewbind();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Select Row in Listview.....", ex.Message);
            }
        }

        public void open()
        {
            if (LVclientadd.SelectedItems.Count > 0)
            {
                txtcompname.Text = LVclientadd.Items[LVclientadd.FocusedItem.Index].SubItems[0].Text;
                txtcompadd.Text = LVclientadd.Items[LVclientadd.FocusedItem.Index].SubItems[1].Text;
                txtcontact.Text = LVclientadd.Items[LVclientadd.FocusedItem.Index].SubItems[2].Text;
                txtphno.Text = LVclientadd.Items[LVclientadd.FocusedItem.Index].SubItems[3].Text;
                // txttotalvat.Text = LVclientadd.Items[LVclientadd.FocusedItem.Index].SubItems[4].Text;
                txtsupplierdesc.Text = LVclientadd.Items[LVclientadd.FocusedItem.Index].SubItems[4].Text;

                con.Open();
                SqlCommand cmd = new SqlCommand("select CompanyID from CompanyMaster where companyname ='" + LVclientadd.Items[LVclientadd.FocusedItem.Index].SubItems[0].Text + "' ", con);
                CompanyID = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                btnsave.Text = "Update";
                listviewbind();
                con.Close();
            }
        }
        private void LVclientadd_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            open();
        }

        

        private void txtcompname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtcompadd.Focus();
                }
            }
            catch
            {
            }
        }

        private void txtcompadd_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtcontact.Focus();
                }
                }
            catch
            {
            }
        }

        private void txtcontact_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtphno.Focus();
                }
            }
            catch
            {
            }
        }

        private void txtphno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtsupplierdesc.Focus();
                    //txttotalvat.Focus();
                }
            }
            catch
            {
            }
        }

        private void txttotalvat_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtsupplierdesc.Focus();
                }
            }
            catch
            {
            }
        }

        private void txtcontact_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtphno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtcompname_Enter(object sender, EventArgs e)
        {
            txtcompname.BackColor = Color.LightYellow;
        }

        private void txtcompname_Leave(object sender, EventArgs e)
        {
            txtcompname.BackColor = Color.White;
        }

        private void txtcompadd_Enter(object sender, EventArgs e)
        {
            txtcompadd.BackColor = Color.LightYellow;
        }

        private void txtcompadd_Leave(object sender, EventArgs e)
        {
            txtcompadd.BackColor = Color.White;
        }

        private void txtcontact_Enter(object sender, EventArgs e)
        {
            txtcontact.BackColor = Color.LightYellow;
        }

        private void txtcontact_Leave(object sender, EventArgs e)
        {
            txtcontact.BackColor = Color.White;
        }

        private void txtphno_Enter(object sender, EventArgs e)
        {
            txtphno.BackColor = Color.LightYellow;
        }

        private void txtphno_Leave(object sender, EventArgs e)
        {
            txtphno.BackColor = Color.White;
        }

        private void txttotalvat_Enter(object sender, EventArgs e)
        {
            txttotalvat.BackColor = Color.LightYellow;
        }

        private void txttotalvat_Leave(object sender, EventArgs e)
        {
            txttotalvat.BackColor = Color.White;
        }

        private void txtsupplierdesc_Enter(object sender, EventArgs e)
        {
            txtsupplierdesc.BackColor = Color.LightYellow;
        }

        private void txtsupplierdesc_Leave(object sender, EventArgs e)
        {
            txtsupplierdesc.BackColor = Color.White;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnsave_MouseEnter(object sender, EventArgs e)
        {
            btnsave.UseVisualStyleBackColor = false;
            btnsave.BackColor = Color.YellowGreen;
            btnsave.ForeColor = Color.White;
        }

        private void btnsave_MouseLeave(object sender, EventArgs e)
        {
            btnsave.UseVisualStyleBackColor = true;
            btnsave.BackColor = Color.FromArgb(51, 153, 255);
            btnsave.ForeColor = Color.White;
        }

        private void btncancel_MouseEnter(object sender, EventArgs e)
        {
            btncancel.UseVisualStyleBackColor = false;
            btncancel.BackColor = Color.FromArgb(248, 152, 94);
            btncancel.ForeColor = Color.White;
        }

        private void btncancel_MouseLeave(object sender, EventArgs e)
        {
            btncancel.UseVisualStyleBackColor = true;
            btncancel.BackColor = Color.FromArgb(51, 153, 255);
            btncancel.ForeColor = Color.White;
        }

        private void btnsave_Enter(object sender, EventArgs e)
        {
            btnsave.UseVisualStyleBackColor = false;
            btnsave.BackColor = Color.YellowGreen;
            btnsave.ForeColor = Color.White;
        }

        private void btnsave_Leave(object sender, EventArgs e)
        {
            btnsave.UseVisualStyleBackColor = true;
            btnsave.BackColor = Color.FromArgb(51, 153, 255);
            btnsave.ForeColor = Color.White;
        }

        private void btncancel_Enter(object sender, EventArgs e)
        {
            btncancel.UseVisualStyleBackColor = false;
            btncancel.BackColor = Color.FromArgb(248, 152, 94);
            btncancel.ForeColor = Color.White;
        }

        private void btncancel_Leave(object sender, EventArgs e)
        {
            btncancel.UseVisualStyleBackColor = true;
            btncancel.BackColor = Color.FromArgb(51, 153, 255);
            btncancel.ForeColor = Color.White;
        }












        internal void Update(string company)
        {
            if (company != "" && company != null)
            {
                SqlCommand cmd5 = new SqlCommand("Select * from CompanyMaster where CompanyID ='" + company + "'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd5);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtcompname.Text = dt.Rows[0]["companyname"].ToString();
                    txtcompadd.Text = dt.Rows[0]["address"].ToString();
                    txtcontact.Text = dt.Rows[0]["mobno"].ToString();
                    txtphno.Text = dt.Rows[0]["phno"].ToString();
                    txttotalvat.Text = dt.Rows[0]["VAT"].ToString();
                    txtsupplierdesc.Text = dt.Rows[0]["Supplierdesc"].ToString();
                    btnsave.Text = "Update";
                    int id = int.Parse(dt.Rows[0]["CompanyID"].ToString());
                    CompanyID = id;
                }
            }
           // throw new NotImplementedException();
        }

        private void LVclientadd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                open();
            }
        }
    }
}
