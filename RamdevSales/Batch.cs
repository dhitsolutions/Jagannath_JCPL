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
    public partial class Batch : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        DataTable serial = new DataTable();
         private string[] strfinalarray;
         private DefaultSale mdefaultSale;
         private DefaultSaleOrder defaultSaleOrder;
         public static string batchno;
        public Batch()
        {
            InitializeComponent();
        }
        int a = 0;
        public Batch(string[] strfinalarray)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.strfinalarray = strfinalarray;
        }
        public static string pvc;
        public Batch(string[] strfinalarray, DefaultSale defaultSale, string activecontroal)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this.strfinalarray = strfinalarray;
            this.mdefaultSale = defaultSale;
            pvc = activecontroal;
            a = 1;
        }

        public Batch(DefaultSaleOrder defaultSaleOrder, string[] strfinalarray, string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.defaultSaleOrder = defaultSaleOrder;
            this.strfinalarray = strfinalarray;
            pvc = activecontroal;
            a = 1;
        }

        public Batch(SalePurchaseOrderSimpleformate salePurchaseOrderSimpleformate, string[] strfinalarray, string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.salePurchaseOrderSimpleformate = salePurchaseOrderSimpleformate;
            this.strfinalarray = strfinalarray;
            pvc = activecontroal;
            a = 1;
        }

      

        private void txtbatchno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtbarcode.Focus();

            }
            if (e.KeyCode == Keys.Tab)
            {
                txtbarcode.Focus();
            }
        }

        private void txtbarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtsaleprice.Focus();

            }
            if (e.KeyCode == Keys.Tab)
            {
                txtsaleprice.Focus();
            }
        }

        private void txtsaleprice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtmrp.Focus();

            }
            if (e.KeyCode == Keys.Tab)
            {
                txtmrp.Focus();
            }
        }

        private void txtmrp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtbasicprice.Focus();

            }
            if (e.KeyCode == Keys.Tab)
            {
                txtbasicprice.Focus();
            }
        }

        private void txtbasicprice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtpurchaseprice.Focus();

            }
            if (e.KeyCode == Keys.Tab)
            {
                txtpurchaseprice.Focus();
            }
        }

        private void txtexpirydate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtmfgdate.Focus();

            }
            if (e.KeyCode == Keys.Tab)
            {
                txtmfgdate.Focus();
            }
        }

        private void txtmfgdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnsave.Focus();

            }
            if (e.KeyCode == Keys.Tab)
            {
                btnsave.Focus();
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
                    //tabControl.SelectTab(1);
                }
                return true;
            }
           
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void btncancle_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Close();
                //tabControl.SelectTab(1);
            }
          
        }
        string p;
        private SalePurchaseOrderSimpleformate salePurchaseOrderSimpleformate;
        private string activecontroal;
        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (strfinalarray[0] == "S")
                {
                    serial = conn.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + DefaultSale.itemname + "'"); 
                }
                else if (strfinalarray[0] == "SR")
                {
                    serial = conn.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + DefaultSale.itemname + "'"); 
                }
                else if (strfinalarray[0] == "P")
                {
                    serial = conn.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + DefaultSale.itemname + "'");
                }

                else if (strfinalarray[0] == "PR")
                {
                    serial = conn.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + DefaultSale.itemname + "'");
                }
                //else if (strfinalarray[0] == "SO")
                //{
                //    serial = conn.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + DefaultSaleOrder.itemname + "'");
                //}
                //else if (strfinalarray[0] == "SC")
                //{
                //    serial = conn.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + DefaultSaleOrder.itemname + "'");
                //}
                //else if (strfinalarray[0] == "PO")
                //{
                //    serial = conn.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + DefaultSaleOrder.itemname + "'");
                //}

                //else if (strfinalarray[0] == "PC")
                //{
                //    serial = conn.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + DefaultSaleOrder.itemname + "'");
                //}
                else if (strfinalarray[0] == "SO")
                {
                    serial = conn.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + SalePurchaseOrderSimpleformate.itemname + "'");
                }
                else if (strfinalarray[0] == "SC")
                {
                    serial = conn.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + SalePurchaseOrderSimpleformate.itemname + "'");
                }
                else if (strfinalarray[0] == "PO")
                {
                    serial = conn.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + SalePurchaseOrderSimpleformate.itemname + "'");
                }

                else if (strfinalarray[0] == "PC")
                {
                    serial = conn.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + SalePurchaseOrderSimpleformate.itemname + "'");
                }
                DataTable dt = conn.getdataset("Select Batchno from ProductPriceMaster where Batchno='" + txtbatchno.Text + "' and ProductID='" + serial.Rows[0]["ProductID"].ToString()+ "'");
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Serial No is exists");
                    txtbatchno.Focus();
                    return;
                }
                if (strfinalarray[0] == "S")
                {
                     p = DefaultSale.productid;
                }
                else if (strfinalarray[0] == "SR")
                {
                     p = DefaultSale.productid;
                }
                else if (strfinalarray[0] == "P")
                {
                     p = DefaultSale.productid;
                }
                else if (strfinalarray[0] == "PR")
                {
                     p = DefaultSale.productid;
                }
                else if (strfinalarray[0] == "SO")
                {
                    p = SalePurchaseOrderSimpleformate.productid;
                }
                else if (strfinalarray[0] == "SC")
                {
                    p = SalePurchaseOrderSimpleformate.productid;
                }
                else if (strfinalarray[0] == "PO")
                {
                    p = SalePurchaseOrderSimpleformate.productid;
                }
                else if (strfinalarray[0] == "PC")
                {
                    p = SalePurchaseOrderSimpleformate.productid;
                }
              
                if (txtsaleprice.Text == "")
                {
                    txtsaleprice.Text = "0";
                }
                if (txtmrp.Text == "")
                {
                    txtmrp.Text = "0";
                }
                if (txtbasicprice.Text == "")
                {
                    txtbasicprice.Text = "0";
                }
                if (txtpurchaseprice.Text == "")
                {
                    txtpurchaseprice.Text = "0";
                }

                conn.execute("INSERT INTO [dbo].[ProductPriceMaster]([Productid],[Batchno],[BasicPrice],[SalePrice],[MRP],[PurchasePrice],[Barcode],[OpStock],[ExpDt],[mfgdt],[Expdays],[SelfVal],[minsaleprice],[oploose],[opstockval],[isactive])VALUES('"+p+"','"+txtbatchno.Text+"','"+txtbasicprice.Text+"','"+txtsaleprice.Text+"','"+txtmrp.Text+"','"+txtpurchaseprice.Text+"','"+txtbarcode.Text+"',0,'"+txtexpirydate.Text+"','"+txtmfgdate.Text+"','0','0','0','0','0.00','1')");
                if (strfinalarray[0] == "S")
                {
                    mdefaultSale.bindbatch();
                }
                else if (strfinalarray[0] == "SR")
                {
                    mdefaultSale.bindbatch();
                }
                else if (strfinalarray[0] == "P")
                {
                    mdefaultSale.bindbatch();
                }
                else if (strfinalarray[0] == "PR")
                {
                    mdefaultSale.bindbatch();
                }
                else if (strfinalarray[0] == "SO")
                {
                   // defaultSaleOrder.bindbatch();
                    salePurchaseOrderSimpleformate.bindbatch();
                }
                else if (strfinalarray[0] == "SC")
                {
                  //  defaultSaleOrder.bindbatch();
                    salePurchaseOrderSimpleformate.bindbatch();
                }
                else if (strfinalarray[0] == "PO")
                {
                   // defaultSaleOrder.bindbatch();
                    salePurchaseOrderSimpleformate.bindbatch();
                }
                else if (strfinalarray[0] == "PC")
                {
                 //   defaultSaleOrder.bindbatch();
                    salePurchaseOrderSimpleformate.bindbatch();
                }
               
                this.Close();
                if (a == 1)
                {
                    batchno = txtbatchno.Text;
                    mdefaultSale.bindbatchfromform(batchno);
                }
                if (a == 2)
                {
                    batchno = txtbatchno.Text;
                 //   defaultSaleOrder.bindbatchfromform(batchno);
                    salePurchaseOrderSimpleformate.bindbatchfromform(batchno);

                   // mdefaultSale.bindbatchfromform(batchno);
                }
                clearall();
            }
            catch
            {
            }
        }
        public void clearall()
        {
            txtbatchno.Text = "";
            txtbarcode.Text = "";
            txtsaleprice.Text = "";
            txtmrp.Text = "";
            txtbasicprice.Text = "";
            txtpurchaseprice.Text = "";
            txtexpirydate.Text = "";
            txtmfgdate.Text = "";
        }
        private void Batch_Load(object sender, EventArgs e)
        {
            try
            {
                this.ActiveControl=txtbatchno;
            }
            catch
            {
            }
        }

        private void txtpurchaseprice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtexpirydate.Focus();

            }
            if (e.KeyCode == Keys.Tab)
            {
                txtexpirydate.Focus();
            }
        }

        private void txtsaleprice_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtmrp_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtbasicprice_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtpurchaseprice_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtbatchno_Enter(object sender, EventArgs e)
        {
            txtbatchno.BackColor = Color.LightYellow;
        }

        private void txtbatchno_Leave(object sender, EventArgs e)
        {
            txtbatchno.BackColor = Color.White;
        }

        private void txtbarcode_Enter(object sender, EventArgs e)
        {
            txtbarcode.BackColor = Color.LightYellow;
        }

        private void txtbarcode_Leave(object sender, EventArgs e)
        {
            txtbarcode.BackColor = Color.White;
        }

        private void txtsaleprice_Enter(object sender, EventArgs e)
        {
            txtsaleprice.BackColor = Color.LightYellow;
        }

        private void txtsaleprice_Leave(object sender, EventArgs e)
        {
            txtsaleprice.BackColor = Color.White;
        }

        private void txtmrp_Enter(object sender, EventArgs e)
        {
            txtmrp.BackColor = Color.LightYellow;
        }

        private void txtmrp_Leave(object sender, EventArgs e)
        {
            txtmrp.BackColor = Color.White;
        }

        private void txtbasicprice_Enter(object sender, EventArgs e)
        {
            txtbasicprice.BackColor = Color.LightYellow;
        }

        private void txtbasicprice_Leave(object sender, EventArgs e)
        {
            txtbasicprice.BackColor = Color.White;
        }

        private void txtpurchaseprice_Enter(object sender, EventArgs e)
        {
            txtpurchaseprice.BackColor = Color.LightYellow;
        }

        private void txtpurchaseprice_Leave(object sender, EventArgs e)
        {
            txtpurchaseprice.BackColor = Color.White;
        }

        private void txtexpirydate_Enter(object sender, EventArgs e)
        {
            txtexpirydate.BackColor = Color.LightYellow;
        }

        private void txtexpirydate_Leave(object sender, EventArgs e)
        {
            txtexpirydate.BackColor = Color.White;
        }

        private void txtmfgdate_Enter(object sender, EventArgs e)
        {
            txtmfgdate.BackColor = Color.LightYellow;
        }

        private void txtmfgdate_Leave(object sender, EventArgs e)
        {
            txtmfgdate.BackColor = Color.White;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnsave_MouseEnter(object sender, EventArgs e)
        {
            btnsave.UseVisualStyleBackColor = false;
            btnsave.BackColor = Color.YellowGreen;
            btnsave.ForeColor = Color.White;
        }

        

        private void btnsave_MouseLeave(object sender, EventArgs e)
        {
            btnsave.UseVisualStyleBackColor = true;
            btnsave.BackColor = Color.FromArgb(51, 153, 255);
            btnsave.ForeColor = Color.White;
        }

        private void btnsave_Enter(object sender, EventArgs e)
        {
            btnsave.UseVisualStyleBackColor = false;
            btnsave.BackColor = Color.YellowGreen;
            btnsave.ForeColor = Color.White;
        }

        private void btnsave_Leave(object sender, EventArgs e)
        {
            btnsave.UseVisualStyleBackColor = true;
            btnsave.BackColor = Color.FromArgb(51, 153, 255);
            btnsave.ForeColor = Color.White;
        }

        private void btncancle_Enter(object sender, EventArgs e)
        {
            btncancle.UseVisualStyleBackColor = false;
            btncancle.BackColor = Color.FromArgb(248, 152, 94);
            btncancle.ForeColor = Color.White;
        }

        private void btncancle_Leave(object sender, EventArgs e)
        {
            btncancle.UseVisualStyleBackColor = true;
            btncancle.BackColor = Color.FromArgb(51, 153, 255);
            btncancle.ForeColor = Color.White;
        }

        private void btncancle_MouseEnter(object sender, EventArgs e)
        {
            btncancle.UseVisualStyleBackColor = false;
            btncancle.BackColor = Color.FromArgb(248, 152, 94);
            btncancle.ForeColor = Color.White;
        }

        private void btncancle_MouseLeave(object sender, EventArgs e)
        {
            btncancle.UseVisualStyleBackColor = true;
            btncancle.BackColor = Color.FromArgb(51, 153, 255);
            btncancle.ForeColor = Color.White;
        }












    }
}
