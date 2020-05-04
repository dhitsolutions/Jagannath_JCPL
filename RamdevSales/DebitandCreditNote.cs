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
    public partial class DebitandCreditNote : Form
    {
        private Master master;
        private TabControl tabControl;
        private string[] debitorcredit;
        public static string activecontroal;
        OleDbSettings ods = new OleDbSettings();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();

        public DebitandCreditNote()
        {
            InitializeComponent();
        }

        public DebitandCreditNote(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }

        public DebitandCreditNote(Master master, TabControl tabControl, string[] debitorcredit)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
            this.debitorcredit = debitorcredit;
        }
        public void bindcustomer()
        {
            string qry = "";
            qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=100 or groupid=99 or groupID=17 or groupid=18 or groupid=11 or groupid=24 or groupid=25 or groupid=26 or groupid=27) order by AccountName";
            //select id,groupname from accountgroup order by groupname asc
            SqlCommand cmd1 = new SqlCommand(qry, con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            cmbaccountName.ValueMember = "ClientID";
            cmbaccountName.DisplayMember = "AccountName";
            cmbaccountName.DataSource = dt1;
            cmbaccountName.SelectedIndex = -1;

        }
        DataTable userrights = new DataTable();
        public void loadpage()
        {
            try
            {
                if (cnt == 0)
                {
                    userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                    if (debitorcredit[0] == "D")
                    {
                        
                        if (userrights.Rows.Count > 0)
                        {
                            if (userrights.Rows[6]["a"].ToString() == "False")
                            {
                                BtnSubmit.Enabled = false;
                            }
                            if (userrights.Rows[6]["d"].ToString() == "False")
                            {
                                btndelete.Enabled = false;
                            }
                        }
                        txtheader.Text = "DEBIT NOTE";
                        this.Text = "DEBIT NOTE";
                        cmbdrcr.Text = "D";
                    }
                    else
                    {
                        if (userrights.Rows.Count > 0)
                        {
                            if (userrights.Rows[7]["a"].ToString() == "False")
                            {
                                BtnSubmit.Enabled = false;
                            }
                            if (userrights.Rows[7]["d"].ToString() == "False")
                            {
                                btndelete.Enabled = false;
                            }
                        }
                        txtheader.Text = "CREDIT NOTE";
                        this.Text = "CREDIT NOTE";
                        cmbdrcr.Text = "C";
                    }
                }
                this.ActiveControl = TxtRundate;
                DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
                TxtRundate.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                TxtRundate.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                TxtRundate.CustomFormat = Master.dateformate;
                lvserial.Columns.Add("DC", 100, HorizontalAlignment.Left);
                lvserial.Columns.Add("Account Name", 260, HorizontalAlignment.Left);
                lvserial.Columns.Add("Debit", 120, HorizontalAlignment.Left);
                lvserial.Columns.Add("Credit", 120, HorizontalAlignment.Left);
                lvserial.Columns.Add("type", 0, HorizontalAlignment.Left);
                lvserial.Columns.Add("Short Narration", 150, HorizontalAlignment.Left);
                lvserial.Columns.Add("AccountID", 0, HorizontalAlignment.Left);

                bindcustomer();
                txtcredittotal.Text = "0";
                txtdebittotal.Text = "0";
            }
            catch
            {
            }
        }
        int cnt = 0;
        private void DebitandCreditNote_Load(object sender, EventArgs e)
        {
            if (cnt == 0)
            {
                loadpage();
            }
        }

        private void TxtRundate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (BtnSubmit.Text != "Update")
                {
                voucherno();
                }
                txtvchno.Focus();
            }
        }

        private void txtvchno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbdrcr.Focus();
              //  cmbdrcr.BackColor = Color.LightBlue;
            }
        }

        private void txtvchno_Enter(object sender, EventArgs e)
        {
            txtvchno.BackColor = Color.LightYellow;
        }

        private void txtvchno_Leave(object sender, EventArgs e)
        {
            txtvchno.BackColor = Color.White;
        }

        private void cmbdrcr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               // cmbdrcr.BackColor = Color.White;
                cmbaccountName.Focus();
            }
        }

        private void cmbaccountName_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbaccountName.SelectedIndex = 0;
                cmbaccountName.DroppedDown = true;
            }
            catch
            {
            }
        }
        public static string s;
        private void cmbaccountName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {


                //for (int j = 0; j < lvserial.Items.Count; j++)
                //{
                //    if (lvserial.Items[j].ToString() == aname && lvserial.Items[j].ToString() == drcr)
                //    {
                //        //  DO NOT FILL
                //        MessageBox.Show("Same Account Cannot be Debited And Credited in a Single Voucher.");
                //    }
                //}
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbaccountName.Items.Count; i++)
                {
                    s = cmbaccountName.GetItemText(cmbaccountName.Items[i]);
                    if (s == cmbaccountName.Text)
                    {
                        inList = true;
                        cmbaccountName.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbaccountName.Text = "";
                }

                txtAmount.Focus();
            }
            if (e.KeyCode == Keys.F3)
            {
                var privouscontroal = cmbaccountName;
                activecontroal = privouscontroal.Name;
                Accountentry client = new Accountentry(this, master, tabControl, activecontroal);

                client.Passed(1);
                //   client.Show();
                master.AddNewTab(client);
            }
            if (e.KeyCode == Keys.F2)
            {
                var privouscontroal = cmbaccountName;
                activecontroal = privouscontroal.Name;
                string iid = cmbaccountName.SelectedValue.ToString();

                Accountentry client = new Accountentry(this, master, tabControl, activecontroal);
                client.Update(1, iid);
                client.Passed(1);
                //  client.Show();
                master.AddNewTab(client);
            }
        }

        private void cmbaccountName_Leave(object sender, EventArgs e)
        {
            cmbaccountName.Text = s;
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!String.IsNullOrEmpty(txtAmount.Text))
                {
                    txtshortnarration.Focus();
                }
            }
        }

        private void txtAmount_Enter(object sender, EventArgs e)
        {
            txtAmount.BackColor = Color.LightYellow;
        }

        private void txtAmount_Leave(object sender, EventArgs e)
        {
            txtAmount.BackColor = Color.White;
        }
        ListViewItem li;
        private void txtshortnarration_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string aname = cmbaccountName.Text;
                    string drcr = cmbdrcr.Text;
                    if (drcr == "D")
                    {
                        drcr = "C";
                    }
                    else
                    {
                        drcr = "D";
                    }
                    foreach (ListViewItem lstItem in lvserial.Items)
                    {
                        if (lstItem.SubItems[1].Text == aname && lstItem.SubItems[0].Text == drcr)
                        {
                            //  DO NOT FILL
                            MessageBox.Show("Same Account Cannot be Debited And Credited in a Single Voucher.");
                            txtshortnarration.Focus();
                            return;
                        }
                    }
                    if (rowid >= 0)
                    {
                        lvserial.Items[rowid].SubItems[0].Text = cmbdrcr.Text;
                        lvserial.Items[rowid].SubItems[1].Text = cmbaccountName.Text;
                        if (cmbdrcr.Text == "D")
                        {
                            lvserial.Items[rowid].SubItems[2].Text = txtAmount.Text;
                            lvserial.Items[rowid].SubItems[3].Text = "0";
                            lvserial.Items[rowid].SubItems[4].Text = "DEBIT NOTE";
                        }
                        else
                        {
                            lvserial.Items[rowid].SubItems[2].Text = "0";
                            lvserial.Items[rowid].SubItems[3].Text = txtAmount.Text;
                            lvserial.Items[rowid].SubItems[4].Text = "CREDIT NOTE";
                        }

                        lvserial.Items[rowid].SubItems[5].Text = txtshortnarration.Text;
                        lvserial.Items[rowid].SubItems[6].Text = Convert.ToString(cmbaccountName.SelectedValue);
                        rowid = -1;
                        decimal debit = 0;
                        decimal credit = 0;
                        foreach (ListViewItem lstItem in lvserial.Items)
                        {
                            debit += decimal.Parse(lstItem.SubItems[2].Text);
                            credit += decimal.Parse(lstItem.SubItems[3].Text);
                        }
                        txtdebittotal.Text = Convert.ToString(debit);
                        txtcredittotal.Text = Convert.ToString(credit);
                        if (txtcredittotal.Text == txtdebittotal.Text)
                        {
                            BtnSubmit.Focus();
                        }
                        else
                        {
                            cmbdrcr.Focus();
                        }
                        txtAmount.Text = "";
                        txtshortnarration.Text = "";
                    }
                    else
                    {
                        //lvserial.Items.Clear();
                        li = lvserial.Items.Add(cmbdrcr.Text);
                        li.SubItems.Add(cmbaccountName.Text);
                        if (cmbdrcr.Text == "D")
                        {
                            li.SubItems.Add(txtAmount.Text);
                            li.SubItems.Add("0");
                            li.SubItems.Add("DEBIT NOTE");
                        }
                        else
                        {
                            li.SubItems.Add("0");
                            li.SubItems.Add(txtAmount.Text);
                            li.SubItems.Add("CREDIT NOTE");
                        }
                        li.SubItems.Add(txtshortnarration.Text);
                        li.SubItems.Add(Convert.ToString(cmbaccountName.SelectedValue));
                        decimal debit = 0;
                        decimal credit = 0;
                        foreach (ListViewItem lstItem in lvserial.Items)
                        {
                            debit += decimal.Parse(lstItem.SubItems[2].Text);
                            credit += decimal.Parse(lstItem.SubItems[3].Text);
                        }
                        txtdebittotal.Text = Convert.ToString(debit);
                        txtcredittotal.Text = Convert.ToString(credit);
                        if (txtcredittotal.Text == txtdebittotal.Text)
                        {
                            BtnSubmit.Focus();
                        }
                        else
                        {
                            cmbdrcr.Focus();
                        }
                        txtAmount.Text = "";
                        txtshortnarration.Text = "";
                        if (cmbdrcr.Text == "D")
                        {
                            cmbdrcr.Text = "C";
                        }
                        else
                        {
                            cmbdrcr.Text = "D";
                        }
                    }
                }
            }
            catch
            {
            }
        }
        public void clearall()
        {
            txtvchno.Text = "";
            cmbdrcr.SelectedIndex = -1;
            cmbaccountName.SelectedIndex = -1;
            txtAmount.Text = "";
            txtshortnarration.Text = "";
            txtlongnarration.Text = "";
            txtdebittotal.Text = "";
            txtcredittotal.Text = "";
            lvserial.Items.Clear();
            TxtRundate.Focus();
        }
        string vono, strvono;
        public void voucherno()
        {
            //vono = conn.ExecuteScalar("select max(Voucherid) as Voucherid from Ledger where isactive=1 and OT7='Bank Entry'");
            vono = conn.ExecuteScalar("select max(VoucherID) as VoucherID from Ledger where isactive=1 and TranType='" + txtheader.Text + "'");
            Int64 id, count;
            if (vono == "")
            {

                id = 1;
                count = 1;
            }
            else
            {
                id = Convert.ToInt32(vono) + 1;
                count = Convert.ToInt32(vono) + 1;
            }
            strvono = Convert.ToString(id);
            txtvchno.Text = strvono;
        }
        private void txtshortnarration_Enter(object sender, EventArgs e)
        {
            txtshortnarration.BackColor = Color.LightYellow;
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
            if (keyData == (Keys.Alt | Keys.U))
            {
                enterdata();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        string amount;
        string partyname123;
        string partynamedebit;
        string crdr;
        public void enterdata()
        {
            try
            {
                if (BtnSubmit.Text == "Update")
                {
                    if (txtheader.Text == "CREDIT NOTE")
                    {
                        if (userrights.Rows.Count > 0)
                        {
                            if (userrights.Rows[6]["u"].ToString() == "False")
                            {
                                MessageBox.Show("You don't have Permission To Update");
                                return;
                            }
                        }
                    }
                    else if (txtheader.Text == "DEBIT NOTE")
                    {
                        if (userrights.Rows.Count > 0)
                        {
                            if (userrights.Rows[7]["u"].ToString() == "False")
                            {
                                MessageBox.Show("You don't have Permission To Update");
                                return;
                            }
                        }
                    }
                    if (txtdebittotal.Text == txtcredittotal.Text)
                    {
                        for (int i = 0; i < lvserial.Items.Count; i++)
                        {
                            DataTable due = conn.getdataset("select * from ClientMaster where isactive=1 and AccountName='" + lvserial.Items[i].SubItems[1].Text.Replace(",", "") + "'");
                            //if (due.Rows.Count > 0)
                            //{
                                DateTime billdate = TxtRundate.Value;
                                string creditdays = due.Rows[0]["credaysale"].ToString();
                                if (string.IsNullOrEmpty(creditdays))
                                {
                                    creditdays = "0";
                                }
                                DateTime duedate = billdate.AddDays(Convert.ToDouble(creditdays));
                                string due1 = duedate.ToString(Master.dateformate);
                          //  }
                            partyname123 = "";
                            string dc = lvserial.Items[i].SubItems[0].Text.Replace(",", "");
                            if (dc == "D")
                            {
                                conn.execute("UPDATE [dbo].[Ledger] SET [isactive]='0' where [VoucherID]='" + txtvchno.Text + "'and [OT4]='" + lvserial.Items[i].SubItems[4].Text.Replace(",", "") + "' and [AccountName]='" + lvserial.Items[i].SubItems[1].Text.Replace(",", "") + "'");
                                for (int j = 0; j < lvserial.Items.Count; j++)
                                {

                                    if (lvserial.Items[j].SubItems[0].Text.Replace(",", "") == "C")
                                    {
                                        partyname123 += lvserial.Items[j].SubItems[1].Text.Replace(",", "") + ",";
                                    }
                                }
                                if (lvserial.Items[i].SubItems[0].Text.Replace(",", "") == "D")
                                {
                                    amount = lvserial.Items[i].SubItems[2].Text.Replace(",", "");
                                }
                                else
                                {
                                    amount = lvserial.Items[i].SubItems[3].Text.Replace(",", "");
                                }
                                partyname123 = partyname123.TrimEnd(',');
                                string cashornot = "";
                                if (lvserial.Items[i].SubItems[1].Text.Replace(",", "") == "Cash")
                                {
                                    cashornot = "Cash";
                                }
                                else
                                {
                                    cashornot = "Credit";
                                }
                               // conn.execute("UPDATE [dbo].[Ledger] SET [AccountID]='" + lvserial.Items[i].SubItems[6].Text.Replace(",", "") + "',[Date1]='" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',[AccountName]='" + lvserial.Items[i].SubItems[1].Text.Replace(",", "") + "',[Amount]='" + amount + "',[DC]='" + lvserial.Items[i].SubItems[0].Text.Replace(",", "") + "',[ShortNarration]='" + lvserial.Items[i].SubItems[5].Text.Replace(",", "") + "',[OT4]='" + lvserial.Items[i].SubItems[4].Text.Replace(",", "") + "',[OT1]='" + lvserial.Items[i].SubItems[1].Text.Replace(",", "") + "',[OT2]='" + txtdebittotal.Text + "',[OT3]='" + txtcredittotal.Text + "',[OT5]='" + txtlongnarration.Text + "' where [VoucherID]='" + txtvchno.Text + "'and [OT4]='" + lvserial.Items[i].SubItems[4].Text.Replace(",", "") + "'");
                                conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountName],[Amount],[DC],[ShortNarration],[isactive],[OT1],[OT2],[OT3],[AccountID],[OT4],[OT5],[OT6],[OD1]) values ('" + txtvchno.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + txtheader.Text + "','" + lvserial.Items[i].SubItems[1].Text.Replace(",", "") + "','" + amount + "','" + lvserial.Items[i].SubItems[0].Text.Replace(",", "") + "','" + lvserial.Items[i].SubItems[5].Text.Replace(",", "") + "',1,'" + cashornot + "','" + txtdebittotal.Text + "','" + txtcredittotal.Text + "','" + lvserial.Items[i].SubItems[6].Text.Replace(",", "") + "','" + lvserial.Items[i].SubItems[4].Text.Replace(",", "") + "','" + txtlongnarration.Text + "','" + partyname123 + "','" + Convert.ToDateTime(due1).ToString(Master.dateformate) + "')");
                            }
                            else
                            {
                                conn.execute("UPDATE [dbo].[Ledger] SET [isactive]='0' where [VoucherID]='" + txtvchno.Text + "'and [OT4]='" + lvserial.Items[i].SubItems[4].Text.Replace(",", "") + "' and [AccountName]='" + lvserial.Items[i].SubItems[1].Text.Replace(",", "") + "'");
                                for (int j = 0; j < lvserial.Items.Count; j++)
                                {

                                    if (lvserial.Items[j].SubItems[0].Text.Replace(",", "") == "D")
                                    {
                                        partyname123 += lvserial.Items[j].SubItems[1].Text.Replace(",", "") + ",";
                                    }
                                }
                                if (lvserial.Items[i].SubItems[0].Text.Replace(",", "") == "D")
                                {
                                    amount = lvserial.Items[i].SubItems[2].Text.Replace(",", "");
                                }
                                else
                                {
                                    amount = lvserial.Items[i].SubItems[3].Text.Replace(",", "");
                                }
                                partyname123 = partyname123.TrimEnd(',');
                                //conn.execute("UPDATE [dbo].[Ledger] SET [AccountID]='" + lvserial.Items[i].SubItems[6].Text.Replace(",", "") + "',[Date1]='" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',[AccountName]='" + lvserial.Items[i].SubItems[1].Text.Replace(",", "") + "',[Amount]='" + amount + "',[DC]='" + lvserial.Items[i].SubItems[0].Text.Replace(",", "") + "',[ShortNarration]='" + lvserial.Items[i].SubItems[5].Text.Replace(",", "") + "',[OT4]='" + lvserial.Items[i].SubItems[4].Text.Replace(",", "") + "',[OT1]='" + lvserial.Items[i].SubItems[1].Text.Replace(",", "") + "',[OT2]='" + txtdebittotal.Text + "',[OT3]='" + txtcredittotal.Text + "',[OT5]='" + txtlongnarration.Text + "' where [VoucherID]='" + txtvchno.Text + "'and [OT4]='" + lvserial.Items[i].SubItems[4].Text.Replace(",", "") + "'");
                                string cashornot = "";
                                if (lvserial.Items[i].SubItems[1].Text.Replace(",", "") == "Cash")
                                {
                                    cashornot = "Cash";
                                }
                                else
                                {
                                    cashornot = "Credit";
                                }
                                conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountName],[Amount],[DC],[ShortNarration],[isactive],[OT1],[OT2],[OT3],[AccountID],[OT4],[OT5],[OT6],[OD1]) values ('" + txtvchno.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + txtheader.Text + "','" + lvserial.Items[i].SubItems[1].Text.Replace(",", "") + "','" + amount + "','" + lvserial.Items[i].SubItems[0].Text.Replace(",", "") + "','" + lvserial.Items[i].SubItems[5].Text.Replace(",", "") + "',1,'" + cashornot + "','" + txtdebittotal.Text + "','" + txtcredittotal.Text + "','" + lvserial.Items[i].SubItems[6].Text.Replace(",", "") + "','" + lvserial.Items[i].SubItems[4].Text.Replace(",", "") + "','" + txtlongnarration.Text + "','" + partyname123 + "','" + Convert.ToDateTime(due1).ToString(Master.dateformate) + "')");
                            }
                         
                        }
                        MessageBox.Show(txtheader.Text + " Entry Saved");
                        clearall();
                        this.ActiveControl = TxtRundate;
                    }
                    else
                    {
                        MessageBox.Show("Debit & Credit Totals Should be Equal");
                        cmbdrcr.Focus();
                    }
                }
                else
                {
                    if (txtdebittotal.Text == txtcredittotal.Text)
                    {
                        for (int i = 0; i < lvserial.Items.Count; i++)
                        {

                            DataTable due = conn.getdataset("select * from ClientMaster where isactive=1 and AccountName='" + lvserial.Items[i].SubItems[1].Text.Replace(",", "") + "'");
                            //if (due.Rows.Count > 0)
                            //{
                            DateTime billdate = TxtRundate.Value;
                            string creditdays = due.Rows[0]["credaysale"].ToString();
                            if (string.IsNullOrEmpty(creditdays))
                            {
                                creditdays = "0";
                            }
                            DateTime duedate = billdate.AddDays(Convert.ToDouble(creditdays));
                            string due1 = duedate.ToString(Master.dateformate);
                            //  }
                            partyname123 = "";
                            string dc = lvserial.Items[i].SubItems[0].Text.Replace(",", "");
                            if (dc == "D")
                            {

                                for (int j = 0; j < lvserial.Items.Count; j++)
                                {

                                    if (lvserial.Items[j].SubItems[0].Text.Replace(",", "") == "C")
                                    {
                                        partyname123 += lvserial.Items[j].SubItems[1].Text.Replace(",", "") + ",";
                                    }
                                }
                                if (lvserial.Items[i].SubItems[0].Text.Replace(",", "") == "D")
                                {
                                    amount = lvserial.Items[i].SubItems[2].Text.Replace(",", "");
                                }
                                else
                                {
                                    amount = lvserial.Items[i].SubItems[3].Text.Replace(",", "");
                                }
                                partyname123 = partyname123.TrimEnd(',');
                                string cashornot = "";
                                if (lvserial.Items[i].SubItems[1].Text.Replace(",", "") == "Cash")
                                {
                                    cashornot = "Cash";
                                }
                                else
                                {
                                    cashornot = "Credit";
                                }
                                conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountName],[Amount],[DC],[ShortNarration],[isactive],[OT1],[OT2],[OT3],[AccountID],[OT4],[OT5],[OT6],[OD1]) values ('" + txtvchno.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + txtheader.Text + "','" + lvserial.Items[i].SubItems[1].Text.Replace(",", "") + "','" + amount + "','" + lvserial.Items[i].SubItems[0].Text.Replace(",", "") + "','" + lvserial.Items[i].SubItems[5].Text.Replace(",", "") + "',1,'" + cashornot + "','" + txtdebittotal.Text + "','" + txtcredittotal.Text + "','" + lvserial.Items[i].SubItems[6].Text.Replace(",", "") + "','" + lvserial.Items[i].SubItems[4].Text.Replace(",", "") + "','" + txtlongnarration.Text + "','" + partyname123 +"','"+ Convert.ToDateTime(due1).ToString(Master.dateformate) + "')");
                            }

                            else
                            {
                                for (int j = 0; j < lvserial.Items.Count; j++)
                                {

                                    if (lvserial.Items[j].SubItems[0].Text.Replace(",", "") == "D")
                                    {
                                        partyname123 += lvserial.Items[j].SubItems[1].Text.Replace(",", "") + ",";
                                    }
                                }
                                if (lvserial.Items[i].SubItems[0].Text.Replace(",", "") == "D")
                                {
                                    amount = lvserial.Items[i].SubItems[2].Text.Replace(",", "");
                                }
                                else
                                {
                                    amount = lvserial.Items[i].SubItems[3].Text.Replace(",", "");
                                }
                                partyname123 = partyname123.TrimEnd(',');
                                string cashornot = "";
                                if (lvserial.Items[i].SubItems[1].Text.Replace(",", "") == "Cash")
                                {
                                    cashornot = "Cash";
                                }
                                else
                                {
                                    cashornot = "Credit";
                                }
                                conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountName],[Amount],[DC],[ShortNarration],[isactive],[OT1],[OT2],[OT3],[AccountID],[OT4],[OT5],[OT6],[OD1]) values ('" + txtvchno.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + txtheader.Text + "','" + lvserial.Items[i].SubItems[1].Text.Replace(",", "") + "','" + amount + "','" + lvserial.Items[i].SubItems[0].Text.Replace(",", "") + "','" + lvserial.Items[i].SubItems[5].Text.Replace(",", "") + "',1,'" + cashornot + "','" + txtdebittotal.Text + "','" + txtcredittotal.Text + "','" + lvserial.Items[i].SubItems[6].Text.Replace(",", "") + "','" + lvserial.Items[i].SubItems[4].Text.Replace(",", "") + "','" + txtlongnarration.Text + "','" + partyname123 + "','" + Convert.ToDateTime(due1).ToString(Master.dateformate) + "')");

                              
                            }
                        }
                        MessageBox.Show(txtheader.Text + " Entry Saved");
                        clearall();
                        this.ActiveControl = TxtRundate;
                    }
                    else
                    {
                        MessageBox.Show("Debit & Credit Totals Shouldbe Equal");
                        cmbdrcr.Focus();
                    }
                }
            }
            catch
            {
            }
        }
        private void txtshortnarration_Leave(object sender, EventArgs e)
        {
            txtshortnarration.BackColor = Color.White;
        }

        private void txtlongnarration_Enter(object sender, EventArgs e)
        {
            txtlongnarration.BackColor = Color.LightYellow;
        }

        private void txtlongnarration_Leave(object sender, EventArgs e)
        {
            txtlongnarration.BackColor = Color.White;
        }

        private void txtdebittotal_Enter(object sender, EventArgs e)
        {
            txtdebittotal.BackColor = Color.LightYellow;
        }

        private void txtdebittotal_Leave(object sender, EventArgs e)
        {
            txtdebittotal.BackColor = Color.White;
        }

        private void txtcredittotal_Enter(object sender, EventArgs e)
        {
            txtcredittotal.BackColor = Color.LightYellow;
        }

        private void txtcredittotal_Leave(object sender, EventArgs e)
        {
            txtcredittotal.BackColor = Color.White;
        }

        private void BtnSubmit_Enter(object sender, EventArgs e)
        {
            BtnSubmit.UseVisualStyleBackColor = false;
            BtnSubmit.BackColor = Color.YellowGreen;
            BtnSubmit.ForeColor = Color.White;
        }

        private void BtnSubmit_Leave(object sender, EventArgs e)
        {
            BtnSubmit.UseVisualStyleBackColor = true;
            BtnSubmit.BackColor = Color.FromArgb(51, 153, 255);
            BtnSubmit.ForeColor = Color.White;
        }

        private void BtnSubmit_MouseEnter(object sender, EventArgs e)
        {
            BtnSubmit.UseVisualStyleBackColor = false;
            BtnSubmit.BackColor = Color.YellowGreen;
            BtnSubmit.ForeColor = Color.White;
        }

        private void BtnSubmit_MouseLeave(object sender, EventArgs e)
        {
            BtnSubmit.UseVisualStyleBackColor = true;
            BtnSubmit.BackColor = Color.FromArgb(51, 153, 255);
            BtnSubmit.ForeColor = Color.White;
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

        private void cmbdrcr_Enter(object sender, EventArgs e)
        {
            try
            {
                // cmbdrcr.SelectedIndex = 0;
               // cmbdrcr.DroppedDown = true;
            }
            catch
            {
            }
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            enterdata();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }
        Int32 rowid = -1;
        public void open()
        {
            try
            {
                if (lvserial.SelectedItems.Count > 0)
                {
                    rowid = lvserial.FocusedItem.Index;
                    cmbdrcr.Text = lvserial.Items[lvserial.FocusedItem.Index].SubItems[0].Text;
                    cmbaccountName.Text = lvserial.Items[lvserial.FocusedItem.Index].SubItems[1].Text;
                    if (lvserial.Items[lvserial.FocusedItem.Index].SubItems[0].Text == "D")
                    {
                        txtAmount.Text = lvserial.Items[lvserial.FocusedItem.Index].SubItems[2].Text;
                        txtdebittotal.Text = "0";
                    }
                    else
                    {
                        txtAmount.Text = lvserial.Items[lvserial.FocusedItem.Index].SubItems[3].Text;
                        txtcredittotal.Text = "0";
                    }
                    txtshortnarration.Text = lvserial.Items[lvserial.FocusedItem.Index].SubItems[5].Text;
                    cmbdrcr.Focus();
                }
            }
            catch
            {
            }
        }
        private void lvserial_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            open();
        }

        internal void updatemode(string str, string type, string[] debitorcredit)
        {
            userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
            cnt = 1;
            DataTable update = conn.getdataset("select * from Ledger where isactive=1 and VoucherID='" + str + "' and TranType='" + type + "'");
            //string[] a = new string[update.Rows.Count];
            //for (int j = 0; j < update.Rows.Count; j++)
            //{
            //    a[j] = update.Rows[j]["DC"].ToString();
            //}
            //string[] debitorcredit = new string[5] { a[], "", "", "", "" };
            if (debitorcredit[0] == "D")
            {
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[6]["a"].ToString() == "False")
                    {
                        BtnSubmit.Enabled = false;
                    }
                    if (userrights.Rows[6]["d"].ToString() == "False")
                    {
                        btndelete.Enabled = false;
                    }
                }
                txtheader.Text = "DEBIT NOTE";
                this.Text = "DEBIT NOTE";
                cmbdrcr.Text = "D";
            }
            else
            {
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[7]["a"].ToString() == "False")
                    {
                        BtnSubmit.Enabled = false;
                    }
                    if (userrights.Rows[7]["d"].ToString() == "False")
                    {
                        btndelete.Enabled = false;
                    }
                }
                txtheader.Text = "CREDIT NOTE";
                this.Text = "CREDIT NOTE";
                cmbdrcr.Text = "C";
            }
            loadpage();
            txtvchno.Text = str;
            for (int i = 0; i < update.Rows.Count; i++)
            {
                li = lvserial.Items.Add(update.Rows[i]["DC"].ToString());
                li.SubItems.Add(update.Rows[i]["AccountName"].ToString());
                if (update.Rows[i]["DC"].ToString() == "D")
                {
                    li.SubItems.Add(update.Rows[i]["Amount"].ToString());
                    li.SubItems.Add("0");
                    li.SubItems.Add(update.Rows[i]["OT4"].ToString());
                }
                else
                {
                    li.SubItems.Add("0");
                    li.SubItems.Add(update.Rows[i]["Amount"].ToString());
                    li.SubItems.Add(update.Rows[i]["OT4"].ToString());
                }
                li.SubItems.Add(update.Rows[i]["ShortNarration"].ToString());
                li.SubItems.Add(Convert.ToString(update.Rows[i]["AccountID"].ToString()));
                decimal debit = 0;
                decimal credit = 0;
                foreach (ListViewItem lstItem in lvserial.Items)
                {
                    debit += decimal.Parse(lstItem.SubItems[2].Text);
                    credit += decimal.Parse(lstItem.SubItems[3].Text);
                }
                txtdebittotal.Text = Convert.ToString(debit);
                txtcredittotal.Text = Convert.ToString(credit);
                BtnSubmit.Text = "Update";
            }
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 45 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            var privouscontroal = cmbaccountName;
            activecontroal = privouscontroal.Name;
            Accountentry client = new Accountentry(this, master, tabControl, activecontroal);

            client.Passed(1);
            //   client.Show();
            master.AddNewTab(client);
        }

        private void btnAccountEdit_Click(object sender, EventArgs e)
        {
            if (cmbaccountName.Text != "" && cmbaccountName.Text != null)
            {
                var privouscontroal = cmbaccountName;
                activecontroal = privouscontroal.Name;
                string iid = cmbaccountName.SelectedValue.ToString();
                Accountentry client = new Accountentry(this, master, tabControl, activecontroal);
                client.Update(1, iid);
                client.Passed(1);
                //  client.Show();
                master.AddNewTab(client);
            }
            else
            {
                MessageBox.Show("Please Select Account Name.");
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Delete?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    conn.execute("Update Ledger set isactive=0 where VoucherID='" + txtvchno.Text + "' and TranType='"+txtheader.Text+"'");
                    MessageBox.Show("Delete Successfully");
                }
            }
            catch (Exception excp)
            {

            }
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

        private void lvserial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                lvserial.Items[lvserial.FocusedItem.Index].Remove();
                decimal debit = 0;
                decimal credit = 0;
                foreach (ListViewItem lstItem in lvserial.Items)
                {
                    debit += decimal.Parse(lstItem.SubItems[2].Text);
                    credit += decimal.Parse(lstItem.SubItems[3].Text);
                }
                txtdebittotal.Text = Convert.ToString(debit);
                txtcredittotal.Text = Convert.ToString(credit);
            }
            if (e.KeyCode == Keys.Enter)
            {
                open();
            }
        }

        private void cmbaccountName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbaccountName.Items.Count; i++)
                {
                    s = cmbaccountName.GetItemText(cmbaccountName.Items[i]);
                    if (s == cmbaccountName.Text)
                    {
                        inList = true;
                        cmbaccountName.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbaccountName.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }
    }
}
