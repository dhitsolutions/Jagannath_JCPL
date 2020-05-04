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
    public partial class hsnreport : Form
    {
        private Master master;
        private TabControl tabControl;
        OleDbSettings ods = new OleDbSettings();
        DataTable main = new DataTable();
        DataTable path = new DataTable();
        Double TotalValue = 0;
        Double TotalTaxableValue = 0;
        Double TotalIntegratedTax = 0;
        Double TotalCentralTax = 0;
        Double TotalStateUTTax = 0;
        Double totalcess = 0;
        string totalhsn = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        public hsnreport()
        {
            InitializeComponent();
        }

        public hsnreport(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
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
            xlsht = xlApp.Application.Workbooks.Open(path1).Worksheets["hsn"];
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
        public void importexcel()
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
            main.Rows.Add("No. of HSN", "", "","","Total Value","Total Taxable Value","Total Integrated Tax","Total Central Tax","Total State/UT Tax","Total Cess");
            main.Rows.Add(totalhsn, "", "", "", TotalValue, TotalTaxableValue, TotalIntegratedTax, TotalCentralTax, TotalStateUTTax, totalcess);
            main.Rows.Add("HSN", "Description", "UQC", "Total Quantity", "Total Value", "Taxable Value","Integrated Tax Amount","Central Tax Amount","State/UT Tax Amount","Cess Amount");
            DataTable invoicedt = conn.getdataset("select Hsn_Sac_Code,GroupName,UQC, sum(totalqty) as totalqty,sum(totalnet) as totalnet,sum(totaltax) as totaltax,sum(igst) as igst,sum(sgst) as sgst,sum(cgst) as cgst,sum(totalcess) as totalcess from (select i.Hsn_Sac_Code,i.GroupName,u.UQC, sum(bp.qty) as totalqty,sum(bp.Amount) as totalnet,sum(bp.Total-bp.discountamt) as totaltax,sum(bp.igdtamt) as igst,sum(bp.sgstamt) as sgst,sum(bp.cgstamt) as cgst,sum(bp.cess) as totalcess from BillProductMaster bp inner join BillMaster b on bp.billno=b.billno inner join ProductMaster i on i.ProductID=bp.productid inner join unitmaster u on u.UnitName=i.unit where b.isactive=1 and bp.isactive=1 and i.isactive=1 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by i.Hsn_Sac_Code,i.GroupName,u.uqc union  select i.OT2,'' as GroupName,'UNT-UNITS' as UQC, 0 as totalqty,sum(bp.amount) as totalnet,sum(bp.valueofexp) as totaltax,sum(bp.igst)as igst,sum(bp.sgst) as sgst,sum(bp.cgst) as cgst,sum(0) as totalcess from billchargesmaster bp inner join BillMaster b on bp.billno=b.billno inner join billsundry i on i.billsundryname=bp.perticulars and i.OT3=1 and i.isactive=1 where b.isactive=1 and bp.isactive=1 and i.isactive=1 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by i.OT2 union select i.Hsn_Sac_Code,i.GroupName,u.UQC,sum(bp.qty) as totalqty,sum(bp.Total)as totalnet,sum(bp.Amount-bp.discountamt) as totaltax,0 as igst,sum(bp.sgst) as sgst,sum(bp.cgst) as cgst,sum(bp.cess) as totalcess from BillPOSProductMaster bp inner join BillPOSMaster b on bp.billno=b.billno inner join ProductMaster i on i.Product_Name=bp.ItemName inner join unitmaster u on u.UnitName=i.unit where b.isactive=1 and bp.isactive=1and i.isactive=1 and b.BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by i.Hsn_Sac_Code,i.GroupName,u.uqc union select bp.hsn,'' as GroupName,'UNT-UNITS' as UQC, 0 as totalqty,sum(bp.netamt) as totalnet,sum(bp.taxableamount) as totaltax,sum(bp.igstamt) as igst,sum(bp.sgstamt) as sgst,sum(bp.cgstamt) as cgst,0 as totalcess from tblgstvoucherproductmaster bp inner join tblgstvouchermaster b on bp.billno=b.billno and (b.party=bp.party or bp.fkid=b.id)   where b.isactive=1 and bp.isactive=1 and  b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.hsn union select i.OT2,'' as GroupName,'UNT-UNITS' as UQC, 0 as totalqty,sum(bp.amount) as totalnet,sum(bp.valueofexp) as totaltax,sum(bp.igst)as igst,sum(bp.sgst) as sgst,sum(bp.cgst) as cgst,sum(0) as totalcess from tblgstvoucherchargesmaster bp inner join tblgstvouchermaster b on bp.billno=b.billno and (b.party=bp.party or bp.fkid=b.id)   inner join billsundry i on i.billsundryname=bp.perticulars and i.OT3=1 and i.isactive=1 where b.isactive=1 and bp.isactive=1 and i.isactive=1 and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by i.OT2 ) entries group by Hsn_Sac_Code , GroupName, uqc");
              for (int i = 0; i < invoicedt.Rows.Count; i++)
              {
                  main.Rows.Add(invoicedt.Rows[i]["Hsn_Sac_Code"].ToString(), invoicedt.Rows[i]["GroupName"].ToString(), invoicedt.Rows[i]["UQC"].ToString(), invoicedt.Rows[i]["totalqty"].ToString(), invoicedt.Rows[i]["totalnet"].ToString(), invoicedt.Rows[i]["totaltax"].ToString(), invoicedt.Rows[i]["igst"].ToString(), invoicedt.Rows[i]["cgst"].ToString(), invoicedt.Rows[i]["sgst"].ToString(), invoicedt.Rows[i]["totalcess"].ToString());
                  TotalValue += Convert.ToDouble(invoicedt.Rows[i]["totalnet"].ToString());
                  TotalTaxableValue += Convert.ToDouble(invoicedt.Rows[i]["totaltax"].ToString());
                  TotalIntegratedTax += Convert.ToDouble(invoicedt.Rows[i]["igst"].ToString());
                  TotalCentralTax += Convert.ToDouble(invoicedt.Rows[i]["cgst"].ToString());
                  TotalStateUTTax += Convert.ToDouble(invoicedt.Rows[i]["sgst"].ToString());
                  totalcess += Convert.ToDouble(invoicedt.Rows[i]["totalcess"].ToString());
              }
            //  DataTable final = conn.getdataset("select DISTINCT Hsn_Sac_Code from (select i.Hsn_Sac_Code,i.GroupName,u.UQC, sum(bp.qty) as totalqty,sum(b.totalnet) as totalnet,sum(bp.tax) as totaltax,sum(bp.sgstamt + bp.cgstamt) as igst,sum(bp.sgstamt) as sgst,sum(bp.cgstamt) as cgst,sum(bp.cess) as totalcess from BillProductMaster bp inner join BillMaster b on bp.billno=b.billno inner join ProductMaster i on i.Product_Name=bp.Productname inner join unitmaster u on u.UnitName=i.unit where b.isactive=1 and bp.isactive=1 and i.isactive=1 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by i.Hsn_Sac_Code,i.GroupName,u.uqc union  select i.OT2,'' as GroupName,'UNT-UNITS' as UQC, 0 as totalqty,sum(bp.amount) as totalnet,sum(bp.valueofexp) as totaltax,sum(bp.igst)as igst,sum(bp.sgst) as sgst,sum(bp.cgst) as cgst,sum(0) as totalcess from billchargesmaster bp inner join BillMaster b on bp.billno=b.billno inner join billsundry i on i.billsundryname=bp.perticulars where b.isactive=1 and bp.isactive=1 and i.isactive=1 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by i.OT2 union select i.Hsn_Sac_Code,i.GroupName,u.UQC,sum(bp.qty) as totalqty,sum(b.totalnet)as totalnet,sum(bp.sgst+bp.cgst+bp.igst) as totaltax,sum(bp.sgst + bp.cgst) as igst,sum(bp.sgst) as sgst,sum(bp.cgst) as cgst,sum(bp.cess) as totalcess from BillPOSProductMaster bp inner join BillPOSMaster b on bp.billno=b.billno inner join ProductMaster i on i.Product_Name=bp.ItemName inner join unitmaster u on u.UnitName=i.unit where b.isactive=1 and bp.isactive=1and i.isactive=1 and b.BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by i.Hsn_Sac_Code,i.GroupName,u.uqc) entries group by Hsn_Sac_Code");
              DataTable final = conn.getdataset("select DISTINCT Hsn_Sac_Code from (select i.Hsn_Sac_Code,i.GroupName,u.UQC, sum(bp.qty) as totalqty,sum(bp.Amount) as totalnet,sum(bp.Total-bp.discountamt) as totaltax,sum(bp.igdtamt) as igst,sum(bp.sgstamt) as sgst,sum(bp.cgstamt) as cgst,sum(bp.cess) as totalcess from BillProductMaster bp inner join BillMaster b on bp.billno=b.billno inner join ProductMaster i on i.ProductID=bp.productid inner join unitmaster u on u.UnitName=i.unit where b.isactive=1 and bp.isactive=1 and i.isactive=1 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by i.Hsn_Sac_Code,i.GroupName,u.uqc union  select i.OT2,'' as GroupName,'UNT-UNITS' as UQC, 0 as totalqty,sum(bp.amount) as totalnet,sum(bp.valueofexp) as totaltax,sum(bp.igst)as igst,sum(bp.sgst) as sgst,sum(bp.cgst) as cgst,sum(0) as totalcess from billchargesmaster bp inner join BillMaster b on bp.billno=b.billno inner join billsundry i on i.billsundryname=bp.perticulars and i.OT3=1 and i.isactive=1 where b.isactive=1 and bp.isactive=1 and i.isactive=1 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by i.OT2 union select i.Hsn_Sac_Code,i.GroupName,u.UQC,sum(bp.qty) as totalqty,sum(bp.Total)as totalnet,sum(bp.Amount-bp.discountamt) as totaltax,0 as igst,sum(bp.sgst) as sgst,sum(bp.cgst) as cgst,sum(bp.cess) as totalcess from BillPOSProductMaster bp inner join BillPOSMaster b on bp.billno=b.billno inner join ProductMaster i on i.Product_Name=bp.ItemName inner join unitmaster u on u.UnitName=i.unit where b.isactive=1 and bp.isactive=1and i.isactive=1 and b.BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by i.Hsn_Sac_Code,i.GroupName,u.uqc union select bp.hsn,'' as GroupName,'UNT-UNITS' as UQC, 0 as totalqty,sum(bp.netamt) as totalnet,sum(bp.taxableamount) as totaltax,sum(bp.igstamt) as igst,sum(bp.sgstamt) as sgst,sum(bp.cgstamt) as cgst,0 as totalcess from tblgstvoucherproductmaster bp inner join tblgstvouchermaster b on bp.billno=b.billno and (b.party=bp.party or bp.fkid=b.id)   where b.isactive=1 and bp.isactive=1 and  b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.hsn union select i.OT2,'' as GroupName,'UNT-UNITS' as UQC, 0 as totalqty,sum(bp.amount) as totalnet,sum(bp.valueofexp) as totaltax,sum(bp.igst)as igst,sum(bp.sgst) as sgst,sum(bp.cgst) as cgst,sum(0) as totalcess from tblgstvoucherchargesmaster bp inner join tblgstvouchermaster b on bp.billno=b.billno and (b.party=bp.party or bp.fkid=b.id)   inner join billsundry i on i.billsundryname=bp.perticulars and i.OT3=1 and i.isactive=1 where b.isactive=1 and bp.isactive=1 and i.isactive=1 and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by i.OT2 ) entries group by Hsn_Sac_Code");
             totalhsn = Convert.ToString(final.Rows.Count);
              main.Rows[2][0] = totalhsn;
              main.Rows[2][4] = TotalValue;
              main.Rows[2][5] = TotalTaxableValue;
              main.Rows[2][6] = TotalIntegratedTax;
              main.Rows[2][7] = TotalCentralTax;
              main.Rows[2][8] = TotalStateUTTax;
              main.Rows[2][9] = totalcess;

              dataGridView1.DataSource = main;
              DataSet ds2 = ods.getdata("select * from Path");
              path = ds2.Tables[0];
        }
        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
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
        private void hsnreport_Load(object sender, EventArgs e)
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
                            wb.SaveAs(folderPath + "GST Register(HSN REPORT)" + DateTimeName + ".xlsx");
                        }
                        DialogResult dr = MessageBox.Show("Do you want to Open Document?", "GST Register(HSN REPORT)", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(folderPath + "GST Register(HSN REPORT)" + DateTimeName + ".xlsx");
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
        public void binddate()
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
                main.Columns.Add("HSN", typeof(string));
                main.Columns.Add("Description", typeof(string));
                main.Columns.Add("UQC", typeof(string));
                main.Columns.Add("Total Quantity", typeof(string));
                main.Columns.Add("Total Value", typeof(string));
                main.Columns.Add("Taxable Value", typeof(string));
                main.Columns.Add("Integrated Tax Amount", typeof(string));
                main.Columns.Add("Central Tax Amount", typeof(string));
                main.Columns.Add("State/UT Tax Amount", typeof(string));
                main.Columns.Add("Cess Amount", typeof(string));

              //  DataTable invoicedt = conn.getdataset("select i.Hsn_Sac_Code,i.GroupName,u.UQC, sum(bp.qty) as totalqty,sum(b.totalnet) as totalnet,sum(bp.tax) as totaltax,sum(bp.sgstamt + bp.cgstamt) as igst,sum(bp.sgstamt) as sgst,sum(bp.cgstamt) as cgst,sum(bp.cess) as totalcess from BillProductMaster bp inner join BillMaster b on bp.billno=b.billno inner join ProductMaster i on i.Product_Name=bp.Productname inner join unitmaster u on u.UnitName=i.unit where b.isactive=1 and bp.isactive=1 and i.isactive=1 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by i.Hsn_Sac_Code,i.GroupName,u.uqc union all select i.Hsn_Sac_Code,i.GroupName,u.UQC,sum(bp.qty) as totalqty,sum(b.totalnet)as totalnet,sum(bp.sgst+bp.cgst) as totaltax,sum(bp.sgst + bp.cgst) as igst,sum(bp.sgst) as sgst,sum(bp.cgst) as cgst,sum(bp.cess) as totalcess from BillPOSProductMaster bp inner join BillPOSMaster b on bp.billno=b.billno inner join ProductMaster i on i.Product_Name=bp.ItemName inner join unitmaster u on u.UnitName=i.unit where b.isactive=1 and bp.isactive=1and i.isactive=1 and b.BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by i.Hsn_Sac_Code,i.GroupName,u.uqc");
                //DataTable invoicedt = conn.getdataset("select Hsn_Sac_Code,GroupName,UQC, sum(totalqty) as totalqty,sum(totalnet) as totalnet,sum(totaltax) as totaltax,sum(igst) as igst,sum(sgst) as sgst,sum(cgst) as cgst,sum(totalcess) as totalcess from (select i.Hsn_Sac_Code,i.GroupName,u.UQC, sum(bp.qty) as totalqty,sum(bp.Amount) as totalnet,sum(bp.Total-bp.discountamt) as totaltax,sum(bp.igdtamt) as igst,sum(bp.sgstamt) as sgst,sum(bp.cgstamt) as cgst,sum(bp.cess) as totalcess from BillProductMaster bp inner join BillMaster b on bp.billno=b.billno inner join ProductMaster i on i.Product_Name=bp.Productname inner join unitmaster u on u.UnitName=i.unit where b.isactive=1 and bp.isactive=1 and i.isactive=1 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by i.Hsn_Sac_Code,i.GroupName,u.uqc union  select i.OT2,'' as GroupName,'UNT-UNITS' as UQC, 0 as totalqty,sum(bp.amount) as totalnet,sum(bp.valueofexp) as totaltax,sum(bp.igst)as igst,sum(bp.sgst) as sgst,sum(bp.cgst) as cgst,sum(0) as totalcess from billchargesmaster bp inner join BillMaster b on bp.billno=b.billno inner join billsundry i on i.billsundryname=bp.perticulars and i.OT3=1 and i.isactive=1 where b.isactive=1 and bp.isactive=1 and i.isactive=1 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by i.OT2 union select i.Hsn_Sac_Code,i.GroupName,u.UQC,sum(bp.qty) as totalqty,sum(bp.Total)as totalnet,sum(bp.Amount-bp.discountamt) as totaltax,0 as igst,sum(bp.sgst) as sgst,sum(bp.cgst) as cgst,sum(bp.cess) as totalcess from BillPOSProductMaster bp inner join BillPOSMaster b on bp.billno=b.billno inner join ProductMaster i on i.Product_Name=bp.ItemName inner join unitmaster u on u.UnitName=i.unit where b.isactive=1 and bp.isactive=1and i.isactive=1 and b.BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by i.Hsn_Sac_Code,i.GroupName,u.uqc) entries group by Hsn_Sac_Code , GroupName, uqc");
               // DataTable invoicedt = conn.getdataset("select Hsn_Sac_Code,GroupName,UQC, sum(totalqty) as totalqty,sum(totalnet) as totalnet,sum(totaltax) as totaltax,sum(igst) as igst,sum(sgst) as sgst,sum(cgst) as cgst,sum(totalcess) as totalcess from (select i.Hsn_Sac_Code,i.GroupName,u.UQC, sum(bp.qty) as totalqty,sum(bp.Amount) as totalnet,sum(bp.Total-bp.discountamt) as totaltax,sum(bp.igdtamt) as igst,sum(bp.sgstamt) as sgst,sum(bp.cgstamt) as cgst,sum(bp.cess) as totalcess from BillProductMaster bp inner join BillMaster b on bp.billno=b.billno inner join ProductMaster i on i.Product_Name=bp.Productname inner join unitmaster u on u.UnitName=i.unit where b.isactive=1 and bp.isactive=1 and i.isactive=1 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and b.BillType='S' and bp.BillType='S'group by i.Hsn_Sac_Code,i.GroupName,u.uqc union select i.Hsn_Sac_Code,i.GroupName,u.UQC, sum(bp.qty)*-1 as totalqty,sum(bp.Amount)*-1 as totalnet,sum(bp.Total-bp.discountamt)*-1 as totaltax,sum(bp.igdtamt)*-1 as igst,sum(bp.sgstamt)*-1 as sgst,sum(bp.cgstamt)*-1 as cgst,sum(bp.cess)*-1 as totalcess from BillProductMaster bp inner join BillMaster b on bp.billno=b.billno inner join ProductMaster i on i.Product_Name=bp.Productname inner join unitmaster u on u.UnitName=i.unit where b.isactive=1 and bp.isactive=1 and i.isactive=1 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and b.BillType='SR' and bp.BillType='SR'group by i.Hsn_Sac_Code,i.GroupName,u.uqc union  select i.OT2,'' as GroupName,'UNT-UNITS' as UQC, 0 as totalqty,sum(bp.amount) as totalnet,sum(bp.valueofexp) as totaltax,sum(bp.igst)as igst,sum(bp.sgst) as sgst,sum(bp.cgst) as cgst,sum(0) as totalcess from billchargesmaster bp inner join BillMaster b on bp.billno=b.billno inner join billsundry i on i.billsundryname=bp.perticulars and i.OT3=1 and i.isactive=1 where b.isactive=1 and bp.isactive=1 and i.isactive=1 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (b.BillType='SR' or b.BillType='S')group by i.OT2 ,b.BillType union select i.Hsn_Sac_Code,i.GroupName,u.UQC,sum(bp.qty) as totalqty,sum(bp.Total)as totalnet,sum(bp.Amount-bp.discountamt) as totaltax,0 as igst,sum(bp.sgst) as sgst,sum(bp.cgst) as cgst,sum(bp.cess) as totalcess from BillPOSProductMaster bp inner join BillPOSMaster b on bp.billno=b.billno inner join ProductMaster i on i.Product_Name=bp.ItemName inner join unitmaster u on u.UnitName=i.unit where b.isactive=1 and bp.isactive=1and i.isactive=1 and b.BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by i.Hsn_Sac_Code,i.GroupName,u.uqc) entries group by Hsn_Sac_Code , GroupName, uqc");
                DataTable invoicedt = conn.getdataset("select Hsn_Sac_Code,GroupName,UQC, sum(totalqty) as totalqty,sum(totalnet) as totalnet,sum(totaltax) as totaltax,sum(igst) as igst,sum(sgst) as sgst,sum(cgst) as cgst,sum(totalcess) as totalcess from (select i.Hsn_Sac_Code,i.GroupName,u.UQC, sum(bp.qty) as totalqty,sum(bp.Amount) as totalnet,sum(bp.Total-bp.discountamt) as totaltax,sum(bp.igdtamt) as igst,sum(bp.sgstamt) as sgst,sum(bp.cgstamt) as cgst,sum(bp.cess) as totalcess from BillProductMaster bp inner join BillMaster b on bp.billno=b.billno inner join ProductMaster i on i.ProductID=bp.productid inner join unitmaster u on u.UnitName=i.unit where b.isactive=1 and bp.isactive=1 and i.isactive=1 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by i.Hsn_Sac_Code,i.GroupName,u.uqc union  select i.OT2,'' as GroupName,'UNT-UNITS' as UQC, 0 as totalqty,sum(bp.amount) as totalnet,sum(bp.valueofexp) as totaltax,sum(bp.igst)as igst,sum(bp.sgst) as sgst,sum(bp.cgst) as cgst,sum(0) as totalcess from billchargesmaster bp inner join BillMaster b on bp.billno=b.billno inner join billsundry i on i.billsundryname=bp.perticulars and i.OT3=1 and i.isactive=1 where b.isactive=1 and bp.isactive=1 and i.isactive=1 and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by i.OT2 union select i.Hsn_Sac_Code,i.GroupName,u.UQC,sum(bp.qty) as totalqty,sum(bp.Total)as totalnet,sum(bp.Amount-bp.discountamt) as totaltax,0 as igst,sum(bp.sgst) as sgst,sum(bp.cgst) as cgst,sum(bp.cess) as totalcess from BillPOSProductMaster bp inner join BillPOSMaster b on bp.billno=b.billno inner join ProductMaster i on i.Product_Name=bp.ItemName inner join unitmaster u on u.UnitName=i.unit where b.isactive=1 and bp.isactive=1and i.isactive=1 and b.BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by i.Hsn_Sac_Code,i.GroupName,u.uqc union select bp.hsn,'' as GroupName,'UNT-UNITS' as UQC, 0 as totalqty,sum(bp.netamt) as totalnet,sum(bp.taxableamount) as totaltax,sum(bp.igstamt) as igst,sum(bp.sgstamt) as sgst,sum(bp.cgstamt) as cgst,0 as totalcess from tblgstvoucherproductmaster bp inner join tblgstvouchermaster b on bp.billno=b.billno and (b.party=bp.party or bp.fkid=b.id)   where b.isactive=1 and bp.isactive=1 and  b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.hsn union select i.OT2,'' as GroupName,'UNT-UNITS' as UQC, 0 as totalqty,sum(bp.amount) as totalnet,sum(bp.valueofexp) as totaltax,sum(bp.igst)as igst,sum(bp.sgst) as sgst,sum(bp.cgst) as cgst,sum(0) as totalcess from tblgstvoucherchargesmaster bp inner join tblgstvouchermaster b on bp.billno=b.billno and (b.party=bp.party or bp.fkid=b.id)   inner join billsundry i on i.billsundryname=bp.perticulars and i.OT3=1 and i.isactive=1 where b.isactive=1 and bp.isactive=1 and i.isactive=1 and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by i.OT2 ) entries group by Hsn_Sac_Code , GroupName, uqc");
                for (int i = 0; i < invoicedt.Rows.Count; i++)
                {
                    DataRow dr = main.NewRow();
                    dr["HSN"] = invoicedt.Rows[i]["Hsn_Sac_Code"].ToString();
                    dr["Description"] = invoicedt.Rows[i]["GroupName"].ToString();
                    dr["UQC"] = invoicedt.Rows[i]["UQC"].ToString();
                    dr["Total Quantity"] = invoicedt.Rows[i]["totalqty"].ToString();
                    dr["Total Value"] = invoicedt.Rows[i]["totalnet"].ToString();
                    dr["Taxable Value"] = invoicedt.Rows[i]["totaltax"].ToString();
                    dr["Integrated Tax Amount"] = invoicedt.Rows[i]["igst"].ToString();
                    dr["Central Tax Amount"] = invoicedt.Rows[i]["cgst"].ToString();
                    dr["State/UT Tax Amount"] = invoicedt.Rows[i]["sgst"].ToString();
                    dr["Cess Amount"] = invoicedt.Rows[i]["totalcess"].ToString();

                    main.Rows.Add(dr);
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
            binddate();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void BtnViewReport_Enter(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = false;
            BtnViewReport.BackColor = System.Drawing.Color.FromArgb(94, 191, 174);
            BtnViewReport.ForeColor = System.Drawing.Color.White;
        }

        private void BtnViewReport_Leave(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = true;
            BtnViewReport.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            BtnViewReport.ForeColor = System.Drawing.Color.White;
        }

        private void BtnViewReport_MouseEnter(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = false;
            BtnViewReport.BackColor = System.Drawing.Color.FromArgb(94, 191, 174);
            BtnViewReport.ForeColor = System.Drawing.Color.White;
        }

        private void BtnViewReport_MouseLeave(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = true;
            BtnViewReport.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            BtnViewReport.ForeColor = System.Drawing.Color.White;
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

        private void btnclose_Enter(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = false;
            btnclose.BackColor = System.Drawing.Color.FromArgb(248, 152, 94);
            btnclose.ForeColor = System.Drawing.Color.White;
        }

        private void btnclose_Leave(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = true;
            btnclose.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnclose.ForeColor = System.Drawing.Color.White;
        }

        private void btnclose_MouseEnter(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = false;
            btnclose.BackColor = System.Drawing.Color.FromArgb(248, 152, 94);
            btnclose.ForeColor = System.Drawing.Color.White;
        }

        private void btnclose_MouseLeave(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = true;
            btnclose.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnclose.ForeColor = System.Drawing.Color.White;
        }
    }
}
