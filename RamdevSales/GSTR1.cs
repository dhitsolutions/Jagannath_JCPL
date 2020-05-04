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
    public partial class GSTR1 : Form
    {
        private Master master;
        private TabControl tabControl;

        public GSTR1()
        {
            InitializeComponent();
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
        public GSTR1(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
            button1.Focus();
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                GST_Register_Bill_Wise gst = new GST_Register_Bill_Wise(master,tabControl);
                master.AddNewTab(gst);

            }
            catch
            {
            }
        }

        private void txtbtb_Click(object sender, EventArgs e)
        {
            try
            {
                B2B b2b = new B2B(master, tabControl);
                master.AddNewTab(b2b);
            }
            catch
            {
            }
        }

        private void btnb2cs_Click(object sender, EventArgs e)
        {
            try
            {
                B2Cs b2b = new B2Cs(master, tabControl);
                master.AddNewTab(b2b);
            }
            catch
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                B2Cl b2c = new B2Cl(master, tabControl);
                master.AddNewTab(b2c);
            }
            catch
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                hsnreport b2c = new hsnreport(master, tabControl);
                master.AddNewTab(b2c);
            }
            catch
            {
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ComplateGST_R1 b2c = new ComplateGST_R1(master, tabControl);
            master.AddNewTab(b2c);
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

        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }

        private void btnregdealers_Click(object sender, EventArgs e)
        {
            try
            {
                cdnr b2b = new cdnr(master, tabControl);
                master.AddNewTab(b2b);
            }
            catch
            {
            }
        }

        private void btncndur_Click(object sender, EventArgs e)
        {
            try
            {
                cdnur b2b = new cdnur(master, tabControl);
                master.AddNewTab(b2b);
            }
            catch
            {
            }
        }

       
    }
}
