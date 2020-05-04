using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RamdevSales
{
    public partial class ItemMasterPriceChange : Form
    {
        Connection cs = new Connection();
        double optotal, cltotal;
        DataTable dt = new DataTable();
        private Master master;
        private TabControl tabControl;
        public ItemMasterPriceChange()
        {
            InitializeComponent();
        }

        public ItemMasterPriceChange(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtqty.Text = "";
               // txtqty.Text = ((100 * Convert.ToDouble(txtamt.Text)) / 100).ToString();
            }
            catch
            {
            }
        }

        private void ItemMasterPriceChange_Load(object sender, EventArgs e)
        {
          //  dt = cs.getdataset("select p.productid as [Item Code],p.product_name as [Name of Item],c.companyname as [Company],pp.batchno as [Batch No],p.Unit,pp.saleprice as [Unit Sale Price]  from productmaster p Left join productpricemaster pp on pp.productid=p.productid inner join companymaster c on c.companyid=p.companyid order by p.product_name");
            bindgrid();
        }
        private void bindgrid()
        {
            optotal = 0;
            DataTable dt1 = cs.getdataset("select p.productid as [Item Code],p.product_name as [Name of Item],c.companyname as [Company],pp.batchno as [Batch No],p.Unit,pp.saleprice as [Unit Sale Price]  from productmaster p Left join productpricemaster pp on pp.productid=p.productid inner join companymaster c on c.companyid=p.companyid where p.isactive=1 and pp.isactive=1 order by p.product_name");
            // dt = dt1;
            grdstock.DataSource = dt1;
            grdstock.Columns[0].Width = 49;
            grdstock.Columns[1].Width = 300;
            grdstock.Columns[0].ReadOnly = true;
            grdstock.Columns[1].ReadOnly = true;
            grdstock.Columns[2].ReadOnly = true;
            grdstock.Columns[3].ReadOnly = true;
            grdstock.Columns[4].ReadOnly = true;
            grdstock.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

         //   openingtotal(dt1);
        }
        private void cmbterms_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtqty_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtqty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtamt.Text = "";
                
                //txtamt.Text = ((100 * Convert.ToDouble(txtqty.Text)) / 100).ToString();
            }
            catch
            {
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                if (grdstock.Rows.Count > 0)
                {
                    for (int i = 0; i < grdstock.Rows.Count; i++)
                    {
                        try
                        {

                            if (Convert.ToDouble(grdstock.Rows[i].Cells[5].Value) != Convert.ToDouble(dt.Rows[i][5].ToString()) )
                            {
                                cs.execute("update ProductPriceMaster set saleprice='" + grdstock.Rows[i].Cells[5].Value + "' where Productid='" + grdstock.Rows[i].Cells[0].Value + "' and batchno='" + grdstock.Rows[i].Cells[3].Value + "'");
                            }

                        }
                        catch (Exception ex)
                        {
                            // MessageBox.Show("Not Updated:" + ex.Message);
                        }
                    }
                    MessageBox.Show("Update Successfully");

                }
            }
            finally
            {
                this.Enabled = true;
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    master.RemoveCurrentTab();
                }

                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void grdstock_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (grdstock.CurrentCell.ColumnIndex == 5) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
        }
        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)
            && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbterms.Text == "+")
                {
                    if (txtamt.Text != "")
                    {
                        for (int i = 0; i < grdstock.Rows.Count; i++)
                        {
                            try
                            {
                                this.grdstock.Rows[i].Cells[5].Value = Math.Round(Convert.ToDouble(grdstock.Rows[i].Cells[5].Value) + Convert.ToDouble(txtamt.Text), 2);
                            }
                            catch
                            {
                            }
                        }
                    }
                    else if (txtqty.Text != "")
                    {
                        for (int i = 0; i < grdstock.Rows.Count; i++)
                        {
                            try
                            {
                                this.grdstock.Rows[i].Cells[5].Value = Math.Round(Convert.ToDouble(grdstock.Rows[i].Cells[5].Value) + ((Convert.ToDouble(grdstock.Rows[i].Cells[5].Value) * Convert.ToDouble(txtqty.Text)) / 100), 2);
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                else if (cmbterms.Text == "-")
                {
                    if (txtamt.Text != "")
                    {
                        for (int i = 0; i < grdstock.Rows.Count; i++)
                        {
                            try
                            {
                                this.grdstock.Rows[i].Cells[5].Value = Math.Round(Convert.ToDouble(grdstock.Rows[i].Cells[5].Value) - Convert.ToDouble(txtamt.Text), 2);
                            }
                            catch
                            {
                            }
                        }
                    }
                    else if (txtqty.Text != "")
                    {
                        for (int i = 0; i < grdstock.Rows.Count; i++)
                        {
                            try
                            {
                                this.grdstock.Rows[i].Cells[5].Value = Math.Round(Convert.ToDouble(grdstock.Rows[i].Cells[5].Value) - ((Convert.ToDouble(grdstock.Rows[i].Cells[5].Value) * Convert.ToDouble(txtqty.Text)) / 100), 2);
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        

        private void txtqty_Enter(object sender, EventArgs e)
        {
            txtqty.BackColor = Color.LightYellow;
        }

        private void txtqty_Leave(object sender, EventArgs e)
        {
            txtqty.BackColor = Color.White;
        }

        private void txtamt_Enter(object sender, EventArgs e)
        {
            txtamt.BackColor = Color.LightYellow;
        }

        private void txtamt_Leave(object sender, EventArgs e)
        {
            txtamt.BackColor = Color.White;
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

        private void btndelete_MouseEnter(object sender, EventArgs e)
        {
            btndelete.UseVisualStyleBackColor = false;
            btndelete.BackColor = Color.FromArgb(255, 77, 77);
            btndelete.ForeColor = Color.White;
        }

        private void btndelete_MouseLeave(object sender, EventArgs e)
        {
            btndelete.UseVisualStyleBackColor = true;
            btndelete.BackColor = Color.FromArgb(51, 153, 255);
            btndelete.ForeColor = Color.White;
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.UseVisualStyleBackColor = false;
            button1.BackColor = Color.FromArgb(236, 233, 216);
            button1.ForeColor = Color.White;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.UseVisualStyleBackColor = true;
            button1.BackColor = Color.FromArgb(51, 153, 255);
            button1.ForeColor = Color.White;
        }

        private void button1_Enter(object sender, EventArgs e)
        {
            button1.UseVisualStyleBackColor = false;
            button1.BackColor = Color.FromArgb(236, 233, 216);
            button1.ForeColor = Color.White;
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            button1.UseVisualStyleBackColor = true;
            button1.BackColor = Color.FromArgb(51, 153, 255);
            button1.ForeColor = Color.White;
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

        private void btndelete_Enter(object sender, EventArgs e)
        {
            btndelete.UseVisualStyleBackColor = false;
            btndelete.BackColor = Color.FromArgb(255, 77, 77);
            btndelete.ForeColor = Color.White;
        }

        private void btndelete_Leave(object sender, EventArgs e)
        {
            btndelete.UseVisualStyleBackColor = true;
            btndelete.BackColor = Color.FromArgb(51, 153, 255);
            btndelete.ForeColor = Color.White;
        }
        public static string s;
        private void cmbterms_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbterms.Items.Count; i++)
                {
                    s = cmbterms.GetItemText(cmbterms.Items[i]);
                    if (s == cmbterms.Text)
                    {
                        inList = true;
                        cmbterms.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbterms.Text = "";
                }
                button1.Focus();
            }
        }

        private void cmbterms_Leave(object sender, EventArgs e)
        {
            cmbterms.Text = s;
        }
    }
}
