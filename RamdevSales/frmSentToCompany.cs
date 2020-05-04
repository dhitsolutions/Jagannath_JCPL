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
    public partial class frmSentToCompany : Form
    {
        private Master master;
        private TabControl tabControl;
        Printing prn = new Printing();
        int cnt = 0;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        OleDbSettings ods = new OleDbSettings();
        public frmSentToCompany()
        {
            InitializeComponent();
        }
        string comid;
        string CompID;
        public frmSentToCompany(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
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
        public void getcompanyid()
        {
            try
            {
                CompID = conn.ExecuteScalar("select max(id) from tblsendtocompanymaster where isactive=1");
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
                txtComplainId.Text = comid;
            }
            catch
            {
            }
        }
        private void frmSentToCompany_Load(object sender, EventArgs e)
        {
            try
            {
                if (cnt == 0)
                {
                    lvcompany.Columns.Add("Complain ID", 100, HorizontalAlignment.Center);
                    lvcompany.Columns.Add("Serial No", 150, HorizontalAlignment.Center);
                    lvcompany.Columns.Add("Item Name", 300, HorizontalAlignment.Center);
                    lvcompany.Columns.Add("Qty", 70, HorizontalAlignment.Center);
                    lvcompany.Columns.Add("Description", 150, HorizontalAlignment.Center);
                    lvcompany.Columns.Add("Replacemant Type", 150, HorizontalAlignment.Center);
                    lvcompany.Columns.Add("Itemid", 0, HorizontalAlignment.Center);
                    DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
                    dtdate.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                    dtdate.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                    dtdate.CustomFormat = Master.dateformate;
                    bindcustomer();
                    bindcomplainid();
                    bindserialno();
                    getcompanyid();
                    this.ActiveControl = dtdate;
                }
            }
            catch
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
                //  cmbComplainId.DataSource = null;
                string qry = "";
                //qry = "select id,complainid from tblcomplainmaster where isactive=1 order by id";
                //qry = "select DISTINCT complainid from tblcomplainmaster where isactive=1";
                qry = "SELECT DISTINCT complainID from tblitemcomplainmaster where isactive=1 and status='Complain Received'";
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
                // cmbSerialNo.DataSource = null;
                string qry = "";
                qry = "select complainID,serialno from tblitemcomplainmaster where isactive=1 and status='Complain Received' order by complainID";
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
                //   cmbComplainId.DataSource = null;
                string qry = "";
                qry = "select DISTINCT complainID from tblitemcomplainmaster where isactive=1 and status='Complain Received' and serialno='" + cmbSerialNo.Text + "'";
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
        public void bindcomplainidbyserialno1()
        {
            try
            {
                // cmbComplainId.DataSource = null;
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
                //     cmbSerialNo.DataSource = null;
                string qry = "";
                qry = "select complainID,serialno from tblitemcomplainmaster where isactive=1 and status='Complain Received' and complainID='" + cmbComplainId.SelectedValue + "'";
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
        public void bindserialnobycompanyid1()
        {
            try
            {
                string qry = "";
                qry = "select complainID,serialno from tblitemcomplainmaster where isactive=1 and status='Send To Company' and complainID='" + cmbComplainId.SelectedValue + "'";
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
        public static string s;
        private void cmbSupplierName_Leave(object sender, EventArgs e)
        {
            //   cmbSupplierName.Text = s;
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
        public static string activecontroal;
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
                    cmbComplainId.Focus();
                    // txtpono.Focus();
                }
                else
                {
                    MessageBox.Show("Please Select Customer");
                    cmbSupplierName.Focus();
                }
                //if (cmbcustname.Text != "")
                //{
                //    cmbsaletype.Focus();
                //}
                //else
                //{
                //    MessageBox.Show("Please Select Customer");
                //    cmbcustname.Focus();
                //}
            }
            if (e.KeyCode == Keys.F3)
            {
                var privouscontroal = cmbSupplierName;
                activecontroal = privouscontroal.Name;
                Accountentry client = new Accountentry(this, master, tabControl, activecontroal);

                client.Passed(1);
                //   client.Show();
                master.AddNewTab(client);
            }
            if (e.KeyCode == Keys.F2)
            {
                var privouscontroal = cmbSupplierName;
                activecontroal = privouscontroal.Name;
                string iid = cmbSupplierName.SelectedValue.ToString();

                Accountentry client = new Accountentry(this, master, tabControl, activecontroal);
                client.Update(1, iid);
                client.Passed(1);
                //  client.Show();
                master.AddNewTab(client);
            }
        }

        private void btnAddPartyName_Click(object sender, EventArgs e)
        {
            var privouscontroal = cmbSupplierName;
            activecontroal = privouscontroal.Name;
            Accountentry client = new Accountentry(this, master, tabControl, activecontroal);
            client.Passed(1);
            master.AddNewTab(client);
        }

        private void btnEditPartyName_Click(object sender, EventArgs e)
        {
            if (cmbSupplierName.Text != "" && cmbSupplierName.Text != null)
            {
                var privouscontroal = cmbSupplierName;
                activecontroal = privouscontroal.Name;
                string iid = cmbSupplierName.SelectedValue.ToString();
                Accountentry client = new Accountentry(this, master, tabControl, activecontroal);
                client.Update(1, iid);
                client.Passed(1);
                //  client.Show();
                master.AddNewTab(client);
            }
            else
            {
                MessageBox.Show("Please Select PartyName.");
            }
        }

        private void dtdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbSupplierName.Focus();
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
                    if (btnSave.Text == "Update")
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
                    MessageBox.Show("Please Select Customer");
                    cmbComplainId.Focus();
                }


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

                    if (btnSave.Text == "Update")
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
                    }
                    btnAddToList.Focus();
                    // txtpono.Focus();
                }
                else
                {
                    MessageBox.Show("Please Select Customer");
                    cmbSerialNo.Focus();
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

        private void cmbComplainId_Leave(object sender, EventArgs e)
        {
            //  cmbComplainId.Text = s;
        }


        private void cmbComplainId_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    bool inList = false;
            //    for (int i = 0; i < cmbComplainId.Items.Count; i++)
            //    {
            //        s = cmbComplainId.GetItemText(cmbComplainId.Items[i]);
            //        if (s == cmbComplainId.Text)
            //        {
            //            inList = true;
            //            cmbComplainId.Text = s;
            //            break;
            //        }
            //    }
            //    if (!inList)
            //    {
            //        cmbComplainId.Text = "";
            //    }
            //    //  bindserialbycomplainid();
            //}
            //catch (Exception excp)
            //{
            //}
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
            cmbSerialNo.Text = s;
        }


        private void cmbSerialNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    bool inList = false;
            //    for (int i = 0; i < cmbSerialNo.Items.Count; i++)
            //    {
            //        s = cmbSerialNo.GetItemText(cmbSerialNo.Items[i]);
            //        if (s == cmbSerialNo.Text)
            //        {
            //            inList = true;
            //            cmbSerialNo.Text = s;
            //            break;
            //        }
            //    }
            //    if (!inList)
            //    {
            //        cmbSerialNo.Text = "";
            //    }
            //    //   bindcomplainidbyserialno();
            //}
            //catch (Exception excp)
            //{
            //}
        }
        ListViewItem li;
        public void clearall()
        {
            cmbSupplierName.SelectedIndex = -1;
            txtTransportDetail.Text = "";
            txtRemarks.Text = "";
            lvcompany.Items.Clear();
            bindserialno();
            bindcomplainid();
            getcompanyid();
            btnSave.Text = "Submit";
            bindcomplainid();
            bindserialno();
        }
        public void clear()
        {
            if (btnSave.Text != "Update")
            {
                bindserialno();
                bindcomplainid();
            }
            cmbComplainId.Focus();
        }
        private void btnAddToList_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cmbComplainId.Text) && !string.IsNullOrEmpty(cmbSerialNo.Text) && !string.IsNullOrEmpty(cmbSupplierName.Text))
                {
                    DataTable dt = conn.getdataset("select * from tblitemcomplainmaster where isactive=1 and complainID='" + cmbComplainId.Text + "' and serialno='" + cmbSerialNo.Text + "'");
                    if (dt.Rows.Count > 0)
                    {
                        bool exists = false;
                        foreach (ListViewItem item in lvcompany.Items)
                        {
                            for (int b = 0; b < item.SubItems.Count; b++)
                            {
                                string srno = item.SubItems[1].Text;
                                if (cmbSerialNo.Text == srno)
                                {
                                    exists = true;
                                }
                            }
                        }
                        if (!exists)
                        {
                            li = lvcompany.Items.Add(dt.Rows[0]["complainID"].ToString());
                            li.SubItems.Add(dt.Rows[0]["serialno"].ToString());
                            li.SubItems.Add(dt.Rows[0]["Itemname"].ToString());
                            li.SubItems.Add(dt.Rows[0]["qty"].ToString());
                            li.SubItems.Add(dt.Rows[0]["description"].ToString());
                            li.SubItems.Add(dt.Rows[0]["replacementtype"].ToString());
                            string itemid = conn.ExecuteScalar("select ProductID from ProductMaster where isactive=1 and Product_Name='" + dt.Rows[0]["Itemname"].ToString() + "'");
                            li.SubItems.Add(itemid);
                            clear();
                        }
                        else
                        {
                            MessageBox.Show("This Item's Complain is Already in Process");
                            cmbSerialNo.Focus();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please Fill All Details");
                    cmbSupplierName.Focus();
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
                if (btnSave.Text == "Update")
                {
                    if (lvcompany.Items.Count > 0)
                    {

                        DataTable final = conn.getdataset("select * from tblsendtocompanyitemmaster where isactive=1 and sendtocompanyID='" + txtComplainId.Text + "'");
                        // string[] count = new string[final.Rows.Count];
                        //   string[] complainid = new string[final.Rows.Count];
                        DataTable countdt = new DataTable();
                        countdt.Columns.Add("id", typeof(string));
                        for (int i = 0; i < final.Rows.Count; i++)
                        {
                            for (int j = 0; j < lvcompany.Items.Count; j++)
                            {
                                // if (final.Rows.Count > lvcompany.Items.Count)
                                // {
                                if (final.Rows[i]["complainID"].ToString() == lvcompany.Items[j].SubItems[0].Text && final.Rows[i]["serialno"].ToString() == lvcompany.Items[j].SubItems[1].Text && final.Rows[i]["itemid"].ToString() == lvcompany.Items[j].SubItems[6].Text)
                                {
                                    //conn.execute("INSERT INTO [dbo].[tblsendtocompanyitemmaster]([sendtocompanyID],[complainID],[serialno],[itemname],[qty],[description],[replacementtype],[itemid],[isactive])VALUES('" + txtComplainId.Text + "','" + lvcompany.Items[i].SubItems[0].Text + "','" + lvcompany.Items[i].SubItems[1].Text + "','" + lvcompany.Items[i].SubItems[2].Text + "','" + lvcompany.Items[i].SubItems[3].Text + "','" + lvcompany.Items[i].SubItems[4].Text + "','" + lvcompany.Items[i].SubItems[5].Text + "','" + lvcompany.Items[i].SubItems[6].Text + "','" + "1" + "')");
                                    conn.execute("Update tblsendtocompanyitemmaster set sendtocompanyID='" + txtComplainId.Text + "',complainID='" + lvcompany.Items[j].SubItems[0].Text + "',serialno='" + lvcompany.Items[j].SubItems[1].Text + "',itemname='" + lvcompany.Items[j].SubItems[2].Text + "',qty='" + lvcompany.Items[j].SubItems[3].Text + "',description='" + lvcompany.Items[j].SubItems[4].Text + "',replacementtype='" + lvcompany.Items[j].SubItems[5].Text + "',itemid='" + lvcompany.Items[j].SubItems[6].Text + "' where complainID='" + lvcompany.Items[j].SubItems[0].Text + "' and serialno='" + lvcompany.Items[j].SubItems[1].Text + "' and itemid='" + lvcompany.Items[j].SubItems[6].Text + "'");
                                    conn.execute("Update tblitemcomplainmaster set status='" + "Send To Company" + "' where isactive=1 and complainID='" + lvcompany.Items[j].SubItems[0].Text + "' and serialno='" + lvcompany.Items[j].SubItems[1].Text + "' and itemid='" + lvcompany.Items[j].SubItems[6].Text + "'");
                                    countdt.Rows.Add(final.Rows[i]["id"].ToString());
                                    //  complainid[j] = final.Rows[i]["complainID"].ToString();
                                }
                                //else
                                //{
                                //    conn.execute("Update tblsendtocompanyitemmaster set isactive=0 where complainID='" + lvcompany.Items[j].SubItems[0].Text + "' and serialno='" + lvcompany.Items[j].SubItems[1].Text + "' and itemid='" + lvcompany.Items[j].SubItems[6].Text + "'");
                                //    conn.execute("Update tblitemcomplainmaster set status='" + "Complain Received" + "' where isactive=1 and complainID='" + lvcompany.Items[i].SubItems[0].Text + "' and serialno='" + lvcompany.Items[i].SubItems[1].Text + "' and itemid='" + lvcompany.Items[i].SubItems[6].Text + "'");
                                //}
                                // }
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

                            DataTable finaldt = conn.getdataset("select * from tblsendtocompanyitemmaster where isactive=1 and id='" + final1.Rows[h]["id"].ToString() + "'");
                            if (finaldt.Rows.Count > 0)
                            {
                                conn.execute("Update tblitemcomplainmaster set status='" + "Complain Received" + "' where isactive=1 and complainID='" + finaldt.Rows[0]["complainID"].ToString() + "' and serialno='" + finaldt.Rows[0]["serialno"].ToString() + "' and itemid='" + finaldt.Rows[0]["itemid"].ToString() + "'");
                            }
                            conn.execute("Update tblsendtocompanyitemmaster set isactive=0 where id='" + final1.Rows[h]["id"].ToString() + "'");
                        }
                        #region
                        //for (int i = 0; i < count.Length; i++)
                        //{
                        //    for (int k = 0; k < final.Rows.Count; k++)
                        //    {
                        //        if (final.Rows[k]["id"].ToString() == count[i])
                        //        {
                        //            break;
                        //        }
                        //        //DataTable dt = conn.getdataset("select * from tblsendtocompanyitemmaster where isactive=1 and id='" + count[i] + "'");
                        //        //if (dt.Rows.Count <= 0)
                        //        //{
                        //        //    conn.execute("Update tblsendtocompanyitemmaster set isactive=0 where id='" + final.Rows[k]["id"].ToString() + "'");
                        //        //    //    conn.execute("Update tblitemcomplainmaster set status='" + "Complain Received" + "' where isactive=1 and complainID='" + lvcompany.Items[i].SubItems[0].Text + "' and serialno='" + lvcompany.Items[i].SubItems[1].Text + "' and itemid='" + lvcompany.Items[i].SubItems[6].Text + "'");
                        //        //}
                        //    }
                        //}
                        //for (int i = 0; i < complainid.Length; i++)
                        //{
                        //    for (int k = 0; k < count.Length; k++)
                        //    {
                        //        DataTable dt = conn.getdataset("select * from tblsendtocompanyitemmaster where isactive=1 and complainID='"+complainid[k]+"' and id='"+count[k]+"'");
                        //        if (dt.Rows.Count>0)
                        //        {
                        //            conn.execute("Update tblsendtocompanyitemmaster set isactive=0 where id='" + dt.Rows[0]["id"].ToString() + "'");
                        //            //    conn.execute("Update tblitemcomplainmaster set status='" + "Complain Received" + "' where isactive=1 and complainID='" + lvcompany.Items[i].SubItems[0].Text + "' and serialno='" + lvcompany.Items[i].SubItems[1].Text + "' and itemid='" + lvcompany.Items[i].SubItems[6].Text + "'");
                        //        }
                        //    }
                        //}
                        ////string s = "Send To Company";
                        ////conn.execute("Update tblsendtocompanyitemmaster set isactive=0 where sendtocompanyID='" + txtComplainId.Text + "'");
                        ////for (int i = 0; i < lvcompany.Items.Count; i++)
                        ////{
                        ////    conn.execute("INSERT INTO [dbo].[tblsendtocompanyitemmaster]([sendtocompanyID],[complainID],[serialno],[itemname],[qty],[description],[replacementtype],[itemid],[isactive])VALUES('" + txtComplainId.Text + "','" + lvcompany.Items[i].SubItems[0].Text + "','" + lvcompany.Items[i].SubItems[1].Text + "','" + lvcompany.Items[i].SubItems[2].Text + "','" + lvcompany.Items[i].SubItems[3].Text + "','" + lvcompany.Items[i].SubItems[4].Text + "','" + lvcompany.Items[i].SubItems[5].Text + "','" + lvcompany.Items[i].SubItems[6].Text + "','" + "1" + "')");
                        ////    conn.execute("Update tblitemcomplainmaster set status='" + s + "' where isactive=1 and complainID='" + lvcompany.Items[i].SubItems[0].Text + "' and serialno='" + lvcompany.Items[i].SubItems[1].Text + "' and itemid='" + lvcompany.Items[i].SubItems[6].Text + "'");
                        ////}
                        #endregion
                        conn.execute("Update tblsendtocompanymaster set date='" + Convert.ToDateTime(dtdate.Text).ToString(Master.dateformate) + "',supplierID='" + cmbSupplierName.SelectedValue + "',suppliername='" + cmbSupplierName.Text + "',transportdetails='" + txtTransportDetail.Text + "',remarks='" + txtRemarks.Text + "' where id='" + txtComplainId.Text + "'");
                        
                        MessageBox.Show("Data Updated Successfully.");
                        Print();
                        clearall();
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Item");
                        cmbComplainId.Focus();
                    }
                }
                else
                {
                    if (lvcompany.Items.Count > 0)
                    {
                        string s = "Send To Company";
                        conn.execute("INSERT INTO [dbo].[tblsendtocompanymaster]([date],[supplierID],[suppliername],[transportdetails],[remarks],[isactive])VALUES('" + Convert.ToDateTime(dtdate.Text).ToString(Master.dateformate) + "','" + cmbSupplierName.SelectedValue + "','" + cmbSupplierName.Text + "','" + txtTransportDetail.Text + "','" + txtRemarks.Text + "','" + "1" + "')");
                        for (int i = 0; i < lvcompany.Items.Count; i++)
                        {
                            conn.execute("INSERT INTO [dbo].[tblsendtocompanyitemmaster]([sendtocompanyID],[complainID],[serialno],[itemname],[qty],[description],[replacementtype],[itemid],[isactive])VALUES('" + txtComplainId.Text + "','" + lvcompany.Items[i].SubItems[0].Text + "','" + lvcompany.Items[i].SubItems[1].Text + "','" + lvcompany.Items[i].SubItems[2].Text + "','" + lvcompany.Items[i].SubItems[3].Text + "','" + lvcompany.Items[i].SubItems[4].Text + "','" + lvcompany.Items[i].SubItems[5].Text + "','" + lvcompany.Items[i].SubItems[6].Text + "','" + "1" + "')");
                            conn.execute("Update tblitemcomplainmaster set status='" + s + "' where isactive=1 and complainID='" + lvcompany.Items[i].SubItems[0].Text + "' and serialno='" + lvcompany.Items[i].SubItems[1].Text + "' and itemid='" + lvcompany.Items[i].SubItems[6].Text + "'");
                        }
                        
                        MessageBox.Show("Data Inserted Successfully.");
                        Print();
                        clearall();
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Item");
                        cmbComplainId.Focus();
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
        public static DataTable CompareTwoDataTable(DataTable dt1, DataTable dt2)
        {
            dt1.Merge(dt2);
            DataTable d3 = dt2.GetChanges();

            return d3;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            enterdata();
        }

        internal void Update(int p, string iid)
        {
            cnt = 1;
            lvcompany.Columns.Add("Complain ID", 100, HorizontalAlignment.Center);
            lvcompany.Columns.Add("Serial No", 150, HorizontalAlignment.Center);
            lvcompany.Columns.Add("Item Name", 300, HorizontalAlignment.Center);
            lvcompany.Columns.Add("Qty", 70, HorizontalAlignment.Center);
            lvcompany.Columns.Add("Description", 150, HorizontalAlignment.Center);
            lvcompany.Columns.Add("Replacemant Type", 150, HorizontalAlignment.Center);
            lvcompany.Columns.Add("Itemid", 0, HorizontalAlignment.Center);
            DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
            dtdate.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            dtdate.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            dtdate.CustomFormat = Master.dateformate;
            try
            {
                string qry = "";
                //qry = "select id,complainid from tblcomplainmaster where isactive=1 order by id";
                //qry = "select DISTINCT complainid from tblcomplainmaster where isactive=1";
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
            bindcustomer();
            //   bindcomplainid();
            // bindserialno();
            // getcompanyid();
            this.ActiveControl = dtdate;
            DataTable dt = conn.getdataset("select * from tblsendtocompanymaster where isactive=1 and id='" + iid + "'");
            DataTable dt11 = conn.getdataset("select * from tblsendtocompanyitemmaster where isactive=1 and sendtocompanyID='" + iid + "'");

            if (dt.Rows.Count > 0)
            {
                txtComplainId.Text = dt.Rows[0]["id"].ToString();
                dtdate.Text = Convert.ToDateTime(dt.Rows[0]["Date"].ToString()).ToString(Master.dateformate);
                // string clientname = conn.ExecuteScalar("select accountname from clientmaster where isactive=1 and clientid='" + dt.Rows[0]["customerid"].ToString() + "' and isactive=1");
                cmbSupplierName.Text = dt.Rows[0]["suppliername"].ToString();

                txtTransportDetail.Text = dt.Rows[0]["transportdetails"].ToString();
                txtRemarks.Text = dt.Rows[0]["remarks"].ToString();

            }

            if (dt11.Rows.Count > 0)
            {
                for (int i = 0; i < dt11.Rows.Count; i++)
                {
                    DataTable comid = conn.getdataset("select * from tblitemcomplainmaster where isactive=1 and ComplainID='" + dt11.Rows[i]["complainID"].ToString() + "' and serialno='" + dt11.Rows[i]["serialno"].ToString() + "' and itemid='" + dt11.Rows[i]["itemid"].ToString() + "'");
                    if (comid.Rows[0]["status"].ToString() == "Send To Company")
                    {
                        ListViewItem li;
                        li = lvcompany.Items.Add(dt11.Rows[i]["complainID"].ToString());
                        li.SubItems.Add(dt11.Rows[i]["serialno"].ToString());
                        li.SubItems.Add(dt11.Rows[i]["itemname"].ToString());
                        li.SubItems.Add(dt11.Rows[i]["qty"].ToString());
                        li.SubItems.Add(dt11.Rows[i]["description"].ToString());
                        li.SubItems.Add(dt11.Rows[i]["replacementtype"].ToString());
                        li.SubItems.Add(dt11.Rows[i]["itemid"].ToString());
                        li.ForeColor = Color.Black;
                    }
                    else
                    {
                        ListViewItem li;
                        li = lvcompany.Items.Add(dt11.Rows[i]["complainID"].ToString());
                        li.SubItems.Add(dt11.Rows[i]["serialno"].ToString());
                        li.SubItems.Add(dt11.Rows[i]["itemname"].ToString());
                        li.SubItems.Add(dt11.Rows[i]["qty"].ToString());
                        li.SubItems.Add(dt11.Rows[i]["description"].ToString());
                        li.SubItems.Add(dt11.Rows[i]["replacementtype"].ToString());
                        li.SubItems.Add(dt11.Rows[i]["itemid"].ToString());
                        li.ForeColor = Color.Red;
                    }
                }
            }
            btnSave.Text = "Update";
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Delete Details?", "Send To Company Details", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    bool exists = false;
                    this.Enabled = false;
                    DataTable dt = conn.getdataset("select * from tblitemcomplainmaster where isactive=1 and complainID='" + txtComplainId.Text + "'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["status"].ToString() != "Send To Company")
                        {
                            exists = true;
                        }
                    }
                    if (!exists)
                    {
                        conn.execute("Update tblsendtocompanymaster set isactive=0 where id='" + txtComplainId.Text + "'");
                        conn.execute("Update tblsendtocompanyitemmaster set isactive=0 where sendtocompanyID='" + txtComplainId.Text + "'");
                        for (int i = 0; i < lvcompany.Items.Count; i++)
                        {
                            conn.execute("Update tblitemcomplainmaster set status='" + "Complain Received" + "' where isactive=1 and complainID='" + lvcompany.Items[i].SubItems[0].Text + "' and serialno='" + lvcompany.Items[i].SubItems[1].Text + "'");
                        }
                        clearall();
                        MessageBox.Show("Delete Successfully");
                        dtdate.Focus();
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

        private void lvcompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Delete Item?", "Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    string complainid = lvcompany.Items[lvcompany.FocusedItem.Index].SubItems[0].Text;
                    string serial = lvcompany.Items[lvcompany.FocusedItem.Index].SubItems[1].Text;
                    string itemid = lvcompany.Items[lvcompany.FocusedItem.Index].SubItems[6].Text;
                    DataTable dt = conn.getdataset("select * from tblitemcomplainmaster where isactive=1 and complainID='" + complainid + "' and serialno='" + serial + "' and itemid='" + itemid + "'");
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["status"].ToString() == "Send To Company" || dt.Rows[0]["status"].ToString() == "Complain Received")
                        {
                            lvcompany.Items[lvcompany.FocusedItem.Index].Remove();
                            //  conn.execute("Update tblitemcomplainmaster set status='" + "Complain Received" + "' where isactive=1 and complainID='" + complainid + "' and serialno='" + serial + "' and itemid='" + itemid + "'");
                            cmbComplainId.Focus();
                        }
                        else
                        {
                            MessageBox.Show("This Item's Complain is Already In Process You Can't Delete");
                            cmbComplainId.Focus();
                        }
                    }
                }
            }
        }

        private void btnSave_Enter(object sender, EventArgs e)
        {
            btnSave.UseVisualStyleBackColor = false;
            btnSave.BackColor = Color.YellowGreen;
            btnSave.ForeColor = Color.White;
        }

        private void btnSave_Leave(object sender, EventArgs e)
        {
            btnSave.UseVisualStyleBackColor = true;
            btnSave.BackColor = Color.FromArgb(51, 153, 255);
            btnSave.ForeColor = Color.White;
        }

        private void btnSave_MouseEnter(object sender, EventArgs e)
        {
            btnSave.UseVisualStyleBackColor = false;
            btnSave.BackColor = Color.YellowGreen;
            btnSave.ForeColor = Color.White;
        }

        private void btnSave_MouseLeave(object sender, EventArgs e)
        {
            btnSave.UseVisualStyleBackColor = true;
            btnSave.BackColor = Color.FromArgb(51, 153, 255);
            btnSave.ForeColor = Color.White;
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

        private void btnprint_Click(object sender, EventArgs e)
        {
            Print();
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
                            string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26)VALUES";
                            qry += "('" + dt1.Rows[0]["CompanyName"].ToString() + "','" + dt1.Rows[0]["SubName"].ToString() + "','" + dt1.Rows[0]["Address"].ToString() + "','" + dt1.Rows[0]["Address2"].ToString() + "','" + dt1.Rows[0]["City"].ToString() + "','" + dt1.Rows[0]["State"].ToString() + "','" + dt1.Rows[0]["Country"].ToString() + "','" + dt1.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Mobile"].ToString() + "','" + dt1.Rows[0]["Email"].ToString() + "','" + dt1.Rows[0]["CSTNo"].ToString() + "','" + txtComplainId.Text + "','" + txtComplainId.Text + "','" + dtdate.Text + "','" + cmbSupplierName.Text + "','" + txtTransportDetail.Text + "','" + txtRemarks.Text + "','" + lvcompany.Items[i].SubItems[0].Text + "','" + lvcompany.Items[i].SubItems[1].Text + "','" + lvcompany.Items[i].SubItems[2].Text + "','" + lvcompany.Items[i].SubItems[3].Text + "','" + lvcompany.Items[i].SubItems[4].Text + "','" + lvcompany.Items[i].SubItems[5].Text + "','" + lvcompany.Items[i].SubItems[6].Text + "','" + dt1.Rows[0]["Website"].ToString() + "','" + j++ + "')";
                            prn.execute(qry);
                        }
                    }
                    string reportName = "SendToCompany";
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
