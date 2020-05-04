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

namespace RamdevSales
{
    public partial class B2B_GSTR2_ : Form
    {
        private Master master;
        private TabControl tabControl;
        OleDbSettings ods = new OleDbSettings();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        DataTable main = new DataTable();
        DataTable path = new DataTable();
        Double totalcess = 0;
        Double totaltax = 0;
        Double totalnetvalue = 0;
        Double totaligst = 0;
        Double totalcgst = 0;
        Double totalsgst = 0;
        string totalbillno = "";
        string totalgstno = "";
        public B2B_GSTR2_()
        {
            InitializeComponent();
        }

        public B2B_GSTR2_(Master master, TabControl tabControl)
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
        private void B2B_GSTR2__Load(object sender, EventArgs e)
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
            }
            catch
            {
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }
        public void importexcel()
        {
            main = new DataTable();

            //     main.Rows.Add("Summary For B2B(4)", "", "", "", "", "", "", "","","","");
            main.Columns.Add("", typeof(string));
            main.Rows.Add("Summary Of Supplies From Registered Suppliers B2B(3)");
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));

            main.Rows.Add("No. of Suppliers", "No. of Invoices", "", "Total Invoice Value", "", "", "", "", "Total Taxable Value", "Total Integrated Tax Paid", "Total Central Tax Paid", "Total TState/UT Tax Paid", "Total Cess", "", "Total Availed ITC Integrated Tax", "Total Availed ITC Central Tax", "Total Availed ITC State/UT Tax", "Total Availed ITC Cess");
            main.Rows.Add(totalgstno, totalbillno, "", totalnetvalue, "", "", "", "", totaltax,totaligst,totalcgst,totalsgst,totalcess, "",totaligst,totalcgst,totalsgst,totalcess);
            main.Rows.Add("GSTIN of Supplier", "Invoice Number", "Invoice Date", "Invoice Value", "Place Of Supply", "Reverse Charge", "Invoice Type", "Rate", "Taxable Value", "Integrated Tax Paid", "Central Tax Paid", "State/UT Tax Paid", "Cess Paid", "Eligibility For ITC", "Availed ITC Integrated Tax", "Availed ITC Central Tax", "Availed ITC State/UT Tax", "Availed ITC Cess");

            // DataTable invoicedt = conn.getdataset("select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.tax) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno   where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='" + "S" + "' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType");
            DataTable invoicedt = conn.getdataset("select GstNo,State,statecode,billno,Bill_Date,totalnet,cgstper,sgstper,igstper,sum(cgstamt) as cgstamt,sum(sgstamt) as sgstamt,sum(igdtamt) as igdtamt,sum(totaltax) as totaltax,totalcess,SaleType,sum(totalt) as totalt,sum(totalc) as totalc from (select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,sum(bp.cgstamt) as cgstamt,sum(bp.sgstamt) as sgstamt,sum(bp.igdtamt) as igdtamt,(bp.cgstamt+bp.sgstamt+bp.igdtamt+bp.addtaxper) as totaltax,b.totalcess,b.SaleType,sum(bp.Total-bp.discountamt) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='P' and bp.BillType='P' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.cgstamt,bp.sgstamt,bp.igdtamt, bp.addtaxper,bp.cgstper,bp.sgstper,bp.igstper, c.GstNo,c.AccountName,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,b.totaltax,b.totalcess,b.SaleType union all select c.GstNo,c.State,c.statecode,b.billno,b.date as Bill_Date,b.totalfinalamount as totalnet,bp.cgstper,bp.sgstper,bp.igstper,sum(bp.cgstamt) as cgstamt,sum(bp.sgstamt) as sgstamt,sum(bp.igstamt) as igdtamt,(bp.cgstamt+bp.sgstamt+bp.igstamt) as totaltax,0 as totalcess,b.type as SaleType,sum(bp.taxableamount) as totalt,sum(0) as totalc from ClientMaster c inner join tblgstvouchermaster b on c.ClientID=b.party inner join tblgstvoucherproductmaster bp on b.billno=bp.billno and (b.party=bp.party or b.id=bp.fkid)where c.isactive=1 and b.isactive=1 and bp.isactive=1 and  (bp.BillType='P' or bp.BillType='EXP') and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.cgstamt,bp.sgstamt,bp.igstamt,bp.cgstper,bp.sgstper,bp.igstper, c.GstNo,c.AccountName,c.State,c.statecode,b.billno,b.Date,b.totalfinalamount,bp.cgstper,bp.sgstper,bp.igstper,b.totxltax,b.type union all select c.GstNo,c.State,c.statecode,b.billno,b.date asBill_Date,b.totalfinalamount,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,sum(case when bp.plusminus='-' then bp.cgst*-1 when bp.plusminus='+' then bp.cgst end) as cgstamt,sum(case when bp.plusminus='-' then bp.sgst*-1 when bp.plusminus='+' then bp.sgst end) as sgstamt,sum(case when bp.plusminus='-' then bp.igst*-1 when bp.plusminus='+' then bp.igst end) as igdtamt,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,0 as totalcess,b.Type,case when bp.plusminus='-' then sum(bp.valueofexp)*-1 when bp.plusminus='+' then sum(bp.valueofexp) end as totalt,0 as totalc from ClientMaster c inner join tblgstvouchermaster b on c.ClientID=b.party inner join tblgstvoucherchargesmaster bp on b.billno=bp.billno and (b.party=bp.party or b.id=bp.fkid)inner join BillSundry bs on bs.BillSundryID=bp.chargeid and bs.OT3=1 and bs.isactive=1 where c.isactive=1 and b.isactive=1 and bp.isactive=1 and (bp.BillType='P' or bp.BillType='EXP') and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.billno,b.Date,b.totalfinalamount,cgst,bp.sgst,bp.plusminus, bp.igst,bp.addtaxamt, b.totxltax,b.Type,bp.tax union all select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,sum(case when bp.plusminus='-' then bp.cgst*-1 when bp.plusminus='+' then bp.cgst end) as cgstamt,sum(case when bp.plusminus='-' then bp.sgst*-1 when bp.plusminus='+' then bp.sgst end) as sgstamt,sum(case when bp.plusminus='-' then bp.igst*-1 when bp.plusminus='+' then bp.igst end) as igdtamt,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,b.totalcess,b.SaleType,case when bp.plusminus='-' then sum(bp.valueofexp)*-1 when bp.plusminus='+' then sum(bp.valueofexp) end as totalt,0 as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1 where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='P' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.plusminus, bp.igst,bp.addtaxamt, b.totaltax,b.totalcess,b.SaleType,bp.tax)entries group by GstNo,State,statecode,billno,Bill_Date,totalnet,totalcess,SaleType,cgstper,sgstper,igstper order by billno");
            for (int i = 0; i < invoicedt.Rows.Count; i++)
            {
                //  DataTable ecomgst = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypeid='" + invoicedt.Rows[i]["SaleType"].ToString() + "' and TaxCalculation='" + "Tax Invoice" + "'");
                DataTable ecomgst = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypeid='" + invoicedt.Rows[i]["SaleType"].ToString() + "'");
                if (ecomgst.Rows.Count > 0)
                {
                    if (ecomgst.Rows[0]["TaxCalculation"].ToString() == "Tax Invoice")
                    {
                        string d = Convert.ToDateTime(invoicedt.Rows[i]["Bill_Date"]).ToString(Master.dateformate);
                        string place = invoicedt.Rows[i]["statecode"].ToString() + "-" + invoicedt.Rows[i]["State"].ToString();
                        double rate = Convert.ToDouble(invoicedt.Rows[i]["cgstper"].ToString()) + Convert.ToDouble(invoicedt.Rows[i]["sgstper"].ToString());
                        double igst = Convert.ToDouble(invoicedt.Rows[i]["igstper"].ToString());
                        string eligibility;
                        if (Convert.ToDouble(invoicedt.Rows[i]["cgstper"].ToString()) == 0 && Convert.ToDouble(invoicedt.Rows[i]["sgstper"].ToString()) == 0 && Convert.ToDouble(invoicedt.Rows[i]["cgstper"].ToString()) == 0 && Convert.ToDouble(invoicedt.Rows[i]["igstper"].ToString()) == 0)
                        {
                            eligibility = "Ineligible";
                        }
                        else
                        {
                            eligibility = "Inputs";
                        }

                        if (ecomgst.Rows[0]["Region"].ToString() == "Local")
                        {
                            main.Rows.Add(invoicedt.Rows[i]["GstNo"].ToString(), invoicedt.Rows[i]["billno"].ToString(), d, invoicedt.Rows[i]["totalnet"].ToString(), place, "N", "Regular", rate, invoicedt.Rows[i]["totalt"].ToString(), invoicedt.Rows[i]["igdtamt"].ToString(), invoicedt.Rows[i]["cgstamt"].ToString(), invoicedt.Rows[i]["sgstamt"].ToString(), invoicedt.Rows[i]["totalc"].ToString(), eligibility, invoicedt.Rows[i]["igdtamt"].ToString(), invoicedt.Rows[i]["cgstamt"].ToString(), invoicedt.Rows[i]["sgstamt"].ToString(), invoicedt.Rows[i]["totalc"].ToString());
                            //totalbillno++;
                        }
                        else
                        {
                            main.Rows.Add(invoicedt.Rows[i]["GstNo"].ToString(), invoicedt.Rows[i]["billno"].ToString(), d, invoicedt.Rows[i]["totalnet"].ToString(), place, "N", "Regular", igst, invoicedt.Rows[i]["totalt"].ToString(), invoicedt.Rows[i]["igdtamt"].ToString(), invoicedt.Rows[i]["cgstamt"].ToString(), invoicedt.Rows[i]["sgstamt"].ToString(), invoicedt.Rows[i]["totalc"].ToString(), eligibility, invoicedt.Rows[i]["igdtamt"].ToString(), invoicedt.Rows[i]["cgstamt"].ToString(), invoicedt.Rows[i]["sgstamt"].ToString(), invoicedt.Rows[i]["totalc"].ToString());
                            // totalbillno++;
                        }

                        totalnetvalue += Convert.ToDouble(invoicedt.Rows[i]["totalnet"].ToString());
                        totaltax += Convert.ToDouble(invoicedt.Rows[i]["totalt"].ToString());
                        totalcess += Convert.ToDouble(invoicedt.Rows[i]["totalc"].ToString());
                        totaligst += Convert.ToDouble(invoicedt.Rows[i]["igdtamt"].ToString());
                        totalsgst += Convert.ToDouble(invoicedt.Rows[i]["sgstamt"].ToString());
                        totalcgst += Convert.ToDouble(invoicedt.Rows[i]["cgstamt"].ToString());
                    }
                }
            }
            DataTable finalbill = conn.getdataset("select DISTINCT GstNo from (select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,sum(bp.cgstamt) as cgstamt,sum(bp.sgstamt) as sgstamt,sum(bp.igdtamt) as igdtamt,(bp.cgstamt+bp.sgstamt+bp.igdtamt+bp.addtaxper) as totaltax,b.totalcess,b.SaleType,sum(bp.Total-bp.discountamt) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='P' and bp.BillType='P' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.cgstamt,bp.sgstamt,bp.igdtamt, bp.addtaxper,bp.cgstper,bp.sgstper,bp.igstper, c.GstNo,c.AccountName,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,b.totaltax,b.totalcess,b.SaleType union all select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,sum(case when bp.plusminus='-' then bp.cgst*-1 when bp.plusminus='+' then bp.cgst end) as cgstamt,sum(case when bp.plusminus='-' then bp.sgst*-1 when bp.plusminus='+' then bp.sgst end) as sgstamt,sum(case when bp.plusminus='-' then bp.igst*-1 when bp.plusminus='+' then bp.igst end) as igdtamt,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,b.totalcess,b.SaleType,case when bp.plusminus='-' then sum(bp.valueofexp)*-1 when bp.plusminus='+' then sum(bp.valueofexp) end as totalt,0 as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1 where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='P' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.plusminus, bp.igst,bp.addtaxamt, b.totaltax,b.totalcess,b.SaleType,bp.tax)entries");
            DataTable final = conn.getdataset("select DISTINCT billno from (select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,sum(bp.cgstamt) as cgstamt,sum(bp.sgstamt) as sgstamt,sum(bp.igdtamt) as igdtamt,(bp.cgstamt+bp.sgstamt+bp.igdtamt+bp.addtaxper) as totaltax,b.totalcess,b.SaleType,sum(bp.Total-bp.discountamt) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='P' and bp.BillType='P' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.cgstamt,bp.sgstamt,bp.igdtamt, bp.addtaxper,bp.cgstper,bp.sgstper,bp.igstper, c.GstNo,c.AccountName,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,b.totaltax,b.totalcess,b.SaleType union all select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,sum(case when bp.plusminus='-' then bp.cgst*-1 when bp.plusminus='+' then bp.cgst end) as cgstamt,sum(case when bp.plusminus='-' then bp.sgst*-1 when bp.plusminus='+' then bp.sgst end) as sgstamt,sum(case when bp.plusminus='-' then bp.igst*-1 when bp.plusminus='+' then bp.igst end) as igdtamt,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,b.totalcess,b.SaleType,case when bp.plusminus='-' then sum(bp.valueofexp)*-1 when bp.plusminus='+' then sum(bp.valueofexp) end as totalt,0 as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1 where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='P' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.plusminus, bp.igst,bp.addtaxamt, b.totaltax,b.totalcess,b.SaleType,bp.tax)entries");
            totalbillno = Convert.ToString(final.Rows.Count);
            totalgstno = Convert.ToString(finalbill.Rows.Count);
            main.Rows[2][0] = totalgstno;
            main.Rows[2][1] = totalbillno;
            main.Rows[2][3] = totalnetvalue;
            main.Rows[2][8] = totaltax;
            main.Rows[2][9] = totaligst;
            main.Rows[2][10] = totalsgst;
            main.Rows[2][11] = totalcgst;
            main.Rows[2][12] = totalcess;
            main.Rows[2][14] = totaligst;
            main.Rows[2][15] = totalsgst;
            main.Rows[2][16] = totalcgst;
            main.Rows[2][17] = totalcess;

            dataGridView1.DataSource = main;
            //   griddesign();
            DataSet ds2 = ods.getdata("select * from Path");
            path = ds2.Tables[0];

        }
        public void exportexcel()
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Worksheet xlsht = new Microsoft.Office.Interop.Excel.Worksheet();
            xlApp.Visible = true;
            string appPath = path.Rows[0]["DefaultPath"].ToString();
            string path1 = appPath + @"\DefaultGSTReports\GSTR2.xlsx";
            //xlsht = xlApp.Application.Workbooks.Open(path).Worksheets["b2b"];
            //  string path11 = @"C:\Users\admin\Desktop\Test.xlsx";
            xlsht = xlApp.Application.Workbooks.Open(path1).Worksheets["b2b"];
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                xlsht.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText;
            }

            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    if (dataGridView1[j, i].ValueType == typeof(string))
                    {
                        xlsht.Cells[i + 2, j + 1] = "" + dataGridView1[j, i].Value.ToString();
                    }
                    else
                    {
                        xlsht.Cells[i + 2, j + 1] = dataGridView1[j, i].Value.ToString();
                    }
                }
            }
            xlsht.Rows[1].Delete();
        }
        public void binddata()
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                LVclient.Columns.Clear();
                LVclient.Items.Clear();
                main = new DataTable();
                main.Columns.Add("GSTIN of Supplier", typeof(string));
                main.Columns.Add("Invoice Number", typeof(string));
                main.Columns.Add("Invoice Date", typeof(string));
                main.Columns.Add("Invoice Value", typeof(string));
                main.Columns.Add("Place Of Supply", typeof(string));
                main.Columns.Add("Reverse Charge", typeof(string));
                main.Columns.Add("Invoice Type", typeof(string));
                main.Columns.Add("Rate", typeof(string));
                main.Columns.Add("Taxable Value", typeof(string));
                main.Columns.Add("Integrated Tax Paid", typeof(string));
                main.Columns.Add("Central Tax Paid", typeof(string));
                main.Columns.Add("State/UT Tax Paid", typeof(string));
                main.Columns.Add("Cess Paid", typeof(string));
                main.Columns.Add("Eligibility For ITC", typeof(string));
                main.Columns.Add("Availed ITC Integrated Tax", typeof(string));
                main.Columns.Add("Availed ITC Central Tax", typeof(string));
                main.Columns.Add("Availed ITC State/UT Tax", typeof(string));
                main.Columns.Add("Availed ITC Cess", typeof(string));

                //DataTable invoicedt = conn.getdataset("select GstNo,AccountName,State,statecode,billno,Bill_Date,totalnet,cgstper,sgstper,igstper,sum(totaltax),totalcess,SaleType,sum(totalt) as totalt,sum(totalc) as totalc from (select c.GstNo,c.AccountName,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.Total-bp.discountamt) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='P' and bp.BillType='P' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.AccountName,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType union all select c.GstNo,c.AccountName,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,b.totalcess,b.SaleType,case when bp.plusminus='-' then sum(bp.valueofexp)*-1 when bp.plusminus='+' then sum(bp.valueofexp) end as totalt,0 as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1 where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='P' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.AccountName,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.plusminus, bp.igst,bp.addtaxamt, b.totaltax,b.totalcess,b.SaleType,bp.tax) entries group by GstNo,AccountName,State,statecode,billno,Bill_Date,totalnet,cgstper,sgstper,igstper,totalcess,SaleType order by billno");
               // DataTable invoicedt = conn.getdataset("select GstNo,State,statecode,billno,Bill_Date,totalnet,cgstper,sgstper,igstper,sum(cgstamt) as cgstamt,sum(sgstamt) as sgstamt,sum(igdtamt) as igdtamt,sum(totaltax) as totaltax,totalcess,SaleType,sum(totalt) as totalt,sum(totalc) as totalc from (select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,sum(bp.cgstamt) as cgstamt,sum(bp.sgstamt) as sgstamt,sum(bp.igdtamt) as igdtamt,(bp.cgstamt+bp.sgstamt+bp.igdtamt+bp.addtaxper) as totaltax,b.totalcess,b.SaleType,sum(bp.Total-bp.discountamt) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='P' and bp.BillType='P' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.cgstamt,bp.sgstamt,bp.igdtamt, bp.addtaxper,bp.cgstper,bp.sgstper,bp.igstper, c.GstNo,c.AccountName,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,b.totaltax,b.totalcess,b.SaleType union all select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,sum(case when bp.plusminus='-' then bp.cgst*-1 when bp.plusminus='+' then bp.cgst end) as cgstamt,sum(case when bp.plusminus='-' then bp.sgst*-1 when bp.plusminus='+' then bp.sgst end) as sgstamt,sum(case when bp.plusminus='-' then bp.igst*-1 when bp.plusminus='+' then bp.igst end) as igdtamt,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,b.totalcess,b.SaleType,case when bp.plusminus='-' then sum(bp.valueofexp)*-1 when bp.plusminus='+' then sum(bp.valueofexp) end as totalt,0 as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1 where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='P' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.plusminus, bp.igst,bp.addtaxamt, b.totaltax,b.totalcess,b.SaleType,bp.tax)entries group by GstNo,State,statecode,billno,Bill_Date,totalnet,totalcess,SaleType,cgstper,sgstper,igstper order by billno");
               // DataTable invoicedt = conn.getdataset("select GstNo,State,statecode,billno,Bill_Date,totalnet,cgstper,sgstper,igstper,sum(cgstamt) as cgstamt,sum(sgstamt) as sgstamt,sum(igdtamt) as igdtamt,sum(totaltax) as totaltax,totalcess,SaleType,sum(totalt) as totalt,sum(totalc) as totalc from (select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,sum(bp.cgstamt) as cgstamt,sum(bp.sgstamt) as sgstamt,sum(bp.igdtamt) as igdtamt,(bp.cgstamt+bp.sgstamt+bp.igdtamt+bp.addtaxper) as totaltax,b.totalcess,b.SaleType,sum(bp.Total-bp.discountamt) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='P' and bp.BillType='P' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.cgstamt,bp.sgstamt,bp.igdtamt, bp.addtaxper,bp.cgstper,bp.sgstper,bp.igstper, c.GstNo,c.AccountName,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,b.totaltax,b.totalcess,b.SaleType union all select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,sum(case when bp.plusminus='-' then bp.cgst*-1 when bp.plusminus='+' then bp.cgst end) as cgstamt,sum(case when bp.plusminus='-' then bp.sgst*-1 when bp.plusminus='+' then bp.sgst end) as sgstamt,sum(case when bp.plusminus='-' then bp.igst*-1 when bp.plusminus='+' then bp.igst end) as igdtamt,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,b.totalcess,b.SaleType,case when bp.plusminus='-' then sum(bp.valueofexp)*-1 when bp.plusminus='+' then sum(bp.valueofexp) end as totalt,0 as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1 where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='P' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.plusminus, bp.igst,bp.addtaxamt, b.totaltax,b.totalcess,b.SaleType,bp.tax)entries group by GstNo,State,statecode,billno,Bill_Date,totalnet,totalcess,SaleType,cgstper,sgstper,igstper order by billno");
                DataTable invoicedt = conn.getdataset("select GstNo,State,statecode,billno,Bill_Date,totalnet,cgstper,sgstper,igstper,sum(cgstamt) as cgstamt,sum(sgstamt) as sgstamt,sum(igdtamt) as igdtamt,sum(totaltax) as totaltax,totalcess,SaleType,sum(totalt) as totalt,sum(totalc) as totalc from (select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,sum(bp.cgstamt) as cgstamt,sum(bp.sgstamt) as sgstamt,sum(bp.igdtamt) as igdtamt,(bp.cgstamt+bp.sgstamt+bp.igdtamt+bp.addtaxper) as totaltax,b.totalcess,b.SaleType,sum(bp.Total-bp.discountamt) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='P' and bp.BillType='P' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.cgstamt,bp.sgstamt,bp.igdtamt, bp.addtaxper,bp.cgstper,bp.sgstper,bp.igstper, c.GstNo,c.AccountName,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,b.totaltax,b.totalcess,b.SaleType union all select c.GstNo,c.State,c.statecode,b.billno,b.date as Bill_Date,b.totalfinalamount as totalnet,bp.cgstper,bp.sgstper,bp.igstper,sum(bp.cgstamt) as cgstamt,sum(bp.sgstamt) as sgstamt,sum(bp.igstamt) as igdtamt,(bp.cgstamt+bp.sgstamt+bp.igstamt) as totaltax,0 as totalcess,b.type as SaleType,sum(bp.taxableamount) as totalt,sum(0) as totalc from ClientMaster c inner join tblgstvouchermaster b on c.ClientID=b.party inner join tblgstvoucherproductmaster bp on b.billno=bp.billno and (b.party=bp.party or b.id=bp.fkid)where c.isactive=1 and b.isactive=1 and bp.isactive=1 and  (bp.BillType='P' or bp.BillType='EXP') and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.cgstamt,bp.sgstamt,bp.igstamt,bp.cgstper,bp.sgstper,bp.igstper, c.GstNo,c.AccountName,c.State,c.statecode,b.billno,b.Date,b.totalfinalamount,bp.cgstper,bp.sgstper,bp.igstper,b.totxltax,b.type union all select c.GstNo,c.State,c.statecode,b.billno,b.date asBill_Date,b.totalfinalamount,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,sum(case when bp.plusminus='-' then bp.cgst*-1 when bp.plusminus='+' then bp.cgst end) as cgstamt,sum(case when bp.plusminus='-' then bp.sgst*-1 when bp.plusminus='+' then bp.sgst end) as sgstamt,sum(case when bp.plusminus='-' then bp.igst*-1 when bp.plusminus='+' then bp.igst end) as igdtamt,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,0 as totalcess,b.Type,case when bp.plusminus='-' then sum(bp.valueofexp)*-1 when bp.plusminus='+' then sum(bp.valueofexp) end as totalt,0 as totalc from ClientMaster c inner join tblgstvouchermaster b on c.ClientID=b.party inner join tblgstvoucherchargesmaster bp on b.billno=bp.billno and (b.party=bp.party or b.id=bp.fkid)inner join BillSundry bs on bs.BillSundryID=bp.chargeid and bs.OT3=1 and bs.isactive=1 where c.isactive=1 and b.isactive=1 and bp.isactive=1 and (bp.BillType='P' or bp.BillType='EXP') and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.billno,b.Date,b.totalfinalamount,cgst,bp.sgst,bp.plusminus, bp.igst,bp.addtaxamt, b.totxltax,b.Type,bp.tax union all select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,sum(case when bp.plusminus='-' then bp.cgst*-1 when bp.plusminus='+' then bp.cgst end) as cgstamt,sum(case when bp.plusminus='-' then bp.sgst*-1 when bp.plusminus='+' then bp.sgst end) as sgstamt,sum(case when bp.plusminus='-' then bp.igst*-1 when bp.plusminus='+' then bp.igst end) as igdtamt,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,b.totalcess,b.SaleType,case when bp.plusminus='-' then sum(bp.valueofexp)*-1 when bp.plusminus='+' then sum(bp.valueofexp) end as totalt,0 as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1 where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='P' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.plusminus, bp.igst,bp.addtaxamt, b.totaltax,b.totalcess,b.SaleType,bp.tax)entries group by GstNo,State,statecode,billno,Bill_Date,totalnet,totalcess,SaleType,cgstper,sgstper,igstper order by billno");
                for (int i = 0; i < invoicedt.Rows.Count; i++)
                {
                    DataTable ecomgst = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypeid='" + invoicedt.Rows[i]["SaleType"].ToString() + "'");
                    if (ecomgst.Rows.Count > 0)
                    {
                        if (ecomgst.Rows[0]["TaxCalculation"].ToString() == "Tax Invoice")
                        {
                            DataRow dr = main.NewRow();
                            string d = Convert.ToDateTime(invoicedt.Rows[i]["Bill_Date"]).ToString(Master.dateformate);
                            dr["GSTIN of Supplier"] = invoicedt.Rows[i]["GstNo"].ToString();
                            dr["Invoice Number"] = invoicedt.Rows[i]["billno"].ToString();
                            dr["Invoice Date"] = d;
                            dr["Invoice Value"] = invoicedt.Rows[i]["totalnet"].ToString();
                            string place = invoicedt.Rows[i]["statecode"].ToString() + "-" + invoicedt.Rows[i]["State"].ToString();
                            dr["Place Of Supply"] = place;
                            dr["Reverse Charge"] = "N";
                            dr["Invoice Type"] = "Regular";
                            double rate = Convert.ToDouble(invoicedt.Rows[i]["cgstper"].ToString()) + Convert.ToDouble(invoicedt.Rows[i]["sgstper"].ToString());
                            double igst = Convert.ToDouble(invoicedt.Rows[i]["igstper"].ToString());
                            if (ecomgst.Rows[0]["Region"].ToString() == "Local")
                            {
                                dr["Rate"] = rate;
                            }
                            else
                            {
                                dr["Rate"] = igst;
                            }
                            dr["Taxable Value"] = invoicedt.Rows[i]["totalt"].ToString();
                            dr["Integrated Tax Paid"] = invoicedt.Rows[i]["igdtamt"].ToString();
                            dr["Central Tax Paid"] = invoicedt.Rows[i]["cgstamt"].ToString();
                            dr["State/UT Tax Paid"] = invoicedt.Rows[i]["sgstamt"].ToString();
                            dr["Cess Paid"] = invoicedt.Rows[i]["totalc"].ToString();
                            if (Convert.ToDouble(invoicedt.Rows[i]["cgstper"].ToString()) == 0 && Convert.ToDouble(invoicedt.Rows[i]["sgstper"].ToString()) == 0 && Convert.ToDouble(invoicedt.Rows[i]["cgstper"].ToString()) == 0 && Convert.ToDouble(invoicedt.Rows[i]["igstper"].ToString()) == 0)
                            {
                                dr["Eligibility For ITC"] = "Ineligible";
                            }
                            else
                            {
                                dr["Eligibility For ITC"] = "Inputs";
                            }
                            dr["Availed ITC Integrated Tax"] = invoicedt.Rows[i]["igdtamt"].ToString();
                            dr["Availed ITC Central Tax"] = invoicedt.Rows[i]["cgstamt"].ToString();
                            dr["Availed ITC State/UT Tax"] = invoicedt.Rows[i]["sgstamt"].ToString();
                            dr["Availed ITC Cess"] = invoicedt.Rows[i]["totalc"].ToString();
                                main.Rows.Add(dr);
                        }
                    }
                }
                LVclient.Items.Clear();
                int ColCount = main.Columns.Count;
                //Add columns
                for (int k = 0; k < ColCount; k++)
                {
                    LVclient.Columns.Add(main.Columns[k].ColumnName,200);
                }
                // Display items in the ListView control
                for (int i = 0; i < main.Rows.Count; i++)
                {
                    DataRow drow = main.Rows[i];

                    // Only row that have not been deleted
                    if (drow.RowState != DataRowState.Deleted)
                    {
                        // Define the list items
                        ListViewItem lvi = new ListViewItem(drow[0].ToString());
                        for (int j = 1; j < ColCount; j++)
                        {
                            lvi.SubItems.Add(drow[j].ToString());
                        }
                        // Add the list items to the ListView
                        LVclient.Items.Add(lvi);
                    }
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
        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            binddata();
        }

        private void btnexcel_Click(object sender, EventArgs e)
        {
            importexcel();
            exportexcel();
        }
    }
}
