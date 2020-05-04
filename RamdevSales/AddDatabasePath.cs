using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace RamdevSales
{
    public partial class AddDatabasePath : Form
    {
        OleDbSettings ods = new OleDbSettings();
        Connection conn = new Connection();
        private Master master;
        private TabControl tabControl;
        DataSet ds2 = new DataSet();
        DataSet ds3 = new DataSet();
        DataSet ds4 = new DataSet();
        public OleDbConnection con;
        DataTable dt,dt1 = new DataTable();
        public AddDatabasePath()
        {
            InitializeComponent();
        }

        public AddDatabasePath(Master master, TabControl tabControl)
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
                // tabControl.TabPages.Remove(tabControl.SelectedTab);
                DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    master.RemoveCurrentTab();
                }
                return true;
            }


            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtsetpath_Click(object sender, EventArgs e)
        {
            try
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();
                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        txtdatabasepath.Text = fbd.SelectedPath;
                        btnapply.Text = "Update";

                    }
                }
            }
            catch
            {
            }
        }
        public static int newcompany = 0;
        private void btnapply_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnapply.Text == "Update")
                {
                    string cstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtdatabasepath.Text + "\\ConnectDB.mdb;Jet OLEDB:Database Password=allthebest;";
                    OleDbConnection con1 = new OleDbConnection(cstr);
                    ds3 = ods.getdata1("Select * from Company", con1);
                    dt = ds3.Tables[0];
                    ds4 = ods.getdata1("Select * from tblreg", con1);
                    dt1 = ds4.Tables[0];
                    ods.execute("delete from Company");
                    ods.execute("delete from Path");
                    ods.execute("delete from tblreg");
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ods.execute("INSERT INTO [Company]([CompanyID],[CompanyName],[SubName],[Address],[Address2],[City],[State],[Statecode],[Country],[Phone],[Mobile],[Email],[Website],[CSTNo],[PANNo],[VATNo],[DLNo1],[DLNo2],[DealsIn],[Stockist],[SQLDatabase],[StartDate],[EndDate],[DatabaseName],[DatabaseConnectionString],[currency],[isActive])VALUES('" + dt.Rows[i]["CompanyID"].ToString() + "','" + dt.Rows[i]["CompanyName"].ToString() + "','" + dt.Rows[i]["SubName"].ToString() + "','" + dt.Rows[i]["Address"].ToString() + "','" + dt.Rows[i]["Address2"].ToString() + "','" + dt.Rows[i]["City"].ToString() + "','" + dt.Rows[i]["State"].ToString() + "','" + dt.Rows[i]["Statecode"].ToString() + "','" + dt.Rows[i]["Country"].ToString() + "','" + dt.Rows[i]["Phone"].ToString() + "','" + dt.Rows[i]["Mobile"].ToString() + "','" + dt.Rows[i]["Email"].ToString() + "','" + dt.Rows[i]["Website"].ToString() + "','" + dt.Rows[i]["CSTNo"].ToString() + "','" + dt.Rows[i]["PANNo"].ToString() + "','" + dt.Rows[i]["VATNo"].ToString() + "','" + dt.Rows[i]["DLNo1"].ToString() + "','" + dt.Rows[i]["DLNo2"].ToString() + "','" + dt.Rows[i]["DealsIn"].ToString() + "','" + dt.Rows[i]["Stockist"].ToString() + "','" + dt.Rows[i]["SQLDatabase"].ToString() + "','" + dt.Rows[i]["StartDate"].ToString() + "','" + dt.Rows[i]["EndDate"].ToString() + "','" + dt.Rows[i]["DatabaseName"].ToString() + "','" + dt.Rows[i]["DatabaseConnectionString"].ToString() + "','" + dt.Rows[i]["currency"].ToString() + "',1)");
                        }
                        ods.execute("INSERT INTO [Path]([DefaultPath])VALUES('" + defaultpath + "')");
                        ods.execute("INSERT INTO [tblreg]([d10],[d11],[d12],[d13],[d14],[d15],[d16],[d17],[d18])VALUES('" + dt1.Rows[0]["d10"].ToString() + "','" + dt1.Rows[0]["d11"].ToString() + "','" + dt1.Rows[0]["d12"].ToString() + "','" + dt1.Rows[0]["d13"].ToString() + "','" + dt1.Rows[0]["d14"].ToString() + "','" + dt1.Rows[0]["d15"].ToString() + "','" + dt1.Rows[0]["d16"].ToString() + "','" + dt1.Rows[0]["d17"].ToString() + "','" + dt1.Rows[0]["d18"].ToString() + "')");
                      //  conn.execute("Update updatedatabase set lanconnection='" + "1" + "' where id='" + "1" + "'");

                    }
                    //ods.execute("UPDATE [Path] SET [DefaultPath]='" + txtdatabasepath.Text + "'");
                    MessageBox.Show("Database Path Update Successfully");
                }
                else
                {
                    ods.execute("INSERT INTO [Path]([DefaultPath])VALUES('" + txtdatabasepath.Text + "')");
                    MessageBox.Show("Database Path Successfully Changed");
                }
                master.RemoveCurrentTab();

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
        public static string defaultpath = "";
        private void AddDatabasePath_Load(object sender, EventArgs e)
        {
            try
            {
                String path = Application.StartupPath;
                String ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\ConnectDB.mdb;Jet OLEDB:Database Password=allthebest;";
                string database = path + "\\ConnectDB.mdb";

                con = new OleDbConnection(ConnectionString);
                ds2 = ods.getdata("select * from Path");
                //  ds2 = ods.getdata("select * from Path",con);
                txtdatabasepath.Text = path;
                defaultpath = txtdatabasepath.Text;
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    btnapply.Text = "Update";
                    path = ds2.Tables[0].Rows[0]["defaultpath"].ToString();
                    txtdatabasepath.Text = path;
                    

                }

            }
            catch
            {
            }
        }

        private void txtsetpath_MouseEnter(object sender, EventArgs e)
        {
            txtsetpath.UseVisualStyleBackColor = true;
            txtsetpath.BackColor = Color.FromArgb(128, 128, 0);
            txtsetpath.ForeColor = Color.White;
        }

        private void txtsetpath_MouseLeave(object sender, EventArgs e)
        {
            txtsetpath.UseVisualStyleBackColor = true;
            txtsetpath.BackColor = Color.FromArgb(51, 153, 255);
            txtsetpath.ForeColor = Color.White;
        }

        private void txtsetpath_Enter(object sender, EventArgs e)
        {
            txtsetpath.UseVisualStyleBackColor = true;
            txtsetpath.BackColor = Color.FromArgb(128, 128, 0);
            txtsetpath.ForeColor = Color.White;
        }

        private void txtsetpath_Leave(object sender, EventArgs e)
        {
            txtsetpath.UseVisualStyleBackColor = true;
            txtsetpath.BackColor = Color.FromArgb(51, 153, 255);
            txtsetpath.ForeColor = Color.White;
        }

        private void btnapply_MouseEnter(object sender, EventArgs e)
        {
            btnapply.UseVisualStyleBackColor = false;
            btnapply.BackColor = Color.YellowGreen;
            btnapply.ForeColor = Color.White;
        }

        private void btnapply_MouseLeave(object sender, EventArgs e)
        {
            btnapply.UseVisualStyleBackColor = true;
            btnapply.BackColor = Color.FromArgb(51, 153, 255);
            btnapply.ForeColor = Color.White;
        }

        private void btnapply_Enter(object sender, EventArgs e)
        {
            btnapply.UseVisualStyleBackColor = false;
            btnapply.BackColor = Color.YellowGreen;
            btnapply.ForeColor = Color.White;
        }

        private void btnapply_Leave(object sender, EventArgs e)
        {
            btnapply.UseVisualStyleBackColor = true;
            btnapply.BackColor = Color.FromArgb(51, 153, 255);
            btnapply.ForeColor = Color.White;
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
    }
}
