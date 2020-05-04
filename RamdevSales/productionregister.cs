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
    public partial class productionregister : Form
    {
        private Master master;
        private TabControl tabControl;
        Connection conn = new Connection();

        public productionregister()
        {
            InitializeComponent();
        }

        public productionregister(Master master, TabControl tabControl)
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

        private void productionregister_Load(object sender, EventArgs e)
        {
            try
            {
                lvproduction.Columns.Add("Date", 100, HorizontalAlignment.Left);
                lvproduction.Columns.Add("Vch.", 50, HorizontalAlignment.Left);
                lvproduction.Columns.Add("Type", 70, HorizontalAlignment.Left);
                lvproduction.Columns.Add("Item", 170, HorizontalAlignment.Left);
                lvproduction.Columns.Add("Main(Cons)", 100, HorizontalAlignment.Left);
                lvproduction.Columns.Add("Unit", 80, HorizontalAlignment.Left);
                lvproduction.Columns.Add("Alt(Cons)", 100, HorizontalAlignment.Left);
                lvproduction.Columns.Add("Unit", 80, HorizontalAlignment.Left);
                lvproduction.Columns.Add("Main Prod.", 100, HorizontalAlignment.Left);
                lvproduction.Columns.Add("Unit", 80, HorizontalAlignment.Left);
                lvproduction.Columns.Add("Alt Prod.", 100, HorizontalAlignment.Left);
                lvproduction.Columns.Add("Unit", 80, HorizontalAlignment.Left);
                lvproduction.Columns.Add("hideVch.",0, HorizontalAlignment.Left);
                this.ActiveControl = BtnViewReport;
                DTPFrom.CustomFormat = Master.dateformate;
                DTPTo.CustomFormat = Master.dateformate;
            }
            catch
            {
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
        ListViewItem li;
        DataTable dt, dt1, dt2 = new DataTable();
        decimal coqty = 0;
        decimal coaqty = 0;
        decimal proqty = 0;
        decimal proaqty = 0;
        public static string processdescription;
        public void binddata()
        {
            try
            {
                lvproduction.Items.Clear();
                dt = conn.getdataset("select * from tblproductionmaster where isactive=1 and date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by date asc");

                coqty = 0;
                coaqty = 0;
                proqty = 0;
                proaqty = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt2 = conn.getdataset("select * from tblproductionrawmaterialmaster where isactive=1 and productionid='" + dt.Rows[i]["id"].ToString() + "'");

                    for (int j = 0; j < dt2.Rows.Count; j++)
                    {
                        if (j == 0)
                        {
                            li = lvproduction.Items.Add(Convert.ToDateTime(dt.Rows[i]["date"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add(dt.Rows[i]["id"].ToString());
                            li.SubItems.Add("Cons");
                            li.SubItems.Add(dt2.Rows[j]["rawitemname"].ToString());
                            li.SubItems.Add(dt2.Rows[j]["rawqty"].ToString());
                            li.SubItems.Add(dt2.Rows[j]["rawunit"].ToString());
                            li.SubItems.Add(dt2.Rows[j]["rawaqty"].ToString());
                            li.SubItems.Add(dt2.Rows[j]["rawaunit"].ToString());
                            li.SubItems.Add("0");
                            li.SubItems.Add("");
                            li.SubItems.Add("0");
                            li.SubItems.Add("");
                            li.SubItems.Add(dt.Rows[i]["id"].ToString());
                        }
                        else
                        {
                            li = lvproduction.Items.Add("");
                            li.SubItems.Add("");
                            li.SubItems.Add("Cons");
                            li.SubItems.Add(dt2.Rows[j]["rawitemname"].ToString());
                            li.SubItems.Add(dt2.Rows[j]["rawqty"].ToString());
                            li.SubItems.Add(dt2.Rows[j]["rawunit"].ToString());
                            li.SubItems.Add(dt2.Rows[j]["rawaqty"].ToString());
                            li.SubItems.Add(dt2.Rows[j]["rawaunit"].ToString());
                            li.SubItems.Add("0");
                            li.SubItems.Add("");
                            li.SubItems.Add("0");
                            li.SubItems.Add("");
                            li.SubItems.Add(dt.Rows[i]["id"].ToString());
                        }
                    }
                    dt1 = conn.getdataset("select * from tblfinishedgoodsmaster where isactive=1 and productionid='" + dt.Rows[i]["id"].ToString() + "'");
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        if (lvproduction.Items.Count < 0)
                        {
                            li = lvproduction.Items.Add(Convert.ToDateTime(dt.Rows[i]["date"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add(dt.Rows[i]["id"].ToString());
                            li.SubItems.Add("Prod");
                            li.SubItems.Add(dt1.Rows[j]["itemname"].ToString());
                            li.SubItems.Add("0");
                            li.SubItems.Add("");
                            li.SubItems.Add("0");
                            li.SubItems.Add("");
                            li.SubItems.Add(dt1.Rows[j]["qty"].ToString());
                            li.SubItems.Add(dt1.Rows[j]["unit"].ToString());
                            li.SubItems.Add(dt1.Rows[j]["aqty"].ToString());
                            li.SubItems.Add(dt1.Rows[j]["aunit"].ToString());
                            li.SubItems.Add(dt.Rows[i]["id"].ToString());
                        }
                        else
                        {
                            li = lvproduction.Items.Add("");
                            li.SubItems.Add("");
                            li.SubItems.Add("Prod");
                            li.SubItems.Add(dt1.Rows[j]["itemname"].ToString());
                            li.SubItems.Add("0");
                            li.SubItems.Add("");
                            li.SubItems.Add("0");
                            li.SubItems.Add("");
                            li.SubItems.Add(dt1.Rows[j]["qty"].ToString());
                            li.SubItems.Add(dt1.Rows[j]["unit"].ToString());
                            li.SubItems.Add(dt1.Rows[j]["aqty"].ToString());
                            li.SubItems.Add(dt1.Rows[j]["aunit"].ToString());
                            li.SubItems.Add(dt.Rows[i]["id"].ToString());
                        }

                    }
                }

                foreach (ListViewItem lstItem in lvproduction.Items)
                {
                    coqty += decimal.Parse(lstItem.SubItems[4].Text);
                    coaqty += decimal.Parse(lstItem.SubItems[6].Text);
                    proqty += decimal.Parse(lstItem.SubItems[8].Text);
                    proaqty += decimal.Parse(lstItem.SubItems[10].Text);
                    //string bal1 = lstItem.SubItems[5].Text;
                    //String withoutLast1 = bal1.Substring(0, (bal1.Length - 3));
                    //decimal d = Convert.ToDecimal(withoutLast1);
                    //balancetotal += d;
                }
                txtconsmainqty.Text = Convert.ToString(coqty.ToString("N2"));
                txtconsaltqty.Text = Convert.ToString(coaqty.ToString("N2"));
                txtpromainqty.Text = Convert.ToString(proqty.ToString("N2"));
                txtproaltqty.Text = Convert.ToString(proaqty.ToString("N2"));

            }
            catch
            {
            }
        }
        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            binddata();
        }
        public static string iid = "";
        public void open()
        {
            iid = lvproduction.Items[lvproduction.FocusedItem.Index].SubItems[12].Text;
            Production p = new Production(this, master, tabControl);
            p.Updatedata(iid);
            master.AddNewTab(p);
        }
        private void lvproduction_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            open();
        }

        private void lvproduction_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                open();
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

        private void btnprint_Enter(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = false;
            btnprint.BackColor = System.Drawing.Color.FromArgb(176, 111, 193);
            btnprint.ForeColor = System.Drawing.Color.White;
        }

        private void btnprint_Leave(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = true;
            btnprint.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnprint.ForeColor = System.Drawing.Color.White;
        }

        private void btnprint_MouseEnter(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = false;
            btnprint.BackColor = System.Drawing.Color.FromArgb(176, 111, 193);
            btnprint.ForeColor = System.Drawing.Color.White;
        }

        private void btnprint_MouseLeave(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = true;
            btnprint.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnprint.ForeColor = System.Drawing.Color.White;
        }
        Printing prn = new Printing();
        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Print Production?", "Production", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    prn.execute("delete from printing");
                    string status;
                    status = "PRODUCTION REGISTER FROM " +DTPFrom.Text+" TO "+DTPTo.Text;
                    for (int i = 0; i < lvproduction.Items.Count; i++)
                    {
                        DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1 and CompanyID='" + Master.companyId + "' ");
                       // dt = conn.getdataset("select processdescription from tblproductionmaster where isactive=1 and processid='" + lvproduction.Items[i].SubItems[12].Text + "' order by date asc");
                       // processdescription = dt.Rows[0]["processdescription"].ToString();
                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26)VALUES";
                        qry += "('" + dt1.Rows[0]["CompanyName"].ToString() + "','" + dt1.Rows[0]["SubName"].ToString() + "','" + dt1.Rows[0]["Address"].ToString() + "','" + dt1.Rows[0]["Address2"].ToString() + "','" + dt1.Rows[0]["City"].ToString() + "','" + dt1.Rows[0]["State"].ToString() + "','" + dt1.Rows[0]["Country"].ToString() + "','" + dt1.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Mobile"].ToString() + "','" + dt1.Rows[0]["Email"].ToString() + "','" + dt1.Rows[0]["CSTNo"].ToString() + "','" + status + "','" + lvproduction.Items[i].SubItems[0].Text + "','" + lvproduction.Items[i].SubItems[1].Text + "','" + lvproduction.Items[i].SubItems[2].Text + "','" + lvproduction.Items[i].SubItems[3].Text + "','" + lvproduction.Items[i].SubItems[4].Text + "','" + lvproduction.Items[i].SubItems[5].Text + "','" + lvproduction.Items[i].SubItems[6].Text + "','" + lvproduction.Items[i].SubItems[7].Text + "','" + lvproduction.Items[i].SubItems[8].Text + "','" + lvproduction.Items[i].SubItems[9].Text + "','" + lvproduction.Items[i].SubItems[10].Text + "','" + lvproduction.Items[i].SubItems[11].Text + "','" + lvproduction.Items[i].SubItems[12].Text + "','" + dt1.Rows[0]["WebSite"].ToString() +"')";
                        prn.execute(qry);
                    }
                    string reportName = "ProductionRegister";
                    Print popup = new Print(reportName);
                    popup.ShowDialog();
                    popup.Dispose();
                }
            }
            catch
            {
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
    }
}
