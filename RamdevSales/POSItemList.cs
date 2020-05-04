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
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.rtf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Web;

namespace RamdevSales
{
    public partial class POSItemList : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        SqlCommand cmd,cmd1,cmd2,cmd3;
        SqlDataAdapter sda,sda1,sda2,sda3;
        private Master master;
        private TabControl tabControl;
        Printing prn = new Printing();
        Connection conn = new Connection();
        public POSItemList()
        {
            InitializeComponent();
        }

        public POSItemList(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }
        DataTable userrights = new DataTable();
        private void POSItemList_Load(object sender, EventArgs e)
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
            LVDayBook.Columns.Add("Billid",0, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("Item Name", 500, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("TotalQty", 70, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("Rate", 140, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("TotalSale", 160, HorizontalAlignment.Center);
            DTPFrom.CustomFormat = Master.dateformate;
            DTPTo.CustomFormat = Master.dateformate;           
            bindgrid();
            
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

                //cmd = new SqlCommand("select b.BillId, b.BillDate, b.totalbasic, b.totaltax,b.totalnet from BillMaster b inner join BillProductMaster bp on bp.BillId = b.BillId where b.isactive=1 and b.BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString("MM-dd-yyyy") + "' and b.BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString("MM-dd-yyyy") + "' order by b.BillId", con);
                //cmd = new SqlCommand("select  BillProdId, BillRunDate, ItemName, Qty, Rate from BillProductMaster where isactive=1 and BillRunDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString("MM-dd-yyyy") + "' and BillRunDate<='" + Convert.ToDateTime(DTPTo.Text).ToString("MM-dd-yyyy") + "' order by BillId", con);
                cmd = new SqlCommand("select  BillId,ItemName,sum(Qty) as totalqty, Rate, sum(qty)*rate as TotalSale from BillPOSProductMaster where isactive=1 and BillRunDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and BillRunDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by itemname, rate,BillId order by totalqty desc", con);
                sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    
                    LVDayBook.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                    
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

        private void btngenrpt_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Do you want to Generate Report?", "ItemList", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    //cmd2 = new SqlCommand("select  ItemName,sum(Qty) as totalqty, Rate, sum(qty)*rate as TotalSale from BillProductMaster group by itemname, rate order by totalqty desc", con);
                    //sda2 = new SqlDataAdapter(cmd2);
                    //DataTable dt2 = new DataTable();
                    //sda2.Fill(dt2);
                    cmd3 = new SqlCommand("select * from company WHERE isactive=1", con);
                    sda3 = new SqlDataAdapter(cmd3);
                    DataTable dt1 = new DataTable();
                    sda3.Fill(dt1);

                    cmd = new SqlCommand("select  BillId,ItemName,sum(Qty) as totalqty, Rate, sum(qty)*rate as TotalSale from BillPOSProductMaster where isactive=1 and BillRunDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString("MM-dd-yyyy") + "' and BillRunDate<='" + Convert.ToDateTime(DTPTo.Text).ToString("MM-dd-yyyy") + "' group by itemname, rate,BillId order by totalqty desc", con);
                    sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);


                    con.Open();
                   // cmd1 = new SqlCommand("delete from printing", con);
                   // cmd1.ExecuteNonQuery();
                    prn.execute("delete from printing");


                    //for (int i = 0; i < dt2.Rows.Count; i++)
                    //{
                    //    string qry = "INSERT INTO [dbo].[Printing]([T1],[T2],[T3],[T4],[T5])VALUES";
                    //    qry += "('" + DateTime.Now.ToShortDateString() + "','" + dt2.Rows[i][0].ToString() + "','" + dt2.Rows[i][1].ToString() + "','" + dt2.Rows[i][2].ToString() + "','" + dt2.Rows[i][3].ToString() + "')";
                    //    cmd = new SqlCommand(qry, con);
                    //    cmd.ExecuteNonQuery();
                    //}

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22)VALUES";
                        qry += "('" + dt.Rows[i][0].ToString() + "','" + DateTime.Now.ToShortDateString() + "','" + dt.Rows[i][1].ToString() + "','" + dt.Rows[i][2].ToString() + "','" + dt.Rows[i][3].ToString() + "','" + dt.Rows[i][4].ToString() + "','" + dt1.Rows[0][0].ToString() + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][2].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + dt1.Rows[0][11].ToString() + "','" + dt1.Rows[0][12].ToString() + "','" + dt1.Rows[0][13].ToString() + "','" + DTPFrom.Text + "','"+ DTPTo.Text +"')";
                        prn.execute(qry);
                       // cmd = new SqlCommand(qry, con);
                       // cmd.ExecuteNonQuery();
                    }
                    string reportName = "Pos Item List";
                    Print popup = new Print(reportName);
                    popup.ShowDialog();
                    popup.Dispose();
                    con.Close();
                  //  POSItemListPrintReport frm = new POSItemListPrintReport();

                  //  frm.StartPosition = FormStartPosition.CenterScreen;

                  //  frm.Show();
                }
                else
                {
                    MessageBox.Show("Please select another date if you want to generate another Report.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:", ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }

        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            bindgrid();
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

       

        private void btnnew_Click(object sender, EventArgs e)
        {

        }
        public void open()
        {
            try
            {
                String str = LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[0].Text;

                DefaultPOS dlg = new DefaultPOS(master, tabControl);
                dlg.Update(1, str);
                //   bd.updatemode(str, LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[0].Text, 1);
                // bd.Show();
                master.AddNewTab(dlg);
            }
            catch
            {
            }
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

        private void BtnViewReport_MouseEnter(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = false;
            BtnViewReport.BackColor = System.Drawing.Color.FromArgb(94, 191, 174);
            BtnViewReport.ForeColor = System.Drawing.Color.White;
        }

        private void BtnViewReport_MouseLeave(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = true;
            BtnViewReport.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            BtnViewReport.ForeColor = System.Drawing.Color.White;
        }

        private void btnnew_MouseEnter(object sender, EventArgs e)
        {
            btnnew.UseVisualStyleBackColor = false;
            btnnew.BackColor = System.Drawing.Color.FromArgb(9, 106, 3);
            btnnew.ForeColor = System.Drawing.Color.White;
        }

        private void btnnew_MouseLeave(object sender, EventArgs e)
        {
            btnnew.UseVisualStyleBackColor = true;
            btnnew.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnnew.ForeColor = System.Drawing.Color.White;
        }

        private void btngenrpt_MouseEnter(object sender, EventArgs e)
        {
            btngenrpt.UseVisualStyleBackColor = false;
            btngenrpt.BackColor = System.Drawing.Color.FromArgb(176, 111, 193);
            btngenrpt.ForeColor = System.Drawing.Color.White;
        }

        private void btngenrpt_MouseLeave(object sender, EventArgs e)
        {
            btngenrpt.UseVisualStyleBackColor = true;
            btngenrpt.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btngenrpt.ForeColor = System.Drawing.Color.White;
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

        private void BtnViewReport_Enter(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = false;
            BtnViewReport.BackColor = System.Drawing.Color.FromArgb(94, 191, 174);
            BtnViewReport.ForeColor = System.Drawing.Color.White;
        }

        private void BtnViewReport_Leave(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = true;
            BtnViewReport.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            BtnViewReport.ForeColor = System.Drawing.Color.White;
        }

        private void btnnew_Enter(object sender, EventArgs e)
        {
            btnnew.UseVisualStyleBackColor = false;
            btnnew.BackColor = System.Drawing.Color.FromArgb(9, 106, 3);
            btnnew.ForeColor = System.Drawing.Color.White;
        }

        private void btnnew_Leave(object sender, EventArgs e)
        {
            btnnew.UseVisualStyleBackColor = true;
            btnnew.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnnew.ForeColor = System.Drawing.Color.White;
        }

        private void btngenrpt_Enter(object sender, EventArgs e)
        {
            btngenrpt.UseVisualStyleBackColor = false;
            btngenrpt.BackColor = System.Drawing.Color.FromArgb(176, 111, 193);
            btngenrpt.ForeColor = System.Drawing.Color.White;
        }

        private void btngenrpt_Leave(object sender, EventArgs e)
        {
            btngenrpt.UseVisualStyleBackColor = true;
            btngenrpt.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btngenrpt.ForeColor = System.Drawing.Color.White;
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
