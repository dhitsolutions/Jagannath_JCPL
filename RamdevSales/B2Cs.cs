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
    public partial class B2Cs : Form
    {
        private Master master;
        private TabControl tabControl;
        DataTable path = new DataTable();
        Double totalcess = 0;
        Double totalnetvalue = 0;
        OleDbSettings ods = new OleDbSettings();
        DataTable main = new DataTable();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        public B2Cs()
        {
            InitializeComponent();
        }

        public B2Cs(Master master, TabControl tabControl)
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
        private void B2Cs_Load(object sender, EventArgs e)
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
                main.Columns.Add("Type", typeof(string));
                main.Columns.Add("Place Of Supply", typeof(string));
                main.Columns.Add("Rate", typeof(string));
                main.Columns.Add("Taxable Value", typeof(string));
                main.Columns.Add("Cess Amount", typeof(string));
                main.Columns.Add("E-Comerce GSTIN", typeof(string));

              //  DataTable invoicedt = conn.getdataset("select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno   where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='" + "S" + "' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by b.Bill_Date");
                //string s = "select type,place, rate, sum(taxamt) as taxamt,cess,txtecom from (select case p.chkecom when '0' then 'OE' when '1' then 'E' end as type,(c.statecode+'-'+upper(c.State)) as place, (b.cgstper+b.sgstper+b.igstper) as rate,sum(b.tax) as taxamt,sum(b.cess) as cess,p.txtecom from billproductmaster b inner join billmaster bm on bm.billno=b.billno inner join purchasetypemaster p on p.purchasetypeid=bm.saletype and p.Region='Local' and p.type='S' and p.TaxCalculation='Bill Of Supply' inner join clientmaster c on c.clientid=bm.clientid where bm.totalnet<250000 and bm.isactive=1 and b.isactive=1 and bm.bill_date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bm.bill_date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and b.bill_Run_date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.bill_Run_date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by b.cgstper,b.sgstper,b.igstper,p.chkecom,c.statecode,c.State,p.txtecom union all select case p.chkecom when '0' then 'OE' when '1' then 'E' end as type,(bm.statecode+'-'+upper(bm.companystate)) as place, igst as rate, sum(sgst+cgst)as taxamt,sum(cess) as cess,p.txtecom from billposproductmaster bp inner join billposmaster bm on bm.billid=bp.billid inner join purchasetypemaster p on p.purchasetypeid=bm.saletypeid and p.Region='Local' and p.type='S' and p.TaxCalculation='Bill Of Supply' where bm.totalnet<250000 and  bm.isactive=1 and bp.isactive=1 and bm.billdate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bm.billdate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and bp.billRundate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bp.billRundate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.igst,p.chkecom,bm.statecode,bm.companystate,p.txtecom ) entries group by type,place, rate, cess, txtecom";
                //string s = "select type,place, rate, sum(taxamt) as taxamt,cess,txtecom from (select case p.chkecom when '0' then 'OE' when '1' then 'E' end as type,(c.statecode+'-'+upper(c.State)) as place, (b.cgstper+b.sgstper+b.igstper) as rate,sum(b.Total-b.discountamt) as taxamt,sum(b.cess) as cess,p.txtecom from billproductmaster b inner join billmaster bm on bm.billno=b.billno inner join purchasetypemaster p on p.purchasetypeid=bm.saletype and p.Region='Local' and p.type='S' and p.TaxCalculation='Bill Of Supply' inner join clientmaster c on c.clientid=bm.clientid where bm.isactive=1 and b.isactive=1 and bm.bill_date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bm.bill_date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and b.bill_Run_date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.bill_Run_date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by b.cgstper,b.sgstper,b.igstper,p.chkecom,c.statecode,c.State,p.txtecom union select case p.chkecom when '0' then 'OE' when '1' then 'E' end as type,(bm.statecode+'-'+upper(bm.companystate)) as place, igst as rate, sum(bp.Amount-bp.DiscountAmt)as taxamt,sum(cess) as cess,p.txtecom from billposproductmaster bp inner join billposmaster bm on bm.billid=bp.billid inner join purchasetypemaster p on p.purchasetypeid=bm.saletypeid and p.Region='Local' and p.type='S' and p.TaxCalculation='Bill Of Supply' where bm.isactive=1 and bp.isactive=1 and bm.billdate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bm.billdate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and bp.billRundate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bp.billRundate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.igst,p.chkecom,bm.statecode,bm.companystate,p.txtecom union select case p.chkecom when '0' then 'OE' when '1' then 'E' end as type,(c.statecode+'-'+upper(c.State)) as place, b.tax as rate,sum(case when b.plusminus='-' then (b.valueofexp)*-1 when b.plusminus='+' then (b.valueofexp) end) as taxamt,0 as cess,p.txtecom from billchargesmaster b inner join billmaster bm on bm.billno=b.billno inner join purchasetypemaster p on p.purchasetypeid=bm.saletype and p.Region='Local' and p.type='S' and p.TaxCalculation='Bill Of Supply' inner join clientmaster c on c.clientid=bm.clientid inner join BillSundry bs on bs.BillSundryID=b.billsundryid and bs.OT3=1 and bs.isactive=1 where bm.isactive=1 and b.isactive=1 and bm.bill_date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bm.bill_date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'group by b.cgst,b.sgst,b.igst,p.chkecom,c.statecode,c.State,p.txtecom,b.tax) entries group by type,place, rate, cess, txtecom";
                string s = "select type,place, rate, sum(taxamt) as taxamt,cess,txtecom from (select case p.chkecom when '0' then 'OE' when '1' then 'E' end as type,(c.statecode+'-'+upper(c.State)) as place, (b.cgstper+b.sgstper+b.igstper) as rate,sum(b.Total-b.discountamt) as taxamt,sum(b.cess) as cess,p.txtecom from billproductmaster b inner join billmaster bm on bm.billno=b.billno inner join purchasetypemaster p on p.purchasetypeid=bm.saletype and p.Region='Local' and p.type='S' and p.TaxCalculation='Bill Of Supply' inner join clientmaster c on c.clientid=bm.clientid where bm.isactive=1 and b.isactive=1 and bm.bill_date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bm.bill_date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and b.bill_Run_date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.bill_Run_date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by b.cgstper,b.sgstper,b.igstper,p.chkecom,c.statecode,c.State,p.txtecom union select case p.chkecom when '0' then 'OE' when '1' then 'E' end as type,(bm.statecode+'-'+upper(bm.companystate)) as place, igst as rate, sum(bp.Amount-bp.DiscountAmt)as taxamt,sum(cess) as cess,p.txtecom from billposproductmaster bp inner join billposmaster bm on bm.billid=bp.billid inner join purchasetypemaster p on p.purchasetypeid=bm.saletypeid and p.Region='Local' and p.type='S' and p.TaxCalculation='Bill Of Supply' where bm.isactive=1 and bp.isactive=1 and bm.billdate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bm.billdate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and bp.billRundate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bp.billRundate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.igst,p.chkecom,bm.statecode,bm.companystate,p.txtecom union select case p.chkecom when '0' then 'OE' when '1' then 'E' end as type,(c.statecode+'-'+upper(c.State)) as place, b.tax as rate,sum(case when b.plusminus='-' then (b.valueofexp)*-1 when b.plusminus='+' then (b.valueofexp) end) as taxamt,0 as cess,p.txtecom from billchargesmaster b inner join billmaster bm on bm.billno=b.billno inner join purchasetypemaster p on p.purchasetypeid=bm.saletype and p.Region='Local' and p.type='S' and p.TaxCalculation='Bill Of Supply' inner join clientmaster c on c.clientid=bm.clientid inner join BillSundry bs on bs.BillSundryID=b.billsundryid and bs.OT3=1 and bs.isactive=1 where bm.isactive=1 and b.isactive=1 and bm.bill_date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bm.bill_date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'group by b.cgst,b.sgst,b.igst,p.chkecom,c.statecode,c.State,p.txtecom,b.tax union select case p.chkecom when '0' then 'OE' when '1' then 'E' end as type,(c.statecode+'-'+upper(c.State)) as place, (b.cgstper+b.sgstper+b.igstper) as rate,sum(b.taxableamount) as taxamt,0 as cess ,p.txtecom from tblgstvoucherproductmaster b inner join tblgstvouchermaster bm on bm.billno=b.billno and (b.party=bm.party or b.fkid=bm.id) inner join purchasetypemaster p on p.purchasetypeid=bm.type and p.Region='Local' and p.FormType='S' and p.TaxCalculation='Bill Of Supply' inner join clientmaster c on c.clientid=bm.party where bm.isactive=1 and b.isactive=1 and bm.date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bm.date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by b.cgstper,b.sgstper,b.igstper,p.chkecom,c.statecode,c.State,p.txtecom union select case p.chkecom when '0' then 'OE' when '1' then 'E' end as type,(c.statecode+'-'+upper(c.State)) as place, b.tax as rate,sum(case when b.plusminus='-' then (b.valueofexp)*-1 when b.plusminus='+' then (b.valueofexp) end) as taxamt,0 as cess,p.txtecom from tblgstvoucherchargesmaster b inner join tblgstvouchermaster bm on bm.billno=b.billno and (b.party=bm.party or b.fkid=bm.id) inner join purchasetypemaster p on p.purchasetypeid=bm.type and p.Region='Local' and p.type='S' and p.TaxCalculation='Bill Of Supply' inner join clientmaster c on c.clientid=bm.party inner join BillSundry bs on bs.BillSundryID=b.chargeid and bs.OT3=1 and bs.isactive=1 where bm.isactive=1 and b.isactive=1 and bm.date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bm.date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'group by b.cgst,b.sgst,b.igst,p.chkecom,c.statecode,c.State,p.txtecom,b.tax) entries group by type,place, rate, cess, txtecom";
                DataTable btocs = conn.getdataset(s);
                for (int i = 0; i < btocs.Rows.Count; i++)
                {
                    DataRow dr = main.NewRow();
                    dr["Type"] = btocs.Rows[i]["type"].ToString();
                    dr["Place Of Supply"] = btocs.Rows[i]["place"].ToString();
                    dr["Rate"] = btocs.Rows[i]["rate"].ToString();
                    dr["Taxable Value"] = btocs.Rows[i]["taxamt"].ToString();
                    dr["Cess Amount"] = btocs.Rows[i]["cess"].ToString();
                    dr["E-Comerce GSTIN"] = btocs.Rows[i]["txtecom"].ToString();
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
                            wb.SaveAs(folderPath + "GST Register(B2CS)" + DateTimeName + ".xlsx");
                        }
                        DialogResult dr = MessageBox.Show("Do you want to Open Document?", "GST Register(B2CS)", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(folderPath + "GST Register(B2CS)" + DateTimeName + ".xlsx");
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
        public void exportexcel()
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Worksheet xlsht = new Microsoft.Office.Interop.Excel.Worksheet();
            xlApp.Visible = true;
            string appPath = path.Rows[0]["DefaultPath"].ToString();
            string path1 = appPath + @"\DefaultGSTReports\GSTR1.xlsx";
            //xlsht = xlApp.Application.Workbooks.Open(path).Worksheets["b2b"];
            //  string path11 = @"C:\Users\admin\Desktop\Test.xlsx";
            xlsht = xlApp.Application.Workbooks.Open(path1).Worksheets["b2cs"];
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
            main.Rows.Add("Summary For B2CS(7)");
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Rows.Add("", "", "","Total Taxable Value", "Total Cess", "");
            main.Rows.Add("", "","", totalnetvalue, totalcess,"");
            main.Rows.Add("Type","Place Of Supply", "Rate", "Taxable Value", "Cess Amount", "E-Commerce GSTIN");
            string s = "select type,place, rate, sum(taxamt) as taxamt,cess,txtecom from (select case p.chkecom when '0' then 'OE' when '1' then 'E' end as type,(c.statecode+'-'+upper(c.State)) as place, (b.cgstper+b.sgstper+b.igstper) as rate,sum(b.Total-b.discountamt) as taxamt,sum(b.cess) as cess,p.txtecom from billproductmaster b inner join billmaster bm on bm.billno=b.billno inner join purchasetypemaster p on p.purchasetypeid=bm.saletype and p.Region='Local' and p.type='S' and p.TaxCalculation='Bill Of Supply' inner join clientmaster c on c.clientid=bm.clientid where bm.isactive=1 and b.isactive=1 and bm.bill_date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bm.bill_date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and b.bill_Run_date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.bill_Run_date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by b.cgstper,b.sgstper,b.igstper,p.chkecom,c.statecode,c.State,p.txtecom union select case p.chkecom when '0' then 'OE' when '1' then 'E' end as type,(bm.statecode+'-'+upper(bm.companystate)) as place, igst as rate, sum(bp.Amount-bp.DiscountAmt)as taxamt,sum(cess) as cess,p.txtecom from billposproductmaster bp inner join billposmaster bm on bm.billid=bp.billid inner join purchasetypemaster p on p.purchasetypeid=bm.saletypeid and p.Region='Local' and p.type='S' and p.TaxCalculation='Bill Of Supply' where bm.isactive=1 and bp.isactive=1 and bm.billdate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bm.billdate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and bp.billRundate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bp.billRundate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.igst,p.chkecom,bm.statecode,bm.companystate,p.txtecom union select case p.chkecom when '0' then 'OE' when '1' then 'E' end as type,(c.statecode+'-'+upper(c.State)) as place, b.tax as rate,sum(case when b.plusminus='-' then (b.valueofexp)*-1 when b.plusminus='+' then (b.valueofexp) end) as taxamt,0 as cess,p.txtecom from billchargesmaster b inner join billmaster bm on bm.billno=b.billno inner join purchasetypemaster p on p.purchasetypeid=bm.saletype and p.Region='Local' and p.type='S' and p.TaxCalculation='Bill Of Supply' inner join clientmaster c on c.clientid=bm.clientid inner join BillSundry bs on bs.BillSundryID=b.billsundryid and bs.OT3=1 and bs.isactive=1 where bm.isactive=1 and b.isactive=1 and bm.bill_date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bm.bill_date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'group by b.cgst,b.sgst,b.igst,p.chkecom,c.statecode,c.State,p.txtecom,b.tax union select case p.chkecom when '0' then 'OE' when '1' then 'E' end as type,(c.statecode+'-'+upper(c.State)) as place, (b.cgstper+b.sgstper+b.igstper) as rate,sum(b.taxableamount) as taxamt,0 as cess ,p.txtecom from tblgstvoucherproductmaster b inner join tblgstvouchermaster bm on bm.billno=b.billno and (b.party=bm.party or b.fkid=bm.id) inner join purchasetypemaster p on p.purchasetypeid=bm.type and p.Region='Local' and p.FormType='S' and p.TaxCalculation='Bill Of Supply' inner join clientmaster c on c.clientid=bm.party where bm.isactive=1 and b.isactive=1 and bm.date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bm.date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by b.cgstper,b.sgstper,b.igstper,p.chkecom,c.statecode,c.State,p.txtecom union select case p.chkecom when '0' then 'OE' when '1' then 'E' end as type,(c.statecode+'-'+upper(c.State)) as place, b.tax as rate,sum(case when b.plusminus='-' then (b.valueofexp)*-1 when b.plusminus='+' then (b.valueofexp) end) as taxamt,0 as cess,p.txtecom from tblgstvoucherchargesmaster b inner join tblgstvouchermaster bm on bm.billno=b.billno and (b.party=bm.party or b.fkid=bm.id) inner join purchasetypemaster p on p.purchasetypeid=bm.type and p.Region='Local' and p.type='S' and p.TaxCalculation='Bill Of Supply' inner join clientmaster c on c.clientid=bm.party inner join BillSundry bs on bs.BillSundryID=b.chargeid and bs.OT3=1 and bs.isactive=1 where bm.isactive=1 and b.isactive=1 and bm.date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bm.date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'group by b.cgst,b.sgst,b.igst,p.chkecom,c.statecode,c.State,p.txtecom,b.tax) entries group by type,place, rate, cess, txtecom";
                DataTable btocs = conn.getdataset(s);
                for (int i = 0; i < btocs.Rows.Count; i++)
                {
                    main.Rows.Add(btocs.Rows[i]["type"].ToString(), btocs.Rows[i]["place"].ToString(), btocs.Rows[i]["rate"].ToString(), btocs.Rows[i]["taxamt"].ToString(), btocs.Rows[i]["cess"].ToString(), btocs.Rows[i]["txtecom"].ToString());

                    totalnetvalue += Convert.ToDouble(btocs.Rows[i]["taxamt"].ToString());
                    totalcess += Convert.ToDouble(btocs.Rows[i]["cess"].ToString());
                }
                main.Rows[2][3] = totalnetvalue;
                main.Rows[2][4] = totalcess;

                dataGridView1.DataSource = main;
                DataSet ds2 = ods.getdata("select * from Path");
                path = ds2.Tables[0];
        }
        private void btnexcel_Click(object sender, EventArgs e)
        {
            importexcel();
            exportexcel();
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
