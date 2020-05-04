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
using System.Drawing.Drawing2D;
using CrystalDecisions.Shared;

namespace RamdevSales
{
    public partial class POS : Form
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        private POSBillList posBillList;
        private TextBox textBox = null;
        public static string iid = "";

        public POS()
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent(); 
            SqlCommand cmd1 = new SqlCommand("select distinct(GroupName) from GroupMaster", con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            bindbuttons(dt1);
            bindcalcbtns();
            this.txtDiscount.GotFocus += new EventHandler(GetF);
            this.txtSerCharge.GotFocus += new EventHandler(GetF);
            this.txtPackCharge.GotFocus += new EventHandler(GetF);
            this.txtCashTendered.GotFocus += new EventHandler(GetF);
            this.dataGridView1.Focus();
            dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
        }

        private void GetF(object sender, EventArgs e)
        {
            // Keeps you selecting textbox object reference.
            textBox = sender as TextBox;

        }

        public POS(POSBillList posBillList)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this.posBillList = posBillList;
           // getcon();
            SqlCommand cmd1 = new SqlCommand("select distinct(GroupName) from GroupMaster", con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            bindbuttons(dt1);
            bindcalcbtns();
            this.txtDiscount.GotFocus += new EventHandler(GetF);
            this.txtSerCharge.GotFocus += new EventHandler(GetF);
            this.txtPackCharge.GotFocus += new EventHandler(GetF);
            this.txtCashTendered.GotFocus += new EventHandler(GetF);            
        }

        public POS(Master master, TabControl tabControl)
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            SqlCommand cmd1 = new SqlCommand("select distinct(GroupName) from GroupMaster", con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            bindbuttons(dt1);
            bindcalcbtns();
            this.txtDiscount.GotFocus += new EventHandler(GetF);
            this.txtSerCharge.GotFocus += new EventHandler(GetF);
            this.txtPackCharge.GotFocus += new EventHandler(GetF);
            this.txtCashTendered.GotFocus += new EventHandler(GetF);
            this.dataGridView1.Focus();
            dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
            // TODO: Complete member initialization
            this.master = master;
            this.tabControl = tabControl;
        }

        private void POS_Load(object sender, EventArgs e)
        {
            try
            {
                if (cnt == 0)
                {
                    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    ShowInTaskbar = true;
                    loadpage();

                    DataTable dt3 = new DataTable();
                    SqlCommand cmd2 = new SqlCommand("select a, u, d, v, p, mId, uId, cId from UserRights where mId=10 and cId= " + Master.companyId + " and isActive=1", con);
                    //SqlCommand cmd2 = new SqlCommand("select a, u, d, v, p, mId, uId, cId from UserRights where mId=10 and uId='" + UserLogin.id + "' and cId= " + Master.companyId + " and isActive=1", con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    
                    sda2.Fill(dt3);
                   
                    if (dt3.Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(dt3.Rows[0][0]) == false)
                        {
                            if (btnPay.Text == "Pay")
                            {
                                btnPay.Visible = false;
                            }

                        }
                        if (Convert.ToBoolean(dt3.Rows[0][1]) == false)
                        {
                            if (btnPay.Text == "Update")
                            {
                                btnPay.Visible = false;
                            }
                        }
                        if (Convert.ToBoolean(dt3.Rows[0][2]) == false)
                        {
                            btnDelete.Visible = false;
                        }

                    }
                }

                
            }
            catch
            {

            }
            
        }

        private void loadpage()
        {
            con.Open();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(this.Height, this.Width);
            this.Location = new Point(0, 0);
            txtRunDate.Text = DateTime.Now.ToShortDateString();
            autoReaderBind();
            con.Close();
        }

        public void autoReaderBind()
        {
            try
            {
                //getcon();
                AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();


                SqlDataReader dReader;
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = con;
                cmd1.CommandType = CommandType.Text;

                //start string

                String qry = "select ProductMaster.Product_Name from ProductMaster";
                //  con.Open();
                int count = 0;

                con.Close();
                qry = qry + " order by ProductMaster.Product_Name";

                if (count == 0)
                {
                    //end string
                    cmd1.CommandText = qry;


                    con.Open();
                    dReader = cmd1.ExecuteReader();

                    if (dReader.HasRows == true)
                    {
                        while (dReader.Read())
                            namesCollection.Add(dReader["Product_Name"].ToString());

                    }
                    else
                    {
                        MessageBox.Show("Data not found");
                    }
                    dReader.Close();

                    txtItemName.AutoCompleteMode = AutoCompleteMode.Suggest;
                    txtItemName.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    txtItemName.AutoCompleteCustomSource = namesCollection;
                }
                else
                {

                    //end string
                    cmd1.CommandText = qry;


                    //    con.Open();
                    dReader = cmd1.ExecuteReader();

                    if (dReader.HasRows == true)
                    {
                        while (dReader.Read())
                            namesCollection.Add(dReader["Product_Name"].ToString());

                    }
                    else
                    {
                        MessageBox.Show("Data not found");
                    }
                    dReader.Close();

                    txtItemName.AutoCompleteMode = AutoCompleteMode.Suggest;
                    txtItemName.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    txtItemName.AutoCompleteCustomSource = namesCollection;
                }
            }
            catch
            {
            }
            finally
            {
                con.Close();
            }
        }


        protected void bindGridViewColumn()
        {
            dataGridView1.Columns[0].Name = "Items";
            dataGridView1.Columns[1].Name = "Qty";
            dataGridView1.Columns[1].Width = 30;
            
            dataGridView1.Columns[2].Name = "Rate";
            dataGridView1.Columns[2].Width = 40;
            dataGridView1.Columns[3].Name = "Amount";
            dataGridView1.Columns[3].Width = 50;
            dataGridView1.Columns[4].Name = "vat";
            dataGridView1.Columns[4].Width = 0;
            dataGridView1.Columns[5].Name = "addvat";
            dataGridView1.Columns[5].Width = 0;
        }

        private void getbillId()
        {
            try
            {
               // getcon();
                con.Open();
                SqlCommand cmd2 = new SqlCommand("select max(BillId) from BillPOSMaster where isactive='1' and CompanyId ="+Master.companyId+"", con);
                String str = cmd2.ExecuteScalar().ToString();
                int id, count = 0;
                if (str == "")
                {

                    id = 1;
                    count = 1;
                }
                else
                {
                    id = Convert.ToInt32(str) + 1;
                    count = Convert.ToInt32(str) + 1;
                }
                txthbillId.Text = count.ToString();
            }
            catch
            {
                con.Close();
            }
            finally
            {
                con.Close();
            }
        }
        
        List<Button> calcbtns = new List<Button>();
        private void bindcalcbtns()
        {
            int l = 0;
            for (int k = 1; k <= 12; k++)
            {
                Button calcButton = new Button();
                calcbtns.Add(calcButton);
                if (k == 10)
                {
                    calcButton.Name = "btn0";
                    calcButton.Text = "0";
                }
                else if (k == 11)
                {
                    calcButton.Name = "btndot";
                    calcButton.Text = ".";
                }
                else if (k == 12)
                {
                    calcButton.Name = "btnClr";
                    calcButton.Text = "Clear";
                }
                else
                {
                    calcButton.Name = "btn" + k;
                    calcButton.Text = k.ToString();
                }
                calcButton.Location = new Point((l * 10) + 3, 10);
                l = l + 10;
                calcButton.Width = 60;
                calcButton.Height = 60;
                calcButton.BackColor = Color.White;
                calcButton.ForeColor = Color.Blue;
                calcButton.Font = new Font("Verdana", 10, FontStyle.Bold);
                calcButton.Font.Bold.Equals(true);
                calcButton.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
                calcButton.Click += new EventHandler(calcbutton_Click);
                flowLayoutPanel2.Controls.Add(calcButton);
            }
        }

        protected void calcbutton_Click(object sender, EventArgs e)
        {
            try
            {
                Button calcbutton = sender as Button;
                if (textBox != null && calcbutton.Text!="Clear")

                    textBox.SelectedText += calcbutton.Text;
                else if (calcbutton.Text == "Clear")
                {
                    textBox.Text = "";
                }
                   for(int i =0;i<dataGridView1.Rows.Count;i++)
                   {
                       // if (dataGridView1.Focused != null)
                    //dataGridView1.Rows[i] += calcbutton.Text;
                        }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            if (dataGridView1.CurrentCell.ColumnIndex == 1) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.GotFocus += new EventHandler(GetF);
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        List<Button> buttons = new List<Button>();
        private void bindbuttons(DataTable dt1)
        {
            int j = 0;

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                Button newButton = new Button();
                buttons.Add(newButton);
                newButton.Name = "btn" + dt1.Rows[i][0].ToString();
                newButton.Text = dt1.Rows[i][0].ToString();

                newButton.Location = new Point((j * 10) + 5, 31);
                j = j + 10;
              

                newButton.Width = 100;
                newButton.Height = 110;
                newButton.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                newButton.BackColor = Color.RoyalBlue;
                newButton.Font = new Font("Calibri", 10, FontStyle.Bold);
                newButton.ForeColor = Color.White;
               

                newButton.TabIndex = 1;
                newButton.TabStop = true;
                newButton.Click += new EventHandler(button_Click);
                flowLayoutPanel1.Controls.Add(newButton);
                flag = 0;
            }
            
        }
      
        int flag;
        protected void button_Click(object sender, EventArgs e)
        {
            try
            {
               // getcon();
                if (flag == 0)
                {

                    Button button = sender as Button;
                    SqlCommand cmd1 = new SqlCommand("select distinct(Product_Name) from ProductMaster where GroupName=(select id from groupmaster where groupname='" + button.Text + "')", con);
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    sda1.Fill(dt1);
                    flag = 1;
                    rebindbuttons(dt1);
                }
                else
                {
                    int vari = 0;
                    Button button = sender as Button;

                    // SqlCommand cmd1 = new SqlCommand("Select pp.BasicPrice as amount,pp.vat,pp.addvat from ProductMaster p inner join ProductPriceMaster pp on p.ProductID = pp.Productid where p.ProductID = pp.Productid and p.Product_Name='" + button.Text + "'", con);
                    SqlCommand cmd1 = new SqlCommand("Select p.Product_Name,p.Convfactor,(pp.saleprice * 100)/(100+ (pp.vat+pp.addvat)) as rate,p.Convfactor*((pp.saleprice * 100)/(100+ (pp.vat+pp.addvat))) as amount,pp.vat,pp.addvat from ProductMaster p inner join ProductPriceMaster pp on p.ProductID = pp.Productid where p.ProductID = pp.Productid and p.Product_Name='" + button.Text + "'", con);
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    sda1.Fill(dt);

                    if (dataGridView1.Rows.Count == 0 && btnPay.Text =="Pay" || btnPay.Text == "Save")
                    {
                        dataGridView1.AutoGenerateColumns = false;

                        dataGridView1.ColumnCount = 6;

                    }

                    //Add Columns


                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {

                        if (dataGridView1.Rows[i].Cells[0].Value.ToString() == dt.Rows[0][0].ToString())
                        {
                            int qty = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value.ToString());
                            qty += 1;
                            Double rate = Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value.ToString()),2);
                            Double amount;
                            amount = qty * rate;
                            
                            dataGridView1.Rows[i].Cells[1].Value = qty;
                            dataGridView1.Rows[i].Cells[3].Value = amount;
                            vari = 1;
                        }
                    }

                    if (vari == 0)
                    {
                      //  bindGridViewColumn();


                        string t0, t1, t2, t3, t4, t5;
                        if (dt.Rows[0][0].ToString() == "")
                        {
                            t0 = "0";
                        }
                        else
                        {
                            t0 = dt.Rows[0][0].ToString();
                        }
                        if (dt.Rows[0][1].ToString() == "")
                        {

                            t1 = "0";
                        }
                        else
                        {
                            t1 = dt.Rows[0][1].ToString();
                        }
                        if (dt.Rows[0][2].ToString() == "")
                        {

                            t2 = "0";
                        }
                        else
                        {
                            t2 = dt.Rows[0][2].ToString();
                            t2 = Math.Round(Convert.ToDecimal(t2), 2).ToString("N2");
                        }
                        if (dt.Rows[0][3].ToString() == "")
                        {

                            t3 = "0";
                        }
                        else
                        {
                            t3 = dt.Rows[0][3].ToString();
                            t3 = Math.Round(Convert.ToDecimal(t3), 2).ToString("N2");
                        }
                        if (dt.Rows[0][4].ToString() == "")
                        {
                            t4 = "0";
                        }
                        else
                        {
                            t4 = dt.Rows[0][4].ToString();
                        }
                        if (dt.Rows[0][5].ToString() == "")
                        {

                            t5 = "0";

                        }
                        else
                        {
                            t5 = dt.Rows[0][5].ToString();
                        }
                        string[] row = new string[] { t0, t1, t2, t3, t4, t5 };

                        if (btnPay.Text == "Update")
                        {
                            DataTable dt4 = dataGridView1.DataSource as DataTable;
                            dt4.Rows.Add(row);
                            dataGridView1.DataSource = dt4;
                        }
                        else
                        {
                            bindGridViewColumn();
                            dataGridView1.Rows.Add(row);
                        }
                    }
                }

                foreach (DataGridViewColumn dc in dataGridView1.Columns)
                {
                    if (dc.Index.Equals(1))
                    {
                        dc.ReadOnly = false;
                    }
                    else
                    {
                        dc.ReadOnly = true;
                    }
                }

                total();

            }
            catch
            {

            }

        }

        private void flag1()
        {
            int vari = 0;
           // Button button = sender as Button;

            // SqlCommand cmd1 = new SqlCommand("Select pp.BasicPrice as amount,pp.vat,pp.addvat from ProductMaster p inner join ProductPriceMaster pp on p.ProductID = pp.Productid where p.ProductID = pp.Productid and p.Product_Name='" + button.Text + "'", con);
            SqlCommand cmd1 = new SqlCommand("Select p.Product_Name,p.Convfactor,(pp.saleprice * 100)/(100+ (pp.vat+pp.addvat)) as rate,p.Convfactor*((pp.saleprice * 100)/(100+ (pp.vat+pp.addvat))) as amount,pp.vat,pp.addvat from ProductMaster p inner join ProductPriceMaster pp on p.ProductID = pp.Productid where p.ProductID = pp.Productid and p.Product_Name='" + txtItemName.Text + "'", con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            sda1.Fill(dt);

            if (dataGridView1.Rows.Count == 0 && btnPay.Text == "Pay" || btnPay.Text == "Save")
            {
                dataGridView1.AutoGenerateColumns = false;

                dataGridView1.ColumnCount = 6;

            }

            //Add Columns


            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                if (dataGridView1.Rows[i].Cells[0].Value.ToString() == dt.Rows[0][0].ToString())
                {
                    int qty = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value.ToString());
                    qty += 1;
                    Double rate = Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value.ToString()), 2);
                    Double amount;
                    amount = qty * rate;

                    dataGridView1.Rows[i].Cells[1].Value = qty;
                    dataGridView1.Rows[i].Cells[3].Value = amount;
                    vari = 1;
                }
            }

            if (vari == 0)
            {
                //  bindGridViewColumn();


                string t0, t1, t2, t3, t4, t5;
                if (dt.Rows[0][0].ToString() == "")
                {
                    t0 = "0";
                }
                else
                {
                    t0 = dt.Rows[0][0].ToString();
                }
                if (dt.Rows[0][1].ToString() == "")
                {

                    t1 = "0";
                }
                else
                {
                    t1 = dt.Rows[0][1].ToString();
                }
                if (dt.Rows[0][2].ToString() == "")
                {

                    t2 = "0";
                }
                else
                {
                    t2 = dt.Rows[0][2].ToString();
                    t2 = Math.Round(Convert.ToDecimal(t2), 2).ToString("N2");
                }
                if (dt.Rows[0][3].ToString() == "")
                {

                    t3 = "0";
                }
                else
                {
                    t3 = dt.Rows[0][3].ToString();
                    t3 = Math.Round(Convert.ToDecimal(t3), 2).ToString("N2");
                }
                if (dt.Rows[0][4].ToString() == "")
                {
                    t4 = "0";
                }
                else
                {
                    t4 = dt.Rows[0][4].ToString();
                }
                if (dt.Rows[0][5].ToString() == "")
                {

                    t5 = "0";

                }
                else
                {
                    t5 = dt.Rows[0][5].ToString();
                }
                string[] row = new string[] { t0, t1, t2, t3, t4, t5 };

                if (btnPay.Text == "Update")
                {
                    DataTable dt4 = dataGridView1.DataSource as DataTable;
                    dt4.Rows.Add(row);
                    dataGridView1.DataSource = dt4;
                }
                else
                {
                    bindGridViewColumn();
                    dataGridView1.Rows.Add(row);
                }
            }
        }

        Double totalqty = 0;
        private void total()
        {
            try
            {
                Double sum = 0, discount = 0, vat = 0, addvat = 0, srcharge = 0, pakcharge = 0, cashTendered = 0;
                if (txtDiscount.Text != "")
                {
                    discount = Convert.ToDouble(txtDiscount.Text);
                }


                if (txtVat.Text == "")
                {
                    vat = 0;

                }

                if (txtAddVat.Text == "")
                {
                    addvat = 0;
                }

                if (txtSerCharge.Text != "")
                {
                    srcharge = Convert.ToDouble(txtSerCharge.Text);
                }

                if (txtPackCharge.Text != "")
                {
                    pakcharge = Convert.ToDouble(txtPackCharge.Text);
                }

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {

                    sum += Double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                    vat += (Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value.ToString())) * (Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value.ToString()) / 100);
                    addvat += (Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value.ToString())) * (Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value.ToString()) / 100);
                }
                txtTA.Text = Math.Round(sum, 2).ToString();
                txtVat.Text = Math.Round(vat, 2).ToString();
                txtAddVat.Text = Math.Round(addvat, 2).ToString();

                double nettotal = (sum - discount + vat + addvat + srcharge + pakcharge);
                lblnettotal.Text = Math.Round(nettotal, 0).ToString("N2");
                lblroundof.Text = Math.Round((Math.Round(nettotal, 0) - nettotal), 2).ToString();
                if (txtCashTendered.Text != "")
                {
                    cashTendered = Convert.ToDouble(txtCashTendered.Text);

                    txtChange.Text = Math.Round((Math.Round(cashTendered, 0) - nettotal), 0).ToString();
                }
                else
                {
                    cashTendered = 0;
                }
               
            }
            catch
            {
            }
        }

        private void rebindbuttons(DataTable dt1)
        {
            //for clear data
            #region
            foreach (Button button in buttons)
            {
                Controls.Remove(button);
                button.Dispose();
            }

            #endregion

            //for rebind data
            #region
            int j = 0;
            for (int i = 0; i < dt1.Rows.Count; i++)
            {

                Button newButton = new Button();
                buttons.Add(newButton);
                newButton.Name = "btn" + dt1.Rows[i][0].ToString();
                newButton.Text = dt1.Rows[i][0].ToString();

                newButton.Location = new Point((j * 10) + 5, 31);
                j = j + 10;
                newButton.Width = 100;
                newButton.Height = 110;
                if (color == 0)
                {
                    newButton.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                    newButton.BackColor = Color.White;
                    newButton.Font = new Font("Calibri", 10, FontStyle.Bold);
                    newButton.ForeColor = Color.RoyalBlue;
                    newButton.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                }
                else
                {
                    newButton.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                    newButton.BackColor = Color.RoyalBlue;
                    newButton.Font = new Font("Calibri", 10, FontStyle.Bold);
                    newButton.ForeColor = Color.White;
                    newButton.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                   
                }
                newButton.TabIndex = 1;
                newButton.TabStop = true;
                newButton.Click += new EventHandler(button_Click);
                flowLayoutPanel1.Controls.Add(newButton);

           
            }
            color = 0;
            #endregion
        }

        int color;
        private void btnback_Click(object sender, EventArgs e)
        {
            SqlCommand cmd1 = new SqlCommand("select distinct(GroupName) from GroupMaster ", con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            color = 1;
            sda1.Fill(dt1);
            rebindbuttons(dt1);
            flag = 0;
        }

        private void dataGridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            total();
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            
            
            total();

        }

        private void txtSerCharge_TextChanged(object sender, EventArgs e)
        {
            total();

        }

        private void txtPackCharge_TextChanged(object sender, EventArgs e)
        {

            total();

        }

        private void clearall()
        {
            dataGridView1.Columns.Clear();
            txtTA.Text = "";
            txtVat.Text = "";
            txtAddVat.Text = "";
            txtDiscount.Text = "";
            txtSerCharge.Text = "";
            txtPackCharge.Text = "";
            lblroundof.Text = "";
            lblnettotal.Text = "";
            txtCashTendered.Text = "";
            txtChange.Text = "";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            clearall();
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            btnsubmit();
            SqlCommand cmd1 = new SqlCommand("select distinct(GroupName) from GroupMaster ", con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            color = 1;
            sda1.Fill(dt1);
            rebindbuttons(dt1);
            flag = 0;
        }

        private void btnsubmit()
        {
            try
            {
               // getcon();
                
                
                    if (btnPay.Text == "Update" || btnPay.Text == "Save")
                    {
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                        con.Open();
                        total();
                        checknull();
                        SqlCommand cmd3 = new SqlCommand("select max(BillId) from BillPOSMaster where isactive='1'", con);
                        String str = cmd3.ExecuteScalar().ToString();
                        txthbillId.Text = str;
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            if (dataGridView1.Rows[i].Cells[1].Value != null)
                            {
                                totalqty += Double.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                            }
                            else
                            {
                                totalqty = 0;
                            }
                        }
                        if (txtTA.Text == "0" || lblnettotal.Text == "0.00")
                        {
                            SqlCommand cmd2 = new SqlCommand("delete from BillPOSProductMaster where BillId='" + txthbillId.Text + "'", con);
                            cmd2.ExecuteNonQuery();
                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                SqlCommand cmd1 = new SqlCommand("INSERT INTO BillPOSProductMaster ([BillId],[BillRunDate],[ItemName],[Qty],[Rate],[Amount],[Total],[Vat],[AddVat],[Discount],[SerCharge],[PackCharge],[RoundOf],[NetTotal],[CashTendered],[Change],[isactive])VALUES('" + txthbillId.Text + "','" + Convert.ToDateTime(txtRunDate.Text).ToString("MM-dd-yyyy") + "','" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "'," + dataGridView1.Rows[i].Cells[1].Value.ToString().Replace(",", "") + "," + dataGridView1.Rows[i].Cells[2].Value.ToString().Replace(",", "") + "," + dataGridView1.Rows[i].Cells[3].Value.ToString().Replace(",", "") + "," + txtTA.Text.Replace(",", "") + "," + txtVat.Text.Replace(",", "") + "," + txtAddVat.Text.Replace(",", "") + "," + txtDiscount.Text.Replace(",", "") + "," + txtSerCharge.Text.Replace(",", "") + "," + txtPackCharge.Text.Replace(",", "") + ",'" + lblroundof.Text.Replace(",", "") + "','" + lblnettotal.Text.Replace(",", "") + "'," + txtCashTendered.Text.Replace(",", "") + "," + txtChange.Text.Replace(",", "") + ",1)", con);
                                cmd1.ExecuteNonQuery();
                            }
                            SqlCommand cmd = new SqlCommand("UPDATE [dbo].[BillPOSMaster] SET [BillId] = '" + txthbillId.Text + "',[BillDate] = '" + Convert.ToDateTime(txtRunDate.Text).ToString("MM-dd-yyyy") + "',[Terms] = 'Cash' ,[totalqty] = " + totalqty + ",[totalbasic] = " + txtTA.Text.Replace(",", "") + ",[totaltax] =" + txtVat.Text.Replace(",", "") + " ,[totalnet] = " + Math.Round(Convert.ToDouble(lblnettotal.Text), 2).ToString("########.00").Replace(",", "") + ",[isactive]=1 where BillId='" + txthbillId.Text + "' and CompanyId=" + Master.companyId + "", con);
                            cmd.ExecuteNonQuery();

                            DialogResult dr1 = MessageBox.Show("Do you want to Print Bill?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dr1 == DialogResult.Yes)
                            {
                                SqlCommand cmd6 = new SqlCommand("select * from BillPOSProductMaster where BillId='" + txthbillId.Text + "'", con);
                                SqlDataAdapter sda6 = new SqlDataAdapter(cmd6);
                                DataTable dt1 = new DataTable();
                                sda6.Fill(dt1);
                                cmd = new SqlCommand("delete from printing", con);
                                cmd.ExecuteNonQuery();
                                int j = 1;
                                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                                {
                                    string qry = "INSERT INTO [dbo].[Printing]([T1],[T2],[T3],[T4],[T5],[T6],[T7],[T8],[T9],[T10],[T11],[T12],[T13],[T14],[T15],[T16],[T17],[T18],[T19])VALUES";
                                    qry += "('" + j++ + "','" + txthbillId.Text + "','" + txtRunDate.Text + "','" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[1].Value.ToString().Replace(",", "") + "','" + dataGridView1.Rows[i].Cells[2].Value.ToString().Replace(",", "") + "','" + dataGridView1.Rows[i].Cells[3].Value.ToString().Replace(",", "") + "','" + dataGridView1.Rows[i].Cells[4].Value.ToString().Replace(",", "") + "','" + dataGridView1.Rows[i].Cells[5].Value.ToString().Replace(",", "") + "','" + txtTA.Text.Replace(",", "") + "','" + txtDiscount.Text.Replace(",", "") + "','" + txtSerCharge.Text.Replace(",", "") + "','" + txtPackCharge.Text.Replace(",", "") + "','" + lblroundof.Text.Replace(",", "") + "','" + lblnettotal.Text.Replace(",", "") + "','" + txtCashTendered.Text.Replace(",", "") + "','" + txtChange.Text.Replace(",", "") + "',1," + Master.companyId + ")";
                                    cmd = new SqlCommand(qry, con);
                                    cmd.ExecuteNonQuery();
                                }
                                SqlCommand cmd7 = new SqlCommand("select CompanyName,Address,Phone,VATNo from Company where CompanyID='" + Master.companyId + "' and isActive=1", con);
                                SqlDataAdapter sda7 = new SqlDataAdapter(cmd7);
                                DataTable dt5 = new DataTable();
                                sda7.Fill(dt5);
                                string query = "update printing set [T20]='" + dt5.Rows[0][0].ToString() + "',[T21]='" + dt5.Rows[0][1].ToString() + "',[T22]=" + dt5.Rows[0][2].ToString() + ",[T23]='" + dt5.Rows[0][3].ToString() + "' where [T2]='" + txthbillId.Text + "'";
                                cmd = new SqlCommand(query, con);
                                cmd.ExecuteNonQuery();
                                clearall();

                                BillPOSReport frm = new BillPOSReport();

                                frm.StartPosition = FormStartPosition.CenterScreen;
                                frm.Show();
                                frm.Close();
                              //  PrintToPrinter();
                            }
                            btnPay.Text = "Save";
                        }
                        else
                        {
                            MessageBox.Show("Please select at least one item.");
                        }
                        
                    }
                    else
                   {
                        getbillId();
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                        con.Open();
                        total();
                        checknull();
                       // con.Open();
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            if (dataGridView1.Rows[i].Cells[1].Value != null)
                            {
                                totalqty += Double.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                            }
                            else
                            {
                                totalqty = 0;
                            }
                        }
                         if (txtTA.Text != "0" || lblnettotal.Text != "0.00")
                        {
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {

                            SqlCommand cmd1 = new SqlCommand("INSERT INTO BillPOSProductMaster ([BillId],[BillRunDate],[ItemName],[Qty],[Rate],[Amount],[Total],[Vat],[AddVat],[Discount],[SerCharge],[PackCharge],[RoundOf],[NetTotal],[CashTendered],[Change],[isactive])VALUES('" + txthbillId.Text + "','" + Convert.ToDateTime(txtRunDate.Text).ToString("MM-dd-yyyy") + "','" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "'," + dataGridView1.Rows[i].Cells[1].Value.ToString().Replace(",", "") + "," + dataGridView1.Rows[i].Cells[2].Value.ToString().Replace(",", "") + "," + dataGridView1.Rows[i].Cells[3].Value.ToString().Replace(",", "") + "," + txtTA.Text.Replace(",", "") + "," + txtVat.Text.Replace(",", "") + "," + txtAddVat.Text.Replace(",", "") + "," + txtDiscount.Text.Replace(",", "") + "," + txtSerCharge.Text.Replace(",", "") + "," + txtPackCharge.Text.Replace(",", "") + ",'" + lblroundof.Text.Replace(",", "") + "','" + lblnettotal.Text.Replace(",", "") + "'," + txtCashTendered.Text.Replace(",", "") + "," + txtChange.Text.Replace(",", "") + ",1)", con);
                            cmd1.ExecuteNonQuery();

                        }
                        Double totalTax = Convert.ToDouble(txtVat.Text) + Convert.ToDouble(txtAddVat.Text);
                        SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[BillPOSMaster]([BillId],[BillDate],[Terms],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[CompanyId])VALUES('" + txthbillId.Text + "','" + Convert.ToDateTime(txtRunDate.Text).ToString("MM-dd-yyyy") + "',' Cash '," + dataGridView1.Rows.Count + "," + totalqty + "," + txtTA.Text.Replace(",", "") + "," + totalTax + "," + Math.Round(Convert.ToDouble(lblnettotal.Text), 2).ToString("########.00").Replace(",", "") + ",1," + Master.companyId + ")", con);

                        cmd.ExecuteNonQuery();
                       
                        DialogResult dr1 = MessageBox.Show("Do you want to Print Bill?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr1 == DialogResult.Yes)
                        {
                            SqlCommand cmd6 = new SqlCommand("select * from BillPOSProductMaster where BillID='" + txthbillId.Text + "'", con);
                            SqlDataAdapter sda6 = new SqlDataAdapter(cmd6);
                            DataTable dt1 = new DataTable();
                            sda6.Fill(dt1);
                            cmd = new SqlCommand("delete from printing", con);
                            cmd.ExecuteNonQuery();
                            int j = 1;
                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                string qry = "INSERT INTO [dbo].[Printing]([T1],[T2],[T3],[T4],[T5],[T6],[T7],[T8],[T9],[T10],[T11],[T12],[T13],[T14],[T15],[T16],[T17],[T18],[T19])VALUES";
                                qry += "('" + j++ + "','" + txthbillId.Text + "','" + txtRunDate.Text + "','" + dataGridView1.Rows[i].Cells[0].Value.ToString().Replace(",", "") + "','" + dataGridView1.Rows[i].Cells[1].Value.ToString().Replace(",", "") + "','" + dataGridView1.Rows[i].Cells[2].Value.ToString().Replace(",", "") + "','" + dataGridView1.Rows[i].Cells[3].Value.ToString().Replace(",", "") + "','" + dataGridView1.Rows[i].Cells[4].Value.ToString().Replace(",", "") + "','" + dataGridView1.Rows[i].Cells[5].Value.ToString().Replace(",", "") + "','" + txtTA.Text.Replace(",", "") + "','" + txtDiscount.Text.Replace(",", "") + "','" + txtSerCharge.Text.Replace(",", "") + "','" + txtPackCharge.Text.Replace(",", "") + "','" + lblroundof.Text.Replace(",", "") + "','" + lblnettotal.Text.Replace(",", "") + "','" + txtCashTendered.Text.Replace(",", "") + "','" + txtChange.Text.Replace(",", "") + "',1," + Master.companyId + ")";
                                cmd = new SqlCommand(qry, con);
                                cmd.ExecuteNonQuery();
                            }
                            
                            SqlCommand cmd7 = new SqlCommand("select CompanyName,Address,Phone,VATNo from Company where CompanyID='" + Master.companyId + "' and isActive=1", con);
                            SqlDataAdapter sda7 = new SqlDataAdapter(cmd7);
                            DataTable dt5 = new DataTable();
                            sda7.Fill(dt5);
                            string query = "update printing set [T20]='" + dt5.Rows[0][0].ToString() + "',[T21]='" + dt5.Rows[0][1].ToString() + "',[T22]=" + dt5.Rows[0][2].ToString() + ",[T23]='" + dt5.Rows[0][3].ToString() + "' where [T2]='"+txthbillId.Text+"'";
                            cmd = new SqlCommand(query, con);
                            cmd.ExecuteNonQuery();
                            BillPOSReport frm = new BillPOSReport();

                        //    frm.StartPosition = FormStartPosition.CenterScreen;
                            frm.Show();
                            frm.Close();
                           // PrintToPrinter();
                        }
                        clearall();
                        }
                         else
                         {
                             MessageBox.Show("Please select at least one item.");
                         }
                    }
                               
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

        private void PrintToPrinter()
        {
            PrintReport(System.Windows.Forms.Application.StartupPath + "\\BillingPOSPrintReport.rpt",
                "Send To OneNote 2010");

            //PrintPreviewDialog ppd = new PrintPreviewDialog();
            //ppd.Load += Load("D:/hitesh/Ramdev Sales Final/RamdevSales/BillingPOSPrintReport.rpt");
            
        }

        private void PrintReport(string reportPath, string PrinterName)
        {
            var dialog = new PrintDialog();
            CrystalDecisions.CrystalReports.Engine.ReportDocument rptDoc =
                                new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            
            //PrintPreviewDialog ppd = new PrintPreviewDialog();
            //ppd.Load += Load("D:/hitesh/Ramdev Sales Final/RamdevSales/BillingPOSPrintReport.rpt");

            rptDoc.Load("D:/hitesh/Ramdev Sales Final/RamdevSales/BillingPOSPrintReport.rpt");
           
            CrystalDecisions.Shared.PageMargins objPageMargins;
            objPageMargins = rptDoc.PrintOptions.PageMargins;
            objPageMargins.bottomMargin = 100;
            objPageMargins.leftMargin = 100;
            objPageMargins.rightMargin = 100;
            objPageMargins.topMargin = 100;
            rptDoc.PrintOptions.ApplyPageMargins(objPageMargins);
            rptDoc.PrintOptions.PrinterName = dialog.PrinterSettings.PrinterName;
            //  rptDoc.PrintOptions.PrinterName ="Hp LeserJet Professional M1136 MFP";
            rptDoc.PrintToPrinter(1, true, 0, 0);

        }
       
        private void checknull()
        {
            if (txtDiscount.Text == "")
            {
                txtDiscount.Text = "0.0";
            }
            if (txtVat.Text == "")
            {
                txtVat.Text = "0.0";
            }
            if (txtAddVat.Text == "")
            {
                txtAddVat.Text = "0.0";
            }
            if (txtSerCharge.Text == "")
            {
                txtSerCharge.Text = "0.0";
            }
            if (txtPackCharge.Text == "")
            {
                txtPackCharge.Text = "0.0";
            }
            if (txtCashTendered.Text == "")
            {
                txtCashTendered.Text = "0.0";
            }
            if (txtChange.Text == "")
            {
                txtChange.Text = "0.0";
            }
        }

        private void txtCashTendered_TextChanged(object sender, EventArgs e)
        {
            if (txtCashTendered.Text != "")
            {
                total();
            }
            else
            {
                txtChange.Text = "";
            }
        }

        int j = 0;
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
         {
            Double qty, rate;
            
            try
            {
                bool qtyid = Double.TryParse(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(), out qty);
                bool rateid = Double.TryParse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString(), out rate);
                if (qtyid && rateid && j == 0)
                {
                    Double amount = rate * qty;
                    j = 1;
                    dataGridView1.Rows[e.RowIndex].Cells[3].Value = amount.ToString();

                }
                    
                else
                {
                    dataGridView1.CancelEdit();
                    
                }
                j = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            total();
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult dr1 = MessageBox.Show("Do you want to Delete Item?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr1 == DialogResult.No)
            {

                e.Cancel = true;


            }

        }

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            total();
        }

        int cnt = 0;
        private Master master;
        private TabControl tabControl;
        internal void updatemode(string str, string p, int p_2)
        {
            loadpage();
            //getcon();
            cnt = 1;
            //SqlCommand cmd = new SqlCommand("select * from BillPOSMaster where BillId='" + p + "' and isactive=1", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);

            SqlCommand cmd1 = new SqlCommand("select ItemName,Qty,Rate,Amount,Vat,AddVat from BillPOSProductMaster where BillId='" + p + "' and isactive=1", con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);

            //txthbillId.Text = dt.Rows[0][0].ToString();
            //txtRunDate.Text = Convert.ToDateTime(dt.Rows[0][1].ToString()).ToString("dd/MM/yyyy");

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                dataGridView1.DataSource = dt1;
                dataGridView1.Columns[1].Width = 30;
                dataGridView1.Columns[2].Width = 40;
                dataGridView1.Columns[3].Width = 50;
                dataGridView1.Columns[4].Width = 0;
                dataGridView1.Columns[5].Width = 0;
            }
            foreach (DataGridViewColumn dc in dataGridView1.Columns)
            {
                if (dc.Index.Equals(1))
                {
                    dc.ReadOnly = false;
                }
                else
                {
                    dc.ReadOnly = true;
                }
            }
            
            //clearall();
            
            btnPay.Text = "Update";
            total();

            con.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //getcon();
            DialogResult dr = MessageBox.Show("Do you want to Delete data?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                SqlCommand cmd = new SqlCommand("update BillPOSMaster set isactive=0  where BillId='" + txthbillId.Text + "' and CompanyId="+Master.companyId+"", con);
                cmd.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("update BillPOSProductMaster set isactive=0 where BillId='" + txthbillId.Text + "'", con);
                cmd2.ExecuteNonQuery();
                clearall();
                MessageBox.Show("Bill Successfully Deleted.");
            }
            else
            {
               
            }
        }
       
        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtSerCharge_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtPackCharge_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtCashTendered_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtTA_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPackCharge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDiscount.Focus();
            }

           
        }

        private void txtDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtDiscount.Text != "" && Convert.ToDouble(txtDiscount.Text) > 0)
                {
                    double amt = (Convert.ToDouble(txtDiscount.Text) * 100) / Convert.ToDouble(txtTA.Text);
                    txtDiscPer.Text = Math.Round(amt, 2).ToString();
                    //itemcalculation(txtqty.Text);
                }
                txtDiscPer.Focus();
            }

           
        }

        private void txtCashTendered_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtChange.Focus();
            }

            
        }

        private void txtChange_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnPay.Focus();
            }

           
        }

        private void txtTA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPackCharge.Focus();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.F1))
            {
                txtPackCharge.Focus();
                return true;
            }
            if (keyData == Keys.F2)
            {
                txtDiscount.Focus();
            }
            if (keyData == Keys.F3)
            {
                txtCashTendered.Focus();
            }
            if (keyData == Keys.F4)
            {
                txtChange.Focus();
            }
            if (keyData == (Keys.Alt | Keys.P))
            {
                btnsubmit();
                SqlCommand cmd1 = new SqlCommand("select distinct(GroupName) from GroupMaster ", con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                color = 1;
                sda1.Fill(dt1);
                rebindbuttons(dt1);
                flag = 0;
            }
            if (keyData == (Keys.Alt | Keys.R))
            {
                clearall();
            }
            if (keyData == Keys.Escape)
            {
                this.Close();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtItemName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtItemName.Text != "")
                    {
                        flag1();
                        txtItemName.Text = "";
                        total();
                    }
                }
            }
            catch
            {
            }
        }

        private void DiscPer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch { }
        }

        private void cmbPayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPayMode.SelectedIndex == 2)
            {
                txtPayMode.Visible=true;
            }
        }

        private void txtDiscPer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtDiscPer.Text != "" && Convert.ToDouble(txtDiscPer.Text) > 0)
                {
                    double amt = (Convert.ToDouble(txtTA.Text) * Convert.ToDouble(txtDiscPer.Text)) / 100;
                    txtDiscount.Text = Math.Round(amt, 2).ToString();
                    //itemcalculation(txtqty.Text);
                }
                txtCashTendered.Focus();
            }

        }

        //private void itemcalculation(String qty)
        //{
        //    try
        //    {
        //        SqlCommand cmd5 = new SqlCommand("select convfactor from ProductMaster where product_name='" + txtitemname.Text + "'", con);
        //        SqlDataAdapter sda = new SqlDataAdapter(cmd5);
        //        DataTable dt = new DataTable();
        //        sda.Fill(dt);
        //        Double convfactor = Convert.ToDouble(dt.Rows[0]["convfactor"].ToString());

        //        double total = Convert.ToDouble(qty) * Convert.ToDouble(convfactor);

        //        double finaltotal = Convert.ToDouble(qty) * Convert.ToDouble(txtrate.Text);

        //        txttotal.Text = Math.Round(finaltotal, 2).ToString();
        //        txtdisamt.Text = (Math.Round((Convert.ToDouble(txtdisper.Text) * Convert.ToDouble(txttotal.Text)) / 100, 2)).ToString();
        //        double discount = Convert.ToDouble(txttotal.Text) - Convert.ToDouble(txtdisamt.Text);
        //        double tax = Math.Round(discount * (Convert.ToDouble(taxid.ToString())) / 100, 2);
        //        double addtax = Math.Round(discount * (Convert.ToDouble(addtaxid.ToString())) / 100, 2);
        //        double amount = Math.Round(discount + ((discount * (Convert.ToDouble(taxid.ToString()) + Convert.ToDouble(addtaxid.ToString()))) / 100), 2);
        //        txtamount.Text = Math.Round(amount, 2).ToString();
        //        txttax.Text = tax.ToString();
        //        txtaddtax.Text = addtax.ToString();

        //    }
        //    catch
        //    {
        //    }
        //}


    }
}
