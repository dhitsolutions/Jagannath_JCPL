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
    public partial class Process : Form
    {
        private Master master;
        private TabControl tabControl;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        public static string pvc;
        int p = 0;
        public Process()
        {
            InitializeComponent();
        }

        public Process(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }

        public Process(ProcessList processList, Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.processList = processList;
            this.master = master;
            this.tabControl = tabControl;
        }

        public Process(Production production, TabControl tabControl, Master master, string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.production = production;
            this.tabControl = tabControl;
            this.master = master;
            pvc = activecontroal;
            p = 1;
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
        public void bindrowitem()
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
        public void bindproductionitem()
        {
            SqlCommand cmd = new SqlCommand("select ProductID,Product_Name from ProductMaster where isactive=1 order by Product_Name", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt11 = new DataTable();
            sda.Fill(dt11);


            cmbitemnamebyproduct.ValueMember = "ProductID";
            cmbitemnamebyproduct.DisplayMember = "Product_Name";
            cmbitemnamebyproduct.DataSource = dt11;
            cmbitemnamebyproduct.SelectedIndex = -1;
        }
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
        int cnt = 0;
        DataTable userrights = new DataTable();
        private void Process_Load(object sender, EventArgs e)
        {
            try
            {
                if (cnt == 0)
                {
                    userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                    if (userrights.Rows.Count > 0)
                    {
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
                    lvproduct.Columns.Add("Item Name", 300, HorizontalAlignment.Center);
                    lvproduct.Columns.Add("Qty", 100, HorizontalAlignment.Center);
                    lvproduct.Columns.Add("Unit", 100, HorizontalAlignment.Center);
                    lvproduct.Columns.Add("Alt Qty", 100, HorizontalAlignment.Center);
                    lvproduct.Columns.Add("Unit", 100, HorizontalAlignment.Center);
                    binditem();
                    bindproductionitem();
                    bindrowitem();
                    this.ActiveControl = txtprocessname;
                }
            }
            catch
            {
            }
        }

        private void txtprocessname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbmainitem.Focus();
            }
        }

        private void cmbmainitem_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbmainitem.SelectedIndex = 0;
                cmbmainitem.DroppedDown = true;
            }
            catch
            {
            }
        }
        public static string s;
        public void getmainitemqtyunits()
        {
            DataTable proid = conn.getdataset("select * from ProductMaster where isactive=1 and ProductID='" + cmbmainitem.SelectedValue + "'");
            lblpunit.Text = proid.Rows[0]["Unit"].ToString();
            lblaunit.Text = proid.Rows[0]["Altunit"].ToString();
        }
        public void getrowitemtqtyunits()
        {
            DataTable proid = conn.getdataset("select * from ProductMaster where isactive=1 and ProductID='" + cmbitemname.SelectedValue + "'");
            lbliaqty.Text = proid.Rows[0]["Unit"].ToString();
            lblipqty.Text = proid.Rows[0]["Altunit"].ToString();
        }
        public void getproductitemtqtyunits()
        {
            DataTable proid = conn.getdataset("select * from ProductMaster where isactive=1 and ProductID='" + cmbitemnamebyproduct.SelectedValue + "'");
            lblproqty.Text = proid.Rows[0]["Unit"].ToString();
            lblproaqty.Text = proid.Rows[0]["Altunit"].ToString();
        }
        public static string activecontroal;
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
                getmainitemqtyunits();
            }
            if (e.KeyCode == Keys.F3)
            {
                var privouscontroal = cmbmainitem;
                activecontroal = privouscontroal.Name;
                Itementry client = new Itementry(this, master, tabControl, activecontroal);

                client.Passed(1);
                //client.Show();

                master.AddNewTab(client);

                // autoreaderbind();
                cmbmainitem.Focus();

            }
            if (e.KeyCode == Keys.F2)
            {
                try
                {
                    var privouscontroal = cmbmainitem;
                    activecontroal = privouscontroal.Name;
                    Itementry client = new Itementry(this, master, tabControl, activecontroal);
                    client.Updatefromsale(cmbmainitem.Text);
                    client.Passed(1);
                    //client.Show();
                    master.AddNewTab(client);
                }
                catch
                {
                }
            }
        }

        private void cmbmainitem_Leave(object sender, EventArgs e)
        {
            cmbmainitem.Text = s;
        }

        private void cmbmainitem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception excp)
            {
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

        private void txtaqty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtaqty.Text))
                {
                    txtaqty.Text = "0";
                }
                cmbitemname.Focus();
            }
        }

        private void cmbitemname_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbitemname.SelectedIndex = 0;
                cmbitemname.DroppedDown = true;
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
                txtiqty.Focus();
                getrowitemtqtyunits();
            }
            if (e.KeyCode == Keys.F3)
            {
                var privouscontroal = cmbitemname;
                activecontroal = privouscontroal.Name;
                Itementry client = new Itementry(this, master, tabControl, activecontroal);

                client.Passed(1);
                //client.Show();

                master.AddNewTab(client);

                // autoreaderbind();
                cmbitemname.Focus();

            }
            if (e.KeyCode == Keys.F2)
            {
                try
                {
                    var privouscontroal = cmbitemname;
                    activecontroal = privouscontroal.Name;
                    Itementry client = new Itementry(this, master, tabControl, activecontroal);
                    client.Updatefromsale(cmbitemname.Text);
                    client.Passed(1);
                    //client.Show();
                    master.AddNewTab(client);
                }
                catch
                {
                }
            }
        }

        private void cmbitemname_Leave(object sender, EventArgs e)
        {
            cmbitemname.Text = s;
        }

        private void cmbitemname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception excp)
            {
            }
        }

        private void txtiqty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtiqty.Text))
                {
                    txtiqty.Text = "0";
                }
                txtipqty.Focus();
            }
        }

        private void txtipqty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtipqty.Text))
                {
                    txtipqty.Text = "0";
                }
                btnaddraw.Focus();
            }
        }

        private void cmbitemnamebyproduct_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbitemnamebyproduct.SelectedIndex = 0;
                cmbitemnamebyproduct.DroppedDown = true;
            }
            catch
            {
            }
        }

        private void cmbitemnamebyproduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbitemnamebyproduct.Items.Count; i++)
                {
                    s = cmbitemnamebyproduct.GetItemText(cmbitemnamebyproduct.Items[i]);
                    if (s == cmbitemnamebyproduct.Text)
                    {
                        inList = true;
                        cmbitemnamebyproduct.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbitemnamebyproduct.Text = "";
                }
                txtproqty.Focus();
                getproductitemtqtyunits();
            }
            if (e.KeyCode == Keys.F3)
            {
                var privouscontroal = cmbitemnamebyproduct;
                activecontroal = privouscontroal.Name;
                Itementry client = new Itementry(this, master, tabControl, activecontroal);

                client.Passed(1);
                //client.Show();

                master.AddNewTab(client);

                // autoreaderbind();
                cmbitemnamebyproduct.Focus();

            }
            if (e.KeyCode == Keys.F2)
            {
                try
                {
                    var privouscontroal = cmbitemnamebyproduct;
                    activecontroal = privouscontroal.Name;
                    Itementry client = new Itementry(this, master, tabControl, activecontroal);
                    client.Updatefromsale(cmbitemname.Text);
                    client.Passed(1);
                    //client.Show();
                    master.AddNewTab(client);
                }
                catch
                {
                }
            }
        }

        private void cmbitemnamebyproduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                bool inList = false;
                for (int i = 0; i < cmbitemnamebyproduct.Items.Count; i++)
                {
                    s = cmbitemnamebyproduct.GetItemText(cmbitemnamebyproduct.Items[i]);
                    if (s == cmbitemnamebyproduct.Text)
                    {
                        inList = true;
                        cmbitemnamebyproduct.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbitemnamebyproduct.Text = "";
                }

            }
            catch (Exception excp)
            {
            }
        }

        private void cmbitemnamebyproduct_Leave(object sender, EventArgs e)
        {
            cmbitemnamebyproduct.Text = s;
        }

        private void txtproqty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtproqty.Text))
                {
                    txtproqty.Text = "0";
                }
                txtproaqty.Focus();
            }
        }

        private void txtproaqty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtproaqty.Text))
                {
                    txtproaqty.Text = "0";
                }
                btnaddproduct.Focus();
            }
        }

        private void txtpqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtaqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtiqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtipqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtproqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtproaqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
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
                    ListViewItem li;
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

        private void btnaddproduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (rowid >= 0)
                {
                    lvproduct.Items[rowid].SubItems[0].Text = cmbitemnamebyproduct.Text;
                    lvproduct.Items[rowid].SubItems[1].Text = txtproqty.Text;
                    lvproduct.Items[rowid].SubItems[2].Text = lblproqty.Text;
                    lvproduct.Items[rowid].SubItems[3].Text = txtproaqty.Text;
                    lvproduct.Items[rowid].SubItems[4].Text = lblproaqty.Text;
                    txtproaqty.Text = "";
                    txtproqty.Text = "";
                    lblproaqty.Text = "";
                    lblproqty.Text = "";
                    btnaddproduct.Text = "Add Item";
                    cmbitemnamebyproduct.Focus();
                    rowid = -1;
                }
                else
                {
                    ListViewItem li;
                    li = lvproduct.Items.Add(cmbitemnamebyproduct.Text);
                    li.SubItems.Add(txtproqty.Text);
                    li.SubItems.Add(lblproqty.Text);
                    li.SubItems.Add(txtproaqty.Text);
                    li.SubItems.Add(lblproaqty.Text);
                    cmbitemnamebyproduct.Focus();
                    //cmbitemnamebyproduct.SelectedIndex = -1;
                    txtproaqty.Text = "";
                    txtproqty.Text = "";
                    lblproaqty.Text = "";
                    lblproqty.Text = "";
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

        private void txtproqty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable proid = conn.getdataset("select * from ProductMaster where isactive=1 and ProductID='" + cmbitemnamebyproduct.SelectedValue + "'");
                Double aqty = Convert.ToDouble(proid.Rows[0]["Convfactor"].ToString()) * Convert.ToDouble(txtproqty.Text);
                txtproaqty.Text = Convert.ToString(aqty);
            }
            catch
            {
            }
        }
        public static int processid;
        private ProcessList processList;
        public void prcessid()
        {
            String str = conn.ExecuteScalar("select max(id) from tblprocessmaster where isactive=1");
            if (str == "")
            {
                processid = 1;
            }
            else
            {
                processid = Convert.ToInt32(str) + 1;
            }
        }
        public void enterdata()
        {
            try
            {
                if (btnsubmit.Text == "Update")
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[19]["u"].ToString() == "True")
                        {
                            if (lvitem.Items.Count > 0)
                            {
                                string activeprocess;
                                if (chkdisactive.Checked == true)
                                {
                                    activeprocess = "0";
                                }
                                else
                                {
                                    activeprocess = "1";
                                }
                                conn.execute("Update tblrowmaterialsmaster set isactive='0' where processid='" + lblprocessno.Text + "'");
                                conn.execute("Update tblproductgeneratedmaster set isactive='0' where processid='" + lblprocessno.Text + "'");
                                conn.execute("UPDATE [dbo].[tblprocessmaster] SET [processname]='" + txtprocessname.Text + "', [mainitemID]='" + cmbmainitem.SelectedValue + "',[mainitemname]='" + cmbmainitem.Text + "',[mqty]='" + txtpqty.Text + "',[munit]='" + lblpunit.Text + "',[maqty]='" + txtaqty.Text + "',[maunit]='" + lblaunit.Text + "',[isactiveprocess]='" + activeprocess + "',[processdescription]='" + txtprodes.Text + "' where [id]='" + lblprocessno.Text + "'");
                                for (int i = 0; i < lvitem.Items.Count; i++)
                                {
                                    conn.execute("INSERT INTO [dbo].[tblrowmaterialsmaster]([processid],[rowitemname],[rowqty],[rowunit],[rowaqty],[rowaunit],[isactive])VALUES('" + lblprocessno.Text + "','" + lvitem.Items[i].SubItems[0].Text.Replace(",", "") + "','" + lvitem.Items[i].SubItems[1].Text.Replace(",", "") + "','" + lvitem.Items[i].SubItems[2].Text.Replace(",", "") + "','" + lvitem.Items[i].SubItems[3].Text.Replace(",", "") + "','" + lvitem.Items[i].SubItems[4].Text.Replace(",", "") + "','" + "1" + "')");
                                }
                                for (int i = 0; i < lvproduct.Items.Count; i++)
                                {
                                    conn.execute("INSERT INTO [dbo].[tblproductgeneratedmaster]([processid],[proitemname],[proqty],[prounit],[proaqty],[proaunit],[isactive])VALUES('" + lblprocessno.Text + "','" + lvproduct.Items[i].SubItems[0].Text.Replace(",", "") + "','" + lvproduct.Items[i].SubItems[1].Text.Replace(",", "") + "','" + lvproduct.Items[i].SubItems[2].Text.Replace(",", "") + "','" + lvproduct.Items[i].SubItems[3].Text.Replace(",", "") + "','" + lvproduct.Items[i].SubItems[4].Text.Replace(",", "") + "','" + "1" + "')");
                                }
                                MessageBox.Show("Data Update Successfully.");
                                if (p == 1)
                                {
                                    production.bindprocess();
                                }
                                if (string.IsNullOrEmpty(pvc) == true)
                                {
                                    master.RemoveCurrentTab();
                                }
                                else
                                {
                                    master.RemoveCurrentTab1(pvc, txtprocessname.Text);
                                }
                                p = 0;
                                if (userrights.Rows.Count > 0)
                                {
                                    if (userrights.Rows[19]["p"].ToString() == "True")
                                    {
                                        printprocess();
                                    }
                                    else
                                    {
                                        MessageBox.Show("You don't have Permission To Print");
                                        return;
                                    }
                                }
                                clearall();
                                btnsubmit.Text = "Submit";

                            }
                            else
                            {
                                MessageBox.Show("No Item To Update.");
                                txtprocessname.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("You don't have Permission To Update");
                            return;
                        }
                    }
                }
                else
                {
                    if (lvitem.Items.Count > 0)
                    {
                        prcessid();
                        string activeprocess;
                        if (chkdisactive.Checked == true)
                        {
                            activeprocess = "0";
                        }
                        else
                        {
                            activeprocess = "1";
                        }
                        conn.execute("INSERT INTO [dbo].[tblprocessmaster]([processname],[mainitemID],[mainitemname],[mqty],[munit],[maqty],[maunit],[isactiveprocess],[isactive],[processdescription])VALUES('" + txtprocessname.Text + "','" + cmbmainitem.SelectedValue + "','" + cmbmainitem.Text + "','" + txtpqty.Text + "','" + lblpunit.Text + "','" + txtaqty.Text + "','" + lblaunit.Text + "','" + activeprocess + "','" + "1" + "','" + txtprodes.Text + "')");
                        for (int i = 0; i < lvitem.Items.Count; i++)
                        {
                            conn.execute("INSERT INTO [dbo].[tblrowmaterialsmaster]([processid],[rowitemname],[rowqty],[rowunit],[rowaqty],[rowaunit],[isactive])VALUES('" + processid + "','" + lvitem.Items[i].SubItems[0].Text.Replace(",", "") + "','" + lvitem.Items[i].SubItems[1].Text.Replace(",", "") + "','" + lvitem.Items[i].SubItems[2].Text.Replace(",", "") + "','" + lvitem.Items[i].SubItems[3].Text.Replace(",", "") + "','" + lvitem.Items[i].SubItems[4].Text.Replace(",", "") + "','" + "1" + "')");
                        }
                        for (int i = 0; i < lvproduct.Items.Count; i++)
                        {
                            conn.execute("INSERT INTO [dbo].[tblproductgeneratedmaster]([processid],[proitemname],[proqty],[prounit],[proaqty],[proaunit],[isactive])VALUES('" + processid + "','" + lvproduct.Items[i].SubItems[0].Text.Replace(",", "") + "','" + lvproduct.Items[i].SubItems[1].Text.Replace(",", "") + "','" + lvproduct.Items[i].SubItems[2].Text.Replace(",", "") + "','" + lvproduct.Items[i].SubItems[3].Text.Replace(",", "") + "','" + lvproduct.Items[i].SubItems[4].Text.Replace(",", "") + "','" + "1" + "')");
                        }
                        MessageBox.Show("Data Inserted Successfully.");
                        if (p == 1)
                        {
                            production.bindprocess();
                        }
                        if (string.IsNullOrEmpty(pvc) == true)
                        {
                            master.RemoveCurrentTab();
                        }
                        else
                        {
                            master.RemoveCurrentTab1(pvc, txtprocessname.Text);
                        }
                        p = 0;
                        if (userrights.Rows.Count > 0)
                        {
                            if (userrights.Rows[19]["p"].ToString() == "True")
                            {
                                printprocess();
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
                        MessageBox.Show("No Item To Save.");
                        txtprocessname.Focus();
                    }
                }
            }
            catch
            {
            }
        }
        public void clearall()
        {
            txtprocessname.Text = "";
            cmbmainitem.SelectedIndex = -1;
            txtpqty.Text = "";
            lblpunit.Text = "";
            txtaqty.Text = "";
            lblaunit.Text = "";
            lvitem.Items.Clear();
            lvproduct.Items.Clear();
            txtipqty.Text = "";
            txtiqty.Text = "";
            lbliaqty.Text = "";
            lblipqty.Text = "";
            cmbitemnamebyproduct.SelectedIndex = -1;
            txtproaqty.Text = "";
            txtproqty.Text = "";
            lblproaqty.Text = "";
            lblproqty.Text = "";
            cmbitemname.SelectedIndex = -1;
            txtprodes.Text = "";
            btnsubmit.Text = "Submit";
        }
        private void btnsubmit_Click(object sender, EventArgs e)
        {
            enterdata();
        }

        internal void Updatedata(string iid)
        {
            try
            {
                cnt = 1;
                userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[19]["d"].ToString() == "False")
                    {
                        btndelete.Enabled = false;
                    }
                }
                DataTable p = conn.getdataset("select * from tblprocessmaster where isactive=1 and id='" + iid + "'");
                DataTable r = conn.getdataset("select * from tblrowmaterialsmaster where isactive=1 and processid='" + iid + "'");
                DataTable s = conn.getdataset("select * from tblproductgeneratedmaster where isactive=1 and processid='" + iid + "'");
                lvitem.Columns.Add("Item Name", 300, HorizontalAlignment.Center);
                lvitem.Columns.Add("Qty", 100, HorizontalAlignment.Center);
                lvitem.Columns.Add("Unit", 100, HorizontalAlignment.Center);
                lvitem.Columns.Add("Alt Qty", 100, HorizontalAlignment.Center);
                lvitem.Columns.Add("Unit", 100, HorizontalAlignment.Center);
                lvproduct.Columns.Add("Item Name", 300, HorizontalAlignment.Center);
                lvproduct.Columns.Add("Qty", 100, HorizontalAlignment.Center);
                lvproduct.Columns.Add("Unit", 100, HorizontalAlignment.Center);
                lvproduct.Columns.Add("Alt Qty", 100, HorizontalAlignment.Center);
                lvproduct.Columns.Add("Unit", 100, HorizontalAlignment.Center);
                binditem();
                bindproductionitem();
                bindrowitem();
                this.ActiveControl = txtprocessname;
                txtprocessname.Text = p.Rows[0]["processname"].ToString();
                cmbmainitem.Text = p.Rows[0]["mainitemname"].ToString();
                txtpqty.Text = p.Rows[0]["mqty"].ToString();
                lblpunit.Text = p.Rows[0]["munit"].ToString();
                txtaqty.Text = p.Rows[0]["maqty"].ToString();
                lblaunit.Text = p.Rows[0]["maunit"].ToString();
                txtprodes.Text = p.Rows[0]["processdescription"].ToString();
                if (Convert.ToBoolean(p.Rows[0]["isactiveprocess"].ToString()) == true)
                {
                    chkdisactive.Checked = false;

                }
                else
                {
                    chkdisactive.Checked = true;
                }

                for (int i = 0; i < r.Rows.Count; i++)
                {
                    ListViewItem li;
                    li = lvitem.Items.Add(r.Rows[i]["rowitemname"].ToString());
                    li.SubItems.Add(r.Rows[i]["rowqty"].ToString());
                    li.SubItems.Add(r.Rows[i]["rowunit"].ToString());
                    li.SubItems.Add(r.Rows[i]["rowaqty"].ToString());
                    li.SubItems.Add(r.Rows[i]["rowaunit"].ToString());
                }
                for (int i = 0; i < s.Rows.Count; i++)
                {
                    ListViewItem li;
                    li = lvproduct.Items.Add(s.Rows[i]["proitemname"].ToString());
                    li.SubItems.Add(s.Rows[i]["proqty"].ToString());
                    li.SubItems.Add(s.Rows[i]["prounit"].ToString());
                    li.SubItems.Add(s.Rows[i]["proaqty"].ToString());
                    li.SubItems.Add(s.Rows[i]["proaunit"].ToString());
                }
                btnsubmit.Text = "Update";
                lblprocessno.Text = iid;

            }
            catch
            {
            }
        }

        private void btnAddmainitem_Click(object sender, EventArgs e)
        {
            var privouscontroal = cmbmainitem;
            activecontroal = privouscontroal.Name;
            Itementry client = new Itementry(this, master, tabControl, activecontroal);
            client.Passed(1);
            //client.Show();
            master.AddNewTab(client);
        }

        private void btnEditmainitem_Click(object sender, EventArgs e)
        {
            if (cmbmainitem.Text != "" && cmbmainitem.Text != null)
            {
                SqlCommand cmd = new SqlCommand("select productid from productmaster where Product_Name='" + cmbmainitem.Text + "' and isactive=1", con);
                SqlDataAdapter sda6 = new SqlDataAdapter(cmd);
                DataTable dtitem = new DataTable();
                sda6.Fill(dtitem);
                if (dtitem.Rows.Count > 0)
                {
                    var privouscontroal = cmbmainitem;
                    activecontroal = privouscontroal.Name;
                    Itementry client = new Itementry(this, master, tabControl, activecontroal);
                    client.Updatefromsale(cmbmainitem.Text);
                    client.Passed(1);
                    //client.Show();
                    master.AddNewTab(client);
                }
                else
                {
                    MessageBox.Show("Please Enter/Select Valid Item.");
                }
            }
            else
            {
                MessageBox.Show("Please Enter/Select Item.");
            }
        }

        private void btnadditem_Click(object sender, EventArgs e)
        {
            var privouscontroal = cmbitemname;
            activecontroal = privouscontroal.Name;
            Itementry client = new Itementry(this, master, tabControl, activecontroal);
            client.Passed(1);
            //client.Show();
            master.AddNewTab(client);
        }

        private void butedititemname_Click(object sender, EventArgs e)
        {
            if (cmbitemname.Text != "" && cmbitemname.Text != null)
            {
                SqlCommand cmd = new SqlCommand("select productid from productmaster where Product_Name='" + cmbitemname.Text + "' and isactive=1", con);
                SqlDataAdapter sda6 = new SqlDataAdapter(cmd);
                DataTable dtitem = new DataTable();
                sda6.Fill(dtitem);
                if (dtitem.Rows.Count > 0)
                {
                    var privouscontroal = cmbitemname;
                    activecontroal = privouscontroal.Name;
                    Itementry client = new Itementry(this, master, tabControl, activecontroal);
                    client.Updatefromsale(cmbitemname.Text);
                    client.Passed(1);
                    //client.Show();
                    master.AddNewTab(client);
                }
                else
                {
                    MessageBox.Show("Please Enter/Select Valid Item.");
                }
            }
            else
            {
                MessageBox.Show("Please Enter/Select Item.");
            }
        }

        private void btnaddpro_Click(object sender, EventArgs e)
        {
            var privouscontroal = cmbitemnamebyproduct;
            activecontroal = privouscontroal.Name;
            Itementry client = new Itementry(this, master, tabControl, activecontroal);
            client.Passed(1);
            //client.Show();
            master.AddNewTab(client);
        }

        private void btneditpro_Click(object sender, EventArgs e)
        {
            if (cmbitemnamebyproduct.Text != "" && cmbitemnamebyproduct.Text != null)
            {
                SqlCommand cmd = new SqlCommand("select productid from productmaster where Product_Name='" + cmbitemnamebyproduct.Text + "' and isactive=1", con);
                SqlDataAdapter sda6 = new SqlDataAdapter(cmd);
                DataTable dtitem = new DataTable();
                sda6.Fill(dtitem);
                if (dtitem.Rows.Count > 0)
                {
                    var privouscontroal = cmbitemnamebyproduct;
                    activecontroal = privouscontroal.Name;
                    Itementry client = new Itementry(this, master, tabControl, activecontroal);
                    client.Updatefromsale(cmbitemnamebyproduct.Text);
                    client.Passed(1);
                    //client.Show();
                    master.AddNewTab(client);
                }
                else
                {
                    MessageBox.Show("Please Enter/Select Valid Item.");
                }
            }
            else
            {
                MessageBox.Show("Please Enter/Select Item.");
            }
        }
        Int32 rowid = -1;
        private Production production;
        private string activecontroal_2;
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

        private void lvproduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Delete Item?", "Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    lvproduct.Items[lvproduct.FocusedItem.Index].Remove();
                }
            }
            if (e.KeyCode == Keys.Enter)
            {

                if (lvproduct.SelectedItems.Count > 0)
                {
                    rowid = lvproduct.FocusedItem.Index;
                    cmbitemnamebyproduct.Text = lvproduct.Items[lvproduct.FocusedItem.Index].SubItems[0].Text;
                    txtproqty.Text = lvproduct.Items[lvproduct.FocusedItem.Index].SubItems[1].Text;
                    lblproqty.Text = lvproduct.Items[lvproduct.FocusedItem.Index].SubItems[2].Text;
                    txtproaqty.Text = lvproduct.Items[lvproduct.FocusedItem.Index].SubItems[3].Text;
                    lblproaqty.Text = lvproduct.Items[lvproduct.FocusedItem.Index].SubItems[4].Text;
                    txtproqty.Focus();
                    btnaddproduct.Text = "Update";
                }
            }
        }

        private void lvproduct_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (lvproduct.SelectedItems.Count > 0)
                {
                    rowid = lvproduct.FocusedItem.Index;
                    cmbitemnamebyproduct.Text = lvproduct.Items[lvproduct.FocusedItem.Index].SubItems[0].Text;
                    txtproqty.Text = lvproduct.Items[lvproduct.FocusedItem.Index].SubItems[1].Text;
                    lblproqty.Text = lvproduct.Items[lvproduct.FocusedItem.Index].SubItems[2].Text;
                    txtproaqty.Text = lvproduct.Items[lvproduct.FocusedItem.Index].SubItems[3].Text;
                    lblproaqty.Text = lvproduct.Items[lvproduct.FocusedItem.Index].SubItems[4].Text;
                    txtproqty.Focus();
                    btnaddproduct.Text = "Update";
                }
            }
            catch
            {
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

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                 DialogResult dr1 = MessageBox.Show("Do you want to Delete Process?", "Process", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                 if (dr1 == DialogResult.Yes)
                 {
                     conn.execute("Update tblrowmaterialsmaster set isactive='0' where processid='" + lblprocessno.Text + "'");
                     conn.execute("Update tblproductgeneratedmaster set isactive='0' where processid='" + lblprocessno.Text + "'");
                     conn.execute("Update tblprocessmaster set isactive='0' where id='" + lblprocessno.Text + "'");
                     MessageBox.Show("Data Delete Successfully.");
                     clearall();
                 }
            }
            catch
            {
            }
        }
        Printing prn = new Printing();
        public void printprocess()
        {
            try
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Print Process?", "Process", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    prn.execute("delete from printing");
                    string status;
                    status = "PROCESS REGISTER";
                    string inamerow = "", pqtyrow = "", aqtyrow = "", punitrow = "", aunitrow = "", inamepro = "", pqtypro = "", aqtypro = "", punitpro = "", aunitpro = "";
                    for (int i = 0; i < lvitem.Items.Count; i++)
                    {
                        inamerow += Environment.NewLine + lvitem.Items[i].SubItems[0].Text;
                        pqtyrow += Environment.NewLine + lvitem.Items[i].SubItems[1].Text;
                        aqtyrow += Environment.NewLine + lvitem.Items[i].SubItems[2].Text;
                        punitrow += Environment.NewLine + lvitem.Items[i].SubItems[3].Text;
                        aunitrow += Environment.NewLine + lvitem.Items[i].SubItems[4].Text;
                    }
                    for (int i = 0; i < lvproduct.Items.Count; i++)
                    {
                        inamepro += Environment.NewLine + lvproduct.Items[i].SubItems[0].Text;
                        pqtypro += Environment.NewLine + lvproduct.Items[i].SubItems[1].Text;
                        aqtypro += Environment.NewLine + lvproduct.Items[i].SubItems[2].Text;
                        punitpro += Environment.NewLine + lvproduct.Items[i].SubItems[3].Text;
                        aunitpro += Environment.NewLine + lvproduct.Items[i].SubItems[4].Text;
                    }
                    DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1 and CompanyID='" + Master.companyId + "' ");
                    string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30)VALUES";
                    qry += "('" + dt1.Rows[0]["CompanyName"].ToString() + "','" + dt1.Rows[0]["SubName"].ToString() + "','" + dt1.Rows[0]["Address"].ToString() + "','" + dt1.Rows[0]["Address2"].ToString() + "','" + dt1.Rows[0]["City"].ToString() + "','" + dt1.Rows[0]["State"].ToString() + "','" + dt1.Rows[0]["Country"].ToString() + "','" + dt1.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Mobile"].ToString() + "','" + dt1.Rows[0]["Email"].ToString() + "','" + dt1.Rows[0]["CSTNo"].ToString() + "','" + status + "','" + dt1.Rows[0]["Website"].ToString() + "','" + inamerow + "','" + pqtyrow + "','" + aqtyrow + "','" + punitrow + "','" + aunitrow + "','" + inamepro + "','" + pqtypro + "','" + aqtypro + "','" + punitpro + "','" + aunitpro + "','" + txtprocessname.Text + "','" + cmbmainitem.Text + "','" + txtpqty.Text + "','" + txtaqty.Text + "','" + txtprodes.Text + "','" + lblpunit.Text + "','" + lblaunit.Text + "')";
                    prn.execute(qry);
                    string reportName = "ProductionProcess";
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
            printprocess();
        }

        private void btnaddraw_Enter(object sender, EventArgs e)
        {
            btnaddraw.UseVisualStyleBackColor = false;
            btnaddraw.BackColor = Color.FromArgb(9,106,3);
            btnaddraw.ForeColor = Color.White;
        }

        private void btnaddraw_Leave(object sender, EventArgs e)
        {
            btnaddraw.UseVisualStyleBackColor = true;
            btnaddraw.BackColor = Color.FromArgb(51, 153, 255);
            btnaddraw.ForeColor = Color.White;
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
            btnaddraw.BackColor = Color.FromArgb(51, 153, 255);
            btnaddraw.ForeColor = Color.White;
        }

        private void btnaddproduct_Enter(object sender, EventArgs e)
        {
            btnaddraw.UseVisualStyleBackColor = false;
            btnaddraw.BackColor = Color.FromArgb(9, 106, 3);
            btnaddraw.ForeColor = Color.White;
        }

        private void btnaddproduct_Leave(object sender, EventArgs e)
        {
            btnaddraw.UseVisualStyleBackColor = true;
            btnaddraw.BackColor = Color.FromArgb(51, 153, 255);
            btnaddraw.ForeColor = Color.White;
        }

        private void btnaddproduct_MouseEnter(object sender, EventArgs e)
        {
            btnaddraw.UseVisualStyleBackColor = false;
            btnaddraw.BackColor = Color.FromArgb(9, 106, 3);
            btnaddraw.ForeColor = Color.White;
        }

        private void btnaddproduct_MouseLeave(object sender, EventArgs e)
        {
            btnaddraw.UseVisualStyleBackColor = true;
            btnaddraw.BackColor = Color.FromArgb(51, 153, 255);
            btnaddraw.ForeColor = Color.White;
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
    }
}
