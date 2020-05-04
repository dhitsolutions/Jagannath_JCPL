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

namespace RamdevSales
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
            testupdate();
        }

        private void testupdate()
        {
            try
            {
                string remoteUri = "http://software.dhitsolutions.com/contil/RamdevSales.exe";
                string fileName = Application.StartupPath+"\\Update\\RamdevSales.exe";
                
            //    DateTime creation = File.GetCreationTime(@"http://software.dhitsolutions.com/contil/RamdevSales.exe");
               

                WebClient myWebClient = new WebClient();
                myWebClient.DownloadFile(remoteUri, fileName);
                string localURl=Application.StartupPath + "\\RamdevSales.exe";
                string serverURl=Application.StartupPath + "\\Update\\RamdevSales.exe";
                DateTime serverfile = File.GetLastWriteTime(@Application.StartupPath + "\\Update\\RamdevSales.exe");
                DateTime localfile = File.GetLastWriteTime(@Application.StartupPath + "\\RamdevSales.exe");

                if (serverfile > localfile)
                {
                    if (File.Exists(localURl))
                    {
                        File.Delete(localURl);
                    }

                    File.Move(serverURl, localURl);

                }
              //  DirectoryInfo di = new DirectoryInfo("");
              //  FileInfo[] files = di.GetFiles("*.exe");
            }
            catch
            {
            }
            finally
            {
            }
        }

        
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            if (i == 0)
            {
                this.Hide();
                var form2 = new Master();
                form2.Closed += (s, args) => this.Close();
                form2.Show();
                i = 1;
            }
            
            //this.Close();
        }
    }
}
