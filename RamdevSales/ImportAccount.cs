using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
using LoggingFramework;

namespace RamdevSales
{
    public partial class ImportAccount : Form
    {
        private Master master;
        private TabControl tabControl;
        string filepath, appPath;
        Connection sql = new Connection();
        DataTable dt = new DataTable();
        OpenFileDialog ofd = new OpenFileDialog();
        public ImportAccount()
        {
            InitializeComponent();
        }

        public ImportAccount(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
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
                        using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [Account$A1:S]", excel_con))//main table == productmaster
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

        private void btnimport_Click(object sender, EventArgs e)
        {
            LoadNewFile();
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
                    using (OleDbDataAdapter oda1 = new OleDbDataAdapter("SELECT * FROM [Account$" + txtform.Text + ":" + txtto.Text + "]", excel_con))//main table == productmaster
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
                            sqldt = sql.getdataset("Select * from ClientMaster where isactive=1 and AccountName='" + dt.Rows[i]["AccountName"].ToString() + "'");
                            DataTable companyid = sql.getdataset("select id from AccountGroup where groupname='" + dt.Rows[i]["GroupName"].ToString() + "'");
                            if (sqldt.Rows.Count > 0)
                            {
                                //update ClientMaster set AccountName ='" + txtaccname.Text +"',statecode='"+txtstatecode.Text+ "',GstNo='" + txtgst.Text + "',AdharNo='" + txtadhar.Text + "', PrintName='" + txtprintname.Text + "',GroupName='" + txtgrop.Text + "',Opbal='" + txtopbal.Text + "',Dr_cr='" + txtcrdr.Text + "',Address='" + txtaddress.Text + "',City='" + txtcity.Text + "',State='" + txtstate.Text + "',Phone='" + txtphone.Text + "',Mobile='" + txtmobile.Text + "',Email='" + txtemail.Text + "',Cstno='" + txtcst.Text + "',Vatno='" + txtvat.Text + "',groupid='" + txtgrop.SelectedValue + "',isactive=1,ismaintain=0 where clientID='" + id + "'
                                string str = "Update ClientMaster set AccountName='" + dt.Rows[i]["AccountName"].ToString() + "',statecode='" + dt.Rows[i]["statecode"].ToString() + "', GstNo='" + dt.Rows[i]["GstNo"].ToString() + "',AdharNo='" + dt.Rows[i]["AdharNo"].ToString() + "',PrintName='" + dt.Rows[i]["PrintName"].ToString() + "',GroupName='" + dt.Rows[i]["GroupName"].ToString() + "',Opbal='" + dt.Rows[i]["Opbal"].ToString() + "',Dr_cr='" + dt.Rows[i]["Dr_cr"].ToString() + "',Address='" + dt.Rows[i]["Address"].ToString() + "',City='" + dt.Rows[i]["City"].ToString() + "',State='" + dt.Rows[i]["State"].ToString() + "',Phone='" + dt.Rows[i]["Phone"].ToString() + "',Mobile='" + dt.Rows[i]["Mobile"].ToString() + "',Email='" + dt.Rows[i]["Email"].ToString() + "',Cstno='" + dt.Rows[i]["Cstno"].ToString() + "',Vatno='" + dt.Rows[i]["Vatno"].ToString() + "',GroupID='" + companyid.Rows[0]["id"].ToString() + "',ismaintain='" + dt.Rows[i]["ismaintain"].ToString() + "'Where AccountName='" + dt.Rows[i]["AccountName"].ToString() + "'";
                                sql.execute(str);
                                LogGenerator.Info(str);
                              //  string str1 = "Update ProductPriceMaster set Batchno='" + dt.Rows[i]["Batchno"].ToString() + "',BasicPrice='" + dt.Rows[i]["BasicPrice"].ToString() + "',SalePrice='" + dt.Rows[i]["SalePrice"].ToString() + "',MRP='" + dt.Rows[i]["MRP"].ToString() + "',PurchasePrice='" + dt.Rows[i]["PurchasePrice"].ToString() + "',Barcode='" + dt.Rows[i]["Barcode"].ToString() + "',OpStock='" + dt.Rows[i]["OpStock"].ToString() + "',ExpDt='" + dt.Rows[i]["ExpDt"].ToString() + "',mfgdt='" + dt.Rows[i]["mfgdt"].ToString() + "',Expdays='" + dt.Rows[i]["Expdays"].ToString() + "',SelfVal='" + dt.Rows[i]["SelfVal"].ToString() + "',minsaleprice='" + dt.Rows[i]["minsaleprice"].ToString() + "',oploose='" + dt.Rows[i]["oploose"].ToString() + "',opstockval='" + dt.Rows[i]["opstockval"].ToString() + "'where ProductID='" + sqldt.Rows[0]["ProductID"].ToString() + "'";
                                //sql.execute(str1);
                                //LogGenerator.Info(str1);
                                // string str2 = "Update TaxSlabMaster set saletypeid='" + dt.Rows[i]["saletypeid"].ToString() + "',system='" + dt.Rows[i]["system"].ToString() + "',category='" + dt.Rows[i]["category"].ToString() + "',sgst='" + dt.Rows[i]["sgst"].ToString() + "',cgst='" + dt.Rows[i]["cgst"].ToString() + "',igst='" + dt.Rows[i]["igst"].ToString() + "',additax='" + dt.Rows[i]["additax"].ToString() + "',onwhich='" + dt.Rows[i]["onwhich"].ToString() + "',isonmrp='" + dt.Rows[i]["isonmrp"].ToString() + "',isonfreegoods='" + dt.Rows[i]["isonfreegoods"].ToString() + "' where Taxslabname='" + sqldt.Rows[0]["taxslab"].ToString() + "'";
                                //sql.execute(str2);
                                //LogGenerator.Info(str2);
                            }
                            else
                            {
                                string str3 = "INSERT INTO [dbo].[ClientMaster]([AccountName],[PrintName],[GroupName],[Opbal],[Dr_cr],[Address],[City],[State],[Phone],[Mobile],[Email],[Cstno],[Vatno],[GstNo],[AdharNo],[GroupID],[statecode],[ismaintain],[isactive])VALUES('" + dt.Rows[i]["AccountName"].ToString() + "','" + dt.Rows[i]["PrintName"].ToString() + "','" + dt.Rows[i]["GroupName"].ToString() + "','" + dt.Rows[i]["Opbal"].ToString() + "','" + dt.Rows[i]["Dr_cr"].ToString() + "','" + dt.Rows[i]["Address"].ToString() + "','" + dt.Rows[i]["City"].ToString() + "','" + dt.Rows[i]["State"].ToString() + "','" + dt.Rows[i]["Phone"].ToString() + "','" + dt.Rows[i]["Mobile"].ToString() + "','" + dt.Rows[i]["Email"].ToString() + "','" + dt.Rows[i]["Cstno"].ToString() + "','" + dt.Rows[i]["Vatno"].ToString() + "','" + dt.Rows[i]["GstNo"].ToString() + "','" + dt.Rows[i]["AdharNo"].ToString() + "','" + companyid.Rows[0]["id"].ToString() + "','" + dt.Rows[i]["statecode"].ToString() + "','" + dt.Rows[i]["ismaintain"].ToString() + "',1)";
                               // string str3 = "INSERT INTO [dbo].[ProductMaster]([Product_Name],[CompanyID],[GroupName],[Unit],[Altunit],[Convfactor],[Packing],[IsBatch],[Hsn_Sac_Code],[isserial],[cessper],[cessamt],[taxslab],[isactive]) VALUES ('" + dt.Rows[i]["Product_Name"].ToString() + "','" + companyid.Rows[0]["CompanyID"].ToString() + "','" + dt.Rows[i]["GroupName"].ToString() + "','" + dt.Rows[i]["Unit"].ToString() + "','" + dt.Rows[i]["AltUnit"].ToString() + "','" + dt.Rows[i]["Convfactor"].ToString() + "','" + dt.Rows[i]["Packing"].ToString() + "','" + dt.Rows[i]["IsBatch"].ToString() + "','" + dt.Rows[i]["Hsn_Sac_Code"].ToString() + "','" + dt.Rows[i]["isserial"].ToString() + "','" + dt.Rows[i]["cessper"].ToString() + "','" + dt.Rows[i]["cessamt"].ToString() + "','" + dt.Rows[i]["taxslab"].ToString() + "','1')";
                                sql.execute(str3);
                                LogGenerator.Info(str3);
                            //    DataTable proid = sql.getdataset("select * from ProductMaster where Product_Name='" + dt.Rows[i]["Product_Name"].ToString() + "'");
                              //  string str4 = "INSERT INTO [dbo].[ProductPriceMaster]([Productid],[Batchno],[BasicPrice],[SalePrice],[MRP],[PurchasePrice],[Barcode],[OpStock],[ExpDt],[mfgdt],[Expdays],[SelfVal],[minsaleprice],[oploose],[opstockval],[isactive])VALUES('" + proid.Rows[0]["ProductID"].ToString() + "','" + dt.Rows[i]["Batchno"].ToString() + "','" + dt.Rows[i]["BasicPrice"].ToString() + "','" + dt.Rows[i]["SalePrice"].ToString() + "','" + dt.Rows[i]["MRP"].ToString() + "','" + dt.Rows[i]["PurchasePrice"].ToString() + "','" + dt.Rows[i]["Barcode"].ToString() + "','" + dt.Rows[i]["OpStock"].ToString() + "','" + dt.Rows[i]["ExpDt"].ToString() + "','" + dt.Rows[i]["mfgdt"].ToString() + "','" + dt.Rows[i]["Expdays"].ToString() + "','" + dt.Rows[i]["SelfVal"].ToString() + "','" + dt.Rows[i]["minsaleprice"].ToString() + "','" + dt.Rows[i]["oploose"].ToString() + "','" + dt.Rows[i]["opstockval"].ToString() + "','1')";
                                //sql.execute(str4);
                                //LogGenerator.Info(str4);
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
                }
            }
            catch
            {
            }
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
