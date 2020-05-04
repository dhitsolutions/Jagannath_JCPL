using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Diagnostics;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.rtf;
using iTextSharp.text.html.simpleparser;
using System.Web;
using System.Text.RegularExpressions;

namespace RamdevSales
{
    public partial class Sale : Form
    {
        DataTable options = new DataTable();
        Printing prn = new Printing();
          static string id;
        static string name;
        static String[] cmpy;
        static string qty;
        static string price;
        static string amt;
        static string vat;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        private string p;
        private string p_2;
        static int flag;
        private DateWiseReport dateWiseReport;
        private Ledger ledger;
        Connection conn = new Connection();
        public Sale()
        {
            InitializeComponent();
            loadcurrency();
            options = conn.getdataset("select * from options");
            TxtBillNo.ReadOnly = false;

        }

        private void loadcurrency()
        {
            lblrate.Text = "Rate" + Master.currency;
            lblamount.Text = "Amount" + Master.currency;
        }

        public Sale(DateWiseReport dateWiseReport)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            options = conn.getdataset("select * from options");
            this.dateWiseReport = dateWiseReport;
            loadcurrency();
        }

        public Sale(Ledger ledger)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this.ledger = ledger;
            options = conn.getdataset("select * from options");
            loadcurrency();
        }

        //public Sale(CashBook cashBook)
        //{
        //    InitializeComponent();
        //    // TODO: Complete member initialization
        //    this.cashBook = cashBook;
        //    options = conn.getdataset("select * from options");
        //    loadcurrency();
        //}
        OleDbSettings ods = new OleDbSettings();
        private void Sale_Load(object sender, EventArgs e)
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
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
           
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);

            DataSet dtrange = ods.getdata("SELECT SQLSetting.* FROM SQLSetting where OT6='" + Master.companyId + "'");
            TxtRundate.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0][8].ToString());
            TxtRundate.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0][9].ToString());
           
            //    TxtComputerName.Text = Environment.MachineName;
            //TxtRundate.Text = DateTime.Now.ToShortDateString();
           // TxtRundate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            this.ActiveControl = cmbterms;
            //LVFO.Columns.Add("Bill_Prod_ID",0, HorizontalAlignment.Left);
            LVFO.Columns.Add("No.of Bags", 100, HorizontalAlignment.Center);
            LVFO.Columns.Add("Packing", 100, HorizontalAlignment.Center);
            LVFO.Columns.Add("Description of Goods", 385, HorizontalAlignment.Left);
            LVFO.Columns.Add("Qty", 90, HorizontalAlignment.Right);
            LVFO.Columns.Add("Total Qty", 90, HorizontalAlignment.Right);
            // LVFO.Columns.Add("A.Qty", 70, HorizontalAlignment.Center);
            LVFO.Columns.Add(lblrate.Text, 125, HorizontalAlignment.Right);
            LVFO.Columns.Add("Per", 100, HorizontalAlignment.Center);
            LVFO.Columns.Add(lblamount.Text, 100, HorizontalAlignment.Center);
            LVFO.Columns.Add("HST", 80, HorizontalAlignment.Center);
            LVFO.Columns.Add("Total", 100, HorizontalAlignment.Center);
            getsr();
            billno = TxtBillNo.Text;
            bindsaletype();
            bindcustomer();
            autoreaderbind();

            con.Close();
        }
        static string billno;
        public void bindsaletype()
        {
            SqlCommand cmd = new SqlCommand("select Purchasetypeid,Purchasetypename from PurchasetypeMaster where type='S' and isactive=1", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            cmbsaletype.ValueMember = "Purchasetypeid";
            cmbsaletype.DisplayMember = "Purchasetypename";
            cmbsaletype.DataSource = dt;
            cmbsaletype.SelectedIndex = -1;

            autobind(dt, cmbsaletype);
        }
        public void bindcustomer()
        {
            SqlCommand cmd1 = new SqlCommand("select ClientID,AccountName from ClientMaster where isactive=1 order by AccountName", con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            cmbcustname.ValueMember = "ClientID";
            cmbcustname.DisplayMember = "AccountName";
            cmbcustname.DataSource = dt1;
            cmbcustname.SelectedIndex = -1;

            
            autobind(dt1, cmbcustname);
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

                SqlCommand cmd = new SqlCommand("select max(Bill_No) from BillMaster where isactive='1' and billtype='S'", con);
                String str = cmd.ExecuteScalar().ToString();
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
                TxtBillNo.Text = count.ToString();

            }
            catch
            {
            }
            finally
            {

            }

        }
        private void Button17_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = null;
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
                // int i = 0;
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
                   // MessageBox.Show("Please Select Company");
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
        public void clearitem()
        {
            
            txtitemname.Text = string.Empty;
            txtpacking.Text = string.Empty;
            txtbags.Text = string.Empty;
            txtqty.Text = string.Empty;
            txtpqty.Text = string.Empty;
            txtrate.Text = string.Empty;
            txtper.Text = string.Empty;
            txttotal.Text = string.Empty;
            txttax.Text = string.Empty;
            txtamount.Text = string.Empty;
           

        }
        public void clearall()
        {
            getsr();
            //  TxtComputerName.Text = Environment.MachineName;
            TxtRundate.Text = DateTime.Now.ToShortDateString();
            cmbterms.SelectedIndex = -1;
            cmbcustname.SelectedIndex = -1;
            cmbsaletype.SelectedIndex = -1;
            txtpono.Text = string.Empty;
            lbltotpqty.Text = string.Empty;
            lbltaxtot.Text = string.Empty;
            //TxtVATExmptAmnt.Text = string.Empty;
            TxtBillTotal.Text = string.Empty;
            lblbasictot.Text = "0";
            lbltaxtot.Text = "0";
            //lbltotaqty.Text = "0";
            lbltotcount.Text = "0";
            lbltotpqty.Text = "0";
            txtweight.Text = string.Empty;
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
                if (cmbcustname.Text == "")
                {
                    MessageBox.Show("Select Party Name");
                }
                else if (cmbsaletype.Text == "")
                {
                    MessageBox.Show("Select Sale type");
                }
                else
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    DialogResult dr = MessageBox.Show("Do you want to Generate Bill?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        this.Enabled = false;
                        if (BtnPayment.Text == "Update")
                        {
                            SqlCommand cmd2 = new SqlCommand("Update billproductmaster set isactive='0' where bill_no='" + TxtBillNo.Text + "' and BillType='S'", con);
                            cmd2.ExecuteNonQuery();
                            for (int i = 0; i < LVFO.Items.Count; i++)
                            {
                                // SqlCommand cmd1 = new SqlCommand("INSERT INTO [Billing].[dbo].[BillProductMaster]([Bill_No],[Bill_Run_Date],[ProductID],[Product_Qty],[Product_total_Amt],[Free],[Product_Per_rate])VALUES('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString("MM-dd-yyyy") + "','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[2].Text + "','" + Convert.ToDouble(LVFO.Items[i].SubItems[5].Text) + "','" + LVFO.Items[i].SubItems[3].Text + "','" + Convert.ToDouble(LVFO.Items[i].SubItems[4].Text) + "')", con);

                                //SqlCommand cmd2 = new SqlCommand("select billproid from billproductmaster where bill_no='" + TxtBillNo.Text + "' and [Productname] = '" + LVFO.Items[i].SubItems[2].Text + "'", con);
                                //string billproductid = cmd2.ExecuteScalar().ToString();
                                //SqlCommand cmd1 = new SqlCommand("UPDATE [dbo].[BillProductMaster]SET [Bill_No] = '" + TxtBillNo.Text + "',[Bill_Run_Date] = '" + Convert.ToDateTime(TxtRundate.Text).ToString("MM-dd-yyyy") + "',[Productname] = '" + LVFO.Items[i].SubItems[2].Text + "',[Packing] = '" + LVFO.Items[i].SubItems[1].Text + "',[Bags] = '" + LVFO.Items[i].SubItems[0].Text + "',[MRP] = '',[Pqty] = '" + LVFO.Items[i].SubItems[3].Text + "',[Aqty] = '',[Rate] = '" + LVFO.Items[i].SubItems[4].Text + "',[Per] = '" + LVFO.Items[i].SubItems[5].Text + "',[Total] = '" + LVFO.Items[i].SubItems[6].Text.Replace(",", "") + "',[Tax] = '" + LVFO.Items[i].SubItems[7].Text + "',[Amount] = '" + LVFO.Items[i].SubItems[8].Text.Replace(",", "") + "' where Bill_prod_id='" + billproductid + "'", con);
                                //cmd1.ExecuteNonQuery();
                                string productid = conn.ExecuteScalar("select Productid from productmaster where Product_Name like '%" + LVFO.Items[i].SubItems[2].Text + "%'");
                                
                                SqlCommand cmd1 = new SqlCommand("INSERT INTO [dbo].[BillProductMaster]([Bill_No],[Bill_Run_Date],[Productname],[Packing],[Bags],[MRP],[Pqty],[Aqty],[Rate],[Per],[Total],[Tax],[Amount],[isactive],[qty],[BillType],[billno],[productid])VALUES('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString("MM-dd-yyyy") + "','" + LVFO.Items[i].SubItems[2].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','" + LVFO.Items[i].SubItems[0].Text + "','','" + LVFO.Items[i].SubItems[4].Text + "','','" + LVFO.Items[i].SubItems[5].Text + "','" + LVFO.Items[i].SubItems[6].Text + "','" + LVFO.Items[i].SubItems[7].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[8].Text + "','" + LVFO.Items[i].SubItems[9].Text.Replace(",", "") + "',1,'" + LVFO.Items[i].SubItems[3].Text + "','S','" + TxtBillNo.Text + "','"+productid+"')", con);
                                cmd1.ExecuteNonQuery();
                                //total = total + Convert.ToDouble(LVFO.Items[i].SubItems[4].Text);
                                //Double multi = 0;
                                //multi = (Convert.ToDouble(LVFO.Items[i].SubItems[4].Text) * (Convert.ToDouble(LVFO.Items[i].SubItems[5].Text))) / 100;
                                //vat = vat + multi;

                            }
                            SqlCommand cmd = new SqlCommand("UPDATE [dbo].[BillMaster] SET [billno]='"+TxtBillNo.Text+"', [Bill_No] = '" + TxtBillNo.Text + "',[Bill_Date] = '" + Convert.ToDateTime(TxtRundate.Text).ToString("MM-dd-yyyy") + "',[Terms] = '" + cmbterms.Text + "',[ClientID] = '" + cmbcustname.SelectedValue + "',[PO_No] = '" + txtpono.Text + "',[SaleType] = '" + cmbsaletype.SelectedValue + "',[count] = " + lbltotcount.Text + ",[totalqty] = " + lbltotpqty.Text + ",[totalbasic] = " + lblbasictot.Text + ",[totaltax] =" + lbltaxtot.Text + " ,[totalnet] = " + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + ",[isactive]=1, [apprweight]='" + txtweight.Text + "',[dispatchdetails]='" + txtdispatch.Text + "',[remarks]='" + txtremarks.Text + "',[billtype]='S' where Bill_No='" + TxtBillNo.Text + "' and [billtype]='S'", con);
                            cmd.ExecuteNonQuery();
                            string vno = conn.ExecuteScalar("select voucherid from ledger where [VoucherID]= '" + TxtBillNo.Text + "' and [TranType] = 'Sale'");
                            if (vno != "0")
                            {
                                conn.execute("UPDATE [dbo].[Ledger] SET [OT1]='"+cmbterms.Text+"', [Date1]='" + Convert.ToDateTime(TxtRundate.Text).ToString("MM-dd-yyyy") + "',[TranType] = 'Sale',[AccountID] = '" + cmbcustname.SelectedValue + "',[AccountName]='" + cmbcustname.Text + "' ,[Amount] = '" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "',[DC] = 'D' where [VoucherID]= '" + TxtBillNo.Text + "' and [TranType] = 'Sale'");
                            }
                            else
                            {
                                conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString("MM-dd-yyyy") + "','Sale','" + cmbcustname.SelectedValue + "','" + cmbcustname.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','D',1,'" + cmbterms.Text + "')");
                            }

                            //  SqlCommand cmd = new SqlCommand("INSERT INTO [Billing].[dbo].[BillMaster] ([Bill_No] ,[Bill_Date],[ClientID],[PO_No],[Bill_Amt],[Bill_vat_Amt],[Bill_Net_Amt],[CompanyID])VALUES ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString("MM-dd-yyyy") + "','" + cmbcustname.SelectedValue + "','" + txtpono.Text + "','" + Convert.ToDouble(TxtBillTotal.Text) + "','" + Convert.ToDouble(TxtVAT16Amnt.Text) + "','" + Convert.ToDouble(TxtVATExmptAmnt.Text) + "','" + cmp + "')", con);

                            //    cmd = new SqlCommand("INSERT INTO [Billing].[dbo].[PaymentMaster] ([Bill_No],[PaymentStatus])VALUES ('" + TxtBillNo.Text + "','Pending')", con);
                            //    cmd.ExecuteNonQuery();

                            print();
                            clearitem();
                            clearall();
                            LVFO.Items.Clear();
                            
                            BtnPayment.Text = "Save";
                        }
                        else
                        {

                            DataTable getdt=conn.getdataset("select * from billmaster where bill_no='" + TxtBillNo.Text + "' and BillType='S' and isactive=1");
                            if (getdt.Rows.Count > 0)
                            {
                                MessageBox.Show("Bill No Already Available");
                                TxtBillNo.Focus();
                            }
                            else
                            {
                                if (billno == TxtBillNo.Text)
                                {
                                    getsr();
                                }
                                for (int i = 0; i < LVFO.Items.Count; i++)
                                {
                                    // SqlCommand cmd1 = new SqlCommand("INSERT INTO [Billing].[dbo].[BillProductMaster]([Bill_No],[Bill_Run_Date],[ProductID],[Product_Qty],[Product_total_Amt],[Free],[Product_Per_rate])VALUES('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString("MM-dd-yyyy") + "','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[2].Text + "','" + Convert.ToDouble(LVFO.Items[i].SubItems[5].Text) + "','" + LVFO.Items[i].SubItems[3].Text + "','" + Convert.ToDouble(LVFO.Items[i].SubItems[4].Text) + "')", con);
                                    string productid = conn.ExecuteScalar("select Productid from productmaster where Product_Name like '%" + LVFO.Items[i].SubItems[2].Text + "%'");

                                    SqlCommand cmd1 = new SqlCommand("INSERT INTO [dbo].[BillProductMaster]([Bill_No],[Bill_Run_Date],[Productname],[Packing],[Bags],[MRP],[Pqty],[Aqty],[Rate],[Per],[Total],[Tax],[Amount],[isactive],[qty],[BillType],[billno],[productid])VALUES('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString("MM-dd-yyyy") + "','" + LVFO.Items[i].SubItems[2].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','" + LVFO.Items[i].SubItems[0].Text + "','','" + LVFO.Items[i].SubItems[4].Text + "','','" + LVFO.Items[i].SubItems[5].Text + "','" + LVFO.Items[i].SubItems[6].Text + "','" + LVFO.Items[i].SubItems[7].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[8].Text + "','" + LVFO.Items[i].SubItems[9].Text.Replace(",", "") + "',1,'" + LVFO.Items[i].SubItems[3].Text + "','S','" + TxtBillNo.Text + "','" + productid + "')", con);
                                    cmd1.ExecuteNonQuery();

                                    //total = total + Convert.ToDouble(LVFO.Items[i].SubItems[4].Text);
                                    //Double multi = 0;
                                    //multi = (Convert.ToDouble(LVFO.Items[i].SubItems[4].Text) * (Convert.ToDouble(LVFO.Items[i].SubItems[5].Text))) / 100;
                                    //vat = vat + multi;

                                }
                                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[BillMaster]([Bill_No],[billno],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType] ,[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[billtype])VALUES('" + TxtBillNo.Text + "','" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString("MM-dd-yyyy") + "','" + cmbterms.Text + "','" + cmbcustname.SelectedValue + "','" + txtpono.Text + "','" + cmbsaletype.SelectedValue + "'," + lbltotcount.Text + "," + lbltotpqty.Text + "," + lblbasictot.Text + "," + lbltaxtot.Text + "," + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + ",1,'" + txtweight.Text + "','" + txtdispatch.Text + "','" + txtremarks.Text + "','S')", con);
                                //  SqlCommand cmd = new SqlCommand("INSERT INTO [Billing].[dbo].[BillMaster] ([Bill_No] ,[Bill_Date],[ClientID],[PO_No],[Bill_Amt],[Bill_vat_Amt],[Bill_Net_Amt],[CompanyID])VALUES ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString("MM-dd-yyyy") + "','" + cmbcustname.SelectedValue + "','" + txtpono.Text + "','" + Convert.ToDouble(TxtBillTotal.Text) + "','" + Convert.ToDouble(TxtVAT16Amnt.Text) + "','" + Convert.ToDouble(TxtVATExmptAmnt.Text) + "','" + cmp + "')", con);
                                cmd.ExecuteNonQuery();

                                conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString("MM-dd-yyyy") + "','Sale','" + cmbcustname.SelectedValue + "','" + cmbcustname.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','D',1,'" + cmbterms.Text + "')");
                                //     cmd = new SqlCommand("INSERT INTO [Billing].[dbo].[PaymentMaster] ([Bill_No],[PaymentStatus])VALUES ('" + TxtBillNo.Text + "','Pending')", con);
                                //     cmd.ExecuteNonQuery();

                                print();
                            }
                            clearitem();
                            clearall();
                            LVFO.Items.Clear();
                          
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

        private void txtamount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ListViewItem li;
                li = LVFO.Items.Add(txtbags.Text);
                li.SubItems.Add(txtpacking.Text);
                li.SubItems.Add(txtitemname.Text);
                li.SubItems.Add((Math.Round(Convert.ToDouble(txtqty.Text),2).ToString()));
                li.SubItems.Add((Math.Round(Convert.ToDouble(txtpqty.Text),2).ToString()));
               // li.SubItems.Add(txtaqty.Text);
                li.SubItems.Add((Math.Round(Convert.ToDouble(txtrate.Text),2).ToString()));
                li.SubItems.Add(txtper.Text);
                li.SubItems.Add((Math.Round(Convert.ToDouble(txttotal.Text),2).ToString()));
                li.SubItems.Add((Math.Round(Convert.ToDouble(txttax.Text),2).ToString()));
                li.SubItems.Add((Math.Round(Convert.ToDouble(txtamount.Text),2).ToString()));

                //if (Convert.ToBoolean(options.Rows[0]["itemsinasedingorderinsale"].ToString()) == true)
                //{
                //    this.LVFO.ListViewItemSorter = new ListViewItemComparer(2);
                //}

                //sorter.SortColumn = 2;
                
                //sorter.Order = System.Windows.Forms.SortOrder.Ascending;
                //LVFO.Sort();
                
                //LVFO.Sort();
                
                //LVFO.Sorting =System.Windows.Forms.SortOrder.Ascending;
             //   li.SubItems.Add(txtamount.Text);
               
               
                totalcalculation();
                clearitem();
                txtitemname.Focus();
            }
        }
        //[System.STAThreadAttribute()]
        //public static void Main()
        //{
        //    Application.Run(new Sale());
        //}

        private void txtitemname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SqlCommand cmd5 = new SqlCommand("select p.Product_Name,p.Unit,p.Packing,b.* from productmaster p inner join ProductPriceMaster b on p.ProductID=b.ProductID where p.product_name='" + txtitemname.Text + "'", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd5);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    txtpacking.Text = dt.Rows[0]["Packing"].ToString();
                    //  txtbarcode.Text = dt.Rows[0]["Productid"].ToString();
                    //   txtbags.Text = dt.Rows[0]["Bags"].ToString();
                    //txtmrp.Text = dt.Rows[0]["MRP"].ToString();
                    txtrate.Text = dt.Rows[0]["SalePrice"].ToString();
                    txtper.Text = dt.Rows[0]["Unit"].ToString();

                    SqlCommand cmd6 = new SqlCommand("select i.Vat from itemtaxmaster i inner join productmaster p on i.productid=p.productid where p.product_name like'%" + txtitemname.Text + "%' and i.saletypeid like '%" + cmbsaletype.Text + "'", con);
                    SqlDataAdapter sda6 = new SqlDataAdapter(cmd6);
                    DataTable dt1 = new DataTable();
                    sda6.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        txttax.Text = dt1.Rows[0]["Vat"].ToString();
                        txtbags.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Not any Tax Available For This Sale Type");
                        txttax.Text = "0";
                        txtbags.Focus();
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
                        client.Passed(1);
                        client.Show();
                    }
                }
            }
            catch
            {
            }
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            clearall();
            clearitem();
            LVFO.Items.Clear();
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

                //txtaqty.Text = Math.Round(total).ToString();


                double finaltotal = Convert.ToDouble(qty) * Convert.ToDouble(txtrate.Text);

                txttotal.Text = Math.Round(finaltotal, 2).ToString();

                double amount = Math.Round(Convert.ToDouble(txttotal.Text) + ((Convert.ToDouble(txttotal.Text) * Convert.ToDouble(txttax.Text)) / 100), 2);
                txtamount.Text = Math.Round(amount, 2).ToString();
            }
            catch
            {
            }
        }
        private void totalcalculation()
        {
            Int32 count = 0;
            Double total = 0;
            Double vat = 0, basic=0;

            Double pqty=0;
            for (int i = 0; i < LVFO.Items.Count; i++)
            {
                count++;
                pqty = pqty + Convert.ToDouble(LVFO.Items[i].SubItems[4].Text);
              //  aqty = aqty + Convert.ToInt32(LVFO.Items[i].SubItems[5].Text);
                basic = basic + Convert.ToDouble(LVFO.Items[i].SubItems[7].Text);
                total = total + Convert.ToDouble(LVFO.Items[i].SubItems[9].Text);
                //double vatcalc = (100 * Convert.ToDouble(LVFO.Items[i].SubItems[6].Text)) / 105;
                Double multi = 0;
                multi = (Convert.ToDouble(LVFO.Items[i].SubItems[7].Text) * (Convert.ToDouble(LVFO.Items[i].SubItems[8].Text) / 100));
                vat = vat + multi;

            }
            lbltotcount.Text = count.ToString();
            lbltotpqty.Text = pqty.ToString();
           // lbltotaqty.Text = aqty.ToString();
            lblbasictot.Text = basic.ToString();
            TxtBillTotal.Text = Math.Round(total, 2).ToString("N2");
            lbltaxtot.Text = Math.Round(vat, 2).ToString("N2");
            //TxtVATExmptAmnt.Text = Math.Round((total + vat), 2).ToString("N2");
        }
        private void txtpqty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtpqty.Text != "")
                {
                   itemcalculation(txtpqty.Text);
                   
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
        Int32 rowid;
        private void LVFO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (LVFO.SelectedItems.Count > 0)
            {
                rowid = LVFO.FocusedItem.Index;
               txtitemname.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[2].Text;
               txtpacking.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[1].Text;
               txtbags.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[0].Text;
               txtqty.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[3].Text;
               txtpqty.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[4].Text;
           //    txtaqty.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[5].Text;
               txtrate.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[5].Text;
               txtper.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[6].Text;
               txttotal.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[7].Text;
               txttax.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[8].Text;
               txtamount.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[9].Text;
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
                //this.SelectNextControl((Control)sender, true, true, true, true);
                txtpacking.Focus();
                

            }
        }

        private void txtpqty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //this.SelectNextControl((Control)sender, true, true, true, true);
                txtrate.Focus();
            }
        }

        private void txtrate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //this.SelectNextControl((Control)sender, true, true, true, true);
                txtamount.Focus();
            }
        }

        private void txtrate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                itemcalculation(txtpqty.Text);
                //double finaltotal = Convert.ToInt32(txtpqty.Text) * Convert.ToDouble(txtrate.Text);

                //txttotal.Text = Math.Round(finaltotal).ToString("N2");

                //double amount = Convert.ToDouble(txttotal.Text) * Convert.ToDouble(txttax.Text);
                //txtamount.Text = Math.Round(amount).ToString("N2");
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
                    txtpono.Focus();
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
                    txtitemname.Focus();
                }
                else
                {
                    MessageBox.Show("Please Select Sale type");
                    cmbsaletype.Focus();
                }
            }
            if (e.KeyCode == Keys.F3)
            {
                SaletypeEntry pt = new SaletypeEntry(this);
                pt.Show();

            }
            if (e.KeyCode == Keys.F2)
            {
                SaletypeEntry pt = new SaletypeEntry(this);
                pt.updatemode("Sale", cmbsaletype.Text, 1);
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
            //if (keyData == Keys.co)
            //{
            //    btnsubmit();
            //    return true;
            //}

            return base.ProcessCmdKey(ref msg, keyData);
        }




        int cnt = 0;
       // private CashBook cashBook;
        internal void updatemode(string str, string p, int p_2)
        {
            try
            {
                loadpage();
                cnt = 1;
                SqlCommand cmd = new SqlCommand("select * from billmaster where bill_no='" + p + "' and isactive=1 and billtype='S'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                SqlCommand cmd1 = new SqlCommand("select * from billproductmaster where bill_no='" + p + "' and billtype='S' and isactive=1", con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);

                TxtBillNo.Text = dt.Rows[0][0].ToString();
                TxtRundate.Text = Convert.ToDateTime(dt.Rows[0][1].ToString()).ToString("dd/MM/yyyy");
                cmbterms.Text = dt.Rows[0][2].ToString();
                txtweight.Text = dt.Rows[0][12].ToString();
                txtdispatch.Text = dt.Rows[0][13].ToString();
                txtremarks.Text = dt.Rows[0][14].ToString();
                cmd = new SqlCommand("select accountname from clientmaster where clientid='" + dt.Rows[0][3].ToString() + "'", con);
                con.Open();
                string clientname = cmd.ExecuteScalar().ToString();
                //  cmbcustname.SelectedIndex = cmbcustname.Items.IndexOf(clientname);
                cmbcustname.Text = clientname;

                txtpono.Text = dt.Rows[0][4].ToString();
                cmd = new SqlCommand("select Purchasetypename from Purchasetypemaster where type='S' and  Purchasetypeid='" + dt.Rows[0][5].ToString() + "'", con);
                string saletypename = cmd.ExecuteScalar().ToString();
                cmbsaletype.Text = saletypename;
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    ListViewItem li;
                    li = LVFO.Items.Add(dt1.Rows[i][5].ToString());
                    li.SubItems.Add(dt1.Rows[i][4].ToString());
                    li.SubItems.Add(dt1.Rows[i][3].ToString());
                    li.SubItems.Add(dt1.Rows[i][15].ToString());
                    li.SubItems.Add(dt1.Rows[i][7].ToString());
                    // li.SubItems.Add(txtaqty.Text);
                    li.SubItems.Add(dt1.Rows[i][9].ToString());
                    li.SubItems.Add(dt1.Rows[i][10].ToString());
                    li.SubItems.Add(dt1.Rows[i][11].ToString());
                    li.SubItems.Add(dt1.Rows[i][12].ToString());
                    li.SubItems.Add(dt1.Rows[i][13].ToString());
                    //   li.SubItems.Add(txtamount.Text);

                }
                //if (Convert.ToBoolean(options.Rows[0]["itemsinasedingorderinsale"].ToString()) == true)
                //{
                //    this.LVFO.ListViewItemSorter = new ListViewItemComparer(2);
                //}
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
                SqlCommand cmd = new SqlCommand("update billmaster set isactive=0  where bill_no='" + TxtBillNo.Text + "' and billtype='S'", con);
                cmd.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("update billproductmaster set isactive=0 where bill_no='" + TxtBillNo.Text + "' and BillType='S'", con);
                cmd2.ExecuteNonQuery();

                SqlCommand cmd3 = new SqlCommand("update ledger set isactive=0 where voucherid='" + TxtBillNo.Text + "' and trantype='Sale'", con);
                cmd3.ExecuteNonQuery();
                MessageBox.Show("Invoice Delete Successfully");
                this.Close();
                dateWiseReport.bindgrid();
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
                txtqty.Focus();
            }
        }

        private void txtpacking_TextChanged(object sender, EventArgs e)
        {
            if (txtpacking.Text != "")
            {

                //string str = txtpacking.Text.ToUpper();

                //string[] split = str.Split('X');
                //if (split[1] != "")
                //{
                //    Int32 qty = Convert.ToInt32(Convert.ToDouble(txtbags.Text) * Convert.ToDouble(split[1]));
                //    txtpqty.Text = qty.ToString();
                //}
            }
        }

        private void txtqty_TextChanged(object sender, EventArgs e)
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
                cmbsaletype.Focus();
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
                        // Int32 qty = Convert.ToInt32(1 * 1);
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

        private void LVFO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                LVFO.Items[LVFO.FocusedItem.Index].Remove();
                totalcalculation();
            }
        }

        private void TxtBillNo_TextChanged(object sender, EventArgs e)
        {

        }

       

        private void btnprint_Click(object sender, EventArgs e)
        {
            print();
        }
        private void print()
        {
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
            // Phrase inwrd = new Phrase(inword, _font2);

            DialogResult dr1 = MessageBox.Show("Do you want to Print Bill?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr1 == DialogResult.Yes)
            {
                SqlCommand cmd6 = new SqlCommand("select * from clientmaster where isactive=1 and clientID='" + cmbcustname.SelectedValue + "'", con);
                SqlDataAdapter sda6 = new SqlDataAdapter(cmd6);
                DataTable dt1 = new DataTable();
                sda6.Fill(dt1);
                prn.execute("delete from printing");
                int j = 1;
               

                for (int i = 0; i < LVFO.Items.Count; i++)
                {
                    try
                    {
                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34,T35,T36,T37,T38,T39,T40,T41,T42,T43,T44,T45,T46,T47,T48,T49,T50)VALUES";
                        qry += "('" + j++ + "','" + TxtBillNo.Text + "','" + TxtRundate.Text + "','" + cmbterms.Text + "','" + cmbcustname.Text + "','" + txtpono.Text + "','" + cmbsaletype.Text + "','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','" + LVFO.Items[i].SubItems[2].Text + "','" + LVFO.Items[i].SubItems[4].Text + "','" + LVFO.Items[i].SubItems[5].Text + "','" + LVFO.Items[i].SubItems[6].Text + "','" + LVFO.Items[i].SubItems[7].Text + "','" + LVFO.Items[i].SubItems[8].Text + "','" + LVFO.Items[i].SubItems[9].Text + "','','" + lbltotcount.Text + "','" + lbltotpqty.Text + "','','" + lblbasictot.Text + "','" + lbltaxtot.Text + "','" + TxtBillTotal.Text + "','" + dt1.Rows[0][0].ToString() + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][2].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + dt1.Rows[0][11].ToString() + "','" + dt1.Rows[0][12].ToString() + "','" + dt1.Rows[0][13].ToString() + "','" + inword + "','" + LVFO.Items[i].SubItems[3].Text + "','" + txtweight.Text + "','" + txtdispatch.Text + "','" + txtremarks.Text + "','','','','','','','','')";
                        prn.execute(qry);
                    }
                    catch
                    {
                    }
                }
                Print popup = new Print("Sale");
                popup.ShowDialog();
                popup.Dispose();
            }
        }

        private void LVFO_SelectedIndexChanged(object sender, EventArgs e)
        {

        }   
    }
}
