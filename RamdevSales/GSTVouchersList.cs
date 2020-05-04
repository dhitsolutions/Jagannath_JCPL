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
    public partial class GSTVouchersList : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        OleDbSettings ods = new OleDbSettings();
        Connection conn = new Connection();
        Printing prn = new Printing();
        private Master master;
        private TabControl tabControl;
        public GSTVouchersList()
        {
            InitializeComponent();
        }

        public GSTVouchersList(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }

        private void GSTVouchersList_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
                DTPFrom.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                DTPFrom.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                DTPTo.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                DTPTo.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                LVDayBook.Columns.Add("Date", 100, HorizontalAlignment.Center);
                LVDayBook.Columns.Add("Enter", 100, HorizontalAlignment.Center);
                LVDayBook.Columns.Add("Terms", 100, HorizontalAlignment.Left);
                LVDayBook.Columns.Add("Party", 280, HorizontalAlignment.Left);
                LVDayBook.Columns.Add("Bill No", 120, HorizontalAlignment.Center);
                LVDayBook.Columns.Add("Taxable Amt", 100, HorizontalAlignment.Center);
                LVDayBook.Columns.Add("TAX Amt", 100, HorizontalAlignment.Center);
                LVDayBook.Columns.Add("Charges", 100, HorizontalAlignment.Center);
                LVDayBook.Columns.Add("Total Amt", 120, HorizontalAlignment.Center);
                LVDayBook.Columns.Add("ClientID", 0, HorizontalAlignment.Center);
                LVDayBook.Columns.Add("Billtype", 0, HorizontalAlignment.Center);
                bindgrid();
                DTPFrom.CustomFormat = Master.dateformate;
                DTPTo.CustomFormat = Master.dateformate;
                this.ActiveControl = btnnew;
            }
            catch
            {
            }
        }
        public void bindgrid()
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                LVDayBook.Items.Clear();
                DataTable dt = conn.getdataset("select gm.date,gm.entry,gm.terms,c.AccountName,gm.billno,gm.totalbasic,gm.totxltax,gm.totalcharges,gm.totalfinalamount,c.ClientID,gm.billtype from tblgstvouchermaster gm inner join ClientMaster c on gm.party=c.ClientID where gm.isactive=1 and c.isactive=1 and gm.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and gm.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by gm.date,gm.entry,gm.terms,c.AccountName,gm.billno,gm.totalbasic,gm.totxltax,gm.totalcharges,gm.totalfinalamount,c.ClientID,gm.billtype");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        LVDayBook.Items.Add(Convert.ToDateTime(dt.Rows[i].ItemArray[0].ToString()).ToString(Master.dateformate));
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[7].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[8].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[9].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[10].ToString());
                    }
                }
            }
            catch
            {
            }
            finally
            {
                con.Close();
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
        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
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

        private void btnnew_MouseLeave(object sender, EventArgs e)
        {
            btnnew.UseVisualStyleBackColor = true;
            btnnew.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnnew.ForeColor = System.Drawing.Color.White;
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

        private void btnExcel_Enter(object sender, EventArgs e)
        {
            btnExcel.UseVisualStyleBackColor = false;
            btnExcel.BackColor = System.Drawing.Color.FromArgb(176, 111, 193);
            btnExcel.ForeColor = System.Drawing.Color.White;
        }

        private void btnExcel_Leave(object sender, EventArgs e)
        {
            btnExcel.UseVisualStyleBackColor = true;
            btnExcel.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnExcel.ForeColor = System.Drawing.Color.White;
        }

        private void btnExcel_MouseEnter(object sender, EventArgs e)
        {
            btnExcel.UseVisualStyleBackColor = false;
            btnExcel.BackColor = System.Drawing.Color.FromArgb(176, 111, 193);
            btnExcel.ForeColor = System.Drawing.Color.White;
        }

        private void btnExcel_MouseLeave(object sender, EventArgs e)
        {
            btnExcel.UseVisualStyleBackColor = true;
            btnExcel.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnExcel.ForeColor = System.Drawing.Color.White;
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            GSTVouchers gs = new GSTVouchers(master,tabControl);
            master.AddNewTab(gs);

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
                BtnViewReport.Focus();
            }
        }

        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            bindgrid();
        }
        private string[] strfinalarray;
        public void setform()
        {
            try
            {
                this.Enabled = false;
                String str = LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[4].Text;
                int clientid = Convert.ToInt32(LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[9].Text);
                string billtype = LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[10].Text;
                if (billtype == "S")
                {
                    strfinalarray = new string[5] { "S", "D", "GSTVOUCHERS", "S", "" };
                }
                else if (billtype == "SR")
                {
                    strfinalarray = new string[5] { "SR", "C", "GSTVOUCHERSR", "SR", "" };
                }
                else if (billtype == "P")
                {
                    strfinalarray = new string[5] { "P", "C", "GSTVOUCHERP", "P", "" };
                }
                else if (billtype == "PR")
                {
                    strfinalarray = new string[5] { "PR", "D", "GSTVOUCHERPR", "PR", "" };
                }
                else if (billtype == "DN")
                {
                    strfinalarray = new string[5] { "DN", "D", "GSTVOUCHERDN", "DN", "" };
                }
                else if (billtype == "CN")
                {
                    strfinalarray = new string[5] { "CN", "C", "GSTVOUCHERCN", "CN", "" };
                }
                else if (billtype == "EXP")
                {
                    strfinalarray = new string[5] { "EXP", "D", "GSTVOUCHEREXP", "EXP", "" };
                }
             //   bd = new DefaultSale(this, master, tabControl, strfinalarray);
                GSTVouchers gs = new GSTVouchers(this, master, tabControl, strfinalarray);
                gs.updatemode(str, LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[4].Text, clientid, strfinalarray);
                master.AddNewTab(gs);
            }
            catch
            {
            }
            finally
            {
                this.Enabled = true;
            }
        }
        private void LVDayBook_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            setform();
        }

        private void LVDayBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                setform();
            }
        }
    }
}
