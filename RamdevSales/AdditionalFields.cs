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
    public partial class AdditionalFields : Form
    {
        private Master master;
        private TabControl tabControl;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        OleDbSettings ods = new OleDbSettings();
        Printing prn = new Printing();
        public AdditionalFields()
        {
            InitializeComponent();
        }

        public AdditionalFields(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
            this.ActiveControl = Button18;
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

        private void cmbmastertype_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbmastertype.SelectedIndex = 0;
                cmbmastertype.DroppedDown = true;
            }
            catch
            {
            }
        }
        public static string s;
        private void cmbmastertype_Leave(object sender, EventArgs e)
        {
            cmbmastertype.Text = s;
        }

        private void cmbmastertype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbmastertype.Items.Count; i++)
                {
                    s = cmbmastertype.GetItemText(cmbmastertype.Items[i]);
                    if (s == cmbmastertype.Text)
                    {
                        inList = true;
                        cmbmastertype.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbmastertype.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void cmbmastertype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbmastertype.Items.Count; i++)
                {
                    s = cmbmastertype.GetItemText(cmbmastertype.Items[i]);
                    if (s == cmbmastertype.Text)
                    {
                        inList = true;
                        cmbmastertype.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbmastertype.Text = "";
                }
                cmbnooffields.Focus();
            }
        }

        private void cmbnooffields_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbnooffields.SelectedIndex = 0;
                cmbnooffields.DroppedDown = true;
            }
            catch
            {
            }
        }

        private void cmbnooffields_Leave(object sender, EventArgs e)
        {
            cmbnooffields.Text = s;
        }
        DataTable dt = new DataTable();
        private void cmbnooffields_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbnooffields.SelectedIndex != -1)
                {
                    bool inList = false;
                    for (int i = 0; i < cmbnooffields.Items.Count; i++)
                    {
                        s = cmbnooffields.GetItemText(cmbnooffields.Items[i]);
                        if (s == cmbnooffields.Text)
                        {
                            inList = true;
                            cmbnooffields.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        cmbnooffields.Text = "";
                    }
                    dt = conn.getdataset("select * from Additional where MasterType='" + cmbmastertype.Text + "'");
                    if (cmbnooffields.Text == "1")
                    {
                        if (dt.Rows.Count > 0)
                        {
                            txtfield1.Text = dt.Rows[0]["field1"].ToString();
                        }
                        else
                        {
                            txtfield1.Text = "";
                        }
                        f1.Visible = true;
                        txtfield1.Visible = true;
                        f2.Visible = false;
                        txtfield2.Visible = false;
                        f3.Visible = false;
                        txtfield3.Visible = false;
                        f4.Visible = false;
                        txtfield4.Visible = false;
                        f5.Visible = false;
                        txtfield5.Visible = false;
                        f6.Visible = false;
                        txtfield6.Visible = false;
                        f7.Visible = false;
                        txtfield7.Visible = false;
                        f8.Visible = false;
                        txtfield8.Visible = false;
                        f9.Visible = false;
                        txtfield9.Visible = false;
                        f10.Visible = false;
                        txtfield10.Visible = false;
                        f11.Visible = false;
                        txtfield11.Visible = false;
                        f12.Visible = false;
                        txtfield12.Visible = false;
                        f13.Visible = false;
                        txtfield13.Visible = false;
                        f14.Visible = false;
                        txtfield14.Visible = false;
                        f15.Visible = false;
                        txtfield15.Visible = false;
                    }
                    else if (cmbnooffields.Text == "2")
                    {
                        if (dt.Rows.Count > 0)
                        {
                            txtfield1.Text = dt.Rows[0]["field1"].ToString();
                            txtfield2.Text = dt.Rows[0]["field2"].ToString();
                        }
                        else
                        {
                            txtfield1.Text = "";
                            txtfield2.Text = "";
                        }
                        f1.Visible = true;
                        txtfield1.Visible = true;
                        f2.Visible = true;
                        txtfield2.Visible = true;
                        f3.Visible = false;
                        txtfield3.Visible = false;
                        f4.Visible = false;
                        txtfield4.Visible = false;
                        f5.Visible = false;
                        txtfield5.Visible = false;
                        f6.Visible = false;
                        txtfield6.Visible = false;
                        f7.Visible = false;
                        txtfield7.Visible = false;
                        f8.Visible = false;
                        txtfield8.Visible = false;
                        f9.Visible = false;
                        txtfield9.Visible = false;
                        f10.Visible = false;
                        txtfield10.Visible = false;
                        f11.Visible = false;
                        txtfield11.Visible = false;
                        f12.Visible = false;
                        txtfield12.Visible = false;
                        f13.Visible = false;
                        txtfield13.Visible = false;
                        f14.Visible = false;
                        txtfield14.Visible = false;
                        f15.Visible = false;
                        txtfield15.Visible = false;
                    }
                    else if (cmbnooffields.Text == "3")
                    {
                        if (dt.Rows.Count > 0)
                        {
                            txtfield1.Text = dt.Rows[0]["field1"].ToString();
                            txtfield2.Text = dt.Rows[0]["field2"].ToString();
                            txtfield3.Text = dt.Rows[0]["field3"].ToString();
                        }
                        else
                        {
                            txtfield1.Text = "";
                            txtfield2.Text = "";
                            txtfield3.Text = "";
                        }
                        f1.Visible = true;
                        txtfield1.Visible = true;
                        f2.Visible = true;
                        txtfield2.Visible = true;
                        f3.Visible = true;
                        txtfield3.Visible = true;
                        f4.Visible = false;
                        txtfield4.Visible = false;
                        f5.Visible = false;
                        txtfield5.Visible = false;
                        f6.Visible = false;
                        txtfield6.Visible = false;
                        f7.Visible = false;
                        txtfield7.Visible = false;
                        f8.Visible = false;
                        txtfield8.Visible = false;
                        f9.Visible = false;
                        txtfield9.Visible = false;
                        f10.Visible = false;
                        txtfield10.Visible = false;
                        f11.Visible = false;
                        txtfield11.Visible = false;
                        f12.Visible = false;
                        txtfield12.Visible = false;
                        f13.Visible = false;
                        txtfield13.Visible = false;
                        f14.Visible = false;
                        txtfield14.Visible = false;
                        f15.Visible = false;
                        txtfield15.Visible = false;
                    }
                    else if (cmbnooffields.Text == "4")
                    {
                        if (dt.Rows.Count > 0)
                        {
                            txtfield1.Text = dt.Rows[0]["field1"].ToString();
                            txtfield2.Text = dt.Rows[0]["field2"].ToString();
                            txtfield3.Text = dt.Rows[0]["field3"].ToString();
                            txtfield4.Text = dt.Rows[0]["field4"].ToString();
                        }
                        else
                        {
                            txtfield1.Text = "";
                            txtfield2.Text = "";
                            txtfield3.Text = "";
                            txtfield4.Text = "";
                        }
                        f1.Visible = true;
                        txtfield1.Visible = true;
                        f2.Visible = true;
                        txtfield2.Visible = true;
                        f3.Visible = true;
                        txtfield3.Visible = true;
                        f4.Visible = true;
                        txtfield4.Visible = true;
                        f5.Visible = false;
                        txtfield5.Visible = false;
                        f6.Visible = false;
                        txtfield6.Visible = false;
                        f7.Visible = false;
                        txtfield7.Visible = false;
                        f8.Visible = false;
                        txtfield8.Visible = false;
                        f9.Visible = false;
                        txtfield9.Visible = false;
                        f10.Visible = false;
                        txtfield10.Visible = false;
                        f11.Visible = false;
                        txtfield11.Visible = false;
                        f12.Visible = false;
                        txtfield12.Visible = false;
                        f13.Visible = false;
                        txtfield13.Visible = false;
                        f14.Visible = false;
                        txtfield14.Visible = false;
                        f15.Visible = false;
                        txtfield15.Visible = false;
                    }
                    else if (cmbnooffields.Text == "5")
                    {
                        if (dt.Rows.Count > 0)
                        {
                            txtfield1.Text = dt.Rows[0]["field1"].ToString();
                            txtfield2.Text = dt.Rows[0]["field2"].ToString();
                            txtfield3.Text = dt.Rows[0]["field3"].ToString();
                            txtfield4.Text = dt.Rows[0]["field4"].ToString();
                            txtfield5.Text = dt.Rows[0]["field5"].ToString();
                        }
                        else
                        {
                            txtfield1.Text = "";
                            txtfield2.Text = "";
                            txtfield3.Text = "";
                            txtfield4.Text = "";
                            txtfield5.Text = "";
                        }
                        f1.Visible = true;
                        txtfield1.Visible = true;
                        f2.Visible = true;
                        txtfield2.Visible = true;
                        f3.Visible = true;
                        txtfield3.Visible = true;
                        f4.Visible = true;
                        txtfield4.Visible = true;
                        f5.Visible = true;
                        txtfield5.Visible = true;
                        f6.Visible = false;
                        txtfield6.Visible = false;
                        f7.Visible = false;
                        txtfield7.Visible = false;
                        f8.Visible = false;
                        txtfield8.Visible = false;
                        f9.Visible = false;
                        txtfield9.Visible = false;
                        f10.Visible = false;
                        txtfield10.Visible = false;
                        f11.Visible = false;
                        txtfield11.Visible = false;
                        f12.Visible = false;
                        txtfield12.Visible = false;
                        f13.Visible = false;
                        txtfield13.Visible = false;
                        f14.Visible = false;
                        txtfield14.Visible = false;
                        f15.Visible = false;
                        txtfield15.Visible = false;
                    }
                    else if (cmbnooffields.Text == "6")
                    {
                        if (dt.Rows.Count > 0)
                        {
                            txtfield1.Text = dt.Rows[0]["field1"].ToString();
                            txtfield2.Text = dt.Rows[0]["field2"].ToString();
                            txtfield3.Text = dt.Rows[0]["field3"].ToString();
                            txtfield4.Text = dt.Rows[0]["field4"].ToString();
                            txtfield5.Text = dt.Rows[0]["field5"].ToString();
                            txtfield6.Text = dt.Rows[0]["field6"].ToString();
                        }
                        else
                        {
                            txtfield1.Text = "";
                            txtfield2.Text = "";
                            txtfield3.Text = "";
                            txtfield4.Text = "";
                            txtfield5.Text = "";
                            txtfield6.Text = "";
                        }
                        f1.Visible = true;
                        txtfield1.Visible = true;
                        f2.Visible = true;
                        txtfield2.Visible = true;
                        f3.Visible = true;
                        txtfield3.Visible = true;
                        f4.Visible = true;
                        txtfield4.Visible = true;
                        f5.Visible = true;
                        txtfield5.Visible = true;
                        f6.Visible = true;
                        txtfield6.Visible = true;
                        f7.Visible = false;
                        txtfield7.Visible = false;
                        f8.Visible = false;
                        txtfield8.Visible = false;
                        f9.Visible = false;
                        txtfield9.Visible = false;
                        f10.Visible = false;
                        txtfield10.Visible = false;
                        f11.Visible = false;
                        txtfield11.Visible = false;
                        f12.Visible = false;
                        txtfield12.Visible = false;
                        f13.Visible = false;
                        txtfield13.Visible = false;
                        f14.Visible = false;
                        txtfield14.Visible = false;
                        f15.Visible = false;
                        txtfield15.Visible = false;
                    }
                    else if (cmbnooffields.Text == "7")
                    {
                        if (dt.Rows.Count > 0)
                        {
                            txtfield1.Text = dt.Rows[0]["field1"].ToString();
                            txtfield2.Text = dt.Rows[0]["field2"].ToString();
                            txtfield3.Text = dt.Rows[0]["field3"].ToString();
                            txtfield4.Text = dt.Rows[0]["field4"].ToString();
                            txtfield5.Text = dt.Rows[0]["field5"].ToString();
                            txtfield6.Text = dt.Rows[0]["field6"].ToString();
                            txtfield7.Text = dt.Rows[0]["field7"].ToString();
                        }
                        else
                        {
                            txtfield1.Text = "";
                            txtfield2.Text = "";
                            txtfield3.Text = "";
                            txtfield4.Text = "";
                            txtfield5.Text = "";
                            txtfield6.Text = "";
                            txtfield7.Text = "";
                        }
                        f1.Visible = true;
                        txtfield1.Visible = true;
                        f2.Visible = true;
                        txtfield2.Visible = true;
                        f3.Visible = true;
                        txtfield3.Visible = true;
                        f4.Visible = true;
                        txtfield4.Visible = true;
                        f5.Visible = true;
                        txtfield5.Visible = true;
                        f6.Visible = true;
                        txtfield6.Visible = true;
                        f7.Visible = true;
                        txtfield7.Visible = true;
                        f8.Visible = false;
                        txtfield8.Visible = false;
                        f9.Visible = false;
                        txtfield9.Visible = false;
                        f10.Visible = false;
                        txtfield10.Visible = false;
                        f11.Visible = false;
                        txtfield11.Visible = false;
                        f12.Visible = false;
                        txtfield12.Visible = false;
                        f13.Visible = false;
                        txtfield13.Visible = false;
                        f14.Visible = false;
                        txtfield14.Visible = false;
                        f15.Visible = false;
                        txtfield15.Visible = false;
                    }
                    else if (cmbnooffields.Text == "8")
                    {
                        if (dt.Rows.Count > 0)
                        {
                            txtfield1.Text = dt.Rows[0]["field1"].ToString();
                            txtfield2.Text = dt.Rows[0]["field2"].ToString();
                            txtfield3.Text = dt.Rows[0]["field3"].ToString();
                            txtfield4.Text = dt.Rows[0]["field4"].ToString();
                            txtfield5.Text = dt.Rows[0]["field5"].ToString();
                            txtfield6.Text = dt.Rows[0]["field6"].ToString();
                            txtfield7.Text = dt.Rows[0]["field7"].ToString();
                            txtfield8.Text = dt.Rows[0]["field8"].ToString();
                        }
                        else
                        {
                            txtfield1.Text = "";
                            txtfield2.Text = "";
                            txtfield3.Text = "";
                            txtfield4.Text = "";
                            txtfield5.Text = "";
                            txtfield6.Text = "";
                            txtfield7.Text = "";
                            txtfield8.Text = "";
                        }
                        f1.Visible = true;
                        txtfield1.Visible = true;
                        f2.Visible = true;
                        txtfield2.Visible = true;
                        f3.Visible = true;
                        txtfield3.Visible = true;
                        f4.Visible = true;
                        txtfield4.Visible = true;
                        f5.Visible = true;
                        txtfield5.Visible = true;
                        f6.Visible = true;
                        txtfield6.Visible = true;
                        f7.Visible = true;
                        txtfield7.Visible = true;
                        f8.Visible = true;
                        txtfield8.Visible = true;
                        f9.Visible = false;
                        txtfield9.Visible = false;
                        f10.Visible = false;
                        txtfield10.Visible = false;
                        f11.Visible = false;
                        txtfield11.Visible = false;
                        f12.Visible = false;
                        txtfield12.Visible = false;
                        f13.Visible = false;
                        txtfield13.Visible = false;
                        f14.Visible = false;
                        txtfield14.Visible = false;
                        f15.Visible = false;
                        txtfield15.Visible = false;
                    }
                    else if (cmbnooffields.Text == "9")
                    {
                        if (dt.Rows.Count > 0)
                        {
                            txtfield1.Text = dt.Rows[0]["field1"].ToString();
                            txtfield2.Text = dt.Rows[0]["field2"].ToString();
                            txtfield3.Text = dt.Rows[0]["field3"].ToString();
                            txtfield4.Text = dt.Rows[0]["field4"].ToString();
                            txtfield5.Text = dt.Rows[0]["field5"].ToString();
                            txtfield6.Text = dt.Rows[0]["field6"].ToString();
                            txtfield7.Text = dt.Rows[0]["field7"].ToString();
                            txtfield8.Text = dt.Rows[0]["field8"].ToString();
                            txtfield9.Text = dt.Rows[0]["field9"].ToString();
                        }
                        else
                        {
                            txtfield1.Text = "";
                            txtfield2.Text = "";
                            txtfield3.Text = "";
                            txtfield4.Text = "";
                            txtfield5.Text = "";
                            txtfield6.Text = "";
                            txtfield7.Text = "";
                            txtfield8.Text = "";
                            txtfield9.Text = "";
                        }
                        f1.Visible = true;
                        txtfield1.Visible = true;
                        f2.Visible = true;
                        txtfield2.Visible = true;
                        f3.Visible = true;
                        txtfield3.Visible = true;
                        f4.Visible = true;
                        txtfield4.Visible = true;
                        f5.Visible = true;
                        txtfield5.Visible = true;
                        f6.Visible = true;
                        txtfield6.Visible = true;
                        f7.Visible = true;
                        txtfield7.Visible = true;
                        f8.Visible = true;
                        txtfield8.Visible = true;
                        f9.Visible = true;
                        txtfield9.Visible = true;
                        f10.Visible = false;
                        txtfield10.Visible = false;
                        f11.Visible = false;
                        txtfield11.Visible = false;
                        f12.Visible = false;
                        txtfield12.Visible = false;
                        f13.Visible = false;
                        txtfield13.Visible = false;
                        f14.Visible = false;
                        txtfield14.Visible = false;
                        f15.Visible = false;
                        txtfield15.Visible = false;
                    }
                    else if (cmbnooffields.Text == "10")
                    {
                        if (dt.Rows.Count > 0)
                        {
                            txtfield1.Text = dt.Rows[0]["field1"].ToString();
                            txtfield2.Text = dt.Rows[0]["field2"].ToString();
                            txtfield3.Text = dt.Rows[0]["field3"].ToString();
                            txtfield4.Text = dt.Rows[0]["field4"].ToString();
                            txtfield5.Text = dt.Rows[0]["field5"].ToString();
                            txtfield6.Text = dt.Rows[0]["field6"].ToString();
                            txtfield7.Text = dt.Rows[0]["field7"].ToString();
                            txtfield8.Text = dt.Rows[0]["field8"].ToString();
                            txtfield9.Text = dt.Rows[0]["field9"].ToString();
                            txtfield10.Text = dt.Rows[0]["field10"].ToString();
                        }
                        else
                        {
                            txtfield1.Text = "";
                            txtfield2.Text = "";
                            txtfield3.Text = "";
                            txtfield4.Text = "";
                            txtfield5.Text = "";
                            txtfield6.Text = "";
                            txtfield7.Text = "";
                            txtfield8.Text = "";
                            txtfield9.Text = "";
                            txtfield10.Text = "";
                        }
                        f1.Visible = true;
                        txtfield1.Visible = true;
                        f2.Visible = true;
                        txtfield2.Visible = true;
                        f3.Visible = true;
                        txtfield3.Visible = true;
                        f4.Visible = true;
                        txtfield4.Visible = true;
                        f5.Visible = true;
                        txtfield5.Visible = true;
                        f6.Visible = true;
                        txtfield6.Visible = true;
                        f7.Visible = true;
                        txtfield7.Visible = true;
                        f8.Visible = true;
                        txtfield8.Visible = true;
                        f9.Visible = true;
                        txtfield9.Visible = true;
                        f10.Visible = true;
                        txtfield10.Visible = true;
                        f11.Visible = false;
                        txtfield11.Visible = false;
                        f12.Visible = false;
                        txtfield12.Visible = false;
                        f13.Visible = false;
                        txtfield13.Visible = false;
                        f14.Visible = false;
                        txtfield14.Visible = false;
                        f15.Visible = false;
                        txtfield15.Visible = false;
                    }
                    else if (cmbnooffields.Text == "11")
                    {
                        if (dt.Rows.Count > 0)
                        {
                            txtfield1.Text = dt.Rows[0]["field1"].ToString();
                            txtfield2.Text = dt.Rows[0]["field2"].ToString();
                            txtfield3.Text = dt.Rows[0]["field3"].ToString();
                            txtfield4.Text = dt.Rows[0]["field4"].ToString();
                            txtfield5.Text = dt.Rows[0]["field5"].ToString();
                            txtfield6.Text = dt.Rows[0]["field6"].ToString();
                            txtfield7.Text = dt.Rows[0]["field7"].ToString();
                            txtfield8.Text = dt.Rows[0]["field8"].ToString();
                            txtfield9.Text = dt.Rows[0]["field9"].ToString();
                            txtfield10.Text = dt.Rows[0]["field10"].ToString();
                            txtfield11.Text = dt.Rows[0]["field11"].ToString();
                        }
                        else
                        {
                            txtfield1.Text = "";
                            txtfield2.Text = "";
                            txtfield3.Text = "";
                            txtfield4.Text = "";
                            txtfield5.Text = "";
                            txtfield6.Text = "";
                            txtfield7.Text = "";
                            txtfield8.Text = "";
                            txtfield9.Text = "";
                            txtfield10.Text = "";
                            txtfield11.Text = "";
                        }
                        f1.Visible = true;
                        txtfield1.Visible = true;
                        f2.Visible = true;
                        txtfield2.Visible = true;
                        f3.Visible = true;
                        txtfield3.Visible = true;
                        f4.Visible = true;
                        txtfield4.Visible = true;
                        f5.Visible = true;
                        txtfield5.Visible = true;
                        f6.Visible = true;
                        txtfield6.Visible = true;
                        f7.Visible = true;
                        txtfield7.Visible = true;
                        f8.Visible = true;
                        txtfield8.Visible = true;
                        f9.Visible = true;
                        txtfield9.Visible = true;
                        f10.Visible = true;
                        txtfield10.Visible = true;
                        f11.Visible = true;
                        txtfield11.Visible = true;
                        f12.Visible = false;
                        txtfield12.Visible = false;
                        f13.Visible = false;
                        txtfield13.Visible = false;
                        f14.Visible = false;
                        txtfield14.Visible = false;
                        f15.Visible = false;
                        txtfield15.Visible = false;
                    }
                    else if (cmbnooffields.Text == "12")
                    {
                        if (dt.Rows.Count > 0)
                        {
                            txtfield1.Text = dt.Rows[0]["field1"].ToString();
                            txtfield2.Text = dt.Rows[0]["field2"].ToString();
                            txtfield3.Text = dt.Rows[0]["field3"].ToString();
                            txtfield4.Text = dt.Rows[0]["field4"].ToString();
                            txtfield5.Text = dt.Rows[0]["field5"].ToString();
                            txtfield6.Text = dt.Rows[0]["field6"].ToString();
                            txtfield7.Text = dt.Rows[0]["field7"].ToString();
                            txtfield8.Text = dt.Rows[0]["field8"].ToString();
                            txtfield9.Text = dt.Rows[0]["field9"].ToString();
                            txtfield10.Text = dt.Rows[0]["field10"].ToString();
                            txtfield11.Text = dt.Rows[0]["field11"].ToString();
                            txtfield12.Text = dt.Rows[0]["field12"].ToString();
                        }
                        else
                        {
                            txtfield1.Text = "";
                            txtfield2.Text = "";
                            txtfield3.Text = "";
                            txtfield4.Text = "";
                            txtfield5.Text = "";
                            txtfield6.Text = "";
                            txtfield7.Text = "";
                            txtfield8.Text = "";
                            txtfield9.Text = "";
                            txtfield10.Text = "";
                            txtfield11.Text = "";
                            txtfield12.Text = "";
                        }
                        f1.Visible = true;
                        txtfield1.Visible = true;
                        f2.Visible = true;
                        txtfield2.Visible = true;
                        f3.Visible = true;
                        txtfield3.Visible = true;
                        f4.Visible = true;
                        txtfield4.Visible = true;
                        f5.Visible = true;
                        txtfield5.Visible = true;
                        f6.Visible = true;
                        txtfield6.Visible = true;
                        f7.Visible = true;
                        txtfield7.Visible = true;
                        f8.Visible = true;
                        txtfield8.Visible = true;
                        f9.Visible = true;
                        txtfield9.Visible = true;
                        f10.Visible = true;
                        txtfield10.Visible = true;
                        f11.Visible = true;
                        txtfield11.Visible = true;
                        f12.Visible = true;
                        txtfield12.Visible = true;
                        f13.Visible = false;
                        txtfield13.Visible = false;
                        f14.Visible = false;
                        txtfield14.Visible = false;
                        f15.Visible = false;
                        txtfield15.Visible = false;
                    }
                    else if (cmbnooffields.Text == "13")
                    {
                        if (dt.Rows.Count > 0)
                        {
                            txtfield1.Text = dt.Rows[0]["field1"].ToString();
                            txtfield2.Text = dt.Rows[0]["field2"].ToString();
                            txtfield3.Text = dt.Rows[0]["field3"].ToString();
                            txtfield4.Text = dt.Rows[0]["field4"].ToString();
                            txtfield5.Text = dt.Rows[0]["field5"].ToString();
                            txtfield6.Text = dt.Rows[0]["field6"].ToString();
                            txtfield7.Text = dt.Rows[0]["field7"].ToString();
                            txtfield8.Text = dt.Rows[0]["field8"].ToString();
                            txtfield9.Text = dt.Rows[0]["field9"].ToString();
                            txtfield10.Text = dt.Rows[0]["field10"].ToString();
                            txtfield11.Text = dt.Rows[0]["field11"].ToString();
                            txtfield12.Text = dt.Rows[0]["field12"].ToString();
                            txtfield13.Text = dt.Rows[0]["field13"].ToString();
                        }
                        else
                        {
                            txtfield1.Text = "";
                            txtfield2.Text = "";
                            txtfield3.Text = "";
                            txtfield4.Text = "";
                            txtfield5.Text = "";
                            txtfield6.Text = "";
                            txtfield7.Text = "";
                            txtfield8.Text = "";
                            txtfield9.Text = "";
                            txtfield10.Text = "";
                            txtfield11.Text = "";
                            txtfield12.Text = "";
                            txtfield13.Text = "";
                        }
                        f1.Visible = true;
                        txtfield1.Visible = true;
                        f2.Visible = true;
                        txtfield2.Visible = true;
                        f3.Visible = true;
                        txtfield3.Visible = true;
                        f4.Visible = true;
                        txtfield4.Visible = true;
                        f5.Visible = true;
                        txtfield5.Visible = true;
                        f6.Visible = true;
                        txtfield6.Visible = true;
                        f7.Visible = true;
                        txtfield7.Visible = true;
                        f8.Visible = true;
                        txtfield8.Visible = true;
                        f9.Visible = true;
                        txtfield9.Visible = true;
                        f10.Visible = true;
                        txtfield10.Visible = true;
                        f11.Visible = true;
                        txtfield11.Visible = true;
                        f12.Visible = true;
                        txtfield12.Visible = true;
                        f13.Visible = true;
                        txtfield13.Visible = true;
                        f14.Visible = false;
                        txtfield14.Visible = false;
                        f15.Visible = false;
                        txtfield15.Visible = false;
                    }
                    else if (cmbnooffields.Text == "14")
                    {
                        if (dt.Rows.Count > 0)
                        {
                            txtfield1.Text = dt.Rows[0]["field1"].ToString();
                            txtfield2.Text = dt.Rows[0]["field2"].ToString();
                            txtfield3.Text = dt.Rows[0]["field3"].ToString();
                            txtfield4.Text = dt.Rows[0]["field4"].ToString();
                            txtfield5.Text = dt.Rows[0]["field5"].ToString();
                            txtfield6.Text = dt.Rows[0]["field6"].ToString();
                            txtfield7.Text = dt.Rows[0]["field7"].ToString();
                            txtfield8.Text = dt.Rows[0]["field8"].ToString();
                            txtfield9.Text = dt.Rows[0]["field9"].ToString();
                            txtfield10.Text = dt.Rows[0]["field10"].ToString();
                            txtfield11.Text = dt.Rows[0]["field11"].ToString();
                            txtfield12.Text = dt.Rows[0]["field12"].ToString();
                            txtfield13.Text = dt.Rows[0]["field13"].ToString();
                            txtfield14.Text = dt.Rows[0]["field14"].ToString();
                        }
                        else
                        {
                            txtfield1.Text = "";
                            txtfield2.Text = "";
                            txtfield3.Text = "";
                            txtfield4.Text = "";
                            txtfield5.Text = "";
                            txtfield6.Text = "";
                            txtfield7.Text = "";
                            txtfield8.Text = "";
                            txtfield9.Text = "";
                            txtfield10.Text = "";
                            txtfield11.Text = "";
                            txtfield12.Text = "";
                            txtfield13.Text = "";
                            txtfield14.Text = "";
                        }
                        f1.Visible = true;
                        txtfield1.Visible = true;
                        f2.Visible = true;
                        txtfield2.Visible = true;
                        f3.Visible = true;
                        txtfield3.Visible = true;
                        f4.Visible = true;
                        txtfield4.Visible = true;
                        f5.Visible = true;
                        txtfield5.Visible = true;
                        f6.Visible = true;
                        txtfield6.Visible = true;
                        f7.Visible = true;
                        txtfield7.Visible = true;
                        f8.Visible = true;
                        txtfield8.Visible = true;
                        f9.Visible = true;
                        txtfield9.Visible = true;
                        f10.Visible = true;
                        txtfield10.Visible = true;
                        f11.Visible = true;
                        txtfield11.Visible = true;
                        f12.Visible = true;
                        txtfield12.Visible = true;
                        f13.Visible = true;
                        txtfield13.Visible = true;
                        f14.Visible = true;
                        txtfield14.Visible = true;
                        f15.Visible = false;
                        txtfield15.Visible = false;
                    }
                    else if (cmbnooffields.Text == "15")
                    {
                        if (dt.Rows.Count > 0)
                        {
                            txtfield1.Text = dt.Rows[0]["field1"].ToString();
                            txtfield2.Text = dt.Rows[0]["field2"].ToString();
                            txtfield3.Text = dt.Rows[0]["field3"].ToString();
                            txtfield4.Text = dt.Rows[0]["field4"].ToString();
                            txtfield5.Text = dt.Rows[0]["field5"].ToString();
                            txtfield6.Text = dt.Rows[0]["field6"].ToString();
                            txtfield7.Text = dt.Rows[0]["field7"].ToString();
                            txtfield8.Text = dt.Rows[0]["field8"].ToString();
                            txtfield9.Text = dt.Rows[0]["field9"].ToString();
                            txtfield10.Text = dt.Rows[0]["field10"].ToString();
                            txtfield11.Text = dt.Rows[0]["field11"].ToString();
                            txtfield12.Text = dt.Rows[0]["field12"].ToString();
                            txtfield13.Text = dt.Rows[0]["field13"].ToString();
                            txtfield14.Text = dt.Rows[0]["field14"].ToString();
                            txtfield15.Text = dt.Rows[0]["field15"].ToString();
                        }
                        else
                        {
                            txtfield1.Text = "";
                            txtfield2.Text = "";
                            txtfield3.Text = "";
                            txtfield4.Text = "";
                            txtfield5.Text = "";
                            txtfield6.Text = "";
                            txtfield7.Text = "";
                            txtfield8.Text = "";
                            txtfield9.Text = "";
                            txtfield10.Text = "";
                            txtfield11.Text = "";
                            txtfield12.Text = "";
                            txtfield13.Text = "";
                            txtfield14.Text = "";
                            txtfield15.Text = "";
                        }
                        f1.Visible = true;
                        txtfield1.Visible = true;
                        f2.Visible = true;
                        txtfield2.Visible = true;
                        f3.Visible = true;
                        txtfield3.Visible = true;
                        f4.Visible = true;
                        txtfield4.Visible = true;
                        f5.Visible = true;
                        txtfield5.Visible = true;
                        f6.Visible = true;
                        txtfield6.Visible = true;
                        f7.Visible = true;
                        txtfield7.Visible = true;
                        f8.Visible = true;
                        txtfield8.Visible = true;
                        f9.Visible = true;
                        txtfield9.Visible = true;
                        f10.Visible = true;
                        txtfield10.Visible = true;
                        f11.Visible = true;
                        txtfield11.Visible = true;
                        f12.Visible = true;
                        txtfield12.Visible = true;
                        f13.Visible = true;
                        txtfield13.Visible = true;
                        f14.Visible = true;
                        txtfield14.Visible = true;
                        f15.Visible = true;
                        txtfield15.Visible = true;
                    }
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void cmbnooffields_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbnooffields.Items.Count; i++)
                {
                    s = cmbnooffields.GetItemText(cmbnooffields.Items[i]);
                    if (s == cmbnooffields.Text)
                    {
                        inList = true;
                        cmbnooffields.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbnooffields.Text = "";
                }
                dt = conn.getdataset("select * from Additional where MasterType='" + cmbmastertype.Text + "'");
                if (cmbnooffields.Text == "1")
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtfield1.Text = dt.Rows[0]["field1"].ToString();
                    }
                    else
                    {
                        txtfield1.Text = "";
                    }
                    f1.Visible = true;
                    txtfield1.Visible = true;
                    f2.Visible = false;
                    txtfield2.Visible = false;
                    f3.Visible = false;
                    txtfield3.Visible = false;
                    f4.Visible = false;
                    txtfield4.Visible = false;
                    f5.Visible = false;
                    txtfield5.Visible = false;
                    f6.Visible = false;
                    txtfield6.Visible = false;
                    f7.Visible = false;
                    txtfield7.Visible = false;
                    f8.Visible = false;
                    txtfield8.Visible = false;
                    f9.Visible = false;
                    txtfield9.Visible = false;
                    f10.Visible = false;
                    txtfield10.Visible = false;
                    f11.Visible = false;
                    txtfield11.Visible = false;
                    f12.Visible = false;
                    txtfield12.Visible = false;
                    f13.Visible = false;
                    txtfield13.Visible = false;
                    f14.Visible = false;
                    txtfield14.Visible = false;
                    f15.Visible = false;
                    txtfield15.Visible = false;
                }
                else if (cmbnooffields.Text == "2")
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtfield1.Text = dt.Rows[0]["field1"].ToString();
                        txtfield2.Text = dt.Rows[0]["field2"].ToString();
                    }
                    else
                    {
                        txtfield1.Text = "";
                        txtfield2.Text = "";
                    }
                    f1.Visible = true;
                    txtfield1.Visible = true;
                    f2.Visible = true;
                    txtfield2.Visible = true;
                    f3.Visible = false;
                    txtfield3.Visible = false;
                    f4.Visible = false;
                    txtfield4.Visible = false;
                    f5.Visible = false;
                    txtfield5.Visible = false;
                    f6.Visible = false;
                    txtfield6.Visible = false;
                    f7.Visible = false;
                    txtfield7.Visible = false;
                    f8.Visible = false;
                    txtfield8.Visible = false;
                    f9.Visible = false;
                    txtfield9.Visible = false;
                    f10.Visible = false;
                    txtfield10.Visible = false;
                    f11.Visible = false;
                    txtfield11.Visible = false;
                    f12.Visible = false;
                    txtfield12.Visible = false;
                    f13.Visible = false;
                    txtfield13.Visible = false;
                    f14.Visible = false;
                    txtfield14.Visible = false;
                    f15.Visible = false;
                    txtfield15.Visible = false;
                }
                else if (cmbnooffields.Text == "3")
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtfield1.Text = dt.Rows[0]["field1"].ToString();
                        txtfield2.Text = dt.Rows[0]["field2"].ToString();
                        txtfield3.Text = dt.Rows[0]["field3"].ToString();
                    }
                    else
                    {
                        txtfield1.Text = "";
                        txtfield2.Text = "";
                        txtfield3.Text = "";
                    }
                    f1.Visible = true;
                    txtfield1.Visible = true;
                    f2.Visible = true;
                    txtfield2.Visible = true;
                    f3.Visible = true;
                    txtfield3.Visible = true;
                    f4.Visible = false;
                    txtfield4.Visible = false;
                    f5.Visible = false;
                    txtfield5.Visible = false;
                    f6.Visible = false;
                    txtfield6.Visible = false;
                    f7.Visible = false;
                    txtfield7.Visible = false;
                    f8.Visible = false;
                    txtfield8.Visible = false;
                    f9.Visible = false;
                    txtfield9.Visible = false;
                    f10.Visible = false;
                    txtfield10.Visible = false;
                    f11.Visible = false;
                    txtfield11.Visible = false;
                    f12.Visible = false;
                    txtfield12.Visible = false;
                    f13.Visible = false;
                    txtfield13.Visible = false;
                    f14.Visible = false;
                    txtfield14.Visible = false;
                    f15.Visible = false;
                    txtfield15.Visible = false;
                }
                else if (cmbnooffields.Text == "4")
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtfield1.Text = dt.Rows[0]["field1"].ToString();
                        txtfield2.Text = dt.Rows[0]["field2"].ToString();
                        txtfield3.Text = dt.Rows[0]["field3"].ToString();
                        txtfield4.Text = dt.Rows[0]["field4"].ToString();
                    }
                    else
                    {
                        txtfield1.Text = "";
                        txtfield2.Text = "";
                        txtfield3.Text = "";
                        txtfield4.Text = "";
                    }
                    f1.Visible = true;
                    txtfield1.Visible = true;
                    f2.Visible = true;
                    txtfield2.Visible = true;
                    f3.Visible = true;
                    txtfield3.Visible = true;
                    f4.Visible = true;
                    txtfield4.Visible = true;
                    f5.Visible = false;
                    txtfield5.Visible = false;
                    f6.Visible = false;
                    txtfield6.Visible = false;
                    f7.Visible = false;
                    txtfield7.Visible = false;
                    f8.Visible = false;
                    txtfield8.Visible = false;
                    f9.Visible = false;
                    txtfield9.Visible = false;
                    f10.Visible = false;
                    txtfield10.Visible = false;
                    f11.Visible = false;
                    txtfield11.Visible = false;
                    f12.Visible = false;
                    txtfield12.Visible = false;
                    f13.Visible = false;
                    txtfield13.Visible = false;
                    f14.Visible = false;
                    txtfield14.Visible = false;
                    f15.Visible = false;
                    txtfield15.Visible = false;
                }
                else if (cmbnooffields.Text == "5")
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtfield1.Text = dt.Rows[0]["field1"].ToString();
                        txtfield2.Text = dt.Rows[0]["field2"].ToString();
                        txtfield3.Text = dt.Rows[0]["field3"].ToString();
                        txtfield4.Text = dt.Rows[0]["field4"].ToString();
                        txtfield5.Text = dt.Rows[0]["field5"].ToString();
                    }
                    else
                    {
                        txtfield1.Text = "";
                        txtfield2.Text = "";
                        txtfield3.Text = "";
                        txtfield4.Text = "";
                        txtfield5.Text = "";
                    }
                    f1.Visible = true;
                    txtfield1.Visible = true;
                    f2.Visible = true;
                    txtfield2.Visible = true;
                    f3.Visible = true;
                    txtfield3.Visible = true;
                    f4.Visible = true;
                    txtfield4.Visible = true;
                    f5.Visible = true;
                    txtfield5.Visible = true;
                    f6.Visible = false;
                    txtfield6.Visible = false;
                    f7.Visible = false;
                    txtfield7.Visible = false;
                    f8.Visible = false;
                    txtfield8.Visible = false;
                    f9.Visible = false;
                    txtfield9.Visible = false;
                    f10.Visible = false;
                    txtfield10.Visible = false;
                    f11.Visible = false;
                    txtfield11.Visible = false;
                    f12.Visible = false;
                    txtfield12.Visible = false;
                    f13.Visible = false;
                    txtfield13.Visible = false;
                    f14.Visible = false;
                    txtfield14.Visible = false;
                    f15.Visible = false;
                    txtfield15.Visible = false;
                }
                else if (cmbnooffields.Text == "6")
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtfield1.Text = dt.Rows[0]["field1"].ToString();
                        txtfield2.Text = dt.Rows[0]["field2"].ToString();
                        txtfield3.Text = dt.Rows[0]["field3"].ToString();
                        txtfield4.Text = dt.Rows[0]["field4"].ToString();
                        txtfield5.Text = dt.Rows[0]["field5"].ToString();
                        txtfield6.Text = dt.Rows[0]["field6"].ToString();
                    }
                    else
                    {
                        txtfield1.Text = "";
                        txtfield2.Text = "";
                        txtfield3.Text = "";
                        txtfield4.Text = "";
                        txtfield5.Text = "";
                        txtfield6.Text = "";
                    }
                    f1.Visible = true;
                    txtfield1.Visible = true;
                    f2.Visible = true;
                    txtfield2.Visible = true;
                    f3.Visible = true;
                    txtfield3.Visible = true;
                    f4.Visible = true;
                    txtfield4.Visible = true;
                    f5.Visible = true;
                    txtfield5.Visible = true;
                    f6.Visible = true;
                    txtfield6.Visible = true;
                    f7.Visible = false;
                    txtfield7.Visible = false;
                    f8.Visible = false;
                    txtfield8.Visible = false;
                    f9.Visible = false;
                    txtfield9.Visible = false;
                    f10.Visible = false;
                    txtfield10.Visible = false;
                    f11.Visible = false;
                    txtfield11.Visible = false;
                    f12.Visible = false;
                    txtfield12.Visible = false;
                    f13.Visible = false;
                    txtfield13.Visible = false;
                    f14.Visible = false;
                    txtfield14.Visible = false;
                    f15.Visible = false;
                    txtfield15.Visible = false;
                }
                else if (cmbnooffields.Text == "7")
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtfield1.Text = dt.Rows[0]["field1"].ToString();
                        txtfield2.Text = dt.Rows[0]["field2"].ToString();
                        txtfield3.Text = dt.Rows[0]["field3"].ToString();
                        txtfield4.Text = dt.Rows[0]["field4"].ToString();
                        txtfield5.Text = dt.Rows[0]["field5"].ToString();
                        txtfield6.Text = dt.Rows[0]["field6"].ToString();
                        txtfield7.Text = dt.Rows[0]["field7"].ToString();
                    }
                    else
                    {
                        txtfield1.Text = "";
                        txtfield2.Text = "";
                        txtfield3.Text = "";
                        txtfield4.Text = "";
                        txtfield5.Text = "";
                        txtfield6.Text = "";
                        txtfield7.Text = "";
                    }
                    f1.Visible = true;
                    txtfield1.Visible = true;
                    f2.Visible = true;
                    txtfield2.Visible = true;
                    f3.Visible = true;
                    txtfield3.Visible = true;
                    f4.Visible = true;
                    txtfield4.Visible = true;
                    f5.Visible = true;
                    txtfield5.Visible = true;
                    f6.Visible = true;
                    txtfield6.Visible = true;
                    f7.Visible = true;
                    txtfield7.Visible = true;
                    f8.Visible = false;
                    txtfield8.Visible = false;
                    f9.Visible = false;
                    txtfield9.Visible = false;
                    f10.Visible = false;
                    txtfield10.Visible = false;
                    f11.Visible = false;
                    txtfield11.Visible = false;
                    f12.Visible = false;
                    txtfield12.Visible = false;
                    f13.Visible = false;
                    txtfield13.Visible = false;
                    f14.Visible = false;
                    txtfield14.Visible = false;
                    f15.Visible = false;
                    txtfield15.Visible = false;
                }
                else if (cmbnooffields.Text == "8")
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtfield1.Text = dt.Rows[0]["field1"].ToString();
                        txtfield2.Text = dt.Rows[0]["field2"].ToString();
                        txtfield3.Text = dt.Rows[0]["field3"].ToString();
                        txtfield4.Text = dt.Rows[0]["field4"].ToString();
                        txtfield5.Text = dt.Rows[0]["field5"].ToString();
                        txtfield6.Text = dt.Rows[0]["field6"].ToString();
                        txtfield7.Text = dt.Rows[0]["field7"].ToString();
                        txtfield8.Text = dt.Rows[0]["field8"].ToString();
                    }
                    else
                    {
                        txtfield1.Text = "";
                        txtfield2.Text = "";
                        txtfield3.Text = "";
                        txtfield4.Text = "";
                        txtfield5.Text = "";
                        txtfield6.Text = "";
                        txtfield7.Text = "";
                        txtfield8.Text = "";
                    }
                    f1.Visible = true;
                    txtfield1.Visible = true;
                    f2.Visible = true;
                    txtfield2.Visible = true;
                    f3.Visible = true;
                    txtfield3.Visible = true;
                    f4.Visible = true;
                    txtfield4.Visible = true;
                    f5.Visible = true;
                    txtfield5.Visible = true;
                    f6.Visible = true;
                    txtfield6.Visible = true;
                    f7.Visible = true;
                    txtfield7.Visible = true;
                    f8.Visible = true;
                    txtfield8.Visible = true;
                    f9.Visible = false;
                    txtfield9.Visible = false;
                    f10.Visible = false;
                    txtfield10.Visible = false;
                    f11.Visible = false;
                    txtfield11.Visible = false;
                    f12.Visible = false;
                    txtfield12.Visible = false;
                    f13.Visible = false;
                    txtfield13.Visible = false;
                    f14.Visible = false;
                    txtfield14.Visible = false;
                    f15.Visible = false;
                    txtfield15.Visible = false;
                }
                else if (cmbnooffields.Text == "9")
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtfield1.Text = dt.Rows[0]["field1"].ToString();
                        txtfield2.Text = dt.Rows[0]["field2"].ToString();
                        txtfield3.Text = dt.Rows[0]["field3"].ToString();
                        txtfield4.Text = dt.Rows[0]["field4"].ToString();
                        txtfield5.Text = dt.Rows[0]["field5"].ToString();
                        txtfield6.Text = dt.Rows[0]["field6"].ToString();
                        txtfield7.Text = dt.Rows[0]["field7"].ToString();
                        txtfield8.Text = dt.Rows[0]["field8"].ToString();
                        txtfield9.Text = dt.Rows[0]["field9"].ToString();
                    }
                    else
                    {
                        txtfield1.Text = "";
                        txtfield2.Text = "";
                        txtfield3.Text = "";
                        txtfield4.Text = "";
                        txtfield5.Text = "";
                        txtfield6.Text = "";
                        txtfield7.Text = "";
                        txtfield8.Text = "";
                        txtfield9.Text = "";
                    }
                    f1.Visible = true;
                    txtfield1.Visible = true;
                    f2.Visible = true;
                    txtfield2.Visible = true;
                    f3.Visible = true;
                    txtfield3.Visible = true;
                    f4.Visible = true;
                    txtfield4.Visible = true;
                    f5.Visible = true;
                    txtfield5.Visible = true;
                    f6.Visible = true;
                    txtfield6.Visible = true;
                    f7.Visible = true;
                    txtfield7.Visible = true;
                    f8.Visible = true;
                    txtfield8.Visible = true;
                    f9.Visible = true;
                    txtfield9.Visible = true;
                    f10.Visible = false;
                    txtfield10.Visible = false;
                    f11.Visible = false;
                    txtfield11.Visible = false;
                    f12.Visible = false;
                    txtfield12.Visible = false;
                    f13.Visible = false;
                    txtfield13.Visible = false;
                    f14.Visible = false;
                    txtfield14.Visible = false;
                    f15.Visible = false;
                    txtfield15.Visible = false;
                }
                else if (cmbnooffields.Text == "10")
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtfield1.Text = dt.Rows[0]["field1"].ToString();
                        txtfield2.Text = dt.Rows[0]["field2"].ToString();
                        txtfield3.Text = dt.Rows[0]["field3"].ToString();
                        txtfield4.Text = dt.Rows[0]["field4"].ToString();
                        txtfield5.Text = dt.Rows[0]["field5"].ToString();
                        txtfield6.Text = dt.Rows[0]["field6"].ToString();
                        txtfield7.Text = dt.Rows[0]["field7"].ToString();
                        txtfield8.Text = dt.Rows[0]["field8"].ToString();
                        txtfield9.Text = dt.Rows[0]["field9"].ToString();
                        txtfield10.Text = dt.Rows[0]["field10"].ToString();
                    }
                    else
                    {
                        txtfield1.Text = "";
                        txtfield2.Text = "";
                        txtfield3.Text = "";
                        txtfield4.Text = "";
                        txtfield5.Text = "";
                        txtfield6.Text = "";
                        txtfield7.Text = "";
                        txtfield8.Text = "";
                        txtfield9.Text = "";
                        txtfield10.Text = "";
                    }
                    f1.Visible = true;
                    txtfield1.Visible = true;
                    f2.Visible = true;
                    txtfield2.Visible = true;
                    f3.Visible = true;
                    txtfield3.Visible = true;
                    f4.Visible = true;
                    txtfield4.Visible = true;
                    f5.Visible = true;
                    txtfield5.Visible = true;
                    f6.Visible = true;
                    txtfield6.Visible = true;
                    f7.Visible = true;
                    txtfield7.Visible = true;
                    f8.Visible = true;
                    txtfield8.Visible = true;
                    f9.Visible = true;
                    txtfield9.Visible = true;
                    f10.Visible = true;
                    txtfield10.Visible = true;
                    f11.Visible = false;
                    txtfield11.Visible = false;
                    f12.Visible = false;
                    txtfield12.Visible = false;
                    f13.Visible = false;
                    txtfield13.Visible = false;
                    f14.Visible = false;
                    txtfield14.Visible = false;
                    f15.Visible = false;
                    txtfield15.Visible = false;
                }
                else if (cmbnooffields.Text == "11")
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtfield1.Text = dt.Rows[0]["field1"].ToString();
                        txtfield2.Text = dt.Rows[0]["field2"].ToString();
                        txtfield3.Text = dt.Rows[0]["field3"].ToString();
                        txtfield4.Text = dt.Rows[0]["field4"].ToString();
                        txtfield5.Text = dt.Rows[0]["field5"].ToString();
                        txtfield6.Text = dt.Rows[0]["field6"].ToString();
                        txtfield7.Text = dt.Rows[0]["field7"].ToString();
                        txtfield8.Text = dt.Rows[0]["field8"].ToString();
                        txtfield9.Text = dt.Rows[0]["field9"].ToString();
                        txtfield10.Text = dt.Rows[0]["field10"].ToString();
                        txtfield11.Text = dt.Rows[0]["field11"].ToString();
                    }
                    else
                    {
                        txtfield1.Text = "";
                        txtfield2.Text = "";
                        txtfield3.Text = "";
                        txtfield4.Text = "";
                        txtfield5.Text = "";
                        txtfield6.Text = "";
                        txtfield7.Text = "";
                        txtfield8.Text = "";
                        txtfield9.Text = "";
                        txtfield10.Text = "";
                        txtfield11.Text = "";
                    }
                    f1.Visible = true;
                    txtfield1.Visible = true;
                    f2.Visible = true;
                    txtfield2.Visible = true;
                    f3.Visible = true;
                    txtfield3.Visible = true;
                    f4.Visible = true;
                    txtfield4.Visible = true;
                    f5.Visible = true;
                    txtfield5.Visible = true;
                    f6.Visible = true;
                    txtfield6.Visible = true;
                    f7.Visible = true;
                    txtfield7.Visible = true;
                    f8.Visible = true;
                    txtfield8.Visible = true;
                    f9.Visible = true;
                    txtfield9.Visible = true;
                    f10.Visible = true;
                    txtfield10.Visible = true;
                    f11.Visible = true;
                    txtfield11.Visible = true;
                    f12.Visible = false;
                    txtfield12.Visible = false;
                    f13.Visible = false;
                    txtfield13.Visible = false;
                    f14.Visible = false;
                    txtfield14.Visible = false;
                    f15.Visible = false;
                    txtfield15.Visible = false;
                }
                else if (cmbnooffields.Text == "12")
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtfield1.Text = dt.Rows[0]["field1"].ToString();
                        txtfield2.Text = dt.Rows[0]["field2"].ToString();
                        txtfield3.Text = dt.Rows[0]["field3"].ToString();
                        txtfield4.Text = dt.Rows[0]["field4"].ToString();
                        txtfield5.Text = dt.Rows[0]["field5"].ToString();
                        txtfield6.Text = dt.Rows[0]["field6"].ToString();
                        txtfield7.Text = dt.Rows[0]["field7"].ToString();
                        txtfield8.Text = dt.Rows[0]["field8"].ToString();
                        txtfield9.Text = dt.Rows[0]["field9"].ToString();
                        txtfield10.Text = dt.Rows[0]["field10"].ToString();
                        txtfield11.Text = dt.Rows[0]["field11"].ToString();
                        txtfield12.Text = dt.Rows[0]["field12"].ToString();
                    }
                    else
                    {
                        txtfield1.Text = "";
                        txtfield2.Text = "";
                        txtfield3.Text = "";
                        txtfield4.Text = "";
                        txtfield5.Text = "";
                        txtfield6.Text = "";
                        txtfield7.Text = "";
                        txtfield8.Text = "";
                        txtfield9.Text = "";
                        txtfield10.Text = "";
                        txtfield11.Text = "";
                        txtfield12.Text = "";
                    }
                    f1.Visible = true;
                    txtfield1.Visible = true;
                    f2.Visible = true;
                    txtfield2.Visible = true;
                    f3.Visible = true;
                    txtfield3.Visible = true;
                    f4.Visible = true;
                    txtfield4.Visible = true;
                    f5.Visible = true;
                    txtfield5.Visible = true;
                    f6.Visible = true;
                    txtfield6.Visible = true;
                    f7.Visible = true;
                    txtfield7.Visible = true;
                    f8.Visible = true;
                    txtfield8.Visible = true;
                    f9.Visible = true;
                    txtfield9.Visible = true;
                    f10.Visible = true;
                    txtfield10.Visible = true;
                    f11.Visible = true;
                    txtfield11.Visible = true;
                    f12.Visible = true;
                    txtfield12.Visible = true;
                    f13.Visible = false;
                    txtfield13.Visible = false;
                    f14.Visible = false;
                    txtfield14.Visible = false;
                    f15.Visible = false;
                    txtfield15.Visible = false;
                }
                else if (cmbnooffields.Text == "13")
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtfield1.Text = dt.Rows[0]["field1"].ToString();
                        txtfield2.Text = dt.Rows[0]["field2"].ToString();
                        txtfield3.Text = dt.Rows[0]["field3"].ToString();
                        txtfield4.Text = dt.Rows[0]["field4"].ToString();
                        txtfield5.Text = dt.Rows[0]["field5"].ToString();
                        txtfield6.Text = dt.Rows[0]["field6"].ToString();
                        txtfield7.Text = dt.Rows[0]["field7"].ToString();
                        txtfield8.Text = dt.Rows[0]["field8"].ToString();
                        txtfield9.Text = dt.Rows[0]["field9"].ToString();
                        txtfield10.Text = dt.Rows[0]["field10"].ToString();
                        txtfield11.Text = dt.Rows[0]["field11"].ToString();
                        txtfield12.Text = dt.Rows[0]["field12"].ToString();
                        txtfield13.Text = dt.Rows[0]["field13"].ToString();
                    }
                    else
                    {
                        txtfield1.Text = "";
                        txtfield2.Text = "";
                        txtfield3.Text = "";
                        txtfield4.Text = "";
                        txtfield5.Text = "";
                        txtfield6.Text = "";
                        txtfield7.Text = "";
                        txtfield8.Text = "";
                        txtfield9.Text = "";
                        txtfield10.Text = "";
                        txtfield11.Text = "";
                        txtfield12.Text = "";
                        txtfield13.Text = "";
                    }
                    f1.Visible = true;
                    txtfield1.Visible = true;
                    f2.Visible = true;
                    txtfield2.Visible = true;
                    f3.Visible = true;
                    txtfield3.Visible = true;
                    f4.Visible = true;
                    txtfield4.Visible = true;
                    f5.Visible = true;
                    txtfield5.Visible = true;
                    f6.Visible = true;
                    txtfield6.Visible = true;
                    f7.Visible = true;
                    txtfield7.Visible = true;
                    f8.Visible = true;
                    txtfield8.Visible = true;
                    f9.Visible = true;
                    txtfield9.Visible = true;
                    f10.Visible = true;
                    txtfield10.Visible = true;
                    f11.Visible = true;
                    txtfield11.Visible = true;
                    f12.Visible = true;
                    txtfield12.Visible = true;
                    f13.Visible = true;
                    txtfield13.Visible = true;
                    f14.Visible = false;
                    txtfield14.Visible = false;
                    f15.Visible = false;
                    txtfield15.Visible = false;
                }
                else if (cmbnooffields.Text == "14")
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtfield1.Text = dt.Rows[0]["field1"].ToString();
                        txtfield2.Text = dt.Rows[0]["field2"].ToString();
                        txtfield3.Text = dt.Rows[0]["field3"].ToString();
                        txtfield4.Text = dt.Rows[0]["field4"].ToString();
                        txtfield5.Text = dt.Rows[0]["field5"].ToString();
                        txtfield6.Text = dt.Rows[0]["field6"].ToString();
                        txtfield7.Text = dt.Rows[0]["field7"].ToString();
                        txtfield8.Text = dt.Rows[0]["field8"].ToString();
                        txtfield9.Text = dt.Rows[0]["field9"].ToString();
                        txtfield10.Text = dt.Rows[0]["field10"].ToString();
                        txtfield11.Text = dt.Rows[0]["field11"].ToString();
                        txtfield12.Text = dt.Rows[0]["field12"].ToString();
                        txtfield13.Text = dt.Rows[0]["field13"].ToString();
                        txtfield14.Text = dt.Rows[0]["field14"].ToString();
                    }
                    else
                    {
                        txtfield1.Text = "";
                        txtfield2.Text = "";
                        txtfield3.Text = "";
                        txtfield4.Text = "";
                        txtfield5.Text = "";
                        txtfield6.Text = "";
                        txtfield7.Text = "";
                        txtfield8.Text = "";
                        txtfield9.Text = "";
                        txtfield10.Text = "";
                        txtfield11.Text = "";
                        txtfield12.Text = "";
                        txtfield13.Text = "";
                        txtfield14.Text = "";
                    }
                    f1.Visible = true;
                    txtfield1.Visible = true;
                    f2.Visible = true;
                    txtfield2.Visible = true;
                    f3.Visible = true;
                    txtfield3.Visible = true;
                    f4.Visible = true;
                    txtfield4.Visible = true;
                    f5.Visible = true;
                    txtfield5.Visible = true;
                    f6.Visible = true;
                    txtfield6.Visible = true;
                    f7.Visible = true;
                    txtfield7.Visible = true;
                    f8.Visible = true;
                    txtfield8.Visible = true;
                    f9.Visible = true;
                    txtfield9.Visible = true;
                    f10.Visible = true;
                    txtfield10.Visible = true;
                    f11.Visible = true;
                    txtfield11.Visible = true;
                    f12.Visible = true;
                    txtfield12.Visible = true;
                    f13.Visible = true;
                    txtfield13.Visible = true;
                    f14.Visible = true;
                    txtfield14.Visible = true;
                    f15.Visible = false;
                    txtfield15.Visible = false;
                }
                else if (cmbnooffields.Text == "15")
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtfield1.Text = dt.Rows[0]["field1"].ToString();
                        txtfield2.Text = dt.Rows[0]["field2"].ToString();
                        txtfield3.Text = dt.Rows[0]["field3"].ToString();
                        txtfield4.Text = dt.Rows[0]["field4"].ToString();
                        txtfield5.Text = dt.Rows[0]["field5"].ToString();
                        txtfield6.Text = dt.Rows[0]["field6"].ToString();
                        txtfield7.Text = dt.Rows[0]["field7"].ToString();
                        txtfield8.Text = dt.Rows[0]["field8"].ToString();
                        txtfield9.Text = dt.Rows[0]["field9"].ToString();
                        txtfield10.Text = dt.Rows[0]["field10"].ToString();
                        txtfield11.Text = dt.Rows[0]["field11"].ToString();
                        txtfield12.Text = dt.Rows[0]["field12"].ToString();
                        txtfield13.Text = dt.Rows[0]["field13"].ToString();
                        txtfield14.Text = dt.Rows[0]["field14"].ToString();
                        txtfield15.Text = dt.Rows[0]["field15"].ToString();
                    }
                    else
                    {
                        txtfield1.Text = "";
                        txtfield2.Text = "";
                        txtfield3.Text = "";
                        txtfield4.Text = "";
                        txtfield5.Text = "";
                        txtfield6.Text = "";
                        txtfield7.Text = "";
                        txtfield8.Text = "";
                        txtfield9.Text = "";
                        txtfield10.Text = "";
                        txtfield11.Text = "";
                        txtfield12.Text = "";
                        txtfield13.Text = "";
                        txtfield14.Text = "";
                        txtfield15.Text = "";
                    }
                    f1.Visible = true;
                    txtfield1.Visible = true;
                    f2.Visible = true;
                    txtfield2.Visible = true;
                    f3.Visible = true;
                    txtfield3.Visible = true;
                    f4.Visible = true;
                    txtfield4.Visible = true;
                    f5.Visible = true;
                    txtfield5.Visible = true;
                    f6.Visible = true;
                    txtfield6.Visible = true;
                    f7.Visible = true;
                    txtfield7.Visible = true;
                    f8.Visible = true;
                    txtfield8.Visible = true;
                    f9.Visible = true;
                    txtfield9.Visible = true;
                    f10.Visible = true;
                    txtfield10.Visible = true;
                    f11.Visible = true;
                    txtfield11.Visible = true;
                    f12.Visible = true;
                    txtfield12.Visible = true;
                    f13.Visible = true;
                    txtfield13.Visible = true;
                    f14.Visible = true;
                    txtfield14.Visible = true;
                    f15.Visible = true;
                    txtfield15.Visible = true;
                }
                Button18.Focus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }
        public void clearall()
        {
            txtfield1.Text = "";
            txtfield2.Text = "";
            txtfield3.Text = "";
            txtfield4.Text = "";
            txtfield5.Text = "";
            txtfield6.Text = "";
            txtfield7.Text = "";
            txtfield8.Text = "";
            txtfield9.Text = "";
            txtfield10.Text = "";
            txtfield11.Text = "";
            txtfield12.Text = "";
            txtfield13.Text = "";
            txtfield14.Text = "";
            txtfield15.Text = "";
        }
        private void Button18_Click(object sender, EventArgs e)
        {
            #region
            try
            {
                if (cmbmastertype.Text == "Quick Payment")
                {
                    //Quick Payment
                    #region
                    //  DataTable dt = conn.getdataset("select * from Additional where MasterType='" + cmbmastertype.Text + "'");
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["MasterType"].ToString() == "Quick Payment")
                        {
                            if (cmbnooffields.Text == "1")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "2")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "3")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "4")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "5")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "6")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "7")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "8")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "',field8='" + txtfield8.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "9")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield9.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "',field8='" + txtfield8.Text + "',field9='" + txtfield9.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "10")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "',field8='" + txtfield8.Text + "',field9='" + txtfield9.Text + "',field10='" + txtfield10.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "11")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "',field8='" + txtfield8.Text + "',field9='" + txtfield9.Text + "',field10='" + txtfield10.Text + "',field11='" + txtfield11.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "12")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text) && !string.IsNullOrEmpty(txtfield12.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "',field8='" + txtfield8.Text + "',field9='" + txtfield9.Text + "',field10='" + txtfield10.Text + "',field11='" + txtfield11.Text + "',field12='" + txtfield12.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "13")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text) && !string.IsNullOrEmpty(txtfield12.Text) && !string.IsNullOrEmpty(txtfield13.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "',field8='" + txtfield8.Text + "',field9='" + txtfield9.Text + "',field10='" + txtfield10.Text + "',field11='" + txtfield11.Text + "',field12='" + txtfield12.Text + "',field13='" + txtfield13.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "14")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text) && !string.IsNullOrEmpty(txtfield12.Text) && !string.IsNullOrEmpty(txtfield13.Text) && !string.IsNullOrEmpty(txtfield14.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "',field8='" + txtfield8.Text + "',field9='" + txtfield9.Text + "',field10='" + txtfield10.Text + "',field11='" + txtfield11.Text + "',field12='" + txtfield12.Text + "',field13='" + txtfield13.Text + "',field14='" + txtfield14.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "15")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text) && !string.IsNullOrEmpty(txtfield12.Text) && !string.IsNullOrEmpty(txtfield13.Text) && !string.IsNullOrEmpty(txtfield14.Text) && !string.IsNullOrEmpty(txtfield15.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "',field8='" + txtfield8.Text + "',field9='" + txtfield9.Text + "',field10='" + txtfield10.Text + "',field11='" + txtfield11.Text + "',field12='" + txtfield12.Text + "',field13='" + txtfield13.Text + "',field14='" + txtfield14.Text + "',field15='" + txtfield15.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                        }
                        //else
                        //{
                        //    if (cmbnooffields.Text == "1")
                        //    {
                        //        if (!string.IsNullOrEmpty(txtfield1.Text))
                        //        {
                        //            string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "')";
                        //            conn.execute(str1);
                        //        }
                        //    }
                        //}
                    }
                    else
                    {
                        if (cmbnooffields.Text == "1")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "2")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "3")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "4")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "5")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "6")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "7")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "8")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7],[field8]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "','" + txtfield8.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "9")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text) && !string.IsNullOrEmpty(txtfield9.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7],[field8],[field9]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "','" + txtfield8.Text + "','" + txtfield9.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "10")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7],[field8],[field9],[field10]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "','" + txtfield8.Text + "','" + txtfield9.Text + "','" + txtfield10.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "11")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7],[field8],[field9],[field10],[field11]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "','" + txtfield8.Text + "','" + txtfield9.Text + "','" + txtfield10.Text + "','" + txtfield11.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "12")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text) && !string.IsNullOrEmpty(txtfield12.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7],[field8],[field9],[field10],[field11],[field12]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "','" + txtfield8.Text + "','" + txtfield9.Text + "','" + txtfield10.Text + "','" + txtfield11.Text + "','" + txtfield12.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "13")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text) && !string.IsNullOrEmpty(txtfield12.Text) && !string.IsNullOrEmpty(txtfield13.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7],[field8],[field9],[field10],[field11],[field12],[field13]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "','" + txtfield8.Text + "','" + txtfield9.Text + "','" + txtfield10.Text + "','" + txtfield11.Text + "','" + txtfield12.Text + "','" + txtfield13.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "14")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text) && !string.IsNullOrEmpty(txtfield12.Text) && !string.IsNullOrEmpty(txtfield13.Text) && !string.IsNullOrEmpty(txtfield14.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7],[field8],[field9],[field10],[field11],[field12],[field13],[field14]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "','" + txtfield8.Text + "','" + txtfield9.Text + "','" + txtfield10.Text + "','" + txtfield11.Text + "','" + txtfield12.Text + "','" + txtfield13.Text + "','" + txtfield14.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "15")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text) && !string.IsNullOrEmpty(txtfield12.Text) && !string.IsNullOrEmpty(txtfield13.Text) && !string.IsNullOrEmpty(txtfield14.Text) && !string.IsNullOrEmpty(txtfield15.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7],[field8],[field9],[field10],[field11],[field12],[field13],[field14],[field15]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "','" + txtfield8.Text + "','" + txtfield9.Text + "','" + txtfield10.Text + "','" + txtfield11.Text + "','" + txtfield12.Text + "','" + txtfield13.Text + "','" + txtfield14.Text + "','" + txtfield15.Text + "')";
                                conn.execute(str1);
                            }
                        }
                    }
                    #endregion
                }
                else if (cmbmastertype.Text == "Quick Receipt")
                {
                    //Quick Recept
                    #region
                    //  DataTable dt = conn.getdataset("select * from Additional where MasterType='" + cmbmastertype.Text + "'");
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["MasterType"].ToString() == "Quick Receipt")
                        {
                            if (cmbnooffields.Text == "1")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "2")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "3")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "4")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "5")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "6")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "7")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "8")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "',field8='" + txtfield8.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "9")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield9.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "',field8='" + txtfield8.Text + "',field9='" + txtfield9.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "10")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "',field8='" + txtfield8.Text + "',field9='" + txtfield9.Text + "',field10='" + txtfield10.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "11")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "',field8='" + txtfield8.Text + "',field9='" + txtfield9.Text + "',field10='" + txtfield10.Text + "',field11='" + txtfield11.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "12")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text) && !string.IsNullOrEmpty(txtfield12.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "',field8='" + txtfield8.Text + "',field9='" + txtfield9.Text + "',field10='" + txtfield10.Text + "',field11='" + txtfield11.Text + "',field12='" + txtfield12.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "13")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text) && !string.IsNullOrEmpty(txtfield12.Text) && !string.IsNullOrEmpty(txtfield13.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "',field8='" + txtfield8.Text + "',field9='" + txtfield9.Text + "',field10='" + txtfield10.Text + "',field11='" + txtfield11.Text + "',field12='" + txtfield12.Text + "',field13='" + txtfield13.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "14")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text) && !string.IsNullOrEmpty(txtfield12.Text) && !string.IsNullOrEmpty(txtfield13.Text) && !string.IsNullOrEmpty(txtfield14.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "',field8='" + txtfield8.Text + "',field9='" + txtfield9.Text + "',field10='" + txtfield10.Text + "',field11='" + txtfield11.Text + "',field12='" + txtfield12.Text + "',field13='" + txtfield13.Text + "',field14='" + txtfield14.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "15")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text) && !string.IsNullOrEmpty(txtfield12.Text) && !string.IsNullOrEmpty(txtfield13.Text) && !string.IsNullOrEmpty(txtfield14.Text) && !string.IsNullOrEmpty(txtfield15.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "',field8='" + txtfield8.Text + "',field9='" + txtfield9.Text + "',field10='" + txtfield10.Text + "',field11='" + txtfield11.Text + "',field12='" + txtfield12.Text + "',field13='" + txtfield13.Text + "',field14='" + txtfield14.Text + "',field15='" + txtfield15.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                        }
                        //else
                        //{
                        //    if (cmbnooffields.Text == "1")
                        //    {
                        //        if (!string.IsNullOrEmpty(txtfield1.Text))
                        //        {
                        //            string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "')";
                        //            conn.execute(str1);
                        //        }
                        //    }
                        //}
                    }
                    else
                    {
                        if (cmbnooffields.Text == "1")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "2")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "3")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "4")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "5")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "6")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "7")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "8")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7],[field8]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "','" + txtfield8.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "9")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text) && !string.IsNullOrEmpty(txtfield9.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7],[field8],[field9]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "','" + txtfield8.Text + "','" + txtfield9.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "10")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7],[field8],[field9],[field10]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "','" + txtfield8.Text + "','" + txtfield9.Text + "','" + txtfield10.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "11")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7],[field8],[field9],[field10],[field11]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "','" + txtfield8.Text + "','" + txtfield9.Text + "','" + txtfield10.Text + "','" + txtfield11.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "12")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text) && !string.IsNullOrEmpty(txtfield12.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7],[field8],[field9],[field10],[field11],[field12]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "','" + txtfield8.Text + "','" + txtfield9.Text + "','" + txtfield10.Text + "','" + txtfield11.Text + "','" + txtfield12.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "13")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text) && !string.IsNullOrEmpty(txtfield12.Text) && !string.IsNullOrEmpty(txtfield13.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7],[field8],[field9],[field10],[field11],[field12],[field13]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "','" + txtfield8.Text + "','" + txtfield9.Text + "','" + txtfield10.Text + "','" + txtfield11.Text + "','" + txtfield12.Text + "','" + txtfield13.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "14")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text) && !string.IsNullOrEmpty(txtfield12.Text) && !string.IsNullOrEmpty(txtfield13.Text) && !string.IsNullOrEmpty(txtfield14.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7],[field8],[field9],[field10],[field11],[field12],[field13],[field14]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "','" + txtfield8.Text + "','" + txtfield9.Text + "','" + txtfield10.Text + "','" + txtfield11.Text + "','" + txtfield12.Text + "','" + txtfield13.Text + "','" + txtfield14.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "15")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text) && !string.IsNullOrEmpty(txtfield12.Text) && !string.IsNullOrEmpty(txtfield13.Text) && !string.IsNullOrEmpty(txtfield14.Text) && !string.IsNullOrEmpty(txtfield15.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7],[field8],[field9],[field10],[field11],[field12],[field13],[field14],[field15]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "','" + txtfield8.Text + "','" + txtfield9.Text + "','" + txtfield10.Text + "','" + txtfield11.Text + "','" + txtfield12.Text + "','" + txtfield13.Text + "','" + txtfield14.Text + "','" + txtfield15.Text + "')";
                                conn.execute(str1);
                            }
                        }
                    }
                    #endregion
                }
                else if (cmbmastertype.Text == "Production")
                {
                    //Quick Recept
                    #region
                    //  DataTable dt = conn.getdataset("select * from Additional where MasterType='" + cmbmastertype.Text + "'");
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["MasterType"].ToString() == "Production")
                        {
                            if (cmbnooffields.Text == "1")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "2")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "3")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "4")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "5")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "6")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "7")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "8")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "',field8='" + txtfield8.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "9")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield9.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "',field8='" + txtfield8.Text + "',field9='" + txtfield9.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "10")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "',field8='" + txtfield8.Text + "',field9='" + txtfield9.Text + "',field10='" + txtfield10.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "11")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "',field8='" + txtfield8.Text + "',field9='" + txtfield9.Text + "',field10='" + txtfield10.Text + "',field11='" + txtfield11.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "12")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text) && !string.IsNullOrEmpty(txtfield12.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "',field8='" + txtfield8.Text + "',field9='" + txtfield9.Text + "',field10='" + txtfield10.Text + "',field11='" + txtfield11.Text + "',field12='" + txtfield12.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "13")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text) && !string.IsNullOrEmpty(txtfield12.Text) && !string.IsNullOrEmpty(txtfield13.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "',field8='" + txtfield8.Text + "',field9='" + txtfield9.Text + "',field10='" + txtfield10.Text + "',field11='" + txtfield11.Text + "',field12='" + txtfield12.Text + "',field13='" + txtfield13.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "14")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text) && !string.IsNullOrEmpty(txtfield12.Text) && !string.IsNullOrEmpty(txtfield13.Text) && !string.IsNullOrEmpty(txtfield14.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "',field8='" + txtfield8.Text + "',field9='" + txtfield9.Text + "',field10='" + txtfield10.Text + "',field11='" + txtfield11.Text + "',field12='" + txtfield12.Text + "',field13='" + txtfield13.Text + "',field14='" + txtfield14.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "15")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text) && !string.IsNullOrEmpty(txtfield12.Text) && !string.IsNullOrEmpty(txtfield13.Text) && !string.IsNullOrEmpty(txtfield14.Text) && !string.IsNullOrEmpty(txtfield15.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "',field8='" + txtfield8.Text + "',field9='" + txtfield9.Text + "',field10='" + txtfield10.Text + "',field11='" + txtfield11.Text + "',field12='" + txtfield12.Text + "',field13='" + txtfield13.Text + "',field14='" + txtfield14.Text + "',field15='" + txtfield15.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                        }
                        //else
                        //{
                        //    if (cmbnooffields.Text == "1")
                        //    {
                        //        if (!string.IsNullOrEmpty(txtfield1.Text))
                        //        {
                        //            string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "')";
                        //            conn.execute(str1);
                        //        }
                        //    }
                        //}
                    }
                    else
                    {
                        if (cmbnooffields.Text == "1")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "2")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "3")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "4")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "5")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "6")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "7")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "8")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7],[field8]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "','" + txtfield8.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "9")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text) && !string.IsNullOrEmpty(txtfield9.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7],[field8],[field9]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "','" + txtfield8.Text + "','" + txtfield9.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "10")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7],[field8],[field9],[field10]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "','" + txtfield8.Text + "','" + txtfield9.Text + "','" + txtfield10.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "11")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7],[field8],[field9],[field10],[field11]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "','" + txtfield8.Text + "','" + txtfield9.Text + "','" + txtfield10.Text + "','" + txtfield11.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "12")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text) && !string.IsNullOrEmpty(txtfield12.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7],[field8],[field9],[field10],[field11],[field12]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "','" + txtfield8.Text + "','" + txtfield9.Text + "','" + txtfield10.Text + "','" + txtfield11.Text + "','" + txtfield12.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "13")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text) && !string.IsNullOrEmpty(txtfield12.Text) && !string.IsNullOrEmpty(txtfield13.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7],[field8],[field9],[field10],[field11],[field12],[field13]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "','" + txtfield8.Text + "','" + txtfield9.Text + "','" + txtfield10.Text + "','" + txtfield11.Text + "','" + txtfield12.Text + "','" + txtfield13.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "14")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text) && !string.IsNullOrEmpty(txtfield12.Text) && !string.IsNullOrEmpty(txtfield13.Text) && !string.IsNullOrEmpty(txtfield14.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7],[field8],[field9],[field10],[field11],[field12],[field13],[field14]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "','" + txtfield8.Text + "','" + txtfield9.Text + "','" + txtfield10.Text + "','" + txtfield11.Text + "','" + txtfield12.Text + "','" + txtfield13.Text + "','" + txtfield14.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "15")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text) && !string.IsNullOrEmpty(txtfield12.Text) && !string.IsNullOrEmpty(txtfield13.Text) && !string.IsNullOrEmpty(txtfield14.Text) && !string.IsNullOrEmpty(txtfield15.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7],[field8],[field9],[field10],[field11],[field12],[field13],[field14],[field15]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "','" + txtfield8.Text + "','" + txtfield9.Text + "','" + txtfield10.Text + "','" + txtfield11.Text + "','" + txtfield12.Text + "','" + txtfield13.Text + "','" + txtfield14.Text + "','" + txtfield15.Text + "')";
                                conn.execute(str1);
                            }
                        }
                    }
                    #endregion
                }
                else if (cmbmastertype.Text == "Item Entry")
                {
                    //Stock Out
                    #region
                    //  DataTable dt = conn.getdataset("select * from Additional where MasterType='" + cmbmastertype.Text + "'");
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["MasterType"].ToString() == "Item Entry")
                        {
                            if (cmbnooffields.Text == "1")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "2")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "3")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "4")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "5")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "6")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "7")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "8")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "',field8='" + txtfield8.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "9")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield9.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "',field8='" + txtfield8.Text + "',field9='" + txtfield9.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "10")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "',field8='" + txtfield8.Text + "',field9='" + txtfield9.Text + "',field10='" + txtfield10.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "11")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "',field8='" + txtfield8.Text + "',field9='" + txtfield9.Text + "',field10='" + txtfield10.Text + "',field11='" + txtfield11.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "12")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text) && !string.IsNullOrEmpty(txtfield12.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "',field8='" + txtfield8.Text + "',field9='" + txtfield9.Text + "',field10='" + txtfield10.Text + "',field11='" + txtfield11.Text + "',field12='" + txtfield12.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "13")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text) && !string.IsNullOrEmpty(txtfield12.Text) && !string.IsNullOrEmpty(txtfield13.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "',field8='" + txtfield8.Text + "',field9='" + txtfield9.Text + "',field10='" + txtfield10.Text + "',field11='" + txtfield11.Text + "',field12='" + txtfield12.Text + "',field13='" + txtfield13.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "14")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text) && !string.IsNullOrEmpty(txtfield12.Text) && !string.IsNullOrEmpty(txtfield13.Text) && !string.IsNullOrEmpty(txtfield14.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "',field8='" + txtfield8.Text + "',field9='" + txtfield9.Text + "',field10='" + txtfield10.Text + "',field11='" + txtfield11.Text + "',field12='" + txtfield12.Text + "',field13='" + txtfield13.Text + "',field14='" + txtfield14.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                            if (cmbnooffields.Text == "15")
                            {

                                if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text) && !string.IsNullOrEmpty(txtfield12.Text) && !string.IsNullOrEmpty(txtfield13.Text) && !string.IsNullOrEmpty(txtfield14.Text) && !string.IsNullOrEmpty(txtfield15.Text))
                                {
                                    string str1 = "Update Additional set MasterType='" + cmbmastertype.Text + "',nooffields='" + cmbnooffields.Text + "',field1='" + txtfield1.Text + "',field2='" + txtfield2.Text + "',field3='" + txtfield3.Text + "',field4='" + txtfield4.Text + "',field5='" + txtfield5.Text + "',field6='" + txtfield6.Text + "',field7='" + txtfield7.Text + "',field8='" + txtfield8.Text + "',field9='" + txtfield9.Text + "',field10='" + txtfield10.Text + "',field11='" + txtfield11.Text + "',field12='" + txtfield12.Text + "',field13='" + txtfield13.Text + "',field14='" + txtfield14.Text + "',field15='" + txtfield15.Text + "' where MasterType='" + dt.Rows[0]["MasterType"].ToString() + "'";
                                    conn.execute(str1);
                                }
                            }
                        }
                        //else
                        //{
                        //    if (cmbnooffields.Text == "1")
                        //    {
                        //        if (!string.IsNullOrEmpty(txtfield1.Text))
                        //        {
                        //            string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "')";
                        //            conn.execute(str1);
                        //        }
                        //    }
                        //}
                    }
                    else
                    {
                        if (cmbnooffields.Text == "1")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "2")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "3")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "4")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "5")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "6")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "7")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "8")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7],[field8]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "','" + txtfield8.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "9")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text) && !string.IsNullOrEmpty(txtfield9.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7],[field8],[field9]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "','" + txtfield8.Text + "','" + txtfield9.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "10")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7],[field8],[field9],[field10]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "','" + txtfield8.Text + "','" + txtfield9.Text + "','" + txtfield10.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "11")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7],[field8],[field9],[field10],[field11]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "','" + txtfield8.Text + "','" + txtfield9.Text + "','" + txtfield10.Text + "','" + txtfield11.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "12")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text) && !string.IsNullOrEmpty(txtfield12.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7],[field8],[field9],[field10],[field11],[field12]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "','" + txtfield8.Text + "','" + txtfield9.Text + "','" + txtfield10.Text + "','" + txtfield11.Text + "','" + txtfield12.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "13")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text) && !string.IsNullOrEmpty(txtfield12.Text) && !string.IsNullOrEmpty(txtfield13.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7],[field8],[field9],[field10],[field11],[field12],[field13]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "','" + txtfield8.Text + "','" + txtfield9.Text + "','" + txtfield10.Text + "','" + txtfield11.Text + "','" + txtfield12.Text + "','" + txtfield13.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "14")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text) && !string.IsNullOrEmpty(txtfield12.Text) && !string.IsNullOrEmpty(txtfield13.Text) && !string.IsNullOrEmpty(txtfield14.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7],[field8],[field9],[field10],[field11],[field12],[field13],[field14]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "','" + txtfield8.Text + "','" + txtfield9.Text + "','" + txtfield10.Text + "','" + txtfield11.Text + "','" + txtfield12.Text + "','" + txtfield13.Text + "','" + txtfield14.Text + "')";
                                conn.execute(str1);
                            }
                        }
                        else if (cmbnooffields.Text == "15")
                        {
                            if (!string.IsNullOrEmpty(txtfield1.Text) && !string.IsNullOrEmpty(txtfield2.Text) && !string.IsNullOrEmpty(txtfield3.Text) && !string.IsNullOrEmpty(txtfield4.Text) && !string.IsNullOrEmpty(txtfield5.Text) && !string.IsNullOrEmpty(txtfield6.Text) && !string.IsNullOrEmpty(txtfield7.Text) && !string.IsNullOrEmpty(txtfield8.Text) && !string.IsNullOrEmpty(txtfield9.Text) && !string.IsNullOrEmpty(txtfield10.Text) && !string.IsNullOrEmpty(txtfield11.Text) && !string.IsNullOrEmpty(txtfield12.Text) && !string.IsNullOrEmpty(txtfield13.Text) && !string.IsNullOrEmpty(txtfield14.Text) && !string.IsNullOrEmpty(txtfield15.Text))
                            {
                                string str1 = "INSERT INTO [dbo].[Additional]([MasterType],[nooffields],[field1],[field2],[field3],[field4],[field5],[field6],[field7],[field8],[field9],[field10],[field11],[field12],[field13],[field14],[field15]) VALUES('" + cmbmastertype.Text + "','" + cmbnooffields.Text + "','" + txtfield1.Text + "','" + txtfield2.Text + "','" + txtfield3.Text + "','" + txtfield4.Text + "','" + txtfield5.Text + "','" + txtfield6.Text + "','" + txtfield7.Text + "','" + txtfield8.Text + "','" + txtfield9.Text + "','" + txtfield10.Text + "','" + txtfield11.Text + "','" + txtfield12.Text + "','" + txtfield13.Text + "','" + txtfield14.Text + "','" + txtfield15.Text + "')";
                                conn.execute(str1);
                            }
                        }
                    }
                    #endregion
                }









                #region
                //  if (cmbmastertype.Text == "Quick Payment")
                //{
                //  if (cmbnooffields.Text == "1")
                //{
                //  if (!string.IsNullOrEmpty(txtfield1.Text))
                //{
                //  string str1 = @"Alter Table paymentreceipt add " + txtfield1.Text + " nvarchar(MAX)";
                // conn.execute(str1);
                // }
                // }
                //        else if (cmbnooffields.Text == "2")
                //        {
                //            if (!string.IsNullOrEmpty(txtfield1.Text))
                //            {
                //                string str1 = @"Alter Table paymentreceipt add " + txtfield1.Text + " nvarchar(MAX)";
                //                conn.execute(str1);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield2.Text))
                //            {
                //                string str2 = @"Alter Table paymentreceipt add " + txtfield2.Text + " nvarchar(MAX)";
                //                conn.execute(str2);
                //            }
                //        }
                //        else if (cmbnooffields.Text == "3")
                //        {
                //            if (!string.IsNullOrEmpty(txtfield1.Text))
                //            {
                //                string str1 = @"Alter Table paymentreceipt add " + txtfield1.Text + " nvarchar(MAX)";
                //                conn.execute(str1);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield2.Text))
                //            {
                //                string str2 = @"Alter Table paymentreceipt add " + txtfield2.Text + " nvarchar(MAX)";
                //                conn.execute(str2);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield3.Text))
                //            {
                //                string str3 = @"Alter Table paymentreceipt add " + txtfield3.Text + " nvarchar(MAX)";
                //                conn.execute(str3);
                //            }
                //        }
                //        else if (cmbnooffields.Text == "4")
                //        {
                //            if (!string.IsNullOrEmpty(txtfield1.Text))
                //            {
                //                string str1 = @"Alter Table paymentreceipt add " + txtfield1.Text + " nvarchar(MAX)";
                //                conn.execute(str1);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield2.Text))
                //            {
                //                string str2 = @"Alter Table paymentreceipt add " + txtfield2.Text + " nvarchar(MAX)";
                //                conn.execute(str2);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield3.Text))
                //            {
                //                string str3 = @"Alter Table paymentreceipt add " + txtfield3.Text + " nvarchar(MAX)";
                //                conn.execute(str3);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield4.Text))
                //            {
                //                string str4 = @"Alter Table paymentreceipt add " + txtfield4.Text + " nvarchar(MAX)";
                //                conn.execute(str4);
                //            }
                //        }
                //        else if (cmbnooffields.Text == "5")
                //        {
                //            if (!string.IsNullOrEmpty(txtfield1.Text))
                //            {
                //                string str1 = @"Alter Table paymentreceipt add " + txtfield1.Text + " nvarchar(MAX)";
                //                conn.execute(str1);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield2.Text))
                //            {
                //                string str2 = @"Alter Table paymentreceipt add " + txtfield2.Text + " nvarchar(MAX)";
                //                conn.execute(str2);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield3.Text))
                //            {
                //                string str3 = @"Alter Table paymentreceipt add " + txtfield3.Text + " nvarchar(MAX)";
                //                conn.execute(str3);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield4.Text))
                //            {
                //                string str4 = @"Alter Table paymentreceipt add " + txtfield4.Text + " nvarchar(MAX)";
                //                conn.execute(str4);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield5.Text))
                //            {
                //                string str5 = @"Alter Table paymentreceipt add " + txtfield5.Text + " nvarchar(MAX)";
                //                conn.execute(str5);
                //            }
                //        }
                //        else if (cmbnooffields.Text == "6")
                //        {
                //            if (!string.IsNullOrEmpty(txtfield1.Text))
                //            {
                //                string str1 = @"Alter Table paymentreceipt add " + txtfield1.Text + " nvarchar(MAX)";
                //                conn.execute(str1);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield2.Text))
                //            {
                //                string str2 = @"Alter Table paymentreceipt add " + txtfield2.Text + " nvarchar(MAX)";
                //                conn.execute(str2);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield3.Text))
                //            {
                //                string str3 = @"Alter Table paymentreceipt add " + txtfield3.Text + " nvarchar(MAX)";
                //                conn.execute(str3);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield4.Text))
                //            {
                //                string str4 = @"Alter Table paymentreceipt add " + txtfield4.Text + " nvarchar(MAX)";
                //                conn.execute(str4);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield5.Text))
                //            {
                //                string str5 = @"Alter Table paymentreceipt add " + txtfield5.Text + " nvarchar(MAX)";
                //                conn.execute(str5);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield6.Text))
                //            {
                //                string str6 = @"Alter Table paymentreceipt add " + txtfield6.Text + " nvarchar(MAX)";
                //                conn.execute(str6);
                //            }
                //        }
                //        else if (cmbnooffields.Text == "7")
                //        {
                //            if (!string.IsNullOrEmpty(txtfield1.Text))
                //            {
                //                string str1 = @"Alter Table paymentreceipt add " + txtfield1.Text + " nvarchar(MAX)";
                //                conn.execute(str1);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield2.Text))
                //            {
                //                string str2 = @"Alter Table paymentreceipt add " + txtfield2.Text + " nvarchar(MAX)";
                //                conn.execute(str2);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield3.Text))
                //            {
                //                string str3 = @"Alter Table paymentreceipt add " + txtfield3.Text + " nvarchar(MAX)";
                //                conn.execute(str3);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield4.Text))
                //            {
                //                string str4 = @"Alter Table paymentreceipt add " + txtfield4.Text + " nvarchar(MAX)";
                //                conn.execute(str4);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield5.Text))
                //            {
                //                string str5 = @"Alter Table paymentreceipt add " + txtfield5.Text + " nvarchar(MAX)";
                //                conn.execute(str5);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield6.Text))
                //            {
                //                string str6 = @"Alter Table paymentreceipt add " + txtfield6.Text + " nvarchar(MAX)";
                //                conn.execute(str6);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield7.Text))
                //            {
                //                string str7 = @"Alter Table paymentreceipt add " + txtfield7.Text + " nvarchar(MAX)";
                //                conn.execute(str7);
                //            }
                //        }
                //        else if (cmbnooffields.Text == "8")
                //        {
                //            if (!string.IsNullOrEmpty(txtfield1.Text))
                //            {
                //                string str1 = @"Alter Table paymentreceipt add " + txtfield1.Text + " nvarchar(MAX)";
                //                conn.execute(str1);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield2.Text))
                //            {
                //                string str2 = @"Alter Table paymentreceipt add " + txtfield2.Text + " nvarchar(MAX)";
                //                conn.execute(str2);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield3.Text))
                //            {
                //                string str3 = @"Alter Table paymentreceipt add " + txtfield3.Text + " nvarchar(MAX)";
                //                conn.execute(str3);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield4.Text))
                //            {
                //                string str4 = @"Alter Table paymentreceipt add " + txtfield4.Text + " nvarchar(MAX)";
                //                conn.execute(str4);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield5.Text))
                //            {
                //                string str5 = @"Alter Table paymentreceipt add " + txtfield5.Text + " nvarchar(MAX)";
                //                conn.execute(str5);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield6.Text))
                //            {
                //                string str6 = @"Alter Table paymentreceipt add " + txtfield6.Text + " nvarchar(MAX)";
                //                conn.execute(str6);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield7.Text))
                //            {
                //                string str7 = @"Alter Table paymentreceipt add " + txtfield7.Text + " nvarchar(MAX)";
                //                conn.execute(str7);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield8.Text))
                //            {
                //                string str8 = @"Alter Table paymentreceipt add " + txtfield8.Text + " nvarchar(MAX)";
                //                conn.execute(str8);
                //            }
                //        }
                //        else if (cmbnooffields.Text == "9")
                //        {
                //            if (!string.IsNullOrEmpty(txtfield1.Text))
                //            {
                //                string str1 = @"Alter Table paymentreceipt add " + txtfield1.Text + " nvarchar(MAX)";
                //                conn.execute(str1);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield2.Text))
                //            {
                //                string str2 = @"Alter Table paymentreceipt add " + txtfield2.Text + " nvarchar(MAX)";
                //                conn.execute(str2);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield3.Text))
                //            {
                //                string str3 = @"Alter Table paymentreceipt add " + txtfield3.Text + " nvarchar(MAX)";
                //                conn.execute(str3);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield4.Text))
                //            {
                //                string str4 = @"Alter Table paymentreceipt add " + txtfield4.Text + " nvarchar(MAX)";
                //                conn.execute(str4);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield5.Text))
                //            {
                //                string str5 = @"Alter Table paymentreceipt add " + txtfield5.Text + " nvarchar(MAX)";
                //                conn.execute(str5);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield6.Text))
                //            {
                //                string str6 = @"Alter Table paymentreceipt add " + txtfield6.Text + " nvarchar(MAX)";
                //                conn.execute(str6);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield7.Text))
                //            {
                //                string str7 = @"Alter Table paymentreceipt add " + txtfield7.Text + " nvarchar(MAX)";
                //                conn.execute(str7);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield8.Text))
                //            {
                //                string str8 = @"Alter Table paymentreceipt add " + txtfield8.Text + " nvarchar(MAX)";
                //                conn.execute(str8);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield9.Text))
                //            {
                //                string str9 = @"Alter Table paymentreceipt add " + txtfield9.Text + " nvarchar(MAX)";
                //                conn.execute(str9);
                //            }
                //        }
                //        else if (cmbnooffields.Text == "10")
                //        {
                //            if (!string.IsNullOrEmpty(txtfield1.Text))
                //            {
                //                string str1 = @"Alter Table paymentreceipt add " + txtfield1.Text + " nvarchar(MAX)";
                //                conn.execute(str1);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield2.Text))
                //            {
                //                string str2 = @"Alter Table paymentreceipt add " + txtfield2.Text + " nvarchar(MAX)";
                //                conn.execute(str2);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield3.Text))
                //            {
                //                string str3 = @"Alter Table paymentreceipt add " + txtfield3.Text + " nvarchar(MAX)";
                //                conn.execute(str3);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield4.Text))
                //            {
                //                string str4 = @"Alter Table paymentreceipt add " + txtfield4.Text + " nvarchar(MAX)";
                //                conn.execute(str4);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield5.Text))
                //            {
                //                string str5 = @"Alter Table paymentreceipt add " + txtfield5.Text + " nvarchar(MAX)";
                //                conn.execute(str5);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield6.Text))
                //            {
                //                string str6 = @"Alter Table paymentreceipt add " + txtfield6.Text + " nvarchar(MAX)";
                //                conn.execute(str6);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield7.Text))
                //            {
                //                string str7 = @"Alter Table paymentreceipt add " + txtfield7.Text + " nvarchar(MAX)";
                //                conn.execute(str7);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield8.Text))
                //            {
                //                string str8 = @"Alter Table paymentreceipt add " + txtfield8.Text + " nvarchar(MAX)";
                //                conn.execute(str8);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield9.Text))
                //            {
                //                string str9 = @"Alter Table paymentreceipt add " + txtfield9.Text + " nvarchar(MAX)";
                //                conn.execute(str9);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield10.Text))
                //            {
                //                string str10 = @"Alter Table paymentreceipt add " + txtfield10.Text + " nvarchar(MAX)";
                //                conn.execute(str10);
                //            }
                //        }
                //        clearall();
                //        MessageBox.Show("Additional Fields Updated");
                //    }
                //    else if (cmbmastertype.Text == "Quick Receipt")
                //    {
                //        if (cmbnooffields.Text == "1")
                //        {
                //            if (!string.IsNullOrEmpty(txtfield1.Text))
                //            {
                //                string str1 = @"Alter Table paymentreceipt add " + txtfield1.Text + " nvarchar(MAX)";
                //                conn.execute(str1);
                //            }
                //        }
                //        else if (cmbnooffields.Text == "2")
                //        {
                //            if (!string.IsNullOrEmpty(txtfield1.Text))
                //            {
                //                string str1 = @"Alter Table paymentreceipt add " + txtfield1.Text + " nvarchar(MAX)";
                //                conn.execute(str1);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield2.Text))
                //            {
                //                string str2 = @"Alter Table paymentreceipt add " + txtfield2.Text + " nvarchar(MAX)";
                //                conn.execute(str2);
                //            }
                //        }
                //        else if (cmbnooffields.Text == "3")
                //        {
                //            if (!string.IsNullOrEmpty(txtfield1.Text))
                //            {
                //                string str1 = @"Alter Table paymentreceipt add " + txtfield1.Text + " nvarchar(MAX)";
                //                conn.execute(str1);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield2.Text))
                //            {
                //                string str2 = @"Alter Table paymentreceipt add " + txtfield2.Text + " nvarchar(MAX)";
                //                conn.execute(str2);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield3.Text))
                //            {
                //                string str3 = @"Alter Table paymentreceipt add " + txtfield3.Text + " nvarchar(MAX)";
                //                conn.execute(str3);
                //            }
                //        }
                //        else if (cmbnooffields.Text == "4")
                //        {
                //            if (!string.IsNullOrEmpty(txtfield1.Text))
                //            {
                //                string str1 = @"Alter Table paymentreceipt add " + txtfield1.Text + " nvarchar(MAX)";
                //                conn.execute(str1);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield2.Text))
                //            {
                //                string str2 = @"Alter Table paymentreceipt add " + txtfield2.Text + " nvarchar(MAX)";
                //                conn.execute(str2);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield3.Text))
                //            {
                //                string str3 = @"Alter Table paymentreceipt add " + txtfield3.Text + " nvarchar(MAX)";
                //                conn.execute(str3);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield4.Text))
                //            {
                //                string str4 = @"Alter Table paymentreceipt add " + txtfield4.Text + " nvarchar(MAX)";
                //                conn.execute(str4);
                //            }
                //        }
                //        else if (cmbnooffields.Text == "5")
                //        {
                //            if (!string.IsNullOrEmpty(txtfield1.Text))
                //            {
                //                string str1 = @"Alter Table paymentreceipt add " + txtfield1.Text + " nvarchar(MAX)";
                //                conn.execute(str1);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield2.Text))
                //            {
                //                string str2 = @"Alter Table paymentreceipt add " + txtfield2.Text + " nvarchar(MAX)";
                //                conn.execute(str2);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield3.Text))
                //            {
                //                string str3 = @"Alter Table paymentreceipt add " + txtfield3.Text + " nvarchar(MAX)";
                //                conn.execute(str3);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield4.Text))
                //            {
                //                string str4 = @"Alter Table paymentreceipt add " + txtfield4.Text + " nvarchar(MAX)";
                //                conn.execute(str4);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield5.Text))
                //            {
                //                string str5 = @"Alter Table paymentreceipt add " + txtfield5.Text + " nvarchar(MAX)";
                //                conn.execute(str5);
                //            }
                //        }
                //        else if (cmbnooffields.Text == "6")
                //        {
                //            if (!string.IsNullOrEmpty(txtfield1.Text))
                //            {
                //                string str1 = @"Alter Table paymentreceipt add " + txtfield1.Text + " nvarchar(MAX)";
                //                conn.execute(str1);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield2.Text))
                //            {
                //                string str2 = @"Alter Table paymentreceipt add " + txtfield2.Text + " nvarchar(MAX)";
                //                conn.execute(str2);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield3.Text))
                //            {
                //                string str3 = @"Alter Table paymentreceipt add " + txtfield3.Text + " nvarchar(MAX)";
                //                conn.execute(str3);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield4.Text))
                //            {
                //                string str4 = @"Alter Table paymentreceipt add " + txtfield4.Text + " nvarchar(MAX)";
                //                conn.execute(str4);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield5.Text))
                //            {
                //                string str5 = @"Alter Table paymentreceipt add " + txtfield5.Text + " nvarchar(MAX)";
                //                conn.execute(str5);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield6.Text))
                //            {
                //                string str6 = @"Alter Table paymentreceipt add " + txtfield6.Text + " nvarchar(MAX)";
                //                conn.execute(str6);
                //            }
                //        }
                //        else if (cmbnooffields.Text == "7")
                //        {
                //            if (!string.IsNullOrEmpty(txtfield1.Text))
                //            {
                //                string str1 = @"Alter Table paymentreceipt add " + txtfield1.Text + " nvarchar(MAX)";
                //                conn.execute(str1);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield2.Text))
                //            {
                //                string str2 = @"Alter Table paymentreceipt add " + txtfield2.Text + " nvarchar(MAX)";
                //                conn.execute(str2);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield3.Text))
                //            {
                //                string str3 = @"Alter Table paymentreceipt add " + txtfield3.Text + " nvarchar(MAX)";
                //                conn.execute(str3);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield4.Text))
                //            {
                //                string str4 = @"Alter Table paymentreceipt add " + txtfield4.Text + " nvarchar(MAX)";
                //                conn.execute(str4);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield5.Text))
                //            {
                //                string str5 = @"Alter Table paymentreceipt add " + txtfield5.Text + " nvarchar(MAX)";
                //                conn.execute(str5);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield6.Text))
                //            {
                //                string str6 = @"Alter Table paymentreceipt add " + txtfield6.Text + " nvarchar(MAX)";
                //                conn.execute(str6);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield7.Text))
                //            {
                //                string str7 = @"Alter Table paymentreceipt add " + txtfield7.Text + " nvarchar(MAX)";
                //                conn.execute(str7);
                //            }
                //        }
                //        else if (cmbnooffields.Text == "8")
                //        {
                //            if (!string.IsNullOrEmpty(txtfield1.Text))
                //            {
                //                string str1 = @"Alter Table paymentreceipt add " + txtfield1.Text + " nvarchar(MAX)";
                //                conn.execute(str1);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield2.Text))
                //            {
                //                string str2 = @"Alter Table paymentreceipt add " + txtfield2.Text + " nvarchar(MAX)";
                //                conn.execute(str2);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield3.Text))
                //            {
                //                string str3 = @"Alter Table paymentreceipt add " + txtfield3.Text + " nvarchar(MAX)";
                //                conn.execute(str3);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield4.Text))
                //            {
                //                string str4 = @"Alter Table paymentreceipt add " + txtfield4.Text + " nvarchar(MAX)";
                //                conn.execute(str4);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield5.Text))
                //            {
                //                string str5 = @"Alter Table paymentreceipt add " + txtfield5.Text + " nvarchar(MAX)";
                //                conn.execute(str5);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield6.Text))
                //            {
                //                string str6 = @"Alter Table paymentreceipt add " + txtfield6.Text + " nvarchar(MAX)";
                //                conn.execute(str6);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield7.Text))
                //            {
                //                string str7 = @"Alter Table paymentreceipt add " + txtfield7.Text + " nvarchar(MAX)";
                //                conn.execute(str7);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield8.Text))
                //            {
                //                string str8 = @"Alter Table paymentreceipt add " + txtfield8.Text + " nvarchar(MAX)";
                //                conn.execute(str8);
                //            }
                //        }
                //        else if (cmbnooffields.Text == "9")
                //        {
                //            if (!string.IsNullOrEmpty(txtfield1.Text))
                //            {
                //                string str1 = @"Alter Table paymentreceipt add " + txtfield1.Text + " nvarchar(MAX)";
                //                conn.execute(str1);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield2.Text))
                //            {
                //                string str2 = @"Alter Table paymentreceipt add " + txtfield2.Text + " nvarchar(MAX)";
                //                conn.execute(str2);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield3.Text))
                //            {
                //                string str3 = @"Alter Table paymentreceipt add " + txtfield3.Text + " nvarchar(MAX)";
                //                conn.execute(str3);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield4.Text))
                //            {
                //                string str4 = @"Alter Table paymentreceipt add " + txtfield4.Text + " nvarchar(MAX)";
                //                conn.execute(str4);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield5.Text))
                //            {
                //                string str5 = @"Alter Table paymentreceipt add " + txtfield5.Text + " nvarchar(MAX)";
                //                conn.execute(str5);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield6.Text))
                //            {
                //                string str6 = @"Alter Table paymentreceipt add " + txtfield6.Text + " nvarchar(MAX)";
                //                conn.execute(str6);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield7.Text))
                //            {
                //                string str7 = @"Alter Table paymentreceipt add " + txtfield7.Text + " nvarchar(MAX)";
                //                conn.execute(str7);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield8.Text))
                //            {
                //                string str8 = @"Alter Table paymentreceipt add " + txtfield8.Text + " nvarchar(MAX)";
                //                conn.execute(str8);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield9.Text))
                //            {
                //                string str9 = @"Alter Table paymentreceipt add " + txtfield9.Text + " nvarchar(MAX)";
                //                conn.execute(str9);
                //            }
                //        }
                //        else if (cmbnooffields.Text == "10")
                //        {
                //            if (!string.IsNullOrEmpty(txtfield1.Text))
                //            {
                //                string str1 = @"Alter Table paymentreceipt add " + txtfield1.Text + " nvarchar(MAX)";
                //                conn.execute(str1);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield2.Text))
                //            {
                //                string str2 = @"Alter Table paymentreceipt add " + txtfield2.Text + " nvarchar(MAX)";
                //                conn.execute(str2);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield3.Text))
                //            {
                //                string str3 = @"Alter Table paymentreceipt add " + txtfield3.Text + " nvarchar(MAX)";
                //                conn.execute(str3);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield4.Text))
                //            {
                //                string str4 = @"Alter Table paymentreceipt add " + txtfield4.Text + " nvarchar(MAX)";
                //                conn.execute(str4);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield5.Text))
                //            {
                //                string str5 = @"Alter Table paymentreceipt add " + txtfield5.Text + " nvarchar(MAX)";
                //                conn.execute(str5);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield6.Text))
                //            {
                //                string str6 = @"Alter Table paymentreceipt add " + txtfield6.Text + " nvarchar(MAX)";
                //                conn.execute(str6);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield7.Text))
                //            {
                //                string str7 = @"Alter Table paymentreceipt add " + txtfield7.Text + " nvarchar(MAX)";
                //                conn.execute(str7);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield8.Text))
                //            {
                //                string str8 = @"Alter Table paymentreceipt add " + txtfield8.Text + " nvarchar(MAX)";
                //                conn.execute(str8);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield9.Text))
                //            {
                //                string str9 = @"Alter Table paymentreceipt add " + txtfield9.Text + " nvarchar(MAX)";
                //                conn.execute(str9);
                //            }
                //            if (!string.IsNullOrEmpty(txtfield10.Text))
                //            {
                //                string str10 = @"Alter Table paymentreceipt add " + txtfield10.Text + " nvarchar(MAX)";
                //                conn.execute(str10);
                //            }
                //        }
                #endregion
                clearall();
                MessageBox.Show("Additional Fields Updated");
                //   }
            }
            catch
            {
            }
            #endregion
        }
    }
}
