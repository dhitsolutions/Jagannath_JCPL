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
    public partial class SelectBills : Form
    {
        private TextBox txtnetamt;
        private DataTable dt;
        Connection con = new Connection();
        data data = new data();
        private object p;
        private string p_2;
        public SelectBills()
        {
            InitializeComponent();
        }
        private QReceipt qreceipt=null;
        public static string buttonname = "";
        public static string billno = "";
        public SelectBills(TextBox txtnetamt, DataTable dt, object p, string p_2, QReceipt qreceipt1,string btnname,string billno1)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.txtnetamt = txtnetamt;
            this.dt = dt;
            this.p = p;
            this.p_2 = p_2;
            qreceipt = qreceipt1 as QReceipt;
            buttonname = btnname;
            billno = billno1;
        }

        public string EnteredText
        {
            get
            {
                return ("");
            }
        }

        private void SelectBills_Load(object sender, EventArgs e)
        {
            LVFO.CheckBoxes = true;
            LVFO.Columns.Add("Date", 150, HorizontalAlignment.Left);
            LVFO.Columns.Add("Bill NO", 80, HorizontalAlignment.Left);
            LVFO.Columns.Add("Bill Amount", 114, HorizontalAlignment.Right);
            LVFO.Columns.Add("Received Amount", 114, HorizontalAlignment.Right);
            LVFO.Columns.Add("Bal Amount", 114, HorizontalAlignment.Right);

            Popup();
        }

        private void Popup()
        {
            if (Convert.ToDouble(txtnetamt.Text) > 0)
            {
                DataTable maindt = new DataTable();
                maindt.Columns.Add("Date", typeof(string));
                maindt.Columns.Add("Bill NO", typeof(string));
                maindt.Columns.Add("Bill Amount", typeof(string));
                maindt.Columns.Add("Received Amount", typeof(string));
                maindt.Columns.Add("Bal Amount", typeof(string));
                Double paidval = Convert.ToDouble(txtnetamt.Text);
                Double billbal = 0;
                if (buttonname == "Update")
                {
                    DataTable dt1 = con.getdataset("Select * from ref where isactive=1 and accountid='" + p + "' and VchNo='" + billno + "' and (TransactionType='R' or TransactionType='Bank Entry') order by OT1");
                    string value = "0";
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        DataTable ref1 = new DataTable();
                        if (value == "")
                        {
                            value = "0.00";
                        }

                        if (buttonname == "Update" && i == 0)
                        {
                            billbal = Math.Round((Convert.ToDouble(dt1.Rows[i]["OT3"].ToString()) - Convert.ToDouble(value)), 2);
                            billbal = billbal + Convert.ToDouble(value);
                        }
                        if (i == 0 && buttonname != "Update")
                        {
                            if (billbal < 0)
                            {
                                billbal = billbal * -1;
                            }
                            billbal = Math.Round((Convert.ToDouble(dt1.Rows[i]["OT3"].ToString()) - Convert.ToDouble(value)), 2);
                        }
                        else
                        {
                            if (Convert.ToDouble(value) == 0)
                            {
                                billbal = 0;
                            }
                            if (billbal < 0)
                            {
                                billbal = billbal * -1;
                            }
                            billbal = Math.Round((Convert.ToDouble(dt1.Rows[i]["OT3"].ToString()) - Convert.ToDouble(billbal)), 2);
                        }


                        if (billbal > 0)
                        {
                            Double paidbill = 0, balance = 0;
                            if (billbal > paidval || billbal == paidval)
                            {
                                paidbill = paidval;
                                balance = billbal - paidbill;
                                paidval = 0;

                            }
                            else if (billbal < paidval)
                            {
                                paidbill = billbal;
                                balance = billbal - paidbill;
                                paidval -= billbal;
                            }

                            maindt.Rows.Add(Convert.ToDateTime(dt1.Rows[i]["OT1"].ToString()).ToString("dd-MM-yyyy"), dt1.Rows[i]["OT2"].ToString(), billbal, paidbill, balance);


                        }
                    }
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    Double balance = Convert.ToDouble(value) - Convert.ToDouble(paidval);
                    //    maindt.Rows.Add(Convert.ToDateTime(dt.Rows[i][0].ToString()).ToString("dd-MM-yyyy"), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), paidval, balance);
                    //}
                }
                else
                {
                    DataTable dt1 = con.getdataset("Select * from ref where isactive=1 and accountid='" + p + "' and (TransactionType='R' or TransactionType='Bank Entry')");
                    String value = con.getsinglevalue("Select sum(OT4) from ref where isactive=1 and accountid='" + p + "' and (TransactionType='R' or TransactionType='Bank Entry')");
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (dt1.Rows[i]["OT2"].ToString() == dt.Rows[j]["billno"].ToString())
                            {
                                if (dt1.Rows[i]["OT6"].ToString() == "True")
                                {
                                    dt.Rows[j].Delete();
                                    dt.AcceptChanges();
                                    value = (Convert.ToDouble(value) - Convert.ToDouble(dt1.Rows[i]["OT4"].ToString())).ToString();
                                }
                                else
                                {
                                    dt.Rows[j]["totalnet"] = Convert.ToDouble(dt.Rows[j]["totalnet"].ToString()) - Convert.ToDouble(dt1.Rows[i]["OT4"].ToString());
                                    dt.AcceptChanges();
                                    value = (Convert.ToDouble(value) - Convert.ToDouble(dt1.Rows[i]["OT4"].ToString())).ToString();
                                }

                            }
                        }
                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataTable ref1 = new DataTable();
                        if (value == "")
                        {
                            value = "0.00";
                        }

                        if (buttonname == "Update" && i == 0)
                        {
                            billbal = Math.Round((Convert.ToDouble(dt.Rows[i][2].ToString()) - Convert.ToDouble(value)), 2);
                            billbal = billbal + Convert.ToDouble(value);
                        }

                        if (i == 0 && buttonname != "Update")
                        {
                            if (billbal < 0)
                            {
                                billbal = billbal * -1;
                            }
                            billbal = Math.Round((Convert.ToDouble(dt.Rows[i][2].ToString()) - Convert.ToDouble(value)), 2);
                        }
                        else
                        {
                            if (Convert.ToDouble(value) == 0)
                            {
                                billbal = 0;
                            }
                            if (billbal < 0)
                            {
                                billbal = billbal * -1;
                            }
                            billbal = Math.Round((Convert.ToDouble(dt.Rows[i][2].ToString()) - Convert.ToDouble(billbal)), 2);
                        }
                        if (billbal > 0)
                        {
                            Double paidbill = 0, balance = 0;
                            if (billbal > paidval || billbal == paidval)
                            {
                                paidbill = paidval;
                                balance = billbal - paidbill;
                                paidval = 0;

                            }
                            else if (billbal < paidval)
                            {
                                paidbill = billbal;
                                balance = billbal - paidbill;
                                paidval -= billbal;
                            }

                            maindt.Rows.Add(Convert.ToDateTime(dt.Rows[i][0].ToString()).ToString("dd-MM-yyyy"), dt.Rows[i][1].ToString(), billbal, paidbill, balance);


                        }

                    }
                }
                inq = 0;
                if (maindt.Rows.Count <= 0)
                {
                    this.Close();
                }
                for (int j = 0; j < maindt.Rows.Count; j++)
                {
                    ListViewItem li;
                    li = LVFO.Items.Add(maindt.Rows[j][0].ToString());
                    li.SubItems.Add(maindt.Rows[j][1].ToString());
                    li.SubItems.Add(maindt.Rows[j][2].ToString());
                    li.SubItems.Add(maindt.Rows[j][3].ToString());
                    li.SubItems.Add(maindt.Rows[j][4].ToString());

                    //   li.SubItems.Add(txtamount.Text);

                }
                for (int i = 0; i < LVFO.Items.Count; i++)
                {
                    if (Convert.ToDouble(LVFO.Items[i].SubItems[3].Text) > 0)
                        LVFO.Items[i].Checked = true;
                }
                inq = 1;
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
        int inq;
        private void LVFO_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            try
            {
                if (inq == 1)
                {
                    Double paidval = Convert.ToDouble(txtnetamt.Text);
                    foreach (ListViewItem Item in LVFO.Items)
                    {
                        if (Item != null)
                        {

                            if (Item.Checked == true)
                            {
                                Double billbal = Math.Round(Convert.ToDouble(Item.SubItems[2].Text), 2);
                                if (billbal > 0)
                                {
                                    Double paidbill = 0, balance = 0;
                                    if (billbal > paidval || billbal == paidval)
                                    {
                                        paidbill = paidval;
                                        balance = billbal - paidbill;
                                        paidval = 0;

                                    }
                                    else if (billbal < paidval)
                                    {
                                        paidbill = billbal;
                                        balance = billbal - paidbill;
                                        paidval -= billbal;
                                    }

                                    Item.SubItems[3].Text = paidbill.ToString();
                                    Item.SubItems[4].Text = balance.ToString();
                                    //   maindt.Rows.Add(Convert.ToDateTime(dt.Rows[i][0].ToString()).ToString("dd-MM-yyyy"), dt.Rows[i][1].ToString(), billbal, paidbill, balance);


                                }
                            }
                            else
                            {
                                Item.SubItems[3].Text = "0.00";
                                Item.SubItems[4].Text = Item.SubItems[2].Text;
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
             //   MessageBox.Show("Message from Itemchecked: " + ex.Message);
            }



        }

        private void LVFO_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void btncontinue_Click(object sender, EventArgs e)
        {
            string remark = "Against Bills : ";
            DataTable selectentry = new DataTable();
            selectentry.Columns.Add("Date", typeof(string));
            selectentry.Columns.Add("BillNO", typeof(string));
            selectentry.Columns.Add("BillAmount", typeof(string));
            selectentry.Columns.Add("ReceivedAmount", typeof(string));
            selectentry.Columns.Add("BalAmount", typeof(string));
            selectentry.Columns.Add("Status", typeof(bool));
            string billno="";
            foreach (ListViewItem Item in LVFO.Items)
            {
                if (Item != null)
                {

                    if (Item.Checked == true)
                    {
                        bool str;
                        if (Convert.ToDouble(Item.SubItems[4].Text) > 0)
                        {
                            //if full invoice not clear
                            str = false;
                        }
                        else
                        {
                            //if full invoice clear
                            str = true;
                        }
                        selectentry.Rows.Add(Item.SubItems[0].Text, Item.SubItems[1].Text, Item.SubItems[2].Text, Item.SubItems[3].Text, Item.SubItems[4].Text, str);
                        remark += "(" + Item.SubItems[3].Text + " Inv.No# " + Item.SubItems[1].Text + " @" + Item.SubItems[0].Text + ")";
                        billno += Item.SubItems[1].Text + ",";
                    }
                }
            }
            
            this.qreceipt.getremarks = remark;
            this.qreceipt.billnovalue = billno;
            qreceipt.dtselectedrow = selectentry;
            this.Close();
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnback_MouseEnter(object sender, EventArgs e)
        {
            btnback.UseVisualStyleBackColor = false;
            btnback.BackColor = Color.FromArgb(128, 128, 128);
            btnback.ForeColor = Color.White;
        }

        private void btnback_MouseLeave(object sender, EventArgs e)
        {
            btnback.UseVisualStyleBackColor = true;
            btnback.BackColor = Color.FromArgb(51, 153, 255);
            btnback.ForeColor = Color.White;
        }

        private void btnback_Enter(object sender, EventArgs e)
        {
            btnback.UseVisualStyleBackColor = false;
            btnback.BackColor = Color.FromArgb(128, 128, 128);
            btnback.ForeColor = Color.White;
        }

        private void btnback_Leave(object sender, EventArgs e)
        {
            btnback.UseVisualStyleBackColor = true;
            btnback.BackColor = Color.FromArgb(51, 153, 255);
            btnback.ForeColor = Color.White;
        }

        private void btncontinue_Enter(object sender, EventArgs e)
        {
            btncontinue.UseVisualStyleBackColor = false;
            btncontinue.BackColor = Color.FromArgb(20,209,82);
            btncontinue.ForeColor = Color.White;
        }

        private void btncontinue_Leave(object sender, EventArgs e)
        {
            btncontinue.UseVisualStyleBackColor = true;
            btncontinue.BackColor = Color.FromArgb(51, 153, 255);
            btncontinue.ForeColor = Color.White;
        }

        private void btncontinue_MouseEnter(object sender, EventArgs e)
        {
            btncontinue.UseVisualStyleBackColor = false;
            btncontinue.BackColor = Color.FromArgb(20, 209, 82);
            btncontinue.ForeColor = Color.White;
        }

        private void btncontinue_MouseLeave(object sender, EventArgs e)
        {
            btncontinue.UseVisualStyleBackColor = true;
            btncontinue.BackColor = Color.FromArgb(51, 153, 255);
            btncontinue.ForeColor = Color.White;
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

        private void btncancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
               // master.RemoveCurrentTab();
                this.Close();
            }
        }
    }
}
