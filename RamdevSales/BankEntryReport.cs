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
    public partial class BankEntryReport : Form
    {
        private Master master;
        private TabControl tabControl;
        OleDbSettings ods = new OleDbSettings();
        Connection conn = new Connection();
        Printing prn = new Printing();
        public BankEntryReport()
        {
            InitializeComponent();
        }

        public BankEntryReport(Master master, TabControl tabControl)
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
        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }
        Double basic = 0;
        Double exp = 0;
        Double totalamount = 0;
        public void binddata()
        {
            try
            {
                LVDayBook.Items.Clear();
                DataTable dt = new DataTable();
                dt = conn.getdataset("select Date,AccountName,PaymentTerms,PartyName,ChequeNo,ChequeDate,OT1,BasicAmount,OT2,TotalAmount,VchNo from Voucher where isactive=1 and TransactionType='Bank Entry' and  Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by date");
                if (dt.Rows.Count > 0)
                {
                    basic = 0;
                    exp = 0;
                    totalamount = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ListViewItem li;
                        li = LVDayBook.Items.Add("");
                        li.SubItems.Add(Convert.ToDateTime(dt.Rows[i]["Date"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add(dt.Rows[i]["AccountName"].ToString());
                        li.SubItems.Add(dt.Rows[i]["PaymentTerms"].ToString());
                        li.SubItems.Add(dt.Rows[i]["PartyName"].ToString());
                        li.SubItems.Add(dt.Rows[i]["ChequeNo"].ToString());
                        li.SubItems.Add(dt.Rows[i]["ChequeDate"].ToString());
                        li.SubItems.Add(dt.Rows[i]["OT1"].ToString());
                        li.SubItems.Add(dt.Rows[i]["BasicAmount"].ToString());
                        li.SubItems.Add(dt.Rows[i]["OT2"].ToString());
                        li.SubItems.Add(dt.Rows[i]["TotalAmount"].ToString());
                        li.SubItems.Add(dt.Rows[i]["VchNo"].ToString());

                        basic = basic + Convert.ToDouble(dt.Rows[i]["BasicAmount"].ToString());
                        exp = exp + Convert.ToDouble(dt.Rows[i]["OT2"].ToString());
                        totalamount = totalamount + Convert.ToDouble(dt.Rows[i]["TotalAmount"].ToString());

                    }
                    txtamount.Text = basic.ToString("N2");
                    txtexp.Text = exp.ToString("N2");
                    txtnetamt.Text = totalamount.ToString("N2");
                }
            }
            catch
            {
            }
        }
        DataTable userrights = new DataTable();
        private void BankEntryReport_Load(object sender, EventArgs e)
        {
            try
            {
                userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[20]["p"].ToString() == "False")
                    {
                        btnprint.Enabled = false;
                    }
                }
                LVDayBook.CheckBoxes = true;
                LVDayBook.Columns.Add("", 20, HorizontalAlignment.Left);
                LVDayBook.Columns.Add("Date", 100, HorizontalAlignment.Center);
                LVDayBook.Columns.Add("Bank", 100, HorizontalAlignment.Center);
                LVDayBook.Columns.Add("Entry", 100, HorizontalAlignment.Center);
                LVDayBook.Columns.Add("Account Name", 200, HorizontalAlignment.Center);
                LVDayBook.Columns.Add("Chq No", 100, HorizontalAlignment.Center);
                LVDayBook.Columns.Add("Chq Dt.", 100, HorizontalAlignment.Center);
                LVDayBook.Columns.Add("Remarks", 100, HorizontalAlignment.Center);
                LVDayBook.Columns.Add("Amount", 100, HorizontalAlignment.Center);
                LVDayBook.Columns.Add("Exp.", 100, HorizontalAlignment.Center);
                LVDayBook.Columns.Add("Net Amt.", 100, HorizontalAlignment.Center);
                LVDayBook.Columns.Add("vid.",0, HorizontalAlignment.Center);
                DTPFrom.CustomFormat = Master.dateformate;
                DTPTo.CustomFormat = Master.dateformate;
                this.ActiveControl = BtnViewReport;
            }
            catch
            {
            }
        }

        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            binddata();
        }
        Double amount = 0;
        Double exp1 = 0;
        Double net = 0;
        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                prn.execute("delete from printing");
                string status;
                status = "BANK ENTERY REGISTER FROM  " + DTPFrom.Text + " TO  " + DTPTo.Text;
                string[] a = new string[LVDayBook.CheckedItems.Count];
                string[] b = new string[LVDayBook.CheckedItems.Count];
                string[] c = new string[LVDayBook.CheckedItems.Count];
                string[] d = new string[LVDayBook.CheckedItems.Count];
                for (int i = 0; i < LVDayBook.Items.Count; i++)
                {
                    if (Convert.ToBoolean(LVDayBook.Items[i].Checked) == true)
                    {
                        amount = +amount + Convert.ToDouble(LVDayBook.Items[i].SubItems[8].Text);
                        exp1 = +exp1 + Convert.ToDouble(LVDayBook.Items[i].SubItems[9].Text);
                        net = +net + Convert.ToDouble(LVDayBook.Items[i].SubItems[10].Text);
                        DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1 and CompanyID='" + Master.companyId + "' ");
                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22)VALUES";
                        qry += "('" + dt1.Rows[0]["CompanyName"].ToString() + "','" + dt1.Rows[0]["SubName"].ToString() + "','" + dt1.Rows[0]["Address"].ToString() + "','" + dt1.Rows[0]["Address2"].ToString() + "','" + dt1.Rows[0]["City"].ToString() + "','" + dt1.Rows[0]["State"].ToString() + "','" + dt1.Rows[0]["Country"].ToString() + "','" + dt1.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Mobile"].ToString() + "','" + dt1.Rows[0]["Email"].ToString() + "','" + dt1.Rows[0]["CSTNo"].ToString() + "','" + status + "','" + LVDayBook.Items[i].SubItems[1].Text + "','" + LVDayBook.Items[i].SubItems[2].Text + "','" + LVDayBook.Items[i].SubItems[3].Text + "','" + LVDayBook.Items[i].SubItems[4].Text + "','" + LVDayBook.Items[i].SubItems[5].Text + "','" + LVDayBook.Items[i].SubItems[6].Text + "','" + LVDayBook.Items[i].SubItems[7].Text + "','" + LVDayBook.Items[i].SubItems[8].Text + "','" + LVDayBook.Items[i].SubItems[9].Text + "','" + LVDayBook.Items[i].SubItems[10].Text +"')";
                        prn.execute(qry);
                    }
                }
                string update = "UPDATE [Printing] SET [T23]='" + amount.ToString("N2") + "',[T24]='" + exp1.ToString("N2") + "',[T25]='" + net.ToString("N2") + "'";
                prn.execute(update);
                Print popup = new Print("BankEnteryReport");
                popup.ShowDialog();
                popup.Dispose();
            }
            catch
            {
            }
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
        public void open()
        {
            if (LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[3].Text == "Cheque Issued")
            {
                String str = LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[11].Text;
                BankEntry be = new BankEntry(master, tabControl);
                be.updatemode(str, LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[3].Text);
                master.AddNewTab(be);
            }
            else if (LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[3].Text == "Draft Issued")
            {
                String str = LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[11].Text;
                BankEntry be = new BankEntry(master, tabControl);
                be.updatemode(str, LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[3].Text);
                master.AddNewTab(be);
            }
            else if (LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[3].Text == "Cheque/Draft/Rtgs Received")
            {
                String str = LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[11].Text;
                BankEntry be = new BankEntry(master, tabControl);
                be.updatemode(str, LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[3].Text);
                master.AddNewTab(be);
            }
            else if (LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[3].Text == "Deposit Cash Into Bank")
            {
                String str = LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[11].Text;
                BankEntry be = new BankEntry(master, tabControl);
                be.updatemode(str, LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[3].Text);
                master.AddNewTab(be);
            }
            else if (LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[3].Text == "Withdraw Cash from Bank")
            {
                String str = LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[11].Text;
                BankEntry be = new BankEntry(master, tabControl);
                be.updatemode(str, LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[3].Text);
                master.AddNewTab(be);
            }
            else if (LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[3].Text == "Bank Expenses")
            {
                String str = LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[11].Text;
                BankEntry be = new BankEntry(master, tabControl);
                be.updatemode(str, LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[3].Text);
                master.AddNewTab(be);
            }
            else if (LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[3].Text == "Online Transfer")
            {
                String str = LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[11].Text;
                BankEntry be = new BankEntry(master, tabControl);
                be.updatemode(str, LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[3].Text);
                master.AddNewTab(be);
            }
            //String str = LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[11].Text;
            //DebitandCreditNote be = new DebitandCreditNote(master, tabControl);
            //be.updatemode(str, LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[3].Text,debitorcredit);
            //master.AddNewTab(be);
        }
        private void LVDayBook_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[20]["v"].ToString() == "True" || userrights.Rows[20]["u"].ToString() == "True")
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
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DTPTo.Focus();
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void DTPTo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BtnViewReport.Focus();
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void LVDayBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[20]["v"].ToString() == "True" || userrights.Rows[20]["u"].ToString() == "True")
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
