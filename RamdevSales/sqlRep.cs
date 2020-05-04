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
    public partial class sqlRep : Form
    {
        OleDbConnection constr = new OleDbConnection(ConfigurationManager.ConnectionStrings["dbComp"].ToString());
        private ReportDocument rpt;
        DataTable dt = new DataTable();
        private string str;
        CrystalReport1 report = new CrystalReport1();
        public sqlRep()
        {
            InitializeComponent();
        }

        public sqlRep(string str)
        {
            try
            {

                InitializeComponent();
                rpt = new ReportDocument();
                sale ds = GetData();
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
        private sale GetData()
        {
            using ((constr))
            {
                //using (SqlCommand cmd = new SqlCommand("select billno,itemname,qty,unitprice,price,disamt,totalamt from itemdetails where billno='" + lblbillno.Text + "'"))
                using (OleDbCommand cmd = new OleDbCommand("select * from Printing"))
                {
                    using (OleDbDataAdapter sda = new OleDbDataAdapter())
                    {
                        constr = new OleDbConnection(ConfigurationManager.ConnectionStrings["db"].ToString());
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
        private void sqlRep_Load(object sender, EventArgs e)
        {

        }
    }
}
