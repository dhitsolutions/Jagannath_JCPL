using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Web;

namespace RamdevSales
{
    public partial class Purchase_Order_Master_Report : Form
    {
        OleDbSettings ods = new OleDbSettings();
        public DataSet ds;
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable fordt = new DataTable();
        DataTable fordt1 = new DataTable();
        DataTable dtall = new DataTable();
        DataTable main = new DataTable();
        DataTable main1 = new DataTable();
        DataTable main2 = new DataTable();
        DataTable main3 = new DataTable();
        DataTable main4 = new DataTable();
        DataTable main5 = new DataTable();
        DataTable main6 = new DataTable();

        DataTable dtbind = new DataTable();

        DataTable dtcn = new DataTable();
        DataTable dtcl = new DataTable();
        public SqlConnection con;
        Connection cl = new Connection();
        ServerConnection conn = new ServerConnection();
        int id;
        int vchno;
        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();

        public Purchase_Order_Master_Report()
        {
            InitializeComponent();
        }

        public void bindgrid()
        {

            dtcl = cl.getdataset("select * from ClientMaster where GroupName='CUSTOMERS' and isactive=1");
            if (dtcl.Rows.Count > 0)
            {
                for (int i = 0; i < dtcl.Rows.Count; i++)
                {
                    checkedListBox1.Items.Add(dtcl.Rows[i]["AccountName"].ToString());
                }

            }
            DataTable
            dtdepartment = cl.getdataset("select * from DepartmentMaster");
            if (dtdepartment.Rows.Count > 0)
            {
                for (int i = 0; i < dtdepartment.Rows.Count; i++)
                {
                    checkedListBox2.Items.Add(dtdepartment.Rows[i]["DepartmentName"].ToString());
                }
            }

        }

        private void Purchase_Order_Master_Report_Load(object sender, EventArgs e)
        {
            try
            {
                bindgrid();
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(0, 0);
                DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                checkBoxColumn.HeaderText = "Select";
                checkBoxColumn.Width = 50;
                checkBoxColumn.Name = "Add";
                gridp.Columns.Insert(0, checkBoxColumn);
                gridp.Visible = false;

            }
            catch
            {
            }
        }
        private void PurchaseOrderReport()
        {
            String path = Application.StartupPath;
            System.IO.Directory.CreateDirectory(path + @"\report");
            Document document = new Document();
            String DateTimeName = DateTime.Now.ToString("dd_MMM_yyyy hh_mm_ss");
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(path + "\\report\\PurchaseOrderReport-" + DateTimeName + ".pdf", FileMode.Create));
            iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 8);
            document.Open();
            foreach (object itemChecked in checkedListBox2.CheckedItems)
            {
                DataTable main = new DataTable();
                main.Columns.Add("DESCRIPTION", typeof(string));
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    main.Columns.Add("" + dt1.Rows[i]["AccountName"].ToString(), typeof(string));
                }
                main.Columns.Add("Total", typeof(string));
                Double[] endtotal = new Double[main.Columns.Count];
                DataTable dtdept = new DataTable();
                //string str = "select * from DepartmentMaster where ";
                //str += "Departmentname like '%" + itemChecked.ToString() + "%'";
                dtdept = cl.getdataset("select * from DepartmentMaster where Departmentname like '%" + itemChecked.ToString() + "%'");
                label2.Text = "Department:" + itemChecked.ToString();
                main.Rows.Add(label2.Text);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Double sum = 0;
                    DataRow dr = main.NewRow();
                    dr["DESCRIPTION"] = dt.Rows[i]["productname"].ToString();
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        string qty = cl.ExecuteScalar("select po.qty from purchaseorderproductmaster po inner join productmaster p on p.Product_Name=po.ProductName where po.isactive=1 and p.DepartmentId='" + dtdept.Rows[0]["DepartmentId"].ToString() + "' and po.productname='" + dt.Rows[i]["productname"].ToString() + "'and po.CompanyID='" + dt1.Rows[j]["clientid"].ToString() + "'");
                        if (qty == "")
                        {
                            qty = "0";
                        }
                        dr["" + dt1.Rows[j]["AccountName"].ToString()] = qty;
                        sum += Convert.ToDouble(qty);
                        endtotal[j + 1] += Convert.ToDouble(qty);
                    }

                    dr["Total"] = sum;
                    if (sum > 0)
                    {
                        main.Rows.Add(dr.ItemArray);
                    }

                }
                double total = 0;
                DataRow lastdr = main.NewRow();
                for (int i = 0; i < endtotal.Length; i++)
                {
                    if (i == 0)
                    {
                        lastdr[i] = "Total";
                    }
                    else
                    {
                        lastdr[i] = endtotal[i];
                        total += Convert.ToDouble(endtotal[i]);
                    }
                }
                lastdr["Total"] = Math.Round(total, 2).ToString();
                main.Rows.Add(lastdr.ItemArray);
                main.Rows.Add();
                PdfPTable bltable = new PdfPTable(main.Columns.Count);
                float[] blwidths = new float[] { 4f, 4f, 4f, 4f };
                bltable.WidthPercentage = 100;

                PdfPCell blpcell = new PdfPCell(new Phrase(""));
                blpcell.BorderColor = iTextSharp.text.Color.WHITE;
                blpcell.FixedHeight = 10f;

                for (int i = 0; i < main.Columns.Count; i++)
                {
                    bltable.AddCell(blpcell);
                }
                #region
                PdfPTable table = new PdfPTable(main.Columns.Count);
                float[] widths = new float[] { 4f, 4f, 4f, 4f };
                table.WidthPercentage = 100;
                PdfPCell cell = new PdfPCell(new Phrase("Products"));

                cell.Colspan = main.Columns.Count;

                foreach (DataColumn c in main.Columns)
                {
                    table.AddCell(new Phrase(c.ColumnName, font5));
                }

                for (int i = 0; i < main.Rows.Count; i++)
                {
                    for (int j = 0; j < main.Columns.Count; j++)
                    {
                        table.AddCell(new Phrase(main.Rows[i][j].ToString(), font5));
                    }
                }



                document.Add(table);
                document.Add(bltable);


                #endregion
            }

            document.Close();
            #region
            //String sReport =path+ "\\report\\PurchaseOrderReport-" + DateTimeName + ".pdf"; //'Complete name/path of PDF file
            //PdfAction action = new PdfAction(PdfAction.PRINTDIALOG);
            //writer.SetOpenAction(action);

            //Print(sReport);
            System.Diagnostics.Process.Start(path + "\\report\\PurchaseOrderReport-" + DateTimeName + ".pdf");
            String pathToExecutable = "AcroRd32.exe";
            #endregion

        }
        public void Print(string sreport)
        {
            //PrintDialog printDialog1 = new PrintDialog();
            //printDialog1.PrinterSettings.PrinterName = "EasyCoder 91 DT (203 dpi)";
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.Refresh();
            //process.StartInfo.Arguments = "EasyCoder 91 DT (203 dpi)";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.Verb = "print";
            process.StartInfo.FileName = @sreport;
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            process.Start();
        }
        private void PurchaseOrderItemReport()
        {
            String path = Application.StartupPath;
            System.IO.Directory.CreateDirectory(path + @"\report");
            Document document = new Document();
            String DateTimeName = DateTime.Now.ToString("dd_MMM_yyyy hh_mm_ss");
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(path + "\\report\\PurchaseOrderItemWiseReport-" + DateTimeName + ".pdf", FileMode.Create));
            iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 8);
            document.Open();
            foreach (object itemChecked in checkedListBox2.CheckedItems)
            {
                DataTable main = new DataTable();
                main.Columns.Add("Client Name", typeof(string));

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    main.Columns.Add(dt.Rows[i]["productname"].ToString());
                }
                main.Columns.Add("Total", typeof(string));
                Double[] endtotal = new Double[main.Columns.Count];
                DataTable dtdept = new DataTable();
                //string str = "select * from DepartmentMaster where ";
                //str += "Departmentname like '%" + itemChecked.ToString() + "%'";
                dtdept = cl.getdataset("select * from DepartmentMaster where Departmentname like '%" + itemChecked.ToString() + "%'");
                label2.Text = "Department:" + itemChecked.ToString();
                main.Rows.Add(label2.Text);
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    Double sum = 0;
                    DataRow dr = main.NewRow();
                    dr["Client Name"] = dt1.Rows[j]["AccountName"].ToString();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string qty = cl.ExecuteScalar("select po.qty from purchaseorderproductmaster po inner join productmaster p on p.Product_Name = po.ProductName where po.isactive=1 and p.DepartmentId='" + dtdept.Rows[0]["DepartmentId"].ToString() + "' and po.productname='" + dt.Rows[i]["productname"].ToString() + "'and po.CompanyID='" + dt1.Rows[j]["clientid"].ToString() + "'");
                        // string qty = conn.ExecuteScalar("select qty from purchaseorderproductmaster where isactive=1 and  productname='" + dt.Rows[i]["productname"].ToString() + "' and Vchno='" + dt1.Rows[j]["Vchno"].ToString() + "'");
                        if (qty == "")
                        {
                            qty = "0";
                        }
                        dr[dt.Rows[i]["ProductName"].ToString()] = qty;
                        sum += Convert.ToDouble(qty);
                        endtotal[i + 1] += Convert.ToDouble(qty);
                    }
                    dr["Total"] = sum;
                    if (sum > 0)
                    {
                        main.Rows.Add(dr.ItemArray);
                    }
                }


                double total = 0;
                DataRow lastdr = main.NewRow();
                for (int i = 0; i < endtotal.Length; i++)
                {
                    if (i == 0)
                    {
                        lastdr[i] = "Total";
                    }
                    else
                    {
                        lastdr[i] = endtotal[i];
                        total += Convert.ToDouble(endtotal[i]);
                    }
                }
                lastdr["Total"] = Math.Round(total, 2).ToString();
                main.Rows.Add(lastdr.ItemArray);
                main.Rows.Add();

                PdfPTable bltable = new PdfPTable(main.Columns.Count);
                float[] blwidths = new float[] { 4f, 4f, 4f, 4f };
                bltable.WidthPercentage = 100;

                PdfPCell blpcell = new PdfPCell(new Phrase(""));
                blpcell.BorderColor = iTextSharp.text.Color.WHITE;
                blpcell.FixedHeight = 10f;

                for (int i = 0; i < main.Columns.Count; i++)
                {
                    bltable.AddCell(blpcell);
                }
                #region


                PdfPTable table = new PdfPTable(main.Columns.Count);
                float[] widths = new float[] { 4f, 4f, 4f, 4f };
                table.WidthPercentage = 100;
                PdfPCell cell = new PdfPCell(new Phrase("Products"));

                cell.Colspan = main.Columns.Count;

                foreach (DataColumn c in main.Columns)
                {
                    table.AddCell(new Phrase(c.ColumnName, font5));
                }

                for (int i = 0; i < main.Rows.Count; i++)
                {
                    for (int j = 0; j < main.Columns.Count; j++)
                    {
                        table.AddCell(new Phrase(main.Rows[i][j].ToString(), font5));
                    }
                }



                document.Add(table);
                document.Add(bltable);


                #endregion

            }
            document.Close();
            #region
            //String sReport = path + "\\report\\PurchaseOrderItemWiseReport-" + DateTimeName + ".pdf"; //'Complete name/path of PDF file
            //PdfAction action = new PdfAction(PdfAction.PRINTDIALOG);
            //writer.SetOpenAction(action);

            //Print(sReport);
            System.Diagnostics.Process.Start(path + "\\report\\PurchaseOrderItemWiseReport-" + DateTimeName + ".pdf");
            String pathToExecutable = "AcroRd32.exe";

            #endregion
        }
        private void Bind()
        {
            #region

            main.Columns.Add("DESCRIPTION", typeof(string));

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                main.Columns.Add("" + dt1.Rows[i]["AccountName"].ToString(), typeof(string));
            }
            main.Columns.Add("Total", typeof(string));
            Double[] endtotal = new Double[main.Columns.Count];
            DataTable dtdept = new DataTable();
            dtdept = cl.getdataset("select * from DepartmentMaster");
            #region
            label2.Text = "Department:" + dtdept.Rows[0][1].ToString();

            // main.Rows.Add();
            main.Rows.Add(label2.Text);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Double sum = 0;
                DataRow dr = main.NewRow();
                dr["DESCRIPTION"] = dt.Rows[i]["productname"].ToString();
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    string qty = cl.ExecuteScalar("select po.qty from purchaseorderproductmaster po inner join productmaster p on p.Product_Name=po.ProductName where po.isactive=1 and p.DepartmentId='" + dtdept.Rows[0][0].ToString() + "' and po.productname='" + dt.Rows[i]["productname"].ToString() + "'");
                    if (qty == "")
                    {
                        qty = "0";
                    }
                    dr["" + dt1.Rows[j]["AccountName"].ToString()] = qty;
                    sum += Convert.ToDouble(qty);
                    endtotal[j + 1] += Convert.ToDouble(qty);
                }

                dr["Total"] = sum;
                if (sum > 0)
                {
                    main.Rows.Add(dr.ItemArray);
                }
            }

            double total = 0;
            DataRow lastdr = main.NewRow();
            for (int i = 0; i < endtotal.Length; i++)
            {
                if (i == 0)
                {
                    lastdr[i] = "Total";
                }
                else
                {
                    lastdr[i] = endtotal[i];
                    total += Convert.ToDouble(endtotal[i]);
                }
            }
            lastdr["Total"] = Math.Round(total, 2).ToString();
            main.Rows.Add(lastdr.ItemArray);
            main.Rows.Add();
            #endregion
            #endregion
            PdfPTable bltable = new PdfPTable(main.Columns.Count);
            float[] blwidths = new float[] { 4f, 4f, 4f, 4f };
            bltable.WidthPercentage = 100;

            PdfPCell blpcell = new PdfPCell(new Phrase(""));
            blpcell.BorderColor = iTextSharp.text.Color.WHITE;
            blpcell.FixedHeight = 10f;

            for (int i = 0; i < main.Columns.Count; i++)
            {
                bltable.AddCell(blpcell);
            }


            #region

            main2.Columns.Add("DESCRIPTION", typeof(string));

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                main2.Columns.Add("" + dt1.Rows[i]["AccountName"].ToString(), typeof(string));
            }
            main2.Columns.Add("Total", typeof(string));
            Double[] endtotal1 = new Double[main2.Columns.Count];
            label2.Text = "Department:" + dtdept.Rows[1][1].ToString();
            //  main2.Rows.Add();
            main2.Rows.Add(label2.Text);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Double sum = 0;
                DataRow dr = main2.NewRow();
                dr["DESCRIPTION"] = dt.Rows[i]["productname"].ToString();
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    string qty = cl.ExecuteScalar("select po.qty from purchaseorderproductmaster po inner join productmaster p on p.Product_Name=po.ProductName where po.isactive=1 and p.DepartmentId='" + dtdept.Rows[1][0].ToString() + "' and po.productname='" + dt.Rows[i]["productname"].ToString() + "'");
                    if (qty == "")
                    {
                        qty = "0";
                    }
                    dr["" + dt1.Rows[j]["AccountName"].ToString()] = qty;
                    sum += Convert.ToDouble(qty);
                    endtotal1[j + 1] += Convert.ToDouble(qty);
                }
                dr["Total"] = sum;
                if (sum > 0)
                {

                    main2.Rows.Add(dr.ItemArray);
                }
            }


            double total1 = 0;
            DataRow lastdr1 = main2.NewRow();
            for (int i = 0; i < endtotal1.Length; i++)
            {
                if (i == 0)
                {
                    lastdr1[i] = "Total";
                }
                else
                {
                    lastdr1[i] = endtotal1[i];
                    total1 += Convert.ToDouble(endtotal1[i]);
                }
            }
            lastdr1["Total"] = Math.Round(total1, 2).ToString();
            main2.Rows.Add(lastdr1.ItemArray);
            main2.Rows.Add();
            #endregion

            PdfPTable bltable1 = new PdfPTable(main2.Columns.Count);
            float[] blwidths1 = new float[] { 4f, 4f, 4f, 4f };
            bltable1.WidthPercentage = 100;

            PdfPCell blpcell1 = new PdfPCell(new Phrase(""));
            blpcell1.BorderColor = iTextSharp.text.Color.WHITE;
            blpcell1.FixedHeight = 10f;

            for (int i = 0; i < main2.Columns.Count; i++)
            {
                bltable1.AddCell(blpcell1);
            }



            #region

            main3.Columns.Add("DESCRIPTION", typeof(string));

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                main3.Columns.Add("" + dt1.Rows[i]["AccountName"].ToString(), typeof(string));
            }
            main3.Columns.Add("Total", typeof(string));
            Double[] endtotal2 = new Double[main3.Columns.Count];
            label2.Text = "Department:" + dtdept.Rows[2][1].ToString();
            // main3.Rows.Add();
            main3.Rows.Add(label2.Text);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Double sum = 0;
                DataRow dr = main3.NewRow();
                dr["DESCRIPTION"] = dt.Rows[i]["productname"].ToString();
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    string qty = cl.ExecuteScalar("select po.qty from purchaseorderproductmaster po inner join productmaster p on p.Product_Name=po.ProductName where po.isactive=1 and p.DepartmentId='" + dtdept.Rows[2][0].ToString() + "' and po.productname='" + dt.Rows[i]["productname"].ToString() + "'");
                    if (qty == "")
                    {
                        qty = "0";
                    }
                    dr["" + dt1.Rows[j]["AccountName"].ToString()] = qty;
                    sum += Convert.ToDouble(qty);
                    endtotal2[j + 1] += Convert.ToDouble(qty);
                }
                dr["Total"] = sum;
                if (sum > 0)
                {

                    main3.Rows.Add(dr.ItemArray);
                }
            }

            double total2 = 0;
            DataRow lastdr2 = main3.NewRow();
            for (int i = 0; i < endtotal2.Length; i++)
            {
                if (i == 0)
                {
                    lastdr2[i] = "Total";
                }
                else
                {
                    lastdr2[i] = endtotal2[i];
                    total2 += Convert.ToDouble(endtotal2[i]);
                }
            }
            lastdr2["Total"] = Math.Round(total2, 2).ToString();
            main3.Rows.Add(lastdr2.ItemArray);
            main3.Rows.Add();
            #endregion
            PdfPTable bltable2 = new PdfPTable(main3.Columns.Count);
            float[] blwidths2 = new float[] { 4f, 4f, 4f, 4f };
            bltable2.WidthPercentage = 100;

            PdfPCell blpcell2 = new PdfPCell(new Phrase(""));
            blpcell2.BorderColor = iTextSharp.text.Color.WHITE;
            blpcell2.FixedHeight = 10f;

            for (int i = 0; i < main3.Columns.Count; i++)
            {
                bltable2.AddCell(blpcell2);
            }
            #region
            String path = Application.StartupPath;
            System.IO.Directory.CreateDirectory(path + @"\report");
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(path + "\\report\\PurchaseOrderReport-" + DateTime.Now.ToString("dd_MMM_yyyy hh_mm_ss") + ".pdf", FileMode.Create));
            iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 8);
            document.Open();

            PdfPTable table = new PdfPTable(main.Columns.Count);
            float[] widths = new float[] { 4f, 4f, 4f, 4f };
            table.WidthPercentage = 100;
            PdfPCell cell = new PdfPCell(new Phrase("Products"));

            cell.Colspan = main.Columns.Count;

            foreach (DataColumn c in main.Columns)
            {
                table.AddCell(new Phrase(c.ColumnName, font5));
            }

            for (int i = 0; i < main.Rows.Count; i++)
            {
                for (int j = 0; j < main.Columns.Count; j++)
                {
                    table.AddCell(new Phrase(main.Rows[i][j].ToString(), font5));
                }
            }

            PdfPTable table1 = new PdfPTable(main2.Columns.Count);
            float[] widths1 = new float[] { 4f, 4f, 4f, 4f };

            table1.WidthPercentage = 100;
            PdfPCell cell1 = new PdfPCell(new Phrase("Products"));

            cell1.Colspan = main2.Columns.Count;

            foreach (DataColumn c in main2.Columns)
            {

                table1.AddCell(new Phrase(c.ColumnName, font5));
            }

            for (int i = 0; i < main2.Rows.Count; i++)
            {
                for (int j = 0; j < main2.Columns.Count; j++)
                {
                    table1.AddCell(new Phrase(main2.Rows[i][j].ToString(), font5));
                }
            }

            PdfPTable table2 = new PdfPTable(main2.Columns.Count);
            float[] widths2 = new float[] { 4f, 4f, 4f, 4f };

            table2.WidthPercentage = 100;
            PdfPCell cell2 = new PdfPCell(new Phrase("Products"));

            cell2.Colspan = main3.Columns.Count;

            foreach (DataColumn c in main3.Columns)
            {

                table2.AddCell(new Phrase(c.ColumnName, font5));
            }

            for (int i = 0; i < main3.Rows.Count; i++)
            {
                for (int j = 0; j < main3.Columns.Count; j++)
                {
                    table2.AddCell(new Phrase(main3.Rows[i][j].ToString(), font5));
                }
            }

            document.Add(table);
            document.Add(bltable);
            document.Add(table1);
            document.Add(bltable1);
            document.Add(table2);
            document.Add(bltable2);
            document.Close();

            #endregion
        }

        private void Bind1()
        {
            #region
            main1.Columns.Add("Client Name", typeof(string));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                main1.Columns.Add(dt.Rows[i]["productname"].ToString());
            }
            main1.Columns.Add("Total", typeof(string));
            Double[] endtotal = new Double[main1.Columns.Count];

            DataTable dtdept = new DataTable();
            dtdept = cl.getdataset("select * from DepartmentMaster");

            label3.Text = "Department:" + dtdept.Rows[0][1].ToString();
            //  main1.Rows.Add();
            main1.Rows.Add(label3.Text);

            for (int j = 0; j < dt1.Rows.Count; j++)
            {
                Double sum = 0;
                DataRow dr = main1.NewRow();
                dr["Client Name"] = dt1.Rows[j]["AccountName"].ToString();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string qty = cl.ExecuteScalar("select po.qty from purchaseorderproductmaster po inner join productmaster p on p.Product_Name = po.ProductName where po.isactive=1 and p.DepartmentId='" + dtdept.Rows[0][0].ToString() + "' and po.productname='" + dt.Rows[i]["productname"].ToString() + "'");
                    // string qty = conn.ExecuteScalar("select qty from purchaseorderproductmaster where isactive=1 and  productname='" + dt.Rows[i]["productname"].ToString() + "' and Vchno='" + dt1.Rows[j]["Vchno"].ToString() + "'");
                    if (qty == "")
                    {
                        qty = "0";
                    }
                    dr[dt.Rows[i]["ProductName"].ToString()] = qty;
                    sum += Convert.ToDouble(qty);
                    endtotal[i + 1] += Convert.ToDouble(qty);
                }
                dr["Total"] = sum;
                if (sum > 0)
                {
                    main1.Rows.Add(dr.ItemArray);
                }
            }


            double total = 0;
            DataRow lastdr = main1.NewRow();
            for (int i = 0; i < endtotal.Length; i++)
            {
                if (i == 0)
                {
                    lastdr[i] = "Total";
                }
                else
                {
                    lastdr[i] = endtotal[i];
                    total += Convert.ToDouble(endtotal[i]);
                }
            }
            lastdr["Total"] = Math.Round(total, 2).ToString();
            main1.Rows.Add(lastdr.ItemArray);
            main1.Rows.Add();
            #endregion
            PdfPTable bltable = new PdfPTable(main1.Columns.Count);
            float[] blwidths = new float[] { 4f, 4f, 4f, 4f };
            bltable.WidthPercentage = 100;

            PdfPCell blpcell = new PdfPCell(new Phrase(""));
            blpcell.BorderColor = iTextSharp.text.Color.WHITE;
            blpcell.FixedHeight = 10f;

            for (int i = 0; i < main1.Columns.Count; i++)
            {
                bltable.AddCell(blpcell);
            }
            #region
            main4.Columns.Add("Client Name", typeof(string));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                main4.Columns.Add(dt.Rows[i]["productname"].ToString());
            }
            main4.Columns.Add("Total", typeof(string));
            Double[] endtotal4 = new Double[main4.Columns.Count];

            label3.Text = "Department:" + dtdept.Rows[1][1].ToString();
            //main4.Rows.Add();
            main4.Rows.Add(label3.Text);

            for (int j = 0; j < dt1.Rows.Count; j++)
            {
                Double sum = 0;
                DataRow dr = main4.NewRow();
                dr["Client Name"] = dt1.Rows[j]["AccountName"].ToString();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string qty = cl.ExecuteScalar("select po.qty from purchaseorderproductmaster po inner join productmaster p on p.Product_Name = po.ProductName where po.isactive=1 and p.DepartmentId='" + dtdept.Rows[1][0].ToString() + "' and po.productname='" + dt.Rows[i]["productname"].ToString() + "'");

                    if (qty == "")
                    {
                        qty = "0";
                    }
                    dr[dt.Rows[i]["ProductName"].ToString()] = qty;
                    sum += Convert.ToDouble(qty);
                    endtotal4[i + 1] += Convert.ToDouble(qty);
                }
                dr["Total"] = sum;
                if (sum > 0)
                {
                    main4.Rows.Add(dr.ItemArray);
                }
            }

            double total4 = 0;
            DataRow lastdr4 = main4.NewRow();
            for (int i = 0; i < endtotal4.Length; i++)
            {
                if (i == 0)
                {
                    lastdr4[i] = "Total";
                }
                else
                {
                    lastdr4[i] = endtotal4[i];
                    total4 += Convert.ToDouble(endtotal4[i]);
                }
            }
            lastdr4["Total"] = Math.Round(total4, 2).ToString();
            main4.Rows.Add(lastdr4.ItemArray);
            main4.Rows.Add();
            #endregion
            PdfPTable bltable1 = new PdfPTable(main4.Columns.Count);
            float[] blwidths1 = new float[] { 4f, 4f, 4f, 4f };
            bltable1.WidthPercentage = 100;

            PdfPCell blpcell1 = new PdfPCell(new Phrase(""));
            blpcell1.BorderColor = iTextSharp.text.Color.WHITE;
            blpcell1.FixedHeight = 10f;

            for (int i = 0; i < main4.Columns.Count; i++)
            {
                bltable1.AddCell(blpcell1);
            }
            #region
            main5.Columns.Add("Client Name", typeof(string));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                main5.Columns.Add(dt.Rows[i]["productname"].ToString());
            }
            main5.Columns.Add("Total", typeof(string));
            Double[] endtotal5 = new Double[main5.Columns.Count];

            label3.Text = "Department:" + dtdept.Rows[2][1].ToString();
            //  main5.Rows.Add();
            main5.Rows.Add(label3.Text);

            for (int j = 0; j < dt1.Rows.Count; j++)
            {
                Double sum = 0;
                DataRow dr = main5.NewRow();
                dr["Client Name"] = dt1.Rows[j]["AccountName"].ToString();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string qty = cl.ExecuteScalar("select po.qty from purchaseorderproductmaster po inner join productmaster p on p.Product_Name = po.ProductName where po.isactive=1 and p.DepartmentId='" + dtdept.Rows[2][0].ToString() + "' and po.productname='" + dt.Rows[i]["productname"].ToString() + "'");

                    if (qty == "")
                    {
                        qty = "0";
                    }
                    dr[dt.Rows[i]["ProductName"].ToString()] = qty;
                    sum += Convert.ToDouble(qty);
                    endtotal5[i + 1] += Convert.ToDouble(qty);
                }
                dr["Total"] = sum;
                if (sum > 0)
                {
                    main5.Rows.Add(dr.ItemArray);
                }
            }

            double total5 = 0;
            DataRow lastdr5 = main5.NewRow();
            for (int i = 0; i < endtotal5.Length; i++)
            {
                if (i == 0)
                {
                    lastdr5[i] = "Total";
                }
                else
                {
                    lastdr5[i] = endtotal5[i];
                    total5 += Convert.ToDouble(endtotal5[i]);
                }
            }
            lastdr5["Total"] = Math.Round(total5, 2).ToString();
            main5.Rows.Add(lastdr5.ItemArray);
            main5.Rows.Add();
            #endregion
            PdfPTable bltable2 = new PdfPTable(main5.Columns.Count);
            float[] blwidths2 = new float[] { 4f, 4f, 4f, 4f };
            bltable2.WidthPercentage = 100;

            PdfPCell blpcell2 = new PdfPCell(new Phrase(""));
            blpcell2.BorderColor = iTextSharp.text.Color.WHITE;
            blpcell2.FixedHeight = 10f;

            for (int i = 0; i < main5.Columns.Count; i++)
            {
                bltable2.AddCell(blpcell2);
            }
            #region
            String path = Application.StartupPath;
            System.IO.Directory.CreateDirectory(path + @"\report");
            Document document = new Document();

            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(path + "\\report\\PurchaseOrderReportItem-" + DateTime.Now.ToString("dd_MMM_yyyy hh_mm_ss") + ".pdf", FileMode.Create));
            iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 8);
            document.Open();

            PdfPTable table = new PdfPTable(main1.Columns.Count);

            float[] widths = new float[] { 4f, 4f, 4f, 4f };

            table.WidthPercentage = 100;

            PdfPCell cell = new PdfPCell(new Phrase("Products"));

            cell.Colspan = main1.Columns.Count;

            foreach (DataColumn c in main1.Columns)
            {

                table.AddCell(new Phrase(c.ColumnName, font5));
            }

            for (int i = 0; i < main1.Rows.Count; i++)
            {
                for (int j = 0; j < main1.Columns.Count; j++)
                {
                    table.AddCell(new Phrase(main1.Rows[i][j].ToString(), font5));
                }
            }

            PdfPTable table4 = new PdfPTable(main4.Columns.Count);

            float[] widths4 = new float[] { 4f, 4f, 4f, 4f };

            table4.WidthPercentage = 100;

            PdfPCell cell4 = new PdfPCell(new Phrase("Products"));

            cell4.Colspan = main4.Columns.Count;

            foreach (DataColumn c in main4.Columns)
            {

                table4.AddCell(new Phrase(c.ColumnName, font5));
            }

            for (int i = 0; i < main4.Rows.Count; i++)
            {
                for (int j = 0; j < main4.Columns.Count; j++)
                {
                    table4.AddCell(new Phrase(main4.Rows[i][j].ToString(), font5));
                }
            }


            PdfPTable table5 = new PdfPTable(main5.Columns.Count);

            float[] widths5 = new float[] { 4f, 4f, 4f, 4f };

            table5.WidthPercentage = 100;

            PdfPCell cell5 = new PdfPCell(new Phrase("Products"));

            cell5.Colspan = main5.Columns.Count;

            foreach (DataColumn c in main5.Columns)
            {

                table5.AddCell(new Phrase(c.ColumnName, font5));
            }

            for (int i = 0; i < main5.Rows.Count; i++)
            {
                for (int j = 0; j < main5.Columns.Count; j++)
                {
                    table5.AddCell(new Phrase(main5.Rows[i][j].ToString(), font5));
                }
            }

            document.Add(table);
            document.Add(bltable);
            document.Add(table4);
            document.Add(bltable1);
            document.Add(table5);
            document.Add(bltable2);
            document.Close();
            #endregion
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                btnsave.Enabled = false;
                string[] chkvchid = new string[gridp.Rows.Count];
                string[] chkvchno = new string[gridp.Rows.Count];
                for (int i = 0; i < gridp.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(gridp.Rows[i].Cells[0].Value) == true)
                    {
                        id = Convert.ToInt32(gridp.Rows[i].Cells["companyID"].Value);
                        vchno = Convert.ToInt32(gridp.Rows[i].Cells["vchno"].Value);
                        chkvchid[i] = id.ToString();
                        chkvchno[i] = vchno.ToString();
                        cl.execute("UPDATE [dbo].[PurchaseOrderMaster] SET [isGenerated]=1,[syncDatetime]='" + DateTime.Now + "',[GeneDate]='" + DateTime.Now + "' where [companyID]='" + id + "'");
                        //DataTable dt = new DataTable();
                        //dt = cl.getdataset("Select CompanyId from PurchaseOrderMaster where VchNo='" + id + "'");
                    }
                }
                if (chkvchid != null && chkvchno != null)
                {
                    for (int i = 0; i < chkvchid.Count(); i++)
                    {
                        fordt = cl.getdataset("select distinct productname from purchaseorderproductmaster where isactive=1 and VchNo='" + chkvchno[i] + "' and companyID='" + chkvchid[i] + "' order by productname");
                        dtall.Merge(fordt, true);
                        dt = dtall.DefaultView.ToTable(true, "productname");
                        fordt1 = cl.getdataset("select distinct companyID from purchaseorderproductmaster where isactive=1 and VchNo='" + chkvchno[i] + "' and companyID='" + chkvchid[i] + "'");
                        DataTable party = new DataTable();
                        party = cl.getdataset("select AccountName,clientid from ClientMaster where ClientID='" + fordt1.Rows[0]["companyID"].ToString() + "'");
                        dt1.Merge(party, true);
                    }
                    //foreach (var s in chkvchid)
                    //{
                    //    foreach (var v in chkvchno)
                    //    {
                    //        fordt = cl.getdataset("select distinct productname from purchaseorderproductmaster where isactive=1 and VchNo='" + v + "' and companyID='" + s + "' order by productname");
                    //        dtall.Merge(fordt, true);
                    //        dt = dtall.DefaultView.ToTable(true, "productname");
                    //        fordt1 = cl.getdataset("select distinct companyID from purchaseorderproductmaster where isactive=1 and VchNo='" + v + "' and companyID='" + s + "'");
                    //        DataTable party = new DataTable();
                    //        party = cl.getdataset("select AccountName,clientid from ClientMaster where ClientID='" + fordt1.Rows[0]["companyID"].ToString() + "'");
                    //        dt1.Merge(party, true);
                    //    }
                    //}
                }
                DialogResult dr = MessageBox.Show("For Order wise Report press YES otherwise for Item wise Report press NO?", "Report", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    //Bind();
                    PurchaseOrderReport();
                    this.Close();
                }
                else
                {
                    // Bind1();
                    PurchaseOrderItemReport();
                    this.Close();
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count == 1 && e.NewValue == CheckState.Unchecked)
            {
                gridp.Visible = false;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            int i = 0;
            foreach (object itemChecked in checkedListBox1.CheckedItems)
            {
                string str = itemChecked.ToString();

                if (dtcl.Rows.Count > 0)
                {

                    dtbind = cl.getdataset("select VchNo,OrderNo,Accountname as ClientName,OrderStatus,OrderDate,Totalqty,companyID from PurchaseOrderMaster po inner join ClientMaster c on c.ClientID=po.CompanyId where po.isactive=1 and c.AccountName='" + str + "' and isGenerated=0");
                    // dtbind = cl.getdataset("select VchNo,OrderNo,OrderStatus,OrderDate,Totalqty,companyID from PurchaseOrderMaster po inner join ClientMaster c on c.ClientID=po.CompanyId where po.isactive=1  and isGenerated=0");
                    if (dtbind != null && dtbind.Rows.Count > 0)
                    {
                        gridp.Visible = true;
                        if (i == 0)
                        {
                            dt = dtbind.Copy();
                            gridp.DataSource = dtbind;
                        }
                        else
                        {
                            //dt = dtbind.Copy();
                            dt.Merge(dtbind);
                            gridp.DataSource = dt;
                        }
                    }

                }

                i++;
            }
        }

        private void btnvieworder_Click(object sender, EventArgs e)
        {
            try
            {
                btnvieworder.Enabled = false;
                string[] chkvchid = new string[gridp.Rows.Count];
                string[] chkvchno = new string[gridp.Rows.Count];
                for (int i = 0; i < gridp.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(gridp.Rows[i].Cells[0].Value) == true)
                    {
                        id = Convert.ToInt32(gridp.Rows[i].Cells["companyID"].Value);
                        vchno = Convert.ToInt32(gridp.Rows[i].Cells["vchno"].Value);
                        chkvchid[i] = id.ToString();
                        chkvchno[i] = vchno.ToString();
                        //   conn.execute("UPDATE [dbo].[PurchaseOrderMaster] SET [isGenerated]=1,[GeneDate]='" + DateTime.Now + "' where [VchNo]='" + id + "'");
                        //    DataTable dt = new DataTable();
                        //    dt = cl.getdataset("Select CompanyId from PurchaseOrderMaster where companyID='" + id + "'");
                    }
                }
                if (chkvchid != null && chkvchno != null)
                {

                    for (int i = 0; i < chkvchid.Count(); i++)
                    {
                        fordt = cl.getdataset("select distinct productname from purchaseorderproductmaster where isactive=1 and VchNo='" + chkvchno[i] + "' and companyID='" + chkvchid[i] + "' order by productname");
                        dtall.Merge(fordt, true);
                        dt = dtall.DefaultView.ToTable(true, "productname");
                        fordt1 = cl.getdataset("select distinct companyID from purchaseorderproductmaster where isactive=1 and VchNo='" + chkvchno[i] + "' and companyID='" + chkvchid[i] + "'");
                        DataTable party = new DataTable();
                        party = cl.getdataset("select AccountName,clientid from ClientMaster where ClientID='" + fordt1.Rows[0]["companyID"].ToString() + "'");
                        dt1.Merge(party, true);
                    }
                    //foreach (var s in chkvchid)
                    //{
                    //    foreach (var v in chkvchno)
                    //    {

                    //            fordt = cl.getdataset("select distinct productname from purchaseorderproductmaster where isactive=1 and VchNo='" + v + "' and companyID='" + s + "' order by productname");
                    //            dtall.Merge(fordt, true);
                    //            dt = dtall.DefaultView.ToTable(true, "productname");
                    //            fordt1 = cl.getdataset("select distinct companyID from purchaseorderproductmaster where isactive=1 and VchNo='" + v + "' and companyID='" + s + "'");
                    //            DataTable party = new DataTable();
                    //            party = cl.getdataset("select AccountName,clientid from ClientMaster where ClientID='" + fordt1.Rows[0]["companyID"].ToString() + "'");
                    //            dt1.Merge(party, true);
                    //    }

                    //}
                }
                DialogResult dr = MessageBox.Show("For Order wise Report press YES otherwise for Item wise Report press NO?", "Report", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    //Bind();
                    PurchaseOrderReport();
                    this.Close();
                }
                else
                {
                    // Bind1();
                    PurchaseOrderItemReport();
                    this.Close();
                }
            }
            catch
            {
                main = new DataTable();
                main1 = new DataTable();
                main2 = new DataTable();
                main3 = new DataTable();
                main4 = new DataTable();
                main5 = new DataTable();
                main6 = new DataTable();
            }
        }

        private void lselectalll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                checkedListBox2.SetItemChecked(i, true);
            }
        }

        private void lremove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                checkedListBox2.SetItemChecked(i, false);
            }
        }

        private void lclient_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
        }

        private void rclient_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
        }

        private void litems_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (DataGridViewRow row in gridp.Rows)
            {
                row.Cells[0].Value = true;

            }

        }

        private void ritem_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (DataGridViewRow row in gridp.Rows)
            {
                row.Cells[0].Value = false;

            }
        }

       



    }
}
