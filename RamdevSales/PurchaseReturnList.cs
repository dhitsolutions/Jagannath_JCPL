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
    public partial class PurchaseReturnList : Form
    {
        Connection con = new Connection();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        OleDbSettings prn = new OleDbSettings();
        static Int32 bill;
        static double total, vat, net;

        public PurchaseReturnList()
        {
            InitializeComponent();
        }

        private void PurchaseReturnList_Load(object sender, EventArgs e)
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
                dt = con.getdataset("select b.bill_No,convert(varchar(11), b.bill_Date, 113)as bill_date, c.subname,c.address,b.totalbasic,b.totaltax,b.TotalDiscount,b.totalnet from billmaster b inner join Company c on c.CompanyID=b.CompanyId  where b.isactive=1 and b.BillType='PR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString("MM-dd-yyyy") + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString("MM-dd-yyyy") + "' and b.CompanyId=" + Master.companyId + "");
               
                bill = 0;
                total = 0;
                vat = 0;
                net = 0;
                if (dt.Rows.Count > 0)
                {
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
                }
                TxtInvoice.Text = bill.ToString();
                txtbillamt.Text = total.ToString("N2");
                txtvat.Text = vat.ToString("N2");
                txtnetamt.Text = net.ToString("N2");

                DataTable dt4 = new DataTable();
                dt4 = con.getdataset("select CompanyName,Address,Phone,VATNo from Company where CompanyID='" + Master.companyId + "' and isActive=1");

                prn.execute("delete from printing");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string qry = "INSERT INTO [Printing]([T1],[T2],[T3],[T4],[T5],[T6],[T7],[T8],[T9],[T10],[T11],[T12])VALUES";
                    qry += "('" + dt.Rows[i][0].ToString() + "','" + dt.Rows[i][1].ToString() + "','" + dt.Rows[i][2].ToString() + "','" + dt.Rows[i][3].ToString() + "','" + dt.Rows[i][4].ToString() + "','" + dt.Rows[i][5].ToString() + "','" + dt.Rows[i][6].ToString() + "','" + dt.Rows[i][7].ToString() + "','" + dt4.Rows[0][0].ToString() + "','" + dt4.Rows[0][1].ToString() + "'," + dt4.Rows[0][2].ToString() + ",'" + dt4.Rows[0][3].ToString() + "')";
                    //cmd3 = new SqlCommand(qry, con);
                    //cmd3.ExecuteNonQuery();
                    prn.execute(qry);


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

            PurchaseReturn bd = new PurchaseReturn(this);
            bd.updatemode(str, LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[0].Text, 1);
            bd.MdiParent = this.MdiParent;
            bd.Show();
        }
        
        private void btnnew_Click(object sender, EventArgs e)
        {
            PurchaseReturn frm = new PurchaseReturn();
            frm.MdiParent = this.MdiParent;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
            //this.Close();
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

        private void btngenrpt_Click(object sender, EventArgs e)
        {
            //Prlist frm = new Prlist();

            //frm.MdiParent = this.MdiParent;
            //frm.StartPosition = FormStartPosition.CenterScreen;

            //frm.Show();
            Print popup = new Print("Purchase Return");
            popup.ShowDialog();
            popup.Dispose();
        }

        private void LVDayBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                String str = LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[0].Text;

                PurchaseReturn bd = new PurchaseReturn(this);
                bd.updatemode(str, LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[0].Text, 1);
                bd.MdiParent = this.MdiParent;
                bd.Show();
            }
        }

    }
}
