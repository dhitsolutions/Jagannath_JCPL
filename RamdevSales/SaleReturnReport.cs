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
    public partial class SaleReturnReport : Form
    {
        SqlConnection constr = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());

        DataTable dt = new DataTable();
        public SaleReturnReport()
        {
            InitializeComponent();
        }

        private void SaleReturnReport_Load(object sender, EventArgs e)
        {
            try
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(0, 0);
                SaleReturnRPT crystal = new SaleReturnRPT();
                sale ds = GetData();
                crystal.SetDataSource(ds);
                this.crystalReportViewer1.ReportSource = crystal;
                this.crystalReportViewer1.RefreshReport();
            }
            catch
            {
            }
        }
        private sale GetData()
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
                        using (sale dspos = new sale())
                        {
                            sda.Fill(dspos, "Printing");
                            return dspos;

                        }

                    }
                }
            }
        }
    }
}
