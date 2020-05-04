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
    public partial class Print : Form
    {
        Printing prnt = new Printing();
          Connection conn = new Connection();
          public static string flagforprint;
        private string p;
        public Print()
        {
            InitializeComponent();
        }

        public Print(string p)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.p = p;
            binddrop();
            
        }
        private void Print_Load(object sender, EventArgs e)
        {
           this.ActiveControl=btnpreview;
           //set the interval  and start the timer
         //  timer1.Interval = 1000;
         //  timer1.Start();
        }

        private void binddrop()
        {
            try
            {

                DataSet ds = prnt.getdata("select * from design where TransactionType='" + p + "'");
                DataTable dt = ds.Tables[0];
                cmbformat.ValueMember = "DesignID";
                cmbformat.DisplayMember = "DesignName";
                cmbformat.DataSource = dt;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["SetDefault"].ToString() == "Y")
                    {
                        cmbformat.SelectedIndex = i;
                    }
                }
            }
            catch
            {
            }
        }
        public void btnpreview_Click(object sender, EventArgs e)
        {
            flagforprint = "0";
            if (p == "GSTR-3B")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "GSTR-3B");

                sql.Show();

                this.Close();

            }
            if (p == "Sale")
            {
                string str = Application.StartupPath + "\\"+lblrptname.Text;
                SQLReport sql = new SQLReport(str,"Sale");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                //        sql.Show();
                //        sql.Hide();
                //    }
                //}
                this.Close();
                //SaleReport sale = new SaleReport(str);
                //sale.Show();
            }
            if (p == "ItemWiseSaleOrder")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "ItemWiseSaleOrder");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                //        sql.Show();
                //        sql.Hide();
                //    }
                //}
                this.Close();
                //SaleReport sale = new SaleReport(str);
                //sale.Show();
            }
            if (p == "Pos")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str,"Pos");
                 //DataTable bill = conn.getdataset("select defaultbill from Options");
                 //if (bill.Rows.Count > 0)
                 //{
                 //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                 //    {
                        sql.Show();
                 //    }
                 //    else
                 //    {
                 //        //SaleReport sale = new SaleReport(str);
                 //        sql.Show();
                 //        sql.Hide();
                 //    }
                 //}
                 this.Close();
            }

            if (p == "Ledger")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str,"Ledger");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                //        sql.Show();
                //        sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "QuickPayment")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "QuickPayment");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                //        sql.Show();
                //        sql.Hide();
                //    }
                //}
                this.Close();

            }
            //QuickReceipt
            if (p == "QuickReceipt")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "QuickReceipt");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                //        sql.Show();
                //        sql.Hide();
                //    }
                //}
                this.Close();

            }

            if (p == "ItemWiseStock")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "ItemWiseStock");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                //        sql.Show();
                //        sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "CashBook")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "CashBook");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                //        sql.Show();
                //        sql.Hide();
                //    }
                //}
                this.Close();

            }
            //SaleBillsList
            if (p == "SaleBillsList")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "SaleBillsList");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                //        sql.Show();
                //        sql.Hide();
                //    }
                //}
                this.Close();

            }
            //OutstandingAnalysis
            if (p == "Outstanding")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Outstanding");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                       sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                //        sql.Show();
                //        sql.Hide();
                //    }
                //}
                this.Close();

            }
            //Print Barcode 1
            if (p == "Barcode1")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Barcode1");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                //        sql.Show();
                //        sql.Hide();
                //    }
                //}
                this.Close();

            }
            //Print Barcode 2
            if (p == "Barcode2")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Barcode2");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                //        sql.Show();
                //        sql.Hide();
                //    }
                //}
                this.Close();

            }
            //Print Barcode 3
            if (p == "Barcode3")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Barcode3");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                //        sql.Show();
                //        sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "Trialbalance")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Trialbalance");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                //        sql.Show();
                //        sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "SaleReturn")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "SaleReturn");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                //        sql.Show();
                //        sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "Sale Order")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Sale Order");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                 sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
               // sql.Show();
             //   sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "Sale Challan")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Sale Challan");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                // sql.Show();
                //   sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "Pos List")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Pos List");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                // sql.Show();
                //   sql.Hide();
                //    }
                //}
                this.Close();

            }
           // 
            if (p == "Pos Item List")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "PosItemList");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                // sql.Show();
                //   sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "BankVoucher")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "BankVoucher");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                // sql.Show();
                //   sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "BankEnteryReport")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "BankEnteryReport");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                // sql.Show();
                //   sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "DebitReport")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "DebitReport");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                // sql.Show();
                //   sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "CreditReport")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "CreditReport");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                // sql.Show();
                //   sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "StockEvaluation")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "StockEvaluation");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                // sql.Show();
                //   sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "ItemReport")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "ItemReport");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                // sql.Show();
                //   sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "AccountReport")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "AccountReport");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                // sql.Show();
                //   sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "AgentReport")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "AgentReport");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                // sql.Show();
                //   sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "Purchase Order")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Purchase Order");
                
                sql.Show();
                
                this.Close();
            }
            //ComplainReceive
            if (p == "ComplainReceive")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "ComplainReceive");

                sql.Show();

                this.Close();
            }
            //
            if (p == "SendToCompany")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "SendToCompany");

                sql.Show();

                this.Close();
            }
            //SendToCustomer
            if (p == "SendToCustomer")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "SendToCustomer");

                sql.Show();

                this.Close();
            }
            //
            if (p == "PosBillListThermalPrint")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "PosBillListThermalPrint");

                sql.Show();

                this.Close();
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    this.Close();
                }
                return true;
            }



            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void cmbformat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = prnt.getdata("select * from design where DesignName='" + cmbformat.Text + "'");
                DataTable dt = ds.Tables[0];
                lblrptname.Text = dt.Rows[0]["Path"].ToString();
                bool inList = false;
                for (int i = 0; i < cmbformat.Items.Count; i++)
                {
                    s = cmbformat.GetItemText(cmbformat.Items[i]);
                    if (s == cmbformat.Text)
                    {
                        inList = true;
                        cmbformat.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbformat.Text = "";
                }
            }
            catch
            {
            }
        }

        private void cmbformat_Enter(object sender, EventArgs e)
        {
            
        }

        private void cmbformat_Leave(object sender, EventArgs e)
        {
            cmbformat.Text = s;
        }
        string searchstr;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //empty the string for every 1 seconds
          //  searchstr = "";
        }

        private void cmbformat_KeyUp(object sender, KeyEventArgs e)
        {
            if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            {
                searchstr = searchstr + Convert.ToChar(e.KeyCode);
                // If the Search string is greater than 1 then use custom logic
                if (searchstr.Length > 1)
                {
                    int index;
                    // Search the Item that matches the string typed
                    index = cmbformat.FindString(searchstr);
                    // Select the Item in the Combo
                    if (index > 0)
                    {
                        cmbformat.SelectedIndex = index;
                    }
                }


            }
        }

        public void btnprint_Click(object sender, EventArgs e)
        {
            flagforprint = "1";
            if (p == "GSTR-3B")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "GSTR-3B");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();
                //SaleReport sale = new SaleReport(str);
                //sale.Show();
            }
            if (p == "Sale")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Sale");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                        sql.Show();
                        sql.Hide();
                //    }
                //}
                this.Close();
                //SaleReport sale = new SaleReport(str);
                //sale.Show();
            }
            if (p == "Pos")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Pos");
                DataTable bill = conn.getdataset("select defaultbill from Options");
                if (bill.Rows.Count > 0)
                {
                    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                    {
                        sql.Show();
                    }
                    else
                    {
                //        //SaleReport sale = new SaleReport(str);
                        sql.Show();
                        sql.Hide();
                    }
                }
                this.Close();
            }

            if (p == "Ledger")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Ledger");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                       sql.Show();
                       sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "QuickPayment")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "QuickPayment");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                       sql.Show();
                       sql.Hide();
                //    }
                //}
                this.Close();

            }
            //QuickReceipt
            if (p == "QuickReceipt")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "QuickReceipt");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                        sql.Show();
                        sql.Hide();
                //    }
                //}
                this.Close();

            }

            if (p == "ItemWiseStock")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "ItemWiseStock");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                        sql.Show();
                        sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "CashBook")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "CashBook");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                        sql.Show();
                        sql.Hide();
                //    }
                //}
                this.Close();

            }
            //SaleBillsList
            if (p == "SaleBillsList")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "SaleBillsList");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                        sql.Show();
                        sql.Hide();
                //    }
                //}
                this.Close();

            }
            //OutstandingAnalysis
            if (p == "Outstanding")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Outstanding");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                        sql.Show();
                        sql.Hide();
                //    }
                //}
                this.Close();

            }
            //Print Barcode 1
            if (p == "Barcode1")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Barcode1");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                        sql.Show();
                        sql.Hide();
                //    }
                //}
                this.Close();

            }
            //Print Barcode 2
            if (p == "Barcode2")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Barcode2");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                        sql.Show();
                        sql.Hide();
                //    }
                //}
                this.Close();

            }
            //Print Barcode 3
            if (p == "Barcode3")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Barcode3");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                       sql.Show();
                       sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "Trialbalance")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Trialbalance");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                        sql.Show();
                        sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "SaleReturn")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "SaleReturn");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
               // sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                        sql.Show();
                        sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "Sale Order")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Sale Order");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                // sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "Sale Challan")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Sale Challan");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
               // sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                 sql.Show();
                   sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "Pos List")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Pos List");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                // sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "Pos Item List")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "PosItemList");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                // sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "BankVoucher")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "BankVoucher");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                // sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "BankEnterReport")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "BankEnterReport");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                // sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "CreditReport")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "CreditReport");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                // sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "DebitReport")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "DebitReport");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                // sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "StockEvaluation")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "StockEvaluation");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                // sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "ItemReport")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "ItemReport");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                // sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "AccountReport")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "AccountReport");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                // sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "AgentReport")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "AgentReport");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                // sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "Purchase Order")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Purchase Order");
                sql.Show();
                sql.Hide();
                this.Close();
            }
            if (p == "ComplainReceive")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "ComplainReceive");
                sql.Show();
                sql.Hide();
                this.Close();
            }
            //SendToCompany
            if (p == "SendToCompany")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "SendToCompany");
                sql.Show();
                sql.Hide();
                this.Close();
            }
            //SendToCustomer
            if (p == "SendToCustomer")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "SendToCustomer");
                sql.Show();
                sql.Hide();
                this.Close();
            }
            //ListPOSThemalPrint
            if (p == "PosBillListThermalPrint")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "PosBillListThermalPrint");
                sql.Show();
                sql.Hide();
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Enter(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = false;
            btnClose.BackColor = Color.FromArgb(248, 152, 94);
            btnClose.ForeColor = Color.White;
        }

        private void btnClose_Leave(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = true;
            btnClose.BackColor = Color.FromArgb(51, 153, 255);
            btnClose.ForeColor = Color.White;
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = false;
            btnClose.BackColor = Color.FromArgb(248, 152, 94);
            btnClose.ForeColor = Color.White;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = true;
            btnClose.BackColor = Color.FromArgb(51, 153, 255);
            btnClose.ForeColor = Color.White;
        }
        public static string s;
        private void cmbformat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbformat.Items.Count; i++)
                {
                    s = cmbformat.GetItemText(cmbformat.Items[i]);
                    if (s == cmbformat.Text)
                    {
                        inList = true;
                        cmbformat.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbformat.Text = "";
                }

            }
        }

        private void btnpreview_Enter(object sender, EventArgs e)
        {
            btnpreview.UseVisualStyleBackColor = false;
            btnpreview.BackColor = Color.FromArgb(20, 209, 82);
            btnpreview.ForeColor = Color.White;
        }

        private void btnpreview_Leave(object sender, EventArgs e)
        {
            btnpreview.UseVisualStyleBackColor = false;
            btnpreview.BackColor = Color.FromArgb(51, 153, 255);
            btnpreview.ForeColor = Color.White;
        }

        private void btnpreview_MouseEnter(object sender, EventArgs e)
        {
            btnpreview.UseVisualStyleBackColor = false;
            btnpreview.BackColor = Color.FromArgb(20, 209, 82);
            btnpreview.ForeColor = Color.White;
        }

        private void btnpreview_MouseLeave(object sender, EventArgs e)
        {
            btnpreview.UseVisualStyleBackColor = false;
            btnpreview.BackColor = Color.FromArgb(51, 153, 255);
            btnpreview.ForeColor = Color.White;
        }

        private void btnprint_MouseEnter(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = false;
            btnprint.BackColor = Color.FromArgb(176, 111, 193);
            btnprint.ForeColor = Color.White;
        }

        private void btnprint_MouseLeave(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = true;
            btnprint.BackColor = Color.FromArgb(51, 153, 255);
            btnprint.ForeColor = Color.White;
        }

        private void btnprint_Enter(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = false;
            btnprint.BackColor = Color.FromArgb(176, 111, 193);
            btnprint.ForeColor = Color.White;
        }

        private void btnprint_Leave(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = true;
            btnprint.BackColor = Color.FromArgb(51, 153, 255);
            btnprint.ForeColor = Color.White;
        }

    }
}
