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
    public partial class PrintBarcode : Form
    {
        private Master master;
        private TabControl tabControl;
        OleDbSettings ods = new OleDbSettings();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        Printing prn = new Printing();
        private ListViewHitTestInfo hitinfo;
        private TextBox editbox = new TextBox();
        bool hasValidate;
        DataTable userrights = new DataTable();

        public PrintBarcode()
        {
            InitializeComponent();
        }

        public PrintBarcode(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
            editbox.Parent = LVledger;
            editbox.Hide();
            editbox.LostFocus += new EventHandler(editbox_LostFocus);
            editbox.KeyDown += new KeyEventHandler(editbox_KeyDown);
            LVledger.MouseDoubleClick += new MouseEventHandler(LVledger_MouseDoubleClick);
            LVledger.FullRowSelect = true;
        }
        void editbox_LostFocus(object sender, EventArgs e)
        {
            hitinfo.SubItem.Text = editbox.Text;
            editbox.Hide();
        }
        void editbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                LVledger.Focus();
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
        private void PrintBarcode_Load(object sender, EventArgs e)
        {
           // userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
           // if (userrights.Rows.Count > 0)
          //  {
                    DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
                    DTPFrom.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                    DTPFrom.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                    DTPTo.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                    DTPTo.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                    DTPFrom.Value = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                    DTPFrom.CustomFormat = Master.dateformate;
                    DTPTo.CustomFormat = Master.dateformate;
                    LVledger.CheckBoxes = true;
                    LVledger.Columns.Add("", 20, HorizontalAlignment.Left);
                    LVledger.Columns.Add("Item", 250, HorizontalAlignment.Left);
                    LVledger.Columns.Add("Batch", 70, HorizontalAlignment.Center);
                    LVledger.Columns.Add("Group Name", 100, HorizontalAlignment.Center);
                    LVledger.Columns.Add("P.Unit", 70, HorizontalAlignment.Center);
                    LVledger.Columns.Add("A.Unit", 70, HorizontalAlignment.Center);
                    LVledger.Columns.Add("Qty", 70, HorizontalAlignment.Center);
                    LVledger.Columns.Add("Rate", 100, HorizontalAlignment.Center);
                    LVledger.Columns.Add("MRP", 100, HorizontalAlignment.Center);
                    LVledger.Columns.Add("GST%", 100, HorizontalAlignment.Center);
                    LVledger.Columns.Add("Barcode", 100, HorizontalAlignment.Center);
                    LVledger.Columns.Add("Item Company", 110, HorizontalAlignment.Center);
                    LVledger.Columns.Add("Company", 120, HorizontalAlignment.Center);
                    LVledger.Columns.Add("HSN", 120, HorizontalAlignment.Center);
                    LVledger.Columns.Add("OT1", 120, HorizontalAlignment.Center);

                    binditemdropdown();

                //if (userrights.Rows[44]["p"].ToString() == "False")
                //{
                //    btnprint1.Enabled = false;
                //    btnprint2.Enabled = false;
                //    btnprint3.Enabled = false;
                //}
          //  }
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
        public void binddata()
        {
            try
            {
                if (cmbbarcode.Text == "Purchased Item")
                {
                    DataTable dt = new DataTable();
                    //dt = conn.getdataset("select bp.Productname,pp.Batchno,bp.qty,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname from BillProductMaster bp inner join ProductMaster p on p.Product_Name=bp.Productname inner join ProductPriceMaster pp on p.ProductID=pp.Productid inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where p.isactive=1 and bp.isactive=1 and pp.isactive=1 and  bp.Billtype='P'and bp.Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bp.Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'");
                    dt = conn.getdataset("select bp.Productname,pp.Batchno,p.GroupName,p.Unit,p.Altunit,sum(bp.qty) as qty,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,p.Hsn_Sac_Code,p.OT1 from BillProductMaster bp inner join ProductMaster p on p.Product_Name=bp.Productname inner join ProductPriceMaster pp on p.ProductID=pp.Productid inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where p.isactive=1 and bp.isactive=1 and pp.isactive=1 and bp.Billtype='P'and bp.Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bp.Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.Productname,pp.Batchno,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,p.GroupName,p.Unit,p.Altunit,p.Hsn_Sac_Code");
                    LVledger.Items.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataTable company = new DataTable();
                        company = conn.getdataset("select * from Company where isActive=1 and CompanyID='" + Master.companyId + "'");
                        ListViewItem li;
                        li = LVledger.Items.Add("");
                        li.SubItems.Add(dt.Rows[i]["Productname"].ToString());
                        li.SubItems.Add(dt.Rows[i]["Batchno"].ToString());
                        li.SubItems.Add(dt.Rows[i]["GroupName"].ToString());
                        li.SubItems.Add(dt.Rows[i]["Unit"].ToString());
                        li.SubItems.Add(dt.Rows[i]["Altunit"].ToString());
                        li.SubItems.Add(dt.Rows[i]["qty"].ToString());
                        li.SubItems.Add(dt.Rows[i]["SalePrice"].ToString());
                        li.SubItems.Add(dt.Rows[i]["MRP"].ToString());
                        li.SubItems.Add(dt.Rows[i]["taxslab"].ToString());
                        li.SubItems.Add(dt.Rows[i]["Barcode"].ToString());
                        li.SubItems.Add(dt.Rows[i]["companyname"].ToString());
                        li.SubItems.Add(company.Rows[0]["companyname"].ToString());
                        li.SubItems.Add(dt.Rows[i]["Hsn_Sac_Code"].ToString());
                        li.SubItems.Add(dt.Rows[i]["OT1"].ToString());
                    }
                }
                else if (cmbbarcode.Text == "Opening Stock")
                {
                    DataTable dt = new DataTable();
                    //dt = conn.getdataset("select bp.Productname,pp.Batchno,bp.qty,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname from BillProductMaster bp inner join ProductMaster p on p.Product_Name=bp.Productname inner join ProductPriceMaster pp on p.ProductID=pp.Productid inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where p.isactive=1 and bp.isactive=1 and pp.isactive=1 and  bp.Billtype='P'and bp.Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bp.Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'");
                    //dt = conn.getdataset("select bp.Productname,pp.Batchno,p.GroupName,p.Unit,p.Altunit,pp.OpStock as OpStock,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname from BillProductMaster bp inner join ProductMaster p on p.Product_Name=bp.Productname inner join ProductPriceMaster pp on p.ProductID=pp.Productid inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where p.isactive=1 and bp.isactive=1 and pp.isactive=1 and bp.Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bp.Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.Productname,pp.Batchno,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,pp.OpStock,p.GroupName,p.Unit,p.Altunit");
                    dt = conn.getdataset("select p.Product_Name,pp.Batchno,p.GroupName,p.Unit,p.Altunit,pp.OpStock as OpStock,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,p.Hsn_Sac_Code,p.OT1 from ProductMaster p  inner join ProductPriceMaster pp on p.ProductID=pp.Productid inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where p.isactive=1 and  pp.isactive=1 group by p.Product_Name,pp.Batchno,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,pp.OpStock,p.GroupName,p.Unit,p.Altunit,p.Hsn_Sac_Code,p.OT1");
                    LVledger.Items.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string bal1 = dt.Rows[i]["OpStock"].ToString();
                        Double d = Convert.ToDouble(bal1);
                        if (Convert.ToInt32(d) > 0)
                        {
                            DataTable company = new DataTable();
                            company = conn.getdataset("select * from Company where isActive=1 and CompanyID='" + Master.companyId + "'");
                            ListViewItem li;
                            li = LVledger.Items.Add("");
                            li.SubItems.Add(dt.Rows[i]["Product_Name"].ToString());
                            li.SubItems.Add(dt.Rows[i]["Batchno"].ToString());
                            li.SubItems.Add(dt.Rows[i]["GroupName"].ToString());
                            li.SubItems.Add(dt.Rows[i]["Unit"].ToString());
                            li.SubItems.Add(dt.Rows[i]["Altunit"].ToString());
                            li.SubItems.Add(dt.Rows[i]["OpStock"].ToString());
                            li.SubItems.Add(dt.Rows[i]["SalePrice"].ToString());
                            li.SubItems.Add(dt.Rows[i]["MRP"].ToString());
                            li.SubItems.Add(dt.Rows[i]["taxslab"].ToString());
                            li.SubItems.Add(dt.Rows[i]["Barcode"].ToString());
                            li.SubItems.Add(dt.Rows[i]["companyname"].ToString());
                            li.SubItems.Add(company.Rows[0]["companyname"].ToString());
                            li.SubItems.Add(dt.Rows[i]["Hsn_Sac_Code"].ToString());
                            li.SubItems.Add(dt.Rows[i]["OT1"].ToString());
                        }
                    }
                }
                else
                {
                    DataTable dt = new DataTable();
                    //  dt = conn.getdataset("select bp.Productname,pp.Batchno,bp.qty,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname from BillProductMaster bp inner join ProductMaster p on p.Product_Name=bp.Productname inner join ProductPriceMaster pp on p.ProductID=pp.Productid inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where p.isactive=1 and bp.isactive=1 and pp.isactive=1 and bp.Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bp.Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'");
                    //   dt = conn.getdataset("select p.Product_Name,pp.Batchno,p.GroupName,p.Unit,p.Altunit,sum(bp.qty) as qty,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname from BillProductMaster bp inner join ProductMaster p on p.Product_Name=bp.Productname inner join ProductPriceMaster pp on p.ProductID=pp.Productid inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where p.isactive=1 and bp.isactive=1 and pp.isactive=1 and bp.Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bp.Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by p.Product_Name,pp.Batchno,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,p.GroupName,p.Unit,p.Altunit");
                    dt = conn.getdataset("select p.Product_Name,pp.Batchno,p.GroupName,p.Unit,p.Altunit,pp.OpStock as OpStock,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,p.Hsn_Sac_Code,p.OT1 from ProductMaster p  inner join ProductPriceMaster pp on p.ProductID=pp.Productid inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where p.isactive=1 and  pp.isactive=1 group by p.Product_Name,pp.Batchno,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,pp.OpStock,p.GroupName,p.Unit,p.Altunit,p.Hsn_Sac_Code,p.OT1");
                    LVledger.Items.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataTable company = new DataTable();
                        company = conn.getdataset("select * from Company where isActive=1 and CompanyID='" + Master.companyId + "'");
                        ListViewItem li;
                        li = LVledger.Items.Add("");
                        li.SubItems.Add(dt.Rows[i]["Product_Name"].ToString());
                        li.SubItems.Add(dt.Rows[i]["Batchno"].ToString());
                        li.SubItems.Add(dt.Rows[i]["GroupName"].ToString());
                        li.SubItems.Add(dt.Rows[i]["Unit"].ToString());
                        li.SubItems.Add(dt.Rows[i]["Altunit"].ToString());
                        li.SubItems.Add(dt.Rows[i]["OpStock"].ToString());
                        li.SubItems.Add(dt.Rows[i]["SalePrice"].ToString());
                        li.SubItems.Add(dt.Rows[i]["MRP"].ToString());
                        li.SubItems.Add(dt.Rows[i]["taxslab"].ToString());
                        li.SubItems.Add(dt.Rows[i]["Barcode"].ToString());
                        li.SubItems.Add(dt.Rows[i]["companyname"].ToString());
                        li.SubItems.Add(company.Rows[0]["companyname"].ToString());
                        li.SubItems.Add(dt.Rows[i]["Hsn_Sac_Code"].ToString());
                        li.SubItems.Add(dt.Rows[i]["OT1"].ToString());
                    }
                }
            }
            catch
            {
            }
        }
        private void BtnViewReport_Click(object sender, EventArgs e)
        {
           // if (userrights.Rows.Count > 0)
          //  {
            //    if (userrights.Rows[44]["v"].ToString() == "True")
              //  {
                    binddata();
             //   }
           //     else
           //     {
           //         MessageBox.Show("You Don't Have Permission to View");
           //         return;
           //     }
         //   }
        }
        public void print()
        {
            try
            {
                prn.execute("delete from printing");
                DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1 and CompanyID='" + Master.companyId + "' ");
                int recordCount = 0;
                hasValidate = true;

                if (LVledger.Items.Count > 0)
                {
                    for (int i = 0; i < LVledger.Items.Count; i++)
                    {
                        if (Convert.ToBoolean(LVledger.Items[i].Checked) == true)
                        {
                            // string str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[6].Text;

                            Double sa = Convert.ToDouble(LVledger.Items[i].SubItems[6].Text);
                            int s = Convert.ToInt32(sa);
                            for (int j = 0; j < s; j++)
                            {
                                string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17)VALUES";
                                qry += "('" + LVledger.Items[i].SubItems[1].Text + "','" + LVledger.Items[i].SubItems[2].Text + "','" + LVledger.Items[i].SubItems[3].Text + "','" + LVledger.Items[i].SubItems[4].Text + "','" + LVledger.Items[i].SubItems[5].Text + "','" + LVledger.Items[i].SubItems[6].Text + "','" + LVledger.Items[i].SubItems[7].Text + "','" + LVledger.Items[i].SubItems[8].Text + "','" + LVledger.Items[i].SubItems[9].Text + "','*" + LVledger.Items[i].SubItems[10].Text + "*','" + LVledger.Items[i].SubItems[11].Text + "','" + LVledger.Items[i].SubItems[12].Text + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][2].ToString() + "','" + LVledger.Items[i].SubItems[10].Text + "','" + LVledger.Items[i].SubItems[13].Text + "','" + LVledger.Items[i].SubItems[14].Text + "')";
                                prn.execute(qry);
                            }
                            recordCount++;
                        }
                    }
                    if (recordCount == 0)
                    {
                        MessageBox.Show("Select Atlist One Record For Printing.");
                        hasValidate = false;
                    }
                }
                else
                {
                    MessageBox.Show("No Record For Printing.");
                    hasValidate = false;
                }
            }
            catch
            {
            }
        }
        private void btnprint1_Click(object sender, EventArgs e)
        {
            try
            {
                print();
                if (hasValidate)
                {
                    Print popup = new Print("Barcode1");
                    popup.ShowDialog();
                    popup.Dispose();
                }
            }
            catch
            {
            }
        }

        private void btnprint2_Click(object sender, EventArgs e)
        {
            try
            {
                print();
                if (hasValidate)
                {
                    Print popup = new Print("Barcode2");
                    popup.ShowDialog();
                    popup.Dispose();
                }
            }
            catch
            {
            }
        }

        private void btnprint3_Click(object sender, EventArgs e)
        {
            try
            {
                print();
                if (hasValidate)
                {
                    Print popup = new Print("Barcode3");
                    popup.ShowDialog();
                    popup.Dispose();
                }
            }
            catch
            {
            }
        }

        public static string s;
        private void cmbbarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbbarcode.Items.Count; i++)
                {
                    s = cmbbarcode.GetItemText(cmbbarcode.Items[i]);
                    if (s == cmbbarcode.Text)
                    {
                        inList = true;
                        cmbbarcode.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbbarcode.Text = "";
                }


                DTPFrom.Focus();
            }
        }

        private void DTPTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnViewReport.Focus();
            }
        }

        private void DTPFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DTPTo.Focus();
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
            BtnViewReport.BackColor = Color.FromArgb(94, 191, 174);
            BtnViewReport.ForeColor = Color.White;
        }

        private void BtnViewReport_Leave(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = true;
            BtnViewReport.BackColor = Color.FromArgb(51, 153, 255);
            BtnViewReport.ForeColor = Color.White;
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

        private void btnprint1_Enter(object sender, EventArgs e)
        {
            btnprint1.UseVisualStyleBackColor = false;
            btnprint1.BackColor = Color.FromArgb(176, 111, 193);
            btnprint1.ForeColor = Color.White;
        }

        private void btnprint1_Leave(object sender, EventArgs e)
        {
            btnprint1.UseVisualStyleBackColor = true;
            btnprint1.BackColor = Color.FromArgb(51, 153, 255);
            btnprint1.ForeColor = Color.White;
        }

        private void btnprint1_MouseEnter(object sender, EventArgs e)
        {
            btnprint1.UseVisualStyleBackColor = false;
            btnprint1.BackColor = Color.FromArgb(176, 111, 193);
            btnprint1.ForeColor = Color.White;
        }

        private void btnprint1_MouseLeave(object sender, EventArgs e)
        {
            btnprint1.UseVisualStyleBackColor = true;
            btnprint1.BackColor = Color.FromArgb(51, 153, 255);
            btnprint1.ForeColor = Color.White;
        }

        private void btnprint2_Enter(object sender, EventArgs e)
        {
            btnprint2.UseVisualStyleBackColor = false;
            btnprint2.BackColor = Color.FromArgb(176, 111, 193);
            btnprint2.ForeColor = Color.White;
        }

        private void btnprint2_Leave(object sender, EventArgs e)
        {
            btnprint2.UseVisualStyleBackColor = true;
            btnprint2.BackColor = Color.FromArgb(51, 153, 255);
            btnprint2.ForeColor = Color.White;
        }

        private void btnprint2_MouseEnter(object sender, EventArgs e)
        {
            btnprint2.UseVisualStyleBackColor = false;
            btnprint2.BackColor = Color.FromArgb(176, 111, 193);
            btnprint2.ForeColor = Color.White;
        }

        private void btnprint2_MouseLeave(object sender, EventArgs e)
        {
            btnprint2.UseVisualStyleBackColor = true;
            btnprint2.BackColor = Color.FromArgb(51, 153, 255);
            btnprint2.ForeColor = Color.White;
        }

        private void btnprint3_Enter(object sender, EventArgs e)
        {
            btnprint3.UseVisualStyleBackColor = false;
            btnprint3.BackColor = Color.FromArgb(176, 111, 193);
            btnprint3.ForeColor = Color.White;
        }

        private void btnprint3_Leave(object sender, EventArgs e)
        {
            btnprint3.UseVisualStyleBackColor = true;
            btnprint3.BackColor = Color.FromArgb(51, 153, 255);
            btnprint3.ForeColor = Color.White;
        }

        private void btnprint3_MouseEnter(object sender, EventArgs e)
        {
            btnprint3.UseVisualStyleBackColor = false;
            btnprint3.BackColor = Color.FromArgb(176, 111, 193);
            btnprint3.ForeColor = Color.White;
        }

        private void btnprint3_MouseLeave(object sender, EventArgs e)
        {
            btnprint3.UseVisualStyleBackColor = true;
            btnprint3.BackColor = Color.FromArgb(51, 153, 255);
            btnprint3.ForeColor = Color.White;
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
        string searchstr;

        private void cmbbarcode_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbbarcode.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbbarcode.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //empty the string for every 1 seconds
            //  searchstr = "";
        }

        private void LVledger_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                hitinfo = LVledger.HitTest(e.X, e.Y);
                editbox.Bounds = hitinfo.SubItem.Bounds;
                editbox.Text = hitinfo.SubItem.Text;
                editbox.Focus();
                editbox.Show();
            }
            catch
            {
            }
        }

        private void cmbbarcode_Leave(object sender, EventArgs e)
        {
            cmbbarcode.Text = s;
        }

        private void cmbbarcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbbarcode.Items.Count; i++)
                {
                    s = cmbbarcode.GetItemText(cmbbarcode.Items[i]);
                    if (s == cmbbarcode.Text)
                    {
                        inList = true;
                        cmbbarcode.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbbarcode.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtitems.Text != "")
                {
                    if (cmbbarcode.Text == "Purchased Item")
                    {
                        DataTable dt = new DataTable();
                        //dt = conn.getdataset("select bp.Productname,pp.Batchno,bp.qty,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname from BillProductMaster bp inner join ProductMaster p on p.Product_Name=bp.Productname inner join ProductPriceMaster pp on p.ProductID=pp.Productid inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where p.isactive=1 and bp.isactive=1 and pp.isactive=1 and  bp.Billtype='P'and bp.Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bp.Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'");
                        if (drpitems.Text == "GroupName" || drpitems.Text == "Packing" || drpitems.Text == "Hsn_Sac_Code" || drpitems.Text == "itemnumber" || drpitems.Text == "ProductID")
                        {
                            //itemname = conn.getsinglevalue("select product_name from productmaster where isactive=1 and (GroupName like '%" + txtitems.Text + "%' and Packing like '%" + txtitems.Text + "%' and Hsn_Sac_Code like '%" + txtitems.Text + "%' and itemnumber like '%" + txtitems.Text + "%')");
                            //    dt = conn.getdataset("select distinct Productname as ItemName from SaleOrderProductMaster where productname in (select product_name from productmaster where isactive=1 and (GroupName like '%" + txtitems.Text + "%' or Packing like '%" + txtitems.Text + "%' or Hsn_Sac_Code like '%" + txtitems.Text + "%' or ProductID like '%" + txtitems.Text + "%' or itemnumber like '%" + txtitems.Text + "%')) and isactive=1 and Billtype='SO' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Productname");
                            dt = conn.getdataset("select bp.Productname,pp.Batchno,p.GroupName,p.Unit,p.Altunit,sum(bp.qty) as qty,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,p.Hsn_Sac_Code,p.OT1 from BillProductMaster bp inner join ProductMaster p on p.Product_Name=bp.Productname inner join ProductPriceMaster pp on p.ProductID=pp.Productid inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where p.Product_Name in (select product_name from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%')) and p.isactive=1 and bp.isactive=1 and pp.isactive=1 and bp.Billtype='P'and bp.Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bp.Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.Productname,pp.Batchno,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,p.GroupName,p.Unit,p.Altunit,p.Hsn_Sac_Code,p.OT1");
                        }
                        else if (drpitems.Text == "companyname")
                        {
                            //itemname = conn.getsinglevalue("select product_name from productmaster where isactive=1 and companyid =(select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))");
                            //    dt = conn.getdataset("select distinct Productname as ItemName from SaleOrderProductMaster where productname in (select product_name from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and isactive=1 and Billtype='SO' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Productname");
                            dt = conn.getdataset("select bp.Productname,pp.Batchno,p.GroupName,p.Unit,p.Altunit,sum(bp.qty) as qty,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,p.Hsn_Sac_Code,p.OT1 from BillProductMaster bp inner join ProductMaster p on p.Product_Name=bp.Productname inner join ProductPriceMaster pp on p.ProductID=pp.Productid inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where p.Product_Name in (select product_name from productmaster where isactive=1 and cm.companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and p.isactive=1 and bp.isactive=1 and pp.isactive=1 and bp.Billtype='P'and bp.Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bp.Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.Productname,pp.Batchno,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,p.GroupName,p.Unit,p.Altunit,p.Hsn_Sac_Code,p.OT1");
                        }
                        else
                        {
                            // itemname = txtitems.Text;
                            // dt = conn.getdataset("select distinct Productname as ItemName from SaleOrderProductMaster where productname like '%" + itemname + "%'  and isactive=1 and Billtype='SO' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Productname");
                            dt = conn.getdataset("select bp.Productname,pp.Batchno,p.GroupName,p.Unit,p.Altunit,sum(bp.qty) as qty,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,p.Hsn_Sac_Code,p.OT1 from BillProductMaster bp inner join ProductMaster p on p.Product_Name=bp.Productname inner join ProductPriceMaster pp on p.ProductID=pp.Productid inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where p.Product_Name like '%" + txtitems.Text + "%' and p.isactive=1 and bp.isactive=1 and pp.isactive=1 and bp.Billtype='P'and bp.Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bp.Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.Productname,pp.Batchno,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,p.GroupName,p.Unit,p.Altunit,p.Hsn_Sac_Code,p.OT1");
                        }

                        LVledger.Items.Clear();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataTable company = new DataTable();
                            company = conn.getdataset("select * from Company where isActive=1 and CompanyID='" + Master.companyId + "'");
                            ListViewItem li;
                            li = LVledger.Items.Add("");
                            li.SubItems.Add(dt.Rows[i]["Productname"].ToString());
                            li.SubItems.Add(dt.Rows[i]["Batchno"].ToString());
                            li.SubItems.Add(dt.Rows[i]["GroupName"].ToString());
                            li.SubItems.Add(dt.Rows[i]["Unit"].ToString());
                            li.SubItems.Add(dt.Rows[i]["Altunit"].ToString());
                            li.SubItems.Add(dt.Rows[i]["qty"].ToString());
                            li.SubItems.Add(dt.Rows[i]["SalePrice"].ToString());
                            li.SubItems.Add(dt.Rows[i]["MRP"].ToString());
                            li.SubItems.Add(dt.Rows[i]["taxslab"].ToString());
                            li.SubItems.Add(dt.Rows[i]["Barcode"].ToString());
                            li.SubItems.Add(dt.Rows[i]["companyname"].ToString());
                            li.SubItems.Add(company.Rows[0]["companyname"].ToString());
                            li.SubItems.Add(dt.Rows[i]["Hsn_Sac_Code"].ToString());
                            li.SubItems.Add(dt.Rows[i]["OT1"].ToString());
                        }
                    }
                    else if (cmbbarcode.Text == "Opening Stock")
                    {
                        DataTable dt = new DataTable();
                        //dt = conn.getdataset("select bp.Productname,pp.Batchno,bp.qty,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname from BillProductMaster bp inner join ProductMaster p on p.Product_Name=bp.Productname inner join ProductPriceMaster pp on p.ProductID=pp.Productid inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where p.isactive=1 and bp.isactive=1 and pp.isactive=1 and  bp.Billtype='P'and bp.Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bp.Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'");
                        //dt = conn.getdataset("select bp.Productname,pp.Batchno,p.GroupName,p.Unit,p.Altunit,pp.OpStock as OpStock,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname from BillProductMaster bp inner join ProductMaster p on p.Product_Name=bp.Productname inner join ProductPriceMaster pp on p.ProductID=pp.Productid inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where p.isactive=1 and bp.isactive=1 and pp.isactive=1 and bp.Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bp.Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.Productname,pp.Batchno,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,pp.OpStock,p.GroupName,p.Unit,p.Altunit");
                        if (drpitems.Text == "GroupName" || drpitems.Text == "Packing" || drpitems.Text == "Hsn_Sac_Code" || drpitems.Text == "itemnumber" || drpitems.Text == "ProductID")
                        {
                            //itemname = conn.getsinglevalue("select product_name from productmaster where isactive=1 and (GroupName like '%" + txtitems.Text + "%' and Packing like '%" + txtitems.Text + "%' and Hsn_Sac_Code like '%" + txtitems.Text + "%' and itemnumber like '%" + txtitems.Text + "%')");
                            //    dt = conn.getdataset("select distinct Productname as ItemName from SaleOrderProductMaster where productname in (select product_name from productmaster where isactive=1 and (GroupName like '%" + txtitems.Text + "%' or Packing like '%" + txtitems.Text + "%' or Hsn_Sac_Code like '%" + txtitems.Text + "%' or ProductID like '%" + txtitems.Text + "%' or itemnumber like '%" + txtitems.Text + "%')) and isactive=1 and Billtype='SO' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Productname");
                            dt = conn.getdataset("select p.Product_Name,pp.Batchno,p.GroupName,p.Unit,p.Altunit,pp.OpStock as OpStock,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,p.Hsn_Sac_Code,p.OT1 from ProductMaster p  inner join ProductPriceMaster pp on p.ProductID=pp.Productid inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where p.Product_Name in (select product_name from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%')) and p.isactive=1 and  pp.isactive=1 group by p.Product_Name,pp.Batchno,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,pp.OpStock,p.GroupName,p.Unit,p.Altunit,p.Hsn_Sac_Code,p.OT1");
                            //    dt = conn.getdataset("select bp.Productname,pp.Batchno,p.GroupName,p.Unit,p.Altunit,sum(bp.qty) as qty,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname from BillProductMaster bp inner join ProductMaster p on p.Product_Name=bp.Productname inner join ProductPriceMaster pp on p.ProductID=pp.Productid inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where p.Product_Name in (select product_name from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%')) and p.isactive=1 and bp.isactive=1 and pp.isactive=1 and bp.Billtype='P'and bp.Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bp.Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.Productname,pp.Batchno,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,p.GroupName,p.Unit,p.Altunit");
                        }
                        else if (drpitems.Text == "companyname")
                        {
                            //itemname = conn.getsinglevalue("select product_name from productmaster where isactive=1 and companyid =(select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))");
                            //    dt = conn.getdataset("select distinct Productname as ItemName from SaleOrderProductMaster where productname in (select product_name from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and isactive=1 and Billtype='SO' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Productname");
                            dt = conn.getdataset("select p.Product_Name,pp.Batchno,p.GroupName,p.Unit,p.Altunit,pp.OpStock as OpStock,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,p.Hsn_Sac_Code,p.OT1 from ProductMaster p  inner join ProductPriceMaster pp on p.ProductID=pp.Productid inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where p.Product_Name in (select product_name from productmaster where isactive=1 and cm.companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and p.isactive=1 and  pp.isactive=1 group by p.Product_Name,pp.Batchno,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,pp.OpStock,p.GroupName,p.Unit,p.Altunit,p.Hsn_Sac_Code,p.OT1");
                            //  dt = conn.getdataset("select bp.Productname,pp.Batchno,p.GroupName,p.Unit,p.Altunit,sum(bp.qty) as qty,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname from BillProductMaster bp inner join ProductMaster p on p.Product_Name=bp.Productname inner join ProductPriceMaster pp on p.ProductID=pp.Productid inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where p.Product_Name in (select product_name from productmaster where isactive=1 and cm.companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and p.isactive=1 and bp.isactive=1 and pp.isactive=1 and bp.Billtype='P'and bp.Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bp.Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.Productname,pp.Batchno,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,p.GroupName,p.Unit,p.Altunit");
                        }
                        else
                        {
                            // itemname = txtitems.Text;
                            // dt = conn.getdataset("select distinct Productname as ItemName from SaleOrderProductMaster where productname like '%" + itemname + "%'  and isactive=1 and Billtype='SO' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Productname");
                            dt = conn.getdataset("select p.Product_Name,pp.Batchno,p.GroupName,p.Unit,p.Altunit,pp.OpStock as OpStock,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,p.Hsn_Sac_Code,p.OT1 from ProductMaster p  inner join ProductPriceMaster pp on p.ProductID=pp.Productid inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where p.Product_Name like '%" + txtitems.Text + "%' and p.isactive=1 and  pp.isactive=1 group by p.Product_Name,pp.Batchno,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,pp.OpStock,p.GroupName,p.Unit,p.Altunit,p.Hsn_Sac_Code,p.OT1");
                            //dt = conn.getdataset("select bp.Productname,pp.Batchno,p.GroupName,p.Unit,p.Altunit,sum(bp.qty) as qty,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname from BillProductMaster bp inner join ProductMaster p on p.Product_Name=bp.Productname inner join ProductPriceMaster pp on p.ProductID=pp.Productid inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where p.Product_Name like '%" + txtitems.Text + "%' and p.isactive=1 and bp.isactive=1 and pp.isactive=1 and bp.Billtype='P'and bp.Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bp.Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.Productname,pp.Batchno,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,p.GroupName,p.Unit,p.Altunit");
                        }
                        // dt = conn.getdataset("select p.Product_Name,pp.Batchno,p.GroupName,p.Unit,p.Altunit,pp.OpStock as OpStock,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname from ProductMaster p  inner join ProductPriceMaster pp on p.ProductID=pp.Productid inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where p.isactive=1 and  pp.isactive=1 group by p.Product_Name,pp.Batchno,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,pp.OpStock,p.GroupName,p.Unit,p.Altunit");
                        LVledger.Items.Clear();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string bal1 = dt.Rows[i]["OpStock"].ToString();
                            Double d = Convert.ToDouble(bal1);
                            if (Convert.ToInt32(d) > 0)
                            {
                                DataTable company = new DataTable();
                                company = conn.getdataset("select * from Company where isActive=1 and CompanyID='" + Master.companyId + "'");
                                ListViewItem li;
                                li = LVledger.Items.Add("");
                                li.SubItems.Add(dt.Rows[i]["Product_Name"].ToString());
                                li.SubItems.Add(dt.Rows[i]["Batchno"].ToString());
                                li.SubItems.Add(dt.Rows[i]["GroupName"].ToString());
                                li.SubItems.Add(dt.Rows[i]["Unit"].ToString());
                                li.SubItems.Add(dt.Rows[i]["Altunit"].ToString());
                                li.SubItems.Add(dt.Rows[i]["OpStock"].ToString());
                                li.SubItems.Add(dt.Rows[i]["SalePrice"].ToString());
                                li.SubItems.Add(dt.Rows[i]["MRP"].ToString());
                                li.SubItems.Add(dt.Rows[i]["taxslab"].ToString());
                                li.SubItems.Add(dt.Rows[i]["Barcode"].ToString());
                                li.SubItems.Add(dt.Rows[i]["companyname"].ToString());
                                li.SubItems.Add(company.Rows[0]["companyname"].ToString());
                                li.SubItems.Add(dt.Rows[i]["Hsn_Sac_Code"].ToString());
                                li.SubItems.Add(dt.Rows[i]["OT1"].ToString());
                            }
                        }
                    }
                    else
                    {
                        DataTable dt = new DataTable();
                        //  dt = conn.getdataset("select bp.Productname,pp.Batchno,bp.qty,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname from BillProductMaster bp inner join ProductMaster p on p.Product_Name=bp.Productname inner join ProductPriceMaster pp on p.ProductID=pp.Productid inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where p.isactive=1 and bp.isactive=1 and pp.isactive=1 and bp.Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bp.Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'");
                        //   dt = conn.getdataset("select p.Product_Name,pp.Batchno,p.GroupName,p.Unit,p.Altunit,sum(bp.qty) as qty,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname from BillProductMaster bp inner join ProductMaster p on p.Product_Name=bp.Productname inner join ProductPriceMaster pp on p.ProductID=pp.Productid inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where p.isactive=1 and bp.isactive=1 and pp.isactive=1 and bp.Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bp.Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by p.Product_Name,pp.Batchno,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,p.GroupName,p.Unit,p.Altunit");
                        if (drpitems.Text == "GroupName" || drpitems.Text == "Packing" || drpitems.Text == "Hsn_Sac_Code" || drpitems.Text == "itemnumber" || drpitems.Text == "ProductID")
                        {
                            //itemname = conn.getsinglevalue("select product_name from productmaster where isactive=1 and (GroupName like '%" + txtitems.Text + "%' and Packing like '%" + txtitems.Text + "%' and Hsn_Sac_Code like '%" + txtitems.Text + "%' and itemnumber like '%" + txtitems.Text + "%')");
                            //    dt = conn.getdataset("select distinct Productname as ItemName from SaleOrderProductMaster where productname in (select product_name from productmaster where isactive=1 and (GroupName like '%" + txtitems.Text + "%' or Packing like '%" + txtitems.Text + "%' or Hsn_Sac_Code like '%" + txtitems.Text + "%' or ProductID like '%" + txtitems.Text + "%' or itemnumber like '%" + txtitems.Text + "%')) and isactive=1 and Billtype='SO' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Productname");
                            //dt = conn.getdataset("select bp.Productname,pp.Batchno,p.GroupName,p.Unit,p.Altunit,sum(bp.qty) as qty,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname from BillProductMaster bp inner join ProductMaster p on p.Product_Name=bp.Productname inner join ProductPriceMaster pp on p.ProductID=pp.Productid inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where p.Product_Name in (select product_name from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%')) and p.isactive=1 and bp.isactive=1 and pp.isactive=1 and bp.Billtype='P'and bp.Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bp.Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.Productname,pp.Batchno,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,p.GroupName,p.Unit,p.Altunit");
                            dt = conn.getdataset("select p.Product_Name,pp.Batchno,p.GroupName,p.Unit,p.Altunit,pp.OpStock as OpStock,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,p.Hsn_Sac_Code,p.OT1 from ProductMaster p  inner join ProductPriceMaster pp on p.ProductID=pp.Productid inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where p.Product_Name in (select product_name from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%')) and p.isactive=1 and  pp.isactive=1 group by p.Product_Name,pp.Batchno,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,pp.OpStock,p.GroupName,p.Unit,p.Altunit,p.Hsn_Sac_Code,p.OT1");
                        }
                        else if (drpitems.Text == "companyname")
                        {
                            //itemname = conn.getsinglevalue("select product_name from productmaster where isactive=1 and companyid =(select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))");
                            //    dt = conn.getdataset("select distinct Productname as ItemName from SaleOrderProductMaster where productname in (select product_name from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and isactive=1 and Billtype='SO' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Productname");
                            //      dt = conn.getdataset("select bp.Productname,pp.Batchno,p.GroupName,p.Unit,p.Altunit,sum(bp.qty) as qty,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname from BillProductMaster bp inner join ProductMaster p on p.Product_Name=bp.Productname inner join ProductPriceMaster pp on p.ProductID=pp.Productid inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where p.Product_Name in (select product_name from productmaster where isactive=1 and cm.companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and p.isactive=1 and bp.isactive=1 and pp.isactive=1 and bp.Billtype='P'and bp.Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bp.Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.Productname,pp.Batchno,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,p.GroupName,p.Unit,p.Altunit");
                            dt = conn.getdataset("select p.Product_Name,pp.Batchno,p.GroupName,p.Unit,p.Altunit,pp.OpStock as OpStock,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,p.Hsn_Sac_Code,p.OT1 from ProductMaster p  inner join ProductPriceMaster pp on p.ProductID=pp.Productid inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where p.Product_Name in (select product_name from productmaster where isactive=1 and cm.companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and p.isactive=1 and  pp.isactive=1 group by p.Product_Name,pp.Batchno,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,pp.OpStock,p.GroupName,p.Unit,p.Altunit,p.Hsn_Sac_Code,p.OT1");
                        }
                        else
                        {
                            // itemname = txtitems.Text;
                            // dt = conn.getdataset("select distinct Productname as ItemName from SaleOrderProductMaster where productname like '%" + itemname + "%'  and isactive=1 and Billtype='SO' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Productname");
                            //     dt = conn.getdataset("select bp.Productname,pp.Batchno,p.GroupName,p.Unit,p.Altunit,sum(bp.qty) as qty,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname from BillProductMaster bp inner join ProductMaster p on p.Product_Name=bp.Productname inner join ProductPriceMaster pp on p.ProductID=pp.Productid inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where p.Product_Name like '%" + txtitems.Text + "%' and p.isactive=1 and bp.isactive=1 and pp.isactive=1 and bp.Billtype='P'and bp.Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and bp.Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by bp.Productname,pp.Batchno,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,p.GroupName,p.Unit,p.Altunit");
                            dt = conn.getdataset("select p.Product_Name,pp.Batchno,p.GroupName,p.Unit,p.Altunit,pp.OpStock as OpStock,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,p.Hsn_Sac_Code,p.OT1 from ProductMaster p  inner join ProductPriceMaster pp on p.ProductID=pp.Productid inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where p.Product_Name like '%" + txtitems.Text + "%' and p.isactive=1 and  pp.isactive=1 group by p.Product_Name,pp.Batchno,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,pp.OpStock,p.GroupName,p.Unit,p.Altunit,p.Hsn_Sac_Code,p.OT1");
                        }
                        //    dt = conn.getdataset("select p.Product_Name,pp.Batchno,p.GroupName,p.Unit,p.Altunit,pp.OpStock as OpStock,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname from ProductMaster p  inner join ProductPriceMaster pp on p.ProductID=pp.Productid inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where p.isactive=1 and  pp.isactive=1 group by p.Product_Name,pp.Batchno,pp.SalePrice,pp.MRP,p.taxslab,pp.Barcode,cm.companyname,pp.OpStock,p.GroupName,p.Unit,p.Altunit");
                        LVledger.Items.Clear();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataTable company = new DataTable();
                            company = conn.getdataset("select * from Company where isActive=1 and CompanyID='" + Master.companyId + "'");
                            ListViewItem li;
                            li = LVledger.Items.Add("");
                            li.SubItems.Add(dt.Rows[i]["Product_Name"].ToString());
                            li.SubItems.Add(dt.Rows[i]["Batchno"].ToString());
                            li.SubItems.Add(dt.Rows[i]["GroupName"].ToString());
                            li.SubItems.Add(dt.Rows[i]["Unit"].ToString());
                            li.SubItems.Add(dt.Rows[i]["Altunit"].ToString());
                            li.SubItems.Add(dt.Rows[i]["OpStock"].ToString());
                            li.SubItems.Add(dt.Rows[i]["SalePrice"].ToString());
                            li.SubItems.Add(dt.Rows[i]["MRP"].ToString());
                            li.SubItems.Add(dt.Rows[i]["taxslab"].ToString());
                            li.SubItems.Add(dt.Rows[i]["Barcode"].ToString());
                            li.SubItems.Add(dt.Rows[i]["companyname"].ToString());
                            li.SubItems.Add(company.Rows[0]["companyname"].ToString());
                            li.SubItems.Add(dt.Rows[i]["Hsn_Sac_Code"].ToString());
                            li.SubItems.Add(dt.Rows[i]["OT1"].ToString());
                        }
                    }
                }
            }
            catch
            {
            }
        }


    }
}
