using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Configuration;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace RamdevSales
{
    public partial class SaleReport : Form
    {
        
        OleDbConnection constr = new OleDbConnection(ConfigurationManager.ConnectionStrings["prnt"].ToString());
        private ReportDocument rpt;
        DataTable dt = new DataTable();
        private string str;
        public SaleReport()
        {
            InitializeComponent();
        }

        public SaleReport(string str)
        {
            try
            {
                // TODO: Complete member initialization
                InitializeComponent();
                this.str = str;
          //          sale ds = GetData();
                    ReportDocument reportDocument=new ReportDocument();
                    reportDocument.Load(str);
                    reportDocument.SetDatabaseLogon("Admin", "abc");
                    crystalReportViewer1.ReportSource = reportDocument;
                  //  ConfigureCrystalReports();
             //   crDbConnection.IntegratedSecurity = false;        
   
             //   crystalReportViewer1.ReportSource = str;
            }
            catch
            {
            }

        }
        private void ConfigureCrystalReports()
        {
            rpt = new ReportDocument();
            string reportPath = str;
            rpt.Load(reportPath);
            ConnectionInfo connectionInfo = new ConnectionInfo();
            connectionInfo.DatabaseName = "Companies12";
            connectionInfo.UserID = "Admin";
            connectionInfo.Password = "all";
            SetDBLogonForReport(connectionInfo, rpt);
            crystalReportViewer1.ReportSource = rpt;
        }

        private void SetDBLogonForReport(ConnectionInfo connectionInfo, ReportDocument reportDocument)
        {
            Tables tables = reportDocument.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
            {
                TableLogOnInfo tableLogonInfo = table.LogOnInfo;
                tableLogonInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(tableLogonInfo);
            }
        }
        private void SaleReport_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    this.StartPosition = FormStartPosition.Manual;
            //    this.Location = new Point(0, 0);
            //    Tally crystal = new Tally();
            //    
            //    crystal.SetDataSource(ds);
            //    this.crystalReportViewer1.ReportSource = crystal;
            //    this.crystalReportViewer1.RefreshReport();
            //}
            //catch
            //{
            //}
        }
        private sale GetData()
        {
            using ((constr))
            {
                //using (SqlCommand cmd = new SqlCommand("select billno,itemname,qty,unitprice,price,disamt,totalamt from itemdetails where billno='" + lblbillno.Text + "'"))
                using (OleDbCommand cmd = new OleDbCommand("select * from Printing"))
                {
                    using (OleDbDataAdapter sda = new OleDbDataAdapter())
                    {
                        constr = new OleDbConnection(ConfigurationManager.ConnectionStrings["prnt"].ToString());
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
