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

namespace RamdevSales
{
    public partial class creditordebitcardinpos : Form
    {
        private POSNEW pOSNEW;
        public static string activecontroal = "";
        public creditordebitcardinpos()
        {
            InitializeComponent();
        }
        public static string tamount = "";
        public static string tbankname = "";
        public static string tcardnum = "";
        public static string tcardtype = "";
        public static string texpdate = "";
        public static string tappcode = "";
        public static string tref = "";
        public static string tinv = "";
        public static string tcholder = "";
        public static string paymenttype = "";
        Connection conn = new Connection();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());   
        public creditordebitcardinpos(POSNEW pOSNEW,string total,string billno)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.pOSNEW = pOSNEW;
            txtamount.Text = total;
            this.ActiveControl = txtbankname;
            if (!string.IsNullOrEmpty(billno))
            {
                DataTable dt = conn.getdataset("select * from BillPOSMaster where isactive=1 and billno='" + billno + "' and isactive=1");
                txtbankname.Text = dt.Rows[0]["bankname"].ToString();
                txtcardnum.Text = dt.Rows[0]["cardnumbar"].ToString();
                txtcardtype.Text = dt.Rows[0]["cardtype"].ToString();
                txtexpdate.Text = dt.Rows[0]["expirydate"].ToString();
                txtappecode.Text = dt.Rows[0]["apprcode"].ToString();
                txtrefno.Text = dt.Rows[0]["refno"].ToString();
                txtinvno.Text = dt.Rows[0]["invno"].ToString();
                txtcardholdername.Text = dt.Rows[0]["cardholdername"].ToString();

            }
            else
            {
                pOSNEW.carddetails();
                if (!string.IsNullOrEmpty(POSNEW.tbankname))
                {
                    txtbankname.Text = POSNEW.tbankname;
                }
                else if (!string.IsNullOrEmpty(POSNEW.tcardnum))
                {
                    txtcardnum.Text = POSNEW.tcardnum;
                }
                else if (!string.IsNullOrEmpty(POSNEW.tcardtype))
                {
                    txtcardtype.Text = POSNEW.tcardtype;
                }
                else if (!string.IsNullOrEmpty(POSNEW.texpdate))
                {
                    txtexpdate.Text = POSNEW.texpdate;
                }
                else if (!string.IsNullOrEmpty(POSNEW.tappcode))
                {
                    txtappecode.Text = POSNEW.tappcode;
                }
                else if (!string.IsNullOrEmpty(POSNEW.tref))
                {
                    txtrefno.Text = POSNEW.tref;
                }
                else if (!string.IsNullOrEmpty(POSNEW.tinv))
                {
                    txtinvno.Text = POSNEW.tinv;
                }
                else if (!string.IsNullOrEmpty(POSNEW.tcholder))
                {
                    txtcardholdername.Text = POSNEW.tcholder;
                }
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            tamount = txtamount.Text;
            tbankname = txtbankname.Text;
            tcardnum = txtcardnum.Text;
            tcardtype = txtcardtype.Text;
            texpdate = txtexpdate.Text;
            tappcode = txtappecode.Text;
            tref = txtrefno.Text;
            tinv = txtinvno.Text;
            tcholder = txtcardholdername.Text;
            paymenttype = "Card";
            this.Close();
        }

        private void txtbankname_Enter(object sender, EventArgs e)
        {
            activecontroal = "txtbankname";
        }

        private void txtcardnum_Enter(object sender, EventArgs e)
        {
            activecontroal = "txtcardnum";
        }

        private void txtcardtype_Enter(object sender, EventArgs e)
        {
            activecontroal = "txtcardtype";
        }

        private void txtexpdate_Enter(object sender, EventArgs e)
        {
            activecontroal = "txtexpdate";
        }

        private void txtappecode_Enter(object sender, EventArgs e)
        {
            activecontroal = "txtappecode";
        }

        private void txtrefno_Enter(object sender, EventArgs e)
        {
            activecontroal = "txtrefno";
        }

        private void txtinvno_Enter(object sender, EventArgs e)
        {
            activecontroal = "txtinvno";
        }

        private void txtcardholdername_Enter(object sender, EventArgs e)
        {
            activecontroal = "txtcardholdername";
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text = "";
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text = "";
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text = "";
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text = "";
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text = "";
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text = "";
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text = "";
            }
            else
            {
                txtcardholdername.Text = "";
            }
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                if (txtbankname.Text.Length > 0)
                {
                    txtbankname.Text = txtbankname.Text.Remove(txtbankname.Text.Length - 1);
                    // this.ActiveControl = txtcash;
                }
                else
                {
                    txtbankname.Focus();
                }
            }
            else if (activecontroal == "txtcardnum")
            {
                if (txtcardnum.Text.Length > 0)
                {
                    txtcardnum.Text = txtcardnum.Text.Remove(txtcardnum.Text.Length - 1);
                    // this.ActiveControl = txtcash;
                }
                else
                {
                    txtcardnum.Focus();
                }
            }
            else if (activecontroal == "txtcardtype")
            {
                if (txtcardtype.Text.Length > 0)
                {
                    txtcardtype.Text = txtcardtype.Text.Remove(txtcardtype.Text.Length - 1);
                    // this.ActiveControl = txtcash;
                }
                else
                {
                    txtcardtype.Focus();
                }
            }
            else if (activecontroal == "txtexpdate")
            {
                if (txtexpdate.Text.Length > 0)
                {
                    txtexpdate.Text = txtexpdate.Text.Remove(txtexpdate.Text.Length - 1);
                    // this.ActiveControl = txtcash;
                }
                else
                {
                    txtexpdate.Focus();
                }
            }
            else if (activecontroal == "txtappecode")
            {
                if (txtappecode.Text.Length > 0)
                {
                    txtappecode.Text = txtappecode.Text.Remove(txtappecode.Text.Length - 1);
                    // this.ActiveControl = txtcash;
                }
                else
                {
                    txtappecode.Focus();
                }
            }
            else if (activecontroal == "txtrefno")
            {
                if (txtrefno.Text.Length > 0)
                {
                    txtrefno.Text = txtrefno.Text.Remove(txtrefno.Text.Length - 1);
                    // this.ActiveControl = txtcash;
                }
                else
                {
                    txtrefno.Focus();
                }
            }
            else if (activecontroal == "txtinvno")
            {
                if (txtinvno.Text.Length > 0)
                {
                    txtinvno.Text = txtinvno.Text.Remove(txtinvno.Text.Length - 1);
                    // this.ActiveControl = txtcash;
                }
                else
                {
                    txtinvno.Focus();
                }
            }
            else if (activecontroal == "txtcardholdername")
            {
                if (txtcardholdername.Text.Length > 0)
                {
                    txtcardholdername.Text = txtcardholdername.Text.Remove(txtcardholdername.Text.Length - 1);
                    // this.ActiveControl = txtcash;
                }
                else
                {
                    txtcardholdername.Focus();
                }
            }
        }

        private void btnspace_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                string str = new string(' ', 1);
                txtbankname.Text += str;
            }
            else if (activecontroal == "txtcardnum")
            {
                string str = new string(' ', 1);
                txtcardnum.Text += str;
            }
            else if (activecontroal == "txtcardtype")
            {
                string str = new string(' ', 1);
                txtcardtype.Text += str;
            }
            else if (activecontroal == "txtexpdate")
            {
                string str = new string(' ', 1);
                txtexpdate.Text += str;
            }
            else if (activecontroal == "txtappecode")
            {
                string str = new string(' ', 1);
                txtappecode.Text += str;
            }
            else if (activecontroal == "txtrefno")
            {
                string str = new string(' ', 1);
                txtrefno.Text += str;
            }
            else if (activecontroal == "txtinvno")
            {
                string str = new string(' ', 1);
                txtinvno.Text += str;
            }
            else if (activecontroal == "txtcardholdername")
            {
                string str = new string(' ', 1);
                txtcardholdername.Text += str;
            }
        }

        private void btnzero_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btnzero.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btnzero.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btnzero.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btnzero.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btnzero.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btnzero.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btnzero.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btnzero.Text;
            }
        }

        private void btnone_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btnone.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btnone.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btnone.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btnone.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btnone.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btnone.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btnone.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btnone.Text;
            }
        }

        private void btntwo_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btntwo.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btntwo.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btntwo.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btntwo.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btntwo.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btntwo.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btntwo.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btntwo.Text;
            }
        }

        private void btnthree_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btnthree.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btnthree.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btnthree.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btnthree.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btnthree.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btnthree.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btnthree.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btnthree.Text;
            }
        }

        private void btnfour_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btnfour.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btnfour.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btnfour.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btnfour.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btnfour.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btnfour.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btnfour.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btnfour.Text;
            }
        }

        private void btnfive_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btnfive.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btnfive.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btnfive.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btnfive.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btnfive.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btnfive.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btnfive.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btnfive.Text;
            }
        }

        private void btnsix_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btnsix.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btnsix.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btnsix.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btnsix.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btnsix.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btnsix.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btnsix.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btnsix.Text;
            }
        }

        private void btnseven_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btnseven.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btnseven.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btnseven.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btnseven.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btnseven.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btnseven.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btnseven.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btnseven.Text;
            }
        }

        private void btneight_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btneight.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btneight.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btneight.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btneight.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btneight.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btneight.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btneight.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btneight.Text;
            }
        }

        private void btnnine_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btnnine.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btnnine.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btnnine.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btnnine.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btnnine.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btnnine.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btnnine.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btnnine.Text;
            }
        }

        private void btnq_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btnq.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btnq.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btnq.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btnq.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btnq.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btnq.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btnq.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btnq.Text;
            }
        }

        private void btnw_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btnw.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btnw.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btnw.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btnw.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btnw.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btnw.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btnw.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btnw.Text;
            }
        }

        private void btne_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btne.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btne.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btne.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btne.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btne.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btne.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btne.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btne.Text;
            }
        }

        private void btnr_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btnr.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btnr.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btnr.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btnr.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btnr.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btnr.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btnr.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btnr.Text;
            }
        }

        private void btnt_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btnt.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btnt.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btnt.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btnt.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btnt.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btnt.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btnt.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btnt.Text;
            }
        }

        private void btny_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btny.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btny.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btny.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btny.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btny.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btny.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btny.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btny.Text;
            }
        }

        private void btnu_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btnu.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btnu.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btnu.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btnu.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btnu.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btnu.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btnu.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btnu.Text;
            }
        }

        private void btni_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btni.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btni.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btni.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btni.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btni.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btni.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btni.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btni.Text;
            }
        }

        private void btno_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btno.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btno.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btno.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btno.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btno.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btno.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btno.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btno.Text;
            }
        }

        private void btnp_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btnp.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btnp.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btnp.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btnp.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btnp.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btnp.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btnp.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btnp.Text;
            }
        }

        private void btna_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btna.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btna.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btna.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btna.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btna.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btna.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btna.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btna.Text;
            }
        }

        private void btns_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btns.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btns.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btns.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btns.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btns.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btns.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btns.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btns.Text;
            }
        }

        private void btnd_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btnd.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btnd.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btnd.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btnd.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btnd.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btnd.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btnd.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btnd.Text;
            }
        }

        private void btnf_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btnf.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btnf.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btnf.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btnf.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btnf.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btnf.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btnf.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btnf.Text;
            }
        }

        private void btng_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btng.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btng.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btng.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btng.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btng.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btng.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btng.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btng.Text;
            }
        }

        private void btnh_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btnh.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btnh.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btnh.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btnh.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btnh.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btnh.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btnh.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btnh.Text;
            }
        }

        private void btnj_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btnj.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btnj.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btnj.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btnj.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btnj.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btnj.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btnj.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btnj.Text;
            }
        }

        private void btnk_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btnk.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btnk.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btnk.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btnk.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btnk.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btnk.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btnk.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btnk.Text;
            }
        }

        private void btnl_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btnl.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btnl.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btnl.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btnl.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btnl.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btnl.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btnl.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btnl.Text;
            }
        }

        private void btnz_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btnz.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btnz.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btnz.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btnz.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btnz.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btnz.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btnz.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btnz.Text;
            }
        }

        private void btnx_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btnx.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btnx.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btnx.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btnx.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btnx.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btnx.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btnx.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btnx.Text;
            }
        }

        private void btnc_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btnc.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btnc.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btnc.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btnc.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btnc.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btnc.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btnc.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btnc.Text;
            }
        }

        private void btnv_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btnv.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btnv.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btnv.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btnv.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btnv.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btnv.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btnv.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btnv.Text;
            }
        }

        private void btnb_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btnb.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btnb.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btnb.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btnb.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btnb.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btnb.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btnb.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btnb.Text;
            }
        }

        private void btnn_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btnn.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btnn.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btnn.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btnn.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btnn.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btnn.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btnn.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btnn.Text;
            }
        }

        private void btnm_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btnm.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btnm.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btnm.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btnm.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btnm.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btnm.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btnm.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btnm.Text;
            }
        }
       
        private void btnsubmit_Click(object sender, EventArgs e)
        {
            tamount = txtamount.Text;
            tbankname = txtbankname.Text;
            tcardnum = txtcardnum.Text;
            tcardtype = txtcardtype.Text;
            texpdate = txtexpdate.Text;
            tappcode = txtappecode.Text;
            tref = txtrefno.Text;
            tinv = txtinvno.Text;
            tcholder = txtcardholdername.Text;
            paymenttype = "Card";
            pOSNEW.savedata();
            pOSNEW.print();
            pOSNEW.clearall();
            this.Close();
        }

        private void btnclace_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btnclace.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btnclace.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btnclace.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btnclace.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btnclace.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btnclace.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btnclace.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btnclace.Text;
            }
        }

        private void btndes_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtbankname")
            {
                txtbankname.Text += btndes.Text;
            }
            else if (activecontroal == "txtcardnum")
            {
                txtcardnum.Text += btndes.Text;
            }
            else if (activecontroal == "txtcardtype")
            {
                txtcardtype.Text += btndes.Text;
            }
            else if (activecontroal == "txtappecode")
            {
                txtappecode.Text += btndes.Text;
            }
            else if (activecontroal == "txtrefno")
            {
                txtrefno.Text += btndes.Text;
            }
            else if (activecontroal == "txtexpdate")
            {
                txtexpdate.Text += btndes.Text;
            }
            else if (activecontroal == "txtinvno")
            {
                txtinvno.Text += btndes.Text;
            }
            else if (activecontroal == "txtcardholdername")
            {
                txtcardholdername.Text += btndes.Text;
            }
        }
    }
}
