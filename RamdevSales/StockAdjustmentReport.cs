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
using LoggingFramework;
using System.Web.UI.WebControls;

namespace RamdevSales
{
    public partial class StockAdjustmentReport : Form
    {
        //  Connection con = new Connection();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection cs = new Connection();
        double optotal, d1, d2, d3, d4, d5, d6, d7, d8, d9;
        public static string iid = "";
        DataTable dt = new DataTable();
        private Master master;
        private TabControl tabControl;
        DataTable userrights = new DataTable();
        string updateid = "";
        public StockAdjustmentReport()
        {
            InitializeComponent();
            //grdstock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public StockAdjustmentReport(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
          //  grdstock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.master = master;
            this.tabControl = tabControl;
        }

        private void StockReport_Load(object sender, EventArgs e)
        {
            buttonclicked = 0;
            binddrop();
            LoadData();
        }
        int columnindex;
        public void LoadData()
        {
            try
            {
                if (buttonclicked == 0)
                {
                    if (updateid == "" || updateid == null)
                    {
                        grdstock.DataSource = null;
                        grdstock.Columns.Clear();
                    }
                }
                if (updateid == "")
                {
                    DataGridViewLinkColumn bcol = new DataGridViewLinkColumn();
                    bcol.HeaderText = "Current Stock";
                    bcol.Text = "View";
                    bcol.Name = "CurrStock";
                    bcol.UseColumnTextForLinkValue = true;
                    grdstock.Columns.Add(bcol);
                }
                userrights = cs.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");

                if (cnt == 0)
                {
                    #region ForInsertStockReportLoad

                    DTPFrom123.CustomFormat = Master.dateformate;
                    DataTable product = new DataTable();
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
                    //dt.Columns.Add("Make/Model");
                    //dt.Columns.Add("batch");
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
                    //dt.Columns.Add("Remarks");
                    string columnname = "";

                    if (txtsearch.Text == "companyname")
                    {
                        columnname = "companyname";
                        columnindex = 4;
                    }
                    else if (txtsearch.Text == "GroupName")
                    {
                        columnname = "GroupName";
                    }
                    else if (txtsearch.Text == "Product_Name")
                    {
                        columnname = "Product_Name";
                        columnindex = 3;
                    }
                    else if (txtsearch.Text == "itemnumber")
                    {
                        columnname = "itemnumber";
                        columnindex = 2;
                    }

                    //get productmaster
                    if (txtser.Text == "")
                    {


                        if (buttonclicked == 0)
                        {
                            dt = cs.getdataset("select p.ProductID as Id,p.itemnumber as [itemnumber],p.Product_Name as [Name of item],c.companyname as [companyname],pp.Batchno as Batch,0 as [Adjust Stock],'' as Remark from productmaster p inner join (select productid, Batchno from productpricemaster where isactive=1) as pp on pp.productid=p.productid inner join companymaster c on c.companyid=p.companyid where p.isactive=1 order by p.product_name");
                            bindgrid();
                            
                        }
                        else
                        {
                            AllGridviewByEmpId("");
                        }
                    }
                    else
                    {
                        if (txtsearch.SelectedIndex == 0)
                        {
                            MessageBox.Show("Select Column Name");
                            return;
                        }
                        else
                        {
                            string query =  txtser.Text.ToUpper();
                            FilterGridviewByEmpId(query,columnindex);
                            //foreach (DataGridViewRow row in grdstock.Rows)
                            //{
                            //    CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[grdstock.DataSource];
                            //    currencyManager1.SuspendBinding();
                            //    row.Visible = false;
                            //    currencyManager1.ResumeBinding();
                            //}

                            //if (query.Trim() != "")
                            //{
                            //    foreach (DataGridViewRow row in grdstock.Rows)
                            //    {
                            //        foreach (TableCell cell in row.Cells)
                            //        {
                            //            string cellText = cell.Text;
                            //            if (cell.Text == "" && cell.Controls.Count > 0)
                            //            {
                            //                cellText = ((System.Web.UI.DataBoundLiteralControl)cell.Controls[0]).Text;
                            //            }
                            //            if (cellText.IndexOf(query) > -1)
                            //            {
                            //                row.Visible = true;
                            //                break;
                            //            }
                            //        }
                            //    }
                            //}
                            //else
                            //{
                            //    foreach (GridViewRow row in grdstock.Rows)
                            //    {
                            //        row.Visible = true;
                            //    }
                            //}
                          //  dt = cs.getdataset("select p.ProductID as Id,p.itemnumber as [Item Code],p.Product_Name as [Name of item],c.companyname as [Make/Model],pp.Batchno as Batch,0 as [Adjust Stock],'' as Remark from productmaster p inner join (select productid, Batchno from productpricemaster where isactive=1) as pp on pp.productid=p.productid inner join companymaster c on c.companyid=p.companyid where p.isactive=1 and " + columnname + " like '%" + txtser.Text.Replace(" ", "%") + "%' order by p.product_name");
                          //  bindgrid();
                          //  FlagForStock = 1;
                        }
                    }
                    //POSSale = cs.getdataset("select ISNULL(SUM(Qty), 0) AS POSSale,ItemName,batchno from BillPOSProductMaster where isactive=1 group by ItemName,batchno");
                    //opstock = cs.getdataset("select * from productpricemaster where isactive=1");

                    //purchasestock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS Purchase, productid,batch from billproductmaster where Billtype = 'P' and isactive = 1 group by productid,batch");

                    //salestock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS Sale, productid,batch from billproductmaster where Billtype = 'S' and isactive = 1 group by productid,batch");
                    //salereturnstock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS SaleReturn, productid,batch from billproductmaster where Billtype = 'SR' and isactive = 1 group by productid,batch");

                    //purchasereturnstock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS PurchaseReturn, productid,batch from billproductmaster where Billtype = 'PR' and isactive = 1 group by productid,batch");
                    //production = cs.getdataset("select ISNULL(SUM(fQty), 0) AS fqty,proitem from tblfinishedgoodsqty where isactive=1 group by proitem");
                    //adjuststock = cs.getdataset("select ISNULL(SUM(adjuststock), 0) AS adjuststock, itemid,batch from stockadujestmentitemmaster where stockdate<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by itemid,batch");

                    //for (int i = 0; i < product.Rows.Count; i++)
                    //{
                    //    DataTable isbatch = cs.getdataset("select * from ProductPriceMaster where isactive=1 and Productid='" + product.Rows[i]["ProductID"].ToString() + "'");
                    //    if (isbatch.Rows.Count > 0)
                    //    {
                    //        for (int k = 0; k < isbatch.Rows.Count; k++)
                    //        {
                    //            if(i== 6158)
                    //            {
                    //            }
                    //            DataRow dr = dt.NewRow();
                    //            dr["ID"] = product.Rows[i]["ProductID"].ToString();
                    //            dr["Item Code"] = product.Rows[i]["itemnumber"].ToString();
                    //            dr["Name of Item"] = product.Rows[i]["Product_Name"].ToString();
                    //            dr["Make/Model"] = product.Rows[i]["companyname"].ToString();
                    //            dr["batch"] = isbatch.Rows[k]["batchno"].ToString();
                    //            dr["Adjust Stock"] = "0";
                    //            dt.Rows.Add(dr);


                    //        }

                    //    }
                    //}



                    #endregion
                }
                else
                {
                    #region ForUpdateStockReportLoad

                    //if (userrights.Rows.Count > 0)
                    //{
                    //    if (userrights.Rows[28]["u"].ToString() == "False")
                    //    {
                    //        btnsubmit.Enabled = false;
                    //    }
                    //    if (userrights.Rows[28]["d"].ToString() == "False")
                    //    {
                    //        btndelete.Enabled = false;
                    //    }
                    //    if (userrights.Rows[28]["p"].ToString() == "False")
                    //    {
                    //        BtnPayment.Enabled = false;
                    //        btnprint.Enabled = false;
                    //    }
                    //}
                    #endregion
                }
            }
            catch
            {
            }
        }
        private void AllGridviewByEmpId(string query)
        {
            SetAllRowsVisible(grdstock, true);
            //foreach (DataGridViewRow row in grdstock.Rows)
            //{
            //    if (row.Cells[2].Value.ToString().Equals(query))
            //    {
            //        CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[grdstock.DataSource];
            //        currencyManager1.SuspendBinding();
            //        row.Visible = false;
            //        currencyManager1.ResumeBinding();

            //    }
            //}
        }
        private void FilterGridviewByEmpId(string query,int index)
        {
            SetAllRowsVisible(grdstock, false);
            foreach (DataGridViewRow row in grdstock.Rows)
            {
                if (row.Cells[index].Value.ToString().Contains(query))
                {
                    CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[grdstock.DataSource];
                    currencyManager1.SuspendBinding();
                    row.Visible = true;
                    currencyManager1.ResumeBinding();
                   
                }
            }
        }

        private void SetAllRowsVisible(DataGridView grdstock, bool p)
        {
            foreach (DataGridViewRow row in grdstock.Rows)
            {
                CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[grdstock.DataSource];
                currencyManager1.SuspendBinding();
                row.Visible = p;
                currencyManager1.ResumeBinding();
                
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
            try
            {
                grdstock.DataSource = dt;
                grdstock.Columns[0].Width = 49;
                grdstock.Columns[1].Width = 0;
                grdstock.Columns[2].Width = 100;
                grdstock.Columns[3].Width = 300;
                grdstock.Columns[4].Width = 100;
                grdstock.Columns[5].Width = 100;
                grdstock.Columns[6].Width = 100;
                grdstock.Columns[7].Width = 300;
              
                //grdstock.Columns[0].ReadOnly = true;
                grdstock.Columns[1].ReadOnly = true;
                grdstock.Columns[2].ReadOnly = true;
                grdstock.Columns[3].ReadOnly = true;
                grdstock.Columns[4].ReadOnly = true;

                grdstock.Columns[1].Visible = false;
                grdstock.Columns[2].DisplayIndex = 0;
                grdstock.Columns[3].DisplayIndex = 2;
                grdstock.Columns[4].DisplayIndex = 3;
                grdstock.Columns[5].DisplayIndex = 4;
                grdstock.Columns[0].DisplayIndex = 5;
                grdstock.Columns[6].DisplayIndex = 6;
                grdstock.Columns[7].DisplayIndex = 7;

            }
            catch (Exception excp)
            {

            }
            //grdstock.ReadOnly = true;
            // grdstock.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

         //   openingtotal();
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
            //try
            //{
            //    this.Enabled = false;
            //    // iid = grdstock.Rows[grdstock.e.Index].SubItems[0].Text;
            //    iid = grdstock.CurrentRow.Cells[1].Value.ToString();
            //    ItemWiseStock dlg = new ItemWiseStock(master, tabControl);
            //    dlg.getitemname(1, iid);
            //    master.AddNewTab(dlg);
            //    dlg.Show();
            //}
            //finally
            //{
            //    this.Enabled = true;
            //}
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
                        string ItemName = "", company = "", Itemcode = "";
                        Itemcode = dt.Rows[i][0].ToString();
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
                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34,T35,T36)VALUES";
                        qry += "('" + j++ + "','" + dt1.Rows[0][0].ToString() + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][2].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + dt1.Rows[0][11].ToString() + "','" + dt1.Rows[0][12].ToString() + "','" + dt1.Rows[0][13].ToString() + "','" + ItemName + "','" + company + "','" + Opstock + "','" + Purchase + "','" + Sale + "','" + SaleReturn + "','" + PurchaseReturn + "','" + ClosingStock + "','" + total + "','" + txt1.Text + "','" + txt2.Text + "','" + txt3.Text + "','" + txt4.Text + "','" + txt5.Text + "','" + txt6.Text + "','" + txt7.Text + "','" + txt8.Text + "','" + possale + "','" + production + "','" + txt9.Text + "','" + Itemcode + "')";
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
        int cnt = 0;
        public void binddrop()
        {
            try
            {
                DataTable dt = new DataTable();
                // SqlCommand cmd = new SqlCommand("select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='ProductMaster'", con);
                //SqlCommand cmd = new SqlCommand("select column_name from INFORMATION_SCHEMA.COLUMNS where (TABLE_NAME='ProductMaster' or TABLE_NAME='Companymaster') and (column_name like '%ProductID%' or column_name like '%Product_Name%' or column_name like '%GroupName%' or column_name like '%Packing%' or column_name like '%itemnumber%' or column_name like '%companyname%' )", con);
                //SqlCommand cmd = new SqlCommand("select  COLUMN_NAME,s.value from INFORMATION_SCHEMA.COLUMNS i_s LEFT OUTER JOIN sys.extended_properties s ON s.major_id = OBJECT_ID(i_s.TABLE_SCHEMA+'.'+i_s.TABLE_NAME) AND s.minor_id = i_s.ORDINAL_POSITION AND s.name = 'MS_Description' where (TABLE_NAME='ProductMaster' or TABLE_NAME='Companymaster') and ( column_name like '%Product_Name%' or column_name like '%itemnumber%' or column_name like '%companyname%' )", con);
                SqlCommand cmd = new SqlCommand("select column_name from INFORMATION_SCHEMA.COLUMNS where (TABLE_NAME='ProductMaster' or TABLE_NAME='Companymaster') and (column_name like '%Product_Name%' or column_name like '%itemnumber%' or column_name like '%companyname%' )", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                // dt = sql.getdataset("select * from psm");
                DataRow dr;
                dr = dt.NewRow();
                dt.Rows.InsertAt(dr, 0);
                dr["column_name"] = "--Select Column Name--";
                if (dt.Rows.Count != 0)
                {
                    // cmbname.DataSource = dt.DefaultView;
                    // cmbname.ValueMember = "sp_id";
                    // cmbname.DisplayMember = "p_name";
                    // btnclr.Enabled = true;
                    // cmbname.SelectedIndex = -1;
                    txtsearch.DataSource = dt;
                    txtsearch.DisplayMember = "column_name";
                    txtsearch.ValueMember = "ClientID";
                }

            }
            catch
            {
            }
        }
        internal void Update(string iid)
        {
            try
            {
                cnt = 1;
                updateid = iid;
                dt = cs.getdataset("select p.ProductID as Id,p.itemnumber as [Item Code],p.Product_Name as [Name of item],c.companyname as [Make/Model],s.batch as Batch,s.closingstock as PastCurrentStock, s.adjuststock as [Adjust Stock],s.remarks as Remark from productmaster p inner join stockadujestmentitemmaster as s on p.productid=s.itemid and s.isactive=1 inner join companymaster c on c.companyid=p.companyid where p.isactive=1 and s.stockid='" + iid + "' order by p.product_name");
                DataTable stock = cs.getdataset("select * from stockadujestmentmaster where isactive=1 and id='" + iid + "'");
                grdstock.DataSource = dt;
                lblid.Text = stock.Rows[0]["id"].ToString();
                DTPFrom123.Text = stock.Rows[0]["stockdate"].ToString();
                txtremarks.Text = stock.Rows[0]["mainremark"].ToString();


                grdstock.Columns[0].Width = 0;
                grdstock.Columns[1].Width = 100;
                grdstock.Columns[2].Width = 300;
                grdstock.Columns[3].Width = 100;
                grdstock.Columns[4].Width = 100;
                grdstock.Columns[5].Width = 100;
                grdstock.Columns[6].Width = 100;
                grdstock.Columns[7].Width = 300;

                grdstock.Columns[1].ReadOnly = true;
                grdstock.Columns[2].ReadOnly = true;
                grdstock.Columns[3].ReadOnly = true;
                grdstock.Columns[4].ReadOnly = true;
                grdstock.Columns[5].Visible = false;

                btnok.Visible = false;
                txtsearch.Visible = false;
                txtser.Visible = false;
                grdstock.Columns[0].Visible = false;
                btnsubmit.Text = "Update";
            }
            catch
            {
            }
        }
        public static string stockid;
        public void bindid()
        {
            String str = cs.ExecuteScalar("select max(id) from stockadujestmentmaster");
            Int64 id;
            //     Object data = dr[1];

            if (str == "")
            {

                id = Convert.ToInt64("1");
            }
            else
            {
                id = Convert.ToInt32(str) + 1;
            }
            stockid = id.ToString();
        }
        private void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                if (btnsubmit.Text == "Update")
                {
                    if (grdstock.Rows.Count > 0)
                    {
                        cs.execute("Update stockadujestmentitemmaster set isactive=0 where stockid='" + lblid.Text + "'");
                        for (int i = 0; i < grdstock.Rows.Count; i++)
                        {
                            if (Convert.ToDouble(grdstock.Rows[i].Cells["Adjust Stock"].Value) != 0)
                            {
                                
                                cs.execute("INSERT INTO [dbo].[stockadujestmentitemmaster]([stockid],[itemid],[itemname],[closingstock],[adjuststock],[remarks],[isactive],[stockdate],[batch])VALUES('" + lblid.Text + "','" + grdstock.Rows[i].Cells[0].Value + "','" + grdstock.Rows[i].Cells[2].Value + "','0','" + grdstock.Rows[i].Cells[6].Value + "','" + grdstock.Rows[i].Cells[7].Value + "','" + "1" + "','" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "','" + grdstock.Rows[i].Cells[4].Value + "')");
                            }
                        }
                        cs.execute("Update stockadujestmentmaster set stockdate='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "',mainremark='" + txtremarks.Text + "' where id='" + lblid.Text + "'");
                        master.RemoveCurrentTab();
                    }
                }
                else
                {
                    if (grdstock.Rows.Count > 0)
                    {
                        bindid();
                        for (int i = 0; i < grdstock.Rows.Count; i++)
                        {
                            if (Convert.ToDouble(grdstock.Rows[i].Cells["Adjust Stock"].Value) != 0)
                            {
                                
                                cs.execute("INSERT INTO [dbo].[stockadujestmentitemmaster]([stockid],[itemid],[itemname],[closingstock],[adjuststock],[remarks],[isactive],[stockdate],[batch])VALUES('" + stockid + "','" + grdstock.Rows[i].Cells[1].Value + "','" + grdstock.Rows[i].Cells[3].Value + "','0','" + grdstock.Rows[i].Cells[6].Value + "','" + grdstock.Rows[i].Cells[7].Value + "','" + "1" + "','" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "','" + grdstock.Rows[i].Cells[5].Value + "')");
                            }
                        }
                        cs.execute("INSERT INTO [dbo].[stockadujestmentmaster]([stockdate],[mainremark],[isactive]) VALUES('" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "','" + txtremarks.Text + "','" + "1" + "')");
                        master.RemoveCurrentTab();
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

        private void btndelete_Click(object sender, EventArgs e)
        {
            DialogResult dr1 = MessageBox.Show("Do you want to Delete?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr1 == DialogResult.Yes)
            {
                cs.execute("Update stockadujestmentitemmaster set isactive=0 where stockid='" + lblid.Text + "'");
                cs.execute("Update stockadujestmentmaster set isactive=0 where id='" + lblid.Text + "'");
                master.RemoveCurrentTab();
            }
        }
        int FlagForStock = 0;
        private void grdstock_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (grdstock.CurrentCell.ColumnIndex == 0 && grdstock.Rows[grdstock.CurrentRow.Index].Cells[0].Value == "View")
                {
                  

                    string opening = "0", purchase = "0", purchasec = "0", sale = "0", salec = "0", salereturn = "0", purchasereturn = "0", possale = "0", finish = "0", pro = "0", adjuststock = "0";
                    
                        opening = cs.getsinglevalue("select ISNULL(SUM(cast(opstock as float)+cast(oploose as float)), 0) AS opstock from productpricemaster where productid='" + grdstock.Rows[grdstock.CurrentRow.Index].Cells[1].Value + "' and batchno='" + grdstock.Rows[grdstock.CurrentRow.Index].Cells[5].Value + "' and isactive=1");
                        possale = cs.getsinglevalue("select ISNULL(SUM(Qty), 0) AS POSSale from BillPOSProductMaster where isactive=1 and BillRunDate<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and itemname='" + grdstock.Rows[grdstock.CurrentRow.Index].Cells[3].Value + "' and batchno='" + grdstock.Rows[grdstock.CurrentRow.Index].Cells[5].Value + "'");
                        purchase = cs.getsinglevalue("select ISNULL(SUM(Pqty), 0) AS Purchase from billproductmaster where Billtype = 'P' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 and productid='" + grdstock.Rows[grdstock.CurrentRow.Index].Cells[1].Value + "' and batch='" + grdstock.Rows[grdstock.CurrentRow.Index].Cells[5].Value + "'");
                        purchasec = cs.getsinglevalue("select ISNULL(SUM(so.Pqty), 0) AS PurchaseChallan from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.Billtype = 'PC'and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' and so.productid='" + grdstock.Rows[grdstock.CurrentRow.Index].Cells[1].Value + "' and so.batch='" + grdstock.Rows[grdstock.CurrentRow.Index].Cells[5].Value + "'");
                        sale = cs.getsinglevalue("select ISNULL(SUM(Pqty), 0) AS Sale from billproductmaster where Billtype = 'S' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 and productid='" + grdstock.Rows[grdstock.CurrentRow.Index].Cells[1].Value + "' and batch='" + grdstock.Rows[grdstock.CurrentRow.Index].Cells[5].Value + "'");
                        salec = cs.getsinglevalue("select ISNULL(SUM(so.Pqty), 0) AS SaleChallan from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.Billtype = 'SC' and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' and so.productid='" + grdstock.Rows[grdstock.CurrentRow.Index].Cells[1].Value + "' and so.batch='" + grdstock.Rows[grdstock.CurrentRow.Index].Cells[5].Value + "'");
                        purchasereturn = cs.getsinglevalue("select ISNULL(SUM(Pqty), 0) AS PurchaseReturn from billproductmaster where Billtype = 'PR' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 and productid='" + grdstock.Rows[grdstock.CurrentRow.Index].Cells[1].Value + "' and batch='" + grdstock.Rows[grdstock.CurrentRow.Index].Cells[5].Value + "'");
                        adjuststock = cs.getsinglevalue("select ISNULL(SUM(adjuststock), 0) AS adjuststock from stockadujestmentitemmaster where stockdate<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 and itemid='" + grdstock.Rows[grdstock.CurrentRow.Index].Cells[1].Value + "' and batch='" + grdstock.Rows[grdstock.CurrentRow.Index].Cells[5].Value + "'");
                        Double closing = Convert.ToDouble(opening) + Convert.ToDouble(purchase.ToString()) + Convert.ToDouble(purchasec.ToString()) - Convert.ToDouble(sale.ToString()) - Convert.ToDouble(salec.ToString()) - Convert.ToDouble(possale.ToString()) + Convert.ToDouble(salereturn.ToString()) - Convert.ToDouble(purchasereturn.ToString()) - Convert.ToDouble(pro.ToString() + Convert.ToDouble(finish.ToString())) + Convert.ToDouble(adjuststock);
                        //string finalclosing = Math.Round(closing, 2).ToString("N2");
                        DataGridViewLinkCell BtnCell = null;

                        BtnCell = (DataGridViewLinkCell)grdstock.Rows[grdstock.CurrentRow.Index].Cells[grdstock.CurrentCell.ColumnIndex];
                        BtnCell.UseColumnTextForLinkValue = false;
                        BtnCell.Value = closing.ToString();
                        BtnCell.LinkColor = Color.Black;
                        BtnCell.LinkBehavior = LinkBehavior.NeverUnderline;
                   
                    //grdstock[grdstock.CurrentCell.ColumnIndex, grdstock.CurrentRow.Index].Value = closing.ToString();
                    //grdstock.Refresh();
                }
            }
            catch
            {
            }
        }
        static int buttonclicked;
        private void btnok_Click(object sender, EventArgs e)
        {
            buttonclicked = 1;
            LoadData();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string opening = "0", purchase = "0", purchasec = "0", sale = "0", salec = "0", salereturn = "0", purchasereturn = "0", possale = "0", finish = "0", pro = "0", adjuststock = "0";

                opening = cs.getsinglevalue("select ISNULL(SUM(cast(opstock as float)+cast(oploose as float)), 0) AS opstock from productpricemaster where productid='" + grdstock.Rows[i].Cells[1].Value + "' and batchno='" + grdstock.Rows[i].Cells[5].Value + "' and isactive=1");
                possale = cs.getsinglevalue("select ISNULL(SUM(Qty), 0) AS POSSale from BillPOSProductMaster where isactive=1 and BillRunDate<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and itemname='" + grdstock.Rows[i].Cells[3].Value + "' and batchno='" + grdstock.Rows[i].Cells[5].Value + "'");
                purchase = cs.getsinglevalue("select ISNULL(SUM(Pqty), 0) AS Purchase from billproductmaster where Billtype = 'P' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 and productid='" + grdstock.Rows[i].Cells[1].Value + "' and batch='" + grdstock.Rows[i].Cells[5].Value + "'");
                purchasec = cs.getsinglevalue("select ISNULL(SUM(so.Pqty), 0) AS PurchaseChallan from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.Billtype = 'PC'and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' and so.productid='" + grdstock.Rows[i].Cells[1].Value + "' and so.batch='" + grdstock.Rows[i].Cells[5].Value + "'");
                sale = cs.getsinglevalue("select ISNULL(SUM(Pqty), 0) AS Sale from billproductmaster where Billtype = 'S' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 and productid='" + grdstock.Rows[i].Cells[1].Value + "' and batch='" + grdstock.Rows[i].Cells[5].Value + "'");
                salec = cs.getsinglevalue("select ISNULL(SUM(so.Pqty), 0) AS SaleChallan from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.Billtype = 'SC' and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' and so.productid='" + grdstock.Rows[i].Cells[1].Value + "' and so.batch='" + grdstock.Rows[i].Cells[5].Value + "'");
                purchasereturn = cs.getsinglevalue("select ISNULL(SUM(Pqty), 0) AS PurchaseReturn from billproductmaster where Billtype = 'PR' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 and productid='" + grdstock.Rows[i].Cells[1].Value + "' and batch='" + grdstock.Rows[i].Cells[5].Value + "'");
                adjuststock = cs.getsinglevalue("select ISNULL(SUM(adjuststock), 0) AS adjuststock from stockadujestmentitemmaster where stockdate<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 and itemid='" + grdstock.Rows[i].Cells[1].Value + "' and batch='" + grdstock.Rows[i].Cells[5].Value + "'");
                Double closing = Convert.ToDouble(opening) + Convert.ToDouble(purchase.ToString()) + Convert.ToDouble(purchasec.ToString()) - Convert.ToDouble(sale.ToString()) - Convert.ToDouble(salec.ToString()) - Convert.ToDouble(possale.ToString()) + Convert.ToDouble(salereturn.ToString()) - Convert.ToDouble(purchasereturn.ToString()) - Convert.ToDouble(pro.ToString() + Convert.ToDouble(finish.ToString())) + Convert.ToDouble(adjuststock);
                //string finalclosing = Math.Round(closing, 2).ToString("N2");
                DataGridViewLinkCell BtnCell = null;

                BtnCell = (DataGridViewLinkCell)grdstock.Rows[i].Cells[grdstock.CurrentCell.ColumnIndex];
                BtnCell.UseColumnTextForLinkValue = false;
                BtnCell.Value = closing.ToString();
                BtnCell.LinkColor = Color.Black;
                BtnCell.LinkBehavior = LinkBehavior.NeverUnderline;
            }
        }

        private void txtser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnok.Focus();
            }
        }
    }
}
