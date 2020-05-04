using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Web;

namespace RamdevSales
{
    public partial class POSBillList : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        SqlCommand cmd,cmd1,cmd2,cmd3;
        SqlDataAdapter sda,sda2,sda3;
        static Int32 bill;
        Connection conn = new Connection();
        static double total, vat, net;
        private Master master;
        private TabControl tabControl;
        public POSBillList()
        {
            InitializeComponent();
        }

        public POSBillList(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }
        DataTable userrights = new DataTable();
        private void POSBillList_Load(object sender, EventArgs e)
        {

            userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[9]["a"].ToString() == "False")
                {
                    btnnew.Enabled = false;
                }
                if (userrights.Rows[9]["p"].ToString() == "False")
                {
                    btngenrpt.Enabled = false;
                }
            }
            LVDayBook.Columns.Add("Bill No",0, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("Bill NO", 70, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("Bill Date", 150, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("Terms", 100, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("Count", 100, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("Total Qty", 100, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("Total Basic", 150, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("Total Tax", 100, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("Total Net", 160, HorizontalAlignment.Center);
            DTPFrom.CustomFormat = Master.dateformate;
            DTPTo.CustomFormat = Master.dateformate;
            bindgrid();
            btnnew.Focus();
            
            this.ActiveControl = btnnew;
        }

        public void bindgrid()
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                LVDayBook.Items.Clear();
             
                //cmd = new SqlCommand("select b.BillId, b.BillDate, b.totalbasic, b.totaltax,b.totalnet from billposmaster b inner join BillProductMaster bp on bp.BillId = b.BillId where b.isactive=1 and b.BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString("MM-dd-yyyy") + "' and b.BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString("MM-dd-yyyy") + "' order by b.BillId", con);
                cmd = new SqlCommand("select BillId, BillDate,Terms,count, totalqty, totalbasic, totaltax, totalnet,billno from BillPOSMaster where isactive=1 and  BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by BillId", con);
                sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
               
                bill = 0;
                total = 0;
                vat = 0;
                
                net = 0;
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    LVDayBook.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[8].ToString());
                    LVDayBook.Items[i].SubItems.Add(Convert.ToDateTime(dt.Rows[i].ItemArray[1].ToString()).ToString(Master.dateformate));
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[7].ToString());
                    
                    bill++;
                    total = total + Convert.ToDouble(dt.Rows[i][5].ToString());
                    vat = vat + Convert.ToDouble(dt.Rows[i][6].ToString());
                   
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
                con.Close();
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
                    master.RemoveCurrentTab();
                }
                return true;
            }


            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            
            if (rball.Checked == true)
            {
                bindgrid();
            }
            else if (rbcash.Checked == true)
            {
                try
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();

                    LVDayBook.Items.Clear();

                    //cmd = new SqlCommand("select b.BillId, b.BillDate, b.totalbasic, b.totaltax,b.totalnet from billposmaster b inner join BillProductMaster bp on bp.BillId = b.BillId where b.isactive=1 and b.BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString("MM-dd-yyyy") + "' and b.BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString("MM-dd-yyyy") + "' order by b.BillId", con);
                    cmd = new SqlCommand("select BillId, BillDate,Terms,count, totalqty, totalbasic, totaltax, totalnet,billno from BillPOSMaster where isactive=1 and Terms='Cash' and  BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by BillId", con);
                    sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    bill = 0;
                    total = 0;
                    vat = 0;

                    net = 0;
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        LVDayBook.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[8].ToString());
                        LVDayBook.Items[i].SubItems.Add(Convert.ToDateTime(dt.Rows[i].ItemArray[1].ToString()).ToString(Master.dateformate));
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[7].ToString());

                        bill++;
                        total = total + Convert.ToDouble(dt.Rows[i][5].ToString());
                        vat = vat + Convert.ToDouble(dt.Rows[i][6].ToString());

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
                    con.Close();
                }
            }
            else if (rbcdcard.Checked == true)
            {
                try
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();

                    LVDayBook.Items.Clear();

                    //cmd = new SqlCommand("select b.BillId, b.BillDate, b.totalbasic, b.totaltax,b.totalnet from billposmaster b inner join BillProductMaster bp on bp.BillId = b.BillId where b.isactive=1 and b.BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString("MM-dd-yyyy") + "' and b.BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString("MM-dd-yyyy") + "' order by b.BillId", con);
                    cmd = new SqlCommand("select BillId, BillDate,Terms,count, totalqty, totalbasic, totaltax, totalnet,billno from BillPOSMaster where isactive=1 and Terms='Credit/Debit Card' and  BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by BillId", con);
                    sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    bill = 0;
                    total = 0;
                    vat = 0;

                    net = 0;
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        LVDayBook.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[8].ToString());
                        LVDayBook.Items[i].SubItems.Add(Convert.ToDateTime(dt.Rows[i].ItemArray[1].ToString()).ToString(Master.dateformate));
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[7].ToString());

                        bill++;
                        total = total + Convert.ToDouble(dt.Rows[i][5].ToString());
                        vat = vat + Convert.ToDouble(dt.Rows[i][6].ToString());

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
                    con.Close();
                }
            }
            else if (rbewallet.Checked == true)
            {
                try
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();

                    LVDayBook.Items.Clear();

                    //cmd = new SqlCommand("select b.BillId, b.BillDate, b.totalbasic, b.totaltax,b.totalnet from billposmaster b inner join BillProductMaster bp on bp.BillId = b.BillId where b.isactive=1 and b.BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString("MM-dd-yyyy") + "' and b.BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString("MM-dd-yyyy") + "' order by b.BillId", con);
                    cmd = new SqlCommand("select BillId, BillDate,Terms,count, totalqty, totalbasic, totaltax, totalnet,billno from BillPOSMaster where isactive=1 and Terms='E-Wallet' and  BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by BillId", con);
                    sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    bill = 0;
                    total = 0;
                    vat = 0;

                    net = 0;
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        LVDayBook.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[8].ToString());
                        LVDayBook.Items[i].SubItems.Add(Convert.ToDateTime(dt.Rows[i].ItemArray[1].ToString()).ToString(Master.dateformate));
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[7].ToString());

                        bill++;
                        total = total + Convert.ToDouble(dt.Rows[i][5].ToString());
                        vat = vat + Convert.ToDouble(dt.Rows[i][6].ToString());

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
                    con.Close();
                }
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
                master.RemoveCurrentTab();
            }
        }
        Printing prn = new Printing();
        private void btngenrpt_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    DialogResult dr = MessageBox.Show("Do you want to Generate Report?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        cmd3 = new SqlCommand("select * from company WHERE isactive=1", con);
                        sda3 = new SqlDataAdapter(cmd3);
                        DataTable dt1 = new DataTable();
                        sda3.Fill(dt1);

                        cmd = new SqlCommand("select BillId, BillDate,Terms,count, totalqty, totalbasic, totaltax, totalnet,billno from billposmaster where isactive=1 and BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString("MM-dd-yyyy") + "' and BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString("MM-dd-yyyy") + "' order by BillId", con);
                        sda = new SqlDataAdapter(cmd);
                        DataTable dt2 = new DataTable();
                        sda.Fill(dt2);
                        con.Open();
                        prn.execute("delete from printing");
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            string qry = "INSERT INTO Printing([T1],[T2],[T3],[T4],[T5],[T6],[T7],[T8],[T9],[T10],[T11],[T12],[T13],[T14],[T15],[T16],[T17],[T18],[T19],[T20],[T21],[T22],[T23],[T24],[T25],[T26],[T27],[T28])VALUES";
                            qry += "('" + dt2.Rows[i][0].ToString() + "','" + dt2.Rows[i][1].ToString() + "','" + dt2.Rows[i][2].ToString() + "','" + dt2.Rows[i][3].ToString() + "','" + dt2.Rows[i][4].ToString() + "','" + dt2.Rows[i][5].ToString() + "','" + dt2.Rows[i][6].ToString() + "','" + dt2.Rows[i][7].ToString() + "','" + dt1.Rows[0][0].ToString() + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][2].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + dt1.Rows[0][11].ToString() + "','" + dt1.Rows[0][12].ToString() + "','" + dt1.Rows[0][13].ToString() + "','" + txtbillamt.Text + "','" + txtvat.Text + "','" + txtnetamt.Text + "','" + DTPFrom.Text + "','" + DTPTo.Text + "','" + dt2.Rows[i][8].ToString() + "')";                         
                            prn.execute(qry);
                        }
                        string reportName = "Pos List";
                        Print popup = new Print(reportName);
                        popup.ShowDialog();
                        popup.Dispose();
                      
                    }
                    else
                    {
                        MessageBox.Show("Please select another date if you want to generate another Report.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public void open()
        {
            String str = LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[0].Text;

            DefaultPOS dlg = new DefaultPOS(master, tabControl);
            dlg.Update(1, str);
            //   bd.updatemode(str, LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[0].Text, 1);
            // bd.Show();
            master.AddNewTab(dlg);
        }
        private void LVDayBook_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[9]["v"].ToString() == "True" || userrights.Rows[9]["u"].ToString() == "True")
                {
                    open();
                }
                else
                {
                    MessageBox.Show("You don't have Permission To View");
                    return;
                }
            }
        }

        //private void LVDayBook_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        String str = "select p.Product_Name,bp.Product_Qty,bp.Free,p.Product_Price,bp.Product_Per_rate,bp.Product_total_Amt from BillProductMaster bp inner join ProductMaster p on p.ProductID=bp.ProductID where Bill_No='" + LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[0].Text + "' and isactive=1";

        //        BillDetails bd = new BillDetails(this);
        //        bd.Fromdatewise(str, LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[0].Text, 1);
        //        bd.StartPosition = FormStartPosition.CenterScreen;
        //        bd.Show();
        //    }
        //}

        private void btnnew_Click(object sender, EventArgs e)
        {
            DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='POS' and setdefault=1");
          //  POS bd = new POS(master, tabControl);
            DefaultPOS p = new DefaultPOS(master, tabControl);
            if (dt1.Rows[0]["formname"].ToString() == p.Text)
            {
               master.AddNewTab(p);
                //bd.MdiParent = this.MdiParent;
                //bd.StartPosition = FormStartPosition.CenterScreen;
                //bd.Show();
            }
            //else if (dt1.Rows[0]["formname"].ToString() == bd.Text)
            //{
            //   master.AddNewTab(bd);
            //    //p.MdiParent = this.MdiParent;
            //    //p.StartPosition = FormStartPosition.CenterScreen;
            //    //p.Show();
            //}
        }

       

        private void TxtInvoice_Enter(object sender, EventArgs e)
        {
            TxtInvoice.BackColor = Color.LightYellow;
        }

        private void TxtInvoice_Leave(object sender, EventArgs e)
        {
            TxtInvoice.BackColor = Color.White;
        }

        private void txtbillamt_Enter(object sender, EventArgs e)
        {
            txtbillamt.BackColor = Color.LightYellow;
        }

        private void txtbillamt_Leave(object sender, EventArgs e)
        {
            txtbillamt.BackColor = Color.White;
        }

        private void txtvat_Enter(object sender, EventArgs e)
        {
            txtvat.BackColor = Color.LightYellow;
        }

        private void txtnetamt_Enter(object sender, EventArgs e)
        {
            txtnetamt.BackColor = Color.LightYellow;
        }

        private void txtvat_Leave(object sender, EventArgs e)
        {
            txtvat.BackColor = Color.White;
        }

        private void txtnetamt_Leave(object sender, EventArgs e)
        {
            txtnetamt.BackColor = Color.White;
        }

        private void TxtInvoice_TextChanged(object sender, EventArgs e)
        {

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

        private void btngenrpt_MouseEnter(object sender, EventArgs e)
        {
            btngenrpt.UseVisualStyleBackColor = false;
            btngenrpt.BackColor = Color.FromArgb(176, 111, 193);
            btngenrpt.ForeColor = Color.White;
        }

        private void btngenrpt_MouseLeave(object sender, EventArgs e)
        {
            btngenrpt.UseVisualStyleBackColor = true;
            btngenrpt.BackColor = Color.FromArgb(51, 153, 255);
            btngenrpt.ForeColor = Color.White;
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

        private void btngenrpt_Enter(object sender, EventArgs e)
        {
            btngenrpt.UseVisualStyleBackColor = false;
            btngenrpt.BackColor = Color.FromArgb(176, 111, 193);
            btngenrpt.ForeColor = Color.White;
        }

        private void btngenrpt_Leave(object sender, EventArgs e)
        {
            btngenrpt.UseVisualStyleBackColor = true;
            btngenrpt.BackColor = Color.FromArgb(51, 153, 255);
            btngenrpt.ForeColor = Color.White;
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

        private void GroupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void DTPFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DTPTo.Focus();
            }
        }

        private void DTPTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnViewReport.Focus();
            }
        }

        private void LVDayBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[9]["v"].ToString() == "True" || userrights.Rows[9]["u"].ToString() == "True")
                    {
                        open();
                    }
                    else
                    {
                        MessageBox.Show("You don't have Permission To View");
                        return;
                    }
                }
            }
        }


    }

}
