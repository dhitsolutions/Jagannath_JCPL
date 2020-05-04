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

namespace RamdevSales
{
    public partial class StockAdjustmentList : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        OleDbSettings ods = new OleDbSettings();
        Connection conn = new Connection();
        private Master master;
        private TabControl tabControl;

        public StockAdjustmentList()
        {
            InitializeComponent();
        }

        public StockAdjustmentList(Master master, TabControl tabControl)
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
        private void StockAdjustmentList_Load(object sender, EventArgs e)
        {
            DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
            DTPFrom.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            DTPFrom.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            DTPTo.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            DTPTo.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            // DTPFrom.Value = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            lvstock.Columns.Add("Stock ID", 100, HorizontalAlignment.Center);
            lvstock.Columns.Add("Stock Date", 120, HorizontalAlignment.Center);
            lvstock.Columns.Add("Remarks", 300, HorizontalAlignment.Left);
            DTPFrom.CustomFormat = Master.dateformate;
            DTPTo.CustomFormat = Master.dateformate;
            binddata();
        }
        public void binddata()
        {
            try
            {
                DataTable dt = conn.getdataset("select id,stockdate,mainremark from stockadujestmentmaster where isactive=1 and stockdate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and stockdate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by stockdate");
                lvstock.Items.Clear();
               for (int i = 0; i <= dt.Rows.Count - 1; i++)
               {
                   lvstock.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                   lvstock.Items[i].SubItems.Add(Convert.ToDateTime(dt.Rows[i].ItemArray[1].ToString()).ToString(Master.dateformate));
                   //lvstock.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                   lvstock.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
               }
            }
            catch
            {
            }
        }

        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            binddata();
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }
        public static string iid;
        public void setform()
        {
            try
            {
                this.Enabled = false;//optional, better target a panel or specific controls
                iid = lvstock.Items[lvstock.FocusedItem.Index].SubItems[0].Text;

                StockAdjustmentReport dlg = new StockAdjustmentReport(master, tabControl);

                dlg.Update(iid);
                master.AddNewTab(dlg);

            }
            finally
            {
                this.Enabled = true;
            }
        }

        private void lvstock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                setform();
            }
        }

        private void lvstock_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            setform();
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            StockAdjustmentReport frm = new StockAdjustmentReport(master, tabControl);
            master.AddNewTab(frm);
        }
    }
}
