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
using ClosedXML.Excel;

namespace RamdevSales
{
    public partial class OpStock : Form
    {
        Connection cs = new Connection();
        double optotal, cltotal, cltotalsale;
        DataTable dt, dtconnstring = new DataTable();
        DataTable dt1, dtnewstock = new DataTable();
        DataSet ds = new DataSet();
        OleDbSettings ods = new OleDbSettings();
        private Master master;
        private TabControl tabControl;
        public OpStock()
        {

            InitializeComponent();
           
            

        }

        public OpStock(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }
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


            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void OpStock_Load(object sender, EventArgs e)
        {
            dt = cs.getdataset("select p.productid as [Item Code],p.product_name as [Name of Item],c.companyname as [Company],pp.batchno as [Batch No],p.Unit,pp.opstock as [Op. Packs],pp.saleprice as [Unit Sale Price]  from productmaster p Left join productpricemaster pp on pp.productid=p.productid inner join companymaster c on c.companyid=p.companyid where p.isactive=1 and pp.isactive=1 order by p.product_name");
            bindgrid();

        }
        private void bindgrid()
        {
            optotal = 0;
            dt1 = cs.getdataset("select p.productid as [Item Code],p.product_name as [Name of Item],c.companyname as [Company],pp.batchno as [Batch No],p.Unit,pp.opstock as [Op. Packs],pp.saleprice as [Unit Sale Price]  from productmaster p Left join productpricemaster pp on pp.productid=p.productid inner join companymaster c on c.companyid=p.companyid where p.isactive=1 and pp.isactive=1 order by p.product_name");
           // dt = dt1;
            grdstock.DataSource = dt1;
            grdstock.Columns[0].Width = 49;
            grdstock.Columns[1].Width = 400;
            grdstock.Columns[0].ReadOnly = true;
            grdstock.Columns[1].ReadOnly = true;
            grdstock.Columns[2].Width = 180;
            grdstock.Columns[2].ReadOnly = true;
            grdstock.Columns[3].ReadOnly = true;
            grdstock.Columns[4].ReadOnly = true;
            grdstock.Columns[6].Width = 180;
            grdstock.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
           // grdstock.Columns[5].ControlStyle.ForeColor = System.Drawing.Color.Red;
            grdstock.Columns[5].DefaultCellStyle.ForeColor = Color.Red;
            openingtotal(dt1);
        }
        public void openingtotal(DataTable dt)
        {
            optotal = 0;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        optotal += Convert.ToDouble(dt.Rows[i][5].ToString());
                       // optotal += Convert.ToDouble(dt.Rows[i][6].ToString());
                    }
                    catch
                    {
                    }
                }
                //txttotamt.Text = cltotal.ToString("N2");
            }


        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                if (grdstock.Rows.Count > 0)
                {
                    for (int i = 0; i < grdstock.Rows.Count; i++)
                    {
                        try
                        {

                            if (Convert.ToDouble(grdstock.Rows[i].Cells[5].Value) != Convert.ToDouble(dt.Rows[i][5].ToString()) || Convert.ToDouble(grdstock.Rows[i].Cells[6].Value) != Convert.ToDouble(dt.Rows[i][6].ToString()))
                            {
                                cs.execute("update ProductPriceMaster set OpStock='" + grdstock.Rows[i].Cells[5].Value + "',saleprice='" + grdstock.Rows[i].Cells[6].Value + "' where Productid='" + grdstock.Rows[i].Cells[0].Value + "' and batchno='" + grdstock.Rows[i].Cells[3].Value + "'");
                            }

                        }
                        catch (Exception ex)
                        {
                            // MessageBox.Show("Not Updated:" + ex.Message);
                        }
                    }
                    MessageBox.Show("Opening Stock Update Successfully");

                }
            }
            finally
            {
                this.Enabled = true;
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }

      
        private void grdstock_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            total();
        }
        public void total()
        {
            cltotal = 0;
            cltotalsale = 0;
            if(grdstock.Rows.Count>0)
            {
                for (int i = 0; i < grdstock.Rows.Count; i++)
                {
                    try
                    {
                        cltotal += Convert.ToDouble(grdstock.Rows[i].Cells[5].Value);
                        cltotalsale += Convert.ToDouble(grdstock.Rows[i].Cells[6].Value);
                    }
                    catch
                    {
                    }
                }
                txttotamt.Text = cltotal.ToString("N2");
                txtunitsalepricetotal.Text = cltotalsale.ToString("N2");
            }
                
            
        }
        private void grdstock_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (grdstock.CurrentCell.ColumnIndex == 5 || grdstock.CurrentCell.ColumnIndex == 6) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)
            && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        public DataTable ReturnDataSet(string spName,SqlConnection con, params SqlParameter[] cmdparam)
        {
            try
            {
              //  getcon();
                con.Open();
               SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;
                cmd.Connection = con;
                if (System.Convert.IsDBNull(cmdparam) == false)
                {
                    SqlParameter parm = null;
                    foreach (SqlParameter parm_loopVariable in cmdparam)
                    {
                        parm = parm_loopVariable;
                        cmd.Parameters.Add(parm);
                    }
                }
               SqlDataAdapter da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return dt;
            }
            catch
            {
                dt = null;
                return dt;
            }

        }
        public DataTable getdataset(string sql, SqlConnection con)
        {
            try
            {

                dt = new DataTable();
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }

            catch
            {
                con.Close();
                return null;
            }
            finally
            {
                con.Close();
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            loadoldyearstock();
            total();
        //    dt = cs.getdataset("select p.productid as [Item Code],p.product_name as [Name of Item],c.companyname as [Company],pp.batchno as [Batch No],p.Unit,pp.opstock as [Op. Packs],pp.saleprice as [Unit Sale Price]  from productmaster p Left join productpricemaster pp on pp.productid=p.productid inner join companymaster c on c.companyid=p.companyid order by p.product_name");
        //    bindgrid();
        }
        public void loadoldyearstock()
        {
            try
            {
                ds = ods.getdata("Select * from SQLSetting");
                dtconnstring = ds.Tables[0];
                string oldconnectionstring = dtconnstring.Rows[0]["OT7"].ToString();
                if (!string.IsNullOrEmpty(oldconnectionstring))
                {
                    SqlConnection scon = new SqlConnection(oldconnectionstring);
                    DataTable product = new DataTable();
                    DataTable opstock = new DataTable();
                    DataTable salestock = new DataTable();
                    DataTable purchasestock = new DataTable();
                    DataTable salereturnstock = new DataTable();
                    DataTable purchasereturnstock = new DataTable();
                    DataTable POSSale = new DataTable();
                    DataTable production = new DataTable();
                    DataTable adjuststock = new DataTable();

                    dt = new DataTable();
                    dt.Columns.Add("Item Code");
                    dt.Columns.Add("Name of Item");
                    dt.Columns.Add("Company");
                    dt.Columns.Add("batch");
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
                    product = cs.getdataset("select p.*,c.companyname from productmaster p inner join (select productid, max(saleprice) SalePrice,max(MRP) MRP, max(purchaseprice) PurchasePrice from productpricemaster group by productid) as pp on pp.productid=p.productid inner join companymaster c on c.companyid=p.companyid where p.isactive=1 order by p.product_name", scon);
                    //product = cs.ReturnDataSet("retrivedatawithfield",
                    //    new SqlParameter("@Fields", "p.*,c.companyname"), 
                    //    new SqlParameter("@TblNm", "productmaster p"),
                    //    new SqlParameter("@WhereClause", "inner join (select productid, max(saleprice) SalePrice,max(MRP) MRP, max(purchaseprice) PurchasePrice from productpricemaster group by productid) as pp on pp.productid=p.productid inner join companymaster c on c.companyid=p.companyid where p.isactive=1 order by p.product_name"));
                    POSSale = cs.getdataset("select ISNULL(SUM(Qty), 0) AS POSSale,ItemName,batchno from BillPOSProductMaster where isactive=1 group by ItemName,batchno", scon);
                    opstock = cs.getdataset("select * from productpricemaster where isactive=1", scon);
                    //opstock = cs.ReturnDataSet("retrivedatawithfield",
                    //       new SqlParameter("@Fields", "*"),
                    //       new SqlParameter("@TblNm", "productpricemaster"),
                    //       new SqlParameter("@WhereClause", " where isactive=1"));

                    purchasestock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS Purchase, productid,batch from billproductmaster where Billtype = 'P' and isactive = 1 group by productid,batch", scon);
                    //purchasestock = cs.ReturnDataSet("retrivedatawithfield",
                    //                       new SqlParameter("@Fields", "ISNULL(SUM(Pqty), 0) AS Purchase, productid"),
                    //                       new SqlParameter("@TblNm", "billproductmaster"),
                    //                       new SqlParameter("@WhereClause", " where Billtype = 'P' and isactive = 1 group by productid"));

                    salestock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS Sale, productid,batch from billproductmaster where Billtype = 'S' and isactive = 1 group by productid,batch", scon);
                    //salestock = cs.ReturnDataSet("retrivedatawithfield",
                    //                       new SqlParameter("@Fields", "ISNULL(SUM(Pqty), 0) AS Sale, productid"),
                    //                       new SqlParameter("@TblNm", "billproductmaster"),
                    //                       new SqlParameter("@WhereClause", " where Billtype = 'S' and isactive = 1 group by productid"));

                    salereturnstock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS SaleReturn, productid,batch from billproductmaster where Billtype = 'SR' and isactive = 1 group by productid,batch", scon);
                    //salereturnstock = cs.ReturnDataSet("retrivedatawithfield",
                    //                       new SqlParameter("@Fields", "ISNULL(SUM(Pqty), 0) AS SaleReturn, productid"),
                    //                       new SqlParameter("@TblNm", "billproductmaster"),
                    //                       new SqlParameter("@WhereClause", " where Billtype = 'SR' and isactive = 1 group by productid"));


                    purchasereturnstock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS PurchaseReturn, productid,batch from billproductmaster where Billtype = 'PR' and isactive = 1 group by productid,batch", scon);
                    //purchasereturnstock = cs.ReturnDataSet("retrivedatawithfield",
                    //                        new SqlParameter("@Fields", "ISNULL(SUM(Pqty), 0) AS PurchaseReturn, productid"),
                    //                        new SqlParameter("@TblNm", "billproductmaster"),
                    //                        new SqlParameter("@WhereClause", " where Billtype = 'PR' and isactive = 1 group by productid"));
                    // production = cs.getdataset("select ISNULL(SUM(fQty), 0) AS fqty,proitem from tblfinishedgoodsqty where isactive=1 group by proitem");
                    adjuststock = cs.getdataset("select ISNULL(SUM(adjuststock), 0) AS adjuststock, itemid from stockadujestmentitemmaster where isactive = 1 group by itemid", scon);

                    for (int i = 0; i < product.Rows.Count; i++)
                    {


                        DataTable isbatch = cs.getdataset("select * from ProductPriceMaster where isactive=1 and Productid='" + product.Rows[i]["ProductID"].ToString() + "'", scon);
                        if (isbatch.Rows.Count > 0)
                        {

                            for (int k = 0; k < isbatch.Rows.Count; k++)
                            {
                                string opening = "0", purchase = "0", sale = "0", salereturn = "0", purchasereturn = "0", possale = "0", pro = "0", ajuststock = "0";
                                DataRow dr = dt.NewRow();
                                dr["Item Code"] = product.Rows[i]["ProductID"].ToString();
                                dr["Name of Item"] = product.Rows[i]["Product_Name"].ToString();
                                dr["Company"] = product.Rows[i]["companyname"].ToString();
                                dr["batch"] = isbatch.Rows[k]["batchno"].ToString();

                                //opening stock
                                dr["Op. Stock"] = "0";
                                for (int j = 0; j < opstock.Rows.Count; j++)
                                {
                                    if (opstock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                    {
                                        if (opstock.Rows[j]["batchno"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                        {
                                            dr["Op. Stock"] = opstock.Rows[j]["OpStock"].ToString();
                                            opening = opstock.Rows[j]["OpStock"].ToString();
                                            break;
                                        }
                                    }

                                }


                                //purchase stock
                                dr["Purchase"] = "0";
                                for (int j = 0; j < purchasestock.Rows.Count; j++)
                                {
                                    if (purchasestock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                    {
                                        if (purchasestock.Rows[j]["batch"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                        {
                                            dr["Purchase"] = purchasestock.Rows[j]["Purchase"].ToString();
                                            purchase = purchasestock.Rows[j]["Purchase"].ToString();
                                            break;
                                        }
                                    }
                                }


                                //sale stock
                                dr["Sale"] = "0";
                                for (int j = 0; j < salestock.Rows.Count; j++)
                                {
                                    if (salestock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                    {
                                        if (salestock.Rows[j]["batch"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                        {
                                            dr["Sale"] = salestock.Rows[j]["Sale"].ToString();
                                            sale = salestock.Rows[j]["Sale"].ToString();
                                            break;
                                        }
                                    }
                                }


                                //pos sttock
                                dr["POSSale"] = "0";
                                for (int j = 0; j < POSSale.Rows.Count; j++)
                                {
                                    if (POSSale.Rows[j]["ItemName"].ToString() == product.Rows[i]["Product_Name"].ToString())
                                    {
                                        if (POSSale.Rows[j]["batchno"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                        {
                                            dr["POSSale"] = POSSale.Rows[j]["POSSale"].ToString();
                                            possale = POSSale.Rows[j]["POSSale"].ToString();
                                            break;
                                        }
                                    }
                                }


                                //Sale Return Stock
                                dr["Sale Return"] = "0";
                                for (int j = 0; j < salereturnstock.Rows.Count; j++)
                                {
                                    if (salereturnstock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                    {
                                        if (salereturnstock.Rows[j]["batch"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                        {
                                            dr["Sale Return"] = salereturnstock.Rows[j]["SaleReturn"].ToString();
                                            salereturn = salereturnstock.Rows[j]["SaleReturn"].ToString();
                                            break;
                                        }
                                    }
                                }

                                //Purchase Return Stock
                                dr["Purchase Return"] = "0";
                                for (int j = 0; j < purchasereturnstock.Rows.Count; j++)
                                {
                                    if (purchasereturnstock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                    {
                                        if (purchasereturnstock.Rows[j]["batch"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                        {
                                            dr["Purchase Return"] = purchasereturnstock.Rows[j]["PurchaseReturn"].ToString();
                                            purchasereturn = purchasereturnstock.Rows[j]["PurchaseReturn"].ToString();
                                            break;
                                        }
                                    }
                                }
                                dr["Production"] = "0";
                                //for (int j = 0; j < production.Rows.Count; j++)
                                //{
                                //    if (production.Rows[j]["proitem"].ToString() == product.Rows[i]["Product_Name"].ToString())
                                //    {
                                //        if (production.Rows[j]["batch"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                //        {
                                //            dr["Production"] = production.Rows[j]["fqty"].ToString();
                                //            pro = production.Rows[j]["fqty"].ToString();
                                //            break;
                                //        }
                                //    }
                                //}

                                //closing
                                Double closing = Convert.ToDouble(opening) + Convert.ToDouble(purchase.ToString()) - Convert.ToDouble(sale.ToString()) - Convert.ToDouble(possale.ToString()) + Convert.ToDouble(salereturn.ToString()) - Convert.ToDouble(purchasereturn.ToString()) - Convert.ToDouble(pro.ToString());
                                dr["Closing"] = Math.Round(closing, 2).ToString("N2");
                                DataTable totalamt = cs.getdataset("select PurchasePrice,SelfVal from ProductPriceMaster where isactive=1 and Productid='" + product.Rows[i]["ProductID"].ToString() + "' and batchno='" + isbatch.Rows[k]["batchno"].ToString() + "'", scon);
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

                                dtnewstock.Columns.Add("Item Code");
                                dtnewstock.Columns.Add("Name of Item");
                                dtnewstock.Columns.Add("Company");
                                dtnewstock.Columns.Add("Batch No");
                                dtnewstock.Columns.Add("Unit");
                                dtnewstock.Columns.Add("Op. Packs");
                                dtnewstock.Columns.Add("Unit Sale Price");

                                dtnewstock.Rows.Add(product.Rows[i]["ProductID"].ToString(), product.Rows[i]["Product_Name"].ToString(), product.Rows[i]["companyname"].ToString(), isbatch.Rows[k]["batchno"].ToString(), product.Rows[i]["Unit"].ToString(), finalclosing, isbatch.Rows[k]["saleprice"].ToString());

                                //  cs.execute("update productpricemaster set opstock='" + Math.Round(finalclosing, 2).ToString() + "' where productid=" + product.Rows[i]["ProductID"].ToString());
                            }
                        }

                    }
                    grdstock.DataSource = dtnewstock;
                    grdstock.Columns[0].Width = 49;
                    grdstock.Columns[1].Width = 400;
                    grdstock.Columns[0].ReadOnly = true;
                    grdstock.Columns[1].ReadOnly = true;
                    grdstock.Columns[2].Width = 180;
                    grdstock.Columns[2].ReadOnly = true;
                    grdstock.Columns[3].ReadOnly = true;
                    grdstock.Columns[4].ReadOnly = true;
                    grdstock.Columns[6].Width = 180;
                    grdstock.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    // grdstock.Columns[5].ControlStyle.ForeColor = System.Drawing.Color.Red;
                    grdstock.Columns[5].DefaultCellStyle.ForeColor = Color.Red;
                    openingtotal(dtnewstock);
                }
            }
            catch
            {
            }
        }
        private void Loadpastyear()
        {
            try
            {
                string connstr = cs.ExecuteScalar("select path from company");
                if (connstr == "")
                {
                }
                else
                {


                    SqlConnection con = new SqlConnection("Data Source=184.168.47.17;Initial Catalog=" + connstr + ";User ID=" + connstr + ";Password=Billing1!");

                    DataTable product = new DataTable();
                    DataTable opstock = new DataTable();
                    DataTable salestock = new DataTable();
                    DataTable purchasestock = new DataTable();
                    DataTable salereturnstock = new DataTable();
                    DataTable purchasereturnstock = new DataTable();

                    //dt.Columns.Add("Item Code");
                    //dt.Columns.Add("Name of Item");
                    //dt.Columns.Add("Company");
                    //dt.Columns.Add("Op. Stock");
                    //dt.Columns.Add("Purchase");
                    //dt.Columns.Add("Sale");
                    //dt.Columns.Add("Sale Return");
                    //dt.Columns.Add("Purchase Return");
                    //dt.Columns.Add("Closing");

                    product = cs.getdataset("select p.*,c.companyname from productmaster p inner join companymaster c on c.companyid=p.companyid order by p.product_name");
                    //product = ReturnDataSet("retrivedatawithfield",con,
                    //                    new SqlParameter("@Fields", "p.*,c.companyname"),
                    //                    new SqlParameter("@TblNm", "productmaster p"),
                    //                    new SqlParameter("@WhereClause", " inner join companymaster c on c.companyid=p.companyid order by p.product_name"));

                    opstock = cs.getdataset("select * from productpricemaster ");
                    //opstock = ReturnDataSet("retrivedatawithfield", con,
                    //       new SqlParameter("@Fields", "*"),
                    //       new SqlParameter("@TblNm", "productpricemaster"),
                    //       new SqlParameter("@WhereClause", ""));

                    purchasestock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS Purchase, productid from billproductmaster where Billtype = 'P' and isactive = 1 group by productid");
                    //purchasestock = ReturnDataSet("retrivedatawithfield", con,
                    //                       new SqlParameter("@Fields", "ISNULL(SUM(Pqty), 0) AS Purchase, productid"),
                    //                       new SqlParameter("@TblNm", "billproductmaster"),
                    //                       new SqlParameter("@WhereClause", " where Billtype = 'P' and isactive = 1 group by productid"));

                    salestock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS Sale, productid from billproductmaster where Billtype = 'S' and isactive = 1 group by productid");
                    //salestock = ReturnDataSet("retrivedatawithfield", con,
                    //                       new SqlParameter("@Fields", "ISNULL(SUM(Pqty), 0) AS Sale, productid"),
                    //                       new SqlParameter("@TblNm", "billproductmaster"),
                    //                       new SqlParameter("@WhereClause", " where Billtype = 'S' and isactive = 1 group by productid"));

                    salereturnstock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS SaleReturn, productid from billproductmaster where Billtype = 'SR' and isactive = 1 group by productid");
                    //salereturnstock = ReturnDataSet("retrivedatawithfield", con,
                    //                       new SqlParameter("@Fields", "ISNULL(SUM(Pqty), 0) AS SaleReturn, productid"),
                    //                       new SqlParameter("@TblNm", "billproductmaster"),
                    //                       new SqlParameter("@WhereClause", " where Billtype = 'SR' and isactive = 1 group by productid"));


                    purchasereturnstock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS PurchaseReturn, productid from billproductmaster where Billtype = 'PR' and isactive = 1 group by productid");
                    //purchasereturnstock = ReturnDataSet("retrivedatawithfield", con,
                    //                        new SqlParameter("@Fields", "ISNULL(SUM(Pqty), 0) AS PurchaseReturn, productid"),
                    //                        new SqlParameter("@TblNm", "billproductmaster"),
                    //                        new SqlParameter("@WhereClause", " where Billtype = 'PR' and isactive = 1 group by productid"));



                    for (int i = 0; i < product.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        //dr["Item Code"] = product.Rows[i]["ProductID"].ToString();
                        //dr["Name of Item"] = product.Rows[i]["Product_Name"].ToString();
                        //dr["Company"] = product.Rows[i]["companyname"].ToString();
                        string productid = product.Rows[i]["ProductID"].ToString();
                        string opening = "0", purchase = "0", sale = "0", salereturn = "0", purchasereturn = "0";

                        //opening stock
                        //dr["Op. Stock"] = "0";
                        for (int j = 0; j < opstock.Rows.Count; j++)
                        {
                            
                            if (opstock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                            {
                                //dr["Op. Stock"] = opstock.Rows[j]["OpStock"].ToString();
                                opening = opstock.Rows[j]["OpStock"].ToString();
                                break;
                            }

                        }

                        //purchase stock
                        //dr["Purchase"] = "0";
                        for (int j = 0; j < purchasestock.Rows.Count; j++)
                        {
                            if (purchasestock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                            {
                                //dr["Purchase"] = purchasestock.Rows[j]["Purchase"].ToString();
                                purchase = purchasestock.Rows[j]["Purchase"].ToString();
                                break;
                            }
                        }

                        //sale stock
                        //dr["Sale"] = "0";
                        for (int j = 0; j < salestock.Rows.Count; j++)
                        {
                            if (salestock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                            {
                                //dr["Sale"] = salestock.Rows[j]["Sale"].ToString();
                                sale = salestock.Rows[j]["Sale"].ToString();
                                break;
                            }
                        }


                        //Sale Return Stock
                        //dr["Sale Return"] = "0";
                        for (int j = 0; j < salereturnstock.Rows.Count; j++)
                        {
                            if (salereturnstock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                            {
                                //dr["Sale Return"] = salereturnstock.Rows[j]["SaleReturn"].ToString();
                                salereturn = salereturnstock.Rows[j]["SaleReturn"].ToString();
                                break;
                            }
                        }


                        //Purchase Return Stock
                        //dr["Purchase Return"] = "0";
                        for (int j = 0; j < purchasereturnstock.Rows.Count; j++)
                        {
                            if (purchasereturnstock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                            {
                                //dr["Purchase Return"] = purchasereturnstock.Rows[j]["PurchaseReturn"].ToString();
                                purchasereturn = purchasereturnstock.Rows[j]["PurchaseReturn"].ToString();
                                break;
                            }
                        }

                        //closing
                        Double closing = Convert.ToDouble(opening) + Convert.ToDouble(purchase.ToString()) - Convert.ToDouble(sale.ToString()) + Convert.ToDouble(salereturn.ToString()) - Convert.ToDouble(purchasereturn.ToString());
                        cs.execute("update productpricemaster set opstock='" + Math.Round(closing, 2).ToString() + "' where productid=" + productid);
                      //  dr["Closing"] = Math.Round(closing, 2).ToString("N2");
                      //  dt.Rows.Add(dr);
                    }




                    //for (int i = 0; i < opstock.Rows.Count; i++)
                    //{
                    //    try
                    //    {
                    //        Double closing = Convert.ToDouble(opstock.Rows[i]["Op. Packs"].ToString()) + Convert.ToDouble(purchasestock.Rows[i]["Purchase"].ToString()) - Convert.ToDouble(salestock.Rows[i]["sale"].ToString()) + Convert.ToDouble(salereturnstock.Rows[i]["Sale Return"].ToString()) - Convert.ToDouble(purchasereturnstock.Rows[i]["Purchase Return"].ToString());
                            
                    //       // grdstock.Rows[i].Cells[5].Value = Math.Round(closing, 2).ToString();
                    //        cs.execute("update productpricemaster set opstock='" + Math.Round(closing, 2).ToString() + "' where productid=" + opstock.Rows[i][0].ToString());

                    //    }
                    //    catch
                    //    {
                    //    }
                    //}
                }
            }
            catch
            {
            }
        }

        
        private void grdstock_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

        private void btndelete_MouseEnter(object sender, EventArgs e)
        {
            btndelete.UseVisualStyleBackColor = false;
            btndelete.BackColor = Color.FromArgb(248, 152, 94);
            btndelete.ForeColor = Color.White;
        }

        private void btndelete_MouseLeave(object sender, EventArgs e)
        {
            btndelete.UseVisualStyleBackColor = true;
            btndelete.BackColor = Color.FromArgb(51, 153, 255);
            btndelete.ForeColor = Color.White;
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

        private void btndelete_Enter(object sender, EventArgs e)
        {
            btndelete.UseVisualStyleBackColor = false;
            btndelete.BackColor = Color.FromArgb(248, 152, 94);
            btndelete.ForeColor = Color.White;
        }

        private void btndelete_Leave(object sender, EventArgs e)
        {
            btndelete.UseVisualStyleBackColor = true;
            btndelete.BackColor = Color.FromArgb(51, 153, 255);
            btndelete.ForeColor = Color.White;
        }

        private void btnexcel_Click(object sender, EventArgs e)
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
                            wb.Worksheets.Add(dt1, "OP_Stock");
                            // wb.Worksheets.Add(dt1, "ItemPrice");
                            wb.SaveAs(folderPath + "OP_Stock_Management" + DateTimeName + ".xlsx");
                        }
                        MessageBox.Show("Export Data Sucessfully");
                        DialogResult dr = MessageBox.Show("Do you want to Open Document?", "OP_Stock Management", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(folderPath + "OP_Stock_Management" + DateTimeName + ".xlsx");
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

        private void btnexcel_Enter(object sender, EventArgs e)
        {
            btnexcel.UseVisualStyleBackColor = false;
            btnexcel.BackColor = System.Drawing.Color.FromArgb(118, 72, 233);
            btnexcel.ForeColor = System.Drawing.Color.White;
        }

        private void btnexcel_Leave(object sender, EventArgs e)
        {
            btnexcel.UseVisualStyleBackColor = true;
            btnexcel.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnexcel.ForeColor = System.Drawing.Color.White;
        }

        private void btnexcel_MouseEnter(object sender, EventArgs e)
        {
            btnexcel.UseVisualStyleBackColor = false;
            btnexcel.BackColor = System.Drawing.Color.FromArgb(118, 72, 233);
            btnexcel.ForeColor = System.Drawing.Color.White;
        }

        private void btnexcel_MouseLeave(object sender, EventArgs e)
        {
            btnexcel.UseVisualStyleBackColor = true;
            btnexcel.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnexcel.ForeColor = System.Drawing.Color.White;
        }
       
    }
}
