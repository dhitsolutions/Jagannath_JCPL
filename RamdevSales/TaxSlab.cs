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
    public partial class TaxSlab : Form
    {
        private Master master;
        private TabControl tabControl;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection cn = new Connection();
        public static string iid = "";
        private Itementry itementry;
        public static string pvc;
        int a;
        public TaxSlab()
        {
            InitializeComponent();
        }

        public TaxSlab(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
            a = 0;
        }

        public TaxSlab(Itementry itementry, TabControl tabControl, Master master,string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.itementry = itementry;
            this.tabControl = tabControl;
            this.master = master;
            a = 1;
            pvc = activecontroal;
        }

        private void TaxSlab_Load(object sender, EventArgs e)
        {
            con.Open();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            if (taxslabname == "")
            {
                pnlgst.Visible = false;
                pnllistslab.Visible = true;
            }
            else
            {
                pnlgst.Visible = true;
                pnllistslab.Visible = false;
            }
            lvgst.Columns.Add("Tax Slab", 100, HorizontalAlignment.Left);
            lvgst.Columns.Add("Sale Type", 100, HorizontalAlignment.Left);
            lvgst.Columns.Add("System", 100, HorizontalAlignment.Left);
            lvgst.Columns.Add("Category", 100, HorizontalAlignment.Left);
            lvgst.Columns.Add("On ", 100, HorizontalAlignment.Left);
            lvgst.Columns.Add("SGST", 100, HorizontalAlignment.Left);
            lvgst.Columns.Add("CGST", 100, HorizontalAlignment.Left);
            lvgst.Columns.Add("IGST", 100, HorizontalAlignment.Left);
            lvgst.Columns.Add("Addl Tax", 100, HorizontalAlignment.Left);
            lvgst.Columns.Add("MRP", 100, HorizontalAlignment.Left);
            lvgst.Columns.Add("FREE", 100, HorizontalAlignment.Left);
            lvgst.Columns.Add("saletypeid", 50, HorizontalAlignment.Left);
            lvlist.Columns.Add("Tax Slab", 375, HorizontalAlignment.Left);
            lvlist.Columns.Add("System", 350, HorizontalAlignment.Left);
           // bindsaletype();
            bindSystem();
            bindtaxslab();
            this.ActiveControl = btnnew;

            //set the interval  and start the timer
            timer1.Interval = 1000;
            timer1.Start();

            con.Close();
        }
        public void bindtaxslab()
        {
            DataTable dt = cn.getdataset("SELECT DISTINCT Taxslabname,system FROM Taxslabmaster");
            if (dt.Rows.Count > 0)
            {
                lvlist.Items.Clear();
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    lvlist.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                    lvlist.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                }
            }
        }
        public void bindSystem()
        {
            if (Master.Taxactation == "")
            {

                SqlCommand cmddept = new SqlCommand("select id,System from System_Master", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmddept);
                DataTable dtdept1 = new DataTable();
                sda.Fill(dtdept1);

                ddlsystem.ValueMember = "id";
                ddlsystem.DisplayMember = "System";
                ddlsystem.DataSource = dtdept1;
                ddlsystem.SelectedIndex = -1;
            }
            else
            {
                SqlCommand cmddept = new SqlCommand("select id,System from System_Master", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmddept);
                DataTable dtdept1 = new DataTable();
                sda.Fill(dtdept1);

                ddlsystem.ValueMember = "id";
                ddlsystem.DisplayMember = "System";
                ddlsystem.DataSource = dtdept1;
                ddlsystem.Text = Master.Taxactation;

            }
        }
        public void bindsaletype()
        {
            SqlCommand cmd = new SqlCommand("select Purchasetypeid,Purchasetypename from PurchasetypeMaster where isactive=1", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sda.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                chklisttaxtype.Items.Add(dt.Rows[i]["Purchasetypename"].ToString());
                chklisttaxtype.SetItemChecked(i, true);

            }

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
                        master.RemoveCurrentTab1(pvc, txttaxslab.Text);
                    }
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void btngstadd_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable getslabname = cn.getdataset("select * from taxslabmaster where isactive=1 and Taxslabname='" + txttaxslab.Text + "'");
                if (getslabname.Rows.Count > 0)
                {
                    MessageBox.Show("Tax Slab Already Available Please add Another");
                    txttaxslab.Focus();
                    clear();
                    return;
                }
                else
                {
                    foreach (object itemChecked in chklisttaxtype.CheckedItems)
                    {
                        string str = itemChecked.ToString();
                        //for (int i=0;i < chklist.Count(); i++)
                        //{
                        DataTable saletypeid = cn.getdataset("select Purchasetypeid from PurchasetypeMaster where isactive=1 and Purchasetypename='" + str + "'");

                        string sid = saletypeid.Rows[0]["Purchasetypeid"].ToString();
                        ListViewItem li;
                        string mrp, free;
                        if (chkonmrp.Checked == true)
                        {
                            mrp = "True";
                        }
                        else
                        {
                            mrp = "False";
                        }
                        if (chkfree.Checked == true)
                        {
                            free = "True";
                        }
                        else
                        {
                            free = "False";
                        }
                        if (txtsgst.Text == "")
                        {
                            txtsgst.Text = "0";
                        }
                        if (txtcgst.Text == "")
                        {
                            txtcgst.Text = "0";
                        }
                        if (txtigst.Text == "")
                        {
                            txtigst.Text = "0";
                        }
                        if (txtaddtax.Text == "")
                        {
                            txtaddtax.Text = "0";
                        }
                        li = lvgst.Items.Add(txttaxslab.Text);
                        li.SubItems.Add(str);
                        li.SubItems.Add(ddlsystem.Text);
                        li.SubItems.Add(ddlcategory.Text);
                        li.SubItems.Add(ddlon.Text);
                        li.SubItems.Add(txtsgst.Text);
                        li.SubItems.Add(txtcgst.Text);
                        li.SubItems.Add(txtigst.Text);
                        li.SubItems.Add(txtaddtax.Text);
                        li.SubItems.Add(mrp);
                        li.SubItems.Add(free);
                        li.SubItems.Add(sid);

                    }
                    txttaxslab.Text = "";
                    cmsaletypegst.SelectedIndex = -1;
                    ddlcategory.SelectedIndex = -1;
                    ddlon.SelectedIndex = -1;
                    txtsgst.Text = string.Empty;
                    txtcgst.Text = string.Empty;
                    txtigst.Text = string.Empty;
                    txtaddtax.Text = string.Empty;
                    chkonmrp.Checked = false;
                    chkfree.Checked = false;
           
                    for (int i = 0; i < chklisttaxtype.Items.Count; i++)
                        chklisttaxtype.SetItemCheckState(i, CheckState.Unchecked);
                    // bindsaletype();
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
        public void clear()
        {
            lvgst.Items.Clear();
          
            bindsaletype();
            bindtaxslab();
            cmsaletypegst.SelectedIndex = -1;
            ddlcategory.SelectedIndex = -1;
            ddlon.SelectedIndex = -1;
            txtsgst.Text = string.Empty;
            txtcgst.Text = string.Empty;
            txtigst.Text = string.Empty;
            txtaddtax.Text = string.Empty;
            chkonmrp.Checked = false;
            chkfree.Checked = false;
            btnsubmit.Text = "Submit";
        }
        private void ddlsystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlsystem.SelectedIndex == 0)
                {
                    pnlgst.Visible = true;
                    // pnlvat.Visible = false;
                }
                else
                {
                    pnlgst.Visible = false;
                    //pnlvat.Visible = true;
                }
            }
            catch
            {
            }
        }
        private void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                if (ddlsystem.Text == "")
                {
                    MessageBox.Show("Please Select Syatem Type");
                }
                if (btnsubmit.Text == "Update")
                {
                    SqlCommand cmd = new SqlCommand("delete from TaxSlabMaster where Taxslabname='" + lvgst.Items[0].SubItems[0].Text + "'", con);
                    cmd.ExecuteNonQuery();
                    for (int i = 0; i < lvgst.Items.Count; i++)
                    {

                        string qry = "INSERT INTO [dbo].[TaxSlabMaster]([Taxslabname],[saletypename],[saletypeid],[System],[category],[onwhich],[SGST],[CGST],[IGST],[Additax],[isonmrp],[isonfreegoods],[isactive])VALUES('" + lvgst.Items[i].SubItems[0].Text + "','" + lvgst.Items[i].SubItems[1].Text + "','" + lvgst.Items[i].SubItems[11].Text + "','" + lvgst.Items[i].SubItems[2].Text + "','" + lvgst.Items[i].SubItems[3].Text + "','" + lvgst.Items[i].SubItems[4].Text + "','" + lvgst.Items[i].SubItems[5].Text + "','" + lvgst.Items[i].SubItems[6].Text + "','" + lvgst.Items[i].SubItems[7].Text + "','" + lvgst.Items[i].SubItems[8].Text + "','" + lvgst.Items[i].SubItems[9].Text + "','" + lvgst.Items[i].SubItems[10].Text + "','1')";
                        SqlCommand cmd4 = new SqlCommand(qry, con);
                        cmd4.ExecuteNonQuery();
                    }
                    MessageBox.Show("TaxSlab Update Sucessfully");
                    clear();
                    if (a == 1)
                    {
                        itementry.bindtaxslab();
                        if (string.IsNullOrEmpty(pvc) == true)
                        {
                            master.RemoveCurrentTab();
                        }
                        else
                        {
                            master.RemoveCurrentTab1(pvc, txttaxslab.Text);
                        }
                    }
                    else
                    {
                        this.ActiveControl = txttaxslab;
                      //  pnllistslab.Visible = true;
                       // pnlgst.Visible = false;
                    }
                    txttaxslab.Text = "";
                }
                else
                {
                    for (int i = 0; i < lvgst.Items.Count; i++)
                    {

                        string qry = "INSERT INTO [dbo].[TaxSlabMaster]([Taxslabname],[saletypename],[saletypeid],[System],[category],[onwhich],[SGST],[CGST],[IGST],[Additax],[isonmrp],[isonfreegoods],[isactive])VALUES('" + lvgst.Items[i].SubItems[0].Text + "','" + lvgst.Items[i].SubItems[1].Text + "','" + lvgst.Items[i].SubItems[11].Text + "','" + lvgst.Items[i].SubItems[2].Text + "','" + lvgst.Items[i].SubItems[3].Text + "','" + lvgst.Items[i].SubItems[4].Text + "','" + lvgst.Items[i].SubItems[5].Text + "','" + lvgst.Items[i].SubItems[6].Text + "','" + lvgst.Items[i].SubItems[7].Text + "','" + lvgst.Items[i].SubItems[8].Text + "','" + lvgst.Items[i].SubItems[9].Text + "','" + lvgst.Items[i].SubItems[10].Text + "','1')";
                        SqlCommand cmd4 = new SqlCommand(qry, con);
                        cmd4.ExecuteNonQuery();
                    }
                    MessageBox.Show("TaxSlab Insert Sucessfully");
                    clear();
                    if (a == 1)
                    {
                        itementry.bindtaxslab();
                        if (string.IsNullOrEmpty(pvc) == true)
                        {
                            master.RemoveCurrentTab();
                        }
                        else
                        {
                            master.RemoveCurrentTab1(pvc, txttaxslab.Text);
                        }
                    }
                    else
                    {
                        this.ActiveControl = txttaxslab;
                    }
                    txttaxslab.Text = "";
                }
               // pnllistslab.Visible = true;
               // pnlgst.Visible = false;
                

                
            }
            catch
            {
            }
            finally
            {
                con.Close();
            }

        }


        private void btncancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }
        private void txtsgst_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtcgst.Focus();
            }
        }

        private void txtcgst_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtigst.Focus();
            }
        }

        private void txtigst_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtaddtax.Focus();
            }
        }
        private void chklisttaxtype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ddlcategory.Focus();
            }
        }
        private void txtsgst_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtcgst_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtigst_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtaddtax_KeyPress(object sender, KeyPressEventArgs e)
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

        private void lvgst_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    open();
                }
                catch
                {
                }
            }
            if (e.KeyCode == Keys.Delete)
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Remove Tax Type?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    lvgst.Items[lvgst.FocusedItem.Index].Remove();
                }
            }
        }
        private void ddlcategory_Enter(object sender, EventArgs e)
        {
            try
            {
                ddlcategory.SelectedIndex = 0;
                ddlcategory.DroppedDown = true;
            }
            catch
            {
            }
        }

        private void ddlon_Enter(object sender, EventArgs e)
        {
            try
            {
                ddlon.SelectedIndex = 0;
                ddlon.DroppedDown = true;
            }
            catch
            {
            }
        }
        private void txtaddtax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               // chkonmrp.Focus();
                ddlon.Focus();
            }
        }

        private void chkonmrp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkfree.Focus();
            }
        }

        private void chkfree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btngstadd.Focus();
            }
        }
        private void cmsaletypegst_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ddlcategory.Focus();
            }
        }
        public static string s;
        private void ddlon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < ddlon.Items.Count; i++)
                {
                    s = ddlon.GetItemText(ddlon.Items[i]);
                    if (s == ddlon.Text)
                    {
                        inList = true;
                        ddlon.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    ddlon.Text = "";
                }

                // chkecom.Focus();
                chkonmrp.Focus();
            }
        }
        private void ddlcategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < ddlcategory.Items.Count; i++)
                {
                    s = ddlcategory.GetItemText(ddlcategory.Items[i]);
                    if (s == ddlcategory.Text)
                    {
                        inList = true;
                        ddlcategory.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    ddlcategory.Text = "";
                }
               // ddlon.Focus();
                txtsgst.Focus();
            }
        }
        private void txtsgst_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtigst.Text = txtsgst.Text;
            }
            catch
            {
            }
        }
        private void txtcgst_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Double s = Convert.ToDouble(txtsgst.Text);
                Double t = Convert.ToDouble(txtcgst.Text);
                Double x = s + t;
                txtigst.Text = x.ToString();
            }
            catch
            {
            }
        }
        public void open()
        {
            if (lvgst.SelectedItems.Count > 0)
            {

                for (int index = 0; index < chklisttaxtype.Items.Count; index++)
                {
                    int sid = Convert.ToInt32(lvgst.Items[lvgst.FocusedItem.Index].SubItems[11].Text);
                    if (index == sid - 1)
                    {
                        chklisttaxtype.SetItemCheckState(index, CheckState.Checked);
                    }
                    else
                    {
                        chklisttaxtype.SetItemCheckState(index, CheckState.Unchecked);
                    }
                }
                txttaxslab.Text = lvgst.Items[lvgst.FocusedItem.Index].SubItems[0].Text;
                ddlsystem.Text = lvgst.Items[lvgst.FocusedItem.Index].SubItems[2].Text;
                ddlcategory.Text = lvgst.Items[lvgst.FocusedItem.Index].SubItems[3].Text;
                ddlon.Text = lvgst.Items[lvgst.FocusedItem.Index].SubItems[4].Text;
                txtsgst.Text = lvgst.Items[lvgst.FocusedItem.Index].SubItems[5].Text;
                txtcgst.Text = lvgst.Items[lvgst.FocusedItem.Index].SubItems[6].Text;
                txtigst.Text = lvgst.Items[lvgst.FocusedItem.Index].SubItems[7].Text;
                txtaddtax.Text = lvgst.Items[lvgst.FocusedItem.Index].SubItems[8].Text;
                if (lvgst.Items[lvgst.FocusedItem.Index].SubItems[9].Text == "True")
                {
                    chkonmrp.Checked = true;
                }
                else
                {
                    chkonmrp.Checked = false;
                }
                if (lvgst.Items[lvgst.FocusedItem.Index].SubItems[10].Text == "True")
                {
                    chkfree.Checked = true;
                }
                else
                {
                    chkfree.Checked = false;
                }

                lvgst.Items[lvgst.FocusedItem.Index].Remove();
                // lvgst.Items.Clear();
            }
        }
        private void lvgst_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                open();
            }
            catch
            {
            }
        }
        private void txtsgst_Enter(object sender, EventArgs e)
        {
            txtsgst.BackColor = Color.LightYellow;
        }

        private void txtsgst_Leave(object sender, EventArgs e)
        {
            txtsgst.BackColor = Color.White;
        }

        private void txtcgst_Enter(object sender, EventArgs e)
        {
            txtcgst.BackColor = Color.LightYellow;
        }

        private void txtcgst_Leave(object sender, EventArgs e)
        {
            txtcgst.BackColor = Color.White;
        }

        private void txtigst_Enter(object sender, EventArgs e)
        {
            txtigst.BackColor = Color.LightYellow;
        }

        private void txtigst_Leave(object sender, EventArgs e)
        {
            txtigst.BackColor = Color.White;
        }

        private void txtaddtax_Enter(object sender, EventArgs e)
        {
            txtaddtax.BackColor = Color.LightYellow;
        }

        private void txtaddtax_Leave(object sender, EventArgs e)
        {
            txtaddtax.BackColor = Color.White;
        }

        private void txttaxslab_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chklisttaxtype.Focus();
            }
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            clear();
            pnlgst.Visible = true;
            pnllistslab.Visible = false;
            txttaxslab.Focus();
            this.ActiveControl = txttaxslab;
        }

        private void txtbacklist_Click(object sender, EventArgs e)
        {
            pnlgst.Visible = false;
            pnllistslab.Visible = true;
        }
        public void getdata()
        {
            pnlgst.Visible = true;
            pnllistslab.Visible = false;
            iid = lvlist.Items[lvlist.FocusedItem.Index].SubItems[0].Text;
            DataTable slab = cn.getdataset("SELECT * FROM Taxslabmaster where Taxslabname='" + iid + "'");
            ListViewItem li;
            for (int i = 0; i < slab.Rows.Count; i++)
            {
                li = lvgst.Items.Add(slab.Rows[i]["Taxslabname"].ToString());
                li.SubItems.Add(slab.Rows[i]["saletypename"].ToString());
                li.SubItems.Add(slab.Rows[i]["system"].ToString());
                li.SubItems.Add(slab.Rows[i]["category"].ToString());
                li.SubItems.Add(slab.Rows[i]["onwhich"].ToString());
                li.SubItems.Add(slab.Rows[i]["sgst"].ToString());
                li.SubItems.Add(slab.Rows[i]["cgst"].ToString());
                li.SubItems.Add(slab.Rows[i]["igst"].ToString());
                li.SubItems.Add(slab.Rows[i]["additax"].ToString());
                if (slab.Rows[0]["isonmrp"].ToString() == "True")
                {
                    li.SubItems.Add("True");
                }
                else
                {
                    li.SubItems.Add("False");
                }
                if (slab.Rows[0]["isonfreegoods"].ToString() == "True")
                {
                    li.SubItems.Add("True");
                }
                else
                {
                    li.SubItems.Add("False");
                }
                li.SubItems.Add(slab.Rows[i]["saletypeid"].ToString());
            }
            btnsubmit.Text = "Update";
            //txttaxslab.Text = slab.Rows[0]["Taxslabname"].ToString();
            //ddlsystem.Text = slab.Rows[0]["system"].ToString();
            //ddlcategory.Text = slab.Rows[0]["category"].ToString();
            //ddlon.Text = slab.Rows[0]["onwhich"].ToString();
            //txtsgst.Text = slab.Rows[0]["sgst"].ToString();
            //txtcgst.Text = slab.Rows[0]["cgst"].ToString();
            //txtigst.Text = slab.Rows[0]["igst"].ToString();
            //txtaddtax.Text = slab.Rows[0]["additax"].ToString();
            //if (slab.Rows[0]["isonmrp"].ToString() == "True")
            //{
            //    chkonmrp.Checked = true;
            //}
            //else
            //{
            //    chkonmrp.Checked = false;
            //}
            //if (slab.Rows[0]["isonfreegoods"].ToString() == "True")
            //{
            //    chkfree.Checked = true;
            //}
            //else
            //{
            //    chkfree.Checked = false;
            //}
        }
        private void lvlist_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            getdata();
        }

        private void lvlist_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                getdata();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
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
                    master.RemoveCurrentTab1(pvc, txttaxslab.Text);
                }
            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void txttaxslab_Enter(object sender, EventArgs e)
        {
            txttaxslab.BackColor = Color.LightYellow;
        }

        private void txttaxslab_Leave(object sender, EventArgs e)
        {
            txttaxslab.BackColor = Color.White;
        }

        private void ddlon_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < ddlon.Items.Count; i++)
                {
                    s = ddlon.GetItemText(ddlon.Items[i]);
                    if (s == ddlon.Text)
                    {
                        inList = true;
                        ddlon.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    ddlon.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void chkonmrp_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkfree_CheckedChanged(object sender, EventArgs e)
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

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = false;
            btnClose.BackColor = Color.FromArgb(248, 152, 94);
            btnClose.ForeColor = Color.White;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = true;
            btnClose.BackColor = Color.FromArgb(51, 153, 255);
            btnClose.ForeColor = Color.White;
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

        private void btnClose_Enter(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = false;
            btnClose.BackColor = Color.FromArgb(248, 152, 94);
            btnClose.ForeColor = Color.White;
        }

        private void btnClose_Leave(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = true;
            btnClose.BackColor = Color.FromArgb(51, 153, 255);
            btnClose.ForeColor = Color.White;
        }

        private void pnllistslab_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnsubmit_MouseEnter(object sender, EventArgs e)
        {
            btnsubmit.UseVisualStyleBackColor = false;
            btnsubmit.BackColor = Color.YellowGreen;
            btnsubmit.ForeColor = Color.White;
        }

        private void btnsubmit_MouseLeave(object sender, EventArgs e)
        {
            btnsubmit.UseVisualStyleBackColor = true;
            btnsubmit.BackColor = Color.FromArgb(51, 153, 255);
            btnsubmit.ForeColor = Color.White;
        }

        private void btnsubmit_Enter(object sender, EventArgs e)
        {
            btnsubmit.UseVisualStyleBackColor = false;
            btnsubmit.BackColor = Color.YellowGreen;
            btnsubmit.ForeColor = Color.White;
        }

        private void btnsubmit_Leave(object sender, EventArgs e)
        {
            btnsubmit.UseVisualStyleBackColor = true;
            btnsubmit.BackColor = Color.FromArgb(51, 153, 255);
            btnsubmit.ForeColor = Color.White;
        }

        private void btncancel_MouseEnter(object sender, EventArgs e)
        {
            btncancel.UseVisualStyleBackColor = false;
            btncancel.BackColor = Color.FromArgb(248, 152, 94);
            btncancel.ForeColor = Color.White;
        }

        private void btncancel_Enter(object sender, EventArgs e)
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

        private void btncancel_Leave(object sender, EventArgs e)
        {
            btncancel.UseVisualStyleBackColor = true;
            btncancel.BackColor = Color.FromArgb(51, 153, 255);
            btncancel.ForeColor = Color.White;
        }

        private void btngstadd_Enter(object sender, EventArgs e)
        {
            btngstadd.UseVisualStyleBackColor = false;
            btngstadd.BackColor = Color.FromArgb(9, 106,3);
            btngstadd.ForeColor = Color.White;
        }

        private void btngstadd_Leave(object sender, EventArgs e)
        {
            btngstadd.UseVisualStyleBackColor = true;
            btngstadd.BackColor = Color.FromArgb(51, 153, 255);
            btngstadd.ForeColor = Color.White;
        }

        private void btngstadd_MouseEnter(object sender, EventArgs e)
        {
            btngstadd.UseVisualStyleBackColor = false;
            btngstadd.BackColor = Color.FromArgb(9, 106, 3);
            btngstadd.ForeColor = Color.White;
        }

        private void btngstadd_MouseLeave(object sender, EventArgs e)
        {
            btngstadd.UseVisualStyleBackColor = true;
            btngstadd.BackColor = Color.FromArgb(51, 153, 255);
            btngstadd.ForeColor = Color.White;
        }

        private void txtbacklist_Enter(object sender, EventArgs e)
        {
            txtbacklist.UseVisualStyleBackColor = false;
            txtbacklist.BackColor = Color.FromArgb(9, 106, 3);
            txtbacklist.ForeColor = Color.White;
        }

        private void txtbacklist_Leave(object sender, EventArgs e)
        {
            txtbacklist.UseVisualStyleBackColor = true;
            txtbacklist.BackColor = Color.FromArgb(51, 153, 255);
            txtbacklist.ForeColor = Color.White;
        }

        private void txtbacklist_MouseEnter(object sender, EventArgs e)
        {
            txtbacklist.UseVisualStyleBackColor = false;
            txtbacklist.BackColor = Color.FromArgb(9, 106, 3);
            txtbacklist.ForeColor = Color.White;
        }

        private void txtbacklist_MouseLeave(object sender, EventArgs e)
        {
            btngstadd.UseVisualStyleBackColor = true;
            btngstadd.BackColor = Color.FromArgb(51, 153, 255);
            btngstadd.ForeColor = Color.White;
        }
        string searchstr;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //empty the string for every 1 seconds
            searchstr = "";
        }

        private void ddlcategory_KeyUp(object sender, KeyEventArgs e)
        {
            if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            {
                searchstr = searchstr + Convert.ToChar(e.KeyCode);
                // If the Search string is greater than 1 then use custom logic
                if (searchstr.Length > 1)
                {
                    int index;
                    // Search the Item that matches the string typed
                    index = ddlcategory.FindString(searchstr);
                    // Select the Item in the Combo
                    if (index > 0)
                    {
                        ddlcategory.SelectedIndex = index;
                    }
                }


            }
        }

        private void cmsaletypegst_KeyUp(object sender, KeyEventArgs e)
        {
            if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            {
                searchstr = searchstr + Convert.ToChar(e.KeyCode);
                // If the Search string is greater than 1 then use custom logic
                if (searchstr.Length > 1)
                {
                    int index;
                    // Search the Item that matches the string typed
                    index = cmsaletypegst.FindString(searchstr);
                    // Select the Item in the Combo
                    if (index > 0)
                    {
                        cmsaletypegst.SelectedIndex = index;
                    }
                }


            }
        }

        string taxslabname = "";
        internal void Update(string taxslab)
        {
            pnlgst.Visible = true;
            pnllistslab.Visible = false;

            taxslabname = taxslab;
            DataTable slab = cn.getdataset("SELECT * FROM Taxslabmaster where Taxslabname='" + taxslabname + "'");
            ListViewItem li;
            for (int i = 0; i < slab.Rows.Count; i++)
            {
                li = lvgst.Items.Add(slab.Rows[i]["Taxslabname"].ToString());
                li.SubItems.Add(slab.Rows[i]["saletypename"].ToString());
                li.SubItems.Add(slab.Rows[i]["system"].ToString());
                li.SubItems.Add(slab.Rows[i]["category"].ToString());
                li.SubItems.Add(slab.Rows[i]["onwhich"].ToString());
                li.SubItems.Add(slab.Rows[i]["sgst"].ToString());
                li.SubItems.Add(slab.Rows[i]["cgst"].ToString());
                li.SubItems.Add(slab.Rows[i]["igst"].ToString());
                li.SubItems.Add(slab.Rows[i]["additax"].ToString());
                if (slab.Rows[0]["isonmrp"].ToString() == "True")
                {
                    li.SubItems.Add("True");
                }
                else
                {
                    li.SubItems.Add("False");
                }
                if (slab.Rows[0]["isonfreegoods"].ToString() == "True")
                {
                    li.SubItems.Add("True");
                }
                else
                {
                    li.SubItems.Add("False");
                }
                li.SubItems.Add(slab.Rows[i]["saletypeid"].ToString());
            }
            btnsubmit.Text = "Update";
           // throw new NotImplementedException();
        }

        private void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < ddlcategory.Items.Count; i++)
                {
                    s = ddlcategory.GetItemText(ddlcategory.Items[i]);
                    if (s == ddlcategory.Text)
                    {
                        inList = true;
                        ddlcategory.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    ddlcategory.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void chkonmrp_Enter(object sender, EventArgs e)
        {
            chkonmrp.ForeColor = Color.Green;
        }

        private void chkonmrp_Leave(object sender, EventArgs e)
        {
            chkonmrp.ForeColor = Color.Black;
        }

        private void chkfree_Enter(object sender, EventArgs e)
        {
            chkfree.ForeColor = Color.Green;
        }

        private void chkfree_Leave(object sender, EventArgs e)
        {
            chkfree.ForeColor = Color.Black;
        }
    }
}
