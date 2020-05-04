using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RamdevSales
{
    public partial class FinishedGoodsList : Form
    {
        private Master master;
        private TabControl tabControl;
        Connection conn = new Connection();

        public FinishedGoodsList()
        {
            InitializeComponent();
        }

        public FinishedGoodsList(Master master, TabControl tabControl)
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
        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }
        int cnt = 0;
        public void binddata()
        {
            try
            {
                lvprolist.Items.Clear();
                DataTable dt = conn.getdataset("select proitem,aqty,altqty,fqty,remarks,proitemid from tblfinishedgoodsqty where isactive=1");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        lvprolist.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                        lvprolist.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                        lvprolist.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                        lvprolist.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                        lvprolist.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                        lvprolist.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                    }
                }
            }
            catch
            {
            }
        }
        DataTable userrights = new DataTable();
        private void FinishedGoodsList_Load(object sender, EventArgs e)
        {
            try
            {
                if (cnt == 0)
                {
                    userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[19]["p"].ToString() == "False")
                        {
                            btnprint.Enabled = false;
                        }
                    }
                    lvprolist.Columns.Add("Production Item", 300, HorizontalAlignment.Left);
                    lvprolist.Columns.Add("Actual Item Qty", 180, HorizontalAlignment.Left);
                    lvprolist.Columns.Add("Alt Item Qty", 180, HorizontalAlignment.Left);
                    lvprolist.Columns.Add("Final Finished Goods Qty", 180, HorizontalAlignment.Left);
                    lvprolist.Columns.Add("Remarks", 200, HorizontalAlignment.Left);
                    lvprolist.Columns.Add("id",10, HorizontalAlignment.Left);
                    binddata();
                    this.ActiveControl = btnsearch;
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
        public static string iid = "";
        public void Open()
        {
            try
            {
                iid = lvprolist.Items[lvprolist.FocusedItem.Index].SubItems[5].Text;
                FinishedGoods p = new FinishedGoods(this, master, tabControl);
                p.Updatedata(iid);
                master.AddNewTab(p);
            }
            catch
            {
            }
        }
        private void lvprolist_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[19]["v"].ToString() == "True" || userrights.Rows[19]["u"].ToString() == "True")
                {
                    Open();
                }
                else
                {
                    MessageBox.Show("You don't have Permission To View");
                    return;
                }
            }
        }

        private void lvprolist_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[19]["v"].ToString() == "True" || userrights.Rows[19]["u"].ToString() == "True")
                    {
                        Open();
                    }
                    else
                    {
                        MessageBox.Show("You don't have Permission To View");
                        return;
                    }
                }
            }
        }
        Printing prn = new Printing();
        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Print FinishedGoods?", "FinishedGoods", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                 if (dr1 == DialogResult.Yes)
                 {
                     prn.execute("delete from printing");
                     string status;
                     status = "FINISHED GOODS";
                     for (int i = 0; i < lvprolist.Items.Count; i++)
                     {
                         DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1 and CompanyID='" + Master.companyId + "' ");
                         string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18)VALUES";
                         qry += "('" + dt1.Rows[0]["CompanyName"].ToString() + "','" + dt1.Rows[0]["SubName"].ToString() + "','" + dt1.Rows[0]["Address"].ToString() + "','" + dt1.Rows[0]["Address2"].ToString() + "','" + dt1.Rows[0]["City"].ToString() + "','" + dt1.Rows[0]["State"].ToString() + "','" + dt1.Rows[0]["Country"].ToString() + "','" + dt1.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Mobile"].ToString() + "','" + dt1.Rows[0]["Email"].ToString() + "','" + dt1.Rows[0]["CSTNo"].ToString() + "','" + status + "','" + lvprolist.Items[i].SubItems[0].Text + "','" + lvprolist.Items[i].SubItems[1].Text + "','" + lvprolist.Items[i].SubItems[2].Text + "','" + lvprolist.Items[i].SubItems[3].Text + "','" + lvprolist.Items[i].SubItems[4].Text + "','" + dt1.Rows[0]["WebSite"].ToString() + "')";
                         prn.execute(qry);
                     }
                     string reportName = "FinishedGoodsList";
                     Print popup = new Print(reportName);
                     popup.ShowDialog();
                     popup.Dispose();
                 }
            }
            catch
            {
            }
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
    }
}
