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
    public partial class Price : Form
    {
        private string p;
        private string p_2;
        private TextBox txtrate;
        private TextBox txtamount;
        double finaltax;
        private string[] strfinalarray;
        public Price()
        {
            InitializeComponent();
        }

        public Price(TextBox txtrate, TextBox txtamount)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.txtrate = txtrate;
            this.txtamount = txtamount;
        }

        public Price(TextBox txtrate, TextBox txtamount, string[] strfinalarray)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.txtrate = txtrate;
            this.txtamount = txtamount;
            this.strfinalarray = strfinalarray;
        }

      

        private void Price_Load(object sender, EventArgs e)
        {
            try
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(480, 271);
                txtprice.Text = txtrate.Text;

                

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

        private void txtprice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double price =Convert.ToDouble(txtrate.Text);
                txtrate.Text = Convert.ToString(price);

            }
            catch
            {
            }
        }

        private void txtprice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

              
                if (strfinalarray[0] == "S")
                {
                    double price = Convert.ToDouble(txtprice.Text);
                    string tax1 = DefaultSale.taxforprice.TrimStart('[');
                    string t = tax1.TrimEnd(']');
                    double tax = Convert.ToDouble(t);
                    string a1 = DefaultSale.ataxforprice.TrimStart('[');
                    string a = a1.TrimEnd(']');
                    double atax = Convert.ToDouble(a);
                    finaltax = Math.Round(price /( (100 + tax + atax)/100), 2);
                    txtrate.Text = Convert.ToString(finaltax);

                    txtrate.Focus();
                }
                else if (strfinalarray[0] == "SR")
                {
                    double price = Convert.ToDouble(txtprice.Text);
                    string tax1 = DefaultSale.taxforprice.TrimStart('[');
                    string t = tax1.TrimEnd(']');
                    double tax = Convert.ToDouble(t);
                    string a1 = DefaultSale.ataxforprice.TrimStart('[');
                    string a = a1.TrimEnd(']');
                    double atax = Convert.ToDouble(a);
                    finaltax = Math.Round(price / ((100 + tax + atax) / 100), 2);
                    txtrate.Text = Convert.ToString(finaltax);

                    txtrate.Focus();
                }
                else if (strfinalarray[0] == "P")
                {
                    double price = Convert.ToDouble(txtprice.Text);
                    string tax1 = DefaultSale.taxforprice.TrimStart('[');
                    string t = tax1.TrimEnd(']');
                    double tax = Convert.ToDouble(t);
                    string a1 = DefaultSale.ataxforprice.TrimStart('[');
                    string a = a1.TrimEnd(']');
                    double atax = Convert.ToDouble(a);
                    finaltax = Math.Round(price / ((100 + tax + atax) / 100), 2);
                    txtrate.Text = Convert.ToString(finaltax);

                    txtrate.Focus();
                }
                else if (strfinalarray[0] == "PR")
                {
                    double price = Convert.ToDouble(txtprice.Text);
                    string tax1 = DefaultSale.taxforprice.TrimStart('[');
                    string t = tax1.TrimEnd(']');
                    double tax = Convert.ToDouble(t);
                    string a1 = DefaultSale.ataxforprice.TrimStart('[');
                    string a = a1.TrimEnd(']');
                    double atax = Convert.ToDouble(a);
                    finaltax = Math.Round(price / ((100 + tax + atax) / 100), 2);
                    txtrate.Text = Convert.ToString(finaltax);

                    txtrate.Focus();
                }
                else if (strfinalarray[0] == "SO")
                {
                    double price = Convert.ToDouble(txtprice.Text);
                    string tax1 = DefaultSaleOrder.taxforprice.TrimStart('[');
                    string t = tax1.TrimEnd(']');
                    double tax = Convert.ToDouble(t);
                    string a1 = DefaultSaleOrder.ataxforprice.TrimStart('[');
                    string a = a1.TrimEnd(']');
                    double atax = Convert.ToDouble(a);
                    finaltax = Math.Round(price / ((100 + tax + atax) / 100), 2);
                    txtrate.Text = Convert.ToString(finaltax);

                    txtrate.Focus();
                }
                else if (strfinalarray[0] == "SC")
                {
                    double price = Convert.ToDouble(txtprice.Text);
                    string tax1 = DefaultSaleOrder.taxforprice.TrimStart('[');
                    string t = tax1.TrimEnd(']');
                    double tax = Convert.ToDouble(t);
                    string a1 = DefaultSaleOrder.ataxforprice.TrimStart('[');
                    string a = a1.TrimEnd(']');
                    double atax = Convert.ToDouble(a);
                    finaltax = Math.Round(price / ((100 + tax + atax) / 100), 2);
                    txtrate.Text = Convert.ToString(finaltax);

                    txtrate.Focus();
                }
                else if (strfinalarray[0] == "PO")
                {
                    double price = Convert.ToDouble(txtprice.Text);
                    string tax1 = DefaultSaleOrder.taxforprice.TrimStart('[');
                    string t = tax1.TrimEnd(']');
                    double tax = Convert.ToDouble(t);
                    string a1 = DefaultSaleOrder.ataxforprice.TrimStart('[');
                    string a = a1.TrimEnd(']');
                    double atax = Convert.ToDouble(a);
                    finaltax = Math.Round(price / ((100 + tax + atax) / 100), 2);
                    txtrate.Text = Convert.ToString(finaltax);

                    txtrate.Focus();
                }
                else if (strfinalarray[0] == "PC")
                {
                    double price = Convert.ToDouble(txtprice.Text);
                    string tax1 = DefaultSaleOrder.taxforprice.TrimStart('[');
                    string t = tax1.TrimEnd(']');
                    double tax = Convert.ToDouble(t);
                    string a1 = DefaultSaleOrder.ataxforprice.TrimStart('[');
                    string a = a1.TrimEnd(']');
                    double atax = Convert.ToDouble(a);
                    finaltax = Math.Round(price / ((100 + tax + atax) / 100), 2);
                    txtrate.Text = Convert.ToString(finaltax);

                    txtrate.Focus();
                }
               
                this.Close();
            }
        }

        private void txtprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            {

                if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 45 || e.KeyChar == 8)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void txtprice_Enter(object sender, EventArgs e)
        {
            txtprice.BackColor = Color.LightYellow;
        }

        private void txtprice_Leave(object sender, EventArgs e)
        {
            txtprice.BackColor = Color.White;
        }
    }
}
