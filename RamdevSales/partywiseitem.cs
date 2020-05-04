using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using ClosedXML.Excel;

namespace RamdevSales
{
    public partial class partywiseitem : Form
    {
        private Master master;
        private TabControl tabControl;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        OleDbSettings ods = new OleDbSettings();
        Printing prn = new Printing();
        public partywiseitem()
        {
            InitializeComponent();
        }

        public partywiseitem(Master master, TabControl tabControl)
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
        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }

        private void partywiseitem_Load(object sender, EventArgs e)
        {
            try
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
            }
            catch
            {
            }
        }
        DataTable maindt = new DataTable();
        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            bindgrid();
        }

        public void bindgrid()
        {
            try
            {
                DataRow dr;
                grdview.DataSource = null;
                if (cmbselectitemrate.Text != "")
                {
                    string itemselectrate = "";
                    if (cmbselectitemrate.Text == "Basic Price")
                    {
                        itemselectrate = "BasicPrice";
                    }
                    else if (cmbselectitemrate.Text == "MRP")
                    {
                        itemselectrate = "MRP";
                    }
                    else if (cmbselectitemrate.Text == "Sale Price")
                    {
                        itemselectrate = "SalePrice";
                    }
                    else if (cmbselectitemrate.Text == "Purchase Price")
                    {
                        itemselectrate = "PurchasePrice";
                    }
                    maindt = conn.getdataset("select c.AccountName,bp.Productname,i.GroupName,co.companyname,sum(bp.Pqty) as Totalqty, sum(bp.amount) as Amount,max(pp." + itemselectrate + ") as " + itemselectrate + " from BillProductMaster bp inner join BillMaster b on b.billno=bp.billno inner join ClientMaster c on c.ClientID=b.ClientID inner join ProductMaster i on i.Product_Name=bp.Productname inner join productpricemaster pp on pp.productid=i.productid and pp.isactive=1 inner join CompanyMaster co on co.CompanyID=i.CompanyID where i.isactive=1 and b.isactive=1 and bp.isactive=1 and c.isactive=1 and bp.BillType='S' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.AccountName,bp.Productname,i.GroupName,co.companyname order by AccountName");
                    dr = maindt.NewRow();
                    for (int i = 4; i < maindt.Columns.Count; i++)
                    {
                        double colstot = 0;
                        for (int j = 0; j < maindt.Rows.Count; j++)
                        {
                            colstot += Convert.ToDouble(maindt.Rows[j][i].ToString());
                            dr[maindt.Columns[i].ColumnName] = colstot;
                        }
                    }
                    maindt.Rows.Add();
                    maindt.Rows.Add(dr);
                    grdview.DataSource = maindt;
                    grdview.Columns[0].Width = 300;
                    grdview.Columns[1].Width = 400;
                    grdview.Columns[2].Width = 100;
                    grdview.Columns[3].Width = 100;
                    grdview.Columns[4].Width = 100;
                    grdview.Columns[5].Width = 100;
                    grdview.Columns[6].Width = 100;
                }
                else
                {
                    MessageBox.Show("Please select any of the item rate");
                }

                
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
                            wb.Worksheets.Add(maindt, "SaleItem");
                            // wb.Worksheets.Add(dt1, "ItemPrice");
                            wb.SaveAs(folderPath + "Sale_Management" + DateTimeName + ".xlsx");
                        }
                        MessageBox.Show("Export Data Sucessfully");
                        DialogResult dr = MessageBox.Show("Do you want to Open Document?", "Sale Management", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(folderPath + "Sale_Management" + DateTimeName + ".xlsx");
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

        private void DTPFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DTPTo.Focus();
            }
        }

        private void DTPTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnViewReport.Focus();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbFilter.Text) || string.IsNullOrEmpty(txtSearch.Text))
                {
                    bindgrid();
                }
                else
                {
                    string itemselectrate = "";
                    if (cmbselectitemrate.Text != "")
                    {
                        if (cmbselectitemrate.Text == "Basic Price")
                        {
                            itemselectrate = "BasicPrice";
                        }
                        else if (cmbselectitemrate.Text == "MRP")
                        {
                            itemselectrate = "MRP";
                        }
                        else if (cmbselectitemrate.Text == "Sale Price")
                        {
                            itemselectrate = "SalePrice";
                        }
                        else if (cmbselectitemrate.Text == "Purchase Price")
                        {
                            itemselectrate = "PurchasePrice";
                        }
                    }
                    if (cmbFilter.Text == "ItemName")
                    {
                        DataTable dtfilter = (DataTable)grdview.DataSource;
                        DataRow dr1;
                        DataRow[] dr = dtfilter.Select("Productname like '%" + txtSearch.Text + "%'");
                        if (dr.Length > 0)
                        {
                            DataTable dtcopydr = dr.CopyToDataTable();
                            dr1 = dtcopydr.NewRow();
                            dr1["AccountName"] = "";
                            dr1["Productname"] = "";
                            dr1["GroupName"] = "";
                            dr1["companyname"] = "";
                            double totQty = 0.0, Amt = 0.0;

                            for (int i = 0; i < dtcopydr.Rows.Count; i++)
                            {
                                totQty += (double.Parse(dtcopydr.Rows[i]["Totalqty"].ToString()));
                                Amt += (double.Parse(dtcopydr.Rows[i]["Amount"].ToString()));
                            }
                            dr1["Totalqty"] = totQty;
                            dr1["Amount"] = Amt;
                            dtcopydr.Rows.Add();
                            dtcopydr.Rows.Add(dr1);
                            dr1[itemselectrate] = 0;
                            grdview.DataSource = dtcopydr;
                            grdview.Columns[0].Width = 300;
                            grdview.Columns[1].Width = 400;
                            grdview.Columns[2].Width = 100;
                            grdview.Columns[3].Width = 100;
                            grdview.Columns[4].Width = 100;
                            grdview.Columns[5].Width = 100;
                            grdview.Columns[6].Width = 100;

                        }
                        else
                        {
                            MessageBox.Show("There are no Item in Search criteria..");
                            bindgrid();
                        }
                    }
                    else if (cmbFilter.Text == "GroupName")
                    {
                        DataTable dtfilter = (DataTable)grdview.DataSource;
                        DataRow dr1;
                        DataRow[] dr = dtfilter.Select("companyname like '%" + txtSearch.Text + "%'");
                        if (dr.Length > 0)
                        {
                            DataTable dtcopydr = dr.CopyToDataTable();
                            dr1 = dtcopydr.NewRow();
                            dr1["AccountName"] = "";
                            dr1["Productname"] = "";
                            dr1["GroupName"] = "";
                            dr1["companyname"] = "";
                            double totQty = 0.0, Amt = 0.0;

                            for (int i = 0; i < dtcopydr.Rows.Count; i++)
                            {
                                totQty += (double.Parse(dtcopydr.Rows[i]["Totalqty"].ToString()));
                                Amt += (double.Parse(dtcopydr.Rows[i]["Amount"].ToString()));
                            }
                            dr1["Totalqty"] = totQty;
                            dr1["Amount"] = Amt;
                            dtcopydr.Rows.Add();
                            dtcopydr.Rows.Add(dr1);
                            dr1[itemselectrate] = 0;
                            grdview.DataSource = dtcopydr;
                            grdview.Columns[0].Width = 300;
                            grdview.Columns[1].Width = 400;
                            grdview.Columns[2].Width = 100;
                            grdview.Columns[3].Width = 100;
                            grdview.Columns[4].Width = 100;
                            grdview.Columns[5].Width = 100;

                        }
                        else
                        {
                            MessageBox.Show("There are no companyname in Search criteria..");
                            bindgrid();
                        }
                    }
                    else if (cmbFilter.Text == "companyname")
                    {
                        DataTable dtfilter = (DataTable)grdview.DataSource;
                        DataRow dr1;
                        DataRow[] dr = dtfilter.Select("companyname like '%" + txtSearch.Text + "%'");
                        if (dr.Length > 0)
                        {
                            DataTable dtcopydr = dr.CopyToDataTable();
                            dr1 = dtcopydr.NewRow();
                            dr1["AccountName"] = "";
                            dr1["Productname"] = "";
                            dr1["GroupName"] = "";
                            dr1["companyname"] = "";
                            double totQty = 0.0, Amt = 0.0;

                            for (int i = 0; i < dtcopydr.Rows.Count; i++)
                            {
                                totQty += (double.Parse(dtcopydr.Rows[i]["Totalqty"].ToString()));
                                Amt += (double.Parse(dtcopydr.Rows[i]["Amount"].ToString()));
                            }
                            dr1["Totalqty"] = totQty;
                            dr1["Amount"] = Amt;
                            dtcopydr.Rows.Add();
                            dtcopydr.Rows.Add(dr1);
                            dr1[itemselectrate] = 0;
                            grdview.DataSource = dtcopydr;
                            grdview.Columns[0].Width = 300;
                            grdview.Columns[1].Width = 400;
                            grdview.Columns[2].Width = 100;
                            grdview.Columns[3].Width = 100;
                            grdview.Columns[4].Width = 100;
                            grdview.Columns[5].Width = 100;

                        }
                        else
                        {
                            MessageBox.Show("There are no companyname in Search criteria..");
                            bindgrid();
                        }
                    }
                    else
                    {
                        DataTable dtfilter = (DataTable)grdview.DataSource;
                        DataRow dr1;
                        DataRow[] dr = dtfilter.Select("AccountName like '%" + txtSearch.Text + "%'");
                        if (dr.Length > 0)
                        {
                            DataTable dtcopydr = dr.CopyToDataTable();
                            dr1 = dtcopydr.NewRow();
                            dr1["AccountName"] = "";
                            dr1["Productname"] = "";
                            dr1["GroupName"] = "";
                            dr1["companyname"] = "";
                            double totQty = 0.0, Amt = 0.0;

                            for (int i = 0; i < dtcopydr.Rows.Count; i++)
                            {
                                totQty += (double.Parse(dtcopydr.Rows[i]["Totalqty"].ToString()));
                                Amt += (double.Parse(dtcopydr.Rows[i]["Amount"].ToString()));
                            }
                            dr1["Totalqty"] = totQty;
                            dr1["Amount"] = Amt;
                            dtcopydr.Rows.Add();
                            dtcopydr.Rows.Add(dr1);
                            dr1[itemselectrate] = 0;
                            grdview.DataSource = dtcopydr;
                            grdview.Columns[0].Width = 300;
                            grdview.Columns[1].Width = 400;
                            grdview.Columns[2].Width = 100;
                            grdview.Columns[3].Width = 100;
                            grdview.Columns[4].Width = 100;
                            grdview.Columns[5].Width = 100;

                        }
                        else
                        {
                            MessageBox.Show("There are no Client in Search criteria..");
                            bindgrid();
                        }
                    }
                }
            }
            catch (Exception excp)
            {

            }
        }
    }
}
