using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace RamdevSales
{
    class UpdateSoftware
    {
        Connection c = new Connection();
        public DataTable dt;
        public SqlConnection con;
        public void getcon()
        {

            // con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());

        }
        public void updatesof(string cn, string conpanyid)
        {
            SqlConnection con = new SqlConnection(cn);
            try
            {

                dt = c.getdataset("select max(updatecode) as updatecode from updatedatabase", con);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["updatecode"].ToString() == "" || dt.Rows[0]["updatecode"].ToString() == null)
                    {
                        c.execute("INSERT INTO [dbo].[updatedatabase]([updatecode])VALUES('" + "0" + "')", con);
                    }
                }
            }
            catch
            {
                string queryString = @"SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON SET ANSI_PADDING ON CREATE TABLE [dbo].[updatedatabase]([id] [int] IDENTITY(1,1) NOT NULL,[updatecode] [int] NULL,CONSTRAINT [PK_updatedatabase] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]SET ANSI_PADDING OFF";
                c.execute(queryString, con);
                c.execute("INSERT INTO [dbo].[updatedatabase]([updatecode])VALUES('" + "0" + "')", con);

            }
            dt = c.getdataset("select max(updatecode) as updatecode from updatedatabase", con);
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 170915)
                {
                    //all alter table
                    string option = @"Alter Table Options add cess bit";
                    c.execute(option, con);
                    string option1 = @"Alter Table Options add savedialogbox bit";
                    c.execute(option1, con);
                    string option2 = @"Alter Table Options add salevoucherno nvarchar(10)";
                    c.execute(option2, con);
                    string option3 = @"Alter Table Options add salervoucherno nvarchar(10)";
                    c.execute(option3, con);
                    string option4 = @"Alter Table Options add purchasevoucherno nvarchar(10)";
                    c.execute(option4, con);
                    string option5 = @"Alter Table Options add purchaservoucherno nvarchar(10)";
                    c.execute(option5, con);
                    string option6 = @"Alter Table Options add saleovoucherno nvarchar(10)";
                    c.execute(option6, con);
                    string option7 = @"Alter Table Options add salecvoucherno nvarchar(10)";
                    c.execute(option7, con);
                    string option8 = @"Alter Table Options add purchaseovoucherno nvarchar(10)";
                    c.execute(option8, con);
                    string option9 = @"Alter Table Options add purchasecvoucherno nvarchar(10)";
                    c.execute(option9, con);
                    string option10 = @"Alter Table Options add requiredcustomerdetailinpos bit";
                    c.execute(option10, con);
                    string billpos1 = @"Alter Table BillPOSMaster add customername nvarchar(MAX)";
                    c.execute(billpos1, con);
                    string billpos2 = @"Alter Table BillPOSMaster add customercity nvarchar(MAX)";
                    c.execute(billpos2, con);
                    string billpos3 = @"Alter Table BillPOSMaster add customermobile nvarchar(50)";
                    c.execute(billpos3, con);
                    string update = "update Options set cess='" + "1" + "', savedialogbox='" + "1" + "', salevoucherno='" + "Continuous" + "',salervoucherno='" + "Continuous" + "',purchasevoucherno='" + "Continuous" + "',purchaservoucherno='" + "Continuous" + "',saleovoucherno='" + "Continuous" + "',salecvoucherno='" + "Continuous" + "',purchaseovoucherno='" + "Continuous" + "',purchasecvoucherno='" + "Continuous" + "',requiredcustomerdetailinpos='" + "1" + "'";
                    c.execute(update, con);
                    //   c.execute("INSERT INTO [dbo].[updatedatabase]([updatecode])VALUES('" + "170915" + "' where id='"+"1"+"')", con);
                    //   c.execute("Update updatedatabase set updatecode='" + "170915" + "' where id='" + "1" + "')", con);
                    string u = "Update updatedatabase set updatecode='" + "170915" + "' where id='" + "1" + "'";
                    c.execute(u, con);


                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 170920)
                {
                    string purchase = @"Alter Table PurchasetypeMaster add chkecom bit";
                    c.execute(purchase, con);
                    string purchase1 = @"Alter Table PurchasetypeMaster add txtecom nvarchar(MAX)";
                    c.execute(purchase1, con);
                    string option11 = @"Alter Table Options add showsidebox bit";
                    c.execute(option11, con);
                    string update = "update Options set showsidebox='" + "1" + "'";
                    c.execute(update, con);
                    string update1 = "update PurchasetypeMaster set chkecom='" + "0" + "'";
                    c.execute(update1, con);
                    string u = "Update updatedatabase set updatecode='" + "170920" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 170921)
                {
                    string queryString = @"SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON SET ANSI_PADDING ON CREATE TABLE [dbo].[TaxSlabMaster](	[id] [bigint] IDENTITY(1,1) NOT NULL,[Taxslabname] [nvarchar](50) NULL,[saletypename] [nvarchar](50) NULL,[saletypeid] [varchar](50) NULL,[system] [varchar](50) NULL,[category] [varchar](50) NULL,[sgst] [numeric](18, 2) NULL,[cgst] [numeric](18, 2) NULL,[igst] [numeric](18, 2) NULL,[additax] [numeric](18, 2) NULL,[onwhich] [varchar](50) NULL,[isonmrp] [bit] NULL,[isonfreegoods] [bit] NULL,[isactive] [bit] NULL, CONSTRAINT [PK_TaxSlabMaster] PRIMARY KEY CLUSTERED([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]SET ANSI_PADDING OFF";
                    c.execute(queryString, con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST Tax Free', N'LOCAL GST SALE', N'1', N'GST', N'Goods', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST Tax Free', N'INTER STATE GST SALE', N'2', N'GST', N'Goods', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST Tax Free', N'LOCAL GST SALE RETURN', N'3', N'GST', N'Goods', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST Tax Free', N'LOCAL GST SALE ORDER', N'4', N'GST', N'Goods', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST Tax Free', N'LOCAL GST SALE CHALLAN', N'5', N'GST', N'Goods', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST Tax Free', N'LOCAL GST PURCHASE', N'6', N'GST', N'Goods', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST Tax Free', N'LOCAL GST PURCHASE RETURN', N'7', N'GST', N'Goods', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST Tax Free', N'LOCAL GST PURCHASE ORDER', N'8', N'GST', N'Goods', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST Tax Free', N'LOCAL GST PURCHASE CHALLAN', N'9', N'GST', N'Goods', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST Tax Free', N'INTER STATE GST SALE RETURN', N'10', N'GST', N'Goods', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST Tax Free', N'INTER STATE GST SALE ORDER', N'11', N'GST', N'Goods', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST Tax Free', N'INTER STATE GST SALE CHALLAN', N'12', N'GST', N'Goods', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST Tax Free', N'INTER STATE GST PURCHASE', N'13', N'GST', N'Goods', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST Tax Free', N'INTER STATE GST PURCHASE RETURN', N'14', N'GST', N'Goods', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST Tax Free', N'INTER STATE GST PURCHASE ORDER', N'15', N'GST', N'Goods', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST Tax Free', N'INTER STATE GST PURCHASE CHALLAN', N'16', N'GST', N'Goods', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 5%', N'LOCAL GST SALE', N'1', N'GST', N'Goods', CAST(2.50 AS Numeric(18, 2)), CAST(2.50 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 5%', N'INTER STATE GST SALE', N'2', N'GST', N'Goods', CAST(2.50 AS Numeric(18, 2)), CAST(2.50 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 5%', N'LOCAL GST SALE RETURN', N'3', N'GST', N'Goods', CAST(2.50 AS Numeric(18, 2)), CAST(2.50 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 5%', N'LOCAL GST SALE ORDER', N'4', N'GST', N'Goods', CAST(2.50 AS Numeric(18, 2)), CAST(2.50 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 5%', N'LOCAL GST SALE CHALLAN', N'5', N'GST', N'Goods', CAST(2.50 AS Numeric(18, 2)), CAST(2.50 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 5%', N'LOCAL GST PURCHASE', N'6', N'GST', N'Goods', CAST(2.50 AS Numeric(18, 2)), CAST(2.50 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 5%', N'LOCAL GST PURCHASE RETURN', N'7', N'GST', N'Goods', CAST(2.50 AS Numeric(18, 2)), CAST(2.50 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 5%', N'LOCAL GST PURCHASE ORDER', N'8', N'GST', N'Goods', CAST(2.50 AS Numeric(18, 2)), CAST(2.50 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 5%', N'LOCAL GST PURCHASE CHALLAN', N'9', N'GST', N'Goods', CAST(2.50 AS Numeric(18, 2)), CAST(2.50 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 5%', N'INTER STATE GST SALE RETURN', N'10', N'GST', N'Goods', CAST(2.50 AS Numeric(18, 2)), CAST(2.50 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 5%', N'INTER STATE GST SALE ORDER', N'11', N'GST', N'Goods', CAST(2.50 AS Numeric(18, 2)), CAST(2.50 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 5%', N'INTER STATE GST SALE CHALLAN', N'12', N'GST', N'Goods', CAST(2.50 AS Numeric(18, 2)), CAST(2.50 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 5%', N'INTER STATE GST PURCHASE', N'13', N'GST', N'Goods', CAST(2.50 AS Numeric(18, 2)), CAST(2.50 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 5%', N'INTER STATE GST PURCHASE RETURN', N'14', N'GST', N'Goods', CAST(2.50 AS Numeric(18, 2)), CAST(2.50 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 5%', N'INTER STATE GST PURCHASE ORDER', N'15', N'GST', N'Goods', CAST(2.50 AS Numeric(18, 2)), CAST(2.50 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 5%', N'INTER STATE GST PURCHASE CHALLAN', N'16', N'GST', N'Goods', CAST(2.50 AS Numeric(18, 2)), CAST(2.50 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 12%', N'LOCAL GST SALE', N'1', N'GST', N'Goods', CAST(6.00 AS Numeric(18, 2)), CAST(6.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 12%', N'INTER STATE GST SALE', N'2', N'GST', N'Goods', CAST(6.00 AS Numeric(18, 2)), CAST(6.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 12%', N'LOCAL GST SALE RETURN', N'3', N'GST', N'Goods', CAST(6.00 AS Numeric(18, 2)), CAST(6.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 12%', N'LOCAL GST SALE ORDER', N'4', N'GST', N'Goods', CAST(6.00 AS Numeric(18, 2)), CAST(6.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 12%', N'LOCAL GST SALE CHALLAN', N'5', N'GST', N'Goods', CAST(6.00 AS Numeric(18, 2)), CAST(6.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 12%', N'LOCAL GST PURCHASE', N'6', N'GST', N'Goods', CAST(6.00 AS Numeric(18, 2)), CAST(6.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 12%', N'LOCAL GST PURCHASE RETURN', N'7', N'GST', N'Goods', CAST(6.00 AS Numeric(18, 2)), CAST(6.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 12%', N'LOCAL GST PURCHASE ORDER', N'8', N'GST', N'Goods', CAST(6.00 AS Numeric(18, 2)), CAST(6.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 12%', N'LOCAL GST PURCHASE CHALLAN', N'9', N'GST', N'Goods', CAST(6.00 AS Numeric(18, 2)), CAST(6.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 12%', N'INTER STATE GST SALE RETURN', N'10', N'GST', N'Goods', CAST(6.00 AS Numeric(18, 2)), CAST(6.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 12%', N'INTER STATE GST SALE ORDER', N'11', N'GST', N'Goods', CAST(6.00 AS Numeric(18, 2)), CAST(6.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 12%', N'INTER STATE GST SALE CHALLAN', N'12', N'GST', N'Goods', CAST(6.00 AS Numeric(18, 2)), CAST(6.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 12%', N'INTER STATE GST PURCHASE', N'13', N'GST', N'Goods', CAST(6.00 AS Numeric(18, 2)), CAST(6.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 12%', N'INTER STATE GST PURCHASE RETURN', N'14', N'GST', N'Goods', CAST(6.00 AS Numeric(18, 2)), CAST(6.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 12%', N'INTER STATE GST PURCHASE ORDER', N'15', N'GST', N'Goods', CAST(6.00 AS Numeric(18, 2)), CAST(6.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 12%', N'INTER STATE GST PURCHASE CHALLAN', N'16', N'GST', N'Goods', CAST(6.00 AS Numeric(18, 2)), CAST(6.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 18%', N'LOCAL GST SALE', N'1', N'GST', N'Goods', CAST(9.00 AS Numeric(18, 2)), CAST(9.00 AS Numeric(18, 2)), CAST(18.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 18%', N'INTER STATE GST SALE', N'2', N'GST', N'Goods', CAST(9.00 AS Numeric(18, 2)), CAST(9.00 AS Numeric(18, 2)), CAST(18.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 18%', N'LOCAL GST SALE RETURN', N'3', N'GST', N'Goods', CAST(9.00 AS Numeric(18, 2)), CAST(9.00 AS Numeric(18, 2)), CAST(18.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 18%', N'LOCAL GST SALE ORDER', N'4', N'GST', N'Goods', CAST(9.00 AS Numeric(18, 2)), CAST(9.00 AS Numeric(18, 2)), CAST(18.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 18%', N'LOCAL GST SALE CHALLAN', N'5', N'GST', N'Goods', CAST(9.00 AS Numeric(18, 2)), CAST(9.00 AS Numeric(18, 2)), CAST(18.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 18%', N'LOCAL GST PURCHASE', N'6', N'GST', N'Goods', CAST(9.00 AS Numeric(18, 2)), CAST(9.00 AS Numeric(18, 2)), CAST(18.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 18%', N'LOCAL GST PURCHASE RETURN', N'7', N'GST', N'Goods', CAST(9.00 AS Numeric(18, 2)), CAST(9.00 AS Numeric(18, 2)), CAST(18.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 18%', N'LOCAL GST PURCHASE ORDER', N'8', N'GST', N'Goods', CAST(9.00 AS Numeric(18, 2)), CAST(9.00 AS Numeric(18, 2)), CAST(18.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 18%', N'LOCAL GST PURCHASE CHALLAN', N'9', N'GST', N'Goods', CAST(9.00 AS Numeric(18, 2)), CAST(9.00 AS Numeric(18, 2)), CAST(18.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 18%', N'INTER STATE GST SALE RETURN', N'10', N'GST', N'Goods', CAST(9.00 AS Numeric(18, 2)), CAST(9.00 AS Numeric(18, 2)), CAST(18.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 18%', N'INTER STATE GST SALE ORDER', N'11', N'GST', N'Goods', CAST(9.00 AS Numeric(18, 2)), CAST(9.00 AS Numeric(18, 2)), CAST(18.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 18%', N'INTER STATE GST SALE CHALLAN', N'12', N'GST', N'Goods', CAST(9.00 AS Numeric(18, 2)), CAST(9.00 AS Numeric(18, 2)), CAST(18.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 18%', N'INTER STATE GST PURCHASE', N'13', N'GST', N'Goods', CAST(9.00 AS Numeric(18, 2)), CAST(9.00 AS Numeric(18, 2)), CAST(18.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 18%', N'INTER STATE GST PURCHASE RETURN', N'14', N'GST', N'Goods', CAST(9.00 AS Numeric(18, 2)), CAST(9.00 AS Numeric(18, 2)), CAST(18.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 18%', N'INTER STATE GST PURCHASE ORDER', N'15', N'GST', N'Goods', CAST(9.00 AS Numeric(18, 2)), CAST(9.00 AS Numeric(18, 2)), CAST(18.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 18%', N'INTER STATE GST PURCHASE CHALLAN', N'16', N'GST', N'Goods', CAST(9.00 AS Numeric(18, 2)), CAST(9.00 AS Numeric(18, 2)), CAST(18.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 28%', N'LOCAL GST SALE', N'1', N'GST', N'Goods', CAST(14.00 AS Numeric(18, 2)), CAST(14.00 AS Numeric(18, 2)), CAST(28.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 28%', N'INTER STATE GST SALE', N'2', N'GST', N'Goods', CAST(14.00 AS Numeric(18, 2)), CAST(14.00 AS Numeric(18, 2)), CAST(28.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 28%', N'LOCAL GST SALE RETURN', N'3', N'GST', N'Goods', CAST(14.00 AS Numeric(18, 2)), CAST(14.00 AS Numeric(18, 2)), CAST(28.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 28%', N'LOCAL GST SALE ORDER', N'4', N'GST', N'Goods', CAST(14.00 AS Numeric(18, 2)), CAST(14.00 AS Numeric(18, 2)), CAST(28.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 28%', N'LOCAL GST SALE CHALLAN', N'5', N'GST', N'Goods', CAST(14.00 AS Numeric(18, 2)), CAST(14.00 AS Numeric(18, 2)), CAST(28.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 28%', N'LOCAL GST PURCHASE', N'6', N'GST', N'Goods', CAST(14.00 AS Numeric(18, 2)), CAST(14.00 AS Numeric(18, 2)), CAST(28.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 28%', N'LOCAL GST PURCHASE RETURN', N'7', N'GST', N'Goods', CAST(14.00 AS Numeric(18, 2)), CAST(14.00 AS Numeric(18, 2)), CAST(28.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 28%', N'LOCAL GST PURCHASE ORDER', N'8', N'GST', N'Goods', CAST(14.00 AS Numeric(18, 2)), CAST(14.00 AS Numeric(18, 2)), CAST(28.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 28%', N'LOCAL GST PURCHASE CHALLAN', N'9', N'GST', N'Goods', CAST(14.00 AS Numeric(18, 2)), CAST(14.00 AS Numeric(18, 2)), CAST(28.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 28%', N'INTER STATE GST SALE RETURN', N'10', N'GST', N'Goods', CAST(14.00 AS Numeric(18, 2)), CAST(14.00 AS Numeric(18, 2)), CAST(28.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 28%', N'INTER STATE GST SALE ORDER', N'11', N'GST', N'Goods', CAST(14.00 AS Numeric(18, 2)), CAST(14.00 AS Numeric(18, 2)), CAST(28.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 28%', N'INTER STATE GST SALE CHALLAN', N'12', N'GST', N'Goods', CAST(14.00 AS Numeric(18, 2)), CAST(14.00 AS Numeric(18, 2)), CAST(28.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 28%', N'INTER STATE GST PURCHASE', N'13', N'GST', N'Goods', CAST(14.00 AS Numeric(18, 2)), CAST(14.00 AS Numeric(18, 2)), CAST(28.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 28%', N'INTER STATE GST PURCHASE RETURN', N'14', N'GST', N'Goods', CAST(14.00 AS Numeric(18, 2)), CAST(14.00 AS Numeric(18, 2)), CAST(28.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 28%', N'INTER STATE GST PURCHASE ORDER', N'15', N'GST', N'Goods', CAST(14.00 AS Numeric(18, 2)), CAST(14.00 AS Numeric(18, 2)), CAST(28.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ( [Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'GST 28%', N'INTER STATE GST PURCHASE CHALLAN', N'16', N'GST', N'Goods', CAST(14.00 AS Numeric(18, 2)), CAST(14.00 AS Numeric(18, 2)), CAST(28.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    string taxslab = @"Alter Table ProductMaster add taxslab nvarchar(50)";
                    c.execute(taxslab, con);
                    string itemgroup = @"SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON CREATE TABLE [dbo].[ItemGroupMaster]([id] [int] IDENTITY(1,1) NOT NULL,[ItemGroupName] [nvarchar](50) NULL, CONSTRAINT [PK_ItemGroupMaster] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]SET ANSI_PADDING OFF";
                    c.execute(itemgroup, con);
                    string itemunit = @"SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON CREATE TABLE [dbo].[UnitMaster]([id] [int] IDENTITY(1,1) NOT NULL,[UnitName] [nvarchar](50) NULL,CONSTRAINT [PK_UnitMaster] PRIMARY KEY CLUSTERED([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]SET ANSI_PADDING OFF";
                    c.execute(itemunit, con);
                    c.execute("INSERT [dbo].[ItemGroupMaster] ([ItemGroupName]) VALUES ( N'General')", con);
                    c.execute("INSERT [dbo].[UnitMaster] ( [UnitName]) VALUES ( N'Bag')", con);
                    c.execute("INSERT [dbo].[UnitMaster] ( [UnitName]) VALUES ( N'Box')", con);
                    c.execute("INSERT [dbo].[UnitMaster] ( [UnitName]) VALUES ( N'Dozen')", con);
                    c.execute("INSERT [dbo].[UnitMaster] ( [UnitName]) VALUES ( N'Gms.')", con);
                    c.execute("INSERT [dbo].[UnitMaster] ( [UnitName]) VALUES ( N'Kgs.')", con);
                    c.execute("INSERT [dbo].[UnitMaster] ( [UnitName]) VALUES ( N'Ltr.')", con);
                    c.execute("INSERT [dbo].[UnitMaster] ( [UnitName]) VALUES ( N'Pcs.')", con);
                    c.execute("INSERT [dbo].[UnitMaster] ( [UnitName]) VALUES ( N'Qntl')", con);


                    string u = "Update updatedatabase set updatecode='" + "170921" + "' where id='" + "1" + "'";
                    c.execute(u, con);

                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 170922)
                {
                    string op = "Update Options Set autosaletype='" + "LOCAL GST SALE" + "'";
                    c.execute(op, con);
                    string sopm = @"Alter Table SaleOrderProductMaster add cess [numeric](18, 2)";
                    c.execute(sopm, con);
                    string som = @"Alter Table SaleOrderMaster add totalcess [numeric](18, 2)";
                    c.execute(som, con);
                    string u = "Update updatedatabase set updatecode='" + "170922" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 170923)
                {
                    string sopm = @"Alter Table BillPOSMaster add totalcess [numeric](18, 2)";
                    c.execute(sopm, con);
                    string sopm1 = @"Alter Table BillPOSMaster add statecode nvarchar(50)";
                    c.execute(sopm1, con);
                    string sopm2 = @"Alter Table BillPOSMaster add companystate nvarchar(50)";
                    c.execute(sopm2, con);
                    string sopm3 = @"Alter Table BillPOSMaster add billno nvarchar(50)";
                    c.execute(sopm3, con);
                    string pos1 = @"Alter Table BillPOSProductMaster add cess nvarchar(50)";
                    c.execute(pos1, con);
                    string pos2 = @"Alter Table BillPOSProductMaster add billno nvarchar(50)";
                    c.execute(pos2, con);
                    string s = @"Alter Table BillPOSMaster add saletypeid int";
                    c.execute(s, con);
                    string u = "Update updatedatabase set updatecode='" + "170923" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 170925)
                {
                    c.execute("INSERT [dbo].[PurchasetypeMaster] ([Purchasetypename], [Groupid], [taxtypeid], [TaxTypename], [Region], [isactive], [Prefix], [startingno], [Type], [TaxCalculation], [PickupPrice], [InvoiceHeading], [FormType], [chkecom], [txtecom]) VALUES (N'BILL OF SUPPLY FOR SALE', 23, 2, N'Tax Inclusive', N'Local', 1, N'BOS-', N'1', N'S', N'Bill Of Supply', N'BasicPrice', N'BILL OF SUPPLY', N'S', 0, N'')", con);
                    c.execute("INSERT [dbo].[PurchasetypeMaster] ([Purchasetypename], [Groupid], [taxtypeid], [TaxTypename], [Region], [isactive], [Prefix], [startingno], [Type], [TaxCalculation], [PickupPrice], [InvoiceHeading], [FormType], [chkecom], [txtecom]) VALUES (N'BILL OF SUPPLY FOR SALE ORDER', 23, 2, N'Tax Inclusive', N'Local', 1, N'BOS-', N'1', N'S', N'Bill Of Supply', N'BasicPrice', N'BILL OF SUPPLY', N'SO', 0, N'')", con);
                    c.execute("INSERT [dbo].[PurchasetypeMaster] ([Purchasetypename], [Groupid], [taxtypeid], [TaxTypename], [Region], [isactive], [Prefix], [startingno], [Type], [TaxCalculation], [PickupPrice], [InvoiceHeading], [FormType], [chkecom], [txtecom]) VALUES (N'BILL OF SUPPLY FOR SALE RETURN', 23, 2, N'Tax Inclusive', N'Local', 1, N'BOS-', N'1', N'S', N'Bill Of Supply', N'BasicPrice', N'BILL OF SUPPLY', N'SR', 0, N'')", con);
                    c.execute("INSERT [dbo].[PurchasetypeMaster] ([Purchasetypename], [Groupid], [taxtypeid], [TaxTypename], [Region], [isactive], [Prefix], [startingno], [Type], [TaxCalculation], [PickupPrice], [InvoiceHeading], [FormType], [chkecom], [txtecom]) VALUES (N'BILL OF SUPPLY FOR SALE CHALLAN', 23, 2, N'Tax Inclusive', N'Local', 1, N'BOS-', N'1', N'S', N'Bill Of Supply', N'BasicPrice', N'BILL OF SUPPLY', N'SC', 0, N'')", con);
                    c.execute("INSERT [dbo].[PurchasetypeMaster] ([Purchasetypename], [Groupid], [taxtypeid], [TaxTypename], [Region], [isactive], [Prefix], [startingno], [Type], [TaxCalculation], [PickupPrice], [InvoiceHeading], [FormType], [chkecom], [txtecom]) VALUES (N'BILL OF SUPPLY FOR PURCHASE', 22, 2, N'Tax Inclusive', N'Local', 1, N'BOS-', N'1', N'P', N'Bill Of Supply', N'BasicPrice', N'BILL OF SUPPLY', N'P', 0, N'')", con);
                    c.execute("INSERT [dbo].[PurchasetypeMaster] ([Purchasetypename], [Groupid], [taxtypeid], [TaxTypename], [Region], [isactive], [Prefix], [startingno], [Type], [TaxCalculation], [PickupPrice], [InvoiceHeading], [FormType], [chkecom], [txtecom]) VALUES (N'BILL OF SUPPLY FOR PURCHASE ORDER', 22, 2, N'Tax Inclusive', N'Local', 1, N'BOS-', N'1', N'P', N'Bill Of Supply', N'BasicPrice', N'BILL OF SUPPLY', N'PO', 0, N'')", con);
                    c.execute("INSERT [dbo].[PurchasetypeMaster] ([Purchasetypename], [Groupid], [taxtypeid], [TaxTypename], [Region], [isactive], [Prefix], [startingno], [Type], [TaxCalculation], [PickupPrice], [InvoiceHeading], [FormType], [chkecom], [txtecom]) VALUES (N'BILL OF SUPPLY FOR PURCHASE RETURN', 22, 2, N'Tax Inclusive', N'Local', 1, N'BOS-', N'1', N'P', N'Bill Of Supply', N'BasicPrice', N'BILL OF SUPPLY', N'PR', 0, N'')", con);
                    c.execute("INSERT [dbo].[PurchasetypeMaster] ([Purchasetypename], [Groupid], [taxtypeid], [TaxTypename], [Region], [isactive], [Prefix], [startingno], [Type], [TaxCalculation], [PickupPrice], [InvoiceHeading], [FormType], [chkecom], [txtecom]) VALUES (N'BILL OF SUPPLY FOR PURCHASE CHALLAN', 22, 2, N'Tax Inclusive', N'Local', 1, N'BOS-', N'1', N'P', N'Bill Of Supply', N'BasicPrice', N'BILL OF SUPPLY', N'PC', 0, N'')", con);

                    c.execute("delete from TaxSlabMaster", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'TAX FREE', N'BILL OF SUPPLY FOR SALE ORDER', N'18', N'GST', N'Goods', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'TAX FREE', N'BILL OF SUPPLY FOR SALE RETURN', N'19', N'GST', N'Goods', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'TAX FREE', N'BILL OF SUPPLY FOR SALE CHALLAN', N'20', N'GST', N'Goods', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'TAX FREE', N'BILL OF SUPPLY FOR PURCHASE', N'21', N'GST', N'Goods', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'TAX FREE', N'BILL OF SUPPLY FOR PURCHASE ORDER', N'22', N'GST', N'Goods', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'TAX FREE', N'BILL OF SUPPLY FOR PURCHASE RETURN', N'23', N'GST', N'Goods', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'TAX FREE', N'BILL OF SUPPLY FOR PURCHASE CHALLAN', N'24', N'GST', N'Goods', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'TAX FREE', N'BILL OF SUPPLY FOR SALE', N'26', N'GST', N'Goods', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);

                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 5%', N'BILL OF SUPPLY FOR SALE ORDER', N'18', N'GST', N'Goods', CAST(2.50 AS Numeric(18, 2)), CAST(2.50 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 5%', N'BILL OF SUPPLY FOR SALE RETURN', N'19', N'GST', N'Goods', CAST(2.50 AS Numeric(18, 2)), CAST(2.50 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 5%', N'BILL OF SUPPLY FOR SALE CHALLAN', N'20', N'GST', N'Goods', CAST(2.50 AS Numeric(18, 2)), CAST(2.50 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 5%', N'BILL OF SUPPLY FOR PURCHASE', N'21', N'GST', N'Goods', CAST(2.50 AS Numeric(18, 2)), CAST(2.50 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 5%', N'BILL OF SUPPLY FOR PURCHASE ORDER', N'22', N'GST', N'Goods', CAST(2.50 AS Numeric(18, 2)), CAST(2.50 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 5%', N'BILL OF SUPPLY FOR PURCHASE RETURN', N'23', N'GST', N'Goods', CAST(2.50 AS Numeric(18, 2)), CAST(2.50 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 5%', N'BILL OF SUPPLY FOR PURCHASE CHALLAN', N'24', N'GST', N'Goods', CAST(2.50 AS Numeric(18, 2)), CAST(2.50 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 5%', N'BILL OF SUPPLY FOR SALE', N'26', N'GST', N'Goods', CAST(2.50 AS Numeric(18, 2)), CAST(2.50 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);

                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 12%', N'BILL OF SUPPLY FOR SALE ORDER', N'18', N'GST', N'Goods', CAST(6.00 AS Numeric(18, 2)), CAST(6.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 12%', N'BILL OF SUPPLY FOR SALE RETURN', N'19', N'GST', N'Goods', CAST(6.00 AS Numeric(18, 2)), CAST(6.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 12%', N'BILL OF SUPPLY FOR SALE CHALLAN', N'20', N'GST', N'Goods', CAST(6.00 AS Numeric(18, 2)), CAST(6.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 12%', N'BILL OF SUPPLY FOR PURCHASE', N'21', N'GST', N'Goods', CAST(6.00 AS Numeric(18, 2)), CAST(6.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 12%', N'BILL OF SUPPLY FOR PURCHASE ORDER', N'22', N'GST', N'Goods', CAST(6.00 AS Numeric(18, 2)), CAST(6.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 12%', N'BILL OF SUPPLY FOR PURCHASE RETURN', N'23', N'GST', N'Goods', CAST(6.00 AS Numeric(18, 2)), CAST(6.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 12%', N'BILL OF SUPPLY FOR PURCHASE CHALLAN', N'24', N'GST', N'Goods', CAST(6.00 AS Numeric(18, 2)), CAST(6.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 12%', N'BILL OF SUPPLY FOR SALE', N'26', N'GST', N'Goods', CAST(6.00 AS Numeric(18, 2)), CAST(6.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);

                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 18%', N'BILL OF SUPPLY FOR SALE ORDER', N'18', N'GST', N'Goods', CAST(9.00 AS Numeric(18, 2)), CAST(9.00 AS Numeric(18, 2)), CAST(18.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 18%', N'BILL OF SUPPLY FOR SALE RETURN', N'19', N'GST', N'Goods', CAST(9.00 AS Numeric(18, 2)), CAST(9.00 AS Numeric(18, 2)), CAST(18.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 18%', N'BILL OF SUPPLY FOR SALE CHALLAN', N'20', N'GST', N'Goods', CAST(9.00 AS Numeric(18, 2)), CAST(9.00 AS Numeric(18, 2)), CAST(18.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 18%', N'BILL OF SUPPLY FOR PURCHASE', N'21', N'GST', N'Goods', CAST(9.00 AS Numeric(18, 2)), CAST(9.00 AS Numeric(18, 2)), CAST(18.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 18%', N'BILL OF SUPPLY FOR PURCHASE ORDER', N'22', N'GST', N'Goods', CAST(9.00 AS Numeric(18, 2)), CAST(9.00 AS Numeric(18, 2)), CAST(18.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 18%', N'BILL OF SUPPLY FOR PURCHASE RETURN', N'23', N'GST', N'Goods', CAST(9.00 AS Numeric(18, 2)), CAST(9.00 AS Numeric(18, 2)), CAST(18.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 18%', N'BILL OF SUPPLY FOR PURCHASE CHALLAN', N'24', N'GST', N'Goods', CAST(9.00 AS Numeric(18, 2)), CAST(9.00 AS Numeric(18, 2)), CAST(18.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 18%', N'BILL OF SUPPLY FOR SALE', N'26', N'GST', N'Goods', CAST(9.00 AS Numeric(18, 2)), CAST(9.00 AS Numeric(18, 2)), CAST(18.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);

                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 28', N'BILL OF SUPPLY FOR SALE ORDER', N'18', N'GST', N'Goods', CAST(14.00 AS Numeric(18, 2)), CAST(14.00 AS Numeric(18, 2)), CAST(28.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 28', N'BILL OF SUPPLY FOR SALE RETURN', N'19', N'GST', N'Goods', CAST(14.00 AS Numeric(18, 2)), CAST(14.00 AS Numeric(18, 2)), CAST(28.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 28', N'BILL OF SUPPLY FOR SALE CHALLAN', N'20', N'GST', N'Goods', CAST(14.00 AS Numeric(18, 2)), CAST(14.00 AS Numeric(18, 2)), CAST(28.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 28', N'BILL OF SUPPLY FOR PURCHASE', N'21', N'GST', N'Goods', CAST(14.00 AS Numeric(18, 2)), CAST(14.00 AS Numeric(18, 2)), CAST(28.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 28', N'BILL OF SUPPLY FOR PURCHASE ORDER', N'22', N'GST', N'Goods', CAST(14.00 AS Numeric(18, 2)), CAST(14.00 AS Numeric(18, 2)), CAST(28.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 28', N'BILL OF SUPPLY FOR PURCHASE RETURN', N'23', N'GST', N'Goods', CAST(14.00 AS Numeric(18, 2)), CAST(14.00 AS Numeric(18, 2)), CAST(28.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 28', N'BILL OF SUPPLY FOR PURCHASE CHALLAN', N'24', N'GST', N'Goods', CAST(14.00 AS Numeric(18, 2)), CAST(14.00 AS Numeric(18, 2)), CAST(28.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 28', N'BILL OF SUPPLY FOR SALE', N'26', N'GST', N'Goods', CAST(14.00 AS Numeric(18, 2)), CAST(14.00 AS Numeric(18, 2)), CAST(28.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);

                    string update = "update Options set autosaletype='" + "BILL OF SUPPLY FOR SALE" + "'";
                    c.execute(update, con);
                    string u = "Update updatedatabase set updatecode='" + "170925" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }

                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 170926)
                {
                    string pos2 = @"Alter Table UnitMaster add UQC nvarchar(50)";
                    c.execute(pos2, con);
                    string op = "delete from UnitMaster";
                    c.execute(op, con);
                    c.execute("INSERT [dbo].[UnitMaster] ([UnitName], [UQC]) VALUES (N'Bag', N'BAG-BAGS	')", con);
                    c.execute("INSERT [dbo].[UnitMaster] ([UnitName], [UQC]) VALUES ( N'Box', N'BOX-BOX	')", con);
                    c.execute("INSERT [dbo].[UnitMaster] ([UnitName], [UQC]) VALUES ( N'Dozen', N'DOZ-DOZEN	')", con);
                    c.execute("INSERT [dbo].[UnitMaster] ([UnitName], [UQC]) VALUES ( N'Gms.', N'GMS-GRAMS	')", con);
                    c.execute("INSERT [dbo].[UnitMaster] ([UnitName], [UQC]) VALUES ( N'Kgs.', N'KGS-KILOGRAMS	')", con);
                    c.execute("INSERT [dbo].[UnitMaster] ([UnitName], [UQC]) VALUES ( N'Ltr.', N'LTR-LITERS	')", con);
                    c.execute("INSERT [dbo].[UnitMaster] ([UnitName], [UQC]) VALUES ( N'Pcs.', N'PCS-PIECES	')", con);
                    c.execute("INSERT [dbo].[UnitMaster] ([UnitName], [UQC]) VALUES ( N'Qntl', N'QTL-QUINTAL	')", con);
                    string u = "Update updatedatabase set updatecode='" + "170926" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171002)
                {
                    c.execute("delete from TaxSlabMaster", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'TAX FREE', N'BILL OF SUPPLY FOR SALE ORDER', N'18', N'GST', N'Goods', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'TAX FREE', N'BILL OF SUPPLY FOR SALE RETURN', N'19', N'GST', N'Goods', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'TAX FREE', N'BILL OF SUPPLY FOR SALE CHALLAN', N'20', N'GST', N'Goods', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'TAX FREE', N'BILL OF SUPPLY FOR PURCHASE', N'21', N'GST', N'Goods', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'TAX FREE', N'BILL OF SUPPLY FOR PURCHASE ORDER', N'22', N'GST', N'Goods', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'TAX FREE', N'BILL OF SUPPLY FOR PURCHASE RETURN', N'23', N'GST', N'Goods', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'TAX FREE', N'BILL OF SUPPLY FOR PURCHASE CHALLAN', N'24', N'GST', N'Goods', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ( N'TAX FREE', N'BILL OF SUPPLY FOR SALE', N'26', N'GST', N'Goods', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);

                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 5%', N'BILL OF SUPPLY FOR SALE ORDER', N'18', N'GST', N'Goods', CAST(2.50 AS Numeric(18, 2)), CAST(2.50 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 5%', N'BILL OF SUPPLY FOR SALE RETURN', N'19', N'GST', N'Goods', CAST(2.50 AS Numeric(18, 2)), CAST(2.50 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 5%', N'BILL OF SUPPLY FOR SALE CHALLAN', N'20', N'GST', N'Goods', CAST(2.50 AS Numeric(18, 2)), CAST(2.50 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 5%', N'BILL OF SUPPLY FOR PURCHASE', N'21', N'GST', N'Goods', CAST(2.50 AS Numeric(18, 2)), CAST(2.50 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 5%', N'BILL OF SUPPLY FOR PURCHASE ORDER', N'22', N'GST', N'Goods', CAST(2.50 AS Numeric(18, 2)), CAST(2.50 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 5%', N'BILL OF SUPPLY FOR PURCHASE RETURN', N'23', N'GST', N'Goods', CAST(2.50 AS Numeric(18, 2)), CAST(2.50 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 5%', N'BILL OF SUPPLY FOR PURCHASE CHALLAN', N'24', N'GST', N'Goods', CAST(2.50 AS Numeric(18, 2)), CAST(2.50 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 5%', N'BILL OF SUPPLY FOR SALE', N'26', N'GST', N'Goods', CAST(2.50 AS Numeric(18, 2)), CAST(2.50 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);

                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 12%', N'BILL OF SUPPLY FOR SALE ORDER', N'18', N'GST', N'Goods', CAST(6.00 AS Numeric(18, 2)), CAST(6.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 12%', N'BILL OF SUPPLY FOR SALE RETURN', N'19', N'GST', N'Goods', CAST(6.00 AS Numeric(18, 2)), CAST(6.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 12%', N'BILL OF SUPPLY FOR SALE CHALLAN', N'20', N'GST', N'Goods', CAST(6.00 AS Numeric(18, 2)), CAST(6.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 12%', N'BILL OF SUPPLY FOR PURCHASE', N'21', N'GST', N'Goods', CAST(6.00 AS Numeric(18, 2)), CAST(6.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 12%', N'BILL OF SUPPLY FOR PURCHASE ORDER', N'22', N'GST', N'Goods', CAST(6.00 AS Numeric(18, 2)), CAST(6.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 12%', N'BILL OF SUPPLY FOR PURCHASE RETURN', N'23', N'GST', N'Goods', CAST(6.00 AS Numeric(18, 2)), CAST(6.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 12%', N'BILL OF SUPPLY FOR PURCHASE CHALLAN', N'24', N'GST', N'Goods', CAST(6.00 AS Numeric(18, 2)), CAST(6.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 12%', N'BILL OF SUPPLY FOR SALE', N'26', N'GST', N'Goods', CAST(6.00 AS Numeric(18, 2)), CAST(6.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);

                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 18%', N'BILL OF SUPPLY FOR SALE ORDER', N'18', N'GST', N'Goods', CAST(9.00 AS Numeric(18, 2)), CAST(9.00 AS Numeric(18, 2)), CAST(18.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 18%', N'BILL OF SUPPLY FOR SALE RETURN', N'19', N'GST', N'Goods', CAST(9.00 AS Numeric(18, 2)), CAST(9.00 AS Numeric(18, 2)), CAST(18.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 18%', N'BILL OF SUPPLY FOR SALE CHALLAN', N'20', N'GST', N'Goods', CAST(9.00 AS Numeric(18, 2)), CAST(9.00 AS Numeric(18, 2)), CAST(18.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 18%', N'BILL OF SUPPLY FOR PURCHASE', N'21', N'GST', N'Goods', CAST(9.00 AS Numeric(18, 2)), CAST(9.00 AS Numeric(18, 2)), CAST(18.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 18%', N'BILL OF SUPPLY FOR PURCHASE ORDER', N'22', N'GST', N'Goods', CAST(9.00 AS Numeric(18, 2)), CAST(9.00 AS Numeric(18, 2)), CAST(18.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 18%', N'BILL OF SUPPLY FOR PURCHASE RETURN', N'23', N'GST', N'Goods', CAST(9.00 AS Numeric(18, 2)), CAST(9.00 AS Numeric(18, 2)), CAST(18.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 18%', N'BILL OF SUPPLY FOR PURCHASE CHALLAN', N'24', N'GST', N'Goods', CAST(9.00 AS Numeric(18, 2)), CAST(9.00 AS Numeric(18, 2)), CAST(18.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 18%', N'BILL OF SUPPLY FOR SALE', N'26', N'GST', N'Goods', CAST(9.00 AS Numeric(18, 2)), CAST(9.00 AS Numeric(18, 2)), CAST(18.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);

                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 28%', N'BILL OF SUPPLY FOR SALE ORDER', N'18', N'GST', N'Goods', CAST(14.00 AS Numeric(18, 2)), CAST(14.00 AS Numeric(18, 2)), CAST(28.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 28%', N'BILL OF SUPPLY FOR SALE RETURN', N'19', N'GST', N'Goods', CAST(14.00 AS Numeric(18, 2)), CAST(14.00 AS Numeric(18, 2)), CAST(28.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 28%', N'BILL OF SUPPLY FOR SALE CHALLAN', N'20', N'GST', N'Goods', CAST(14.00 AS Numeric(18, 2)), CAST(14.00 AS Numeric(18, 2)), CAST(28.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 28%', N'BILL OF SUPPLY FOR PURCHASE', N'21', N'GST', N'Goods', CAST(14.00 AS Numeric(18, 2)), CAST(14.00 AS Numeric(18, 2)), CAST(28.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 28%', N'BILL OF SUPPLY FOR PURCHASE ORDER', N'22', N'GST', N'Goods', CAST(14.00 AS Numeric(18, 2)), CAST(14.00 AS Numeric(18, 2)), CAST(28.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 28%', N'BILL OF SUPPLY FOR PURCHASE RETURN', N'23', N'GST', N'Goods', CAST(14.00 AS Numeric(18, 2)), CAST(14.00 AS Numeric(18, 2)), CAST(28.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 28%', N'BILL OF SUPPLY FOR PURCHASE CHALLAN', N'24', N'GST', N'Goods', CAST(14.00 AS Numeric(18, 2)), CAST(14.00 AS Numeric(18, 2)), CAST(28.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    c.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES (N'GST 28%', N'BILL OF SUPPLY FOR SALE', N'26', N'GST', N'Goods', CAST(14.00 AS Numeric(18, 2)), CAST(14.00 AS Numeric(18, 2)), CAST(28.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'Taxable Amt', 1, 1, 1)", con);
                    string u = "Update updatedatabase set updatecode='" + "171002" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171004)
                {
                    string u = "Update updatedatabase set updatecode='" + "171004" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171005)
                {
                    string option = @"Alter Table Options add multyprintinpos bit";
                    c.execute(option, con);
                    string update = "update Options set multyprintinpos='" + "0" + "'";
                    c.execute(update, con);
                    string u = "Update updatedatabase set updatecode='" + "171005" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171006)
                {
                    string u = "Update updatedatabase set updatecode='" + "171006" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171007)
                {
                    string u = "Update updatedatabase set updatecode='" + "171007" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171011)
                {
                    string u = "Update updatedatabase set updatecode='" + "171011" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171012)
                {
                    string u = "Update updatedatabase set updatecode='" + "171012" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171013)
                {
                    string u = "Update updatedatabase set updatecode='" + "171013" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171014)
                {
                    string option = @"Alter Table Options add noofcopyofsalebills nvarchar(10)";
                    c.execute(option, con);
                    string update = "update Options set noofcopyofsalebills='" + "1" + "'";
                    c.execute(update, con);
                    string u = "Update updatedatabase set updatecode='" + "171014" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171016)
                {
                    string option = @"Alter Table Options add noofcopyofsalechallanbills nvarchar(10)";
                    c.execute(option, con);
                    string update = "update Options set noofcopyofsalechallanbills='" + "1" + "'";
                    c.execute(update, con);
                    string option1 = @"Alter Table Options add noofcopyofsaleorderbills nvarchar(10)";
                    c.execute(option1, con);
                    string update1 = "update Options set noofcopyofsaleorderbills='" + "1" + "'";
                    c.execute(update1, con);
                    string option2 = @"Alter Table Options add noofcopyofsalereturnbills nvarchar(10)";
                    c.execute(option2, con);
                    string update2 = "update Options set noofcopyofsalereturnbills='" + "1" + "'";
                    c.execute(update2, con);
                    string u = "Update updatedatabase set updatecode='" + "171016" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171017)
                {
                    string u = "Update updatedatabase set updatecode='" + "171017" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171018)
                {
                    string u = "Update updatedatabase set updatecode='" + "171018" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171028)
                {
                    string u = "Update updatedatabase set updatecode='" + "171028" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171030)
                {
                    string option = @"Alter Table Options add requirprintpopupinpos bit";
                    c.execute(option, con);
                    string update = "update Options set requirprintpopupinpos='" + "0" + "'";
                    c.execute(update, con);
                    string u = "Update updatedatabase set updatecode='" + "171030" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171031)
                {
                    string option = @"Alter Table Options add showallitemlistinpos bit";
                    c.execute(option, con);
                    string update = "update Options set showallitemlistinpos='" + "1" + "'";
                    c.execute(update, con);
                    string u = "Update updatedatabase set updatecode='" + "171031" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171101)
                {
                    string option = @"Alter Table Options add posbillno nvarchar(10)";
                    c.execute(option, con);
                    string update = "update Options set posbillno='" + "Continuous" + "'";
                    c.execute(update, con);
                    string query = "SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON CREATE TABLE [dbo].[Voucher]([VoucherID] [bigint] IDENTITY(1,1) NOT NULL,[TransactionType] [nvarchar](255) NULL,[VchNo] [int] NULL,[Date] [datetime] NULL,[PurchaseTypeID] [float] NULL,[SaleTypeID] [float] NULL,[LongNarration] [nvarchar](255) NULL,[Transport] [nvarchar](255) NULL,[GRRRNo] [nvarchar](255) NULL,[VehicleNo] [nvarchar](255) NULL,[Station] [nvarchar](255) NULL,[PaymentTerms] [nvarchar](255) NULL,[Freight] [nvarchar](255) NULL,[PurchaseBillNo] [nvarchar](255) NULL,[PurchaseBillDate] [nvarchar](255) NULL,[DescriptionOfItems] [nvarchar](255) NULL,[TotalQty] [nvarchar](255) NULL,[FormVat36No] [nvarchar](255) NULL,[FormIssuableAmount] [float] NULL,[FormReceivableAmount] [float] NULL,[Attachment1] [nvarchar](255) NULL,[Attachment2] [nvarchar](255) NULL,[Attachment3] [nvarchar](255) NULL,[Attachment4] [nvarchar](255) NULL,[CashDetails1] [nvarchar](255) NULL,[CashDetails2] [nvarchar](255) NULL,[CashDetails3] [nvarchar](255) NULL,[TotalPacks] [float] NULL,[TotalQty1] [float] NULL,[VatAmount] [float] NULL,[Subtotal] [float] NULL,[BillSundryAmount] [float] NULL,[TotalAmount] [float] NULL,[MarginDiscount] [nvarchar](255) NULL,[MarginDiscountPercentage] [float] NULL,[ChequeNo] [nvarchar](255) NULL,[ChequeDate] [nvarchar](255) NULL,[BankName] [nvarchar](255) NULL,[AccountID] [float] NULL,[AccountName] [nvarchar](255) NULL,[PartyID] [float] NULL,[PartyName] [nvarchar](255) NULL,[BasicAmount] [float] NULL,[MarginDiscountAmount] [float] NULL,[ValueOfGoods] [float] NULL,[NotionalVatPercentage] [float] NULL,[NotionalVatAmount] [float] NULL,[FarmerName] [nvarchar](255) NULL,[Address] [nvarchar](max) NULL,[VillageID] [float] NULL,[VillageName] [nvarchar](255) NULL,[ContactNo1] [nvarchar](255) NULL,[ContactNo2] [nvarchar](255) NULL,[CommittedAmt] [float] NULL,[NetCommittedAmt] [float] NULL,[TransactionAccountID] [float] NULL,[TransactionAccountName] [nvarchar](255) NULL,[ItemsAmount] [float] NULL,[ProcessID] [float] NULL,[ProcessName] [nvarchar](255) NULL,[ItemID] [float] NULL,[ItemName] [nvarchar](255) NULL,[Qty] [float] NULL,[UnitName] [nvarchar](255) NULL,[MarketID] [float] NULL,[MarketName] [nvarchar](255) NULL,[TotalNug] [float] NULL,[TotalQntls] [float] NULL,[TotalKG] [float] NULL,[Charges] [float] NULL,[BrokerID] [float] NULL,[BrokerName] [nvarchar](255) NULL,[BrokerageRate] [float] NULL,[SubType] [nvarchar](255) NULL,[Surcharge] [float] NULL,[DueDate] [datetime] NULL,[ReminderDate] [datetime] NULL,[Status] [nvarchar](255) NULL,[Audit] [nvarchar](255) NULL,[OT1] [nvarchar](255) NULL,[OT2] [nvarchar](255) NULL,[OT3] [nvarchar](255) NULL,[OT4] [nvarchar](255) NULL,[OT5] [nvarchar](255) NULL,[OT6] [nvarchar](255) NULL,[OT7] [nvarchar](255) NULL,[OT8] [nvarchar](255) NULL,[OT9] [nvarchar](255) NULL,[OT10] [nvarchar](255) NULL,[OT11] [nvarchar](255) NULL,[OT12] [nvarchar](255) NULL,[OT13] [nvarchar](255) NULL,[OT14] [nvarchar](255) NULL,[OT15] [nvarchar](255) NULL,[OT16] [nvarchar](255) NULL,[OT17] [nvarchar](255) NULL,[OT18] [nvarchar](255) NULL,[OT19] [nvarchar](255) NULL,[OT20] [nvarchar](255) NULL,[ON1] [float] NULL,[ON2] [float] NULL,[ON3] [float] NULL,[ON4] [float] NULL,[ON5] [float] NULL,[ON6] [float] NULL,[ON7] [float] NULL,[ON8] [float] NULL,[ON9] [float] NULL,[ON10] [float] NULL,[ON11] [float] NULL,[ON12] [float] NULL,[ON13] [float] NULL,[ON14] [float] NULL,[ON15] [float] NULL,[ON16] [float] NULL,[ON17] [float] NULL,[ON18] [float] NULL,[ON19] [float] NULL,[ON20] [float] NULL,[OM1] [nvarchar](max) NULL,[OM2] [nvarchar](max) NULL,[OM3] [nvarchar](max) NULL,[OM4] [nvarchar](max) NULL,	[OM5] [nvarchar](max) NULL,[OD1] [datetime] NULL,[OD2] [datetime] NULL,[OD3] [datetime] NULL,[OD4] [datetime] NULL,[OD5] [datetime] NULL,[RoundOff] [float] NULL,[DiscountRate] [float] NULL,[DiscountAmt] [float] NULL,[Address1] [nvarchar](255) NULL,[Address2] [nvarchar](255) NULL,[City] [nvarchar](255) NULL,	[PatientName] [nvarchar](255) NULL,[ContactNo] [nvarchar](255) NULL,[Contact2] [nvarchar](255) NULL,[DoctorID] [float] NULL,[DoctorName] [nvarchar](255) NULL,[SupplierID] [float] NULL,[SupplierName] [nvarchar](255) NULL,[StockID] [float] NULL,[InvoiceTypeID] [float] NULL,[InvoiceTypeName] [nvarchar](255) NULL,[Nature] [nvarchar](255) NULL,[ContractDate] [datetime] NULL,[RenewalDate] [datetime] NULL,[TagID] [float] NULL,[TagNo] [nvarchar](255) NULL,[CustomerName] [nvarchar](255) NULL,[FatherName] [nvarchar](255) NULL,[Maker] [nvarchar](255) NULL,[EngineNo] [nvarchar](255) NULL,[ModelNo] [nvarchar](255) NULL,[BatteryNo] [nvarchar](255) NULL,[KeyNo] [nvarchar](255) NULL,[Type] [nvarchar](255) NULL,[BookletNo] [nvarchar](255) NULL,[ChasisNo] [nvarchar](255) NULL,[Place] [nvarchar](255) NULL,[FrameNo] [nvarchar](255) NULL,[Fuel] [nvarchar](255) NULL,[YearMfg] [nvarchar](255) NULL,[FromDate] [nvarchar](255) NULL,[ToDate] [nvarchar](255) NULL,[Class] [nvarchar](255) NULL,[UserID] [float] NULL,[UserName] [nvarchar](255) NULL,[UserID1] [float] NULL,[UserName1] [nvarchar](255) NULL,[EntryDate] [datetime] NULL,[HSNCode] [nvarchar](255) NULL,[SGSTPercentage] [float] NULL,[CGSTPercentage] [float] NULL,[IGSTPercentage] [float] NULL,[SGSTAmount] [float] NULL,[CGSTAmount] [float] NULL,[IGSTAmount] [float] NULL,[ZeroTaxType] [nvarchar](255) NULL,[TaxSystem] [nvarchar](255) NULL,[OriginalInvoiceNo] [nvarchar](255) NULL,[OriginalInvoiceDate] [nvarchar](255) NULL,	[AdditionalTaxAmount] [float] NULL, CONSTRAINT [PK_Voucher] PRIMARY KEY CLUSTERED ([VoucherID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]SET ANSI_PADDING OFF";
                    c.execute(query, con);
                    string u = "Update updatedatabase set updatecode='" + "171101" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171102)
                {
                    string option = @"Alter Table Voucher add isactive bit";
                    c.execute(option, con);
                    string update = "update Voucher set isactive='" + "1" + "'";
                    c.execute(update, con);
                    string u = "Update updatedatabase set updatecode='" + "171102" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171103)
                {
                    string option = @"Alter Table UnitMaster add isactive bit";
                    c.execute(option, con);

                    string update = "update UnitMaster set isactive='" + "1" + "'";
                    c.execute(update, con);

                    string option1 = @"Alter Table ItemGroupMaster add isactive bit";
                    c.execute(option1, con);

                    string update1 = "update ItemGroupMaster set isactive='" + "1" + "'";
                    c.execute(update1, con);

                    string u = "Update updatedatabase set updatecode='" + "171103" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171103)
                {
                    string client = "Update ClientMaster set Dr_cr='" + "Dr." + "' where Dr_cr='D'";
                    c.execute(client, con);
                    string client1 = "Update ClientMaster set Dr_cr='" + "Cr." + "' where Dr_cr='C'";
                    c.execute(client1, con);
                    string u = "Update updatedatabase set updatecode='" + "171104" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171106)
                {
                    string u = "Update updatedatabase set updatecode='" + "171106" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171107)
                {
                    string u = "Update updatedatabase set updatecode='" + "171107" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171108)
                {
                    string u = "Update updatedatabase set updatecode='" + "171108" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171109)
                {
                    string u = "Update updatedatabase set updatecode='" + "171109" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171110)
                {
                    string u = "Update updatedatabase set updatecode='" + "171110" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171111)
                {
                    string option = @"Alter Table Options add invoicenoinpos bit";
                    c.execute(option, con);
                    string update = "update Options set invoicenoinpos='" + "0" + "'";
                    c.execute(update, con);
                    string u = "Update updatedatabase set updatecode='" + "171111" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171113)
                {
                    string u = "Update updatedatabase set updatecode='" + "171113" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171115)
                {
                    string sale = @"Alter Table BillMaster Alter column Bill_No int";
                    c.execute(sale, con);
                    string sale1 = @"Alter Table SaleOrderMaster Alter column Bill_No int";
                    c.execute(sale1, con);
                    string sale2 = @"Alter Table Voucher Alter column VoucherID bigint";
                    c.execute(sale2, con);
                    string u = "Update updatedatabase set updatecode='" + "171115" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171116)
                {
                    string u = "Update updatedatabase set updatecode='" + "171116" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171117)
                {
                    string u = "Update updatedatabase set updatecode='" + "171117" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171118)
                {
                    string u = "Update updatedatabase set updatecode='" + "171118" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171120)
                {
                    string u = "Update updatedatabase set updatecode='" + "171120" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171121)
                {
                    c.execute("INSERT [dbo].[ChargesHeadApplyOn] ([applyon], [isactive]) VALUES (N'Net', 1)", con);
                    c.execute("INSERT [dbo].[ChargesHeadApplyOn] ([applyon], [isactive]) VALUES (N'Basic Amount', 1)", con);
                    c.execute("INSERT [dbo].[ChargesHeadApplyOn] ([applyon], [isactive]) VALUES (N'Items Total', 1)", con);
                    c.execute("INSERT [dbo].[ChargesHeadApplyOn] ([applyon], [isactive]) VALUES (N'Previous Bill Sundry', 0)", con);
                    c.execute("INSERT [dbo].[ChargesHeadApplyOn] ([applyon], [isactive]) VALUES (N'1 Before previous Its', 0)", con);
                    c.execute("INSERT [dbo].[ChargesHeadApplyOn] ([applyon], [isactive]) VALUES (N'2 Before previous Its', 0)", con);
                    c.execute("INSERT [dbo].[ChargesHeadApplyOn] ([applyon], [isactive]) VALUES (N'Taxable Amount', 1)", con);
                    c.execute("INSERT [dbo].[ChargesHeadApplyOn] ([applyon], [isactive]) VALUES (N'Tax Amount', 1)", con);
                    c.execute("INSERT [dbo].[ChargesHeadApplyOn] ([applyon], [isactive]) VALUES (N'Tax + AddTax', 1)", con);
                    c.execute("INSERT [dbo].[ChargesHeadApplyOn] ([applyon], [isactive]) VALUES (N'Service Charges', 1)", con);
                    c.execute("INSERT [dbo].[ChargesHeadApplyOn] ([applyon], [isactive]) VALUES (N'Auto Round Off', 1)", con);
                    string u = "Update updatedatabase set updatecode='" + "171121" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171122)
                {
                    string u = "Update updatedatabase set updatecode='" + "171122" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171123)
                {
                    string s = "Update ClientMaster set GroupName='Stock-in-hand' where AccountName='Replacement'";
                    c.execute(s, con);
                    string u = "Update updatedatabase set updatecode='" + "171123" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171124)
                {
                    string option = @"Alter Table Ref add isactive bit";
                    c.execute(option, con);
                    string update = "update Ref set isactive='" + "1" + "'";
                    c.execute(update, con);
                    string u = "Update updatedatabase set updatecode='" + "171124" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171127)
                {
                    string option = @"Alter Table Options add showagentnameinsale bit";
                    c.execute(option, con);
                    string update = "update Options set showagentnameinsale='" + "0" + "'";
                    c.execute(update, con);
                    string agent = @"Alter Table BillPOSMaster add agentid int";
                    c.execute(agent, con);
                    string u = "Update updatedatabase set updatecode='" + "171127" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171128)
                {
                    string agent = @"Alter Table BillMaster add agentID int";
                    c.execute(agent, con);
                    string option = @"Alter Table Options add requiragentnameinpos nvarchar(50)";
                    c.execute(option, con);
                    string update = "update Options set requiragentnameinpos='" + "No Agent Required" + "'";
                    c.execute(update, con);
                    string agent1 = @"Alter Table BillPOSProductMaster add agentid int";
                    c.execute(agent1, con);
                    string u = "Update updatedatabase set updatecode='" + "171128" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171129)
                {
                    string reftable = @"Alter Table Ref Alter column OT4 float";
                    c.execute(reftable, con);
                    string u = "Update updatedatabase set updatecode='" + "171129" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171130)
                {
                    string u = "Update updatedatabase set updatecode='" + "171130" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171201)
                {
                    string reftable = @"Alter Table Serials Alter column VchNo nvarchar(50)";
                    c.execute(reftable, con);
                    string sale = @"Alter Table BillMaster Alter column Bill_No bigint";
                    c.execute(sale, con);
                    string sale1 = @"Alter Table SaleOrderMaster Alter column Bill_No bigint";
                    c.execute(sale1, con);
                    string u = "Update updatedatabase set updatecode='" + "171201" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171202)
                {
                    string u = "Update updatedatabase set updatecode='" + "171202" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171204)
                {
                    string process = "SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON CREATE TABLE [dbo].[tblprocessmaster]([id] [bigint] IDENTITY(1,1) NOT NULL,[processname] [nvarchar](max) NULL,[mainitemID] [bigint] NULL,[mainitemname] [nvarchar](max) NULL,[mqty] [numeric](18, 2) NULL,[munit] [nvarchar](50) NULL,[maqty] [numeric](18, 2) NULL,[maunit] [nvarchar](50) NULL,[isactiveprocess] [bit] NULL,[isactive] [bit] NULL,CONSTRAINT [PK_tblprocessmaster] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]SET ANSI_PADDING OFF";
                    c.execute(process, con);
                    string row = "SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON CREATE TABLE [dbo].[tblrowmaterialsmaster]([id] [bigint] IDENTITY(1,1) NOT NULL,[processid] [bigint] NULL,[rowitemid] [bigint] NULL,[rowitemname] [nvarchar](max) NULL,[rowqty] [numeric](18, 2) NULL,[rowunit] [nvarchar](50) NULL,[rowaqty] [numeric](18, 2) NULL,[rowaunit] [nvarchar](50) NULL,[isactive] [bit] NULL,CONSTRAINT [PK_tblrowmaterialsmaster] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]SET ANSI_PADDING OFF";
                    c.execute(row, con);
                    string progen = "SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON CREATE TABLE [dbo].[tblproductgeneratedmaster]([id] [bigint] IDENTITY(1,1) NOT NULL,[processid] [bigint] NULL,[proitemid] [bigint] NULL,[proitemname] [nvarchar](max) NULL,[proqty] [numeric](18, 2) NULL,[prounit] [nvarchar](50) NULL,[proaqty] [numeric](18, 2) NULL,[proaunit] [nvarchar](50) NULL,[isactive] [bit] NULL,CONSTRAINT [PK_tblproductgeneratedmaster] PRIMARY KEY CLUSTERED([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]SET ANSI_PADDING OFF";
                    c.execute(progen, con);
                    string u = "Update updatedatabase set updatecode='" + "171204" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171205)
                {
                    string production = "SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON CREATE TABLE [dbo].[tblproductionmaster]([id] [bigint] IDENTITY(1,1) NOT NULL,[date] [datetime] NULL,[processid] [bigint] NULL,[processname] [nvarchar](max) NULL,[mainitemid] [bigint] NULL,[mainitemname] [nvarchar](max) NULL,[mqty] [numeric](18, 2) NULL,[munit] [nvarchar](50) NULL,[maqty] [numeric](18, 2) NULL,[maunit] [nvarchar](50) NULL,[isactive] [bit] NULL,CONSTRAINT [PK_tblproductionmaster] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]SET ANSI_PADDING OFF";
                    c.execute(production, con);
                    string row = "SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON CREATE TABLE [dbo].[tblproductionrawmaterialmaster]([id] [bigint] IDENTITY(1,1) NOT NULL,[productionid] [bigint] NULL,[rawitemname] [nvarchar](max) NULL,[rawqty] [numeric](18, 2) NULL,[rawunit] [nvarchar](50) NULL,[rawaqty] [numeric](18, 2) NULL,[rawaunit] [nvarchar](50) NULL,[isactive] [bit] NULL,CONSTRAINT [PK_tblrawmaterialmaster] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]SET ANSI_PADDING OFF";
                    c.execute(row, con);
                    string finished = "SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON CREATE TABLE [dbo].[tblfinishedgoodsmaster]([id] [bigint] IDENTITY(1,1) NOT NULL,[productionid] [bigint] NULL,[itemname] [nvarchar](max) NULL,[qty] [numeric](18, 2) NULL,[unit] [nvarchar](50) NULL,[aqty] [numeric](18, 2) NULL,[aunit] [nvarchar](50) NULL,[isactive] [bit] NULL,CONSTRAINT [PK_tblfinishedgoodsmaster] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]SET ANSI_PADDING OFF";
                    c.execute(finished, con);
                    string u = "Update updatedatabase set updatecode='" + "171205" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171206)
                {
                    string u = "Update updatedatabase set updatecode='" + "171206" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171207)
                {
                    string u = "Update updatedatabase set updatecode='" + "171207" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171208)
                {
                    string option = @"Alter Table tblproductionmaster add isfinished bit";
                    c.execute(option, con);
                    string update = "update tblproductionmaster set isfinished='" + "0" + "'";
                    c.execute(update, con);
                    string pro = "SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON CREATE TABLE [dbo].[tblfinishedgoodsqty]([id] [bigint] IDENTITY(1,1) NOT NULL,[proitemid] [bigint] NULL,[proitem] [nvarchar](max) NULL,[aqty] [numeric](18, 2) NULL,[altqty] [numeric](18, 2) NULL,[fqty] [numeric](18, 2) NULL,[remarks] [nvarchar](max) NULL,[isactive] [bit] NULL,CONSTRAINT [PK_tblfinishedgoodsqty] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]SET ANSI_PADDING OFF";
                    c.execute(pro, con);
                    string u = "Update updatedatabase set updatecode='" + "171208" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171209)
                {
                    string option = @"Alter Table ProductMaster add isHotProduct bit";
                    c.execute(option, con);
                    string update = "update ProductMaster set isHotProduct='" + "0" + "'";
                    c.execute(update, con);
                    string u = "Update updatedatabase set updatecode='" + "171209" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171211)
                {
                    string u = "Update updatedatabase set updatecode='" + "171211" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171212)
                {
                    string u = "Update updatedatabase set updatecode='" + "171212" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171213)
                {
                    string a = "Update FormFormat set formname='POSNEW' where id='" + "5" + "'";
                    c.execute(a, con);
                    string u = "Update updatedatabase set updatecode='" + "171213" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171215)
                {
                    string option = @"Alter Table tblproductionmaster add processdescription nvarchar(MAX)";
                    c.execute(option, con);
                    string option1 = @"Alter Table tblprocessmaster add processdescription nvarchar(MAX)";
                    c.execute(option1, con);
                    string u = "Update updatedatabase set updatecode='" + "171215" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171216)
                {
                    string u = "Update updatedatabase set updatecode='" + "171216" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171218)
                {
                    string option = @"Alter Table Options add r1 nvarchar(50)";
                    c.execute(option, con);
                    string update = "update Options set r1=N'₹ 50'";
                    c.execute(update, con);
                    string option1 = @"Alter Table Options add r2 nvarchar(50)";
                    c.execute(option1, con);
                    string update1 = "update Options set r2=N'₹ 100'";
                    c.execute(update1, con);
                    string option2 = @"Alter Table Options add r3 nvarchar(50)";
                    c.execute(option2, con);
                    string update3 = "update Options set r3=N'₹ 50'";
                    c.execute(update3, con);
                    string option4 = @"Alter Table Options add r4 nvarchar(50)";
                    c.execute(option4, con);
                    string update4 = "update Options set r4=N'₹ 2000'";
                    c.execute(update4, con);
                    string u = "Update updatedatabase set updatecode='" + "171218" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171219)
                {
                    string option4 = @"Alter Table BillPOSMaster add reciveamt nvarchar(50)";
                    c.execute(option4, con);
                    string update4 = "update BillPOSMaster set reciveamt='" + "0" + "'";
                    c.execute(update4, con);
                    string option = @"Alter Table BillPOSMaster add returnamount nvarchar(50)";
                    c.execute(option, con);
                    string update = "update BillPOSMaster set returnamount='" + "0" + "'";
                    c.execute(update, con);
                    string chaege = @"Alter Table SaleOrderchargesmaster add billsundryid bigint";
                    c.execute(chaege, con);
                    string chaege1 = @"Alter Table Billchargesmaster add billsundryid bigint";
                    c.execute(chaege1, con);
                    DataTable billcharge = c.getdataset("select * from Billchargesmaster where isactive=1", con);
                    if (billcharge.Rows.Count > 0)
                    {
                        for (int i = 0; i < billcharge.Rows.Count; i++)
                        {
                            string charge = c.ExecuteScalar("select BillSundryID from BillSundry where isactive=1 and BillSundryName='" + billcharge.Rows[i]["perticulars"].ToString() + "'");
                            string update1 = "update Billchargesmaster set billsundryid='" + charge + "' where perticulars='" + billcharge.Rows[i]["perticulars"].ToString() + "' and isactive=1";
                            c.execute(update1, con);
                        }
                    }
                    DataTable billcharge1 = c.getdataset("select * from SaleOrderchargesmaster where isactive=1", con);
                    if (billcharge1.Rows.Count > 0)
                    {
                        for (int i = 0; i < billcharge1.Rows.Count; i++)
                        {
                            string charge = c.ExecuteScalar("select BillSundryID from BillSundry where isactive=1 and BillSundryName='" + billcharge1.Rows[i]["perticulars"].ToString() + "'");
                            string update1 = "update SaleOrderchargesmaster set billsundryid='" + charge + "'where perticulars='" + billcharge1.Rows[i]["perticulars"].ToString() + "' and isactive=1";
                            c.execute(update1, con);
                        }
                    }
                    string option44 = @"Alter Table Options add productionidtype bit";
                    c.execute(option44, con);
                    string update44 = "update Options set productionidtype='" + "Continuous" + "'";
                    c.execute(update44, con);
                    string pro = @"Alter Table tblproductionmaster add proidmanual nvarchar(MAX)";
                    c.execute(pro, con);
                    string u = "Update updatedatabase set updatecode='" + "171219" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171220)
                {
                    string agent = @"Alter Table SaleOrderMaster add agentID int";
                    c.execute(agent, con);
                    string orbillno = @"Alter Table BillMaster add originalbillno nvarchar(MAX)";
                    c.execute(orbillno, con);
                    string orbilldate = @"Alter Table BillMaster add originalbilldate nvarchar(50)";
                    c.execute(orbilldate, con);
                    string u = "Update updatedatabase set updatecode='" + "171220" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171221)
                {
                    string complain = "SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON CREATE TABLE [dbo].[tblcomplainmaster]([id] [bigint] IDENTITY(1,1) NOT NULL,[date] [datetime] NULL,[customerid] [bigint] NULL,[customername] [nvarchar](max) NULL,[itemname] [nvarchar](max) NULL,[replacementtype] [nvarchar](50) NULL,[qty] [numeric](18, 2) NULL,[srno] [nvarchar](max) NULL,[descriprtion] [nvarchar](max) NULL,[remarks] [nvarchar](max) NULL,[isactive] [bit] NULL,CONSTRAINT [PK_tblcomplainmaster] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]SET ANSI_PADDING OFF";
                    c.execute(complain, con);
                    string company = "SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON CREATE TABLE [dbo].[tblsendtocompanymaster]([id] [bigint] IDENTITY(1,1) NOT NULL,[date] [datetime] NULL,[supplierID] [bigint] NULL,[suppliername] [nvarchar](max) NULL,[complainID] [bigint] NULL,[serialno] [nvarchar](max) NULL,[itemname] [nvarchar](max) NULL,[description] [nvarchar](max) NULL,[replacementtype] [nvarchar](50) NULL,[qty] [numeric](18, 2) NULL,[transportdetails] [nvarchar](max) NULL,[remarks] [nvarchar](max) NULL,[isactive] [bit] NULL,CONSTRAINT [PK_tblsendtocompanymaster] PRIMARY KEY CLUSTERED([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]SET ANSI_PADDING OFF";
                    c.execute(company, con);
                    string u = "Update updatedatabase set updatecode='" + "171221" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171222)
                {
                    string agent = @"Alter Table tblcomplainmaster add complainid bigint";
                    c.execute(agent, con);
                    string agent1 = @"Alter Table tblcomplainmaster add itemid bigint";
                    c.execute(agent1, con);
                    string company = @"Alter Table tblsendtocompanymaster add companyid bigint";
                    c.execute(company, con);
                    string company1 = @"Alter Table tblsendtocompanymaster add itemid bigint";
                    c.execute(company1, con);
                    string status = @"Alter Table tblsendtocompanymaster add status nvarchar(50)";
                    c.execute(status, con);
                    string status1 = @"Alter Table tblcomplainmaster add status nvarchar(50)";
                    c.execute(status1, con);
                    string option44 = @"Alter Table Options add natureofbusiness bit";
                    c.execute(option44, con);
                    string update44 = "update Options set natureofbusiness='" + "0" + "'";
                    c.execute(update44, con);
                    string u = "Update updatedatabase set updatecode='" + "171222" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171225)
                {
                    string rec = "SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON CREATE TABLE [dbo].[tblreceivefromcompany]([id] [bigint] IDENTITY(1,1) NOT NULL,[date] [datetime] NULL,[complainid] [bigint] NULL,[oldserialno] [nvarchar](max) NULL,[newserialno] [nvarchar](max) NULL,[supplierid] [bigint] NULL,[suppliername] [nvarchar](max) NULL,[repairarea] [nvarchar](50) NULL,[remarks] [nvarchar](max) NULL,[isactive] [bit] NULL,CONSTRAINT [PK_tblreceivefromcompany] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]SET ANSI_PADDING OFF";
                    c.execute(rec, con);
                    string u = "Update updatedatabase set updatecode='" + "171225" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171226)
                {
                    string cus = "SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON CREATE TABLE [dbo].[tblsendtocustomer]([id] [bigint] IDENTITY(1,1) NOT NULL,[complainID] [bigint] NULL,[serialno] [nvarchar](max) NULL,[costomerID] [bigint] NULL,[customername] [nvarchar](max) NULL,[replacementtype] [nvarchar](50) NULL,[qty] [numeric](18, 2) NULL,[transportdetails] [nvarchar](max) NULL,[remarks] [nvarchar](max) NULL,[customertype] [nvarchar](50) NULL,[date] [datetime] NULL,[isactive] [bit] NULL,CONSTRAINT [PK_tblsendtocustomer] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]SET ANSI_PADDING OFF";
                    c.execute(cus, con);
                    string u = "Update updatedatabase set updatecode='" + "171226" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171227)
                {
                    string option44 = @"Alter Table BillPOSMaster add totaltoundoff numeric(18, 2)";
                    c.execute(option44, con);
                    string update44 = "update BillPOSMaster set totaltoundoff='" + "0" + "'";
                    c.execute(update44, con);
                    string u = "Update updatedatabase set updatecode='" + "171227" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171228)
                {
                    string option44 = @"drop table tblcomplainmaster";
                    c.execute(option44, con);
                    string option4 = @"drop table tblsendtocompanymaster";
                    c.execute(option4, con);
                    string ca = "SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON CREATE TABLE [dbo].[tblcomplainmaster]([id] [bigint] IDENTITY(1,1) NOT NULL,[date] [datetime] NULL,[customerid] [bigint] NULL,[customername] [nvarchar](max) NULL,[isactive] [bit] NULL,CONSTRAINT [PK_tblcomplainmaster] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]SET ANSI_PADDING OFF";
                    c.execute(ca, con);
                    string ci = "SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON CREATE TABLE [dbo].[tblitemcomplainmaster]([id] [bigint] IDENTITY(1,1) NOT NULL,[complainID] [bigint] NULL,[Itemname] [nvarchar](max) NULL,[description] [nvarchar](max) NULL,[replacementtype] [nvarchar](50) NULL,[qty] [numeric](18, 2) NULL,[serialno] [nvarchar](max) NULL,[remarks] [nvarchar](max) NULL,[status] [nvarchar](50) NULL,[itemid] [bigint] NULL,[isactive] [bit] NULL,CONSTRAINT [PK_tblitemcomplainmaster] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]SET ANSI_PADDING OFF";
                    c.execute(ci, con);
                    string sc = "SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON CREATE TABLE [dbo].[tblsendtocompanymaster]([id] [bigint] IDENTITY(1,1) NOT NULL,[date] [datetime] NULL,[supplierID] [bigint] NULL,[suppliername] [nvarchar](max) NULL,[transportdetails] [nvarchar](max) NULL,[remarks] [nvarchar](max) NULL,[isactive] [bit] NULL,CONSTRAINT [PK_tblsendtocompanymaster] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]SET ANSI_PADDING OFF";
                    c.execute(sc, con);
                    string si = "SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON CREATE TABLE [dbo].[tblsendtocompanyitemmaster]([id] [bigint] IDENTITY(1,1) NOT NULL,[sendtocompanyID] [bigint] NULL,[complainID] [bigint] NULL,[serialno] [nvarchar](max) NULL,[itemname] [nvarchar](max) NULL,[qty] [numeric](18, 2) NULL,[description] [nvarchar](max) NULL,[replacementtype] [nvarchar](50) NULL,[itemid] [bigint] NULL,[isactive] [bit] NULL,CONSTRAINT [PK_tblsendtocompanyitemmaster] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]SET ANSI_PADDING OFF";
                    c.execute(si, con);
                    string u = "Update updatedatabase set updatecode='" + "171228" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171229)
                {
                    string option4 = @"drop table tblreceivefromcompany";
                    c.execute(option4, con);
                    string rc = "SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON CREATE TABLE [dbo].[tblitemreceivefromcompany]([id] [bigint] IDENTITY(1,1) NOT NULL,[receivefromcompanyid] [bigint] NULL,[sendtocompanyid] [bigint] NULL,[complainID] [bigint] NULL,[serialno] [nvarchar](max) NULL,[itemname] [nvarchar](max) NULL,[remarks] [nvarchar](max) NULL,[transportdetails] [nvarchar](max) NULL,[senddate] [datetime] NULL,[itemid] [bigint] NULL,[isactive] [bit] NULL,CONSTRAINT [PK_tblitemreceivefromcompany] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]SET ANSI_PADDING OFF";
                    c.execute(rc, con);
                    string ri = "SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON CREATE TABLE [dbo].[tblreceivefromcompany]([id] [bigint] IDENTITY(1,1) NOT NULL,[date] [datetime] NULL,[complainid] [bigint] NULL,[oldserialno] [nvarchar](max) NULL,[newserialno] [nvarchar](max) NULL,[supplierid] [bigint] NULL,[suppliername] [nvarchar](max) NULL,[remarks] [nvarchar](max) NULL,[isactive] [bit] NULL,CONSTRAINT [PK_tblreceivefromcompany] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]SET ANSI_PADDING OFF";
                    c.execute(ri, con);
                    string u = "Update updatedatabase set updatecode='" + "171229" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 171230)
                {
                    string pos = "ALTER TABLE BillPOSMaster DROP CONSTRAINT PK_BillPOSMaster";
                    c.execute(pos, con);
                    string pos1 = @"Alter Table BillPOSMaster add id bigint IDENTITY(1,1)";
                    c.execute(pos1, con);
                    string pos2 = "ALTER TABLE BillPOSMaster ADD CONSTRAINT PK_BillPOSMaster PRIMARY KEY CLUSTERED (id);";
                    c.execute(pos2, con);
                    string agent = @"Alter Table tblitemreceivefromcompany add newserialno nvarchar(MAX)";
                    c.execute(agent, con);
                    string u = "Update updatedatabase set updatecode='" + "171230" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 180101)
                {
                    string u = "Update updatedatabase set updatecode='" + "180101" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 180102)
                {
                    string u = "Update updatedatabase set updatecode='" + "180102" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 180103)
                {
                    string u = "Update updatedatabase set updatecode='" + "180103" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 180104)
                {
                    string option4 = @"drop table tblsendtocustomer";
                    c.execute(option4, con);
                    //string a="SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON CREATE TABLE [dbo].[tblsendtocompanyitemmaster]([id] [bigint] IDENTITY(1,1) NOT NULL,[sendtocompanyID] [bigint] NULL,[complainID] [bigint] NULL,[serialno] [nvarchar](max) NULL,[itemname] [nvarchar](max) NULL,[qty] [numeric](18, 2) NULL,[description] [nvarchar](max) NULL,[replacementtype] [nvarchar](50) NULL,[itemid] [bigint] NULL,[isactive] [bit] NULL,CONSTRAINT [PK_tblsendtocompanyitemmaster] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]SET ANSI_PADDING OFF";
                    //c.execute(a, con);
                    string b = "SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON CREATE TABLE [dbo].[tblsendtocustomer]([id] [bigint] IDENTITY(1,1) NOT NULL,[complainID] [bigint] NULL,[serialno] [nvarchar](max) NULL,[customerID] [bigint] NULL,[customername] [nvarchar](max) NULL,[replacementtype] [nvarchar](50) NULL,[qty] [numeric](18, 2) NULL,[transportdetails] [nvarchar](max) NULL,[remarks] [nvarchar](max) NULL,[serialnotype] [nvarchar](50) NULL,[date] [datetime] NULL,[isactive] [bit] NULL,CONSTRAINT [PK_tblsendtocustomer] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]SET ANSI_PADDING OFF";
                    c.execute(b, con);
                    string u = "Update updatedatabase set updatecode='" + "180104" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 180105)
                {
                    string u = "Update updatedatabase set updatecode='" + "180105" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1801061800)
                {
                    string option44 = @"Alter Table Options add PersistSecurityInfo bit";
                    c.execute(option44, con);
                    string update44 = "update Options set PersistSecurityInfo='" + "1" + "'";
                    c.execute(update44, con);
                    string sale = @"Alter Table updatedatabase Alter column updatecode bigint";
                    c.execute(sale, con);
                    string u = "Update updatedatabase set updatecode='" + "1801061800" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1801071800)
                {
                    string a = "SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON CREATE TABLE [dbo].[tblitemsendtocustomer]([id] [bigint] IDENTITY(1,1) NOT NULL,[sendtocustomerid] [bigint] NULL,[complainID] [bigint] NULL,[itemname] [nvarchar](max) NULL,[description] [nvarchar](max) NULL,[serialno] [nvarchar](max) NULL,[replacementtype] [nvarchar](50) NULL,[newserialno] [nvarchar](max) NULL,[itemid] [bigint] NULL,[isactive] [bit] NULL,CONSTRAINT [PK_tblitemsendtocustomer] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]SET ANSI_PADDING OFF";
                    c.execute(a, con);
                    string u = "Update updatedatabase set updatecode='" + "1801071800" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1801081800)
                {
                    string u = "Update updatedatabase set updatecode='" + "1801081800" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1801091800)
                {
                    string option4 = @"Alter Table BillPOSMaster add customercheckno nvarchar(50)";
                    c.execute(option4, con);
                    string option1 = @"Alter Table BillPOSMaster add customerchequename nvarchar(MAX)";
                    c.execute(option1, con);
                    string option2 = @"Alter Table BillPOSMaster add customerchequebankname nvarchar(MAX)";
                    c.execute(option2, con);
                    string option44 = @"Alter Table Options add noofcopyofquickreceipt nvarchar(10)";
                    c.execute(option44, con);
                    string update44 = "update Options set noofcopyofquickreceipt='" + "1" + "'";
                    c.execute(update44, con);
                    string option441 = @"Alter Table Options add noofcopyofquickpayment nvarchar(10)";
                    c.execute(option441, con);
                    string update442 = "update Options set noofcopyofquickpayment='" + "1" + "'";
                    c.execute(update442, con);
                    string u = "Update updatedatabase set updatecode='" + "1801091800" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1801101800)
                {
                    string option1 = @"Alter Table paymentreceipt add OT1 nvarchar(MAX)";
                    c.execute(option1, con);
                    string option2 = @"Alter Table paymentreceipt add OT2 nvarchar(MAX)";
                    c.execute(option2, con);
                    string option3 = @"Alter Table paymentreceipt add OT3 nvarchar(MAX)";
                    c.execute(option3, con);
                    string option4 = @"Alter Table paymentreceipt add OT4 nvarchar(MAX)";
                    c.execute(option4, con);
                    string option5 = @"Alter Table paymentreceipt add OT5 nvarchar(MAX)";
                    c.execute(option5, con);
                    string option6 = @"Alter Table paymentreceipt add OT6 nvarchar(MAX)";
                    c.execute(option6, con);
                    string option7 = @"Alter Table paymentreceipt add OT7 nvarchar(MAX)";
                    c.execute(option7, con);
                    string option8 = @"Alter Table paymentreceipt add OT8 nvarchar(MAX)";
                    c.execute(option8, con);
                    string option9 = @"Alter Table paymentreceipt add OT9 nvarchar(MAX)";
                    c.execute(option9, con);
                    string option10 = @"Alter Table paymentreceipt add OT10 nvarchar(MAX)";
                    c.execute(option10, con);
                    string option11 = @"Alter Table paymentreceipt add OT11 nvarchar(MAX)";
                    c.execute(option11, con);
                    string option12 = @"Alter Table paymentreceipt add OT12 nvarchar(MAX)";
                    c.execute(option12, con);
                    string option13 = @"Alter Table paymentreceipt add OT13 nvarchar(MAX)";
                    c.execute(option13, con);
                    string option14 = @"Alter Table paymentreceipt add OT14 nvarchar(MAX)";
                    c.execute(option14, con);
                    string option15 = @"Alter Table paymentreceipt add OT15 nvarchar(MAX)";
                    c.execute(option15, con);
                    string option16 = @"Alter Table paymentreceipt add OT16 nvarchar(MAX)";
                    c.execute(option16, con);
                    string option17 = @"Alter Table paymentreceipt add OT17 nvarchar(MAX)";
                    c.execute(option17, con);
                    string option18 = @"Alter Table paymentreceipt add OT18 nvarchar(MAX)";
                    c.execute(option18, con);
                    string option19 = @"Alter Table paymentreceipt add OT19 nvarchar(MAX)";
                    c.execute(option19, con);
                    string option20 = @"Alter Table paymentreceipt add OT20 nvarchar(MAX)";
                    c.execute(option20, con);
                    string add = "SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON CREATE TABLE [dbo].[Additional]([id] [bigint] IDENTITY(1,1) NOT NULL,[MasterType] [nvarchar](50) NULL,[nooffields] [nvarchar](50) NULL,[field1] [nvarchar](50) NULL,[field2] [nvarchar](50) NULL,[field3] [nvarchar](50) NULL,[field4] [nvarchar](50) NULL,[field5] [nvarchar](50) NULL,[field6] [nvarchar](50) NULL,[field7] [nvarchar](50) NULL,[field8] [nvarchar](50) NULL,[field9] [nvarchar](50) NULL,[field10] [nvarchar](50) NULL,CONSTRAINT [PK_Additional] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]SET ANSI_PADDING OFF";
                    c.execute(add, con);
                    string u = "Update updatedatabase set updatecode='" + "1801101800" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1801161800)
                {
                    string u = "Update updatedatabase set updatecode='" + "1801161800" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1801171800)
                {
                    string option1 = @"Alter Table paymentreceipt add OT21 nvarchar(MAX)";
                    c.execute(option1, con);
                    string option2 = @"Alter Table paymentreceipt add OT22 nvarchar(MAX)";
                    c.execute(option2, con);
                    string option3 = @"Alter Table paymentreceipt add OT23 nvarchar(MAX)";
                    c.execute(option3, con);
                    string option4 = @"Alter Table paymentreceipt add OT24 nvarchar(MAX)";
                    c.execute(option4, con);
                    string option5 = @"Alter Table paymentreceipt add OT25 nvarchar(MAX)";
                    c.execute(option5, con);
                    string option6 = @"Alter Table paymentreceipt add OT26 nvarchar(MAX)";
                    c.execute(option6, con);
                    string option7 = @"Alter Table paymentreceipt add OT27 nvarchar(MAX)";
                    c.execute(option7, con);
                    string option8 = @"Alter Table paymentreceipt add OT28 nvarchar(MAX)";
                    c.execute(option8, con);
                    string option9 = @"Alter Table paymentreceipt add OT29 nvarchar(MAX)";
                    c.execute(option9, con);
                    string option10 = @"Alter Table paymentreceipt add OT30 nvarchar(MAX)";
                    c.execute(option10, con);
                    string add = @"Alter Table Additional add Field11 nvarchar(MAX)";
                    c.execute(add, con);
                    string add1 = @"Alter Table Additional add Field12 nvarchar(MAX)";
                    c.execute(add1, con);
                    string add2 = @"Alter Table Additional add Field13 nvarchar(MAX)";
                    c.execute(add2, con);
                    string add3 = @"Alter Table Additional add Field14 nvarchar(MAX)";
                    c.execute(add3, con);
                    string add4 = @"Alter Table Additional add Field15 nvarchar(MAX)";
                    c.execute(add4, con);
                    string u = "Update updatedatabase set updatecode='" + "1801171800" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1801181800)
                {
                    string option1 = @"Alter Table tblproductionmaster add OT1 nvarchar(MAX)";
                    c.execute(option1, con);
                    string option2 = @"Alter Table tblproductionmaster add OT2 nvarchar(MAX)";
                    c.execute(option2, con);
                    string option3 = @"Alter Table tblproductionmaster add OT3 nvarchar(MAX)";
                    c.execute(option3, con);
                    string option4 = @"Alter Table tblproductionmaster add OT4 nvarchar(MAX)";
                    c.execute(option4, con);
                    string option5 = @"Alter Table tblproductionmaster add OT5 nvarchar(MAX)";
                    c.execute(option5, con);
                    string option6 = @"Alter Table tblproductionmaster add OT6 nvarchar(MAX)";
                    c.execute(option6, con);
                    string option7 = @"Alter Table tblproductionmaster add OT7 nvarchar(MAX)";
                    c.execute(option7, con);
                    string option8 = @"Alter Table tblproductionmaster add OT8 nvarchar(MAX)";
                    c.execute(option8, con);
                    string option9 = @"Alter Table tblproductionmaster add OT9 nvarchar(MAX)";
                    c.execute(option9, con);
                    string option10 = @"Alter Table tblproductionmaster add OT10 nvarchar(MAX)";
                    c.execute(option10, con);
                    string option11 = @"Alter Table tblproductionmaster add OT11 nvarchar(MAX)";
                    c.execute(option11, con);
                    string option12 = @"Alter Table tblproductionmaster add OT12 nvarchar(MAX)";
                    c.execute(option12, con);
                    string option13 = @"Alter Table tblproductionmaster add OT13 nvarchar(MAX)";
                    c.execute(option13, con);
                    string option14 = @"Alter Table tblproductionmaster add OT14 nvarchar(MAX)";
                    c.execute(option14, con);
                    string option15 = @"Alter Table tblproductionmaster add OT15 nvarchar(MAX)";
                    c.execute(option15, con);
                    string sale = @"Alter Table Options Alter column productionidtype nvarchar(50)";
                    c.execute(sale, con);
                    string client = @"Alter Table ClientMaster add crelimite nvarchar(50)";
                    c.execute(client, con);
                    string client1 = @"Alter Table ClientMaster add billlimite nvarchar(50)";
                    c.execute(client1, con);
                    string client2 = @"Alter Table ClientMaster add credaysale nvarchar(50)";
                    c.execute(client2, con);
                    string client3 = @"Alter Table ClientMaster add credaypurchase nvarchar(50)";
                    c.execute(client3, con);
                    string u = "Update updatedatabase set updatecode='" + "1801181800" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1801191800)
                {
                    //DataTable dt1 = c.getdataset("select * from BillMaster where isactive=1", con);
                    //if (dt1.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < dt1.Rows.Count; i++)
                    //    {
                    //        c.execute("Update Ledger set OD1='" + Convert.ToDateTime(dt1.Rows[i]["duedate"].ToString()).ToString(Master.dateformate) + "' where VoucherID='" + dt1.Rows[i]["billno"].ToString() + "'", con);
                    //    }
                    //}
                    DataTable dt1 = c.getdataset("select * from ledger where isactive=1", con);
                    if (dt1.Rows.Count > 0)
                    {
                        //for (int i = 0; i < dt1.Rows.Count; i++)
                        //{
                        c.execute("Update Ledger set OD1=Date1 where isactive=1", con);
                        //}
                    }
                    string u = "Update updatedatabase set updatecode='" + "1801191800" + "' where id='" + "1" + "'";
                    c.execute(u, con);


                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1801201800)
                {
                    string acc = "SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON CREATE TABLE [dbo].[AccountCustomerType]([id] [bigint] IDENTITY(1,1) NOT NULL,[customertype] [nvarchar](50) NULL,[isactive] [bit] NULL,CONSTRAINT [PK_AccountCustomerType] PRIMARY KEY CLUSTERED ([id] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]SET ANSI_PADDING OFF";
                    c.execute(acc, con);
                    string client = @"Alter Table ClientMaster add accountnumber nvarchar(MAX)";
                    c.execute(client, con);
                    string client1 = @"Alter Table ClientMaster add customertypeid bigint";
                    c.execute(client1, con);
                    string client2 = @"Alter Table ClientMaster add customertype nvarchar(50)";
                    c.execute(client2, con);
                    string client3 = @"Alter Table ClientMaster add noteorremarks nvarchar(MAX)";
                    c.execute(client3, con);
                    string u = "Update updatedatabase set updatecode='" + "1801201800" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1801221800)
                {
                    string user = @"Alter Table UserInfo Alter column UserName nvarchar(50)";
                    c.execute(user, con);
                    string user1 = @"Alter Table UserInfo Alter column Position nvarchar(50)";
                    c.execute(user1, con);
                    string user2 = @"Alter Table UserInfo Alter column Password nvarchar(50)";
                    c.execute(user2, con);
                    string user3 = @"Alter Table UserInfo add tital nvarchar(50)";
                    c.execute(user3, con);
                    string user4 = @"Alter Table UserInfo add name nvarchar(MAX)";
                    c.execute(user4, con);
                    string user5 = @"Alter Table UserInfo add address nvarchar(MAX)";
                    c.execute(user5, con);
                    string user6 = @"Alter Table UserInfo add phoneno nvarchar(50)";
                    c.execute(user6, con);
                    string user7 = @"Alter Table UserInfo add swipeid nvarchar(50)";
                    c.execute(user7, con);
                    string user8 = @"Alter Table UserInfo add commissiontype nvarchar(50)";
                    c.execute(user8, con);
                    string user9 = @"Alter Table UserInfo add commissiontypevalue nvarchar(50)";
                    c.execute(user9, con);
                    string user10 = @"Alter Table UserInfo add targetcommission nvarchar(50)";
                    c.execute(user10, con);
                    string user11 = @"Alter Table UserInfo add targetcommissionvalue nvarchar(50)";
                    c.execute(user11, con);
                    string user12 = @"Alter Table UserInfo add Positionid bigint";
                    c.execute(user12, con);
                    string bill = @"Alter Table BillPOSMaster add adddisper numeric(18, 2)";
                    c.execute(bill, con);
                    string update44 = "update BillPOSMaster set adddisper='" + "0" + "'";
                    c.execute(update44, con);
                    string u = "Update updatedatabase set updatecode='" + "1801221800" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1801231800)
                {
                    string option44 = @"Alter Table Options add accountbillno nvarchar(10)";
                    c.execute(option44, con);
                    string update44 = "update Options set accountbillno='" + "Continuous" + "'";
                    c.execute(update44, con);
                    string option4 = @"Alter Table Options add accountprefix nvarchar(50)";
                    c.execute(option4, con);
                    //c.execute("update BillMaster set OrderStatus='Pending' where isactive=1", con);
                    string u = "Update updatedatabase set updatecode='" + "1801231800" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1801241800)
                {

                    string update44 = "update Options set accountbillno='" + "Continuous" + "'";
                    c.execute(update44, con);
                    string user = "SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON CREATE TABLE [dbo].[tbluser_employeetype]([id] [bigint] IDENTITY(1,1) NOT NULL,[employeetype] [nvarchar](50) NULL,[isactive] [bit] NULL,CONSTRAINT [PK_tbluser_employeetype] PRIMARY KEY CLUSTERED ([id] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]SET ANSI_PADDING OFF";
                    c.execute(user, con);
                    string insert = "INSERT INTO [dbo].[AccountCustomerType]([customertype],[isactive])VALUES('" + "General" + "','" + "1" + "')";
                    c.execute(insert, con);
                    string insert1 = "INSERT INTO [dbo].[tbluser_employeetype]([employeetype],[isactive])VALUES('" + "admin" + "','" + "1" + "')";
                    c.execute(insert1, con);
                    string u = "Update updatedatabase set updatecode='" + "1801241800" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1801251800)
                {
                    string option4 = @"Alter Table Company add companylogo nvarchar(MAX)";
                    c.execute(option4, con);
                    string u = "Update updatedatabase set updatecode='" + "1801251800" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1801261800)
                {
                    string truncate = "Truncate table MenuMaster";
                    c.execute(truncate, con);
                    c.execute("INSERT [dbo].[MenuMaster] ([mName], [details], [addDate], [isActive]) VALUES (N'Sales', N'-', NULL, 1)", con);
                    c.execute("INSERT [dbo].[MenuMaster] ([mName], [details], [addDate], [isActive]) VALUES (N'Sales Order', N'-', NULL, 1)", con);
                    c.execute("INSERT [dbo].[MenuMaster] ([mName], [details], [addDate], [isActive]) VALUES (N'Quick Receipt', N'-', NULL, 1)", con);
                    c.execute("INSERT [dbo].[MenuMaster] ([mName], [details], [addDate], [isActive]) VALUES (N'Purchase', N'-', NULL, 1)", con);
                    c.execute("INSERT [dbo].[MenuMaster] ([mName], [details], [addDate], [isActive]) VALUES (N'Purchase Order', N'-', NULL, 1)", con);
                    c.execute("INSERT [dbo].[MenuMaster] ([mName], [details], [addDate], [isActive]) VALUES (N'Quick Payment', N'-', NULL, 1)", con);
                    c.execute("INSERT [dbo].[MenuMaster] ([mName], [details], [addDate], [isActive]) VALUES (N'Debit Note', N'-', NULL, 1)", con);
                    c.execute("INSERT [dbo].[MenuMaster] ([mName], [details], [addDate], [isActive]) VALUES (N'Credit Note', N'-', NULL, 1)", con);
                    c.execute("INSERT [dbo].[MenuMaster] ([mName], [details], [addDate], [isActive]) VALUES (N'User', N'-', NULL, 1)", con);
                    c.execute("INSERT [dbo].[MenuMaster] ([mName], [details], [addDate], [isActive]) VALUES (N'POS', N'-', NULL, 1)", con);
                    c.execute("INSERT [dbo].[MenuMaster] ([mName], [details], [addDate], [isActive]) VALUES (N'Items', N'-', NULL, 1)", con);
                    c.execute("INSERT [dbo].[MenuMaster] ([mName], [details], [addDate], [isActive]) VALUES (N'Accounts', N'-', NULL, 1)", con);
                    c.execute("INSERT [dbo].[MenuMaster] ([mName], [details], [addDate], [isActive]) VALUES (N'Sale Challan', N'-', NULL, 1)", con);
                    c.execute("INSERT [dbo].[MenuMaster] ([mName], [details], [addDate], [isActive]) VALUES (N'Sale Return', N'-', NULL, 1)", con);
                    c.execute("INSERT [dbo].[MenuMaster] ([mName], [details], [addDate], [isActive]) VALUES (N'Sale Return Challan', N'-', NULL, 1)", con);
                    c.execute("INSERT [dbo].[MenuMaster] ([mName], [details], [addDate], [isActive]) VALUES (N'Purchase Challan', N'-', NULL, 1)", con);
                    c.execute("INSERT [dbo].[MenuMaster] ([mName], [details], [addDate], [isActive]) VALUES (N'Purchase Return', N'-', NULL, 1)", con);
                    c.execute("INSERT [dbo].[MenuMaster] ([mName], [details], [addDate], [isActive]) VALUES (N'Purchase Return Challan', N'-', NULL, 1)", con);
                    c.execute("INSERT [dbo].[MenuMaster] ([mName], [details], [addDate], [isActive]) VALUES (N'Account Vouchers', N'-', NULL, 1)", con);
                    c.execute("INSERT [dbo].[MenuMaster] ([mName], [details], [addDate], [isActive]) VALUES (N'Production', N'-', NULL, 1)", con);
                    DataTable dt123 = c.getdataset("select mId,mName from MenuMaster where isActive=1", con);
                    if (dt123.Rows.Count > 0)
                    {
                        DataTable ur = c.getdataset("select * from UserRights where isactive=1", con);
                        if (ur.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt123.Rows.Count; i++)
                            {
                                //c.execute("INSERT INTO [UserRights]([uId],[uName],[cId],[mId],[a],[u],[d],[v],[p],[isActive]) values(" + "1" + ",'" + "admin" + "'," + Master.companyId + "," + dt123.Rows[i][0].ToString() + ",'" + "True" + "','" + "True" + "','" + "True" + "','" + "True" + "','" + "True" + "',1)", con);
                                c.execute("Update UserRights set uId='" + "1" + "',mId='" + dt123.Rows[i][0].ToString() + "',a='" + "True" + "',u='" + "True" + "',d='" + "True" + "',v='" + "True" + "',p='" + "True" + "' where uName='" + "admin" + "' and mId='" + dt123.Rows[i][0].ToString() + "'", con);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < dt123.Rows.Count; i++)
                            {
                                c.execute("INSERT INTO [UserRights]([uId],[uName],[cId],[mId],[a],[u],[d],[v],[p],[isActive]) values(" + "1" + ",'" + "admin" + "'," + conpanyid + "," + dt123.Rows[i][0].ToString() + ",'" + "True" + "','" + "True" + "','" + "True" + "','" + "True" + "','" + "True" + "',1)", con);
                            }
                        }
                    }
                    string option44 = @"Alter Table Options add production bit";
                    c.execute(option44, con);
                    string update44 = "update Options set production='" + "0" + "'";
                    c.execute(update44, con);
                    string u = "Update updatedatabase set updatecode='" + "1801261800" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1801291800)
                {
                    string option44 = @"Alter Table Options add issync bit";
                    c.execute(option44, con);
                    string update44 = "update Options set issync='" + "0" + "'";
                    c.execute(update44, con);
                    string bank = c.ExecuteScalar("select mName from MenuMaster where isActive=1 and mName='Bank Entry'", con);
                    if (string.IsNullOrEmpty(bank))
                    {
                            c.execute("INSERT [dbo].[MenuMaster] ([mName], [details], [addDate], [isActive]) VALUES (N'Bank Entry', N'-', NULL, 1)", con);
                    }
                    string u = "Update updatedatabase set updatecode='" + "1801291800" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1801311800)
                {
                    c.execute("INSERT INTO [dbo].[FormFormat]([name],[formname],[type],[setdefault],[isactive])VALUES('" + "ExportType" + "','" + "SalePurchaseOrderSimpleformate" + "','" + "SO" + "','" + "0" + "','" + "1" + "')", con);
                    c.execute("INSERT INTO [dbo].[FormFormat]([name],[formname],[type],[setdefault],[isactive])VALUES('" + "ExportType" + "','" + "SalePurchaseOrderSimpleformate" + "','" + "PO" + "','" + "0" + "','" + "1" + "')", con);
                    string bank = c.ExecuteScalar("select mid from MenuMaster where isActive=1 and mName='Bank Entry'", con);
                    if (string.IsNullOrEmpty(bank))
                    {
                        c.execute("INSERT INTO [UserRights]([uId],[uName],[cId],[mId],[a],[u],[d],[v],[p],[isActive]) values(" + "1" + ",'" + "admin" + "'," + conpanyid + "," + bank + ",'" + "True" + "','" + "True" + "','" + "True" + "','" + "True" + "','" + "True" + "',1)", con);
                    }
                        string u = "Update updatedatabase set updatecode='" + "1801311800" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1802011800)
                {
                    string pos1 = @"Alter Table complainmaster add autoid bigint IDENTITY(1,1)";
                    c.execute(pos1, con);
                    string pos2 = "ALTER TABLE complainmaster ADD CONSTRAINT PK_complainmaster PRIMARY KEY CLUSTERED (autoid);";
                    c.execute(pos2, con);
                    string pos3 = @"Alter Table complainstatus add autoid bigint IDENTITY(1,1)";
                    c.execute(pos3, con);
                    string pos4 = "ALTER TABLE complainstatus ADD CONSTRAINT PK_complainstatus PRIMARY KEY CLUSTERED (autoid);";
                    c.execute(pos4, con);
                    string pos5 = @"Alter Table fromcompany add autoid bigint IDENTITY(1,1)";
                    c.execute(pos5, con);
                    string pos6 = "ALTER TABLE fromcompany ADD CONSTRAINT PK_fromcompany PRIMARY KEY CLUSTERED (autoid);";
                    c.execute(pos6, con);
                    string pos7 = @"Alter Table PartyCompanyDiscount add autoid bigint IDENTITY(1,1)";
                    c.execute(pos7, con);
                    string pos8 = "ALTER TABLE PartyCompanyDiscount ADD CONSTRAINT PK_PartyCompanyDiscount PRIMARY KEY CLUSTERED (autoid);";
                    c.execute(pos8, con);
                    string pos9 = @"Alter Table PartyGroupDiscount add autoid bigint IDENTITY(1,1)";
                    c.execute(pos9, con);
                    string pos10 = "ALTER TABLE PartyGroupDiscount ADD CONSTRAINT PK_PartyGroupDiscount PRIMARY KEY CLUSTERED (autoid);";
                    c.execute(pos10, con);
                   // string pos11 = @"Alter Table paymentreceipt add autoid bigint IDENTITY(1,1)";
                   // c.execute(pos11, con);
                    string pos12 = "ALTER TABLE paymentreceipt ADD CONSTRAINT PK_paymentreceipt PRIMARY KEY CLUSTERED (id);";
                    c.execute(pos12, con);
                    string pos13 = @"Alter Table tocompany add autoid bigint IDENTITY(1,1)";
                    c.execute(pos13, con);
                    string pos14= "ALTER TABLE tocompany ADD CONSTRAINT PK_tocompany PRIMARY KEY CLUSTERED (autoid);";
                    c.execute(pos14, con);
                    string u = "Update updatedatabase set updatecode='" + "1802011800" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1802021800)
                {
                    string user = @"Alter Table Company Alter column LinkRemote nvarchar(MAX)";
                    c.execute(user, con);
                    string u = "Update updatedatabase set updatecode='" + "1802021800" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1802031800)
                {
                    string option44 = @"Alter Table Options add itembillno nvarchar(10)";
                    c.execute(option44, con);
                    string update44 = "update Options set itembillno='" + "Continuous" + "'";
                    c.execute(update44, con);
                    string option4 = @"Alter Table Options add itemprefix nvarchar(50)";
                    c.execute(option4, con);
                    string u = "Update updatedatabase set updatecode='" + "1802031800" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1802051800)
                {
                    string option44 = @"Alter Table ProductMaster add itemnumber nvarchar(MAX)";
                    c.execute(option44, con);
                    string option4 = @"Alter Table ProductMaster add itemimage nvarchar(MAX)";
                    c.execute(option4, con);
                    string syncid = @"Alter Table BillMaster add SyncID nvarchar(MAX)";
                    c.execute(syncid, con);
                    string syncid1 = @"Alter Table Billchargesmaster add SyncID nvarchar(MAX)";
                    c.execute(syncid1, con);
                    string syncid2 = @"Alter Table BillProductMaster add SyncID nvarchar(MAX)";
                    c.execute(syncid2, con);
                    string syncid3 = @"Alter Table SaleOrderchargesmaster add SyncID nvarchar(MAX)";
                    c.execute(syncid3, con);
                    string syncid4 = @"Alter Table SaleOrderMaster add SyncID nvarchar(MAX)";
                    c.execute(syncid4, con);
                    string syncid5 = @"Alter Table SaleOrderProductMaster add SyncID nvarchar(MAX)";
                    c.execute(syncid5, con);
                    string syncid6 = @"Alter Table Ledger add SyncID nvarchar(MAX)";
                    c.execute(syncid6, con);
                    string syncid7 = @"Alter Table Serials add SyncID nvarchar(MAX)";
                    c.execute(syncid7, con);
                    string syncid0 = @"Alter Table BillMaster add SyncDatetime datetime";
                    c.execute(syncid0, con);
                    string syncid11 = @"Alter Table Billchargesmaster add SyncDatetime datetime";
                    c.execute(syncid11, con);
                    string syncid12 = @"Alter Table BillProductMaster add SyncDatetime datetime";
                    c.execute(syncid12, con);
                    string syncid13 = @"Alter Table SaleOrderchargesmaster add SyncDatetime datetime";
                    c.execute(syncid13, con);
                    string syncid14 = @"Alter Table SaleOrderMaster add SyncDatetime datetime";
                    c.execute(syncid14, con);
                    string syncid15 = @"Alter Table SaleOrderProductMaster add SyncDatetime datetime";
                    c.execute(syncid15, con);
                    string syncid16 = @"Alter Table Ledger add SyncDatetime datetime";
                    c.execute(syncid16, con);
                    string syncid17 = @"Alter Table Serials add SyncDatetime datetime";
                    c.execute(syncid17, con);
                    string u = "Update updatedatabase set updatecode='" + "1802051800" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1802061800)
                {
                    string u = "Update updatedatabase set updatecode='" + "1802061800" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1802071800)
                {
                    string syncid16 = @"Alter Table Company add isfactory bit";
                    c.execute(syncid16, con);
                    string update44 = "update Company set isfactory='" + "0" + "'";
                    c.execute(update44, con);
                    string u = "Update updatedatabase set updatecode='" + "1802071800" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1802081800)
                {
                    string syncid16 = @"Alter Table ProductMaster add isshare bit";
                    c.execute(syncid16, con);
                    string update44 = "update ProductMaster set isshare='" + "0" + "'";
                    c.execute(update44, con);
                    string syncid1 = @"Alter Table ProductMaster add SyncID nvarchar(MAX)";
                    c.execute(syncid1, con);
                    string syncid6 = @"Alter Table ProductMaster add SyncDatetime datetime";
                    c.execute(syncid6, con);
                    string syncid10 = @"Alter Table ProductPriceMaster add SyncID nvarchar(MAX)";
                    c.execute(syncid10, con);
                    string syncid61 = @"Alter Table ProductPriceMaster add SyncDatetime datetime";
                    c.execute(syncid61, con);
                    string syncid116 = @"Alter Table ProductPriceMaster add isshare bit";
                    c.execute(syncid116, con);
                    string update414 = "update ProductPriceMaster set isshare='" + "0" + "'";
                    c.execute(update414, con);
                    string u = "Update updatedatabase set updatecode='" + "1802081800" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                    
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1802091800)
                {
                    string u = "Update updatedatabase set updatecode='" + "1802091800" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1802161800)
                {
                    string u = "Update updatedatabase set updatecode='" + "1802161800" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1802171200)
                {
                    string option1 = @"Alter Table Options add Showcompanylist bit";
                    c.execute(option1, con);
                    string update44 = "update Options set Showcompanylist='" + "0" + "'";
                    c.execute(update44, con);
                    string u = "Update updatedatabase set updatecode='" + "1802171200" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1802261800)
                {
                    DataTable client = c.getdataset("select * from ClientMaster where isactive=1");
                    for (int i = 0; i < client.Rows.Count; i++)
                    {
                        c.execute("Update ClientMaster set accountnumber='" + client.Rows[i]["ClientID"].ToString() + "' where ClientID='" + client.Rows[i]["ClientID"].ToString() + "'");
                    }
                    DataTable item = c.getdataset("select * from ProductMaster where isactive=1");
                    for (int i = 0; i < item.Rows.Count; i++)
                    {
                        c.execute("Update ProductMaster set itemnumber='" + item.Rows[i]["ProductID"].ToString() + "' where ProductID='" + item.Rows[i]["ProductID"].ToString() + "'");
                    }
                    string option1 = @"Alter Table updatedatabase add LastSyncDate datetime";
                    c.execute(option1, con);
                  //  string update44 = "update updatedatabase set LastSyncDate='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "'";
                  //  c.execute(update44, con);
                    string u = "Update updatedatabase set updatecode='" + "1802261800" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1803071000)
                {
                    string option1 = @"Alter Table Options add requirstockinfo bit";
                    c.execute(option1, con);
                    string update44 = "update Options set requirstockinfo='" + "0" + "'";
                    c.execute(update44, con);
                    string stock = "SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON CREATE TABLE [dbo].[stockadujestmentitemmaster]([id] [bigint] IDENTITY(1,1) NOT NULL,[stockid] [bigint] NULL,[itemid] [bigint] NULL,[itemname] [nvarchar](max) NULL,[closingstock] [numeric](18, 2) NULL,[adjuststock] [numeric](18, 2) NULL,[remarks] [nvarchar](max) NULL,[isactive] [bit] NULL,CONSTRAINT [PK_stockadujestmentmaster] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]SET ANSI_PADDING OFF";
                    c.execute(stock, con);
                    string stock1 = "SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON CREATE TABLE [dbo].[stockadujestmentmaster]([id] [bigint] IDENTITY(1,1) NOT NULL,[stockdate] [datetime] NULL,[mainremark] [nvarchar](max) NULL,[isactive] [bit] NULL,CONSTRAINT [PK_stockadujestmentmaster_1] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]SET ANSI_PADDING OFF";
                    c.execute(stock1, con);
                    string u = "Update updatedatabase set updatecode='" + "1803071000" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1804051700)
                {
                    string option1 = @"Alter Table Options add itemspeed nvarchar(50)";
                    c.execute(option1, con);
                    string update44 = "update Options set itemspeed='" + "All Columns" + "'";
                    c.execute(update44, con);
                    string a = "SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON SET ANSI_PADDING ON CREATE TABLE [dbo].[tbldebitcreditnote](	[id] [bigint] IDENTITY(1,1) NOT NULL,[VoucherID] [bigint] NULL,[Date1] [datetime] NULL,[TranType] [varchar](255) NULL,[AccountName] [varchar](255) NULL,[Amount] [float] NULL,[DC] [varchar](255) NULL,[ShortNarration] [varchar](max) NULL,[isactive] [bit] NULL,[OT1] [varchar](255) NULL,[OT2] [varchar](255) NULL,[OT3] [varchar](255) NULL,[AccountID] [float] NULL,[OT4] [varchar](255) NULL,[OT5] [varchar](255) NULL,[OT6] [varchar](255) NULL,[OD1] [datetime] NULL,CONSTRAINT [PK_tbldebitcreditnote] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]SET ANSI_PADDING OFF";
                    c.execute(a, con);
                    DataTable credit = c.getdataset("select * from Ledger where isactive=1 and TranType='DEBIT NOTE'", con);
                    for (int i = 0; i < credit.Rows.Count; i++)
                    {
                        c.execute("INSERT INTO [dbo].[tbldebitcreditnote]([VoucherID],[Date1],[TranType],[AccountName],[Amount],[DC],[ShortNarration],[isactive],[OT1],[OT2],[OT3],[AccountID],[OT4],[OT5],[OT6],[OD1]) values ('" + credit.Rows[i]["VoucherID"].ToString() + "','" + credit.Rows[i]["Date1"].ToString() + "','" + credit.Rows[i]["TranType"].ToString() + "','" + credit.Rows[i]["AccountName"].ToString() + "','" + credit.Rows[i]["Amount"].ToString() + "','" + credit.Rows[i]["DC"].ToString() + "','" + credit.Rows[i]["ShortNarration"].ToString() + "','" + credit.Rows[i]["isactive"].ToString() + "','" + credit.Rows[i]["OT1"].ToString() + "','" + credit.Rows[i]["OT2"].ToString() + "','" + credit.Rows[i]["OT3"].ToString() + "','" + credit.Rows[i]["AccountID"].ToString() + "','" + credit.Rows[i]["OT4"].ToString() + "','" + credit.Rows[i]["OT5"].ToString() + "','" + credit.Rows[i]["OT6"].ToString() + "','" + credit.Rows[i]["OD1"].ToString() + "')", con);
                    }
                    DataTable debit = c.getdataset("select * from Ledger where isactive=1 and TranType='CREDIT NOTE'", con);
                    for (int i = 0; i < debit.Rows.Count; i++)
                    {
                        c.execute("INSERT INTO [dbo].[tbldebitcreditnote]([VoucherID],[Date1],[TranType],[AccountName],[Amount],[DC],[ShortNarration],[isactive],[OT1],[OT2],[OT3],[AccountID],[OT4],[OT5],[OT6],[OD1]) values ('" + debit.Rows[i]["VoucherID"].ToString() + "','" + debit.Rows[i]["Date1"].ToString() + "','" + debit.Rows[i]["TranType"].ToString() + "','" + debit.Rows[i]["AccountName"].ToString() + "','" + debit.Rows[i]["Amount"].ToString() + "','" + debit.Rows[i]["DC"].ToString() + "','" + debit.Rows[i]["ShortNarration"].ToString() + "','" + debit.Rows[i]["isactive"].ToString() + "','" + debit.Rows[i]["OT1"].ToString() + "','" + debit.Rows[i]["OT2"].ToString() + "','" + debit.Rows[i]["OT3"].ToString() + "','" + debit.Rows[i]["AccountID"].ToString() + "','" + debit.Rows[i]["OT4"].ToString() + "','" + debit.Rows[i]["OT5"].ToString() + "','" + debit.Rows[i]["OT6"].ToString() + "','" + debit.Rows[i]["OD1"].ToString() + "')", con);
                    }
                    string u = "Update updatedatabase set updatecode='" + "1804051700" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1804241100)
                {
                    string user = @"Alter Table Voucher Alter column OT1 nvarchar(MAX)";
                    c.execute(user, con);
                    string u = "Update updatedatabase set updatecode='" + "1804241100" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1804261100)
                {
                    string option = @"Alter Table BillMaster add cusname nvarchar(MAX)";
                    c.execute(option, con);
                    string option1 = @"Alter Table BillMaster add cusadd nvarchar(MAX)";
                    c.execute(option1, con);
                    string option2 = @"Alter Table BillMaster add cuscity nvarchar(MAX)";
                    c.execute(option2, con);
                    string option3 = @"Alter Table BillMaster add cusphone nvarchar(MAX)";
                    c.execute(option3, con);
                    string option4 = @"Alter Table BillMaster add cusmobile nvarchar(MAX)";
                    c.execute(option4, con);
                    string option5 = @"Alter Table BillMaster add cuspancard nvarchar(MAX)";
                    c.execute(option5, con);
                    string option6 = @"Alter Table BillMaster add cusadhar nvarchar(MAX)";
                    c.execute(option6, con);
                    string option7 = @"Alter Table SaleOrderMaster add cusname nvarchar(MAX)";
                    c.execute(option7, con);
                    string option8 = @"Alter Table SaleOrderMaster add cusadd nvarchar(MAX)";
                    c.execute(option8, con);
                    string option9 = @"Alter Table SaleOrderMaster add cuscity nvarchar(MAX)";
                    c.execute(option9, con);
                    string option10 = @"Alter Table SaleOrderMaster add cusphone nvarchar(MAX)";
                    c.execute(option10, con);
                    string option11 = @"Alter Table SaleOrderMaster add cusmobile nvarchar(MAX)";
                    c.execute(option11, con);
                    string option12 = @"Alter Table SaleOrderMaster add cuspancard nvarchar(MAX)";
                    c.execute(option12, con);
                    string option13 = @"Alter Table SaleOrderMaster add cusadhar nvarchar(MAX)";
                    c.execute(option13, con);
                    string u = "Update updatedatabase set updatecode='" + "1804261100" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1805011900)
                {
                    string user2 = @"Alter Table Ledger Alter column OT6 nvarchar(MAX)";
                    c.execute(user2, con);
                    string user = @"Alter Table BillMaster Alter column refno nvarchar(MAX)";
                    c.execute(user, con);
                    string user1 = @"Alter Table SaleOrderMaster Alter column refno nvarchar(MAX)";
                    c.execute(user1, con);
                    string gvm = "SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON SET ANSI_PADDING ON CREATE TABLE [dbo].[tblgstvouchermaster]([id] [bigint] IDENTITY(1,1) NOT NULL,[date] [datetime] NULL,[entry] [nvarchar](max) NULL,[terms] [nvarchar](10) NULL,[party] [bigint] NULL,[type] [bigint] NULL,[onaccount] [bigint] NULL,[vchno] [bigint] NULL,[Bill_no] [bigint] NULL,[billno] [nvarchar](max) NULL,[transportdetails] [nvarchar](max) NULL,[delieveryat] [nvarchar](max) NULL,[fraight] [nvarchar](max) NULL,[vehicleno] [nvarchar](max) NULL,[grrrno] [nvarchar](max) NULL,[narration] [nvarchar](max) NULL,[billtype] [varchar](50) NULL,[totalbasic] [numeric](18, 2) NULL,[totxltax] [numeric](18, 2) NULL,[totalamount] [numeric](18, 2) NULL,[totalcharges] [numeric](18, 2) NULL,[totalrounoff] [numeric](18, 2) NULL,[totalfinalamount] [numeric](18, 2) NULL,[isactive] [bit] NULL, CONSTRAINT [PK_tblgstvouchermaster] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] SET ANSI_PADDING OFF";
                    c.execute(gvm, con);
                    string gvpm = "SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON SET ANSI_PADDING ON CREATE TABLE [dbo].[tblgstvoucherproductmaster]([id] [bigint] IDENTITY(1,1) NOT NULL,[taxper] [nvarchar](50) NULL,[hsn] [nvarchar](max) NULL,[taxableamount] [numeric](18, 2) NULL,[sgstamt] [numeric](18, 2) NULL,[cgstamt] [numeric](18, 2) NULL,[igstamt] [numeric](18, 2) NULL,[addtax] [numeric](18, 2) NULL,[netamt] [numeric](18, 2) NULL,[description] [nvarchar](max) NULL,[party] [bigint] NULL,[billtype] [varchar](50) NULL,[Bill_No] [bigint] NULL,[billno] [nvarchar](max) NULL,[isactive] [bit] NULL,CONSTRAINT [PK_tblgstvoucherproductmaster] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]SET ANSI_PADDING OFF";
                    c.execute(gvpm, con);
                    string gvcm = "SET ANSI_NULLS ON SET QUOTED_IDENTIFIER ON SET ANSI_PADDING ON CREATE TABLE [dbo].[tblgstvoucherchargesmaster]([id] [bigint] IDENTITY(1,1) NOT NULL,[perticulars] [nvarchar](max) NULL,[remarks] [nvarchar](max) NULL,[onvalue] [numeric](18, 2) NULL,[at] [numeric](18, 2) NULL,[plusminus] [varchar](50) NULL,[amount] [numeric](18, 2) NULL,[party] [bigint] NULL,[billtype] [varchar](50) NULL,[Bill_No] [bigint] NULL,[billno] [nvarchar](max) NULL,[valueofexp] [numeric](18, 2) NULL,[tax] [numeric](18, 2) NULL,[sgst] [numeric](18, 2) NULL,[cgst] [numeric](18, 2) NULL,[igst] [numeric](18, 2) NULL,[additax] [numeric](18, 2) NULL,[addtaxamt] [numeric](18, 2) NULL,[chargeid] [bigint] NULL,[isactive] [bit] NULL,CONSTRAINT [PK_tblgstvoucherchargesmaster] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]SET ANSI_PADDING OFF";
                    c.execute(gvcm, con);
                    string u = "Update updatedatabase set updatecode='" + "1805011900" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1805141900)
                {
                    string option1 = @"Alter Table Options add autoroundoffingstvouchers bit";
                    c.execute(option1, con);
                    string update44 = "update Options set autoroundoffingstvouchers='" + "0" + "'";
                    c.execute(update44, con);
                    string option2 = @"Alter Table tblgstvoucherproductmaster add sgstper numeric(18, 2)";
                    c.execute(option2, con);
                    string option3 = @"Alter Table tblgstvoucherproductmaster add cgstper numeric(18, 2)";
                    c.execute(option3, con);
                    string option4 = @"Alter Table tblgstvoucherproductmaster add igstper numeric(18, 2)";
                    c.execute(option4, con);
                    string option39 = @"Alter Table tblgstvouchermaster add originalbillno nvarchar(MAX)";
                    c.execute(option39, con);
                    string option390 = @"Alter Table tblgstvouchermaster add originalbilldate nvarchar(50)";
                    c.execute(option390, con);
                    string u = "Update updatedatabase set updatecode='" + "1805141900" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1805221100)
                {
                    string option15 = @"Alter Table BillProductMaster add ClientID bigint";
                    c.execute(option15, con);
                    string option151 = @"Alter Table Billchargesmaster add ClientID bigint";
                    c.execute(option151, con);
                    string option30 = @"Alter Table SaleOrderProductMaster add ClientID bigint";
                    c.execute(option30, con);
                    string option301 = @"Alter Table SaleOrderchargesmaster add ClientID bigint";
                    c.execute(option301, con);
                    string option2 = @"Alter Table tblgstvoucherproductmaster add fkid bigint";
                    c.execute(option2, con);
                    string option3 = @"Alter Table tblgstvoucherchargesmaster add fkid bigint";
                    c.execute(option3, con);
                    string option24 = @"Alter Table BillProductMaster add fkid bigint";
                    c.execute(option24, con);
                    string option35 = @"Alter Table Billchargesmaster add fkid bigint";
                    c.execute(option35, con);
                    string option26 = @"Alter Table SaleOrderProductMaster add fkid bigint";
                    c.execute(option26, con);
                    string option37 = @"Alter Table SaleOrderchargesmaster add fkid bigint";
                    c.execute(option37, con);
                    string user = @"Alter Table BillPOSProductMaster Alter column Qty numeric(18, 2)";
                    c.execute(user, con);
                    string option1 = @"Alter Table Options add pdfpath nvarchar(MAX)";
                    c.execute(option1, con);
                    string u = "Update updatedatabase set updatecode='" + "1805221100" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1807271000)
                {


                    string option1 = @"Alter Table ProductMaster add OT1 nvarchar(MAX)";
                    c.execute(option1, con);
                    string option2 = @"Alter Table ProductMaster add OT2 nvarchar(MAX)";
                    c.execute(option2, con);
                    string option3 = @"Alter Table ProductMaster add OT3 nvarchar(MAX)";
                    c.execute(option3, con);
                    string option4 = @"Alter Table ProductMaster add OT4 nvarchar(MAX)";
                    c.execute(option4, con);
                    string option5 = @"Alter Table ProductMaster add OT5 nvarchar(MAX)";
                    c.execute(option5, con);
                    string option6 = @"Alter Table ProductMaster add OT6 nvarchar(MAX)";
                    c.execute(option6, con);
                    string option7 = @"Alter Table ProductMaster add OT7 nvarchar(MAX)";
                    c.execute(option7, con);
                    string option8 = @"Alter Table ProductMaster add OT8 nvarchar(MAX)";
                    c.execute(option8, con);
                    string option9 = @"Alter Table ProductMaster add OT9 nvarchar(MAX)";
                    c.execute(option9, con);
                    string option10 = @"Alter Table ProductMaster add OT10 nvarchar(MAX)";
                    c.execute(option10, con);
                    string option11 = @"Alter Table ProductMaster add OT11 nvarchar(MAX)";
                    c.execute(option11, con);
                    string option12 = @"Alter Table ProductMaster add OT12 nvarchar(MAX)";
                    c.execute(option12, con);
                    string option13 = @"Alter Table ProductMaster add OT13 nvarchar(MAX)";
                    c.execute(option13, con);
                    string option14 = @"Alter Table ProductMaster add OT14 nvarchar(MAX)";
                    c.execute(option14, con);
                    string option15 = @"Alter Table ProductMaster add OT15 nvarchar(MAX)";
                    c.execute(option15, con);
                    string u = "Update updatedatabase set updatecode='" + "1807271000" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1808171100)
                {
                    string option1 = @"Alter Table ClientMaster add Deliveryat nvarchar(MAX)";
                    c.execute(option1, con);
                    string sa2 = @"Alter Table ProductMaster add minstock numeric(18, 2)";
                    c.execute(sa2, con);
                    string update44 = "update ProductMaster set minstock='" + "0" + "'";
                    c.execute(update44, con);
                    string sa3 = @"Alter Table ProductMaster add maxstock numeric(18, 2)";
                    c.execute(sa3, con);
                    string update4 = "update ProductMaster set maxstock='" + "0" + "'";
                    c.execute(update4, con);
                    string sa4 = @"Alter Table ProductMaster add reorderqty numeric(18, 2)";
                    c.execute(sa4, con);
                    string update40 = "update ProductMaster set reorderqty='" + "0" + "'";
                    c.execute(update40, con);
                    string u = "Update updatedatabase set updatecode='" + "1808171100" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1810101100)
                {
                    string option1 = @"Alter Table stockadujestmentitemmaster add stockdate datetime";
                    c.execute(option1, con);
                    string sa2 = @"Alter Table stockadujestmentitemmaster add batch nvarchar(50)";
                    c.execute(sa2, con);
                    string u = "Update updatedatabase set updatecode='" + "1810101100" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1901161830)
                {
                    string option28 = @"Alter Table Options add chkRestrictPriceinPOPC bit";
                    c.execute(option28, con);
                    string update1 = "update Options set chkRestrictPriceinPOPC='" + "0" + "'";
                    c.execute(update1, con);


                    string aa = "Update updatedatabase set updatecode='" + "1901161830" + "' where id='" + "1" + "'";
                    c.execute(aa, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1901241200)
                {
                    string option1 = @"Alter Table billposproductmaster add serialno nvarchar(MAX)";
                    c.execute(option1, con);

                    string u = "Update updatedatabase set updatecode='" + "1901241200" + "' where id='" + "1" + "'";
                    c.execute(u, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1901291200)
                {
                    string option28 = @"Alter Table Options add userlog bit";
                    c.execute(option28, con);
                    string update1 = "update Options set userlog='" + "0" + "'";
                    c.execute(update1, con);


                    string aa = "Update updatedatabase set updatecode='" + "1901291200" + "' where id='" + "1" + "'";
                    c.execute(aa, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1901291208)
                {
                    string option28 = @"Alter Table Options add ShowTotalBillAmount bit";
                    c.execute(option28, con);
                    string update1 = "update Options set ShowTotalBillAmount='" + "0" + "'";
                    c.execute(update1, con);
                    string option29 = @"Alter Table Options add itemload bit";
                    c.execute(option29, con);
                    string update12 = "update Options set itemload='" + "0" + "'";
                    c.execute(update12, con);
                    string option101 = @"Alter Table Options add kot bit";
                    c.execute(option101, con);
                    string option4 = @"Alter Table BillPOSProductMaster add itemid bigint";
                    c.execute(option4, con);
                    string aa = "Update updatedatabase set updatecode='" + "1901291208" + "' where id='" + "1" + "'";
                    c.execute(aa, con);
                }
                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1901301208)
                {
                    string option28 = @"Alter Table Options add txtposadd1 nvarchar(50),txtposadd2 nvarchar(50),txtposadd3 nvarchar(50),txtposadd4 nvarchar(50),txtposadd5 nvarchar(50)";
                    c.execute(option28, con);
                    string update1 = "update Options set txtposadd1='" + "" + "',txtposadd2='" + "" + "',txtposadd3='" + "" + "',txtposadd4='" + "" + "',txtposadd5='" + "" + "'";
                    c.execute(update1, con);
                    string aa = "Update updatedatabase set updatecode='" + "1901301208" + "' where id='" + "1" + "'";
                    c.execute(aa, con);
                }

                if (Convert.ToInt32(dt.Rows[0]["updatecode"].ToString()) < 1904121600)
                {

                    string option28 = @"Alter Table Options add posorderprefix nvarchar(50)";
                    c.execute(option28, con);

                    string aa = "Update updatedatabase set updatecode='" + "1904121600" + "' where id='" + "1" + "'";
                    c.execute(aa, con);
                }
            }


        }


    }
}
