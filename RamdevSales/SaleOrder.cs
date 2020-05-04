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
using System.Diagnostics;

namespace RamdevSales
{

    public partial class SaleOrder : Form
    {
        OleDbSettings ods = new OleDbSettings();
        public DataSet ds;
        public DataTable dt, dtpo;
        private Master master;
        private TabControl tabControl;
        DataTable dtcn = new DataTable();
        DataTable dtprod = new DataTable();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
        public String lvid = string.Empty;
        public String lvid1 = string.Empty;
        public String type = string.Empty;
        public SaleOrder()
        {
            InitializeComponent();
        }
        //private SaleOfOrder saleOfOrder;
        private DefaultSale defaultSale;
        public SaleOrder(string id, DefaultSale defaultSale1)
        {
            InitializeComponent();
            loadpage();
            binddata(id);
            //saleOfOrder = saleOfOrder1 as SaleOfOrder;
            defaultSale = defaultSale1 as DefaultSale;
        }

        public SaleOrder(string p, DefaultSale defaultSale, string[] strfinalarray)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            loadpage();
            this.p = p;
            this.defaultSale = defaultSale;
            this.strfinalarray = strfinalarray;
            binddata(p);
        }

        public SaleOrder(string p, Master master, string[] strfinalarray)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            loadpage();
            this.p = p;
            this.master = master;
            this.strfinalarray = strfinalarray;
            binddata(p);
        }

        public SaleOrder(string p, DefaultSaleOrder defaultSaleOrder, string[] strfinalarray)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            loadpage();
            this.p = p;
            this.defaultSaleOrder = defaultSaleOrder;
            this.strfinalarray = strfinalarray;
            binddata(p);
        }

        public SaleOrder(string p, SalePurchaseOrderSimpleformate salePurchaseOrderSimpleformate, string[] strfinalarray)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            loadpage();
            this.p = p;
            this.salePurchaseOrderSimpleformate = salePurchaseOrderSimpleformate;
            this.strfinalarray = strfinalarray;
            binddata(p);
        }

        public string EnteredText
        {
            get
            {
                return ("");
            }
        }

        private void SaleOrder_Load(object sender, EventArgs e)
        {
            try
            {

                //   loadpage();

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
        public void getcon()
        {
            ds = ods.getdata("Select * from SQLSetting where DBName='Server'");
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {

                string qry = dt.Rows[0][6].ToString();
                con = new SqlConnection(qry);

            }
            else
            {
                //AddConString frm = new AddConString();
                //frm.Show();
            }
        }

        private void loadpage()
        {
            //   getcon();
            con.Open();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Location = new Point(0, 0);

            LVFO.CheckBoxes = true;
            LVFO.Columns.Add("",20, HorizontalAlignment.Left);
            LVFO.Columns.Add("Order No", 80, HorizontalAlignment.Left);
            LVFO.Columns.Add("Date",90, HorizontalAlignment.Left);
            LVFO.Columns.Add("Type", 50, HorizontalAlignment.Left);
            LVFO.Columns.Add("Items", 50, HorizontalAlignment.Left);
            LVFO.Columns.Add("Party Name",0, HorizontalAlignment.Left);
            LVFO.Columns.Add("Challan Amt",90, HorizontalAlignment.Left);
            LVFO.Columns.Add("Clientid", 0, HorizontalAlignment.Left);
            LVFO.Columns.Add("Billno", 0, HorizontalAlignment.Left);
            con.Close();
        }

        public static string compid;
        string biltype;
        public void binddata(string id)
        {
            ListViewItem li;
            LVFO.Items.Clear();
            if (strfinalarray[0] == "SC")
            {
                biltype = "SO";
            }
            else if (strfinalarray[0] == "PC")
            {
                biltype = "PO";
            }
            else if (strfinalarray[0] == "S")
            {
                biltype = "S";
            }
            else if (strfinalarray[0] == "P")
            {
                biltype = "P";
            }

            //dtcn = conn.getdataset("Select po.VchNo,po.OrderNo,po.OrderDate,c.SubName,po.TotalQty from SaleMaster po inner join Company c on c.CompanyId=po.CompanyId where po.isactive=1 and po.CompanyId=" + id + " and po.OrderStatus='Pending'");
            //dtcn = conn.getdataset("Select po.VchNo,po.OrderNo,po.OrderDate,c.PurchaseTypename,po.TotalQty,po.clientid from PurchaseOrderMaster po inner join PurchaseTypeMaster c on c.PurchaseTypeid=po.PurchaseType where po.isactive=1 and po.ClientId=" + id + " and po.OrderStatus='Pending'");
            //select b.OrderNo,convert(varchar(11), b.OrderDate, 113)as OrderDate, c.subname,c.address,b.totalqty,b.CompanyId from PurchaseOrderMaster b inner join Company c on c.CompanyId=b.CompanyId  where b.isactive=1
            // dtcn = conn.getdataset("Select b.VchNo,b.OrderNo,convert(varchar(11), b.OrderDate, 113)as OrderDate, c.subname,c.address,b.totalqty,b.CompanyId from PurchaseOrderMaster b inner join Company c on c.CompanyId=b.CompanyId where b.isactive=1 and b.OrderStatus='Pending'");
            dtcn = conn.getdataset("select b.PO_NO,b.billtype,convert(varchar(11), b.Bill_Date)as Date, c.printname,c.address,b.totalqty,b.totalnet,b.ClientID,b.billno from SaleOrderMaster b inner join Clientmaster c on c.clientid=b.ClientID where b.isactive=1 and b.OrderStatus='Pending'and b.BillType like '%" + biltype + "%' and b.ClientID='" + id + "'");
            DataTable dtclient = new DataTable();

            if (dtcn != null && dtcn.Rows.Count > 0)
            {
                for (int i = 0; i < dtcn.Rows.Count; i++)
                {
                    li = LVFO.Items.Add("");
                    li.SubItems.Add(dtcn.Rows[i]["billno"].ToString());
                    li.SubItems.Add(Convert.ToDateTime(dtcn.Rows[i]["Date"].ToString()).ToString(Master.dateformate));
                    li.SubItems.Add(dtcn.Rows[i]["billtype"].ToString());
                    li.SubItems.Add(dtcn.Rows[i]["TotalQty"].ToString());
                    li.SubItems.Add(dtcn.Rows[i]["printname"].ToString());
                    li.SubItems.Add(dtcn.Rows[i]["totalnet"].ToString());
                    li.SubItems.Add(dtcn.Rows[i]["ClientID"].ToString());
                    li.SubItems.Add(dtcn.Rows[i]["billno"].ToString());
                }

            }


        }

        private void BtnPayment_Click(object sender, EventArgs e)
        {
            setform();
        }
        public static string partyname, orderno;
        private string p;
        private string[] strfinalarray;
        private DefaultSaleOrder defaultSaleOrder;
        public int clientid;
        private SalePurchaseOrderSimpleformate salePurchaseOrderSimpleformate;

        public void setform()
        {
            this.Close();
            String str = LVFO.Items[LVFO.FocusedItem.Index].SubItems[1].Text;
            DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
            DefaultSale bd = new DefaultSale(this, master, LVFO.Items[LVFO.FocusedItem.Index].SubItems[3].Text);
            DefaultSaleOrder bd1 = new DefaultSaleOrder(this, master, LVFO.Items[LVFO.FocusedItem.Index].SubItems[3].Text);
            SalePurchaseOrderSimpleformate bd2 = new SalePurchaseOrderSimpleformate(this, master, LVFO.Items[LVFO.FocusedItem.Index].SubItems[3].Text);
          //  Sale p = new Sale(this, master, tabControl);
          
            string[] a = new string[LVFO.CheckedItems.Count];
            string[] b = new string[LVFO.CheckedItems.Count];
            string[] c = new string[LVFO.CheckedItems.Count];
            for (int i = 0,j=0; i < LVFO.Items.Count; i++)
            {
                //if (LVFO.Items[i].Checked == true)
                //{
                if (Convert.ToBoolean(LVFO.Items[i].Checked) == true)
                {
                    lvid = LVFO.Items[i].SubItems[8].Text;
                    lvid1 = LVFO.Items[i].SubItems[1].Text;
                    type = LVFO.Items[i].SubItems[3].Text;
                    clientid = Convert.ToInt32(LVFO.Items[i].SubItems[7].Text);
                    a[j] = lvid;
                    b[j] = type;
                    c[j] = lvid1;
                    j++;
                }

            }
            if (dt1.Rows[0]["formname"].ToString() == bd.Text)
            {
                defaultSale.getdata(a, a, clientid, b, c);
                //   master.AddNewTab(bd);
            }
            else if (dt1.Rows[0]["formname"].ToString() == bd1.Text)
            {
                defaultSaleOrder.getdata(a, a, clientid, b, c);
            }
            else if (dt1.Rows[0]["formname"].ToString() == bd2.Text)
            {
                salePurchaseOrderSimpleformate.getdata(a, a, clientid, b, c);
            }
            //else if (dt1.Rows[0]["formname"].ToString() == p.Text)
            //{
            //  //  p.updatemode(str, LVFO.Items[LVFO.FocusedItem.Index].SubItems[1].Text, 1);
            // //   master.AddNewTab(p);
            //}
        }
        private void btnsubmit()
        {
            try
            {

                //string[] a = new string[LVFO.Items.Count];
                //string[] b = new string[LVFO.Items.Count];
                //string[] c = new string[LVFO.Items.Count];
                //string[] d = new string[LVFO.Items.Count];
                // for (int i = 0; i < LVFO.Items.Count; i++)
                //{
                //    if (LVFO.Items[i].Checked == true)
                //    {
                //        lvid = LVFO.Items[i].SubItems[1].Text;
                //        compid = LVFO.Items[i].SubItems[7].Text;
                //        partyname = LVFO.Items[i].SubItems[4].Text;
                //        orderno = LVFO.Items[i].SubItems[2].Text;
                //        a[i] = lvid;
                //        b[i] = compid;
                //        c[i] = partyname;
                //        d[i] = orderno;
                //    }
                //}

                //  this.defaultSale.getVchNos(a, b, c,d);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void LVFO_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnPayment.Focus();
                }
            }
            catch
            {
            }
        }

        private void chkselectall_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkselectall.Checked == true)
                {
                    for (int i = 0; i < LVFO.Items.Count; i++)
                    {
                        LVFO.Items[i].Checked = true;
                    }
                }
                else
                {
                    for (int i = 0; i < LVFO.Items.Count; i++)
                    {
                        LVFO.Items[i].Checked = false;
                    }
                }
            }
            catch
            {
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
               // master.RemoveCurrentTab();
                this.Close();
            }
        }

        private void btnClose_Enter(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = false;
            btnClose.BackColor = Color.FromArgb(248, 152, 94);
            btnClose.ForeColor = Color.White;
        }

        private void btnClose_Leave(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = true;
            btnClose.BackColor = Color.FromArgb(51, 153, 255);
            btnClose.ForeColor = Color.White;
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = false;
            btnClose.BackColor = Color.FromArgb(248, 152, 94);
            btnClose.ForeColor = Color.White;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = true;
            btnClose.BackColor = Color.FromArgb(51, 153, 255);
            btnClose.ForeColor = Color.White;
        }

        private void btnPayment_Enter(object sender, EventArgs e)
        {
            btnPayment.UseVisualStyleBackColor = false;
            btnPayment.BackColor = Color.FromArgb(20, 209, 82);
            btnPayment.ForeColor = Color.White;
        }

        private void btnPayment_Leave(object sender, EventArgs e)
        {
            btnPayment.UseVisualStyleBackColor = true;
            btnPayment.BackColor = Color.FromArgb(51, 153, 255);
            btnPayment.ForeColor = Color.White;
        }

        private void btnPayment_MouseEnter(object sender, EventArgs e)
        {
            btnPayment.UseVisualStyleBackColor = false;
            btnPayment.BackColor = Color.FromArgb(20, 209, 82);
            btnPayment.ForeColor = Color.White;
        }

        private void btnPayment_MouseLeave(object sender, EventArgs e)
        {
            btnPayment.UseVisualStyleBackColor = true;
            btnPayment.BackColor = Color.FromArgb(51, 153, 255);
            btnPayment.ForeColor = Color.White;
        }

        private void button1_Enter(object sender, EventArgs e)
        {
            button1.UseVisualStyleBackColor = false;
            button1.BackColor = Color.FromArgb(128, 128, 128);
            button1.ForeColor = Color.White;
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            button1.UseVisualStyleBackColor = true;
            button1.BackColor = Color.FromArgb(51, 153, 255);
            button1.ForeColor = Color.White;
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.UseVisualStyleBackColor = false;
            button1.BackColor = Color.FromArgb(128, 128, 128);
            button1.ForeColor = Color.White;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.UseVisualStyleBackColor = true;
            button1.BackColor = Color.FromArgb(51, 153, 255);
            button1.ForeColor = Color.White;
        }

    }
}
