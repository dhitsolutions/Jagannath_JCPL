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
    public partial class FinishedGoods : Form
    {
        private Master master;
        private TabControl tabControl;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        public FinishedGoods()
        {
            InitializeComponent();
        }

        public FinishedGoods(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }

        public FinishedGoods(FinishedGoodsList finishedGoodsList, Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.finishedGoodsList = finishedGoodsList;
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
            return base.ProcessCmdKey(ref msg, keyData);
        }
        public void binditem()
        {
            SqlCommand cmd = new SqlCommand("select id,mainitemname from tblproductionmaster where isactive=1 and isfinished=0", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt11 = new DataTable();
            sda.Fill(dt11);

            cmbproductitem.ValueMember = "id";
            cmbproductitem.DisplayMember = "mainitemname";
            cmbproductitem.DataSource = dt11;
            cmbproductitem.SelectedIndex = -1;
        }
        private void btncancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }
        int cnt = 0;
        DataTable userrights = new DataTable();
        private void FinishedGoods_Load(object sender, EventArgs e)
        {
            try
            {
                if (cnt == 0)
                {
                    userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[19]["d"].ToString() == "False")
                        {
                            btndelete.Enabled = false;
                        }
                    }
                    binditem();
                    this.ActiveControl = cmbproductitem;
                }
            }
            catch
            {
            }
        }

        private void cmbproductitem_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbproductitem.SelectedIndex = 0;
                cmbproductitem.DroppedDown = true;
            }
            catch
            {
            }
        }

        private void cmbproductitem_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool inList = false;
            for (int i = 0; i < cmbproductitem.Items.Count; i++)
            {
                s = cmbproductitem.GetItemText(cmbproductitem.Items[i]);
                if (s == cmbproductitem.Text)
                {
                    inList = true;
                    cmbproductitem.Text = s;
                    break;
                }
            }
            if (!inList)
            {
                cmbproductitem.Text = "";
            }
        }
        public static string s;
        private FinishedGoodsList finishedGoodsList;
        private void cmbproductitem_Leave(object sender, EventArgs e)
        {
            cmbproductitem.Text = s;
        }
        public void bindprodata()
        {
            try
            {
               DataTable dt= conn.getdataset("select * from tblproductionmaster where isactive=1 and id='"+cmbproductitem.SelectedValue+"'");
               txtaqty.Text = dt.Rows[0]["mqty"].ToString();
               txtaltqty.Text = dt.Rows[0]["maqty"].ToString();

            }
            catch
            {
            }
        }
        private void cmbproductitem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbproductitem.Items.Count; i++)
                {
                    s = cmbproductitem.GetItemText(cmbproductitem.Items[i]);
                    if (s == cmbproductitem.Text)
                    {
                        inList = true;
                        cmbproductitem.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbproductitem.Text = "";
                }
                if (btnsubmit.Text != "Update")
                {
                    bindprodata();
                }
                txtfqty.Focus();
            }
        }

        private void txtfqty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtremarks.Focus();
            }
        }

        private void txtremarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnsubmit.Focus();
            }
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
        public void clearall()
        {
            cmbproductitem.SelectedIndex = -1;
            txtaqty.Text = "";
            txtfqty.Text = "";
            txtremarks.Text = "";
            txtaltqty.Text = "";
            btnsubmit.Text = "Submit";
        }
        public void binddata()
        {
            try
            {
                if (btnsubmit.Text == "Update")
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[19]["u"].ToString() == "True")
                        {
                            conn.execute("Update tblfinishedgoodsqty set proitem='" + cmbproductitem.Text + "',aqty='" + txtaqty.Text + "',altqty='" + txtaltqty.Text + "',fqty='" + txtfqty.Text + "',remarks='" + txtremarks.Text + "' Where proitemid='" + lblid.Text + "'");
                            MessageBox.Show("Data Update Successfully.");
                            clearall();
                        }
                        else
                        {
                            MessageBox.Show("You don't have Permission To Update");
                            return;
                        }
                    }
                }
                else
                {
                    conn.execute("INSERT INTO [dbo].[tblfinishedgoodsqty]([proitemid],[proitem],[aqty],[altqty],[fqty],[remarks],[isactive])VALUES('" + cmbproductitem.SelectedValue + "','" + cmbproductitem.Text + "','" + txtaqty.Text + "','"+txtaltqty.Text+"','" + txtfqty.Text + "','" + txtremarks.Text + "','" + "1" + "')");
                    conn.execute("Update tblproductionmaster set isfinished=1 where id='"+cmbproductitem.SelectedValue+"'");
                    MessageBox.Show("Data Inserted Successfully.");
                    clearall();
                }
            }
            catch
            {
            }
        }
        private void btnsubmit_Click(object sender, EventArgs e)
        {
            binddata();
        }


        internal void Updatedata(string iid)
        {
            try
            {
                userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[19]["d"].ToString() == "False")
                    {
                        btndelete.Enabled = false;
                    }
                }
                cnt = 1;
                binditem();
                DataTable d = conn.getdataset("select proitem,aqty,altqty,fqty,remarks,proitemid from tblfinishedgoodsqty where isactive=1 and proitemid='" + iid + "'");
                cmbproductitem.Text = d.Rows[0]["proitem"].ToString();
                txtaqty.Text = d.Rows[0]["aqty"].ToString();
                txtaltqty.Text = d.Rows[0]["altqty"].ToString();
                txtfqty.Text = d.Rows[0]["fqty"].ToString();
                txtremarks.Text = d.Rows[0]["remarks"].ToString();
                lblid.Text = d.Rows[0]["proitemid"].ToString();
                btnsubmit.Text = "Update";

                this.ActiveControl = cmbproductitem;

            }
            catch
            {
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            DialogResult dr1 = MessageBox.Show("Do you want to Delete?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr1 == DialogResult.Yes)
            {
                conn.execute("Update tblfinishedgoodsqty set isactive='0' where proitemid='" + lblid.Text + "'");
                conn.execute("Update tblproductionmaster set isfinished=0 where id='" + lblid.Text + "'");
                MessageBox.Show("Data Delete Successfully.");
                clearall();
            }

        }
    }
}
