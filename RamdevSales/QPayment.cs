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

namespace RamdevSales
{
    public partial class QPayment : Form
    {
        Connection con = new Connection();
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        data data = new data();
        public QPayment()
        {
            InitializeComponent();
            loadall();
        }

        public QPayment(Ledger ledger)
        {
            InitializeComponent();
            loadall();
            // TODO: Complete member initialization
            this.ledger = ledger;
        }

        public QPayment(CashBook cashBook)
        {
            // TODO: Complete member initialization
            this.cashBook = cashBook;
        }

        public QPayment(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            loadall();
            this.master = master;
            this.tabControl = tabControl;
        }
        OleDbSettings ods = new OleDbSettings();
        public void loadall()
        {
            isVisible(false);
            DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
            DTDate.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            DTDate.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            //DataSet dtrange = ods.getdata("SELECT SQLSetting.* FROM SQLSetting where OT6='" + Master.companyId + "'");
            //DTDate.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0][8].ToString());
            //DTDate.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0][9].ToString());
            getsr();
            bindcustomer();
            LVFO.Columns.Add("Receipt No", 114, HorizontalAlignment.Center);
            LVFO.Columns.Add("Account Name", 420, HorizontalAlignment.Left);
            LVFO.Columns.Add("Total Amount", 114, HorizontalAlignment.Right);
            LVFO.Columns.Add("Discount Amount", 114, HorizontalAlignment.Right);
            LVFO.Columns.Add("Net Amount", 114, HorizontalAlignment.Right);
            // LVFO.Columns.Add("A.Qty", 70, HorizontalAlignment.Center);
            LVFO.Columns.Add("Remarks", 385, HorizontalAlignment.Left);
        }
        DataTable userrights = new DataTable();
        private void QPayment_Load(object sender, EventArgs e)
        {
            userrights = con.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[5]["a"].ToString() == "False")
                {
                    btnsave.Enabled = false;
                }
                if (userrights.Rows[5]["p"].ToString() == "False")
                {
                    btnprint.Enabled = false;
                }
                if (userrights.Rows[5]["d"].ToString() == "False")
                {
                    btndelete.Enabled = false;
                }
            }
            DTDate.CustomFormat = Master.dateformate;
            this.ActiveControl = DTDate;
            //set the interval  and start the timer
            // timer1.Interval = 1000;
            // timer1.Start();
        }
        public static string activecontroal;
        public void bindcustomer()
        {
            cmbaccname.DataSource = null;
            DataTable dt1 = new DataTable();
            dt1 = con.getdataset("select ClientID,AccountName from ClientMaster where isactive=1 order by accountname");
            cmbaccname.ValueMember = "ClientID";
            cmbaccname.DisplayMember = "AccountName";
            cmbaccname.DataSource = dt1;
            cmbaccname.SelectedIndex = -1;


            // autobind(dt1, cmbaccname);
          //  autoreaderbind();
        }
        public void autoreaderbind()
        {
            try
            {
                AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();


                SqlDataReader dReader;
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = con1;
                cmd1.CommandType = CommandType.Text;

                //start string

                String qry = "select ClientID,AccountName from ClientMaster";
                //  con.Open();
                int count = 0;

                con1.Close();
                qry = qry + " where isactive=1 order by accountname";

                if (count == 0)
                {
                    //end string
                    cmd1.CommandText = qry;


                    con1.Open();
                    dReader = cmd1.ExecuteReader();

                    if (dReader.HasRows == true)
                    {
                        while (dReader.Read())
                            namesCollection.Add(dReader["AccountName"].ToString());

                    }
                    else
                    {
                        // MessageBox.Show("Data not found");
                    }
                    dReader.Close();

                    cmbaccname.AutoCompleteMode = AutoCompleteMode.Suggest;
                    cmbaccname.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    cmbaccname.AutoCompleteCustomSource = namesCollection;
                }
                else
                {

                    //end string
                    cmd1.CommandText = qry;


                    //    con.Open();
                    dReader = cmd1.ExecuteReader();

                    if (dReader.HasRows == true)
                    {
                        while (dReader.Read())
                            namesCollection.Add(dReader["AccountName"].ToString());

                    }
                    else
                    {
                        //MessageBox.Show("Data not found");
                    }
                    dReader.Close();

                    cmbaccname.AutoCompleteMode = AutoCompleteMode.Suggest;
                    cmbaccname.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    cmbaccname.AutoCompleteCustomSource = namesCollection;
                }
            }
            catch
            {
            }
            finally
            {
                con1.Close();
            }
        }
        private void autobind(DataTable dt1, ComboBox cmbcustname)
        {
            string[] arr = new string[dt1.Rows.Count];
            //  string list="";
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                arr[i] = dt1.Rows[i][1].ToString();
            }

            //    var stringArr = dt.Rows[0].ItemArray.Select(x => x.ToString()).ToArray();

            cmbcustname.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cmbcustname.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbcustname.AutoCompleteCustomSource.AddRange(arr);
        }
        void getsr()
        {
            try
            {


                String str = con.ExecuteScalar("select max(recno) from paymentreceipt where isactive='1' and type='P'");
                int id, count;
                //     Object data = dr[1];

                if (str == "")
                {

                    id = 1;
                    count = 1;
                }
                else
                {
                    id = Convert.ToInt32(str) + 1;
                    count = Convert.ToInt32(str) + 1;
                }
                txtrecno.Text = count.ToString();

            }
            catch
            {
            }
            finally
            {

            }

        }
        private void isVisible(bool p)
        {
            lblchqdate.Visible = p;
            lblchqno.Visible = p;
            lblbankname.Visible = p;
            txtchqdate.Visible = p;
            txtchqno.Visible = p;
            txtbankname.Visible = p;
        }


        private void cmbpaymode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbpaymode.Text != "Cash")
            {
                isVisible(true);
            }
            else
            {
                isVisible(false);
            }
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbpaymode.Items.Count; i++)
                {
                    s = cmbpaymode.GetItemText(cmbpaymode.Items[i]);
                    if (s == cmbpaymode.Text)
                    {
                        inList = true;
                        cmbpaymode.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbpaymode.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void DTDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbpaymode.Focus();
            }
        }
        public static string s;
        private void cmbpaymode_KeyDown(object sender, KeyEventArgs e)
        {
            LVFO.Items.Clear();
            if (cmbpaymode.Text != "Cash")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true; // This will eliminate the beeping
                    bool inList = false;
                    for (int i = 0; i < cmbpaymode.Items.Count; i++)
                    {
                        s = cmbpaymode.GetItemText(cmbpaymode.Items[i]);
                        if (s == cmbpaymode.Text)
                        {
                            inList = true;
                            cmbpaymode.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        cmbpaymode.Text = "";
                    }


                    txtchqno.Focus();
                    DataTable dt1 = con.getdataset("select * from paymentreceipt where isactive='1' and type='P' and [date] = '" + Convert.ToDateTime(DTDate.Text).ToString("MM-dd-yyyy") + "' and [mode] = '" + cmbpaymode.Text + "'");

                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        ListViewItem li;
                        li = LVFO.Items.Add(dt1.Rows[i][1].ToString());
                        string str = data.getclientname(dt1.Rows[i][8].ToString());
                        li.SubItems.Add(str);
                        li.SubItems.Add(dt1.Rows[i][9].ToString());
                        li.SubItems.Add(dt1.Rows[i][10].ToString());
                        li.SubItems.Add(dt1.Rows[i][11].ToString());
                        // li.SubItems.Add(txtaqty.Text);
                        li.SubItems.Add(dt1.Rows[i][12].ToString());

                        //   li.SubItems.Add(txtamount.Text);

                    }
                }
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true; // This will eliminate the beeping
                    bool inList = false;
                    for (int i = 0; i < cmbpaymode.Items.Count; i++)
                    {
                        s = cmbpaymode.GetItemText(cmbpaymode.Items[i]);
                        if (s == cmbpaymode.Text)
                        {
                            inList = true;
                            cmbpaymode.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        cmbpaymode.Text = "";
                    }

                    cmbaccname.Focus();
                    DataTable dt1 = con.getdataset("select * from paymentreceipt where isactive='1' and type='P' and [date] = '" + Convert.ToDateTime(DTDate.Text).ToString("MM-dd-yyyy") + "' and [mode] = '" + cmbpaymode.Text + "'");

                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        ListViewItem li;
                        li = LVFO.Items.Add(dt1.Rows[i][1].ToString());
                        string str = data.getclientname(dt1.Rows[i][8].ToString());
                        li.SubItems.Add(str);
                        li.SubItems.Add(dt1.Rows[i][9].ToString());
                        li.SubItems.Add(dt1.Rows[i][10].ToString());
                        li.SubItems.Add(dt1.Rows[i][11].ToString());
                        // li.SubItems.Add(txtaqty.Text);
                        li.SubItems.Add(dt1.Rows[i][12].ToString());

                        //   li.SubItems.Add(txtamount.Text);

                    }
                }

            }
            total();

        }
        public DataTable dtselectedrow = new DataTable();

        public void popup()
        {
            DataTable dt = con.getdataset("select bill_date,billno,totalnet from billmaster where  clientid='" + cmbaccname.SelectedValue + "' and Billtype='P' and isactive='1' order by bill_date");
            if (dt.Rows.Count > 0)
            {


                PurchaseSelectBills popup = new PurchaseSelectBills(txtnetamt, dt, cmbaccname.SelectedValue, cmbaccname.Text, this, btnsave.Text,txtrecno.Text);

                popup.ShowDialog();

                string userEnteredText = popup.EnteredText;

                popup.Dispose();
            }
        }

        private void cmbaccname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {

                    e.SuppressKeyPress = true; // This will eliminate the beeping
                    bool inList = false;
                    for (int i = 0; i < cmbaccname.Items.Count; i++)
                    {
                        s = cmbaccname.GetItemText(cmbaccname.Items[i]);
                        if (s == cmbaccname.Text)
                        {
                            inList = true;
                            cmbaccname.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        cmbaccname.Text = "";
                    }

                    DataTable dt = new DataTable();
                    dt = con.getdataset("select AccountName from ClientMaster order by AccountName");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (cmbaccname.Text == dt.Rows[i]["AccountName"].ToString())
                        {
                            txtamt.Focus();
                        }
                    }

                }
                if (e.KeyCode == Keys.F3)
                {
                    var privouscontroal = cmbaccname;
                    activecontroal = privouscontroal.Name;
                    Accountentry dlg = new Accountentry(this, master, tabControl, activecontroal);
                    //dlg.MdiParent = this.MdiParent;
                    //dlg.StartPosition = FormStartPosition.CenterScreen;
                    //// this.Hide();
                    //dlg.Show();
                    master.AddNewTab(dlg);
                    // bindcustomer();
                    this.ActiveControl = cmbaccname;
                }
                if (e.KeyCode == Keys.F2)
                {
                    if (cmbaccname.Text != "" && cmbaccname.Text != null)
                    {

                        string accname = cmbaccname.Text;
                        DataTable clientID = con.getdataset("select * from ClientMaster where AccountName='" + accname + "'");
                        if (clientID.Rows.Count > 0)
                        {
                            var privouscontroal = cmbaccname;
                            activecontroal = privouscontroal.Name;
                            int accountid = int.Parse(cmbaccname.SelectedValue.ToString());
                            Accountentry dlg = new Accountentry(this, master, tabControl, activecontroal);
                            dlg.Update(accountid);
                            master.AddNewTab(dlg);
                            this.ActiveControl = cmbaccname;
                        }
                        else
                        {
                            MessageBox.Show("Please Enter/Select Valid Account Name ");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select Account Name");
                    }
                    bindcustomer();
                }
            }
            catch
            {
            }
        }

        private void txtchqno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtchqdate.Focus();
            }
        }

        private void txtchqdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtbankname.Focus();
            }
        }

        private void txtbankname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbaccname.Focus();
            }
        }

        private void txtamt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtdiscount.Focus();
            }
        }
        DataTable qp = new DataTable();
        private void txtdiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                qp = con.getdataset("select * from Additional where MasterType='Quick Payment'");
                if (qp.Rows.Count > 0)
                {
                    pnladditional.Visible = true;
                    if (qp.Rows[0]["nooffields"].ToString() == "1")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "2")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "3")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "4")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "5")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "6")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "7")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "8")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        lblf8.Visible = true;
                        txtf8.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                            lblf8.Text = qp.Rows[0]["field8"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "9")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        lblf8.Visible = true;
                        txtf8.Visible = true;
                        lblf9.Visible = true;
                        txtf9.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                            lblf8.Text = qp.Rows[0]["field8"].ToString();
                            lblf9.Text = qp.Rows[0]["field9"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "10")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        lblf8.Visible = true;
                        txtf8.Visible = true;
                        lblf9.Visible = true;
                        txtf9.Visible = true;
                        lblf10.Visible = true;
                        txtf10.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                            lblf8.Text = qp.Rows[0]["field8"].ToString();
                            lblf9.Text = qp.Rows[0]["field9"].ToString();
                            lblf10.Text = qp.Rows[0]["field10"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "11")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        lblf8.Visible = true;
                        txtf8.Visible = true;
                        lblf9.Visible = true;
                        txtf9.Visible = true;
                        lblf10.Visible = true;
                        txtf10.Visible = true;
                        lblf11.Visible = true;
                        txtf11.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                            lblf8.Text = qp.Rows[0]["field8"].ToString();
                            lblf9.Text = qp.Rows[0]["field9"].ToString();
                            lblf10.Text = qp.Rows[0]["field10"].ToString();
                            lblf11.Text = qp.Rows[0]["field11"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "12")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        lblf8.Visible = true;
                        txtf8.Visible = true;
                        lblf9.Visible = true;
                        txtf9.Visible = true;
                        lblf10.Visible = true;
                        txtf10.Visible = true;
                        lblf11.Visible = true;
                        txtf11.Visible = true;
                        lblf12.Visible = true;
                        txtf12.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                            lblf8.Text = qp.Rows[0]["field8"].ToString();
                            lblf9.Text = qp.Rows[0]["field9"].ToString();
                            lblf10.Text = qp.Rows[0]["field10"].ToString();
                            lblf11.Text = qp.Rows[0]["field11"].ToString();
                            lblf12.Text = qp.Rows[0]["field12"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "13")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        lblf8.Visible = true;
                        txtf8.Visible = true;
                        lblf9.Visible = true;
                        txtf9.Visible = true;
                        lblf10.Visible = true;
                        txtf10.Visible = true;
                        lblf11.Visible = true;
                        txtf11.Visible = true;
                        lblf12.Visible = true;
                        txtf12.Visible = true;
                        lblf13.Visible = true;
                        txtf13.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                            lblf8.Text = qp.Rows[0]["field8"].ToString();
                            lblf9.Text = qp.Rows[0]["field9"].ToString();
                            lblf10.Text = qp.Rows[0]["field10"].ToString();
                            lblf11.Text = qp.Rows[0]["field11"].ToString();
                            lblf12.Text = qp.Rows[0]["field12"].ToString();
                            lblf13.Text = qp.Rows[0]["field13"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "14")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        lblf8.Visible = true;
                        txtf8.Visible = true;
                        lblf9.Visible = true;
                        txtf9.Visible = true;
                        lblf10.Visible = true;
                        txtf10.Visible = true;
                        lblf11.Visible = true;
                        txtf11.Visible = true;
                        lblf12.Visible = true;
                        txtf12.Visible = true;
                        lblf13.Visible = true;
                        txtf13.Visible = true;
                        lblf14.Visible = true;
                        txtf14.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                            lblf8.Text = qp.Rows[0]["field8"].ToString();
                            lblf9.Text = qp.Rows[0]["field9"].ToString();
                            lblf10.Text = qp.Rows[0]["field10"].ToString();
                            lblf11.Text = qp.Rows[0]["field11"].ToString();
                            lblf12.Text = qp.Rows[0]["field12"].ToString();
                            lblf13.Text = qp.Rows[0]["field13"].ToString();
                            lblf14.Text = qp.Rows[0]["field14"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "15")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        lblf8.Visible = true;
                        txtf8.Visible = true;
                        lblf9.Visible = true;
                        txtf9.Visible = true;
                        lblf10.Visible = true;
                        txtf10.Visible = true;
                        lblf11.Visible = true;
                        txtf11.Visible = true;
                        lblf12.Visible = true;
                        txtf12.Visible = true;
                        lblf13.Visible = true;
                        txtf13.Visible = true;
                        lblf14.Visible = true;
                        txtf14.Visible = true;
                        lblf15.Visible = true;
                        txtf15.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                            lblf8.Text = qp.Rows[0]["field8"].ToString();
                            lblf9.Text = qp.Rows[0]["field9"].ToString();
                            lblf10.Text = qp.Rows[0]["field10"].ToString();
                            lblf11.Text = qp.Rows[0]["field11"].ToString();
                            lblf12.Text = qp.Rows[0]["field12"].ToString();
                            lblf13.Text = qp.Rows[0]["field13"].ToString();
                            lblf14.Text = qp.Rows[0]["field14"].ToString();
                            lblf15.Text = qp.Rows[0]["field15"].ToString();
                        }
                    }
                    txtf1.Focus();
                }
                else
                {
                    txtnetamt.Focus();
                }
            }
        }

        private void txtnetamt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtnetamt.Text))
                {
                    if (Convert.ToDouble(txtnetamt.Text) > 0)
                    {
                        popup();
                        txtremark.Focus();
                    }
                }

            }
        }

        private void txtremark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnsave.Focus();
            }
        }
        public void submit()
        {
            try
            {
                this.Enabled = false;
                if (btnsave.Text == "Update")
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[5]["u"].ToString() == "True")
                        {
                            if (cmbaccname.Text != "")
                            {
                                string strOne = lblbillno.Text;
                                string[] strArrayOne = new string[] { "" };
                                //somewhere in your code
                                strArrayOne = strOne.Split(',');
                                con.execute("UPDATE [dbo].[Ref] SET isactive=0 where [VchNo]='" + txtrecno.Text + "' and [TransactionType]='" + "P" + "'");
                                for (int i = 0; i < dtselectedrow.Rows.Count; i++)
                                {
                                    DataTable dt = con.getdataset("select * from billmaster where  Billtype='P' and isactive='1' and billno='" + dtselectedrow.Rows[i]["BillNo"].ToString() + "'");
                                    if (Convert.ToDouble(dtselectedrow.Rows[i]["BalAmount"].ToString()) <= 0)
                                    {
                                        con.execute("Update billmaster set OrderStatus='" + "Clear" + "' where billno='" + dtselectedrow.Rows[i]["BillNo"].ToString() + "'");
                                    }
                                    DataTable client = con.getdataset("select * from ClientMaster where isactive=1 and ClientID='" + dt.Rows[0]["ClientID"].ToString() + "'");
                                    con.execute("INSERT INTO [dbo].[Ref]([VchNo],[TransactionType],[AccountID],[AccountName],[RefAmount],[DC],[ReceiptNo],[ReceiptDate],[OT7],[OM1],[isactive],[OT1],[OT2],[OT3],[OT4],[OT5],[OT6]) values ('" + txtrecno.Text + "','P','" + dt.Rows[0]["ClientID"].ToString() + "','" + client.Rows[0]["AccountName"].ToString() + "','" + txtnetamt.Text + "','D','" + txtrecno.Text + "','" + Convert.ToDateTime(DTDate.Text).ToString("MM-dd-yyyy") + "','" + txtremark.Text + "','" + strArrayOne[i] + "',1,'" + dtselectedrow.Rows[i]["Date"].ToString() + "','" + dtselectedrow.Rows[i]["BillNO"].ToString() + "','" + dtselectedrow.Rows[i]["BillAmount"].ToString() + "','" + dtselectedrow.Rows[i]["ReceivedAmount"].ToString() + "','" + dtselectedrow.Rows[i]["BalAmount"].ToString() + "','" + dtselectedrow.Rows[i]["Status"].ToString() + "')");
                                }
                                //for (int i = 0; i < strArrayOne.Length; i++)
                                //{
                                //    if (!string.IsNullOrEmpty(strArrayOne[i]))
                                //    {
                                //        DataTable dt = con.getdataset("select * from billmaster where  Billtype='P' and isactive='1' and billno='" + strArrayOne[i] + "'");
                                //        DataTable client = con.getdataset("select * from ClientMaster where isactive=1 and ClientID='" + dt.Rows[0]["ClientID"].ToString() + "'");
                                //        //con.execute("INSERT INTO [dbo].[Ref]([VchNo],[TransactionType],[AccountID],[AccountName],[RefAmount],[DC],[ReceiptNo],[ReceiptDate],[OT1],[OM1]) values('" + txtrecno.Text + "','P','" + dt.Rows[0]["ClientID"].ToString() + "','" + client.Rows[0]["AccountName"].ToString() + "','" + txtnetamt.Text + "','D','" + txtrecno + "','" + Convert.ToDateTime(DTDate.Text).ToString("MM-dd-yyyy") + "','" + txtremark.Text + "','" + strArrayOne[i] + "')");
                                //        con.execute("UPDATE [dbo].[Ref] SET [TransactionType]='P',[AccountID]='" + dt.Rows[0]["ClientID"].ToString() + "',[AccountName]='" + client.Rows[0]["AccountName"].ToString() + "',[RefAmount]='" + txtnetamt.Text + "',[ReceiptNo]='" + txtrecno.Text + "',[ReceiptDate]='" + Convert.ToDateTime(DTDate.Text).ToString("MM-dd-yyyy") + "',[OT1]='" + txtremark.Text + "',[OM1]='" + strArrayOne[i] + "' where [VchNo]='" + txtrecno.Text + "' and [TransactionType]='" + "P" + "'");
                                //    }
                                //}

                                strArrayOne = null;
                                con.execute("UPDATE [dbo].[paymentreceipt]SET [date] = '" + Convert.ToDateTime(DTDate.Text).ToString("MM-dd-yyyy") + "',[mode] = '" + cmbpaymode.Text + "',[chqno] = '" + txtchqno.Text + "',[chqdate] = '" + txtchqdate.Text + "',[bankname] = '" + txtbankname.Text + "',[clientid] = '" + cmbaccname.SelectedValue + "',[totalamount] = " + txtamt.Text + ",[discountamt] = " + txtdiscount.Text + ",[netamt] = " + txtnetamt.Text + ",[remarks] = '" + txtremark.Text + "',[OT1]='" + txtf1.Text + "',[OT2]='" + txtf2.Text + "',[OT3]='" + txtf3.Text + "',[OT4]='" + txtf4.Text + "',[OT5]='" + txtf5.Text + "',[OT6]='" + txtf6.Text + "',[OT7]='" + txtf7.Text + "',[OT8]='" + txtf8.Text + "',[OT9]='" + txtf9.Text + "',[OT10]='" + txtf10.Text + "',[OT11]='" + txtf11.Text + "',[OT12]='" + txtf12.Text + "',[OT13]='" + txtf13.Text + "',[OT14]='" + txtf14.Text + "',[OT15]='" + txtf15.Text + "' WHERE [recno] = '" + txtrecno.Text + "' and type='P' and [isactive]=1");
                                con.execute("UPDATE [dbo].[Ledger] SET [Date1]='" + Convert.ToDateTime(DTDate.Text).ToString("MM-dd-yyyy") + "',[AccountID] = '" + cmbaccname.SelectedValue + "',[AccountName]='" + cmbaccname.Text + "' ,[Amount] = '" + Math.Round(Convert.ToDouble(txtnetamt.Text), 2).ToString("########.00") + "',[DC] = 'D',[OT1]='" + cmbpaymode.Text + "',[ShortNarration]='" + txtremark.Text + "' where [VoucherID]= '" + txtrecno.Text + "' and [TranType] = 'Pmnt'");
                                MessageBox.Show("Update Successfully");

                                ListViewItem li;
                                li = LVFO.Items.Add(txtrecno.Text);
                                li.SubItems.Add(cmbaccname.Text);
                                li.SubItems.Add(txtamt.Text);
                                li.SubItems.Add(txtdiscount.Text);
                                li.SubItems.Add(txtnetamt.Text);
                                // li.SubItems.Add(txtaqty.Text);
                                li.SubItems.Add(txtremark.Text);
                                total();
                                clearall();
                                btnsave.Text = "Submit";
                                isVisible(false);
                                getsr();
                                this.ActiveControl = DTDate;
                                // DTDate.Focus();
                            }
                            else
                            {
                                MessageBox.Show("Please select Account Name");
                                cmbaccname.Focus();
                            }
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
                    if (cmbaccname.Text != "")
                    {
                        string strOne = lblbillno.Text;
                        string[] strArrayOne = new string[] { "" };
                        //somewhere in your code
                        strArrayOne = strOne.Split(',');
                        for (int i = 0; i < dtselectedrow.Rows.Count; i++)
                        {
                            DataTable dt = con.getdataset("select * from billmaster where  Billtype='P' and isactive='1' and billno='" + dtselectedrow.Rows[i]["BillNo"].ToString() + "'");
                            if (Convert.ToDouble(dtselectedrow.Rows[i]["BalAmount"].ToString()) <= 0)
                            {
                                con.execute("Update billmaster set OrderStatus='" + "Clear" + "' where billno='" + dtselectedrow.Rows[i]["BillNo"].ToString() + "'");
                            }
                            DataTable client = con.getdataset("select * from ClientMaster where isactive=1 and ClientID='" + dt.Rows[0]["ClientID"].ToString() + "'");
                            con.execute("INSERT INTO [dbo].[Ref]([VchNo],[TransactionType],[AccountID],[AccountName],[RefAmount],[DC],[ReceiptNo],[ReceiptDate],[OT7],[OM1],[isactive],[OT1],[OT2],[OT3],[OT4],[OT5],[OT6]) values ('" + txtrecno.Text + "','P','" + dt.Rows[0]["ClientID"].ToString() + "','" + client.Rows[0]["AccountName"].ToString() + "','" + txtnetamt.Text + "','D','" + txtrecno.Text + "','" + Convert.ToDateTime(DTDate.Text).ToString("MM-dd-yyyy") + "','" + txtremark.Text + "','" + strArrayOne[i] + "',1,'" + dtselectedrow.Rows[i]["Date"].ToString() + "','" + dtselectedrow.Rows[i]["BillNO"].ToString() + "','" + dtselectedrow.Rows[i]["BillAmount"].ToString() + "','" + dtselectedrow.Rows[i]["ReceivedAmount"].ToString() + "','" + dtselectedrow.Rows[i]["BalAmount"].ToString() + "','" + dtselectedrow.Rows[i]["Status"].ToString() + "')");
                        }
                        //for (int i = 0; i < strArrayOne.Length; i++)
                        //{
                        //    if (!string.IsNullOrEmpty(strArrayOne[i]))
                        //    {
                        //        DataTable dt = con.getdataset("select * from billmaster where  Billtype='P' and isactive='1' and billno='" + strArrayOne[i] + "'");
                        //        DataTable client = con.getdataset("select * from ClientMaster where isactive=1 and ClientID='" + dt.Rows[0]["ClientID"].ToString() + "'");
                        //        con.execute("INSERT INTO [dbo].[Ref]([VchNo],[TransactionType],[AccountID],[AccountName],[RefAmount],[DC],[ReceiptNo],[ReceiptDate],[OT1],[OM1],[isactive]) values('" + txtrecno.Text + "','P','" + dt.Rows[0]["ClientID"].ToString() + "','" + client.Rows[0]["AccountName"].ToString() + "','" + txtnetamt.Text + "','D','" + txtrecno.Text + "','" + Convert.ToDateTime(DTDate.Text).ToString("MM-dd-yyyy") + "','" + txtremark.Text + "','" + strArrayOne[i] + "',1)");
                        //    }
                        //}

                        strArrayOne = null;

                        con.execute("INSERT INTO [dbo].[paymentreceipt]([recno],[type],[date],[mode],[chqno],[chqdate],[bankname],[clientid],[totalamount],[discountamt],[netamt],[remarks],[OT1],[OT2],[OT3],[OT4],[OT5],[OT6],[OT7],[OT8],[OT9],[OT10],[OT11],[OT12],[OT13],[OT14],[OT15],[isactive])VALUES('" + txtrecno.Text + "','P','" + Convert.ToDateTime(DTDate.Text).ToString("MM-dd-yyyy") + "','" + cmbpaymode.Text + "','" + txtchqno.Text + "','" + txtchqdate.Text + "','" + txtbankname.Text + "','" + cmbaccname.SelectedValue + "'," + txtamt.Text + "," + txtdiscount.Text + "," + txtnetamt.Text + ",'" + txtremark.Text + "','" + txtf1.Text + "','" + txtf2.Text + "','" + txtf3.Text + "','" + txtf4.Text + "','" + txtf5.Text + "','" + txtf6.Text + "','" + txtf7.Text + "','" + txtf8.Text + "','" + txtf9.Text + "','" + txtf10.Text + "','" + txtf11.Text + "','" + txtf12.Text + "','" + txtf13.Text + "','" + txtf14.Text + "','" + txtf15.Text + "','1')");
                        con.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[ShortNarration],[OT1],[isactive]) values ('" + txtrecno.Text + "','" + Convert.ToDateTime(DTDate.Text).ToString("MM-dd-yyyy") + "','Pmnt','" + cmbaccname.SelectedValue + "','" + cmbaccname.Text + "','" + txtnetamt.Text + "','D','" + txtremark.Text + "','" + cmbpaymode.Text + "',1)");
                        MessageBox.Show("Submit Successfully");

                        ListViewItem li;
                        li = LVFO.Items.Add(txtrecno.Text);
                        li.SubItems.Add(cmbaccname.Text);
                        li.SubItems.Add(txtamt.Text);
                        li.SubItems.Add(txtdiscount.Text);
                        li.SubItems.Add(txtnetamt.Text);
                        // li.SubItems.Add(txtaqty.Text);
                        li.SubItems.Add(txtremark.Text);
                        total();
                        clearall();
                        isVisible(false);
                        getsr();
                        this.ActiveControl = DTDate;
                        //DTDate.Focus();


                    }
                    else
                    {
                        MessageBox.Show("Please select Account Name");
                        cmbaccname.Focus();
                    }
                }

            }
            catch
            {
            }
            finally
            {
                this.Enabled = true;
            }
        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            submit();
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
            if (keyData == (Keys.Alt | Keys.U))
            {
                //DialogResult dr = MessageBox.Show("Do you want to Update?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (dr == DialogResult.Yes)
                //{
                submit();
                //}
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void total()
        {


            Double amt = 0, dis = 0, net = 0;


            for (int i = 0; i < LVFO.Items.Count; i++)
            {
                // count++;
                amt = amt + Convert.ToDouble(LVFO.Items[i].SubItems[2].Text);
                //  aqty = aqty + Convert.ToInt32(LVFO.Items[i].SubItems[5].Text);
                dis = dis + Convert.ToDouble(LVFO.Items[i].SubItems[3].Text);
                net = net + Convert.ToDouble(LVFO.Items[i].SubItems[4].Text);
                //double vatcalc = (100 * Convert.ToDouble(LVFO.Items[i].SubItems[6].Text)) / 105;
                //Double multi = 0;
                //multi = (Convert.ToDouble(LVFO.Items[i].SubItems[8].Text) * (Convert.ToDouble(LVFO.Items[i].SubItems[9].Text) / 100));
                //vat = vat + multi;

            }
            txttotamt.Text = Math.Round(amt, 2).ToString("N2");
            txttotdis.Text = Math.Round(dis, 2).ToString("N2");
            // lbltotaqty.Text = aqty.ToString();
            txttotnet.Text = Math.Round(net, 2).ToString("N2");

        }
        private void clearall()
        {
            cmbaccname.Text = "";
            cmbpaymode.Text = "";
            txtamt.Text = "";
            txtbankname.Text = "";
            txtchqdate.Text = "";
            txtchqno.Text = "";
            txtdiscount.Text = "";
            txtnetamt.Text = "";
            txtrecno.Text = "";
            txtremark.Text = "";
            lblbillno.Text = "";
            txtf1.Text = "";
            txtf2.Text = "";
            txtf3.Text = "";
            txtf4.Text = "";
            txtf5.Text = "";
            txtf6.Text = "";
            txtf7.Text = "";
            txtf8.Text = "";
            txtf9.Text = "";
            txtf10.Text = "";
            txtf11.Text = "";
            txtf12.Text = "";
            txtf13.Text = "";
            txtf14.Text = "";
            txtf15.Text = "";
            txtf1.Text = "";
            //  txttotamt.Text = "";
            //  txttotdis.Text = "";
            //  txttotnet.Text = "";

        }

        private void txtamt_TextChanged(object sender, EventArgs e)
        {
            calculations();
        }

        private void calculations()
        {
            if (txtamt.Text == "")
            {
                txtamt.Text = "0";
            }
            if (txtdiscount.Text == "")
            {
                txtdiscount.Text = "0";
            }
            if (txtnetamt.Text == "")
            {
                txtnetamt.Text = "0";
            }
            txtnetamt.Text = (Convert.ToDouble(txtamt.Text) - Convert.ToDouble(txtdiscount.Text)).ToString();
        }

        private void txtdiscount_TextChanged(object sender, EventArgs e)
        {
            calculations();
        }
        private string receiptno;
        public void open()
        {
            try
            {
                if (LVFO.SelectedItems.Count > 0)
                {

                    txtrecno.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[0].Text;
                    cmbaccname.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[1].Text;
                    txtamt.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[2].Text;
                    txtdiscount.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[3].Text;
                    txtnetamt.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[4].Text;
                    //    txtaqty.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[5].Text;
                    txtremark.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[5].Text;
                    DataTable dt = con.getdataset("select * from paymentreceipt where isactive='1' and type='P' and recno='" + txtrecno.Text + "'");
                    cmbpaymode.Text = dt.Rows[0][4].ToString();
                    DTDate.Text = dt.Rows[0][3].ToString();
                    txtchqno.Text = dt.Rows[0][5].ToString();
                    txtchqdate.Text = dt.Rows[0][6].ToString();
                    txtbankname.Text = dt.Rows[0][7].ToString();
                    txtf1.Text = dt.Rows[0]["OT1"].ToString();
                    txtf2.Text = dt.Rows[0]["OT2"].ToString();
                    txtf3.Text = dt.Rows[0]["OT3"].ToString();
                    txtf4.Text = dt.Rows[0]["OT4"].ToString();
                    txtf5.Text = dt.Rows[0]["OT5"].ToString();
                    txtf6.Text = dt.Rows[0]["OT6"].ToString();
                    txtf7.Text = dt.Rows[0]["OT7"].ToString();
                    txtf8.Text = dt.Rows[0]["OT8"].ToString();
                    txtf9.Text = dt.Rows[0]["OT9"].ToString();
                    txtf10.Text = dt.Rows[0]["OT10"].ToString();
                    txtf11.Text = dt.Rows[0]["OT11"].ToString();
                    txtf12.Text = dt.Rows[0]["OT12"].ToString();
                    txtf13.Text = dt.Rows[0]["OT13"].ToString();
                    txtf14.Text = dt.Rows[0]["OT14"].ToString();
                    txtf15.Text = dt.Rows[0]["OT15"].ToString();
                    LVFO.Items[LVFO.FocusedItem.Index].Remove();
                    total();
                    this.ActiveControl = txtamt;
                    btnsave.Text = "Update";

                }
            }
            catch
            {
            }
        }
        private void LVFO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            open();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var privouscontroal = cmbaccname;
            activecontroal = privouscontroal.Name;
            Accountentry dlg = new Accountentry(this, master, tabControl, activecontroal);
            //dlg.MdiParent = this.MdiParent;
            //dlg.StartPosition = FormStartPosition.CenterScreen;
            //// this.Hide();
            //dlg.Show();
            master.AddNewTab(dlg);
            // bindcustomer();
            // this.ActiveControl = cmbaccname;
        }
        string remark;
        string purchasebillno;
        private Ledger ledger;
        private CashBook cashBook;
        private Master master;
        private TabControl tabControl;
        public void getremark(string getstring, string getstring1)
        {
            remark = getstring;
            purchasebillno = getstring1;
        }
        public string getremarks
        {
            get { return remark; }
            set { txtremark.Text = value; }
        }
        public string getbillno
        {
            get { return purchasebillno; }
            set { lblbillno.Text = value; }
        }





        internal void updatemode(string str, string p, int p_2, string date)
        {
            userrights = con.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[5]["a"].ToString() == "False")
                {
                    btnsave.Enabled = false;
                }
                if (userrights.Rows[5]["p"].ToString() == "False")
                {
                    btnprint.Enabled = false;
                }
                if (userrights.Rows[5]["d"].ToString() == "False")
                {
                    btndelete.Enabled = false;
                }
            }
            cmbpaymode.Text = str;
            string DateString = date;
            DTDate.Value = DateTime.ParseExact(DateString, "dd-MMM-yyyy", System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None);
            //  DTDate.Text =Convert.ToDateTime(date).ToString("dd/MM/yyyy");
            LVFO.Items.Clear();
            if (cmbpaymode.Text != "Cash")
            {

                txtchqno.Focus();
                DataTable dt1 = con.getdataset("select * from paymentreceipt where isactive='1' and type='P' and [date] = '" + Convert.ToDateTime(DTDate.Value).ToString("MM-dd-yyyy") + "' and [mode] = '" + cmbpaymode.Text + "'");

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    ListViewItem li;
                    li = LVFO.Items.Add(dt1.Rows[i][1].ToString());
                    string str1 = data.getclientname(dt1.Rows[i][8].ToString());
                    li.SubItems.Add(str1);
                    li.SubItems.Add(dt1.Rows[i][9].ToString());
                    li.SubItems.Add(dt1.Rows[i][10].ToString());
                    li.SubItems.Add(dt1.Rows[i][11].ToString());
                    // li.SubItems.Add(txtaqty.Text);
                    li.SubItems.Add(dt1.Rows[i][12].ToString());

                    //   li.SubItems.Add(txtamount.Text);
                    txtf1.Text = dt1.Rows[i]["OT1"].ToString();
                    txtf2.Text = dt1.Rows[i]["OT2"].ToString();
                    txtf3.Text = dt1.Rows[i]["OT3"].ToString();
                    txtf4.Text = dt1.Rows[i]["OT4"].ToString();
                    txtf5.Text = dt1.Rows[i]["OT5"].ToString();
                    txtf6.Text = dt1.Rows[i]["OT6"].ToString();
                    txtf7.Text = dt1.Rows[i]["OT7"].ToString();
                    txtf8.Text = dt1.Rows[i]["OT8"].ToString();
                    txtf9.Text = dt1.Rows[i]["OT9"].ToString();
                    txtf10.Text = dt1.Rows[i]["OT10"].ToString();
                    txtf11.Text = dt1.Rows[i]["OT11"].ToString();
                    txtf12.Text = dt1.Rows[i]["OT12"].ToString();
                    txtf13.Text = dt1.Rows[i]["OT13"].ToString();
                    txtf14.Text = dt1.Rows[i]["OT14"].ToString();
                    txtf15.Text = dt1.Rows[i]["OT15"].ToString();

                }

            }
            else
            {
                cmbaccname.Focus();
                DataTable dt1 = con.getdataset("select * from paymentreceipt where isactive='1' and type='P' and [date] = '" + Convert.ToDateTime(DTDate.Value).ToString("MM-dd-yyyy") + "' and [mode] = '" + cmbpaymode.Text + "'");

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    ListViewItem li;
                    li = LVFO.Items.Add(dt1.Rows[i][1].ToString());
                    string str1 = data.getclientname(dt1.Rows[i][8].ToString());
                    li.SubItems.Add(str1);
                    li.SubItems.Add(dt1.Rows[i][9].ToString());
                    li.SubItems.Add(dt1.Rows[i][10].ToString());
                    li.SubItems.Add(dt1.Rows[i][11].ToString());
                    // li.SubItems.Add(txtaqty.Text);
                    li.SubItems.Add(dt1.Rows[i][12].ToString());

                    //   li.SubItems.Add(txtamount.Text);
                    txtf1.Text = dt1.Rows[i]["OT1"].ToString();
                    txtf2.Text = dt1.Rows[i]["OT2"].ToString();
                    txtf3.Text = dt1.Rows[i]["OT3"].ToString();
                    txtf4.Text = dt1.Rows[i]["OT4"].ToString();
                    txtf5.Text = dt1.Rows[i]["OT5"].ToString();
                    txtf6.Text = dt1.Rows[i]["OT6"].ToString();
                    txtf7.Text = dt1.Rows[i]["OT7"].ToString();
                    txtf8.Text = dt1.Rows[i]["OT8"].ToString();
                    txtf9.Text = dt1.Rows[i]["OT9"].ToString();
                    txtf10.Text = dt1.Rows[i]["OT10"].ToString();
                    txtf11.Text = dt1.Rows[i]["OT11"].ToString();
                    txtf12.Text = dt1.Rows[i]["OT12"].ToString();
                    txtf13.Text = dt1.Rows[i]["OT13"].ToString();
                    txtf14.Text = dt1.Rows[i]["OT14"].ToString();
                    txtf15.Text = dt1.Rows[i]["OT15"].ToString();
                }


            }
            total();


            if (LVFO.Items.Count > 0)
            {
                int i;
                for (i = 0; i < LVFO.Items.Count; i++)
                {
                    if (p == LVFO.Items[i].SubItems[0].Text)
                    {
                        LVFO.Items[i].Focused = true;
                        break;
                    }
                }
                txtrecno.Text = LVFO.Items[i].SubItems[0].Text;
                cmbaccname.Text = LVFO.Items[i].SubItems[1].Text;
                txtamt.Text = LVFO.Items[i].SubItems[2].Text;
                txtdiscount.Text = LVFO.Items[i].SubItems[3].Text;
                txtnetamt.Text = LVFO.Items[i].SubItems[4].Text;
                //    txtaqty.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[5].Text;
                txtremark.Text = LVFO.Items[i].SubItems[5].Text;
                DataTable dt = con.getdataset("select * from paymentreceipt where isactive='1' and type='P' and recno='" + txtrecno.Text + "'");
                cmbpaymode.Text = dt.Rows[0][4].ToString();
                DTDate.Text = dt.Rows[0][3].ToString();
                txtchqno.Text = dt.Rows[0][5].ToString();
                txtchqdate.Text = dt.Rows[0][6].ToString();
                txtbankname.Text = dt.Rows[0][7].ToString();
                LVFO.Items[i].Remove();
                total();
                btnsave.Text = "Update";
            }


        }



        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                Printing prndata = new Printing();
                if (LVFO.SelectedItems.Count > 0)
                {
                    ChangeNumbersToWords sh = new ChangeNumbersToWords();
                    String s1 = Math.Round(Convert.ToDouble(txttotnet.Text), 2).ToString("########.00");
                    string[] words = s1.Split('.');
                    string str = sh.changeToWords(words[0]);
                    string str1 = sh.changeToWords(words[1]);
                    if (str1 == " " || str1 == null || words[1] == "00")
                    {
                        str1 = "Zero ";
                    }
                    String inword = "" + str + "and " + str1 + "Paise Only";

                    DialogResult dr1 = MessageBox.Show("Do you want to Print Quick Payment Receipt?", "Quick Payment", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr1 == DialogResult.Yes)
                    {

                        prndata.execute("delete from printing");
                        string custname = LVFO.Items[LVFO.FocusedItem.Index].SubItems[1].Text;
                        DataTable clientID = con.getdataset("select * from clientmaster where isactive=1 and AccountName ='" + custname + "'");
                        int customerid = int.Parse(clientID.Rows[0][0].ToString());
                        DataTable client = con.getdataset("select * from clientmaster where isactive=1 and clientID='" + customerid + "'");
                        DataTable dt1 = con.getdataset("select * from company WHERE isactive=1");


                        string date = "", billno = "", Amount = "", Discount = "", Remarks = "", chequeNo = "", chequeDate = "", BankName = "", AmountInWord = "";//type = "", Account = "", drAmount = "", crAmount = "", balance = "";
                        date = DTDate.Text;
                        billno = LVFO.Items[LVFO.FocusedItem.Index].SubItems[0].Text;
                        Amount = LVFO.Items[LVFO.FocusedItem.Index].SubItems[2].Text;
                        Discount = LVFO.Items[LVFO.FocusedItem.Index].SubItems[3].Text;
                        Remarks = LVFO.Items[LVFO.FocusedItem.Index].SubItems[5].Text; ;
                        AmountInWord = inword;
                        if (cmbpaymode.SelectedIndex > 0)
                        {
                            chequeNo = txtchqno.Text;
                            chequeDate = txtchqdate.Text;
                            BankName = txtbankname.Text;
                        }
                        DataTable dtp = con.getdataset("select * from paymentreceipt where isactive='1' and type='P' and recno='" + billno + "'");
                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34,T35,T36,T37,T38,T39,T40,T41,T42,T43,T44,T45,T46,T47,T48,T49,T50,T51,T52)VALUES";
                        qry += "('" + dt1.Rows[0][0].ToString() + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][2].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + dt1.Rows[0][11].ToString() + "','" + dt1.Rows[0][12].ToString() + "','" + dt1.Rows[0][13].ToString() + "','" + client.Rows[0][0].ToString() + "','" + client.Rows[0][1].ToString() + "','" + client.Rows[0][2].ToString() + "','" + client.Rows[0][3].ToString() + "','" + client.Rows[0][4].ToString() + "','" + client.Rows[0][5].ToString() + "','" + client.Rows[0][6].ToString() + "','" + client.Rows[0][7].ToString() + "','" + client.Rows[0][8].ToString() + "','" + client.Rows[0][9].ToString() + "','" + client.Rows[0][10].ToString() + "','" + client.Rows[0][11].ToString() + "','" + client.Rows[0][12].ToString() + "','" + client.Rows[0][13].ToString() + "','" + date + "','" + billno + "','" + Amount + "','" + Discount + "','" + Remarks + "','" + chequeNo + "','" + chequeDate + "','" + BankName + "','" + AmountInWord + "','" + dtp.Rows[0]["OT1"].ToString() + "','" + dtp.Rows[0]["OT2"].ToString() + "','" + dtp.Rows[0]["OT3"].ToString() + "','" + dtp.Rows[0]["OT4"].ToString() + "','" + dtp.Rows[0]["OT5"].ToString() + "','" + dtp.Rows[0]["OT6"].ToString() + "','" + dtp.Rows[0]["OT7"].ToString() + "','" + dtp.Rows[0]["OT8"].ToString() + "','" + dtp.Rows[0]["OT9"].ToString() + "','" + dtp.Rows[0]["OT10"].ToString() + "','" + dtp.Rows[0]["OT11"].ToString() + "','" + dtp.Rows[0]["OT12"].ToString() + "','" + dtp.Rows[0]["OT13"].ToString() + "','" + dtp.Rows[0]["OT14"].ToString() + "','" + dtp.Rows[0]["OT15"].ToString() + "')";
                        prndata.execute(qry);

                        string reportName = "QuickPayment";
                        //  string reportName = "Sale";
                        Print popup = new Print(reportName);
                        popup.ShowDialog();
                        popup.Dispose();

                    }
                }
                else
                {
                    MessageBox.Show("No Records Select For Print", "Quick Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch
            {
            }
        }

        private void cmbpaymode_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbpaymode.SelectedIndex = 0;
                cmbpaymode.DroppedDown = true;
                //cmbpaymode.ForeColor = Color.LightYellow;
            }
            catch
            {
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Delete Payment?", "Payment", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    if (LVFO.SelectedItems.Count > 0)
                    {
                        // LVFO.Items[0].Selected = true;
                        string billno = LVFO.Items[LVFO.FocusedItem.Index].SubItems[0].Text;
                        if (billno != "" && billno != null)
                        {
                            //   string name;
                            con.execute("UPDATE [dbo].[paymentreceipt] SET  [isactive]=0 WHERE [recno] = '" + billno + "' and type='P'");
                            con.execute("UPDATE [dbo].[Ledger] SET [isactive]=0  where [VoucherID]= '" + billno + "' and [TranType] = 'Pmnt'");
                            con.execute("UPDATE [dbo].[Ref] SET  [isactive]=0 WHERE [VchNo] = '" + billno + "' and TransactionType='P'");
                            LVFO.Items.Clear();
                            DataTable dt1 = con.getdataset("select * from paymentreceipt where isactive='1' and type='P' and [date] = '" + Convert.ToDateTime(DTDate.Text).ToString("MM-dd-yyyy") + "' and [mode] = '" + cmbpaymode.Text + "'");
                            for (int i = 0; i < dt1.Rows.Count; i++)
                            {
                                ListViewItem li;
                                li = LVFO.Items.Add(dt1.Rows[i][1].ToString());
                                string str = data.getclientname(dt1.Rows[i][8].ToString());
                                li.SubItems.Add(str);
                                li.SubItems.Add(dt1.Rows[i][9].ToString());
                                li.SubItems.Add(dt1.Rows[i][10].ToString());
                                li.SubItems.Add(dt1.Rows[i][11].ToString());
                                // li.SubItems.Add(txtaqty.Text);
                                li.SubItems.Add(dt1.Rows[i][12].ToString());

                                //   li.SubItems.Add(txtamount.Text);

                            }
                            total();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Select Payment Record..", "Payment", MessageBoxButtons.OK);
                    }
                }
            }
            catch
            {
            }
        }

        private void txtamt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtdiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtnetamt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtrecno_Enter(object sender, EventArgs e)
        {
            txtrecno.BackColor = Color.LightYellow;
        }

        private void txtrecno_Leave(object sender, EventArgs e)
        {
            txtrecno.BackColor = Color.White;
        }

        private void cmbaccname_Enter(object sender, EventArgs e)
        {
            //cmbaccname.ForeColor = Color.LightYellow;
        }

        private void cmbaccname_Leave(object sender, EventArgs e)
        {
            cmbaccname.Text = s;
            //   cmbaccname.ForeColor = Color.White;
        }

        private void txtchqno_Enter(object sender, EventArgs e)
        {
            txtchqno.BackColor = Color.LightYellow;
        }

        private void txtchqno_Leave(object sender, EventArgs e)
        {
            txtchqno.BackColor = Color.White;
        }

        private void txtchqdate_Enter(object sender, EventArgs e)
        {
            txtchqdate.BackColor = Color.LightYellow;
        }

        private void txtchqdate_Leave(object sender, EventArgs e)
        {
            txtchqdate.BackColor = Color.White;
        }

        private void txtbankname_Enter(object sender, EventArgs e)
        {
            txtbankname.BackColor = Color.LightYellow;
        }

        private void txtbankname_Leave(object sender, EventArgs e)
        {
            txtbankname.BackColor = Color.White;
        }

        private void txtamt_Enter(object sender, EventArgs e)
        {
            txtamt.BackColor = Color.LightYellow;
        }

        private void txtamt_Leave(object sender, EventArgs e)
        {
            txtamt.BackColor = Color.White;
        }

        private void txtdiscount_Enter(object sender, EventArgs e)
        {
            txtdiscount.BackColor = Color.LightYellow;
        }

        private void txtdiscount_Leave(object sender, EventArgs e)
        {
            txtdiscount.BackColor = Color.White;
        }

        private void txtnetamt_Enter(object sender, EventArgs e)
        {
            txtnetamt.BackColor = Color.LightYellow;
        }

        private void txtnetamt_Leave(object sender, EventArgs e)
        {
            txtnetamt.BackColor = Color.White;
        }

        private void txtremark_Enter(object sender, EventArgs e)
        {
            txtremark.BackColor = Color.LightYellow;
        }

        private void txtremark_Leave(object sender, EventArgs e)
        {
            txtremark.BackColor = Color.White;
        }

        private void cmbpaymode_Leave(object sender, EventArgs e)
        {
            cmbpaymode.Text = s;
            //cmbpaymode.ForeColor = Color.White;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
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

        private void btnsave_MouseEnter(object sender, EventArgs e)
        {
            btnsave.UseVisualStyleBackColor = false;
            btnsave.BackColor = Color.YellowGreen;
            btnsave.ForeColor = Color.White;
        }

        private void btnsave_MouseLeave(object sender, EventArgs e)
        {
            btnsave.UseVisualStyleBackColor = true;
            btnsave.BackColor = Color.FromArgb(51, 153, 255);
            btnsave.ForeColor = Color.White;
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = false;
            btnClose.BackColor = Color.FromArgb(248, 152, 94);
            btnClose.ForeColor = Color.White;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = true;
            btnClose.BackColor = Color.FromArgb(51, 153, 255);
            btnClose.ForeColor = Color.White;
        }

        private void btnAdd_MouseEnter(object sender, EventArgs e)
        {
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.BackColor = Color.FromArgb(9, 106, 3);
            btnAdd.ForeColor = Color.White;
        }

        private void btnAdd_MouseLeave(object sender, EventArgs e)
        {
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.BackColor = Color.FromArgb(51, 153, 255);
            btnAdd.ForeColor = Color.White;
        }

        private void btnsave_Enter(object sender, EventArgs e)
        {
            btnsave.UseVisualStyleBackColor = false;
            btnsave.BackColor = Color.YellowGreen;
            btnsave.ForeColor = Color.White;
        }

        private void btnsave_Leave(object sender, EventArgs e)
        {
            btnsave.UseVisualStyleBackColor = true;
            btnsave.BackColor = Color.FromArgb(51, 153, 255);
            btnsave.ForeColor = Color.White;
        }

        private void btnClose_Enter(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = false;
            btnClose.BackColor = Color.FromArgb(248, 152, 94);
            btnClose.ForeColor = Color.White;
        }

        private void btnClose_Leave(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = true;
            btnClose.BackColor = Color.FromArgb(51, 153, 255);
            btnClose.ForeColor = Color.White;
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

        private void btnAdd_Enter(object sender, EventArgs e)
        {
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.BackColor = Color.FromArgb(9, 106, 3);
            btnAdd.ForeColor = Color.White;
        }

        private void btnAdd_Leave(object sender, EventArgs e)
        {
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.BackColor = Color.FromArgb(51, 153, 255);
            btnAdd.ForeColor = Color.White;
        }
        string searchstr;
        private void cmbpaymode_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbpaymode.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbpaymode.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //empty the string for every 1 seconds
            // searchstr = "";
        }

        private void cmbaccname_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbaccname.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbaccname.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void btnGroupEdit_Click(object sender, EventArgs e)
        {
            if (cmbaccname.Text != "" && cmbaccname.Text != null)
            {
                string accname = cmbaccname.Text;
                DataTable clientID = con.getdataset("select * from ClientMaster where AccountName='" + accname + "'");
                if (clientID.Rows.Count > 0)
                {
                    var privouscontroal = cmbaccname;
                    activecontroal = privouscontroal.Name;
                    int accountid = int.Parse(cmbaccname.SelectedValue.ToString());
                    Accountentry dlg = new Accountentry(this, master, tabControl, activecontroal);
                    dlg.Update(accountid);
                    master.AddNewTab(dlg);
                    this.ActiveControl = cmbaccname;
                }
                else
                {
                    MessageBox.Show("Please Enter/Select Valid Account Name ");
                }
            }
            else
            {
                MessageBox.Show("Please select Account Name");
            }
            bindcustomer();
        }

        private void DTDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void LVFO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                open();
            }
        }

        private void txtf1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "1")
                {
                    txtnetamt.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf2.Focus();
                }
            }
        }

        private void txtf2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "2")
                {
                    txtnetamt.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf3.Focus();
                }
            }
        }

        private void txtf3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "3")
                {
                    txtnetamt.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf4.Focus();
                }
            }
        }

        private void txtf4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "4")
                {
                    txtnetamt.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf5.Focus();
                }
            }
        }

        private void txtf5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "5")
                {
                    txtnetamt.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf6.Focus();
                }
            }
        }

        private void txtf6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "6")
                {
                    txtnetamt.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf7.Focus();
                }
            }
        }

        private void txtf7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "7")
                {
                    txtnetamt.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf8.Focus();
                }
            }
        }

        private void txtf8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "8")
                {
                    txtnetamt.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf9.Focus();
                }
            }
        }

        private void txtf9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "9")
                {
                    txtnetamt.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf10.Focus();
                }
            }
        }

        private void txtf10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "10")
                {
                    txtnetamt.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf11.Focus();
                }
            }
        }

        private void txtf11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "11")
                {
                    txtnetamt.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf12.Focus();
                }
            }
        }

        private void txtf12_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "12")
                {
                    txtnetamt.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf13.Focus();
                }
            }
        }

        private void txtf13_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "13")
                {
                    txtnetamt.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf4.Focus();
                }
            }
        }

        private void txtf14_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "14")
                {
                    txtnetamt.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf15.Focus();
                }
            }
        }

        private void txtf15_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtnetamt.Focus();
                pnladditional.Visible = false;
            }
        }


    }
}
