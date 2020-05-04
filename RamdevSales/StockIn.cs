using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.rtf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Web;
using System.Configuration;

namespace RamdevSales
{
    public partial class StockIn : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        SqlCommand cmd;
        SqlDataAdapter sda;
        static Int32 bill;
        static double total, vat, net;
        public StockIn()
        {
            InitializeComponent();
        }

        private void StockIn_Load(object sender, EventArgs e)
        {
            LVDayBook.Columns.Add("Invoice No", 150, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("Invoice Date", 100, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("SupplierName", 250, HorizontalAlignment.Left);
            LVDayBook.Columns.Add("Cheque No", 100, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("Cheque Date", 100, HorizontalAlignment.Left);
            LVDayBook.Columns.Add("Bill Amount", 100, HorizontalAlignment.Right);
            
        }

        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                LVDayBook.Items.Clear();

                SqlCommand cmd = new SqlCommand("select i.InvoiceNo,i.InvoiceDate,c.SupplierDesc,i.ChequeNo,i.ChequeDate,i.Billamt from InwardMstr i inner join CompanyMaster c on c.CompanyID=i.CompanyID where InvoiceDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString("MM-dd-yyyy") + "' and InvoiceDate<='" + Convert.ToDateTime(DTPTo.Text).ToString("MM-dd-yyyy") + "'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                bill = 0;
                total = 0;
                vat = 0;
                net = 0;
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    LVDayBook.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                  
                    bill++;
                    total = total + Convert.ToDouble(dt.Rows[i][5].ToString());
                
                }

                TxtInvoice.Text = bill.ToString();
                txtnetamt.Text = total.ToString("N2");

               
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void DTPFrom_ValueChanged(object sender, EventArgs e)
        {
            DTPTo.MinDate = Convert.ToDateTime(DTPFrom.Text);
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btngenrpt_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                Document document1;
                PdfWriter Writer;
                document1 = new Document(PageSize.A4);
                Writer = PdfWriter.GetInstance(document1, new FileStream("C:\\Report\\STOCKINWARD"+DateTime.Now.ToShortDateString()+".pdf", FileMode.Create));

                callreport(document1, Writer);

                System.Diagnostics.Process.Start("C:\\Report\\STOCKINWARD" + DateTime.Now.ToShortDateString() + ".pdf");
               // String pathToExecutable = "AcroRd32.exe";

                String sReport = "C:\\Report\\STOCKINWARD" + DateTime.Now.ToShortDateString() + ".pdf"; //'Complete name/path of PDF file



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void callreport(Document document1, PdfWriter Writer)
        {
            Paragraph para1;


            para1 = new Paragraph();



            iTextSharp.text.Font _fontitalic = FontFactory.GetFont("Verdana", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
            iTextSharp.text.Font _font24normal = FontFactory.GetFont("Verdana", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
            iTextSharp.text.Font _font24 = FontFactory.GetFont("Verdana", 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
            iTextSharp.text.Font _fontsize24 = FontFactory.GetFont("Verdana", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
            iTextSharp.text.Font _fontunder = FontFactory.GetFont("Verdana", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLUE);
            iTextSharp.text.Font _fontsmall = FontFactory.GetFont("Verdana", 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
            iTextSharp.text.Font _font1 = FontFactory.GetFont("Verdana", 9, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
            iTextSharp.text.Font _font2 = FontFactory.GetFont("Verdana", 9, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLUE);
            Paragraph line = new Paragraph("_____________________________________________________________________________________________________________________________________________________________________________________________________", _fontsmall);


            para1.Alignment = Element.ALIGN_CENTER;




            Paragraph paragraph3 = new Paragraph();
            paragraph3.Alignment = Element.ALIGN_CENTER;
            paragraph3.Add("Date:" + DateTime.Now);

            Paragraph paragraph2;
            paragraph2 = new Paragraph();
            paragraph2.Alignment = Element.ALIGN_CENTER;

            Phrase q = new Phrase("Ramdev Sales Corporation" + Environment.NewLine, _font24normal);
            Phrase q1 = new Phrase("Gorti, Vadodara" + Environment.NewLine, _font1);
            Phrase p = new Phrase(Environment.NewLine + "DATE WISE STOCK INWARD" + Environment.NewLine, _fontunder);

            paragraph2.Add(p);
            Phrase ps, ph, pss, phh;
            //DataTable dt= new DataTable();
            //SqlCommand cmd= new SqlCommand("SELECT * FROM companymaster WHERE SetDefault=1", con);
            // SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);
            //Phrase ph = new Phrase();
            //try
            //{
            //    ps = new Phrase(dt.Rows[0][1].ToString(), _fontitalic);
            //    ph = new Phrase(Environment.NewLine + dt.Rows[0][4].ToString() + Environment.NewLine + dt.Rows[0][5].ToString() + Environment.NewLine + dt.Rows[0][7].ToString() + Environment.NewLine, _fontsmall);
            //}
            //catch(Exception ex)
            //{
            //    ps = new Phrase("CYPOS", _fontitalic);
            //}

            para1.Add(q);
            para1.Add(q1);
            para1.Add(p);
            ps = new Phrase(Environment.NewLine + "                From Date:  ", _font2);
            ph = new Phrase(DTPFrom.Text + "                                              ", _font1);
            pss = new Phrase("To Date:  ", _font2);
            phh = new Phrase(DTPTo.Text, _font1);

            para1.Add(ps);
            para1.Add(ph);
            para1.Add(pss);
            para1.Add(phh);


            HeaderFooter pdfHeader = new HeaderFooter(para1, false);
            pdfHeader.Alignment = Element.ALIGN_CENTER;
            pdfHeader.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            document1.Header = pdfHeader;
            HeaderFooter footer = new HeaderFooter(new Phrase("Printed On: " + DateTime.Now + "                                                                                                   Page No: ", _fontsmall), true);
            footer.Alignment = Element.ALIGN_CENTER;
            footer.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            document1.Footer = footer;


            document1.Open();


            PdfPTable table1 = new PdfPTable(6);
            float[] achoDecolumns = new float[] { 20f, 10f, 55f, 10f, 10f, 15F };
            table1.SetWidths(achoDecolumns);
            table1.DefaultCell.BorderColor = new iTextSharp.text.Color(System.Drawing.Color.Black);
            table1.TotalWidth = 550f;
            table1.LockedWidth = true;

            Phrase ps1 = new Phrase("Invoice No", _fontsize24);
            Phrase ps2 = new Phrase("Invoice Date", _fontsize24);
            Phrase ps3 = new Phrase("SupplierName", _fontsize24);
            Phrase ps4 = new Phrase("Cheque No", _fontsize24);
            Phrase ps5 = new Phrase("Cheque Date", _fontsize24);
            Phrase ps6 = new Phrase("Bill Amt", _fontsize24);
          





            table1.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            table1.DefaultCell.Padding = 5;

            PdfPCell cel = new PdfPCell(ps1);
            cel.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            cel.HorizontalAlignment = 1;
            cel.Padding = 4;

            PdfPCell cel1 = new PdfPCell(ps2);
            cel1.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            cel1.HorizontalAlignment = 1;
            cel1.Padding = 4;

            PdfPCell cel2 = new PdfPCell(ps3);
            cel2.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            cel2.HorizontalAlignment = 0;
            cel2.Padding = 4;

            PdfPCell cel3 = new PdfPCell(ps4);
            cel3.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            cel3.HorizontalAlignment = 1;
            cel3.Padding = 4;

            PdfPCell cel4 = new PdfPCell(ps5);
            cel4.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            cel4.HorizontalAlignment = 1;
            cel4.Padding = 4;

            PdfPCell cel5 = new PdfPCell(ps6);
            cel5.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            cel5.HorizontalAlignment = 2;
            cel5.Padding = 4;

            

            table1.AddCell(cel);
            table1.AddCell(cel1);
            table1.AddCell(cel2);
            table1.AddCell(cel3);
            table1.AddCell(cel4);
            table1.AddCell(cel5);
            



            for (int i = 0; i < 6; i++)
                table1.AddCell("");


            cmd = new SqlCommand("select i.InvoiceNo,i.InvoiceDate,c.SupplierDesc,i.ChequeNo,i.ChequeDate,i.Billamt from InwardMstr i inner join CompanyMaster c on c.CompanyID=i.CompanyID where InvoiceDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString("MM-dd-yyyy") + "' and InvoiceDate<='" + Convert.ToDateTime(DTPTo.Text).ToString("MM-dd-yyyy") + "'", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable main = new DataTable();
            sda.Fill(main);

            for (int i = 0; i < main.Rows.Count; i++)
            {
                for (int j = 0; j < main.Columns.Count; j++)
                {
                    if (j == 5)
                    {

                        Phrase pp = new Phrase("" + main.Rows[i][j].ToString(), _font24);
                        PdfPCell cell = new PdfPCell(pp);
                        cell.Padding = 5;
                        cell.HorizontalAlignment = 2;
                        table1.AddCell(cell);

                    }
                    else if (j == 2)
                    {
                        string str = main.Rows[i][j].ToString();
                        String[] str1 = str.Split('\n');
                        String fullstr = "";
                        for (int a = 0; a < str1.Length; a++)
                        {
                            String[] str2 = str1[a].Split('\r');
                            for (int b = 0; b < str2.Length; b++)
                            {
                                fullstr = fullstr + str2[b];
                            }
                        }
                        Phrase pp = new Phrase("" + fullstr, _font24);
                        PdfPCell cell = new PdfPCell(pp);

                        cell.HorizontalAlignment = 0;
                        table1.AddCell(cell);
                    }
                    else
                    {
                        try
                        {
                            Phrase pp = new Phrase("" + Convert.ToDateTime(main.Rows[i][j].ToString()).ToString("dd-MM-yyyy"), _font24);
                            PdfPCell cell = new PdfPCell(pp);
                            cell.Padding = 5;
                            cell.HorizontalAlignment = 1;
                            table1.AddCell(cell);
                        }
                        catch
                        {
                            Phrase pp = new Phrase("" + main.Rows[i][j].ToString(), _font24);
                            PdfPCell cell = new PdfPCell(pp);
                            cell.Padding = 5;
                            cell.HorizontalAlignment = 1;
                            table1.AddCell(cell);
                        }
                    }

                }
            }

            for (int i = 0; i < 4; i++)
                table1.AddCell("");

            Phrase phr = new Phrase("TOTAL:", _font1);
            table1.AddCell(phr);

           
            Phrase phr3 = new Phrase(txtnetamt.Text, _fontsize24);
            PdfPCell cl3 = new PdfPCell(phr3);
            cl3.Padding = 5;
            cl3.HorizontalAlignment = 2;
            table1.AddCell(cl3);

            for (int i = 0; i < 6; i++)
                table1.AddCell("");

            document1.Add(table1);
            document1.Close();
     
        }

        private void LVDayBook_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            String str = "select pm.Product_Name,ip.qty from InwardProductMstr ip inner join InwardMstr i on i.InwardID=ip.InwardID inner join ProductMaster pm on pm.ProductID=ip.ProductID where i.InvoiceNo='" + LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[0].Text + "'";

            InwardDetails bd = new InwardDetails(this);
            bd.Fromdatewise(str, LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[0].Text, 1);
            bd.Show();
        }
    }
}
