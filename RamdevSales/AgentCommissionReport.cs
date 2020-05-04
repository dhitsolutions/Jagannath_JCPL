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
    public partial class AgentCommissionReport : Form
    {
        private Master master;
        private TabControl tabControl;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        OleDbSettings ods = new OleDbSettings();
        Printing prn = new Printing();

        public AgentCommissionReport()
        {
            InitializeComponent();
        }

        public AgentCommissionReport(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }
        public void binaagent()
        {
            string qry = "";
            qry = "select ClientID,AccountName from ClientMaster where isactive=1 and groupID=50 order by AccountName";
            SqlCommand cmd1 = new SqlCommand(qry, con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            cmbagentname.ValueMember = "ClientID";
            cmbagentname.DisplayMember = "AccountName";
            cmbagentname.DataSource = dt1;
            cmbagentname.SelectedIndex = -1;
        }
        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
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

        private void AgentCommissionReport_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
                DTPFrom.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                DTPFrom.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                DTPTo.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                DTPTo.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                DTPFrom.Value = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());

                LVagent.Columns.Add("Party Name", 400, HorizontalAlignment.Center);
                LVagent.Columns.Add("Op.Balance", 150, HorizontalAlignment.Center);
                LVagent.Columns.Add("Sales", 150, HorizontalAlignment.Center);
                LVagent.Columns.Add("Receipt", 150, HorizontalAlignment.Center);
                //   LVagent.Columns.Add("Sale Return", 130, HorizontalAlignment.Center);
                // LVagent.Columns.Add("Discount", 130, HorizontalAlignment.Center);
                // LVagent.Columns.Add("Cl.Balance", 130, HorizontalAlignment.Center);
                LVagent.Columns.Add("Commission", 150, HorizontalAlignment.Center);
                binaagent();
                DTPFrom.CustomFormat = Master.dateformate;
                DTPTo.CustomFormat = Master.dateformate;
                DTPFrom.Focus();
                this.ActiveControl = DTPFrom;
            }
            catch
            {
            }
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

        private void btngenrpt_Enter(object sender, EventArgs e)
        {
            btngenrpt.UseVisualStyleBackColor = false;
            btngenrpt.BackColor = Color.FromArgb(176, 111, 193);
            btngenrpt.ForeColor = Color.White;
        }

        private void btngenrpt_Leave(object sender, EventArgs e)
        {
            btngenrpt.UseVisualStyleBackColor = true;
            btngenrpt.BackColor = Color.FromArgb(51, 153, 255);
            btngenrpt.ForeColor = Color.White;
        }

        private void btngenrpt_MouseEnter(object sender, EventArgs e)
        {
            btngenrpt.UseVisualStyleBackColor = false;
            btngenrpt.BackColor = Color.FromArgb(176, 111, 193);
            btngenrpt.ForeColor = Color.White;
        }

        private void btngenrpt_MouseLeave(object sender, EventArgs e)
        {
            btngenrpt.UseVisualStyleBackColor = true;
            btngenrpt.BackColor = Color.FromArgb(51, 153, 255);
            btngenrpt.ForeColor = Color.White;
        }

        private void BtnViewReport_Enter(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = false;
            BtnViewReport.BackColor = Color.FromArgb(94, 191, 174);
            BtnViewReport.ForeColor = Color.White;
        }

        private void BtnViewReport_Leave(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = true;
            BtnViewReport.BackColor = Color.FromArgb(51, 153, 255);
            BtnViewReport.ForeColor = Color.White;
        }

        private void BtnViewReport_MouseEnter(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = false;
            BtnViewReport.BackColor = Color.FromArgb(94, 191, 174);
            BtnViewReport.ForeColor = Color.White;
        }

        private void BtnViewReport_MouseLeave(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = true;
            BtnViewReport.BackColor = Color.FromArgb(51, 153, 255);
            BtnViewReport.ForeColor = Color.White;
        }

        private void cmbagentname_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbagentname.SelectedIndex = 0;
                cmbagentname.DroppedDown = true;
            }
            catch
            {
            }
        }

        private void cmbagentname_Leave(object sender, EventArgs e)
        {
            try
            {
                cmbagentname.Text = s;
            }
            catch
            {
            }
        }
        public static string activecontroal;
        public static string s;
        private void cmbagentname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbagentname.Items.Count; i++)
                {
                    s = cmbagentname.GetItemText(cmbagentname.Items[i]);
                    if (s == cmbagentname.Text)
                    {
                        inList = true;
                        cmbagentname.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbagentname.Text = "";
                }
                txtcommission.Focus();
            }
            if (e.KeyCode == Keys.F3)
            {
                var privouscontroal = cmbagentname;
                activecontroal = privouscontroal.Name;
                Accountentry client = new Accountentry(this, master, tabControl, activecontroal);

                client.Passed(1);
                //   client.Show();
                master.AddNewTab(client);
            }
            if (e.KeyCode == Keys.F2)
            {
                var privouscontroal = cmbagentname;
                activecontroal = privouscontroal.Name;
                string iid = cmbagentname.SelectedValue.ToString();

                Accountentry client = new Accountentry(this, master, tabControl, activecontroal);
                client.Update(1, iid);
                client.Passed(1);
                //  client.Show();
                master.AddNewTab(client);
            }
        }

        private void cmbagentname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbagentname.Items.Count; i++)
                {
                    s = cmbagentname.GetItemText(cmbagentname.Items[i]);
                    if (s == cmbagentname.Text)
                    {
                        inList = true;
                        cmbagentname.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbagentname.Text = "";
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
                cmbagentname.Focus();
            }
        }

        private void txtcommission_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnViewReport.Focus();
            }
        }
        ListViewItem li;
        decimal opbal = 0;
        decimal sale = 0;
        decimal rec = 0;
        decimal comm = 0;
        public void binddata()
        {
            try
            {
                LVagent.Items.Clear();
                DataTable dt = new DataTable();
                dt = conn.getdataset("select * from BillMaster where isactive=1 and agentID='" + cmbagentname.SelectedValue + "'");
                DataTable dt1 = new DataTable();
                dt1 = conn.getdataset("select * from BillPOSMaster where isactive=1 and agentID='" + cmbagentname.SelectedValue + "'");
                opbal = 0;
                sale = 0;
                rec = 0;
                comm = 0;
                if (cmbagentname.SelectedValue != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataTable client = conn.getdataset("select * from ClientMaster where isactive=1 and ClientID='" + dt.Rows[i]["ClientID"].ToString() + "'");
                            li = LVagent.Items.Add(client.Rows[0]["AccountName"].ToString());
                            li.SubItems.Add(client.Rows[0]["Opbal"].ToString());
                            li.SubItems.Add(dt.Rows[i]["totalnet"].ToString());
                            li.SubItems.Add("0.00");
                            if (string.IsNullOrEmpty(txtcommission.Text))
                            {
                                txtcommission.Text = "0";
                            }
                            Double commissionval = Convert.ToDouble(txtcommission.Text);
                            Double totalnet = Convert.ToDouble(dt.Rows[i]["totalnet"].ToString());
                            Double commi = (totalnet * commissionval) / 100;
                            li.SubItems.Add(Convert.ToString(commi.ToString("N2")));
                        }
                    }
                    if (dt1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            li = LVagent.Items.Add(dt1.Rows[i]["customername"].ToString());
                            li.SubItems.Add("0.00");
                            li.SubItems.Add(dt1.Rows[i]["totalnet"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["totalnet"].ToString());
                            if (string.IsNullOrEmpty(txtcommission.Text))
                            {
                                txtcommission.Text = "0";
                            }
                            Double commissionval = Convert.ToDouble(txtcommission.Text);
                            Double totalnet = Convert.ToDouble(dt1.Rows[i]["totalnet"].ToString());
                            Double commi = (totalnet * commissionval) / 100;
                            li.SubItems.Add(Convert.ToString(commi.ToString("N2")));
                        }
                    }
                    foreach (ListViewItem lstItem in LVagent.Items)
                    {
                        opbal += decimal.Parse(lstItem.SubItems[1].Text);
                        sale += decimal.Parse(lstItem.SubItems[2].Text);
                        rec += decimal.Parse(lstItem.SubItems[3].Text);
                        comm += decimal.Parse(lstItem.SubItems[4].Text);

                    }
                    txtopbalance.Text = Convert.ToString(opbal.ToString("N2"));
                    txtsales.Text = Convert.ToString(sale.ToString("N2"));
                    txtrecript.Text = Convert.ToString(rec.ToString("N2"));
                    txtcommamt.Text = Convert.ToString(comm.ToString("N2"));
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

        private void btngenrpt_Click(object sender, EventArgs e)
        {
            try
            {
                prn.execute("delete from printing");
                string status;
                status = "Commission Report Of " + cmbagentname.Text + " From " + DTPFrom.Text + " To " + DTPTo.Text;
                for (int i = 0; i < LVagent.Items.Count; i++)
                {
                    DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1 and CompanyID='" + Master.companyId + "' ");
                    string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21)VALUES";
                    qry += "('" + dt1.Rows[0]["CompanyName"].ToString() + "','" + dt1.Rows[0]["SubName"].ToString() + "','" + dt1.Rows[0]["Address"].ToString() + "','" + dt1.Rows[0]["Address2"].ToString() + "','" + dt1.Rows[0]["City"].ToString() + "','" + dt1.Rows[0]["State"].ToString() + "','" + dt1.Rows[0]["Country"].ToString() + "','" + dt1.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Mobile"].ToString() + "','" + dt1.Rows[0]["Email"].ToString() + "','" + dt1.Rows[0]["CSTNo"].ToString() + "','" + status + "','" + LVagent.Items[i].SubItems[0].Text + "','" + LVagent.Items[i].SubItems[1].Text + "','" + LVagent.Items[i].SubItems[2].Text + "','" + LVagent.Items[i].SubItems[3].Text + "','" + LVagent.Items[i].SubItems[4].Text +"','"+txtopbalance.Text+"','"+txtsales.Text+"','"+txtrecript.Text+"','"+txtcommamt.Text+ "')";
                    prn.execute(qry);
                }
                Print popup = new Print("AgentReport");
                popup.ShowDialog();
                popup.Dispose();
            }
            catch
            {
            }
        }
    }
}
