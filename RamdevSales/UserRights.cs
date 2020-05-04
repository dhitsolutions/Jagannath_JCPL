using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RamdevSales
{
    public partial class UserRights : Form
    {
        Connection cl = new Connection();
        DataSet ds = new DataSet();
        DataTable dt,dt1 = new DataTable();
       // public string mid = string.Empty;
        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
        DataGridViewCheckBoxColumn checkBoxColumn1 = new DataGridViewCheckBoxColumn();
        DataGridViewCheckBoxColumn checkBoxColumn2 = new DataGridViewCheckBoxColumn();
        DataGridViewCheckBoxColumn checkBoxColumn3 = new DataGridViewCheckBoxColumn();
        DataGridViewCheckBoxColumn checkBoxColumn4 = new DataGridViewCheckBoxColumn();
        private Master master;
        private TabControl tabControl;
       
        public UserRights()
        {
            InitializeComponent();
        }

        public UserRights(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            this.master = master;
            this.tabControl = tabControl;
            InitializeComponent();
        }
        public void GridDesign()
        {

            this.dataGridView1.DefaultCellStyle.Font = new Font("Calibri", 11, FontStyle.Regular);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", 11.25f, FontStyle.Bold);
            DataGridViewCellStyle fooCellStyle = new DataGridViewCellStyle();
            DataGridViewHeaderCell f = new DataGridViewHeaderCell();

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Maroon;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.DefaultCellStyle.ForeColor = Color.White;
            dataGridView1.EnableHeadersVisualStyles = false;
          //  dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersVisible = false;
            //DataGridViewColumn dataGridViewColumn = dgvitem.Columns[2];
            //dataGridViewColumn.HeaderCell.Style.BackColor = Color.Green;


            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells[0].Style.Font = new Font("Calibri", 11, FontStyle.Bold);
                row.Cells[0].Style.BackColor = Color.LightGray;
                row.Cells[0].Style.ForeColor = Color.Black;
                row.Cells[1].Style.Font = new Font("Calibri", 11, FontStyle.Bold);
                row.Cells[1].Style.BackColor = Color.LightGray;
                row.Cells[1].Style.ForeColor = Color.Black;
                row.Cells[2].Style.Font = new Font("Calibri", 11, FontStyle.Bold);
                row.Cells[2].Style.BackColor = Color.LightGray;
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
            }

            // dgvitem.Enabled = false;

        }
        private void UserRights_Load(object sender, EventArgs e)
        {
            bindUName();
            bindMenu();
          //  GridDesign();
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Columns[0].Width = 150;
            //set the interval  and start the timer
          //  timer1.Interval = 1000;
          //  timer1.Start();
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
        private void bindUName()
        {
            dt = cl.getdataset("select UserId,UserName from UserInfo where CompanyId="+Master.companyId+"");
            if (dt.Rows.Count > 0)
            {
                cmbUserName.ValueMember = "UserId";
                cmbUserName.DisplayMember = "UserName";
                cmbUserName.DataSource = dt;
                cmbUserName.SelectedIndex = -1;
              //  autobind(dt, cmbUserName);
            }
            else
            {
                MessageBox.Show("Please Add User.");
            }
        
        }

        private void autobind(DataTable dt1, ComboBox cmbcustname)
        {
            string[] arr = new string[dt1.Rows.Count];
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                arr[i] = dt1.Rows[i][1].ToString();
            }

            cmbcustname.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cmbcustname.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbcustname.AutoCompleteCustomSource.AddRange(arr);
        }

        private void bindMenu()
        {
            try
            {
                dt = cl.getdataset("select mName from MenuMaster where isActive=1");

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dataGridView1.DataSource = dt;
                        // dataGridView1.Columns[0].Name = "Add";

                    }
                  //  dataGridView1.Columns[0].Width = 300;
                    dataGridView1.Columns[0].HeaderText = "";
                    dataGridView1.Columns[0].ReadOnly = true;
                    
                    DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                    checkBoxColumn.HeaderText = "Add";
                    checkBoxColumn.Width = 50;
                    checkBoxColumn.Name = "Add";
                    dataGridView1.Columns.Insert(1, checkBoxColumn);


                    checkBoxColumn1.HeaderText = "Update";
                    checkBoxColumn1.Width = 50;
                    checkBoxColumn1.Name = "Update";
                    dataGridView1.Columns.Insert(2, checkBoxColumn1);

                    // DataGridViewCheckBoxColumn checkBoxColumn2 = new DataGridViewCheckBoxColumn();
                    checkBoxColumn2.HeaderText = "Delete";
                    checkBoxColumn2.Width = 50;
                    checkBoxColumn2.Name = "Delete";
                    dataGridView1.Columns.Insert(3, checkBoxColumn2);

                    //  DataGridViewCheckBoxColumn checkBoxColumn3 = new DataGridViewCheckBoxColumn();
                    checkBoxColumn3.HeaderText = "View";
                    checkBoxColumn3.Width = 50;
                    checkBoxColumn3.Name = "View";
                    dataGridView1.Columns.Insert(4, checkBoxColumn3);

                    //   DataGridViewCheckBoxColumn checkBoxColumn4 = new DataGridViewCheckBoxColumn();
                    checkBoxColumn4.HeaderText = "Print";
                    checkBoxColumn4.Width = 50;
                    checkBoxColumn4.Name = "Print";
                    dataGridView1.Columns.Insert(5, checkBoxColumn4);

                }
            }
            catch
            {
            }
        }

        private void cmbUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
           // uid = cmbUserName.SelectedIndex.ToString();
            try
            {
                // bindMenu();
                if (cmbUserName.SelectedIndex != -1)
                {
                    dt1 = cl.getdataset("Select m.mname,u.a,u.u,u.d,u.v,u.p from UserRights u inner join menumaster m on m.mid=u.mid where uId='" + cmbUserName.SelectedValue + "' and u.isActive=1");
                    if (dt1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            if (dt1.Rows[i]["a"].ToString() == "True")
                            {
                                dataGridView1.Rows[i].Cells[1].Value = true;
                            }
                            else
                            {
                                dataGridView1.Rows[i].Cells[1].Value = false;
                            }

                            if (dt1.Rows[i]["u"].ToString() == "True")
                            {
                                dataGridView1.Rows[i].Cells[2].Value = true;
                            }
                            else
                            {
                                dataGridView1.Rows[i].Cells[2].Value = false;
                            }

                            if (dt1.Rows[i]["d"].ToString() == "True")
                            {
                                dataGridView1.Rows[i].Cells[3].Value = true;
                            }
                            else
                            {
                                dataGridView1.Rows[i].Cells[3].Value = false;
                            }

                            if (dt1.Rows[i]["v"].ToString() == "True")
                            {
                                dataGridView1.Rows[i].Cells[4].Value = true;
                            }
                            else
                            {
                                dataGridView1.Rows[i].Cells[4].Value = false;
                            }

                            if (dt1.Rows[i]["p"].ToString() == "True")
                            {
                                dataGridView1.Rows[i].Cells[5].Value = true;
                            }
                            else
                            {
                                dataGridView1.Rows[i].Cells[5].Value = false;
                            }
                        }

                    }
                    else
                    {
                        clearSelection();
                    }
                    bool inList = false;
                    for (int i = 0; i < cmbUserName.Items.Count; i++)
                    {
                        s = cmbUserName.GetItemText(cmbUserName.Items[i]);
                        if (s == cmbUserName.Text)
                        {
                            inList = true;
                            cmbUserName.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        cmbUserName.Text = "";
                    }
                }
            }
            catch
            {
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if( cmbUserName.SelectedIndex == -1)
            {
                MessageBox.Show("Select User Name");
                return;
            }
            else
            {
            dt = cl.getdataset("select mId,mName from MenuMaster where isActive=1");
            
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if ((dataGridView1.Rows[i].Cells["Add"].Value) == null)
                    {
                        dataGridView1.Rows[i].Cells["Add"].Value = "False";
                    }
                    if ((dataGridView1.Rows[i].Cells["Update"].Value) == null)
                    {
                        dataGridView1.Rows[i].Cells["Update"].Value = "False";
                    }
                    if ((dataGridView1.Rows[i].Cells["Delete"].Value) == null)
                    {
                        dataGridView1.Rows[i].Cells["Delete"].Value = "False";
                    }
                    if ((dataGridView1.Rows[i].Cells["View"].Value) == null)
                    {
                        dataGridView1.Rows[i].Cells["View"].Value = "False";
                    }
                    if ((dataGridView1.Rows[i].Cells["Print"].Value) == null)
                    {
                        dataGridView1.Rows[i].Cells["Print"].Value = "False";
                    }
                    if (cmbUserName.Text != null)
                    {
                        cl.execute("INSERT INTO [UserRights]([uId],[uName],[cId],[mId],[a],[u],[d],[v],[p],[isActive]) values(" + cmbUserName.SelectedValue + ",'" + cmbUserName.Text + "'," + Master.companyId + "," + dt.Rows[i][0].ToString() + ",'" + Convert.ToString(dataGridView1.Rows[i].Cells["Add"].Value) + "','" + Convert.ToString(dataGridView1.Rows[i].Cells["Update"].Value) + "','" + Convert.ToString(dataGridView1.Rows[i].Cells["Delete"].Value) + "','" + Convert.ToString(dataGridView1.Rows[i].Cells["View"].Value) + "','" + Convert.ToString(dataGridView1.Rows[i].Cells["Print"].Value) + "',1)");
                        
                    }
                    
                }
                MessageBox.Show("User rights applied successfully.");
                clearSelection();
                cmbUserName.SelectedIndex = -1;
            }
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cmbUserName.SelectedIndex == -1)
            {
                MessageBox.Show("Select User Name");
                return;
            }
            else
            {
                dt = cl.getdataset("select mId,mName from MenuMaster where isActive=1");

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    cl.execute("Update UserRights set [a]='" + Convert.ToString(dataGridView1.Rows[i].Cells["Add"].Value) + "',[u]='" + Convert.ToString(dataGridView1.Rows[i].Cells["Update"].Value) + "',[d]='" + Convert.ToString(dataGridView1.Rows[i].Cells["Delete"].Value) + "',[v]='" + Convert.ToString(dataGridView1.Rows[i].Cells["View"].Value) + "',[p]='" + Convert.ToString(dataGridView1.Rows[i].Cells["Print"].Value) + "' where uId=" + cmbUserName.SelectedValue + " and isActive=1 and mId=" + dt.Rows[i][0].ToString() + " and cId=" + Master.companyId + "");
                }
                MessageBox.Show("User rights updated successfully.");
                clearSelection();
                cmbUserName.SelectedIndex = -1;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (cmbUserName.SelectedIndex == -1)
            {
                MessageBox.Show("Select User Name");
                return;
            }
            else
            {
                DialogResult dr = MessageBox.Show("Do you want to Delete Rights?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        cl.execute("Update UserRights set isActive=0 where uId=" + cmbUserName.SelectedValue + " and isActive=1 and cId=" + Master.companyId + "");
                    }
                    MessageBox.Show("User rights deleted successfully.");
                    clearSelection();
                    cmbUserName.SelectedIndex = -1;
                }
                else
                {
                }
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

        private void clearSelection()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                //dataGridView1.Rows[i].Cells[0].Value = false;
                dataGridView1.Rows[i].Cells[1].Value = false;
                dataGridView1.Rows[i].Cells[2].Value = false;
                dataGridView1.Rows[i].Cells[3].Value = false;
                dataGridView1.Rows[i].Cells[4].Value = false;
                dataGridView1.Rows[i].Cells[5].Value = false;
            }
        }

        

        private void btnAdd_MouseEnter(object sender, EventArgs e)
        {
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.BackColor = Color.YellowGreen;
            btnAdd.ForeColor = Color.White;
        }

        private void btnAdd_MouseLeave(object sender, EventArgs e)
        {

            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.BackColor = Color.FromArgb(51, 153, 255);
            btnAdd.ForeColor = Color.White;
        }

        private void btnUpdate_MouseEnter(object sender, EventArgs e)
        {
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.BackColor = Color.YellowGreen;
            btnUpdate.ForeColor = Color.White;
        }

        private void btnUpdate_MouseLeave(object sender, EventArgs e)
        {
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.BackColor = Color.FromArgb(51, 153, 255);
            btnUpdate.ForeColor = Color.White;
        }

        private void btnDelete_MouseEnter(object sender, EventArgs e)
        {
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.BackColor = Color.FromArgb(255, 77, 77);
            btnDelete.ForeColor = Color.White;
        }

        private void btnDelete_MouseLeave(object sender, EventArgs e)
        {
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.BackColor = Color.FromArgb(51, 153, 255);
            btnDelete.ForeColor = Color.White;
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

        private void btnAdd_Enter(object sender, EventArgs e)
        {
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.BackColor = Color.YellowGreen;
            btnAdd.ForeColor = Color.White;
        }

        private void btnAdd_Leave(object sender, EventArgs e)
        {
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.BackColor = Color.FromArgb(51, 153, 255);
            btnAdd.ForeColor = Color.White;
        }

        private void btnUpdate_Leave(object sender, EventArgs e)
        {
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.BackColor = Color.FromArgb(51, 153, 255);
            btnUpdate.ForeColor = Color.White;
        }

        private void btnUpdate_Enter(object sender, EventArgs e)
        {
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.BackColor = Color.YellowGreen;
            btnUpdate.ForeColor = Color.White;
        }

        private void btnDelete_Enter(object sender, EventArgs e)
        {
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.BackColor = Color.FromArgb(255, 77, 77);
            btnDelete.ForeColor = Color.White;
        }

        private void btnDelete_Leave(object sender, EventArgs e)
        {
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.BackColor = Color.FromArgb(51, 153, 255);
            btnDelete.ForeColor = Color.White;
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
        private void timer1_Tick(object sender, EventArgs e)
        {
            //empty the string for every 1 seconds
           // searchstr = "";
        }

        private void cmbUserName_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbUserName.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbUserName.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        public static string s;
        private void cmbUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbUserName.Items.Count; i++)
                {
                    s = cmbUserName.GetItemText(cmbUserName.Items[i]);
                    if (s == cmbUserName.Text)
                    {
                        inList = true;
                        cmbUserName.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbUserName.Text = "";
                }

            }
        }
        
    }
}
