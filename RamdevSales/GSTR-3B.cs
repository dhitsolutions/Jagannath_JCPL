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
    public partial class GSTR_3B : Form
    {
        private Master master;
        private TabControl tabControl;
        Printing prn = new Printing();
        OleDbSettings ods = new OleDbSettings();
        DataTable main1 = new DataTable();
        DataTable main2 = new DataTable();
        DataTable main3 = new DataTable();
        DataTable main4 = new DataTable();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        public GSTR_3B()
        {
            InitializeComponent();
        }

        public GSTR_3B(Master master, TabControl tabControl)
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

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

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
                lv1.Columns.Clear();
                lv1.Items.Clear();
                lv2.Columns.Clear();
                lv2.Items.Clear();
                lv3.Columns.Clear();
                lv4.Items.Clear();
                lv4.Columns.Clear();
                lv4.Items.Clear();
                // LV1
                #region
                main1 = new DataTable();
                main1.Columns.Add("Particulars", typeof(string));
                main1.Columns.Add("Taxable Amt.", typeof(string));
                main1.Columns.Add("IGST", typeof(string));
                main1.Columns.Add("CGST", typeof(string));
                main1.Columns.Add("SGST", typeof(string));
                main1.Columns.Add("Cess", typeof(string));

                DataTable invoicedt = conn.getdataset("select sum(taxableamt) as taxableamt,sum(igstamt) as igstamt,sum(cgstamt) as cgstamt,sum(sgstamt) as sgstamt,sum(cess) as cess from (select sum(total-discountamt) as taxableamt,0 as igstamt, sum(cgst) as cgstamt, sum(sgst) as sgstamt, sum(cess) as cess from BillPOSProductMaster where isactive=1 and  BillRunDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and BillRunDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (cgst+sgst)>0 union select sum(total-discountamt) as taxableamt,sum(igdtamt) as igstamt, sum(cgstamt) as cgstamt, sum(sgstamt) as sgstamt, sum(cess) as cess from BillProductMaster where isactive=1 and BillType='S' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (cgstamt+sgstamt+igdtamt)>0 union select sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as taxableamt,sum(case when bp.plusminus='-' then (bp.igst)*-1 when bp.plusminus='+' then (bp.igst) end) as igstamt, sum(case when bp.plusminus='-' then (bp.cgst)*-1 when bp.plusminus='+' then (bp.cgst) end) as cgstamt, sum(case when bp.plusminus='-' then (bp.sgst)*-1 when bp.plusminus='+' then (bp.sgst) end) as sgstamt, 0 as cess from BillMaster b inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1 where b.isactive=1 and bp.isactive=1 and b.BillType='S' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgst+bp.sgst+bp.igst)>0 union select sum(total-discountamt)*-1 as taxableamt,sum(igdtamt)*-1 as igstamt, sum(cgstamt)*-1 as cgstamt, sum(sgstamt)*-1 as sgstamt, sum(cess)*-1 as cess from BillProductMaster where isactive=1 and BillType='SR' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (cgstamt+sgstamt+igdtamt)>0 union select sum(case when bp.plusminus='-' then (bp.valueofexp) when bp.plusminus='+' then (bp.valueofexp)*-1 end) as taxableamt,sum(case when bp.plusminus='-' then (bp.igst) when bp.plusminus='+' then (bp.igst)*-1 end) as igstamt, sum(case when bp.plusminus='-' then (bp.cgst) when bp.plusminus='+' then (bp.cgst)*-1 end) as cgstamt,sum(case when bp.plusminus='-' then (bp.sgst) when bp.plusminus='+' then (bp.sgst)*-1 end) as sgstamt, 0 as cess from BillMaster b inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1 where b.isactive=1 and bp.isactive=1 and b.BillType='SR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgst+bp.sgst+bp.igst)>0 union select sum(bp.taxableamount) as taxableamt,sum(bp.igstamt) as igstamt, sum(bp.cgstamt) as cgstamt, sum(bp.sgstamt) as sgstamt, 0 as cess from tblgstvoucherproductmaster bp inner join tblgstvouchermaster b on bp.billno=b.billno and (b.party=bp.party or bp.fkid=b.id)where b.isactive=1 and bp.isactive=1 and bp.BillType='S' and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgstamt+bp.sgstamt+bp.igstamt)>0 union select sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as taxableamt,sum(case when bp.plusminus='-' then (bp.igst)*-1 when bp.plusminus='+' then (bp.igst) end) as igstamt, sum(case when bp.plusminus='-' then (bp.cgst)*-1 when bp.plusminus='+' then (bp.cgst) end) as cgstamt,sum(case when bp.plusminus='-' then (bp.sgst)*-1 when bp.plusminus='+' then (bp.sgst) end) as sgstamt, 0 as cess from tblgstvouchermaster b inner join tblgstvoucherchargesmaster bp on b.billno=bp.billno inner join tblgstvoucherproductmaster gp on gp.billno=b.billno and (b.party=gp.party or gp.fkid=b.id)inner join BillSundry bs on bs.BillSundryID=bp.chargeid and bs.OT3=1 and bs.isactive=1 where b.isactive=1 and gp.isactive=1 and bp.isactive=1 and b.BillType='S' and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgst+bp.sgst+bp.igst)>0 union select sum(bp.taxableamount)*-1 as taxableamt,sum(bp.igstamt)*-1 as igstamt, sum(bp.cgstamt)*-1 as cgstamt, sum(bp.sgstamt)*-1 as sgstamt, 0 as cess from tblgstvoucherproductmaster bp inner join tblgstvouchermaster b on bp.billno=b.billno and (b.party=bp.party or bp.fkid=b.id)where b.isactive=1 and bp.isactive=1 and bp.BillType='SR' and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgstamt+bp.sgstamt+bp.igstamt)>0 union select sum(case when bp.plusminus='-' then (bp.valueofexp) when bp.plusminus='+' then (bp.valueofexp)*-1 end) as taxableamt,sum(case when bp.plusminus='-' then (bp.igst) when bp.plusminus='+' then (bp.igst)*-1 end) as igstamt,sum(case when bp.plusminus='-' then (bp.cgst) when bp.plusminus='+' then (bp.cgst)*-1 end) as cgstamt,sum(case when bp.plusminus='-' then (bp.sgst) when bp.plusminus='+' then (bp.sgst)*-1 end) as sgstamt, 0 as cess from tblgstvouchermaster b inner join tblgstvoucherchargesmaster bp on b.billno=bp.billno inner join tblgstvoucherproductmaster gp on gp.billno=b.billno and (b.party=gp.party or gp.fkid=b.id) inner join BillSundry bs on bs.BillSundryID=bp.chargeid and bs.OT3=1 and bs.isactive=1 where b.isactive=1 and gp.isactive=1 and bp.isactive=1 and b.BillType='SR' and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgst+bp.sgst+bp.igst)>0) entries");
                // for (int i = 0; i < invoicedt.Rows.Count; i++)
                //{
                if (invoicedt.Rows.Count > 0)
                {
                    DataRow dr = main1.NewRow();
                    dr["Particulars"] = "Outward taxable supplies other than zero rated,nil rated";
                    if (string.IsNullOrEmpty(invoicedt.Rows[0]["taxableamt"].ToString()))
                    {
                        dr["Taxable Amt."] = "0";
                    }
                    else
                    {
                        dr["Taxable Amt."] = invoicedt.Rows[0]["taxableamt"].ToString();
                    }
                    if (string.IsNullOrEmpty(invoicedt.Rows[0]["igstamt"].ToString()))
                    {
                        dr["IGST"] = "0";
                    }
                    else
                    {
                        dr["IGST"] = invoicedt.Rows[0]["igstamt"].ToString();
                    }
                    if (string.IsNullOrEmpty(invoicedt.Rows[0]["cgstamt"].ToString()))
                    {
                        dr["CGST"] = "0";
                    }
                    else
                    {
                        dr["CGST"] = invoicedt.Rows[0]["cgstamt"].ToString();
                    }
                    if (string.IsNullOrEmpty(invoicedt.Rows[0]["sgstamt"].ToString()))
                    {
                        dr["SGST"] = "0";
                    }
                    else
                    {
                        dr["SGST"] = invoicedt.Rows[0]["sgstamt"].ToString();
                    }
                    if (string.IsNullOrEmpty(invoicedt.Rows[0]["Cess"].ToString()))
                    {
                        dr["Cess"] = "0";
                    }
                    else
                    {
                        dr["Cess"] = invoicedt.Rows[0]["cess"].ToString();
                    }
                    main1.Rows.Add(dr);
                }
                DataTable invoicedt1 = conn.getdataset("select sum(taxableamt) as taxableamt,sum(igstamt) as igstamt,sum(cgstamt) as cgstamt,sum(sgstamt) as sgstamt,sum(cess) as cess from (select sum(total-discountamt) as taxableamt,0 as igstamt, sum(cgst) as cgstamt, sum(sgst) as sgstamt, sum(cess) as cess from BillPOSProductMaster where isactive=1 and  BillRunDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and BillRunDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (cgst+sgst)=0 union select sum(total-discountamt) as taxableamt,sum(igdtamt) as igstamt, sum(cgstamt) as cgstamt, sum(sgstamt) as sgstamt, sum(cess) as cess from BillProductMaster where isactive=1 and BillType='S' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (cgstamt+sgstamt+igdtamt)=0 union select sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as taxableamt,sum(case when bp.plusminus='-' then (bp.igst)*-1 when bp.plusminus='+' then (bp.igst) end) as igstamt, sum(case when bp.plusminus='-' then (bp.cgst)*-1 when bp.plusminus='+' then (bp.cgst) end) as cgstamt, sum(case when bp.plusminus='-' then (bp.sgst)*-1 when bp.plusminus='+' then (bp.sgst) end) as sgstamt, 0 as cess from BillMaster b inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1 where b.isactive=1 and bp.isactive=1 and b.BillType='S' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgst+bp.sgst+bp.igst)=0 union select sum(total-discountamt)*-1 as taxableamt,sum(igdtamt)*-1 as igstamt, sum(cgstamt)*-1 as cgstamt, sum(sgstamt)*-1 as sgstamt, sum(cess)*-1 as cess from BillProductMaster where isactive=1 and BillType='SR' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (cgstamt+sgstamt+igdtamt)=0 union select sum(case when bp.plusminus='-' then (bp.valueofexp) when bp.plusminus='+' then (bp.valueofexp)*-1 end) as taxableamt,sum(case when bp.plusminus='-' then (bp.igst) when bp.plusminus='+' then (bp.igst)*-1 end) as igstamt, sum(case when bp.plusminus='-' then (bp.cgst) when bp.plusminus='+' then (bp.cgst)*-1 end) as cgstamt, sum(case when bp.plusminus='-' then (bp.sgst) when bp.plusminus='+' then (bp.sgst)*-1 end) as sgstamt, 0 as cess from BillMaster b inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1 where b.isactive=1 and bp.isactive=1 and b.BillType='SR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgst+bp.sgst+bp.igst)=0 union select sum(bp.taxableamount) as taxableamt,sum(bp.igstamt) as igstamt, sum(bp.cgstamt) as cgstamt, sum(bp.sgstamt) as sgstamt, 0 as cess from tblgstvoucherproductmaster bp inner join tblgstvouchermaster b on bp.billno=b.billno and (b.party=bp.party or bp.fkid=b.id)where b.isactive=1 and bp.isactive=1 and bp.BillType='S' and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgstamt+bp.sgstamt+bp.igstamt)=0 union select sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as taxableamt,sum(case when bp.plusminus='-' then (bp.igst)*-1 when bp.plusminus='+' then (bp.igst) end) as igstamt, sum(case when bp.plusminus='-' then (bp.cgst)*-1 when bp.plusminus='+' then (bp.cgst) end) as cgstamt, sum(case when bp.plusminus='-' then (bp.sgst)*-1 when bp.plusminus='+' then (bp.sgst) end) as sgstamt, 0 as cess from tblgstvouchermaster b inner join tblgstvoucherchargesmaster bp on b.billno=bp.billno inner join tblgstvoucherproductmaster gp on gp.billno=b.billno and (b.party=gp.party or gp.fkid=b.id)inner join BillSundry bs on bs.BillSundryID=bp.chargeid and bs.OT3=1 and bs.isactive=1 where b.isactive=1 and gp.isactive=1 and bp.isactive=1 and b.BillType='S' and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgst+bp.sgst+bp.igst)=0 union select sum(bp.taxableamount)*-1 as taxableamt,sum(bp.igstamt)*-1 as igstamt, sum(bp.cgstamt)*-1 as cgstamt, sum(bp.sgstamt)*-1 as sgstamt, 0 as cess from tblgstvoucherproductmaster bp inner join tblgstvouchermaster b on bp.billno=b.billno and (b.party=bp.party or bp.fkid=b.id)where b.isactive=1 and bp.isactive=1 and bp.BillType='SR' and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgstamt+bp.sgstamt+bp.igstamt)=0 union select sum(case when bp.plusminus='-' then (bp.valueofexp) when bp.plusminus='+' then (bp.valueofexp)*-1 end) as taxableamt,sum(case when bp.plusminus='-' then (bp.igst) when bp.plusminus='+' then (bp.igst)*-1 end) as igstamt, sum(case when bp.plusminus='-' then (bp.cgst) when bp.plusminus='+' then (bp.cgst)*-1 end) as cgstamt, sum(case when bp.plusminus='-' then (bp.sgst) when bp.plusminus='+' then (bp.sgst)*-1 end) as sgstamt, 0 as cess from tblgstvouchermaster b inner join tblgstvoucherchargesmaster bp on b.billno=bp.billno inner join tblgstvoucherproductmaster gp on gp.billno=b.billno and (b.party=gp.party or gp.fkid=b.id)inner join BillSundry bs on bs.BillSundryID=bp.chargeid and bs.OT3=1 and bs.isactive=1 where b.isactive=1 and gp.isactive=1 and bp.isactive=1 and b.BillType='SR' and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgst+bp.sgst+bp.igst)=0) entries ");
                //    for (int i = 0; i < invoicedt1.Rows.Count; i++)
                //   {
                if (invoicedt1.Rows.Count > 0)
                {
                    DataRow dr = main1.NewRow();
                    dr["Particulars"] = "Outwared taxable supplies(Zero Rated)";
                    //dr["Taxable Amt."] = invoicedt1.Rows[0]["taxableamt"].ToString();
                    //dr["IGST"] = invoicedt1.Rows[0]["igstamt"].ToString();
                    //dr["CGST"] = invoicedt1.Rows[0]["cgstamt"].ToString();
                    //dr["SGST"] = invoicedt1.Rows[0]["sgstamt"].ToString();
                    //dr["Cess"] = invoicedt1.Rows[0]["cess"].ToString();
                    if (string.IsNullOrEmpty(invoicedt1.Rows[0]["taxableamt"].ToString()))
                    {
                        dr["Taxable Amt."] = "0";
                    }
                    else
                    {
                        dr["Taxable Amt."] = invoicedt1.Rows[0]["taxableamt"].ToString();
                    }
                    if (string.IsNullOrEmpty(invoicedt1.Rows[0]["igstamt"].ToString()))
                    {
                        dr["IGST"] = "0";
                    }
                    else
                    {
                        dr["IGST"] = invoicedt1.Rows[0]["igstamt"].ToString();
                    }
                    if (string.IsNullOrEmpty(invoicedt1.Rows[0]["cgstamt"].ToString()))
                    {
                        dr["CGST"] = "0";
                    }
                    else
                    {
                        dr["CGST"] = invoicedt1.Rows[0]["cgstamt"].ToString();
                    }
                    if (string.IsNullOrEmpty(invoicedt1.Rows[0]["sgstamt"].ToString()))
                    {
                        dr["SGST"] = "0";
                    }
                    else
                    {
                        dr["SGST"] = invoicedt1.Rows[0]["sgstamt"].ToString();
                    }
                    if (string.IsNullOrEmpty(invoicedt1.Rows[0]["Cess"].ToString()))
                    {
                        dr["Cess"] = "0";
                    }
                    else
                    {
                        dr["Cess"] = invoicedt1.Rows[0]["cess"].ToString();
                    }
                    main1.Rows.Add(dr);
                }
                DataTable invoicedt2 = conn.getdataset("");
                //   if (invoicedt2.Rows.Count == 0)
                //   {
                DataRow dr1 = main1.NewRow();
                dr1["Particulars"] = "Other outward supplies(Nill Rated)";
                dr1["Taxable Amt."] = "0";
                dr1["IGST"] = "0";
                dr1["CGST"] = "0";
                dr1["SGST"] = "0";
                dr1["Cess"] = "0";
                main1.Rows.Add(dr1);
                //  }
                DataTable invoicedt3 = conn.getdataset("");
                //   if (invoicedt3.Rows.Count == 0)
                //   {
                DataRow dr2 = main1.NewRow();
                dr2["Particulars"] = "Inward supplies Liable to reverse charge";
                dr2["Taxable Amt."] = "0";
                dr2["IGST"] = "0";
                dr2["CGST"] = "0";
                dr2["SGST"] = "0";
                dr2["Cess"] = "0";
                main1.Rows.Add(dr2);
                //    }
                DataTable invoicedt4 = conn.getdataset("");
                //  if (invoicedt4.Rows.Count == 0)
                //  {
                DataRow dr3 = main1.NewRow();
                dr3["Particulars"] = "NoN-GST outward supplies";
                dr3["Taxable Amt."] = "0";
                dr3["IGST"] = "0";
                dr3["CGST"] = "0";
                dr3["SGST"] = "0";
                dr3["Cess"] = "0";
                main1.Rows.Add(dr3);
                //  }
                Double[] tot = new Double[main1.Columns.Count];
                for (int i = 0; i < main1.Rows.Count; i++)
                {
                    for (int j = 1; j < main1.Columns.Count; j++)
                    {
                        if (main1.Rows[i][j].ToString() == "")
                        {
                            tot[j] += Convert.ToDouble("0");
                        }
                        else
                        {
                            tot[j] += Convert.ToDouble(main1.Rows[i][j].ToString());
                        }
                    }
                }
                DataRow lastdr = main1.NewRow();
                lastdr[0] = "";
                for (int i = 1; i < main1.Columns.Count; i++)
                {
                    if (i == 1)
                    {
                        lastdr[i] = tot[i].ToString("N2");
                    }
                    else
                    {
                        lastdr[i] = tot[i].ToString("N2");
                    }
                }
                main1.Rows.Add();
                main1.Rows.Add(lastdr);
                lv1.Items.Clear();
                int ColCount1 = main1.Columns.Count;
                //Add columns
                for (int k = 0; k < ColCount1; k++)
                {
                    if (k == 0)
                    {
                        lv1.Columns.Add(main1.Columns[k].ColumnName, 400);
                    }
                    if (k == 1)
                    {
                        lv1.Columns.Add(main1.Columns[k].ColumnName, 120);
                    }
                    if (k == 2)
                    {
                        lv1.Columns.Add(main1.Columns[k].ColumnName, 120);
                    }
                    if (k == 3)
                    {
                        lv1.Columns.Add(main1.Columns[k].ColumnName, 120);
                    }
                    if (k == 4)
                    {
                        lv1.Columns.Add(main1.Columns[k].ColumnName, 120);
                    }
                    if (k == 5)
                    {
                        lv1.Columns.Add(main1.Columns[k].ColumnName, 120);
                    }

                }
                for (int i = 0; i < main1.Rows.Count; i++)
                {
                    DataRow drow = main1.Rows[i];

                    // Only row that have not been deleted
                    if (drow.RowState != DataRowState.Deleted)
                    {
                        // Define the list items
                        ListViewItem lvi = new ListViewItem(drow[0].ToString());
                        for (int j = 1; j < ColCount1; j++)
                        {
                            lvi.SubItems.Add(drow[j].ToString());
                        }
                        // Add the list items to the ListView
                        lv1.Items.Add(lvi);
                    }
                }
                #endregion
                // LV2
                #region
                main2 = new DataTable();
                main2.Columns.Add("Particulars", typeof(string));
                main2.Columns.Add("Place Of Supply", typeof(string));
                main2.Columns.Add("Taxable Amt.", typeof(string));
                main2.Columns.Add("IGST", typeof(string));
                DataTable invoicedt5 = conn.getdataset("");
                //  if (invoicedt4.Rows.Count == 0)
                //  {
                DataRow dr4 = main2.NewRow();
                dr4["Particulars"] = "Supplies made to Unregistered persons";
                dr4["Place Of Supply"] = "0";
                dr4["Taxable Amt."] = "0";
                dr4["IGST"] = "0";
                main2.Rows.Add(dr4);
                //  }
                DataTable invoicedt6 = conn.getdataset("");
                //  if (invoicedt4.Rows.Count == 0)
                //  {
                DataRow dr5 = main2.NewRow();
                dr5["Particulars"] = "Supplies made to Composition Taxable person";
                dr5["Place Of Supply"] = "0";
                dr5["Taxable Amt."] = "0";
                dr5["IGST"] = "0";
                main2.Rows.Add(dr5);
                //  }
                DataTable invoicedt7 = conn.getdataset("");
                //  if (invoicedt4.Rows.Count == 0)
                //  {
                DataRow dr6 = main2.NewRow();
                dr6["Particulars"] = "Supplies made to UIN Holders";
                dr6["Place Of Supply"] = "0";
                dr6["Taxable Amt."] = "0";
                dr6["IGST"] = "0";
                main2.Rows.Add(dr6);
                //  }
                Double[] tot1 = new Double[main2.Columns.Count];
                for (int i = 0; i < main2.Rows.Count; i++)
                {
                    for (int j = 2; j < main2.Columns.Count; j++)
                    {
                        if (main2.Rows[i][j].ToString() == "")
                        {
                            tot1[j] += Convert.ToDouble("0");
                        }
                        else
                        {
                            tot1[j] += Convert.ToDouble(main2.Rows[i][j].ToString());
                        }
                    }
                }
                DataRow lastdr1 = main2.NewRow();
                lastdr1[0] = "";
                lastdr1[1] = "";
                for (int i = 2; i < main2.Columns.Count; i++)
                {
                    if (i == 2)
                    {
                        lastdr1[i] = tot1[i].ToString("N2");
                    }
                    else
                    {
                        lastdr1[i] = tot1[i].ToString("N2");
                    }
                }
                main2.Rows.Add();
                main2.Rows.Add(lastdr1);
                lv2.Items.Clear();
                int ColCount2 = main2.Columns.Count;
                //Add columns
                for (int k = 0; k < ColCount2; k++)
                {
                    if (k == 0)
                    {
                        lv2.Columns.Add(main2.Columns[k].ColumnName, 400);
                    }
                    if (k == 1)
                    {
                        lv2.Columns.Add(main2.Columns[k].ColumnName, 200);
                    }
                    if (k == 2)
                    {
                        lv2.Columns.Add(main2.Columns[k].ColumnName, 200);
                    }
                    if (k == 3)
                    {
                        lv2.Columns.Add(main2.Columns[k].ColumnName, 200);
                    }
                }
                for (int i = 0; i < main2.Rows.Count; i++)
                {
                    DataRow drow = main2.Rows[i];

                    // Only row that have not been deleted
                    if (drow.RowState != DataRowState.Deleted)
                    {
                        // Define the list items
                        ListViewItem lvi = new ListViewItem(drow[0].ToString());
                        for (int j = 1; j < ColCount2; j++)
                        {
                            lvi.SubItems.Add(drow[j].ToString());
                        }
                        // Add the list items to the ListView
                        lv2.Items.Add(lvi);
                    }
                }
                #endregion
                // LV3
                #region
                main3 = new DataTable();
                main3.Columns.Add("Details", typeof(string));
                main3.Columns.Add("Integrated Tax", typeof(string));
                main3.Columns.Add("Central Tax", typeof(string));
                main3.Columns.Add("State/UT tax", typeof(string));
                main3.Columns.Add("Cess", typeof(string));
                DataRow dr8 = main3.NewRow();
                dr8["Details"] = "(A) ITC Avaliable(Whether in Full or Part)";
                dr8["Integrated Tax"] = "";
                dr8["Central Tax"] = "";
                dr8["State/UT tax"] = "";
                dr8["Cess"] = "";
                main3.Rows.Add(dr8);
                DataRow dr9 = main3.NewRow();
                dr9["Details"] = "      (1) Import of Goods";
                dr9["Integrated Tax"] = "0";
                dr9["Central Tax"] = "0";
                dr9["State/UT tax"] = "0";
                dr9["Cess"] = "0";
                main3.Rows.Add(dr9);
                DataRow dr10 = main3.NewRow();
                dr10["Details"] = "      (2) Import of Services";
                dr10["Integrated Tax"] = "0";
                dr10["Central Tax"] = "0";
                dr10["State/UT tax"] = "0";
                dr10["Cess"] = "0";
                main3.Rows.Add(dr10);
                DataTable invoicedt11 = conn.getdataset("select sum(taxableamt) as taxableamt,sum(igstamt) as igstamt,sum(cgstamt) as cgstamt,sum(sgstamt) as sgstamt,sum(cess) as cess from (select sum(bp.total-bp.discountamt) as taxableamt,sum(bp.igdtamt) as igstamt, sum(bp.cgstamt) as cgstamt, sum(bp.sgstamt) as sgstamt, sum(bp.cess) as cess from BillProductMaster bp inner join BillMaster b on (b.billno=bp.billno) where b.isactive=1 and  bp.isactive=1 and b.SaleType in (select Purchasetypeid from PurchasetypeMaster where FormType='P' and isactive=1 and TaxCalculation='Bill Of Supply') and bp.BillType='P' and bp.Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bp.Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgstamt+bp.sgstamt+bp.igdtamt)>0 union select sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as taxableamt,sum(case when bp.plusminus='-' then (bp.igst)*-1 when bp.plusminus='+' then (bp.igst) end) as igstamt, sum(case when bp.plusminus='-' then (bp.cgst)*-1 when bp.plusminus='+' then (bp.cgst) end) as cgstamt, sum(case when bp.plusminus='-' then (bp.sgst)*-1 when bp.plusminus='+' then (bp.sgst) end) as sgstamt, 0 as cess from BillMaster b inner join billchargesmaster bp on b.billno=bp.billno and b.id=bp.fkid inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1 where b.SaleType in (select Purchasetypeid from PurchasetypeMaster where FormType='P' and isactive=1 and TaxCalculation='Bill Of Supply') and b.isactive=1 and bp.isactive=1 and b.BillType='P' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgst+bp.sgst+bp.igst)>0 union select sum(bp.total-bp.discountamt)*-1 as taxableamt,sum(bp.igdtamt)*-1 as igstamt, sum(bp.cgstamt)*-1 as cgstamt, sum(bp.sgstamt)*-1 as sgstamt, sum(bp.cess)*-1 as cess from BillProductMaster bp inner join BillMaster b on (b.id=bp.fkid or b.billno=b.billno)where  b.isactive=1 and b.SaleType in (select Purchasetypeid from PurchasetypeMaster where FormType='PR' and isactive=1 and TaxCalculation='Bill Of Supply') and bp.isactive=1 and bp.BillType='PR' and bp.Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bp.Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgstamt+bp.sgstamt+bp.igdtamt)>0 union select sum(case when bp.plusminus='-' then (bp.valueofexp) when bp.plusminus='+' then (bp.valueofexp)*-1 end) as taxableamt,sum(case when bp.plusminus='-' then (bp.igst) when bp.plusminus='+' then (bp.igst)*-1 end) as igstamt, sum(case when bp.plusminus='-' then (bp.cgst) when bp.plusminus='+' then (bp.cgst)*-1 end) as cgstamt, sum(case when bp.plusminus='-' then (bp.sgst) when bp.plusminus='+' then (bp.sgst)*-1 end) as sgstamt, 0 as cess from BillMaster b inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1 where b.SaleType in (select Purchasetypeid from PurchasetypeMaster where FormType='PR' and isactive=1 and TaxCalculation='Bill Of Supply') and b.isactive=1 and bp.isactive=1 and b.BillType='PR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgst+bp.sgst+bp.igst)>0 union select sum(bp.taxableamount) as taxableamt,sum(bp.igstamt) as igstamt, sum(bp.cgstamt) as cgstamt, sum(bp.sgstamt) as sgstamt, 0 as cess from tblgstvoucherproductmaster bp inner join tblgstvouchermaster b on bp.billno=b.billno and (b.party=bp.party or bp.fkid=b.id)where b.Type in (select Purchasetypeid from PurchasetypeMaster where FormType='P' and isactive=1 and TaxCalculation='Bill Of Supply') and b.isactive=1 and bp.isactive=1 and (bp.BillType='P'  or b.BillType='EXP') and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgstamt+bp.sgstamt+bp.igstamt)>0 union select sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as taxableamt,sum(case when bp.plusminus='-' then (bp.igst)*-1 when bp.plusminus='+' then (bp.igst) end) as igstamt, sum(case when bp.plusminus='-' then (bp.cgst)*-1 when bp.plusminus='+' then (bp.cgst) end) as cgstamt, sum(case when bp.plusminus='-' then (bp.sgst)*-1 when bp.plusminus='+' then (bp.sgst) end) as sgstamt, 0 as cess from tblgstvouchermaster b inner join tblgstvoucherchargesmaster bp on b.id=bp.fkid and b.billno=bp.billno inner join tblgstvoucherproductmaster gp on gp.billno=b.billno and (b.party=gp.party or gp.fkid=b.id)inner join BillSundry bs on bs.BillSundryID=bp.chargeid and bs.OT3=1 and bs.isactive=1 where b.Type in (select Purchasetypeid from PurchasetypeMaster where FormType='P' and isactive=1 and TaxCalculation='Bill Of Supply') and b.isactive=1 and gp.isactive=1 and bp.isactive=1 and (b.BillType='P' or b.BillType='EXP') and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgst+bp.sgst+bp.igst)>0 union select sum(bp.taxableamount)*-1 as taxableamt,sum(bp.igstamt)*-1 as igstamt, sum(bp.cgstamt)*-1 as cgstamt, sum(bp.sgstamt)*-1 as sgstamt, 0 as cess from tblgstvoucherproductmaster bp inner join tblgstvouchermaster b on bp.billno=b.billno and (b.party=bp.party or bp.fkid=b.id)where b.Type in (select Purchasetypeid from PurchasetypeMaster where FormType='PR' and isactive=1 and TaxCalculation='Bill Of Supply') and b.isactive=1 and bp.isactive=1 and bp.BillType='PR' and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgstamt+bp.sgstamt+bp.igstamt)>0 union select sum(case when bp.plusminus='-' then (bp.valueofexp) when bp.plusminus='+' then (bp.valueofexp)*-1 end) as taxableamt,sum(case when bp.plusminus='-' then (bp.igst) when bp.plusminus='+' then (bp.igst)*-1 end) as igstamt,sum(case when bp.plusminus='-' then (bp.cgst) when bp.plusminus='+' then (bp.cgst)*-1 end) as cgstamt, sum(case when bp.plusminus='-' then (bp.sgst) when bp.plusminus='+' then (bp.sgst)*-1 end) as sgstamt, 0 as cess from tblgstvouchermaster b inner join tblgstvoucherchargesmaster bp on b.id=bp.fkid and b.billno=bp.billno inner join tblgstvoucherproductmaster gp on gp.billno=b.billno and (b.party=gp.party or gp.fkid=b.id)inner join BillSundry bs on bs.BillSundryID=bp.chargeid and bs.OT3=1 and bs.isactive=1 where b.Type in (select Purchasetypeid from PurchasetypeMaster where FormType='PR' and isactive=1 and TaxCalculation='Bill Of Supply') and b.isactive=1 and gp.isactive=1 and bp.isactive=1 and (b.BillType='PR') and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgst+bp.sgst+bp.igst)>0) entries ");
                if (invoicedt11.Rows.Count > 0)
                {
                    DataRow dr11 = main3.NewRow();
                    dr11["Details"] = "      (3) Inward Supplies liable to reverse charge";
                    if (string.IsNullOrEmpty(invoicedt11.Rows[0]["igstamt"].ToString()))
                    {
                        dr11["Integrated Tax"] = "0";
                    }
                    else
                    {
                        dr11["Integrated Tax"] = invoicedt11.Rows[0]["igstamt"].ToString();
                    }
                    if (string.IsNullOrEmpty(invoicedt11.Rows[0]["cgstamt"].ToString()))
                    {
                        dr11["Central Tax"] = "0";
                    }
                    else
                    {
                        dr11["Central Tax"] = invoicedt11.Rows[0]["cgstamt"].ToString();
                    }
                    if (string.IsNullOrEmpty(invoicedt11.Rows[0]["sgstamt"].ToString()))
                    {
                        dr11["State/UT tax"] = "0";
                    }
                    else
                    {
                        dr11["State/UT tax"] = invoicedt11.Rows[0]["sgstamt"].ToString();
                    }
                    if (string.IsNullOrEmpty(invoicedt11.Rows[0]["cess"].ToString()))
                    {
                        dr11["Cess"] = "0";
                    }
                    else
                    {
                        dr11["Cess"] = invoicedt11.Rows[0]["cess"].ToString();
                    }
                    main3.Rows.Add(dr11);
                }
                DataRow dr12 = main3.NewRow();
                dr12["Details"] = "      (4) Inward Supplies from isd";
                dr12["Integrated Tax"] = "0";
                dr12["Central Tax"] = "0";
                dr12["State/UT tax"] = "0";
                dr12["Cess"] = "0";
                main3.Rows.Add(dr12);
              //  DataTable invoicedt13 = conn.getdataset("select sum(taxableamt) as taxableamt,sum(igstamt) as igstamt,sum(cgstamt) as cgstamt,sum(sgstamt) as sgstamt,sum(cess) as cess from (select sum(total-discountamt) as taxableamt,0 as igstamt, sum(cgst) as cgstamt, sum(sgst) as sgstamt, sum(cess) as cess from BillPOSProductMaster where isactive=1 and  BillRunDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and BillRunDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (cgst+sgst)>0 union select sum(total-discountamt) as taxableamt,sum(igdtamt) as igstamt, sum(cgstamt) as cgstamt, sum(sgstamt) as sgstamt, sum(cess) as cess from BillProductMaster where isactive=1 and BillType='P' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (cgstamt+sgstamt+igdtamt)>0 union select sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as taxableamt,sum(case when bp.plusminus='-' then (bp.igst)*-1 when bp.plusminus='+' then (bp.igst) end) as igstamt, sum(case when bp.plusminus='-' then (bp.cgst)*-1 when bp.plusminus='+' then (bp.cgst) end) as cgstamt, sum(case when bp.plusminus='-' then (bp.sgst)*-1 when bp.plusminus='+' then (bp.sgst) end) as sgstamt, 0 as cess from BillMaster b inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1 where b.isactive=1 and bp.isactive=1 and b.BillType='P' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgst+bp.sgst+bp.igst)>0 union select sum(total-discountamt)*-1 as taxableamt,sum(igdtamt)*-1 as igstamt, sum(cgstamt)*-1 as cgstamt, sum(sgstamt)*-1 as sgstamt, sum(cess)*-1 as cess from BillProductMaster where isactive=1 and BillType='PR' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (cgstamt+sgstamt+igdtamt)>0 union select sum(case when bp.plusminus='-' then (bp.valueofexp) when bp.plusminus='+' then (bp.valueofexp)*-1 end) as taxableamt,sum(case when bp.plusminus='-' then (bp.igst) when bp.plusminus='+' then (bp.igst)*-1 end) as igstamt, sum(case when bp.plusminus='-' then (bp.cgst) when bp.plusminus='+' then (bp.cgst)*-1 end) as cgstamt, sum(case when bp.plusminus='-' then (bp.sgst) when bp.plusminus='+' then (bp.sgst)*-1 end) as sgstamt, 0 as cess from BillMaster b inner join billchargesmaster bp on b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1 where b.isactive=1 and bp.isactive=1 and b.BillType='PR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgst+bp.sgst+bp.igst)>0 union select sum(bp.taxableamount) as taxableamt,sum(bp.igstamt) as igstamt, sum(bp.cgstamt) as cgstamt, sum(bp.sgstamt) as sgstamt, 0 as cess from tblgstvoucherproductmaster bp inner join tblgstvouchermaster b on bp.billno=b.billno and (b.party=bp.party or bp.fkid=b.id)where b.isactive=1 and bp.isactive=1 and (bp.BillType='P'  or b.BillType='EXP') and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgstamt+bp.sgstamt+bp.igstamt)>0 union select sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as taxableamt,sum(case when bp.plusminus='-' then (bp.igst)*-1 when bp.plusminus='+' then (bp.igst) end) as igstamt, sum(case when bp.plusminus='-' then (bp.cgst)*-1 when bp.plusminus='+' then (bp.cgst) end) as cgstamt, sum(case when bp.plusminus='-' then (bp.sgst)*-1 when bp.plusminus='+' then (bp.sgst) end) as sgstamt, 0 as cess from tblgstvouchermaster b inner join tblgstvoucherchargesmaster bp on b.billno=bp.billno inner join tblgstvoucherproductmaster gp on gp.billno=b.billno and (b.party=gp.party or gp.fkid=b.id)inner join BillSundry bs on bs.BillSundryID=bp.chargeid and bs.OT3=1 and bs.isactive=1 where b.isactive=1 and gp.isactive=1 and bp.isactive=1 and (b.BillType='P' or b.BillType='EXP') and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgst+bp.sgst+bp.igst)>0 union select sum(bp.taxableamount)*-1 as taxableamt,sum(bp.igstamt)*-1 as igstamt, sum(bp.cgstamt)*-1 as cgstamt, sum(bp.sgstamt)*-1 as sgstamt, 0 as cess from tblgstvoucherproductmaster bp inner join tblgstvouchermaster b on bp.billno=b.billno and (b.party=bp.party or bp.fkid=b.id)where b.isactive=1 and bp.isactive=1 and bp.BillType='PR' and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgstamt+bp.sgstamt+bp.igstamt)>0 union select sum(case when bp.plusminus='-' then (bp.valueofexp) when bp.plusminus='+' then (bp.valueofexp)*-1 end) as taxableamt,sum(case when bp.plusminus='-' then (bp.igst) when bp.plusminus='+' then (bp.igst)*-1 end) as igstamt, sum(case when bp.plusminus='-' then (bp.cgst) when bp.plusminus='+' then (bp.cgst)*-1 end) as cgstamt, sum(case when bp.plusminus='-' then (bp.sgst) when bp.plusminus='+' then (bp.sgst)*-1 end) as sgstamt, 0 as cess from tblgstvouchermaster b inner join tblgstvoucherchargesmaster bp on b.billno=bp.billno inner join tblgstvoucherproductmaster gp on gp.billno=b.billno and (b.party=gp.party or gp.fkid=b.id)inner join BillSundry bs on bs.BillSundryID=bp.chargeid and bs.OT3=1 and bs.isactive=1 where b.isactive=1 and gp.isactive=1 and bp.isactive=1 and (b.BillType='PR') and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgst+bp.sgst+bp.igst)>0) entries ");
                DataTable invoicedt13 = conn.getdataset("select sum(taxableamt) as taxableamt,sum(igstamt) as igstamt,sum(cgstamt) as cgstamt,sum(sgstamt) as sgstamt,sum(cess) as cess from (select sum(bp.total-bp.discountamt) as taxableamt,sum(bp.igdtamt) as igstamt, sum(bp.cgstamt) as cgstamt, sum(bp.sgstamt) as sgstamt, sum(bp.cess) as cess from BillProductMaster bp inner join BillMaster b on (b.billno=bp.billno)where b.isactive=1 and  bp.isactive=1 and b.SaleType in (select Purchasetypeid from PurchasetypeMaster where FormType='P' and isactive=1 and TaxCalculation='Tax Invoice') and bp.BillType='P' and bp.Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bp.Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgstamt+bp.sgstamt+bp.igdtamt)>0 union select sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as taxableamt,sum(case when bp.plusminus='-' then (bp.igst)*-1 when bp.plusminus='+' then (bp.igst) end) as igstamt, sum(case when bp.plusminus='-' then (bp.cgst)*-1 when bp.plusminus='+' then (bp.cgst) end) as cgstamt, sum(case when bp.plusminus='-' then (bp.sgst)*-1 when bp.plusminus='+' then (bp.sgst) end) as sgstamt, 0 as cess from BillMaster b inner join billchargesmaster bp on b.id=bp.fkid and b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1 where b.SaleType in (select Purchasetypeid from PurchasetypeMaster where FormType='P' and isactive=1 and TaxCalculation='Tax Invoice') and b.isactive=1 and bp.isactive=1 and b.BillType='P' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgst+bp.sgst+bp.igst)>0 union select sum(bp.total-bp.discountamt)*-1 as taxableamt,sum(bp.igdtamt)*-1 as igstamt, sum(bp.cgstamt)*-1 as cgstamt, sum(bp.sgstamt)*-1 as sgstamt, sum(bp.cess)*-1 as cess from BillProductMaster bp inner join BillMaster b on (b.id=bp.fkid or b.billno=b.billno)where  b.isactive=1 and b.SaleType in (select Purchasetypeid from PurchasetypeMaster where FormType='PR' and isactive=1 and TaxCalculation='Tax Invoice') and bp.isactive=1 and bp.BillType='PR' and bp.Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bp.Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgstamt+bp.sgstamt+bp.igdtamt)>0 union select sum(case when bp.plusminus='-' then (bp.valueofexp) when bp.plusminus='+' then (bp.valueofexp)*-1 end) as taxableamt,sum(case when bp.plusminus='-' then (bp.igst) when bp.plusminus='+' then (bp.igst)*-1 end) as igstamt, sum(case when bp.plusminus='-' then (bp.cgst) when bp.plusminus='+' then (bp.cgst)*-1 end) as cgstamt, sum(case when bp.plusminus='-' then (bp.sgst) when bp.plusminus='+' then (bp.sgst)*-1 end) as sgstamt, 0 as cess from BillMaster b inner join billchargesmaster bp on b.id=bp.fkid and b.billno=bp.billno inner join BillSundry bs on bs.BillSundryID=bp.billsundryid and bs.OT3=1 and bs.isactive=1 where b.SaleType in (select Purchasetypeid from PurchasetypeMaster where FormType='PR' and isactive=1 and TaxCalculation='Tax Invoice') and b.isactive=1 and bp.isactive=1 and b.BillType='PR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgst+bp.sgst+bp.igst)>0 union select sum(bp.taxableamount) as taxableamt,sum(bp.igstamt) as igstamt, sum(bp.cgstamt) as cgstamt, sum(bp.sgstamt) as sgstamt, 0 as cess from tblgstvoucherproductmaster bp inner join tblgstvouchermaster b on bp.billno=b.billno and (b.party=bp.party or bp.fkid=b.id)where b.Type in (select Purchasetypeid from PurchasetypeMaster where FormType='P' and isactive=1 and TaxCalculation='Tax Invoice') and b.isactive=1 and bp.isactive=1 and (bp.BillType='P'  or b.BillType='EXP') and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgstamt+bp.sgstamt+bp.igstamt)>0 union select sum(case when bp.plusminus='-' then (bp.valueofexp)*-1 when bp.plusminus='+' then (bp.valueofexp) end) as taxableamt,sum(case when bp.plusminus='-' then (bp.igst)*-1 when bp.plusminus='+' then (bp.igst) end) as igstamt, sum(case when bp.plusminus='-' then (bp.cgst)*-1 when bp.plusminus='+' then (bp.cgst) end) as cgstamt, sum(case when bp.plusminus='-' then (bp.sgst)*-1 when bp.plusminus='+' then (bp.sgst) end) as sgstamt, 0 as cess from tblgstvouchermaster b inner join tblgstvoucherchargesmaster bp on b.id=bp.fkid and b.billno=bp.billno inner join tblgstvoucherproductmaster gp on gp.billno=b.billno and (b.party=gp.party or gp.fkid=b.id)inner join BillSundry bs on bs.BillSundryID=bp.chargeid and bs.OT3=1 and bs.isactive=1 where b.Type in (select Purchasetypeid from PurchasetypeMaster where FormType='P' and isactive=1 and TaxCalculation='Tax Invoice') and b.isactive=1 and gp.isactive=1 and bp.isactive=1 and (b.BillType='P' or b.BillType='EXP') and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgst+bp.sgst+bp.igst)>0 union select sum(bp.taxableamount)*-1 as taxableamt,sum(bp.igstamt)*-1 as igstamt, sum(bp.cgstamt)*-1 as cgstamt, sum(bp.sgstamt)*-1 as sgstamt, 0 as cess from tblgstvoucherproductmaster bp inner join tblgstvouchermaster b on bp.billno=b.billno and (b.party=bp.party or bp.fkid=b.id) where b.Type in (select Purchasetypeid from PurchasetypeMaster where FormType='PR' and isactive=1 and TaxCalculation='Tax Invoice') and b.isactive=1 and bp.isactive=1 and bp.BillType='PR' and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgstamt+bp.sgstamt+bp.igstamt)>0 union select sum(case when bp.plusminus='-' then (bp.valueofexp) when bp.plusminus='+' then (bp.valueofexp)*-1 end) as taxableamt,sum(case when bp.plusminus='-' then (bp.igst) when bp.plusminus='+' then (bp.igst)*-1 end) as igstamt,sum(case when bp.plusminus='-' then (bp.cgst) when bp.plusminus='+' then (bp.cgst)*-1 end) as cgstamt, sum(case when bp.plusminus='-' then (bp.sgst) when bp.plusminus='+' then (bp.sgst)*-1 end) as sgstamt, 0 as cess from tblgstvouchermaster b inner join tblgstvoucherchargesmaster bp on b.id=bp.fkid and b.billno=bp.billno inner join tblgstvoucherproductmaster gp on gp.billno=b.billno and (b.party=gp.party or gp.fkid=b.id)inner join BillSundry bs on bs.BillSundryID=bp.chargeid and bs.OT3=1 and bs.isactive=1 where b.Type in (select Purchasetypeid from PurchasetypeMaster where FormType='PR' and isactive=1 and TaxCalculation='Tax Invoice') and b.isactive=1 and gp.isactive=1 and bp.isactive=1 and (b.BillType='PR') and b.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and (bp.cgst+bp.sgst+bp.igst)>0) entries ");
                if (invoicedt13.Rows.Count > 0)
                {
                    DataRow dr13 = main3.NewRow();
                    dr13["Details"] = "      (5) All Other ITC";
                    if (string.IsNullOrEmpty(invoicedt13.Rows[0]["igstamt"].ToString()))
                    {
                        dr13["Integrated Tax"] = "0";
                    }
                    else
                    {
                        dr13["Integrated Tax"] = invoicedt13.Rows[0]["igstamt"].ToString();
                    }
                    if (string.IsNullOrEmpty(invoicedt13.Rows[0]["cgstamt"].ToString()))
                    {
                        dr13["Central Tax"] = "0";
                    }
                    else
                    {
                        dr13["Central Tax"] = invoicedt13.Rows[0]["cgstamt"].ToString();
                    }
                    if (string.IsNullOrEmpty(invoicedt13.Rows[0]["sgstamt"].ToString()))
                    {
                        dr13["State/UT tax"] = "0";
                    }
                    else
                    {
                        dr13["State/UT tax"] = invoicedt13.Rows[0]["sgstamt"].ToString();
                    }
                    if (string.IsNullOrEmpty(invoicedt13.Rows[0]["Cess"].ToString()))
                    {
                        dr13["Cess"] = "0";
                    }
                    else
                    {
                        dr13["Cess"] = invoicedt13.Rows[0]["cess"].ToString();
                    }
                    main3.Rows.Add(dr13);
                }
                DataRow dr14 = main3.NewRow();
                dr14["Details"] = "(B) ITC Reversed";
                dr14["Integrated Tax"] = "";
                dr14["Central Tax"] = "";
                dr14["State/UT tax"] = "";
                dr14["Cess"] = "";
                main3.Rows.Add(dr14);
                DataRow dr15 = main3.NewRow();
                dr15["Details"] = "      (1) As per rules 42 and 43 of cgst rules";
                dr15["Integrated Tax"] = "0";
                dr15["Central Tax"] = "0";
                dr15["State/UT tax"] = "0";
                dr15["Cess"] = "0";
                main3.Rows.Add(dr15);
                DataRow dr16 = main3.NewRow();
                dr16["Details"] = "      (2) Others";
                dr16["Integrated Tax"] = "0";
                dr16["Central Tax"] = "0";
                dr16["State/UT tax"] = "0";
                dr16["Cess"] = "0";
                main3.Rows.Add(dr16);
                Double[] tot11 = new Double[main3.Columns.Count];
                for (int i = 0; i < main3.Rows.Count; i++)
                {
                    for (int j = 1; j < main3.Columns.Count; j++)
                    {
                        if (main3.Rows[i][j].ToString() == "")
                        {
                            tot11[j] += Convert.ToDouble("0");
                        }
                        else
                        {
                            tot11[j] += Convert.ToDouble(main3.Rows[i][j].ToString());
                        }
                    }
                }
                DataRow lastdr11 = main3.NewRow();
                lastdr11[0] = "(C) Net ITC Available (A)-(B)";
                lastdr11[1] = "";
                for (int i = 1; i < main3.Columns.Count; i++)
                {
                    if (i == 1)
                    {
                        lastdr11[i] = tot11[i].ToString("N2");
                    }
                    else
                    {
                        lastdr11[i] = tot11[i].ToString("N2");
                    }
                }
                main3.Rows.Add(lastdr11);
                //DataRow dr17 = main3.NewRow();
                //dr17["Details"] = "(C) Net ITC Available (A)-(B)";
                //dr17["Integrated Tax"] = "0";
                //dr17["Central Tax"] = "0";
                //dr17["State/UT tax"] = "0";
                //dr17["Cess"] = "0";
                //main3.Rows.Add(dr17);
                DataRow dr18 = main3.NewRow();
                dr18["Details"] = "(D) Ineligible ITC";
                dr18["Integrated Tax"] = "";
                dr18["Central Tax"] = "";
                dr18["State/UT tax"] = "";
                dr18["Cess"] = "";
                main3.Rows.Add(dr18);
                DataRow dr19 = main3.NewRow();
                dr19["Details"] = "      (1) As per section 17(5)";
                dr19["Integrated Tax"] = "0";
                dr19["Central Tax"] = "0";
                dr19["State/UT tax"] = "0";
                dr19["Cess"] = "0";
                main3.Rows.Add(dr19);
                DataRow dr20 = main3.NewRow();
                dr20["Details"] = "      (2) Others";
                dr20["Integrated Tax"] = "0";
                dr20["Central Tax"] = "0";
                dr20["State/UT tax"] = "0";
                dr20["Cess"] = "0";
                main3.Rows.Add(dr20);
                lv3.Items.Clear();
                int ColCount3 = main3.Columns.Count;
                //Add columns
                for (int k = 0; k < ColCount3; k++)
                {
                    if (k == 0)
                    {
                        lv3.Columns.Add(main3.Columns[k].ColumnName, 400);
                    }
                    if (k == 1)
                    {
                        lv3.Columns.Add(main3.Columns[k].ColumnName, 120);
                    }
                    if (k == 2)
                    {
                        lv3.Columns.Add(main3.Columns[k].ColumnName, 120);
                    }
                    if (k == 3)
                    {
                        lv3.Columns.Add(main3.Columns[k].ColumnName, 120);
                    }
                }
                for (int i = 0; i < main3.Rows.Count; i++)
                {
                    DataRow drow = main3.Rows[i];

                    // Only row that have not been deleted
                    if (drow.RowState != DataRowState.Deleted)
                    {
                        // Define the list items
                        ListViewItem lvi = new ListViewItem(drow[0].ToString());
                        for (int j = 1; j < ColCount3; j++)
                        {
                            lvi.SubItems.Add(drow[j].ToString());
                        }
                        // Add the list items to the ListView
                        lv3.Items.Add(lvi);
                    }
                }
                #endregion
                // LV4
                #region
                main4 = new DataTable();
                main4.Columns.Add("Details", typeof(string));
                main4.Columns.Add("Interstate Supplies", typeof(string));
                main4.Columns.Add("Intrastate Supplies", typeof(string));
                DataTable invoicedt8 = conn.getdataset("");
                //  if (invoicedt4.Rows.Count == 0)
                //  {
                DataRow dr7 = main4.NewRow();
                dr7["Details"] = "From a Supplier Under Composition Scheme,Exempt or nil rated";
                dr7["Interstate Supplies"] = "0";
                dr7["Intrastate Supplies"] = "0";
                main4.Rows.Add(dr7);
                //  }
                lv4.Items.Clear();
                int ColCount4 = main4.Columns.Count;
                //Add columns
                for (int k = 0; k < ColCount4; k++)
                {
                    if (k == 0)
                    {
                        lv4.Columns.Add(main4.Columns[k].ColumnName, 500);
                    }
                    if (k == 1)
                    {
                        lv4.Columns.Add(main4.Columns[k].ColumnName, 200);
                    }
                    if (k == 2)
                    {
                        lv4.Columns.Add(main4.Columns[k].ColumnName, 200);
                    }
                }
                for (int i = 0; i < main4.Rows.Count; i++)
                {
                    DataRow drow = main4.Rows[i];

                    // Only row that have not been deleted
                    if (drow.RowState != DataRowState.Deleted)
                    {
                        // Define the list items
                        ListViewItem lvi = new ListViewItem(drow[0].ToString());
                        for (int j = 1; j < ColCount4; j++)
                        {
                            lvi.SubItems.Add(drow[j].ToString());
                        }
                        // Add the list items to the ListView
                        lv4.Items.Add(lvi);
                    }
                }
                #endregion

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
            try
            {
                binddata();
            }
            catch
            {
            }
        }

        private void GSTR_3B_Load(object sender, EventArgs e)
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

        private void btnexcel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Print ?", "Print", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    prn.execute("delete from printing");
                    string year = DTPFrom.Value.Year.ToString();
                    string month = DTPFrom.Value.Month.ToString();
                    DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1 and CompanyID='" + Master.companyId + "'");
                    string lv1c0 = "", lv1c1 = "", lv1c2 = "", lv1c3 = "", lv1c4 = "", lv1c5 = "", lv2c0 = "", lv2c1 = "", lv2c2 = "", lv2c3 = "", lv3c0 = "", lv3c1 = "", lv3c2 = "", lv3c3 = "", lv3c4 = "", lv4c0 = "", lv4c1 = "", lv4c2 = "";
                    for (int i = 0; i < lv1.Items.Count; i++)
                    {
                        lv1c0 += lv1.Items[i].SubItems[0].Text + Environment.NewLine;
                        lv1c1 += lv1.Items[i].SubItems[1].Text + Environment.NewLine;
                        lv1c2 += lv1.Items[i].SubItems[2].Text + Environment.NewLine;
                        lv1c3 += lv1.Items[i].SubItems[3].Text + Environment.NewLine;
                        lv1c4 += lv1.Items[i].SubItems[4].Text + Environment.NewLine;
                        lv1c5 += lv1.Items[i].SubItems[5].Text + Environment.NewLine;
                    }
                    for (int i = 0; i < lv2.Items.Count; i++)
                    {
                        lv2c0 += lv2.Items[i].SubItems[0].Text + Environment.NewLine;
                        lv2c1 += lv2.Items[i].SubItems[1].Text + Environment.NewLine;
                        lv2c2 += lv2.Items[i].SubItems[2].Text + Environment.NewLine;
                        lv2c3 += lv2.Items[i].SubItems[3].Text + Environment.NewLine;
                    }
                    for (int i = 0; i < lv3.Items.Count; i++)
                    {
                        lv3c0 += lv3.Items[i].SubItems[0].Text + Environment.NewLine;
                        lv3c1 += lv3.Items[i].SubItems[1].Text + Environment.NewLine;
                        lv3c2 += lv3.Items[i].SubItems[2].Text + Environment.NewLine;
                        lv3c3 += lv3.Items[i].SubItems[3].Text + Environment.NewLine;
                        lv3c4 += lv3.Items[i].SubItems[4].Text + Environment.NewLine;
                    }
                    for (int i = 0; i < lv4.Items.Count; i++)
                    {
                        lv4c0 += lv4.Items[i].SubItems[0].Text + Environment.NewLine;
                        lv4c1 += lv4.Items[i].SubItems[1].Text + Environment.NewLine;
                        lv4c2 += lv4.Items[i].SubItems[2].Text + Environment.NewLine;
                    }
                    string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,P81,P82,P83,P84,P85,P86,P87,P88,P89,P90,P91,P92,P93,P94,P95,P96,P97,P98)VALUES";
                    qry += "('" + dt1.Rows[0][0].ToString() + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][2].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + dt1.Rows[0][11].ToString() + "','" + dt1.Rows[0][12].ToString() + "','" + dt1.Rows[0][13].ToString() + "','" + month + "','" + year + "','" + lv1c0 + "','" + lv1c1 + "','" + lv1c2 + "','" + lv1c3 + "','" + lv1c4 + "','" + lv1c5 + "','" + lv2c0 + "','" + lv2c1 + "','" + lv2c2 + "','" + lv2c3 + "','" + lv3c0 + "','" + lv3c1 + "','" + lv3c2 + "','" + lv3c3 + "','" + lv3c4 + "','" + lv4c0 + "','" + lv4c1 + "','" + lv4c2 + "')";
                    prn.execute(qry);
                }
                Print popup = new Print("GSTR-3B");
                popup.ShowDialog();
                popup.Dispose();
            }
            catch
            {
            }
        }
    }
}
