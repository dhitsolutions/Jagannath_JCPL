using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using LoggingFramework;

namespace RamdevSales
{
    public partial class RegisterNow : Form
    {
        private Master master;
        private TabControl tabControl;
        SqlConnection constring = new SqlConnection("Data Source=184.168.47.17;Initial Catalog=BusinessPlus;User ID=Businessplus;Password=Businessplus1!");
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["servercon"].ToString());
        ServerConnection con = new ServerConnection();
        OleDbSettings ods = new OleDbSettings();
        DataTable dt,dt100 = new DataTable();
        public static string serialno = string.Empty;
        public static string cname = string.Empty;
        public static string caddress = string.Empty;
        public static string email = string.Empty;
        public static string contact = string.Empty;
        public static string regdate = string.Empty;
        public static string regtime = string.Empty;
        public static string reregdate = string.Empty;
        public static string reregtime = string.Empty;
        public static string expdate = string.Empty;
        public static string exptime = string.Empty;
        public static string status = string.Empty;
        public static string licanceno=string.Empty;
        public static string licancetype = string.Empty;
        public static String sMacAddress = string.Empty;
        datetime defaultdateformate = new datetime();
        public RegisterNow()
        {
            InitializeComponent();
           // txtsystemid.Focus();
            this.ActiveControl = txtsystemid;
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
        public RegisterNow(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
           // txtsystemid.Focus();
            this.ActiveControl = txtsystemid;
        }

        private void txtsystemid_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnnext.Focus();
                }
            }
            catch
            {
            }
        }



        private void txtpartnercode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtpartnercode.Text == "")
                    {
                        lblpartner.Visible = true;
                       
                    }
                    else
                    {
                        lblpartner.Visible = false;
                        btnsubmit.Focus();
                    }
                   
                }
            }
            catch
            {
            }
        }

        private void RegisterNow_Load(object sender, EventArgs e)
        {
            try
            {
                pnlreg.Visible = false;
                pnlserial.Visible = true;
                //txtsystemid.Focus();
                this.ActiveControl = txtsystemid;
            }
            catch
            {
            }
        }
        public void clearall()
        {
            txtsystemid.Text = "";
            txtcname.Text = "";
            txtcaddress.Text = "";
            txtcontact.Text = "";
            txtemail.Text = "";
            txtpartnercode.Text = "";
            sMacAddress = "";
            licanceno = "";
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
        string d, t,sa,statu="Reg",licancecount="";
        private void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                dt100 = con.getdataset("select * from tblRegistrationMaster where isactive=1 and srno='" + txtsystemid.Text + "' and partnercode='"+txtpartnercode.Text+"'", constring);
                LogGenerator.Info("select * from tblRegistrationMaster where isactive=1 and srno='" + txtsystemid.Text + "' and partnercode='" + txtpartnercode.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    if (txtemail.Text == "" || txtcontact.Text == "" || txtpartnercode.Text == "")
                    {
                        if (txtpartnercode.Text == "")
                        {
                            lblpartner.Visible = true;

                        }
                        else
                        {
                            lblpartner.Visible = false;

                        }
                        if (txtemail.Text == "")
                        {
                            lblemail.Visible = true;
                        }
                        else
                        {
                            lblemail.Visible = false;
                        }
                        if (txtcontact.Text == "")
                        {
                            lblcontact.Visible = true;

                        }
                        else
                        {
                            lblcontact.Visible = false;
                        }
                    }
                    else
                    {
                        if (dt.Rows[0]["srno"].ToString() == txtsystemid.Text && dt.Rows[0]["isused"].ToString() == "True")
                        {
                            //d = Convert.ToDateTime(dt.Rows[0]["regdate"]).ToString("dd-MM-yyyy");
                            d = DateTime.Parse(dt.Rows[0]["regdate"].ToString()).ToString("yyyy-MM-dd");
                            d = defaultdateformate.convertdate(Convert.ToString(d), "dd-MM-yyyy", "yyyy-MM-dd", '-');
                            t = dt.Rows[0]["regtime"].ToString();
                            //sa = Convert.ToDateTime(dt.Rows[0]["expdate"]).ToString("dd-MM-yyyy");
                            sa = DateTime.Parse(dt.Rows[0]["expdate"].ToString()).ToString("yyyy-MM-dd");
                            sa = defaultdateformate.convertdate(Convert.ToString(sa), "dd-MM-yyyy", "yyyy-MM-dd", '-');
                            Encryptserialno(txtsystemid.Text);
                            Encryptcname(txtcname.Text);
                            Encryptcaddress(txtcaddress.Text);
                            Encryptcemail(txtemail.Text);
                            Encryptcontact(txtcontact.Text);
                            Encrypt(sMacAddress);
                            Encryptregdate(d);
                            Encryptregtime(t);
                            Encrypststus(statu);
                            Encryptexpdate(sa);
                            Encryptlicancetype(dt.Rows[0]["licencetype"].ToString());

                        }
                        else
                        {
                            d = DateTime.Now.ToString("dd-MM-yyyy");
                            d = defaultdateformate.convertdate(d, "dd-MM-yyyy", "yyyy-MM-dd", '-');
                            t = DateTime.Now.ToString("H:mm tt");
                            DateTime dt1 = Convert.ToDateTime(d);
                            dt1 = dt1.AddDays(364);
                            sa = dt1.ToString("dd-MM-yyyy");
                            Encryptserialno(txtsystemid.Text);
                            Encryptcname(txtcname.Text);
                            Encryptcaddress(txtcaddress.Text);
                            Encryptcemail(txtemail.Text);
                            Encryptcontact(txtcontact.Text);
                            Encrypt(sMacAddress);
                            Encryptregdate(d);
                            Encryptregtime(t);
                            Encryptexpdate(sa);
                            Encrypststus(statu);
                            Encryptlicancetype(dt.Rows[0]["licencetype"].ToString());
                        }
                        string startdate = DateTime.Now.ToString("dd-MM-yyyy");
                        string fy1 = defaultdateformate.convertdate(startdate, "dd-MM-yyyy", "yyyy-MM-dd", '-');
                        string fy2 = defaultdateformate.convertdate(sa, "dd-MM-yyyy", "yyyy-MM-dd", '-');
                        if (dt.Rows[0]["srno"].ToString() == txtsystemid.Text && dt.Rows[0]["isused"].ToString() == "True")
                        {
                           // ods.execute("UPDATE [tblreg] SET [d1] ='" + serialno + "',d2='1',d3='" + cname + "',d4='" + caddress + "',d5='" + email + "',d6='" + contact + "',d7='" + sMacAddress + "',d8='" + txtpartnercode.Text + "',d9='" + licanceno + "',d10='" + regdate + "',d11='" + regtime + "',d12='" + regdate + "',d13='" + regtime + "',d14='" + expdate + "',d15='" + regtime + "',d16='" + status + "'");
                            ods.execute("UPDATE [tblreg] SET [d1] ='" + serialno + "',d2='1',d3='" + cname + "',d4='" + caddress + "',d5='" + email + "',d6='" + contact + "',d7='" + sMacAddress + "',d9='" + licanceno + "',d10='" + regdate + "',d11='" + regtime + "',d12='" + regdate + "',d13='" + regtime + "',d14='" + expdate + "',d15='" + regtime + "',d16='" + status + "',d19='" + licancetype + "',d20='" + dt.Rows[0]["Usercount"].ToString() + "'");
                        }
                        else
                        {
                           // con.execute("UPDATE [businessplus].[tblRegistrationMaster] SET [srno] ='" + txtsystemid.Text + "',isused='1',companyname='" + txtcname.Text + "',companyaddress='" + txtcaddress.Text + "',email='" + txtemail.Text + "',contact='" + txtcontact.Text + "',mac='" + sMacAddress + "',partnercode='" + txtpartnercode.Text + "',licanceno='" + licanceno + "',regdate='" + fy1 + "',regtime='" + DateTime.Now.ToString("H:mm tt") + "',reregdate='" + fy1 + "',reregtime='" + DateTime.Now.ToString("H:mm tt") + "',expdate='" + fy2 + "',exptime='" + DateTime.Now.ToString("H:mm tt") + "' where srno='" + txtsystemid.Text + "'", constring);
                            //ods.execute("UPDATE [tblreg] SET [d1] ='" + serialno + "',d2='1',d3='" + cname + "',d4='" + caddress + "',d5='" + email + "',d6='" + contact + "',d7='" + sMacAddress + "',d8='" + txtpartnercode.Text + "',d9='" + licanceno + "',d10='" + regdate + "',d11='" + regtime + "',d12='" + regdate + "',d13='" + regtime + "',d14='" + expdate + "',d15='" + regtime + "',d16='" + status + "'");
                            if (dt.Rows[0]["licencetype"].ToString() == "Single User")
                            {
                                Double l = Convert.ToDouble(dt.Rows[0]["Usercount"].ToString());
                                licancecount = Convert.ToString(1);
                            }
                            else
                            {
                                Double l = Convert.ToDouble(dt.Rows[0]["Usercount"].ToString());
                                licancecount = Convert.ToString(l + 1);
                            }
                            con.execute("UPDATE [businessplus].[tblRegistrationMaster] SET [srno] ='" + txtsystemid.Text + "',isused='1',companyname='" + txtcname.Text + "',companyaddress='" + txtcaddress.Text + "',email='" + txtemail.Text + "',contact='" + txtcontact.Text + "',mac='" + sMacAddress + "',licanceno='" + licanceno + "',regdate='" + fy1 + "',regtime='" + DateTime.Now.ToString("H:mm tt") + "',reregdate='" + fy1 + "',reregtime='" + DateTime.Now.ToString("H:mm tt") + "',expdate='" + fy2 + "',exptime='" + DateTime.Now.ToString("H:mm tt") + "',licencetype='" + dt.Rows[0]["licencetype"].ToString() + "',Usercount='" + licancecount + "' where srno='" + txtsystemid.Text + "'", constring);
                            string a = "UPDATE [businessplus].[tblRegistrationMaster] SET [srno] ='" + txtsystemid.Text + "',isused='1',companyname='" + txtcname.Text + "',companyaddress='" + txtcaddress.Text + "',email='" + txtemail.Text + "',contact='" + txtcontact.Text + "',mac='" + sMacAddress + "',licanceno='" + licanceno + "',regdate='" + fy1 + "',regtime='" + DateTime.Now.ToString("H:mm tt") + "',reregdate='" + fy1 + "',reregtime='" + DateTime.Now.ToString("H:mm tt") + "',expdate='" + fy2 + "',exptime='" + DateTime.Now.ToString("H:mm tt") + "',licencetype='" + dt.Rows[0]["licencetype"].ToString() + "',Usercount='" + licancecount + "' where srno='" + txtsystemid.Text + "'";
                            LogGenerator.Info(a);
                            ods.execute("UPDATE [tblreg] SET [d1] ='" + serialno + "',d2='1',d3='" + cname + "',d4='" + caddress + "',d5='" + email + "',d6='" + contact + "',d7='" + sMacAddress + "',d9='" + licanceno + "',d10='" + regdate + "',d11='" + regtime + "',d12='" + regdate + "',d13='" + regtime + "',d14='" + expdate + "',d15='" + regtime + "',d16='" + status + "',d19='" + licancetype + "',d20='" + licancecount + "'");

                        }
                        MessageBox.Show("Registration Successfully Done");
                        SendingEmail(txtcname.Text, txtemail.Text, "Thank You For Registration", "Your Licence No is '" + licanceno + "'");
                        clearall();
                        master.RemoveCurrentTab();
                        Application.Exit();

                    }
                }
                else
                {
                    MessageBox.Show("Invalid Partner Code. Please Try Again");
                    txtpartnercode.Focus();
                }
                
            }

            catch
            {
            }
        }
        public void createlicanceno()
        {
            if (sMacAddress.Length == 12)
            {
               // Int64 refcode = Convert.ToInt64(sMacAddress);

              //  refcode = 999999999999 - refcode;
                string rf_str = Convert.ToString(sMacAddress);
                string add = "";
                for (int i = 0; i < 12 - rf_str.Length; i++)
                {
                    add = add + "0";
                }
                rf_str = add + rf_str;
                string s1 = rf_str.Substring(5, 3);
                string s2 = rf_str.Substring(0, 2);
                string s3 = rf_str.Substring(10, 2);
                string s4 = rf_str.Substring(2, 3);
                string s5 = rf_str.Substring(8, 2);

                string code = s1 + s2 + s3 + s4 + s5;


                licanceno = code;
            }
           
        }
        public static string Encryptserialno(string clearText)
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
                    serialno = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Encryptcname(string clearText)
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
                    cname = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Encryptlicancetype(string clearText)
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
                    licancetype = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Encryptcaddress(string clearText)
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
                    caddress = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Encryptcemail(string clearText)
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
                    email = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Encryptcontact(string clearText)
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
                    contact = Convert.ToBase64String(ms.ToArray());
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
        public static string Encryptregtime(string clearText)
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
                    regtime = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
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
                    licanceno = Convert.ToBase64String(ms.ToArray());
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
        public string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();

                }
            } return sMacAddress;
        }
        private void btnnext_Click(object sender, EventArgs e)
        {
            try
            {
                dt = con.getdataset("select * from tblRegistrationMaster where isactive=1 and srno='"+txtsystemid.Text+"'",constring);
                LogGenerator.Info("select * from tblRegistrationMaster where isactive=1 and srno='" + txtsystemid.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["srno"].ToString() == txtsystemid.Text && dt.Rows[0]["isused"].ToString() == "False")
                    {
                        pnlreg.Visible = true;
                        pnlserial.Visible = false;
                        txtcname.Focus();
                        GetMACAddress();
                        txtpartnercode.Text = dt.Rows[0]["partnercode"].ToString();
                    }
                    else
                    {
                        GetMACAddress();
                        if (dt.Rows[0]["mac"].ToString() == sMacAddress)
                        {
                            pnlreg.Visible = true;
                            pnlserial.Visible = false;
                            txtcname.Focus();
                            txtcname.Text = dt.Rows[0]["companyname"].ToString();
                            txtcaddress.Text = dt.Rows[0]["companyaddress"].ToString();
                            txtcontact.Text = dt.Rows[0]["contact"].ToString();
                            txtemail.Text = dt.Rows[0]["email"].ToString();
                            txtpartnercode.Text = dt.Rows[0]["partnercode"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Serial No. Please Try Again");
                            txtsystemid.Focus();
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Invalid Serial No. Please Try Again");
                    txtsystemid.Focus();

                }
            }
            catch
            {
            }
        }

        private void txtcname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter)
                {
                    txtcaddress.Focus();
                }
            }
            catch
            {
            }
        }

        private void txtcaddress_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter)
                {
                    txtemail.Focus();
                }
            }
            catch
            {
            }
        }

        private void txtemail_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter)
                {
                    if (txtemail.Text == "")
                    {
                        lblemail.Visible = true;
                    }
                    else
                    {
                        lblemail.Visible = false;
                        txtcontact.Focus();
                    }
                   
                }
            }
            catch
            {
            }
        }

        private void txtcontact_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtcontact.Text == "")
                    {
                        lblcontact.Visible = true;
                      
                    }
                    else
                    {
                        lblcontact.Visible = false;
                        txtpartnercode.Focus();
                    }
                   
                }
            }
            catch
            {
            }
        }

        private void txtcontact_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtemail_Validating(object sender, CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex rEmail = new System.Text.RegularExpressions.Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            if (txtemail.Text.Length > 0)
            {
                if (!rEmail.IsMatch(txtemail.Text))
                {
                    MessageBox.Show("Please provide valid email address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtemail.Text = "";
                    txtemail.Focus();
                }
            }
        }

        private void txtcname_Enter(object sender, EventArgs e)
        {
            txtcname.BackColor = Color.LightYellow;
        }

        private void txtcname_Leave(object sender, EventArgs e)
        {
            txtcname.BackColor = Color.White;
        }

        private void txtemail_Enter(object sender, EventArgs e)
        {
            txtemail.BackColor = Color.LightYellow;
        }

        private void txtemail_Leave(object sender, EventArgs e)
        {
            txtemail.BackColor = Color.White;
        }

        private void txtcaddress_Enter(object sender, EventArgs e)
        {
            txtcaddress.BackColor = Color.LightYellow;
        }

        private void txtcaddress_Leave(object sender, EventArgs e)
        {
            txtcaddress.BackColor = Color.White;
        }

        private void txtcontact_Enter(object sender, EventArgs e)
        {
            txtcontact.BackColor = Color.LightYellow;
        }

        private void txtcontact_Leave(object sender, EventArgs e)
        {
            txtcontact.BackColor = Color.White;
        }

        private void txtpartnercode_Enter(object sender, EventArgs e)
        {
            txtpartnercode.BackColor = Color.LightYellow;
        }

        private void txtpartnercode_Leave(object sender, EventArgs e)
        {
            txtpartnercode.BackColor = Color.White;
        }

        private void btnnext_MouseEnter(object sender, EventArgs e)
        {
            btnnext.UseVisualStyleBackColor = false;
            btnnext.BackColor = Color.FromArgb(20,209,82);
            btnnext.ForeColor = Color.White;
        }

        private void btnnext_MouseLeave(object sender, EventArgs e)
        {
            btnnext.UseVisualStyleBackColor = true;
            btnnext.BackColor = Color.FromArgb(51, 153, 255);
            btnnext.ForeColor = Color.White;
        }

        private void btnnext_Enter(object sender, EventArgs e)
        {
            btnnext.UseVisualStyleBackColor = false;
            btnnext.BackColor = Color.FromArgb(20, 209, 82);
            btnnext.ForeColor = Color.White;
        }

        private void btnnext_Leave(object sender, EventArgs e)
        {
            btnnext.UseVisualStyleBackColor = true;
            btnnext.BackColor = Color.FromArgb(51, 153, 255);
            btnnext.ForeColor = Color.White;
        }

        private void btnsubmit_Enter(object sender, EventArgs e)
        {
            btnsubmit.UseVisualStyleBackColor = false;
            btnsubmit.BackColor = Color.YellowGreen;
            btnsubmit.ForeColor = Color.White;
        }

        private void btnsubmit_Leave(object sender, EventArgs e)
        {
            btnsubmit.UseVisualStyleBackColor = true;
            btnsubmit.BackColor = Color.FromArgb(51, 153, 255);
            btnsubmit.ForeColor = Color.White;
        }

        private void btnsubmit_MouseEnter(object sender, EventArgs e)
        {
            btnsubmit.UseVisualStyleBackColor = false;
            btnsubmit.BackColor = Color.YellowGreen;
            btnsubmit.ForeColor = Color.White;
        }

        private void btnsubmit_MouseLeave(object sender, EventArgs e)
        {
            btnsubmit.UseVisualStyleBackColor = true;
            btnsubmit.BackColor = Color.FromArgb(51, 153, 255);
            btnsubmit.ForeColor = Color.White;
        }

        private void btnClose_Enter(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = false;
            btnClose.BackColor = Color.FromArgb(248, 152, 94);
            btnClose.ForeColor = Color.White;
        }

        private void btnClose_Leave(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = true;
            btnClose.BackColor = Color.FromArgb(51, 153, 255);
            btnClose.ForeColor = Color.White;
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = false;
            btnClose.BackColor = Color.FromArgb(248, 152, 94);
            btnClose.ForeColor = Color.White;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = true;
            btnClose.BackColor = Color.FromArgb(51, 153, 255);
            btnClose.ForeColor = Color.White;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }

        private void txtsystemid_Enter(object sender, EventArgs e)
        {
            txtsystemid.BackColor = Color.LightYellow;
        }

        private void txtsystemid_Leave(object sender, EventArgs e)
        {
            txtsystemid.BackColor = Color.White;
        }
        DataSet ds, ds2 = new DataSet();
        public static string decrypexpdate;
        public static string Encryptexpdate1 = string.Empty;
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
                    decrypexpdate = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        public static string Encryptupdatedexpdate(string clearText)
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
                    Encryptexpdate1 = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        DataTable expcount = new DataTable();
        private void brntrymore_Click(object sender, EventArgs e)
        {
            try
            {
                ds2 = ods.getdata("select * from tblreg");
                DataTable expdate = new DataTable();
                expdate = ds2.Tables[0];
                Decryptexpdate(expdate.Rows[0]["d18"].ToString());
                string newexpdate = decrypexpdate;
                newexpdate = defaultdateformate.convertdate(Convert.ToString(newexpdate), "dd-MM-yyyy", "yyyy-MM-dd", '-');
                DateTime dt2 = Convert.ToDateTime(newexpdate);
                dt2 = Convert.ToDateTime(newexpdate).AddDays(7);
                string exd = dt2.ToString("dd-MM-yyyy");
                Encryptupdatedexpdate(exd);
                ods.execute("UPDATE [tblreg] SET [d18] ='" + Encryptexpdate1 + "'");
                if (expcount.Rows.Count > 0)
                {
                    ods.execute("UPDATE [SQLSetting] SET [OT4] ='" + "1" + "'");
                }
                else
                {
                    ods.execute("INSERT INTO [SQLSetting]([OT4])VALUES('" + "1" + "')");
                }
                Application.Exit();


            }
            catch
            {
            }
        }


    }
}
