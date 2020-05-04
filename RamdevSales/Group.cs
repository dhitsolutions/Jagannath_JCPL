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
    public partial class Group : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        DataTable dt = new DataTable();
        int flagforbind = 0;
        int cnt = 0;
        public Group()
        {
            InitializeComponent();
            flagforbind = 0;
        }

        public Group(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
            flagforbind = 0;
        }
        public static string pvc;
        public Group(Accountentry accountentry, TabControl tabControl, Master master,string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.accountentry = accountentry;
            this.tabControl = tabControl;
            this.master = master;
            flagforbind = 1;
            pvc = activecontroal;
        }
        public void bindgroup()
        {

            //DataTable dt = new DataTable();
            //dt = cn.getdataset("select * from accountgroup order by groupname asc");
            //txtgrop.Refresh();
            //txtgrop.ValueMember = "id";
            //txtgrop.DisplayMember = "groupname";
            //txtgrop.DataSource = dt;
            //txtgrop.SelectedIndex = -1;

            SqlCommand cmd = new SqlCommand("select id,groupname from accountgroup order by groupname asc", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt11 = new DataTable();
            sda.Fill(dt11);

            txtgrop.ValueMember = "id";
            txtgrop.DisplayMember = "groupname";
            txtgrop.DataSource = dt11;
            txtgrop.SelectedIndex = -1;
            // autobind(dt, cmbsaletype);

        }
       

        private void Group_Load(object sender, EventArgs e)
        {
            con.Open();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            LVclient.Columns.Add("Account Group Name", 300, HorizontalAlignment.Center);
            LVclient.Columns.Add("Primary", 100, HorizontalAlignment.Center);
            LVclient.Columns.Add("Under Group", 200, HorizontalAlignment.Center);
            if (cnt == 0)
            {
                bindgroup();
            }
            listviewbind();
           // binddrop();
            con.Close();
           // txtgrpname.Focus();
            this.ActiveControl = txtgrpname;
        }

        private void listviewbind()
        {
            try
            {
                LVclient.Items.Clear();

                SqlCommand cmd = new SqlCommand("Select * from AccountGroup order by groupname", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    LVclient.Items.Add(dt.Rows[i].ItemArray[1].ToString());
                    LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                    LVclient.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                  
                }
            }
            catch
            {
            }
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

                        SqlCommand cmd = new SqlCommand("select * from AccountGroup where groupname='" + ABC + "'", con);
                        string id = cmd.ExecuteScalar().ToString();
                        string chk;
                        if (chkpgroup.Checked == true)
                        {
                            chk = "Y";
                        }
                        else
                        {
                            chk = "N";
                        }
                        cmd = new SqlCommand("UPDATE [dbo].[AccountGroup] SET groupname='" + txtgrpname.Text + "',PrimaryGroup='"+chk+"', UnderGroupID='" + txtgrop.SelectedValue + "', UnderGroup='" + txtgrop.Text + "' where id=" + id, con);
                        //  SqlCommand cmd = new SqlCommand("INSERT INTO [Billing].[dbo].[BillMaster] ([Bill_No] ,[Bill_Date],[ClientID],[PO_No],[Bill_Amt],[Bill_vat_Amt],[Bill_Net_Amt],[CompanyID])VALUES ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString("MM-dd-yyyy") + "','" + cmbcustname.SelectedValue + "','" + txtpono.Text + "','" + Convert.ToDouble(TxtBillTotal.Text) + "','" + Convert.ToDouble(TxtVAT16Amnt.Text) + "','" + Convert.ToDouble(TxtVATExmptAmnt.Text) + "','" + cmp + "')", con);
                        cmd.ExecuteNonQuery();
                       
                        Button18.Text = "SAVE";
                        listviewbind();
                        if (flagforbind == 1)
                        {
                            accountentry.bindgroup();
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
                            txtgrpname.Focus();
                            this.ActiveControl = txtgrpname;
                        }
                        txtgrpname.Text = "";
                        txtgrop.SelectedIndex = -1;
                    }
                    else
                    {
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }

                        con.Open();
                        SqlCommand cmd = new SqlCommand("Select * from AccountGroup where groupname='"+ txtgrpname.Text +"'", con);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if(dt.Rows.Count > 0)
                        {
                            MessageBox.Show("Account Group Name Already Exist..");
                        }
                        else
                        {
                            string chk;
                            if (chkpgroup.Checked == true)
                            {
                                chk = "Y";
                            }
                            else
                            {
                                chk = "N";
                            }
                            SqlCommand cmd1 = new SqlCommand("INSERT INTO [dbo].[AccountGroup]([groupname],[PrimaryGroup],[UnderGroupID],[UnderGroup])values ('" + txtgrpname.Text +"','"+chk+ "','" + txtgrop.SelectedValue + "','" + txtgrop.Text + "')", con);
                            //  SqlCommand cmd = new SqlCommand("INSERT INTO [Billing].[dbo].[BillMaster] ([Bill_No] ,[Bill_Date],[ClientID],[PO_No],[Bill_Amt],[Bill_vat_Amt],[Bill_Net_Amt],[CompanyID])VALUES ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString("MM-dd-yyyy") + "','" + cmbcustname.SelectedValue + "','" + txtpono.Text + "','" + Convert.ToDouble(TxtBillTotal.Text) + "','" + Convert.ToDouble(TxtVAT16Amnt.Text) + "','" + Convert.ToDouble(TxtVATExmptAmnt.Text) + "','" + cmp + "')", con);
                            cmd1.ExecuteNonQuery();
                            listviewbind();
                            
                            Button18.Text = "SAVE";
                        }
                        if (flagforbind == 1)
                        {
                            accountentry.bindgroup();
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
                            txtgrpname.Focus();
                            this.ActiveControl = txtgrpname;
                        }
                        txtgrpname.Text = "";
                        txtgrop.SelectedIndex = -1;
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
        private void Button18_Click(object sender, EventArgs e)
        {
            submit();
        }
        string ABC="";
        private Master master;
        private TabControl tabControl;
        private Accountentry accountentry;
        public void open()
        {
            try
            {
                if (LVclient.SelectedItems.Count > 0)
                {
                    ABC = LVclient.Items[LVclient.FocusedItem.Index].SubItems[0].Text;
                    txtgrpname.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[0].Text;
                    if (LVclient.Items[LVclient.FocusedItem.Index].SubItems[1].Text == "Y")
                    {
                        chkpgroup.Checked = true;
                    }
                    else
                    {
                        chkpgroup.Checked = false;
                    }
                    txtgrop.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[2].Text;
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
        
        internal void updatemode(int p, string iid)
        {

            cnt = 1;
                dt = conn.getdataset("select * from AccountGroup where groupname='" + iid + "'");
                if (dt.Rows.Count > 0)
                {
                    txtgrpname.Text = dt.Rows[0][1].ToString();
                    if (dt.Rows[0]["PrimaryGroup"].ToString() == "Y")
                    {
                        chkpgroup.Checked = true;
                    }
                    else
                    {
                        chkpgroup.Checked = false;
                    }
                    ABC = dt.Rows[0][1].ToString();
                    bindgroup();
                    txtgrop.Text = dt.Rows[0]["UnderGroup"].ToString();
                    Button18.Text = "UPDATE";
                }
                else
                {
                    MessageBox.Show("Please Select valid Group Name", "Group", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

        }
        private void txtgrpname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkpgroup.Focus();
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

        private void LVclient_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void btnClose_Enter(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = false;
            btnClose.BackColor = System.Drawing.Color.FromArgb(248, 152, 94);
            btnClose.ForeColor = System.Drawing.Color.White;
        }

        private void btnClose_Leave(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = true;
            btnClose.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnClose.ForeColor = System.Drawing.Color.White;
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = false;
            btnClose.BackColor = System.Drawing.Color.FromArgb(248, 152, 94);
            btnClose.ForeColor = System.Drawing.Color.White;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = true;
            btnClose.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnClose.ForeColor = System.Drawing.Color.White;
        }

        private void LVclient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                open();
            }
        }

        private void txtgrop_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < txtgrop.Items.Count; i++)
                {
                    s = txtgrop.GetItemText(txtgrop.Items[i]);
                    if (s == txtgrop.Text)
                    {
                        inList = true;
                        txtgrop.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    txtgrop.Text = "";
                }
                Button18.Focus();
            }
        }

        private void txtgrop_Enter(object sender, EventArgs e)
        {
            try
            {
                txtgrop.SelectedIndex = 0;
                txtgrop.DroppedDown = true;
            }
            catch
            {
            }
        }
        public static string s;
        private void txtgrop_Leave(object sender, EventArgs e)
        {
            txtgrop.Text = s;
        }

        private void txtgrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < txtgrop.Items.Count; i++)
                {
                    s = txtgrop.GetItemText(txtgrop.Items[i]);
                    if (s == txtgrop.Text)
                    {
                        inList = true;
                        txtgrop.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    txtgrop.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void chkpgroup_CheckedChanged(object sender, EventArgs e)
        {
            if (chkpgroup.Checked == true)
            {
                txtgrop.Enabled = false;
                txtgrop.SelectedIndex = -1;
            }
            else
            {
                txtgrop.Enabled = true;
                
            }
        }

        private void chkpgroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (chkpgroup.Checked == true)
                {
                    Button18.Focus();
                }
                else
                {
                    txtgrop.Focus();
                }
            }
        }

    }
}
