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
    public partial class ChargesPnl : Form
    {
        public static string chargesamount = "";
        public static string valofexp = "";
        public static string tax1 = "";
        public static string sgst1 = "";
        public static string cgst1 = "";
        public static string igst1 = "";
        public static string additax = "";
        public static string additaxamt = "";

        Connection conn = new Connection();
        DefaultSale d = new DefaultSale();
        DefaultSaleOrder so = new DefaultSaleOrder();
        DataTable dt = new DataTable();
        double tax, value, sgst,cgst,igst;
        private TextBox txtcharamt;
        private DefaultSale defaultSale;
        private string[] strfinalarray;
        private DefaultSaleOrder defaultSaleOrder;
        private SalePurchaseOrderSimpleformate salePurchaseOrderSimpleformate;
        private GSTVouchers gSTVouchers;
        public ChargesPnl()
        {
            InitializeComponent();
        }

        public ChargesPnl(TextBox txtcharamt)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.txtcharamt = txtcharamt;
        }

        public ChargesPnl(TextBox txtcharamt, DefaultSale defaultSale, string[] strfinalarray)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.txtcharamt = txtcharamt;
            this.defaultSale = defaultSale;
            this.strfinalarray = strfinalarray;
        }

        public ChargesPnl(TextBox txtcharamt, DefaultSaleOrder defaultSaleOrder, string[] strfinalarray)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.txtcharamt = txtcharamt;
            this.defaultSaleOrder = defaultSaleOrder;
            this.strfinalarray = strfinalarray;
        }

        public ChargesPnl(TextBox txtcharamt, SalePurchaseOrderSimpleformate salePurchaseOrderSimpleformate, string[] strfinalarray)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.txtcharamt = txtcharamt;
            this.salePurchaseOrderSimpleformate = salePurchaseOrderSimpleformate;
            this.strfinalarray = strfinalarray;
        }

        public ChargesPnl(TextBox txtcharamt, GSTVouchers gSTVouchers, string[] strfinalarray)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.txtcharamt = txtcharamt;
            this.gSTVouchers = gSTVouchers;
            this.strfinalarray = strfinalarray;
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
        private void txtvalexp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                //chargesamount = txtvalexp.Text;
                //d.bindtotalvalue();
                if (txtvalexp.Text == "")
                {
                    txtvalexp.Text = "0";
                }
                txttax.Focus();
                valofexp = txtvalexp.Text;
            }
        }

        private void txttax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (txttax.Text == "")
                {
                    txttax.Text = "0";
                }
                txtsgst.Focus();
                tax1 = txttax.Text;
            }
        }

        private void txtsgst_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (txtsgst.Text == "")
                {
                    txtsgst.Text = "0";
                }
                txtcgst.Focus();
                sgst1 = txtsgst.Text;
            }
        }

        private void txtcgst_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (txtcgst.Text == "")
                {
                    txtcgst.Text = "0";
                }
                txtigst.Focus();
                cgst1 = txtsgst.Text;
            }
        }

        private void txtigst_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (txtigst.Text == "")
                {
                    txtigst.Text = "0";
                }
                txtadditax.Focus();
                igst1 = txtigst.Text;
            }
        }

        private void ChargesPnl_Load(object sender, EventArgs e)
        
{
            try
            {
                this.ActiveControl = txtvalexp;

                if (DefaultSale.taxvalue != "" && DefaultSale.taxvalue != null)
                {
                    txtvalexp.Text = DefaultSale.taxvalue;
                }
                if (DefaultSaleOrder.taxvalue != "" && DefaultSaleOrder.taxvalue !=null)
                {
                    txtvalexp.Text = DefaultSaleOrder.taxvalue;
                }
                if (SalePurchaseOrderSimpleformate.taxvalue != "" && SalePurchaseOrderSimpleformate.taxvalue != null)
                {
                    txtvalexp.Text = DefaultSaleOrder.taxvalue;
                }
                if (GSTVouchers.taxvalue != "" && GSTVouchers.taxvalue != null)
                {
                    txtvalexp.Text = GSTVouchers.taxvalue;
                }
                if (GSTVouchers.lvfvalue != "" && GSTVouchers.lvfvalue != null)
                {
                    txtvalexp.Text = GSTVouchers.lvfvalue;
                    txttax.Text = GSTVouchers.lvftax;
                    txtsgst.Text = GSTVouchers.lvfsgst;
                    txtcgst.Text = GSTVouchers.lvfcgst;
                    txtigst.Text = GSTVouchers.lvfigst;
                    txtadditax.Text = GSTVouchers.lvfaddtax;
                }
                if (DefaultSale.lvfvalue != "" && DefaultSale.lvfvalue != null)
                {
                    txtvalexp.Text = DefaultSale.lvfvalue;
                    txttax.Text = DefaultSale.lvftax;
                    txtsgst.Text = DefaultSale.lvfsgst;
                    txtcgst.Text = DefaultSale.lvfcgst;
                    txtigst.Text = DefaultSale.lvfigst;
                    txtadditax.Text = DefaultSale.lvfaddtax;
                }
                if (DefaultSaleOrder.lvfvalue != "" && DefaultSaleOrder.lvfvalue != null)
                {
                    txtvalexp.Text = DefaultSaleOrder.lvfvalue;
                    txttax.Text = DefaultSaleOrder.lvftax;
                    txtsgst.Text = DefaultSaleOrder.lvfsgst;
                    txtcgst.Text = DefaultSaleOrder.lvfcgst;
                    txtigst.Text = DefaultSaleOrder.lvfigst;
                    txtadditax.Text = DefaultSaleOrder.lvfaddtax;
                }
                if (SalePurchaseOrderSimpleformate.lvfvalue != "" && SalePurchaseOrderSimpleformate.lvfvalue != null)
                {
                    txtvalexp.Text = SalePurchaseOrderSimpleformate.lvfvalue;
                    txttax.Text = SalePurchaseOrderSimpleformate.lvftax;
                    txtsgst.Text = SalePurchaseOrderSimpleformate.lvfsgst;
                    txtcgst.Text = SalePurchaseOrderSimpleformate.lvfcgst;
                    txtigst.Text = SalePurchaseOrderSimpleformate.lvfigst;
                    txtadditax.Text = SalePurchaseOrderSimpleformate.lvfaddtax;
                }
            }
            catch
            {
            }
        }

        private void txtvalexp_TextChanged(object sender, EventArgs e)
        {
            //txtvalexp.Text = chargesamount;
            //chargesamount = txtvalexp.Text;
            txtcharamt.Text = txtvalexp.Text;
           // d.bindtotalvalue();
            
        }

        private void txttax_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(GSTVouchers.saletype))
                {
                    dt = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + GSTVouchers.saletype + "'");
                }
                else
                {
                    if (strfinalarray[0] == "S")
                    {
                        dt = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + DefaultSale.saletype + "'");
                    }
                    else if (strfinalarray[0] == "SR")
                    {
                        dt = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + DefaultSale.saletype + "'");
                    }
                    else if (strfinalarray[0] == "P")
                    {
                        dt = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + DefaultSale.saletype + "'");
                    }
                    else if (strfinalarray[0] == "PR")
                    {
                        dt = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + DefaultSale.saletype + "'");
                    }
                    else if (strfinalarray[0] == "SO")
                    {
                        dt = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + DefaultSaleOrder.saletype + "'");
                    }
                    else if (strfinalarray[0] == "SC")
                    {
                        dt = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + DefaultSaleOrder.saletype + "'");
                    }
                    else if (strfinalarray[0] == "PO")
                    {
                        dt = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + DefaultSaleOrder.saletype + "'");
                    }
                    else if (strfinalarray[0] == "PC")
                    {
                        dt = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + DefaultSaleOrder.saletype + "'");
                    }
                    //dt = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + DefaultSaleOrder.saletype + "'");
                }
                if (txttax.Text == "")
                {
                    txttax.Text = "0";
                }
                if (dt.Rows[0]["Region"].ToString() == "Local")
                {
                    tax = Convert.ToDouble(txttax.Text);
                    value = Convert.ToDouble(txtvalexp.Text);
                    sgst = (tax * value / 2) / 100;
                    cgst = (tax * value / 2) / 100;
                    txtsgst.Text = Convert.ToString(sgst);
                    txtcgst.Text = Convert.ToString(cgst);
                    txtsgst.ReadOnly = true;
                    txtcgst.ReadOnly = true;
                    txtigst.ReadOnly = true;
                    txtigst.Text = "0";
                    txtadditax.Text = "0";
                    chargesamount = Convert.ToString(value + sgst + cgst);
                }
                else
                {
                    tax = Convert.ToDouble(txttax.Text);
                    value = Convert.ToDouble(txtvalexp.Text);
                    igst = tax * value / 100;
                    txtigst.Text = Convert.ToString(igst);
                    txtsgst.ReadOnly = true;
                    txtcgst.ReadOnly = true;
                    txtigst.ReadOnly = true;
                    txtsgst.Text = "0";
                    txtcgst.Text = "0";
                    txtadditax.Text = "0";
                    chargesamount = Convert.ToString(value + igst);

                }
            }
            catch
            {
            }
        }

        private void txtadditax_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (txtadditax.Text == "")
                    {
                        txtadditax.Text = "0";
                    }
                    if (txtvalexp.Text == "")
                    {
                        txtvalexp.Text = "0";
                    }
                    if (txttax.Text == "")
                    {
                        txttax.Text = "0";
                    }
                    additax = txtadditax.Text;
                    double valuee =Convert.ToDouble(valofexp);
                    double ata = Convert.ToDouble(additax);
                    double adita = (valuee * ata) / 100;
                    additaxamt = Convert.ToString(adita);
                    this.Close();

                    double add = Convert.ToDouble(chargesamount) * Convert.ToDouble(txtadditax.Text) / 100;
                    double csk = Convert.ToDouble(chargesamount);
                    double csk1 = csk - add;
                    chargesamount = Convert.ToString(csk1);
                    txtcharamt.Focus();
                    txtcharamt.Text = chargesamount;


                }
            }
            catch
            {
            }
        }

        private void txtvalexp_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txttax_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtadditax_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtvalexp_Enter(object sender, EventArgs e)
        {
            txtvalexp.BackColor = Color.LightYellow;
        }

        private void txtvalexp_Leave(object sender, EventArgs e)
        {
            txtvalexp.BackColor = Color.White;
        }

        private void txttax_Enter(object sender, EventArgs e)
        {
            txttax.BackColor = Color.LightYellow;
        }

        private void txttax_Leave(object sender, EventArgs e)
        {
            txttax.BackColor = Color.White;
        }

        private void txtsgst_Enter(object sender, EventArgs e)
        {
            txtsgst.BackColor = Color.LightYellow;
        }

        private void txtsgst_Leave(object sender, EventArgs e)
        {
            txtsgst.BackColor = Color.White;
        }

        private void txtcgst_Enter(object sender, EventArgs e)
        {
            txtcgst.BackColor = Color.LightYellow;
        }

        private void txtcgst_Leave(object sender, EventArgs e)
        {
            txtcgst.BackColor = Color.White;
        }

        private void txtigst_Enter(object sender, EventArgs e)
        {
            txtigst.BackColor = Color.LightYellow;
        }

        private void txtigst_Leave(object sender, EventArgs e)
        {
            txtigst.BackColor = Color.White;
        }

        private void txtadditax_Enter(object sender, EventArgs e)
        {
            txtadditax.BackColor = Color.LightYellow;
        }

        private void txtadditax_Leave(object sender, EventArgs e)
        {
            txtadditax.BackColor = Color.White;
        }







    }
}
