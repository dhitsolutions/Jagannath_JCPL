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
using System.Diagnostics;

namespace RamdevSales
{
    public partial class DefaultPurchase : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        private DateWisePurchaseReport dateWisePurchaseReport;
        DataTable options = new DataTable();

        public DefaultPurchase()
        {
            InitializeComponent();
            options = conn.getdataset("select * from options");
            loadcurrency();
        }

        private void loadcurrency()
        {
            try
            {
                lblrate.Text = "Rate" + Master.currency;
                lblamount.Text = "Amount" + Master.currency;
            }
            catch
            {
            }
        }

        public DefaultPurchase(DateWisePurchaseReport dateWisePurchaseReport)
        {

            InitializeComponent();
            this.dateWisePurchaseReport = dateWisePurchaseReport;
            options = conn.getdataset("select * from options");
            loadcurrency();
        }

        public DefaultPurchase(Ledger ledger)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this.ledger = ledger;
            options = conn.getdataset("select * from options");
            loadcurrency();
        }

        private void Purchase_Load(object sender, EventArgs e)
        {
            try
            {
                if (cnt == 0)
                {
                    options = conn.getdataset("select * from options");
                    loadpage();
                    bindperticular();
                }
            }
            catch
            {

            }
        }

        private void loadpage()
        {
            DataTable dt3 = new DataTable();
            dt3 = conn.getdataset("select a, u, d, v, p, mId, uId, cId from UserRights where mId=4 and uId='" + UserLogin.id + "' and cId= " + Master.companyId + " and isActive=1");


            if (dt3.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dt3.Rows[0][2]) == false)
                {
                    btndelete.Visible = false;
                }
                if (Convert.ToBoolean(dt3.Rows[0][4]) == false)
                {
                    btnCalculator.Visible = false;

                }

            }
            con.Open();
            charges = 1;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            TxtRundate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            this.ActiveControl = TxtRundate;
            LVFO.Columns.Add("Description of Goods", 289, HorizontalAlignment.Left);
            LVFO.Columns.Add("Packing", 69, HorizontalAlignment.Center);
            LVFO.Columns.Add("Batch", 98, HorizontalAlignment.Center);
            LVFO.Columns.Add("P.Qty", 63, HorizontalAlignment.Center);
            LVFO.Columns.Add("A.Qty", 63, HorizontalAlignment.Center);
            LVFO.Columns.Add("Total Qty", 69, HorizontalAlignment.Right);
            LVFO.Columns.Add("Free", 63, HorizontalAlignment.Center);
            LVFO.Columns.Add(lblrate.Text, 95, HorizontalAlignment.Right);
            LVFO.Columns.Add("Per", 61, HorizontalAlignment.Center);
            LVFO.Columns.Add(lblamount.Text, 90, HorizontalAlignment.Center);
            LVFO.Columns.Add("Dis(%)", 63, HorizontalAlignment.Center);
            LVFO.Columns.Add("Dis Per", 63, HorizontalAlignment.Center);
            LVFO.Columns.Add("TAX", 70, HorizontalAlignment.Center);
            LVFO.Columns.Add("Add Tax", 59, HorizontalAlignment.Center);
            LVFO.Columns.Add("Total", 108, HorizontalAlignment.Center);

            LVCHARGES.Columns.Add("Perticulars", 411, HorizontalAlignment.Left);
            LVCHARGES.Columns.Add("Remarks", 411, HorizontalAlignment.Left);
            LVCHARGES.Columns.Add("Value", 167, HorizontalAlignment.Left);
            LVCHARGES.Columns.Add("@", 122, HorizontalAlignment.Left);
            LVCHARGES.Columns.Add("+/-", 91, HorizontalAlignment.Left);
            LVCHARGES.Columns.Add("Amount", 117, HorizontalAlignment.Right);
           // getsr();
            bindsaletype();
            bindcustomer();
            autoreaderbind();
            
            con.Close();
        }

        public void bindsaletype()
        {
            SqlCommand cmd = new SqlCommand("select purchasetypeid,purchasetypename from purchasetypeMaster where isactive='1' and type='P'", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            cmbsaletype.ValueMember = "purchasetypeid";
            cmbsaletype.DisplayMember = "purchasetypename";
            cmbsaletype.DataSource = dt;
            cmbsaletype.SelectedIndex = -1;

            autobind(dt, cmbsaletype);
        }

        public void bindcustomer()
        {
            string qry = "";
            if (Convert.ToBoolean(options.Rows[0]["showcustomersupplierseperate"].ToString()) == true)
            {
                qry = "select ClientID,AccountName from ClientMaster where isactive=1 and groupID=100 order by AccountName";
            }
            else
            {
                qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=100 or groupid=99) order by AccountName";
            }


            SqlCommand cmd1 = new SqlCommand(qry, con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            cmbcustname.ValueMember = "ClientID";
            cmbcustname.DisplayMember = "AccountName";
            cmbcustname.DataSource = dt1;
            cmbcustname.SelectedIndex = -1;


            autobind(dt1, cmbcustname);
        }

        public void autoreaderbind()
        {
            try
            {
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

                    txtitemname.AutoCompleteMode = AutoCompleteMode.Suggest;
                    txtitemname.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    txtitemname.AutoCompleteCustomSource = namesCollection;
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

                    txtitemname.AutoCompleteMode = AutoCompleteMode.Suggest;
                    txtitemname.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    txtitemname.AutoCompleteCustomSource = namesCollection;
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

            
            txtitemname.Text = string.Empty;
            txtpacking.Text = string.Empty;
            txtbags.Text = string.Empty;
            qtyflag = 1;
            txtqty.Text = string.Empty;
            qtyflag = 1;
            txtpqty.Text = string.Empty;
            txtrate.Text = string.Empty;
            txtper.Text = string.Empty;
            txttotal.Text = string.Empty;
            txttax.Text = string.Empty;
            txtaddtax.Text = string.Empty;
            txtamount.Text = string.Empty;
            lbladdtax1.Text = "[]";
            lbltax1.Text = "[]";
            lblbagqty.Text = "[]";
            lblaltqty.Text = "[]";
            txtfree.Text = "";
            txtdisamt.Text = "";
            txtdisper.Text = "";
           



        }

        public void clearall()
        {
            //getsr();
            TxtRundate.Text = DateTime.Now.ToShortDateString();
            cmbterms.SelectedIndex = -1;
            cmbcustname.SelectedIndex = -1;
            cmbsaletype.SelectedIndex = -1;
            txtpono.Text = string.Empty;
            lbltotpqty.Text = string.Empty;
            txttottax.Text = string.Empty;
            TxtBillTotal.Text = string.Empty;
            lblbasictot.Text = "0";
            txttottax.Text = "0";
            lbltotcount.Text = "0";
            lbltotpqty.Text = "0";
            txtweight.Text = string.Empty;
            txttransport.Text = string.Empty;
            txtremarks.Text = string.Empty;
            TxtBillNo.Text = "";
            TxtRundate.Focus();
            txtdelieveryat.Text = "";
        }

        private void BtnPayment_Click(object sender, EventArgs e)
        {
            btnsubmit();
        }

        private void btnsubmit()
        {
            try
            {
                if (cmbcustname.Text == "")
                {
                    MessageBox.Show("Select Party Name");
                    cmbcustname.Focus();
                }
                else if (cmbsaletype.Text == "")
                {
                    MessageBox.Show("Select Sale type");
                    cmbsaletype.Focus();
                }
                else if (LVFO.Items.Count == 0)
                {
                    MessageBox.Show("Please Enter atleast one item to generate sale");
                    txtitemname.Focus();
                }
                else
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    DialogResult dr = MessageBox.Show("Do you want to Save?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        this.Enabled = false;
                        if (BtnPayment.Text == "Update")
                        {
                            SqlCommand cmd2 = new SqlCommand("Update billproductmaster set isactive='0' where billno='" + TxtBillNo.Text + "' and BillType='P'", con);
                            cmd2.ExecuteNonQuery();
                            conn.execute("update Billchargesmaster set isactive='0' where billno='" + TxtBillNo.Text + "' and billtype='P'");
                            for (int i = 0; i < LVFO.Items.Count; i++)
                            {
                                Guid guid;
                                guid = Guid.NewGuid();
                                conn.execute("INSERT INTO [dbo].[BillProductMaster]([Bill_No],[Bill_Run_Date],[Productname],[Packing],[Bags],[MRP],[Pqty],[Aqty],[Rate],[Per],[Total],[Tax],[Amount],[isactive],[qty],[Billtype],[billno],[addtax],[batch],[free],[discountper],[discountamt],[IdToSync],[isSync])VALUES('0','" + DateTime.Now + "','" + LVFO.Items[i].SubItems[0].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[1].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[3].Text.Replace(",", "") + "','','" + LVFO.Items[i].SubItems[5].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[4].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[7].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[8].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[9].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[12].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[14].Text.Replace(",", "") + "','1','" + LVFO.Items[i].SubItems[5].Text.Replace(",", "") + "','P','" + TxtBillNo.Text + "','" + LVFO.Items[i].SubItems[13].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[2].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[6].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[10].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[11].Text.Replace(",", "") + "','"+guid+"',0)");

                            }
                            for (int i = 0; i < LVCHARGES.Items.Count; i++)
                            {
                                conn.execute("INSERT INTO [Billchargesmaster]([billno],[perticulars],[remarks],[value],[at],[plusminus],[amount],[billtype],[isactive])VALUES('" + TxtBillNo.Text + "','" + LVCHARGES.Items[i].SubItems[0].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[1].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[2].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[3].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[4].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[5].Text.Replace(",", "") + "','P',1)");
                            }
                            Guid guid1;
                            guid1 = Guid.NewGuid();
                            conn.execute("UPDATE [dbo].[BillMaster]SET [Bill_No] = '0',[Bill_Date] = '" + DateTime.Now + "',[Terms] = '" + cmbterms.Text + "',[ClientID] = '" + cmbcustname.SelectedValue + "',[PO_No] = '" + txtpono.Text.Replace(",", "") + "',[SaleType] = '" + cmbsaletype.SelectedValue + "',[count] = " + lbltotcount.Text.Replace(",", "") + ",[totalqty] = " + lbltotpqty.Text.Replace(",", "") + ",[totalbasic] = " + lblbasictot.Text.Replace(",", "") + ",[totaltax] = " + txttottax.Text.Replace(",", "") + ",[totalnet] = " + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + ",[isactive] = '1',[apprweight] = '" + txtweight.Text.Replace(",", "") + "',[dispatchdetails] = '" + txttransport.Text + "',[remarks] = '" + txtremarks.Text + "',[BillType] = 'P',[billno] = '" + TxtBillNo.Text + "',[totaladdtax] = '" + txttotaddvat.Text + "',[roudoff] = '" + txtroundoff.Text + "',[Duedate] = '" + Convert.ToDateTime(txtduedate.Text).ToString("dd/MMM/yyyy") + "',[totalaqty] = " + txttotaqty.Text.Replace(",", "") + ",[totalfree] = " + txttotfree.Text.Replace(",", "") + ",[totaldiscount] =" + txttotdiscount.Text.Replace(",", "") + ",[totaladddiscount] = " + txttotadis.Text.Replace(",", "") + ",[totalamount] = " + txtamt.Text.Replace(",", "") + ",[totalservicejob] = " + txttotservice.Text.Replace(",", "") + ",[totalcharges] = " + txttotalcharges.Text.Replace(",", "") + ",[Delieveryat] = '" + txtdelieveryat.Text + "',[fraight] = '" + txtfraight.Text + "',[vehicleno] = '" + txtvehicleno.Text + "',[grrrno] = '" + txtgrrrno.Text + "',[noofskids] = '" + txtskids.Text + "',[IdToSync]='"+guid1+"',[isSync]=0 where BillNo='" + TxtBillNo.Text + "' and [billtype]='P'");

                            string vno = conn.ExecuteScalar("select voucherid from ledger where [VoucherID]= '" + TxtBillNo.Text + "' and [TranType] = 'Purchase'");
                            if (vno != "0")
                            {
                                conn.execute("UPDATE [dbo].[Ledger] SET [Date1]='" + DateTime.Now + "',[TranType] = 'Purchase',[AccountID] = '" + cmbcustname.SelectedValue + "',[AccountName]='" + cmbcustname.Text + "' ,[Amount] = '" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "',[DC] = 'C' where [VoucherID]= '" + TxtBillNo.Text + "' and [TranType] = 'Purchase'");
                            }
                            else
                            {
                                conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString("dd/MMM/yyyy") + "','Purchase','" + cmbcustname.SelectedValue + "','" + cmbcustname.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','C',1)");
                            }

                            //ChangeNumbersToWords sh = new ChangeNumbersToWords();
                            //String s1 = Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00");
                            //string[] words = s1.Split('.');


                            //string str = sh.changeToWords(words[0]);
                            //string str1 = sh.changeToWords(words[1]);
                            //if (str1 == " " || str1 == null || words[1] == "00")
                            //{
                            //    str1 = "Zero ";
                            //}
                            //String inword = "In words: " + str + "and " + str1 + "Paise Only";
                            //// Phrase inwrd = new Phrase(inword, _font2);

                            clearitem();
                            clearall();
                            clearfooter();
                            LVFO.Items.Clear();
                            LVCHARGES.Items.Clear();
                            MessageBox.Show("Data Updated Successfully.");

                            BtnPayment.Text = "Save";
                        }
                        else
                        {
                            //getsr();
                            for (int i = 0; i < LVFO.Items.Count; i++)
                            {
                                //0 LVFO.Columns.Add("Description of Goods", 289, HorizontalAlignment.Left);
                                //1 LVFO.Columns.Add("Packing", 69, HorizontalAlignment.Center);
                                //2 LVFO.Columns.Add("Batch", 98, HorizontalAlignment.Center);
                                //3 LVFO.Columns.Add("P.Qty", 63, HorizontalAlignment.Center);
                                //4 LVFO.Columns.Add("A.Qty", 63, HorizontalAlignment.Center);
                                //5 LVFO.Columns.Add("Total Qty", 69, HorizontalAlignment.Right);
                                //6 LVFO.Columns.Add("Free", 63, HorizontalAlignment.Center);
                                //7 LVFO.Columns.Add(lblrate.Text, 95, HorizontalAlignment.Right);
                                //8 LVFO.Columns.Add("Per", 61, HorizontalAlignment.Center);
                                //9 LVFO.Columns.Add(lblamount.Text, 90, HorizontalAlignment.Center);
                                //10 LVFO.Columns.Add("Dis(%)", 63, HorizontalAlignment.Center);
                                //11 LVFO.Columns.Add("Dis Per", 63, HorizontalAlignment.Center);
                                //12 LVFO.Columns.Add("TAX", 70, HorizontalAlignment.Center);
                                //13 LVFO.Columns.Add("Add Tax", 59, HorizontalAlignment.Center);
                                //14 LVFO.Columns.Add("Total", 108, HorizontalAlignment.Center);

                                //0 LVCHARGES.Columns.Add("Perticulars", 411, HorizontalAlignment.Left);
                                //1  LVCHARGES.Columns.Add("Remarks", 411, HorizontalAlignment.Left);
                                //2 LVCHARGES.Columns.Add("Value", 167, HorizontalAlignment.Left);
                                //3 LVCHARGES.Columns.Add("@", 122, HorizontalAlignment.Left);
                                //4 LVCHARGES.Columns.Add("+/-", 91, HorizontalAlignment.Left);
                                //5 LVCHARGES.Columns.Add("Amount", 117, HorizontalAlignment.Right);
                                Guid guid2;
                                guid2 = Guid.NewGuid();
                                conn.execute("INSERT INTO [dbo].[BillProductMaster]([Bill_No],[Bill_Run_Date],[Productname],[Packing],[Bags],[MRP],[Pqty],[Aqty],[Rate],[Per],[Total],[Tax],[Amount],[isactive],[qty],[Billtype],[billno],[addtax],[batch],[free],[discountper],[discountamt],[IdToSync],[isSync])VALUES('0','" + DateTime.Now + "','" + LVFO.Items[i].SubItems[0].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[1].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[3].Text.Replace(",", "") + "','','" + LVFO.Items[i].SubItems[5].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[4].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[7].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[8].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[9].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[12].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[14].Text.Replace(",", "") + "','1','" + LVFO.Items[i].SubItems[5].Text.Replace(",", "") + "','P','" + TxtBillNo.Text + "','" + LVFO.Items[i].SubItems[13].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[2].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[6].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[10].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[11].Text.Replace(",", "") + "','"+guid2+"',0)");
                                //SqlCommand cmd1 = new SqlCommand("INSERT INTO [dbo].[BillProductMaster]([BillNo],[Bill_Run_Date],[Productname],[Packing],[Bags],[MRP],[Pqty],[Aqty],[Rate],[Per],[Total],[Tax],[addtax],[Amount],[isactive],[qty],[BillType],[Bill_no],[batch],[free],[discountper],[discountamt])VALUES('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString("dd/MMM/yyyy") + "','" + LVFO.Items[i].SubItems[1].Text + "','" + LVFO.Items[i].SubItems[2].Text + "','" + LVFO.Items[i].SubItems[0].Text + "','','" + LVFO.Items[i].SubItems[4].Text + "','','" + LVFO.Items[i].SubItems[5].Text + "','" + LVFO.Items[i].SubItems[6].Text + "','" + LVFO.Items[i].SubItems[7].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[8].Text + "','" + LVFO.Items[i].SubItems[9].Text + "','" + LVFO.Items[i].SubItems[10].Text.Replace(",", "") + "',1,'" + LVFO.Items[i].SubItems[3].Text + "','P','0','" + LVFO.Items[i].SubItems[2].Text + "','" + LVFO.Items[i].SubItems[6].Text + "','','')", con);
                                //cmd1.ExecuteNonQuery();

                            }
                            for (int i = 0; i < LVCHARGES.Items.Count; i++)
                            {
                                conn.execute("INSERT INTO [Billchargesmaster]([billno],[perticulars],[remarks],[value],[at],[plusminus],[amount],[billtype],[isactive])VALUES('" + TxtBillNo.Text + "','" + LVCHARGES.Items[i].SubItems[0].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[1].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[2].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[3].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[4].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[5].Text.Replace(",", "") + "','P',1)");
                            }
                            Guid guid3;
                            guid3 = Guid.NewGuid();
                            conn.execute("INSERT INTO [dbo].[BillMaster]([Bill_No],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[BillType],[billno],[totaladdtax],[roudoff],[Duedate],[totalaqty],[totalfree],[totaldiscount],[totaladddiscount],[totalamount],[totalservicejob],[totalcharges],[Delieveryat],[fraight],[vehicleno],[grrrno],[noofskids],[IdToSync],[isSync])VALUES('0','" + DateTime.Now + "','" + cmbterms.Text + "','" + cmbcustname.SelectedValue + "','" + txtpono.Text.Replace(",", "") + "','" + cmbsaletype.SelectedValue + "'," + lbltotcount.Text.Replace(",", "") + "," + lbltotpqty.Text.Replace(",", "") + "," + lblbasictot.Text.Replace(",", "") + "," + txttottax.Text.Replace(",", "") + "," + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + ",'1','" + txtweight.Text.Replace(",", "") + "','" + txttransport.Text + "','" + txtremarks.Text + "','P','" + TxtBillNo.Text + "','" + txttotaddvat.Text + "','" + txtroundoff.Text + "','" + Convert.ToDateTime(txtduedate.Text).ToString("dd/MMM/yyyy") + "'," + txttotaqty.Text.Replace(",", "") + "," + txttotfree.Text.Replace(",", "") + "," + txttotdiscount.Text.Replace(",", "") + "," + txttotadis.Text.Replace(",", "") + "," + txtamt.Text.Replace(",", "") + "," + txttotservice.Text.Replace(",", "") + "," + txttotalcharges.Text.Replace(",", "") + ",'" + txtdelieveryat.Text + "','" + txtfraight.Text + "','" + txtvehicleno.Text + "','" + txtgrrrno.Text + "','" + txtskids.Text + "','"+guid3+"',0)");
                            // SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[BillMaster]([BillNo],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType] ,[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[billtype],[Bill_no],[totaladdtax],[roudoff])VALUES('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString("dd/MMM/yyyy") + "','" + cmbterms.Text + "','" + cmbcustname.SelectedValue + "','" + txtpono.Text + "','" + cmbsaletype.SelectedValue + "'," + lbltotcount.Text + "," + lbltotpqty.Text + "," + lblbasictot.Text + "," + txttottax.Text + "," + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + ",1,'" + txtweight.Text + "','" + txttransport.Text + "','" + txtremarks.Text + "','P','0','" + txttotaddvat.Text + "','" + txtroundoff.Text + "')", con);
                            //cmd.ExecuteNonQuery();

                            conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive]) values ('" + TxtBillNo.Text + "','" + DateTime.Now + "','Purchase','" + cmbcustname.SelectedValue + "','" + cmbcustname.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','C',1)");

                            //ChangeNumbersToWords sh = new ChangeNumbersToWords();
                            //String s1 = Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00");
                            //string[] words = s1.Split('.');


                            //string str = sh.changeToWords(words[0]);
                            //string str1 = sh.changeToWords(words[1]);
                            //if (str1 == " " || str1 == null || words[1] == "00")
                            //{
                            //    str1 = "Zero ";
                            //}
                            //String inword = "In words: " + str + "and " + str1 + "Paise Only";

                            clearitem();
                            clearall();
                            clearfooter();
                            LVFO.Items.Clear();
                            LVCHARGES.Items.Clear();
                            MessageBox.Show("Data Inserted Successfully.");


                        }

                    }
                    else
                    {
                        MessageBox.Show("please fill all information");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally
            {
                this.Enabled = true;
                con.Close();
            }
        }

        private void clearfooter()
        {
            lbltotcount.Text = "0";
            lbltotpqty.Text = "0";
            txttotaqty.Text = "0";
            txttotfree.Text = "0";
            lblbasictot.Text = "0";
            txttotdiscount.Text = "0";
            txttotadis.Text = "0";
            txttottax.Text = "0";
            txttotaddvat.Text = "0";
            txtamt.Text = "0";
            txttotservice.Text = "0";
            txttotalcharges.Text = "0";
            txtroundoff.Text = "0";
            TxtBillTotal.Text = "0";

        }

        double addtax;
        private void txtamount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ListViewItem li;
                lbliteminfo.Visible = false;
                li = LVFO.Items.Add(txtitemname.Text);
                li.SubItems.Add(txtpacking.Text);
                li.SubItems.Add("");
                li.SubItems.Add(txtbags.Text);
                li.SubItems.Add((Math.Round(Convert.ToDouble(txtqty.Text), 2).ToString()));
                li.SubItems.Add((Math.Round(Convert.ToDouble(txtpqty.Text), 2).ToString()));
                li.SubItems.Add(txtfree.Text);
                li.SubItems.Add((Math.Round(Convert.ToDouble(txtrate.Text), 2).ToString()));
                li.SubItems.Add(txtper.Text);
                li.SubItems.Add((Math.Round(Convert.ToDouble(txttotal.Text), 2).ToString()));
                li.SubItems.Add((Math.Round(Convert.ToDouble(txtdisper.Text), 2).ToString()));
                li.SubItems.Add((Math.Round((Convert.ToDouble(txtdisamt.Text)), 2).ToString()));
                li.SubItems.Add((Math.Round((Convert.ToDouble(txttax.Text)), 2).ToString()));
                li.SubItems.Add((Math.Round((Convert.ToDouble(txtaddtax.Text)), 2).ToString()));
                li.SubItems.Add((Math.Round(Convert.ToDouble(txtamount.Text), 2).ToString()));


                totalcalculation();
                clearitem();
                txtitemname.Focus();
            }
        }

        double taxid, addtaxid;
        private void txtitemname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    x = 0;
                    SqlCommand cmd5 = new SqlCommand("select p.*,b.* from productmaster p inner join ProductPriceMaster b on p.ProductID=b.ProductID where p.product_name='" + txtitemname.Text + "'", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd5);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    lbliteminfo.Visible = true;
                    qtyflag = 0;
                    txtpacking.Text = dt.Rows[0]["Packing"].ToString();
                    txtrate.Text = dt.Rows[0]["PurchasePrice"].ToString();
                    txtper.Text = dt.Rows[0]["Unit"].ToString();
                    lblbagqty.Text = "[" + dt.Rows[0]["Unit"].ToString() + "]";
                    lblaltqty.Text = "[" + dt.Rows[0]["Altunit"].ToString() + "]";
                    txtqty.Text = dt.Rows[0]["Convfactor"].ToString();
                    txtdisamt.Text = "0.00";
                    txtdisper.Text = "0.00";
                    txtfree.Text = "0";
                    lbliteminfo.Text = "Sale Price=" + dt.Rows[0]["SalePrice"].ToString() + "  MRP=" + dt.Rows[0]["MRP"].ToString() + "  Basic=" + dt.Rows[0]["BasicPrice"].ToString() + "  Prch Price=" + dt.Rows[0]["PurchasePrice"].ToString();
                    SqlCommand cmd6 = new SqlCommand("select * from itemtaxmaster i inner join productmaster p on i.productid=p.productid where p.product_name like'%" + txtitemname.Text + "%' and i.saletypeid like '%" + cmbsaletype.Text + "'", con);
                    SqlDataAdapter sda6 = new SqlDataAdapter(cmd6);
                    DataTable dt1 = new DataTable();
                    sda6.Fill(dt1);
                    string istax = conn.ExecuteScalar("Select taxtypename from purchasetypemaster where isactive=1 and type='P' and purchasetypename='" + cmbsaletype.Text + "'");
                    if (dt1.Rows.Count > 0)
                    {
                        txttax.Text = "0";
                        txtaddtax.Text = "0";
                        if (istax != "Tax Free")
                        {
                            lbltax1.Text = "[" + Math.Round(Convert.ToDouble(dt1.Rows[0]["Vat"].ToString()), 2).ToString() + "]";
                            lbladdtax1.Text = "[" + Math.Round(Convert.ToDouble(dt1.Rows[0]["AddVat"].ToString()), 2).ToString() + "]";
                        }
                        else
                        {
                            lbltax1.Text = "[0]";
                            lbladdtax1.Text = "[0]";
                        }
                       
                        txtpacking.Focus();
                        taxid = Math.Round(Convert.ToDouble(dt1.Rows[0]["Vat"].ToString()), 2);
                        addtaxid = Math.Round(Convert.ToDouble(dt1.Rows[0]["AddVat"].ToString()), 2);

                    }
                    else
                    {
                        
                        MessageBox.Show("Not any Tax Available For This Sale Type");
                        txttax.Text = "0";
                        txtaddtax.Text = "0";
                        lbltax1.Text = "[0]";
                        lbladdtax1.Text = "[0]";
                        taxid = 0;
                        addtaxid = 0;
                        txtpacking.Focus();
                    }


                }
                if (e.KeyCode == Keys.F3)
                {
                    Itementry client = new Itementry(this);

                    client.Passed(1);
                    client.Show();
                }
                if (e.KeyCode == Keys.F2)
                {
                    if (txtitemname.Text != "")
                    {
                        Itementry client = new Itementry(this);
                        client.Updatefromsale(txtitemname.Text);
                        client.Show();
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
                SqlCommand cmd5 = new SqlCommand("select convfactor from ProductMaster where product_name='" + txtitemname.Text + "'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd5);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Double convfactor = Convert.ToDouble(dt.Rows[0]["convfactor"].ToString());

                double total = Convert.ToDouble(qty) * Convert.ToDouble(convfactor);

                double finaltotal = Convert.ToDouble(qty) * Convert.ToDouble(txtrate.Text);
                
                txttotal.Text = Math.Round(finaltotal, 2).ToString();
                txtdisamt.Text = (Math.Round((Convert.ToDouble(txtdisper.Text) * Convert.ToDouble(txttotal.Text)) / 100, 2)).ToString();
                double discount = Convert.ToDouble(txttotal.Text) - Convert.ToDouble(txtdisamt.Text);
                double tax = Math.Round(discount * (Convert.ToDouble(taxid.ToString())) / 100, 2);
                double addtax = Math.Round(discount * (Convert.ToDouble(addtaxid.ToString())) / 100, 2);
                double amount = Math.Round(discount + ((discount * (Convert.ToDouble(taxid.ToString()) + Convert.ToDouble(addtaxid.ToString()))) / 100), 2);
                txtamount.Text = Math.Round(amount, 2).ToString();
                txttax.Text = tax.ToString();
                txtaddtax.Text = addtax.ToString();
                
            }
            catch
            {
            }
        }

        private void totalcalculation()
        {
            try
            {

                Int32 count = 0;
                Double total = 0;
                Double vat = 0, basic = 0,discount=0;
                Double addvat = 0;
               
                Double pqty = 0,aqty=0,free=0;
                for (int i = 0; i < LVFO.Items.Count; i++)
                {
                    count++;
                    
                    aqty = aqty + Convert.ToDouble(LVFO.Items[i].SubItems[3].Text);
                    pqty = pqty + Convert.ToDouble(LVFO.Items[i].SubItems[5].Text);
                    free = free + Convert.ToDouble(LVFO.Items[i].SubItems[6].Text);
                    basic = basic + Convert.ToDouble(LVFO.Items[i].SubItems[9].Text);
                    discount += Convert.ToDouble(LVFO.Items[i].SubItems[11].Text);
                    vat+=Convert.ToDouble(LVFO.Items[i].SubItems[12].Text);
                    addvat += Convert.ToDouble(LVFO.Items[i].SubItems[13].Text);
                    total = total + Convert.ToDouble(LVFO.Items[i].SubItems[14].Text);
                    Double multi = 0, add = 0;
                   
                    //multi = (Convert.ToDouble(LVFO.Items[i].SubItems[7].Text) * (Convert.ToDouble(LVFO.Items[i].SubItems[8].Text) / 100));
                    //vat = vat + multi;
                    //add = (Convert.ToDouble(LVFO.Items[i].SubItems[7].Text) * (Convert.ToDouble(LVFO.Items[i].SubItems[9].Text) / 100));
                    //addvat += add;

                }
                lbltotcount.Text = count.ToString("");
                lbltotpqty.Text = pqty.ToString("");
                txttotfree.Text = free.ToString("");
                txttotaqty.Text = aqty.ToString();
                lblbasictot.Text = basic.ToString();
                txttotdiscount.Text = discount.ToString("N2");
                txttottax.Text = Math.Round(vat, 2).ToString("N2");
                txttotaddvat.Text = Math.Round(addvat, 2).ToString("N2");
                txtamt.Text = Math.Round(total, 2).ToString("N2");
                getOptions(Math.Round(total, 2));
                
              
            }
            catch
            {
            }
        }

        private void getOptions(Double total)
        {
           
            DataTable dt= options;
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dt.Rows[0]["autoroundoffpurchase"].ToString()) == true)
                {

                    double charges = Convert.ToDouble(txttotalcharges.Text);
                    TxtBillTotal.Text = Math.Round(total + charges, 0).ToString("N2");
                    txtroundoff.Text = Math.Round((Math.Round(Convert.ToDouble(TxtBillTotal.Text), 0) - Convert.ToDouble(total + charges)), 2).ToString();


                }
                else
                {
                    double charges = Convert.ToDouble(txttotalcharges.Text);
                    TxtBillTotal.Text = Math.Round(total + charges, 2).ToString("N2");
                    txtroundoff.Text = "0";
                }
            }

        }

        int qtyflag;
        private void txtpqty_TextChanged(object sender, EventArgs e)
        {
            try
            {
               if (qtyflag == 0)
                {
                    qtyflag = 1;
                    if (txtpqty.Text != "")
                    {
                        if (txtqty.Text != "")
                        {
                            Double qty = Convert.ToDouble(Convert.ToDouble(txtpqty.Text) / Convert.ToDouble(txtqty.Text));

                            txtbags.Text = qty.ToString();

                        }
                        else
                        {
                            Double qty = Convert.ToDouble(Convert.ToDouble(txtpqty.Text) / 1);
                            txtbags.Text = qty.ToString();

                        }
                    }
                    else
                    {
                        if (txtqty.Text != "")
                        {
                            Double qty = Convert.ToDouble(1 * Convert.ToDouble(txtqty.Text));
                            txtbags.Text = qty.ToString();

                        }
                        else
                        {
                            txtpqty.Text = "0.00";
                        }

                    }
                    if (txtpqty.Text != "" && txtpqty.Text != "0.00")
                    {
                        itemcalculation(txtpqty.Text);

                    }
                    else
                    {
                        itemcalculation("0");
                    }
                    qtyflag = 0;
                }
            }
            catch
            {
            }


            try
            {
                if (txtpqty.Text != "")
                {
                    

                }
                else
                {
                    itemcalculation("1");
                }
            }
            catch
            {
            }
        }

        private void LVFO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (LVFO.SelectedItems.Count > 0)
            {
                lbliteminfo.Visible = true;
                txtitemname.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[0].Text;
                txtpacking.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[1].Text;
                cmbbatch.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[2].Text;
                txtbags.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[3].Text;
                txtqty.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[4].Text;
                txtpqty.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[5].Text;
                txtfree.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[6].Text;
                txtrate.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[7].Text;
                txtper.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[8].Text;
                txttotal.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[9].Text;
                txtdisper.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[10].Text;
                txtdisamt.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[11].Text;
                txttax.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[12].Text;
                txtaddtax.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[13].Text;
                txtamount.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[14].Text;
                SqlCommand cmd5 = new SqlCommand("select p.*,b.* from productmaster p inner join ProductPriceMaster b on p.ProductID=b.ProductID where p.product_name='" + txtitemname.Text + "'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd5);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                lblbagqty.Text = "[" + dt.Rows[0]["Unit"].ToString() + "]";
                lblaltqty.Text = "[" + dt.Rows[0]["Altunit"].ToString() + "]";

                SqlCommand cmd6 = new SqlCommand("select * from itemtaxmaster i inner join productmaster p on i.productid=p.productid where p.product_name like'%" + txtitemname.Text + "%' and i.saletypeid like '%" + cmbsaletype.Text + "'", con);
                SqlDataAdapter sda6 = new SqlDataAdapter(cmd6);
                DataTable dt1 = new DataTable();
                sda6.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    
                    lbltax1.Text = "[" + Math.Round(Convert.ToDouble(dt1.Rows[0]["Vat"].ToString()), 2).ToString() + "]";
                    lbladdtax1.Text = "[" + Math.Round(Convert.ToDouble(dt1.Rows[0]["AddVat"].ToString()), 2).ToString() + "]";
                    txtbags.Focus();
                    taxid = Math.Round(Convert.ToDouble(dt1.Rows[0]["Vat"].ToString()), 2);
                    addtaxid = Math.Round(Convert.ToDouble(dt1.Rows[0]["AddVat"].ToString()), 2);

                }
                else
                {

                  //  MessageBox.Show("Not any Tax Available For This Sale Type");
                  
                    lbltax1.Text = "[0]";
                    lbladdtax1.Text = "[0]";
                    taxid = 0;
                    addtaxid = 0;
                    txtbags.Focus();
                }

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

        private void txtbags_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtpqty.Focus();


            }
        }

        private void txtpqty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtfree.Focus();
            }
        }

        private void txtrate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                txttotal.Focus();

            }
        }
        int x,y;
        private void txtrate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDouble(txtrate.Text) > 0)
                {
                    if (x == 0)
                    {

                        itemcalculation(txtpqty.Text);
                    }
                }
            }
            catch
            {
            }
        }

        private void cmbterms_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbcustname.Focus();
            }
        }

        private void cmbcustname_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                if (cmbcustname.Text != "")
                {
                    cmbsaletype.Focus();
                }
                else
                {
                    MessageBox.Show("Please Select Customer");
                    cmbcustname.Focus();
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
                if (cmbsaletype.Text != "")
                {
                    txtpono.Focus();
                }
                else
                {
                    MessageBox.Show("Please Select Sale type");
                    cmbsaletype.Focus();
                }
            }
            if (e.KeyCode == Keys.F3)
            {
                PurchaseTypeEntry pt = new PurchaseTypeEntry(this);
                pt.Show();

            }
            if (e.KeyCode == Keys.F2)
            {
                PurchaseTypeEntry pt = new PurchaseTypeEntry(this);
                pt.updatemode("Purchase", cmbsaletype.Text, 1);
                pt.Show();

            }

        }

        private void txtpono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtweight.Focus();
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
                BtnPayment.PerformClick();
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
        private Ledger ledger;

        internal void updatemode(string str, string p, int p_2)
        {
            try
            {
                loadpage();
                cnt = 1;
                SqlCommand cmd = new SqlCommand("select * from billmaster where billno='" + p + "' and isactive=1 and billtype='P'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                SqlCommand cmd1 = new SqlCommand("select * from billproductmaster where billno='" + p + "' and billtype='P' and isactive=1", con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);

                DataTable dt2 = conn.getdataset("select * from Billchargesmaster where billno='" + p + "' and billtype='P' and isactive=1");

                if (dt.Rows.Count > 0)
                {
                    TxtBillNo.Text = dt.Rows[0]["billno"].ToString();
                    TxtRundate.Text = Convert.ToDateTime(dt.Rows[0]["Bill_Date"].ToString()).ToString("dd/MM/yyyy");
                    cmbterms.Text = dt.Rows[0]["Terms"].ToString();

                    cmd = new SqlCommand("select accountname from clientmaster where clientid='" + dt.Rows[0]["ClientID"].ToString() + "' and isactive=1", con);
                    con.Open();
                    string clientname = cmd.ExecuteScalar().ToString();
                    //  cmbcustname.SelectedIndex = cmbcustname.Items.IndexOf(clientname);
                    cmbcustname.Text = clientname;
                    txtpono.Text = dt.Rows[0]["PO_No"].ToString();

                    cmd = new SqlCommand("select purchasetypename from purchasetypemaster where type='P' and purchasetypeid='" + dt.Rows[0]["SaleType"].ToString() + "'", con);
                    string saletypename = cmd.ExecuteScalar().ToString();
                    cmbsaletype.Text = saletypename;
                    //lbltotcount.Text = dt.Rows[0]["count"].ToString();
                    //lbltotpqty.Text = dt.Rows[0]["totalqty"].ToString();
                    //lblbasictot.Text = dt.Rows[0]["totalbasic"].ToString();
                    //txttottax.Text = dt.Rows[0]["totaltax"].ToString();
                    //TxtBillTotal.Text = dt.Rows[0]["totaltax"].ToString();

                    txtweight.Text = dt.Rows[0]["apprweight"].ToString();
                    txttransport.Text = dt.Rows[0]["dispatchdetails"].ToString();
                    txtremarks.Text = dt.Rows[0]["remarks"].ToString();
                    txtduedate.Text = Convert.ToDateTime(dt.Rows[0]["Duedate"].ToString()).ToString("dd/MM/yyyy");
                    txtdelieveryat.Text = dt.Rows[0]["Delieveryat"].ToString();
                    txtfraight.Text = dt.Rows[0]["fraight"].ToString();
                    txtvehicleno.Text = dt.Rows[0]["vehicleno"].ToString();
                    txtgrrrno.Text = dt.Rows[0]["grrrno"].ToString();
                    txtskids.Text = dt.Rows[0]["noofskids"].ToString();
                }

                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        ListViewItem li;
                        li = LVFO.Items.Add(dt1.Rows[i]["Productname"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["Packing"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["batch"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["Bags"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["Aqty"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["Pqty"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["free"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["Rate"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["Per"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["Total"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["discountper"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["discountamt"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["Tax"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["addtax"].ToString());
                        li.SubItems.Add(dt1.Rows[i]["Amount"].ToString());
                    }
                }
                if (dt2.Rows.Count > 0)
                {
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        ListViewItem li;
                        li = LVCHARGES.Items.Add(dt2.Rows[i]["perticulars"].ToString());
                        li.SubItems.Add(dt2.Rows[i]["remarks"].ToString());
                        li.SubItems.Add(dt2.Rows[i]["value"].ToString());
                        li.SubItems.Add(dt2.Rows[i]["at"].ToString());
                        li.SubItems.Add(dt2.Rows[i]["plusminus"].ToString());
                        li.SubItems.Add(dt2.Rows[i]["amount"].ToString());
                    }
                }
                calculatetotalcharges();
                totalcalculation();
                clearitem();
                txtitemname.Focus();

                BtnPayment.Text = "Update";


                con.Close();
            }
            catch
            {
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
            try
            {
                this.Enabled = false;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                SqlCommand cmd = new SqlCommand("update billmaster set isactive=0  where billno='" + TxtBillNo.Text + "' and billtype='P'", con);
                cmd.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("update billproductmaster set isactive=0 where billno='" + TxtBillNo.Text + "' and BillType='P'", con);
                cmd2.ExecuteNonQuery();
                conn.execute("update Billchargesmaster set isactive='0' where billno='" + TxtBillNo.Text + "' and billtype='P'");

                SqlCommand cmd3 = new SqlCommand("update ledger set isactive=0 where voucherid='" + TxtBillNo.Text + "' and trantype='Purchase'", con);
                cmd3.ExecuteNonQuery();
                MessageBox.Show("Delete Successfully");
                this.Close();
            }
            finally
            {
                this.Enabled = true;
            }
        }

        private void txtpacking_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtbags.Focus();
            }
        }

        private void txtpacking_TextChanged(object sender, EventArgs e)
        {
            if (txtpacking.Text != "")
            {

            }
        }

        private void txtqty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (qtyflag == 0)
                {
                    if (txtqty.Text != "")
                    {
                        if (txtbags.Text != "")
                        {
                            Double qty = Convert.ToDouble(Convert.ToDouble(txtbags.Text) * Convert.ToDouble(txtqty.Text));
                            txtpqty.Text = qty.ToString();
                        }
                        else
                        {
                            Double qty = Convert.ToDouble(1 * Convert.ToDouble(txtqty.Text));
                            txtpqty.Text = qty.ToString();
                        }
                    }
                    else
                    {
                        if (txtbags.Text != "")
                        {
                            Double qty = Convert.ToDouble(Convert.ToDouble(txtbags.Text) * 1);
                            txtpqty.Text = qty.ToString();
                        }
                        else
                        {
                            txtpqty.Text = "0.00";
                        }

                    }
                    if (txtpqty.Text != "" && txtpqty.Text != "0.00")
                    {
                        itemcalculation(txtpqty.Text);

                    }
                    else
                    {
                        itemcalculation("0");
                    }
                    qtyflag = 1;
                }

            }
            catch
            {
            }
        }

        private void txtqty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtpqty.Focus();
            }
        }

        private void txtweight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtskids.Focus();
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
                BtnPayment.Focus();
            }
        }

        private void txtbags_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (qtyflag == 0)
                {
                    qtyflag = 1;
                    if (txtbags.Text != "")
                    {
                        if (txtqty.Text != "")
                        {
                            Double qty = Convert.ToDouble(Convert.ToDouble(txtbags.Text) * Convert.ToDouble(txtqty.Text));
                            txtpqty.Text = qty.ToString();
                        }
                        else
                        {
                            Double qty = Convert.ToDouble(Convert.ToDouble(txtbags.Text) * 1);
                            txtpqty.Text = qty.ToString();
                        }
                    }
                    else
                    {
                        if (txtqty.Text != "")
                        {
                            Double qty = Convert.ToDouble(1 * Convert.ToDouble(txtqty.Text));
                            txtpqty.Text = qty.ToString();
                        }
                        else
                        {
                            txtpqty.Text = "0.00";
                        }

                    }
                    if (txtpqty.Text != "" && txtpqty.Text != "0.00")
                    {
                        itemcalculation(txtpqty.Text);

                    }
                    else
                    {
                        itemcalculation("0");
                    }
                }
                qtyflag = 0;
            }
            catch
            {
            }
        }

        private void txtbags_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtqty_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtpqty_KeyPress(object sender, KeyPressEventArgs e)
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
                txtitemname.Focus();
            }
        }

        private void TxtRundate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbterms.Focus();
            }
        }

        private void LVFO_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LVFO_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Delete)
            {
                LVFO.Items[LVFO.FocusedItem.Index].Remove();
                totalcalculation();
            }

        }

        private void cmbbatch_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void cmbbatch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtper_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txttotal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtdisper.Focus();
            }
        }

        private void txttotal_TextChanged(object sender, EventArgs e)
        {
            if (txttotal.Text != "" && y == 0)
            {
                try
                {
                    double amt = Convert.ToDouble(txttotal.Text) / Convert.ToDouble(txtpqty.Text);
                    txtrate.Text = Math.Round(amt, 2).ToString();
                    double discount = Convert.ToDouble(txttotal.Text) - Convert.ToDouble(txtdisamt.Text);
                    double tax = Math.Round(discount * (Convert.ToDouble(taxid.ToString())) / 100, 2);
                    double addtax = Math.Round(discount * (Convert.ToDouble(addtaxid.ToString())) / 100, 2);
                    double amount = Math.Round(discount + ((discount * (Convert.ToDouble(taxid.ToString()) + Convert.ToDouble(addtaxid.ToString()))) / 100), 2);
                    txtamount.Text = Math.Round(amount, 2).ToString();
                    txttax.Text = tax.ToString();
                    txtaddtax.Text = addtax.ToString();
                }
                catch
                {
                }

            }
        }

        private void txtdisper_TextChanged(object sender, EventArgs e)
        {
            if (txtdisper.Text != "" && p == 0 && Convert.ToDouble(txtdisper.Text) > 0)
            {
                double amt = (Convert.ToDouble(txttotal.Text) * Convert.ToDouble(txtdisper.Text)) / 100;
                txtdisamt.Text = Math.Round(amt, 2).ToString();
                itemcalculation(txtpqty.Text);
            }
        }

        int p, q;
        private void txtdisper_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtdisamt.Focus();
            }
        }

        private void txtdisper_KeyPress(object sender, KeyPressEventArgs e)
        {
            p = 0;
            q = 1;
        }

        private void txtfree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                x = 0;
                txtrate.Focus();
            }
        }

        private void txtfree_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtdisamt_TextChanged(object sender, EventArgs e)
        {
            if ((txtdisamt.Text != "" && q == 0) && Convert.ToDouble(txtdisamt.Text) > 0)
            {
                if (Convert.ToDouble(txttotal.Text) == 0)
                {
                    txtdisper.Text = Math.Round(0.00, 2).ToString();
                }
                else
                {
                    double amt = (100 * Convert.ToDouble(txtdisamt.Text)) / Convert.ToDouble(txttotal.Text);
                    txtdisper.Text = Math.Round(amt, 2).ToString();
                }

                itemcalculation(txtpqty.Text);
            }
        }

        private void txtdisamt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtamount.Focus();
            }
        }

        private void txtdisamt_KeyPress(object sender, KeyPressEventArgs e)
        {
            p = 1;
            q = 0;
        }

        private void txtrate_KeyPress(object sender, KeyPressEventArgs e)
        {
            x = 0;
            y = 1;
        }

        private void txttotal_Validated(object sender, EventArgs e)
        {
           
        }

        private void txttotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            x = 1;
            y = 0;
        }

        private void textBox9_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox18_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtcharval.Text != "0")
                {
                    txtcharval.Focus();
                }
                else
                {
                    txtcharamt.Focus();
                }
            }
        }

        private void cmbcharper_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtcharremark.Focus();
            }
            if (e.KeyCode == Keys.F3)
            {

                ChargesHead popup = new ChargesHead(this, cmbcharper.Text);

                popup.ShowDialog();

            //    string userEnteredText = popup.EnteredText;

                popup.Dispose();

            }
        }

        public void bindperticular()
        {
            DataTable dt = conn.getdataset("select billsundryid,billsundryname from billsundry where isactive=1");

            
            cmbcharper.ValueMember = "billsundryid";
            cmbcharper.DisplayMember = "billsundryname";
            cmbcharper.DataSource = dt;
            cmbcharper.SelectedIndex = -1;
            charges = 0;
            autobind(dt, cmbcharper);
            
        }
        int charges;
        private void cmbcharper_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (charges != 1)
            {
                if (cmbcharper.Text != "")
                {
                    DataTable dt = conn.getdataset("select * from billsundry where isactive=1 and billsundryid='" + cmbcharper.SelectedValue + "'");
                    if (dt.Rows[0]["symbol"].ToString() == "%")
                    {

                        if (dt.Rows[0]["applyon"].ToString() == "Net")
                        {
                            txtcharval.Enabled = true;
                            txtcharat.Enabled = true;
                            double value = (Convert.ToDouble(dt.Rows[0]["ON1"].ToString()) * Convert.ToDouble(txtamt.Text)) / 100;
                            txtcharval.Text = Math.Round(value, 2).ToString();
                            txtcharat.Text = dt.Rows[0]["percentage"].ToString();
                        }
                    }
                    else
                    {
                        txtcharval.Text = "0";
                        txtcharval.Enabled = false;
                        txtcharat.Enabled = false;
                    }
                    
                    txtcharplusminus.Text = dt.Rows[0]["BillSundryType"].ToString();
                    txtcharamt.Text = Math.Round(Convert.ToDouble(txtcharval.Text) * (Convert.ToDouble(txtcharat.Text) / 100), 2).ToString();
                    txtcharremark.Focus();
                }
            }
           
        }

        private void addchargesdatadata()
        {
            DataTable dt = conn.getdataset("select * from billsundry where isactive=1 and billsundryid='" + cmbcharper.SelectedValue + "'");


        }

        private void txtcharval_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtcharat.Focus();
            }
        }

        private void txtcharval_TextChanged(object sender, EventArgs e)
        {
            if (txtcharval.Text != "")
            {
                chargescalculator();
            }
        }

        private void chargescalculator()
        {
            if (txtcharat.Text == "")
            {
                txtcharat.Text = "0";
            }
            txtcharamt.Text = Math.Round(Convert.ToDouble(txtcharval.Text) * (Convert.ToDouble(txtcharat.Text) / 100), 2).ToString();
        }

        private void txtcharat_TextChanged(object sender, EventArgs e)
        {
            if (txtcharat.Text != "")
            {
                chargescalculator();
            }
        }

        private void txtcharplusminus_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtcharamt_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtcharplusminus_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtcharamt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btncharadditem.Focus();
            }
        }

        private void txtcharat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtcharamt.Focus();
            }
        }

        private void btncharadditem_Click(object sender, EventArgs e)
        {
            try
            {

                ListViewItem li;
                lbliteminfo.Visible = false;
                li = LVCHARGES.Items.Add(cmbcharper.Text);
                li.SubItems.Add(txtcharremark.Text);
                li.SubItems.Add(txtcharval.Text);
                li.SubItems.Add(txtcharat.Text);
                li.SubItems.Add(txtcharplusminus.Text);
                li.SubItems.Add((Math.Round(Convert.ToDouble(txtcharamt.Text), 2).ToString()));


                calculatetotalcharges();

                totalcalculation();
                clearcharitem();
                cmbcharper.Focus();
            }
            catch
            {
            }
        }

        private void calculatetotalcharges()
        {
            Double charges = 0;
            for (int i = 0; i < LVCHARGES.Items.Count; i++)
            {
                if (LVCHARGES.Items[i].SubItems[4].Text == "+")
                {
                    string str = LVCHARGES.Items[i].SubItems[5].Text;
                    //  totalcalculation();

                    charges += Convert.ToDouble(LVCHARGES.Items[i].SubItems[5].Text);
                }
                if (LVCHARGES.Items[i].SubItems[4].Text == "-")
                {
                    string str = LVCHARGES.Items[i].SubItems[5].Text;
                    charges -= Convert.ToDouble(LVCHARGES.Items[i].SubItems[5].Text);
                }

            }
            txttotalcharges.Text = Math.Round(charges, 2).ToString();
        }

        private void clearcharitem()
        {
            cmbcharper.Text = "";
            txtcharamt.Text = "";
            txtcharat.Text = "";
            txtcharremark.Text = "";
            txtcharval.Text = "";
            txtcharplusminus.Text = "";

        }

        private void txtcharval_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtcharat_KeyPress(object sender, KeyPressEventArgs e)
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
        
        private void txtcharamt_KeyPress(object sender, KeyPressEventArgs e)
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

        private void LVCHARGES_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (LVCHARGES.SelectedItems.Count > 0)
                {
                    cmbcharper.Text = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[0].Text;
                    txtcharremark.Text = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[1].Text;
                    txtcharval.Text = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[2].Text;
                    txtcharat.Text = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[3].Text;
                    txtcharplusminus.Text = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[4].Text;
                    txtcharamt.Text = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[5].Text;

                    LVCHARGES.Items[LVCHARGES.FocusedItem.Index].Remove();
                    calculatetotalcharges();
                    totalcalculation();
                    

                }
            }
            catch
            {
            }
        }

        private void LVCHARGES_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                LVCHARGES.Items[LVCHARGES.FocusedItem.Index].Remove();
                calculatetotalcharges();
                totalcalculation();
            }
        }

        private void txttransport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtdelieveryat.Focus();
            }
        }

        private void txtdelieveryat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtfraight.Focus();
            }
        }

        private void txtfraight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtvehicleno.Focus();
            }
        }

        private void txtvehicleno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtgrrrno.Focus();
            }
        }

        private void txtgrrrno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtremarks.Focus();
            }
        }

        private void txtremarks_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtweight.Focus();
            }
        }

        private void txtweight_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtskids.Focus();
            }
        }

        private void txtskids_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnPayment.Focus();
            }
        }

        private void txtpono_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtBillNo.Focus();
            }
            
        }

        private void txtduedate_KeyDown(object sender, KeyEventArgs e)
        {

        }

    }
}
