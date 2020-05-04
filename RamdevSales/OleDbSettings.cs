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

namespace RamdevSales
{
    class OleDbSettings
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

        public OleDbSettings()
        {


            String path = Application.StartupPath;
            String ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\ConnectDB.mdb;Jet OLEDB:Database Password=allthebest;";
            string database = path + "\\ConnectDB.mdb";

            con = new OleDbConnection(ConnectionString);

            cmd = new OleDbCommand("select * from path", con);
            da = new OleDbDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                path = ds.Tables[0].Rows[0]["defaultpath"].ToString();
                ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + "\\ConnectDB.mdb;Jet OLEDB:Database Password=allthebest;";
                database = path + "\\ConnectDB.mdb";
                con = new OleDbConnection(ConnectionString);

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

        internal DataSet getdata1(string p, OleDbConnection con)
        {
            try
            {
                con.Open();
                da = new OleDbDataAdapter(p, con);
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
        internal DataSet getdata(string p, OleDbConnection con)
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
    }
}
