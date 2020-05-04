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
using LoggingFramework;
using System.Web.Script.Serialization;

namespace RamdevSales
{
    public partial class SaleOfOrder : Form
    {
        OleDbSettings ods = new OleDbSettings();
        public DataSet ds;
        public DataTable dt, dtpo;
        public SqlConnection con;
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        //ServerConnection conn = new ServerConnection();
        Connection lcon = new Connection();
        private DateWiseSaleOrderReport DateWiseSaleOrderReport;
        private SaleOrder saleOrder;
        public static string compid;
        public SaleOfOrder()
        {
            InitializeComponent();
        }

        public SaleOfOrder(SaleOrder saleOrder)
        {
            
            InitializeComponent();
            this.saleOrder = saleOrder;
            
        }

        public SaleOfOrder(DateWiseSaleOrderReport DateWiseSaleOrderReport)
        {
            InitializeComponent();
            this.DateWiseSaleOrderReport = DateWiseSaleOrderReport;
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

        int cnt = 0;
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

        private void SaleOfOrder_Load(object sender, EventArgs e)
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

        public void bindPurchaseType()
        {
            getcon();
            SqlCommand cmd = new SqlCommand("select saletypeid,saletypename from SaletypeMaster", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            //JavaScriptSerializer js = new JavaScriptSerializer();
         //   var serializedData = js.Serialize(dt.DataSet);
          //  LogGenerator.Info(serializedData);
            cmbPurchaseType.ValueMember = "saletypeid";
            cmbPurchaseType.DisplayMember = "saletypename";
            cmbPurchaseType.DataSource = dt;
            cmbPurchaseType.SelectedIndex = -1;

            autobind(dt, cmbPurchaseType);
        }

        public void bindCustomer()
        {
            getcon();
            //SqlCommand cmd1 = new SqlCommand("select CompanyId,SubName from Company WHERE isactive=1", con);
            //SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            //DataTable dt1 = new DataTable();
            //sda1.Fill(dt1);
            //cmbCustName.ValueMember = "CompanyId";
            //cmbCustName.DisplayMember = "SubName";
            //cmbCustName.DataSource = dt1;
            //cmbCustName.SelectedIndex = -1;

            SqlCommand cmd1 = new SqlCommand("select ClientID,AccountName from ClientMaster WHERE groupname like '%CUSTOMERS%' order by AccountName", con);
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

        private void loadpage()
        {
            if (cnt == 0)
            {
                getcon();
                con.Open();
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(0, 0);
                TxtBillDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                this.ActiveControl = txtVchNo;

                //DataTable dt3 = new DataTable();

                //dt3 = conn.getdataset("select a, u, d, v, p, mId, uId, cId from UserRights where mId=4 and uId='" + UserLogin.id + "' and cId= " + Master.companyId + " and isActive=1");

                //if (dt3.Rows.Count > 0)
                //{

                //    if (Convert.ToBoolean(dt3.Rows[0][2]) == false)
                //    {
                //        btndelete.Visible = false;
                //    }
                //}

                LVFO.Columns.Add("Items", 300, HorizontalAlignment.Left);
                LVFO.Columns.Add("Qty", 60, HorizontalAlignment.Right);
                LVFO.Columns.Add("Description of Items", 360, HorizontalAlignment.Right);
                getsr();
                bindPurchaseType();
                bindCustomer();
                autoReaderBind();

                con.Close();
                cnt = 1;
            }
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

        private void btnPayment_Click(object sender, EventArgs e)
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
               // dtpo = conn.getdataset("Select isGenerated,GeneDate from PurchaseOrderMaster order by vchno desc ");
                // DataTable dtup = new DataTable();
                // dtup = conn.getdataset("Select * from PurchaseOrderMaster where OrderDate='" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' and isactive=1");
                DialogResult dr = MessageBox.Show("Do you want to Generate Sale Order?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                        
                        insertorupdate();
                    }

                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
                LogGenerator.Error(ex.Message, ex);
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
                //SqlCommand cmd2 = new SqlCommand("Update PurchaseOrderProductMaster set isactive='0' where VchNo='" + txtVchNo.Text + "' and companyid='" + compid + "'", con);
                //cmd2.ExecuteNonQuery();

                string qry="Update PurchaseOrderProductMaster set isactive='0',syncDatetime='"+DateTime.Now+"' where VchNo='" + txtVchNo.Text + "' and companyid='" + compid + "'";
                SqlCommand cmd2 = new SqlCommand(qry, con);
                cmd2.ExecuteNonQuery();
                LogGenerator.Info(qry);
                lcon.execute("Insert into sync values('" + qry.Replace("'", "\"") + "')");
               // lcon.execute("insert into sync values('Update PurchaseOrderProductMaster set isactive='0' where VchNo='" + txtVchNo.Text + "' and companyid='" + compid + "'')");
                //if (VchNo != null)
                //{
                //    foreach (var item in VchNo)
                //    {
                //        conn.execute("Update PurchaseOrderMaster set OrderStatus='Success' where isactive=1 and vchno=" + item + " and companyId=" + Master.companyId + "");
                //    }
                //}
                for (int i = 0; i < LVFO.Items.Count; i++)
                {
                    dtpid = lcon.getdataset("Select Productid from productmaster where product_name='" + LVFO.Items[i].SubItems[0].Text.Replace(",", "") + "'");

                    Guid guid;
                    guid = Guid.NewGuid(); 
                    //SqlCommand cmd1 = new SqlCommand("INSERT INTO [dbo].[PurchaseOrderProductMaster]([VchNo],[OrderNo],[OrderRunDate],[ProductName],[Qty],[Free],[Rate],[Per],[BasicAmount],[DiscountPer],[Discount],[Vat],[AddVat],[Total],[isactive],[OrderStatus],[Description],[Productid],[isscyn],[IdToSync])VALUES('" + txtVchNo.Text + "','" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtBillDate.Text).ToString("dd/MMM/yyyy") + "','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','0','0','0','0','0','0','0','0','0',1,'SaleOrder','" + LVFO.Items[i].SubItems[2].Text + "','" + dtpid.Rows[0][0].ToString() + "',0,'"+guid+"')", con);
                    //cmd1.ExecuteNonQuery();
                    string qry1 = "INSERT INTO [dbo].[PurchaseOrderProductMaster]([VchNo],[OrderNo],[OrderRunDate],[ProductName],[Qty],[Free],[Rate],[Per],[BasicAmount],[DiscountPer],[Discount],[Vat],[AddVat],[Total],[isactive],[OrderStatus],[Description],[Productid],[isscyn],[IdToSync],[companyid],[syncDatetime])VALUES('" + txtVchNo.Text + "','" + TxtBillNo.Text + "','" + DateTime.Now + "','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','0','0','0','0','0','0','0','0','0',1,'SaleOrder','" + LVFO.Items[i].SubItems[2].Text + "','" + dtpid.Rows[0][0].ToString() + "',0,'" + guid + "','" + cmbCustName.SelectedValue + "','" + DateTime.Now + "')";
                    LogGenerator.Info(qry1);
                    SqlCommand cmd1 = new SqlCommand(qry1, con);
                    cmd1.ExecuteNonQuery();
                    lcon.execute("Insert into sync values('" + qry1.Replace("'", "\"") + "')");
                }
                //Guid guid1;
                //guid1 = Guid.NewGuid(); 
                //SqlCommand cmd = new SqlCommand("UPDATE [dbo].[PurchaseOrderMaster]SET [VChNo]='" + txtVchNo.Text + "',[OrderNo] = '" + TxtBillNo.Text + "',[OrderDate] = '" + Convert.ToDateTime(TxtBillDate.Text).ToString("dd/MMM/yyyy") + "',[Terms] = '" + cmbTerms.Text + "',[ClientID] = '" + cmbCustName.SelectedValue + "',[PurchaseType] = '" + cmbPurchaseType.SelectedValue + "',[Count] = " + lbltotcount.Text + ",[TotalQty] = " + lbltotpqty.Text + ",[isactive]=1, [OrderStatus]='Pending',[isscyn]=0,[IdToSync]='"+guid1+"' where VchNo='" + txtVchNo.Text + "' and CompanyId=" + compid + "", con);
                //cmd.ExecuteNonQuery();
                string qry2;
                if (Master.companyType == "Factory")
                {
                    qry2 = "UPDATE [dbo].[PurchaseOrderMaster]SET [CompanyID]='"+cmbCustName.SelectedValue+"',[VChNo]='" + txtVchNo.Text + "',[OrderNo] = '" + TxtBillNo.Text + "',[OrderDate] = '" + DateTime.Now + "',[Terms] = '" + cmbTerms.Text + "',[ClientID] = '" + Master.companyId + "',[PurchaseType] = '" + cmbPurchaseType.SelectedValue + "',[Count] = " + lbltotcount.Text + ",[TotalQty] = " + lbltotpqty.Text + ",[isactive]=1, [OrderStatus]='Pending',[isscyn]=0,[syncDatetime]='" + DateTime.Now + "' where VchNo='" + txtVchNo.Text + "' and CompanyId=" + compid + "";
                }
                else
                {
                    qry2 = "UPDATE [dbo].[PurchaseOrderMaster]SET [VChNo]='" + txtVchNo.Text + "',[OrderNo] = '" + TxtBillNo.Text + "',[OrderDate] = '" + DateTime.Now + "',[Terms] = '" + cmbTerms.Text + "',[ClientID] = '" + cmbCustName.SelectedValue + "',[PurchaseType] = '" + cmbPurchaseType.SelectedValue + "',[Count] = " + lbltotcount.Text + ",[TotalQty] = " + lbltotpqty.Text + ",[isactive]=1, [OrderStatus]='Pending',[isscyn]=0,[syncDatetime]='" + DateTime.Now + "' where VchNo='" + txtVchNo.Text + "' and CompanyId=" + compid + "";
                }
                LogGenerator.Info(qry2);
                SqlCommand cmd3 = new SqlCommand(qry2, con);
                cmd3.ExecuteNonQuery();
                lcon.execute("Insert into sync values('" + qry2.Replace("'", "\"") + "')");
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

                //if (VchNo != null)
                //{
                //    foreach (var item in VchNo)
                //    {
                //        conn.execute("Update PurchaseOrderMaster set OrderStatus='Success' where isactive=1 and vchno=" + item + " and companyId=" + Master.companyId + "");
                //    }
                //}

                for (int i = 0; i < LVFO.Items.Count; i++)
                {
                    dtpid = lcon.getdataset("Select Productid from productmaster where product_name='" + LVFO.Items[i].SubItems[0].Text.Replace(",", "") + "'");
                    Guid guid2;
                    guid2 = Guid.NewGuid(); 
                    //SqlCommand cmd1 = new SqlCommand("INSERT INTO [dbo].[PurchaseOrderProductMaster]([VchNo],[OrderNo],[OrderRunDate],[ProductName],[Qty],[Free],[Rate],[Per],[BasicAmount],[DiscountPer],[Discount],[Vat],[AddVat],[Total],[isactive],[OrderStatus],[Description],[Productid],[isscyn],[IdToSync])VALUES('" + txtVchNo.Text + "','" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtBillDate.Text).ToString("dd/MMM/yyyy") + "','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','0','0','0','0','0','0','0','0','0',1,'SaleOrder','" + LVFO.Items[i].SubItems[2].Text + "','" + dtpid.Rows[0][0].ToString() + "',0,'"+guid2+"')", con);
                    //// SqlCommand cmd1 = new SqlCommand("INSERT INTO [dbo].[PurchaseOrderProductMaster]([VchNo],[OrderNo],[OrderRunDate],[OrderStatus],[ProductName],[Qty],[Description],[isactive])VALUES('" + txtVchNo.Text + "','" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtBillDate.Text).ToString("dd/MMM/yyyy") + "','Pending','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','"+txtDesc.Text+"',1)", con);
                    //cmd1.ExecuteNonQuery();
                    string qry = "INSERT INTO [dbo].[PurchaseOrderProductMaster]([VchNo],[OrderNo],[OrderRunDate],[ProductName],[Qty],[Free],[Rate],[Per],[BasicAmount],[DiscountPer],[Discount],[Vat],[AddVat],[Total],[isactive],[OrderStatus],[Description],[Productid],[isscyn],[IdToSync],[companyid],[syncDatetime])VALUES('" + txtVchNo.Text + "','" + TxtBillNo.Text + "','" + DateTime.Now + "','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','0','0','0','0','0','0','0','0','0',1,'SaleOrder','" + LVFO.Items[i].SubItems[2].Text + "','" + dtpid.Rows[0][0].ToString() + "',0,'" + guid2 + "','" + cmbCustName.SelectedValue + "','" + DateTime.Now + "')";
                    SqlCommand cmd = new SqlCommand(qry, con);
                    cmd.ExecuteNonQuery();
                    lcon.execute("Insert into sync values('" + qry.Replace("'", "\"") + "')");
                    LogGenerator.Info(qry);
                }
                Guid guid3;
                guid3 = Guid.NewGuid(); 
                //SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[PurchaseOrderMaster]([VchNo],[OrderNo],[OrderDate],[Terms],[ClientID],[PurchaseType],[Count],[TotalQty],[TotalBasic],[TotalTax],[TotalAddTax],[TotalDiscount],[TotalNet],[isactive],[DispatchDetails],[Remarks],[CompanyId])VALUES('" + txtVchNo.Text + "','" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtBillDate.Text).ToString("yyyy-MM-dd").TrimEnd() + "','" + cmbTerms.Text + "','" + cmbCustName.SelectedValue + "','" + cmbPurchaseType.SelectedValue + "'," + lbltotcount.Text + "," + lbltotpqty.Text + "," + lblbasictot.Text + "," + lbltaxtot.Text + "," + txtaddtax.Text + "," + lbltotaldiscount.Text + "," + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + ",1,'" + txtdispatch.Text + "','" + txtremarks.Text + "'," + Master.companyId + ")", con);
                //// SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[PurchaseOrderMaster]([VchNo],[OrderNo],[OrderDate],[OrderStatus],[Terms],[ClientID],[PurchaseType],[Count],[TotalQty],[isactive],[CompanyId],[isGenerated],[GeneDate],[isInvoice],[InvoiceDate])VALUES('" + txtVchNo.Text + "','" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtBillDate.Text).ToString("dd/MMM/yyyy").TrimEnd() + "','Pending','" + cmbTerms.Text + "','" + cmbCustName.SelectedValue + "','" + cmbPurchaseType.SelectedValue + "'," + lbltotcount.Text + "," + lbltotpqty.Text + ",1," + Master.companyId + ",0,@param1,0,@param2)", con);
                //cmd.ExecuteNonQuery();
                string qry1 = "INSERT INTO [dbo].[PurchaseOrderMaster]([VchNo],[OrderNo],[OrderDate],[Terms],[ClientID],[PurchaseType],[Count],[TotalQty],[TotalBasic],[TotalTax],[TotalAddTax],[TotalDiscount],[TotalNet],[isactive],[DispatchDetails],[Remarks],[CompanyId],[OrderStatus],[isscyn],[isGenerated],[isInvoice],[IdToSync],[syncDatetime])VALUES('" + txtVchNo.Text + "','" + TxtBillNo.Text + "','" + DateTime.Now + "','" + cmbTerms.Text + "','" + Master.companyId + "','" + cmbPurchaseType.SelectedValue + "'," + lbltotcount.Text + "," + lbltotpqty.Text + "," + lblbasictot.Text + "," + lbltaxtot.Text + "," + txtaddtax.Text + "," + lbltotaldiscount.Text + "," + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + ",1,'" + txtdispatch.Text + "','" + txtremarks.Text + "'," + cmbCustName.SelectedValue + ",'Pending',0,0,0,'" + guid3 + "','" + DateTime.Now + "')";
                LogGenerator.Info(qry1);
                
                SqlCommand cmd1 = new SqlCommand(qry1, con);
                cmd1.ExecuteNonQuery();
                lcon.execute("Insert into sync values('" + qry1.Replace("'", "\"") + "')");
                clearitem();
                clearall();
                LVFO.Items.Clear();
                MessageBox.Show("Data Insert Successfully.");
                this.Hide();

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
               
                    //  txtpono.Focus();
                if (cmbCustName.Text != "")
                    {

                        getsr();
                        DataTable dtcn = new DataTable();

                        dtcn = lcon.getdataset("select * from PurchaseOrderMaster where isactive=1 and isGenerated=0 and CompanyID='" + cmbCustName.SelectedValue + "'");
                        if (dtcn != null && dtcn.Rows.Count > 0)
                        {
                            MessageBox.Show("Your Order had been Already Generated");
                            btnPayment.Visible = false;
                        }
                        else
                        {
                            //txtpono.Focus();
                            // cmbcustname.Focus();
                            cmbPurchaseType.Focus();
                            btnPayment.Visible = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Select Customer");
                        cmbCustName.Focus();
                    }
                
//                if (cmbCustName.Text != "")
//                {
//                    if (cmbCustName.SelectedIndex >= 0)
//                    {
//                        DataTable dtcn = new DataTable();
////DataTable dtcn1 = new DataTable();

//                        dtcn = conn.getdataset("Select distinct CompanyId from PurchaseOrderMaster where CompanyId=" + cmbCustName.SelectedValue + " and isactive=1 and OrderStatus='Pending'");
                      
//                        //dtcn = conn.getdataset("Select ClientId,accountName from ClientMaster where ClientId=" + cmbCustName.SelectedValue + "");
//                        if (dtcn != null && dtcn.Rows.Count > 0)
//                        {
                           
//                                SaleOrder frm = new SaleOrder(dtcn.Rows[0]["CompanyId"].ToString(), this);
                               
//                                frm.ShowDialog();
//                                string userEnteredText = frm.EnteredText;
//                                frm.Dispose();
                           
//                        }

//                    }
                  
                }
                //else
                //{
                //    MessageBox.Show("Please Select Customer");
                //    cmbCustName.Focus();
                //}
           // }
            //if (e.KeyCode == Keys.F3)
            //{
                //Accountentry client = new Accountentry(this);

                //client.Passed(1);
                //client.Show();
            //}
            

        }

        public static string[] VchNo;
        public void getVchNos(string[] getstring)
        {
            VchNo = getstring;
            updatemode(VchNo);
        }
        //public string getVchNo
        //{
        //    private string vn= VchNo.ToString();
        //    get { return VchNo; }
        //    set { txtVchNo.Text = value; }
        //}

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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        internal void updatemode(string[] vn)
        {
            
            DataTable data = new DataTable();
            int vchno =0;
            loadpage();
            cnt = 1;
            foreach (var item in vn)
            {
                data = lcon.getdataset("Select pop.ProductName,pop.Qty,pop.Description from PurchaseOrderProductMaster pop inner join PurchaseOrderMaster po on po.VchNo=pop.VchNo where pop.VchNo = " + item + " and pop.isactive=1 and po.CompanyId="+cmbCustName.SelectedValue+"");
                ListViewItem li;
                if (data != null && data.Rows.Count > 0)
                {
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        li = LVFO.Items.Add(data.Rows[i][0].ToString());

                        li.SubItems.Add(data.Rows[i][1].ToString());
                        li.SubItems.Add(data.Rows[i][2].ToString());
                    }
                }
                vchno=Convert.ToInt16(item);
            }

            DataTable pom = new DataTable();
            pom = lcon.getdataset("Select * from purchaseOrdermaster where Vchno=" + vchno + "");
            if (pom.Rows.Count > 0)
            {
                
                cmbTerms.SelectedItem = pom.Rows[0]["Terms"].ToString();

                cmbPurchaseType.SelectedValue = pom.Rows[0]["PurchaseType"].ToString();
                
                cmbCustName.SelectedValue = pom.Rows[0]["CompanyId"].ToString();
            }
            totalcalculation();
            clearitem();
            txtItemName.Focus();
        }
        DataTable temptable = new DataTable();
        string uniqkey;
        internal void updatemode(string str, string p, int p_2)
        {
            getcon();
            loadpage();
            cnt = 1;
            compid =p;
            SqlCommand cmd = new SqlCommand("select * from PurchaseOrderMaster where OrderNo='" + str + "' and isactive=1 and CompanyId=" + p + "", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            SqlCommand cmd1 = new SqlCommand("select * from PurchaseOrderProductMaster where OrderNo='" + str + "'and CompanyID="+p+" and isactive=1", con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            DataTable dtpidu = new DataTable();
            temptable.Columns.AddRange(new DataColumn[4] { new DataColumn("ProductName"), new DataColumn("Qty"), new DataColumn("Description"), new DataColumn("IdToSync") });

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                temptable.Rows.Add(dt1.Rows[i]["Productname"].ToString(), dt1.Rows[i]["qty"].ToString(), dt1.Rows[i]["Description"].ToString(), dt1.Rows[i]["IdToSync"].ToString());
            }
            if (dt.Rows.Count > 0)
            {
                txtVchNo.Text = dt.Rows[0][0].ToString();
                TxtBillNo.Text = dt.Rows[0][1].ToString();
                TxtBillDate.Text = Convert.ToDateTime(dt.Rows[0][2].ToString()).ToString("dd/MMM/yyyy");
                cmbTerms.Text = dt.Rows[0][3].ToString();
                con.Open();
                if (Master.companyType == "Factory")
                {
                    cmd1 = new SqlCommand("Select printname from Clientmaster where ClientId=" + p + "", con);
                    cmbCustName.Text = cmd1.ExecuteScalar().ToString();
                }

                
                
                cmd = new SqlCommand("select saletypename from SaletypeMaster where saletypeid='" + dt.Rows[0][5].ToString() + "'", con);
                string saletypename = cmd.ExecuteScalar().ToString();
                cmbPurchaseType.Text = saletypename;
            }

            if (dt1.Rows.Count > 0)
            {
                for (
                    int i = 0; i < dt1.Rows.Count; i++)
                {
                    ListViewItem li;
                    dtpidu = lcon.getdataset("Select product_name from productmaster where productid=" + dt1.Rows[i]["Productid"].ToString() + "");

                    li = LVFO.Items.Add(dtpidu.Rows[0][0].ToString());

                    li.SubItems.Add(dt1.Rows[i][5].ToString());
                    li.SubItems.Add(dt1.Rows[i]["Description"].ToString());
                }
            }
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
                //SqlCommand cmd = new SqlCommand("update PurchaseOrderMaster set isactive=0  where VchNo='" + txtVchNo.Text + "' and CompanyId=" + compid + "", con);
                //cmd.ExecuteNonQuery();
                string qry="update PurchaseOrderMaster set isactive=0  where VchNo='" + txtVchNo.Text + "' and CompanyId=" + compid + "";
                SqlCommand cmd = new SqlCommand(qry, con);
                LogGenerator.Info(qry);
                cmd.ExecuteNonQuery();
                lcon.execute("Insert into sync values('" + qry.Replace("'", "\"") + "')");
                SqlCommand cmd2 = new SqlCommand("update PurchaseOrderProductMaster set isactive=0 where VchNo='" + txtVchNo.Text + "'and CompanyId=" + compid , con);
                cmd2.ExecuteNonQuery();
                string qry1="update PurchaseOrderProductMaster set isactive=0 where VchNo='" + txtVchNo.Text + "' and CompanyId=" + compid+"";
                lcon.execute("Insert into sync values('" + qry1.Replace("'", "\"") + "')");
                LogGenerator.Info(qry1);
                clearall();
                clearitem();
                LVFO.Clear();
                this.Close();
                DateWisePurchaseReport frm = new DateWisePurchaseReport();
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

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void LVFO_KeyDown_1(object sender, KeyEventArgs e)
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

                    DataRow dr = temptable.Rows[LVFO.FocusedItem.Index];
                    LVFO.Items[LVFO.FocusedItem.Index].Remove();
                    //     uniqkey = temptable.Rows[LVFO.FocusedItem.Index]["IdToSync"].ToString();
                   
                    dr.Delete();
                    temptable.AcceptChanges();
                    totalcalculation();
                }
            }
        }

        private void LVFO_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            listback();
        }

     

      

      
    }
}
