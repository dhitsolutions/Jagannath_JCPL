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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.rtf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using ClosedXML.Excel;
using System.Data.OleDb;
using System.Data.Common;
using System.Diagnostics;


namespace RamdevSales
{

    public partial class Itemmaster : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection con1 = new Connection();
        public static string iid = "";
        Master master;
        private TabControl tabControl;
        public Itemmaster()
        {
            InitializeComponent();
        }

        public Itemmaster(TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.tabControl = tabControl;
        }

        public Itemmaster(Master master, TabControl tabControl)
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
        public void listviewbind()
        {
            try
            {
                LVclient.Items.Clear();

                SqlCommand cmd = new SqlCommand("select p.ProductID,p.Product_Name,c.Companyname,p.groupname,p.Unit,pp.SalePrice,pp.MRP,pp.PurchasePrice from productmaster p inner join (select productid, max(saleprice) SalePrice,max(MRP) MRP, max(purchaseprice) PurchasePrice from productpricemaster where isactive=1 group by productid) as pp on pp.productid=p.productid inner join companymaster c on c.companyid=p.companyid where p.isactive=1 order by p.product_name", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        LVclient.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[7].ToString());

                    }
                }
            }
            catch (Exception ex)
            {
                //  MessageBox.Show("Error:" + ex.Message);
            }
            finally
            {

            }
        }
        public void binddrop()
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
                    txtsearch.DataSource = dt;
                    txtsearch.DisplayMember = "column_name";
                    txtsearch.ValueMember = "ClientID";
                }

            }
            catch
            {
            }
        }
        private void btnnew_Click(object sender, EventArgs e)
        {
            try
            {
                Itementry dlg = new Itementry(master, tabControl);
                master.AddNewTab(dlg);
                //dlg.MdiParent = this.MdiParent;
                //dlg.StartPosition = FormStartPosition.CenterScreen;
                //this.Hide();
                //dlg.Show();
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Error: " + ex.Message);
            }
        }
        DataTable userrights = new DataTable();
        private void Itemmaster_Load(object sender, EventArgs e)
        {
            con.Open();
            userrights = con1.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[10]["a"].ToString() == "False")
                {
                    btnnew.Enabled = false;
                }
                if (userrights.Rows[10]["p"].ToString() == "False")
                {
                    btnprint.Enabled = false;
                }
            }
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            LVclient.Columns.Add("ProductID", 0, HorizontalAlignment.Center);
            LVclient.Columns.Add("Item Name", 200, HorizontalAlignment.Center);
            LVclient.Columns.Add("Company Name", 200, HorizontalAlignment.Center);
            LVclient.Columns.Add("Group Name", 200, HorizontalAlignment.Center);
            LVclient.Columns.Add("Unit", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("Sale Price", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("MRP", 100, HorizontalAlignment.Left);
            LVclient.Columns.Add("PurchasePrice", 150, HorizontalAlignment.Left);
            listviewbind();
            binddrop();
            con.Close();
            // this.ActiveControl = txtsearch;
            this.ActiveControl = btnnew;
        }
        public void open()
        {
            try
            {
                this.Enabled = false;//optional, better target a panel or specific controls
                iid = LVclient.Items[LVclient.FocusedItem.Index].SubItems[0].Text;

                Itementry dlg = new Itementry(master, tabControl);

                dlg.Update(1);
                master.AddNewTab(dlg);

            }
            finally
            {
                this.Enabled = true;
            }
        }
        private void LVclient_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[10]["v"].ToString() == "True" || userrights.Rows[10]["u"].ToString() == "True")
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
        public void bindserch()
        {
            try
            {
                LVclient.Items.Clear();

                if (txtser.Text == "")
                {
                    SqlCommand cmd = new SqlCommand("select p.ProductID,p.Product_Name,c.Companyname,p.groupname,p.Unit,pp.SalePrice,pp.MRP,pp.PurchasePrice from productmaster p inner join (select productid, max(saleprice) SalePrice,max(MRP) MRP, max(purchaseprice) PurchasePrice from productpricemaster where isactive=1 group by productid) as pp on pp.productid=p.productid inner join companymaster c on c.companyid=p.companyid where p.isactive=1 order by p.product_name", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        LVclient.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[7].ToString());
                    }
                }
                else
                {
                    if (txtsearch.SelectedIndex == 0)
                    {
                        MessageBox.Show("Select Column Name");
                        return;
                    }
                    SqlCommand cmd = new SqlCommand("select p.ProductID,p.Product_Name,c.Companyname,p.groupname,p.Unit,pp.SalePrice,pp.MRP,pp.PurchasePrice from productmaster p inner join (select productid, max(saleprice) SalePrice,max(MRP) MRP, max(purchaseprice) PurchasePrice from productpricemaster group by productid) as pp on pp.productid=p.productid inner join companymaster c on c.companyid=p.companyid where " + txtsearch.Text + " like '%" + txtser.Text + "%' and p.isactive=1 order by p.product_name", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        LVclient.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                        LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[7].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                //  MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void btnsearch_Click(object sender, EventArgs e)
        {
            bindserch();
        }



        private void btnexport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                //SqlCommand cmd = new SqlCommand("select p.Product_Name,c.companyname,p.GroupName,p.Unit,p.Altunit,p.Convfactor,p.Packing,p.IsBatch,p.Hsn_Sac_Code,pp.Batchno,pp.BasicPrice,pp.SalePrice,pp.MRP,pp.PurchasePrice,pp.Barcode,pp.OpStock,pp.ExpDt,pp.mfgdt,pp.Expdays,pp.SelfVal,pp.minsaleprice,pp.oploose,pp.opstockval,i.saletypeid,i.system,i.category,i.sgst,i.cgst,i.igst,i.additax,i.onwhich,i.isonmrp,i.isonfreegoods from ProductMaster p inner join ProductPriceMaster pp on p.ProductID=pp.ProductID inner join ItemTaxMaster i on pp.ProductID=i.ProductID inner join CompanyMaster c on p.CompanyID=c.CompanyID where p.isactive=1 and pp.isactive=1 and i.isactive=1", con);
                // SqlCommand cmd = new SqlCommand("select p.Product_Name,c.companyname,p.GroupName,p.Unit,p.Altunit,p.Convfactor,p.Packing,p.IsBatch,p.Hsn_Sac_Code,pp.Batchno,pp.BasicPrice,pp.SalePrice,pp.MRP,pp.PurchasePrice,pp.Barcode,pp.OpStock,pp.ExpDt,pp.mfgdt,pp.Expdays,pp.SelfVal,pp.minsaleprice,pp.oploose,pp.opstockval,i.saletypeid,i.system,i.category,i.sgst,i.cgst,i.igst,i.additax,i.onwhich,i.isonmrp,i.isonfreegoods from ProductMaster p inner join ProductPriceMaster pp on p.ProductID=pp.ProductID inner join TaxSlabMaster i on p.taxslab=i.Taxslabname inner join CompanyMaster c on p.CompanyID=c.CompanyID where p.isactive=1 and pp.isactive=1 and i.isactive=1 group by p.Product_Name,c.companyname,p.GroupName,p.Unit,p.Altunit,p.Convfactor,p.Packing,p.IsBatch,p.Hsn_Sac_Code,pp.Batchno,pp.BasicPrice,pp.SalePrice,pp.MRP,pp.PurchasePrice,pp.Barcode,pp.OpStock,pp.ExpDt,pp.mfgdt,pp.Expdays,pp.SelfVal,pp.minsaleprice,pp.oploose,pp.opstockval,i.saletypeid,i.system,i.category,i.sgst,i.cgst,i.igst,i.additax,i.onwhich,i.isonmrp,i.isonfreegoods", con);
                SqlCommand cmd = new SqlCommand("select p.Product_Name,c.companyname,p.GroupName,p.Unit,p.Altunit,p.Convfactor,p.Packing,p.IsBatch,p.Hsn_Sac_Code,p.isserial,p.cessper,p.cessamt,p.taxslab,pp.Batchno,pp.BasicPrice,pp.SalePrice,pp.MRP,pp.PurchasePrice,pp.Barcode,pp.OpStock,pp.ExpDt,pp.mfgdt,pp.Expdays,pp.SelfVal,pp.minsaleprice,pp.oploose,pp.opstockval,p.itemnumber,p.OT1 from ProductMaster p inner join ProductPriceMaster pp on p.ProductID=pp.ProductID  inner join CompanyMaster c on p.CompanyID=c.CompanyID where p.isactive=1 and pp.isactive=1  group by p.Product_Name,c.companyname,p.GroupName,p.Unit,p.Altunit,p.Convfactor,p.Packing,p.IsBatch,p.Hsn_Sac_Code,pp.Batchno,pp.BasicPrice,pp.SalePrice,pp.MRP,pp.PurchasePrice,pp.Barcode,pp.OpStock,pp.ExpDt,pp.mfgdt,pp.Expdays,pp.SelfVal,pp.minsaleprice,pp.oploose,pp.opstockval,p.isserial,p.cessper,p.cessamt,p.taxslab,p.itemnumber,p.OT1", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);
                //DataTable dt1 = new DataTable();
                //SqlCommand cmd1 = new SqlCommand("SELECT * from ProductPriceMaster", con);
                //SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

                //da1.Fill(dt1);
                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        string[] files = Directory.GetFiles(fbd.SelectedPath);
                        string folderPath = fbd.SelectedPath + "\\";
                        String DateTimeName = DateTime.Now.ToString("dd_MMM_yyyy hh_mm_ss");
                        // string folderPath = "C:\\Excel\\";
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(dt, "ItemName");
                            // wb.Worksheets.Add(dt1, "ItemPrice");
                            wb.SaveAs(folderPath + "Item" + DateTimeName + ".xlsx");
                        }
                        MessageBox.Show("Export Data Sucessfully");
                        DialogResult dr = MessageBox.Show("Do you want to Open Document?", "Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(folderPath + "Item" + DateTimeName + ".xlsx");
                            String pathToExecutable = "AcroRd32.exe";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnimport_Click(object sender, EventArgs e)
        {
            try
            {
                ImportItem frm = new ImportItem(master, tabControl);
                master.AddNewTab(frm);

            }
            catch
            {
            }
        }

        private void btnnew_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    txtsearch.Focus();
            //}
        }
        public static string s;
        private void txtsearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < txtsearch.Items.Count; i++)
                {
                    s = txtsearch.GetItemText(txtsearch.Items[i]);
                    if (s == txtsearch.Text)
                    {
                        inList = true;
                        txtsearch.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    txtsearch.Text = "";
                }

                txtser.Focus();
            }
        }

        private void txtser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnsearch.Focus();
            }
        }

        private void LVclient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[10]["v"].ToString() == "True" || userrights.Rows[10]["u"].ToString() == "True")
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

        private void txtser_Enter(object sender, EventArgs e)
        {
            txtser.BackColor = System.Drawing.Color.LightYellow;
        }

        private void txtser_Leave(object sender, EventArgs e)
        {
            txtser.BackColor = System.Drawing.Color.White;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void LVclient_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }

        private void btnimport_MouseEnter(object sender, EventArgs e)
        {
            btnimport.UseVisualStyleBackColor = false;
            btnimport.BackColor = System.Drawing.Color.FromArgb(118, 72, 233);
            btnimport.ForeColor = System.Drawing.Color.White;
        }

        private void btnimport_MouseLeave(object sender, EventArgs e)
        {
            btnimport.UseVisualStyleBackColor = true;
            btnimport.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnimport.ForeColor = System.Drawing.Color.White;
        }

        private void btnsearch_MouseEnter(object sender, EventArgs e)
        {
            btnsearch.UseVisualStyleBackColor = false;
            btnsearch.BackColor = System.Drawing.Color.FromArgb(94, 191, 174);
            btnsearch.ForeColor = System.Drawing.Color.White;
        }

        private void btnsearch_MouseLeave(object sender, EventArgs e)
        {
            btnsearch.UseVisualStyleBackColor = true;
            btnsearch.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnsearch.ForeColor = System.Drawing.Color.White;
        }

        private void btnnew_MouseEnter(object sender, EventArgs e)
        {
            btnnew.UseVisualStyleBackColor = false;
            btnnew.BackColor = System.Drawing.Color.FromArgb(9, 106, 3);
            btnnew.ForeColor = System.Drawing.Color.White;
        }

        private void btnnew_MouseLeave(object sender, EventArgs e)
        {
            btnnew.UseVisualStyleBackColor = true;
            btnnew.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnnew.ForeColor = System.Drawing.Color.White;
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = false;
            btnClose.BackColor = System.Drawing.Color.FromArgb(248, 152, 94);
            btnClose.ForeColor = System.Drawing.Color.White;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = true;
            btnClose.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnClose.ForeColor = System.Drawing.Color.White;
        }

        private void btnexport_MouseEnter(object sender, EventArgs e)
        {
            btnexport.UseVisualStyleBackColor = false;
            btnexport.BackColor = System.Drawing.Color.FromArgb(118, 72, 233);
            btnexport.ForeColor = System.Drawing.Color.White;
        }

        private void btnexport_MouseLeave(object sender, EventArgs e)
        {
            btnexport.UseVisualStyleBackColor = true;
            btnexport.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnexport.ForeColor = System.Drawing.Color.White;
        }

        private void btnsearch_Enter(object sender, EventArgs e)
        {
            btnsearch.UseVisualStyleBackColor = false;
            btnsearch.BackColor = System.Drawing.Color.FromArgb(94, 191, 174);
            btnsearch.ForeColor = System.Drawing.Color.White;
        }

        private void btnsearch_Leave(object sender, EventArgs e)
        {
            btnsearch.UseVisualStyleBackColor = true;
            btnsearch.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnsearch.ForeColor = System.Drawing.Color.White;
        }

        private void btnnew_Enter(object sender, EventArgs e)
        {
            btnnew.UseVisualStyleBackColor = false;
            btnnew.BackColor = System.Drawing.Color.FromArgb(9, 106, 3);
            btnnew.ForeColor = System.Drawing.Color.White;
        }

        private void btnnew_Leave(object sender, EventArgs e)
        {
            btnnew.UseVisualStyleBackColor = true;
            btnnew.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnnew.ForeColor = System.Drawing.Color.White;
        }

        private void btnimport_Enter(object sender, EventArgs e)
        {
            btnimport.UseVisualStyleBackColor = false;
            btnimport.BackColor = System.Drawing.Color.FromArgb(118, 72, 233);
            btnimport.ForeColor = System.Drawing.Color.White;
        }

        private void btnimport_Leave(object sender, EventArgs e)
        {
            btnimport.UseVisualStyleBackColor = true;
            btnimport.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnimport.ForeColor = System.Drawing.Color.White;
        }

        private void btnexport_Enter(object sender, EventArgs e)
        {
            btnexport.UseVisualStyleBackColor = false;
            btnexport.BackColor = System.Drawing.Color.FromArgb(118, 72, 233);
            btnexport.ForeColor = System.Drawing.Color.White;
        }

        private void btnexport_Leave(object sender, EventArgs e)
        {
            btnexport.UseVisualStyleBackColor = true;
            btnexport.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnexport.ForeColor = System.Drawing.Color.White;
        }

        private void btnClose_Enter(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = false;
            btnClose.BackColor = System.Drawing.Color.FromArgb(248, 152, 94);
            btnClose.ForeColor = System.Drawing.Color.White;
        }

        private void btnClose_Leave(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = true;
            btnClose.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnClose.ForeColor = System.Drawing.Color.White;
        }
        string searchstr;
        private void txtsearch_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = txtsearch.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            txtsearch.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //empty the string for every 1 seconds
            //searchstr = "";
        }

        private void txtsearch_Leave(object sender, EventArgs e)
        {
            txtsearch.Text = s;
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                Printing prndata = new Printing();
                if (LVclient.Items.Count > 0)
                {
                    prndata.execute("delete from printing");
                    DataTable dt1 = con1.getdataset("select * from company WHERE isactive=1");
                    int j = 1;
                    for (int i = 0; i < LVclient.Items.Count; i++)
                    {
                        string itemname = "", CompanyName = "", GroupName = "", Unit = "", SalePrice = "", MRP = "", PurchasePrice = "";
                        itemname = LVclient.Items[i].SubItems[1].Text;
                        CompanyName = LVclient.Items[i].SubItems[2].Text;
                        GroupName = LVclient.Items[i].SubItems[3].Text;
                        Unit = LVclient.Items[i].SubItems[4].Text;
                        SalePrice = LVclient.Items[i].SubItems[5].Text;
                        MRP = LVclient.Items[i].SubItems[6].Text;
                        PurchasePrice = LVclient.Items[i].SubItems[7].Text;
                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22)VALUES";
                        qry += "('" + j++ + "','" + dt1.Rows[0][0].ToString() + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][2].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + dt1.Rows[0][11].ToString() + "','" + dt1.Rows[0][12].ToString() + "','" + dt1.Rows[0][13].ToString() + "','" + itemname + "','" + CompanyName + "','" + GroupName + "','" + Unit + "','" + SalePrice + "','" + MRP + "','" + PurchasePrice + "')";
                        prndata.execute(qry);
                    }
                    Print popup = new Print("ItemReport");
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

        private void btnprint_Enter(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = false;
            btnprint.BackColor = System.Drawing.Color.FromArgb(176, 111, 193);
            btnprint.ForeColor = System.Drawing.Color.White;
        }

        private void btnprint_Leave(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = true;
            btnprint.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnprint.ForeColor = System.Drawing.Color.White;
        }

        private void btnprint_MouseEnter(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = false;
            btnprint.BackColor = System.Drawing.Color.FromArgb(176, 111, 193);
            btnprint.ForeColor = System.Drawing.Color.White;
        }

        private void btnprint_MouseLeave(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = true;
            btnprint.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnprint.ForeColor = System.Drawing.Color.White;
        }

        private void txtsearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < txtsearch.Items.Count; i++)
                {
                    s = txtsearch.GetItemText(txtsearch.Items[i]);
                    if (s == txtsearch.Text)
                    {
                        inList = true;
                        txtsearch.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    txtsearch.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void txtser_TextChanged(object sender, EventArgs e)
        {
            bindserch();
        }





        //private void btnimport_Click(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        string excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Excel\\Item10_Jul_2017 09_46_33.xlsx;Extended Properties=\"Excel 12.0 Xml;HDR=Yes;IMEX=1;\"";

        //    // Create Connection to Excel Workbook
        //    using (OleDbConnection connection =
        //                 new OleDbConnection(excelConnectionString))
        //    {
        //        OleDbCommand command = new OleDbCommand
        //                ("Select * FROM [ItemName$]", connection);

        //        connection.Open(); //HERE IS WHERE THE ERROR IS

        //        // Create DbDataReader to Data Worksheet
        //        using (DbDataReader dr = command.ExecuteReader())
        //        {
        //            // SQL Server Connection String
        //            string sqlConnectionString = "Data Source=ADMIN;Initial Catalog=contilsoftware;User ID=sa;Password=root;Integrated Security=True";

        //            // Bulk Copy to SQL Server
        //            using (SqlBulkCopy bulkCopy =
        //                       new SqlBulkCopy(sqlConnectionString))
        //            {
        //                bulkCopy.DestinationTableName = "BillPOSMaster";
        //                bulkCopy.WriteToServer(dr);
        //                MessageBox.Show("Data Exoprted To Sql Server Succefully");
        //            }
        //        }

        //    }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error: " + ex.Message);
        //    }
        //}


    }
}
