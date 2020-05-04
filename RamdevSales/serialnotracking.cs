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
    public partial class serialnotracking : Form
    {
        private Master master;
        private TabControl tabControl;
        Connection conn = new Connection();
        
        public serialnotracking()
        {
            InitializeComponent();
        }

        public serialnotracking(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
            lvstockin.Columns.Add("Date", 100, HorizontalAlignment.Left);
            lvstockin.Columns.Add("Type", 60, HorizontalAlignment.Left);
            lvstockin.Columns.Add("Bill No", 80, HorizontalAlignment.Left);
            lvstockin.Columns.Add("Party", 100, HorizontalAlignment.Left);
            lvstockin.Columns.Add("Item", 150, HorizontalAlignment.Left);
            lvstockin.Columns.Add("CO", 100, HorizontalAlignment.Left);
            lvstockin.Columns.Add("Batch", 50, HorizontalAlignment.Left);
            lvstockin.Columns.Add("Price", 100, HorizontalAlignment.Left);
            lvstockin.Columns.Add("Dis%", 60, HorizontalAlignment.Left);
            lvstockin.Columns.Add("billno", 0, HorizontalAlignment.Left);

            lvstockout.Columns.Add("Date", 100, HorizontalAlignment.Left);
            lvstockout.Columns.Add("Type", 60, HorizontalAlignment.Left);
            lvstockout.Columns.Add("Bill No", 80, HorizontalAlignment.Left);
            lvstockout.Columns.Add("Party", 100, HorizontalAlignment.Left);
            lvstockout.Columns.Add("Item", 150, HorizontalAlignment.Left);
            lvstockout.Columns.Add("CO", 100, HorizontalAlignment.Left);
            lvstockout.Columns.Add("Batch", 50, HorizontalAlignment.Left);
            lvstockout.Columns.Add("Price", 100, HorizontalAlignment.Left);
            lvstockout.Columns.Add("Dis%", 60, HorizontalAlignment.Left);
            this.ActiveControl = txtserialno;
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
        ListViewItem li;
        private void btnok_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = conn.getdataset("select Bill_Run_Date,Billtype,Bill_No,Productname,per,batch,Rate,discountper,billno from BillProductMaster where serialno like '%" + txtserialno.Text + "%' and isactive=1 and Billtype='P' or Billtype='SR'");
                DataTable clientid = conn.getdataset("select ClientID from BillMaster where isactive=1 and billno='" + dt.Rows[0]["billno"].ToString() + "'");
                DataTable partyname = conn.getdataset("select AccountName from ClientMaster where isactive=1 and ClientID='" + clientid.Rows[0]["ClientID"].ToString() + "'");

                if (dt.Rows.Count > 0)
                {
                    li = lvstockin.Items.Add(Convert.ToDateTime(dt.Rows[0].ItemArray[0].ToString()).ToString(Master.dateformate));
                    li.SubItems.Add(dt.Rows[0]["Billtype"].ToString());
                    li.SubItems.Add(dt.Rows[0]["billno"].ToString());
                    li.SubItems.Add(partyname.Rows[0]["AccountName"].ToString());
                    li.SubItems.Add(dt.Rows[0]["Productname"].ToString());
                    li.SubItems.Add(dt.Rows[0]["per"].ToString());
                    li.SubItems.Add(dt.Rows[0]["batch"].ToString());
                    li.SubItems.Add(dt.Rows[0]["Rate"].ToString());
                    li.SubItems.Add(dt.Rows[0]["discountper"].ToString());
                    li.SubItems.Add(dt.Rows[0]["billno"].ToString());
                    txtstatus.Text = "Product Available";
                }

                DataTable dt1 = conn.getdataset("select Bill_Run_Date,Billtype,Bill_No,Productname,per,batch,Rate,discountper,billno from BillProductMaster where serialno like '%" + txtserialno.Text + "%' and isactive=1 and Billtype='S' or Billtype='PR'");
                DataTable clientid1 = conn.getdataset("select ClientID from BillMaster where isactive=1 and billno='" + dt1.Rows[0]["billno"].ToString() + "'");
                DataTable partyname1 = conn.getdataset("select AccountName from ClientMaster where isactive=1 and ClientID='" + clientid1.Rows[0]["ClientID"].ToString() + "'");

                if (dt1.Rows.Count > 0)
                {
                    li = lvstockout.Items.Add(Convert.ToDateTime(dt1.Rows[0].ItemArray[0].ToString()).ToString(Master.dateformate));
                    li.SubItems.Add(dt1.Rows[0]["Billtype"].ToString());
                    li.SubItems.Add(dt1.Rows[0]["billno"].ToString());
                    li.SubItems.Add(partyname1.Rows[0]["AccountName"].ToString());
                    li.SubItems.Add(dt1.Rows[0]["Productname"].ToString());
                    li.SubItems.Add(dt1.Rows[0]["per"].ToString());
                    li.SubItems.Add(dt1.Rows[0]["batch"].ToString());
                    li.SubItems.Add(dt1.Rows[0]["Rate"].ToString());
                    li.SubItems.Add(dt1.Rows[0]["discountper"].ToString());
                    txtstatus.Text = "Product Not Available";
                   
                }
                //int stockincount = lvstockin.Items.Count;
                //int stockoutcount = lvstockout.Items.Count;
                //if (stockincount == stockoutcount)
                //{
                //    txtcstatus.Text = "Item Not Avalable";
                //}
                //else if (stockincount < stockoutcount)
                //{
                //    txtcstatus.Text = "Item Not Avalable";
                //}
                //else if (stockincount > stockoutcount)
                //{
                //    txtcstatus.Text = "Item Avalable";
                //}
            }
            catch
            {
            }
        }
        public void setformstockin()
        {
            //  this.Enabled = false;
            string[] strfinalarray = new string[lvstockin.Items.Count];
            if (lvstockin.Items[lvstockin.FocusedItem.Index].SubItems[1].Text == "S")
            {
                strfinalarray = new string[5] { "S", "D", "Sale", "S", "" };
            }
            else if (lvstockin.Items[lvstockin.FocusedItem.Index].SubItems[1].Text == "P")
            {
                strfinalarray = new string[5] { "P", "C", "Purchase", "P", "" };
            }
            else if (lvstockin.Items[lvstockin.FocusedItem.Index].SubItems[1].Text == "SR")
            {
                strfinalarray = new string[5] { "SR", "C", "SaleReturn", "SR", "" };
            }
            else if (lvstockin.Items[lvstockin.FocusedItem.Index].SubItems[1].Text == "PR")
            {
                strfinalarray = new string[5] { "PR", "D", "PurchaseReturn", "PR", "" };
            }
            String str = lvstockin.Items[lvstockin.FocusedItem.Index].SubItems[2].Text;
            DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
            DefaultSale bd = new DefaultSale(this, master, tabControl, strfinalarray);
            //  Sale p = new Sale(this, master, tabControl);
            if (dt1.Rows[0]["formname"].ToString() == bd.Text)
            {
                bd.updatemode(str, lvstockin.Items[lvstockin.FocusedItem.Index].SubItems[2].Text, 1, strfinalarray);
                master.AddNewTab(bd);
            }
            //else if (dt1.Rows[0]["formname"].ToString() == p.Text)
            //{
            //    p.updatemode(str, lvstockin.Items[lvstockin.FocusedItem.Index].SubItems[2].Text, 1);
            //    master.AddNewTab(p);
            //}
        }
        public void setformstockout()
        {
            //this.Enabled = false;
            string[] strfinalarray = new string[lvstockout.Items.Count];
            if (lvstockout.Items[lvstockout.FocusedItem.Index].SubItems[1].Text == "S")
            {
                strfinalarray = new string[5] { "S", "D", "Sale", "S", "" };
            }
            else if (lvstockout.Items[lvstockout.FocusedItem.Index].SubItems[1].Text == "P")
            {
                strfinalarray = new string[5] { "P", "C", "Purchase", "P", "" };
            }
            else if (lvstockout.Items[lvstockout.FocusedItem.Index].SubItems[1].Text == "SR")
            {
                strfinalarray = new string[5] { "SR", "C", "SaleReturn", "SR", "" };
            }
            else if (lvstockout.Items[lvstockout.FocusedItem.Index].SubItems[1].Text == "PR")
            {
                strfinalarray = new string[5] { "PR", "D", "PurchaseReturn", "PR", "" };
            }
            String str = lvstockout.Items[lvstockout.FocusedItem.Index].SubItems[2].Text;
            DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
            DefaultSale bd = new DefaultSale(this, master, tabControl, strfinalarray);
            //  Sale p = new Sale(this, master, tabControl);
            if (dt1.Rows[0]["formname"].ToString() == bd.Text)
            {
                bd.updatemode(str, lvstockout.Items[lvstockout.FocusedItem.Index].SubItems[2].Text, 1, strfinalarray);
                master.AddNewTab(bd);
            }
            //else if (dt1.Rows[0]["formname"].ToString() == p.Text)
            //{
            //    p.updatemode(str, lvstockin.Items[lvstockin.FocusedItem.Index].SubItems[2].Text, 1);
            //    master.AddNewTab(p);
            //}
        }

        private void lvstockin_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                setformstockin();
            }
            catch
            {
            }
        }

        private void lvstockout_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                setformstockout();
            }
            catch
            {
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnok_MouseEnter(object sender, EventArgs e)
        {
            btnok.UseVisualStyleBackColor = false;
            btnok.BackColor = Color.FromArgb(94, 191, 174);
            btnok.ForeColor = Color.White;
        }

        private void btnok_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void btnok_MouseLeave(object sender, EventArgs e)
        {
            btnok.UseVisualStyleBackColor = true;
            btnok.BackColor = Color.FromArgb(51, 153, 255);
            btnok.ForeColor = Color.White;
        }

        private void btnok_Enter(object sender, EventArgs e)
        {
            btnok.UseVisualStyleBackColor = false;
            btnok.BackColor = Color.FromArgb(94, 191, 174);
            btnok.ForeColor = Color.White;
        }

        private void btnok_Leave(object sender, EventArgs e)
        {
            btnok.UseVisualStyleBackColor = true;
            btnok.BackColor = Color.FromArgb(51, 153, 255);
            btnok.ForeColor = Color.White;
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                 master.RemoveCurrentTab();
                //this.Close();
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

        private void lvstockin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    setformstockin();
                }
                catch
                {
                }
            }
        }

        private void lvstockout_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    setformstockout();
                }
                catch
                {
                }
            }
        }
    }
}
