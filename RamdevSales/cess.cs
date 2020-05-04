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
    public partial class cess : Form
    {
        private string p;
        private string p_2;
        private string p_3;
        Double MRP,qty,disamt;
        private TextBox txtrate;
        private TextBox txtcess;
        private TextBox txttotal;
        private TextBox txtbags;
        private TextBox txtdisamt;
        public cess()
        {
            InitializeComponent();
        }

        public cess(string p, string p_2)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.p = p;
            this.p_2 = p_2;
        }

        public cess(string p, string p_2, string p_3)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.p = p;
            this.p_2 = p_2;
            this.p_3 = p_3;
        }

        public cess(string p, string p_2, TextBox txtrate, TextBox txtcess)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.p = p;
            this.p_2 = p_2;
            this.txtrate = txtrate;
            this.txtcess = txtcess;
        }

        public cess(string p, string p_2, TextBox txttotal, TextBox txtcess, TextBox txtbags, TextBox txtdisamt)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.p = p;
            this.p_2 = p_2;
            this.txttotal = txttotal;
            this.txtcess = txtcess;
            this.txtbags = txtbags;
            this.txtdisamt = txtdisamt;
        }

        private void cess_Load(object sender, EventArgs e)
        {
            try
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(480, 271);
                MRP = Convert.ToDouble(txttotal.Text);
                qty = Convert.ToDouble(txtbags.Text);
                disamt = Convert.ToDouble(txtdisamt.Text);
                lblcess.Text ="["+ p+"%]";
                Double cessval = (MRP * Convert.ToDouble(p)) / 100;
                txtcessper.Text = Convert.ToString(cessval);
                Double cessamt = Convert.ToDouble(txtbags.Text) * Convert.ToDouble(p_2);
                txtcessamt.Text = Convert.ToString(cessamt);
               
            }
            catch
            {
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {

                DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    this.Close();
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void txtcessper_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtcessamt.Focus();
            }
        }

        private void txtcessamt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Double cess = Convert.ToDouble(txtcessper.Text) + Convert.ToDouble(txtcessamt.Text);
                Double totalcess = cess - Convert.ToDouble(txtdisamt.Text);
                Double cessval = Convert.ToDouble(txtcess.Text) + totalcess;
                txtcess.Text = Convert.ToString(cessval.ToString("N2"));
                this.Close();

            }
        }
    }
}
