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
    public partial class CreditNoteReport : Form
    {
        private Master master;
        private TabControl tabControl;
        OleDbSettings ods = new OleDbSettings();
        Connection conn = new Connection();
        Printing prn = new Printing();
        public CreditNoteReport()
        {
            InitializeComponent();
        }

        public CreditNoteReport(Master master, TabControl tabControl)
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
                // tabControl.TabPages.Remove(tabControl.SelectedTab);
                DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    master.RemoveCurrentTab();
                }
                return true;
            }


            return base.ProcessCmdKey(ref msg, keyData);
        }
        DataTable userrights = new DataTable();
        private void DebitNoteReport_Load(object sender, EventArgs e)
        {
            try
            {
                userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[7]["p"].ToString() == "False")
                    {
                        btnprint.Enabled = false;
                    }
                }
                LVDayBook.Columns.Add("Date", 100, HorizontalAlignment.Center);
                LVDayBook.Columns.Add("Type", 150, HorizontalAlignment.Center);
                LVDayBook.Columns.Add("Account", 200, HorizontalAlignment.Center);
                LVDayBook.Columns.Add("Debit", 150, HorizontalAlignment.Center);
                LVDayBook.Columns.Add("Credit", 150, HorizontalAlignment.Center);
                LVDayBook.Columns.Add("Short Narration", 200, HorizontalAlignment.Center);
                LVDayBook.Columns.Add("VoucherID", 0, HorizontalAlignment.Center);
                DTPFrom.CustomFormat = Master.dateformate;
                DTPTo.CustomFormat = Master.dateformate;
                this.ActiveControl = DTPFrom;
            }
            catch
            {
            }
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
        decimal debit = 0;
        decimal credit = 0;
        public void binddate()
        {
            try
            {
                LVDayBook.Items.Clear();
                DataTable dt = new DataTable();
                dt = conn.getdataset("select Date1,TranType,AccountName,Amount,ShortNarration,dc,VoucherID from Ledger where isactive=1 and TranType='CREDIT NOTE' and  Date1>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Date1<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by date1");
                if (dt.Rows.Count > 0)
                {
                    debit = 0;
                    credit = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ListViewItem li;
                        li = LVDayBook.Items.Add(Convert.ToDateTime(dt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add(dt.Rows[i]["TranType"].ToString());
                        li.SubItems.Add(dt.Rows[i]["AccountName"].ToString());
                        if (dt.Rows[i]["dc"].ToString() == "D")
                        {
                            li.SubItems.Add(dt.Rows[i]["Amount"].ToString());
                            li.SubItems.Add("0");
                        }
                        else
                        {
                            li.SubItems.Add("0");
                            li.SubItems.Add(dt.Rows[i]["Amount"].ToString());
                        }
                        li.SubItems.Add(dt.Rows[i]["ShortNarration"].ToString());
                        li.SubItems.Add(dt.Rows[i]["VoucherID"].ToString());
                    }
                    foreach (ListViewItem lstItem in LVDayBook.Items)
                    {
                        debit += decimal.Parse(lstItem.SubItems[3].Text);
                        credit += decimal.Parse(lstItem.SubItems[4].Text);
                    }
                    txttotaldr.Text = Convert.ToString(debit);
                    txttotalcr.Text = Convert.ToString(credit);
                }
            }
            catch
            {
            }
        }
        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            binddate();
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                prn.execute("delete from printing");
                string status;
                status = "DAY BOOK FROM " + DTPFrom.Text;
                for (int i = 0; i < LVDayBook.Items.Count; i++)
                {
                    DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1 and CompanyID='" + Master.companyId + "' ");
                    string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20)VALUES";
                    qry += "('" + dt1.Rows[0]["CompanyName"].ToString() + "','" + dt1.Rows[0]["SubName"].ToString() + "','" + dt1.Rows[0]["Address"].ToString() + "','" + dt1.Rows[0]["Address2"].ToString() + "','" + dt1.Rows[0]["City"].ToString() + "','" + dt1.Rows[0]["State"].ToString() + "','" + dt1.Rows[0]["Country"].ToString() + "','" + dt1.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Mobile"].ToString() + "','" + dt1.Rows[0]["Email"].ToString() + "','" + dt1.Rows[0]["CSTNo"].ToString() + "','" + status + "','" + txttotaldr.Text + "','" + txttotalcr.Text + "','" + LVDayBook.Items[i].SubItems[0].Text + "','" + LVDayBook.Items[i].SubItems[1].Text + "','" + LVDayBook.Items[i].SubItems[2].Text + "','" + LVDayBook.Items[i].SubItems[3].Text + "','" + LVDayBook.Items[i].SubItems[4].Text + "','" + LVDayBook.Items[i].SubItems[5].Text + "')";
                    prn.execute(qry);
                }
                Print popup = new Print("CreditReport");
                popup.ShowDialog();
                popup.Dispose();
            }
            catch
            {
            }
        }
        public void open()
        {
            try
            {
                string[] debitorcredit = new string[5] { "C", "", "", "", "" };
                String str = LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[6].Text;
                DebitandCreditNote be = new DebitandCreditNote(master, tabControl);
                be.updatemode(str, LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[1].Text, debitorcredit);
                master.AddNewTab(be);
            }
            catch
            {
            }
        }
        private void LVDayBook_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[7]["v"].ToString() == "True" || userrights.Rows[7]["u"].ToString() == "True")
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

        private void BtnViewReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnprint.Focus();
            }
        }

        private void LVDayBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[7]["v"].ToString() == "True" || userrights.Rows[7]["u"].ToString() == "True")
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
    }
}
