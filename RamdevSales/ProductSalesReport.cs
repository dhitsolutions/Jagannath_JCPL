using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.rtf;
using iTextSharp.text.html.simpleparser;
using System.Web;
using System.IO;



namespace RamdevSales
{
    public partial class ProductSalesReport : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        SqlCommand cmd;
        SqlDataAdapter sda;
        public ProductSalesReport()
        {
            InitializeComponent();
        }

        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("select bm.Bill_No,bm.Bill_Date,cm.ClientName,bm.Bill_Net_Amt  from BillMaster bm inner join ClientMaster cm on cm.ClientID = bm.ClientID where Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString("MM-dd-yyyy") + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString("MM-dd-yyyy") + "'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                //LVDayBook.Items.Clear();
                //int tot = 0, cash = 0, cheque = 0;
                //Double amt = 0, cashamt = 0, chequeamt = 0;
                //if (dt.Rows.Count > 0)
                //{
                //    tot = dt.Rows.Count;
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    LVDayBook.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                //    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                //    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                //    LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[7].ToString());
                }

                //if (dt.Rows[i][3].ToString() == "Cash")
                //{
                //    cash++;
                //    cashamt = cashamt + Convert.ToDouble(dt.Rows[i].ItemArray[4].ToString());
                //}
                //if (dt.Rows[i][3].ToString() == "Cheque")
                //{
                //    cheque++;
                //    chequeamt = chequeamt + Convert.ToDouble(dt.Rows[i].ItemArray[4].ToString());
                //}
                //amt = amt + Convert.ToDouble(dt.Rows[i].ItemArray[4].ToString());

                //}
                //     else
                //{
                //    LVDayBook.Items.Clear();
                //    MessageBox.Show("Empty Stack");
                //}
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

        private void btngenrpt_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    con.Open();
                    Document document1;
                    PdfWriter Writer;
                    document1 = new Document(PageSize.A4.Rotate());
                    Writer = PdfWriter.GetInstance(document1, new FileStream("C:\\Report\\ProductSalesReport.pdf", FileMode.Create));

                    callreport(document1, Writer);

                    System.Diagnostics.Process.Start("C:\\Report\\ProductSalesReport.pdf");
                    //String pathToExecutable = "AcroRd32.exe";

                    //String sReport = "C:\\Report\\ProductSalesReport.pdf"; //'Complete name/path of PDF file



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

        }

        private void callreport(Document document1, PdfWriter Writer)
        {
            Paragraph para1;


            para1 = new Paragraph();



            iTextSharp.text.Font _fontitalic = FontFactory.GetFont("Verdana", 16, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
            iTextSharp.text.Font _font24normal = FontFactory.GetFont("Verdana", 20, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
            iTextSharp.text.Font _font24 = FontFactory.GetFont("Verdana", 9, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
            iTextSharp.text.Font _fontsize24 = FontFactory.GetFont("Verdana", 9, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
            iTextSharp.text.Font _fontunder = FontFactory.GetFont("Verdana", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLUE);
            iTextSharp.text.Font _fontsmall = FontFactory.GetFont("Verdana", 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
            iTextSharp.text.Font _font1 = FontFactory.GetFont("Verdana", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
            iTextSharp.text.Font _font2 = FontFactory.GetFont("Verdana", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLUE);
            Paragraph line = new Paragraph("_____________________________________________________________________________________________________________________________________________________________________________________________________", _fontsmall);


            para1.Alignment = Element.ALIGN_CENTER;




            Paragraph paragraph3 = new Paragraph();
            paragraph3.Alignment = Element.ALIGN_CENTER;
            paragraph3.Add("Date:" + DateTime.Now);

            Paragraph paragraph2;
            paragraph2 = new Paragraph();
            paragraph2.Alignment = Element.ALIGN_CENTER;

            Phrase q = new Phrase("Ramdev Sales Corporation" + Environment.NewLine, _font24normal);
            Phrase q1 = new Phrase("MatruKrupa, F-45, Gunatit Park, Near Panchamrut Flats," + Environment.NewLine, _font1);
            Phrase q2 = new Phrase("Behind T.B. Hospital, Gorti, Vadodara" + Environment.NewLine, _font1);
            Phrase p = new Phrase(Environment.NewLine + "BILL INVOICE REPORT" + Environment.NewLine, _fontunder);

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
            para1.Add(q2);
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
            HeaderFooter footer = new HeaderFooter(new Phrase("Printed On: " + DateTime.Now + "                                                                                                                                                                                                                                                                                                            Page No: ", _fontsmall), true);
            footer.Alignment = Element.ALIGN_CENTER;
            footer.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            document1.Footer = footer;


            document1.Open();


            PdfPTable table1 = new PdfPTable(5);
            float[] achoDecolumns = new float[] { 10f, 50f, 30f, 20f,20f };
            table1.SetWidths(achoDecolumns);
            table1.DefaultCell.BorderColor = new iTextSharp.text.Color(System.Drawing.Color.Black);
            table1.TotalWidth = 700f;
            table1.LockedWidth = true;



            Phrase ps1 = new Phrase("Product_No", _fontsize24);
            Phrase ps2 = new Phrase("Product_Name", _fontsize24);
            Phrase ps3 = new Phrase("Sales Qty ", _fontsize24);
            Phrase ps4 = new Phrase("Total Amt", _fontsize24);
            Phrase ps5 = new Phrase("Free Product", _fontsize24);
            //Phrase ps6 = new Phrase("Bill_vat_Amt", _fontsize24);
            //Phrase ps7 = new Phrase("Bill_Net_Amt", _fontsize24);





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
            cel2.HorizontalAlignment = 1;
            cel2.Padding = 4;

            PdfPCell cel3 = new PdfPCell(ps4);
            cel3.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            cel3.HorizontalAlignment = 1;
            cel3.Padding = 4;

            PdfPCell cel4 = new PdfPCell(ps5);
            cel4.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            cel4.HorizontalAlignment = 1;
            cel4.Padding = 4;

            //PdfPCell cel5 = new PdfPCell(ps6);
            //cel5.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            //cel5.HorizontalAlignment = 1;
            //cel5.Padding = 4;

            //PdfPCell cel6 = new PdfPCell(ps7);
            //cel6.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            //cel6.HorizontalAlignment = 1;
            //cel6.Padding = 4;

            table1.AddCell(cel);
            table1.AddCell(cel1);
            table1.AddCell(cel2);
            table1.AddCell(cel3);
            table1.AddCell(cel4);
            //table1.AddCell(cel5);
            //table1.AddCell(cel6);



            for (int i = 0; i < 5; i++)
                table1.AddCell("");


            cmd = new SqlCommand("select bm.Bill_No,bm.Bill_Date,cm.ClientName,bm.Bill_Net_Amt  from BillMaster bm inner join ClientMaster cm on cm.ClientID = bm.ClientID where Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString("MM-dd-yyyy") + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString("MM-dd-yyyy") + "'", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable main = new DataTable();
            sda.Fill(main);

            for (int i = 0; i < main.Rows.Count; i++)
            {
                for (int j = 0; j < main.Columns.Count; j++)
                {
                    if (j == 3||j==4)
                    {

                        Phrase pp = new Phrase("" + main.Rows[i][j].ToString(), _font24);
                        PdfPCell cell = new PdfPCell(pp);
                        cell.Padding = 5;
                        cell.HorizontalAlignment = 2;
                        table1.AddCell(cell);

                    }
                    else if (j == 1)
                    {

                        Phrase pp = new Phrase("" + main.Rows[i][j].ToString(), _font24);
                        PdfPCell cell = new PdfPCell(pp);
                        cell.Padding = 5;
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

            for (int i = 0; i < 5; i++)
                table1.AddCell("");

            document1.Add(table1);
            document1.Close();
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you Wand to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void DTPFrom_ValueChanged(object sender, EventArgs e)
        {
            DTPFrom.MinDate = Convert.ToDateTime(DTPFrom.Text);
        }

        private void ProductSalesReport_Load(object sender, EventArgs e)
        {
            LVDayBook.Columns.Add("Product_No", 100, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("Product_Name", 350, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("Sales Qty", 100, HorizontalAlignment.Left);
            LVDayBook.Columns.Add("Total Amt", 100, HorizontalAlignment.Left);
            LVDayBook.Columns.Add("Free Product", 100, HorizontalAlignment.Center);
            //LVDayBook.Columns.Add("Bill_vat_Amt", 100, HorizontalAlignment.Center);
            //LVDayBook.Columns.Add("Bill_Net_Amt", 120, HorizontalAlignment.Center);
            //LVDayBook.Columns.Add("ClientName", 120, HorizontalAlignment.Center);
        }
    }
}
