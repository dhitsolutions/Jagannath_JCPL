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
using System.Web.UI.WebControls;

namespace RamdevSales
{
    public partial class PartyGroupwiseDiscount : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        OleDbSettings ods = new OleDbSettings();
        public PartyGroupwiseDiscount()
        {
            InitializeComponent();
        }

        public PartyGroupwiseDiscount(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }

        private void PartyGroupwiseDiscount_Load(object sender, EventArgs e)
        {
            bindcustomer();
            bindcompany();
            Button17.Visible = false;
            //set the interval  and start the timer
           // timer1.Interval = 1000;
           // timer1.Start();
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
        private void bindgridlist()
        {
            if (cmbpattern.SelectedItem.ToString() == "Item Wise")
            {
                DataTable dt = conn.getdataset("select distinct partyname as Party,itemcompanyname as Company from partyrates");
                grdlist.DataSource = dt;
                grdlist.ReadOnly = true;
            }
            else if (cmbpattern.SelectedItem.ToString() == "Item Group wise")
            {
                DataTable dt = conn.getdataset("select distinct partyname as Party from PartyGroupDiscount");
                grdlist.DataSource = dt;
                grdlist.ReadOnly = true;
            }
            else if (cmbpattern.SelectedItem.ToString() == "Company Wise")
            {
                DataTable dt = conn.getdataset("select distinct partyname as Party from PartyCompanyDiscount");
                grdlist.DataSource = dt;
                grdlist.ReadOnly = true;
            }

          

        }

        private void bindcompany()
        {
            SqlCommand cmd1 = new SqlCommand("select companyid, companyname from Companymaster order by companyname", con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            cmbcompanynm.ValueMember = "companyid";
            cmbcompanynm.DisplayMember = "companyname";
            cmbcompanynm.DataSource = dt1;
            cmbcompanynm.SelectedIndex = -1;
        }

        private void bindcustomer()
        {
            SqlCommand cmd1 = new SqlCommand("select ClientID,AccountName from ClientMaster where (groupid=99 or groupid=100) and isactive=1 order by AccountName", con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            cmbpartynm.ValueMember = "ClientID";
            cmbpartynm.DisplayMember = "AccountName";
            DataRow row = dt1.NewRow();
            row["AccountName"] = "---ALL PARTIES---";
            dt1.Rows.InsertAt(row, 0);
            cmbpartynm.DataSource = dt1;
            
            cmbpartynm.SelectedIndex = -1;
        }
        
        private void cmbpartynm_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbpartynm.Items.Count; i++)
                {
                    s = cmbpartynm.GetItemText(cmbpartynm.Items[i]);
                    if (s == cmbpartynm.Text)
                    {
                        inList = true;
                        cmbpartynm.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbpartynm.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void cmbpartynm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbpartynm.Items.Count; i++)
                {
                    s = cmbpartynm.GetItemText(cmbpartynm.Items[i]);
                    if (s == cmbpartynm.Text)
                    {
                        inList = true;
                        cmbpartynm.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbpartynm.Text = "";
                }


                if (cmbpattern.SelectedItem.ToString() == "Item Wise")
                {
                    bindgridlist();
                    cmbcompanynm.Focus();
                }
                else if (cmbpattern.SelectedItem.ToString() == "Item Group wise")
                {
                    bindgridlist();
                    binditemgroupwise();
                }
                else if (cmbpattern.SelectedItem.ToString() == "Company Wise")
                {
                    bindgridlist();
                    binditemcompanywise();
                }

            }
        }

        private void binditemcompanywise()
        {
            DataTable isav = new DataTable();
            try
            {
                isav = conn.getdataset("select distinct partyname as Party,itemcompanyname from PartycompanyDiscount where partyname like '%" + cmbpartynm.Text + "%'");
            }
            catch
            {
            }
            if (isav.Rows.Count > 0)
            {
                MousecompanyDouble();
            }
            else
            {
                DataTable main = new DataTable();
                main.Columns.Add("Company ID", typeof(string));
                main.Columns.Add("Company Name", typeof(string));
                main.Columns.Add("Discount Rate", typeof(string));
                main.Columns.Add("Discount %", typeof(string));


                DataTable dt = conn.getdataset("select * from companymaster order by Companyname");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    main.Rows.Add(dt.Rows[i]["CompanyId"].ToString(),dt.Rows[i]["Companyname"].ToString(), '0', '0');
                }
                grdstock.DataSource = null;
                grdstock.DataSource = main;


                grdstock.Columns[0].Width = 49;
                grdstock.Columns[1].Width = 300;
                grdstock.Columns[2].Width = 160;
                grdstock.Columns[3].Width = 160;


                grdstock.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                grdstock.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                grdstock.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                grdstock.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                grdstock.Columns[0].ReadOnly = true;
                grdstock.Columns[1].ReadOnly = true;

                BtnPayment.Text = "Save";
            }
        }
        private void MousecompanyDouble()
        {
            DataTable dt = conn.getdataset("select partyid, partyname,Itemcompanyid, Itemcompanyname, SpecialRate, Discount from PartycompanyDiscount where partyname like '%" + grdlist.Rows[grdlist.CurrentCell.RowIndex].Cells[0].Value + "%'");
            cmbcompanynm.Text = dt.Rows[0]["Itemcompanyname"].ToString();
            cmbpartynm.Text = dt.Rows[0]["partyname"].ToString();
            DataTable main = new DataTable();
            main.Columns.Add("Company ID", typeof(string));
            main.Columns.Add("Company Name", typeof(string));
            main.Columns.Add("Discount Rate", typeof(string));
            main.Columns.Add("Discount %", typeof(string));


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                main.Rows.Add(dt.Rows[i]["ItemCompanyId"].ToString(), dt.Rows[i]["ItemCompanyname"].ToString(), dt.Rows[i]["SpecialRate"].ToString(), dt.Rows[i]["Discount"].ToString());
            }
            grdstock.DataSource = null;
            grdstock.DataSource = main;

            grdstock.Columns[0].Width = 49;
            grdstock.Columns[1].Width = 300;
            grdstock.Columns[2].Width = 160;
            grdstock.Columns[3].Width = 160;


            grdstock.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grdstock.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grdstock.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdstock.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            grdstock.Columns[0].ReadOnly = true;
            grdstock.Columns[1].ReadOnly = true;

            BtnPayment.Text = "Update";
        }   
        

        private void binditemgroupwise()
        {
            DataTable isav = new DataTable();
            try
            {
                isav = conn.getdataset("select distinct partyname as Party,itemgroupname from PartyGroupDiscount where partyname like '%" + cmbpartynm.Text + "%' and itemgroupname='" + cmbcompanynm.Text + "'");
            }
            catch
            {
            }
            if (isav.Rows.Count > 0)
            {
                MousegroupDouble();
            }
            else
            {
                DataTable main = new DataTable();
                main.Columns.Add("Group Name", typeof(string));
                main.Columns.Add("Discount Rate", typeof(string));
                main.Columns.Add("Discount %", typeof(string));


                DataTable dt = conn.getdataset("select distinct groupname from productmaster order by groupname");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    main.Rows.Add(dt.Rows[i]["groupname"].ToString(), '0', '0');
                }

                grdstock.DataSource = main;

          
                grdstock.Columns[0].Width = 349;
                grdstock.Columns[1].Width = 160;
                grdstock.Columns[2].Width = 160;

                grdstock.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                grdstock.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                grdstock.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                grdstock.Columns[0].ReadOnly = true;
               
                BtnPayment.Text = "Save";
            }
        }
        private void MousegroupDouble()
        {
            DataTable dt = conn.getdataset("select partyid, partyname,Itemgroupid, Itemgroupname, SpecialRate, Discount from PartyGroupDiscount where partyname like '%" + grdlist.Rows[grdlist.CurrentCell.RowIndex].Cells[0].Value + "%'");
            cmbcompanynm.Text = dt.Rows[0]["Itemgroupname"].ToString();
            cmbpartynm.Text = dt.Rows[0]["partyname"].ToString();
            DataTable main = new DataTable();
            main.Columns.Add("Group Name", typeof(string));
            main.Columns.Add("Discount Rate", typeof(string));
            main.Columns.Add("Discount %", typeof(string));


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                main.Rows.Add(dt.Rows[i]["Itemgroupname"].ToString(), dt.Rows[i]["SpecialRate"].ToString(), dt.Rows[i]["Discount"].ToString());
            }
            grdstock.DataSource = null;
            grdstock.DataSource = main;

            grdstock.Columns[0].Width = 349;
            grdstock.Columns[1].Width = 160;
            grdstock.Columns[2].Width = 160;

            grdstock.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grdstock.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdstock.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            grdstock.Columns[0].ReadOnly = true;

            BtnPayment.Text = "Update";
        }
        private void cmbcompanynm_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbcompanynm.Items.Count; i++)
                {
                    s = cmbcompanynm.GetItemText(cmbcompanynm.Items[i]);
                    if (s == cmbcompanynm.Text)
                    {
                        inList = true;
                        cmbcompanynm.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbcompanynm.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void cmbcompanynm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbcompanynm.Items.Count; i++)
                {
                    s = cmbcompanynm.GetItemText(cmbcompanynm.Items[i]);
                    if (s == cmbcompanynm.Text)
                    {
                        inList = true;
                        cmbcompanynm.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbcompanynm.Text = "";
                }



                DataTable isav = new DataTable();
                try
                {
                   isav = conn.getdataset("select distinct partyname as Party,itemcompanyname as Company from partyrates where partyname like '%" + cmbpartynm.Text + "%' and itemcompanyname='" + cmbcompanynm.Text + "'");
                }
               catch
                {
                }
                if (isav.Rows.Count > 0)
                {
                    MouseDouble();
                }
                else
                {
                    DataTable main = new DataTable();
                    main.Columns.Add("product id", typeof(string));
                    main.Columns.Add("Product Name", typeof(string));
                    main.Columns.Add("Batch", typeof(string));
                    main.Columns.Add("MRP", typeof(string));
                    main.Columns.Add("Special Rate", typeof(string));
                    main.Columns.Add("Discount %", typeof(string));


                    DataTable dt = conn.getdataset("select p.productid,p.product_name,pp.batchno,pp.mrp from productmaster p inner join productpricemaster pp on p.productid=pp.productid where pp.isactive=1 and p.companyid='" + cmbcompanynm.SelectedValue + "' order by p.product_name");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        main.Rows.Add(dt.Rows[i]["productid"].ToString(), dt.Rows[i]["product_name"].ToString(), dt.Rows[i]["batchno"].ToString(), dt.Rows[i]["mrp"].ToString(), '0', '0');
                    }

                    grdstock.DataSource = main;

                    grdstock.Columns[0].Width = 49;
                    grdstock.Columns[1].Width = 300;
                    grdstock.Columns[2].Width = 70;
                    grdstock.Columns[3].Width = 70;
                    grdstock.Columns[4].Width = 90;
                    grdstock.Columns[5].Width = 90;

                    grdstock.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    grdstock.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    grdstock.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    grdstock.Columns[0].ReadOnly = true;
                    grdstock.Columns[1].ReadOnly = true;
                    grdstock.Columns[2].ReadOnly = true;
                    grdstock.Columns[3].ReadOnly = true;
                    BtnPayment.Text = "Save";
                }
            }
        }

        private void grdstock_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (grdstock.CurrentCell.ColumnIndex == 4 || grdstock.CurrentCell.ColumnIndex == 5) //Desired Column
            {
                System.Windows.Forms.TextBox tb = e.Control as System.Windows.Forms.TextBox;
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

        private void grdstock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                try
                {

                    //int columnindex = grdstock.CurrentCell.ColumnIndex;
                    //int rowindex = grdstock.CurrentCell.RowIndex;

                    //if (columnindex < grdstock.ColumnCount - 1)
                    //{
                    //    grdstock.CurrentCell = grdstock.Rows[rowindex].Cells[columnindex + 1];
                    //}
                    //else
                    //{
                    //    grdstock.CurrentCell = grdstock.Rows[rowindex+1].Cells[columnindex - 1];
                    //}
                    //resetRow = false;
                }
                catch
                {
                }
            }
        }
        private int currentRow,currentcell;
        private bool resetRow = false;
        private Master master;
        private TabControl tabControl;
        private void grdstock_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            resetRow = true;
            currentRow = e.RowIndex;
            currentcell = e.ColumnIndex;
            grdstock.Rows[currentRow].Cells[currentcell].Value = Math.Round(Convert.ToDouble(grdstock.Rows[currentRow].Cells[currentcell].Value), 2).ToString("N2");
        }

        private void grdstock_SelectionChanged(object sender, EventArgs e)
        {
            if (resetRow)
            {
                resetRow = false;
                grdstock.CurrentCell = grdstock.Rows[currentRow].Cells[currentcell];
            }

        }

        private void BtnPayment_Click(object sender, EventArgs e)
        {
            if (BtnPayment.Text == "Update")
            {
                if (cmbpattern.SelectedItem.ToString() == "Item Wise")
                {
                    for (int i = 0; i < grdstock.Rows.Count; i++)
                    {
                        conn.execute("UPDATE [dbo].[PartyRates] SET [PartyID] = '" + cmbpartynm.SelectedValue + "',[PartyName] = '" + cmbpartynm.Text + "',[ItemCompanyID] = '" + cmbcompanynm.SelectedValue + "',[ItemCompanyName] = '" + cmbcompanynm.Text + "',[ItemID] = '" + grdstock.Rows[i].Cells["product id"].Value + "',[ItemName] = '" + grdstock.Rows[i].Cells["Product Name"].Value + "',[SalePrice] = '" + grdstock.Rows[i].Cells["MRP"].Value + "',[SpecialRate] = '" + grdstock.Rows[i].Cells["Special Rate"].Value.ToString().Replace(',', ' ') + "',[Discount] = '" + grdstock.Rows[i].Cells["Discount %"].Value.ToString().Replace(',', ' ') + "',[OT1] = '" + grdstock.Rows[i].Cells["Batch"].Value + "' where partyname like '%" + grdlist.Rows[grdlist.CurrentCell.RowIndex].Cells[0].Value + "%' and itemcompanyname='" + grdlist.Rows[grdlist.CurrentCell.RowIndex].Cells[1].Value + "' and itemid='" + grdstock.Rows[i].Cells["product id"].Value + "'");
                    }
                   
                }
                else if (cmbpattern.SelectedItem.ToString() == "Item Group wise")
                {
                    for (int i = 0; i < grdstock.Rows.Count; i++)
                    {
                        conn.execute("UPDATE [dbo].[PartyGroupDiscount] SET [PartyID] = '" + cmbpartynm.SelectedValue + "',[PartyName] = '" + cmbpartynm.Text + "',[itemgroupid] = '0',[ItemGroupName] = '" + grdstock.Rows[i].Cells["Group Name"].Value + "',[SpecialRate] = '" + grdstock.Rows[i].Cells["Discount Rate"].Value + "',[Discount] = '" + grdstock.Rows[i].Cells["Discount %"].Value + "'where partyname like '%" + grdlist.Rows[grdlist.CurrentCell.RowIndex].Cells[0].Value + "%' and itemgroupname='" + grdstock.Rows[i].Cells["Group Name"].Value + "'");
                    }
                }
                else if (cmbpattern.SelectedItem.ToString() == "Company Wise")
                {
                    for (int i = 0; i < grdstock.Rows.Count; i++)
                    {
                        conn.execute("UPDATE [dbo].[PartyCompanyDiscount] SET [PartyID] = '" + cmbpartynm.SelectedValue + "',[PartyName] = '" + cmbpartynm.Text + "',[itemcompanyid] = '" + grdstock.Rows[i].Cells["Company ID"].Value + "',[ItemcompanyName] = '" + grdstock.Rows[i].Cells["Company Name"].Value + "',[SpecialRate] = '" + grdstock.Rows[i].Cells["Discount Rate"].Value + "',[Discount] = '" + grdstock.Rows[i].Cells["Discount %"].Value + "'where partyname like '%" + grdlist.Rows[grdlist.CurrentCell.RowIndex].Cells[0].Value + "%' and itemcompanyname='" + grdstock.Rows[i].Cells["Company Name"].Value + "' and [itemcompanyid] = '" + grdstock.Rows[i].Cells["Company ID"].Value + "'");
                    }
                }

                BtnPayment.Text = "Save";
            }
            else
            {
                if (cmbpattern.SelectedItem.ToString() == "Item Wise")
                {
                    for (int i = 0; i < grdstock.Rows.Count; i++)
                    {
                        conn.execute("INSERT INTO [dbo].[PartyRates]([PartyID],[PartyName],[ItemCompanyID],[ItemCompanyName],[ItemID],[ItemName],[SalePrice],[SpecialRate],[Discount],[OT1]) values ('" + cmbpartynm.SelectedValue + "','" + cmbpartynm.Text + "','" + cmbcompanynm.SelectedValue + "','" + cmbcompanynm.Text + "','" + grdstock.Rows[i].Cells["product id"].Value + "','" + grdstock.Rows[i].Cells["Product Name"].Value + "','" + grdstock.Rows[i].Cells["MRP"].Value + "','" + grdstock.Rows[i].Cells["Special Rate"].Value + "','" + grdstock.Rows[i].Cells["Discount %"].Value + "','" + grdstock.Rows[i].Cells["Batch"].Value + "')");
                    }

                }
                else if (cmbpattern.SelectedItem.ToString() == "Item Group wise")
                {
                    for (int i = 0; i < grdstock.Rows.Count; i++)
                    {
                        conn.execute("INSERT INTO [dbo].[PartyGroupDiscount]([PartyID],[PartyName],[Itemgroupid],[ItemGroupName],[SpecialRate],[Discount]) values ('" + cmbpartynm.SelectedValue + "','" + cmbpartynm.Text + "','0','" + grdstock.Rows[i].Cells["Group Name"].Value + "','" + grdstock.Rows[i].Cells["Discount Rate"].Value + "','" + grdstock.Rows[i].Cells["Discount %"].Value + "')");
                    }
                }
                else if (cmbpattern.SelectedItem.ToString() == "Company Wise")
                {
                    for (int i = 0; i < grdstock.Rows.Count; i++)
                    {
                        conn.execute("INSERT INTO [dbo].[PartyCompanyDiscount]([PartyID],[PartyName],[Itemcompanyid],[ItemCompanyName],[SpecialRate],[Discount]) values ('" + cmbpartynm.SelectedValue + "','" + cmbpartynm.Text + "','" + grdstock.Rows[i].Cells["Company ID"].Value + "','" + grdstock.Rows[i].Cells["Company Name"].Value + "','" + grdstock.Rows[i].Cells["Discount Rate"].Value + "','" + grdstock.Rows[i].Cells["Discount %"].Value + "')");
                    }
                }
                
            }
            bindgridlist();
            grdstock.DataSource = null;
            cmbcompanynm.Text = "";
            cmbpartynm.Text = "";
           
        }

        private void grdlist_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (cmbpattern.SelectedItem.ToString() == "Item Wise")
            {
                MouseDouble();
            }
            else if (cmbpattern.SelectedItem.ToString() == "Item Group wise")
            {
                MousegroupDouble();
            }
            else if (cmbpattern.SelectedItem.ToString() == "Company Wise")
            {
                MousecompanyDouble();
            }
            
        }

        private void MouseDouble()
        {
            DataTable dt = conn.getdataset("select partyid, partyname,Itemcompanyid, Itemcompanyname, Itemid, Itemname,OT1,SalePrice, SpecialRate, Discount from partyrates where partyname like '%" + grdlist.Rows[grdlist.CurrentCell.RowIndex].Cells[0].Value + "%' and itemcompanyname='" + grdlist.Rows[grdlist.CurrentCell.RowIndex].Cells[1].Value + "'");
            cmbcompanynm.Text = dt.Rows[0]["Itemcompanyname"].ToString();
            cmbpartynm.Text = dt.Rows[0]["partyname"].ToString();
            DataTable main = new DataTable();
            main.Columns.Add("product id", typeof(string));
            main.Columns.Add("Product Name", typeof(string));
            main.Columns.Add("Batch", typeof(string));
            main.Columns.Add("MRP", typeof(string));
            main.Columns.Add("Special Rate", typeof(string));
            main.Columns.Add("Discount %", typeof(string));


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                main.Rows.Add(dt.Rows[i]["Itemid"].ToString(), dt.Rows[i]["Itemname"].ToString(), dt.Rows[i]["OT1"].ToString(), dt.Rows[i]["SalePrice"].ToString(), dt.Rows[i]["SpecialRate"].ToString(), dt.Rows[i]["Discount"].ToString());
            }
            grdstock.DataSource = null;
            grdstock.DataSource = main;

            grdstock.Columns[0].Width = 49;
            grdstock.Columns[1].Width = 300;
            grdstock.Columns[2].Width = 70;
            grdstock.Columns[3].Width = 70;
            grdstock.Columns[4].Width = 90;
            grdstock.Columns[5].Width = 90;

            grdstock.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdstock.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdstock.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            grdstock.Columns[0].ReadOnly = true;
            grdstock.Columns[1].ReadOnly = true;
            grdstock.Columns[2].ReadOnly = true;
            grdstock.Columns[3].ReadOnly = true;

            BtnPayment.Text = "Update";
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbpattern.SelectedItem.ToString() == "Item Wise")
                {
                    conn.execute("delete from partyrates where partyname like '%" + cmbpartynm.Text + "%' and itemcompanyname='" + cmbcompanynm.Text + "'");
                    MessageBox.Show("Delete successfully");
                    bindgridlist();
                    grdstock.DataSource = null;
                    cmbcompanynm.Text = "";
                    cmbpartynm.Text = "";
                }
                else if (cmbpattern.SelectedItem.ToString() == "Item Group wise")
                {
                    conn.execute("delete from partygroupdiscount where partyname like '%" + cmbpartynm.Text + "%' and itemgroupname='" + cmbcompanynm.Text + "'");
                    MessageBox.Show("Delete successfully");
                    bindgridlist();
                    grdstock.DataSource = null;
                    cmbcompanynm.Text = "";
                    cmbpartynm.Text = "";
                }
                else if (cmbpattern.SelectedItem.ToString() == "Company Wise")
                {
                    conn.execute("delete from partycompanydiscount where partyname like '%" + cmbpartynm.Text + "%' and itemcompanyname='" + cmbcompanynm.Text + "'");
                    MessageBox.Show("Delete successfully");
                    bindgridlist();
                    grdstock.DataSource = null;
                    cmbcompanynm.Text = "";
                    cmbpartynm.Text = "";
                }

            }
            catch
            {
            }
        }

        private void cmbpattern_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbpattern.SelectedItem == "Item Wise")
            {
                lblcompany.Visible = true;
                cmbpartynm.Visible = true;
                lblpartyname.Visible = true;
                cmbcompanynm.Visible = true;
            }
            else if (cmbpattern.SelectedItem == "Item Group wise")
            {
                lblpartyname.Visible = true;
                cmbpartynm.Visible = true;
                lblcompany.Visible = false;
                cmbcompanynm.Visible = false;
            }
            else if (cmbpattern.SelectedItem == "Company Wise")
            {
                lblpartyname.Visible = true;
                cmbpartynm.Visible = true;
                lblcompany.Visible = false;
                cmbcompanynm.Visible = false;
            }
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbpattern.Items.Count; i++)
                {
                    s = cmbpattern.GetItemText(cmbpattern.Items[i]);
                    if (s == cmbpattern.Text)
                    {
                        inList = true;
                        cmbpattern.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbpattern.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }
        public static string s;
        private void cmbpattern_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true; // This will eliminate the beeping
                    bool inList = false;
                    for (int i = 0; i < cmbpattern.Items.Count; i++)
                    {
                        s = cmbpattern.GetItemText(cmbpattern.Items[i]);
                        if (s == cmbpattern.Text)
                        {
                            inList = true;
                            cmbpattern.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        cmbpattern.Text = "";
                    }

                }
                cmbpartynm.Focus();
            }
        }

        

        private void grdlist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnPayment_MouseEnter(object sender, EventArgs e)
        {
            BtnPayment.UseVisualStyleBackColor = false;
            BtnPayment.BackColor = Color.YellowGreen;
            BtnPayment.ForeColor = Color.White;
        }

        private void BtnPayment_MouseLeave(object sender, EventArgs e)
        {
            BtnPayment.UseVisualStyleBackColor = true;
            BtnPayment.BackColor = Color.FromArgb(51, 153, 255);
            BtnPayment.ForeColor = Color.White;
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

        private void Button17_Click(object sender, EventArgs e)
        {

        }

        private void BtnPayment_Enter(object sender, EventArgs e)
        {
            BtnPayment.UseVisualStyleBackColor = false;
            BtnPayment.BackColor = Color.YellowGreen;
            BtnPayment.ForeColor = Color.White;
        }

        private void BtnPayment_Leave(object sender, EventArgs e)
        {
            BtnPayment.UseVisualStyleBackColor = true;
            BtnPayment.BackColor = Color.FromArgb(51, 153, 255);
            BtnPayment.ForeColor = Color.White;
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
        string searchstr;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //empty the string for every 1 seconds
           // searchstr = "";
        }

        private void cmbpattern_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbpattern.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbpattern.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void cmbpartynm_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbpartynm.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbpartynm.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void cmbcompanynm_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbcompanynm.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbcompanynm.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }

        private void btnclose_Enter(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = false;
            btnclose.BackColor = Color.FromArgb(248, 152, 94);
            btnclose.ForeColor = Color.White;
        }

        private void btnclose_Leave(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = true;
            btnclose.BackColor = Color.FromArgb(51, 153, 255);
            btnclose.ForeColor = Color.White;
        }

        private void btnclose_MouseEnter(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = false;
            btnclose.BackColor = Color.FromArgb(248, 152, 94);
            btnclose.ForeColor = Color.White;
        }

        private void btnclose_MouseLeave(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = true;
            btnclose.BackColor = Color.FromArgb(51, 153, 255);
            btnclose.ForeColor = Color.White;
        }

        private void cmbpattern_Leave(object sender, EventArgs e)
        {
            cmbpattern.Text = s;
        }

        private void cmbpartynm_Leave(object sender, EventArgs e)
        {
            cmbpartynm.Text = s;
        }

        private void cmbcompanynm_Leave(object sender, EventArgs e)
        {
            cmbcompanynm.Text = s;
        }

        
    }
}
