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
    public partial class PromotionOffer : Form
    {
        private Master master;
        private TabControl tabControl;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        OleDbSettings ods = new OleDbSettings();
        Connection conn = new Connection();

        public PromotionOffer()
        {
            InitializeComponent();
        }

        public PromotionOffer(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
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
        public void bindallitem1()
        {
            try
            {
                DataTable allitem = new DataTable();
                lvallitem1.Items.Clear();
                allitem = conn.getdataset("select ProductMaster.Product_Name from ProductMaster where isactive=1 order by ProductMaster.Product_Name asc");
                for (int i = 0; i < allitem.Rows.Count; i++)
                {
                    ListViewItem li;
                    li = lvallitem1.Items.Add(allitem.Rows[i]["Product_Name"].ToString());
                }
            }
            catch
            {
            }
        }
        private void PromotionOffer_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
                DTPFrom.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                DTPFrom.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                DTPTo.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                DTPTo.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                lvallitem.Columns.Add("Product Name", 300, HorizontalAlignment.Left);
                lvallitem1.Columns.Add("Product Name", 300, HorizontalAlignment.Left);
                DTPFrom.CustomFormat = Master.dateformate;
                DTPTo.CustomFormat = Master.dateformate;
                this.ActiveControl = DTPFrom;
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
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void txtitemname_Enter(object sender, EventArgs e)
        {
            try
            {
                txtitemname.BackColor = Color.LightYellow;
                pnlallitem.Visible = true;
                bindallitem();
                //  lvallitem.Select();
                //lvallitem.Items[0].Selected = true;
            }
            catch
            {
            }
        }

        private void txtitemname_Leave(object sender, EventArgs e)
        {
            txtitemname.BackColor = Color.White;
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

        private void DTPFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DTPTo.Focus();
            }
        }

        private void DTPTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtitemname.Focus();
            }
        }

        private void txtitemname_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // lvallitem.Items[0].Selected = true;
                SqlCommand cmd = new SqlCommand("select Product_Name from productmaster where Product_Name like'%" + txtitemname.Text + "%' and isactive=1", con);
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
                if (txtitemname.Text == "" && txtitemname.Text == null)
                {
                    bindallitem();
                }
            }
            catch
            {
            }
        }

        private void txtitemname_KeyDown(object sender, KeyEventArgs e)
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
                if (chkfreeitem.Checked == true)
                {
                    txtqty.Focus();
                }
                else
                {
                    cmbdiscount.Focus();
                }
            }
        }

        private void lvallitem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                String str = lvallitem.Items[lvallitem.FocusedItem.Index].SubItems[0].Text;
                //   lvallitem.Items[0].Selected = false;
                txtitemname.Text = str;
                txtitemname.Focus();
            }
        }

        private void lvallitem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            String str = lvallitem.Items[lvallitem.FocusedItem.Index].SubItems[0].Text;
            //lvallitem.Items[0].Selected = false;
            txtitemname.Text = str;
            txtitemname.Focus();
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }

        private void btnclose_Enter(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = false;
            btnclose.BackColor = System.Drawing.Color.FromArgb(248, 152, 94);
            btnclose.ForeColor = System.Drawing.Color.White;
        }

        private void btnclose_Leave(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = true;
            btnclose.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnclose.ForeColor = System.Drawing.Color.White;
        }

        private void btnclose_MouseEnter(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = false;
            btnclose.BackColor = System.Drawing.Color.FromArgb(248, 152, 94);
            btnclose.ForeColor = System.Drawing.Color.White;
        }

        private void btnclose_MouseLeave(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = true;
            btnclose.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnclose.ForeColor = System.Drawing.Color.White;
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

        private void chkfreeitem_CheckedChanged(object sender, EventArgs e)
        {
            if (chkfreeitem.Checked == true)
            {
                txtfreeitem.Visible = true;
                lblfreeqty.Visible = true;
                txtfreeqty.Visible = true;
                lblqty.Visible = true;
                txtqty.Visible = true;
                lblfreeitem.Visible = true;
                txtqty.Focus();
            }
            else
            {
                txtfreeitem.Visible = false;
                lblfreeqty.Visible = false;
                txtfreeqty.Visible = false;
                lblfreeitem.Visible = false;
                lblqty.Visible = false;
                txtqty.Visible = false;
            }
        }

        private void txtfreeitem_Enter(object sender, EventArgs e)
        {
            try
            {
                txtfreeitem.BackColor = Color.LightYellow;
                pnlallitem1.Visible = true;
                bindallitem1();
                //  lvallitem.Select();
                //lvallitem.Items[0].Selected = true;
            }
            catch
            {
            }
        }

        private void txtfreeitem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // lvallitem.Items[0].Selected = true;
                SqlCommand cmd = new SqlCommand("select Product_Name from productmaster where Product_Name like'%" + txtfreeitem.Text + "%' and isactive=1", con);
                SqlDataAdapter sda6 = new SqlDataAdapter(cmd);
                DataTable dtitem = new DataTable();
                sda6.Fill(dtitem);
                lvallitem1.Items.Clear();
                for (int i = 0; i < dtitem.Rows.Count; i++)
                {
                    ListViewItem li;
                    li = lvallitem1.Items.Add(dtitem.Rows[i]["Product_Name"].ToString());
                }
                //  lvallitem.Focus();
                if (txtfreeitem.Text == "" && txtfreeitem.Text == null)
                {
                    bindallitem1();
                }
            }
            catch
            {
            }
        }

        private void txtfreeitem_Leave(object sender, EventArgs e)
        {
            txtfreeitem.BackColor = Color.White;
            try
            {
                if (lvallitem1.Items[0].Selected == true)
                {
                    pnlallitem1.Visible = true;
                }
                else
                {
                    pnlallitem1.Visible = false;
                }
            }
            catch
            {
            }
        }

        private void txtfreeitem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                try
                {
                    // lvallitem.Focus();
                    lvallitem1.Items[0].Selected = true;
                    lvallitem1.Select();


                }
                catch
                {
                }
            }
            if (e.KeyCode == Keys.Enter)
            {
                txtfreeqty.Focus();
            }
        }

        private void lvallitem1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            String str = lvallitem1.Items[lvallitem1.FocusedItem.Index].SubItems[0].Text;
            //lvallitem.Items[0].Selected = false;
            txtfreeitem.Text = str;
            txtfreeitem.Focus();
        }

        private void lvallitem1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                String str = lvallitem1.Items[lvallitem1.FocusedItem.Index].SubItems[0].Text;
                //   lvallitem.Items[0].Selected = false;
                txtfreeitem.Text = str;
                txtfreeitem.Focus();
            }
        }

        private void cmbdiscount_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbdiscount.SelectedIndex = 0;
                cmbdiscount.DroppedDown = true;
            }
            catch
            {
            }
        }
        public static string s;
        private void cmbdiscount_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbdiscount.Items.Count; i++)
                {
                    s = cmbdiscount.GetItemText(cmbdiscount.Items[i]);
                    if (s == cmbdiscount.Text)
                    {
                        inList = true;
                        cmbdiscount.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbdiscount.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void cmbdiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbdiscount.Items.Count; i++)
                {
                    s = cmbdiscount.GetItemText(cmbdiscount.Items[i]);
                    if (s == cmbdiscount.Text)
                    {
                        inList = true;
                        cmbdiscount.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbdiscount.Text = "";
                }
                txtdiscount.Focus();
            }
        }

        private void txtfreeqty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (chkdiscount.Checked == true)
                {
                    cmbdiscount.Focus();
                }
                else
                {
                    btnsave.Focus();
                }
            }
        }

        private void txtqty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtfreeitem.Focus();
            }
        }

        private void txtqty_Leave(object sender, EventArgs e)
        {
            txtqty.BackColor = Color.White;
        }

        private void txtqty_Enter(object sender, EventArgs e)
        {
            txtqty.BackColor = Color.LightYellow;
        }

        private void chkdiscount_CheckedChanged(object sender, EventArgs e)
        {
            if (chkdiscount.Checked == true)
            {
                lbldisvalue.Visible = true;
                lblenterdis.Visible = true;
                cmbdiscount.Visible = true;
                txtdiscount.Visible = true;
                cmbdiscount.Focus();
            }
            else
            {
                lbldisvalue.Visible = false;
                lblenterdis.Visible = false;
                cmbdiscount.Visible = false;
                txtdiscount.Visible = false;
            }
        }

        private void txtdiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnsave.Focus();
            }
        }
    }
}
