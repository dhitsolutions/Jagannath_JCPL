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
    public partial class ComplateGST_R1 : Form
    {
        private Master master;
        private TabControl tabControl;
        OleDbSettings ods = new OleDbSettings();
        DataTable main = new DataTable();
        DataTable main1 = new DataTable();
        DataTable path = new DataTable();
        DataSet ds1 = new DataSet();
        Double totalcessb2b = 0;
        Double totaltaxb2b = 0;
        Double totalnetvalueb2b = 0;
        string totalbillnob2b = "";
        string totalgstnob2b = "";
        Double totalcessb2cl = 0;
        Double totaltaxb2cl = 0;
        Double totalnetvalueb2cl = 0;
        string totalbillnob2cl = "";
        Double totalnetvalueb2cs = 0;
        Double totalcessb2cs = 0;
        Double TotalValuehsn = 0;
        Double TotalTaxableValuehsn = 0;
        Double TotalIntegratedTaxhsn = 0;
        Double TotalCentralTaxhsn = 0;
        Double TotalStateUTTaxhsn = 0;
        Double totalcesshsn = 0;
        string totalhsnhsn = "";
        Double totalcesscndr = 0;
        Double totaltaxcndr = 0;
        Double totalnetvaluecndr = 0;
        string totalbillnocndr = "";
        string totalgstnocndr = "";
        Double totalcesscndur = 0;
        Double totaltaxcndur = 0;
        Double totalnetvaluecndur = 0;
        string totalbillnocndur = "";
        string totalgstnocndur = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        public ComplateGST_R1()
        {
            InitializeComponent();
        }

        public ComplateGST_R1(Master master, TabControl tabControl)
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
        string documenttype, type;
        public void exportexcel()
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Worksheet xlsht = new Microsoft.Office.Interop.Excel.Worksheet();
            xlApp.Visible = true;
            string appPath = path.Rows[0]["DefaultPath"].ToString();
            string path1 = appPath + @"\DefaultGSTReports\GSTR1.xlsx";
            //xlsht = xlApp.Application.Workbooks.Open(path).Worksheets["b2b"];
            //  string path11 = @"C:\Users\admin\Desktop\Test.xlsx";
            xlsht = xlApp.Application.Workbooks.Open(path1).Worksheets["b2b"];
            for (int i = 0; i < dgb2b.ColumnCount; i++)
            {
                xlsht.Cells[1, i + 1] = dgb2b.Columns[i].HeaderText;
            }

            for (int i = 0; i < dgb2b.RowCount - 1; i++)
            {
                for (int j = 0; j < dgb2b.ColumnCount; j++)
                {
                    try
                    {
                        if (dgb2b[j, i].ValueType == typeof(string))
                        {
                            xlsht.Cells[i + 2, j + 1] = "" + dgb2b[j, i].Value.ToString();
                        }
                        else
                        {
                            xlsht.Cells[i + 2, j + 1] = dgb2b[j, i].Value.ToString();
                        }
                    }
                    catch
                    {
                    }
                }
            }
            xlsht.Rows[1].Delete();
            String DateTimeName = DateTime.Now.ToString("dd_MMM_yyyy hh_mm_ss");
            appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\GST Reports\";
            if (Directory.Exists(appPath) == false)
            {
                Directory.CreateDirectory(appPath);
            }
            string path2 = appPath+"COMPLATEGSTR-1-" + DateTimeName + ".xlsx";
            xlsht.SaveAs(path2);
            xlApp.Visible = false;
            xlApp.Visible = true;
            xlsht = xlApp.Application.Workbooks.Open(path2).Worksheets["b2cl"];
            for (int i = 0; i < dgb2cl.ColumnCount; i++)
            {
                xlsht.Cells[1, i + 1] = dgb2cl.Columns[i].HeaderText;
            }

            for (int i = 0; i < dgb2cl.RowCount - 1; i++)
            {
                for (int j = 0; j < dgb2cl.ColumnCount; j++)
                {
                    if (dgb2cl[j, i].ValueType == typeof(string))
                    {
                        xlsht.Cells[i + 2, j + 1] = "" + dgb2cl[j, i].Value.ToString();
                    }
                    else
                    {
                        xlsht.Cells[i + 2, j + 1] = dgb2cl[j, i].Value.ToString();
                    }
                }
            }
            xlsht.Rows[1].Delete();
            //if (File.Exists(path2))
            //{
            //    File.Delete(path2);
            //}
            xlApp.DisplayAlerts = false;
            xlsht.SaveAs(path2);
            xlApp.Visible = false;
            xlApp.Visible = true;

            xlsht = xlApp.Application.Workbooks.Open(path2).Worksheets["b2cs"];
            for (int i = 0; i < dgb2cs.ColumnCount; i++)
            {
                xlsht.Cells[1, i + 1] = dgb2cs.Columns[i].HeaderText;
            }

            for (int i = 0; i < dgb2cs.RowCount - 1; i++)
            {
                for (int j = 0; j < dgb2cs.ColumnCount; j++)
                {
                    if (dgb2cs[j, i].ValueType == typeof(string))
                    {
                        xlsht.Cells[i + 2, j + 1] = "" + dgb2cs[j, i].Value.ToString();
                    }
                    else
                    {
                        xlsht.Cells[i + 2, j + 1] = dgb2cs[j, i].Value.ToString();
                    }
                }
            }
            xlsht.Rows[1].Delete();
            xlApp.DisplayAlerts = false;
            xlsht.SaveAs(path2);
            xlApp.Visible = false;
            xlApp.Visible = true;
            xlsht = xlApp.Application.Workbooks.Open(path2).Worksheets["hsn"];
            for (int i = 0; i < dghsn.ColumnCount; i++)
            {
                xlsht.Cells[1, i + 1] = dghsn.Columns[i].HeaderText;
            }

            for (int i = 0; i < dghsn.RowCount - 1; i++)
            {
                for (int j = 0; j < dghsn.ColumnCount; j++)
                {
                    if (dghsn[j, i].ValueType == typeof(string))
                    {
                        xlsht.Cells[i + 2, j + 1] = "" + dghsn[j, i].Value.ToString();
                    }
                    else
                    {
                        xlsht.Cells[i + 2, j + 1] = dghsn[j, i].Value.ToString();
                    }
                }
            }
            xlsht.Rows[1].Delete();
            xlApp.DisplayAlerts = false;
            xlsht.SaveAs(path2);
            xlApp.Visible = false;
            xlApp.Visible = true;
            xlsht = xlApp.Application.Workbooks.Open(path2).Worksheets["cdnr"];
            for (int i = 0; i < dgvcndr.ColumnCount; i++)
            {
                xlsht.Cells[1, i + 1] = dgvcndr.Columns[i].HeaderText;
            }

            for (int i = 0; i < dgvcndr.RowCount - 1; i++)
            {
                for (int j = 0; j < dgvcndr.ColumnCount; j++)
                {
                    if (dgvcndr[j, i].ValueType == typeof(string))
                    {
                        xlsht.Cells[i + 2, j + 1] = "" + dgvcndr[j, i].Value.ToString();
                    }
                    else
                    {
                        xlsht.Cells[i + 2, j + 1] = dgvcndr[j, i].Value.ToString();
                    }
                }
            }
            xlsht.Rows[1].Delete();
            xlApp.DisplayAlerts = false;
            xlsht.SaveAs(path2);
            xlApp.Visible = false;
            xlApp.Visible = true;
            xlsht = xlApp.Application.Workbooks.Open(path2).Worksheets["cdnur"];
            for (int i = 0; i < dgvcdnur.ColumnCount; i++)
            {
                xlsht.Cells[1, i + 1] = dgvcdnur.Columns[i].HeaderText;
            }

            for (int i = 0; i < dgvcdnur.RowCount - 1; i++)
            {
                for (int j = 0; j < dgvcdnur.ColumnCount; j++)
                {
                    if (dgvcdnur[j, i].ValueType == typeof(string))
                    {
                        xlsht.Cells[i + 2, j + 1] = "" + dgvcdnur[j, i].Value.ToString();
                    }
                    else
                    {
                        xlsht.Cells[i + 2, j + 1] = dgvcdnur[j, i].Value.ToString();
                    }
                }
            }
            xlsht.Rows[1].Delete();

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
        public void importexcelb2b()
        {
            main = new DataTable();

            //     main.Rows.Add("Summary For B2B(4)", "", "", "", "", "", "", "","","","");
            main.Columns.Add("", typeof(string));
            main.Rows.Add("Summary For B2B(4)");
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
            main.Rows.Add("No. of Recipients", "No. of Invoices", "", "Total Invoice Value", "", "", "", "", "", "", "Total Taxable Value", "Total Cess");
            main.Rows.Add(totalgstnob2b, totalbillnob2b, "", totalnetvalueb2b, "", "", "", "", "", "",totaltaxb2b, totalcessb2b);
            main.Rows.Add("GSTIN/UIN Of Recipient", "Receiver Name", "Invoice Number", "Invoice Date", "Invoice Value", "Place Of Supply", "Reverse Charge", "Invoice Type", "E-Commerce GSTIN", "Rate", "Taxable Value", "Cess Amount");
            // DataTable invoicedt = conn.getdataset("select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.tax) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno   where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='" + "S" + "' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType");
          //  DataTable invoicedt = conn.getdataset("select GstNo,State,statecode,billno,Bill_Date,totalnet,cgstper,sgstper,igstper,totaltax,totalcess,SaleType,sum(totalt) as totalt,sum(totalc) as totalc from (select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.tax) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType union all select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.cgst+bp.sgst+bp.igst) as totalt,0 as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join billchargesmaster bp on b.billno=bp.billno  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleType,bp.tax) entries group by GstNo,State,statecode,billno,Bill_Date,totalnet,cgstper,sgstper,igstper,totaltax,totalcess,SaleType");
            //DataTable invoicedt = conn.getdataset("select GstNo,AccountName,State,statecode,billno,Bill_Date,totalnet,cgstper,sgstper,igstper,sum(totaltax),totalcess,SaleType,sum(totalt) as totalt,sum(totalc) as totalc from (select c.GstNo,c.AccountName,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.Total-bp.discountamt) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and bp.BillType='S' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.AccountName,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType union all select c.GstNo,c.AccountName,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,b.totalcess,b.SaleType,case when bp.plusminus='-' then sum(bp.valueofexp)*-1 when bp.plusminus='+' then sum(bp.valueofexp) end as totalt,0 as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1 where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.AccountName,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.plusminus, bp.igst,bp.addtaxamt, b.totaltax,b.totalcess,b.SaleType,bp.tax) entries group by GstNo,AccountName,State,statecode,billno,Bill_Date,totalnet,cgstper,sgstper,igstper,totalcess,SaleType order by billno");
            DataTable invoicedt = conn.getdataset("select GstNo,AccountName,State,statecode,billno,Bill_Date,totalnet,cgstper,sgstper,igstper,sum(totaltax),totalcess,SaleType,sum(totalt) as totalt,sum(totalc) as totalc from (select c.GstNo,c.AccountName,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.Total-bp.discountamt) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and bp.BillType='S' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'group by c.GstNo,c.AccountName,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType union all select c.GstNo,c.AccountName,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,b.totalcess,b.SaleType,case when bp.plusminus='-' then sum(bp.valueofexp)*-1 when bp.plusminus='+' then sum(bp.valueofexp) end as totalt,0 as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1 where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.AccountName,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.plusminus, bp.igst,bp.addtaxamt, b.totaltax,b.totalcess,b.SaleType,bp.tax union all select c.GstNo,c.AccountName,c.State,c.statecode,b.billno,b.date as Bill_Date,b.totalfinalamount as totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totxltax as totaltax,0 as totalcess,b.type as SaleType,sum(bp.taxableamount) as totalt,sum(0) as totalc from ClientMaster c inner join tblgstvouchermaster b on c.ClientID=b.party inner join tblgstvoucherproductmaster bp on b.billno=bp.billno  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and bp.BillType='S' and b.date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'group by c.GstNo,c.AccountName,c.State,c.statecode,b.billno,b.date,b.totalfinalamount,bp.cgstper,bp.sgstper,bp.igstper,b.totxltax,b.type union all select c.GstNo,c.AccountName,c.State,c.statecode,b.billno,b.date as Bill_Date, b.totalfinalamount as totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,0 as totalcess,b.type,case when bp.plusminus='-' then sum(bp.valueofexp)*-1 when bp.plusminus='+' then sum(bp.valueofexp) end as totalt,0 as totalc from ClientMaster c inner join tblgstvouchermaster b on c.ClientID=b.party inner join tblgstvoucherchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.chargeid and bs.OT3=1 and bs.isactive=1 where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and b.date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.AccountName,c.State,c.statecode,b.billno,b.date ,b.totalfinalamount,cgst,bp.sgst,bp.plusminus, bp.igst,bp.addtaxamt, b.totxltax,b.type,bp.tax)entries group by GstNo,AccountName,State,statecode,billno,Bill_Date,totalnet,cgstper,sgstper,igstper,totalcess,SaleType order by billno");
            for (int i = 0; i < invoicedt.Rows.Count; i++)
            {
                //  DataTable ecomgst = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypeid='" + invoicedt.Rows[i]["SaleType"].ToString() + "' and TaxCalculation='" + "Tax Invoice" + "'");
                DataTable ecomgst = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypeid='" + invoicedt.Rows[i]["SaleType"].ToString() + "'");
                if (ecomgst.Rows.Count > 0)
                {
                    if (ecomgst.Rows[0]["TaxCalculation"].ToString() == "Tax Invoice")
                    {
                        //DataRow dr = main.NewRow();
                        //DataTable uniquegstno = conn.getdataset("select * from clientmaster where GstNo != '' or GstNo !=null and isactive=1");
                        //for (int u = 0; u < uniquegstno.Rows.Count; u++)
                        //{
                        //    if (uniquegstno.Rows[0]["GstNo"].ToString() == invoicedt.Rows[i]["GstNo"].ToString())
                        //    {
                        //        totalgstno++;
                        //    }
                        //}
                        string d = Convert.ToDateTime(invoicedt.Rows[i]["Bill_Date"]).ToString(Master.dateformate);
                        string place = invoicedt.Rows[i]["statecode"].ToString() + "-" + invoicedt.Rows[i]["State"].ToString();
                        double rate = Convert.ToDouble(invoicedt.Rows[i]["cgstper"].ToString()) + Convert.ToDouble(invoicedt.Rows[i]["sgstper"].ToString());
                        double igst = Convert.ToDouble(invoicedt.Rows[i]["igstper"].ToString());

                        if (ecomgst.Rows[0]["Region"].ToString() == "Local")
                        {
                            main.Rows.Add(invoicedt.Rows[i]["GstNo"].ToString(),invoicedt.Rows[i]["AccountName"].ToString(), invoicedt.Rows[i]["billno"].ToString(), d, invoicedt.Rows[i]["totalnet"].ToString(), place, "N", "Regular", ecomgst.Rows[0]["txtecom"].ToString(), rate, invoicedt.Rows[i]["totalt"].ToString(), invoicedt.Rows[i]["totalc"].ToString());
                            //totalbillno++;
                        }
                        else
                        {
                            main.Rows.Add(invoicedt.Rows[i]["GstNo"].ToString(),invoicedt.Rows[i]["AccountName"].ToString(), invoicedt.Rows[i]["billno"].ToString(), d, invoicedt.Rows[i]["totalnet"].ToString(), place, "N", "Regular", ecomgst.Rows[0]["txtecom"].ToString(), igst, invoicedt.Rows[i]["totalt"].ToString(), invoicedt.Rows[i]["totalc"].ToString());
                            // totalbillno++;
                        }

                        totalnetvalueb2b += Convert.ToDouble(invoicedt.Rows[i]["totalnet"].ToString());
                        totaltaxb2b += Convert.ToDouble(invoicedt.Rows[i]["totalt"].ToString());
                        totalcessb2b += Convert.ToDouble(invoicedt.Rows[i]["totalc"].ToString());
                    }
                }
            }
            DataTable finalbill = conn.getdataset("select DISTINCT GstNo from(select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.tax) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno  inner join PurchasetypeMaster p on p.Purchasetypeid=b.SaleType where p.TaxCalculation='Tax Invoice' and c.isactive=1 and p.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType union all select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.cgst+bp.sgst+bp.igst) as totalt,0 as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join billchargesmaster bp on b.billno=bp.billno  inner join PurchasetypeMaster p on p.Purchasetypeid=b.SaleType where p.TaxCalculation='Tax Invoice' and c.isactive=1 and p.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleType,bp.tax) entries ");
            DataTable final = conn.getdataset("select DISTINCT billno from(select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.tax) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno  inner join PurchasetypeMaster p on p.Purchasetypeid=b.SaleType where p.TaxCalculation='Tax Invoice' and c.isactive=1 and p.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType union all select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.cgst+bp.sgst+bp.igst) as totalt,0 as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join billchargesmaster bp on b.billno=bp.billno  inner join PurchasetypeMaster p on p.Purchasetypeid=b.SaleType where p.TaxCalculation='Tax Invoice' and c.isactive=1 and p.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleType,bp.tax) entries ");
            totalbillnob2b = Convert.ToString(finalbill.Rows.Count);
            totalgstnob2b = Convert.ToString(final.Rows.Count);
            main.Rows[2][0] = totalgstnob2b;
            main.Rows[2][1] = totalbillnob2b;
            main.Rows[2][3] = totalnetvalueb2b;
            main.Rows[2][10] = totaltaxb2b;
            main.Rows[2][11] = totalcessb2b;

            dgb2b.DataSource = main;
            //   griddesign();
            DataSet ds2 = ods.getdata("select * from Path");
            path = ds2.Tables[0];

        }
        public void importexcelb2cl()
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
            main.Rows.Add("No. of Invoices", "", "Total Inv Value", "", "", "Total Taxable Value", "Total Cess", "");
            main.Rows.Add(totalbillnob2cl, "", totalnetvalueb2cl, "", "", totaltaxb2cl, totalcessb2cl);
            main.Rows.Add("Invoice Number", "Invoice date", "Invoice Value", "Place Of Supply", "Rate", "Taxable Value", "Cess Amount", "E-Commerce GSTIN");
          //  DataTable invoicedt = conn.getdataset("select state,statecode, billno, bill_date,totalnet, cgstper, sgstper, igstper, totaltax,totalcess,saletype,sum(totalt) as totalt,sum(totalc) as totalc from (select c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.Tax) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno   where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and b.totalnet>250000 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType union select c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.cgst+bp.sgst+bp.igst) as totalt,0 as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillchargesMaster bp on b.billno=bp.billno   where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and b.totalnet>250000 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'group by c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleType,bp.tax union select b.companystate as State,b.statecode,b.billno,b.BillDate as Bill_Date,b.totalnet,(bp.igst/2) as cgstper,(bp.igst/2) as sgstper,bp.igst as igstper,b.totaltax,b.totalcess,b.SaleTypeid as SaleType,sum(bp.sgst + bp.cgst) as totalt,sum(bp.cess) as totalc from BillPOSMaster b  inner join BillPOSProductMaster bp on b.billno=bp.billno   where b.isactive=1 and bp.isactive=1  and b.totalnet>250000 and b.BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'and b.BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by b.companystate,b.statecode,b.billno,b.BillDate,b.totalnet,bp.cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleTypeid) entries group by state,statecode, billno, bill_date,totalnet, cgstper, sgstper, igstper, totaltax,totalcess,saletype");
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
                    totalnetvalueb2cl += Convert.ToDouble(invoicedt.Rows[i]["totalnet"].ToString());
                    totaltaxb2cl += Convert.ToDouble(invoicedt.Rows[i]["totalt"].ToString());
                    totalcessb2cl += Convert.ToDouble(invoicedt.Rows[i]["totalc"].ToString());
                }
            }
            DataTable finalbill = conn.getdataset("select DISTINCT billno from (select c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,(bp.cgstamt+bp.sgstamt+bp.igdtamt+bp.addtaxper) as totaltax,b.totalcess,b.SaleType,sum(bp.Total-bp.DiscountAmt) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno inner join PurchasetypeMaster p on p.Purchasetypeid=b.SaleType   where p.TaxCalculation='Bill Of Supply' and  p.isactive=1 and c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and bp.BillType='S' and b.totalnet>250000 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.cgstamt,bp.sgstamt,bp.igdtamt,bp.addtaxper, c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType union select c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,b.totalcess,b.SaleType,sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as totalt,0 as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillchargesMaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1   where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and b.totalnet>250000 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,bp.plusminus,bp.addtaxamt, c.statecode,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleType,bp.tax union select b.companystate as State,b.statecode,b.billno,b.BillDate as Bill_Date,b.totalnet,(bp.igst/2) as cgstper,(bp.igst/2) as sgstper,bp.igst as igstper,b.totaltax,b.totalcess,b.SaleTypeid as SaleType,sum(bp.Amount-bp.DiscountAmt) as totalt,sum(bp.cess) as totalc from BillPOSMaster b  inner join BillPOSProductMaster bp on b.billno=bp.billno   where b.isactive=1 and bp.isactive=1  and b.totalnet>250000 and b.BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'and b.BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by b.companystate,b.statecode,b.billno,b.BillDate,b.totalnet,bp.cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleTypeid union select c.State,c.statecode,b.billno,b.date,b.totalfinalamount,bp.cgstper,bp.sgstper,bp.igstper,(bp.cgstamt+bp.sgstamt+bp.igstamt+bp.addtax) as totaltax,0 as totalcess,b.type,sum(bp.taxableamount) as totalt,0 as totalc from ClientMaster c inner join tblgstvouchermaster b on c.ClientID=b.party inner join tblgstvoucherproductmaster bp on b.billno=bp.billno  and (b.party=bp.party or bp.fkid=b.id) where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and bp.BillType='S' and b.totalfinalamount>250000 and b.date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.cgstamt,bp.sgstamt,bp.igstamt,bp.addtax, c.State,c.statecode,b.billno,b.Date,b.totalfinalamount,bp.cgstper,bp.sgstper,bp.igstper,b.totxltax,b.Type union select c.State,c.statecode,b.billno,b.date,b.totalfinalamount,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,0 as totalcess,b.Type,sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as totalt,0 as totalc from ClientMaster c inner join tblgstvouchermaster b on c.ClientID=b.party inner join tblgstvoucherchargesmaster bp on b.billno=bp.billno and (b.party=bp.party or bp.fkid=b.id) inner join BillSundry bs on bs.BillSundryID=bp.chargeid and bs.OT3=1 and bs.isactive=1   where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='S' and b.totalfinalamount>250000 and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,bp.plusminus,bp.addtaxamt, c.statecode,b.billno,b.Date,b.totalfinalamount,cgst,bp.sgst,bp.igst,b.totxltax,b.Type,bp.tax ) entries");
            totalbillnob2cl = Convert.ToString(finalbill.Rows.Count);
            main.Rows[2][0] = totalbillnob2cl;
            main.Rows[2][2] = totalnetvalueb2cl;
            main.Rows[2][5] = totaltaxb2cl;
            main.Rows[2][6] = totalcessb2cl;

            dgb2cl.DataSource = main;
            DataSet ds2 = ods.getdata("select * from Path");
            path = ds2.Tables[0];
        }
        public void importexcelb2cs()
        {
            main = new DataTable();

            //     main.Rows.Add("Summary For B2B(4)", "", "", "", "", "", "", "","","","");
            main.Columns.Add("", typeof(string));
            main.Rows.Add("Summary For B2CS(7)");
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Rows.Add("", "", "", "Total Taxable Value", "Total Cess", "");
            main.Rows.Add("", "", "", totalnetvalueb2cs, totalcessb2cs, "");
            main.Rows.Add("Type", "Place Of Supply", "Rate", "Taxable Value", "Cess Amount", "E-Commerce GSTIN");
          //  string s = "select type,place, rate, sum(taxamt) as taxamt,cess,txtecom from (select case p.chkecom when '0' then 'OE' when '1' then 'E' end as type,(c.statecode+'-'+upper(c.State)) as place, (b.cgstper+b.sgstper+b.igstper) as rate,sum(b.tax) as taxamt,sum(b.cess) as cess,p.txtecom from billproductmaster b inner join billmaster bm on bm.billno=b.billno inner join purchasetypemaster p on p.purchasetypeid=bm.saletype and p.Region='Local' and p.type='S' and p.TaxCalculation='Bill Of Supply' inner join clientmaster c on c.clientid=bm.clientid where bm.totalnet<250000 and bm.isactive=1 and b.isactive=1 and bm.bill_date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bm.bill_date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and b.bill_Run_date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.bill_Run_date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by b.cgstper,b.sgstper,b.igstper,p.chkecom,c.statecode,c.State,p.txtecom union select case p.chkecom when '0' then 'OE' when '1' then 'E' end as type,(bm.statecode+'-'+upper(bm.companystate)) as place, igst as rate, sum(sgst+cgst)as taxamt,sum(cess) as cess,p.txtecom from billposproductmaster bp inner join billposmaster bm on bm.billid=bp.billid inner join purchasetypemaster p on p.purchasetypeid=bm.saletypeid and p.Region='Local' and p.type='S' and p.TaxCalculation='Bill Of Supply' where bm.totalnet<250000 and  bm.isactive=1 and bp.isactive=1 and bm.billdate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bm.billdate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and bp.billRundate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bp.billRundate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.igst,p.chkecom,bm.statecode,bm.companystate,p.txtecom union select case p.chkecom when '0' then 'OE' when '1' then 'E' end as type,(c.statecode+'-'+upper(c.State)) as place, b.tax as rate,sum(b.sgst+b.cgst+b.igst) as taxamt,0 as cess,p.txtecom from billchargesmaster b inner join billmaster bm on bm.billno=b.billno inner join purchasetypemaster p on p.purchasetypeid=bm.saletype and p.Region='Local' and p.type='S' and p.TaxCalculation='Bill Of Supply' inner join clientmaster c on c.clientid=bm.clientid where bm.totalnet<250000 and bm.isactive=1 and b.isactive=1 and bm.bill_date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bm.bill_date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'group by b.cgst,b.sgst,b.igst,p.chkecom,c.statecode,c.State,p.txtecom,b.tax) entries group by type,place, rate, cess, txtecom";
            string s = "select type,place, rate, sum(taxamt) as taxamt,cess,txtecom from (select case p.chkecom when '0' then 'OE' when '1' then 'E' end as type,(c.statecode+'-'+upper(c.State)) as place, (b.cgstper+b.sgstper+b.igstper) as rate,sum(b.Total-b.discountamt) as taxamt,sum(b.cess) as cess,p.txtecom from billproductmaster b inner join billmaster bm on bm.billno=b.billno inner join purchasetypemaster p on p.purchasetypeid=bm.saletype and p.Region='Local' and p.type='S' and p.TaxCalculation='Bill Of Supply' inner join clientmaster c on c.clientid=bm.clientid where bm.isactive=1 and b.isactive=1 and bm.bill_date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bm.bill_date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and b.bill_Run_date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.bill_Run_date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by b.cgstper,b.sgstper,b.igstper,p.chkecom,c.statecode,c.State,p.txtecom union select case p.chkecom when '0' then 'OE' when '1' then 'E' end as type,(bm.statecode+'-'+upper(bm.companystate)) as place, igst as rate, sum(bp.Amount-bp.DiscountAmt)as taxamt,sum(cess) as cess,p.txtecom from billposproductmaster bp inner join billposmaster bm on bm.billid=bp.billid inner join purchasetypemaster p on p.purchasetypeid=bm.saletypeid and p.Region='Local' and p.type='S' and p.TaxCalculation='Bill Of Supply' where bm.isactive=1 and bp.isactive=1 and bm.billdate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bm.billdate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and bp.billRundate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bp.billRundate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.igst,p.chkecom,bm.statecode,bm.companystate,p.txtecom union select case p.chkecom when '0' then 'OE' when '1' then 'E' end as type,(c.statecode+'-'+upper(c.State)) as place, b.tax as rate,sum(case when b.plusminus='-' then (b.valueofexp)*-1 when b.plusminus='+' then (b.valueofexp) end) as taxamt,0 as cess,p.txtecom from billchargesmaster b inner join billmaster bm on bm.billno=b.billno inner join purchasetypemaster p on p.purchasetypeid=bm.saletype and p.Region='Local' and p.type='S' and p.TaxCalculation='Bill Of Supply' inner join clientmaster c on c.clientid=bm.clientid inner join BillSundry bs on bs.BillSundryID=b.billsundryid and bs.OT3=1 and bs.isactive=1 where bm.isactive=1 and b.isactive=1 and bm.bill_date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bm.bill_date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'group by b.cgst,b.sgst,b.igst,p.chkecom,c.statecode,c.State,p.txtecom,b.tax union select case p.chkecom when '0' then 'OE' when '1' then 'E' end as type,(c.statecode+'-'+upper(c.State)) as place, (b.cgstper+b.sgstper+b.igstper) as rate,sum(b.taxableamount) as taxamt,0 as cess ,p.txtecom from tblgstvoucherproductmaster b inner join tblgstvouchermaster bm on bm.billno=b.billno and (b.party=bm.party or b.fkid=bm.id) inner join purchasetypemaster p on p.purchasetypeid=bm.type and p.Region='Local' and p.FormType='S' and p.TaxCalculation='Bill Of Supply' inner join clientmaster c on c.clientid=bm.party where bm.isactive=1 and b.isactive=1 and bm.date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bm.date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by b.cgstper,b.sgstper,b.igstper,p.chkecom,c.statecode,c.State,p.txtecom union select case p.chkecom when '0' then 'OE' when '1' then 'E' end as type,(c.statecode+'-'+upper(c.State)) as place, b.tax as rate,sum(case when b.plusminus='-' then (b.valueofexp)*-1 when b.plusminus='+' then (b.valueofexp) end) as taxamt,0 as cess,p.txtecom from tblgstvoucherchargesmaster b inner join tblgstvouchermaster bm on bm.billno=b.billno and (b.party=bm.party or b.fkid=bm.id) inner join purchasetypemaster p on p.purchasetypeid=bm.type and p.Region='Local' and p.type='S' and p.TaxCalculation='Bill Of Supply' inner join clientmaster c on c.clientid=bm.party inner join BillSundry bs on bs.BillSundryID=b.chargeid and bs.OT3=1 and bs.isactive=1 where bm.isactive=1 and b.isactive=1 and bm.date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bm.date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'group by b.cgst,b.sgst,b.igst,p.chkecom,c.statecode,c.State,p.txtecom,b.tax) entries group by type,place, rate, cess, txtecom";
            DataTable btocs = conn.getdataset(s);
            for (int i = 0; i < btocs.Rows.Count; i++)
            {
                main.Rows.Add(btocs.Rows[i]["type"].ToString(), btocs.Rows[i]["place"].ToString(), btocs.Rows[i]["rate"].ToString(), btocs.Rows[i]["taxamt"].ToString(), btocs.Rows[i]["cess"].ToString(), btocs.Rows[i]["txtecom"].ToString());

                totalnetvalueb2cs += Convert.ToDouble(btocs.Rows[i]["taxamt"].ToString());
                totalcessb2cs += Convert.ToDouble(btocs.Rows[i]["cess"].ToString());
            }
            main.Rows[2][3] = totalnetvalueb2cs;
            main.Rows[2][4] = totalcessb2cs;

            dgb2cs.DataSource = main;
            DataSet ds2 = ods.getdata("select * from Path");
            path = ds2.Tables[0];
        }
        public void importexcelhsn()
        {
            main = new DataTable();

            //     main.Rows.Add("Summary For B2B(4)", "", "", "", "", "", "", "","","","");
            main.Columns.Add("", typeof(string));
            main.Rows.Add("Summary For HSN(12)");
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Rows.Add("No. of HSN", "", "", "", "Total Value", "Total Taxable Value", "Total Integrated Tax", "Total Central Tax", "Total State/UT Tax", "Total Cess");
            main.Rows.Add(totalhsnhsn, "", "", "", TotalValuehsn, TotalTaxableValuehsn, TotalIntegratedTaxhsn, TotalCentralTaxhsn, TotalStateUTTaxhsn, totalcesshsn);
            main.Rows.Add("HSN", "Description", "UQC", "Total Quantity", "Total Value", "Taxable Value", "Integrated Tax Amount", "Central Tax Amount", "State/UT Tax Amount", "Cess Amount");
           // DataTable invoicedt = conn.getdataset("select Hsn_Sac_Code,GroupName,UQC, sum(totalqty) as totalqty,sum(totalnet) as totalnet,sum(totaltax) as totaltax,sum(igst) as igst,sum(sgst) as sgst,sum(cgst) as cgst,sum(totalcess) as totalcess from (select i.Hsn_Sac_Code,i.GroupName,u.UQC, sum(bp.qty) as totalqty,sum(b.totalnet) as totalnet,sum(bp.tax) as totaltax,sum(bp.sgstamt + bp.cgstamt) as igst,sum(bp.sgstamt) as sgst,sum(bp.cgstamt) as cgst,sum(bp.cess) as totalcess from BillProductMaster bp inner join BillMaster b on bp.billno=b.billno inner join ProductMaster i on i.Product_Name=bp.Productname inner join unitmaster u on u.UnitName=i.unit where b.isactive=1 and bp.isactive=1 and i.isactive=1 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by i.Hsn_Sac_Code,i.GroupName,u.uqc union  select i.OT2,'' as GroupName,'UNT-UNITS' as UQC, 0 as totalqty,sum(bp.amount) as totalnet,sum(bp.valueofexp) as totaltax,sum(bp.igst)as igst,sum(bp.sgst) as sgst,sum(bp.cgst) as cgst,sum(0) as totalcess from billchargesmaster bp inner join BillMaster b on bp.billno=b.billno inner join billsundry i on i.billsundryname=bp.perticulars where b.isactive=1 and bp.isactive=1 and i.isactive=1 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by i.OT2 union select i.Hsn_Sac_Code,i.GroupName,u.UQC,sum(bp.qty) as totalqty,sum(b.totalnet)as totalnet,sum(bp.sgst+bp.cgst+bp.igst) as totaltax,sum(bp.sgst + bp.cgst) as igst,sum(bp.sgst) as sgst,sum(bp.cgst) as cgst,sum(bp.cess) as totalcess from BillPOSProductMaster bp inner join BillPOSMaster b on bp.billno=b.billno inner join ProductMaster i on i.Product_Name=bp.ItemName inner join unitmaster u on u.UnitName=i.unit where b.isactive=1 and bp.isactive=1and i.isactive=1 and b.BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by i.Hsn_Sac_Code,i.GroupName,u.uqc) entries group by Hsn_Sac_Code , GroupName, uqc");
            DataTable invoicedt = conn.getdataset("select Hsn_Sac_Code,GroupName,UQC, sum(totalqty) as totalqty,sum(totalnet) as totalnet,sum(totaltax) as totaltax,sum(igst) as igst,sum(sgst) as sgst,sum(cgst) as cgst,sum(totalcess) as totalcess from (select i.Hsn_Sac_Code,i.GroupName,u.UQC, sum(bp.qty) as totalqty,sum(bp.Amount) as totalnet,sum(bp.Total-bp.discountamt) as totaltax,sum(bp.igdtamt) as igst,sum(bp.sgstamt) as sgst,sum(bp.cgstamt) as cgst,sum(bp.cess) as totalcess from BillProductMaster bp inner join BillMaster b on bp.billno=b.billno inner join ProductMaster i on i.ProductID=bp.productid inner join unitmaster u on u.UnitName=i.unit where b.isactive=1 and bp.isactive=1 and i.isactive=1 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by i.Hsn_Sac_Code,i.GroupName,u.uqc union  select i.OT2,'' as GroupName,'UNT-UNITS' as UQC, 0 as totalqty,sum(bp.amount) as totalnet,sum(bp.valueofexp) as totaltax,sum(bp.igst)as igst,sum(bp.sgst) as sgst,sum(bp.cgst) as cgst,sum(0) as totalcess from billchargesmaster bp inner join BillMaster b on bp.billno=b.billno inner join billsundry i on i.billsundryname=bp.perticulars and i.OT3=1 and i.isactive=1 where b.isactive=1 and bp.isactive=1 and i.isactive=1 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by i.OT2 union select i.Hsn_Sac_Code,i.GroupName,u.UQC,sum(bp.qty) as totalqty,sum(bp.Total)as totalnet,sum(bp.Amount-bp.discountamt) as totaltax,0 as igst,sum(bp.sgst) as sgst,sum(bp.cgst) as cgst,sum(bp.cess) as totalcess from BillPOSProductMaster bp inner join BillPOSMaster b on bp.billno=b.billno inner join ProductMaster i on i.Product_Name=bp.ItemName inner join unitmaster u on u.UnitName=i.unit where b.isactive=1 and bp.isactive=1and i.isactive=1 and b.BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by i.Hsn_Sac_Code,i.GroupName,u.uqc union select bp.hsn,'' as GroupName,'UNT-UNITS' as UQC, 0 as totalqty,sum(bp.netamt) as totalnet,sum(bp.taxableamount) as totaltax,sum(bp.igstamt) as igst,sum(bp.sgstamt) as sgst,sum(bp.cgstamt) as cgst,0 as totalcess from tblgstvoucherproductmaster bp inner join tblgstvouchermaster b on bp.billno=b.billno and (b.party=bp.party or bp.fkid=b.id)   where b.isactive=1 and bp.isactive=1 and  b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.hsn union select i.OT2,'' as GroupName,'UNT-UNITS' as UQC, 0 as totalqty,sum(bp.amount) as totalnet,sum(bp.valueofexp) as totaltax,sum(bp.igst)as igst,sum(bp.sgst) as sgst,sum(bp.cgst) as cgst,sum(0) as totalcess from tblgstvoucherchargesmaster bp inner join tblgstvouchermaster b on bp.billno=b.billno and (b.party=bp.party or bp.fkid=b.id)   inner join billsundry i on i.billsundryname=bp.perticulars and i.OT3=1 and i.isactive=1 where b.isactive=1 and bp.isactive=1 and i.isactive=1 and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by i.OT2 ) entries group by Hsn_Sac_Code , GroupName, uqc");
            for (int i = 0; i < invoicedt.Rows.Count; i++)
            {
                main.Rows.Add(invoicedt.Rows[i]["Hsn_Sac_Code"].ToString(), invoicedt.Rows[i]["GroupName"].ToString(), invoicedt.Rows[i]["UQC"].ToString(), invoicedt.Rows[i]["totalqty"].ToString(), invoicedt.Rows[i]["totalnet"].ToString(), invoicedt.Rows[i]["totaltax"].ToString(), invoicedt.Rows[i]["igst"].ToString(), invoicedt.Rows[i]["cgst"].ToString(), invoicedt.Rows[i]["sgst"].ToString(), invoicedt.Rows[i]["totalcess"].ToString());
                TotalValuehsn += Convert.ToDouble(invoicedt.Rows[i]["totalnet"].ToString());
                TotalTaxableValuehsn += Convert.ToDouble(invoicedt.Rows[i]["totaltax"].ToString());
                TotalIntegratedTaxhsn += Convert.ToDouble(invoicedt.Rows[i]["igst"].ToString());
                TotalCentralTaxhsn += Convert.ToDouble(invoicedt.Rows[i]["cgst"].ToString());
                TotalStateUTTaxhsn += Convert.ToDouble(invoicedt.Rows[i]["sgst"].ToString());
                totalcesshsn += Convert.ToDouble(invoicedt.Rows[i]["totalcess"].ToString());
            }
            DataTable final = conn.getdataset("select DISTINCT Hsn_Sac_Code from (select i.Hsn_Sac_Code,i.GroupName,u.UQC, sum(bp.qty) as totalqty,sum(bp.Amount) as totalnet,sum(bp.Total-bp.discountamt) as totaltax,sum(bp.igdtamt) as igst,sum(bp.sgstamt) as sgst,sum(bp.cgstamt) as cgst,sum(bp.cess) as totalcess from BillProductMaster bp inner join BillMaster b on bp.billno=b.billno inner join ProductMaster i on i.ProductID=bp.productid inner join unitmaster u on u.UnitName=i.unit where b.isactive=1 and bp.isactive=1 and i.isactive=1 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by i.Hsn_Sac_Code,i.GroupName,u.uqc union  select i.OT2,'' as GroupName,'UNT-UNITS' as UQC, 0 as totalqty,sum(bp.amount) as totalnet,sum(bp.valueofexp) as totaltax,sum(bp.igst)as igst,sum(bp.sgst) as sgst,sum(bp.cgst) as cgst,sum(0) as totalcess from billchargesmaster bp inner join BillMaster b on bp.billno=b.billno inner join billsundry i on i.billsundryname=bp.perticulars and i.OT3=1 and i.isactive=1 where b.isactive=1 and bp.isactive=1 and i.isactive=1 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by i.OT2 union select i.Hsn_Sac_Code,i.GroupName,u.UQC,sum(bp.qty) as totalqty,sum(bp.Total)as totalnet,sum(bp.Amount-bp.discountamt) as totaltax,0 as igst,sum(bp.sgst) as sgst,sum(bp.cgst) as cgst,sum(bp.cess) as totalcess from BillPOSProductMaster bp inner join BillPOSMaster b on bp.billno=b.billno inner join ProductMaster i on i.Product_Name=bp.ItemName inner join unitmaster u on u.UnitName=i.unit where b.isactive=1 and bp.isactive=1and i.isactive=1 and b.BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by i.Hsn_Sac_Code,i.GroupName,u.uqc union select bp.hsn,'' as GroupName,'UNT-UNITS' as UQC, 0 as totalqty,sum(bp.netamt) as totalnet,sum(bp.taxableamount) as totaltax,sum(bp.igstamt) as igst,sum(bp.sgstamt) as sgst,sum(bp.cgstamt) as cgst,0 as totalcess from tblgstvoucherproductmaster bp inner join tblgstvouchermaster b on bp.billno=b.billno and (b.party=bp.party or bp.fkid=b.id)   where b.isactive=1 and bp.isactive=1 and  b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.hsn union select i.OT2,'' as GroupName,'UNT-UNITS' as UQC, 0 as totalqty,sum(bp.amount) as totalnet,sum(bp.valueofexp) as totaltax,sum(bp.igst)as igst,sum(bp.sgst) as sgst,sum(bp.cgst) as cgst,sum(0) as totalcess from tblgstvoucherchargesmaster bp inner join tblgstvouchermaster b on bp.billno=b.billno and (b.party=bp.party or bp.fkid=b.id)   inner join billsundry i on i.billsundryname=bp.perticulars and i.OT3=1 and i.isactive=1 where b.isactive=1 and bp.isactive=1 and i.isactive=1 and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by i.OT2 ) entries group by Hsn_Sac_Code");
            totalhsnhsn = Convert.ToString(final.Rows.Count);
            main.Rows[2][0] = totalhsnhsn;
            main.Rows[2][4] = TotalValuehsn;
            main.Rows[2][5] = TotalTaxableValuehsn;
            main.Rows[2][6] = TotalIntegratedTaxhsn;
            main.Rows[2][7] = TotalCentralTaxhsn;
            main.Rows[2][8] = TotalStateUTTaxhsn;
            main.Rows[2][9] = totalcesshsn;

            dghsn.DataSource = main;
            DataSet ds2 = ods.getdata("select * from Path");
            path = ds2.Tables[0];
        }
        public void importexcelcndr()
        {
            main = new DataTable();

            //     main.Rows.Add("Summary For B2B(4)", "", "", "", "", "", "", "","","","");
            main.Columns.Add("", typeof(string));
            main.Rows.Add("Summary For CDNR(9B)");
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
            main.Rows.Add("No. of Recipients", "No. of Invoices", "", "No. of Notes/Vouchers", "", "", "", "", "Total Note/Refund Voucher Value", "", "Total Taxable Value", "Total Cess", "");
            main.Rows.Add(totalgstnocndr, totalbillnocndr, "", totalnetvaluecndr, "", "", "", "", "", "", totaltaxcndr, totalcesscndr, "");
            main.Rows.Add("GSTIN/UIN Of Recipient", "Invoice/Advance Receipt Number", "Invoice/Advance Receipt Date", "Note/Refund Voucher Number", "Note/Refund Voucher Date", "Document Type", "Place Of Supply", "Note/Refund Voucher Value", "Applicable % of Tax Rate", "Rate", "Taxable Value", "Cess Amount", "Pre GST");
            DataTable invoicedt = conn.getdataset("select GstNo,AccountName,State,statecode,originalbillno,originalbilldate,billno,Bill_Date,totalnet,cgstper,sgstper,igstper, sum(totaltax) as totaltax,totalcess,SaleType,sum(totalt) as totalt,sum(totalc) as totalc,BillType from (select c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,(bp.cgstamt+bp.sgstamt+bp.igdtamt+bp.addtax)as totaltax,b.totalcess,b.SaleType,sum(bp.Total-bp.discountamt) as totalt,sum(bp.cess) as totalc,b.BillType from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='SR' and bp.BillType='SR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'group by bp.cgstamt,bp.sgstamt,bp.igdtamt,bp.addtax, c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType,b.BillType union all select c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,b.totalcess,b.SaleType,sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as totalt,0 as totalc,b.BillType from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1 where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='SR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.plusminus,bp.addtaxamt, c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleType,bp.tax,b.BillType union all select c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Date,b.totalfinalamount,bp.cgstper,bp.sgstper,bp.igstper,(bp.cgstamt+bp.sgstamt+bp.igstamt+bp.addtax)as totaltax,0 as totalcess,b.Type,sum(bp.taxableamount) as totalt,0 as totalc,b.BillType from ClientMaster c inner join tblgstvouchermaster b on c.ClientID=b.party inner join tblgstvoucherproductmaster bp on b.billno=bp.billno  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and (b.BillType='SR' or b.BillType='CN')  and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.cgstamt,bp.sgstamt,bp.igstamt,bp.addtax, c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Date,b.totalfinalamount,bp.cgstper,bp.sgstper,bp.igstper,b.totxltax,b.Type,b.BillType union all select c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Date,b.totalfinalamount,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,0 as totalcess,b.Type,sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as totalt,0 as totalc,b.BillType from ClientMaster c inner join tblgstvouchermaster b on c.ClientID=b.party inner join tblgstvoucherchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.chargeid and bs.OT3=1 and bs.isactive=1 where c.isactive=1 and b.isactive=1 and bp.isactive=1 and (b.BillType='SR' or b.BillType='CN') and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.plusminus,bp.addtaxamt, c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Date,b.totalfinalamount,cgst,bp.sgst,bp.igst,b.totxltax,b.Type,bp.tax,b.BillType)entries group by GstNo,AccountName,State,statecode,originalbillno,originalbilldate,billno,Bill_Date,totalnet,cgstper,sgstper,igstper,totalcess,SaleType,BillType order by billno");
            //DataTable invoicedt1 = conn.getdataset("select GstNo,State,statecode,originalbillno,originalbilldate,billno,Bill_Date,totalnet,cgstper,sgstper,igstper,totaltax,totalcess,SaleType,sum(totalt) as totalt,sum(totalc) as totalc,BillType from (select c.GstNo,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.Total-bp.discountamt) as totalt,sum(bp.cess) as totalc,b.BillType from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='PR' and bp.BillType='PR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType,b.BillType union all select c.GstNo,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.valueofexp) as totalt,0 as totalc,b.BillType from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1 where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='PR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleType,bp.tax,b.BillType) entries group by GstNo,State,statecode,originalbillno,originalbilldate,billno,Bill_Date,totalnet,cgstper,sgstper,igstper,totaltax,totalcess,SaleType,BillType order by billno");
            //invoicedt1 = changedtclone(invoicedt1);
            //invoicedt = changedtclone(invoicedt);
            //invoicedt.Merge(invoicedt1); 
            for (int i = 0; i < invoicedt.Rows.Count; i++)
            {
                DataTable ecomgst = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypeid='" + invoicedt.Rows[i]["SaleType"].ToString() + "'");
                if (ecomgst.Rows.Count > 0)
                {
                    if (ecomgst.Rows[0]["TaxCalculation"].ToString() == "Tax Invoice")
                    {
                        string d = Convert.ToDateTime(invoicedt.Rows[i]["Bill_Date"]).ToString(Master.dateformate);
                        string place = invoicedt.Rows[i]["statecode"].ToString() + "-" + invoicedt.Rows[i]["State"].ToString();
                        double rate = Convert.ToDouble(invoicedt.Rows[i]["cgstper"].ToString()) + Convert.ToDouble(invoicedt.Rows[i]["sgstper"].ToString());
                        double igst = Convert.ToDouble(invoicedt.Rows[i]["igstper"].ToString());
                        
                        if (invoicedt.Rows[i]["BillType"].ToString() == "SR")
                        {
                            documenttype = "C";
                            type = "Sales Return";
                        }
                        //else
                        //{
                        //    documenttype = "D";
                        //    type = "Purchase Return";
                        //}
                        if (ecomgst.Rows[0]["Region"].ToString() == "Local")
                        {
                            main.Rows.Add(invoicedt.Rows[i]["GstNo"].ToString(), invoicedt.Rows[i]["originalbillno"].ToString(), invoicedt.Rows[i]["originalbilldate"].ToString(), invoicedt.Rows[i]["billno"].ToString(), d, documenttype, place, invoicedt.Rows[i]["totalnet"].ToString(), "", rate, invoicedt.Rows[i]["totalt"].ToString(), invoicedt.Rows[i]["totalc"].ToString(), "N");
                            //totalbillno++;
                        }
                        else
                        {
                            main.Rows.Add(invoicedt.Rows[i]["GstNo"].ToString(), invoicedt.Rows[i]["originalbillno"].ToString(), invoicedt.Rows[i]["originalbilldate"].ToString(), invoicedt.Rows[i]["billno"].ToString(), d, documenttype, place, invoicedt.Rows[i]["totalnet"].ToString(), "", igst, invoicedt.Rows[i]["totalt"].ToString(), invoicedt.Rows[i]["totalc"].ToString(), "N");
                            // totalbillno++;
                        }

                        totalnetvaluecndr += Convert.ToDouble(invoicedt.Rows[i]["totalnet"].ToString());
                        totaltaxcndr += Convert.ToDouble(invoicedt.Rows[i]["totalt"].ToString());
                        totalcesscndr += Convert.ToDouble(invoicedt.Rows[i]["totalc"].ToString());
                    }
                }
            }
            DataTable finalbill = conn.getdataset("select DISTINCT GstNo from(select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.tax) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno  inner join PurchasetypeMaster p on p.Purchasetypeid=b.SaleType where p.TaxCalculation='Tax Invoice' and c.isactive=1 and p.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='SR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType union all select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.cgst+bp.sgst+bp.igst) as totalt,0 as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1  inner join PurchasetypeMaster p on p.Purchasetypeid=b.SaleType where p.TaxCalculation='Tax Invoice' and c.isactive=1 and p.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='SR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleType,bp.tax) entries ");
            DataTable final = conn.getdataset("select DISTINCT billno from(select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.tax) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno  inner join PurchasetypeMaster p on p.Purchasetypeid=b.SaleType where p.TaxCalculation='Tax Invoice' and c.isactive=1 and p.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='SR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType union all select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.cgst+bp.sgst+bp.igst) as totalt,0 as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1  inner join PurchasetypeMaster p on p.Purchasetypeid=b.SaleType where p.TaxCalculation='Tax Invoice' and c.isactive=1 and p.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='SR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleType,bp.tax) entries ");
            totalbillnocndr = Convert.ToString(finalbill.Rows.Count);
            totalgstnocndr = Convert.ToString(final.Rows.Count);
            main.Rows[2][1] = totalgstnocndr;
            main.Rows[2][0] = totalbillnocndr;
            main.Rows[2][8] = totalnetvaluecndr;
            main.Rows[2][10] = totaltaxcndr;
            main.Rows[2][11] = totalcesscndr;

            dgvcndr.DataSource = main;
            //   griddesign();
            DataSet ds2 = ods.getdata("select * from Path");
            path = ds2.Tables[0];
        }
        public void importexcelcndur()
        {
            main = new DataTable();

            //     main.Rows.Add("Summary For B2B(4)", "", "", "", "", "", "", "","","","");
            main.Columns.Add("", typeof(string));
            main.Rows.Add("Summary For CDNUR(9B)");
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
            main.Rows.Add("No. of Notes/Vouchers", "No. of Invoices", "", "No. of Notes/Vouchers", "", "", "", "", "Total Note/Refund Voucher Value", "", "Total Taxable Value", "Total Cess", "");
            main.Rows.Add(totalgstnocndur, totalbillnocndur, "", totalnetvaluecndur, "", "", "", "", "", "", totaltaxcndur, totalcesscndur, "");
            main.Rows.Add("UR Type", "Note/Refund Voucher Number", "Note/Refund Voucher Date", "Document Type", "Invoice/Advance Receipt Number", "Invoice/Advance Receipt Date", "Place Of Supply", "Note/Refund Voucher Value", "Applicable % of Tax Rate", "Rate", "Taxable Value", "Cess Amount", "Pre GST");
            DataTable invoicedt = conn.getdataset("select GstNo,State,statecode,originalbillno,originalbilldate,billno,Bill_Date,totalnet,cgstper,sgstper,igstper,sum(totaltax) as totaltax,totalcess,SaleType,sum(totalt) as totalt,sum(totalc) as totalc from (select c.GstNo,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,(bp.cgstamt+bp.sgstamt+bp.igdtamt+bp.addtax)as totaltax,b.totalcess,b.SaleType,sum(bp.Total-bp.discountamt) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='SR' and bp.BillType='SR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.cgstamt,bp.sgstamt,bp.igdtamt,bp.addtax, c.GstNo,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totalcess,b.SaleType union all select c.GstNo,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,b.totalcess,b.SaleType,sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as totalt,0 as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1 where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='SR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.plusminus,bp.addtaxamt, c.GstNo,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleType,bp.tax union all select c.GstNo,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Date,b.totalfinalamount,bp.cgstper,bp.sgstper,bp.igstper,(bp.cgstamt+bp.sgstamt+bp.igstamt+bp.addtax)as totaltax,0 as totalcess,b.type,sum(bp.taxableamount) as totalt,0 as totalc from ClientMaster c inner join tblgstvouchermaster b on c.ClientID=b.party inner join tblgstvoucherproductmaster bp on b.billno=bp.billno  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and (b.BillType='SR' or b.BillType='CN') and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.cgstamt,bp.sgstamt,bp.igstamt,bp.addtax, c.GstNo,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Date,b.totalfinalamount,bp.cgstper,bp.sgstper,bp.igstper,b.Type union all select c.GstNo,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Date,b.totalfinalamount,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,0 as totalcess,b.Type,sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as totalt,0 as totalc from ClientMaster c inner join tblgstvouchermaster b on c.ClientID=b.party inner join tblgstvoucherchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.chargeid and bs.OT3=1 and bs.isactive=1 where c.isactive=1 and b.isactive=1 and bp.isactive=1 and (b.BillType='SR' or b.BillType='CN') and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.plusminus,bp.addtaxamt, c.GstNo,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Date,b.totalfinalamount,cgst,bp.sgst,bp.igst,b.totxltax,b.Type,bp.tax)entries group by GstNo,State,statecode,originalbillno,originalbilldate,billno,Bill_Date,totalnet,cgstper,sgstper,igstper,totalcess,SaleType order by billno");
            for (int i = 0; i < invoicedt.Rows.Count; i++)
            {
                DataTable ecomgst = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypeid='" + invoicedt.Rows[i]["SaleType"].ToString() + "'");
                if (ecomgst.Rows.Count > 0)
                {
                    if (ecomgst.Rows[0]["TaxCalculation"].ToString() != "Tax Invoice")
                    {
                        string d = Convert.ToDateTime(invoicedt.Rows[i]["Bill_Date"]).ToString(Master.dateformate);
                        string place = invoicedt.Rows[i]["statecode"].ToString() + "-" + invoicedt.Rows[i]["State"].ToString();
                        double rate = Convert.ToDouble(invoicedt.Rows[i]["cgstper"].ToString()) + Convert.ToDouble(invoicedt.Rows[i]["sgstper"].ToString());
                        double igst = Convert.ToDouble(invoicedt.Rows[i]["igstper"].ToString());
                        if (ecomgst.Rows[0]["Region"].ToString() == "Local")
                        {
                            main.Rows.Add("B2CL", invoicedt.Rows[i]["billno"].ToString(), d, invoicedt.Rows[i]["originalbillno"].ToString(), invoicedt.Rows[i]["originalbilldate"].ToString(), "R", place, invoicedt.Rows[i]["totalnet"].ToString(),"", rate, invoicedt.Rows[i]["totalt"].ToString(), invoicedt.Rows[i]["totalc"].ToString(), "N");
                            //totalbillno++;
                        }
                        else
                        {
                            main.Rows.Add("B2CL", invoicedt.Rows[i]["billno"].ToString(), d, invoicedt.Rows[i]["originalbillno"].ToString(), invoicedt.Rows[i]["originalbilldate"].ToString(), "R", place, invoicedt.Rows[i]["totalnet"].ToString(),"", igst, invoicedt.Rows[i]["totalt"].ToString(), invoicedt.Rows[i]["totalc"].ToString(), "N");
                            // totalbillno++;
                        }

                        totalnetvaluecndur += Convert.ToDouble(invoicedt.Rows[i]["totalnet"].ToString());
                        totaltaxcndur += Convert.ToDouble(invoicedt.Rows[i]["totalt"].ToString());
                        totalcesscndur += Convert.ToDouble(invoicedt.Rows[i]["totalc"].ToString());
                    }
                }
            }
            DataTable finalbill = conn.getdataset("select DISTINCT GstNo from(select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.tax) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno  inner join PurchasetypeMaster p on p.Purchasetypeid=b.SaleType where p.TaxCalculation='Tax Invoice' and c.isactive=1 and p.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='SR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType union all select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.cgst+bp.sgst+bp.igst) as totalt,0 as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1  inner join PurchasetypeMaster p on p.Purchasetypeid=b.SaleType where p.TaxCalculation='Tax Invoice' and c.isactive=1 and p.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='SR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleType,bp.tax) entries ");
            DataTable final = conn.getdataset("select DISTINCT billno from(select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.tax) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno  inner join PurchasetypeMaster p on p.Purchasetypeid=b.SaleType where p.TaxCalculation='Tax Invoice' and c.isactive=1 and p.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='SR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType union all select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.cgst+bp.sgst+bp.igst) as totalt,0 as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1  inner join PurchasetypeMaster p on p.Purchasetypeid=b.SaleType where p.TaxCalculation='Tax Invoice' and c.isactive=1 and p.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='SR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleType,bp.tax) entries ");
            totalbillnocndur = Convert.ToString(finalbill.Rows.Count);
            totalgstnocndur = Convert.ToString(final.Rows.Count);
            main.Rows[2][1] = totalgstnocndur;
            main.Rows[2][0] = totalbillnocndur;
            main.Rows[2][8] = totalnetvaluecndur;
            main.Rows[2][10] = totaltaxcndur;
            main.Rows[2][11] = totalcesscndur;

            dgvcdnur.DataSource = main;
            //   griddesign();
            DataSet ds2 = ods.getdata("select * from Path");
            path = ds2.Tables[0];
        }
        private void btnexcel_Click(object sender, EventArgs e)
        {
            importexcelb2b();
            importexcelb2cl();
            importexcelb2cs();
            importexcelhsn();
            importexcelcndr();
            importexcelcndur();
            //DataTable dtb2b = (DataTable)dgb2b.DataSource;
            //dtb2b.TableName = "b2b";
            //DataTable dtb2cl = (DataTable)dgb2cl.DataSource;
            //dtb2cl.TableName = "b2cl";
            //DataTable dtb2cs = (DataTable)dgb2cs.DataSource;
            //dtb2cs.TableName = "b2cs";
            //DataTable dtb2hsn = (DataTable)dghsn.DataSource;
            //dtb2hsn.TableName = "hsn";
            //ds1.Tables.Add(dtb2b);
            //ds1.Tables.Add(dtb2cl);
            //ds1.Tables.Add(dtb2cs);
            //ds1.Tables.Add(dtb2hsn);
            exportexcel();
            //master.RemoveCurrentTab();
        }

        private void ComplateGST_R1_Load(object sender, EventArgs e)
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

        private void btnexcel_Enter(object sender, EventArgs e)
        {
            btnexcel.UseVisualStyleBackColor = true;
            btnexcel.BackColor = Color.FromArgb(206, 204, 254);
            btnexcel.ForeColor = Color.White;
        }

        private void btnexcel_Leave(object sender, EventArgs e)
        {
            btnexcel.UseVisualStyleBackColor = true;
            btnexcel.BackColor = Color.FromArgb(51, 153, 255);
            btnexcel.ForeColor = Color.White;
        }

        private void btnexcel_MouseEnter(object sender, EventArgs e)
        {
            btnexcel.UseVisualStyleBackColor = true;
            btnexcel.BackColor = Color.FromArgb(206, 204, 254);
            btnexcel.ForeColor = Color.White;
        }

        private void btnexcel_MouseLeave(object sender, EventArgs e)
        {
            btnexcel.UseVisualStyleBackColor = true;
            btnexcel.BackColor = Color.FromArgb(51, 153, 255);
            btnexcel.ForeColor = Color.White;
        }

        private void btnclose_Enter(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = false;
            btnclose.BackColor = Color.FromArgb(248, 152, 94);
            btnclose.ForeColor = Color.White;
        }

        private void btnclose_Leave(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = true;
            btnclose.BackColor = Color.FromArgb(51, 153, 255);
            btnclose.ForeColor = Color.White;
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
