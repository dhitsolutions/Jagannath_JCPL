using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

namespace RamdevSales
{
    public partial class frmComplainMasterData : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        private TabControl tabControl;
        private Master master;
        int flag;
        Int32 rowid = -1;
        string CompID;
        string CustomerName = "";
        string CustomerID = "";
        string ItemName = "", ItemId = "";
        string Replacement;
        Int32 rowidofItem = -1;
        string DateComplainReg;
        Int32 rowidForDelete = -1;
        Printing prn = new Printing();
        int cnt = 0;
        string comid;
        OleDbSettings ods = new OleDbSettings();
        public frmComplainMasterData(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }
        public void getcomplainid()
        {
            try
            {
                CompID = conn.ExecuteScalar("select max(id) from tblcomplainmaster where isactive=1");
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
                cmbcustname.ValueMember = "ClientID";
                cmbcustname.DisplayMember = "AccountName";
                cmbcustname.DataSource = dt1;
                cmbcustname.SelectedIndex = -1;
            }
            catch
            {
            }
        }
        public void bindallitem()
        {
            try
            {
                DataTable allitem = new DataTable();
                lvallitem.Items.Clear();
                allitem = conn.getdataset("select ProductMaster.Product_Name from ProductMaster where isactive=1 order by ProductMaster.Product_Name asc");
                for (int i = 0; i < allitem.Rows.Count; i++)
                {
                    ListViewItem li;
                    li = lvallitem.Items.Add(allitem.Rows[i]["Product_Name"].ToString());
                }
            }
            catch
            {
            }
        }
        private void frmComplainMasterData_Load(object sender, EventArgs e)
        {
            try
            {
                if (cnt == 0)
                {
                    DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
                    dtpDate.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                    dtpDate.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                    dtpDate.CustomFormat = Master.dateformate;
                    getcomplainid();
                    bindcustomer();
                    this.ActiveControl = dtpDate;
                    LVFO.Columns.Add("ItemName", 350, HorizontalAlignment.Center);
                    LVFO.Columns.Add("Description", 170, HorizontalAlignment.Center);
                    LVFO.Columns.Add("ReplacementType", 130, HorizontalAlignment.Center);
                    LVFO.Columns.Add("Qty", 70, HorizontalAlignment.Center);
                    LVFO.Columns.Add("SerialNo", 150, HorizontalAlignment.Center);
                    LVFO.Columns.Add("Remark", 170, HorizontalAlignment.Center);
                    LVFO.Columns.Add("Itemid", 0, HorizontalAlignment.Center);
                    LVFO.Columns.Add("ComplainStatus", 0, HorizontalAlignment.Center);
                    lvallitem.Columns.Add("Product Name", 400, HorizontalAlignment.Left);

                }
            }
            catch
            {
            }
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.U))
            {
                //DialogResult dr = MessageBox.Show("Do you want to Update?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (dr == DialogResult.Yes)
                //{
                enterdata();
                //}
                return true;
            }
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
        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }

        }

        private void dtpDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbcustname.Focus();
            }
        }

        private void cmbcustname_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbcustname.SelectedIndex = 0;
                cmbcustname.DroppedDown = true;
            }
            catch
            {
            }
        }
        public static string s;
        private void cmbcustname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbcustname.Items.Count; i++)
                {
                    s = cmbcustname.GetItemText(cmbcustname.Items[i]);
                    if (s == cmbcustname.Text)
                    {
                        inList = true;
                        cmbcustname.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbcustname.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void cmbcustname_Leave(object sender, EventArgs e)
        {
            cmbcustname.Text = s;
        }
        public static string activecontroal;
        private void cmbcustname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbcustname.Items.Count; i++)
                {
                    s = cmbcustname.GetItemText(cmbcustname.Items[i]);
                    if (s == cmbcustname.Text)
                    {
                        inList = true;
                        cmbcustname.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbcustname.Text = "";
                }

                if (cmbcustname.Text != "")
                {
                    cmbItemName.Focus();
                    // txtpono.Focus();
                }
                else
                {
                    MessageBox.Show("Please Select Customer");
                    cmbcustname.Focus();
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
                var privouscontroal = cmbcustname;
                activecontroal = privouscontroal.Name;
                Accountentry client = new Accountentry(this, master, tabControl, activecontroal);

                client.Passed(1);
                //   client.Show();
                master.AddNewTab(client);
            }
            if (e.KeyCode == Keys.F2)
            {
                var privouscontroal = cmbcustname;
                activecontroal = privouscontroal.Name;
                string iid = cmbcustname.SelectedValue.ToString();

                Accountentry client = new Accountentry(this, master, tabControl, activecontroal);
                client.Update(1, iid);
                client.Passed(1);
                //  client.Show();
                master.AddNewTab(client);
            }
        }

        private void btnAddPartyName_Click(object sender, EventArgs e)
        {
            var privouscontroal = cmbcustname;
            activecontroal = privouscontroal.Name;
            Accountentry client = new Accountentry(this, master, tabControl, activecontroal);
            client.Passed(1);
            master.AddNewTab(client);
        }

        private void btnEditPartyName_Click(object sender, EventArgs e)
        {
            if (cmbcustname.Text != "" && cmbcustname.Text != null)
            {
                var privouscontroal = cmbcustname;
                activecontroal = privouscontroal.Name;
                string iid = cmbcustname.SelectedValue.ToString();
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

        private void cmbItemName_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbItemName.BackColor = Color.LightYellow;
                pnlallitem.Visible = true;
                bindallitem();
                //  lvallitem.Select();
                //lvallitem.Items[0].Selected = true;
            }
            catch
            {
            }
        }

        private void cmbItemName_Leave(object sender, EventArgs e)
        {
            cmbItemName.BackColor = Color.White;
            try
            {
                if (lvallitem.Items[0].Selected == true)
                {
                    pnlallitem.Visible = true;
                }
                else
                {
                    pnlallitem.Visible = false;
                }
            }
            catch
            {
            }
        }

        private void cmbItemName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    try
                    {
                        // lvallitem.Focus();
                        lvallitem.Items[0].Selected = true;
                        lvallitem.Select();


                    }
                    catch
                    {
                    }
                }
                if (e.KeyCode == Keys.Enter)
                {
                    if (!string.IsNullOrEmpty(cmbItemName.Text))
                    {
                        cmbReplacementType.Focus();
                        txtQty.Text = "1";
                        bindallitem();
                    }
                    else
                    {
                        MessageBox.Show("Enter Item");
                        cmbItemName.Focus();
                    }

                }
                if (e.KeyCode == Keys.F3)
                {
                    var privouscontroal = cmbItemName;
                    activecontroal = privouscontroal.Name;
                    Itementry client = new Itementry(this, master, tabControl, activecontroal);

                    client.Passed(1);
                    //client.Show();

                    master.AddNewTab(client);

                    // autoreaderbind();
                    cmbItemName.Focus();

                }
                if (e.KeyCode == Keys.F2)
                {
                    if (cmbItemName.Text != "")
                    {
                        var privouscontroal = cmbItemName;
                        activecontroal = privouscontroal.Name;
                        Itementry client = new Itementry(this, master, tabControl, activecontroal);
                        client.Updatefromsale(cmbItemName.Text);
                        client.Passed(1);
                        //client.Show();
                        master.AddNewTab(client);
                    }
                }
            }
            catch
            {
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var privouscontroal = cmbItemName;
            activecontroal = privouscontroal.Name;
            Itementry client = new Itementry(this, master, tabControl, activecontroal);
            client.Passed(1);
            //client.Show();
            master.AddNewTab(client);
            // autoreaderbind();
            cmbItemName.Focus();
        }

        private void btnGroupEdit_Click(object sender, EventArgs e)
        {
            if (cmbItemName.Text != "")
            {
                // SqlDataAdapter da = new SqlDataAdapter();
                // DataTable dtitem = new DataTable();
                SqlCommand cmd = new SqlCommand("select productid from productmaster where Product_Name='" + cmbItemName.Text + "' and isactive=1", con);
                SqlDataAdapter sda6 = new SqlDataAdapter(cmd);
                DataTable dtitem = new DataTable();
                sda6.Fill(dtitem);
                if (dtitem.Rows.Count > 0)
                {
                    var privouscontroal = cmbItemName;
                    activecontroal = privouscontroal.Name;
                    Itementry client = new Itementry(this, master, tabControl, activecontroal);
                    client.Updatefromsale(cmbItemName.Text);
                    client.Passed(1);
                    //client.Show();
                    master.AddNewTab(client);
                }
                else
                {
                    MessageBox.Show("Please Enter/Select Valid Item.");
                }
            }
            else
            {
                MessageBox.Show("Please Enter/Select Item.");
            }
        }

        private void lvallitem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                String str = lvallitem.Items[lvallitem.FocusedItem.Index].SubItems[0].Text;
                //lvallitem.Items[0].Selected = false;
                cmbItemName.Text = str;
                cmbReplacementType.Focus();
                txtQty.Text = "1";
                bindallitem();
                //try
                //{
                //    if (lvallitem.Items[0].Selected == true)
                //    {
                //        pnlallitem.Visible = true;
                //    }
                //    else
                //    {
                //        pnlallitem.Visible = false;
                //    }
                //}
                //catch
                //{
                //}
            }
            catch
            {
            }
        }

        private void lvallitem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                String str = lvallitem.Items[lvallitem.FocusedItem.Index].SubItems[0].Text;
                //   lvallitem.Items[0].Selected = false;
                cmbItemName.Text = str;
                cmbReplacementType.Focus();
                txtQty.Text = "1";
                bindallitem();
                try
                {
                    if (lvallitem.Items[0].Selected == true)
                    {
                        pnlallitem.Visible = true;
                    }
                    else
                    {
                        pnlallitem.Visible = false;
                    }
                }
                catch
                {
                }
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

        private void cmbReplacementType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtserialno.Focus();
            }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //DataTable serial = new DataTable();
                //serial = conn.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + cmbItemName.Text + "'");
                //if (serial.Rows[0]["isserial"].ToString() == "True")
                //{
                //    pnlserial.Visible = true;
                //    txtserial.Focus();
                //}
                //else
                //{
                //    txtDescription.Focus();
                //}

            }
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtRemark.Focus();
            }
        }
        public void clearall()
        {
            txtQty.Text = "";
            cmbItemName.Text = "";
            txtDescription.Text = "";
            txtRemark.Text = "";
            serialno = "";
            cmbItemName.Focus();
            cmbReplacementType.SelectedIndex = -1;
            txtserialno.Text = "";
            // btnSave.Text = "Submit";
        }
        public void cleallallaftersubmit()
        {
            cmbcustname.SelectedIndex = -1;
            txtComplainId.Text = "";
            LVFO.Items.Clear();
            txtserialno.Text = "";
            txtQty.Text = "";
            cmbItemName.Text = "";
            txtDescription.Text = "";
            txtRemark.Text = "";
            serialno = "";
            dtpDate.Focus();
            cmbReplacementType.SelectedIndex = -1;
            btnSave.Text = "Submit";
            getcomplainid();
        }
        string serialno;
        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (rowid >= 0)
                    {
                        serialno = "";
                        LVFO.Items[rowid].SubItems[0].Text = cmbItemName.Text;
                        LVFO.Items[rowid].SubItems[1].Text = txtDescription.Text;
                        LVFO.Items[rowid].SubItems[2].Text = cmbReplacementType.Text;
                        LVFO.Items[rowid].SubItems[3].Text = txtQty.Text;
                        LVFO.Items[rowid].SubItems[4].Text = txtserialno.Text;
                        LVFO.Items[rowid].SubItems[5].Text = txtRemark.Text;
                        string itemid = conn.ExecuteScalar("select ProductID from ProductMaster where isactive=1 and Product_Name='" + cmbItemName.Text + "'");
                        LVFO.Items[rowid].SubItems[6].Text = itemid;
                        LVFO.Items[rowid].SubItems[7].Text = "Complain Received";
                        clearall();
                    }
                    else
                    {
                        if (rowid == -1 && btnSave.Text == "Update")
                        {
                            MessageBox.Show("You Don't Enter New Item");
                            clearall();
                            btnSave.Focus();
                        }
                        else
                        {
                            string d = "Product Sent To Customer";
                            DataTable dt = conn.getdataset("select serialno,status from tblitemcomplainmaster where isactive=1 and serialno='" + txtserialno.Text + "'and itemname='" + cmbItemName.Text + "' and status='" + d + "'");
                            if (dt.Rows.Count > 0 && dt.Rows[0][1].ToString() != d)
                            {
                                MessageBox.Show("This Item's Complain Is Already In Process");
                                txtserialno.Focus();
                                return;
                            }
                            else
                            {
                                ListViewItem li;
                                li = LVFO.Items.Add(cmbItemName.Text);
                                li.SubItems.Add(txtDescription.Text);
                                li.SubItems.Add(cmbReplacementType.Text);
                                li.SubItems.Add(txtQty.Text);
                                li.SubItems.Add(txtserialno.Text);
                                li.SubItems.Add(txtRemark.Text);
                                string itemid = conn.ExecuteScalar("select ProductID from ProductMaster where isactive=1 and Product_Name='" + cmbItemName.Text + "'");
                                li.SubItems.Add(itemid);
                                li.SubItems.Add("Complain Received");
                                clearall();
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }
        int flagserial = 0;
        public static DataTable temptable = new DataTable();
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
        public void enterdata()
        {
            try
            {
                if (btnSave.Text == "Update")
                {
                    if (LVFO.Items.Count > 0)
                    {
                        //conn.execute("Update tblitemcomplainmaster set isactive=0 where complainid='" + txtComplainId.Text + "'");
                        //for (int i = 0; i < LVFO.Items.Count; i++)
                        //{
                        //    conn.execute("INSERT INTO [dbo].[tblitemcomplainmaster]([complainID],[Itemname],[description],[replacementtype],[qty],[serialno],[remarks],[status],[itemid],[isactive])VALUES('" + txtComplainId.Text + "','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','" + LVFO.Items[i].SubItems[2].Text + "','" + LVFO.Items[i].SubItems[3].Text + "','" + LVFO.Items[i].SubItems[4].Text + "','" + LVFO.Items[i].SubItems[5].Text + "','" + "Complain Received" + "','" + LVFO.Items[i].SubItems[6].Text + "','" + "1" + "')");

                        //}
                        DataTable final = conn.getdataset("select * from tblitemcomplainmaster where isactive=1 and complainID='" + txtComplainId.Text + "'");
                        DataTable countdt = new DataTable();
                        countdt.Columns.Add("id", typeof(string));
                        for (int i = 0; i < final.Rows.Count; i++)
                        {
                            for (int j = 0; j < LVFO.Items.Count; j++)
                            {
                                if (final.Rows[i]["complainID"].ToString() == txtComplainId.Text && final.Rows[i]["itemid"].ToString() == LVFO.Items[j].SubItems[6].Text)
                                {
                                    //conn.execute("INSERT INTO [dbo].[tblitemreceivefromcompany]([receivefromcompanyid],[sendtocompanyid],[complainID],[serialno],[itemname],[remarks],[transportdetails],[senddate],[newserialno],[itemid],[isactive])VALUES('" + lblreceiveid.Text + "','" + lvcompany.Items[i].SubItems[0].Text + "','" + lvcompany.Items[i].SubItems[1].Text + "','" + lvcompany.Items[i].SubItems[2].Text + "','" + lvcompany.Items[i].SubItems[3].Text + "','" + lvcompany.Items[i].SubItems[4].Text + "','" + lvcompany.Items[i].SubItems[5].Text + "','" + lvcompany.Items[i].SubItems[6].Text + "','" + lvcompany.Items[i].SubItems[7].Text + "','" + lvcompany.Items[i].SubItems[8].Text + "','" + "1" + "')");
                                    //conn.execute("Update tblitemreceivefromcompany set receivefromcompanyid='" + lblreceiveid.Text + "',sendtocompanyid='" + lvcompany.Items[i].SubItems[0].Text + "',complainID='" + lvcompany.Items[i].SubItems[1].Text + "',serialno='" + lvcompany.Items[i].SubItems[2].Text + "',itemname='" + lvcompany.Items[i].SubItems[3].Text + "',remarks='" + lvcompany.Items[i].SubItems[4].Text + "',transportdetails='" + lvcompany.Items[i].SubItems[5].Text + "',senddate='" + lvcompany.Items[i].SubItems[6].Text + "',newserialno='" + lvcompany.Items[i].SubItems[7].Text + "',itemid='" + lvcompany.Items[i].SubItems[8].Text + "' where complainID='" + lvcompany.Items[j].SubItems[1].Text + "' and serialno='" + lvcompany.Items[j].SubItems[2].Text + "' and itemid='" + lvcompany.Items[j].SubItems[8].Text + "'");
                                    conn.execute("Update tblitemcomplainmaster set complainID='" + txtComplainId.Text + "',itemname='" + LVFO.Items[i].SubItems[0].Text + "',description='" + LVFO.Items[i].SubItems[1].Text + "',replacementtype='" + LVFO.Items[i].SubItems[2].Text + "',qty='" + LVFO.Items[i].SubItems[3].Text + "',serialno='" + LVFO.Items[i].SubItems[4].Text + "',remarks='" + LVFO.Items[i].SubItems[5].Text + "',status='" + "Complain Received" + "',itemid='" + LVFO.Items[i].SubItems[6].Text + "' where complainID='" + txtComplainId.Text + "' and itemid='" + LVFO.Items[i].SubItems[6].Text + "'");
                                    //   conn.execute("Update tblitemcomplainmaster set status='" + "Complain Received" + "' where isactive=1 and complainID='" + txtComplainId.Text + "' and serialno='" + LVFO.Items[j].SubItems[4].Text + "' and itemid='" + LVFO.Items[j].SubItems[6].Text + "'");
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

                            DataTable finaldt = conn.getdataset("select * from tblitemcomplainmaster where isactive=1 and id='" + final1.Rows[h]["id"].ToString() + "'");
                            if (finaldt.Rows.Count > 0)
                            {
                                //  conn.execute("Update tblitemcomplainmaster set status='" + "Complain Received" + "' where isactive=1 and complainID='" + finaldt.Rows[0]["complainID"].ToString() + "' and serialno='" + finaldt.Rows[0]["serialno"].ToString() + "' and itemid='" + finaldt.Rows[0]["itemid"].ToString() + "'");
                            }
                            conn.execute("Update tblitemcomplainmaster set isactive=0 where id='" + final1.Rows[h]["id"].ToString() + "'");
                        }
                        conn.execute("Update tblcomplainmaster set date='" + Convert.ToDateTime(dtpDate.Text).ToString(Master.dateformate) + "',customerid='" + cmbcustname.SelectedValue + "',customername='" + cmbcustname.Text + "'where id='" + txtComplainId.Text + "'");
                        MessageBox.Show("Data Updated Successfully.");
                        Print();
                        cleallallaftersubmit();
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Item");
                        cmbItemName.Focus();
                    }
                }
                else
                {
                    if (LVFO.Items.Count > 0)
                    {
                        conn.execute("INSERT INTO [dbo].[tblcomplainmaster]([date],[customerid],[customername],[isactive])VALUES('" + Convert.ToDateTime(dtpDate.Text).ToString(Master.dateformate) + "','" + cmbcustname.SelectedValue + "','" + cmbcustname.Text + "','" + "1" + "')");
                        for (int i = 0; i < LVFO.Items.Count; i++)
                        {
                            // conn.execute("INSERT INTO [dbo].[tblcomplainmaster]([date],[customerid],[customername],[itemname],[replacementtype],[qty],[srno],[descriprtion],[remarks],[isactive],[complainid],[itemid],[status])VALUES('" + Convert.ToDateTime(dtpDate.Text).ToString(Master.dateformate) + "','" + cmbcustname.SelectedValue + "','" + cmbcustname.Text + "','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[2].Text + "','" + LVFO.Items[i].SubItems[3].Text + "','" + LVFO.Items[i].SubItems[4].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','" + LVFO.Items[i].SubItems[5].Text + "','" + "1" + "','" + txtComplainId.Text + "','" + LVFO.Items[i].SubItems[6].Text + "','" + "Complain Received" + "')");
                            conn.execute("INSERT INTO [dbo].[tblitemcomplainmaster]([complainID],[Itemname],[description],[replacementtype],[qty],[serialno],[remarks],[status],[itemid],[isactive])VALUES('" + txtComplainId.Text + "','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','" + LVFO.Items[i].SubItems[2].Text + "','" + LVFO.Items[i].SubItems[3].Text + "','" + LVFO.Items[i].SubItems[4].Text + "','" + LVFO.Items[i].SubItems[5].Text + "','" + "Complain Received" + "','" + LVFO.Items[i].SubItems[6].Text + "','" + "1" + "')");
                        }
                        
                        MessageBox.Show("Data Inserted Successfully.");
                        Print();
                        cleallallaftersubmit();
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Item");
                        cmbItemName.Focus();
                    }
                }
            }
            catch
            {
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                enterdata();
            }
            catch
            {
            }
        }

        internal void Update(int p, string iid)
        {
            cnt = 1;
            DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
            dtpDate.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            dtpDate.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            dtpDate.CustomFormat = Master.dateformate;
            // getcomplainid();
            bindcustomer();
            this.ActiveControl = dtpDate;
            LVFO.Columns.Add("ItemName", 350, HorizontalAlignment.Center);
            LVFO.Columns.Add("Description", 170, HorizontalAlignment.Center);
            LVFO.Columns.Add("ReplacementType", 130, HorizontalAlignment.Center);
            LVFO.Columns.Add("Qty", 70, HorizontalAlignment.Center);
            LVFO.Columns.Add("SerialNo", 150, HorizontalAlignment.Center);
            LVFO.Columns.Add("Remark", 170, HorizontalAlignment.Center);
            LVFO.Columns.Add("Itemid", 0, HorizontalAlignment.Center);
            LVFO.Columns.Add("ComplainStatus", 0, HorizontalAlignment.Center);
            lvallitem.Columns.Add("Product Name", 400, HorizontalAlignment.Left);


            DataTable dt = conn.getdataset("select * from tblcomplainmaster where isactive=1 and id='" + iid + "'");
            DataTable dt1 = conn.getdataset("select * from tblitemcomplainmaster where isactive=1 and ComplainID='" + iid + "'");
            if (dt.Rows.Count > 0)
            {
                txtComplainId.Text = dt.Rows[0]["id"].ToString();
                dtpDate.Text = Convert.ToDateTime(dt.Rows[0]["Date"].ToString()).ToString(Master.dateformate);
                // string clientname = conn.ExecuteScalar("select accountname from clientmaster where isactive=1 and clientid='" + dt.Rows[0]["customerid"].ToString() + "' and isactive=1");
                cmbcustname.Text = dt.Rows[0]["customername"].ToString();


            }
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    if (dt1.Rows[i]["status"].ToString() == "Complain Received")
                    {
                        ListViewItem li;
                        li = LVFO.Items.Add(dt1.Rows[i]["itemname"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["description"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["replacementtype"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["qty"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["serialno"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["remarks"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["itemid"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["status"].ToString());
                        //  li.BackColor = Color.White;
                        li.ForeColor = Color.Black;

                    }
                    else
                    {
                        ListViewItem li;
                        li = LVFO.Items.Add(dt1.Rows[i]["itemname"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["description"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["replacementtype"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["qty"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["serialno"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["remarks"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["itemid"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["status"].ToString());
                        //  li.BackColor = Color.White;
                        li.ForeColor = Color.Red;
                    }
                }
            }
            btnSave.Text = "Update";
        }
        public void getdate()
        {
            try
            {
                if (LVFO.Items[LVFO.FocusedItem.Index].SubItems[7].Text == "Complain Received")
                {
                    rowid = LVFO.FocusedItem.Index;
                    cmbItemName.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[0].Text;
                    txtDescription.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[1].Text;
                    cmbReplacementType.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[2].Text;
                    txtQty.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[3].Text;
                    txtserialno.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[4].Text;
                    txtRemark.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[5].Text;
                    cmbItemName.Focus();
                }
                else
                {
                    MessageBox.Show("This Item's Complain is Already In Process You Can't Update");
                    cmbItemName.Focus();
                }

            }
            catch
            {
            }
        }
        private void LVFO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                getdate();
            }
            if (e.KeyCode == Keys.Delete)
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Delete Item?", "Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    if (LVFO.Items[LVFO.FocusedItem.Index].SubItems[7].Text == "Complain Received")
                    {
                        LVFO.Items[LVFO.FocusedItem.Index].Remove();
                        cmbItemName.Focus();
                    }
                    else
                    {
                        MessageBox.Show("This Item's Complain is Already In Process You Can't Delete");
                        cmbItemName.Focus();
                    }
                }
            }
        }

        private void LVFO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            getdate();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Delete Complain?", "Complain", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    bool exists = false;
                    this.Enabled = false;
                    DataTable dt = conn.getdataset("select * from tblitemcomplainmaster where isactive=1 and complainID='" + txtComplainId.Text + "'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["status"].ToString() != "Complain Received")
                        {
                            exists = true;
                        }
                    }
                    if (!exists)
                    {
                        conn.execute("Update tblcomplainmaster set isactive=0 where id='" + txtComplainId.Text + "'");
                        conn.execute("Update tblitemcomplainmaster set isactive=0 where complainid='" + txtComplainId.Text + "'");
                        cleallallaftersubmit();
                        MessageBox.Show("Delete Successfully");
                        dtpDate.Focus();
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




        private void btnprint_Click(object sender, EventArgs e)
        {
            Print();
        }

        public void Print()
        {
            try
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Print Complain?", "Complain", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    if (LVFO.Items.Count > 0)
                    {
                        prn.execute("delete from printing");
                        int j = 1;
                        for (int i = 0; i < LVFO.Items.Count; i++)
                        {
                            DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1 and CompanyID='" + Master.companyId + "' ");
                            string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22)VALUES";
                            qry += "('" + dt1.Rows[0]["CompanyName"].ToString() + "','" + dt1.Rows[0]["SubName"].ToString() + "','" + dt1.Rows[0]["Address"].ToString() + "','" + dt1.Rows[0]["Address2"].ToString() + "','" + dt1.Rows[0]["City"].ToString() + "','" + dt1.Rows[0]["State"].ToString() + "','" + dt1.Rows[0]["Country"].ToString() + "','" + dt1.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Mobile"].ToString() + "','" + dt1.Rows[0]["Email"].ToString() + "','" + dt1.Rows[0]["CSTNo"].ToString() + "','" + txtComplainId.Text + "','" + cmbcustname.Text + "','" + dtpDate.Text + "','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','" + LVFO.Items[i].SubItems[2].Text + "','" + LVFO.Items[i].SubItems[3].Text + "','" + LVFO.Items[i].SubItems[4].Text + "','" + LVFO.Items[i].SubItems[5].Text + "','" + dt1.Rows[0]["Website"].ToString() + "','" + j++ + "')";
                            prn.execute(qry);
                        }
                    }
                    string reportName = "ComplainReceive";
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
        private void txtserialno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtserialno.Text))
                {
                    if (LVFO.Items.Count != 0)
                    {

                        bool exists = false;
                        foreach (ListViewItem item in LVFO.Items)
                        {
                            for (int b = 0; b < item.SubItems.Count; b++)
                            {
                                string srno = item.SubItems[4].Text;
                                string itemname = item.SubItems[0].Text;
                                if (txtserialno.Text == srno && cmbItemName.Text == itemname)
                                {
                                    exists = true;
                                }
                            }
                        }
                        if (!exists)
                        {
                            string d = "Product Sent To Customer";
                            DataTable dt = conn.getdataset("Select * from tblitemcomplainmaster where isactive=1 and serialno='" + txtserialno.Text + "'and itemname='" + cmbItemName.Text + "' and status <> '" + d + "'");
                            if (dt.Rows.Count > 0)
                            {
                                MessageBox.Show("This Item's Complain is Already in Process");
                                txtserialno.Focus();
                            }
                            else
                            {
                                txtDescription.Focus();
                            }
                        }
                        else
                        {
                            if (rowid == -1)
                            {
                                MessageBox.Show("This Serial Number has been Inserted Already");
                                txtserialno.Focus();
                            }
                            else
                            {
                                string s = LVFO.Items[rowid].SubItems[4].Text;
                                if (s == txtserialno.Text)
                                {
                                    txtDescription.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("This Serial Number has been Inserted Already");
                                    txtserialno.Focus();
                                }
                            }
                        }
                    }
                    else
                    {
                        string d = "Product Sent To Customer";
                        DataTable dt = conn.getdataset("Select * from tblitemcomplainmaster where isactive=1 and serialno='" + txtserialno.Text + "'and itemname='" + cmbItemName.Text + "' and status <> '" + d + "'");
                        if (dt.Rows.Count > 0 && dt.Rows[0]["status"].ToString() != d)
                        {
                            MessageBox.Show("This Item's Complain is Already in Process");
                            txtserialno.Focus();
                        }
                        else
                        {
                            txtDescription.Focus();
                        }
                    }
                }
                else
                {
                    //  MessageBox.Show("Enter Serial No");
                    txtserialno.Focus();
                    //  if (txtserialno.Text == "" || txtserialno.Text == "0")
                    //{
                    //  txtserialno.Text =Convert.ToString(("SerialNo-" + Convert.ToInt32(txtComplainId.Text)));
                    // }
                    // var guid = Guid.NewGuid().ToString();
                    // txtserialno.Text = Convert.ToString(guid);
                    GenOTPSerialKey();
                    txtserialno.Text = Convert.ToString(strrandom);
                }
            }
        }
        string strrandom;
        public void GenOTPSerialKey()
        {
            char[] charArr = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            strrandom = string.Empty;
            Random objran = new Random();
            int noofcharacters = Convert.ToInt32(5);
            for (int i = 0; i < noofcharacters; i++)
            {
                //It will not allow Repetation of Characters
                int pos = objran.Next(1, charArr.Length);
                if (!strrandom.Contains(charArr.GetValue(pos).ToString()))
                    strrandom += charArr.GetValue(pos);
                else
                    i--;
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

        private void cmbItemName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select Product_Name from productmaster where Product_Name like'%" + cmbItemName.Text + "%' and isactive=1", con);
                SqlDataAdapter sda6 = new SqlDataAdapter(cmd);
                DataTable dtitem = new DataTable();
                sda6.Fill(dtitem);
                lvallitem.Items.Clear();
                for (int i = 0; i < dtitem.Rows.Count; i++)
                {
                    ListViewItem li;
                    li = lvallitem.Items.Add(dtitem.Rows[i]["Product_Name"].ToString());
                }
                //  lvallitem.Focus();
                if (cmbItemName.Text == "" && cmbItemName.Text == null)
                {
                    bindallitem();
                }
            }
            catch
            {
            }
        }

        private void lvallitem_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                String str = lvallitem.Items[lvallitem.FocusedItem.Index].SubItems[0].Text;
                //lvallitem.Items[0].Selected = false;
                cmbItemName.Text = str;
                cmbReplacementType.Focus();
                txtQty.Text = "1";
                bindallitem();
                //try
                //{
                //    if (lvallitem.Items[0].Selected == true)
                //    {
                //        pnlallitem.Visible = true;
                //    }
                //    else
                //    {
                //        pnlallitem.Visible = false;
                //    }
                //}
                //catch
                //{
                //}
            }
            catch
            {
            }
        }

    }

}

