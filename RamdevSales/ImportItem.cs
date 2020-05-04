using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Configuration;
using DocumentFormat.OpenXml.Drawing;
using System.IO;
using System.Data.Common;
using System.Data.SqlClient;
using LoggingFramework;

namespace RamdevSales
{
    public partial class ImportItem : Form
    {
        private Master master;
        private TabControl tabControl;
        string filepath, appPath;
        Connection sql = new Connection();
        DataTable company = new DataTable();
        DataTable dt = new DataTable();
        OpenFileDialog ofd = new OpenFileDialog();
        DataTable itemgroup = new DataTable();
        private Itemmaster itemmaster;

        public ImportItem()
        {
            InitializeComponent();
        }





        public ImportItem(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }

        private void btnimport_Click(object sender, EventArgs e)
        {
            LoadNewFile();
        }
        private void LoadNewFile()
        {
            #region

            ofd.Title = "Select a Document";
            ofd.Filter = "xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\Excel\";
            if (Directory.Exists(appPath) == false)
            {
                Directory.CreateDirectory(appPath);
            }
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string iName = ofd.SafeFileName;
                    filepath = ofd.FileName;
                    string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filepath + ";Extended Properties=\"Excel 12.0 Xml;HDR=Yes;IMEX=1;\"";
                    using (OleDbConnection excel_con = new OleDbConnection(connectionString))
                    {
                        excel_con.Open();
                        using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [ItemName$A1:AC]", excel_con))//main table == productmaster
                        {

                            oda.Fill(dt);
                            if (dt.Rows.Count > 2000)
                            {
                                MessageBox.Show("You Enter Only 2000 Records On One Time");
                                return;
                            }
                            else
                            {
                                try
                                {
                                    File.Delete(appPath + iName);
                                }
                                catch
                                {
                                }
                                File.Copy(filepath, appPath + iName);
                                lblitem.Text = iName;


                            }

                        }

                        excel_con.Close();
                    }


                    //   importexcel();
                    //saveimage(pictureBox1.Image, appPath);
                }
                catch (Exception exp)
                {

                    MessageBox.Show("Unable to open file " + exp.Message);
                }
                finally
                {
                    // con.Close();
                }
            }
            else
            {
                ofd.Dispose();
            }

            #endregion
        }
        public void importexcel()
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filepath + ";Extended Properties=\"Excel 12.0 Xml;HDR=Yes;IMEX=1;\"";
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");
            using (DbConnection connection = factory.CreateConnection())
            {
                using (OleDbConnection excel_con = new OleDbConnection(connectionString))
                {
                    excel_con.Open();
                    //  string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["tbl_shippingcharge_data"].ToString();
                    DataTable dtExcelData = new DataTable();

                    //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                    dtExcelData.Columns.AddRange(new DataColumn[9] {
                                                new DataColumn("GroupName", typeof(string)),
                                                new DataColumn("Product_Name", typeof(string)),
                                                new DataColumn("Unit",typeof(string)),
                                                new DataColumn("Altunit",typeof(string)),
                                                new DataColumn("Convfactor",typeof(int)),
                                                new DataColumn("Packing",typeof(string)),
                                                new DataColumn("IsBatch",typeof(Boolean)),
                                                new DataColumn("Hsn_Sac_Code",typeof(int)),
                                                new DataColumn("isactive",typeof(Boolean))});
                    //new DataColumn("vendorId", typeof(int))

                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [ItemName$A1:I]", excel_con))//main table == productmaster
                    {
                        oda.Fill(dtExcelData);
                        DataTable dt = new DataTable();
                        oda.Fill(dt);
                        for (int x = 0; x < dt.Rows.Count; x++)
                        {
                        }
                    }
                    excel_con.Close();

                    string consString = ConfigurationManager.ConnectionStrings["qry"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(consString))
                    {
                        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                        {
                            //Set the database table name
                            sqlBulkCopy.DestinationTableName = "dbo.DemoProductMaster";//dummy table demoproductmaster

                            //[OPTIONAL]: Map the Excel columns with that of the database table
                            sqlBulkCopy.ColumnMappings.Add("GroupName", "GroupName");
                            sqlBulkCopy.ColumnMappings.Add("Product_Name", "Product_Name");
                            sqlBulkCopy.ColumnMappings.Add("Unit", "Unit");
                            sqlBulkCopy.ColumnMappings.Add("Altunit", "Altunit");
                            sqlBulkCopy.ColumnMappings.Add("Convfactor", "Convfactor");
                            sqlBulkCopy.ColumnMappings.Add("Packing", "Packing");
                            sqlBulkCopy.ColumnMappings.Add("IsBatch", "IsBatch");
                            sqlBulkCopy.ColumnMappings.Add("Hsn_Sac_Code", "Hsn_Sac_Code");
                            sqlBulkCopy.ColumnMappings.Add("isactive", "isactive");
                            //sqlBulkCopy.ColumnMappings.Add("vendorId", "vendorId");
                            con.Open();
                            sqlBulkCopy.WriteToServer(dtExcelData);
                            con.Close();
                        }
                    }
                }
            }


        }


        int flag = 0;
        private void btnimoortdata_Click(object sender, EventArgs e)
        {

            try
            {
                string iName = ofd.SafeFileName;
                filepath = ofd.FileName;
                string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filepath + ";Extended Properties=\"Excel 12.0 Xml;HDR=Yes;IMEX=1;\"";
                using (OleDbConnection excel_con = new OleDbConnection(connectionString))
                {
                    excel_con.Open();
                    DataTable sqldt = new DataTable();
                    //  sqldt = sql.getdataset("select p.Product_Name,c.companyname,p.GroupName,p.Unit,p.Altunit,p.Convfactor,p.Packing,p.IsBatch,p.Hsn_Sac_Code,pp.Batchno,pp.BasicPrice,pp.SalePrice,pp.MRP,pp.PurchasePrice,pp.Barcode,pp.OpStock,pp.ExpDt,pp.mfgdt,pp.Expdays,pp.SelfVal,pp.minsaleprice,pp.oploose,pp.opstockval,i.saletypeid,i.system,i.category,i.sgst,i.cgst,i.igst,i.additax,i.onwhich,i.isonmrp,i.isonfreegoods from ProductMaster p inner join ProductPriceMaster pp on p.ProductID=pp.ProductID inner join ItemTaxMaster i on pp.ProductID=i.ProductID inner join CompanyMaster c on p.CompanyID=c.CompanyID where p.isactive=1 and pp.isactive=1 and i.isactive=1");
                    using (OleDbDataAdapter oda1 = new OleDbDataAdapter("SELECT * FROM [ItemName$" + txtform.Text + ":" + txtto.Text + "]", excel_con))//main table == productmaster
                    {
                        DataTable dt1 = new DataTable();
                        oda1.Fill(dt1);
                        for (int i = dt1.Rows.Count - 1; i >= 0; i--)
                        {
                            if (dt1.Rows[i][1] == DBNull.Value)
                                dt1.Rows[i].Delete();
                        }
                        dt1.AcceptChanges();
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            //for (int j = 0; j < sqldt.Rows.Count;j++)
                            //{
                            //  sqldt = sql.getdataset("Select ProductID,Product_Name,CompanyID,taxslab from ProductMaster where Product_Name='" + dt.Rows[i]["Product_Name"].ToString() + "'");
                            sqldt = sql.getdataset("Select p.ProductID,p.Product_Name,p.CompanyID,p.taxslab from ProductMaster p inner join ProductPriceMaster pp on p.ProductID=pp.Productid where p.isactive=1 and pp.isactive=1 and Product_Name='" + dt.Rows[i]["Product_Name"].ToString() + "' and pp.Batchno='" + dt.Rows[i]["BatchNo"].ToString() + "'");
                            company = sql.getdataset("select * from CompanyMaster where Companyname='" + dt.Rows[i]["companyname"].ToString() + "'");
                            //   DataTable companyid = sql.getdataset("select * from CompanyMaster where companyname='" + dt.Rows[0]["companyname"].ToString() + "'");
                            itemgroup = sql.getdataset("select * from itemgroupmaster where Itemgroupname='" + dt.Rows[i]["GroupName"].ToString() + "'");
                            if (itemgroup.Rows.Count <= 0)
                            {
                                sql.execute("Insert into itemgroupmaster (Itemgroupname) values ('" + dt.Rows[i]["GroupName"].ToString() + "')");
                                itemgroup = sql.getdataset("select * from itemgroupmaster where Itemgroupname='" + dt.Rows[i]["GroupName"].ToString() + "'");
                            }
                            if (sqldt.Rows.Count > 0)
                            {
                                string str = "Update ProductMaster set SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "', itemnumber='" + dt.Rows[i]["itemnumber"].ToString() + "',Product_Name='" + dt.Rows[i]["Product_Name"].ToString() + "',CompanyID='" + company.Rows[0]["CompanyID"].ToString() + "', GroupName='" + dt.Rows[i]["GroupName"].ToString() + "',Unit='" + dt.Rows[i]["Unit"].ToString() + "',Altunit='" + dt.Rows[i]["AltUnit"].ToString() + "',Convfactor='" + dt.Rows[i]["Convfactor"].ToString() + "',Packing='" + dt.Rows[i]["Packing"].ToString() + "',IsBatch='" + dt.Rows[i]["IsBatch"].ToString() + "',Hsn_Sac_Code='" + dt.Rows[i]["Hsn_Sac_Code"].ToString() + "',isserial='" + dt.Rows[i]["isserial"].ToString() + "',cessper='" + dt.Rows[i]["cessper"].ToString() + "',cessamt='" + dt.Rows[i]["cessamt"].ToString() + "',taxslab='" + dt.Rows[i]["taxslab"].ToString() + "',OT1='" + dt.Rows[i]["OT1"].ToString() + "'Where Product_Name='" + dt.Rows[i]["Product_Name"].ToString() + "'";
                                sql.execute(str);
                                LogGenerator.Info(str);
                                string str1 = "Update ProductPriceMaster set SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',Batchno='" + dt.Rows[i]["Batchno"].ToString() + "',BasicPrice='" + dt.Rows[i]["BasicPrice"].ToString() + "',SalePrice='" + dt.Rows[i]["SalePrice"].ToString() + "',MRP='" + dt.Rows[i]["MRP"].ToString() + "',PurchasePrice='" + dt.Rows[i]["PurchasePrice"].ToString() + "',Barcode='" + dt.Rows[i]["Barcode"].ToString() + "',OpStock='" + dt.Rows[i]["OpStock"].ToString() + "',ExpDt='" + dt.Rows[i]["ExpDt"].ToString() + "',mfgdt='" + dt.Rows[i]["mfgdt"].ToString() + "',Expdays='" + dt.Rows[i]["Expdays"].ToString() + "',SelfVal='" + dt.Rows[i]["SelfVal"].ToString() + "',minsaleprice='" + dt.Rows[i]["minsaleprice"].ToString() + "',oploose='" + dt.Rows[i]["oploose"].ToString() + "',opstockval='" + dt.Rows[i]["opstockval"].ToString() + "'where ProductID='" + sqldt.Rows[0]["ProductID"].ToString() + "'";
                                sql.execute(str1);
                                LogGenerator.Info(str1);
                                // string str2 = "Update TaxSlabMaster set saletypeid='" + dt.Rows[i]["saletypeid"].ToString() + "',system='" + dt.Rows[i]["system"].ToString() + "',category='" + dt.Rows[i]["category"].ToString() + "',sgst='" + dt.Rows[i]["sgst"].ToString() + "',cgst='" + dt.Rows[i]["cgst"].ToString() + "',igst='" + dt.Rows[i]["igst"].ToString() + "',additax='" + dt.Rows[i]["additax"].ToString() + "',onwhich='" + dt.Rows[i]["onwhich"].ToString() + "',isonmrp='" + dt.Rows[i]["isonmrp"].ToString() + "',isonfreegoods='" + dt.Rows[i]["isonfreegoods"].ToString() + "' where Taxslabname='" + sqldt.Rows[0]["taxslab"].ToString() + "'";
                                //sql.execute(str2);
                                //LogGenerator.Info(str2);
                            }
                            else
                            {
                                DataTable proid1 = sql.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + dt.Rows[i]["Product_Name"].ToString() + "'");
                                if (proid1.Rows.Count == 0)
                                {
                                    Guid guid;
                                    guid = Guid.NewGuid();
                                    string str3 = "INSERT INTO [dbo].[ProductMaster]([Product_Name],[CompanyID],[GroupName],[Unit],[Altunit],[Convfactor],[Packing],[IsBatch],[Hsn_Sac_Code],[isserial],[cessper],[cessamt],[taxslab],[SyncID],[SyncDatetime],[itemnumber],[isactive],[OT1]) VALUES ('" + dt.Rows[i]["Product_Name"].ToString() + "','" + company.Rows[0]["CompanyID"].ToString() + "','" + dt.Rows[i]["GroupName"].ToString() + "','" + dt.Rows[i]["Unit"].ToString() + "','" + dt.Rows[i]["AltUnit"].ToString() + "','" + dt.Rows[i]["Convfactor"].ToString() + "','" + dt.Rows[i]["Packing"].ToString() + "','" + dt.Rows[i]["IsBatch"].ToString() + "','" + dt.Rows[i]["Hsn_Sac_Code"].ToString() + "','" + dt.Rows[i]["isserial"].ToString() + "','" + dt.Rows[i]["cessper"].ToString() + "','" + dt.Rows[i]["cessamt"].ToString() + "','" + dt.Rows[i]["taxslab"].ToString() + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "','" + dt.Rows[i]["itemnumber"].ToString() +  "','1','"+ dt.Rows[i]["OT1"].ToString() +"')";
                                    sql.execute(str3);
                                    LogGenerator.Info(str3);
                                }
                                else if (dt.Rows[i]["Product_Name"].ToString() != proid1.Rows[0]["Product_Name"].ToString())
                                {
                                    Guid guid;
                                    guid = Guid.NewGuid();
                                    string str3 = "INSERT INTO [dbo].[ProductMaster]([Product_Name],[CompanyID],[GroupName],[Unit],[Altunit],[Convfactor],[Packing],[IsBatch],[Hsn_Sac_Code],[isserial],[cessper],[cessamt],[taxslab],[SyncID],[SyncDatetime],[itemnumber],[isactive],[OT1]) VALUES ('" + dt.Rows[i]["Product_Name"].ToString() + "','" + company.Rows[0]["CompanyID"].ToString() + "','" + dt.Rows[i]["GroupName"].ToString() + "','" + dt.Rows[i]["Unit"].ToString() + "','" + dt.Rows[i]["AltUnit"].ToString() + "','" + dt.Rows[i]["Convfactor"].ToString() + "','" + dt.Rows[i]["Packing"].ToString() + "','" + dt.Rows[i]["IsBatch"].ToString() + "','" + dt.Rows[i]["Hsn_Sac_Code"].ToString() + "','" + dt.Rows[i]["isserial"].ToString() + "','" + dt.Rows[i]["cessper"].ToString() + "','" + dt.Rows[i]["cessamt"].ToString() + "','" + dt.Rows[i]["taxslab"].ToString() + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "','" + dt.Rows[i]["itemnumber"].ToString() + "','1','" + dt.Rows[i]["OT1"].ToString() + "')";
                                    sql.execute(str3);
                                    LogGenerator.Info(str3);
                                }
                                Guid guid1;
                                guid1 = Guid.NewGuid();
                                DataTable proid = sql.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + dt.Rows[i]["Product_Name"].ToString() + "'");
                                string str4 = "INSERT INTO [dbo].[ProductPriceMaster]([Productid],[Batchno],[BasicPrice],[SalePrice],[MRP],[PurchasePrice],[Barcode],[OpStock],[ExpDt],[mfgdt],[Expdays],[SelfVal],[minsaleprice],[oploose],[opstockval],[SyncID],[SyncDatetime],[isactive])VALUES('" + proid.Rows[0]["ProductID"].ToString() + "','" + dt.Rows[i]["Batchno"].ToString() + "','" + dt.Rows[i]["BasicPrice"].ToString() + "','" + dt.Rows[i]["SalePrice"].ToString() + "','" + dt.Rows[i]["MRP"].ToString() + "','" + dt.Rows[i]["PurchasePrice"].ToString() + "','" + dt.Rows[i]["Barcode"].ToString() + "','" + dt.Rows[i]["OpStock"].ToString() + "','" + dt.Rows[i]["ExpDt"].ToString() + "','" + dt.Rows[i]["mfgdt"].ToString() + "','" + dt.Rows[i]["Expdays"].ToString() + "','" + dt.Rows[i]["SelfVal"].ToString() + "','" + dt.Rows[i]["minsaleprice"].ToString() + "','" + dt.Rows[i]["oploose"].ToString() + "','" + dt.Rows[i]["opstockval"].ToString() + "','" + guid1 + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "','1')";
                                sql.execute(str4);
                                LogGenerator.Info(str4);
                                //  string str5 = "INSERT INTO [dbo].[TaxSlabMaster]([saletypeid],[system],[category],[sgst],[cgst],[igst],[additax],[onwhich],[isonmrp],[isonfreegoods],[isactive])VALUES('"  + dt.Rows[i]["saletypeid"].ToString() + "','" + dt.Rows[i]["system"].ToString() + "','" + dt.Rows[i]["category"].ToString() + "','" + dt.Rows[i]["sgst"].ToString() + "','" + dt.Rows[i]["cgst"].ToString() + "','" + dt.Rows[i]["igst"].ToString() + "','" + dt.Rows[i]["additax"].ToString() + "','" + dt.Rows[i]["onwhich"].ToString() + "','" + dt.Rows[i]["isonmrp"].ToString() + "','" + dt.Rows[i]["isonfreegoods"].ToString() + "','1')";
                                //sql.execute(str5);
                                //LogGenerator.Info(str5);
                            }



                            //  }

                        }

                    }
                    txtform.Text = "";
                    txtto.Text = "";
                    MessageBox.Show("Data Update Sucessfully");
                    excel_con.Close();
                    master.RemoveCurrentTab();
                    excel_con.Dispose();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {


            }

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
        private void ImportItem_Load(object sender, EventArgs e)
        {

        }

        private void txtform_Enter(object sender, EventArgs e)
        {
            txtform.BackColor = Color.LightYellow;
        }

        private void txtto_Enter(object sender, EventArgs e)
        {
            txtto.BackColor = Color.LightYellow;
        }

        private void txtto_Leave(object sender, EventArgs e)
        {
            txtto.BackColor = Color.White;
        }

        private void txtform_Leave(object sender, EventArgs e)
        {
            txtform.BackColor = Color.White;
        }

        private void btnimport_Enter(object sender, EventArgs e)
        {
            btnimport.UseVisualStyleBackColor = false;
            btnimport.BackColor = System.Drawing.Color.FromArgb(118, 72, 233);
            btnimport.ForeColor = System.Drawing.Color.White;
        }

        private void btnimport_Leave(object sender, EventArgs e)
        {
            btnimport.UseVisualStyleBackColor = true;
            btnimport.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnimport.ForeColor = System.Drawing.Color.White;
        }

        private void btnimport_MouseEnter(object sender, EventArgs e)
        {
            btnimport.UseVisualStyleBackColor = false;
            btnimport.BackColor = System.Drawing.Color.FromArgb(118, 72, 233);
            btnimport.ForeColor = System.Drawing.Color.White;
        }

        private void btnimport_MouseLeave(object sender, EventArgs e)
        {
            btnimport.UseVisualStyleBackColor = true;
            btnimport.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnimport.ForeColor = System.Drawing.Color.White;
        }

        private void btnimoortdata_Enter(object sender, EventArgs e)
        {
            btnimoortdata.UseVisualStyleBackColor = false;
            btnimoortdata.BackColor = System.Drawing.Color.FromArgb(118, 72, 233);
            btnimoortdata.ForeColor = System.Drawing.Color.White;
        }

        private void btnimoortdata_Leave(object sender, EventArgs e)
        {
            btnimoortdata.UseVisualStyleBackColor = true;
            btnimoortdata.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnimoortdata.ForeColor = System.Drawing.Color.White;
        }

        private void btnimoortdata_MouseEnter(object sender, EventArgs e)
        {
            btnimoortdata.UseVisualStyleBackColor = false;
            btnimoortdata.BackColor = System.Drawing.Color.FromArgb(118, 72, 233);
            btnimoortdata.ForeColor = System.Drawing.Color.White;
        }

        private void btnimoortdata_MouseLeave(object sender, EventArgs e)
        {
            btnimoortdata.UseVisualStyleBackColor = true;
            btnimoortdata.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnimoortdata.ForeColor = System.Drawing.Color.White;
        }






    }
}
