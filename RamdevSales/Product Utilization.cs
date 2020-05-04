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
    public partial class Product_Utilization : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        public Product_Utilization()
        {
            InitializeComponent();
        }

        public Product_Utilization(Master master, TabControl tabControl)
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
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void btnsearch_Enter(object sender, EventArgs e)
        {
            btnsearch.UseVisualStyleBackColor = false;
            btnsearch.BackColor = System.Drawing.Color.FromArgb(94, 191, 174);
            btnsearch.ForeColor = System.Drawing.Color.White;
        }

        private void btnsearch_Leave(object sender, EventArgs e)
        {
            btnsearch.UseVisualStyleBackColor = true;
            btnsearch.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnsearch.ForeColor = System.Drawing.Color.White;
        }

        private void btnsearch_MouseEnter(object sender, EventArgs e)
        {
            btnsearch.UseVisualStyleBackColor = false;
            btnsearch.BackColor = System.Drawing.Color.FromArgb(94, 191, 174);
            btnsearch.ForeColor = System.Drawing.Color.White;
        }

        private void btnsearch_MouseLeave(object sender, EventArgs e)
        {
            btnsearch.UseVisualStyleBackColor = true;
            btnsearch.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnsearch.ForeColor = System.Drawing.Color.White;
        }

        private void btnprint_Enter(object sender, EventArgs e)
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

        private void btnprint_MouseEnter(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = false;
            btnprint.BackColor = System.Drawing.Color.FromArgb(176, 111, 193);
            btnprint.ForeColor = System.Drawing.Color.White;
        }

        private void btnprint_MouseLeave(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = true;
            btnprint.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnprint.ForeColor = System.Drawing.Color.White;
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
        public void binditem()
        {
            SqlCommand cmd = new SqlCommand("select ProductID,Product_Name from ProductMaster where isactive=1 order by Product_Name", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt11 = new DataTable();
            sda.Fill(dt11);

            cmbitem.ValueMember = "ProductID";
            cmbitem.DisplayMember = "Product_Name";
            cmbitem.DataSource = dt11;
            cmbitem.SelectedIndex = -1;
        }
        int cnt = 0;
        private void Product_Utilization_Load(object sender, EventArgs e)
        {
            try
            {
                if (cnt == 0)
                {
                    lvproduct.Columns.Add("Process",200, HorizontalAlignment.Left);
                    lvproduct.Columns.Add("Item Consumed", 200, HorizontalAlignment.Left);
                    lvproduct.Columns.Add("Cons.Qty", 120, HorizontalAlignment.Left);
                    lvproduct.Columns.Add("Unit", 100, HorizontalAlignment.Left);
                    lvproduct.Columns.Add("Item Qty", 120, HorizontalAlignment.Left);
                    lvproduct.Columns.Add("Unit", 100, HorizontalAlignment.Left);
                    lvproduct.Columns.Add("Type", 100, HorizontalAlignment.Left);
                    binditem();
                    this.ActiveControl = cmbitem;
                }
            }
            catch
            {
            }
        }
        public static string s;
        private Master master;
        private TabControl tabControl;
        private void cmbitem_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbitem.SelectedIndex = 0;
                cmbitem.DroppedDown = true;
            }
            catch
            {
            }
        }
        string productName = "";
        private void cmbitem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    productName = cmbitem.Text;
                    e.SuppressKeyPress = true; // This will eliminate the beeping
                    bool inList = false;
                    for (int i = 0; i < cmbitem.Items.Count; i++)
                    {
                        s = cmbitem.GetItemText(cmbitem.Items[i]);
                        if (s == cmbitem.Text)
                        {
                            inList = true;
                            cmbitem.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        cmbitem.Text = "";
                    }
                    btnsearch.Focus();
                }
            }
            catch
            {
            }
        }

        private void cmbitem_Leave(object sender, EventArgs e)
        {
            cmbitem.Text = s;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }
        public void binddata()
        {
            try
            {
                lvproduct.Items.Clear();
                DataTable dt = conn.getdataset("select p.processname,p.mainitemname,p.mqty,p.munit,r.rowqty,r.rowunit from tblprocessmaster p inner join tblrowmaterialsmaster r on p.id=r.processid where p.isactive=1 and r.isactive=1 and(p.mainitemname='" + cmbitem.Text + "' or r.rowitemname='" + cmbitem.Text + "')");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lvproduct.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                        lvproduct.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                        lvproduct.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                        lvproduct.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                        lvproduct.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                        lvproduct.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                        lvproduct.Items[i].SubItems.Add("Raw Material");
                    }
                }
                DataTable dt1 = conn.getdataset("select p.processname,p.mainitemname,p.mqty,p.munit,pg.proqty,pg.prounit from tblprocessmaster p inner join tblproductgeneratedmaster pg on p.id=pg.processid where p.isactive=1 and pg.isactive=1 and(p.mainitemname='" + cmbitem.Text + "' or pg.proitemname='" + cmbitem.Text + "')");
                if (dt1.Rows.Count > 0)
                {
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        ListViewItem li;
                        li = lvproduct.Items.Add(dt1.Rows[j]["processname"].ToString());
                        li.SubItems.Add(dt1.Rows[j]["mainitemname"].ToString());
                        li.SubItems.Add(dt1.Rows[j]["mqty"].ToString());
                        li.SubItems.Add(dt1.Rows[j]["munit"].ToString());
                        li.SubItems.Add(dt1.Rows[j]["proqty"].ToString());
                        li.SubItems.Add(dt1.Rows[j]["prounit"].ToString());
                        li.SubItems.Add("By Product");
                        //lvproduct.Items.Add(dt1.Rows[j].ItemArray[0].ToString());
                        //lvproduct.Items[j].SubItems.Add(dt1.Rows[j].ItemArray[1].ToString());
                        //lvproduct.Items[j].SubItems.Add(dt1.Rows[j].ItemArray[2].ToString());
                        //lvproduct.Items[j].SubItems.Add(dt1.Rows[j].ItemArray[3].ToString());
                        //lvproduct.Items[j].SubItems.Add(dt1.Rows[j].ItemArray[4].ToString());
                        //lvproduct.Items[j].SubItems.Add(dt1.Rows[j].ItemArray[5].ToString());
                        //lvproduct.Items[j].SubItems.Add("By Product");
                    }
                }

            }
            catch
            {
            }
        }
        private void btnsearch_Click(object sender, EventArgs e)
        {
            binddata();
        }

        private void cmbitem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbitem.Items.Count; i++)
                {
                    s = cmbitem.GetItemText(cmbitem.Items[i]);
                    if (s == cmbitem.Text)
                    {
                        inList = true;
                        cmbitem.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbitem.Text = "";
                }
            }
            catch
            {
            }
        }
        Printing prn = new Printing();
        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                 DialogResult dr1 = MessageBox.Show("Do you want to Print Process?", "Process", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                 if (dr1 == DialogResult.Yes)
                 {
                     prn.execute("delete from printing");
                     string status;
                     status = "PRODUCT UTILISATION REPORT OF " + productName;
                     for (int i = 0; i < lvproduct.Items.Count; i++)
                     {
                         DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1 and CompanyID='" + Master.companyId + "' ");
                         string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20)VALUES";
                         qry += "('" + dt1.Rows[0]["CompanyName"].ToString() + "','" + dt1.Rows[0]["SubName"].ToString() + "','" + dt1.Rows[0]["Address"].ToString() + "','" + dt1.Rows[0]["Address2"].ToString() + "','" + dt1.Rows[0]["City"].ToString() + "','" + dt1.Rows[0]["State"].ToString() + "','" + dt1.Rows[0]["Country"].ToString() + "','" + dt1.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Mobile"].ToString() + "','" + dt1.Rows[0]["Email"].ToString() + "','" + dt1.Rows[0]["CSTNo"].ToString() + "','" + status + "','" + lvproduct.Items[i].SubItems[0].Text + "','" + lvproduct.Items[i].SubItems[1].Text + "','" + lvproduct.Items[i].SubItems[2].Text + "','" + lvproduct.Items[i].SubItems[3].Text + "','" + lvproduct.Items[i].SubItems[4].Text + "','" + lvproduct.Items[i].SubItems[5].Text + "','" + lvproduct.Items[i].SubItems[6].Text + "','" + dt1.Rows[0]["WebSite"].ToString() + "')";
                         prn.execute(qry);
                     }
                     string reportName = "ProductionUtilization";
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
