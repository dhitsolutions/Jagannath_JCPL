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
    public partial class purchaseregisterdetailed : Form
    {
        private Master master;
        private TabControl tabControl;
        Connection conn = new Connection();
        OleDbSettings ods = new OleDbSettings();
        DataTable main = new DataTable();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        public purchaseregisterdetailed()
        {
            InitializeComponent();
        }

        public purchaseregisterdetailed(Master master, TabControl tabControl)
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
        DataTable userrights = new DataTable();
        private void saleregisterdetailed_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
                DTPFrom.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                DTPFrom.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                DTPTo.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                DTPTo.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                DTPFrom.Value = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[3]["p"].ToString() == "False")
                    {
                        btnprint.Enabled = false;
                    }
                }
                
                //lvsalereg.Columns.Add("Date", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Bill No", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Party", 120, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Item", 120, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Company", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Group", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Salt", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Batch", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Qty", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Alt Qty", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Free", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Price", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Basic Amt", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Dis(%)", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Dis Amt", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Tax(%)", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Tax Amt", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Cess Amt", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Subtotal", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Other", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Net Amt", 100, HorizontalAlignment.Left);
                DTPFrom.CustomFormat = Master.dateformate;
                DTPTo.CustomFormat = Master.dateformate;
                this.ActiveControl = DTPFrom;
                bindaccountdropdown();
                binditemdropdown();
            }
            catch
            {
            }
        }
        private void bindaccountdropdown()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='ClientMaster' and (column_name like '%AccountName%' or column_name like '%PrintName%' or column_name like '%Groupname%' or column_name like '%Address%' or column_name like '%City%' or column_name like '%State%')", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                // dt = sql.getdataset("select * from psm");
                DataRow dr;
                dr = dt.NewRow();
                dt.Rows.InsertAt(dr, 0);
                dr["column_name"] = "--Select--";
                if (dt.Rows.Count != 0)
                {
                    // cmbname.DataSource = dt.DefaultView;
                    // cmbname.ValueMember = "sp_id";
                    // cmbname.DisplayMember = "p_name";
                    // btnclr.Enabled = true;
                    // cmbname.SelectedIndex = -1;
                    drpaccount.DataSource = dt;
                    drpaccount.DisplayMember = "column_name";
                    drpaccount.ValueMember = "ClientID";
                }
            }
            catch
            {
            }
        }

        private void binditemdropdown()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("select column_name from INFORMATION_SCHEMA.COLUMNS where (TABLE_NAME='ProductMaster' or TABLE_NAME='Companymaster') and (column_name like '%ProductID%' or column_name like '%Product_Name%' or column_name like '%GroupName%' or column_name like '%Packing%' or column_name like '%HSN_Sac_Code%' or column_name like '%itemnumber%' or column_name like '%companyname%' )", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                // dt = sql.getdataset("select * from psm");
                DataRow dr;
                dr = dt.NewRow();
                dt.Rows.InsertAt(dr, 0);
                dr["column_name"] = "--Select Column Name--";
                if (dt.Rows.Count != 0)
                {
                    // cmbname.DataSource = dt.DefaultView;
                    // cmbname.ValueMember = "sp_id";
                    // cmbname.DisplayMember = "p_name";
                    // btnclr.Enabled = true;
                    // cmbname.SelectedIndex = -1;
                    drpitems.DataSource = dt;
                    drpitems.DisplayMember = "column_name";
                    drpitems.ValueMember = "ClientID";
                }

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

        private void btnprint_Enter(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = false;
            btnprint.BackColor = Color.FromArgb(176, 111, 193);
            btnprint.ForeColor = Color.White;
        }

        private void btnprint_Leave(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = true;
            btnprint.BackColor = Color.FromArgb(51, 153, 255);
            btnprint.ForeColor = Color.White;
        }

        private void btnprint_MouseEnter(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = false;
            btnprint.BackColor = Color.FromArgb(176, 111, 193);
            btnprint.ForeColor = Color.White;
        }

        private void btnprint_MouseLeave(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = true;
            btnprint.BackColor = Color.FromArgb(51, 153, 255);
            btnprint.ForeColor = Color.White;
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
        public void binddata()
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                main = new DataTable();
                main.Columns.Add("billnocontinus", typeof(string));
                main.Columns.Add("Date", typeof(string));
                main.Columns.Add("Bill No", typeof(string));
                main.Columns.Add("Party", typeof(string));
                main.Columns.Add("Item", typeof(string));
                main.Columns.Add("Company", typeof(string));
                main.Columns.Add("Group", typeof(string));
                main.Columns.Add("Batch", typeof(string));
                main.Columns.Add("Qty", typeof(string));
                main.Columns.Add("Alt Qty", typeof(string));
                main.Columns.Add("Free", typeof(string));
                main.Columns.Add("Price", typeof(string));
                main.Columns.Add("Basic Amt", typeof(string));
                main.Columns.Add("Dis(%)", typeof(string));
                main.Columns.Add("Dis Amt", typeof(string));
                main.Columns.Add("Tax(%)", typeof(string));
                main.Columns.Add("Tax Amt", typeof(string));
                main.Columns.Add("Add Tax(%)", typeof(string));
                main.Columns.Add("Add Tax Amt", typeof(string));
                main.Columns.Add("Cess Amt", typeof(string));
                main.Columns.Add("Subtotal", typeof(string));
                main.Columns.Add("Other", typeof(string));
                main.Columns.Add("Round Off", typeof(string));
                main.Columns.Add("Net Amt", typeof(string));
                main.Columns.Add("Clientid", typeof(string));

                //  DataTable dt1 = conn.getdataset("select b.Bill_Date,b.billno,b.ClientID,bp.Productname,bp.batch,bp.qty,bp.Aqty,bp.free,bp.Rate,bp.Total,bp.discountper,bp.discountamt,sum(bp.cgstper + bp.sgstper + bp.igstper) as taxper,bp.Tax,bp.addtaxper,bp.addtax,bp.Amount,sum(bc.amount) as others,b.totalnet  from BillMaster b inner join BillProductMaster bp on b.billno=bp.billno inner join Billchargesmaster bc on bp.billno=bc.billno where b.isactive=1 and bp.isactive=1 and bc.isactive=1 group by b.Bill_Date,b.billno,b.ClientID,bp.Productname,bp.batch,bp.qty,bp.Aqty,bp.free,bp.Rate,bp.Total,bp.discountper,bp.discountamt,bp.Tax,bp.addtaxper,bp.addtax,bp.Amount,b.totalnet");
                DataTable dt1 = conn.getdataset("select * from BillMaster where isactive=1 and BillType='P' and Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Bill_Date asc");
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        DataTable dt = conn.getdataset("select b.Bill_Date,b.billno,b.ClientID,bp.Productname,bp.batch,sum(bp.qty) as qty,sum(cast (bp.Aqty as float)) as aqty,sum(bp.free) as free,bp.Rate,sum(bp.Total) as total,bp.discountper,sum(bp.discountamt) as discountamt,sum(bp.cgstper + bp.sgstper + bp.igstper) as taxper,sum(bp.Tax) as tax,bp.addtaxper,sum(bp.addtax) as addtax,sum(bp.cess) as cess,sum(bp.Amount) as Amount,b.totalnet,bp.Productid,b.roudoff  from BillMaster b inner join BillProductMaster bp on b.billno=bp.billno where b.isactive=1 and bp.isactive=1  and b.BillType='P' and bp.BillType='P' and b.ClientID='" + dt1.Rows[i]["ClientID"].ToString() + "' and bp.ClientID='" + dt1.Rows[i]["ClientID"].ToString() + "' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and b.billno='" + dt1.Rows[i]["billno"].ToString() + "' group by b.Bill_Date,b.billno,b.ClientID,bp.Productname,bp.batch,bp.Rate,bp.discountper,bp.addtaxper,bp.Productid,b.roudoff,b.totalnet");
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            DataTable productid = conn.getdataset("select CompanyID,GroupName from ProductMaster where ProductID='" + dt.Rows[j]["Productid"].ToString() + "'");
                            string companyname = conn.ExecuteScalar("select companyname from CompanyMaster where CompanyID='" + productid.Rows[0]["CompanyID"].ToString() + "'");
                            string party = conn.ExecuteScalar("select AccountName from ClientMaster where isactive=1 and ClientID='" + dt.Rows[j]["ClientID"].ToString() + "'");
                            string other = conn.ExecuteScalar("select sum(amount) as others from Billchargesmaster where isactive=1 and and BillType='P' and billno='" + dt1.Rows[i]["billno"].ToString() + "'");
                            if (string.IsNullOrEmpty(other))
                            {
                                other = "0";
                            }
                            if (j == 0)
                            {
                                DataRow dr = main.NewRow();
                                string d = Convert.ToDateTime(dt.Rows[j]["Bill_Date"]).ToString(Master.dateformate);
                                dr["billnocontinus"] = dt.Rows[j]["billno"].ToString();
                                dr["Date"] = d;
                                dr["Bill No"] = dt.Rows[j]["billno"].ToString();
                                dr["Party"] = party;
                                dr["Item"] = dt.Rows[j]["Productname"].ToString();
                                dr["Company"] = companyname;
                                dr["Group"] = productid.Rows[0]["GroupName"].ToString();
                                dr["Batch"] = dt.Rows[j]["batch"].ToString();
                                dr["Qty"] = dt.Rows[j]["qty"].ToString();
                                dr["Alt Qty"] = dt.Rows[j]["aqty"].ToString();
                                dr["Free"] = dt.Rows[j]["free"].ToString();
                                dr["Price"] = dt.Rows[j]["rate"].ToString();
                                dr["Basic Amt"] = dt.Rows[j]["Total"].ToString();
                                dr["Dis(%)"] = dt.Rows[j]["discountper"].ToString();
                                dr["Dis Amt"] = dt.Rows[j]["discountamt"].ToString();
                                dr["Tax(%)"] = dt.Rows[j]["taxper"].ToString();
                                dr["Tax Amt"] = dt.Rows[j]["Tax"].ToString();
                                dr["Add Tax(%)"] = dt.Rows[j]["addtaxper"].ToString();
                                dr["Add Tax Amt"] = dt.Rows[j]["addtax"].ToString();
                                dr["Cess Amt"] = dt.Rows[j]["cess"].ToString();
                                dr["Subtotal"] = dt.Rows[j]["Amount"].ToString();
                                dr["Other"] = other;
                                dr["Round Off"] = dt.Rows[j]["roudoff"].ToString();
                                //dr["Net Amt"] = dt.Rows[j]["totalnet"].ToString();
                               // Double roundoff = Convert.ToDouble(dt.Rows[j]["roudoff"].ToString()) * -1;
                               // Double nettotal = Convert.ToDouble(dt.Rows[j]["totalnet"].ToString()) + roundoff;
                                dr["Net Amt"] = dt.Rows[j]["totalnet"].ToString();
                                dr["Clientid"] = dt.Rows[j]["ClientID"].ToString();
                                main.Rows.Add(dr);
                            }
                            else
                            {
                                DataRow dr = main.NewRow();
                                dr["billnocontinus"] = dt.Rows[j]["billno"].ToString();
                                dr["Date"] = "";
                                dr["Bill No"] = "";
                                dr["Party"] = party;
                                dr["Item"] = dt.Rows[j]["Productname"].ToString();
                                dr["Company"] = companyname;
                                dr["Group"] = productid.Rows[0]["GroupName"].ToString();
                                dr["Batch"] = dt.Rows[j]["batch"].ToString();
                                dr["Qty"] = dt.Rows[j]["qty"].ToString();
                                dr["Alt Qty"] = dt.Rows[j]["aqty"].ToString();
                                dr["Free"] = dt.Rows[j]["free"].ToString();
                                dr["Price"] = dt.Rows[j]["rate"].ToString();
                                dr["Basic Amt"] = dt.Rows[j]["Total"].ToString();
                                dr["Dis(%)"] = dt.Rows[j]["discountper"].ToString();
                                dr["Dis Amt"] = dt.Rows[j]["discountamt"].ToString();
                                dr["Tax(%)"] = dt.Rows[j]["taxper"].ToString();
                                dr["Tax Amt"] = dt.Rows[j]["Tax"].ToString();
                                dr["Add Tax(%)"] = dt.Rows[j]["addtaxper"].ToString();
                                dr["Add Tax Amt"] = dt.Rows[j]["addtax"].ToString();
                                dr["Cess Amt"] = dt.Rows[j]["cess"].ToString();
                                dr["Subtotal"] = dt.Rows[j]["Amount"].ToString();
                                dr["Other"] = "";
                                dr["Round Off"] = "";
                                dr["Net Amt"] = "";
                                dr["Clientid"] = dt.Rows[j]["ClientID"].ToString();
                                main.Rows.Add(dr);
                            }
                        }

                    }
                    Double[] tot = new Double[main.Columns.Count];
                    for (int i = 0; i < main.Rows.Count; i++)
                    {
                        for (int j = 8; j < main.Columns.Count; j++)
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
                    lastdr[4] = "";
                    lastdr[5] = "";
                    lastdr[6] = "";
                    lastdr[7] = "";
                    for (int i = 8; i < main.Columns.Count-1; i++)
                    {
                        if (i == 8)
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
                    lvsalereg.Columns.Clear();
                    lvsalereg.Items.Clear();

                    int ColCount = main.Columns.Count;
                    //Add columns
                    for (int k = 0; k < ColCount; k++)
                    {
                        if (k == 0)
                        {
                            lvsalereg.Columns.Add(main.Columns[k].ColumnName, 0);
                        }
                        else
                        {
                            if (k == 24)
                            {
                                lvsalereg.Columns.Add(main.Columns[k].ColumnName, 0);
                            }
                            else
                            {
                                lvsalereg.Columns.Add(main.Columns[k].ColumnName, 120);
                            }
                        }
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
                            lvsalereg.Items.Add(lvi);
                        }
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

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvsalereg.Items.Count > 0)
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
                                wb.Worksheets.Add(main, "PURCHASE Register");
                                // wb.Worksheets.Add(dt1, "ItemPrice");
                                wb.SaveAs(folderPath + "PURCHASE Register(BillWise)" + DateTimeName + ".xlsx");
                            }
                            DialogResult dr = MessageBox.Show("Do you want to Open Document?", "PURCHASE Register(BillWise)", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dr == DialogResult.Yes)
                            {
                                System.Diagnostics.Process.Start(folderPath + "PURCHASE Register(BillWise)" + DateTimeName + ".xlsx");
                                String pathToExecutable = "AcroRd32.exe";
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No Records For Export Data..");
                }
                
                // MessageBox.Show("Export Data Sucessfully");
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Error: " + ex.Message);
            }
        }
        public void open()
        {
            try
            {
                string[] strfinalarray = new string[5] { "P", "C", "Purchase", "P", "" };
                this.Enabled = false;
                String str = lvsalereg.Items[lvsalereg.FocusedItem.Index].SubItems[0].Text;
                int clientid = Convert.ToInt32(lvsalereg.Items[lvsalereg.FocusedItem.Index].SubItems[24].Text);
                DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                DefaultSale bd = new DefaultSale(this, master, tabControl, strfinalarray);
                //  Sale p = new Sale(this, master, tabControl);
                if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                {
                    bd.updatemode(str, lvsalereg.Items[lvsalereg.FocusedItem.Index].SubItems[0].Text, clientid, strfinalarray);
                    master.AddNewTab(bd);
                }
                //else if (dt1.Rows[0]["formname"].ToString() == p.Text)
                //{
                //    p.updatemode(str, LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[0].Text, 1);
                //    master.AddNewTab(p);
                //}
            }
            catch
            {
            }
            finally
            {
                this.Enabled = true;
            }
        }
        private void lvsalereg_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[3]["v"].ToString() == "True" || userrights.Rows[3]["u"].ToString() == "True")
                {
                    open();
                }
                else
                {
                    MessageBox.Show("You don't have Permission To View");
                    return;
                }
            }
        }

        private void lvsalereg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[3]["v"].ToString() == "True" || userrights.Rows[3]["u"].ToString() == "True")
                    {
                        open();
                    }
                    else
                    {
                        MessageBox.Show("You don't have Permission To View");
                        return;
                    }
                }
            }
        }

        private void btnCalculator_Click(object sender, EventArgs e)
        {
            try
            {
                Printing prndata = new Printing();
                if (lvsalereg.Items.Count > 0)
                {
                    prndata.execute("delete from printing");
                    DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1");
                    int j = 1;
                    for (int i = 0; i < lvsalereg.Items.Count; i++)
                    {

                        string date = "", Billno = "", PartyName = "", Itemname = "", Company = "", Group = "", batch = "", Qty = "", AltQty = "", Free = "", Price = "", BasicAmount = "", DisPer = "", DisAmount = "", TaxPer = "", TaxAmount = "", AdditionalTaxPer = "", AddTaxAmount = "", CessAmount = "", SubTotal = "", Other = "", NetAmount = "", FromDateToDate = "";
                        date = lvsalereg.Items[i].SubItems[1].Text;
                        Billno = lvsalereg.Items[i].SubItems[2].Text;
                        PartyName = lvsalereg.Items[i].SubItems[3].Text;
                        Itemname = lvsalereg.Items[i].SubItems[4].Text;
                        Company = lvsalereg.Items[i].SubItems[5].Text;
                        Group = lvsalereg.Items[i].SubItems[6].Text;
                        batch = lvsalereg.Items[i].SubItems[7].Text;
                        Qty = lvsalereg.Items[i].SubItems[8].Text;
                        AltQty = lvsalereg.Items[i].SubItems[9].Text;
                        Free = lvsalereg.Items[i].SubItems[10].Text;
                        Price = lvsalereg.Items[i].SubItems[11].Text;
                        BasicAmount = lvsalereg.Items[i].SubItems[12].Text;
                        DisPer = lvsalereg.Items[i].SubItems[13].Text;
                        DisAmount = lvsalereg.Items[i].SubItems[14].Text;
                        TaxPer = lvsalereg.Items[i].SubItems[15].Text;
                        TaxAmount = lvsalereg.Items[i].SubItems[16].Text;
                        AdditionalTaxPer = lvsalereg.Items[i].SubItems[17].Text;
                        AddTaxAmount = lvsalereg.Items[i].SubItems[18].Text;
                        CessAmount = lvsalereg.Items[i].SubItems[19].Text;
                        SubTotal = lvsalereg.Items[i].SubItems[20].Text;
                        Other = lvsalereg.Items[i].SubItems[21].Text;
                        NetAmount = lvsalereg.Items[i].SubItems[22].Text;
                        FromDateToDate = "PURCHASE REGISTER FROM " + DTPFrom.Text + " TO " + DTPTo.Text;
                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34)VALUES";
                        qry += "('" + date + "','" + Billno + "','" + PartyName + "','" + Itemname + "','" + Company + "','" + Group + "','" + batch + "','" + Qty + "','" + AltQty + "','" + Free + "','" + Price + "','" + BasicAmount + "','" + DisPer + "','" + DisAmount + "','" + TaxPer + "','" + TaxAmount + "','" + AdditionalTaxPer + "','" + AddTaxAmount + "','" + CessAmount + "','" + SubTotal + "','" + Other + "','" + NetAmount + "','" + dt1.Rows[0]["SubName"].ToString() + "','" + dt1.Rows[0]["Address"].ToString() + "','" + dt1.Rows[0]["Address2"].ToString() + "','" + dt1.Rows[0]["City"].ToString() + "','" + dt1.Rows[0]["State"].ToString() + "','" + dt1.Rows[0]["Country"].ToString() + "','" + dt1.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Mobile"].ToString() + "','" + dt1.Rows[0]["Email"].ToString() + "','" + dt1.Rows[0]["Website"].ToString() + "','" + FromDateToDate + "','" + dt1.Rows[0]["CSTNO"].ToString() + "')";
                        prndata.execute(qry);
                    }
                    Print popup = new Print("PurchaseRegister");
                    popup.ShowDialog();
                    popup.Dispose();
                }
                else
                {
                    MessageBox.Show("No Records For Printing..");
                }
            }
            catch (Exception excp)
            {

            }
        }
        private void bindsearch()
        {
            // if (txtaccount.Text != "" || txtitems.Text != "")
            //   {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            con.Open();
            main = new DataTable();
            main.Columns.Add("billnocontinus", typeof(string));
            main.Columns.Add("Date", typeof(string));
            main.Columns.Add("Bill No", typeof(string));
            main.Columns.Add("Party", typeof(string));
            main.Columns.Add("Item", typeof(string));
            main.Columns.Add("Company", typeof(string));
            main.Columns.Add("Group", typeof(string));
            main.Columns.Add("Batch", typeof(string));
            main.Columns.Add("Qty", typeof(string));
            main.Columns.Add("Alt Qty", typeof(string));
            main.Columns.Add("Free", typeof(string));
            main.Columns.Add("Price", typeof(string));
            main.Columns.Add("Basic Amt", typeof(string));
            main.Columns.Add("Dis(%)", typeof(string));
            main.Columns.Add("Dis Amt", typeof(string));
            main.Columns.Add("Tax(%)", typeof(string));
            main.Columns.Add("Tax Amt", typeof(string));
            main.Columns.Add("Add Tax(%)", typeof(string));
            main.Columns.Add("Add Tax Amt", typeof(string));
            main.Columns.Add("Cess Amt", typeof(string));
            main.Columns.Add("Subtotal", typeof(string));
            main.Columns.Add("Other", typeof(string));
            main.Columns.Add("Round Off", typeof(string));
            main.Columns.Add("Net Amt", typeof(string));
            DataTable dt1 = new DataTable();
            if (drpaccount.Text == "AccountName" || drpaccount.Text == "PrintName" || drpaccount.Text == "GroupName" || drpaccount.Text == "Address" || drpaccount.Text == "City" || drpaccount.Text == "State" || drpaccount.Text == "statecode")
            {
                //clientid = conn.getsinglevalue("select clientid from clientmaster where isactive=1 and (AccountName like '%" + txtaccount.Text + "%' and PrintName like '%" + txtaccount.Text + "%' and GroupName like '%" + txtaccount.Text + "%' and Address like '%" + txtaccount.Text + "%' and City like '%" + txtaccount.Text + "%'and State like '%" + txtaccount.Text + "%' and statecode like '%" + txtaccount.Text + "%')");
                dt1 = conn.getdataset("select * from BillMaster where clientid in (select clientid from clientmaster where isactive=1 and (" + drpaccount.Text + " like '%" + txtaccount.Text + "%')) and isactive=1 and BillType='P' and Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Bill_Date");
            }
            else
            {
                dt1 = conn.getdataset("select * from BillMaster where clientid like '%" + txtaccount.Text + "%' and isactive=1 and BillType='P' and Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'");
            }
            //  DataTable dt1 = conn.getdataset("select b.Bill_Date,b.billno,b.ClientID,bp.Productname,bp.batch,bp.qty,bp.Aqty,bp.free,bp.Rate,bp.Total,bp.discountper,bp.discountamt,sum(bp.cgstper + bp.sgstper + bp.igstper) as taxper,bp.Tax,bp.addtaxper,bp.addtax,bp.Amount,sum(bc.amount) as others,b.totalnet  from BillMaster b inner join BillProductMaster bp on b.billno=bp.billno inner join Billchargesmaster bc on bp.billno=bc.billno where b.isactive=1 and bp.isactive=1 and bc.isactive=1 group by b.Bill_Date,b.billno,b.ClientID,bp.Productname,bp.batch,bp.qty,bp.Aqty,bp.free,bp.Rate,bp.Total,bp.discountper,bp.discountamt,bp.Tax,bp.addtaxper,bp.addtax,bp.Amount,b.totalnet");
          //  lvsalereg.Columns.Clear();
            lvsalereg.Items.Clear();
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    DataTable dt = new DataTable();
                    if (drpitems.Text == "GroupName" || drpitems.Text == "Packing" || drpitems.Text == "Hsn_Sac_Code" || drpitems.Text == "itemnumber" || drpitems.Text == "ProductID")
                    {
                        //itemname = conn.getsinglevalue("select product_name from productmaster where isactive=1 and (GroupName like '%" + txtitems.Text + "%' and Packing like '%" + txtitems.Text + "%' and Hsn_Sac_Code like '%" + txtitems.Text + "%' and itemnumber like '%" + txtitems.Text + "%')");
                        //items = conn.getdataset("select distinct Productname as ItemName from SaleOrderProductMaster where productname in (select product_name from productmaster where isactive=1 and (GroupName like '%" + txtitems.Text + "%' or Packing like '%" + txtitems.Text + "%' or Hsn_Sac_Code like '%" + txtitems.Text + "%' or ProductID like '%" + txtitems.Text + "%' or itemnumber like '%" + txtitems.Text + "%')) and isactive=1 and Billtype='SO' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Productname");
                        dt = conn.getdataset("select b.Bill_Date,b.billno,b.ClientID,bp.Productname,bp.batch,sum(bp.qty) as qty,sum(cast (bp.Aqty as float)) as aqty,sum(bp.free) as free,bp.Rate,sum(bp.Total) as total,bp.discountper,sum(bp.discountamt) as discountamt,sum(bp.cgstper + bp.sgstper + bp.igstper) as taxper,sum(bp.Tax) as tax,bp.addtaxper,sum(bp.addtax) as addtax,sum(bp.cess) as cess,sum(bp.Amount) as Amount,b.totalnet,bp.Productid,b.roudoff  from BillMaster b inner join BillProductMaster bp on b.billno=bp.billno where b.isactive=1 and bp.isactive=1  and b.BillType='P' and bp.productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and bp.BillType='P' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and b.billno='" + dt1.Rows[i]["billno"].ToString() + "' group by b.Bill_Date,b.billno,b.ClientID,bp.Productname,bp.batch,bp.Rate,bp.discountper,bp.addtaxper,bp.Productid,b.roudoff,b.totalnet");
                    }
                    else if (drpitems.Text == "companyname")
                    {
                        //itemname = conn.getsinglevalue("select product_name from productmaster where isactive=1 and companyid =(select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))");
                        //items = conn.getdataset("select distinct Productname as ItemName from SaleOrderProductMaster where productname in (select product_name from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and isactive=1 and Billtype='SO' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Productname");
                        dt = conn.getdataset("select b.Bill_Date,b.billno,b.ClientID,bp.Productname,bp.batch,sum(bp.qty) as qty,sum(cast (bp.Aqty as float)) as aqty,sum(bp.free) as free,bp.Rate,sum(bp.Total) as total,bp.discountper,sum(bp.discountamt) as discountamt,sum(bp.cgstper + bp.sgstper + bp.igstper) as taxper,sum(bp.Tax) as tax,bp.addtaxper,sum(bp.addtax) as addtax,sum(bp.cess) as cess,sum(bp.Amount) as Amount,b.totalnet,bp.Productid,b.roudoff  from BillMaster b inner join BillProductMaster bp on b.billno=bp.billno where b.isactive=1 and bp.isactive=1 and bp.productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and b.BillType='P' and bp.BillType='P' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and b.billno='" + dt1.Rows[i]["billno"].ToString() + "' group by b.Bill_Date,b.billno,b.ClientID,bp.Productname,bp.batch,bp.Rate,bp.discountper,bp.addtaxper,bp.Productid,b.roudoff,b.totalnet");
                    }
                    else
                    {
                        dt = conn.getdataset("select b.Bill_Date,b.billno,b.ClientID,bp.Productname,bp.batch,sum(bp.qty) as qty,sum(cast (bp.Aqty as float)) as aqty,sum(bp.free) as free,bp.Rate,sum(bp.Total) as total,bp.discountper,sum(bp.discountamt) as discountamt,sum(bp.cgstper + bp.sgstper + bp.igstper) as taxper,sum(bp.Tax) as tax,bp.addtaxper,sum(bp.addtax) as addtax,sum(bp.cess) as cess,sum(bp.Amount) as Amount,b.totalnet,bp.Productid,b.roudoff  from BillMaster b inner join BillProductMaster bp on b.billno=bp.billno where b.isactive=1 and bp.isactive=1  and b.BillType='P' and bp.BillType='P' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and b.billno='" + dt1.Rows[i]["billno"].ToString() + "' group by b.Bill_Date,b.billno,b.ClientID,bp.Productname,bp.batch,bp.Rate,bp.discountper,bp.addtaxper,bp.Productid,b.roudoff,b.totalnet");
                    }

                    //  DataTable dt = conn.getdataset("select b.Bill_Date,b.billno,b.ClientID,bp.Productname,bp.batch,bp.qty,bp.Aqty,bp.free,bp.Rate,bp.Total,bp.discountper,bp.discountamt,sum(bp.cgstper + bp.sgstper + bp.igstper) as taxper,bp.Tax,bp.addtaxper,bp.addtax,bp.cess,bp.Amount,b.totalnet,bp.Productid  from BillMaster b inner join BillProductMaster bp on b.billno=bp.billno where b.isactive=1 and bp.isactive=1  and b.BillType='S' and bp.BillType='S' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and b.billno='" + dt1.Rows[i]["billno"].ToString() + "' group by b.Bill_Date,b.billno,b.ClientID,bp.Productname,bp.batch,bp.qty,bp.Aqty,bp.free,bp.Rate,bp.Total,bp.discountper,bp.discountamt,bp.Tax,bp.addtaxper,bp.addtax,bp.cess,bp.Amount,b.totalnet,bp.Productid");
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        DataTable productid = conn.getdataset("select CompanyID,GroupName from ProductMaster where isactive=1 and ProductID='" + dt.Rows[j]["Productid"].ToString() + "'");
                        string companyname = conn.ExecuteScalar("select companyname from CompanyMaster where CompanyID='" + productid.Rows[0]["CompanyID"].ToString() + "'");
                        string party = conn.ExecuteScalar("select AccountName from ClientMaster where isactive=1 and ClientID='" + dt.Rows[j]["ClientID"].ToString() + "'");
                        string other = conn.ExecuteScalar("select sum(amount) as others from Billchargesmaster where isactive=1 and BillType='P' and billno='" + dt1.Rows[i]["billno"].ToString() + "'");
                        if (string.IsNullOrEmpty(other))
                        {
                            other = "0";
                        }
                        if (j == 0)
                        {
                            DataRow dr = main.NewRow();
                            string d = Convert.ToDateTime(dt.Rows[j]["Bill_Date"]).ToString(Master.dateformate);
                            dr["billnocontinus"] = dt.Rows[j]["billno"].ToString();
                            dr["Date"] = d;
                            dr["Bill No"] = dt.Rows[j]["billno"].ToString();
                            dr["Party"] = party;
                            dr["Item"] = dt.Rows[j]["Productname"].ToString();
                            dr["Company"] = companyname;
                            dr["Group"] = productid.Rows[0]["GroupName"].ToString();
                            dr["Batch"] = dt.Rows[j]["batch"].ToString();
                            dr["Qty"] = dt.Rows[j]["qty"].ToString();
                            dr["Alt Qty"] = dt.Rows[j]["aqty"].ToString();
                            dr["Free"] = dt.Rows[j]["free"].ToString();
                            dr["Price"] = dt.Rows[j]["rate"].ToString();
                            dr["Basic Amt"] = dt.Rows[j]["Total"].ToString();
                            dr["Dis(%)"] = dt.Rows[j]["discountper"].ToString();
                            dr["Dis Amt"] = dt.Rows[j]["discountamt"].ToString();
                            dr["Tax(%)"] = dt.Rows[j]["taxper"].ToString();
                            dr["Tax Amt"] = dt.Rows[j]["Tax"].ToString();
                            dr["Add Tax(%)"] = dt.Rows[j]["addtaxper"].ToString();
                            dr["Add Tax Amt"] = dt.Rows[j]["addtax"].ToString();
                            dr["Cess Amt"] = dt.Rows[j]["cess"].ToString();
                            dr["Subtotal"] = dt.Rows[j]["Amount"].ToString();
                            dr["Other"] = other;
                            dr["Round Off"] = dt.Rows[j]["roudoff"].ToString();
                            //dr["Net Amt"] = dt.Rows[j]["totalnet"].ToString();
                            //Double roundoff = Convert.ToDouble(dt.Rows[j]["roudoff"].ToString()) * -1;
                            //Double nettotal = Convert.ToDouble(dt.Rows[j]["totalnet"].ToString()) + roundoff;
                            dr["Net Amt"] = dt.Rows[j]["totalnet"].ToString();

                            main.Rows.Add(dr);
                        }
                        else
                        {
                            DataRow dr = main.NewRow();
                            dr["billnocontinus"] = dt.Rows[j]["billno"].ToString();
                            dr["Date"] = "";
                            dr["Bill No"] = "";
                            dr["Party"] = party;
                            dr["Item"] = dt.Rows[j]["Productname"].ToString();
                            dr["Company"] = companyname;
                            dr["Group"] = productid.Rows[0]["GroupName"].ToString();
                            dr["Batch"] = dt.Rows[j]["batch"].ToString();
                            dr["Qty"] = dt.Rows[j]["qty"].ToString();
                            dr["Alt Qty"] = dt.Rows[j]["aqty"].ToString();
                            dr["Free"] = dt.Rows[j]["free"].ToString();
                            dr["Price"] = dt.Rows[j]["rate"].ToString();
                            dr["Basic Amt"] = dt.Rows[j]["Total"].ToString();
                            dr["Dis(%)"] = dt.Rows[j]["discountper"].ToString();
                            dr["Dis Amt"] = dt.Rows[j]["discountamt"].ToString();
                            dr["Tax(%)"] = dt.Rows[j]["taxper"].ToString();
                            dr["Tax Amt"] = dt.Rows[j]["Tax"].ToString();
                            dr["Add Tax(%)"] = dt.Rows[j]["addtaxper"].ToString();
                            dr["Add Tax Amt"] = dt.Rows[j]["addtax"].ToString();
                            dr["Cess Amt"] = dt.Rows[j]["cess"].ToString();
                            dr["Subtotal"] = dt.Rows[j]["Amount"].ToString();
                            dr["Other"] = "0";
                            dr["Round Off"] = "";
                            dr["Net Amt"] = dt.Rows[j]["totalnet"].ToString();

                            main.Rows.Add(dr);
                        }
                    }

                }
                Double[] tot = new Double[main.Columns.Count];
                for (int i = 0; i < main.Rows.Count; i++)
                {
                    for (int j = 8; j < main.Columns.Count; j++)
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
                lastdr[4] = "";
                lastdr[5] = "";
                lastdr[6] = "";
                lastdr[7] = "";
                for (int i = 8; i < main.Columns.Count; i++)
                {
                    if (i == 8)
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
                lvsalereg.Columns.Clear();
                lvsalereg.Items.Clear();
                int ColCount = main.Columns.Count;
                //Add columns
                for (int k = 0; k < ColCount; k++)
                {
                    if (k == 0)
                    {
                        lvsalereg.Columns.Add(main.Columns[k].ColumnName, 0);
                    }
                    else
                    {
                        lvsalereg.Columns.Add(main.Columns[k].ColumnName, 120);
                    }
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
                        lvsalereg.Items.Add(lvi);
                        //  }
                    }
                }
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                bindsearch();
            }
            catch
            {
            }
            finally
            {
                con.Close();
            }
        }
    }
}
