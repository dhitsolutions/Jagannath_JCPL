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
    public partial class serialnotrackingreport : Form
    {
        private Master master;
        private TabControl tabControl;
        Connection conn = new Connection();
        public serialnotrackingreport()
        {
            InitializeComponent();
        }

        public serialnotrackingreport(Master master, TabControl tabControl)
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
        private void serialnotrackingreport_Load(object sender, EventArgs e)
        {
            try
            {
                lvcomplain.Columns.Add("Complain ID", 150, HorizontalAlignment.Left);
                lvcomplain.Columns.Add("Item Name", 300, HorizontalAlignment.Left);
                lvcomplain.Columns.Add("Serial No",200, HorizontalAlignment.Left);
                lvcomplain.Columns.Add("Status",300, HorizontalAlignment.Left);
                lvsendtocompany.Columns.Add("sendtocomplainID",0, HorizontalAlignment.Left);
                lvsendtocompany.Columns.Add("Complain ID", 150, HorizontalAlignment.Left);
                lvsendtocompany.Columns.Add("Item Name", 300, HorizontalAlignment.Left);
                lvsendtocompany.Columns.Add("Serial No", 200, HorizontalAlignment.Left);
                lvsendtocompany.Columns.Add("Status", 300, HorizontalAlignment.Left);
                lvreceivefromcompany.Columns.Add("receiveformcompanyID",0, HorizontalAlignment.Left);
                lvreceivefromcompany.Columns.Add("Complain ID", 150, HorizontalAlignment.Left);
                lvreceivefromcompany.Columns.Add("Item Name", 300, HorizontalAlignment.Left);
                lvreceivefromcompany.Columns.Add("Serial No", 200, HorizontalAlignment.Left);
                lvreceivefromcompany.Columns.Add("Status", 300, HorizontalAlignment.Left);
                lvsendtocustomer.Columns.Add("sendtocustomerID",0, HorizontalAlignment.Left);
                lvsendtocustomer.Columns.Add("Complain ID", 150, HorizontalAlignment.Left);
                lvsendtocustomer.Columns.Add("Item Name", 300, HorizontalAlignment.Left);
                lvsendtocustomer.Columns.Add("Serial No", 200, HorizontalAlignment.Left);
                lvsendtocustomer.Columns.Add("Status", 300, HorizontalAlignment.Left);
                this.ActiveControl = txtserialno;
            }
            catch
            {
            }
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
        ListViewItem li;
        public void binddata()
        {
            try
            {
                string oldserial = conn.ExecuteScalar("select serialno from tblitemreceivefromcompany where isactive=1 and newserialno='"+txtserialno.Text+"'");
                if (!string.IsNullOrEmpty(oldserial))
                {
                    lblnewserialno.Visible = true;
                    lblnewserialno.Text ="New Serial No="+ txtserialno.Text;
                    txtserialno.Text = oldserial;
                    
                }
               DataTable dt=  conn.getdataset("select * from tblitemcomplainmaster where isactive=1 and serialno='"+txtserialno.Text+"'");
               if (dt.Rows.Count > 0)
               {
                   lvcomplain.Items.Clear();
                   for (int i = 0; i < dt.Rows.Count; i++)
                   {
                       li = lvcomplain.Items.Add(dt.Rows[i]["ComplainID"].ToString());
                       li.SubItems.Add(dt.Rows[i]["itemname"].ToString());
                       li.SubItems.Add(dt.Rows[i]["serialno"].ToString());
                       li.SubItems.Add("Complain Received");
                   }
                   txtcurrentstatus.Text = "Complain Received";
               }
               DataTable dt1 = conn.getdataset("select * from tblsendtocompanyitemmaster where isactive=1 and serialno='" + txtserialno.Text + "'");
               if (dt1.Rows.Count > 0)
               {
                   lvsendtocompany.Items.Clear();
                   for (int i = 0; i < dt1.Rows.Count; i++)
                   {
                       li = lvsendtocompany.Items.Add(dt1.Rows[i]["sendtocompanyID"].ToString());
                       li.SubItems.Add(dt1.Rows[i]["ComplainID"].ToString());
                       li.SubItems.Add(dt1.Rows[i]["itemname"].ToString());
                       li.SubItems.Add(dt1.Rows[i]["serialno"].ToString());
                       li.SubItems.Add("Send To Company");
                   }
                   txtcurrentstatus.Text = "Send To Company";
               }
               DataTable dt2 = conn.getdataset("select * from tblitemreceivefromcompany where isactive=1 and serialno='" + txtserialno.Text + "'");
               if (dt2.Rows.Count > 0)
               {
                   lvreceivefromcompany.Items.Clear();
                   for (int i = 0; i < dt2.Rows.Count; i++)
                   {
                       li = lvreceivefromcompany.Items.Add(dt2.Rows[i]["receivefromcompanyID"].ToString());
                       li.SubItems.Add(dt2.Rows[i]["ComplainID"].ToString());
                       li.SubItems.Add(dt2.Rows[i]["itemname"].ToString());
                       li.SubItems.Add(dt2.Rows[i]["serialno"].ToString());
                       li.SubItems.Add("Product Received From Company");
                   }
                   txtcurrentstatus.Text = "Product Received From Company";
               }
               DataTable dt3 = conn.getdataset("select * from tblitemsendtocustomer where isactive=1 and serialno='" + txtserialno.Text + "'");
               if (dt3.Rows.Count > 0)
               {
                   lvsendtocustomer.Items.Clear();
                   for (int i = 0; i < dt3.Rows.Count; i++)
                   {
                       li = lvsendtocustomer.Items.Add(dt3.Rows[i]["sendtocustomerID"].ToString());
                       li.SubItems.Add(dt3.Rows[i]["ComplainID"].ToString());
                       li.SubItems.Add(dt3.Rows[i]["itemname"].ToString());
                       li.SubItems.Add(dt3.Rows[i]["serialno"].ToString());
                       li.SubItems.Add("Product Sent To Customer");
                   }
                   txtcurrentstatus.Text = "Product Sent To Customer";
               }
               if (lvcomplain.Items.Count == 0 && lvreceivefromcompany.Items.Count == 0 && lvsendtocompany.Items.Count == 0 && lvsendtocustomer.Items.Count == 0)
               {
                   txtcurrentstatus.Text = "Serail No Not Available";
               }
            }
            catch
            {
            }
        }
        private void btnok_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtserialno.Text))
            {
                lblnewserialno.Visible = false;
                lvcomplain.Items.Clear();
                lvsendtocompany.Items.Clear();
                lvreceivefromcompany.Items.Clear();
                lvsendtocustomer.Items.Clear();
                binddata();
            }
            else
            {
                MessageBox.Show("Enter Serial No");
            }
        }

        private void txtserialno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnok.Focus();
            }
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

        private void btnok_MouseEnter(object sender, EventArgs e)
        {
            btnok.UseVisualStyleBackColor = false;
            btnok.BackColor = Color.FromArgb(94, 191, 174);
            btnok.ForeColor = Color.White;
        }

        private void btnok_MouseLeave(object sender, EventArgs e)
        {
            btnok.UseVisualStyleBackColor = true;
            btnok.BackColor = Color.FromArgb(51, 153, 255);
            btnok.ForeColor = Color.White;
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
        public static string lvcompany = "";
        public void lvcompany1()
        {
            try
            {
                try
                {
                    this.Enabled = false;
                    lvcompany = lvcomplain.Items[lvcomplain.FocusedItem.Index].SubItems[0].Text;

                    frmComplainMasterData dlg = new frmComplainMasterData(master, tabControl);


                    dlg.Update(1, lvcompany);
                    master.AddNewTab(dlg);
                    // dlg.Show();
                }
                finally
                {
                    this.Enabled = true;
                }
            }
            catch
            {
            }
        }
        public static string lvsendtocompany11;
        public void lvsendtocompany1()
        {
            try
            {
                try
                {
                    this.Enabled = false;
                    lvsendtocompany11 = lvsendtocompany.Items[lvsendtocompany.FocusedItem.Index].SubItems[0].Text;

                    frmSentToCompany dlg = new frmSentToCompany(master, tabControl);


                    dlg.Update(1, lvsendtocompany11);
                    master.AddNewTab(dlg);
                    // dlg.Show();
                }
                finally
                {
                    this.Enabled = true;
                }
            }
            catch
            {
            }
        }
        private void lvcomplain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            lvcompany1();
        }

        private void lvcomplain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lvcompany1();
            }
        }

        private void lvsendtocompany_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            lvsendtocompany1();
        }

        private void lvsendtocompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lvsendtocompany1();
            }
        }
        public static string lvrevcompany;
        public void receivefrocompany()
        {
            try
            {
                try
                {
                    this.Enabled = false;
                    lvrevcompany = lvreceivefromcompany.Items[lvreceivefromcompany.FocusedItem.Index].SubItems[0].Text;

                    frmReceiveFromCompany dlg = new frmReceiveFromCompany(master, tabControl);


                    dlg.Update(1, lvrevcompany);
                    master.AddNewTab(dlg);
                    // dlg.Show();
                }
                finally
                {
                    this.Enabled = true;
                }
            }
            catch
            {
            }
        }
        private void lvreceivefromcompany_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            receivefrocompany();
        }

        private void lvreceivefromcompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                receivefrocompany();
            }
        }
        public static string iid;
        public void open()
        {
            try
            {
                try
                {
                    this.Enabled = false;
                    iid = lvsendtocustomer.Items[lvsendtocustomer.FocusedItem.Index].SubItems[0].Text;

                    frmSendToCustomer dlg = new frmSendToCustomer(master, tabControl);


                    dlg.Update(1, iid);
                    master.AddNewTab(dlg);
                    // dlg.Show();
                }
                finally
                {
                    this.Enabled = true;
                }
            }
            catch
            {
            }
        }
        private void lvsendtocustomer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            open();
        }

        private void lvsendtocustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                open();
            }
        }
    }
}
