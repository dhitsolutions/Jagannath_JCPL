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
    public partial class POSRegister : Form
    {
        private Master master;
        private TabControl tabControl;
        public static string iid = "";
        Connection conn = new Connection();
        OleDbSettings ods = new OleDbSettings();
        DataTable main = new DataTable();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        public POSRegister()
        {
            InitializeComponent();
        }

        public POSRegister(Master master, TabControl tabControl)
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
        private void POSRegister_Load(object sender, EventArgs e)
        {
            try
            {
                userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[9]["p"].ToString() == "False")
                    {
                        btnexcel.Enabled = false;
                    }
                }
                DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
                DTPFrom.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                DTPFrom.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                DTPTo.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                DTPTo.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                DTPFrom.Value = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());

                DTPFrom.CustomFormat = Master.dateformate;
                DTPTo.CustomFormat = Master.dateformate;
                this.ActiveControl = DTPFrom;
            }
            catch
            {
            }
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
                main = new DataTable();
                main.Columns.Add("Date", typeof(string));
                main.Columns.Add("Bill No", typeof(string));
                main.Columns.Add("POS Bill No", typeof(string));
                main.Columns.Add("Party", typeof(string));
                main.Columns.Add("Item", typeof(string));
                main.Columns.Add("MRP", typeof(string));
                main.Columns.Add("Qty", typeof(string));
                main.Columns.Add("Rate", typeof(string));
                main.Columns.Add("Basic Amt", typeof(string));
                main.Columns.Add("Dis[%]", typeof(string));
                main.Columns.Add("Dis Amt", typeof(string));
                main.Columns.Add("Tax[%]", typeof(string));
                main.Columns.Add("Tax Amt", typeof(string));
                main.Columns.Add("Add Tax Amt", typeof(string));
                main.Columns.Add("Total Bill Amt", typeof(string));


                //DataTable dt = conn.getdataset("select b.BillDate,b.BillId,b.billno,b.Terms,bp.ItemName,bp.Qty,bp.Rate,bp.Amount as basicamt,bp.Discount,bp.DiscountAmt,bp.igst as tax,bp.cgst,bp.sgst,bp.Addtax,bp.Addtaxamt from BillPOSMaster b inner join BillPOSProductMaster bp on b.BillId=bp.BillId where b.isactive=1 and bp.isactive=1");
                DataTable dt = conn.getdataset("select b.BillDate,b.BillId,b.billno,b.Terms,bp.ItemName,bp.Qty,bp.Rate,bp.Amount as basicamt,sum(bp.Discount + b.disamt) as Discount,sum(bp.DiscountAmt + b.adddisamt) as DiscountAmt,bp.igst as tax,bp.cgst,bp.sgst,bp.Addtax,bp.Addtaxamt,bp.total from BillPOSMaster b inner join BillPOSProductMaster bp on b.BillId=bp.BillId where b.isactive=1 and bp.isactive=1 group by b.BillDate,b.BillId,b.billno,b.Terms,bp.ItemName,bp.Qty,bp.Rate,bp.Amount,bp.igst,bp.cgst,bp.sgst,bp.Addtax,bp.Addtaxamt,bp.total");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataTable productid = conn.getdataset("select productID from ProductMaster where isactive=1 and Product_Name='" + dt.Rows[i]["ItemName"].ToString() + "'");
                    DataTable mrp = conn.getdataset("select MRP from Productpricemaster where isactive=1 and productid='" + productid.Rows[0]["productID"].ToString() + "'");

                    DataRow dr = main.NewRow();
                    string d = Convert.ToDateTime(dt.Rows[i]["BillDate"]).ToString(Master.dateformate);
                    dr["Date"] = d;
                    dr["Bill No"] = dt.Rows[i]["BillId"].ToString();
                    dr["POS Bill No"] = dt.Rows[i]["billno"].ToString();
                    dr["Party"] = dt.Rows[i]["Terms"].ToString();
                    dr["Item"] = dt.Rows[i]["ItemName"].ToString();
                    dr["MRP"] = mrp.Rows[0]["MRP"].ToString();
                    dr["Qty"] = dt.Rows[i]["Qty"].ToString();
                    dr["Rate"] = dt.Rows[i]["Rate"].ToString();
                    dr["Basic Amt"] = dt.Rows[i]["basicamt"].ToString();
                    dr["Dis[%]"] = dt.Rows[i]["Discount"].ToString();
                    dr["Dis Amt"] = dt.Rows[i]["DiscountAmt"].ToString();
                    dr["Tax[%]"] = dt.Rows[i]["tax"].ToString();
                    Double taxamt = Convert.ToDouble(dt.Rows[i]["cgst"].ToString()) + Convert.ToDouble(dt.Rows[i]["sgst"].ToString());
                    dr["Tax Amt"] = taxamt;
                    dr["Add Tax Amt"] = dt.Rows[i]["Addtax"].ToString();
                    dr["Total Bill Amt"] = dt.Rows[i]["total"].ToString();

                    main.Rows.Add(dr);
                }
                Double[] tot = new Double[main.Columns.Count];
                for (int i = 0; i < main.Rows.Count; i++)
                {
                    for (int j = 5; j < main.Columns.Count; j++)
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
                for (int i = 5; i < main.Columns.Count; i++)
                {
                    if (i == 5)
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
                LVpos.Items.Clear();
                int ColCount = main.Columns.Count;
                //Add columns
                for (int k = 0; k < ColCount; k++)
                {
                    LVpos.Columns.Add(main.Columns[k].ColumnName, 120);
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
                        LVpos.Items.Add(lvi);
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

        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
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
                        string folderPath = fbd.SelectedPath + "\\";
                        String DateTimeName = DateTime.Now.ToString("dd_MMM_yyyy hh_mm_ss");
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(main, "POS Register");
                            // wb.Worksheets.Add(dt1, "ItemPrice");
                            wb.SaveAs(folderPath + "POS Register(BillWise)" + DateTimeName + ".xlsx");
                        }
                        DialogResult dr = MessageBox.Show("Do you want to Open Document?", "POS Register(BillWise)", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(folderPath + "POS Register(BillWise)" + DateTimeName + ".xlsx");
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
        public void open()
        {
            try
            {
                this.Enabled = false;
                iid = LVpos.Items[LVpos.FocusedItem.Index].SubItems[1].Text;

                DefaultPOS dlg = new DefaultPOS(master, tabControl);


                dlg.Update(1, iid);
                master.AddNewTab(dlg);
                dlg.Show();
            }
            finally
            {
                this.Enabled = true;
            }
        }
        private void LVpos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[9]["v"].ToString() == "True" || userrights.Rows[9]["u"].ToString() == "True")
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

        private void BtnViewReport_Enter(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = false;
            BtnViewReport.BackColor = Color.FromArgb(94, 191, 174);
            BtnViewReport.ForeColor = Color.White;
        }

        private void BtnViewReport_Leave(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = true;
            BtnViewReport.BackColor = Color.FromArgb(51, 153, 255);
            BtnViewReport.ForeColor = Color.White;
        }

        private void btnexcel_Enter(object sender, EventArgs e)
        {
            btnexcel.UseVisualStyleBackColor = false;
            btnexcel.BackColor = Color.FromArgb(206, 204, 254);
            btnexcel.ForeColor = Color.White;
        }

        private void btnexcel_Leave(object sender, EventArgs e)
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

        private void LVpos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[9]["v"].ToString() == "True" || userrights.Rows[9]["u"].ToString() == "True")
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
    }
}
