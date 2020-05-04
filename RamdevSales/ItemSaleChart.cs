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

namespace RamdevSales
{
    public partial class ItemSaleChart : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        OleDbSettings ods = new OleDbSettings();
        Printing prn = new Printing();
        private Master master;
        private TabControl tabControl;
        public ItemSaleChart()
        {
            InitializeComponent();
        }

        public ItemSaleChart(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }
        Double debit = 0;
        Double qty = 0;
        Int32 rowid = -1;
        private void ItemSaleChart_Load(object sender, EventArgs e)
        {
            DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
            DTPFrom.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            DTPFrom.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            DTPTo.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            DTPTo.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            DTPFrom.Value = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            DTPFrom.CustomFormat = Master.dateformate;
            DTPTo.CustomFormat = Master.dateformate;
            this.ActiveControl = DTPFrom;
            bindaccountdropdown();
            binditemdropdown();
        }
        DataTable maindt = new DataTable();
        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            try
            {
                grdview.DataSource = null;
                DataTable bills = conn.getdataset("select * from BillMaster where isactive=1 and BillType='S' and Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Bill_Date");
                bills = changedtclone(bills);

                DataTable items = conn.getdataset("select distinct Productname as ItemName from BillProductMaster where isactive=1 and Billtype='S' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Productname");
                items = changedtclone(items);

                maindt = new DataTable();
                maindt.Columns.Add("Items name");
                for (int i = 0; i < bills.Rows.Count; i++)
                {
                    bool isexist = false;
                    bool iscolexist = false;
                    for (int j = 0; j < items.Rows.Count; j++)
                    {
                        string qty = conn.ExecuteScalar("select isnull(sum(qty),0) as qty from BillProductMaster where isactive=1 and Billtype='S' and billno='" + bills.Rows[i]["billno"].ToString() + "' and productname='" + items.Rows[j]["ItemName"].ToString() + "'");
                        
                        if (Convert.ToDouble(qty) > 0)
                        {
                            isexist = true;
                            if (iscolexist == false)
                            {
                                maindt.Columns.Add(bills.Rows[i]["billno"].ToString() + Environment.NewLine + Convert.ToDateTime(bills.Rows[i]["Bill_Date"].ToString()).ToString("dd-MMM-yyyy"));
                                iscolexist = true;
                            }
                        }
                        if (isexist == true)
                        {
                            break;
                        }
                        
                    }
                }
                maindt.Columns.Add("Total");
                DataRow dr;
               
                for (int i = 0; i < items.Rows.Count; i++)
                {
                    bool isexist = false;
                    dr = maindt.NewRow();
                    dr["Items name"] = items.Rows[i]["ItemName"].ToString();
                    double rowstot = 0;
                    for (int j = 0; j < bills.Rows.Count; j++)
                    {
                        string qty = conn.ExecuteScalar("select isnull(sum(qty),0) as qty from BillProductMaster where isactive=1 and Billtype='S' and billno='" + bills.Rows[j]["billno"].ToString() + "' and productname='" + items.Rows[i]["ItemName"].ToString() + "'");
                        dr[bills.Rows[j]["billno"].ToString() + Environment.NewLine + Convert.ToDateTime(bills.Rows[j]["Bill_Date"].ToString()).ToString("dd-MMM-yyyy")] = qty;
                        rowstot += Convert.ToDouble(qty);
                    }
                    dr["Total"] = rowstot;
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
        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }
        string qry = "", qry1 = "";
        public void bindserch()
        {
            try
            {
                if (txtaccount.Text != "" || txtitems.Text != "")
                {
                    grdview.DataSource = null;
                    string clientid = "";
                    DataTable bills = new DataTable();
                    if (drpaccount.Text == "AccountName" || drpaccount.Text == "PrintName" || drpaccount.Text == "GroupName" || drpaccount.Text == "Address" || drpaccount.Text == "City" || drpaccount.Text == "State" || drpaccount.Text == "statecode")
                    {
                        qry = "select * from BillMaster where clientid in (select clientid from clientmaster where isactive=1 and (";
                        for (int i = 0; i < chkacc.Items.Count; i++)
                        {

                            if (chkacc.GetItemChecked(i))
                            {
                                string str = chkacc.Items[i].ToString();
                                qry += drpaccount.Text + " like '%" + str + "%' or ";
                                //   bills = conn.getdataset("select * from SaleOrderMaster where clientid in (select clientid from clientmaster where isactive=1 and (AccountName like '%" + str + "%' or PrintName like '%" + str + "%' or GroupName like '%" + str + "%' or Address like '%" + str + "%' or City like '%" + str + "%'or State like '%" + str + "%' or statecode like '%" + str + "%')) and isactive=1 and BillType='SO' and Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Bill_Date");
                            }
                        }
                        String withoutLast1 = qry.Substring(0, (qry.Length - 3));
                        withoutLast1 += ")) and isactive=1 and BillType='S' and Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Bill_Date";
                       bills= conn.getdataset(withoutLast1);
                        //clientid = conn.getsinglevalue("select clientid from clientmaster where isactive=1 and (AccountName like '%" + txtaccount.Text + "%' and PrintName like '%" + txtaccount.Text + "%' and GroupName like '%" + txtaccount.Text + "%' and Address like '%" + txtaccount.Text + "%' and City like '%" + txtaccount.Text + "%'and State like '%" + txtaccount.Text + "%' and statecode like '%" + txtaccount.Text + "%')");
                        //bills = conn.getdataset("select * from BillMaster where clientid in (select clientid from clientmaster where isactive=1 and (AccountName like '%" + txtaccount.Text + "%' or PrintName like '%" + txtaccount.Text + "%' or GroupName like '%" + txtaccount.Text + "%' or Address like '%" + txtaccount.Text + "%' or City like '%" + txtaccount.Text + "%'or State like '%" + txtaccount.Text + "%' or statecode like '%" + txtaccount.Text + "%')) and isactive=1 and BillType='S' and Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Bill_Date");
                    }
                    else
                    {
                        clientid = txtaccount.Text;
                        bills = conn.getdataset("select * from BillMaster where clientid like '%" + clientid + "%' and isactive=1 and BillType='S' and Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Bill_Date");
                    }

                    bills = changedtclone(bills);

                    DataTable items = new DataTable();
                    string itemname = "";
                    if (drpitems.Text == "GroupName" || drpitems.Text == "Packing" || drpitems.Text == "Hsn_Sac_Code" || drpitems.Text == "itemnumber" || drpitems.Text == "ProductID" || drpitems.Text == "Product_Name")
                    {
                        qry1 = "select distinct Productname as ItemName from BillProductMaster where productname in (select product_name from productmaster where isactive=1 and (";
                        for (int i = 0; i < chkitem.Items.Count; i++)
                        {

                            if (chkitem.GetItemChecked(i))
                            {
                                string str = chkitem.Items[i].ToString();
                                qry1 += drpitems.Text + " like '%" + str + "%' or ";
                            }
                        }
                        String withoutLast1 = qry1.Substring(0, (qry1.Length - 3));
                        withoutLast1 += ")) and isactive=1 and Billtype='S' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Productname";
                        items = conn.getdataset(withoutLast1);
                        //itemname = conn.getsinglevalue("select product_name from productmaster where isactive=1 and (GroupName like '%" + txtitems.Text + "%' and Packing like '%" + txtitems.Text + "%' and Hsn_Sac_Code like '%" + txtitems.Text + "%' and itemnumber like '%" + txtitems.Text + "%')");
                        //items = conn.getdataset("select distinct Productname as ItemName from BillProductMaster where productname in (select product_name from productmaster where isactive=1 and (GroupName like '%" + txtitems.Text + "%' or Packing like '%" + txtitems.Text + "%' or Hsn_Sac_Code like '%" + txtitems.Text + "%' or ProductID like '%" + txtitems.Text + "%' or itemnumber like '%" + txtitems.Text + "%')) and isactive=1 and Billtype='S' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Productname");
                    }
                    else if (drpitems.Text == "companyname")
                    {
                        qry1 = "select distinct Productname as ItemName from BillProductMaster where productname in (select product_name from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (";
                        for (int i = 0; i < chkitem.Items.Count; i++)
                        {

                            if (chkitem.GetItemChecked(i))
                            {
                                string str = chkitem.Items[i].ToString();
                                qry1 += drpitems.Text + " like '%" + str + "%' or ";
                            }
                        }
                        String withoutLast1 = qry1.Substring(0, (qry1.Length - 3));
                        withoutLast1 += "))) and isactive=1 and Billtype='S' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Productname";
                        items = conn.getdataset(withoutLast1);
                        //itemname = conn.getsinglevalue("select product_name from productmaster where isactive=1 and companyid =(select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))");
                      //  items = conn.getdataset("select distinct Productname as ItemName from BillProductMaster where productname in (select product_name from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and isactive=1 and Billtype='S' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Productname");
                    }
                    else
                    {
                    //    qry1 = "select distinct Productname as ItemName from BillProductMaster where";
                    //    for (int i = 0; i < chkitem.Items.Count; i++)
                    //    {

                    //        if (chkitem.GetItemChecked(i))
                    //        {
                    //            string str = chkitem.Items[i].ToString();
                    //            qry1 += drpitems.Text + " like '%" + str + "%' or ";
                    //        }
                    //    }
                    //    String withoutLast1 = qry1.Substring(0, (qry1.Length - 3));
                    //    withoutLast1 += "and isactive=1 and Billtype='S' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Productname";
                        itemname = txtitems.Text;
                        items = conn.getdataset("select distinct Productname as ItemName from BillProductMaster where productname like '%" + itemname + "%'  and isactive=1 and Billtype='S' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Productname");
                    }

                    items = changedtclone(items);

                    maindt = new DataTable();
                    maindt.Columns.Add("Items name");
                    for (int i = 0; i < bills.Rows.Count; i++)
                    {
                        bool isexist = false;
                        bool iscolexist = false;
                        for (int j = 0; j < items.Rows.Count; j++)
                        {
                            string qty = conn.ExecuteScalar("select isnull(sum(qty),0) as qty from BillProductMaster where isactive=1 and Billtype='S' and billno='" + bills.Rows[i]["billno"].ToString() + "' and productname='" + items.Rows[j]["ItemName"].ToString() + "'");

                            if (Convert.ToDouble(qty) > 0)
                            {
                                isexist = true;
                                if (iscolexist == false)
                                {
                                    maindt.Columns.Add(bills.Rows[i]["billno"].ToString() + Environment.NewLine + Convert.ToDateTime(bills.Rows[i]["Bill_Date"].ToString()).ToString("dd-MMM-yyyy"));
                                    iscolexist = true;
                                }
                            }
                            if (isexist == true)
                            {
                                break;
                            }

                        }
                    }
                    maindt.Columns.Add("Total");
                    DataRow dr;

                    for (int i = 0; i < items.Rows.Count; i++)
                    {
                        bool isexist = false;
                        dr = maindt.NewRow();
                        dr["Items name"] = items.Rows[i]["ItemName"].ToString();
                        double rowstot = 0;
                        for (int j = 0; j < bills.Rows.Count; j++)
                        {
                            try
                            {
                                string qty = conn.ExecuteScalar("select isnull(sum(qty),0) as qty from BillProductMaster where isactive=1 and Billtype='S' and billno='" + bills.Rows[j]["billno"].ToString() + "' and productname='" + items.Rows[i]["ItemName"].ToString() + "'");
                                dr[bills.Rows[j]["billno"].ToString() + Environment.NewLine + Convert.ToDateTime(bills.Rows[j]["Bill_Date"].ToString()).ToString("dd-MMM-yyyy")] = qty;
                                rowstot += Convert.ToDouble(qty);
                            }
                            catch
                            {
                            }
                        }
                        dr["Total"] = rowstot;
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
            }
            catch
            {
            }
        }
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
        private void btnSearch_Click(object sender, EventArgs e)
        {
            bindserch();
        }

        private void drpaccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = conn.getdataset("select distinct " + drpaccount.Text + " from ClientMaster where isactive=1 and " + drpaccount.Text + "!=''");
                if (drpaccount.Text == "AccountName")
                {
                    DataTable dt1 = conn.getdataset("select cusname as " + drpaccount.Text + " from BillMaster where isactive=1 and ClientID='101'");
                    if (dt1.Rows.Count > 0)
                    {
                        dt1 = changedtclone(dt1);
                        dt.Merge(dt1);
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
                    DataTable dt = conn.getdataset("select distinct " + drpitems.Text + " from ProductMaster  where isactive=1 and " + drpitems.Text + "!=''");
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

        private void grdview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
