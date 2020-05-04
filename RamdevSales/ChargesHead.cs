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
using System.Diagnostics;

namespace RamdevSales
{
    public partial class ChargesHead : Form
    {
       // private DefaultPurchase purchase;
        private string p;
        Connection conn = new Connection();
        public ChargesHead()
        {
            InitializeComponent();
            pageLoad();
        }

        //public ChargesHead(DefaultPurchase purchase, string p)
        //{
        //    // TODO: Complete member initialization
        //    InitializeComponent();
        //    this.purchase = purchase;
        //    this.p = p;
        //    pageLoad();
        //}

        public ChargesHead(DefaultSale defaultSale, string p)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this.defaultSale = defaultSale;
            this.p = p;
            pageLoad();
        }
        public static string pvc;
        public ChargesHead(DefaultSale defaultSale, string p, Master master, TabControl tabControl, string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.defaultSale = defaultSale;
            this.p = p;
            this.master = master;
            this.tabControl = tabControl;
            pageLoad();
            pvc = activecontroal;
            flagforbind = 1;
            bindchargeshead();
        }

        public ChargesHead(DefaultSaleOrder defaultSaleOrder, string p, Master master, TabControl tabControl, string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.defaultSaleOrder = defaultSaleOrder;
            this.p = p;
            this.master = master;
            this.tabControl = tabControl;
            pageLoad();
            pvc = activecontroal;
            flagforbind = 1;
        }

        public ChargesHead(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            pageLoad();
            this.master = master;
            this.tabControl = tabControl;
        }

        public ChargesHead(SalePurchaseOrderSimpleformate salePurchaseOrderSimpleformate, string p, Master master, TabControl tabControl, string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.salePurchaseOrderSimpleformate = salePurchaseOrderSimpleformate;
            this.p = p;
            this.master = master;
            this.tabControl = tabControl;
            pageLoad();
            pvc = activecontroal;
            flagforbind = 1;
        }

        public ChargesHead(GSTVouchers gSTVouchers, string p, Master master, TabControl tabControl, string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.gSTVouchers = gSTVouchers;
            this.p = p;
            this.master = master;
            this.tabControl = tabControl;
            this.activecontroal = activecontroal;
            pageLoad();
            pvc = activecontroal;
            flagforbind = 1;
        }

        private void pageLoad()
        {
            listviewbind();
            bindaccount();
            bindchargeshead();
        }

        private void listviewbind()
        {
            LVFO.Columns.Add("Charges Head", 300, HorizontalAlignment.Center);
            LVFO.Columns.Add("Account", 300, HorizontalAlignment.Left);
            LVFO.Columns.Add("Type", 114, HorizontalAlignment.Left);

            listviewdatabind();
        }

        private void listviewdatabind()
        {
            LVFO.Items.Clear();
            DataTable dt = conn.getdataset("select * from billsundry where isactive=1");
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                LVFO.Items.Add(dt.Rows[i].ItemArray[1].ToString());
                LVFO.Items[i].SubItems.Add(dt.Rows[i].ItemArray[8].ToString());
                LVFO.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
               
            }
        }
        private void bindaccount()
        {
            DataTable dt = conn.getdataset("select ClientID,AccountName from ClientMaster where isactive=1 order by AccountName");
            cmbaccount.ValueMember = "ClientID";
            cmbaccount.DisplayMember = "AccountName";
            cmbaccount.DataSource = dt;
            cmbaccount.SelectedIndex = -1;
        }

        private void bindchargeshead()
        {
            DataTable dt = conn.getdataset("select id,applyon from chargesheadapplyon where isactive=1");
            cmbperof.ValueMember = "id";
            cmbperof.DisplayMember = "applyon";
            cmbperof.DataSource = dt;
            cmbperof.SelectedIndex = -1;
        }
        string sundryid;
        private DefaultSale defaultSale;
        private Master master;
        private TabControl tabControl;
        private DefaultSaleOrder defaultSaleOrder;
        int flagforbind = 0;
        public void open()
        {
            DataTable dt = conn.getdataset("select * from billsundry where billsundryname like '%" + LVFO.Items[LVFO.FocusedItem.Index].SubItems[0].Text + "%' and accountname like '%" + LVFO.Items[LVFO.FocusedItem.Index].SubItems[1].Text + "%' and isactive=1");
            sundryid = dt.Rows[0]["BillSundryid"].ToString();
            txtheading.Text = dt.Rows[0]["BillSundryName"].ToString();
            txtcalculate.Text = dt.Rows[0]["Percentage"].ToString();
            cmbaccount.Text = dt.Rows[0]["AccountName"].ToString();

            if (dt.Rows[0]["BillSundryType"].ToString() == "+")
            {
                rdplus.Checked = true;
            }
            else
            {
                rdminus.Checked = true;
            }
            if (dt.Rows[0]["BillSundryType"].ToString() == "+")
            {
                rdplus.Checked = true;
            }
            else
            {
                rdminus.Checked = true;
            }


            if (dt.Rows[0]["Symbol"].ToString() == "0")
            {
                rdabsoluteamt.Checked = true;
            }
            else if (dt.Rows[0]["Symbol"].ToString() == "1")
            {
                rdonqty.Checked = true;
            }
            else if (dt.Rows[0]["Symbol"].ToString() == "%")
            {
                rdper.Checked = true;
            }
            else if (dt.Rows[0]["Symbol"].ToString() == "2")
            {
                rdbags.Checked = true;
            }
            else if (dt.Rows[0]["Symbol"].ToString() == "3")
            {
                rdonweight.Checked = true;
            }
            if (dt.Rows[0]["OT3"].ToString() == "1")
            {
                pnlgst.Visible = true;
                chkgst.Checked = true;
                txthsn.Text = dt.Rows[0]["OT2"].ToString();
            }
            else
            {
                pnlgst.Visible = false;
            }
            cmbcosting.Text = dt.Rows[0]["OT1"].ToString();
            txtapplyon.Text = dt.Rows[0]["ON1"].ToString();
            cmbperof.Text = dt.Rows[0]["Applyon"].ToString();
            btnsave.Text = "Update";
        }
        private void LVFO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            open();
        }

        private void txtheading_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtcalculate.Focus();
            }
        }

        private void txtcalculate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbaccount.Focus();
            }
        }
        public static string s;
        private void cmbaccount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbaccount.Items.Count; i++)
                {
                    s = cmbaccount.GetItemText(cmbaccount.Items[i]);
                    if (s == cmbaccount.Text)
                    {
                        inList = true;
                        cmbaccount.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbaccount.Text = "";
                }
                rdplus.Focus();
                rdplus.ForeColor = Color.Green;
            }
        }

        private void txtapplyon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbperof.Focus();
            }
        }

        private void cmbperof_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbperof.Items.Count; i++)
                {
                    s = cmbperof.GetItemText(cmbperof.Items[i]);
                    if (s == cmbperof.Text)
                    {
                        inList = true;
                        cmbperof.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbperof.Text = "";
                }
                cmbcosting.Focus();
            }
        }

        private void cmbcosting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbcosting.Items.Count; i++)
                {
                    s = cmbcosting.GetItemText(cmbcosting.Items[i]);
                    if (s == cmbcosting.Text)
                    {
                        inList = true;
                        cmbcosting.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbcosting.Text = "";
                }
                //btnsave.Focus();
                chkgst.Focus();
                chkgst.ForeColor = Color.Green;
            }
        }

        private void LVFO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                open();
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (btnsave.Text == "Update")
            {
                string charges, fedas = "";
                if (rdplus.Checked == true)
                {
                    charges = "+";
                }
                else
                {
                    charges = "-";
                }

                if (rdabsoluteamt.Checked == true)
                {
                    fedas = "0";
                }
                else if (rdonqty.Checked == true)
                {
                    fedas = "1";
                }
                else if (rdper.Checked == true)
                {
                    fedas = "%";
                }
                else if (rdbags.Checked == true)
                {
                    fedas = "2";
                }
                else if (rdonweight.Checked == true)
                {
                    fedas = "3";
                }
                string gst;
                if (chkgst.Checked == true)
                {
                    gst = "1";
                }
                else
                {
                    gst = "0";
                }
                conn.execute("UPDATE [dbo].[BillSundry]SET [BillSundryName] = '" + txtheading.Text + "',[Percentage] = '" + txtcalculate.Text + "',[Symbol] = '" + fedas + "',[BillSundryType] = '" + charges + "',[IE] = 'E',[ApplyOn] = '" + cmbperof.Text + "',[AccountID] = '" + cmbaccount.SelectedValue + "',[AccountName] = '" + cmbaccount.Text + "',[OT1] = '" + cmbcosting.Text + "',[OT2] = '"+txthsn.Text+"',[OT3] = '"+gst+"',[OT4] = '',[OT5] = '',[OT6] = '',[OT7] = '',[OT8] = '',[OT9] = '',[OT10] = '',[ON1] = '" + txtapplyon.Text + "',[isactive]=1 WHERE billsundryid='" + sundryid + "'");
                clearall();
                listviewdatabind();
                btnsave.Text = "Save";
                MessageBox.Show("Update successfully");
                if (flagforbind == 1)
                {
                   // defaultSale.bindperticular();
                    //defaultSaleOrder.bindperticular();
                    try
                    {
                        gSTVouchers.bindperticular();
                    }
                    catch
                    {
                    }
                    try
                    {
                        defaultSale.bindperticular();
                    }
                    catch
                    {
                    }
                    try
                    {
                        defaultSaleOrder.bindperticular();
                    }
                    catch
                    {
                    }
                    try
                    {
                        salePurchaseOrderSimpleformate.bindperticular();
                    }
                    catch
                    {
                    }
                    if (string.IsNullOrEmpty(pvc) == true)
                    {
                        master.RemoveCurrentTab();
                    }
                    else
                    {
                        master.RemoveCurrentTab1(pvc, txtheading.Text);
                    }
                }
                else
                {
                    txtheading.Focus();
                    this.ActiveControl = txtheading;
                }
                txtheading.Text = "";
               
               
              //  conn.execute("INSERT INTO [dbo].[BillSundry]([BillSundryName],[Percentage],[Symbol],[BillSundryType],[IE],[ApplyOn],[AccountID],[AccountName],[OT1],[OT2],[OT3],[OT4],[OT5],[OT6],[OT7],[OT8],[OT9],[OT10],[ON1])VALUES(,,,,'E',,,,,'','','','','','','','','',");
            }
            else
            {
                string charges, fedas = "";
                if (rdplus.Checked == true)
                {
                    charges = "+";
                }
                else
                {
                    charges = "-";
                }

                if (rdabsoluteamt.Checked == true)
                {
                    fedas = "0";
                }
                else if (rdonqty.Checked == true)
                {
                    fedas = "1";
                }
                else if (rdper.Checked == true)
                {
                    fedas = "%";
                }
                else if (rdbags.Checked == true)
                {
                    fedas = "2";
                }
                else if (rdonweight.Checked == true)
                {
                    fedas = "3";
                }
                string gst1;
                if (chkgst.Checked == true)
                {
                    gst1 = "1";
                }
                else
                {
                    gst1 = "0";
                }
                conn.execute("INSERT INTO [dbo].[BillSundry]([BillSundryName],[Percentage],[Symbol],[BillSundryType],[IE],[ApplyOn],[AccountID],[AccountName],[OT1],[OT2],[OT3],[OT4],[OT5],[OT6],[OT7],[OT8],[OT9],[OT10],[ON1],[isactive])VALUES('" + txtheading.Text + "','" + txtcalculate.Text + "','" + fedas + "','" + charges + "','E','" + cmbperof.Text + "','" + cmbaccount.SelectedValue + "','" + cmbaccount.Text + "','" + cmbcosting.Text + "','"+txthsn.Text+"','"+gst1+"','','','','','','','','" + txtapplyon.Text + "','1')");
                clearall();
                listviewdatabind();
                MessageBox.Show("Insert successfully");
                if (flagforbind == 1)
                {
                    try
                    {
                        gSTVouchers.bindperticular();
                    }
                    catch
                    {
                    }
                    try
                    {
                        defaultSale.bindperticular();
                    }
                    catch
                    {
                    }
                    try
                    {
                        defaultSaleOrder.bindperticular();
                    }
                    catch
                    {
                    }
                    try
                    {
                        salePurchaseOrderSimpleformate.bindperticular();
                    }
                    catch
                    {
                    }
                    if (string.IsNullOrEmpty(pvc) == true)
                    {
                        master.RemoveCurrentTab();
                    }
                    else
                    {
                        master.RemoveCurrentTab1(pvc, txtheading.Text);
                    }
                }
                else
                {
                    txtheading.Focus();
                    this.ActiveControl = txtheading;
                }
                txtheading.Text = "";
            }
        }

        public void clearall()
        {
            txtapplyon.Text = "";
            txtcalculate.Text="";
          
            cmbaccount.Text = "";
            cmbcosting.Text = "";
            cmbperof.Text = "";
            txthsn.Text = "";
            chkgst.Checked = false;

        }
        internal void Update(int p, string iid)
        {
            DataTable dt = conn.getdataset("select * from billsundry where billsundryname like '%" + iid+ "%' and isactive=1");
            sundryid = dt.Rows[0]["BillSundryid"].ToString();
            txtheading.Text = dt.Rows[0]["BillSundryName"].ToString();
            txtcalculate.Text = dt.Rows[0]["Percentage"].ToString();
          
            cmbaccount.Text = dt.Rows[0]["AccountName"].ToString();
            if (dt.Rows[0]["BillSundryType"].ToString() == "+")
            {
                rdplus.Checked = true;
            }
            else
            {
                rdminus.Checked = true;
            }
            if (dt.Rows[0]["BillSundryType"].ToString() == "+")
            {
                rdplus.Checked = true;
            }
            else
            {
                rdminus.Checked = true;
            }
            if (dt.Rows[0]["Symbol"].ToString() == "0")
            {
                rdabsoluteamt.Checked = true;
            }
            else if (dt.Rows[0]["Symbol"].ToString() == "1")
            {
                rdonqty.Checked = true;
            }
            else if (dt.Rows[0]["Symbol"].ToString() == "%")
            {
                rdper.Checked = true;
            }
            else if (dt.Rows[0]["Symbol"].ToString() == "2")
            {
                rdbags.Checked = true;
            }
            else if (dt.Rows[0]["Symbol"].ToString() == "3")
            {
                rdonweight.Checked = true;
            }
           
            cmbcosting.Text = dt.Rows[0]["OT1"].ToString();
            txtapplyon.Text = dt.Rows[0]["ON1"].ToString();
            cmbperof.Text = dt.Rows[0]["Applyon"].ToString();
            btnsave.Text = "Update";
            if (dt.Rows[0]["OT3"].ToString() == "1")
            {

                chkgst.Checked = true;
                pnlgst.Visible = true;
                txthsn.Text = dt.Rows[0]["OT2"].ToString();
            }
            else
            {
                pnlgst.Visible = false;
            }

        }
        private void btndelete_Click(object sender, EventArgs e)
        {
             DialogResult dr = MessageBox.Show("Do you want to Delete this Record?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
             if (dr == DialogResult.Yes)
             {
                 conn.execute("update billsundry set isactive=0 where billsundryid='" + sundryid + "'");
                 MessageBox.Show("Record Deleted Successfully");
                 clearall();
                 listviewdatabind();
             }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            try
            {
                 DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                 if (dr == DialogResult.Yes)
                 {
                     if (string.IsNullOrEmpty(pvc) == true)
                     {
                         master.RemoveCurrentTab();
                     }
                     else
                     {
                         master.RemoveCurrentTab1(pvc, txtheading.Text);
                     }
                 }
            }
            catch
            {
            }
        }

        private void rdper_CheckedChanged(object sender, EventArgs e)
        {
            if (rdper.Checked == true)
            {
                pnlapplyon.Visible = true;
            }
        }

        private void rdabsoluteamt_CheckedChanged(object sender, EventArgs e)
        {
            if (rdabsoluteamt.Checked == true)
            {
                pnlapplyon.Visible = false;
            }
        }

        private void rdonqty_CheckedChanged(object sender, EventArgs e)
        {
            if (rdonqty.Checked == true)
            {
                pnlapplyon.Visible = false;
            }
        }

        private void rdbags_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbags.Checked == true)
            {
                pnlapplyon.Visible = false;
            }
        }

        private void rdonweight_CheckedChanged(object sender, EventArgs e)
        {
            if (rdonweight.Checked == true)
            {
                pnlapplyon.Visible = false;
            }
        }

        

        private void chkgst_CheckedChanged(object sender, EventArgs e)
        {
            if (chkgst.Checked == true)
            {
                pnlgst.Visible = true;
            }
            else
            {
                pnlgst.Visible = false;
            }
        }

        private void ChargesHead_Load(object sender, EventArgs e)
        {
            if (chkgst.Checked == true)
            {
                pnlgst.Visible = true;
            }
            else
            {
                pnlgst.Visible = false;
            }
            this.ActiveControl = txtheading;
            //set the interval  and start the timer
           // timer1.Interval = 1000;
           // timer1.Start();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                // tabControl.TabPages.Remove(tabControl.SelectedTab);
                DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    if (string.IsNullOrEmpty(pvc) == true)
                    {
                        master.RemoveCurrentTab();
                    }
                    else
                    {
                        master.RemoveCurrentTab1(pvc, txtheading.Text);
                    }
                }
                return true;
            }


            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtheading_Enter(object sender, EventArgs e)
        {
            txtheading.BackColor = Color.LightYellow;
        }

        private void txtheading_Leave(object sender, EventArgs e)
        {
            txtheading.BackColor = Color.White;
        }

        private void txtcalculate_Enter(object sender, EventArgs e)
        {
            txtcalculate.BackColor = Color.LightYellow;
        }

        private void txtcalculate_Leave(object sender, EventArgs e)
        {
            txtcalculate.BackColor = Color.White;
        }

        private void cmbaccount_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbaccount.SelectedIndex = 0;
                cmbaccount.DroppedDown = true;
                //cmbpaymode.ForeColor = Color.LightYellow;
            }
            catch
            {
            }
        }

        private void cmbaccount_Leave(object sender, EventArgs e)
        {
            cmbaccount.Text = s;
            cmbaccount.BackColor = Color.White;
        }

        private void txtapplyon_Enter(object sender, EventArgs e)
        {
            txtapplyon.BackColor = Color.LightYellow;
        }

        private void txtapplyon_Leave(object sender, EventArgs e)
        {
            txtapplyon.BackColor = Color.White;
        }

        private void cmbperof_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbperof.SelectedIndex = 0;
                cmbperof.DroppedDown = true;
                //cmbpaymode.ForeColor = Color.LightYellow;
            }
            catch
            {
            }
        }

        private void cmbperof_Leave(object sender, EventArgs e)
        {
            cmbperof.Text = s;
            cmbperof.BackColor = Color.White;
        }

        private void cmbcosting_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbcosting.SelectedIndex = 0;
                cmbcosting.DroppedDown = true;
                //cmbpaymode.ForeColor = Color.LightYellow;
            }
            catch
            {
            }
        }

        private void cmbcosting_Leave(object sender, EventArgs e)
        {
            cmbcosting.Text = s;
           // cmbcosting.BackColor = Color.White;
        }

        private void txthsn_Enter(object sender, EventArgs e)
        {
            txthsn.BackColor = Color.LightYellow;
        }

        private void txthsn_Leave(object sender, EventArgs e)
        {
            txthsn.BackColor = Color.White;
        }

        private void lblbarcord_Click(object sender, EventArgs e)
        {

        }

        private void txtheading_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtcalculate_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbaccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbaccount.Items.Count; i++)
                {
                    s = cmbaccount.GetItemText(cmbaccount.Items[i]);
                    if (s == cmbaccount.Text)
                    {
                        inList = true;
                        cmbaccount.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbaccount.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pnlapplyon_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void cmbcosting_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbcosting.Items.Count; i++)
                {
                    s = cmbcosting.GetItemText(cmbcosting.Items[i]);
                    if (s == cmbcosting.Text)
                    {
                        inList = true;
                        cmbcosting.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbcosting.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void pnlgst_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LVFO_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnsave_MouseEnter(object sender, EventArgs e)
        {
            btnsave.UseVisualStyleBackColor = false;
            btnsave.BackColor = Color.YellowGreen;
            btnsave.ForeColor = Color.White;
        }

        private void btnsave_MouseLeave(object sender, EventArgs e)
        {
            btnsave.UseVisualStyleBackColor = true;
            btnsave.BackColor = Color.FromArgb(51, 153, 255);
            btnsave.ForeColor = Color.White;
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

        private void btnsave_Enter(object sender, EventArgs e)
        {
            btnsave.UseVisualStyleBackColor = false;
            btnsave.BackColor = Color.YellowGreen;
            btnsave.ForeColor = Color.White;
        }

        private void btnsave_Leave(object sender, EventArgs e)
        {
            btnsave.UseVisualStyleBackColor = true;
            btnsave.BackColor = Color.FromArgb(51, 153, 255);
            btnsave.ForeColor = Color.White;
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

        private void txthsn_TextChanged(object sender, EventArgs e)
        {

        }
        string searchstr;
        private SalePurchaseOrderSimpleformate salePurchaseOrderSimpleformate;
        private string activecontroal;
        private GSTVouchers gSTVouchers;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //empty the string for every 1 seconds
           // searchstr = "";
        }

        private void cmbaccount_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbaccount.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbaccount.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void cmbperof_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbperof.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbperof.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void cmbcosting_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbcosting.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbcosting.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void rdplus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                rdplus.ForeColor = Color.Black;
                rdper.Focus();
                rdper.ForeColor = Color.Green;
            }
            if (e.KeyCode == Keys.Right)
            {
                rdplus.ForeColor = Color.Black;
                rdminus.ForeColor = Color.Green;
            }
        }

        private void rdminus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                rdminus.ForeColor = Color.Black;
                rdper.Focus();
                rdper.ForeColor = Color.Green;
            }
        }

        private void rdper_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                rdper.ForeColor = Color.Black;
                txtapplyon.Focus();
            }
        }

        private void rdabsoluteamt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //txtapplyon.Focus();
                cmbcosting.Focus();
            }
        }

        private void rdonqty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //txtapplyon.Focus();
                cmbcosting.Focus();
            }
        }

        private void rdbags_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               // txtapplyon.Focus();
                cmbcosting.Focus();
            }
        }

        private void rdonweight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               // txtapplyon.Focus();
                cmbcosting.Focus();
            }
        }

        private void chkgst_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (chkgst.Checked == true)
                {
                    txthsn.Focus();
                }
                else
                {
                    btnsave.Focus();
                }
            }
        }

        private void txthsn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnsave.Focus();
            }
        }

        private void rdper_Enter(object sender, EventArgs e)
        {
            rdper.ForeColor = Color.Green;
        }

        private void rdper_Leave(object sender, EventArgs e)
        {
            rdper.ForeColor = Color.Black;
        }

        private void rdabsoluteamt_Enter(object sender, EventArgs e)
        {
            rdabsoluteamt.ForeColor = Color.Green;
        }

        private void rdabsoluteamt_Leave(object sender, EventArgs e)
        {
            rdabsoluteamt.ForeColor = Color.Black;
        }

        private void rdonqty_Enter(object sender, EventArgs e)
        {
            rdonqty.ForeColor = Color.Green;
        }

        private void rdonqty_Leave(object sender, EventArgs e)
        {
            rdonqty.ForeColor = Color.Black;
        }

        private void rdplus_Enter(object sender, EventArgs e)
        {
            rdplus.ForeColor = Color.Green;
        }

        private void rdplus_Leave(object sender, EventArgs e)
        {
            rdplus.ForeColor = Color.Black;
        }

        private void rdminus_Enter(object sender, EventArgs e)
        {
            rdminus.ForeColor = Color.Green;
        }

        private void rdminus_Leave(object sender, EventArgs e)
        {
            rdminus.ForeColor = Color.Black;
        }

        private void rdbags_Enter(object sender, EventArgs e)
        {
            rdbags.ForeColor = Color.Green;
        }

        private void rdbags_Leave(object sender, EventArgs e)
        {
            rdbags.ForeColor = Color.Black;
        }

        private void rdonweight_Enter(object sender, EventArgs e)
        {
            rdonweight.ForeColor = Color.Green;
        }

        private void rdonweight_Leave(object sender, EventArgs e)
        {
            rdonweight.ForeColor = Color.Black;
        }

        private void chkgst_Enter(object sender, EventArgs e)
        {
            chkgst.ForeColor = Color.Green;
        }

        private void chkgst_Leave(object sender, EventArgs e)
        {
            chkgst.ForeColor = Color.Black;
        }

        private void cmbperof_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbperof.Items.Count; i++)
                {
                    s = cmbperof.GetItemText(cmbperof.Items[i]);
                    if (s == cmbperof.Text)
                    {
                        inList = true;
                        cmbperof.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbperof.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }
















        


    }
}
