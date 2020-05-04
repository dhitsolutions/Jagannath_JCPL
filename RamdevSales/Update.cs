using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RamdevSales
{
    public partial class Update : Form
    {
        private Master master;
        private TabControl tabControl;
        Connection conn = new Connection();
        public Update()
        {
            InitializeComponent();
        }

        public Update(Master master, TabControl tabControl)
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
        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtup.Text == "allthebest")
                {
                    conn.getdataset("Update updatedatabase set updatecode='" + txtdate.Text + "' where id='" + "1" + "'");
                    MessageBox.Show("Data Updated Successfully.");
                    master.RemoveCurrentTab();
                }
                else
                {
                    MessageBox.Show("You Are Not Authorized to Access");
                }
            }
            catch
            {
            }
        }
    }
}
