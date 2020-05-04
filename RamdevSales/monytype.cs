using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace RamdevSales
{
    public partial class monytype : Form
    {
        Connection sql = new Connection();
        DataTable dt = new DataTable();
        string a;
        Printing prn = new Printing();
        public string constr = ConfigurationManager.ConnectionStrings["qry"].ToString();
        AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
        DataTable options = new DataTable();
        public monytype()
        {
            InitializeComponent();
        }

        public monytype(DefaultPOS defaultPOS)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.defaultPOS = defaultPOS;
           // defaultPOS.savedata();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                rbcash.Checked = true;
                rbcash.Focus();
                pnlcash.Visible = true;
                pnlcard.Visible = false;
                txtcastt.Focus();
            }
            if (keyData == Keys.F2)
            {
                rbcredit.Checked = true;
                rbcredit.Focus();
                pnlcash.Visible = false;
                pnlcard.Visible = true;
                txtamount.Focus();
            }
            if (keyData == Keys.F3)
            {
                rbdebit.Checked = true;
                rbdebit.Focus();
                pnlcash.Visible = false;
                pnlcard.Visible = true;
                txtamount.Focus();
            }
            if (keyData == Keys.Escape)
            {
                this.Close();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void btnok_Click(object sender, EventArgs e)
        {
            if (DefaultPOS.str == "")
            {
                lblid.Text = ListPOS.iid;
            }
            else
            {
                lblid.Text = DefaultPOS.str;
            }
            
            
            try
            {
                  
                if (rbcash.Checked == true)
                {
                    a = rbcash.Text;
                }
                if (rbcredit.Checked == true)
                {
                    a = rbcredit.Text;
                }
                if (rbdebit.Checked == true)
                {
                    a = rbdebit.Text;
                }
               // sql.execute("update printdata set paymenttype='" + a + "' where billno='" + lblid.Text + "'");
               // sql.execute("update itemdetails set paymenttype='" + a + "' where billno='" + lblid.Text + "'");
                defaultPOS.savedata();
                sql.execute("Update BillPOSMaster set Terms='"+a+"', bankname='" + txtbankname.Text +"',cardnumbar='"+txtcardnumber.Text+"',cardtype='"+txtcardtype.Text+"',expirydate='"+txtexpiry.Text+"',apprcode='"+txtapprcode.Text+"',refno='"+txtrefno.Text+"',amountrs='"+txtamount.Text+"',invno='"+txtinvno.Text+"',cardholdername='"+txtcardholdername.Text+"',cashtendered='"+txtcastt.Text+"',change='"+txtchange.Text+ "'where billid='" + lblid.Text + "'");
                this.Close();
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[9]["p"].ToString() == "True")
                    {
                        print();
                    }
                    else
                    {
                        MessageBox.Show("You don't have Permission To Print");
                        return;
                    }
                }
                defaultPOS.clearall();
                
          
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void PrintToPrinter()
        {
            PrintReport(System.Windows.Forms.Application.StartupPath + "\\CrystalReportbill.rpt",
                "Send To OneNote 2010");
        }
        private void PrintReport(string reportPath, string PrinterName)
        {
            var dialog = new PrintDialog();
            CrystalDecisions.CrystalReports.Engine.ReportDocument rptDoc =
                                new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            rptDoc.Load("Z:/Hitesh/DALY BACKUP/d/Project/software/POS/1-7-16/pos/pos/pos/CrystalReportbill.rpt");

            CrystalDecisions.Shared.PageMargins objPageMargins;
            objPageMargins = rptDoc.PrintOptions.PageMargins;
            objPageMargins.bottomMargin = 100;
            objPageMargins.leftMargin = 100;
            objPageMargins.rightMargin = 100;
            objPageMargins.topMargin = 100;
            rptDoc.PrintOptions.ApplyPageMargins(objPageMargins);
            rptDoc.PrintOptions.PrinterName = dialog.PrinterSettings.PrinterName;
            //  rptDoc.PrintOptions.PrinterName ="Hp LeserJet Professional M1136 MFP";
            rptDoc.PrintToPrinter(1, false, 0, 0);
          
        }


        public void bankname()
        {
            SqlDataReader dReader;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = constr;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            // (dgvmst_subpat.DataSource as DataTable).DefaultView.RowFilter = string.Format("p_name like '%{0}%' ", txt_name.Text);
            cmd.CommandText = "select bankname from BillPOSMaster where isactive=1 order by bankname";
            conn.Open();
            dReader = cmd.ExecuteReader();
            if (dReader.HasRows == true)
            {
                while (dReader.Read())
                    namesCollection.Add(dReader["bankname"].ToString());

            }
            else
            {
                //MessageBox.Show("Data not found");
            }
            dReader.Close();

            txtbankname.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtbankname.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtbankname.AutoCompleteCustomSource = namesCollection;
        }
        public void cardtype()
        {
            SqlDataReader dReader;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = constr;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            // (dgvmst_subpat.DataSource as DataTable).DefaultView.RowFilter = string.Format("p_name like '%{0}%' ", txt_name.Text);
            cmd.CommandText = "select cardtype from BillPOSMaster where isactive=1 order by cardtype";
            conn.Open();
            dReader = cmd.ExecuteReader();
            if (dReader.HasRows == true)
            {
                while (dReader.Read())
                    namesCollection.Add(dReader["cardtype"].ToString());

            }
            else
            {
              //  MessageBox.Show("Data not found");
            }
            dReader.Close();

            txtcardtype.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtcardtype.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtcardtype.AutoCompleteCustomSource = namesCollection;
        }
        DataTable userrights = new DataTable();
        private void monytype_Load(object sender, EventArgs e)
        {
            //txtmony.Focus();
            txttotal.Text = DefaultPOS.gtotalamt;
            txtamount.Text = DefaultPOS.gtotalamt;
            txtcastt.Text = "0";
            this.ActiveControl = rbcash;
            pnlcard.Visible = false;
            bankname();
            cardtype();
            option = sql.getdataset("select * from options");
            userrights = sql.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
        }

      

        private void txtmony_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                
                if (e.KeyData == Keys.D1)
                {
                    rbcash.Checked = true;
                    pnlcash.Visible = true;
                    pnlcard.Visible = false;
                }
                if (e.KeyData == Keys.D2)
                {
                    rbcredit.Checked = true;
                    pnlcash.Visible = false;
                    pnlcard.Visible = true;
                }
                if (e.KeyData == Keys.D3)
                {
                    rbdebit.Checked = true;
                    pnlcash.Visible = false;
                    pnlcard.Visible = true;
                }
                if (e.KeyData == Keys.Escape)
                {
                    this.Close();
                }
                if (e.KeyData == Keys.Enter)
                {
                  //  lblid.Text = pos.str;
                    try
                    {

                        if (rbcash.Checked == true)
                        {
                            a = rbcash.Text;
                        }
                        if (rbcredit.Checked == true)
                        {
                            a = rbcredit.Text;
                        }
                        if (rbdebit.Checked == true)
                        {
                            a = rbdebit.Text;
                        }
                        sql.execute("update printdata set paymenttype='" + a + "' where billno='" + lblid.Text + "'");
                        sql.execute("update itemdetails set paymenttype='" + a + "' where billno='" + lblid.Text + "'");
                        this.Close();
                    
                    //    lblkey.Text = pos.key;
                        if (lblkey.Text == "F3")
                        {
                 //           cyreports cy = new cyreports();
                 //           cy.Show();
                        }
                        if (lblkey.Text == "F6")
                        {
                   //         cyreports cy = new cyreports();
                  //          cy.Show();
                  //          cy.Hide();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rbcash_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    
                    pnlcash.Visible = true;
                    pnlcard.Visible = false;
                    txtcastt.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtcastt_TextChanged(object sender, EventArgs e)
        {
            try
            {
              
                if (txtcastt.Text == "")
                {
                    txtcastt.Text = "0";
                }
                else
                {
                    double a = Convert.ToDouble(txttotal.Text) - Convert.ToDouble(txtcastt.Text);
                    a = Math.Round(a);
                    txtchange.Text = Convert.ToString(a);
                }        
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtcastt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.D1)
            {
                rbcash.Checked = true;
                pnlcash.Visible = true;
                pnlcard.Visible = false;
            }
            if (e.KeyData == Keys.D2)
            {
                rbcredit.Checked = true;
                pnlcash.Visible = false;
                pnlcard.Visible = true;
            }
            if (e.KeyData == Keys.D3)
            {
                rbdebit.Checked = true;
                pnlcash.Visible = false;
                pnlcard.Visible = true;
            }
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyData == Keys.Enter)
            {
                btnok.Focus();
            }
        }

        private void rbcredit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                   
                    pnlcash.Visible = false;
                    pnlcard.Visible = true;
                    lblbankname.Text = "Bank Name";
                    txtamount.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rbdebit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                   
                    pnlcash.Visible = false;
                    pnlcard.Visible = true;
                    lblbankname.Text = "E-Wallet Name";
                    txtamount.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtbankname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {

                txtcardnumber.Focus();
            }
        }

        private void txtcardnumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {

                txtcardtype.Focus();
            }
        }

        private void txtcardtype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {

                txtexpiry.Focus();
            }
        }

        private void txtexpiry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {

                txtapprcode.Focus();
            }
        }

        private void txtapprcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {

                txtrefno.Focus();
            }
        }

        private void txtrefno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {

                txtinvno.Focus();
            }
        }

        private void txtinvno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {

                txtcardholdername.Focus();
            }
        }

        private void txtcardholdername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {

                btnok.Focus();
            }
        }
        DataTable option = new DataTable();
        private DefaultPOS defaultPOS;
        private void txtamount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {

                txtbankname.Focus();

            }
            if (e.KeyData == Keys.D1)
            {
                rbcash.Checked = true;
                rbcash.Focus();
                pnlcash.Visible = true;
                pnlcard.Visible = false;
                txtcastt.Focus();
            }
            if (e.KeyData == Keys.D2)
            {
                rbcredit.Checked = true;
                rbcredit.Focus();
                pnlcash.Visible = false;
                pnlcard.Visible = true;
                txtamount.Focus();
            }
            if (e.KeyData == Keys.D3)
            {
                rbdebit.Checked = true;
                rbdebit.Focus();
                pnlcash.Visible = false;
                pnlcard.Visible = true;
                txtamount.Focus();
            }
        }
        string finalbillno;
        private void print()
        {
            ChangeNumbersToWords sh = new ChangeNumbersToWords();
            String s1 = Math.Round(Convert.ToDouble(DefaultPOS.gtotalamt), 2).ToString("########.00");
            string[] words = s1.Split('.');


            string str = sh.changeToWords(words[0]);
            string str1 = sh.changeToWords(words[1]);
            if (str1 == " " || str1 == null || words[1] == "00")
            {
                str1 = "Zero ";
            }
            String inword = "In words: " + str + "and " + str1 + "Paise Only";
            DataTable multyprint1 = sql.getdataset("select requirprintpopupinpos from Options");
            if (Convert.ToBoolean(multyprint1.Rows[0]["requirprintpopupinpos"].ToString()) == true)
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Print Bill?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                string bid;
                if (DefaultPOS.str == "")
                {
                    bid = ListPOS.iid;
                }
                else
                {
                    bid = DefaultPOS.str;
                }
                // DataTable client = sql.getdataset("select * from clientmaster where isactive=1 and clientID='" + cmbcustname.SelectedValue + "'");
                DataTable dt1 = sql.getdataset("select * from BillPOSMaster WHERE isactive=1 and Billid='" + bid + "'");
                DataTable dt2 = sql.getdataset("select * from BillPOSProductMaster WHERE isactive=1 and Billid='" + bid + "'");


                DataTable dt3 = sql.getdataset("select * from company WHERE isactive=1");
                DataTable dt4 = sql.getdataset("select sum(amount)-sum(discountamt) as basicamount,sum(sgst) as sgst,sum(cgst) as cgst,sum(amount)+sum(sgst)+sum(cgst)-sum(discountamt) as total, igst,sum(Addtaxamt) as Addtaxamt,sum(Addtax) as Addtax  from BillPOSProductMaster WHERE isactive=1 and Billid='" + bid + "' group by igst");
                string taxable = "Taxable Amt" + Environment.NewLine, cgstper = "CGST % " + Environment.NewLine, cgstamt = "CGST AMT" + Environment.NewLine, sgstper = "SGST %" + Environment.NewLine, sgstamt = "SGST AMT" + Environment.NewLine, totalamt = "Total AMT" + Environment.NewLine, addper = "AddTax%" + Environment.NewLine, addamt = "AddAmt" + Environment.NewLine;
                double cgst = 0, sgst = 0, basicamt = 0, nettotal = 0, Addtax = 0; ;
                for (int i = 0; i < dt4.Rows.Count; i++)
                {
                    taxable += Environment.NewLine + dt4.Rows[i]["basicamount"].ToString();
                    basicamt += Convert.ToDouble(dt4.Rows[i]["basicamount"].ToString());

                    cgstper += Environment.NewLine + (Convert.ToDouble(dt4.Rows[i]["igst"].ToString()) / 2).ToString();
                    cgstamt += Environment.NewLine + dt4.Rows[i]["cgst"].ToString();
                    cgst += Convert.ToDouble(dt4.Rows[i]["cgst"].ToString());

                    sgstper += Environment.NewLine + (Convert.ToDouble(dt4.Rows[i]["igst"].ToString()) / 2).ToString();
                    sgstamt += Environment.NewLine + dt4.Rows[i]["sgst"].ToString();
                    sgst += Convert.ToDouble(dt4.Rows[i]["sgst"].ToString());

                    addper += Environment.NewLine + (Convert.ToDouble(dt4.Rows[i]["Addtax"].ToString()) / 2).ToString();
                    addamt += Environment.NewLine + dt4.Rows[i]["Addtaxamt"].ToString();
                    Addtax += Convert.ToDouble(dt4.Rows[i]["Addtaxamt"].ToString());

                    totalamt += Environment.NewLine + dt4.Rows[i]["total"].ToString();
                    nettotal += Convert.ToDouble(dt4.Rows[i]["total"].ToString());
                }

                if (Convert.ToBoolean(option.Rows[0]["autoroundoffpos"].ToString()) == true)
                {
                    nettotal = Math.Round(nettotal, 0);
                }
                prn.execute("delete from printing");
                int j = 1;
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    try
                    {
                        if (option.Rows[0]["posbillno"].ToString() == "Continuous")
                        {
                            finalbillno = dt1.Rows[0]["Billid"].ToString();
                        }
                        else
                        {
                            finalbillno = dt1.Rows[0]["billno"].ToString();
                        }
                        DataTable hsn = sql.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + dt2.Rows[i]["ItemName"].ToString() + "'");
                        //   DataTable item = sql.getdataset("select * from ItemTaxMaster where isactive=1 and ProductID='" + hsn.Rows[0]["ProductID"].ToString() + "'");
                        DataTable item = sql.getdataset("select * from TaxSlabMaster where isactive=1 and saletypename='" + DefaultPOS.saletype + "' and Taxslabname='" + hsn.Rows[0]["taxslab"].ToString() + "'");
                        string mrp = sql.ExecuteScalar("select MRP from ProductPriceMaster where isactive=1 and Productid='" + hsn.Rows[0]["Productid"].ToString() + "'");
                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34,T35,T36,T37,T38,T39,T40,T41,T42,T43,T44,T45,T46,T47,T48,T49,T50,T51,T52,T53,T54,T55,T56,T57,T58,T59,T60,T61,T62,T63,T64,T65,T66,T67,T68,T69,T70,T71,T72,T73,T74,T75,T76,T77,T78,T79,T80,T81,T82,T83,T84,T85,T86,T87,T88,T89,T90,T91,T92,T93,T94,T95,T96)VALUES";
                        // qry += "('" + dt3.Rows[0]["CompanyName"].ToString() + "','" + dt3.Rows[0]["Address"].ToString() + "','" + dt3.Rows[0]["Address2"].ToString() + "','" + dt3.Rows[0]["city"].ToString() + "','" + dt3.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Billid"].ToString() + "','" + Convert.ToDateTime(dt1.Rows[0]["BillDate"].ToString()).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(dt1.Rows[0]["BillDate"].ToString()).ToString("HH:mm tt") + "','" + hsn.Rows[0]["Hsn_Sac_Code"].ToString() + "','" + dt2.Rows[i]["ItemName"].ToString() + "','" + dt2.Rows[i]["qty"].ToString() + "','" + dt2.Rows[i]["Rate"].ToString() + "','" + dt2.Rows[i]["Amount"].ToString() + "')";
                        qry += "('" + dt3.Rows[0]["CompanyName"].ToString() + "','" + dt3.Rows[0]["SubName"].ToString() + "','" + dt3.Rows[0]["Address"].ToString() + "','" + dt3.Rows[0]["Address2"].ToString() + "','" + dt3.Rows[0]["City"].ToString() + "','" + dt3.Rows[0]["Phone"].ToString() + "','" + dt3.Rows[0]["Mobile"].ToString() + "','" + dt3.Rows[0]["Email"].ToString() + "','" + dt3.Rows[0]["Website"].ToString() + "','" + dt3.Rows[0]["CSTNo"].ToString() + "','" + dt3.Rows[0]["PANNo"].ToString() + "','" + dt3.Rows[0]["VATNo"].ToString() + "','" + dt3.Rows[0]["DLNo1"].ToString() + "','" + dt3.Rows[0]["DLNo2"].ToString() + "','" + dt3.Rows[0]["DealsIn"].ToString() + "','" + dt3.Rows[0]["Stockist"].ToString() + "','" + dt3.Rows[0]["currency"].ToString() + "','" + dt3.Rows[0]["StartDate"].ToString() + "','" + dt3.Rows[0]["EndDate"].ToString() + "','" + dt3.Rows[0]["MyDSNName"].ToString() + "','" + dt3.Rows[0]["LinkRemote"].ToString() + "','" + dt3.Rows[0]["DBType"].ToString() + "','" + dt3.Rows[0]["Catalyst"].ToString() + "','" + finalbillno + "','" + dt1.Rows[0]["BillDate"].ToString() + "','" + dt1.Rows[0]["Terms"].ToString() + "','" + dt1.Rows[0]["count"].ToString() + "','" + dt1.Rows[0]["totalqty"].ToString() + "','" + dt1.Rows[0]["totalbasic"].ToString() + "','" + dt1.Rows[0]["totaltax"].ToString() + "','" + dt1.Rows[0]["totalnet"].ToString() + "','" + dt1.Rows[0]["disamt"].ToString() + "','" + dt1.Rows[0]["adddisamt"].ToString() + "','" + dt1.Rows[0]["bankname"].ToString() + "','" + dt1.Rows[0]["cardnumbar"].ToString() + "','" + dt1.Rows[0]["cardtype"].ToString() + "','" + dt1.Rows[0]["expirydate"].ToString() + "','" + dt1.Rows[0]["apprcode"].ToString() + "','" + dt1.Rows[0]["refno"].ToString() + "','" + dt1.Rows[0]["amountrs"].ToString() + "','" + dt1.Rows[0]["invno"].ToString() + "','" + dt1.Rows[0]["cardholdername"].ToString() + "','" + dt1.Rows[0]["cashtendered"].ToString() + "','" + dt1.Rows[0]["change"].ToString() + "','" + dt2.Rows[i]["ItemName"].ToString() + "','" + dt2.Rows[i]["Qty"].ToString() + "','" + dt2.Rows[i]["Rate"].ToString() + "','" + dt2.Rows[i]["Amount"].ToString() + "','" + dt2.Rows[i]["Total"].ToString() + "','" + dt2.Rows[i]["igst"].ToString() + "','" + dt2.Rows[i]["Addtax"].ToString() + "','" + dt2.Rows[i]["Discount"].ToString() + "','" + dt2.Rows[i]["Per"].ToString() + "','" + dt2.Rows[i]["SerCharge"].ToString() + "','" + dt2.Rows[i]["PackCharge"].ToString() + "','" + dt2.Rows[i]["RoundOf"].ToString() + "','" + dt2.Rows[i]["NetTotal"].ToString() + "','" + dt2.Rows[i]["CashTendered"].ToString() + "','" + dt2.Rows[i]["Change"].ToString() + "','" + dt2.Rows[i]["sgst"].ToString() + "','" + dt2.Rows[i]["cgst"].ToString() + "','" + hsn.Rows[0]["ProductID"].ToString() + "','" + hsn.Rows[0]["CompanyID"].ToString() + "','" + hsn.Rows[0]["GroupName"].ToString() + "','" + hsn.Rows[0]["Product_Name"].ToString() + "','" + hsn.Rows[0]["Unit"].ToString() + "','" + hsn.Rows[0]["Altunit"].ToString() + "','" + hsn.Rows[0]["Convfactor"].ToString() + "','" + hsn.Rows[0]["Packing"].ToString() + "','" + hsn.Rows[0]["IsBatch"].ToString() + "','" + hsn.Rows[0]["Hsn_Sac_Code"].ToString() + "','" + dt3.Rows[0]["CompanyID"].ToString() + "','" + item.Rows[0]["sgst"].ToString() + "','" + item.Rows[0]["cgst"].ToString() + "','" + item.Rows[0]["additax"].ToString() + "','" + option.Rows[0]["autosaletype"].ToString() + "','" + taxable + "','" + cgstper + "','" + cgstamt + "','" + sgstper + "','" + sgstamt + "','" + totalamt + "','" + basicamt + "','" + cgst + "','" + sgst + "','" + nettotal.ToString("N2") + "','" + dt2.Rows[i]["DiscountAmt"].ToString() + "','" + addper + "','" + addamt + "','" + Addtax + "','" + dt1.Rows[0]["customername"].ToString() + "','" + dt1.Rows[0]["customercity"].ToString() + "','" + dt1.Rows[0]["customermobile"].ToString() + "','" + hsn.Rows[0]["taxslab"].ToString() + "','" + dt1.Rows[0]["billno"].ToString() +"','"+mrp+ "')";
                        prn.execute(qry);
                    }
                    catch
                    {
                    }
                }
                //int j = 1;
                //string perticulars = "", remarks = "", value = "", at = "", plusminus = "", amount = "";
                //for (int i = 0; i < LVCHARGES.Items.Count; i++)
                //{
                //    perticulars += LVCHARGES.Items[i].SubItems[0].Text + Environment.NewLine;
                //    remarks += LVCHARGES.Items[i].SubItems[1].Text + Environment.NewLine;
                //    value += LVCHARGES.Items[i].SubItems[2].Text + Environment.NewLine;
                //    at += LVCHARGES.Items[i].SubItems[3].Text + Environment.NewLine;
                //    plusminus += LVCHARGES.Items[i].SubItems[4].Text + Environment.NewLine;
                //    amount += LVCHARGES.Items[i].SubItems[5].Text + Environment.NewLine;
                //}

                //for (int i = 0; i < LVFO.Items.Count; i++)
                //{
                //    try
                //    {
                //        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34,T35,T36,T37,T38,T39,T40,T41,T42,T43,T44,T45,T46,T47,T48,T49,T50,T51,T52,T53,T54,T55,T56,T57,T58,T59,T60,T61,T62,T63,T64,T65,T66,T67,T68,T69,T70,T71,T72,T73,T74,T75,T76,T77,T78,T79,T80)VALUES";
                //        qry += "('" + j++ + "','" + TxtBillNo.Text + "','" + TxtRundate.Text + "','" + cmbterms.Text + "','" + txtduedate.Text + "','" + cmbcustname.Text + "','" + txtpono.Text + "','" + cmbsaletype.Text + "','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','" + LVFO.Items[i].SubItems[2].Text + "','" + LVFO.Items[i].SubItems[3].Text + "','" + LVFO.Items[i].SubItems[4].Text + "','" + LVFO.Items[i].SubItems[5].Text + "','" + LVFO.Items[i].SubItems[6].Text + "','" + LVFO.Items[i].SubItems[7].Text + "','" + LVFO.Items[i].SubItems[8].Text + "','" + LVFO.Items[i].SubItems[9].Text + "','" + LVFO.Items[i].SubItems[10].Text + "','" + LVFO.Items[i].SubItems[11].Text + "','" + LVFO.Items[i].SubItems[12].Text + "','" + LVFO.Items[i].SubItems[13].Text + "','" + LVFO.Items[i].SubItems[14].Text + "','" + lbltotcount.Text + "','" + lbltotpqty.Text + "','" + txttotaqty.Text + "','" + txttotfree.Text + "','" + lblbasictot.Text + "','" + txttotdiscount.Text + "','" + txttotadis.Text + "','" + txttottax.Text + "','" + txttotaddvat.Text + "','" + txtamt.Text + "','" + txttotservice.Text + "','" + txttotalcharges.Text + "','" + txtroundoff.Text + "','" + TxtBillTotal.Text + "','" + dt1.Rows[0][0].ToString() + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][2].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + dt1.Rows[0][11].ToString() + "','" + dt1.Rows[0][12].ToString() + "','" + dt1.Rows[0][13].ToString() + "','" + inword + "','" + txttransport.Text + "','" + txtdelieveryat.Text + "','" + txtfraight.Text + "','" + txtvehicleno.Text + "','" + txtgrrrno.Text + "','" + txtremarks.Text + "','" + txtweight.Text + "','" + txtskids.Text + "','" + perticulars + "','" + remarks + "','" + value + "','" + at + "','" + plusminus + "','" + amount + "','" + client.Rows[0][0].ToString() + "','" + client.Rows[0][1].ToString() + "','" + client.Rows[0][2].ToString() + "','" + client.Rows[0][3].ToString() + "','" + client.Rows[0][4].ToString() + "','" + client.Rows[0][5].ToString() + "','" + client.Rows[0][6].ToString() + "','" + client.Rows[0][7].ToString() + "','" + client.Rows[0][8].ToString() + "','" + client.Rows[0][9].ToString() + "','" + client.Rows[0][10].ToString() + "','" + client.Rows[0][11].ToString() + "','" + client.Rows[0][12].ToString() + "','" + client.Rows[0][13].ToString() + "')";
                //        prn.execute(qry);
                //    }
                //    catch
                //    {
                //    }
                //}
                //    Print popup = new Print("Pos");
                //  popup.ShowDialog();
                // popup.Dispose();
                DataTable multyprint = sql.getdataset("select multyprintinpos from Options");
                if (Convert.ToBoolean(multyprint.Rows[0]["multyprintinpos"].ToString()) == true)
                {
                    Print popup = new Print("Pos");
                    popup.ShowDialog();
                    popup.Dispose();
                }
                else
                {
                    string strreport = Application.StartupPath + "\\" + "QuickSale.rpt";
                    SQLReport sqlreport = new SQLReport(strreport, "Pos");
                    DataTable bill = sql.getdataset("select defaultbill from Options");
                    if (bill.Rows.Count > 0)
                    {
                        if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                        {
                            sqlreport.Show();
                        }
                        else
                        {
                            //SaleReport sale = new SaleReport(str);
                            sqlreport.Show();
                            sqlreport.Hide();
                        }
                    }
                }
            }
            }
            else
            {
                //DialogResult dr1 = MessageBox.Show("Do you want to Print Bill?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (dr1 == DialogResult.Yes)
                //{
                    string bid;
                    if (DefaultPOS.str == "")
                    {
                        bid = ListPOS.iid;
                    }
                    else
                    {
                        bid = DefaultPOS.str;
                    }
                    // DataTable client = sql.getdataset("select * from clientmaster where isactive=1 and clientID='" + cmbcustname.SelectedValue + "'");
                    DataTable dt1 = sql.getdataset("select * from BillPOSMaster WHERE isactive=1 and Billid='" + bid + "'");
                    DataTable dt2 = sql.getdataset("select * from BillPOSProductMaster WHERE isactive=1 and Billid='" + bid + "'");


                    DataTable dt3 = sql.getdataset("select * from company WHERE isactive=1");
                    DataTable dt4 = sql.getdataset("select sum(amount)-sum(discountamt) as basicamount,sum(sgst) as sgst,sum(cgst) as cgst,sum(amount)+sum(sgst)+sum(cgst)-sum(discountamt) as total, igst,sum(Addtaxamt) as Addtaxamt,sum(Addtax) as Addtax  from BillPOSProductMaster WHERE isactive=1 and Billid='" + bid + "' group by igst");
                    string taxable = "Taxable Amt" + Environment.NewLine, cgstper = "CGST % " + Environment.NewLine, cgstamt = "CGST AMT" + Environment.NewLine, sgstper = "SGST %" + Environment.NewLine, sgstamt = "SGST AMT" + Environment.NewLine, totalamt = "Total AMT" + Environment.NewLine, addper = "AddTax%" + Environment.NewLine, addamt = "AddAmt" + Environment.NewLine;
                    double cgst = 0, sgst = 0, basicamt = 0, nettotal = 0, Addtax = 0; ;
                    for (int i = 0; i < dt4.Rows.Count; i++)
                    {
                        taxable += Environment.NewLine + dt4.Rows[i]["basicamount"].ToString();
                        basicamt += Convert.ToDouble(dt4.Rows[i]["basicamount"].ToString());

                        cgstper += Environment.NewLine + (Convert.ToDouble(dt4.Rows[i]["igst"].ToString()) / 2).ToString();
                        cgstamt += Environment.NewLine + dt4.Rows[i]["cgst"].ToString();
                        cgst += Convert.ToDouble(dt4.Rows[i]["cgst"].ToString());

                        sgstper += Environment.NewLine + (Convert.ToDouble(dt4.Rows[i]["igst"].ToString()) / 2).ToString();
                        sgstamt += Environment.NewLine + dt4.Rows[i]["sgst"].ToString();
                        sgst += Convert.ToDouble(dt4.Rows[i]["sgst"].ToString());

                        addper += Environment.NewLine + (Convert.ToDouble(dt4.Rows[i]["Addtax"].ToString()) / 2).ToString();
                        addamt += Environment.NewLine + dt4.Rows[i]["Addtaxamt"].ToString();
                        Addtax += Convert.ToDouble(dt4.Rows[i]["Addtaxamt"].ToString());

                        totalamt += Environment.NewLine + dt4.Rows[i]["total"].ToString();
                        nettotal += Convert.ToDouble(dt4.Rows[i]["total"].ToString());
                    }

                    if (Convert.ToBoolean(option.Rows[0]["autoroundoffpos"].ToString()) == true)
                    {
                        nettotal = Math.Round(nettotal, 0);
                    }
                    prn.execute("delete from printing");
                    int j = 1;
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        try
                        {
                            if (option.Rows[0]["posbillno"].ToString() == "Continuous")
                            {
                                finalbillno = dt1.Rows[0]["Billid"].ToString();
                            }
                            else
                            {
                                finalbillno = dt1.Rows[0]["billno"].ToString();
                            }
                            DataTable hsn = sql.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + dt2.Rows[i]["ItemName"].ToString() + "'");
                            //   DataTable item = sql.getdataset("select * from ItemTaxMaster where isactive=1 and ProductID='" + hsn.Rows[0]["ProductID"].ToString() + "'");
                            DataTable item = sql.getdataset("select * from TaxSlabMaster where isactive=1 and saletypename='" + DefaultPOS.saletype + "' and Taxslabname='" + hsn.Rows[0]["taxslab"].ToString() + "'");
                            string mrp = sql.ExecuteScalar("select MRP from ProductPriceMaster where isactive=1 and Productid='" + hsn.Rows[0]["Productid"].ToString() + "'");
                            string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34,T35,T36,T37,T38,T39,T40,T41,T42,T43,T44,T45,T46,T47,T48,T49,T50,T51,T52,T53,T54,T55,T56,T57,T58,T59,T60,T61,T62,T63,T64,T65,T66,T67,T68,T69,T70,T71,T72,T73,T74,T75,T76,T77,T78,T79,T80,T81,T82,T83,T84,T85,T86,T87,T88,T89,T90,T91,T92,T93,T94,T95,T96)VALUES";
                            // qry += "('" + dt3.Rows[0]["CompanyName"].ToString() + "','" + dt3.Rows[0]["Address"].ToString() + "','" + dt3.Rows[0]["Address2"].ToString() + "','" + dt3.Rows[0]["city"].ToString() + "','" + dt3.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Billid"].ToString() + "','" + Convert.ToDateTime(dt1.Rows[0]["BillDate"].ToString()).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(dt1.Rows[0]["BillDate"].ToString()).ToString("HH:mm tt") + "','" + hsn.Rows[0]["Hsn_Sac_Code"].ToString() + "','" + dt2.Rows[i]["ItemName"].ToString() + "','" + dt2.Rows[i]["qty"].ToString() + "','" + dt2.Rows[i]["Rate"].ToString() + "','" + dt2.Rows[i]["Amount"].ToString() + "')";
                            qry += "('" + dt3.Rows[0]["CompanyName"].ToString() + "','" + dt3.Rows[0]["SubName"].ToString() + "','" + dt3.Rows[0]["Address"].ToString() + "','" + dt3.Rows[0]["Address2"].ToString() + "','" + dt3.Rows[0]["City"].ToString() + "','" + dt3.Rows[0]["Phone"].ToString() + "','" + dt3.Rows[0]["Mobile"].ToString() + "','" + dt3.Rows[0]["Email"].ToString() + "','" + dt3.Rows[0]["Website"].ToString() + "','" + dt3.Rows[0]["CSTNo"].ToString() + "','" + dt3.Rows[0]["PANNo"].ToString() + "','" + dt3.Rows[0]["VATNo"].ToString() + "','" + dt3.Rows[0]["DLNo1"].ToString() + "','" + dt3.Rows[0]["DLNo2"].ToString() + "','" + dt3.Rows[0]["DealsIn"].ToString() + "','" + dt3.Rows[0]["Stockist"].ToString() + "','" + dt3.Rows[0]["currency"].ToString() + "','" + dt3.Rows[0]["StartDate"].ToString() + "','" + dt3.Rows[0]["EndDate"].ToString() + "','" + dt3.Rows[0]["MyDSNName"].ToString() + "','" + dt3.Rows[0]["LinkRemote"].ToString() + "','" + dt3.Rows[0]["DBType"].ToString() + "','" + dt3.Rows[0]["Catalyst"].ToString() + "','" + finalbillno + "','" + dt1.Rows[0]["BillDate"].ToString() + "','" + dt1.Rows[0]["Terms"].ToString() + "','" + dt1.Rows[0]["count"].ToString() + "','" + dt1.Rows[0]["totalqty"].ToString() + "','" + dt1.Rows[0]["totalbasic"].ToString() + "','" + dt1.Rows[0]["totaltax"].ToString() + "','" + dt1.Rows[0]["totalnet"].ToString() + "','" + dt1.Rows[0]["disamt"].ToString() + "','" + dt1.Rows[0]["adddisamt"].ToString() + "','" + dt1.Rows[0]["bankname"].ToString() + "','" + dt1.Rows[0]["cardnumbar"].ToString() + "','" + dt1.Rows[0]["cardtype"].ToString() + "','" + dt1.Rows[0]["expirydate"].ToString() + "','" + dt1.Rows[0]["apprcode"].ToString() + "','" + dt1.Rows[0]["refno"].ToString() + "','" + dt1.Rows[0]["amountrs"].ToString() + "','" + dt1.Rows[0]["invno"].ToString() + "','" + dt1.Rows[0]["cardholdername"].ToString() + "','" + dt1.Rows[0]["cashtendered"].ToString() + "','" + dt1.Rows[0]["change"].ToString() + "','" + dt2.Rows[i]["ItemName"].ToString() + "','" + dt2.Rows[i]["Qty"].ToString() + "','" + dt2.Rows[i]["Rate"].ToString() + "','" + dt2.Rows[i]["Amount"].ToString() + "','" + dt2.Rows[i]["Total"].ToString() + "','" + dt2.Rows[i]["igst"].ToString() + "','" + dt2.Rows[i]["Addtax"].ToString() + "','" + dt2.Rows[i]["Discount"].ToString() + "','" + dt2.Rows[i]["Per"].ToString() + "','" + dt2.Rows[i]["SerCharge"].ToString() + "','" + dt2.Rows[i]["PackCharge"].ToString() + "','" + dt2.Rows[i]["RoundOf"].ToString() + "','" + dt2.Rows[i]["NetTotal"].ToString() + "','" + dt2.Rows[i]["CashTendered"].ToString() + "','" + dt2.Rows[i]["Change"].ToString() + "','" + dt2.Rows[i]["sgst"].ToString() + "','" + dt2.Rows[i]["cgst"].ToString() + "','" + hsn.Rows[0]["ProductID"].ToString() + "','" + hsn.Rows[0]["CompanyID"].ToString() + "','" + hsn.Rows[0]["GroupName"].ToString() + "','" + hsn.Rows[0]["Product_Name"].ToString() + "','" + hsn.Rows[0]["Unit"].ToString() + "','" + hsn.Rows[0]["Altunit"].ToString() + "','" + hsn.Rows[0]["Convfactor"].ToString() + "','" + hsn.Rows[0]["Packing"].ToString() + "','" + hsn.Rows[0]["IsBatch"].ToString() + "','" + hsn.Rows[0]["Hsn_Sac_Code"].ToString() + "','" + dt3.Rows[0]["CompanyID"].ToString() + "','" + item.Rows[0]["sgst"].ToString() + "','" + item.Rows[0]["cgst"].ToString() + "','" + item.Rows[0]["additax"].ToString() + "','" + option.Rows[0]["autosaletype"].ToString() + "','" + taxable + "','" + cgstper + "','" + cgstamt + "','" + sgstper + "','" + sgstamt + "','" + totalamt + "','" + basicamt + "','" + cgst + "','" + sgst + "','" + nettotal.ToString("N2") + "','" + dt2.Rows[i]["DiscountAmt"].ToString() + "','" + addper + "','" + addamt + "','" + Addtax + "','" + dt1.Rows[0]["customername"].ToString() + "','" + dt1.Rows[0]["customercity"].ToString() + "','" + dt1.Rows[0]["customermobile"].ToString() + "','" + hsn.Rows[0]["taxslab"].ToString() + "','" + dt1.Rows[0]["billno"].ToString() +"','"+mrp+ "')";
                            prn.execute(qry);
                        }
                        catch
                        {
                        }
                    }
                    //int j = 1;
                    //string perticulars = "", remarks = "", value = "", at = "", plusminus = "", amount = "";
                    //for (int i = 0; i < LVCHARGES.Items.Count; i++)
                    //{
                    //    perticulars += LVCHARGES.Items[i].SubItems[0].Text + Environment.NewLine;
                    //    remarks += LVCHARGES.Items[i].SubItems[1].Text + Environment.NewLine;
                    //    value += LVCHARGES.Items[i].SubItems[2].Text + Environment.NewLine;
                    //    at += LVCHARGES.Items[i].SubItems[3].Text + Environment.NewLine;
                    //    plusminus += LVCHARGES.Items[i].SubItems[4].Text + Environment.NewLine;
                    //    amount += LVCHARGES.Items[i].SubItems[5].Text + Environment.NewLine;
                    //}

                    //for (int i = 0; i < LVFO.Items.Count; i++)
                    //{
                    //    try
                    //    {
                    //        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34,T35,T36,T37,T38,T39,T40,T41,T42,T43,T44,T45,T46,T47,T48,T49,T50,T51,T52,T53,T54,T55,T56,T57,T58,T59,T60,T61,T62,T63,T64,T65,T66,T67,T68,T69,T70,T71,T72,T73,T74,T75,T76,T77,T78,T79,T80)VALUES";
                    //        qry += "('" + j++ + "','" + TxtBillNo.Text + "','" + TxtRundate.Text + "','" + cmbterms.Text + "','" + txtduedate.Text + "','" + cmbcustname.Text + "','" + txtpono.Text + "','" + cmbsaletype.Text + "','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','" + LVFO.Items[i].SubItems[2].Text + "','" + LVFO.Items[i].SubItems[3].Text + "','" + LVFO.Items[i].SubItems[4].Text + "','" + LVFO.Items[i].SubItems[5].Text + "','" + LVFO.Items[i].SubItems[6].Text + "','" + LVFO.Items[i].SubItems[7].Text + "','" + LVFO.Items[i].SubItems[8].Text + "','" + LVFO.Items[i].SubItems[9].Text + "','" + LVFO.Items[i].SubItems[10].Text + "','" + LVFO.Items[i].SubItems[11].Text + "','" + LVFO.Items[i].SubItems[12].Text + "','" + LVFO.Items[i].SubItems[13].Text + "','" + LVFO.Items[i].SubItems[14].Text + "','" + lbltotcount.Text + "','" + lbltotpqty.Text + "','" + txttotaqty.Text + "','" + txttotfree.Text + "','" + lblbasictot.Text + "','" + txttotdiscount.Text + "','" + txttotadis.Text + "','" + txttottax.Text + "','" + txttotaddvat.Text + "','" + txtamt.Text + "','" + txttotservice.Text + "','" + txttotalcharges.Text + "','" + txtroundoff.Text + "','" + TxtBillTotal.Text + "','" + dt1.Rows[0][0].ToString() + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][2].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + dt1.Rows[0][11].ToString() + "','" + dt1.Rows[0][12].ToString() + "','" + dt1.Rows[0][13].ToString() + "','" + inword + "','" + txttransport.Text + "','" + txtdelieveryat.Text + "','" + txtfraight.Text + "','" + txtvehicleno.Text + "','" + txtgrrrno.Text + "','" + txtremarks.Text + "','" + txtweight.Text + "','" + txtskids.Text + "','" + perticulars + "','" + remarks + "','" + value + "','" + at + "','" + plusminus + "','" + amount + "','" + client.Rows[0][0].ToString() + "','" + client.Rows[0][1].ToString() + "','" + client.Rows[0][2].ToString() + "','" + client.Rows[0][3].ToString() + "','" + client.Rows[0][4].ToString() + "','" + client.Rows[0][5].ToString() + "','" + client.Rows[0][6].ToString() + "','" + client.Rows[0][7].ToString() + "','" + client.Rows[0][8].ToString() + "','" + client.Rows[0][9].ToString() + "','" + client.Rows[0][10].ToString() + "','" + client.Rows[0][11].ToString() + "','" + client.Rows[0][12].ToString() + "','" + client.Rows[0][13].ToString() + "')";
                    //        prn.execute(qry);
                    //    }
                    //    catch
                    //    {
                    //    }
                    //}
                    //    Print popup = new Print("Pos");
                    //  popup.ShowDialog();
                    // popup.Dispose();
                    DataTable multyprint = sql.getdataset("select multyprintinpos from Options");
                    if (Convert.ToBoolean(multyprint.Rows[0]["multyprintinpos"].ToString()) == true)
                    {
                        Print popup = new Print("Pos");
                        popup.ShowDialog();
                        popup.Dispose();
                    }
                    else
                    {
                        string strreport = Application.StartupPath + "\\" + "QuickSale.rpt";
                        SQLReport sqlreport = new SQLReport(strreport, "Pos");
                        DataTable bill = sql.getdataset("select defaultbill from Options");
                        if (bill.Rows.Count > 0)
                        {
                            if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                            {
                                sqlreport.Show();
                            }
                            else
                            {
                                //SaleReport sale = new SaleReport(str);
                                sqlreport.Show();
                                sqlreport.Hide();
                            }
                        }
                    }
              //  }
            }
            
        }

        private void rbcash_CheckedChanged(object sender, EventArgs e)
        {
            pnlcash.Visible = true;
            pnlcard.Visible = false;
            txtcastt.Focus();

        }

        private void rbcredit_CheckedChanged(object sender, EventArgs e)
        {
            pnlcash.Visible = false;
            pnlcard.Visible = true;
            lblbankname.Text = "Bank Name";
            txtamount.Focus();
        }

        private void rbdebit_CheckedChanged(object sender, EventArgs e)
        {
            pnlcash.Visible = false;
            pnlcard.Visible = true;
            lblbankname.Text = "E-Wallet Name";
            txtamount.Focus();
        }

        private void txttotal_Enter(object sender, EventArgs e)
        {
            txttotal.BackColor = Color.LightYellow;
        }

        private void txttotal_Leave(object sender, EventArgs e)
        {
            txttotal.BackColor = Color.White;
        }

        private void txtcastt_Enter(object sender, EventArgs e)
        {
            txtcastt.BackColor = Color.LightYellow;
        }

        private void txtcastt_Leave(object sender, EventArgs e)
        {
            txtcastt.BackColor = Color.White;
        }

        private void txtchange_Enter(object sender, EventArgs e)
        {
            txtchange.BackColor = Color.LightYellow;
        }

        private void txtchange_Leave(object sender, EventArgs e)
        {
            txtchange.BackColor = Color.White;
        }

        private void txtamount_Enter(object sender, EventArgs e)
        {
            txtamount.BackColor = Color.LightYellow;
        }

        private void txtamount_Leave(object sender, EventArgs e)
        {
            txtamount.BackColor = Color.White;
        }

        private void txtbankname_Enter(object sender, EventArgs e)
        {
            txtbankname.BackColor = Color.LightYellow;
        }

        private void txtbankname_Leave(object sender, EventArgs e)
        {
            txtbankname.BackColor = Color.White;
        }

        private void txtcardnumber_Enter(object sender, EventArgs e)
        {
            txtcardnumber.BackColor = Color.LightYellow;
        }

        private void txtcardnumber_Leave(object sender, EventArgs e)
        {
            txtcardnumber.BackColor = Color.White;
        }

        private void txtcardtype_Enter(object sender, EventArgs e)
        {
            txtcardtype.BackColor = Color.LightYellow;
        }

        private void txtcardtype_Leave(object sender, EventArgs e)
        {
            txtcardtype.BackColor = Color.White;
        }

        private void txtexpiry_Enter(object sender, EventArgs e)
        {
            txtexpiry.BackColor = Color.LightYellow;
        }

        private void txtexpiry_Leave(object sender, EventArgs e)
        {
            txtexpiry.BackColor = Color.White;
        }

        private void txtapprcode_Enter(object sender, EventArgs e)
        {
            txtapprcode.BackColor = Color.LightYellow;
        }

        private void txtapprcode_Leave(object sender, EventArgs e)
        {
            txtapprcode.BackColor = Color.White;
        }

        private void txtrefno_Enter(object sender, EventArgs e)
        {
            txtrefno.BackColor = Color.LightYellow;
        }

        private void txtrefno_Leave(object sender, EventArgs e)
        {
            txtrefno.BackColor = Color.White;
        }

        private void txtinvno_Enter(object sender, EventArgs e)
        {
            txtinvno.BackColor = Color.LightYellow;
        }

        private void txtinvno_Leave(object sender, EventArgs e)
        {
            txtinvno.BackColor = Color.White;
        }

        private void btnok_Enter(object sender, EventArgs e)
        {
            btnok.UseVisualStyleBackColor = false;
            btnok.BackColor = Color.YellowGreen;
            btnok.ForeColor = Color.White;
        }

        private void btnok_Leave(object sender, EventArgs e)
        {
            btnok.UseVisualStyleBackColor = true;
            btnok.BackColor = Color.FromArgb(51, 153, 255);
            btnok.ForeColor = Color.White;
        }

        private void btnok_MouseEnter(object sender, EventArgs e)
        {
            btnok.UseVisualStyleBackColor = false;
            btnok.BackColor = Color.YellowGreen;
            btnok.ForeColor = Color.White;
        }

        private void btnok_MouseLeave(object sender, EventArgs e)
        {
            btnok.UseVisualStyleBackColor = true;
            btnok.BackColor = Color.FromArgb(51, 153, 255);
            btnok.ForeColor = Color.White;
        }

        private void btnclose_Enter(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = false;
            btnclose.BackColor = Color.FromArgb(248, 152, 94);
            btnclose.ForeColor = Color.White;
        }

        private void btnclose_Leave(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = true;
            btnclose.BackColor = Color.FromArgb(51, 153, 255);
            btnclose.ForeColor = Color.White;
        }

        private void btnclose_MouseEnter(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = false;
            btnclose.BackColor = Color.FromArgb(248, 152, 94);
            btnclose.ForeColor = Color.White;
        }

        private void btnclose_MouseLeave(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = true;
            btnclose.BackColor = Color.FromArgb(51, 153, 255);
            btnclose.ForeColor = Color.White;
        }






       

       
    }
}
