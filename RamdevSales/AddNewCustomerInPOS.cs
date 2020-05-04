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
    public partial class AddNewCustomerInPOS : Form
    {
        Connection conn = new Connection();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());   
        public AddNewCustomerInPOS()
        {
            InitializeComponent();
            
        }

        public AddNewCustomerInPOS(POSNEW pOSNEW,string billno)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.pOSNEW = pOSNEW;
            bindcustomer();
            txtcusname.Focus();
            if (!string.IsNullOrEmpty(billno))
            {
                DataTable dt= conn.getdataset("select * from BillPOSMaster where isactive=1 and billno='" + billno + "' and isactive=1");
                txtcusname.Text = dt.Rows[0]["customername"].ToString();
                txtcuscity.Text = dt.Rows[0]["customercity"].ToString();
                txtcusmobile.Text = dt.Rows[0]["customermobile"].ToString();
            }
            else
            {
                pOSNEW.cusdetails();
                if (!string.IsNullOrEmpty(POSNEW.customername1))
                {
                    txtcusname.Text = POSNEW.customername1;
                }
                if (!string.IsNullOrEmpty(POSNEW.customercity1))
                {
                    txtcuscity.Text = POSNEW.customercity1;
                }
                if (!string.IsNullOrEmpty(POSNEW.customermobile1))
                {
                    txtcusmobile.Text = POSNEW.customermobile1;
                }
                if (POSNEW.customername1 != "")
                {
                    btnsubmit.Visible = false;
                }
            }
        }
        public static string activecontroal = "";
        public static string customername = "";
        public static string customercity = "";
        public static string customermobile = "";
        public static string customerid = "";
        private POSNEW pOSNEW;
        private void btnclose_Click(object sender, EventArgs e)
        {
            customername = txtcusname.Text;
            customercity = txtcuscity.Text;
            customermobile = txtcusmobile.Text;
            customerid = txtcusname.SelectedValue.ToString();
            this.Close();
        }

        private void txtcusname_Enter(object sender, EventArgs e)
        {
            activecontroal = "txtcusname";
        }

        private void txtcuscity_Enter(object sender, EventArgs e)
        {
            activecontroal = "txtcuscity";
        }

        private void txtcusmobile_Enter(object sender, EventArgs e)
        {
            activecontroal = "txtcusmobile";
        }

        private void txtcusmobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void btnzero_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnzero.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btnzero.Text;
            }
            else
            {
                txtcusmobile.Text += btnzero.Text;
            }
        }

        private void btnq_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnq.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btnq.Text;
            }
            //else
            //{
            //    txtcusmobile.Text += btnq.Text;
            //}
        }

        private void btnone_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnone.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btnone.Text;
            }
            else
            {
                txtcusmobile.Text += btnone.Text;
            }
        }

        private void btntwo_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btntwo.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btntwo.Text;
            }
            else
            {
                txtcusmobile.Text += btntwo.Text;
            }
        }

        private void btnthree_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnthree.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btnthree.Text;
            }
            else
            {
                txtcusmobile.Text += btnthree.Text;
            }
        }

        private void btnfour_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnfour.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btnfour.Text;
            }
            else
            {
                txtcusmobile.Text += btnfour.Text;
            }
        }

        private void btnfive_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnfive.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btnfive.Text;
            }
            else
            {
                txtcusmobile.Text += btnfive.Text;
            }
        }

        private void btnsix_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnsix.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btnsix.Text;
            }
            else
            {
                txtcusmobile.Text += btnsix.Text;
            }
        }

        private void btnseven_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnseven.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btnseven.Text;
            }
            else
            {
                txtcusmobile.Text += btnseven.Text;
            }
        }

        private void btneight_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btneight.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btneight.Text;
            }
            else
            {
                txtcusmobile.Text += btneight.Text;
            }
        }

        private void btnnine_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnnine.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btnnine.Text;
            }
            else
            {
                txtcusmobile.Text += btnnine.Text;
            }
        }

        private void btnw_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnw.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btnw.Text;
            }
            //else
            //{
            //    txtcusmobile.Text += btnw.Text;
            //}
        }

        private void btne_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btne.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btne.Text;
            }
            //else
            //{
            //    txtcusmobile.Text += btne.Text;
            //}
        }

        private void btnr_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnr.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btnr.Text;
            }
            //else
            //{
            //    txtcusmobile.Text += btnr.Text;
            //}
        }

        private void btnt_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnt.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btnt.Text;
            }
            //else
            //{
            //    txtcusmobile.Text += btnt.Text;
            //}
        }

        private void btny_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btny.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btny.Text;
            }
            //else
            //{
            //    txtcusmobile.Text += btny.Text;
            //}
        }

        private void btnu_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnu.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btnu.Text;
            }
            //else
            //{
            //    txtcusmobile.Text += btnu.Text;
            //}
        }

        private void btni_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btni.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btni.Text;
            }
            //else
            //{
            //    txtcusmobile.Text += btni.Text;
            //}
        }

        private void btno_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btno.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btno.Text;
            }
            //else
            //{
            //    txtcusmobile.Text += btno.Text;
            //}
        }

        private void btnp_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnp.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btnp.Text;
            }
            //else
            //{
            //    txtcusmobile.Text += btnp.Text;
            //}
        }

        private void btna_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btna.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btna.Text;
            }
            //else
            //{
            //    txtcusmobile.Text += btna.Text;
            //}
        }

        private void btns_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btns.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btns.Text;
            }
            //else
            //{
            //    txtcusmobile.Text += btns.Text;
            //}
        }

        private void btnd_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnd.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btnd.Text;
            }
            //else
            //{
            //    txtcusmobile.Text += btnd.Text;
            //}
        }

        private void btnf_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnf.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btnf.Text;
            }
            //else
            //{
            //    txtcusmobile.Text += btnf.Text;
            //}
        }

        private void btng_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btng.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btng.Text;
            }
            //else
            //{
            //    txtcusmobile.Text += btng.Text;
            //}
        }

        private void btnh_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnh.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btnh.Text;
            }
            //else
            //{
            //    txtcusmobile.Text += btnh.Text;
            //}
        }

        private void btnj_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnj.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btnj.Text;
            }
            //else
            //{
            //    txtcusmobile.Text += btnj.Text;
            //}
        }

        private void btnk_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnk.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btnk.Text;
            }
            //else
            //{
            //    txtcusmobile.Text += btnk.Text;
            //}
        }

        private void btnl_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnl.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btnl.Text;
            }
            //else
            //{
            //    txtcusmobile.Text += btnl.Text;
            //}
        }

        private void btnz_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnz.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btnz.Text;
            }
            //else
            //{
            //    txtcusmobile.Text += btnz.Text;
            //}
        }

        private void btnx_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnx.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btnx.Text;
            }
            //else
            //{
            //    txtcusmobile.Text += btnx.Text;
            //}
        }

        private void btnc_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnc.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btnc.Text;
            }
            //else
            //{
            //    txtcusmobile.Text += btnc.Text;
            //}
        }

        private void btnv_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnv.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btnv.Text;
            }
            //else
            //{
            //    txtcusmobile.Text += btnv.Text;
            //}
        }

        private void btnb_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnb.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btnb.Text;
            }
            //else
            //{
            //    txtcusmobile.Text += btnb.Text;
            //}
        }

        private void btnn_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnn.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btnn.Text;
            }
            //else
            //{
            //    txtcusmobile.Text += btnn.Text;
            //}
        }

        private void btnm_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnm.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btnm.Text;
            }
            //else
            //{
            //    txtcusmobile.Text += btnm.Text;
            //}
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
            else if (activecontroal == "txtcuscity")
            {
                if (txtcuscity.Text.Length > 0)
                {
                    txtcuscity.Text = txtcuscity.Text.Remove(txtcuscity.Text.Length - 1);
                    // this.ActiveControl = txtcash;
                }
                else
                {
                    txtcuscity.Focus();
                }
            }
            else
            {
                if (txtcusmobile.Text.Length > 0)
                {
                    txtcusmobile.Text = txtcusmobile.Text.Remove(txtcusmobile.Text.Length - 1);
                    // this.ActiveControl = txtcash;
                }
                else
                {
                    txtcusmobile.Focus();
                }
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text = "";
                //  this.ActiveControl = txtcash;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text = "";
                //  this.ActiveControl = txtcash;
            }
            else
            {
                txtcusmobile.Text = "";
            }
        }

        private void btnspace_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                string str = new string(' ', 1);
                txtcusname.Text += str;
            }
            else if (activecontroal == "txtcuscity")
            {
                string str = new string(' ', 1);
                txtcuscity.Text += str;
            }
            else
            {
                string str = new string(' ', 1);
                txtcusmobile.Text += str;
            }
        }

        private void btndes_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btndes.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btndes.Text;
            }
        }

        private void btnclace_Click(object sender, EventArgs e)
        {
            if (activecontroal == "txtcusname")
            {
                txtcusname.Text += btnclace.Text;
            }
            else if (activecontroal == "txtcuscity")
            {
                txtcuscity.Text += btnclace.Text;
            }
        }
        public void bindcustomer()
        {
            DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='99'");
            string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
            string qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=99 or GroupID='" + groupid + "') order by AccountName";
            SqlCommand cmd1 = new SqlCommand(qry, con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            txtcusname.ValueMember = "ClientID";
            txtcusname.DisplayMember = "AccountName";
            txtcusname.DataSource = dt1;
            txtcusname.SelectedIndex = -1;
        }
        private void cmbcustname_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public string accountno="";
        DataTable options = new DataTable();
        public void getaccountno()
        {
            try
            {
                DataTable options = conn.getdataset("select * from options");
                if (options.Rows[0]["accountbillno"].ToString() == "Continuous")
                {
                    string str = conn.ExecuteScalar("select max(ClientID) from ClientMaster where isactive=1");
                    Int64 id, count;
                    if (str == "")
                    {

                        id = Convert.ToInt64(1);
                        count = Convert.ToInt64(1);
                    }
                    else
                    {
                        id = Convert.ToInt64(str) + 1;
                        count = Convert.ToInt64(str) + 1;
                    }
                    accountno = options.Rows[0]["accountprefix"].ToString() + count.ToString();
                    
                }
               
               
            }
            catch
            {
            }
        }
        private void btnsubmit_Click(object sender, EventArgs e)
        {
            bool concat = txtcuscity.Text.Contains("'");
            string address = string.Empty;
            if (concat)
            {
                char[] textArray = txtcuscity.Text.ToCharArray();
                for (int i = 0; i < txtcuscity.Text.Length; i++)
                {
                    if (textArray[i].Equals('\''))
                    {
                        address += "'";
                    }
                    address += Convert.ToString(textArray[i]);
                }
            }
            else
            {
                address = txtcuscity.Text;
            }
            con.Open();
            string sql = "INSERT INTO [dbo].[ClientMaster]([AccountName],[PrintName],[GroupName],[Opbal],[Dr_cr],[Address],[City],[State],[Phone],[Mobile],[Email],[Cstno],[Vatno],[GstNo],[AdharNo],[GroupID],[statecode],[crelimite],[billlimite],[credaysale],[credaypurchase],[accountnumber],[customertypeid],[customertype],[noteorremarks],[isactive],[ismaintain])VALUES('" + txtcusname.Text + "','" + txtcusname.Text + "','CUSTOMERS','0','CR','" + address + "','" + address + "','','" + txtcusmobile.Text + "','" + txtcusmobile.Text + "','','','','','','99','','','','','','" + accountno + "','1','General','',1,0)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
           

            con.Close();
            // listviewbind();
            MessageBox.Show("Insert Data Successfully...");
        }
        public int flag = 0;
        private void txtcusname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable isavail = conn.getdataset("select * from clientmaster where isactive=1 and accountname='" + txtcusname.Text + "'");
                if (isavail.Rows.Count > 0)
                {
                    txtcusmobile.Text = isavail.Rows[0]["mobile"].ToString();
                    txtcuscity.Text = isavail.Rows[0]["city"].ToString();
                    btnsubmit.Visible = false;
                    btnclose.Focus();
                }
                else
                {
                    txtcuscity.Focus();
                }
                

            }
        }

        private void txtcuscity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtcusmobile.Focus();
            }
        }

        private void txtcusmobile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnsubmit.Focus();
            }
        }

        
    }
}
