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
    public partial class frmSendToCustomer : Form
    {
        private Master master;
        private TabControl tabControl;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        Printing prn = new Printing();
        public frmSendToCustomer()
        {
            InitializeComponent();
        }

        public frmSendToCustomer(Master master, TabControl tabControl)
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
        public void bindcomplainid()
        {
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
        }
        public void bindserialno()
        {
            try
            {
                cmbSerialNo.DataSource = null;
                string qry = "";
                qry = "select complainID,serialno from tblitemcomplainmaster where isactive=1 and status='Product Received From Company' order by complainID";
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
        public void bindnewserialno()
        {
            try
            {
                cmbSerialNo.DataSource = null;
                string qry = "";
                qry = "select complainID,newserialno from tblitemreceivefromcompany where isactive=1 and newserialno!='' and complainID='" + cmbComplainId.Text + "'";
                SqlCommand cmd1 = new SqlCommand(qry, con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                cmbSerialNo.ValueMember = "complainID";
                cmbSerialNo.DisplayMember = "newserialno";
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
                if (rdbNew.Checked == true)
                {
                    string qry = "";
                    qry = "select DISTINCT complainID from tblitemcomplainmaster where isactive=1 and status='Product Received From Company' and serialno='" + oldserail + "'";
                    SqlCommand cmd1 = new SqlCommand(qry, con);
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    sda1.Fill(dt1);
                    cmbComplainId.ValueMember = "complainID";
                    cmbComplainId.DisplayMember = "complainID";
                    cmbComplainId.DataSource = dt1;
                }
                else
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
                qry = "select complainID,serialno from tblitemcomplainmaster where isactive=1 and status='Product Received From Company' and complainid='" + cmbComplainId.SelectedValue + "'";
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
                cmbComplainId.DataSource = null;
                if (rdbNew.Checked == true)
                {
                    oldserail = conn.ExecuteScalar("select serialno from tblitemreceivefromcompany where isactive=1 and newserialno='" + cmbSerialNo.Text + "'");
                }
                else
                {
                    oldserail = cmbSerialNo.Text;
                }
                string qry = "";
                qry = "select DISTINCT complainID from tblitemcomplainmaster where isactive=1 and status='Product Sent To Customer' and serialno='" + oldserail + "'";
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
                qry = "select complainID,serialno from tblitemcomplainmaster where isactive=1 and status='Product Sent To Customer' and complainid='" + cmbComplainId.SelectedValue + "'";
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
        int cnt = 0;
        OleDbSettings ods = new OleDbSettings();
        private void frmSendToCustomer_Load(object sender, EventArgs e)
        {
            try
            {
                if (cnt == 0)
                {
                    DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
                    dtdate.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                    dtdate.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                    dtdate.CustomFormat = Master.dateformate;
                    lvcompany.Columns.Add("complainID", 0, HorizontalAlignment.Left);
                    lvcompany.Columns.Add("Item Name", 250, HorizontalAlignment.Left);
                    lvcompany.Columns.Add("Description", 150, HorizontalAlignment.Left);
                    lvcompany.Columns.Add("Serial No", 150, HorizontalAlignment.Left);
                    lvcompany.Columns.Add("Replacement Type", 100, HorizontalAlignment.Left);
                    lvcompany.Columns.Add("New Serial No", 150, HorizontalAlignment.Left);
                    lvcompany.Columns.Add("itemid", 0, HorizontalAlignment.Left);
                    bindcomplainid();
                    bindserialno();
                    bindcustomer();
                    getcompanyid();
                    this.ActiveControl = dtdate;
                }
            }
            catch
            {
            }
        }
        public void bindcustomer()
        {
            try
            {
                string qry = "";
                DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='99'");
                string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=99 or GroupID='" + groupid + "') order by AccountName";
                SqlCommand cmd1 = new SqlCommand(qry, con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                cmbCustomerName.ValueMember = "ClientID";
                cmbCustomerName.DisplayMember = "AccountName";
                cmbCustomerName.DataSource = dt1;
                cmbCustomerName.SelectedIndex = -1;
            }
            catch
            {
            }
        }
        private void dtdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbComplainId.Focus();
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
        public static string oldserail;
        public void binddata()
        {
            try
            {
                if (rdbNew.Checked == true)
                {
                    oldserail = conn.ExecuteScalar("select serialno from tblitemreceivefromcompany where isactive=1 and newserialno='" + cmbSerialNo.Text + "'");
                    //  DataTable dt = conn.getdataset("select * from tblcomplainmaster where isactive=1 and complainid='" + cmbComplainId.Text + "' and srno='" + oldserail + "'");
                    DataTable dt = conn.getdataset("select c.customerid,c.customername,ci.replacementtype from tblcomplainmaster c inner join tblitemcomplainmaster ci on c.id=ci.complainID where c.isactive=1 and ci.isactive=1 and ci.ComplainID='" + cmbComplainId.Text + "' and ci.serialno='" + oldserail + "'");
                    if (dt.Rows.Count > 0)
                    {
                        cmbCustomerName.Text = dt.Rows[0]["customername"].ToString();
                        cmbCustomerName.Enabled = false;
                        //  bindcusid();
                        txtCustId.Text = dt.Rows[0]["customerid"].ToString();
                        cmbReplacementType.Text = dt.Rows[0]["replacementtype"].ToString();
                        cmbReplacementType.Enabled = false;
                    }
                }
                else
                {
                    //  DataTable dt = conn.getdataset("select * from tblcomplainmaster where isactive=1 and complainid='" + cmbComplainId.Text + "' and srno='" + cmbSerialNo.Text + "'");
                    DataTable dt = conn.getdataset("select c.customerid,c.customername,ci.replacementtype from tblcomplainmaster c inner join tblitemcomplainmaster ci on c.id=ci.complainID where c.isactive=1 and ci.isactive=1 and ci.ComplainID='" + cmbComplainId.Text + "' and ci.serialno='" + cmbSerialNo.Text + "'");
                    if (dt.Rows.Count > 0)
                    {
                        cmbCustomerName.Text = dt.Rows[0]["customername"].ToString();
                        cmbCustomerName.Enabled = false;
                        //bindcusid();
                        txtCustId.Text = dt.Rows[0]["customerid"].ToString();
                        cmbReplacementType.Text = dt.Rows[0]["replacementtype"].ToString();
                        cmbReplacementType.Enabled = false;
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
                        bindcomplainidbyserialno1();
                    }
                    else
                    {
                        bindcomplainidbyserialno();
                    }
                    binddata();
                    btnadd.Focus();
                    txtQty.Text = "1";
                    // txtpono.Focus();
                }
                else
                {
                    MessageBox.Show("Please Select Serial No");
                    cmbSerialNo.Focus();
                }

            }
        }
        public void bindcusid()
        {
            try
            {
                string dt = conn.ExecuteScalar("select ClientID from ClientMaster where isactive=1 and AccountName='" + cmbCustomerName.Text + "'");
                txtCustId.Text = dt;
            }
            catch
            {
            }
        }
        private void cmbCustomerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbCustomerName.Items.Count; i++)
                {
                    s = cmbCustomerName.GetItemText(cmbCustomerName.Items[i]);
                    if (s == cmbCustomerName.Text)
                    {
                        inList = true;
                        cmbCustomerName.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbCustomerName.Text = "";
                }

                if (cmbCustomerName.Text != "")
                {
                    cmbReplacementType.Focus();
                    bindcusid();
                    // txtpono.Focus();
                }
                else
                {
                    MessageBox.Show("Please Select Supplier Name");
                    cmbCustomerName.Focus();
                }

            }
        }

        private void txtCustId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbReplacementType.Focus();
            }
        }

        private void cmbReplacementType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbReplacementType.Items.Count; i++)
                {
                    s = cmbReplacementType.GetItemText(cmbReplacementType.Items[i]);
                    if (s == cmbReplacementType.Text)
                    {
                        inList = true;
                        cmbReplacementType.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbReplacementType.Text = "";
                }

                if (cmbReplacementType.Text != "")
                {
                    btnadd.Focus();
                    // txtpono.Focus();
                }
                else
                {
                    MessageBox.Show("Please Select Supplier Name");
                    cmbReplacementType.Focus();
                }

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
            //   cmbComplainId.Text = s;
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
            //  cmbSerialNo.Text = s;
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

        private void cmbCustomerName_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbCustomerName.SelectedIndex = 0;
                cmbCustomerName.DroppedDown = true;
            }
            catch
            {
            }

        }

        private void cmbCustomerName_Leave(object sender, EventArgs e)
        {
            // cmbCustomerName.Text = s;
        }

        private void cmbCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbCustomerName.Items.Count; i++)
                {
                    s = cmbCustomerName.GetItemText(cmbCustomerName.Items[i]);
                    if (s == cmbCustomerName.Text)
                    {
                        inList = true;
                        cmbCustomerName.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbCustomerName.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void cmbReplacementType_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbReplacementType.SelectedIndex = 0;
                cmbReplacementType.DroppedDown = true;
            }
            catch
            {
            }
        }

        private void cmbReplacementType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbReplacementType.Items.Count; i++)
                {
                    s = cmbReplacementType.GetItemText(cmbReplacementType.Items[i]);
                    if (s == cmbReplacementType.Text)
                    {
                        inList = true;
                        cmbReplacementType.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbReplacementType.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void cmbReplacementType_Leave(object sender, EventArgs e)
        {
            //  cmbReplacementType.Text = s;
        }
        ListViewItem li;
        string srno;
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
        private void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSubmit.Text == "Update")
                {
                    bool exists = false;
                    foreach (ListViewItem item in lvcompany.Items)
                    {
                        for (int b = 0; b < item.SubItems.Count; b++)
                        {
                            if (rdbNew.Checked == true)
                            {
                                srno = item.SubItems[5].Text;
                            }
                            else
                            {
                                srno = item.SubItems[3].Text;
                            }
                            if (cmbSerialNo.Text == srno)
                            {
                                exists = true;
                            }
                        }
                    }
                    if (!exists)
                    {
                        if (rdbNew.Checked == true)
                        {
                            DataTable dt = conn.getdataset("select c.*,r.newserialno from tblitemcomplainmaster c inner join tblitemreceivefromcompany r on c.serialno=r.serialno where c.isactive=1 and r.isactive=1 and c.serialno='" + oldserail + "' and c.status='Product Sent To Customer'");
                            if (dt.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    li = lvcompany.Items.Add(cmbComplainId.Text);
                                    li.SubItems.Add(dt.Rows[i]["Itemname"].ToString());
                                    li.SubItems.Add(dt.Rows[i]["description"].ToString());
                                    li.SubItems.Add(dt.Rows[i]["serialno"].ToString());
                                    li.SubItems.Add(dt.Rows[i]["replacementtype"].ToString());
                                    li.SubItems.Add(dt.Rows[i]["newserialno"].ToString());
                                    string itemid = conn.ExecuteScalar("select ProductID from ProductMaster where isactive=1 and Product_Name='" + dt.Rows[i]["Itemname"].ToString() + "'");
                                    li.SubItems.Add(itemid);
                                }
                            }
                        }
                        else
                        {
                            DataTable dt = conn.getdataset("select c.*,r.newserialno from tblitemcomplainmaster c inner join tblitemreceivefromcompany r on c.serialno=r.serialno where c.isactive=1 and r.isactive=1 and c.serialno='" + cmbSerialNo.Text + "' and c.status='Product Sent To Customer'");
                            if (dt.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    li = lvcompany.Items.Add(cmbComplainId.Text);
                                    li.SubItems.Add(dt.Rows[i]["Itemname"].ToString());
                                    li.SubItems.Add(dt.Rows[i]["description"].ToString());
                                    li.SubItems.Add(dt.Rows[i]["serialno"].ToString());
                                    li.SubItems.Add(dt.Rows[i]["replacementtype"].ToString());
                                    li.SubItems.Add(dt.Rows[i]["newserialno"].ToString());
                                    string itemid = conn.ExecuteScalar("select ProductID from ProductMaster where isactive=1 and Product_Name='" + dt.Rows[i]["Itemname"].ToString() + "'");
                                    li.SubItems.Add(itemid);
                                }
                            }
                        }
                        cmbComplainId.Enabled = false;
                        cmbSerialNo.Focus();
                    }
                    else
                    {
                        MessageBox.Show("This Item's Complain is Already in Process");
                        cmbSerialNo.Focus();
                    }
                }
                else
                {
                    bool exists = false;
                    foreach (ListViewItem item in lvcompany.Items)
                    {
                        for (int b = 0; b < item.SubItems.Count; b++)
                        {
                            if (rdbNew.Checked == true)
                            {
                                srno = item.SubItems[5].Text;
                            }
                            else
                            {
                                srno = item.SubItems[3].Text;
                            }
                            if (cmbSerialNo.Text == srno)
                            {
                                exists = true;
                            }
                        }
                    }
                    if (!exists)
                    {
                        if (rdbNew.Checked == true)
                        {
                            DataTable dt = conn.getdataset("select c.*,r.newserialno from tblitemcomplainmaster c inner join tblitemreceivefromcompany r on c.serialno=r.serialno where c.isactive=1 and r.isactive=1 and c.serialno='" + oldserail + "' and c.status='Product Received From Company'");
                            if (dt.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    li = lvcompany.Items.Add(cmbComplainId.Text);
                                    li.SubItems.Add(dt.Rows[i]["Itemname"].ToString());
                                    li.SubItems.Add(dt.Rows[i]["description"].ToString());
                                    li.SubItems.Add(dt.Rows[i]["serialno"].ToString());
                                    li.SubItems.Add(dt.Rows[i]["replacementtype"].ToString());
                                    li.SubItems.Add(dt.Rows[i]["newserialno"].ToString());
                                    string itemid = conn.ExecuteScalar("select ProductID from ProductMaster where isactive=1 and Product_Name='" + dt.Rows[i]["Itemname"].ToString() + "'");
                                    li.SubItems.Add(itemid);
                                }
                            }
                        }
                        else
                        {
                            DataTable dt = conn.getdataset("select c.*,r.newserialno from tblitemcomplainmaster c inner join tblitemreceivefromcompany r on c.serialno=r.serialno where c.isactive=1 and r.isactive=1 and c.serialno='" + cmbSerialNo.Text + "' and c.status='Product Received From Company'");
                            if (dt.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    li = lvcompany.Items.Add(cmbComplainId.Text);
                                    li.SubItems.Add(dt.Rows[i]["Itemname"].ToString());
                                    li.SubItems.Add(dt.Rows[i]["description"].ToString());
                                    li.SubItems.Add(dt.Rows[i]["serialno"].ToString());
                                    li.SubItems.Add(dt.Rows[i]["replacementtype"].ToString());
                                    li.SubItems.Add(dt.Rows[i]["newserialno"].ToString());
                                    string itemid = conn.ExecuteScalar("select ProductID from ProductMaster where isactive=1 and Product_Name='" + dt.Rows[i]["Itemname"].ToString() + "'");
                                    li.SubItems.Add(itemid);
                                }
                            }
                        }
                        cmbComplainId.Enabled = false;
                        cmbSerialNo.Focus();
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
        public void enterdata()
        {
            try
            {
                if (btnSubmit.Text == "Update")
                {
                    if (lvcompany.Items.Count > 0)
                    {
                        string s = "Product Sent To Customer";
                        string redio;
                        if (rdbNew.Checked == true)
                        {
                            redio = "New Customer";
                        }
                        else
                        {
                            redio = "Old Customer";
                        }
                        DataTable final = conn.getdataset("select * from tblitemsendtocustomer where isactive=1 and sendtocustomerid='" + lblsendtocustumarid.Text + "'");
                        DataTable countdt = new DataTable();
                        countdt.Columns.Add("id", typeof(string));
                        for (int i = 0; i < final.Rows.Count; i++)
                        {
                            for (int j = 0; j < lvcompany.Items.Count; j++)
                            {
                                if (final.Rows[i]["complainID"].ToString() == lvcompany.Items[j].SubItems[0].Text && final.Rows[i]["serialno"].ToString() == lvcompany.Items[j].SubItems[3].Text && final.Rows[i]["itemid"].ToString() == lvcompany.Items[j].SubItems[6].Text)
                                {
                                    //conn.execute("INSERT INTO [dbo].[tblitemreceivefromcompany]([receivefromcompanyid],[sendtocompanyid],[complainID],[serialno],[itemname],[remarks],[transportdetails],[senddate],[newserialno],[itemid],[isactive])VALUES('" + lblreceiveid.Text + "','" + lvcompany.Items[i].SubItems[0].Text + "','" + lvcompany.Items[i].SubItems[1].Text + "','" + lvcompany.Items[i].SubItems[2].Text + "','" + lvcompany.Items[i].SubItems[3].Text + "','" + lvcompany.Items[i].SubItems[4].Text + "','" + lvcompany.Items[i].SubItems[5].Text + "','" + lvcompany.Items[i].SubItems[6].Text + "','" + lvcompany.Items[i].SubItems[7].Text + "','" + lvcompany.Items[i].SubItems[8].Text + "','" + "1" + "')");
                                    conn.execute("Update tblitemsendtocustomer set sendtocustomerid='" + lblsendtocustumarid.Text + "',complainID='" + lvcompany.Items[i].SubItems[0].Text + "',itemname='" + lvcompany.Items[i].SubItems[1].Text + "',description='" + lvcompany.Items[i].SubItems[2].Text + "',serialno='" + lvcompany.Items[i].SubItems[3].Text + "',replacementtype='" + lvcompany.Items[i].SubItems[4].Text + "',newserialno='" + lvcompany.Items[i].SubItems[5].Text + "',itemid='" + lvcompany.Items[i].SubItems[6].Text + "' where complainID='" + lvcompany.Items[i].SubItems[0].Text + "' and serialno='" + lvcompany.Items[i].SubItems[3].Text + "' and itemid='" + lvcompany.Items[i].SubItems[6].Text + "'");
                                    conn.execute("Update tblitemcomplainmaster set status='" + "Product Sent To Customer" + "' where isactive=1 and complainID='" + lvcompany.Items[j].SubItems[0].Text + "' and serialno='" + lvcompany.Items[j].SubItems[3].Text + "' and itemid='" + lvcompany.Items[j].SubItems[6].Text + "'");
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

                            DataTable finaldt = conn.getdataset("select * from tblitemsendtocustomer where isactive=1 and id='" + final1.Rows[h]["id"].ToString() + "'");
                            if (finaldt.Rows.Count > 0)
                            {
                                conn.execute("Update tblitemcomplainmaster set status='" + "Product Received From Company" + "' where isactive=1 and complainID='" + finaldt.Rows[0]["complainID"].ToString() + "' and serialno='" + finaldt.Rows[0]["serialno"].ToString() + "' and itemid='" + finaldt.Rows[0]["itemid"].ToString() + "'");
                            }
                            conn.execute("Update tblitemsendtocustomer set isactive=0 where id='" + final1.Rows[h]["id"].ToString() + "'");
                        }
                        //  conn.execute("Update tblitemsendtocustomer set isactive=0 where sendtocustomerid='"+lblsendtocustumarid.Text+"'");
                        conn.execute("Update tblsendtocustomer set complainID='" + cmbComplainId.Text + "',serialno='" + cmbSerialNo.Text + "',customerID='" + txtCustId.Text + "',customername='" + cmbCustomerName.Text + "',replacementtype='" + cmbReplacementType.Text + "',qty='" + txtQty.Text + "',transportdetails='" + txtTransportDetail.Text + "',remarks='" + txtRemarks.Text + "',serialnotype='" + redio + "',date='" + Convert.ToDateTime(dtdate.Text).ToString(Master.dateformate) + "' where ID='" + lblsendtocustumarid.Text + "'");
                        //for (int i = 0; i < lvcompany.Items.Count; i++)
                        //{
                        //    conn.execute("INSERT INTO [dbo].[tblitemsendtocustomer]([sendtocustomerid],[complainID],[itemname],[description],[serialno],[replacementtype],[newserialno],[itemid],[isactive])VALUES('" + lblsendtocustumarid.Text + "','" + lvcompany.Items[i].SubItems[0].Text + "','" + lvcompany.Items[i].SubItems[1].Text + "','" + lvcompany.Items[i].SubItems[2].Text + "','" + lvcompany.Items[i].SubItems[3].Text + "','" + lvcompany.Items[i].SubItems[4].Text + "','" + lvcompany.Items[i].SubItems[5].Text + "','" + lvcompany.Items[i].SubItems[6].Text + "','" + "1" + "')");
                        //    conn.execute("Update tblitemcomplainmaster set status='" + s + "' where isactive=1 and complainID='" + lvcompany.Items[i].SubItems[0].Text + "' and serialno='" + lvcompany.Items[i].SubItems[3].Text + "' and itemid='" + lvcompany.Items[i].SubItems[6].Text + "'");
                        //}
                        
                        MessageBox.Show("Data Updated Successfully.");
                        Print();
                        clearall();
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Item");
                        btnadd.Focus();
                    }
                }
                else
                {
                    if (lvcompany.Items.Count > 0)
                    {
                        string s = "Product Sent To Customer";
                        string redio;
                        if (rdbNew.Checked == true)
                        {
                            redio = "New Customer";
                        }
                        else
                        {
                            redio = "Old Customer";
                        }
                        conn.execute("INSERT INTO [dbo].[tblsendtocustomer]([complainID],[serialno],[customerID],[customername],[replacementtype],[qty],[transportdetails],[remarks],[serialnotype],[date],[isactive])VALUES('" + cmbComplainId.Text + "','" + cmbSerialNo.Text + "','" + txtCustId.Text + "','" + cmbCustomerName.Text + "','" + cmbReplacementType.Text + "','" + txtQty.Text + "','" + txtTransportDetail.Text + "','" + txtRemarks.Text + "','" + redio + "','" + Convert.ToDateTime(dtdate.Text).ToString(Master.dateformate) + "','" + "1" + "')");
                        for (int i = 0; i < lvcompany.Items.Count; i++)
                        {
                            conn.execute("INSERT INTO [dbo].[tblitemsendtocustomer]([sendtocustomerid],[complainID],[itemname],[description],[serialno],[replacementtype],[newserialno],[itemid],[isactive])VALUES('" + lblsendtocustumarid.Text + "','" + lvcompany.Items[i].SubItems[0].Text + "','" + lvcompany.Items[i].SubItems[1].Text + "','" + lvcompany.Items[i].SubItems[2].Text + "','" + lvcompany.Items[i].SubItems[3].Text + "','" + lvcompany.Items[i].SubItems[4].Text + "','" + lvcompany.Items[i].SubItems[5].Text + "','" + lvcompany.Items[i].SubItems[6].Text + "','" + "1" + "')");
                            conn.execute("Update tblitemcomplainmaster set status='" + s + "' where isactive=1 and complainID='" + lvcompany.Items[i].SubItems[0].Text + "' and serialno='" + lvcompany.Items[i].SubItems[3].Text + "' and itemid='" + lvcompany.Items[i].SubItems[6].Text + "'");
                        }
                        
                        MessageBox.Show("Data Inserted Successfully.");
                        Print();
                        clearall();
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Item");
                        btnadd.Focus();
                    }
                }
            }
            catch
            {
            }
        }
        string CompID;
        string comid;
        public void getcompanyid()
        {
            try
            {
                CompID = conn.ExecuteScalar("select max(id) from tblsendtocustomer where isactive=1");
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
                lblsendtocustumarid.Text = comid;
            }
            catch
            {
            }
        }
        public void clearall()
        {
            lvcompany.Items.Clear();
            cmbReplacementType.SelectedIndex = -1;
            cmbSerialNo.SelectedIndex = -1;
            cmbCustomerName.SelectedIndex = -1;
            txtCustId.Text = "";
            txtQty.Text = "";
            txtRemarks.Text = "";
            txtTransportDetail.Text = "";
            dtdate.Focus();
            cmbReplacementType.Enabled = true;
            cmbCustomerName.Enabled = true;
            cmbComplainId.Enabled = true;
            lblsendtocustumarid.Text = "";
            getcompanyid();
            btnSubmit.Text = "Submit";
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            enterdata();
        }

        internal void Update(int p, string iid)
        {
            cnt = 1;
            DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
            dtdate.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            dtdate.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            dtdate.CustomFormat = Master.dateformate;
            lvcompany.Columns.Add("complainID", 0, HorizontalAlignment.Left);
            lvcompany.Columns.Add("Item Name", 250, HorizontalAlignment.Left);
            lvcompany.Columns.Add("Description", 150, HorizontalAlignment.Left);
            lvcompany.Columns.Add("Serial No", 150, HorizontalAlignment.Left);
            lvcompany.Columns.Add("Replacement Type", 100, HorizontalAlignment.Left);
            lvcompany.Columns.Add("New Serial No", 150, HorizontalAlignment.Left);
            lvcompany.Columns.Add("itemid", 0, HorizontalAlignment.Left);
            //bindcomplainid();
            //bindserialno();
            bindcustomer();
            btnSubmit.Text = "Update";
            try
            {
                string qry = "";
                qry = "select DISTINCT complainID from tblitemcomplainmaster where isactive=1 and status='Product Sent To Customer'";
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
            try
            {
                string qry = "";
                qry = "select complainID,serialno from tblitemcomplainmaster where isactive=1 and status='Product Sent To Customer'";
                SqlCommand cmd1 = new SqlCommand(qry, con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                cmbSerialNo.ValueMember = "complainID";
                cmbSerialNo.DisplayMember = "serialno";
                cmbSerialNo.DataSource = dt1;
                // cmbSerialNo.SelectedIndex = -1;
                DataTable dt = conn.getdataset("select * from tblsendtocustomer where isactive=1 and ID='" + iid + "'");
                DataTable dt11 = conn.getdataset("select * from tblitemsendtocustomer where isactive=1 and sendtocustomerid='" + iid + "'");
                //  DataTable comid = conn.getdataset("select * from tblitemcomplainmaster where isactive=1 and ComplainID='" + dt11.Rows[0]["complainID"].ToString() + "'");
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["serialnotype"].ToString() == "Old Customer")
                    {
                        rdbOld.Checked = true;
                    }
                    else
                    {
                        rdbNew.Checked = true;
                    }
                    lblsendtocustumarid.Text = dt.Rows[0]["ID"].ToString();
                    dtdate.Text = Convert.ToDateTime(dt.Rows[0]["Date"].ToString()).ToString(Master.dateformate);
                    //  cmbSerialNo.Text = dt.Rows[0]["serialno"].ToString();
                    cmbCustomerName.Text = dt.Rows[0]["customername"].ToString();
                    cmbCustomerName.Enabled = false;
                    txtCustId.Text = dt.Rows[0]["customerID"].ToString();
                    cmbReplacementType.Text = dt.Rows[0]["replacementtype"].ToString();
                    cmbReplacementType.Enabled = false;
                    txtQty.Text = dt.Rows[0]["qty"].ToString();
                    txtRemarks.Text = dt.Rows[0]["remarks"].ToString();
                    txtTransportDetail.Text = dt.Rows[0]["transportdetails"].ToString();

                }
                if (dt11.Rows.Count > 0)
                {
                    for (int i = 0; i < dt11.Rows.Count; i++)
                    {
                        DataTable comid = conn.getdataset("select * from tblitemcomplainmaster where isactive=1 and ComplainID='" + dt11.Rows[i]["complainID"].ToString() + "' and serialno='" + dt11.Rows[i]["serialno"].ToString() + "' and itemid='" + dt11.Rows[i]["itemid"].ToString() + "'");
                        cmbComplainId.Text = dt11.Rows[i]["complainID"].ToString();
                        if (comid.Rows[0]["status"].ToString() == "Product Sent To Customer")
                        {
                            ListViewItem li;
                            li = lvcompany.Items.Add(dt11.Rows[i]["complainID"].ToString());
                            li.SubItems.Add(dt11.Rows[i]["Itemname"].ToString());
                            li.SubItems.Add(dt11.Rows[i]["description"].ToString());
                            li.SubItems.Add(dt11.Rows[i]["serialno"].ToString());
                            li.SubItems.Add(dt11.Rows[i]["replacementtype"].ToString());
                            li.SubItems.Add(dt11.Rows[i]["newserialno"].ToString());
                            li.SubItems.Add(dt11.Rows[i]["itemid"].ToString());
                            li.ForeColor = Color.Black;
                        }
                        else
                        {
                            ListViewItem li;
                            li = lvcompany.Items.Add(dt11.Rows[i]["complainID"].ToString());
                            li.SubItems.Add(dt11.Rows[i]["Itemname"].ToString());
                            li.SubItems.Add(dt11.Rows[i]["description"].ToString());
                            li.SubItems.Add(dt11.Rows[i]["serialno"].ToString());
                            li.SubItems.Add(dt11.Rows[i]["replacementtype"].ToString());
                            li.SubItems.Add(dt11.Rows[i]["newserialno"].ToString());
                            li.SubItems.Add(dt11.Rows[i]["itemid"].ToString());
                            li.ForeColor = Color.Red;
                        }
                    }
                }

            }
            catch
            {
            }
            this.ActiveControl = dtdate;
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Delete Details?", "Sent To Customer Details", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    bool exists = false;
                    this.Enabled = false;
                    DataTable dt = conn.getdataset("select * from tblitemcomplainmaster where isactive=1 and complainID='" + cmbComplainId.Text + "'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["status"].ToString() != "Product Sent To Customer")
                        {
                            exists = true;
                        }
                    }
                    if (!exists)
                    {
                        conn.execute("Update tblsendtocustomer set isactive=0 where id='" + lblsendtocustumarid.Text + "'");
                        conn.execute("Update tblitemsendtocustomer set isactive=0 where sendtocustomerid='" + lblsendtocustumarid.Text + "'");
                        // conn.execute("Update tblcomplainmaster set status='" + "Product Received From Company" + "' where isactive=1 and complainID='" + cmbComplainId.Text + "' and srno='" + cmbSerialNo.Text + "'");
                        clearall();
                        MessageBox.Show("Delete Successfully");
                    }
                    else
                    {
                        MessageBox.Show("This Complain is Alaready in Process You Can't Delete");
                        dtdate.Focus();
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

        private void rdbNew_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdbNew.Checked == true)
                {
                    if (btnSubmit.Text == "Update")
                    {
                        try
                        {
                            cmbSerialNo.DataSource = null;
                            string qry = "";
                            qry = "select complainID,newserialno from tblitemreceivefromcompany where isactive=1 and newserialno!='' and complainID='" + cmbComplainId.Text + "'";
                            SqlCommand cmd1 = new SqlCommand(qry, con);
                            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                            DataTable dt1 = new DataTable();
                            sda1.Fill(dt1);
                            cmbSerialNo.ValueMember = "complainID";
                            cmbSerialNo.DisplayMember = "newserialno";
                            cmbSerialNo.DataSource = dt1;
                            cmbSerialNo.SelectedIndex = -1;
                            cmbSerialNo.Focus();
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        bindnewserialno();
                        cmbSerialNo.Focus();
                    }
                }
            }
            catch
            {
            }
        }

        private void rdbOld_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdbOld.Checked == true)
                {
                    if (btnSubmit.Text == "Update")
                    {
                        try
                        {
                            cmbSerialNo.DataSource = null;
                            string qry = "";
                            qry = "select complainID,serialno from tblitemcomplainmaster where isactive=1 and status='Product Sent To Customer'";
                            SqlCommand cmd1 = new SqlCommand(qry, con);
                            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                            DataTable dt1 = new DataTable();
                            sda1.Fill(dt1);
                            cmbSerialNo.ValueMember = "complainID";
                            cmbSerialNo.DisplayMember = "serialno";
                            cmbSerialNo.DataSource = dt1;
                            cmbSerialNo.Focus();
                        }
                        catch
                        {
                        }
                    }
                    else
                    {

                        bindserialno();
                        cmbSerialNo.Focus();
                    }
                }
            }
            catch
            {
            }
        }

        private void lvcompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Delete Item?", "Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    string complainid = lvcompany.Items[lvcompany.FocusedItem.Index].SubItems[0].Text;
                    string serial = lvcompany.Items[lvcompany.FocusedItem.Index].SubItems[3].Text;
                    string itemid = lvcompany.Items[lvcompany.FocusedItem.Index].SubItems[6].Text;
                    DataTable dt = conn.getdataset("select * from tblitemcomplainmaster where isactive=1 and complainID='" + complainid + "' and serialno='" + serial + "' and itemid='" + itemid + "'");
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["status"].ToString() == "Product Sent To Customer")
                        {
                            lvcompany.Items[lvcompany.FocusedItem.Index].Remove();
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

        private void btnprint_Click(object sender, EventArgs e)
        {
            
        }
        public void Print()
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
                            DataTable date = conn.getdataset("select c.date from tblcomplainmaster c inner join tblitemcomplainmaster ci on c.id=ci.complainID where c.isactive=1 and ci.isactive=1 and ci.serialno='" + lvcompany.Items[i].SubItems[3].Text + "' and itemname='" + lvcompany.Items[i].SubItems[1].Text + "'");
                            string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29)VALUES";
                            qry += "('" + dt1.Rows[0]["CompanyName"].ToString() + "','" + dt1.Rows[0]["SubName"].ToString() + "','" + dt1.Rows[0]["Address"].ToString() + "','" + dt1.Rows[0]["Address2"].ToString() + "','" + dt1.Rows[0]["City"].ToString() + "','" + dt1.Rows[0]["State"].ToString() + "','" + dt1.Rows[0]["Country"].ToString() + "','" + dt1.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Mobile"].ToString() + "','" + dt1.Rows[0]["Email"].ToString() + "','" + dt1.Rows[0]["CSTNo"].ToString() + "','" + dtdate.Text + "','" + cmbComplainId.Text + "','" + txtCustId.Text + "','" + cmbCustomerName.Text + "','" + cmbReplacementType.Text + "','" + txtQty.Text + "','" + txtTransportDetail.Text + "','" + txtRemarks.Text + "','" + lvcompany.Items[i].SubItems[0].Text + "','" + lvcompany.Items[i].SubItems[1].Text + "','" + lvcompany.Items[i].SubItems[2].Text + "','" + lvcompany.Items[i].SubItems[3].Text + "','" + lvcompany.Items[i].SubItems[4].Text + "','" + lvcompany.Items[i].SubItems[5].Text + "','" + lvcompany.Items[i].SubItems[6].Text + "','" + dt1.Rows[0]["Website"].ToString() + "','" + j++ + "','" + Convert.ToDateTime(date.Rows[0]["date"].ToString()).ToString(Master.dateformate) + "')";
                            prn.execute(qry);
                        }
                    }
                    string reportName = "SendToCustomer";
                    //  string reportName = "Sale";
                    Print popup = new Print(reportName);
                    popup.ShowDialog();
                    popup.Dispose();
                }
            }
            catch
            {
            }
        }
    }
}
