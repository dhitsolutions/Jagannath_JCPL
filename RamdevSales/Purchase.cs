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
    public partial class Purchase : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        private DateWisePurchaseReport dateWisePurchaseReport;
       
        public Purchase()
        {
            InitializeComponent();
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

        public Purchase(DateWisePurchaseReport dateWisePurchaseReport)
        {

            InitializeComponent();
            this.dateWisePurchaseReport = dateWisePurchaseReport;
            loadcurrency();
        }

        public Purchase(Ledger ledger)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this.ledger = ledger;
            loadcurrency();
        }

        private void Purchase_Load(object sender, EventArgs e)
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
            con.Open();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            TxtRundate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            this.ActiveControl = TxtBillNo;
            LVFO.Columns.Add("No.of Bags", 100, HorizontalAlignment.Center);
            LVFO.Columns.Add("Packing", 100, HorizontalAlignment.Center);
            LVFO.Columns.Add("Description of Goods", 385, HorizontalAlignment.Left);
            LVFO.Columns.Add("Qty", 90, HorizontalAlignment.Right);
            LVFO.Columns.Add("Total Qty", 90, HorizontalAlignment.Right);
            LVFO.Columns.Add(lblrate.Text, 125, HorizontalAlignment.Right);
            LVFO.Columns.Add("Per", 100, HorizontalAlignment.Center);
            LVFO.Columns.Add(lblamount.Text, 100, HorizontalAlignment.Center);
            LVFO.Columns.Add("TAX", 80, HorizontalAlignment.Center);
            LVFO.Columns.Add("Add Tax", 80, HorizontalAlignment.Center);
            LVFO.Columns.Add("Total", 100, HorizontalAlignment.Center);
            //getsr();
            bindsaletype();
            bindcustomer();
            autoreaderbind();

            con.Close();
        }
        //void getsr()
        //{
        //    try
        //    {

        //        SqlCommand cmd = new SqlCommand("select max(Bill_No) from BillMaster where isactive='1' and billtype='P' and CompanyId=" + Master.companyId + "", con);
        //        String str = cmd.ExecuteScalar().ToString();
        //        int id, count;

        //        if (str == "")
        //        {

        //            id = 1;
        //            count = 1;
        //        }
        //        else
        //        {
        //            id = Convert.ToInt32(str) + 1;
        //            count = Convert.ToInt32(str) + 1;
        //        }
        //        TxtBillNo.Text = count.ToString();

        //    }
        //    catch
        //    {
        //    }
        //    finally
        //    {

        //    }

        //}
        public void bindsaletype()
        {
            SqlCommand cmd = new SqlCommand("select Purchasetypeid,purchasetypename from Purchasetypemaster where type='P'and isactive=1", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            cmbsaletype.ValueMember = "Purchasetypeid";
            cmbsaletype.DisplayMember = "purchasetypename";
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
            txtqty.Text = string.Empty;
            txtpqty.Text = string.Empty;
            txtrate.Text = string.Empty;
            txtper.Text = string.Empty;
            txttotal.Text = string.Empty;
            txttax.Text = string.Empty;
            txtaddtax.Text = string.Empty;
            txtamount.Text = string.Empty;
            lbladdtax1.Text = "[]";
            lbltax1.Text = "[]";


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
            lbltaxtot.Text = string.Empty;
            TxtBillTotal.Text = string.Empty;
            lblbasictot.Text = "0";
            lbltaxtot.Text = "0";
            lbltotcount.Text = "0";
            lbltotpqty.Text = "0";
            txtweight.Text = string.Empty;
            txtdispatch.Text = string.Empty;
            txtremarks.Text = string.Empty;
            TxtBillNo.Text = "";
            TxtBillNo.Focus();
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
                    DataTable dtpid = new DataTable();

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
                            for (int i = 0; i < LVFO.Items.Count; i++)
                            {
                                dtpid = conn.getdataset("Select Productid from productmaster where product_name='" + LVFO.Items[i].SubItems[2].Text.Replace(",", "") + "'");

                                SqlCommand cmd1 = new SqlCommand("INSERT INTO [dbo].[BillProductMaster]([BillNo],[Bill_Run_Date],[Productname],[Packing],[Bags],[MRP],[Pqty],[Aqty],[Rate],[Per],[Total],[Tax],[addtax],[Amount],[isactive],[qty],[BillType],[Bill_no],[productid])VALUES('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString("dd/MMM/yyyy") + "','" + LVFO.Items[i].SubItems[2].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','" + LVFO.Items[i].SubItems[0].Text + "','','" + LVFO.Items[i].SubItems[4].Text + "','','" + LVFO.Items[i].SubItems[5].Text + "','" + LVFO.Items[i].SubItems[6].Text + "','" + LVFO.Items[i].SubItems[7].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[8].Text + "','" + LVFO.Items[i].SubItems[9].Text + "','" + LVFO.Items[i].SubItems[10].Text.Replace(",", "") + "',1,'" + LVFO.Items[i].SubItems[3].Text + "','P','0','"+dtpid.Rows[0][0].ToString()+"')", con);
                                cmd1.ExecuteNonQuery();

                            }
                            SqlCommand cmd = new SqlCommand("UPDATE [dbo].[BillMaster]SET [BillNo] = '" + TxtBillNo.Text + "',[Bill_Date] = '" + Convert.ToDateTime(TxtRundate.Text).ToString("dd/MMM/yyyy") + "',[Terms] = '" + cmbterms.Text + "',[ClientID] = '" + cmbcustname.SelectedValue + "',[PO_No] = '" + txtpono.Text + "',[SaleType] = '" + cmbsaletype.SelectedValue + "',[count] = " + lbltotcount.Text + ",[totalqty] = " + lbltotpqty.Text + ",[totalbasic] = " + lblbasictot.Text + ",[totaltax] =" + lbltaxtot.Text.Replace(",", "") + " ,[totalnet] = " + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + ",[isactive]=1, [apprweight]='" + txtweight.Text + "',[dispatchdetails]='" + txtdispatch.Text + "',[totaladdtax]='" + txttotaddvat.Text + "',[remarks]='" + txtremarks.Text + "',[billtype]='P',[roudoff]='" + txtroundoff.Text + "' where BillNo='" + TxtBillNo.Text + "' and [billtype]='P' and [CompanyId]=" + Master.companyId + "", con);
                            cmd.ExecuteNonQuery();
                            string vno = conn.ExecuteScalar("select voucherid from ledger where [VoucherID]= '" + TxtBillNo.Text + "' and [TranType] = 'Purchase'");
                            if (vno != "0")
                            {
                                conn.execute("UPDATE [dbo].[Ledger] SET [Date1]='" + Convert.ToDateTime(TxtRundate.Text).ToString("dd/MMM/yyyy") + "',[TranType] = 'Purchase',[AccountID] = '" + cmbcustname.SelectedValue + "',[AccountName]='" + cmbcustname.Text + "' ,[Amount] = '" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "',[DC] = 'C' where [VoucherID]= '" + TxtBillNo.Text + "' and [TranType] = 'Purchase'");
                            }
                            else
                            {
                                conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[CompanyId]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString("dd/MMM/yyyy") + "','Purchase','" + cmbcustname.SelectedValue + "','" + cmbcustname.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','C',1," + Master.companyId + ")");
                            }

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

                            clearitem();
                            clearall();
                            LVFO.Items.Clear();
                            MessageBox.Show("Data Updated Successfully.");

                            BtnPayment.Text = "Save";
                            this.Hide();
                        }
                        else
                        {
                           // getsr();
                            for (int i = 0; i < LVFO.Items.Count; i++)
                            {
                                dtpid = conn.getdataset("Select Productid from productmaster where product_name='" + LVFO.Items[i].SubItems[2].Text.Replace(",", "") + "'");

                                SqlCommand cmd1 = new SqlCommand("INSERT INTO [dbo].[BillProductMaster]([BillNo],[Bill_Run_Date],[Productname],[Packing],[Bags],[MRP],[Pqty],[Aqty],[Rate],[Per],[Total],[Tax],[addtax],[Amount],[isactive],[qty],[BillType],[Bill_no],[productid])VALUES('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString("dd/MMM/yyyy") + "','" + LVFO.Items[i].SubItems[2].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','" + LVFO.Items[i].SubItems[0].Text + "','','" + LVFO.Items[i].SubItems[4].Text + "','','" + LVFO.Items[i].SubItems[5].Text + "','" + LVFO.Items[i].SubItems[6].Text + "','" + LVFO.Items[i].SubItems[7].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[8].Text + "','" + LVFO.Items[i].SubItems[9].Text + "','" + LVFO.Items[i].SubItems[10].Text.Replace(",", "") + "',1,'" + LVFO.Items[i].SubItems[3].Text + "','P','0','"+dtpid.Rows[0][0].ToString()+"')", con);
                                cmd1.ExecuteNonQuery();

                            }
                            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[BillMaster]([BillNo],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType] ,[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[billtype],[Bill_no],[totaladdtax],[roudoff],[CompanyId])VALUES('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString("dd/MMM/yyyy") + "','" + cmbterms.Text + "','" + cmbcustname.SelectedValue + "','" + txtpono.Text + "','" + cmbsaletype.SelectedValue + "'," + Math.Round(Convert.ToDouble(lbltotcount.Text), 2) + "," + Math.Round(Convert.ToDouble(lbltotpqty.Text), 2) + "," + Math.Round(Convert.ToDouble(lblbasictot.Text), 2) + "," + Math.Round(Convert.ToDouble(lbltaxtot.Text), 2) + "," + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + ",1,'" + txtweight.Text + "','" + txtdispatch.Text + "','" + txtremarks.Text + "','P','0','" + Math.Round(Convert.ToDouble(txttotaddvat.Text), 2) + "','" + Math.Round(Convert.ToDouble(txtroundoff.Text), 2) + "'," + Master.companyId + ")", con);
                            cmd.ExecuteNonQuery();

                            conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[CompanyId]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString("dd/MMM/yyyy") + "','Purchase','" + cmbcustname.SelectedValue + "','" + cmbcustname.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','C',1," + Master.companyId + ")");

                            clearitem();
                            clearall();
                            LVFO.Items.Clear();
                            MessageBox.Show("Data Inserted Successfully.");
                            this.Hide();

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

        double addtax;
        private void txtamount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ListViewItem li;
                li = LVFO.Items.Add(txtbags.Text);
                li.SubItems.Add(txtpacking.Text);
                li.SubItems.Add(txtitemname.Text);
                li.SubItems.Add((Math.Round(Convert.ToDouble(txtqty.Text), 2).ToString()));
                li.SubItems.Add((Math.Round(Convert.ToDouble(txtpqty.Text), 2).ToString()));
                li.SubItems.Add((Math.Round(Convert.ToDouble(txtrate.Text), 2).ToString()));
                li.SubItems.Add(txtper.Text);
                li.SubItems.Add((Math.Round(Convert.ToDouble(txttotal.Text), 2).ToString()));
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
                    SqlCommand cmd5 = new SqlCommand("select p.Product_Name,p.Unit,p.Packing,b.* from productmaster p inner join ProductPriceMaster b on p.ProductID=b.ProductID where p.product_name='" + txtitemname.Text + "'", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd5);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    txtpacking.Text = dt.Rows[0]["Packing"].ToString();
                    txtrate.Text = dt.Rows[0]["PurchasePrice"].ToString();
                    txtper.Text = dt.Rows[0]["Unit"].ToString();

                    SqlCommand cmd6 = new SqlCommand("select * from itemtaxmaster i inner join productmaster p on i.productid=p.productid where p.product_name like'%" + txtitemname.Text + "%' and i.saletypeid like '%" + cmbsaletype.Text + "'", con);
                    SqlDataAdapter sda6 = new SqlDataAdapter(cmd6);
                    DataTable dt1 = new DataTable();
                    sda6.Fill(dt1);
                    string istax = conn.ExecuteScalar("Select taxtypename from purchasetypemaster where type='P' and isactive=1 and purchasetypename='" + cmbsaletype.Text + "'");
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
                        
                        txtbags.Focus();
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
                
                double tax = Math.Round(Convert.ToDouble(txttotal.Text) * (Convert.ToDouble(taxid.ToString())) / 100, 2);
                double addtax = Math.Round(Convert.ToDouble(txttotal.Text) * (Convert.ToDouble(addtaxid.ToString())) / 100, 2);
                double amount = Math.Round(Convert.ToDouble(txttotal.Text) + ((Convert.ToDouble(txttotal.Text) * (Convert.ToDouble(taxid.ToString()) + Convert.ToDouble(addtaxid.ToString()))) / 100), 2);
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
                Double vat = 0, basic = 0;
                Double addvat = 0;

                Double pqty = 0;
                for (int i = 0; i < LVFO.Items.Count; i++)
                {
                    count++;
                    pqty = pqty + Convert.ToDouble(LVFO.Items[i].SubItems[4].Text);
                    basic = basic + Convert.ToDouble(LVFO.Items[i].SubItems[7].Text);
                    total = total + Convert.ToDouble(LVFO.Items[i].SubItems[10].Text);
                    vat+=Convert.ToDouble(LVFO.Items[i].SubItems[8].Text);
                    addvat += Convert.ToDouble(LVFO.Items[i].SubItems[9].Text);
                    Double multi = 0, add = 0;
                   
                    //multi = (Convert.ToDouble(LVFO.Items[i].SubItems[7].Text) * (Convert.ToDouble(LVFO.Items[i].SubItems[8].Text) / 100));
                    //vat = vat + multi;
                    //add = (Convert.ToDouble(LVFO.Items[i].SubItems[7].Text) * (Convert.ToDouble(LVFO.Items[i].SubItems[9].Text) / 100));
                    //addvat += add;

                }
                lbltotcount.Text = count.ToString();
                lbltotpqty.Text = pqty.ToString();
                lblbasictot.Text = basic.ToString();
                lbltaxtot.Text = Math.Round(vat, 2).ToString("N2");
                txttotaddvat.Text = Math.Round(addvat, 2).ToString("N2");
                TxtBillTotal.Text = Math.Round(total, 2).ToString("N2");
                getOptions(Math.Round(total, 2));
                
              
            }
            catch
            {
            }
        }

        private void getOptions(Double total)
        {
            DataTable dt= conn.getdataset("select * from options");
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dt.Rows[0]["autoroundoffpurchase"].ToString()) == true)
                {
                    TxtBillTotal.Text = Math.Round(total, 0).ToString("N2");
                    txtroundoff.Text = Math.Round((Math.Round(total, 0) - total), 2).ToString();

                }
            }

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

        private void LVFO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (LVFO.SelectedItems.Count > 0)
            {
                txtitemname.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[2].Text;
                txtpacking.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[1].Text;
                txtbags.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[0].Text;
                txtqty.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[3].Text;
                txtpqty.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[4].Text;
                txtrate.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[5].Text;
                txtper.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[6].Text;
                txttotal.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[7].Text;
                txttax.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[8].Text;
                txtaddtax.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[9].Text;
                txtamount.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[10].Text;
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
                txtpacking.Focus();


            }
        }

        private void txtpqty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtrate.Focus();
            }
        }

        private void txtrate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtamount.Focus();
            }
        }

        private void txtrate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                itemcalculation(txtpqty.Text);
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
                SqlCommand cmd = new SqlCommand("select * from billmaster where billno='" + p + "' and isactive=1 and billtype='P' and CompanyId="+Master.companyId+"", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                SqlCommand cmd1 = new SqlCommand("select * from billproductmaster where billno='" + p + "' and billtype='P' and isactive=1 ", con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                 DataTable dtpidu = new DataTable();
                if (dt.Rows.Count > 0)
                {
                    TxtBillNo.Text = dt.Rows[0]["billno"].ToString();
                    TxtRundate.Text = Convert.ToDateTime(dt.Rows[0][1].ToString()).ToString("yyyy-MM-dd");
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
                    cmd = new SqlCommand("select purchasetypename from Purchasetypemaster where type='P' and purchasetypeid='" + dt.Rows[0][5].ToString() + "'", con);
                    string saletypename = cmd.ExecuteScalar().ToString();
                    cmbsaletype.Text = saletypename;
                }
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        ListViewItem li;
                        dtpidu = conn.getdataset("Select product_name from productmaster where productid=" + dt1.Rows[i]["Productid"].ToString() + "");

                        li = LVFO.Items.Add(dt1.Rows[i][5].ToString());
                        li.SubItems.Add(dt1.Rows[i][4].ToString());
                        li.SubItems.Add(dtpidu.Rows[0][0].ToString());
                        li.SubItems.Add(dt1.Rows[i][15].ToString());
                        li.SubItems.Add(dt1.Rows[i][7].ToString());
                        // li.SubItems.Add(txtaqty.Text);
                        li.SubItems.Add(dt1.Rows[i][9].ToString());
                        li.SubItems.Add(dt1.Rows[i][10].ToString());
                        li.SubItems.Add(dt1.Rows[i][11].ToString());
                        li.SubItems.Add(dt1.Rows[i][12].ToString());
                        li.SubItems.Add(dt1.Rows[i]["addtax"].ToString());
                        li.SubItems.Add(dt1.Rows[i][13].ToString());
                        //   li.SubItems.Add(txtamount.Text);

                    }
                }
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
                DialogResult dr = MessageBox.Show("Do you want to Delete?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    this.Enabled = false;
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update billmaster set isactive=0  where billno='" + TxtBillNo.Text + "' and billtype='P' and CompanyId=" + Master.companyId + "", con);
                    cmd.ExecuteNonQuery();

                    SqlCommand cmd2 = new SqlCommand("update billproductmaster set isactive=0 where billno='" + TxtBillNo.Text + "' and BillType='P'", con);
                    cmd2.ExecuteNonQuery();

                    SqlCommand cmd3 = new SqlCommand("update ledger set isactive=0 where voucherid='" + TxtBillNo.Text + "' and trantype='Purchase' and CompanyId=" + Master.companyId + "", con);
                    cmd3.ExecuteNonQuery();
                    MessageBox.Show("Delete Successfully");
                    this.Hide();
                }
                else
                {
                    cmbterms.Focus();
                }
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

        private void TxtBillNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtRundate.Focus();
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

        private void Label13_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void txtremarks_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtBillTotal_TextChanged(object sender, EventArgs e)
        {

        }
               
    }
}
