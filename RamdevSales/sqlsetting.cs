using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;

namespace RamdevSales
{
    public partial class sqlsetting : Form
    {
        public static string servername = string.Empty;
        public static string username = string.Empty;
        public static string pass = string.Empty;
        public static string databaseconnection = string.Empty;
        public static string Authentication = string.Empty;
        public sqlsetting()
        {
            InitializeComponent();
        }

        private void sqlsetting_Load(object sender, EventArgs e)
        {

            DataTable table = SqlDataSourceEnumerator.Instance.GetDataSources();
            foreach (DataRow server in table.Rows)
            {
                cmbservername.Items.Add(server[table.Columns["ServerName"]].ToString());
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                // tabControl.TabPages.Remove(tabControl.SelectedTab);
                DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    this.Close();
                }
                return true;
            }


            return base.ProcessCmdKey(ref msg, keyData);
        }
        public void createdatabasewindows()
        {
            using (SqlConnection myConn = new SqlConnection("Data Source='" + cmbservername.Text + "';"))
            {
                try
                {
                    string str;
                    string appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\SQLDB\";
                    //  appPath.Replace("'\'", "\\");
                    string databasename = "sql";
                    appPath = appPath + databasename;
                    //FILENAME = 'J:\\contil\\29-02-2017\\contil\\Contil\\trunk\\Ramdev Sales Final\\RamdevSales\\bin\\Debug\\SQLDB\\tempdb12.mdf'
                    str = "CREATE DATABASE " + databasename + " ON PRIMARY " +
    "(NAME = " + databasename + "_Data, " +
    "FILENAME = '" + appPath + ".mdf', " +
    "SIZE = 3MB, MAXSIZE = 10MB, FILEGROWTH = 10%) " +
    "LOG ON (NAME = " + databasename + ", " +
    "FILENAME = '" + appPath + ".ldf', " +
    "SIZE = 512KB, " +
    "MAXSIZE = 5MB, " +
    "FILEGROWTH = 10%)";
                    if (myConn.State == ConnectionState.Open)
                    {
                        myConn.Close();
                    }
                    myConn.Open();
                    SqlCommand myCommand = new SqlCommand(str, myConn);
                    myCommand.ExecuteNonQuery();
                }
                catch
                {
                }
            }
        }
        public void createdatabase()
        {
            using (SqlConnection myConn = new SqlConnection("Data Source='" + cmbservername.Text + "';User ID='" + txtusername.Text + "';Password='" + txtpass.Text + "'"))
            {
                try
                {
                    string str;
                    string appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\SQLDB\";
                    //  appPath.Replace("'\'", "\\");
                    string databasename = "sql";
                    appPath = appPath + databasename;
                    //FILENAME = 'J:\\contil\\29-02-2017\\contil\\Contil\\trunk\\Ramdev Sales Final\\RamdevSales\\bin\\Debug\\SQLDB\\tempdb12.mdf'
                    str = "CREATE DATABASE " + databasename + " ON PRIMARY " +
    "(NAME = " + databasename + "_Data, " +
    "FILENAME = '" + appPath + ".mdf', " +
    "SIZE = 3MB, MAXSIZE = 10MB, FILEGROWTH = 10%) " +
    "LOG ON (NAME = " + databasename + ", " +
    "FILENAME = '" + appPath + ".ldf', " +
    "SIZE = 512KB, " +
    "MAXSIZE = 5MB, " +
    "FILEGROWTH = 10%)";
                    if (myConn.State == ConnectionState.Open)
                    {
                        myConn.Close();
                    }
                    myConn.Open();
                    SqlCommand myCommand = new SqlCommand(str, myConn);
                    myCommand.ExecuteNonQuery();
                    SqlConnection Conn = new SqlConnection("Data Source='" + cmbservername.Text + "';Initial Catalog='" + databasename + "';User ID='" + txtusername.Text + "';Password='" + txtpass.Text + "'");
                    string appPath1 = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\SQLDB\";
                    var fileContent = File.ReadAllText(appPath1 + "\\script.sql");
                    var sqlqueries = fileContent.Split(new[] { " GO " }, StringSplitOptions.RemoveEmptyEntries);


                    var cmd = new SqlCommand("query", Conn);
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                    }
                    Conn.Open();
                    foreach (var query in sqlqueries)
                    {
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();

                    }
                    Conn.Close();
                    MessageBox.Show("DataBase is Created Successfully", "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    if (myConn.State == ConnectionState.Open)
                    {
                        myConn.Close();
                    }
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbauthentication.Text == "Windows Authentication")
                {
                    //Data Source=.;Initial Catalog=master;Integrated Security=True
                    //Data Source=myServerAddress;Initial Catalog=myDataBase;Integrated Security=SSPI;
                    using (SqlConnection myConn = new SqlConnection("Data Source='"+ cmbservername.Text +"';Integrated Security=SSPI;"))
                    {
                        try
                        {
                            if (myConn.State == ConnectionState.Open)
                            {
                                myConn.Close();
                            }
                            myConn.Open();
                            if (myConn.State == ConnectionState.Open)
                            {
                                servername = cmbservername.Text;
                                username = txtusername.Text;
                                pass = txtpass.Text;
                                Authentication = cmbauthentication.Text;
                                // createdatabase();
                                MessageBox.Show("You have been successfully connected to the database!");
                                clearall();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Connection failed.");
                            }
                        }
                        catch (SqlException)
                        {
                            MessageBox.Show("Connection failed.");
                        }
                    }
                }
                else
                {
                    using (SqlConnection myConn = new SqlConnection("Data Source='" + cmbservername.Text + "';User ID='" + txtusername.Text + "';Password='" + txtpass.Text + "'"))
                    {
                        try
                        {
                            if (myConn.State == ConnectionState.Open)
                            {
                                myConn.Close();
                            }
                            myConn.Open();
                            if (myConn.State == ConnectionState.Open)
                            {
                                servername = cmbservername.Text;
                                username = txtusername.Text;
                                pass = txtpass.Text;
                                Authentication = cmbauthentication.Text;
                                // createdatabase();
                                MessageBox.Show("You have been successfully connected to the database!");
                                clearall();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Connection failed.");
                            }
                        }
                        catch (SqlException)
                        {
                            MessageBox.Show("Connection failed.");
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chyba v přihlášení: " + ex);
            }
        }
        public void clearall()
        {
            txtusername.Text = "";
            txtpass.Text = "";
            cmbservername.SelectedIndex = 0;
            cmbauthentication.SelectedIndex = 0;
        }
        private void cmbservername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                cmbauthentication.Focus();
            }
        }

        private void cmbauthentication_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtusername.Focus();
            }
        }

        private void txtusername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtpass.Focus();
            }
        }

        private void txtpass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave.Focus();
            }
        }

        private void btncancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbservername_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbservername.SelectedIndex = 0;
                cmbservername.DroppedDown = true;
            }
            catch 
            {
            }
        }

        private void cmbauthentication_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbauthentication.SelectedIndex = 0;
                cmbauthentication.DroppedDown = true;
            }
            catch
            {
            }
            
            
        }

        private void cmbauthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbauthentication.SelectedIndex == 0)
            {
                txtusername.ReadOnly = true;
                txtpass.ReadOnly = true;
                txtusername.Text = "";
                txtpass.Text = "";
                txtusername.Text = cmbservername.Text + "/admin";
            }
            else
            {
                txtusername.ReadOnly = false;
                txtpass.ReadOnly = false;
                txtusername.Text = "";
                txtpass.Text = "";
            }
        }

        private void cmbservername_Leave(object sender, EventArgs e)
        {
            
        }

        private void cmbauthentication_Leave(object sender, EventArgs e)
        {
            
        }

        private void txtusername_Enter(object sender, EventArgs e)
        {
            txtusername.BackColor = Color.LightYellow;
        }

        private void txtusername_Leave(object sender, EventArgs e)
        {
            txtusername.BackColor = Color.White;
        }

        private void txtpass_Enter(object sender, EventArgs e)
        {
            txtpass.BackColor = Color.LightYellow;
        }

        private void txtpass_Leave(object sender, EventArgs e)
        {
            txtpass.BackColor = Color.White;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnSave_Enter(object sender, EventArgs e)
        {
            btnSave.UseVisualStyleBackColor = false;
            btnSave.BackColor = Color.YellowGreen;
            btnSave.ForeColor = Color.White;
        }

        private void btnSave_Leave(object sender, EventArgs e)
        {
            btnSave.UseVisualStyleBackColor = true;
            btnSave.BackColor = Color.FromArgb(51, 153, 255);
            btnSave.ForeColor = Color.White;
        }

        private void btnSave_MouseEnter(object sender, EventArgs e)
        {
            btnSave.UseVisualStyleBackColor = false;
            btnSave.BackColor = Color.YellowGreen;
            btnSave.ForeColor = Color.White;
        }

        private void btnSave_MouseLeave(object sender, EventArgs e)
        {
            btnSave.UseVisualStyleBackColor = true;
            btnSave.BackColor = Color.FromArgb(51, 153, 255);
            btnSave.ForeColor = Color.White;
        }

        private void btncancle_Enter(object sender, EventArgs e)
        {
            btncancle.UseVisualStyleBackColor = false;
            btncancle.BackColor = Color.FromArgb(248, 152, 94);
            btncancle.ForeColor = Color.White;
        }

        private void btncancle_Leave(object sender, EventArgs e)
        {
            btncancle.UseVisualStyleBackColor = true;
            btncancle.BackColor = Color.FromArgb(51, 153, 255);
            btncancle.ForeColor = Color.White;
        }

        private void btncancle_MouseEnter(object sender, EventArgs e)
        {
            btncancle.UseVisualStyleBackColor = false;
            btncancle.BackColor = Color.FromArgb(248, 152, 94);
            btncancle.ForeColor = Color.White;
        }

        private void btncancle_MouseLeave(object sender, EventArgs e)
        {
            btncancle.UseVisualStyleBackColor = true;
            btncancle.BackColor = Color.FromArgb(51, 153, 255);
            btncancle.ForeColor = Color.White;
        }
    }
}
