using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.Drawing;
using System.Net;
using System.Web;
using System.Net.Mail;
using LoggingFramework;


namespace RamdevSales
{
    class Printing
    {
        public OleDbConnection con;
        public OleDbCommand cmd;
        public OleDbDataAdapter da;
        public OleDbDataReader dr;
        public DataSet ds;
        public int i;
        public object a;
        public String s;
        public Control Parent { get; set; }

        public Printing()
        {
            try
            {
                String path = Application.StartupPath;
                String ConnectionString = ConfigurationManager.ConnectionStrings["prnt"].ToString();
                LogGenerator.Info(ConnectionString);
                string database = path + "\\Companies12.mdb";
                LogGenerator.Info(database);
                con = new OleDbConnection(ConnectionString);
            }
            catch
            {
            }
        }

        public int insert(String s)
        {
            try
            {
                con.Open();
                cmd = new OleDbCommand(s, con);
                i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (Exception e)
            {
                MessageBox.Show("" + e.Message);
                return 0;
            }

        }

        public void execute(string s)
        {
         
            OleDbCommand cmd = new OleDbCommand(s, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                //MessageBox.Show("Executed Complated");
            }
            catch(Exception ex)
            {
               // MessageBox.Show("Error"+ex.Message);
                con.Close();
            }
            finally
            {
                con.Close();
            }
        }

        public object Scalar(String s)
        {
            try
            {
                con.Open();
                cmd = new OleDbCommand(s, con);
                a = cmd.ExecuteScalar();
                return a;
            }
            catch (Exception e)
            {
                MessageBox.Show("" + e.Message);
                return 0;
            }
        }

        public int check(String s)
        {
            con.Open();
            cmd = new OleDbCommand(s, con);
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
                return 1;
            else
                return 0;
        }

        public DataSet getdata(string s)
        {
            try
            {
                con.Open();
                da = new OleDbDataAdapter(s, con);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch
            {
                con.Close();
                ds = null; 
                return ds;
            }
            finally
            {
                con.Close();
            }
        }

        public DataSet selectall(string str)
        {
            try
            {
                cmd = new OleDbCommand(str, con);
                da = new OleDbDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch
            {
                con.Close();
                ds = null;
                return ds;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
