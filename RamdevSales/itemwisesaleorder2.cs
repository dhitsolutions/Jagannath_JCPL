using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Linq;
//using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using ClosedXML.Excel;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace RamdevSales
{
    public partial class itemwisesaleorder2 : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        OleDbSettings ods = new OleDbSettings();
        Printing prn = new Printing();
        private Master master;
        private TabControl tabControl;

        public itemwisesaleorder2()
        {
            InitializeComponent();
        }

        public itemwisesaleorder2(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }
        DataTable maindt = new DataTable();
        Double debit = 0;
        Double qty = 0;
        Int32 rowid = -1;
        private void bindaccountdropdown()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='ClientMaster' and (column_name like '%AccountName%' or column_name like '%PrintName%' or column_name like '%Groupname%' or column_name like '%Address%' or column_name like '%City%' or column_name like '%State%')", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                // dt = sql.getdataset("select * from psm");
                DataRow dr;
                dr = dt.NewRow();
                dt.Rows.InsertAt(dr, 0);
                dr["column_name"] = "--Select--";
                if (dt.Rows.Count != 0)
                {
                    // cmbname.DataSource = dt.DefaultView;
                    // cmbname.ValueMember = "sp_id";
                    // cmbname.DisplayMember = "p_name";
                    // btnclr.Enabled = true;
                    // cmbname.SelectedIndex = -1;
                    drpaccount.DataSource = dt;
                    drpaccount.DisplayMember = "column_name";
                    drpaccount.ValueMember = "ClientID";
                }
            }
            catch
            {
            }
        }

        private void binditemdropdown()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("select column_name from INFORMATION_SCHEMA.COLUMNS where (TABLE_NAME='ProductMaster' or TABLE_NAME='Companymaster') and (column_name like '%ProductID%' or column_name like '%Product_Name%' or column_name like '%GroupName%' or column_name like '%Packing%' or column_name like '%HSN_Sac_Code%' or column_name like '%itemnumber%' or column_name like '%companyname%' )", con);
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
                    drpitems.DataSource = dt;
                    drpitems.DisplayMember = "column_name";
                    drpitems.ValueMember = "ClientID";
                }

            }
            catch
            {
            }
        }

        private void itemwisesaleorder2_Load(object sender, EventArgs e)
        {
            DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
            DTPFrom.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            DTPFrom.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            DTPTo.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            DTPTo.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            // DTPFrom.Value = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            DTPFrom.CustomFormat = Master.dateformate;
            DTPTo.CustomFormat = Master.dateformate;
            this.ActiveControl = DTPFrom;
            bindaccountdropdown();
            binditemdropdown();
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

        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }
        DataRow dr;
        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            try
            {
                grdview.DataSource = null;
                DataTable bills = conn.getdataset("select so.*,c.AccountName from SaleOrderMaster  so inner join ClientMaster c on c.ClientID=so.ClientID where so.isactive=1 and c.isactive=1  and so.BillType='SO' and so.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by so.Bill_Date");
                bills = changedtclone(bills);

                DataTable items = conn.getdataset("select distinct Productname as ItemName from SaleOrderProductMaster where isactive=1 and Billtype='SO' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Productname");
                items = changedtclone(items);

                maindt = new DataTable();
                maindt.Columns.Add("Items name");
                //     for (int i = 0; i < bills.Rows.Count; i++)
                //     {
                bool isexist = false;
                bool iscolexist = false;
                for (int j = 0; j < items.Rows.Count; j++)
                {
                    //string qty = conn.ExecuteScalar("select isnull(sum(qty),0) as qty from SaleOrderProductMaster where isactive=1 and Billtype='SO' and billno='" + bills.Rows[i]["billno"].ToString() + "' and productname='" + items.Rows[j]["ItemName"].ToString() + "'");
                    //// string clientname = conn.ExecuteScalar("select AccountName from ClientMaster where isactive=1 and ClientID='" + bills.Rows[i]["ClientID"].ToString() + "'");
                    //if (Convert.ToDouble(qty) > 0)
                    //{
                    //    isexist = true;
                    //    if (iscolexist == false)
                    //    {
                    //        if ((bills.Rows[i]["AccountName"].ToString()).ToUpper() == "CASH")
                    //        {
                    maindt.Columns.Add(items.Rows[j]["ItemName"].ToString());
                    //        }
                    //        else
                    //        {
                    //            maindt.Columns.Add(bills.Rows[i]["billno"].ToString() + Environment.NewLine + bills.Rows[i]["AccountName"].ToString());
                    //        }
                    //        iscolexist = true;
                    //    }
                    //}
                    //if (isexist == true)
                    //{
                    //    break;
                    //}

                }
                //   }
                maindt.Columns.Add("Total");



                //bool isexist = false;


                for (int j = 0; j < bills.Rows.Count; j++)
                {
                    dr = maindt.NewRow();
                    if ((bills.Rows[j]["AccountName"].ToString()).ToUpper() == "CASH")
                    {
                        dr["Items name"] = bills.Rows[j]["billno"].ToString() + Environment.NewLine + bills.Rows[j]["cusname"].ToString();
                    }
                    else
                    {
                        dr["Items name"] = bills.Rows[j]["billno"].ToString() + Environment.NewLine + bills.Rows[j]["AccountName"].ToString();
                    }
                    double rowstot = 0;
                    for (int i = 0; i < items.Rows.Count; i++)
                    {
                        string qty = conn.ExecuteScalar("select isnull(sum(qty),0) as qty from SaleOrderProductMaster where isactive=1 and Billtype='SO' and billno='" + bills.Rows[j]["billno"].ToString() + "' and productname='" + items.Rows[i]["ItemName"].ToString() + "'");
                        //if ((bills.Rows[j]["AccountName"].ToString()).ToUpper() == "CASH")
                        //{
                        dr[items.Rows[i]["ItemName"].ToString()] = Convert.ToDouble(qty).ToString("0.##");
                        //  }
                        //else
                        //{
                        //    dr[items.Rows[i]["ItemName"].ToString()] = Convert.ToDouble(qty).ToString("0.##");
                        //}
                        rowstot += Convert.ToDouble(qty);
                    }
                    dr["Total"] = rowstot.ToString("0.##");
                    maindt.Rows.Add(dr);
                }


                dr = maindt.NewRow();
                dr["items Name"] = "Total";
                for (int i = 1; i < maindt.Columns.Count; i++)
                {
                    double colstot = 0;
                    for (int j = 0; j < maindt.Rows.Count; j++)
                    {
                        colstot += Convert.ToDouble(maindt.Rows[j][i].ToString());
                        dr[maindt.Columns[i].ColumnName] = colstot;
                    }
                }
                maindt.Rows.Add(dr);

                grdview.DataSource = maindt;
            }
            catch
            {
            }
        }

        private void btngenrpt_Click(object sender, EventArgs e)
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
                            wb.Worksheets.Add(maindt, "SaleOrder");
                            // wb.Worksheets.Add(dt1, "ItemPrice");
                            wb.SaveAs(folderPath + "SaleOrder_Management" + DateTimeName + ".xlsx");
                        }
                        MessageBox.Show("Export Data Sucessfully");
                        DialogResult dr = MessageBox.Show("Do you want to Open Document?", "SaleOrder Management", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(folderPath + "SaleOrder_Management" + DateTimeName + ".xlsx");
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

        private void drpaccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = conn.getdataset("select distinct " + drpaccount.Text + " from ClientMaster c inner join SaleOrderMaster so on so.ClientID=c.ClientID where so.isactive=1 and c.isactive=1 and so.BillType='SO' and so.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'  and " + drpaccount.Text + "!=''");
                if (drpaccount.Text == "AccountName" || drpaccount.Text == "PrintName")
                {
                    //DataTable dt1 = conn.getdataset("select distinct cusname as " + drpaccount.Text + " from saleordermaster where isactive=1 and ClientID='101'");
                    DataTable dt1 = conn.getdataset("select distinct case when upper(" + drpaccount.Text + ")='CASH' then '' when UPPER(" + drpaccount.Text + ")<>'CASH' THEN AccountName end + cusname as " + drpaccount.Text + " from ClientMaster c inner join SaleOrderMaster so on so.ClientID=c.ClientID where so.isactive=1 and c.isactive=1 and so.BillType='SO' and so.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'");
                    if (dt1.Rows.Count > 0)
                    {
                        //  dt1 = changedtclone(dt1);
                        dt = dt1;
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    chkacc.Items.Clear();
                    foreach (DataRow item in dt.Rows)
                    {
                        chkacc.Items.Add(item[drpaccount.Text].ToString());
                    }
                }
            }
            catch
            {
            }
        }

        private void drpitems_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpitems.Text == "companyname")
                {
                    DataTable dt = conn.getdataset("select distinct " + drpitems.Text + " from Companymaster  where " + drpitems.Text + "!=''");
                    if (dt.Rows.Count > 0)
                    {
                        chkitem.Items.Clear();
                        foreach (DataRow item in dt.Rows)
                        {
                            chkitem.Items.Add(item[drpitems.Text].ToString());
                        }
                    }
                }
                else
                {
                    DataTable dt = conn.getdataset("select distinct " + drpitems.Text + " from ProductMaster p inner join SaleOrderProductMaster s on s.productid=p.ProductID where p.isactive=1 and s.isactive=1 and Billtype='SO' and " + drpitems.Text + "!=''");
                    if (dt.Rows.Count > 0)
                    {
                        chkitem.Items.Clear();
                        foreach (DataRow item in dt.Rows)
                        {
                            chkitem.Items.Add(item[drpitems.Text].ToString());
                        }
                    }
                }
            }
            catch
            {
            }
        }
        string qry = "", qry1 = "";
        public void bindserch()
        {
            try
            {

                grdview.DataSource = null;
                string clientid = "";
                DataTable bills = new DataTable();


                if (drpaccount.Text == "AccountName" || drpaccount.Text == "PrintName" || drpaccount.Text == "GroupName" || drpaccount.Text == "Address" || drpaccount.Text == "City" || drpaccount.Text == "State" || drpaccount.Text == "statecode")
                {
                    qry = "select so.*,c.AccountName from SaleOrderMaster  so inner join ClientMaster c on c.ClientID=so.ClientID where so.isactive=1 and c.isactive=1  and so.BillType='SO' and (";
                    for (int i = 0; i < chkacc.Items.Count; i++)
                    {

                        if (chkacc.GetItemChecked(i))
                        {
                            string str = chkacc.Items[i].ToString();
                            if (drpaccount.Text == "AccountName")
                            {

                                qry += drpaccount.Text + " like '%" + str + "%' or cusname like '%" + str + "%' or ";
                            }
                            else
                            {
                                qry += drpaccount.Text + " like '%" + str + "%' or ";
                            }
                            //   bills = conn.getdataset("select * from SaleOrderMaster where clientid in (select clientid from clientmaster where isactive=1 and (AccountName like '%" + str + "%' or PrintName like '%" + str + "%' or GroupName like '%" + str + "%' or Address like '%" + str + "%' or City like '%" + str + "%'or State like '%" + str + "%' or statecode like '%" + str + "%')) and isactive=1 and BillType='SO' and Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Bill_Date");
                        }
                    }
                    String withoutLast1 = qry.Substring(0, (qry.Length - 3));
                    withoutLast1 += ") and Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Bill_Date";
                    bills = conn.getdataset(withoutLast1);
                    //clientid = conn.getsinglevalue("select clientid from clientmaster where isactive=1 and (AccountName like '%" + txtaccount.Text + "%' and PrintName like '%" + txtaccount.Text + "%' and GroupName like '%" + txtaccount.Text + "%' and Address like '%" + txtaccount.Text + "%' and City like '%" + txtaccount.Text + "%'and State like '%" + txtaccount.Text + "%' and statecode like '%" + txtaccount.Text + "%')");
                    //bills = conn.getdataset("select * from SaleOrderMaster where clientid in (select clientid from clientmaster where isactive=1 and (AccountName like '%" + txtaccount.Text + "%' or PrintName like '%" + txtaccount.Text + "%' or GroupName like '%" + txtaccount.Text + "%' or Address like '%" + txtaccount.Text + "%' or City like '%" + txtaccount.Text + "%'or State like '%" + txtaccount.Text + "%' or statecode like '%" + txtaccount.Text + "%')) and isactive=1 and BillType='SO' and Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Bill_Date");
                }
                else
                {
                    clientid = txtaccount.Text;
                    bills = conn.getdataset("select so.*,c.AccountName from SaleOrderMaster  so inner join ClientMaster c on c.ClientID=so.ClientID where so.isactive=1 and c.isactive=1  and so.BillType='SO' and c.clientid like '%" + clientid + "%' and Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Bill_Date");
                }


                bills = changedtclone(bills);

                DataTable items = new DataTable();
                string itemname = "";
                if (drpitems.Text == "GroupName" || drpitems.Text == "Packing" || drpitems.Text == "Hsn_Sac_Code" || drpitems.Text == "itemnumber" || drpitems.Text == "ProductID" || drpitems.Text == "Product_Name")
                {
                    qry1 = "select distinct Productname as ItemName from SaleOrderProductMaster where productname in (select product_name from productmaster where isactive=1 and (";
                    for (int i = 0; i < chkitem.Items.Count; i++)
                    {

                        if (chkitem.GetItemChecked(i))
                        {
                            string str = chkitem.Items[i].ToString();
                            qry1 += drpitems.Text + " like '%" + str + "%' or ";
                        }
                    }
                    String withoutLast1 = qry1.Substring(0, (qry1.Length - 3));
                    withoutLast1 += ")) and isactive=1 and Billtype='SO' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Productname";
                    items = conn.getdataset(withoutLast1);
                    //itemname = conn.getsinglevalue("select product_name from productmaster where isactive=1 and (GroupName like '%" + txtitems.Text + "%' and Packing like '%" + txtitems.Text + "%' and Hsn_Sac_Code like '%" + txtitems.Text + "%' and itemnumber like '%" + txtitems.Text + "%')");
                    // items = conn.getdataset("select distinct Productname as ItemName from SaleOrderProductMaster where productname in (select product_name from productmaster where isactive=1 and (GroupName like '%" + txtitems.Text + "%' or Packing like '%" + txtitems.Text + "%' or Hsn_Sac_Code like '%" + txtitems.Text + "%' or ProductID like '%" + txtitems.Text + "%' or itemnumber like '%" + txtitems.Text + "%')) and isactive=1 and Billtype='SO' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Productname");
                }
                else if (drpitems.Text == "companyname")
                {
                    qry1 = "select distinct Productname as ItemName from SaleOrderProductMaster where productname in (select product_name from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (";
                    for (int i = 0; i < chkitem.Items.Count; i++)
                    {

                        if (chkitem.GetItemChecked(i))
                        {
                            string str = chkitem.Items[i].ToString();
                            qry1 += drpitems.Text + " like '%" + str + "%' or ";
                        }
                    }
                    String withoutLast1 = qry1.Substring(0, (qry1.Length - 3));
                    withoutLast1 += "))) and isactive=1 and Billtype='SO' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Productname";
                    items = conn.getdataset(withoutLast1);
                    //itemname = conn.getsinglevalue("select product_name from productmaster where isactive=1 and companyid =(select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))");
                    // items = conn.getdataset("select distinct Productname as ItemName from SaleOrderProductMaster where productname in (select product_name from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and isactive=1 and Billtype='SO' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Productname");
                }
                else
                {
                    //    qry1 = "select distinct Productname as ItemName from SaleOrderProductMaster where";
                    //    for (int i = 0; i < chkitem.Items.Count; i++)
                    //    {

                    //        if (chkitem.GetItemChecked(i))
                    //        {
                    //            string str = chkitem.Items[i].ToString();
                    //            qry1 += drpitems.Text + " like '%" + str + "%' or ";
                    //        }
                    //    }
                    //    String withoutLast1 = qry1.Substring(0, (qry1.Length - 3));
                    //    withoutLast1 += "and isactive=1 and Billtype='SO' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Productname";
                    itemname = txtitems.Text;
                    items = conn.getdataset("select distinct Productname as ItemName from SaleOrderProductMaster where productname like '%" + itemname + "%'  and isactive=1 and Billtype='SO' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Productname");
                }

                items = changedtclone(items);

                maindt = new DataTable();
                maindt.Columns.Add("Items name");
                //     for (int i = 0; i < bills.Rows.Count; i++)
                //     {
                bool isexist = false;
                bool iscolexist = false;
                for (int j = 0; j < items.Rows.Count; j++)
                {
                    //string qty = conn.ExecuteScalar("select isnull(sum(qty),0) as qty from SaleOrderProductMaster where isactive=1 and Billtype='SO' and billno='" + bills.Rows[i]["billno"].ToString() + "' and productname='" + items.Rows[j]["ItemName"].ToString() + "'");
                    //// string clientname = conn.ExecuteScalar("select AccountName from ClientMaster where isactive=1 and ClientID='" + bills.Rows[i]["ClientID"].ToString() + "'");
                    //if (Convert.ToDouble(qty) > 0)
                    //{
                    //    isexist = true;
                    //    if (iscolexist == false)
                    //    {
                    //        if ((bills.Rows[i]["AccountName"].ToString()).ToUpper() == "CASH")
                    //        {
                    maindt.Columns.Add(items.Rows[j]["ItemName"].ToString());
                    //        }
                    //        else
                    //        {
                    //            maindt.Columns.Add(bills.Rows[i]["billno"].ToString() + Environment.NewLine + bills.Rows[i]["AccountName"].ToString());
                    //        }
                    //        iscolexist = true;
                    //    }
                    //}
                    //if (isexist == true)
                    //{
                    //    break;
                    //}

                }
                //   }
                maindt.Columns.Add("Total");



                //bool isexist = false;


                for (int j = 0; j < bills.Rows.Count; j++)
                {
                    dr = maindt.NewRow();
                    if ((bills.Rows[j]["AccountName"].ToString()).ToUpper() == "CASH")
                    {
                        dr["Items name"] = bills.Rows[j]["billno"].ToString() + Environment.NewLine + bills.Rows[j]["cusname"].ToString();
                    }
                    else
                    {
                        dr["Items name"] = bills.Rows[j]["billno"].ToString() + Environment.NewLine + bills.Rows[j]["AccountName"].ToString();
                    }
                    double rowstot = 0;
                    for (int i = 0; i < items.Rows.Count; i++)
                    {
                        string qty = conn.ExecuteScalar("select isnull(sum(qty),0) as qty from SaleOrderProductMaster where isactive=1 and Billtype='SO' and billno='" + bills.Rows[j]["billno"].ToString() + "' and productname='" + items.Rows[i]["ItemName"].ToString() + "'");
                        //if ((bills.Rows[j]["AccountName"].ToString()).ToUpper() == "CASH")
                        //{
                        dr[items.Rows[i]["ItemName"].ToString()] = Convert.ToDouble(qty).ToString("0.##");
                        //  }
                        //else
                        //{
                        //    dr[items.Rows[i]["ItemName"].ToString()] = Convert.ToDouble(qty).ToString("0.##");
                        //}
                        rowstot += Convert.ToDouble(qty);
                    }
                    dr["Total"] = rowstot.ToString("0.##");
                    maindt.Rows.Add(dr);
                }
                dr = maindt.NewRow();
                dr["items Name"] = "Total";
                for (int i = 1; i < maindt.Columns.Count; i++)
                {
                    double colstot = 0;
                    for (int j = 0; j < maindt.Rows.Count; j++)
                    {
                        colstot += Convert.ToDouble(maindt.Rows[j][i].ToString());
                        dr[maindt.Columns[i].ColumnName] = colstot;
                    }
                }
                maindt.Rows.Add(dr);

                grdview.DataSource = maindt;

            }
            catch
            {
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            bindserch();
        }
        void ExportDataTableToPdf(DataTable dttable, string strpdfFile, string strHeader, string folderpath)
        {
            try
            {
                System.IO.FileStream fs = new FileStream(strpdfFile, FileMode.Create, FileAccess.Write, FileShare.None);
                Document document = new Document();
                document.SetPageSize(iTextSharp.text.PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                document.Open();

                BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font fntHead = new iTextSharp.text.Font(bfntHead, 8, 1, iTextSharp.text.Color.GRAY);

                Paragraph prgHeading = new Paragraph();
                prgHeading.Alignment = Element.ALIGN_CENTER;
                prgHeading.Add(new Chunk(strHeader.ToUpper(), fntHead));
                document.Add(prgHeading);


                Paragraph prgspace = new Paragraph();
                prgspace.Add(new Chunk("\n"));
                document.Add(prgspace);

                PdfPTable table = new PdfPTable(dttable.Columns.Count);
                BaseFont btncolumnheader = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font fntColumnHeader = new Font(btncolumnheader, 4, 1, iTextSharp.text.Color.WHITE);

                BaseFont btnrow = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font fntRow = new Font(btnrow, 4, 1, iTextSharp.text.Color.BLACK);

                for (int i = 0; i < dttable.Columns.Count; i++)
                {
                    PdfPCell cell = new PdfPCell();
                    cell.Rotation = 90;
                    cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    cell.BackgroundColor = iTextSharp.text.Color.GRAY;
                    cell.AddElement(new Chunk(dttable.Columns[i].ColumnName.ToUpper(), fntColumnHeader));
                    table.AddCell(cell);
                }
                for (int i = 0; i < dttable.Rows.Count; i++)
                {
                    for (int j = 0; j < dttable.Columns.Count; j++)
                    {
                        PdfPCell cell = new PdfPCell();
                        if (dttable.Rows[i][j].ToString() == "0")
                        {
                            cell.AddElement(new Chunk("", fntRow));
                        }
                        else
                        {
                            cell.AddElement(new Chunk(dttable.Rows[i][j].ToString(), fntRow));
                        }
                        table.AddCell(cell);
                    }
                }
                document.Add(table);
                document.Close();
                writer.Close();
                fs.Close();
                System.Diagnostics.Process.Start(folderpath + "ITEM_WISE_SALE_ORDER.pdf");
                MessageBox.Show("PDF Created Sucessfully");
            }
            catch (Exception excp)
            {
                if (excp.Message.Contains("The process cannot access the file"))
                {
                    MessageBox.Show("Please Close the Already open PDF File..");
                    return;
                }

            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);
                    string folderPath = fbd.SelectedPath + "\\";
                    String DateTimeName = DateTime.Now.ToString("dd_MMM_yyyy hh_mm_ss");

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    DataTable dtdata = (DataTable)grdview.DataSource;

                    //  ExportDataTableToPdf(dtdata, folderPath + "ITEM_WISE_SALE_ORDER" + DateTimeName + ".pdf", "ITEM WISE SALE ORDER FROM "+DTPFrom.Text+" TO" +DTPTo.Text);
                    //  System.Diagnostics.Process.Start(folderPath + "ITEM_WISE_SALE_ORDER" + DateTimeName + ".pdf");

                    ExportDataTableToPdf(dtdata, folderPath + "ITEM_WISE_SALE_ORDER.pdf", "ITEM WISE SALE ORDER FROM " + DTPFrom.Text + " TO " + DTPTo.Text, folderPath);

                    //  ExportDataTableToPdf(dtdata, folderPath + "ITEM_WISE_SALE_ORDER1.pdf", "ITEM WISE SALE ORDER FROM " + DTPFrom.Text + " TO" + DTPTo.Text);
                    //  System.Diagnostics.Process.Start(folderPath + "ITEM_WISE_SALE_ORDER1.pdf");


                }
            }
        }
    }
}
