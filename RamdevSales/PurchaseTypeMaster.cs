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
    public partial class PurchaseTypeMaster : Form
    {
        Connection con = new Connection();
        private Master master;
        private TabControl tabControl;
       
        public PurchaseTypeMaster()
        {
            InitializeComponent();
            
        }

        public PurchaseTypeMaster(Master master, TabControl tabControl)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this.master = master;
            this.tabControl = tabControl;
        }

        

        private void PurchaseTypeMaster_Load(object sender, EventArgs e)
        {
            LVDayBook.Columns.Add("Type", 350, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("Account", 200, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("Tax. Type", 150, HorizontalAlignment.Left);
            LVDayBook.Columns.Add("Region", 150, HorizontalAlignment.Left);
            LVDayBook.Columns.Add("Form Type", 150, HorizontalAlignment.Left);
            bindlistview();
            this.ActiveControl = btnnew;
        }

        private void bindlistview()
        {
            DataTable dt = con.getdataset("select ag.groupname,p.* from purchasetypemaster p inner join AccountGroup ag on ag.id=p.groupid where p.Type='P' and p.isactive=1");
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                LVDayBook.Items.Add(dt.Rows[i].ItemArray[2].ToString());
                LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[0].ToString());
                LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[14].ToString());
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
                    //tabControl.SelectTab(1);
                }
                return true;
            }


            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void btnnew_Click(object sender, EventArgs e)
        {
            PurchaseTypeEntry frm = new PurchaseTypeEntry(master,tabControl);
            master.AddNewTab(frm);
           // frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.WindowsDefaultLocation;
            //frm.Show();
        }
        public void open()
        {
            try
            {
                this.Enabled = false;
                String str = LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[1].Text;
                PurchaseTypeEntry bd = new PurchaseTypeEntry(this, master, tabControl);
                bd.updatemode(str, LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[0].Text, LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[4].Text);
                //  bd.Show();
                master.AddNewTab(bd);
            }
            finally
            {
                this.Enabled = true;
            }
        }
        private void LVDayBook_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            open();
        }

        private void LVDayBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                open();
            }
           
        }

       

        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
                //tabControl.SelectTab(1);
            }
        }

        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            LVDayBook.Items.Clear();
            bindlistview();
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

        private void btnnew_MouseEnter(object sender, EventArgs e)
        {
            btnnew.UseVisualStyleBackColor = false;
            btnnew.BackColor = Color.FromArgb(9, 106, 3);
            btnnew.ForeColor = Color.White;
        }

        private void btnnew_MouseLeave(object sender, EventArgs e)
        {
            btnnew.UseVisualStyleBackColor = true;
            btnnew.BackColor = Color.FromArgb(51, 153, 255);
            btnnew.ForeColor = Color.White;
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

        private void btnnew_Enter(object sender, EventArgs e)
        {
            btnnew.UseVisualStyleBackColor = false;
            btnnew.BackColor = Color.FromArgb(9, 106, 3);
            btnnew.ForeColor = Color.White;
        }

        private void btnnew_Leave(object sender, EventArgs e)
        {
            btnnew.UseVisualStyleBackColor = true;
            btnnew.BackColor = Color.FromArgb(51, 153, 255);
            btnnew.ForeColor = Color.White;
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
    }
}
