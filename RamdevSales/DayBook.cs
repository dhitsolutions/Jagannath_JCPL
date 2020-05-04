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
    public partial class DayBook : Form
    {
        private Master master;
        private TabControl tabControl;
        OleDbSettings ods = new OleDbSettings();
        Connection conn = new Connection();
        Printing prn = new Printing();
        public DayBook()
        {
            InitializeComponent();
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
        public DayBook(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
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
        ListViewItem li;
        decimal debit = 0;
        decimal credit = 0;
        public void binddata()
        {
            try
            {
                LVDayBook.Items.Clear();
                //Sale
                #region
                DataTable dt = new DataTable();
                dt = conn.getdataset("select * from BillMaster where isactive=1 and BillType='S' and Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Bill_Date asc");
                if (dt.Rows.Count > 0)
                {
                    debit = 0;
                    credit = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataTable client = conn.getdataset("select * from ClientMaster where isactive=1 and ClientID='" + dt.Rows[i]["ClientID"].ToString() + "'");
                        DataTable saletype = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypeid='" + dt.Rows[i]["SaleType"].ToString() + "'");
                        DataTable accgrpname = conn.getdataset("select * from AccountGroup where id='" + saletype.Rows[0]["Groupid"].ToString() + "'");
                        li = LVDayBook.Items.Add(Convert.ToDateTime(dt.Rows[i]["Bill_Date"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Sale");
                        li.SubItems.Add(accgrpname.Rows[0]["groupname"].ToString());
                        li.SubItems.Add("");
                        li.SubItems.Add("0");
                        Double totalbasic = Convert.ToDouble(dt.Rows[i]["totalbasic"].ToString()) - Convert.ToDouble(dt.Rows[i]["totaldiscount"].ToString()) - Convert.ToDouble(dt.Rows[i]["totaladddiscount"].ToString());
                        li.SubItems.Add(Convert.ToString(totalbasic));
                        DataTable charges = conn.getdataset("select perticulars,sum(valueofexp) as valueofexp from Billchargesmaster where isactive=1 and billno='" + dt.Rows[i]["billno"].ToString() + "' and plusminus='+' group by perticulars");
                        if (charges.Rows.Count > 0)
                        {
                            if (charges.Rows.Count > 0)
                            {
                                for (int j = 0; j < charges.Rows.Count; j++)
                                {
                                    li = LVDayBook.Items.Add("");
                                    li.SubItems.Add("");
                                    li.SubItems.Add(charges.Rows[j]["perticulars"].ToString());
                                    li.SubItems.Add("");
                                    li.SubItems.Add("0");
                                    li.SubItems.Add(charges.Rows[j]["valueofexp"].ToString());
                                }
                            }
                        }
                        if (Convert.ToDouble(dt.Rows[i]["roudoff"].ToString()) != 0)
                        {
                            li = LVDayBook.Items.Add("");
                            li.SubItems.Add("");
                            li.SubItems.Add("Round Off");
                            li.SubItems.Add("");
                            li.SubItems.Add("0");
                            if (dt.Rows[i]["roudoff"].ToString() == "")
                            {
                                dt.Rows[i]["roudoff"] = "0";
                                dt.AcceptChanges();
                            }
                            li.SubItems.Add(dt.Rows[i]["roudoff"].ToString());
                        }
                        li = LVDayBook.Items.Add("");
                        li.SubItems.Add("");
                        li.SubItems.Add("SGST");
                        li.SubItems.Add("");
                        li.SubItems.Add("0");
                        li.SubItems.Add(dt.Rows[i]["sgstamt"].ToString());
                        li = LVDayBook.Items.Add("");
                        li.SubItems.Add("");
                        li.SubItems.Add("CGST");
                        li.SubItems.Add("");
                        li.SubItems.Add("0");
                        li.SubItems.Add(dt.Rows[i]["cgatamt"].ToString());
                        li = LVDayBook.Items.Add("");
                        li.SubItems.Add("");
                        li.SubItems.Add(client.Rows[0]["AccountName"].ToString());
                        li.SubItems.Add("");
                        li.SubItems.Add(dt.Rows[i]["totalnet"].ToString());
                        li.SubItems.Add("0");
                        DataTable charges1 = conn.getdataset("select perticulars,sum(amount) as amount from Billchargesmaster where isactive=1 and billno='" + dt.Rows[i]["billno"].ToString() + "' and plusminus='-' group by perticulars");
                        if (charges1.Rows.Count > 0)
                        {
                            if (charges1.Rows.Count > 0)
                            {
                                for (int j = 0; j < charges1.Rows.Count; j++)
                                {
                                    li = LVDayBook.Items.Add("");
                                    li.SubItems.Add("");
                                    li.SubItems.Add(charges1.Rows[j]["perticulars"].ToString());
                                    li.SubItems.Add("");
                                    li.SubItems.Add(charges1.Rows[j]["amount"].ToString());
                                    li.SubItems.Add("0");
                                }
                            }
                        }


                    }


                }
                #endregion
                //Purchase
                #region
                DataTable dt1 = new DataTable();
                dt1 = conn.getdataset("select * from BillMaster where isactive=1 and BillType='P' and Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Bill_Date asc");
                if (dt1.Rows.Count > 0)
                {
                    debit = 0;
                    credit = 0;
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        DataTable client = conn.getdataset("select * from ClientMaster where isactive=1 and ClientID='" + dt1.Rows[i]["ClientID"].ToString() + "'");
                        DataTable saletype = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypeid='" + dt1.Rows[i]["SaleType"].ToString() + "'");
                        DataTable accgrpname = conn.getdataset("select * from AccountGroup where id='" + saletype.Rows[0]["Groupid"].ToString() + "'");
                        li = LVDayBook.Items.Add(Convert.ToDateTime(dt1.Rows[i]["Bill_Date"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Purchase");
                        li.SubItems.Add(accgrpname.Rows[0]["groupname"].ToString());
                        li.SubItems.Add("");
                        Double totalbasic = Convert.ToDouble(dt1.Rows[i]["totalbasic"].ToString()) - Convert.ToDouble(dt1.Rows[i]["totaldiscount"].ToString()) - Convert.ToDouble(dt1.Rows[i]["totaladddiscount"].ToString());
                        li.SubItems.Add(Convert.ToString(totalbasic));
                        li.SubItems.Add("0");
                        DataTable charges = conn.getdataset("select perticulars,sum(valueofexp) as valueofexp from Billchargesmaster where isactive=1 and billno='" + dt1.Rows[i]["billno"].ToString() + "' and plusminus='+' group by perticulars");
                        if (charges.Rows.Count > 0)
                        {
                            if (charges.Rows.Count > 0)
                            {
                                for (int j = 0; j < charges.Rows.Count; j++)
                                {
                                    li = LVDayBook.Items.Add("");
                                    li.SubItems.Add("");
                                    li.SubItems.Add(charges.Rows[j]["perticulars"].ToString());
                                    li.SubItems.Add("");
                                    li.SubItems.Add(charges.Rows[j]["valueofexp"].ToString());
                                    li.SubItems.Add("0");
                                }
                            }
                        }
                        if (Convert.ToDouble(dt1.Rows[i]["roudoff"].ToString()) != 0)
                        {
                            li = LVDayBook.Items.Add("");
                            li.SubItems.Add("");
                            li.SubItems.Add("Round Off");
                            li.SubItems.Add("");
                            li.SubItems.Add(dt1.Rows[i]["roudoff"].ToString());
                            li.SubItems.Add("0");
                        }
                        li = LVDayBook.Items.Add("");
                        li.SubItems.Add("");
                        li.SubItems.Add("SGST");
                        li.SubItems.Add("");
                        li.SubItems.Add(dt1.Rows[i]["sgstamt"].ToString());
                        li.SubItems.Add("0");
                        li = LVDayBook.Items.Add("");
                        li.SubItems.Add("");
                        li.SubItems.Add("CGST");
                        li.SubItems.Add("");
                        li.SubItems.Add(dt1.Rows[i]["cgatamt"].ToString());
                        li.SubItems.Add("0");
                        li = LVDayBook.Items.Add("");
                        li.SubItems.Add("");
                        li.SubItems.Add(client.Rows[0]["AccountName"].ToString());
                        li.SubItems.Add("");
                        li.SubItems.Add("0");
                        li.SubItems.Add(dt1.Rows[i]["totalnet"].ToString());
                        DataTable charges1 = conn.getdataset("select perticulars,sum(amount) as amount from Billchargesmaster where isactive=1 and billno='" + dt1.Rows[i]["billno"].ToString() + "' and plusminus='-' group by perticulars");
                        if (charges1.Rows.Count > 0)
                        {
                            if (charges1.Rows.Count > 0)
                            {
                                for (int j = 0; j < charges1.Rows.Count; j++)
                                {
                                    li = LVDayBook.Items.Add("");
                                    li.SubItems.Add("");
                                    li.SubItems.Add(charges1.Rows[j]["perticulars"].ToString());
                                    li.SubItems.Add("");
                                    li.SubItems.Add("0");
                                    li.SubItems.Add(charges1.Rows[j]["amount"].ToString());

                                }
                            }
                        }
                    }


                }
                #endregion
                //BankEntry
                #region
                DataTable dt2 = new DataTable();
                dt2 = conn.getdataset("select * from Voucher where isactive=1 and TransactionType='Bank Entry' and Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Date asc");
                if (dt2.Rows.Count > 0)
                {
                    debit = 0;
                    credit = 0;
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        if (dt2.Rows[i]["PaymentTerms"].ToString() == "Cheque Issued")
                        {
                            li = LVDayBook.Items.Add(Convert.ToDateTime(dt2.Rows[i]["Date"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add(dt2.Rows[i]["PaymentTerms"].ToString());
                            li.SubItems.Add(dt2.Rows[i]["AccountName"].ToString());
                            li.SubItems.Add(dt2.Rows[i]["OT1"].ToString());
                            // if (dt2.Rows[i]["DC"].ToString() == "D")
                            //{
                            li.SubItems.Add("0");
                            li.SubItems.Add(dt2.Rows[i]["TotalAmount"].ToString());

                            //  }
                            li = LVDayBook.Items.Add("");
                            li.SubItems.Add("");
                            li.SubItems.Add(dt2.Rows[i]["PartyName"].ToString());
                            li.SubItems.Add("");
                            // if (dt2.Rows[i]["DC"].ToString() == "C")
                            //{

                            li.SubItems.Add(dt2.Rows[i]["TotalAmount"].ToString());
                            li.SubItems.Add("0");
                            //}
                        }
                        else if (dt2.Rows[i]["PaymentTerms"].ToString() == "Draft Issued")
                        {
                            li = LVDayBook.Items.Add(Convert.ToDateTime(dt2.Rows[i]["Date"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add(dt2.Rows[i]["PaymentTerms"].ToString());
                            li.SubItems.Add(dt2.Rows[i]["AccountName"].ToString());
                            li.SubItems.Add(dt2.Rows[i]["OT1"].ToString());
                            // if (dt2.Rows[i]["DC"].ToString() == "D")
                            //{
                            li.SubItems.Add("0");
                            li.SubItems.Add(dt2.Rows[i]["TotalAmount"].ToString());

                            //  }

                            li = LVDayBook.Items.Add("");
                            li.SubItems.Add("");
                            li.SubItems.Add(dt2.Rows[i]["PartyName"].ToString());
                            li.SubItems.Add("");
                            // if (dt2.Rows[i]["DC"].ToString() == "C")
                            //{
                            li.SubItems.Add(dt2.Rows[i]["BasicAmount"].ToString());
                            li.SubItems.Add("0");
                            if (Convert.ToDouble(dt2.Rows[i]["OT2"].ToString()) != 0)
                            {
                                li = LVDayBook.Items.Add("");
                                li.SubItems.Add("");
                                li.SubItems.Add("Bank Charges");
                                li.SubItems.Add("");
                                // if (dt2.Rows[i]["DC"].ToString() == "C")
                                //{
                                li.SubItems.Add(dt2.Rows[i]["OT2"].ToString());
                                li.SubItems.Add("0");
                            }
                        }
                        else if (dt2.Rows[i]["PaymentTerms"].ToString() == "Cheque/Draft/Rtgs Received")
                        {
                            li = LVDayBook.Items.Add(Convert.ToDateTime(dt2.Rows[i]["Date"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add(dt2.Rows[i]["PaymentTerms"].ToString());
                            li.SubItems.Add(dt2.Rows[i]["PartyName"].ToString());
                            li.SubItems.Add(dt2.Rows[i]["OT1"].ToString());
                            // if (dt2.Rows[i]["DC"].ToString() == "D")
                            //{
                            li.SubItems.Add("0");
                            li.SubItems.Add(dt2.Rows[i]["TotalAmount"].ToString());
                            //  }
                            li = LVDayBook.Items.Add("");
                            li.SubItems.Add("");
                            li.SubItems.Add(dt2.Rows[i]["AccountName"].ToString());
                            li.SubItems.Add("");
                            // if (dt2.Rows[i]["DC"].ToString() == "C")
                            //{
                            li.SubItems.Add(dt2.Rows[i]["TotalAmount"].ToString());
                            li.SubItems.Add("0");
                        }
                        else if (dt2.Rows[i]["PaymentTerms"].ToString() == "Deposit Cash Into Bank")
                        {
                            li = LVDayBook.Items.Add(Convert.ToDateTime(dt2.Rows[i]["Date"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add(dt2.Rows[i]["PaymentTerms"].ToString());
                            li.SubItems.Add(dt2.Rows[i]["PartyName"].ToString());
                            li.SubItems.Add(dt2.Rows[i]["OT1"].ToString());
                            // if (dt2.Rows[i]["DC"].ToString() == "D")
                            //{
                            li.SubItems.Add("0");
                            li.SubItems.Add(dt2.Rows[i]["TotalAmount"].ToString());
                            //  }
                            li = LVDayBook.Items.Add("");
                            li.SubItems.Add("");
                            li.SubItems.Add(dt2.Rows[i]["AccountName"].ToString());
                            li.SubItems.Add("");
                            // if (dt2.Rows[i]["DC"].ToString() == "C")
                            //{
                            li.SubItems.Add(dt2.Rows[i]["TotalAmount"].ToString());
                            li.SubItems.Add("0");
                        }
                        else if (dt2.Rows[i]["PaymentTerms"].ToString() == "Withdraw Cash from Bank")
                        {
                            li = LVDayBook.Items.Add(Convert.ToDateTime(dt2.Rows[i]["Date"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add(dt2.Rows[i]["PaymentTerms"].ToString());
                            li.SubItems.Add(dt2.Rows[i]["AccountName"].ToString());
                            li.SubItems.Add(dt2.Rows[i]["OT1"].ToString());
                            // if (dt2.Rows[i]["DC"].ToString() == "D")
                            //{
                            li.SubItems.Add("0");
                            li.SubItems.Add(dt2.Rows[i]["TotalAmount"].ToString());

                            //  }
                            li = LVDayBook.Items.Add("");
                            li.SubItems.Add("");
                            li.SubItems.Add(dt2.Rows[i]["PartyName"].ToString());
                            li.SubItems.Add("");
                            // if (dt2.Rows[i]["DC"].ToString() == "C")
                            //{

                            li.SubItems.Add(dt2.Rows[i]["TotalAmount"].ToString());
                            li.SubItems.Add("0");
                            //}
                        }
                        else if (dt2.Rows[i]["PaymentTerms"].ToString() == "Bank Expenses")
                        {
                            li = LVDayBook.Items.Add(Convert.ToDateTime(dt2.Rows[i]["Date"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add(dt2.Rows[i]["PaymentTerms"].ToString());
                            li.SubItems.Add(dt2.Rows[i]["AccountName"].ToString());
                            li.SubItems.Add(dt2.Rows[i]["OT1"].ToString());
                            // if (dt2.Rows[i]["DC"].ToString() == "D")
                            //{
                            li.SubItems.Add("0");
                            li.SubItems.Add(dt2.Rows[i]["TotalAmount"].ToString());

                            //  }
                            li = LVDayBook.Items.Add("");
                            li.SubItems.Add("");
                            li.SubItems.Add(dt2.Rows[i]["PartyName"].ToString());
                            li.SubItems.Add("");
                            // if (dt2.Rows[i]["DC"].ToString() == "C")
                            //{

                            li.SubItems.Add(dt2.Rows[i]["TotalAmount"].ToString());
                            li.SubItems.Add("0");
                            //}
                        }
                        else if (dt2.Rows[i]["PaymentTerms"].ToString() == "Online Transfer")
                        {
                            li = LVDayBook.Items.Add(Convert.ToDateTime(dt2.Rows[i]["Date"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add(dt2.Rows[i]["PaymentTerms"].ToString());
                            li.SubItems.Add(dt2.Rows[i]["AccountName"].ToString());
                            li.SubItems.Add(dt2.Rows[i]["OT1"].ToString());
                            // if (dt2.Rows[i]["DC"].ToString() == "D")
                            //{
                            li.SubItems.Add("0");
                            li.SubItems.Add(dt2.Rows[i]["TotalAmount"].ToString());

                            //  }

                            li = LVDayBook.Items.Add("");
                            li.SubItems.Add("");
                            li.SubItems.Add(dt2.Rows[i]["PartyName"].ToString());
                            li.SubItems.Add("");
                            // if (dt2.Rows[i]["DC"].ToString() == "C")
                            //{
                            li.SubItems.Add(dt2.Rows[i]["BasicAmount"].ToString());
                            li.SubItems.Add("0");
                            if (Convert.ToDouble(dt2.Rows[i]["OT2"].ToString()) != 0)
                            {
                                li = LVDayBook.Items.Add("");
                                li.SubItems.Add("");
                                li.SubItems.Add("Bank Charges");
                                li.SubItems.Add("");
                                // if (dt2.Rows[i]["DC"].ToString() == "C")
                                //{
                                li.SubItems.Add(dt2.Rows[i]["OT2"].ToString());
                                li.SubItems.Add("0");
                            }
                        }
                    }
                }
                #endregion
                //DebitNote
                #region
                DataTable dt3 = new DataTable();
                dt3 = conn.getdataset("select * from Ledger where isactive=1 and TranType='DEBIT NOTE' and Date1>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Date1<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Date1 asc");
                if (dt3.Rows.Count > 0)
                {
                    debit = 0;
                    credit = 0;

                    for (int i = 0; i < dt3.Rows.Count; i++)
                    {
                        if (dt3.Rows[i]["DC"].ToString() == "D")
                        {
                            li = LVDayBook.Items.Add(Convert.ToDateTime(dt3.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add(dt3.Rows[i]["TranType"].ToString());
                            li.SubItems.Add(dt3.Rows[i]["AccountName"].ToString());
                            li.SubItems.Add(dt3.Rows[i]["ShortNarration"].ToString());
                            li.SubItems.Add(dt3.Rows[i]["Amount"].ToString());
                            li.SubItems.Add("0");


                        }
                        else
                        {
                            li = LVDayBook.Items.Add("");
                            li.SubItems.Add("");
                            li.SubItems.Add(dt3.Rows[i]["AccountName"].ToString());
                            li.SubItems.Add("");
                            li.SubItems.Add("0");
                            li.SubItems.Add(dt3.Rows[i]["Amount"].ToString());
                        }
                    }
                }
                #endregion
                //Credit Note
                #region
                DataTable dt4 = new DataTable();
                dt4 = conn.getdataset("select * from Ledger where isactive=1 and TranType='CREDIT NOTE' and Date1>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Date1<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Date1 asc");
                if (dt4.Rows.Count > 0)
                {
                    debit = 0;
                    credit = 0;

                    for (int i = 0; i < dt4.Rows.Count; i++)
                    {
                        if (dt4.Rows[i]["DC"].ToString() == "D")
                        {
                            li = LVDayBook.Items.Add("");
                            li.SubItems.Add("");
                            li.SubItems.Add(dt4.Rows[i]["AccountName"].ToString());
                            li.SubItems.Add("");
                            li.SubItems.Add("0");
                            li.SubItems.Add(dt4.Rows[i]["Amount"].ToString());



                        }
                        else
                        {
                            li = LVDayBook.Items.Add(Convert.ToDateTime(dt4.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add(dt4.Rows[i]["TranType"].ToString());
                            li.SubItems.Add(dt4.Rows[i]["AccountName"].ToString());
                            li.SubItems.Add(dt4.Rows[i]["ShortNarration"].ToString());
                            li.SubItems.Add(dt4.Rows[i]["Amount"].ToString());
                            li.SubItems.Add("0");
                        }
                    }
                }
                #endregion
                //Quick Payment
                #region
                DataTable dt5 = new DataTable();
                dt5 = conn.getdataset("select p.*,l.AccountName from paymentreceipt p inner join Ledger l on p.recno=l.VoucherID where p.isactive=1 and p.type='P' and l.isactive=1 and l.TranType='Pmnt' and l.Date1>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and l.Date1<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by l.Date1 asc");
                if (dt5.Rows.Count > 0)
                {
                    debit = 0;
                    credit = 0;

                    for (int i = 0; i < dt5.Rows.Count; i++)
                    {

                        if (Convert.ToDouble(dt5.Rows[i]["discountamt"].ToString()) != 0)
                        {
                            li = LVDayBook.Items.Add(Convert.ToDateTime(dt5.Rows[i]["Date"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add("Quick Payment");
                            li.SubItems.Add("Discount");
                            li.SubItems.Add(dt5.Rows[i]["remarks"].ToString());
                            li.SubItems.Add("0");
                            li.SubItems.Add(dt5.Rows[i]["discountamt"].ToString());
                            li = LVDayBook.Items.Add("");
                            li.SubItems.Add("");
                            li.SubItems.Add(dt5.Rows[i]["mode"].ToString());
                            li.SubItems.Add("");
                            li.SubItems.Add("0");
                            li.SubItems.Add(dt5.Rows[i]["netamt"].ToString());
                            li = LVDayBook.Items.Add("");
                            li.SubItems.Add("");
                            li.SubItems.Add(dt5.Rows[i]["AccountName"].ToString());
                            li.SubItems.Add("");
                            li.SubItems.Add(dt5.Rows[i]["totalamount"].ToString());
                            li.SubItems.Add("0");
                        }
                        else
                        {
                            li = LVDayBook.Items.Add(Convert.ToDateTime(dt5.Rows[i]["Date"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add("Quick Payment");
                            li.SubItems.Add(dt5.Rows[i]["mode"].ToString());
                            li.SubItems.Add(dt5.Rows[i]["remarks"].ToString());
                            li.SubItems.Add("0");
                            li.SubItems.Add(dt5.Rows[i]["netamt"].ToString());
                            li = LVDayBook.Items.Add("");
                            li.SubItems.Add("");
                            li.SubItems.Add(dt5.Rows[i]["AccountName"].ToString());
                            li.SubItems.Add("");
                            li.SubItems.Add(dt5.Rows[i]["totalamount"].ToString());
                            li.SubItems.Add("0");
                        }

                    }
                }
                #endregion
                //Quick Receipt
                #region
                DataTable dt6 = new DataTable();
                dt6 = conn.getdataset("select p.*,l.AccountName from paymentreceipt p inner join Ledger l on p.recno=l.VoucherID where p.isactive=1 and p.type='R' and l.isactive=1 and l.TranType='Rect' and l.Date1>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and l.Date1<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by l.Date1 asc");
                if (dt6.Rows.Count > 0)
                {
                    debit = 0;
                    credit = 0;

                    for (int i = 0; i < dt6.Rows.Count; i++)
                    {

                        if (Convert.ToDouble(dt6.Rows[i]["discountamt"].ToString()) != 0)
                        {
                            li = LVDayBook.Items.Add(Convert.ToDateTime(dt6.Rows[i]["Date"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add("Quick Receipt");
                            li.SubItems.Add(dt6.Rows[i]["AccountName"].ToString());
                            li.SubItems.Add(dt6.Rows[i]["remarks"].ToString());
                            li.SubItems.Add("0");
                            li.SubItems.Add(dt6.Rows[i]["totalamount"].ToString());
                            li = LVDayBook.Items.Add("");
                            li.SubItems.Add("");
                            li.SubItems.Add("Discount");
                            li.SubItems.Add("");
                            li.SubItems.Add(dt6.Rows[i]["discountamt"].ToString());
                            li.SubItems.Add("0");
                            li = LVDayBook.Items.Add("");
                            li.SubItems.Add("");
                            li.SubItems.Add(dt6.Rows[i]["mode"].ToString());
                            li.SubItems.Add("");
                            li.SubItems.Add(dt6.Rows[i]["netamt"].ToString());
                            li.SubItems.Add("0");


                        }
                        else
                        {
                            li = LVDayBook.Items.Add(Convert.ToDateTime(dt6.Rows[i]["Date"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add("Quick Receipt");
                            li.SubItems.Add(dt6.Rows[i]["AccountName"].ToString());
                            li.SubItems.Add(dt6.Rows[i]["remarks"].ToString());
                            li.SubItems.Add("0");
                            li.SubItems.Add(dt6.Rows[i]["totalamount"].ToString());
                            li = LVDayBook.Items.Add("");
                            li.SubItems.Add("");
                            li.SubItems.Add(dt6.Rows[i]["mode"].ToString());
                            li.SubItems.Add("");
                            li.SubItems.Add(dt6.Rows[i]["netamt"].ToString());
                            li.SubItems.Add("0");
                        }

                    }
                }
                #endregion
                //POS
                #region
                DataTable dt7 = new DataTable();
                dt7 = conn.getdataset("select * from BillPOSMaster where isactive=1 and BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by BillDate asc");
                if (dt7.Rows.Count > 0)
                {
                    debit = 0;
                    credit = 0;

                    for (int i = 0; i < dt7.Rows.Count; i++)
                    {
                        DataTable saletype = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypeid='" + dt7.Rows[i]["SaleTypeid"].ToString() + "'");
                        DataTable accgrpname = conn.getdataset("select * from AccountGroup where id='" + saletype.Rows[0]["Groupid"].ToString() + "'");

                        li = LVDayBook.Items.Add(Convert.ToDateTime(dt7.Rows[i]["BillDate"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Sale");
                        li.SubItems.Add(accgrpname.Rows[0]["groupname"].ToString());
                        li.SubItems.Add("");
                        li.SubItems.Add("0");
                        Double totalbasic = Convert.ToDouble(dt7.Rows[i]["totalbasic"].ToString()) - Convert.ToDouble(dt7.Rows[i]["disamt"].ToString()) - Convert.ToDouble(dt7.Rows[i]["adddisamt"].ToString());
                        li.SubItems.Add(Convert.ToString(totalbasic));
                        li = LVDayBook.Items.Add("");
                        li.SubItems.Add("");
                        li.SubItems.Add("SGST");
                        li.SubItems.Add("Bill No:" + dt7.Rows[i]["billno"].ToString() + ",Value=" + dt7.Rows[i]["totalnet"].ToString() + "," + dt7.Rows[i]["Terms"].ToString() + "");
                        li.SubItems.Add("0");
                        Double sgst = (Convert.ToDouble(dt7.Rows[i]["totaltax"].ToString()) / 2);
                        li.SubItems.Add(Convert.ToString(sgst));
                        li = LVDayBook.Items.Add("");
                        li.SubItems.Add("");
                        li.SubItems.Add("CGST");
                        li.SubItems.Add("Bill No:" + dt7.Rows[i]["billno"].ToString() + ",Value=" + dt7.Rows[i]["totalnet"].ToString() + "," + dt7.Rows[i]["Terms"].ToString() + "");
                        li.SubItems.Add("0");
                        Double cgst = (Convert.ToDouble(dt7.Rows[i]["totaltax"].ToString()) / 2);
                        li.SubItems.Add(Convert.ToString(cgst));
                        li = LVDayBook.Items.Add("");
                        li.SubItems.Add("");
                        li.SubItems.Add("Round Off");
                        li.SubItems.Add("");
                        li.SubItems.Add("0");
                        
                        if (dt7.Rows[i]["totaltoundoff"].ToString() == "")
                        {
                            dt7.Rows[i]["totaltoundoff"] = "0";
                            dt7.AcceptChanges();
                        }
                        li.SubItems.Add(Convert.ToString(dt7.Rows[i]["totaltoundoff"].ToString()));
                        li = LVDayBook.Items.Add("");
                        li.SubItems.Add("");
                        li.SubItems.Add(dt7.Rows[i]["Terms"].ToString());
                        li.SubItems.Add("Bill No:" + dt7.Rows[i]["billno"].ToString() + ",Value=" + dt7.Rows[i]["totalnet"].ToString() + "," + dt7.Rows[i]["Terms"].ToString() + "");
                        li.SubItems.Add(Convert.ToString(dt7.Rows[i]["totalnet"].ToString()));
                        li.SubItems.Add("0");
                        
                    }
                }
                #endregion
                //Sale Return
                #region
                DataTable dt8 = new DataTable();
                dt8 = conn.getdataset("select * from BillMaster where isactive=1 and BillType='SR' and Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Bill_Date asc");
                if (dt8.Rows.Count > 0)
                {
                    debit = 0;
                    credit = 0;

                    for (int i = 0; i < dt8.Rows.Count; i++)
                    {
                        DataTable client = conn.getdataset("select * from ClientMaster where isactive=1 and ClientID='" + dt8.Rows[i]["ClientID"].ToString() + "'");
                        DataTable saletype = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypeid='" + dt8.Rows[i]["SaleType"].ToString() + "'");
                        DataTable accgrpname = conn.getdataset("select * from AccountGroup where id='" + saletype.Rows[0]["Groupid"].ToString() + "'");
                        li = LVDayBook.Items.Add(Convert.ToDateTime(dt8.Rows[i]["Bill_Date"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Sale Return");
                        li.SubItems.Add(client.Rows[0]["AccountName"].ToString());
                        li.SubItems.Add("");
                        li.SubItems.Add("0");
                        li.SubItems.Add(dt8.Rows[i]["totalnet"].ToString());
                        DataTable charges1 = conn.getdataset("select perticulars,sum(amount) as amount from Billchargesmaster where isactive=1 and billno='" + dt8.Rows[i]["billno"].ToString() + "' and plusminus='-' group by perticulars");
                        if (charges1.Rows.Count > 0)
                        {
                            if (charges1.Rows.Count > 0)
                            {
                                for (int j = 0; j < charges1.Rows.Count; j++)
                                {
                                    li = LVDayBook.Items.Add("");
                                    li.SubItems.Add("");
                                    li.SubItems.Add(charges1.Rows[j]["perticulars"].ToString());
                                    li.SubItems.Add("");
                                    li.SubItems.Add("0");
                                    li.SubItems.Add(charges1.Rows[j]["amount"].ToString());
                                    
                                }
                            }
                        }
                        li = LVDayBook.Items.Add("");
                        li.SubItems.Add("");
                        li.SubItems.Add("SGST");
                        li.SubItems.Add("");
                        li.SubItems.Add(dt8.Rows[i]["sgstamt"].ToString());
                        li.SubItems.Add("0");
                        li = LVDayBook.Items.Add("");
                        li.SubItems.Add("");
                        li.SubItems.Add("CGST");
                        li.SubItems.Add("");
                        li.SubItems.Add(dt8.Rows[i]["cgatamt"].ToString());
                        li.SubItems.Add("0");
                        if (Convert.ToDouble(dt8.Rows[i]["roudoff"].ToString()) != 0)
                        {
                            li = LVDayBook.Items.Add("");
                            li.SubItems.Add("");
                            li.SubItems.Add("Round Off");
                            li.SubItems.Add("");
                            if (dt8.Rows[i]["roudoff"].ToString() == "")
                            {
                                dt8.Rows[i]["roudoff"] = "0";
                                dt8.AcceptChanges();
                            }
                            li.SubItems.Add(dt8.Rows[i]["roudoff"].ToString());
                            li.SubItems.Add("0");
                            
                        }
                        DataTable charges = conn.getdataset("select perticulars,sum(valueofexp) as valueofexp from Billchargesmaster where isactive=1 and billno='" + dt8.Rows[i]["billno"].ToString() + "' and plusminus='+' group by perticulars");
                        if (charges.Rows.Count > 0)
                        {
                            if (charges.Rows.Count > 0)
                            {
                                for (int j = 0; j < charges.Rows.Count; j++)
                                {
                                    li = LVDayBook.Items.Add("");
                                    li.SubItems.Add("");
                                    li.SubItems.Add(charges.Rows[j]["perticulars"].ToString());
                                    li.SubItems.Add("");
                                    li.SubItems.Add(charges.Rows[j]["valueofexp"].ToString());
                                    li.SubItems.Add("0");
                                    
                                }
                            }
                        }
                        li = LVDayBook.Items.Add("");
                        li.SubItems.Add("");
                        li.SubItems.Add(accgrpname.Rows[0]["groupname"].ToString());
                        li.SubItems.Add("");
                        Double totalbasic = Convert.ToDouble(dt8.Rows[i]["totalbasic"].ToString()) - Convert.ToDouble(dt8.Rows[i]["totaldiscount"].ToString()) - Convert.ToDouble(dt8.Rows[i]["totaladddiscount"].ToString());
                        li.SubItems.Add(Convert.ToString(totalbasic));
                        li.SubItems.Add("0");
                    }
                }
                #endregion
                //Purchase Return
                #region
                DataTable dt9 = new DataTable();
                dt9 = conn.getdataset("select * from BillMaster where isactive=1 and BillType='PR' and Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Bill_Date asc");
                if (dt9.Rows.Count > 0)
                {
                    debit = 0;
                    credit = 0;

                    for (int i = 0; i < dt9.Rows.Count; i++)
                    {
                        DataTable client = conn.getdataset("select * from ClientMaster where isactive=1 and ClientID='" + dt9.Rows[i]["ClientID"].ToString() + "'");
                        DataTable saletype = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypeid='" + dt9.Rows[i]["SaleType"].ToString() + "'");
                        DataTable accgrpname = conn.getdataset("select * from AccountGroup where id='" + saletype.Rows[0]["Groupid"].ToString() + "'");
                        li = LVDayBook.Items.Add(Convert.ToDateTime(dt9.Rows[i]["Bill_Date"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Purchase Return");
                        li.SubItems.Add(client.Rows[0]["AccountName"].ToString());
                        li.SubItems.Add("");
                        li.SubItems.Add(dt9.Rows[i]["totalnet"].ToString());
                        li.SubItems.Add("0");
                        DataTable charges1 = conn.getdataset("select perticulars,sum(amount) as amount from Billchargesmaster where isactive=1 and billno='" + dt9.Rows[i]["billno"].ToString() + "' and plusminus='-' group by perticulars");
                        if (charges1.Rows.Count > 0)
                        {
                            if (charges1.Rows.Count > 0)
                            {
                                for (int j = 0; j < charges1.Rows.Count; j++)
                                {
                                    li = LVDayBook.Items.Add("");
                                    li.SubItems.Add("");
                                    li.SubItems.Add(charges1.Rows[j]["perticulars"].ToString());
                                    li.SubItems.Add("");
                                    li.SubItems.Add(charges1.Rows[j]["amount"].ToString());
                                    li.SubItems.Add("0");

                                }
                            }
                        }
                        li = LVDayBook.Items.Add("");
                        li.SubItems.Add("");
                        li.SubItems.Add("SGST");
                        li.SubItems.Add("");
                        li.SubItems.Add("0");
                        li.SubItems.Add(dt9.Rows[i]["sgstamt"].ToString());
                        li = LVDayBook.Items.Add("");
                        li.SubItems.Add("");
                        li.SubItems.Add("CGST");
                        li.SubItems.Add("");
                        li.SubItems.Add("0");
                        li.SubItems.Add(dt9.Rows[i]["cgatamt"].ToString());
                        if (Convert.ToDouble(dt9.Rows[i]["roudoff"].ToString()) != 0)
                        {
                            li = LVDayBook.Items.Add("");
                            li.SubItems.Add("");
                            li.SubItems.Add("Round Off");
                            li.SubItems.Add("");
                            li.SubItems.Add("0");
                            if (dt9.Rows[i]["roudoff"].ToString() == "")
                            {
                                dt9.Rows[i]["roudoff"] = "0";
                                dt9.AcceptChanges();
                            }
                            li.SubItems.Add(dt9.Rows[i]["roudoff"].ToString());

                        }
                        DataTable charges = conn.getdataset("select perticulars,sum(valueofexp) as valueofexp from Billchargesmaster where isactive=1 and billno='" + dt9.Rows[i]["billno"].ToString() + "' and plusminus='+' group by perticulars");
                        if (charges.Rows.Count > 0)
                        {
                            if (charges.Rows.Count > 0)
                            {
                                for (int j = 0; j < charges.Rows.Count; j++)
                                {
                                    li = LVDayBook.Items.Add("");
                                    li.SubItems.Add("");
                                    li.SubItems.Add(charges.Rows[j]["perticulars"].ToString());
                                    li.SubItems.Add("");
                                    li.SubItems.Add("0");
                                    li.SubItems.Add(charges.Rows[j]["valueofexp"].ToString());

                                }
                            }
                        }
                        li = LVDayBook.Items.Add("");
                        li.SubItems.Add("");
                        li.SubItems.Add(accgrpname.Rows[0]["groupname"].ToString());
                        li.SubItems.Add("");
                        li.SubItems.Add("0");
                        Double totalbasic = Convert.ToDouble(dt9.Rows[i]["totalbasic"].ToString()) - Convert.ToDouble(dt9.Rows[i]["totaldiscount"].ToString()) - Convert.ToDouble(dt9.Rows[i]["totaladddiscount"].ToString());
                        li.SubItems.Add(Convert.ToString(totalbasic));
                        
                    }
                }
                #endregion
                foreach (ListViewItem lstItem in LVDayBook.Items)
                {
                    debit += decimal.Parse(lstItem.SubItems[4].Text);
                    credit += decimal.Parse(lstItem.SubItems[5].Text);
                }
                txttotaldr.Text = Convert.ToString(debit);
                txttotalcr.Text = Convert.ToString(credit);
            }
            catch
            {
            }
        }
        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            binddata();
        }

        private void DayBook_Load(object sender, EventArgs e)
        {
            try
            {
                LVDayBook.Columns.Add("Date", 100, HorizontalAlignment.Left);
                LVDayBook.Columns.Add("Type", 150, HorizontalAlignment.Left);
                LVDayBook.Columns.Add("Account", 210, HorizontalAlignment.Left);
                LVDayBook.Columns.Add("Remarks", 250, HorizontalAlignment.Left);
                LVDayBook.Columns.Add("Debit", 150, HorizontalAlignment.Right);
                LVDayBook.Columns.Add("Credit", 150, HorizontalAlignment.Right);

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
                BtnViewReport.Focus();
            }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Print DayBook?", "CashBook", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    prn.execute("delete from printing");
                    string status;
                    status = "Day Book From" + DTPFrom.Text;
                    for (int i = 0; i < LVDayBook.Items.Count; i++)
                    {
                        DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1 and CompanyID='" + Master.companyId + "' ");
                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20)VALUES";
                        qry += "('" + dt1.Rows[0]["CompanyName"].ToString() + "','" + dt1.Rows[0]["SubName"].ToString() + "','" + dt1.Rows[0]["Address"].ToString() + "','" + dt1.Rows[0]["Address2"].ToString() + "','" + dt1.Rows[0]["City"].ToString() + "','" + dt1.Rows[0]["State"].ToString() + "','" + dt1.Rows[0]["Country"].ToString() + "','" + dt1.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Mobile"].ToString() + "','" + dt1.Rows[0]["Email"].ToString() + "','" + dt1.Rows[0]["CSTNo"].ToString() + "','" + status + "','" + LVDayBook.Items[i].SubItems[0].Text + "','" + LVDayBook.Items[i].SubItems[1].Text + "','" + LVDayBook.Items[i].SubItems[2].Text + "','" + LVDayBook.Items[i].SubItems[3].Text + "','" + LVDayBook.Items[i].SubItems[4].Text +"','" + LVDayBook.Items[i].SubItems[5].Text+"','"+txttotalcr.Text+"','"+txttotaldr.Text+ "')";
                        prn.execute(qry);
                    }
                }
            }
            catch
            {
            }
        }
    }
}
