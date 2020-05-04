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
    public partial class cdnr_GSTR2_ : Form
    {
        private Master master;
        private TabControl tabControl;
        OleDbSettings ods = new OleDbSettings();
        DataTable main = new DataTable();
        DataTable main1 = new DataTable();
        DataTable path = new DataTable();
        Double totalcess = 0;
        Double totaltax = 0;
        Double totalnetvalue = 0;
        Double totaligst = 0;
        Double totalcgst = 0;
        Double totalsgst = 0;
        string totalbillno = "";
        string totalgstno = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        public cdnr_GSTR2_()
        {
            InitializeComponent();
        }

        public cdnr_GSTR2_(Master master, TabControl tabControl)
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

        private void cdnr_Load(object sender, EventArgs e)
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
                main.Columns.Add("Note/Refund Voucher Number", typeof(string));
                main.Columns.Add("Note/Refund Voucher date", typeof(string));
                main.Columns.Add("Invoice/Advance Payment Voucher Number", typeof(string));
                main.Columns.Add("Invoice/Advance Payment Voucher date",typeof(string));
                main.Columns.Add("Pre GST",typeof(string));
                main.Columns.Add("Document Type", typeof(string));
                main.Columns.Add("Reason For Issuing Document", typeof(string));
                main.Columns.Add("Supply Type", typeof(string));
                main.Columns.Add("Note/Refund Voucher Value", typeof(string));
               // main.Columns.Add("Applicable % of Tax Rate", typeof(string));
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

                
              //  DataTable invoicedt = conn.getdataset("select GstNo,AccountName,State,statecode,originalbillno,originalbilldate,billno,Bill_Date,totalnet,cgstper,sum(cgstamt) as cgstamt,sgstper,sum(sgstamt) as sgstamt,igstper,sum(igstamt) as igstamt, sum(totaltax) as totaltax,totalcess,SaleType,sum(totalt) as totalt,sum(totalc) as totalc,BillType from (select c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,sum(bp.cgstamt) as cgstamt,sum(bp.sgstamt) as sgstamt,sum(bp.igdtamt) as igstamt,(bp.cgstamt+bp.sgstamt+bp.igdtamt+bp.addtax)as totaltax,b.totalcess,b.SaleType,sum(bp.Total-bp.discountamt) as totalt,sum(bp.cess) as totalc,b.BillType  from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='PR' and bp.BillType='PR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'  group by bp.cgstamt,bp.sgstamt,bp.igdtamt,bp.addtax, c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType,b.BillType  union all  select c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,sum(case when bp.plusminus='-' then bp.cgst*-1 when bp.plusminus='+' then bp.cgst end) as cgstamt,sum(case when bp.plusminus='-' then bp.sgst*-1 when bp.plusminus='+' then bp.sgst end) as sgstamt,sum(case when bp.plusminus='-' then bp.igst*-1 when bp.plusminus='+' then bp.igst end) as igstamt,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,b.totalcess,b.SaleType,sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as totalt,0 as totalc,b.BillType  from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='PR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'  group by bp.plusminus,bp.addtaxamt, c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleType,bp.tax,b.BillType)entries  group by GstNo,AccountName,State,statecode,originalbillno,originalbilldate,billno,Bill_Date,totalnet,cgstper,sgstper,igstper,totalcess,SaleType,BillType order by billno");
                DataTable invoicedt = conn.getdataset("select GstNo,AccountName,State,statecode,originalbillno,originalbilldate,billno,Bill_Date,totalnet,cgstper,sum(cgstamt) as cgstamt,sgstper,sum(sgstamt) as sgstamt,igstper,sum(igstamt) as igstamt, sum(totaltax) as totaltax,totalcess,SaleType,sum(totalt) as totalt,sum(totalc) as totalc,BillType from (select c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,sum(bp.cgstamt) as cgstamt,sum(bp.sgstamt) as sgstamt,sum(bp.igdtamt) as igstamt,(bp.cgstamt+bp.sgstamt+bp.igdtamt+bp.addtax)as totaltax,b.totalcess,b.SaleType,sum(bp.Total-bp.discountamt) as totalt,sum(bp.cess) as totalc,b.BillType  from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='PR' and bp.BillType='PR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'  group by bp.cgstamt,bp.sgstamt,bp.igdtamt,bp.addtax, c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType,b.BillType  union all  select c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.date as Bill_Date,b.totalfinalamount,bp.cgstper,bp.sgstper,bp.igstper,sum(bp.cgstamt) as cgstamt,sum(bp.sgstamt) as sgstamt,sum(bp.igstamt) as igstamt,(bp.cgstamt+bp.sgstamt+bp.igstamt+bp.addtax)as totaltax,0 as totalcess,b.Type,sum(taxableamount) as totalt,sum(0) as totalc,b.BillType  from ClientMaster c inner join tblgstvouchermaster b on c.ClientID=b.party inner join tblgstvoucherproductmaster bp on b.billno=bp.billno  and (b.party=bp.party or b.id=bp.fkid) where c.isactive=1 and b.isactive=1 and bp.isactive=1 and (b.BillType='PR' or bp.BillType='DN') and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'  group by bp.cgstamt,bp.sgstamt,bp.igstamt,bp.addtax, c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Date,b.totalfinalamount,bp.cgstper,bp.sgstper,bp.igstper,b.totxltax,b.Type,b.BillType  union all select c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.date as Bill_Date,b.totalfinalamount,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,sum(case when bp.plusminus='-' then bp.cgst*-1 when bp.plusminus='+' then bp.cgst end) as cgstamt,sum(case when bp.plusminus='-' then bp.sgst*-1 when bp.plusminus='+' then bp.sgst end) as sgstamt,sum(case when bp.plusminus='-' then bp.igst*-1 when bp.plusminus='+' then bp.igst end) as igstamt,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,0 as totalcess,b.Type,sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as totalt,0 as totalc,b.BillType  from ClientMaster c inner join tblgstvouchermaster b on c.ClientID=b.party inner join tblgstvoucherchargesmaster bp on b.billno=bp.billno and (b.party=bp.party or b.id=bp.fkid) inner join BillSundry bs on bs.BillSundryID=bp.chargeid and bs.OT3=1 and bs.isactive=1  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and (b.BillType='PR' or bp.BillType='DN') and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'  group by bp.plusminus,bp.addtaxamt, c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Date,b.totalfinalamount,cgst,bp.sgst,bp.igst,b.totxltax,b.Type,bp.tax,b.BillType union all select c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,sum(case when bp.plusminus='-' then bp.cgst*-1 when bp.plusminus='+' then bp.cgst end) as cgstamt,sum(case when bp.plusminus='-' then bp.sgst*-1 when bp.plusminus='+' then bp.sgst end) as sgstamt,sum(case when bp.plusminus='-' then bp.igst*-1 when bp.plusminus='+' then bp.igst end) as igstamt,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,b.totalcess,b.SaleType,sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as totalt,0 as totalc,b.BillType  from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='PR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'  group by bp.plusminus,bp.addtaxamt, c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleType,bp.tax,b.BillType)entries  group by GstNo,AccountName,State,statecode,originalbillno,originalbilldate,billno,Bill_Date,totalnet,cgstper,sgstper,igstper,totalcess,SaleType,BillType order by billno");
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
                             DataRow dr = main.NewRow();
                             string d = Convert.ToDateTime(invoicedt.Rows[i]["Bill_Date"]).ToString(Master.dateformate);
                             dr["GSTIN of Supplier"] = invoicedt.Rows[i]["GstNo"].ToString();
                           //  dr["Receiver Name"] = invoicedt.Rows[i]["AccountName"].ToString();
                             dr["Note/Refund Voucher Number"] = invoicedt.Rows[i]["billno"].ToString(); ;
                             dr["Note/Refund Voucher Date"] = d; ;
                             dr["Invoice/Advance Payment Voucher Number"] = invoicedt.Rows[i]["originalbillno"].ToString();
                             dr["Invoice/Advance Payment Voucher date"] = invoicedt.Rows[i]["originalbilldate"].ToString();
                             dr["Pre GST"] = "N";
                             if (invoicedt.Rows[i]["BillType"].ToString() == "PR")
                             {
                                 dr["Document Type"] = "D";
                                 dr["Reason For Issuing Document"] = "Purchase Return";
                             }
                             if (ecomgst.Rows[0]["Region"].ToString() == "Local")
                             {
                                 dr["Supply Type"] = "Intra State";
                             }
                             else
                             {
                                 dr["Supply Type"] = "Inter State";
                             }
                             dr["Note/Refund Voucher Value"] = invoicedt.Rows[i]["totalnet"].ToString();
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
                             dr["Integrated Tax Paid"] = invoicedt.Rows[i]["igstamt"].ToString();
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
                             dr["Availed ITC Integrated Tax"] = invoicedt.Rows[i]["igstamt"].ToString();
                             dr["Availed ITC Central Tax"] = invoicedt.Rows[i]["cgstamt"].ToString();
                             dr["Availed ITC State/UT Tax"] = invoicedt.Rows[i]["sgstamt"].ToString();
                             dr["Availed ITC Cess"] = invoicedt.Rows[i]["totalc"].ToString();
                             //string place = invoicedt.Rows[i]["statecode"].ToString() + "-" + invoicedt.Rows[i]["State"].ToString();
                             //dr["Place Of Supply"] = place;
                             //dr["Applicable % of Tax Rate"] = "";

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
        string documenttype, type,eligiable;
        public void importexcel()
        {
            main = new DataTable();

            //     main.Rows.Add("Summary For B2B(4)", "", "", "", "", "", "", "","","","");
            main.Columns.Add("", typeof(string));
            main.Rows.Add("Summary For CDNR(6C)");
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
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Columns.Add("", typeof(string));
            main.Rows.Add("No. of Supplier", "No. of Notes/Vouchers", "", "No. of Invoices", "", "", "", "","","Total Note/Voucher Value","","Total Taxable Value","Total Integrated Tax Paid","Total Central Tax Paid","Total TState/UT Tax Paid","Total Cess","","Total Availed ITC Integrated Tax","Total Availed ITC Central Tax","Total Availed ITC State/UT Tax","Total Availed ITC Cess");
            main.Rows.Add(totalgstno, totalbillno, "", totalnetvalue, "", "", "", "", "", "", totaltax, totalcess, "", "", "", "", "", "", "", "", "");
            main.Rows.Add("GSTIN of Supplier", "Note/Refund Voucher Number", "Note/Refund Voucher date", "Invoice/Advance Payment Voucher Number", "Invoice/Advance Payment Voucher date", "Pre GST", "Document Type", "Reason For Issuing document","Supply Type", "Note/Refund Voucher Value", "Rate", "Taxable Value", "Integrated Tax Paid","Central Tax Paid","State/UT Tax Paid","Cess Paid","Eligibility For ITC","Availed ITC Integrated Tax","Availed ITC Central Tax","Availed ITC State/UT Tax","Availed ITC Cess");
            // DataTable invoicedt = conn.getdataset("select GstNo,State,statecode,originalbillno,originalbilldate,billno,Bill_Date,totalnet,cgstper,sgstper,igstper,totaltax,totalcess,SaleType,sum(totalt) as totalt,sum(totalc) as totalc from (select c.GstNo,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.Total-bp.discountamt) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='SR' and bp.BillType='SR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType union all select c.GstNo,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.valueofexp) as totalt,0 as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1 where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='SR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleType,bp.tax) entries group by GstNo,State,statecode,originalbillno,originalbilldate,billno,Bill_Date,totalnet,cgstper,sgstper,igstper,totaltax,totalcess,SaleType order by billno");
            DataTable invoicedt = conn.getdataset("select GstNo,AccountName,State,statecode,originalbillno,originalbilldate,billno,Bill_Date,totalnet,cgstper,sum(cgstamt) as cgstamt,sgstper,sum(sgstamt) as sgstamt,igstper,sum(igstamt) as igstamt, sum(totaltax) as totaltax,totalcess,SaleType,sum(totalt) as totalt,sum(totalc) as totalc,BillType from (select c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,sum(bp.cgstamt) as cgstamt,sum(bp.sgstamt) as sgstamt,sum(bp.igdtamt) as igstamt,(bp.cgstamt+bp.sgstamt+bp.igdtamt+bp.addtax)as totaltax,b.totalcess,b.SaleType,sum(bp.Total-bp.discountamt) as totalt,sum(bp.cess) as totalc,b.BillType  from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='PR' and bp.BillType='PR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'  group by bp.cgstamt,bp.sgstamt,bp.igdtamt,bp.addtax, c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType,b.BillType  union all  select c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.date as Bill_Date,b.totalfinalamount,bp.cgstper,bp.sgstper,bp.igstper,sum(bp.cgstamt) as cgstamt,sum(bp.sgstamt) as sgstamt,sum(bp.igstamt) as igstamt,(bp.cgstamt+bp.sgstamt+bp.igstamt+bp.addtax)as totaltax,0 as totalcess,b.Type,sum(taxableamount) as totalt,sum(0) as totalc,b.BillType  from ClientMaster c inner join tblgstvouchermaster b on c.ClientID=b.party inner join tblgstvoucherproductmaster bp on b.billno=bp.billno  and (b.party=bp.party or b.id=bp.fkid) where c.isactive=1 and b.isactive=1 and bp.isactive=1 and (b.BillType='PR' or bp.BillType='DN') and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'  group by bp.cgstamt,bp.sgstamt,bp.igstamt,bp.addtax, c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Date,b.totalfinalamount,bp.cgstper,bp.sgstper,bp.igstper,b.totxltax,b.Type,b.BillType  union all select c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.date as Bill_Date,b.totalfinalamount,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,sum(case when bp.plusminus='-' then bp.cgst*-1 when bp.plusminus='+' then bp.cgst end) as cgstamt,sum(case when bp.plusminus='-' then bp.sgst*-1 when bp.plusminus='+' then bp.sgst end) as sgstamt,sum(case when bp.plusminus='-' then bp.igst*-1 when bp.plusminus='+' then bp.igst end) as igstamt,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,0 as totalcess,b.Type,sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as totalt,0 as totalc,b.BillType  from ClientMaster c inner join tblgstvouchermaster b on c.ClientID=b.party inner join tblgstvoucherchargesmaster bp on b.billno=bp.billno and (b.party=bp.party or b.id=bp.fkid) inner join BillSundry bs on bs.BillSundryID=bp.chargeid and bs.OT3=1 and bs.isactive=1  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and (b.BillType='PR' or bp.BillType='DN') and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'  group by bp.plusminus,bp.addtaxamt, c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Date,b.totalfinalamount,cgst,bp.sgst,bp.igst,b.totxltax,b.Type,bp.tax,b.BillType union all select c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,sum(case when bp.plusminus='-' then bp.cgst*-1 when bp.plusminus='+' then bp.cgst end) as cgstamt,sum(case when bp.plusminus='-' then bp.sgst*-1 when bp.plusminus='+' then bp.sgst end) as sgstamt,sum(case when bp.plusminus='-' then bp.igst*-1 when bp.plusminus='+' then bp.igst end) as igstamt,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,b.totalcess,b.SaleType,sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as totalt,0 as totalc,b.BillType  from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='PR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'  group by bp.plusminus,bp.addtaxamt, c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleType,bp.tax,b.BillType)entries  group by GstNo,AccountName,State,statecode,originalbillno,originalbilldate,billno,Bill_Date,totalnet,cgstper,sgstper,igstper,totalcess,SaleType,BillType order by billno");
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
                          
                          if (invoicedt.Rows[i]["BillType"].ToString() == "PR")
                          {
                              documenttype = "D";
                              type = "Purdchase Return";
                          }
                          //else
                          //{
                          //    documenttype = "D";
                          //    type = "Purchase Return";
                          //}
                          if (Convert.ToDouble(invoicedt.Rows[i]["cgstper"].ToString()) == 0 && Convert.ToDouble(invoicedt.Rows[i]["sgstper"].ToString()) == 0 && Convert.ToDouble(invoicedt.Rows[i]["cgstper"].ToString()) == 0 && Convert.ToDouble(invoicedt.Rows[i]["igstper"].ToString()) == 0)
                          {
                              eligiable = "Ineligible";
                          }
                          else
                          {
                              eligiable = "Inputs";
                          }
                          if (ecomgst.Rows[0]["Region"].ToString() == "Local")
                          {
                              main.Rows.Add(invoicedt.Rows[i]["GstNo"].ToString(), invoicedt.Rows[i]["billno"].ToString(), d, invoicedt.Rows[i]["originalbillno"].ToString(), invoicedt.Rows[i]["originalbilldate"].ToString(),"N", documenttype, type, "Intra State",invoicedt.Rows[i]["totalnet"].ToString(), rate, invoicedt.Rows[i]["totalt"].ToString(), invoicedt.Rows[i]["igstamt"].ToString(), invoicedt.Rows[i]["cgstamt"].ToString(), invoicedt.Rows[i]["sgstamt"].ToString(), invoicedt.Rows[i]["totalc"].ToString(), eligiable, invoicedt.Rows[i]["igstamt"].ToString(), invoicedt.Rows[i]["cgstamt"].ToString(), invoicedt.Rows[i]["sgstamt"].ToString(), invoicedt.Rows[i]["totalc"].ToString());
                              //totalbillno++;
                          }
                          else
                          {
                              main.Rows.Add(invoicedt.Rows[i]["GstNo"].ToString(), invoicedt.Rows[i]["billno"].ToString(), d, invoicedt.Rows[i]["originalbillno"].ToString(), invoicedt.Rows[i]["originalbilldate"].ToString(), "N", documenttype, type, "Inter State", igst,invoicedt.Rows[i]["totalnet"].ToString(), invoicedt.Rows[i]["totalt"].ToString(), invoicedt.Rows[i]["igstamt"].ToString(), invoicedt.Rows[i]["cgstamt"].ToString(), invoicedt.Rows[i]["sgstamt"].ToString(), invoicedt.Rows[i]["totalc"].ToString(), eligiable, invoicedt.Rows[i]["igstamt"].ToString(), invoicedt.Rows[i]["cgstamt"].ToString(), invoicedt.Rows[i]["sgstamt"].ToString(), invoicedt.Rows[i]["totalc"].ToString());
                              // totalbillno++;
                          }

                          totalnetvalue += Convert.ToDouble(invoicedt.Rows[i]["totalnet"].ToString());
                          totaltax += Convert.ToDouble(invoicedt.Rows[i]["totalt"].ToString());
                          totalcess += Convert.ToDouble(invoicedt.Rows[i]["totalc"].ToString());
                          totaligst += Convert.ToDouble(invoicedt.Rows[i]["igstamt"].ToString());
                          totalsgst += Convert.ToDouble(invoicedt.Rows[i]["sgstamt"].ToString());
                          totalcgst += Convert.ToDouble(invoicedt.Rows[i]["cgstamt"].ToString());
                      }
                  }
             }
            //DISTINCT
            DataTable finalbill = conn.getdataset("select DISTINCT GstNo from (select c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,sum(bp.cgstamt) as cgstamt,sum(bp.sgstamt) as sgstamt,sum(bp.igdtamt) as igstamt, bp.cgstper,bp.sgstper,bp.igstper,(bp.cgstamt+bp.sgstamt+bp.igdtamt+bp.addtax)as totaltax,b.totalcess,b.SaleType,sum(bp.Total-bp.discountamt) as totalt,sum(bp.cess) as totalc,b.BillType  from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='PR' and bp.BillType='PR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'  group by bp.cgstamt,bp.sgstamt,bp.igdtamt,bp.addtax, c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType,b.BillType  union all  select c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,sum(case when bp.plusminus='-' then bp.cgst*-1 when bp.plusminus='+' then bp.cgst end) as cgstamt,sum(case when bp.plusminus='-' then bp.sgst*-1 when bp.plusminus='+' then bp.sgst end) as sgstamt,sum(case when bp.plusminus='-' then bp.igst*-1 when bp.plusminus='+' then bp.igst end) as igstamt,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,b.totalcess,b.SaleType,sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as totalt,0 as totalc,b.BillType  from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='PR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'  group by bp.plusminus,bp.addtaxamt, c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleType,bp.tax,b.BillType)entries");
            DataTable final = conn.getdataset("select DISTINCT billno from (select c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,sum(bp.cgstamt) as cgstamt,sum(bp.sgstamt) as sgstamt,sum(bp.igdtamt) as igstamt, bp.cgstper,bp.sgstper,bp.igstper,(bp.cgstamt+bp.sgstamt+bp.igdtamt+bp.addtax)as totaltax,b.totalcess,b.SaleType,sum(bp.Total-bp.discountamt) as totalt,sum(bp.cess) as totalc,b.BillType  from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='PR' and bp.BillType='PR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'  group by bp.cgstamt,bp.sgstamt,bp.igdtamt,bp.addtax, c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType,b.BillType  union all  select c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,sum(case when bp.plusminus='-' then bp.cgst*-1 when bp.plusminus='+' then bp.cgst end) as cgstamt,sum(case when bp.plusminus='-' then bp.sgst*-1 when bp.plusminus='+' then bp.sgst end) as sgstamt,sum(case when bp.plusminus='-' then bp.igst*-1 when bp.plusminus='+' then bp.igst end) as igstamt,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,b.totalcess,b.SaleType,sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as totalt,0 as totalc,b.BillType  from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='PR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'  group by bp.plusminus,bp.addtaxamt, c.GstNo,c.AccountName,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleType,bp.tax,b.BillType)entries");
             totalbillno = Convert.ToString(final.Rows.Count);
             totalgstno = Convert.ToString(finalbill.Rows.Count);
             main.Rows[2][0] = totalgstno;
             main.Rows[2][1] = totalbillno;
             main.Rows[2][3] = totalbillno;
             main.Rows[2][9] = totalnetvalue;
             main.Rows[2][11] = totaltax;
             main.Rows[2][12] = totaligst;
             main.Rows[2][13] = totalcgst;
             main.Rows[2][14] = totalsgst;
             main.Rows[2][15] = totalcess;
             main.Rows[2][17] = totaligst;
             main.Rows[2][18] = totalcgst;
             main.Rows[2][19] = totalsgst;
             main.Rows[2][20] = totalcess;

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
            xlsht = xlApp.Application.Workbooks.Open(path1).Worksheets["cdnr"];
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
        private void btnexcel_Click(object sender, EventArgs e)
        {
            importexcel();
            exportexcel();
        }
    }
}
