﻿using System;
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
using LoggingFramework;

namespace RamdevSales
{
    public partial class PurchaseOrder : Form
    {
        OleDbSettings ods = new OleDbSettings();
        public DataSet ds;
        public DataTable dt, dtpo;
        public SqlConnection con;
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        private DateWisePurchaseOrderReport dateWisePurchaseOrderReport;
        public PurchaseOrder()
        {
            InitializeComponent();
        }

        public PurchaseOrder(DateWisePurchaseOrderReport dateWisePurchaseOrderReport)
        {
            InitializeComponent();
            this.dateWisePurchaseOrderReport = dateWisePurchaseOrderReport;
        }

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

        private void PurchaseOrder_Load(object sender, EventArgs e)
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
            getcon();
            con.Open();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            TxtBillDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            this.ActiveControl = txtVchNo;

            DataTable dt3 = new DataTable();

            dt3 = conn.getdataset("select a, u, d, v, p, mId, uId, cId from UserRights where mId=5 and uId='" + UserLogin.id + "' and cId= " + Master.companyId + " and isActive=1");

            if (dt3.Rows.Count > 0)
            {

                if (Convert.ToBoolean(dt3.Rows[0][2]) == false)
                {
                    btndelete.Visible = false;
                }
            }

            LVFO.Columns.Add("Items", 300, HorizontalAlignment.Left);
            LVFO.Columns.Add("Qty", 60, HorizontalAlignment.Right);
            LVFO.Columns.Add("Description of Items", 360, HorizontalAlignment.Right);
            getsr();
            bindPurchaseType();
            bindCustomer();
            autoReaderBind();

            con.Close();
        }

        void getsr()
        {
            try
            {
                getcon();
                con.Open();
                SqlCommand cmd = new SqlCommand("select max(VchNo) from PurchaseOrderMaster where isactive='1'", con);
                String str = cmd.ExecuteScalar().ToString();
                int id, count;

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
                txtVchNo.Text = count.ToString();
                TxtBillNo.Text = count.ToString();
            }
            catch
            {
                con.Close();
            }
            finally
            {
                con.Close();
            }

        }

        public void bindPurchaseType()
        {
            getcon();
            SqlCommand cmd = new SqlCommand("select purchasetypeid,purchasetypename from PurchasetypeMaster", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            cmbPurchaseType.ValueMember = "purchasetypeid";
            cmbPurchaseType.DisplayMember = "purchasetypename";
            cmbPurchaseType.DataSource = dt;
            cmbPurchaseType.SelectedIndex = -1;

            autobind(dt, cmbPurchaseType);
        }

        public void bindCustomer()
        {
            getcon();
            SqlCommand cmd1 = new SqlCommand("select ClientID,AccountName from ClientMaster WHERE groupname like '%SUPPLIERS%' order by AccountName", con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            cmbCustName.ValueMember = "ClientID";
            cmbCustName.DisplayMember = "AccountName";
            cmbCustName.DataSource = dt1;
            cmbCustName.SelectedIndex = -1;


            autobind(dt1, cmbCustName);
        }

        public void autoReaderBind()
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
            txtDesc.Text = string.Empty;
        }

        public void clearall()
        {
            getsr();
            TxtBillDate.Text = DateTime.Now.ToShortDateString();
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
                getcon();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                dtpo = conn.getdataset("Select isGenerated,GeneDate from PurchaseOrderMaster where isactive=1 order by vchno desc ");
                DialogResult dr = MessageBox.Show("Do you want to Generate Purchase Order?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                        DataTable dtpid = new DataTable();

                        if (dtpo.Rows.Count > 0 && dtpo.Rows[0][1].ToString() != string.Empty)
                        {

                            if (dtpo.Rows[0][0].ToString() == "1")
                            {

                                //DateTime date = Convert.ToDateTime(dtpo.Rows[0][1]).Date;
                                //DateTime curdate = DateTime.Now.Date;
                                //if (date == curdate)
                                //{
                                //TimeSpan d = Convert.ToDateTime(dtpo.Rows[0][1]).TimeOfDay;
                                //TimeSpan cd = DateTime.Now.TimeOfDay;
                                //if (cd > TimeSpan.FromHours(23))
                                //{
                                //DialogResult dr1 = MessageBox.Show("Purchase Report is generated. Do you want to Generate it tomorrow?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                //if (dr1 == DialogResult.Yes)
                                //{

                                //if (btnPayment.Text == "Update")
                                //{
                                string qry = "Update PurchaseOrderProductMaster set isactive='0',syncDatetime='" + DateTime.Now + "' where VchNo='" + txtVchNo.Text + "' and  CompanyId=" + Master.companyId + "";
                                SqlCommand cmd2 = new SqlCommand("Update PurchaseOrderProductMaster set isactive='0',syncDatetime='"+DateTime.Now+"' where VchNo='" + txtVchNo.Text + "' and  CompanyId=" + Master.companyId + "", con);
                                cmd2.ExecuteNonQuery();
                                LogGenerator.Info(qry);
                              //  string qry = "Update PurchaseOrderProductMaster set isactive='0' where VchNo='" + txtVchNo.Text + "' and  CompanyId=" + Master.companyId + "";
                           //     conn.execute("Insert Into sync values('" + qry.Replace("'", "\"") + "')");
                                for (int i = 0; i < LVFO.Items.Count; i++)
                                {
                                    //getsyncid();
                                    //con.Open();
                                    dtpid = conn.getdataset("Select Productid from productmaster where product_name='" + LVFO.Items[i].SubItems[0].Text.Replace(",", "") + "'");
                                    Guid guid;
                                    guid = Guid.NewGuid();
                                    SqlCommand cmd1 = new SqlCommand("INSERT INTO [dbo].[PurchaseOrderProductMaster]([VchNo],[OrderNo],[OrderRunDate],[OrderStatus],[ProductName],[Qty],[Free],[Rate],[Per],[BasicAmount],[DiscountPer],[Discount],[Vat],[AddVat],[Total],[isactive],[Description],[isscyn],[IdToSync],[ProductId],[CompanyId],[syncDatetime])VALUES('" + txtVchNo.Text + "','" + TxtBillNo.Text + "','" + DateTime.Now + "','Pending','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','0','0','0','0','0','0','0','0','0',1,'" + LVFO.Items[i].SubItems[2].Text + "',0,'" + guid + "','" + dtpid.Rows[0][0].ToString() + "','" + Master.companyId + "','" + DateTime.Now + "')", con);
                                    cmd1.ExecuteNonQuery();
                                    string qry1 = "INSERT INTO [dbo].[PurchaseOrderProductMaster]([VchNo],[OrderNo],[OrderRunDate],[OrderStatus],[ProductName],[Qty],[Free],[Rate],[Per],[BasicAmount],[DiscountPer],[Discount],[Vat],[AddVat],[Total],[isactive],[Description],[isscyn],[IdToSync],[ProductId],[CompanyId],[syncDatetime])VALUES('" + txtVchNo.Text + "','" + TxtBillNo.Text + "','" + DateTime.Now.AddDays(1) + "','Pending','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','0','0','0','0','0','0','0','0','0',1,'" + LVFO.Items[i].SubItems[2].Text + "',0,'" + guid + "','" + dtpid.Rows[0][0].ToString() + "','" + Master.companyId + "','" + DateTime.Now + "')";
                                    LogGenerator.Info(qry1);
                               //   string  qry = "INSERT INTO [dbo].[PurchaseOrderProductMaster]([VchNo],[OrderNo],[OrderRunDate],[OrderStatus],[ProductName],[Qty],[Free],[Rate],[Per],[BasicAmount],[DiscountPer],[Discount],[Vat],[AddVat],[Total],[isactive],[Description],[isscyn],[IdToSync],[ProductId],[CompanyId],[syncDatetime])VALUES('" + txtVchNo.Text + "','" + TxtBillNo.Text + "','" + DateTime.Now.AddDays(1) + "','Pending','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','0','0','0','0','0','0','0','0','0',1,'" + LVFO.Items[i].SubItems[2].Text + "',0,'" + guid + "','" + dtpid.Rows[0][0].ToString() + "','" + Master.companyId +"','"+DateTime.Now+ "')";
                                 //   conn.execute("Insert into sync values('" + qry.Replace("'", "\"") + "')");


                                }
                                //Guid guid1;
                                //guid1 = Guid.NewGuid();
                                SqlCommand cmd = new SqlCommand("UPDATE [dbo].[PurchaseOrderMaster]SET [VChNo]='" + txtVchNo.Text + "',[OrderNo] = '" + TxtBillNo.Text + "',[OrderDate] = '" + DateTime.Now + "',[Terms] = '" + cmbTerms.Text + "',[ClientID] = '" + cmbCustName.SelectedValue + "',[PurchaseType] = '" + cmbPurchaseType.SelectedValue + "',[Count] = " + lbltotcount.Text + ",[TotalQty] = " + lbltotpqty.Text + ",[isactive]=1,[isscyn]=0,syncDatetime="+DateTime.Now+ "' where VchNo='" + txtVchNo.Text + "' and CompanyId=" + Master.companyId + "", con);
                                cmd.ExecuteNonQuery();
                                qry = "UPDATE [dbo].[PurchaseOrderMaster]SET [VChNo]='" + txtVchNo.Text + "',[OrderNo] = '" + TxtBillNo.Text + "',[OrderDate] = '" + DateTime.Now + "',[Terms] = '" + cmbTerms.Text + "',[ClientID] = '" + cmbCustName.SelectedValue + "',[PurchaseType] = '" + cmbPurchaseType.SelectedValue + "',[Count] = " + lbltotcount.Text + ",[TotalQty] = " + lbltotpqty.Text + ",[isactive]=1,[isscyn]=0,syncDatetime=" + DateTime.Now + "' where VchNo='" + txtVchNo.Text + "' and CompanyId=" + Master.companyId + "";
                             //   conn.execute("Insert into sycn values('" + qry.Replace("'", "\"") + "')");
                                LogGenerator.Info(qry);
                                clearitem();
                                clearall();
                                LVFO.Items.Clear();
                                btnPayment.Text = "Save";
                                this.Hide();
                                //}

                                //else
                                //{
                                //    getsr();
                                //    con.Open();
                                //    for (int i = 0; i < LVFO.Items.Count; i++)
                                //    {
                                //        //getsyncid();
                                //        //con.Open();
                                //        dtpid = conn.getdataset("Select Productid from productmaster where product_name='" + LVFO.Items[i].SubItems[0].Text.Replace(",", "") + "'");

                                //        SqlCommand cmd1 = new SqlCommand("INSERT INTO [dbo].[PurchaseOrderProductMaster]([VchNo],[OrderNo],[OrderRunDate],[OrderStatus],[ProductName],[Qty],[Free],[Rate],[Per],[BasicAmount],[DiscountPer],[Discount],[Vat],[AddVat],[Total],[isactive],[Description],[isscyn],[IdToSync],[ProductId])VALUES('" + txtVchNo.Text + "','" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtBillDate.Text).AddDays(1).ToString("dd-MMM-yyyy") + "','Pending','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','0','0','0','0','0','0','0','0','0',1,'" + LVFO.Items[i].SubItems[2].Text + "',0,'" + new Guid() + "','" + dtpid.Rows[0][0].ToString() + "')", con);
                                //        // SqlCommand cmd1 = new SqlCommand("INSERT INTO [dbo].[PurchaseOrderProductMaster]([VchNo],[OrderNo],[OrderRunDate],[OrderStatus],[ProductName],[Qty],[Description],[isactive])VALUES('" + txtVchNo.Text + "','" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtBillDate.Text).ToString("dd/MMM/yyyy") + "','Pending','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','"+txtDesc.Text+"',1)", con);
                                //        cmd1.ExecuteNonQuery();

                                //    }

                                //    SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[PurchaseOrderMaster]([VchNo],[OrderNo],[OrderDate],[OrderStatus],[Terms],[ClientID],[PurchaseType],[Count],[TotalQty],[TotalBasic],[TotalTax],[TotalAddTax],[TotalDiscount],[TotalNet],[isactive],[DispatchDetails],[Remarks],[CompanyId],[isGenerated],[GeneDate],[isInvoice],[InvoiceDate],[isscyn])VALUES('" + txtVchNo.Text + "','" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtBillDate.Text).AddDays(1).ToString("dd-MMM-yyyy").TrimEnd() + "','Pending','" + cmbTerms.Text + "','" + cmbCustName.SelectedValue + "','" + cmbPurchaseType.SelectedValue + "'," + lbltotcount.Text + "," + lbltotpqty.Text + "," + lblbasictot.Text + "," + lbltaxtot.Text + "," + txtaddtax.Text + "," + lbltotaldiscount.Text + "," + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + ",1,'" + txtdispatch.Text + "','" + txtremarks.Text + "'," + Master.companyId + ",0,@param1,0,@param2,0)", con);
                                //    // SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[PurchaseOrderMaster]([VchNo],[OrderNo],[OrderDate],[OrderStatus],[Terms],[ClientID],[PurchaseType],[Count],[TotalQty],[isactive],[CompanyId],[isGenerated],[GeneDate],[isInvoice],[InvoiceDate])VALUES('" + txtVchNo.Text + "','" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtBillDate.Text).ToString("dd/MMM/yyyy").TrimEnd() + "','Pending','" + cmbTerms.Text + "','" + cmbCustName.SelectedValue + "','" + cmbPurchaseType.SelectedValue + "'," + lbltotcount.Text + "," + lbltotpqty.Text + ",1," + Master.companyId + ",0,@param1,0,@param2)", con);
                                //    cmd.Parameters.Add("@param1", DateTime.Now.ToString("dd-MMM-yyyy"));
                                //    cmd.Parameters.Add("@param2", DateTime.Now.ToString("dd-MMM-yyyy"));
                                //    cmd.ExecuteNonQuery();

                                //    clearitem();
                                //    clearall();
                                //    LVFO.Items.Clear();

                                // }
                                //}

                                //else
                                //{
                                //    this.Close();
                                //}
                                //}
                                //else
                                //{
                                //    btnPayment.Text = "Update";
                                //    //DialogResult dr1 = MessageBox.Show("Purchase Report is generated. Do you want to Update it?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                //    //if (dr1 == DialogResult.Yes)
                                //    //{
                                //        SqlCommand cmd2 = new SqlCommand("Update PurchaseOrderProductMaster set isactive='0' where VchNo='" + txtVchNo.Text + "'", con);
                                //        cmd2.ExecuteNonQuery();
                                //        for (int i = 0; i < LVFO.Items.Count; i++)
                                //        {
                                //            getsyncid();
                                //            con.Open();
                                //            dtpid = conn.getdataset("Select Productid from productmaster where product_name='" + LVFO.Items[i].SubItems[0].Text.Replace(",", "") + "'");

                                //            SqlCommand cmd1 = new SqlCommand("INSERT INTO [dbo].[PurchaseOrderProductMaster]([VchNo],[OrderNo],[OrderRunDate],[OrderStatus],[ProductName],[Qty],[Free],[Rate],[Per],[BasicAmount],[DiscountPer],[Discount],[Vat],[AddVat],[Total],[isactive],[Description],[isscyn],[IdToSync],[ProductId])VALUES('" + txtVchNo.Text + "','" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtBillDate.Text).AddDays(1).ToString("yyyy-MM-dd") + "','Pending','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','0','0','0','0','0','0','0','0','0',1,'" + LVFO.Items[i].SubItems[2].Text + "',0,'" + new Guid() + "','" + dtpid.Rows[0][0].ToString() + "')", con);

                                //            ////SqlCommand cmd1 = new SqlCommand("INSERT INTO [dbo].[PurchaseOrderProductMaster]([VchNo],[OrderNo],[OrderRunDate],[ProductName],[Qty],[Description],[isactive])VALUES('" + txtVchNo.Text + "','" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtBillDate.Text).ToString("dd/MMM/yyyy") + "','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','" + LVFO.Items[i].SubItems["Description"].Text + "',1)", con);
                                //           // SqlCommand cmd1 = new SqlCommand("INSERT INTO [dbo].[PurchaseOrderProductMaster]([VchNo],[OrderNo],[OrderRunDate],[OrderStatus],[ProductName],[Qty],[Free],[Rate],[Per],[BasicAmount],[DiscountPer],[Discount],[Vat],[AddVat],[Total],[isactive],[Description],[isscyn],[isscynID])VALUES('" + txtVchNo.Text + "','" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtBillDate.Text).ToString("yyyy-MM-dd") + "','Pending','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','0','0','0','0','0','0','0','0','0',1,'" + LVFO.Items[i].SubItems[2].Text + "',0," + syncid + ")", con);
                                //            cmd1.ExecuteNonQuery();

                                //        }

                                //        SqlCommand cmd = new SqlCommand("UPDATE [dbo].[PurchaseOrderMaster]SET [VChNo]='" + txtVchNo.Text + "',[OrderNo] = '" + TxtBillNo.Text + "',[OrderDate] = '" + Convert.ToDateTime(TxtBillDate.Text).ToString("yyyy-MM-dd") + "',[Terms] = '" + cmbTerms.Text + "',[ClientID] = '" + cmbCustName.SelectedValue + "',[PurchaseType] = '" + cmbPurchaseType.SelectedValue + "',[Count] = " + lbltotcount.Text + ",[TotalQty] = " + lbltotpqty.Text + ",[isactive]=1, [isscyn]=0 where VchNo='" + txtVchNo.Text + "' and CompanyId=" + Master.companyId + "", con);
                                //        cmd.ExecuteNonQuery();

                                //        clearitem();
                                //        clearall();
                                //        LVFO.Items.Clear();
                                //        MessageBox.Show("Data Updated Successfully.");

                                //        btnPayment.Text = "Save";
                                //        this.Hide();

                                //}
                                // }


                                //else
                                //{
                                //    insertorupdate();
                                //    MessageBox.Show("Purchase order generated successfully.");
                                //}
                            }
                            else
                            {
                                // btnPayment.Text = "Update";
                                insertorupdate();
                                MessageBox.Show("Purchase order generated successfully.");
                            }

                        }

                        else
                        {
                            insertorupdate();
                            MessageBox.Show("Purchase order generated successfully.");
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
                LogGenerator.Error(ex.Message,ex);
            }
            finally
            {
                con.Close();
            }
        }

        private void insertorupdate()
        {
            DataTable dtpid = new DataTable();
            if (btnPayment.Text == "Update")
            {
                string qry = "Update PurchaseOrderProductMaster set isactive='0',syncDatetime='" + DateTime.Now + "' where VchNo='" + txtVchNo.Text + "'and CompanyId=" + Master.companyId + "";
              //  SqlCommand cmd2 = new SqlCommand("Update PurchaseOrderProductMaster set isactive='0',syncDatetime='"+DateTime.Now+"' where VchNo='" + txtVchNo.Text + "'and CompanyId=" + Master.companyId + "", con);
                SqlCommand cmd2=new SqlCommand(qry,con);
                cmd2.ExecuteNonQuery();
                LogGenerator.Info(qry);
                //string qryd = "Update PurchaseOrderProductMaster set isactive='0' where VchNo='" + txtVchNo.Text + "'and CompanyId=" + Master.companyId + "";
                //conn.execute("Insert into sync values('" + qryd.Replace("'", "\"") + "')");
                // DataTable dtits = new DataTable();
                //dtits = conn.getdataset("Select * from purchaseorderproductmaster where vchno ='" + txtVchNo.Text + "'");
                //if (dtits.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dtits.Rows.Count; i++)
                //    {
                //       // dtpid = conn.getdataset("Select Productid from productmaster where product_name='" + LVFO.Items[i].SubItems[0].Text.Replace(",", "") + "'");

                //        //dt = conn.getdataset("Select IdToSync from purchaseorderproductmaster where vchno ='" + txtVchNo.Text + "'");
                //        Guid guid;
                //        guid = Guid.NewGuid();
                //        string qry = "INSERT INTO [dbo].[PurchaseOrderProductMaster]([VchNo],[OrderNo],[OrderRunDate],[OrderStatus],[ProductName],[Qty],[Free],[Rate],[Per],[BasicAmount],[DiscountPer],[Discount],[Vat],[AddVat],[Total],[isactive],[Description],[isscyn],[IdToSync],[ProductId],[CompanyId])VALUES('" + txtVchNo.Text + "','" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtBillDate.Text).AddDays(1).ToString("dd-MMM-yyyy") + "','Pending','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','0','0','0','0','0','0','0','0','0',1,'" + LVFO.Items[i].SubItems[2].Text + "',0,'" + guid + "','" + dtits.Rows[0][0].ToString() + "','" + Master.companyId + "')";
                //        //string qry = "Update [dbo].[PurchaseOrderProductMaster] SET [Vchno]='" + txtVchNo.Text + "',[OrderNo]='" + TxtBillNo.Text + "', [OrderRunDate]='" + DateTime.Now + "',[OrderStatus]='Pending',[ProductName]='" + LVFO.Items[i].SubItems[0].Text + "',[Qty]='" + LVFO.Items[i].SubItems[1].Text + "',[Description]='" + LVFO.Items[i].SubItems[2].Text + "' where [VchNo]='" + txtVchNo.Text + "' and [IdToSync]='" + dtits.Rows[i]["IdToSync"].ToString() + "' and  CompanyId=" + Master.companyId + "";

                //        SqlCommand cmd1 = new SqlCommand(qry, con);
                //        conn.execute("Insert into sync values('"+qry.Replace("'","\"")+"')");
                //        //SqlCommand cmd1 = new SqlCommand("INSERT INTO [dbo].[PurchaseOrderProductMaster]([VchNo],[OrderNo],[OrderRunDate],[OrderStatus],[ProductName],[Qty],[Free],[Rate],[Per],[BasicAmount],[DiscountPer],[Discount],[Vat],[AddVat],[Total],[isactive],[Description],[isscyn],[isscynID])VALUES('" + txtVchNo.Text + "','" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtBillDate.Text).ToString("yyyy-MM-dd") + "','Pending','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','0','0','0','0','0','0','0','0','0',1,'" + LVFO.Items[i].SubItems[2].Text + "',0," + syncid + ")", con);
                //        cmd1.ExecuteNonQuery();

                //    }
                //}
                for (int i = 0; i < LVFO.Items.Count; i++)
                {
                    dtpid = conn.getdataset("Select Productid from productmaster where product_name='" + LVFO.Items[i].SubItems[0].Text.Replace(",", "") + "'");
                    //if(dt
                    dt = conn.getdataset("Select IdToSync from purchaseorderproductmaster where vchno ='" + txtVchNo.Text + "'");
                    Guid guid;
                    guid = Guid.NewGuid();
                    string qry1 = "INSERT INTO [dbo].[PurchaseOrderProductMaster]([VchNo],[OrderNo],[OrderRunDate],[OrderStatus],[ProductName],[Qty],[Free],[Rate],[Per],[BasicAmount],[DiscountPer],[Discount],[Vat],[AddVat],[Total],[isactive],[Description],[isscyn],[IdToSync],[ProductId],[CompanyId],[syncDatetime])VALUES('" + txtVchNo.Text + "','" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtBillDate.Text).ToString("dd-MMM-yyyy") + "','Pending','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','0','0','0','0','0','0','0','0','0',1,'" + LVFO.Items[i].SubItems[2].Text + "',0,'" + guid + "','" + dtpid.Rows[0][0].ToString() + "','" + Master.companyId + "','" + DateTime.Now + "')";
                    // string qry = "Update [dbo].[PurchaseOrderProductMaster] SET [Vchno]='" + txtVchNo.Text + "',[OrderNo]='" + TxtBillNo.Text + "', [OrderRunDate]='" + DateTime.Now + "',[OrderStatus]='Pending',[ProductName]='" + LVFO.Items[i].SubItems[0].Text + "',[Qty]='" + LVFO.Items[i].SubItems[1].Text + "',[Description]='" + LVFO.Items[i].SubItems[2].Text + "' where [VchNo]='" + txtVchNo.Text + "' and [IdToSync]='" + dtits.Rows[i]["IdToSync"].ToString() + "' ";
                    SqlCommand cmd1 = new SqlCommand(qry1, con);
                    //SqlCommand cmd1 = new SqlCommand("INSERT INTO [dbo].[PurchaseOrderProductMaster]([VchNo],[OrderNo],[OrderRunDate],[OrderStatus],[ProductName],[Qty],[Free],[Rate],[Per],[BasicAmount],[DiscountPer],[Discount],[Vat],[AddVat],[Total],[isactive],[Description],[isscyn],[isscynID])VALUES('" + txtVchNo.Text + "','" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtBillDate.Text).ToString("yyyy-MM-dd") + "','Pending','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','0','0','0','0','0','0','0','0','0',1,'" + LVFO.Items[i].SubItems[2].Text + "',0," + syncid + ")", con);
                    cmd1.ExecuteNonQuery();
                    LogGenerator.Info(qry1);
                    conn.execute("Insert into sync values('" + qry1.Replace("'", "\"") + "')");

                }

                //Guid guid2;
                //guid2 = Guid.NewGuid();
                SqlCommand cmd = new SqlCommand("UPDATE [dbo].[PurchaseOrderMaster]SET [VChNo]='" + txtVchNo.Text + "',[OrderNo] = '" + TxtBillNo.Text + "',[OrderDate] = '" + Convert.ToDateTime(TxtBillDate.Text).ToString("yyyy-MM-dd") + "',[Terms] = '" + cmbTerms.Text + "',[ClientID] = '" + cmbCustName.SelectedValue + "',[PurchaseType] = '" + cmbPurchaseType.SelectedValue + "',[Count] = " + lbltotcount.Text + ",[TotalQty] = " + lbltotpqty.Text + ",[isactive]=1,[isscyn]=0,syncDatetime='" + DateTime.Now + "' where VchNo='" + txtVchNo.Text + "' and CompanyId=" + Master.companyId + "", con);
                cmd.ExecuteNonQuery();
                string qry2 = "UPDATE [dbo].[PurchaseOrderMaster]SET [VChNo]='" + txtVchNo.Text + "',[OrderNo] = '" + TxtBillNo.Text + "',[OrderDate] = '" + Convert.ToDateTime(TxtBillDate.Text).ToString("yyyy-MM-dd") + "',[Terms] = '" + cmbTerms.Text + "',[ClientID] = '" + cmbCustName.SelectedValue + "',[PurchaseType] = '" + cmbPurchaseType.SelectedValue + "',[Count] = " + lbltotcount.Text + ",[TotalQty] = " + lbltotpqty.Text + ",[isactive]=1,[isscyn]=0,syncDatetime='" + DateTime.Now + "' where VchNo='" + txtVchNo.Text + "' and CompanyId=" + Master.companyId + "";
                conn.execute("Insert into sync values('" + qry2.Replace("'", "\"") + "')");
                LogGenerator.Info(qry2);
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

                con.Open();
                for (int i = 0; i < LVFO.Items.Count; i++)
                {
                    //getsyncid();
                    //con.Open();
                    dtpid = conn.getdataset("Select Productid from productmaster where product_name='" + LVFO.Items[i].SubItems[0].Text.Replace(",", "") + "'");
                    Guid guid1;
                    guid1 = Guid.NewGuid();
                    string qry1 = "INSERT INTO [dbo].[PurchaseOrderProductMaster]([VchNo],[OrderNo],[OrderRunDate],[OrderStatus],[ProductName],[Qty],[Free],[Rate],[Per],[BasicAmount],[DiscountPer],[Discount],[Vat],[AddVat],[Total],[isactive],[Description],[isscyn],[IdToSync],[ProductId],[CompanyId],[syncDatetime])VALUES('" + txtVchNo.Text + "','" + TxtBillNo.Text + "','" + DateTime.Now + "','Pending','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','0','0','0','0','0','0','0','0','0',1,'" + LVFO.Items[i].SubItems[2].Text + "',0,'" + guid1 + "','" + dtpid.Rows[0][0].ToString() + "','" + Master.companyId +"','"+DateTime.Now+ "')";
                    SqlCommand cmd1 = new SqlCommand(qry1, con);
                    conn.execute("Insert into sync values('" + qry1.Replace("'", "\"") + "')");
                    LogGenerator.Info(qry1);
                    //SqlCommand cmd1 = new SqlCommand("INSERT INTO [dbo].[PurchaseOrderProductMaster]([VchNo],[OrderNo],[OrderRunDate],[OrderStatus],[ProductName],[Qty],[Free],[Rate],[Per],[BasicAmount],[DiscountPer],[Discount],[Vat],[AddVat],[Total],[isactive],[Description],[isscyn],[isscynID])VALUES('" + txtVchNo.Text + "','" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtBillDate.Text).ToString("yyyy-MM-dd") + "','Pending','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','0','0','0','0','0','0','0','0','0',1,'" + LVFO.Items[i].SubItems[2].Text + "',0,"+syncid+")", con);
                    //// SqlCommand cmd1 = new SqlCommand("INSERT INTO [dbo].[PurchaseOrderProductMaster]([VchNo],[OrderNo],[OrderRunDate],[OrderStatus],[ProductName],[Qty],[Description],[isactive])VALUES('" + txtVchNo.Text + "','" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtBillDate.Text).ToString("dd/MMM/yyyy") + "','Pending','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','"+txtDesc.Text+"',1)", con);
                    cmd1.ExecuteNonQuery();

                }
                Guid guid3;
                guid3 = Guid.NewGuid();
                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[PurchaseOrderMaster]([VchNo],[OrderNo],[OrderDate],[OrderStatus],[Terms],[ClientID],[PurchaseType],[Count],[TotalQty],[TotalBasic],[TotalTax],[TotalAddTax],[TotalDiscount],[TotalNet],[isactive],[DispatchDetails],[Remarks],[CompanyId],[isGenerated],[GeneDate],[isInvoice],[InvoiceDate],[isscyn],[IdToSync],[syncDatetime])VALUES('" + txtVchNo.Text + "','" + TxtBillNo.Text + "','" + DateTime.Now + "','Pending','" + cmbTerms.Text + "','" + cmbCustName.SelectedValue + "','" + cmbPurchaseType.SelectedValue + "'," + lbltotcount.Text + "," + lbltotpqty.Text + "," + lblbasictot.Text + "," + lbltaxtot.Text + "," + txtaddtax.Text + "," + lbltotaldiscount.Text + "," + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + ",1,'" + txtdispatch.Text + "','" + txtremarks.Text + "'," + Master.companyId + ",0,@param1,0,@param2,0,'" + guid3 +"','"+DateTime.Now+ "')", con);
                // SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[PurchaseOrderMaster]([VchNo],[OrderNo],[OrderDate],[OrderStatus],[Terms],[ClientID],[PurchaseType],[Count],[TotalQty],[isactive],[CompanyId],[isGenerated],[GeneDate],[isInvoice],[InvoiceDate])VALUES('" + txtVchNo.Text + "','" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtBillDate.Text).ToString("dd/MMM/yyyy").TrimEnd() + "','Pending','" + cmbTerms.Text + "','" + cmbCustName.SelectedValue + "','" + cmbPurchaseType.SelectedValue + "'," + lbltotcount.Text + "," + lbltotpqty.Text + ",1," + Master.companyId + ",0,@param1,0,@param2)", con);
                cmd.Parameters.Add("@param1", DateTime.Now);
                cmd.Parameters.Add("@param2", DateTime.Now);
                cmd.ExecuteNonQuery();
                string qry2 = "INSERT INTO [dbo].[PurchaseOrderMaster]([VchNo],[OrderNo],[OrderDate],[OrderStatus],[Terms],[ClientID],[PurchaseType],[Count],[TotalQty],[TotalBasic],[TotalTax],[TotalAddTax],[TotalDiscount],[TotalNet],[isactive],[DispatchDetails],[Remarks],[CompanyId],[isGenerated],[GeneDate],[isInvoice],[InvoiceDate],[isscyn],[IdToSync],[syncDatetime])VALUES('" + txtVchNo.Text + "','" + TxtBillNo.Text + "','" + DateTime.Now + "','Pending','" + cmbTerms.Text + "','" + cmbCustName.SelectedValue + "','" + cmbPurchaseType.SelectedValue + "'," + lbltotcount.Text + "," + lbltotpqty.Text + "," + lblbasictot.Text + "," + lbltaxtot.Text + "," + txtaddtax.Text + "," + lbltotaldiscount.Text + "," + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + ",1,'" + txtdispatch.Text + "','" + txtremarks.Text + "'," + Master.companyId + ",0,'" + DateTime.Now + "',0,'" + DateTime.Now + "',0,'" + guid3 + "','"+DateTime.Now+"')";
                conn.execute("Insert into sync values('" + qry2.Replace("'", "\"") + "')");
                LogGenerator.Info(qry2);
                clearitem();
                clearall();
                LVFO.Items.Clear();
                this.Hide();

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
                    li.SubItems.Add(txtDesc.Text);

                    //li.SubItems.Add(txtFree.Text);
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
                    SqlCommand cmd5 = new SqlCommand("select p.Product_Name, p.Unit, b.* from productmaster p inner join ProductPriceMaster b on p.ProductID=b.ProductID where p.product_name='" + txtItemName.Text + "'", con);

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
                        // MessageBox.Show("Not any Tax Available For This Sale Type");
                        txtVat.Text = "0";
                        txtAddVat.Text = "0";
                        txtQty.Focus();
                        //itemcalculation(txtQty.Text);
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
                double vat = ((Convert.ToDouble(txtBasicAmount.Text) - Convert.ToDouble(txtDiscount.Text)) * Convert.ToDouble(txtVat.Text)) / 100;
                double addvat = ((Convert.ToDouble(txtBasicAmount.Text) - Convert.ToDouble(txtDiscount.Text)) * Convert.ToDouble(txtAddVat.Text)) / 100;
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

            }
            lbltotcount.Text = count.ToString();
            lbltotpqty.Text = pqty.ToString();
            lblbasictot.Text = basic.ToString();
            lbltotaldiscount.Text = discount.ToString();
            TxtBillTotal.Text = Math.Round(total, 2).ToString("N2");
            lbltaxtot.Text = Math.Round(vat, 2).ToString("N2");
            txtaddtax.Text = Math.Round(addvat, 2).ToString("N2");
        }

        private void LVFO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            listback();
        }
        string uniqkey;
        private void listback()
        {
            if (LVFO.SelectedItems.Count > 0)
            {
                txtItemName.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[0].Text;

                txtQty.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[1].Text;
                txtDesc.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[2].Text;

                Double total = 0;

                TxtBillTotal.Text = total.ToString();
                LVFO.Items[LVFO.FocusedItem.Index].Remove();
                uniqkey = temptable.Rows[LVFO.FocusedItem.Index]["IdToSync"].ToString();
                DataRow dr = temptable.Rows[LVFO.FocusedItem.Index];
                dr.Delete();
                temptable.AcceptChanges();
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
        DataTable temptable = new DataTable();

        internal void updatemode(string str, string p, int p_2)
        {
            getcon();
            loadpage();
            cnt = 1;
            SqlCommand cmd = new SqlCommand("select * from PurchaseOrderMaster where OrderNo='" + p + "' and isactive=1 and CompanyId=" + Master.companyId + "", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            SqlCommand cmd1 = new SqlCommand("select * from PurchaseOrderProductMaster where OrderNo='" + p + "' and isactive=1", con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            temptable.Columns.AddRange(new DataColumn[4] { new DataColumn("ProductName"), new DataColumn("Qty"), new DataColumn("Description"), new DataColumn("IdToSync") });

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                temptable.Rows.Add(dt1.Rows[i]["Productname"].ToString(), dt1.Rows[i]["qty"].ToString(), dt1.Rows[i]["Description"].ToString(), dt1.Rows[i]["IdToSync"].ToString());
            }
            // temptable = dt1;
            if (dt.Rows.Count > 0)
            {
                txtVchNo.Text = dt.Rows[0][0].ToString();
                TxtBillNo.Text = dt.Rows[0][1].ToString();
                TxtBillDate.Text = Convert.ToDateTime(dt.Rows[0][2].ToString()).ToString("dd/MMM/yyyy");
                cmbTerms.Text = dt.Rows[0][3].ToString();
                cmd = new SqlCommand("select AccountName from ClientMaster where isactive=1 and ClientId='" + dt.Rows[0][4].ToString() + "'", con);

                con.Open();
                string clientname = cmd.ExecuteScalar().ToString();
                cmbCustName.Text = clientname;


                cmd = new SqlCommand("select purchasetypename from purchasetypemaster where purchasetypeid='" + dt.Rows[0][5].ToString() + "'", con);
                string saletypename = cmd.ExecuteScalar().ToString();
                cmbPurchaseType.Text = saletypename;
            }

            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    ListViewItem li;
                    li = LVFO.Items.Add(dt1.Rows[i][4].ToString());

                    li.SubItems.Add(dt1.Rows[i][5].ToString());
                    li.SubItems.Add(dt1.Rows[i]["Description"].ToString());

                }
            }
            //if (temptable.Rows.Count > 0)
            //{
            //    for (int i = 0; i < temptable.Rows.Count; i++)
            //    {
            //        ListViewItem li;
            //        li = LVFO.Items.Add(temptable.Rows[i][4].ToString());

            //        li.SubItems.Add(temptable.Rows[i][5].ToString());
            //        li.SubItems.Add(temptable.Rows[i]["Description"].ToString());

            //    }
            //}
            totalcalculation();
            clearitem();
            txtItemName.Focus();

            btnPayment.Text = "Update";


            con.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clearall();
            clearitem();
            LVFO.Items.Clear();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Delete Bill?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                getcon();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                SqlCommand cmd = new SqlCommand("update PurchaseOrderMaster set isactive=0  where VchNo='" + txtVchNo.Text + "' and CompanyId=" + Master.companyId + "", con);
                cmd.ExecuteNonQuery();
                string qry = "update PurchaseOrderMaster set isactive=0  where VchNo='" + txtVchNo.Text + "' and CompanyId=" + Master.companyId + "";
                conn.execute("Insert into sync values('" + qry.Replace("'", "\"") + "')");
                LogGenerator.Info(qry);
                SqlCommand cmd2 = new SqlCommand("update PurchaseOrderProductMaster set isactive=0 where VchNo='" + txtVchNo.Text + "'and CompanyId=" + Master.companyId + "", con);
                cmd2.ExecuteNonQuery();
                qry = "update PurchaseOrderProductMaster set isactive=0 where VchNo='" + txtVchNo.Text + "'and CompanyId=" + Master.companyId + "";
                conn.execute("Insert into sync values('" + qry.Replace("'", "\"") + "')");
                LogGenerator.Info(qry);
                clearall();
                clearitem();
                LVFO.Clear();
                this.Close();
                DateWisePurchaseOrderReport frm = new DateWisePurchaseOrderReport();
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
                txtDesc.Focus();
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

        private void LVFO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                listback();
            }
            if (e.KeyCode == Keys.Delete)
            {
                if (LVFO.SelectedItems.Count > 0)
                {


                    Double total = 0;


                    LVFO.Items[LVFO.FocusedItem.Index].Remove();
                    //     uniqkey = temptable.Rows[LVFO.FocusedItem.Index]["IdToSync"].ToString();
                    DataRow dr = temptable.Rows[LVFO.FocusedItem.Index];
                    dr.Delete();
                    temptable.AcceptChanges();
                    totalcalculation();
                }
            }
        }
        public void temp()
        {
            temptable.Columns.AddRange(new DataColumn[4] { new DataColumn("ProductName"), new DataColumn("Qty"), new DataColumn("Description"), new DataColumn("IdToSync") });
            //  temptable.Rows.Add(temptable.Rows[0]["OrderProdID"].ToString());
        }
        private void txtDesc_KeyDown(object sender, KeyEventArgs e)
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
                    li.SubItems.Add(txtDesc.Text);
                    if (btnPayment.Text == "Update")
                    {
                        if (uniqkey == null)
                        {
                            Guid guid1;
                            guid1 = Guid.NewGuid();
                            temptable.Rows.Add(txtItemName.Text, Math.Round(Convert.ToDouble(txtQty.Text), 2).ToString(), txtDesc.Text, guid1);
                        }
                        else
                        {
                            temptable.Rows.Add(txtItemName.Text, Math.Round(Convert.ToDouble(txtQty.Text), 2).ToString(), txtDesc.Text, uniqkey);
                            uniqkey = "";

                        }
                    }

                    totalcalculation();
                    clearitem();
                    txtItemName.Focus();
                }
            }
        }

    }
}
