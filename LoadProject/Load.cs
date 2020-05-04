using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.IO.Compression;

namespace LoadProject
{
    public partial class Load : Form
    {
        public Load()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            ShowInTaskbar = true;
            this.StartPosition = FormStartPosition.CenterScreen;

           
        }
        int i; 
        private void Load_Load(object sender, EventArgs e)
        {
            i = 0;
            timer1.Interval = 1000;
            timer1.Start();
            timer1.Tick += new EventHandler(timer1_Tick);

          
        }
        static bool flag = false;
        int filelength=1;
        private void testupdate()
        {
            if (flag == false)
            {
                try
                {

                    #region  get the text file for taken all files record
                    string URL = "http://software.totalbusinessplus.com/update/files.txt";
                    string local = Application.StartupPath + "\\Update\\files.txt";
                    string path = Application.StartupPath + "\\Update";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    string l = File.ReadAllText(local);
                    string[] flnames = l.Split('\n');
                    WebClient wc = new WebClient();
                    wc.DownloadFile(URL, local);
                    string f = File.ReadAllText(local);
                    string[] filenames = f.Split('\n');


                    #endregion
                    if (Convert.ToDateTime(flnames[0].Replace('\r', ' ').Trim()) < Convert.ToDateTime(filenames[0].Replace('\r', ' ').Trim()))
                    {
                        #region   download all files from server
                        progressBar1.Maximum = filenames.Length;
                        filelength = filenames.Length;
                        progressBar1.PerformStep();
                        for (int i = 0; i < filenames.Length; i++)
                        {
                            try
                            {
                                DateTime localfile = File.GetLastWriteTime(@Application.StartupPath + "" + filenames[i].Replace('\r', ' ').Trim());
                                string remoteUri = "http://software.totalbusinessplus.com/update/" + filenames[i].Replace('\r', ' ').Trim();
                                //string fileName = Application.StartupPath+"\\Update\\RamdevSales.exe";
                                string fileName = Application.StartupPath + "\\Update\\" + filenames[i].Replace('\r', ' ').Trim();
                                //string path=Application.StartupPath+"\\Update";
                                WebClient myWebClient = new WebClient();
                                myWebClient.DownloadFile(remoteUri, fileName);
                                //string localURl=Application.StartupPath + "\\RamdevSales.exe";
                                //string serverURl=Application.StartupPath + "\\Update\\RamdevSales.exe";
                                string localURl = Application.StartupPath + "\\" + filenames[i].Replace('\r', ' ').Trim();
                                string serverURl = Application.StartupPath + "\\Update\\" + filenames[i].Replace('\r', ' ').Trim();

                                DateTime serverfile = File.GetLastWriteTime(@Application.StartupPath + "\\Update\\" + filenames[i].Replace('\r', ' ').Trim());


                                if (serverfile > localfile)
                                {

                                    if (File.Exists(localURl))
                                    {
                                        if (!Directory.Exists(Application.StartupPath + "\\backup\\Setup"))
                                        {
                                            Directory.CreateDirectory(Application.StartupPath + "\\backup\\Setup");
                                        }
                                        if (!File.Exists(Application.StartupPath + "\\backup\\Setup\\" + DateTime.Now.ToString("yyyyMMdd") + filenames[i].Replace('\r', ' ').Trim()))
                                        {
                                            File.Move(localURl, Application.StartupPath + "\\backup\\Setup\\" + DateTime.Now.ToString("yyyyMMdd") + filenames[i].Replace('\r', ' ').Trim() + "");
                                        }

                                        //File.Delete(localURl);
                                    }


                                    File.Move(serverURl, localURl);

                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error:" + ex.Message);
                            }
                            progressBar1.PerformStep();
                        }

                        #endregion
                        //  DirectoryInfo di = new DirectoryInfo("");
                        //  FileInfo[] files = di.GetFiles("*.exe");
                    }
                    else
                    {
                        progressBar1.Maximum = 1;
                        filelength = 1;
                        progressBar1.PerformStep();
                    }

                }
                catch (Exception ex)
                {
                    //flag = true;
                  //  MessageBox.Show("error:" + ex.Message);
                    progressBar1.Maximum = 1;
                    filelength = 1;
                    progressBar1.PerformStep();
                    
                }
                finally
                {
                }
            }
        }
       
        private void timer1_Tick(object sender, EventArgs e)
        {
           
            
            if (i==0)
            {
                testupdate();
                if (flag == false)
                {
                    if (progressBar1.Value == filelength)
                    {
                        flag = true;
                        timer1.Enabled = false;   //Add this line
                        try
                        {
                            Process.Start(@Application.StartupPath + "\\RamdevSales.exe");
                        }
                        catch
                        {
                        }
                        this.Close();
                    }
                }
              
            }
           // timer1.Stop();
            //if (i == 0)
            //{
            //    this.Hide();
            //    Process.Start(@Application.StartupPath+"\\RamdevSales.exe");
            //    //var form2 = new Master();
            //    //form2.Closed += (s, args) => this.Close();
            //    //form2.Show();
            //    i = 1;
            //    this.Close();
                
            //}
            //progressBar1.PerformStep();
            
            //this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
