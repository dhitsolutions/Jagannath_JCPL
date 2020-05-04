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
using ClosedXML.Excel;
using System.IO;

namespace RamdevSales
{
    public partial class StockReport : Form
    {
        //  Connection con = new Connection();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection cs = new Connection();
        double optotal, d1, d2, d3, d4, d5, d6, d7, d8, d9;
        public static string iid = "";
        DataTable dt = new DataTable();
        private Master master;
        private TabControl tabControl;
        public StockReport()
        {
            InitializeComponent();
            grdstock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public StockReport(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            grdstock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.master = master;
            this.tabControl = tabControl;
        }
        public void bindgroup()
        {
            SqlCommand cmd = new SqlCommand("select id,ItemGroupName from ItemGroupMaster where isactive=1", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            txtgroup.ValueMember = "id";
            txtgroup.DisplayMember = "ItemGroupName";
            txtgroup.DataSource = dt;
           // txtgroup.SelectedIndex = -1;
        }
        private void StockReport_Load(object sender, EventArgs e)
        {

            try
            {
                bindgroup();
                //DataTable product = new DataTable();
                //DataTable opstock = new DataTable();
                //DataTable salestock = new DataTable();
                //DataTable purchasestock = new DataTable();
                //DataTable salereturnstock = new DataTable();
                //DataTable purchasereturnstock = new DataTable();
                //DataTable POSSale = new DataTable();
                //DataTable production = new DataTable();
                //DataTable adjuststock = new DataTable();

                //dt.Columns.Add("Item Code");
                //dt.Columns.Add("Name of Item");
                //dt.Columns.Add("Company");
                //dt.Columns.Add("Op. Stock");
                //dt.Columns.Add("Purchase");
                //dt.Columns.Add("Sale");
                //dt.Columns.Add("POSSale");
                //dt.Columns.Add("Sale Return");
                //dt.Columns.Add("Purchase Return");
                //dt.Columns.Add("Production");
                //dt.Columns.Add("Closing");
                //dt.Columns.Add("Total Amount");
                //dt.Columns.Add("Adjust Stock");
                //dt.Columns.Add("Final Closing");


                ////get productmaster
                //product = cs.getdataset("select p.*,c.companyname from productmaster p inner join (select productid, max(saleprice) SalePrice,max(MRP) MRP, max(purchaseprice) PurchasePrice from productpricemaster group by productid) as pp on pp.productid=p.productid inner join companymaster c on c.companyid=p.companyid where p.isactive=1 order by p.product_name");
                ////product = cs.ReturnDataSet("retrivedatawithfield",
                ////    new SqlParameter("@Fields", "p.*,c.companyname"), 
                ////    new SqlParameter("@TblNm", "productmaster p"),
                ////    new SqlParameter("@WhereClause", "inner join (select productid, max(saleprice) SalePrice,max(MRP) MRP, max(purchaseprice) PurchasePrice from productpricemaster group by productid) as pp on pp.productid=p.productid inner join companymaster c on c.companyid=p.companyid where p.isactive=1 order by p.product_name"));
                //POSSale = cs.getdataset("select ISNULL(SUM(Qty), 0) AS POSSale,ItemName from BillPOSProductMaster where isactive=1 group by ItemName");
                //opstock = cs.getdataset("select * from productpricemaster where isactive=1");
                ////opstock = cs.ReturnDataSet("retrivedatawithfield",
                ////       new SqlParameter("@Fields", "*"),
                ////       new SqlParameter("@TblNm", "productpricemaster"),
                ////       new SqlParameter("@WhereClause", " where isactive=1"));

                //purchasestock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS Purchase, productid from billproductmaster where Billtype = 'P' and isactive = 1 group by productid");
                ////purchasestock = cs.ReturnDataSet("retrivedatawithfield",
                ////                       new SqlParameter("@Fields", "ISNULL(SUM(Pqty), 0) AS Purchase, productid"),
                ////                       new SqlParameter("@TblNm", "billproductmaster"),
                ////                       new SqlParameter("@WhereClause", " where Billtype = 'P' and isactive = 1 group by productid"));

                //salestock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS Sale, productid from billproductmaster where Billtype = 'S' and isactive = 1 group by productid");
                ////salestock = cs.ReturnDataSet("retrivedatawithfield",
                ////                       new SqlParameter("@Fields", "ISNULL(SUM(Pqty), 0) AS Sale, productid"),
                ////                       new SqlParameter("@TblNm", "billproductmaster"),
                ////                       new SqlParameter("@WhereClause", " where Billtype = 'S' and isactive = 1 group by productid"));

                //salereturnstock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS SaleReturn, productid from billproductmaster where Billtype = 'SR' and isactive = 1 group by productid");
                ////salereturnstock = cs.ReturnDataSet("retrivedatawithfield",
                ////                       new SqlParameter("@Fields", "ISNULL(SUM(Pqty), 0) AS SaleReturn, productid"),
                ////                       new SqlParameter("@TblNm", "billproductmaster"),
                ////                       new SqlParameter("@WhereClause", " where Billtype = 'SR' and isactive = 1 group by productid"));


                //purchasereturnstock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS PurchaseReturn, productid from billproductmaster where Billtype = 'PR' and isactive = 1 group by productid");
                ////purchasereturnstock = cs.ReturnDataSet("retrivedatawithfield",
                ////                        new SqlParameter("@Fields", "ISNULL(SUM(Pqty), 0) AS PurchaseReturn, productid"),
                ////                        new SqlParameter("@TblNm", "billproductmaster"),
                ////                        new SqlParameter("@WhereClause", " where Billtype = 'PR' and isactive = 1 group by productid"));
                //production = cs.getdataset("select ISNULL(SUM(fQty), 0) AS fqty,proitem from tblfinishedgoodsqty where isactive=1 group by proitem");
                //adjuststock = cs.getdataset("select ISNULL(SUM(adjuststock), 0) AS adjuststock, itemid from stockadujestmentitemmaster where isactive = 1 group by itemid");

                //for (int i = 0; i < product.Rows.Count; i++)
                //{
                //    DataRow dr = dt.NewRow();
                //    dr["Item Code"] = product.Rows[i]["ProductID"].ToString();
                //    dr["Name of Item"] = product.Rows[i]["Product_Name"].ToString();
                //    dr["Company"] = product.Rows[i]["companyname"].ToString();

                //    string opening = "0", purchase = "0", sale = "0", salereturn = "0", purchasereturn = "0", possale = "0", pro = "0", ajuststock = "0";

                //    //opening stock
                //    dr["Op. Stock"] = "0";
                //    for (int j = 0; j < opstock.Rows.Count; j++)
                //    {
                //        if (opstock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                //        {
                //            dr["Op. Stock"] = opstock.Rows[j]["OpStock"].ToString();
                //            opening = opstock.Rows[j]["OpStock"].ToString();
                //            break;
                //        }

                //    }

                //    //purchase stock
                //    dr["Purchase"] = "0";
                //    for (int j = 0; j < purchasestock.Rows.Count; j++)
                //    {
                //        if (purchasestock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                //        {
                //            dr["Purchase"] = purchasestock.Rows[j]["Purchase"].ToString();
                //            purchase = purchasestock.Rows[j]["Purchase"].ToString();
                //            break;
                //        }
                //    }

                //    //sale stock
                //    dr["Sale"] = "0";
                //    for (int j = 0; j < salestock.Rows.Count; j++)
                //    {
                //        if (salestock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                //        {
                //            dr["Sale"] = salestock.Rows[j]["Sale"].ToString();
                //            sale = salestock.Rows[j]["Sale"].ToString();
                //            break;
                //        }
                //    }
                //    dr["POSSale"] = "0";
                //    for (int j = 0; j < POSSale.Rows.Count; j++)
                //    {
                //        if (POSSale.Rows[j]["ItemName"].ToString() == product.Rows[i]["Product_Name"].ToString())
                //        {
                //            dr["POSSale"] = POSSale.Rows[j]["POSSale"].ToString();
                //            possale = POSSale.Rows[j]["POSSale"].ToString();
                //            break;
                //        }
                //    }

                //    //Sale Return Stock
                //    dr["Sale Return"] = "0";
                //    for (int j = 0; j < salereturnstock.Rows.Count; j++)
                //    {
                //        if (salereturnstock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                //        {
                //            dr["Sale Return"] = salereturnstock.Rows[j]["SaleReturn"].ToString();
                //            salereturn = salereturnstock.Rows[j]["SaleReturn"].ToString();
                //            break;
                //        }
                //    }


                //    //Purchase Return Stock
                //    dr["Purchase Return"] = "0";
                //    for (int j = 0; j < purchasereturnstock.Rows.Count; j++)
                //    {
                //        if (purchasereturnstock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                //        {
                //            dr["Purchase Return"] = purchasereturnstock.Rows[j]["PurchaseReturn"].ToString();
                //            purchasereturn = purchasereturnstock.Rows[j]["PurchaseReturn"].ToString();
                //            break;
                //        }
                //    }
                //    dr["Production"] = "0";
                //    for (int j = 0; j < production.Rows.Count; j++)
                //    {
                //        if (production.Rows[j]["proitem"].ToString() == product.Rows[i]["Product_Name"].ToString())
                //        {
                //            dr["Production"] = production.Rows[j]["fqty"].ToString();
                //            pro = production.Rows[j]["fqty"].ToString();
                //            break;
                //        }
                //    }
                //    //closing
                //    Double closing = Convert.ToDouble(opening) + Convert.ToDouble(purchase.ToString()) - Convert.ToDouble(sale.ToString()) - Convert.ToDouble(possale.ToString()) + Convert.ToDouble(salereturn.ToString()) - Convert.ToDouble(purchasereturn.ToString()) - Convert.ToDouble(pro.ToString());
                //    dr["Closing"] = Math.Round(closing, 2).ToString("N2");
                //    DataTable totalamt = cs.getdataset("select PurchasePrice,SelfVal from ProductPriceMaster where isactive=1 and Productid='" + product.Rows[i]["ProductID"].ToString() + "'");
                //    Double total = closing * (Convert.ToDouble(totalamt.Rows[0]["PurchasePrice"].ToString()) + Convert.ToDouble(totalamt.Rows[0]["SelfVal"].ToString()));
                //    dr["Total Amount"] = total;
                //    //Adjust Stock
                //    dr["Adjust Stock"] = "0";
                //    for (int j = 0; j < adjuststock.Rows.Count; j++)
                //    {
                //        if (adjuststock.Rows[j]["itemid"].ToString() == product.Rows[i]["ProductID"].ToString())
                //        {
                //            dr["Adjust Stock"] = adjuststock.Rows[j]["adjuststock"].ToString();
                //            ajuststock = adjuststock.Rows[j]["adjuststock"].ToString();
                //            break;
                //        }
                //    }
                //    Double finalclosing = Convert.ToDouble(closing) + Convert.ToDouble(ajuststock);
                //    dr["Final Closing"] = Math.Round(finalclosing, 2).ToString("N2");
                //    dt.Rows.Add(dr);

                //}

                //////get opening stock
                ////opstock = cs.getdataset("SELECT TOP (100) PERCENT p.ProductID AS [Item Code], p.Product_Name AS [Name of Item], c.companyname AS Company, pp.OpStock AS [Op. Packs] FROM dbo.ProductMaster AS p LEFT OUTER JOIN dbo.ProductPriceMaster AS pp ON pp.Productid = p.ProductID INNER JOIN dbo.CompanyMaster AS c ON c.CompanyID = p.CompanyID ORDER BY [Name of Item]");
                ////salestock = cs.getdataset("SELECT TOP (100) PERCENT p.ProductID AS [Item Code], p.Product_Name AS [Name of Item], c.companyname AS Company, ISNULL(SUM(sb.Pqty), 0) AS sale FROM dbo.ProductMaster AS p LEFT OUTER JOIN dbo.ProductPriceMaster AS pp ON pp.Productid = p.ProductID INNER JOIN dbo.CompanyMaster AS c ON c.CompanyID = p.CompanyID LEFT OUTER JOIN dbo.BillProductMaster AS sb ON sb.Productname = p.Product_Name AND sb.Billtype = 's' AND sb.isactive = 1 GROUP BY p.ProductID, p.Product_Name, c.companyname ORDER BY [Name of Item]");
                ////purchasestock = cs.getdataset("SELECT TOP (100) PERCENT p.ProductID AS [Item Code], p.Product_Name AS [Name of Item], c.companyname AS Company, ISNULL(SUM(sb.Pqty), 0) AS Purchase FROM dbo.ProductMaster AS p LEFT OUTER JOIN dbo.ProductPriceMaster AS pp ON pp.Productid = p.ProductID INNER JOIN dbo.CompanyMaster AS c ON c.CompanyID = p.CompanyID LEFT OUTER JOIN dbo.BillProductMaster AS sb ON sb.Productname = p.Product_Name AND sb.Billtype = 'P' AND sb.isactive = 1 GROUP BY p.ProductID, p.Product_Name, c.companyname ORDER BY [Name of Item]");
                ////purchasereturnstock = cs.getdataset("SELECT TOP (100) PERCENT p.ProductID AS [Item Code], p.Product_Name AS [Name of Item], c.companyname AS Company, ISNULL(SUM(sb.Pqty), 0) AS [Purchase Return] FROM dbo.ProductMaster AS p LEFT OUTER JOIN dbo.ProductPriceMaster AS pp ON pp.Productid = p.ProductID INNER JOIN dbo.CompanyMaster AS c ON c.CompanyID = p.CompanyID LEFT OUTER JOIN dbo.BillProductMaster AS sb ON sb.Productname = p.Product_Name AND sb.Billtype = 'PR' AND sb.isactive = 1 GROUP BY p.ProductID, p.Product_Name, c.companyname ORDER BY [Name of Item]");
                ////salereturnstock = cs.getdataset("SELECT TOP (100) PERCENT p.ProductID AS [Item Code], p.Product_Name AS [Name of Item], c.companyname AS Company, ISNULL(SUM(sb.Pqty), 0) AS [Sale Return] FROM dbo.ProductMaster AS p LEFT OUTER JOIN dbo.ProductPriceMaster AS pp ON pp.Productid = p.ProductID INNER JOIN dbo.CompanyMaster AS c ON c.CompanyID = p.CompanyID LEFT OUTER JOIN dbo.BillProductMaster AS sb ON sb.Productname = p.Product_Name AND sb.Billtype = 'sr' AND sb.isactive = 1 GROUP BY p.ProductID, p.Product_Name, c.companyname ORDER BY [Name of Item]"); 



                ////for (int i = 0; i < opstock.Rows.Count; i++)
                ////{
                ////    try
                ////    {
                ////        Double closing = Convert.ToDouble(opstock.Rows[i]["Op. Packs"].ToString()) + Convert.ToDouble(purchasestock.Rows[i]["Purchase"].ToString()) - Convert.ToDouble(salestock.Rows[i]["sale"].ToString()) + Convert.ToDouble(salereturnstock.Rows[i]["Sale Return"].ToString()) - Convert.ToDouble(purchasereturnstock.Rows[i]["Purchase Return"].ToString());
                ////        dt.Rows.Add(opstock.Rows[i]["Item Code"].ToString(), opstock.Rows[i]["Name of Item"].ToString(), opstock.Rows[i]["Company"].ToString(), opstock.Rows[i]["Op. Packs"].ToString(), purchasestock.Rows[i]["Purchase"].ToString(), salestock.Rows[i]["sale"].ToString(), salereturnstock.Rows[i]["Sale Return"].ToString(), purchasereturnstock.Rows[i]["Purchase Return"].ToString(), Math.Round(closing, 0).ToString());
                ////    }
                ////    catch
                ////    {
                ////    }
                ////}



                //// dt = cs.getdataset("Select * from Stock_Management");
                //bindgrid();
            }
            catch
            {
            }
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


            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void bindgrid()
        {
            grdstock.DataSource = dt;
            grdstock.Columns[0].Width = 49;
            grdstock.Columns[1].Width = 300;
            grdstock.ReadOnly = true;
            // grdstock.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            openingtotal();
        }

        private void openingtotal()
        {
            d3 = 0;
            if (grdstock.Rows.Count > 0)
            {
                for (int i = 0; i < grdstock.Rows.Count; i++)
                {
                    try
                    {
                        d1 += Convert.ToDouble(grdstock.Rows[i].Cells[3].Value);
                        d2 += Convert.ToDouble(grdstock.Rows[i].Cells[4].Value);
                        d3 += Convert.ToDouble(grdstock.Rows[i].Cells[5].Value);
                        d4 += Convert.ToDouble(grdstock.Rows[i].Cells[6].Value);
                        d5 += Convert.ToDouble(grdstock.Rows[i].Cells[7].Value);
                        d6 += Convert.ToDouble(grdstock.Rows[i].Cells[8].Value);
                        d7 += Convert.ToDouble(grdstock.Rows[i].Cells[9].Value);
                        d8 += Convert.ToDouble(grdstock.Rows[i].Cells[10].Value);
                        d9 += Convert.ToDouble(grdstock.Rows[i].Cells[11].Value);
                    }
                    catch
                    {
                    }
                }
                txt1.Text = d1.ToString("N2");
                txt2.Text = d2.ToString("N2");
                txt3.Text = d3.ToString("N2");
                txt4.Text = d4.ToString("N2");
                txt5.Text = d5.ToString("N2");
                txt6.Text = d6.ToString("N2");
                txt7.Text = d7.ToString("N2");
                txt8.Text = d8.ToString("N2");
                txt9.Text = d9.ToString("N2");
            }
        }

        private void grdstock_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                this.Enabled = false;
                // iid = grdstock.Rows[grdstock.e.Index].SubItems[0].Text;
                iid = grdstock.CurrentRow.Cells[1].Value.ToString();
                ItemWiseStock dlg = new ItemWiseStock(master, tabControl);
                dlg.getitemname(1, iid);
                master.AddNewTab(dlg);
                dlg.Show();
            }
            finally
            {
                this.Enabled = true;
            }
        }

        private void grdstock_KeyDown(object sender, KeyEventArgs e)
        {

        }



        private void BtnPayment_MouseEnter(object sender, EventArgs e)
        {
            BtnPayment.UseVisualStyleBackColor = false;
            BtnPayment.BackColor = Color.FromArgb(176, 111, 193);
            BtnPayment.ForeColor = Color.White;
        }

        private void BtnPayment_MouseLeave(object sender, EventArgs e)
        {
            BtnPayment.UseVisualStyleBackColor = true;
            BtnPayment.BackColor = Color.FromArgb(51, 153, 255);
            BtnPayment.ForeColor = Color.White;
        }

        private void BtnPayment_Enter(object sender, EventArgs e)
        {
            BtnPayment.UseVisualStyleBackColor = false;
            BtnPayment.BackColor = Color.FromArgb(176, 111, 193);
            BtnPayment.ForeColor = Color.White;
        }

        private void BtnPayment_Leave(object sender, EventArgs e)
        {
            BtnPayment.UseVisualStyleBackColor = true;
            BtnPayment.BackColor = Color.FromArgb(51, 153, 255);
            BtnPayment.ForeColor = Color.White;
        }

        private void BtnPayment_Click(object sender, EventArgs e)
        {
            try
            {

                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        string[] files = Directory.GetFiles(fbd.SelectedPath);
                        string folderPath = fbd.SelectedPath + "\\";
                        String DateTimeName = DateTime.Now.ToString("dd_MMM_yyyy hh_mm_ss");
                        // string folderPath = "C:\\Excel\\";
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(dt, "Stock");
                            // wb.Worksheets.Add(dt1, "ItemPrice");
                            wb.SaveAs(folderPath + "Stock_Management" + DateTimeName + ".xlsx");
                        }
                        MessageBox.Show("Export Data Sucessfully");
                        DialogResult dr = MessageBox.Show("Do you want to Open Document?", "Stock Management", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(folderPath + "Stock_Management" + DateTimeName + ".xlsx");
                            String pathToExecutable = "AcroRd32.exe";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Error: " + ex.Message);
            }
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

        private void btncancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
                //this.Close();
            }
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

        private void btnprint_Click(object sender, EventArgs e)
        {
            Printing prndata = new Printing();
            if (grdstock.Rows.Count > 0)
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Print Stock Report?", "Stock Report", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    DataTable dt = new DataTable();
                    DataTable dt1 = cs.getdataset("select * from company WHERE isactive=1");
                    dt = (DataTable)grdstock.DataSource;
                    prndata.execute("delete from printing");
                    int j = 1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string Opstock = "", ClosingStock = "", Purchase = "", Sale = "", PurchaseReturn = "", SaleReturn = "", total = "", possale = "", production = "";
                        string ItemName = "", company = "";
                        ItemName = dt.Rows[i][1].ToString();
                        company = dt.Rows[i][2].ToString();
                        Opstock = dt.Rows[i][3].ToString();
                        Purchase = dt.Rows[i][4].ToString();
                        Sale = dt.Rows[i][5].ToString();
                        possale = dt.Rows[i][6].ToString();
                        SaleReturn = dt.Rows[i][7].ToString();
                        PurchaseReturn = dt.Rows[i][8].ToString();
                        production = dt.Rows[i][9].ToString();
                        ClosingStock = dt.Rows[i][10].ToString();
                        total = dt.Rows[i][11].ToString();
                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34,T35)VALUES";
                        qry += "('" + j++ + "','" + dt1.Rows[0][0].ToString() + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][2].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + dt1.Rows[0][11].ToString() + "','" + dt1.Rows[0][12].ToString() + "','" + dt1.Rows[0][13].ToString() + "','" + ItemName + "','" + company + "','" + Opstock + "','" + Purchase + "','" + Sale + "','" + SaleReturn + "','" + PurchaseReturn + "','" + ClosingStock + "','" + total + "','" + txt1.Text + "','" + txt2.Text + "','" + txt3.Text + "','" + txt4.Text + "','" + txt5.Text + "','" + txt6.Text + "','" + txt7.Text + "','" + txt8.Text + "','" + possale + "','" + production + "','" + txt9.Text + "')";
                        prndata.execute(qry);


                    }
                    string reportName = "StockEvaluation";
                    //  string reportName = "Sale";
                    Print popup = new Print(reportName);
                    popup.ShowDialog();
                    popup.Dispose();
                }
            }
            else
            {
                MessageBox.Show("No Records For Print..");
            }
        }

        private void txtgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (txtgroup.SelectedIndex != -1)
            //{

                #region
                dt.Columns.Clear();
                dt.Rows.Clear();
                DataTable product = new DataTable();
                DataTable opstock = new DataTable();
                DataTable salestock = new DataTable();
                DataTable purchasestock = new DataTable();
                DataTable salereturnstock = new DataTable();
                DataTable purchasereturnstock = new DataTable();
                DataTable POSSale = new DataTable();
                DataTable production = new DataTable();
                DataTable adjuststock = new DataTable();

                dt.Columns.Add("Item Code");
                dt.Columns.Add("Name of Item");
                dt.Columns.Add("Make/Model");
                dt.Columns.Add("Op. Stock");
                dt.Columns.Add("Purchase");
                dt.Columns.Add("Sale");
                dt.Columns.Add("POSSale");
                dt.Columns.Add("Sale Return");
                dt.Columns.Add("Purchase Return");
                dt.Columns.Add("Production");
                dt.Columns.Add("Closing");
                dt.Columns.Add("Total Amount");
                dt.Columns.Add("Adjust Stock");
                dt.Columns.Add("Final Closing");


                //get productmaster
                product = cs.getdataset("select p.*,c.companyname from productmaster p inner join (select productid, max(saleprice) SalePrice,max(MRP) MRP, max(purchaseprice) PurchasePrice from productpricemaster group by productid) as pp on pp.productid=p.productid inner join companymaster c on c.companyid=p.companyid where p.isactive=1 and p.GroupName='" + txtgroup.Text + "' order by p.product_name");
                //product = cs.ReturnDataSet("retrivedatawithfield",
                //    new SqlParameter("@Fields", "p.*,c.companyname"), 
                //    new SqlParameter("@TblNm", "productmaster p"),
                //    new SqlParameter("@WhereClause", "inner join (select productid, max(saleprice) SalePrice,max(MRP) MRP, max(purchaseprice) PurchasePrice from productpricemaster group by productid) as pp on pp.productid=p.productid inner join companymaster c on c.companyid=p.companyid where p.isactive=1 order by p.product_name"));
                POSSale = cs.getdataset("select ISNULL(SUM(Qty), 0) AS POSSale,ItemName from BillPOSProductMaster where isactive=1 group by ItemName");
                opstock = cs.getdataset("select * from productpricemaster where isactive=1");
                //opstock = cs.ReturnDataSet("retrivedatawithfield",
                //       new SqlParameter("@Fields", "*"),
                //       new SqlParameter("@TblNm", "productpricemaster"),
                //       new SqlParameter("@WhereClause", " where isactive=1"));

                purchasestock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS Purchase, productid from billproductmaster where Billtype = 'P' and isactive = 1 group by productid");
                //purchasestock = cs.ReturnDataSet("retrivedatawithfield",
                //                       new SqlParameter("@Fields", "ISNULL(SUM(Pqty), 0) AS Purchase, productid"),
                //                       new SqlParameter("@TblNm", "billproductmaster"),
                //                       new SqlParameter("@WhereClause", " where Billtype = 'P' and isactive = 1 group by productid"));

                salestock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS Sale, productid from billproductmaster where Billtype = 'S' and isactive = 1 group by productid");
                //salestock = cs.ReturnDataSet("retrivedatawithfield",
                //                       new SqlParameter("@Fields", "ISNULL(SUM(Pqty), 0) AS Sale, productid"),
                //                       new SqlParameter("@TblNm", "billproductmaster"),
                //                       new SqlParameter("@WhereClause", " where Billtype = 'S' and isactive = 1 group by productid"));

                salereturnstock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS SaleReturn, productid from billproductmaster where Billtype = 'SR' and isactive = 1 group by productid");
                //salereturnstock = cs.ReturnDataSet("retrivedatawithfield",
                //                       new SqlParameter("@Fields", "ISNULL(SUM(Pqty), 0) AS SaleReturn, productid"),
                //                       new SqlParameter("@TblNm", "billproductmaster"),
                //                       new SqlParameter("@WhereClause", " where Billtype = 'SR' and isactive = 1 group by productid"));


                purchasereturnstock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS PurchaseReturn, productid from billproductmaster where Billtype = 'PR' and isactive = 1 group by productid");
                //purchasereturnstock = cs.ReturnDataSet("retrivedatawithfield",
                //                        new SqlParameter("@Fields", "ISNULL(SUM(Pqty), 0) AS PurchaseReturn, productid"),
                //                        new SqlParameter("@TblNm", "billproductmaster"),
                //                        new SqlParameter("@WhereClause", " where Billtype = 'PR' and isactive = 1 group by productid"));
                production = cs.getdataset("select ISNULL(SUM(fQty), 0) AS fqty,proitem from tblfinishedgoodsqty where isactive=1 group by proitem");

                //adjuststock = cs.ReturnDataSet("retrivedatawithfield",
                //                        new SqlParameter("@Fields", "ISNULL(SUM(adjuststock), 0) AS adjuststock, itemid"),
                //                        new SqlParameter("@TblNm", "stockadujestmentitemmaster"),
                //                        new SqlParameter("@WhereClause", " where isactive = 1 group by itemid"));
                adjuststock = cs.getdataset("select ISNULL(SUM(adjuststock), 0) AS adjuststock, itemid from stockadujestmentitemmaster where isactive = 1 group by itemid");

                for (int i = 0; i < product.Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["Item Code"] = product.Rows[i]["itemnumber"].ToString();
                    dr["Name of Item"] = product.Rows[i]["Product_Name"].ToString();
                    dr["Make/Model"] = product.Rows[i]["companyname"].ToString();

                    string opening = "0", purchase = "0", sale = "0", salereturn = "0", purchasereturn = "0", possale = "0", pro = "0", ajuststock = "0";

                    //opening stock
                    dr["Op. Stock"] = "0";
                    for (int j = 0; j < opstock.Rows.Count; j++)
                    {
                        if (opstock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                        {
                            dr["Op. Stock"] = opstock.Rows[j]["OpStock"].ToString();
                            opening = opstock.Rows[j]["OpStock"].ToString();
                            break;
                        }

                    }

                    //purchase stock
                    dr["Purchase"] = "0";
                    for (int j = 0; j < purchasestock.Rows.Count; j++)
                    {
                        if (purchasestock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                        {
                            dr["Purchase"] = purchasestock.Rows[j]["Purchase"].ToString();
                            purchase = purchasestock.Rows[j]["Purchase"].ToString();
                            break;
                        }
                    }

                    //sale stock
                    dr["Sale"] = "0";
                    for (int j = 0; j < salestock.Rows.Count; j++)
                    {
                        if (salestock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                        {
                            dr["Sale"] = salestock.Rows[j]["Sale"].ToString();
                            sale = salestock.Rows[j]["Sale"].ToString();
                            break;
                        }
                    }
                    dr["POSSale"] = "0";
                    for (int j = 0; j < POSSale.Rows.Count; j++)
                    {
                        if (POSSale.Rows[j]["ItemName"].ToString() == product.Rows[i]["Product_Name"].ToString())
                        {
                            dr["POSSale"] = POSSale.Rows[j]["POSSale"].ToString();
                            possale = POSSale.Rows[j]["POSSale"].ToString();
                            break;
                        }
                    }

                    //Sale Return Stock
                    dr["Sale Return"] = "0";
                    for (int j = 0; j < salereturnstock.Rows.Count; j++)
                    {
                        if (salereturnstock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                        {
                            dr["Sale Return"] = salereturnstock.Rows[j]["SaleReturn"].ToString();
                            salereturn = salereturnstock.Rows[j]["SaleReturn"].ToString();
                            break;
                        }
                    }


                    //Purchase Return Stock
                    dr["Purchase Return"] = "0";
                    for (int j = 0; j < purchasereturnstock.Rows.Count; j++)
                    {
                        if (purchasereturnstock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                        {
                            dr["Purchase Return"] = purchasereturnstock.Rows[j]["PurchaseReturn"].ToString();
                            purchasereturn = purchasereturnstock.Rows[j]["PurchaseReturn"].ToString();
                            break;
                        }
                    }
                    dr["Production"] = "0";
                    for (int j = 0; j < production.Rows.Count; j++)
                    {
                        if (production.Rows[j]["proitem"].ToString() == product.Rows[i]["Product_Name"].ToString())
                        {
                            dr["Production"] = production.Rows[j]["fqty"].ToString();
                            pro = production.Rows[j]["fqty"].ToString();
                            break;
                        }
                    }
                    //closing
                    Double closing = Convert.ToDouble(opening) + Convert.ToDouble(purchase.ToString()) - Convert.ToDouble(sale.ToString()) - Convert.ToDouble(possale.ToString()) + Convert.ToDouble(salereturn.ToString()) - Convert.ToDouble(purchasereturn.ToString()) - Convert.ToDouble(pro.ToString());
                    dr["Closing"] = Math.Round(closing, 2).ToString("N2");
                    DataTable totalamt = cs.getdataset("select PurchasePrice,SelfVal from ProductPriceMaster where isactive=1 and Productid='" + product.Rows[i]["ProductID"].ToString() + "'");
                    Double total = closing * (Convert.ToDouble(totalamt.Rows[0]["PurchasePrice"].ToString()) + Convert.ToDouble(totalamt.Rows[0]["SelfVal"].ToString()));
                    dr["Total Amount"] = total;
                    //Adjust Stock
                    dr["Adjust Stock"] = "0";
                    for (int j = 0; j < adjuststock.Rows.Count; j++)
                    {
                        if (adjuststock.Rows[j]["itemid"].ToString() == product.Rows[i]["ProductID"].ToString())
                        {
                            dr["Adjust Stock"] = adjuststock.Rows[j]["adjuststock"].ToString();
                            ajuststock = adjuststock.Rows[j]["adjuststock"].ToString();
                            break;
                        }
                    }
                    Double finalclosing = Convert.ToDouble(closing) + Convert.ToDouble(ajuststock);
                    dr["Final Closing"] = Math.Round(finalclosing, 2).ToString("N2");
                    dt.Rows.Add(dr);

                }
                bindgrid();
                #endregion
           // }
            //else
            //{
            //    #region
            //    DataTable product = new DataTable();
            //    DataTable opstock = new DataTable();
            //    DataTable salestock = new DataTable();
            //    DataTable purchasestock = new DataTable();
            //    DataTable salereturnstock = new DataTable();
            //    DataTable purchasereturnstock = new DataTable();
            //    DataTable POSSale = new DataTable();
            //    DataTable production = new DataTable();
            //    DataTable adjuststock = new DataTable();

            //    dt.Columns.Add("Item Code");
            //    dt.Columns.Add("Name of Item");
            //    dt.Columns.Add("Make/Model");
            //    dt.Columns.Add("Op. Stock");
            //    dt.Columns.Add("Purchase");
            //    dt.Columns.Add("Sale");
            //    dt.Columns.Add("POSSale");
            //    dt.Columns.Add("Sale Return");
            //    dt.Columns.Add("Purchase Return");
            //    dt.Columns.Add("Production");
            //    dt.Columns.Add("Closing");
            //    dt.Columns.Add("Total Amount");
            //    dt.Columns.Add("Adjust Stock");
            //    dt.Columns.Add("Final Closing");


            //    //get productmaster
            //    product = cs.getdataset("select p.*,c.companyname from productmaster p inner join (select productid, max(saleprice) SalePrice,max(MRP) MRP, max(purchaseprice) PurchasePrice from productpricemaster group by productid) as pp on pp.productid=p.productid inner join companymaster c on c.companyid=p.companyid where p.isactive=1 order by p.product_name");
            //    //product = cs.ReturnDataSet("retrivedatawithfield",
            //    //    new SqlParameter("@Fields", "p.*,c.companyname"), 
            //    //    new SqlParameter("@TblNm", "productmaster p"),
            //    //    new SqlParameter("@WhereClause", "inner join (select productid, max(saleprice) SalePrice,max(MRP) MRP, max(purchaseprice) PurchasePrice from productpricemaster group by productid) as pp on pp.productid=p.productid inner join companymaster c on c.companyid=p.companyid where p.isactive=1 order by p.product_name"));
            //    POSSale = cs.getdataset("select ISNULL(SUM(Qty), 0) AS POSSale,ItemName from BillPOSProductMaster where isactive=1 group by ItemName");
            //    opstock = cs.getdataset("select * from productpricemaster where isactive=1");
            //    //opstock = cs.ReturnDataSet("retrivedatawithfield",
            //    //       new SqlParameter("@Fields", "*"),
            //    //       new SqlParameter("@TblNm", "productpricemaster"),
            //    //       new SqlParameter("@WhereClause", " where isactive=1"));

            //    purchasestock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS Purchase, productid from billproductmaster where Billtype = 'P' and isactive = 1 group by productid");
            //    //purchasestock = cs.ReturnDataSet("retrivedatawithfield",
            //    //                       new SqlParameter("@Fields", "ISNULL(SUM(Pqty), 0) AS Purchase, productid"),
            //    //                       new SqlParameter("@TblNm", "billproductmaster"),
            //    //                       new SqlParameter("@WhereClause", " where Billtype = 'P' and isactive = 1 group by productid"));

            //    salestock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS Sale, productid from billproductmaster where Billtype = 'S' and isactive = 1 group by productid");
            //    //salestock = cs.ReturnDataSet("retrivedatawithfield",
            //    //                       new SqlParameter("@Fields", "ISNULL(SUM(Pqty), 0) AS Sale, productid"),
            //    //                       new SqlParameter("@TblNm", "billproductmaster"),
            //    //                       new SqlParameter("@WhereClause", " where Billtype = 'S' and isactive = 1 group by productid"));

            //    salereturnstock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS SaleReturn, productid from billproductmaster where Billtype = 'SR' and isactive = 1 group by productid");
            //    //salereturnstock = cs.ReturnDataSet("retrivedatawithfield",
            //    //                       new SqlParameter("@Fields", "ISNULL(SUM(Pqty), 0) AS SaleReturn, productid"),
            //    //                       new SqlParameter("@TblNm", "billproductmaster"),
            //    //                       new SqlParameter("@WhereClause", " where Billtype = 'SR' and isactive = 1 group by productid"));


            //    purchasereturnstock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS PurchaseReturn, productid from billproductmaster where Billtype = 'PR' and isactive = 1 group by productid");
            //    //purchasereturnstock = cs.ReturnDataSet("retrivedatawithfield",
            //    //                        new SqlParameter("@Fields", "ISNULL(SUM(Pqty), 0) AS PurchaseReturn, productid"),
            //    //                        new SqlParameter("@TblNm", "billproductmaster"),
            //    //                        new SqlParameter("@WhereClause", " where Billtype = 'PR' and isactive = 1 group by productid"));
            //    production = cs.getdataset("select ISNULL(SUM(fQty), 0) AS fqty,proitem from tblfinishedgoodsqty where isactive=1 group by proitem");

            //    //adjuststock = cs.ReturnDataSet("retrivedatawithfield",
            //    //                        new SqlParameter("@Fields", "ISNULL(SUM(adjuststock), 0) AS adjuststock, itemid"),
            //    //                        new SqlParameter("@TblNm", "stockadujestmentitemmaster"),
            //    //                        new SqlParameter("@WhereClause", " where isactive = 1 group by itemid"));
            //    adjuststock = cs.getdataset("select ISNULL(SUM(adjuststock), 0) AS adjuststock, itemid from stockadujestmentitemmaster where isactive = 1 group by itemid");

            //    for (int i = 0; i < product.Rows.Count; i++)
            //    {
            //        DataRow dr = dt.NewRow();
            //        dr["Item Code"] = product.Rows[i]["itemnumber"].ToString();
            //        dr["Name of Item"] = product.Rows[i]["Product_Name"].ToString();
            //        dr["Make/Model"] = product.Rows[i]["companyname"].ToString();

            //        string opening = "0", purchase = "0", sale = "0", salereturn = "0", purchasereturn = "0", possale = "0", pro = "0", ajuststock = "0";

            //        //opening stock
            //        dr["Op. Stock"] = "0";
            //        for (int j = 0; j < opstock.Rows.Count; j++)
            //        {
            //            if (opstock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
            //            {
            //                dr["Op. Stock"] = opstock.Rows[j]["OpStock"].ToString();
            //                opening = opstock.Rows[j]["OpStock"].ToString();
            //                break;
            //            }

            //        }

            //        //purchase stock
            //        dr["Purchase"] = "0";
            //        for (int j = 0; j < purchasestock.Rows.Count; j++)
            //        {
            //            if (purchasestock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
            //            {
            //                dr["Purchase"] = purchasestock.Rows[j]["Purchase"].ToString();
            //                purchase = purchasestock.Rows[j]["Purchase"].ToString();
            //                break;
            //            }
            //        }

            //        //sale stock
            //        dr["Sale"] = "0";
            //        for (int j = 0; j < salestock.Rows.Count; j++)
            //        {
            //            if (salestock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
            //            {
            //                dr["Sale"] = salestock.Rows[j]["Sale"].ToString();
            //                sale = salestock.Rows[j]["Sale"].ToString();
            //                break;
            //            }
            //        }
            //        dr["POSSale"] = "0";
            //        for (int j = 0; j < POSSale.Rows.Count; j++)
            //        {
            //            if (POSSale.Rows[j]["ItemName"].ToString() == product.Rows[i]["Product_Name"].ToString())
            //            {
            //                dr["POSSale"] = POSSale.Rows[j]["POSSale"].ToString();
            //                possale = POSSale.Rows[j]["POSSale"].ToString();
            //                break;
            //            }
            //        }

            //        //Sale Return Stock
            //        dr["Sale Return"] = "0";
            //        for (int j = 0; j < salereturnstock.Rows.Count; j++)
            //        {
            //            if (salereturnstock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
            //            {
            //                dr["Sale Return"] = salereturnstock.Rows[j]["SaleReturn"].ToString();
            //                salereturn = salereturnstock.Rows[j]["SaleReturn"].ToString();
            //                break;
            //            }
            //        }


            //        //Purchase Return Stock
            //        dr["Purchase Return"] = "0";
            //        for (int j = 0; j < purchasereturnstock.Rows.Count; j++)
            //        {
            //            if (purchasereturnstock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
            //            {
            //                dr["Purchase Return"] = purchasereturnstock.Rows[j]["PurchaseReturn"].ToString();
            //                purchasereturn = purchasereturnstock.Rows[j]["PurchaseReturn"].ToString();
            //                break;
            //            }
            //        }
            //        dr["Production"] = "0";
            //        for (int j = 0; j < production.Rows.Count; j++)
            //        {
            //            if (production.Rows[j]["proitem"].ToString() == product.Rows[i]["Product_Name"].ToString())
            //            {
            //                dr["Production"] = production.Rows[j]["fqty"].ToString();
            //                pro = production.Rows[j]["fqty"].ToString();
            //                break;
            //            }
            //        }
            //        //closing
            //        Double closing = Convert.ToDouble(opening) + Convert.ToDouble(purchase.ToString()) - Convert.ToDouble(sale.ToString()) - Convert.ToDouble(possale.ToString()) + Convert.ToDouble(salereturn.ToString()) - Convert.ToDouble(purchasereturn.ToString()) - Convert.ToDouble(pro.ToString());
            //        dr["Closing"] = Math.Round(closing, 2).ToString("N2");
            //        DataTable totalamt = cs.getdataset("select PurchasePrice,SelfVal from ProductPriceMaster where isactive=1 and Productid='" + product.Rows[i]["ProductID"].ToString() + "'");
            //        Double total = closing * (Convert.ToDouble(totalamt.Rows[0]["PurchasePrice"].ToString()) + Convert.ToDouble(totalamt.Rows[0]["SelfVal"].ToString()));
            //        dr["Total Amount"] = total;
            //        //Adjust Stock
            //        dr["Adjust Stock"] = "0";
            //        for (int j = 0; j < adjuststock.Rows.Count; j++)
            //        {
            //            if (adjuststock.Rows[j]["itemid"].ToString() == product.Rows[i]["ProductID"].ToString())
            //            {
            //                dr["Adjust Stock"] = adjuststock.Rows[j]["adjuststock"].ToString();
            //                ajuststock = adjuststock.Rows[j]["adjuststock"].ToString();
            //                break;
            //            }
            //        }
            //        Double finalclosing = Convert.ToDouble(closing) + Convert.ToDouble(ajuststock);
            //        dr["Final Closing"] = Math.Round(finalclosing, 2).ToString("N2");
            //        dt.Rows.Add(dr);

            //    }

            //    ////get opening stock
            //    //opstock = cs.getdataset("SELECT TOP (100) PERCENT p.ProductID AS [Item Code], p.Product_Name AS [Name of Item], c.companyname AS Company, pp.OpStock AS [Op. Packs] FROM dbo.ProductMaster AS p LEFT OUTER JOIN dbo.ProductPriceMaster AS pp ON pp.Productid = p.ProductID INNER JOIN dbo.CompanyMaster AS c ON c.CompanyID = p.CompanyID ORDER BY [Name of Item]");
            //    //salestock = cs.getdataset("SELECT TOP (100) PERCENT p.ProductID AS [Item Code], p.Product_Name AS [Name of Item], c.companyname AS Company, ISNULL(SUM(sb.Pqty), 0) AS sale FROM dbo.ProductMaster AS p LEFT OUTER JOIN dbo.ProductPriceMaster AS pp ON pp.Productid = p.ProductID INNER JOIN dbo.CompanyMaster AS c ON c.CompanyID = p.CompanyID LEFT OUTER JOIN dbo.BillProductMaster AS sb ON sb.Productname = p.Product_Name AND sb.Billtype = 's' AND sb.isactive = 1 GROUP BY p.ProductID, p.Product_Name, c.companyname ORDER BY [Name of Item]");
            //    //purchasestock = cs.getdataset("SELECT TOP (100) PERCENT p.ProductID AS [Item Code], p.Product_Name AS [Name of Item], c.companyname AS Company, ISNULL(SUM(sb.Pqty), 0) AS Purchase FROM dbo.ProductMaster AS p LEFT OUTER JOIN dbo.ProductPriceMaster AS pp ON pp.Productid = p.ProductID INNER JOIN dbo.CompanyMaster AS c ON c.CompanyID = p.CompanyID LEFT OUTER JOIN dbo.BillProductMaster AS sb ON sb.Productname = p.Product_Name AND sb.Billtype = 'P' AND sb.isactive = 1 GROUP BY p.ProductID, p.Product_Name, c.companyname ORDER BY [Name of Item]");
            //    //purchasereturnstock = cs.getdataset("SELECT TOP (100) PERCENT p.ProductID AS [Item Code], p.Product_Name AS [Name of Item], c.companyname AS Company, ISNULL(SUM(sb.Pqty), 0) AS [Purchase Return] FROM dbo.ProductMaster AS p LEFT OUTER JOIN dbo.ProductPriceMaster AS pp ON pp.Productid = p.ProductID INNER JOIN dbo.CompanyMaster AS c ON c.CompanyID = p.CompanyID LEFT OUTER JOIN dbo.BillProductMaster AS sb ON sb.Productname = p.Product_Name AND sb.Billtype = 'PR' AND sb.isactive = 1 GROUP BY p.ProductID, p.Product_Name, c.companyname ORDER BY [Name of Item]");
            //    //salereturnstock = cs.getdataset("SELECT TOP (100) PERCENT p.ProductID AS [Item Code], p.Product_Name AS [Name of Item], c.companyname AS Company, ISNULL(SUM(sb.Pqty), 0) AS [Sale Return] FROM dbo.ProductMaster AS p LEFT OUTER JOIN dbo.ProductPriceMaster AS pp ON pp.Productid = p.ProductID INNER JOIN dbo.CompanyMaster AS c ON c.CompanyID = p.CompanyID LEFT OUTER JOIN dbo.BillProductMaster AS sb ON sb.Productname = p.Product_Name AND sb.Billtype = 'sr' AND sb.isactive = 1 GROUP BY p.ProductID, p.Product_Name, c.companyname ORDER BY [Name of Item]"); 



            //    //for (int i = 0; i < opstock.Rows.Count; i++)
            //    //{
            //    //    try
            //    //    {
            //    //        Double closing = Convert.ToDouble(opstock.Rows[i]["Op. Packs"].ToString()) + Convert.ToDouble(purchasestock.Rows[i]["Purchase"].ToString()) - Convert.ToDouble(salestock.Rows[i]["sale"].ToString()) + Convert.ToDouble(salereturnstock.Rows[i]["Sale Return"].ToString()) - Convert.ToDouble(purchasereturnstock.Rows[i]["Purchase Return"].ToString());
            //    //        dt.Rows.Add(opstock.Rows[i]["Item Code"].ToString(), opstock.Rows[i]["Name of Item"].ToString(), opstock.Rows[i]["Company"].ToString(), opstock.Rows[i]["Op. Packs"].ToString(), purchasestock.Rows[i]["Purchase"].ToString(), salestock.Rows[i]["sale"].ToString(), salereturnstock.Rows[i]["Sale Return"].ToString(), purchasereturnstock.Rows[i]["Purchase Return"].ToString(), Math.Round(closing, 0).ToString());
            //    //    }
            //    //    catch
            //    //    {
            //    //    }
            //    //}



            //    // dt = cs.getdataset("Select * from Stock_Management");
            //    bindgrid();
            //    #endregion
            //}
        }
    }
}
