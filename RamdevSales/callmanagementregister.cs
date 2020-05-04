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
    public partial class callmanagementregister : Form
    {
        private Master master;
        private TabControl tabControl;
        Connection conn = new Connection();
        OleDbSettings ods = new OleDbSettings();
        DataTable main = new DataTable();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        public callmanagementregister()
        {
            InitializeComponent();
        }

        public callmanagementregister(Master master, TabControl tabControl)
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

        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }

        }

        private void callmanagementregister_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
                DTPFrom.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                DTPFrom.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                DTPTo.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                DTPTo.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                DTPFrom.Value = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                DTPFrom.CustomFormat = Master.dateformate;
                DTPTo.CustomFormat = Master.dateformate;
                this.ActiveControl = DTPFrom;
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
                cmbstatus.Focus();
            }
        }
        ListViewItem li;
        public void binddata()
        {
            try
            {
                LVcall.Columns.Clear();
                if (cmbstatus.Text == "Complain Received")
                {
                    LVcall.Columns.Add("Date", 120, HorizontalAlignment.Left);
                    LVcall.Columns.Add("Complain ID", 120, HorizontalAlignment.Left);
                    LVcall.Columns.Add("Party Name", 200, HorizontalAlignment.Left);
                    LVcall.Columns.Add("Item Name", 300, HorizontalAlignment.Left);
                    LVcall.Columns.Add("Serial No", 140, HorizontalAlignment.Left);
                    LVcall.Columns.Add("New Serial No", 140, HorizontalAlignment.Left);
                    LVcall.Columns.Add("Status", 300, HorizontalAlignment.Left);
                }
                else if (cmbstatus.Text == "Send To Company")
                {
                    LVcall.Columns.Add("Date", 120, HorizontalAlignment.Left);
                    LVcall.Columns.Add("Party Name", 200, HorizontalAlignment.Left);
                    LVcall.Columns.Add("Complain ID", 120, HorizontalAlignment.Left);
                    LVcall.Columns.Add("Company ID", 120, HorizontalAlignment.Left);
                    LVcall.Columns.Add("Item Name", 300, HorizontalAlignment.Left);
                    LVcall.Columns.Add("Serial No", 140, HorizontalAlignment.Left);
                    LVcall.Columns.Add("New Serial No", 140, HorizontalAlignment.Left);
                    LVcall.Columns.Add("Status", 300, HorizontalAlignment.Left);
                }
                else if (cmbstatus.Text == "Product Received From Company")
                {
                    LVcall.Columns.Add("Date", 120, HorizontalAlignment.Left);
                    LVcall.Columns.Add("Party Name", 200, HorizontalAlignment.Left);
                    LVcall.Columns.Add("Complain ID", 120, HorizontalAlignment.Left);
                    LVcall.Columns.Add("Receive ID", 120, HorizontalAlignment.Left);
                    LVcall.Columns.Add("Item Name", 300, HorizontalAlignment.Left);
                    LVcall.Columns.Add("Serial No", 140, HorizontalAlignment.Left);
                    LVcall.Columns.Add("New Serial No", 140, HorizontalAlignment.Left);
                    LVcall.Columns.Add("Status", 300, HorizontalAlignment.Left);
                }
                else
                {
                    LVcall.Columns.Add("Date", 120, HorizontalAlignment.Left);
                    LVcall.Columns.Add("Party Name", 200, HorizontalAlignment.Left);
                    LVcall.Columns.Add("Complain ID", 120, HorizontalAlignment.Left);
                    LVcall.Columns.Add("SendToCustomer ID", 150, HorizontalAlignment.Left);
                    LVcall.Columns.Add("Item Name", 300, HorizontalAlignment.Left);
                    LVcall.Columns.Add("Serial No", 140, HorizontalAlignment.Left);
                    LVcall.Columns.Add("New Serial No", 140, HorizontalAlignment.Left);
                    LVcall.Columns.Add("Status", 300, HorizontalAlignment.Left);
                }
                LVcall.Items.Clear();
                if (cmbstatus.Text == "Complain Received")
                {
                    DataTable dt = conn.getdataset("select DISTINCT c.id,c.customername,c.date from tblcomplainmaster c inner join tblitemcomplainmaster ci on c.id=ci.complainID where c.isactive=1 and ci.isactive=1 and ci.status='Complain Received' and c.date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and c.date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by c.date");
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            li = LVcall.Items.Add(Convert.ToDateTime(dt.Rows[i]["date"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add(dt.Rows[i]["id"].ToString());
                            li.SubItems.Add(dt.Rows[i]["customername"].ToString());
                            DataTable dt1 = conn.getdataset("select * from tblitemcomplainmaster where isactive=1 and status='Complain Received' and complainID='" + dt.Rows[i]["id"].ToString() + "'");
                            for (int j = 0; j < dt1.Rows.Count; j++)
                            {
                                if (j == 0)
                                {
                                    li.SubItems.Add(dt1.Rows[j]["Itemname"].ToString());
                                    li.SubItems.Add(dt1.Rows[j]["serialno"].ToString());
                                    li.SubItems.Add("            -");
                                    li.SubItems.Add(dt1.Rows[j]["status"].ToString());
                                }
                                else
                                {
                                    li = LVcall.Items.Add("");
                                    li.SubItems.Add("");
                                    li.SubItems.Add("");
                                    li.SubItems.Add(dt1.Rows[j]["Itemname"].ToString());
                                    li.SubItems.Add(dt1.Rows[j]["serialno"].ToString());
                                    li.SubItems.Add("            -");
                                    li.SubItems.Add(dt1.Rows[j]["status"].ToString());
                                }
                            }
                        }
                    }
                }
                else if (cmbstatus.Text == "Send To Company")
                {
                    DataTable dt = conn.getdataset("select DISTINCT sc.id,sc.date,sc.suppliername from tblsendtocompanymaster sc inner join tblsendtocompanyitemmaster sci on sc.id=sci.sendtocompanyID where sc.isactive=1 and sci.isactive=1 and sc.date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and sc.date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by sc.date asc");
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            li = LVcall.Items.Add(Convert.ToDateTime(dt.Rows[i]["date"].ToString()).ToString(Master.dateformate));
                            // li.SubItems.Add(dt.Rows[i]["id"].ToString());
                            li.SubItems.Add(dt.Rows[i]["suppliername"].ToString());
                            DataTable dt1 = conn.getdataset("select * from tblsendtocompanyitemmaster where isactive=1 and  sendtocompanyid='" + dt.Rows[i]["id"].ToString() + "'");
                            for (int j = 0; j < dt1.Rows.Count; j++)
                            {
                                if (j == 0)
                                {
                                    li.SubItems.Add(dt1.Rows[j]["complainid"].ToString());
                                    li.SubItems.Add(dt1.Rows[j]["sendtocompanyid"].ToString());
                                    li.SubItems.Add(dt1.Rows[j]["Itemname"].ToString());
                                    li.SubItems.Add(dt1.Rows[j]["serialno"].ToString());
                                    li.SubItems.Add("            -");
                                    li.SubItems.Add("Send To Company");
                                }
                                else
                                {
                                    li = LVcall.Items.Add("");
                                    li.SubItems.Add("");
                                    li.SubItems.Add(dt1.Rows[j]["complainid"].ToString());
                                    li.SubItems.Add(dt1.Rows[j]["sendtocompanyid"].ToString());
                                    li.SubItems.Add(dt1.Rows[j]["Itemname"].ToString());
                                    li.SubItems.Add(dt1.Rows[j]["serialno"].ToString());
                                    li.SubItems.Add("            -");
                                    li.SubItems.Add("Send To Company");
                                }
                            }
                        }
                    }
                }
                else if (cmbstatus.Text == "Product Received From Company")
                {
                    DataTable dt = conn.getdataset("select DISTINCT sc.id,sc.date,sc.suppliername from tblreceivefromcompany sc inner join tblitemreceivefromcompany sci on sc.id=sci.receivefromcompanyid where sc.isactive=1 and sci.isactive=1 and sc.date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and sc.date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by sc.date asc");
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            li = LVcall.Items.Add(Convert.ToDateTime(dt.Rows[i]["date"].ToString()).ToString(Master.dateformate));
                            //   li.SubItems.Add(dt.Rows[i]["id"].ToString());
                            li.SubItems.Add(dt.Rows[i]["suppliername"].ToString());
                            DataTable dt1 = conn.getdataset("select * from tblitemreceivefromcompany where isactive=1 and  receivefromcompanyid='" + dt.Rows[i]["id"].ToString() + "'");
                            for (int j = 0; j < dt1.Rows.Count; j++)
                            {
                                if (j == 0)
                                {
                                    li.SubItems.Add(dt1.Rows[j]["complainid"].ToString());
                                    li.SubItems.Add(dt1.Rows[j]["receivefromcompanyid"].ToString());
                                    li.SubItems.Add(dt1.Rows[j]["Itemname"].ToString());
                                    li.SubItems.Add(dt1.Rows[j]["serialno"].ToString());
                                    li.SubItems.Add(dt1.Rows[j]["newserialno"].ToString());
                                    li.SubItems.Add("Product Received From Company");
                                }
                                else
                                {
                                    li = LVcall.Items.Add("");
                                    li.SubItems.Add("");
                                    li.SubItems.Add(dt1.Rows[j]["complainid"].ToString());
                                    li.SubItems.Add(dt1.Rows[j]["receivefromcompanyid"].ToString());
                                    li.SubItems.Add(dt1.Rows[j]["Itemname"].ToString());
                                    li.SubItems.Add(dt1.Rows[j]["serialno"].ToString());
                                    li.SubItems.Add(dt1.Rows[j]["newserialno"].ToString());
                                    li.SubItems.Add("Product Received From Company");
                                }
                            }
                        }



                    }
                }
                else
                {
                    DataTable dt = conn.getdataset("select DISTINCT sc.id,sc.date,sc.customername from tblsendtocustomer sc inner join tblitemsendtocustomer sci on sc.id=sci.sendtocustomerid where sc.isactive=1 and sci.isactive=1 and sc.date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and sc.date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by sc.date asc");
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            li = LVcall.Items.Add(Convert.ToDateTime(dt.Rows[i]["date"].ToString()).ToString(Master.dateformate));
                            // li.SubItems.Add(dt.Rows[i]["id"].ToString());
                            li.SubItems.Add(dt.Rows[i]["customername"].ToString());
                            DataTable dt1 = conn.getdataset("select * from tblitemsendtocustomer where isactive=1 and  sendtocustomerid='" + dt.Rows[i]["id"].ToString() + "'");
                            for (int j = 0; j < dt1.Rows.Count; j++)
                            {
                                if (j == 0)
                                {
                                    li.SubItems.Add(dt1.Rows[j]["complainid"].ToString());
                                    li.SubItems.Add(dt1.Rows[j]["sendtocustomerid"].ToString());
                                    li.SubItems.Add(dt1.Rows[j]["Itemname"].ToString());
                                    li.SubItems.Add(dt1.Rows[j]["serialno"].ToString());
                                    li.SubItems.Add(dt1.Rows[j]["newserialno"].ToString());
                                    li.SubItems.Add("Product Sent To Customer");
                                }
                                else
                                {
                                    li = LVcall.Items.Add("");
                                    li.SubItems.Add("");
                                    li.SubItems.Add(dt1.Rows[j]["complainid"].ToString());
                                    li.SubItems.Add(dt1.Rows[j]["sendtocustomerid"].ToString());
                                    li.SubItems.Add(dt1.Rows[j]["Itemname"].ToString());
                                    li.SubItems.Add(dt1.Rows[j]["serialno"].ToString());
                                    li.SubItems.Add(dt1.Rows[j]["newserialno"].ToString());
                                    li.SubItems.Add("Product Sent To Customer");
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }
        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            binddata();
        }

        private void cmbstatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbstatus.Items.Count; i++)
                {
                    s = cmbstatus.GetItemText(cmbstatus.Items[i]);
                    if (s == cmbstatus.Text)
                    {
                        inList = true;
                        cmbstatus.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbstatus.Text = "";
                }

                BtnViewReport.Focus();
            }
        }

        private void cmbstatus_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbstatus.SelectedIndex = 0;
                cmbstatus.DroppedDown = true;
            }
            catch
            {
            }
        }
        public static string s;
        private void cmbstatus_Leave(object sender, EventArgs e)
        {
            try
            {
                cmbstatus.Text = s;
            }
            catch
            {
            }
        }

        private void cmbstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbstatus.Items.Count; i++)
                {
                    s = cmbstatus.GetItemText(cmbstatus.Items[i]);
                    if (s == cmbstatus.Text)
                    {
                        inList = true;
                        cmbstatus.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbstatus.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void BtnViewReport_Enter(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = false;
            BtnViewReport.BackColor = System.Drawing.Color.FromArgb(94, 191, 174);
            BtnViewReport.ForeColor = System.Drawing.Color.White;
        }

        private void BtnViewReport_Leave(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = true;
            BtnViewReport.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            BtnViewReport.ForeColor = System.Drawing.Color.White;
        }

        private void BtnViewReport_MouseEnter(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = false;
            BtnViewReport.BackColor = System.Drawing.Color.FromArgb(94, 191, 174);
            BtnViewReport.ForeColor = System.Drawing.Color.White;
        }

        private void BtnViewReport_MouseLeave(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = true;
            BtnViewReport.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            BtnViewReport.ForeColor = System.Drawing.Color.White;
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
        Printing prn = new Printing();
        private void btnprint_Click(object sender, EventArgs e)
        {
            DialogResult dr1 = MessageBox.Show("Do you want to Print ?", "Print", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr1 == DialogResult.Yes)
            {
                prn.execute("delete from printing");
                string status;
                int j = 1;
                status = "Call Management From" + DTPFrom.Text;
                for (int i = 0; i < LVcall.Items.Count; i++)
                {
                    if (cmbstatus.Text == "Complain Received")
                    {
                        DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1 and CompanyID='" + Master.companyId + "' ");
                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24)VALUES";
                        qry += "('" + dt1.Rows[0]["CompanyName"].ToString() + "','" + dt1.Rows[0]["SubName"].ToString() + "','" + dt1.Rows[0]["Address"].ToString() + "','" + dt1.Rows[0]["Address2"].ToString() + "','" + dt1.Rows[0]["City"].ToString() + "','" + dt1.Rows[0]["State"].ToString() + "','" + dt1.Rows[0]["Country"].ToString() + "','" + dt1.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Mobile"].ToString() + "','" + dt1.Rows[0]["Email"].ToString() + "','" + dt1.Rows[0]["CSTNo"].ToString() + "','" + status + "','" + LVcall.Items[i].SubItems[0].Text + "','" + LVcall.Items[i].SubItems[1].Text + "','" + LVcall.Items[i].SubItems[2].Text + "','" + LVcall.Items[i].SubItems[3].Text + "','" + LVcall.Items[i].SubItems[4].Text + "','" + LVcall.Items[i].SubItems[5].Text + "','" + LVcall.Items[i].SubItems[6].Text + "','" + DTPFrom.Text + "','" + DTPTo.Text + "','" + cmbstatus.Text + "','" + dt1.Rows[0]["Website"].ToString() + "','" + j++ + "')";
                        prn.execute(qry);
                    }
                    else if (cmbstatus.Text == "Send To Company")
                    {
                        DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1 and CompanyID='" + Master.companyId + "' ");
                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25)VALUES";
                        qry += "('" + dt1.Rows[0]["CompanyName"].ToString() + "','" + dt1.Rows[0]["SubName"].ToString() + "','" + dt1.Rows[0]["Address"].ToString() + "','" + dt1.Rows[0]["Address2"].ToString() + "','" + dt1.Rows[0]["City"].ToString() + "','" + dt1.Rows[0]["State"].ToString() + "','" + dt1.Rows[0]["Country"].ToString() + "','" + dt1.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Mobile"].ToString() + "','" + dt1.Rows[0]["Email"].ToString() + "','" + dt1.Rows[0]["CSTNo"].ToString() + "','" + status + "','" + LVcall.Items[i].SubItems[0].Text + "','" + LVcall.Items[i].SubItems[1].Text + "','" + LVcall.Items[i].SubItems[2].Text + "','" + LVcall.Items[i].SubItems[3].Text + "','" + LVcall.Items[i].SubItems[4].Text + "','" + LVcall.Items[i].SubItems[5].Text + "','" + LVcall.Items[i].SubItems[6].Text + "','" + LVcall.Items[i].SubItems[7].Text + "','" + DTPFrom.Text + "','" + DTPTo.Text + "','" + cmbstatus.Text + "','" + dt1.Rows[0]["Website"].ToString() + "','" + j++ + "')";
                        prn.execute(qry);
                    }
                    else if (cmbstatus.Text == "Product Received From Company")
                    {
                        DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1 and CompanyID='" + Master.companyId + "' ");
                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25)VALUES";
                        qry += "('" + dt1.Rows[0]["CompanyName"].ToString() + "','" + dt1.Rows[0]["SubName"].ToString() + "','" + dt1.Rows[0]["Address"].ToString() + "','" + dt1.Rows[0]["Address2"].ToString() + "','" + dt1.Rows[0]["City"].ToString() + "','" + dt1.Rows[0]["State"].ToString() + "','" + dt1.Rows[0]["Country"].ToString() + "','" + dt1.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Mobile"].ToString() + "','" + dt1.Rows[0]["Email"].ToString() + "','" + dt1.Rows[0]["CSTNo"].ToString() + "','" + status + "','" + LVcall.Items[i].SubItems[0].Text + "','" + LVcall.Items[i].SubItems[1].Text + "','" + LVcall.Items[i].SubItems[2].Text + "','" + LVcall.Items[i].SubItems[3].Text + "','" + LVcall.Items[i].SubItems[4].Text + "','" + LVcall.Items[i].SubItems[5].Text + "','" + LVcall.Items[i].SubItems[6].Text + "','" + LVcall.Items[i].SubItems[7].Text + "','" + DTPFrom.Text + "','" + DTPTo.Text + "','" + cmbstatus.Text + "','" + dt1.Rows[0]["Website"].ToString() + "','" + j++ + "')";
                        prn.execute(qry);
                    }
                    else
                    {
                        DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1 and CompanyID='" + Master.companyId + "' ");
                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25)VALUES";
                        qry += "('" + dt1.Rows[0]["CompanyName"].ToString() + "','" + dt1.Rows[0]["SubName"].ToString() + "','" + dt1.Rows[0]["Address"].ToString() + "','" + dt1.Rows[0]["Address2"].ToString() + "','" + dt1.Rows[0]["City"].ToString() + "','" + dt1.Rows[0]["State"].ToString() + "','" + dt1.Rows[0]["Country"].ToString() + "','" + dt1.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Mobile"].ToString() + "','" + dt1.Rows[0]["Email"].ToString() + "','" + dt1.Rows[0]["CSTNo"].ToString() + "','" + status + "','" + LVcall.Items[i].SubItems[0].Text + "','" + LVcall.Items[i].SubItems[1].Text + "','" + LVcall.Items[i].SubItems[2].Text + "','" + LVcall.Items[i].SubItems[3].Text + "','" + LVcall.Items[i].SubItems[4].Text + "','" + LVcall.Items[i].SubItems[5].Text + "','" + LVcall.Items[i].SubItems[6].Text + "','" + LVcall.Items[i].SubItems[7].Text + "','" + DTPFrom.Text + "','" + DTPTo.Text + "','" + cmbstatus.Text + "','" + dt1.Rows[0]["Website"].ToString() + "','" + j++ + "')";
                        prn.execute(qry);
                    }
                }
            }
        }
    }
}
