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
    public partial class PurchaseOrderReport : Form
    {
        SqlConnection constr = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());

        DataTable dt = new DataTable();
        public PurchaseOrderReport()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            try
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(0, 0);
              //  PurchaseOrderCrystalReport crystal = new PurchaseOrderCrystalReport();
                PurchaseOrder ds = GetData();
              //  crystal.SetDataSource(ds);
//this.crystalReportViewer1.ReportSource = crystal;
                this.crystalReportViewer1.RefreshReport();
            }
            catch
            {
            }
        }

        private PurchaseOrder GetData()
        {
            using ((constr))
            {
                //using (SqlCommand cmd = new SqlCommand("select billno,itemname,qty,unitprice,price,disamt,totalamt from itemdetails where billno='" + lblbillno.Text + "'"))
                using (SqlCommand cmd = new SqlCommand("select * from Printing"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = constr;
                        sda.SelectCommand = cmd;
                        using (PurchaseOrder dspos = new PurchaseOrder())
                        {
                          //  sda.Fill(dspos, "Printing");
                            return dspos;

                        }

                    }
                }
            }
        }
    }
}
