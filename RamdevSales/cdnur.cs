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
    public partial class cdnur : Form
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
        string totalbillno = "";
        string totalgstno = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        public cdnur()
        {
            InitializeComponent();
        }

        public cdnur(Master master, TabControl tabControl)
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
                main.Columns.Add("UR Type", typeof(string));
                main.Columns.Add("Note/Refund Voucher Number", typeof(string));
                main.Columns.Add("Note/Refund Voucher Date", typeof(string));
                main.Columns.Add("Document Type", typeof(string));
                main.Columns.Add("Invoice/Advance Receipt Number", typeof(string));
                main.Columns.Add("Invoice/Advance Receipt Date", typeof(string));
                //main.Columns.Add("Reason For Issuing Document", typeof(string));
                main.Columns.Add("Place OF Supply", typeof(string));
                main.Columns.Add("Note/Refund Voucher Value", typeof(string));
                main.Columns.Add("Applicable % of Tax Rate", typeof(string));
                main.Columns.Add("Rate", typeof(string));
                main.Columns.Add("Taxable Value", typeof(string));
                main.Columns.Add("Cess Amount", typeof(string));
                main.Columns.Add("Pre GST", typeof(string));
                

                //DataTable invoicedt = conn.getdataset("select GstNo,State,statecode,originalbillno,originalbilldate,billno,Bill_Date,totalnet,cgstper,sgstper,igstper,totaltax,totalcess,SaleType,sum(totalt) as totalt,sum(totalc) as totalc from (select c.GstNo,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.Total-bp.discountamt) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='SR' and bp.BillType='SR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType union all select c.GstNo,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.valueofexp) as totalt,0 as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1 where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='SR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleType,bp.tax) entries group by GstNo,State,statecode,originalbillno,originalbilldate,billno,Bill_Date,totalnet,cgstper,sgstper,igstper,totaltax,totalcess,SaleType order by billno");
                //DataTable invoicedt = conn.getdataset("select GstNo,State,statecode,originalbillno,originalbilldate,billno,Bill_Date,totalnet,cgstper,sgstper,igstper,sum(totaltax) as totaltax,totalcess,SaleType,sum(totalt) as totalt,sum(totalc) as totalc from (select c.GstNo,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,(bp.cgstamt+bp.sgstamt+bp.igdtamt+bp.addtax)as totaltax,b.totalcess,b.SaleType,sum(bp.Total-bp.discountamt) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='SR' and bp.BillType='SR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.cgstamt,bp.sgstamt,bp.igdtamt,bp.addtax, c.GstNo,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totalcess,b.SaleType union all select c.GstNo,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,b.totalcess,b.SaleType,sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as totalt,0 as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1 where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='SR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.plusminus,bp.addtaxamt, c.GstNo,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleType,bp.tax)entries group by GstNo,State,statecode,originalbillno,originalbilldate,billno,Bill_Date,totalnet,cgstper,sgstper,igstper,totalcess,SaleType order by billno");
                DataTable invoicedt = conn.getdataset("select GstNo,State,statecode,originalbillno,originalbilldate,billno,Bill_Date,totalnet,cgstper,sgstper,igstper,sum(totaltax) as totaltax,totalcess,SaleType,sum(totalt) as totalt,sum(totalc) as totalc from (select c.GstNo,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,(bp.cgstamt+bp.sgstamt+bp.igdtamt+bp.addtax)as totaltax,b.totalcess,b.SaleType,sum(bp.Total-bp.discountamt) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='SR' and bp.BillType='SR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.cgstamt,bp.sgstamt,bp.igdtamt,bp.addtax, c.GstNo,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totalcess,b.SaleType union all select c.GstNo,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,b.totalcess,b.SaleType,sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as totalt,0 as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1 where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='SR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.plusminus,bp.addtaxamt, c.GstNo,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleType,bp.tax union all select c.GstNo,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Date,b.totalfinalamount,bp.cgstper,bp.sgstper,bp.igstper,(bp.cgstamt+bp.sgstamt+bp.igstamt+bp.addtax)as totaltax,0 as totalcess,b.type,sum(bp.taxableamount) as totalt,0 as totalc from ClientMaster c inner join tblgstvouchermaster b on c.ClientID=b.party inner join tblgstvoucherproductmaster bp on b.billno=bp.billno  where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='SR' and bp.BillType='SR' and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.cgstamt,bp.sgstamt,bp.igstamt,bp.addtax, c.GstNo,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Date,b.totalfinalamount,bp.cgstper,bp.sgstper,bp.igstper,b.Type union all select c.GstNo,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Date,b.totalfinalamount,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,case when bp.plusminus='-' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt)*-1 when bp.plusminus='+' then (bp.cgst+bp.sgst+bp.igst+bp.addtaxamt) end as totaltax,0 as totalcess,b.Type,sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as totalt,0 as totalc from ClientMaster c inner join tblgstvouchermaster b on c.ClientID=b.party inner join tblgstvoucherchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.chargeid and bs.OT3=1 and bs.isactive=1 where c.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='SR' and bp.billtype='SR' and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.plusminus,bp.addtaxamt, c.GstNo,c.State,c.statecode,b.originalbillno,b.originalbilldate,b.billno,b.Date,b.totalfinalamount,cgst,bp.sgst,bp.igst,b.totxltax,b.Type,bp.tax)entries group by GstNo,State,statecode,originalbillno,originalbilldate,billno,Bill_Date,totalnet,cgstper,sgstper,igstper,totalcess,SaleType order by billno");
                for (int i = 0; i < invoicedt.Rows.Count; i++)
                {
                    DataTable ecomgst = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypeid='" + invoicedt.Rows[i]["SaleType"].ToString() + "'");
                    if (ecomgst.Rows.Count > 0)
                    {
                        if (ecomgst.Rows[0]["TaxCalculation"].ToString() != "Tax Invoice")
                        {
                            DataRow dr = main.NewRow();
                            string d = Convert.ToDateTime(invoicedt.Rows[i]["Bill_Date"]).ToString(Master.dateformate);
                            dr["UR Type"] = "B2CL";
                            dr["Note/Refund Voucher Number"] = invoicedt.Rows[i]["originalbillno"].ToString(); ;
                            dr["Note/Refund Voucher Date"] = invoicedt.Rows[i]["originalbilldate"].ToString(); ;
                            dr["Document Type"] = "R";
                            dr["Invoice/Advance Receipt Number"] = invoicedt.Rows[i]["billno"].ToString();
                            dr["Invoice/Advance Receipt Date"] = d;
                           // dr["Reason For Issuing Document"] = "Sales Return";
                            string place = invoicedt.Rows[i]["statecode"].ToString() + "-" + invoicedt.Rows[i]["State"].ToString();
                            dr["Place Of Supply"] = place;
                            dr["Note/Refund Voucher Value"] = invoicedt.Rows[i]["totalnet"].ToString();
                            dr["Applicable % of Tax Rate"] = "";
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
                            dr["Pre GST"] = "N";
                            

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
        public void importexcel()
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
            main.Rows.Add(totalgstno, totalbillno, "", totalnetvalue, "", "", "", "", "", "", totaltax, totalcess, "");
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

                        totalnetvalue += Convert.ToDouble(invoicedt.Rows[i]["totalnet"].ToString());
                        totaltax += Convert.ToDouble(invoicedt.Rows[i]["totalt"].ToString());
                        totalcess += Convert.ToDouble(invoicedt.Rows[i]["totalc"].ToString());
                    }
                }
            }
            DataTable finalbill = conn.getdataset("select DISTINCT GstNo from(select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.tax) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno  inner join PurchasetypeMaster p on p.Purchasetypeid=b.SaleType where p.TaxCalculation='Tax Invoice' and c.isactive=1 and p.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='SR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType union all select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.cgst+bp.sgst+bp.igst) as totalt,0 as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1  inner join PurchasetypeMaster p on p.Purchasetypeid=b.SaleType where p.TaxCalculation='Tax Invoice' and c.isactive=1 and p.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='SR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleType,bp.tax) entries ");
            DataTable final = conn.getdataset("select DISTINCT billno from(select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.tax) as totalt,sum(bp.cess) as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join BillProductMaster bp on b.billno=bp.billno  inner join PurchasetypeMaster p on p.Purchasetypeid=b.SaleType where p.TaxCalculation='Tax Invoice' and c.isactive=1 and p.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='SR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,b.totaltax,b.totalcess,b.SaleType union all select c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as cgstper,case when bp.igst<= 0 then (tax/2) when bp.igst> 0 then 0 end as sgstper,case when bp.igst<= 0 then 0 when bp.igst> 0 then tax end as igstper,b.totaltax,b.totalcess,b.SaleType,sum(bp.cgst+bp.sgst+bp.igst) as totalt,0 as totalc from ClientMaster c inner join BillMaster b on c.ClientID=b.ClientID inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1  inner join PurchasetypeMaster p on p.Purchasetypeid=b.SaleType where p.TaxCalculation='Tax Invoice' and c.isactive=1 and p.isactive=1 and b.isactive=1 and bp.isactive=1 and b.BillType='SR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.GstNo,c.State,c.statecode,b.billno,b.Bill_Date,b.totalnet,cgst,bp.sgst,bp.igst,b.totaltax,b.totalcess,b.SaleType,bp.tax) entries ");
            totalbillno = Convert.ToString(finalbill.Rows.Count);
            totalgstno = Convert.ToString(final.Rows.Count);
            main.Rows[2][1] = totalgstno;
            main.Rows[2][0] = totalbillno;
            main.Rows[2][8] = totalnetvalue;
            main.Rows[2][10] = totaltax;
            main.Rows[2][11] = totalcess;

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
            string path1 = appPath + @"\DefaultGSTReports\GSTR1.xlsx";
            //xlsht = xlApp.Application.Workbooks.Open(path).Worksheets["b2b"];
            //  string path11 = @"C:\Users\admin\Desktop\Test.xlsx";
            xlsht = xlApp.Application.Workbooks.Open(path1).Worksheets["cdnur"];
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

        private void cdnur_Load(object sender, EventArgs e)
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
    }
}
