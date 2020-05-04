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
    public partial class chequedetails : Form
    {
        private POSNEW pOSNEW;

        public chequedetails()
        {
            InitializeComponent();
        }
        public static string activecontroal = "";
        public static string customername = "";
        public static string customerbank = "";
        public static string customercheque = "";
        public static string paymenttype = "";
        Connection conn = new Connection();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString()); 
        public chequedetails(POSNEW pOSNEW,string billno)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.pOSNEW = pOSNEW;
            this.ActiveControl = txtcusname;
            if (!string.IsNullOrEmpty(billno))
            {
                DataTable dt = conn.getdataset("select * from BillPOSMaster where isactive=1 and billno='" + billno + "' and isactive=1");
                txtcusname.Text = dt.Rows[0]["customerchequename"].ToString();
                txtcusbank.Text = dt.Rows[0]["customerchequebankname"].ToString();
                txtcuschequeno.Text = dt.Rows[0]["customercheckno"].ToString();
            }
            else
            {
                pOSNEW.chequedetail();
                if (!string.IsNullOrEmpty(POSNEW.chequecusname))
                {
                    txtcusname.Text = POSNEW.chequecusname;
                }
                if (!string.IsNullOrEmpty(POSNEW.chequebankname))
                {
                    txtcusbank.Text = POSNEW.chequebankname;
                }
                if (!string.IsNullOrEmpty(POSNEW.chequeno))
                {
                    txtcuschequeno.Text = POSNEW.chequeno;
                }
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            customername = txtcusname.Text;
            customerbank = txtcusbank.Text;
            customercheque = txtcuschequeno.Text;
            this.Close();
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                if (txtcusname.Text.Length > 0)
                {
                    txtcusname.Text = txtcusname.Text.Remove(txtcusname.Text.Length - 1);
                    // this.ActiveControl = txtcash;
                }
                else
                {
                    txtcusname.Focus();
                }
            }
            else if (activecontroal == "txtcusbank")
            {
                if (txtcusbank.Text.Length > 0)
                {
                    txtcusbank.Text = txtcusbank.Text.Remove(txtcusbank.Text.Length - 1);
                    // this.ActiveControl = txtcash;
                }
                else
                {
                    txtcusbank.Focus();
                }
            }
            else
            {
                if (txtcuschequeno.Text.Length > 0)
                {
                    txtcuschequeno.Text = txtcuschequeno.Text.Remove(txtcuschequeno.Text.Length - 1);
                    // this.ActiveControl = txtcash;
                }
                else
                {
                    txtcuschequeno.Focus();
                }
            }
        }

        private void btnspace_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                string str = new string(' ', 1);
                txtcusname.Text += str;
            }
            else if (activecontroal == "txtcusbank")
            {
                string str = new string(' ', 1);
                txtcusbank.Text += str;
            }
            else
            {
                string str = new string(' ', 1);
                txtcuschequeno.Text += str;
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text = "";
                //  this.ActiveControl = txtcash;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text = "";
                //  this.ActiveControl = txtcash;
            }
            else
            {
                txtcuschequeno.Text = "";
            }
        }

        private void btnzero_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnzero.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btnzero.Text;
            }
            else
            {
                txtcuschequeno.Text += btnzero.Text;
            }
        }

        private void txtcusname_Enter(object sender, EventArgs e)
        {
            activecontroal = "txtcusname";
        }

        private void txtcusbank_Enter(object sender, EventArgs e)
        {
            activecontroal = "txtcusbank";
        }

        private void txtcuschequeno_Enter(object sender, EventArgs e)
        {
            activecontroal = "txtcuschequeno";
        }

        private void btnone_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnone.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btnone.Text;
            }
            else
            {
                txtcuschequeno.Text += btnone.Text;
            }
        }

        private void btntwo_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btntwo.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btntwo.Text;
            }
            else
            {
                txtcuschequeno.Text += btntwo.Text;
            }
        }

        private void btnthree_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnthree.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btnthree.Text;
            }
            else
            {
                txtcuschequeno.Text += btnthree.Text;
            }
        }

        private void btnfour_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnfour.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btnfour.Text;
            }
            else
            {
                txtcuschequeno.Text += btnfour.Text;
            }
        }

        private void btnfive_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnfive.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btnfive.Text;
            }
            else
            {
                txtcuschequeno.Text += btnfive.Text;
            }
        }

        private void btnsix_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnsix.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btnsix.Text;
            }
            else
            {
                txtcuschequeno.Text += btnsix.Text;
            }
        }

        private void btnseven_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnseven.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btnseven.Text;
            }
            else
            {
                txtcuschequeno.Text += btnseven.Text;
            }
        }

        private void btneight_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btneight.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btneight.Text;
            }
            else
            {
                txtcuschequeno.Text += btneight.Text;
            }
        }

        private void btnnine_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnnine.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btnnine.Text;
            }
            else
            {
                txtcuschequeno.Text += btnnine.Text;
            }
        }

        private void btnq_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnq.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btnq.Text;
            }
            else
            {
                txtcuschequeno.Text += btnq.Text;
            }
        }

        private void btnw_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnw.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btnw.Text;
            }
            else
            {
                txtcuschequeno.Text += btnw.Text;
            }
        }

        private void btne_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btne.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btne.Text;
            }
            else
            {
                txtcuschequeno.Text += btne.Text;
            }
        }

        private void btnr_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnr.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btnr.Text;
            }
            else
            {
                txtcuschequeno.Text += btnr.Text;
            }
        }

        private void btnt_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnt.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btnt.Text;
            }
            else
            {
                txtcuschequeno.Text += btnt.Text;
            }
        }

        private void btny_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btny.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btny.Text;
            }
            else
            {
                txtcuschequeno.Text += btny.Text;
            }
        }

        private void btnu_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnu.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btnu.Text;
            }
            else
            {
                txtcuschequeno.Text += btnu.Text;
            }
        }

        private void btni_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btni.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btni.Text;
            }
            else
            {
                txtcuschequeno.Text += btni.Text;
            }
        }

        private void btno_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btno.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btno.Text;
            }
            else
            {
                txtcuschequeno.Text += btno.Text;
            }
        }

        private void btnp_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnp.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btnp.Text;
            }
            else
            {
                txtcuschequeno.Text += btnp.Text;
            }
        }

        private void btna_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btna.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btna.Text;
            }
            else
            {
                txtcuschequeno.Text += btna.Text;
            }
        }

        private void btns_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btns.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btns.Text;
            }
            else
            {
                txtcuschequeno.Text += btns.Text;
            }
        }

        private void btnd_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnd.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btnd.Text;
            }
            else
            {
                txtcuschequeno.Text += btnd.Text;
            }
        }

        private void btnf_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnf.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btnf.Text;
            }
            else
            {
                txtcuschequeno.Text += btnf.Text;
            }
        }

        private void btng_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btng.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btng.Text;
            }
            else
            {
                txtcuschequeno.Text += btng.Text;
            }
        }

        private void btnh_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnh.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btnh.Text;
            }
            else
            {
                txtcuschequeno.Text += btnh.Text;
            }
        }

        private void btnj_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnj.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btnj.Text;
            }
            else
            {
                txtcuschequeno.Text += btnj.Text;
            }
        }

        private void btnk_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnk.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btnk.Text;
            }
            else
            {
                txtcuschequeno.Text += btnk.Text;
            }
        }

        private void btnl_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnl.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btnl.Text;
            }
            else
            {
                txtcuschequeno.Text += btnl.Text;
            }
        }

        private void btnz_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnz.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btnz.Text;
            }
            else
            {
                txtcuschequeno.Text += btnz.Text;
            }
        }

        private void btnx_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnx.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btnx.Text;
            }
            else
            {
                txtcuschequeno.Text += btnx.Text;
            }
        }

        private void btnc_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnc.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btnc.Text;
            }
            else
            {
                txtcuschequeno.Text += btnc.Text;
            }
        }

        private void btnv_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnv.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btnv.Text;
            }
            else
            {
                txtcuschequeno.Text += btnv.Text;
            }
        }

        private void btnb_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnb.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btnb.Text;
            }
            else
            {
                txtcuschequeno.Text += btnb.Text;
            }
        }

        private void btnn_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnn.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btnn.Text;
            }
            else
            {
                txtcuschequeno.Text += btnn.Text;
            }
        }

        private void btnm_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnm.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btnm.Text;
            }
            else
            {
                txtcuschequeno.Text += btnm.Text;
            }
        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
            customername = txtcusname.Text;
            customerbank = txtcusbank.Text;
            customercheque = txtcuschequeno.Text;
            paymenttype = "Cheque";
            pOSNEW.savedata();
            pOSNEW.print();
            pOSNEW.clearall();
            this.Close();
        }

        private void btnclace_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnclace.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btnclace.Text;
            }
            else
            {
                txtcuschequeno.Text += btnclace.Text;
            }
        }

        private void btndes_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btndes.Text;
            }
            else if (activecontroal == "txtcusbank")
            {
                txtcusbank.Text += btndes.Text;
            }
            else
            {
                txtcuschequeno.Text += btndes.Text;
            }
        }
    }
}
