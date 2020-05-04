using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Data.OleDb;

namespace RamdevSales
{
    class Connection
    {
        public OleDbSettings ods = new OleDbSettings();
        public DataSet ds;
        public DataTable dt;
        public SqlConnection con;
        public SqlCommand cmd;
        public SqlDataAdapter da;
        public string companyid;
        public void getcon()
        {

            con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
                
                    }
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        
        public void execute(string sql)
        {
            getcon();
           SqlCommand cmd = new SqlCommand(sql, con);
           
           
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

        public String ExecuteScalar(String s)
        {
            try
            {
                getcon();
                con.Open();
                string str = "";
                cmd = new SqlCommand(s, con);
                String count = Convert.ToString(cmd.ExecuteScalar());
                //   str = cmd.ExecuteScalar().ToString();
                str = count.ToString();
                return str;

            }
            catch (Exception e)
            {

                return "";
            }
            finally
            {
                con.Close();
            }
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
        private byte[] convertPicBoxImageToByte(PictureBox picturebox2)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            picturebox2.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }
        public int insertimage(String s, PictureBox picturebox2)
        {
            int i;
            try
            {
                getcon();
                con.Open();
                byte[] b = new byte[0];
                b = convertPicBoxImageToByte(picturebox2);
                cmd = new SqlCommand(s, con);
                cmd.Parameters.Add(new SqlParameter("@Image", SqlDbType.VarBinary, convertPicBoxImageToByte(picturebox2).Length, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Current, convertPicBoxImageToByte(picturebox2)));
                i = cmd.ExecuteNonQuery();
                con.Close();
                return i;
            }
            catch (Exception e)
            {
                MessageBox.Show("" + e.Message);
                con.Close();
                return 0;
            }

        }

        public String getsinglevalue(String s)
        {
            try
            {
                getcon();
                con.Open();
                string str = "";
                cmd = new SqlCommand(s, con);
                str = cmd.ExecuteScalar().ToString();
                return str;

            }
            catch (Exception e)
            {

                return "";
            }
            finally
            {
                con.Close();
            }
        }

        public DataTable ReturnDataSet(string spName, params SqlParameter[] cmdparam)
        {
            try
            {
                getcon();
                con.Open();
                cmd = new SqlCommand();
               
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;
                cmd.Connection = con;
                if (System.Convert.IsDBNull(cmdparam) == false)
                {
                    SqlParameter parm = null;
                    foreach (SqlParameter parm_loopVariable in cmdparam)
                    {
                        parm = parm_loopVariable;
                        cmd.Parameters.Add(parm);
                    }
                }
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return dt;
            }
            catch
            {
                dt = null;
                return dt;
            }

        }
        public object ReturnScalerObject(string spName, params SqlParameter[] cmdparam)
        {
            getcon();
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = spName;
            cmd.Connection = con;
            if (System.Convert.IsDBNull(cmdparam) == false)
            {
                SqlParameter parm;
                foreach (SqlParameter parm_loopVariable in cmdparam)
                {
                    parm = parm_loopVariable;
                    cmd.Parameters.Add(parm);
                }
            }

            object obj = cmd.ExecuteScalar();
            con.Close();
            return obj;
        }
        public DataTable getdataset(string sql)
        {
            try
            {
                getcon();
                dt = new DataTable();
                cmd = new SqlCommand(sql, con);
                con.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }

            catch
            {
                con.Close();
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        public DataSet getdata(string sql)
        {
            try
            {
                getcon();
                cmd = new SqlCommand(sql, con);
                con.Open();
                da = new SqlDataAdapter(cmd);
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
                getcon();
                con.Open();
                cmd = new SqlCommand(str, con);
                da = new SqlDataAdapter(cmd);
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

        internal System.Data.SqlClient.SqlDataReader readdata(string sql1)
        {
            try
            {
                getcon();
                con.Open();
                SqlCommand cmd = new SqlCommand(sql1, con);
                SqlDataReader idr = cmd.ExecuteReader();
                return idr;
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
            }


        }

        internal void insertimage(string s)
        {
            int i;
            try
            {
                getcon();
                con.Open();
                
                SqlCommand cmd = new SqlCommand(s, con);
                
                i = cmd.ExecuteNonQuery();
                con.Close();
              //  return i;
            }
            catch (Exception e)
            {
                MessageBox.Show("" + e.Message);
                con.Close();
              //  return 0;
            }
        }



        internal DataTable getdataset(string sql, SqlConnection con)
        {
            try
            {
               // getcon();
                dt = new DataTable();
                cmd = new SqlCommand(sql, con);
                con.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }

            catch
            {
                con.Close();
                return null;
            }
            finally
            {
                con.Close();
            }
        }
        internal String ExecuteScalar(String s, SqlConnection con)
        {
            try
            {
                //getcon();
                con.Open();
                string str = "";
                cmd = new SqlCommand(s, con);
                String count = Convert.ToString(cmd.ExecuteScalar());
                //   str = cmd.ExecuteScalar().ToString();
                str = count.ToString();
                return str;

            }
            catch (Exception e)
            {

                return "";
            }
            finally
            {
                con.Close();
            }
        }
        internal void execute(string sql, SqlConnection con)
        {
          //  getcon();
            SqlCommand cmd = new SqlCommand(sql, con);


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
    }      

}
