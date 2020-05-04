using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Synchronization;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;
using System.Configuration;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;

namespace RamdevSales
{
    class Syncronization
    {
        Connection conn = new Connection();

        OleDbSettings ods = new OleDbSettings();
        DataSet ds, ds1, ds2 = new DataSet();
        public static DateTime lastsyncdatetime;
        static string sServerConnection;
        static string sClientConnection = ConfigurationManager.ConnectionStrings["qry"].ToString();
        public void bindfactory()
        {
            try
            {
                ds = ods.getdata("select * from SQLSetting");
                DataTable Sconnnection = new DataTable();
                Sconnnection = ds.Tables[0];
                for (int j = 0; j < Sconnnection.Rows.Count; j++)
                {
                    sServerConnection = Sconnnection.Rows[j]["Authentication"].ToString();
                    SqlConnection con = new SqlConnection(sServerConnection);
                    string ClientID = Sconnnection.Rows[j]["OT3"].ToString();
                    //Bill Master
                    #region
                    //  DataTable clientbillmaster = conn.getdataset("Select * from BillMaster");
                    DataTable serverbillmaster = conn.getdataset("Select * from BillMaster where SyncDatetime >'" + lastsyncdatetime.ToString("MM/dd/yyyy h:mm:ss") + "'", con);
                    if (serverbillmaster.Rows.Count > 0)
                    {
                        for (int i = 0; i < serverbillmaster.Rows.Count; i++)
                        {
                            if (serverbillmaster.Rows[i]["Billtype"].ToString() == "P")
                            {
                                serverbillmaster.Rows[i]["Billtype"] = "S";
                                DataTable saletypeID = conn.getdataset("select purchasetypeid from Purchasetypemaster where isactive=1 and type='S'");
                                serverbillmaster.Rows[i]["SaleType"] = saletypeID.Rows[0]["purchasetypeid"].ToString();
                            }
                            serverbillmaster.AcceptChanges();
                            DataTable isexist = conn.getdataset("Select * from BillMaster where SyncID='" + serverbillmaster.Rows[i]["SyncID"].ToString() + "'");
                            if (isexist.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(isexist.Rows[0]["SyncID"].ToString()))
                                {
                                    string Server = serverbillmaster.Rows[i]["SyncDatetime"].ToString();
                                    string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                    DateTime localsyncdatetime = Convert.ToDateTime(local);
                                    DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                    if (localsyncdatetime < serversyncdatetime)
                                    {
                                        //Update in local
                                        conn.execute("UPDATE [dbo].[BillMaster] SET [Bill_No] ='" + serverbillmaster.Rows[i]["Bill_No"].ToString() + "',[Bill_Date] = '" + serverbillmaster.Rows[i]["Bill_Date"].ToString() + "',[Terms] = '" + serverbillmaster.Rows[i]["Terms"].ToString() + "',[ClientID] = '" + ClientID + "',[PO_No] = '" + serverbillmaster.Rows[i]["PO_No"].ToString() + "',[SaleType] = '" + serverbillmaster.Rows[i]["SaleType"].ToString() + "',[count] = '" + serverbillmaster.Rows[i]["count"].ToString() + "',[totalqty] = '" + serverbillmaster.Rows[i]["totalqty"].ToString() + "',[totalbasic] = '" + serverbillmaster.Rows[i]["totalbasic"].ToString() + "',[totaltax] = '" + serverbillmaster.Rows[i]["totaltax"].ToString() + "',[totalnet] = '" + serverbillmaster.Rows[i]["totalnet"].ToString() + "',[isactive] = '" + serverbillmaster.Rows[i]["isactive"].ToString() + "',[apprweight] = '" + serverbillmaster.Rows[i]["apprweight"].ToString() + "',[dispatchdetails] = '" + serverbillmaster.Rows[i]["dispatchdetails"].ToString() + "',[remarks] = '" + serverbillmaster.Rows[i]["remarks"].ToString() + "',[BillType] = '" + serverbillmaster.Rows[i]["BillType"].ToString() + "',[billno] = '" + serverbillmaster.Rows[i]["billno"].ToString() + "',[totaladdtax] = '" + serverbillmaster.Rows[i]["totaladdtax"].ToString() + "',[roudoff] = '" + serverbillmaster.Rows[i]["roudoff"].ToString() + "',[Duedate] = '" + serverbillmaster.Rows[i]["Duedate"].ToString() + "',[totalaqty] = '" + serverbillmaster.Rows[i]["totalaqty"].ToString() + "',[totalfree] = '" + serverbillmaster.Rows[i]["totalfree"].ToString() + "',[totaldiscount] = '" + serverbillmaster.Rows[i]["totaldiscount"].ToString() + "',[totaladddiscount] = '" + serverbillmaster.Rows[i]["totaladddiscount"].ToString() + "',[totalamount] = '" + serverbillmaster.Rows[i]["totalamount"].ToString() + "',[totalservicejob] = '" + serverbillmaster.Rows[i]["totalservicejob"].ToString() + "',[totalcharges] = '" + serverbillmaster.Rows[i]["totalcharges"].ToString() + "',[Delieveryat] = '" + serverbillmaster.Rows[i]["Delieveryat"].ToString() + "',[fraight] = '" + serverbillmaster.Rows[i]["fraight"].ToString() + "',[vehicleno] = '" + serverbillmaster.Rows[i]["vehicleno"].ToString() + "',[grrrno] = '" + serverbillmaster.Rows[i]["grrrno"].ToString() + "',[noofskids] = '" + serverbillmaster.Rows[i]["noofskids"].ToString() + "',[sgstamt] = '" + serverbillmaster.Rows[i]["sgstamt"].ToString() + "',[cgatamt] = '" + serverbillmaster.Rows[i]["cgatamt"].ToString() + "',[igstamt] = '" + serverbillmaster.Rows[i]["igstamt"].ToString() + "',[OrderStatus] = '" + serverbillmaster.Rows[i]["OrderStatus"].ToString() + "',[refno] = '" + serverbillmaster.Rows[i]["refno"].ToString() + "',[totalcess] = '" + serverbillmaster.Rows[i]["totalcess"].ToString() + "',[agentID] = '" + serverbillmaster.Rows[i]["agentID"].ToString() + "',[originalbillno] = '" + serverbillmaster.Rows[i]["originalbillno"].ToString() + "',[originalbilldate] = '" + serverbillmaster.Rows[i]["originalbilldate"].ToString() + "',[SyncID] = '" + serverbillmaster.Rows[i]["SyncID"].ToString() + "',[SyncDatetime] = '" + serverbillmaster.Rows[i]["SyncDatetime"].ToString() + "' WHERE SyncID='" + serverbillmaster.Rows[i]["SyncID"].ToString() + "'");
                                    }
                                    else
                                    {
                                        //Update in Server
                                    }
                                }
                                else
                                {
                                    string Server = serverbillmaster.Rows[i]["SyncDatetime"].ToString();
                                    string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                    DateTime localsyncdatetime = Convert.ToDateTime(local);
                                    DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                    if (localsyncdatetime < serversyncdatetime)
                                    {
                                        //Insert in local
                                        conn.execute("INSERT INTO [dbo].[BillMaster]([Bill_No],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[BillType],[billno],[totaladdtax],[roudoff],[Duedate],[totalaqty],[totalfree],[totaldiscount],[totaladddiscount],[totalamount],[totalservicejob],[totalcharges],[Delieveryat],[fraight],[vehicleno],[grrrno],[noofskids],[sgstamt],[cgatamt],[igstamt],[OrderStatus],[refno],[totalcess],[agentID],[originalbillno],[originalbilldate],[SyncID],[SyncDatetime])VALUES('" + serverbillmaster.Rows[i]["Bill_No"].ToString() + "','" + serverbillmaster.Rows[i]["Bill_Date"].ToString() + "','" + serverbillmaster.Rows[i]["Terms"].ToString() + "','" + ClientID + "','" + serverbillmaster.Rows[i]["PO_No"].ToString() + "','" + serverbillmaster.Rows[i]["SaleType"].ToString() + "','" + serverbillmaster.Rows[i]["count"].ToString() + "','" + serverbillmaster.Rows[i]["totalqty"].ToString() + "','" + serverbillmaster.Rows[i]["totalbasic"].ToString() + "','" + serverbillmaster.Rows[i]["totaltax"].ToString() + "','" + serverbillmaster.Rows[i]["totalnet"].ToString() + "','" + serverbillmaster.Rows[i]["isactive"].ToString() + "','" + serverbillmaster.Rows[i]["apprweight"].ToString() + "','" + serverbillmaster.Rows[i]["dispatchdetails"].ToString() + "','" + serverbillmaster.Rows[i]["remarks"].ToString() + "','" + serverbillmaster.Rows[i]["BillType"].ToString() + "','" + serverbillmaster.Rows[i]["billno"].ToString() + "','" + serverbillmaster.Rows[i]["totaladdtax"].ToString() + "','" + serverbillmaster.Rows[i]["roudoff"].ToString() + "','" + serverbillmaster.Rows[i]["Duedate"].ToString() + "','" + serverbillmaster.Rows[i]["totalaqty"].ToString() + "','" + serverbillmaster.Rows[i]["totalfree"].ToString() + "','" + serverbillmaster.Rows[i]["totaldiscount"].ToString() + "','" + serverbillmaster.Rows[i]["totaladddiscount"].ToString() + "','" + serverbillmaster.Rows[i]["totalamount"].ToString() + "','" + serverbillmaster.Rows[i]["totalservicejob"].ToString() + "','" + serverbillmaster.Rows[i]["totalcharges"].ToString() + "','" + serverbillmaster.Rows[i]["Delieveryat"].ToString() + "','" + serverbillmaster.Rows[i]["fraight"].ToString() + "','" + serverbillmaster.Rows[i]["vehicleno"].ToString() + "','" + serverbillmaster.Rows[i]["grrrno"].ToString() + "','" + serverbillmaster.Rows[i]["noofskids"].ToString() + "','" + serverbillmaster.Rows[i]["sgstamt"].ToString() + "','" + serverbillmaster.Rows[i]["cgatamt"].ToString() + "','" + serverbillmaster.Rows[i]["igstamt"].ToString() + "','" + serverbillmaster.Rows[i]["OrderStatus"].ToString() + "','" + serverbillmaster.Rows[i]["refno"].ToString() + "','" + serverbillmaster.Rows[i]["totalcess"].ToString() + "','" + serverbillmaster.Rows[i]["agentID"].ToString() + "','" + serverbillmaster.Rows[i]["originalbillno"].ToString() + "','" + serverbillmaster.Rows[i]["originalbilldate"].ToString() + "','" + serverbillmaster.Rows[i]["SyncID"].ToString() + "','" + serverbillmaster.Rows[i]["SyncDatetime"].ToString() + "')");
                                    }
                                    else
                                    {
                                        //Insert in Server

                                    }
                                }
                            }
                            else
                            {
                                conn.execute("INSERT INTO [dbo].[BillMaster]([Bill_No],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[BillType],[billno],[totaladdtax],[roudoff],[Duedate],[totalaqty],[totalfree],[totaldiscount],[totaladddiscount],[totalamount],[totalservicejob],[totalcharges],[Delieveryat],[fraight],[vehicleno],[grrrno],[noofskids],[sgstamt],[cgatamt],[igstamt],[OrderStatus],[refno],[totalcess],[agentID],[originalbillno],[originalbilldate],[SyncID],[SyncDatetime])VALUES('" + serverbillmaster.Rows[i]["Bill_No"].ToString() + "','" + serverbillmaster.Rows[i]["Bill_Date"].ToString() + "','" + serverbillmaster.Rows[i]["Terms"].ToString() + "','" + ClientID + "','" + serverbillmaster.Rows[i]["PO_No"].ToString() + "','" + serverbillmaster.Rows[i]["SaleType"].ToString() + "','" + serverbillmaster.Rows[i]["count"].ToString() + "','" + serverbillmaster.Rows[i]["totalqty"].ToString() + "','" + serverbillmaster.Rows[i]["totalbasic"].ToString() + "','" + serverbillmaster.Rows[i]["totaltax"].ToString() + "','" + serverbillmaster.Rows[i]["totalnet"].ToString() + "','" + serverbillmaster.Rows[i]["isactive"].ToString() + "','" + serverbillmaster.Rows[i]["apprweight"].ToString() + "','" + serverbillmaster.Rows[i]["dispatchdetails"].ToString() + "','" + serverbillmaster.Rows[i]["remarks"].ToString() + "','" + serverbillmaster.Rows[i]["BillType"].ToString() + "','" + serverbillmaster.Rows[i]["billno"].ToString() + "','" + serverbillmaster.Rows[i]["totaladdtax"].ToString() + "','" + serverbillmaster.Rows[i]["roudoff"].ToString() + "','" + serverbillmaster.Rows[i]["Duedate"].ToString() + "','" + serverbillmaster.Rows[i]["totalaqty"].ToString() + "','" + serverbillmaster.Rows[i]["totalfree"].ToString() + "','" + serverbillmaster.Rows[i]["totaldiscount"].ToString() + "','" + serverbillmaster.Rows[i]["totaladddiscount"].ToString() + "','" + serverbillmaster.Rows[i]["totalamount"].ToString() + "','" + serverbillmaster.Rows[i]["totalservicejob"].ToString() + "','" + serverbillmaster.Rows[i]["totalcharges"].ToString() + "','" + serverbillmaster.Rows[i]["Delieveryat"].ToString() + "','" + serverbillmaster.Rows[i]["fraight"].ToString() + "','" + serverbillmaster.Rows[i]["vehicleno"].ToString() + "','" + serverbillmaster.Rows[i]["grrrno"].ToString() + "','" + serverbillmaster.Rows[i]["noofskids"].ToString() + "','" + serverbillmaster.Rows[i]["sgstamt"].ToString() + "','" + serverbillmaster.Rows[i]["cgatamt"].ToString() + "','" + serverbillmaster.Rows[i]["igstamt"].ToString() + "','" + serverbillmaster.Rows[i]["OrderStatus"].ToString() + "','" + serverbillmaster.Rows[i]["refno"].ToString() + "','" + serverbillmaster.Rows[i]["totalcess"].ToString() + "','" + serverbillmaster.Rows[i]["agentID"].ToString() + "','" + serverbillmaster.Rows[i]["originalbillno"].ToString() + "','" + serverbillmaster.Rows[i]["originalbilldate"].ToString() + "','" + serverbillmaster.Rows[i]["SyncID"].ToString() + "','" + serverbillmaster.Rows[i]["SyncDatetime"].ToString() + "')");
                            }
                        }
                    }
                    #endregion

                    //Bill ProductMaster
                    #region
                    DataTable serverbillProductmaster = conn.getdataset("Select * from BillProductMaster where SyncDatetime >'" + lastsyncdatetime.ToString("MM/dd/yyyy h:mm:ss") + "'", con);
                    if (serverbillProductmaster.Rows.Count > 0)
                    {
                        for (int i = 0; i < serverbillProductmaster.Rows.Count; i++)
                        {
                            if (serverbillProductmaster.Rows[i]["Billtype"].ToString() == "P")
                            {
                                serverbillProductmaster.Rows[i]["Billtype"] = "S";
                            }
                            serverbillProductmaster.AcceptChanges();
                            DataTable isexist = conn.getdataset("Select * from BillProductMaster where SyncID='" + serverbillProductmaster.Rows[i]["SyncID"].ToString() + "'");
                            if (isexist.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(isexist.Rows[0]["SyncID"].ToString()))
                                {
                                    string Server = serverbillProductmaster.Rows[i]["SyncDatetime"].ToString();
                                    string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                    DateTime localsyncdatetime = Convert.ToDateTime(local);
                                    DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                    if (localsyncdatetime < serversyncdatetime)
                                    {
                                        //Update in local
                                        conn.execute("UPDATE [dbo].[BillProductMaster] SET [Bill_No] ='" + serverbillProductmaster.Rows[i]["Bill_No"].ToString() + "',[Bill_Run_Date] = '" + serverbillProductmaster.Rows[i]["Bill_Run_Date"].ToString() + "',[Productname] = '" + serverbillProductmaster.Rows[i]["Productname"].ToString() + "',[Packing] = '" + serverbillProductmaster.Rows[i]["Packing"].ToString() + "',[Bags] = '" + serverbillProductmaster.Rows[i]["Bags"].ToString() + "',[MRP] = '" + serverbillProductmaster.Rows[i]["MRP"].ToString() + "',[Pqty] = '" + serverbillProductmaster.Rows[i]["Pqty"].ToString() + "',[Aqty] = '" + serverbillProductmaster.Rows[i]["Aqty"].ToString() + "',[Rate] = '" + serverbillProductmaster.Rows[i]["Rate"].ToString() + "',[Per] = '" + serverbillProductmaster.Rows[i]["Per"].ToString() + "',[Total] = '" + serverbillProductmaster.Rows[i]["Total"].ToString() + "',[Tax] = '" + serverbillProductmaster.Rows[i]["Tax"].ToString() + "',[Amount] = '" + serverbillProductmaster.Rows[i]["Amount"].ToString() + "',[isactive] = '" + serverbillProductmaster.Rows[i]["isactive"].ToString() + "',[qty] = '" + serverbillProductmaster.Rows[i]["qty"].ToString() + "',[Billtype] = '" + serverbillProductmaster.Rows[i]["Billtype"].ToString() + "',[billno] = '" + serverbillProductmaster.Rows[i]["billno"].ToString() + "',[addtax] = '" + serverbillProductmaster.Rows[i]["addtax"].ToString() + "',[batch] = '" + serverbillProductmaster.Rows[i]["batch"].ToString() + "',[free] = '" + serverbillProductmaster.Rows[i]["free"].ToString() + "',[discountper] = '" + serverbillProductmaster.Rows[i]["discountper"].ToString() + "',[discountamt] = '" + serverbillProductmaster.Rows[i]["discountamt"].ToString() + "',[productid] = '" + serverbillProductmaster.Rows[i]["productid"].ToString() + "',[sgstper] = '" + serverbillProductmaster.Rows[i]["sgstper"].ToString() + "',[sgstamt] = '" + serverbillProductmaster.Rows[i]["sgstamt"].ToString() + "',[cgstper] = '" + serverbillProductmaster.Rows[i]["cgstper"].ToString() + "',[cgstamt] = '" + serverbillProductmaster.Rows[i]["cgstamt"].ToString() + "',[igstper] = '" + serverbillProductmaster.Rows[i]["igstper"].ToString() + "',[igdtamt] = '" + serverbillProductmaster.Rows[i]["igdtamt"].ToString() + "',[addtaxper] = '" + serverbillProductmaster.Rows[i]["addtaxper"].ToString() + "',[serialno] = '" + serverbillProductmaster.Rows[i]["serialno"].ToString() + "',[refno] = '" + serverbillProductmaster.Rows[i]["refno"].ToString() + "',[cess] = '" + serverbillProductmaster.Rows[i]["cess"].ToString() + "',[SyncID] = '" + serverbillProductmaster.Rows[i]["SyncID"].ToString() + "',[SyncDatetime] = '" + serverbillProductmaster.Rows[i]["SyncDatetime"].ToString() + "' WHERE SyncID='" + serverbillProductmaster.Rows[i]["SyncID"].ToString() + "'");
                                    }
                                    else
                                    {
                                        //Update in Server
                                    }
                                }
                                else
                                {
                                    string Server = serverbillProductmaster.Rows[i]["SyncDatetime"].ToString();
                                    string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                    DateTime localsyncdatetime = Convert.ToDateTime(local);
                                    DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                    if (localsyncdatetime < serversyncdatetime)
                                    {
                                        //Insert in local
                                        conn.execute("INSERT INTO [dbo].[BillProductMaster]([Bill_No],[Bill_Run_Date],[Productname],[Packing],[Bags],[MRP],[Pqty],[Aqty],[Rate],[Per],[Total],[Tax],[Amount],[isactive],[qty],[Billtype],[billno],[addtax],[batch],[free],[discountper],[discountamt],[productid],[sgstper],[sgstamt],[cgstper],[cgstamt],[igstper],[igdtamt],[addtaxper],[serialno],[refno],[cess],[SyncID],[SyncDatetime])VALUES('" + serverbillProductmaster.Rows[i]["Bill_No"].ToString() + "','" + serverbillProductmaster.Rows[i]["Bill_Run_Date"].ToString() + "','" + serverbillProductmaster.Rows[i]["Productname"].ToString() + "','" + serverbillProductmaster.Rows[i]["Packing"].ToString() + "','" + serverbillProductmaster.Rows[i]["Bags"].ToString() + "','" + serverbillProductmaster.Rows[i]["MRP"].ToString() + "','" + serverbillProductmaster.Rows[i]["Pqty"].ToString() + "','" + serverbillProductmaster.Rows[i]["Aqty"].ToString() + "','" + serverbillProductmaster.Rows[i]["Rate"].ToString() + "','" + serverbillProductmaster.Rows[i]["Per"].ToString() + "','" + serverbillProductmaster.Rows[i]["Total"].ToString() + "','" + serverbillProductmaster.Rows[i]["Tax"].ToString() + "','" + serverbillProductmaster.Rows[i]["Amount"].ToString() + "','" + serverbillProductmaster.Rows[i]["isactive"].ToString() + "','" + serverbillProductmaster.Rows[i]["qty"].ToString() + "','" + serverbillProductmaster.Rows[i]["Billtype"].ToString() + "','" + serverbillProductmaster.Rows[i]["billno"].ToString() + "','" + serverbillProductmaster.Rows[i]["addtax"].ToString() + "','" + serverbillProductmaster.Rows[i]["batch"].ToString() + "','" + serverbillProductmaster.Rows[i]["free"].ToString() + "','" + serverbillProductmaster.Rows[i]["discountper"].ToString() + "','" + serverbillProductmaster.Rows[i]["discountamt"].ToString() + "','" + serverbillProductmaster.Rows[i]["productid"].ToString() + "','" + serverbillProductmaster.Rows[i]["sgstper"].ToString() + "','" + serverbillProductmaster.Rows[i]["sgstamt"].ToString() + "','" + serverbillProductmaster.Rows[i]["cgstper"].ToString() + "','" + serverbillProductmaster.Rows[i]["cgstamt"].ToString() + "','" + serverbillProductmaster.Rows[i]["igstper"].ToString() + "','" + serverbillProductmaster.Rows[i]["igdtamt"].ToString() + "','" + serverbillProductmaster.Rows[i]["addtaxper"].ToString() + "','" + serverbillProductmaster.Rows[i]["serialno"].ToString() + "','" + serverbillProductmaster.Rows[i]["refno"].ToString() + "','" + serverbillProductmaster.Rows[i]["cess"].ToString() + "','" + serverbillProductmaster.Rows[i]["SyncID"].ToString() + "','" + serverbillProductmaster.Rows[i]["SyncDatetime"].ToString() + "')");
                                        //conn.execute("INSERT INTO [dbo].[BillMaster]([Bill_No],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[BillType],[billno],[totaladdtax],[roudoff],[Duedate],[totalaqty],[totalfree],[totaldiscount],[totaladddiscount],[totalamount],[totalservicejob],[totalcharges],[Delieveryat],[fraight],[vehicleno],[grrrno],[noofskids],[sgstamt],[cgatamt],[igstamt],[OrderStatus],[refno],[totalcess],[agentID],[originalbillno],[originalbilldate],[SyncID],[SyncDatetime])VALUES('" + serverbillmaster.Rows[i]["Bill_No"].ToString() + "','" + serverbillmaster.Rows[i]["Bill_Date"].ToString() + "','" + serverbillmaster.Rows[i]["Terms"].ToString() + "','" + serverbillmaster.Rows[i]["ClientID"].ToString() + "','" + serverbillmaster.Rows[i]["PO_No"].ToString() + "','" + serverbillmaster.Rows[i]["SaleType"].ToString() + "','" + serverbillmaster.Rows[i]["count"].ToString() + "','" + serverbillmaster.Rows[i]["totalqty"].ToString() + "','" + serverbillmaster.Rows[i]["totalbasic"].ToString() + "','" + serverbillmaster.Rows[i]["totaltax"].ToString() + "','" + serverbillmaster.Rows[i]["totalnet"].ToString() + "','" + serverbillmaster.Rows[i]["isactive"].ToString() + "','" + serverbillmaster.Rows[i]["apprweight"].ToString() + "','" + serverbillmaster.Rows[i]["dispatchdetails"].ToString() + "','" + serverbillmaster.Rows[i]["remarks"].ToString() + "','" + serverbillmaster.Rows[i]["BillType"].ToString() + "','" + serverbillmaster.Rows[i]["billno"].ToString() + "','" + serverbillmaster.Rows[i]["totaladdtax"].ToString() + "','" + serverbillmaster.Rows[i]["roudoff"].ToString() + "','" + serverbillmaster.Rows[i]["Duedate"].ToString() + "','" + serverbillmaster.Rows[i]["totalaqty"].ToString() + "','" + serverbillmaster.Rows[i]["totalfree"].ToString() + "','" + serverbillmaster.Rows[i]["totaldiscount"].ToString() + "','" + serverbillmaster.Rows[i]["totaladddiscount"].ToString() + "','" + serverbillmaster.Rows[i]["totalamount"].ToString() + "','" + serverbillmaster.Rows[i]["totalservicejob"].ToString() + "','" + serverbillmaster.Rows[i]["totalcharges"].ToString() + "','" + serverbillmaster.Rows[i]["Delieveryat"].ToString() + "','" + serverbillmaster.Rows[i]["fraight"].ToString() + "','" + serverbillmaster.Rows[i]["vehicleno"].ToString() + "','" + serverbillmaster.Rows[i]["grrrno"].ToString() + "','" + serverbillmaster.Rows[i]["noofskids"].ToString() + "','" + serverbillmaster.Rows[i]["sgstamt"].ToString() + "','" + serverbillmaster.Rows[i]["cgatamt"].ToString() + "','" + serverbillmaster.Rows[i]["igstamt"].ToString() + "','" + serverbillmaster.Rows[i]["OrderStatus"].ToString() + "','" + serverbillmaster.Rows[i]["refno"].ToString() + "','" + serverbillmaster.Rows[i]["totalcess"].ToString() + "','" + serverbillmaster.Rows[i]["agentID"].ToString() + "','" + serverbillmaster.Rows[i]["originalbillno"].ToString() + "','" + serverbillmaster.Rows[i]["originalbilldate"].ToString() + "','" + serverbillmaster.Rows[i]["SyncID"].ToString() + "','" + serverbillmaster.Rows[i]["SyncDatetime"].ToString() + "')");
                                    }
                                    else
                                    {
                                        //Insert in Server
                                    }
                                }
                            }
                            else
                            {
                                conn.execute("INSERT INTO [dbo].[BillProductMaster]([Bill_No],[Bill_Run_Date],[Productname],[Packing],[Bags],[MRP],[Pqty],[Aqty],[Rate],[Per],[Total],[Tax],[Amount],[isactive],[qty],[Billtype],[billno],[addtax],[batch],[free],[discountper],[discountamt],[productid],[sgstper],[sgstamt],[cgstper],[cgstamt],[igstper],[igdtamt],[addtaxper],[serialno],[refno],[cess],[SyncID],[SyncDatetime])VALUES('" + serverbillProductmaster.Rows[i]["Bill_No"].ToString() + "','" + serverbillProductmaster.Rows[i]["Bill_Run_Date"].ToString() + "','" + serverbillProductmaster.Rows[i]["Productname"].ToString() + "','" + serverbillProductmaster.Rows[i]["Packing"].ToString() + "','" + serverbillProductmaster.Rows[i]["Bags"].ToString() + "','" + serverbillProductmaster.Rows[i]["MRP"].ToString() + "','" + serverbillProductmaster.Rows[i]["Pqty"].ToString() + "','" + serverbillProductmaster.Rows[i]["Aqty"].ToString() + "','" + serverbillProductmaster.Rows[i]["Rate"].ToString() + "','" + serverbillProductmaster.Rows[i]["Per"].ToString() + "','" + serverbillProductmaster.Rows[i]["Total"].ToString() + "','" + serverbillProductmaster.Rows[i]["Tax"].ToString() + "','" + serverbillProductmaster.Rows[i]["Amount"].ToString() + "','" + serverbillProductmaster.Rows[i]["isactive"].ToString() + "','" + serverbillProductmaster.Rows[i]["qty"].ToString() + "','" + serverbillProductmaster.Rows[i]["Billtype"].ToString() + "','" + serverbillProductmaster.Rows[i]["billno"].ToString() + "','" + serverbillProductmaster.Rows[i]["addtax"].ToString() + "','" + serverbillProductmaster.Rows[i]["batch"].ToString() + "','" + serverbillProductmaster.Rows[i]["free"].ToString() + "','" + serverbillProductmaster.Rows[i]["discountper"].ToString() + "','" + serverbillProductmaster.Rows[i]["discountamt"].ToString() + "','" + serverbillProductmaster.Rows[i]["productid"].ToString() + "','" + serverbillProductmaster.Rows[i]["sgstper"].ToString() + "','" + serverbillProductmaster.Rows[i]["sgstamt"].ToString() + "','" + serverbillProductmaster.Rows[i]["cgstper"].ToString() + "','" + serverbillProductmaster.Rows[i]["cgstamt"].ToString() + "','" + serverbillProductmaster.Rows[i]["igstper"].ToString() + "','" + serverbillProductmaster.Rows[i]["igdtamt"].ToString() + "','" + serverbillProductmaster.Rows[i]["addtaxper"].ToString() + "','" + serverbillProductmaster.Rows[i]["serialno"].ToString() + "','" + serverbillProductmaster.Rows[i]["refno"].ToString() + "','" + serverbillProductmaster.Rows[i]["cess"].ToString() + "','" + serverbillProductmaster.Rows[i]["SyncID"].ToString() + "','" + serverbillProductmaster.Rows[i]["SyncDatetime"].ToString() + "')");
                            }
                        }
                    }
                    #endregion

                    //Billchargesmaster
                    #region
                    DataTable serverbillchargesmaster = conn.getdataset("Select * from Billchargesmaster where SyncDatetime >'" + lastsyncdatetime.ToString("MM/dd/yyyy h:mm:ss") + "'", con);
                    if (serverbillchargesmaster.Rows.Count > 0)
                    {
                        for (int i = 0; i < serverbillchargesmaster.Rows.Count; i++)
                        {
                            if (serverbillchargesmaster.Rows[i]["Billtype"].ToString() == "P")
                            {
                                serverbillchargesmaster.Rows[i]["Billtype"] = "S";
                            }
                            serverbillchargesmaster.AcceptChanges();
                            DataTable isexist = conn.getdataset("Select * from Billchargesmaster where SyncID='" + serverbillchargesmaster.Rows[i]["SyncID"].ToString() + "'");
                            if (isexist.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(isexist.Rows[0]["SyncID"].ToString()))
                                {
                                    string Server = serverbillchargesmaster.Rows[i]["SyncDatetime"].ToString();
                                    string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                    DateTime localsyncdatetime = Convert.ToDateTime(local);
                                    DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                    if (localsyncdatetime < serversyncdatetime)
                                    {
                                        //Update in local
                                        conn.execute("UPDATE [dbo].[Billchargesmaster] SET [billno] = '" + serverbillchargesmaster.Rows[i]["billno"].ToString() + "',[perticulars] = '" + serverbillchargesmaster.Rows[i]["perticulars"].ToString() + "',[remarks] = '" + serverbillchargesmaster.Rows[i]["remarks"].ToString() + "',[value] = '" + serverbillchargesmaster.Rows[i]["value"].ToString() + "',[at] = '" + serverbillchargesmaster.Rows[i]["at"].ToString() + "',[plusminus] = '" + serverbillchargesmaster.Rows[i]["plusminus"].ToString() + "',[amount] = '" + serverbillchargesmaster.Rows[i]["amount"].ToString() + "',[billtype] = '" + serverbillchargesmaster.Rows[i]["billtype"].ToString() + "',[isactive] = '" + serverbillchargesmaster.Rows[i]["isactive"].ToString() + "',[valueofexp] = '" + serverbillchargesmaster.Rows[i]["valueofexp"].ToString() + "',[tax] = '" + serverbillchargesmaster.Rows[i]["tax"].ToString() + "',[sgst] = '" + serverbillchargesmaster.Rows[i]["sgst"].ToString() + "',[cgst] = '" + serverbillchargesmaster.Rows[i]["cgst"].ToString() + "',[igst] = '" + serverbillchargesmaster.Rows[i]["igst"].ToString() + "',[additax] ='" + serverbillchargesmaster.Rows[i]["additax"].ToString() + "',[addtaxamt] = '" + serverbillchargesmaster.Rows[i]["addtaxamt"].ToString() + "',[billsundryid] = '" + serverbillchargesmaster.Rows[i]["billsundryid"].ToString() + "',[SyncID] = '" + serverbillchargesmaster.Rows[i]["SyncID"].ToString() + "',[SyncDatetime] = '" + serverbillchargesmaster.Rows[i]["SyncDatetime"].ToString() + "' WHERE SyncID='" + serverbillchargesmaster.Rows[i]["SyncID"].ToString() + "'");
                                    }
                                    else
                                    {
                                        //Update in Server
                                    }
                                }
                                else
                                {
                                    string Server = serverbillchargesmaster.Rows[i]["SyncDatetime"].ToString();
                                    string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                    DateTime localsyncdatetime = Convert.ToDateTime(local);
                                    DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                    if (localsyncdatetime < serversyncdatetime)
                                    {
                                        //Insert in local
                                        conn.execute("INSERT INTO [dbo].[Billchargesmaster]([billno],[perticulars],[remarks],[value],[at],[plusminus],[amount],[billtype],[isactive],[valueofexp],[tax],[sgst],[cgst],[igst],[additax],[addtaxamt],[billsundryid],[SyncID],[SyncDatetime])VALUES('" + serverbillchargesmaster.Rows[i]["billno"].ToString() + "','" + serverbillchargesmaster.Rows[i]["perticulars"].ToString() + "','" + serverbillchargesmaster.Rows[i]["remarks"].ToString() + "','" + serverbillchargesmaster.Rows[i]["value"].ToString() + "','" + serverbillchargesmaster.Rows[i]["at"].ToString() + "','" + serverbillchargesmaster.Rows[i]["plusminus"].ToString() + "','" + serverbillchargesmaster.Rows[i]["amount"].ToString() + "','" + serverbillchargesmaster.Rows[i]["billtype"].ToString() + "','" + serverbillchargesmaster.Rows[i]["isactive"].ToString() + "','" + serverbillchargesmaster.Rows[i]["valueofexp"].ToString() + "','" + serverbillchargesmaster.Rows[i]["tax"].ToString() + "','" + serverbillchargesmaster.Rows[i]["sgst"].ToString() + "','" + serverbillchargesmaster.Rows[i]["cgst"].ToString() + "','" + serverbillchargesmaster.Rows[i]["igst"].ToString() + "','" + serverbillchargesmaster.Rows[i]["additax"].ToString() + "','" + serverbillchargesmaster.Rows[i]["addtaxamt"].ToString() + "','" + serverbillchargesmaster.Rows[i]["billsundryid"].ToString() + "','" + serverbillchargesmaster.Rows[i]["SyncID"].ToString() + "','" + serverbillchargesmaster.Rows[i]["SyncDatetime"].ToString() + "')");
                                    }
                                    else
                                    {
                                        //Insert in Server
                                    }
                                }
                            }
                            else
                            {
                                conn.execute("INSERT INTO [dbo].[Billchargesmaster]([billno],[perticulars],[remarks],[value],[at],[plusminus],[amount],[billtype],[isactive],[valueofexp],[tax],[sgst],[cgst],[igst],[additax],[addtaxamt],[billsundryid],[SyncID],[SyncDatetime])VALUES('" + serverbillchargesmaster.Rows[i]["billno"].ToString() + "','" + serverbillchargesmaster.Rows[i]["perticulars"].ToString() + "','" + serverbillchargesmaster.Rows[i]["remarks"].ToString() + "','" + serverbillchargesmaster.Rows[i]["value"].ToString() + "','" + serverbillchargesmaster.Rows[i]["at"].ToString() + "','" + serverbillchargesmaster.Rows[i]["plusminus"].ToString() + "','" + serverbillchargesmaster.Rows[i]["amount"].ToString() + "','" + serverbillchargesmaster.Rows[i]["billtype"].ToString() + "','" + serverbillchargesmaster.Rows[i]["isactive"].ToString() + "','" + serverbillchargesmaster.Rows[i]["valueofexp"].ToString() + "','" + serverbillchargesmaster.Rows[i]["tax"].ToString() + "','" + serverbillchargesmaster.Rows[i]["sgst"].ToString() + "','" + serverbillchargesmaster.Rows[i]["cgst"].ToString() + "','" + serverbillchargesmaster.Rows[i]["igst"].ToString() + "','" + serverbillchargesmaster.Rows[i]["additax"].ToString() + "','" + serverbillchargesmaster.Rows[i]["addtaxamt"].ToString() + "','" + serverbillchargesmaster.Rows[i]["billsundryid"].ToString() + "','" + serverbillchargesmaster.Rows[i]["SyncID"].ToString() + "','" + serverbillchargesmaster.Rows[i]["SyncDatetime"].ToString() + "')");
                            }
                        }
                    }
                    #endregion

                    //SaleOrderMaster
                    #region
                    DataTable serversaleordermaster = conn.getdataset("Select * from SaleOrderMaster where SyncDatetime >'" + lastsyncdatetime.ToString("MM/dd/yyyy h:mm:ss") + "'", con);
                    if (serversaleordermaster.Rows.Count > 0)
                    {
                        for (int i = 0; i < serversaleordermaster.Rows.Count; i++)
                        {
                            if (serversaleordermaster.Rows[i]["Billtype"].ToString() == "PO")
                            {
                                serversaleordermaster.Rows[i]["Billtype"] = "SO";
                                DataTable saletypeID = conn.getdataset("select purchasetypeid from Purchasetypemaster where isactive=1 and type='S'");
                                serversaleordermaster.Rows[i]["SaleType"] = saletypeID.Rows[0]["purchasetypeid"].ToString();
                            }
                            serversaleordermaster.AcceptChanges();
                            DataTable isexist = conn.getdataset("Select * from SaleOrderMaster where SyncID='" + serversaleordermaster.Rows[i]["SyncID"].ToString() + "'");
                            if (isexist.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(isexist.Rows[0]["SyncID"].ToString()))
                                {
                                    string Server = serversaleordermaster.Rows[i]["SyncDatetime"].ToString();
                                    string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                    DateTime localsyncdatetime = Convert.ToDateTime(local);
                                    DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                    if (localsyncdatetime < serversyncdatetime)
                                    {
                                        //Update in local

                                        conn.execute("UPDATE [dbo].[SaleOrderMaster] SET [Bill_No] ='" + serversaleordermaster.Rows[i]["Bill_No"].ToString() + "',[Bill_Date] = '" + serversaleordermaster.Rows[i]["Bill_Date"].ToString() + "',[Terms] = '" + serversaleordermaster.Rows[i]["Terms"].ToString() + "',[ClientID] = '" + ClientID + "',[PO_No] = '" + serversaleordermaster.Rows[i]["PO_No"].ToString() + "',[SaleType] = '" + serversaleordermaster.Rows[i]["SaleType"].ToString() + "',[count] = '" + serversaleordermaster.Rows[i]["count"].ToString() + "',[totalqty] = '" + serversaleordermaster.Rows[i]["totalqty"].ToString() + "',[totalbasic] = '" + serversaleordermaster.Rows[i]["totalbasic"].ToString() + "',[totaltax] = '" + serversaleordermaster.Rows[i]["totaltax"].ToString() + "',[totalnet] = '" + serversaleordermaster.Rows[i]["totalnet"].ToString() + "',[isactive] = '" + serversaleordermaster.Rows[i]["isactive"].ToString() + "',[apprweight] = '" + serversaleordermaster.Rows[i]["apprweight"].ToString() + "',[dispatchdetails] = '" + serversaleordermaster.Rows[i]["dispatchdetails"].ToString() + "',[remarks] = '" + serversaleordermaster.Rows[i]["remarks"].ToString() + "',[BillType] = '" + serversaleordermaster.Rows[i]["BillType"].ToString() + "',[billno] = '" + serversaleordermaster.Rows[i]["billno"].ToString() + "',[totaladdtax] = '" + serversaleordermaster.Rows[i]["totaladdtax"].ToString() + "',[roudoff] = '" + serversaleordermaster.Rows[i]["roudoff"].ToString() + "',[Duedate] = '" + serversaleordermaster.Rows[i]["Duedate"].ToString() + "',[totalaqty] = '" + serversaleordermaster.Rows[i]["totalaqty"].ToString() + "',[totalfree] = '" + serversaleordermaster.Rows[i]["totalfree"].ToString() + "',[totaldiscount] = '" + serversaleordermaster.Rows[i]["totaldiscount"].ToString() + "',[totaladddiscount] = '" + serversaleordermaster.Rows[i]["totaladddiscount"].ToString() + "',[totalamount] = '" + serversaleordermaster.Rows[i]["totalamount"].ToString() + "',[totalservicejob] = '" + serversaleordermaster.Rows[i]["totalservicejob"].ToString() + "',[totalcharges] = '" + serversaleordermaster.Rows[i]["totalcharges"].ToString() + "',[Delieveryat] = '" + serversaleordermaster.Rows[i]["Delieveryat"].ToString() + "',[fraight] = '" + serversaleordermaster.Rows[i]["fraight"].ToString() + "',[vehicleno] = '" + serversaleordermaster.Rows[i]["vehicleno"].ToString() + "',[grrrno] = '" + serversaleordermaster.Rows[i]["grrrno"].ToString() + "',[noofskids] = '" + serversaleordermaster.Rows[i]["noofskids"].ToString() + "',[sgstamt] = '" + serversaleordermaster.Rows[i]["sgstamt"].ToString() + "',[cgatamt] = '" + serversaleordermaster.Rows[i]["cgatamt"].ToString() + "',[igstamt] = '" + serversaleordermaster.Rows[i]["igstamt"].ToString() + "',[OrderStatus] = '" + serversaleordermaster.Rows[i]["OrderStatus"].ToString() + "',[refno] = '" + serversaleordermaster.Rows[i]["refno"].ToString() + "',[totalcess] = '" + serversaleordermaster.Rows[i]["totalcess"].ToString() + "',[agentID] = '" + serversaleordermaster.Rows[i]["agentID"].ToString() + "',[SyncID] = '" + serversaleordermaster.Rows[i]["SyncID"].ToString() + "',[SyncDatetime] = '" + serversaleordermaster.Rows[i]["SyncDatetime"].ToString() + "' WHERE SyncID='" + serversaleordermaster.Rows[i]["SyncID"].ToString() + "'");
                                    }
                                    else
                                    {
                                        //Update in Server
                                    }
                                }
                                else
                                {
                                    string Server = serversaleordermaster.Rows[i]["SyncDatetime"].ToString();
                                    string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                    DateTime localsyncdatetime = Convert.ToDateTime(local);
                                    DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                    if (localsyncdatetime < serversyncdatetime)
                                    {
                                        //Insert in local

                                        conn.execute("INSERT INTO [dbo].[SaleOrderMaster]([Bill_No],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[BillType],[billno],[totaladdtax],[roudoff],[Duedate],[totalaqty],[totalfree],[totaldiscount],[totaladddiscount],[totalamount],[totalservicejob],[totalcharges],[Delieveryat],[fraight],[vehicleno],[grrrno],[noofskids],[sgstamt],[cgatamt],[igstamt],[OrderStatus],[refno],[totalcess],[agentID],[SyncID],[SyncDatetime])VALUES('" + serversaleordermaster.Rows[i]["Bill_No"].ToString() + "','" + serversaleordermaster.Rows[i]["Bill_Date"].ToString() + "','" + serversaleordermaster.Rows[i]["Terms"].ToString() + "','" + ClientID + "','" + serversaleordermaster.Rows[i]["PO_No"].ToString() + "','" + serversaleordermaster.Rows[i]["SaleType"].ToString() + "','" + serversaleordermaster.Rows[i]["count"].ToString() + "','" + serversaleordermaster.Rows[i]["totalqty"].ToString() + "','" + serversaleordermaster.Rows[i]["totalbasic"].ToString() + "','" + serversaleordermaster.Rows[i]["totaltax"].ToString() + "','" + serversaleordermaster.Rows[i]["totalnet"].ToString() + "','" + serversaleordermaster.Rows[i]["isactive"].ToString() + "','" + serversaleordermaster.Rows[i]["apprweight"].ToString() + "','" + serversaleordermaster.Rows[i]["dispatchdetails"].ToString() + "','" + serversaleordermaster.Rows[i]["remarks"].ToString() + "','" + serversaleordermaster.Rows[i]["BillType"].ToString() + "','" + serversaleordermaster.Rows[i]["billno"].ToString() + "','" + serversaleordermaster.Rows[i]["totaladdtax"].ToString() + "','" + serversaleordermaster.Rows[i]["roudoff"].ToString() + "','" + serversaleordermaster.Rows[i]["Duedate"].ToString() + "','" + serversaleordermaster.Rows[i]["totalaqty"].ToString() + "','" + serversaleordermaster.Rows[i]["totalfree"].ToString() + "','" + serversaleordermaster.Rows[i]["totaldiscount"].ToString() + "','" + serversaleordermaster.Rows[i]["totaladddiscount"].ToString() + "','" + serversaleordermaster.Rows[i]["totalamount"].ToString() + "','" + serversaleordermaster.Rows[i]["totalservicejob"].ToString() + "','" + serversaleordermaster.Rows[i]["totalcharges"].ToString() + "','" + serversaleordermaster.Rows[i]["Delieveryat"].ToString() + "','" + serversaleordermaster.Rows[i]["fraight"].ToString() + "','" + serversaleordermaster.Rows[i]["vehicleno"].ToString() + "','" + serversaleordermaster.Rows[i]["grrrno"].ToString() + "','" + serversaleordermaster.Rows[i]["noofskids"].ToString() + "','" + serversaleordermaster.Rows[i]["sgstamt"].ToString() + "','" + serversaleordermaster.Rows[i]["cgatamt"].ToString() + "','" + serversaleordermaster.Rows[i]["igstamt"].ToString() + "','" + serversaleordermaster.Rows[i]["OrderStatus"].ToString() + "','" + serversaleordermaster.Rows[i]["refno"].ToString() + "','" + serversaleordermaster.Rows[i]["totalcess"].ToString() + "','" + serversaleordermaster.Rows[i]["agentID"].ToString() + "','" + serversaleordermaster.Rows[i]["SyncID"].ToString() + "','" + serversaleordermaster.Rows[i]["SyncDatetime"].ToString() + "')");
                                    }
                                    else
                                    {
                                        //Insert in Server
                                    }
                                }
                            }
                            else
                            {
                                conn.execute("INSERT INTO [dbo].[SaleOrderMaster]([Bill_No],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[BillType],[billno],[totaladdtax],[roudoff],[Duedate],[totalaqty],[totalfree],[totaldiscount],[totaladddiscount],[totalamount],[totalservicejob],[totalcharges],[Delieveryat],[fraight],[vehicleno],[grrrno],[noofskids],[sgstamt],[cgatamt],[igstamt],[OrderStatus],[refno],[totalcess],[agentID],[SyncID],[SyncDatetime])VALUES('" + serversaleordermaster.Rows[i]["Bill_No"].ToString() + "','" + serversaleordermaster.Rows[i]["Bill_Date"].ToString() + "','" + serversaleordermaster.Rows[i]["Terms"].ToString() + "','" + ClientID + "','" + serversaleordermaster.Rows[i]["PO_No"].ToString() + "','" + serversaleordermaster.Rows[i]["SaleType"].ToString() + "','" + serversaleordermaster.Rows[i]["count"].ToString() + "','" + serversaleordermaster.Rows[i]["totalqty"].ToString() + "','" + serversaleordermaster.Rows[i]["totalbasic"].ToString() + "','" + serversaleordermaster.Rows[i]["totaltax"].ToString() + "','" + serversaleordermaster.Rows[i]["totalnet"].ToString() + "','" + serversaleordermaster.Rows[i]["isactive"].ToString() + "','" + serversaleordermaster.Rows[i]["apprweight"].ToString() + "','" + serversaleordermaster.Rows[i]["dispatchdetails"].ToString() + "','" + serversaleordermaster.Rows[i]["remarks"].ToString() + "','" + serversaleordermaster.Rows[i]["BillType"].ToString() + "','" + serversaleordermaster.Rows[i]["billno"].ToString() + "','" + serversaleordermaster.Rows[i]["totaladdtax"].ToString() + "','" + serversaleordermaster.Rows[i]["roudoff"].ToString() + "','" + serversaleordermaster.Rows[i]["Duedate"].ToString() + "','" + serversaleordermaster.Rows[i]["totalaqty"].ToString() + "','" + serversaleordermaster.Rows[i]["totalfree"].ToString() + "','" + serversaleordermaster.Rows[i]["totaldiscount"].ToString() + "','" + serversaleordermaster.Rows[i]["totaladddiscount"].ToString() + "','" + serversaleordermaster.Rows[i]["totalamount"].ToString() + "','" + serversaleordermaster.Rows[i]["totalservicejob"].ToString() + "','" + serversaleordermaster.Rows[i]["totalcharges"].ToString() + "','" + serversaleordermaster.Rows[i]["Delieveryat"].ToString() + "','" + serversaleordermaster.Rows[i]["fraight"].ToString() + "','" + serversaleordermaster.Rows[i]["vehicleno"].ToString() + "','" + serversaleordermaster.Rows[i]["grrrno"].ToString() + "','" + serversaleordermaster.Rows[i]["noofskids"].ToString() + "','" + serversaleordermaster.Rows[i]["sgstamt"].ToString() + "','" + serversaleordermaster.Rows[i]["cgatamt"].ToString() + "','" + serversaleordermaster.Rows[i]["igstamt"].ToString() + "','" + serversaleordermaster.Rows[i]["OrderStatus"].ToString() + "','" + serversaleordermaster.Rows[i]["refno"].ToString() + "','" + serversaleordermaster.Rows[i]["totalcess"].ToString() + "','" + serversaleordermaster.Rows[i]["agentID"].ToString() + "','" + serversaleordermaster.Rows[i]["SyncID"].ToString() + "','" + serversaleordermaster.Rows[i]["SyncDatetime"].ToString() + "')");
                            }
                        }
                    }
                    #endregion

                    //SaleOrderProductMaster
                    #region
                    DataTable serverSaleOrderProductmaster = conn.getdataset("Select * from SaleOrderProductMaster where SyncDatetime >'" + lastsyncdatetime.ToString("MM/dd/yyyy h:mm:ss") + "'", con);
                    if (serverSaleOrderProductmaster.Rows.Count > 0)
                    {
                        for (int i = 0; i < serverSaleOrderProductmaster.Rows.Count; i++)
                        {
                            if (serverSaleOrderProductmaster.Rows[i]["Billtype"].ToString() == "PO")
                            {
                                serverSaleOrderProductmaster.Rows[i]["Billtype"] = "SO";
                            }
                            serverSaleOrderProductmaster.AcceptChanges();
                            DataTable isexist = conn.getdataset("Select * from SaleOrderProductMaster where SyncID='" + serverSaleOrderProductmaster.Rows[i]["SyncID"].ToString() + "'");
                            if (isexist.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(isexist.Rows[0]["SyncID"].ToString()))
                                {
                                    string Server = serverSaleOrderProductmaster.Rows[i]["SyncDatetime"].ToString();
                                    string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                    DateTime localsyncdatetime = Convert.ToDateTime(local);
                                    DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                    if (localsyncdatetime < serversyncdatetime)
                                    {
                                        //Update in local
                                        conn.execute("UPDATE [dbo].[SaleOrderProductMaster] SET [Bill_No] ='" + serverSaleOrderProductmaster.Rows[i]["Bill_No"].ToString() + "',[Bill_Run_Date] = '" + serverSaleOrderProductmaster.Rows[i]["Bill_Run_Date"].ToString() + "',[Productname] = '" + serverSaleOrderProductmaster.Rows[i]["Productname"].ToString() + "',[Packing] = '" + serverSaleOrderProductmaster.Rows[i]["Packing"].ToString() + "',[Bags] = '" + serverSaleOrderProductmaster.Rows[i]["Bags"].ToString() + "',[MRP] = '" + serverSaleOrderProductmaster.Rows[i]["MRP"].ToString() + "',[Pqty] = '" + serverSaleOrderProductmaster.Rows[i]["Pqty"].ToString() + "',[Aqty] = '" + serverSaleOrderProductmaster.Rows[i]["Aqty"].ToString() + "',[Rate] = '" + serverSaleOrderProductmaster.Rows[i]["Rate"].ToString() + "',[Per] = '" + serverSaleOrderProductmaster.Rows[i]["Per"].ToString() + "',[Total] = '" + serverSaleOrderProductmaster.Rows[i]["Total"].ToString() + "',[Tax] = '" + serverSaleOrderProductmaster.Rows[i]["Tax"].ToString() + "',[Amount] = '" + serverSaleOrderProductmaster.Rows[i]["Amount"].ToString() + "',[isactive] = '" + serverSaleOrderProductmaster.Rows[i]["isactive"].ToString() + "',[qty] = '" + serverSaleOrderProductmaster.Rows[i]["qty"].ToString() + "',[Billtype] = '" + serverSaleOrderProductmaster.Rows[i]["Billtype"].ToString() + "',[billno] = '" + serverSaleOrderProductmaster.Rows[i]["billno"].ToString() + "',[addtax] = '" + serverSaleOrderProductmaster.Rows[i]["addtax"].ToString() + "',[batch] = '" + serverSaleOrderProductmaster.Rows[i]["batch"].ToString() + "',[free] = '" + serverSaleOrderProductmaster.Rows[i]["free"].ToString() + "',[discountper] = '" + serverSaleOrderProductmaster.Rows[i]["discountper"].ToString() + "',[discountamt] = '" + serverSaleOrderProductmaster.Rows[i]["discountamt"].ToString() + "',[productid] = '" + serverSaleOrderProductmaster.Rows[i]["productid"].ToString() + "',[sgstper] = '" + serverSaleOrderProductmaster.Rows[i]["sgstper"].ToString() + "',[sgstamt] = '" + serverSaleOrderProductmaster.Rows[i]["sgstamt"].ToString() + "',[cgstper] = '" + serverSaleOrderProductmaster.Rows[i]["cgstper"].ToString() + "',[cgstamt] = '" + serverSaleOrderProductmaster.Rows[i]["cgstamt"].ToString() + "',[igstper] = '" + serverSaleOrderProductmaster.Rows[i]["igstper"].ToString() + "',[igdtamt] = '" + serverSaleOrderProductmaster.Rows[i]["igdtamt"].ToString() + "',[addtaxper] = '" + serverSaleOrderProductmaster.Rows[i]["addtaxper"].ToString() + "',[serialno] = '" + serverSaleOrderProductmaster.Rows[i]["serialno"].ToString() + "',[refno] = '" + serverSaleOrderProductmaster.Rows[i]["refno"].ToString() + "',[cess] = '" + serverSaleOrderProductmaster.Rows[i]["cess"].ToString() + "',[SyncID] = '" + serverSaleOrderProductmaster.Rows[i]["SyncID"].ToString() + "',[SyncDatetime] = '" + serverSaleOrderProductmaster.Rows[i]["SyncDatetime"].ToString() + "' WHERE SyncID='" + serverSaleOrderProductmaster.Rows[i]["SyncID"].ToString() + "'");
                                    }
                                    else
                                    {
                                        //Update in Server
                                    }
                                }
                                else
                                {
                                    string Server = serverSaleOrderProductmaster.Rows[i]["SyncDatetime"].ToString();
                                    string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                    DateTime localsyncdatetime = Convert.ToDateTime(local);
                                    DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                    if (localsyncdatetime < serversyncdatetime)
                                    {
                                        //Insert in local
                                        conn.execute("INSERT INTO [dbo].[SaleOrderProductMaster]([Bill_No],[Bill_Run_Date],[Productname],[Packing],[Bags],[MRP],[Pqty],[Aqty],[Rate],[Per],[Total],[Tax],[Amount],[isactive],[qty],[Billtype],[billno],[addtax],[batch],[free],[discountper],[discountamt],[productid],[sgstper],[sgstamt],[cgstper],[cgstamt],[igstper],[igdtamt],[addtaxper],[serialno],[refno],[cess],[SyncID],[SyncDatetime])VALUES('" + serverSaleOrderProductmaster.Rows[i]["Bill_No"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Bill_Run_Date"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Productname"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Packing"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Bags"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["MRP"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Pqty"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Aqty"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Rate"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Per"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Total"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Tax"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Amount"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["isactive"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["qty"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Billtype"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["billno"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["addtax"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["batch"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["free"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["discountper"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["discountamt"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["productid"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["sgstper"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["sgstamt"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["cgstper"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["cgstamt"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["igstper"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["igdtamt"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["addtaxper"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["serialno"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["refno"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["cess"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["SyncID"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["SyncDatetime"].ToString() + "')");
                                        //conn.execute("INSERT INTO [dbo].[BillMaster]([Bill_No],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[BillType],[billno],[totaladdtax],[roudoff],[Duedate],[totalaqty],[totalfree],[totaldiscount],[totaladddiscount],[totalamount],[totalservicejob],[totalcharges],[Delieveryat],[fraight],[vehicleno],[grrrno],[noofskids],[sgstamt],[cgatamt],[igstamt],[OrderStatus],[refno],[totalcess],[agentID],[originalbillno],[originalbilldate],[SyncID],[SyncDatetime])VALUES('" + serverbillmaster.Rows[i]["Bill_No"].ToString() + "','" + serverbillmaster.Rows[i]["Bill_Date"].ToString() + "','" + serverbillmaster.Rows[i]["Terms"].ToString() + "','" + serverbillmaster.Rows[i]["ClientID"].ToString() + "','" + serverbillmaster.Rows[i]["PO_No"].ToString() + "','" + serverbillmaster.Rows[i]["SaleType"].ToString() + "','" + serverbillmaster.Rows[i]["count"].ToString() + "','" + serverbillmaster.Rows[i]["totalqty"].ToString() + "','" + serverbillmaster.Rows[i]["totalbasic"].ToString() + "','" + serverbillmaster.Rows[i]["totaltax"].ToString() + "','" + serverbillmaster.Rows[i]["totalnet"].ToString() + "','" + serverbillmaster.Rows[i]["isactive"].ToString() + "','" + serverbillmaster.Rows[i]["apprweight"].ToString() + "','" + serverbillmaster.Rows[i]["dispatchdetails"].ToString() + "','" + serverbillmaster.Rows[i]["remarks"].ToString() + "','" + serverbillmaster.Rows[i]["BillType"].ToString() + "','" + serverbillmaster.Rows[i]["billno"].ToString() + "','" + serverbillmaster.Rows[i]["totaladdtax"].ToString() + "','" + serverbillmaster.Rows[i]["roudoff"].ToString() + "','" + serverbillmaster.Rows[i]["Duedate"].ToString() + "','" + serverbillmaster.Rows[i]["totalaqty"].ToString() + "','" + serverbillmaster.Rows[i]["totalfree"].ToString() + "','" + serverbillmaster.Rows[i]["totaldiscount"].ToString() + "','" + serverbillmaster.Rows[i]["totaladddiscount"].ToString() + "','" + serverbillmaster.Rows[i]["totalamount"].ToString() + "','" + serverbillmaster.Rows[i]["totalservicejob"].ToString() + "','" + serverbillmaster.Rows[i]["totalcharges"].ToString() + "','" + serverbillmaster.Rows[i]["Delieveryat"].ToString() + "','" + serverbillmaster.Rows[i]["fraight"].ToString() + "','" + serverbillmaster.Rows[i]["vehicleno"].ToString() + "','" + serverbillmaster.Rows[i]["grrrno"].ToString() + "','" + serverbillmaster.Rows[i]["noofskids"].ToString() + "','" + serverbillmaster.Rows[i]["sgstamt"].ToString() + "','" + serverbillmaster.Rows[i]["cgatamt"].ToString() + "','" + serverbillmaster.Rows[i]["igstamt"].ToString() + "','" + serverbillmaster.Rows[i]["OrderStatus"].ToString() + "','" + serverbillmaster.Rows[i]["refno"].ToString() + "','" + serverbillmaster.Rows[i]["totalcess"].ToString() + "','" + serverbillmaster.Rows[i]["agentID"].ToString() + "','" + serverbillmaster.Rows[i]["originalbillno"].ToString() + "','" + serverbillmaster.Rows[i]["originalbilldate"].ToString() + "','" + serverbillmaster.Rows[i]["SyncID"].ToString() + "','" + serverbillmaster.Rows[i]["SyncDatetime"].ToString() + "')");
                                    }
                                    else
                                    {
                                        //Insert in Server
                                    }
                                }
                            }
                            else
                            {
                                conn.execute("INSERT INTO [dbo].[SaleOrderProductMaster]([Bill_No],[Bill_Run_Date],[Productname],[Packing],[Bags],[MRP],[Pqty],[Aqty],[Rate],[Per],[Total],[Tax],[Amount],[isactive],[qty],[Billtype],[billno],[addtax],[batch],[free],[discountper],[discountamt],[productid],[sgstper],[sgstamt],[cgstper],[cgstamt],[igstper],[igdtamt],[addtaxper],[serialno],[refno],[cess],[SyncID],[SyncDatetime])VALUES('" + serverSaleOrderProductmaster.Rows[i]["Bill_No"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Bill_Run_Date"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Productname"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Packing"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Bags"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["MRP"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Pqty"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Aqty"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Rate"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Per"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Total"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Tax"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Amount"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["isactive"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["qty"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Billtype"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["billno"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["addtax"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["batch"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["free"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["discountper"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["discountamt"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["productid"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["sgstper"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["sgstamt"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["cgstper"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["cgstamt"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["igstper"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["igdtamt"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["addtaxper"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["serialno"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["refno"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["cess"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["SyncID"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["SyncDatetime"].ToString() + "')");
                            }
                        }
                    }
                    #endregion

                    //SaleOrderchargesmaster
                    #region
                    DataTable serversaleorderchargesmaster = conn.getdataset("Select * from SaleOrderchargesmaster where SyncDatetime >'" + lastsyncdatetime.ToString("MM/dd/yyyy h:mm:ss") + "'", con);
                    if (serversaleorderchargesmaster.Rows.Count > 0)
                    {
                        for (int i = 0; i < serversaleorderchargesmaster.Rows.Count; i++)
                        {
                            if (serversaleorderchargesmaster.Rows[i]["Billtype"].ToString() == "PO")
                            {
                                serversaleorderchargesmaster.Rows[i]["Billtype"] = "SO";
                            }
                            serversaleorderchargesmaster.AcceptChanges();
                            DataTable isexist = conn.getdataset("Select * from SaleOrderchargesmaster where SyncID='" + serversaleorderchargesmaster.Rows[i]["SyncID"].ToString() + "'");
                            if (isexist.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(isexist.Rows[0]["SyncID"].ToString()))
                                {
                                    string Server = serversaleorderchargesmaster.Rows[i]["SyncDatetime"].ToString();
                                    string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                    DateTime localsyncdatetime = Convert.ToDateTime(local);
                                    DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                    if (localsyncdatetime < serversyncdatetime)
                                    {
                                        //Update in local
                                        conn.execute("UPDATE [dbo].[SaleOrderchargesmaster] SET [billno] = '" + serversaleorderchargesmaster.Rows[i]["billno"].ToString() + "',[perticulars] = '" + serversaleorderchargesmaster.Rows[i]["perticulars"].ToString() + "',[remarks] = '" + serversaleorderchargesmaster.Rows[i]["remarks"].ToString() + "',[value] = '" + serversaleorderchargesmaster.Rows[i]["value"].ToString() + "',[at] = '" + serversaleorderchargesmaster.Rows[i]["at"].ToString() + "',[plusminus] = '" + serversaleorderchargesmaster.Rows[i]["plusminus"].ToString() + "',[amount] = '" + serversaleorderchargesmaster.Rows[i]["amount"].ToString() + "',[billtype] = '" + serversaleorderchargesmaster.Rows[i]["billtype"].ToString() + "',[isactive] = '" + serversaleorderchargesmaster.Rows[i]["isactive"].ToString() + "',[valueofexp] = '" + serversaleorderchargesmaster.Rows[i]["valueofexp"].ToString() + "',[tax] = '" + serversaleorderchargesmaster.Rows[i]["tax"].ToString() + "',[sgst] = '" + serversaleorderchargesmaster.Rows[i]["sgst"].ToString() + "',[cgst] = '" + serversaleorderchargesmaster.Rows[i]["cgst"].ToString() + "',[igst] = '" + serversaleorderchargesmaster.Rows[i]["igst"].ToString() + "',[additax] ='" + serversaleorderchargesmaster.Rows[i]["additax"].ToString() + "',[addtaxamt] = '" + serversaleorderchargesmaster.Rows[i]["addtaxamt"].ToString() + "',[billsundryid] = '" + serversaleorderchargesmaster.Rows[i]["billsundryid"].ToString() + "',[SyncID] = '" + serversaleorderchargesmaster.Rows[i]["SyncID"].ToString() + "',[SyncDatetime] = '" + serversaleorderchargesmaster.Rows[i]["SyncDatetime"].ToString() + "' WHERE SyncID='" + serversaleorderchargesmaster.Rows[i]["SyncID"].ToString() + "'");
                                    }
                                    else
                                    {
                                        //Update in Server
                                    }
                                }
                                else
                                {
                                    string Server = serversaleorderchargesmaster.Rows[i]["SyncDatetime"].ToString();
                                    string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                    DateTime localsyncdatetime = Convert.ToDateTime(local);
                                    DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                    if (localsyncdatetime < serversyncdatetime)
                                    {
                                        //Insert in local
                                        conn.execute("INSERT INTO [dbo].[SaleOrderchargesmaster]([billno],[perticulars],[remarks],[value],[at],[plusminus],[amount],[billtype],[isactive],[valueofexp],[tax],[sgst],[cgst],[igst],[additax],[addtaxamt],[billsundryid],[SyncID],[SyncDatetime])VALUES('" + serversaleorderchargesmaster.Rows[i]["billno"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["perticulars"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["remarks"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["value"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["at"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["plusminus"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["amount"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["billtype"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["isactive"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["valueofexp"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["tax"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["sgst"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["cgst"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["igst"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["additax"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["addtaxamt"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["billsundryid"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["SyncID"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["SyncDatetime"].ToString() + "')");
                                    }
                                    else
                                    {
                                        //Insert in Server
                                    }
                                }
                            }
                            else
                            {
                                conn.execute("INSERT INTO [dbo].[SaleOrderchargesmaster]([billno],[perticulars],[remarks],[value],[at],[plusminus],[amount],[billtype],[isactive],[valueofexp],[tax],[sgst],[cgst],[igst],[additax],[addtaxamt],[billsundryid],[SyncID],[SyncDatetime])VALUES('" + serversaleorderchargesmaster.Rows[i]["billno"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["perticulars"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["remarks"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["value"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["at"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["plusminus"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["amount"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["billtype"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["isactive"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["valueofexp"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["tax"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["sgst"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["cgst"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["igst"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["additax"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["addtaxamt"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["billsundryid"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["SyncID"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["SyncDatetime"].ToString() + "')");
                            }
                        }
                    }
                    #endregion

                    //Serials
                    #region
                    DataTable serverserial = conn.getdataset("Select * from Serials where SyncDatetime >'" + lastsyncdatetime.ToString("MM/dd/yyyy h:mm:ss") + "'", con);
                    if (serverserial.Rows.Count > 0)
                    {
                        for (int i = 0; i < serverserial.Rows.Count; i++)
                        {
                            DataTable isexist = conn.getdataset("Select * from Serials where SyncID='" + serverserial.Rows[i]["SyncID"].ToString() + "'");
                            if (isexist.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(isexist.Rows[0]["SyncID"].ToString()))
                                {
                                    string Server = serverserial.Rows[i]["SyncDatetime"].ToString();
                                    string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                    DateTime localsyncdatetime = Convert.ToDateTime(local);
                                    DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                    if (localsyncdatetime < serversyncdatetime)
                                    {
                                        //Update in local
                                        conn.execute("UPDATE [dbo].[Serials] SET [Date] ='" + serverserial.Rows[i]["Date"].ToString() + "',[VoucherID] ='" + serverserial.Rows[i]["VoucherID"].ToString() + "',[TransactionID] ='" + serverserial.Rows[i]["TransactionID"].ToString() + "',[SerialNo] ='" + serverserial.Rows[i]["SerialNo"].ToString() + "',[TransactionType] ='" + serverserial.Rows[i]["TransactionType"].ToString() + "',[VchNo] ='" + serverserial.Rows[i]["VchNo"].ToString() + "',[PurchaseTypeID] ='" + serverserial.Rows[i]["PurchaseTypeID"].ToString() + "',[PurchaseTypeName] ='" + serverserial.Rows[i]["PurchaseTypeName"].ToString() + "',[SaleTypeID] ='" + serverserial.Rows[i]["SaleTypeID"].ToString() + "',[SaleTypeName] ='" + serverserial.Rows[i]["SaleTypeName"].ToString() + "',[PartyID] ='" + serverserial.Rows[i]["PartyID"].ToString() + "',[PartyName] ='" + serverserial.Rows[i]["PartyName"].ToString() + "',[ItemCompanyID] ='" + serverserial.Rows[i]["ItemCompanyID"].ToString() + "',[SyncID] ='" + serverserial.Rows[i]["SyncID"].ToString() + "',[SyncDatetime] ='" + serverserial.Rows[i]["SyncDatetime"].ToString() + "',[isactive] ='" + serverserial.Rows[i]["isactive"].ToString() + "' where SyncID='" + serverserial.Rows[i]["SyncID"].ToString() + "'");
                                    }
                                    else
                                    {
                                        //Update in Server
                                    }
                                }
                                else
                                {
                                    string Server = serverserial.Rows[i]["SyncDatetime"].ToString();
                                    string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                    DateTime localsyncdatetime = Convert.ToDateTime(local);
                                    DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                    if (localsyncdatetime < serversyncdatetime)
                                    {
                                        //Insert in local
                                        conn.execute("INSERT INTO [dbo].[Serials]([Date],[VoucherID],[TransactionID],[SerialNo],[TransactionType],[VchNo],[PurchaseTypeID],[PurchaseTypeName],[SaleTypeID],[SaleTypeName],[PartyID],[PartyName],[ItemCompanyID],[SyncID],[SyncDatetime],[isactive])VALUES('" + serverserial.Rows[i]["Date"].ToString() + "','" + serverserial.Rows[i]["VoucherID"].ToString() + "','" + serverserial.Rows[i]["TransactionID"].ToString() + "','" + serverserial.Rows[i]["SerialNo"].ToString() + "','" + serverserial.Rows[i]["TransactionType"].ToString() + "','" + serverserial.Rows[i]["VchNo"].ToString() + "','" + serverserial.Rows[i]["PurchaseTypeID"].ToString() + "','" + serverserial.Rows[i]["PurchaseTypeName"].ToString() + "','" + serverserial.Rows[i]["SaleTypeID"].ToString() + "','" + serverserial.Rows[i]["SaleTypeName"].ToString() + "','" + serverserial.Rows[i]["PartyID"].ToString() + "','" + serverserial.Rows[i]["PartyName"].ToString() + "','" + serverserial.Rows[i]["ItemCompanyID"].ToString() + "','" + serverserial.Rows[i]["SyncID"].ToString() + "','" + serverserial.Rows[i]["SyncDatetime"].ToString() + "','" + serverserial.Rows[i]["isactive"].ToString() + "')");
                                    }
                                    else
                                    {
                                        //Insert in Server
                                    }
                                }
                            }
                            else
                            {
                                conn.execute("INSERT INTO [dbo].[Serials]([Date],[VoucherID],[TransactionID],[SerialNo],[TransactionType],[VchNo],[PurchaseTypeID],[PurchaseTypeName],[SaleTypeID],[SaleTypeName],[PartyID],[PartyName],[ItemCompanyID],[SyncID],[SyncDatetime],[isactive])VALUES('" + serverserial.Rows[i]["Date"].ToString() + "','" + serverserial.Rows[i]["VoucherID"].ToString() + "','" + serverserial.Rows[i]["TransactionID"].ToString() + "','" + serverserial.Rows[i]["SerialNo"].ToString() + "','" + serverserial.Rows[i]["TransactionType"].ToString() + "','" + serverserial.Rows[i]["VchNo"].ToString() + "','" + serverserial.Rows[i]["PurchaseTypeID"].ToString() + "','" + serverserial.Rows[i]["PurchaseTypeName"].ToString() + "','" + serverserial.Rows[i]["SaleTypeID"].ToString() + "','" + serverserial.Rows[i]["SaleTypeName"].ToString() + "','" + serverserial.Rows[i]["PartyID"].ToString() + "','" + serverserial.Rows[i]["PartyName"].ToString() + "','" + serverserial.Rows[i]["ItemCompanyID"].ToString() + "','" + serverserial.Rows[i]["SyncID"].ToString() + "','" + serverserial.Rows[i]["SyncDatetime"].ToString() + "','" + serverserial.Rows[i]["isactive"].ToString() + "')");
                            }
                        }
                    }
                    #endregion

                    //Ledger
                    #region
                    DataTable serverLedger = conn.getdataset("Select * from Ledger where SyncDatetime >'" + lastsyncdatetime.ToString("MM/dd/yyyy h:mm:ss") + "'", con);
                    if (serverLedger.Rows.Count > 0)
                    {
                        for (int i = 0; i < serverLedger.Rows.Count; i++)
                        {
                            string trantype = serverLedger.Rows[i]["TranType"].ToString();
                            if (trantype == "Purchase")
                            {
                                serverLedger.Rows[i]["TranType"] = "Sale";
                            }
                            serverLedger.AcceptChanges();
                            DataTable isexist = conn.getdataset("Select * from Ledger where SyncID='" + serverLedger.Rows[i]["SyncID"].ToString() + "'");
                            if (isexist.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(isexist.Rows[0]["SyncID"].ToString()))
                                {
                                    string Server = serverLedger.Rows[i]["SyncDatetime"].ToString();
                                    string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                    DateTime localsyncdatetime = Convert.ToDateTime(local);
                                    DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                    if (localsyncdatetime < serversyncdatetime)
                                    {
                                        //Update in local
                                        conn.execute("UPDATE [dbo].[Ledger] SET [VoucherID] ='" + serverLedger.Rows[i]["VoucherID"].ToString() + "',Date1='" + serverLedger.Rows[i]["Date1"].ToString() + "',TranType='" + serverLedger.Rows[i]["TranType"].ToString() + "',AccountID='" + serverLedger.Rows[i]["AccountID"].ToString() + "',AccountName='" + serverLedger.Rows[i]["AccountName"].ToString() + "',Amount='" + serverLedger.Rows[i]["Amount"].ToString() + "',DC='" + serverLedger.Rows[i]["DC"].ToString() + "',isactive='" + serverLedger.Rows[i]["isactive"].ToString() + "',OT1='" + serverLedger.Rows[i]["OT1"].ToString() + "',OD1='" + serverLedger.Rows[i]["OD1"].ToString() + "',SyncID='" + serverLedger.Rows[i]["SyncID"].ToString() + "',SyncDatetime='" + serverLedger.Rows[i]["SyncDatetime"].ToString() + "' where SyncID='" + serverLedger.Rows[i]["SyncID"].ToString() + "'");
                                    }
                                    else
                                    {
                                        //Update in Server
                                    }
                                }
                                else
                                {
                                    string Server = serverLedger.Rows[i]["SyncDatetime"].ToString();
                                    string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                    DateTime localsyncdatetime = Convert.ToDateTime(local);
                                    DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                    if (localsyncdatetime < serversyncdatetime)
                                    {
                                        //Insert in local
                                        conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OD1],[SyncID],[SyncDatetime]) values ('" + serverLedger.Rows[i]["VoucherID"].ToString() + "','" + serverLedger.Rows[i]["Date1"].ToString() + "','" + serverLedger.Rows[i]["TranType"].ToString() + "','" + serverLedger.Rows[i]["AccountID"].ToString() + "','" + serverLedger.Rows[i]["AccountName"].ToString() + "','" + serverLedger.Rows[i]["Amount"].ToString() + "','" + serverLedger.Rows[i]["DC"].ToString() + "','" + serverLedger.Rows[i]["isactive"].ToString() + "','" + serverLedger.Rows[i]["OT1"].ToString() + "','" + serverLedger.Rows[i]["OD1"].ToString() + "','" + serverLedger.Rows[i]["SyncID"].ToString() + "','" + serverLedger.Rows[i]["SyncDatetime"].ToString() + "')");
                                    }
                                    else
                                    {
                                        //Insert in Server
                                    }
                                }
                            }
                            else
                            {
                                conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OD1],[SyncID],[SyncDatetime]) values ('" + serverLedger.Rows[i]["VoucherID"].ToString() + "','" + serverLedger.Rows[i]["Date1"].ToString() + "','" + serverLedger.Rows[i]["TranType"].ToString() + "','" + serverLedger.Rows[i]["AccountID"].ToString() + "','" + serverLedger.Rows[i]["AccountName"].ToString() + "','" + serverLedger.Rows[i]["Amount"].ToString() + "','" + serverLedger.Rows[i]["DC"].ToString() + "','" + serverLedger.Rows[i]["isactive"].ToString() + "','" + serverLedger.Rows[i]["OT1"].ToString() + "','" + serverLedger.Rows[i]["OD1"].ToString() + "','" + serverLedger.Rows[i]["SyncID"].ToString() + "','" + serverLedger.Rows[i]["SyncDatetime"].ToString() + "')");
                            }
                        }
                    }
                    #endregion

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void bindclient()
        {
            try
            {
                ds = ods.getdata("select Authentication from SQLSetting where SQLServerName='ServerConnection'");
                DataTable client = new DataTable();
                client = ds.Tables[0];
                sServerConnection = client.Rows[0]["Authentication"].ToString();
                SqlConnection onlinecon = new SqlConnection(sServerConnection);
                //ProductMaster
                #region
                DataTable clientproductmaster = conn.getdataset("Select * from ProductMaster where SyncDatetime >'" + lastsyncdatetime.ToString("MM/dd/yyyy h:mm:ss") + "'");
                DataTable serverproductmaster = conn.getdataset("Select * from ProductMaster where SyncDatetime >'" + lastsyncdatetime.ToString("MM/dd/yyyy h:mm:ss") + "'", onlinecon);
                if (clientproductmaster.Rows.Count > 0)
                {
                    for (int i = 0; i < clientproductmaster.Rows.Count; i++)
                    {
                        DataTable isexist = conn.getdataset("Select * from ProductMaster where SyncID='" + clientproductmaster.Rows[i]["SyncID"].ToString() + "'", onlinecon);
                        if (isexist.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(isexist.Rows[0]["SyncID"].ToString()))
                            {
                                string local = clientproductmaster.Rows[i]["SyncDatetime"].ToString();
                                string Server = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Update in local
                                    conn.execute("UPDATE [dbo].[ProductMaster] SET [CompanyID] = '" + isexist.Rows[0]["CompanyID"].ToString() + "',[GroupName] = '" + isexist.Rows[0]["GroupName"].ToString() + "',[Product_Name] = '" + isexist.Rows[0]["Product_Name"].ToString() + "',[Unit] = '" + isexist.Rows[0]["Unit"].ToString() + "',[Altunit] = '" + isexist.Rows[0]["Altunit"].ToString() + "',[Convfactor] = '" + isexist.Rows[0]["Convfactor"].ToString() + "',[Packing] = '" + isexist.Rows[0]["Packing"].ToString() + "',[IsBatch] = '" + isexist.Rows[0]["IsBatch"].ToString() + "',[Hsn_Sac_Code] = '" + isexist.Rows[0]["Hsn_Sac_Code"].ToString() + "',[isactive] = '" + isexist.Rows[0]["isactive"].ToString() + "',[isserial] = '" + isexist.Rows[0]["isserial"].ToString() + "',[cessper] = '" + isexist.Rows[0]["cessper"].ToString() + "',[cessamt] = '" + isexist.Rows[0]["cessamt"].ToString() + "',[taxslab] = '" + isexist.Rows[0]["taxslab"].ToString() + "',[isHotProduct] = '" + isexist.Rows[0]["isHotProduct"].ToString() + "',[itemnumber] = '" + isexist.Rows[0]["itemnumber"].ToString() + "',[itemimage] = '" + isexist.Rows[0]["itemimage"].ToString() + "',[isshare] = '" + isexist.Rows[0]["isshare"].ToString() + "',[SyncID] = '" + isexist.Rows[0]["SyncID"].ToString() + "',[SyncDatetime] = '" + isexist.Rows[0]["SyncDatetime"].ToString() + "' WHERE SyncID='" + isexist.Rows[0]["SyncID"].ToString() + "'");
                                    if (isexist.Rows[0]["isshare"].ToString() == "True" && isexist.Rows[0]["isactive"].ToString() == "True")
                                    {
                                        conn.execute("UPDATE [dbo].[ProductMaster] SET [isactive] ='" + "1" + "' where SyncID='" + isexist.Rows[0]["SyncID"].ToString() + "'");
                                    }
                                    else
                                    {
                                        conn.execute("UPDATE [dbo].[ProductMaster] SET [isactive] ='" + "0" + "' where SyncID='" + isexist.Rows[0]["SyncID"].ToString() + "'");
                                    }

                                }
                                else
                                {
                                    //Update in Server
                                   // conn.execute("UPDATE [dbo].[ProductMaster] SET [CompanyID] = '" + clientproductmaster.Rows[i]["CompanyID"].ToString() + "',[GroupName] = '" + clientproductmaster.Rows[i]["GroupName"].ToString() + "',[Product_Name] = '" + clientproductmaster.Rows[i]["Product_Name"].ToString() + "',[Unit] = '" + clientproductmaster.Rows[i]["Unit"].ToString() + "',[Altunit] = '" + clientproductmaster.Rows[i]["Altunit"].ToString() + "',[Convfactor] = '" + clientproductmaster.Rows[i]["Convfactor"].ToString() + "',[Packing] = '" + clientproductmaster.Rows[i]["Packing"].ToString() + "',[IsBatch] = '" + clientproductmaster.Rows[i]["IsBatch"].ToString() + "',[Hsn_Sac_Code] = '" + clientproductmaster.Rows[i]["Hsn_Sac_Code"].ToString() + "',[isactive] = '" + clientproductmaster.Rows[i]["isactive"].ToString() + "',[isserial] = '" + clientproductmaster.Rows[i]["isserial"].ToString() + "',[cessper] = '" + clientproductmaster.Rows[i]["cessper"].ToString() + "',[cessamt] = '" + clientproductmaster.Rows[i]["cessamt"].ToString() + "',[taxslab] = '" + clientproductmaster.Rows[i]["taxslab"].ToString() + "',[isHotProduct] = '" + clientproductmaster.Rows[i]["isHotProduct"].ToString() + "',[itemnumber] = '" + clientproductmaster.Rows[i]["itemnumber"].ToString() + "',[itemimage] = '" + clientproductmaster.Rows[i]["itemimage"].ToString() + "',[isshare] = '" + clientproductmaster.Rows[i]["isshare"].ToString() + "',[SyncID] = '" + clientproductmaster.Rows[i]["SyncID"].ToString() + "',[SyncDatetime] = '" + clientproductmaster.Rows[i]["SyncDatetime"].ToString() + "' WHERE SyncID='" + clientproductmaster.Rows[i]["SyncID"].ToString() + "'", con);
                                }
                            }
                        }
                        else
                        {
                          //  conn.execute("INSERT INTO [dbo].[ProductMaster]([CompanyID],[GroupName],[Product_Name],[Unit],[Altunit],[Convfactor],[Packing],[IsBatch],[Hsn_Sac_Code],[isactive],[isserial],[cessper],[cessamt],[taxslab],[isHotProduct],[itemnumber],[itemimage],[isshare],[SyncID],[SyncDatetime])VALUES('" + clientproductmaster.Rows[i]["CompanyID"].ToString() + "','" + clientproductmaster.Rows[i]["GroupName"].ToString() + "','" + clientproductmaster.Rows[i]["Product_Name"].ToString() + "','" + clientproductmaster.Rows[i]["Unit"].ToString() + "','" + clientproductmaster.Rows[i]["Altunit"].ToString() + "','" + clientproductmaster.Rows[i]["Convfactor"].ToString() + "','" + clientproductmaster.Rows[i]["Packing"].ToString() + "','" + clientproductmaster.Rows[i]["IsBatch"].ToString() + "','" + clientproductmaster.Rows[i]["Hsn_Sac_Code"].ToString() + "','" + clientproductmaster.Rows[i]["isactive"].ToString() + "','" + clientproductmaster.Rows[i]["isserial"].ToString() + "','" + clientproductmaster.Rows[i]["cessper"].ToString() + "','" + clientproductmaster.Rows[i]["cessamt"].ToString() + "','" + clientproductmaster.Rows[i]["taxslab"].ToString() + "','" + clientproductmaster.Rows[i]["isHotProduct"].ToString() + "','" + clientproductmaster.Rows[i]["itemnumber"].ToString() + "','" + clientproductmaster.Rows[i]["itemimage"].ToString() + "','" + clientproductmaster.Rows[i]["isshare"].ToString() + "','" + clientproductmaster.Rows[i]["SyncID"].ToString() + "','" + clientproductmaster.Rows[i]["SyncDatetime"].ToString() + "')", con);
                        }
                    }
                }
                if (serverproductmaster.Rows.Count > 0)
                {
                    for (int i = 0; i < serverproductmaster.Rows.Count; i++)
                    {
                        DataTable isexist = conn.getdataset("Select * from ProductMaster where SyncID='" + serverproductmaster.Rows[i]["SyncID"].ToString() + "'");
                        if (isexist.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(isexist.Rows[0]["SyncID"].ToString()))
                            {
                                string Server = serverproductmaster.Rows[i]["SyncDatetime"].ToString();
                                string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Update in local
                                    conn.execute("UPDATE [dbo].[ProductMaster] SET [CompanyID] = '" + serverproductmaster.Rows[i]["CompanyID"].ToString() + "',[GroupName] = '" + serverproductmaster.Rows[i]["GroupName"].ToString() + "',[Product_Name] = '" + serverproductmaster.Rows[i]["Product_Name"].ToString() + "',[Unit] = '" + serverproductmaster.Rows[i]["Unit"].ToString() + "',[Altunit] = '" + serverproductmaster.Rows[i]["Altunit"].ToString() + "',[Convfactor] = '" + serverproductmaster.Rows[i]["Convfactor"].ToString() + "',[Packing] = '" + serverproductmaster.Rows[i]["Packing"].ToString() + "',[IsBatch] = '" + serverproductmaster.Rows[i]["IsBatch"].ToString() + "',[Hsn_Sac_Code] = '" + serverproductmaster.Rows[i]["Hsn_Sac_Code"].ToString() + "',[isactive] = '" + serverproductmaster.Rows[i]["isactive"].ToString() + "',[isserial] = '" + serverproductmaster.Rows[i]["isserial"].ToString() + "',[cessper] = '" + serverproductmaster.Rows[i]["cessper"].ToString() + "',[cessamt] = '" + serverproductmaster.Rows[i]["cessamt"].ToString() + "',[taxslab] = '" + serverproductmaster.Rows[i]["taxslab"].ToString() + "',[isHotProduct] = '" + serverproductmaster.Rows[i]["isHotProduct"].ToString() + "',[itemnumber] = '" + serverproductmaster.Rows[i]["itemnumber"].ToString() + "',[itemimage] = '" + serverproductmaster.Rows[i]["itemimage"].ToString() + "',[isshare] = '" + serverproductmaster.Rows[i]["isshare"].ToString() + "',[SyncID] = '" + serverproductmaster.Rows[i]["SyncID"].ToString() + "',[SyncDatetime] = '" + serverproductmaster.Rows[i]["SyncDatetime"].ToString() + "' WHERE SyncID='" + serverproductmaster.Rows[i]["SyncID"].ToString() + "'");
                                    if (serverproductmaster.Rows[i]["isshare"].ToString() == "True" && serverproductmaster.Rows[i]["isactive"].ToString() == "True")
                                    {
                                        conn.execute("UPDATE [dbo].[ProductMaster] SET [isactive] ='" + "1" + "' where SyncID='" + serverproductmaster.Rows[i]["SyncID"].ToString() + "'");
                                    }
                                    else
                                    {
                                        conn.execute("UPDATE [dbo].[ProductMaster] SET [isactive] ='" + "0" + "' where SyncID='" + serverproductmaster.Rows[i]["SyncID"].ToString() + "'");
                                    }
                                }
                                else
                                {
                                    //Update in Server
                                  //  conn.execute("UPDATE [dbo].[ProductMaster] SET [CompanyID] = '" + isexist.Rows[0]["CompanyID"].ToString() + "',[GroupName] = '" + isexist.Rows[0]["GroupName"].ToString() + "',[Product_Name] = '" + isexist.Rows[0]["Product_Name"].ToString() + "',[Unit] = '" + isexist.Rows[0]["Unit"].ToString() + "',[Altunit] = '" + isexist.Rows[0]["Altunit"].ToString() + "',[Convfactor] = '" + isexist.Rows[0]["Convfactor"].ToString() + "',[Packing] = '" + isexist.Rows[0]["Packing"].ToString() + "',[IsBatch] = '" + isexist.Rows[0]["IsBatch"].ToString() + "',[Hsn_Sac_Code] = '" + isexist.Rows[0]["Hsn_Sac_Code"].ToString() + "',[isactive] = '" + isexist.Rows[0]["isactive"].ToString() + "',[isserial] = '" + isexist.Rows[0]["isserial"].ToString() + "',[cessper] = '" + isexist.Rows[0]["cessper"].ToString() + "',[cessamt] = '" + isexist.Rows[0]["cessamt"].ToString() + "',[taxslab] = '" + isexist.Rows[0]["taxslab"].ToString() + "',[isHotProduct] = '" + isexist.Rows[0]["isHotProduct"].ToString() + "',[itemnumber] = '" + isexist.Rows[0]["itemnumber"].ToString() + "',[itemimage] = '" + isexist.Rows[0]["itemimage"].ToString() + "',[isshare] = '" + isexist.Rows[0]["isshare"].ToString() + "',[SyncID] = '" + isexist.Rows[0]["SyncID"].ToString() + "',[SyncDatetime] = '" + isexist.Rows[0]["SyncDatetime"].ToString() + "' WHERE SyncID='" + isexist.Rows[0]["SyncID"].ToString() + "'", con);
                                }
                            }
                        }
                        else
                        {
                            //conn.execute("SET IDENTITY_INSERT ProductMaster ON");
                            conn.execute("SET IDENTITY_INSERT ProductMaster ON INSERT INTO [dbo].[ProductMaster]([ProductID],[CompanyID],[GroupName],[Product_Name],[Unit],[Altunit],[Convfactor],[Packing],[IsBatch],[Hsn_Sac_Code],[isactive],[isserial],[cessper],[cessamt],[taxslab],[isHotProduct],[itemnumber],[itemimage],[isshare],[SyncID],[SyncDatetime])VALUES('" + serverproductmaster.Rows[i]["ProductID"].ToString() + "','" + serverproductmaster.Rows[i]["CompanyID"].ToString() + "','" + serverproductmaster.Rows[i]["GroupName"].ToString() + "','" + serverproductmaster.Rows[i]["Product_Name"].ToString() + "','" + serverproductmaster.Rows[i]["Unit"].ToString() + "','" + serverproductmaster.Rows[i]["Altunit"].ToString() + "','" + serverproductmaster.Rows[i]["Convfactor"].ToString() + "','" + serverproductmaster.Rows[i]["Packing"].ToString() + "','" + serverproductmaster.Rows[i]["IsBatch"].ToString() + "','" + serverproductmaster.Rows[i]["Hsn_Sac_Code"].ToString() + "','" + serverproductmaster.Rows[i]["isactive"].ToString() + "','" + serverproductmaster.Rows[i]["isserial"].ToString() + "','" + serverproductmaster.Rows[i]["cessper"].ToString() + "','" + serverproductmaster.Rows[i]["cessamt"].ToString() + "','" + serverproductmaster.Rows[i]["taxslab"].ToString() + "','" + serverproductmaster.Rows[i]["isHotProduct"].ToString() + "','" + serverproductmaster.Rows[i]["itemnumber"].ToString() + "','" + serverproductmaster.Rows[i]["itemimage"].ToString() + "','" + serverproductmaster.Rows[i]["isshare"].ToString() + "','" + serverproductmaster.Rows[i]["SyncID"].ToString() + "','" + serverproductmaster.Rows[i]["SyncDatetime"].ToString() + "')SET IDENTITY_INSERT ProductMaster OFF");
                            if (serverproductmaster.Rows[i]["isshare"].ToString() == "True" && serverproductmaster.Rows[i]["isactive"].ToString() == "True")
                            {
                                conn.execute("UPDATE [dbo].[ProductMaster] SET [isactive] ='" + "1" + "' where SyncID='" + serverproductmaster.Rows[i]["SyncID"].ToString() + "'");
                            }
                            else
                            {
                                conn.execute("UPDATE [dbo].[ProductMaster] SET [isactive] ='" + "0" + "' where SyncID='" + serverproductmaster.Rows[i]["SyncID"].ToString() + "'");
                            }
                           // conn.execute("SET IDENTITY_INSERT ProductMaster OFF");
                        }
                    }
                }
                #endregion

                //ProductPriceMaster
                #region
                DataTable clientproductpricemaster = conn.getdataset("Select * from ProductPriceMaster where SyncDatetime >'" + lastsyncdatetime.ToString("MM/dd/yyyy h:mm:ss") + "'");
                DataTable serverproductpricemaster = conn.getdataset("Select * from ProductPriceMaster where SyncDatetime >'" + lastsyncdatetime.ToString("MM/dd/yyyy h:mm:ss") + "'", onlinecon);
                if (clientproductpricemaster.Rows.Count > 0)
                {
                    for (int i = 0; i < clientproductpricemaster.Rows.Count; i++)
                    {
                        DataTable isexist = conn.getdataset("Select * from ProductPriceMaster where SyncID='" + clientproductpricemaster.Rows[i]["SyncID"].ToString() + "'", onlinecon);
                        if (isexist.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(isexist.Rows[0]["SyncID"].ToString()))
                            {
                                string local = clientproductpricemaster.Rows[i]["SyncDatetime"].ToString();
                                string Server = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Update in local
                                    conn.execute("UPDATE [dbo].[ProductPriceMaster] SET [Productid] = '" + isexist.Rows[0]["Productid"].ToString() + "',[Batchno] = '" + isexist.Rows[0]["Batchno"].ToString() + "',[BasicPrice] = '" + isexist.Rows[0]["BasicPrice"].ToString() + "',[SalePrice] = '" + isexist.Rows[0]["SalePrice"].ToString() + "',[MRP] = '" + isexist.Rows[0]["MRP"].ToString() + "',[PurchasePrice] = '" + isexist.Rows[0]["PurchasePrice"].ToString() + "',[Barcode] = '" + isexist.Rows[0]["Barcode"].ToString() + "',[OpStock] = '" + isexist.Rows[0]["OpStock"].ToString() + "',[ExpDt] = '" + isexist.Rows[0]["ExpDt"].ToString() + "',[mfgdt] = '" + isexist.Rows[0]["mfgdt"].ToString() + "',[Expdays] = '" + isexist.Rows[0]["Expdays"].ToString() + "',[SelfVal] = '" + isexist.Rows[0]["SelfVal"].ToString() + "',[minsaleprice] = '" + isexist.Rows[0]["minsaleprice"].ToString() + "',[oploose] = '" + isexist.Rows[0]["oploose"].ToString() + "',[opstockval] = '" + isexist.Rows[0]["opstockval"].ToString() + "',[isactive] ='" + isexist.Rows[0]["isactive"].ToString() + "',[SyncID] = '" + isexist.Rows[0]["SyncID"].ToString() + "',[SyncDatetime] = '" + isexist.Rows[0]["SyncDatetime"].ToString() + "',[isshare] = '" + isexist.Rows[0]["isshare"].ToString() + "' WHERE SyncID='" + isexist.Rows[0]["SyncID"].ToString() + "'");
                                    if (isexist.Rows[0]["isshare"].ToString() == "True" && isexist.Rows[0]["isactive"].ToString() == "True")
                                    {
                                        conn.execute("UPDATE [dbo].[ProductPriceMaster] SET [isactive] ='" + "1" + "' where SyncID='" + isexist.Rows[0]["SyncID"].ToString() + "'");
                                    }
                                    else
                                    {
                                        conn.execute("UPDATE [dbo].[ProductPriceMaster] SET [isactive] ='" + "0" + "' where SyncID='" + isexist.Rows[0]["SyncID"].ToString() + "'");
                                    }
                                }
                                else
                                {
                                    //Update in Server
                                  //  conn.execute("UPDATE [dbo].[ProductPriceMaster] SET [Productid] = '" + clientproductpricemaster.Rows[i]["Productid"].ToString() + "',[Batchno] = '" + clientproductpricemaster.Rows[i]["Batchno"].ToString() + "',[BasicPrice] = '" + clientproductpricemaster.Rows[i]["BasicPrice"].ToString() + "',[SalePrice] = '" + clientproductpricemaster.Rows[i]["SalePrice"].ToString() + "',[MRP] = '" + clientproductpricemaster.Rows[i]["MRP"].ToString() + "',[PurchasePrice] = '" + clientproductpricemaster.Rows[i]["PurchasePrice"].ToString() + "',[Barcode] = '" + clientproductpricemaster.Rows[i]["Barcode"].ToString() + "',[OpStock] = '" + clientproductpricemaster.Rows[i]["OpStock"].ToString() + "',[ExpDt] = '" + clientproductpricemaster.Rows[i]["ExpDt"].ToString() + "',[mfgdt] = '" + clientproductpricemaster.Rows[i]["mfgdt"].ToString() + "',[Expdays] = '" + clientproductpricemaster.Rows[i]["Expdays"].ToString() + "',[SelfVal] = '" + clientproductpricemaster.Rows[i]["SelfVal"].ToString() + "',[minsaleprice] = '" + clientproductpricemaster.Rows[i]["minsaleprice"].ToString() + "',[oploose] = '" + clientproductpricemaster.Rows[i]["oploose"].ToString() + "',[opstockval] = '" + clientproductpricemaster.Rows[i]["opstockval"].ToString() + "',[isactive] ='" + clientproductpricemaster.Rows[i]["isactive"].ToString() + "',[SyncID] = '" + clientproductpricemaster.Rows[i]["SyncID"].ToString() + "',[SyncDatetime] = '" + clientproductpricemaster.Rows[i]["SyncDatetime"].ToString() + "',[isshare] = '" + clientproductpricemaster.Rows[i]["isshare"].ToString() + "' WHERE SyncID='" + clientproductpricemaster.Rows[i]["SyncID"].ToString() + "'", con);
                                }
                            }
                        }
                        else
                        {
                            //conn.execute("INSERT INTO [dbo].[ProductPriceMaster]([Productid],[Batchno],[BasicPrice],[SalePrice],[MRP],[PurchasePrice],[Barcode],[OpStock],[ExpDt],[mfgdt],[Expdays],[SelfVal],[minsaleprice],[oploose],[opstockval],[isactive],[SyncID],[SyncDatetime],[isshare])VALUES('" + clientproductpricemaster.Rows[i]["Productid"].ToString() + "','" + clientproductpricemaster.Rows[i]["Batchno"].ToString() + "','" + clientproductpricemaster.Rows[i]["BasicPrice"].ToString() + "','" + clientproductpricemaster.Rows[i]["SalePrice"].ToString() + "','" + clientproductpricemaster.Rows[i]["MRP"].ToString() + "','" + clientproductpricemaster.Rows[i]["PurchasePrice"].ToString() + "','" + clientproductpricemaster.Rows[i]["Barcode"].ToString() + "','" + clientproductpricemaster.Rows[i]["OpStock"].ToString() + "','" + clientproductpricemaster.Rows[i]["ExpDt"].ToString() + "','" + clientproductpricemaster.Rows[i]["mfgdt"].ToString() + "','" + clientproductpricemaster.Rows[i]["Expdays"].ToString() + "','" + clientproductpricemaster.Rows[i]["SelfVal"].ToString() + "','" + clientproductpricemaster.Rows[i]["minsaleprice"].ToString() + "','" + clientproductpricemaster.Rows[i]["oploose"].ToString() + "','" + clientproductpricemaster.Rows[i]["opstockval"].ToString() + "','" + clientproductpricemaster.Rows[i]["isactive"].ToString() + "','" + clientproductpricemaster.Rows[i]["SyncID"].ToString() + "','" + clientproductpricemaster.Rows[i]["SyncDatetime"].ToString() + "','" + clientproductpricemaster.Rows[i]["isshare"].ToString() + "')", con);
                        }
                    }
                }
                if (serverproductpricemaster.Rows.Count > 0)
                {
                    for (int i = 0; i < serverproductpricemaster.Rows.Count; i++)
                    {
                        DataTable isexist = conn.getdataset("Select * from ProductPriceMaster where SyncID='" + serverproductpricemaster.Rows[i]["SyncID"].ToString() + "'");
                        if (isexist.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(isexist.Rows[0]["SyncID"].ToString()))
                            {
                                string Server = serverproductpricemaster.Rows[i]["SyncDatetime"].ToString();
                                string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Update in local
                                    conn.execute("UPDATE [dbo].[ProductPriceMaster] SET [Productid] = '" + serverproductpricemaster.Rows[i]["Productid"].ToString() + "',[Batchno] = '" + serverproductpricemaster.Rows[i]["Batchno"].ToString() + "',[BasicPrice] = '" + serverproductpricemaster.Rows[i]["BasicPrice"].ToString() + "',[SalePrice] = '" + serverproductpricemaster.Rows[i]["SalePrice"].ToString() + "',[MRP] = '" + serverproductpricemaster.Rows[i]["MRP"].ToString() + "',[PurchasePrice] = '" + serverproductpricemaster.Rows[i]["PurchasePrice"].ToString() + "',[Barcode] = '" + serverproductpricemaster.Rows[i]["Barcode"].ToString() + "',[OpStock] = '" + serverproductpricemaster.Rows[i]["OpStock"].ToString() + "',[ExpDt] = '" + serverproductpricemaster.Rows[i]["ExpDt"].ToString() + "',[mfgdt] = '" + serverproductpricemaster.Rows[i]["mfgdt"].ToString() + "',[Expdays] = '" + serverproductpricemaster.Rows[i]["Expdays"].ToString() + "',[SelfVal] = '" + serverproductpricemaster.Rows[i]["SelfVal"].ToString() + "',[minsaleprice] = '" + serverproductpricemaster.Rows[i]["minsaleprice"].ToString() + "',[oploose] = '" + serverproductpricemaster.Rows[i]["oploose"].ToString() + "',[opstockval] = '" + serverproductpricemaster.Rows[i]["opstockval"].ToString() + "',[isactive] ='" + serverproductpricemaster.Rows[i]["isactive"].ToString() + "',[SyncID] = '" + serverproductpricemaster.Rows[i]["SyncID"].ToString() + "',[SyncDatetime] = '" + serverproductpricemaster.Rows[i]["SyncDatetime"].ToString() + "',[isshare] = '" + serverproductpricemaster.Rows[i]["isshare"].ToString() + "' WHERE SyncID='" + serverproductpricemaster.Rows[i]["SyncID"].ToString() + "'");
                                    if (serverproductpricemaster.Rows[i]["isshare"].ToString() == "True" && serverproductpricemaster.Rows[i]["isactive"].ToString() == "True")
                                    {
                                        conn.execute("UPDATE [dbo].[ProductPriceMaster] SET [isactive] ='" + "1" + "' where SyncID='" + serverproductpricemaster.Rows[i]["SyncID"].ToString() + "'");
                                    }
                                    else
                                    {
                                        conn.execute("UPDATE [dbo].[ProductPriceMaster] SET [isactive] ='" + "0" + "' where SyncID='" + serverproductpricemaster.Rows[i]["SyncID"].ToString() + "'");
                                    }
                                }
                                else
                                {
                                    //Update in Server
                                    //conn.execute("UPDATE [dbo].[ProductPriceMaster] SET [Productid] = '" + isexist.Rows[0]["Productid"].ToString() + "',[Batchno] = '" + isexist.Rows[0]["Batchno"].ToString() + "',[BasicPrice] = '" + isexist.Rows[0]["BasicPrice"].ToString() + "',[SalePrice] = '" + isexist.Rows[0]["SalePrice"].ToString() + "',[MRP] = '" + isexist.Rows[0]["MRP"].ToString() + "',[PurchasePrice] = '" + isexist.Rows[0]["PurchasePrice"].ToString() + "',[Barcode] = '" + isexist.Rows[0]["Barcode"].ToString() + "',[OpStock] = '" + isexist.Rows[0]["OpStock"].ToString() + "',[ExpDt] = '" + isexist.Rows[0]["ExpDt"].ToString() + "',[mfgdt] = '" + isexist.Rows[0]["mfgdt"].ToString() + "',[Expdays] = '" + isexist.Rows[0]["Expdays"].ToString() + "',[SelfVal] = '" + isexist.Rows[0]["SelfVal"].ToString() + "',[minsaleprice] = '" + isexist.Rows[0]["minsaleprice"].ToString() + "',[oploose] = '" + isexist.Rows[0]["oploose"].ToString() + "',[opstockval] = '" + isexist.Rows[0]["opstockval"].ToString() + "',[isactive] ='" + isexist.Rows[0]["isactive"].ToString() + "',[SyncID] = '" + isexist.Rows[0]["SyncID"].ToString() + "',[SyncDatetime] = '" + isexist.Rows[0]["SyncDatetime"].ToString() + "',[isshare] = '" + isexist.Rows[0]["isshare"].ToString() + "' WHERE SyncID='" + isexist.Rows[0]["SyncID"].ToString() + "'", con);
                                }
                            }
                        }
                        else
                        {
                            conn.execute("INSERT INTO [dbo].[ProductPriceMaster]([Productid],[Batchno],[BasicPrice],[SalePrice],[MRP],[PurchasePrice],[Barcode],[OpStock],[ExpDt],[mfgdt],[Expdays],[SelfVal],[minsaleprice],[oploose],[opstockval],[isactive],[SyncID],[SyncDatetime],[isshare])VALUES('" + serverproductpricemaster.Rows[i]["Productid"].ToString() + "','" + serverproductpricemaster.Rows[i]["Batchno"].ToString() + "','" + serverproductpricemaster.Rows[i]["BasicPrice"].ToString() + "','" + serverproductpricemaster.Rows[i]["SalePrice"].ToString() + "','" + serverproductpricemaster.Rows[i]["MRP"].ToString() + "','" + serverproductpricemaster.Rows[i]["PurchasePrice"].ToString() + "','" + serverproductpricemaster.Rows[i]["Barcode"].ToString() + "','" + serverproductpricemaster.Rows[i]["OpStock"].ToString() + "','" + serverproductpricemaster.Rows[i]["ExpDt"].ToString() + "','" + serverproductpricemaster.Rows[i]["mfgdt"].ToString() + "','" + serverproductpricemaster.Rows[i]["Expdays"].ToString() + "','" + serverproductpricemaster.Rows[i]["SelfVal"].ToString() + "','" + serverproductpricemaster.Rows[i]["minsaleprice"].ToString() + "','" + serverproductpricemaster.Rows[i]["oploose"].ToString() + "','" + serverproductpricemaster.Rows[i]["opstockval"].ToString() + "','" + serverproductpricemaster.Rows[i]["isactive"].ToString() + "','" + serverproductpricemaster.Rows[i]["SyncID"].ToString() + "','" + serverproductpricemaster.Rows[i]["SyncDatetime"].ToString() + "','" + serverproductpricemaster.Rows[i]["isshare"].ToString() + "')");
                            if (serverproductpricemaster.Rows[i]["isshare"].ToString() == "True" && serverproductpricemaster.Rows[i]["isactive"].ToString() == "True")
                            {
                                conn.execute("UPDATE [dbo].[ProductPriceMaster] SET [isactive] ='" + "1" + "' where SyncID='" + serverproductpricemaster.Rows[i]["SyncID"].ToString() + "'");
                            }
                            else
                            {
                                conn.execute("UPDATE [dbo].[ProductPriceMaster] SET [isactive] ='" + "0" + "' where SyncID='" + serverproductpricemaster.Rows[i]["SyncID"].ToString() + "'");
                            }
                        }
                    }
                }
                #endregion
            }
            catch
            {
            }
        }
        public void bindserverconnection()
        {
            try
            {
                DataTable d = conn.getdataset("Select * from updatedatabase");
                try
                {
                    lastsyncdatetime = Convert.ToDateTime(d.Rows[0]["LastSyncDate"].ToString());
                }
                catch
                {
                    string startdate= conn.ExecuteScalar("select startdate from company where companyID='"+Master.companyId+"'");
                    lastsyncdatetime = Convert.ToDateTime(startdate);
                }


                
                conn.execute("Update updatedatabase set LastSyncDate='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "' where id='" + "1" + "'");
                string isfactory = conn.ExecuteScalar("select isfactory from Company where CompanyId='"+Master.companyId+"'");
                if (isfactory == "True")
                {
                    bindfactory();
                }
                else
                {
                    bindclient();
                }
                CultureInfo en = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentCulture = en;
                ds2 = ods.getdata("select * from Company where companyid='"+Master.companyId+"'");
                DataTable Sconnnection = new DataTable();
                Sconnnection = ds2.Tables[0];
                if (Sconnnection.Rows.Count > 0)
                {
                    sServerConnection = Sconnnection.Rows[0]["LinkRemote"].ToString();
                }
                SqlConnection con = new SqlConnection(sServerConnection);

                //ProductMaster
                #region
                DataTable clientproductmaster = conn.getdataset("Select * from ProductMaster where SyncDatetime >'" + lastsyncdatetime.ToString("MM/dd/yyyy h:mm:ss") + "'");
                DataTable serverproductmaster = conn.getdataset("Select * from ProductMaster where SyncDatetime >'" + lastsyncdatetime.ToString("MM/dd/yyyy h:mm:ss") + "'", con);
                if (clientproductmaster.Rows.Count > 0)
                {
                    for (int i = 0; i < clientproductmaster.Rows.Count; i++)
                    {
                        DataTable isexist = conn.getdataset("Select * from ProductMaster where SyncID='" + clientproductmaster.Rows[i]["SyncID"].ToString() + "'", con);
                        if (isexist.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(isexist.Rows[0]["SyncID"].ToString()))
                            {
                                string local = clientproductmaster.Rows[i]["SyncDatetime"].ToString();
                                string Server = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Update in local
                                    conn.execute("UPDATE [dbo].[ProductMaster] SET [CompanyID] = '" + isexist.Rows[0]["CompanyID"].ToString() + "',[GroupName] = '" + isexist.Rows[0]["GroupName"].ToString() + "',[Product_Name] = '" + isexist.Rows[0]["Product_Name"].ToString() + "',[Unit] = '" + isexist.Rows[0]["Unit"].ToString() + "',[Altunit] = '" + isexist.Rows[0]["Altunit"].ToString() + "',[Convfactor] = '" + isexist.Rows[0]["Convfactor"].ToString() + "',[Packing] = '" + isexist.Rows[0]["Packing"].ToString() + "',[IsBatch] = '" + isexist.Rows[0]["IsBatch"].ToString() + "',[Hsn_Sac_Code] = '" + isexist.Rows[0]["Hsn_Sac_Code"].ToString() + "',[isactive] = '" + isexist.Rows[0]["isactive"].ToString() + "',[isserial] = '" + isexist.Rows[0]["isserial"].ToString() + "',[cessper] = '" + isexist.Rows[0]["cessper"].ToString() + "',[cessamt] = '" + isexist.Rows[0]["cessamt"].ToString() + "',[taxslab] = '" + isexist.Rows[0]["taxslab"].ToString() + "',[isHotProduct] = '" + isexist.Rows[0]["isHotProduct"].ToString() + "',[itemnumber] = '" + isexist.Rows[0]["itemnumber"].ToString() + "',[itemimage] = '" + isexist.Rows[0]["itemimage"].ToString() + "',[isshare] = '" + isexist.Rows[0]["isshare"].ToString() + "',[SyncID] = '" + isexist.Rows[0]["SyncID"].ToString() + "',[SyncDatetime] = '" + isexist.Rows[0]["SyncDatetime"].ToString() + "' WHERE SyncID='" + isexist.Rows[0]["SyncID"].ToString() + "'");
                                }
                                else
                                {
                                    //Update in Server
                                    conn.execute("UPDATE [dbo].[ProductMaster] SET [CompanyID] = '" + clientproductmaster.Rows[i]["CompanyID"].ToString() + "',[GroupName] = '" + clientproductmaster.Rows[i]["GroupName"].ToString() + "',[Product_Name] = '" + clientproductmaster.Rows[i]["Product_Name"].ToString() + "',[Unit] = '" + clientproductmaster.Rows[i]["Unit"].ToString() + "',[Altunit] = '" + clientproductmaster.Rows[i]["Altunit"].ToString() + "',[Convfactor] = '" + clientproductmaster.Rows[i]["Convfactor"].ToString() + "',[Packing] = '" + clientproductmaster.Rows[i]["Packing"].ToString() + "',[IsBatch] = '" + clientproductmaster.Rows[i]["IsBatch"].ToString() + "',[Hsn_Sac_Code] = '" + clientproductmaster.Rows[i]["Hsn_Sac_Code"].ToString() + "',[isactive] = '" + clientproductmaster.Rows[i]["isactive"].ToString() + "',[isserial] = '" + clientproductmaster.Rows[i]["isserial"].ToString() + "',[cessper] = '" + clientproductmaster.Rows[i]["cessper"].ToString() + "',[cessamt] = '" + clientproductmaster.Rows[i]["cessamt"].ToString() + "',[taxslab] = '" + clientproductmaster.Rows[i]["taxslab"].ToString() + "',[isHotProduct] = '" + clientproductmaster.Rows[i]["isHotProduct"].ToString() + "',[itemnumber] = '" + clientproductmaster.Rows[i]["itemnumber"].ToString() + "',[itemimage] = '" + clientproductmaster.Rows[i]["itemimage"].ToString() + "',[isshare] = '" + clientproductmaster.Rows[i]["isshare"].ToString() + "',[SyncID] = '" + clientproductmaster.Rows[i]["SyncID"].ToString() + "',[SyncDatetime] = '" + clientproductmaster.Rows[i]["SyncDatetime"].ToString() + "' WHERE SyncID='" + clientproductmaster.Rows[i]["SyncID"].ToString() + "'",con);
                                }
                            }
                        }
                        else
                        {
                            conn.execute("INSERT INTO [dbo].[ProductMaster]([CompanyID],[GroupName],[Product_Name],[Unit],[Altunit],[Convfactor],[Packing],[IsBatch],[Hsn_Sac_Code],[isactive],[isserial],[cessper],[cessamt],[taxslab],[isHotProduct],[itemnumber],[itemimage],[isshare],[SyncID],[SyncDatetime])VALUES('" + clientproductmaster.Rows[i]["CompanyID"].ToString() + "','" + clientproductmaster.Rows[i]["GroupName"].ToString() + "','" + clientproductmaster.Rows[i]["Product_Name"].ToString() + "','" + clientproductmaster.Rows[i]["Unit"].ToString() + "','" + clientproductmaster.Rows[i]["Altunit"].ToString() + "','" + clientproductmaster.Rows[i]["Convfactor"].ToString() + "','" + clientproductmaster.Rows[i]["Packing"].ToString() + "','" + clientproductmaster.Rows[i]["IsBatch"].ToString() + "','" + clientproductmaster.Rows[i]["Hsn_Sac_Code"].ToString() + "','" + clientproductmaster.Rows[i]["isactive"].ToString() + "','" + clientproductmaster.Rows[i]["isserial"].ToString() + "','" + clientproductmaster.Rows[i]["cessper"].ToString() + "','" + clientproductmaster.Rows[i]["cessamt"].ToString() + "','" + clientproductmaster.Rows[i]["taxslab"].ToString() + "','" + clientproductmaster.Rows[i]["isHotProduct"].ToString() + "','" + clientproductmaster.Rows[i]["itemnumber"].ToString() + "','" + clientproductmaster.Rows[i]["itemimage"].ToString() + "','" + clientproductmaster.Rows[i]["isshare"].ToString() + "','" + clientproductmaster.Rows[i]["SyncID"].ToString() + "','" + clientproductmaster.Rows[i]["SyncDatetime"].ToString() + "')", con);
                        }
                    }
                }
                if (serverproductmaster.Rows.Count > 0)
                {
                    for (int i = 0; i < serverproductmaster.Rows.Count; i++)
                    {
                        DataTable isexist = conn.getdataset("Select * from ProductMaster where SyncID='" + serverproductmaster.Rows[i]["SyncID"].ToString() + "'");
                        if (isexist.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(isexist.Rows[0]["SyncID"].ToString()))
                            {
                                string Server = serverproductmaster.Rows[i]["SyncDatetime"].ToString();
                                string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Update in local
                                    conn.execute("UPDATE [dbo].[ProductMaster] SET [CompanyID] = '" + serverproductmaster.Rows[i]["CompanyID"].ToString() + "',[GroupName] = '" + serverproductmaster.Rows[i]["GroupName"].ToString() + "',[Product_Name] = '" + serverproductmaster.Rows[i]["Product_Name"].ToString() + "',[Unit] = '" + serverproductmaster.Rows[i]["Unit"].ToString() + "',[Altunit] = '" + serverproductmaster.Rows[i]["Altunit"].ToString() + "',[Convfactor] = '" + serverproductmaster.Rows[i]["Convfactor"].ToString() + "',[Packing] = '" + serverproductmaster.Rows[i]["Packing"].ToString() + "',[IsBatch] = '" + serverproductmaster.Rows[i]["IsBatch"].ToString() + "',[Hsn_Sac_Code] = '" + serverproductmaster.Rows[i]["Hsn_Sac_Code"].ToString() + "',[isactive] = '" + serverproductmaster.Rows[i]["isactive"].ToString() + "',[isserial] = '" + serverproductmaster.Rows[i]["isserial"].ToString() + "',[cessper] = '" + serverproductmaster.Rows[i]["cessper"].ToString() + "',[cessamt] = '" + serverproductmaster.Rows[i]["cessamt"].ToString() + "',[taxslab] = '" + serverproductmaster.Rows[i]["taxslab"].ToString() + "',[isHotProduct] = '" + serverproductmaster.Rows[i]["isHotProduct"].ToString() + "',[itemnumber] = '" + serverproductmaster.Rows[i]["itemnumber"].ToString() + "',[itemimage] = '" + serverproductmaster.Rows[i]["itemimage"].ToString() + "',[isshare] = '" + serverproductmaster.Rows[i]["isshare"].ToString() + "',[SyncID] = '" + serverproductmaster.Rows[i]["SyncID"].ToString() + "',[SyncDatetime] = '" + serverproductmaster.Rows[i]["SyncDatetime"].ToString() + "' WHERE SyncID='" + serverproductmaster.Rows[i]["SyncID"].ToString() + "'");
                                    
                                }
                                else
                                {
                                    //Update in Server
                                    conn.execute("UPDATE [dbo].[ProductMaster] SET [CompanyID] = '" + isexist.Rows[0]["CompanyID"].ToString() + "',[GroupName] = '" + isexist.Rows[0]["GroupName"].ToString() + "',[Product_Name] = '" + isexist.Rows[0]["Product_Name"].ToString() + "',[Unit] = '" + isexist.Rows[0]["Unit"].ToString() + "',[Altunit] = '" + isexist.Rows[0]["Altunit"].ToString() + "',[Convfactor] = '" + isexist.Rows[0]["Convfactor"].ToString() + "',[Packing] = '" + isexist.Rows[0]["Packing"].ToString() + "',[IsBatch] = '" + isexist.Rows[0]["IsBatch"].ToString() + "',[Hsn_Sac_Code] = '" + isexist.Rows[0]["Hsn_Sac_Code"].ToString() + "',[isactive] = '" + isexist.Rows[0]["isactive"].ToString() + "',[isserial] = '" + isexist.Rows[0]["isserial"].ToString() + "',[cessper] = '" + isexist.Rows[0]["cessper"].ToString() + "',[cessamt] = '" + isexist.Rows[0]["cessamt"].ToString() + "',[taxslab] = '" + isexist.Rows[0]["taxslab"].ToString() + "',[isHotProduct] = '" + isexist.Rows[0]["isHotProduct"].ToString() + "',[itemnumber] = '" + isexist.Rows[0]["itemnumber"].ToString() + "',[itemimage] = '" + isexist.Rows[0]["itemimage"].ToString() + "',[isshare] = '" + isexist.Rows[0]["isshare"].ToString() + "',[SyncID] = '" + isexist.Rows[0]["SyncID"].ToString() + "',[SyncDatetime] = '" + isexist.Rows[0]["SyncDatetime"].ToString() + "' WHERE SyncID='" + isexist.Rows[0]["SyncID"].ToString() + "'",con);
                                }
                            }
                        }
                        else
                        {
                            conn.execute("INSERT INTO [dbo].[ProductMaster]([CompanyID],[GroupName],[Product_Name],[Unit],[Altunit],[Convfactor],[Packing],[IsBatch],[Hsn_Sac_Code],[isactive],[isserial],[cessper],[cessamt],[taxslab],[isHotProduct],[itemnumber],[itemimage],[isshare],[SyncID],[SyncDatetime])VALUES('" + serverproductmaster.Rows[i]["CompanyID"].ToString() + "','" + serverproductmaster.Rows[i]["GroupName"].ToString() + "','" + serverproductmaster.Rows[i]["Product_Name"].ToString() + "','" + serverproductmaster.Rows[i]["Unit"].ToString() + "','" + serverproductmaster.Rows[i]["Altunit"].ToString() + "','" + serverproductmaster.Rows[i]["Convfactor"].ToString() + "','" + serverproductmaster.Rows[i]["Packing"].ToString() + "','" + serverproductmaster.Rows[i]["IsBatch"].ToString() + "','" + serverproductmaster.Rows[i]["Hsn_Sac_Code"].ToString() + "','" + serverproductmaster.Rows[i]["isactive"].ToString() + "','" + serverproductmaster.Rows[i]["isserial"].ToString() + "','" + serverproductmaster.Rows[i]["cessper"].ToString() + "','" + serverproductmaster.Rows[i]["cessamt"].ToString() + "','" + serverproductmaster.Rows[i]["taxslab"].ToString() + "','" + serverproductmaster.Rows[i]["isHotProduct"].ToString() + "','" + serverproductmaster.Rows[i]["itemnumber"].ToString() + "','" + serverproductmaster.Rows[i]["itemimage"].ToString() + "','" + serverproductmaster.Rows[i]["isshare"].ToString() + "','" + serverproductmaster.Rows[i]["SyncID"].ToString() + "','" + serverproductmaster.Rows[i]["SyncDatetime"].ToString() + "')");
                        }
                    }
                }
                #endregion

                //ProductPriceMaster
                #region
                DataTable clientproductpricemaster = conn.getdataset("Select * from ProductPriceMaster where SyncDatetime >'" + lastsyncdatetime.ToString("MM/dd/yyyy h:mm:ss") + "'");
                DataTable serverproductpricemaster = conn.getdataset("Select * from ProductPriceMaster where SyncDatetime >'" + lastsyncdatetime.ToString("MM/dd/yyyy h:mm:ss") + "'", con);
                if (clientproductpricemaster.Rows.Count > 0)
                {
                    for (int i = 0; i < clientproductpricemaster.Rows.Count; i++)
                    {
                        DataTable isexist = conn.getdataset("Select * from ProductPriceMaster where SyncID='" + clientproductpricemaster.Rows[i]["SyncID"].ToString() + "'", con);
                        if (isexist.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(isexist.Rows[0]["SyncID"].ToString()))
                            {
                                string local = clientproductpricemaster.Rows[i]["SyncDatetime"].ToString();
                                string Server = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Update in local
                                    conn.execute("UPDATE [dbo].[ProductPriceMaster] SET [Productid] = '" + isexist.Rows[0]["Productid"].ToString() + "',[Batchno] = '" + isexist.Rows[0]["Batchno"].ToString() + "',[BasicPrice] = '" + isexist.Rows[0]["BasicPrice"].ToString() + "',[SalePrice] = '" + isexist.Rows[0]["SalePrice"].ToString() + "',[MRP] = '" + isexist.Rows[0]["MRP"].ToString() + "',[PurchasePrice] = '" + isexist.Rows[0]["PurchasePrice"].ToString() + "',[Barcode] = '" + isexist.Rows[0]["Barcode"].ToString() + "',[OpStock] = '" + isexist.Rows[0]["OpStock"].ToString() + "',[ExpDt] = '" + isexist.Rows[0]["ExpDt"].ToString() + "',[mfgdt] = '" + isexist.Rows[0]["mfgdt"].ToString() + "',[Expdays] = '" + isexist.Rows[0]["Expdays"].ToString() + "',[SelfVal] = '" + isexist.Rows[0]["SelfVal"].ToString() + "',[minsaleprice] = '" + isexist.Rows[0]["minsaleprice"].ToString() + "',[oploose] = '" + isexist.Rows[0]["oploose"].ToString() + "',[opstockval] = '" + isexist.Rows[0]["opstockval"].ToString() + "',[isactive] ='" + isexist.Rows[0]["isactive"].ToString() + "',[SyncID] = '" + isexist.Rows[0]["SyncID"].ToString() + "',[SyncDatetime] = '" + isexist.Rows[0]["SyncDatetime"].ToString() + "',[isshare] = '" + isexist.Rows[0]["isshare"].ToString() + "' WHERE SyncID='" + isexist.Rows[0]["SyncID"].ToString() + "'");
                                }
                                else
                                {
                                    //Update in Server
                                    conn.execute("UPDATE [dbo].[ProductPriceMaster] SET [Productid] = '" + clientproductpricemaster.Rows[i]["Productid"].ToString() + "',[Batchno] = '" + clientproductpricemaster.Rows[i]["Batchno"].ToString() + "',[BasicPrice] = '" + clientproductpricemaster.Rows[i]["BasicPrice"].ToString() + "',[SalePrice] = '" + clientproductpricemaster.Rows[i]["SalePrice"].ToString() + "',[MRP] = '" + clientproductpricemaster.Rows[i]["MRP"].ToString() + "',[PurchasePrice] = '" + clientproductpricemaster.Rows[i]["PurchasePrice"].ToString() + "',[Barcode] = '" + clientproductpricemaster.Rows[i]["Barcode"].ToString() + "',[OpStock] = '" + clientproductpricemaster.Rows[i]["OpStock"].ToString() + "',[ExpDt] = '" + clientproductpricemaster.Rows[i]["ExpDt"].ToString() + "',[mfgdt] = '" + clientproductpricemaster.Rows[i]["mfgdt"].ToString() + "',[Expdays] = '" + clientproductpricemaster.Rows[i]["Expdays"].ToString() + "',[SelfVal] = '" + clientproductpricemaster.Rows[i]["SelfVal"].ToString() + "',[minsaleprice] = '" + clientproductpricemaster.Rows[i]["minsaleprice"].ToString() + "',[oploose] = '" + clientproductpricemaster.Rows[i]["oploose"].ToString() + "',[opstockval] = '" + clientproductpricemaster.Rows[i]["opstockval"].ToString() + "',[isactive] ='" + clientproductpricemaster.Rows[i]["isactive"].ToString() + "',[SyncID] = '" + clientproductpricemaster.Rows[i]["SyncID"].ToString() + "',[SyncDatetime] = '" + clientproductpricemaster.Rows[i]["SyncDatetime"].ToString() + "',[isshare] = '" + clientproductpricemaster.Rows[i]["isshare"].ToString() + "' WHERE SyncID='" + clientproductpricemaster.Rows[i]["SyncID"].ToString() + "'",con);
                                }
                            }
                        }
                        else
                        {
                            conn.execute("INSERT INTO [dbo].[ProductPriceMaster]([Productid],[Batchno],[BasicPrice],[SalePrice],[MRP],[PurchasePrice],[Barcode],[OpStock],[ExpDt],[mfgdt],[Expdays],[SelfVal],[minsaleprice],[oploose],[opstockval],[isactive],[SyncID],[SyncDatetime],[isshare])VALUES('" + clientproductpricemaster.Rows[i]["Productid"].ToString() + "','" + clientproductpricemaster.Rows[i]["Batchno"].ToString() + "','" + clientproductpricemaster.Rows[i]["BasicPrice"].ToString() + "','" + clientproductpricemaster.Rows[i]["SalePrice"].ToString() + "','" + clientproductpricemaster.Rows[i]["MRP"].ToString() + "','" + clientproductpricemaster.Rows[i]["PurchasePrice"].ToString() + "','" + clientproductpricemaster.Rows[i]["Barcode"].ToString() + "','" + clientproductpricemaster.Rows[i]["OpStock"].ToString() + "','" + clientproductpricemaster.Rows[i]["ExpDt"].ToString() + "','" + clientproductpricemaster.Rows[i]["mfgdt"].ToString() + "','" + clientproductpricemaster.Rows[i]["Expdays"].ToString() + "','" + clientproductpricemaster.Rows[i]["SelfVal"].ToString() + "','" + clientproductpricemaster.Rows[i]["minsaleprice"].ToString() + "','" + clientproductpricemaster.Rows[i]["oploose"].ToString() + "','" + clientproductpricemaster.Rows[i]["opstockval"].ToString() + "','" + clientproductpricemaster.Rows[i]["isactive"].ToString() + "','" + clientproductpricemaster.Rows[i]["SyncID"].ToString() + "','" + clientproductpricemaster.Rows[i]["SyncDatetime"].ToString() + "','" + clientproductpricemaster.Rows[i]["isshare"].ToString() + "')", con);
                        }
                    }
                }
                if (serverproductpricemaster.Rows.Count > 0)
                {
                    for (int i = 0; i < serverproductpricemaster.Rows.Count; i++)
                    {
                        DataTable isexist = conn.getdataset("Select * from ProductPriceMaster where SyncID='" + serverproductpricemaster.Rows[i]["SyncID"].ToString() + "'");
                        if (isexist.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(isexist.Rows[0]["SyncID"].ToString()))
                            {
                                string Server = serverproductpricemaster.Rows[i]["SyncDatetime"].ToString();
                                string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Update in local
                                    conn.execute("UPDATE [dbo].[ProductPriceMaster] SET [Productid] = '" + serverproductpricemaster.Rows[i]["Productid"].ToString() + "',[Batchno] = '" + serverproductpricemaster.Rows[i]["Batchno"].ToString() + "',[BasicPrice] = '" + serverproductpricemaster.Rows[i]["BasicPrice"].ToString() + "',[SalePrice] = '" + serverproductpricemaster.Rows[i]["SalePrice"].ToString() + "',[MRP] = '" + serverproductpricemaster.Rows[i]["MRP"].ToString() + "',[PurchasePrice] = '" + serverproductpricemaster.Rows[i]["PurchasePrice"].ToString() + "',[Barcode] = '" + serverproductpricemaster.Rows[i]["Barcode"].ToString() + "',[OpStock] = '" + serverproductpricemaster.Rows[i]["OpStock"].ToString() + "',[ExpDt] = '" + serverproductpricemaster.Rows[i]["ExpDt"].ToString() + "',[mfgdt] = '" + serverproductpricemaster.Rows[i]["mfgdt"].ToString() + "',[Expdays] = '" + serverproductpricemaster.Rows[i]["Expdays"].ToString() + "',[SelfVal] = '" + serverproductpricemaster.Rows[i]["SelfVal"].ToString() + "',[minsaleprice] = '" + serverproductpricemaster.Rows[i]["minsaleprice"].ToString() + "',[oploose] = '" + serverproductpricemaster.Rows[i]["oploose"].ToString() + "',[opstockval] = '" + serverproductpricemaster.Rows[i]["opstockval"].ToString() + "',[isactive] ='" + serverproductpricemaster.Rows[i]["isactive"].ToString() + "',[SyncID] = '" + serverproductpricemaster.Rows[i]["SyncID"].ToString() + "',[SyncDatetime] = '" + serverproductpricemaster.Rows[i]["SyncDatetime"].ToString() + "',[isshare] = '" + serverproductpricemaster.Rows[i]["isshare"].ToString() + "' WHERE SyncID='" + serverproductpricemaster.Rows[i]["SyncID"].ToString() + "'");
                                    
                                }
                                else
                                {
                                    //Update in Server
                                    conn.execute("UPDATE [dbo].[ProductPriceMaster] SET [Productid] = '" + isexist.Rows[0]["Productid"].ToString() + "',[Batchno] = '" + isexist.Rows[0]["Batchno"].ToString() + "',[BasicPrice] = '" + isexist.Rows[0]["BasicPrice"].ToString() + "',[SalePrice] = '" + isexist.Rows[0]["SalePrice"].ToString() + "',[MRP] = '" + isexist.Rows[0]["MRP"].ToString() + "',[PurchasePrice] = '" + isexist.Rows[0]["PurchasePrice"].ToString() + "',[Barcode] = '" + isexist.Rows[0]["Barcode"].ToString() + "',[OpStock] = '" + isexist.Rows[0]["OpStock"].ToString() + "',[ExpDt] = '" + isexist.Rows[0]["ExpDt"].ToString() + "',[mfgdt] = '" + isexist.Rows[0]["mfgdt"].ToString() + "',[Expdays] = '" + isexist.Rows[0]["Expdays"].ToString() + "',[SelfVal] = '" + isexist.Rows[0]["SelfVal"].ToString() + "',[minsaleprice] = '" + isexist.Rows[0]["minsaleprice"].ToString() + "',[oploose] = '" + isexist.Rows[0]["oploose"].ToString() + "',[opstockval] = '" + isexist.Rows[0]["opstockval"].ToString() + "',[isactive] ='" + isexist.Rows[0]["isactive"].ToString() + "',[SyncID] = '" + isexist.Rows[0]["SyncID"].ToString() + "',[SyncDatetime] = '" + isexist.Rows[0]["SyncDatetime"].ToString() + "',[isshare] = '" + isexist.Rows[0]["isshare"].ToString() + "' WHERE SyncID='" + isexist.Rows[0]["SyncID"].ToString() + "'",con);
                                }
                            }
                        }
                        else
                        {
                            conn.execute("INSERT INTO [dbo].[ProductPriceMaster]([Productid],[Batchno],[BasicPrice],[SalePrice],[MRP],[PurchasePrice],[Barcode],[OpStock],[ExpDt],[mfgdt],[Expdays],[SelfVal],[minsaleprice],[oploose],[opstockval],[isactive],[SyncID],[SyncDatetime],[isshare])VALUES('" + serverproductpricemaster.Rows[i]["Productid"].ToString() + "','" + serverproductpricemaster.Rows[i]["Batchno"].ToString() + "','" + serverproductpricemaster.Rows[i]["BasicPrice"].ToString() + "','" + serverproductpricemaster.Rows[i]["SalePrice"].ToString() + "','" + serverproductpricemaster.Rows[i]["MRP"].ToString() + "','" + serverproductpricemaster.Rows[i]["PurchasePrice"].ToString() + "','" + serverproductpricemaster.Rows[i]["Barcode"].ToString() + "','" + serverproductpricemaster.Rows[i]["OpStock"].ToString() + "','" + serverproductpricemaster.Rows[i]["ExpDt"].ToString() + "','" + serverproductpricemaster.Rows[i]["mfgdt"].ToString() + "','" + serverproductpricemaster.Rows[i]["Expdays"].ToString() + "','" + serverproductpricemaster.Rows[i]["SelfVal"].ToString() + "','" + serverproductpricemaster.Rows[i]["minsaleprice"].ToString() + "','" + serverproductpricemaster.Rows[i]["oploose"].ToString() + "','" + serverproductpricemaster.Rows[i]["opstockval"].ToString() + "','" + serverproductpricemaster.Rows[i]["isactive"].ToString() + "','" + serverproductpricemaster.Rows[i]["SyncID"].ToString() + "','" + serverproductpricemaster.Rows[i]["SyncDatetime"].ToString() + "','" + serverproductpricemaster.Rows[i]["isshare"].ToString() + "')");
                        }
                    }
                }
                #endregion
                //Bill Master
                #region
                DataTable clientbillmaster = conn.getdataset("Select * from BillMaster where SyncDatetime >'" + lastsyncdatetime.ToString("MM/dd/yyyy h:mm:ss") + "'");
                DataTable serverbillmaster = conn.getdataset("Select * from BillMaster where SyncDatetime >'" + lastsyncdatetime.ToString("MM/dd/yyyy h:mm:ss") + "'", con);
                if (clientbillmaster.Rows.Count > 0)
                {
                    for (int i = 0; i < clientbillmaster.Rows.Count; i++)
                    {
                        DataTable isexist = conn.getdataset("Select * from BillMaster where SyncID='" + clientbillmaster.Rows[i]["SyncID"].ToString() + "'", con);
                        if (isexist.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(isexist.Rows[0]["SyncID"].ToString()))
                            {
                                string local = clientbillmaster.Rows[i]["SyncDatetime"].ToString();
                                string Server = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Update in local
                                    conn.execute("UPDATE [dbo].[BillMaster] SET [Bill_No] ='" + isexist.Rows[0]["Bill_No"].ToString() + "',[Bill_Date] = '" + isexist.Rows[0]["Bill_Date"].ToString() + "',[Terms] = '" + isexist.Rows[0]["Terms"].ToString() + "',[ClientID] = '" + isexist.Rows[0]["ClientID"].ToString() + "',[PO_No] = '" + isexist.Rows[0]["PO_No"].ToString() + "',[SaleType] = '" + isexist.Rows[0]["SaleType"].ToString() + "',[count] = '" + isexist.Rows[0]["count"].ToString() + "',[totalqty] = '" + isexist.Rows[0]["totalqty"].ToString() + "',[totalbasic] = '" + isexist.Rows[0]["totalbasic"].ToString() + "',[totaltax] = '" + isexist.Rows[0]["totaltax"].ToString() + "',[totalnet] = '" + isexist.Rows[0]["totalnet"].ToString() + "',[isactive] = '" + isexist.Rows[0]["isactive"].ToString() + "',[apprweight] = '" + isexist.Rows[0]["apprweight"].ToString() + "',[dispatchdetails] = '" + isexist.Rows[0]["dispatchdetails"].ToString() + "',[remarks] = '" + isexist.Rows[0]["remarks"].ToString() + "',[BillType] = '" + isexist.Rows[0]["BillType"].ToString() + "',[billno] = '" + isexist.Rows[0]["billno"].ToString() + "',[totaladdtax] = '" + isexist.Rows[0]["totaladdtax"].ToString() + "',[roudoff] = '" + isexist.Rows[0]["roudoff"].ToString() + "',[Duedate] = '" + isexist.Rows[0]["Duedate"].ToString() + "',[totalaqty] = '" + isexist.Rows[0]["totalaqty"].ToString() + "',[totalfree] = '" + isexist.Rows[0]["totalfree"].ToString() + "',[totaldiscount] = '" + isexist.Rows[0]["totaldiscount"].ToString() + "',[totaladddiscount] = '" + isexist.Rows[0]["totaladddiscount"].ToString() + "',[totalamount] = '" + isexist.Rows[0]["totalamount"].ToString() + "',[totalservicejob] = '" + isexist.Rows[0]["totalservicejob"].ToString() + "',[totalcharges] = '" + isexist.Rows[0]["totalcharges"].ToString() + "',[Delieveryat] = '" + isexist.Rows[0]["Delieveryat"].ToString() + "',[fraight] = '" + isexist.Rows[0]["fraight"].ToString() + "',[vehicleno] = '" + isexist.Rows[0]["vehicleno"].ToString() + "',[grrrno] = '" + isexist.Rows[0]["grrrno"].ToString() + "',[noofskids] = '" + isexist.Rows[0]["noofskids"].ToString() + "',[sgstamt] = '" + isexist.Rows[0]["sgstamt"].ToString() + "',[cgatamt] = '" + isexist.Rows[0]["cgatamt"].ToString() + "',[igstamt] = '" + isexist.Rows[0]["igstamt"].ToString() + "',[OrderStatus] = '" + isexist.Rows[0]["OrderStatus"].ToString() + "',[refno] = '" + isexist.Rows[0]["refno"].ToString() + "',[totalcess] = '" + isexist.Rows[0]["totalcess"].ToString() + "',[agentID] = '" + isexist.Rows[0]["agentID"].ToString() + "',[originalbillno] = '" + isexist.Rows[0]["originalbillno"].ToString() + "',[originalbilldate] = '" + isexist.Rows[0]["originalbilldate"].ToString() + "',[SyncID] = '" + isexist.Rows[0]["SyncID"].ToString() + "',[SyncDatetime] = '" + isexist.Rows[0]["SyncDatetime"].ToString() + "' WHERE SyncID='" + isexist.Rows[0]["SyncID"].ToString() + "'");
                                }
                                else
                                {
                                    //Update in Server
                                    conn.execute("UPDATE [dbo].[BillMaster] SET [Bill_No] ='" + clientbillmaster.Rows[i]["Bill_No"].ToString() + "',[Bill_Date] = '" + clientbillmaster.Rows[i]["Bill_Date"].ToString() + "',[Terms] = '" + clientbillmaster.Rows[i]["Terms"].ToString() + "',[ClientID] = '" + clientbillmaster.Rows[i]["ClientID"].ToString() + "',[PO_No] = '" + clientbillmaster.Rows[i]["PO_No"].ToString() + "',[SaleType] = '" + clientbillmaster.Rows[i]["SaleType"].ToString() + "',[count] = '" + clientbillmaster.Rows[i]["count"].ToString() + "',[totalqty] = '" + clientbillmaster.Rows[i]["totalqty"].ToString() + "',[totalbasic] = '" + clientbillmaster.Rows[i]["totalbasic"].ToString() + "',[totaltax] = '" + clientbillmaster.Rows[i]["totaltax"].ToString() + "',[totalnet] = '" + clientbillmaster.Rows[i]["totalnet"].ToString() + "',[isactive] = '" + clientbillmaster.Rows[i]["isactive"].ToString() + "',[apprweight] = '" + clientbillmaster.Rows[i]["apprweight"].ToString() + "',[dispatchdetails] = '" + clientbillmaster.Rows[i]["dispatchdetails"].ToString() + "',[remarks] = '" + clientbillmaster.Rows[i]["remarks"].ToString() + "',[BillType] = '" + clientbillmaster.Rows[i]["BillType"].ToString() + "',[billno] = '" + clientbillmaster.Rows[i]["billno"].ToString() + "',[totaladdtax] = '" + clientbillmaster.Rows[i]["totaladdtax"].ToString() + "',[roudoff] = '" + clientbillmaster.Rows[i]["roudoff"].ToString() + "',[Duedate] = '" + clientbillmaster.Rows[i]["Duedate"].ToString() + "',[totalaqty] = '" + clientbillmaster.Rows[i]["totalaqty"].ToString() + "',[totalfree] = '" + clientbillmaster.Rows[i]["totalfree"].ToString() + "',[totaldiscount] = '" + clientbillmaster.Rows[i]["totaldiscount"].ToString() + "',[totaladddiscount] = '" + clientbillmaster.Rows[i]["totaladddiscount"].ToString() + "',[totalamount] = '" + clientbillmaster.Rows[i]["totalamount"].ToString() + "',[totalservicejob] = '" + clientbillmaster.Rows[i]["totalservicejob"].ToString() + "',[totalcharges] = '" + clientbillmaster.Rows[i]["totalcharges"].ToString() + "',[Delieveryat] = '" + clientbillmaster.Rows[i]["Delieveryat"].ToString() + "',[fraight] = '" + clientbillmaster.Rows[i]["fraight"].ToString() + "',[vehicleno] = '" + clientbillmaster.Rows[i]["vehicleno"].ToString() + "',[grrrno] = '" + clientbillmaster.Rows[i]["grrrno"].ToString() + "',[noofskids] = '" + clientbillmaster.Rows[i]["noofskids"].ToString() + "',[sgstamt] = '" + clientbillmaster.Rows[i]["sgstamt"].ToString() + "',[cgatamt] = '" + clientbillmaster.Rows[i]["cgatamt"].ToString() + "',[igstamt] = '" + clientbillmaster.Rows[i]["igstamt"].ToString() + "',[OrderStatus] = '" + clientbillmaster.Rows[i]["OrderStatus"].ToString() + "',[refno] = '" + clientbillmaster.Rows[i]["refno"].ToString() + "',[totalcess] = '" + clientbillmaster.Rows[i]["totalcess"].ToString() + "',[agentID] = '" + clientbillmaster.Rows[i]["agentID"].ToString() + "',[originalbillno] = '" + clientbillmaster.Rows[i]["originalbillno"].ToString() + "',[originalbilldate] = '" + clientbillmaster.Rows[i]["originalbilldate"].ToString() + "',[SyncID] = '" + clientbillmaster.Rows[i]["SyncID"].ToString() + "',[SyncDatetime] = '" + clientbillmaster.Rows[i]["SyncDatetime"].ToString() + "' WHERE SyncID='" + clientbillmaster.Rows[i]["SyncID"].ToString() + "'", con);
                                }
                            }
                            else
                            {
                                string local = clientbillmaster.Rows[i]["SyncDatetime"].ToString();
                                string Server = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Insert in local
                                    conn.execute("INSERT INTO [dbo].[BillMaster]([Bill_No],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[BillType],[billno],[totaladdtax],[roudoff],[Duedate],[totalaqty],[totalfree],[totaldiscount],[totaladddiscount],[totalamount],[totalservicejob],[totalcharges],[Delieveryat],[fraight],[vehicleno],[grrrno],[noofskids],[sgstamt],[cgatamt],[igstamt],[OrderStatus],[refno],[totalcess],[agentID],[originalbillno],[originalbilldate],[SyncID],[SyncDatetime])VALUES('" + isexist.Rows[0]["Bill_No"].ToString() + "','" + isexist.Rows[0]["Bill_Date"].ToString() + "','" + isexist.Rows[0]["Terms"].ToString() + "','" + isexist.Rows[0]["ClientID"].ToString() + "','" + isexist.Rows[0]["PO_No"].ToString() + "','" + isexist.Rows[0]["SaleType"].ToString() + "','" + isexist.Rows[0]["count"].ToString() + "','" + isexist.Rows[0]["totalqty"].ToString() + "','" + isexist.Rows[0]["totalbasic"].ToString() + "','" + isexist.Rows[0]["totaltax"].ToString() + "','" + isexist.Rows[0]["totalnet"].ToString() + "','" + isexist.Rows[0]["isactive"].ToString() + "','" + isexist.Rows[0]["apprweight"].ToString() + "','" + isexist.Rows[0]["dispatchdetails"].ToString() + "','" + isexist.Rows[0]["remarks"].ToString() + "','" + isexist.Rows[0]["BillType"].ToString() + "','" + isexist.Rows[0]["billno"].ToString() + "','" + isexist.Rows[0]["totaladdtax"].ToString() + "','" + isexist.Rows[0]["roudoff"].ToString() + "','" + isexist.Rows[0]["Duedate"].ToString() + "','" + isexist.Rows[0]["totalaqty"].ToString() + "','" + isexist.Rows[0]["totalfree"].ToString() + "','" + isexist.Rows[0]["totaldiscount"].ToString() + "','" + isexist.Rows[0]["totaladddiscount"].ToString() + "','" + isexist.Rows[0]["totalamount"].ToString() + "','" + isexist.Rows[0]["totalservicejob"].ToString() + "','" + isexist.Rows[0]["totalcharges"].ToString() + "','" + isexist.Rows[0]["Delieveryat"].ToString() + "','" + isexist.Rows[0]["fraight"].ToString() + "','" + isexist.Rows[0]["vehicleno"].ToString() + "','" + isexist.Rows[0]["grrrno"].ToString() + "','" + isexist.Rows[0]["noofskids"].ToString() + "','" + isexist.Rows[0]["sgstamt"].ToString() + "','" + isexist.Rows[0]["cgatamt"].ToString() + "','" + isexist.Rows[0]["igstamt"].ToString() + "','" + isexist.Rows[0]["OrderStatus"].ToString() + "','" + isexist.Rows[0]["refno"].ToString() + "','" + isexist.Rows[0]["totalcess"].ToString() + "','" + isexist.Rows[0]["agentID"].ToString() + "','" + isexist.Rows[0]["originalbillno"].ToString() + "','" + isexist.Rows[0]["originalbilldate"].ToString() + "','" + isexist.Rows[0]["SyncID"].ToString() + "','" + isexist.Rows[0]["SyncDatetime"].ToString() + "')");

                                }
                                else
                                {
                                    //Insert in Server
                                    conn.execute("INSERT INTO [dbo].[BillMaster]([Bill_No],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[BillType],[billno],[totaladdtax],[roudoff],[Duedate],[totalaqty],[totalfree],[totaldiscount],[totaladddiscount],[totalamount],[totalservicejob],[totalcharges],[Delieveryat],[fraight],[vehicleno],[grrrno],[noofskids],[sgstamt],[cgatamt],[igstamt],[OrderStatus],[refno],[totalcess],[agentID],[originalbillno],[originalbilldate],[SyncID],[SyncDatetime])VALUES('" + clientbillmaster.Rows[i]["Bill_No"].ToString() + "','" + clientbillmaster.Rows[i]["Bill_Date"].ToString() + "','" + clientbillmaster.Rows[i]["Terms"].ToString() + "','" + clientbillmaster.Rows[i]["ClientID"].ToString() + "','" + clientbillmaster.Rows[i]["PO_No"].ToString() + "','" + clientbillmaster.Rows[i]["SaleType"].ToString() + "','" + clientbillmaster.Rows[i]["count"].ToString() + "','" + clientbillmaster.Rows[i]["totalqty"].ToString() + "','" + clientbillmaster.Rows[i]["totalbasic"].ToString() + "','" + clientbillmaster.Rows[i]["totaltax"].ToString() + "','" + clientbillmaster.Rows[i]["totalnet"].ToString() + "','" + clientbillmaster.Rows[i]["isactive"].ToString() + "','" + clientbillmaster.Rows[i]["apprweight"].ToString() + "','" + clientbillmaster.Rows[i]["dispatchdetails"].ToString() + "','" + clientbillmaster.Rows[i]["remarks"].ToString() + "','" + clientbillmaster.Rows[i]["BillType"].ToString() + "','" + clientbillmaster.Rows[i]["billno"].ToString() + "','" + clientbillmaster.Rows[i]["totaladdtax"].ToString() + "','" + clientbillmaster.Rows[i]["roudoff"].ToString() + "','" + clientbillmaster.Rows[i]["Duedate"].ToString() + "','" + clientbillmaster.Rows[i]["totalaqty"].ToString() + "','" + clientbillmaster.Rows[i]["totalfree"].ToString() + "','" + clientbillmaster.Rows[i]["totaldiscount"].ToString() + "','" + clientbillmaster.Rows[i]["totaladddiscount"].ToString() + "','" + clientbillmaster.Rows[i]["totalamount"].ToString() + "','" + clientbillmaster.Rows[i]["totalservicejob"].ToString() + "','" + clientbillmaster.Rows[i]["totalcharges"].ToString() + "','" + clientbillmaster.Rows[i]["Delieveryat"].ToString() + "','" + clientbillmaster.Rows[i]["fraight"].ToString() + "','" + clientbillmaster.Rows[i]["vehicleno"].ToString() + "','" + clientbillmaster.Rows[i]["grrrno"].ToString() + "','" + clientbillmaster.Rows[i]["noofskids"].ToString() + "','" + clientbillmaster.Rows[i]["sgstamt"].ToString() + "','" + clientbillmaster.Rows[i]["cgatamt"].ToString() + "','" + clientbillmaster.Rows[i]["igstamt"].ToString() + "','" + clientbillmaster.Rows[i]["OrderStatus"].ToString() + "','" + clientbillmaster.Rows[i]["refno"].ToString() + "','" + clientbillmaster.Rows[i]["totalcess"].ToString() + "','" + clientbillmaster.Rows[i]["agentID"].ToString() + "','" + clientbillmaster.Rows[i]["originalbillno"].ToString() + "','" + clientbillmaster.Rows[i]["originalbilldate"].ToString() + "','" + clientbillmaster.Rows[i]["SyncID"].ToString() + "','" + clientbillmaster.Rows[i]["SyncDatetime"].ToString() + "')", con);
                                }
                            }
                        }
                        else
                        {
                            conn.execute("INSERT INTO [dbo].[BillMaster]([Bill_No],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[BillType],[billno],[totaladdtax],[roudoff],[Duedate],[totalaqty],[totalfree],[totaldiscount],[totaladddiscount],[totalamount],[totalservicejob],[totalcharges],[Delieveryat],[fraight],[vehicleno],[grrrno],[noofskids],[sgstamt],[cgatamt],[igstamt],[OrderStatus],[refno],[totalcess],[agentID],[originalbillno],[originalbilldate],[SyncID],[SyncDatetime])VALUES('" + clientbillmaster.Rows[i]["Bill_No"].ToString() + "','" + clientbillmaster.Rows[i]["Bill_Date"].ToString() + "','" + clientbillmaster.Rows[i]["Terms"].ToString() + "','" + clientbillmaster.Rows[i]["ClientID"].ToString() + "','" + clientbillmaster.Rows[i]["PO_No"].ToString() + "','" + clientbillmaster.Rows[i]["SaleType"].ToString() + "','" + clientbillmaster.Rows[i]["count"].ToString() + "','" + clientbillmaster.Rows[i]["totalqty"].ToString() + "','" + clientbillmaster.Rows[i]["totalbasic"].ToString() + "','" + clientbillmaster.Rows[i]["totaltax"].ToString() + "','" + clientbillmaster.Rows[i]["totalnet"].ToString() + "','" + clientbillmaster.Rows[i]["isactive"].ToString() + "','" + clientbillmaster.Rows[i]["apprweight"].ToString() + "','" + clientbillmaster.Rows[i]["dispatchdetails"].ToString() + "','" + clientbillmaster.Rows[i]["remarks"].ToString() + "','" + clientbillmaster.Rows[i]["BillType"].ToString() + "','" + clientbillmaster.Rows[i]["billno"].ToString() + "','" + clientbillmaster.Rows[i]["totaladdtax"].ToString() + "','" + clientbillmaster.Rows[i]["roudoff"].ToString() + "','" + clientbillmaster.Rows[i]["Duedate"].ToString() + "','" + clientbillmaster.Rows[i]["totalaqty"].ToString() + "','" + clientbillmaster.Rows[i]["totalfree"].ToString() + "','" + clientbillmaster.Rows[i]["totaldiscount"].ToString() + "','" + clientbillmaster.Rows[i]["totaladddiscount"].ToString() + "','" + clientbillmaster.Rows[i]["totalamount"].ToString() + "','" + clientbillmaster.Rows[i]["totalservicejob"].ToString() + "','" + clientbillmaster.Rows[i]["totalcharges"].ToString() + "','" + clientbillmaster.Rows[i]["Delieveryat"].ToString() + "','" + clientbillmaster.Rows[i]["fraight"].ToString() + "','" + clientbillmaster.Rows[i]["vehicleno"].ToString() + "','" + clientbillmaster.Rows[i]["grrrno"].ToString() + "','" + clientbillmaster.Rows[i]["noofskids"].ToString() + "','" + clientbillmaster.Rows[i]["sgstamt"].ToString() + "','" + clientbillmaster.Rows[i]["cgatamt"].ToString() + "','" + clientbillmaster.Rows[i]["igstamt"].ToString() + "','" + clientbillmaster.Rows[i]["OrderStatus"].ToString() + "','" + clientbillmaster.Rows[i]["refno"].ToString() + "','" + clientbillmaster.Rows[i]["totalcess"].ToString() + "','" + clientbillmaster.Rows[i]["agentID"].ToString() + "','" + clientbillmaster.Rows[i]["originalbillno"].ToString() + "','" + clientbillmaster.Rows[i]["originalbilldate"].ToString() + "','" + clientbillmaster.Rows[i]["SyncID"].ToString() + "','" + clientbillmaster.Rows[i]["SyncDatetime"].ToString() + "')", con);
                        }
                    }
                }
                if (serverbillmaster.Rows.Count > 0)
                {
                    for (int i = 0; i < serverbillmaster.Rows.Count; i++)
                    {
                        DataTable isexist = conn.getdataset("Select * from BillMaster where SyncID='" + serverbillmaster.Rows[i]["SyncID"].ToString() + "'");
                        if (isexist.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(isexist.Rows[0]["SyncID"].ToString()))
                            {
                                string Server = serverbillmaster.Rows[i]["SyncDatetime"].ToString();
                                string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Update in local

                                    conn.execute("UPDATE [dbo].[BillMaster] SET [Bill_No] ='" + serverbillmaster.Rows[i]["Bill_No"].ToString() + "',[Bill_Date] = '" + serverbillmaster.Rows[i]["Bill_Date"].ToString() + "',[Terms] = '" + serverbillmaster.Rows[i]["Terms"].ToString() + "',[ClientID] = '" + serverbillmaster.Rows[i]["ClientID"].ToString() + "',[PO_No] = '" + serverbillmaster.Rows[i]["PO_No"].ToString() + "',[SaleType] = '" + serverbillmaster.Rows[i]["SaleType"].ToString() + "',[count] = '" + serverbillmaster.Rows[i]["count"].ToString() + "',[totalqty] = '" + serverbillmaster.Rows[i]["totalqty"].ToString() + "',[totalbasic] = '" + serverbillmaster.Rows[i]["totalbasic"].ToString() + "',[totaltax] = '" + serverbillmaster.Rows[i]["totaltax"].ToString() + "',[totalnet] = '" + serverbillmaster.Rows[i]["totalnet"].ToString() + "',[isactive] = '" + serverbillmaster.Rows[i]["isactive"].ToString() + "',[apprweight] = '" + serverbillmaster.Rows[i]["apprweight"].ToString() + "',[dispatchdetails] = '" + serverbillmaster.Rows[i]["dispatchdetails"].ToString() + "',[remarks] = '" + serverbillmaster.Rows[i]["remarks"].ToString() + "',[BillType] = '" + serverbillmaster.Rows[i]["BillType"].ToString() + "',[billno] = '" + serverbillmaster.Rows[i]["billno"].ToString() + "',[totaladdtax] = '" + serverbillmaster.Rows[i]["totaladdtax"].ToString() + "',[roudoff] = '" + serverbillmaster.Rows[i]["roudoff"].ToString() + "',[Duedate] = '" + serverbillmaster.Rows[i]["Duedate"].ToString() + "',[totalaqty] = '" + serverbillmaster.Rows[i]["totalaqty"].ToString() + "',[totalfree] = '" + serverbillmaster.Rows[i]["totalfree"].ToString() + "',[totaldiscount] = '" + serverbillmaster.Rows[i]["totaldiscount"].ToString() + "',[totaladddiscount] = '" + serverbillmaster.Rows[i]["totaladddiscount"].ToString() + "',[totalamount] = '" + serverbillmaster.Rows[i]["totalamount"].ToString() + "',[totalservicejob] = '" + serverbillmaster.Rows[i]["totalservicejob"].ToString() + "',[totalcharges] = '" + serverbillmaster.Rows[i]["totalcharges"].ToString() + "',[Delieveryat] = '" + serverbillmaster.Rows[i]["Delieveryat"].ToString() + "',[fraight] = '" + serverbillmaster.Rows[i]["fraight"].ToString() + "',[vehicleno] = '" + serverbillmaster.Rows[i]["vehicleno"].ToString() + "',[grrrno] = '" + serverbillmaster.Rows[i]["grrrno"].ToString() + "',[noofskids] = '" + serverbillmaster.Rows[i]["noofskids"].ToString() + "',[sgstamt] = '" + serverbillmaster.Rows[i]["sgstamt"].ToString() + "',[cgatamt] = '" + serverbillmaster.Rows[i]["cgatamt"].ToString() + "',[igstamt] = '" + serverbillmaster.Rows[i]["igstamt"].ToString() + "',[OrderStatus] = '" + serverbillmaster.Rows[i]["OrderStatus"].ToString() + "',[refno] = '" + serverbillmaster.Rows[i]["refno"].ToString() + "',[totalcess] = '" + serverbillmaster.Rows[i]["totalcess"].ToString() + "',[agentID] = '" + serverbillmaster.Rows[i]["agentID"].ToString() + "',[originalbillno] = '" + serverbillmaster.Rows[i]["originalbillno"].ToString() + "',[originalbilldate] = '" + serverbillmaster.Rows[i]["originalbilldate"].ToString() + "',[SyncID] = '" + serverbillmaster.Rows[i]["SyncID"].ToString() + "',[SyncDatetime] = '" + serverbillmaster.Rows[i]["SyncDatetime"].ToString() + "' WHERE SyncID='" + serverbillmaster.Rows[i]["SyncID"].ToString() + "'");
                                }
                                else
                                {
                                    //Update in Server
                                    conn.execute("UPDATE [dbo].[BillMaster] SET [Bill_No] ='" + isexist.Rows[0]["Bill_No"].ToString() + "',[Bill_Date] = '" + isexist.Rows[0]["Bill_Date"].ToString() + "',[Terms] = '" + isexist.Rows[0]["Terms"].ToString() + "',[ClientID] = '" + isexist.Rows[0]["ClientID"].ToString() + "',[PO_No] = '" + isexist.Rows[0]["PO_No"].ToString() + "',[SaleType] = '" + isexist.Rows[0]["SaleType"].ToString() + "',[count] = '" + isexist.Rows[0]["count"].ToString() + "',[totalqty] = '" + isexist.Rows[0]["totalqty"].ToString() + "',[totalbasic] = '" + isexist.Rows[0]["totalbasic"].ToString() + "',[totaltax] = '" + isexist.Rows[0]["totaltax"].ToString() + "',[totalnet] = '" + isexist.Rows[0]["totalnet"].ToString() + "',[isactive] = '" + isexist.Rows[0]["isactive"].ToString() + "',[apprweight] = '" + isexist.Rows[0]["apprweight"].ToString() + "',[dispatchdetails] = '" + isexist.Rows[0]["dispatchdetails"].ToString() + "',[remarks] = '" + isexist.Rows[0]["remarks"].ToString() + "',[BillType] = '" + isexist.Rows[0]["BillType"].ToString() + "',[billno] = '" + isexist.Rows[0]["billno"].ToString() + "',[totaladdtax] = '" + isexist.Rows[0]["totaladdtax"].ToString() + "',[roudoff] = '" + isexist.Rows[0]["roudoff"].ToString() + "',[Duedate] = '" + isexist.Rows[0]["Duedate"].ToString() + "',[totalaqty] = '" + isexist.Rows[0]["totalaqty"].ToString() + "',[totalfree] = '" + isexist.Rows[0]["totalfree"].ToString() + "',[totaldiscount] = '" + isexist.Rows[0]["totaldiscount"].ToString() + "',[totaladddiscount] = '" + isexist.Rows[0]["totaladddiscount"].ToString() + "',[totalamount] = '" + isexist.Rows[0]["totalamount"].ToString() + "',[totalservicejob] = '" + isexist.Rows[0]["totalservicejob"].ToString() + "',[totalcharges] = '" + isexist.Rows[0]["totalcharges"].ToString() + "',[Delieveryat] = '" + isexist.Rows[0]["Delieveryat"].ToString() + "',[fraight] = '" + isexist.Rows[0]["fraight"].ToString() + "',[vehicleno] = '" + isexist.Rows[0]["vehicleno"].ToString() + "',[grrrno] = '" + isexist.Rows[0]["grrrno"].ToString() + "',[noofskids] = '" + isexist.Rows[0]["noofskids"].ToString() + "',[sgstamt] = '" + isexist.Rows[0]["sgstamt"].ToString() + "',[cgatamt] = '" + isexist.Rows[0]["cgatamt"].ToString() + "',[igstamt] = '" + isexist.Rows[0]["igstamt"].ToString() + "',[OrderStatus] = '" + isexist.Rows[0]["OrderStatus"].ToString() + "',[refno] = '" + isexist.Rows[0]["refno"].ToString() + "',[totalcess] = '" + isexist.Rows[0]["totalcess"].ToString() + "',[agentID] = '" + isexist.Rows[0]["agentID"].ToString() + "',[originalbillno] = '" + isexist.Rows[0]["originalbillno"].ToString() + "',[originalbilldate] = '" + isexist.Rows[0]["originalbilldate"].ToString() + "',[SyncID] = '" + isexist.Rows[0]["SyncID"].ToString() + "',[SyncDatetime] = '" + isexist.Rows[0]["SyncDatetime"].ToString() + "' WHERE SyncID='" + isexist.Rows[0]["SyncID"].ToString() + "'", con);
                                }
                            }
                            else
                            {
                                string Server = serverbillmaster.Rows[i]["SyncDatetime"].ToString();
                                string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = DateTime.ParseExact(local, Master.dateformate, en);
                                DateTime serversyncdatetime = DateTime.ParseExact(Server, Master.dateformate, en);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Insert in local

                                    conn.execute("INSERT INTO [dbo].[BillMaster]([Bill_No],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[BillType],[billno],[totaladdtax],[roudoff],[Duedate],[totalaqty],[totalfree],[totaldiscount],[totaladddiscount],[totalamount],[totalservicejob],[totalcharges],[Delieveryat],[fraight],[vehicleno],[grrrno],[noofskids],[sgstamt],[cgatamt],[igstamt],[OrderStatus],[refno],[totalcess],[agentID],[originalbillno],[originalbilldate],[SyncID],[SyncDatetime])VALUES('" + serverbillmaster.Rows[i]["Bill_No"].ToString() + "','" + serverbillmaster.Rows[i]["Bill_Date"].ToString() + "','" + serverbillmaster.Rows[i]["Terms"].ToString() + "','" + serverbillmaster.Rows[i]["ClientID"].ToString() + "','" + serverbillmaster.Rows[i]["PO_No"].ToString() + "','" + serverbillmaster.Rows[i]["SaleType"].ToString() + "','" + serverbillmaster.Rows[i]["count"].ToString() + "','" + serverbillmaster.Rows[i]["totalqty"].ToString() + "','" + serverbillmaster.Rows[i]["totalbasic"].ToString() + "','" + serverbillmaster.Rows[i]["totaltax"].ToString() + "','" + serverbillmaster.Rows[i]["totalnet"].ToString() + "','" + serverbillmaster.Rows[i]["isactive"].ToString() + "','" + serverbillmaster.Rows[i]["apprweight"].ToString() + "','" + serverbillmaster.Rows[i]["dispatchdetails"].ToString() + "','" + serverbillmaster.Rows[i]["remarks"].ToString() + "','" + serverbillmaster.Rows[i]["BillType"].ToString() + "','" + serverbillmaster.Rows[i]["billno"].ToString() + "','" + serverbillmaster.Rows[i]["totaladdtax"].ToString() + "','" + serverbillmaster.Rows[i]["roudoff"].ToString() + "','" + serverbillmaster.Rows[i]["Duedate"].ToString() + "','" + serverbillmaster.Rows[i]["totalaqty"].ToString() + "','" + serverbillmaster.Rows[i]["totalfree"].ToString() + "','" + serverbillmaster.Rows[i]["totaldiscount"].ToString() + "','" + serverbillmaster.Rows[i]["totaladddiscount"].ToString() + "','" + serverbillmaster.Rows[i]["totalamount"].ToString() + "','" + serverbillmaster.Rows[i]["totalservicejob"].ToString() + "','" + serverbillmaster.Rows[i]["totalcharges"].ToString() + "','" + serverbillmaster.Rows[i]["Delieveryat"].ToString() + "','" + serverbillmaster.Rows[i]["fraight"].ToString() + "','" + serverbillmaster.Rows[i]["vehicleno"].ToString() + "','" + serverbillmaster.Rows[i]["grrrno"].ToString() + "','" + serverbillmaster.Rows[i]["noofskids"].ToString() + "','" + serverbillmaster.Rows[i]["sgstamt"].ToString() + "','" + serverbillmaster.Rows[i]["cgatamt"].ToString() + "','" + serverbillmaster.Rows[i]["igstamt"].ToString() + "','" + serverbillmaster.Rows[i]["OrderStatus"].ToString() + "','" + serverbillmaster.Rows[i]["refno"].ToString() + "','" + serverbillmaster.Rows[i]["totalcess"].ToString() + "','" + serverbillmaster.Rows[i]["agentID"].ToString() + "','" + serverbillmaster.Rows[i]["originalbillno"].ToString() + "','" + serverbillmaster.Rows[i]["originalbilldate"].ToString() + "','" + serverbillmaster.Rows[i]["SyncID"].ToString() + "','" + serverbillmaster.Rows[i]["SyncDatetime"].ToString() + "')");
                                }
                                else
                                {
                                    //Insert in Server
                                    conn.execute("INSERT INTO [dbo].[BillMaster]([Bill_No],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[BillType],[billno],[totaladdtax],[roudoff],[Duedate],[totalaqty],[totalfree],[totaldiscount],[totaladddiscount],[totalamount],[totalservicejob],[totalcharges],[Delieveryat],[fraight],[vehicleno],[grrrno],[noofskids],[sgstamt],[cgatamt],[igstamt],[OrderStatus],[refno],[totalcess],[agentID],[originalbillno],[originalbilldate],[SyncID],[SyncDatetime])VALUES('" + isexist.Rows[0]["Bill_No"].ToString() + "','" + isexist.Rows[0]["Bill_Date"].ToString() + "','" + isexist.Rows[0]["Terms"].ToString() + "','" + isexist.Rows[0]["ClientID"].ToString() + "','" + isexist.Rows[0]["PO_No"].ToString() + "','" + isexist.Rows[0]["SaleType"].ToString() + "','" + isexist.Rows[0]["count"].ToString() + "','" + isexist.Rows[0]["totalqty"].ToString() + "','" + isexist.Rows[0]["totalbasic"].ToString() + "','" + isexist.Rows[0]["totaltax"].ToString() + "','" + isexist.Rows[0]["totalnet"].ToString() + "','" + isexist.Rows[0]["isactive"].ToString() + "','" + isexist.Rows[0]["apprweight"].ToString() + "','" + isexist.Rows[0]["dispatchdetails"].ToString() + "','" + isexist.Rows[0]["remarks"].ToString() + "','" + isexist.Rows[0]["BillType"].ToString() + "','" + isexist.Rows[0]["billno"].ToString() + "','" + isexist.Rows[0]["totaladdtax"].ToString() + "','" + isexist.Rows[0]["roudoff"].ToString() + "','" + isexist.Rows[0]["Duedate"].ToString() + "','" + isexist.Rows[0]["totalaqty"].ToString() + "','" + isexist.Rows[0]["totalfree"].ToString() + "','" + isexist.Rows[0]["totaldiscount"].ToString() + "','" + isexist.Rows[0]["totaladddiscount"].ToString() + "','" + isexist.Rows[0]["totalamount"].ToString() + "','" + isexist.Rows[0]["totalservicejob"].ToString() + "','" + isexist.Rows[0]["totalcharges"].ToString() + "','" + isexist.Rows[0]["Delieveryat"].ToString() + "','" + isexist.Rows[0]["fraight"].ToString() + "','" + isexist.Rows[0]["vehicleno"].ToString() + "','" + isexist.Rows[0]["grrrno"].ToString() + "','" + isexist.Rows[0]["noofskids"].ToString() + "','" + isexist.Rows[0]["sgstamt"].ToString() + "','" + isexist.Rows[0]["cgatamt"].ToString() + "','" + isexist.Rows[0]["igstamt"].ToString() + "','" + isexist.Rows[0]["OrderStatus"].ToString() + "','" + isexist.Rows[0]["refno"].ToString() + "','" + isexist.Rows[0]["totalcess"].ToString() + "','" + isexist.Rows[0]["agentID"].ToString() + "','" + isexist.Rows[0]["originalbillno"].ToString() + "','" + isexist.Rows[0]["originalbilldate"].ToString() + "','" + isexist.Rows[0]["SyncID"].ToString() + "','" + isexist.Rows[0]["SyncDatetime"].ToString() + "')", con);
                                }
                            }
                        }
                        else
                        {
                            conn.execute("INSERT INTO [dbo].[BillMaster]([Bill_No],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[BillType],[billno],[totaladdtax],[roudoff],[Duedate],[totalaqty],[totalfree],[totaldiscount],[totaladddiscount],[totalamount],[totalservicejob],[totalcharges],[Delieveryat],[fraight],[vehicleno],[grrrno],[noofskids],[sgstamt],[cgatamt],[igstamt],[OrderStatus],[refno],[totalcess],[agentID],[originalbillno],[originalbilldate],[SyncID],[SyncDatetime])VALUES('" + serverbillmaster.Rows[i]["Bill_No"].ToString() + "','" + serverbillmaster.Rows[i]["Bill_Date"].ToString() + "','" + serverbillmaster.Rows[i]["Terms"].ToString() + "','" + serverbillmaster.Rows[i]["ClientID"].ToString() + "','" + serverbillmaster.Rows[i]["PO_No"].ToString() + "','" + serverbillmaster.Rows[i]["SaleType"].ToString() + "','" + serverbillmaster.Rows[i]["count"].ToString() + "','" + serverbillmaster.Rows[i]["totalqty"].ToString() + "','" + serverbillmaster.Rows[i]["totalbasic"].ToString() + "','" + serverbillmaster.Rows[i]["totaltax"].ToString() + "','" + serverbillmaster.Rows[i]["totalnet"].ToString() + "','" + serverbillmaster.Rows[i]["isactive"].ToString() + "','" + serverbillmaster.Rows[i]["apprweight"].ToString() + "','" + serverbillmaster.Rows[i]["dispatchdetails"].ToString() + "','" + serverbillmaster.Rows[i]["remarks"].ToString() + "','" + serverbillmaster.Rows[i]["BillType"].ToString() + "','" + serverbillmaster.Rows[i]["billno"].ToString() + "','" + serverbillmaster.Rows[i]["totaladdtax"].ToString() + "','" + serverbillmaster.Rows[i]["roudoff"].ToString() + "','" + serverbillmaster.Rows[i]["Duedate"].ToString() + "','" + serverbillmaster.Rows[i]["totalaqty"].ToString() + "','" + serverbillmaster.Rows[i]["totalfree"].ToString() + "','" + serverbillmaster.Rows[i]["totaldiscount"].ToString() + "','" + serverbillmaster.Rows[i]["totaladddiscount"].ToString() + "','" + serverbillmaster.Rows[i]["totalamount"].ToString() + "','" + serverbillmaster.Rows[i]["totalservicejob"].ToString() + "','" + serverbillmaster.Rows[i]["totalcharges"].ToString() + "','" + serverbillmaster.Rows[i]["Delieveryat"].ToString() + "','" + serverbillmaster.Rows[i]["fraight"].ToString() + "','" + serverbillmaster.Rows[i]["vehicleno"].ToString() + "','" + serverbillmaster.Rows[i]["grrrno"].ToString() + "','" + serverbillmaster.Rows[i]["noofskids"].ToString() + "','" + serverbillmaster.Rows[i]["sgstamt"].ToString() + "','" + serverbillmaster.Rows[i]["cgatamt"].ToString() + "','" + serverbillmaster.Rows[i]["igstamt"].ToString() + "','" + serverbillmaster.Rows[i]["OrderStatus"].ToString() + "','" + serverbillmaster.Rows[i]["refno"].ToString() + "','" + serverbillmaster.Rows[i]["totalcess"].ToString() + "','" + serverbillmaster.Rows[i]["agentID"].ToString() + "','" + serverbillmaster.Rows[i]["originalbillno"].ToString() + "','" + serverbillmaster.Rows[i]["originalbilldate"].ToString() + "','" + serverbillmaster.Rows[i]["SyncID"].ToString() + "','" + serverbillmaster.Rows[i]["SyncDatetime"].ToString() + "')");
                        }
                    }
                }
                #endregion

                //Bill ProductMaster
                #region
                DataTable clientbillProductmaster = conn.getdataset("Select * from BillProductMaster where SyncDatetime >'" + lastsyncdatetime.ToString("MM/dd/yyyy h:mm:ss") + "'");
                DataTable serverbillProductmaster = conn.getdataset("Select * from BillProductMaster where SyncDatetime >'" + lastsyncdatetime.ToString("MM/dd/yyyy h:mm:ss") + "'", con);
                if (clientbillProductmaster.Rows.Count > 0)
                {
                    for (int i = 0; i < clientbillProductmaster.Rows.Count; i++)
                    {
                        DataTable isexist = conn.getdataset("Select * from BillProductMaster where SyncID='" + clientbillProductmaster.Rows[i]["SyncID"].ToString() + "'", con);
                        if (isexist.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(isexist.Rows[0]["SyncID"].ToString()))
                            {
                                string local = clientbillProductmaster.Rows[i]["SyncDatetime"].ToString();
                                string Server = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Update in local
                                    conn.execute("UPDATE [dbo].[BillProductMaster] SET [Bill_No] ='" + isexist.Rows[0]["Bill_No"].ToString() + "',[Bill_Run_Date] = '" + isexist.Rows[0]["Bill_Run_Date"].ToString() + "',[Productname] = '" + isexist.Rows[0]["Productname"].ToString() + "',[Packing] = '" + isexist.Rows[0]["Packing"].ToString() + "',[Bags] = '" + isexist.Rows[0]["Bags"].ToString() + "',[MRP] = '" + isexist.Rows[0]["MRP"].ToString() + "',[Pqty] = '" + isexist.Rows[0]["Pqty"].ToString() + "',[Aqty] = '" + isexist.Rows[0]["Aqty"].ToString() + "',[Rate] = '" + isexist.Rows[0]["Rate"].ToString() + "',[Per] = '" + isexist.Rows[0]["Per"].ToString() + "',[Total] = '" + isexist.Rows[0]["Total"].ToString() + "',[Tax] = '" + isexist.Rows[0]["Tax"].ToString() + "',[Amount] = '" + isexist.Rows[0]["Amount"].ToString() + "',[isactive] = '" + isexist.Rows[0]["isactive"].ToString() + "',[qty] = '" + isexist.Rows[0]["qty"].ToString() + "',[Billtype] = '" + isexist.Rows[0]["Billtype"].ToString() + "',[billno] = '" + isexist.Rows[0]["billno"].ToString() + "',[addtax] = '" + isexist.Rows[0]["addtax"].ToString() + "',[batch] = '" + isexist.Rows[0]["batch"].ToString() + "',[free] = '" + isexist.Rows[0]["free"].ToString() + "',[discountper] = '" + isexist.Rows[0]["discountper"].ToString() + "',[discountamt] = '" + isexist.Rows[0]["discountamt"].ToString() + "',[productid] = '" + isexist.Rows[0]["productid"].ToString() + "',[sgstper] = '" + isexist.Rows[0]["sgstper"].ToString() + "',[sgstamt] = '" + isexist.Rows[0]["sgstamt"].ToString() + "',[cgstper] = '" + isexist.Rows[0]["cgstper"].ToString() + "',[cgstamt] = '" + isexist.Rows[0]["cgstamt"].ToString() + "',[igstper] = '" + isexist.Rows[0]["igstper"].ToString() + "',[igdtamt] = '" + isexist.Rows[0]["igdtamt"].ToString() + "',[addtaxper] = '" + isexist.Rows[0]["addtaxper"].ToString() + "',[serialno] = '" + isexist.Rows[0]["serialno"].ToString() + "',[refno] = '" + isexist.Rows[0]["refno"].ToString() + "',[cess] = '" + isexist.Rows[0]["cess"].ToString() + "',[SyncID] = '" + isexist.Rows[0]["SyncID"].ToString() + "',[SyncDatetime] = '" + isexist.Rows[0]["SyncDatetime"].ToString() + "' WHERE SyncID='" + isexist.Rows[0]["SyncID"].ToString() + "'");
                                }
                                else
                                {
                                    //Update in Server
                                    conn.execute("UPDATE [dbo].[BillProductMaster] SET [Bill_No] ='" + clientbillProductmaster.Rows[i]["Bill_No"].ToString() + "',[Bill_Run_Date] = '" + clientbillProductmaster.Rows[i]["Bill_Run_Date"].ToString() + "',[Productname] = '" + clientbillProductmaster.Rows[i]["Productname"].ToString() + "',[Packing] = '" + clientbillProductmaster.Rows[i]["Packing"].ToString() + "',[Bags] = '" + clientbillProductmaster.Rows[i]["Bags"].ToString() + "',[MRP] = '" + clientbillProductmaster.Rows[i]["MRP"].ToString() + "',[Pqty] = '" + clientbillProductmaster.Rows[i]["Pqty"].ToString() + "',[Aqty] = '" + clientbillProductmaster.Rows[i]["Aqty"].ToString() + "',[Rate] = '" + clientbillProductmaster.Rows[i]["Rate"].ToString() + "',[Per] = '" + clientbillProductmaster.Rows[i]["Per"].ToString() + "',[Total] = '" + clientbillProductmaster.Rows[i]["Total"].ToString() + "',[Tax] = '" + clientbillProductmaster.Rows[i]["Tax"].ToString() + "',[Amount] = '" + clientbillProductmaster.Rows[i]["Amount"].ToString() + "',[isactive] = '" + clientbillProductmaster.Rows[i]["isactive"].ToString() + "',[qty] = '" + clientbillProductmaster.Rows[i]["qty"].ToString() + "',[Billtype] = '" + clientbillProductmaster.Rows[i]["Billtype"].ToString() + "',[billno] = '" + clientbillProductmaster.Rows[i]["billno"].ToString() + "',[addtax] = '" + clientbillProductmaster.Rows[i]["addtax"].ToString() + "',[batch] = '" + clientbillProductmaster.Rows[i]["batch"].ToString() + "',[free] = '" + clientbillProductmaster.Rows[i]["free"].ToString() + "',[discountper] = '" + clientbillProductmaster.Rows[i]["discountper"].ToString() + "',[discountamt] = '" + clientbillProductmaster.Rows[i]["discountamt"].ToString() + "',[productid] = '" + clientbillProductmaster.Rows[i]["productid"].ToString() + "',[sgstper] = '" + clientbillProductmaster.Rows[i]["sgstper"].ToString() + "',[sgstamt] = '" + clientbillProductmaster.Rows[i]["sgstamt"].ToString() + "',[cgstper] = '" + clientbillProductmaster.Rows[i]["cgstper"].ToString() + "',[cgstamt] = '" + clientbillProductmaster.Rows[i]["cgstamt"].ToString() + "',[igstper] = '" + clientbillProductmaster.Rows[i]["igstper"].ToString() + "',[igdtamt] = '" + clientbillProductmaster.Rows[i]["igdtamt"].ToString() + "',[addtaxper] = '" + clientbillProductmaster.Rows[i]["addtaxper"].ToString() + "',[serialno] = '" + clientbillProductmaster.Rows[i]["serialno"].ToString() + "',[refno] = '" + clientbillProductmaster.Rows[i]["refno"].ToString() + "',[cess] = '" + clientbillProductmaster.Rows[i]["cess"].ToString() + "',[SyncID] = '" + clientbillProductmaster.Rows[i]["SyncID"].ToString() + "',[SyncDatetime] = '" + clientbillProductmaster.Rows[i]["SyncDatetime"].ToString() + "' WHERE SyncID='" + clientbillProductmaster.Rows[i]["SyncID"].ToString() + "'", con);
                                }
                            }
                            else
                            {
                                string local = clientbillProductmaster.Rows[i]["SyncDatetime"].ToString();
                                string Server = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Insert in local
                                    conn.execute("INSERT INTO [dbo].[BillProductMaster]([Bill_No],[Bill_Run_Date],[Productname],[Packing],[Bags],[MRP],[Pqty],[Aqty],[Rate],[Per],[Total],[Tax],[Amount],[isactive],[qty],[Billtype],[billno],[addtax],[batch],[free],[discountper],[discountamt],[productid],[sgstper],[sgstamt],[cgstper],[cgstamt],[igstper],[igdtamt],[addtaxper],[serialno],[refno],[cess],[SyncID],[SyncDatetime])VALUES('" + isexist.Rows[0]["Bill_No"].ToString() + "','" + isexist.Rows[0]["Bill_Run_Date"].ToString() + "','" + isexist.Rows[0]["Productname"].ToString() + "','" + isexist.Rows[0]["Packing"].ToString() + "','" + isexist.Rows[0]["Bags"].ToString() + "','" + isexist.Rows[0]["MRP"].ToString() + "','" + isexist.Rows[0]["Pqty"].ToString() + "','" + isexist.Rows[0]["Aqty"].ToString() + "','" + isexist.Rows[0]["Rate"].ToString() + "','" + isexist.Rows[0]["Per"].ToString() + "','" + isexist.Rows[0]["Total"].ToString() + "','" + isexist.Rows[0]["Tax"].ToString() + "','" + isexist.Rows[0]["Amount"].ToString() + "','" + isexist.Rows[0]["isactive"].ToString() + "','" + isexist.Rows[0]["qty"].ToString() + "','" + isexist.Rows[0]["Billtype"].ToString() + "','" + isexist.Rows[0]["billno"].ToString() + "','" + isexist.Rows[0]["addtax"].ToString() + "','" + isexist.Rows[0]["batch"].ToString() + "','" + isexist.Rows[0]["free"].ToString() + "','" + isexist.Rows[0]["discountper"].ToString() + "','" + isexist.Rows[0]["discountamt"].ToString() + "','" + isexist.Rows[0]["productid"].ToString() + "','" + isexist.Rows[0]["sgstper"].ToString() + "','" + isexist.Rows[0]["sgstamt"].ToString() + "','" + isexist.Rows[0]["cgstper"].ToString() + "','" + isexist.Rows[0]["cgstamt"].ToString() + "','" + isexist.Rows[0]["igstper"].ToString() + "','" + isexist.Rows[0]["igdtamt"].ToString() + "','" + isexist.Rows[0]["addtaxper"].ToString() + "','" + isexist.Rows[0]["serialno"].ToString() + "','" + isexist.Rows[0]["refno"].ToString() + "','" + isexist.Rows[0]["cess"].ToString() + "','" + isexist.Rows[0]["SyncID"].ToString() + "','" + isexist.Rows[0]["SyncDatetime"].ToString() + "')");

                                }
                                else
                                {
                                    //Insert in Server
                                    conn.execute("INSERT INTO [dbo].[BillProductMaster]([Bill_No],[Bill_Run_Date],[Productname],[Packing],[Bags],[MRP],[Pqty],[Aqty],[Rate],[Per],[Total],[Tax],[Amount],[isactive],[qty],[Billtype],[billno],[addtax],[batch],[free],[discountper],[discountamt],[productid],[sgstper],[sgstamt],[cgstper],[cgstamt],[igstper],[igdtamt],[addtaxper],[serialno],[refno],[cess],[SyncID],[SyncDatetime])VALUES('" + clientbillProductmaster.Rows[i]["Bill_No"].ToString() + "','" + clientbillProductmaster.Rows[i]["Bill_Run_Date"].ToString() + "','" + clientbillProductmaster.Rows[i]["Productname"].ToString() + "','" + clientbillProductmaster.Rows[i]["Packing"].ToString() + "','" + clientbillProductmaster.Rows[i]["Bags"].ToString() + "','" + clientbillProductmaster.Rows[i]["MRP"].ToString() + "','" + clientbillProductmaster.Rows[i]["Pqty"].ToString() + "','" + clientbillProductmaster.Rows[i]["Aqty"].ToString() + "','" + clientbillProductmaster.Rows[i]["Rate"].ToString() + "','" + clientbillProductmaster.Rows[i]["Per"].ToString() + "','" + clientbillProductmaster.Rows[i]["Total"].ToString() + "','" + clientbillProductmaster.Rows[i]["Tax"].ToString() + "','" + clientbillProductmaster.Rows[i]["Amount"].ToString() + "','" + clientbillProductmaster.Rows[i]["isactive"].ToString() + "','" + clientbillProductmaster.Rows[i]["qty"].ToString() + "','" + clientbillProductmaster.Rows[i]["Billtype"].ToString() + "','" + clientbillProductmaster.Rows[i]["billno"].ToString() + "','" + clientbillProductmaster.Rows[i]["addtax"].ToString() + "','" + clientbillProductmaster.Rows[i]["batch"].ToString() + "','" + clientbillProductmaster.Rows[i]["free"].ToString() + "','" + clientbillProductmaster.Rows[i]["discountper"].ToString() + "','" + clientbillProductmaster.Rows[i]["discountamt"].ToString() + "','" + clientbillProductmaster.Rows[i]["productid"].ToString() + "','" + clientbillProductmaster.Rows[i]["sgstper"].ToString() + "','" + clientbillProductmaster.Rows[i]["sgstamt"].ToString() + "','" + clientbillProductmaster.Rows[i]["cgstper"].ToString() + "','" + clientbillProductmaster.Rows[i]["cgstamt"].ToString() + "','" + clientbillProductmaster.Rows[i]["igstper"].ToString() + "','" + clientbillProductmaster.Rows[i]["igdtamt"].ToString() + "','" + clientbillProductmaster.Rows[i]["addtaxper"].ToString() + "','" + clientbillProductmaster.Rows[i]["serialno"].ToString() + "','" + clientbillProductmaster.Rows[i]["refno"].ToString() + "','" + clientbillProductmaster.Rows[i]["cess"].ToString() + "','" + clientbillProductmaster.Rows[i]["SyncID"].ToString() + "','" + clientbillProductmaster.Rows[i]["SyncDatetime"].ToString() + "')", con);
                                }
                            }
                        }
                        else
                        {
                            conn.execute("INSERT INTO [dbo].[BillProductMaster]([Bill_No],[Bill_Run_Date],[Productname],[Packing],[Bags],[MRP],[Pqty],[Aqty],[Rate],[Per],[Total],[Tax],[Amount],[isactive],[qty],[Billtype],[billno],[addtax],[batch],[free],[discountper],[discountamt],[productid],[sgstper],[sgstamt],[cgstper],[cgstamt],[igstper],[igdtamt],[addtaxper],[serialno],[refno],[cess],[SyncID],[SyncDatetime])VALUES('" + clientbillProductmaster.Rows[i]["Bill_No"].ToString() + "','" + clientbillProductmaster.Rows[i]["Bill_Run_Date"].ToString() + "','" + clientbillProductmaster.Rows[i]["Productname"].ToString() + "','" + clientbillProductmaster.Rows[i]["Packing"].ToString() + "','" + clientbillProductmaster.Rows[i]["Bags"].ToString() + "','" + clientbillProductmaster.Rows[i]["MRP"].ToString() + "','" + clientbillProductmaster.Rows[i]["Pqty"].ToString() + "','" + clientbillProductmaster.Rows[i]["Aqty"].ToString() + "','" + clientbillProductmaster.Rows[i]["Rate"].ToString() + "','" + clientbillProductmaster.Rows[i]["Per"].ToString() + "','" + clientbillProductmaster.Rows[i]["Total"].ToString() + "','" + clientbillProductmaster.Rows[i]["Tax"].ToString() + "','" + clientbillProductmaster.Rows[i]["Amount"].ToString() + "','" + clientbillProductmaster.Rows[i]["isactive"].ToString() + "','" + clientbillProductmaster.Rows[i]["qty"].ToString() + "','" + clientbillProductmaster.Rows[i]["Billtype"].ToString() + "','" + clientbillProductmaster.Rows[i]["billno"].ToString() + "','" + clientbillProductmaster.Rows[i]["addtax"].ToString() + "','" + clientbillProductmaster.Rows[i]["batch"].ToString() + "','" + clientbillProductmaster.Rows[i]["free"].ToString() + "','" + clientbillProductmaster.Rows[i]["discountper"].ToString() + "','" + clientbillProductmaster.Rows[i]["discountamt"].ToString() + "','" + clientbillProductmaster.Rows[i]["productid"].ToString() + "','" + clientbillProductmaster.Rows[i]["sgstper"].ToString() + "','" + clientbillProductmaster.Rows[i]["sgstamt"].ToString() + "','" + clientbillProductmaster.Rows[i]["cgstper"].ToString() + "','" + clientbillProductmaster.Rows[i]["cgstamt"].ToString() + "','" + clientbillProductmaster.Rows[i]["igstper"].ToString() + "','" + clientbillProductmaster.Rows[i]["igdtamt"].ToString() + "','" + clientbillProductmaster.Rows[i]["addtaxper"].ToString() + "','" + clientbillProductmaster.Rows[i]["serialno"].ToString() + "','" + clientbillProductmaster.Rows[i]["refno"].ToString() + "','" + clientbillProductmaster.Rows[i]["cess"].ToString() + "','" + clientbillProductmaster.Rows[i]["SyncID"].ToString() + "','" + clientbillProductmaster.Rows[i]["SyncDatetime"].ToString() + "')", con);
                        }
                    }
                }
                if (serverbillProductmaster.Rows.Count > 0)
                {
                    for (int i = 0; i < serverbillProductmaster.Rows.Count; i++)
                    {
                        DataTable isexist = conn.getdataset("Select * from BillProductMaster where SyncID='" + serverbillProductmaster.Rows[i]["SyncID"].ToString() + "'");
                        if (isexist.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(isexist.Rows[0]["SyncID"].ToString()))
                            {
                                string Server = serverbillProductmaster.Rows[i]["SyncDatetime"].ToString();
                                string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Update in local
                                    conn.execute("UPDATE [dbo].[BillProductMaster] SET [Bill_No] ='" + serverbillProductmaster.Rows[i]["Bill_No"].ToString() + "',[Bill_Run_Date] = '" + serverbillProductmaster.Rows[i]["Bill_Run_Date"].ToString() + "',[Productname] = '" + serverbillProductmaster.Rows[i]["Productname"].ToString() + "',[Packing] = '" + serverbillProductmaster.Rows[i]["Packing"].ToString() + "',[Bags] = '" + serverbillProductmaster.Rows[i]["Bags"].ToString() + "',[MRP] = '" + serverbillProductmaster.Rows[i]["MRP"].ToString() + "',[Pqty] = '" + serverbillProductmaster.Rows[i]["Pqty"].ToString() + "',[Aqty] = '" + serverbillProductmaster.Rows[i]["Aqty"].ToString() + "',[Rate] = '" + serverbillProductmaster.Rows[i]["Rate"].ToString() + "',[Per] = '" + serverbillProductmaster.Rows[i]["Per"].ToString() + "',[Total] = '" + serverbillProductmaster.Rows[i]["Total"].ToString() + "',[Tax] = '" + serverbillProductmaster.Rows[i]["Tax"].ToString() + "',[Amount] = '" + serverbillProductmaster.Rows[i]["Amount"].ToString() + "',[isactive] = '" + serverbillProductmaster.Rows[i]["isactive"].ToString() + "',[qty] = '" + serverbillProductmaster.Rows[i]["qty"].ToString() + "',[Billtype] = '" + serverbillProductmaster.Rows[i]["Billtype"].ToString() + "',[billno] = '" + serverbillProductmaster.Rows[i]["billno"].ToString() + "',[addtax] = '" + serverbillProductmaster.Rows[i]["addtax"].ToString() + "',[batch] = '" + serverbillProductmaster.Rows[i]["batch"].ToString() + "',[free] = '" + serverbillProductmaster.Rows[i]["free"].ToString() + "',[discountper] = '" + serverbillProductmaster.Rows[i]["discountper"].ToString() + "',[discountamt] = '" + serverbillProductmaster.Rows[i]["discountamt"].ToString() + "',[productid] = '" + serverbillProductmaster.Rows[i]["productid"].ToString() + "',[sgstper] = '" + serverbillProductmaster.Rows[i]["sgstper"].ToString() + "',[sgstamt] = '" + serverbillProductmaster.Rows[i]["sgstamt"].ToString() + "',[cgstper] = '" + serverbillProductmaster.Rows[i]["cgstper"].ToString() + "',[cgstamt] = '" + serverbillProductmaster.Rows[i]["cgstamt"].ToString() + "',[igstper] = '" + serverbillProductmaster.Rows[i]["igstper"].ToString() + "',[igdtamt] = '" + serverbillProductmaster.Rows[i]["igdtamt"].ToString() + "',[addtaxper] = '" + serverbillProductmaster.Rows[i]["addtaxper"].ToString() + "',[serialno] = '" + serverbillProductmaster.Rows[i]["serialno"].ToString() + "',[refno] = '" + serverbillProductmaster.Rows[i]["refno"].ToString() + "',[cess] = '" + serverbillProductmaster.Rows[i]["cess"].ToString() + "',[SyncID] = '" + serverbillProductmaster.Rows[i]["SyncID"].ToString() + "',[SyncDatetime] = '" + serverbillProductmaster.Rows[i]["SyncDatetime"].ToString() + "' WHERE SyncID='" + serverbillProductmaster.Rows[i]["SyncID"].ToString() + "'");
                                }
                                else
                                {
                                    //Update in Server
                                    conn.execute("UPDATE [dbo].[BillProductMaster] SET [Bill_No] ='" + isexist.Rows[0]["Bill_No"].ToString() + "',[Bill_Run_Date] = '" + isexist.Rows[0]["Bill_Run_Date"].ToString() + "',[Productname] = '" + isexist.Rows[0]["Productname"].ToString() + "',[Packing] = '" + isexist.Rows[0]["Packing"].ToString() + "',[Bags] = '" + isexist.Rows[0]["Bags"].ToString() + "',[MRP] = '" + isexist.Rows[0]["MRP"].ToString() + "',[Pqty] = '" + isexist.Rows[0]["Pqty"].ToString() + "',[Aqty] = '" + isexist.Rows[0]["Aqty"].ToString() + "',[Rate] = '" + isexist.Rows[0]["Rate"].ToString() + "',[Per] = '" + isexist.Rows[0]["Per"].ToString() + "',[Total] = '" + isexist.Rows[0]["Total"].ToString() + "',[Tax] = '" + isexist.Rows[0]["Tax"].ToString() + "',[Amount] = '" + isexist.Rows[0]["Amount"].ToString() + "',[isactive] = '" + isexist.Rows[0]["isactive"].ToString() + "',[qty] = '" + isexist.Rows[0]["qty"].ToString() + "',[Billtype] = '" + isexist.Rows[0]["Billtype"].ToString() + "',[billno] = '" + isexist.Rows[0]["billno"].ToString() + "',[addtax] = '" + isexist.Rows[0]["addtax"].ToString() + "',[batch] = '" + isexist.Rows[0]["batch"].ToString() + "',[free] = '" + isexist.Rows[0]["free"].ToString() + "',[discountper] = '" + isexist.Rows[0]["discountper"].ToString() + "',[discountamt] = '" + isexist.Rows[0]["discountamt"].ToString() + "',[productid] = '" + isexist.Rows[0]["productid"].ToString() + "',[sgstper] = '" + isexist.Rows[0]["sgstper"].ToString() + "',[sgstamt] = '" + isexist.Rows[0]["sgstamt"].ToString() + "',[cgstper] = '" + isexist.Rows[0]["cgstper"].ToString() + "',[cgstamt] = '" + isexist.Rows[0]["cgstamt"].ToString() + "',[igstper] = '" + isexist.Rows[0]["igstper"].ToString() + "',[igdtamt] = '" + isexist.Rows[0]["igdtamt"].ToString() + "',[addtaxper] = '" + isexist.Rows[0]["addtaxper"].ToString() + "',[serialno] = '" + isexist.Rows[0]["serialno"].ToString() + "',[refno] = '" + isexist.Rows[0]["refno"].ToString() + "',[cess] = '" + isexist.Rows[0]["cess"].ToString() + "',[SyncID] = '" + isexist.Rows[0]["SyncID"].ToString() + "',[SyncDatetime] = '" + isexist.Rows[0]["SyncDatetime"].ToString() + "' WHERE SyncID='" + isexist.Rows[0]["SyncID"].ToString() + "'", con);
                                }
                            }
                            else
                            {
                                string Server = serverbillProductmaster.Rows[i]["SyncDatetime"].ToString();
                                string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Insert in local
                                    conn.execute("INSERT INTO [dbo].[BillProductMaster]([Bill_No],[Bill_Run_Date],[Productname],[Packing],[Bags],[MRP],[Pqty],[Aqty],[Rate],[Per],[Total],[Tax],[Amount],[isactive],[qty],[Billtype],[billno],[addtax],[batch],[free],[discountper],[discountamt],[productid],[sgstper],[sgstamt],[cgstper],[cgstamt],[igstper],[igdtamt],[addtaxper],[serialno],[refno],[cess],[SyncID],[SyncDatetime])VALUES('" + serverbillProductmaster.Rows[i]["Bill_No"].ToString() + "','" + serverbillProductmaster.Rows[i]["Bill_Run_Date"].ToString() + "','" + serverbillProductmaster.Rows[i]["Productname"].ToString() + "','" + serverbillProductmaster.Rows[i]["Packing"].ToString() + "','" + serverbillProductmaster.Rows[i]["Bags"].ToString() + "','" + serverbillProductmaster.Rows[i]["MRP"].ToString() + "','" + serverbillProductmaster.Rows[i]["Pqty"].ToString() + "','" + serverbillProductmaster.Rows[i]["Aqty"].ToString() + "','" + serverbillProductmaster.Rows[i]["Rate"].ToString() + "','" + serverbillProductmaster.Rows[i]["Per"].ToString() + "','" + serverbillProductmaster.Rows[i]["Total"].ToString() + "','" + serverbillProductmaster.Rows[i]["Tax"].ToString() + "','" + serverbillProductmaster.Rows[i]["Amount"].ToString() + "','" + serverbillProductmaster.Rows[i]["isactive"].ToString() + "','" + serverbillProductmaster.Rows[i]["qty"].ToString() + "','" + serverbillProductmaster.Rows[i]["Billtype"].ToString() + "','" + serverbillProductmaster.Rows[i]["billno"].ToString() + "','" + serverbillProductmaster.Rows[i]["addtax"].ToString() + "','" + serverbillProductmaster.Rows[i]["batch"].ToString() + "','" + serverbillProductmaster.Rows[i]["free"].ToString() + "','" + serverbillProductmaster.Rows[i]["discountper"].ToString() + "','" + serverbillProductmaster.Rows[i]["discountamt"].ToString() + "','" + serverbillProductmaster.Rows[i]["productid"].ToString() + "','" + serverbillProductmaster.Rows[i]["sgstper"].ToString() + "','" + serverbillProductmaster.Rows[i]["sgstamt"].ToString() + "','" + serverbillProductmaster.Rows[i]["cgstper"].ToString() + "','" + serverbillProductmaster.Rows[i]["cgstamt"].ToString() + "','" + serverbillProductmaster.Rows[i]["igstper"].ToString() + "','" + serverbillProductmaster.Rows[i]["igdtamt"].ToString() + "','" + serverbillProductmaster.Rows[i]["addtaxper"].ToString() + "','" + serverbillProductmaster.Rows[i]["serialno"].ToString() + "','" + serverbillProductmaster.Rows[i]["refno"].ToString() + "','" + serverbillProductmaster.Rows[i]["cess"].ToString() + "','" + serverbillProductmaster.Rows[i]["SyncID"].ToString() + "','" + serverbillProductmaster.Rows[i]["SyncDatetime"].ToString() + "')");
                                    //conn.execute("INSERT INTO [dbo].[BillMaster]([Bill_No],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[BillType],[billno],[totaladdtax],[roudoff],[Duedate],[totalaqty],[totalfree],[totaldiscount],[totaladddiscount],[totalamount],[totalservicejob],[totalcharges],[Delieveryat],[fraight],[vehicleno],[grrrno],[noofskids],[sgstamt],[cgatamt],[igstamt],[OrderStatus],[refno],[totalcess],[agentID],[originalbillno],[originalbilldate],[SyncID],[SyncDatetime])VALUES('" + serverbillmaster.Rows[i]["Bill_No"].ToString() + "','" + serverbillmaster.Rows[i]["Bill_Date"].ToString() + "','" + serverbillmaster.Rows[i]["Terms"].ToString() + "','" + serverbillmaster.Rows[i]["ClientID"].ToString() + "','" + serverbillmaster.Rows[i]["PO_No"].ToString() + "','" + serverbillmaster.Rows[i]["SaleType"].ToString() + "','" + serverbillmaster.Rows[i]["count"].ToString() + "','" + serverbillmaster.Rows[i]["totalqty"].ToString() + "','" + serverbillmaster.Rows[i]["totalbasic"].ToString() + "','" + serverbillmaster.Rows[i]["totaltax"].ToString() + "','" + serverbillmaster.Rows[i]["totalnet"].ToString() + "','" + serverbillmaster.Rows[i]["isactive"].ToString() + "','" + serverbillmaster.Rows[i]["apprweight"].ToString() + "','" + serverbillmaster.Rows[i]["dispatchdetails"].ToString() + "','" + serverbillmaster.Rows[i]["remarks"].ToString() + "','" + serverbillmaster.Rows[i]["BillType"].ToString() + "','" + serverbillmaster.Rows[i]["billno"].ToString() + "','" + serverbillmaster.Rows[i]["totaladdtax"].ToString() + "','" + serverbillmaster.Rows[i]["roudoff"].ToString() + "','" + serverbillmaster.Rows[i]["Duedate"].ToString() + "','" + serverbillmaster.Rows[i]["totalaqty"].ToString() + "','" + serverbillmaster.Rows[i]["totalfree"].ToString() + "','" + serverbillmaster.Rows[i]["totaldiscount"].ToString() + "','" + serverbillmaster.Rows[i]["totaladddiscount"].ToString() + "','" + serverbillmaster.Rows[i]["totalamount"].ToString() + "','" + serverbillmaster.Rows[i]["totalservicejob"].ToString() + "','" + serverbillmaster.Rows[i]["totalcharges"].ToString() + "','" + serverbillmaster.Rows[i]["Delieveryat"].ToString() + "','" + serverbillmaster.Rows[i]["fraight"].ToString() + "','" + serverbillmaster.Rows[i]["vehicleno"].ToString() + "','" + serverbillmaster.Rows[i]["grrrno"].ToString() + "','" + serverbillmaster.Rows[i]["noofskids"].ToString() + "','" + serverbillmaster.Rows[i]["sgstamt"].ToString() + "','" + serverbillmaster.Rows[i]["cgatamt"].ToString() + "','" + serverbillmaster.Rows[i]["igstamt"].ToString() + "','" + serverbillmaster.Rows[i]["OrderStatus"].ToString() + "','" + serverbillmaster.Rows[i]["refno"].ToString() + "','" + serverbillmaster.Rows[i]["totalcess"].ToString() + "','" + serverbillmaster.Rows[i]["agentID"].ToString() + "','" + serverbillmaster.Rows[i]["originalbillno"].ToString() + "','" + serverbillmaster.Rows[i]["originalbilldate"].ToString() + "','" + serverbillmaster.Rows[i]["SyncID"].ToString() + "','" + serverbillmaster.Rows[i]["SyncDatetime"].ToString() + "')");
                                }
                                else
                                {
                                    //Insert in Server
                                    //  conn.execute("INSERT INTO [dbo].[BillMaster]([Bill_No],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[BillType],[billno],[totaladdtax],[roudoff],[Duedate],[totalaqty],[totalfree],[totaldiscount],[totaladddiscount],[totalamount],[totalservicejob],[totalcharges],[Delieveryat],[fraight],[vehicleno],[grrrno],[noofskids],[sgstamt],[cgatamt],[igstamt],[OrderStatus],[refno],[totalcess],[agentID],[originalbillno],[originalbilldate],[SyncID],[SyncDatetime])VALUES('" + isexist.Rows[0]["Bill_No"].ToString() + "','" + isexist.Rows[0]["Bill_Date"].ToString() + "','" + isexist.Rows[0]["Terms"].ToString() + "','" + isexist.Rows[0]["ClientID"].ToString() + "','" + isexist.Rows[0]["PO_No"].ToString() + "','" + isexist.Rows[0]["SaleType"].ToString() + "','" + isexist.Rows[0]["count"].ToString() + "','" + isexist.Rows[0]["totalqty"].ToString() + "','" + isexist.Rows[0]["totalbasic"].ToString() + "','" + isexist.Rows[0]["totaltax"].ToString() + "','" + isexist.Rows[0]["totalnet"].ToString() + "','" + isexist.Rows[0]["isactive"].ToString() + "','" + isexist.Rows[0]["apprweight"].ToString() + "','" + isexist.Rows[0]["dispatchdetails"].ToString() + "','" + isexist.Rows[0]["remarks"].ToString() + "','" + isexist.Rows[0]["BillType"].ToString() + "','" + isexist.Rows[0]["billno"].ToString() + "','" + isexist.Rows[0]["totaladdtax"].ToString() + "','" + isexist.Rows[0]["roudoff"].ToString() + "','" + isexist.Rows[0]["Duedate"].ToString() + "','" + isexist.Rows[0]["totalaqty"].ToString() + "','" + isexist.Rows[0]["totalfree"].ToString() + "','" + isexist.Rows[0]["totaldiscount"].ToString() + "','" + isexist.Rows[0]["totaladddiscount"].ToString() + "','" + isexist.Rows[0]["totalamount"].ToString() + "','" + isexist.Rows[0]["totalservicejob"].ToString() + "','" + isexist.Rows[0]["totalcharges"].ToString() + "','" + isexist.Rows[0]["Delieveryat"].ToString() + "','" + isexist.Rows[0]["fraight"].ToString() + "','" + isexist.Rows[0]["vehicleno"].ToString() + "','" + isexist.Rows[0]["grrrno"].ToString() + "','" + isexist.Rows[0]["noofskids"].ToString() + "','" + isexist.Rows[0]["sgstamt"].ToString() + "','" + isexist.Rows[0]["cgatamt"].ToString() + "','" + isexist.Rows[0]["igstamt"].ToString() + "','" + isexist.Rows[0]["OrderStatus"].ToString() + "','" + isexist.Rows[0]["refno"].ToString() + "','" + isexist.Rows[0]["totalcess"].ToString() + "','" + isexist.Rows[0]["agentID"].ToString() + "','" + isexist.Rows[0]["originalbillno"].ToString() + "','" + isexist.Rows[0]["originalbilldate"].ToString() + "','" + isexist.Rows[0]["SyncID"].ToString() + "','" + isexist.Rows[0]["SyncDatetime"].ToString() + "')", con);
                                    conn.execute("INSERT INTO [dbo].[BillProductMaster]([Bill_No],[Bill_Run_Date],[Productname],[Packing],[Bags],[MRP],[Pqty],[Aqty],[Rate],[Per],[Total],[Tax],[Amount],[isactive],[qty],[Billtype],[billno],[addtax],[batch],[free],[discountper],[discountamt],[productid],[sgstper],[sgstamt],[cgstper],[cgstamt],[igstper],[igdtamt],[addtaxper],[serialno],[refno],[cess],[SyncID],[SyncDatetime])VALUES('" + isexist.Rows[0]["Bill_No"].ToString() + "','" + isexist.Rows[0]["Bill_Run_Date"].ToString() + "','" + isexist.Rows[0]["Productname"].ToString() + "','" + isexist.Rows[0]["Packing"].ToString() + "','" + isexist.Rows[0]["Bags"].ToString() + "','" + isexist.Rows[0]["MRP"].ToString() + "','" + isexist.Rows[0]["Pqty"].ToString() + "','" + isexist.Rows[0]["Aqty"].ToString() + "','" + isexist.Rows[0]["Rate"].ToString() + "','" + isexist.Rows[0]["Per"].ToString() + "','" + isexist.Rows[0]["Total"].ToString() + "','" + isexist.Rows[0]["Tax"].ToString() + "','" + isexist.Rows[0]["Amount"].ToString() + "','" + isexist.Rows[0]["isactive"].ToString() + "','" + isexist.Rows[0]["qty"].ToString() + "','" + isexist.Rows[0]["Billtype"].ToString() + "','" + isexist.Rows[0]["billno"].ToString() + "','" + isexist.Rows[0]["addtax"].ToString() + "','" + isexist.Rows[0]["batch"].ToString() + "','" + isexist.Rows[0]["free"].ToString() + "','" + isexist.Rows[0]["discountper"].ToString() + "','" + isexist.Rows[0]["discountamt"].ToString() + "','" + isexist.Rows[0]["productid"].ToString() + "','" + isexist.Rows[0]["sgstper"].ToString() + "','" + isexist.Rows[0]["sgstamt"].ToString() + "','" + isexist.Rows[0]["cgstper"].ToString() + "','" + isexist.Rows[0]["cgstamt"].ToString() + "','" + isexist.Rows[0]["igstper"].ToString() + "','" + isexist.Rows[0]["igdtamt"].ToString() + "','" + isexist.Rows[0]["addtaxper"].ToString() + "','" + isexist.Rows[0]["serialno"].ToString() + "','" + isexist.Rows[0]["refno"].ToString() + "','" + isexist.Rows[0]["cess"].ToString() + "','" + isexist.Rows[0]["SyncID"].ToString() + "','" + isexist.Rows[0]["SyncDatetime"].ToString() + "')", con);
                                }
                            }
                        }
                        else
                        {
                            conn.execute("INSERT INTO [dbo].[BillProductMaster]([Bill_No],[Bill_Run_Date],[Productname],[Packing],[Bags],[MRP],[Pqty],[Aqty],[Rate],[Per],[Total],[Tax],[Amount],[isactive],[qty],[Billtype],[billno],[addtax],[batch],[free],[discountper],[discountamt],[productid],[sgstper],[sgstamt],[cgstper],[cgstamt],[igstper],[igdtamt],[addtaxper],[serialno],[refno],[cess],[SyncID],[SyncDatetime])VALUES('" + serverbillProductmaster.Rows[i]["Bill_No"].ToString() + "','" + serverbillProductmaster.Rows[i]["Bill_Run_Date"].ToString() + "','" + serverbillProductmaster.Rows[i]["Productname"].ToString() + "','" + serverbillProductmaster.Rows[i]["Packing"].ToString() + "','" + serverbillProductmaster.Rows[i]["Bags"].ToString() + "','" + serverbillProductmaster.Rows[i]["MRP"].ToString() + "','" + serverbillProductmaster.Rows[i]["Pqty"].ToString() + "','" + serverbillProductmaster.Rows[i]["Aqty"].ToString() + "','" + serverbillProductmaster.Rows[i]["Rate"].ToString() + "','" + serverbillProductmaster.Rows[i]["Per"].ToString() + "','" + serverbillProductmaster.Rows[i]["Total"].ToString() + "','" + serverbillProductmaster.Rows[i]["Tax"].ToString() + "','" + serverbillProductmaster.Rows[i]["Amount"].ToString() + "','" + serverbillProductmaster.Rows[i]["isactive"].ToString() + "','" + serverbillProductmaster.Rows[i]["qty"].ToString() + "','" + serverbillProductmaster.Rows[i]["Billtype"].ToString() + "','" + serverbillProductmaster.Rows[i]["billno"].ToString() + "','" + serverbillProductmaster.Rows[i]["addtax"].ToString() + "','" + serverbillProductmaster.Rows[i]["batch"].ToString() + "','" + serverbillProductmaster.Rows[i]["free"].ToString() + "','" + serverbillProductmaster.Rows[i]["discountper"].ToString() + "','" + serverbillProductmaster.Rows[i]["discountamt"].ToString() + "','" + serverbillProductmaster.Rows[i]["productid"].ToString() + "','" + serverbillProductmaster.Rows[i]["sgstper"].ToString() + "','" + serverbillProductmaster.Rows[i]["sgstamt"].ToString() + "','" + serverbillProductmaster.Rows[i]["cgstper"].ToString() + "','" + serverbillProductmaster.Rows[i]["cgstamt"].ToString() + "','" + serverbillProductmaster.Rows[i]["igstper"].ToString() + "','" + serverbillProductmaster.Rows[i]["igdtamt"].ToString() + "','" + serverbillProductmaster.Rows[i]["addtaxper"].ToString() + "','" + serverbillProductmaster.Rows[i]["serialno"].ToString() + "','" + serverbillProductmaster.Rows[i]["refno"].ToString() + "','" + serverbillProductmaster.Rows[i]["cess"].ToString() + "','" + serverbillProductmaster.Rows[i]["SyncID"].ToString() + "','" + serverbillProductmaster.Rows[i]["SyncDatetime"].ToString() + "')");
                        }
                    }
                }
                #endregion

                //Billchargesmaster
                #region
                DataTable clientbillchargesmaster = conn.getdataset("Select * from Billchargesmaster where SyncDatetime >'" + lastsyncdatetime.ToString("MM/dd/yyyy h:mm:ss") + "'");
                DataTable serverbillchargesmaster = conn.getdataset("Select * from Billchargesmaster where SyncDatetime >'" + lastsyncdatetime.ToString("MM/dd/yyyy h:mm:ss") + "'", con);
                if (clientbillchargesmaster.Rows.Count > 0)
                {
                    for (int i = 0; i < clientbillchargesmaster.Rows.Count; i++)
                    {
                        DataTable isexist = conn.getdataset("Select * from Billchargesmaster where SyncID='" + clientbillchargesmaster.Rows[i]["SyncID"].ToString() + "'", con);
                        if (isexist.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(isexist.Rows[0]["SyncID"].ToString()))
                            {
                                string local = clientbillchargesmaster.Rows[i]["SyncDatetime"].ToString();
                                string Server = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Update in local
                                    conn.execute("UPDATE [dbo].[Billchargesmaster] SET [billno] = '" + isexist.Rows[0]["billno"].ToString() + "',[perticulars] = '" + isexist.Rows[0]["perticulars"].ToString() + "',[remarks] = '" + isexist.Rows[0]["remarks"].ToString() + "',[value] = '" + isexist.Rows[0]["value"].ToString() + "',[at] = '" + isexist.Rows[0]["at"].ToString() + "',[plusminus] = '" + isexist.Rows[0]["plusminus"].ToString() + "',[amount] = '" + isexist.Rows[0]["amount"].ToString() + "',[billtype] = '" + isexist.Rows[0]["billtype"].ToString() + "',[isactive] = '" + isexist.Rows[0]["isactive"].ToString() + "',[valueofexp] = '" + isexist.Rows[0]["valueofexp"].ToString() + "',[tax] = '" + isexist.Rows[0]["tax"].ToString() + "',[sgst] = '" + isexist.Rows[0]["sgst"].ToString() + "',[cgst] = '" + isexist.Rows[0]["cgst"].ToString() + "',[igst] = '" + isexist.Rows[0]["igst"].ToString() + "',[additax] ='" + isexist.Rows[0]["additax"].ToString() + "',[addtaxamt] = '" + isexist.Rows[0]["addtaxamt"].ToString() + "',[billsundryid] = '" + isexist.Rows[0]["billsundryid"].ToString() + "',[SyncID] = '" + isexist.Rows[0]["SyncID"].ToString() + "',[SyncDatetime] = '" + isexist.Rows[0]["SyncDatetime"].ToString() + "' WHERE SyncID='" + isexist.Rows[0]["SyncID"].ToString() + "'");
                                }
                                else
                                {
                                    //Update in Server
                                    conn.execute("UPDATE [dbo].[Billchargesmaster] SET [billno] = '" + clientbillchargesmaster.Rows[i]["billno"].ToString() + "',[perticulars] = '" + clientbillchargesmaster.Rows[i]["perticulars"].ToString() + "',[remarks] = '" + clientbillchargesmaster.Rows[i]["remarks"].ToString() + "',[value] = '" + clientbillchargesmaster.Rows[i]["value"].ToString() + "',[at] = '" + clientbillchargesmaster.Rows[i]["at"].ToString() + "',[plusminus] = '" + clientbillchargesmaster.Rows[i]["plusminus"].ToString() + "',[amount] = '" + clientbillchargesmaster.Rows[i]["amount"].ToString() + "',[billtype] = '" + clientbillchargesmaster.Rows[i]["billtype"].ToString() + "',[isactive] = '" + clientbillchargesmaster.Rows[i]["isactive"].ToString() + "',[valueofexp] = '" + clientbillchargesmaster.Rows[i]["valueofexp"].ToString() + "',[tax] = '" + clientbillchargesmaster.Rows[i]["tax"].ToString() + "',[sgst] = '" + clientbillchargesmaster.Rows[i]["sgst"].ToString() + "',[cgst] = '" + clientbillchargesmaster.Rows[i]["cgst"].ToString() + "',[igst] = '" + clientbillchargesmaster.Rows[i]["igst"].ToString() + "',[additax] ='" + clientbillchargesmaster.Rows[i]["additax"].ToString() + "',[addtaxamt] = '" + clientbillchargesmaster.Rows[i]["addtaxamt"].ToString() + "',[billsundryid] = '" + clientbillchargesmaster.Rows[i]["billsundryid"].ToString() + "',[SyncID] = '" + clientbillchargesmaster.Rows[i]["SyncID"].ToString() + "',[SyncDatetime] = '" + clientbillchargesmaster.Rows[i]["SyncDatetime"].ToString() + "' WHERE SyncID='" + clientbillchargesmaster.Rows[i]["SyncID"].ToString() + "'", con);
                                }
                            }
                            else
                            {
                                string local = clientbillchargesmaster.Rows[i]["SyncDatetime"].ToString();
                                string Server = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Insert in local
                                    conn.execute("INSERT INTO [dbo].[Billchargesmaster]([billno],[perticulars],[remarks],[value],[at],[plusminus],[amount],[billtype],[isactive],[valueofexp],[tax],[sgst],[cgst],[igst],[additax],[addtaxamt],[billsundryid],[SyncID],[SyncDatetime])VALUES('" + isexist.Rows[0]["billno"].ToString() + "','" + isexist.Rows[0]["perticulars"].ToString() + "','" + isexist.Rows[0]["remarks"].ToString() + "','" + isexist.Rows[0]["value"].ToString() + "','" + isexist.Rows[0]["at"].ToString() + "','" + isexist.Rows[0]["plusminus"].ToString() + "','" + isexist.Rows[0]["amount"].ToString() + "','" + isexist.Rows[0]["billtype"].ToString() + "','" + isexist.Rows[0]["isactive"].ToString() + "','" + isexist.Rows[0]["valueofexp"].ToString() + "','" + isexist.Rows[0]["tax"].ToString() + "','" + isexist.Rows[0]["sgst"].ToString() + "','" + isexist.Rows[0]["cgst"].ToString() + "','" + isexist.Rows[0]["igst"].ToString() + "','" + isexist.Rows[0]["additax"].ToString() + "','" + isexist.Rows[0]["addtaxamt"].ToString() + "','" + isexist.Rows[0]["billsundryid"].ToString() + "','" + isexist.Rows[0]["SyncID"].ToString() + "','" + isexist.Rows[0]["SyncDatetime"].ToString() + "')");
                                }
                                else
                                {
                                    //Insert in Server
                                    conn.execute("INSERT INTO [dbo].[Billchargesmaster]([billno],[perticulars],[remarks],[value],[at],[plusminus],[amount],[billtype],[isactive],[valueofexp],[tax],[sgst],[cgst],[igst],[additax],[addtaxamt],[billsundryid],[SyncID],[SyncDatetime])VALUES('" + clientbillchargesmaster.Rows[i]["billno"].ToString() + "','" + clientbillchargesmaster.Rows[i]["perticulars"].ToString() + "','" + clientbillchargesmaster.Rows[i]["remarks"].ToString() + "','" + clientbillchargesmaster.Rows[i]["value"].ToString() + "','" + clientbillchargesmaster.Rows[i]["at"].ToString() + "','" + clientbillchargesmaster.Rows[i]["plusminus"].ToString() + "','" + clientbillchargesmaster.Rows[i]["amount"].ToString() + "','" + clientbillchargesmaster.Rows[i]["billtype"].ToString() + "','" + clientbillchargesmaster.Rows[i]["isactive"].ToString() + "','" + clientbillchargesmaster.Rows[i]["valueofexp"].ToString() + "','" + clientbillchargesmaster.Rows[i]["tax"].ToString() + "','" + clientbillchargesmaster.Rows[i]["sgst"].ToString() + "','" + clientbillchargesmaster.Rows[i]["cgst"].ToString() + "','" + clientbillchargesmaster.Rows[i]["igst"].ToString() + "','" + clientbillchargesmaster.Rows[i]["additax"].ToString() + "','" + clientbillchargesmaster.Rows[i]["addtaxamt"].ToString() + "','" + clientbillchargesmaster.Rows[i]["billsundryid"].ToString() + "','" + clientbillchargesmaster.Rows[i]["SyncID"].ToString() + "','" + clientbillchargesmaster.Rows[i]["SyncDatetime"].ToString() + "')", con);
                                }
                            }
                        }
                        else
                        {
                            conn.execute("INSERT INTO [dbo].[Billchargesmaster]([billno],[perticulars],[remarks],[value],[at],[plusminus],[amount],[billtype],[isactive],[valueofexp],[tax],[sgst],[cgst],[igst],[additax],[addtaxamt],[billsundryid],[SyncID],[SyncDatetime])VALUES('" + clientbillchargesmaster.Rows[i]["billno"].ToString() + "','" + clientbillchargesmaster.Rows[i]["perticulars"].ToString() + "','" + clientbillchargesmaster.Rows[i]["remarks"].ToString() + "','" + clientbillchargesmaster.Rows[i]["value"].ToString() + "','" + clientbillchargesmaster.Rows[i]["at"].ToString() + "','" + clientbillchargesmaster.Rows[i]["plusminus"].ToString() + "','" + clientbillchargesmaster.Rows[i]["amount"].ToString() + "','" + clientbillchargesmaster.Rows[i]["billtype"].ToString() + "','" + clientbillchargesmaster.Rows[i]["isactive"].ToString() + "','" + clientbillchargesmaster.Rows[i]["valueofexp"].ToString() + "','" + clientbillchargesmaster.Rows[i]["tax"].ToString() + "','" + clientbillchargesmaster.Rows[i]["sgst"].ToString() + "','" + clientbillchargesmaster.Rows[i]["cgst"].ToString() + "','" + clientbillchargesmaster.Rows[i]["igst"].ToString() + "','" + clientbillchargesmaster.Rows[i]["additax"].ToString() + "','" + clientbillchargesmaster.Rows[i]["addtaxamt"].ToString() + "','" + clientbillchargesmaster.Rows[i]["billsundryid"].ToString() + "','" + clientbillchargesmaster.Rows[i]["SyncID"].ToString() + "','" + clientbillchargesmaster.Rows[i]["SyncDatetime"].ToString() + "')", con);
                        }
                    }
                }
                if (serverbillchargesmaster.Rows.Count > 0)
                {
                    for (int i = 0; i < serverbillchargesmaster.Rows.Count; i++)
                    {
                        DataTable isexist = conn.getdataset("Select * from Billchargesmaster where SyncID='" + serverbillchargesmaster.Rows[i]["SyncID"].ToString() + "'");
                        if (isexist.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(isexist.Rows[0]["SyncID"].ToString()))
                            {
                                string Server = serverbillchargesmaster.Rows[i]["SyncDatetime"].ToString();
                                string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Update in local
                                    conn.execute("UPDATE [dbo].[Billchargesmaster] SET [billno] = '" + serverbillchargesmaster.Rows[i]["billno"].ToString() + "',[perticulars] = '" + serverbillchargesmaster.Rows[i]["perticulars"].ToString() + "',[remarks] = '" + serverbillchargesmaster.Rows[i]["remarks"].ToString() + "',[value] = '" + serverbillchargesmaster.Rows[i]["value"].ToString() + "',[at] = '" + serverbillchargesmaster.Rows[i]["at"].ToString() + "',[plusminus] = '" + serverbillchargesmaster.Rows[i]["plusminus"].ToString() + "',[amount] = '" + serverbillchargesmaster.Rows[i]["amount"].ToString() + "',[billtype] = '" + serverbillchargesmaster.Rows[i]["billtype"].ToString() + "',[isactive] = '" + serverbillchargesmaster.Rows[i]["isactive"].ToString() + "',[valueofexp] = '" + serverbillchargesmaster.Rows[i]["valueofexp"].ToString() + "',[tax] = '" + serverbillchargesmaster.Rows[i]["tax"].ToString() + "',[sgst] = '" + serverbillchargesmaster.Rows[i]["sgst"].ToString() + "',[cgst] = '" + serverbillchargesmaster.Rows[i]["cgst"].ToString() + "',[igst] = '" + serverbillchargesmaster.Rows[i]["igst"].ToString() + "',[additax] ='" + serverbillchargesmaster.Rows[i]["additax"].ToString() + "',[addtaxamt] = '" + serverbillchargesmaster.Rows[i]["addtaxamt"].ToString() + "',[billsundryid] = '" + serverbillchargesmaster.Rows[i]["billsundryid"].ToString() + "',[SyncID] = '" + serverbillchargesmaster.Rows[i]["SyncID"].ToString() + "',[SyncDatetime] = '" + serverbillchargesmaster.Rows[i]["SyncDatetime"].ToString() + "' WHERE SyncID='" + serverbillchargesmaster.Rows[i]["SyncID"].ToString() + "'");
                                }
                                else
                                {
                                    //Update in Server
                                    conn.execute("UPDATE [dbo].[Billchargesmaster] SET [billno] = '" + isexist.Rows[0]["billno"].ToString() + "',[perticulars] = '" + isexist.Rows[0]["perticulars"].ToString() + "',[remarks] = '" + isexist.Rows[0]["remarks"].ToString() + "',[value] = '" + isexist.Rows[0]["value"].ToString() + "',[at] = '" + isexist.Rows[0]["at"].ToString() + "',[plusminus] = '" + isexist.Rows[0]["plusminus"].ToString() + "',[amount] = '" + isexist.Rows[0]["amount"].ToString() + "',[billtype] = '" + isexist.Rows[0]["billtype"].ToString() + "',[isactive] = '" + isexist.Rows[0]["isactive"].ToString() + "',[valueofexp] = '" + isexist.Rows[0]["valueofexp"].ToString() + "',[tax] = '" + isexist.Rows[0]["tax"].ToString() + "',[sgst] = '" + isexist.Rows[0]["sgst"].ToString() + "',[cgst] = '" + isexist.Rows[0]["cgst"].ToString() + "',[igst] = '" + isexist.Rows[0]["igst"].ToString() + "',[additax] ='" + isexist.Rows[0]["additax"].ToString() + "',[addtaxamt] = '" + isexist.Rows[0]["addtaxamt"].ToString() + "',[billsundryid] = '" + isexist.Rows[0]["billsundryid"].ToString() + "',[SyncID] = '" + isexist.Rows[0]["SyncID"].ToString() + "',[SyncDatetime] = '" + isexist.Rows[0]["SyncDatetime"].ToString() + "' WHERE SyncID='" + isexist.Rows[0]["SyncID"].ToString() + "'", con);
                                }
                            }
                            else
                            {
                                string Server = serverbillchargesmaster.Rows[i]["SyncDatetime"].ToString();
                                string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = DateTime.ParseExact(local, Master.dateformate, en);
                                DateTime serversyncdatetime = DateTime.ParseExact(Server, Master.dateformate, en);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Insert in local
                                    conn.execute("INSERT INTO [dbo].[Billchargesmaster]([billno],[perticulars],[remarks],[value],[at],[plusminus],[amount],[billtype],[isactive],[valueofexp],[tax],[sgst],[cgst],[igst],[additax],[addtaxamt],[billsundryid],[SyncID],[SyncDatetime])VALUES('" + serverbillchargesmaster.Rows[i]["billno"].ToString() + "','" + serverbillchargesmaster.Rows[i]["perticulars"].ToString() + "','" + serverbillchargesmaster.Rows[i]["remarks"].ToString() + "','" + serverbillchargesmaster.Rows[i]["value"].ToString() + "','" + serverbillchargesmaster.Rows[i]["at"].ToString() + "','" + serverbillchargesmaster.Rows[i]["plusminus"].ToString() + "','" + serverbillchargesmaster.Rows[i]["amount"].ToString() + "','" + serverbillchargesmaster.Rows[i]["billtype"].ToString() + "','" + serverbillchargesmaster.Rows[i]["isactive"].ToString() + "','" + serverbillchargesmaster.Rows[i]["valueofexp"].ToString() + "','" + serverbillchargesmaster.Rows[i]["tax"].ToString() + "','" + serverbillchargesmaster.Rows[i]["sgst"].ToString() + "','" + serverbillchargesmaster.Rows[i]["cgst"].ToString() + "','" + serverbillchargesmaster.Rows[i]["igst"].ToString() + "','" + serverbillchargesmaster.Rows[i]["additax"].ToString() + "','" + serverbillchargesmaster.Rows[i]["addtaxamt"].ToString() + "','" + serverbillchargesmaster.Rows[i]["billsundryid"].ToString() + "','" + serverbillchargesmaster.Rows[i]["SyncID"].ToString() + "','" + serverbillchargesmaster.Rows[i]["SyncDatetime"].ToString() + "')");
                                }
                                else
                                {
                                    //Insert in Server
                                    //  conn.execute("INSERT INTO [dbo].[BillMaster]([Bill_No],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[BillType],[billno],[totaladdtax],[roudoff],[Duedate],[totalaqty],[totalfree],[totaldiscount],[totaladddiscount],[totalamount],[totalservicejob],[totalcharges],[Delieveryat],[fraight],[vehicleno],[grrrno],[noofskids],[sgstamt],[cgatamt],[igstamt],[OrderStatus],[refno],[totalcess],[agentID],[originalbillno],[originalbilldate],[SyncID],[SyncDatetime])VALUES('" + isexist.Rows[0]["Bill_No"].ToString() + "','" + isexist.Rows[0]["Bill_Date"].ToString() + "','" + isexist.Rows[0]["Terms"].ToString() + "','" + isexist.Rows[0]["ClientID"].ToString() + "','" + isexist.Rows[0]["PO_No"].ToString() + "','" + isexist.Rows[0]["SaleType"].ToString() + "','" + isexist.Rows[0]["count"].ToString() + "','" + isexist.Rows[0]["totalqty"].ToString() + "','" + isexist.Rows[0]["totalbasic"].ToString() + "','" + isexist.Rows[0]["totaltax"].ToString() + "','" + isexist.Rows[0]["totalnet"].ToString() + "','" + isexist.Rows[0]["isactive"].ToString() + "','" + isexist.Rows[0]["apprweight"].ToString() + "','" + isexist.Rows[0]["dispatchdetails"].ToString() + "','" + isexist.Rows[0]["remarks"].ToString() + "','" + isexist.Rows[0]["BillType"].ToString() + "','" + isexist.Rows[0]["billno"].ToString() + "','" + isexist.Rows[0]["totaladdtax"].ToString() + "','" + isexist.Rows[0]["roudoff"].ToString() + "','" + isexist.Rows[0]["Duedate"].ToString() + "','" + isexist.Rows[0]["totalaqty"].ToString() + "','" + isexist.Rows[0]["totalfree"].ToString() + "','" + isexist.Rows[0]["totaldiscount"].ToString() + "','" + isexist.Rows[0]["totaladddiscount"].ToString() + "','" + isexist.Rows[0]["totalamount"].ToString() + "','" + isexist.Rows[0]["totalservicejob"].ToString() + "','" + isexist.Rows[0]["totalcharges"].ToString() + "','" + isexist.Rows[0]["Delieveryat"].ToString() + "','" + isexist.Rows[0]["fraight"].ToString() + "','" + isexist.Rows[0]["vehicleno"].ToString() + "','" + isexist.Rows[0]["grrrno"].ToString() + "','" + isexist.Rows[0]["noofskids"].ToString() + "','" + isexist.Rows[0]["sgstamt"].ToString() + "','" + isexist.Rows[0]["cgatamt"].ToString() + "','" + isexist.Rows[0]["igstamt"].ToString() + "','" + isexist.Rows[0]["OrderStatus"].ToString() + "','" + isexist.Rows[0]["refno"].ToString() + "','" + isexist.Rows[0]["totalcess"].ToString() + "','" + isexist.Rows[0]["agentID"].ToString() + "','" + isexist.Rows[0]["originalbillno"].ToString() + "','" + isexist.Rows[0]["originalbilldate"].ToString() + "','" + isexist.Rows[0]["SyncID"].ToString() + "','" + isexist.Rows[0]["SyncDatetime"].ToString() + "')", con);
                                    conn.execute("INSERT INTO [dbo].[Billchargesmaster]([billno],[perticulars],[remarks],[value],[at],[plusminus],[amount],[billtype],[isactive],[valueofexp],[tax],[sgst],[cgst],[igst],[additax],[addtaxamt],[billsundryid],[SyncID],[SyncDatetime])VALUES('" + isexist.Rows[0]["billno"].ToString() + "','" + isexist.Rows[0]["perticulars"].ToString() + "','" + isexist.Rows[0]["remarks"].ToString() + "','" + isexist.Rows[0]["value"].ToString() + "','" + isexist.Rows[0]["at"].ToString() + "','" + isexist.Rows[0]["plusminus"].ToString() + "','" + isexist.Rows[0]["amount"].ToString() + "','" + isexist.Rows[0]["billtype"].ToString() + "','" + isexist.Rows[0]["isactive"].ToString() + "','" + isexist.Rows[0]["valueofexp"].ToString() + "','" + isexist.Rows[0]["tax"].ToString() + "','" + isexist.Rows[0]["sgst"].ToString() + "','" + isexist.Rows[0]["cgst"].ToString() + "','" + isexist.Rows[0]["igst"].ToString() + "','" + isexist.Rows[0]["additax"].ToString() + "','" + isexist.Rows[0]["addtaxamt"].ToString() + "','" + isexist.Rows[0]["billsundryid"].ToString() + "','" + isexist.Rows[0]["SyncID"].ToString() + "','" + isexist.Rows[0]["SyncDatetime"].ToString() + "')", con);
                                }
                            }
                        }
                        else
                        {
                            conn.execute("INSERT INTO [dbo].[Billchargesmaster]([billno],[perticulars],[remarks],[value],[at],[plusminus],[amount],[billtype],[isactive],[valueofexp],[tax],[sgst],[cgst],[igst],[additax],[addtaxamt],[billsundryid],[SyncID],[SyncDatetime])VALUES('" + serverbillchargesmaster.Rows[i]["billno"].ToString() + "','" + serverbillchargesmaster.Rows[i]["perticulars"].ToString() + "','" + serverbillchargesmaster.Rows[i]["remarks"].ToString() + "','" + serverbillchargesmaster.Rows[i]["value"].ToString() + "','" + serverbillchargesmaster.Rows[i]["at"].ToString() + "','" + serverbillchargesmaster.Rows[i]["plusminus"].ToString() + "','" + serverbillchargesmaster.Rows[i]["amount"].ToString() + "','" + serverbillchargesmaster.Rows[i]["billtype"].ToString() + "','" + serverbillchargesmaster.Rows[i]["isactive"].ToString() + "','" + serverbillchargesmaster.Rows[i]["valueofexp"].ToString() + "','" + serverbillchargesmaster.Rows[i]["tax"].ToString() + "','" + serverbillchargesmaster.Rows[i]["sgst"].ToString() + "','" + serverbillchargesmaster.Rows[i]["cgst"].ToString() + "','" + serverbillchargesmaster.Rows[i]["igst"].ToString() + "','" + serverbillchargesmaster.Rows[i]["additax"].ToString() + "','" + serverbillchargesmaster.Rows[i]["addtaxamt"].ToString() + "','" + serverbillchargesmaster.Rows[i]["billsundryid"].ToString() + "','" + serverbillchargesmaster.Rows[i]["SyncID"].ToString() + "','" + serverbillchargesmaster.Rows[i]["SyncDatetime"].ToString() + "')");
                        }
                    }
                }
                #endregion

                //SaleOrderMaster
                #region
                DataTable clientsaleordermaster = conn.getdataset("Select * from SaleOrderMaster where SyncDatetime >'" + lastsyncdatetime.ToString("MM/dd/yyyy h:mm:ss") + "'");
                DataTable serversaleordermaster = conn.getdataset("Select * from SaleOrderMaster where SyncDatetime >'" + lastsyncdatetime.ToString("MM/dd/yyyy h:mm:ss") + "'", con);
                if (clientsaleordermaster.Rows.Count > 0)
                {
                    for (int i = 0; i < clientsaleordermaster.Rows.Count; i++)
                    {
                        DataTable isexist = conn.getdataset("Select * from SaleOrderMaster where SyncID='" + clientsaleordermaster.Rows[i]["SyncID"].ToString() + "'", con);
                        if (isexist.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(isexist.Rows[0]["SyncID"].ToString()))
                            {
                                string local = clientsaleordermaster.Rows[i]["SyncDatetime"].ToString();
                                string Server = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Update in local
                                    conn.execute("UPDATE [dbo].[SaleOrderMaster] SET [Bill_No] ='" + isexist.Rows[0]["Bill_No"].ToString() + "',[Bill_Date] = '" + isexist.Rows[0]["Bill_Date"].ToString() + "',[Terms] = '" + isexist.Rows[0]["Terms"].ToString() + "',[ClientID] = '" + isexist.Rows[0]["ClientID"].ToString() + "',[PO_No] = '" + isexist.Rows[0]["PO_No"].ToString() + "',[SaleType] = '" + isexist.Rows[0]["SaleType"].ToString() + "',[count] = '" + isexist.Rows[0]["count"].ToString() + "',[totalqty] = '" + isexist.Rows[0]["totalqty"].ToString() + "',[totalbasic] = '" + isexist.Rows[0]["totalbasic"].ToString() + "',[totaltax] = '" + isexist.Rows[0]["totaltax"].ToString() + "',[totalnet] = '" + isexist.Rows[0]["totalnet"].ToString() + "',[isactive] = '" + isexist.Rows[0]["isactive"].ToString() + "',[apprweight] = '" + isexist.Rows[0]["apprweight"].ToString() + "',[dispatchdetails] = '" + isexist.Rows[0]["dispatchdetails"].ToString() + "',[remarks] = '" + isexist.Rows[0]["remarks"].ToString() + "',[BillType] = '" + isexist.Rows[0]["BillType"].ToString() + "',[billno] = '" + isexist.Rows[0]["billno"].ToString() + "',[totaladdtax] = '" + isexist.Rows[0]["totaladdtax"].ToString() + "',[roudoff] = '" + isexist.Rows[0]["roudoff"].ToString() + "',[Duedate] = '" + isexist.Rows[0]["Duedate"].ToString() + "',[totalaqty] = '" + isexist.Rows[0]["totalaqty"].ToString() + "',[totalfree] = '" + isexist.Rows[0]["totalfree"].ToString() + "',[totaldiscount] = '" + isexist.Rows[0]["totaldiscount"].ToString() + "',[totaladddiscount] = '" + isexist.Rows[0]["totaladddiscount"].ToString() + "',[totalamount] = '" + isexist.Rows[0]["totalamount"].ToString() + "',[totalservicejob] = '" + isexist.Rows[0]["totalservicejob"].ToString() + "',[totalcharges] = '" + isexist.Rows[0]["totalcharges"].ToString() + "',[Delieveryat] = '" + isexist.Rows[0]["Delieveryat"].ToString() + "',[fraight] = '" + isexist.Rows[0]["fraight"].ToString() + "',[vehicleno] = '" + isexist.Rows[0]["vehicleno"].ToString() + "',[grrrno] = '" + isexist.Rows[0]["grrrno"].ToString() + "',[noofskids] = '" + isexist.Rows[0]["noofskids"].ToString() + "',[sgstamt] = '" + isexist.Rows[0]["sgstamt"].ToString() + "',[cgatamt] = '" + isexist.Rows[0]["cgatamt"].ToString() + "',[igstamt] = '" + isexist.Rows[0]["igstamt"].ToString() + "',[OrderStatus] = '" + isexist.Rows[0]["OrderStatus"].ToString() + "',[refno] = '" + isexist.Rows[0]["refno"].ToString() + "',[totalcess] = '" + isexist.Rows[0]["totalcess"].ToString() + "',[agentID] = '" + isexist.Rows[0]["agentID"].ToString() + "',[SyncID] = '" + isexist.Rows[0]["SyncID"].ToString() + "',[SyncDatetime] = '" + isexist.Rows[0]["SyncDatetime"].ToString() + "' WHERE SyncID='" + isexist.Rows[0]["SyncID"].ToString() + "'");
                                }
                                else
                                {
                                    //Update in Server
                                    conn.execute("UPDATE [dbo].[SaleOrderMaster] SET [Bill_No] ='" + clientsaleordermaster.Rows[i]["Bill_No"].ToString() + "',[Bill_Date] = '" + clientsaleordermaster.Rows[i]["Bill_Date"].ToString() + "',[Terms] = '" + clientsaleordermaster.Rows[i]["Terms"].ToString() + "',[ClientID] = '" + clientsaleordermaster.Rows[i]["ClientID"].ToString() + "',[PO_No] = '" + clientsaleordermaster.Rows[i]["PO_No"].ToString() + "',[SaleType] = '" + clientsaleordermaster.Rows[i]["SaleType"].ToString() + "',[count] = '" + clientsaleordermaster.Rows[i]["count"].ToString() + "',[totalqty] = '" + clientsaleordermaster.Rows[i]["totalqty"].ToString() + "',[totalbasic] = '" + clientsaleordermaster.Rows[i]["totalbasic"].ToString() + "',[totaltax] = '" + clientsaleordermaster.Rows[i]["totaltax"].ToString() + "',[totalnet] = '" + clientsaleordermaster.Rows[i]["totalnet"].ToString() + "',[isactive] = '" + clientsaleordermaster.Rows[i]["isactive"].ToString() + "',[apprweight] = '" + clientsaleordermaster.Rows[i]["apprweight"].ToString() + "',[dispatchdetails] = '" + clientsaleordermaster.Rows[i]["dispatchdetails"].ToString() + "',[remarks] = '" + clientsaleordermaster.Rows[i]["remarks"].ToString() + "',[BillType] = '" + clientsaleordermaster.Rows[i]["BillType"].ToString() + "',[billno] = '" + clientsaleordermaster.Rows[i]["billno"].ToString() + "',[totaladdtax] = '" + clientsaleordermaster.Rows[i]["totaladdtax"].ToString() + "',[roudoff] = '" + clientsaleordermaster.Rows[i]["roudoff"].ToString() + "',[Duedate] = '" + clientsaleordermaster.Rows[i]["Duedate"].ToString() + "',[totalaqty] = '" + clientsaleordermaster.Rows[i]["totalaqty"].ToString() + "',[totalfree] = '" + clientsaleordermaster.Rows[i]["totalfree"].ToString() + "',[totaldiscount] = '" + clientsaleordermaster.Rows[i]["totaldiscount"].ToString() + "',[totaladddiscount] = '" + clientsaleordermaster.Rows[i]["totaladddiscount"].ToString() + "',[totalamount] = '" + clientsaleordermaster.Rows[i]["totalamount"].ToString() + "',[totalservicejob] = '" + clientsaleordermaster.Rows[i]["totalservicejob"].ToString() + "',[totalcharges] = '" + clientsaleordermaster.Rows[i]["totalcharges"].ToString() + "',[Delieveryat] = '" + clientsaleordermaster.Rows[i]["Delieveryat"].ToString() + "',[fraight] = '" + clientsaleordermaster.Rows[i]["fraight"].ToString() + "',[vehicleno] = '" + clientsaleordermaster.Rows[i]["vehicleno"].ToString() + "',[grrrno] = '" + clientsaleordermaster.Rows[i]["grrrno"].ToString() + "',[noofskids] = '" + clientsaleordermaster.Rows[i]["noofskids"].ToString() + "',[sgstamt] = '" + clientsaleordermaster.Rows[i]["sgstamt"].ToString() + "',[cgatamt] = '" + clientsaleordermaster.Rows[i]["cgatamt"].ToString() + "',[igstamt] = '" + clientsaleordermaster.Rows[i]["igstamt"].ToString() + "',[OrderStatus] = '" + clientsaleordermaster.Rows[i]["OrderStatus"].ToString() + "',[refno] = '" + clientsaleordermaster.Rows[i]["refno"].ToString() + "',[totalcess] = '" + clientsaleordermaster.Rows[i]["totalcess"].ToString() + "',[agentID] = '" + clientsaleordermaster.Rows[i]["agentID"].ToString() + "',[SyncID] = '" + clientsaleordermaster.Rows[i]["SyncID"].ToString() + "',[SyncDatetime] = '" + clientsaleordermaster.Rows[i]["SyncDatetime"].ToString() + "' WHERE SyncID='" + clientsaleordermaster.Rows[i]["SyncID"].ToString() + "'", con);
                                }
                            }
                            else
                            {
                                string local = clientsaleordermaster.Rows[i]["SyncDatetime"].ToString();
                                string Server = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Insert in local
                                    conn.execute("INSERT INTO [dbo].[SaleOrderMaster]([Bill_No],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[BillType],[billno],[totaladdtax],[roudoff],[Duedate],[totalaqty],[totalfree],[totaldiscount],[totaladddiscount],[totalamount],[totalservicejob],[totalcharges],[Delieveryat],[fraight],[vehicleno],[grrrno],[noofskids],[sgstamt],[cgatamt],[igstamt],[OrderStatus],[refno],[totalcess],[agentID],[SyncID],[SyncDatetime])VALUES('" + isexist.Rows[0]["Bill_No"].ToString() + "','" + isexist.Rows[0]["Bill_Date"].ToString() + "','" + isexist.Rows[0]["Terms"].ToString() + "','" + isexist.Rows[0]["ClientID"].ToString() + "','" + isexist.Rows[0]["PO_No"].ToString() + "','" + isexist.Rows[0]["SaleType"].ToString() + "','" + isexist.Rows[0]["count"].ToString() + "','" + isexist.Rows[0]["totalqty"].ToString() + "','" + isexist.Rows[0]["totalbasic"].ToString() + "','" + isexist.Rows[0]["totaltax"].ToString() + "','" + isexist.Rows[0]["totalnet"].ToString() + "','" + isexist.Rows[0]["isactive"].ToString() + "','" + isexist.Rows[0]["apprweight"].ToString() + "','" + isexist.Rows[0]["dispatchdetails"].ToString() + "','" + isexist.Rows[0]["remarks"].ToString() + "','" + isexist.Rows[0]["BillType"].ToString() + "','" + isexist.Rows[0]["billno"].ToString() + "','" + isexist.Rows[0]["totaladdtax"].ToString() + "','" + isexist.Rows[0]["roudoff"].ToString() + "','" + isexist.Rows[0]["Duedate"].ToString() + "','" + isexist.Rows[0]["totalaqty"].ToString() + "','" + isexist.Rows[0]["totalfree"].ToString() + "','" + isexist.Rows[0]["totaldiscount"].ToString() + "','" + isexist.Rows[0]["totaladddiscount"].ToString() + "','" + isexist.Rows[0]["totalamount"].ToString() + "','" + isexist.Rows[0]["totalservicejob"].ToString() + "','" + isexist.Rows[0]["totalcharges"].ToString() + "','" + isexist.Rows[0]["Delieveryat"].ToString() + "','" + isexist.Rows[0]["fraight"].ToString() + "','" + isexist.Rows[0]["vehicleno"].ToString() + "','" + isexist.Rows[0]["grrrno"].ToString() + "','" + isexist.Rows[0]["noofskids"].ToString() + "','" + isexist.Rows[0]["sgstamt"].ToString() + "','" + isexist.Rows[0]["cgatamt"].ToString() + "','" + isexist.Rows[0]["igstamt"].ToString() + "','" + isexist.Rows[0]["OrderStatus"].ToString() + "','" + isexist.Rows[0]["refno"].ToString() + "','" + isexist.Rows[0]["totalcess"].ToString() + "','" + isexist.Rows[0]["agentID"].ToString() + "','" + isexist.Rows[0]["SyncID"].ToString() + "','" + isexist.Rows[0]["SyncDatetime"].ToString() + "')");

                                }
                                else
                                {
                                    //Insert in Server
                                    conn.execute("INSERT INTO [dbo].[SaleOrderMaster]([Bill_No],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[BillType],[billno],[totaladdtax],[roudoff],[Duedate],[totalaqty],[totalfree],[totaldiscount],[totaladddiscount],[totalamount],[totalservicejob],[totalcharges],[Delieveryat],[fraight],[vehicleno],[grrrno],[noofskids],[sgstamt],[cgatamt],[igstamt],[OrderStatus],[refno],[totalcess],[agentID],[SyncID],[SyncDatetime])VALUES('" + clientsaleordermaster.Rows[i]["Bill_No"].ToString() + "','" + clientsaleordermaster.Rows[i]["Bill_Date"].ToString() + "','" + clientsaleordermaster.Rows[i]["Terms"].ToString() + "','" + clientsaleordermaster.Rows[i]["ClientID"].ToString() + "','" + clientsaleordermaster.Rows[i]["PO_No"].ToString() + "','" + clientsaleordermaster.Rows[i]["SaleType"].ToString() + "','" + clientsaleordermaster.Rows[i]["count"].ToString() + "','" + clientsaleordermaster.Rows[i]["totalqty"].ToString() + "','" + clientsaleordermaster.Rows[i]["totalbasic"].ToString() + "','" + clientsaleordermaster.Rows[i]["totaltax"].ToString() + "','" + clientsaleordermaster.Rows[i]["totalnet"].ToString() + "','" + clientsaleordermaster.Rows[i]["isactive"].ToString() + "','" + clientsaleordermaster.Rows[i]["apprweight"].ToString() + "','" + clientsaleordermaster.Rows[i]["dispatchdetails"].ToString() + "','" + clientsaleordermaster.Rows[i]["remarks"].ToString() + "','" + clientsaleordermaster.Rows[i]["BillType"].ToString() + "','" + clientsaleordermaster.Rows[i]["billno"].ToString() + "','" + clientsaleordermaster.Rows[i]["totaladdtax"].ToString() + "','" + clientsaleordermaster.Rows[i]["roudoff"].ToString() + "','" + clientsaleordermaster.Rows[i]["Duedate"].ToString() + "','" + clientsaleordermaster.Rows[i]["totalaqty"].ToString() + "','" + clientsaleordermaster.Rows[i]["totalfree"].ToString() + "','" + clientsaleordermaster.Rows[i]["totaldiscount"].ToString() + "','" + clientsaleordermaster.Rows[i]["totaladddiscount"].ToString() + "','" + clientsaleordermaster.Rows[i]["totalamount"].ToString() + "','" + clientsaleordermaster.Rows[i]["totalservicejob"].ToString() + "','" + clientsaleordermaster.Rows[i]["totalcharges"].ToString() + "','" + clientsaleordermaster.Rows[i]["Delieveryat"].ToString() + "','" + clientsaleordermaster.Rows[i]["fraight"].ToString() + "','" + clientsaleordermaster.Rows[i]["vehicleno"].ToString() + "','" + clientsaleordermaster.Rows[i]["grrrno"].ToString() + "','" + clientsaleordermaster.Rows[i]["noofskids"].ToString() + "','" + clientsaleordermaster.Rows[i]["sgstamt"].ToString() + "','" + clientsaleordermaster.Rows[i]["cgatamt"].ToString() + "','" + clientsaleordermaster.Rows[i]["igstamt"].ToString() + "','" + clientsaleordermaster.Rows[i]["OrderStatus"].ToString() + "','" + clientsaleordermaster.Rows[i]["refno"].ToString() + "','" + clientsaleordermaster.Rows[i]["totalcess"].ToString() + "','" + clientsaleordermaster.Rows[i]["agentID"].ToString() + "','" + clientsaleordermaster.Rows[i]["SyncID"].ToString() + "','" + clientsaleordermaster.Rows[i]["SyncDatetime"].ToString() + "')", con);
                                }
                            }
                        }
                        else
                        {
                            conn.execute("INSERT INTO [dbo].[SaleOrderMaster]([Bill_No],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[BillType],[billno],[totaladdtax],[roudoff],[Duedate],[totalaqty],[totalfree],[totaldiscount],[totaladddiscount],[totalamount],[totalservicejob],[totalcharges],[Delieveryat],[fraight],[vehicleno],[grrrno],[noofskids],[sgstamt],[cgatamt],[igstamt],[OrderStatus],[refno],[totalcess],[agentID],[SyncID],[SyncDatetime])VALUES('" + clientsaleordermaster.Rows[i]["Bill_No"].ToString() + "','" + clientsaleordermaster.Rows[i]["Bill_Date"].ToString() + "','" + clientsaleordermaster.Rows[i]["Terms"].ToString() + "','" + clientsaleordermaster.Rows[i]["ClientID"].ToString() + "','" + clientsaleordermaster.Rows[i]["PO_No"].ToString() + "','" + clientsaleordermaster.Rows[i]["SaleType"].ToString() + "','" + clientsaleordermaster.Rows[i]["count"].ToString() + "','" + clientsaleordermaster.Rows[i]["totalqty"].ToString() + "','" + clientsaleordermaster.Rows[i]["totalbasic"].ToString() + "','" + clientsaleordermaster.Rows[i]["totaltax"].ToString() + "','" + clientsaleordermaster.Rows[i]["totalnet"].ToString() + "','" + clientsaleordermaster.Rows[i]["isactive"].ToString() + "','" + clientsaleordermaster.Rows[i]["apprweight"].ToString() + "','" + clientsaleordermaster.Rows[i]["dispatchdetails"].ToString() + "','" + clientsaleordermaster.Rows[i]["remarks"].ToString() + "','" + clientsaleordermaster.Rows[i]["BillType"].ToString() + "','" + clientsaleordermaster.Rows[i]["billno"].ToString() + "','" + clientsaleordermaster.Rows[i]["totaladdtax"].ToString() + "','" + clientsaleordermaster.Rows[i]["roudoff"].ToString() + "','" + clientsaleordermaster.Rows[i]["Duedate"].ToString() + "','" + clientsaleordermaster.Rows[i]["totalaqty"].ToString() + "','" + clientsaleordermaster.Rows[i]["totalfree"].ToString() + "','" + clientsaleordermaster.Rows[i]["totaldiscount"].ToString() + "','" + clientsaleordermaster.Rows[i]["totaladddiscount"].ToString() + "','" + clientsaleordermaster.Rows[i]["totalamount"].ToString() + "','" + clientsaleordermaster.Rows[i]["totalservicejob"].ToString() + "','" + clientsaleordermaster.Rows[i]["totalcharges"].ToString() + "','" + clientsaleordermaster.Rows[i]["Delieveryat"].ToString() + "','" + clientsaleordermaster.Rows[i]["fraight"].ToString() + "','" + clientsaleordermaster.Rows[i]["vehicleno"].ToString() + "','" + clientsaleordermaster.Rows[i]["grrrno"].ToString() + "','" + clientsaleordermaster.Rows[i]["noofskids"].ToString() + "','" + clientsaleordermaster.Rows[i]["sgstamt"].ToString() + "','" + clientsaleordermaster.Rows[i]["cgatamt"].ToString() + "','" + clientsaleordermaster.Rows[i]["igstamt"].ToString() + "','" + clientsaleordermaster.Rows[i]["OrderStatus"].ToString() + "','" + clientsaleordermaster.Rows[i]["refno"].ToString() + "','" + clientsaleordermaster.Rows[i]["totalcess"].ToString() + "','" + clientsaleordermaster.Rows[i]["agentID"].ToString() + "','" + clientsaleordermaster.Rows[i]["SyncID"].ToString() + "','" + clientsaleordermaster.Rows[i]["SyncDatetime"].ToString() + "')", con);
                        }
                    }
                }
                if (serversaleordermaster.Rows.Count > 0)
                {
                    for (int i = 0; i < serversaleordermaster.Rows.Count; i++)
                    {
                        DataTable isexist = conn.getdataset("Select * from SaleOrderMaster where SyncID='" + serversaleordermaster.Rows[i]["SyncID"].ToString() + "'");
                        if (isexist.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(isexist.Rows[0]["SyncID"].ToString()))
                            {
                                string Server = serversaleordermaster.Rows[i]["SyncDatetime"].ToString();
                                string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Update in local

                                    conn.execute("UPDATE [dbo].[SaleOrderMaster] SET [Bill_No] ='" + serversaleordermaster.Rows[i]["Bill_No"].ToString() + "',[Bill_Date] = '" + serversaleordermaster.Rows[i]["Bill_Date"].ToString() + "',[Terms] = '" + serversaleordermaster.Rows[i]["Terms"].ToString() + "',[ClientID] = '" + serversaleordermaster.Rows[i]["ClientID"].ToString() + "',[PO_No] = '" + serversaleordermaster.Rows[i]["PO_No"].ToString() + "',[SaleType] = '" + serversaleordermaster.Rows[i]["SaleType"].ToString() + "',[count] = '" + serversaleordermaster.Rows[i]["count"].ToString() + "',[totalqty] = '" + serversaleordermaster.Rows[i]["totalqty"].ToString() + "',[totalbasic] = '" + serversaleordermaster.Rows[i]["totalbasic"].ToString() + "',[totaltax] = '" + serversaleordermaster.Rows[i]["totaltax"].ToString() + "',[totalnet] = '" + serversaleordermaster.Rows[i]["totalnet"].ToString() + "',[isactive] = '" + serversaleordermaster.Rows[i]["isactive"].ToString() + "',[apprweight] = '" + serversaleordermaster.Rows[i]["apprweight"].ToString() + "',[dispatchdetails] = '" + serversaleordermaster.Rows[i]["dispatchdetails"].ToString() + "',[remarks] = '" + serversaleordermaster.Rows[i]["remarks"].ToString() + "',[BillType] = '" + serversaleordermaster.Rows[i]["BillType"].ToString() + "',[billno] = '" + serversaleordermaster.Rows[i]["billno"].ToString() + "',[totaladdtax] = '" + serversaleordermaster.Rows[i]["totaladdtax"].ToString() + "',[roudoff] = '" + serversaleordermaster.Rows[i]["roudoff"].ToString() + "',[Duedate] = '" + serversaleordermaster.Rows[i]["Duedate"].ToString() + "',[totalaqty] = '" + serversaleordermaster.Rows[i]["totalaqty"].ToString() + "',[totalfree] = '" + serversaleordermaster.Rows[i]["totalfree"].ToString() + "',[totaldiscount] = '" + serversaleordermaster.Rows[i]["totaldiscount"].ToString() + "',[totaladddiscount] = '" + serversaleordermaster.Rows[i]["totaladddiscount"].ToString() + "',[totalamount] = '" + serversaleordermaster.Rows[i]["totalamount"].ToString() + "',[totalservicejob] = '" + serversaleordermaster.Rows[i]["totalservicejob"].ToString() + "',[totalcharges] = '" + serversaleordermaster.Rows[i]["totalcharges"].ToString() + "',[Delieveryat] = '" + serversaleordermaster.Rows[i]["Delieveryat"].ToString() + "',[fraight] = '" + serversaleordermaster.Rows[i]["fraight"].ToString() + "',[vehicleno] = '" + serversaleordermaster.Rows[i]["vehicleno"].ToString() + "',[grrrno] = '" + serversaleordermaster.Rows[i]["grrrno"].ToString() + "',[noofskids] = '" + serversaleordermaster.Rows[i]["noofskids"].ToString() + "',[sgstamt] = '" + serversaleordermaster.Rows[i]["sgstamt"].ToString() + "',[cgatamt] = '" + serversaleordermaster.Rows[i]["cgatamt"].ToString() + "',[igstamt] = '" + serversaleordermaster.Rows[i]["igstamt"].ToString() + "',[OrderStatus] = '" + serversaleordermaster.Rows[i]["OrderStatus"].ToString() + "',[refno] = '" + serversaleordermaster.Rows[i]["refno"].ToString() + "',[totalcess] = '" + serversaleordermaster.Rows[i]["totalcess"].ToString() + "',[agentID] = '" + serversaleordermaster.Rows[i]["agentID"].ToString() + "',[SyncID] = '" + serversaleordermaster.Rows[i]["SyncID"].ToString() + "',[SyncDatetime] = '" + serversaleordermaster.Rows[i]["SyncDatetime"].ToString() + "' WHERE SyncID='" + serversaleordermaster.Rows[i]["SyncID"].ToString() + "'");
                                }
                                else
                                {
                                    //Update in Server
                                    conn.execute("UPDATE [dbo].[SaleOrderMaster] SET [Bill_No] ='" + isexist.Rows[0]["Bill_No"].ToString() + "',[Bill_Date] = '" + isexist.Rows[0]["Bill_Date"].ToString() + "',[Terms] = '" + isexist.Rows[0]["Terms"].ToString() + "',[ClientID] = '" + isexist.Rows[0]["ClientID"].ToString() + "',[PO_No] = '" + isexist.Rows[0]["PO_No"].ToString() + "',[SaleType] = '" + isexist.Rows[0]["SaleType"].ToString() + "',[count] = '" + isexist.Rows[0]["count"].ToString() + "',[totalqty] = '" + isexist.Rows[0]["totalqty"].ToString() + "',[totalbasic] = '" + isexist.Rows[0]["totalbasic"].ToString() + "',[totaltax] = '" + isexist.Rows[0]["totaltax"].ToString() + "',[totalnet] = '" + isexist.Rows[0]["totalnet"].ToString() + "',[isactive] = '" + isexist.Rows[0]["isactive"].ToString() + "',[apprweight] = '" + isexist.Rows[0]["apprweight"].ToString() + "',[dispatchdetails] = '" + isexist.Rows[0]["dispatchdetails"].ToString() + "',[remarks] = '" + isexist.Rows[0]["remarks"].ToString() + "',[BillType] = '" + isexist.Rows[0]["BillType"].ToString() + "',[billno] = '" + isexist.Rows[0]["billno"].ToString() + "',[totaladdtax] = '" + isexist.Rows[0]["totaladdtax"].ToString() + "',[roudoff] = '" + isexist.Rows[0]["roudoff"].ToString() + "',[Duedate] = '" + isexist.Rows[0]["Duedate"].ToString() + "',[totalaqty] = '" + isexist.Rows[0]["totalaqty"].ToString() + "',[totalfree] = '" + isexist.Rows[0]["totalfree"].ToString() + "',[totaldiscount] = '" + isexist.Rows[0]["totaldiscount"].ToString() + "',[totaladddiscount] = '" + isexist.Rows[0]["totaladddiscount"].ToString() + "',[totalamount] = '" + isexist.Rows[0]["totalamount"].ToString() + "',[totalservicejob] = '" + isexist.Rows[0]["totalservicejob"].ToString() + "',[totalcharges] = '" + isexist.Rows[0]["totalcharges"].ToString() + "',[Delieveryat] = '" + isexist.Rows[0]["Delieveryat"].ToString() + "',[fraight] = '" + isexist.Rows[0]["fraight"].ToString() + "',[vehicleno] = '" + isexist.Rows[0]["vehicleno"].ToString() + "',[grrrno] = '" + isexist.Rows[0]["grrrno"].ToString() + "',[noofskids] = '" + isexist.Rows[0]["noofskids"].ToString() + "',[sgstamt] = '" + isexist.Rows[0]["sgstamt"].ToString() + "',[cgatamt] = '" + isexist.Rows[0]["cgatamt"].ToString() + "',[igstamt] = '" + isexist.Rows[0]["igstamt"].ToString() + "',[OrderStatus] = '" + isexist.Rows[0]["OrderStatus"].ToString() + "',[refno] = '" + isexist.Rows[0]["refno"].ToString() + "',[totalcess] = '" + isexist.Rows[0]["totalcess"].ToString() + "',[agentID] = '" + isexist.Rows[0]["agentID"].ToString() + "',[SyncID] = '" + isexist.Rows[0]["SyncID"].ToString() + "',[SyncDatetime] = '" + isexist.Rows[0]["SyncDatetime"].ToString() + "' WHERE SyncID='" + isexist.Rows[0]["SyncID"].ToString() + "'", con);
                                }
                            }
                            else
                            {
                                string Server = serversaleordermaster.Rows[i]["SyncDatetime"].ToString();
                                string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = DateTime.ParseExact(local, Master.dateformate, en);
                                DateTime serversyncdatetime = DateTime.ParseExact(Server, Master.dateformate, en);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Insert in local

                                    conn.execute("INSERT INTO [dbo].[SaleOrderMaster]([Bill_No],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[BillType],[billno],[totaladdtax],[roudoff],[Duedate],[totalaqty],[totalfree],[totaldiscount],[totaladddiscount],[totalamount],[totalservicejob],[totalcharges],[Delieveryat],[fraight],[vehicleno],[grrrno],[noofskids],[sgstamt],[cgatamt],[igstamt],[OrderStatus],[refno],[totalcess],[agentID],[SyncID],[SyncDatetime])VALUES('" + serversaleordermaster.Rows[i]["Bill_No"].ToString() + "','" + serversaleordermaster.Rows[i]["Bill_Date"].ToString() + "','" + serversaleordermaster.Rows[i]["Terms"].ToString() + "','" + serversaleordermaster.Rows[i]["ClientID"].ToString() + "','" + serversaleordermaster.Rows[i]["PO_No"].ToString() + "','" + serversaleordermaster.Rows[i]["SaleType"].ToString() + "','" + serversaleordermaster.Rows[i]["count"].ToString() + "','" + serversaleordermaster.Rows[i]["totalqty"].ToString() + "','" + serversaleordermaster.Rows[i]["totalbasic"].ToString() + "','" + serversaleordermaster.Rows[i]["totaltax"].ToString() + "','" + serversaleordermaster.Rows[i]["totalnet"].ToString() + "','" + serversaleordermaster.Rows[i]["isactive"].ToString() + "','" + serversaleordermaster.Rows[i]["apprweight"].ToString() + "','" + serversaleordermaster.Rows[i]["dispatchdetails"].ToString() + "','" + serversaleordermaster.Rows[i]["remarks"].ToString() + "','" + serversaleordermaster.Rows[i]["BillType"].ToString() + "','" + serversaleordermaster.Rows[i]["billno"].ToString() + "','" + serversaleordermaster.Rows[i]["totaladdtax"].ToString() + "','" + serversaleordermaster.Rows[i]["roudoff"].ToString() + "','" + serversaleordermaster.Rows[i]["Duedate"].ToString() + "','" + serversaleordermaster.Rows[i]["totalaqty"].ToString() + "','" + serversaleordermaster.Rows[i]["totalfree"].ToString() + "','" + serversaleordermaster.Rows[i]["totaldiscount"].ToString() + "','" + serversaleordermaster.Rows[i]["totaladddiscount"].ToString() + "','" + serversaleordermaster.Rows[i]["totalamount"].ToString() + "','" + serversaleordermaster.Rows[i]["totalservicejob"].ToString() + "','" + serversaleordermaster.Rows[i]["totalcharges"].ToString() + "','" + serversaleordermaster.Rows[i]["Delieveryat"].ToString() + "','" + serversaleordermaster.Rows[i]["fraight"].ToString() + "','" + serversaleordermaster.Rows[i]["vehicleno"].ToString() + "','" + serversaleordermaster.Rows[i]["grrrno"].ToString() + "','" + serversaleordermaster.Rows[i]["noofskids"].ToString() + "','" + serversaleordermaster.Rows[i]["sgstamt"].ToString() + "','" + serversaleordermaster.Rows[i]["cgatamt"].ToString() + "','" + serversaleordermaster.Rows[i]["igstamt"].ToString() + "','" + serversaleordermaster.Rows[i]["OrderStatus"].ToString() + "','" + serversaleordermaster.Rows[i]["refno"].ToString() + "','" + serversaleordermaster.Rows[i]["totalcess"].ToString() + "','" + serversaleordermaster.Rows[i]["agentID"].ToString() + "','" + serversaleordermaster.Rows[i]["SyncID"].ToString() + "','" + serversaleordermaster.Rows[i]["SyncDatetime"].ToString() + "')");
                                }
                                else
                                {
                                    //Insert in Server
                                    conn.execute("INSERT INTO [dbo].[SaleOrderMaster]([Bill_No],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[BillType],[billno],[totaladdtax],[roudoff],[Duedate],[totalaqty],[totalfree],[totaldiscount],[totaladddiscount],[totalamount],[totalservicejob],[totalcharges],[Delieveryat],[fraight],[vehicleno],[grrrno],[noofskids],[sgstamt],[cgatamt],[igstamt],[OrderStatus],[refno],[totalcess],[agentID],[SyncID],[SyncDatetime])VALUES('" + isexist.Rows[0]["Bill_No"].ToString() + "','" + isexist.Rows[0]["Bill_Date"].ToString() + "','" + isexist.Rows[0]["Terms"].ToString() + "','" + isexist.Rows[0]["ClientID"].ToString() + "','" + isexist.Rows[0]["PO_No"].ToString() + "','" + isexist.Rows[0]["SaleType"].ToString() + "','" + isexist.Rows[0]["count"].ToString() + "','" + isexist.Rows[0]["totalqty"].ToString() + "','" + isexist.Rows[0]["totalbasic"].ToString() + "','" + isexist.Rows[0]["totaltax"].ToString() + "','" + isexist.Rows[0]["totalnet"].ToString() + "','" + isexist.Rows[0]["isactive"].ToString() + "','" + isexist.Rows[0]["apprweight"].ToString() + "','" + isexist.Rows[0]["dispatchdetails"].ToString() + "','" + isexist.Rows[0]["remarks"].ToString() + "','" + isexist.Rows[0]["BillType"].ToString() + "','" + isexist.Rows[0]["billno"].ToString() + "','" + isexist.Rows[0]["totaladdtax"].ToString() + "','" + isexist.Rows[0]["roudoff"].ToString() + "','" + isexist.Rows[0]["Duedate"].ToString() + "','" + isexist.Rows[0]["totalaqty"].ToString() + "','" + isexist.Rows[0]["totalfree"].ToString() + "','" + isexist.Rows[0]["totaldiscount"].ToString() + "','" + isexist.Rows[0]["totaladddiscount"].ToString() + "','" + isexist.Rows[0]["totalamount"].ToString() + "','" + isexist.Rows[0]["totalservicejob"].ToString() + "','" + isexist.Rows[0]["totalcharges"].ToString() + "','" + isexist.Rows[0]["Delieveryat"].ToString() + "','" + isexist.Rows[0]["fraight"].ToString() + "','" + isexist.Rows[0]["vehicleno"].ToString() + "','" + isexist.Rows[0]["grrrno"].ToString() + "','" + isexist.Rows[0]["noofskids"].ToString() + "','" + isexist.Rows[0]["sgstamt"].ToString() + "','" + isexist.Rows[0]["cgatamt"].ToString() + "','" + isexist.Rows[0]["igstamt"].ToString() + "','" + isexist.Rows[0]["OrderStatus"].ToString() + "','" + isexist.Rows[0]["refno"].ToString() + "','" + isexist.Rows[0]["totalcess"].ToString() + "','" + isexist.Rows[0]["agentID"].ToString() + "','" + isexist.Rows[0]["SyncID"].ToString() + "','" + isexist.Rows[0]["SyncDatetime"].ToString() + "')", con);
                                }
                            }
                        }
                        else
                        {
                            conn.execute("INSERT INTO [dbo].[SaleOrderMaster]([Bill_No],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[BillType],[billno],[totaladdtax],[roudoff],[Duedate],[totalaqty],[totalfree],[totaldiscount],[totaladddiscount],[totalamount],[totalservicejob],[totalcharges],[Delieveryat],[fraight],[vehicleno],[grrrno],[noofskids],[sgstamt],[cgatamt],[igstamt],[OrderStatus],[refno],[totalcess],[agentID],[SyncID],[SyncDatetime])VALUES('" + serversaleordermaster.Rows[i]["Bill_No"].ToString() + "','" + serversaleordermaster.Rows[i]["Bill_Date"].ToString() + "','" + serversaleordermaster.Rows[i]["Terms"].ToString() + "','" + serversaleordermaster.Rows[i]["ClientID"].ToString() + "','" + serversaleordermaster.Rows[i]["PO_No"].ToString() + "','" + serversaleordermaster.Rows[i]["SaleType"].ToString() + "','" + serversaleordermaster.Rows[i]["count"].ToString() + "','" + serversaleordermaster.Rows[i]["totalqty"].ToString() + "','" + serversaleordermaster.Rows[i]["totalbasic"].ToString() + "','" + serversaleordermaster.Rows[i]["totaltax"].ToString() + "','" + serversaleordermaster.Rows[i]["totalnet"].ToString() + "','" + serversaleordermaster.Rows[i]["isactive"].ToString() + "','" + serversaleordermaster.Rows[i]["apprweight"].ToString() + "','" + serversaleordermaster.Rows[i]["dispatchdetails"].ToString() + "','" + serversaleordermaster.Rows[i]["remarks"].ToString() + "','" + serversaleordermaster.Rows[i]["BillType"].ToString() + "','" + serversaleordermaster.Rows[i]["billno"].ToString() + "','" + serversaleordermaster.Rows[i]["totaladdtax"].ToString() + "','" + serversaleordermaster.Rows[i]["roudoff"].ToString() + "','" + serversaleordermaster.Rows[i]["Duedate"].ToString() + "','" + serversaleordermaster.Rows[i]["totalaqty"].ToString() + "','" + serversaleordermaster.Rows[i]["totalfree"].ToString() + "','" + serversaleordermaster.Rows[i]["totaldiscount"].ToString() + "','" + serversaleordermaster.Rows[i]["totaladddiscount"].ToString() + "','" + serversaleordermaster.Rows[i]["totalamount"].ToString() + "','" + serversaleordermaster.Rows[i]["totalservicejob"].ToString() + "','" + serversaleordermaster.Rows[i]["totalcharges"].ToString() + "','" + serversaleordermaster.Rows[i]["Delieveryat"].ToString() + "','" + serversaleordermaster.Rows[i]["fraight"].ToString() + "','" + serversaleordermaster.Rows[i]["vehicleno"].ToString() + "','" + serversaleordermaster.Rows[i]["grrrno"].ToString() + "','" + serversaleordermaster.Rows[i]["noofskids"].ToString() + "','" + serversaleordermaster.Rows[i]["sgstamt"].ToString() + "','" + serversaleordermaster.Rows[i]["cgatamt"].ToString() + "','" + serversaleordermaster.Rows[i]["igstamt"].ToString() + "','" + serversaleordermaster.Rows[i]["OrderStatus"].ToString() + "','" + serversaleordermaster.Rows[i]["refno"].ToString() + "','" + serversaleordermaster.Rows[i]["totalcess"].ToString() + "','" + serversaleordermaster.Rows[i]["agentID"].ToString() + "','" + serversaleordermaster.Rows[i]["SyncID"].ToString() + "','" + serversaleordermaster.Rows[i]["SyncDatetime"].ToString() + "')");
                        }
                    }
                }
                #endregion

                //SaleOrderProductMaster
                #region
                DataTable clientSaleOrderProductmaster = conn.getdataset("Select * from SaleOrderProductMaster where SyncDatetime >'" + lastsyncdatetime.ToString("MM/dd/yyyy h:mm:ss") + "'");
                DataTable serverSaleOrderProductmaster = conn.getdataset("Select * from SaleOrderProductMaster where SyncDatetime >'" + lastsyncdatetime.ToString("MM/dd/yyyy h:mm:ss") + "'", con);
                if (clientSaleOrderProductmaster.Rows.Count > 0)
                {
                    for (int i = 0; i < clientSaleOrderProductmaster.Rows.Count; i++)
                    {
                        DataTable isexist = conn.getdataset("Select * from SaleOrderProductMaster where SyncID='" + clientSaleOrderProductmaster.Rows[i]["SyncID"].ToString() + "'", con);
                        if (isexist.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(isexist.Rows[0]["SyncID"].ToString()))
                            {
                                string local = clientSaleOrderProductmaster.Rows[i]["SyncDatetime"].ToString();
                                string Server = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Update in local
                                    conn.execute("UPDATE [dbo].[SaleOrderProductMaster] SET [Bill_No] ='" + isexist.Rows[0]["Bill_No"].ToString() + "',[Bill_Run_Date] = '" + isexist.Rows[0]["Bill_Run_Date"].ToString() + "',[Productname] = '" + isexist.Rows[0]["Productname"].ToString() + "',[Packing] = '" + isexist.Rows[0]["Packing"].ToString() + "',[Bags] = '" + isexist.Rows[0]["Bags"].ToString() + "',[MRP] = '" + isexist.Rows[0]["MRP"].ToString() + "',[Pqty] = '" + isexist.Rows[0]["Pqty"].ToString() + "',[Aqty] = '" + isexist.Rows[0]["Aqty"].ToString() + "',[Rate] = '" + isexist.Rows[0]["Rate"].ToString() + "',[Per] = '" + isexist.Rows[0]["Per"].ToString() + "',[Total] = '" + isexist.Rows[0]["Total"].ToString() + "',[Tax] = '" + isexist.Rows[0]["Tax"].ToString() + "',[Amount] = '" + isexist.Rows[0]["Amount"].ToString() + "',[isactive] = '" + isexist.Rows[0]["isactive"].ToString() + "',[qty] = '" + isexist.Rows[0]["qty"].ToString() + "',[Billtype] = '" + isexist.Rows[0]["Billtype"].ToString() + "',[billno] = '" + isexist.Rows[0]["billno"].ToString() + "',[addtax] = '" + isexist.Rows[0]["addtax"].ToString() + "',[batch] = '" + isexist.Rows[0]["batch"].ToString() + "',[free] = '" + isexist.Rows[0]["free"].ToString() + "',[discountper] = '" + isexist.Rows[0]["discountper"].ToString() + "',[discountamt] = '" + isexist.Rows[0]["discountamt"].ToString() + "',[productid] = '" + isexist.Rows[0]["productid"].ToString() + "',[sgstper] = '" + isexist.Rows[0]["sgstper"].ToString() + "',[sgstamt] = '" + isexist.Rows[0]["sgstamt"].ToString() + "',[cgstper] = '" + isexist.Rows[0]["cgstper"].ToString() + "',[cgstamt] = '" + isexist.Rows[0]["cgstamt"].ToString() + "',[igstper] = '" + isexist.Rows[0]["igstper"].ToString() + "',[igdtamt] = '" + isexist.Rows[0]["igdtamt"].ToString() + "',[addtaxper] = '" + isexist.Rows[0]["addtaxper"].ToString() + "',[serialno] = '" + isexist.Rows[0]["serialno"].ToString() + "',[refno] = '" + isexist.Rows[0]["refno"].ToString() + "',[cess] = '" + isexist.Rows[0]["cess"].ToString() + "',[SyncID] = '" + isexist.Rows[0]["SyncID"].ToString() + "',[SyncDatetime] = '" + isexist.Rows[0]["SyncDatetime"].ToString() + "' WHERE SyncID='" + isexist.Rows[0]["SyncID"].ToString() + "'");
                                }
                                else
                                {
                                    //Update in Server
                                    conn.execute("UPDATE [dbo].[SaleOrderProductMaster] SET [Bill_No] ='" + clientSaleOrderProductmaster.Rows[i]["Bill_No"].ToString() + "',[Bill_Run_Date] = '" + clientSaleOrderProductmaster.Rows[i]["Bill_Run_Date"].ToString() + "',[Productname] = '" + clientSaleOrderProductmaster.Rows[i]["Productname"].ToString() + "',[Packing] = '" + clientSaleOrderProductmaster.Rows[i]["Packing"].ToString() + "',[Bags] = '" + clientSaleOrderProductmaster.Rows[i]["Bags"].ToString() + "',[MRP] = '" + clientSaleOrderProductmaster.Rows[i]["MRP"].ToString() + "',[Pqty] = '" + clientSaleOrderProductmaster.Rows[i]["Pqty"].ToString() + "',[Aqty] = '" + clientSaleOrderProductmaster.Rows[i]["Aqty"].ToString() + "',[Rate] = '" + clientSaleOrderProductmaster.Rows[i]["Rate"].ToString() + "',[Per] = '" + clientSaleOrderProductmaster.Rows[i]["Per"].ToString() + "',[Total] = '" + clientSaleOrderProductmaster.Rows[i]["Total"].ToString() + "',[Tax] = '" + clientSaleOrderProductmaster.Rows[i]["Tax"].ToString() + "',[Amount] = '" + clientSaleOrderProductmaster.Rows[i]["Amount"].ToString() + "',[isactive] = '" + clientSaleOrderProductmaster.Rows[i]["isactive"].ToString() + "',[qty] = '" + clientSaleOrderProductmaster.Rows[i]["qty"].ToString() + "',[Billtype] = '" + clientSaleOrderProductmaster.Rows[i]["Billtype"].ToString() + "',[billno] = '" + clientSaleOrderProductmaster.Rows[i]["billno"].ToString() + "',[addtax] = '" + clientSaleOrderProductmaster.Rows[i]["addtax"].ToString() + "',[batch] = '" + clientSaleOrderProductmaster.Rows[i]["batch"].ToString() + "',[free] = '" + clientSaleOrderProductmaster.Rows[i]["free"].ToString() + "',[discountper] = '" + clientSaleOrderProductmaster.Rows[i]["discountper"].ToString() + "',[discountamt] = '" + clientSaleOrderProductmaster.Rows[i]["discountamt"].ToString() + "',[productid] = '" + clientSaleOrderProductmaster.Rows[i]["productid"].ToString() + "',[sgstper] = '" + clientSaleOrderProductmaster.Rows[i]["sgstper"].ToString() + "',[sgstamt] = '" + clientSaleOrderProductmaster.Rows[i]["sgstamt"].ToString() + "',[cgstper] = '" + clientSaleOrderProductmaster.Rows[i]["cgstper"].ToString() + "',[cgstamt] = '" + clientSaleOrderProductmaster.Rows[i]["cgstamt"].ToString() + "',[igstper] = '" + clientSaleOrderProductmaster.Rows[i]["igstper"].ToString() + "',[igdtamt] = '" + clientSaleOrderProductmaster.Rows[i]["igdtamt"].ToString() + "',[addtaxper] = '" + clientSaleOrderProductmaster.Rows[i]["addtaxper"].ToString() + "',[serialno] = '" + clientSaleOrderProductmaster.Rows[i]["serialno"].ToString() + "',[refno] = '" + clientSaleOrderProductmaster.Rows[i]["refno"].ToString() + "',[cess] = '" + clientSaleOrderProductmaster.Rows[i]["cess"].ToString() + "',[SyncID] = '" + clientSaleOrderProductmaster.Rows[i]["SyncID"].ToString() + "',[SyncDatetime] = '" + clientSaleOrderProductmaster.Rows[i]["SyncDatetime"].ToString() + "' WHERE SyncID='" + clientSaleOrderProductmaster.Rows[i]["SyncID"].ToString() + "'", con);
                                }
                            }
                            else
                            {
                                string local = clientSaleOrderProductmaster.Rows[i]["SyncDatetime"].ToString();
                                string Server = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Insert in local
                                    conn.execute("INSERT INTO [dbo].[SaleOrderProductMaster]([Bill_No],[Bill_Run_Date],[Productname],[Packing],[Bags],[MRP],[Pqty],[Aqty],[Rate],[Per],[Total],[Tax],[Amount],[isactive],[qty],[Billtype],[billno],[addtax],[batch],[free],[discountper],[discountamt],[productid],[sgstper],[sgstamt],[cgstper],[cgstamt],[igstper],[igdtamt],[addtaxper],[serialno],[refno],[cess],[SyncID],[SyncDatetime])VALUES('" + isexist.Rows[0]["Bill_No"].ToString() + "','" + isexist.Rows[0]["Bill_Run_Date"].ToString() + "','" + isexist.Rows[0]["Productname"].ToString() + "','" + isexist.Rows[0]["Packing"].ToString() + "','" + isexist.Rows[0]["Bags"].ToString() + "','" + isexist.Rows[0]["MRP"].ToString() + "','" + isexist.Rows[0]["Pqty"].ToString() + "','" + isexist.Rows[0]["Aqty"].ToString() + "','" + isexist.Rows[0]["Rate"].ToString() + "','" + isexist.Rows[0]["Per"].ToString() + "','" + isexist.Rows[0]["Total"].ToString() + "','" + isexist.Rows[0]["Tax"].ToString() + "','" + isexist.Rows[0]["Amount"].ToString() + "','" + isexist.Rows[0]["isactive"].ToString() + "','" + isexist.Rows[0]["qty"].ToString() + "','" + isexist.Rows[0]["Billtype"].ToString() + "','" + isexist.Rows[0]["billno"].ToString() + "','" + isexist.Rows[0]["addtax"].ToString() + "','" + isexist.Rows[0]["batch"].ToString() + "','" + isexist.Rows[0]["free"].ToString() + "','" + isexist.Rows[0]["discountper"].ToString() + "','" + isexist.Rows[0]["discountamt"].ToString() + "','" + isexist.Rows[0]["productid"].ToString() + "','" + isexist.Rows[0]["sgstper"].ToString() + "','" + isexist.Rows[0]["sgstamt"].ToString() + "','" + isexist.Rows[0]["cgstper"].ToString() + "','" + isexist.Rows[0]["cgstamt"].ToString() + "','" + isexist.Rows[0]["igstper"].ToString() + "','" + isexist.Rows[0]["igdtamt"].ToString() + "','" + isexist.Rows[0]["addtaxper"].ToString() + "','" + isexist.Rows[0]["serialno"].ToString() + "','" + isexist.Rows[0]["refno"].ToString() + "','" + isexist.Rows[0]["cess"].ToString() + "','" + isexist.Rows[0]["SyncID"].ToString() + "','" + isexist.Rows[0]["SyncDatetime"].ToString() + "')");

                                }
                                else
                                {
                                    //Insert in Server
                                    conn.execute("INSERT INTO [dbo].[SaleOrderProductMaster]([Bill_No],[Bill_Run_Date],[Productname],[Packing],[Bags],[MRP],[Pqty],[Aqty],[Rate],[Per],[Total],[Tax],[Amount],[isactive],[qty],[Billtype],[billno],[addtax],[batch],[free],[discountper],[discountamt],[productid],[sgstper],[sgstamt],[cgstper],[cgstamt],[igstper],[igdtamt],[addtaxper],[serialno],[refno],[cess],[SyncID],[SyncDatetime])VALUES('" + clientSaleOrderProductmaster.Rows[i]["Bill_No"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["Bill_Run_Date"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["Productname"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["Packing"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["Bags"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["MRP"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["Pqty"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["Aqty"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["Rate"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["Per"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["Total"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["Tax"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["Amount"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["isactive"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["qty"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["Billtype"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["billno"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["addtax"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["batch"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["free"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["discountper"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["discountamt"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["productid"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["sgstper"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["sgstamt"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["cgstper"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["cgstamt"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["igstper"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["igdtamt"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["addtaxper"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["serialno"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["refno"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["cess"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["SyncID"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["SyncDatetime"].ToString() + "')", con);
                                }
                            }
                        }
                        else
                        {
                            conn.execute("INSERT INTO [dbo].[SaleOrderProductMaster]([Bill_No],[Bill_Run_Date],[Productname],[Packing],[Bags],[MRP],[Pqty],[Aqty],[Rate],[Per],[Total],[Tax],[Amount],[isactive],[qty],[Billtype],[billno],[addtax],[batch],[free],[discountper],[discountamt],[productid],[sgstper],[sgstamt],[cgstper],[cgstamt],[igstper],[igdtamt],[addtaxper],[serialno],[refno],[cess],[SyncID],[SyncDatetime])VALUES('" + clientSaleOrderProductmaster.Rows[i]["Bill_No"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["Bill_Run_Date"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["Productname"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["Packing"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["Bags"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["MRP"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["Pqty"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["Aqty"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["Rate"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["Per"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["Total"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["Tax"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["Amount"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["isactive"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["qty"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["Billtype"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["billno"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["addtax"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["batch"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["free"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["discountper"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["discountamt"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["productid"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["sgstper"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["sgstamt"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["cgstper"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["cgstamt"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["igstper"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["igdtamt"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["addtaxper"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["serialno"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["refno"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["cess"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["SyncID"].ToString() + "','" + clientSaleOrderProductmaster.Rows[i]["SyncDatetime"].ToString() + "')", con);
                        }
                    }
                }
                if (serverSaleOrderProductmaster.Rows.Count > 0)
                {
                    for (int i = 0; i < serverSaleOrderProductmaster.Rows.Count; i++)
                    {
                        DataTable isexist = conn.getdataset("Select * from SaleOrderProductMaster where SyncID='" + serverSaleOrderProductmaster.Rows[i]["SyncID"].ToString() + "'");
                        if (isexist.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(isexist.Rows[0]["SyncID"].ToString()))
                            {
                                string Server = serverSaleOrderProductmaster.Rows[i]["SyncDatetime"].ToString();
                                string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Update in local
                                    conn.execute("UPDATE [dbo].[SaleOrderProductMaster] SET [Bill_No] ='" + serverSaleOrderProductmaster.Rows[i]["Bill_No"].ToString() + "',[Bill_Run_Date] = '" + serverSaleOrderProductmaster.Rows[i]["Bill_Run_Date"].ToString() + "',[Productname] = '" + serverSaleOrderProductmaster.Rows[i]["Productname"].ToString() + "',[Packing] = '" + serverSaleOrderProductmaster.Rows[i]["Packing"].ToString() + "',[Bags] = '" + serverSaleOrderProductmaster.Rows[i]["Bags"].ToString() + "',[MRP] = '" + serverSaleOrderProductmaster.Rows[i]["MRP"].ToString() + "',[Pqty] = '" + serverSaleOrderProductmaster.Rows[i]["Pqty"].ToString() + "',[Aqty] = '" + serverSaleOrderProductmaster.Rows[i]["Aqty"].ToString() + "',[Rate] = '" + serverSaleOrderProductmaster.Rows[i]["Rate"].ToString() + "',[Per] = '" + serverSaleOrderProductmaster.Rows[i]["Per"].ToString() + "',[Total] = '" + serverSaleOrderProductmaster.Rows[i]["Total"].ToString() + "',[Tax] = '" + serverSaleOrderProductmaster.Rows[i]["Tax"].ToString() + "',[Amount] = '" + serverSaleOrderProductmaster.Rows[i]["Amount"].ToString() + "',[isactive] = '" + serverSaleOrderProductmaster.Rows[i]["isactive"].ToString() + "',[qty] = '" + serverSaleOrderProductmaster.Rows[i]["qty"].ToString() + "',[Billtype] = '" + serverSaleOrderProductmaster.Rows[i]["Billtype"].ToString() + "',[billno] = '" + serverSaleOrderProductmaster.Rows[i]["billno"].ToString() + "',[addtax] = '" + serverSaleOrderProductmaster.Rows[i]["addtax"].ToString() + "',[batch] = '" + serverSaleOrderProductmaster.Rows[i]["batch"].ToString() + "',[free] = '" + serverSaleOrderProductmaster.Rows[i]["free"].ToString() + "',[discountper] = '" + serverSaleOrderProductmaster.Rows[i]["discountper"].ToString() + "',[discountamt] = '" + serverSaleOrderProductmaster.Rows[i]["discountamt"].ToString() + "',[productid] = '" + serverSaleOrderProductmaster.Rows[i]["productid"].ToString() + "',[sgstper] = '" + serverSaleOrderProductmaster.Rows[i]["sgstper"].ToString() + "',[sgstamt] = '" + serverSaleOrderProductmaster.Rows[i]["sgstamt"].ToString() + "',[cgstper] = '" + serverSaleOrderProductmaster.Rows[i]["cgstper"].ToString() + "',[cgstamt] = '" + serverSaleOrderProductmaster.Rows[i]["cgstamt"].ToString() + "',[igstper] = '" + serverSaleOrderProductmaster.Rows[i]["igstper"].ToString() + "',[igdtamt] = '" + serverSaleOrderProductmaster.Rows[i]["igdtamt"].ToString() + "',[addtaxper] = '" + serverSaleOrderProductmaster.Rows[i]["addtaxper"].ToString() + "',[serialno] = '" + serverSaleOrderProductmaster.Rows[i]["serialno"].ToString() + "',[refno] = '" + serverSaleOrderProductmaster.Rows[i]["refno"].ToString() + "',[cess] = '" + serverSaleOrderProductmaster.Rows[i]["cess"].ToString() + "',[SyncID] = '" + serverSaleOrderProductmaster.Rows[i]["SyncID"].ToString() + "',[SyncDatetime] = '" + serverSaleOrderProductmaster.Rows[i]["SyncDatetime"].ToString() + "' WHERE SyncID='" + serverSaleOrderProductmaster.Rows[i]["SyncID"].ToString() + "'");
                                }
                                else
                                {
                                    //Update in Server
                                    conn.execute("UPDATE [dbo].[SaleOrderProductMaster] SET [Bill_No] ='" + isexist.Rows[0]["Bill_No"].ToString() + "',[Bill_Run_Date] = '" + isexist.Rows[0]["Bill_Run_Date"].ToString() + "',[Productname] = '" + isexist.Rows[0]["Productname"].ToString() + "',[Packing] = '" + isexist.Rows[0]["Packing"].ToString() + "',[Bags] = '" + isexist.Rows[0]["Bags"].ToString() + "',[MRP] = '" + isexist.Rows[0]["MRP"].ToString() + "',[Pqty] = '" + isexist.Rows[0]["Pqty"].ToString() + "',[Aqty] = '" + isexist.Rows[0]["Aqty"].ToString() + "',[Rate] = '" + isexist.Rows[0]["Rate"].ToString() + "',[Per] = '" + isexist.Rows[0]["Per"].ToString() + "',[Total] = '" + isexist.Rows[0]["Total"].ToString() + "',[Tax] = '" + isexist.Rows[0]["Tax"].ToString() + "',[Amount] = '" + isexist.Rows[0]["Amount"].ToString() + "',[isactive] = '" + isexist.Rows[0]["isactive"].ToString() + "',[qty] = '" + isexist.Rows[0]["qty"].ToString() + "',[Billtype] = '" + isexist.Rows[0]["Billtype"].ToString() + "',[billno] = '" + isexist.Rows[0]["billno"].ToString() + "',[addtax] = '" + isexist.Rows[0]["addtax"].ToString() + "',[batch] = '" + isexist.Rows[0]["batch"].ToString() + "',[free] = '" + isexist.Rows[0]["free"].ToString() + "',[discountper] = '" + isexist.Rows[0]["discountper"].ToString() + "',[discountamt] = '" + isexist.Rows[0]["discountamt"].ToString() + "',[productid] = '" + isexist.Rows[0]["productid"].ToString() + "',[sgstper] = '" + isexist.Rows[0]["sgstper"].ToString() + "',[sgstamt] = '" + isexist.Rows[0]["sgstamt"].ToString() + "',[cgstper] = '" + isexist.Rows[0]["cgstper"].ToString() + "',[cgstamt] = '" + isexist.Rows[0]["cgstamt"].ToString() + "',[igstper] = '" + isexist.Rows[0]["igstper"].ToString() + "',[igdtamt] = '" + isexist.Rows[0]["igdtamt"].ToString() + "',[addtaxper] = '" + isexist.Rows[0]["addtaxper"].ToString() + "',[serialno] = '" + isexist.Rows[0]["serialno"].ToString() + "',[refno] = '" + isexist.Rows[0]["refno"].ToString() + "',[cess] = '" + isexist.Rows[0]["cess"].ToString() + "',[SyncID] = '" + isexist.Rows[0]["SyncID"].ToString() + "',[SyncDatetime] = '" + isexist.Rows[0]["SyncDatetime"].ToString() + "' WHERE SyncID='" + isexist.Rows[0]["SyncID"].ToString() + "'", con);
                                }
                            }
                            else
                            {
                                string Server = serverSaleOrderProductmaster.Rows[i]["SyncDatetime"].ToString();
                                string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = DateTime.ParseExact(local, Master.dateformate, en);
                                DateTime serversyncdatetime = DateTime.ParseExact(Server, Master.dateformate, en);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Insert in local
                                    conn.execute("INSERT INTO [dbo].[SaleOrderProductMaster]([Bill_No],[Bill_Run_Date],[Productname],[Packing],[Bags],[MRP],[Pqty],[Aqty],[Rate],[Per],[Total],[Tax],[Amount],[isactive],[qty],[Billtype],[billno],[addtax],[batch],[free],[discountper],[discountamt],[productid],[sgstper],[sgstamt],[cgstper],[cgstamt],[igstper],[igdtamt],[addtaxper],[serialno],[refno],[cess],[SyncID],[SyncDatetime])VALUES('" + serverSaleOrderProductmaster.Rows[i]["Bill_No"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Bill_Run_Date"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Productname"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Packing"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Bags"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["MRP"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Pqty"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Aqty"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Rate"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Per"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Total"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Tax"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Amount"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["isactive"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["qty"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Billtype"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["billno"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["addtax"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["batch"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["free"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["discountper"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["discountamt"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["productid"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["sgstper"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["sgstamt"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["cgstper"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["cgstamt"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["igstper"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["igdtamt"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["addtaxper"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["serialno"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["refno"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["cess"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["SyncID"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["SyncDatetime"].ToString() + "')");
                                    //conn.execute("INSERT INTO [dbo].[BillMaster]([Bill_No],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[BillType],[billno],[totaladdtax],[roudoff],[Duedate],[totalaqty],[totalfree],[totaldiscount],[totaladddiscount],[totalamount],[totalservicejob],[totalcharges],[Delieveryat],[fraight],[vehicleno],[grrrno],[noofskids],[sgstamt],[cgatamt],[igstamt],[OrderStatus],[refno],[totalcess],[agentID],[originalbillno],[originalbilldate],[SyncID],[SyncDatetime])VALUES('" + serverbillmaster.Rows[i]["Bill_No"].ToString() + "','" + serverbillmaster.Rows[i]["Bill_Date"].ToString() + "','" + serverbillmaster.Rows[i]["Terms"].ToString() + "','" + serverbillmaster.Rows[i]["ClientID"].ToString() + "','" + serverbillmaster.Rows[i]["PO_No"].ToString() + "','" + serverbillmaster.Rows[i]["SaleType"].ToString() + "','" + serverbillmaster.Rows[i]["count"].ToString() + "','" + serverbillmaster.Rows[i]["totalqty"].ToString() + "','" + serverbillmaster.Rows[i]["totalbasic"].ToString() + "','" + serverbillmaster.Rows[i]["totaltax"].ToString() + "','" + serverbillmaster.Rows[i]["totalnet"].ToString() + "','" + serverbillmaster.Rows[i]["isactive"].ToString() + "','" + serverbillmaster.Rows[i]["apprweight"].ToString() + "','" + serverbillmaster.Rows[i]["dispatchdetails"].ToString() + "','" + serverbillmaster.Rows[i]["remarks"].ToString() + "','" + serverbillmaster.Rows[i]["BillType"].ToString() + "','" + serverbillmaster.Rows[i]["billno"].ToString() + "','" + serverbillmaster.Rows[i]["totaladdtax"].ToString() + "','" + serverbillmaster.Rows[i]["roudoff"].ToString() + "','" + serverbillmaster.Rows[i]["Duedate"].ToString() + "','" + serverbillmaster.Rows[i]["totalaqty"].ToString() + "','" + serverbillmaster.Rows[i]["totalfree"].ToString() + "','" + serverbillmaster.Rows[i]["totaldiscount"].ToString() + "','" + serverbillmaster.Rows[i]["totaladddiscount"].ToString() + "','" + serverbillmaster.Rows[i]["totalamount"].ToString() + "','" + serverbillmaster.Rows[i]["totalservicejob"].ToString() + "','" + serverbillmaster.Rows[i]["totalcharges"].ToString() + "','" + serverbillmaster.Rows[i]["Delieveryat"].ToString() + "','" + serverbillmaster.Rows[i]["fraight"].ToString() + "','" + serverbillmaster.Rows[i]["vehicleno"].ToString() + "','" + serverbillmaster.Rows[i]["grrrno"].ToString() + "','" + serverbillmaster.Rows[i]["noofskids"].ToString() + "','" + serverbillmaster.Rows[i]["sgstamt"].ToString() + "','" + serverbillmaster.Rows[i]["cgatamt"].ToString() + "','" + serverbillmaster.Rows[i]["igstamt"].ToString() + "','" + serverbillmaster.Rows[i]["OrderStatus"].ToString() + "','" + serverbillmaster.Rows[i]["refno"].ToString() + "','" + serverbillmaster.Rows[i]["totalcess"].ToString() + "','" + serverbillmaster.Rows[i]["agentID"].ToString() + "','" + serverbillmaster.Rows[i]["originalbillno"].ToString() + "','" + serverbillmaster.Rows[i]["originalbilldate"].ToString() + "','" + serverbillmaster.Rows[i]["SyncID"].ToString() + "','" + serverbillmaster.Rows[i]["SyncDatetime"].ToString() + "')");
                                }
                                else
                                {
                                    //Insert in Server
                                    //  conn.execute("INSERT INTO [dbo].[BillMaster]([Bill_No],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[BillType],[billno],[totaladdtax],[roudoff],[Duedate],[totalaqty],[totalfree],[totaldiscount],[totaladddiscount],[totalamount],[totalservicejob],[totalcharges],[Delieveryat],[fraight],[vehicleno],[grrrno],[noofskids],[sgstamt],[cgatamt],[igstamt],[OrderStatus],[refno],[totalcess],[agentID],[originalbillno],[originalbilldate],[SyncID],[SyncDatetime])VALUES('" + isexist.Rows[0]["Bill_No"].ToString() + "','" + isexist.Rows[0]["Bill_Date"].ToString() + "','" + isexist.Rows[0]["Terms"].ToString() + "','" + isexist.Rows[0]["ClientID"].ToString() + "','" + isexist.Rows[0]["PO_No"].ToString() + "','" + isexist.Rows[0]["SaleType"].ToString() + "','" + isexist.Rows[0]["count"].ToString() + "','" + isexist.Rows[0]["totalqty"].ToString() + "','" + isexist.Rows[0]["totalbasic"].ToString() + "','" + isexist.Rows[0]["totaltax"].ToString() + "','" + isexist.Rows[0]["totalnet"].ToString() + "','" + isexist.Rows[0]["isactive"].ToString() + "','" + isexist.Rows[0]["apprweight"].ToString() + "','" + isexist.Rows[0]["dispatchdetails"].ToString() + "','" + isexist.Rows[0]["remarks"].ToString() + "','" + isexist.Rows[0]["BillType"].ToString() + "','" + isexist.Rows[0]["billno"].ToString() + "','" + isexist.Rows[0]["totaladdtax"].ToString() + "','" + isexist.Rows[0]["roudoff"].ToString() + "','" + isexist.Rows[0]["Duedate"].ToString() + "','" + isexist.Rows[0]["totalaqty"].ToString() + "','" + isexist.Rows[0]["totalfree"].ToString() + "','" + isexist.Rows[0]["totaldiscount"].ToString() + "','" + isexist.Rows[0]["totaladddiscount"].ToString() + "','" + isexist.Rows[0]["totalamount"].ToString() + "','" + isexist.Rows[0]["totalservicejob"].ToString() + "','" + isexist.Rows[0]["totalcharges"].ToString() + "','" + isexist.Rows[0]["Delieveryat"].ToString() + "','" + isexist.Rows[0]["fraight"].ToString() + "','" + isexist.Rows[0]["vehicleno"].ToString() + "','" + isexist.Rows[0]["grrrno"].ToString() + "','" + isexist.Rows[0]["noofskids"].ToString() + "','" + isexist.Rows[0]["sgstamt"].ToString() + "','" + isexist.Rows[0]["cgatamt"].ToString() + "','" + isexist.Rows[0]["igstamt"].ToString() + "','" + isexist.Rows[0]["OrderStatus"].ToString() + "','" + isexist.Rows[0]["refno"].ToString() + "','" + isexist.Rows[0]["totalcess"].ToString() + "','" + isexist.Rows[0]["agentID"].ToString() + "','" + isexist.Rows[0]["originalbillno"].ToString() + "','" + isexist.Rows[0]["originalbilldate"].ToString() + "','" + isexist.Rows[0]["SyncID"].ToString() + "','" + isexist.Rows[0]["SyncDatetime"].ToString() + "')", con);
                                    conn.execute("INSERT INTO [dbo].[SaleOrderProductMaster]([Bill_No],[Bill_Run_Date],[Productname],[Packing],[Bags],[MRP],[Pqty],[Aqty],[Rate],[Per],[Total],[Tax],[Amount],[isactive],[qty],[Billtype],[billno],[addtax],[batch],[free],[discountper],[discountamt],[productid],[sgstper],[sgstamt],[cgstper],[cgstamt],[igstper],[igdtamt],[addtaxper],[serialno],[refno],[cess],[SyncID],[SyncDatetime])VALUES('" + isexist.Rows[0]["Bill_No"].ToString() + "','" + isexist.Rows[0]["Bill_Run_Date"].ToString() + "','" + isexist.Rows[0]["Productname"].ToString() + "','" + isexist.Rows[0]["Packing"].ToString() + "','" + isexist.Rows[0]["Bags"].ToString() + "','" + isexist.Rows[0]["MRP"].ToString() + "','" + isexist.Rows[0]["Pqty"].ToString() + "','" + isexist.Rows[0]["Aqty"].ToString() + "','" + isexist.Rows[0]["Rate"].ToString() + "','" + isexist.Rows[0]["Per"].ToString() + "','" + isexist.Rows[0]["Total"].ToString() + "','" + isexist.Rows[0]["Tax"].ToString() + "','" + isexist.Rows[0]["Amount"].ToString() + "','" + isexist.Rows[0]["isactive"].ToString() + "','" + isexist.Rows[0]["qty"].ToString() + "','" + isexist.Rows[0]["Billtype"].ToString() + "','" + isexist.Rows[0]["billno"].ToString() + "','" + isexist.Rows[0]["addtax"].ToString() + "','" + isexist.Rows[0]["batch"].ToString() + "','" + isexist.Rows[0]["free"].ToString() + "','" + isexist.Rows[0]["discountper"].ToString() + "','" + isexist.Rows[0]["discountamt"].ToString() + "','" + isexist.Rows[0]["productid"].ToString() + "','" + isexist.Rows[0]["sgstper"].ToString() + "','" + isexist.Rows[0]["sgstamt"].ToString() + "','" + isexist.Rows[0]["cgstper"].ToString() + "','" + isexist.Rows[0]["cgstamt"].ToString() + "','" + isexist.Rows[0]["igstper"].ToString() + "','" + isexist.Rows[0]["igdtamt"].ToString() + "','" + isexist.Rows[0]["addtaxper"].ToString() + "','" + isexist.Rows[0]["serialno"].ToString() + "','" + isexist.Rows[0]["refno"].ToString() + "','" + isexist.Rows[0]["cess"].ToString() + "','" + isexist.Rows[0]["SyncID"].ToString() + "','" + isexist.Rows[0]["SyncDatetime"].ToString() + "')", con);
                                }
                            }
                        }
                        else
                        {
                            conn.execute("INSERT INTO [dbo].[SaleOrderProductMaster]([Bill_No],[Bill_Run_Date],[Productname],[Packing],[Bags],[MRP],[Pqty],[Aqty],[Rate],[Per],[Total],[Tax],[Amount],[isactive],[qty],[Billtype],[billno],[addtax],[batch],[free],[discountper],[discountamt],[productid],[sgstper],[sgstamt],[cgstper],[cgstamt],[igstper],[igdtamt],[addtaxper],[serialno],[refno],[cess],[SyncID],[SyncDatetime])VALUES('" + serverSaleOrderProductmaster.Rows[i]["Bill_No"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Bill_Run_Date"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Productname"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Packing"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Bags"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["MRP"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Pqty"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Aqty"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Rate"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Per"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Total"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Tax"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Amount"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["isactive"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["qty"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["Billtype"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["billno"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["addtax"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["batch"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["free"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["discountper"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["discountamt"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["productid"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["sgstper"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["sgstamt"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["cgstper"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["cgstamt"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["igstper"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["igdtamt"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["addtaxper"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["serialno"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["refno"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["cess"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["SyncID"].ToString() + "','" + serverSaleOrderProductmaster.Rows[i]["SyncDatetime"].ToString() + "')");
                        }
                    }
                }
                #endregion

                //SaleOrderchargesmaster
                #region
                DataTable clientsaleorderchargesmaster = conn.getdataset("Select * from SaleOrderchargesmaster where SyncDatetime >'" + lastsyncdatetime.ToString("MM/dd/yyyy h:mm:ss") + "'");
                DataTable serversaleorderchargesmaster = conn.getdataset("Select * from SaleOrderchargesmaster where SyncDatetime >'" + lastsyncdatetime.ToString("MM/dd/yyyy h:mm:ss") + "'", con);
                if (clientsaleorderchargesmaster.Rows.Count > 0)
                {
                    for (int i = 0; i < clientsaleorderchargesmaster.Rows.Count; i++)
                    {
                        DataTable isexist = conn.getdataset("Select * from SaleOrderchargesmaster where SyncID='" + clientsaleorderchargesmaster.Rows[i]["SyncID"].ToString() + "'", con);
                        if (isexist.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(isexist.Rows[0]["SyncID"].ToString()))
                            {
                                string local = clientsaleorderchargesmaster.Rows[i]["SyncDatetime"].ToString();
                                string Server = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Update in local
                                    conn.execute("UPDATE [dbo].[SaleOrderchargesmaster] SET [billno] = '" + isexist.Rows[0]["billno"].ToString() + "',[perticulars] = '" + isexist.Rows[0]["perticulars"].ToString() + "',[remarks] = '" + isexist.Rows[0]["remarks"].ToString() + "',[value] = '" + isexist.Rows[0]["value"].ToString() + "',[at] = '" + isexist.Rows[0]["at"].ToString() + "',[plusminus] = '" + isexist.Rows[0]["plusminus"].ToString() + "',[amount] = '" + isexist.Rows[0]["amount"].ToString() + "',[billtype] = '" + isexist.Rows[0]["billtype"].ToString() + "',[isactive] = '" + isexist.Rows[0]["isactive"].ToString() + "',[valueofexp] = '" + isexist.Rows[0]["valueofexp"].ToString() + "',[tax] = '" + isexist.Rows[0]["tax"].ToString() + "',[sgst] = '" + isexist.Rows[0]["sgst"].ToString() + "',[cgst] = '" + isexist.Rows[0]["cgst"].ToString() + "',[igst] = '" + isexist.Rows[0]["igst"].ToString() + "',[additax] ='" + isexist.Rows[0]["additax"].ToString() + "',[addtaxamt] = '" + isexist.Rows[0]["addtaxamt"].ToString() + "',[billsundryid] = '" + isexist.Rows[0]["billsundryid"].ToString() + "',[SyncID] = '" + isexist.Rows[0]["SyncID"].ToString() + "',[SyncDatetime] = '" + isexist.Rows[0]["SyncDatetime"].ToString() + "' WHERE SyncID='" + isexist.Rows[0]["SyncID"].ToString() + "'");
                                }
                                else
                                {
                                    //Update in Server
                                    conn.execute("UPDATE [dbo].[SaleOrderchargesmaster] SET [billno] = '" + clientsaleorderchargesmaster.Rows[i]["billno"].ToString() + "',[perticulars] = '" + clientsaleorderchargesmaster.Rows[i]["perticulars"].ToString() + "',[remarks] = '" + clientsaleorderchargesmaster.Rows[i]["remarks"].ToString() + "',[value] = '" + clientsaleorderchargesmaster.Rows[i]["value"].ToString() + "',[at] = '" + clientsaleorderchargesmaster.Rows[i]["at"].ToString() + "',[plusminus] = '" + clientsaleorderchargesmaster.Rows[i]["plusminus"].ToString() + "',[amount] = '" + clientsaleorderchargesmaster.Rows[i]["amount"].ToString() + "',[billtype] = '" + clientsaleorderchargesmaster.Rows[i]["billtype"].ToString() + "',[isactive] = '" + clientsaleorderchargesmaster.Rows[i]["isactive"].ToString() + "',[valueofexp] = '" + clientsaleorderchargesmaster.Rows[i]["valueofexp"].ToString() + "',[tax] = '" + clientsaleorderchargesmaster.Rows[i]["tax"].ToString() + "',[sgst] = '" + clientsaleorderchargesmaster.Rows[i]["sgst"].ToString() + "',[cgst] = '" + clientsaleorderchargesmaster.Rows[i]["cgst"].ToString() + "',[igst] = '" + clientsaleorderchargesmaster.Rows[i]["igst"].ToString() + "',[additax] ='" + clientsaleorderchargesmaster.Rows[i]["additax"].ToString() + "',[addtaxamt] = '" + clientsaleorderchargesmaster.Rows[i]["addtaxamt"].ToString() + "',[billsundryid] = '" + clientsaleorderchargesmaster.Rows[i]["billsundryid"].ToString() + "',[SyncID] = '" + clientsaleorderchargesmaster.Rows[i]["SyncID"].ToString() + "',[SyncDatetime] = '" + clientsaleorderchargesmaster.Rows[i]["SyncDatetime"].ToString() + "' WHERE SyncID='" + clientsaleorderchargesmaster.Rows[i]["SyncID"].ToString() + "'", con);
                                }
                            }
                            else
                            {
                                string local = clientsaleorderchargesmaster.Rows[i]["SyncDatetime"].ToString();
                                string Server = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Insert in local
                                    conn.execute("INSERT INTO [dbo].[SaleOrderchargesmaster]([billno],[perticulars],[remarks],[value],[at],[plusminus],[amount],[billtype],[isactive],[valueofexp],[tax],[sgst],[cgst],[igst],[additax],[addtaxamt],[billsundryid],[SyncID],[SyncDatetime])VALUES('" + isexist.Rows[0]["billno"].ToString() + "','" + isexist.Rows[0]["perticulars"].ToString() + "','" + isexist.Rows[0]["remarks"].ToString() + "','" + isexist.Rows[0]["value"].ToString() + "','" + isexist.Rows[0]["at"].ToString() + "','" + isexist.Rows[0]["plusminus"].ToString() + "','" + isexist.Rows[0]["amount"].ToString() + "','" + isexist.Rows[0]["billtype"].ToString() + "','" + isexist.Rows[0]["isactive"].ToString() + "','" + isexist.Rows[0]["valueofexp"].ToString() + "','" + isexist.Rows[0]["tax"].ToString() + "','" + isexist.Rows[0]["sgst"].ToString() + "','" + isexist.Rows[0]["cgst"].ToString() + "','" + isexist.Rows[0]["igst"].ToString() + "','" + isexist.Rows[0]["additax"].ToString() + "','" + isexist.Rows[0]["addtaxamt"].ToString() + "','" + isexist.Rows[0]["billsundryid"].ToString() + "','" + isexist.Rows[0]["SyncID"].ToString() + "','" + isexist.Rows[0]["SyncDatetime"].ToString() + "')");
                                }
                                else
                                {
                                    //Insert in Server
                                    conn.execute("INSERT INTO [dbo].[SaleOrderchargesmaster]([billno],[perticulars],[remarks],[value],[at],[plusminus],[amount],[billtype],[isactive],[valueofexp],[tax],[sgst],[cgst],[igst],[additax],[addtaxamt],[billsundryid],[SyncID],[SyncDatetime])VALUES('" + clientsaleorderchargesmaster.Rows[i]["billno"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["perticulars"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["remarks"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["value"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["at"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["plusminus"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["amount"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["billtype"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["isactive"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["valueofexp"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["tax"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["sgst"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["cgst"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["igst"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["additax"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["addtaxamt"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["billsundryid"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["SyncID"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["SyncDatetime"].ToString() + "')", con);
                                }
                            }
                        }
                        else
                        {
                            conn.execute("INSERT INTO [dbo].[SaleOrderchargesmaster]([billno],[perticulars],[remarks],[value],[at],[plusminus],[amount],[billtype],[isactive],[valueofexp],[tax],[sgst],[cgst],[igst],[additax],[addtaxamt],[billsundryid],[SyncID],[SyncDatetime])VALUES('" + clientsaleorderchargesmaster.Rows[i]["billno"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["perticulars"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["remarks"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["value"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["at"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["plusminus"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["amount"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["billtype"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["isactive"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["valueofexp"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["tax"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["sgst"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["cgst"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["igst"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["additax"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["addtaxamt"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["billsundryid"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["SyncID"].ToString() + "','" + clientsaleorderchargesmaster.Rows[i]["SyncDatetime"].ToString() + "')", con);
                        }
                    }
                }
                if (serversaleorderchargesmaster.Rows.Count > 0)
                {
                    for (int i = 0; i < serversaleorderchargesmaster.Rows.Count; i++)
                    {
                        DataTable isexist = conn.getdataset("Select * from SaleOrderchargesmaster where SyncID='" + serversaleorderchargesmaster.Rows[i]["SyncID"].ToString() + "'");
                        if (isexist.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(isexist.Rows[0]["SyncID"].ToString()))
                            {
                                string Server = serversaleorderchargesmaster.Rows[i]["SyncDatetime"].ToString();
                                string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Update in local
                                    conn.execute("UPDATE [dbo].[SaleOrderchargesmaster] SET [billno] = '" + serversaleorderchargesmaster.Rows[i]["billno"].ToString() + "',[perticulars] = '" + serversaleorderchargesmaster.Rows[i]["perticulars"].ToString() + "',[remarks] = '" + serversaleorderchargesmaster.Rows[i]["remarks"].ToString() + "',[value] = '" + serversaleorderchargesmaster.Rows[i]["value"].ToString() + "',[at] = '" + serversaleorderchargesmaster.Rows[i]["at"].ToString() + "',[plusminus] = '" + serversaleorderchargesmaster.Rows[i]["plusminus"].ToString() + "',[amount] = '" + serversaleorderchargesmaster.Rows[i]["amount"].ToString() + "',[billtype] = '" + serversaleorderchargesmaster.Rows[i]["billtype"].ToString() + "',[isactive] = '" + serversaleorderchargesmaster.Rows[i]["isactive"].ToString() + "',[valueofexp] = '" + serversaleorderchargesmaster.Rows[i]["valueofexp"].ToString() + "',[tax] = '" + serversaleorderchargesmaster.Rows[i]["tax"].ToString() + "',[sgst] = '" + serversaleorderchargesmaster.Rows[i]["sgst"].ToString() + "',[cgst] = '" + serversaleorderchargesmaster.Rows[i]["cgst"].ToString() + "',[igst] = '" + serversaleorderchargesmaster.Rows[i]["igst"].ToString() + "',[additax] ='" + serversaleorderchargesmaster.Rows[i]["additax"].ToString() + "',[addtaxamt] = '" + serversaleorderchargesmaster.Rows[i]["addtaxamt"].ToString() + "',[billsundryid] = '" + serversaleorderchargesmaster.Rows[i]["billsundryid"].ToString() + "',[SyncID] = '" + serversaleorderchargesmaster.Rows[i]["SyncID"].ToString() + "',[SyncDatetime] = '" + serversaleorderchargesmaster.Rows[i]["SyncDatetime"].ToString() + "' WHERE SyncID='" + serversaleorderchargesmaster.Rows[i]["SyncID"].ToString() + "'");
                                }
                                else
                                {
                                    //Update in Server
                                    conn.execute("UPDATE [dbo].[SaleOrderchargesmaster] SET [billno] = '" + isexist.Rows[0]["billno"].ToString() + "',[perticulars] = '" + isexist.Rows[0]["perticulars"].ToString() + "',[remarks] = '" + isexist.Rows[0]["remarks"].ToString() + "',[value] = '" + isexist.Rows[0]["value"].ToString() + "',[at] = '" + isexist.Rows[0]["at"].ToString() + "',[plusminus] = '" + isexist.Rows[0]["plusminus"].ToString() + "',[amount] = '" + isexist.Rows[0]["amount"].ToString() + "',[billtype] = '" + isexist.Rows[0]["billtype"].ToString() + "',[isactive] = '" + isexist.Rows[0]["isactive"].ToString() + "',[valueofexp] = '" + isexist.Rows[0]["valueofexp"].ToString() + "',[tax] = '" + isexist.Rows[0]["tax"].ToString() + "',[sgst] = '" + isexist.Rows[0]["sgst"].ToString() + "',[cgst] = '" + isexist.Rows[0]["cgst"].ToString() + "',[igst] = '" + isexist.Rows[0]["igst"].ToString() + "',[additax] ='" + isexist.Rows[0]["additax"].ToString() + "',[addtaxamt] = '" + isexist.Rows[0]["addtaxamt"].ToString() + "',[billsundryid] = '" + isexist.Rows[0]["billsundryid"].ToString() + "',[SyncID] = '" + isexist.Rows[0]["SyncID"].ToString() + "',[SyncDatetime] = '" + isexist.Rows[0]["SyncDatetime"].ToString() + "' WHERE SyncID='" + isexist.Rows[0]["SyncID"].ToString() + "'", con);
                                }
                            }
                            else
                            {
                                string Server = serversaleorderchargesmaster.Rows[i]["SyncDatetime"].ToString();
                                string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = DateTime.ParseExact(local, Master.dateformate, en);
                                DateTime serversyncdatetime = DateTime.ParseExact(Server, Master.dateformate, en);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Insert in local
                                    conn.execute("INSERT INTO [dbo].[SaleOrderchargesmaster]([billno],[perticulars],[remarks],[value],[at],[plusminus],[amount],[billtype],[isactive],[valueofexp],[tax],[sgst],[cgst],[igst],[additax],[addtaxamt],[billsundryid],[SyncID],[SyncDatetime])VALUES('" + serversaleorderchargesmaster.Rows[i]["billno"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["perticulars"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["remarks"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["value"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["at"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["plusminus"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["amount"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["billtype"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["isactive"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["valueofexp"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["tax"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["sgst"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["cgst"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["igst"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["additax"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["addtaxamt"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["billsundryid"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["SyncID"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["SyncDatetime"].ToString() + "')");
                                }
                                else
                                {
                                    //Insert in Server
                                    //  conn.execute("INSERT INTO [dbo].[BillMaster]([Bill_No],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[BillType],[billno],[totaladdtax],[roudoff],[Duedate],[totalaqty],[totalfree],[totaldiscount],[totaladddiscount],[totalamount],[totalservicejob],[totalcharges],[Delieveryat],[fraight],[vehicleno],[grrrno],[noofskids],[sgstamt],[cgatamt],[igstamt],[OrderStatus],[refno],[totalcess],[agentID],[originalbillno],[originalbilldate],[SyncID],[SyncDatetime])VALUES('" + isexist.Rows[0]["Bill_No"].ToString() + "','" + isexist.Rows[0]["Bill_Date"].ToString() + "','" + isexist.Rows[0]["Terms"].ToString() + "','" + isexist.Rows[0]["ClientID"].ToString() + "','" + isexist.Rows[0]["PO_No"].ToString() + "','" + isexist.Rows[0]["SaleType"].ToString() + "','" + isexist.Rows[0]["count"].ToString() + "','" + isexist.Rows[0]["totalqty"].ToString() + "','" + isexist.Rows[0]["totalbasic"].ToString() + "','" + isexist.Rows[0]["totaltax"].ToString() + "','" + isexist.Rows[0]["totalnet"].ToString() + "','" + isexist.Rows[0]["isactive"].ToString() + "','" + isexist.Rows[0]["apprweight"].ToString() + "','" + isexist.Rows[0]["dispatchdetails"].ToString() + "','" + isexist.Rows[0]["remarks"].ToString() + "','" + isexist.Rows[0]["BillType"].ToString() + "','" + isexist.Rows[0]["billno"].ToString() + "','" + isexist.Rows[0]["totaladdtax"].ToString() + "','" + isexist.Rows[0]["roudoff"].ToString() + "','" + isexist.Rows[0]["Duedate"].ToString() + "','" + isexist.Rows[0]["totalaqty"].ToString() + "','" + isexist.Rows[0]["totalfree"].ToString() + "','" + isexist.Rows[0]["totaldiscount"].ToString() + "','" + isexist.Rows[0]["totaladddiscount"].ToString() + "','" + isexist.Rows[0]["totalamount"].ToString() + "','" + isexist.Rows[0]["totalservicejob"].ToString() + "','" + isexist.Rows[0]["totalcharges"].ToString() + "','" + isexist.Rows[0]["Delieveryat"].ToString() + "','" + isexist.Rows[0]["fraight"].ToString() + "','" + isexist.Rows[0]["vehicleno"].ToString() + "','" + isexist.Rows[0]["grrrno"].ToString() + "','" + isexist.Rows[0]["noofskids"].ToString() + "','" + isexist.Rows[0]["sgstamt"].ToString() + "','" + isexist.Rows[0]["cgatamt"].ToString() + "','" + isexist.Rows[0]["igstamt"].ToString() + "','" + isexist.Rows[0]["OrderStatus"].ToString() + "','" + isexist.Rows[0]["refno"].ToString() + "','" + isexist.Rows[0]["totalcess"].ToString() + "','" + isexist.Rows[0]["agentID"].ToString() + "','" + isexist.Rows[0]["originalbillno"].ToString() + "','" + isexist.Rows[0]["originalbilldate"].ToString() + "','" + isexist.Rows[0]["SyncID"].ToString() + "','" + isexist.Rows[0]["SyncDatetime"].ToString() + "')", con);
                                    conn.execute("INSERT INTO [dbo].[SaleOrderchargesmaster]([billno],[perticulars],[remarks],[value],[at],[plusminus],[amount],[billtype],[isactive],[valueofexp],[tax],[sgst],[cgst],[igst],[additax],[addtaxamt],[billsundryid],[SyncID],[SyncDatetime])VALUES('" + isexist.Rows[0]["billno"].ToString() + "','" + isexist.Rows[0]["perticulars"].ToString() + "','" + isexist.Rows[0]["remarks"].ToString() + "','" + isexist.Rows[0]["value"].ToString() + "','" + isexist.Rows[0]["at"].ToString() + "','" + isexist.Rows[0]["plusminus"].ToString() + "','" + isexist.Rows[0]["amount"].ToString() + "','" + isexist.Rows[0]["billtype"].ToString() + "','" + isexist.Rows[0]["isactive"].ToString() + "','" + isexist.Rows[0]["valueofexp"].ToString() + "','" + isexist.Rows[0]["tax"].ToString() + "','" + isexist.Rows[0]["sgst"].ToString() + "','" + isexist.Rows[0]["cgst"].ToString() + "','" + isexist.Rows[0]["igst"].ToString() + "','" + isexist.Rows[0]["additax"].ToString() + "','" + isexist.Rows[0]["addtaxamt"].ToString() + "','" + isexist.Rows[0]["billsundryid"].ToString() + "','" + isexist.Rows[0]["SyncID"].ToString() + "','" + isexist.Rows[0]["SyncDatetime"].ToString() + "')", con);
                                }
                            }
                        }
                        else
                        {
                            conn.execute("INSERT INTO [dbo].[SaleOrderchargesmaster]([billno],[perticulars],[remarks],[value],[at],[plusminus],[amount],[billtype],[isactive],[valueofexp],[tax],[sgst],[cgst],[igst],[additax],[addtaxamt],[billsundryid],[SyncID],[SyncDatetime])VALUES('" + serversaleorderchargesmaster.Rows[i]["billno"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["perticulars"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["remarks"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["value"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["at"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["plusminus"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["amount"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["billtype"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["isactive"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["valueofexp"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["tax"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["sgst"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["cgst"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["igst"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["additax"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["addtaxamt"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["billsundryid"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["SyncID"].ToString() + "','" + serversaleorderchargesmaster.Rows[i]["SyncDatetime"].ToString() + "')");
                        }
                    }
                }
                #endregion

                //Serials
                #region
                DataTable clientserial = conn.getdataset("Select * from Serials where SyncDatetime >'" + lastsyncdatetime.ToString("MM/dd/yyyy h:mm:ss") + "'");
                DataTable serverserial = conn.getdataset("Select * from Serials where SyncDatetime >'" + lastsyncdatetime.ToString("MM/dd/yyyy h:mm:ss") + "'", con);
                if (clientserial.Rows.Count > 0)
                {
                    for (int i = 0; i < clientserial.Rows.Count; i++)
                    {
                        DataTable isexist = conn.getdataset("Select * from Serials where SyncID='" + clientserial.Rows[i]["SyncID"].ToString() + "'", con);
                        if (isexist.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(isexist.Rows[0]["SyncID"].ToString()))
                            {
                                string local = clientserial.Rows[i]["SyncDatetime"].ToString();
                                string Server = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Update in local
                                    conn.execute("UPDATE [dbo].[Serials] SET [Date] ='" + isexist.Rows[0]["Date"].ToString() + "',[VoucherID] ='" + isexist.Rows[0]["VoucherID"].ToString() + "',[TransactionID] ='" + isexist.Rows[0]["TransactionID"].ToString() + "',[SerialNo] ='" + isexist.Rows[0]["SerialNo"].ToString() + "',[TransactionType] ='" + isexist.Rows[0]["TransactionType"].ToString() + "',[VchNo] ='" + isexist.Rows[0]["VchNo"].ToString() + "',[PurchaseTypeID] ='" + isexist.Rows[0]["PurchaseTypeID"].ToString() + "',[PurchaseTypeName] ='" + isexist.Rows[0]["PurchaseTypeName"].ToString() + "',[SaleTypeID] ='" + isexist.Rows[0]["SaleTypeID"].ToString() + "',[SaleTypeName] ='" + isexist.Rows[0]["SaleTypeName"].ToString() + "',[PartyID] ='" + isexist.Rows[0]["PartyID"].ToString() + "',[PartyName] ='" + isexist.Rows[0]["PartyName"].ToString() + "',[ItemCompanyID] ='" + isexist.Rows[0]["ItemCompanyID"].ToString() + "',[SyncID] ='" + isexist.Rows[0]["SyncID"].ToString() + "',[SyncDatetime] ='" + isexist.Rows[0]["SyncDatetime"].ToString() + "',[isactive] ='" + isexist.Rows[0]["isactive"].ToString() + "' where SyncID='" + isexist.Rows[0]["SyncID"].ToString() + "'");
                                }
                                else
                                {
                                    //Update in Server
                                    conn.execute("UPDATE [dbo].[Serials] SET [Date] ='" + clientserial.Rows[i]["Date"].ToString() + "',[VoucherID] ='" + clientserial.Rows[i]["VoucherID"].ToString() + "',[TransactionID] ='" + clientserial.Rows[i]["TransactionID"].ToString() + "',[SerialNo] ='" + clientserial.Rows[i]["SerialNo"].ToString() + "',[TransactionType] ='" + clientserial.Rows[i]["TransactionType"].ToString() + "',[VchNo] ='" + clientserial.Rows[i]["VchNo"].ToString() + "',[PurchaseTypeID] ='" + clientserial.Rows[i]["PurchaseTypeID"].ToString() + "',[PurchaseTypeName] ='" + clientserial.Rows[i]["PurchaseTypeName"].ToString() + "',[SaleTypeID] ='" + clientserial.Rows[i]["SaleTypeID"].ToString() + "',[SaleTypeName] ='" + clientserial.Rows[i]["SaleTypeName"].ToString() + "',[PartyID] ='" + clientserial.Rows[i]["PartyID"].ToString() + "',[PartyName] ='" + clientserial.Rows[i]["PartyName"].ToString() + "',[ItemCompanyID] ='" + clientserial.Rows[i]["ItemCompanyID"].ToString() + "',[SyncID] ='" + clientserial.Rows[i]["SyncID"].ToString() + "',[SyncDatetime] ='" + clientserial.Rows[i]["SyncDatetime"].ToString() + "',[isactive] ='" + clientserial.Rows[i]["isactive"].ToString() + "' where SyncID='" + clientserial.Rows[i]["SyncID"].ToString() + "'", con);
                                }
                            }
                            else
                            {
                                string local = clientserial.Rows[i]["SyncDatetime"].ToString();
                                string Server = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Insert in local
                                    conn.execute("INSERT INTO [dbo].[Serials]([Date],[VoucherID],[TransactionID],[SerialNo],[TransactionType],[VchNo],[PurchaseTypeID],[PurchaseTypeName],[SaleTypeID],[SaleTypeName],[PartyID],[PartyName],[ItemCompanyID],[SyncID],[SyncDatetime],[isactive])VALUES('" + isexist.Rows[0]["Date"].ToString() + "','" + isexist.Rows[0]["VoucherID"].ToString() + "','" + isexist.Rows[0]["TransactionID"].ToString() + "','" + isexist.Rows[0]["SerialNo"].ToString() + "','" + isexist.Rows[0]["TransactionType"].ToString() + "','" + isexist.Rows[0]["VchNo"].ToString() + "','" + isexist.Rows[0]["PurchaseTypeID"].ToString() + "','" + isexist.Rows[0]["PurchaseTypeName"].ToString() + "','" + isexist.Rows[0]["SaleTypeID"].ToString() + "','" + isexist.Rows[0]["SaleTypeName"].ToString() + "','" + isexist.Rows[0]["PartyID"].ToString() + "','" + isexist.Rows[0]["PartyName"].ToString() + "','" + isexist.Rows[0]["ItemCompanyID"].ToString() + "','" + isexist.Rows[0]["SyncID"].ToString() + "','" + isexist.Rows[0]["SyncDatetime"].ToString() + "','" + isexist.Rows[0]["isactive"].ToString() + "')");
                                }
                                else
                                {
                                    //Insert in Server
                                    conn.execute("INSERT INTO [dbo].[Serials]([Date],[VoucherID],[TransactionID],[SerialNo],[TransactionType],[VchNo],[PurchaseTypeID],[PurchaseTypeName],[SaleTypeID],[SaleTypeName],[PartyID],[PartyName],[ItemCompanyID],[SyncID],[SyncDatetime],[isactive])VALUES('" + clientserial.Rows[i]["Date"].ToString() + "','" + clientserial.Rows[i]["VoucherID"].ToString() + "','" + clientserial.Rows[i]["TransactionID"].ToString() + "','" + clientserial.Rows[i]["SerialNo"].ToString() + "','" + clientserial.Rows[i]["TransactionType"].ToString() + "','" + clientserial.Rows[i]["VchNo"].ToString() + "','" + clientserial.Rows[i]["PurchaseTypeID"].ToString() + "','" + clientserial.Rows[i]["PurchaseTypeName"].ToString() + "','" + clientserial.Rows[i]["SaleTypeID"].ToString() + "','" + clientserial.Rows[i]["SaleTypeName"].ToString() + "','" + clientserial.Rows[i]["PartyID"].ToString() + "','" + clientserial.Rows[i]["PartyName"].ToString() + "','" + clientserial.Rows[i]["ItemCompanyID"].ToString() + "','" + clientserial.Rows[i]["SyncID"].ToString() + "','" + clientserial.Rows[i]["SyncDatetime"].ToString() + "','" + clientserial.Rows[i]["isactive"].ToString() + "')", con);
                                }
                            }
                        }
                        else
                        {
                            conn.execute("INSERT INTO [dbo].[Serials]([Date],[VoucherID],[TransactionID],[SerialNo],[TransactionType],[VchNo],[PurchaseTypeID],[PurchaseTypeName],[SaleTypeID],[SaleTypeName],[PartyID],[PartyName],[ItemCompanyID],[SyncID],[SyncDatetime],[isactive])VALUES('" + clientserial.Rows[i]["Date"].ToString() + "','" + clientserial.Rows[i]["VoucherID"].ToString() + "','" + clientserial.Rows[i]["TransactionID"].ToString() + "','" + clientserial.Rows[i]["SerialNo"].ToString() + "','" + clientserial.Rows[i]["TransactionType"].ToString() + "','" + clientserial.Rows[i]["VchNo"].ToString() + "','" + clientserial.Rows[i]["PurchaseTypeID"].ToString() + "','" + clientserial.Rows[i]["PurchaseTypeName"].ToString() + "','" + clientserial.Rows[i]["SaleTypeID"].ToString() + "','" + clientserial.Rows[i]["SaleTypeName"].ToString() + "','" + clientserial.Rows[i]["PartyID"].ToString() + "','" + clientserial.Rows[i]["PartyName"].ToString() + "','" + clientserial.Rows[i]["ItemCompanyID"].ToString() + "','" + clientserial.Rows[i]["SyncID"].ToString() + "','" + clientserial.Rows[i]["SyncDatetime"].ToString() + "','" + clientserial.Rows[i]["isactive"].ToString() + "')", con);
                        }
                    }
                }
                if (serverserial.Rows.Count > 0)
                {
                    for (int i = 0; i < serverserial.Rows.Count; i++)
                    {
                        DataTable isexist = conn.getdataset("Select * from Serials where SyncID='" + serverserial.Rows[i]["SyncID"].ToString() + "'");
                        if (isexist.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(isexist.Rows[0]["SyncID"].ToString()))
                            {
                                string Server = serverserial.Rows[i]["SyncDatetime"].ToString();
                                string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Update in local
                                    conn.execute("UPDATE [dbo].[Serials] SET [Date] ='" + serverserial.Rows[i]["Date"].ToString() + "',[VoucherID] ='" + serverserial.Rows[i]["VoucherID"].ToString() + "',[TransactionID] ='" + serverserial.Rows[i]["TransactionID"].ToString() + "',[SerialNo] ='" + serverserial.Rows[i]["SerialNo"].ToString() + "',[TransactionType] ='" + serverserial.Rows[i]["TransactionType"].ToString() + "',[VchNo] ='" + serverserial.Rows[i]["VchNo"].ToString() + "',[PurchaseTypeID] ='" + serverserial.Rows[i]["PurchaseTypeID"].ToString() + "',[PurchaseTypeName] ='" + serverserial.Rows[i]["PurchaseTypeName"].ToString() + "',[SaleTypeID] ='" + serverserial.Rows[i]["SaleTypeID"].ToString() + "',[SaleTypeName] ='" + serverserial.Rows[i]["SaleTypeName"].ToString() + "',[PartyID] ='" + serverserial.Rows[i]["PartyID"].ToString() + "',[PartyName] ='" + serverserial.Rows[i]["PartyName"].ToString() + "',[ItemCompanyID] ='" + serverserial.Rows[i]["ItemCompanyID"].ToString() + "',[SyncID] ='" + serverserial.Rows[i]["SyncID"].ToString() + "',[SyncDatetime] ='" + serverserial.Rows[i]["SyncDatetime"].ToString() + "',[isactive] ='" + serverserial.Rows[i]["isactive"].ToString() + "' where SyncID='" + serverserial.Rows[i]["SyncID"].ToString() + "'");
                                }
                                else
                                {
                                    //Update in Server
                                    conn.execute("UPDATE [dbo].[Serials] SET [Date] ='" + isexist.Rows[0]["Date"].ToString() + "',[VoucherID] ='" + isexist.Rows[0]["VoucherID"].ToString() + "',[TransactionID] ='" + isexist.Rows[0]["TransactionID"].ToString() + "',[SerialNo] ='" + isexist.Rows[0]["SerialNo"].ToString() + "',[TransactionType] ='" + isexist.Rows[0]["TransactionType"].ToString() + "',[VchNo] ='" + isexist.Rows[0]["VchNo"].ToString() + "',[PurchaseTypeID] ='" + isexist.Rows[0]["PurchaseTypeID"].ToString() + "',[PurchaseTypeName] ='" + isexist.Rows[0]["PurchaseTypeName"].ToString() + "',[SaleTypeID] ='" + isexist.Rows[0]["SaleTypeID"].ToString() + "',[SaleTypeName] ='" + isexist.Rows[0]["SaleTypeName"].ToString() + "',[PartyID] ='" + isexist.Rows[0]["PartyID"].ToString() + "',[PartyName] ='" + isexist.Rows[0]["PartyName"].ToString() + "',[ItemCompanyID] ='" + isexist.Rows[0]["ItemCompanyID"].ToString() + "',[SyncID] ='" + isexist.Rows[0]["SyncID"].ToString() + "',[SyncDatetime] ='" + isexist.Rows[0]["SyncDatetime"].ToString() + "',[isactive] ='" + isexist.Rows[0]["isactive"].ToString() + "' where SyncID='" + isexist.Rows[0]["SyncID"].ToString() + "'", con);
                                }
                            }
                            else
                            {
                                string Server = serverserial.Rows[i]["SyncDatetime"].ToString();
                                string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = DateTime.ParseExact(local, Master.dateformate, en);
                                DateTime serversyncdatetime = DateTime.ParseExact(Server, Master.dateformate, en);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Insert in local
                                    conn.execute("INSERT INTO [dbo].[Serials]([Date],[VoucherID],[TransactionID],[SerialNo],[TransactionType],[VchNo],[PurchaseTypeID],[PurchaseTypeName],[SaleTypeID],[SaleTypeName],[PartyID],[PartyName],[ItemCompanyID],[SyncID],[SyncDatetime],[isactive])VALUES('" + serverserial.Rows[i]["Date"].ToString() + "','" + serverserial.Rows[i]["VoucherID"].ToString() + "','" + serverserial.Rows[i]["TransactionID"].ToString() + "','" + serverserial.Rows[i]["SerialNo"].ToString() + "','" + serverserial.Rows[i]["TransactionType"].ToString() + "','" + serverserial.Rows[i]["VchNo"].ToString() + "','" + serverserial.Rows[i]["PurchaseTypeID"].ToString() + "','" + serverserial.Rows[i]["PurchaseTypeName"].ToString() + "','" + serverserial.Rows[i]["SaleTypeID"].ToString() + "','" + serverserial.Rows[i]["SaleTypeName"].ToString() + "','" + serverserial.Rows[i]["PartyID"].ToString() + "','" + serverserial.Rows[i]["PartyName"].ToString() + "','" + serverserial.Rows[i]["ItemCompanyID"].ToString() + "','" + serverserial.Rows[i]["SyncID"].ToString() + "','" + serverserial.Rows[i]["SyncDatetime"].ToString() + "','" + serverserial.Rows[i]["isactive"].ToString() + "')");
                                }
                                else
                                {
                                    //Insert in Server
                                    //  conn.execute("INSERT INTO [dbo].[BillMaster]([Bill_No],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[BillType],[billno],[totaladdtax],[roudoff],[Duedate],[totalaqty],[totalfree],[totaldiscount],[totaladddiscount],[totalamount],[totalservicejob],[totalcharges],[Delieveryat],[fraight],[vehicleno],[grrrno],[noofskids],[sgstamt],[cgatamt],[igstamt],[OrderStatus],[refno],[totalcess],[agentID],[originalbillno],[originalbilldate],[SyncID],[SyncDatetime])VALUES('" + isexist.Rows[0]["Bill_No"].ToString() + "','" + isexist.Rows[0]["Bill_Date"].ToString() + "','" + isexist.Rows[0]["Terms"].ToString() + "','" + isexist.Rows[0]["ClientID"].ToString() + "','" + isexist.Rows[0]["PO_No"].ToString() + "','" + isexist.Rows[0]["SaleType"].ToString() + "','" + isexist.Rows[0]["count"].ToString() + "','" + isexist.Rows[0]["totalqty"].ToString() + "','" + isexist.Rows[0]["totalbasic"].ToString() + "','" + isexist.Rows[0]["totaltax"].ToString() + "','" + isexist.Rows[0]["totalnet"].ToString() + "','" + isexist.Rows[0]["isactive"].ToString() + "','" + isexist.Rows[0]["apprweight"].ToString() + "','" + isexist.Rows[0]["dispatchdetails"].ToString() + "','" + isexist.Rows[0]["remarks"].ToString() + "','" + isexist.Rows[0]["BillType"].ToString() + "','" + isexist.Rows[0]["billno"].ToString() + "','" + isexist.Rows[0]["totaladdtax"].ToString() + "','" + isexist.Rows[0]["roudoff"].ToString() + "','" + isexist.Rows[0]["Duedate"].ToString() + "','" + isexist.Rows[0]["totalaqty"].ToString() + "','" + isexist.Rows[0]["totalfree"].ToString() + "','" + isexist.Rows[0]["totaldiscount"].ToString() + "','" + isexist.Rows[0]["totaladddiscount"].ToString() + "','" + isexist.Rows[0]["totalamount"].ToString() + "','" + isexist.Rows[0]["totalservicejob"].ToString() + "','" + isexist.Rows[0]["totalcharges"].ToString() + "','" + isexist.Rows[0]["Delieveryat"].ToString() + "','" + isexist.Rows[0]["fraight"].ToString() + "','" + isexist.Rows[0]["vehicleno"].ToString() + "','" + isexist.Rows[0]["grrrno"].ToString() + "','" + isexist.Rows[0]["noofskids"].ToString() + "','" + isexist.Rows[0]["sgstamt"].ToString() + "','" + isexist.Rows[0]["cgatamt"].ToString() + "','" + isexist.Rows[0]["igstamt"].ToString() + "','" + isexist.Rows[0]["OrderStatus"].ToString() + "','" + isexist.Rows[0]["refno"].ToString() + "','" + isexist.Rows[0]["totalcess"].ToString() + "','" + isexist.Rows[0]["agentID"].ToString() + "','" + isexist.Rows[0]["originalbillno"].ToString() + "','" + isexist.Rows[0]["originalbilldate"].ToString() + "','" + isexist.Rows[0]["SyncID"].ToString() + "','" + isexist.Rows[0]["SyncDatetime"].ToString() + "')", con);
                                    conn.execute("INSERT INTO [dbo].[Serials]([Date],[VoucherID],[TransactionID],[SerialNo],[TransactionType],[VchNo],[PurchaseTypeID],[PurchaseTypeName],[SaleTypeID],[SaleTypeName],[PartyID],[PartyName],[ItemCompanyID],[SyncID],[SyncDatetime],[isactive])VALUES('" + isexist.Rows[0]["Date"].ToString() + "','" + isexist.Rows[0]["VoucherID"].ToString() + "','" + isexist.Rows[0]["TransactionID"].ToString() + "','" + isexist.Rows[0]["SerialNo"].ToString() + "','" + isexist.Rows[0]["TransactionType"].ToString() + "','" + isexist.Rows[0]["VchNo"].ToString() + "','" + isexist.Rows[0]["PurchaseTypeID"].ToString() + "','" + isexist.Rows[0]["PurchaseTypeName"].ToString() + "','" + isexist.Rows[0]["SaleTypeID"].ToString() + "','" + isexist.Rows[0]["SaleTypeName"].ToString() + "','" + isexist.Rows[0]["PartyID"].ToString() + "','" + isexist.Rows[0]["PartyName"].ToString() + "','" + isexist.Rows[0]["ItemCompanyID"].ToString() + "','" + isexist.Rows[0]["SyncID"].ToString() + "','" + isexist.Rows[0]["SyncDatetime"].ToString() + "','" + isexist.Rows[0]["isactive"].ToString() + "')", con);
                                }
                            }
                        }
                        else
                        {
                            conn.execute("INSERT INTO [dbo].[Serials]([Date],[VoucherID],[TransactionID],[SerialNo],[TransactionType],[VchNo],[PurchaseTypeID],[PurchaseTypeName],[SaleTypeID],[SaleTypeName],[PartyID],[PartyName],[ItemCompanyID],[SyncID],[SyncDatetime],[isactive])VALUES('" + serverserial.Rows[i]["Date"].ToString() + "','" + serverserial.Rows[i]["VoucherID"].ToString() + "','" + serverserial.Rows[i]["TransactionID"].ToString() + "','" + serverserial.Rows[i]["SerialNo"].ToString() + "','" + serverserial.Rows[i]["TransactionType"].ToString() + "','" + serverserial.Rows[i]["VchNo"].ToString() + "','" + serverserial.Rows[i]["PurchaseTypeID"].ToString() + "','" + serverserial.Rows[i]["PurchaseTypeName"].ToString() + "','" + serverserial.Rows[i]["SaleTypeID"].ToString() + "','" + serverserial.Rows[i]["SaleTypeName"].ToString() + "','" + serverserial.Rows[i]["PartyID"].ToString() + "','" + serverserial.Rows[i]["PartyName"].ToString() + "','" + serverserial.Rows[i]["ItemCompanyID"].ToString() + "','" + serverserial.Rows[i]["SyncID"].ToString() + "','" + serverserial.Rows[i]["SyncDatetime"].ToString() + "','" + serverserial.Rows[i]["isactive"].ToString() + "')");
                        }
                    }
                }
                #endregion

                //Ledger
                #region
                DataTable clientLedger = conn.getdataset("Select * from Ledger where SyncDatetime >'" + lastsyncdatetime.ToString("MM/dd/yyyy h:mm:ss") + "'");
                DataTable serverLedger = conn.getdataset("Select * from Ledger where SyncDatetime >'" + lastsyncdatetime.ToString("MM/dd/yyyy h:mm:ss") + "'", con);
                if (clientLedger.Rows.Count > 0)
                {
                    for (int i = 0; i < clientLedger.Rows.Count; i++)
                    {
                        DataTable isexist = conn.getdataset("Select * from Ledger where SyncID='" + clientLedger.Rows[i]["SyncID"].ToString() + "'", con);
                        if (isexist.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(isexist.Rows[0]["SyncID"].ToString()))
                            {
                                string local = clientLedger.Rows[i]["SyncDatetime"].ToString();
                                string Server = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Update in local
                                    conn.execute("UPDATE [dbo].[Ledger] SET [VoucherID] ='" + isexist.Rows[0]["VoucherID"].ToString() + "',Date1='" + isexist.Rows[0]["Date1"].ToString() + "',TranType='" + isexist.Rows[0]["TranType"].ToString() + "',AccountID='" + isexist.Rows[0]["AccountID"].ToString() + "',AccountName='" + isexist.Rows[0]["AccountName"].ToString() + "',Amount='" + isexist.Rows[0]["Amount"].ToString() + "',DC='" + isexist.Rows[0]["DC"].ToString() + "',isactive='" + isexist.Rows[0]["isactive"].ToString() + "',OT1='" + isexist.Rows[0]["OT1"].ToString() + "',OD1='" + isexist.Rows[0]["OD1"].ToString() + "',SyncID='" + isexist.Rows[0]["SyncID"].ToString() + "',SyncDatetime='" + isexist.Rows[0]["SyncDatetime"].ToString() + "' where SyncID='" + isexist.Rows[0]["SyncID"].ToString() + "'");
                                }
                                else
                                {
                                    //Update in Server
                                    conn.execute("UPDATE [dbo].[Ledger] SET [VoucherID] ='" + clientLedger.Rows[i]["VoucherID"].ToString() + "',Date1='" + clientLedger.Rows[i]["Date1"].ToString() + "',TranType='" + clientLedger.Rows[i]["TranType"].ToString() + "',AccountID='" + clientLedger.Rows[i]["AccountID"].ToString() + "',AccountName='" + clientLedger.Rows[i]["AccountName"].ToString() + "',Amount='" + clientLedger.Rows[i]["Amount"].ToString() + "',DC='" + clientLedger.Rows[i]["DC"].ToString() + "',isactive='" + clientLedger.Rows[i]["isactive"].ToString() + "',OT1='" + clientLedger.Rows[i]["OT1"].ToString() + "',OD1='" + clientLedger.Rows[i]["OD1"].ToString() + "',SyncID='" + clientLedger.Rows[i]["SyncID"].ToString() + "',SyncDatetime='" + clientLedger.Rows[i]["SyncDatetime"].ToString() + "' where SyncID='" + clientLedger.Rows[i]["SyncID"].ToString() + "'", con);
                                }
                            }
                            else
                            {
                                string local = clientLedger.Rows[i]["SyncDatetime"].ToString();
                                string Server = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Insert in local
                                    conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OD1],[SyncID],[SyncDatetime]) values ('" + isexist.Rows[0]["VoucherID"].ToString() + "','" + isexist.Rows[0]["Date1"].ToString() + "','" + isexist.Rows[0]["TranType"].ToString() + "','" + isexist.Rows[0]["AccountID"].ToString() + "','" + isexist.Rows[0]["AccountName"].ToString() + "','" + isexist.Rows[0]["Amount"].ToString() + "','" + isexist.Rows[0]["DC"].ToString() + "','" + isexist.Rows[0]["isactive"].ToString() + "','" + isexist.Rows[0]["OT1"].ToString() + "','" + isexist.Rows[0]["OD1"].ToString() + "','" + isexist.Rows[0]["SyncID"].ToString() + "','" + isexist.Rows[0]["SyncDatetime"].ToString() + "')");
                                }
                                else
                                {
                                    //Insert in Server
                                    conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OD1],[SyncID],[SyncDatetime]) values ('" + clientLedger.Rows[i]["VoucherID"].ToString() + "','" + clientLedger.Rows[i]["Date1"].ToString() + "','" + clientLedger.Rows[i]["TranType"].ToString() + "','" + clientLedger.Rows[i]["AccountID"].ToString() + "','" + clientLedger.Rows[i]["AccountName"].ToString() + "','" + clientLedger.Rows[i]["Amount"].ToString() + "','" + clientLedger.Rows[i]["DC"].ToString() + "','" + clientLedger.Rows[i]["isactive"].ToString() + "','" + clientLedger.Rows[i]["OT1"].ToString() + "','" + clientLedger.Rows[i]["OD1"].ToString() + "','" + clientLedger.Rows[i]["SyncID"].ToString() + "','" + clientLedger.Rows[i]["SyncDatetime"].ToString() + "')", con);
                                }
                            }
                        }
                        else
                        {
                            conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OD1],[SyncID],[SyncDatetime]) values ('" + clientLedger.Rows[i]["VoucherID"].ToString() + "','" + clientLedger.Rows[i]["Date1"].ToString() + "','" + clientLedger.Rows[i]["TranType"].ToString() + "','" + clientLedger.Rows[i]["AccountID"].ToString() + "','" + clientLedger.Rows[i]["AccountName"].ToString() + "','" + clientLedger.Rows[i]["Amount"].ToString() + "','" + clientLedger.Rows[i]["DC"].ToString() + "','" + clientLedger.Rows[i]["isactive"].ToString() + "','" + clientLedger.Rows[i]["OT1"].ToString() + "','" + clientLedger.Rows[i]["OD1"].ToString() + "','" + clientLedger.Rows[i]["SyncID"].ToString() + "','" + clientLedger.Rows[i]["SyncDatetime"].ToString() + "')", con);
                        }
                    }
                }
                if (serverLedger.Rows.Count > 0)
                {
                    for (int i = 0; i < serverLedger.Rows.Count; i++)
                    {
                        DataTable isexist = conn.getdataset("Select * from Ledger where SyncID='" + serverLedger.Rows[i]["SyncID"].ToString() + "'");
                        if (isexist.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(isexist.Rows[0]["SyncID"].ToString()))
                            {
                                string Server = serverLedger.Rows[i]["SyncDatetime"].ToString();
                                string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = Convert.ToDateTime(local);
                                DateTime serversyncdatetime = Convert.ToDateTime(Server);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Update in local
                                    conn.execute("UPDATE [dbo].[Ledger] SET [VoucherID] ='" + serverLedger.Rows[i]["VoucherID"].ToString() + "',Date1='" + serverLedger.Rows[i]["Date1"].ToString() + "',TranType='" + serverLedger.Rows[i]["TranType"].ToString() + "',AccountID='" + serverLedger.Rows[i]["AccountID"].ToString() + "',AccountName='" + serverLedger.Rows[i]["AccountName"].ToString() + "',Amount='" + serverLedger.Rows[i]["Amount"].ToString() + "',DC='" + serverLedger.Rows[i]["DC"].ToString() + "',isactive='" + serverLedger.Rows[i]["isactive"].ToString() + "',OT1='" + serverLedger.Rows[i]["OT1"].ToString() + "',OD1='" + serverLedger.Rows[i]["OD1"].ToString() + "',SyncID='" + serverLedger.Rows[i]["SyncID"].ToString() + "',SyncDatetime='" + serverLedger.Rows[i]["SyncDatetime"].ToString() + "' where SyncID='" + serverLedger.Rows[i]["SyncID"].ToString() + "'");
                                }
                                else
                                {
                                    //Update in Server
                                    conn.execute("UPDATE [dbo].[Ledger] SET [VoucherID] ='" + isexist.Rows[0]["VoucherID"].ToString() + "',Date1='" + isexist.Rows[0]["Date1"].ToString() + "',TranType='" + isexist.Rows[0]["TranType"].ToString() + "',AccountID='" + isexist.Rows[0]["AccountID"].ToString() + "',AccountName='" + isexist.Rows[0]["AccountName"].ToString() + "',Amount='" + isexist.Rows[0]["Amount"].ToString() + "',DC='" + isexist.Rows[0]["DC"].ToString() + "',isactive='" + isexist.Rows[0]["isactive"].ToString() + "',OT1='" + isexist.Rows[0]["OT1"].ToString() + "',OD1='" + isexist.Rows[0]["OD1"].ToString() + "',SyncID='" + isexist.Rows[0]["SyncID"].ToString() + "',SyncDatetime='" + isexist.Rows[0]["SyncDatetime"].ToString() + "' where SyncID='" + isexist.Rows[0]["SyncID"].ToString() + "'", con);
                                }
                            }
                            else
                            {
                                string Server = serverLedger.Rows[i]["SyncDatetime"].ToString();
                                string local = isexist.Rows[0]["SyncDatetime"].ToString();
                                DateTime localsyncdatetime = DateTime.ParseExact(local, Master.dateformate, en);
                                DateTime serversyncdatetime = DateTime.ParseExact(Server, Master.dateformate, en);
                                if (localsyncdatetime < serversyncdatetime)
                                {
                                    //Insert in local
                                    conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OD1],[SyncID],[SyncDatetime]) values ('" + serverLedger.Rows[i]["VoucherID"].ToString() + "','" + serverLedger.Rows[i]["Date1"].ToString() + "','" + serverLedger.Rows[i]["TranType"].ToString() + "','" + serverLedger.Rows[i]["AccountID"].ToString() + "','" + serverLedger.Rows[i]["AccountName"].ToString() + "','" + serverLedger.Rows[i]["Amount"].ToString() + "','" + serverLedger.Rows[i]["DC"].ToString() + "','" + serverLedger.Rows[i]["isactive"].ToString() + "','" + serverLedger.Rows[i]["OT1"].ToString() + "','" + serverLedger.Rows[i]["OD1"].ToString() + "','" + serverLedger.Rows[i]["SyncID"].ToString() + "','" + serverLedger.Rows[i]["SyncDatetime"].ToString() + "')");
                                }
                                else
                                {
                                    //Insert in Server
                                    //  conn.execute("INSERT INTO [dbo].[BillMaster]([Bill_No],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[BillType],[billno],[totaladdtax],[roudoff],[Duedate],[totalaqty],[totalfree],[totaldiscount],[totaladddiscount],[totalamount],[totalservicejob],[totalcharges],[Delieveryat],[fraight],[vehicleno],[grrrno],[noofskids],[sgstamt],[cgatamt],[igstamt],[OrderStatus],[refno],[totalcess],[agentID],[originalbillno],[originalbilldate],[SyncID],[SyncDatetime])VALUES('" + isexist.Rows[0]["Bill_No"].ToString() + "','" + isexist.Rows[0]["Bill_Date"].ToString() + "','" + isexist.Rows[0]["Terms"].ToString() + "','" + isexist.Rows[0]["ClientID"].ToString() + "','" + isexist.Rows[0]["PO_No"].ToString() + "','" + isexist.Rows[0]["SaleType"].ToString() + "','" + isexist.Rows[0]["count"].ToString() + "','" + isexist.Rows[0]["totalqty"].ToString() + "','" + isexist.Rows[0]["totalbasic"].ToString() + "','" + isexist.Rows[0]["totaltax"].ToString() + "','" + isexist.Rows[0]["totalnet"].ToString() + "','" + isexist.Rows[0]["isactive"].ToString() + "','" + isexist.Rows[0]["apprweight"].ToString() + "','" + isexist.Rows[0]["dispatchdetails"].ToString() + "','" + isexist.Rows[0]["remarks"].ToString() + "','" + isexist.Rows[0]["BillType"].ToString() + "','" + isexist.Rows[0]["billno"].ToString() + "','" + isexist.Rows[0]["totaladdtax"].ToString() + "','" + isexist.Rows[0]["roudoff"].ToString() + "','" + isexist.Rows[0]["Duedate"].ToString() + "','" + isexist.Rows[0]["totalaqty"].ToString() + "','" + isexist.Rows[0]["totalfree"].ToString() + "','" + isexist.Rows[0]["totaldiscount"].ToString() + "','" + isexist.Rows[0]["totaladddiscount"].ToString() + "','" + isexist.Rows[0]["totalamount"].ToString() + "','" + isexist.Rows[0]["totalservicejob"].ToString() + "','" + isexist.Rows[0]["totalcharges"].ToString() + "','" + isexist.Rows[0]["Delieveryat"].ToString() + "','" + isexist.Rows[0]["fraight"].ToString() + "','" + isexist.Rows[0]["vehicleno"].ToString() + "','" + isexist.Rows[0]["grrrno"].ToString() + "','" + isexist.Rows[0]["noofskids"].ToString() + "','" + isexist.Rows[0]["sgstamt"].ToString() + "','" + isexist.Rows[0]["cgatamt"].ToString() + "','" + isexist.Rows[0]["igstamt"].ToString() + "','" + isexist.Rows[0]["OrderStatus"].ToString() + "','" + isexist.Rows[0]["refno"].ToString() + "','" + isexist.Rows[0]["totalcess"].ToString() + "','" + isexist.Rows[0]["agentID"].ToString() + "','" + isexist.Rows[0]["originalbillno"].ToString() + "','" + isexist.Rows[0]["originalbilldate"].ToString() + "','" + isexist.Rows[0]["SyncID"].ToString() + "','" + isexist.Rows[0]["SyncDatetime"].ToString() + "')", con);
                                    conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OD1],[SyncID],[SyncDatetime]) values ('" + isexist.Rows[0]["VoucherID"].ToString() + "','" + isexist.Rows[0]["Date1"].ToString() + "','" + isexist.Rows[0]["TranType"].ToString() + "','" + isexist.Rows[0]["AccountID"].ToString() + "','" + isexist.Rows[0]["AccountName"].ToString() + "','" + isexist.Rows[0]["Amount"].ToString() + "','" + isexist.Rows[0]["DC"].ToString() + "','" + isexist.Rows[0]["isactive"].ToString() + "','" + isexist.Rows[0]["OT1"].ToString() + "','" + isexist.Rows[0]["OD1"].ToString() + "','" + isexist.Rows[0]["SyncID"].ToString() + "','" + isexist.Rows[0]["SyncDatetime"].ToString() + "')", con);
                                }
                            }
                        }
                        else
                        {
                            conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OD1],[SyncID],[SyncDatetime]) values ('" + serverLedger.Rows[i]["VoucherID"].ToString() + "','" + serverLedger.Rows[i]["Date1"].ToString() + "','" + serverLedger.Rows[i]["TranType"].ToString() + "','" + serverLedger.Rows[i]["AccountID"].ToString() + "','" + serverLedger.Rows[i]["AccountName"].ToString() + "','" + serverLedger.Rows[i]["Amount"].ToString() + "','" + serverLedger.Rows[i]["DC"].ToString() + "','" + serverLedger.Rows[i]["isactive"].ToString() + "','" + serverLedger.Rows[i]["OT1"].ToString() + "','" + serverLedger.Rows[i]["OD1"].ToString() + "','" + serverLedger.Rows[i]["SyncID"].ToString() + "','" + serverLedger.Rows[i]["SyncDatetime"].ToString() + "')");
                        }
                    }
                }
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #region
        //      //static string sServerConnection = @"Data Source=68.71.135.2,1533;Initial Catalog=inventory;User ID=inventory;Password=Inventory1!;Persist Security Info=False;Connect Timeout=60";
        //      static string sServerConnection = newsqlconnection;
        //      static string sClientConnection = ConfigurationManager.ConnectionStrings["qry"].ToString();
        //     static string DateTimeName = DateTime.Now.ToString("dd_MMM_yyyy hh_mm_ss");
        //   //   static string sScope = "AccountCustomerType" + DateTimeName;
        //   //   static string sScope1 = "AccountGroup" + DateTimeName;
        //   //   static string sScope2 = "Additional" + DateTimeName;
        //      static string sScope3 = "Billchargesmaster" + DateTimeName;
        //      static string sScope4 = "BillMaster" + DateTimeName;
        //  //    static string sScope5 = "BillPOSMaster" + DateTimeName;
        //  //    static string sScope6 = "BillPOSProductMaster" + DateTimeName;
        //      static string sScope7 = "BillProductMaster" + DateTimeName;
        //  //    static string sScope8 = "BillSundry" + DateTimeName;
        //  //    static string sScope9 = "ChargesHead"+ DateTimeName;
        //  //    static string sScope10 = "ChargesHeadApplyOn"+ DateTimeName;
        //  //    static string sScope11 = "ClientMaster"+ DateTimeName;
        //  //    static string sScope12 = "ClientProductMargin"+ DateTimeName;
        //  //    static string sScope13 = "Company"+ DateTimeName;
        //  //    static string sScope14 = "CompanyMaster"+ DateTimeName;
        //  //    static string sScope15 = "complainmaster"+ DateTimeName;
        //  //    static string sScope16 = "complainstatus"+ DateTimeName;
        //  //    static string sScope17 = "Country_Master"+ DateTimeName;
        //  //    static string sScope18 = "FormFormat"+ DateTimeName;
        //  //    static string sScope19 = "fromcompany"+ DateTimeName;
        //  //    static string sScope20 = "GroupMaster"+ DateTimeName;
        //  //    static string sScope21 = "InwardMstr"+ DateTimeName;
        //  //    static string sScope22 = "InwardProductMstr"+ DateTimeName;
        //  //    static string sScope23 = "ItemGroupMaster"+ DateTimeName;
        //  //    static string sScope24 = "ItemTaxMaster"+ DateTimeName;
        //      static string sScope25 = "Ledger"+ DateTimeName;
        //  //    static string sScope26 = "MenuMaster"+ DateTimeName;
        //  //    static string sScope27 = "Options"+ DateTimeName;
        //  //    static string sScope28 = "OwnerMaster"+ DateTimeName;
        //  //    static string sScope29 = "PartyCompanyDiscount"+ DateTimeName;
        //  //    static string sScope30 = "PartyGroupDiscount"+ DateTimeName;
        //  //    static string sScope31 = "PartyRates"+ DateTimeName;
        //  //    static string sScope32 = "PaymentMaster"+ DateTimeName;
        //  //    static string sScope33 = "paymentreceipt"+ DateTimeName;
        //  //    static string sScope34 = "ProductMaster"+ DateTimeName;
        //  //    static string sScope35 = "ProductPriceMaster"+ DateTimeName;
        //  //    static string sScope36 = "PurchasetypeMaster"+ DateTimeName;
        //  //    static string sScope37 = "Ref"+ DateTimeName;
        //      static string sScope38 = "SaleOrderchargesmaster"+ DateTimeName;
        //      static string sScope39 = "SaleOrderMaster"+ DateTimeName;
        //      static string sScope40 = "SaleOrderProductMaster"+ DateTimeName;
        // //     static string sScope41 = "SaletypeMaster"+ DateTimeName;
        //      static string sScope42 = "Serials"+ DateTimeName;
        ////      static string sScope43 = "State_Master"+ DateTimeName;
        ////      static string sScope44 = "System_Master"+ DateTimeName;
        ////      static string sScope45 = "TaxSlabMaster"+ DateTimeName;
        ////      static string sScope46 = "TaxType"+ DateTimeName;
        ////      static string sScope47 = "tblcomplainmaster"+ DateTimeName;
        ////      static string sScope48 = "tblfinishedgoodsmaster"+ DateTimeName;
        ////      static string sScope49 = "tblfinishedgoodsqty"+ DateTimeName;
        ////      static string sScope50 = "tblitemcomplainmaster"+ DateTimeName;
        ////      static string sScope51 = "tblitemreceivefromcompany"+ DateTimeName;
        ////      static string sScope52 = "tblitemsendtocustomer"+ DateTimeName;
        ////      static string sScope53 = "tblprocessmaster"+ DateTimeName;
        ////      static string sScope54 = "tblproductgeneratedmaster"+ DateTimeName;
        ////      static string sScope55 = "tblproductionmaster"+ DateTimeName;
        ////      static string sScope56 = "tblproductionrawmaterialmaster"+ DateTimeName;
        ////      static string sScope57 = "tblreceivefromcompany"+ DateTimeName;
        ////      static string sScope58 = "tblrowmaterialsmaster"+ DateTimeName;
        ////      static string sScope59 = "tblsendtocompanyitemmaster"+ DateTimeName;
        ////      static string sScope60 = "tblsendtocompanymaster"+ DateTimeName;
        ////      static string sScope61 = "tblsendtocustomer"+ DateTimeName;
        ////      static string sScope62 = "tbluser_employeetype"+ DateTimeName;
        ////      static string sScope63 = "tocompany"+ DateTimeName;
        ////      static string sScope64 = "UnitMaster"+ DateTimeName;
        ////      static string sScope65 = "updatedatabase"+ DateTimeName;
        ////      static string sScope66 = "UserInfo"+ DateTimeName;
        ////      static string sScope67 = "UserRights"+ DateTimeName;
        ////      static string sScope68 = "Voucher"+ DateTimeName;
        //      public void ProvisionServer()
        //      {
        //          try
        //          {
        //          //    //AccountCustomerType
        //          //    #region

        //          ////    conn.execute("Truncate table scope_info");
        //          //  //  conn.execute("Truncate table scope_config");
        //          //  //  conn.execute("Truncate table schema_info");
        //          //    SqlConnection serverConn = new SqlConnection(sServerConnection);
        //          //    //conn.execute("Truncate table scope_info", serverConn);
        //          //   // conn.execute("Truncate table scope_config", serverConn);
        //          //    //conn.execute("Truncate table schema_info", serverConn);
        //          //    DbSyncScopeDescription scopeDesc = new DbSyncScopeDescription(sScope);
        //          //    DbSyncTableDescription tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("AccountCustomerType", serverConn);
        //          //    scopeDesc.Tables.Add(tableDesc);
        //          //    SqlSyncScopeProvisioning serverProvision = new SqlSyncScopeProvisioning(serverConn, scopeDesc);
        //          //    serverProvision.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //          //    serverProvision.Apply();
        //          //    #endregion

        //          //    //AccountGroup
        //          //    #region
        //          //    SqlConnection serverConn1 = new SqlConnection(sServerConnection);
        //          //    DbSyncScopeDescription scopeDesc1 = new DbSyncScopeDescription(sScope1);
        //          //    DbSyncTableDescription tableDesc1 = SqlSyncDescriptionBuilder.GetDescriptionForTable("AccountGroup", serverConn1);
        //          //    scopeDesc1.Tables.Add(tableDesc1);
        //          //    SqlSyncScopeProvisioning serverProvision1 = new SqlSyncScopeProvisioning(serverConn1, scopeDesc1);
        //          //    serverProvision1.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //          //    serverProvision1.Apply();
        //          //    #endregion

        //          //    //Additional
        //          //    #region
        //          //    SqlConnection serverConn2 = new SqlConnection(sServerConnection);
        //          //    DbSyncScopeDescription scopeDesc2 = new DbSyncScopeDescription(sScope2);
        //          //    DbSyncTableDescription tableDesc2 = SqlSyncDescriptionBuilder.GetDescriptionForTable("Additional", serverConn2);
        //          //    scopeDesc2.Tables.Add(tableDesc2);
        //          //    SqlSyncScopeProvisioning serverProvision2 = new SqlSyncScopeProvisioning(serverConn2, scopeDesc2);
        //          //    serverProvision2.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //          //    serverProvision2.Apply();
        //          //    #endregion


        //              //Billchargesmaster
        //              #region
        //              SqlConnection serverConn3 = new SqlConnection(sServerConnection);
        //              DbSyncScopeDescription scopeDesc3 = new DbSyncScopeDescription(sScope3);
        //              DbSyncTableDescription tableDesc3 = SqlSyncDescriptionBuilder.GetDescriptionForTable("Billchargesmaster", serverConn3);
        //              scopeDesc3.Tables.Add(tableDesc3);
        //              SqlSyncScopeProvisioning serverProvision3 = new SqlSyncScopeProvisioning(serverConn3, scopeDesc3);
        //              serverProvision3.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              serverProvision3.Apply();
        //              #endregion


        //              //BillMaster
        //              #region
        //              SqlConnection serverConn4 = new SqlConnection(sServerConnection);
        //              DbSyncScopeDescription scopeDesc4 = new DbSyncScopeDescription(sScope4);
        //              DbSyncTableDescription tableDesc4 = SqlSyncDescriptionBuilder.GetDescriptionForTable("BillMaster", serverConn4);
        //              scopeDesc4.Tables.Add(tableDesc4);
        //              SqlSyncScopeProvisioning serverProvision4 = new SqlSyncScopeProvisioning(serverConn4, scopeDesc4);
        //              serverProvision4.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              serverProvision4.Apply();
        //              #endregion


        //              ////BillPOSMaster
        //              //#region
        //              //SqlConnection serverConn5 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc5 = new DbSyncScopeDescription(sScope5);
        //              //DbSyncTableDescription tableDesc5 = SqlSyncDescriptionBuilder.GetDescriptionForTable("BillPOSMaster", serverConn5);
        //              //scopeDesc5.Tables.Add(tableDesc5);
        //              //SqlSyncScopeProvisioning serverProvision5 = new SqlSyncScopeProvisioning(serverConn5, scopeDesc5);
        //              //serverProvision5.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision5.Apply();
        //              //#endregion


        //              ////BillPOSProductMaster
        //              //#region
        //              //SqlConnection serverConn6 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc6 = new DbSyncScopeDescription(sScope6);
        //              //DbSyncTableDescription tableDesc6 = SqlSyncDescriptionBuilder.GetDescriptionForTable("BillPOSProductMaster", serverConn6);
        //              //scopeDesc6.Tables.Add(tableDesc6);
        //              //SqlSyncScopeProvisioning serverProvision6 = new SqlSyncScopeProvisioning(serverConn6, scopeDesc6);
        //              //serverProvision6.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision6.Apply();
        //              //#endregion


        //              //BillProductMaster
        //              #region
        //              SqlConnection serverConn7 = new SqlConnection(sServerConnection);
        //              DbSyncScopeDescription scopeDesc7 = new DbSyncScopeDescription(sScope7);
        //              DbSyncTableDescription tableDesc7 = SqlSyncDescriptionBuilder.GetDescriptionForTable("BillProductMaster", serverConn7);
        //              scopeDesc7.Tables.Add(tableDesc7);
        //              SqlSyncScopeProvisioning serverProvision7 = new SqlSyncScopeProvisioning(serverConn7, scopeDesc7);
        //              serverProvision7.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              serverProvision7.Apply();
        //              #endregion

        //              ////BillSundry
        //              //#region
        //              //SqlConnection serverConn8 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc8 = new DbSyncScopeDescription(sScope8);
        //              //DbSyncTableDescription tableDesc8 = SqlSyncDescriptionBuilder.GetDescriptionForTable("BillSundry", serverConn8);
        //              //scopeDesc8.Tables.Add(tableDesc8);
        //              //SqlSyncScopeProvisioning serverProvision8 = new SqlSyncScopeProvisioning(serverConn8, scopeDesc8);
        //              //serverProvision8.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision8.Apply();
        //              //#endregion

        //              ////ChargesHead
        //              //#region
        //              //SqlConnection serverConn9 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc9 = new DbSyncScopeDescription(sScope9);
        //              //DbSyncTableDescription tableDesc9 = SqlSyncDescriptionBuilder.GetDescriptionForTable("ChargesHead", serverConn9);
        //              //scopeDesc9.Tables.Add(tableDesc9);
        //              //SqlSyncScopeProvisioning serverProvision9 = new SqlSyncScopeProvisioning(serverConn9, scopeDesc9);
        //              //serverProvision9.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision9.Apply();
        //              //#endregion

        //              //// ChargesHeadApplyOn
        //              //#region
        //              //SqlConnection serverConn10 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc10 = new DbSyncScopeDescription(sScope10);
        //              //DbSyncTableDescription tableDesc10 = SqlSyncDescriptionBuilder.GetDescriptionForTable("ChargesHeadApplyOn", serverConn10);
        //              //scopeDesc10.Tables.Add(tableDesc10);
        //              //SqlSyncScopeProvisioning serverProvision10 = new SqlSyncScopeProvisioning(serverConn10, scopeDesc10);
        //              //serverProvision10.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision10.Apply();
        //              //#endregion

        //              ////ClientMaster
        //              //#region
        //              //SqlConnection serverConn11 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc11 = new DbSyncScopeDescription(sScope11);
        //              //DbSyncTableDescription tableDesc11 = SqlSyncDescriptionBuilder.GetDescriptionForTable("ClientMaster", serverConn11);
        //              //scopeDesc11.Tables.Add(tableDesc11);
        //              //SqlSyncScopeProvisioning serverProvision11 = new SqlSyncScopeProvisioning(serverConn11, scopeDesc11);
        //              //serverProvision11.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision11.Apply();
        //              //#endregion

        //              ////ClientProductMargin
        //              //#region
        //              //SqlConnection serverConn12 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc12 = new DbSyncScopeDescription(sScope12);
        //              //DbSyncTableDescription tableDesc12 = SqlSyncDescriptionBuilder.GetDescriptionForTable("ClientProductMargin", serverConn12);
        //              //scopeDesc12.Tables.Add(tableDesc12);
        //              //SqlSyncScopeProvisioning serverProvision12 = new SqlSyncScopeProvisioning(serverConn12, scopeDesc12);
        //              //serverProvision12.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision12.Apply();
        //              //#endregion

        //              ////Company
        //              //#region
        //              //SqlConnection serverConn13 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc13 = new DbSyncScopeDescription(sScope13);
        //              //DbSyncTableDescription tableDesc13 = SqlSyncDescriptionBuilder.GetDescriptionForTable("Company", serverConn13);
        //              //scopeDesc13.Tables.Add(tableDesc13);
        //              //SqlSyncScopeProvisioning serverProvision13 = new SqlSyncScopeProvisioning(serverConn13, scopeDesc13);
        //              //serverProvision13.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision13.Apply();
        //              //#endregion

        //              ////CompanyMaster
        //              //#region
        //              //SqlConnection serverConn14 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc14 = new DbSyncScopeDescription(sScope14);
        //              //DbSyncTableDescription tableDesc14 = SqlSyncDescriptionBuilder.GetDescriptionForTable("CompanyMaster", serverConn14);
        //              //scopeDesc14.Tables.Add(tableDesc14);
        //              //SqlSyncScopeProvisioning serverProvision14 = new SqlSyncScopeProvisioning(serverConn14, scopeDesc14);
        //              //serverProvision14.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision14.Apply();
        //              //#endregion

        //              ////complainmaster
        //              //#region
        //              //SqlConnection serverConn15 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc15 = new DbSyncScopeDescription(sScope15);
        //              //DbSyncTableDescription tableDesc15 = SqlSyncDescriptionBuilder.GetDescriptionForTable("complainmaster", serverConn15);
        //              //scopeDesc15.Tables.Add(tableDesc15);
        //              //SqlSyncScopeProvisioning serverProvision15 = new SqlSyncScopeProvisioning(serverConn15, scopeDesc15);
        //              //serverProvision15.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision15.Apply();
        //              //#endregion

        //              ////complainstatus
        //              //#region
        //              //SqlConnection serverConn16 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc16 = new DbSyncScopeDescription(sScope16);
        //              //DbSyncTableDescription tableDesc16 = SqlSyncDescriptionBuilder.GetDescriptionForTable("complainstatus", serverConn16);
        //              //scopeDesc16.Tables.Add(tableDesc16);
        //              //SqlSyncScopeProvisioning serverProvision16 = new SqlSyncScopeProvisioning(serverConn16, scopeDesc16);
        //              //serverProvision16.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision16.Apply();
        //              //#endregion

        //              ////Country_Master
        //              //#region
        //              //SqlConnection serverConn17 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc17 = new DbSyncScopeDescription(sScope17);
        //              //DbSyncTableDescription tableDesc17 = SqlSyncDescriptionBuilder.GetDescriptionForTable("Country_Master", serverConn17);
        //              //scopeDesc17.Tables.Add(tableDesc17);
        //              //SqlSyncScopeProvisioning serverProvision17 = new SqlSyncScopeProvisioning(serverConn17, scopeDesc17);
        //              //serverProvision17.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision17.Apply();
        //              //#endregion

        //              ////FormFormat
        //              //#region
        //              //SqlConnection serverConn18 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc18 = new DbSyncScopeDescription(sScope18);
        //              //DbSyncTableDescription tableDesc18 = SqlSyncDescriptionBuilder.GetDescriptionForTable("FormFormat", serverConn18);
        //              //scopeDesc18.Tables.Add(tableDesc18);
        //              //SqlSyncScopeProvisioning serverProvision18 = new SqlSyncScopeProvisioning(serverConn18, scopeDesc18);
        //              //serverProvision18.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision18.Apply();
        //              //#endregion

        //              ////fromcompany
        //              //#region
        //              //SqlConnection serverConn19 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc19 = new DbSyncScopeDescription(sScope19);
        //              //DbSyncTableDescription tableDesc19 = SqlSyncDescriptionBuilder.GetDescriptionForTable("fromcompany", serverConn19);
        //              //scopeDesc19.Tables.Add(tableDesc19);
        //              //SqlSyncScopeProvisioning serverProvision19 = new SqlSyncScopeProvisioning(serverConn19, scopeDesc19);
        //              //serverProvision19.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision19.Apply();
        //              //#endregion

        //              ////GroupMaster
        //              //#region
        //              //SqlConnection serverConn20 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc20 = new DbSyncScopeDescription(sScope20);
        //              //DbSyncTableDescription tableDesc20 = SqlSyncDescriptionBuilder.GetDescriptionForTable("GroupMaster", serverConn20);
        //              //scopeDesc20.Tables.Add(tableDesc20);
        //              //SqlSyncScopeProvisioning serverProvision20 = new SqlSyncScopeProvisioning(serverConn20, scopeDesc20);
        //              //serverProvision20.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision20.Apply();
        //              //#endregion

        //              ////InwardMstr
        //              //#region
        //              //SqlConnection serverConn21 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc21 = new DbSyncScopeDescription(sScope21);
        //              //DbSyncTableDescription tableDesc21 = SqlSyncDescriptionBuilder.GetDescriptionForTable("InwardMstr", serverConn21);
        //              //scopeDesc21.Tables.Add(tableDesc21);
        //              //SqlSyncScopeProvisioning serverProvision21 = new SqlSyncScopeProvisioning(serverConn21, scopeDesc21);
        //              //serverProvision21.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision21.Apply();
        //              //#endregion

        //              ////InwardProductMstr
        //              //#region
        //              //SqlConnection serverConn22 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc22 = new DbSyncScopeDescription(sScope22);
        //              //DbSyncTableDescription tableDesc22 = SqlSyncDescriptionBuilder.GetDescriptionForTable("InwardProductMstr", serverConn22);
        //              //scopeDesc22.Tables.Add(tableDesc22);
        //              //SqlSyncScopeProvisioning serverProvision22 = new SqlSyncScopeProvisioning(serverConn22, scopeDesc22);
        //              //serverProvision22.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision22.Apply();
        //              //#endregion

        //              ////ItemGroupMaster
        //              //#region
        //              //SqlConnection serverConn23 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc23 = new DbSyncScopeDescription(sScope23);
        //              //DbSyncTableDescription tableDesc23 = SqlSyncDescriptionBuilder.GetDescriptionForTable("ItemGroupMaster", serverConn23);
        //              //scopeDesc23.Tables.Add(tableDesc23);
        //              //SqlSyncScopeProvisioning serverProvision23 = new SqlSyncScopeProvisioning(serverConn23, scopeDesc23);
        //              //serverProvision23.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision23.Apply();
        //              //#endregion

        //              ////ItemTaxMaster
        //              //#region
        //              //SqlConnection serverConn24 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc24 = new DbSyncScopeDescription(sScope24);
        //              //DbSyncTableDescription tableDesc24 = SqlSyncDescriptionBuilder.GetDescriptionForTable("ItemTaxMaster", serverConn24);
        //              //scopeDesc24.Tables.Add(tableDesc24);
        //              //SqlSyncScopeProvisioning serverProvision24 = new SqlSyncScopeProvisioning(serverConn24, scopeDesc24);
        //              //serverProvision24.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision24.Apply();
        //              //#endregion

        //              //Ledger
        //              #region
        //              SqlConnection serverConn25 = new SqlConnection(sServerConnection);
        //              DbSyncScopeDescription scopeDesc25 = new DbSyncScopeDescription(sScope25);
        //              DbSyncTableDescription tableDesc25 = SqlSyncDescriptionBuilder.GetDescriptionForTable("Ledger", serverConn25);
        //              scopeDesc25.Tables.Add(tableDesc25);
        //              SqlSyncScopeProvisioning serverProvision25 = new SqlSyncScopeProvisioning(serverConn25, scopeDesc25);
        //              serverProvision25.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              serverProvision25.Apply();
        //              #endregion

        //              ////MenuMaster
        //              //#region
        //              //SqlConnection serverConn26 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc26 = new DbSyncScopeDescription(sScope26);
        //              //DbSyncTableDescription tableDesc26 = SqlSyncDescriptionBuilder.GetDescriptionForTable("MenuMaster", serverConn26);
        //              //scopeDesc26.Tables.Add(tableDesc26);
        //              //SqlSyncScopeProvisioning serverProvision26 = new SqlSyncScopeProvisioning(serverConn26, scopeDesc26);
        //              //serverProvision26.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision26.Apply();
        //              //#endregion

        //              ////Options
        //              //#region
        //              //SqlConnection serverConn27 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc27 = new DbSyncScopeDescription(sScope27);
        //              //DbSyncTableDescription tableDesc27 = SqlSyncDescriptionBuilder.GetDescriptionForTable("Options", serverConn27);
        //              //scopeDesc27.Tables.Add(tableDesc27);
        //              //SqlSyncScopeProvisioning serverProvision27 = new SqlSyncScopeProvisioning(serverConn27, scopeDesc27);
        //              //serverProvision27.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision27.Apply();
        //              //#endregion

        //              ////OwnerMaster
        //              //#region
        //              //SqlConnection serverConn28 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc28 = new DbSyncScopeDescription(sScope28);
        //              //DbSyncTableDescription tableDesc28 = SqlSyncDescriptionBuilder.GetDescriptionForTable("OwnerMaster", serverConn28);
        //              //scopeDesc28.Tables.Add(tableDesc28);
        //              //SqlSyncScopeProvisioning serverProvision28 = new SqlSyncScopeProvisioning(serverConn28, scopeDesc28);
        //              //serverProvision28.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision28.Apply();
        //              //#endregion

        //              ////PartyCompanyDiscount
        //              //#region
        //              //SqlConnection serverConn29 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc29 = new DbSyncScopeDescription(sScope29);
        //              //DbSyncTableDescription tableDesc29 = SqlSyncDescriptionBuilder.GetDescriptionForTable("PartyCompanyDiscount", serverConn29);
        //              //scopeDesc29.Tables.Add(tableDesc29);
        //              //SqlSyncScopeProvisioning serverProvision29 = new SqlSyncScopeProvisioning(serverConn29, scopeDesc29);
        //              //serverProvision29.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision29.Apply();
        //              //#endregion

        //              ////PartyGroupDiscount
        //              //#region
        //              //SqlConnection serverConn30 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc30 = new DbSyncScopeDescription(sScope30);
        //              //DbSyncTableDescription tableDesc30 = SqlSyncDescriptionBuilder.GetDescriptionForTable("PartyGroupDiscount", serverConn30);
        //              //scopeDesc30.Tables.Add(tableDesc30);
        //              //SqlSyncScopeProvisioning serverProvision30 = new SqlSyncScopeProvisioning(serverConn30, scopeDesc30);
        //              //serverProvision30.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision30.Apply();
        //              //#endregion

        //              //// PartyRates
        //              //#region
        //              //SqlConnection serverConn31 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc31 = new DbSyncScopeDescription(sScope31);
        //              //DbSyncTableDescription tableDesc31 = SqlSyncDescriptionBuilder.GetDescriptionForTable("PartyRates", serverConn31);
        //              //scopeDesc31.Tables.Add(tableDesc31);
        //              //SqlSyncScopeProvisioning serverProvision31 = new SqlSyncScopeProvisioning(serverConn31, scopeDesc31);
        //              //serverProvision31.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision31.Apply();
        //              //#endregion

        //              ////PaymentMaster
        //              //#region
        //              //SqlConnection serverConn32 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc32 = new DbSyncScopeDescription(sScope32);
        //              //DbSyncTableDescription tableDesc32 = SqlSyncDescriptionBuilder.GetDescriptionForTable("PaymentMaster", serverConn32);
        //              //scopeDesc32.Tables.Add(tableDesc32);
        //              //SqlSyncScopeProvisioning serverProvision32 = new SqlSyncScopeProvisioning(serverConn32, scopeDesc32);
        //              //serverProvision32.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision32.Apply();
        //              //#endregion

        //              ////paymentreceipt
        //              //#region
        //              //SqlConnection serverConn33 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc33 = new DbSyncScopeDescription(sScope33);
        //              //DbSyncTableDescription tableDesc33 = SqlSyncDescriptionBuilder.GetDescriptionForTable("paymentreceipt", serverConn33);
        //              //scopeDesc33.Tables.Add(tableDesc33);
        //              //SqlSyncScopeProvisioning serverProvision33 = new SqlSyncScopeProvisioning(serverConn33, scopeDesc33);
        //              //serverProvision33.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision33.Apply();
        //              //#endregion

        //              ////ProductMaster
        //              //#region
        //              //SqlConnection serverConn34 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc34 = new DbSyncScopeDescription(sScope34);
        //              //DbSyncTableDescription tableDesc34 = SqlSyncDescriptionBuilder.GetDescriptionForTable("ProductMaster", serverConn34);
        //              //scopeDesc34.Tables.Add(tableDesc34);
        //              //SqlSyncScopeProvisioning serverProvision34 = new SqlSyncScopeProvisioning(serverConn34, scopeDesc34);
        //              //serverProvision34.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision34.Apply();
        //              //#endregion

        //              ////ProductPriceMaster
        //              //#region
        //              //SqlConnection serverConn35 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc35 = new DbSyncScopeDescription(sScope35);
        //              //DbSyncTableDescription tableDesc35 = SqlSyncDescriptionBuilder.GetDescriptionForTable("ProductPriceMaster", serverConn35);
        //              //scopeDesc35.Tables.Add(tableDesc35);
        //              //SqlSyncScopeProvisioning serverProvision35 = new SqlSyncScopeProvisioning(serverConn35, scopeDesc35);
        //              //serverProvision35.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision35.Apply();
        //              //#endregion

        //              ////PurchasetypeMaster
        //              //#region
        //              //SqlConnection serverConn36 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc36 = new DbSyncScopeDescription(sScope36);
        //              //DbSyncTableDescription tableDesc36 = SqlSyncDescriptionBuilder.GetDescriptionForTable("PurchasetypeMaster", serverConn36);
        //              //scopeDesc36.Tables.Add(tableDesc36);
        //              //SqlSyncScopeProvisioning serverProvision36 = new SqlSyncScopeProvisioning(serverConn36, scopeDesc36);
        //              //serverProvision36.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision36.Apply();
        //              //#endregion

        //              ////Ref
        //              //#region
        //              //SqlConnection serverConn37 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc37 = new DbSyncScopeDescription(sScope37);
        //              //DbSyncTableDescription tableDesc37 = SqlSyncDescriptionBuilder.GetDescriptionForTable("Ref", serverConn37);
        //              //scopeDesc37.Tables.Add(tableDesc37);
        //              //SqlSyncScopeProvisioning serverProvision37 = new SqlSyncScopeProvisioning(serverConn37, scopeDesc37);
        //              //serverProvision37.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision37.Apply();
        //              //#endregion

        //              //SaleOrderchargesmaster";
        //              #region
        //              SqlConnection serverConn38 = new SqlConnection(sServerConnection);
        //              DbSyncScopeDescription scopeDesc38 = new DbSyncScopeDescription(sScope38);
        //              DbSyncTableDescription tableDesc38 = SqlSyncDescriptionBuilder.GetDescriptionForTable("SaleOrderchargesmaster", serverConn38);
        //              scopeDesc38.Tables.Add(tableDesc38);
        //              SqlSyncScopeProvisioning serverProvision38 = new SqlSyncScopeProvisioning(serverConn38, scopeDesc38);
        //              serverProvision38.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              serverProvision38.Apply();
        //              #endregion

        //              //SaleOrderMaster
        //              #region
        //              SqlConnection serverConn39 = new SqlConnection(sServerConnection);
        //              DbSyncScopeDescription scopeDesc39 = new DbSyncScopeDescription(sScope39);
        //              DbSyncTableDescription tableDesc39 = SqlSyncDescriptionBuilder.GetDescriptionForTable("SaleOrderMaster", serverConn39);
        //              scopeDesc39.Tables.Add(tableDesc39);
        //              SqlSyncScopeProvisioning serverProvision39 = new SqlSyncScopeProvisioning(serverConn39, scopeDesc39);
        //              serverProvision39.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              serverProvision39.Apply();
        //              #endregion

        //              //SaleOrderProductMaster
        //              #region
        //              SqlConnection serverConn40 = new SqlConnection(sServerConnection);
        //              DbSyncScopeDescription scopeDesc40 = new DbSyncScopeDescription(sScope40);
        //              DbSyncTableDescription tableDesc40 = SqlSyncDescriptionBuilder.GetDescriptionForTable("SaleOrderProductMaster", serverConn40);
        //              scopeDesc40.Tables.Add(tableDesc40);
        //              SqlSyncScopeProvisioning serverProvision40 = new SqlSyncScopeProvisioning(serverConn40, scopeDesc40);
        //              serverProvision40.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              serverProvision40.Apply();
        //              #endregion

        //              ////SaletypeMaster
        //              //#region
        //              //SqlConnection serverConn41 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc41 = new DbSyncScopeDescription(sScope41);
        //              //DbSyncTableDescription tableDesc41 = SqlSyncDescriptionBuilder.GetDescriptionForTable("SaletypeMaster", serverConn41);
        //              //scopeDesc41.Tables.Add(tableDesc41);
        //              //SqlSyncScopeProvisioning serverProvision41 = new SqlSyncScopeProvisioning(serverConn41, scopeDesc41);
        //              //serverProvision41.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision41.Apply();
        //              //#endregion

        //              //Serials
        //              #region
        //              SqlConnection serverConn42 = new SqlConnection(sServerConnection);
        //              DbSyncScopeDescription scopeDesc42 = new DbSyncScopeDescription(sScope42);
        //              DbSyncTableDescription tableDesc42 = SqlSyncDescriptionBuilder.GetDescriptionForTable("Serials", serverConn42);
        //              scopeDesc42.Tables.Add(tableDesc42);
        //              SqlSyncScopeProvisioning serverProvision42 = new SqlSyncScopeProvisioning(serverConn42, scopeDesc42);
        //              serverProvision42.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              serverProvision42.Apply();
        //              #endregion

        //              ////State_Master
        //              //#region
        //              //SqlConnection serverConn43 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc43 = new DbSyncScopeDescription(sScope43);
        //              //DbSyncTableDescription tableDesc43 = SqlSyncDescriptionBuilder.GetDescriptionForTable("State_Master", serverConn43);
        //              //scopeDesc43.Tables.Add(tableDesc43);
        //              //SqlSyncScopeProvisioning serverProvision43 = new SqlSyncScopeProvisioning(serverConn43, scopeDesc43);
        //              //serverProvision43.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision43.Apply();
        //              //#endregion

        //              ////System_Master
        //              //#region
        //              //SqlConnection serverConn44 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc44 = new DbSyncScopeDescription(sScope44);
        //              //DbSyncTableDescription tableDesc44 = SqlSyncDescriptionBuilder.GetDescriptionForTable("System_Master", serverConn44);
        //              //scopeDesc44.Tables.Add(tableDesc44);
        //              //SqlSyncScopeProvisioning serverProvision44 = new SqlSyncScopeProvisioning(serverConn44, scopeDesc44);
        //              //serverProvision44.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision44.Apply();
        //              //#endregion

        //              ////TaxSlabMaster
        //              //#region
        //              //SqlConnection serverConn45 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc45 = new DbSyncScopeDescription(sScope45);
        //              //DbSyncTableDescription tableDesc45 = SqlSyncDescriptionBuilder.GetDescriptionForTable("TaxSlabMaster", serverConn45);
        //              //scopeDesc45.Tables.Add(tableDesc45);
        //              //SqlSyncScopeProvisioning serverProvision45 = new SqlSyncScopeProvisioning(serverConn45, scopeDesc45);
        //              //serverProvision45.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision45.Apply();
        //              //#endregion

        //              ////TaxType
        //              //#region
        //              //SqlConnection serverConn46 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc46 = new DbSyncScopeDescription(sScope46);
        //              //DbSyncTableDescription tableDesc46 = SqlSyncDescriptionBuilder.GetDescriptionForTable("TaxType", serverConn46);
        //              //scopeDesc46.Tables.Add(tableDesc46);
        //              //SqlSyncScopeProvisioning serverProvision46 = new SqlSyncScopeProvisioning(serverConn46, scopeDesc46);
        //              //serverProvision46.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision46.Apply();
        //              //#endregion

        //              ////tblcomplainmaster
        //              //#region
        //              //SqlConnection serverConn47 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc47 = new DbSyncScopeDescription(sScope47);
        //              //DbSyncTableDescription tableDesc47 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tblcomplainmaster", serverConn47);
        //              //scopeDesc47.Tables.Add(tableDesc47);
        //              //SqlSyncScopeProvisioning serverProvision47 = new SqlSyncScopeProvisioning(serverConn47, scopeDesc47);
        //              //serverProvision47.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision47.Apply();
        //              //#endregion

        //              ////tblfinishedgoodsmaster
        //              //#region
        //              //SqlConnection serverConn48 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc48 = new DbSyncScopeDescription(sScope48);
        //              //DbSyncTableDescription tableDesc48 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tblfinishedgoodsmaster", serverConn48);
        //              //scopeDesc48.Tables.Add(tableDesc48);
        //              //SqlSyncScopeProvisioning serverProvision48 = new SqlSyncScopeProvisioning(serverConn48, scopeDesc48);
        //              //serverProvision48.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision48.Apply();
        //              //#endregion

        //              ////tblfinishedgoodsqty
        //              //#region
        //              //SqlConnection serverConn49 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc49 = new DbSyncScopeDescription(sScope49);
        //              //DbSyncTableDescription tableDesc49 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tblfinishedgoodsqty", serverConn49);
        //              //scopeDesc49.Tables.Add(tableDesc49);
        //              //SqlSyncScopeProvisioning serverProvision49 = new SqlSyncScopeProvisioning(serverConn49, scopeDesc49);
        //              //serverProvision49.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision49.Apply();
        //              //#endregion

        //              ////tblitemcomplainmaster
        //              //#region
        //              //SqlConnection serverConn50 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc50 = new DbSyncScopeDescription(sScope50);
        //              //DbSyncTableDescription tableDesc50 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tblitemcomplainmaster", serverConn50);
        //              //scopeDesc50.Tables.Add(tableDesc50);
        //              //SqlSyncScopeProvisioning serverProvision50 = new SqlSyncScopeProvisioning(serverConn50, scopeDesc50);
        //              //serverProvision50.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision50.Apply();
        //              //#endregion

        //              ////tblitemreceivefromcompany
        //              //#region
        //              //SqlConnection serverConn51 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc51 = new DbSyncScopeDescription(sScope51);
        //              //DbSyncTableDescription tableDesc51 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tblitemreceivefromcompany", serverConn51);
        //              //scopeDesc51.Tables.Add(tableDesc51);
        //              //SqlSyncScopeProvisioning serverProvision51 = new SqlSyncScopeProvisioning(serverConn51, scopeDesc51);
        //              //serverProvision51.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision51.Apply();
        //              //#endregion

        //              ////tblitemsendtocustomer
        //              //#region
        //              //SqlConnection serverConn52 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc52 = new DbSyncScopeDescription(sScope52);
        //              //DbSyncTableDescription tableDesc52 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tblitemsendtocustomer", serverConn52);
        //              //scopeDesc52.Tables.Add(tableDesc52);
        //              //SqlSyncScopeProvisioning serverProvision52 = new SqlSyncScopeProvisioning(serverConn52, scopeDesc52);
        //              //serverProvision52.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision52.Apply();
        //              //#endregion

        //              ////tblprocessmaster
        //              //#region
        //              //SqlConnection serverConn53 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc53 = new DbSyncScopeDescription(sScope53);
        //              //DbSyncTableDescription tableDesc53 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tblprocessmaster", serverConn53);
        //              //scopeDesc53.Tables.Add(tableDesc53);
        //              //SqlSyncScopeProvisioning serverProvision53 = new SqlSyncScopeProvisioning(serverConn53, scopeDesc53);
        //              //serverProvision53.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision53.Apply();
        //              //#endregion

        //              ////tblproductgeneratedmaster
        //              //#region
        //              //SqlConnection serverConn54 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc54 = new DbSyncScopeDescription(sScope54);
        //              //DbSyncTableDescription tableDesc54 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tblproductgeneratedmaster", serverConn54);
        //              //scopeDesc54.Tables.Add(tableDesc54);
        //              //SqlSyncScopeProvisioning serverProvision54 = new SqlSyncScopeProvisioning(serverConn54, scopeDesc54);
        //              //serverProvision54.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision54.Apply();
        //              //#endregion

        //              ////tblproductionmaster
        //              //#region
        //              //SqlConnection serverConn55 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc55 = new DbSyncScopeDescription(sScope55);
        //              //DbSyncTableDescription tableDesc55 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tblproductionmaster", serverConn55);
        //              //scopeDesc55.Tables.Add(tableDesc55);
        //              //SqlSyncScopeProvisioning serverProvision55 = new SqlSyncScopeProvisioning(serverConn55, scopeDesc55);
        //              //serverProvision55.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision55.Apply();
        //              //#endregion

        //              ////tblproductionrawmaterialmaster
        //              //#region
        //              //SqlConnection serverConn56 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc56 = new DbSyncScopeDescription(sScope56);
        //              //DbSyncTableDescription tableDesc56 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tblproductionrawmaterialmaster", serverConn56);
        //              //scopeDesc56.Tables.Add(tableDesc56);
        //              //SqlSyncScopeProvisioning serverProvision56 = new SqlSyncScopeProvisioning(serverConn56, scopeDesc56);
        //              //serverProvision56.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision56.Apply();
        //              //#endregion

        //              ////tblreceivefromcompany
        //              //#region
        //              //SqlConnection serverConn57 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc57 = new DbSyncScopeDescription(sScope57);
        //              //DbSyncTableDescription tableDesc57 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tblreceivefromcompany", serverConn57);
        //              //scopeDesc57.Tables.Add(tableDesc57);
        //              //SqlSyncScopeProvisioning serverProvision57 = new SqlSyncScopeProvisioning(serverConn57, scopeDesc57);
        //              //serverProvision57.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision57.Apply();
        //              //#endregion

        //              ////tblrowmaterialsmaster
        //              //#region
        //              //SqlConnection serverConn58 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc58 = new DbSyncScopeDescription(sScope58);
        //              //DbSyncTableDescription tableDesc58 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tblrowmaterialsmaster", serverConn58);
        //              //scopeDesc58.Tables.Add(tableDesc58);
        //              //SqlSyncScopeProvisioning serverProvision58 = new SqlSyncScopeProvisioning(serverConn58, scopeDesc58);
        //              //serverProvision58.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision58.Apply();
        //              //#endregion

        //              ////tblsendtocompanyitemmaster
        //              //#region
        //              //SqlConnection serverConn59 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc59 = new DbSyncScopeDescription(sScope59);
        //              //DbSyncTableDescription tableDesc59 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tblsendtocompanyitemmaster", serverConn59);
        //              //scopeDesc59.Tables.Add(tableDesc59);
        //              //SqlSyncScopeProvisioning serverProvision59 = new SqlSyncScopeProvisioning(serverConn59, scopeDesc59);
        //              //serverProvision59.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision59.Apply();
        //              //#endregion

        //              ////tblsendtocompanymaster
        //              //#region
        //              //SqlConnection serverConn60 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc60 = new DbSyncScopeDescription(sScope60);
        //              //DbSyncTableDescription tableDesc60 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tblsendtocompanymaster", serverConn60);
        //              //scopeDesc60.Tables.Add(tableDesc60);
        //              //SqlSyncScopeProvisioning serverProvision60 = new SqlSyncScopeProvisioning(serverConn60, scopeDesc60);
        //              //serverProvision60.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision60.Apply();
        //              //#endregion

        //              ////tblsendtocustomer
        //              //#region
        //              //SqlConnection serverConn61 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc61 = new DbSyncScopeDescription(sScope61);
        //              //DbSyncTableDescription tableDesc61 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tblsendtocustomer", serverConn61);
        //              //scopeDesc61.Tables.Add(tableDesc61);
        //              //SqlSyncScopeProvisioning serverProvision61 = new SqlSyncScopeProvisioning(serverConn61, scopeDesc61);
        //              //serverProvision61.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision61.Apply();
        //              //#endregion

        //              ////tbluser_employeetype
        //              //#region
        //              //SqlConnection serverConn62 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc62 = new DbSyncScopeDescription(sScope62);
        //              //DbSyncTableDescription tableDesc62 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tbluser_employeetype", serverConn62);
        //              //scopeDesc62.Tables.Add(tableDesc62);
        //              //SqlSyncScopeProvisioning serverProvision62 = new SqlSyncScopeProvisioning(serverConn62, scopeDesc62);
        //              //serverProvision62.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision62.Apply();
        //              //#endregion

        //              ////tocompany
        //              //#region
        //              //SqlConnection serverConn63 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc63 = new DbSyncScopeDescription(sScope63);
        //              //DbSyncTableDescription tableDesc63 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tocompany", serverConn63);
        //              //scopeDesc63.Tables.Add(tableDesc63);
        //              //SqlSyncScopeProvisioning serverProvision63 = new SqlSyncScopeProvisioning(serverConn63, scopeDesc63);
        //              //serverProvision63.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision63.Apply();
        //              //#endregion

        //              ////UnitMaster
        //              //#region
        //              //SqlConnection serverConn64 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc64 = new DbSyncScopeDescription(sScope64);
        //              //DbSyncTableDescription tableDesc64 = SqlSyncDescriptionBuilder.GetDescriptionForTable("UnitMaster", serverConn64);
        //              //scopeDesc64.Tables.Add(tableDesc64);
        //              //SqlSyncScopeProvisioning serverProvision64 = new SqlSyncScopeProvisioning(serverConn64, scopeDesc64);
        //              //serverProvision64.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision64.Apply();
        //              //#endregion

        //              ////updatedatabase
        //              //#region
        //              //SqlConnection serverConn65 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc65 = new DbSyncScopeDescription(sScope65);
        //              //DbSyncTableDescription tableDesc65 = SqlSyncDescriptionBuilder.GetDescriptionForTable("updatedatabase", serverConn65);
        //              //scopeDesc65.Tables.Add(tableDesc65);
        //              //SqlSyncScopeProvisioning serverProvision65 = new SqlSyncScopeProvisioning(serverConn65, scopeDesc65);
        //              //serverProvision65.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision65.Apply();
        //              //#endregion

        //              ////UserInfo
        //              //#region
        //              //SqlConnection serverConn66 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc66 = new DbSyncScopeDescription(sScope66);
        //              //DbSyncTableDescription tableDesc66 = SqlSyncDescriptionBuilder.GetDescriptionForTable("UserInfo", serverConn66);
        //              //scopeDesc66.Tables.Add(tableDesc66);
        //              //SqlSyncScopeProvisioning serverProvision66 = new SqlSyncScopeProvisioning(serverConn66, scopeDesc66);
        //              //serverProvision66.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision66.Apply();
        //              //#endregion

        //              ////UserRights
        //              //#region
        //              //SqlConnection serverConn67 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc67 = new DbSyncScopeDescription(sScope67);
        //              //DbSyncTableDescription tableDesc67 = SqlSyncDescriptionBuilder.GetDescriptionForTable("UserRights", serverConn67);
        //              //scopeDesc67.Tables.Add(tableDesc67);
        //              //SqlSyncScopeProvisioning serverProvision67 = new SqlSyncScopeProvisioning(serverConn67, scopeDesc67);
        //              //serverProvision67.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision67.Apply();
        //              //#endregion

        //              ////Voucher
        //              //#region
        //              //SqlConnection serverConn68 = new SqlConnection(sServerConnection);
        //              //DbSyncScopeDescription scopeDesc68 = new DbSyncScopeDescription(sScope68);
        //              //DbSyncTableDescription tableDesc68 = SqlSyncDescriptionBuilder.GetDescriptionForTable("Voucher", serverConn68);
        //              //scopeDesc68.Tables.Add(tableDesc68);
        //              //SqlSyncScopeProvisioning serverProvision68 = new SqlSyncScopeProvisioning(serverConn68, scopeDesc68);
        //              //serverProvision68.SetCreateTableDefault(DbSyncCreationOption.Skip);
        //              //serverProvision68.Apply();
        //              //#endregion

        //          }
        //          catch(Exception ex)
        //          {
        //              MessageBox.Show("Server side" + ex.Message);
        //          }
        //      }
        //      public void ProvisionClient()
        //      {
        //          try
        //          {
        //              ////AccountCustomerType
        //              //#region
        //              //SqlConnection serverConn = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope, serverConn);
        //              //SqlSyncScopeProvisioning clientProvision = new SqlSyncScopeProvisioning(clientConn, scopeDesc);
        //              //clientProvision.Apply();
        //              //#endregion

        //              ////AccountGroup
        //              //#region
        //              //SqlConnection serverConn1 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn1 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc1 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope1, serverConn1);
        //              //SqlSyncScopeProvisioning clientProvision1 = new SqlSyncScopeProvisioning(clientConn1, scopeDesc1);
        //              //clientProvision1.Apply();
        //              //#endregion

        //              ////Additional
        //              //#region
        //              //SqlConnection serverConn2 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn2 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc2 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope2, serverConn2);
        //              //SqlSyncScopeProvisioning clientProvision2 = new SqlSyncScopeProvisioning(clientConn2, scopeDesc2);
        //              //clientProvision2.Apply();
        //              //#endregion
        //              //Billchargesmaster
        //              #region
        //              SqlConnection serverConn3 = new SqlConnection(sServerConnection);
        //              SqlConnection clientConn3 = new SqlConnection(sClientConnection);
        //              DbSyncScopeDescription scopeDesc3 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope3, serverConn3);
        //              SqlSyncScopeProvisioning clientProvision3 = new SqlSyncScopeProvisioning(clientConn3, scopeDesc3);
        //              clientProvision3.Apply();
        //              #endregion
        //              //BillMaster
        //              #region
        //              SqlConnection serverConn4 = new SqlConnection(sServerConnection);
        //              SqlConnection clientConn4 = new SqlConnection(sClientConnection);
        //              DbSyncScopeDescription scopeDesc4 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope4, serverConn4);
        //              SqlSyncScopeProvisioning clientProvision4 = new SqlSyncScopeProvisioning(clientConn4, scopeDesc4);
        //              clientProvision4.Apply();
        //              #endregion
        //              ////BillPOSMaster
        //              //#region
        //              //SqlConnection serverConn5 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn5 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc5 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope5, serverConn5);
        //              //SqlSyncScopeProvisioning clientProvision5 = new SqlSyncScopeProvisioning(clientConn5, scopeDesc5);
        //              //clientProvision5.Apply();
        //              //#endregion
        //              ////BillPOSProductMaster
        //              //#region
        //              //SqlConnection serverConn6 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn6 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc6 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope6, serverConn6);
        //              //SqlSyncScopeProvisioning clientProvision6 = new SqlSyncScopeProvisioning(clientConn6, scopeDesc6);
        //              //clientProvision6.Apply();
        //              //#endregion
        //              //BillProductMaster
        //              #region
        //              SqlConnection serverConn7 = new SqlConnection(sServerConnection);
        //              SqlConnection clientConn7 = new SqlConnection(sClientConnection);
        //              DbSyncScopeDescription scopeDesc7 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope7, serverConn7);
        //              SqlSyncScopeProvisioning clientProvision7 = new SqlSyncScopeProvisioning(clientConn7, scopeDesc7);
        //              clientProvision7.Apply();
        //              #endregion
        //              ////BillSundry
        //              //#region
        //              //SqlConnection serverConn8 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn8 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc8 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope8, serverConn8);
        //              //SqlSyncScopeProvisioning clientProvision8 = new SqlSyncScopeProvisioning(clientConn8, scopeDesc8);
        //              //clientProvision8.Apply();
        //              //#endregion
        //              ////ChargesHead
        //              //#region
        //              //SqlConnection serverConn9 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn9 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc9 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope9, serverConn9);
        //              //SqlSyncScopeProvisioning clientProvision9 = new SqlSyncScopeProvisioning(clientConn9, scopeDesc9);
        //              //clientProvision9.Apply();
        //              //#endregion
        //              ////ChargesHeadApplyOn
        //              //#region
        //              //SqlConnection serverConn10 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn10 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc10 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope10, serverConn10);
        //              //SqlSyncScopeProvisioning clientProvision10 = new SqlSyncScopeProvisioning(clientConn10, scopeDesc10);
        //              //clientProvision10.Apply();
        //              //#endregion
        //              ////ClientMaster
        //              //#region
        //              //SqlConnection serverConn11 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn11 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc11 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope11, serverConn11);
        //              //SqlSyncScopeProvisioning clientProvision11 = new SqlSyncScopeProvisioning(clientConn11, scopeDesc11);
        //              //clientProvision11.Apply();
        //              //#endregion
        //              ////ClientProductMargin
        //              //#region
        //              //SqlConnection serverConn12 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn12 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc12 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope12, serverConn12);
        //              //SqlSyncScopeProvisioning clientProvision12 = new SqlSyncScopeProvisioning(clientConn12, scopeDesc12);
        //              //clientProvision12.Apply();
        //              //#endregion
        //              ////Company
        //              //#region
        //              //SqlConnection serverConn13 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn13 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc13 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope13, serverConn13);
        //              //SqlSyncScopeProvisioning clientProvision13 = new SqlSyncScopeProvisioning(clientConn13, scopeDesc13);
        //              //clientProvision13.Apply();
        //              //#endregion
        //              ////CompanyMaster
        //              //#region
        //              //SqlConnection serverConn14 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn14 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc14 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope14, serverConn14);
        //              //SqlSyncScopeProvisioning clientProvision14 = new SqlSyncScopeProvisioning(clientConn14, scopeDesc14);
        //              //clientProvision14.Apply();
        //              //#endregion
        //              ////complainmaster
        //              //#region
        //              //SqlConnection serverConn15 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn15 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc15 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope15, serverConn15);
        //              //SqlSyncScopeProvisioning clientProvision15 = new SqlSyncScopeProvisioning(clientConn15, scopeDesc15);
        //              //clientProvision15.Apply();
        //              //#endregion
        //              ////complainstatus
        //              //#region
        //              //SqlConnection serverConn16 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn16 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc16 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope16, serverConn16);
        //              //SqlSyncScopeProvisioning clientProvision16 = new SqlSyncScopeProvisioning(clientConn16, scopeDesc16);
        //              //clientProvision16.Apply();
        //              //#endregion
        //              ////Country_Master
        //              //#region
        //              //SqlConnection serverConn17 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn17 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc17 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope17, serverConn17);
        //              //SqlSyncScopeProvisioning clientProvision17 = new SqlSyncScopeProvisioning(clientConn17, scopeDesc17);
        //              //clientProvision17.Apply();
        //              //#endregion
        //              ////FormFormat
        //              //#region
        //              //SqlConnection serverConn18 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn18 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc18 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope18, serverConn18);
        //              //SqlSyncScopeProvisioning clientProvision18 = new SqlSyncScopeProvisioning(clientConn18, scopeDesc18);
        //              //clientProvision18.Apply();
        //              //#endregion
        //              ////fromcompany
        //              //#region
        //              //SqlConnection serverConn19 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn19 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc19 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope19, serverConn19);
        //              //SqlSyncScopeProvisioning clientProvision19 = new SqlSyncScopeProvisioning(clientConn19, scopeDesc19);
        //              //clientProvision19.Apply();
        //              //#endregion
        //              ////GroupMaster
        //              //#region
        //              //SqlConnection serverConn20 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn20 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc20 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope20, serverConn20);
        //              //SqlSyncScopeProvisioning clientProvision20 = new SqlSyncScopeProvisioning(clientConn20, scopeDesc20);
        //              //clientProvision20.Apply();
        //              //#endregion
        //              ////InwardMstr
        //              //#region
        //              //SqlConnection serverConn21 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn21 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc21 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope21, serverConn21);
        //              //SqlSyncScopeProvisioning clientProvision21 = new SqlSyncScopeProvisioning(clientConn21, scopeDesc21);
        //              //clientProvision21.Apply();
        //              //#endregion
        //              ////InwardProductMstr
        //              //#region
        //              //SqlConnection serverConn22 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn22 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc22 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope22, serverConn22);
        //              //SqlSyncScopeProvisioning clientProvision22 = new SqlSyncScopeProvisioning(clientConn22, scopeDesc22);
        //              //clientProvision22.Apply();
        //              //#endregion
        //              ////ItemGroupMaster
        //              //#region
        //              //SqlConnection serverConn23 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn23 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc23 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope23, serverConn23);
        //              //SqlSyncScopeProvisioning clientProvision23 = new SqlSyncScopeProvisioning(clientConn23, scopeDesc23);
        //              //clientProvision23.Apply();
        //              //#endregion
        //              ////ItemTaxMaster
        //              //#region
        //              //SqlConnection serverConn24 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn24 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc24 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope24, serverConn24);
        //              //SqlSyncScopeProvisioning clientProvision24 = new SqlSyncScopeProvisioning(clientConn24, scopeDesc24);
        //              //clientProvision24.Apply();
        //              //#endregion
        //              ////Ledger
        //              #region
        //              SqlConnection serverConn25 = new SqlConnection(sServerConnection);
        //              SqlConnection clientConn25 = new SqlConnection(sClientConnection);
        //              DbSyncScopeDescription scopeDesc25 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope25, serverConn25);
        //              SqlSyncScopeProvisioning clientProvision25 = new SqlSyncScopeProvisioning(clientConn25, scopeDesc25);
        //              clientProvision25.Apply();
        //              #endregion
        //              ////MenuMaster
        //              //#region
        //              //SqlConnection serverConn26 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn26 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc26 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope26, serverConn26);
        //              //SqlSyncScopeProvisioning clientProvision26 = new SqlSyncScopeProvisioning(clientConn26, scopeDesc26);
        //              //clientProvision26.Apply();
        //              //#endregion
        //              ////Options
        //              //#region
        //              //SqlConnection serverConn27 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn27 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc27 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope27, serverConn27);
        //              //SqlSyncScopeProvisioning clientProvision27 = new SqlSyncScopeProvisioning(clientConn27, scopeDesc27);
        //              //clientProvision27.Apply();
        //              //#endregion
        //              ////OwnerMaster
        //              //#region
        //              //SqlConnection serverConn28 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn28 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc28 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope28, serverConn28);
        //              //SqlSyncScopeProvisioning clientProvision28 = new SqlSyncScopeProvisioning(clientConn28, scopeDesc28);
        //              //clientProvision28.Apply();
        //              //#endregion
        //              ////PartyCompanyDiscount
        //              //#region
        //              //SqlConnection serverConn29 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn29 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc29 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope29, serverConn29);
        //              //SqlSyncScopeProvisioning clientProvision29 = new SqlSyncScopeProvisioning(clientConn29, scopeDesc29);
        //              //clientProvision29.Apply();
        //              //#endregion
        //              ////PartyGroupDiscount
        //              //#region
        //              //SqlConnection serverConn30 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn30 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc30 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope30, serverConn30);
        //              //SqlSyncScopeProvisioning clientProvision30 = new SqlSyncScopeProvisioning(clientConn30, scopeDesc30);
        //              //clientProvision30.Apply();
        //              //#endregion
        //              ////PartyRates
        //              //#region
        //              //SqlConnection serverConn31 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn31 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc31 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope31, serverConn31);
        //              //SqlSyncScopeProvisioning clientProvision31 = new SqlSyncScopeProvisioning(clientConn31, scopeDesc31);
        //              //clientProvision31.Apply();
        //              //#endregion
        //              ////PaymentMaster
        //              //#region
        //              //SqlConnection serverConn32 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn32 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc32 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope32, serverConn32);
        //              //SqlSyncScopeProvisioning clientProvision32 = new SqlSyncScopeProvisioning(clientConn32, scopeDesc32);
        //              //clientProvision32.Apply();
        //              //#endregion
        //              ////paymentreceipt
        //              //#region
        //              //SqlConnection serverConn33 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn33 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc33 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope33, serverConn33);
        //              //SqlSyncScopeProvisioning clientProvision33 = new SqlSyncScopeProvisioning(clientConn33, scopeDesc33);
        //              //clientProvision33.Apply();
        //              //#endregion
        //              ////ProductMaster
        //              //#region
        //              //SqlConnection serverConn34 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn34 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc34 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope34, serverConn34);
        //              //SqlSyncScopeProvisioning clientProvision34 = new SqlSyncScopeProvisioning(clientConn34, scopeDesc34);
        //              //clientProvision34.Apply();
        //              //#endregion
        //              ////ProductPriceMaster
        //              //#region
        //              //SqlConnection serverConn35 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn35 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc35 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope35, serverConn35);
        //              //SqlSyncScopeProvisioning clientProvision35 = new SqlSyncScopeProvisioning(clientConn35, scopeDesc35);
        //              //clientProvision35.Apply();
        //              //#endregion
        //              ////PurchasetypeMaster
        //              //#region
        //              //SqlConnection serverConn36 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn36 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc36 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope36, serverConn36);
        //              //SqlSyncScopeProvisioning clientProvision36 = new SqlSyncScopeProvisioning(clientConn36, scopeDesc36);
        //              //clientProvision36.Apply();
        //              //#endregion
        //              ////Ref
        //              //#region
        //              //SqlConnection serverConn37 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn37 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc37 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope37, serverConn37);
        //              //SqlSyncScopeProvisioning clientProvision37 = new SqlSyncScopeProvisioning(clientConn37, scopeDesc37);
        //              //clientProvision37.Apply();
        //              //#endregion
        //              //SaleOrderchargesmaster
        //              #region
        //              SqlConnection serverConn38 = new SqlConnection(sServerConnection);
        //              SqlConnection clientConn38 = new SqlConnection(sClientConnection);
        //              DbSyncScopeDescription scopeDesc38 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope38, serverConn38);
        //              SqlSyncScopeProvisioning clientProvision38 = new SqlSyncScopeProvisioning(clientConn38, scopeDesc38);
        //              clientProvision38.Apply();
        //              #endregion
        //              //SaleOrderMaster
        //              #region
        //              SqlConnection serverConn39 = new SqlConnection(sServerConnection);
        //              SqlConnection clientConn39 = new SqlConnection(sClientConnection);
        //              DbSyncScopeDescription scopeDesc39 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope39, serverConn39);
        //              SqlSyncScopeProvisioning clientProvision39 = new SqlSyncScopeProvisioning(clientConn39, scopeDesc39);
        //              clientProvision39.Apply();
        //              #endregion
        //              //SaleOrderProductMaster
        //              #region
        //              SqlConnection serverConn40 = new SqlConnection(sServerConnection);
        //              SqlConnection clientConn40 = new SqlConnection(sClientConnection);
        //              DbSyncScopeDescription scopeDesc40 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope40, serverConn40);
        //              SqlSyncScopeProvisioning clientProvision40 = new SqlSyncScopeProvisioning(clientConn40, scopeDesc40);
        //              clientProvision40.Apply();
        //              #endregion
        //              ////SaletypeMaster
        //              //#region
        //              //SqlConnection serverConn41 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn41 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc41 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope41, serverConn41);
        //              //SqlSyncScopeProvisioning clientProvision41 = new SqlSyncScopeProvisioning(clientConn41, scopeDesc41);
        //              //clientProvision41.Apply();
        //              //#endregion
        //              ////Serials
        //              //#region
        //              //SqlConnection serverConn42 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn42 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc42 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope42, serverConn42);
        //              //SqlSyncScopeProvisioning clientProvision42 = new SqlSyncScopeProvisioning(clientConn42, scopeDesc42);
        //              //clientProvision42.Apply();
        //              //#endregion
        //              ////State_Master
        //              //#region
        //              //SqlConnection serverConn43 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn43 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc43 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope43, serverConn43);
        //              //SqlSyncScopeProvisioning clientProvision43 = new SqlSyncScopeProvisioning(clientConn43, scopeDesc43);
        //              //clientProvision43.Apply();
        //              //#endregion
        //              ////System_Master
        //              //#region
        //              //SqlConnection serverConn44 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn44 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc44 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope44, serverConn44);
        //              //SqlSyncScopeProvisioning clientProvision44 = new SqlSyncScopeProvisioning(clientConn44, scopeDesc44);
        //              //clientProvision44.Apply();
        //              //#endregion
        //              ////TaxSlabMaster
        //              //#region
        //              //SqlConnection serverConn45 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn45 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc45 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope45, serverConn45);
        //              //SqlSyncScopeProvisioning clientProvision45 = new SqlSyncScopeProvisioning(clientConn45, scopeDesc45);
        //              //clientProvision45.Apply();
        //              //#endregion
        //              ////TaxType
        //              //#region
        //              //SqlConnection serverConn46 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn46 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc46 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope46, serverConn46);
        //              //SqlSyncScopeProvisioning clientProvision46 = new SqlSyncScopeProvisioning(clientConn46, scopeDesc46);
        //              //clientProvision46.Apply();
        //              //#endregion
        //              ////tblcomplainmaster
        //              //#region
        //              //SqlConnection serverConn47 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn47 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc47 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope47, serverConn47);
        //              //SqlSyncScopeProvisioning clientProvision47 = new SqlSyncScopeProvisioning(clientConn47, scopeDesc47);
        //              //clientProvision47.Apply();
        //              //#endregion
        //              ////tblfinishedgoodsmaster
        //              //#region
        //              //SqlConnection serverConn48 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn48 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc48 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope48, serverConn48);
        //              //SqlSyncScopeProvisioning clientProvision48 = new SqlSyncScopeProvisioning(clientConn48, scopeDesc48);
        //              //clientProvision48.Apply();
        //              //#endregion
        //              ////tblfinishedgoodsqty
        //              //#region
        //              //SqlConnection serverConn49 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn49 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc49 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope49, serverConn49);
        //              //SqlSyncScopeProvisioning clientProvision49 = new SqlSyncScopeProvisioning(clientConn49, scopeDesc49);
        //              //clientProvision49.Apply();
        //              //#endregion
        //              ////tblitemcomplainmaster
        //              //#region
        //              //SqlConnection serverConn50 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn50 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc50 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope50, serverConn50);
        //              //SqlSyncScopeProvisioning clientProvision50 = new SqlSyncScopeProvisioning(clientConn50, scopeDesc50);
        //              //clientProvision50.Apply();
        //              //#endregion
        //              ////tblitemreceivefromcompany
        //              //#region
        //              //SqlConnection serverConn51 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn51 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc51 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope51, serverConn51);
        //              //SqlSyncScopeProvisioning clientProvision51 = new SqlSyncScopeProvisioning(clientConn51, scopeDesc51);
        //              //clientProvision51.Apply();
        //              //#endregion
        //              ////tblitemsendtocustomer
        //              //#region
        //              //SqlConnection serverConn52 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn52 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc52 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope52, serverConn52);
        //              //SqlSyncScopeProvisioning clientProvision52 = new SqlSyncScopeProvisioning(clientConn52, scopeDesc52);
        //              //clientProvision52.Apply();
        //              //#endregion
        //              ////tblprocessmaster
        //              //#region
        //              //SqlConnection serverConn53 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn53 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc53 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope53, serverConn53);
        //              //SqlSyncScopeProvisioning clientProvision53 = new SqlSyncScopeProvisioning(clientConn53, scopeDesc53);
        //              //clientProvision53.Apply();
        //              //#endregion
        //              ////tblproductgeneratedmaster
        //              //#region
        //              //SqlConnection serverConn54 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn54 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc54 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope54, serverConn54);
        //              //SqlSyncScopeProvisioning clientProvision54 = new SqlSyncScopeProvisioning(clientConn54, scopeDesc54);
        //              //clientProvision54.Apply();
        //              //#endregion
        //              ////tblproductionmaster
        //              //#region
        //              //SqlConnection serverConn55 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn55 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc55 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope55, serverConn55);
        //              //SqlSyncScopeProvisioning clientProvision55 = new SqlSyncScopeProvisioning(clientConn55, scopeDesc55);
        //              //clientProvision55.Apply();
        //              //#endregion
        //              ////tblproductionrawmaterialmaster
        //              //#region
        //              //SqlConnection serverConn56 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn56 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc56 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope56, serverConn56);
        //              //SqlSyncScopeProvisioning clientProvision56 = new SqlSyncScopeProvisioning(clientConn56, scopeDesc56);
        //              //clientProvision56.Apply();
        //              //#endregion
        //              ////tblreceivefromcompany
        //              //#region
        //              //SqlConnection serverConn57 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn57 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc57 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope57, serverConn57);
        //              //SqlSyncScopeProvisioning clientProvision57 = new SqlSyncScopeProvisioning(clientConn57, scopeDesc57);
        //              //clientProvision57.Apply();
        //              //#endregion
        //              ////tblrowmaterialsmaster
        //              //#region
        //              //SqlConnection serverConn58 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn58 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc58 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope58, serverConn58);
        //              //SqlSyncScopeProvisioning clientProvision58 = new SqlSyncScopeProvisioning(clientConn58, scopeDesc58);
        //              //clientProvision58.Apply();
        //              //#endregion
        //              ////tblsendtocompanyitemmaster
        //              //#region
        //              //SqlConnection serverConn59 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn59 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc59 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope59, serverConn59);
        //              //SqlSyncScopeProvisioning clientProvision59 = new SqlSyncScopeProvisioning(clientConn59, scopeDesc59);
        //              //clientProvision59.Apply();
        //              //#endregion
        //              ////tblsendtocompanymaster
        //              //#region
        //              //SqlConnection serverConn60 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn60 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc60 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope60, serverConn60);
        //              //SqlSyncScopeProvisioning clientProvision60 = new SqlSyncScopeProvisioning(clientConn60, scopeDesc60);
        //              //clientProvision60.Apply();
        //              //#endregion
        //              ////tblsendtocustomer
        //              //#region
        //              //SqlConnection serverConn61 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn61 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc61 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope61, serverConn61);
        //              //SqlSyncScopeProvisioning clientProvision61 = new SqlSyncScopeProvisioning(clientConn61, scopeDesc61);
        //              //clientProvision61.Apply();
        //              //#endregion
        //              ////tbluser_employeetype
        //              //#region
        //              //SqlConnection serverConn62 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn62 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc62 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope62, serverConn62);
        //              //SqlSyncScopeProvisioning clientProvision62 = new SqlSyncScopeProvisioning(clientConn62, scopeDesc62);
        //              //clientProvision62.Apply();
        //              //#endregion
        //              ////tocompany
        //              //#region
        //              //SqlConnection serverConn63 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn63 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc63 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope63, serverConn63);
        //              //SqlSyncScopeProvisioning clientProvision63 = new SqlSyncScopeProvisioning(clientConn63, scopeDesc63);
        //              //clientProvision63.Apply();
        //              //#endregion
        //              ////UnitMaster
        //              //#region
        //              //SqlConnection serverConn64 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn64 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc64 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope64, serverConn64);
        //              //SqlSyncScopeProvisioning clientProvision64 = new SqlSyncScopeProvisioning(clientConn64, scopeDesc64);
        //              //clientProvision64.Apply();
        //              //#endregion
        //              ////updatedatabase
        //              //#region
        //              //SqlConnection serverConn65 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn65 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc65 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope65, serverConn65);
        //              //SqlSyncScopeProvisioning clientProvision65 = new SqlSyncScopeProvisioning(clientConn65, scopeDesc65);
        //              //clientProvision65.Apply();
        //              //#endregion
        //              ////UserInfo
        //              //#region
        //              //SqlConnection serverConn66 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn66 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc66 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope66, serverConn66);
        //              //SqlSyncScopeProvisioning clientProvision66 = new SqlSyncScopeProvisioning(clientConn66, scopeDesc66);
        //              //clientProvision66.Apply();
        //              //#endregion
        //              ////UserRights
        //              //#region
        //              //SqlConnection serverConn67 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn67 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc67 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope67, serverConn67);
        //              //SqlSyncScopeProvisioning clientProvision67 = new SqlSyncScopeProvisioning(clientConn67, scopeDesc67);
        //              //clientProvision67.Apply();
        //              //#endregion
        //              ////Voucher
        //              //#region
        //              //SqlConnection serverConn68 = new SqlConnection(sServerConnection);
        //              //SqlConnection clientConn68 = new SqlConnection(sClientConnection);
        //              //DbSyncScopeDescription scopeDesc68 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope68, serverConn68);
        //              //SqlSyncScopeProvisioning clientProvision68 = new SqlSyncScopeProvisioning(clientConn68, scopeDesc68);
        //              //clientProvision68.Apply();
        //              //#endregion

        //          }
        //          catch(Exception ex)
        //          {
        //              MessageBox.Show("Client Side" + ex.Message);
        //          }
        //      }
        //      //public string Sync()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator = new SyncOrchestrator();
        //      //        syncOrchestrator.LocalProvider = new SqlSyncProvider(sScope, clientConn);
        //      //        syncOrchestrator.RemoteProvider = new SqlSyncProvider(sScope, serverConn);
        //      //        syncOrchestrator.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats = syncOrchestrator.Synchronize();
        //      //        string str = "Start Time: " + syncStats.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;

        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync1()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope1, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope1, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync2()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope2, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope2, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      public string Sync3()
        //      {
        //          try
        //          {
        //              SqlConnection serverConn = new SqlConnection(sServerConnection);
        //              SqlConnection clientConn = new SqlConnection(sClientConnection);
        //              SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //              syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope3, clientConn);
        //              syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope3, serverConn);
        //              syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //              ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //              SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //              string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //              str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //              str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //              str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //              str += String.Empty + Environment.NewLine;
        //              return str;
        //          }
        //          catch
        //          {
        //              return "";
        //          }
        //      }
        //      public string Sync4()
        //      {
        //          try
        //          {
        //              SqlConnection serverConn = new SqlConnection(sServerConnection);
        //              SqlConnection clientConn = new SqlConnection(sClientConnection);
        //              SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //              syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope4, clientConn);
        //              syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope4, serverConn);
        //              syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //              ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //              SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //              string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //              str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //              str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //              str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //              str += String.Empty + Environment.NewLine;
        //              return str;
        //          }
        //          catch
        //          {
        //              return "";
        //          }
        //      }
        //      //public string Sync5()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope5, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope5, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync6()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope6, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope6, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      public string Sync7()
        //      {
        //          try
        //          {
        //              SqlConnection serverConn = new SqlConnection(sServerConnection);
        //              SqlConnection clientConn = new SqlConnection(sClientConnection);
        //              SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //              syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope7, clientConn);
        //              syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope7, serverConn);
        //              syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //              ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //              SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //              string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //              str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //              str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //              str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //              str += String.Empty + Environment.NewLine;
        //              return str;
        //          }
        //          catch
        //          {
        //              return "";
        //          }
        //      }
        //      //public string Sync8()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope8, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope8, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync9()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope9, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope9, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync10()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator = new SyncOrchestrator();
        //      //        syncOrchestrator.LocalProvider = new SqlSyncProvider(sScope10, clientConn);
        //      //        syncOrchestrator.RemoteProvider = new SqlSyncProvider(sScope10, serverConn);
        //      //        syncOrchestrator.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats = syncOrchestrator.Synchronize();
        //      //        string str = "Start Time: " + syncStats.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;

        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync11()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope11, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope11, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync12()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope12, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope12, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync13()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope13, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope13, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync14()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope14, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope14, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync15()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope15, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope15, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync16()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope16, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope16, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync17()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope17, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope17, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync18()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope18, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope18, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync19()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope19, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope19, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync20()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator = new SyncOrchestrator();
        //      //        syncOrchestrator.LocalProvider = new SqlSyncProvider(sScope20, clientConn);
        //      //        syncOrchestrator.RemoteProvider = new SqlSyncProvider(sScope20, serverConn);
        //      //        syncOrchestrator.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats = syncOrchestrator.Synchronize();
        //      //        string str = "Start Time: " + syncStats.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;

        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync21()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope21, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope21, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync22()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope22, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope22, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync23()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope23, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope23, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync24()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope24, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope24, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      public string Sync25()
        //      {
        //          try
        //          {
        //              SqlConnection serverConn = new SqlConnection(sServerConnection);
        //              SqlConnection clientConn = new SqlConnection(sClientConnection);
        //              SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //              syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope25, clientConn);
        //              syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope25, serverConn);
        //              syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //              ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //              SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //              string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //              str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //              str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //              str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //              str += String.Empty + Environment.NewLine;
        //              return str;
        //          }
        //          catch
        //          {
        //              return "";
        //          }
        //      }
        //      //public string Sync26()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope26, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope26, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync27()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope27, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope27, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync28()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope28, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope28, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync29()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope29, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope29, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync30()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator = new SyncOrchestrator();
        //      //        syncOrchestrator.LocalProvider = new SqlSyncProvider(sScope30, clientConn);
        //      //        syncOrchestrator.RemoteProvider = new SqlSyncProvider(sScope30, serverConn);
        //      //        syncOrchestrator.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats = syncOrchestrator.Synchronize();
        //      //        string str = "Start Time: " + syncStats.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;

        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync31()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope31, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope31, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync32()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope32, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope32, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync33()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope33, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope33, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync34()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope34, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope34, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync35()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope35, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope35, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync36()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope36, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope36, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync37()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope37, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope37, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      public string Sync38()
        //      {
        //          try
        //          {
        //              SqlConnection serverConn = new SqlConnection(sServerConnection);
        //              SqlConnection clientConn = new SqlConnection(sClientConnection);
        //              SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //              syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope38, clientConn);
        //              syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope38, serverConn);
        //              syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //              ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //              SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //              string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //              str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //              str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //              str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //              str += String.Empty + Environment.NewLine;
        //              return str;
        //          }
        //          catch
        //          {
        //              return "";
        //          }
        //      }
        //      public string Sync39()
        //      {
        //          try
        //          {
        //              SqlConnection serverConn = new SqlConnection(sServerConnection);
        //              SqlConnection clientConn = new SqlConnection(sClientConnection);
        //              SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //              syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope39, clientConn);
        //              syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope39, serverConn);
        //              syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //              ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //              SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //              string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //              str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //              str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //              str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //              str += String.Empty + Environment.NewLine;
        //              return str;
        //          }
        //          catch
        //          {
        //              return "";
        //          }
        //      }
        //      public string Sync40()
        //      {
        //          try
        //          {
        //              SqlConnection serverConn = new SqlConnection(sServerConnection);
        //              SqlConnection clientConn = new SqlConnection(sClientConnection);
        //              SyncOrchestrator syncOrchestrator = new SyncOrchestrator();
        //              syncOrchestrator.LocalProvider = new SqlSyncProvider(sScope40, clientConn);
        //              syncOrchestrator.RemoteProvider = new SqlSyncProvider(sScope40, serverConn);
        //              syncOrchestrator.Direction = SyncDirectionOrder.UploadAndDownload;
        //              ((SqlSyncProvider)syncOrchestrator.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //              SyncOperationStatistics syncStats = syncOrchestrator.Synchronize();
        //              string str = "Start Time: " + syncStats.SyncStartTime + Environment.NewLine;
        //              str += "Total Changes Uploaded: " + syncStats.UploadChangesTotal + Environment.NewLine;
        //              str += "Total Changes Downloaded: " + syncStats.DownloadChangesTotal + Environment.NewLine;
        //              str += "Complete Time: " + syncStats.SyncEndTime + Environment.NewLine;
        //              str += String.Empty + Environment.NewLine;
        //              return str;

        //          }
        //          catch
        //          {
        //              return "";
        //          }
        //      }
        //      //public string Sync41()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope41, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope41, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      public string Sync42()
        //      {
        //          try
        //          {
        //              SqlConnection serverConn = new SqlConnection(sServerConnection);
        //              SqlConnection clientConn = new SqlConnection(sClientConnection);
        //              SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //              syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope42, clientConn);
        //              syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope42, serverConn);
        //              syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //              ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //              SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //              string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //              str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //              str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //              str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //              str += String.Empty + Environment.NewLine;
        //              return str;
        //          }
        //          catch
        //          {
        //              return "";
        //          }
        //      }
        //      //public string Sync43()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope43, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope43, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync44()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope44, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope44, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync45()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope45, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope45, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync46()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope46, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope46, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync47()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope47, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope47, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync48()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope48, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope48, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync49()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope49, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope49, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync50()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator = new SyncOrchestrator();
        //      //        syncOrchestrator.LocalProvider = new SqlSyncProvider(sScope50, clientConn);
        //      //        syncOrchestrator.RemoteProvider = new SqlSyncProvider(sScope50, serverConn);
        //      //        syncOrchestrator.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats = syncOrchestrator.Synchronize();
        //      //        string str = "Start Time: " + syncStats.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;

        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync51()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope51, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope51, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync52()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope52, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope52, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync53()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope53, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope53, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync54()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope54, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope54, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync55()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope55, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope55, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync56()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope56, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope56, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync57()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope57, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope57, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync58()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope58, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope58, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync59()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope59, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope59, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync60()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator = new SyncOrchestrator();
        //      //        syncOrchestrator.LocalProvider = new SqlSyncProvider(sScope60, clientConn);
        //      //        syncOrchestrator.RemoteProvider = new SqlSyncProvider(sScope60, serverConn);
        //      //        syncOrchestrator.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats = syncOrchestrator.Synchronize();
        //      //        string str = "Start Time: " + syncStats.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;

        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync61()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope61, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope61, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync62()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope62, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope62, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync63()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope63, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope63, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync64()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope64, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope64, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync65()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope65, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope65, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync66()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope66, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope66, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync67()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope67, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope67, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}
        //      //public string Sync68()
        //      //{
        //      //    try
        //      //    {
        //      //        SqlConnection serverConn = new SqlConnection(sServerConnection);
        //      //        SqlConnection clientConn = new SqlConnection(sClientConnection);
        //      //        SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
        //      //        syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope68, clientConn);
        //      //        syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope68, serverConn);
        //      //        syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
        //      //        ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
        //      //        SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
        //      //        string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
        //      //        str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
        //      //        str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
        //      //        str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
        //      //        str += String.Empty + Environment.NewLine;
        //      //        return str;
        //      //    }
        //      //    catch
        //      //    {
        //      //        return "";
        //      //    }
        //      //}

        //      static void Program_ApplyChangeFailed(object sender, DbApplyChangeFailedEventArgs e)
        //      {
        //          Console.WriteLine(e.Conflict.Type);
        //          Console.WriteLine(e.Error);

        //      }
        #endregion
    }
}
