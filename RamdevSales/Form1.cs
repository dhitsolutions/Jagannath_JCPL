using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Diagnostics;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.rtf;
using iTextSharp.text.html.simpleparser;
using System.Web;



namespace RamdevSales
{

    public partial class Form1 : Form
    {
        static string id;
        static string name;
        static String[] cmpy;
        static string qty;
        static string price;
        static string amt;
        static string vat;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        private string p;
        private string p_2;
        public Form1()
        {
            InitializeComponent();
        }
        static int flag;


        public void Data_From_Form2(string data1)
        {
            TxtProdcode.Text = data1;
        }


        public Form1(string p, string p_2)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this.p = p;
            this.p_2 = p_2;
            String id = p_2;
            TxtProductDscrption.Text = id;
            TxtProdcode.Text = this.p;
            
        }

        public void result()
        {
            InitializeComponent();
            TxtProdcode.Text = id;
            TxtProductDscrption.Text = name;
            TxtQty.Text = qty;
            TxtPrice.Text = price;
            TxtVATCode.Text = vat;

        }

        private void TxtScancode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F1)
                {
                    if (cmbcustname.Text != "")
                    {

                        ProductSearch prodserch = new ProductSearch(this);
                        String qry = "select p.ProductID,p.Product_barcode,p.Product_Name,p.Product_Price,p.Product_Vat,cp.MarginInPer from ProductMaster p inner join ClientProductMargin cp on cp.ProductID=p.ProductID inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where cp.ClientID='" + cmbcustname.SelectedValue.ToString() + "' and (";
                        con.Open();
                        int i = 0, m = 0;
                        cmpy = new String[10];
                        foreach (var itemChecked in chklistcompany.CheckedItems)
                        {

                            string itemName = itemChecked.ToString();
                            SqlCommand cmd = new SqlCommand("select companyID from CompanyMaster where Companyname='" + itemName + "'", con);
                            String companyid = cmd.ExecuteScalar().ToString();
                            cmpy[m] = companyid;
                            m++;
                            if (i == 0)
                            {
                                qry = qry + "p.CompanyID='" + companyid + "'";
                                i++;
                            }
                            else
                            {
                                qry = qry + " or p.CompanyID='" + companyid + "'";
                                i++;
                            }
                        }
                        con.Close();
                        qry = qry + ") order by p.ProductID";
                        //Boolean state = true;
                        //for (int i = 0; i < chklistcompany.Items.Count; i++)
                        //    chklistcompany.SetItemCheckState(i, (state ? CheckState.Checked : CheckState.Unchecked));

                        prodserch.Data_From_Form1(TxtProdcode.Text, cmbcustname.SelectedValue.ToString(), qry);
                        prodserch.Show();

                    }
                    else
                    {
                        MessageBox.Show("Please select any Client");
                        cmbcustname.Focus();
                    }
                }
            }
            catch
            {
            }
            finally
            {
                con.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            con.Open();
            this.WindowState = FormWindowState.Maximized;
            LVFO.Columns.Add("Product Code", 176, HorizontalAlignment.Left);
            LVFO.Columns.Add("Product Name", 424, HorizontalAlignment.Left);
            LVFO.Columns.Add("Qty", 127, HorizontalAlignment.Center);
            LVFO.Columns.Add("Free", 128, HorizontalAlignment.Center);
            LVFO.Columns.Add(" Unit Price", 147, HorizontalAlignment.Right);
            LVFO.Columns.Add("Amount", 147, HorizontalAlignment.Right);
            LVFO.Columns.Add("VAT", 120, HorizontalAlignment.Center);

            getsr();
            TxtComputerName.Text = Environment.MachineName;
            TxtRundate.Text = DateTime.Now.ToShortDateString();
          
            SqlCommand cmd = new SqlCommand("select ClientID,ClientName from ClientMaster", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            cmbcustname.ValueMember = "ClientID";
            cmbcustname.DisplayMember = "ClientName";
            cmbcustname.DataSource = dt;
            cmbcustname.SelectedIndex = -1;
            txtbilldesc.Text = "";
            con.Close();
            cmbcustname.Focus();
            bindcheckedboxlist();

          //  autoreaderbind();
        }

        private void autoreaderbind()
        {
            try
            {
                AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();


                SqlDataReader dReader;
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = con;
                cmd1.CommandType = CommandType.Text;

                //start string

                String qry = "select ProductMaster.Product_Name from ProductMaster   ";
                con.Open();
                int i = 0;
                int count = 0;


                foreach (var itemChecked in chklistcompany.CheckedItems)
                {
                    count = count + 1;
                }

                foreach (var itemChecked in chklistcompany.CheckedItems)
                {
                    string itemName = itemChecked.ToString();
                    SqlCommand cmd = new SqlCommand("select companyID from CompanyMaster where Companyname='" + itemName + "'", con);
                    //SqlCommand cmd = new SqlCommand("select companyID from CompanyMaster where Companyname='" + itemName + "'", con);
                    //  String companyid = cmd.ExecuteScalar().ToString();
                    DataSet ds = new DataSet();
                    SqlDataAdapter dq = new SqlDataAdapter("select companyID from CompanyMaster where Companyname='" + itemName + "'", con);
                    dq.Fill(ds);

                    String companyid = ds.Tables[0].Rows[0][0].ToString();
                    if (i == 0)
                    {
                        qry = qry + " where ProductMaster.CompanyID=" + companyid + "";
                        i++;
                    }
                    else
                    {
                        qry = qry + " or ProductMaster.CompanyID=" + companyid + "";
                        i++;
                    }
                }
                con.Close();
                qry = qry + " order by ProductMaster.Product_Name";

                if (count == 0)
                {
                    //end string
                    cmd1.CommandText = qry;


                    con.Open();
                    dReader = cmd1.ExecuteReader();

                    if (dReader.HasRows == true)
                    {
                        while (dReader.Read())
                            namesCollection.Add(dReader["Product_Name"].ToString());

                    }
                    else
                    {
                        MessageBox.Show("Data not found");
                    }
                    dReader.Close();

                    TxtProductDscrption.AutoCompleteMode = AutoCompleteMode.Suggest;
                    TxtProductDscrption.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    TxtProductDscrption.AutoCompleteCustomSource = namesCollection;
                    MessageBox.Show("Please Select Company");
                }
                else
                {

                    //end string
                    cmd1.CommandText = qry;


                    con.Open();
                    dReader = cmd1.ExecuteReader();

                    if (dReader.HasRows == true)
                    {
                        while (dReader.Read())
                            namesCollection.Add(dReader["Product_Name"].ToString());

                    }
                    else
                    {
                        MessageBox.Show("Data not found");
                    }
                    dReader.Close();

                    TxtProductDscrption.AutoCompleteMode = AutoCompleteMode.Suggest;
                    TxtProductDscrption.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    TxtProductDscrption.AutoCompleteCustomSource = namesCollection;
                }
            }
            catch
            {
            }
            finally
            {
                con.Close();
            }
        }

        private void bindcheckedboxlist()
        {
            SqlCommand cmd = new SqlCommand("select * from CompanyMaster", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow drItem in dt.Rows)
            {
                //this.chkLB.Items.Add(drItem["Name"]);
                this.chklistcompany.Items.Add(drItem["CompanyName"]);
            }
        }

        void getsr()
        {
            try
            {

                SqlCommand cmd = new SqlCommand("select max(Bill_No) from BillMaster", con);
                String str = cmd.ExecuteScalar().ToString();
                int id, count;
                //     Object data = dr[1];

                if (str == "")
                {

                    id = 1;
                    count = 1;
                }
                else
                {
                    id = Convert.ToInt32(str) + 1;
                    count = Convert.ToInt32(str) + 1;
                }
                TxtBillNo.Text = count.ToString();

            }
            catch
            {
            }
            finally
            {
               
            }

        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                frm.Close();
            }
            this.Close();
        }

        private void TxtProdcode_TextChanged(object sender, EventArgs e)
        {


        }

        private void TxtQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (TxtQty.Text != "")
                {
                    double total = Convert.ToInt32(TxtQty.Text) * Convert.ToDouble(TxtPrice.Text);

                    TxtTotalAmount.Text = Math.Round(total, 2).ToString("N2");

                }
                //DialogResult dr = MessageBox.Show("Do you want to Change Unit Price?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (dr == DialogResult.Yes)
                //{
                //    TxtPrice.Focus();
                //}
                        

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error:" + ex.Message);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            //Int32 productcode = 0;
            //Int32 billno = 0;
            //string billdate = "";
            //string productdesc = "";
            //Int32 qty = 0;
            //double price = 0;
            //double amount = 0;
            //string vat_code = "";

            //if (string.IsNullOrEmpty(this.TxtBillNo.Text))
            //{
            //    billno = Convert.ToInt32(TxtBillNo.Text);

            //}
            //if (string.IsNullOrEmpty(this.TxtSystemDate.Text))
            //{
            //    billdate = Convert.ToDateTime(DateTime.Now).ToString("MM-dd-yyyy");
            //}
            //if (string.IsNullOrEmpty(this.TxtProductCode.Text))
            //{
            //    productcode = Convert.ToInt32(TxtProductCode.Text);
            //}
            //if (string.IsNullOrEmpty(this.TxtProductDscrption.Text))
            //{
            //    productdesc = TxtProductDscrption.Text;
            //}
            //if (string.IsNullOrEmpty(this.TxtQty.Text))
            //{
            //    qty = Convert.ToInt32(TxtQty.Text);
            //}
            //if (string.IsNullOrEmpty(this.TxtPrice.Text))
            //{
            //    price = Convert.ToDouble(TxtPrice.Text);
            //}
            //if (string.IsNullOrEmpty(this.TxtTotalAmount.Text))
            //{
            //    amount = Convert.ToDouble(TxtTotalAmount.Text);
            //}
            //if (string.IsNullOrEmpty(this.TxtVATCode.Text))
            //{
            //    vat_code = TxtVATCode.Text;
            //}

            
            ListViewItem li;
            li = LVFO.Items.Add(TxtProdcode.Text);
            li.SubItems.Add(TxtProductDscrption.Text);
            li.SubItems.Add(TxtQty.Text);
            li.SubItems.Add(txtfree.Text);
            li.SubItems.Add(TxtPrice.Text);
            li.SubItems.Add(TxtTotalAmount.Text);
            li.SubItems.Add(TxtVATCode.Text);

            TxtProductDscrption.Focus();
            clearfirst();
            totalcalculation();
            

        }

        private void totalcalculation()
        {
            Double total = 0;
            Double vat = 0;
            int qty = 0;
            for (int i = 0; i < LVFO.Items.Count; i++)
            {
                qty = qty + Convert.ToInt32(LVFO.Items[i].SubItems[2].Text);
                total = total + Convert.ToDouble(LVFO.Items[i].SubItems[5].Text);
                //double vatcalc = (100 * Convert.ToDouble(LVFO.Items[i].SubItems[6].Text)) / 105;
                Double multi = 0;
                multi = (Convert.ToDouble(LVFO.Items[i].SubItems[5].Text) * (Convert.ToDouble(LVFO.Items[i].SubItems[6].Text) / 100));
                vat = vat + multi;

            }

            txttotqty.Text = qty.ToString();
            TxtBillTotal.Text = Math.Round(total, 2).ToString("N2");
            TxtVAT16Amnt.Text = Math.Round(vat, 2).ToString("N2");
            TxtVATExmptAmnt.Text = Math.Round((total + vat), 2).ToString("N2");
        }

        private void clearfirst()
        {
            TxtProdcode.Text = "";
            TxtProductDscrption.Text = "";
            TxtQty.Text = "";
            TxtPrice.Text = "";
            TxtTotalAmount.Text = "";
            TxtVATCode.Text = "";
            txtfree.Text = "";
        }

       
        private void TxtProdcode_Validated(object sender, EventArgs e)
        {
            try
            {
                if (cmbcustname.Text != "")
                {
                    if (TxtProdcode.Text != "")
                    {
                        String qry = "select p.*,cp.MarginInPer from ProductMaster p inner join ClientProductMargin cp on cp.ProductID=p.ProductID inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where p.ProductID='" + TxtProdcode.Text + "' and cp.ClientID='" + cmbcustname.SelectedValue + "' and (";
                          int i = 0;
                          con.Open();
                    foreach (var itemChecked in chklistcompany.CheckedItems)
                    {
                        string itemName = itemChecked.ToString();
                            SqlCommand cmd = new SqlCommand("select companyID from CompanyMaster where Companyname='" + itemName + "'", con);
                            String companyid = cmd.ExecuteScalar().ToString();
                            if (i == 0)
                            {
                                qry = qry + "p.CompanyID='" + companyid + "'";
                                i++;
                            }
                            else
                            {
                                qry = qry + " or p.CompanyID='" + companyid + "'";
                                i++;
                            }
                    }
                    con.Close();
                    qry = qry + ")"; 

                        SqlCommand cmd1 = new SqlCommand(qry, con);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd1);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            TxtProductDscrption.Text = dt.Rows[0][3].ToString();
                            Double temp = Convert.ToDouble(dt.Rows[0][4].ToString()) * (Convert.ToDouble(dt.Rows[0][6].ToString()) / 100);
                            double t1 = (Convert.ToDouble(dt.Rows[0][4].ToString()) - temp);
                            double tempvat = (t1 * Convert.ToDouble(dt.Rows[0][5].ToString())) / (100 + Convert.ToDouble(dt.Rows[0][5].ToString()));
                            TxtPrice.Text = Math.Round((t1 - tempvat), 2).ToString("N2");
                            TxtVATCode.Text = dt.Rows[0][5].ToString();

                            if (TxtQty.Text == "")
                            {
                                TxtQty.Text = "1";
                                txtfree.Text = "0";
                            }
                            else
                            {
                                TxtQty.Text = "";
                                TxtQty.SelectedText = "1";
                            }
                        }
                        else
                        {
                            MessageBox.Show("please choose valid product");
                            TxtProdcode.Text = "";
                            TxtProdcode.Focus();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please Select Client First");
                    cmbcustname.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
                TxtProdcode.Text = "";
                TxtProdcode.Focus();
            }
        }

        private void LVFO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
            if(LVFO.SelectedItems.Count > 0)
            {
                TxtProdcode.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[0].Text;
                TxtProductDscrption.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[1].Text;
                TxtQty.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[2].Text;
                txtfree.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[3].Text;
                TxtPrice.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[4].Text;
                TxtTotalAmount.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[5].Text;
                TxtVATCode.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[6].Text;
                Double total = 0;
                try
                {
                    total = Math.Round((Convert.ToDouble(TxtBillTotal.Text) - Convert.ToDouble(LVFO.Items[LVFO.FocusedItem.Index].SubItems[4].Text)), 2);
                }
                catch
                {
                }
                TxtBillTotal.Text = total.ToString();
                LVFO.Items[LVFO.FocusedItem.Index].Remove();
                totalcalculation();
              

            }
        
        }

       

        private void cmbcustname_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select On_Bill_Desc from Clientmaster where ClientName='" + cmbcustname.Text + "'", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                txtbilldesc.Text = dt.Rows[0][0].ToString();
                clearfirst();
                LVFO.Items.Clear();
                TxtBillTotal.Text = "";
                TxtVAT16Amnt.Text = "";
                TxtVATExmptAmnt.Text = "";
                txtpono.Text = "";
                
                txtpono.Focus();
            }
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = null;
            if (p == null)
            {
                p = new Process();
                p.StartInfo.FileName = "Calc.exe";
                p.Start();

            }
            else
            {
                p.Close();
                p.Dispose();

            }
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            ChangeNumbersToWords sh = new ChangeNumbersToWords();
            string str = sh.changeToWords("11254.36");
            String str1 = sh.NumberToText(11254);
            clearfirst();
            LVFO.Items.Clear();
            TxtBillTotal.Text = "";
            TxtVAT16Amnt.Text = "";
            TxtVATExmptAmnt.Text = "";
            cmbcustname.SelectedIndex = -1;
            txtbilldesc.Text = "";
            txtpono.Text = "";
            TxtProdcode.Focus();
        }

        private void BtnPayment_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                if (TxtBillNo.Text != "" && TxtBillTotal.Text != "" && cmbcustname.Text != "" && txtbilldesc.Text != "")
                {
                    DialogResult dr = MessageBox.Show("Do you want to Generate Bill?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        

                        for (int i = 0; i < LVFO.Items.Count; i++)
                        {
                            SqlCommand cmd1 = new SqlCommand("INSERT INTO [Billing].[dbo].[BillProductMaster]([Bill_No],[Bill_Run_Date],[ProductID],[Product_Qty],[Product_total_Amt],[Free],[Product_Per_rate])VALUES('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString("MM-dd-yyyy") + "','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[2].Text + "','" +Convert.ToDouble(LVFO.Items[i].SubItems[5].Text) + "','" + LVFO.Items[i].SubItems[3].Text + "','" +Convert.ToDouble(LVFO.Items[i].SubItems[4].Text) + "')", con);
                            cmd1.ExecuteNonQuery();

                            //total = total + Convert.ToDouble(LVFO.Items[i].SubItems[4].Text);
                            //Double multi = 0;
                            //multi = (Convert.ToDouble(LVFO.Items[i].SubItems[4].Text) * (Convert.ToDouble(LVFO.Items[i].SubItems[5].Text))) / 100;
                            //vat = vat + multi;

                        }
                        String cmp="";
                        //for (int i = 0; i < cmpy.Length; i++)
                        //{
                        //    if (i == 0)
                        //    {
                        //        cmp = cmpy[i];
                        //    }
                        //    else
                        //    {
                        //        cmp = cmp + "," + cmpy[i];
                        //    }
                        //}


                        SqlCommand cmd = new SqlCommand("INSERT INTO [Billing].[dbo].[BillMaster] ([Bill_No] ,[Bill_Date],[ClientID],[PO_No],[Bill_Amt],[Bill_vat_Amt],[Bill_Net_Amt],[CompanyID])VALUES ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString("MM-dd-yyyy") + "','" + cmbcustname.SelectedValue + "','" + txtpono.Text + "','" + Convert.ToDouble(TxtBillTotal.Text) + "','" + Convert.ToDouble(TxtVAT16Amnt.Text) + "','" + Convert.ToDouble(TxtVATExmptAmnt.Text) + "','" + cmp + "')", con);
                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("INSERT INTO [Billing].[dbo].[PaymentMaster] ([Bill_No],[PaymentStatus])VALUES ('" + TxtBillNo.Text + "','Pending')", con);
                        cmd.ExecuteNonQuery();
                        LVFO.Items.Clear();
                        generatePDF();
                        clearfirst();
                        TxtBillTotal.Text = "";
                        TxtVAT16Amnt.Text = "";
                        TxtVATExmptAmnt.Text = "";
                        txtpono.Text = "";
                        getsr();
                        cmbcustname.SelectedIndex = -1;
                        txtbilldesc.Text = "";
                        TxtProdcode.Focus();

                       
                    }
                }
                else
                {
                    MessageBox.Show("please fill all information");
                }
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

        private void generatePDF()
        {
            try
            {
                //con.Open();
                Document document1;
                PdfWriter Writer;
                document1 = new Document(PageSize.A4);
                Writer = PdfWriter.GetInstance(document1, new FileStream("C:\\report\\BillInoiceNo" + TxtBillNo.Text + ".pdf", FileMode.Create));

                Paragraph para1;

                para1 = new Paragraph();

                iTextSharp.text.Font _fontitalic = FontFactory.GetFont("Verdana", 16, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                iTextSharp.text.Font _font24normal = FontFactory.GetFont("Verdana", 20, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                iTextSharp.text.Font _font24 = FontFactory.GetFont("Verdana", 9, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                iTextSharp.text.Font _fontsize24 = FontFactory.GetFont("Verdana", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                iTextSharp.text.Font _fontunder = FontFactory.GetFont("Verdana", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                iTextSharp.text.Font _fontsmall = FontFactory.GetFont("Verdana", 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                iTextSharp.text.Font _font1 = FontFactory.GetFont("Verdana", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                iTextSharp.text.Font _font2 = FontFactory.GetFont("Verdana", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                Paragraph line = new Paragraph(Environment.NewLine+"_____________________________________________________________________________________________________________________________", _fontsmall);


                para1.Alignment = Element.ALIGN_CENTER;
           
                para1.Alignment = Element.ALIGN_CENTER;

                Paragraph paragraph3 = new Paragraph();
                paragraph3.Alignment = Element.ALIGN_RIGHT;
                paragraph3.Add("Date:" + Convert.ToDateTime(TxtRundate.Text).ToString("dd-MMM-yyyy"));

                Paragraph paragraph2;
                paragraph2 = new Paragraph();
                paragraph2.Alignment = Element.ALIGN_CENTER;

                SqlCommand cmd = new SqlCommand("select * from OwnerMaster", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Phrase ppp = new Phrase("!! Shree Ganeshay Namah: !!" + Environment.NewLine, _fontsmall);
                Phrase q = new Phrase(dt.Rows[0][1].ToString() + Environment.NewLine, _font24normal);
                Phrase q1 = new Phrase(dt.Rows[0][2].ToString() + Environment.NewLine, _font1);
                Phrase q2 = new Phrase("Contact No: " + dt.Rows[0][3].ToString() + Environment.NewLine, _font1);
                Phrase q3 = new Phrase("G.S.T. TIN No: " + dt.Rows[0][4].ToString() + ". FOOD LICENCE NO: " + dt.Rows[0][5].ToString() + Environment.NewLine, _font1);

                Phrase p = new Phrase(Environment.NewLine + "TAX INVOICE" + Environment.NewLine, _fontunder);



                paragraph2.Add(p);
                //Phrase ps, ph, pss, phh;
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



                para1.Add(ppp);
                para1.Add(q);
                para1.Add(q1);
                para1.Add(q2);
                para1.Add(q3);
                para1.Add(p);

                HeaderFooter pdfHeader = new HeaderFooter(para1, false);
                pdfHeader.Alignment = Element.ALIGN_CENTER;
                pdfHeader.Border = iTextSharp.text.Rectangle.NO_BORDER;
                document1.Header = pdfHeader;
                HeaderFooter footer = new HeaderFooter(new Phrase("Printed On: " + DateTime.Now + "                                                                                                                                                                               Page No: ", _fontsmall), true);
                footer.Alignment = Element.ALIGN_CENTER;
                footer.Border = iTextSharp.text.Rectangle.NO_BORDER;
                document1.Footer = footer;

                document1.Open();


                cmd = new SqlCommand("select b.*,c.On_Bill_desc,c.ClientName,c.TinNo from BillMaster b inner join ClientMaster c on c.ClientID=b.ClientID where b.Bill_No='" + TxtBillNo.Text + "'", con);
                sda = new SqlDataAdapter(cmd);
                DataTable dt2 = new DataTable();
                sda.Fill(dt2);

                PdfPTable table = new PdfPTable(2);

                table.DefaultCell.BorderColor = new iTextSharp.text.Color(System.Drawing.Color.White);
                //table.TotalWidth = 700f;
                //table.LockedWidth = true;
                table.DefaultCell.HorizontalAlignment = 0;

                Phrase ppp1 = new Phrase("INVOICE TO:    ", _font2);
                Phrase prs = new Phrase(Environment.NewLine + dt2.Rows[0][8].ToString() + Environment.NewLine +"Tin No: "+ dt2.Rows[0][10].ToString(), _font1);
                //Phrase ppp2  = new Phrase(dt2.Rows[0][3].ToString(), _fontsize24);
                Phrase pppp1 = new Phrase("INVOICE NO: ", _font2);
                Phrase pppp3 = new Phrase("INVOICE DATE: ", _font2);
                Phrase pppp5 = new Phrase("PURCHASE ORDER NO: ", _font2);

                Phrase Ino = new Phrase(dt2.Rows[0][0].ToString(), _font1);
                Phrase Idt = new Phrase(Convert.ToDateTime(dt2.Rows[0][1].ToString()).ToString("dd-MM-yyyy"), _font1);
                Phrase po = new Phrase(dt2.Rows[0][3].ToString(), _font1);
                Phrase ppp6 = new Phrase(dt2.Rows[0][8].ToString(), _font1);
                Phrase ppp7 = new Phrase(dt2.Rows[0][8].ToString(), _font1);

                Paragraph prr = new Paragraph();
                prr.Add(ppp1);
                prr.Add(prs);
                table.AddCell(prr);

                //table.AddCell(ppp2);


                PdfPTable tablemid = new PdfPTable(2);
                tablemid.DefaultCell.BorderColor = new iTextSharp.text.Color(System.Drawing.Color.White);
                float[] widths = new float[] { 2f, 1f };
                tablemid.SetWidths(widths);
                tablemid.DefaultCell.HorizontalAlignment = 2;



                PdfPCell cell11 = new PdfPCell(pppp1);
                cell11.BorderColor = iTextSharp.text.Color.WHITE;
                cell11.HorizontalAlignment = 2;
                tablemid.AddCell(cell11);

                tablemid.AddCell(Ino);

                PdfPCell cell12 = new PdfPCell(pppp3);
                cell12.BorderColor = iTextSharp.text.Color.WHITE;
                cell12.HorizontalAlignment = 2;
                tablemid.AddCell(cell12);


                tablemid.AddCell(Idt);

                PdfPCell cell13 = new PdfPCell(pppp5);
                cell13.HorizontalAlignment = 2;
                cell13.BorderColor = iTextSharp.text.Color.WHITE;
                tablemid.AddCell(cell13);
                tablemid.AddCell(po);
                table.AddCell(tablemid);

                document1.Add(table);



                cmd = new SqlCommand("select p.ProductID,p.Product_Name,p.Product_Price,bp.Product_Qty,bp.Free,bp.Product_per_Rate, bp.Product_total_Amt from BillProductMaster bp inner join ProductMaster p on p.ProductID=bp.ProductID where Bill_No='" + TxtBillNo.Text + "'", con);
                sda = new SqlDataAdapter(cmd);
                DataTable data = new DataTable();
                sda.Fill(data);
                if (data.Rows.Count > 0)
                {
                    PdfPTable table1 = new PdfPTable(8);
                    table1.DefaultCell.BorderColor = new iTextSharp.text.Color(System.Drawing.Color.Black);
                    //table1.TotalWidth = document1.PageSize.Width;
                    float[] width = new float[] { 10f, 37f, 50f, 20f, 15f, 12f, 25f, 28f };
                    table1.SetWidths(width);
                    table1.DefaultCell.HorizontalAlignment = 2;
                    Phrase no = new Phrase("No.", _font2);
                    Phrase bar = new Phrase("Barcode No", _font2);
                    Phrase prod1 = new Phrase("Product Description", _font2);
                    Phrase prod2 = new Phrase("MRP", _font2);
                    Phrase prod3 = new Phrase("Qty (Pkt)", _font2);
                    Phrase prod4 = new Phrase("Free", _font2);
                    Phrase prod5 = new Phrase("Unit Price", _font2);
                    Phrase prod6 = new Phrase("Amount (Rs.)", _font2);

                    PdfPCell cno = new PdfPCell(no);
                    cno.BorderColor = iTextSharp.text.Color.BLACK;
                    cno.HorizontalAlignment = 1;
                    table1.AddCell(cno);

                    PdfPCell cbar = new PdfPCell(bar);
                    cbar.BorderColor = iTextSharp.text.Color.BLACK;
                    cbar.HorizontalAlignment = 1;
                    table1.AddCell(cbar);

                    PdfPCell c1 = new PdfPCell(prod1);
                    c1.BorderColor = iTextSharp.text.Color.BLACK;
                    c1.HorizontalAlignment = 0;
                    table1.AddCell(c1);

                    PdfPCell c2 = new PdfPCell(prod2);
                    c2.BorderColor = iTextSharp.text.Color.BLACK;
                    c2.HorizontalAlignment = 2;
                    table1.AddCell(c2);

                    PdfPCell c3 = new PdfPCell(prod3);
                    c3.BorderColor = iTextSharp.text.Color.BLACK;
                    c3.HorizontalAlignment = 1;
                    table1.AddCell(c3);

                    PdfPCell c4 = new PdfPCell(prod4);
                    c4.BorderColor = iTextSharp.text.Color.BLACK;
                    c4.HorizontalAlignment = 1;
                    table1.AddCell(c4);

                    PdfPCell c5 = new PdfPCell(prod5);
                    c5.BorderColor = iTextSharp.text.Color.BLACK;
                    c5.HorizontalAlignment = 2;
                    table1.AddCell(c5);

                    PdfPCell c6 = new PdfPCell(prod6);
                    c6.BorderColor = iTextSharp.text.Color.BLACK;
                    c6.HorizontalAlignment = 2;
                    table1.AddCell(c6);

                    for (int i = 0; i < 8; i++)
                    {
                        table1.AddCell("");
                    }
                    Int16 count = 1;
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        Phrase pno = new Phrase(count.ToString(), _fontsize24);
                        PdfPCell cc = new PdfPCell(pno);
                        cc.BorderColor = iTextSharp.text.Color.BLACK;
                        cc.HorizontalAlignment = 1;
                        table1.AddCell(cc);
                        count++;

                        for (int j = 0; j < data.Columns.Count; j++)
                        {

                            if (j == 1)
                            {
                                Phrase prod11 = new Phrase(data.Rows[i][j].ToString(), _fontsize24);
                                PdfPCell c = new PdfPCell(prod11);
                                c.BorderColor = iTextSharp.text.Color.BLACK;
                                c.HorizontalAlignment = 0;
                                table1.AddCell(c);
                            }
                            else if (j == 3 || j == 4 || j==0)
                            {
                                Phrase prod11 = new Phrase(data.Rows[i][j].ToString(), _fontsize24);
                                PdfPCell c = new PdfPCell(prod11);
                                c.BorderColor = iTextSharp.text.Color.BLACK;
                                c.HorizontalAlignment = 1;
                                table1.AddCell(c);
                            }
                            else
                            {
                                Phrase prod11 = new Phrase(data.Rows[i][j].ToString(), _fontsize24);
                                PdfPCell c = new PdfPCell(prod11);
                                c.BorderColor = iTextSharp.text.Color.BLACK;
                                c.HorizontalAlignment = 2;
                                table1.AddCell(c);
                            }
                        }
                    }
                    for (int i = 0; i < 8; i++)
                    {
                        table1.AddCell("");
                    }
                    document1.Add(table1);
                }

                PdfPTable ftable = new PdfPTable(4);
                ftable.DefaultCell.BorderColor = new iTextSharp.text.Color(System.Drawing.Color.White);
                //ftable.TotalWidth = document1.PageSize.Width;
                float[] widthh = new float[] { 10f, 85f, 72f, 28f };
                ftable.SetWidths(widthh);
                ftable.DefaultCell.HorizontalAlignment = 2;

                Phrase fprod1 = new Phrase("Total: "+txttotqty.Text+"        Total Amount", _font2);
                Phrase fprod2 = new Phrase("Vat (5%) Amount", _font2);
                Phrase fprod3 = new Phrase("Net Amount", _font2);

                Phrase fans1 = new Phrase(Math.Round(Convert.ToDouble(TxtBillTotal.Text),2).ToString("N2"),_font2);
                Phrase fans2 = new Phrase(Math.Round(Convert.ToDouble(TxtVAT16Amnt.Text),2).ToString("N2"), _font2);
                Phrase fans3 = new Phrase(Math.Round(Convert.ToDouble(TxtVATExmptAmnt.Text),2).ToString("N2"), _font2);

                for (int i = 0; i < 2; i++)
                    ftable.AddCell("");

                ftable.AddCell(fprod1);
                ftable.AddCell(fans1);

                for (int i = 0; i < 2; i++)
                    ftable.AddCell("");

                ftable.AddCell(fprod2);
                ftable.AddCell(fans2);

               
                    ftable.AddCell("");
                

              

                ChangeNumbersToWords sh = new ChangeNumbersToWords();
                String s1 = Math.Round(Convert.ToDouble(TxtVATExmptAmnt.Text), 2).ToString("########.00");
                string[] words = s1.Split('.');


                string str = sh.changeToWords(words[0]);
                string str1 = sh.changeToWords(words[1]);
                if (str1 == " " || str1 == null || words[1]=="00")
                {
                    str1 = "Zero ";
                }
                String inword = "Rs. in words: " + str + "and " + str1 + "Paise Only";
                Phrase inwrd = new Phrase(inword, _font2);

                ftable.AddCell(inwrd);
                ftable.AddCell(fprod3);
                ftable.AddCell(fans3);
                
                for (int i = 0; i < 4; i++)
                    ftable.AddCell("");

                document1.Add(ftable);
                document1.Add(line);

                Phrase w=new Phrase("Terms & Conditions:"+Environment.NewLine,_font2);
                Phrase w1 = new Phrase(Environment.NewLine+"(1) WARRANTEED: We hearby Certify that Food Monitored in this Invoice is warranted to be of the Nature and Quantity which Porposes to be.", _fontsmall);
                Phrase w2 = new Phrase(Environment.NewLine+"(2) Do not use goods if packet is not Intact.", _fontsmall);
                Phrase w3 = new Phrase(Environment.NewLine+"(3) Goods Sold are not Returnable.",_fontsmall);
                Phrase w4 = new Phrase(Environment.NewLine+"(3) All Warranty Subject to Respective Company's Policy.", _fontsmall);
                Phrase w5 = new Phrase(Environment.NewLine+Environment.NewLine+"***SUBJECT TO VADODARA JURISDICTION***", _fontsmall);

                Phrase w6 = new Phrase("Receiver's Sign & Stamp", _font2);
                Phrase w7 = new Phrase("For, " + dt.Rows[0][1].ToString() + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + "Authorized Signatory", _font2);

                PdfPTable wtable = new PdfPTable(3);
                wtable.DefaultCell.BorderColor = new iTextSharp.text.Color(System.Drawing.Color.White);
                //ftable.TotalWidth = document1.PageSize.Width;
                float[] wwidth = new float[] { 33, 33, 33 };
                wtable.SetWidths(wwidth);
                wtable.DefaultCell.HorizontalAlignment = 1;

                Paragraph ph = new Paragraph();
                ph.Add(w);
                ph.Add(w1);
                ph.Add(w2);
                //ph.Add(w3);
                ph.Add(w4);
                ph.Add(w5);

                PdfPCell wc1 = new PdfPCell(ph);
                wc1.BorderColor = iTextSharp.text.Color.WHITE;
                wc1.HorizontalAlignment = 0;
                wtable.AddCell(wc1);

                PdfPCell wc2 = new PdfPCell(w6);
                wc2.BorderColor = iTextSharp.text.Color.WHITE;
                wc2.HorizontalAlignment = 1;
                wc2.VerticalAlignment = Element.ALIGN_BOTTOM;
                wtable.AddCell(wc2);

                PdfPCell wc3 = new PdfPCell(w7);
                wc3.BorderColor = iTextSharp.text.Color.WHITE;
                wc3.HorizontalAlignment = 2;
                wc3.VerticalAlignment = Element.ALIGN_BOTTOM;
                wtable.AddCell(wc3);

                document1.Add(wtable);
                document1.Close();
                System.Diagnostics.Process.Start("C:\\report\\BillInoiceNo" + TxtBillNo.Text + ".pdf");
                //String pathToExecutable = "AcroRd32.exe";

                String sReport = "C:\\report\\BillInoiceNo" + TxtBillNo.Text + ".pdf"; //'Complete name/path of PDF file
              


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally
            {
               
            }
        }

       

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            clearfirst();
            TxtProdcode.Focus();
        }

        private void TxtPrice_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F2)
            {
                flag = 1;
                TxtPrice.ReadOnly = false;
            }

        }

        private void TxtPrice_MouseClick(object sender, MouseEventArgs e)
        {
            flag = 1;
            TxtPrice.ReadOnly = false;
        }

        private void TxtPrice_Validated(object sender, EventArgs e)
        {
            if (TxtPrice.Text != "" && flag == 1)
            {
                double d1 = (Convert.ToDouble(TxtPrice.Text) / (Convert.ToDouble(TxtVATCode.Text) + 100)) * 100;
                //double d2 = Convert.ToDouble(TxtPrice.Text) - d1;
                TxtPrice.Text = d1.ToString("N2");
                double total = Convert.ToInt32(TxtQty.Text) * Convert.ToDouble(TxtPrice.Text);
                TxtTotalAmount.Text = Math.Round(total, 2).ToString("N2");
                TxtPrice.ReadOnly = true;
            }
            flag = 0;
        }

        private void FloatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClientRegistration clnt = new ClientRegistration();
            clnt.StartPosition = FormStartPosition.CenterParent;
            clnt.Show();
        }

        private void TxtRundate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                TxtRundate.ReadOnly = false;
            }
        }

        private void TxtRundate_Validated(object sender, EventArgs e)
        {
            TxtRundate.ReadOnly = true;
        }

        private void TxtProductDscrption_Validated(object sender, EventArgs e)
        {
            {
                try
                {

                    if (cmbcustname.Text != "")
                    {
                        if (TxtProductDscrption.Text != "")
                        {
                            String qry = "select p.*,cp.MarginInPer from ProductMaster p inner join ClientProductMargin cp on cp.ProductID=p.ProductID inner join CompanyMaster cm on cm.CompanyID=p.CompanyID where p.Product_Name='" + TxtProductDscrption.Text + "' and cp.ClientID='" + cmbcustname.SelectedValue + "' and (";
                            int i = 0;
                            con.Open();
                            foreach (var itemChecked in chklistcompany.CheckedItems)
                            {
                                string itemName = itemChecked.ToString();
                                SqlCommand cmd = new SqlCommand("select companyID from CompanyMaster where Companyname='" + itemName + "'", con);
                                String companyid = cmd.ExecuteScalar().ToString();
                                if (i == 0)
                                {
                                    qry = qry + "p.CompanyID='" + companyid + "'";
                                    i++;
                                }
                                else
                                {
                                    qry = qry + " or p.CompanyID='" + companyid + "'";
                                    i++;
                                }
                            }
                            con.Close();
                            qry = qry + ")";

                            SqlCommand cmd1 = new SqlCommand(qry, con);
                            SqlDataAdapter sda = new SqlDataAdapter(cmd1);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                TxtProdcode.Text = dt.Rows[0][2].ToString();
                                Double temp = Convert.ToDouble(dt.Rows[0][4].ToString()) * (Convert.ToDouble(dt.Rows[0][6].ToString()) / 100);
                                double t1 = (Convert.ToDouble(dt.Rows[0][4].ToString()) - temp);
                                double tempvat = (t1 * Convert.ToDouble(dt.Rows[0][5].ToString())) / (100 + Convert.ToDouble(dt.Rows[0][5].ToString()));
                                TxtPrice.Text = Math.Round((t1 - tempvat), 2).ToString("N2");
                                TxtVATCode.Text = dt.Rows[0][5].ToString();
                                txtprodprice.Text = dt.Rows[0][4].ToString();
                                if (TxtQty.Text == "")
                                {
                                    TxtQty.Text = "1";
                                    txtfree.Text = "0";
                                }
                                else
                                {
                                    TxtQty.Text = "";
                                    TxtQty.SelectedText = "1";
                                }
                            }
                            else
                            {
                                MessageBox.Show("please choose valid product");
                                TxtProdcode.Text = "";
                                TxtProdcode.Focus();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Select Client First");
                        cmbcustname.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message);
                    TxtProdcode.Text = "";
                    TxtProdcode.Focus();
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void chklistcompany_Validated(object sender, EventArgs e)
        {
            autoreaderbind();
        }

        private void Label14_Click(object sender, EventArgs e)
        {

        }

       

    }
}