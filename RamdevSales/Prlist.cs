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
using System.Data.SqlClient;

namespace RamdevSales
{
    public partial class Prlist : Form
    {
        OleDbConnection constr = new OleDbConnection(ConfigurationManager.ConnectionStrings["dbComp"].ToString());
        private ReportDocument rpt;
        DataTable dt = new DataTable();
        private string str;
        CrystalReport1 report = new CrystalReport1();
        public Prlist()
        {
            InitializeComponent();
        }

        public Prlist(string str)
        {
            try
            {

                InitializeComponent();
                rpt = new ReportDocument();
                BillingPOSPrintDataSet ds = GetData();
                rpt.Load(str);
                rpt.SetDataSource(ds.Tables["Printing"]);
                SetDBLogonForReport(rpt, ds);
                crystalReportViewer1.ReportSource = rpt;
            }
            catch
            {
            }
        }
        private void SetDBLogonForReport(ReportDocument reportDocument, DataSet ds)
        {

            ConnectionInfo connectionInfo = new ConnectionInfo();
            //connectionInfo.ServerName = @"D:\\Development\\TabsFM Table Checker Code - 2017y01m05d\\TabsFM Table Checker\\TabsFM Reference DB Analyser\\ReferenceDBFile.accdb";
            connectionInfo.ServerName = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Companies12.mdb;Persist Security Info=False";
            connectionInfo.DatabaseName = "MS Access Database";
            connectionInfo.IntegratedSecurity = true;
            //connectionInfo.UserID = "Admin";
            //connectionInfo.Password = "";
            Tables tables = reportDocument.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
            {
                TableLogOnInfo tableLogonInfo = table.LogOnInfo;
                tableLogonInfo.ConnectionInfo = connectionInfo;
                // ds.Tables[0].TableName = table.Name;
                table.ApplyLogOnInfo(tableLogonInfo);
                //table.Location = table.Location;

                //if (table.Name != "command_1" && ds.Tables[0].TableName == "TB1")
                //    ds.Tables[0].TableName = table.Name;
                //else
                //{
                //    ds.Tables[1].TableName = table.LogOnInfo.TableName;
                //}
            }
        }
        

        private void Prlist_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    this.StartPosition = FormStartPosition.Manual;
            //    this.Location = new Point(0, 0);
            //    SrlistReport crystal = new SrlistReport();
            //    BillingPOSPrintDataSet ds = GetData();
            //    crystal.SetDataSource(ds);
            //    this.crystalReportViewer1.ReportSource = crystal;
            //    crystalReportViewer1.RefreshReport();
            //    return;
            //}
            //catch
            //{
            //}
        }
        private BillingPOSPrintDataSet GetData()
        {
            //getcon();
            using ((ServerConnection.con))
            {
                using (SqlCommand cmd = new SqlCommand("Select * from Printing"))
                {//SqlCommand cmd = new SqlCommand("select b.*,bp.* from billposmaster b inner join BillProductMaster bp on bp.BillId = b.BillId");

                    //SqlCommand cmd1 = new SqlCommand("Select * from BillMaster",con);

                    SqlDataAdapter sda1 = new SqlDataAdapter();
                    cmd.Connection = ServerConnection.con;
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
