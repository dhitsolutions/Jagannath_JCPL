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
using System.IO;
using ClosedXML.Excel;

namespace RamdevSales
{
    public partial class B2Cl : Form
    {
        private Master master;
        private TabControl tabControl;
        OleDbSettings ods = new OleDbSettings();
        DataTable main = new DataTable();
        DataTable path = new DataTable();
        Double totalcess = 0;
        Double totaltax = 0;
        Double totalnetvalue = 0;
        string totalbillno = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        public B2Cl()
        {
            InitializeComponent();
        }

        public B2Cl(Master master, TabControl tabControl)
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
        public void bindexcelold()
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
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(main, "GST Register");
                            // wb.Worksheets.Add(dt1, "ItemPrice");
                            wb.SaveAs(folderPath + "GST Register(B2Cl)" + DateTimeName + ".xlsx");
                        }
                        DialogResult dr = MessageBox.Show("Do you want to Open Document?", "GST Register(B2Cl)", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(folderPath + "GST Register(B2Cl)" + DateTimeName + ".xlsx");
                            String pathToExecutable = "AcroRd32.exe";
                        }
                    }
                }

                // MessageBox.Show("Export Data Sucessfully");
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void btnexcel_Click(object sender, EventArgs e)
        {
            importexcel();
            exportexcel();
        }

        private void B2Cl_Load(object sender, EventArgs e)
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
        public void importexcel()
        {
            main = new DataTable();

            //     main.Rows.Add("Summary For B2B(4)", "", "", "", "", "", "", "","","","");
            main.Columns.Add("", typeof(string));
            main.Rows.Add("Summary For B2CL(5)");
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Rows.Add("No. of Invoices", "", "Total Inv Value", "", "", "Total Taxable Value", "Total Cess","");
            main.Rows.Add(totalbillno, "",totalnetvalue, "", "",totaltax,totalcess);
            main.Rows.Add("Invoice Number", "Invoice date","Invoice Value", "Place Of Supply", "Rate", "Taxable Value","Cess Amount","E-Commerce GSTIN");
            DataTable invoicedt = conn.getdataset("select state,statecode, billno, bill_date,totalnet, cgstper, sgstper, igstper, sum(totaltax) as totaltax,totalcess,saletype,sum(totalt) as totalt,sum(totalc) as totalc from (select c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,(bp.cgstamt+bp.sgstamt+bp.igdtamt+bp.addtaxper) as totaltax,b.totalcess,b.SaleType,sum(bp.Total-bp.DiscountAmt) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno   where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and bp.BillType='S' and b.totalnet>250000 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.cgstamt,bp.sgstamt,bp.igdtamt,bp.addtaxper, c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType union select c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,b.totalcess,b.SaleType,sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as totalt,0 as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillchargesMaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1   where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and b.totalnet>250000 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,bp.plusminus,bp.addtaxamt, c.statecode,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleType,bp.tax union select b.companystate as State,b.statecode,b.billno,b.BillDate as Bill_Date,b.totalnet,(bp.igst/2) as cgstper,(bp.igst/2) as sgstper,bp.igst as igstper,b.totaltax,b.totalcess,b.SaleTypeid as SaleType,sum(bp.Amount-bp.DiscountAmt) as totalt,sum(bp.cess) as totalc from BillPOSMaster b  inner join BillPOSProductMaster bp on b.billno=bp.billno   where b.isactive=1 and bp.isactive=1  and b.totalnet>250000 and b.BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'and b.BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by b.companystate,b.statecode,b.billno,b.BillDate,b.totalnet,bp.cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleTypeid union select c.State,c.statecode,b.billno,b.date,b.totalfinalamount,bp.cgstper,bp.sgstper,bp.igstper,(bp.cgstamt+bp.sgstamt+bp.igstamt+bp.addtax) as totaltax,0 as totalcess,b.type,sum(bp.taxableamount) as totalt,0 as totalc from ClientMaster c inner join tblgstvouchermaster b on c.ClientID=b.party inner join tblgstvoucherproductmaster bp on b.billno=bp.billno  and (b.party=bp.party or bp.fkid=b.id) where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and bp.BillType='S' and b.totalfinalamount>250000 and b.date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.cgstamt,bp.sgstamt,bp.igstamt,bp.addtax, c.State,c.statecode,b.billno,b.Date,b.totalfinalamount,bp.cgstper,bp.sgstper,bp.igstper,b.totxltax,b.Type union select c.State,c.statecode,b.billno,b.date,b.totalfinalamount,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,0 as totalcess,b.Type,sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as totalt,0 as totalc from ClientMaster c inner join tblgstvouchermaster b on c.ClientID=b.party inner join tblgstvoucherchargesmaster bp on b.billno=bp.billno and (b.party=bp.party or bp.fkid=b.id) inner join BillSundry bs on bs.BillSundryID=bp.chargeid and bs.OT3=1 and bs.isactive=1   where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and b.totalfinalamount>250000 and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,bp.plusminus,bp.addtaxamt, c.statecode,b.billno,b.Date,b.totalfinalamount,cgst,bp.sgst,bp.igst,b.totxltax,b.Type,bp.tax ) entries group by state,statecode, billno, bill_date,totalnet, cgstper, sgstper, igstper, totalcess,saletype order by billno");
             for (int i = 0; i < invoicedt.Rows.Count; i++)
             {
                 DataTable ecomgst = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypeid='" + invoicedt.Rows[i]["SaleType"].ToString() + "'");
                 if (ecomgst.Rows[0]["TaxCalculation"].ToString() == "Bill Of Supply" && ecomgst.Rows[0]["Region"].ToString() == "central")
                 {
                     string d = Convert.ToDateTime(invoicedt.Rows[i]["Bill_Date"]).ToString(Master.dateformate);
                     string place = invoicedt.Rows[i]["statecode"].ToString() + "-" + invoicedt.Rows[i]["State"].ToString();
                     double rate = Convert.ToDouble(invoicedt.Rows[i]["cgstper"].ToString()) + Convert.ToDouble(invoicedt.Rows[i]["sgstper"].ToString());
                     double igst = Convert.ToDouble(invoicedt.Rows[i]["igstper"].ToString());
                     if (ecomgst.Rows[0]["Region"].ToString() == "Local")
                     {
                         main.Rows.Add(invoicedt.Rows[i]["billno"].ToString(), d, invoicedt.Rows[i]["totalnet"].ToString(), place, rate, invoicedt.Rows[i]["totalt"].ToString(), invoicedt.Rows[i]["totalc"].ToString(), ecomgst.Rows[0]["txtecom"].ToString());
                     }
                     else
                     {
                         main.Rows.Add(invoicedt.Rows[i]["billno"].ToString(), d, invoicedt.Rows[i]["totalnet"].ToString(), place, igst, invoicedt.Rows[i]["totalt"].ToString(), invoicedt.Rows[i]["totalc"].ToString(), ecomgst.Rows[0]["txtecom"].ToString());
                     }
                     totalnetvalue += Convert.ToDouble(invoicedt.Rows[i]["totalnet"].ToString());
                     totaltax += Convert.ToDouble(invoicedt.Rows[i]["totalt"].ToString());
                     totalcess += Convert.ToDouble(invoicedt.Rows[i]["totalc"].ToString());
                 }
             }
            // DataTable finalbill = conn.getdataset("select DISTINCT billno  from (select c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.Tax) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno inner join PurchasetypeMaster p on p.Purchasetypeid=b.SaleType   where p.TaxCalculation='Bill Of Supply' and  p.isactive=1 and c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and b.totalnet>250000 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType union select c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.cgst+bp.sgst+bp.igst) as totalt,0 as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillchargesMaster bp on b.billno=bp.billno inner join PurchasetypeMaster p on p.Purchasetypeid=b.SaleType   where p.TaxCalculation='Bill Of Supply' and  p.isactive=1 and c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and b.totalnet>250000 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'group by c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleType,bp.tax union select b.companystate as State,b.statecode,b.billno,b.BillDate as Bill_Date,b.totalnet,(bp.igst/2) as cgstper,(bp.igst/2) as sgstper,bp.igst as igstper,b.totaltax,b.totalcess,b.SaleTypeid as SaleType,sum(bp.sgst + bp.cgst) as totalt,sum(bp.cess) as totalc from BillPOSMaster b  inner join BillPOSProductMaster bp on b.billno=bp.billno inner join PurchasetypeMaster p on p.Purchasetypeid=b.saletypeid  where p.TaxCalculation='Bill Of Supply' and  p.isactive=1 and b.isactive=1 and bp.isactive=1  and b.totalnet>250000 and b.BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'and b.BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by b.companystate,b.statecode,b.billno,b.BillDate,b.totalnet,bp.cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleTypeid) entries");
             DataTable finalbill = conn.getdataset("select DISTINCT billno from (select c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,(bp.cgstamt+bp.sgstamt+bp.igdtamt+bp.addtaxper) as totaltax,b.totalcess,b.SaleType,sum(bp.Total-bp.DiscountAmt) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno inner join PurchasetypeMaster p on p.Purchasetypeid=b.SaleType   where p.TaxCalculation='Bill Of Supply' and  p.isactive=1 and c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and bp.BillType='S' and b.totalnet>250000 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.cgstamt,bp.sgstamt,bp.igdtamt,bp.addtaxper, c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType union select c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,b.totalcess,b.SaleType,sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as totalt,0 as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillchargesMaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1   where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and b.totalnet>250000 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,bp.plusminus,bp.addtaxamt, c.statecode,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleType,bp.tax union select b.companystate as State,b.statecode,b.billno,b.BillDate as Bill_Date,b.totalnet,(bp.igst/2) as cgstper,(bp.igst/2) as sgstper,bp.igst as igstper,b.totaltax,b.totalcess,b.SaleTypeid as SaleType,sum(bp.Amount-bp.DiscountAmt) as totalt,sum(bp.cess) as totalc from BillPOSMaster b  inner join BillPOSProductMaster bp on b.billno=bp.billno   where b.isactive=1 and bp.isactive=1  and b.totalnet>250000 and b.BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'and b.BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by b.companystate,b.statecode,b.billno,b.BillDate,b.totalnet,bp.cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleTypeid union select c.State,c.statecode,b.billno,b.date,b.totalfinalamount,bp.cgstper,bp.sgstper,bp.igstper,(bp.cgstamt+bp.sgstamt+bp.igstamt+bp.addtax) as totaltax,0 as totalcess,b.type,sum(bp.taxableamount) as totalt,0 as totalc from ClientMaster c inner join tblgstvouchermaster b on c.ClientID=b.party inner join tblgstvoucherproductmaster bp on b.billno=bp.billno  and (b.party=bp.party or bp.fkid=b.id) where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and bp.BillType='S' and b.totalfinalamount>250000 and b.date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.cgstamt,bp.sgstamt,bp.igstamt,bp.addtax, c.State,c.statecode,b.billno,b.Date,b.totalfinalamount,bp.cgstper,bp.sgstper,bp.igstper,b.totxltax,b.Type union select c.State,c.statecode,b.billno,b.date,b.totalfinalamount,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,0 as totalcess,b.Type,sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as totalt,0 as totalc from ClientMaster c inner join tblgstvouchermaster b on c.ClientID=b.party inner join tblgstvoucherchargesmaster bp on b.billno=bp.billno and (b.party=bp.party or bp.fkid=b.id) inner join BillSundry bs on bs.BillSundryID=bp.chargeid and bs.OT3=1 and bs.isactive=1   where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and b.totalfinalamount>250000 and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,bp.plusminus,bp.addtaxamt, c.statecode,b.billno,b.Date,b.totalfinalamount,cgst,bp.sgst,bp.igst,b.totxltax,b.Type,bp.tax ) entries");
             totalbillno = Convert.ToString(finalbill.Rows.Count); 
             main.Rows[2][0] = totalbillno;
             main.Rows[2][2] = totalnetvalue;
             main.Rows[2][5] = totaltax;
             main.Rows[2][6] = totalcess;

             dataGridView1.DataSource = main;
             DataSet ds2 = ods.getdata("select * from Path");
             path = ds2.Tables[0];
        }
        public void exportexcel()
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Worksheet xlsht = new Microsoft.Office.Interop.Excel.Worksheet();
            xlApp.Visible = true;
            string appPath = path.Rows[0]["DefaultPath"].ToString();
            string path1 = appPath + @"\DefaultGSTReports\GSTR1.xlsx";
            //xlsht = xlApp.Application.Workbooks.Open(path).Worksheets["b2b"];
            //  string path11 = @"C:\Users\admin\Desktop\Test.xlsx";
            xlsht = xlApp.Application.Workbooks.Open(path1).Worksheets["b2cl"];
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
                main.Columns.Add("Invoice Number", typeof(string));
                main.Columns.Add("Invoice Date", typeof(string));
                main.Columns.Add("Invoice Value", typeof(string));
                main.Columns.Add("Place Of Supply", typeof(string));
                main.Columns.Add("Rate", typeof(string));
                main.Columns.Add("Taxable Value", typeof(string));
                main.Columns.Add("Cess Amount", typeof(string));
                main.Columns.Add("E-Commerce GSTIN", typeof(string));

                //DataTable invoicedt = conn.getdataset("select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno   where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='" + "S" + "' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by b.Bill_Date");
             //   DataTable invoicedt = conn.getdataset("select c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.Tax) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno   where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and b.totalnet>250000 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'group by c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType union all select b.companystate as State,b.statecode,b.billno,b.BillDate as Bill_Date,b.totalnet,(bp.igst/2) as cgstper,(bp.igst/2) as sgstper,bp.igst as igstper,b.totaltax,b.totalcess,b.SaleTypeid as SaleType,sum(bp.sgst + bp.cgst) as totalt,sum(bp.cess) as totalc from BillPOSMaster b  inner join BillPOSProductMaster bp on b.billno=bp.billno   where b.isactive=1 and bp.isactive=1  and b.totalnet>250000 and b.BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'and b.BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'group by b.companystate,b.statecode,b.billno,b.BillDate,b.totalnet,bp.cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleTypeid");
               // DataTable invoicedt = conn.getdataset("select state,statecode, billno, bill_date,totalnet, cgstper, sgstper, igstper, totaltax,totalcess,saletype,sum(totalt) as totalt,sum(totalc) as totalc from (select c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.Amount-bp.DiscountAmt) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno   where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and bp.BillType='S' and b.totalnet>250000 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType union select c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.valueofexp) as totalt,0 as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillchargesMaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1   where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and b.totalnet>250000 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'group by c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleType,bp.tax union select b.companystate as State,b.statecode,b.billno,b.BillDate as Bill_Date,b.totalnet,(bp.igst/2) as cgstper,(bp.igst/2) as sgstper,bp.igst as igstper,b.totaltax,b.totalcess,b.SaleTypeid as SaleType,sum(bp.Amount-bp.DiscountAmt) as totalt,sum(bp.cess) as totalc from BillPOSMaster b  inner join BillPOSProductMaster bp on b.billno=bp.billno   where b.isactive=1 and bp.isactive=1  and b.totalnet>250000 and b.BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'and b.BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by b.companystate,b.statecode,b.billno,b.BillDate,b.totalnet,bp.cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleTypeid) entries group by state,statecode, billno, bill_date,totalnet, cgstper, sgstper, igstper, totaltax,totalcess,saletype order by billno");
             //   DataTable invoicedt = conn.getdataset("select state,statecode, billno, bill_date,totalnet, cgstper, sgstper, igstper, sum(totaltax) as totaltax,totalcess,saletype,sum(totalt) as totalt,sum(totalc) as totalc from (select c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,(bp.cgstamt+bp.sgstamt+bp.igdtamt+bp.addtaxper) as totaltax,b.totalcess,b.SaleType,sum(bp.Total-bp.DiscountAmt) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno   where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and bp.BillType='S' and b.totalnet>250000 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.cgstamt,bp.sgstamt,bp.igdtamt,bp.addtaxper, c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType union select c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,b.totalcess,b.SaleType,sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as totalt,0 as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillchargesMaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1   where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and b.totalnet>250000 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,bp.plusminus,bp.addtaxamt, c.statecode,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleType,bp.tax union select b.companystate as State,b.statecode,b.billno,b.BillDate as Bill_Date,b.totalnet,(bp.igst/2) as cgstper,(bp.igst/2) as sgstper,bp.igst as igstper,b.totaltax,b.totalcess,b.SaleTypeid as SaleType,sum(bp.Amount-bp.DiscountAmt) as totalt,sum(bp.cess) as totalc from BillPOSMaster b  inner join BillPOSProductMaster bp on b.billno=bp.billno   where b.isactive=1 and bp.isactive=1  and b.totalnet>250000 and b.BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'and b.BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by b.companystate,b.statecode,b.billno,b.BillDate,b.totalnet,bp.cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleTypeid) entries group by state,statecode, billno, bill_date,totalnet, cgstper, sgstper, igstper, totalcess,saletype order by billno");
                DataTable invoicedt = conn.getdataset("select state,statecode, billno, bill_date,totalnet, cgstper, sgstper, igstper, sum(totaltax) as totaltax,totalcess,saletype,sum(totalt) as totalt,sum(totalc) as totalc from (select c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,(bp.cgstamt+bp.sgstamt+bp.igdtamt+bp.addtaxper) as totaltax,b.totalcess,b.SaleType,sum(bp.Total-bp.DiscountAmt) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno   where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and bp.BillType='S' and b.totalnet>250000 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.cgstamt,bp.sgstamt,bp.igdtamt,bp.addtaxper, c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType union select c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,b.totalcess,b.SaleType,sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as totalt,0 as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillchargesMaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1   where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and b.totalnet>250000 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,bp.plusminus,bp.addtaxamt, c.statecode,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleType,bp.tax union select b.companystate as State,b.statecode,b.billno,b.BillDate as Bill_Date,b.totalnet,(bp.igst/2) as cgstper,(bp.igst/2) as sgstper,bp.igst as igstper,b.totaltax,b.totalcess,b.SaleTypeid as SaleType,sum(bp.Amount-bp.DiscountAmt) as totalt,sum(bp.cess) as totalc from BillPOSMaster b  inner join BillPOSProductMaster bp on b.billno=bp.billno   where b.isactive=1 and bp.isactive=1  and b.totalnet>250000 and b.BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'and b.BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by b.companystate,b.statecode,b.billno,b.BillDate,b.totalnet,bp.cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleTypeid union select c.State,c.statecode,b.billno,b.date,b.totalfinalamount,bp.cgstper,bp.sgstper,bp.igstper,(bp.cgstamt+bp.sgstamt+bp.igstamt+bp.addtax) as totaltax,0 as totalcess,b.type,sum(bp.taxableamount) as totalt,0 as totalc from ClientMaster c inner join tblgstvouchermaster b on c.ClientID=b.party inner join tblgstvoucherproductmaster bp on b.billno=bp.billno  and (b.party=bp.party or bp.fkid=b.id) where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and bp.BillType='S' and b.totalfinalamount>250000 and b.date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.cgstamt,bp.sgstamt,bp.igstamt,bp.addtax, c.State,c.statecode,b.billno,b.Date,b.totalfinalamount,bp.cgstper,bp.sgstper,bp.igstper,b.totxltax,b.Type union select c.State,c.statecode,b.billno,b.date,b.totalfinalamount,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,0 as totalcess,b.Type,sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as totalt,0 as totalc from ClientMaster c inner join tblgstvouchermaster b on c.ClientID=b.party inner join tblgstvoucherchargesmaster bp on b.billno=bp.billno and (b.party=bp.party or bp.fkid=b.id) inner join BillSundry bs on bs.BillSundryID=bp.chargeid and bs.OT3=1 and bs.isactive=1   where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and b.totalfinalamount>250000 and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,bp.plusminus,bp.addtaxamt, c.statecode,b.billno,b.Date,b.totalfinalamount,cgst,bp.sgst,bp.igst,b.totxltax,b.Type,bp.tax ) entries group by state,statecode, billno, bill_date,totalnet, cgstper, sgstper, igstper, totalcess,saletype order by billno");
                for (int i = 0; i < invoicedt.Rows.Count; i++)
                {
                    //DataTable ecomgst = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypeid='" + invoicedt.Rows[i]["SaleType"].ToString() + "' and TaxCalculation='" + "Tax Invoice" + "'");
                    DataTable ecomgst = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypeid='" + invoicedt.Rows[i]["SaleType"].ToString() +"'");
                    if (ecomgst.Rows.Count > 0)
                    {
                        if (ecomgst.Rows[0]["TaxCalculation"].ToString() == "Bill Of Supply" && ecomgst.Rows[0]["Region"].ToString() == "central")
                        {
                            DataRow dr = main.NewRow();
                            string d = Convert.ToDateTime(invoicedt.Rows[i]["Bill_Date"]).ToString(Master.dateformate);
                            dr["Invoice Number"] = invoicedt.Rows[i]["billno"].ToString();
                            dr["Invoice Date"] = d;
                            dr["Invoice Value"] = invoicedt.Rows[i]["totalnet"].ToString();
                            string place = invoicedt.Rows[i]["statecode"].ToString() + "-" + invoicedt.Rows[i]["State"].ToString();
                            dr["Place Of Supply"] = place;
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
                            dr["Cess Amount"] = invoicedt.Rows[i]["totalc"].ToString();
                            dr["E-Commerce GSTIN"] = ecomgst.Rows[0]["txtecom"].ToString();
                            main.Rows.Add(dr);
                        }
                    }
                }
                LVclient.Items.Clear();
                int ColCount = main.Columns.Count;
                //Add columns
                for (int k = 0; k < ColCount; k++)
                {
                    LVclient.Columns.Add(main.Columns[k].ColumnName, 120);
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

        private void BtnViewReport_MouseEnter(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = false;
            BtnViewReport.BackColor = Color.FromArgb(94, 191, 174);
            BtnViewReport.ForeColor = Color.White;
        }

        private void BtnViewReport_MouseLeave(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = true;
            BtnViewReport.BackColor = Color.FromArgb(51, 153, 255);
            BtnViewReport.ForeColor = Color.White;
        }

        private void btnexcel_MouseEnter(object sender, EventArgs e)
        {
            btnexcel.UseVisualStyleBackColor = false;
            btnexcel.BackColor = Color.FromArgb(206, 204, 254);
            btnexcel.ForeColor = Color.White;
        }

        private void btnexcel_MouseLeave(object sender, EventArgs e)
        {
            btnexcel.UseVisualStyleBackColor = true;
            btnexcel.BackColor = Color.FromArgb(51, 153, 255);
            btnexcel.ForeColor = Color.White;
        }

        private void btnclose_MouseEnter(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = false;
            btnclose.BackColor = Color.FromArgb(248, 152, 94);
            btnclose.ForeColor = Color.White;
        }

        private void btnclose_MouseLeave(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = true;
            btnclose.BackColor = Color.FromArgb(51, 153, 255);
            btnclose.ForeColor = Color.White;
        }
    }
}
