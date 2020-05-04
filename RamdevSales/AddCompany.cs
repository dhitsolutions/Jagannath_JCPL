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
    public partial class AddCompany : Form
    {
        ServerConnection sc = new ServerConnection();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        public static string CompanyID;
        public static string CN = string.Empty;
        public AddCompany()
        {
            InitializeComponent();
            listView1.Columns.Add("Company Name", 150, HorizontalAlignment.Center);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Update")
            {
                sc.execute("Update CompanyMaster set CompanyName='" + txtcompname.Text + "' where CompanyId=" + CompanyID + "");
                listviewbind();
                CN = txtcompname.Text;
                txtcompname.Text = "";
                
                btnAdd.Text = "Save";
                MessageBox.Show("Update Data Successfully...");
                
            }
            else
            {
                
                sc.execute("insert into CompanyMaster([CompanyName]) values('" + txtcompname.Text + "')");
                listviewbind();
                CN = txtcompname.Text;
                txtcompname.Text = "";
                
                btnAdd.Text = "ADD";
                MessageBox.Show("Data Inserted Successfully...");
                this.Close();
               
            }
        }

        private void AddCompany_Load(object sender, EventArgs e)
        {
            
            listviewbind();
        }
        private void listviewbind()
        {
            try
            {
                listView1.Items.Clear();
                dt = sc.getdataset("select CompanyName from CompanyMaster order by CompanyID desc");
                
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    listView1.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                    
                }
            }
            catch
            {
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                txtcompname.Text = listView1.Items[listView1.FocusedItem.Index].SubItems[0].Text;

                dt = sc.getdataset("select CompanyID from CompanyMaster where companyname ='" + listView1.Items[listView1.FocusedItem.Index].SubItems[0].Text + "' ");
                CompanyID = dt.Rows[0][0].ToString();
                
                btnAdd.Text = "Update";
                listviewbind();
            }
        }
    }
}
