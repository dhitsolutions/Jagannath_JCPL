using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace RamdevSales
{
    class data
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        public string getclientname(string clientid)
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                SqlCommand cmd = new SqlCommand("select accountname from clientmaster where clientid='" + clientid + "'", con);
              
                string clientname = cmd.ExecuteScalar().ToString();
                //  cmbcustname.SelectedIndex = cmbcustname.Items.IndexOf(clientname);

                return clientname;
            }
            catch (Exception e)
            {
                MessageBox.Show("" + e.Message);
                con.Close();
                return "";
            }
            finally
            {
                con.Close();
            }
        }
    }
}
