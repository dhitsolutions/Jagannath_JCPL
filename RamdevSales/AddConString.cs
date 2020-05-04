using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Data.OleDb;

namespace RamdevSales
{
    public partial class AddConString : Form
    {
        OleDbSettings ods = new OleDbSettings();

        public AddConString()
        {
            InitializeComponent();
            
        }

        private void AddConString_Load(object sender, EventArgs e)
        {
            loadPage();
            
        }

        private void loadPage()
        {
            
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
           // this.ActiveControl = chkEnableSQL;
            txtServerName.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //Data Source=ADMIN;Initial Catalog=inventory;User ID=sa;Password=root
                if (txtServerName.Text == "" || txtUserName.Text == "" || txtPassword.Text == "" || cmbAuth.SelectedIndex == -1)
                {
                    if (txtServerName.Text == "")
                    {
                        MessageBox.Show("Please Enter Server Name");
                        txtServerName.Focus();
                    }
                    else if (cmbAuth.SelectedIndex == -1)
                    {
                        MessageBox.Show("Please Select Authentication type");
                        cmbAuth.Focus();
                    }
                    else if (txtUserName.Text == "")
                    {
                        MessageBox.Show("Please Enter User Name");
                        txtServerName.Focus();
                    }
                    else if (txtPassword.Text == "")
                    {
                        MessageBox.Show("Please Enter Password");
                        txtPassword.Focus();
                    }
                }
                else
                {
                    string constr = "Data Source=" + txtServerName.Text + ";Initial Catalog=inventory;User ID=" + txtUserName.Text + ";Password=" + txtPassword.Text + "";
                    ods.execute("INSERT INTO [SQLSetting]([EnableSQL],[SQLServerName],[Authentication],[UserName],[Password1],[DBName],[ConString]) values('" + chkEnableSQL.Checked + "','" + txtServerName.Text + "','" + cmbAuth.SelectedItem + "','" + txtUserName.Text + "','" + txtPassword.Text + "','Local','" + constr + "')");

                    clearAll();
                    MessageBox.Show("Data Inserted Successfully.");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }

            finally
            {
               
            }
        }

        private void clearAll()
        {
            txtServerName.Text = "";
            cmbAuth.SelectedIndex = 0;
            txtUserName.Text = "";
            txtPassword.Text = "";
        }

        private void txtServerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbAuth.Focus();
            }
        }

        private void cmbAuth_KeyPress(object sender, KeyPressEventArgs e)
        {
                txtUserName.Focus();
           
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave.Focus();
            }
        }
       
    }
}
