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

namespace RamdevSales
{
    public partial class POSBillListReport : Form
    {
        SqlConnection constr = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());

        DataTable dt = new DataTable();
        public POSBillListReport()
        {
            InitializeComponent();
        }

        private void POSBillListReport_Load(object sender, EventArgs e)
        {
            try
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(0, 0);
                POSBillListCrystalReport crystal = new POSBillListCrystalReport();
                BillingPOSPrintDataSet ds = GetData();
                crystal.SetDataSource(ds);
                this.crystalReportViewer1.ReportSource = crystal;
                crystalReportViewer1.RefreshReport();
                return;
            }
            catch
            {
            }
        }
        private BillingPOSPrintDataSet GetData()
        {
            using ((constr))
            {
                using (SqlCommand cmd = new SqlCommand("Select * from Printing"))
                {//SqlCommand cmd = new SqlCommand("select b.*,bp.* from billposmaster b inner join BillProductMaster bp on bp.BillId = b.BillId");

                    //SqlCommand cmd1 = new SqlCommand("Select * from BillMaster",con);

                    SqlDataAdapter sda1 = new SqlDataAdapter();
                    cmd.Connection = constr;
                    sda1.SelectCommand = cmd;
                    using (BillingPOSPrintDataSet dspos = new BillingPOSPrintDataSet())
                    {
                        sda1.Fill(dspos, "Printing");
                        return dspos;

                    }
                }
            }
        }

    }
}
