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
    public partial class ItemPriceList : Form
    {
        private Master master;
        private TabControl tabControl;
        Connection conn = new Connection();
        OleDbSettings ods = new OleDbSettings();
        DataTable main = new DataTable();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        public ItemPriceList()
        {
            InitializeComponent();
        }
        public ItemPriceList(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
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
                LVitem.Items.Clear();
                DataTable dt = new DataTable();
                dt = conn.getdataset("select p.Product_Name,pp.Batchno,pp.BasicPrice,pp.SalePrice,pp.MRP,pp.PurchasePrice from ProductMaster p inner join ProductPriceMaster pp on p.ProductID=pp.Productid where p.isactive=1 and pp.isactive=1 order by p.Product_Name");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ListViewItem li;
                        li = LVitem.Items.Add(dt.Rows[i]["Product_Name"].ToString());
                        li.SubItems.Add(dt.Rows[i]["Batchno"].ToString());
                        li.SubItems.Add(dt.Rows[i]["BasicPrice"].ToString());
                        li.SubItems.Add(dt.Rows[i]["SalePrice"].ToString());
                        li.SubItems.Add(dt.Rows[i]["MRP"].ToString());
                        li.SubItems.Add(dt.Rows[i]["PurchasePrice"].ToString());
                    }
                }
            }
            catch
            { 
            }
        }
        DataTable userrights = new DataTable();
        private void ItemPriceList_Load(object sender, EventArgs e)
        {
            try
            {
                LVitem.Columns.Add("Item Name", 300, HorizontalAlignment.Left);
                LVitem.Columns.Add("Batch No", 120, HorizontalAlignment.Center);
                LVitem.Columns.Add("Basic Price", 120, HorizontalAlignment.Center);
                LVitem.Columns.Add("Sale Price", 120, HorizontalAlignment.Center);
                LVitem.Columns.Add("MRP", 120, HorizontalAlignment.Center);
                LVitem.Columns.Add("Purchase Price", 120, HorizontalAlignment.Center);
                binddata();
                userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[10]["p"].ToString() == "False")
                    {
                        btnprint.Enabled = false;
                    }
                }
            }
            catch
            {
            }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt1 = new DataTable();
                dt1 = conn.getdataset("select p.Product_Name,pp.Batchno,pp.BasicPrice,pp.SalePrice,pp.MRP,pp.PurchasePrice from ProductMaster p inner join ProductPriceMaster pp on p.ProductID=pp.Productid where p.isactive=1 and pp.isactive=1 order by p.Product_Name");
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
                            wb.Worksheets.Add(dt1, "Item Price List");
                            // wb.Worksheets.Add(dt1, "ItemPrice");
                            wb.SaveAs(folderPath + "Item Price List" + DateTimeName + ".xlsx");
                        }
                        MessageBox.Show("Export Data Sucessfully");
                        DialogResult dr = MessageBox.Show("Do you want to Open Document?", "Item Price List", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(folderPath + "Item Price List" + DateTimeName + ".xlsx");
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
    }
}
