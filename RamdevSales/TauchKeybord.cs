using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RamdevSales
{
    public partial class TauchKeybord : Form
    {
        private TextBox lblUName;

        public TauchKeybord()
        {
            InitializeComponent();
        }

        public TauchKeybord(TextBox lblUName)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.lblUName = lblUName;
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnzero_Click(object sender, EventArgs e)
        {
            lblUName.Text += btnzero.Text;
        }

        private void btnlower_Click(object sender, EventArgs e)
        {
            btna.Text = "a";
            btnb.Text = "b";
            btnc.Text = "c";
            btnd.Text = "d";
            btne.Text = "e";
            btnf.Text = "f";
            btng.Text = "g";
            btnh.Text = "h";
            btni.Text = "i";
            btnj.Text = "j";
            btnk.Text = "k";
            btnl.Text = "l";
            btnm.Text = "m";
            btnn.Text = "n";
            btno.Text = "o";
            btnp.Text = "p";
            btnq.Text = "q";
            btnr.Text = "r";
            btns.Text = "s";
            btnt.Text = "t";
            btnu.Text = "u";
            btnv.Text = "v";
            btnw.Text = "w";
            btnx.Text = "x";
            btny.Text = "y";
            btnz.Text = "z";
        }

        private void btnupper_Click(object sender, EventArgs e)
        {
            btna.Text = "A";
            btnb.Text = "B";
            btnc.Text = "C";
            btnd.Text = "D";
            btne.Text = "E";
            btnf.Text = "F";
            btng.Text = "G";
            btnh.Text = "H";
            btni.Text = "I";
            btnj.Text = "J";
            btnk.Text = "K";
            btnl.Text = "L";
            btnm.Text = "M";
            btnn.Text = "N";
            btno.Text = "O";
            btnp.Text = "P";
            btnq.Text = "Q";
            btnr.Text = "R";
            btns.Text = "S";
            btnt.Text = "T";
            btnu.Text = "U";
            btnv.Text = "V";
            btnw.Text = "W";
            btnx.Text = "X";
            btny.Text = "Y";
            btnz.Text = "Z";
        }

        private void btnone_Click(object sender, EventArgs e)
        {
            lblUName.Text += btnone.Text;
        }

        private void btntwo_Click(object sender, EventArgs e)
        {
            lblUName.Text += btntwo.Text;
        }

        private void btnthree_Click(object sender, EventArgs e)
        {
            lblUName.Text += btnthree.Text;
        }

        private void btnfour_Click(object sender, EventArgs e)
        {
            lblUName.Text += btnfour.Text;
        }

        private void btnfive_Click(object sender, EventArgs e)
        {
            lblUName.Text += btnfive.Text;
        }

        private void btnsix_Click(object sender, EventArgs e)
        {
            lblUName.Text += btnsix.Text;
        }

        private void btnseven_Click(object sender, EventArgs e)
        {
            lblUName.Text += btnseven.Text;
        }

        private void btneight_Click(object sender, EventArgs e)
        {
            lblUName.Text += btneight.Text;
        }

        private void btnnine_Click(object sender, EventArgs e)
        {
            lblUName.Text += btnnine.Text;
        }

        private void btnq_Click(object sender, EventArgs e)
        {
            lblUName.Text += btnq.Text;
        }

        private void btnw_Click(object sender, EventArgs e)
        {
            lblUName.Text += btnw.Text;
        }

        private void btne_Click(object sender, EventArgs e)
        {
            lblUName.Text += btne.Text;
        }

        private void btnr_Click(object sender, EventArgs e)
        {
            lblUName.Text += btnr.Text;
        }

        private void btnt_Click(object sender, EventArgs e)
        {
            lblUName.Text += btnt.Text;
        }

        private void btny_Click(object sender, EventArgs e)
        {
            lblUName.Text += btny.Text;
        }

        private void btnu_Click(object sender, EventArgs e)
        {
            lblUName.Text += btnu.Text;
        }

        private void btni_Click(object sender, EventArgs e)
        {
            lblUName.Text += btni.Text;
        }

        private void btno_Click(object sender, EventArgs e)
        {
            lblUName.Text += btno.Text;
        }

        private void btnp_Click(object sender, EventArgs e)
        {
            lblUName.Text += btnp.Text;
        }

        private void btnl_Click(object sender, EventArgs e)
        {
            lblUName.Text += btnl.Text;
        }

        private void btnk_Click(object sender, EventArgs e)
        {
            lblUName.Text += btnk.Text;
        }

        private void btnj_Click(object sender, EventArgs e)
        {
            lblUName.Text += btnj.Text;
        }

        private void btnh_Click(object sender, EventArgs e)
        {
            lblUName.Text += btnh.Text;
        }

        private void btng_Click(object sender, EventArgs e)
        {
            lblUName.Text += btng.Text;
        }

        private void btnf_Click(object sender, EventArgs e)
        {
            lblUName.Text += btnf.Text;
        }

        private void btnd_Click(object sender, EventArgs e)
        {
            lblUName.Text += btnd.Text;
        }

        private void btns_Click(object sender, EventArgs e)
        {
            lblUName.Text += btns.Text;
        }

        private void btna_Click(object sender, EventArgs e)
        {
            lblUName.Text += btna.Text;
        }

        private void btnz_Click(object sender, EventArgs e)
        {
            lblUName.Text += btnz.Text;
        }

        private void btnx_Click(object sender, EventArgs e)
        {
            lblUName.Text += btnx.Text;
        }

        private void btnc_Click(object sender, EventArgs e)
        {
            lblUName.Text += btnc.Text;
        }

        private void btnv_Click(object sender, EventArgs e)
        {
            lblUName.Text += btnv.Text;
        }

        private void btnb_Click(object sender, EventArgs e)
        {
            lblUName.Text += btnb.Text;
        }

        private void btnn_Click(object sender, EventArgs e)
        {
            lblUName.Text += btnn.Text;
        }

        private void btnm_Click(object sender, EventArgs e)
        {
            lblUName.Text += btnm.Text;
        }

        private void btndes_Click(object sender, EventArgs e)
        {
            lblUName.Text += btndes.Text;
        }

        private void btnclace_Click(object sender, EventArgs e)
        {
            lblUName.Text += btnclace.Text;
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            lblUName.Text = lblUName.Text.Remove(lblUName.Text.Length - 1);
        }

        private void btnspace_Click(object sender, EventArgs e)
        {
            string str = new string(' ', 1);
            lblUName.Text += str;
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            lblUName.Text = "";
        }
    }
}
