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
using System.Security.Cryptography;
using System.IO;
using LoggingFramework;

namespace RamdevSales
{
    public partial class DefaultPOS : Form
    {
        public string constr = ConfigurationManager.ConnectionStrings["qry"].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        //SqlConnection constr = new SqlConnection(ConfigurationManager.ConnectionStrings["pos"].ToString());
        AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
        Connection sql = new Connection();
        Printing prn = new Printing();
        DataTable dt, dt1, dt3, dt4, dt5, dt6 = new DataTable();
        DataTable discount = new DataTable();
        Double t4, t5, t6, t7, t8, t9, t10;
        Double t12, t14, t15, t16, t18, n, o, p, q, g5, g3, g4, g6, g7, g8, g9, tax, cess, g12;
        string gdis1, total1, Dprice, tax12;
        string g1, g2, g10;
        string maxbillno;
        private string id;
        int flagmultyitem = 0;
        string cname, address, address2, city, cusname, iname, cusmobile, phone, tc;
        string qty, unitprice, totalamt, icount, totalqty, gtotal, gdisamt, disprice;
        public static int countqry = 0;
        public static string a = "";
        public static string b = "";
        public static string str = "";
        public static string key = "";
        public static string gtotalamt = "";
        public static string batchno = "";
        public static string saletype = "";
        public static string statusreg = string.Empty;
        double disper;
        double disamt;
        DataSet ds = new DataSet();
        public static DataTable temptable = new DataTable();
        DataTable options = new DataTable();
        public DefaultPOS()
        {
            InitializeComponent();
        }

        public DefaultPOS(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization

            InitializeComponent();
            temptable = new DataTable();
            temptable.Columns.Add("BARCODE", typeof(string));
            temptable.Columns.Add("ITEMNAME", typeof(string));
            temptable.Columns.Add("QTY", typeof(string));
            temptable.Columns.Add("RATE", typeof(string));
            temptable.Columns.Add("BASIC AMT", typeof(string));
            temptable.Columns.Add("DISCOUNT PER", typeof(string));
            temptable.Columns.Add("DISCOUNT AMT", typeof(string));
            temptable.Columns.Add("GST AMT", typeof(string));
            temptable.Columns.Add("ADD TAX", typeof(string));
            temptable.Columns.Add("AMOUNT", typeof(string));
            temptable.Columns.Add("Batchno", typeof(string));
            temptable.Columns.Add("cess", typeof(string));
            temptable.Columns.Add("AgentID", typeof(string));
            dgvitem.AllowUserToAddRows = false;
            DataRow dr = temptable.NewRow();
            dr["BARCODE"] = "";
            dr["ITEMNAME"] = "";
            dr["QTY"] = "";
            dr["RATE"] = "";
            dr["BASIC AMT"] = "";
            dr["DISCOUNT PER"] = "";
            dr["DISCOUNT AMT"] = "";
            dr["GST AMT"] = "";
            dr["ADD TAX"] = "";
            dr["AMOUNT"] = "";
            dr["Batchno"] = "";
            dr["cess"] = "";
            dr["AgentID"] = "";
            temptable.Rows.Add(dr);
            dgvitem.DataSource = temptable;
            this.master = master;
            this.tabControl = tabControl;
        }

        public DefaultPOS(SelectItemBatchWise selectItemBatchWise, string batch)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.selectItemBatchWise = selectItemBatchWise;
            this.batch = batch;
        }

        public DefaultPOS(POSBillList pOSBillList, Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.pOSBillList = pOSBillList;
            this.master = master;
            this.tabControl = tabControl;
        }
        public void billno()
        {
            maxbillno = sql.getsinglevalue("select max (BillId)+1 from BillPOSMaster where isactive=1");
            if (maxbillno == "")
            {
                maxbillno = "1";
            }

            str = maxbillno.ToString();
        }
        public void binaagent()
        {
            string qry = "";
            qry = "select ClientID,AccountName from ClientMaster where isactive=1 and groupID=50 order by AccountName";
            SqlCommand cmd1 = new SqlCommand(qry, con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            cmbagentname.ValueMember = "ClientID";
            cmbagentname.DisplayMember = "AccountName";
            cmbagentname.DataSource = dt1;
            cmbagentname.SelectedIndex = -1;
        }
        DataTable userrights = new DataTable();
        private void DefaultPOS_Load(object sender, EventArgs e)
        {
            try
            {
                userrights = sql.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[9]["d"].ToString() == "False")
                    {
                        btndelete.Enabled = false;
                    }
                }
                options = sql.getdataset("select * from options");
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(0, 0);
                //  temptable = new DataTable();

                //txtbarcode.Focus();
                lbldate.Text = Convert.ToString(DateTime.Now);
                lblsaletype.Text = options.Rows[0]["autosaletype"].ToString();
               
                //txtdisamt.Enabled = false;
                //txtqty.Enabled = false;
                //txtgtotal.Enabled = false;
                //txtstotal.Enabled = false;
                if (btnnosale.Text == "Update")
                {
                    GridDesign();

                    if (Convert.ToBoolean(options.Rows[0]["showallitemlistinpos"].ToString()) == true)
                    {
                        lvallitem.Columns.Add("Product Name", 700, HorizontalAlignment.Left);
                        //  pnlallitem.Visible = true;
                        // bindallitem();
                    }
                    else
                    {
                        //  pnlallitem.Visible = false;
                        // autoextender();
                    }
                    dgvitem.Columns[0].Visible = false;
                    dgvitem.Columns[1].Width = 300;
                    dgvitem.Columns[2].Width = 100;
                    dgvitem.Columns[3].Width = 100;
                    dgvitem.Columns[5].Width = 130;
                    dgvitem.Columns[6].Width = 100;
                    dgvitem.Columns[7].Width = 100;
                    dgvitem.Columns[8].Width = 100;
                    dgvitem.Columns[9].Width = 150;
                    dgvitem.Columns[10].Visible = false;
                    dgvitem.Columns[11].Visible = false;
                   dgvitem.Columns[12].Visible = false;
                    dgvitem.Columns[0].ReadOnly = true;
                    dgvitem.Columns[1].ReadOnly = true;
                    dgvitem.Columns[3].ReadOnly = true;
                    dgvitem.Columns[4].ReadOnly = true;
                    dgvitem.Columns[5].ReadOnly = true;
                    dgvitem.Columns[6].ReadOnly = true;
                    dgvitem.Columns[7].ReadOnly = true;
                    dgvitem.Columns[8].ReadOnly = true;
                    dgvitem.Columns[9].ReadOnly = true;
                    DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                    dgvitem.Columns.Add(btn);
                    btn.HeaderText = "Delete";
                    btn.Text = "";
                    btn.Name = "btndelete";
                    btn.UseColumnTextForButtonValue = true;
                    dgvitem.Columns[13].Width = 38;
                    todaydate.CustomFormat = Master.dateformate;

                }
                else
                {
                    binaagent();
                    if (Convert.ToBoolean(options.Rows[0]["invoicenoinpos"].ToString()) == true)
                    {
                        try
                        {
                            if (options.Rows[0]["posbillno"].ToString() == "Continuous")
                            {
                                String str = sql.ExecuteScalar("select max(BillId) from BillPOSMaster where isactive='1'");
                                DataTable dt = sql.getdataset("select * from [PurchasetypeMaster] where isactive=1 and Purchasetypename='" + lblsaletype.Text + "'");
                                Int64 id, count;
                                if (str == "")
                                {

                                    id = Convert.ToInt64(1);
                                    count = Convert.ToInt64(1);
                                }
                                else
                                {
                                    id = Convert.ToInt32(str) + 1;
                                    count = Convert.ToInt32(str) + 1;
                                }
                                // lblbill_no.Text = count.ToString();
                                lblinvoice.Visible = true;
                                lblinvoice.Text = count.ToString();
                                lblinv.Visible = true;

                            }
                            else
                            {
                                string saletype = sql.ExecuteScalar("select Purchasetypeid from PurchasetypeMaster where isactive=1 and Purchasetypename='" + lblsaletype.Text + "'");
                                String str = sql.ExecuteScalar("select max(BillId) from BillPOSMaster where isactive='1' and saletypeid='" + saletype + "'");
                                DataTable dt = sql.getdataset("select * from [PurchasetypeMaster] where isactive=1 and Purchasetypename='" + lblsaletype.Text + "'");
                                Int64 id, count;
                                //     Object data = dr[1];

                                if (str == "")
                                {

                                    id = Convert.ToInt64(dt.Rows[0]["startingno"].ToString());
                                    count = Convert.ToInt64(dt.Rows[0]["startingno"].ToString());
                                }
                                else
                                {
                                    id = Convert.ToInt32(str) + 1;
                                    count = Convert.ToInt32(str) + 1;
                                }
                                // lblbill_no.Text = count.ToString();
                                lblinvoice.Visible = true;
                                lblinvoice.Text = dt.Rows[0]["prefix"].ToString() + count.ToString();
                                lblinv.Visible = true;


                            }
                        }
                        catch
                        {
                        }

                    }
                    temptable = new DataTable();
                    temptable.Columns.Add("BARCODE", typeof(string));
                    temptable.Columns.Add("ITEMNAME", typeof(string));
                    temptable.Columns.Add("QTY", typeof(string));
                    temptable.Columns.Add("RATE", typeof(string));
                    temptable.Columns.Add("BASIC AMT", typeof(string));
                    temptable.Columns.Add("DISCOUNT PER", typeof(string));
                    temptable.Columns.Add("DISCOUNT AMT", typeof(string));
                    temptable.Columns.Add("GST AMT", typeof(string));
                    temptable.Columns.Add("ADD TAX", typeof(string));
                    temptable.Columns.Add("AMOUNT", typeof(string));
                    temptable.Columns.Add("Batchno", typeof(string));
                    temptable.Columns.Add("cess", typeof(string));
                    temptable.Columns.Add("AgentID", typeof(string));
                    lvallitem.Columns.Add("Product Name", 700, HorizontalAlignment.Left);
                    GridDesign();
                    dgvitem.DataSource = temptable;
                    dgvitem.Columns[0].Visible = false;
                    dgvitem.Columns[1].Width = 300;
                    dgvitem.Columns[2].Width = 100;
                    dgvitem.Columns[3].Width = 100;
                    dgvitem.Columns[5].Width = 130;
                    dgvitem.Columns[6].Width = 100;
                    dgvitem.Columns[7].Width = 100;
                    dgvitem.Columns[8].Width = 100;
                    dgvitem.Columns[9].Width = 150;
                    dgvitem.Columns[10].Visible = false;
                    dgvitem.Columns[11].Visible = false;
                   dgvitem.Columns[12].Visible = false;

                    todaydate.Value = DateTime.Now;
                    todaydate.CustomFormat = Master.dateformate;
                }
                //if (Convert.ToBoolean(options.Rows[0]["showallitemlistinpos"].ToString()) == true)
                //{
                //   // pnlallitem.Visible = true;
                //    bindallitem();
                //}
                //else
                //{
                //   // pnlallitem.Visible = false;
                //    autoextender();
                //}
                if (Convert.ToBoolean(options.Rows[0]["requiredcustomerdetailinpos"].ToString()) == true)
                {
                    this.ActiveControl = txtcname;
                    txtcname.Enabled = true;
                    txtcity.Enabled = true;
                    txtcmobile.Enabled = true;
                }
                else
                {
                    this.ActiveControl = txtbarcode;
                    txtcname.Enabled = false;
                    txtcity.Enabled = false;
                    txtcmobile.Enabled = false;
                }



            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
        private void txtbarcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // lvallitem.Items[0].Selected = true;
                SqlCommand cmd = new SqlCommand("select Product_Name from productmaster where Product_Name like'%" + txtbarcode.Text + "%' and isactive=1", con);
                SqlDataAdapter sda6 = new SqlDataAdapter(cmd);
                DataTable dtitem = new DataTable();
                sda6.Fill(dtitem);
                lvallitem.Items.Clear();
                for (int i = 0; i < dtitem.Rows.Count; i++)
                {
                    ListViewItem li;
                    li = lvallitem.Items.Add(dtitem.Rows[i]["Product_Name"].ToString());
                }
                //  lvallitem.Focus();
                if (txtbarcode.Text == "" && txtbarcode.Text == null)
                {
                    bindallitem();
                }
            }
            catch
            {
            }
        }

        public void binditem()
        {
            try
            {
                //   const string s ="*123*";
                // string u = s.Replace("*", "");
                string v = txtbarcode.Text.Replace("*", "");
                txtbarcode.Text = v;
                if (txtbarcode.Text == "")
                {
                    MessageBox.Show("Select Item");
                }
                else
                {
                    DataTable dt = sql.getdataset("select p.ProductID,pp.* from ProductMaster p inner join ProductPriceMaster pp on p.Productid=pp.Productid where p.isactive=1 and pp.isactive=1 and  (p.Product_Name='" + txtbarcode.Text + "'or pp.barcode='" + txtbarcode.Text + "')");
                    if (dt.Rows.Count == 1)
                    {

                        batchno = dt.Rows[0]["batchno"].ToString();
                        temptableenter();
                        if (btnnosale.Text != "Update")
                        {
                            billno();
                        }
                        //dgvitem.Columns[0].Visible = false;
                        GridDesign();
                    }
                    else
                    {
                        SelectItemBatchWise si = new SelectItemBatchWise(this, dt.Rows[0]["ProductID"].ToString(), txtbarcode.Text, temptable);
                        si.ShowDialog();
                        // if (txtbarcode.Text != temptable.Rows[0]["BARCODE"].ToString() || txtbarcode.Text != temptable.Rows[0]["ITEMNAME"].ToString())
                        // {
                        if (flagmultyitem == 0)
                        {
                            if (temptable.Rows.Count == 1)
                            {
                                flagmultyitem = 1;
                                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                                dgvitem.Columns.Add(btn);
                                btn.HeaderText = "Delete";
                                btn.Text = "";
                                btn.Name = "btndelete";
                                btn.UseColumnTextForButtonValue = true;
                                dgvitem.Columns[13].Width = 38;
                            }
                        }
                        //  }

                        GridDesign();
                        totaltaxbox();
                        txtbarcode.Text = "";
                        txtbarcode.Focus();


                    }
                }
            }
            catch
            {
            }
        }
        public void enteritem()
        {
            try
            {
                pnlallitem.Visible = false;
                binditem();
                if (Convert.ToBoolean(options.Rows[0]["showallitemlistinpos"].ToString()) == true)
                {
                    pnlallitem.Visible = true;
                    bindallitem();
                }
                else
                {
                    pnlallitem.Visible = false;
                    // autoextender();
                }
                this.ActiveControl = txtbarcode;
                #region
                //if (txtbarcode.Text == "")
                //{
                //    MessageBox.Show("Select Item");
                //}
                //else
                //{

                //    if (batchno == "")
                //    {
                //        batchno = "NA";
                //    }
                //    DataTable dt = sql.getdataset("select p.ProductID,pp.* from ProductMaster p inner join ProductPriceMaster pp on p.Productid=pp.Productid where   p.Product_Name='" + txtbarcode.Text + "'or pp.barcode='" + txtbarcode.Text + "' and pp.BatchNo='" + batchno + "' and p.isactive=1 and pp.isactive=1");
                //    //   DataTable dt1 = sql.getdataset("select * from ProductPriceMaster where isactive=1 and productID='"+dt.Rows[0]["Product_Name"].ToString()+"'");
                //    if (batchno != "NA")
                //    {
                //        temptableenter();
                //        // totaltaxbox();
                //        dgvitem.Columns[0].Visible = false;
                //        if (btnnosale.Text != "Update")
                //        {
                //            billno();
                //        }
                //        GridDesign();
                //        batchno = "";
                //    }
                //    else
                //    {
                //        if (dt.Rows[0]["BatchNo"].ToString() == "NA")
                //        {
                //            temptableenter();
                //            // totaltaxbox();
                //            dgvitem.Columns[0].Visible = false;
                //            if (btnnosale.Text != "Update")
                //            {
                //                billno();
                //            }
                //            GridDesign();
                //            batchno = "";

                //        }
                //        else
                //        {
                //            SelectItemBatchWise si = new SelectItemBatchWise(this, dt.Rows[0]["ProductID"].ToString());
                //            si.ShowDialog();
                //        }
                //    }

                //}
                #endregion
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }
        private void txtbarcode_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Down)
                {
                    try
                    {
                        lvallitem.Items[0].Selected = true;
                        lvallitem.Select();
                    }
                    catch
                    {
                    }
                }
                if (e.Alt == true && e.KeyCode == Keys.D)
                {
                    txtadddisamt.Focus();
                }
                if (e.KeyData == Keys.Enter)
                {
                    if (options.Rows[0]["requiragentnameinpos"].ToString() == "Ask For Agent After Item Entry")
                    {
                        pnlagent.Visible = true;
                        cmbagentname.Focus();
                    }
                    else if (options.Rows[0]["requiragentnameinpos"].ToString() == "Ask For Agent Bill Wise")
                    {
                        if (flagforagent == 0)
                        {
                            pnlagent.Visible = true;
                            cmbagentname.Focus();
                            flagforagent = 1;
                        }
                        else
                        {
                            enteritem();
                        }
                    }
                    else
                    {
                        enteritem();
                    }
                   // enteritem();
                }
                if (e.KeyData == Keys.F6)
                {
                    //savedata();
                    //// PrintToPrinter();
                    //key = "F6";
                    //monytype m = new monytype();
                    //m.Show();
                    //dgvitem.DataSource = null;

                    //temptable = new DataTable();
                    //clearall();


                }
                //if (e.KeyData == Keys.F1)
                //{
                //    try
                //    {

                //        //txtbarcode.Focus();
                //        dgvitem.Focus();
                //       // dgvitem.Enabled = true;
                //        e.Handled = true;
                //        DataGridViewCell cell = dgvitem.Rows[0].Cells[2];
                //        dgvitem.CurrentCell = cell;
                //        dgvitem.BeginEdit(true);



                //        dgvitem.Columns[0].Visible = false;
                //        dgvitem.Columns[0].ReadOnly = true;
                //        dgvitem.Columns[1].ReadOnly = true;
                //        dgvitem.Columns[3].ReadOnly = true;
                //        dgvitem.Columns[4].ReadOnly = true;
                //        dgvitem.Columns[5].ReadOnly = true;

                //        txtbarcode.Text = "";

                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show(ex.Message);
                //    }
                //}
                //if (e.KeyData == Keys.F2)
                //{
                //    try
                //    {
                //        master.RemoveCurrentTab();

                //    }
                //    catch (Exception ex)
                //    {
                //       // MessageBox.Show(ex.Message);
                //    }
                //}
                //if (e.KeyData == (Keys.ControlKey | Keys.I))
                //{
                //    try
                //    {

                //        Itemmaster m = new Itemmaster(master, tabControl);
                //        master.AddNewTab(m);

                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show(ex.Message);
                //    }
                //}
                //if (e.KeyData == (Keys.ControlKey | Keys.A))
                //{
                //    try
                //    {

                //        ClientRegistration am = new ClientRegistration(master, tabControl);
                //        master.AddNewTab(am);

                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show(ex.Message);
                //    }
                //}

                //if (e.KeyData == Keys.Alt+S)
                //{
                //    key = "F3";
                //    savedata();
                //    dgvitem.DataSource = null;

                //    temptable = new DataTable();
                //    clearall();
                //    monytype m = new monytype();
                //    m.Show();

                //}
                if (e.KeyData == Keys.Escape)
                {
                    try
                    {

                        this.Close();

                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                    }
                }
                if (e.KeyData == Keys.F1)
                {
                    try
                    {

                        txtbarcode.Focus();

                    }
                    catch (Exception ex)
                    {
                        //  MessageBox.Show(ex.Message);
                    }
                }
                //if (e.KeyData == Keys.F8)
                //{
                //    try
                //    {
                //        dgvitem.Focus();
                //        //dgvitem.Enabled = true;
                //        dgvitem.Rows[0].Selected = true;


                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show(ex.Message);
                //    }
                //}
                //if (e.KeyData == Keys.F10)
                //{
                //    try
                //    {

                //       // dgvitem.Enabled = false;
                //        //temptable.Clear();
                //        //dgvitem.DataSource = temptable;
                //        //temptable = new DataTable();
                //        clearall();

                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show(ex.Message);
                //    }
                //}
            }
        }
        public void clearall()
        {

            //temptable = new DataTable();
            //temptable.Columns.Add("BARCODE", typeof(string));
            //temptable.Columns.Add("ITEMNAME", typeof(string));
            //temptable.Columns.Add("QTY", typeof(string));
            //temptable.Columns.Add("RATE", typeof(string));
            //temptable.Columns.Add("BASIC AMT", typeof(string));
            //temptable.Columns.Add("DISCOUNT PER", typeof(string));
            //temptable.Columns.Add("DISCOUNT AMT", typeof(string));
            //temptable.Columns.Add("GST AMT", typeof(string));
            //temptable.Columns.Add("ADD TAX", typeof(string));
            //temptable.Columns.Add("AMOUNT", typeof(string));
            //temptable.Columns.Add("Batchno", typeof(string));
            // temptable.Columns.Add("Delete", typeof(string));

            GridDesign();
            temptable.Clear();
            dgvitem.DataSource = temptable;
            dgvitem.AutoGenerateColumns = false;
            dgvitem.Columns.Remove("btndelete");
            //dgvitem.Columns[0].Visible = false;
            //dgvitem.Columns[1].Width = 300;
            //dgvitem.Columns[2].Width = 100;
            //dgvitem.Columns[3].Width = 100;
            //dgvitem.Columns[5].Width = 130;
            //dgvitem.Columns[6].Width = 100;
            //dgvitem.Columns[7].Width = 100;
            //dgvitem.Columns[8].Width = 100;
            //dgvitem.Columns[9].Width = 150;
            //dgvitem.Columns[10].Visible = false;
            //dgvitem.Columns[11].Width = 40;
            str = string.Empty;
            txtbarcode.Text = "";
            txtstotal.Text = "";
            txtqty.Text = "";
            txtdisamt.Text = "";
            txtgtotal.Text = "";
            txtadddisamt.Text = "";
            txtcess.Text = "";
            txttotaltax.Text = "";
            txttotalitem.Text = "";
            txtaddtax.Text = "";
            txtcmobile.Text = "";
            txtcity.Text = "";
            txtcname.Text = "";
            lblroundof.Text = "";
            lblroundof.Visible = false;
            lblro.Visible = false;
            flagmultyitem = 0;
            flagforagent = 0;
            if (Convert.ToBoolean(options.Rows[0]["requiredcustomerdetailinpos"].ToString()) == true)
            {
                this.ActiveControl = txtcname;
                txtcname.Enabled = true;
                txtcity.Enabled = true;
                txtcmobile.Enabled = true;
            }
            else
            {
                this.ActiveControl = txtbarcode;
                txtcname.Enabled = false;
                txtcity.Enabled = false;
                txtcmobile.Enabled = false;
            }


        }
        public void GridDesign()
        {

            this.dgvitem.DefaultCellStyle.Font = new Font("Calibri", 11, FontStyle.Regular);
            dgvitem.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", 11.25f, FontStyle.Bold);
            DataGridViewCellStyle fooCellStyle = new DataGridViewCellStyle();
            DataGridViewHeaderCell f = new DataGridViewHeaderCell();

            dgvitem.ColumnHeadersDefaultCellStyle.BackColor = Color.Maroon;
            dgvitem.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvitem.DefaultCellStyle.ForeColor = Color.White;
            dgvitem.EnableHeadersVisualStyles = false;

            //DataGridViewColumn dataGridViewColumn = dgvitem.Columns[2];
            //dataGridViewColumn.HeaderCell.Style.BackColor = Color.Green;


            foreach (DataGridViewRow row in dgvitem.Rows)
            {
                row.Cells[1].Style.Font = new Font("Calibri", 11, FontStyle.Bold);
                row.Cells[1].Style.BackColor = Color.LightGray;
                row.Cells[1].Style.ForeColor = Color.Black;
                row.Cells[2].Style.Font = new Font("Calibri", 11, FontStyle.Bold);
                row.Cells[2].Style.BackColor = Color.Cyan;
                row.Cells[2].Style.ForeColor = Color.Black;
                row.Cells[3].Style.Font = new Font("Calibri", 11, FontStyle.Bold);
                row.Cells[3].Style.BackColor = Color.LightGray;
                row.Cells[3].Style.ForeColor = Color.Black;
                row.Cells[4].Style.Font = new Font("Calibri", 11, FontStyle.Bold);
                row.Cells[4].Style.BackColor = Color.LightGray;
                row.Cells[4].Style.ForeColor = Color.Black;
                row.Cells[5].Style.Font = new Font("Calibri", 11, FontStyle.Bold);
                row.Cells[5].Style.BackColor = Color.LightGray;
                row.Cells[5].Style.ForeColor = Color.Black;
                row.Cells[6].Style.Font = new Font("Calibri", 11, FontStyle.Bold);
                row.Cells[6].Style.BackColor = Color.LightGray;
                row.Cells[6].Style.ForeColor = Color.Black;
                row.Cells[7].Style.Font = new Font("Calibri", 11, FontStyle.Bold);
                row.Cells[7].Style.BackColor = Color.LightGray;
                row.Cells[7].Style.ForeColor = Color.Black;
                row.Cells[8].Style.Font = new Font("Calibri", 11, FontStyle.Bold);
                row.Cells[8].Style.BackColor = Color.LightGray;
                row.Cells[8].Style.ForeColor = Color.Black;
                row.Cells[9].Style.Font = new Font("Calibri", 11, FontStyle.Bold);
                row.Cells[9].Style.BackColor = Color.LightGray;
                row.Cells[9].Style.ForeColor = Color.Black;
                row.Cells[12].Style.Font = new Font("Calibri", 11, FontStyle.Bold);
                row.Cells[12].Style.BackColor = Color.LightGray;
                row.Cells[12].Style.ForeColor = Color.Black;
            }

            // dgvitem.Enabled = false;

        }
        string billnowithprifix;
        void getsr()
        {
            try
            {
                string saletype = sql.ExecuteScalar("select Purchasetypeid from PurchasetypeMaster where isactive=1 and Purchasetypename='" + lblsaletype.Text + "'");
                String str = sql.ExecuteScalar("select max(BillId) from BillPOSMaster where isactive='1' and saletypeid='" + saletype + "'");
                DataTable dt = sql.getdataset("select * from [PurchasetypeMaster] where isactive=1 and Purchasetypename='" + lblsaletype.Text + "'");
                Int64 id, count;
                //     Object data = dr[1];

                if (str == "")
                {

                    id = Convert.ToInt64(dt.Rows[0]["startingno"].ToString());
                    count = Convert.ToInt64(dt.Rows[0]["startingno"].ToString());
                }
                else
                {
                    id = Convert.ToInt32(str) + 1;
                    count = Convert.ToInt32(str) + 1;
                }
                // lblbill_no.Text = count.ToString();
                billnowithprifix = dt.Rows[0]["prefix"].ToString() + count.ToString();

            }
            catch
            {
            }
            finally
            {

            }

        }
        public void bindbillno()
        {
            try
            {
                if (options.Rows[0]["posbillno"].ToString() == "Continuous")
                {
                    String str = sql.ExecuteScalar("select max(BillId) from BillPOSMaster where isactive='1'");
                    DataTable dt = sql.getdataset("select * from [PurchasetypeMaster] where isactive=1 and Purchasetypename='" + lblsaletype.Text + "'");
                    Int64 id, count;
                    if (str == "")
                    {

                        id = Convert.ToInt64(1);
                        count = Convert.ToInt64(1);
                    }
                    else
                    {
                        id = Convert.ToInt32(str) + 1;
                        count = Convert.ToInt32(str) + 1;
                    }
                    // lblbill_no.Text = count.ToString();
                    billnowithprifix = dt.Rows[0]["prefix"].ToString() + count.ToString();
                }
                else
                {
                    getsr();
                }

            }
            catch
            {
            }
        }
        string agentid = "";
        public void savedata()
        {
            try
            {
                if (btnnosale.Text == "Update")
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[9]["u"].ToString() == "True")
                        {
                            // bindbillno();

                            String billno1 = sql.ExecuteScalar("select billno from BillPOSProductMaster where isactive=1 and BillId='" + id + "'");
                            sql.execute("Update BillPOSProductMaster set isactive=0 where BillId='" + id + "'");
                            string totaltax1 = string.Empty;
                            int count = dgvitem.Rows.Count;
                            foreach (DataGridViewRow row in dgvitem.Rows)
                            {

                                g1 = row.Cells[0].Value.ToString();// barcode code
                                g2 = row.Cells[1].Value.ToString(); // itamname
                                g3 = Convert.ToDouble(row.Cells[2].Value.ToString()); // qty
                                g4 = Convert.ToDouble(row.Cells[3].Value.ToString()); // rate
                                g5 = Convert.ToDouble(row.Cells[4].Value.ToString()); // basicamt
                                g6 = Convert.ToDouble(row.Cells[5].Value.ToString()); // disper
                                g7 = Convert.ToDouble(row.Cells[6].Value.ToString()); // disamt
                                g8 = Convert.ToDouble(row.Cells[7].Value.ToString()); // totaltax
                                g9 = Convert.ToDouble(row.Cells[9].Value.ToString()); // Tax
                                g10 = row.Cells[10].Value.ToString(); // Batch
                                g12 = Convert.ToDouble(row.Cells[11].Value.ToString()); // cess
                                agentid = row.Cells[12].Value.ToString(); // agentid

                                double pric = g5;
                                double aTruncated = Math.Truncate(pric * 100) / 100;
                                string pric1 = string.Format("{0:0.00}", aTruncated);

                                double dec = g6;
                                double aTruncated1 = Math.Truncate(dec * 100) / 100;
                                string dec1 = string.Format("{0:0.00}", aTruncated1);

                                double total = g7;
                                double aTruncated2 = Math.Truncate(total * 100) / 100;
                                string total1 = string.Format("{0:0.00}", aTruncated2);
                                double totaltax = g8;
                                double aTruncated3 = Math.Truncate(totaltax * 100) / 100;
                                totaltax1 = string.Format("{0:0.00}", aTruncated3);
                                DataTable gettax = sql.getdataset("select * from TaxSlabMaster where isactive=1 and saletypename='" + lblsaletype.Text + "' and Taxslabname=(select taxslab from productmaster where product_name like '%" + g2 + "%')");
                                //DataTable gettax = sql.getdataset("select * from itemtaxmaster where isactive=1 and productid=(select productid from productmaster where product_name like '%" + g2 + "%')");
                                double sgst1 = g5 - g7;
                                double sgst = sgst1 * (Convert.ToDouble(gettax.Rows[0]["sgst"].ToString()) / 100);
                                sgst = Math.Round(sgst, 2);
                                double cgst1 = g5 - g7;
                                double cgst = cgst1 * (Convert.ToDouble(gettax.Rows[0]["cgst"].ToString()) / 100);
                                cgst = Math.Round(cgst, 2);
                                double addtaxamt;
                                if (gettax.Rows[0]["onwhich"].ToString() == "Tax Amt")
                                {
                                    double addt = Convert.ToDouble(gettax.Rows[0]["Additax"].ToString());
                                    addtaxamt = (g5 * addt) / 100;
                                }
                                else
                                {
                                    double addt = Convert.ToDouble(gettax.Rows[0]["Additax"].ToString());
                                    addtaxamt = (g9 * addt) / 100;
                                }
                                sql.execute("INSERT INTO [dbo].[BillPOSProductMaster]([BillId],[BillRunDate],[ItemName],[Qty],[Rate],[Amount],[Total],[igst],[Addtax],[Discount],[sgst],[cgst],[RoundOf],[DiscountAmt],[Addtaxamt],[Batchno],[cess],[billno],[agentid],[isactive])VALUES('" + id + "','" + Convert.ToDateTime(todaydate.Text).ToString(Master.dateformate) + "','" + g2 + "','" + g3 + "','" + g4 + "','" + g5 + "','" + g9 + "','" + gettax.Rows[0]["igst"].ToString() + "','" + gettax.Rows[0]["Additax"].ToString() + "','" + g6 + "','" + sgst + "','" + cgst + "','" + lblroundof.Text + "','" + g7 + "','" + addtaxamt + "','" + g10 + "','" + g12 + "','" + billno1 + "','" + agentid + "','1')");
                                //sql.execute("INSERT INTO [dbo].[itemdetails]([billno],[barcode],[itemname],[qty],[unitprice],[price],[disamt],[totalamt],[gqty],[gprice],[gdisamt],[gtotal],[itamcount]) VALUES('" + maxbillno + "','" + g1 + "','" + g2 + "','" + g3 + "','" + g4 + "','" + pric1 + "','" + dec1 + "','" + total1 + "','" + txtqty.Text + "','" + txtstotal.Text + "','" + txtdisamt.Text + "','" + txtgtotal.Text + "','" + count + "')");

                            }
                            DataTable getstate = sql.getdataset("select * from Company where isactive=1 and CompanyID='" + Master.companyId + "'");
                            DataTable saletypeid = sql.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + lblsaletype.Text + "'");
                            sql.execute("Update BillPOSMaster Set BillDate='" + Convert.ToDateTime(todaydate.Text).ToString(Master.dateformate) + "',count='" + count + "',totalqty='" + txtqty.Text + "',totalbasic='" + txtstotal.Text + "',totaltax='" + txttotaltax.Text + "',totalnet='" + txtgtotal.Text + "',disamt='" + txtdisamt.Text + "',adddisamt='" + txtadddisamt.Text + "',customername='" + txtcname.Text + "',customercity='" + txtcity.Text + "',customermobile='" + txtcmobile.Text + "',totalcess='" + txtcess.Text + "',statecode='" + getstate.Rows[0]["Statecode"].ToString() + "',companystate='" + getstate.Rows[0]["State"].ToString() + "',saletypeid='" + saletypeid.Rows[0]["Purchasetypeid"].ToString() + "',agentid='" + cmbagentname.SelectedValue + "'where BillId='" + id + "'");
                            btnnosale.Text = "Save Alt-S";
                        }
                        else
                        {
                            MessageBox.Show("You don't have Permission To Update");
                            return;
                        }
                    }
                }
                else
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[9]["u"].ToString() == "True")
                        {
                            bindbillno();
                            billno();
                            string totaltax1 = string.Empty;
                            int count = dgvitem.Rows.Count;
                            foreach (DataGridViewRow row in dgvitem.Rows)
                            {

                                g1 = row.Cells[0].Value.ToString();// barcode code
                                g2 = row.Cells[1].Value.ToString(); // itamname
                                g3 = Convert.ToDouble(row.Cells[2].Value.ToString()); // qty
                                g4 = Convert.ToDouble(row.Cells[3].Value.ToString()); // rate
                                g5 = Convert.ToDouble(row.Cells[4].Value.ToString()); // basicamt
                                g6 = Convert.ToDouble(row.Cells[5].Value.ToString()); // disper
                                g7 = Convert.ToDouble(row.Cells[6].Value.ToString()); // disamt
                                g8 = Convert.ToDouble(row.Cells[7].Value.ToString()); // totaltax
                                g9 = Convert.ToDouble(row.Cells[9].Value.ToString()); // Tax
                                g10 = row.Cells[10].Value.ToString(); // Batch
                                g12 = Convert.ToDouble(row.Cells[11].Value.ToString()); // cess
                                agentid = row.Cells[12].Value.ToString(); // agentid
                                double pric = g5;
                                double aTruncated = Math.Truncate(pric * 100) / 100;
                                string pric1 = string.Format("{0:0.00}", aTruncated);

                                double dec = g6;
                                double aTruncated1 = Math.Truncate(dec * 100) / 100;
                                string dec1 = string.Format("{0:0.00}", aTruncated1);

                                double total = g7;
                                double aTruncated2 = Math.Truncate(total * 100) / 100;
                                string total1 = string.Format("{0:0.00}", aTruncated2);
                                double totaltax = g8;
                                double aTruncated3 = Math.Truncate(totaltax * 100) / 100;
                                totaltax1 = string.Format("{0:0.00}", aTruncated3);

                                DataTable gettax = sql.getdataset("select * from TaxSlabMaster where isactive=1 and saletypename='" + lblsaletype.Text + "' and Taxslabname=(select taxslab from productmaster where product_name = '" + g2 + "')");
                                double sgst1 = g5 - g7;
                                double sgst = sgst1 * (Convert.ToDouble(gettax.Rows[0]["sgst"].ToString()) / 100);
                                sgst = Math.Round(sgst, 2);
                                double cgst1 = g5 - g7;
                                double cgst = cgst1 * (Convert.ToDouble(gettax.Rows[0]["cgst"].ToString()) / 100);
                                cgst = Math.Round(cgst, 2);
                                double addtaxamt;
                                if (gettax.Rows[0]["onwhich"].ToString() == "Tax Amt")
                                {
                                    double addt = Convert.ToDouble(gettax.Rows[0]["Additax"].ToString());
                                    addtaxamt = (g5 * addt) / 100;
                                }
                                else
                                {
                                    double addt = Convert.ToDouble(gettax.Rows[0]["Additax"].ToString());
                                    addtaxamt = (g9 * addt) / 100;
                                }
                                sql.execute("INSERT INTO [dbo].[BillPOSProductMaster]([BillId],[BillRunDate],[ItemName],[Qty],[Rate],[Amount],[Total],[igst],[Addtax],[Discount],[sgst],[cgst],[RoundOf],[DiscountAmt],[Addtaxamt],[Batchno],[cess],[billno],[agentid],[isactive])VALUES('" + maxbillno + "','" + Convert.ToDateTime(todaydate.Text).ToString(Master.dateformate) + "','" + g2 + "','" + g3 + "','" + g4 + "','" + g5 + "','" + g9 + "','" + gettax.Rows[0]["igst"].ToString() + "','" + gettax.Rows[0]["Additax"].ToString() + "','" + g6 + "','" + sgst + "','" + cgst + "','" + lblroundof.Text + "','" + g7 + "','" + addtaxamt + "','" + g10 + "','" + g12 + "','" + billnowithprifix + "','" + agentid + "','1')");
                                //sql.execute("INSERT INTO [dbo].[itemdetails]([billno],[barcode],[itemname],[qty],[unitprice],[price],[disamt],[totalamt],[gqty],[gprice],[gdisamt],[gtotal],[itamcount]) VALUES('" + maxbillno + "','" + g1 + "','" + g2 + "','" + g3 + "','" + g4 + "','" + pric1 + "','" + dec1 + "','" + total1 + "','" + txtqty.Text + "','" + txtstotal.Text + "','" + txtdisamt.Text + "','" + txtgtotal.Text + "','" + count + "')");

                            }
                            DataTable getstate = sql.getdataset("select * from Company where isactive=1 and CompanyID='" + Master.companyId + "'");
                            DataTable saletypeid = sql.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + lblsaletype.Text + "'");
                            sql.execute("INSERT INTO [dbo].[BillPOSMaster]([BillId],[BillDate],[Terms],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[disamt],[adddisamt],[customername],[customercity],[customermobile],[totalcess],[statecode],[companystate],[billno],[saletypeid],[agentid],[isactive])VALUES('" + maxbillno + "','" + Convert.ToDateTime(todaydate.Text).ToString(Master.dateformate) + "','Cash','" + count + "','" + txtqty.Text + "','" + txtstotal.Text + "','" + txttotaltax.Text + "','" + txtgtotal.Text + "','" + txtdisamt.Text + "','" + txtadddisamt.Text + "','" + txtcname.Text + "','" + txtcity.Text + "','" + txtcmobile.Text + "','" + txtcess.Text + "','" + getstate.Rows[0]["Statecode"].ToString() + "','" + getstate.Rows[0]["State"].ToString() + "','" + billnowithprifix + "','" + saletypeid.Rows[0]["Purchasetypeid"].ToString() + "','" + cmbagentname.SelectedValue + "','1')");
                            string abc = "INSERT INTO [dbo].[BillPOSMaster]([BillId],[BillDate],[Terms],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[disamt],[adddisamt],[customername],[customercity],[customermobile],[totalcess],[statecode],[companystate],[billno],[saletypeid],[agentid],[isactive])VALUES('" + maxbillno + "','" + Convert.ToDateTime(todaydate.Text).ToString(Master.dateformate) + "','Cash','" + count + "','" + txtqty.Text + "','" + txtstotal.Text + "','" + txttotaltax.Text + "','" + txtgtotal.Text + "','" + txtdisamt.Text + "','" + txtadddisamt.Text + "','" + txtcname.Text + "','" + txtcity.Text + "','" + txtcmobile.Text + "','" + txtcess.Text + "','" + getstate.Rows[0]["Statecode"].ToString() + "','" + getstate.Rows[0]["State"].ToString() + "','" + billnowithprifix + "','" + saletypeid.Rows[0]["Purchasetypeid"].ToString() + "','" + cmbagentname.SelectedValue + "','1')";
                            //temptable.Dispose();
                            LogGenerator.Info(abc);
                        }
                        else
                        {
                            MessageBox.Show("You don't have Permission To Submit");
                            return;
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }

        }
        public void autotextbox()
        {

            //SqlCommand cmd = new SqlCommand("select itemname from itemmaster where itemname like '"+"%" + txtbarcode.Text + "%" + "\'", constr);
            // constr.Open();
            //SqlDataReader dr = cmd.ExecuteReader();
            //AutoCompleteStringCollection myCollection = new AutoCompleteStringCollection();
            //while (dr.Read())
            //{
            //    myCollection.Add(dr.GetString(0));
            //}
            //txtbarcode.AutoCompleteCustomSource = myCollection;
            //constr.Close();
            //cmd.Dispose();
        }
        public void autoextender()
        {
            SqlDataReader dReader;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = constr;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            // (dgvmst_subpat.DataSource as DataTable).DefaultView.RowFilter = string.Format("p_name like '%{0}%' ", txt_name.Text);
            cmd.CommandText = "select product_name from productmaster where isactive=1 order by product_name";
            conn.Open();
            dReader = cmd.ExecuteReader();
            if (dReader.HasRows == true)
            {
                while (dReader.Read())
                    namesCollection.Add(dReader["product_name"].ToString());

            }
            else
            {
                // MessageBox.Show("Data not found");
            }
            dReader.Close();

            txtbarcode.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtbarcode.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtbarcode.AutoCompleteCustomSource = namesCollection;
        }

        public void totaltax()
        {

            try
            {
                t4 = Convert.ToDouble(txtdisamt.Text);

                if (txtdisamt.Text == "")
                {
                    txtdisamt.Text = t4.ToString();

                }
                else
                {
                    dt1 = sql.getdataset("select i.itemname,t.taxper from taxsalb t inner join itemmaster i on i.taxslab=t.id  where barcode='" + txtbarcode.Text + "'");
                    lbltax.Text = dt1.Rows[0]["taxper"].ToString();

                    t5 = Convert.ToDouble(lbltax.Text);


                    t6 = (t4 * t5) / 100;

                    //txttaxamt.Text = t6.ToString();
                    totalprice();


                }
            }
            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message);
            }
        }
        public double[] calculatediscount(string itemname, string partyname, double basicprice)
        {
            double itemper = 0, itemamt = 0, discount = 0, disamt = 0, dispercompany = 0, disamtcompant = 0;
            string discountval = "";
            DataTable dt12 = sql.getdataset("select * from PartyRates where itemname='" + itemname + "' and (PartyName='" + partyname + "' OR PartyID='0')");

            if (dt12.Rows.Count > 0)
            {
                itemper = Convert.ToDouble(dt12.Rows[0]["Discount"].ToString());
                itemamt = Convert.ToDouble(dt12.Rows[0]["SpecialRate"].ToString());
            }
            DataTable dtgroup = sql.getdataset("select * from ProductMaster where Product_Name='" + itemname + "'");
            string groupid = dtgroup.Rows[0]["GroupName"].ToString();
            string companyid = dtgroup.Rows[0]["CompanyID"].ToString();
            DataTable dt123 = sql.getdataset("select * from PartyGroupDiscount where ItemGroupName='" + groupid + "'and(PartyName='" + partyname + "'OR PartyID=' ')");


            if (dt123.Rows.Count > 0)
            {
                discount = Convert.ToDouble(dt123.Rows[0]["Discount"].ToString());
                disamt = Convert.ToDouble(dt123.Rows[0]["SpecialRate"].ToString());
            }

            DataTable dt1234 = sql.getdataset("select * from PartyCompanyDiscount where ItemCompanyID='" + companyid + "'and(PartyName='" + partyname + "'OR PartyID=' ')");
            if (dt1234.Rows.Count > 0)
            {
                dispercompany = Convert.ToDouble(dt1234.Rows[0]["Discount"].ToString());
                disamtcompant = Convert.ToDouble(dt1234.Rows[0]["SpecialRate"].ToString());
            }

            double disper = 0, disa = 0;
            if (itemper > 0)
            {
                disper = itemper;
                disa = 0;
                if (disper != 0)
                {
                    disa = (Convert.ToDouble(basicprice) * Convert.ToDouble(disper)) / 100;
                }
                else
                {
                    disper = (100 * Convert.ToDouble(disa)) / Convert.ToDouble(basicprice);
                }
                disper = Math.Round(disper, 2);
                disamt = Math.Round(disa, 2);

            }
            else if (itemamt > 0)
            {
                //disper = 0;
                //disa = itemamt;
                //if (disper != 0)
                //{
                //    disa = (Convert.ToDouble(basicprice) * Convert.ToDouble(disper)) / 100;
                //}
                //else
                //{
                //    disper = (100 * Convert.ToDouble(disa)) / Convert.ToDouble(basicprice);
                //}
                //disper = Math.Round(disper, 2);
                //disamt = Math.Round(disa, 2);

            }
            else if (discount > 0)
            {
                disper = discount;
                disa = 0;
                if (disper != 0)
                {
                    disa = (Convert.ToDouble(basicprice) * Convert.ToDouble(disper)) / 100;
                }
                else
                {
                    disper = (100 * Convert.ToDouble(disa)) / Convert.ToDouble(basicprice);
                }
                disper = Math.Round(disper, 2);
                disamt = Math.Round(disa, 2);
            }
            else if (disamt > 0)
            {
                //disper = 0;
                //disa = disamt;
                //if (disper != 0)
                //{
                //    disa = (Convert.ToDouble(basicprice) * Convert.ToDouble(disper)) / 100;
                //}
                //else
                //{
                //    disper = (100 * Convert.ToDouble(disa)) / Convert.ToDouble(basicprice);
                //}
                //disper = Math.Round(disper, 2);
                //disamt = Math.Round(disa, 2);
            }
            else if (dispercompany > 0)
            {
                disper = dispercompany;
                disa = 0;
                if (disper != 0)
                {
                    disa = (Convert.ToDouble(basicprice) * Convert.ToDouble(disper)) / 100;
                }
                else
                {
                    disper = (100 * Convert.ToDouble(disa)) / Convert.ToDouble(basicprice);
                }
                disper = Math.Round(disper, 2);
                disamt = Math.Round(disa, 2);
            }
            else if (disamtcompant > 0)
            {
                //disper = 0;
                //disa = disamtcompant;
                //if (disper != 0)
                //{
                //    disa = (Convert.ToDouble(basicprice) * Convert.ToDouble(disper)) / 100;
                //}
                //else
                //{
                //    disper = (100 * Convert.ToDouble(disa)) / Convert.ToDouble(basicprice);
                //}
                //disper = Math.Round(disper, 2);
                //disamt = Math.Round(disa, 2);
            }
            double[] dis = new double[2];
            dis[0] = disper;
            dis[1] = disamt;
            return dis;
        }
        public void temptableenter()
        {
            try
            {
                if (temptable.Rows.Count > 0)
                {

                    try
                    {
                        DataTable pricetab = new DataTable();
                        pricetab = sql.getdataset("select Defaultprice from Options");
                        Dprice = pricetab.Rows[0]["Defaultprice"].ToString();
                        DataRow dr = temptable.Select("Batchno='"+batchno+"' and (BARCODE='" + txtbarcode.Text + "' or ITEMNAME ='" + txtbarcode.Text + "')").FirstOrDefault();

                        if (dr != null)
                        {
                            //if (batchno == "")
                            //{
                            //    batchno = "NA";
                            //}
                            DataTable productid = new DataTable();
                            productid = sql.getdataset("select DISTINCT p.taxslab,p.ProductID from ProductMaster p inner join ProductPriceMaster pp on p.Productid=pp.Productid where   p.Product_Name='" + txtbarcode.Text + "'or pp.barcode='" + txtbarcode.Text + "' and pp.BatchNo='" + batchno + "' and p.isactive=1 and pp.isactive=1");

                            dt = sql.getdataset("select p.cessper,p.cessamt,pp.Barcode,p.Product_Name,pp." + Dprice + " as Dprice,pp.BasicPrice,i.igst,i.additax,i.cgst,i.sgst from ProductPriceMaster pp inner join ProductMaster p on p.ProductID=pp.Productid  inner join TaxSlabMaster i on i.Taxslabname=p.taxslab where p.ProductID='" + productid.Rows[0]["ProductID"].ToString() + "' and p.taxslab='" + productid.Rows[0]["taxslab"].ToString() + "'and i.saletypename='" + lblsaletype.Text + "' and pp.BatchNo='" + batchno + "' and p.isactive=1 and pp.isactive=1 and i.isactive=1");
                            //   dt = sql.getdataset("select p.*,b.* from productmaster p inner join ProductPriceMaster b on p.ProductID=b.ProductID where b.barcode='" + txtbarcode.Text + "' or p.product_name like '%" + txtbarcode.Text + "%'");
                            //  dt = sql.getdataset("Select p.Product_Name,p.Unit,pp.saleprice  as rate,pp.MRP as Amount from ProductMaster p inner join ProductPriceMaster pp on p.ProductID = pp.Productid where p.ProductID = pp.Productid and p.Product_Name='" + txtbarcode.Text + "'or pp.barcode='" + txtbarcode.Text + "'");


                            //  dt = sql.getdataset("Select p.Product_Name,p.Convfactor,(pp.saleprice * 100)/(100+ (pp.vat+pp.addvat)) as rate,p.Convfactor*((pp.saleprice * 100)/(100+ (pp.vat+pp.addvat))) as amount,pp.vat,pp.addvat from ProductMaster p inner join ProductPriceMaster pp on p.ProductID = pp.Productid where p.ProductID = pp.Productid and p.Product_Name='" + txtbarcode.Text + "'or pp.barcode='" + txtbarcode.Text + "'");
                            //   dt = sql.getdataset("select i.qty,i.mrp,i.discount,i.barcode,t.Product_Name from ProductMaster t inner join ProductPriceMaster i on i.taxslab=t.id  where barcode='" + txtbarcode.Text + "' or i.itemname='" + txtbarcode.Text + "'");
                            if (dt.Rows.Count > 0)
                            {

                                Double qty = Convert.ToDouble(dr["qty"]);
                                Double pbasicprice = Convert.ToDouble(dr["BASIC AMT"]);
                                Double discounta = Convert.ToDouble(dr["DISCOUNT AMT"]);
                                Double igst = Convert.ToDouble(dt.Rows[0]["igst"].ToString());
                                Double netamt = Convert.ToDouble(dr["AMOUNT"]);
                                //  Double tcess = Convert.ToDouble(dr["cess"]);
                                Double addtax = Convert.ToDouble(dt.Rows[0]["additax"].ToString());
                                Double dprice = Convert.ToDouble(dr["Rate"]);
                                qty++;
                                //double basicprice = price * qty;
                                //double disa = price * disper / 100;
                                //double mrp1 = price - disa;
                                //double tax = mrp1 * igst;
                                double MRP = Convert.ToDouble(dr["Rate"]);
                                double total = MRP;
                                total = Math.Round(total, 2);
                                double cessper1 = Convert.ToDouble(dt.Rows[0]["cessper"].ToString());
                                double cessamt1 = Convert.ToDouble(dt.Rows[0]["cessamt"].ToString());
                                //consider example of MRP=67.83, cessamt1=20, igst=18, addtax=0, cessper=4 and get the total tax amt including of all kind of taxes with cess also. as per the example the tax amt is: 8.63
                                double tax = ((MRP - cessamt1) * (igst + addtax + cessper1) / (100 + (igst + addtax + cessper1)));
                                //    if (addtax != 0)
                                //    {
                                //        tax = tax * addtax / 100;
                                //    }
                                //  tax = Math.Round(tax, 2);
                                DataTable discount = new DataTable();
                                discount = sql.getdataset("select Discount,SpecialRate from PartyRates where ItemID='" + productid.Rows[0]["ProductID"].ToString() + "'");
                                if (discount.Rows.Count > 0)
                                {
                                    disper = Convert.ToInt32(discount.Rows[0]["Discount"].ToString());
                                    if (Convert.ToInt32(discount.Rows[0]["SpecialRate"].ToString()) > 0)
                                    {
                                        dt.Rows[0]["Dprice"] = Convert.ToInt32(discount.Rows[0]["SpecialRate"].ToString());
                                        disamt = 1;
                                    }
                                    else
                                    {
                                        disamt = 0;
                                    }
                                    dt.AcceptChanges();
                                }
                                else
                                {
                                    disper = 0;
                                    disamt = 0;
                                }
                                //this amount represent the before discount deduct from sale/mrp value. as per the example the amount should be 39.2
                                double aftrdisamt = MRP - cessamt1 - tax;
                                double rate = aftrdisamt;
                                //this is the actual rate of item. as per the example the amount is 40.
                                //if (disper > 0 && disamt == 0)
                                //{
                                //    double bfrdisamt = (aftrdisamt * 100) / (100 - disper);
                                //    rate = aftrdisamt;
                                //}
                                //else if (disper > 0 && disamt == 1)
                                //{
                                //    rate = aftrdisamt;
                                //}
                                //else if (disper == 0 && disamt == 1)
                                //{
                                //    rate = aftrdisamt;
                                //}
                                rate = Math.Round(MRP, 2);
                                double basicprice = rate * qty;
                                basicprice = Math.Round(basicprice, 2);
                                double disa = disamt;
                                double[] dis = new double[2];
                                dis = calculatediscount(dt.Rows[0]["Product_Name"].ToString(), "ALL PARTIES", basicprice);
                                tax = Math.Round((basicprice - dis[1]) * igst / (100), 2);
                                double cess1 = Math.Round((basicprice - dis[1]) * cessper1 / (100), 2);
                                cess1 = cess1 + (cessamt1 * qty);
                                disper = Math.Round(dis[0], 2);
                                disa = Math.Round(dis[1], 2);
                                //if (disper != 0)
                                //{
                                //   disa = (Convert.ToDouble(basicprice) * Convert.ToDouble(disper)) / 100;
                                //}
                                //else
                                //{
                                //    disper = (100 * Convert.ToDouble(disamt)) / Convert.ToDouble(basicprice);
                                //}
                                // disper = Math.Round(disper, 2);

                                if (txtaddtax.Text == "")
                                {
                                    txtaddtax.Text = "0";
                                }
                                double adtax = Math.Round((basicprice - dis[1]) * (addtax) / (100), 2);
                                double atax = Math.Round(adtax, 2);
                                //double aatax = Convert.ToDouble(txtaddtax.Text);
                                //double a = aatax + atax;
                                //txtaddtax.Text = Convert.ToString(a);


                                double amount = basicprice - disa + tax + atax + cess1;
                                amount = Math.Round(amount, 2);


                                //   lblroundof.Text = Math.Round((Math.Round(amount, 0) - amount), 2).ToString();
                                basicprice = rate * qty;
                                //double[] dis = new double[2];
                                //dis = calculatediscount(txtbarcode.Text, "ALL PARTIES", basicprice);
                                //  Double cess = Convert.ToDouble(basicprice) * Convert.ToDouble(dt.Rows[0]["cessper"].ToString()) / 100;
                                //  Double cessamt = Convert.ToDouble(dt.Rows[0]["cessamt"].ToString());
                                //  Double tcess = cess + cessamt;
                                double cessval = cess1;

                                (dr["QTY"]) = qty;
                                (dr["BASIC AMT"]) = basicprice;
                                (dr["DISCOUNT AMT"]) = dis[1];
                                (dr["GST AMT"]) = tax;
                                (dr["ADD TAX"]) = atax;
                                (dr["AMOUNT"]) = amount;
                                (dr["cess"]) = cessval;

                                //   totaltaxbox();

                            }
                        }
                        else
                        {
                            //if (batchno == "")
                            //{
                            //    batchno = "NA";
                            //}
                            DataTable productid = new DataTable();
                            productid = sql.getdataset("select DISTINCT p.taxslab,p.ProductID from ProductMaster p inner join ProductPriceMaster pp on p.Productid=pp.Productid where    p.Product_Name='" + txtbarcode.Text + "'or pp.barcode='" + txtbarcode.Text + "' and pp.BatchNo='" + batchno + "' and p.isactive=1 and pp.isactive=1");
                            double disper;
                            double disamt=0;
                            dt = sql.getdataset("select p.cessper,p.cessamt,pp.Barcode,p.Product_Name,pp." + Dprice + " as Dprice,pp.BasicPrice,i.igst,i.additax,i.cgst,i.sgst from ProductPriceMaster pp inner join ProductMaster p on p.ProductID=pp.Productid  inner join TaxSlabMaster i on i.Taxslabname=p.taxslab where p.ProductID='" + productid.Rows[0]["ProductID"].ToString() + "' and  p.taxslab='" + productid.Rows[0]["taxslab"].ToString() + "'and i.saletypename='" + lblsaletype.Text + "' and pp.BatchNo='" + batchno + "' and pp.isactive=1 and p.isactive=1 and i.isactive=1");
                            //   dt = sql.getdataset("select p.*,b.* from productmaster p inner join ProductPriceMaster b on p.ProductID=b.ProductID where b.barcode='" + txtbarcode.Text + "' or p.product_name like '%" + txtbarcode.Text + "%'");
                            //  dt = sql.getdataset("Select p.Product_Name,p.Unit,pp.saleprice  as rate,pp.MRP as Amount from ProductMaster p inner join ProductPriceMaster pp on p.ProductID = pp.Productid where p.ProductID = pp.Productid and p.Product_Name='" + txtbarcode.Text + "'or pp.barcode='" + txtbarcode.Text + "'");
                            DataTable discount = new DataTable();
                            discount = sql.getdataset("select Discount,SpecialRate from PartyRates where ItemID='" + productid.Rows[0]["ProductID"].ToString() + "'");
                            if (discount.Rows.Count > 0)
                            {
                                disper = Convert.ToInt32(discount.Rows[0]["Discount"].ToString());
                                if (Convert.ToInt32(discount.Rows[0]["SpecialRate"].ToString()) > 0)
                                {
                                    dt.Rows[0]["Dprice"] = Convert.ToInt32(discount.Rows[0]["SpecialRate"].ToString());
                                    disamt = 1;
                                }
                                else
                                {
                                    disamt = 0;
                                }
                                dt.AcceptChanges();
                                
                            }
                            else
                            {
                                disper = 0;
                                disamt = 0;
                            }
                            // dt = sql.getdataset("Select p.Product_Name,p.Convfactor,(pp.saleprice * 100)/(100+ (pp.vat+pp.addvat)) as rate,p.Convfactor*((pp.saleprice * 100)/(100+ (pp.vat+pp.addvat))) as amount,pp.vat,pp.addvat from ProductMaster p inner join ProductPriceMaster pp on p.ProductID = pp.Productid where p.ProductID = pp.Productid and p.Product_Name='" + txtbarcode.Text + "'or pp.barcode='" + txtbarcode.Text + "'");
                            //  dt = sql.getdataset("select i.itemname,i.qty,i.mrp,i.discount,i.barcode,t.taxper from taxsalb t inner join itemmaster i on i.taxslab=t.id  where barcode='" + txtbarcode.Text + "' or i.itemname='" + txtbarcode.Text + "'");
                            if (dt.Rows.Count > 0)
                            {
                                //Double cess = Convert.ToDouble(dt.Rows[0]["Dprice"].ToString()) * Convert.ToDouble(dt.Rows[0]["cessper"].ToString()) / 100;
                                //Double cessamt = Convert.ToDouble(dt.Rows[0]["cessamt"].ToString());
                                //Double tcess = cess + cessamt;

                                //itemcalculate(dt.Rows[0]["Barcode"].ToString(), dt.Rows[0]["Product_Name"].ToString(), Convert.ToDouble(dt.Rows[0]["Dprice"].ToString()), disper, disamt, Convert.ToDouble(dt.Rows[0]["igst"].ToString()), Convert.ToDouble(dt.Rows[0]["additax"].ToString()));
                                itemcalculate(dt.Rows[0]["Barcode"].ToString(), dt.Rows[0]["Product_Name"].ToString(), Convert.ToDouble(dt.Rows[0]["Dprice"].ToString()), disper, disamt, Convert.ToDouble(dt.Rows[0]["igst"].ToString()), Convert.ToDouble(dt.Rows[0]["additax"].ToString()), Convert.ToDouble(dt.Rows[0]["cessper"].ToString()), Convert.ToDouble(dt.Rows[0]["cessamt"].ToString()));
                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        // MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    //if (batchno == "")
                    //{
                    //    batchno = "NA";
                    //}
                    DataTable pricetab = new DataTable();
                    pricetab = sql.getdataset("select Defaultprice from Options");
                    Dprice = pricetab.Rows[0]["Defaultprice"].ToString();
                    DataTable productid = new DataTable();
                    productid = sql.getdataset("select DISTINCT p.taxslab,p.ProductID from ProductMaster p inner join ProductPriceMaster pp on p.Productid=pp.Productid and pp.isactive=1 where p.Product_Name='" + txtbarcode.Text + "'or pp.barcode='" + txtbarcode.Text + "' and pp.BatchNo='" + batchno + "' and p.isactive=1 and pp.isactive=1");
                    double disper;
                    double disamt=0;

                    dt = sql.getdataset("select p.cessper,p.cessamt,pp.Barcode,p.Product_Name,pp." + Dprice + " as Dprice,pp.BasicPrice,isnull(i.igst,0)as igst,isnull(i.additax,0)as additax,isnull(i.cgst,0)as cgst,isnull(i.sgst,0)as sgst from ProductPriceMaster pp inner join ProductMaster p on p.ProductID=pp.Productid and pp.isactive=1 left join TaxSlabMaster i on i.Taxslabname=p.taxslab where p.ProductID='" + productid.Rows[0]["ProductID"].ToString() + "' and pp.isactive=1 and p.isactive=1 and i.isactive=1 and p.taxslab='" + productid.Rows[0]["taxslab"].ToString() + "' and pp.BatchNo='" + batchno + "' and i.saletypename='" + lblsaletype.Text + "'");
                    //   dt = sql.getdataset("select p.*,b.* from productmaster p inner join ProductPriceMaster b on p.ProductID=b.ProductID where b.barcode='" + txtbarcode.Text + "' or p.product_name like '%" + txtbarcode.Text + "%'");
                    //  dt = sql.getdataset("Select p.Product_Name,p.Unit,pp.saleprice  as rate,pp.MRP as Amount from ProductMaster p inner join ProductPriceMaster pp on p.ProductID = pp.Productid where p.ProductID = pp.Productid and p.Product_Name='" + txtbarcode.Text + "'or pp.barcode='" + txtbarcode.Text + "'");
                    //DataTable discount = new DataTable();
                    //discount = sql.getdataset("select Discount,SpecialRate from PartyRates where ItemID='" + productid.Rows[0]["ProductID"].ToString() + "'");
                    //if (discount.Rows.Count > 0)
                    //{
                    //    disper = Convert.ToInt32(discount.Rows[0]["Discount"].ToString());
                    //    disamt = Convert.ToInt32(discount.Rows[0]["SpecialRate"].ToString());
                    //}
                    //else
                    //{
                    //    disper = 0;
                    //    disamt = 0;
                    //}
                    DataTable discount = new DataTable();
                    discount = sql.getdataset("select Discount,SpecialRate from PartyRates where ItemID='" + productid.Rows[0]["ProductID"].ToString() + "'");
                    if (discount.Rows.Count > 0)
                    {
                        disper = Convert.ToInt32(discount.Rows[0]["Discount"].ToString());
                        if (Convert.ToInt32(discount.Rows[0]["SpecialRate"].ToString()) > 0)
                        {
                            dt.Rows[0]["Dprice"] = Convert.ToInt32(discount.Rows[0]["SpecialRate"].ToString());
                            disamt = 1;
                        }
                        else
                        {
                            disamt = 0;
                        }
                        dt.AcceptChanges();
                    }
                    else
                    {
                        disper = 0;
                        disamt = 0;
                    }
                    if (dt.Rows.Count > 0)
                    {

                        //temptable = new DataTable();
                        //temptable.Columns.Add("BARCODE", typeof(string));
                        //temptable.Columns.Add("ITEMNAME", typeof(string));
                        //temptable.Columns.Add("QTY", typeof(string));
                        //temptable.Columns.Add("RATE", typeof(string));
                        //temptable.Columns.Add("BASIC AMT", typeof(string));
                        //temptable.Columns.Add("DISCOUNT PER", typeof(string));
                        //temptable.Columns.Add("DISCOUNT AMT", typeof(string));
                        //temptable.Columns.Add("GST AMT", typeof(string));
                        //temptable.Columns.Add("ADD TAX", typeof(string));
                        //temptable.Columns.Add("AMOUNT", typeof(string));

                        t12 = Convert.ToInt32(1);

                        //  t14 = Convert.ToInt32(dt.Rows[0]["mrp"].ToString());
                        // t18 = Convert.ToInt32(dt.Rows[0]["taxper"].ToString());
                        //t15 = Convert.ToInt16(dt.Rows[0]["discount"].ToString());
                        //t16 = (t14 * t15) / 100;
                        //Double cess = Convert.ToDouble(dt.Rows[0]["Dprice"].ToString()) * Convert.ToDouble(dt.Rows[0]["cessper"].ToString())/100;
                        //Double cessamt=Convert.ToDouble(dt.Rows[0]["cessamt"].ToString());
                        //Double tcess = cess + cessamt;
                        itemcalculate(dt.Rows[0]["Barcode"].ToString(), dt.Rows[0]["Product_Name"].ToString(), Convert.ToDouble(dt.Rows[0]["Dprice"].ToString()), disper, disamt, Convert.ToDouble(dt.Rows[0]["igst"].ToString()), Convert.ToDouble(dt.Rows[0]["additax"].ToString()), Convert.ToDouble(dt.Rows[0]["cessper"].ToString()), Convert.ToDouble(dt.Rows[0]["cessamt"].ToString()));





                    }
                    else
                    {

                    }
                }
                //dgvitem.DataSource = temptable;
                //dgvitem.Columns[1].Width = 300;
                //dgvitem.Columns[2].Width = 100;
                //dgvitem.Columns[3].Width = 100;
                //dgvitem.Columns[5].Width = 130;
                //dgvitem.Columns[6].Width = 100;
                //dgvitem.Columns[7].Width = 100;
                //dgvitem.Columns[8].Width = 100;
                //dgvitem.Columns[9].Width = 150;
                txtbarcode.Text = "";
                txtbarcode.Focus();
                totaltaxbox();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }

        }
        internal void Update(int p, string iid)
        {
            try
            {
                userrights = sql.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[9]["d"].ToString() == "False")
                    {
                        btndelete.Enabled = false;
                    }
                }
                options = sql.getdataset("select * from options");
                if (iid != "")
                {
                    id = iid;
                    binaagent();
                    SqlCommand cmd = new SqlCommand("select * from BillPOSMaster where isactive=1 and BillId='" + id + "' and isactive=1", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    SqlCommand cmd1 = new SqlCommand("select * from BillPOSProductMaster where isactive=1 and BillId='" + id + "' and isactive=1", con);
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    sda1.Fill(dt1);
                    if (options.Rows[0]["requiragentnameinpos"].ToString() == "Ask For Agent After Item Entry")
                    {
                        pnlagent.Visible = true;
                    }
                    else if (options.Rows[0]["requiragentnameinpos"].ToString() == "Ask For Agent Bill Wise")
                    {
                        pnlagent.Visible = true;
                        DataTable agent = new DataTable();
                        agent = sql.getdataset("select accountname from clientmaster where isactive=1 and clientid='" + dt.Rows[0]["agentid"].ToString() + "' and isactive=1");
                        if (agent.Rows.Count > 0)
                        {
                            //cmd = new SqlCommand("select accountname from clientmaster where isactive=1 and clientid='" + dt.Rows[0]["agentid"].ToString() + "' and isactive=1", con);
                            //con.Open();
                            string agentname = agent.Rows[0]["accountname"].ToString();
                            cmbagentname.Text = agentname;
                        }
                        //con.Close();
                    }
                    else
                    {

                    }
                    temptable.Rows.Clear();
                    if (dt1.Rows.Count > 0)
                    {

                        // temptable = new DataTable();

                        // temptable.Columns.Add("BARCODE", typeof(string));
                        // temptable.Columns.Add("ITEMNAME", typeof(string));
                        // temptable.Columns.Add("QTY", typeof(string));
                        // temptable.Columns.Add("RATE", typeof(string));
                        // temptable.Columns.Add("BASIC AMT", typeof(string));
                        // temptable.Columns.Add("DISCOUNT PER", typeof(string));
                        // temptable.Columns.Add("DISCOUNT AMT", typeof(string));
                        // temptable.Columns.Add("GST AMT", typeof(string));
                        // temptable.Columns.Add("ADD TAX", typeof(string));
                        // temptable.Columns.Add("AMOUNT", typeof(string));
                        // temptable.Columns.Add("Batchno", typeof(string));
                        //// temptable.Rows.Add("", "", "", "", "", "", "", "", "", "", "");

                        //  dgvitem.DataSource = temptable;
                        if (dt.Rows.Count > 0)
                        {
                            if (Convert.ToBoolean(options.Rows[0]["invoicenoinpos"].ToString()) == true)
                            {
                                if (options.Rows[0]["posbillno"].ToString() == "Continuous")
                                {
                                    lblinv.Visible = true;
                                    lblinvoice.Text = dt.Rows[0]["BillId"].ToString();
                                    lblinvoice.Visible = true;
                                }
                                else
                                {
                                    lblinv.Visible = true;
                                    lblinvoice.Text = dt.Rows[0]["billno"].ToString();
                                    lblinvoice.Visible = true;
                                }
                            }
                            txtcname.Text = dt.Rows[0]["customername"].ToString();
                            txtcity.Text = dt.Rows[0]["customercity"].ToString();
                            txtcmobile.Text = dt.Rows[0]["customermobile"].ToString();
                            todaydate.Value = Convert.ToDateTime(dt.Rows[0]["BillDate"].ToString());
                        }
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            DataTable proid = sql.getdataset("Select * from ProductMaster where isactive=1 and Product_Name='" + dt1.Rows[i]["ItemName"].ToString() + "'");
                            DataTable barcode = sql.getdataset("select * from ProductPriceMaster where isactive=1 and Productid='" + proid.Rows[0]["Productid"].ToString() + "' and Batchno='" + dt1.Rows[i]["batchno"].ToString() + "'");

                            double basic = Convert.ToDouble(dt1.Rows[i]["amount"].ToString());
                            double[] dis = new double[2];
                            dis = calculatediscount(dt1.Rows[i]["ItemName"].ToString(), "ALL PARTIES", basic);
                            //  temptableenter();
                            double disp = Math.Round(dis[0], 2);
                            double disa = Math.Round(dis[1], 2);
                            double tax = (Convert.ToDouble(dt1.Rows[i]["rate"].ToString()) * (Convert.ToDouble(dt1.Rows[i]["igst"].ToString()) + Convert.ToDouble(dt1.Rows[i]["addtax"].ToString())) / (100 + (Convert.ToDouble(dt1.Rows[i]["igst"].ToString()) + Convert.ToDouble(dt1.Rows[i]["addtax"].ToString()))));
                            tax = Math.Round(tax, 2);
                            tax = Math.Round((basic - dis[1]) * Convert.ToDouble(dt1.Rows[i]["igst"].ToString()) / (100), 2);
                            temptable.Rows.Add(barcode.Rows[0]["barcode"].ToString(), dt1.Rows[i]["ItemName"].ToString(), dt1.Rows[i]["qty"].ToString(), dt1.Rows[i]["rate"].ToString(), dt1.Rows[i]["amount"].ToString(), disp, disa, tax, dt1.Rows[i]["Addtax"].ToString(), dt1.Rows[i]["Total"].ToString(), dt1.Rows[i]["Batchno"].ToString(), dt1.Rows[i]["cess"].ToString(), dt1.Rows[i]["agentid"].ToString());

                        }
                        dgvitem.DataSource = temptable;
                        dgvitem.AllowUserToAddRows = false;



                    }


                }

                //    dgvitem.DataSource = temptable;

                //DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                //dgvitem.Columns.Add(btn);
                //btn.HeaderText = "Delete";
                //btn.Text = "";
                //btn.Name = "btndelete";
                //btn.UseColumnTextForButtonValue = true;
                //dgvitem.Columns[11].Width = 38;

                //   dgvitem.Columns[0].Width = 0;



                txtbarcode.Text = "";
                txtbarcode.Focus();
                totaltaxbox();
                btnnosale.Text = "Update";


            }
            catch
            {
            }
        }
        public void getdata(string str, string itemname)
        {
            userrights = sql.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[9]["d"].ToString() == "False")
                {
                    btndelete.Enabled = false;
                }
            }
            options = sql.getdataset("select * from options");
            lblsaletype.Text = options.Rows[0]["autosaletype"].ToString();
            batchno = str;
            DataTable dt = sql.getdataset("select ProductID from ProductMaster where isactive=1 and Product_Name='" + itemname + "'");
            DataTable dt1 = sql.getdataset("select Barcode from ProductPriceMaster where isactive=1 and ProductID='" + dt.Rows[0]["ProductID"].ToString() + "' and BatchNo='" + batchno + "'");
            //DataTable dt = sql.getdataset("select p.ProductID,pp.* from ProductMaster p inner join ProductPriceMaster pp on p.Productid=pp.Productid where   p.Product_Name='" + itemname + "'or pp.barcode='" + itemname + "' and pp.BatchNo='" + batchno + "' and p.isactive=1 and pp.isactive=1");

            txtbarcode.Text = dt1.Rows[0]["Barcode"].ToString();
            temptableenter();
            // totaltaxbox();
            //  dgvitem.Columns[0].Visible = false;
            if (btnnosale.Text != "Update")
            {
                billno();
            }

            batchno = "";
            GridDesign();

        }

        public void bindallitem()
        {
            try
            {
                DataTable allitem = new DataTable();
                lvallitem.Items.Clear();
                allitem = sql.getdataset("select ProductMaster.Product_Name from ProductMaster where isactive=1 order by ProductMaster.Product_Name asc");
                for (int i = 0; i < allitem.Rows.Count; i++)
                {
                    ListViewItem li;
                    li = lvallitem.Items.Add(allitem.Rows[i]["Product_Name"].ToString());
                }
            }
            catch
            {
            }
        }

        public void itemcalculate(String barcode, String itemname, double MRP, double disper, double disamt, double igst, double addtax, double cessper1, double cessamt1)
        {
            int qty = 1;
            double total = MRP;
            total = Math.Round(total, 2);
            //consider example of MRP=67.83, cessamt1=20, igst=18, addtax=0, cessper=4 and get the total tax amt including of all kind of taxes with cess also. as per the example the tax amt is: 8.63
            double tax = ((MRP - cessamt1) * (igst + addtax + cessper1) / (100 + (igst + addtax + cessper1)));
            //   double cess = ((MRP-cessamt1) * (cessper1) / (100 + cessper1));
            //   cess = cess + cessamt1;
            //    if (addtax != 0)
            //    {
            //        tax = tax * addtax / 100;
            //    }
            //  tax = Math.Round(tax, 2);

            //this amount represent the before discount deduct from sale/mrp value. as per the example the amount should be 39.2
            double aftrdisamt = MRP - cessamt1 - tax;
            double rate = aftrdisamt;
            //this is the actual rate of item. as per the example the amount is 40.
            //if (disper > 0 && disamt == 0)
            //{
            //   // double bfrdisamt = (aftrdisamt * 100) / (100 - disper);
            //    rate = aftrdisamt;
            //}
            //else if(disper>0 && disamt==1)
            //{
            //    rate = aftrdisamt;
            //}
            //else if (disper == 0 && disamt == 1)
            //{
            //    rate = aftrdisamt;
            //}

            rate = Math.Round(rate, 2);
            double basicprice = rate * qty;
            basicprice = Math.Round(basicprice, 2);
            double disa = disamt;
            double[] dis = new double[2];
            dis = calculatediscount(dt.Rows[0]["Product_Name"].ToString(), "ALL PARTIES", basicprice);
            tax = Math.Round((basicprice - dis[1]) * igst / (100), 2);
            double cess1 = Math.Round((basicprice - dis[1]) * cessper1 / (100), 2);
            cess1 = cess1 + (cessamt1 * qty);
            disper = Math.Round(dis[0], 2);
            disa = Math.Round(dis[1], 2);
            //if (disper != 0)
            //{
            //   disa = (Convert.ToDouble(basicprice) * Convert.ToDouble(disper)) / 100;
            //}
            //else
            //{
            //    disper = (100 * Convert.ToDouble(disamt)) / Convert.ToDouble(basicprice);
            //}
            // disper = Math.Round(disper, 2);

            if (txtaddtax.Text == "")
            {
                txtaddtax.Text = "0";
            }
            double adtax = Math.Round((basicprice - dis[1]) * (addtax) / (100), 2);
            double atax = Math.Round(adtax, 2);
            //double aatax = Convert.ToDouble(txtaddtax.Text);
            //double a= aatax + atax;
            //txtaddtax.Text = Convert.ToString(a);


            double amount = basicprice - disa + tax + atax + cess1;
            amount = Math.Round(amount, 2);
            //lblroundof.Text = Math.Round((Math.Round(amount, 0) - amount), 2).ToString();
            // double afterdis = total - discount;
            // double finaltotal = afterdis * qty;
            //  double qtyamt = price * qty;
            //    Double cess = Convert.ToDouble(basicprice) * Convert.ToDouble(dt.Rows[0]["cessper"].ToString()) / 100;
            //            Double cessamt = Convert.ToDouble(dt.Rows[0]["cessamt"].ToString());
            //          Double tcess = cess + cessamt;
            temptable.Rows.Add(barcode, itemname, qty, rate, basicprice, disper, disa, tax, atax, amount, batchno, cess1,cmbagentname.SelectedValue);
            // dgvitem.DataSource = temptable;
            dgvitem.Columns[1].ReadOnly = true;
            dgvitem.Columns[3].ReadOnly = true;
            dgvitem.Columns[4].ReadOnly = true;
            dgvitem.Columns[5].ReadOnly = true;
            dgvitem.Columns[6].ReadOnly = true;
            dgvitem.Columns[7].ReadOnly = true;
            dgvitem.Columns[8].ReadOnly = true;
            dgvitem.Columns[9].ReadOnly = true;
            if (temptable.Rows.Count == 1)
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                dgvitem.Columns.Add(btn);
                btn.HeaderText = "Delete";
                btn.Text = "";
                btn.Name = "btndelete";
                btn.UseColumnTextForButtonValue = true;
                dgvitem.Columns[13].Width = 38;
            }

            //  dgvitem.DataSource = temptable;
            // totaltaxbox();
        }

        private void PrintToPrinter()
        {
            PrintReport(System.Windows.Forms.Application.StartupPath + "\\POSInvoiceA5Portrait.rpt",
                "Send To OneNote 2010");
        }
        private void PrintReport(string reportPath, string PrinterName)
        {
            CrystalDecisions.CrystalReports.Engine.ReportDocument rptDoc =
                                new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            rptDoc.Load(reportPath);

            CrystalDecisions.Shared.PageMargins objPageMargins;
            objPageMargins = rptDoc.PrintOptions.PageMargins;
            objPageMargins.bottomMargin = 100;
            objPageMargins.leftMargin = 100;
            objPageMargins.rightMargin = 100;
            objPageMargins.topMargin = 100;
            rptDoc.PrintOptions.ApplyPageMargins(objPageMargins);
            rptDoc.PrintOptions.PrinterName = PrinterName;
            rptDoc.PrintToPrinter(1, false, 0, 0);
        }
        public void totalprice()
        {
            try
            {
                t7 = Convert.ToDouble(txtstotal.Text);
                t8 = Convert.ToDouble(txtdisamt.Text);
                //    t9 = Convert.ToDouble(txttaxamt.Text);

                t10 = (t7 - t8) + t9;
                // txtgtotal.Text = t10.ToString();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }
        double at;
        private Master master;
        private TabControl tabControl;
        private SelectItemBatchWise selectItemBatchWise;
        private string batch;
        private POSBillList pOSBillList;
        public void totaltaxbox()
        {
            try
            {
                n = 0;
                o = 0;
                p = 0;
                q = 0;
                at = 0;
                tax = 0;
                cess = 0;
                for (int i = 0; i < dgvitem.Rows.Count; i++)
                {
                    n += Convert.ToDouble(dgvitem.Rows[i].Cells[2].Value);
                    o += Convert.ToDouble(dgvitem.Rows[i].Cells[4].Value);
                    p += Convert.ToDouble(dgvitem.Rows[i].Cells[6].Value);
                    tax += Convert.ToDouble(dgvitem.Rows[i].Cells[7].Value);
                    at += Convert.ToDouble(dgvitem.Rows[i].Cells[8].Value);
                    q += Convert.ToDouble(dgvitem.Rows[i].Cells[9].Value);
                    cess += Convert.ToDouble(dgvitem.Rows[i].Cells[11].Value);

                }

                txtqty.Text = n.ToString();
                txttotalitem.Text = Convert.ToString(dgvitem.Rows.Count);
                double sub = o;
                double aTruncated1 = Math.Truncate(sub * 100) / 100;
                string sub1 = aTruncated1.ToString();
                txtstotal.Text = sub1.ToString();
                double gdis = p;
                double aTruncated2 = Math.Truncate(gdis * 100) / 100;
                gdis1 = aTruncated2.ToString();
                txtdisamt.Text = gdis1.ToString();
                double total = q;
                double aTruncated3 = Math.Truncate(total * 100) / 100;
                total1 = aTruncated3.ToString();
                txtgtotal.Text = total1.ToString();
                double tax1 = tax;
                double aTruncated4 = Math.Truncate(tax1 * 100) / 100;
                tax12 = aTruncated4.ToString();
                txttotaltax.Text = tax12.ToString();
                double atax1 = at;
                double aTruncated5 = Math.Truncate(atax1 * 100) / 100;
                string tax13 = aTruncated5.ToString();
                txtaddtax.Text = tax13.ToString();

                double atax12 = cess;
                double aTruncated51 = Math.Truncate(atax12 * 100) / 100;
                string tax131 = aTruncated51.ToString();
                txtcess.Text = tax131.ToString();

                string adis = "0";
                txtadddisamt.Text = adis;

                if (Convert.ToBoolean(options.Rows[0]["autoroundoffpos"].ToString()) == true)
                {
                    double t = Convert.ToDouble(total1);


                    lblroundof.Text = Math.Round((Math.Round(t, 0) - t), 2).ToString();
                    double g = Convert.ToDouble(lblroundof.Text);
                    double ga = Convert.ToDouble(txtgtotal.Text);
                    if (lblroundof.Text != "0")
                    {
                        lblroundof.Visible = true;
                        lblro.Visible = true;
                        double gaa = ga + g;
                        txtgtotal.Text = Math.Round(gaa, 2).ToString(".00");
                    }
                    else
                    {
                        lblroundof.Visible = false;
                        lblro.Visible = false;
                    }
                }
                gtotalamt = txtgtotal.Text;
            }
            catch (Exception ex)
            {

            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            if (keyData == Keys.Alt && keyData == Keys.D)
            {

                txtadddisamt.Focus();
                this.ActiveControl = txtadddisamt;
            }
            if (keyData == Keys.Escape)
            {
                DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    master.RemoveCurrentTab();
                    temptable = new DataTable();

                }
                return true;
            }
            if (keyData == (Keys.Alt | Keys.U))
            {
                if (dgvitem.DataSource == null)
                {
                    MessageBox.Show("Enter Items");
                }
                else
                {
                    if (Convert.ToBoolean(options.Rows[0]["savedialogbox"].ToString()) == true)
                    {
                        monytype m = new monytype(this);
                        m.Show();
                    }
                    else
                    {
                        savedata();
                        print();
                        clearall();
                    }
                }
                return true;
            }
            if (keyData == Keys.F8)
            {
                try
                {

                    //txtbarcode.Focus();
                    dgvitem.Focus();
                    dgvitem.Enabled = true;
                    DataGridViewCell cell = dgvitem.Rows[0].Cells[2];
                    dgvitem.CurrentCell = cell;
                    dgvitem.BeginEdit(true);



                    dgvitem.Columns[0].Visible = false;
                    dgvitem.Columns[0].ReadOnly = true;
                    dgvitem.Columns[1].ReadOnly = true;
                    dgvitem.Columns[3].ReadOnly = true;
                    dgvitem.Columns[4].ReadOnly = true;
                    dgvitem.Columns[5].ReadOnly = true;

                    txtbarcode.Text = "";

                }
                catch (Exception ex)
                {
                    //  MessageBox.Show(ex.Message);
                }
            }
            if (keyData == Keys.F6)
            {
                //  savedata();
                // PrintToPrinter();
                //key = "F6";
                //monytype m = new monytype();
                //m.Show();
                //dgvitem.DataSource = null;

                //temptable = new DataTable();
                //clearall();


            }
            //if (keyData == Keys.F2)
            //{
            //    try
            //    {
            //        master.RemoveCurrentTab();

            //    }
            //    catch (Exception ex)
            //    {
            //     //   MessageBox.Show(ex.Message);
            //    }
            //}
            //if (keyData == (Keys.ControlKey | Keys.I))
            //{
            //    try
            //    {

            //        Itemmaster m = new Itemmaster(master, tabControl);
            //        master.AddNewTab(m);

            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
            //if (keyData == (Keys.ControlKey | Keys.A))
            //{
            //    try
            //    {

            //        ClientRegistration am = new ClientRegistration(master, tabControl);
            //        master.AddNewTab(am);

            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
            if (keyData == Keys.F1)
            {
                try
                {

                    txtbarcode.Focus();

                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }
            //if (keyData == Keys.F8)
            //{
            //    try
            //    {
            //        dgvitem.Focus();
            //     //   dgvitem.Enabled = true;
            //        dgvitem.Rows[0].Selected = true;


            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
            //if (keyData == Keys.F10)
            //{
            //    try
            //    {

            //       // dgvitem.Enabled = false;

            //        //temptable.Clear();
            //        //dgvitem.DataSource = temptable;
            //        //temptable = new DataTable();
            //        clearall();

            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void dgvitem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt == true && e.KeyCode == Keys.D)
            {
                txtadddisamt.Focus();
            }
            //if (e.KeyData == Keys.Delete)
            //{
            //    try
            //    {
            //        DialogResult dr = MessageBox.Show("Do you want to Delete?", "Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //        if (dr == DialogResult.Yes)
            //        {
            //            int selectedCount = dgvitem.SelectedRows.Count;
            //            if (dgvitem.SelectedRows.Count > 0)
            //            {
            //                dgvitem.Rows.RemoveAt(this.dgvitem.SelectedRows[0].Index);
            //            }
            //        }
            //        totaltaxbox();
            //        txtbarcode.Focus();
            //    }
            //    catch (Exception ex)
            //    {
            //        // MessageBox.Show(ex.Message);
            //    }
            //}
            if (e.KeyData == Keys.F1)
            {
                try
                {

                    txtbarcode.Focus();

                }
                catch (Exception ex)
                {
                    //  MessageBox.Show(ex.Message);
                }
            }

        }

        private void dgvitem_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            {
                try
                {
                    a = ((DataGridView)sender).CurrentRow.Cells[0].Value.ToString();
                    b = ((DataGridView)sender).CurrentRow.Cells[10].Value.ToString();



                    DataRow dr = temptable.Select("BARCODE='" + a + "' and Batchno='"+b+"'").FirstOrDefault();
                    if (dr != null)
                    {
                        DataTable pricetab = new DataTable();
                        pricetab = sql.getdataset("select Defaultprice from Options");
                        Dprice = pricetab.Rows[0]["Defaultprice"].ToString();
                        DataTable productid = new DataTable();
                        productid = sql.getdataset("select DISTINCT p.taxslab,p.ProductID from ProductMaster p inner join ProductPriceMaster pp on p.Productid=pp.Productid where   p.Product_Name='" +a + "'or pp.barcode='" + a + "' and pp.BatchNo='" + b + "' and p.isactive=1 and pp.isactive=1");
                      //  productid = sql.getdataset("select DISTINCT p.taxslab,p.ProductID from ProductMaster p inner join ProductPriceMaster pp on p.Productid=pp.Productid where p.Product_Name='" + a + "'or pp.barcode='" + a + "' and p.isactive=1 and pp.isactive=1");
                        dt = sql.getdataset("select p.cessper,p.cessamt,pp.Barcode,p.Product_Name,pp." + Dprice + " as Dprice,pp.BasicPrice,i.igst,i.additax,i.cgst,i.sgst from ProductPriceMaster pp inner join ProductMaster p on p.ProductID=pp.Productid  inner join TaxSlabMaster i on i.Taxslabname=p.taxslab where p.ProductID='" + productid.Rows[0]["ProductID"].ToString() + "' and p.taxslab='" + productid.Rows[0]["taxslab"].ToString() + "'and i.saletypename='" + lblsaletype.Text + "' and pp.BatchNo='" + b + "' and p.isactive=1 and pp.isactive=1 and i.isactive=1");
                      //  dt = sql.getdataset("select p.cessper,p.cessamt,pp.Barcode,p.Product_Name,pp." + Dprice + " as Dprice,pp.BasicPrice,i.igst,i.additax,i.cgst,i.sgst from ProductPriceMaster pp inner join ProductMaster p on p.ProductID=pp.Productid  inner join TaxSlabMaster i on i.Taxslabname=p.taxslab where p.ProductID='" + productid.Rows[0]["ProductID"].ToString() + "' and p.taxslab='" + productid.Rows[0]["taxslab"].ToString() + "'and i.saletypename='" + lblsaletype.Text + "' and p.isactive=1 and pp.isactive=1 and i.isactive=1");
                        //   dt = sql.getdataset("select p.*,b.* from productmaster p inner join ProductPriceMaster b on p.ProductID=b.ProductID where b.barcode='" + txtbarcode.Text + "' or p.product_name like '%" + txtbarcode.Text + "%'");
                        //  dt = sql.getdataset("Select p.Product_Name,p.Unit,pp.saleprice  as rate,pp.MRP as Amount from ProductMaster p inner join ProductPriceMaster pp on p.ProductID = pp.Productid where p.ProductID = pp.Productid and p.Product_Name='" + txtbarcode.Text + "'or pp.barcode='" + txtbarcode.Text + "'");


                        //  dt = sql.getdataset("Select p.Product_Name,p.Convfactor,(pp.saleprice * 100)/(100+ (pp.vat+pp.addvat)) as rate,p.Convfactor*((pp.saleprice * 100)/(100+ (pp.vat+pp.addvat))) as amount,pp.vat,pp.addvat from ProductMaster p inner join ProductPriceMaster pp on p.ProductID = pp.Productid where p.ProductID = pp.Productid and p.Product_Name='" + txtbarcode.Text + "'or pp.barcode='" + txtbarcode.Text + "'");
                        //   dt = sql.getdataset("select i.qty,i.mrp,i.discount,i.barcode,t.Product_Name from ProductMaster t inner join ProductPriceMaster i on i.taxslab=t.id  where barcode='" + txtbarcode.Text + "' or i.itemname='" + txtbarcode.Text + "'");
                        if (dt.Rows.Count > 0)
                        {

                            Double qty = Convert.ToDouble(dr["qty"]);
                            Double pbasicprice = Convert.ToDouble(dr["BASIC AMT"]);
                            Double discounta = Convert.ToDouble(dr["DISCOUNT AMT"]);
                            Double igst = Convert.ToDouble(dt.Rows[0]["igst"].ToString());
                            Double netamt = Convert.ToDouble(dr["AMOUNT"]);
                            //  Double tcess = Convert.ToDouble(dr["cess"]);
                            Double addtax = Convert.ToDouble(dt.Rows[0]["additax"].ToString());
                            Double dprice = Convert.ToDouble(dr["Rate"]);
                            //    qty++;
                            DataTable discount = new DataTable();
                            discount = sql.getdataset("select Discount,SpecialRate from PartyRates where ItemID='" + productid.Rows[0]["ProductID"].ToString() + "'");
                            if (discount.Rows.Count > 0)
                            {
                                disper = Convert.ToInt32(discount.Rows[0]["Discount"].ToString());
                                if (Convert.ToInt32(discount.Rows[0]["SpecialRate"].ToString()) > 0)
                                {
                                    dt.Rows[0]["Dprice"] = Convert.ToInt32(discount.Rows[0]["SpecialRate"].ToString());
                                    disamt = 1;
                                }
                                else
                                {
                                    disamt = 0;
                                }
                                dt.AcceptChanges();
                            }
                            else
                            {
                                disper = 0;
                                disamt = 0;
                            }
                            //double basicprice = price * qty;
                            //double disa = price * disper / 100;
                            //double mrp1 = price - disa;
                            //double tax = mrp1 * igst;
                            double MRP = Convert.ToDouble(dr["Rate"]);
                            double total = MRP;
                            total = Math.Round(total, 2);
                            double cessper1 = Convert.ToDouble(dt.Rows[0]["cessper"].ToString());
                            double cessamt1 = Convert.ToDouble(dt.Rows[0]["cessamt"].ToString());
                            //consider example of MRP=67.83, cessamt1=20, igst=18, addtax=0, cessper=4 and get the total tax amt including of all kind of taxes with cess also. as per the example the tax amt is: 8.63
                            //double tax = ((MRP - cessamt1) * (igst + addtax + cessper1) / (100 + (igst + addtax + cessper1)));
                            ////    if (addtax != 0)
                            ////    {
                            ////        tax = tax * addtax / 100;
                            ////    }
                            ////  tax = Math.Round(tax, 2);
                            ////this amount represent the before discount deduct from sale/mrp value. as per the example the amount should be 39.2
                            //double aftrdisamt = MRP - cessamt1 - tax;
                            //double rate = aftrdisamt;
                            //this is the actual rate of item. as per the example the amount is 40.
                            //if (disper > 0 && disamt == 0)
                            //{
                            //    double bfrdisamt = (aftrdisamt * 100) / (100 - disper);
                            //    rate = aftrdisamt;
                            //}
                            //else if (disper > 0 && disamt == 1)
                            //{
                            //    rate = aftrdisamt;
                            //}
                            //else if (disper == 0 && disamt == 1)
                            //{
                            //    rate = aftrdisamt;
                            //}
                            double rate = Math.Round(MRP, 2);
                            double basicprice = rate * qty;
                            basicprice = Math.Round(basicprice, 2);
                            double disa = disamt;
                            double[] dis = new double[2];
                            dis = calculatediscount(dt.Rows[0]["Product_Name"].ToString(), "ALL PARTIES", basicprice);
                            tax = Math.Round((basicprice - dis[1]) * igst / (100), 2);
                            double cess1 = Math.Round((basicprice - dis[1]) * cessper1 / (100), 2);
                            cess1 = cess1 + (cessamt1 * qty);
                            disper = Math.Round(dis[0], 2);
                            disa = Math.Round(dis[1], 2);
                            //if (disper != 0)
                            //{
                            //   disa = (Convert.ToDouble(basicprice) * Convert.ToDouble(disper)) / 100;
                            //}
                            //else
                            //{
                            //    disper = (100 * Convert.ToDouble(disamt)) / Convert.ToDouble(basicprice);
                            //}
                            // disper = Math.Round(disper, 2);

                            if (txtaddtax.Text == "")
                            {
                                txtaddtax.Text = "0";
                            }
                            double adtax = Math.Round((basicprice - dis[1]) * (addtax) / (100), 2);
                            double atax = Math.Round(adtax, 2);
                            //double aatax = Convert.ToDouble(txtaddtax.Text);
                            //double a = aatax + atax;
                            //txtaddtax.Text = Convert.ToString(a);


                            double amount = basicprice - disa + tax + atax + cess1;
                            amount = Math.Round(amount, 2);


                            // lblroundof.Text = Math.Round((Math.Round(amount, 0) - amount), 2).ToString();


                            basicprice = rate * qty;
                            //double[] dis = new double[2];
                            //dis = calculatediscount(txtbarcode.Text, "ALL PARTIES", basicprice);
                            //      Double cess = Convert.ToDouble(basicprice) * Convert.ToDouble(dt.Rows[0]["cessper"].ToString()) / 100;
                            //      Double cessamt = Convert.ToDouble(dt.Rows[0]["cessamt"].ToString());
                            //      Double tcess = cess + cessamt;

                            double cessval = cess1;
                            (dr["QTY"]) = qty;
                            (dr["BASIC AMT"]) = basicprice;
                            (dr["DISCOUNT AMT"]) = dis[1];
                            (dr["GST AMT"]) = tax;
                            (dr["ADD TAX"]) = atax;
                            (dr["AMOUNT"]) = amount;
                            (dr["cess"]) = cessval;

                            totaltaxbox();

                        }
                    }
                }
                catch (Exception ex)
                {
                    // MessageBox.Show(ex.Message);

                }
            }
        }

        private void dgvitem_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            {
                // txtbarcode.Focus();
            }
        }

        private void DefaultPOS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt == true && e.KeyCode == Keys.D)
            {
                txtaddtax.Focus();
            }
            if (e.KeyData == Keys.F6)
            {
                //savedata();
                //// PrintToPrinter();
                //key = "F6";
                //monytype m = new monytype();
                //m.Show();
                //dgvitem.DataSource = null;

                //temptable = new DataTable();
                //clearall();


            }
            //if (e.KeyData == Keys.F1)
            //{
            //    try
            //    {

            //        //txtbarcode.Focus();
            //        dgvitem.Focus();
            //       // dgvitem.Enabled = true;
            //        e.Handled = true;
            //        DataGridViewCell cell = dgvitem.Rows[0].Cells[2];
            //        dgvitem.CurrentCell = cell;
            //        dgvitem.BeginEdit(true);



            //        dgvitem.Columns[0].Visible = false;
            //        dgvitem.Columns[0].ReadOnly = true;
            //        dgvitem.Columns[1].ReadOnly = true;
            //        dgvitem.Columns[3].ReadOnly = true;
            //        dgvitem.Columns[4].ReadOnly = true;
            //        dgvitem.Columns[5].ReadOnly = true;

            //        txtbarcode.Text = "";

            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
            //if (e.KeyData == Keys.F2)
            //{
            //    try
            //    {
            //        master.RemoveCurrentTab();

            //    }
            //    catch (Exception ex)
            //    {
            //       // MessageBox.Show(ex.Message);
            //    }
            //}
            if (e.KeyData == (Keys.ControlKey | Keys.I))
            {
                try
                {

                    Itemmaster m = new Itemmaster(master, tabControl);
                    master.AddNewTab(m);

                }
                catch (Exception ex)
                {
                    //  MessageBox.Show(ex.Message);
                }
            }
            if (e.KeyData == (Keys.ControlKey | Keys.A))
            {
                try
                {

                    ClientRegistration am = new ClientRegistration(master, tabControl);
                    master.AddNewTab(am);

                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }

            if (e.KeyData == Keys.Escape)
            {
                try
                {

                    this.Close();

                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }
            if (e.KeyData == Keys.F1)
            {
                try
                {

                    txtbarcode.Focus();

                }
                catch (Exception ex)
                {
                    // MessageBox.Show(ex.Message);
                }
            }
            //if (e.KeyData == Keys.F8)
            //{
            //    try
            //    {
            //        dgvitem.Focus();
            //       // dgvitem.Enabled = true;
            //        dgvitem.Rows[0].Selected = true;


            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
            //if (e.KeyData == Keys.F10)
            //{
            //    try
            //    {

            //        //dgvitem.Enabled = false;
            //        //temptable.Clear();
            //        //dgvitem.DataSource = temptable;
            //        //temptable = new DataTable();
            //        clearall();

            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
        }

        private void btnreturn_Click(object sender, EventArgs e)
        {

            {
                try
                {
                    DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        master.RemoveCurrentTab();
                        //Master ma = new Master();
                        //ma.Show();
                        //ma.Close();
                    }


                }
                catch (Exception ex)
                {
                    // MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnnosale_Click(object sender, EventArgs e)
        {
            key = "F3";
            //savedata();
            //dgvitem.DataSource = null;
            //temptable = new DataTable();
            //clearall();
            if (dgvitem.RowCount == 0)
            {
                MessageBox.Show("Enter Items");
            }
            else
            {
                if (Convert.ToBoolean(options.Rows[0]["savedialogbox"].ToString()) == true)
                {
                    gtotalamt = txtgtotal.Text;
                    saletype = lblsaletype.Text;
                    monytype m = new monytype(this);
                    m.ShowDialog();
                }
                else
                {
                    savedata();
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[9]["p"].ToString() == "True")
                        {
                            print();
                        }
                        else
                        {
                            MessageBox.Show("You don't have Permission To Print");
                            return;
                        }
                    }
                    clearall();
                }
            }


        }
        string finalbillno;
        private void print()
        {
            ChangeNumbersToWords sh = new ChangeNumbersToWords();
            String s1 = Math.Round(Convert.ToDouble(DefaultPOS.gtotalamt), 2).ToString("########.00");
            string[] words = s1.Split('.');


            string str = sh.changeToWords(words[0]);
            string str1 = sh.changeToWords(words[1]);
            if (str1 == " " || str1 == null || words[1] == "00")
            {
                str1 = "Zero ";
            }
            String inword = "In words: " + str + "and " + str1 + "Paise Only";
            if (Convert.ToBoolean(options.Rows[0]["requirprintpopupinpos"].ToString()) == true)
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Print Bill?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    string bid;
                    if (DefaultPOS.str == "")
                    {
                        bid = ListPOS.iid;
                    }
                    else
                    {
                        bid = DefaultPOS.str;
                    }
                    // DataTable client = sql.getdataset("select * from clientmaster where isactive=1 and clientID='" + cmbcustname.SelectedValue + "'");
                    DataTable dt1 = sql.getdataset("select * from BillPOSMaster WHERE isactive=1 and Billid='" + bid + "'");
                    DataTable dt2 = sql.getdataset("select * from BillPOSProductMaster WHERE isactive=1 and Billid='" + bid + "'");


                    DataTable dt3 = sql.getdataset("select * from company WHERE isactive=1");
                    DataTable dt4 = sql.getdataset("select sum(amount)-sum(discountamt) as basicamount,sum(sgst) as sgst,sum(cgst) as cgst,sum(amount)+sum(sgst)+sum(cgst)-sum(discountamt) as total, igst,sum(Addtaxamt) as Addtaxamt,sum(Addtax) as Addtax  from BillPOSProductMaster WHERE isactive=1 and Billid='" + bid + "' group by igst");
                    string taxable = "Taxable Amt" + Environment.NewLine, cgstper = "CGST % " + Environment.NewLine, cgstamt = "CGST AMT" + Environment.NewLine, sgstper = "SGST %" + Environment.NewLine, sgstamt = "SGST AMT" + Environment.NewLine, totalamt = "Total AMT" + Environment.NewLine, addper = "AddTax%" + Environment.NewLine, addamt = "AddAmt" + Environment.NewLine;
                    double cgst = 0, sgst = 0, basicamt = 0, nettotal = 0, Addtax = 0; ;
                    for (int i = 0; i < dt4.Rows.Count; i++)
                    {
                        taxable += Environment.NewLine + dt4.Rows[i]["basicamount"].ToString();
                        basicamt += Convert.ToDouble(dt4.Rows[i]["basicamount"].ToString());

                        cgstper += Environment.NewLine + (Convert.ToDouble(dt4.Rows[i]["igst"].ToString()) / 2).ToString();
                        cgstamt += Environment.NewLine + dt4.Rows[i]["cgst"].ToString();
                        cgst += Convert.ToDouble(dt4.Rows[i]["cgst"].ToString());

                        sgstper += Environment.NewLine + (Convert.ToDouble(dt4.Rows[i]["igst"].ToString()) / 2).ToString();
                        sgstamt += Environment.NewLine + dt4.Rows[i]["sgst"].ToString();
                        sgst += Convert.ToDouble(dt4.Rows[i]["sgst"].ToString());

                        addper += Environment.NewLine + (Convert.ToDouble(dt4.Rows[i]["Addtax"].ToString()) / 2).ToString();
                        addamt += Environment.NewLine + dt4.Rows[i]["Addtaxamt"].ToString();
                        Addtax += Convert.ToDouble(dt4.Rows[i]["Addtaxamt"].ToString());

                        totalamt += Environment.NewLine + dt4.Rows[i]["total"].ToString();
                        nettotal += Convert.ToDouble(dt4.Rows[i]["total"].ToString());
                    }

                    if (Convert.ToBoolean(options.Rows[0]["autoroundoffpos"].ToString()) == true)
                    {
                        nettotal = Math.Round(nettotal, 0);
                    }
                    prn.execute("delete from printing");
                    int j = 1;
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        try
                        {
                            if (options.Rows[0]["posbillno"].ToString() == "Continuous")
                            {
                                finalbillno = dt1.Rows[0]["Billid"].ToString();
                            }
                            else
                            {
                                finalbillno = dt1.Rows[0]["billno"].ToString();
                            }
                            DataTable hsn = sql.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + dt2.Rows[i]["ItemName"].ToString() + "'");
                            DataTable item = sql.getdataset("select * from TaxSlabMaster where isactive=1 and saletypename='" + lblsaletype.Text + "' and Taxslabname='" + hsn.Rows[0]["taxslab"].ToString() + "'");
                            string mrp = sql.ExecuteScalar("select MRP from ProductPriceMaster where isactive=1 and Productid='" + hsn.Rows[0]["Productid"].ToString() + "'");

                            string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34,T35,T36,T37,T38,T39,T40,T41,T42,T43,T44,T45,T46,T47,T48,T49,T50,T51,T52,T53,T54,T55,T56,T57,T58,T59,T60,T61,T62,T63,T64,T65,T66,T67,T68,T69,T70,T71,T72,T73,T74,T75,T76,T77,T78,T79,T80,T81,T82,T83,T84,T85,T86,T87,T88,T89,T90,T91,T92,T93,T94,T95,T96)VALUES";
                            // qry += "('" + dt3.Rows[0]["CompanyName"].ToString() + "','" + dt3.Rows[0]["Address"].ToString() + "','" + dt3.Rows[0]["Address2"].ToString() + "','" + dt3.Rows[0]["city"].ToString() + "','" + dt3.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Billid"].ToString() + "','" + Convert.ToDateTime(dt1.Rows[0]["BillDate"].ToString()).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(dt1.Rows[0]["BillDate"].ToString()).ToString("HH:mm tt") + "','" + hsn.Rows[0]["Hsn_Sac_Code"].ToString() + "','" + dt2.Rows[i]["ItemName"].ToString() + "','" + dt2.Rows[i]["qty"].ToString() + "','" + dt2.Rows[i]["Rate"].ToString() + "','" + dt2.Rows[i]["Amount"].ToString() + "')";
                            qry += "('" + dt3.Rows[0]["CompanyName"].ToString() + "','" + dt3.Rows[0]["SubName"].ToString() + "','" + dt3.Rows[0]["Address"].ToString() + "','" + dt3.Rows[0]["Address2"].ToString() + "','" + dt3.Rows[0]["City"].ToString() + "','" + dt3.Rows[0]["Phone"].ToString() + "','" + dt3.Rows[0]["Mobile"].ToString() + "','" + dt3.Rows[0]["Email"].ToString() + "','" + dt3.Rows[0]["Website"].ToString() + "','" + dt3.Rows[0]["CSTNo"].ToString() + "','" + dt3.Rows[0]["PANNo"].ToString() + "','" + dt3.Rows[0]["VATNo"].ToString() + "','" + dt3.Rows[0]["DLNo1"].ToString() + "','" + dt3.Rows[0]["DLNo2"].ToString() + "','" + dt3.Rows[0]["DealsIn"].ToString() + "','" + dt3.Rows[0]["Stockist"].ToString() + "','" + dt3.Rows[0]["currency"].ToString() + "','" + dt3.Rows[0]["StartDate"].ToString() + "','" + dt3.Rows[0]["EndDate"].ToString() + "','" + dt3.Rows[0]["MyDSNName"].ToString() + "','" + dt3.Rows[0]["LinkRemote"].ToString() + "','" + dt3.Rows[0]["DBType"].ToString() + "','" + dt3.Rows[0]["Catalyst"].ToString() + "','" + finalbillno + "','" + dt1.Rows[0]["BillDate"].ToString() + "','" + dt1.Rows[0]["Terms"].ToString() + "','" + dt1.Rows[0]["count"].ToString() + "','" + dt1.Rows[0]["totalqty"].ToString() + "','" + dt1.Rows[0]["totalbasic"].ToString() + "','" + dt1.Rows[0]["totaltax"].ToString() + "','" + dt1.Rows[0]["totalnet"].ToString() + "','" + dt1.Rows[0]["disamt"].ToString() + "','" + dt1.Rows[0]["adddisamt"].ToString() + "','" + dt1.Rows[0]["bankname"].ToString() + "','" + dt1.Rows[0]["cardnumbar"].ToString() + "','" + dt1.Rows[0]["cardtype"].ToString() + "','" + dt1.Rows[0]["expirydate"].ToString() + "','" + dt1.Rows[0]["apprcode"].ToString() + "','" + dt1.Rows[0]["refno"].ToString() + "','" + dt1.Rows[0]["amountrs"].ToString() + "','" + dt1.Rows[0]["invno"].ToString() + "','" + dt1.Rows[0]["cardholdername"].ToString() + "','" + dt1.Rows[0]["cashtendered"].ToString() + "','" + dt1.Rows[0]["change"].ToString() + "','" + dt2.Rows[i]["ItemName"].ToString() + "','" + dt2.Rows[i]["Qty"].ToString() + "','" + dt2.Rows[i]["Rate"].ToString() + "','" + dt2.Rows[i]["Amount"].ToString() + "','" + dt2.Rows[i]["Total"].ToString() + "','" + dt2.Rows[i]["igst"].ToString() + "','" + dt2.Rows[i]["Addtax"].ToString() + "','" + dt2.Rows[i]["Discount"].ToString() + "','" + dt2.Rows[i]["Per"].ToString() + "','" + dt2.Rows[i]["SerCharge"].ToString() + "','" + dt2.Rows[i]["PackCharge"].ToString() + "','" + dt2.Rows[i]["RoundOf"].ToString() + "','" + dt2.Rows[i]["NetTotal"].ToString() + "','" + dt2.Rows[i]["CashTendered"].ToString() + "','" + dt2.Rows[i]["Change"].ToString() + "','" + dt2.Rows[i]["sgst"].ToString() + "','" + dt2.Rows[i]["cgst"].ToString() + "','" + hsn.Rows[0]["ProductID"].ToString() + "','" + hsn.Rows[0]["CompanyID"].ToString() + "','" + hsn.Rows[0]["GroupName"].ToString() + "','" + hsn.Rows[0]["Product_Name"].ToString() + "','" + hsn.Rows[0]["Unit"].ToString() + "','" + hsn.Rows[0]["Altunit"].ToString() + "','" + hsn.Rows[0]["Convfactor"].ToString() + "','" + hsn.Rows[0]["Packing"].ToString() + "','" + hsn.Rows[0]["IsBatch"].ToString() + "','" + hsn.Rows[0]["Hsn_Sac_Code"].ToString() + "','" + dt3.Rows[0]["CompanyID"].ToString() + "','" + item.Rows[0]["sgst"].ToString() + "','" + item.Rows[0]["cgst"].ToString() + "','" + item.Rows[0]["additax"].ToString() + "','" + options.Rows[0]["autosaletype"].ToString() + "','" + taxable + "','" + cgstper + "','" + cgstamt + "','" + sgstper + "','" + sgstamt + "','" + totalamt + "','" + basicamt + "','" + cgst + "','" + sgst + "','" + nettotal.ToString("N2") + "','" + dt2.Rows[i]["DiscountAmt"].ToString() + "','" + addper + "','" + addamt + "','" + Addtax + "','" + dt1.Rows[0]["customername"].ToString() + "','" + dt1.Rows[0]["customercity"].ToString() + "','" + dt1.Rows[0]["customermobile"].ToString() + "','" + hsn.Rows[0]["taxslab"].ToString() + "','" + dt1.Rows[0]["billno"].ToString() +"','"+mrp+ "')";
                            prn.execute(qry);
                        }
                        catch
                        {
                        }
                    }
                    DataTable multyprint = sql.getdataset("select defaultbill from Options");
                    if (Convert.ToBoolean(options.Rows[0]["multyprintinpos"].ToString()) == true)
                    {
                        Print popup = new Print("Pos");
                        popup.ShowDialog();
                        popup.Dispose();
                    }
                    else
                    {
                        string strreport = Application.StartupPath + "\\" + "QuickSale.rpt";
                        SQLReport sqlreport = new SQLReport(strreport, "Pos");
                        DataTable bill = sql.getdataset("select defaultbill from Options");
                        if (bill.Rows.Count > 0)
                        {
                            if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                            {
                                sqlreport.Show();
                            }
                            else
                            {
                                //SaleReport sale = new SaleReport(str);
                                sqlreport.Show();
                                sqlreport.Hide();
                            }
                        }
                    }
                }
            }
            else
            {
                // DialogResult dr1 = MessageBox.Show("Do you want to Print Bill?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (dr1 == DialogResult.Yes)
                // {

                string bid;
                if (DefaultPOS.str == "")
                {
                    bid = ListPOS.iid;
                }
                else
                {
                    bid = DefaultPOS.str;
                }
                // DataTable client = sql.getdataset("select * from clientmaster where isactive=1 and clientID='" + cmbcustname.SelectedValue + "'");
                DataTable dt1 = sql.getdataset("select * from BillPOSMaster WHERE isactive=1 and Billid='" + bid + "'");
                DataTable dt2 = sql.getdataset("select * from BillPOSProductMaster WHERE isactive=1 and Billid='" + bid + "'");


                DataTable dt3 = sql.getdataset("select * from company WHERE isactive=1");
                DataTable dt4 = sql.getdataset("select sum(amount)-sum(discountamt) as basicamount,sum(sgst) as sgst,sum(cgst) as cgst,sum(amount)+sum(sgst)+sum(cgst)-sum(discountamt) as total, igst,sum(Addtaxamt) as Addtaxamt,sum(Addtax) as Addtax  from BillPOSProductMaster WHERE isactive=1 and Billid='" + bid + "' group by igst");
                string taxable = "Taxable Amt" + Environment.NewLine, cgstper = "CGST % " + Environment.NewLine, cgstamt = "CGST AMT" + Environment.NewLine, sgstper = "SGST %" + Environment.NewLine, sgstamt = "SGST AMT" + Environment.NewLine, totalamt = "Total AMT" + Environment.NewLine, addper = "AddTax%" + Environment.NewLine, addamt = "AddAmt" + Environment.NewLine;
                double cgst = 0, sgst = 0, basicamt = 0, nettotal = 0, Addtax = 0; ;
                for (int i = 0; i < dt4.Rows.Count; i++)
                {
                    taxable += Environment.NewLine + dt4.Rows[i]["basicamount"].ToString();
                    basicamt += Convert.ToDouble(dt4.Rows[i]["basicamount"].ToString());

                    cgstper += Environment.NewLine + (Convert.ToDouble(dt4.Rows[i]["igst"].ToString()) / 2).ToString();
                    cgstamt += Environment.NewLine + dt4.Rows[i]["cgst"].ToString();
                    cgst += Convert.ToDouble(dt4.Rows[i]["cgst"].ToString());

                    sgstper += Environment.NewLine + (Convert.ToDouble(dt4.Rows[i]["igst"].ToString()) / 2).ToString();
                    sgstamt += Environment.NewLine + dt4.Rows[i]["sgst"].ToString();
                    sgst += Convert.ToDouble(dt4.Rows[i]["sgst"].ToString());

                    addper += Environment.NewLine + (Convert.ToDouble(dt4.Rows[i]["Addtax"].ToString()) / 2).ToString();
                    addamt += Environment.NewLine + dt4.Rows[i]["Addtaxamt"].ToString();
                    Addtax += Convert.ToDouble(dt4.Rows[i]["Addtaxamt"].ToString());

                    totalamt += Environment.NewLine + dt4.Rows[i]["total"].ToString();
                    nettotal += Convert.ToDouble(dt4.Rows[i]["total"].ToString());
                }

                if (Convert.ToBoolean(options.Rows[0]["autoroundoffpos"].ToString()) == true)
                {
                    nettotal = Math.Round(nettotal, 0);
                }
                prn.execute("delete from printing");
                int j = 1;
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    try
                    {
                        if (options.Rows[0]["posbillno"].ToString() == "Continuous")
                        {
                            finalbillno = dt1.Rows[0]["Billid"].ToString();
                        }
                        else
                        {
                            finalbillno = dt1.Rows[0]["billno"].ToString();
                        }
                        DataTable hsn = sql.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + dt2.Rows[i]["ItemName"].ToString() + "'");
                        DataTable item = sql.getdataset("select * from TaxSlabMaster where isactive=1 and saletypename='" + lblsaletype.Text + "' and Taxslabname='" + hsn.Rows[0]["taxslab"].ToString() + "'");
                        string mrp = sql.ExecuteScalar("select MRP from ProductPriceMaster where isactive=1 and Productid='" + hsn.Rows[0]["Productid"].ToString() + "'");

                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34,T35,T36,T37,T38,T39,T40,T41,T42,T43,T44,T45,T46,T47,T48,T49,T50,T51,T52,T53,T54,T55,T56,T57,T58,T59,T60,T61,T62,T63,T64,T65,T66,T67,T68,T69,T70,T71,T72,T73,T74,T75,T76,T77,T78,T79,T80,T81,T82,T83,T84,T85,T86,T87,T88,T89,T90,T91,T92,T93,T94,T95,T96)VALUES";
                        // qry += "('" + dt3.Rows[0]["CompanyName"].ToString() + "','" + dt3.Rows[0]["Address"].ToString() + "','" + dt3.Rows[0]["Address2"].ToString() + "','" + dt3.Rows[0]["city"].ToString() + "','" + dt3.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Billid"].ToString() + "','" + Convert.ToDateTime(dt1.Rows[0]["BillDate"].ToString()).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(dt1.Rows[0]["BillDate"].ToString()).ToString("HH:mm tt") + "','" + hsn.Rows[0]["Hsn_Sac_Code"].ToString() + "','" + dt2.Rows[i]["ItemName"].ToString() + "','" + dt2.Rows[i]["qty"].ToString() + "','" + dt2.Rows[i]["Rate"].ToString() + "','" + dt2.Rows[i]["Amount"].ToString() + "')";
                        qry += "('" + dt3.Rows[0]["CompanyName"].ToString() + "','" + dt3.Rows[0]["SubName"].ToString() + "','" + dt3.Rows[0]["Address"].ToString() + "','" + dt3.Rows[0]["Address2"].ToString() + "','" + dt3.Rows[0]["City"].ToString() + "','" + dt3.Rows[0]["Phone"].ToString() + "','" + dt3.Rows[0]["Mobile"].ToString() + "','" + dt3.Rows[0]["Email"].ToString() + "','" + dt3.Rows[0]["Website"].ToString() + "','" + dt3.Rows[0]["CSTNo"].ToString() + "','" + dt3.Rows[0]["PANNo"].ToString() + "','" + dt3.Rows[0]["VATNo"].ToString() + "','" + dt3.Rows[0]["DLNo1"].ToString() + "','" + dt3.Rows[0]["DLNo2"].ToString() + "','" + dt3.Rows[0]["DealsIn"].ToString() + "','" + dt3.Rows[0]["Stockist"].ToString() + "','" + dt3.Rows[0]["currency"].ToString() + "','" + dt3.Rows[0]["StartDate"].ToString() + "','" + dt3.Rows[0]["EndDate"].ToString() + "','" + dt3.Rows[0]["MyDSNName"].ToString() + "','" + dt3.Rows[0]["LinkRemote"].ToString() + "','" + dt3.Rows[0]["DBType"].ToString() + "','" + dt3.Rows[0]["Catalyst"].ToString() + "','" + finalbillno + "','" + dt1.Rows[0]["BillDate"].ToString() + "','" + dt1.Rows[0]["Terms"].ToString() + "','" + dt1.Rows[0]["count"].ToString() + "','" + dt1.Rows[0]["totalqty"].ToString() + "','" + dt1.Rows[0]["totalbasic"].ToString() + "','" + dt1.Rows[0]["totaltax"].ToString() + "','" + dt1.Rows[0]["totalnet"].ToString() + "','" + dt1.Rows[0]["disamt"].ToString() + "','" + dt1.Rows[0]["adddisamt"].ToString() + "','" + dt1.Rows[0]["bankname"].ToString() + "','" + dt1.Rows[0]["cardnumbar"].ToString() + "','" + dt1.Rows[0]["cardtype"].ToString() + "','" + dt1.Rows[0]["expirydate"].ToString() + "','" + dt1.Rows[0]["apprcode"].ToString() + "','" + dt1.Rows[0]["refno"].ToString() + "','" + dt1.Rows[0]["amountrs"].ToString() + "','" + dt1.Rows[0]["invno"].ToString() + "','" + dt1.Rows[0]["cardholdername"].ToString() + "','" + dt1.Rows[0]["cashtendered"].ToString() + "','" + dt1.Rows[0]["change"].ToString() + "','" + dt2.Rows[i]["ItemName"].ToString() + "','" + dt2.Rows[i]["Qty"].ToString() + "','" + dt2.Rows[i]["Rate"].ToString() + "','" + dt2.Rows[i]["Amount"].ToString() + "','" + dt2.Rows[i]["Total"].ToString() + "','" + dt2.Rows[i]["igst"].ToString() + "','" + dt2.Rows[i]["Addtax"].ToString() + "','" + dt2.Rows[i]["Discount"].ToString() + "','" + dt2.Rows[i]["Per"].ToString() + "','" + dt2.Rows[i]["SerCharge"].ToString() + "','" + dt2.Rows[i]["PackCharge"].ToString() + "','" + dt2.Rows[i]["RoundOf"].ToString() + "','" + dt2.Rows[i]["NetTotal"].ToString() + "','" + dt2.Rows[i]["CashTendered"].ToString() + "','" + dt2.Rows[i]["Change"].ToString() + "','" + dt2.Rows[i]["sgst"].ToString() + "','" + dt2.Rows[i]["cgst"].ToString() + "','" + hsn.Rows[0]["ProductID"].ToString() + "','" + hsn.Rows[0]["CompanyID"].ToString() + "','" + hsn.Rows[0]["GroupName"].ToString() + "','" + hsn.Rows[0]["Product_Name"].ToString() + "','" + hsn.Rows[0]["Unit"].ToString() + "','" + hsn.Rows[0]["Altunit"].ToString() + "','" + hsn.Rows[0]["Convfactor"].ToString() + "','" + hsn.Rows[0]["Packing"].ToString() + "','" + hsn.Rows[0]["IsBatch"].ToString() + "','" + hsn.Rows[0]["Hsn_Sac_Code"].ToString() + "','" + dt3.Rows[0]["CompanyID"].ToString() + "','" + item.Rows[0]["sgst"].ToString() + "','" + item.Rows[0]["cgst"].ToString() + "','" + item.Rows[0]["additax"].ToString() + "','" + options.Rows[0]["autosaletype"].ToString() + "','" + taxable + "','" + cgstper + "','" + cgstamt + "','" + sgstper + "','" + sgstamt + "','" + totalamt + "','" + basicamt + "','" + cgst + "','" + sgst + "','" + nettotal.ToString("N2") + "','" + dt2.Rows[i]["DiscountAmt"].ToString() + "','" + addper + "','" + addamt + "','" + Addtax + "','" + dt1.Rows[0]["customername"].ToString() + "','" + dt1.Rows[0]["customercity"].ToString() + "','" + dt1.Rows[0]["customermobile"].ToString() + "','" + hsn.Rows[0]["taxslab"].ToString() + "','" + dt1.Rows[0]["billno"].ToString() +"','"+mrp+ "')";
                        prn.execute(qry);
                    }
                    catch
                    {
                    }
                }
                DataTable multyprint = sql.getdataset("select defaultbill from Options");
                if (Convert.ToBoolean(options.Rows[0]["multyprintinpos"].ToString()) == true)
                {
                    Print popup = new Print("Pos");
                    popup.ShowDialog();
                    popup.Dispose();
                }
                else
                {
                    string strreport = Application.StartupPath + "\\" + "QuickSale.rpt";
                    SQLReport sqlreport = new SQLReport(strreport, "Pos");
                    DataTable bill = sql.getdataset("select defaultbill from Options");
                    if (bill.Rows.Count > 0)
                    {
                        if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                        {
                            sqlreport.Show();
                        }
                        else
                        {
                            //SaleReport sale = new SaleReport(str);
                            sqlreport.Show();
                            sqlreport.Hide();
                        }
                    }
                }

                // }
            }

        }
        private void btnitem_Click(object sender, EventArgs e)
        {

            {
                try
                {

                    Itemmaster m = new Itemmaster(master, tabControl);
                    master.AddNewTab(m);
                    //m.Show();

                }
                catch (Exception ex)
                {
                    //  MessageBox.Show(ex.Message);
                }
            }
        }
        public static string Decrypstatus(string cipherText)
        {
            string EncryptionKey = "00";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    statusreg = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        private void btncustomer_Click(object sender, EventArgs e)
        {

            {
                try
                {

                    ClientRegistration am = new ClientRegistration(master, tabControl);
                    master.AddNewTab(am);
                    //am.Show();

                }
                catch (Exception ex)
                {
                    //  MessageBox.Show(ex.Message);
                }
            }
        }
        OleDbSettings ods = new OleDbSettings();
        DataTable dtreg = new DataTable();
        private void btnprint_Click(object sender, EventArgs e)
        {
            ds = ods.getdata("Select * from tblreg");
            dtreg = ds.Tables[0];
            string reg = dtreg.Rows[0]["d16"].ToString();
            Decrypstatus(reg);
            if (statusreg == "Reg")
            {
                ListPOS frm = new ListPOS(this, master, tabControl);
                master.AddNewTab(frm);
            }
        }

        private void txtadddisamt_KeyUp(object sender, KeyEventArgs e)
        {

            {
                try
                {
                    Double diaamt = 0, adddisamt = 0, totaldis = 0, tgtotal = 0, tg = 0;

                    diaamt = Convert.ToDouble(gdis1);
                    tgtotal = Convert.ToDouble(total1);
                    if (txtadddisamt.Text == "")
                    {
                        double gd = diaamt;
                        double aTruncated2 = Math.Truncate(gd * 100) / 100;
                        string gdis123 = string.Format("{0:0.00}", aTruncated2);
                        txtdisamt.Text = gdis123.ToString();

                        double to = tgtotal;
                        double aTruncated3 = Math.Truncate(to * 100) / 100;
                        string tot = string.Format("{0:0.00}", aTruncated3);
                        txtgtotal.Text = tot.ToString();
                        double t = Convert.ToDouble(to);
                        if (Convert.ToBoolean(options.Rows[0]["autoroundoffpos"].ToString()) == true)
                        {
                            lblroundof.Text = Math.Round((Math.Round(t, 0) - t), 2).ToString();
                            double g = Convert.ToDouble(lblroundof.Text);
                            double ga = Convert.ToDouble(txtgtotal.Text);
                            if (lblroundof.Text != "0")
                            {
                                lblroundof.Visible = true;
                                lblro.Visible = true;
                                double gaa = ga + g;
                                txtgtotal.Text = Math.Round(gaa).ToString(".00");

                            }
                            else
                            {
                                lblroundof.Visible = false;
                                lblro.Visible = false;
                            }
                        }
                    }
                    else
                    {

                        adddisamt = Convert.ToDouble(txtadddisamt.Text);
                        tg = (tgtotal - adddisamt);
                        totaldis = (diaamt + adddisamt);
                        // txtdisamt.Text = totaldis.ToString();
                        // txtgtotal.Text = tg.ToString();

                        double gdis = totaldis;
                        double aTruncated2 = Math.Truncate(gdis * 100) / 100;
                        string gdis12 = string.Format("{0:0.00}", aTruncated2);
                        txtdisamt.Text = gdis12.ToString();

                        double total = tg;
                        double aTruncated3 = Math.Truncate(total * 100) / 100;
                        string total12 = string.Format("{0:0.00}", aTruncated3);
                        txtgtotal.Text = total12.ToString();
                        if (Convert.ToBoolean(options.Rows[0]["autoroundoffpos"].ToString()) == true)
                        {
                            double t = Convert.ToDouble(total);

                            lblroundof.Text = Math.Round((Math.Round(t, 0) - t), 2).ToString();
                            double g = Convert.ToDouble(lblroundof.Text);
                            double ga = Convert.ToDouble(txtgtotal.Text);
                            if (lblroundof.Text != "0")
                            {
                                lblroundof.Visible = true;
                                lblro.Visible = true;
                                double gaa = ga + g;
                                txtgtotal.Text = Math.Round(gaa).ToString(".00");

                            }
                            else
                            {

                                lblroundof.Visible = false;
                                lblro.Visible = false;
                            }
                        }
                        diaamt = 0;
                        adddisamt = 0;
                        totaldis = 0;


                    }


                }
                catch (Exception ex)
                {

                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbldate.Text = Convert.ToString(DateTime.Now);
            //lbltime.Text = DateTime.Now.ToLongTimeString();
        }



        private void txtbarcode_Enter(object sender, EventArgs e)
        {
            try
            {
                txtbarcode.BackColor = Color.LightYellow;
                if (Convert.ToBoolean(options.Rows[0]["showallitemlistinpos"].ToString()) == true)
                {
                    pnlallitem.Visible = true;
                    bindallitem();
                }
                else
                {
                    pnlallitem.Visible = false;
                    autoextender();
                }
            }
            catch
            {
            }
            //   this.ActiveControl = txtbarcode;
        }

        private void txtbarcode_Leave(object sender, EventArgs e)
        {
            txtbarcode.BackColor = Color.White;
            try
            {
                if (Convert.ToBoolean(options.Rows[0]["showallitemlistinpos"].ToString()) == true)
                {
                    if (lvallitem.Items[0].Selected == true)
                    {
                        pnlallitem.Visible = true;
                    }
                    else
                    {
                        pnlallitem.Visible = false;
                    }
                }
                else
                {
                    pnlallitem.Visible = false;
                }
            }
            catch
            {
            }
            //pnlallitem.Visible = false;
        }

        private void txtqty_Enter(object sender, EventArgs e)
        {
            txtqty.BackColor = Color.LightYellow;
        }

        private void txtqty_Leave(object sender, EventArgs e)
        {
            txtqty.BackColor = Color.White;
        }

        private void txttotalitem_Enter(object sender, EventArgs e)
        {
            txttotalitem.BackColor = Color.LightYellow;
        }

        private void txttotalitem_Leave(object sender, EventArgs e)
        {
            txttotalitem.BackColor = Color.White;
        }

        private void txtstotal_Enter(object sender, EventArgs e)
        {
            txtstotal.BackColor = Color.LightYellow;
        }

        private void txtstotal_Leave(object sender, EventArgs e)
        {
            txtstotal.BackColor = Color.White;
        }

        private void txtdisamt_Enter(object sender, EventArgs e)
        {
            txtdisamt.BackColor = Color.LightYellow;
        }

        private void txtdisamt_Leave(object sender, EventArgs e)
        {
            txtdisamt.BackColor = Color.White;
        }

        private void txtadddisamt_Enter(object sender, EventArgs e)
        {
            txtadddisamt.BackColor = Color.LightYellow;
        }

        private void txtadddisamt_Leave(object sender, EventArgs e)
        {
            txtadddisamt.BackColor = Color.White;
        }

        private void txttotaltax_Enter(object sender, EventArgs e)
        {
            txttotaltax.BackColor = Color.LightYellow;
        }

        private void txttotaltax_Leave(object sender, EventArgs e)
        {
            txttotaltax.BackColor = Color.White;
        }

        private void txtaddtax_Enter(object sender, EventArgs e)
        {
            txtaddtax.BackColor = Color.LightYellow;
        }

        private void txtaddtax_Leave(object sender, EventArgs e)
        {
            txtaddtax.BackColor = Color.White;
        }

        private void txtgtotal_Enter(object sender, EventArgs e)
        {
            txtgtotal.BackColor = Color.LightYellow;
        }

        private void txtgtotal_Leave(object sender, EventArgs e)
        {
            txtgtotal.BackColor = Color.White;
        }

        internal void getdata(string str, string itemname, DataGridView dgvitem)
        {
            userrights = sql.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[9]["d"].ToString() == "False")
                {
                    btndelete.Enabled = false;
                }
            }
            options = sql.getdataset("select * from options");
            lblsaletype.Text = options.Rows[0]["autosaletype"].ToString();
            batchno = str;
            DataTable dt = sql.getdataset("select ProductID from ProductMaster where isactive=1 and Product_Name='" + itemname + "'");
            DataTable dt1 = sql.getdataset("select Barcode from ProductPriceMaster where isactive=1 and ProductID='" + dt.Rows[0]["ProductID"].ToString() + "' and BatchNo='" + batchno + "'");
            //DataTable dt = sql.getdataset("select p.ProductID,pp.* from ProductMaster p inner join ProductPriceMaster pp on p.Productid=pp.Productid where   p.Product_Name='" + itemname + "'or pp.barcode='" + itemname + "' and pp.BatchNo='" + batchno + "' and p.isactive=1 and pp.isactive=1");

            txtbarcode.Text = dt1.Rows[0]["Barcode"].ToString();
            temptableenter();
            // totaltaxbox();
            //  dgvitem.Columns[0].Visible = false;
            if (btnnosale.Text != "Update")
            {
                billno();
            }

            batchno = "";
            //txtbarcode.Text = "";
            //txtbarcode.Focus();
            //GridDesign();

        }

        internal void getdata(string str, string itemname, DataTable temptable)
        {
            userrights = sql.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[9]["d"].ToString() == "False")
                {
                    btndelete.Enabled = false;
                }
            }
            dgvitem.DataSource = temptable;
            options = sql.getdataset("select * from options");
            lblsaletype.Text = options.Rows[0]["autosaletype"].ToString();
            batchno = str;
            DataTable dt = sql.getdataset("select ProductID from ProductMaster where isactive=1 and Product_Name='" + itemname + "'");
            DataTable dt1 = sql.getdataset("select Barcode from ProductPriceMaster where isactive=1 and ProductID='" + dt.Rows[0]["ProductID"].ToString() + "' and BatchNo='" + batchno + "'");
            //DataTable dt = sql.getdataset("select p.ProductID,pp.* from ProductMaster p inner join ProductPriceMaster pp on p.Productid=pp.Productid where   p.Product_Name='" + itemname + "'or pp.barcode='" + itemname + "' and pp.BatchNo='" + batchno + "' and p.isactive=1 and pp.isactive=1");

            txtbarcode.Text = dt1.Rows[0]["Barcode"].ToString();
            temptableenter();
            // totaltaxbox();
            //  dgvitem.Columns[0].Visible = false;
            if (btnnosale.Text != "Update")
            {
                billno();
            }

            batchno = "";
        }

        private void dgvitem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dgvitem.Columns["btndelete"].Index)
                {
                    if (dgvitem.RowCount == 1)
                    {
                        dgvitem.Columns.RemoveAt(e.ColumnIndex);
                    }
                    dgvitem.Rows.RemoveAt(e.RowIndex);

                    totaltaxbox();
                    txtbarcode.Focus();
                    flagmultyitem = 0;
                }
            }
            catch (Exception excp)
            {

            }
        }

        private void txtcname_Enter(object sender, EventArgs e)
        {
            txtcname.BackColor = Color.LightYellow;
        }

        private void txtcname_Leave(object sender, EventArgs e)
        {
            txtcname.BackColor = Color.White;
        }

        private void txtcity_Enter(object sender, EventArgs e)
        {
            txtcity.BackColor = Color.LightYellow;
        }

        private void txtcity_Leave(object sender, EventArgs e)
        {
            txtcity.BackColor = Color.White;
        }

        private void txtcmobile_Enter(object sender, EventArgs e)
        {
            txtcmobile.BackColor = Color.LightYellow;
        }

        private void txtcmobile_Leave(object sender, EventArgs e)
        {
            txtcmobile.BackColor = Color.White;
        }

        private void txtcmobile_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtcname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtcity.Focus();
            }
        }

        private void txtcity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtcmobile.Focus();
            }
        }

        private void txtcmobile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //txtbarcode.Focus();
                this.ActiveControl = txtbarcode;
            }
        }

        private void txtcess_Enter(object sender, EventArgs e)
        {
            txtcess.BackColor = Color.LightYellow;
        }

        private void txtcess_Leave(object sender, EventArgs e)
        {
            txtcess.BackColor = Color.White;
        }

        private void txtaddtax_TextChanged(object sender, EventArgs e)
        {

        }

        private void txttotaltax_TextChanged(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void btnlist_Enter(object sender, EventArgs e)
        {
            btnlist.UseVisualStyleBackColor = false;
            btnlist.BackColor = System.Drawing.Color.FromArgb(0, 254, 22);
            btnlist.ForeColor = System.Drawing.Color.White;
        }

        private void btnlist_Leave(object sender, EventArgs e)
        {
            btnlist.UseVisualStyleBackColor = true;
            btnlist.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnlist.ForeColor = System.Drawing.Color.White;
        }

        private void btnlist_MouseEnter(object sender, EventArgs e)
        {
            btnlist.UseVisualStyleBackColor = false;
            btnlist.BackColor = System.Drawing.Color.FromArgb(0, 254, 22);
            btnlist.ForeColor = System.Drawing.Color.White;
        }

        private void btnlist_MouseLeave(object sender, EventArgs e)
        {
            btnlist.UseVisualStyleBackColor = true;
            btnlist.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnlist.ForeColor = System.Drawing.Color.White;
        }

        private void btnnosale_Enter(object sender, EventArgs e)
        {
            btnnosale.UseVisualStyleBackColor = false;
            btnnosale.BackColor = Color.YellowGreen;
            btnnosale.ForeColor = Color.White;
        }

        private void btnnosale_Leave(object sender, EventArgs e)
        {
            btnnosale.UseVisualStyleBackColor = true;
            btnnosale.BackColor = Color.FromArgb(51, 153, 255);
            btnnosale.ForeColor = Color.White;
        }

        private void btnnosale_MouseEnter(object sender, EventArgs e)
        {
            btnnosale.UseVisualStyleBackColor = false;
            btnnosale.BackColor = Color.YellowGreen;
            btnnosale.ForeColor = Color.White;
        }

        private void btnnosale_MouseLeave(object sender, EventArgs e)
        {
            btnnosale.UseVisualStyleBackColor = true;
            btnnosale.BackColor = Color.FromArgb(51, 153, 255);
            btnnosale.ForeColor = Color.White;
        }

        private void btnitem_Enter(object sender, EventArgs e)
        {
            btnitem.UseVisualStyleBackColor = false;
            btnitem.BackColor = Color.FromArgb(236, 233, 216);
            btnitem.ForeColor = Color.White;
        }

        private void btnitem_Leave(object sender, EventArgs e)
        {
            btnitem.UseVisualStyleBackColor = true;
            btnitem.BackColor = Color.FromArgb(51, 153, 255);
            btnitem.ForeColor = Color.White;
        }

        private void btnitem_MouseEnter(object sender, EventArgs e)
        {
            btnitem.UseVisualStyleBackColor = false;
            btnitem.BackColor = Color.FromArgb(236, 233, 216);
            btnitem.ForeColor = Color.White;
        }

        private void btnitem_MouseLeave(object sender, EventArgs e)
        {
            btnitem.UseVisualStyleBackColor = true;
            btnitem.BackColor = Color.FromArgb(51, 153, 255);
            btnitem.ForeColor = Color.White;
        }

        private void btncustomer_Enter(object sender, EventArgs e)
        {
            btncustomer.UseVisualStyleBackColor = false;
            btncustomer.BackColor = Color.FromArgb(20, 209, 82);
            btncustomer.ForeColor = Color.White;
        }

        private void btncustomer_Leave(object sender, EventArgs e)
        {
            btncustomer.UseVisualStyleBackColor = true;
            btncustomer.BackColor = Color.FromArgb(51, 153, 255);
            btncustomer.ForeColor = Color.White;
        }

        private void btncustomer_MouseEnter(object sender, EventArgs e)
        {
            btncustomer.UseVisualStyleBackColor = false;
            btncustomer.BackColor = Color.FromArgb(20, 209, 82);
            btncustomer.ForeColor = Color.White;
        }

        private void btncustomer_MouseLeave(object sender, EventArgs e)
        {
            btncustomer.UseVisualStyleBackColor = true;
            btncustomer.BackColor = Color.FromArgb(51, 153, 255);
            btncustomer.ForeColor = Color.White;
        }

        private void btnreturn_Enter(object sender, EventArgs e)
        {
            btnreturn.UseVisualStyleBackColor = false;
            btnreturn.BackColor = Color.FromArgb(248, 152, 94);
            btnreturn.ForeColor = Color.White;
        }

        private void btnreturn_Leave(object sender, EventArgs e)
        {
            btnreturn.UseVisualStyleBackColor = true;
            btnreturn.BackColor = Color.FromArgb(51, 153, 255);
            btnreturn.ForeColor = Color.White;
        }

        private void btnreturn_MouseEnter(object sender, EventArgs e)
        {
            btnreturn.UseVisualStyleBackColor = false;
            btnreturn.BackColor = Color.FromArgb(248, 152, 94);
            btnreturn.ForeColor = Color.White;
        }

        private void btnreturn_MouseLeave(object sender, EventArgs e)
        {
            btnreturn.UseVisualStyleBackColor = true;
            btnreturn.BackColor = Color.FromArgb(51, 153, 255);
            btnreturn.ForeColor = Color.White;
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (id != "" && id != null)
                {
                    DialogResult dr1 = MessageBox.Show("Do you want to Delete Bill?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr1 == DialogResult.Yes)
                    {
                        this.Enabled = false;
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                        con.Open();
                        sql.execute("Update BillPOSProductMaster set isactive=0 where BillId='" + id + "'");
                        sql.execute("Update BillPOSMaster set isactive=0 where BillId='" + id + "'");
                        MessageBox.Show("Delete Successfully");
                        clearall();
                        master.RemoveCurrentTab();
                    }
                }
                else
                {
                    MessageBox.Show("Select Bill");
                    this.ActiveControl = txtbarcode;
                }
            }
            catch
            {
            }
            finally
            {
                this.Enabled = true;
            }
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
        int flagforagent = 0;
        private void lvallitem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (options.Rows[0]["requiragentnameinpos"].ToString() == "Ask For Agent After Item Entry")
                {
                    pnlagent.Visible = true;
                    cmbagentname.Focus();
                }
                else if (options.Rows[0]["requiragentnameinpos"].ToString() == "Ask For Agent Bill Wise")
                {
                    if (flagforagent == 0)
                    {
                        pnlagent.Visible = true;
                        cmbagentname.Focus();
                        flagforagent = 1;
                    }
                    else
                    {
                        String str = lvallitem.Items[lvallitem.FocusedItem.Index].SubItems[0].Text;
                        txtbarcode.Text = str;
                        txtbarcode.Focus();
                        enteritem();
                    }
                }
                else
                {
                    String str = lvallitem.Items[lvallitem.FocusedItem.Index].SubItems[0].Text;
                    txtbarcode.Text = str;
                    txtbarcode.Focus();
                    enteritem();
                }
            }
        }

        private void lvallitem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (options.Rows[0]["requiragentnameinpos"].ToString() == "Ask For Agent After Item Entry")
                {
                    pnlagent.Visible = true;
                    cmbagentname.Focus();
                }
                else if (options.Rows[0]["requiragentnameinpos"].ToString() == "Ask For Agent Bill Wise")
                {
                    if (flagforagent == 0)
                    {
                        pnlagent.Visible = true;
                        cmbagentname.Focus();
                        flagforagent = 1;
                    }
                    else
                    {
                        String str = lvallitem.Items[lvallitem.FocusedItem.Index].SubItems[0].Text;
                        txtbarcode.Text = str;
                        txtbarcode.Focus();
                        enteritem();
                    }
                }
                else
                {
                    String str = lvallitem.Items[lvallitem.FocusedItem.Index].SubItems[0].Text;
                    txtbarcode.Text = str;
                    txtbarcode.Focus();
                    enteritem();
                }
            }
            catch
            {
            }
        }

        private void lvallitem_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (options.Rows[0]["requiragentnameinpos"].ToString() == "Ask For Agent After Item Entry")
                {
                    pnlagent.Visible = true;
                    cmbagentname.Focus();
                }
                else if (options.Rows[0]["requiragentnameinpos"].ToString() == "Ask For Agent Bill Wise")
                {
                    if (flagforagent == 0)
                    {
                        pnlagent.Visible = true;
                        cmbagentname.Focus();
                        flagforagent = 1;
                    }
                    else
                    {
                        String str = lvallitem.Items[lvallitem.FocusedItem.Index].SubItems[0].Text;
                        txtbarcode.Text = str;
                        txtbarcode.Focus();
                        enteritem();
                    }
                }
                else
                {
                    String str = lvallitem.Items[lvallitem.FocusedItem.Index].SubItems[0].Text;
                    txtbarcode.Text = str;
                    txtbarcode.Focus();
                    enteritem();
                }
            }
            catch
            {
            }
        }

        private void cmbagentname_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbagentname.SelectedIndex = 0;
                cmbagentname.DroppedDown = true;
            }
            catch
            {
            }
        }
        public static string s;
        private void cmbagentname_Leave(object sender, EventArgs e)
        {
            try
            {
                cmbagentname.Text = s;
            }
            catch
            {
            }
        }
        public static string activecontroal;
        private void cmbagentname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true; // This will eliminate the beeping
                    bool inList = false;
                    for (int i = 0; i < cmbagentname.Items.Count; i++)
                    {
                        s = cmbagentname.GetItemText(cmbagentname.Items[i]);
                        if (s == cmbagentname.Text)
                        {
                            inList = true;
                            cmbagentname.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        cmbagentname.Text = "";
                    }
                    String str = lvallitem.Items[lvallitem.FocusedItem.Index].SubItems[0].Text;
                    txtbarcode.Text = str;
                    txtbarcode.Focus();
                    enteritem();
                }
                catch
                {
                    txtbarcode.Focus();
                }
            }
            if (e.KeyCode == Keys.F3)
            {
                var privouscontroal = cmbagentname;
                activecontroal = privouscontroal.Name;
                Accountentry client = new Accountentry(this, master, tabControl, activecontroal);

                client.Passed(1);
                //   client.Show();
                master.AddNewTab(client);
            }
            if (e.KeyCode == Keys.F2)
            {
                var privouscontroal = cmbagentname;
                activecontroal = privouscontroal.Name;
                string iid = cmbagentname.SelectedValue.ToString();

                Accountentry client = new Accountentry(this, master, tabControl, activecontroal);
                client.Update(1, iid);
                client.Passed(1);
                //  client.Show();
                master.AddNewTab(client);
            }
        }


    }
}
