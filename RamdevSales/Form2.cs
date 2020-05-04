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
    public partial class Form2 : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            List<Button> buttons = new List<Button>();
            for (int i = 0; i < 10; i++)
            {
                Button newButton = new Button();
                buttons.Add(newButton);
                newButton.Name = "hello" + i;
                newButton.Text = "Hello" + i;
                newButton.Location=new Point(i*10, i*30);
                string str = "hello" + i+"_Click";
                newButton.Click += new EventHandler(button_Click);
                this.Controls.Add(newButton);
            }
        }


        protected void button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            MessageBox.Show("Button " + button.Text + " Clicked");
            // identify which button was clicked and perform necessary actions
        }
        private void autobind()
        {
            String qry = "select ProductMaster.Product_Name from ProductMaster order by ProductMaster.Product_Name";
           // con.Open();
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            string[] arr = new string[dt.Rows.Count];
          //  string list="";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                arr[i]= dt.Rows[i][0].ToString();
            }

        //    var stringArr = dt.Rows[0].ItemArray.Select(x => x.ToString()).ToArray();

            txtlist.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtlist.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtlist.AutoCompleteCustomSource.AddRange(arr);

          
        }
        int key = 0;
        private void txtlist_TextChanged(object sender, EventArgs e)
        {
            if (key == 1)
            {
                autobind(txtlist.Text);
            }
        }

        private void autobind(string p)
        {
            String qry = "select ProductMaster.Product_Name from ProductMaster where ProductMaster.Product_Name like '%" + p + "%' order by ProductMaster.Product_Name";
            // con.Open();
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            string[] arr = new string[dt.Rows.Count];
            //  string list="";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                arr[i] = dt.Rows[i][0].ToString();
            }

            //    var stringArr = dt.Rows[0].ItemArray.Select(x => x.ToString()).ToArray();
            key = 0;
            txtlist.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtlist.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtlist.AutoCompleteCustomSource.AddRange(arr);
            
        }

        private void txtlist_KeyPress(object sender, KeyPressEventArgs e)
        {
            key = 1;
         //   autobind(txtlist.Text);
        }
    }
}
