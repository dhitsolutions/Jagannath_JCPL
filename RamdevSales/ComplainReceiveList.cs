using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;


namespace RamdevSales
{
    public partial class ComplainReceiveList : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        private TabControl tabControl;
        OleDbSettings ods = new OleDbSettings();
        private Master master;
        Connection conn = new Connection();
        DataTable dt = new DataTable();
        string complainID, id;
        string CustomerName;
        public ComplainReceiveList(Master master, TabControl tabcontrol)
        {
            this.tabControl = tabcontrol;
            this.master = master;
            InitializeComponent();
            //DTPFrom.CustomFormat = Master.dateformate;
            //DTPTo.CustomFormat = Master.dateformate;
            //DTPFrom.Value = Convert.ToDateTime(DateTime.Now.ToString(Master.dateformate));
            //DTPTo.Value = Convert.ToDateTime(DateTime.Now.ToString(Master.dateformate));

            //bindgrid();
            DTPFrom.Focus();
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
        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }



        private void ComplainReceiveList_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet dtrange = conn.getdata("SELECT * FROM Company where CompanyID='" + Master.companyId + "'");
                DTPFrom.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                DTPFrom.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                DTPTo.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                DTPTo.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                DTPFrom.Value = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                LV.Columns.Add("ComplainNo", 100, HorizontalAlignment.Center);
                LV.Columns.Add("ComplainDate", 200, HorizontalAlignment.Center);
                LV.Columns.Add("CustomerName", 350, HorizontalAlignment.Center);
                LV.Columns.Add("Total Count", 100, HorizontalAlignment.Center);
               // LV.Columns.Add("Item Name",300, HorizontalAlignment.Center);
               // LV.Columns.Add("Itemid",0, HorizontalAlignment.Center);
              //  AddColumn();
                binddata();
                btnAdd.Focus();
                this.ActiveControl = btnAdd;
                DTPFrom.CustomFormat = Master.dateformate;
                DTPTo.CustomFormat = Master.dateformate;
            }
            catch
            {
            }
        }
        ListViewItem li;
        Int32 rowid1 = -1;
        public void AddColumn()
        {
            try
            {
                //LV.Columns.Add("id", 50, HorizontalAlignment.Center);
                

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                LV.Items.Clear();
                dt = conn.getdataset("select complainid,date,customername from tblcomplainmaster where isactive=1 and Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'group by complainid,date,customername");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        li = LV.Items.Add(dt.Rows[i]["complainid"].ToString());
                        li.SubItems.Add(Convert.ToDateTime(dt.Rows[i]["date"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add(dt.Rows[i]["customername"].ToString());
                    }
                }
            }
            catch
            {
            }
            finally
            {
                con.Close();
            }



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

        private void BtnViewReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmComplainMasterData fcm = new frmComplainMasterData(master, tabControl);
            master.AddNewTab(fcm);
        }
        public void binddata()
        {
            try
            {
              //  if (rbunlock.Checked == true)
               // {
                    LV.Enabled = true;
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();

                    LV.Items.Clear();
                    dt = conn.getdataset("select c.id,c.date,c.customername from tblcomplainmaster c inner join tblitemcomplainmaster ci on c.id=ci.complainID where c.isactive=1 and ci.isactive=1 and c.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and c.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by c.id,c.date,c.customername");
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            li = LV.Items.Add(dt.Rows[i]["id"].ToString());
                            li.SubItems.Add(Convert.ToDateTime(dt.Rows[i]["date"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add(dt.Rows[i]["customername"].ToString());
                            string count = conn.ExecuteScalar("select Count(*) as TotalRecords from tblitemcomplainmaster where isactive=1 and complainID='" + dt.Rows[i]["id"].ToString() + "'");
                            li.SubItems.Add(Convert.ToString(count));
                       //     li.SubItems.Add(dt.Rows[i]["itemname"].ToString());
                         //   li.SubItems.Add(dt.Rows[i]["itemid"].ToString());
                        }
                    }
            //    }
                //else
                //{
                //    LV.Enabled = false;
                //    if (con.State == ConnectionState.Open)
                //    {
                //        con.Close();
                //    }
                //    con.Open();

                //    LV.Items.Clear();
                //    dt = conn.getdataset("select c.id,c.date,c.customername from tblcomplainmaster c inner join tblitemcomplainmaster ci on c.id=ci.complainID where ci.status !='Complain Received' and c.isactive=1 and ci.isactive=1 and c.Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and c.Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'");
                //   // dt = conn.getdataset("select complainid,date,customername,itemname from tblcomplainmaster where isactive=1 and status!='Complain Received' and Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'group by complainid,date,customername,itemname");
                //    if (dt.Rows.Count > 0)
                //    {
                //        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                //        {
                //            li = LV.Items.Add(dt.Rows[i]["id"].ToString());
                //            li.SubItems.Add(Convert.ToDateTime(dt.Rows[i]["date"].ToString()).ToString(Master.dateformate));
                //            li.SubItems.Add(dt.Rows[i]["customername"].ToString());
                //           // li.SubItems.Add(dt.Rows[i]["itemname"].ToString());
                //           // li.SubItems.Add(dt.Rows[i]["itemid"].ToString());
                //        }
                //    }
                //}
            }
            catch
            {
            }
            finally
            {
                con.Close();
            }
        }
        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            binddata();
        }
        public static string iid = "";
        public void open()
        {
            try
            {
                try
                {
                    this.Enabled = false;
                    iid = LV.Items[LV.FocusedItem.Index].SubItems[0].Text;

                    frmComplainMasterData dlg = new frmComplainMasterData(master, tabControl);


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
        private void LV_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            open();
        }

        private void LV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                open();
            }
        }

        private void txtcomplainname_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtcomplainname.Text))
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();

                    LV.Items.Clear();
                    dt = conn.getdataset("select c.id,c.date,c.customername from tblcomplainmaster c inner join tblitemcomplainmaster ci on c.id=ci.complainID where c.isactive=1 and ci.isactive=1 and c.id like'%" + txtcomplainname.Text + "%'group by c.id,c.date,c.customername");
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            li = LV.Items.Add(dt.Rows[i]["id"].ToString());
                            li.SubItems.Add(Convert.ToDateTime(dt.Rows[i]["date"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add(dt.Rows[i]["customername"].ToString());
                            string count = conn.ExecuteScalar("select Count(*) as TotalRecords from tblitemcomplainmaster where isactive=1 and complainID='" + dt.Rows[i]["id"].ToString() + "'");
                            li.SubItems.Add(Convert.ToString(count));
                        }
                    }
                }
                else
                {
                    binddata();
                }
            }
            catch
            {
            }
            finally
            {
                con.Close();
            }
        }

        private void txtdate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtdate.Text))
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();

                    LV.Items.Clear();
                    dt = conn.getdataset("select c.id,c.date,c.customername from tblcomplainmaster c inner join tblitemcomplainmaster ci on c.id=ci.complainID where c.isactive=1 and ci.isactive=1 and c.date like'%" + txtdate.Text + "%'group by c.id,c.date,c.customername");
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            li = LV.Items.Add(dt.Rows[i]["id"].ToString());
                            li.SubItems.Add(Convert.ToDateTime(dt.Rows[i]["date"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add(dt.Rows[i]["customername"].ToString());
                            string count = conn.ExecuteScalar("select Count(*) as TotalRecords from tblitemcomplainmaster where isactive=1 and complainID='" + dt.Rows[i]["id"].ToString() + "'");
                            li.SubItems.Add(Convert.ToString(count));
                        }
                    }
                }
                else
                {
                    binddata();
                }
            }
            catch
            {
            }
            finally
            {
                con.Close();
            }
        }

        private void txtcustomarname_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtcustomarname.Text))
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();

                    LV.Items.Clear();
                    dt = conn.getdataset("select c.id,c.date,c.customername from tblcomplainmaster c inner join tblitemcomplainmaster ci on c.id=ci.complainID where c.isactive=1 and ci.isactive=1 and c.customername like'%" + txtcustomarname.Text + "%'group by c.id,c.date,c.customername");
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            li = LV.Items.Add(dt.Rows[i]["id"].ToString());
                            li.SubItems.Add(Convert.ToDateTime(dt.Rows[i]["date"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add(dt.Rows[i]["customername"].ToString());
                            string count = conn.ExecuteScalar("select Count(*) as TotalRecords from tblitemcomplainmaster where isactive=1 and complainID='" + dt.Rows[i]["id"].ToString() + "'");
                            li.SubItems.Add(Convert.ToString(count));
                        }
                    }
                }
                else
                {
                    binddata();
                }
            }
            catch
            {
            }
            finally
            {
                con.Close();
            }
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

        private void btnAdd_Enter(object sender, EventArgs e)
        {
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.BackColor = System.Drawing.Color.FromArgb(9, 106, 3);
            btnAdd.ForeColor = System.Drawing.Color.White;
        }

        private void btnAdd_Leave(object sender, EventArgs e)
        {
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnAdd.ForeColor = System.Drawing.Color.White;
        }

        private void btnAdd_MouseEnter(object sender, EventArgs e)
        {
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.BackColor = System.Drawing.Color.FromArgb(9, 106, 3);
            btnAdd.ForeColor = System.Drawing.Color.White;
        }

        private void btnAdd_MouseLeave(object sender, EventArgs e)
        {
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnAdd.ForeColor = System.Drawing.Color.White;
        }







    }
}
