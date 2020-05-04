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
    public partial class SalesReturnList : Form
    {
        ServerConnection con = new ServerConnection();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        static Int32 bill;
        static double total, vat, net;

        public SalesReturnList()
        {
            InitializeComponent();
        }

        private void SalesReturnList_Load(object sender, EventArgs e)
        {
            LVDayBook.Columns.Add("Bill No", 70, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("Bill Date", 100, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("Party Name", 300, HorizontalAlignment.Left);
            LVDayBook.Columns.Add("Address", 200, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("Total Amt", 100, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("TAX Amt", 100, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("Discount", 100, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("Net Amt", 120, HorizontalAlignment.Center);
            bindgrid();
            btnnew.Focus();
        }

        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            bindgrid();

        }

        public void bindgrid()
        {
            try
            {

                LVDayBook.Items.Clear();
                dt = con.getdataset("select b.billNo, b.billDate, c.accountname,c.address,b.totalbasic,b.totaltax,b.TotalDiscount,b.totalnet from billmaster b inner join clientmaster c on c.clientid=b.clientid  where b.isactive=1 and BillType='SR' and b.CompanyId=" + Master.companyId + " and b.BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString("MM-dd-yyyy") + "' and b.BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString("MM-dd-yyyy") + "' order by b.VchNo");

                bill = 0;
                total = 0;
                vat = 0;
                net = 0;
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    LVDayBook.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[7].ToString());

                    bill++;
                    total = total + Convert.ToDouble(dt.Rows[i][4].ToString());
                    vat = vat + Convert.ToDouble(dt.Rows[i][5].ToString());
                    net = net + Convert.ToDouble(dt.Rows[i][7].ToString());
                }

                TxtInvoice.Text = bill.ToString();
                txtbillamt.Text = total.ToString("N2");
                txtvat.Text = vat.ToString("N2");
                txtnetamt.Text = net.ToString("N2");

            }

            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally
            {

            }
        }

        private void DTPFrom_ValueChanged(object sender, EventArgs e)
        {
            DTPTo.MinDate = Convert.ToDateTime(DTPFrom.Text);
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void LVDayBook_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            String str = LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[0].Text;

            SalesReturn bd = new SalesReturn(this);
            bd.updatemode(str, LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[0].Text, 1);
            bd.Show();
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            SalesReturn frm = new SalesReturn();
            frm.MdiParent = this.MdiParent;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        
    }
}
