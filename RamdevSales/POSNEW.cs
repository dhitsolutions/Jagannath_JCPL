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
using System.Security.Cryptography;
using System.IO;
using LoggingFramework;

namespace RamdevSales
{
    public partial class POSNEW : Form
    {
        Connection conn = new Connection();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
	   
        public POSNEW()
        {
            InitializeComponent();
            grpWeightingScale.Visible = false;
            // pnlMiscVat.Visible = false;
            pnlMiscVatCalci.Visible = false;
            pnlpayment.Visible = false;
            editbox.Parent = lvItem;
            editbox.Hide();
            editbox.LostFocus += new EventHandler(editbox_LostFocus);
            editbox.KeyDown += new KeyEventHandler(editbox_KeyDown);
            lvItem.MouseDoubleClick += new MouseEventHandler(lvItem_MouseDoubleClick);
            lvItem.FullRowSelect = true;

        }

        public POSNEW(POSMultiProduct pOSMultiProduct, string batch)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.pOSMultiProduct = pOSMultiProduct;
            this.batch = batch;
            editbox.Parent = lvItem;
            editbox.Hide();
            editbox.LostFocus += new EventHandler(editbox_LostFocus);
            editbox.KeyDown += new KeyEventHandler(editbox_KeyDown);
            lvItem.MouseDoubleClick += new MouseEventHandler(lvItem_MouseDoubleClick);
            lvItem.FullRowSelect = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LogGenerator.Info("POS User Logout UserID=" + Master.userid);
            if (options.Rows[0]["userlog"].ToString() == "True")
            {
                conn.execute("INSERT INTO [dbo].[USerLog]([userid],[loginpalce],[Status],[isactive],[datetime])VALUES('" + Master.userid + "','" + "POS" + "','" + "Logout" + "','" + "1" + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "')");
            }
            this.Close();
            clearall();
        }

        private void btnScaleItems_Click(object sender, EventArgs e)
        {
            grpWeightingScale.Visible = true;
            panel7.Visible = true; //3 Part Panel
            pnlCalci.Visible = false;
            lvMainCategory1.Visible = true;
            lvsubcategory1.Visible = true;
            lvitemlist1.Visible = true;
            pnlMiscVatCalci.Visible = false;

        }

        private void btnMiscVat_Click(object sender, EventArgs e)
        {
            lvMainCategory1.Visible = false;
            lvsubcategory1.Visible = false;
            lvitemlist1.Visible = false;
            // pnlMiscVat.Visible = true;
            pnlMiscVatCalci.Visible = true;
            pnlCalci.Visible = true;
            pnlMiscVatCalci.Visible = true;
            grpWeightingScale.Visible = false;

        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (lvItem.Items.Count > 0)
            {
                btncash.Enabled = false;
                pnlpayment.Visible = true;
            }
            else
            {
                MessageBox.Show("Enter Item");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            btncash.Enabled = true;
            pnlpayment.Visible = false;
        }

        private void btnMiscNonVat_Click(object sender, EventArgs e)
        {
            lvMainCategory1.Visible = false;
            lvsubcategory1.Visible = false;
            lvitemlist1.Visible = false;
            // pnlMiscVat.Visible = true;
            pnlMiscVatCalci.Visible = true;
            pnlCalci.Visible = true;
            pnlMiscVatCalci.Visible = true;
            grpWeightingScale.Visible = false;
        }

        private void btnHotKeys_Click(object sender, EventArgs e)
        {
            panel7.Visible = true;
            pnlpayment.Visible = false;
            grpWeightingScale.Visible = false;
            panel5.Visible = true;
            lvMainCategory1.Visible = true;
            lvsubcategory1.Visible = true;
            lvitemlist1.Visible = true;
            pnlCalci.Visible = true;
            pnlMiscVatCalci.Visible = false;

        }

        private void lvItem_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        List<Button> buttons = new List<Button>();
        private void bindbuttons(DataTable dt1)
        {
            int j = 0;
            //#region
            //foreach (Button button in buttons)
            //{
            //    Controls.Remove(button);
            //    button.Dispose();
            //}

            //#endregion
            lvMainCategory.Controls.Clear();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                if (dt1.Rows.Count > 5)
                {
                    Button newButton = new Button();
                    buttons.Add(newButton);
                    newButton.Name = "btn" + dt1.Rows[i][0].ToString();
                    newButton.Text = dt1.Rows[i][0].ToString();
                    newButton.BackColor = Color.FromArgb(0, 229, 229);
                    newButton.ForeColor = Color.FromArgb(0, 0, 0);
                    newButton.FlatStyle = FlatStyle.Flat;
                    newButton.Location = new Point((j * 10) + 5, 31);
                    j = j + 10;
                    newButton.Width = 80;
                    newButton.Height = 60;
                    newButton.Anchor = (AnchorStyles.Top | AnchorStyles.Left);

                    newButton.TabIndex = 1;
                    newButton.TabStop = true;
                    newButton.Click += new EventHandler(button_Click);
                    lvMainCategory.Controls.Add(newButton);
                }
                else
                {
                    Button newButton = new Button();
                    buttons.Add(newButton);
                    newButton.Name = "btn" + dt1.Rows[i][0].ToString();
                    newButton.Text = dt1.Rows[i][0].ToString();
                    newButton.BackColor = Color.FromArgb(0, 229, 229);
                    newButton.ForeColor = Color.FromArgb(0, 0, 0);
                    newButton.FlatStyle = FlatStyle.Flat;
                    newButton.Location = new Point((j * 10) + 5, 31);
                    j = j + 10;
                    newButton.Width = 140;
                    newButton.Height = 70;
                    newButton.Anchor = (AnchorStyles.Top | AnchorStyles.Left);

                    newButton.TabIndex = 1;
                    newButton.TabStop = true;
                    newButton.Click += new EventHandler(button_Click);
                    lvMainCategory.Controls.Add(newButton);
                }
            }

        }
        private void bindbuttons1(DataTable dt1)
        {
            int j = 0;
            //#region
            //foreach (Button button in buttons)
            //{
            //    Controls.Remove(button);
            //    button.Dispose();
            //}

            //#endregion
            lvsubcategory.Controls.Clear();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                if (dt1.Rows.Count > 5)
                {
                    Button newButton = new Button();
                    buttons.Add(newButton);
                    newButton.Name = "btn" + dt1.Rows[i][0].ToString();
                    newButton.Text = dt1.Rows[i][0].ToString();
                    newButton.BackColor = Color.FromArgb(0, 0, 0);
                    newButton.FlatStyle = FlatStyle.Flat;
                    newButton.ForeColor = Color.FromArgb(255, 255, 255);
                    newButton.Location = new Point((j * 10) + 5, 31);
                    j = j + 10;
                    newButton.Width = 80;
                    newButton.Height = 60;
                    newButton.Anchor = (AnchorStyles.Top | AnchorStyles.Left);

                    newButton.TabIndex = 1;
                    newButton.TabStop = true;
                    newButton.Click += new EventHandler(button1_Click);
                    lvsubcategory.Controls.Add(newButton);
                }
                else
                {
                    Button newButton = new Button();
                    buttons.Add(newButton);
                    newButton.Name = "btn" + dt1.Rows[i][0].ToString();
                    newButton.Text = dt1.Rows[i][0].ToString();
                    newButton.BackColor = Color.FromArgb(0, 0, 0);
                    newButton.FlatStyle = FlatStyle.Flat;
                    newButton.ForeColor = Color.FromArgb(255, 255, 255);
                    newButton.Location = new Point((j * 10) + 5, 31);
                    j = j + 10;
                    newButton.Width = 140;
                    newButton.Height = 70;
                    newButton.Anchor = (AnchorStyles.Top | AnchorStyles.Left);

                    newButton.TabIndex = 1;
                    newButton.TabStop = true;
                    newButton.Click += new EventHandler(button1_Click);
                    lvsubcategory.Controls.Add(newButton);
                }
            }

        }
        private void bindbuttons2(DataTable dt1)
        {
            int j = 0;
            //#region
            //foreach (Button button in buttons)
            //{
            //    Controls.Remove(button);
            //    button.Dispose();
            //}

            //#endregion
            lvitemlist.Controls.Clear();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                if (dt1.Rows.Count > 5)
                {
                    Button newButton = new Button();
                    buttons.Add(newButton);
                    newButton.Name = "btn" + dt1.Rows[i][0].ToString();
                    newButton.Text = dt1.Rows[i][0].ToString();
                    newButton.BackColor = Color.FromArgb(0, 0, 0);
                    newButton.ForeColor = Color.FromArgb(255, 255, 255);
                    newButton.FlatStyle = FlatStyle.Flat;
                    newButton.Location = new Point((j * 10) + 5, 31);
                    j = j + 10;
                    newButton.Width = 80;
                    newButton.Height = 60;
                    newButton.Anchor = (AnchorStyles.Top | AnchorStyles.Left);

                    newButton.TabIndex = 1;
                    newButton.TabStop = true;
                    newButton.Click += new EventHandler(button2_Click);
                    lvitemlist.Controls.Add(newButton);
                }
                else
                {
                    Button newButton = new Button();
                    buttons.Add(newButton);
                    newButton.Name = "btn" + dt1.Rows[i][0].ToString();
                    newButton.Text = dt1.Rows[i][0].ToString();
                    newButton.BackColor = Color.FromArgb(0, 0, 0);
                    newButton.ForeColor = Color.FromArgb(255, 255, 255);
                    newButton.FlatStyle = FlatStyle.Flat;
                    newButton.Location = new Point((j * 10) + 5, 31);
                    j = j + 10;
                    newButton.Width = 140;
                    newButton.Height = 70;
                    newButton.Anchor = (AnchorStyles.Top | AnchorStyles.Left);

                    newButton.TabIndex = 1;
                    newButton.TabStop = true;
                    newButton.Click += new EventHandler(button2_Click);
                    lvitemlist.Controls.Add(newButton);
                }
            }

        }
        protected void button_Click(object sender, EventArgs e)
        {
            try
            {
                Button button = sender as Button;
                string comid = conn.ExecuteScalar("Select CompanyID from CompanyMaster where companyname='" + button.Text + "'");
                DataTable dt = conn.getdataset("select distinct(GroupName) from ProductMaster where isactive=1 and CompanyID='" + comid + "' and isHotProduct='1'");
                bindbuttons1(dt);
            }
            catch
            {
            }
        }
        protected void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Button button = sender as Button;
                DataTable dt = conn.getdataset("select distinct(Product_Name) from ProductMaster where isactive=1 and GroupName='" + button.Text + "' and isHotProduct='1'");
                bindbuttons2(dt);
            }
            catch
            {
            }
        }
        ListViewItem li;
        Double qty = 0;
        string updateqty, price;
        Int32 rowid = -1;
        double finaldis;
        DataTable options, pricetab = new DataTable();
        public static string batchno, Itemname, updatebillno;
        string Dprice = "";
        double disper;
        double disamt;
        DataTable pos = new DataTable();
		string existserial="";
        string serialno="";					  
								
        public void insertdata()
        {
            try
            {
			 

										   
                if (lvItem.Items.Count > 0)
                {
                    bool exists = false;
                    foreach (ListViewItem item in lvItem.Items)
                    {
                        for (int b = 0; b < item.SubItems.Count; b++)
                        {
                            string iname = item.SubItems[0].Text;
                            string barcode = item.SubItems[6].Text;
                            string updatebatch = item.SubItems[11].Text;
                            Itemname = Itemname.ToUpper();
                            iname = iname.ToUpper();
                            if ((Itemname == iname || Itemname == barcode) && batchno == updatebatch)
                            {
								 existserial = item.SubItems[14].Text;									 
                                rowid = item.Index;
                                updateqty = item.SubItems[1].Text;
                                price = item.SubItems[2].Text;
											  
                                exists = true;
                            }
                        }
                    }
								
                    if (!exists)
                    {
                        DataTable productid = new DataTable();
                        productid = conn.getdataset("select DISTINCT p.taxslab,p.ProductID from ProductMaster p inner join ProductPriceMaster pp on p.Productid=pp.Productid where    (p.Product_Name='" + Itemname + "'or pp.barcode='" + Itemname + "') and pp.BatchNo='" + batchno + "' and p.isactive=1 and pp.isactive=1");
                        double disper;
                        double disamt;
                        pos = conn.getdataset("select p.cessper,p.cessamt,pp.Barcode,p.Product_Name,pp." + Dprice + " as Dprice,pp.BasicPrice,i.igst,i.additax,i.cgst,i.sgst from ProductPriceMaster pp inner join ProductMaster p on p.ProductID=pp.Productid  inner join TaxSlabMaster i on i.Taxslabname=p.taxslab where p.ProductID='" + productid.Rows[0]["ProductID"].ToString() + "' and  p.taxslab='" + productid.Rows[0]["taxslab"].ToString() + "'and i.saletypename='" + lblsaletype.Text + "' and pp.BatchNo='" + batchno + "' and pp.isactive=1 and p.isactive=1 and i.isactive=1");
                        //   dt = sql.getdataset("select p.*,b.* from productmaster p inner join ProductPriceMaster b on p.ProductID=b.ProductID where b.barcode='" + txtbarcode.Text + "' or p.product_name like '%" + txtbarcode.Text + "%'");
                        //  dt = sql.getdataset("Select p.Product_Name,p.Unit,pp.saleprice  as rate,pp.MRP as Amount from ProductMaster p inner join ProductPriceMaster pp on p.ProductID = pp.Productid where p.ProductID = pp.Productid and p.Product_Name='" + txtbarcode.Text + "'or pp.barcode='" + txtbarcode.Text + "'");
                        //DataTable discount = new DataTable();
                        //discount = conn.getdataset("select Discount,SpecialRate from PartyRates where ItemID='" + productid.Rows[0]["ProductID"].ToString() + "'");
                        //if (discount.Rows.Count > 0)
                        //{
                        //    disper = Convert.ToInt32(discount.Rows[0]["Discount"].ToString());
                        //    disamt = Convert.ToInt32(discount.Rows[0]["SpecialRate"].ToString());
                        //}
                        //else
                        //{
                        //    disper = 0;
                        //    disamt = 0;
                        //}
                        DataTable discount = new DataTable();
                        discount = conn.getdataset("select Discount,SpecialRate from PartyRates where ItemID='" + productid.Rows[0]["ProductID"].ToString() + "'");
                        if (discount.Rows.Count > 0)
                        {
                            disper = Convert.ToInt32(discount.Rows[0]["Discount"].ToString());
                            if (Convert.ToInt32(discount.Rows[0]["SpecialRate"].ToString()) > 0)
                            {
                                pos.Rows[0]["Dprice"] = Convert.ToInt32(discount.Rows[0]["SpecialRate"].ToString());
                                disamt = 1;
                            }
                            else
                            {
                                disamt = 0;
                            }
                            pos.AcceptChanges();
                        }
                        else
                        {
                            disper = 0;
                            disamt = 0;
                        }
                        // dt = sql.getdataset("Select p.Product_Name,p.Convfactor,(pp.saleprice * 100)/(100+ (pp.vat+pp.addvat)) as rate,p.Convfactor*((pp.saleprice * 100)/(100+ (pp.vat+pp.addvat))) as amount,pp.vat,pp.addvat from ProductMaster p inner join ProductPriceMaster pp on p.ProductID = pp.Productid where p.ProductID = pp.Productid and p.Product_Name='" + txtbarcode.Text + "'or pp.barcode='" + txtbarcode.Text + "'");
                        //  dt = sql.getdataset("select i.itemname,i.qty,i.mrp,i.discount,i.barcode,t.taxper from taxsalb t inner join itemmaster i on i.taxslab=t.id  where barcode='" + txtbarcode.Text + "' or i.itemname='" + txtbarcode.Text + "'");
                        if (pos.Rows.Count > 0)
                        {
                            //Double cess = Convert.ToDouble(dt.Rows[0]["Dprice"].ToString()) * Convert.ToDouble(dt.Rows[0]["cessper"].ToString()) / 100;
                            //Double cessamt = Convert.ToDouble(dt.Rows[0]["cessamt"].ToString());
                            //Double tcess = cess + cessamt;

                            //itemcalculate(dt.Rows[0]["Barcode"].ToString(), dt.Rows[0]["Product_Name"].ToString(), Convert.ToDouble(dt.Rows[0]["Dprice"].ToString()), disper, disamt, Convert.ToDouble(dt.Rows[0]["igst"].ToString()), Convert.ToDouble(dt.Rows[0]["additax"].ToString()));
                            itemcalculate(pos.Rows[0]["Barcode"].ToString(), pos.Rows[0]["Product_Name"].ToString(), Convert.ToDouble(pos.Rows[0]["Dprice"].ToString()), disper, disamt, Convert.ToDouble(pos.Rows[0]["igst"].ToString()), Convert.ToDouble(pos.Rows[0]["additax"].ToString()), Convert.ToDouble(pos.Rows[0]["cessper"].ToString()), Convert.ToDouble(pos.Rows[0]["cessamt"].ToString()));
                        }
                    }
                    else
                    {
                        DataTable productid = new DataTable();
                        productid = conn.getdataset("select DISTINCT p.taxslab,p.ProductID from ProductMaster p inner join ProductPriceMaster pp on p.Productid=pp.Productid where   (p.Product_Name='" + Itemname + "'or pp.barcode='" + Itemname + "') and pp.BatchNo='" + batchno + "' and p.isactive=1 and pp.isactive=1");

                        pos = conn.getdataset("select p.cessper,p.cessamt,pp.Barcode,p.Product_Name,pp." + Dprice + " as Dprice,pp.BasicPrice,i.igst,i.additax,i.cgst,i.sgst from ProductPriceMaster pp inner join ProductMaster p on p.ProductID=pp.Productid  inner join TaxSlabMaster i on i.Taxslabname=p.taxslab where p.ProductID='" + productid.Rows[0]["ProductID"].ToString() + "' and p.taxslab='" + productid.Rows[0]["taxslab"].ToString() + "'and i.saletypename='" + lblsaletype.Text + "' and pp.BatchNo='" + batchno + "' and p.isactive=1 and pp.isactive=1 and i.isactive=1");
                        //   dt = sql.getdataset("select p.*,b.* from productmaster p inner join ProductPriceMaster b on p.ProductID=b.ProductID where b.barcode='" + txtbarcode.Text + "' or p.product_name like '%" + txtbarcode.Text + "%'");
                        //  dt = sql.getdataset("Select p.Product_Name,p.Unit,pp.saleprice  as rate,pp.MRP as Amount from ProductMaster p inner join ProductPriceMaster pp on p.ProductID = pp.Productid where p.ProductID = pp.Productid and p.Product_Name='" + txtbarcode.Text + "'or pp.barcode='" + txtbarcode.Text + "'");


                        //  dt = sql.getdataset("Select p.Product_Name,p.Convfactor,(pp.saleprice * 100)/(100+ (pp.vat+pp.addvat)) as rate,p.Convfactor*((pp.saleprice * 100)/(100+ (pp.vat+pp.addvat))) as amount,pp.vat,pp.addvat from ProductMaster p inner join ProductPriceMaster pp on p.ProductID = pp.Productid where p.ProductID = pp.Productid and p.Product_Name='" + txtbarcode.Text + "'or pp.barcode='" + txtbarcode.Text + "'");
                        //   dt = sql.getdataset("select i.qty,i.mrp,i.discount,i.barcode,t.Product_Name from ProductMaster t inner join ProductPriceMaster i on i.taxslab=t.id  where barcode='" + txtbarcode.Text + "' or i.itemname='" + txtbarcode.Text + "'");
                        if (pos.Rows.Count > 0)
                        {

                            Double qty = Convert.ToDouble(updateqty);
                            Double netamt = Convert.ToDouble(price);
                            //  Double tcess = Convert.ToDouble(dr["cess"]);
                            Double igst = Convert.ToDouble(pos.Rows[0]["igst"].ToString());
                            Double addtax = Convert.ToDouble(pos.Rows[0]["additax"].ToString());
                            Double dprice = Convert.ToDouble(price);
								  
                            qty++;
                            //double basicprice = price * qty;
                            //double disa = price * disper / 100;
                            //double mrp1 = price - disa;
                            //double tax = mrp1 * igst;
                            double MRP = Convert.ToDouble(price);
                            double total = MRP;
                            total = Math.Round(total, 2);
                            double cessper1 = Convert.ToDouble(pos.Rows[0]["cessper"].ToString());
                            double cessamt1 = Convert.ToDouble(pos.Rows[0]["cessamt"].ToString());
                            //consider example of MRP=67.83, cessamt1=20, igst=18, addtax=0, cessper=4 and get the total tax amt including of all kind of taxes with cess also. as per the example the tax amt is: 8.63
                            string saletype = conn.ExecuteScalar("select TaxTypename from PurchasetypeMaster where Purchasetypename='" + lblsaletype.Text + "'");
                            double tax = 0;
                            double aftrdisamt = 0;
                            if (!string.IsNullOrEmpty(saletype))
                            {
                                if (saletype == "Tax Exclusive")
                                {
                                    tax = ((MRP - cessamt1) * (igst + addtax + cessper1) / (100 + (igst + addtax + cessper1)));
                                    aftrdisamt = MRP;
                                }
                                else if (saletype == "Tax Free")
                                {
                                    tax = 0;
                                    aftrdisamt = MRP - cessamt1 - tax;
                                }
                                else
                                {
                                    tax = ((MRP - cessamt1) * (igst + addtax + cessper1) / (100 + (igst + addtax + cessper1)));
                                    aftrdisamt = MRP - cessamt1 - tax;
                                }
                            }
                            // double tax = ((MRP - cessamt1) * (igst + addtax + cessper1) / (100 + (igst + addtax + cessper1)));
                            //    if (addtax != 0)
                            //    {
                            //        tax = tax * addtax / 100;
                            //    }
                            //  tax = Math.Round(tax, 2);

                            //this amount represent the before discount deduct from sale/mrp value. as per the example the amount should be 39.2
                            //   double aftrdisamt = MRP - cessamt1 - tax;
                            double rate = aftrdisamt;
                            DataTable discount = new DataTable();
                            discount = conn.getdataset("select Discount,SpecialRate from PartyRates where ItemID='" + productid.Rows[0]["ProductID"].ToString() + "'");
                            if (discount.Rows.Count > 0)
                            {
                                disper = Convert.ToInt32(discount.Rows[0]["Discount"].ToString());
                                if (Convert.ToInt32(discount.Rows[0]["SpecialRate"].ToString()) > 0)
                                {
                                    pos.Rows[0]["Dprice"] = Convert.ToInt32(discount.Rows[0]["SpecialRate"].ToString());
                                    disamt = 1;
                                }
                                else
                                {
                                    disamt = 0;
                                }
                                pos.AcceptChanges();
                            }
                            else
                            {
                                disper = 0;
                                disamt = 0;
                            }
                            //if (disper > 0 && disamt == 0)
                            //{
                            //    double bfrdisamt = (aftrdisamt * 100) / (100 - disper);
                            //    rate = bfrdisamt;
                            //}
                            //else if (disper > 0 && disamt == 1)
                            //{
                            //    rate = aftrdisamt;
                            //}
                            //else if (disper == 0 && disamt == 1)
                            //{
                            //    rate = aftrdisamt;
                            //}
                            rate = Math.Round(MRP, 2);
                            double basicprice = rate * qty;
                            basicprice = Math.Round(basicprice, 2);
                            double disa = disamt;
                            double[] dis = new double[2];
                            dis = calculatediscount(pos.Rows[0]["Product_Name"].ToString(), "ALL PARTIES", basicprice);
                            double cess1 = 0;
                            double adtax = 0;
                            double atax = 0;
                            if (saletype == "Tax Exclusive")
                            {
                                tax = Math.Round((basicprice - dis[1]) * igst / (100), 2);
                                cess1 = Math.Round((basicprice - dis[1]) * cessper1 / (100), 2);
                                cess1 = cess1 + (cessamt1 * qty);
                                disper = Math.Round(dis[0], 2);
                                disa = Math.Round(dis[1], 2);
                                adtax = Math.Round((basicprice - dis[1]) * (addtax) / (100), 2);
                                atax = Math.Round(adtax, 2);
                            }
                            else if (saletype == "Tax Free")
                            {
                                tax = 0;
                                cess1 = 0;
                                cess1 = cess1 + (cessamt1 * qty);
                                disper = Math.Round(dis[0], 2);
                                disa = Math.Round(dis[1], 2);
                                adtax = 0;
                                atax = 0;
                            }
                            else
                            {
                                tax = Math.Round((basicprice - dis[1]) * igst / (100), 2);
                                cess1 = Math.Round((basicprice - dis[1]) * cessper1 / (100), 2);
                                cess1 = cess1 + (cessamt1 * qty);
                                disper = Math.Round(dis[0], 2);
                                disa = Math.Round(dis[1], 2);
                                adtax = Math.Round((basicprice - dis[1]) * (addtax) / (100), 2);
                                atax = Math.Round(adtax, 2);
                            }
                            //tax = Math.Round((basicprice - dis[1]) * igst / (100), 2);
                            //double cess1 = Math.Round((basicprice - dis[1]) * cessper1 / (100), 2);
                            //cess1 = cess1 + (cessamt1 * qty);
                            //disper = Math.Round(dis[0], 2);
                            //disa = Math.Round(dis[1], 2);
                            //if (disper != 0)
                            //{
                            //   disa = (Convert.ToDouble(basicprice) * Convert.ToDouble(disper)) / 100;
                            //}
                            //else
                            //{
                            //    disper = (100 * Convert.ToDouble(disamt)) / Convert.ToDouble(basicprice);
                            //}
                            // disper = Math.Round(disper, 2);


                            //double adtax = Math.Round((basicprice - dis[1]) * (addtax) / (100), 2);
                            //double atax = Math.Round(adtax, 2);
                            //double aatax = Convert.ToDouble(txtaddtax.Text);
                            //double a = aatax + atax;
                            //txtaddtax.Text = Convert.ToString(a);


                            double amount = basicprice - disa + tax + atax + cess1;
                            amount = Math.Round(amount, 2);


                            //   lblroundof.Text = Math.Round((Math.Round(amount, 0) - amount), 2).ToString();
                            basicprice = rate * qty;
                            //double[] dis = new double[2];
                            //dis = calculatediscount(txtbarcode.Text, "ALL PARTIES", basicprice);
                            //  Double cess = Convert.ToDouble(basicprice) * Convert.ToDouble(dt.Rows[0]["cessper"].ToString()) / 100;
                            //  Double cessamt = Convert.ToDouble(dt.Rows[0]["cessamt"].ToString());
                            //  Double tcess = cess + cessamt;
                            double cessval = cess1;

                            lvItem.Items[rowid].SubItems[1].Text = Convert.ToString(qty);
                            lvItem.Items[rowid].SubItems[7].Text = Convert.ToString(basicprice.ToString("N2"));
                            lvItem.Items[rowid].SubItems[3].Text = Convert.ToString(dis[0]);
                            lvItem.Items[rowid].SubItems[8].Text = Convert.ToString(dis[1]);
                            lvItem.Items[rowid].SubItems[4].Text = Convert.ToString(tax.ToString("N2"));
                            lvItem.Items[rowid].SubItems[9].Text = Convert.ToString(atax.ToString("N2"));
                            lvItem.Items[rowid].SubItems[5].Text = Convert.ToString(amount.ToString("N2"));
                            lvItem.Items[rowid].SubItems[10].Text = Convert.ToString(amount.ToString("N2"));
                            lvItem.Items[rowid].SubItems[12].Text = Convert.ToString(cessval.ToString("N2"));
							lvItem.Items[rowid].SubItems[13].Text = Convert.ToString(cessval.ToString("N2"));
                            lvItem.Items[rowid].SubItems[14].Text = existserial.Trim() + "," + serialno.Trim();
																				 
										 


                            finaltotal();
                            //finaltotalcoumn();
                        }
                    }
                }



                else
                #region
                {

                    DataTable productid = new DataTable();
                    productid = conn.getdataset("select DISTINCT p.taxslab,p.ProductID from ProductMaster p inner join ProductPriceMaster pp on p.Productid=pp.Productid where (p.Product_Name='" + Itemname + "'or pp.barcode='" + Itemname + "') and pp.BatchNo='" + batchno + "' and p.isactive=1 and pp.isactive=1");
                    double disper;
                    double disamt;
                    pos = conn.getdataset("select p.cessper,p.cessamt,pp.Barcode,p.Product_Name,pp." + Dprice + " as Dprice,pp.BasicPrice,isnull(i.igst,0)as igst,isnull(i.additax,0)as additax,isnull(i.cgst,0)as cgst,isnull(i.sgst,0)as sgst from ProductPriceMaster pp inner join ProductMaster p on p.ProductID=pp.Productid  left join TaxSlabMaster i on i.Taxslabname=p.taxslab where p.ProductID='" + productid.Rows[0]["ProductID"].ToString() + "' and pp.isactive=1 and p.isactive=1 and i.isactive=1 and p.taxslab='" + productid.Rows[0]["taxslab"].ToString() + "' and pp.BatchNo='" + batchno + "' and i.saletypename='" + lblsaletype.Text + "'");
                    if (pos.Rows.Count > 0)
                    {
                        DataTable discount = new DataTable();
                        discount = conn.getdataset("select Discount,SpecialRate from PartyRates where ItemID='" + productid.Rows[0]["ProductID"].ToString() + "'");
                        if (discount.Rows.Count > 0)
                        {
                            disper = Convert.ToInt32(discount.Rows[0]["Discount"].ToString());
                            if (Convert.ToInt32(discount.Rows[0]["SpecialRate"].ToString()) > 0)
                            {
                                pos.Rows[0]["Dprice"] = Convert.ToInt32(discount.Rows[0]["SpecialRate"].ToString());
                                disamt = 1;
                            }
                            else
                            {
                                disamt = 0;
                            }
                            pos.AcceptChanges();
                        }
                        else
                        {
                            disper = 0;
                            disamt = 0;
                        }
				 
                        itemcalculate(pos.Rows[0]["Barcode"].ToString(), pos.Rows[0]["Product_Name"].ToString(), Convert.ToDouble(pos.Rows[0]["Dprice"].ToString()), disper, disamt, Convert.ToDouble(pos.Rows[0]["igst"].ToString()), Convert.ToDouble(pos.Rows[0]["additax"].ToString()), Convert.ToDouble(pos.Rows[0]["cessper"].ToString()), Convert.ToDouble(pos.Rows[0]["cessamt"].ToString()));
                    }
                }
				#endregion		  
            }
            catch
            {
            }
        }
        private DataTable changedtclone(DataTable dt)
        {
            DataTable dtClone = dt.Clone(); //just copy structure, no data
            for (int i = 0; i < dtClone.Columns.Count; i++)
            {
                if (dtClone.Columns[i].DataType != typeof(string))
                    dtClone.Columns[i].DataType = typeof(string);
            }

            foreach (DataRow dr in dt.Rows)
            {
                dtClone.ImportRow(dr);
            }
            return dtClone;
        }
        decimal debit1 = 0;
        decimal credit1 = 0;
        private string getbalance(double opbal, double p, String DC, String actualdc, int i)
        {
            double balance;
            string bal = "";

            if (actualdc == "C")
            {
                balance = opbal + p;
                bal = balance.ToString("N2") + "";
            }

            else
            {
                balance = opbal - p;
                bal = balance.ToString("N2") + "";
            }

            return bal;
        }
        private void getstock(string p)
        {
            #region
            string proid = conn.ExecuteScalar("select Productid from ProductMaster where isactive=1 and Product_Name='" + p + "'");
            string openingstockfromitem = conn.ExecuteScalar("select opstock from productpricemaster where isactive=1 and productid= '" + proid + "'");
            string totalPurchase = "";
            string totalSale = "";
            if (totalPurchase == "" || totalPurchase == "NULL")
            {
                totalPurchase = "0.00";
            }
            if (totalSale == "" || totalSale == "NULL")
            {
                totalSale = "0.00";
            }
            Double opbal;
            string DC = "";
            if (Convert.ToDouble(totalPurchase) >= Convert.ToDouble(totalSale))
            {
                opbal = Convert.ToDouble(totalPurchase) - Convert.ToDouble(totalSale) + Convert.ToDouble(openingstockfromitem);
                // txtopbal.Text = opbal.ToString("N2");
                DC = "D";
            }
            else
            {
                opbal = Convert.ToDouble(totalSale) - Convert.ToDouble(totalPurchase) + Convert.ToDouble(openingstockfromitem);
                // txtopbal.Text = opbal.ToString("N2");
                DC = "C";
            }
            #endregion

            #region
            DataTable pos = conn.getdataset("select 'POS' as Billtype,BillDate as Bill_Run_Date,billno,totalnet as Rate,totalqty as pqty,BillId as Bill_No  from BillPOSMaster where isactive=1 order by BillDate asc");
            pos = changedtclone(pos);

            DataTable SPdt = conn.getdataset("select Billtype,Bill_Run_Date,billno,Rate,Pqty,Bill_No from billproductmaster where isactive=1 and productid='" + proid + "' order by bill_Run_Date");
            string balance = "0.00";
            balance = Convert.ToString(opbal);
            Double debit = 0, credit = 0;
            SPdt = changedtclone(SPdt);
            SPdt.Merge(pos);
            SPdt.DefaultView.Sort = "[bill_Run_Date] DESC";
            SPdt = SPdt.DefaultView.ToTable();
            debit = 0;
            credit = 0;
            debit1 = 0;
            credit1 = 0;
            for (int i = 0; i < SPdt.Rows.Count; i++)
            {
                if (SPdt.Rows[i]["Billtype"].ToString() == "S")
                {
                    if (i != 0)
                    {
                        string[] str = balance.Split(' ');
                        char temp = str[0][0];
                        DC = temp.ToString();
                        opbal = Convert.ToDouble(str[0]);
                    }
                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "D", i);
                }
                else if (SPdt.Rows[i]["Billtype"].ToString() == "POS")
                {
                    if (i != 0)
                    {
                        string[] str = balance.Split(' ');
                        char temp = str[0][0];
                        DC = temp.ToString();
                        opbal = Convert.ToDouble(str[0]);
                    }
                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "D", i);
                }
                else if (SPdt.Rows[i]["Billtype"].ToString() == "SR")
                {
                    if (i != 0)
                    {
                        string[] str = balance.Split(' ');
                        char temp = str[0][0];
                        DC = temp.ToString();
                        opbal = Convert.ToDouble(str[0]);
                    }
                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "C", i);
                }
                else if (SPdt.Rows[i]["Billtype"].ToString() == "P")
                {
                    if (i != 0)
                    {
                        string[] str = balance.Split(' ');
                        char temp = str[0][0];
                        DC = temp.ToString();
                        opbal = Convert.ToDouble(str[0]);
                    }
                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "C", i);
                }
                else if (SPdt.Rows[i]["Billtype"].ToString() == "PR")
                {
                    if (i != 0)
                    {
                        string[] str = balance.Split(' ');
                        char temp = str[0][0];
                        DC = temp.ToString();
                        opbal = Convert.ToDouble(str[0]);
                    }
                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "D", i);
                }

            }
            DataTable stockadjustment = conn.getdataset("select sm.*,sim.* from stockadujestmentmaster sm inner join stockadujestmentitemmaster sim on sm.id=sim.stockid where sm.isactive=1 and sim.isactive=1 and sim.itemid='" + proid + "' order by sm.stockdate");
            for (int i = 0; i < stockadjustment.Rows.Count; i++)
            {

                if (Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString()) > 0)
                {
                    credit += Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString());
                }
                else
                {
                    Double d = Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString());
                    Double a1 = d * -1;
                    debit += Convert.ToDouble(a1);
                }
                Double bal = Convert.ToDouble(balance);
                Double astock = Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString());
                Double fbalance = bal + astock;
                balance = Convert.ToString(fbalance);
            }
            string minstock = conn.ExecuteScalar("select minstock from ProductMaster where isactive=1 and ProductID='" + proid + "'");
            Double a = Convert.ToDouble(minstock);
            Double b = Convert.ToDouble(balance);
            if (a >= b)
            {
                MessageBox.Show(balance, "Min Stock level");
            }
            #endregion
        }
        public void enteritem()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtitemname.Text))
                {

                    string v = txtitemname.Text.Replace("*", "");
                    Itemname = v;
                }
                DataTable batch = conn.getdataset("select p.ProductID,P.Product_Name,pp.* from ProductMaster p inner join ProductPriceMaster pp on p.Productid=pp.Productid where p.isactive=1 and pp.isactive=1 and  (p.Product_Name='" + Itemname + "'or pp.barcode='" + Itemname + "')");
                if (options.Rows[0]["requirstockinfo"].ToString() == "True")
                {
                    getstock(batch.Rows[0]["Product_Name"].ToString());
                }
                if (batch.Rows.Count == 1)
                {
                    batchno = batch.Rows[0]["batchno"].ToString();
				string isserial = conn.getsinglevalue("select DISTINCT p.isserial from ProductMaster p inner join ProductPriceMaster pp on p.Productid=pp.Productid where (p.Product_Name='" + Itemname + "'or pp.barcode='" + Itemname + "') and p.isactive=1 and pp.isactive=1");
                    if (isserial == "True")
                    {
                        pnlserial.Visible = true;
                        txtserial.Focus();
                    }
                    else
                    {
                        insertdata();
                    }
                }
                else
                {
                    POSMultiProduct si = new POSMultiProduct(this, batch.Rows[0]["ProductID"].ToString(), Itemname);
                    si.ShowDialog();
				string isserial = conn.getsinglevalue("select DISTINCT p.isserial from ProductMaster p inner join ProductPriceMaster pp on p.Productid=pp.Productid where (p.Product_Name='" + Itemname + "'or pp.barcode='" + Itemname + "') and p.isactive=1 and pp.isactive=1");
                    if (isserial == "True")
                    {
                        pnlserial.Visible = true;
                        txtserial.Focus();
                    }
                    else
                    {
                        insertdata();
                    }
				 
                }
            }
            catch
            {
            }
        }
        int flagforagent = 0;
        private POSMultiProduct pOSMultiProduct;
        private string batch;
        protected void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Button button = sender as Button;
                Itemname = button.Text;

                if (options.Rows[0]["requiragentnameinpos"].ToString() == "Ask For Agent After Item Entry")
                {
                    pnlagent.Visible = true;
                    cmbagentname.Focus();
                }
                else if (options.Rows[0]["requiragentnameinpos"].ToString() == "Ask For Agent Bill Wise")
                {
                    if (flagforagent == 0)
                    {
                        pnlagent.Visible = true;
                        cmbagentname.Focus();
                        flagforagent = 1;
                    }
                    else
                    {
                        enteritem();
                    }
                }
                else
                {
                    enteritem();
                }





                #region
                //  double disper;
                //  double disamt;

                ////  string Dprice = conn.ExecuteScalar("select Defaultprice from Options");
                //  options = conn.getdataset("select * from options");
                //  lblsaletype.Text = options.Rows[0]["autosaletype"].ToString();
                //  string Dprice = options.Rows[0]["Defaultprice"].ToString();
                //  string proid = conn.ExecuteScalar("select ProductID from ProductMaster where isactive=1 and isHotProduct=1 and Product_Name='"+button.Text+"'");
                //  DataTable dt = conn.getdataset("select *,"+Dprice+" as dprice from ProductPriceMaster where isactive=1 and Productid='"+proid+"'");

                //  bool exists = false;
                //  foreach (ListViewItem item in lvItem.Items)
                //  {
                //      for (int b = 0; b < item.SubItems.Count; b++)
                //      {
                //          string iname = item.SubItems[0].Text;
                //          if (button.Text == iname)
                //          {
                //              rowid = item.Index;
                //              updateqty = item.SubItems[1].Text;
                //              price = item.SubItems[2].Text;
                //              exists = true;
                //          }
                //      }
                //  }
                //  if (!exists)
                //  {
                //      qty = 1;
                //      li = lvItem.Items.Add(button.Text);
                //      li.SubItems.Add(Convert.ToString(qty));
                //      li.SubItems.Add(dt.Rows[0]["dprice"].ToString());
                //      DataTable discount = new DataTable();
                //      discount = conn.getdataset("select Discount,SpecialRate from PartyRates where ItemID='" + proid + "'");
                //      if (discount.Rows.Count > 0)
                //      {
                //          disper = Convert.ToInt32(discount.Rows[0]["Discount"].ToString());
                //          disamt = Convert.ToInt32(discount.Rows[0]["SpecialRate"].ToString());
                //      }
                //      else
                //      {
                //          disper = 0;
                //          disamt = 0;
                //      }
                //      if (disper > 0)
                //      {
                //          finaldis = (Convert.ToDouble(dt.Rows[0]["dprice"].ToString()) * 100) / (100 - disper);
                //      }
                //      else
                //      {
                //          finaldis = disamt;
                //      }
                //      li.SubItems.Add(Convert.ToString(finaldis.ToString("N2")));
                //      Double total = Convert.ToDouble(dt.Rows[0]["dprice"].ToString()) * qty;
                //      total = total - finaldis;
                //      li.SubItems.Add(Convert.ToString(total.ToString("N2")));
                //  }
                //  else
                //  {
                //      qty = Convert.ToDouble(updateqty) + 1;
                //      lvItem.Items[rowid].SubItems[1].Text = Convert.ToString(qty);
                //      DataTable discount = new DataTable();
                //      discount = conn.getdataset("select Discount,SpecialRate from PartyRates where ItemID='" + proid + "'");
                //      if (discount.Rows.Count > 0)
                //      {
                //          disper = Convert.ToInt32(discount.Rows[0]["Discount"].ToString());
                //          disamt = Convert.ToInt32(discount.Rows[0]["SpecialRate"].ToString());
                //      }
                //      else
                //      {
                //          disper = 0;
                //          disamt = 0;
                //      }
                //      if (disper > 0)
                //      {
                //          finaldis = (Convert.ToDouble(dt.Rows[0]["dprice"].ToString()) * 100) / (100 - disper);
                //      }
                //      else
                //      {
                //          finaldis = disamt;
                //      }
                //      lvItem.Items[rowid].SubItems[3].Text = Convert.ToString(finaldis.ToString("N2"));
                //      Double total = Convert.ToDouble(dt.Rows[0]["dprice"].ToString()) * qty;
                //      total = total - finaldis;
                //      lvItem.Items[rowid].SubItems[4].Text = Convert.ToString(total.ToString("N2"));
                //  }
                #endregion

            }
            catch
            {
            }
        }
        public void binaagent()
        {
            string qry = "";
            qry = "select ClientID,AccountName from ClientMaster where isactive=1 and groupID=50 order by AccountName";
            SqlCommand cmd1 = new SqlCommand(qry, con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            cmbagentname.ValueMember = "ClientID";
            cmbagentname.DisplayMember = "AccountName";
            cmbagentname.DataSource = dt1;
            cmbagentname.SelectedIndex = -1;
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
                qry = qry + " where isactive=1 order by ProductMaster.Product_Name";

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
                        // MessageBox.Show("Data not found");
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
                        //MessageBox.Show("Data not found");
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
        int cnt = 0;
        DataTable userrights = new DataTable();
        private void POSNEW_Load(object sender, EventArgs e)
        {
            try
            {
                if (cnt == 0)
                {
												  
                    loadpage();
                    userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[9]["d"].ToString() == "False")
                        {
                            btnDeleteItem.Enabled = false;
                        }
                        if (userrights.Rows[9]["p"].ToString() == "False")
                        {
                            button21.Enabled = false;
                        }
                    }
                }

            }
            catch
            {
            }
        }

        private void loadpage()
        {

            options = conn.getdataset("select * from options");
            if (Convert.ToBoolean(options.Rows[0]["ShowTotalBillAmount"].ToString()) == true)
            {
                lblNetAmount.Visible = true;
            }
            else
            {
                lblNetAmount.Visible = false;
            }
            btnd1.Text = options.Rows[0]["r1"].ToString();
            btnd2.Text = options.Rows[0]["r2"].ToString();
            btnd3.Text = options.Rows[0]["r3"].ToString();
            btnd4.Text = options.Rows[0]["r4"].ToString();
            lblsaletype.Text = options.Rows[0]["autosaletype"].ToString();
            Dprice = options.Rows[0]["Defaultprice"].ToString();
            lvItem.Columns.Add("Description", 200, HorizontalAlignment.Left);
            lvItem.Columns.Add("Qty", 50, HorizontalAlignment.Center);
            lvItem.Columns.Add("Unit Price", 90, HorizontalAlignment.Right);
            lvItem.Columns.Add("Dis(%)", 70, HorizontalAlignment.Right);
            lvItem.Columns.Add("GstAmt", 80, HorizontalAlignment.Center);
            lvItem.Columns.Add("Total Price", 100, HorizontalAlignment.Right);
            lvItem.Columns.Add("Barcode", 0, HorizontalAlignment.Center);
            lvItem.Columns.Add("BasicAmt", 0, HorizontalAlignment.Center);
            lvItem.Columns.Add("Disamt", 0, HorizontalAlignment.Center);
            lvItem.Columns.Add("AddTax", 0, HorizontalAlignment.Center);
            lvItem.Columns.Add("Amount", 0, HorizontalAlignment.Center);
            lvItem.Columns.Add("Batchno", 0, HorizontalAlignment.Center);
            lvItem.Columns.Add("cess", 0, HorizontalAlignment.Center);
            lvItem.Columns.Add("AgentID", 0, HorizontalAlignment.Center);
			lvItem.Columns.Add("serial", 0, HorizontalAlignment.Center);																				  
            lvItem.Columns.Add("Delete", 60, HorizontalAlignment.Center);

            DataTable dt = conn.getdataset("select distinct(companyname) from CompanyMaster");
            DataTable dt1 = conn.getdataset("select distinct(GroupName) from ProductMaster where isactive=1 and isHotProduct=1");

            bindbuttons(dt);
            bindbuttons1(dt1);
            if (Convert.ToBoolean(options.Rows[0]["itemload"].ToString()) == false)
            {
                DataTable dt2 = conn.getdataset("select distinct(Product_Name) from ProductMaster where isactive=1 and isHotProduct=1");
                bindbuttons2(dt2);
            }
            binaagent();
            autoreaderbind();
            this.ActiveControl = txtitemname;
            lbldate.Text = Convert.ToString(DateTime.Now.ToString("dd-MM-yyyy"));
            try
            {
                DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
                if (Convert.ToDateTime(lbldate.Text) > Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString()))
                {
                    string date = Convert.ToString(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                    lbldate.Text = Convert.ToDateTime(date).ToString("dd-MM-yyyy");
                }
            }
            catch
            {
            }

        }
        public double[] calculatediscount(string itemname, string partyname, double basicprice)
        {
            double itemper = 0, itemamt = 0, discount = 0, disamt = 0, dispercompany = 0, disamtcompant = 0;
            string discountval = "";
            DataTable dt12 = conn.getdataset("select * from PartyRates where itemname='" + itemname + "' and (PartyName='" + partyname + "' OR PartyID='0')");

            if (dt12.Rows.Count > 0)
            {
                itemper = Convert.ToDouble(dt12.Rows[0]["Discount"].ToString());
                itemamt = Convert.ToDouble(dt12.Rows[0]["SpecialRate"].ToString());
            }
            DataTable dtgroup = conn.getdataset("select * from ProductMaster where Product_Name='" + itemname + "'");
            string groupid = dtgroup.Rows[0]["GroupName"].ToString();
            string companyid = dtgroup.Rows[0]["CompanyID"].ToString();
            DataTable dt123 = conn.getdataset("select * from PartyGroupDiscount where ItemGroupName='" + groupid + "'and(PartyName='" + partyname + "'OR PartyID=' ')");


            if (dt123.Rows.Count > 0)
            {
                discount = Convert.ToDouble(dt123.Rows[0]["Discount"].ToString());
                disamt = Convert.ToDouble(dt123.Rows[0]["SpecialRate"].ToString());
            }

            DataTable dt1234 = conn.getdataset("select * from PartyCompanyDiscount where ItemCompanyID='" + companyid + "'and(PartyName='" + partyname + "'OR PartyID=' ')");
            if (dt1234.Rows.Count > 0)
            {
                dispercompany = Convert.ToDouble(dt1234.Rows[0]["Discount"].ToString());
                disamtcompant = Convert.ToDouble(dt1234.Rows[0]["SpecialRate"].ToString());
            }

            double disper = 0, disa = 0;
            if (itemper > 0)
            {
                disper = itemper;
                disa = 0;
                if (disper != 0)
                {
                    disa = (Convert.ToDouble(basicprice) * Convert.ToDouble(disper)) / 100;
                }
                else
                {
                    disper = (100 * Convert.ToDouble(disa)) / Convert.ToDouble(basicprice);
                }
                disper = Math.Round(disper, 2);
                disamt = Math.Round(disa, 2);

            }
            else if (itemamt > 0)
            {
                //disper = 0;
                //disa = itemamt;
                //if (disper != 0)
                //{
                //    disa = (Convert.ToDouble(basicprice) * Convert.ToDouble(disper)) / 100;
                //}
                //else
                //{
                //    disper = (100 * Convert.ToDouble(disa)) / Convert.ToDouble(basicprice);
                //}
                //disper = Math.Round(disper, 2);
                //disamt = Math.Round(disa, 2);

            }
            else if (discount > 0)
            {
                disper = discount;
                disa = 0;
                if (disper != 0)
                {
                    disa = (Convert.ToDouble(basicprice) * Convert.ToDouble(disper)) / 100;
                }
                else
                {
                    disper = (100 * Convert.ToDouble(disa)) / Convert.ToDouble(basicprice);
                }
                disper = Math.Round(disper, 2);
                disamt = Math.Round(disa, 2);
            }
            else if (disamt > 0)
            {
                //disper = 0;
                //disa = disamt;
                //if (disper != 0)
                //{
                //    disa = (Convert.ToDouble(basicprice) * Convert.ToDouble(disper)) / 100;
                //}
                //else
                //{
                //    disper = (100 * Convert.ToDouble(disa)) / Convert.ToDouble(basicprice);
                //}
                //disper = Math.Round(disper, 2);
                //disamt = Math.Round(disa, 2);
            }
            else if (dispercompany > 0)
            {
                disper = dispercompany;
                disa = 0;
                if (disper != 0)
                {
                    disa = (Convert.ToDouble(basicprice) * Convert.ToDouble(disper)) / 100;
                }
                else
                {
                    disper = (100 * Convert.ToDouble(disa)) / Convert.ToDouble(basicprice);
                }
                disper = Math.Round(disper, 2);
                disamt = Math.Round(disa, 2);
            }
            else if (disamtcompant > 0)
            {
                //disper = 0;
                //disa = disamtcompant;
                //if (disper != 0)
                //{
                //    disa = (Convert.ToDouble(basicprice) * Convert.ToDouble(disper)) / 100;
                //}
                //else
                //{
                //    disper = (100 * Convert.ToDouble(disa)) / Convert.ToDouble(basicprice);
                //}
                //disper = Math.Round(disper, 2);
                //disamt = Math.Round(disa, 2);
            }
            double[] dis = new double[2];
            dis[0] = disper;
            dis[1] = disamt;
            return dis;
        }
        public void itemcalculate(String barcode, String itemname, double MRP, double disper, double disamt, double igst, double addtax, double cessper1, double cessamt1)
        {
            int qty = 1;
            double total = MRP;
            total = Math.Round(total, 2);
            //consider example of MRP=67.83, cessamt1=20, igst=18, addtax=0, cessper=4 and get the total tax amt including of all kind of taxes with cess also. as per the example the tax amt is: 8.63
            string saletype = conn.ExecuteScalar("select TaxTypename from PurchasetypeMaster where Purchasetypename='" + lblsaletype.Text + "'");
            double tax = 0;
            double aftrdisamt = 0;
            if (!string.IsNullOrEmpty(saletype))
            {
                if (saletype == "Tax Exclusive")
                {
                    tax = ((MRP - cessamt1) * (igst + addtax + cessper1) / (100 + (igst + addtax + cessper1)));
                    aftrdisamt = MRP;
                }
                else if (saletype == "Tax Free")
                {
                    tax = 0;
                    aftrdisamt = MRP - cessamt1 - tax;
                }
                else
                {
                    tax = ((MRP - cessamt1) * (igst + addtax + cessper1) / (100 + (igst + addtax + cessper1)));
                    aftrdisamt = MRP - cessamt1 - tax;
                }
            }
            //this amount represent the before discount deduct from sale/mrp value. as per the example the amount should be 39.2

            double rate = aftrdisamt;
            //this is the actual rate of item. as per the example the amount is 40.
            //if (disper > 0 && disamt == 0)
            //{
            //    double bfrdisamt = (aftrdisamt * 100) / (100 - disper);
            //    rate = bfrdisamt;
            //}
            //else if (disper > 0 && disamt == 1)
            //{
            //    rate = aftrdisamt;
            //}
            //else if (disper == 0 && disamt == 1)
            //{
            //    rate = aftrdisamt;
            //}
            rate = Math.Round(rate, 2);
            double basicprice = rate * qty;
            basicprice = Math.Round(basicprice, 2);
            double disa = disamt;
            double[] dis = new double[2];
            dis = calculatediscount(pos.Rows[0]["Product_Name"].ToString(), "ALL PARTIES", basicprice);
            double cess1 = 0;
            double adtax = 0;
            double atax = 0;
            if (saletype == "Tax Exclusive")
            {
                tax = Math.Round((basicprice - dis[1]) * igst / (100), 2);
                cess1 = Math.Round((basicprice - dis[1]) * cessper1 / (100), 2);
                cess1 = cess1 + (cessamt1 * qty);
                disper = Math.Round(dis[0], 2);
                disa = Math.Round(dis[1], 2);
                adtax = Math.Round((basicprice - dis[1]) * (addtax) / (100), 2);
                atax = Math.Round(adtax, 2);
            }
            else if (saletype == "Tax Free")
            {
                tax = 0;
                cess1 = 0;
                cess1 = cess1 + (cessamt1 * qty);
                disper = Math.Round(dis[0], 2);
                disa = Math.Round(dis[1], 2);
                adtax = 0;
                atax = 0;
            }
            else
            {
                tax = Math.Round((basicprice - dis[1]) * igst / (100), 2);
                cess1 = Math.Round((basicprice - dis[1]) * cessper1 / (100), 2);
                cess1 = cess1 + (cessamt1 * qty);
                disper = Math.Round(dis[0], 2);
                disa = Math.Round(dis[1], 2);
                adtax = Math.Round((basicprice - dis[1]) * (addtax) / (100), 2);
                atax = Math.Round(adtax, 2);
            }
            
            double amount = basicprice - disa + tax + atax + cess1;
            amount = Math.Round(amount, 2);

            li = lvItem.Items.Add(itemname);
            li.BackColor = Color.LightYellow;
            li.SubItems.Add(Convert.ToString(qty));
            li.SubItems.Add(Convert.ToString(rate));
            li.SubItems.Add(Convert.ToString(disper));
            li.SubItems.Add(Convert.ToString(tax));
            li.SubItems.Add(Convert.ToString(amount));
            li.SubItems.Add(Convert.ToString(barcode));
            li.SubItems.Add(Convert.ToString(basicprice));
            li.SubItems.Add(Convert.ToString(disa));
            li.SubItems.Add(Convert.ToString(atax));
            li.SubItems.Add(Convert.ToString(amount));
            li.SubItems.Add(Convert.ToString(batchno));
            li.SubItems.Add(Convert.ToString(cess1));
            li.SubItems.Add(Convert.ToString(cmbagentname.SelectedValue));
li.SubItems.Add(serialno.Trim());
            li.SubItems.Add("Delete", Color.White, Color.Red, Font);
            finaltotal();


            // finaltotalcoumn();
        }
        Double totalqty = 0;
        Double totalunit = 0;
        Double totaldic = 0;
        Double totalprice = 0;
        Double totaldis = 0;
        Double totaltax = 0;
        Int32 count = 0;
        public static string totalforchange;
        public void finaltotal()
        {
            try
            {
                for (int j = 0; j < lvItem.Items.Count; j++)
                {
                    count++;
                    totalqty = totalqty + Convert.ToDouble(lvItem.Items[j].SubItems[1].Text);
                    totalunit = totalunit + Convert.ToDouble(lvItem.Items[j].SubItems[7].Text);
                    totaldic = totaldic + Convert.ToDouble(lvItem.Items[j].SubItems[3].Text);
                    totalprice = totalprice + Convert.ToDouble(lvItem.Items[j].SubItems[5].Text);
                    totaldis = totaldis + Convert.ToDouble(lvItem.Items[j].SubItems[8].Text);
                    totaltax = totaltax + Convert.ToDouble(lvItem.Items[j].SubItems[4].Text);
                }
                txttax.Text = totaltax.ToString();
                txtqty.Text = count.ToString();
                lblqty.Text = totalqty.ToString();
                txttamt.Text = totalunit.ToString("N2");
                //  txtldis.Text = "0";
                txtodis.Text = totaldic.ToString("N2");
                txtodis1.Text = totaldis.ToString("N2");
                //Double d = Convert.ToDouble(txtldis.Text);
                //if (d == 0)
                //{
                //    txtldis.Text = "0.00";
                //}
                txtptotal.Text = totalprice.ToString("N2");
                btntotalachual.Text = totalprice.ToString("N2");
                totalforchange = totalprice.ToString("N2");
                Double t = Math.Round(totalprice);
                lblroundof.Text += Math.Round((Math.Round(t, 0) - t), 2).ToString();
                btntotalroundoff.Text = Convert.ToString(t);
                //   txtpaymenttotalamt.Text = totalprice.ToString("N2");
                // btnpaymentachualamt.Text = totalprice.ToString("N2");
                //btnpaymentroundoff.Text = Convert.ToString(t);
                count = 0;
                totalqty = 0;
                totalunit = 0;
                totaldic = 0;
                totalprice = 0;
                totaldis = 0;
                totaltax = 0;
                t = 0;
            }
            catch
            {
            }
        }

        internal void getdata(string p, string itemname)
        {
            options = conn.getdataset("select * from options");
            Dprice = options.Rows[0]["Defaultprice"].ToString();
            lblsaletype.Text = options.Rows[0]["autosaletype"].ToString();
            batchno = p;
            //  DataTable dt = conn.getdataset("select ProductID from ProductMaster where Product_Name='" + itemname + "'");
            // DataTable dt1 = conn.getdataset("select Barcode from ProductPriceMaster where ProductID='" + dt.Rows[0]["ProductID"].ToString() + "' and BatchNo='" + batchno + "'");
            //    insertdata();
        }
        public static string a = "";
        public static string b = "";
        public static string c = "";
        public static string d = "";
        private ListViewHitTestInfo hitinfo;
        private TextBox editbox = new TextBox();
        void editbox_LostFocus(object sender, EventArgs e)
        {
            if (columnindex == 1 && columnindex == 3)
            {
                hitinfo.SubItem.Text = editbox.Text;
                editbox.Hide();
            }
            else
            {
                editbox.Hide();
            }
        }
        void editbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {

                lvItem.Focus();
                if (columnindex == 1)
                {
                    string q = editbox.Text;
                    a = lvItem.Items[lvItem.FocusedItem.Index].SubItems[0].Text;
                    b = lvItem.Items[lvItem.FocusedItem.Index].SubItems[11].Text;
                    c = lvItem.Items[lvItem.FocusedItem.Index].SubItems[2].Text;
                    options = conn.getdataset("select * from options");
                    Dprice = options.Rows[0]["Defaultprice"].ToString();
                    lblsaletype.Text = options.Rows[0]["autosaletype"].ToString();
                    batchno = b;
                    rowid = lvItem.FocusedItem.Index;


                    #region
                    try
                    {


                        if (!string.IsNullOrEmpty(a))
                        {
                            DataTable productid = new DataTable();
                            productid = conn.getdataset("select DISTINCT p.taxslab,p.ProductID from ProductMaster p inner join ProductPriceMaster pp on p.Productid=pp.Productid where (p.Product_Name='" + a + "'or pp.barcode='" + a + "') and p.isactive=1 and pp.isactive=1");

                            pos = conn.getdataset("select p.cessper,p.cessamt,pp.Barcode,p.Product_Name,pp." + Dprice + " as Dprice,pp.BasicPrice,i.igst,i.additax,i.cgst,i.sgst from ProductPriceMaster pp inner join ProductMaster p on p.ProductID=pp.Productid  inner join TaxSlabMaster i on i.Taxslabname=p.taxslab where p.ProductID='" + productid.Rows[0]["ProductID"].ToString() + "' and p.taxslab='" + productid.Rows[0]["taxslab"].ToString() + "'and i.saletypename='" + lblsaletype.Text + "' and p.isactive=1 and pp.isactive=1 and i.isactive=1");

                            if (pos.Rows.Count > 0)
                            {

                                Double qty = Convert.ToDouble(q);
                                Double igst = Convert.ToDouble(pos.Rows[0]["igst"].ToString());
                                Double netamt = Convert.ToDouble(c);
                                //  Double tcess = Convert.ToDouble(dr["cess"]);
                                Double addtax = Convert.ToDouble(pos.Rows[0]["additax"].ToString());
                                Double dprice = Convert.ToDouble(c);
                                //    qty++;
                                //double basicprice = price * qty;
                                //double disa = price * disper / 100;
                                //double mrp1 = price - disa;
                                //double tax = mrp1 * igst;
                                double MRP = Convert.ToDouble(c);
                                double total = MRP;
                                total = Math.Round(total, 2);
                                double cessper1 = Convert.ToDouble(pos.Rows[0]["cessper"].ToString());
                                double cessamt1 = Convert.ToDouble(pos.Rows[0]["cessamt"].ToString());
                                //consider example of MRP=67.83, cessamt1=20, igst=18, addtax=0, cessper=4 and get the total tax amt including of all kind of taxes with cess also. as per the example the tax amt is: 8.63
                                string saletype = conn.ExecuteScalar("select TaxTypename from PurchasetypeMaster where Purchasetypename='" + lblsaletype.Text + "'");
                                double tax = 0;
                                double aftrdisamt = 0;
                                if (!string.IsNullOrEmpty(saletype))
                                {
                                    if (saletype == "Tax Exclusive")
                                    {
                                        tax = ((MRP - cessamt1) * (igst + addtax + cessper1) / (100 + (igst + addtax + cessper1)));
                                        aftrdisamt = MRP;
                                    }
                                    else if (saletype == "Tax Free")
                                    {
                                        tax = 0;
                                        aftrdisamt = MRP - cessamt1 - tax;
                                    }
                                    else
                                    {
                                        tax = ((MRP - cessamt1) * (igst + addtax + cessper1) / (100 + (igst + addtax + cessper1)));
                                        aftrdisamt = MRP - cessamt1 - tax;
                                    }
                                }
                                // double tax = ((MRP - cessamt1) * (igst + addtax + cessper1) / (100 + (igst + addtax + cessper1)));
                                //    if (addtax != 0)
                                //    {
                                //        tax = tax * addtax / 100;
                                //    }
                                //  tax = Math.Round(tax, 2);
                                //this amount represent the before discount deduct from sale/mrp value. as per the example the amount should be 39.2
                                // double aftrdisamt = MRP - cessamt1 - tax;
                                double rate = aftrdisamt;
                                DataTable discount = new DataTable();
                                discount = conn.getdataset("select Discount,SpecialRate from PartyRates where ItemID='" + productid.Rows[0]["ProductID"].ToString() + "'");
                                if (discount.Rows.Count > 0)
                                {
                                    disper = Convert.ToInt32(discount.Rows[0]["Discount"].ToString());
                                    if (Convert.ToInt32(discount.Rows[0]["SpecialRate"].ToString()) > 0)
                                    {
                                        pos.Rows[0]["Dprice"] = Convert.ToInt32(discount.Rows[0]["SpecialRate"].ToString());
                                        disamt = 1;
                                    }
                                    else
                                    {
                                        disamt = 0;
                                    }
                                    pos.AcceptChanges();
                                }
                                else
                                {
                                    disper = 0;
                                    disamt = 0;
                                }
                                //if (disper > 0 && disamt == 0)
                                //{
                                //    double bfrdisamt = (aftrdisamt * 100) / (100 - disper);
                                //    rate = bfrdisamt;
                                //}
                                //else if (disper > 0 && disamt == 1)
                                //{
                                //    rate = aftrdisamt;
                                //}
                                //else if (disper == 0 && disamt == 1)
                                //{
                                //    rate = aftrdisamt;
                                //}
                                rate = Math.Round(MRP, 2);
                                double basicprice = rate * qty;
                                basicprice = Math.Round(basicprice, 2);
                                double disa = disamt;
                                double[] dis = new double[2];
                                dis = calculatediscount(pos.Rows[0]["Product_Name"].ToString(), "ALL PARTIES", basicprice);
                                tax = Math.Round((basicprice - dis[1]) * igst / (100), 2);
                                double cess1 = Math.Round((basicprice - dis[1]) * cessper1 / (100), 2);
                                cess1 = cess1 + (cessamt1 * qty);
                                disper = Math.Round(dis[0], 2);
                                disa = Math.Round(dis[1], 2);
                                //if (disper != 0)
                                //{
                                //   disa = (Convert.ToDouble(basicprice) * Convert.ToDouble(disper)) / 100;
                                //}
                                //else
                                //{
                                //    disper = (100 * Convert.ToDouble(disamt)) / Convert.ToDouble(basicprice);
                                //}
                                // disper = Math.Round(disper, 2);

                                double adtax = Math.Round((basicprice - dis[1]) * (addtax) / (100), 2);
                                double atax = Math.Round(adtax, 2);
                                //double aatax = Convert.ToDouble(txtaddtax.Text);
                                //double a = aatax + atax;
                                //txtaddtax.Text = Convert.ToString(a);


                                double amount = basicprice - disa + tax + atax + cess1;
                                amount = Math.Round(amount, 2);


                                // lblroundof.Text = Math.Round((Math.Round(amount, 0) - amount), 2).ToString();


                                basicprice = rate * qty;
                                //double[] dis = new double[2];
                                //dis = calculatediscount(txtbarcode.Text, "ALL PARTIES", basicprice);
                                //      Double cess = Convert.ToDouble(basicprice) * Convert.ToDouble(dt.Rows[0]["cessper"].ToString()) / 100;
                                //      Double cessamt = Convert.ToDouble(dt.Rows[0]["cessamt"].ToString());
                                //      Double tcess = cess + cessamt;

                                double cessval = cess1;

                                lvItem.Items[rowid].SubItems[1].Text = Convert.ToString(qty);
                                lvItem.Items[rowid].SubItems[7].Text = Convert.ToString(basicprice.ToString("N2"));
                                lvItem.Items[rowid].SubItems[3].Text = Convert.ToString(disper);
                                lvItem.Items[rowid].SubItems[8].Text = Convert.ToString(disa);
                                lvItem.Items[rowid].SubItems[4].Text = Convert.ToString(tax.ToString("N2"));
                                lvItem.Items[rowid].SubItems[9].Text = Convert.ToString(atax.ToString("N2"));
                                lvItem.Items[rowid].SubItems[5].Text = Convert.ToString(amount.ToString("N2"));
                                lvItem.Items[rowid].SubItems[10].Text = Convert.ToString(amount.ToString("N2"));
                                lvItem.Items[rowid].SubItems[12].Text = Convert.ToString(cessval.ToString("N2"));
                            }

                        }
                        else
                        {
                            MessageBox.Show("Enter Item Barcode");
                        }
                    }
                    catch (Exception ex)
                    {
                        // MessageBox.Show(ex.Message);

                    }
                    #endregion
                    finaltotal();
                }
                else if (columnindex == 3)
                {
                    string q = editbox.Text;
                    d = lvItem.Items[lvItem.FocusedItem.Index].SubItems[1].Text;
                    a = lvItem.Items[lvItem.FocusedItem.Index].SubItems[0].Text;
                    b = lvItem.Items[lvItem.FocusedItem.Index].SubItems[11].Text;
                    c = lvItem.Items[lvItem.FocusedItem.Index].SubItems[2].Text;
                    options = conn.getdataset("select * from options");
                    Dprice = options.Rows[0]["Defaultprice"].ToString();
                    lblsaletype.Text = options.Rows[0]["autosaletype"].ToString();
                    batchno = b;
                    rowid = lvItem.FocusedItem.Index;


                    #region
                    try
                    {


                        if (!string.IsNullOrEmpty(a))
                        {
                            DataTable productid = new DataTable();
                            productid = conn.getdataset("select DISTINCT p.taxslab,p.ProductID from ProductMaster p inner join ProductPriceMaster pp on p.Productid=pp.Productid where p.Product_Name='" + a + "'or pp.barcode='" + a + "' and p.isactive=1 and pp.isactive=1");

                            pos = conn.getdataset("select p.cessper,p.cessamt,pp.Barcode,p.Product_Name,pp." + Dprice + " as Dprice,pp.BasicPrice,i.igst,i.additax,i.cgst,i.sgst from ProductPriceMaster pp inner join ProductMaster p on p.ProductID=pp.Productid  inner join TaxSlabMaster i on i.Taxslabname=p.taxslab where p.ProductID='" + productid.Rows[0]["ProductID"].ToString() + "' and p.taxslab='" + productid.Rows[0]["taxslab"].ToString() + "'and i.saletypename='" + lblsaletype.Text + "' and p.isactive=1 and pp.isactive=1 and i.isactive=1");

                            if (pos.Rows.Count > 0)
                            {

                                Double qty = Convert.ToDouble(d);
                                Double igst = Convert.ToDouble(pos.Rows[0]["igst"].ToString());
                                Double netamt = Convert.ToDouble(c);
                                Double addtax = Convert.ToDouble(pos.Rows[0]["additax"].ToString());
                                Double dprice = Convert.ToDouble(c);
                                double MRP = Convert.ToDouble(c);
                                double total = MRP;
                                total = Math.Round(total, 2);
                                double cessper1 = Convert.ToDouble(pos.Rows[0]["cessper"].ToString());
                                double cessamt1 = Convert.ToDouble(pos.Rows[0]["cessamt"].ToString());
                                string saletype = conn.ExecuteScalar("select TaxTypename from PurchasetypeMaster where Purchasetypename='" + lblsaletype.Text + "'");
                                double tax = 0;
                                double aftrdisamt = 0;
                                if (!string.IsNullOrEmpty(saletype))
                                {
                                    if (saletype == "Tax Exclusive")
                                    {
                                        tax = ((MRP - cessamt1) * (igst + addtax + cessper1) / (100 + (igst + addtax + cessper1)));
                                        aftrdisamt = MRP;
                                    }
                                    else if (saletype == "Tax Free")
                                    {
                                        tax = 0;
                                        aftrdisamt = MRP - cessamt1 - tax;
                                    }
                                    else
                                    {
                                        tax = ((MRP - cessamt1) * (igst + addtax + cessper1) / (100 + (igst + addtax + cessper1)));
                                        aftrdisamt = MRP - cessamt1 - tax;
                                    }
                                }
                                //   double tax = ((MRP - cessamt1) * (igst + addtax + cessper1) / (100 + (igst + addtax + cessper1)));
                                //   double aftrdisamt = MRP - cessamt1 - tax;
                                double rate = aftrdisamt;
                                DataTable discount = new DataTable();
                                discount = conn.getdataset("select Discount,SpecialRate from PartyRates where ItemID='" + productid.Rows[0]["ProductID"].ToString() + "'");
                                if (discount.Rows.Count > 0)
                                {
                                    disper = Convert.ToInt32(discount.Rows[0]["Discount"].ToString());
                                    if (Convert.ToInt32(discount.Rows[0]["SpecialRate"].ToString()) > 0)
                                    {
                                        pos.Rows[0]["Dprice"] = Convert.ToInt32(discount.Rows[0]["SpecialRate"].ToString());
                                        disamt = 1;
                                    }
                                    else
                                    {
                                        disamt = 0;
                                    }
                                    pos.AcceptChanges();
                                }
                                else
                                {
                                    disper = 0;
                                    disamt = 0;
                                }
                                rate = Math.Round(MRP, 2);
                                double basicprice = rate * qty;
                                basicprice = Math.Round(basicprice, 2);
                                double disa = disamt;
                                double[] dis = new double[2];
                                // dis = calculatediscount(pos.Rows[0]["Product_Name"].ToString(), "ALL PARTIES", basicprice);
                                dis[0] = Convert.ToDouble(q);
                                double manualdisamt = Math.Round((basicprice * (Convert.ToDouble(q) / 100)), 2);
                                double cess1 = 0;
                                double adtax = 0;
                                double atax = 0;
                                if (saletype == "Tax Exclusive")
                                {
                                    tax = Math.Round((basicprice - manualdisamt) * igst / (100), 2);
                                    cess1 = Math.Round((basicprice - manualdisamt) * cessper1 / (100), 2);
                                    cess1 = cess1 + (cessamt1 * qty);
                                    disper = Math.Round(dis[0], 2);
                                    disa = Math.Round(manualdisamt, 2);
                                    adtax = Math.Round((basicprice - manualdisamt) * (addtax) / (100), 2);
                                    atax = Math.Round(adtax, 2);
                                }
                                else if (saletype == "Tax Free")
                                {
                                    tax = 0;
                                    cess1 = 0;
                                    cess1 = cess1 + (cessamt1 * qty);
                                    disper = Math.Round(dis[0], 2);
                                    disa = Math.Round(manualdisamt, 2);
                                    adtax = 0;
                                    atax = 0;
                                }
                                else
                                {
                                    tax = Math.Round((basicprice - manualdisamt) * igst / (100), 2);
                                    cess1 = Math.Round((basicprice - manualdisamt) * cessper1 / (100), 2);
                                    cess1 = cess1 + (cessamt1 * qty);
                                    disper = Math.Round(dis[0], 2);
                                    disa = Math.Round(manualdisamt, 2);
                                    adtax = Math.Round((basicprice - manualdisamt) * (addtax) / (100), 2);
                                    atax = Math.Round(adtax, 2);
                                }

                                
                                double amount = basicprice - disa + tax + atax + cess1;
                                amount = Math.Round(amount, 2);
                                basicprice = rate * qty;
                                double cessval = cess1;
                                lvItem.Items[rowid].SubItems[1].Text = Convert.ToString(qty);
                                lvItem.Items[rowid].SubItems[7].Text = Convert.ToString(basicprice.ToString("N2"));
                                lvItem.Items[rowid].SubItems[3].Text = Convert.ToString(disper);
                                lvItem.Items[rowid].SubItems[8].Text = Convert.ToString(disa);
                                lvItem.Items[rowid].SubItems[4].Text = Convert.ToString(tax.ToString("N2"));
                                lvItem.Items[rowid].SubItems[9].Text = Convert.ToString(atax.ToString("N2"));
                                lvItem.Items[rowid].SubItems[5].Text = Convert.ToString(amount.ToString("N2"));
                                lvItem.Items[rowid].SubItems[10].Text = Convert.ToString(amount.ToString("N2"));
                                lvItem.Items[rowid].SubItems[12].Text = Convert.ToString(cessval.ToString("N2"));
                            }

                        }
                        else
                        {
                            MessageBox.Show("Enter Item Barcode");
                        }
                    }
                    catch (Exception ex)
                    {
                        // MessageBox.Show(ex.Message);

                    }
                    #endregion
                    finaltotal();
                }
            }
        }
        int columnindex;
        private void lvItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                hitinfo = lvItem.HitTest(e.X, e.Y);
                editbox.Bounds = hitinfo.SubItem.Bounds;
                editbox.Text = hitinfo.SubItem.Text;
                editbox.Focus();
                editbox.Show();
                columnindex = hitinfo.Item.SubItems.IndexOf(hitinfo.SubItem);

            }
            catch
            {
            }
        }

        private void txtitemname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (options.Rows[0]["requiragentnameinpos"].ToString() == "Ask For Agent After Item Entry")
                    {
                        pnlagent.Visible = true;
                        cmbagentname.Focus();
                    }
                    else if (options.Rows[0]["requiragentnameinpos"].ToString() == "Ask For Agent Bill Wise")
                    {
                        if (flagforagent == 0)
                        {
                            pnlagent.Visible = true;
                            cmbagentname.Focus();
                            flagforagent = 1;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(txtitemname.Text))
                            {
                                enteritem();
                            }
                            else
                            {
                                MessageBox.Show("Enter Item");
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(txtitemname.Text))
                        {
                            enteritem();
                        }
                        else
                        {
                            MessageBox.Show("Enter Item");
                        }
                    }
                    txtitemname.Text = "";
                }
            }
            catch
            {
            }
        }
        public static string activecontroal = "";
        private void btnpayclear_Click(object sender, EventArgs e)
        {
            //if (activecontroal == "txtpaymenttotalamt")
            //{
            //    txtpaymenttotalamt.Text = "";
            //    txtpaymenttotalamt.Focus();
            //}
            //else if (activecontroal == "txtpaidamt")
            //{
            //    txtpaidamt.Text = "";
            //    txtpaidamt.Focus();
            //}
            //else if (activecontroal == "txtbalance")
            //{
            //    txtbalance.Text = "";
            //    txtbalance.Focus();
            //}
            //else if (activecontroal == "txtchange")
            //{
            //    txtchange.Text = "";
            //    txtchange.Focus();
            //}
            //else
            //{
            //    txtpay.Text = "";
            //    txtpay.Focus();
            //}
        }

        private void btnpayback_Click(object sender, EventArgs e)
        {
            //if (activecontroal == "txtpaymenttotalamt")
            //{
            //    if (txtpaymenttotalamt.Text.Length > 0)
            //    {
            //        txtpaymenttotalamt.Text = txtpaymenttotalamt.Text.Remove(txtpaymenttotalamt.Text.Length - 1);
            //    }
            //    else
            //    {
            //        txtpaymenttotalamt.Focus();
            //    }
            //}
            //else if (activecontroal == "txtpaidamt")
            //{
            //    if (txtpaidamt.Text.Length > 0)
            //    {
            //        txtpaidamt.Text = txtpaidamt.Text.Remove(txtpaidamt.Text.Length - 1);
            //    }
            //    else
            //    {
            //        txtpaidamt.Focus();
            //    }
            //}
            //else if (activecontroal == "txtbalance")
            //{
            //    if (txtbalance.Text.Length > 0)
            //    {
            //        txtbalance.Text = txtbalance.Text.Remove(txtbalance.Text.Length - 1);
            //    }
            //    else
            //    {
            //        txtbalance.Focus();
            //    }
            //}
            //else if (activecontroal == "txtchange")
            //{
            //    if (txtchange.Text.Length > 0)
            //    {
            //        txtchange.Text = txtchange.Text.Remove(txtchange.Text.Length - 1);
            //    }
            //    else
            //    {
            //        txtchange.Focus();
            //    }
            //}
            //else
            //{
            //    if (txtpay.Text.Length > 0)
            //    {
            //        txtpay.Text = txtpay.Text.Remove(txtpay.Text.Length - 1);
            //    }
            //    else
            //    {
            //        txtpay.Focus();
            //    }
            //}
        }

        private void txtpaymenttotalamt_Enter(object sender, EventArgs e)
        {
            activecontroal = "txtpaymenttotalamt";
        }

        private void txtpaidamt_Enter(object sender, EventArgs e)
        {
            activecontroal = "txtpaidamt";
        }

        private void txtbalance_Enter(object sender, EventArgs e)
        {
            activecontroal = "txtbalance";
        }

        private void txtchange_Enter(object sender, EventArgs e)
        {
            activecontroal = "txtchange";
        }

        private void txtpay_Enter(object sender, EventArgs e)
        {
            activecontroal = "txtpay";
        }
        private void btnCashforpayment_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (!string.IsNullOrEmpty(txtpaidamt.Text))
            //    {
            //        string srno = "";
            //        if (lvpayment.Items.Count == 0)
            //        {
            //            srno = "1";
            //        }
            //        else
            //        {
            //            srno = Convert.ToString(lvpayment.Items.Count);
            //            Double a = Convert.ToDouble(srno) + 1;
            //            srno = Convert.ToString(a);
            //        }
            //        li = lvpayment.Items.Add(srno);
            //        li.SubItems.Add(btnCashforpayment.Text);
            //        li.SubItems.Add(txtpaidamt.Text);
            //        txtpaidamt.Text = "";
            //        txtbalance.Text = "";
            //        txtchange.Text = "";
            //        txtpaidamt.Focus();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Enter Paid Amt");
            //        txtpaidamt.Focus();
            //    }
            //}
            //catch
            //{
            //}
        }

        private void btnCard_Click(object sender, EventArgs e)
        {
            try
            {
                creditordebitcardinpos c = new creditordebitcardinpos(this, txtptotal.Text, updatebillno);
                c.ShowDialog();
                //    if (!string.IsNullOrEmpty(txtpaidamt.Text))
                //    {
                //        string srno = "";
                //        if (lvpayment.Items.Count == 0)
                //        {
                //            srno = "1";
                //        }
                //        else
                //        {
                //            srno = Convert.ToString(lvpayment.Items.Count);
                //            Double a = Convert.ToDouble(srno) + 1;
                //            srno = Convert.ToString(a);
                //        }
                //        li = lvpayment.Items.Add(srno);
                //        li.SubItems.Add(btnCard.Text);
                //        li.SubItems.Add(txtpaidamt.Text);
                //        txtpaidamt.Text = "";
                //        txtbalance.Text = "";
                //        txtchange.Text = "";
                //        txtpaidamt.Focus();
                //    }
                //    else
                //    {
                //        MessageBox.Show("Enter Paid Amt");
                //        txtpaidamt.Focus();
                //    }
            }
            catch
            {
            }
        }

        private void btncheque_Click(object sender, EventArgs e)
        {
            try
            {
                chequedetails c = new chequedetails(this, updatebillno);
                c.ShowDialog();
                //    if (!string.IsNullOrEmpty(txtpaidamt.Text))
                //    {
                //        string srno = "";
                //        if (lvpayment.Items.Count == 0)
                //        {
                //            srno = "1";
                //        }
                //        else
                //        {
                //            srno = Convert.ToString(lvpayment.Items.Count);
                //            Double a = Convert.ToDouble(srno) + 1;
                //            srno = Convert.ToString(a);
                //        }
                //        li = lvpayment.Items.Add(srno);
                //        li.SubItems.Add(btncheque.Text);
                //        li.SubItems.Add(txtpaidamt.Text);
                //        txtpaidamt.Text = "";
                //        txtbalance.Text = "";
                //        txtchange.Text = "";
                //        txtpaidamt.Focus();
                //    }
                //    else
                //    {
                //        MessageBox.Show("Enter Paid Amt");
                //        txtpaidamt.Focus();
                //    }
            }
            catch
            {
            }
        }

        private void btnclr_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcash")
            {
                txtcash.Text = "";
                //  this.ActiveControl = txtcash;
            }
            else
            {
                txtcash.Focus();
            }
        }

        private void btnbkp_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcash")
            {
                if (txtcash.Text.Length > 0)
                {
                    txtcash.Text = txtcash.Text.Remove(txtcash.Text.Length - 1);
                    // this.ActiveControl = txtcash;
                }
                else
                {
                    txtcash.Focus();
                }
            }
        }

        private void txtcash_Enter(object sender, EventArgs e)
        {
            activecontroal = "txtcash";
        }

        private void btntotalachual_Click(object sender, EventArgs e)
        {
            txtcash.Text = "";
            txtcash.Text += btntotalachual.Text;
        }

        private void btndot_Click(object sender, EventArgs e)
        {
            txtcash.Text += btndot.Text;
        }

        private void btndoublezero_Click(object sender, EventArgs e)
        {
            txtcash.Text += btndoublezero.Text;
        }

        private void btnzero_Click(object sender, EventArgs e)
        {
            txtcash.Text += btnzero.Text;
        }

        private void btntotalroundoff_Click(object sender, EventArgs e)
        {
            txtcash.Text = "";
            txtcash.Text += btntotalroundoff.Text;
        }

        private void btnthree_Click(object sender, EventArgs e)
        {
            txtcash.Text += btnthree.Text;
        }

        private void btntwo_Click(object sender, EventArgs e)
        {
            txtcash.Text += btntwo.Text;
        }

        private void btnone_Click(object sender, EventArgs e)
        {
            txtcash.Text += btnone.Text;
        }

        private void btnsix_Click(object sender, EventArgs e)
        {
            txtcash.Text += btnsix.Text;
        }

        private void btnfive_Click(object sender, EventArgs e)
        {
            txtcash.Text += btnfive.Text;
        }

        private void btnfour_Click(object sender, EventArgs e)
        {
            txtcash.Text += btnfour.Text;
        }

        private void btnnine_Click(object sender, EventArgs e)
        {
            txtcash.Text += btnnine.Text;
        }

        private void btneight_Click(object sender, EventArgs e)
        {
            txtcash.Text += btneight.Text;
        }

        private void btnseven_Click(object sender, EventArgs e)
        {
            txtcash.Text += btnseven.Text;
        }



        private void btnd1_Click(object sender, EventArgs e)
        {
            txtcash.Text = "";
            var str = btnd1.Text;
            str = str.Substring(2);
            txtcash.Text += str;
        }

        private void btnd2_Click(object sender, EventArgs e)
        {
            txtcash.Text = "";
            var str = btnd2.Text;
            str = str.Substring(2);
            txtcash.Text += str;
        }

        private void btnd3_Click(object sender, EventArgs e)
        {
            txtcash.Text = "";
            var str = btnd3.Text;
            str = str.Substring(2);
            txtcash.Text += str;
        }

        private void btnd4_Click(object sender, EventArgs e)
        {
            txtcash.Text = "";
            var str = btnd4.Text;
            str = str.Substring(2);
            txtcash.Text += str;
        }
        public static string strbillno = "";
        DataTable discount = new DataTable();
        Double t4, t5, t6, t7, t8, t9, t10;
        Double t12, t14, t15, t16, t18, n, o, p, q, g5, g3, g4, g6, g7, g8, g9, tax, cess, g12;
        string gdis1, total1, tax12;
        string g1, g2, g10;
        string agentid = "";
        string maxbillno;
        string billnowithprifix;
        public static string customername1 = "";
        public static string customercity1 = "";
        public static string customermobile1 = "";
        public static string tbankname = "";
        public static string tcardnum = "";
        public static string tcardtype = "";
        public static string texpdate = "";
        public static string tappcode = "";
        public static string tref = "";
        public static string tinv = "";
        public static string tcholder = "";
        public static string tamount = "";
        public static string chequecusname = "";
        public static string chequebankname = "";
        public static string chequeno = "";
        public void cusdetails()
        {
            customername1 = AddNewCustomerInPOS.customername;
            customercity1 = AddNewCustomerInPOS.customercity;
            customermobile1 = AddNewCustomerInPOS.customermobile;
            lblcusname.Text = AddNewCustomerInPOS.customername;
            lblcuscity.Text = AddNewCustomerInPOS.customercity;
            lblcusmobile.Text = AddNewCustomerInPOS.customermobile;
		 lblcusid.Text = AddNewCustomerInPOS.customerid;
        }
        public void carddetails()
        {
            tbankname = creditordebitcardinpos.tbankname;
            tcardnum = creditordebitcardinpos.tcardnum;
            tcardtype = creditordebitcardinpos.tcardtype;
            texpdate = creditordebitcardinpos.texpdate;
            tappcode = creditordebitcardinpos.tappcode;
            tref = creditordebitcardinpos.tref;
            tinv = creditordebitcardinpos.tinv;
            tcholder = creditordebitcardinpos.tcholder;
            tamount = creditordebitcardinpos.tamount;
        }
        public void chequedetail()
        {
            chequecusname = chequedetails.customername;
            chequebankname = chequedetails.customerbank;
            chequeno = chequedetails.customercheque;
        }
        void getsr()
        {
            try
            {
                string saletype = conn.ExecuteScalar("select Purchasetypeid from PurchasetypeMaster where isactive=1 and Purchasetypename='" + lblsaletype.Text + "'");
                String str = conn.ExecuteScalar("select max(BillId) from BillPOSMaster where isactive='1' and saletypeid='" + saletype + "'");
                DataTable dt = conn.getdataset("select * from [PurchasetypeMaster] where isactive=1 and Purchasetypename='" + lblsaletype.Text + "'");
                Int64 id, count;
                //     Object data = dr[1];

                if (str == "")
                {

                    id = Convert.ToInt64(dt.Rows[0]["startingno"].ToString());
                    count = Convert.ToInt64(dt.Rows[0]["startingno"].ToString());
                }
                else
                {
                    id = Convert.ToInt32(str) + 1;
                    count = Convert.ToInt32(str) + 1;
                }
                // lblbill_no.Text = count.ToString();
                billnowithprifix = dt.Rows[0]["prefix"].ToString() + count.ToString();

            }
            catch
            {
            }
            finally
            {

            }

        }
        public void bindbillno()
        {
            try
            {
                if (options.Rows[0]["posbillno"].ToString() == "Continuous")
                {
                    String str = conn.ExecuteScalar("select max(BillId) from BillPOSMaster where isactive='1'");
                    DataTable dt = conn.getdataset("select * from [PurchasetypeMaster] where isactive=1 and Purchasetypename='" + lblsaletype.Text + "'");
                    Int64 id, count;
                    if (str == "")
                    {

                        id = Convert.ToInt64(1);
                        count = Convert.ToInt64(1);
                    }
                    else
                    {
                        id = Convert.ToInt32(str) + 1;
                        count = Convert.ToInt32(str) + 1;
                    }
                    // lblbill_no.Text = count.ToString();
                    billnowithprifix = dt.Rows[0]["prefix"].ToString() + count.ToString();
                }
                else
                {
                    getsr();
                }

            }
            catch
            {
            }
        }
        public void billno()
        {
            maxbillno = conn.getsinglevalue("select max (BillId)+1 from BillPOSMaster where isactive=1");
            if (maxbillno == "")
            {
                maxbillno = "1";
            }

            strbillno = maxbillno.ToString();
        }
        public static string statusreg1 = string.Empty;
        public static string Decrypstatus1(string cipherText)
        {
            string EncryptionKey = "00";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    statusreg1 = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        OleDbSettings ods = new OleDbSettings();
        public void bindstatus()
        {
            DataSet ds = ods.getdata("Select * from tblreg");
            string reg = Convert.ToString(ds.Tables[0].Rows[0]["d16"].ToString());
            Decrypstatus1(reg);
            if (statusreg1 == "Edu")
            {
                string sale = conn.ExecuteScalar("select count(id) from BillPOSMaster where isactive=1");
                if (sale == "5")
                {
                    MessageBox.Show("You Are Not Authorized to Add More Then 5 Bill");
                    return;
                }
            }
        }
        public static string statusreg = string.Empty;
        public static string Decrypstatus(string cipherText)
        {
            string EncryptionKey = "00";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    statusreg = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        public void savedata()
        {
            try
            {
                if (!string.IsNullOrEmpty(updatebillno))
                {
                    DataSet ds = ods.getdata("Select * from tblreg");
                    string reg = Convert.ToString(ds.Tables[0].Rows[0]["d16"].ToString());
                    Decrypstatus(reg);
                    if (statusreg == "Edu")
                    {
                        string pos = conn.ExecuteScalar("select count(id) from BillPOSMaster where isactive=1");
                        if (pos == "5")
                        {
                            MessageBox.Show("You Are Not Authorized to Add More Then 5 Bill");
                            return;
                        }
                    }
                }
                //  bindstatus();
                if (!string.IsNullOrEmpty(updatebillno))
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[9]["u"].ToString() == "True")
                        {
                            // bindbillno();
                            billno();
                            conn.execute("Update BillPOSProductMaster set isactive=0 where billno='" + updatebillno + "'");
conn.execute("Update serials set isactive=0 where billno='" + updatebillno + "'");																																										
                            string totaltax1 = string.Empty;
                            int count = lvItem.Items.Count;
							 DataTable getstate = conn.getdataset("select * from Company where isactive=1 and CompanyID='" + Master.companyId + "'");
                            DataTable saletypeid = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + lblsaletype.Text + "'");
                           foreach (ListViewItem row in lvItem.Items)
                            {

                                g1 = row.SubItems[6].Text.ToString();// barcode code
                                g2 = row.SubItems[0].Text.ToString(); // itamname
                                g3 = Convert.ToDouble(row.SubItems[1].Text.ToString()); // qty
                                g4 = Convert.ToDouble(row.SubItems[2].Text.ToString()); // rate
                                g5 = Convert.ToDouble(row.SubItems[7].Text.ToString()); // basicamt
                                g6 = Convert.ToDouble(row.SubItems[3].Text.ToString()); // disper
                                g7 = Convert.ToDouble(row.SubItems[8].Text.ToString()); // disamt
                                g8 = Convert.ToDouble(row.SubItems[4].Text.ToString()); // totaltax
                                g9 = Convert.ToDouble(row.SubItems[5].Text.ToString()); // totalamt
                                g10 = row.SubItems[11].Text.ToString(); // Batch
                                g12 = Convert.ToDouble(row.SubItems[12].Text.ToString()); // cess
                                agentid = row.SubItems[13].Text.ToString(); // agentid
																					 
                                double pric = g5;
                                double aTruncated = Math.Truncate(pric * 100) / 100;
                                string pric1 = string.Format("{0:0.00}", aTruncated);

                                double dec = g6;
                                double aTruncated1 = Math.Truncate(dec * 100) / 100;
                                string dec1 = string.Format("{0:0.00}", aTruncated1);

                                double total = g7;
                                double aTruncated2 = Math.Truncate(total * 100) / 100;
                                string total1 = string.Format("{0:0.00}", aTruncated2);
                                double totaltax = g8;
                                double aTruncated3 = Math.Truncate(totaltax * 100) / 100;
                                totaltax1 = string.Format("{0:0.00}", aTruncated3);

                                DataTable gettax = conn.getdataset("select * from TaxSlabMaster where isactive=1 and saletypename='" + lblsaletype.Text + "' and Taxslabname=(select taxslab from productmaster where isactive=1 and product_name = '" + g2 + "')");
                                double sgst1 = g5 - g7;
                                double sgst = sgst1 * (Convert.ToDouble(gettax.Rows[0]["sgst"].ToString()) / 100);
                                sgst = Math.Round(sgst, 2);
                                double cgst1 = g5 - g7;
                                double cgst = cgst1 * (Convert.ToDouble(gettax.Rows[0]["cgst"].ToString()) / 100);
                                cgst = Math.Round(cgst, 2);
                                double addtaxamt;
                                if (gettax.Rows[0]["onwhich"].ToString() == "Tax Amt")
                                {
                                    double addt = Convert.ToDouble(gettax.Rows[0]["Additax"].ToString());
                                    addtaxamt = (g5 * addt) / 100;
                                }
                                else
                                {
                                    double addt = Convert.ToDouble(gettax.Rows[0]["Additax"].ToString());
                                    addtaxamt = (g9 * addt) / 100;
                                }
                                double to = g9;
                                double aTruncated31 = Math.Truncate(to * 100) / 100;
                                string tot = string.Format("{0:0.00}", aTruncated31);
                                double t = Convert.ToDouble(to);
                                if (Convert.ToBoolean(options.Rows[0]["autoroundoffpos"].ToString()) == true)
                                {
                                    lblroundof.Text = Math.Round((Math.Round(t, 0) - t), 2).ToString();
                                    double g = Convert.ToDouble(lblroundof.Text);
                                    if (lblroundof.Text != "0")
                                    {
                                        lblroundof.Visible = true;
                                    }
                                    else
                                    {
                                        lblroundof.Visible = false;
                                    }
                                }
								 
                                {
                                    agentid = "0";
                                } 
																		   
								 
																							  
                                string itemid = conn.ExecuteScalar("select ProductID from ProductMaster where isactive=1 and Product_Name='" + g2 + "'");
                                conn.execute("INSERT INTO [dbo].[BillPOSProductMaster]([BillId],[BillRunDate],[ItemName],[Qty],[Rate],[Amount],[Total],[igst],[Addtax],[Discount],[sgst],[cgst],[RoundOf],[DiscountAmt],[Addtaxamt],[Batchno],[cess],[billno],[agentid],[itemid],[isactive],[serialno])VALUES('" + billid + "','" + Convert.ToDateTime(lbldate.Text).ToString(Master.dateformate) + "','" + g2 + "','" + g3 + "','" + g4 + "','" + g5 + "','" + g9 + "','" + gettax.Rows[0]["igst"].ToString() + "','" + gettax.Rows[0]["Additax"].ToString() + "','" + g6 + "','" + sgst + "','" + cgst + "','" + lblroundof.Text + "','" + g7 + "','" + addtaxamt + "','" + g10 + "','" + g12 + "','" + updatebillno + "','" + agentid + "','" + itemid + "','1','" + row.SubItems[14].Text.ToString() + "')");
                                //sql.execute("INSERT INTO [dbo].[itemdetails]([billno],[barcode],[itemname],[qty],[unitprice],[price],[disamt],[totalamt],[gqty],[gprice],[gdisamt],[gtotal],[itamcount]) VALUES('" + maxbillno + "','" + g1 + "','" + g2 + "','" + g3 + "','" + g4 + "','" + pric1 + "','" + dec1 + "','" + total1 + "','" + txtqty.Text + "','" + txtstotal.Text + "','" + txtdisamt.Text + "','" + txtgtotal.Text + "','" + count + "')");
								if (row.SubItems[14].Text.ToString() != "")
                                {
                                    string[] sr = row.SubItems[14].Text.ToString().Split(',');
                                    for (int i = 0; i < sr.Length; i++)
                                    {
                                        if (sr[i] != "")
                                        {
                                            Guid guid;
                                            guid = Guid.NewGuid();
                                            conn.execute("INSERT INTO [dbo].[Serials]([Date],[VoucherID],[TransactionID],[SerialNo],[TransactionType],[VchNo],[PurchaseTypeID],[PurchaseTypeName],[SaleTypeID],[SaleTypeName],[PartyID],[PartyName],[ItemCompanyID],[SyncID],[SyncDatetime],[isactive],[ItemName],[ItemID])VALUES('" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "','" + updatebillno + "','POS','" + sr[i] + "','POS','','" + saletypeid.Rows[0]["Purchasetypeid"].ToString() + "','" + lblsaletype.Text + "','" + saletypeid.Rows[0]["Purchasetypeid"].ToString() + "','" + lblsaletype.Text + "','" + lblcusid.Text + "','" + lblcusname.Text + "','" + Master.companyId + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',1,'" + g2 + "','" + itemid + "')");
                                        }
                                    }
                                }									   
                            }
                            finaltotalcoumn();
                            if (string.IsNullOrEmpty(lblroundof.Text))
                            {
                                lblroundof.Text = "0";
                            }
                            if (string.IsNullOrEmpty(txttotaldisamt.Text))
                            {
                                txttotaldisamt.Text = "0";
                            }
                            if (!string.IsNullOrEmpty(creditordebitcardinpos.paymenttype))
                            {
                                lblpaymenttype.Text = "Credit/Debit Card";
                            }
                            else if (!string.IsNullOrEmpty(chequedetails.paymenttype))
                            {
                                lblpaymenttype.Text = "Cheque";
                            }
                            else
                            {
                                lblpaymenttype.Text = "Cash";
                            }
                            if (string.IsNullOrEmpty(txtldis.Text))
                            {
                                txtldis.Text = "0";
                            }
                            //conn.execute("INSERT INTO [dbo].[BillPOSMaster]([BillId],[BillDate],[Terms],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[disamt],[adddisamt],[totalcess],[statecode],[companystate],[billno],[saletypeid],[agentid],[reciveamt],[returnamount],[totaltoundoff],[cashtendered],[change],[customername],[customercity],[customermobile],[bankname],[cardnumbar],[cardtype],[expirydate],[apprcode],[refno],[amountrs],[invno],[cardholdername],[customerchequename],[customerchequebankname],[customercheckno],[isactive])VALUES('" + maxbillno + "','" + Convert.ToDateTime(lbldate.Text).ToString(Master.dateformate) + "','" + lblpaymenttype.Text + "','" + count + "','" + lblqty.Text + "','" + txtstotal + "','" + txttotaltax + "','" + txtgtotal + "','" + txtdisamt + "','" + txttotaldisamt.Text + "','" + txtcess + "','" + getstate.Rows[0]["Statecode"].ToString() + "','" + getstate.Rows[0]["State"].ToString() + "','" + billnowithprifix + "','" + saletypeid.Rows[0]["Purchasetypeid"].ToString() + "','" + cmbagentname.SelectedValue + "','" + txtcash.Text + "','" + txtreturnamount.Text + "','" + lblroundof.Text + "','" + txtcash.Text + "','" + txtreturnamount.Text + "','" + lblcusname.Text + "','" + lblcuscity.Text + "','" + lblcusmobile.Text + "','" + tbankname + "','" + tcardnum + "','" + tcardtype + "','" + texpdate + "','" + tappcode + "','" + tref + "','" + tamount + "','" + tinv + "','" + tcholder + "','" + chequecusname + "','" + chequebankname + "','" + chequeno + "','1')");
                            conn.execute("Update BillPOSMaster set BillDate='" + Convert.ToDateTime(lbldate.Text).ToString(Master.dateformate) + "',Terms='" + lblpaymenttype.Text + "',count='" + count + "',totalqty='" + lblqty.Text + "',totalbasic='" + txtstotal + "',totaltax='" + txttotaltax + "',totalnet='" + txtgtotal + "',disamt='" + txtdisamt + "',adddisamt='" + txttotaldisamt.Text + "',totalcess='" + txtcess + "',statecode='" + getstate.Rows[0]["Statecode"].ToString() + "',companystate='" + getstate.Rows[0]["State"].ToString() + "',billno='" + updatebillno + "',saletypeid='" + saletypeid.Rows[0]["Purchasetypeid"].ToString() + "',agentid='" + cmbagentname.SelectedValue + "',reciveamt='" + txtcash.Text + "',returnamount='" + txtreturnamount.Text + "',totaltoundoff='" + lblroundof.Text + "',cashtendered='" + txtcash.Text + "',change='" + txtreturnamount.Text + "',customername='" + lblcusname.Text + "',customercity='" + lblcuscity.Text + "',customermobile='" + lblcusmobile.Text + "',bankname='" + tbankname + "',cardnumbar='" + tcardnum + "',cardtype='" + tcardtype + "',expirydate='" + texpdate + "',apprcode='" + tappcode + "',refno='" + tref + "',amountrs='" + tamount + "',invno='" + tinv + "',cardholdername='" + tcholder + "',customerchequename='" + chequecusname + "',customerchequebankname='" + chequebankname + "',customercheckno='" + chequeno + "',adddisper='" + txtldis.Text + "' where billno='" + updatebillno + "'");
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
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[9]["a"].ToString() == "True")
                        {
                            cusdetails();
                            carddetails();
                            chequedetail();
                            bindbillno();
                            billno();
                            string totaltax1 = string.Empty;
                            int count = lvItem.Items.Count;
							 DataTable getstate = conn.getdataset("select * from Company where isactive=1 and CompanyID='" + Master.companyId + "'");
                            DataTable saletypeid = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + lblsaletype.Text + "'");
                            foreach (ListViewItem row in lvItem.Items)
                            {

                                g1 = row.SubItems[6].Text.ToString();// barcode code
                                g2 = row.SubItems[0].Text.ToString(); // itamname
                                g3 = Convert.ToDouble(row.SubItems[1].Text.ToString()); // qty
                                g4 = Convert.ToDouble(row.SubItems[2].Text.ToString()); // rate
                                g5 = Convert.ToDouble(row.SubItems[7].Text.ToString()); // basicamt
                                g6 = Convert.ToDouble(row.SubItems[3].Text.ToString()); // disper
                                g7 = Convert.ToDouble(row.SubItems[8].Text.ToString()); // disamt
                                g8 = Convert.ToDouble(row.SubItems[4].Text.ToString()); // totaltax
                                g9 = Convert.ToDouble(row.SubItems[5].Text.ToString()); // totalamt
                                g10 = row.SubItems[11].Text.ToString(); // Batch
                                g12 = Convert.ToDouble(row.SubItems[12].Text.ToString()); // cess
                                agentid = row.SubItems[13].Text.ToString(); // agentid
                                double pric = g5;
                                double aTruncated = Math.Truncate(pric * 100) / 100;
                                string pric1 = string.Format("{0:0.00}", aTruncated);

                                double dec = g6;
                                double aTruncated1 = Math.Truncate(dec * 100) / 100;
                                string dec1 = string.Format("{0:0.00}", aTruncated1);

                                double total = g7;
                                double aTruncated2 = Math.Truncate(total * 100) / 100;
                                string total1 = string.Format("{0:0.00}", aTruncated2);
                                double totaltax = g8;
                                double aTruncated3 = Math.Truncate(totaltax * 100) / 100;
                                totaltax1 = string.Format("{0:0.00}", aTruncated3);

                                DataTable gettax = conn.getdataset("select * from TaxSlabMaster where isactive=1 and saletypename='" + lblsaletype.Text + "' and Taxslabname=(select taxslab from productmaster where isactive=1 and product_name = '" + g2 + "')");
                                double sgst1 = g5 - g7;
                                double sgst = sgst1 * (Convert.ToDouble(gettax.Rows[0]["sgst"].ToString()) / 100);
                                sgst = Math.Round(sgst, 2);
                                double cgst1 = g5 - g7;
                                double cgst = cgst1 * (Convert.ToDouble(gettax.Rows[0]["cgst"].ToString()) / 100);
                                cgst = Math.Round(cgst, 2);
                                double addtaxamt;
                                if (gettax.Rows[0]["onwhich"].ToString() == "Tax Amt")
                                {
                                    double addt = Convert.ToDouble(gettax.Rows[0]["Additax"].ToString());
                                    addtaxamt = (g5 * addt) / 100;
                                }
                                else
                                {
                                    double addt = Convert.ToDouble(gettax.Rows[0]["Additax"].ToString());
                                    addtaxamt = (g9 * addt) / 100;
                                }
                                double to = g9;
                                double aTruncated31 = Math.Truncate(to * 100) / 100;
                                string tot = string.Format("{0:0.00}", aTruncated31);
                                double t = Convert.ToDouble(to);
                                if (Convert.ToBoolean(options.Rows[0]["autoroundoffpos"].ToString()) == true)
                                {
                                    lblroundof.Text = Math.Round((Math.Round(t, 0) - t), 2).ToString();
                                    double g = Convert.ToDouble(lblroundof.Text);
                                    if (lblroundof.Text != "0")
                                    {
                                        lblroundof.Visible = true;
                                    }
                                    else
                                    {
                                        lblroundof.Visible = false;
                                    }
                                }
								 
                                {
                                    agentid = "0";
                                }		 
																		   
								 
																							  
                                string itemid = conn.ExecuteScalar("select ProductID from ProductMaster where isactive=1 and Product_Name='" + g2 + "'");
                                conn.execute("INSERT INTO [dbo].[BillPOSProductMaster]([BillId],[BillRunDate],[ItemName],[Qty],[Rate],[Amount],[Total],[igst],[Addtax],[Discount],[sgst],[cgst],[RoundOf],[DiscountAmt],[Addtaxamt],[Batchno],[cess],[billno],[agentid],[itemid],[isactive],[serialno])VALUES('" + maxbillno + "','" + Convert.ToDateTime(lbldate.Text).ToString(Master.dateformate) + "','" + g2 + "','" + g3 + "','" + g4 + "','" + g5 + "','" + g9 + "','" + gettax.Rows[0]["igst"].ToString() + "','" + gettax.Rows[0]["Additax"].ToString() + "','" + g6 + "','" + sgst + "','" + cgst + "','" + lblroundof.Text + "','" + g7 + "','" + addtaxamt + "','" + g10 + "','" + g12 + "','" + billnowithprifix + "','" + agentid + "','" + itemid + "','1','" + row.SubItems[14].Text.ToString() + "')");
                                //sql.execute("INSERT INTO [dbo].[itemdetails]([billno],[barcode],[itemname],[qty],[unitprice],[price],[disamt],[totalamt],[gqty],[gprice],[gdisamt],[gtotal],[itamcount]) VALUES('" + maxbillno + "','" + g1 + "','" + g2 + "','" + g3 + "','" + g4 + "','" + pric1 + "','" + dec1 + "','" + total1 + "','" + txtqty.Text + "','" + txtstotal.Text + "','" + txtdisamt.Text + "','" + txtgtotal.Text + "','" + count + "')");
								if (row.SubItems[14].Text.ToString() != "")
                                {
                                    string[] sr = row.SubItems[14].Text.ToString().Split(',');
                                    for (int i = 0; i < sr.Length; i++)
                                    {
                                        if (sr[i] != "")
                                        {
                                            Guid guid;
                                            guid = Guid.NewGuid();
                                            conn.execute("INSERT INTO [dbo].[Serials]([Date],[VoucherID],[TransactionID],[SerialNo],[TransactionType],[VchNo],[PurchaseTypeID],[PurchaseTypeName],[SaleTypeID],[SaleTypeName],[PartyID],[PartyName],[ItemCompanyID],[SyncID],[SyncDatetime],[isactive],[ItemName],[ItemID])VALUES('"
                                                + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "','" + updatebillno + "','POS','" + sr[i] + "','POS','','" + saletypeid.Rows[0]["Purchasetypeid"].ToString() + "','" + lblsaletype.Text + "','" + saletypeid.Rows[0]["Purchasetypeid"].ToString() + "','" + lblsaletype.Text + "','" + lblcusid.Text + "','" + lblcusname.Text + "','" + Master.companyId + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',1,'" + g2 + "','" + itemid + "')");
                                        }
                                    }
                                }
                            }
                             finaltotalcoumn();
                            if (string.IsNullOrEmpty(lblroundof.Text))
                            {
                                lblroundof.Text = "0";
                            }
                            if (string.IsNullOrEmpty(txttotaldisamt.Text))
                            {
                                txttotaldisamt.Text = "0";
                            }
                            if (!string.IsNullOrEmpty(creditordebitcardinpos.paymenttype))
                            {
                                lblpaymenttype.Text = "Credit/Debit Card";
                            }
                            else if (!string.IsNullOrEmpty(chequedetails.paymenttype))
                            {
                                lblpaymenttype.Text = "Cheque";
                            }
                            else
                            {
                                lblpaymenttype.Text = "Cash";
                            }
                            if (string.IsNullOrEmpty(txtldis.Text))
                            {
                                txtldis.Text = "0";
                            }
                            conn.execute("INSERT INTO [dbo].[BillPOSMaster]([BillId],[BillDate],[Terms],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[disamt],[adddisamt],[totalcess],[statecode],[companystate],[billno],[saletypeid],[agentid],[reciveamt],[returnamount],[totaltoundoff],[cashtendered],[change],[customername],[customercity],[customermobile],[bankname],[cardnumbar],[cardtype],[expirydate],[apprcode],[refno],[amountrs],[invno],[cardholdername],[customerchequename],[customerchequebankname],[customercheckno],[adddisper],[isactive])VALUES('" + maxbillno + "','" + Convert.ToDateTime(lbldate.Text).ToString(Master.dateformate) + "','" + lblpaymenttype.Text + "','" + count + "','" + lblqty.Text + "','" + txtstotal + "','" + txttotaltax + "','" + txtgtotal + "','" + txtdisamt + "','" + txttotaldisamt.Text + "','" + txtcess + "','" + getstate.Rows[0]["Statecode"].ToString() + "','" + getstate.Rows[0]["State"].ToString() + "','" + billnowithprifix + "','" + saletypeid.Rows[0]["Purchasetypeid"].ToString() + "','" + cmbagentname.SelectedValue + "','" + txtcash.Text + "','" + txtreturnamount.Text + "','" + lblroundof.Text + "','" + txtcash.Text + "','" + txtreturnamount.Text + "','" + lblcusname.Text + "','" + lblcuscity.Text + "','" + lblcusmobile.Text + "','" + tbankname + "','" + tcardnum + "','" + tcardtype + "','" + texpdate + "','" + tappcode + "','" + tref + "','" + tamount + "','" + tinv + "','" + tcholder + "','" + chequecusname + "','" + chequebankname + "','" + chequeno + "','" + txtldis.Text + "','1')");
                        }
                        else
                        {
                            MessageBox.Show("You don't have Permission To Submit");
                            return;
                        }
                    }
                }
            }
            catch
            {
            }
        }

        double at;
        string txtstotal, txtdisamt, txtgtotal, txttotaltax, txtaddtax, txtcess, txtadddisamt;
        public void finaltotalcoumn()
        {
            try
            {
                n = 0;
                o = 0;
                p = 0;
                q = 0;
                at = 0;
                tax = 0;
                cess = 0;
                for (int i = 0; i < lvItem.Items.Count; i++)
                {
                    n += Convert.ToDouble(lvItem.Items[i].SubItems[1].Text);
                    o += Convert.ToDouble(lvItem.Items[i].SubItems[7].Text);
                    p += Convert.ToDouble(lvItem.Items[i].SubItems[8].Text);
                    tax += Convert.ToDouble(lvItem.Items[i].SubItems[4].Text);
                    at += Convert.ToDouble(lvItem.Items[i].SubItems[9].Text);
                    q += Convert.ToDouble(lvItem.Items[i].SubItems[5].Text);
                    cess += Convert.ToDouble(lvItem.Items[i].SubItems[12].Text);

                }
                lblqty.Text = n.ToString();
                double sub = o;
                double aTruncated1 = Math.Truncate(sub * 100) / 100;
                string sub1 = aTruncated1.ToString();
                txtstotal = sub1.ToString();
                double gdis = p;
                double aTruncated2 = Math.Truncate(gdis * 100) / 100;
                gdis1 = aTruncated2.ToString();
                txtdisamt = gdis1.ToString();
                double total = q;
                double aTruncated3 = Math.Truncate(total * 100) / 100;
                total1 = aTruncated3.ToString();
                txtgtotal = total1.ToString();
                double tax1 = tax;
                double aTruncated4 = Math.Truncate(tax1 * 100) / 100;
                tax12 = aTruncated4.ToString();
                txttotaltax = tax12.ToString();
                double atax1 = at;
                double aTruncated5 = Math.Truncate(atax1 * 100) / 100;
                string tax13 = aTruncated5.ToString();
                txtaddtax = tax13.ToString();

                double atax12 = cess;
                double aTruncated51 = Math.Truncate(atax12 * 100) / 100;
                string tax131 = aTruncated51.ToString();
                txtcess = tax131.ToString();

                //double billdis = Convert.ToDouble(txtldis.Text);
                //double finaltotal = Convert.ToDouble(txtptotal.Text);
                //double manualdisamt = Math.Round((finaltotal * (Convert.ToDouble(billdis) / 100)), 2);
                //txtadddisamt = Convert.ToString(manualdisamt);
                //double abc = Convert.ToDouble(txtgtotal) - Convert.ToDouble(txtadddisamt);
                //txtgtotal = Convert.ToString(abc);
																 
				if (txttotaldisamt.Text == "")
                {
                    txttotaldisamt.Text = "0";
                }	 
				 
														   
                double a = Convert.ToDouble(txttotaldisamt.Text);
                double b = Convert.ToDouble(txtgtotal);
                double c = b - a;
                txtgtotal = Convert.ToString(c);
                if (Convert.ToBoolean(options.Rows[0]["autoroundoffpos"].ToString()) == true)
                {
                    double t = Convert.ToDouble(txtgtotal);


                    lblroundof.Text = Math.Round((Math.Round(t, 0) - t), 2).ToString();
                    double g = Convert.ToDouble(lblroundof.Text);
                    double ga = Convert.ToDouble(t);
                    if (lblroundof.Text != "0")
                    {
                        lblroundof.Visible = true;
                        double gaa = ga + g;
                        txtgtotal = Math.Round(gaa, 2).ToString(".00");
                    }
                    else
                    {
                        lblroundof.Visible = false;
                    }
                }
            }
            catch
            {
            }
        }
        public void clearall()
        {
            creditordebitcardinpos.tbankname = "";
            creditordebitcardinpos.tcardnum = "";
            creditordebitcardinpos.tcardtype = "";
            creditordebitcardinpos.texpdate = "";
            creditordebitcardinpos.tappcode = "";
            creditordebitcardinpos.tref = "";
            creditordebitcardinpos.tinv = "";
            creditordebitcardinpos.tcholder = "";
            creditordebitcardinpos.tamount = "";
            AddNewCustomerInPOS.customername = "";
            AddNewCustomerInPOS.customercity = "";
            AddNewCustomerInPOS.customermobile = "";
            creditordebitcardinpos.paymenttype = "";
            chequedetails.customerbank = "";
            chequedetails.customercheque = "";
            chequedetails.customername = "";
            txttax.Text = "";
            txtqty.Text = "";
            txtitemname.Text = "";
            lvItem.Items.Clear();
            txtcash.Text = "0";
            lblqty.Text = "";
            txttamt.Text = "";
            txtldis.Text = "";
            txtodis.Text = "";
            txtodis1.Text = "";
            txttotaldisamt.Text = "";
            txtptotal.Text = "";
            txtreturnamount.Text = "0";
            tamount = "";
            tbankname = "";
            tcardnum = "";
            tcardtype = "";
            texpdate = "";
            tappcode = "";
            tref = "";
            tinv = "";
            tcholder = "";
            lblpaymenttype.Text = "";
            lblcusname.Text = "";
            lblcuscity.Text = "";
            lblcusmobile.Text = "";
            pnlpayment.Visible = false;
            updatebillno = "";

        }
        private void btncash_Click(object sender, EventArgs e)
        {
            if (lvItem.Items.Count > 0)
            {

                btncash.Enabled = false;
                savedata();
                btncash.Enabled = true;
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[9]["p"].ToString() == "True")
                    {
                        print();
                    }
                    else
                    {
                        MessageBox.Show("You don't have Permission To Print");
                        return;
                    }
                }
                //  print();
                clearall();
                showTotalBillAmount();
            }
            else
            {
                MessageBox.Show("Enter Item");
            }
        }
        public void showTotalBillAmount()
        {
            DataTable dt1 = conn.getdataset("select sum(totalnet) as totalsum from BillPOsMaster  where isactive=1 and BillDate='" + Convert.ToDateTime(lbldate.Text).ToString(Master.dateformate) + "'", con);
            //select sum(totalnet) as totalsum,billno from BillPOsMaster  where isactive=1 and BillDate='"+ Convert.ToDateTime(lbldate.Text).ToString(Master.dateformate) +"'" group by billno
            if (dt1 == null)
            {
                lblNetAmount.Text = "Today's Bill Amount:0";
            }
            else
            {
                if (dt1.Rows.Count > 0)
                {
                    lblNetAmount.Text = "Today's Bill Amount:" + dt1.Rows[0][0].ToString();
                }
                else
                {
                    lblNetAmount.Text = "Today's Bill Amount:0";
                }
            }
        }
        Printing prn = new Printing();
        string finalbillno;
        string finalroundoff;
        public void print()
        {
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[9]["p"].ToString() == "True")
                {
                    ChangeNumbersToWords sh = new ChangeNumbersToWords();
                    String s1 = Math.Round(Convert.ToDouble(txtgtotal), 2).ToString("########.00");
                    string[] words = s1.Split('.');


                    string str = sh.changeToWords(words[0]);
                    string str1 = sh.changeToWords(words[1]);
                    if (str1 == " " || str1 == null || words[1] == "00")
                    {
                        str1 = "Zero ";
                    }
                    String inword = "In words: " + str + "and " + str1 + "Paise Only";
                    if (Convert.ToBoolean(options.Rows[0]["requirprintpopupinpos"].ToString()) == true)
                    {
                        DialogResult dr1 = MessageBox.Show("Do you want to Print Bill?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr1 == DialogResult.Yes)
                        {
                            string bid;
                            if (!string.IsNullOrEmpty(updatebillno))
                            {
                                bid = billid;

                            }
                            else
                            {

                                bid = strbillno;
                            }
                            // DataTable client = sql.getdataset("select * from clientmaster where isactive=1 and clientID='" + cmbcustname.SelectedValue + "'");
                            DataTable dt1 = conn.getdataset("select * from BillPOSMaster WHERE isactive=1 and Billid='" + bid + "'");
                            DataTable dt2 = conn.getdataset("select * from BillPOSProductMaster WHERE isactive=1 and Billid='" + bid + "'");


                            DataTable dt3 = conn.getdataset("select * from company WHERE isactive=1");
                            DataTable dt4 = conn.getdataset("select sum(amount)-sum(discountamt) as basicamount,sum(sgst) as sgst,sum(cgst) as cgst,sum(amount)+sum(sgst)+sum(cgst)-sum(discountamt) as total, igst,sum(Addtaxamt) as Addtaxamt,sum(Addtax) as Addtax  from BillPOSProductMaster WHERE isactive=1 and Billid='" + bid + "' group by igst");
                            string taxable = "Taxable Amt" + Environment.NewLine, cgstper = "CGST % " + Environment.NewLine, cgstamt = "CGST AMT" + Environment.NewLine, sgstper = "SGST %" + Environment.NewLine, sgstamt = "SGST AMT" + Environment.NewLine, totalamt = "Total AMT" + Environment.NewLine, addper = "AddTax%" + Environment.NewLine, addamt = "AddAmt" + Environment.NewLine;
                            double cgst = 0, sgst = 0, basicamt = 0, nettotal = 0, Addtax = 0; ;
                            for (int i = 0; i < dt4.Rows.Count; i++)
                            {
                                taxable += Environment.NewLine + dt4.Rows[i]["basicamount"].ToString();
                                basicamt += Convert.ToDouble(dt4.Rows[i]["basicamount"].ToString());

                                cgstper += Environment.NewLine + (Convert.ToDouble(dt4.Rows[i]["igst"].ToString()) / 2).ToString();
                                cgstamt += Environment.NewLine + dt4.Rows[i]["cgst"].ToString();
                                cgst += Convert.ToDouble(dt4.Rows[i]["cgst"].ToString());

                                sgstper += Environment.NewLine + (Convert.ToDouble(dt4.Rows[i]["igst"].ToString()) / 2).ToString();
                                sgstamt += Environment.NewLine + dt4.Rows[i]["sgst"].ToString();
                                sgst += Convert.ToDouble(dt4.Rows[i]["sgst"].ToString());

                                addper += Environment.NewLine + (Convert.ToDouble(dt4.Rows[i]["Addtax"].ToString()) / 2).ToString();
                                addamt += Environment.NewLine + dt4.Rows[i]["Addtaxamt"].ToString();
                                Addtax += Convert.ToDouble(dt4.Rows[i]["Addtaxamt"].ToString());

                                totalamt += Environment.NewLine + dt4.Rows[i]["total"].ToString();
                                nettotal += Convert.ToDouble(dt4.Rows[i]["total"].ToString());
                            }
                            if (Convert.ToBoolean(options.Rows[0]["autoroundoffpos"].ToString()) == true)
                            {
                                finalroundoff = Math.Round((Math.Round(nettotal, 0) - nettotal), 2).ToString();
                                nettotal = Math.Round(nettotal, 0);
                            }
                            prn.execute("delete from printing");
                            int j = 1;
                            for (int i = 0; i < dt2.Rows.Count; i++)
                            {
                                try
                                {
                                    if (options.Rows[0]["posbillno"].ToString() == "Continuous")
                                    {
                                        finalbillno = dt1.Rows[0]["Billid"].ToString();
                                    }
                                    else
                                    {
                                        finalbillno = dt1.Rows[0]["billno"].ToString();
                                    }
                                    DataTable hsn = conn.getdataset("select * from ProductMaster where isactive=1 and ProductID='" + dt2.Rows[i]["itemid"].ToString() + "'");
                                    string MRP = conn.ExecuteScalar("select MRP from ProductPriceMaster where isactive=1 and ProductID='" + dt2.Rows[i]["itemid"].ToString() + "'");
                                    DataTable item = conn.getdataset("select * from TaxSlabMaster where isactive=1 and saletypename='" + lblsaletype.Text + "' and Taxslabname='" + hsn.Rows[0]["taxslab"].ToString() + "'");
                                    DataTable barcode = conn.getdataset("select Barcode from ProductPriceMaster where isactive=1 and ProductID='" + dt2.Rows[i]["itemid"].ToString() + "'");
                                   

                                    string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34,T35,T36,T37,T38,T39,T40,T41,T42,T43,T44,T45,T46,T47,T48,T49,T50,T51,T52,T53,T54,T55,T56,T57,T58,T59,T60,T61,T62,T63,T64,T65,T66,T67,T68,T69,T70,T71,T72,T73,T74,T75,T76,T77,T78,T79,T80,T81,T82,T83,T84,T85,T86,T87,T88,T89,T90,T91,T92,T93,T94,T95,T96,T97,T98,T99,T100,P1,P2,P3,P4,P5,P6,P7,P8,P9,P10,P11,P12,P13,P14,P15,P16,P17)VALUES";
                                    // qry += "('" + dt3.Rows[0]["CompanyName"].ToString() + "','" + dt3.Rows[0]["Address"].ToString() + "','" + dt3.Rows[0]["Address2"].ToString() + "','" + dt3.Rows[0]["city"].ToString() + "','" + dt3.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Billid"].ToString() + "','" + Convert.ToDateTime(dt1.Rows[0]["BillDate"].ToString()).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(dt1.Rows[0]["BillDate"].ToString()).ToString("HH:mm tt") + "','" + hsn.Rows[0]["Hsn_Sac_Code"].ToString() + "','" + dt2.Rows[i]["ItemName"].ToString() + "','" + dt2.Rows[i]["qty"].ToString() + "','" + dt2.Rows[i]["Rate"].ToString() + "','" + dt2.Rows[i]["Amount"].ToString() + "')";
                                    qry += "('" + dt3.Rows[0]["CompanyName"].ToString() + "','" + dt3.Rows[0]["SubName"].ToString() + "','" + dt3.Rows[0]["Address"].ToString() + "','" + dt3.Rows[0]["Address2"].ToString() + "','" + dt3.Rows[0]["City"].ToString() + "','" + dt3.Rows[0]["Phone"].ToString() + "','" + dt3.Rows[0]["Mobile"].ToString() + "','" + dt3.Rows[0]["Email"].ToString() + "','" + dt3.Rows[0]["Website"].ToString() + "','" + dt3.Rows[0]["CSTNo"].ToString() + "','" + dt3.Rows[0]["PANNo"].ToString() + "','" + dt3.Rows[0]["VATNo"].ToString() + "','" + dt3.Rows[0]["DLNo1"].ToString() + "','" + dt3.Rows[0]["DLNo2"].ToString() + "','" + dt3.Rows[0]["DealsIn"].ToString() + "','" + dt3.Rows[0]["Stockist"].ToString() + "','" + dt3.Rows[0]["currency"].ToString() + "','" + dt3.Rows[0]["StartDate"].ToString() + "','" + dt3.Rows[0]["EndDate"].ToString() + "','" + dt3.Rows[0]["MyDSNName"].ToString() + "','" + dt3.Rows[0]["LinkRemote"].ToString() + "','" + dt3.Rows[0]["DBType"].ToString() + "','" + dt3.Rows[0]["Catalyst"].ToString() + "','" + finalbillno + "','" + Convert.ToDateTime(dt1.Rows[0]["BillDate"].ToString()).ToString(Master.dateformate) + "','" + dt1.Rows[0]["Terms"].ToString() + "','" + dt1.Rows[0]["count"].ToString() + "','" + dt1.Rows[0]["totalqty"].ToString() + "','" + dt1.Rows[0]["totalbasic"].ToString() + "','" + dt1.Rows[0]["totaltax"].ToString() + "','" + dt1.Rows[0]["totalnet"].ToString() + "','" + dt1.Rows[0]["disamt"].ToString() + "','" + dt1.Rows[0]["adddisamt"].ToString() + "','" + dt1.Rows[0]["bankname"].ToString() + "','" + dt1.Rows[0]["cardnumbar"].ToString() + "','" + dt1.Rows[0]["cardtype"].ToString() + "','" + dt1.Rows[0]["expirydate"].ToString() + "','" + dt1.Rows[0]["apprcode"].ToString() + "','" + dt1.Rows[0]["refno"].ToString() + "','" + dt1.Rows[0]["amountrs"].ToString() + "','" + dt1.Rows[0]["invno"].ToString() + "','" + dt1.Rows[0]["cardholdername"].ToString() + "','" + dt1.Rows[0]["cashtendered"].ToString() + "','" + dt1.Rows[0]["change"].ToString() + "','" + dt2.Rows[i]["ItemName"].ToString() + "','" + dt2.Rows[i]["Qty"].ToString() + "','" + dt2.Rows[i]["Rate"].ToString() + "','" + dt2.Rows[i]["Amount"].ToString() + "','" + dt2.Rows[i]["Total"].ToString() + "','" + dt2.Rows[i]["igst"].ToString() + "','" + dt2.Rows[i]["Addtax"].ToString() + "','" + dt2.Rows[i]["Discount"].ToString() + "','" + dt2.Rows[i]["Per"].ToString() + "','" + dt2.Rows[i]["SerCharge"].ToString() + "','" + dt2.Rows[i]["PackCharge"].ToString() + "','" + dt2.Rows[i]["RoundOf"].ToString() + "','" + dt2.Rows[i]["NetTotal"].ToString() + "','" + dt2.Rows[i]["CashTendered"].ToString() + "','" + dt2.Rows[i]["Change"].ToString() + "','" + dt2.Rows[i]["sgst"].ToString() + "','" + dt2.Rows[i]["cgst"].ToString() + "','" + hsn.Rows[0]["ProductID"].ToString() + "','" + hsn.Rows[0]["CompanyID"].ToString() + "','" + hsn.Rows[0]["GroupName"].ToString() + "','" + hsn.Rows[0]["Product_Name"].ToString() + "','" + hsn.Rows[0]["Unit"].ToString() + "','" + hsn.Rows[0]["Altunit"].ToString() + "','" + hsn.Rows[0]["Convfactor"].ToString() + "','" + hsn.Rows[0]["Packing"].ToString() + "','" + hsn.Rows[0]["IsBatch"].ToString() + "','" + hsn.Rows[0]["Hsn_Sac_Code"].ToString() + "','" + dt3.Rows[0]["CompanyID"].ToString() + "','" + item.Rows[0]["sgst"].ToString() + "','" + item.Rows[0]["cgst"].ToString() + "','" + item.Rows[0]["additax"].ToString() + "','" + options.Rows[0]["autosaletype"].ToString() + "','" + taxable + "','" + cgstper + "','" + cgstamt + "','" + sgstper + "','" + sgstamt + "','" + totalamt + "','" + basicamt + "','" + cgst + "','" + sgst + "','" + nettotal.ToString("N2") + "','" + dt2.Rows[i]["DiscountAmt"].ToString() + "','" + addper + "','" + addamt + "','" + Addtax + "','" + dt1.Rows[0]["customername"].ToString() + "','" + dt1.Rows[0]["customercity"].ToString() + "','" + dt1.Rows[0]["customermobile"].ToString() + "','" + hsn.Rows[0]["taxslab"].ToString() + "','" + dt1.Rows[0]["billno"].ToString() + "','" + txtldis.Text + "','" + inword + "','" + MRP + "','" + finalroundoff + "','" + hsn.Rows[0]["OT1"].ToString() + "','" + hsn.Rows[0]["OT2"].ToString() + "','" + hsn.Rows[0]["OT3"].ToString() + "','" + hsn.Rows[0]["OT4"].ToString() + "','" + hsn.Rows[0]["OT5"].ToString() + "','" + hsn.Rows[0]["OT6"].ToString() + "','" + hsn.Rows[0]["OT7"].ToString() + "','" + hsn.Rows[0]["OT8"].ToString() + "','" + hsn.Rows[0]["OT9"].ToString() + "','" + hsn.Rows[0]["OT10"].ToString() + "','" + hsn.Rows[0]["OT11"].ToString() + "','" + hsn.Rows[0]["OT12"].ToString() + "','" + hsn.Rows[0]["OT13"].ToString() + "','" + hsn.Rows[0]["OT14"].ToString() + "','" + hsn.Rows[0]["OT15"].ToString() + "','" + barcode.Rows[0][0].ToString()+ "','"+ j++ +"','" + dt2.Rows[0]["serialno"].ToString() + "')";                                   prn.execute(qry);
                                }
                                catch
                                {
                                }
                            }
                            DataTable multyprint = conn.getdataset("select defaultbill from Options");
                            if (Convert.ToBoolean(options.Rows[0]["multyprintinpos"].ToString()) == true)
                            {
                                Print popup = new Print("Pos");
                                popup.ShowDialog();
                                popup.Dispose();
                            }
                            else
                            {
                                string strreport = Application.StartupPath + "\\" + "QuickSale.rpt";
                                SQLReport sqlreport = new SQLReport(strreport, "Pos");
                                DataTable bill = conn.getdataset("select defaultbill,kot from Options");
                                if (bill.Rows.Count > 0)
                                {
                                    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                                    {
                                        sqlreport.Show();
                                    }
                                    else
                                    {
                                        //SaleReport sale = new SaleReport(str);
                                        sqlreport.Show();
                                        sqlreport.Hide();
                                    }
                                    if (bill.Rows[0]["kot"].ToString() == "Preview")
                                    {
                                        string strreport1 = Application.StartupPath + "\\" + "KOT.rpt";
                                        SQLReport sqlreport1 = new SQLReport(strreport, "Pos");
                                        sqlreport1.Show();
                                        sqlreport1.Hide();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        string bid;
                        if (!string.IsNullOrEmpty(updatebillno))
                        {
                            bid = billid;

                        }
                        else
                        {
                            bid = strbillno;

                        }
                        // DataTable client = sql.getdataset("select * from clientmaster where isactive=1 and clientID='" + cmbcustname.SelectedValue + "'");
                        DataTable dt1 = conn.getdataset("select * from BillPOSMaster WHERE isactive=1 and Billid='" + bid + "'");
                        DataTable dt2 = conn.getdataset("select * from BillPOSProductMaster WHERE isactive=1 and Billid='" + bid + "'");


                        DataTable dt3 = conn.getdataset("select * from company WHERE isactive=1");
                        DataTable dt4 = conn.getdataset("select sum(amount)-sum(discountamt) as basicamount,sum(sgst) as sgst,sum(cgst) as cgst,sum(amount)+sum(sgst)+sum(cgst)-sum(discountamt) as total, igst,sum(Addtaxamt) as Addtaxamt,sum(Addtax) as Addtax  from BillPOSProductMaster WHERE isactive=1 and Billid='" + bid + "' group by igst");
                        string taxable = "Taxable Amt" + Environment.NewLine, cgstper = "CGST % " + Environment.NewLine, cgstamt = "CGST AMT" + Environment.NewLine, sgstper = "SGST %" + Environment.NewLine, sgstamt = "SGST AMT" + Environment.NewLine, totalamt = "Total AMT" + Environment.NewLine, addper = "AddTax%" + Environment.NewLine, addamt = "AddAmt" + Environment.NewLine;
                        double cgst = 0, sgst = 0, basicamt = 0, nettotal = 0, Addtax = 0; ;
                        for (int i = 0; i < dt4.Rows.Count; i++)
                        {
                            taxable += Environment.NewLine + dt4.Rows[i]["basicamount"].ToString();
                            basicamt += Convert.ToDouble(dt4.Rows[i]["basicamount"].ToString());

                            cgstper += Environment.NewLine + (Convert.ToDouble(dt4.Rows[i]["igst"].ToString()) / 2).ToString();
                            cgstamt += Environment.NewLine + dt4.Rows[i]["cgst"].ToString();
                            cgst += Convert.ToDouble(dt4.Rows[i]["cgst"].ToString());

                            sgstper += Environment.NewLine + (Convert.ToDouble(dt4.Rows[i]["igst"].ToString()) / 2).ToString();
                            sgstamt += Environment.NewLine + dt4.Rows[i]["sgst"].ToString();
                            sgst += Convert.ToDouble(dt4.Rows[i]["sgst"].ToString());

                            addper += Environment.NewLine + (Convert.ToDouble(dt4.Rows[i]["Addtax"].ToString()) / 2).ToString();
                            addamt += Environment.NewLine + dt4.Rows[i]["Addtaxamt"].ToString();
                            Addtax += Convert.ToDouble(dt4.Rows[i]["Addtaxamt"].ToString());

                            totalamt += Environment.NewLine + dt4.Rows[i]["total"].ToString();
                            nettotal += Convert.ToDouble(dt4.Rows[i]["total"].ToString());
                        }
                        if (Convert.ToBoolean(options.Rows[0]["autoroundoffpos"].ToString()) == true)
                        {
                            finalroundoff = Math.Round((Math.Round(nettotal, 0) - nettotal), 2).ToString();
                            nettotal = Math.Round(nettotal, 0);

                        }
                        prn.execute("delete from printing");
                        int j = 1;
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            try
                            {
                                if (options.Rows[0]["posbillno"].ToString() == "Continuous")
                                {
                                    finalbillno = dt1.Rows[0]["Billid"].ToString();
                                }
                                else
                                {
                                    finalbillno = dt1.Rows[0]["billno"].ToString();
                                }
                                DataTable hsn = conn.getdataset("select * from ProductMaster where isactive=1 and ProductID='" + dt2.Rows[i]["itemid"].ToString() + "'");
                                DataTable item = conn.getdataset("select * from TaxSlabMaster where isactive=1 and saletypename='" + lblsaletype.Text + "' and Taxslabname='" + hsn.Rows[0]["taxslab"].ToString() + "'");
                                string MRP = conn.ExecuteScalar("select MRP from ProductPriceMaster where isactive=1 and ProductID='" + dt2.Rows[i]["itemid"].ToString() + "'");
                                DataTable barcode = conn.getdataset("select Barcode from ProductPriceMaster where isactive=1 and ProductID='" + dt2.Rows[i]["itemid"].ToString() + "'");

                                string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34,T35,T36,T37,T38,T39,T40,T41,T42,T43,T44,T45,T46,T47,T48,T49,T50,T51,T52,T53,T54,T55,T56,T57,T58,T59,T60,T61,T62,T63,T64,T65,T66,T67,T68,T69,T70,T71,T72,T73,T74,T75,T76,T77,T78,T79,T80,T81,T82,T83,T84,T85,T86,T87,T88,T89,T90,T91,T92,T93,T94,T95,T96,T97,T98,T99,T100,P1,P2,P3,P4,P5,P6,P7,P8,P9,P10,P11,P12,P13,P14,P15,P16,P17)VALUES";
                                // qry += "('" + dt3.Rows[0]["CompanyName"].ToString() + "','" + dt3.Rows[0]["Address"].ToString() + "','" + dt3.Rows[0]["Address2"].ToString() + "','" + dt3.Rows[0]["city"].ToString() + "','" + dt3.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Billid"].ToString() + "','" + Convert.ToDateTime(dt1.Rows[0]["BillDate"].ToString()).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(dt1.Rows[0]["BillDate"].ToString()).ToString("HH:mm tt") + "','" + hsn.Rows[0]["Hsn_Sac_Code"].ToString() + "','" + dt2.Rows[i]["ItemName"].ToString() + "','" + dt2.Rows[i]["qty"].ToString() + "','" + dt2.Rows[i]["Rate"].ToString() + "','" + dt2.Rows[i]["Amount"].ToString() + "')";
                                qry += "('" + dt3.Rows[0]["CompanyName"].ToString() + "','" + dt3.Rows[0]["SubName"].ToString() + "','" + dt3.Rows[0]["Address"].ToString() + "','" + dt3.Rows[0]["Address2"].ToString() + "','" + dt3.Rows[0]["City"].ToString() + "','" + dt3.Rows[0]["Phone"].ToString() + "','" + dt3.Rows[0]["Mobile"].ToString() + "','" + dt3.Rows[0]["Email"].ToString() + "','" + dt3.Rows[0]["Website"].ToString() + "','" + dt3.Rows[0]["CSTNo"].ToString() + "','" + dt3.Rows[0]["PANNo"].ToString() + "','" + dt3.Rows[0]["VATNo"].ToString() + "','" + dt3.Rows[0]["DLNo1"].ToString() + "','" + dt3.Rows[0]["DLNo2"].ToString() + "','" + dt3.Rows[0]["DealsIn"].ToString() + "','" + dt3.Rows[0]["Stockist"].ToString() + "','" + dt3.Rows[0]["currency"].ToString() + "','" + dt3.Rows[0]["StartDate"].ToString() + "','" + dt3.Rows[0]["EndDate"].ToString() + "','" + dt3.Rows[0]["MyDSNName"].ToString() + "','" + dt3.Rows[0]["LinkRemote"].ToString() + "','" + dt3.Rows[0]["DBType"].ToString() + "','" + dt3.Rows[0]["Catalyst"].ToString() + "','" + finalbillno + "','" + Convert.ToDateTime(dt1.Rows[0]["BillDate"].ToString()).ToString(Master.dateformate) + "','" + dt1.Rows[0]["Terms"].ToString() + "','" + dt1.Rows[0]["count"].ToString() + "','" + dt1.Rows[0]["totalqty"].ToString() + "','" + dt1.Rows[0]["totalbasic"].ToString() + "','" + dt1.Rows[0]["totaltax"].ToString() + "','" + dt1.Rows[0]["totalnet"].ToString() + "','" + dt1.Rows[0]["disamt"].ToString() + "','" + dt1.Rows[0]["adddisamt"].ToString() + "','" + dt1.Rows[0]["bankname"].ToString() + "','" + dt1.Rows[0]["cardnumbar"].ToString() + "','" + dt1.Rows[0]["cardtype"].ToString() + "','" + dt1.Rows[0]["expirydate"].ToString() + "','" + dt1.Rows[0]["apprcode"].ToString() + "','" + dt1.Rows[0]["refno"].ToString() + "','" + dt1.Rows[0]["amountrs"].ToString() + "','" + dt1.Rows[0]["invno"].ToString() + "','" + dt1.Rows[0]["cardholdername"].ToString() + "','" + dt1.Rows[0]["cashtendered"].ToString() + "','" + dt1.Rows[0]["change"].ToString() + "','" + dt2.Rows[i]["ItemName"].ToString() + "','" + dt2.Rows[i]["Qty"].ToString() + "','" + dt2.Rows[i]["Rate"].ToString() + "','" + dt2.Rows[i]["Amount"].ToString() + "','" + dt2.Rows[i]["Total"].ToString() + "','" + dt2.Rows[i]["igst"].ToString() + "','" + dt2.Rows[i]["Addtax"].ToString() + "','" + dt2.Rows[i]["Discount"].ToString() + "','" + dt2.Rows[i]["Per"].ToString() + "','" + dt2.Rows[i]["SerCharge"].ToString() + "','" + dt2.Rows[i]["PackCharge"].ToString() + "','" + dt2.Rows[i]["RoundOf"].ToString() + "','" + dt2.Rows[i]["NetTotal"].ToString() + "','" + dt2.Rows[i]["CashTendered"].ToString() + "','" + dt2.Rows[i]["Change"].ToString() + "','" + dt2.Rows[i]["sgst"].ToString() + "','" + dt2.Rows[i]["cgst"].ToString() + "','" + hsn.Rows[0]["ProductID"].ToString() + "','" + hsn.Rows[0]["CompanyID"].ToString() + "','" + hsn.Rows[0]["GroupName"].ToString() + "','" + hsn.Rows[0]["Product_Name"].ToString() + "','" + hsn.Rows[0]["Unit"].ToString() + "','" + hsn.Rows[0]["Altunit"].ToString() + "','" + hsn.Rows[0]["Convfactor"].ToString() + "','" + hsn.Rows[0]["Packing"].ToString() + "','" + hsn.Rows[0]["IsBatch"].ToString() + "','" + hsn.Rows[0]["Hsn_Sac_Code"].ToString() + "','" + dt3.Rows[0]["CompanyID"].ToString() + "','" + item.Rows[0]["sgst"].ToString() + "','" + item.Rows[0]["cgst"].ToString() + "','" + item.Rows[0]["additax"].ToString() + "','" + options.Rows[0]["autosaletype"].ToString() + "','" + taxable + "','" + cgstper + "','" + cgstamt + "','" + sgstper + "','" + sgstamt + "','" + totalamt + "','" + basicamt + "','" + cgst + "','" + sgst + "','" + nettotal.ToString("N2") + "','" + dt2.Rows[i]["DiscountAmt"].ToString() + "','" + addper + "','" + addamt + "','" + Addtax + "','" + dt1.Rows[0]["customername"].ToString() + "','" + dt1.Rows[0]["customercity"].ToString() + "','" + dt1.Rows[0]["customermobile"].ToString() + "','" + hsn.Rows[0]["taxslab"].ToString() + "','" + dt1.Rows[0]["billno"].ToString() + "','" + txtldis.Text + "','" + inword + "','" + MRP + "','" + finalroundoff + "','" + hsn.Rows[0]["OT1"].ToString() + "','" + hsn.Rows[0]["OT2"].ToString() + "','" + hsn.Rows[0]["OT3"].ToString() + "','" + hsn.Rows[0]["OT4"].ToString() + "','" + hsn.Rows[0]["OT5"].ToString() + "','" + hsn.Rows[0]["OT6"].ToString() + "','" + hsn.Rows[0]["OT7"].ToString() + "','" + hsn.Rows[0]["OT8"].ToString() + "','" + hsn.Rows[0]["OT9"].ToString() + "','" + hsn.Rows[0]["OT10"].ToString() + "','" + hsn.Rows[0]["OT11"].ToString() + "','" + hsn.Rows[0]["OT12"].ToString() + "','" + hsn.Rows[0]["OT13"].ToString() + "','" + hsn.Rows[0]["OT14"].ToString() + "','" + hsn.Rows[0]["OT15"].ToString() + "','" + barcode.Rows[0][0].ToString() + "','"+ j++ +"','" + dt2.Rows[0]["serialno"].ToString() + "')";
                                prn.execute(qry);
                            }
                            catch
                            {
                            }
                        }
                        DataTable multyprint = conn.getdataset("select defaultbill from Options");
                        if (Convert.ToBoolean(options.Rows[0]["multyprintinpos"].ToString()) == true)
                        {
                            Print popup = new Print("Pos");
                            popup.ShowDialog();
                            popup.Dispose();
                        }
                        else
                        {
                            string strreport = Application.StartupPath + "\\" + "QuickSale.rpt";
                            SQLReport sqlreport = new SQLReport(strreport, "Pos");
                            DataTable bill = conn.getdataset("select defaultbill,kot from Options");
                            if (bill.Rows.Count > 0)
                            {
                                if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                                {
                                    sqlreport.Show();
                                }
                                else
                                {
                                    //SaleReport sale = new SaleReport(str);
                                    sqlreport.Show();
                                    sqlreport.Hide();
                                }
                                if (bill.Rows[0]["kot"].ToString() == "True")
                                {
                                    string strreport1 = Application.StartupPath + "\\" + "KOT.rpt";
                                    SQLReport sqlreport1 = new SQLReport(strreport1, "Pos");
                                    sqlreport1.Show();
                                    sqlreport1.Hide();
                                }
                            }
                        }
                    }

                }
                else
                {
                    MessageBox.Show("You don't have Permission To Print");
                    return;
                }
            }
        }

        private void btnvoucher_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (!string.IsNullOrEmpty(txtpaidamt.Text))
            //    {
            //        string srno = "";
            //        if (lvpayment.Items.Count == 0)
            //        {
            //            srno = "1";
            //        }
            //        else
            //        {
            //            srno = Convert.ToString(lvpayment.Items.Count);
            //            Double a = Convert.ToDouble(srno) + 1;
            //            srno = Convert.ToString(a);
            //        }
            //        li = lvpayment.Items.Add(srno);
            //        li.SubItems.Add(btnvoucher.Text);
            //        li.SubItems.Add(txtpaidamt.Text);
            //        txtpaidamt.Text = "";
            //        txtbalance.Text = "";
            //        txtchange.Text = "";
            //        txtpaidamt.Focus();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Enter Paid Amt");
            //        txtpaidamt.Focus();
            //    }
            //}
            //catch
            //{
            //}
        }

        private void btncredit_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (!string.IsNullOrEmpty(txtpaidamt.Text))
            //    {
            //        string srno = "";
            //        if (lvpayment.Items.Count == 0)
            //        {
            //            srno = "1";
            //        }
            //        else
            //        {
            //            srno = Convert.ToString(lvpayment.Items.Count);
            //            Double a = Convert.ToDouble(srno) + 1;
            //            srno = Convert.ToString(a);
            //        }
            //        li = lvpayment.Items.Add(srno);
            //        li.SubItems.Add(btncredit.Text);
            //        li.SubItems.Add(txtpaidamt.Text);
            //        txtpaidamt.Text = "";
            //        txtbalance.Text = "";
            //        txtchange.Text = "";
            //        txtpaidamt.Focus();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Enter Paid Amt");
            //        txtpaidamt.Focus();
            //    }
            //}
            //catch
            //{
            //}
        }

        private void btnbank_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (!string.IsNullOrEmpty(txtpaidamt.Text))
            //    {
            //        string srno = "";
            //        if (lvpayment.Items.Count == 0)
            //        {
            //            srno = "1";
            //        }
            //        else
            //        {
            //            srno = Convert.ToString(lvpayment.Items.Count);
            //            Double a = Convert.ToDouble(srno) + 1;
            //            srno = Convert.ToString(a);
            //        }
            //        li = lvpayment.Items.Add(srno);
            //        li.SubItems.Add(btnbank.Text);
            //        li.SubItems.Add(txtpaidamt.Text);
            //        txtpaidamt.Text = "";
            //        txtbalance.Text = "";
            //        txtchange.Text = "";
            //        txtpaidamt.Focus();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Enter Paid Amt");
            //        txtpaidamt.Focus();
            //    }
            //}
            //catch
            //{
            //}
        }

        private void btnDeleteRow_Click(object sender, EventArgs e)
        {
            //try
            //{

            //    lvpayment.Items[lvpayment.FocusedItem.Index].Remove();

            //}
            //catch
            //{
            //}
        }

        private void txtcash_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Double cashamt = Convert.ToDouble(txtcash.Text);
                Double totalamt = Convert.ToDouble(txtptotal.Text);
                Double final = cashamt - totalamt;
                txtreturnamount.Text = Convert.ToString(final.ToString("N2"));
            }
            catch
            {
            }
        }

        private void txtldis_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtldis.Text) && txtldis.Text != "0")
                {
                    //if (string.IsNullOrEmpty(txttotaldisamt.Text) || txttotaldisamt.Text == "0")
                    //{
                        double billdis = Convert.ToDouble(txtldis.Text);
                        double finaltotal = Convert.ToDouble(totalforchange);
                        double manualdisamt = Math.Round((finaltotal * (Convert.ToDouble(billdis) / 100)), 2);
                        txttotaldisamt.Text = Convert.ToString(manualdisamt);
                        double abc = Convert.ToDouble(finaltotal) - Convert.ToDouble(txttotaldisamt.Text);
                        txtptotal.Text = Convert.ToString(abc);
                    //}
                }
                if (string.IsNullOrEmpty(txtldis.Text))
                {
                    txtptotal.Text = totalforchange;
                    txttotaldisamt.Text = "0";
                }

            }
            catch
            {
            }
        }

        private void lvItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Delete Item?", "Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    lvItem.Items[lvItem.FocusedItem.Index].Remove();
                    finaltotal();
                }
            }
        }

        private void lvItem_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {

            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Near;
                using (Font headerFont =
                new Font("Microsoft Sans Serif", 10, FontStyle.Bold)) //Font size!!!!
                {
                    //e.Graphics.DrawString(e.Header.Text, headerFont,
                    //    Brushes.White, e.Bounds, sf);
                    //e.Graphics.FillRectangle(Brushes.LightBlue, e.Bounds);
                    e.Graphics.FillRectangle(Brushes.LightBlue, e.Bounds);
                    e.Graphics.DrawString(e.Header.Text, headerFont,
                        Brushes.Black, e.Bounds, sf);
                    //e.DrawText();
                }
            }
        }

        private void lvItem_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void lvItem_MouseClick(object sender, MouseEventArgs e)
        {
            hitinfo = lvItem.HitTest(e.X, e.Y);
            columnindex = hitinfo.Item.SubItems.IndexOf(hitinfo.SubItem);
            if (columnindex == 15)
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Delete Item?", "Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {

                    lvItem.Items[lvItem.FocusedItem.Index].Remove();
                    finaltotal();
                }
            }
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    DialogResult dr1 = MessageBox.Show("Do you want to Delete All Item?", "Items", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (dr1 == DialogResult.Yes)
            //    {
            //        lvItem.Items.Clear();
            //        finaltotal();
            //    }
            //}
            //catch
            //{
            //}
            try
            {
                if (id != "" && id != null)
                {
                    DialogResult dr1 = MessageBox.Show("Do you want to Delete Bill?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr1 == DialogResult.Yes)
                    {
                        this.Enabled = false;
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                        con.Open();
                        conn.execute("Update BillPOSProductMaster set isactive=0 where BillId='" + id + "'");
                        conn.execute("Update BillPOSMaster set isactive=0 where BillId='" + id + "'");
			 conn.execute("Update BillPOSProductMaster set isactive=0 where billno='" + updatebillno + "'");
                        conn.execute("Update serials set isactive=0 where billno='" + updatebillno + "'");

					
				 
                        MessageBox.Show("Delete Successfully");
                        con.Close();
                        clearall();
                        showTotalBillAmount();
                    }
                }
                else
                {
                    MessageBox.Show("Select Bill");
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

        private string id;
        public static string billid;
        internal void Update(int p, string iid)
        {
            try
            {
                userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[9]["d"].ToString() == "False")
                    {
                        btnDeleteItem.Enabled = false;
                    }
                    if (userrights.Rows[9]["p"].ToString() == "False")
                    {
                        button21.Enabled = false;
                    }
                }
                cnt = 1;
                loadpage();
                options = conn.getdataset("select * from options");
                if (iid != "")
                {
                    id = iid;
                    strbillno = iid;
                    binaagent();
                    SqlCommand cmd = new SqlCommand("select * from BillPOSMaster where isactive=1 and BillId='" + id + "' and isactive=1", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    SqlCommand cmd1 = new SqlCommand("select * from BillPOSProductMaster where isactive=1 and BillId='" + id + "' and isactive=1", con);
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    sda1.Fill(dt1);
                    updatebillno = dt.Rows[0]["billno"].ToString();
                    billid = dt.Rows[0]["Billid"].ToString();
                    txtldis.Text = dt.Rows[0]["adddisper"].ToString();
                    txttotaldisamt.Text = dt.Rows[0]["adddisamt"].ToString();
                    txttax.Text = dt.Rows[0]["totaltax"].ToString();
                    string udate = dt.Rows[0]["billdate"].ToString();
                    lbldate.Text = Convert.ToDateTime(udate).ToString("dd-MM-yyyy");
                    if (options.Rows[0]["requiragentnameinpos"].ToString() == "Ask For Agent After Item Entry")
                    {
                        pnlagent.Visible = true;
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            ListViewItem li;
                            //  string productname = conn.ExecuteScalar("select product_name from productmaster where productid='" + dt1.Rows[i]["productid"].ToString() + "'");
                            //  li.SubItems.Add(productname);
                            li = lvItem.Items.Add(dt1.Rows[i]["ItemName"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["Qty"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["Rate"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["Discount"].ToString());
                            //  li.SubItems.Add((Convert.ToDouble(dt1.Rows[i]["cgst"].ToString()) + Convert.ToDouble(dt1.Rows[i]["sgst"].ToString())).ToString());
                            Double sgst = Convert.ToDouble(dt1.Rows[i]["sgst"].ToString());
                            Double cgst = Convert.ToDouble(dt1.Rows[i]["cgst"].ToString());
                            Double totaltax = sgst + cgst;
                            li.SubItems.Add(Convert.ToString(totaltax));
                            li.SubItems.Add(dt1.Rows[i]["Total"].ToString());
                            DataTable proid = conn.getdataset("Select * from ProductMaster where isactive=1 and Product_Name='" + dt1.Rows[i]["ItemName"].ToString() + "'");
                            DataTable barcode = conn.getdataset("select * from ProductPriceMaster where isactive=1 and Productid='" + proid.Rows[0]["Productid"].ToString() + "' and Batchno='" + dt1.Rows[i]["batchno"].ToString() + "'");
                            li.SubItems.Add(barcode.Rows[0]["barcode"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["Amount"].ToString());
                            double discoamt = (Convert.ToDouble(dt1.Rows[i]["Rate"].ToString()) * (Convert.ToDouble(dt1.Rows[i]["Discount"].ToString()) / 100));
                            li.SubItems.Add(dt1.Rows[i]["DiscountAmt"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["AddtaxAmt"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["Total"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["Batchno"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["cess"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["agentid"].ToString());
							li.SubItems.Add(dt1.Rows[i]["serialno"].ToString());							  
                            li.SubItems.Add("Delete", Color.White, Color.Red, Font);
                            li.BackColor = Color.LightYellow;

                        }
                    }
                    else if (options.Rows[0]["requiragentnameinpos"].ToString() == "Ask For Agent Bill Wise")
                    {
                        pnlagent.Visible = true;
                        DataTable agent = new DataTable();
                        agent = conn.getdataset("select accountname from clientmaster where isactive=1 and clientid='" + dt.Rows[0]["agentid"].ToString() + "' and isactive=1");
                        if (agent.Rows.Count > 0)
                        {
                            //cmd = new SqlCommand("select accountname from clientmaster where isactive=1 and clientid='" + dt.Rows[0]["agentid"].ToString() + "' and isactive=1", con);
                            //con.Open();
                            string agentname = agent.Rows[0]["accountname"].ToString();
                            cmbagentname.Text = agentname;
                        }
                        //con.Close();
                        //lvItem.Columns.Add("Description", 200, HorizontalAlignment.Left);
                        //lvItem.Columns.Add("Qty", 50, HorizontalAlignment.Center);
                        //lvItem.Columns.Add("Unit Price", 90, HorizontalAlignment.Right);
                        //lvItem.Columns.Add("Disc", 70, HorizontalAlignment.Right);
                        //lvItem.Columns.Add("GstAmt", 80, HorizontalAlignment.Center);
                        //lvItem.Columns.Add("Total Price", 100, HorizontalAlignment.Right);
                        //lvItem.Columns.Add("Barcode", 0, HorizontalAlignment.Center);
                        //lvItem.Columns.Add("BasicAmt", 0, HorizontalAlignment.Center);
                        //lvItem.Columns.Add("Disamt", 0, HorizontalAlignment.Center);
                        //lvItem.Columns.Add("AddTax", 0, HorizontalAlignment.Center);
                        //lvItem.Columns.Add("Amount", 0, HorizontalAlignment.Center);
                        //lvItem.Columns.Add("Batchno", 0, HorizontalAlignment.Center);
                        //lvItem.Columns.Add("cess", 0, HorizontalAlignment.Center);
                        //lvItem.Columns.Add("AgentID", 0, HorizontalAlignment.Center);
                        //lvItem.Columns.Add("Delete", 60, HorizontalAlignment.Center);
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            ListViewItem li;
                            //   string productname = conn.ExecuteScalar("select product_name from productmaster where productid='" + dt1.Rows[i]["productid"].ToString() + "'");
                            //  li.SubItems.Add(productname);
                            li = lvItem.Items.Add(dt1.Rows[i]["ItemName"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["Qty"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["Rate"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["Discount"].ToString());
                            //  li.SubItems.Add((Convert.ToDouble(dt1.Rows[i]["cgst"].ToString()) + Convert.ToDouble(dt1.Rows[i]["sgst"].ToString())).ToString());
                            Double sgst = Convert.ToDouble(dt1.Rows[i]["sgst"].ToString());
                            Double cgst = Convert.ToDouble(dt1.Rows[i]["cgst"].ToString());
                            Double totaltax = sgst + cgst;
                            li.SubItems.Add(Convert.ToString(totaltax));
                            li.SubItems.Add(dt1.Rows[i]["Total"].ToString());
                            DataTable proid = conn.getdataset("Select * from ProductMaster where isactive=1 and Product_Name='" + dt1.Rows[i]["ItemName"].ToString() + "'");
                            DataTable barcode = conn.getdataset("select * from ProductPriceMaster where isactive=1 and Productid='" + proid.Rows[0]["Productid"].ToString() + "' and Batchno='" + dt1.Rows[i]["batchno"].ToString() + "'");
                            li.SubItems.Add(barcode.Rows[0]["barcode"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["Amount"].ToString());
                            double discoamt = (Convert.ToDouble(dt1.Rows[i]["Rate"].ToString()) * (Convert.ToDouble(dt1.Rows[i]["Discount"].ToString()) / 100));
                            li.SubItems.Add(dt1.Rows[i]["DiscountAmt"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["AddtaxAmt"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["Total"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["Batchno"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["cess"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["agentid"].ToString());
							li.SubItems.Add(dt1.Rows[i]["serialno"].ToString());											   
                            li.SubItems.Add("Delete", Color.White, Color.Red, Font);
                            li.BackColor = Color.LightYellow;

                        }
                    }

                    else
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            ListViewItem li;
                            //  string productname = conn.ExecuteScalar("select product_name from productmaster where productid='" + dt1.Rows[i]["productid"].ToString() + "'");
                            //  li.SubItems.Add(productname);

                            li = lvItem.Items.Add(dt1.Rows[i]["ItemName"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["Qty"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["Rate"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["Discount"].ToString());
                            Double sgst = Convert.ToDouble(dt1.Rows[i]["sgst"].ToString());
                            Double cgst = Convert.ToDouble(dt1.Rows[i]["cgst"].ToString());
                            Double totaltax = sgst + cgst;
                            li.SubItems.Add(Convert.ToString(totaltax));
                            li.SubItems.Add(dt1.Rows[i]["Total"].ToString());
                            DataTable proid = conn.getdataset("Select * from ProductMaster where isactive=1 and Product_Name='" + dt1.Rows[i]["ItemName"].ToString() + "'");
                            DataTable barcode = conn.getdataset("select * from ProductPriceMaster where isactive=1 and Productid='" + proid.Rows[0]["Productid"].ToString() + "' and Batchno='" + dt1.Rows[i]["batchno"].ToString() + "'");
                            li.SubItems.Add(barcode.Rows[0]["barcode"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["Amount"].ToString());
                            double discoamt = (Convert.ToDouble(dt1.Rows[i]["Rate"].ToString()) * (Convert.ToDouble(dt1.Rows[i]["Discount"].ToString()) / 100));
                            li.SubItems.Add(dt1.Rows[i]["DiscountAmt"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["AddtaxAmt"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["Total"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["Batchno"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["cess"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["agentid"].ToString());
							li.SubItems.Add(dt1.Rows[i]["serialno"].ToString());		  
                            li.SubItems.Add("Delete", Color.White, Color.Red, Font);
                            li.BackColor = Color.LightYellow;
                        }
                        finaltotal();
                        txtptotal.Text = dt.Rows[0]["totalnet"].ToString();
                    }
                }
                showTotalBillAmount();
            }
            catch
            {
            }
        }

        private void btncustomer_Click(object sender, EventArgs e)
        {
            AddNewCustomerInPOS acp = new AddNewCustomerInPOS(this, updatebillno);
            acp.ShowDialog();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            print();
            clearall();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbldate.Text = Convert.ToString(DateTime.Now);
        }

        private void txttotaldisamt_TextChanged(object sender, EventArgs e)
        {
            try
            {
						
                if (!string.IsNullOrEmpty(txttotaldisamt.Text))
                {
                    double finaltotal = Convert.ToDouble(totalforchange);
                    if (Convert.ToDouble(finaltotal) == 0)
                    {
                        txtldis.Text = Math.Round(0.00, 2).ToString();
                    }
                    else
                    {

                        double amt = (100 * Convert.ToDouble(txttotaldisamt.Text)) / Convert.ToDouble(finaltotal);
                        txtldis.Text = Math.Round(amt, 2).ToString();
                        double abc = Convert.ToDouble(finaltotal) - Convert.ToDouble(txttotaldisamt.Text);
                        txtptotal.Text = Convert.ToString(abc);
                    }

                }
                else
                {
                    txttotaldisamt.Text = "0.00";
                    txtldis.Text = "0.00";
                }
            }
            catch
            {
                txttotaldisamt.Text = "0.00";
                txtldis.Text = "0.00";
            }
        }

        private void txtldis_TextChanged_1(object sender, EventArgs e)
        {
        }

        private void txtserial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string ItemID = conn.ExecuteScalar("select p.ProductID from ProductMaster p inner join ProductPriceMaster pp on p.Productid=pp.Productid where p.isactive=1 and pp.isactive=1 and  (p.Product_Name='" + Itemname + "'or pp.barcode='" + Itemname + "')");
                //string srno = Regex.Replace(txtserial.Text, @"\s+", string.Empty);
                string srno = txtserial.Text.Trim();
                addsrno(ItemID, srno);


            }
        }
        private void addsrno(string ItemID, string srno)
        {

            DataTable checkS = conn.getdataset("select * from Serials where isactive=1 and ItemID='" + ItemID + "' and SerialNo='" + srno + "' and TransactionID='S'");
            if (checkS.Rows.Count > 0)
            {
                DataTable checkSR = conn.getdataset("select * from Serials where isactive=1 and ItemID='" + ItemID + "' and SerialNo='" + srno + "' and TransactionID='SR'");
                if (checkSR.Rows.Count > 0)
                {

                }
                else
                {
                    MessageBox.Show("Serial No. already added into the Sale. please Remove first then add again");
                    return;
                }
            }
            else
            {
                DataTable checkPC = conn.getdataset("select * from Serials s inner join SaleOrderMaster so on so.billno=s.VoucherID and so.isactive=1 where s.isactive=1 and s.ItemID='" + ItemID + "' and s.SerialNo='" + srno + "' and s.TransactionID='PC' and so.OrderStatus='Pending'");
                if (checkPC.Rows.Count > 0)
                {

                }
                else
                {
                    DataTable checkP = conn.getdataset("select * from Serials where isactive=1 and ItemID='" + ItemID + "' and SerialNo='" + srno + "' and TransactionID='P'");
                    if (checkP.Rows.Count > 0)
                    {
                        DataTable checkPR = conn.getdataset("select * from Serials where isactive=1 and ItemID='" + ItemID + "' and SerialNo='" + srno + "' and TransactionID='PR'");
                        if (checkPR.Rows.Count > 0)
                        {
                            MessageBox.Show("Serial No. already Return From the Purchase. please Remove first then add again");
                            return;
                        }

                    }
                    else
                    {
                        DialogResult dr1 = MessageBox.Show("Serial No. not available into Purchase Do you want to still add?", "Serial", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr1 == DialogResult.Yes)
                        {

                        }
                        else
                        {
                            srno = "";
                            return;
                        }

                    }

                }

            }


            bool exists = false;
            foreach (ListViewItem item in lvItem.Items)
            {
                for (int b = 0; b < item.SubItems.Count; b++)
                {
                    //   string srno = item.SubItems[22].Text;
                    //string newsrno = Regex.Replace(item.SubItems[22].Text, @"\s+", string.Empty);
                    string newsrno = item.SubItems[14].Text.Trim();
                    if (string.IsNullOrEmpty(newsrno))
                    {
                        string[] values = newsrno.Split(',');
                        for (int i = 0; i < values.Length; i++)
                        {
                            string itemname = item.SubItems[0].Text;
                            if (txtserial.Text == values[i] && txtitemname.Text == itemname)
                            {
                                exists = true;
                            }
                        }
                    }
                }
            }
            if (exists == true)
            {
                MessageBox.Show("Serial No is exists");
                existserial = "";
                serialno = "";
                txtserial.Text = "";
                return;
            }


            if (txtserial.Text == "")
            {
                MessageBox.Show("Enter Serial No");
                existserial = "";
                serialno = "";
                txtserial.Text = "";
                return;

            }

            ListViewItem li = new ListViewItem();
            foreach (ListViewItem item in lvItem.Items)
            {
                if (item.SubItems[14].Text.ToString() != "")
                {
                    string[] sr = item.SubItems[14].Text.ToString().Split(',');
                    for (int i = 0; i < sr.Length; i++)
                    {
                        if (sr[i] == txtserial.Text.Trim())
                        {
                            MessageBox.Show("Serial No is exists");
                            existserial = "";
                            serialno = "";
                            txtserial.Text = "";
                            return;
                        }
                    }
                }
            }

            serialno = txtserial.Text;
            insertdata();
            existserial = "";
            serialno = "";
            txtserial.Text = "";
            pnlserial.Visible = false;
            txtserial.Text = "";
        }

        public void bindorder()
        {
            if (isorder == true)
            {
                //ListViewItem li;
                ////   string productname = conn.ExecuteScalar("select product_name from productmaster where productid='" + dt1.Rows[i]["productid"].ToString() + "'");
                ////  li.SubItems.Add(productname);
                //li = lvItem.Items.Add("Custom Order");
                //li.SubItems.Add("1");
                //li.SubItems.Add(CustomOrder.amount);
                //li.SubItems.Add("0");
                ////  li.SubItems.Add((Convert.ToDouble(dt1.Rows[i]["cgst"].ToString()) + Convert.ToDouble(dt1.Rows[i]["sgst"].ToString())).ToString());
                //Double sgst = Convert.ToDouble(dt1.Rows[i]["sgst"].ToString());
                //Double cgst = Convert.ToDouble(dt1.Rows[i]["cgst"].ToString());
                //Double totaltax = sgst + cgst;
                //li.SubItems.Add(Convert.ToString(totaltax));
                //li.SubItems.Add(dt1.Rows[i]["Total"].ToString());
                //DataTable proid = conn.getdataset("Select * from ProductMaster where isactive=1 and Product_Name='" + dt1.Rows[i]["ItemName"].ToString() + "'");
                //DataTable barcode = conn.getdataset("select * from ProductPriceMaster where isactive=1 and Productid='" + proid.Rows[0]["Productid"].ToString() + "' and Batchno='" + dt1.Rows[i]["batchno"].ToString() + "'");
                //li.SubItems.Add(barcode.Rows[0]["barcode"].ToString());
                //li.SubItems.Add(dt1.Rows[i]["Amount"].ToString());
                //double discoamt = (Convert.ToDouble(dt1.Rows[i]["Rate"].ToString()) * (Convert.ToDouble(dt1.Rows[i]["Discount"].ToString()) / 100));
                //li.SubItems.Add(dt1.Rows[i]["DiscountAmt"].ToString());
                //li.SubItems.Add(dt1.Rows[i]["AddtaxAmt"].ToString());
                //li.SubItems.Add(dt1.Rows[i]["Total"].ToString());
                //li.SubItems.Add(dt1.Rows[i]["Batchno"].ToString());
                //li.SubItems.Add(dt1.Rows[i]["cess"].ToString());
                //li.SubItems.Add(dt1.Rows[i]["agentid"].ToString());
                //li.SubItems.Add(dt1.Rows[i]["serialno"].ToString());
                //li.SubItems.Add("Delete", Color.White, Color.Red, Font);
                //li.BackColor = Color.LightYellow;
            }
        }
        private void button18_Click(object sender, EventArgs e)
        {
            clearall();
            showTotalBillAmount();
            txtitemname.Text = "";
        }
        public bool isorder;
        private void btncustomorder_Click(object sender, EventArgs e)
        {
            CustomOrder acp = new CustomOrder(this, updatebillno);
            acp.ShowDialog();
            
        }

        private void txtorderlist_Click(object sender, EventArgs e)
        {

        }																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																	   
    }
}
