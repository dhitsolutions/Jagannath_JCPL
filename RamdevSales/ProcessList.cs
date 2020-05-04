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
    public partial class ProcessList : Form
    {
        private Master master;
        private TabControl tabControl;
        Connection conn = new Connection();
        public ProcessList()
        {
            InitializeComponent();
        }

        public ProcessList(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            try
            {
                Process p = new Process(master,tabControl);
                master.AddNewTab(p);
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
        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
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

        private void btnprint_MouseHover(object sender, EventArgs e)
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

        private void btnnew_Enter(object sender, EventArgs e)
        {
            btnnew.UseVisualStyleBackColor = false;
            btnnew.BackColor = System.Drawing.Color.FromArgb(9, 106, 3);
            btnnew.ForeColor = System.Drawing.Color.White;
        }

        private void btnnew_Leave(object sender, EventArgs e)
        {
            btnnew.UseVisualStyleBackColor = true;
            btnnew.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnnew.ForeColor = System.Drawing.Color.White;
        }

        private void btnnew_MouseEnter(object sender, EventArgs e)
        {
            btnnew.UseVisualStyleBackColor = false;
            btnnew.BackColor = System.Drawing.Color.FromArgb(9, 106, 3);
            btnnew.ForeColor = System.Drawing.Color.White;
        }

        private void btnnew_MouseHover(object sender, EventArgs e)
        {
            btnnew.UseVisualStyleBackColor = true;
            btnnew.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnnew.ForeColor = System.Drawing.Color.White;
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

        private void btnsearch_MouseHover(object sender, EventArgs e)
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
        public void binddata()
        {
            try
            {
                lvprocess.Items.Clear();
                DataTable dt = conn.getdataset("select processname,mainitemname,mqty,munit,maqty,maunit,id from tblprocessmaster where isactive=1");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        lvprocess.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                        lvprocess.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                        lvprocess.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                        lvprocess.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                        lvprocess.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                        lvprocess.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                        lvprocess.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                    }
                }
            }
            catch
            {
            }

        }
        DataTable userrights = new DataTable();
        private void ProcessList_Load(object sender, EventArgs e)
        {
            try
            {
                userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[19]["a"].ToString() == "False")
                    {
                        btnnew.Enabled = false;
                    }
                    if (userrights.Rows[19]["p"].ToString() == "False")
                    {
                        btnprint.Enabled = false;
                    }
                }
                lvprocess.Columns.Add("Process Name", 300, HorizontalAlignment.Left);
                lvprocess.Columns.Add("Item Name",250, HorizontalAlignment.Left);
                lvprocess.Columns.Add("Main Qty", 100, HorizontalAlignment.Left);
                lvprocess.Columns.Add("Unit", 100, HorizontalAlignment.Left);
                lvprocess.Columns.Add("Alt Qty", 100, HorizontalAlignment.Left);
                lvprocess.Columns.Add("Unit", 100, HorizontalAlignment.Left);
                lvprocess.Columns.Add("processid",0, HorizontalAlignment.Left);
                binddata();
                this.ActiveControl = btnsearch;
            }
            catch
            {
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkdisactiveprocess.Checked == true && chkactiveprocess.Checked == true)
                {
                    binddata();
                }
                else if (chkactiveprocess.Checked == true)
                {
                    lvprocess.Items.Clear();
                    DataTable dt = conn.getdataset("select processname,mainitemname,mqty,munit,maqty,maunit,id from tblprocessmaster where isactive=1 and isactiveprocess=1");
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            lvprocess.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                            lvprocess.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                            lvprocess.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                            lvprocess.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                            lvprocess.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                            lvprocess.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                            lvprocess.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                        }
                    }
                }
                else if(chkdisactiveprocess.Checked==true)
                {
                    lvprocess.Items.Clear();
                    DataTable dt = conn.getdataset("select processname,mainitemname,mqty,munit,maqty,maunit,id from tblprocessmaster where isactive=1 and isactiveprocess=0");
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            lvprocess.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                            lvprocess.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                            lvprocess.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                            lvprocess.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                            lvprocess.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                            lvprocess.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                            lvprocess.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                        }
                    }
                }
                
            }
            catch
            {
            }
        }

        private void lvprocess_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[19]["v"].ToString() == "True" || userrights.Rows[19]["u"].ToString() == "True")
                {
                    open();
                }
                else
                {
                    MessageBox.Show("You don't have Permission To View");
                    return;
                }
            }
        }
        public static string iid = "";
        public void open()
        {
            try
            {
                iid = lvprocess.Items[lvprocess.FocusedItem.Index].SubItems[6].Text;
                Process p = new Process(this,master,tabControl);
                p.Updatedata(iid);
                master.AddNewTab(p);
            }
            catch
            {
            }
        }
        private void lvprocess_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[19]["v"].ToString() == "True" || userrights.Rows[19]["u"].ToString() == "True")
                    {
                        open();
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
                DialogResult dr1 = MessageBox.Show("Do you want to Print Process?", "Process", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    prn.execute("delete from printing");
                    string status;
                    status = "PROCESS REGISTER";
                    for (int i = 0; i < lvprocess.Items.Count; i++)
                    {
                        DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1 and CompanyID='" + Master.companyId + "' ");
                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19)VALUES";
                        qry += "('" + dt1.Rows[0]["CompanyName"].ToString() + "','" + dt1.Rows[0]["SubName"].ToString() + "','" + dt1.Rows[0]["Address"].ToString() + "','" + dt1.Rows[0]["Address2"].ToString() + "','" + dt1.Rows[0]["City"].ToString() + "','" + dt1.Rows[0]["State"].ToString() + "','" + dt1.Rows[0]["Country"].ToString() + "','" + dt1.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Mobile"].ToString() + "','" + dt1.Rows[0]["Email"].ToString() + "','" + dt1.Rows[0]["CSTNo"].ToString() + "','" + status + "','" + lvprocess.Items[i].SubItems[0].Text + "','" + lvprocess.Items[i].SubItems[1].Text + "','" + lvprocess.Items[i].SubItems[2].Text + "','" + lvprocess.Items[i].SubItems[3].Text + "','" + lvprocess.Items[i].SubItems[4].Text + "','" + lvprocess.Items[i].SubItems[5].Text + "','" + dt1.Rows[0]["Website"].ToString() + "')";
                        prn.execute(qry);
                    }
                    string reportName = "ProductionProcessRegister";
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
