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

namespace RamdevSales
{
    public partial class frmReceiveFromCompany : Form
    {
        private Master master;
        private TabControl tabControl;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        Printing prn = new Printing();
        int cnt = 0;
        string comid;
        OleDbSettings ods = new OleDbSettings();
        public frmReceiveFromCompany()
        {
            InitializeComponent();
        }

        public frmReceiveFromCompany(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
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
                    master.RemoveCurrentTab();
                }
                return true;
            }
            if (keyData == (Keys.Alt | Keys.U))
            {
                //DialogResult dr = MessageBox.Show("Do you want to Update?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (dr == DialogResult.Yes)
                //{
                enterdata();
                //}
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }
        public void bindcustomer()
        {
            try
            {
                string qry = "";
                DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='100'");
                string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=100 or GroupID='" + groupid + "') order by AccountName";
                SqlCommand cmd1 = new SqlCommand(qry, con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                cmbSupplierName.ValueMember = "ClientID";
                cmbSupplierName.DisplayMember = "AccountName";
                cmbSupplierName.DataSource = dt1;
                cmbSupplierName.SelectedIndex = -1;
            }
            catch
            {
            }
        }
        public void bindsendtocompanyid()
        {
            try
            {
                string qry = "";
                //qry = "select id,complainid from tblcomplainmaster where isactive=1 order by id";
                qry = "SELECT DISTINCT id from tblsendtocompanymaster where isactive=1";
                SqlCommand cmd1 = new SqlCommand(qry, con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                cmbscno.ValueMember = "id";
                cmbscno.DisplayMember = "id";
                cmbscno.DataSource = dt1;
                cmbscno.SelectedIndex = -1;
            }
            catch
            {
            }
        }
        public void bindcomplainid()
        {
            try
            {
                string qry = "";
                //qry = "select id,complainid from tblcomplainmaster where isactive=1 order by id";
                qry = "SELECT DISTINCT complainID from tblitemcomplainmaster where isactive=1 and status='Send To Company'";
                SqlCommand cmd1 = new SqlCommand(qry, con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                cmbComplainId.ValueMember = "complainID";
                cmbComplainId.DisplayMember = "complainID";
                cmbComplainId.DataSource = dt1;
                cmbComplainId.SelectedIndex = -1;
            }
            catch
            {
            }
        }
        public void bindserialno()
        {
            try
            {
                string qry = "";
                qry = "select complainID,serialno from tblitemcomplainmaster where isactive=1 and status='Send To Company' order by complainID";
                SqlCommand cmd1 = new SqlCommand(qry, con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                cmbSerialNo.ValueMember = "complainID";
                cmbSerialNo.DisplayMember = "serialno";
                cmbSerialNo.DataSource = dt1;
                cmbSerialNo.SelectedIndex = -1;
            }
            catch
            {
            }
        }
        public void bindcomplainidbyserialno()
        {
            try
            {
                string qry = "";
                qry = "select DISTINCT complainID from tblitemcomplainmaster where isactive=1 and status='Send To Company' and serialno='" + cmbSerialNo.Text + "'";
                SqlCommand cmd1 = new SqlCommand(qry, con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                cmbComplainId.ValueMember = "complainID";
                cmbComplainId.DisplayMember = "complainID";
                cmbComplainId.DataSource = dt1;
                //cmbComplainId.SelectedIndex = -1;
            }
            catch
            {
            }
        }
        public void bindserialnobycompanyid()
        {
            try
            {
                cmbSerialNo.DataSource = null;
                string qry = "";
                qry = "select complainID,serialno from tblitemcomplainmaster where isactive=1 and status='Send To Company' and complainID='" + cmbComplainId.SelectedValue + "'";
                SqlCommand cmd1 = new SqlCommand(qry, con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                cmbSerialNo.ValueMember = "complainID";
                cmbSerialNo.DisplayMember = "serialno";
                cmbSerialNo.DataSource = dt1;
                // cmbSerialNo.SelectedIndex = -1;
            }
            catch
            {
            }
        }
        public void bindcomplainidbyserialno1()
        {
            try
            {
                string qry = "";
                qry = "select DISTINCT complainID from tblitemcomplainmaster where isactive=1 and status='Product Received From Company' and serialno='" + cmbSerialNo.Text + "'";
                SqlCommand cmd1 = new SqlCommand(qry, con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                cmbComplainId.ValueMember = "complainID";
                cmbComplainId.DisplayMember = "complainID";
                cmbComplainId.DataSource = dt1;
                //cmbComplainId.SelectedIndex = -1;
            }
            catch
            {
            }
        }
        public void bindserialnobycompanyid1()
        {
            try
            {
                cmbSerialNo.DataSource = null;
                string qry = "";
                qry = "select complainID,serialno from tblitemcomplainmaster where isactive=1 and status='Product Received From Company' and complainID='" + cmbComplainId.SelectedValue + "'";
                SqlCommand cmd1 = new SqlCommand(qry, con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                cmbSerialNo.ValueMember = "complainID";
                cmbSerialNo.DisplayMember = "serialno";
                cmbSerialNo.DataSource = dt1;
                // cmbSerialNo.SelectedIndex = -1;
            }
            catch
            {
            }
        }
        public void bindserialnobysendtocompanyid()
        {
            try
            {
                cmbSerialNo.DataSource = null;
                string qry = "";
                qry = "select sendtocompanyid,serialno from tblsendtocompanyitemmaster where isactive=1 and sendtocompanyID='"+cmbscno.Text+"'";
                SqlCommand cmd1 = new SqlCommand(qry, con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                cmbSerialNo.ValueMember = "sendtocompanyid";
                cmbSerialNo.DisplayMember = "serialno";
                cmbSerialNo.DataSource = dt1;
                cmbSerialNo.SelectedIndex = -1;
            }
            catch
            {
            }
        }
        private void frmReceiveFromCompany_Load(object sender, EventArgs e)
        {
            try
            {
                if (cnt == 0)
                {
                    lvcompany.Columns.Add("Issue ID", 70, HorizontalAlignment.Left);
                    lvcompany.Columns.Add("Complain ID", 70, HorizontalAlignment.Left);
                    lvcompany.Columns.Add("Serial No", 120, HorizontalAlignment.Left);
                    lvcompany.Columns.Add("Item Name", 250, HorizontalAlignment.Left);
                    lvcompany.Columns.Add("Remarks", 100, HorizontalAlignment.Left);
                    lvcompany.Columns.Add("Transport Details", 100, HorizontalAlignment.Left);
                    lvcompany.Columns.Add("Sent Date", 100, HorizontalAlignment.Left);
                    lvcompany.Columns.Add("New Serial No", 200, HorizontalAlignment.Left);
                    lvcompany.Columns.Add("itemid", 0, HorizontalAlignment.Left);
                    DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
                    dtpDate.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                    dtpDate.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                    dtpDate.CustomFormat = Master.dateformate;
                    bindcomplainid();
                    bindserialno();
                    bindcustomer();
                    getcompanyid();
                    bindsendtocompanyid();
                    this.ActiveControl = dtpDate;
                }
            }
            catch
            {
            }
        }

        private void cmbComplainId_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbComplainId.SelectedIndex = 0;
                cmbComplainId.DroppedDown = true;
            }
            catch
            {
            }
        }
        public static string s;
        private void cmbComplainId_Leave(object sender, EventArgs e)
        {
          //  cmbComplainId.Text = s;
        }

        private void cmbComplainId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbComplainId.Items.Count; i++)
                {
                    s = cmbComplainId.GetItemText(cmbComplainId.Items[i]);
                    if (s == cmbComplainId.Text)
                    {
                        inList = true;
                        cmbComplainId.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbComplainId.Text = "";
                }
                //  bindserialbycomplainid();
            }
            catch (Exception excp)
            {
            }
        }

        private void cmbComplainId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbComplainId.Items.Count; i++)
                {
                    s = cmbComplainId.GetItemText(cmbComplainId.Items[i]);
                    if (s == cmbComplainId.Text)
                    {
                        inList = true;
                        cmbComplainId.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbComplainId.Text = "";
                }

                if (cmbComplainId.Text != "")
                {
                    if (btnSubmit.Text == "Update")
                    {
                        bindserialnobycompanyid1();
                    }
                    else
                    {
                        bindserialnobycompanyid();
                    }
                    cmbSerialNo.Focus();
                    // txtpono.Focus();
                }
                else
                {
                    MessageBox.Show("Please Select Complain ID");
                    cmbComplainId.Focus();
                }


            }
        }
        ListViewItem li;
        public void bindlistview()
        {
            try
            {
                DataTable dt = new DataTable();
                if (cmbComplainId.SelectedIndex == -1)
                {
                    dt = conn.getdataset("select * from tblsendtocompanyitemmaster where isactive=1  and sendtocompanyid='" + cmbscno.Text + "' and serialno='" + cmbSerialNo.Text + "'");
                }
                else
                {
                    //DataTable dt = conn.getdataset("select * from tblsendtocompanymaster where isactive=1 and serialno='" + cmbSerialNo.Text + "' and complainid='" + cmbComplainId.Text + "'");
                    dt = conn.getdataset("select * from tblsendtocompanyitemmaster where isactive=1  and complainID='" + cmbComplainId.Text + "' and serialno='" + cmbSerialNo.Text + "'");
                }
                
                if (dt.Rows.Count > 0)
                {
                    bool exists = false;
                    foreach (ListViewItem item in lvcompany.Items)
                    {
                        for (int b = 0; b < item.SubItems.Count; b++)
                        {
                            string srno = item.SubItems[2].Text;
                            if (cmbSerialNo.Text == srno)
                            {
                                exists = true;
                            }
                        }
                    }
                    if (!exists)
                    {
                        DataTable dt1 = conn.getdataset("select * from tblsendtocompanymaster where isactive=1 and id='" + dt.Rows[0]["sendtocompanyID"].ToString() + "'");
                        if (dt1.Rows.Count > 0)
                        {
                            cmbSupplierName.Text = dt1.Rows[0]["suppliername"].ToString();
                            cmbSupplierName.Enabled = false;

                        }
                      //  lvcompany.Items.Clear();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            li = lvcompany.Items.Add(dt.Rows[i]["sendtocompanyID"].ToString());
                            li.SubItems.Add(dt.Rows[i]["complainID"].ToString());
                            li.SubItems.Add(dt.Rows[i]["serialno"].ToString());
                            li.SubItems.Add(dt.Rows[i]["itemname"].ToString());
                            li.SubItems.Add(dt1.Rows[0]["remarks"].ToString());
                            li.SubItems.Add(dt1.Rows[0]["transportdetails"].ToString());
                            li.SubItems.Add(Convert.ToDateTime(dt1.Rows[0]["date"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add(txtNewSerialNo.Text);
                            string itemid = conn.ExecuteScalar("select ProductID from ProductMaster where isactive=1 and Product_Name='" + dt.Rows[i]["itemname"].ToString() + "'");
                            li.SubItems.Add(itemid);
                        }
                    }
                    else
                    {
                        MessageBox.Show("This Item's Complain is Already in Process");
                        cmbSerialNo.Focus();
                    }
                }
            }
            catch
            {
            }
        }
        private void cmbSerialNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbSerialNo.Items.Count; i++)
                {
                    s = cmbSerialNo.GetItemText(cmbSerialNo.Items[i]);
                    if (s == cmbSerialNo.Text)
                    {
                        inList = true;
                        cmbSerialNo.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbSerialNo.Text = "";
                }

                if (cmbSerialNo.Text != "")
                {
                    if (btnSubmit.Text == "Update")
                    {
                        if (cmbComplainId.SelectedIndex == -1)
                        {
                            bindcomplainidbyserialno1();
                        }
                    }
                    else
                    {
                        if (cmbComplainId.SelectedIndex == -1)
                        {
                            bindcomplainidbyserialno();
                            
                        }
                        //else
                        //{
                        //    bindlistview();
                        //}
                    }
                  
                    txtNewSerialNo.Focus();
                    // txtpono.Focus();
                }
                else
                {
                    MessageBox.Show("Please Select Serial No");
                    cmbSerialNo.Focus();
                }
            }
        }

        private void cmbSerialNo_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbSerialNo.SelectedIndex = 0;
                cmbSerialNo.DroppedDown = true;
            }
            catch
            {
            }

        }


        private void cmbSerialNo_Leave(object sender, EventArgs e)
        {
           // cmbSerialNo.Text = s;
        }

        private void cmbSerialNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbSerialNo.Items.Count; i++)
                {
                    s = cmbSerialNo.GetItemText(cmbSerialNo.Items[i]);
                    if (s == cmbSerialNo.Text)
                    {
                        inList = true;
                        cmbSerialNo.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbSerialNo.Text = "";
                }
                //   bindcomplainidbyserialno();
            }
            catch (Exception excp)
            {
            }
        }

        private void dtpDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbscno.Focus();
            }
        }

        private void cmbSupplierName_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbSupplierName.SelectedIndex = 0;
                cmbSupplierName.DroppedDown = true;
            }
            catch
            {
            }
        }

        private void cmbSupplierName_Leave(object sender, EventArgs e)
        {
           // cmbSupplierName.Text = s;
        }

        private void cmbSupplierName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbSupplierName.Items.Count; i++)
                {
                    s = cmbSupplierName.GetItemText(cmbSupplierName.Items[i]);
                    if (s == cmbSupplierName.Text)
                    {
                        inList = true;
                        cmbSupplierName.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbSupplierName.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void cmbSupplierName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbSupplierName.Items.Count; i++)
                {
                    s = cmbSupplierName.GetItemText(cmbSupplierName.Items[i]);
                    if (s == cmbSupplierName.Text)
                    {
                        inList = true;
                        cmbSupplierName.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbSupplierName.Text = "";
                }

                if (cmbSupplierName.Text != "")
                {
                    txtNewSerialNo.Focus();
                    // txtpono.Focus();
                }
                else
                {
                    MessageBox.Show("Please Select Supplier Name");
                    cmbSupplierName.Focus();
                }

            }
        }

        private void txtNewSerialNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bindlistview();
                txtNewSerialNo.Text = "";
                if (rdbCompanyRepair.Checked == true)
                {
                    cmbscno.Focus();
                }
                else
                {
                    cmbComplainId.Focus();
                }
            }
        }

        private void txtRemarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSubmit.Focus();
            }
        }

        private void rdbMaxRepair_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbMaxRepair.Checked == true)
            {
                if (btnSubmit.Text == "Update")
                {
                    cmbComplainId.Focus();
                }
                else
                {
                    bindcomplainid();
                    cmbComplainId.Focus();
                }
            }
        }

        private void rdbCompanyRepair_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbCompanyRepair.Checked == true)
            {
                if (btnSubmit.Text == "Update")
                {
                    cmbscno.Focus();
                }
                else
                {
                    bindsendtocompanyid();
                    cmbscno.Focus();
                }
            }
        }

        private void btnSubmit_Enter(object sender, EventArgs e)
        {
            btnSubmit.UseVisualStyleBackColor = false;
            btnSubmit.BackColor = Color.YellowGreen;
            btnSubmit.ForeColor = Color.White;
        }

        private void btnSubmit_Leave(object sender, EventArgs e)
        {
            btnSubmit.UseVisualStyleBackColor = true;
            btnSubmit.BackColor = Color.FromArgb(51, 153, 255);
            btnSubmit.ForeColor = Color.White;
        }

        private void btnSubmit_MouseEnter(object sender, EventArgs e)
        {
            btnSubmit.UseVisualStyleBackColor = false;
            btnSubmit.BackColor = Color.YellowGreen;
            btnSubmit.ForeColor = Color.White;
        }

        private void btnSubmit_MouseLeave(object sender, EventArgs e)
        {
            btnSubmit.UseVisualStyleBackColor = true;
            btnSubmit.BackColor = Color.FromArgb(51, 153, 255);
            btnSubmit.ForeColor = Color.White;
        }

        private void btndelete_Enter(object sender, EventArgs e)
        {
            btndelete.UseVisualStyleBackColor = false;
            btndelete.BackColor = Color.FromArgb(255, 77, 77);
            btndelete.ForeColor = Color.White;
        }

        private void btndelete_Leave(object sender, EventArgs e)
        {
            btndelete.UseVisualStyleBackColor = true;
            btndelete.BackColor = Color.FromArgb(51, 153, 255);
            btndelete.ForeColor = Color.White;
        }

        private void btndelete_MouseEnter(object sender, EventArgs e)
        {
            btndelete.UseVisualStyleBackColor = false;
            btndelete.BackColor = Color.FromArgb(255, 77, 77);
            btndelete.ForeColor = Color.White;
        }

        private void btndelete_MouseLeave(object sender, EventArgs e)
        {
            btndelete.UseVisualStyleBackColor = true;
            btndelete.BackColor = Color.FromArgb(51, 153, 255);
            btndelete.ForeColor = Color.White;
        }

        private void btnprint_Enter(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = false;
            btnprint.BackColor = Color.FromArgb(176, 111, 193);
            btnprint.ForeColor = Color.White;
        }

        private void btnprint_Leave(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = true;
            btnprint.BackColor = Color.FromArgb(51, 153, 255);
            btnprint.ForeColor = Color.White;
        }

        private void btnprint_MouseEnter(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = false;
            btnprint.BackColor = Color.FromArgb(176, 111, 193);
            btnprint.ForeColor = Color.White;
        }

        private void btnprint_MouseLeave(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = true;
            btnprint.BackColor = Color.FromArgb(51, 153, 255);
            btnprint.ForeColor = Color.White;
        }

        private void btnclose_Enter(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = false;
            btnclose.BackColor = Color.FromArgb(248, 152, 94);
            btnclose.ForeColor = Color.White;
        }

        private void btnclose_Leave(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = true;
            btnclose.BackColor = Color.FromArgb(51, 153, 255);
            btnclose.ForeColor = Color.White;
        }

        private void btnclose_MouseEnter(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = false;
            btnclose.BackColor = Color.FromArgb(248, 152, 94);
            btnclose.ForeColor = Color.White;
        }

        private void btnclose_MouseLeave(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = true;
            btnclose.BackColor = Color.FromArgb(51, 153, 255);
            btnclose.ForeColor = Color.White;
        }
        string CompID;
        public void getcompanyid()
        {
            try
            {
                CompID = conn.ExecuteScalar("select max(id) from tblreceivefromcompany where isactive=1");
                Int64 id, count;
                if (CompID == "")
                {

                    id = 1;
                    count = 1;
                }
                else
                {
                    id = Convert.ToInt32(CompID) + 1;
                    count = Convert.ToInt32(CompID) + 1;
                }
                comid = Convert.ToString(id);
                lblreceiveid.Text = comid;
            }
            catch
            {
            }
        }
        public void clearall()
        {
            cmbSerialNo.SelectedIndex = -1;
            cmbComplainId.SelectedIndex = -1;
            cmbSupplierName.SelectedIndex = -1;
            txtRemarks.Text = "";
            txtNewSerialNo.Text = "";
            lvcompany.Items.Clear();
            cmbSupplierName.Enabled = true;
            getcompanyid();
            btnSubmit.Text = "Submit";
        }
        public void enterdata()
        {
            try
            {

                if (btnSubmit.Text == "Update")
                {
                    if (lvcompany.Items.Count > 0)
                    {
                        DataTable final = conn.getdataset("select * from tblitemreceivefromcompany where isactive=1 and receivefromcompanyid='" + lblreceiveid.Text + "'");
                        DataTable countdt = new DataTable();
                        countdt.Columns.Add("id", typeof(string));
                        for (int i = 0; i < final.Rows.Count; i++)
                        {
                            for (int j = 0; j < lvcompany.Items.Count; j++)
                            {
                                if (final.Rows[i]["complainID"].ToString() == lvcompany.Items[j].SubItems[1].Text && final.Rows[i]["serialno"].ToString() == lvcompany.Items[j].SubItems[2].Text && final.Rows[i]["itemid"].ToString() == lvcompany.Items[j].SubItems[8].Text)
                                {
                                    //conn.execute("INSERT INTO [dbo].[tblitemreceivefromcompany]([receivefromcompanyid],[sendtocompanyid],[complainID],[serialno],[itemname],[remarks],[transportdetails],[senddate],[newserialno],[itemid],[isactive])VALUES('" + lblreceiveid.Text + "','" + lvcompany.Items[i].SubItems[0].Text + "','" + lvcompany.Items[i].SubItems[1].Text + "','" + lvcompany.Items[i].SubItems[2].Text + "','" + lvcompany.Items[i].SubItems[3].Text + "','" + lvcompany.Items[i].SubItems[4].Text + "','" + lvcompany.Items[i].SubItems[5].Text + "','" + lvcompany.Items[i].SubItems[6].Text + "','" + lvcompany.Items[i].SubItems[7].Text + "','" + lvcompany.Items[i].SubItems[8].Text + "','" + "1" + "')");
                                    conn.execute("Update tblitemreceivefromcompany set receivefromcompanyid='" + lblreceiveid.Text + "',sendtocompanyid='" + lvcompany.Items[i].SubItems[0].Text + "',complainID='" + lvcompany.Items[i].SubItems[1].Text + "',serialno='" + lvcompany.Items[i].SubItems[2].Text + "',itemname='" + lvcompany.Items[i].SubItems[3].Text + "',remarks='" + lvcompany.Items[i].SubItems[4].Text + "',transportdetails='" + lvcompany.Items[i].SubItems[5].Text + "',senddate='" + lvcompany.Items[i].SubItems[6].Text + "',newserialno='" + lvcompany.Items[i].SubItems[7].Text + "',itemid='" + lvcompany.Items[i].SubItems[8].Text + "' where complainID='" + lvcompany.Items[j].SubItems[1].Text + "' and serialno='" + lvcompany.Items[j].SubItems[2].Text + "' and itemid='" + lvcompany.Items[j].SubItems[8].Text + "'");
                                    conn.execute("Update tblitemcomplainmaster set status='" + "Product Received From Company" + "' where isactive=1 and complainID='" + lvcompany.Items[j].SubItems[1].Text + "' and serialno='" + lvcompany.Items[j].SubItems[2].Text + "' and itemid='" + lvcompany.Items[j].SubItems[8].Text + "'");
                                    countdt.Rows.Add(final.Rows[i]["id"].ToString());
                                }
                            }
                        }
                        DataTable final1 = final.DefaultView.ToTable(false, "id");
                        final1 = changedtclone(final1);
                        countdt = changedtclone(countdt);
                        for (int i = 0; i < countdt.Rows.Count; i++)
                        {
                            for (int j = 0; j < final1.Rows.Count; j++)
                            {
                                if (final1.Rows[j]["id"].ToString() == countdt.Rows[i]["id"].ToString())
                                {
                                    final1.Rows.RemoveAt(j);
                                }
                            }
                        }
                        final1.AcceptChanges();
                        for (int h = 0; h < final1.Rows.Count; h++)
                        {

                            DataTable finaldt = conn.getdataset("select * from tblitemreceivefromcompany where isactive=1 and id='" + final1.Rows[h]["id"].ToString() + "'");
                            if (finaldt.Rows.Count > 0)
                            {
                                conn.execute("Update tblitemcomplainmaster set status='" + "Send To Company" + "' where isactive=1 and complainID='" + finaldt.Rows[0]["complainID"].ToString() + "' and serialno='" + finaldt.Rows[0]["serialno"].ToString() + "' and itemid='" + finaldt.Rows[0]["itemid"].ToString() + "'");
                            }
                            conn.execute("Update tblitemreceivefromcompany set isactive=0 where id='" + final1.Rows[h]["id"].ToString() + "'");
                        }
                        //string s = "Product Received From Company";
                        //conn.execute("Update tblitemreceivefromcompany set isactive=0 where receivefromcompanyid='" + lblreceiveid.Text + "'");
                        //for (int i = 0; i < lvcompany.Items.Count; i++)
                        //{
                        //    conn.execute("INSERT INTO [dbo].[tblitemreceivefromcompany]([receivefromcompanyid],[sendtocompanyid],[complainID],[serialno],[itemname],[remarks],[transportdetails],[senddate],[newserialno],[itemid],[isactive])VALUES('" + lblreceiveid.Text + "','" + lvcompany.Items[i].SubItems[0].Text + "','" + lvcompany.Items[i].SubItems[1].Text + "','" + lvcompany.Items[i].SubItems[2].Text + "','" + lvcompany.Items[i].SubItems[3].Text + "','" + lvcompany.Items[i].SubItems[4].Text + "','" + lvcompany.Items[i].SubItems[5].Text + "','" + lvcompany.Items[i].SubItems[6].Text + "','" +lvcompany.Items[i].SubItems[7].Text+"','"+ lvcompany.Items[i].SubItems[8].Text + "','" + "1" + "')");
                        //    conn.execute("Update tblitemcomplainmaster set status='" + s + "' where isactive=1 and complainID='" + lvcompany.Items[i].SubItems[1].Text + "' and serialno='" + lvcompany.Items[i].SubItems[2].Text + "' and itemid='" + lvcompany.Items[i].SubItems[8].Text + "'");
                        //}
                        conn.execute("Update tblreceivefromcompany set date='" + Convert.ToDateTime(dtpDate.Text).ToString(Master.dateformate) + "',complainid='" + cmbComplainId.Text + "',oldserialno='" + cmbSerialNo.Text + "',newserialno='" + txtNewSerialNo.Text + "',supplierid='" + cmbSupplierName.SelectedValue + "',suppliername='" + cmbSupplierName.Text + "',remarks='" + txtRemarks.Text + "' where id='" + lblreceiveid.Text + "'");
                        clearall();
                        MessageBox.Show("Data Updated Successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Item");
                        txtNewSerialNo.Focus();
                    }
                }
                else
                {
                    if (lvcompany.Items.Count > 0)
                    {
                        string s = "Product Received From Company";
                        //string readio;
                        //if (rdbCompanyRepair.Checked == true)
                        //{
                        //    readio = rdbCompanyRepair.Text;
                        //}
                        //else
                        //{
                        //    readio = rdbMaxRepair.Text;
                        //}
                        conn.getdataset("INSERT INTO [dbo].[tblreceivefromcompany]([date],[complainid],[oldserialno],[newserialno],[supplierid],[suppliername],[remarks],[isactive])VALUES('" + Convert.ToDateTime(dtpDate.Text).ToString(Master.dateformate) + "','" + cmbComplainId.Text + "','" + cmbSerialNo.Text + "','" + txtNewSerialNo.Text + "','" + cmbSupplierName.SelectedValue + "','" + cmbSupplierName.Text + "','" + txtRemarks.Text + "','" + "1" + "')");
                        for (int i = 0; i < lvcompany.Items.Count; i++)
                        {
                            conn.execute("INSERT INTO [dbo].[tblitemreceivefromcompany]([receivefromcompanyid],[sendtocompanyid],[complainID],[serialno],[itemname],[remarks],[transportdetails],[senddate],[newserialno],[itemid],[isactive])VALUES('" + lblreceiveid.Text + "','" + lvcompany.Items[i].SubItems[0].Text + "','" + lvcompany.Items[i].SubItems[1].Text + "','" + lvcompany.Items[i].SubItems[2].Text + "','" + lvcompany.Items[i].SubItems[3].Text + "','" + lvcompany.Items[i].SubItems[4].Text + "','" + lvcompany.Items[i].SubItems[5].Text + "','" + lvcompany.Items[i].SubItems[6].Text + "','" + lvcompany.Items[i].SubItems[7].Text + "','" + lvcompany.Items[i].SubItems[8].Text + "','" + "1" + "')");
                            conn.execute("Update tblitemcomplainmaster set status='" + s + "' where isactive=1 and complainID='" + lvcompany.Items[i].SubItems[1].Text + "' and serialno='" + lvcompany.Items[i].SubItems[2].Text + "' and itemid='" + lvcompany.Items[i].SubItems[8].Text + "'");
                        }
                        clearall();
                        MessageBox.Show("Data Inserted Successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Item");
                        txtNewSerialNo.Focus();
                    }
                }
            }
            catch
            {
            }
        }
        private DataTable changedtclone(DataTable dt)
        {
            DataTable dtClone = dt.Clone(); //just copy structure, no data
            for (int i = 0; i < dtClone.Columns.Count; i++)
            {
                if (dtClone.Columns[i].DataType != typeof(string))
                    dtClone.Columns[i].DataType = typeof(string);
            }

            foreach (DataRow dr in dt.Rows)
            {
                dtClone.ImportRow(dr);
            }
            return dtClone;
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            enterdata();
        }

        internal void Update(int p, string iid)
        {
            cnt = 1;
            lvcompany.Columns.Add("Issue ID", 70, HorizontalAlignment.Left);
            lvcompany.Columns.Add("Complain ID", 70, HorizontalAlignment.Left);
            lvcompany.Columns.Add("Serial No", 120, HorizontalAlignment.Left);
            lvcompany.Columns.Add("Item Name", 250, HorizontalAlignment.Left);
            lvcompany.Columns.Add("Remarks", 100, HorizontalAlignment.Left);
            lvcompany.Columns.Add("Transport Details", 100, HorizontalAlignment.Left);
            lvcompany.Columns.Add("Sent Date", 100, HorizontalAlignment.Left);
            lvcompany.Columns.Add("New Serial No", 200, HorizontalAlignment.Left);
            lvcompany.Columns.Add("itemid",0, HorizontalAlignment.Left);
            DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
            dtpDate.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            dtpDate.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            dtpDate.CustomFormat = Master.dateformate;
            try
            {
                string qry = "";
                //qry = "select id,complainid from tblcomplainmaster where isactive=1 order by id";
                qry = "SELECT DISTINCT complainID from tblitemcomplainmaster where isactive=1 and status='Product Received From Company'";
                SqlCommand cmd1 = new SqlCommand(qry, con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                cmbComplainId.ValueMember = "complainID";
                cmbComplainId.DisplayMember = "complainID";
                cmbComplainId.DataSource = dt1;
                cmbComplainId.SelectedIndex = -1;
            }
            catch
            {
            }
            try
            {
                string qry = "";
                qry = "select complainid,serialno from tblitemcomplainmaster where isactive=1 and status='Product Received From Company' order by complainID";
                SqlCommand cmd1 = new SqlCommand(qry, con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                cmbSerialNo.ValueMember = "complainID";
                cmbSerialNo.DisplayMember = "serialno";
                cmbSerialNo.DataSource = dt1;
                cmbSerialNo.SelectedIndex = -1;
            }
            catch
            {
            }
            //   bindcomplainid();
            // bindserialno();
            bindcustomer();
            bindsendtocompanyid();
            this.ActiveControl = dtpDate;
            DataTable dt = conn.getdataset("select * from tblreceivefromcompany where isactive=1 and id='" + iid + "'");
            DataTable dt11 = conn.getdataset("select * from tblitemreceivefromcompany where isactive=1 and receivefromcompanyid='" + iid + "'");
            
            if (dt.Rows.Count > 0)
            {
                lblreceiveid.Text = dt.Rows[0]["id"].ToString();
                cmbComplainId.Text = dt.Rows[0]["complainid"].ToString();
                dtpDate.Text = Convert.ToDateTime(dt.Rows[0]["Date"].ToString()).ToString(Master.dateformate);
                cmbSupplierName.Text = dt.Rows[0]["suppliername"].ToString();
                cmbSerialNo.Text = dt.Rows[0]["oldserialno"].ToString();
                txtNewSerialNo.Text = dt.Rows[0]["newserialno"].ToString();
                txtRemarks.Text = dt.Rows[0]["remarks"].ToString();
                //if (dt.Rows[0]["repairarea"].ToString() == "On Repair")
                //{
                //    rdbMaxRepair.Checked = true;
                //}
                //else
                //{
                //    rdbCompanyRepair.Checked = true;
                //}
            }
            if (dt11.Rows.Count > 0)
            {
                for (int i = 0; i < dt11.Rows.Count; i++)
                {
                    DataTable comid = conn.getdataset("select * from tblitemcomplainmaster where isactive=1 and ComplainID='" + dt11.Rows[i]["complainID"].ToString() + "' and serialno='" + dt11.Rows[i]["serialno"].ToString() + "' and itemid='" + dt11.Rows[i]["itemid"].ToString() + "'");
                    if (comid.Rows[0]["status"].ToString() == "Product Received From Company")
                    {
                        ListViewItem li;
                        li = lvcompany.Items.Add(dt11.Rows[i]["sendtocompanyID"].ToString());
                        li.SubItems.Add(dt11.Rows[i]["complainID"].ToString());
                        li.SubItems.Add(dt11.Rows[i]["serialno"].ToString());
                        li.SubItems.Add(dt11.Rows[i]["itemname"].ToString());
                        li.SubItems.Add(dt11.Rows[i]["remarks"].ToString());
                        li.SubItems.Add(dt11.Rows[i]["transportdetails"].ToString());
                        li.SubItems.Add(Convert.ToDateTime(dt11.Rows[i]["senddate"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add(dt11.Rows[i]["newserialno"].ToString());
                        li.SubItems.Add(dt11.Rows[i]["itemid"].ToString());
                        li.ForeColor = Color.Black;
                    }
                    else
                    {
                        ListViewItem li;
                        li = lvcompany.Items.Add(dt11.Rows[i]["sendtocompanyID"].ToString());
                        li.SubItems.Add(dt11.Rows[i]["complainID"].ToString());
                        li.SubItems.Add(dt11.Rows[i]["serialno"].ToString());
                        li.SubItems.Add(dt11.Rows[i]["itemname"].ToString());
                        li.SubItems.Add(dt11.Rows[i]["remarks"].ToString());
                        li.SubItems.Add(dt11.Rows[i]["transportdetails"].ToString());
                        li.SubItems.Add(Convert.ToDateTime(dt11.Rows[i]["senddate"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add(dt11.Rows[i]["newserialno"].ToString());
                        li.SubItems.Add(dt11.Rows[i]["itemid"].ToString());
                        li.ForeColor = Color.Red;
                    }
                }
            }
            btnSubmit.Text = "Update";
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Delete Details?", "Received From Company Details", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    bool exists = false;
                    this.Enabled = false;
                    DataTable dt = conn.getdataset("select * from tblitemcomplainmaster where isactive=1 and complainID='" + cmbComplainId.Text + "'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["status"].ToString() != "Product Received From Company")
                        {
                            exists = true;
                        }
                    }
                    if (!exists)
                    {
                        conn.execute("Update tblreceivefromcompany set isactive=0 where id='" + lblreceiveid.Text + "'");
                        conn.execute("Update tblitemreceivefromcompany set isactive=0 where receivefromcompanyid='" + lblreceiveid.Text + "'");
                        conn.execute("Update tblcomplainmaster set status='" + "Send To Company" + "' where isactive=1 and complainID='" + cmbComplainId.Text + "' and srno='" + cmbSerialNo.Text + "'");
                        clearall();
                        MessageBox.Show("Delete Successfully");
                    }
                    else
                    {
                        MessageBox.Show("This Complain is Alaready in Process You Can't Delete");
                        dtpDate.Focus();
                    }
                }
            }
            catch
            {
            }
            finally
            {
                this.Enabled = true;
            }
        }

        private void lvcompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Delete Item?", "Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    string complainid = lvcompany.Items[lvcompany.FocusedItem.Index].SubItems[1].Text;
                    string serial = lvcompany.Items[lvcompany.FocusedItem.Index].SubItems[2].Text;
                    string itemid = lvcompany.Items[lvcompany.FocusedItem.Index].SubItems[8].Text;
                    DataTable dt = conn.getdataset("select * from tblitemcomplainmaster where isactive=1 and complainID='" + complainid + "' and serialno='" + serial + "' and itemid='" + itemid + "'");
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["status"].ToString() == "Product Received From Company" || dt.Rows[0]["status"].ToString() == "Send To Company")
                        {
                            lvcompany.Items[lvcompany.FocusedItem.Index].Remove();
                            // conn.execute("Update tblitemcomplainmaster set status='" + "Send To Company" + "' where isactive=1 and complainID='" + complainid + "' and serialno='" + serial + "' and itemid='" + itemid + "'");
                            cmbSerialNo.Focus();
                        }
                        else
                        {
                            MessageBox.Show("This Item's Complain is Already In Process You Can't Delete");
                            cmbSerialNo.Focus();
                        }
                    }
                }
                
            }
        }

        private void cmbscno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbscno.Items.Count; i++)
                {
                    s = cmbscno.GetItemText(cmbscno.Items[i]);
                    if (s == cmbscno.Text)
                    {
                        inList = true;
                        cmbscno.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbscno.Text = "";
                }

                if (cmbscno.Text != "")
                {
                    bindserialnobysendtocompanyid();
                    cmbSerialNo.Focus();
                    // txtpono.Focus();
                }
                else
                {
                    MessageBox.Show("Please Select Send To Company No");
                    cmbscno.Focus();
                }
            }
        }

        private void cmbscno_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbscno.SelectedIndex = 0;
                cmbscno.DroppedDown = true;
            }
            catch
            {
            }
        }

        private void cmbscno_Leave(object sender, EventArgs e)
        {
            cmbscno.Text = s;
        }

        private void cmbscno_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbscno.Items.Count; i++)
                {
                    s = cmbscno.GetItemText(cmbscno.Items[i]);
                    if (s == cmbscno.Text)
                    {
                        inList = true;
                        cmbscno.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbscno.Text = "";
                }
                //   bindcomplainidbyserialno();
            }
            catch (Exception excp)
            {
            }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Print?", "Print", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    if (lvcompany.Items.Count > 0)
                    {
                        prn.execute("delete from printing");
                        int j = 1;
                        for (int i = 0; i < lvcompany.Items.Count; i++)
                        {
                            DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1 and CompanyID='" + Master.companyId + "' ");
                            string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25)VALUES";
                            qry += "('" + dt1.Rows[0]["CompanyName"].ToString() + "','" + dt1.Rows[0]["SubName"].ToString() + "','" + dt1.Rows[0]["Address"].ToString() + "','" + dt1.Rows[0]["Address2"].ToString() + "','" + dt1.Rows[0]["City"].ToString() + "','" + dt1.Rows[0]["State"].ToString() + "','" + dt1.Rows[0]["Country"].ToString() + "','" + dt1.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Mobile"].ToString() + "','" + dt1.Rows[0]["Email"].ToString() + "','" + dt1.Rows[0]["CSTNo"].ToString() + "','" + dtpDate.Text + "','" + cmbSupplierName.Text + "','" + txtRemarks.Text + "','" + cmbSupplierName.Text + "','" + lvcompany.Items[i].SubItems[0].Text + "','" + lvcompany.Items[i].SubItems[1].Text + "','" + lvcompany.Items[i].SubItems[2].Text + "','" + lvcompany.Items[i].SubItems[3].Text + "','" + lvcompany.Items[i].SubItems[4].Text + "','" + lvcompany.Items[i].SubItems[5].Text + "','" + lvcompany.Items[i].SubItems[6].Text + "','" + lvcompany.Items[i].SubItems[7].Text + "','" + dt1.Rows[0]["Website"].ToString() +"','"+j+++ "')";
                            prn.execute(qry);
                        }
                    }
                }
            }
            catch
            {
            }
        }
    }

}
