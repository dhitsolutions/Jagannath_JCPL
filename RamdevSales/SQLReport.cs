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
    public partial class SQLReport : Form
    {
        Connection conn = new Connection();
        OleDbConnection constr = new OleDbConnection(ConfigurationManager.ConnectionStrings["prnt"].ToString());
        private ReportDocument rpt;
        private string str;
        DataTable options, dt = new DataTable();
        CrystalReport1 report = new CrystalReport1();
        public SQLReport()
        {
            InitializeComponent();
        }
        public void pos()
        {
            DataTable bill = conn.getdataset("select defaultbill from Options");
            if (bill.Rows.Count > 0)
            {
                if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                {
                    crystalReportViewer1.ReportSource = rpt;
                    crystalReportViewer1.Visible = true;
                    //  rpt.PrintToPrinter(1, false, 0, 0);
                }
                else
                {
                    crystalReportViewer1.Visible = false;
                    rpt.PrintToPrinter(1, false, 0, 0);
                }
            }
        }
        public SQLReport(string str,string formname)
        {
            try
            {

                InitializeComponent();
                options = conn.getdataset("select * from options");
                rpt = new ReportDocument();
                sale ds = GetData();
                rpt.Load(str);
                rpt.SetDataSource(ds.Tables["Printing"]);
                SetDBLogonForReport(rpt, ds);
                if (formname == "Pos")
                {
                    pos();
                }
                else if (formname == "Sale")
                {
                    if (Print.flagforprint == "1")
                    {
                        DataTable bill = conn.getdataset("select noofcopyofsalebills from Options");
                        int billcopy = Convert.ToInt32(bill.Rows[0]["noofcopyofsalebills"].ToString());
                        crystalReportViewer1.Visible = false;
                        rpt.PrintToPrinter(billcopy, false, 0, 0);
                    }
                    else
                    {
                        crystalReportViewer1.ReportSource = rpt;
                        crystalReportViewer1.Visible = true;
                    }
                }
                else if (formname == "SaleReturn")
                {
                    if (Print.flagforprint == "1")
                    {
                        crystalReportViewer1.Visible = false;
                        rpt.PrintToPrinter(1, false, 0, 0);
                    }
                    else
                    {
                        crystalReportViewer1.ReportSource = rpt;
                        crystalReportViewer1.Visible = true;
                    }
                }
                else if (formname == "ItemWiseSaleOrder")
                {
                    if (Print.flagforprint == "1")
                    {
                        crystalReportViewer1.Visible = false;
                        rpt.PrintToPrinter(1, false, 0, 0);
                    }
                    else
                    {
                        crystalReportViewer1.ReportSource = rpt;
                        crystalReportViewer1.Visible = true;
                    }
                }
                else if (formname == "Sale Order")
                {
                    if (Print.flagforprint == "1")
                    {
                        crystalReportViewer1.Visible = false;
                        rpt.PrintToPrinter(1, false, 0, 0);
                    }
                    else
                    {
                        crystalReportViewer1.ReportSource = rpt;
                        crystalReportViewer1.Visible = true;
                    }
                }
                else if (formname == "Sale Challan")
                {
                    if (Print.flagforprint == "1")
                    {
                        crystalReportViewer1.Visible = false;
                        rpt.PrintToPrinter(1, false, 0, 0);
                    }
                    else
                    {
                        crystalReportViewer1.ReportSource = rpt;
                        crystalReportViewer1.Visible = true;
                    }
                }
                else if (formname == "QuickPayment")
                {
                    if (Print.flagforprint == "1")
                    {
                        DataTable bill = conn.getdataset("select noofcopyofquickpayment from Options");
                        int billcopy = Convert.ToInt32(bill.Rows[0]["noofcopyofquickpayment"].ToString());
                        crystalReportViewer1.Visible = false;
                        rpt.PrintToPrinter(billcopy, false, 0, 0);
                    }
                    else
                    {
                        crystalReportViewer1.ReportSource = rpt;
                        crystalReportViewer1.Visible = true;
                    }
                }
                else if (formname == "QuickReceipt")
                {
                    if (Print.flagforprint == "1")
                    {
                        DataTable bill = conn.getdataset("select noofcopyofquickreceipt from Options");
                        int billcopy = Convert.ToInt32(bill.Rows[0]["noofcopyofquickreceipt"].ToString());
                        crystalReportViewer1.Visible = false;
                        rpt.PrintToPrinter(billcopy, false, 0, 0);
                    }
                    else
                    {
                        crystalReportViewer1.ReportSource = rpt;
                        crystalReportViewer1.Visible = true;
                    }
                }
                else
                {
                    if (Print.flagforprint == "1")
                    {
                        crystalReportViewer1.Visible = false;
                        rpt.PrintToPrinter(1, false, 0, 0);
                    }
                    else
                    {
                        crystalReportViewer1.ReportSource = rpt;
                        crystalReportViewer1.Visible = true;
                    }
                }

            }
            catch
            {
            }
        }

        private void SetDBLogonForReport(ReportDocument reportDocument, DataSet ds)
        {

            ConnectionInfo connectionInfo = new ConnectionInfo();
            //connectionInfo.ServerName = @"D:\\Development\\TabsFM Table Checker Code - 2017y01m05d\\TabsFM Table Checker\\TabsFM Reference DB Analyser\\ReferenceDBFile.accdb";
            if (options.Rows[0]["PersistSecurityInfo"].ToString() == "True")
            {
                connectionInfo.ServerName = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Companies12.mdb;Persist Security Info=False";
            }
            else
            {
                connectionInfo.ServerName = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Companies12.mdb";
            }
            connectionInfo.DatabaseName = "MS Access Database";
            connectionInfo.IntegratedSecurity = true;
            //connectionInfo.UserID = "Admin";
            //connectionInfo.Password = "";
            Tables tables = reportDocument.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
            {
                TableLogOnInfo tableLogonInfo = table.LogOnInfo;
                tableLogonInfo.ConnectionInfo = connectionInfo;
                ds.Tables[0].TableName = table.Name;
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
                        constr = new OleDbConnection(ConfigurationManager.ConnectionStrings["prnt"].ToString());
                        cmd.Connection = constr;
                        sda.SelectCommand = cmd;
                        using (sale dspos = new sale())
                        {
                            dspos.EnforceConstraints = false;
                            sda.Fill(dspos, "Printing");
                            return dspos;

                        }

                    }
                }
            }
        }


    }


}
