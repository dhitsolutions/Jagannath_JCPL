using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;

namespace RamdevSales
{
    public partial class PurchaseReturn : Form
    {
        public PurchaseReturn()
        {
            InitializeComponent();
        }
        
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        OleDbSettings ods = new OleDbSettings();
        SqlConnection con;
        Connection conn = new Connection();
        DataSet ds = new DataSet();
        DataTable dt,dt1 = new DataTable();
        Printing prn = new Printing();
        private PurchaseReturnList purchaseReturnList;
        public void getcon()
        {
            ds = ods.getdata("Select * from SQLSetting where DBName='Local'");
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {

                string qry = dt.Rows[0][6].ToString();
                con = new SqlConnection(qry);

            }
            else
            {
                AddConString frm = new AddConString();
                frm.Show();
            }
        }

        public PurchaseReturn(PurchaseReturnList purchaseReturnList)
        {
            InitializeComponent();
            this.purchaseReturnList = purchaseReturnList;
        }

        private void PurchaseReturn_Load(object sender, EventArgs e)
        {
            try
            {
                if (cnt == 0)
                {
                    loadpage();
                }
            }
            catch
            {

            }
        }

        private void loadpage()
        {
            
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            TxtBillDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            this.ActiveControl = txtVchNo;
            
            LVFO.Columns.Add("Description of Goods", 300, HorizontalAlignment.Left);
            LVFO.Columns.Add("Qty", 60, HorizontalAlignment.Right);
            LVFO.Columns.Add("Bags", 90, HorizontalAlignment.Right);
            LVFO.Columns.Add("Rate", 100, HorizontalAlignment.Right);
            LVFO.Columns.Add("Per", 100, HorizontalAlignment.Center);
            LVFO.Columns.Add("Basic Amount", 125, HorizontalAlignment.Center);
            LVFO.Columns.Add("Discount(%)", 100, HorizontalAlignment.Center);
            LVFO.Columns.Add("Discount", 100, HorizontalAlignment.Center);
            LVFO.Columns.Add("Vat", 100, HorizontalAlignment.Center);
            LVFO.Columns.Add("Add Vat", 100, HorizontalAlignment.Center);
            LVFO.Columns.Add("Total", 100, HorizontalAlignment.Center);
            if (btnPayment.Text == "&Save")
            {

                getsr();
            }
            bindsaletype();
            bindcustomer();
            autoreaderbind();

        }

        void getsr()
        {
            try
            {

                dt = conn.getdataset("select max(Bill_No) from BillMaster where isactive='1' and billtype='PR'");
                String str = "";
                
                int id, count=0;
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        str = dt.Rows[0][0].ToString();
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
                    }
                    else
                    {
                        id = 1;
                        count = 1;
                    }
                }
                else
                {
                    id = 1;
                    count = 1;
                }
                
                txtVchNo.Text = count.ToString();

            }
            catch
            {
            }
            finally
            {

            }

        }

        public void bindsaletype()
        {
            dt = conn.getdataset("select purchasetypeid,purchasetypename from PurchasetypeMaster");
            
            cmbPurchaseType.ValueMember = "purchasetypeid";
            cmbPurchaseType.DisplayMember = "purchasetypename";
            cmbPurchaseType.DataSource = dt;
            cmbPurchaseType.SelectedIndex = -1;

            autobind(dt, cmbPurchaseType);
        }

        public void bindcustomer()
        {
            dt1 = conn.getdataset("select ClientID,AccountName from ClientMaster order by AccountName");
           
            cmbCustName.ValueMember = "ClientID";
            cmbCustName.DisplayMember = "AccountName";
            cmbCustName.DataSource = dt1;
            cmbCustName.SelectedIndex = -1;


            autobind(dt1, cmbCustName);
        }

        public void autoreaderbind()
        {
            try
            {
                getcon();
                AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();


                SqlDataReader dReader;
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = con;
                cmd1.CommandType = CommandType.Text;

                //start string

                String qry = "select ProductMaster.Product_Name from ProductMaster";
                //  con.Open();
                int count = 0;

                con.Close();
                qry = qry + " order by ProductMaster.Product_Name";

                if (count == 0)
                {
                    //end string
                    cmd1.CommandText = qry;


                    con.Open();
                    dReader = cmd1.ExecuteReader();

                    if (dReader.HasRows == true)
                    {
                        while (dReader.Read())
                            namesCollection.Add(dReader["Product_Name"].ToString());

                    }
                    else
                    {
                        MessageBox.Show("Data not found");
                    }
                    dReader.Close();

                    txtItemName.AutoCompleteMode = AutoCompleteMode.Suggest;
                    txtItemName.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    txtItemName.AutoCompleteCustomSource = namesCollection;
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
                            namesCollection.Add(dReader["Product_Name"].ToString());

                    }
                    else
                    {
                        MessageBox.Show("Data not found");
                    }
                    dReader.Close();

                    txtItemName.AutoCompleteMode = AutoCompleteMode.Suggest;
                    txtItemName.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    txtItemName.AutoCompleteCustomSource = namesCollection;
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

        private void autobind(DataTable dt1, ComboBox cmbcustname)
        {
            string[] arr = new string[dt1.Rows.Count];
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                arr[i] = dt1.Rows[i][1].ToString();
            }

            cmbcustname.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cmbcustname.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbcustname.AutoCompleteCustomSource.AddRange(arr);
        }

        private void btnCalculator_Click(object sender, EventArgs e)
        {
            Process p = null;
            if (p == null)
            {
                p = new Process();
                p.StartInfo.FileName = "Calc.exe";
                p.Start();

            }
            else
            {
                p.Close();
                p.Dispose();

            }
        }

        public void clearitem()
        {
            txtItemName.Text = string.Empty;
            txtQty.Text = string.Empty;
            txtFree.Text = string.Empty;
            txtRate.Text = string.Empty;
            txtPer.Text = string.Empty;
            txtBasicAmount.Text = string.Empty;
            txtDiscount.Text = string.Empty;
            txtDiscountPer.Text = string.Empty;
            txtVat.Text = string.Empty;
            txtAddVat.Text = string.Empty;
            txtAmount.Text = string.Empty;
        }

        public void clearall()
        {
            getsr();
            TxtBillDate.Text = DateTime.Now.ToShortDateString();
            TxtBillNo.Text = "";
            cmbTerms.SelectedIndex = -1;
            cmbCustName.SelectedIndex = -1;
            cmbPurchaseType.SelectedIndex = -1;
            
            lbltotpqty.Text = string.Empty;
            lbltaxtot.Text = string.Empty;
            TxtBillTotal.Text = string.Empty;

            lbltotcount.Text = "0";
            lbltotpqty.Text = "0";
            lblbasictot.Text = "0";
            lbltaxtot.Text = "0";
            txtaddtax.Text = "0";
            lbltotaldiscount.Text = "0";

            txtdispatch.Text = string.Empty;
            txtremarks.Text = string.Empty;
        }

        private void BtnPayment_Click(object sender, EventArgs e)
        {
            btnsubmit();
        }

        private void btnsubmit()
        {
            try
            {
                //if (cmbCustName.Text == "")
                //{
                //    MessageBox.Show("Select Party Name");
                //    cmbCustName.Focus();
                //}
                //else if (cmbPurchaseType.Text == "")
                //{
                //    MessageBox.Show("Select Sale type");
                //    cmbPurchaseType.Focus();
                //}
                //else if (LVFO.Items.Count == 0)
                //{
                //    MessageBox.Show("Please Enter atleast one item to generate sale return");
                //    txtItemName.Focus();
                //}
                //else
                //{
                DataTable dtpid = new DataTable();
                    getcon();
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    DialogResult dr = MessageBox.Show("Do you want to Generate Bill?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        if (cmbPurchaseType.Text == "" || cmbCustName.Text == "" || cmbTerms.Text == "")
                        {
                            MessageBox.Show("Please fill all the information.");
                            if (cmbTerms.Text == "")
                            {
                                cmbTerms.Focus();
                            }

                            else if (cmbCustName.Text == "")
                            {
                                cmbCustName.Focus();
                            }
                            else if (cmbPurchaseType.Text == "")
                            {
                                cmbPurchaseType.Focus();
                            }

                        }
                        else
                        {
                            if (btnPayment.Text == "Update")
                            {
                                SqlCommand cmd2 = new SqlCommand("Update billproductmaster set isactive='0' where Bill_No='" + txtVchNo.Text + "' and BillType='PR'", con);
                                cmd2.ExecuteNonQuery();
                                for (int i = 0; i < LVFO.Items.Count; i++)
                                {
                                    dtpid = conn.getdataset("Select Productid from productmaster where product_name='" + LVFO.Items[i].SubItems[0].Text.Replace(",", "") + "'");

                                    SqlCommand cmd1 = new SqlCommand("INSERT INTO [BillProductMaster]([Bill_No],[Bill_Run_Date],[ProductName],[Pqty],[Bags],[Rate],[Per],[Amount],[DiscountPer],[Discountamt],[Tax],[Addtax],[Total],[isactive],[BillType],[productid])VALUES('" + txtVchNo.Text + "','" + Convert.ToDateTime(TxtBillDate.Text).ToString("dd/MMM/yyyy") + "','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','" + LVFO.Items[i].SubItems[2].Text + "','" + LVFO.Items[i].SubItems[3].Text + "','" + LVFO.Items[i].SubItems[4].Text + "','" + LVFO.Items[i].SubItems[5].Text + "','" + LVFO.Items[i].SubItems[6].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[7].Text + "','" + LVFO.Items[i].SubItems[8].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[9].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[10].Text.Replace(",", "") + "',1,'PR','"+dtpid.Rows[0][0].ToString()+"')", con);
                                    cmd1.ExecuteNonQuery();

                                }
                                SqlCommand cmd = new SqlCommand("UPDATE [BillMaster] SET [Bill_No] = '" + txtVchNo.Text + "',[Bill_Date] = '" + Convert.ToDateTime(TxtBillDate.Text).ToString("dd/MMM/yyyy") + "',[Terms] = '" + cmbTerms.Text + "',[ClientID] = '" + cmbCustName.SelectedValue + "',[SaleType] = '" + cmbPurchaseType.SelectedValue + "',[Count] = " + lbltotcount.Text + ",[TotalQty] = " + lbltotpqty.Text + ",[TotalBasic] = " + lblbasictot.Text + ",[TotalTax] =" + lbltaxtot.Text + " ,[TotalAddTax] =" + txtaddtax.Text + " ,[TotalDiscount] =" + lbltotaldiscount.Text + " ,[TotalNet] = " + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + ",[isactive]=1,[DispatchDetails]='" + txtdispatch.Text + "',[Remarks]='" + txtremarks.Text + "',[billtype]='PR',[CompanyId]=" + Master.companyId + " where Bill_No='" + txtVchNo.Text + "' and [billtype]='PR' and [CompanyId]=" + Master.companyId + "", con);
                                cmd.ExecuteNonQuery();
                                conn.execute("UPDATE [Ledger] SET [Date1]='" + Convert.ToDateTime(TxtBillDate.Text).ToString("dd/MMM/yyyy") + "',[TranType] = 'PurchaseReturn',[AccountID] = '" + cmbCustName.SelectedValue + "',[AccountName]='" + cmbCustName.Text + "' ,[Amount] = '" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "',[DC] = 'D',[CompanyId]=" + Master.companyId + " where [VoucherID]= '" + txtVchNo.Text + "' and [TranType] = 'PurchaseReturn' and CompanyId=" + Master.companyId + "");

                                ChangeNumbersToWords sh = new ChangeNumbersToWords();
                                String s1 = Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00");
                                string[] words = s1.Split('.');


                                string str = sh.changeToWords(words[0]);
                                string str1 = sh.changeToWords(words[1]);
                                if (str1 == " " || str1 == null || words[1] == "00")
                                {
                                    str1 = "Zero ";
                                }
                                String inword = "In words: " + str + "and " + str1 + "Paise Only";
                                //Phrase inwrd = new Phrase(inword, _font2);

                                DataTable client = conn.getdataset("select * from clientmaster where isactive=1 and clientID='" + cmbCustName.SelectedValue + "'");
                                DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1");
                                prn.execute("delete from printing");
                                int j = 1;

                                for (int i = 0; i < LVFO.Items.Count; i++)
                                {
                                    try
                                    {
                                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34,T35,T36,T37,T38,T39,T40,T41,T42,T43,T44,T45,T46,T47,T48,T49,T50,T51,T52,T53,T54,T55,T56,T57,T58,T59,T60,T61,T62,T63,T64,T65,T66,T67,T68,T69,T70,T71,T72,T73,T74,T75,T76,T77,T78,T79,T80)VALUES";
                                        qry += "('" + j++ + "','" + TxtBillNo.Text + "','" + TxtBillDate.Text + "','" + cmbTerms.Text + "','0','" + cmbCustName.Text + "','0','" + cmbPurchaseType.Text + "','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','" + LVFO.Items[i].SubItems[2].Text + "','" + LVFO.Items[i].SubItems[3].Text + "','" + LVFO.Items[i].SubItems[4].Text + "','" + LVFO.Items[i].SubItems[5].Text + "','" + LVFO.Items[i].SubItems[6].Text + "','" + LVFO.Items[i].SubItems[7].Text + "','" + LVFO.Items[i].SubItems[8].Text + "','" + LVFO.Items[i].SubItems[9].Text + "','" + LVFO.Items[i].SubItems[10].Text + "','" + LVFO.Items[i].SubItems[11].Text + "','" + LVFO.Items[i].SubItems[12].Text + "','" + LVFO.Items[i].SubItems[13].Text + "','" + LVFO.Items[i].SubItems[14].Text + "','" + lbltotcount.Text + "','" + lbltotpqty.Text + "','0','0','" + lblbasictot.Text + "','0','0','0','0','0','0','0','0','" + TxtBillTotal.Text + "','" + dt1.Rows[0][0].ToString() + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][2].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + dt1.Rows[0][11].ToString() + "','" + dt1.Rows[0][12].ToString() + "','" + dt1.Rows[0][13].ToString() + "','" + inword + "','0','0','0','0','0','" + txtremarks.Text + "','0','0','0','0','0','0','0','0','" + client.Rows[0][0].ToString() + "','" + client.Rows[0][1].ToString() + "','" + client.Rows[0][2].ToString() + "','" + client.Rows[0][3].ToString() + "','" + client.Rows[0][4].ToString() + "','" + client.Rows[0][5].ToString() + "','" + client.Rows[0][6].ToString() + "','" + client.Rows[0][7].ToString() + "','" + client.Rows[0][8].ToString() + "','" + client.Rows[0][9].ToString() + "','" + client.Rows[0][10].ToString() + "','" + client.Rows[0][11].ToString() + "','" + client.Rows[0][12].ToString() + "','" + client.Rows[0][13].ToString() + "')";
                                        prn.execute(qry);
                                    }
                                    catch
                                    {
                                    }
                                }
                                Print popup = new Print("Purchase Return");
                                popup.ShowDialog();
                                popup.Dispose();

                                clearitem();
                                clearall();
                                LVFO.Items.Clear();
                                MessageBox.Show("Data Updated Successfully.");

                                btnPayment.Text = "Save";
                                this.Hide();
                            }
                            else
                            {
                                getsr();
                                for (int i = 0; i < LVFO.Items.Count; i++)
                                {

                                    dtpid = conn.getdataset("Select Productid from productmaster where product_name='" + LVFO.Items[i].SubItems[0].Text.Replace(",", "") + "'");

                                    SqlCommand cmd1 = new SqlCommand("INSERT INTO [BillProductMaster]([Bill_No],[Bill_Run_Date],[ProductName],[Pqty],[Bags],[Rate],[Per],[Amount],[DiscountPer],[Discountamt],[Tax],[Addtax],[Total],[isactive],[BillType],[productid])VALUES('" + txtVchNo.Text + "','" + Convert.ToDateTime(TxtBillDate.Text).ToString("dd/MMM/yyyy") + "','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','" + LVFO.Items[i].SubItems[2].Text + "','" + LVFO.Items[i].SubItems[3].Text + "','" + LVFO.Items[i].SubItems[4].Text + "','" + LVFO.Items[i].SubItems[5].Text + "','" + LVFO.Items[i].SubItems[6].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[7].Text + "','" + LVFO.Items[i].SubItems[8].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[9].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[10].Text.Replace(",", "") + "',1,'PR','" + dtpid.Rows[0][0].ToString() + "')", con);
                                    //SqlCommand cmd1 = new SqlCommand("INSERT INTO [BillProductMaster]([VchNo],[BillNo],[BillRunDate],[ProductName],[Qty],[Free],[Rate],[Per],[BasicAmount],[DiscountPer],[Discount],[Vat],[AddVat],[Total],[isactive],[BillType])VALUES('" + txtVchNo.Text + "','"+TxtBillNo.Text+"','" + Convert.ToDateTime(TxtBillDate.Text).ToString("dd/MMM/yyyy") + "','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','" + LVFO.Items[i].SubItems[2].Text + "','" + LVFO.Items[i].SubItems[3].Text + "','" + LVFO.Items[i].SubItems[4].Text + "','" + LVFO.Items[i].SubItems[5].Text + "','" + LVFO.Items[i].SubItems[6].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[7].Text + "','" + LVFO.Items[i].SubItems[8].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[9].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[10].Text.Replace(",", "") + "',1,'PR')", con);
                                    cmd1.ExecuteNonQuery();

                                }
                                SqlCommand cmd = new SqlCommand("INSERT INTO [BillMaster]([Bill_No],[Bill_Date],[Terms],[ClientID],[SaleType],[Count],[TotalQty],[TotalBasic],[TotalTax],[TotalAddTax],[TotalDiscount],[TotalNet],[isactive],[DispatchDetails],[Remarks],[BillType],[CompanyId])VALUES('" + txtVchNo.Text + "','" + Convert.ToDateTime(TxtBillDate.Text).ToString("dd/MMM/yyyy") + "','" + cmbTerms.Text + "','" + cmbCustName.SelectedValue + "','" + cmbPurchaseType.SelectedValue + "'," + lbltotcount.Text + "," + lbltotpqty.Text + "," + lblbasictot.Text + "," + lbltaxtot.Text + "," + txtaddtax.Text + "," + lbltotaldiscount.Text + "," + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + ",1,'" + txtdispatch.Text + "','" + txtremarks.Text + "','PR'," + Master.companyId + ")", con);
                                cmd.ExecuteNonQuery();

                                conn.execute("INSERT INTO [Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[CompanyId],[isActive]) values ('" + txtVchNo.Text + "','" + Convert.ToDateTime(TxtBillDate.Text).ToString("dd/MMM/yyyy") + "','PurchaseReturn','" + cmbCustName.SelectedValue + "','" + cmbCustName.Text + "','" + TxtBillTotal.Text + "','D'," + Master.companyId + ",1)");

                                ChangeNumbersToWords sh = new ChangeNumbersToWords();
                                String s1 = Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00");
                                string[] words = s1.Split('.');


                                string str = sh.changeToWords(words[0]);
                                string str1 = sh.changeToWords(words[1]);
                                if (str1 == " " || str1 == null || words[1] == "00")
                                {
                                    str1 = "Zero ";
                                }
                                String inword = "In words: " + str + "and " + str1 + "Paise Only";

                                DataTable client = conn.getdataset("select * from clientmaster where isactive=1 and clientID='" + cmbCustName.SelectedValue + "'");
                                DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1");
                                prn.execute("delete from printing");
                                int j = 1;

                                for (int i = 0; i < LVFO.Items.Count; i++)
                                {
                                    try
                                    {
                                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34,T35,T36,T37,T38,T39,T40,T41,T42,T43,T44,T45,T46,T47,T48,T49,T50,T51,T52,T53,T54,T55,T56,T57,T58,T59,T60,T61,T62,T63,T64,T65,T66,T67,T68,T69,T70,T71,T72,T73,T74,T75,T76,T77,T78,T79,T80)VALUES";
                                        qry += "('" + j++ + "','" + TxtBillNo.Text + "','" + TxtBillDate.Text + "','" + cmbTerms.Text + "','0','" + cmbCustName.Text + "','0','" + cmbPurchaseType.Text + "','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','" + LVFO.Items[i].SubItems[2].Text + "','" + LVFO.Items[i].SubItems[3].Text + "','" + LVFO.Items[i].SubItems[4].Text + "','" + LVFO.Items[i].SubItems[5].Text + "','" + LVFO.Items[i].SubItems[6].Text + "','" + LVFO.Items[i].SubItems[7].Text + "','" + LVFO.Items[i].SubItems[8].Text + "','" + LVFO.Items[i].SubItems[9].Text + "','" + LVFO.Items[i].SubItems[10].Text + "','" + LVFO.Items[i].SubItems[11].Text + "','" + LVFO.Items[i].SubItems[12].Text + "','" + LVFO.Items[i].SubItems[13].Text + "','" + LVFO.Items[i].SubItems[14].Text + "','" + lbltotcount.Text + "','" + lbltotpqty.Text + "','0','0','" + lblbasictot.Text + "','0','0','0','0','0','0','0','0','" + TxtBillTotal.Text + "','" + dt1.Rows[0][0].ToString() + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][2].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + dt1.Rows[0][11].ToString() + "','" + dt1.Rows[0][12].ToString() + "','" + dt1.Rows[0][13].ToString() + "','" + inword + "','0','0','0','0','0','" + txtremarks.Text + "','0','0','0','0','0','0','0','0','" + client.Rows[0][0].ToString() + "','" + client.Rows[0][1].ToString() + "','" + client.Rows[0][2].ToString() + "','" + client.Rows[0][3].ToString() + "','" + client.Rows[0][4].ToString() + "','" + client.Rows[0][5].ToString() + "','" + client.Rows[0][6].ToString() + "','" + client.Rows[0][7].ToString() + "','" + client.Rows[0][8].ToString() + "','" + client.Rows[0][9].ToString() + "','" + client.Rows[0][10].ToString() + "','" + client.Rows[0][11].ToString() + "','" + client.Rows[0][12].ToString() + "','" + client.Rows[0][13].ToString() + "')";
                                        prn.execute(qry);
                                    }
                                    catch
                                    {
                                    }
                                }
                                Print popup = new Print("Purchase Return");
                                popup.ShowDialog();
                                popup.Dispose();

                                clearitem();
                                clearall();
                                LVFO.Items.Clear();
                                this.Hide();

                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("please fill all information");
                    }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void txtamount_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtItemName.Text == "" || txtItemName.Text == null)
            {
                MessageBox.Show("Enter Item Name.");
                txtItemName.Focus();
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    ListViewItem li;
                    li = LVFO.Items.Add(txtItemName.Text);
                    li.SubItems.Add((Math.Round(Convert.ToDouble(txtQty.Text), 2).ToString()));
                    li.SubItems.Add(txtFree.Text);
                    li.SubItems.Add((Math.Round(Convert.ToDouble(txtRate.Text), 2).ToString()));
                    li.SubItems.Add(txtPer.Text);
                    li.SubItems.Add((Math.Round(Convert.ToDouble(txtBasicAmount.Text), 2).ToString()));
                    li.SubItems.Add(txtDiscountPer.Text);
                    li.SubItems.Add((Math.Round(Convert.ToDouble(txtDiscount.Text), 2).ToString()));
                    li.SubItems.Add((Math.Round(Convert.ToDouble(txtVat.Text), 2).ToString()));
                    li.SubItems.Add((Math.Round(Convert.ToDouble(txtAddVat.Text), 2).ToString()));
                    li.SubItems.Add((Math.Round(Convert.ToDouble(txtAmount.Text), 2).ToString()));

                    totalcalculation();
                    clearitem();
                    txtItemName.Focus();
                }
            }
        }

        private void txtitemname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                getcon();
                if (e.KeyCode == Keys.Enter)
                {
                    SqlCommand cmd5 = new SqlCommand("select p.Product_Name, p.Unit, b.* from productmaster p inner join ProductPriceMaster b on p.ProductID=b.ProductID where p.product_name='" + txtItemName.Text + "' and p.CompanyID="+Master.companyId+"", con);
                    
                    SqlDataAdapter sda = new SqlDataAdapter(cmd5);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {

                        txtRate.Text = dt.Rows[0]["PurchasePrice"].ToString();
                        txtPer.Text = dt.Rows[0]["Unit"].ToString();
                    }
                    else
                    {
                        txtRate.Text = "0";
                        txtPer.Text = "QTY";
                    }
                    flag = 1;
                    txtDiscount.Text = "0.00";
                    txtDiscountPer.Text = "0.00";
                    flag = 0;
                    txtQty.Text = "1";
                    
                    SqlCommand cmd6 = new SqlCommand("select i.Vat,i.addvat from itemtaxmaster i inner join productmaster p on i.productid=p.productid where p.product_name like'%" + txtItemName.Text + "%' and i.saletypeid like '%" + cmbPurchaseType.Text + "'", con);
                    SqlDataAdapter sda6 = new SqlDataAdapter(cmd6);
                    DataTable dt1 = new DataTable();
                    sda6.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        txtVat.Text = dt1.Rows[0]["Vat"].ToString();
                        txtAddVat.Text = dt1.Rows[0]["addVat"].ToString();
                        txtQty.Focus();
                        itemcalculation(txtQty.Text);
                    }
                    else
                    {
                        MessageBox.Show("Not any Tax Available For This Sale Type");
                        txtVat.Text = "0";
                        txtAddVat.Text = "0";
                        txtQty.Focus();
                    }


                }
                if (e.KeyCode == Keys.F3)
                {
                    //Itementry client = new Itementry(this);
                    //client.Passed(1);
                    //client.Show();
                }
                if (e.KeyCode == Keys.F2)
                {
                    if (txtItemName.Text != "")
                    {
                        //Itementry client = new Itementry(this);
                        //client.Updatefromsale(txtItemName.Text);
                        //client.Show();
                    }
                }
            }
            catch
            {
            }
        }

        private void itemcalculation(String qty)
        
        {
            try
            {
                getcon();
                SqlCommand cmd5 = new SqlCommand("select convfactor from ProductMaster where product_name='" + txtItemName.Text + "'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd5);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Double convfactor;
                if (dt.Rows.Count > 0)
                {
                    convfactor = Convert.ToDouble(dt.Rows[0]["convfactor"].ToString());
                }
                else
                {
                    convfactor = 1;
                }

                double total = Convert.ToDouble(qty) * Convert.ToDouble(convfactor);

                double finaltotal = Convert.ToDouble(qty) * Convert.ToDouble(txtRate.Text);

                txtBasicAmount.Text = Math.Round(finaltotal, 2).ToString();
                double vat = ((Convert.ToDouble(txtBasicAmount.Text)-Convert.ToDouble(txtDiscount.Text)) * Convert.ToDouble(txtVat.Text)) / 100;
                double addvat=((Convert.ToDouble(txtBasicAmount.Text)-Convert.ToDouble(txtDiscount.Text)) * Convert.ToDouble(txtAddVat.Text)) / 100;
                double amount = Math.Round(Convert.ToDouble(txtBasicAmount.Text) - Convert.ToDouble(txtDiscount.Text) + vat + addvat, 2);
                txtAmount.Text = Math.Round(amount, 2).ToString();
            }
            catch
            {
            }
        }

        private void totalcalculation()
        {
            Int32 count = 0;
            Double total = 0;
            Double vat = 0, addvat = 0, basic = 0, free = 0, discount = 0;

            Double pqty = 0;

            for (int i = 0; i < LVFO.Items.Count; i++)
            {
                count++;
                
                pqty = pqty + Convert.ToDouble(LVFO.Items[i].SubItems[1].Text);
                if (LVFO.Items[i].SubItems[2].Text != "")
                {
                    free = free + Convert.ToDouble(LVFO.Items[i].SubItems[2].Text);
                }
                
                basic = basic + Convert.ToDouble(LVFO.Items[i].SubItems[5].Text);
                discount = discount + Convert.ToDouble(LVFO.Items[i].SubItems[7].Text);
                total = total + Convert.ToDouble(LVFO.Items[i].SubItems[10].Text);
                Double multi = 0;
                multi = ((Convert.ToDouble(LVFO.Items[i].SubItems[5].Text)-Convert.ToDouble(LVFO.Items[i].SubItems[7].Text)) * (Convert.ToDouble(LVFO.Items[i].SubItems[8].Text) / 100));
                vat = vat + multi;
                Double multi1 = 0;
                multi1 = ((Convert.ToDouble(LVFO.Items[i].SubItems[5].Text)-Convert.ToDouble(LVFO.Items[i].SubItems[7].Text)) * (Convert.ToDouble(LVFO.Items[i].SubItems[9].Text) / 100));
                addvat = addvat + multi1;


            }
            lbltotcount.Text = count.ToString();
            lbltotpqty.Text = pqty.ToString();
            lblbasictot.Text = basic.ToString();
            lbltotaldiscount.Text=discount.ToString();
            TxtBillTotal.Text = Math.Round(total, 2).ToString("N2");
            lbltaxtot.Text = Math.Round(vat, 2).ToString("N2");
            txtaddtax.Text = Math.Round(addvat, 2).ToString("N2");
        }

        private void LVFO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                listback();
            }
            catch { }
        }

        private void listback()
        {
            if (LVFO.SelectedItems.Count > 0)
            {
                txtItemName.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[0].Text;

                txtQty.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[1].Text;
                txtFree.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[2].Text;

                txtRate.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[3].Text;
                txtPer.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[4].Text;
                txtBasicAmount.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[5].Text;
                txtDiscountPer.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[6].Text;
                txtDiscount.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[7].Text;
                txtVat.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[8].Text;
                txtAddVat.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[9].Text;
                txtAmount.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[10].Text;
                Double total = 0;
                try
                {
                    total = Math.Round((Convert.ToDouble(TxtBillTotal.Text) - Convert.ToDouble(LVFO.Items[LVFO.FocusedItem.Index].SubItems[5].Text)), 2);
                }
                catch
                {
                }
                TxtBillTotal.Text = total.ToString();
                LVFO.Items[LVFO.FocusedItem.Index].Remove();
                totalcalculation();
            }
        }

        private void txtrate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                txtPer.Focus();
            }
        }

        private void cmbterms_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbCustName.Focus();
            }
        }

        private void cmbcustname_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                if (cmbCustName.Text != "")
                {
                    cmbPurchaseType.Focus();
                }
                else
                {
                    MessageBox.Show("Please Select Customer");
                    cmbCustName.Focus();
                }
            }
            if (e.KeyCode == Keys.F3)
            {
                Accountentry client = new Accountentry(this);

                client.Passed(1);
                client.Show();
            }

        }

        private void cmbsaletype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cmbPurchaseType.Text != "")
                {
                    txtItemName.Focus();
                }
                else
                {
                    MessageBox.Show("Please Select Purchase type");
                    cmbPurchaseType.Focus();
                }
            }

        }

        private void Sale_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            if (e.Alt == true && e.KeyCode == Keys.S)
            {
                btnPayment.PerformClick();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        int cnt = 0;
        internal void updatemode(string str, string p, int p_2)
        {
            try
            {
                getcon();
                loadpage();
                cnt = 1;
                SqlCommand cmd = new SqlCommand("select * from billmaster where Bill_No='" + p + "' and isactive=1 and billtype='PR' and CompanyId="+Master.companyId+"", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dtbm = new DataTable();
                sda.Fill(dtbm);

                SqlCommand cmd1 = new SqlCommand("select * from billproductmaster where BillType='PR' and Bill_No='" + p + "' and isactive=1", con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                if (dt.Rows.Count > 0)
                {
                    txtVchNo.Text = dtbm.Rows[0][0].ToString();
                    TxtBillNo.Text = dtbm.Rows[0][0].ToString();
                    TxtBillDate.Text = Convert.ToDateTime(dtbm.Rows[0][1].ToString()).ToString("dd/MMM/yyyy");
                    cmbTerms.Text = dtbm.Rows[0][2].ToString();

                    txtdispatch.Text = dtbm.Rows[0]["dispatchdetails"].ToString();
                    txtremarks.Text = dtbm.Rows[0]["remarks"].ToString();
                    cmd = new SqlCommand("select accountname from clientmaster where clientid='" + dtbm.Rows[0]["ClientID"].ToString() + "'", con);
                    con.Open();
                    string clientname = cmd.ExecuteScalar().ToString();
                    cmbCustName.Text = clientname;


                    cmd = new SqlCommand("select purchasetypename from PurchasetypeMaster where purchasetypeid='" + dtbm.Rows[0]["SaleType"].ToString() + "'", con);
                    string saletypename = cmd.ExecuteScalar().ToString();
                    cmbPurchaseType.Text = saletypename;
                }
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        ListViewItem li;
                        li = LVFO.Items.Add(dt1.Rows[i]["ProductName"].ToString());

                        li.SubItems.Add(dt1.Rows[i]["Pqty"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["Bags"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["Rate"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["Per"].ToString());
                        
                        li.SubItems.Add(dt1.Rows[i]["Total"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["discountper"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["discountamt"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["tax"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["addtax"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["amount"].ToString());
                    }
                }
                totalcalculation();
            }
            catch
            {
            }
            finally
            {
                clearitem();
                txtItemName.Focus();

                btnPayment.Text = "Update";

                con.Close();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clearall();
            clearitem();
            LVFO.Items.Clear();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Delete Purchase Return?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                getcon();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                SqlCommand cmd = new SqlCommand("update billmaster set isactive=0  where Bill_No='" + txtVchNo.Text + "' and billtype='PR' and CompanyId=" + Master.companyId + "", con);
                cmd.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("update billproductmaster set isactive=0 where Bill_No='" + txtVchNo.Text + "' and BillType='PR'", con);
                cmd2.ExecuteNonQuery();

                SqlCommand cmd1 = new SqlCommand("update Ledger set isactive=0 where VoucherId='" + txtVchNo.Text + "' and TranType='PurchaseReturn' and CompanyId=" + Master.companyId + "", con);
                cmd2.ExecuteNonQuery();
                clearall();
                clearitem();
                LVFO.Clear();
                this.Close();
                PurchaseReturnList frm = new PurchaseReturnList();
                frm.MdiParent = this.MdiParent;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.Show();
            }
            else
            {
                if (cmbPurchaseType.Text == "" || cmbCustName.Text == "" || cmbTerms.Text == "")
                {
                    MessageBox.Show("Please fill all the information.");
                    if (cmbPurchaseType.Text == "")
                    {
                        cmbPurchaseType.Focus();
                    }
                    else if (cmbCustName.Text == "")
                    {
                        cmbCustName.Focus();
                    }
                    else if (cmbTerms.Text == "")
                    {
                        cmbTerms.Focus();
                    }

                }
            }
        }

        private void txtdispatch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtremarks.Focus();
            }
        }

        private void txtremarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnPayment.Focus();
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void TxtBillNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtBillDate.Focus();
            }
        }

        private void TxtRundate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbTerms.Focus();
            }
        }

        private void txtVchNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtBillNo.Focus();
            }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                itemcalculation(txtQty.Text);
                txtFree.Focus();
            }
        }

        private void txtFree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtRate.Focus();
            }
        }

        private void txtPer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBasicAmount.Focus();
            }
        }

        private void txtBasicAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDiscountPer.Focus();
            }
        }

        private void txtDiscountPer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDiscount.Focus();
            }
        }

        private void txtDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtVat.Focus();
            }
        }

        private void txtVat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAddVat.Focus();
            }
        }

        private void txtAddVat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAmount.Focus();
            }
        }

        private void txtRate_TextChanged(object sender, EventArgs e)
        {
            itemcalculation(txtQty.Text);
        }


        int flag = 0;
        private void txtDiscountPer_TextChanged(object sender, EventArgs e)
        {

            if (txtDiscountPer.Text != "" && flag == 0)
            {
                double disc = ((Convert.ToDouble(txtBasicAmount.Text)) * Convert.ToDouble(txtDiscountPer.Text)) / 100;
                flag = 1;
                txtDiscount.Text = Math.Round(disc, 2).ToString();
                flag = 0;
                itemcalculation(txtQty.Text);
            }
        }
        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            if (txtDiscount.Text != "" && flag == 0)
            {
                double disc = (Convert.ToDouble(txtDiscount.Text) * 100) / Convert.ToDouble(txtBasicAmount.Text);
                flag = 1;
                txtDiscountPer.Text = Math.Round(disc, 2).ToString();
                flag = 0;
                itemcalculation(txtQty.Text);
            }
        }

        private void txtBasicAmount_TextChanged(object sender, EventArgs e)
        {
            //double basic = (Convert.ToDouble(txtBasicAmount.Text)) / Convert.ToDouble(txtQty.Text);
            //txtRate.Text = Math.Round(basic, 2).ToString();
        }

        private void LVFO_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    listback();
                }
            }
            catch { }
        }

    }
}
