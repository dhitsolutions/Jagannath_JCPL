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
    public partial class POSMultiProduct : Form
    {
        private POSNEW pOSNEW;
        private string p;
        private string Itemname;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        DataTable dtcn = new DataTable();
        string iname;
        public POSMultiProduct()
        {
            InitializeComponent();
        }

        public POSMultiProduct(POSNEW pOSNEW, string p, string Itemname)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.pOSNEW = pOSNEW;
            this.p = p;
            this.Itemname = Itemname;
            loadpage();
            binddata(p);
            iname = Itemname;
        }
        private void loadpage()
        {
            //   getcon();
            con.Open();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Location = new Point(0, 0);

            LVFO.Columns.Add("Batch", 50, HorizontalAlignment.Left);
            LVFO.Columns.Add("Barcode", 80, HorizontalAlignment.Left);
            LVFO.Columns.Add("Sale Price", 90, HorizontalAlignment.Left);
            LVFO.Columns.Add("M.R.P", 90, HorizontalAlignment.Left);
            con.Close();
        }
        public void binddata(string id)
        {
            ListViewItem li;
            LVFO.Items.Clear();
            dtcn = conn.getdataset("select * from ProductPriceMaster where isactive=1 and ProductID='" + id + "'");
            DataTable dtclient = new DataTable();

            if (dtcn != null && dtcn.Rows.Count > 0)
            {
                for (int i = 0; i < dtcn.Rows.Count; i++)
                {
                    li = LVFO.Items.Add(dtcn.Rows[i]["BatchNo"].ToString());
                    li.SubItems.Add(dtcn.Rows[i]["Barcode"].ToString());
                    li.SubItems.Add(dtcn.Rows[i]["SalePrice"].ToString());
                    li.SubItems.Add(dtcn.Rows[i]["MRP"].ToString());
                }

            }
            LVFO.Focus();
            LVFO.Items[0].Selected = true;

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {

                //DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (dr == DialogResult.Yes)
                //{
                this.Close();
                //}

                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void getitem()
        {
            try
            {
                this.Close();
                string batch = LVFO.Items[LVFO.FocusedItem.Index].SubItems[0].Text;
                DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='POS' and setdefault=1");
                //  POS bd = new POS(this, batch);
                POSNEW p = new POSNEW(this, batch);
                if (dt1.Rows[0]["formname"].ToString() == p.Text)
                {
                    p.getdata(LVFO.Items[LVFO.FocusedItem.Index].SubItems[0].Text,iname);
                    this.Close();
                }
                //else if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                //{

                //    //  bd.updatemode(LVFO.Items[LVFO.FocusedItem.Index].SubItems[0].Text);
                //    //master.AddNewTab(bd);

                //}
            }
            catch
            {
            }
        }
        private void LVFO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            getitem();
        }

        private void LVFO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                getitem();
            }
        }
    }
}
