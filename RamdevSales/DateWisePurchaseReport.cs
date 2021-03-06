﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace RamdevSales
{
    public partial class DateWisePurchaseReport : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection connn = new Connection();
        SqlCommand cmd;
        SqlDataAdapter sda;
        OleDbSettings ods = new OleDbSettings();
        static Int32 bill;
        static double total, vat, net;
        private string p;
        public DateWisePurchaseReport()
        {
            InitializeComponent();
        }

        public DateWisePurchaseReport(string p)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.p = p;
        }

        private void DateWisePurchaseReport_Load(object sender, EventArgs e)
        {
            DataSet dtrange = connn.getdata("SELECT * FROM Company where CompanyId='" + Master.companyId + "'");
           
            DTPFrom.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["StartDate"].ToString());
            DTPFrom.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["EndDate"].ToString());
            DTPTo.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["StartDate"].ToString());
            DTPTo.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["EndDate"].ToString());
            DTPFrom.Value = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["StartDate"].ToString());
            LVDayBook.Columns.Add("Bill No", 70, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("Bill Date", 100, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("PO No", 100, HorizontalAlignment.Left);
            LVDayBook.Columns.Add("ClientName", 300, HorizontalAlignment.Left);
            LVDayBook.Columns.Add("Address", 200, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("Total Amt", 100, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("TAX Amt", 100, HorizontalAlignment.Center);
            LVDayBook.Columns.Add("Net Amt", 120, HorizontalAlignment.Center);
            bindgrid();
            btnnew.Focus();
        }

        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            bindgrid();

        }

        public void bindgrid()
        {
            try
            {
                DataTable dt3 = new DataTable();
                dt3 = conn.getdataset("select a, u, d, v, p, mId, uId, cId from UserRights where mId=4 and uId='" + UserLogin.id + "' and cId= " + Master.companyId + " and isActive=1");


                if (dt3.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dt3.Rows[0][0]) == false)
                    {
                        btnnew.Visible = false;
                    }
                    if (Convert.ToBoolean(dt3.Rows[0][1]) == false)
                    {
                        LVDayBook.Enabled = false;
                        LVDayBook.GridLines = true;
                        LVDayBook.BackColor = System.Drawing.Color.LightYellow;

                    }
                    if (Convert.ToBoolean(dt3.Rows[0][4]) == false)
                    {
                        btngenrpt.Visible = false;

                    }

                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                LVDayBook.Items.Clear();
                SqlCommand cmd = new SqlCommand("select b.billno,convert(varchar(11), b.bill_date, 113)as bill_date, b.po_no, c.subName,c.Address,b.totalbasic,b.totaltax,b.totalnet from billmaster b inner join Company c on c.CompanyId=b.CompanyId where b.isactive=1 and BillType='" + p + "' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString("MM-dd-yyyy") + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString("MM-dd-yyyy") + "' and b.CompanyId=" + Master.companyId + " order by b.BillNo", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                
                bill = 0;
                total = 0;
                vat = 0;
                net = 0;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        LVDayBook.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                        LVDayBook.Items[i].SubItems.Add(dt.Rows[i].ItemArray[7].ToString());
                        bill++;
                        total = total + Convert.ToDouble(dt.Rows[i][5].ToString());
                        vat = vat + Convert.ToDouble(dt.Rows[i][6].ToString());
                        net = net + Convert.ToDouble(dt.Rows[i][7].ToString());
                    }
                }


                TxtInvoice.Text = bill.ToString();
                txtbillamt.Text = total.ToString("N2");
                txtvat.Text = vat.ToString("N2");
                txtnetamt.Text = net.ToString("N2");

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
            Phrase p = new Phrase(Environment.NewLine + "DATE WISE PURCHASE LIST" + Environment.NewLine, _fontunder);

            paragraph2.Add(p);
            Phrase ps, ph, pss, phh;
            
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
            HeaderFooter footer = new HeaderFooter(new Phrase("Printed On: " + DateTime.Now + "                                                                                                                                                                                                                                                                                                            Page No: ", _fontsmall), true);
            footer.Alignment = Element.ALIGN_CENTER;
            footer.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            document1.Footer = footer;

            document1.Open();

            PdfPTable table1 = new PdfPTable(8);
            float[] achoDecolumns = new float[] { 10f, 12f, 12f, 50f, 13f, 12f, 11f, 13f };
            table1.SetWidths(achoDecolumns);
            table1.DefaultCell.BorderColor = new iTextSharp.text.Color(System.Drawing.Color.Black);
            table1.TotalWidth = 550f;
            table1.LockedWidth = true;

            Phrase ps1 = new Phrase("BillNo", _fontsize24);
            Phrase ps2 = new Phrase("BillDate", _fontsize24);
            Phrase ps3 = new Phrase("PONo", _fontsize24);
            Phrase ps4 = new Phrase("ClientName", _fontsize24);
            Phrase ps8 = new Phrase("Tin No", _fontsize24);
            Phrase ps5 = new Phrase("Bill Amt", _fontsize24);
            Phrase ps6 = new Phrase("Billvat Amt", _fontsize24);
            Phrase ps7 = new Phrase("BillNet Amt", _fontsize24);

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

            PdfPCell cel5 = new PdfPCell(ps6);
            cel5.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            cel5.HorizontalAlignment = 1;
            cel5.Padding = 4;

            PdfPCell cel6 = new PdfPCell(ps7);
            cel6.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            cel6.HorizontalAlignment = 1;
            cel6.Padding = 4;

            PdfPCell cel7 = new PdfPCell(ps8);
            cel7.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            cel7.HorizontalAlignment = 1;
            cel7.Padding = 4;

            table1.AddCell(cel);
            table1.AddCell(cel1);
            table1.AddCell(cel2);
            table1.AddCell(cel3);
            table1.AddCell(cel7);
            table1.AddCell(cel4);
            table1.AddCell(cel5);
            table1.AddCell(cel6);

            for (int i = 0; i < 8; i++)
                table1.AddCell("");

            cmd = new SqlCommand("select bm.Bill_No,bm.Bill_Date,bm.PO_No,cm.On_Bill_desc,cm.TinNO,bm.Bill_Amt,bm.Bill_vat_Amt,bm.Bill_Net_Amt from BillMaster bm inner join ClientMaster cm on cm.ClientID = bm.ClientID where isactive=1 and CompanyId="+Master.companyId+" and BillType='"+p+"' and Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString("MM-dd-yyyy") + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString("MM-dd-yyyy") + "'", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable main = new DataTable();
            sda.Fill(main);

            for (int i = 0; i < main.Rows.Count; i++)
            {
                for (int j = 0; j < main.Columns.Count; j++)
                {
                    if (j == 4 || j == 5 || j == 6 || j == 7)
                    {

                        Phrase pp = new Phrase("" + main.Rows[i][j].ToString(), _font24);
                        PdfPCell cell = new PdfPCell(pp);
                        cell.Padding = 5;
                        cell.HorizontalAlignment = 2;
                        table1.AddCell(cell);

                    }
                    else if (j == 3)
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

            Phrase phr1 = new Phrase(txtbillamt.Text, _fontsize24);
            PdfPCell cll = new PdfPCell(phr1);
            cll.Padding = 5;
            cll.HorizontalAlignment = 2;
            table1.AddCell(cll);

            Phrase phr2 = new Phrase(txtvat.Text, _fontsize24);
            PdfPCell cl2 = new PdfPCell(phr2);
            cl2.Padding = 5;
            cl2.HorizontalAlignment = 2;
            table1.AddCell(cl2);

            Phrase phr3 = new Phrase(txtnetamt.Text, _fontsize24);
            PdfPCell cl3 = new PdfPCell(phr3);
            cl3.Padding = 5;
            cl3.HorizontalAlignment = 2;
            table1.AddCell(cl3);

            for (int i = 0; i < 8; i++)
                table1.AddCell("");

            document1.Add(table1);
            document1.Close();
        }
        Connection conn = new Connection();
        private void LVDayBook_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                setform();
            }
            finally
            {
                this.Enabled = true;
            }
        }

        public void setform()
        {
            this.Enabled = false;
            String str = LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[0].Text;
            DataTable dt1 = conn.getdataset("select * from [contilindia].FormFormat where isactive=1 and type='P' and setdefault=1");
            DefaultPurchase bd = new DefaultPurchase(this);
            Purchase p = new Purchase(this);
            if (dt1.Rows[0]["formname"].ToString() == bd.Text)
            {
                bd.updatemode(str, LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[0].Text, 1);
                bd.Show();
            }
            else if (dt1.Rows[0]["formname"].ToString() == p.Text)
            {
                p.updatemode(str, LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[0].Text, 1);
                p.Show();
            }
        }
        private void LVDayBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    setform();
                }
                finally
                {
                    this.Enabled = true;
                }

            }
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            DataTable dt1 = conn.getdataset("select * from [Contilindia].[FormFormat] where isactive=1 and type='P' and setdefault=1");
            DefaultPurchase frm = new DefaultPurchase();
            
            Purchase p = new Purchase();
            if (dt1.Rows[0]["formname"].ToString() == frm.Text)
            {
                frm.MdiParent = this.MdiParent;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.Show();
            }
            else if (dt1.Rows[0]["formname"].ToString() == p.Text)
            {
                p.MdiParent = this.MdiParent;
                p.StartPosition = FormStartPosition.CenterScreen;
                p.Show();
            }

           
            
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

       

    }
}
