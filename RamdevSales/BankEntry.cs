using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace RamdevSales
{
    public partial class BankEntry : Form
    {
        private Master master;
        private TabControl tabControl;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        Printing prn = new Printing();
        OleDbSettings ods = new OleDbSettings();
        string vono, strvono;
        public static string activecontroal;
        public BankEntry()
        {
            InitializeComponent();
        }

        public BankEntry(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }
        DataTable userrights = new DataTable();
        private void BankEntry_Load(object sender, EventArgs e)
        {
            try
            {
                if (cnt == 0)
                {
                    userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[20]["p"].ToString() == "False")
                        {
                            btnPrint.Enabled = false;
                        }
                        if (userrights.Rows[20]["a"].ToString() == "False")
                        {
                            BtnSubmit.Enabled = false;
                        }
                        if (userrights.Rows[20]["d"].ToString() == "False")
                        {
                            btndelete.Enabled = false;
                        }
                    }
                    loadpage();
                }
            }
            catch
            {
            }
        }
        int cnt = 0;
        public void loadpage()
        {
            DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
            TxtRundate.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            TxtRundate.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            TxtRundate.Focus();
            this.ActiveControl = TxtRundate;
            bindbank();
            bindcustomer();
            lvserial.CheckBoxes = true;
            lvserial.Columns.Add("", 20, HorizontalAlignment.Left);
            lvserial.Columns.Add("ParyID", 0, HorizontalAlignment.Left);
            lvserial.Columns.Add("Party Name", 260, HorizontalAlignment.Left);
            lvserial.Columns.Add("Chq No.", 120, HorizontalAlignment.Left);
            lvserial.Columns.Add("Chq Dt.", 120, HorizontalAlignment.Left);
            lvserial.Columns.Add("Amount", 140, HorizontalAlignment.Left);
            lvserial.Columns.Add("Exp.", 100, HorizontalAlignment.Left);
            lvserial.Columns.Add("Total Amount", 130, HorizontalAlignment.Left);
            lvserial.Columns.Add("Remarks", 130, HorizontalAlignment.Left);
            //timer1.Interval = 1000;
            //timer1.Start();
            TxtRundate.CustomFormat = Master.dateformate;
        }
        public void bindbank()
        {
            string qry = "";
            qry = "select ClientID,AccountName from ClientMaster where isactive=1 and groupID=12 order by AccountName";
            //select id,groupname from accountgroup order by groupname asc
            SqlCommand cmd1 = new SqlCommand(qry, con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            cmbBankSelect.ValueMember = "ClientID";
            cmbBankSelect.DisplayMember = "AccountName";
            cmbBankSelect.DataSource = dt1;
            cmbBankSelect.SelectedIndex = -1;


            //  autobind(dt1, cmbcustname);
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
            cmbPartyName.ValueMember = "ClientID";
            cmbPartyName.DisplayMember = "AccountName";
            cmbPartyName.DataSource = dt1;
            cmbPartyName.SelectedIndex = -1;

        }
        public void popup()
        {
            try
            {
                DataTable dt = new DataTable();
                // DataTable dt = conn.getdataset("select Date1,VoucherID,OT3 from Ledger where isactive=1 and Ot7='Bank Entry' and AccountID='" + cmbPartyName.SelectedValue + "'");
                if (cmbEntry.Text == "Cheque Issued")
                {
                    dt = conn.getdataset("select bill_date,billno,totalnet from billmaster where OrderStatus='Pending' and clientid='" + cmbPartyName.SelectedValue + "' and Billtype='P' and isactive='1'");
                }
                else if (cmbEntry.Text == "Draft Issued")
                {
                    dt = conn.getdataset("select bill_date,billno,totalnet from billmaster where OrderStatus='Pending' and clientid='" + cmbPartyName.SelectedValue + "' and Billtype='P' and isactive='1'");
                }
                else if (cmbEntry.Text == "Cheque/Draft/Rtgs Received")
                {
                    dt = conn.getdataset("select bill_date,billno,totalnet from billmaster where OrderStatus='Pending' and clientid='" + cmbPartyName.SelectedValue + "' and Billtype='S' and isactive='1'");
                }
                else if (cmbEntry.Text == "Online Transfer")
                {
                    dt = conn.getdataset("select bill_date,billno,totalnet from billmaster where OrderStatus='Pending' and clientid='" + cmbPartyName.SelectedValue + "' and Billtype='P' and isactive='1'");
                }
                if (dt.Rows.Count > 0)
                {
                    SelectBankBills popup = new SelectBankBills(txtTotalAmount, dt, cmbPartyName.SelectedValue, cmbPartyName.Text, this, BtnSubmit.Text, lblvno.Text);

                    popup.ShowDialog();

                    string userEnteredText = popup.EnteredText;

                    popup.Dispose();
                }
            }
            catch
            {
            }
        }
        public DataTable dtselectedrow = new DataTable();
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

        private void btnPrint_Enter(object sender, EventArgs e)
        {
            btnPrint.UseVisualStyleBackColor = false;
            btnPrint.BackColor = Color.FromArgb(176, 111, 193);
            btnPrint.ForeColor = Color.White;
        }

        private void btnPrint_Leave(object sender, EventArgs e)
        {
            btnPrint.UseVisualStyleBackColor = true;
            btnPrint.BackColor = Color.FromArgb(51, 153, 255);
            btnPrint.ForeColor = Color.White;
        }

        private void btnPrint_MouseEnter(object sender, EventArgs e)
        {
            btnPrint.UseVisualStyleBackColor = false;
            btnPrint.BackColor = Color.FromArgb(176, 111, 193);
            btnPrint.ForeColor = Color.White;
        }

        private void btnPrint_MouseLeave(object sender, EventArgs e)
        {
            btnPrint.UseVisualStyleBackColor = true;
            btnPrint.BackColor = Color.FromArgb(51, 153, 255);
            btnPrint.ForeColor = Color.White;
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

        private void txtAmount_Enter(object sender, EventArgs e)
        {
            txtAmount.BackColor = Color.LightYellow;
        }

        private void txtAmount_Leave(object sender, EventArgs e)
        {
            txtAmount.BackColor = Color.White;
        }

        private void txtChequeNo_Enter(object sender, EventArgs e)
        {
            txtChequeNo.BackColor = Color.LightYellow;
        }

        private void txtChequeNo_Leave(object sender, EventArgs e)
        {
            txtChequeNo.BackColor = Color.White;
        }

        private void txtDated_Enter(object sender, EventArgs e)
        {
            txtDated.BackColor = Color.LightYellow;
        }
        datetime defaultdateformate = new datetime();
        private void txtDated_Leave(object sender, EventArgs e)
        {
            try
            {
                txtDated.BackColor = Color.White;
                //if (txtDated.Text != "")
                //{
                //    //   d.convertdate(txtDated.Text,"dd-MM-yyyy","dd-MM-yyyy",'-');
                //    string da = defaultdateformate.convertdate(txtDated.Text, "dd-MM-yyyy", "dd-MM-yyyy", '-');
                //    //   string s = DateTime.ParseExact(txtDated.Text, "dd/MM/yyyy",CultureInfo.InvariantCulture ).ToString("dd/MM/yyyy");

                //    //  string fy1 = defaultdateformate.convertdate(Convert.ToString(txtDated.Text), "dd-MM-yyyy", "dd-MM-yyyy", '-');
                //    //string fy = DateTime.Parse(txtDated.Text).ToString("dd-MM-yyyy");
                //    txtDated.Text = da;
                //}
            }
            catch
            {
                txtDated.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }
        }

        private void txtExpences_Enter(object sender, EventArgs e)
        {
            txtExpences.BackColor = Color.LightYellow;
        }

        private void txtExpences_Leave(object sender, EventArgs e)
        {
            txtExpences.BackColor = Color.White;
        }

        private void txtTotalAmount_Enter(object sender, EventArgs e)
        {
            txtTotalAmount.BackColor = Color.LightYellow;
        }

        private void txtTotalAmount_Leave(object sender, EventArgs e)
        {
            txtTotalAmount.BackColor = Color.White;
        }
        public static string s;
        private void cmbBankSelect_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbBankSelect.Items.Count; i++)
                {
                    s = cmbBankSelect.GetItemText(cmbBankSelect.Items[i]);
                    if (s == cmbBankSelect.Text)
                    {
                        inList = true;
                        cmbBankSelect.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbBankSelect.Text = "";
                }

                cmbEntry.Focus();
            }
            if (e.KeyCode == Keys.F3)
            {
                var privouscontroal = cmbBankSelect;
                activecontroal = privouscontroal.Name;
                Accountentry client = new Accountentry(this, master, tabControl, activecontroal);

                client.Passed(1);
                //   client.Show();
                master.AddNewTab(client);
            }
            if (e.KeyCode == Keys.F2)
            {
                var privouscontroal = cmbBankSelect;
                activecontroal = privouscontroal.Name;
                string iid = cmbBankSelect.SelectedValue.ToString();

                Accountentry client = new Accountentry(this, master, tabControl, activecontroal);
                client.Update(1, iid);
                client.Passed(1);
                //  client.Show();
                master.AddNewTab(client);
            }
        }

        private void cmbBankSelect_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbBankSelect.SelectedIndex = 0;
                cmbBankSelect.DroppedDown = true;
            }
            catch
            {
            }
        }

        private void cmbEntry_Enter(object sender, EventArgs e)
        {

            cmbEntry.SelectedIndex = 0;
            cmbEntry.DroppedDown = true;
        }



        private void TxtRundate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbBankSelect.Focus();
            }
        }

        private void cmbEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbEntry.Items.Count; i++)
                {
                    s = cmbEntry.GetItemText(cmbEntry.Items[i]);
                    if (s == cmbEntry.Text)
                    {
                        inList = true;
                        cmbEntry.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbEntry.Text = "";
                }


                cmbPartyName.Focus();
                if (cmbEntry.SelectedIndex == 0)
                {
                    lblcheque.Visible = true;
                    lbldated.Visible = true;
                    txtChequeNo.Visible = true;
                    txtDated.Visible = true;
                    lblexpe.Visible = false;
                    txtExpences.Visible = false;
                    lbltotalamt.Visible = false;
                    txtTotalAmount.Visible = false;
                    txtExpences.Text = "0";
                    lbldate.Visible = true;
                }
                else if (cmbEntry.SelectedIndex == 1)
                {
                    lblcheque.Visible = true;
                    lbldated.Visible = true;
                    txtChequeNo.Visible = true;
                    txtDated.Visible = true;
                    lbldate.Visible = true;
                    lblexpe.Visible = true;
                    txtExpences.Visible = true;
                    lbltotalamt.Visible = true;
                    txtTotalAmount.Visible = true;
                    txtExpences.Text = "0";
                }
                else if (cmbEntry.SelectedIndex == 2)
                {
                    lblcheque.Visible = true;
                    lbldated.Visible = true;
                    txtChequeNo.Visible = true;
                    txtDated.Visible = true;
                    lbldate.Visible = true;
                    lblexpe.Visible = false;
                    txtExpences.Visible = false;
                    lbltotalamt.Visible = false;
                    txtTotalAmount.Visible = false;
                    txtExpences.Text = "0";
                }
                else if (cmbEntry.SelectedIndex == 3)
                {
                    lblcheque.Visible = false;
                    lbldated.Visible = false;
                    txtChequeNo.Visible = false;
                    txtDated.Visible = false;
                    lbldate.Visible = false;
                    lblexpe.Visible = false;
                    txtExpences.Visible = false;
                    lbltotalamt.Visible = false;
                    txtTotalAmount.Visible = false;
                    txtExpences.Text = "0";

                    //cmbEntry.SelectedIndex = 0;
                    // cmbEntry.DroppedDown = false;
                    cmbPartyName.SelectedValue = 101;
                    cmbPartyName.Text = "CASH";
                }
                else if (cmbEntry.SelectedIndex == 4)
                {
                    lblcheque.Visible = true;
                    lbldated.Visible = true;
                    txtChequeNo.Visible = true;
                    txtDated.Visible = true;
                    lbldate.Visible = true;
                    lblexpe.Visible = false;
                    txtExpences.Visible = false;
                    lbltotalamt.Visible = false;
                    txtTotalAmount.Visible = false;
                    txtExpences.Text = "0";
                    cmbPartyName.SelectedValue = 101;
                    cmbPartyName.Text = "CASH";
                }
                else if (cmbEntry.SelectedIndex == 5)
                {
                    lblcheque.Visible = false;
                    lbldated.Visible = false;
                    txtChequeNo.Visible = false;
                    txtDated.Visible = false;
                    lbldate.Visible = false;
                    lblexpe.Visible = false;
                    txtExpences.Visible = false;
                    lbltotalamt.Visible = false;
                    txtTotalAmount.Visible = false;
                    txtExpences.Text = "0";
                }
                else if (cmbEntry.SelectedIndex == 6)
                {
                    lblcheque.Visible = false;
                    lbldated.Visible = false;
                    txtChequeNo.Visible = false;
                    txtDated.Visible = false;
                    lbldate.Visible = false;
                    lblexpe.Visible = true;
                    txtExpences.Visible = true;
                    lbltotalamt.Visible = true;
                    txtTotalAmount.Visible = true;
                    txtExpences.Text = "0";
                }
            }
        }

        private void cmbPartyName_Enter(object sender, EventArgs e)
        {
            if (cmbEntry.SelectedIndex != 3 && cmbEntry.SelectedIndex != 4)
            {
                cmbPartyName.SelectedIndex = 0;
                cmbPartyName.DroppedDown = true;
            }
        }

        private void cmbRemark_Enter(object sender, EventArgs e)
        {
            cmbRemark.BackColor = Color.LightYellow;
        }

        private void cmbRemark_Leave(object sender, EventArgs e)
        {
            cmbRemark.BackColor = Color.White;
        }

        private void cmbPartyName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbPartyName.Items.Count; i++)
                {
                    s = cmbPartyName.GetItemText(cmbPartyName.Items[i]);
                    if (s == cmbPartyName.Text)
                    {
                        inList = true;
                        cmbPartyName.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbPartyName.Text = "";
                }



                lvserial.Items.Clear();
                txtAmount.Focus();
                if (cmbEntry.Text == "Cheque Issued")
                {
                    lvserial.Columns[6].Width = 0;
                    lvserial.Columns[7].Width = 0;
                }
                else if (cmbEntry.Text == "Draft Issued")
                {
                    lvserial.Columns[6].Width = 0;
                    lvserial.Columns[7].Width = 0;
                }
                else if (cmbEntry.Text == "Cheque/Draft/Rtgs Received")
                {
                    lvserial.Columns[3].Width = 0;
                    lvserial.Columns[4].Width = 0;
                    lvserial.Columns[6].Width = 0;
                    lvserial.Columns[7].Width = 0;
                }
                else if (cmbEntry.Text == "Deposit Cash Into Bank")
                {
                    lvserial.Columns[6].Width = 0;
                    lvserial.Columns[7].Width = 0;
                }
                else if (cmbEntry.Text == "Withdraw Cash from Bank")
                {
                    lvserial.Columns[3].Width = 0;
                    lvserial.Columns[4].Width = 0;
                    lvserial.Columns[6].Width = 0;
                    lvserial.Columns[7].Width = 0;
                }
                else if (cmbEntry.Text == "Bank Expenses")
                {
                    lvserial.Columns[3].Width = 0;
                    lvserial.Columns[4].Width = 0;
                }
                else if (cmbEntry.Text == "Online Transfer")
                {
                    lvserial.Columns[3].Width = 0;
                    lvserial.Columns[4].Width = 0;
                }
                DataTable dt = conn.getdataset("select * from Voucher where PartyName='" + cmbPartyName.Text + "' and PaymentTerms='" + cmbEntry.Text + "' and Date='" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "' and isactive=1 and Transactiontype='Bank Entry'");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListViewItem li;
                    li = lvserial.Items.Add("");
                    li.SubItems.Add(dt.Rows[i]["VchNo"].ToString());
                    li.SubItems.Add(dt.Rows[i]["PartyName"].ToString());
                    li.SubItems.Add(dt.Rows[i]["ChequeNo"].ToString());
                    li.SubItems.Add(dt.Rows[i]["ChequeDate"].ToString());
                    if (cmbEntry.SelectedIndex == 1 || cmbEntry.SelectedIndex == 6)
                    {
                        li.SubItems.Add(dt.Rows[i]["TotalAmount"].ToString());
                    }
                    else
                    {
                        li.SubItems.Add(dt.Rows[i]["BasicAmount"].ToString());
                    }
                    li.SubItems.Add(dt.Rows[i]["OT2"].ToString());
                    li.SubItems.Add(dt.Rows[i]["TotalAmount"].ToString());
                    li.SubItems.Add(dt.Rows[i]["OT8"].ToString());
                }
            }
            if (e.KeyCode == Keys.F3)
            {
                var privouscontroal = cmbPartyName;
                activecontroal = privouscontroal.Name;
                Accountentry client = new Accountentry(this, master, tabControl, activecontroal);

                client.Passed(1);
                //   client.Show();
                master.AddNewTab(client);
            }
            if (e.KeyCode == Keys.F2)
            {
                var privouscontroal = cmbPartyName;
                activecontroal = privouscontroal.Name;
                string iid = cmbPartyName.SelectedValue.ToString();

                Accountentry client = new Accountentry(this, master, tabControl, activecontroal);
                client.Update(1, iid);
                client.Passed(1);
                //  client.Show();
                master.AddNewTab(client);
            }
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtAmount.Text))
                {
                    if (txtAmount.Text != "0")
                    {
                        popup();
                        if (txtExpences.Visible == true)
                        {
                            txtExpences.Focus();
                        }
                        else if (txtChequeNo.Visible == true)
                        {
                            txtChequeNo.Focus();
                        }
                        else
                        {
                            cmbRemark.Focus();
                        }
                    }
                }
            }
        }

        private void txtExpences_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtTotalAmount.Focus();
            }
        }

        private void txtTotalAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtChequeNo.Visible == true)
                {
                    txtChequeNo.Focus();
                }
                else
                {
                    cmbRemark.Focus();
                }
            }
        }

        private void txtChequeNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDated.Focus();
            }
        }

        private void txtDated_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                cmbRemark.Focus();
            }
        }

        private void cmbRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSubmit.Focus();
            }
        }
        public void voucherno()
        {
            //vono = conn.ExecuteScalar("select max(Voucherid) as Voucherid from Ledger where isactive=1 and OT7='Bank Entry'");
            vono = conn.ExecuteScalar("select max(VchNo) as Voucherid from Voucher where isactive=1 and TransactionType='Bank Entry'");
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
        }
        public void enterdata()
        {
            try
            {
                if (BtnSubmit.Text == "Update")
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[20]["u"].ToString() == "True")
                        {
                            DataTable dt = conn.getdataset("select DC from Ledger where isactive=1 and [VoucherID]='" + lblvno.Text + "' and OT7='Bank Entry'");
                            if (dt.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (cmbEntry.Text == "Cheque/Draft/Rtgs Received")
                                    {

                                        if (dt.Rows[i]["DC"].ToString() == "D")
                                        {
                                            conn.execute("UPDATE [dbo].[Ledger] SET [AccountID]='" + cmbBankSelect.SelectedValue + "',[AccountName]='" + cmbBankSelect.Text + "'[OT1]='Credit',[OT8]='" + cmbPartyName.Text + "', [Amount]='" + txtAmount.Text + "',Date1='" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',[OT2]='" + txtExpences.Text + "',[OT3]='" + txtTotalAmount.Text + "',[OT4]='" + txtChequeNo.Text + "',[OT5]='" + txtDated.Text + "',[OT6]='" + cmbRemark.Text + "' where [VoucherID]='" + lblvno.Text + "' and [DC]='" + "D" + "' and OT7='Bank Entry'");
                                        }
                                        else
                                        {
                                            conn.execute("UPDATE [dbo].[Ledger] SET [AccountID]='" + cmbPartyName.SelectedValue + "',[AccountName]='" + cmbPartyName.Text + "',[OT1]='Credit',[OT8]='" + cmbBankSelect.Text + "', [Amount]='" + txtAmount.Text + "',Date1='" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',[OT2]='" + txtExpences.Text + "',[OT3]='" + txtTotalAmount.Text + "',[OT4]='" + txtChequeNo.Text + "',[OT5]='" + txtDated.Text + "',[OT6]='" + cmbRemark.Text + "' where [VoucherID]='" + lblvno.Text + "' and [DC]='" + "C" + "' and OT7='Bank Entry'");
                                        }
                                    }
                                    else if (cmbEntry.Text == "Deposit Cash Into Bank")
                                    {
                                        if (dt.Rows[i]["DC"].ToString() == "D")
                                        {
                                            conn.execute("UPDATE [dbo].[Ledger] SET [AccountID]='" + cmbBankSelect.SelectedValue + "',[AccountName]='" + cmbBankSelect.Text + "',[OT1]='Cash',[OT8]='" + cmbPartyName.Text + "', [Amount]='" + txtAmount.Text + "',Date1='" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',[OT2]='" + txtExpences.Text + "',[OT3]='" + txtTotalAmount.Text + "',[OT4]='" + txtChequeNo.Text + "',[OT5]='" + txtDated.Text + "',[OT6]='" + cmbRemark.Text + "' where [VoucherID]='" + lblvno.Text + "' and [DC]='" + "D" + "' and OT7='Bank Entry'");
                                        }
                                        else
                                        {
                                            conn.execute("UPDATE [dbo].[Ledger] SET [AccountID]='" + cmbPartyName.SelectedValue + "',[AccountName]='" + cmbPartyName.Text + "',[OT1]='Cash',[OT8]='" + cmbBankSelect.Text + "', [Amount]='" + txtAmount.Text + "',Date1='" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',[OT2]='" + txtExpences.Text + "',[OT3]='" + txtTotalAmount.Text + "',[OT4]='" + txtChequeNo.Text + "',[OT5]='" + txtDated.Text + "',[OT6]='" + cmbRemark.Text + "' where [VoucherID]='" + lblvno.Text + "' and [DC]='" + "C" + "' and OT7='Bank Entry'");
                                        }
                                    }
                                    else if (cmbEntry.Text == "Withdraw Cash from Bank")
                                    {
                                        if (dt.Rows[i]["DC"].ToString() == "D")
                                        {
                                            conn.execute("UPDATE [dbo].[Ledger] SET [AccountID]='" + cmbPartyName.SelectedValue + "',[AccountName]='" + cmbPartyName.Text + "',[OT1]='Cash',[OT8]='" + cmbBankSelect.Text + "', [Amount]='" + txtAmount.Text + "',Date1='" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',[OT2]='" + txtExpences.Text + "',[OT3]='" + txtTotalAmount.Text + "',[OT4]='" + txtChequeNo.Text + "',[OT5]='" + txtDated.Text + "',[OT6]='" + cmbRemark.Text + "' where [VoucherID]='" + lblvno.Text + "' and [DC]='" + "D" + "' and OT7='Bank Entry'");
                                        }
                                        else
                                        {
                                            conn.execute("UPDATE [dbo].[Ledger] SET [AccountID]='" + cmbBankSelect.SelectedValue + "',[AccountName]='" + cmbBankSelect.Text + "',[OT1]='Cash',[OT8]='" + cmbPartyName.Text + "', [Amount]='" + txtAmount.Text + "',Date1='" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',[OT2]='" + txtExpences.Text + "',[OT3]='" + txtTotalAmount.Text + "',[OT4]='" + txtChequeNo.Text + "',[OT5]='" + txtDated.Text + "',[OT6]='" + cmbRemark.Text + "' where [VoucherID]='" + lblvno.Text + "' and [DC]='" + "C" + "' and OT7='Bank Entry'");
                                        }
                                    }
                                    else
                                    {
                                        if (dt.Rows[i]["DC"].ToString() == "D")
                                        {
                                            conn.execute("UPDATE [dbo].[Ledger] SET [AccountID]='" + cmbPartyName.SelectedValue + "',[AccountName]='" + cmbPartyName.Text + "',[OT1]='Credit',[OT8]='" + cmbBankSelect.Text + "', [Amount]='" + txtAmount.Text + "',Date1='" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',[OT2]='" + txtExpences.Text + "',[OT3]='" + txtTotalAmount.Text + "',[OT4]='" + txtChequeNo.Text + "',[OT5]='" + txtDated.Text + "',[OT6]='" + cmbRemark.Text + "' where [VoucherID]='" + lblvno.Text + "' and [DC]='" + "D" + "' and OT7='Bank Entry'");
                                        }
                                        else
                                        {
                                            conn.execute("UPDATE [dbo].[Ledger] SET [AccountID]='" + cmbBankSelect.SelectedValue + "',[AccountName]='" + cmbBankSelect.Text + "',[OT1]='Credit',[OT8]='" + cmbPartyName.Text + "', [Amount]='" + txtAmount.Text + "',Date1='" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',[OT2]='" + txtExpences.Text + "',[OT3]='" + txtTotalAmount.Text + "',[OT4]='" + txtChequeNo.Text + "',[OT5]='" + txtDated.Text + "',[OT6]='" + cmbRemark.Text + "' where [VoucherID]='" + lblvno.Text + "' and [DC]='" + "C" + "' and OT7='Bank Entry'");
                                        }
                                    }
                                }
                            }
                            conn.execute("UPDATE [dbo].[Voucher] SET [AccountID]='" + cmbBankSelect.SelectedValue + "',[AccountName]='" + cmbBankSelect.Text + "', [PartyID]='" + cmbPartyName.SelectedValue + "',[PartyName]='" + cmbPartyName.Text + "', [Date]='" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',[TotalAmount]='" + txtTotalAmount.Text + "',[ChequeNo]='" + txtChequeNo.Text + "',ChequeDate='" + txtDated.Text + "',[BasicAmount]='" + txtAmount.Text + "',[OT1]='" + cmbRemark.Text + "',[OT2]='" + txtExpences.Text + "' where [VchNo]='" + lblvno.Text + "' and Transactiontype='Bank Entry'");
                            conn.execute("UPDATE [dbo].[Ref] SET isactive=0 where [VchNo]='" + lblvno.Text + "' and TransactionType='Bank Entry'");
                            for (int i = 0; i < dtselectedrow.Rows.Count; i++)
                            {
                                //if (Convert.ToDouble(dtselectedrow.Rows[i]["BalAmount"].ToString()) <= 0)
                                //{
                                //    conn.execute("Update billmaster set OrderStatus='" + "Clear" + "' where billno='" + dtselectedrow.Rows[i]["BillNo"].ToString() + "'");
                                //}
                                conn.execute("INSERT INTO [dbo].[Ref]([VchNo],[TransactionType],[AccountID],[AccountName],[Method],[RefAmount],[Date1],[isactive],[OT1],[OT2],[OT3],[OT4],[OT5],[OT6]) values ('" + strvono + "','" + "Bank Entry" + "','" + cmbPartyName.SelectedValue + "','" + cmbPartyName.Text + "','" + cmbEntry.Text + "','" + txtTotalAmount.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',1,'" + dtselectedrow.Rows[i]["Date"].ToString() + "','" + dtselectedrow.Rows[i]["BillNO"].ToString() + "','" + dtselectedrow.Rows[i]["BillAmount"].ToString() + "','" + dtselectedrow.Rows[i]["ReceivedAmount"].ToString() + "','" + dtselectedrow.Rows[i]["BalAmount"].ToString() + "','" + dtselectedrow.Rows[i]["Status"].ToString() + "')");
                            }

                            //[Method],[RefAmount],[Date1]
                            BtnSubmit.Text = "Submit";
                            // lvserial.Items.Clear();
                            if (rowid >= 0)
                            {
                                lvserial.Items[rowid].SubItems[0].Text = "";
                                lvserial.Items[rowid].SubItems[1].Text = lblvno.Text;
                                lvserial.Items[rowid].SubItems[2].Text = cmbPartyName.Text;
                                lvserial.Items[rowid].SubItems[3].Text = txtChequeNo.Text;
                                lvserial.Items[rowid].SubItems[4].Text = txtDated.Text;
                                lvserial.Items[rowid].SubItems[5].Text = txtAmount.Text;
                                lvserial.Items[rowid].SubItems[6].Text = txtExpences.Text;
                                lvserial.Items[rowid].SubItems[7].Text = txtTotalAmount.Text;
                                lvserial.Items[rowid].SubItems[8].Text = cmbRemark.Text;
                                rowid = -1;
                                //ListViewItem li;
                                //li = lvserial.Items.Add("");
                                //li.SubItems.Add(strvono);
                                //li.SubItems.Add(cmbPartyName.Text);
                                //li.SubItems.Add(txtChequeNo.Text);
                                //li.SubItems.Add(Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate));
                                //li.SubItems.Add(txtAmount.Text);
                                //li.SubItems.Add(txtExpences.Text);
                                //li.SubItems.Add(txtTotalAmount.Text);
                                //li.SubItems.Add(cmbRemark.Text);
                            }
                            else
                            {
                                lvserial.Items.Clear();
                                ListViewItem li;
                                li = lvserial.Items.Add("");
                                li.SubItems.Add(lblvno.Text);
                                li.SubItems.Add(cmbPartyName.Text);
                                li.SubItems.Add(txtChequeNo.Text);
                                li.SubItems.Add(txtDated.Text);
                                li.SubItems.Add(txtAmount.Text);
                                li.SubItems.Add(txtExpences.Text);
                                li.SubItems.Add(txtTotalAmount.Text);
                                li.SubItems.Add(cmbRemark.Text);
                            }
                            cleartextbox();
                        }
                        else
                        {
                            MessageBox.Show("You don't have Permission To Update");
                            return;
                        }
                    }
                }
                else
                {
                    voucherno();

                    //   if (cmbEntry.Text == "Cheque/Draft/Rtgs Received")
                    // {
                    //   conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OT2],[OT3],[OT4],[OT5],[OT6],[OT7]) values ('" + strvono + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + cmbEntry.Text + "','" + cmbPartyName.SelectedValue + "','" + cmbPartyName.Text + "','" + Math.Round(Convert.ToDouble(txtAmount.Text), 2).ToString("########.00") + "','" + "C" + "',1,'" + cmbBankSelect.Text + "','" + txtExpences.Text + "','" + txtTotalAmount.Text + "','" + txtChequeNo.Text + "','" + txtDated.Text + "','" + cmbRemark.Text + "','" + "Bank Entry" + "')");
                    // }
                    //if (cmbPartyName.Text == "Cash")
                    //{
                    //    conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OT2],[OT3],[OT4],[OT5],[OT6],[OT7],[OT8]) values ('" + strvono + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + cmbEntry.Text + "','" + cmbPartyName.SelectedValue + "','" + cmbPartyName.Text + "','" + Math.Round(Convert.ToDouble(txtAmount.Text), 2).ToString("########.00") + "','" + "C" + "',1,'Cash','" + txtExpences.Text + "','" + txtTotalAmount.Text + "','" + txtChequeNo.Text + "','" + txtDated.Text + "','" + cmbRemark.Text + "','" + "Bank Entry" + "','" + cmbBankSelect.Text + "')");
                    //    conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OT2],[OT3],[OT4],[OT5],[OT6],[OT7],[OT8]) values ('" + strvono + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + cmbEntry.Text + "','" + cmbBankSelect.SelectedValue + "','" + cmbBankSelect.Text + "','" + Math.Round(Convert.ToDouble(txtAmount.Text), 2).ToString("########.00") + "','" + "D" + "',1,'Credit','" + txtExpences.Text + "','" + txtTotalAmount.Text + "','" + txtChequeNo.Text + "','" + txtDated.Text + "','" + cmbRemark.Text + "','" + "Bank Entry" + "','" + cmbPartyName.Text + "')");

                    //}
                    if (cmbEntry.Text == "Cheque Issued")
                    {
                        conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OT2],[OT3],[OT4],[OT5],[OT6],[OT7],[OT8]) values ('" + strvono + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + cmbEntry.Text + "','" + cmbPartyName.SelectedValue + "','" + cmbPartyName.Text + "','" + Math.Round(Convert.ToDouble(txtAmount.Text), 2).ToString("########.00") + "','" + "D" + "',1,'Credit','" + txtExpences.Text + "','" + txtTotalAmount.Text + "','" + txtChequeNo.Text + "','" + txtDated.Text + "','" + cmbRemark.Text + "','" + "Bank Entry" + "','" + cmbBankSelect.Text + "')");
                        conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OT2],[OT3],[OT4],[OT5],[OT6],[OT7],[OT8]) values ('" + strvono + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + cmbEntry.Text + "','" + cmbBankSelect.SelectedValue + "','" + cmbBankSelect.Text + "','" + Math.Round(Convert.ToDouble(txtAmount.Text), 2).ToString("########.00") + "','" + "C" + "',1,'Credit','" + txtExpences.Text + "','" + txtTotalAmount.Text + "','" + txtChequeNo.Text + "','" + txtDated.Text + "','" + cmbRemark.Text + "','" + "Bank Entry" + "','" + cmbPartyName.Text + "')");
                    }
                    else if (cmbEntry.Text == "Draft Issued")
                    {
                        conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OT2],[OT3],[OT4],[OT5],[OT6],[OT7],[OT8]) values ('" + strvono + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + cmbEntry.Text + "','" + cmbPartyName.SelectedValue + "','" + cmbPartyName.Text + "','" + Math.Round(Convert.ToDouble(txtAmount.Text), 2).ToString("########.00") + "','" + "D" + "',1,'Credit','" + txtExpences.Text + "','" + txtTotalAmount.Text + "','" + txtChequeNo.Text + "','" + txtDated.Text + "','" + cmbRemark.Text + "','" + "Bank Entry" + "','" + cmbBankSelect.Text + "')");
                        conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OT2],[OT3],[OT4],[OT5],[OT6],[OT7],[OT8]) values ('" + strvono + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + cmbEntry.Text + "','" + cmbBankSelect.SelectedValue + "','" + cmbBankSelect.Text + "','" + Math.Round(Convert.ToDouble(txtAmount.Text), 2).ToString("########.00") + "','" + "C" + "',1,'Credit','" + txtExpences.Text + "','" + txtTotalAmount.Text + "','" + txtChequeNo.Text + "','" + txtDated.Text + "','" + cmbRemark.Text + "','" + "Bank Entry" + "','" + cmbPartyName.Text + "')");
                    }
                    else if (cmbEntry.Text == "Withdraw Cash from Bank")
                    {
                        conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OT2],[OT3],[OT4],[OT5],[OT6],[OT7],[OT8]) values ('" + strvono + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + cmbEntry.Text + "','" + cmbPartyName.SelectedValue + "','" + cmbPartyName.Text + "','" + Math.Round(Convert.ToDouble(txtAmount.Text), 2).ToString("########.00") + "','" + "D" + "',1,'Credit','" + txtExpences.Text + "','" + txtTotalAmount.Text + "','" + txtChequeNo.Text + "','" + txtDated.Text + "','" + cmbRemark.Text + "','" + "Bank Entry" + "','" + cmbBankSelect.Text + "')");
                        conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OT2],[OT3],[OT4],[OT5],[OT6],[OT7],[OT8]) values ('" + strvono + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + cmbEntry.Text + "','" + cmbBankSelect.SelectedValue + "','" + cmbBankSelect.Text + "','" + Math.Round(Convert.ToDouble(txtAmount.Text), 2).ToString("########.00") + "','" + "C" + "',1,'Cash','" + txtExpences.Text + "','" + txtTotalAmount.Text + "','" + txtChequeNo.Text + "','" + txtDated.Text + "','" + cmbRemark.Text + "','" + "Bank Entry" + "','" + cmbPartyName.Text + "')");
                    }
                    else if (cmbEntry.Text == "Bank Expenses")
                    {
                        conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OT2],[OT3],[OT4],[OT5],[OT6],[OT7],[OT8]) values ('" + strvono + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + cmbEntry.Text + "','" + cmbPartyName.SelectedValue + "','" + cmbPartyName.Text + "','" + Math.Round(Convert.ToDouble(txtAmount.Text), 2).ToString("########.00") + "','" + "D" + "',1,'Credit','" + txtExpences.Text + "','" + txtTotalAmount.Text + "','" + txtChequeNo.Text + "','" + txtDated.Text + "','" + cmbRemark.Text + "','" + "Bank Entry" + "','" + cmbBankSelect.Text + "')");
                        conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OT2],[OT3],[OT4],[OT5],[OT6],[OT7],[OT8]) values ('" + strvono + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + cmbEntry.Text + "','" + cmbBankSelect.SelectedValue + "','" + cmbBankSelect.Text + "','" + Math.Round(Convert.ToDouble(txtAmount.Text), 2).ToString("########.00") + "','" + "C" + "',1,'Credit','" + txtExpences.Text + "','" + txtTotalAmount.Text + "','" + txtChequeNo.Text + "','" + txtDated.Text + "','" + cmbRemark.Text + "','" + "Bank Entry" + "','" + cmbPartyName.Text + "')");
                    }
                    else if (cmbEntry.Text == "Online Transfer")
                    {
                        conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OT2],[OT3],[OT4],[OT5],[OT6],[OT7],[OT8]) values ('" + strvono + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + cmbEntry.Text + "','" + cmbPartyName.SelectedValue + "','" + cmbPartyName.Text + "','" + Math.Round(Convert.ToDouble(txtAmount.Text), 2).ToString("########.00") + "','" + "D" + "',1,'Credit','" + txtExpences.Text + "','" + txtTotalAmount.Text + "','" + txtChequeNo.Text + "','" + txtDated.Text + "','" + cmbRemark.Text + "','" + "Bank Entry" + "','" + cmbBankSelect.Text + "')");
                        conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OT2],[OT3],[OT4],[OT5],[OT6],[OT7],[OT8]) values ('" + strvono + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + cmbEntry.Text + "','" + cmbBankSelect.SelectedValue + "','" + cmbBankSelect.Text + "','" + Math.Round(Convert.ToDouble(txtAmount.Text), 2).ToString("########.00") + "','" + "C" + "',1,'Credit','" + txtExpences.Text + "','" + txtTotalAmount.Text + "','" + txtChequeNo.Text + "','" + txtDated.Text + "','" + cmbRemark.Text + "','" + "Bank Entry" + "','" + cmbPartyName.Text + "')");
                    }
                    else if (cmbEntry.Text == "Cheque/Draft/Rtgs Received")
                    {
                        conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OT2],[OT3],[OT4],[OT5],[OT6],[OT7],[OT8]) values ('" + strvono + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + cmbEntry.Text + "','" + cmbPartyName.SelectedValue + "','" + cmbPartyName.Text + "','" + Math.Round(Convert.ToDouble(txtAmount.Text), 2).ToString("########.00") + "','" + "C" + "',1,'Credit','" + txtExpences.Text + "','" + txtTotalAmount.Text + "','" + txtChequeNo.Text + "','" + txtDated.Text + "','" + cmbRemark.Text + "','" + "Bank Entry" + "','" + cmbBankSelect.Text + "')");
                        conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OT2],[OT3],[OT4],[OT5],[OT6],[OT7],[OT8]) values ('" + strvono + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + cmbEntry.Text + "','" + cmbBankSelect.SelectedValue + "','" + cmbBankSelect.Text + "','" + Math.Round(Convert.ToDouble(txtAmount.Text), 2).ToString("########.00") + "','" + "D" + "',1,'Credit','" + txtExpences.Text + "','" + txtTotalAmount.Text + "','" + txtChequeNo.Text + "','" + txtDated.Text + "','" + cmbRemark.Text + "','" + "Bank Entry" + "','" + cmbPartyName.Text + "')");
                    }
                    else if (cmbEntry.Text == "Deposit Cash Into Bank")
                    {
                        conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OT2],[OT3],[OT4],[OT5],[OT6],[OT7],[OT8]) values ('" + strvono + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + cmbEntry.Text + "','" + cmbPartyName.SelectedValue + "','" + cmbPartyName.Text + "','" + Math.Round(Convert.ToDouble(txtAmount.Text), 2).ToString("########.00") + "','" + "C" + "',1,'Cash','" + txtExpences.Text + "','" + txtTotalAmount.Text + "','" + txtChequeNo.Text + "','" + txtDated.Text + "','" + cmbRemark.Text + "','" + "Bank Entry" + "','" + cmbBankSelect.Text + "')");
                        conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OT2],[OT3],[OT4],[OT5],[OT6],[OT7],[OT8]) values ('" + strvono + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + cmbEntry.Text + "','" + cmbBankSelect.SelectedValue + "','" + cmbBankSelect.Text + "','" + Math.Round(Convert.ToDouble(txtAmount.Text), 2).ToString("########.00") + "','" + "D" + "',1,'Credit','" + txtExpences.Text + "','" + txtTotalAmount.Text + "','" + txtChequeNo.Text + "','" + txtDated.Text + "','" + cmbRemark.Text + "','" + "Bank Entry" + "','" + cmbPartyName.Text + "')");
                    }
                    conn.execute("INSERT INTO [dbo].[Voucher]([TransactionType],[VchNo],[Date],[PaymentTerms],[TotalAmount],[ChequeNo],[ChequeDate],[AccountID],[AccountName],[PartyID],[PartyName],[BasicAmount],[OT1],[Ot2],[isactive]) values ('" + "Bank Entry" + "','" + strvono + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + cmbEntry.Text + "','" + txtTotalAmount.Text + "','" + txtChequeNo.Text + "','" + txtDated.Text + "','" + cmbBankSelect.SelectedValue + "','" + cmbBankSelect.Text + "','" + cmbPartyName.SelectedValue + "','" + cmbPartyName.Text + "','" + txtAmount.Text + "','" + cmbRemark.Text + "','" + txtExpences.Text + "','" + "1" + "')");
                    for (int i = 0; i < dtselectedrow.Rows.Count; i++)
                    {
                        //if (Convert.ToDouble(dtselectedrow.Rows[i]["BalAmount"].ToString()) <= 0)
                        //{
                        //    conn.execute("Update billmaster set OrderStatus='" + "Clear" + "' where billno='" + dtselectedrow.Rows[i]["BillNo"].ToString() + "'");
                        //}
                        conn.execute("INSERT INTO [dbo].[Ref]([VchNo],[TransactionType],[AccountID],[AccountName],[Method],[RefAmount],[Date1],[isactive],[OT1],[OT2],[OT3],[OT4],[OT5],[OT6]) values ('" + strvono + "','" + "Bank Entry" + "','" + cmbPartyName.SelectedValue + "','" + cmbPartyName.Text + "','" + cmbEntry.Text + "','" + txtTotalAmount.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',1,'" + dtselectedrow.Rows[i]["Date"].ToString() + "','" + dtselectedrow.Rows[i]["BillNO"].ToString() + "','" + dtselectedrow.Rows[i]["BillAmount"].ToString() + "','" + dtselectedrow.Rows[i]["ReceivedAmount"].ToString() + "','" + dtselectedrow.Rows[i]["BalAmount"].ToString() + "','" + dtselectedrow.Rows[i]["Status"].ToString() + "')");
                    }


                    //  DataTable dt = conn.getdataset("select * from Voucher where PartyName='"+cmbPartyName.Text+"' and PaymentTerms='"+cmbEntry.Text+"'");
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    ListViewItem li;
                    li = lvserial.Items.Add("");
                    li.SubItems.Add(strvono);
                    li.SubItems.Add(cmbPartyName.Text);
                    li.SubItems.Add(txtChequeNo.Text);
                    li.SubItems.Add(txtDated.Text);
                    li.SubItems.Add(txtAmount.Text);
                    li.SubItems.Add(txtExpences.Text);
                    li.SubItems.Add(txtTotalAmount.Text);
                    li.SubItems.Add(cmbRemark.Text);
                    // }
                    clearall();
                }

            }
            catch
            {
            }
        }
        public void clearall()
        {
            //  cmbBankSelect.SelectedIndex = -1;
            // cmbEntry.SelectedIndex = -1;
            cmbPartyName.SelectedIndex = -1;
            txtAmount.Text = "";
            txtExpences.Text = "0";
            txtTotalAmount.Text = "";
            txtChequeNo.Text = "";
            txtDated.Text = "";
            cmbRemark.Text = "";
            TxtRundate.Focus();
            this.ActiveControl = TxtRundate;
            //  lvserial.Items.Clear();
        }
        public void cleartextbox()
        {
            txtAmount.Text = "";
            txtExpences.Text = "0";
            txtTotalAmount.Text = "";
            txtChequeNo.Text = "";
            txtDated.Text = "";
            cmbRemark.Text = "";
            TxtRundate.Focus();
            this.ActiveControl = TxtRundate;
        }
        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                enterdata();
            }
            catch
            {
            }
        }
        Int32 rowid = -1;
        public void open()
        {
            try
            {
                if (lvserial.SelectedItems.Count > 0)
                {

                    // ABC = LVclient.Items[LVclient.FocusedItem.Index].SubItems[0].Text;
                    rowid = lvserial.FocusedItem.Index;
                    lblvno.Text = lvserial.Items[lvserial.FocusedItem.Index].SubItems[1].Text;
                    DataTable update = conn.getdataset("select * from Voucher where isactive=1 and VchNo='" + lblvno.Text + "' and Transactiontype='Bank Entry'");
                    string account = update.Rows[0]["AccountName"].ToString();
                    cmbBankSelect.Text = account;
                    cmbEntry.Text = update.Rows[0]["PaymentTerms"].ToString();
                    cmbPartyName.Text = lvserial.Items[lvserial.FocusedItem.Index].SubItems[2].Text;
                    txtChequeNo.Text = lvserial.Items[lvserial.FocusedItem.Index].SubItems[3].Text;
                    txtDated.Text = lvserial.Items[lvserial.FocusedItem.Index].SubItems[4].Text;
                    txtAmount.Text = lvserial.Items[lvserial.FocusedItem.Index].SubItems[5].Text;
                    txtExpences.Text = lvserial.Items[lvserial.FocusedItem.Index].SubItems[6].Text;
                    txtTotalAmount.Text = lvserial.Items[lvserial.FocusedItem.Index].SubItems[7].Text;
                    cmbRemark.Text = lvserial.Items[lvserial.FocusedItem.Index].SubItems[8].Text;
                    BtnSubmit.Text = "Update";
                    this.ActiveControl = txtAmount;
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

        private void btncancel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    master.RemoveCurrentTab();
                }
            }
            catch
            {
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvserial.CheckedItems.Count <= 0)
                {
                    MessageBox.Show("Select Entry First");
                }
                else
                {
                    for (int i = 0; i < lvserial.Items.Count; i++)
                    {
                        if (Convert.ToBoolean(lvserial.Items[i].Checked) == true)
                        {
                            conn.execute("Update Ledger set isactive=0 where VoucherID='" + lvserial.Items[i].SubItems[1].Text + "' and OT7='Bank Entry'");
                            conn.execute("Update Voucher set isactive=0 where VchNo='" + lvserial.Items[i].SubItems[1].Text + "' and Transactiontype='Bank Entry'");
                            conn.execute("Update Ref set isactive=0 where VchNo='" + lvserial.Items[i].SubItems[1].Text + "' and Transactiontype='Bank Entry'");
                        }
                    }
                    MessageBox.Show("Delete successfully");
                    clearall();
                    lvserial.Items.Clear();
                }
            }
            catch
            {
            }
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtTotalAmount.Text = txtAmount.Text;
            }
            catch
            {
            }
        }

        private void txtExpences_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Double t = Convert.ToDouble(txtAmount.Text) + Convert.ToDouble(txtExpences.Text);
                txtTotalAmount.Text = Convert.ToString(t);
            }
            catch
            {
            }
        }

        private void cmbBankSelect_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbBankSelect.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbBankSelect.SelectedIndex = index;
            //        }
            //    }


            //}
        }
        string searchstr;
        private void cmbEntry_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbEntry.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbEntry.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void cmbPartyName_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbPartyName.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbPartyName.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                prn.execute("delete from printing");
                DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1");
                if (lvserial.CheckedItems.Count > 0)
                {
                    for (int i = 0; i < lvserial.CheckedItems.Count; i++)
                    {
                        if (Convert.ToBoolean(lvserial.Items[i].Checked) == true)
                        {
                            ChangeNumbersToWords sh = new ChangeNumbersToWords();
                            String s1 = Math.Round(Convert.ToDouble(lvserial.Items[i].SubItems[5].Text), 2).ToString("########.00");
                            string[] words = s1.Split('.');


                            string str = sh.changeToWords(words[0]);
                            string str1 = sh.changeToWords(words[1]);
                            if (str1 == " " || str1 == null || words[1] == "00")
                            {
                                str1 = "Zero ";
                            }
                            String inword = "Rupees " + str + "and " + str1 + "Paise Only";

                            string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23)VALUES";
                            qry += "('" + lvserial.Items[i].SubItems[2].Text + "','" + lvserial.Items[i].SubItems[3].Text + "','" + lvserial.Items[i].SubItems[4].Text + "','" + lvserial.Items[i].SubItems[5].Text + "','" + lvserial.Items[i].SubItems[6].Text + "','" + lvserial.Items[i].SubItems[7].Text + "','" + lvserial.Items[i].SubItems[8].Text + "','" + inword + "','" + TxtRundate.Text + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][2].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + dt1.Rows[0][11].ToString() + "','" + dt1.Rows[0][12].ToString() + "','" + dt1.Rows[0][13].ToString() + "','" + cmbEntry.Text + "')";
                            prn.execute(qry);


                        }
                    }
                    Print popup = new Print("BankVoucher");
                    popup.ShowDialog();
                    popup.Dispose();
                }
                else
                {
                    MessageBox.Show("Please Check Records..");
                }
            }
            catch
            {
            }
        }



        internal void updatemode(string str, string type)
        {
            cnt = 1;
            userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[20]["p"].ToString() == "False")
                {
                    btnPrint.Enabled = false;
                }
                if (userrights.Rows[20]["a"].ToString() == "False")
                {
                    BtnSubmit.Enabled = false;
                }
                if (userrights.Rows[20]["d"].ToString() == "False")
                {
                    btndelete.Enabled = false;
                }
            }
            DataTable update = conn.getdataset("select * from Voucher where isactive=1 and VchNo='" + str + "' and PaymentTerms='" + type + "' and Transactiontype='Bank Entry'");
            loadpage();
            lblvno.Text = update.Rows[0]["VchNo"].ToString();
            TxtRundate.Text = Convert.ToDateTime(update.Rows[0]["Date"].ToString()).ToString(Master.dateformate);
            string account = update.Rows[0]["AccountName"].ToString();
            cmbBankSelect.Text = account;
            cmbEntry.Text = update.Rows[0]["PaymentTerms"].ToString();
            string party = update.Rows[0]["PartyName"].ToString();
            cmbPartyName.Text = party;
            txtAmount.Text = update.Rows[0]["BasicAmount"].ToString();
            txtExpences.Text = update.Rows[0]["OT2"].ToString();
            txtTotalAmount.Text = update.Rows[0]["TotalAmount"].ToString();
            txtChequeNo.Text = update.Rows[0]["ChequeNo"].ToString();
            txtDated.Text = update.Rows[0]["ChequeDate"].ToString();
            cmbRemark.Text = update.Rows[0]["OT8"].ToString();
            #region
            if (cmbEntry.SelectedIndex == 0)
            {
                lblcheque.Visible = true;
                lbldated.Visible = true;
                txtChequeNo.Visible = true;
                txtDated.Visible = true;
                lbldate.Visible = true;
                lblexpe.Visible = false;
                txtExpences.Visible = false;
                lbltotalamt.Visible = false;
                txtTotalAmount.Visible = false;
                // txtExpences.Text = "0";
            }
            else if (cmbEntry.SelectedIndex == 1)
            {
                lblcheque.Visible = true;
                lbldated.Visible = true;
                txtChequeNo.Visible = true;
                txtDated.Visible = true;
                lbldate.Visible = true;
                lblexpe.Visible = true;
                txtExpences.Visible = true;
                lbltotalamt.Visible = true;
                txtTotalAmount.Visible = true;
                //  txtExpences.Text = "0";
            }
            else if (cmbEntry.SelectedIndex == 2)
            {
                lblcheque.Visible = true;
                lbldated.Visible = true;
                txtChequeNo.Visible = true;
                txtDated.Visible = true;
                lbldate.Visible = true;
                lblexpe.Visible = false;
                txtExpences.Visible = false;
                lbltotalamt.Visible = false;
                txtTotalAmount.Visible = false;
                //  txtExpences.Text = "0";
            }
            else if (cmbEntry.SelectedIndex == 3)
            {
                lblcheque.Visible = false;
                lbldated.Visible = false;
                txtChequeNo.Visible = false;
                txtDated.Visible = false;
                lbldate.Visible = false;
                lblexpe.Visible = false;
                txtExpences.Visible = false;
                lbltotalamt.Visible = false;
                txtTotalAmount.Visible = false;
                //   txtExpences.Text = "0";

                //cmbEntry.SelectedIndex = 0;
                // cmbEntry.DroppedDown = false;
                cmbPartyName.SelectedValue = 101;
                cmbPartyName.Text = "CASH";
            }
            else if (cmbEntry.SelectedIndex == 4)
            {
                lblcheque.Visible = true;
                lbldated.Visible = true;
                txtChequeNo.Visible = true;
                txtDated.Visible = true;
                lbldate.Visible = true;
                lblexpe.Visible = false;
                txtExpences.Visible = false;
                lbltotalamt.Visible = false;
                txtTotalAmount.Visible = false;
                //  txtExpences.Text = "0";
                cmbPartyName.SelectedValue = 101;
                cmbPartyName.Text = "CASH";
            }
            else if (cmbEntry.SelectedIndex == 5)
            {
                lblcheque.Visible = false;
                lbldated.Visible = false;
                txtChequeNo.Visible = false;
                txtDated.Visible = false;
                lblexpe.Visible = false;
                lbldate.Visible = false;
                txtExpences.Visible = false;
                lbltotalamt.Visible = false;
                txtTotalAmount.Visible = false;
                //  txtExpences.Text = "0";
            }
            else if (cmbEntry.SelectedIndex == 6)
            {
                lblcheque.Visible = false;
                lbldated.Visible = false;
                txtChequeNo.Visible = false;
                txtDated.Visible = false;
                lbldate.Visible = false;
                lblexpe.Visible = true;
                txtExpences.Visible = true;
                lbltotalamt.Visible = true;
                txtTotalAmount.Visible = true;
                // txtExpences.Text = "0";
            }
            #endregion
            #region
            if (cmbEntry.Text == "Cheque Issued")
            {
                lvserial.Columns[6].Width = 0;
                lvserial.Columns[7].Width = 0;
            }
            else if (cmbEntry.Text == "Draft Issued")
            {
                lvserial.Columns[6].Width = 0;
                lvserial.Columns[7].Width = 0;
            }
            else if (cmbEntry.Text == "Cheque/Draft/Rtgs Received")
            {
                lvserial.Columns[3].Width = 0;
                lvserial.Columns[4].Width = 0;
                lvserial.Columns[6].Width = 0;
                lvserial.Columns[7].Width = 0;
            }
            else if (cmbEntry.Text == "Deposit Cash Into Bank")
            {
                lvserial.Columns[6].Width = 0;
                lvserial.Columns[7].Width = 0;
            }
            else if (cmbEntry.Text == "Withdraw Cash from Bank")
            {
                lvserial.Columns[3].Width = 0;
                lvserial.Columns[4].Width = 0;
                lvserial.Columns[6].Width = 0;
                lvserial.Columns[7].Width = 0;
            }
            else if (cmbEntry.Text == "Bank Expenses")
            {
                lvserial.Columns[3].Width = 0;
                lvserial.Columns[4].Width = 0;
            }
            else if (cmbEntry.Text == "Online Transfer")
            {
                lvserial.Columns[3].Width = 0;
                lvserial.Columns[4].Width = 0;
            }
            #endregion
            //DataTable update1 = conn.getdataset("select * from Ledger where isactive=1 and VoucherID='" + str + "' and TranType='" + type + "'");
            //DataTable dt = conn.getdataset("select * from Voucher where PartyName='" + cmbPartyName.Text + "' and PaymentTerms='" + cmbEntry.Text + "'");
            //lvserial.Items.Clear();
            //for (int i = 0; i < update1.Rows.Count; i++)
            //{
            //    ListViewItem li;
            //    li = lvserial.Items.Add("");
            //    li.SubItems.Add(update1.Rows[i]["VoucherID"].ToString());
            //    li.SubItems.Add(update1.Rows[i]["OT1"].ToString());
            //    li.SubItems.Add(update1.Rows[i]["OT4"].ToString());
            //    li.SubItems.Add(Convert.ToDateTime(update1.Rows[i]["OT5"].ToString()).ToString(Master.dateformate));
            //    li.SubItems.Add(update1.Rows[i]["Amount"].ToString());
            //    li.SubItems.Add(update1.Rows[i]["OT2"].ToString());
            //    li.SubItems.Add(update1.Rows[i]["Ot3"].ToString());
            //    li.SubItems.Add(update1.Rows[i]["OT6"].ToString());
            //}
            DataTable update1 = conn.getdataset("select * from Voucher where isactive=1 and VchNo='" + str + "' and PaymentTerms='" + type + "'");
            for (int i = 0; i < update1.Rows.Count; i++)
            {
                ListViewItem li;
                li = lvserial.Items.Add("");
                li.SubItems.Add(update1.Rows[i]["VchNo"].ToString());
                li.SubItems.Add(update1.Rows[i]["PartyName"].ToString());
                li.SubItems.Add(update1.Rows[i]["ChequeNo"].ToString());
                li.SubItems.Add(update1.Rows[i]["ChequeDate"].ToString());
                if (cmbEntry.SelectedIndex == 1 || cmbEntry.SelectedIndex == 6)
                {
                    li.SubItems.Add(update1.Rows[i]["TotalAmount"].ToString());
                }
                else
                {
                    li.SubItems.Add(update1.Rows[i]["BasicAmount"].ToString());
                }
                li.SubItems.Add(update1.Rows[i]["OT2"].ToString());
                li.SubItems.Add(update1.Rows[i]["TotalAmount"].ToString());
                li.SubItems.Add(update1.Rows[i]["OT8"].ToString());
            }
            txtAmount.Focus();
            this.ActiveControl = txtAmount;
            BtnSubmit.Text = "Update";
        }

        private void txtDated_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 45 || e.KeyChar == 8 || e.KeyChar == 47)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void btnAddBank_Click(object sender, EventArgs e)
        {
            var privouscontroal = cmbBankSelect;
            activecontroal = privouscontroal.Name;
            Accountentry client = new Accountentry(this, master, tabControl, activecontroal);

            client.Passed(1);
            //   client.Show();
            master.AddNewTab(client);
        }

        private void btnAddParty_Click(object sender, EventArgs e)
        {
            var privouscontroal = cmbPartyName;
            activecontroal = privouscontroal.Name;
            Accountentry client = new Accountentry(this, master, tabControl, activecontroal);

            client.Passed(1);
            //   client.Show();
            master.AddNewTab(client);
        }

        private void btnEditBank_Click(object sender, EventArgs e)
        {
            if (cmbBankSelect.Text != "" && cmbBankSelect.Text != null)
            {
                var privouscontroal = cmbBankSelect;
                activecontroal = privouscontroal.Name;
                string iid = cmbBankSelect.SelectedValue.ToString();
                Accountentry client = new Accountentry(this, master, tabControl, activecontroal);
                client.Update(1, iid);
                client.Passed(1);
                //  client.Show();
                master.AddNewTab(client);
            }
            else
            {
                MessageBox.Show("Please Select Bank.");
            }
        }

        private void btnPartyEdit_Click(object sender, EventArgs e)
        {
            if (cmbPartyName.Text != "" && cmbPartyName.Text != null)
            {
                var privouscontroal = cmbPartyName;
                activecontroal = privouscontroal.Name;
                string iid = cmbPartyName.SelectedValue.ToString();
                Accountentry client = new Accountentry(this, master, tabControl, activecontroal);
                client.Update(1, iid);
                client.Passed(1);
                //  client.Show();
                master.AddNewTab(client);
            }
            else
            {
                MessageBox.Show("Please Select PartyName.");
            }
        }

        private void cmbBankSelect_Leave(object sender, EventArgs e)
        {
            cmbBankSelect.Text = s;
        }

        private void cmbEntry_Leave(object sender, EventArgs e)
        {
            cmbEntry.Text = s;
        }

        private void cmbPartyName_Leave(object sender, EventArgs e)
        {
            cmbPartyName.Text = s;
        }
        string remark;
        public string getremarks
        {
            get { return remark; }
            set { cmbRemark.Text = value; }
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

        private void txtExpences_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtTotalAmount_KeyPress(object sender, KeyPressEventArgs e)
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

        private void cmbBankSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbBankSelect.Items.Count; i++)
                {
                    s = cmbBankSelect.GetItemText(cmbBankSelect.Items[i]);
                    if (s == cmbBankSelect.Text)
                    {
                        inList = true;
                        cmbBankSelect.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbBankSelect.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void cmbEntry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbEntry.Items.Count; i++)
                {
                    s = cmbEntry.GetItemText(cmbEntry.Items[i]);
                    if (s == cmbEntry.Text)
                    {
                        inList = true;
                        cmbEntry.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbEntry.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void cmbPartyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbPartyName.Items.Count; i++)
                {
                    s = cmbPartyName.GetItemText(cmbPartyName.Items[i]);
                    if (s == cmbPartyName.Text)
                    {
                        inList = true;
                        cmbPartyName.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbPartyName.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void lvserial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                open();
            }
        }
    }
}
