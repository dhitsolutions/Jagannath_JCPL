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
    public partial class SelectItemBatchWise : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        DataTable dtcn = new DataTable();
        private DefaultPOS defaultPOS;
        private string p;
        private string p_2;
        private DataGridView dgvitem;
        private DataTable temptable;
        public SelectItemBatchWise()
        {
            InitializeComponent();
        }

        public SelectItemBatchWise(DefaultPOS defaultPOS, string p)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.defaultPOS = defaultPOS;
            this.p = p;
            loadpage();
            binddata(p);
        }

        public SelectItemBatchWise(DefaultPOS defaultPOS, string p, string p_2)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.defaultPOS = defaultPOS;
            this.p = p;
            this.p_2 = p_2;
            loadpage();
            binddata(p);
        }

        public SelectItemBatchWise(DefaultPOS defaultPOS, string p, string p_2, DataGridView dgvitem)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.defaultPOS = defaultPOS;
            this.p = p;
            this.p_2 = p_2;
            this.dgvitem = dgvitem;
            loadpage();
            binddata(p);
        }

        public SelectItemBatchWise(DefaultPOS defaultPOS, string p, string p_2,  DataTable temptable)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.defaultPOS = defaultPOS;
            this.p = p;
            this.p_2 = p_2;
       //     this.dgvitem = dgvitem;
            this.temptable = temptable;
            loadpage();
            binddata(p);
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
            dtcn = conn.getdataset("select * from ProductPriceMaster where isactive=1 and ProductID='"+id+"'");
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
        public void getitem()
        {
            try
            {
                this.Close();
                string batch = LVFO.Items[LVFO.FocusedItem.Index].SubItems[0].Text;
                DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='POS' and setdefault=1");
              //  POS bd = new POS(this, batch);
                DefaultPOS p = new DefaultPOS(this, batch);
                if (dt1.Rows[0]["formname"].ToString() == p.Text)
                {
                    p.getdata(LVFO.Items[LVFO.FocusedItem.Index].SubItems[0].Text, p_2, temptable);
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
        private void LVFO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                getitem();
            }
        }

        private void LVFO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            getitem();
        }
    }
}
