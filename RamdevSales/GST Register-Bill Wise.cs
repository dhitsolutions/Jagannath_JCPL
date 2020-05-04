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
using DocumentFormat.OpenXml.Spreadsheet;

namespace RamdevSales
{
    public partial class GST_Register_Bill_Wise : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        SqlCommand cmd;
        SqlDataAdapter sda;
        OleDbSettings ods = new OleDbSettings();
        static Int32 bill;
        DataTable dt = new DataTable();
        DataTable main = new DataTable();
        Connection conn = new Connection();
        static double totalnet,zeroper,taxableamt,taxamt,totalamt;
        private Master master;
        private TabControl tabControl;

        public GST_Register_Bill_Wise()
        {
            InitializeComponent();
        }

        public GST_Register_Bill_Wise(Master master, TabControl tabControl)
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
        private void GST_Register_Bill_Wise_Load(object sender, EventArgs e)
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

        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            try
            {
                #region
                //LVclient.Columns.Add("Bill No", 70, HorizontalAlignment.Center);
                //LVclient.Columns.Add("Date", 100, HorizontalAlignment.Center);
                //LVclient.Columns.Add("Party", 300, HorizontalAlignment.Left);
                //LVclient.Columns.Add("GST No", 70, HorizontalAlignment.Center);
                //LVclient.Columns.Add("0 %", 100, HorizontalAlignment.Center);
                //LVclient.Columns.Add("5 % Amt", 100, HorizontalAlignment.Center);
                //LVclient.Columns.Add("Igst 5%", 100, HorizontalAlignment.Center);
                //LVclient.Columns.Add("Cgst 2.5%", 100, HorizontalAlignment.Center);
                //LVclient.Columns.Add("Sgst 2.5%", 100, HorizontalAlignment.Center);
                //LVclient.Columns.Add("A.Tax 0%", 100, HorizontalAlignment.Center);
                //LVclient.Columns.Add("12 % Amt", 100, HorizontalAlignment.Center);
                //LVclient.Columns.Add("Igst 12%", 100, HorizontalAlignment.Center);
                //LVclient.Columns.Add("Cgst 6%", 100, HorizontalAlignment.Center);
                //LVclient.Columns.Add("Sgst 6%", 100, HorizontalAlignment.Center);
                //LVclient.Columns.Add("A.Tax 0%", 100, HorizontalAlignment.Center);
                //LVclient.Columns.Add("18 % Amt", 100, HorizontalAlignment.Center);
                //LVclient.Columns.Add("Igst 18%", 100, HorizontalAlignment.Center);
                //LVclient.Columns.Add("Cgst 9%", 100, HorizontalAlignment.Center);
                //LVclient.Columns.Add("Sgst 9%", 100, HorizontalAlignment.Center);
                //LVclient.Columns.Add("A.Tax 0%", 100, HorizontalAlignment.Center);
                //LVclient.Columns.Add("28 % Amt", 100, HorizontalAlignment.Center);
                //LVclient.Columns.Add("Igst 28%", 100, HorizontalAlignment.Center);
                //LVclient.Columns.Add("Cgst 14%", 100, HorizontalAlignment.Center);
                //LVclient.Columns.Add("Sgst 14%", 100, HorizontalAlignment.Center);
                //LVclient.Columns.Add("A.Tax 0%", 100, HorizontalAlignment.Center);
                //LVclient.Columns.Add("Taxable Amt", 100, HorizontalAlignment.Center);
                //LVclient.Columns.Add("Tax Amt", 100, HorizontalAlignment.Center);
                //LVclient.Columns.Add("Totat Amt", 120, HorizontalAlignment.Center);
                #endregion
                bindgrid();
            }
            catch
            {
            }
        }
        public void bindgrid()
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                totalnet = 0;
                zeroper = 0;
                taxableamt = 0;
                taxamt = 0;
                totalamt = 0;
                LVclient.Items.Clear();
                main = new DataTable();
                main.Columns.Add("Bill NO", typeof(string));
                main.Columns.Add("Date", typeof(string));
                main.Columns.Add("Party Name", typeof(string));
                main.Columns.Add("GST No.", typeof(string));
                main.Columns.Add("Bill Amt", typeof(string));
                main.Columns.Add("0%", typeof(string));
                DataTable invoicedt = conn.getdataset("select b.billno, b.bill_date,c.accountname,c.gstno,b.totalnet,b.totalbasic,b.totaltax from billmaster b inner join clientmaster c on c.clientid=b.clientid where b.isactive=1 and b.billtype='S' and c.isactive=1 and b.bill_date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.bill_date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by b.id");
            //    DataTable items = conn.getdataset("select distinct sgstper,cgstper,igstper,addtaxper from billproductmaster where isactive=1 and billtype='S' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by sgstper");
                DataTable items = conn.getdataset("select distinct convert(decimal(18,2), case when igstper>sgstper+cgstper then round(igstper/2,2) else sgstper end) as sgstper,convert(decimal(18,2),case when igstper>sgstper+cgstper then igstper/2 else cgstper end) as cgstper,convert(decimal(18,2),(case when igstper>sgstper+cgstper then round(igstper/2,2) else sgstper end )+(case when igstper>sgstper+cgstper then igstper/2 else cgstper end)) as igstper,addtaxper from billproductmaster where isactive=1 and billtype='S' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by sgstper");
                string taxes = "", addtax = "";
                for (int i = 0; i < items.Rows.Count; i++)
                {
                    double tax = 0;
                    if (Convert.ToDouble(items.Rows[i]["igstper"].ToString()) > 0)
                    {
                        tax = Convert.ToDouble(items.Rows[i]["igstper"].ToString());
                    }
                    else
                    {
                        tax = Convert.ToDouble(items.Rows[i]["sgstper"].ToString()) + Convert.ToDouble(items.Rows[i]["cgstper"].ToString()) + Convert.ToDouble(items.Rows[i]["igstper"].ToString());
                    }
                    //if (Convert.ToDouble(items.Rows[i]["igstper"].ToString()) > (Convert.ToDouble(items.Rows[i]["sgstper"].ToString()) + Convert.ToDouble(items.Rows[i]["cgstper"].ToString())))
                    //{
                    //    items.Rows[i]["cgstper"] = Math.Round(Convert.ToDouble(items.Rows[i]["igstper"].ToString()), 2) / 2;
                    //    items.Rows[i]["sgstper"] = Math.Round(Convert.ToDouble(items.Rows[i]["igstper"].ToString()), 2) / 2;
                    //    items.AcceptChanges();
                    //}
                    taxes += tax.ToString("N2") + ",";
                    DataColumnCollection columns = main.Columns;
                    if (!columns.Contains(tax.ToString("N2") + "% Amt."))
                    {
                        main.Columns.Add(tax.ToString("N2") + "% Amt.", typeof(string));
                    }
                    if (!columns.Contains("IGST " + tax.ToString("N2") + "%"))
                    {
                        main.Columns.Add("IGST " + tax.ToString("N2") + "%", typeof(string));
                    }
                    
                    if (!columns.Contains("CGST " + items.Rows[i]["cgstper"].ToString() + "%"))
                    {
                        main.Columns.Add("CGST " + items.Rows[i]["cgstper"].ToString() + "%", typeof(string));
                    }
                    if (!columns.Contains("SGST " + items.Rows[i]["sgstper"].ToString() + "%"))
                    {
                        main.Columns.Add("SGST " + items.Rows[i]["sgstper"].ToString() + "%", typeof(string));
                    }
                    
                    string p = tax.ToString("N2") + "% A.Tax " + items.Rows[i]["addtaxper"].ToString() + "%";
                    addtax += p + ",";
                    if (!columns.Contains(p))
                    {
                        main.Columns.Add(p, typeof(string));
                    }
                  
                }
                DataTable charge = conn.getdataset("select distinct tax,additax,addtaxamt from Billchargesmaster where isactive=1 and billtype='S'");
                string[] tx = taxes.Split(',');
                string[] atx = addtax.Split(',');
                for (int i = 0; i < charge.Rows.Count; i++)
                {
                    int flag = 0;
                    for (int j = 0; j < tx.Length - 1; j++)
                    {
                        if (Convert.ToDouble(charge.Rows[i]["tax"].ToString()) == Convert.ToDouble(tx[j]))
                        {
                            flag = 1;
                        }
                    }
                    if (flag == 0 && Convert.ToDouble(charge.Rows[i]["tax"].ToString()) > 0)
                    {
                        main.Columns.Add(charge.Rows[i]["tax"].ToString() + "% Amt.", typeof(string));
                        main.Columns.Add("IGST " + charge.Rows[i]["tax"].ToString() + "%", typeof(string));
                        main.Columns.Add("CGST " + (Convert.ToDouble(charge.Rows[i]["tax"].ToString()) / 2).ToString("N2") + "%", typeof(string));
                        main.Columns.Add("SGST " + (Convert.ToDouble(charge.Rows[i]["tax"].ToString()) / 2).ToString("N2") + "%", typeof(string));
                        //string t=charge.Rows[i]["tax"].ToString() + "% A.Tax " + charge.Rows[i]["additax"].ToString() + "%";
                        //main.Columns.Add(t, typeof(string));
                    }
                    flag = 0;
                    for (int j = 0; j < atx.Length - 1; j++)
                    {
                        if (charge.Rows[i]["tax"].ToString() + "% A.Tax " + charge.Rows[i]["additax"].ToString() + "%" == atx[j])
                        {
                            flag = 1;
                        }
                    }
                    if (flag == 0 && Convert.ToDouble(charge.Rows[i]["tax"].ToString()) > 0)
                    {
                        string t = charge.Rows[i]["tax"].ToString() + "% A.Tax " + charge.Rows[i]["additax"].ToString() + "%";
                        main.Columns.Add(t, typeof(string));
                    }

                }

                main.Columns.Add("Taxable Amt", typeof(string));
                main.Columns.Add("Tax Amt", typeof(string));
                main.Columns.Add("Total Amt", typeof(string));

                for (int i = 0; i < invoicedt.Rows.Count; i++)
                {
                    DataRow dr = main.NewRow();
                    string d = Convert.ToDateTime(invoicedt.Rows[i]["bill_date"]).ToString(Master.dateformate);
                    dr["Bill NO"] = invoicedt.Rows[i]["billno"].ToString();
                    dr["Date"] = d;
                    dr["Party Name"] = invoicedt.Rows[i]["accountname"].ToString();
                    dr["GST No."] = invoicedt.Rows[i]["gstno"].ToString();
                    dr["Bill Amt"] = invoicedt.Rows[i]["totalnet"].ToString();
                    totalnet = totalnet + Convert.ToDouble(invoicedt.Rows[i]["totalnet"].ToString());
                    dr["0%"] = "";
                    double taxable = 0, tax = 0, total = 0;
                    DataTable taxdetails = conn.getdataset("select sum(total) as basic,sgstper,sum(sgstamt) as sgstamt,cgstper,sum(cgstamt) as cgstamt,igstper,sum(igdtamt) as igstamt,addtaxper,sum(addtax) as addtax,sum(discountamt) as disamt from billproductmaster where billno='" + invoicedt.Rows[i]["billno"].ToString() + "' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and isactive=1 and billtype='S'  group by sgstper,cgstper,igstper,addtaxper order by sgstper");
                    for (int j = 0; j < taxdetails.Rows.Count; j++)
                    {
                        string taxx = Math.Round(Convert.ToDouble(taxdetails.Rows[j]["sgstper"].ToString()) + Convert.ToDouble(taxdetails.Rows[j]["cgstper"].ToString()) + Convert.ToDouble(taxdetails.Rows[j]["igstper"].ToString()), 2).ToString("N2");


                        if (Convert.ToDouble(taxdetails.Rows[j]["sgstper"].ToString()) == 0 && Convert.ToDouble(taxdetails.Rows[j]["cgstper"].ToString()) == 0 && Convert.ToDouble(taxdetails.Rows[j]["igstper"].ToString()) == 0)
                        {
                            dr["0%"] = Math.Round((Convert.ToDouble(taxdetails.Rows[j]["basic"].ToString())- Convert.ToDouble(taxdetails.Rows[j]["disamt"].ToString())), 2).ToString("N2");
                            taxable += Convert.ToDouble(dr["0%"]);
                            zeroper += Convert.ToDouble(dr["0%"].ToString());
                           
                        }
                        else
                        {
                            dr[taxx + "% Amt."] = Math.Round(Convert.ToDouble(taxdetails.Rows[j]["basic"].ToString()) - Convert.ToDouble(taxdetails.Rows[j]["disamt"].ToString()), 2).ToString("N2");
                            taxable += Convert.ToDouble(dr[taxx + "% Amt."]);
                            dr["IGST " + taxx + "%"] = Math.Round(Convert.ToDouble(taxdetails.Rows[j]["igstamt"].ToString()), 2).ToString("N2");
                            tax += Convert.ToDouble(dr["IGST " + taxx + "%"]);
                            dr["CGST " + Math.Round(Convert.ToDouble(taxx) / 2, 2).ToString("N2") + "%"] = Math.Round(Convert.ToDouble(taxdetails.Rows[j]["cgstamt"].ToString()), 2).ToString("N2");
                            tax += Convert.ToDouble(dr["CGST " + Math.Round(Convert.ToDouble(taxx) / 2, 2).ToString("N2") + "%"]);
                            dr["SGST " + Math.Round(Convert.ToDouble(taxx) / 2, 2).ToString("N2") + "%"] = Math.Round(Convert.ToDouble(taxdetails.Rows[j]["sgstamt"].ToString()), 2).ToString("N2");
                            tax += Convert.ToDouble(dr["SGST " + Math.Round(Convert.ToDouble(taxx) / 2, 2).ToString("N2") + "%"]);
                            dr[taxx + "% A.Tax " + taxdetails.Rows[j]["addtaxper"].ToString() + "%"] = Math.Round(Convert.ToDouble(taxdetails.Rows[j]["addtax"].ToString()), 2).ToString("N2");
                            tax += Convert.ToDouble(dr[taxx + "% A.Tax " + taxdetails.Rows[j]["addtaxper"].ToString() + "%"]);
                        }
                    }
                    DataTable charges = conn.getdataset("select tax,plusminus,addtaxamt, ISNULL(sum(valueofexp),0.00) as value, ISNULL(sum(sgst),0.00) as sgst, ISNULL(sum(cgst),0.00) as cgst,ISNULL(sum(igst),0.00) as igst, ISNULL(sum(additax),0.00) as addtax from Billchargesmaster where billno='" + invoicedt.Rows[i]["billno"].ToString() + "'  and isactive=1 and billtype='S' GROUP BY tax,plusminus,addtaxamt");
                    for (int j = 0; j < charges.Rows.Count; j++)
                    {
                        if (j == 0)
                        {
                            taxable = 0; tax = 0;
                        }
                        string taxx = charges.Rows[j]["tax"].ToString();
                        if (Convert.ToDouble(taxx) == 0)
                        {

                            //  dr["0%"] = Math.Round(Convert.ToDouble(dr["0"]) + Convert.ToDouble(charges.Rows[0]["value"].ToString()), 2).ToString("N2");

                        }
                        else
                        {
                            try
                            {
                                string str = Convert.ToDouble(dr[taxx + "% Amt."]).ToString();
                            }
                            catch
                            {
                                dr[taxx + "% Amt."] = 0;
                            }
                            try
                            {
                                string str = Convert.ToDouble(dr["IGST " + taxx + "%"]).ToString();
                            }
                            catch
                            {
                                dr["IGST " + taxx + "%"] = 0;
                            }
                            try
                            {
                                string str = Convert.ToDouble(dr["CGST " + Math.Round(Convert.ToDouble(taxx) / 2, 2).ToString("N2") + "%"]).ToString();
                            }
                            catch
                            {
                                dr["CGST " + Math.Round(Convert.ToDouble(taxx) / 2, 2).ToString("N2") + "%"] = 0;
                            }
                            try
                            {
                                string str = Convert.ToDouble(dr["SGST " + Math.Round(Convert.ToDouble(taxx) / 2, 2).ToString("N2") + "%"]).ToString();
                            }
                            catch
                            {
                                dr["SGST " + Math.Round(Convert.ToDouble(taxx) / 2, 2).ToString("N2") + "%"] = 0;
                            }
                            try
                            {
                                string str = Convert.ToDouble(dr[taxx + "% A.Tax " + charges.Rows[j]["addtax"].ToString() + "%"]).ToString();
                            }
                            catch
                            {
                                dr[taxx + "% A.Tax " + charges.Rows[j]["addtax"].ToString() + "%"] = 0;
                            }
                            
                            if (charges.Rows[j]["plusminus"].ToString() == "+")
                            {
                                dr[taxx + "% Amt."] = Math.Round(Convert.ToDouble(dr[taxx + "% Amt."]) + Convert.ToDouble(charges.Rows[j]["value"].ToString()), 2).ToString("N2");
                            }
                            else
                            {
                                dr[taxx + "% Amt."] = Math.Round(Convert.ToDouble(dr[taxx + "% Amt."]) - Convert.ToDouble(charges.Rows[j]["value"].ToString()), 2).ToString("N2");
                            }
                            taxable += Convert.ToDouble(dr[taxx + "% Amt."]);

                            if (charges.Rows[j]["plusminus"].ToString() == "+")
                            {
                                dr["IGST " + taxx + "%"] = Math.Round(Convert.ToDouble(dr["IGST " + taxx + "%"]) + Convert.ToDouble(charges.Rows[j]["igst"].ToString()), 2).ToString("N2");
                            }
                            else
                            {
                                dr["IGST " + taxx + "%"] = Math.Round(Convert.ToDouble(dr["IGST " + taxx + "%"]) - Convert.ToDouble(charges.Rows[j]["igst"].ToString()), 2).ToString("N2");
                            }
                            tax += Convert.ToDouble(dr["IGST " + taxx + "%"]);

                            if (charges.Rows[j]["plusminus"].ToString() == "+")
                            {
                                dr["CGST " + Math.Round(Convert.ToDouble(taxx) / 2, 2).ToString("N2") + "%"] = Math.Round(Convert.ToDouble(dr["CGST " + Math.Round(Convert.ToDouble(taxx) / 2, 2).ToString("N2") + "%"]) + Convert.ToDouble(charges.Rows[j]["cgst"].ToString()), 2).ToString("N2");
                            }
                            else
                            {
                                dr["CGST " + Math.Round(Convert.ToDouble(taxx) / 2, 2).ToString("N2") + "%"] = Math.Round(Convert.ToDouble(dr["CGST " + Math.Round(Convert.ToDouble(taxx) / 2, 2).ToString("N2") + "%"]) - Convert.ToDouble(charges.Rows[j]["cgst"].ToString()), 2).ToString("N2");
                            }
                            tax += Convert.ToDouble(dr["CGST " + Math.Round(Convert.ToDouble(taxx) / 2, 2).ToString("N2") + "%"]);

                            if (charges.Rows[j]["plusminus"].ToString() == "+")
                            {
                                dr["SGST " + Math.Round(Convert.ToDouble(taxx) / 2, 2).ToString("N2") + "%"] = Math.Round(Convert.ToDouble(dr["SGST " + Math.Round(Convert.ToDouble(taxx) / 2, 2).ToString("N2") + "%"]) + Convert.ToDouble(charges.Rows[j]["sgst"].ToString()), 2).ToString("N2");
                            }
                            else
                            {
                                dr["SGST " + Math.Round(Convert.ToDouble(taxx) / 2, 2).ToString("N2") + "%"] = Math.Round(Convert.ToDouble(dr["SGST " + Math.Round(Convert.ToDouble(taxx) / 2, 2).ToString("N2") + "%"]) - Convert.ToDouble(charges.Rows[j]["sgst"].ToString()), 2).ToString("N2");
                            }
                            tax += Convert.ToDouble(dr["SGST " + Math.Round(Convert.ToDouble(taxx) / 2, 2).ToString("N2") + "%"]);

                            if (charges.Rows[j]["plusminus"].ToString() == "+")
                            {
                                dr[taxx + "% A.Tax " + charges.Rows[j]["addtax"].ToString() + "%"] = Math.Round(Convert.ToDouble(dr[taxx + "% A.Tax " + charges.Rows[j]["addtax"].ToString() + "%"]) + Convert.ToDouble(charges.Rows[j]["addtaxamt"].ToString()), 2).ToString("N2");
                            }
                            else
                            {
                                dr[taxx + "% A.Tax " + charges.Rows[j]["addtax"].ToString() + "%"] = Math.Round(Convert.ToDouble(dr[taxx + "% A.Tax " + charges.Rows[j]["addtax"].ToString() + "%"]) - Convert.ToDouble(charges.Rows[j]["addtaxamt"].ToString()), 2).ToString("N2");
                            }
                            tax += Convert.ToDouble(dr[taxx + "% A.Tax " + charges.Rows[j]["addtax"].ToString() + "%"]);
                        }
                    }

                    dr["Taxable Amt"] = taxable.ToString("N2");
                    dr["Tax Amt"] = tax.ToString("N2");
                    dr["Total Amt"] = (taxable + tax).ToString("N2");

                   


                    main.Rows.Add(dr);

                }
                Double[] tot=new Double[main.Columns.Count];
                for (int i = 0; i < main.Rows.Count; i++)
                {
                    for (int j = 6; j < main.Columns.Count; j++)
                    {
                        if (main.Rows[i][j].ToString() == "")
                        {
                            tot[j] += Convert.ToDouble("0");
                        }
                        else
                        {
                            tot[j] += Convert.ToDouble(main.Rows[i][j].ToString());
                        }
                    }
                }
                DataRow lastdr = main.NewRow();
                lastdr[0] = "";
                lastdr[1] = "";
                lastdr[2] = "";
                lastdr[3] = "";
                lastdr[4] = totalnet.ToString("N2");
                lastdr[5] = zeroper.ToString("N2");
                
                for (int i = 6; i < main.Columns.Count; i++)
                {
                    if (i == 6)
                    {
                        lastdr[i] = tot[i].ToString("N2");
                    }
                    else
                    {
                        lastdr[i] = tot[i].ToString("N2");
                    }
                }
                main.Rows.Add();
                main.Rows.Add(lastdr);
                bill = 0;
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

            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
        }


      
        private void btnexcel_Click(object sender, EventArgs e)
        {
            try
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        string[] files = Directory.GetFiles(fbd.SelectedPath);
                        string folderPath = fbd.SelectedPath +"\\";
                        String DateTimeName = DateTime.Now.ToString("dd_MMM_yyyy hh_mm_ss");
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(main, "GST Register");
                            // wb.Worksheets.Add(dt1, "ItemPrice");
                            wb.SaveAs(folderPath + "GST Register(BillWise)" + DateTimeName + ".xlsx");
                        }
                        DialogResult dr = MessageBox.Show("Do you want to Open Document?", "GST Register(BillWise)", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(folderPath + "GST Register(BillWise)" + DateTimeName + ".xlsx");
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

        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }

        private void BtnViewReport_MouseEnter(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = false;
            btnclose.BackColor = System.Drawing.Color.FromArgb(94,191,174);
            btnclose.ForeColor = System.Drawing.Color.White;
        }

        private void BtnViewReport_MouseLeave(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = true;
            btnclose.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnclose.ForeColor = System.Drawing.Color.White;
        }

        private void btnexcel_MouseEnter(object sender, EventArgs e)
        {
            btnexcel.UseVisualStyleBackColor = false;
            btnexcel.BackColor = System.Drawing.Color.FromArgb(206,204,254);
            btnexcel.ForeColor = System.Drawing.Color.White;
        }

        private void btnexcel_MouseLeave(object sender, EventArgs e)
        {
            btnexcel.UseVisualStyleBackColor = true;
            btnexcel.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnexcel.ForeColor = System.Drawing.Color.White;
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LVclient_SelectedIndexChanged(object sender, EventArgs e)
        {

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
    }
}
