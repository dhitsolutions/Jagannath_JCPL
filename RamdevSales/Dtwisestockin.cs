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
    public partial class Dtwisestockin : Form
    {
        public Dtwisestockin()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        private void Dtwisestockin_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select CompanyId,companyname from Companymaster ", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            cmbcomp.ValueMember = "CompanyId";
            cmbcomp.DisplayMember = "companyname";
            cmbcomp.DataSource = dt;
            cmbcomp.SelectedIndex = -1;
        }

        private void BtnViewReport_Click(object sender, EventArgs e)
        {

        }
    }
}
