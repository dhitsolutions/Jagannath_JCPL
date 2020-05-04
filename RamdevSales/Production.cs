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

namespace RamdevSales
{
    public partial class Production : Form
    {
        private Master master;
        private TabControl tabControl;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        DataTable options = new DataTable();
        public static string activecontroal;
        public static string pvc;
        public Production()
        {
            InitializeComponent();
        }

        public Production(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }

        public Production(productionregister productionregister, Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.productionregister = productionregister;
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
            if (keyData == (Keys.Alt | Keys.U))
            {
                DialogResult dr = MessageBox.Show("Do you want to Update?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    enterdata();
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void btncancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }
        int cnt = 0;
        public void binditem()
        {
            SqlCommand cmd = new SqlCommand("select ProductID,Product_Name from ProductMaster where isactive=1 order by Product_Name", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt11 = new DataTable();
            sda.Fill(dt11);

            cmbmainitem.ValueMember = "ProductID";
            cmbmainitem.DisplayMember = "Product_Name";
            cmbmainitem.DataSource = dt11;
            cmbmainitem.SelectedIndex = -1;
        }
        public void bindproductionitem()
        {
            SqlCommand cmd = new SqlCommand("select ProductID,Product_Name from ProductMaster where isactive=1 order by Product_Name", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt11 = new DataTable();
            sda.Fill(dt11);


            cmbitemname.ValueMember = "ProductID";
            cmbitemname.DisplayMember = "Product_Name";
            cmbitemname.DataSource = dt11;
            cmbitemname.SelectedIndex = -1;
        }
        public void bindrowitem()
        {
            SqlCommand cmd = new SqlCommand("select ProductID,Product_Name from ProductMaster where isactive=1 order by Product_Name", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt11 = new DataTable();
            sda.Fill(dt11);


            cmbrowitemname.ValueMember = "ProductID";
            cmbrowitemname.DisplayMember = "Product_Name";
            cmbrowitemname.DataSource = dt11;
            cmbrowitemname.SelectedIndex = -1;

        }
        public void bindprocess()
        {
            SqlCommand cmd = new SqlCommand("select id,processname from tblprocessmaster where isactive=1 and isactiveprocess=1", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt11 = new DataTable();
            sda.Fill(dt11);

            cmbprocessname.ValueMember = "id";
            cmbprocessname.DisplayMember = "processname";
            cmbprocessname.DataSource = dt11;
            cmbprocessname.SelectedIndex = -1;
        }
        DataTable userrights = new DataTable();
        private void Production_Load(object sender, EventArgs e)
        {
            try
            {
                if (cnt == 0)
                {
                    userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[19]["p"].ToString() == "False")
                        {
                            btnprint.Enabled = false;
                        }
                        if (userrights.Rows[19]["d"].ToString() == "False")
                        {
                            btndelete.Enabled = false;
                        }
                    }
                    lvitem.Columns.Add("Item Name", 300, HorizontalAlignment.Center);
                    lvitem.Columns.Add("Qty", 100, HorizontalAlignment.Center);
                    lvitem.Columns.Add("Unit", 100, HorizontalAlignment.Center);
                    lvitem.Columns.Add("Alt Qty", 100, HorizontalAlignment.Center);
                    lvitem.Columns.Add("Unit", 100, HorizontalAlignment.Center);
                    lvrow.Columns.Add("Item Name", 300, HorizontalAlignment.Center);
                    lvrow.Columns.Add("Qty", 100, HorizontalAlignment.Center);
                    lvrow.Columns.Add("Unit", 100, HorizontalAlignment.Center);
                    lvrow.Columns.Add("Alt Qty", 100, HorizontalAlignment.Center);
                    lvrow.Columns.Add("Unit", 100, HorizontalAlignment.Center);
                    this.ActiveControl = TxtRundate;
                    TxtRundate.CustomFormat = Master.dateformate;
                    bindprocess();
                    binditem();
                    bindrowitem();
                    bindproductionitem();
                    production();
                    options = conn.getdataset("select * from options");
                }
            }
            catch
            {
            }
        }

        private void TxtRundate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtvchno.Focus();
                if (options.Rows[0]["productionidtype"].ToString() == "Continuous")
                {
                    txtvchno.Text = Convert.ToString(productionid);
                }
                else
                {
                    //txtvchno.Text = "";
                    this.ActiveControl = txtvchno;
                }
            }
        }

        private void txtvchno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbprocessname.Focus();
            }
        }
        public void getfinishitemqtyunits()
        {
            try
            {
                DataTable proid = conn.getdataset("select * from ProductMaster where isactive=1 and ProductID='" + cmbitemname.SelectedValue + "'");
                lbliaqty.Text = proid.Rows[0]["Unit"].ToString();
                lblipqty.Text = proid.Rows[0]["Altunit"].ToString();
            }
            catch
            {
            }
        }
        public void getrowitemqtyunits()
        {
            try
            {
                DataTable proid = conn.getdataset("select * from ProductMaster where isactive=1 and ProductID='" + cmbrowitemname.SelectedValue + "'");
                lblrowqty.Text = proid.Rows[0]["Unit"].ToString();
                lblrowaqty.Text = proid.Rows[0]["Altunit"].ToString();
            }
            catch
            {
            }
        }
        public void binddata()
        {
            DataTable dt = conn.getdataset("select * from tblprocessmaster where isactive=1 and isactiveprocess=1 and processname='" + cmbprocessname.Text + "'");
            cmbmainitem.Text = dt.Rows[0]["mainitemname"].ToString();
            lblpunit.Text = dt.Rows[0]["munit"].ToString();
            lblaunit.Text = dt.Rows[0]["maunit"].ToString();
            txtpqty.Text = dt.Rows[0]["mqty"].ToString();
            txtaqty.Text = dt.Rows[0]["maqty"].ToString();
        }

        public static string s;
        private void cmbprocessname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbmainitem.Items.Count; i++)
                {
                    s = cmbmainitem.GetItemText(cmbmainitem.Items[i]);
                    if (s == cmbmainitem.Text)
                    {
                        inList = true;
                        cmbmainitem.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbmainitem.Text = "";
                }
                binddata();
                cmbmainitem.Focus();
            }
            if (e.KeyCode == Keys.F3)
            {
                var privouscontroal = cmbprocessname;
                activecontroal = privouscontroal.Name;
                Process popup = new Process(this, tabControl, master, activecontroal);
                master.AddNewTab(popup);
            }
            if (e.KeyCode == Keys.F2)
            {
                if (cmbprocessname.Text != "" && cmbprocessname.Text != null)
                {
                    string group = cmbprocessname.SelectedValue.ToString();
                    if (group == " " || group == null)
                    {
                        MessageBox.Show("Select Process Name Name");
                    }
                    else
                    {
                        var privouscontroal = cmbprocessname;
                        activecontroal = privouscontroal.Name;
                        Process popup = new Process(this, tabControl, master, activecontroal);
                        popup.Updatedata(group);
                        master.AddNewTab(popup);
                        //SqlCommand cmd5 = new SqlCommand("Select * from CompanyMaster where companyname ='"+company+"'", con);
                        //SqlDataAdapter sda = new SqlDataAdapter(cmd5);
                        //DataTable dt = new DataTable();
                        //sda.Fill(dt);
                    }
                }
            }
        }

        private void cmbmainitem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbmainitem.Items.Count; i++)
                {
                    s = cmbmainitem.GetItemText(cmbmainitem.Items[i]);
                    if (s == cmbmainitem.Text)
                    {
                        inList = true;
                        cmbmainitem.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbmainitem.Text = "";
                }
                txtpqty.Focus();
            }

        }

        private void txtpqty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtpqty.Text))
                {
                    txtpqty.Text = "0";
                }
                txtaqty.Focus();
            }
        }
        DataTable qp = new DataTable();
        private void txtaqty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                qp = conn.getdataset("select * from Additional where MasterType='Production'");
                if (qp.Rows.Count > 0)
                {
                    pnladditional.Visible = true;
                    if (qp.Rows[0]["nooffields"].ToString() == "1")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "2")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "3")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "4")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "5")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "6")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "7")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "8")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        lblf8.Visible = true;
                        txtf8.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                            lblf8.Text = qp.Rows[0]["field8"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "9")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        lblf8.Visible = true;
                        txtf8.Visible = true;
                        lblf9.Visible = true;
                        txtf9.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                            lblf8.Text = qp.Rows[0]["field8"].ToString();
                            lblf9.Text = qp.Rows[0]["field9"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "10")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        lblf8.Visible = true;
                        txtf8.Visible = true;
                        lblf9.Visible = true;
                        txtf9.Visible = true;
                        lblf10.Visible = true;
                        txtf10.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                            lblf8.Text = qp.Rows[0]["field8"].ToString();
                            lblf9.Text = qp.Rows[0]["field9"].ToString();
                            lblf10.Text = qp.Rows[0]["field10"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "11")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        lblf8.Visible = true;
                        txtf8.Visible = true;
                        lblf9.Visible = true;
                        txtf9.Visible = true;
                        lblf10.Visible = true;
                        txtf10.Visible = true;
                        lblf11.Visible = true;
                        txtf11.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                            lblf8.Text = qp.Rows[0]["field8"].ToString();
                            lblf9.Text = qp.Rows[0]["field9"].ToString();
                            lblf10.Text = qp.Rows[0]["field10"].ToString();
                            lblf11.Text = qp.Rows[0]["field11"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "12")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        lblf8.Visible = true;
                        txtf8.Visible = true;
                        lblf9.Visible = true;
                        txtf9.Visible = true;
                        lblf10.Visible = true;
                        txtf10.Visible = true;
                        lblf11.Visible = true;
                        txtf11.Visible = true;
                        lblf12.Visible = true;
                        txtf12.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                            lblf8.Text = qp.Rows[0]["field8"].ToString();
                            lblf9.Text = qp.Rows[0]["field9"].ToString();
                            lblf10.Text = qp.Rows[0]["field10"].ToString();
                            lblf11.Text = qp.Rows[0]["field11"].ToString();
                            lblf12.Text = qp.Rows[0]["field12"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "13")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        lblf8.Visible = true;
                        txtf8.Visible = true;
                        lblf9.Visible = true;
                        txtf9.Visible = true;
                        lblf10.Visible = true;
                        txtf10.Visible = true;
                        lblf11.Visible = true;
                        txtf11.Visible = true;
                        lblf12.Visible = true;
                        txtf12.Visible = true;
                        lblf13.Visible = true;
                        txtf13.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                            lblf8.Text = qp.Rows[0]["field8"].ToString();
                            lblf9.Text = qp.Rows[0]["field9"].ToString();
                            lblf10.Text = qp.Rows[0]["field10"].ToString();
                            lblf11.Text = qp.Rows[0]["field11"].ToString();
                            lblf12.Text = qp.Rows[0]["field12"].ToString();
                            lblf13.Text = qp.Rows[0]["field13"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "14")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        lblf8.Visible = true;
                        txtf8.Visible = true;
                        lblf9.Visible = true;
                        txtf9.Visible = true;
                        lblf10.Visible = true;
                        txtf10.Visible = true;
                        lblf11.Visible = true;
                        txtf11.Visible = true;
                        lblf12.Visible = true;
                        txtf12.Visible = true;
                        lblf13.Visible = true;
                        txtf13.Visible = true;
                        lblf14.Visible = true;
                        txtf14.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                            lblf8.Text = qp.Rows[0]["field8"].ToString();
                            lblf9.Text = qp.Rows[0]["field9"].ToString();
                            lblf10.Text = qp.Rows[0]["field10"].ToString();
                            lblf11.Text = qp.Rows[0]["field11"].ToString();
                            lblf12.Text = qp.Rows[0]["field12"].ToString();
                            lblf13.Text = qp.Rows[0]["field13"].ToString();
                            lblf14.Text = qp.Rows[0]["field14"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "15")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        lblf8.Visible = true;
                        txtf8.Visible = true;
                        lblf9.Visible = true;
                        txtf9.Visible = true;
                        lblf10.Visible = true;
                        txtf10.Visible = true;
                        lblf11.Visible = true;
                        txtf11.Visible = true;
                        lblf12.Visible = true;
                        txtf12.Visible = true;
                        lblf13.Visible = true;
                        txtf13.Visible = true;
                        lblf14.Visible = true;
                        txtf14.Visible = true;
                        lblf15.Visible = true;
                        txtf15.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                            lblf8.Text = qp.Rows[0]["field8"].ToString();
                            lblf9.Text = qp.Rows[0]["field9"].ToString();
                            lblf10.Text = qp.Rows[0]["field10"].ToString();
                            lblf11.Text = qp.Rows[0]["field11"].ToString();
                            lblf12.Text = qp.Rows[0]["field12"].ToString();
                            lblf13.Text = qp.Rows[0]["field13"].ToString();
                            lblf14.Text = qp.Rows[0]["field14"].ToString();
                            lblf15.Text = qp.Rows[0]["field15"].ToString();
                        }
                    }
                    txtf1.Focus();
                }
                else
                {
                    if (string.IsNullOrEmpty(txtaqty.Text))
                    {
                        txtaqty.Text = "0";
                    }
                    btnok.Focus();
                }
               
            }
        }

        private void cmbprocessname_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbprocessname.SelectedIndex = 0;
                cmbprocessname.DroppedDown = true;
            }
            catch
            {
            }
        }

        private void cmbprocessname_Leave(object sender, EventArgs e)
        {
            cmbprocessname.Text = s;
        }

        private void cmbprocessname_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool inList = false;
            for (int i = 0; i < cmbprocessname.Items.Count; i++)
            {
                s = cmbprocessname.GetItemText(cmbprocessname.Items[i]);
                if (s == cmbprocessname.Text)
                {
                    inList = true;
                    cmbprocessname.Text = s;
                    break;
                }
            }
            if (!inList)
            {
                cmbprocessname.Text = "";
            }
        }

        private void cmbmainitem_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool inList = false;
            for (int i = 0; i < cmbmainitem.Items.Count; i++)
            {
                s = cmbmainitem.GetItemText(cmbmainitem.Items[i]);
                if (s == cmbmainitem.Text)
                {
                    inList = true;
                    cmbmainitem.Text = s;
                    break;
                }
            }
            if (!inList)
            {
                cmbmainitem.Text = "";
            }
        }
        ListViewItem li;
        private void btnok_Click(object sender, EventArgs e)
        {
            try
            {
                lvitem.Items.Clear();
                lvrow.Items.Clear();
                DataTable dt = conn.getdataset("select * from tblprocessmaster where isactive=1 and isactiveprocess=1 and processname='" + cmbprocessname.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    txtprodes.Text = dt.Rows[0]["processdescription"].ToString();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        li = lvitem.Items.Add(dt.Rows[i]["mainitemname"].ToString());
                        double qty = Convert.ToDouble(dt.Rows[i]["mqty"].ToString()) * Convert.ToDouble(txtpqty.Text);
                        li.SubItems.Add(Convert.ToString(qty));
                        li.SubItems.Add(dt.Rows[i]["munit"].ToString());
                        double aqty = Convert.ToDouble(dt.Rows[i]["maqty"].ToString()) * Convert.ToDouble(txtaqty.Text);
                        li.SubItems.Add(Convert.ToString(aqty));
                        li.SubItems.Add(dt.Rows[i]["maunit"].ToString());
                    }
                    DataTable dt1 = conn.getdataset("select * from tblrowmaterialsmaster where isactive=1 and processid='" + dt.Rows[0]["id"].ToString() + "'");
                    DataTable dt2 = conn.getdataset("select * from tblproductgeneratedmaster where isactive=1 and processid='" + dt.Rows[0]["id"].ToString() + "'");
                    if (dt1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            li = lvrow.Items.Add(dt1.Rows[i]["rowitemname"].ToString());
                            double qty = Convert.ToDouble(dt1.Rows[i]["rowqty"].ToString()) * Convert.ToDouble(txtpqty.Text);
                            li.SubItems.Add(Convert.ToString(qty));
                            li.SubItems.Add(dt1.Rows[i]["rowunit"].ToString());
                            double aqty = Convert.ToDouble(dt1.Rows[i]["rowaqty"].ToString()) * Convert.ToDouble(txtpqty.Text);
                            li.SubItems.Add(Convert.ToString(aqty));
                            li.SubItems.Add(dt1.Rows[i]["rowaunit"].ToString());
                        }
                    }
                    if (dt2.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            li = lvitem.Items.Add(dt2.Rows[i]["proitemname"].ToString());
                            double qty = Convert.ToDouble(dt2.Rows[i]["proqty"].ToString()) * Convert.ToDouble(txtpqty.Text);
                            li.SubItems.Add(Convert.ToString(qty));
                            li.SubItems.Add(dt2.Rows[i]["prounit"].ToString());
                            double aqty = Convert.ToDouble(dt2.Rows[i]["proaqty"].ToString()) * Convert.ToDouble(txtpqty.Text);
                            li.SubItems.Add(Convert.ToString(aqty));
                            li.SubItems.Add(dt2.Rows[i]["proaunit"].ToString());
                        }
                    }


                }




            }
            catch
            {
            }
        }

        private void txtpqty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable proid = conn.getdataset("select * from ProductMaster where isactive=1 and ProductID='" + cmbmainitem.SelectedValue + "'");
                Double aqty = Convert.ToDouble(proid.Rows[0]["Convfactor"].ToString()) * Convert.ToDouble(txtpqty.Text);
                txtaqty.Text = Convert.ToString(aqty);
            }
            catch
            {
            }
        }

        private void cmbitemname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbitemname.Items.Count; i++)
                {
                    s = cmbitemname.GetItemText(cmbitemname.Items[i]);
                    if (s == cmbitemname.Text)
                    {
                        inList = true;
                        cmbitemname.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbitemname.Text = "";
                }
                getfinishitemqtyunits();
                txtiqty.Focus();
            }
        }

        private void txtiqty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtipqty.Focus();
            }
        }

        private void txtipqty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnaddraw.Focus();
            }
        }

        private void txtiqty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable proid = conn.getdataset("select * from ProductMaster where isactive=1 and ProductID='" + cmbitemname.SelectedValue + "'");
                Double aqty = Convert.ToDouble(proid.Rows[0]["Convfactor"].ToString()) * Convert.ToDouble(txtiqty.Text);
                txtipqty.Text = Convert.ToString(aqty);
            }
            catch
            {
            }
        }

        private void cmbrowitemname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbrowitemname.Items.Count; i++)
                {
                    s = cmbrowitemname.GetItemText(cmbrowitemname.Items[i]);
                    if (s == cmbrowitemname.Text)
                    {
                        inList = true;
                        cmbrowitemname.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbrowitemname.Text = "";
                }
                getrowitemqtyunits();
                txtrowqty.Focus();
            }
        }

        private void txtrowqty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtrowaqty.Focus();
            }
        }

        private void txtrowaqty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnrowitemname.Focus();
            }
        }

        private void txtrowqty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable proid = conn.getdataset("select * from ProductMaster where isactive=1 and ProductID='" + cmbrowitemname.SelectedValue + "'");
                Double aqty = Convert.ToDouble(proid.Rows[0]["Convfactor"].ToString()) * Convert.ToDouble(txtrowqty.Text);
                txtrowaqty.Text = Convert.ToString(aqty);
            }
            catch
            {
            }
        }
        Int32 rowid = -1;
        private void btnaddraw_Click(object sender, EventArgs e)
        {
            try
            {
                if (rowid >= 0)
                {
                    lvitem.Items[rowid].SubItems[0].Text = cmbitemname.Text;
                    lvitem.Items[rowid].SubItems[1].Text = txtiqty.Text;
                    lvitem.Items[rowid].SubItems[2].Text = lbliaqty.Text;
                    lvitem.Items[rowid].SubItems[3].Text = txtipqty.Text;
                    lvitem.Items[rowid].SubItems[4].Text = lblipqty.Text;
                    txtipqty.Text = "";
                    txtiqty.Text = "";
                    lbliaqty.Text = "";
                    lblipqty.Text = "";
                    btnaddraw.Text = "Add Item";
                    cmbitemname.Focus();
                    rowid = -1;
                }
                else
                {
                    li = lvitem.Items.Add(cmbitemname.Text);
                    li.SubItems.Add(txtiqty.Text);
                    li.SubItems.Add(lbliaqty.Text);
                    li.SubItems.Add(txtipqty.Text);
                    li.SubItems.Add(lblipqty.Text);
                    cmbitemname.Focus();
                    //cmbitemname.SelectedIndex = -1;
                    txtipqty.Text = "";
                    txtiqty.Text = "";
                    lbliaqty.Text = "";
                    lblipqty.Text = "";
                }
            }
            catch
            {
            }
        }

        private void btnrowitemname_Click(object sender, EventArgs e)
        {
            try
            {
                if (rowid >= 0)
                {
                    lvrow.Items[rowid].SubItems[0].Text = cmbrowitemname.Text;
                    lvrow.Items[rowid].SubItems[1].Text = txtrowqty.Text;
                    lvrow.Items[rowid].SubItems[2].Text = lblrowqty.Text;
                    lvrow.Items[rowid].SubItems[3].Text = txtrowaqty.Text;
                    lvrow.Items[rowid].SubItems[4].Text = lblrowaqty.Text;
                    txtrowqty.Text = "";
                    lblrowqty.Text = "";
                    txtrowaqty.Text = "";
                    lblrowaqty.Text = "";
                    btnrowitemname.Text = "Add Item";
                    cmbrowitemname.Focus();
                    rowid = -1;
                }
                else
                {
                    li = lvrow.Items.Add(cmbrowitemname.Text);
                    li.SubItems.Add(txtrowqty.Text);
                    li.SubItems.Add(lblrowqty.Text);
                    li.SubItems.Add(txtrowaqty.Text);
                    li.SubItems.Add(lblrowaqty.Text);
                    cmbrowitemname.Focus();
                    //cmbitemname.SelectedIndex = -1;
                    txtrowqty.Text = "";
                    lblrowqty.Text = "";
                    txtrowaqty.Text = "";
                    lblrowaqty.Text = "";
                }
            }
            catch
            {
            }
        }

        private void lvitem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Delete Item?", "Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    lvitem.Items[lvitem.FocusedItem.Index].Remove();
                }
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (lvitem.SelectedItems.Count > 0)
                {
                    rowid = lvitem.FocusedItem.Index;
                    cmbitemname.Text = lvitem.Items[lvitem.FocusedItem.Index].SubItems[0].Text;
                    txtiqty.Text = lvitem.Items[lvitem.FocusedItem.Index].SubItems[1].Text;
                    lbliaqty.Text = lvitem.Items[lvitem.FocusedItem.Index].SubItems[2].Text;
                    txtipqty.Text = lvitem.Items[lvitem.FocusedItem.Index].SubItems[3].Text;
                    lblipqty.Text = lvitem.Items[lvitem.FocusedItem.Index].SubItems[4].Text;
                    btnaddraw.Text = "Update";
                    txtiqty.Focus();
                }
            }
        }

        private void lvitem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (lvitem.SelectedItems.Count > 0)
                {
                    rowid = lvitem.FocusedItem.Index;
                    cmbitemname.Text = lvitem.Items[lvitem.FocusedItem.Index].SubItems[0].Text;
                    txtiqty.Text = lvitem.Items[lvitem.FocusedItem.Index].SubItems[1].Text;
                    lbliaqty.Text = lvitem.Items[lvitem.FocusedItem.Index].SubItems[2].Text;
                    txtipqty.Text = lvitem.Items[lvitem.FocusedItem.Index].SubItems[3].Text;
                    lblipqty.Text = lvitem.Items[lvitem.FocusedItem.Index].SubItems[4].Text;
                    btnaddraw.Text = "Update";
                    txtiqty.Focus();
                }
            }
            catch
            {
            }
        }

        private void lvrow_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (lvrow.SelectedItems.Count > 0)
                {
                    rowid = lvrow.FocusedItem.Index;
                    cmbrowitemname.Text = lvrow.Items[lvrow.FocusedItem.Index].SubItems[0].Text;
                    txtrowqty.Text = lvrow.Items[lvrow.FocusedItem.Index].SubItems[1].Text;
                    lblrowqty.Text = lvrow.Items[lvrow.FocusedItem.Index].SubItems[2].Text;
                    txtrowaqty.Text = lvrow.Items[lvrow.FocusedItem.Index].SubItems[3].Text;
                    lblrowaqty.Text = lvrow.Items[lvrow.FocusedItem.Index].SubItems[4].Text;
                    btnrowitemname.Text = "Update";
                    txtrowqty.Focus();
                }
            }
            catch
            {
            }
        }

        private void lvrow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Delete Item?", "Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    lvrow.Items[lvrow.FocusedItem.Index].Remove();
                }
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (lvrow.SelectedItems.Count > 0)
                {
                    rowid = lvrow.FocusedItem.Index;
                    cmbrowitemname.Text = lvrow.Items[lvrow.FocusedItem.Index].SubItems[0].Text;
                    txtrowqty.Text = lvrow.Items[lvrow.FocusedItem.Index].SubItems[1].Text;
                    lblrowqty.Text = lvrow.Items[lvrow.FocusedItem.Index].SubItems[2].Text;
                    txtrowaqty.Text = lvrow.Items[lvrow.FocusedItem.Index].SubItems[3].Text;
                    lblrowaqty.Text = lvrow.Items[lvrow.FocusedItem.Index].SubItems[4].Text;
                    btnrowitemname.Text = "Update";
                    txtrowqty.Focus();
                }
            }
        }

        private void txtpqty_Enter(object sender, EventArgs e)
        {
            try
            {
                DataTable proid = conn.getdataset("select * from ProductMaster where isactive=1 and ProductID='" + cmbmainitem.SelectedValue + "'");
                Double aqty = Convert.ToDouble(proid.Rows[0]["Convfactor"].ToString()) * Convert.ToDouble(txtpqty.Text);
                txtaqty.Text = Convert.ToString(aqty);
            }
            catch
            {
            }
        }

        private void txtiqty_Enter(object sender, EventArgs e)
        {
            try
            {
                DataTable proid = conn.getdataset("select * from ProductMaster where isactive=1 and ProductID='" + cmbitemname.SelectedValue + "'");
                Double aqty = Convert.ToDouble(proid.Rows[0]["Convfactor"].ToString()) * Convert.ToDouble(txtiqty.Text);
                txtipqty.Text = Convert.ToString(aqty);
            }
            catch
            {
            }
        }

        private void txtrowqty_Enter(object sender, EventArgs e)
        {
            try
            {
                DataTable proid = conn.getdataset("select * from ProductMaster where isactive=1 and ProductID='" + cmbrowitemname.SelectedValue + "'");
                Double aqty = Convert.ToDouble(proid.Rows[0]["Convfactor"].ToString()) * Convert.ToDouble(txtrowqty.Text);
                txtrowaqty.Text = Convert.ToString(aqty);
            }
            catch
            {
            }
        }
        public static int productionid;
        private productionregister productionregister;
        public void production()
        {
           
                String str = conn.ExecuteScalar("select max(id) from tblproductionmaster where isactive=1");
                if (str == "")
                {
                    productionid = 1;
                }
                else
                {
                    productionid = Convert.ToInt32(str) + 1;
                }
           
        }
        public void clearall()
        {
            txtvchno.Text = "";
            lvitem.Items.Clear();
            lvrow.Items.Clear();
            cmbprocessname.SelectedIndex = -1;
            cmbmainitem.SelectedIndex = -1;
            txtpqty.Text = "";
            lblpunit.Text = "";
            txtaqty.Text = "";
            lblaunit.Text = "";
            cmbitemname.SelectedIndex = -1;
            txtiqty.Text = "";
            lbliaqty.Text = "";
            txtipqty.Text = "";
            lblipqty.Text = "";
            cmbrowitemname.SelectedIndex = -1;
            txtrowaqty.Text = "";
            lblrowaqty.Text = "";
            txtrowqty.Text = "";
            lblrowqty.Text = "";
            txtprodes.Text = "";
            txtf1.Text = "";
            txtf2.Text = "";
            txtf3.Text = "";
            txtf4.Text = "";
            txtf5.Text = "";
            txtf6.Text = "";
            txtf7.Text = "";
            txtf8.Text = "";
            txtf9.Text = "";
            txtf10.Text = "";
            txtf11.Text = "";
            txtf12.Text = "";
            txtf13.Text = "";
            txtf14.Text = "";
            txtf15.Text = "";
            txtf1.Text = "";
            btnsubmit.Text = "Submit";
        }
        public void enterdata()
        {
            if (btnsubmit.Text == "Update")
            {
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[19]["u"].ToString() == "True")
                    {
                        conn.execute("Update tblfinishedgoodsmaster set isactive='0' where productionid='" + lblproduction.Text + "'");
                        conn.execute("Update tblproductionrawmaterialmaster set isactive='0' where productionid='" + lblproduction.Text + "'");
                        conn.execute("Update [dbo].[tblproductionmaster] SET [date]='" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',[processid]='" + cmbprocessname.SelectedValue + "',[processname]='" + cmbprocessname.Text + "',[mainitemid]='" + cmbmainitem.SelectedValue + "',[mainitemname]='" + cmbmainitem.Text + "',[mqty]='" + txtpqty.Text + "',[munit]='" + lblpunit.Text + "',[maqty]='" + txtaqty.Text + "',[maunit]='" + lblaunit.Text + "',[processdescription]='" + txtprodes.Text + "',[proidmanual]='" + txtvchno.Text + "',[OT1]='" + txtf1.Text + "',[OT2]='" + txtf2.Text + "',[OT3]='" + txtf3.Text + "',[OT4]='" + txtf4.Text + "',[OT5]='" + txtf5.Text + "',[OT6]='" + txtf6.Text + "',[OT7]='" + txtf7.Text + "',[OT8]='" + txtf8.Text + "',[OT9]='" + txtf9.Text + "',[OT10]='" + txtf10.Text + "',[OT11]='" + txtf11.Text + "',[OT12]='" + txtf12.Text + "',[OT13]='" + txtf13.Text + "',[OT14]='" + txtf14.Text + "',[OT15]='" + txtf15.Text + "' where [id]='" + lblproduction.Text + "'");
                        for (int i = 0; i < lvitem.Items.Count; i++)
                        {
                            conn.execute("INSERT INTO [dbo].[tblfinishedgoodsmaster]([productionid],[itemname],[qty],[unit],[aqty],[aunit],[isactive])VALUES('" + lblproduction.Text + "','" + lvitem.Items[i].SubItems[0].Text.Replace(",", "") + "','" + lvitem.Items[i].SubItems[1].Text.Replace(",", "") + "','" + lvitem.Items[i].SubItems[2].Text.Replace(",", "") + "','" + lvitem.Items[i].SubItems[3].Text.Replace(",", "") + "','" + lvitem.Items[i].SubItems[4].Text.Replace(",", "") + "','" + "1" + "')");
                        }
                        for (int i = 0; i < lvrow.Items.Count; i++)
                        {
                            conn.execute("INSERT INTO [dbo].[tblproductionrawmaterialmaster]([productionid],[rawitemname],[rawqty],[rawunit],[rawaqty],[rawaunit],[isactive])VALUES('" + lblproduction.Text + "','" + lvrow.Items[i].SubItems[0].Text.Replace(",", "") + "','" + lvrow.Items[i].SubItems[1].Text.Replace(",", "") + "','" + lvrow.Items[i].SubItems[2].Text.Replace(",", "") + "','" + lvrow.Items[i].SubItems[3].Text.Replace(",", "") + "','" + lvrow.Items[i].SubItems[4].Text.Replace(",", "") + "','" + "1" + "')");
                        }
                        MessageBox.Show("Data Update Successfully.");
                    }
                    else
                    {
                        MessageBox.Show("You don't have Permission To Update");
                        return;
                    }
                }
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[19]["p"].ToString() == "True")
                    {
                        printroduction();
                    }
                    else
                    {
                        MessageBox.Show("You don't have Permission To Print");
                        return;
                    }
                }
                clearall();
            }
            else
            {

                conn.execute("INSERT INTO [dbo].[tblproductionmaster]([date],[processid],[processname],[mainitemid],[mainitemname],[mqty],[munit],[maqty],[maunit],[isactive],[isfinished],[processdescription],[proidmanual],[OT1],[OT2],[OT3],[OT4],[OT5],[OT6],[OT7],[OT8],[OT9],[OT10],[OT11],[OT12],[OT13],[OT14],[OT15])VALUES('" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + cmbprocessname.SelectedValue + "','" + cmbprocessname.Text + "','" + cmbmainitem.SelectedValue + "','" + cmbmainitem.Text + "','" + txtpqty.Text + "','" + lblpunit.Text + "','" + txtaqty.Text + "','" + lblaunit.Text + "','" + "1" + "','" + "0" + "','" + txtprodes.Text + "','" + txtvchno.Text + "','" + txtf1.Text + "','" + txtf2.Text + "','" + txtf3.Text + "','" + txtf4.Text + "','" + txtf5.Text + "','" + txtf6.Text + "','" + txtf7.Text + "','" + txtf8.Text + "','" + txtf9.Text + "','" + txtf10.Text + "','" + txtf11.Text + "','" + txtf12.Text + "','" + txtf13.Text + "','" + txtf14.Text + "','" + txtf15.Text + "')");
                for (int i = 0; i < lvitem.Items.Count; i++)
                {
                    conn.execute("INSERT INTO [dbo].[tblfinishedgoodsmaster]([productionid],[itemname],[qty],[unit],[aqty],[aunit],[isactive])VALUES('" + productionid + "','" + lvitem.Items[i].SubItems[0].Text.Replace(",", "") + "','" + lvitem.Items[i].SubItems[1].Text.Replace(",", "") + "','" + lvitem.Items[i].SubItems[2].Text.Replace(",", "") + "','" + lvitem.Items[i].SubItems[3].Text.Replace(",", "") + "','" + lvitem.Items[i].SubItems[4].Text.Replace(",", "") + "','" + "1" + "')");
                }
                for (int i = 0; i < lvrow.Items.Count; i++)
                {
                    conn.execute("INSERT INTO [dbo].[tblproductionrawmaterialmaster]([productionid],[rawitemname],[rawqty],[rawunit],[rawaqty],[rawaunit],[isactive])VALUES('" + productionid + "','" + lvrow.Items[i].SubItems[0].Text.Replace(",", "") + "','" + lvrow.Items[i].SubItems[1].Text.Replace(",", "") + "','" + lvrow.Items[i].SubItems[2].Text.Replace(",", "") + "','" + lvrow.Items[i].SubItems[3].Text.Replace(",", "") + "','" + lvrow.Items[i].SubItems[4].Text.Replace(",", "") + "','" + "1" + "')");
                }
                MessageBox.Show("Data Inserted Successfully.");
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[19]["p"].ToString() == "True")
                    {
                        printroduction();
                    }
                    else
                    {
                        MessageBox.Show("You don't have Permission To Print");
                        return;
                    }
                }
                clearall();
            }
        }
        private void btnsubmit_Click(object sender, EventArgs e)
        {
            enterdata();
        }

        private void btnok_Enter(object sender, EventArgs e)
        {
            btnok.UseVisualStyleBackColor = false;
            btnok.BackColor = System.Drawing.Color.FromArgb(94, 191, 174);
            btnok.ForeColor = System.Drawing.Color.White;
        }

        private void btnok_Leave(object sender, EventArgs e)
        {
            btnok.UseVisualStyleBackColor = true;
            btnok.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnok.ForeColor = System.Drawing.Color.White;
        }

        private void btnok_MouseEnter(object sender, EventArgs e)
        {
            btnok.UseVisualStyleBackColor = false;
            btnok.BackColor = System.Drawing.Color.FromArgb(94, 191, 174);
            btnok.ForeColor = System.Drawing.Color.White;
        }

        private void btnok_MouseLeave(object sender, EventArgs e)
        {
            btnok.UseVisualStyleBackColor = true;
            btnok.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnok.ForeColor = System.Drawing.Color.White;
        }

        private void btncancel_Enter(object sender, EventArgs e)
        {
            btncancel.UseVisualStyleBackColor = false;
            btncancel.BackColor = Color.FromArgb(248, 152, 94);
            btncancel.ForeColor = Color.White;
        }

        private void btncancel_Leave(object sender, EventArgs e)
        {
            btncancel.UseVisualStyleBackColor = true;
            btncancel.BackColor = Color.FromArgb(51, 153, 255);
            btncancel.ForeColor = Color.White;
        }

        private void btncancel_MouseEnter(object sender, EventArgs e)
        {
            btncancel.UseVisualStyleBackColor = false;
            btncancel.BackColor = Color.FromArgb(248, 152, 94);
            btncancel.ForeColor = Color.White;
        }

        private void btncancel_MouseLeave(object sender, EventArgs e)
        {
            btncancel.UseVisualStyleBackColor = true;
            btncancel.BackColor = Color.FromArgb(51, 153, 255);
            btncancel.ForeColor = Color.White;
        }

        private void btndelete_Enter(object sender, EventArgs e)
        {
            btndelete.UseVisualStyleBackColor = false;
            btndelete.BackColor = Color.FromArgb(255, 77, 77);
            btndelete.ForeColor = Color.White;
        }

        private void btndelete_Leave(object sender, EventArgs e)
        {
            btndelete.UseVisualStyleBackColor = true;
            btndelete.BackColor = Color.FromArgb(51, 153, 255);
            btndelete.ForeColor = Color.White;
        }

        private void btndelete_MouseEnter(object sender, EventArgs e)
        {
            btndelete.UseVisualStyleBackColor = false;
            btndelete.BackColor = Color.FromArgb(255, 77, 77);
            btndelete.ForeColor = Color.White;
        }

        private void btndelete_MouseLeave(object sender, EventArgs e)
        {
            btndelete.UseVisualStyleBackColor = true;
            btndelete.BackColor = Color.FromArgb(51, 153, 255);
            btndelete.ForeColor = Color.White;
        }

        private void btnsubmit_Enter(object sender, EventArgs e)
        {
            btnsubmit.UseVisualStyleBackColor = false;
            btnsubmit.BackColor = Color.YellowGreen;
            btnsubmit.ForeColor = Color.White;
        }

        private void btnsubmit_Leave(object sender, EventArgs e)
        {
            btnsubmit.UseVisualStyleBackColor = true;
            btnsubmit.BackColor = Color.FromArgb(51, 153, 255);
            btnsubmit.ForeColor = Color.White;
        }

        private void btnsubmit_MouseEnter(object sender, EventArgs e)
        {
            btnsubmit.UseVisualStyleBackColor = false;
            btnsubmit.BackColor = Color.YellowGreen;
            btnsubmit.ForeColor = Color.White;
        }

        private void btnsubmit_MouseLeave(object sender, EventArgs e)
        {
            btnsubmit.UseVisualStyleBackColor = true;
            btnsubmit.BackColor = Color.FromArgb(51, 153, 255);
            btnsubmit.ForeColor = Color.White;
        }
        public static string printid;
        internal void Updatedata(string iid)
        {
            btnsubmit.Text = "Update";
            userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[19]["p"].ToString() == "False")
                {
                    btnprint.Enabled = false;
                }
                if (userrights.Rows[19]["d"].ToString() == "False")
                {
                    btndelete.Enabled = false;
                }
            }
            options = conn.getdataset("select * from options");
            cnt = 1;
            lvitem.Columns.Add("Item Name", 300, HorizontalAlignment.Center);
            lvitem.Columns.Add("Qty", 100, HorizontalAlignment.Center);
            lvitem.Columns.Add("Unit", 100, HorizontalAlignment.Center);
            lvitem.Columns.Add("Alt Qty", 100, HorizontalAlignment.Center);
            lvitem.Columns.Add("Unit", 100, HorizontalAlignment.Center);
            lvrow.Columns.Add("Item Name", 300, HorizontalAlignment.Center);
            lvrow.Columns.Add("Qty", 100, HorizontalAlignment.Center);
            lvrow.Columns.Add("Unit", 100, HorizontalAlignment.Center);
            lvrow.Columns.Add("Alt Qty", 100, HorizontalAlignment.Center);
            lvrow.Columns.Add("Unit", 100, HorizontalAlignment.Center);
            this.ActiveControl = TxtRundate;
            TxtRundate.CustomFormat = Master.dateformate;
            bindprocess();
            binditem();
            bindrowitem();
            bindproductionitem();
            production();
            DataTable p = conn.getdataset("select * from tblproductionmaster where isactive=1 and id='" + iid + "'");
            DataTable r = conn.getdataset("select * from tblfinishedgoodsmaster where isactive=1 and productionid='" + iid + "'");
            DataTable s = conn.getdataset("select * from tblproductionrawmaterialmaster where isactive=1 and productionid='" + iid + "'");

            TxtRundate.Text = Convert.ToDateTime(p.Rows[0]["date"].ToString()).ToString(Master.dateformate);
            if (options.Rows[0]["productionidtype"].ToString() == "Continuous")
            {
                txtvchno.Text = p.Rows[0]["id"].ToString();
            }
            else
            {
                txtvchno.Text = p.Rows[0]["proidmanual"].ToString();
            }
            txtf1.Text = p.Rows[0]["OT1"].ToString();
            txtf2.Text = p.Rows[0]["OT2"].ToString();
            txtf3.Text = p.Rows[0]["OT3"].ToString();
            txtf4.Text = p.Rows[0]["OT4"].ToString();
            txtf5.Text = p.Rows[0]["OT5"].ToString();
            txtf6.Text = p.Rows[0]["OT6"].ToString();
            txtf7.Text = p.Rows[0]["OT7"].ToString();
            txtf8.Text = p.Rows[0]["OT8"].ToString();
            txtf9.Text = p.Rows[0]["OT9"].ToString();
            txtf10.Text = p.Rows[0]["OT10"].ToString();
            txtf11.Text = p.Rows[0]["OT11"].ToString();
            txtf12.Text = p.Rows[0]["OT12"].ToString();
            txtf13.Text = p.Rows[0]["OT13"].ToString();
            txtf14.Text = p.Rows[0]["OT14"].ToString();
            txtf15.Text = p.Rows[0]["OT15"].ToString();
            cmbprocessname.Text = p.Rows[0]["processname"].ToString();
            cmbmainitem.Text = p.Rows[0]["mainitemname"].ToString();
            txtpqty.Text = p.Rows[0]["mqty"].ToString();
            lblpunit.Text = p.Rows[0]["munit"].ToString();
            txtaqty.Text = p.Rows[0]["maqty"].ToString();
            lblaunit.Text = p.Rows[0]["maunit"].ToString();
            txtprodes.Text = p.Rows[0]["processdescription"].ToString();
            for (int i = 0; i < r.Rows.Count; i++)
            {
                ListViewItem li;
                li = lvitem.Items.Add(r.Rows[i]["itemname"].ToString());
                li.SubItems.Add(r.Rows[i]["qty"].ToString());
                li.SubItems.Add(r.Rows[i]["unit"].ToString());
                li.SubItems.Add(r.Rows[i]["aqty"].ToString());
                li.SubItems.Add(r.Rows[i]["aunit"].ToString());
            }
            for (int i = 0; i < s.Rows.Count; i++)
            {
                ListViewItem li;
                li = lvrow.Items.Add(s.Rows[i]["rawitemname"].ToString());
                li.SubItems.Add(s.Rows[i]["rawqty"].ToString());
                li.SubItems.Add(s.Rows[i]["rawunit"].ToString());
                li.SubItems.Add(s.Rows[i]["rawaqty"].ToString());
                li.SubItems.Add(s.Rows[i]["rawaunit"].ToString());
            }
            
            lblproduction.Text = iid;
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            DialogResult dr1 = MessageBox.Show("Do you want to Delete Production?", "Production", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr1 == DialogResult.Yes)
            {
                conn.execute("Update tblproductionrawmaterialmaster set isactive='0' where productionid='" + lblproduction.Text + "'");
                conn.execute("Update tblfinishedgoodsmaster set isactive='0' where productionid='" + lblproduction.Text + "'");
                conn.execute("Update tblproductionmaster set isactive='0' where id='" + lblproduction.Text + "'");
                MessageBox.Show("Data Delete Successfully.");
                clearall();
            }
        }

        private void btnaddpname_Click(object sender, EventArgs e)
        {
            var privouscontroal = cmbprocessname;
            activecontroal = privouscontroal.Name;
            Process popup = new Process(this, tabControl, master, activecontroal);
            master.AddNewTab(popup);
        }

        private void btneditpname_Click(object sender, EventArgs e)
        {
            if (cmbprocessname.Text != "" && cmbprocessname.Text != null)
            {
                string group = cmbprocessname.SelectedValue.ToString();
                if (group == " " || group == null)
                {
                    MessageBox.Show("Select Process Name");
                }
                else
                {
                    var privouscontroal = cmbprocessname;
                    activecontroal = privouscontroal.Name;
                    Process popup = new Process(this, tabControl, master, activecontroal);
                    popup.Updatedata(group);
                    master.AddNewTab(popup);
                    //SqlCommand cmd5 = new SqlCommand("Select * from CompanyMaster where companyname ='"+company+"'", con);
                    //SqlDataAdapter sda = new SqlDataAdapter(cmd5);
                    //DataTable dt = new DataTable();
                    //sda.Fill(dt);
                }
            }
            else
            {
                MessageBox.Show("Select Process Name");
            }
        }

        private void cmbmainitem_Leave(object sender, EventArgs e)
        {
            cmbmainitem.Text = s;
        }

        private void cmbitemname_Leave(object sender, EventArgs e)
        {
            cmbitemname.Text = s;
        }

        private void cmbrowitemname_Leave(object sender, EventArgs e)
        {
            cmbrowitemname.Text = s;
        }

        private void cmbitemname_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool inList = false;
            for (int i = 0; i < cmbitemname.Items.Count; i++)
            {
                s = cmbitemname.GetItemText(cmbitemname.Items[i]);
                if (s == cmbitemname.Text)
                {
                    inList = true;
                    cmbitemname.Text = s;
                    break;
                }
            }
            if (!inList)
            {
                cmbitemname.Text = "";
            }
        }

        private void cmbrowitemname_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool inList = false;
            for (int i = 0; i < cmbrowitemname.Items.Count; i++)
            {
                s = cmbrowitemname.GetItemText(cmbrowitemname.Items[i]);
                if (s == cmbrowitemname.Text)
                {
                    inList = true;
                    cmbrowitemname.Text = s;
                    break;
                }
            }
            if (!inList)
            {
                cmbrowitemname.Text = "";
            }
        }

        private void btnaddraw_Enter(object sender, EventArgs e)
        {
            btnaddraw.UseVisualStyleBackColor = false;
            btnaddraw.BackColor = Color.FromArgb(9, 106,3);
            btnaddraw.ForeColor = Color.White;

        }

        private void btnaddraw_Leave(object sender, EventArgs e)
        {
            btnaddraw.UseVisualStyleBackColor = true;
            btnaddraw.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnaddraw.ForeColor = System.Drawing.Color.White;
        }

        private void btnaddraw_MouseEnter(object sender, EventArgs e)
        {
            btnaddraw.UseVisualStyleBackColor = false;
            btnaddraw.BackColor = Color.FromArgb(9, 106, 3);
            btnaddraw.ForeColor = Color.White;
        }

        private void btnaddraw_MouseLeave(object sender, EventArgs e)
        {
            btnaddraw.UseVisualStyleBackColor = true;
            btnaddraw.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnaddraw.ForeColor = System.Drawing.Color.White;
        }

        private void btnrowitemname_Enter(object sender, EventArgs e)
        {
            btnrowitemname.UseVisualStyleBackColor = false;
            btnrowitemname.BackColor = Color.FromArgb(9, 106, 3);
            btnrowitemname.ForeColor = Color.White;
        }

        private void btnrowitemname_Leave(object sender, EventArgs e)
        {
            btnrowitemname.UseVisualStyleBackColor = true;
            btnrowitemname.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnrowitemname.ForeColor = System.Drawing.Color.White;
        }

        private void btnrowitemname_MouseEnter(object sender, EventArgs e)
        {
            btnrowitemname.UseVisualStyleBackColor = false;
            btnrowitemname.BackColor = Color.FromArgb(9, 106, 3);
            btnrowitemname.ForeColor = Color.White;
        }

        private void btnrowitemname_MouseLeave(object sender, EventArgs e)
        {
            btnrowitemname.UseVisualStyleBackColor = true;
            btnrowitemname.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnrowitemname.ForeColor = System.Drawing.Color.White;
        }
        Printing prn = new Printing();
        public void printroduction()
        {
            try
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Print Production?", "Production", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    if (btnsubmit.Text != "Update")
                    {
                        printid = txtvchno.Text;
                    }
                    prn.execute("delete from printing");
                    string status;
                    status = "PRODUCTION REGISTER";
                    string inamerow = "", pqtyrow = "", aqtyrow = "", punitrow = "", aunitrow = "", inamepro = "", pqtypro = "", aqtypro = "", punitpro = "", aunitpro = "";
                    for (int i = 0; i < lvitem.Items.Count; i++)
                    {
                        inamerow += Environment.NewLine + lvitem.Items[i].SubItems[0].Text;
                        pqtyrow += Environment.NewLine + lvitem.Items[i].SubItems[1].Text;
                        aqtyrow += Environment.NewLine + lvitem.Items[i].SubItems[2].Text;
                        punitrow += Environment.NewLine + lvitem.Items[i].SubItems[3].Text;
                        aunitrow += Environment.NewLine + lvitem.Items[i].SubItems[4].Text;
                    }
                    for (int i = 0; i < lvrow.Items.Count; i++)
                    {
                        inamepro += Environment.NewLine + lvrow.Items[i].SubItems[0].Text;
                        pqtypro += Environment.NewLine + lvrow.Items[i].SubItems[1].Text;
                        aqtypro += Environment.NewLine + lvrow.Items[i].SubItems[2].Text;
                        punitpro += Environment.NewLine + lvrow.Items[i].SubItems[3].Text;
                        aunitpro += Environment.NewLine + lvrow.Items[i].SubItems[4].Text;
                    }
                    DataTable dt = conn.getdataset("select * from tblproductionmaster where isactive=1 and id='" + printid + "'");
                    DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1 and CompanyID='" + Master.companyId + "' ");
                    string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34,T35,T36,T37,T38,T39,T40,T41,T42,T43,T44,T45,T46)VALUES";
                    qry += "('" + dt1.Rows[0]["CompanyName"].ToString() + "','" + dt1.Rows[0]["SubName"].ToString() + "','" + dt1.Rows[0]["Address"].ToString() + "','" + dt1.Rows[0]["Address2"].ToString() + "','" + dt1.Rows[0]["City"].ToString() + "','" + dt1.Rows[0]["State"].ToString() + "','" + dt1.Rows[0]["Country"].ToString() + "','" + dt1.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Mobile"].ToString() + "','" + dt1.Rows[0]["Email"].ToString() + "','" + dt1.Rows[0]["CSTNo"].ToString() + "','" + status + "','" + dt1.Rows[0]["Website"].ToString() + "','" + inamerow + "','" + pqtyrow + "','" + aqtyrow + "','" + punitrow + "','" + aunitrow + "','" + inamepro + "','" + pqtypro + "','" + aqtypro + "','" + punitpro + "','" + aunitpro + "','" + cmbprocessname.Text + "','" + cmbmainitem.Text + "','" + txtpqty.Text + "','" + txtaqty.Text + "','" + txtprodes.Text + "','" + lblpunit.Text + "','" + lblaunit.Text + "','" + txtvchno.Text + "','" + dt.Rows[0]["OT1"].ToString() + "','" + dt.Rows[0]["OT2"].ToString() + "','" + dt.Rows[0]["OT3"].ToString() + "','" + dt.Rows[0]["OT4"].ToString() + "','" + dt.Rows[0]["OT5"].ToString() + "','" + dt.Rows[0]["OT6"].ToString() + "','" + dt.Rows[0]["OT7"].ToString() + "','" + dt.Rows[0]["OT8"].ToString() + "','" + dt.Rows[0]["OT9"].ToString() + "','" + dt.Rows[0]["OT10"].ToString() + "','" + dt.Rows[0]["OT11"].ToString() + "','" + dt.Rows[0]["OT12"].ToString() + "','" + dt.Rows[0]["OT13"].ToString() + "','" + dt.Rows[0]["OT14"].ToString() + "','" + dt.Rows[0]["OT15"].ToString() + "')";
                    prn.execute(qry);

                    string reportName = "Production";
                    Print popup = new Print(reportName);
                    popup.ShowDialog();
                    popup.Dispose();
                }
            }
            catch
            {
            }
        }
        private void btnprint_Click(object sender, EventArgs e)
        {
            printroduction();
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

        private void txtf1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "1")
                {
                    btnok.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf2.Focus();
                }
            }
        }

        private void txtf2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "2")
                {
                    btnok.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf3.Focus();
                }
            }
        }

        private void txtf3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "3")
                {
                    btnok.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf4.Focus();
                }
            }
        }

        private void txtf4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "4")
                {
                    btnok.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf5.Focus();
                }
            }
        }

        private void txtf5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "5")
                {
                    btnok.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf6.Focus();
                }
            }
        }

        private void txtf6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "6")
                {
                    btnok.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf7.Focus();
                }
            }
        }

        private void txtf7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "7")
                {
                    btnok.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf8.Focus();
                }
            }
        }

        private void txtf8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "8")
                {
                    btnok.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf9.Focus();
                }
            }
        }

        private void txtf9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "9")
                {
                    btnok.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf10.Focus();
                }
            }
        }

        private void txtf10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "10")
                {
                    btnok.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf11.Focus();
                }
            }
        }

        private void txtf11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "11")
                {
                    btnok.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf12.Focus();
                }
            }
        }

        private void txtf12_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "12")
                {
                    btnok.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf13.Focus();
                }
            }
        }

        private void txtf13_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "13")
                {
                    btnok.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf14.Focus();
                }
            }
        }

        private void txtf14_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "14")
                {
                    btnok.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf15.Focus();
                }
            }
        }

        private void txtf15_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnok.Focus();
                pnladditional.Visible = false;
            }
        }


    }
}
