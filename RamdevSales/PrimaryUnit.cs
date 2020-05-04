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
    public partial class PrimaryUnit : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        DataTable dt = new DataTable();
        private Itementry itementry;
        int a;
        int cnt = 0;
        public static string pvc;
        private Master master;
        private TabControl tabControl;
        string ids;
        string ABC = "";
        public PrimaryUnit()
        {
            InitializeComponent();
        }

        public PrimaryUnit(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
            a = 0;
           
        }

        public PrimaryUnit(Itementry itementry, TabControl tabControl, Master master,string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.itementry = itementry;
            this.tabControl = tabControl;
            this.master = master;
            a = 1;
            pvc = activecontroal;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    if (string.IsNullOrEmpty(pvc) == true)
                    {
                        master.RemoveCurrentTab();
                    }
                    else
                    {
                        master.RemoveCurrentTab1(pvc, txtgrpname.Text);
                    }

                }
                return true;
            }
            if (keyData == (Keys.Alt | Keys.U))
            {
                DialogResult dr = MessageBox.Show("Do you want to Update?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    submit();
                }
                return true;
            }


            return base.ProcessCmdKey(ref msg, keyData);
        }
        public void submit()
        {
            try
            {
                if (txtgrpname.Text != "")
                {
                    if (Button18.Text == "UPDATE")
                    {
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }

                        con.Open();

                        SqlCommand cmd = new SqlCommand("select * from UnitMaster where UnitName='" + ABC + "'", con);
                        string id = cmd.ExecuteScalar().ToString();
                        ids = id;
                        cmd = new SqlCommand("UPDATE [dbo].[UnitMaster] SET UnitName='" + txtgrpname.Text +"',UQC='"+txtpunit.Text+ "' where id=" + id, con);
                        //  SqlCommand cmd = new SqlCommand("INSERT INTO [Billing].[dbo].[BillMaster] ([Bill_No] ,[Bill_Date],[ClientID],[PO_No],[Bill_Amt],[Bill_vat_Amt],[Bill_Net_Amt],[CompanyID])VALUES ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString("MM-dd-yyyy") + "','" + cmbcustname.SelectedValue + "','" + txtpono.Text + "','" + Convert.ToDouble(TxtBillTotal.Text) + "','" + Convert.ToDouble(TxtVAT16Amnt.Text) + "','" + Convert.ToDouble(TxtVATExmptAmnt.Text) + "','" + cmp + "')", con);
                        cmd.ExecuteNonQuery();
                       
                        txtpunit.SelectedIndex = -1;
                        Button18.Text = "SAVE";
                        listviewbind();
                        if (a == 1)
                        {
                            itementry.bindgpunit();
                            itementry.bindgaunit();
                            if (string.IsNullOrEmpty(pvc) == true)
                            {
                                master.RemoveCurrentTab();
                            }
                            else
                            {
                                master.RemoveCurrentTab1(pvc, txtgrpname.Text);
                            }
                        }

                        else
                        {
                            this.ActiveControl = txtgrpname;
                            txtgrpname.Focus();
                        }
                        txtgrpname.Text = "";
                     //   master.RemoveCurrentTab();
                    }
                    else
                    {
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }

                        con.Open();
                        SqlCommand cmd = new SqlCommand("Select * from UnitMaster where UnitName='" + txtgrpname.Text + "' AND UQC='"+ txtpunit.Text +"' AND isactive=1", con);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        
                        if (dt.Rows.Count > 0)
                        {
                            int iid = Convert.ToInt32(dt.Rows[0]["id"].ToString());
                            cmd = new SqlCommand("UPDATE [dbo].[UnitMaster] SET UnitName='" + txtgrpname.Text + "',UQC='" + txtpunit.Text + "' where id=" + iid, con);
                            //  SqlCommand cmd = new SqlCommand("INSERT INTO [Billing].[dbo].[BillMaster] ([Bill_No] ,[Bill_Date],[ClientID],[PO_No],[Bill_Amt],[Bill_vat_Amt],[Bill_Net_Amt],[CompanyID])VALUES ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString("MM-dd-yyyy") + "','" + cmbcustname.SelectedValue + "','" + txtpono.Text + "','" + Convert.ToDouble(TxtBillTotal.Text) + "','" + Convert.ToDouble(TxtVAT16Amnt.Text) + "','" + Convert.ToDouble(TxtVATExmptAmnt.Text) + "','" + cmp + "')", con);
                            cmd.ExecuteNonQuery();
                            listviewbind();
                            txtgrpname.Text = "";
                            txtpunit.SelectedIndex = -1;
                            Button18.Text = "SAVE";
                           // MessageBox.Show("Item Group Name Already Exist..");
                        }
                        else
                        {
                            SqlCommand cmd1 = new SqlCommand("INSERT INTO [dbo].[UnitMaster]([UnitName],[UQC],[isactive])values ('" + txtgrpname.Text +"','"+txtpunit.Text+ "',1)", con);
                            //  SqlCommand cmd = new SqlCommand("INSERT INTO [Billing].[dbo].[BillMaster] ([Bill_No] ,[Bill_Date],[ClientID],[PO_No],[Bill_Amt],[Bill_vat_Amt],[Bill_Net_Amt],[CompanyID])VALUES ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString("MM-dd-yyyy") + "','" + cmbcustname.SelectedValue + "','" + txtpono.Text + "','" + Convert.ToDouble(TxtBillTotal.Text) + "','" + Convert.ToDouble(TxtVAT16Amnt.Text) + "','" + Convert.ToDouble(TxtVATExmptAmnt.Text) + "','" + cmp + "')", con);
                            cmd1.ExecuteNonQuery();
                            listviewbind();
                          
                            txtpunit.SelectedIndex = -1;
                            Button18.Text = "SAVE";
                            if (a == 1)
                            {
                                itementry.bindgpunit();
                                itementry.bindgaunit();
                                if (string.IsNullOrEmpty(pvc) == true)
                                {
                                    master.RemoveCurrentTab();
                                }
                                else
                                {
                                    master.RemoveCurrentTab1(pvc, txtgrpname.Text);
                                }
                            }
                            else
                            {
                                this.ActiveControl = txtgrpname;
                                txtgrpname.Focus();
                            }
                            txtgrpname.Text = "";
                           // master.RemoveCurrentTab();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Group Name cannot be Blank");
                    this.ActiveControl = txtgrpname;
                    return;
                }
            }
            catch
            {
                con.Close();
            }
        }
        private void listviewbind()
        {
            try
            {
                LVclient.Items.Clear();

                SqlCommand cmd = new SqlCommand("Select * from UnitMaster where isactive=1 order by UnitName", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    LVclient.Items.Add(dt.Rows[i].ItemArray[1].ToString());
                    LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());

                }
            }
            catch
            {
            }
        }
        private void PrimaryUnit_Load(object sender, EventArgs e)
        {
            con.Open();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            LVclient.Columns.Add("Item Unit Name", 300, HorizontalAlignment.Center);
            LVclient.Columns.Add("Unit Quantity Code(UQC)", 300, HorizontalAlignment.Center);

            listviewbind();
            // binddrop();
            con.Close();
          //  txtgrpname.Focus();
            this.ActiveControl = txtgrpname;
         //   txtpunit.SelectedIndex = -1;
            //set the interval  and start the timer
          //  timer1.Interval = 1000;
          //  timer1.Start();
        }

        private void txtgrpname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtpunit.Focus();
            } 
        }

        private void txtgrpname_Enter(object sender, EventArgs e)
        {
            txtgrpname.BackColor = Color.LightYellow;
        }

        private void txtgrpname_Leave(object sender, EventArgs e)
        {
            txtgrpname.BackColor = Color.White;
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            submit();
        }
        public void open()
        {
            try
            {
                if (LVclient.SelectedItems.Count > 0)
                {
                    ABC = LVclient.Items[LVclient.FocusedItem.Index].SubItems[0].Text;
                    txtgrpname.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[0].Text;
                    txtpunit.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[1].Text;
                    Button18.Text = "UPDATE";
                    LVclient.Items[LVclient.FocusedItem.Index].Remove();
                }
            }
            catch
            {
            }
        }
        private void LVclient_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            open();
        }

        private void LVclient_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(pvc) == true)
                {
                    master.RemoveCurrentTab();
                }
                else
                {
                    master.RemoveCurrentTab1(pvc, txtgrpname.Text);
                }
            }
        }

        private void Button18_MouseEnter(object sender, EventArgs e)
        {
            Button18.UseVisualStyleBackColor = false;
            Button18.BackColor = Color.YellowGreen;
            Button18.ForeColor = Color.White;
        }

        private void Button18_MouseLeave(object sender, EventArgs e)
        {
            Button18.UseVisualStyleBackColor = true;
            Button18.BackColor = Color.FromArgb(51, 153, 255);
            Button18.ForeColor = Color.White;
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

        private void txtpunit_Enter(object sender, EventArgs e)
        {
            try
            {
                txtpunit.SelectedIndex = 0;
                txtpunit.DroppedDown = true;
            }
            catch
            {
            }
        }
        public static string s;
        private void txtpunit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < txtpunit.Items.Count; i++)
                {
                    s = txtpunit.GetItemText(txtpunit.Items[i]);
                    if (s == txtpunit.Text)
                    {
                        inList = true;
                        txtpunit.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    txtpunit.Text = "";
                }

                Button18.Focus();
            } 
        }

        private void Button18_Enter(object sender, EventArgs e)
        {
            Button18.UseVisualStyleBackColor = false;
            Button18.BackColor = Color.YellowGreen;
            Button18.ForeColor = Color.White;
        }

        private void Button18_Leave(object sender, EventArgs e)
        {
            Button18.UseVisualStyleBackColor = true;
            Button18.BackColor = Color.FromArgb(51, 153, 255);
            Button18.ForeColor = Color.White;
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
        string searchstr;
        private void txtpunit_KeyUp(object sender, KeyEventArgs e)
        {
            if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            {
                searchstr = searchstr + Convert.ToChar(e.KeyCode);
                // If the Search string is greater than 1 then use custom logic
                if (searchstr.Length > 1)
                {
                    int index;
                    // Search the Item that matches the string typed
                    index = txtpunit.FindString(searchstr);
                    // Select the Item in the Combo
                    if (index > 0)
                    {
                        txtpunit.SelectedIndex = index;
                    }
                }


            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //empty the string for every 1 seconds
           // searchstr = "";
        }

        internal void Update(string primaryunit)
        {
            if (primaryunit.ToString() != "" && primaryunit.ToString() != null)
            {
                SqlCommand cmd5 = new SqlCommand("select * from UnitMaster where id='" + primaryunit + "'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd5);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    txtgrpname.Text = dt.Rows[0]["UnitName"].ToString();
                    txtpunit.Text = dt.Rows[0]["UQC"].ToString();
                    ids = dt.Rows[0]["id"].ToString();
                    ABC = dt.Rows[0]["UnitName"].ToString();
                    Button18.Text = "UPDATE";
                }
            }
          //  throw new NotImplementedException();
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

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Delete Unit?", "Unit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                   // this.Enabled = false;
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    string id = LVclient.Items[LVclient.FocusedItem.Index].SubItems[0].Text;;
                    SqlCommand cmd = new SqlCommand("update UnitMaster set isactive=0 where UnitName='" + id + "'", con);
                    cmd.ExecuteNonQuery();
                    listviewbind();
                }
            }
            catch (Exception excp)
            {

            }
        }

        private void txtpunit_Leave(object sender, EventArgs e)
        {
            txtpunit.Text = s;
        }

        private void txtpunit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < txtpunit.Items.Count; i++)
                {
                    s = txtpunit.GetItemText(txtpunit.Items[i]);
                    if (s == txtpunit.Text)
                    {
                        inList = true;
                        txtpunit.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    txtpunit.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void LVclient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                open();
            }
        }
    }
}
