using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Data.SqlClient;

namespace RamdevSales
{
    public partial class AMC : Form
    {
        OleDbSettings ods = new OleDbSettings();
        ServerConnection con = new ServerConnection();
        Connection conn = new Connection();
        SqlConnection constring = new SqlConnection("Data Source=184.168.47.17;Initial Catalog=BusinessPlus;User ID=Businessplus;Password=Businessplus1!");
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        string cname, cemail;
        public static string encryptdate = string.Empty;
        public static string expdatenew = string.Empty;
        private Master master;
        private TabControl tabControl;
        public AMC()
        {
            InitializeComponent();
        }

        public AMC(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
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
        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "00";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                    encryptdate = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "00";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        public static string Decryptexpdate(string cipherText)
        {
            string EncryptionKey = "00";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    expdatenew = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        private void AMC_Load(object sender, EventArgs e)
        {
            try
            {
                ds = ods.getdata("Select * from tblreg");
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    Decryptsrno(dt.Rows[0]["d1"].ToString());
                    txtserialkey.Text = srno;
                    Decryptexpdate(dt.Rows[0]["d14"].ToString());
                    lbldate.Text = DateTime.Parse(expdatenew).ToString(Master.dateformate);
                    this.ActiveControl = txtextendkey;
                }
                
                // cname = dt.Rows[0]["regcompanyname"].ToString();
                //  cemail = dt.Rows[0]["regemail"].ToString();
                //Encrypt(dt.Rows[0]["expdate"].ToString());
                // Decrypt(txtextendkey.Text);
            }
            catch
            {
            }
        }
        public static string SendingEmail(string Name, string Email, string Subject, string Message)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(Email);

            msg.Subject = Subject;
            msg.From = new MailAddress(Email);
            msg.IsBodyHtml = true;
            msg.Body = "<p><strong>Contact Name:</strong> " + Name + "<br />";
            msg.Body += "<strong>Email:</strong> " + Email + "<br />";
            msg.Body += "<strong>Message:</strong> " + Message + "<br />";
            // set the reply to address to the address entered in the form  
            msg.ReplyToList.Add(new MailAddress(Email));
            // set up smtp client and credentials  
            SmtpClient smtpClnt = new SmtpClient();
            smtpClnt.Host = "smtp.gmail.com";
            smtpClnt.EnableSsl = true;
            smtpClnt.Credentials = new NetworkCredential("support@totalbusinessplus.com", "mhaqoanapzdocxra"); // send the message  
            smtpClnt.Port = 587;
            smtpClnt.Send(msg);
            return "Ok";
        }
        public static string Encryptexpdate(string clearText)
        {
            string EncryptionKey = "00";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                    expdate = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Encryptregdate(string clearText)
        {
            string EncryptionKey = "00";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                    regdate = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        datetime defaultdateformate = new datetime();
        public static string expdate = string.Empty;
        public static string regdate = string.Empty;
        public static string Encrypststus(string clearText)
        {
            string EncryptionKey = "00";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                    status = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string status = string.Empty;
        string statu = "Reg";
        private void btnok_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = con.getdataset("select * from Renewal where isactive=1 and SRNo='" + txtserialkey.Text + "'", constring);
                if (dt.Rows.Count > 0)
                {
                    if (txtserialkey.Text == dt.Rows[0]["SRNo"].ToString() && txtextendkey.Text == dt.Rows[0]["RenewalSerialNumber"].ToString())
                    {
                        string d = DateTime.Now.ToString("dd-MM-yyyy");
                        DateTime dt1 = Convert.ToDateTime(d);
                        string amc = conn.ExecuteScalar("Select amcday from updatedatabase where id='" + "1" + "'");
                        if (!string.IsNullOrEmpty(amc))
                        {
                            dt1 = dt1.AddDays(Convert.ToInt32(amc));
                        }
                        else
                        {
                            dt1 = dt1.AddDays(364);
                        }
                        string s = dt1.ToString("dd-MM-yyyy");
                        string startdate = DateTime.Now.ToString("dd-MM-yyyy");
                        string fy1 = defaultdateformate.convertdate(startdate, "dd-MM-yyyy", "yyyy-MM-dd", '-');
                        string fy2 = defaultdateformate.convertdate(s, "dd-MM-yyyy", "yyyy-MM-dd", '-');
                        con.execute("UPDATE [businessplus].[tblRegistrationMaster] SET regdate='" + fy1 + "',regtime='" + DateTime.Now.ToString("H:mm tt") + "',reregdate='" + fy1 + "',reregtime='" + DateTime.Now.ToString("H:mm tt") + "',expdate='" + fy2 + "',exptime='" + DateTime.Now.ToString("H:mm tt") + "' where srno='" + txtserialkey.Text + "'", constring);
                        Encryptexpdate(s);
                        Encryptregdate(d);
                        Encrypststus(statu);
                        ods.execute("UPDATE [tblreg] SET [d10]='" + regdate + "',[d14] ='" + expdate + "',[d16]='" + status + "'");
                        DataTable renewal = con.getdataset("select * from tblRegistrationMaster where isactive=1 and Srno='" + txtserialkey.Text + "'", constring);
                        if (renewal.Rows.Count > 0)
                        {
                            con.execute("UPDATE [dbo].[Renewal] SET  CompanyName='" + renewal.Rows[0]["companyname"].ToString() + "',CompanyAddress='" + renewal.Rows[0]["companyaddress"].ToString() + "',Email='" + renewal.Rows[0]["email"].ToString() + "',Contact='" + renewal.Rows[0]["contact"].ToString() + "',MAC='" + renewal.Rows[0]["mac"].ToString() + "',PartnerCode='" + renewal.Rows[0]["partnercode"].ToString() + "',LicenceNo='" + renewal.Rows[0]["licanceno"].ToString() + "',RegDate='" + fy1 + "',RegTime='" + DateTime.Now.ToString("H:mm tt") + "',ReRegiDate='" + fy1 + "',ReRegiTime='" + DateTime.Now.ToString("H:mm tt") + "',ExpDate='" + fy2 + "',ExpTime='" + DateTime.Now.ToString("H:mm tt") + "',NewExpdate='" + fy2 + "',RenewalSerialNumber='" + txtextendkey.Text + "' where SRNo='" + txtserialkey.Text + "'", constring);
                            SendingEmail(renewal.Rows[0]["companyname"].ToString(), renewal.Rows[0]["email"].ToString(), "Thank You For Re New Registration", "Your Licence No is '" + renewal.Rows[0]["licanceno"].ToString() + "'");
                        }
                        ods.execute("UPDATE [tblBody] SET [tblBody]='" + "0" + "'");
                        MessageBox.Show("Registration Sucessfully Done");
                      //  SendingEmail(renewal.Rows[0]["companyname"].ToString(), renewal.Rows[0]["email"].ToString(), "Thank You For Re New", "Your Licanceno is '" + renewal.Rows[0]["licanceno"].ToString() + "'");
                        
                        Application.Exit();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Serial No. Please Try Again");
                        txtserialkey.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Serial No. Please Try Again");
                    txtserialkey.Focus();
                }
                //Decrypt(txtextendkey.Text);
                //string d = DateTime.Now.ToString(Master.dateformate);
                //DateTime dt = Convert.ToDateTime(d);
                //dt = dt.AddDays(364);
                //string s = dt.ToString(Master.dateformate);
                //con.execute("UPDATE [businessplus].[tblRegistrationMaster] SET licanceno='" + txtextendkey.Text + "',regdate='" + DateTime.Now.ToString(Master.dateformate) + "',regtime='" + DateTime.Now.ToString("H:mm tt") + "',reregdate='" + DateTime.Now.ToString(Master.dateformate) + "',reregtime='" + DateTime.Now.ToString("H:mm tt") + "',expdate='" + s + "',exptime='" + DateTime.Now.ToString("H:mm tt") + "' where srno='" + txtserialkey.Text + "'");
                //ods.execute("UPDATE [Company] SET licanceno='" + txtextendkey.Text + "',regdate='" + DateTime.Now.ToString(Master.dateformate) + "',regtime='" + DateTime.Now.ToString("H:mm tt") + "',reregdate='" + DateTime.Now.ToString(Master.dateformate) + "',reregtime='" + DateTime.Now.ToString("H:mm tt") + "',expdate='" + s + "',exptime='" + DateTime.Now.ToString("H:mm tt") + "',status='" + "Reg" + "',installationdate='" + DateTime.Now.ToString(Master.dateformate) + "' where CompanyID='" + Master.companyId + "'");
                //MessageBox.Show("Registration Sucessfully Done");
                //master.RemoveCurrentTab();

                //SendingEmail(cname,cemail, "Thank You For Re New", "Your Licanceno is '" + txtextendkey.Text + "'");
            }
            catch
            {
                Application.Exit();
            }
        }

        private void txtserialkey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtextendkey.Focus();
            }
        }

        private void txtextendkey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnok1.Focus();
            }
        }

        private void txtserialkey_Enter(object sender, EventArgs e)
        {
            txtserialkey.BackColor = Color.LightYellow;
        }

        private void txtserialkey_Leave(object sender, EventArgs e)
        {
            txtserialkey.BackColor = Color.White;
        }

        private void txtextendkey_Enter(object sender, EventArgs e)
        {
            txtextendkey.BackColor = Color.LightYellow;
        }

        private void txtextendkey_Leave(object sender, EventArgs e)
        {
            txtextendkey.BackColor = Color.White;
        }
        public static string id = string.Empty;
        public static string days = string.Empty;
        public static string srno = string.Empty;
        private string Encryptid(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                    id = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        private string Encryptdays(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                    days = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Decryptsrno(string cipherText)
        {
            string EncryptionKey = "00";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    srno = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        private void btnregonline_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtserialkey.Text))
                {
                    //Decryptsrno(txtserialkey.Text);
                    string amcday= conn.ExecuteScalar("select amcday from updatedatabase");
                    if (string.IsNullOrEmpty(amcday))
                    {
                        amcday = "364";
                    }
                    DataTable dt = con.getdataset("select id from tblRegistrationMaster where isactive=1 and srno='" + txtserialkey.Text + "'", constring);
                    if (dt.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[0]["id"].ToString()))
                        {
                            Encryptid(dt.Rows[0]["id"].ToString());
                            Encryptdays(amcday);
                            System.Diagnostics.Process.Start("http://admin.totalbusinessplus.com/SoftwarePayment/Index?Id=" + id + "&Days=" + days + "");
                            Application.Exit();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Serial No. Please Try Again");
                            txtserialkey.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid Serial No. Please Try Again");
                        txtserialkey.Focus();
                    }
                }
            }
            catch
            {
            }
        }






    }
}
